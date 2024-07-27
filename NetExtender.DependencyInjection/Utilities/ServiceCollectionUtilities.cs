// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NetExtender.DependencyInjection.Interfaces;
using NetExtender.Types.Exceptions;

namespace NetExtender.Utilities.Types
{
    public static partial class ServiceCollectionUtilities
    {
        private static Type TransientType { get; } = typeof(ITransient);
        private static Type ScopedType { get; } = typeof(IScoped);
        private static Type SingletonType { get; } = typeof(ISingleton);
        
        private static Boolean GetServiceType(this Type type, out ServiceLifetime result)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (type.IsInterface || type.IsAbstract)
            {
                result = default;
                return false;
            }

            if (TransientType.IsAssignableFrom(type))
            {
                result = ServiceLifetime.Transient;
                return true;
            }

            if (ScopedType.IsAssignableFrom(type))
            {
                result = ServiceLifetime.Scoped;
                return true;
            }

            if (SingletonType.IsAssignableFrom(type))
            {
                result = ServiceLifetime.Singleton;
                return true;
            }

            result = default;
            return false;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InjectTo(this IServiceCollection source)
        {
            InjectTo(source, ServiceAmbiguousHandler.New);
        }
        
        public static void InjectTo(this IServiceCollection source, ServiceAmbiguousHandler handler)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            ServiceProviderUtilities.ICustomServiceProvider provider = (ServiceProviderUtilities.ICustomServiceProvider) ServiceProviderUtilities.Provider;
            InjectTo(source, provider.Services, handler);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InjectFrom(this IServiceCollection destination)
        {
            InjectFrom(destination, ServiceAmbiguousHandler.New);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InjectFrom(this IServiceCollection destination, ServiceAmbiguousHandler handler)
        {
            if (destination is null)
            {
                throw new ArgumentNullException(nameof(destination));
            }

            ServiceProviderUtilities.ICustomServiceProvider provider = (ServiceProviderUtilities.ICustomServiceProvider) ServiceProviderUtilities.Provider;
            InjectFrom(destination, provider.Services, handler);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection InjectTo(this IServiceCollection source, IServiceCollection destination)
        {
            return InjectTo(source, destination, ServiceAmbiguousHandler.New);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static IServiceCollection InjectTo(this IServiceCollection source, IServiceCollection destination, ServiceAmbiguousHandler handler)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            if (destination is null)
            {
                throw new ArgumentNullException(nameof(destination));
            }
            
            ConcurrentHashSet<ServiceDescriptor> set = new ConcurrentHashSet<ServiceDescriptor>(source);
            return Verify(destination, set, handler);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection InjectFrom(this IServiceCollection destination, IServiceCollection source)
        {
            return InjectFrom(destination, source, ServiceAmbiguousHandler.New);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static IServiceCollection InjectFrom(this IServiceCollection destination, IServiceCollection source, ServiceAmbiguousHandler handler)
        {
            if (destination is null)
            {
                throw new ArgumentNullException(nameof(destination));
            }
            
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            ConcurrentHashSet<ServiceDescriptor> set = new ConcurrentHashSet<ServiceDescriptor>(source);
            return Verify(destination, set, handler);
        }

        public static IServiceCollection Register(this IServiceCollection collection, Type type)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            return type.GetServiceType(out ServiceLifetime lifetime) ? collection.Add(type, lifetime) : collection;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection Register(this IServiceCollection collection, params Type?[]? types)
        {
            return Register(collection, (IEnumerable<Type?>?) types);
        }
        
        public static IServiceCollection Register(this IServiceCollection collection, IEnumerable<Type?>? types)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }
            
            return types is not null ? types.WhereNotNull().Aggregate(collection, (current, type) => current.Register(type)) : collection;
        }
        
        public static IServiceCollection Register(this IServiceCollection collection, Assembly assembly)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            return assembly.DefinedTypes.Aggregate(collection, (current, type) => current.Register(type));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection Register(this IServiceCollection collection, params Assembly?[]? assemblies)
        {
            return Register(collection, (IEnumerable<Assembly?>?) assemblies);
        }
        
        public static IServiceCollection Register(this IServiceCollection collection, IEnumerable<Assembly?>? assemblies)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            return assemblies is not null ? assemblies.WhereNotNull().Aggregate(collection, (current, assembly) => current.Register(assembly)) : collection;
        }

        public static IServiceCollection Add(this IServiceCollection collection, Type service, ServiceLifetime lifetime)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (service is null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            return lifetime switch
            {
                ServiceLifetime.Transient => collection.AddTransient(service),
                ServiceLifetime.Scoped => collection.AddScoped(service),
                ServiceLifetime.Singleton => collection.AddSingleton(service),
                _ => throw new EnumUndefinedOrNotSupportedException<ServiceLifetime>(lifetime, nameof(lifetime), null)
            };
        }

        public static IServiceCollection Add(this IServiceCollection collection, Type service, Type implementation, ServiceLifetime lifetime)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (service is null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            if (implementation is null)
            {
                throw new ArgumentNullException(nameof(implementation));
            }

            return lifetime switch
            {
                ServiceLifetime.Transient => collection.AddTransient(service, implementation),
                ServiceLifetime.Scoped => collection.AddScoped(service, implementation),
                ServiceLifetime.Singleton => collection.AddSingleton(service, implementation),
                _ => throw new EnumUndefinedOrNotSupportedException<ServiceLifetime>(lifetime, nameof(lifetime), null)
            };
        }

        public static IServiceCollection Add(this IServiceCollection collection, Type service, Func<IServiceProvider, Object> factory, ServiceLifetime lifetime)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (service is null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return lifetime switch
            {
                ServiceLifetime.Transient => collection.AddTransient(service, factory),
                ServiceLifetime.Scoped => collection.AddScoped(service, factory),
                ServiceLifetime.Singleton => collection.AddSingleton(service, factory),
                _ => throw new EnumUndefinedOrNotSupportedException<ServiceLifetime>(lifetime, nameof(lifetime), null)
            };
        }

        public static IServiceCollection Add<TService>(this IServiceCollection collection, ServiceLifetime lifetime) where TService : class
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            return lifetime switch
            {
                ServiceLifetime.Transient => collection.AddTransient<TService>(),
                ServiceLifetime.Scoped => collection.AddScoped<TService>(),
                ServiceLifetime.Singleton => collection.AddSingleton<TService>(),
                _ => throw new EnumUndefinedOrNotSupportedException<ServiceLifetime>(lifetime, nameof(lifetime), null)
            };
        }

