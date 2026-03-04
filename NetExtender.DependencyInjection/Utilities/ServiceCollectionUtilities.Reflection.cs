// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NetExtender.Cecil;
using NetExtender.DependencyInjection;
using NetExtender.DependencyInjection.Exceptions;
using NetExtender.DependencyInjection.Interfaces;
using NetExtender.Types.Comparers;
using NetExtender.Types.Entities;
using NetExtender.Exceptions;
using NetExtender.Utilities.Core;

namespace NetExtender.Utilities.Types
{
    public static partial class ServiceCollectionUtilities
    {
        public static ServiceAmbiguousHandler AmbiguousHandler { get; set; } = ServiceAmbiguousHandler.Throw;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IReadOnlyCollection<ServiceDescriptor> Create(MonoCecilType @interface, MonoCecilType type, ServiceLifetime lifetime, Options options)
        {
            return Create(@interface, type, null, lifetime, options);
        }

        // ReSharper disable once CognitiveComplexity
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static IReadOnlyCollection<ServiceDescriptor> Create(MonoCecilType @interface, MonoCecilType type, Object? key, ServiceLifetime lifetime, Options options)
        {
            if (@interface is null)
            {
                throw new ArgumentNullException(nameof(@interface));
            }

            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (type.Type is not { IsAbstract: false } implementation)
            {
                return ImmutableArray<ServiceDescriptor>.Empty;
            }

            static ServiceStrategy GetStrategyFromInterface(MonoCecilType @interface)
            {
                if (!@interface.IsGenericType)
                {
                    return ServiceStrategy.New;
                }

                Type? generic = @interface.GetGenericTypeDefinition();
                if (generic is not null && InterfaceToStrategy.TryGetValue(generic, out ServiceStrategy strategy))
                {
                    return strategy;
                }

                return ServiceStrategy.New;
            }

            ServiceStrategy strategy;
            if (@interface.Type is not { IsInterface: true } service)
            {
                service = @interface.Type;
                strategy = GetStrategyFromInterface(@interface);
                return ImmutableList<ServiceDescriptor>.Empty.AddIfNotNull(service is not null ? Metadata.Set(new ServiceDescriptor(service, key, implementation, lifetime), strategy, ServiceDependencyInfoAttribute.ToSingle(strategy), 0) : null);
            }

            if (options.Inherit.TryGetValue(@interface, out ReflectionInheritResult? result) && !result.Inherit.Types.Contains(type))
            {
                throw new TypeNotSupportedException(type, $"Type '{type}' must implement interface '{@interface}'.");
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            static Boolean IsServiceDependency(MonoCecilType @interface)
            {
                return @interface.TryGetGenericTypeDefinition() == typeof(IDependencyService<>);
            }

            Dictionary<(MonoCecilType Type, Object? Key), ServiceDescriptor> descriptors = new Dictionary<(MonoCecilType Type, Object? Key), ServiceDescriptor>();

            foreach (MonoCecilType argument in @interface.Interfaces.Where<MonoCecilType>(IsServiceDependency).Select(static dependency => dependency.Generics[0]))
            {
                if (!argument.IsInterface || !options.Inherit.TryGetValue(argument, out result) || !result.Inherit.Types.Contains(type))
                {
                    throw new TypeNotSupportedException(type, $"Type '{type}' must implement interface '{argument}'.");
                }

                if (argument.Type is { } argtype)
                {
                    strategy = GetStrategyFromInterface(argument);
                    descriptors.TryAdd((argument, key), Metadata.Set(new ServiceDescriptor(argtype, key, implementation, lifetime), strategy, ServiceDependencyInfoAttribute.ToSingle(strategy), 0));
                }
            }

            strategy = GetStrategyFromInterface(@interface);
            descriptors.TryAdd((@interface, key), Metadata.Set(new ServiceDescriptor(service, key, implementation, lifetime), strategy, ServiceDependencyInfoAttribute.ToSingle(strategy), 0));
            return descriptors.Values.ToImmutableList();
        }

        private static readonly ImmutableDictionary<Type, ServiceStrategy> InterfaceToStrategy = new Dictionary<Type, ServiceStrategy>
        {
            { typeof(IDependencyService), ServiceStrategy.New },
            { typeof(IDependencyService<>), ServiceStrategy.New },
            { typeof(IMultiDependencyService<>), ServiceStrategy.Multi },
            { typeof(ISingleDependencyService<>), ServiceStrategy.Single },
            { typeof(IReplaceDependencyService<>), ServiceStrategy.Replace },
            { typeof(IMultiReplaceDependencyService<>), ServiceStrategy.MultiReplace },

            { typeof(ITransient), ServiceStrategy.New },
            { typeof(ITransient<>), ServiceStrategy.New },
            { typeof(IMultiTransient<>), ServiceStrategy.Multi },
            { typeof(ISingleTransient<>), ServiceStrategy.Single },
            { typeof(IReplaceTransient<>), ServiceStrategy.Replace },
            { typeof(IMultiReplaceTransient<>), ServiceStrategy.MultiReplace },

            { typeof(IScoped), ServiceStrategy.New },
            { typeof(IScoped<>), ServiceStrategy.New },
            { typeof(IMultiScoped<>), ServiceStrategy.Multi },
            { typeof(ISingleScoped<>), ServiceStrategy.Single },
            { typeof(IReplaceScoped<>), ServiceStrategy.Replace },
            { typeof(IMultiReplaceScoped<>), ServiceStrategy.MultiReplace },

            { typeof(ISingleton), ServiceStrategy.New },
            { typeof(ISingleton<>), ServiceStrategy.New },
            { typeof(IMultiSingleton<>), ServiceStrategy.Multi },
            { typeof(ISingleSingleton<>), ServiceStrategy.Single },
            { typeof(IReplaceSingleton<>), ServiceStrategy.Replace },
            { typeof(IMultiReplaceSingleton<>), ServiceStrategy.MultiReplace }
        }.ToImmutableDictionary();

        private static readonly ImmutableDictionary<Type, ServiceLifetime> InterfaceToLifetime = new Dictionary<Type, ServiceLifetime>
        {
            { typeof(ITransient), ServiceLifetime.Transient },
            { typeof(ITransient<>), ServiceLifetime.Transient },
            { typeof(IMultiTransient<>), ServiceLifetime.Transient },
            { typeof(ISingleTransient<>), ServiceLifetime.Transient },
            { typeof(IReplaceTransient<>), ServiceLifetime.Transient },
            { typeof(IMultiReplaceTransient<>), ServiceLifetime.Transient },

            { typeof(IScoped), ServiceLifetime.Scoped },
            { typeof(IScoped<>), ServiceLifetime.Scoped },
            { typeof(IMultiScoped<>), ServiceLifetime.Scoped },
            { typeof(ISingleScoped<>), ServiceLifetime.Scoped },
            { typeof(IReplaceScoped<>), ServiceLifetime.Scoped },
            { typeof(IMultiReplaceScoped<>), ServiceLifetime.Scoped },

            { typeof(ISingleton), ServiceLifetime.Singleton },
            { typeof(ISingleton<>), ServiceLifetime.Singleton },
            { typeof(IMultiSingleton<>), ServiceLifetime.Singleton },
            { typeof(ISingleSingleton<>), ServiceLifetime.Singleton },
            { typeof(IReplaceSingleton<>), ServiceLifetime.Singleton },
            { typeof(IMultiReplaceSingleton<>), ServiceLifetime.Singleton }
        }.ToImmutableDictionary();

        // ReSharper disable once CognitiveComplexity
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static IServiceCollection Scan(this IServiceCollection services, Options options)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            ConcurrentHashSet<ServiceDescriptor> descriptors = new ConcurrentHashSet<ServiceDescriptor>(ServiceDescriptorEqualityComparer.Implementation);

            if (options.Inherit.TryGetValue(typeof(IDependencyService), out ReflectionInheritResult? dependency))
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                static void CallStaticConstructor(MonoCecilType type)
                {
                    type.Type?.CallStaticConstructor();
                }

                Parallel.ForEach<MonoCecilType>(dependency.Inherit.Interfaces, CallStaticConstructor);
            }

