// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using Microsoft.Extensions.DependencyInjection;

namespace NetExtender.Utilities.Types
{
    public static partial class ServiceCollectionUtilities
    {
        public static IServiceCollection AddTransientIf(this IServiceCollection services, Type service, Boolean condition)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (service is null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            return condition ? services.AddSingleton(service) : services;
        }

        public static IServiceCollection AddTransientIfNot(this IServiceCollection services, Type service, Boolean condition)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (service is null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            return !condition ? services.AddTransient(service) : services;
        }

        public static IServiceCollection AddTransientIf(this IServiceCollection services, Type service, Type implementation, Boolean condition)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (service is null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            if (implementation is null)
            {
                throw new ArgumentNullException(nameof(implementation));
            }

            return condition ? services.AddTransient(service, implementation) : services;
        }

        public static IServiceCollection AddTransientIfNot(this IServiceCollection services, Type service, Type implementation, Boolean condition)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (service is null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            if (implementation is null)
            {
                throw new ArgumentNullException(nameof(implementation));
            }

            return !condition ? services.AddTransient(service, implementation) : services;
        }

        public static IServiceCollection AddTransientIf(this IServiceCollection services, Type service, Func<IServiceProvider, Object> factory, Boolean condition)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (service is null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return condition ? services.AddTransient(service, factory) : services;
        }

        public static IServiceCollection AddTransientIfNot(this IServiceCollection services, Type service, Func<IServiceProvider, Object> factory, Boolean condition)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (service is null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return !condition ? services.AddTransient(service, factory) : services;
        }

        public static IServiceCollection AddTransientIf<TService>(this IServiceCollection services, Boolean condition) where TService : class
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            return condition ? services.AddTransient<TService>() : services;
        }

