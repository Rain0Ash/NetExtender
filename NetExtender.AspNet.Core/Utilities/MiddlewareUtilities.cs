// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using NetExtender.AspNet.Core.Middlewares;

namespace NetExtender.Utilities.AspNetCore.Types
{
    public static class MiddlewareUtilities
    {
        private const DynamicallyAccessedMemberTypes MiddlewareAccessibility = DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.PublicMethods;
        
        public static Func<HttpContext, Task>? GetMiddlewareInvokeAsync<[DynamicallyAccessedMembers(MiddlewareAccessibility)]TMiddleware>(TMiddleware middleware) where TMiddleware : class
        {
            if (middleware is null)
            {
                throw new ArgumentNullException(nameof(middleware));
            }

            MethodInfo? method = typeof(TMiddleware).GetMethod("InvokeAsync", new[] {typeof(HttpContext)});
            return method is not null ? context => (Task) method.Invoke(middleware, new Object[]{ context })! : null;
        }

        public static Func<HttpContext, Task> RequireMiddlewareInvokeAsync<[DynamicallyAccessedMembers(MiddlewareAccessibility)]TMiddleware>(TMiddleware middleware) where TMiddleware : class
        {
            if (middleware is null)
            {
                throw new ArgumentNullException(nameof(middleware));
            }

            Func<HttpContext, Task>? invoke = GetMiddlewareInvokeAsync(middleware);

            if (invoke is null)
            {
                throw new MissingMethodException(typeof(TMiddleware).Name, "InvokeAsync");
            }

            return invoke;
        }

        public static IApplicationBuilder UseAccessRestrictionMiddleware(this IApplicationBuilder builder, Func<HttpContext, Boolean> access)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (access is null)
            {
                throw new ArgumentNullException(nameof(access));
            }

            return builder.UseMiddleware<AccessRestrictionMiddleware>(access);
        }

        public static IApplicationBuilder UseAccessRestrictionMiddleware(this IApplicationBuilder builder, Func<HttpContext, Boolean> access, HttpStatusCode reject)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (access is null)
            {
                throw new ArgumentNullException(nameof(access));
            }

            return builder.UseMiddleware<AccessRestrictionMiddleware>(access, reject);
        }
        
        public static IApplicationBuilder UseAccessRestrictionMiddleware(this IApplicationBuilder builder, Func<HttpContext, Boolean> access, Int32 reject)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (access is null)
            {
                throw new ArgumentNullException(nameof(access));
            }

            return builder.UseMiddleware<AccessRestrictionMiddleware>(access, reject);
        }
        
        public static IApplicationBuilder UseAccessRestrictionMiddleware(this IApplicationBuilder builder, Func<HttpContext, HttpStatusCode> access)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (access is null)
            {
                throw new ArgumentNullException(nameof(access));
            }

            return builder.UseMiddleware<AccessRestrictionMiddleware>(access);
        }
        
        public static IApplicationBuilder UseAccessRestrictionMiddleware(this IApplicationBuilder builder, Func<HttpContext, Int32> access)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (access is null)
            {
                throw new ArgumentNullException(nameof(access));
            }

            return builder.UseMiddleware<AccessRestrictionMiddleware>(access);
        }

        public static IApplicationBuilder UseExternalAccessRestrictionMiddleware(this IApplicationBuilder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.UseMiddleware<ExternalAccessRestrictionMiddleware>();
        }
        
        public static IApplicationBuilder UseExternalAccessRestrictionMiddleware(this IApplicationBuilder builder, HttpStatusCode code)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.UseMiddleware<ExternalAccessRestrictionMiddleware>(code);
        }
        
        public static IApplicationBuilder UseExternalAccessRestrictionMiddleware(this IApplicationBuilder builder, Int32 code)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.UseMiddleware<ExternalAccessRestrictionMiddleware>(code);
        }

        /// <inheritdoc cref="UseMiddlewareExtensions.UseMiddleware{TMiddleware}"/>
        public static IApplicationBuilder UseMiddlewareWhen<[DynamicallyAccessedMembers(MiddlewareAccessibility)]TMiddleware>(this IApplicationBuilder builder, Func<HttpContext, Boolean> predicate, params Object[]? args) where TMiddleware : class
        {
            return UseMiddlewareWhen(builder, predicate, typeof(TMiddleware), args);
        }

        /// <inheritdoc cref="UseMiddlewareExtensions.UseMiddleware"/>
        public static IApplicationBuilder UseMiddlewareWhen(this IApplicationBuilder builder, Func<HttpContext, Boolean> predicate, Type middleware, params Object[]? args)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            if (middleware is null)
            {
                throw new ArgumentNullException(nameof(middleware));
            }

            void Configuration(IApplicationBuilder app)
            {
                app.UseMiddleware(middleware, args ?? Array.Empty<Object>());
            }
            
            return builder.UseWhen(predicate, Configuration);
        }
    }
}