using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using NetExtender.Cecil;
using NetExtender.DependencyInjection.Interfaces;
using NetExtender.Exceptions;
using NetExtender.Utilities.Core;

namespace NetExtender.Utilities.Types
{
    public static partial class ServiceCollectionUtilities
    {
        private readonly struct Options
        {
            public TypeSet Source { get; init; }
            public TypeSet Interfaces { get; init; }
            public IImmutableSet<MonoCecilType> Except { get; init; }
            public Inherit.Result Inherit { get; init; }
            public ReflectionInheritResult ServiceDependency { get; init; }
            public ServiceAmbiguousHandler Handler { get; init; }
        }

        private readonly struct Split : IStruct<Split>
        {
            public MonoCecilType Type { get; }
            public TypeSet Transient { get; }
            public TypeSet Scoped { get; }
            public TypeSet Singleton { get; }
            public ServiceLifetime? Lifetime { get; }

            public Boolean IsEmpty
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Type is null;
                }
            }

            public Split(MonoCecilType type, Options options, TypeSet interfaces)
            {
                Type = type ?? throw new ArgumentNullException(nameof(type));

                if (interfaces is null)
                {
                    throw new ArgumentNullException(nameof(interfaces));
                }

                Transient = Handle(typeof(ITransient<>), options, interfaces);
                Scoped = Handle(typeof(IScoped<>), options, interfaces);
                Singleton = Handle(typeof(ISingleton<>), options, interfaces);

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
            private static TypeSet Handle(MonoCecilType @interface, Options options, TypeSet interfaces)
            {
                if (!options.Inherit.TryGetValue(@interface, out ReflectionInheritResult? result))
                {
                    return TypeSet.Empty;
                }

                TypeSet targets = result.Interfaces;

                if (options.ServiceDependency is { } dependency)
                {
                    targets = targets.Except(dependency.Interfaces);
                }

                if (targets.Count > 0 && interfaces.Intersect(targets) is { Count: > 0 } services)
                {
                    return services;
                }

                return TypeSet.Empty;
            }

            [MethodImpl(MethodImplOptions.AggressiveOptimization)]
            private static ServiceLifetime? VerifyLifetime(TypeSet interfaces)
            {
                if (interfaces is null)
                {
                    throw new ArgumentNullException(nameof(interfaces));
                }

                if (interfaces.Intersect(TypeSet.Create(IDependencyService.Services.Keys)) is not { Count: 1 } intersect)
                {
                    return null;
                }

                return IDependencyService.Services.TryGetValue(intersect.Single, out ServiceLifetime lifetime) ? lifetime : null;
            }

            public void Populate(ICollection<MonoCecilType> collection)
            {
                if (collection is null)
                {
                    throw new ArgumentNullException(nameof(collection));
                }

                Populate(collection, ServiceLifetime.Transient);
                Populate(collection, ServiceLifetime.Scoped);
                Populate(collection, ServiceLifetime.Singleton);
            }

            public void Populate(ICollection<MonoCecilType> collection, ServiceLifetime lifetime)
            {
                if (collection is null)
                {
                    throw new ArgumentNullException(nameof(collection));
                }

                IImmutableSet<MonoCecilType> services = lifetime switch
                {
                    ServiceLifetime.Transient => Transient,
                    ServiceLifetime.Scoped => Scoped,
                    ServiceLifetime.Singleton => Singleton,
                    _ => throw new EnumUndefinedOrNotSupportedException<ServiceLifetime>(lifetime, nameof(lifetime), null)
                };

                foreach (MonoCecilType service in services)
                {
                    collection.Add(service);
                }
            }
        }
    }
}