        public static IServiceCollection AddTransientIfNot<TService>(this IServiceCollection services, Boolean condition) where TService : class
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            return !condition ? services.AddTransient<TService>() : services;
        }

        public static IServiceCollection AddTransientIf<TService>(this IServiceCollection services, Func<IServiceProvider, TService> factory, Boolean condition) where TService : class
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return condition ? services.AddTransient(factory) : services;
        }

        public static IServiceCollection AddTransientIfNot<TService>(this IServiceCollection services, Func<IServiceProvider, TService> factory, Boolean condition) where TService : class
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return !condition ? services.AddTransient(factory) : services;
        }

        public static IServiceCollection AddTransientIf<TService, T>(this IServiceCollection services, Boolean condition) where TService : class where T : class, TService
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            return condition ? services.AddTransient<TService, T>() : services;
        }

        public static IServiceCollection AddTransientIfNot<TService, T>(this IServiceCollection services, Boolean condition) where TService : class where T : class, TService
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            return !condition ? services.AddTransient<TService, T>() : services;
        }

        public static IServiceCollection AddTransientIf<TService, T>(this IServiceCollection services, Func<IServiceProvider, T> factory, Boolean condition) where TService : class where T : class, TService
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return condition ? services.AddTransient<TService, T>(factory) : services;
        }

        public static IServiceCollection AddTransientIfNot<TService, T>(this IServiceCollection services, Func<IServiceProvider, T> factory, Boolean condition) where TService : class where T : class, TService
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return !condition ? services.AddTransient<TService, T>(factory) : services;
        }

        public static IServiceCollection AddScopedIf(this IServiceCollection services, Type service, Boolean condition)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (service is null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            return condition ? services.AddScoped(service) : services;
        }

        public static IServiceCollection AddScopedIfNot(this IServiceCollection services, Type service, Boolean condition)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (service is null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            return !condition ? services.AddScoped(service) : services;
        }

        public static IServiceCollection AddScopedIf(this IServiceCollection services, Type service, Type implementation, Boolean condition)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (service is null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            if (implementation is null)
            {
                throw new ArgumentNullException(nameof(implementation));
            }

            return condition ? services.AddScoped(service, implementation) : services;
        }

        public static IServiceCollection AddScopedIfNot(this IServiceCollection services, Type service, Type implementation, Boolean condition)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (service is null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            if (implementation is null)
            {
                throw new ArgumentNullException(nameof(implementation));
            }

            return !condition ? services.AddScoped(service, implementation) : services;
        }

        public static IServiceCollection AddScopedIf(this IServiceCollection services, Type service, Func<IServiceProvider, Object> factory, Boolean condition)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (service is null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return condition ? services.AddScoped(service, factory) : services;
        }

        public static IServiceCollection AddScopedIfNot(this IServiceCollection services, Type service, Func<IServiceProvider, Object> factory, Boolean condition)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (service is null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return !condition ? services.AddScoped(service, factory) : services;
        }

        public static IServiceCollection AddScopedIf<TService>(this IServiceCollection services, Boolean condition) where TService : class
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            return condition ? services.AddScoped<TService>() : services;
        }

        public static IServiceCollection AddScopedIfNot<TService>(this IServiceCollection services, Boolean condition) where TService : class
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            return !condition ? services.AddScoped<TService>() : services;
        }

        public static IServiceCollection AddScopedIf<TService>(this IServiceCollection services, Func<IServiceProvider, TService> factory, Boolean condition) where TService : class
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return condition ? services.AddScoped(factory) : services;
        }

        public static IServiceCollection AddScopedIfNot<TService>(this IServiceCollection services, Func<IServiceProvider, TService> factory, Boolean condition) where TService : class
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return !condition ? services.AddScoped(factory) : services;
        }

        public static IServiceCollection AddScopedIf<TService, T>(this IServiceCollection services, Boolean condition) where TService : class where T : class, TService
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            return condition ? services.AddScoped<TService, T>() : services;
        }

        public static IServiceCollection AddScopedIfNot<TService, T>(this IServiceCollection services, Boolean condition) where TService : class where T : class, TService
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            return !condition ? services.AddScoped<TService, T>() : services;
        }

        public static IServiceCollection AddScopedIf<TService, T>(this IServiceCollection services, Func<IServiceProvider, T> factory, Boolean condition) where TService : class where T : class, TService
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return condition ? services.AddScoped<TService, T>(factory) : services;
        }

        public static IServiceCollection AddScopedIfNot<TService, T>(this IServiceCollection services, Func<IServiceProvider, T> factory, Boolean condition) where TService : class where T : class, TService
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return !condition ? services.AddScoped<TService, T>(factory) : services;
        }

        public static IServiceCollection AddSingletonIf(this IServiceCollection services, Type service, Boolean condition)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (service is null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            return condition ? services.AddSingleton(service) : services;
        }

        public static IServiceCollection AddSingletonIfNot(this IServiceCollection services, Type service, Boolean condition)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (service is null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            return !condition ? services.AddSingleton(service) : services;
        }

        public static IServiceCollection AddSingletonIf(this IServiceCollection services, Type service, Type implementation, Boolean condition)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (service is null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            if (implementation is null)
            {
                throw new ArgumentNullException(nameof(implementation));
            }

            return condition ? services.AddSingleton(service, implementation) : services;
        }

        public static IServiceCollection AddSingletonIfNot(this IServiceCollection services, Type service, Type implementation, Boolean condition)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (service is null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            if (implementation is null)
            {
                throw new ArgumentNullException(nameof(implementation));
            }

            return !condition ? services.AddSingleton(service, implementation) : services;
        }

        public static IServiceCollection AddSingletonIf(this IServiceCollection services, Type service, Object implementation, Boolean condition)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (service is null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            if (implementation is null)
            {
                throw new ArgumentNullException(nameof(implementation));
            }

            return condition ? services.AddSingleton(service, implementation) : services;
        }

        public static IServiceCollection AddSingletonIfNot(this IServiceCollection services, Type service, Object implementation, Boolean condition)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (service is null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            if (implementation is null)
            {
                throw new ArgumentNullException(nameof(implementation));
            }

            return !condition ? services.AddSingleton(service, implementation) : services;
        }

        public static IServiceCollection AddSingletonIf(this IServiceCollection services, Type service, Func<IServiceProvider, Object> factory, Boolean condition)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (service is null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return condition ? services.AddSingleton(service, factory) : services;
        }

        public static IServiceCollection AddSingletonIfNot(this IServiceCollection services, Type service, Func<IServiceProvider, Object> factory, Boolean condition)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (service is null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return !condition ? services.AddSingleton(service, factory) : services;
        }

        public static IServiceCollection AddSingletonIf<TService>(this IServiceCollection services, Boolean condition) where TService : class
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            return condition ? services.AddSingleton<TService>() : services;
        }

        public static IServiceCollection AddSingletonIfNot<TService>(this IServiceCollection services, Boolean condition) where TService : class
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            return !condition ? services.AddSingleton<TService>() : services;
        }

        public static IServiceCollection AddSingletonIf<TService>(this IServiceCollection services, TService implementation, Boolean condition) where TService : class
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (implementation is null)
            {
                throw new ArgumentNullException(nameof(implementation));
            }

            return condition ? services.AddSingleton(implementation) : services;
        }

        public static IServiceCollection AddSingletonIfNot<TService>(this IServiceCollection services, TService implementation, Boolean condition) where TService : class
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (implementation is null)
            {
                throw new ArgumentNullException(nameof(implementation));
            }

            return !condition ? services.AddSingleton(implementation) : services;
        }

        public static IServiceCollection AddSingletonIf<TService>(this IServiceCollection services, Func<IServiceProvider, TService> factory, Boolean condition) where TService : class
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return condition ? services.AddSingleton(factory) : services;
        }

        public static IServiceCollection AddSingletonIfNot<TService>(this IServiceCollection services, Func<IServiceProvider, TService> factory, Boolean condition) where TService : class
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return !condition ? services.AddSingleton(factory) : services;
        }

        public static IServiceCollection AddSingletonIf<TService, T>(this IServiceCollection services, Boolean condition) where TService : class where T : class, TService
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            return condition ? services.AddSingleton<TService, T>() : services;
        }

        public static IServiceCollection AddSingletonIfNot<TService, T>(this IServiceCollection services, Boolean condition) where TService : class where T : class, TService
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            return !condition ? services.AddSingleton<TService, T>() : services;
        }

        public static IServiceCollection AddSingletonIf<TService, T>(this IServiceCollection services, Func<IServiceProvider, T> factory, Boolean condition) where TService : class where T : class, TService
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return condition ? services.AddSingleton<TService, T>(factory) : services;
        }

        public static IServiceCollection AddSingletonIfNot<TService, T>(this IServiceCollection services, Func<IServiceProvider, T> factory, Boolean condition) where TService : class where T : class, TService
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return !condition ? services.AddSingleton<TService, T>(factory) : services;
        }
    }
}