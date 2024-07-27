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
    
    public readonly struct ServiceAmbiguousHandler
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
        
        private readonly struct Split
        {
            public Type Type { get; }
            public ImmutableHashSet<Type> Transient { get; }
            public ImmutableHashSet<Type> Scoped { get; }
            public ImmutableHashSet<Type> Singleton { get; }
            public ServiceLifetime? Lifetime { get; }
            
            public Boolean IsEmpty
            {
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
                
                if (interfaces.Intersect(IServiceDependency.Services.Keys) is not { Count: 1 } intersect)
                {
                    return null;
                }
                
                return IServiceDependency.Services.TryGetValue(intersect.Single(), out ServiceLifetime lifetime) ? lifetime : null;
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
        
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static IReadOnlyCollection<ServiceDescriptor> Create(Type @interface, Type type, ServiceLifetime lifetime, Options options)
        {
            if (@interface is null)
            {
                throw new ArgumentNullException(nameof(@interface));
            }
            
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            if (!@interface.IsInterface)
            {
                return ImmutableList<ServiceDescriptor>.Empty.Add(new ServiceDescriptor(@interface, type, lifetime));
            }
            
            if (options.Inherit.TryGetValue(@interface, out ReflectionInheritResult? result) && !result.Inherit.Types.Contains(type))
            {
                throw new TypeNotSupportedException(type, $"Type '{type}' must implement interface '{@interface}'.");
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            static Boolean IsServiceDependency(Type @interface)
            {
                return @interface.IsGenericType && @interface.GetGenericTypeDefinition() == typeof(IServiceDependency<>);
            }
            
            Dictionary<Type, ServiceDescriptor> descriptors = new Dictionary<Type, ServiceDescriptor>();
            
            foreach (Type argument in @interface.GetInterfaces().Where(IsServiceDependency).Select(static dependency => dependency.GenericTypeArguments[0]))
            {
                if (!argument.IsInterface || !options.Inherit.TryGetValue(argument, out result) || !result.Inherit.Types.Contains(type))
                {
                    throw new TypeNotSupportedException(type, $"Type '{type}' must implement interface '{argument}'.");
                }
                
                descriptors.TryAdd(argument, new ServiceDescriptor(argument, type, lifetime));
            }
            
            descriptors.TryAdd(@interface, new ServiceDescriptor(@interface, type, lifetime));
            return descriptors.Values.ToImmutableList();
        }
        
        // ReSharper disable once CognitiveComplexity
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static IServiceCollection Scan(this IServiceCollection collection, Options options)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }
            
            ConcurrentHashSet<ServiceDescriptor> services = new ConcurrentHashSet<ServiceDescriptor>(ServiceDescriptorEqualityComparer.Implementation);

            if (options.Inherit.TryGetValue(typeof(IServiceDependency), out ReflectionInheritResult? dependency))
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
            void Handler(Type type)
            {
                ImmutableHashSet<Type> interfaces = type.GetInterfaces().Where(@interface => options.Interfaces.Contains(@interface.TryGetGenericTypeDefinition())).ToImmutableHashSet();
                
                if (interfaces.Count > 0)
                {
                    Split split = new Split(type, options, interfaces);
                    
                    foreach (Type @interface in split.Transient)
                    {
                        services.AddRange(Create(@interface, type, ServiceLifetime.Transient, options));
                    }
                    
                    foreach (Type @interface in split.Scoped)
                    {
                        services.AddRange(Create(@interface, type, ServiceLifetime.Scoped, options));
                    }
                    
                    foreach (Type @interface in split.Singleton)
                    {
                        services.AddRange(Create(@interface, type, ServiceLifetime.Singleton, options));
                    }
                    
                    if (split.Lifetime is { } lifetime)
                    {
                        services.Add(new ServiceDescriptor(type, type, lifetime));
                    }
                }
                
                foreach (ServiceDependencyAttribute attribute in ReflectionUtilities.GetCustomAttributes<ServiceDependencyAttribute>(type))
                {
                    if (attribute.Interface is { } @interface)
                    {
                        services.AddRange(Create(@interface, type, attribute.Lifetime, options));
                    }

                    services.Add(new ServiceDescriptor(type, type, attribute.Lifetime));
                }
            }
            
            Parallel.ForEach(options.Source, Handler);
            return Verify(collection, services, options);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static IServiceCollection VerifyIgnore(IServiceCollection collection, ConcurrentHashSet<ServiceDescriptor> set)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }
            
            if (set is null)
            {
                throw new ArgumentNullException(nameof(set));
            }
            
            lock (collection)
            {
                lock (set)
                {
                    return collection.Add(set);
                }
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        // ReSharper disable once CognitiveComplexity
        private static IServiceCollection VerifyNew(IServiceCollection collection, ConcurrentHashSet<ServiceDescriptor> set, ServiceAmbiguousHandler handler)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }
            
            if (set is null)
            {
                throw new ArgumentNullException(nameof(set));
            }
            
            ConcurrentDictionary<Type, List<ServiceDescriptorHandler>> counter = new ConcurrentDictionary<Type, List<ServiceDescriptorHandler>>();
            
            lock (collection)
            {
                lock (set)
                {
                    foreach (ServiceDescriptor descriptor in set)
                    {
                        ServiceDescriptorHandler item = new ServiceDescriptorHandler(descriptor, ServiceDescriptorHandler.Affiliation.Destination);
                        List<ServiceDescriptorHandler> handlers = counter.GetOrAdd(descriptor.ServiceType, static _ => new List<ServiceDescriptorHandler>());
                        
                        if (!handlers.Contains(item))
                        {
                            handlers.Add(item);
                        }
                    }
                    
                    foreach (ServiceDescriptor descriptor in collection)
                    {
                        if (!counter.TryGetValue(descriptor.ServiceType, out List<ServiceDescriptorHandler>? handlers))
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
                    
                    static Exception Exception(KeyValuePair<Type, List<ServiceDescriptorHandler>> pair)
                    {
                        TypeComparer comparer = TypeComparer.NameOrdinalIgnoreCase;
                        IEnumerable<ServiceDescriptor> source = pair.Value.Unwrap();
                        String? message = source.OrderBy(static descriptor => descriptor.ImplementationType, comparer).ThenByDescending(static descriptor => descriptor.Lifetime).GetString();
                        return new ScanAmbiguousException(message);
                    }
                    
                    void Handler(KeyValuePair<Type, List<ServiceDescriptorHandler>> pair)
                    {
                        List<ServiceDescriptorHandler> container = pair.Value;
                        ServiceDescriptor? descriptor = handler.Invoke(container.AsReadOnlySpan());
                        
                        if (descriptor is null)
                        {
                            return;
                        }
                        
                        container.Remove(descriptor);
                        collection.RemoveAll(container.Unwrap());
                        set.RemoveAll(container.Unwrap());
                        
                        container.Clear();
                        container.Add(new ServiceDescriptorHandler(descriptor, ServiceDescriptorHandler.Affiliation.Handle));
                    }
                    
                    Parallel.ForEach(counter.NotUnique(static pair => pair.Value.Count), Handler);
                    
                    const String message = "Ambiguous registration for service type:";
                    Exception[] exceptions = counter.NotUnique(static pair => pair.Value.Count).OrderBy(static pair => pair.Key, TypeComparer.NameOrdinalIgnoreCase).Select(Exception).ToArray();
                    
                    return exceptions.Length switch
                    {
                        0 => collection.Add(set),
                        1 => throw new ScanAmbiguousException(message + ' ' + exceptions[0].Message),
                        _ => throw new ScanAmbiguousException(message, new AggregateException(exceptions))
                    };
                }
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        private static IServiceCollection VerifyThrow(IServiceCollection collection, ConcurrentHashSet<ServiceDescriptor> set, ServiceAmbiguousHandler handler)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }
            
            if (set is null)
            {
                throw new ArgumentNullException(nameof(set));
            }
            
            ConcurrentDictionary<Type, List<ServiceDescriptorHandler>> counter = new ConcurrentDictionary<Type, List<ServiceDescriptorHandler>>();
            
            lock (collection)
            {
                lock (set)
                {
                    foreach (ServiceDescriptor descriptor in collection)
                    {
                        ServiceDescriptorHandler item = new ServiceDescriptorHandler(descriptor, ServiceDescriptorHandler.Affiliation.Source);
                        counter.GetOrAdd(descriptor.ServiceType, static _ => new List<ServiceDescriptorHandler>()).Add(item);
                    }
                    
                    foreach (ServiceDescriptor descriptor in set)
                    {
                        ServiceDescriptorHandler item = new ServiceDescriptorHandler(descriptor, ServiceDescriptorHandler.Affiliation.Destination);
                        List<ServiceDescriptorHandler> handlers = counter.GetOrAdd(descriptor.ServiceType, static _ => new List<ServiceDescriptorHandler>());
                        
                        if (!handlers.Contains(item))
                        {
                            handlers.Add(item);
                        }
                    }
                    
                    static Exception Exception(KeyValuePair<Type, List<ServiceDescriptorHandler>> pair)
                    {
                        TypeComparer comparer = TypeComparer.NameOrdinalIgnoreCase;
                        IEnumerable<ServiceDescriptor> source = pair.Value.Unwrap();
                        String? message = source.OrderBy(static descriptor => descriptor.ImplementationType, comparer).ThenByDescending(static descriptor => descriptor.Lifetime).GetString();
                        return new ScanAmbiguousException(message);
                    }
                    
                    void Handler(KeyValuePair<Type, List<ServiceDescriptorHandler>> pair)
                    {
                        List<ServiceDescriptorHandler> container = pair.Value;
                        ServiceDescriptor? descriptor = handler.Invoke(container.AsReadOnlySpan());
                        
                        if (descriptor is null)
                        {
                            return;
                        }
                        
                        container.Remove(descriptor);
                        collection.RemoveAll(container.Unwrap());
                        set.RemoveAll(container.Unwrap());
                        
                        container.Clear();
                        container.Add(new ServiceDescriptorHandler(descriptor, ServiceDescriptorHandler.Affiliation.Handle));
                    }
                    
                    Parallel.ForEach(counter.NotUnique(static pair => pair.Value.Count), Handler);
                    
                    const String message = "Ambiguous registration for service type:";
                    Exception[] exceptions = counter.NotUnique(static pair => pair.Value.Count).OrderBy(static pair => pair.Key, TypeComparer.NameOrdinalIgnoreCase).Select(Exception).ToArray();
                    
                    return exceptions.Length switch
                    {
                        0 => collection.Add(set),
                        1 => throw new ScanAmbiguousException(message + ' ' + exceptions[0].Message),
                        _ => throw new ScanAmbiguousException(message, new AggregateException(exceptions))
                    };
                }
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IServiceCollection Verify(IServiceCollection collection, ConcurrentHashSet<ServiceDescriptor> set)
        {
            return Verify(collection, set, Handler);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IServiceCollection Verify(IServiceCollection collection, ConcurrentHashSet<ServiceDescriptor> set, Options options)
        {
            return Verify(collection, set, options.Handler);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IServiceCollection Verify(IServiceCollection collection, ConcurrentHashSet<ServiceDescriptor> set, ServiceAmbiguousHandler handler)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }
            
            if (set is null)
            {
                throw new ArgumentNullException(nameof(set));
            }
            
            return handler.Type switch
            {
                ServiceAmbiguousHandlerType.Ignore => VerifyIgnore(collection, set),
                ServiceAmbiguousHandlerType.Throw => VerifyThrow(collection, set, handler),
                ServiceAmbiguousHandlerType.New => VerifyNew(collection, set, handler),
                ServiceAmbiguousHandlerType.Custom => VerifyThrow(collection, set, handler),
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
        public static IServiceCollection Scan(this IServiceCollection collection)
        {
            return Scan(collection, Handler);
        }
        
        public static IServiceCollection Scan(this IServiceCollection collection, ServiceAmbiguousHandler handler)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }
            
            Inherit.Result inherit = ReflectionUtilities.Inherit;
            if (!inherit.TryGetValue(typeof(IServiceDependency), out ReflectionInheritResult? result))
            {
                throw new InvalidOperationException();
            }
            
            ImmutableHashSet<Type> attributes = inherit.Type[typeof(ServiceDependencyAttribute)].Types.Add(typeof(ServiceDependencyAttribute));
            ImmutableHashSet<Type> set = result.Inherit.Types.Union(attributes.SelectMany(attribute => inherit.Attribute[attribute].Types));
            ImmutableHashSet<Type> except = inherit.Type[typeof(IUnscanServiceDependency)].Add(typeof(IUnscanServiceDependency));
            
            Options options = new Options
            {
                Source = set.Except(except),
                Interfaces = result.Interfaces.Union(result.Generic.Interfaces).Except(except),
                Inherit = inherit,
                Except = except,
                ServiceDependency = result,
                Handler = handler
            };
            
            return Scan(collection, options);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection Scan(this IServiceCollection collection, IEnumerable<Type?> source)
        {
            return Scan(collection, source, Handler);
        }
        
        public static IServiceCollection Scan(this IServiceCollection collection, IEnumerable<Type?> source, ServiceAmbiguousHandler handler)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }
            
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            Inherit.Result inherit = ReflectionUtilities.Inherit;
            if (!inherit.TryGetValue(typeof(IServiceDependency), out ReflectionInheritResult? result))
            {
                throw new InvalidOperationException();
            }
            
            ImmutableHashSet<Type> attributes = inherit.Type[typeof(ServiceDependencyAttribute)].Types.Add(typeof(ServiceDependencyAttribute));
            ImmutableHashSet<Type> set = result.Inherit.Types.Union(attributes.SelectMany(attribute => inherit.Attribute[attribute].Types));
            ImmutableHashSet<Type> except = inherit.Type[typeof(IUnscanServiceDependency)].Add(typeof(IUnscanServiceDependency));
            
            Options options = new Options
            {
                Source = set.Except(except).Intersect(source!),
                Interfaces = result.Interfaces.Union(result.Generic.Interfaces).Except(except),
                Inherit = inherit,
                Except = except,
                ServiceDependency = result,
                Handler = handler
            };
            
            return Scan(collection, options);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection Scan(this IServiceCollection collection, Assembly assembly)
        {
            return Scan(collection, assembly, Handler);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection Scan(this IServiceCollection collection, Assembly assembly, ServiceAmbiguousHandler handler)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }
            
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }
            
            return Scan(collection, assembly.GetTypes(), handler);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection Scan(this IServiceCollection collection, IEnumerable<Assembly?> assemblies)
        {
            return Scan(collection, assemblies, Handler);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection Scan(this IServiceCollection collection, IEnumerable<Assembly?> assemblies, ServiceAmbiguousHandler handler)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }
            
            if (assemblies is null)
            {
                throw new ArgumentNullException(nameof(assemblies));
            }
            
            return Scan(collection, assemblies.GetTypes(), handler);
        }
    }
}