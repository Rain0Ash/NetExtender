// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;

namespace NetExtender.Utilities.Types
{
    public static partial class ServiceCollectionUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

            return condition ? services.AddTransient(service) : services;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddTransientIf<TService>(this IServiceCollection services, Boolean condition) where TService : class
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            return condition ? services.AddTransient<TService>() : services;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddTransientIfNot<TService>(this IServiceCollection services, Boolean condition) where TService : class
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            return !condition ? services.AddTransient<TService>() : services;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddTransientIf<TService, T>(this IServiceCollection services, Boolean condition) where TService : class where T : class, TService
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            return condition ? services.AddTransient<TService, T>() : services;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddTransientIfNot<TService, T>(this IServiceCollection services, Boolean condition) where TService : class where T : class, TService
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            return !condition ? services.AddTransient<TService, T>() : services;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddKeyedTransientIf(this IServiceCollection services, Type service, Object? key, Boolean condition)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (service is null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            return condition ? services.AddKeyedTransient(service, key) : services;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddKeyedTransientIfNot(this IServiceCollection services, Type service, Object? key, Boolean condition)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (service is null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            return !condition ? services.AddKeyedTransient(service, key) : services;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddKeyedTransientIf(this IServiceCollection services, Type service, Object? key, Type implementation, Boolean condition)
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

            return condition ? services.AddKeyedTransient(service, key, implementation) : services;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddKeyedTransientIfNot(this IServiceCollection services, Type service, Object? key, Type implementation, Boolean condition)
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

            return !condition ? services.AddKeyedTransient(service, key, implementation) : services;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddKeyedTransientIf(this IServiceCollection services, Type service, Object? key, Func<IServiceProvider, Object?, Object> factory, Boolean condition)
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

            return condition ? services.AddKeyedTransient(service, key, factory) : services;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddKeyedTransientIfNot(this IServiceCollection services, Type service, Object? key, Func<IServiceProvider, Object?, Object> factory, Boolean condition)
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

            return !condition ? services.AddKeyedTransient(service, key, factory) : services;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddKeyedTransientIf<TService>(this IServiceCollection services, Object? key, Boolean condition) where TService : class
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            return condition ? services.AddKeyedTransient<TService>(key) : services;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddKeyedTransientIfNot<TService>(this IServiceCollection services, Object? key, Boolean condition) where TService : class
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            return !condition ? services.AddKeyedTransient<TService>(key) : services;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddKeyedTransientIf<TService>(this IServiceCollection services, Object? key, Func<IServiceProvider, Object?, TService> factory, Boolean condition) where TService : class
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return condition ? services.AddKeyedTransient(key, factory) : services;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddKeyedTransientIfNot<TService>(this IServiceCollection services, Object? key, Func<IServiceProvider, Object?, TService> factory, Boolean condition) where TService : class
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return !condition ? services.AddKeyedTransient(key, factory) : services;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddKeyedTransientIf<TService, T>(this IServiceCollection services, Object? key, Boolean condition) where TService : class where T : class, TService
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            return condition ? services.AddKeyedTransient<TService, T>(key) : services;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddKeyedTransientIfNot<TService, T>(this IServiceCollection services, Object? key, Boolean condition) where TService : class where T : class, TService
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            return !condition ? services.AddKeyedTransient<TService, T>(key) : services;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddKeyedTransientIf<TService, T>(this IServiceCollection services, Object? key, Func<IServiceProvider, Object?, T> factory, Boolean condition) where TService : class where T : class, TService
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return condition ? services.AddKeyedTransient<TService, T>(key, factory) : services;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddKeyedTransientIfNot<TService, T>(this IServiceCollection services, Object? key, Func<IServiceProvider, Object?, T> factory, Boolean condition) where TService : class where T : class, TService
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return !condition ? services.AddKeyedTransient<TService, T>(key, factory) : services;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddScopedIf<TService>(this IServiceCollection services, Boolean condition) where TService : class
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            return condition ? services.AddScoped<TService>() : services;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddScopedIfNot<TService>(this IServiceCollection services, Boolean condition) where TService : class
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            return !condition ? services.AddScoped<TService>() : services;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddScopedIf<TService, T>(this IServiceCollection services, Boolean condition) where TService : class where T : class, TService
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            return condition ? services.AddScoped<TService, T>() : services;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddScopedIfNot<TService, T>(this IServiceCollection services, Boolean condition) where TService : class where T : class, TService
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            return !condition ? services.AddScoped<TService, T>() : services;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddKeyedScopedIf(this IServiceCollection services, Type service, Object? key, Boolean condition)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (service is null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            return condition ? services.AddKeyedScoped(service, key) : services;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddKeyedScopedIfNot(this IServiceCollection services, Type service, Object? key, Boolean condition)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (service is null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            return !condition ? services.AddKeyedScoped(service, key) : services;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddKeyedScopedIf(this IServiceCollection services, Type service, Object? key, Type implementation, Boolean condition)
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

            return condition ? services.AddKeyedScoped(service, key, implementation) : services;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddKeyedScopedIfNot(this IServiceCollection services, Type service, Object? key, Type implementation, Boolean condition)
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

            return !condition ? services.AddKeyedScoped(service, key, implementation) : services;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddKeyedScopedIf(this IServiceCollection services, Type service, Object? key, Func<IServiceProvider, Object?, Object> factory, Boolean condition)
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

            return condition ? services.AddKeyedScoped(service, key, factory) : services;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddKeyedScopedIfNot(this IServiceCollection services, Type service, Object? key, Func<IServiceProvider, Object?, Object> factory, Boolean condition)
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

            return !condition ? services.AddKeyedScoped(service, key, factory) : services;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddKeyedScopedIf<TService>(this IServiceCollection services, Object? key, Boolean condition) where TService : class
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            return condition ? services.AddKeyedScoped<TService>(key) : services;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddKeyedScopedIfNot<TService>(this IServiceCollection services, Object? key, Boolean condition) where TService : class
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            return !condition ? services.AddKeyedScoped<TService>(key) : services;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddKeyedScopedIf<TService>(this IServiceCollection services, Object? key, Func<IServiceProvider, Object?, TService> factory, Boolean condition) where TService : class
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return condition ? services.AddKeyedScoped(key, factory) : services;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddKeyedScopedIfNot<TService>(this IServiceCollection services, Object? key, Func<IServiceProvider, Object?, TService> factory, Boolean condition) where TService : class
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return !condition ? services.AddKeyedScoped(key, factory) : services;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddKeyedScopedIf<TService, T>(this IServiceCollection services, Object? key, Boolean condition) where TService : class where T : class, TService
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            return condition ? services.AddKeyedScoped<TService, T>(key) : services;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddKeyedScopedIfNot<TService, T>(this IServiceCollection services, Object? key, Boolean condition) where TService : class where T : class, TService
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            return !condition ? services.AddKeyedScoped<TService, T>(key) : services;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddKeyedScopedIf<TService, T>(this IServiceCollection services, Object? key, Func<IServiceProvider, Object?, T> factory, Boolean condition) where TService : class where T : class, TService
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return condition ? services.AddKeyedScoped<TService, T>(key, factory) : services;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddKeyedScopedIfNot<TService, T>(this IServiceCollection services, Object? key, Func<IServiceProvider, Object?, T> factory, Boolean condition) where TService : class where T : class, TService
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return !condition ? services.AddKeyedScoped<TService, T>(key, factory) : services;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddSingletonIf<TService>(this IServiceCollection services, Boolean condition) where TService : class
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            return condition ? services.AddSingleton<TService>() : services;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddSingletonIfNot<TService>(this IServiceCollection services, Boolean condition) where TService : class
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            return !condition ? services.AddSingleton<TService>() : services;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddSingletonIf<TService, T>(this IServiceCollection services, Boolean condition) where TService : class where T : class, TService
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            return condition ? services.AddSingleton<TService, T>() : services;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddSingletonIfNot<TService, T>(this IServiceCollection services, Boolean condition) where TService : class where T : class, TService
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            return !condition ? services.AddSingleton<TService, T>() : services;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddKeyedSingletonIf(this IServiceCollection services, Type service, Object? key, Boolean condition)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (service is null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            return condition ? services.AddKeyedSingleton(key, service) : services;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddKeyedSingletonIfNot(this IServiceCollection services, Type service, Object? key, Boolean condition)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (service is null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            return !condition ? services.AddKeyedSingleton(key, service) : services;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddKeyedSingletonIf(this IServiceCollection services, Type service, Object? key, Type implementation, Boolean condition)
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

            return condition ? services.AddKeyedSingleton(service, key, implementation) : services;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddKeyedSingletonIfNot(this IServiceCollection services, Type service, Object? key, Type implementation, Boolean condition)
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

            return !condition ? services.AddKeyedSingleton(service, key, implementation) : services;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddKeyedSingletonIf(this IServiceCollection services, Type service, Object? key, Object implementation, Boolean condition)
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

            return condition ? services.AddKeyedSingleton(service, key, implementation) : services;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddKeyedSingletonIfNot(this IServiceCollection services, Type service, Object? key, Object implementation, Boolean condition)
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

            return !condition ? services.AddKeyedSingleton(service, key, implementation) : services;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddKeyedSingletonIf(this IServiceCollection services, Type service, Object? key, Func<IServiceProvider, Object?, Object> factory, Boolean condition)
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

            return condition ? services.AddKeyedSingleton(service, key, factory) : services;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddKeyedSingletonIfNot(this IServiceCollection services, Type service, Object? key, Func<IServiceProvider, Object?, Object> factory, Boolean condition)
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

            return !condition ? services.AddKeyedSingleton(service, key, factory) : services;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddKeyedSingletonIf<TService>(this IServiceCollection services, Object? key, Boolean condition) where TService : class
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            return condition ? services.AddKeyedSingleton<TService>(key) : services;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddKeyedSingletonIfNot<TService>(this IServiceCollection services, Object? key, Boolean condition) where TService : class
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            return !condition ? services.AddKeyedSingleton<TService>(key) : services;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddKeyedSingletonIf<TService>(this IServiceCollection services, Object? key, TService implementation, Boolean condition) where TService : class
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (implementation is null)
            {
                throw new ArgumentNullException(nameof(implementation));
            }

            return condition ? services.AddKeyedSingleton(key, implementation) : services;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddKeyedSingletonIfNot<TService>(this IServiceCollection services, Object? key, TService implementation, Boolean condition) where TService : class
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (implementation is null)
            {
                throw new ArgumentNullException(nameof(implementation));
            }

            return !condition ? services.AddKeyedSingleton(key, implementation) : services;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddKeyedSingletonIf<TService>(this IServiceCollection services, Object? key, Func<IServiceProvider, Object?, TService> factory, Boolean condition) where TService : class
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return condition ? services.AddKeyedSingleton(key, factory) : services;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddKeyedSingletonIfNot<TService>(this IServiceCollection services, Object? key, Func<IServiceProvider, Object?, TService> factory, Boolean condition) where TService : class
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return !condition ? services.AddKeyedSingleton(key, factory) : services;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddKeyedSingletonIf<TService, T>(this IServiceCollection services, Object? key, Boolean condition) where TService : class where T : class, TService
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            return condition ? services.AddKeyedSingleton<TService, T>(key) : services;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddKeyedSingletonIfNot<TService, T>(this IServiceCollection services, Object? key, Boolean condition) where TService : class where T : class, TService
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            return !condition ? services.AddKeyedSingleton<TService, T>(key) : services;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddKeyedSingletonIf<TService, T>(this IServiceCollection services, Object? key, Func<IServiceProvider, Object?, T> factory, Boolean condition) where TService : class where T : class, TService
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return condition ? services.AddKeyedSingleton<TService, T>(key, factory) : services;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddKeyedSingletonIfNot<TService, T>(this IServiceCollection services, Object? key, Func<IServiceProvider, Object?, T> factory, Boolean condition) where TService : class where T : class, TService
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return !condition ? services.AddKeyedSingleton<TService, T>(key, factory) : services;
        }
    }
}