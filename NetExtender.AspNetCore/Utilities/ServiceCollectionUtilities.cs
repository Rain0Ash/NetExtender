// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;
using NetExtender.AspNetCore.Filters;
using NetExtender.AspNetCore.Identity.Interfaces;
using NetExtender.AspNetCore.Types.Services.Interfaces;

namespace NetExtender.Utilities.AspNetCore.Types
{
    public static class ServiceCollectionUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMvcBuilder InjectControllers(this IServiceCollection services)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            
            return services.AddControllers().AddControllersAsServices();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMvcBuilder AddControllers<TId, TUser, TRole, TFilter>(this IServiceCollection services) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole>, new() where TRole : IEquatable<TRole> where TFilter : IdentityServiceFilter<TId, TUser, TRole>
        {
            return AddControllers<TId, TUser, TRole, TFilter>(services, null);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMvcBuilder AddControllers<TId, TUser, TRole, TFilter>(this IServiceCollection services, Action<MvcOptions>? options) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole>, new() where TRole : IEquatable<TRole> where TFilter : IdentityServiceFilter<TId, TUser, TRole>
        {
            return AddControllers<TId, TUser, TRole, ActionInfoServiceFilter<TId, TUser, TRole>, TFilter>(services, options);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMvcBuilder AddControllers<TId, TUser, TRole, TActionFilter, TIdentityFilter>(this IServiceCollection services) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole>, new() where TRole : IEquatable<TRole> where TIdentityFilter : IdentityServiceFilter<TId, TUser, TRole> where TActionFilter : ActionInfoServiceFilter<TId, TUser, TRole>
        {
            return AddControllers<TId, TUser, TRole, TActionFilter, TIdentityFilter>(services, null);
        }
        
        public static IMvcBuilder AddControllers<TId, TUser, TRole, TActionFilter, TIdentityFilter>(this IServiceCollection services, Action<MvcOptions>? options) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole>, new() where TRole : IEquatable<TRole> where TIdentityFilter : IdentityServiceFilter<TId, TUser, TRole> where TActionFilter : ActionInfoServiceFilter<TId, TUser, TRole>
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services = services.AddScoped<IActionInfoService, TActionFilter>().AddScoped<IIdentityUserService<TId, TUser, TRole>, TIdentityFilter>();
            return services.AddControllers(mvc =>
            {
                mvc.Filters.AddIdentityFilter<TId, TUser, TRole, TActionFilter, TIdentityFilter>();
                options?.Invoke(mvc);
            });
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMvcBuilder AddControllersWithViews<TId, TUser, TRole, TFilter>(this IServiceCollection services) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole>, new() where TRole : IEquatable<TRole> where TFilter : IdentityServiceFilter<TId, TUser, TRole>
        {
            return AddControllersWithViews<TId, TUser, TRole, TFilter>(services, null);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMvcBuilder AddControllersWithViews<TId, TUser, TRole, TFilter>(this IServiceCollection services, Action<MvcOptions>? options) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole>, new() where TRole : IEquatable<TRole> where TFilter : IdentityServiceFilter<TId, TUser, TRole>
        {
            return AddControllersWithViews<TId, TUser, TRole, ActionInfoServiceFilter<TId, TUser, TRole>, TFilter>(services, options);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMvcBuilder AddControllersWithViews<TId, TUser, TRole, TActionFilter, TIdentityFilter>(this IServiceCollection services) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole>, new() where TRole : IEquatable<TRole> where TIdentityFilter : IdentityServiceFilter<TId, TUser, TRole> where TActionFilter : ActionInfoServiceFilter<TId, TUser, TRole>
        {
            return AddControllersWithViews<TId, TUser, TRole, TActionFilter, TIdentityFilter>(services, null);
        }
        
        public static IMvcBuilder AddControllersWithViews<TId, TUser, TRole, TActionFilter, TIdentityFilter>(this IServiceCollection services, Action<MvcOptions>? options) where TId : struct, IEquatable<TId> where TUser : class, IUserInfo<TId, TRole>, new() where TRole : IEquatable<TRole> where TIdentityFilter : IdentityServiceFilter<TId, TUser, TRole> where TActionFilter : ActionInfoServiceFilter<TId, TUser, TRole>
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services = services.AddScoped<IActionInfoService, TActionFilter>().AddScoped<IIdentityUserService<TId, TUser, TRole>, TIdentityFilter>();
            return services.AddControllersWithViews(mvc =>
            {
                mvc.Filters.AddIdentityFilter<TId, TUser, TRole, TActionFilter, TIdentityFilter>();
                options?.Invoke(mvc);
            });
        }
        
        public static IServiceCollection AddContextAccessor(this IServiceCollection services)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (services.All(service => service.ServiceType != typeof(IHttpContextAccessor)))
            {
                services = services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            }

            if (services.All(service => service.ServiceType != typeof(IActionContextAccessor)))
            {
                services = services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            }

            return services;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddHttpContextAccessor(this IServiceCollection services)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            return services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IServiceCollection AddApiVersioning<T>(this IServiceCollection services) where T : IApiVersionReader
        {
            return AddApiVersioning<T>(services, null);
        }

        public static IServiceCollection AddApiVersioning<T>(this IServiceCollection services, String? version) where T : IApiVersionReader
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = ApiVersion.TryParse(version, out ApiVersion? api) ? api : ApiVersion.Default;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.ApiVersionReader = Activator.CreateInstance<T>();
            });
            
            return services;
        }
    }
}