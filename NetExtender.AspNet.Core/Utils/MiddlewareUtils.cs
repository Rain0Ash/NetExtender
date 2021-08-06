// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using NetExtender.AspNet.Core.Middlewares;

namespace NetExtender.Utils.AspNetCore.Types
{
    public static class MiddlewareUtils
    {
        public static Func<HttpContext, Task>? GetMiddlewareInvokeAsync<T>(T middleware) where T : class
        {
            if (middleware is null)
            {
                throw new ArgumentNullException(nameof(middleware));
            }

            MethodInfo? method = typeof(T).GetMethod("InvokeAsync", new[] {typeof(HttpContext)});
            return method is not null ? context => (Task) method.Invoke(middleware, new Object[]{ context })! : null;
        }

        public static Func<HttpContext, Task> RequireMiddlewareInvokeAsync<T>(T middleware) where T : class
        {
            if (middleware is null)
            {
                throw new ArgumentNullException(nameof(middleware));
            }

            Func<HttpContext, Task>? invoke = GetMiddlewareInvokeAsync(middleware);

            if (invoke is null)
            {
                throw new MissingMethodException(typeof(T).Name, "InvokeAsync");
            }

            return invoke;
        }

        public static IApplicationBuilder UseLocalhostMiddleware(this IApplicationBuilder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.UseMiddleware<LocalhostMiddleware>();
        }
        
        public static IApplicationBuilder UseLocalhostMiddleware(this IApplicationBuilder builder, Int32 code)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.UseMiddleware<LocalhostMiddleware>(code);
        }
    }
}