        public static IServiceCollection Add<TService, T>(this IServiceCollection collection, Func<IServiceProvider, T> factory, ServiceLifetime lifetime) where TService : class where T : class, TService
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return lifetime switch
            {
                ServiceLifetime.Transient => collection.AddTransient<TService, T>(factory),
                ServiceLifetime.Scoped => collection.AddScoped<TService, T>(factory),
                ServiceLifetime.Singleton => collection.AddSingleton<TService, T>(factory),
                _ => throw new EnumUndefinedOrNotSupportedException<ServiceLifetime>(lifetime, nameof(lifetime), null)
            };
        }

        public static IServiceCollection Add(this IServiceCollection collection, IServiceDependency service)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (service is null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            return collection.Add(service.GetType(), service.Lifetime);
        }

        public static IServiceCollection AddIf(this IServiceCollection collection, Func<IServiceCollection, IServiceCollection> modifier, Boolean condition)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (modifier is null)
            {
                throw new ArgumentNullException(nameof(modifier));
            }

            return condition ? modifier(collection) : collection;
        }

        public static IServiceCollection AddIf(this IServiceCollection collection, Func<IServiceCollection, IServiceCollection> @if, Func<IServiceCollection, IServiceCollection> @else, Boolean condition)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (@if is null)
            {
                throw new ArgumentNullException(nameof(@if));
            }

            if (@else is null)
            {
                throw new ArgumentNullException(nameof(@else));
            }

            return condition ? @if(collection) : @else(collection);
        }

        public static IServiceCollection AddIfNot(this IServiceCollection collection, Func<IServiceCollection, IServiceCollection> modifier, Boolean condition)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (modifier is null)
            {
                throw new ArgumentNullException(nameof(modifier));
            }

            return !condition ? modifier(collection) : collection;
        }

        public static IServiceCollection AddIfNot(this IServiceCollection collection, Func<IServiceCollection, IServiceCollection> @if, Func<IServiceCollection, IServiceCollection> @else, Boolean condition)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (@if is null)
            {
                throw new ArgumentNullException(nameof(@if));
            }

            if (@else is null)
            {
                throw new ArgumentNullException(nameof(@else));
            }

            return !condition ? @if(collection) : @else(collection);
        }

        public static IServiceCollection AddLogging(this IServiceCollection collection)
        {
            return AddLogging<Object?>(collection);
        }

        public static IServiceCollection AddLogging<T>(this IServiceCollection collection)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            return collection.AddSingleton<ILogger>(provider => provider.GetRequiredService<ILogger<T>>());
        }
    }
}