            [MethodImpl(MethodImplOptions.AggressiveOptimization)]
            void Handler(MonoCecilType? type)
            {
                if (type?.Type is not { IsAbstract: false } implementation)
                {
                    return;
                }

                TypeSet interfaces = type.Interfaces.Where(options, static (@interface, options) => options.Interfaces.Contains(@interface.TryGetGenericTypeDefinition()!));

                if (interfaces.Count > 0)
                {
                    Split split = new Split(type, options, interfaces);

                    foreach (MonoCecilType @interface in split.Transient)
                    {
                        descriptors.AddRange(Create(@interface, type, ServiceLifetime.Transient, options));
                    }

                    foreach (MonoCecilType @interface in split.Scoped)
                    {
                        descriptors.AddRange(Create(@interface, type, ServiceLifetime.Scoped, options));
                    }

                    foreach (MonoCecilType @interface in split.Singleton)
                    {
                        descriptors.AddRange(Create(@interface, type, ServiceLifetime.Singleton, options));
                    }

                    if (split.Lifetime is { } lifetime)
                    {
                        descriptors.Add(new ServiceDescriptor(implementation, implementation, lifetime));
                    }

                    interfaces = interfaces.Except(split.Transient).Except(split.Scoped).Except(split.Singleton);
                    foreach (MonoCecilType @interface in interfaces)
                    {
                        if (!@interface.IsGenericType || @interface.GetGenericTypeDefinition().Type is not { } generic)
                        {
                            continue;
                        }

                        if (!InterfaceToStrategy.TryGetValue(generic, out ServiceStrategy strategy) || !InterfaceToLifetime.TryGetValue(generic, out lifetime))
                        {
                            continue;
                        }

                        switch (@interface.Generics[0].Type)
                        {
                            case { IsInterface: true } service:
                            {
                                descriptors.Add(Metadata.Set(new ServiceDescriptor(service, null, implementation, lifetime), strategy, ServiceDependencyInfoAttribute.ToSingle(strategy), 0));
                                break;
                            }
                            case { IsInterface: false } service:
                            {
                                throw new TypeNotSupportedException(service, $"Type '{service}' must be interface.");
                            }
                            default:
                            {
                                break;
                            }
                        }

                        foreach (MonoCecilType @base in @interface.Interfaces)
                        {
                            if (!@base.IsGenericType || (generic = @base.GetGenericTypeDefinition().Type) is null)
                            {
                                continue;
                            }

                            if (!InterfaceToStrategy.TryGetValue(generic, out strategy) || !InterfaceToLifetime.TryGetValue(generic, out lifetime))
                            {
                                continue;
                            }

                            switch (@base.Generics[0].Type)
                            {
                                case { IsInterface: true } service:
                                {
                                    descriptors.Add(Metadata.Set(new ServiceDescriptor(service, null, implementation, lifetime), strategy, ServiceDependencyInfoAttribute.ToSingle(strategy), 0));
                                    break;
                                }
                                case { IsInterface: false } service:
                                {
                                    throw new TypeNotSupportedException(service, $"Type '{service}' must be interface.");
                                }
                                default:
                                {
                                    break;
                                }
                            }
                        }
                    }
                }

                foreach (ServiceDependencyInfoAttribute attribute in AttributeUtilities.GetCustomAttributes<ServiceDependencyInfoAttribute>(implementation))
                {
                    if (attribute.Interface is { } @interface)
                    {
                        foreach (ServiceDescriptor descriptor in Create(@interface, type, attribute.Key, attribute.Lifetime, options))
                        {
                            descriptors.Add(Metadata.Set(descriptor, attribute.Strategy, attribute.IsSingle, attribute.Order));
                        }
                    }

                    descriptors.Add(Metadata.Set(new ServiceDescriptor(implementation, implementation, attribute.Lifetime), attribute.Strategy, attribute.IsSingle, attribute.Order));
                }
            }

            Parallel.ForEach<MonoCecilType>(options.Source, Handler);
            return Verify(services, descriptors, options);
        }

        [SuppressMessage("ReSharper", "ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator")]
        private static Int32 CountDestination(List<ServiceDescriptorHandler> handlers)
        {
            Int32 count = 0;
            foreach (ServiceDescriptorHandler handler in handlers)
            {
                if (handler.Source is ServiceDescriptorHandler.Affiliation.Destination)
                {
                    count++;
                }
            }

            return count;
        }

        [SuppressMessage("ReSharper", "ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator")]
        private static Boolean IsSingleStrategy(List<ServiceDescriptorHandler> handlers)
        {
            foreach (ServiceDescriptorHandler handler in handlers)
            {
                if (handler.Source is ServiceDescriptorHandler.Affiliation.Destination && Metadata.GetStrategy(handler.Service) is ServiceStrategy.Single)
                {
                    return true;
                }
            }

            return false;
        }

