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
using NetExtender.DependencyInjection.Attributes;
using NetExtender.DependencyInjection.Comparers;
using NetExtender.DependencyInjection.Interfaces;
using NetExtender.Types.Comparers;
using NetExtender.Types.Entities;
using NetExtender.Types.Exceptions;
using NetExtender.Utilities.Core;

namespace NetExtender.Utilities.Types
{
    public delegate ServiceDescriptor? ServiceAmbiguousDelegate(ReadOnlySpan<ServiceDescriptorHandler> services);

    public enum ServiceAmbiguousHandlerType
    {
        Ignore,
        Throw,
        New,
        Custom
    }
    
    public readonly struct ServiceAmbiguousHandler : IStruct<ServiceAmbiguousHandler>
    {
        public static implicit operator ServiceAmbiguousHandler(ServiceAmbiguousHandlerType value)
        {
            // ReSharper disable once SwitchExpressionHandlesSomeKnownEnumValuesWithExceptionInDefault
            return value switch
            {
                ServiceAmbiguousHandlerType.Ignore => Ignore,
                ServiceAmbiguousHandlerType.Throw => Throw,
                ServiceAmbiguousHandlerType.New => New,
                _ => throw new EnumUndefinedOrNotSupportedException<ServiceAmbiguousHandlerType>(value, nameof(value), null)
            };
        }
        
        public static implicit operator ServiceAmbiguousHandler(ServiceAmbiguousDelegate? value)
        {
            return new ServiceAmbiguousHandler(value);
        }
        
        public static ServiceAmbiguousHandler Ignore { get; } = new ServiceAmbiguousHandler(null);
        public static ServiceAmbiguousHandler Throw { get; } = new ServiceAmbiguousHandler(static _ => null) { Type = ServiceAmbiguousHandlerType.Throw };
        public static ServiceAmbiguousHandler New { get; } = new ServiceAmbiguousHandler(static _ => null) { Type = ServiceAmbiguousHandlerType.New };
        
        private ServiceAmbiguousDelegate? Handler { get; }
        public ServiceAmbiguousHandlerType Type { get; private init; }
        
        public Boolean IsEmpty
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Handler is null;
            }
        }
        
        private ServiceAmbiguousHandler(ServiceAmbiguousDelegate? handler)
        {
            Handler = handler;
            Type = Handler is null ? ServiceAmbiguousHandlerType.Ignore : ServiceAmbiguousHandlerType.Custom;
        }
        
        public ServiceDescriptor? Invoke(ReadOnlySpan<ServiceDescriptorHandler> services)
        {
            if (Handler is null)
            {
                throw new InvalidOperationException(nameof(IsEmpty));
            }
            
            return Handler.Invoke(services);
        }
    }
    
    public readonly struct ServiceDescriptorHandler : IEquatable<ServiceDescriptorHandler.Affiliation>, IEquatable<ServiceDescriptor>, IEquatable<ServiceDescriptorHandler>
    {
        public enum Affiliation : Byte
        {
            Unknown,
            Source,
            Destination,
            Handle,
            Any
        }
        
        public static implicit operator Affiliation(ServiceDescriptorHandler value)
        {
            return value.Source;
        }
        
        public static implicit operator ServiceDescriptor(ServiceDescriptorHandler value)
        {
            return value.Service;
        }
        
        public static implicit operator ServiceDescriptorHandler(ServiceDescriptor? value)
        {
            return value is not null ? new ServiceDescriptorHandler(value) : default;
        }
        
        public static Boolean operator ==(ServiceDescriptorHandler first, ServiceDescriptorHandler second)
        {
            return first.Equals(second);
        }
        
        public static Boolean operator !=(ServiceDescriptorHandler first, ServiceDescriptorHandler second)
        {
            return !(first == second);
        }
        
        public Affiliation Source { get; }
        public ServiceDescriptor Service { get; }
        
        public ServiceDescriptorHandler(ServiceDescriptor service)
            : this(service, Affiliation.Any)
        {
        }
        
        public ServiceDescriptorHandler(ServiceDescriptor service, Affiliation source)
        {
            Source = source;
            Service = service ?? throw new ArgumentNullException(nameof(service));
        }
        
        public override Int32 GetHashCode()
        {
            return Service is not null ? ServiceDescriptorEqualityComparer.Default.GetHashCode(Service) : 0;
        }
        
        public override Boolean Equals(Object? other)
        {
            return other switch
            {
                null => Equals((ServiceDescriptor?) null),
                ServiceDescriptorHandler value => Equals(value),
                ServiceDescriptor value => Equals(value),
                Affiliation value => Equals(value),
                _ => false
            };
        }
        
        public Boolean Equals(Affiliation other)
        {
            return Source is Affiliation.Any || other is Affiliation.Any || Source == other;
        }
        
        public Boolean Equals(ServiceDescriptor? other)
        {
            return ServiceDescriptorEqualityComparer.Default.Equals(Service, other);
        }
        
        public Boolean Equals(ServiceDescriptorHandler other)
        {
            return Equals(other.Source) && Equals(other.Service);
        }
        
        public override String ToString()
        {
            return $"{{ {nameof(Affiliation)}: {Source}, {nameof(Service)}: {Service.GetString()} }}";
        }
    }
    
    public partial class ServiceCollectionUtilities
    {
        public static ServiceAmbiguousHandler Handler { get; set; } = ServiceAmbiguousHandler.Throw;
        
        [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
        private readonly struct Options
        {
            public ImmutableHashSet<Type> Source { get; init; }
            public ImmutableHashSet<Type> Interfaces { get; init; }
            public ImmutableHashSet<Type> Except { get; init; }
            public Inherit.Result Inherit { get; init; }
            public ReflectionInheritResult ServiceDependency { get; init; }
            public ServiceAmbiguousHandler Handler { get; init; }
        }
        
        private readonly struct Split : IStruct<Split>
        {
            public Type Type { get; }
            public ImmutableHashSet<Type> Transient { get; }
            public ImmutableHashSet<Type> Scoped { get; }
            public ImmutableHashSet<Type> Singleton { get; }
            public ServiceLifetime? Lifetime { get; }
            
            public Boolean IsEmpty
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Type is null;
                }
            }
            
            public Split(Type type, Options options, ImmutableHashSet<Type> interfaces)
            {
                Type = type ?? throw new ArgumentNullException(nameof(type));
                
                if (interfaces is null)
                {
                    throw new ArgumentNullException(nameof(interfaces));
                }
                
                Transient = Handle(typeof(ITransient<>), options, interfaces) ?? ImmutableHashSet<Type>.Empty;
                Scoped = Handle(typeof(IScoped<>), options, interfaces) ?? ImmutableHashSet<Type>.Empty;
                Singleton = Handle(typeof(ISingleton<>), options, interfaces) ?? ImmutableHashSet<Type>.Empty;
                
                Lifetime = Transient.Count switch
                {
                    > 0 when Scoped.Count <= 0 && Singleton.Count <= 0 => ServiceLifetime.Transient,
                    <= 0 when Scoped.Count > 0 && Singleton.Count <= 0 => ServiceLifetime.Scoped,
                    <= 0 when Scoped.Count <= 0 && Singleton.Count > 0 => ServiceLifetime.Singleton,
                    <= 0 when Scoped.Count <= 0 && Singleton.Count <= 0 => VerifyLifetime(interfaces),
                    _ => null
                };
            }
            
            [MethodImpl(MethodImplOptions.AggressiveOptimization)]
            private static ImmutableHashSet<Type>? Handle(Type @interface, Options options, ImmutableHashSet<Type> interfaces)
            {
                if (!options.Inherit.TryGetValue(@interface, out ReflectionInheritResult? result))
                {
                    return null;
                }

                if (result.Interfaces.Count > 0 && interfaces.Intersect(result.Interfaces) is { Count: > 0 } services)
                {
                    return services;
                }

                interfaces = interfaces.Except(interfaces.SelectMany(static @interface => @interface.GetInterfaces()));
                return interfaces.Count > 0 ? interfaces : null;
            }
            
            [MethodImpl(MethodImplOptions.AggressiveOptimization)]
            private static ServiceLifetime? VerifyLifetime(ImmutableHashSet<Type> interfaces)
            {
                if (interfaces is null)
                {
                    throw new ArgumentNullException(nameof(interfaces));
                }
                
                if (interfaces.Intersect(IDependencyService.Services.Keys) is not { Count: 1 } intersect)
                {
                    return null;
                }
                
                return IDependencyService.Services.TryGetValue(intersect.Single(), out ServiceLifetime lifetime) ? lifetime : null;
            }
            
            public void Populate(ICollection<Type> collection)
            {
                if (collection is null)
                {
                    throw new ArgumentNullException(nameof(collection));
                }
                
                Populate(collection, ServiceLifetime.Transient);
                Populate(collection, ServiceLifetime.Scoped);
                Populate(collection, ServiceLifetime.Singleton);
            }
            
            public void Populate(ICollection<Type> collection, ServiceLifetime lifetime)
            {
                if (collection is null)
                {
                    throw new ArgumentNullException(nameof(collection));
                }
                
                ImmutableHashSet<Type> services = lifetime switch
                {
                    ServiceLifetime.Transient => Transient,
                    ServiceLifetime.Scoped => Scoped,
                    ServiceLifetime.Singleton => Singleton,
                    _ => throw new EnumUndefinedOrNotSupportedException<ServiceLifetime>(lifetime, nameof(lifetime), null)
                };
                
                foreach (Type service in services)
                {
                    collection.Add(service);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IReadOnlyCollection<ServiceDescriptor> Create(Type @interface, Type type, ServiceLifetime lifetime, Options options)
        {
            return Create(@interface, type, null, lifetime, options);
        }

        // ReSharper disable once CognitiveComplexity
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static IReadOnlyCollection<ServiceDescriptor> Create(Type @interface, Type type, Object? key, ServiceLifetime lifetime, Options options)
        {
            if (@interface is null)
            {
                throw new ArgumentNullException(nameof(@interface));
            }
            
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            if (type.IsAbstract)
            {
                return ImmutableArray<ServiceDescriptor>.Empty;
            }
            
            if (!@interface.IsInterface)
            {
                return ImmutableList<ServiceDescriptor>.Empty.Add(new ServiceDescriptor(@interface, key, type, lifetime));
            }
            
            if (options.Inherit.TryGetValue(@interface, out ReflectionInheritResult? result) && !result.Inherit.Types.Contains(type))
            {
                throw new TypeNotSupportedException(type, $"Type '{type}' must implement interface '{@interface}'.");
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            static Boolean IsServiceDependency(Type @interface)
            {
                return @interface.IsGenericType && @interface.GetGenericTypeDefinition() == typeof(IDependencyService<>);
            }
            
            Dictionary<(Type, Object?), ServiceDescriptor> descriptors = new Dictionary<(Type, Object?), ServiceDescriptor>();
            
            foreach (Type argument in @interface.GetInterfaces().Where(IsServiceDependency).Select(static dependency => dependency.GenericTypeArguments[0]))
            {
                if (!argument.IsInterface || !options.Inherit.TryGetValue(argument, out result) || !result.Inherit.Types.Contains(type))
                {
                    throw new TypeNotSupportedException(type, $"Type '{type}' must implement interface '{argument}'.");
                }
                
                descriptors.TryAdd((argument, key), new ServiceDescriptor(argument, key, type, lifetime));
            }
            
            descriptors.TryAdd((@interface, key), new ServiceDescriptor(@interface, key, type, lifetime));
            return descriptors.Values.ToImmutableList();
        }

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
                static void CallStaticConstructor(Type type)
                {
                    type.CallStaticConstructor();
                }

                Parallel.ForEach(dependency.Inherit.Interfaces, CallStaticConstructor);
            }

            // ReSharper disable once VariableHidesOuterVariable
            [MethodImpl(MethodImplOptions.AggressiveOptimization)]
            void Handler(Type? type)
            {
                if (type is null || type.IsAbstract)
                {
                    return;
                }
                
                ImmutableHashSet<Type> interfaces = type.GetInterfaces().Where(@interface => options.Interfaces.Contains(@interface.TryGetGenericTypeDefinition())).ToImmutableHashSet();
                
                if (interfaces.Count > 0)
                {
                    Split split = new Split(type, options, interfaces);
                    
                    foreach (Type @interface in split.Transient)
                    {
                        descriptors.AddRange(Create(@interface, type, ServiceLifetime.Transient, options));
                    }
                    
                    foreach (Type @interface in split.Scoped)
                    {
                        descriptors.AddRange(Create(@interface, type, ServiceLifetime.Scoped, options));
                    }
                    
                    foreach (Type @interface in split.Singleton)
                    {
                        descriptors.AddRange(Create(@interface, type, ServiceLifetime.Singleton, options));
                    }
                    
                    if (split.Lifetime is { } lifetime)
                    {
                        descriptors.Add(new ServiceDescriptor(type, type, lifetime));
                    }
                }
                
                foreach (ServiceDependencyAttribute attribute in ReflectionUtilities.GetCustomAttributes<ServiceDependencyAttribute>(type))
                {
                    if (attribute.Interface is { } @interface)
                    {
                        descriptors.AddRange(Create(@interface, type, attribute.Key, attribute.Lifetime, options));
                    }

                    descriptors.Add(new ServiceDescriptor(type, type, attribute.Lifetime));
                }
            }
            
            Parallel.ForEach(options.Source, Handler);
            return Verify(services, descriptors, options);
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
                    return services.Add(set);
                }
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        // ReSharper disable once CognitiveComplexity
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
            
            ConcurrentDictionary<(Type, Object?), List<ServiceDescriptorHandler>> counter = new ConcurrentDictionary<(Type, Object?), List<ServiceDescriptorHandler>>();
            
            lock (services)
            {
                lock (set)
                {
                    foreach (ServiceDescriptor descriptor in set)
                    {
                        ServiceDescriptorHandler item = new ServiceDescriptorHandler(descriptor, ServiceDescriptorHandler.Affiliation.Destination);
                        List<ServiceDescriptorHandler> handlers = counter.GetOrAdd((descriptor.ServiceType, descriptor.ServiceKey), static _ => new List<ServiceDescriptorHandler>(4));
                        
                        if (!handlers.Contains(item))
                        {
                            handlers.Add(item);
                        }
                    }
                    
                    foreach (ServiceDescriptor descriptor in services)
                    {
                        if (!counter.TryGetValue((descriptor.ServiceType, descriptor.ServiceKey), out List<ServiceDescriptorHandler>? handlers))
                        {
                            continue;
                        }
                        
                        handlers.Add(new ServiceDescriptorHandler(descriptor, ServiceDescriptorHandler.Affiliation.Source));
                    }
                    
                    [SuppressMessage("ReSharper", "ForCanBeConvertedToForeach")]
                    [MethodImpl(MethodImplOptions.AggressiveOptimization)]
                    static void Sort(List<ServiceDescriptorHandler> handlers)
                    {
                        ServiceDescriptorHandler[] array = new ServiceDescriptorHandler[handlers.Count];
                        
                        Int32 i = 0;
                        for (Int32 j = 0; j < handlers.Count; j++)
                        {
                            if (handlers[j] is { Source: ServiceDescriptorHandler.Affiliation.Source } handler)
                            {
                                array[i++] = handler;
                            }
                        }
                        
                        for (Int32 j = 0; j < handlers.Count; j++)
                        {
                            if (handlers[j] is { Source: ServiceDescriptorHandler.Affiliation.Destination } handler)
                            {
                                array[i++] = handler;
                            }
                        }
                        
                        handlers.Clear();
                        handlers.AddRange(array);
                    }
                    
                    Parallel.ForEach(counter.Values, Sort);
                    
                    static Exception Exception(KeyValuePair<(Type, Object?), List<ServiceDescriptorHandler>> pair)
                    {
                        TypeComparer comparer = TypeComparer.NameOrdinalIgnoreCase;
                        IEnumerable<ServiceDescriptor> source = pair.Value.Unwrap();
                        String? message = source.OrderBy(static descriptor => descriptor.ImplementationType, comparer).ThenByDescending(static descriptor => descriptor.Lifetime).GetString();
                        return new ScanAmbiguousException(message);
                    }
                    
                    void Handler(KeyValuePair<(Type, Object?), List<ServiceDescriptorHandler>> pair)
                    {
                        List<ServiceDescriptorHandler> container = pair.Value;
                        ServiceDescriptor? descriptor = handler.Invoke(container.AsReadOnlySpan());
                        
                        if (descriptor is null)
                        {
                            return;
                        }
                        
                        container.Remove(descriptor);
                        services.RemoveAll(container.Unwrap());
                        set.RemoveAll(container.Unwrap());
                        
                        container.Clear();
                        container.Add(new ServiceDescriptorHandler(descriptor, ServiceDescriptorHandler.Affiliation.Handle));
                    }
                    
                    Parallel.ForEach(counter.NotUnique(static pair => pair.Value.Count), Handler);
                    
                    const String message = "Ambiguous registration for service type:";
                    Exception[] exceptions = counter.NotUnique(static pair => pair.Value.Count).OrderBy(static pair => pair.Key.Item1, TypeComparer.NameOrdinalIgnoreCase).Select(Exception).ToArray();

                    return exceptions.Length switch
                    {
                        0 => services.Add(set),
                        1 => throw new ScanAmbiguousException(message + ' ' + exceptions[0].Message),
                        _ => throw new ScanAmbiguousException(message, new AggregateException(exceptions))
                    };
                }
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
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
            
            ConcurrentDictionary<(Type, Object?), List<ServiceDescriptorHandler>> counter = new ConcurrentDictionary<(Type, Object?), List<ServiceDescriptorHandler>>();
            
            lock (services)
            {
                lock (set)
                {
                    foreach (ServiceDescriptor descriptor in services)
                    {
                        ServiceDescriptorHandler item = new ServiceDescriptorHandler(descriptor, ServiceDescriptorHandler.Affiliation.Source);
                        counter.GetOrAdd((descriptor.ServiceType, descriptor.ServiceKey), static _ => new List<ServiceDescriptorHandler>()).Add(item);
                    }
                    
                    foreach (ServiceDescriptor descriptor in set)
                    {
                        ServiceDescriptorHandler item = new ServiceDescriptorHandler(descriptor, ServiceDescriptorHandler.Affiliation.Destination);
                        List<ServiceDescriptorHandler> handlers = counter.GetOrAdd((descriptor.ServiceType, descriptor.ServiceKey), static _ => new List<ServiceDescriptorHandler>());
                        
                        if (!handlers.Contains(item))
                        {
                            handlers.Add(item);
                        }
                    }
                    
                    static Exception Exception(KeyValuePair<(Type, Object?), List<ServiceDescriptorHandler>> pair)
                    {
                        TypeComparer comparer = TypeComparer.NameOrdinalIgnoreCase;
                        IEnumerable<ServiceDescriptor> source = pair.Value.Unwrap();
                        String? message = source.OrderBy(static descriptor => descriptor.ImplementationType, comparer).ThenByDescending(static descriptor => descriptor.Lifetime).GetString();
                        return new ScanAmbiguousException(message);
                    }
                    
                    void Handler(KeyValuePair<(Type, Object?), List<ServiceDescriptorHandler>> pair)
                    {
                        List<ServiceDescriptorHandler> container = pair.Value;
                        ServiceDescriptor? descriptor = handler.Invoke(container.AsReadOnlySpan());
                        
                        if (descriptor is null)
                        {
                            return;
                        }
                        
                        container.Remove(descriptor);
                        services.RemoveAll(container.Unwrap());
                        set.RemoveAll(container.Unwrap());
                        
                        container.Clear();
                        container.Add(new ServiceDescriptorHandler(descriptor, ServiceDescriptorHandler.Affiliation.Handle));
                    }
                    
                    Parallel.ForEach(counter.NotUnique(static pair => pair.Value.Count), Handler);
                    
                    const String message = "Ambiguous registration for service type:";
                    Exception[] exceptions = counter.NotUnique(static pair => pair.Value.Count).OrderBy(static pair => pair.Key.Item1, TypeComparer.NameOrdinalIgnoreCase).Select(Exception).ToArray();
                    
                    return exceptions.Length switch
                    {
                        0 => services.Add(set),
                        1 => throw new ScanAmbiguousException(message + ' ' + exceptions[0].Message),
                        _ => throw new ScanAmbiguousException(message, new AggregateException(exceptions))
                    };
                }
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IServiceCollection Verify(IServiceCollection services, ConcurrentHashSet<ServiceDescriptor> set)
        {
            return Verify(services, set, Handler);
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
            return Scan<Any>(services, Handler);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection Scan(this IServiceCollection services, ServiceAmbiguousHandler handler)
        {
            return Scan<Any>(services, handler);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection Scan<T>(this IServiceCollection services) where T : class
        {
            return Scan<T>(services, Handler);
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
            
            ImmutableHashSet<Type> attributes = inherit.Type[typeof(ServiceDependencyAttribute)].Types.Add(typeof(ServiceDependencyAttribute));
            ImmutableHashSet<Type> set = result.Inherit.Types.Union(attributes.SelectMany(attribute => inherit.Attribute[attribute].Types));
            ImmutableHashSet<Type> except = inherit.Type[typeof(IUnscanDependencyService)].Add(typeof(IUnscanDependencyService));

            if (typeof(T) == typeof(Any))
            {
                except = except.Union(inherit.Type[typeof(ISpecialServiceDependency)].Add(typeof(ISpecialServiceDependency)));
            }
            else if (inherit[typeof(Attribute)].Contains(typeof(T)))
            {
                set = set.Intersect(inherit.Attribute[typeof(T)]);
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
                Inherit = inherit,
                Except = except,
                ServiceDependency = result,
                Handler = handler
            };
            
            return Scan(services, options);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection Scan(this IServiceCollection services, IEnumerable<Type?> source)
        {
            return Scan(services, source, Handler);
        }
        
        public static IServiceCollection Scan(this IServiceCollection services, IEnumerable<Type?> source, ServiceAmbiguousHandler handler)
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
            
            ImmutableHashSet<Type> attributes = inherit.Type[typeof(ServiceDependencyAttribute)].Types.Add(typeof(ServiceDependencyAttribute));
            ImmutableHashSet<Type> set = result.Inherit.Types.Union(attributes.SelectMany(attribute => inherit.Attribute[attribute].Types));
            ImmutableHashSet<Type> except = inherit.Type[typeof(IUnscanDependencyService)].Add(typeof(IUnscanDependencyService));
            
            Options options = new Options
            {
                Source = set.Except(except).Intersect(source!),
                Interfaces = result.Interfaces.Union(result.Generic.Interfaces).Except(except),
                Inherit = inherit,
                Except = except,
                ServiceDependency = result,
                Handler = handler
            };
            
            return Scan(services, options);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection Scan(this IServiceCollection services, Assembly assembly)
        {
            return Scan(services, assembly, Handler);
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
            
            return Scan(services, assembly.GetSafeTypes(), handler);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection Scan(this IServiceCollection services, IEnumerable<Assembly?> assemblies)
        {
            return Scan(services, assemblies, Handler);
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
            
            return Scan(services, assemblies.GetTypes(), handler);
        }
    }
}