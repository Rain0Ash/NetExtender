// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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

        public static IServiceCollection Register(this IServiceCollection services, Type type)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            return type.GetServiceType(out ServiceLifetime lifetime) ? services.Add(type, lifetime) : services;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection Register(this IServiceCollection services, params Type?[]? types)
        {
            return Register(services, (IEnumerable<Type?>?) types);
        }
        
        public static IServiceCollection Register(this IServiceCollection services, IEnumerable<Type?>? types)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            
            return types is not null ? types.WhereNotNull().Aggregate(services, (current, type) => current.Register(type)) : services;
        }
        
        public static IServiceCollection Register(this IServiceCollection services, Assembly assembly)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            return assembly.DefinedTypes.Aggregate(services, (current, type) => current.Register(type));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection Register(this IServiceCollection services, params Assembly?[]? assemblies)
        {
            return Register(services, (IEnumerable<Assembly?>?) assemblies);
        }
        
        public static IServiceCollection Register(this IServiceCollection services, IEnumerable<Assembly?>? assemblies)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            return assemblies is not null ? assemblies.WhereNotNull().Aggregate(services, (current, assembly) => current.Register(assembly)) : services;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ServiceLifetime Find<TService>(this IServiceCollection services, ServiceLifetime alternate)
        {
            return Find(services, typeof(TService), alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ServiceLifetime Find(this IServiceCollection services, Type service, ServiceLifetime alternate)
        {
            return Find(services, service, out ServiceLifetime lifetime) ? lifetime : alternate;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Find<TService>(this IServiceCollection services, out ServiceLifetime result)
        {
            return Find(services, typeof(TService), out result);
        }

        public static Boolean Find(this IServiceCollection services, Type service, out ServiceLifetime result)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (service is null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            if (Find(services, service, out ServiceDescriptor? descriptor))
            {
                result = descriptor.Lifetime;
                return true;
            }
            
            result = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Find<TService>(this IServiceCollection services, [MaybeNullWhen(false)] out ServiceDescriptor result)
        {
            return Find(services, typeof(TService), out result);
        }

        public static Boolean Find(this IServiceCollection services, Type service, [MaybeNullWhen(false)] out ServiceDescriptor result)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (service is null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            for (Int32 index = services.Count - 1; index >= 0; index--)
            {
                ServiceDescriptor descriptor = services[index];
                if (descriptor.ServiceType != service)
                {
                    continue;
                }

                result = descriptor;
                return true;
            }

            result = default;
            return false;
        }

        public static IServiceCollection Add(this IServiceCollection services, Type service, ServiceLifetime lifetime)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (service is null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            return lifetime switch
            {
                ServiceLifetime.Transient => services.AddTransient(service),
                ServiceLifetime.Scoped => services.AddScoped(service),
                ServiceLifetime.Singleton => services.AddSingleton(service),
                _ => throw new EnumUndefinedOrNotSupportedException<ServiceLifetime>(lifetime, nameof(lifetime), null)
            };
        }

        public static IServiceCollection Add(this IServiceCollection services, Type service, Type implementation, ServiceLifetime lifetime)
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

            return lifetime switch
            {
                ServiceLifetime.Transient => services.AddTransient(service, implementation),
                ServiceLifetime.Scoped => services.AddScoped(service, implementation),
                ServiceLifetime.Singleton => services.AddSingleton(service, implementation),
                _ => throw new EnumUndefinedOrNotSupportedException<ServiceLifetime>(lifetime, nameof(lifetime), null)
            };
        }

        public static IServiceCollection Add(this IServiceCollection services, Type service, Func<IServiceProvider, Object> factory, ServiceLifetime lifetime)
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

            return lifetime switch
            {
                ServiceLifetime.Transient => services.AddTransient(service, factory),
                ServiceLifetime.Scoped => services.AddScoped(service, factory),
                ServiceLifetime.Singleton => services.AddSingleton(service, factory),
                _ => throw new EnumUndefinedOrNotSupportedException<ServiceLifetime>(lifetime, nameof(lifetime), null)
            };
        }

        public static IServiceCollection Add<TService>(this IServiceCollection services, ServiceLifetime lifetime) where TService : class
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            return lifetime switch
            {
                ServiceLifetime.Transient => services.AddTransient<TService>(),
                ServiceLifetime.Scoped => services.AddScoped<TService>(),
                ServiceLifetime.Singleton => services.AddSingleton<TService>(),
                _ => throw new EnumUndefinedOrNotSupportedException<ServiceLifetime>(lifetime, nameof(lifetime), null)
            };
        }

        public static IServiceCollection Add<TService>(this IServiceCollection services, Func<IServiceProvider, TService> factory, ServiceLifetime lifetime) where TService : class
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return lifetime switch
            {
                ServiceLifetime.Transient => services.AddTransient(factory),
                ServiceLifetime.Scoped => services.AddScoped(factory),
                ServiceLifetime.Singleton => services.AddSingleton(factory),
                _ => throw new EnumUndefinedOrNotSupportedException<ServiceLifetime>(lifetime, nameof(lifetime), null)
            };
        }

        public static IServiceCollection Add<TService, TImplementation>(this IServiceCollection services, ServiceLifetime lifetime) where TService : class where TImplementation : class, TService
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            return lifetime switch
            {
                ServiceLifetime.Transient => services.AddTransient<TService, TImplementation>(),
                ServiceLifetime.Scoped => services.AddScoped<TService, TImplementation>(),
                ServiceLifetime.Singleton => services.AddSingleton<TService, TImplementation>(),
                _ => throw new EnumUndefinedOrNotSupportedException<ServiceLifetime>(lifetime, nameof(lifetime), null)
            };
        }

        public static IServiceCollection Add<TService, TImplementation>(this IServiceCollection services, Func<IServiceProvider, TImplementation> factory, ServiceLifetime lifetime) where TService : class where TImplementation : class, TService
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return lifetime switch
            {
                ServiceLifetime.Transient => services.AddTransient<TService, TImplementation>(factory),
                ServiceLifetime.Scoped => services.AddScoped<TService, TImplementation>(factory),
                ServiceLifetime.Singleton => services.AddSingleton<TService, TImplementation>(factory),
                _ => throw new EnumUndefinedOrNotSupportedException<ServiceLifetime>(lifetime, nameof(lifetime), null)
            };
        }

        public static IServiceCollection Add(this IServiceCollection services, IDependencyService service)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (service is null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            return services.Add(service.GetType(), service.Lifetime);
        }

        public static IServiceCollection AddIf(this IServiceCollection services, Func<IServiceCollection, IServiceCollection> modifier, Boolean condition)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (modifier is null)
            {
                throw new ArgumentNullException(nameof(modifier));
            }

            return condition ? modifier(services) : services;
        }

        public static IServiceCollection AddIf(this IServiceCollection services, Func<IServiceCollection, IServiceCollection> @if, Func<IServiceCollection, IServiceCollection> @else, Boolean condition)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (@if is null)
            {
                throw new ArgumentNullException(nameof(@if));
            }

            if (@else is null)
            {
                throw new ArgumentNullException(nameof(@else));
            }

            return condition ? @if(services) : @else(services);
        }

        public static IServiceCollection AddIfNot(this IServiceCollection services, Func<IServiceCollection, IServiceCollection> modifier, Boolean condition)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (modifier is null)
            {
                throw new ArgumentNullException(nameof(modifier));
            }

            return !condition ? modifier(services) : services;
        }

        public static IServiceCollection AddIfNot(this IServiceCollection services, Func<IServiceCollection, IServiceCollection> @if, Func<IServiceCollection, IServiceCollection> @else, Boolean condition)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (@if is null)
            {
                throw new ArgumentNullException(nameof(@if));
            }

            if (@else is null)
            {
                throw new ArgumentNullException(nameof(@else));
            }

            return !condition ? @if(services) : @else(services);
        }

        public static IServiceCollection AddLogging(this IServiceCollection services)
        {
            return AddLogging<Object?>(services);
        }

        public static IServiceCollection AddLogging<T>(this IServiceCollection services)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            return services.AddSingleton<ILogger>(provider => provider.GetRequiredService<ILogger<T>>());
        }
    }
}