        [SuppressMessage("ReSharper", "ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator")]
        private static Boolean IsSingleOrInvalidStrategy(List<ServiceDescriptorHandler> handlers)
        {
            foreach (ServiceDescriptorHandler handler in handlers)
            {
                if (handler.Source is ServiceDescriptorHandler.Affiliation.Destination && Metadata.GetStrategy(handler.Service) is default(ServiceStrategy) or ServiceStrategy.Single)
                {
                    return true;
                }
            }

            return false;
        }

        [SuppressMessage("ReSharper", "ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator")]
        private static Boolean HasAnyDestinationNewOrReplace(List<ServiceDescriptorHandler> handlers)
        {
            foreach (ServiceDescriptorHandler handler in handlers)
            {
                if (handler.Source is not ServiceDescriptorHandler.Affiliation.Destination)
                {
                    continue;
                }

                if (Metadata.GetStrategy(handler.Service) is ServiceStrategy.New or ServiceStrategy.Replace)
                {
                    return true;
                }
            }

            return false;
        }

        [SuppressMessage("ReSharper", "ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator")]
        private static IServiceCollection ApplyReplace(IServiceCollection services, ConcurrentHashSet<ServiceDescriptor> set)
        {
            HashSet<(Type ServiceType, Object? ServiceKey)> keys = new HashSet<(Type ServiceType, Object? ServiceKey)>();

            foreach (ServiceDescriptor descriptor in set)
            {
                if (Metadata.GetStrategy(descriptor) is ServiceStrategy.Replace or ServiceStrategy.MultiReplace)
                {
                    keys.Add((descriptor.ServiceType, descriptor.ServiceKey));
                }
            }

            if (keys.Count <= 0)
            {
                return services;
            }

            for (Int32 i = services.Count - 1; i >= 0; i--)
            {
                ServiceDescriptor descriptor = services[i];
                if (keys.Contains((descriptor.ServiceType, descriptor.ServiceKey)))
                {
                    services.RemoveAt(i);
                }
            }

            return services;
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static IServiceCollection VerifyIgnore(IServiceCollection services, ConcurrentHashSet<ServiceDescriptor> set)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (set is null)
            {
                throw new ArgumentNullException(nameof(set));
            }

            lock (services)
            {
                lock (set)
                {
                    return Sort(services.Add(set));
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        [SuppressMessage("ReSharper", "CognitiveComplexity")]
        private static IServiceCollection VerifyNew(IServiceCollection services, ConcurrentHashSet<ServiceDescriptor> set, ServiceAmbiguousHandler handler)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (set is null)
            {
                throw new ArgumentNullException(nameof(set));
            }

            ConcurrentDictionary<(MonoCecilType Type, Object? Key), List<ServiceDescriptorHandler>> counter = new ConcurrentDictionary<(MonoCecilType Type, Object? Key), List<ServiceDescriptorHandler>>();

            lock (services)
            {
                lock (set)
                {
                    foreach (ServiceDescriptor descriptor in set)
                    {
                        ServiceDescriptorHandler item = new ServiceDescriptorHandler(descriptor, ServiceDescriptorHandler.Affiliation.Destination);
                        List<ServiceDescriptorHandler> handlers = counter.GetOrNew((descriptor.ServiceType, descriptor.ServiceKey));

                        if (!handlers.Contains(item))
                        {
                            handlers.Add(item);
                        }
                    }

                    foreach (ServiceDescriptor descriptor in services)
                    {
                        if (counter.TryGetValue((descriptor.ServiceType, descriptor.ServiceKey), out List<ServiceDescriptorHandler>? handlers))
                        {
                            handlers.Add(new ServiceDescriptorHandler(descriptor, ServiceDescriptorHandler.Affiliation.Source));
                        }
                    }

                    Parallel.ForEach(counter.Values, [MethodImpl(MethodImplOptions.AggressiveOptimization)] static (handlers) =>
                    {
                        ServiceDescriptorHandler[] array = new ServiceDescriptorHandler[handlers.Count];

                        Int32 i = 0;
                        foreach (ServiceDescriptorHandler handler in handlers)
                        {
                            if (handler.Source is ServiceDescriptorHandler.Affiliation.Source)
                            {
                                array[i++] = handler;
                            }
                        }

                        foreach (ServiceDescriptorHandler handler in handlers)
                        {
                            if (handler.Source is ServiceDescriptorHandler.Affiliation.Destination)
                            {
                                array[i++] = handler;
                            }
                        }

                        handlers.Clear();
                        handlers.AddRange(array);
                    });

                    static Boolean RequireUseHandler(KeyValuePair<(MonoCecilType Type, Object? Key), List<ServiceDescriptorHandler>> pair)
                    {
                        if (pair.Value is not { Count: > 1 } handlers)
                        {
                            return false;
                        }

                        if (IsSingleStrategy(handlers))
                        {
                            return true;
                        }

                        if (HasAnyDestinationNewOrReplace(handlers))
                        {
                            return CountDestination(handlers) > 1;
                        }

                        return false;
                    }

                    static Boolean IsInvalid(KeyValuePair<(MonoCecilType Type, Object? Key), List<ServiceDescriptorHandler>> pair)
                    {
                        if (pair.Value is not { Count: > 1 } handlers)
                        {
                            return false;
                        }

                        if (IsSingleOrInvalidStrategy(handlers))
                        {
                            return true;
                        }

                        if (HasAnyDestinationNewOrReplace(handlers))
                        {
                            return CountDestination(handlers) > 1;
                        }

                        return false;
                    }

                    [SuppressMessage("ReSharper", "VariableHidesOuterVariable")]
                    void Handler(KeyValuePair<(MonoCecilType Type, Object? Key), List<ServiceDescriptorHandler>> pair)
                    {
                        List<ServiceDescriptorHandler> container = pair.Value;

                        Boolean conflict = IsSingleOrInvalidStrategy(container);
                        ServiceDescriptorHandler[] candidates = conflict ? container.ToArray() : container.Where(static handler => handler.Source is ServiceDescriptorHandler.Affiliation.Destination).ToArray();

                        if (candidates.Length <= 0 || handler.Invoke(candidates) is not { } select)
                        {
                            return;
                        }

                        if (conflict)
                        {
                            for (Int32 i = container.Count - 1; i >= 0; i--)
                            {
                                ServiceDescriptorHandler handler = container[i];
                                if (ServiceDescriptorEqualityComparer.Default.Equals(handler.Service, select))
                                {
                                    continue;
                                }

                                switch (handler.Source)
                                {
                                    case ServiceDescriptorHandler.Affiliation.Source:
                                        services.Remove(handler.Service);
                                        break;
                                    case ServiceDescriptorHandler.Affiliation.Destination:
                                        set.Remove(handler.Service);
                                        break;
                                    default:
                                        continue;
                                }

                                container.RemoveAt(i);
                            }

                            container.Clear();
                            container.Add(new ServiceDescriptorHandler(select, ServiceDescriptorHandler.Affiliation.Handle));
                            return;
                        }

                        for (Int32 i = container.Count - 1; i >= 0; i--)
                        {
                            ServiceDescriptorHandler handler = container[i];
                            if (handler.Source is not ServiceDescriptorHandler.Affiliation.Destination || ServiceDescriptorEqualityComparer.Default.Equals(handler.Service, select))
                            {
                                continue;
                            }

                            set.Remove(handler.Service);
                            container.RemoveAt(i);
                        }
                    }

                    static Exception Exception(KeyValuePair<(MonoCecilType Type, Object? Key), List<ServiceDescriptorHandler>> pair)
                    {
                        TypeComparer comparer = TypeComparer.NameOrdinalIgnoreCase;
                        IEnumerable<ServiceDescriptor> source = pair.Value.Unwrap();
                        String? message = source.OrderBy(static descriptor => descriptor.ImplementationType, comparer).ThenByDescending(static descriptor => descriptor.Lifetime).Select(static descriptor => $"{descriptor.ImplementationType?.Name}:{Metadata.GetOrder(descriptor)}[{Metadata.GetStrategy(descriptor)}] {descriptor.Lifetime}").GetString();
                        return new ScanAmbiguousException(message);
                    }

                    Parallel.ForEach(counter.Where(RequireUseHandler), Handler);

                    const String message = "Ambiguous registration for service type:";
                    Exception[] exceptions = counter.Where(IsInvalid).OrderBy(static pair => pair.Key.Type, TypeComparer.NameOrdinalIgnoreCase).Select(Exception).ToArray();

                    return exceptions.Length switch
                    {
                        0 => Sort(ApplyReplace(services, set).Add(set)),
                        1 => throw new ScanAmbiguousException(message + ' ' + exceptions[0].Message),
                        _ => throw new ScanAmbiguousException(message, new AggregateException(exceptions))
                    };
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        [SuppressMessage("ReSharper", "CognitiveComplexity")]
        private static IServiceCollection VerifyThrow(IServiceCollection services, ConcurrentHashSet<ServiceDescriptor> set, ServiceAmbiguousHandler handler)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (set is null)
            {
                throw new ArgumentNullException(nameof(set));
            }

            ConcurrentDictionary<(MonoCecilType Type, Object? Key), List<ServiceDescriptorHandler>> counter = new ConcurrentDictionary<(MonoCecilType Type, Object? Key), List<ServiceDescriptorHandler>>();

            lock (services)
            {
                lock (set)
                {
                    foreach (ServiceDescriptor descriptor in services)
                    {
                        ServiceDescriptorHandler item = new ServiceDescriptorHandler(descriptor, ServiceDescriptorHandler.Affiliation.Source);
                        counter.GetOrNew((descriptor.ServiceType, descriptor.ServiceKey)).Add(item);
                    }

                    foreach (ServiceDescriptor descriptor in set)
                    {
                        ServiceDescriptorHandler item = new ServiceDescriptorHandler(descriptor, ServiceDescriptorHandler.Affiliation.Destination);
                        List<ServiceDescriptorHandler> handlers = counter.GetOrNew((descriptor.ServiceType, descriptor.ServiceKey));

                        if (!handlers.Contains(item))
                        {
                            handlers.Add(item);
                        }
                    }

                    Parallel.ForEach(counter.Values, [MethodImpl(MethodImplOptions.AggressiveOptimization)] static (handlers) =>
                    {
                        ServiceDescriptorHandler[] array = new ServiceDescriptorHandler[handlers.Count];

                        Int32 i = 0;
                        foreach (ServiceDescriptorHandler handler in handlers)
                        {
                            if (handler.Source is ServiceDescriptorHandler.Affiliation.Source)
                            {
                                array[i++] = handler;
                            }
                        }

                        foreach (ServiceDescriptorHandler handler in handlers)
                        {
                            if (handler.Source is ServiceDescriptorHandler.Affiliation.Destination)
                            {
                                array[i++] = handler;
                            }
                        }

                        handlers.Clear();
                        handlers.AddRange(array);
                    });

                    static Boolean RequireUseHandler(KeyValuePair<(MonoCecilType Type, Object? Key), List<ServiceDescriptorHandler>> pair)
                    {
                        if (pair.Value is not { Count: > 1 } handlers)
                        {
                            return false;
                        }

                        Int32 destination = CountDestination(handlers);

                        if (destination == 0)
                        {
                            return true;
                        }

                        if (IsSingleStrategy(handlers))
                        {
                            return true;
                        }

                        if (HasAnyDestinationNewOrReplace(handlers))
                        {
                            return destination > 1;
                        }

                        return false;
                    }

                    static Boolean IsInvalid(KeyValuePair<(MonoCecilType Type, Object? Key), List<ServiceDescriptorHandler>> pair)
                    {
                        if (pair.Value is not { Count: > 1 } handlers)
                        {
                            return false;
                        }

                        Int32 destination = CountDestination(handlers);

                        if (destination == 0)
                        {
                            return true;
                        }

                        if (IsSingleOrInvalidStrategy(handlers))
                        {
                            return true;
                        }

                        if (HasAnyDestinationNewOrReplace(handlers))
                        {
                            return destination > 1;
                        }

                        return false;
                    }

                    static Exception Exception(KeyValuePair<(MonoCecilType Type, Object? Key), List<ServiceDescriptorHandler>> pair)
                    {
                        TypeComparer comparer = TypeComparer.NameOrdinalIgnoreCase;
                        IEnumerable<ServiceDescriptor> source = pair.Value.Unwrap();
                        String? message = source.OrderBy(static descriptor => descriptor.ImplementationType, comparer).ThenByDescending(static descriptor => descriptor.Lifetime).Select(static descriptor => $"{descriptor.ImplementationType?.Name}:{Metadata.GetOrder(descriptor)}[{Metadata.GetStrategy(descriptor)}] {descriptor.Lifetime}").GetString();
                        return new ScanAmbiguousException(message);
                    }

                    [SuppressMessage("ReSharper", "VariableHidesOuterVariable")]
                    void Handler(KeyValuePair<(MonoCecilType Type, Object? Key), List<ServiceDescriptorHandler>> pair)
                    {
                        List<ServiceDescriptorHandler> container = pair.Value;
                        Int32 destination = CountDestination(container);
                        Boolean conflict = destination == 0 || IsSingleOrInvalidStrategy(container);
                        ServiceDescriptorHandler[] candidates = conflict ? container.ToArray() : container.Where(static handler => handler.Source is ServiceDescriptorHandler.Affiliation.Destination).ToArray();

                        if (candidates.Length <= 0 || handler.Invoke(candidates) is not { } select)
                        {
                            return;
                        }

                        if (conflict)
                        {
                            for (Int32 i = container.Count - 1; i >= 0; i--)
                            {
                                ServiceDescriptorHandler handler = container[i];
                                if (ServiceDescriptorEqualityComparer.Default.Equals(handler.Service, select))
                                {
                                    continue;
                                }

                                switch (handler.Source)
                                {
                                    case ServiceDescriptorHandler.Affiliation.Source:
                                        services.Remove(handler.Service);
                                        break;
                                    case ServiceDescriptorHandler.Affiliation.Destination:
                                        set.Remove(handler.Service);
                                        break;
                                    default:
                                        continue;
                                }

                                container.RemoveAt(i);
                            }

                            container.Clear();
                            container.Add(new ServiceDescriptorHandler(select, ServiceDescriptorHandler.Affiliation.Handle));
                            return;
                        }

                        for (Int32 i = container.Count - 1; i >= 0; i--)
                        {
                            ServiceDescriptorHandler handler = container[i];
                            if (handler.Source is not ServiceDescriptorHandler.Affiliation.Destination || ServiceDescriptorEqualityComparer.Default.Equals(handler.Service, select))
                            {
                                continue;
                            }

                            set.Remove(handler.Service);
                            container.RemoveAt(i);
                        }
                    }

                    Parallel.ForEach(counter.Where(RequireUseHandler), Handler);

                    const String message = "Ambiguous registration for service type:";
                    Exception[] exceptions = counter.Where(IsInvalid).OrderBy(static pair => pair.Key.Type, TypeComparer.NameOrdinalIgnoreCase).Select(Exception).ToArray();

                    return exceptions.Length switch
                    {
                        0 => Sort(ApplyReplace(services, set).Add(set)),
                        1 => throw new ScanAmbiguousException(message + ' ' + exceptions[0].Message),
                        _ => throw new ScanAmbiguousException(message, new AggregateException(exceptions))
                    };
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IServiceCollection Verify(IServiceCollection services, ConcurrentHashSet<ServiceDescriptor> set)
        {
            return Verify(services, set, AmbiguousHandler);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IServiceCollection Verify(IServiceCollection services, ConcurrentHashSet<ServiceDescriptor> set, Options options)
        {
            return Verify(services, set, options.Handler);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IServiceCollection Verify(IServiceCollection services, ConcurrentHashSet<ServiceDescriptor> set, ServiceAmbiguousHandler handler)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (set is null)
            {
                throw new ArgumentNullException(nameof(set));
            }

            return handler.Type switch
            {
                ServiceAmbiguousHandlerType.Ignore => VerifyIgnore(services, set),
                ServiceAmbiguousHandlerType.Throw => VerifyThrow(services, set, handler),
                ServiceAmbiguousHandlerType.New => VerifyNew(services, set, handler),
                ServiceAmbiguousHandlerType.Custom => VerifyThrow(services, set, handler),
                _ => throw new EnumUndefinedOrNotSupportedException<ServiceAmbiguousHandlerType>(handler.Type, nameof(ServiceAmbiguousHandlerType), null)
            };
        }

        private static Dictionary<(Type ServiceType, Object? ServiceKey), List<Int32>> Populate(IServiceCollection services)
        {
            Dictionary<(Type ServiceType, Object? ServiceKey), List<Int32>> storage = new Dictionary<(Type ServiceType, Object? ServiceKey), List<Int32>>();

            for (Int32 i = 0; i < services.Count; i++)
            {
                ServiceDescriptor descriptor = services[i];
                (Type ServiceType, Object? ServiceKey) key = (descriptor.ServiceType, descriptor.ServiceKey);

                if (!storage.TryGetValue(key, out List<Int32>? indexes))
                {
                    storage[key] = indexes = new List<Int32>();
                }

                indexes.Add(i);
            }

            return storage;
        }

        [return: NotNullIfNotNull("services")]
        [SuppressMessage("ReSharper", "LoopCanBeConvertedToQuery")]
        private static IServiceCollection? Sort(IServiceCollection? services)
        {
            if (services is null)
            {
                return null;
            }

            if (services.Count <= 1)
            {
                return services;
            }

            Dictionary<(Type ServiceType, Object? ServiceKey), List<Int32>> storage = Populate(services);

            foreach ((_, List<Int32> indexes) in storage)
            {
                if (indexes.Count <= 1)
                {
                    continue;
                }

                List<(Int32 OriginalIndex, ServiceDescriptor Descriptor, Int32 Position)> items = new List<(Int32, ServiceDescriptor, Int32)>(storage.Count);
                for (Int32 i = 0; i < indexes.Count; i++)
                {
                    items.Add((indexes[i], services[indexes[i]], i));
                }

                items.Sort(static (first, second) =>
                {
                    Int32 compare = Metadata.GetOrder(first.Descriptor).CompareTo(Metadata.GetOrder(second.Descriptor));
                    return compare != 0 ? compare : first.Position.CompareTo(second.Position);
                });

                for (Int32 i = 0; i < indexes.Count; i++)
                {
                    services[indexes[i]] = items[i].Descriptor;
                }
            }

            return VerifySort(services);
        }

        private record SortData
        {
            [return: NotNullIfNotNull("value")]
            public static implicit operator List<Int32>?(SortData? value)
            {
                return value?.Indexes;
            }

            private HashSet<Type>? _cover;
            public HashSet<Type> Cover
            {
                get
                {
                    return _cover ??= new HashSet<Type>();
                }
            }

            private List<Int32>? _indexes;
            public List<Int32> Indexes
            {
                get
                {
                    return _indexes ??= new List<Int32>();
                }
            }

            public ServiceDependsOnAttribute? Dependency { get; }

            public Boolean Depends
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Dependency is { HasDependency: true };
                }
            }

            public ImmutableArray<ServiceDependencyInfoAttribute> Attributes { get; }

            public SortData(Type implementation)
            {
                Dependency = AttributeUtilities.GetCustomAttribute<ServiceDependsOnAttribute>(implementation);
                Attributes = AttributeUtilities.GetCustomAttributes<ServiceDependencyInfoAttribute>(implementation).ToImmutableArray();

                foreach (ServiceDependencyInfoAttribute attribute in Attributes)
                {
                    if (attribute.Key is null && attribute.Interface is { IsInterface: true })
                    {
                        Cover.Add(attribute.Interface);
                    }
                }
            }

            public List<Int32>.Enumerator GetEnumerator()
            {
                return Indexes.GetEnumerator();
            }
        }

        [SuppressMessage("ReSharper", "CognitiveComplexity")]
        private static IServiceCollection VerifySort(IServiceCollection services)
        {
            Dictionary<Type, SortData> storage = new Dictionary<Type, SortData>();

            ServiceDescriptor[] descriptors = services.ToArray();
            for (Int32 i = 0; i < descriptors.Length; i++)
            {
                if (descriptors[i].ImplementationType is not { } implementation)
                {
                    continue;
                }

                if (!storage.TryGetValue(implementation, out SortData? data))
                {
                    storage[implementation] = data = new SortData(implementation);
                }

                data.Indexes.Add(i);
            }

            List<Exception> errors = new List<Exception>(64);

            HashSet<Int32> @fixed = new HashSet<Int32>(1024);
            foreach (IGrouping<(Type ServiceType, Object? ServiceKey), (Type ServiceType, Object? ServiceKey, Int32 Index)> group in descriptors.Select(static (descriptor, index) => (descriptor.ServiceType, descriptor.ServiceKey, Index: index)).GroupBy(static descriptor => (descriptor.ServiceType, descriptor.ServiceKey)))
            {
                Int32? single = null;
                Int32? maximum = null;
                foreach ((_, _, Int32 index) in group)
                {
                    switch (Metadata.GetSingle(descriptors[index]))
                    {
                        case null when descriptors[index].ImplementationType is { } implementation:
                        {
                            if (storage.TryGetValue(implementation, out SortData? data) && data.Depends && descriptors[index].ServiceKey is null && descriptors[index].ServiceType != implementation && !data.Cover.Contains(descriptors[index].ServiceType))
                            {
                                continue;
                            }

                            maximum = Math.Max(maximum ?? index, index);
                            continue;
                        }
                        case false:
                        {
                            continue;
                        }
                        case true when single is not null:
                        {
                            errors.Add(new ServiceIsSingleException($"Can't have multiple services with {nameof(ServiceDependencyInfoAttribute.Single)} argument in service{(group.Key.ServiceKey is { } key ? $" group: (Type: '{group.Key.ServiceType}', Key: '{key}')" : $": '{group.Key.ServiceType}'")}."));
                            goto case true;
                        }
                        case true:
                        {
                            single = index;
                            continue;
                        }
                    }
                }

                if (single.HasValue)
                {
                    @fixed.Add(single.Value);
                }
                else if (maximum.HasValue)
                {
                    @fixed.Add(maximum.Value);
                }
            }

            switch (errors.Count)
            {
                case 0:
                    break;
                case 1:
                    throw errors[0];
                default:
                    throw new AggregateException("Services sorting validation is failed:", errors);
            }

            List<Int32>[] graph = new List<Int32>[descriptors.Length];
            for (Int32 i = 0; i < graph.Length; i++)
            {
                graph[i] = new List<Int32>(8);
            }

            static Exception? EdgeBefore(ReadOnlySpan<ServiceDescriptor> descriptors, ReadOnlySpan<List<Int32>> graph, HashSet<Int32> @fixed, Int32 from, Int32 to)
            {
                if (@fixed.Contains(from))
                {
                    return new ServiceIsSingleException($"Service '{descriptors[from].ImplementationType?.Name ?? "unknown"}' is single-get service and can't be placed before '{descriptors[to].ImplementationType?.Name ?? "unknown"}'.");
                }

                graph[from].Add(to);
                return null;
            }

            static Exception? EdgeAfter(ReadOnlySpan<ServiceDescriptor> descriptors, ReadOnlySpan<List<Int32>> graph, HashSet<Int32> @fixed, Int32 from, Int32 to)
            {
                if (@fixed.Contains(from))
                {
                    return new ServiceIsSingleException($"Service '{descriptors[from].ImplementationType?.Name ?? "unknown"}' is single-get service and can't be placed after '{descriptors[to].ImplementationType?.Name ?? "unknown"}'.");
                }

                graph[to].Add(from);
                return null;
            }

            Dictionary<(Type ServiceType, Object? ServiceKey, Type ImplementationType), Int32> map = new Dictionary<(Type ServiceType, Object? ServiceKey, Type ImplementationType), Int32>();
            for (Int32 i = 0; i < descriptors.Length; i++)
            {
                ServiceDescriptor descriptor = descriptors[i];
                if (descriptor.ImplementationType is not null)
                {
                    map[(descriptor.ServiceType, descriptor.ServiceKey, descriptor.ImplementationType)] = i;
                }
            }

            foreach ((Type implementation, SortData data) in storage)
            {
                foreach (ServiceDependencyInfoAttribute attribute in data.Attributes)
                {
                    Type service = attribute.Interface ?? implementation;
                    if (!map.TryGetValue((service, attribute.Key, implementation), out Int32 from))
                    {
                        if (attribute.Strict)
                        {
                            errors.Add(new ServiceReferenceNotFoundException($"Service '{implementation.Name}' has attribute for {(attribute.Interface is not null ? $"interface {attribute.Interface.Name}" : "self")}{(attribute.Key is not null ? $" with key '{attribute.Key}'" : String.Empty)}, but no matching descriptor found."));
                        }

                        continue;
                    }

                    if (attribute.Before is { Length: > 0 } before)
                    {
                        foreach (Type dependency in before)
                        {
                            if (dependency == implementation)
                            {
                                errors.Add(new ServiceSelfReferenceException($"Service '{implementation.Name}' has '{nameof(ServiceDependencyInfoAttribute.Before)}' self-reference."));
                                continue;
                            }

                            if (storage.TryGetValue(dependency, out SortData? targets))
                            {
                                foreach (Int32 to in targets)
                                {
                                    if (descriptors[to].ServiceType == service && Equals(descriptors[to].ServiceKey, attribute.Key))
                                    {
                                        errors.AddIfNotNull(EdgeBefore(descriptors, graph, @fixed, from, to));
                                    }
                                }
                            }
                            else if (attribute.Strict)
                            {
                                errors.Add(new ServiceReferenceNotFoundException($"Service '{dependency.Name}' depends from '{implementation.Name}', but dependency not exists in service collection."));
                            }
                        }
                    }

                    if (attribute.After is { Length: > 0 } after)
                    {
                        foreach (Type dependency in after)
                        {
                            if (dependency == implementation)
                            {
                                errors.Add(new ServiceSelfReferenceException($"Service '{implementation.Name}' has '{nameof(ServiceDependencyInfoAttribute.After)}' self-reference."));
                                continue;
                            }

                            if (storage.TryGetValue(dependency, out SortData? targets))
                            {
                                foreach (Int32 to in targets)
                                {
                                    if (descriptors[to].ServiceType == service && Equals(descriptors[to].ServiceKey, attribute.Key))
                                    {
                                        errors.AddIfNotNull(EdgeAfter(descriptors, graph, @fixed, from, to));
                                    }
                                }
                            }
                            else if (attribute.Strict)
                            {
                                errors.Add(new ServiceReferenceNotFoundException($"Service '{implementation.Name}' depends from '{dependency.Name}', but dependency not exists in service collection."));
                            }
                        }
                    }
                }

                if (data.Dependency is { HasDependency: true } depends)
                {
                    HashSet<Type> cover = new HashSet<Type>();
                    foreach (ServiceDependencyInfoAttribute attribute in data.Attributes)
                    {
                        if (attribute.Key is null && attribute.Interface is { IsInterface: true })
                        {
                            cover.Add(attribute.Interface);
                        }
                    }

                    if (storage.TryGetValue(implementation, out SortData? info))
                    {
                        foreach (Int32 from in info)
                        {
                            if (descriptors[from] is not { ServiceKey: null } service || service.ServiceType == implementation || cover.Contains(service.ServiceType))
                            {
                                continue;
                            }

                            if (depends.Before is { Length: > 0 } before)
                            {
                                foreach (Type dependency in before)
                                {
                                    if (dependency == implementation)
                                    {
                                        errors.Add(new ServiceSelfReferenceException($"Service '{implementation.Name}' has '{nameof(ServiceDependsOnAttribute.Before)}' self-reference."));
                                        continue;
                                    }

                                    if (!storage.TryGetValue(dependency, out SortData? targets))
                                    {
                                        continue;
                                    }

                                    foreach (Int32 to in targets)
                                    {
                                        if (descriptors[to] is { ServiceKey: null } descriptor && descriptor.ServiceType != dependency)
                                        {
                                            errors.AddIfNotNull(EdgeBefore(descriptors, graph, @fixed, from, to));
                                        }
                                    }
                                }
                            }

                            if (depends.After is { Length: > 0 } after)
                            {
                                foreach (Type dependency in after)
                                {
                                    if (dependency == implementation)
                                    {
                                        errors.Add(new ServiceSelfReferenceException($"Service '{implementation.Name}' has '{nameof(ServiceDependsOnAttribute.After)}' self-reference."));
                                        continue;
                                    }

                                    if (!storage.TryGetValue(dependency, out SortData? targets))
                                    {
                                        continue;
                                    }

                                    foreach (Int32 to in targets)
                                    {
                                        if (descriptors[to] is { ServiceKey: null } descriptor && descriptor.ServiceType != dependency)
                                        {
                                            errors.AddIfNotNull(EdgeAfter(descriptors, graph, @fixed, from, to));
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            switch (errors.Count)
            {
                case 0:
                    break;
                case 1:
                    throw errors[0];
                default:
                    throw new AggregateException("Services sorting validation is failed:", errors);
            }

            Dictionary<(Type ServiceType, Object? ServiceKey), IEnumerable<(Type ServiceType, Object? ServiceKey, Int32 Index)>> groups = new Dictionary<(Type ServiceType, Object? ServiceKey), IEnumerable<(Type ServiceType, Object? ServiceKey, Int32 Index)>>(descriptors.Length);
            foreach (IGrouping<(Type ServiceType, Object? ServiceKey), (Type ServiceType, Object? ServiceKey, Int32 index)> group in descriptors.Select(static (descriptor, index) => (descriptor.ServiceType, descriptor.ServiceKey, index)).GroupBy(static descriptor => (descriptor.ServiceType, descriptor.ServiceKey)))
            {
                groups.Add(group.Key, group);
            }

            List<Int32> indexes = new List<Int32>(Math.Max(@fixed.Count, groups.Count));

            foreach (Int32 index in @fixed)
            {
                ServiceDescriptor descriptor = descriptors[index];
                indexes.Clear();

                foreach ((_, _, Int32 service) in groups[(descriptor.ServiceType, descriptor.ServiceKey)])
                {
                    if (service != index)
                    {
                        indexes.Add(service);
                    }
                }

                indexes.Sort();

                foreach (Int32 service in indexes)
                {
                    errors.AddIfNotNull(EdgeBefore(descriptors, graph, @fixed, service, index));
                }
            }

            switch (errors.Count)
            {
                case 0:
                    break;
                case 1:
                    throw errors[0];
                default:
                    throw new AggregateException("Services sorting is failed:", errors);
            }

            Span<Int32> degree = stackalloc Int32[graph.Length];
            foreach (List<Int32> edges in graph)
            {
                foreach (Int32 to in edges)
                {
                    degree[to]++;
                }
            }

            Queue<Int32> queue = new Queue<Int32>(graph.Length);

            for (Int32 i = 0; i < graph.Length; i++)
            {
                if (degree[i] == 0)
                {
                    queue.Add(i);
                }
            }

            indexes.Clear();

            while (queue.Count > 0)
            {
                Int32 index = queue.Dequeue();
                indexes.Add(index);

                foreach (Int32 to in graph[index])
                {
                    if (--degree[to] == 0)
                    {
                        queue.Enqueue(to);
                    }
                }
            }

            if (indexes.Count != graph.Length)
            {
                List<String> remaining = Enumerable.Range(0, graph.Length).Except(indexes).Select(i => descriptors[i].ImplementationType?.Name ?? "unknown").ToList();
                throw new ServiceReferenceException($"A cyclic relationship between the following types has been discovered: '{String.Join(" -> ", remaining)}'.");
            }

            services.Clear();
            foreach (Int32 index in indexes)
            {
                services.Add(descriptors[index]);
            }

            return services;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IEnumerable<ServiceDescriptor> Unwrap(this IEnumerable<ServiceDescriptorHandler> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Select(static descriptor => descriptor.Service);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection Scan(this IServiceCollection services)
        {
            return Scan<Any>(services, AmbiguousHandler);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection Scan(this IServiceCollection services, ServiceAmbiguousHandler handler)
        {
            return Scan<Any>(services, handler);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection Scan<T>(this IServiceCollection services) where T : class
        {
            return Scan<T>(services, AmbiguousHandler);
        }

        public static IServiceCollection Scan<T>(this IServiceCollection services, ServiceAmbiguousHandler handler) where T : class
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            Inherit.Result inherit = ReflectionUtilities.Inherit;
            if (!inherit.TryGetValue(typeof(IDependencyService), out ReflectionInheritResult? result))
            {
                throw new InvalidOperationException();
            }

            TypeSet attributes = inherit.Type[typeof(ServiceDependencyInfoAttribute)].Types.Add(typeof(ServiceDependencyInfoAttribute));
            TypeSet set = result.Inherit.Types.Union(attributes.SelectMany<MonoCecilType, MonoCecilType>(attribute => inherit.Attributes[attribute].Types));
            IImmutableSet<MonoCecilType> except = inherit.Type[typeof(IUnscanDependencyService)].Add(typeof(IUnscanDependencyService));

            if (typeof(T) == typeof(Any) || typeof(T) == typeof(Any.Value))
            {
                except = except.Union(inherit.Type[typeof(ISpecialServiceDependency)].Add(typeof(ISpecialServiceDependency)));
            }
            else if (inherit[typeof(Attribute)].Contains(typeof(T)))
            {
                set = set.Intersect(inherit.Attributes[typeof(T)]);
            }
            else if (typeof(T).IsInterface)
            {
                set = set.Intersect(inherit[typeof(T)].Add(typeof(T)));
            }
            else if (typeof(T).IsAbstract)
            {
                throw new TypeNotSupportedException<T>($"Type '{typeof(T).Name}' cannot be abstract.");
            }
            else
            {
                set = set.Intersect(inherit[typeof(T)].Add(typeof(T)));
            }

            Options options = new Options
            {
                Source = set.Except(except),
                Interfaces = result.Interfaces.Union(result.Generic.Interfaces).Except(except),
                Except = except,
                Inherit = inherit,
                ServiceDependency = result,
                Handler = handler
            };

            return Scan(services, options);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection Scan(this IServiceCollection services, IEnumerable<MonoCecilType?> source)
        {
            return Scan(services, source, AmbiguousHandler);
        }

        public static IServiceCollection Scan(this IServiceCollection services, IEnumerable<MonoCecilType?> source, ServiceAmbiguousHandler handler)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            Inherit.Result inherit = ReflectionUtilities.Inherit;
            if (!inherit.TryGetValue(typeof(IDependencyService), out ReflectionInheritResult? result))
            {
                throw new InvalidOperationException();
            }

            TypeSet attributes = inherit.Type[typeof(ServiceDependencyInfoAttribute)].Types.Add(typeof(ServiceDependencyInfoAttribute));
            TypeSet set = result.Inherit.Types.Union(attributes.SelectMany<MonoCecilType, MonoCecilType>(attribute => inherit.Attributes[attribute].Types));
            IImmutableSet<MonoCecilType> except = inherit.Type[typeof(IUnscanDependencyService)].Add(typeof(IUnscanDependencyService));

            Options options = new Options
            {
                Source = set.Intersect(source!).Except(except),
                Interfaces = result.Interfaces.Union(result.Generic.Interfaces).Except(except),
                Except = except,
                Inherit = inherit,
                ServiceDependency = result,
                Handler = handler
            };

            return Scan(services, options);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection Scan(this IServiceCollection services, Assembly assembly)
        {
            return Scan(services, assembly, AmbiguousHandler);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection Scan(this IServiceCollection services, Assembly assembly, ServiceAmbiguousHandler handler)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            return Scan(services, assembly.GetCecilTypes(), handler);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection Scan(this IServiceCollection services, IEnumerable<Assembly?> assemblies)
        {
            return Scan(services, assemblies, AmbiguousHandler);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection Scan(this IServiceCollection services, IEnumerable<Assembly?> assemblies, ServiceAmbiguousHandler handler)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (assemblies is null)
            {
                throw new ArgumentNullException(nameof(assemblies));
            }

            return Scan(services, assemblies.GetCecilTypes(), handler);
        }

        private sealed record Metadata
        {
            public static implicit operator (ServiceStrategy Strategy, Boolean? Single, Int32 Order)(Metadata? metadata)
            {
                return metadata is not null ? (metadata.Strategy, metadata.Single, metadata.Order) : default;
            }

            private static ConditionalWeakTable<ServiceDescriptor, Metadata> Storage { get; } = new ConditionalWeakTable<ServiceDescriptor, Metadata>();

            public ServiceStrategy Strategy { get; set; }
            public Boolean? Single { get; set; }
            public Int32 Order { get; set; }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static (ServiceStrategy Strategy, Boolean? Single, Int32 Order) Get(ServiceDescriptor? descriptor)
            {
                return descriptor is not null && Storage.TryGetValue(descriptor, out Metadata? metadata) ? metadata : (ServiceStrategy.New, null, 0);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static ServiceStrategy GetStrategy(ServiceDescriptor? descriptor)
            {
                return descriptor is not null && Storage.TryGetValue(descriptor, out Metadata? metadata) ? metadata.Strategy : ServiceStrategy.New;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean? GetSingle(ServiceDescriptor? descriptor)
            {
                return descriptor is not null && Storage.TryGetValue(descriptor, out Metadata? metadata) ? metadata.Single : null;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Int32 GetOrder(ServiceDescriptor? descriptor)
            {
                return descriptor is not null && Storage.TryGetValue(descriptor, out Metadata? metadata) ? metadata.Order : 0;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static ServiceDescriptor Set(ServiceDescriptor descriptor, ServiceStrategy strategy, Boolean? single, Int32 order)
            {
                if (descriptor is null)
                {
                    throw new ArgumentNullException(nameof(descriptor));
                }

                Metadata metadata = Storage.GetOrCreateValue(descriptor);
                metadata.Strategy = strategy;
                metadata.Single = single;
                metadata.Order = order;
                return descriptor;
            }
        }
    }
}