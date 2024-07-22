// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using Microsoft.Extensions.DependencyInjection;

namespace NetExtender.Utilities.Types
{
    public static partial class ServiceCollectionUtilities
    {
        public static IServiceCollection AddTransientIf(this IServiceCollection collection, Type service, Boolean condition)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (service is null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            return condition ? collection.AddSingleton(service) : collection;
        }

        public static IServiceCollection AddTransientIfNot(this IServiceCollection collection, Type service, Boolean condition)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (service is null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            return !condition ? collection.AddTransient(service) : collection;
        }

        public static IServiceCollection AddTransientIf(this IServiceCollection collection, Type service, Type implementation, Boolean condition)
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

            return condition ? collection.AddTransient(service, implementation) : collection;
        }

        public static IServiceCollection AddTransientIfNot(this IServiceCollection collection, Type service, Type implementation, Boolean condition)
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

            return !condition ? collection.AddTransient(service, implementation) : collection;
        }

        public static IServiceCollection AddTransientIf(this IServiceCollection collection, Type service, Func<IServiceProvider, Object> factory, Boolean condition)
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

            return condition ? collection.AddTransient(service, factory) : collection;
        }

        public static IServiceCollection AddTransientIfNot(this IServiceCollection collection, Type service, Func<IServiceProvider, Object> factory, Boolean condition)
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

            return !condition ? collection.AddTransient(service, factory) : collection;
        }

        public static IServiceCollection AddTransientIf<TService>(this IServiceCollection collection, Boolean condition) where TService : class
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            return condition ? collection.AddTransient<TService>() : collection;
        }

        public static IServiceCollection AddTransientIfNot<TService>(this IServiceCollection collection, Boolean condition) where TService : class
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            return !condition ? collection.AddTransient<TService>() : collection;
        }

        public static IServiceCollection AddTransientIf<TService>(this IServiceCollection collection, Func<IServiceProvider, TService> factory, Boolean condition) where TService : class
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return condition ? collection.AddTransient(factory) : collection;
        }

        public static IServiceCollection AddTransientIfNot<TService>(this IServiceCollection collection, Func<IServiceProvider, TService> factory, Boolean condition) where TService : class
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return !condition ? collection.AddTransient(factory) : collection;
        }

        public static IServiceCollection AddTransientIf<TService, T>(this IServiceCollection collection, Boolean condition) where TService : class where T : class, TService
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            return condition ? collection.AddTransient<TService, T>() : collection;
        }

        public static IServiceCollection AddTransientIfNot<TService, T>(this IServiceCollection collection, Boolean condition) where TService : class where T : class, TService
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            return !condition ? collection.AddTransient<TService, T>() : collection;
        }

        public static IServiceCollection AddTransientIf<TService, T>(this IServiceCollection collection, Func<IServiceProvider, T> factory, Boolean condition) where TService : class where T : class, TService
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return condition ? collection.AddTransient<TService, T>(factory) : collection;
        }

        public static IServiceCollection AddTransientIfNot<TService, T>(this IServiceCollection collection, Func<IServiceProvider, T> factory, Boolean condition) where TService : class where T : class, TService
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return !condition ? collection.AddTransient<TService, T>(factory) : collection;
        }

        public static IServiceCollection AddScopedIf(this IServiceCollection collection, Type service, Boolean condition)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (service is null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            return condition ? collection.AddScoped(service) : collection;
        }

        public static IServiceCollection AddScopedIfNot(this IServiceCollection collection, Type service, Boolean condition)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (service is null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            return !condition ? collection.AddScoped(service) : collection;
        }

        public static IServiceCollection AddScopedIf(this IServiceCollection collection, Type service, Type implementation, Boolean condition)
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

            return condition ? collection.AddScoped(service, implementation) : collection;
        }

        public static IServiceCollection AddScopedIfNot(this IServiceCollection collection, Type service, Type implementation, Boolean condition)
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

            return !condition ? collection.AddScoped(service, implementation) : collection;
        }

        public static IServiceCollection AddScopedIf(this IServiceCollection collection, Type service, Func<IServiceProvider, Object> factory, Boolean condition)
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

            return condition ? collection.AddScoped(service, factory) : collection;
        }

        public static IServiceCollection AddScopedIfNot(this IServiceCollection collection, Type service, Func<IServiceProvider, Object> factory, Boolean condition)
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

            return !condition ? collection.AddScoped(service, factory) : collection;
        }

        public static IServiceCollection AddScopedIf<TService>(this IServiceCollection collection, Boolean condition) where TService : class
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            return condition ? collection.AddScoped<TService>() : collection;
        }

        public static IServiceCollection AddScopedIfNot<TService>(this IServiceCollection collection, Boolean condition) where TService : class
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            return !condition ? collection.AddScoped<TService>() : collection;
        }

        public static IServiceCollection AddScopedIf<TService>(this IServiceCollection collection, Func<IServiceProvider, TService> factory, Boolean condition) where TService : class
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return condition ? collection.AddScoped(factory) : collection;
        }

        public static IServiceCollection AddScopedIfNot<TService>(this IServiceCollection collection, Func<IServiceProvider, TService> factory, Boolean condition) where TService : class
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return !condition ? collection.AddScoped(factory) : collection;
        }

        public static IServiceCollection AddScopedIf<TService, T>(this IServiceCollection collection, Boolean condition) where TService : class where T : class, TService
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            return condition ? collection.AddScoped<TService, T>() : collection;
        }

        public static IServiceCollection AddScopedIfNot<TService, T>(this IServiceCollection collection, Boolean condition) where TService : class where T : class, TService
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            return !condition ? collection.AddScoped<TService, T>() : collection;
        }

        public static IServiceCollection AddScopedIf<TService, T>(this IServiceCollection collection, Func<IServiceProvider, T> factory, Boolean condition) where TService : class where T : class, TService
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return condition ? collection.AddScoped<TService, T>(factory) : collection;
        }

        public static IServiceCollection AddScopedIfNot<TService, T>(this IServiceCollection collection, Func<IServiceProvider, T> factory, Boolean condition) where TService : class where T : class, TService
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return !condition ? collection.AddScoped<TService, T>(factory) : collection;
        }

        public static IServiceCollection AddSingletonIf(this IServiceCollection collection, Type service, Boolean condition)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (service is null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            return condition ? collection.AddSingleton(service) : collection;
        }

        public static IServiceCollection AddSingletonIfNot(this IServiceCollection collection, Type service, Boolean condition)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (service is null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            return !condition ? collection.AddSingleton(service) : collection;
        }

        public static IServiceCollection AddSingletonIf(this IServiceCollection collection, Type service, Type implementation, Boolean condition)
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

            return condition ? collection.AddSingleton(service, implementation) : collection;
        }

        public static IServiceCollection AddSingletonIfNot(this IServiceCollection collection, Type service, Type implementation, Boolean condition)
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

            return !condition ? collection.AddSingleton(service, implementation) : collection;
        }

        public static IServiceCollection AddSingletonIf(this IServiceCollection collection, Type service, Object implementation, Boolean condition)
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

            return condition ? collection.AddSingleton(service, implementation) : collection;
        }

        public static IServiceCollection AddSingletonIfNot(this IServiceCollection collection, Type service, Object implementation, Boolean condition)
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

            return !condition ? collection.AddSingleton(service, implementation) : collection;
        }

        public static IServiceCollection AddSingletonIf(this IServiceCollection collection, Type service, Func<IServiceProvider, Object> factory, Boolean condition)
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

            return condition ? collection.AddSingleton(service, factory) : collection;
        }

        public static IServiceCollection AddSingletonIfNot(this IServiceCollection collection, Type service, Func<IServiceProvider, Object> factory, Boolean condition)
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

            return !condition ? collection.AddSingleton(service, factory) : collection;
        }

        public static IServiceCollection AddSingletonIf<TService>(this IServiceCollection collection, Boolean condition) where TService : class
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            return condition ? collection.AddSingleton<TService>() : collection;
        }

        public static IServiceCollection AddSingletonIfNot<TService>(this IServiceCollection collection, Boolean condition) where TService : class
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            return !condition ? collection.AddSingleton<TService>() : collection;
        }

        public static IServiceCollection AddSingletonIf<TService>(this IServiceCollection collection, TService implementation, Boolean condition) where TService : class
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (implementation is null)
            {
                throw new ArgumentNullException(nameof(implementation));
            }

            return condition ? collection.AddSingleton(implementation) : collection;
        }

        public static IServiceCollection AddSingletonIfNot<TService>(this IServiceCollection collection, TService implementation, Boolean condition) where TService : class
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (implementation is null)
            {
                throw new ArgumentNullException(nameof(implementation));
            }

            return !condition ? collection.AddSingleton(implementation) : collection;
        }

        public static IServiceCollection AddSingletonIf<TService>(this IServiceCollection collection, Func<IServiceProvider, TService> factory, Boolean condition) where TService : class
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return condition ? collection.AddSingleton(factory) : collection;
        }

        public static IServiceCollection AddSingletonIfNot<TService>(this IServiceCollection collection, Func<IServiceProvider, TService> factory, Boolean condition) where TService : class
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return !condition ? collection.AddSingleton(factory) : collection;
        }

        public static IServiceCollection AddSingletonIf<TService, T>(this IServiceCollection collection, Boolean condition) where TService : class where T : class, TService
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            return condition ? collection.AddSingleton<TService, T>() : collection;
        }

        public static IServiceCollection AddSingletonIfNot<TService, T>(this IServiceCollection collection, Boolean condition) where TService : class where T : class, TService
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            return !condition ? collection.AddSingleton<TService, T>() : collection;
        }

        public static IServiceCollection AddSingletonIf<TService, T>(this IServiceCollection collection, Func<IServiceProvider, T> factory, Boolean condition) where TService : class where T : class, TService
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return condition ? collection.AddSingleton<TService, T>(factory) : collection;
        }

        public static IServiceCollection AddSingletonIfNot<TService, T>(this IServiceCollection collection, Func<IServiceProvider, T> factory, Boolean condition) where TService : class where T : class, TService
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return !condition ? collection.AddSingleton<TService, T>(factory) : collection;
        }
    }
}