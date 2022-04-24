// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NetExtender.AspNetCore.Types.DependencyInjection.Interfaces;
using NetExtender.AspNetCore.Types.Identities;
using NetExtender.Utilities.Types;

namespace NetExtender.Utilities.AspNetCore.Types
{
    public static partial class ServiceCollectionUtilities
    {
        private static TypeInfo TransientType { get; } = typeof(ITransient).GetTypeInfo();
        private static TypeInfo ScopedType { get; } = typeof(IScoped).GetTypeInfo();
        private static TypeInfo SingletonType { get; } = typeof(ISingleton).GetTypeInfo();

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

        public static IServiceCollection RegisterType(this IServiceCollection collection, Type type)
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

        public static IServiceCollection RegisterType(this IServiceCollection collection, params Type?[]? types)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            return types is not null ? types.WhereNotNull().Aggregate(collection, (current, type) => current.RegisterType(type)) : collection;
        }
        
        public static IServiceCollection RegisterAssembly(this IServiceCollection collection, Assembly assembly)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            return assembly.DefinedTypes.Aggregate(collection, (current, type) => current.RegisterType(type));
        }

        public static IServiceCollection RegisterAssembly(this IServiceCollection collection, params Assembly?[]? assemblies)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            return assemblies is not null ? assemblies.WhereNotNull().Aggregate(collection, (current, assembly) => current.RegisterAssembly(assembly)) : collection;
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
                _ => throw new NotSupportedException()
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
                _ => throw new NotSupportedException()
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
                _ => throw new NotSupportedException()
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
        
        public static IServiceCollection AddIf(this IServiceCollection collection, Func<IServiceCollection, IServiceCollection> @if, Func<IServiceCollection, IServiceCollection> @else , Boolean condition)
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
        
        public static IServiceCollection AddIfNot(this IServiceCollection collection, Func<IServiceCollection, IServiceCollection> @if, Func<IServiceCollection, IServiceCollection> @else , Boolean condition)
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
        
        public static IServiceCollection AddGenericLogging(this IServiceCollection collection)
        {
            return AddGenericLogging<Object?>(collection);
        }

        public static IServiceCollection AddGenericLogging<T>(this IServiceCollection collection)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }
            
            return collection.AddSingleton<ILogger>(provider => provider.GetRequiredService<ILogger<T>>());
        }
        
        public static IServiceCollection AddContextAccessor(this IServiceCollection collection)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (collection.All(service => service.ServiceType != typeof(IHttpContextAccessor)))
            {
                collection = collection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            }

            if (collection.All(service => service.ServiceType != typeof(IActionContextAccessor)))
            {
                collection = collection.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            }

            return collection;
        }
        
        public static IServiceCollection AddHttpContextAccessor(this IServiceCollection collection)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            return collection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }
        
        public static IServiceCollection AddDefaultUser<TUser, TKey>(this IServiceCollection collection) where TUser : IdentityUser<TKey> where TKey : IEquatable<TKey>
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            return collection.AddScoped<DefaultIdentityUser<TUser, TKey>>();
        }

        public static void AddApiVersioning<T>(this IServiceCollection services) where T : IApiVersionReader
        {
            AddApiVersioning<T>(services, null);
        }

        public static void AddApiVersioning<T>(this IServiceCollection services, String? version) where T : IApiVersionReader
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = ApiVersion.TryParse(version, out ApiVersion? api) && api is not null ? api : ApiVersion.Default;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.ApiVersionReader = Activator.CreateInstance<T>();
            });
        }
    }
}