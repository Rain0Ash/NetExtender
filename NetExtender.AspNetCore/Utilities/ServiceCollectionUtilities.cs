// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;
using NetExtender.AspNetCore.Types.Identities;

namespace NetExtender.Utilities.AspNetCore.Types
{
    public static class ServiceCollectionUtilities
    {
        public static IMvcBuilder InjectControllers(this IServiceCollection collection)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }
            
            return collection.AddControllers().AddControllersAsServices();
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

        public static IServiceCollection AddApiVersioning<T>(this IServiceCollection collection) where T : IApiVersionReader
        {
            return AddApiVersioning<T>(collection, null);
        }

        public static IServiceCollection AddApiVersioning<T>(this IServiceCollection collection, String? version) where T : IApiVersionReader
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            collection.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = ApiVersion.TryParse(version, out ApiVersion? api) ? api : ApiVersion.Default;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.ApiVersionReader = Activator.CreateInstance<T>();
            });
            
            return collection;
        }
    }
}