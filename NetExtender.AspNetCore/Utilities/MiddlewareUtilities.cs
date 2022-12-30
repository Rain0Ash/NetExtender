// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using NetExtender.AspNetCore.Types.Middlewares;

namespace NetExtender.Utilities.AspNetCore.Types
{
    public static class MiddlewareUtilities
    {
        private const DynamicallyAccessedMemberTypes MiddlewareAccessibility = DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.PublicMethods;
        
        public static IApplicationBuilder UseMiddlewareIf(this IApplicationBuilder builder, Type middleware, Boolean condition, params Object?[] args)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (middleware is null)
            {
                throw new ArgumentNullException(nameof(middleware));
            }

            return condition ? builder.UseMiddleware(middleware, args) : builder;
        }

        public static IApplicationBuilder UseMiddlewareIf(this IApplicationBuilder builder, Type middleware, Func<Boolean> condition, params Object?[] args)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (middleware is null)
            {
                throw new ArgumentNullException(nameof(middleware));
            }

            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            return condition() ? builder.UseMiddleware(middleware, args) : builder;
        }

        public static IApplicationBuilder UseMiddlewareIf(this IApplicationBuilder builder, Type middleware, Func<IApplicationBuilder, Boolean> condition, params Object?[] args)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (middleware is null)
            {
                throw new ArgumentNullException(nameof(middleware));
            }

            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            return condition(builder) ? builder.UseMiddleware(middleware, args) : builder;
        }

        public static IApplicationBuilder UseMiddlewareIfNot(this IApplicationBuilder builder, Type middleware, Boolean condition, params Object?[] args)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (middleware is null)
            {
                throw new ArgumentNullException(nameof(middleware));
            }

            return !condition ? builder.UseMiddleware(middleware, args) : builder;
        }

        public static IApplicationBuilder UseMiddlewareIfNot(this IApplicationBuilder builder, Type middleware, Func<Boolean> condition, params Object?[] args)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (middleware is null)
            {
                throw new ArgumentNullException(nameof(middleware));
            }

            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            return !condition() ? builder.UseMiddleware(middleware, args) : builder;
        }

        public static IApplicationBuilder UseMiddlewareIfNot(this IApplicationBuilder builder, Type middleware, Func<IApplicationBuilder, Boolean> condition, params Object?[] args)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (middleware is null)
            {
                throw new ArgumentNullException(nameof(middleware));
            }

            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            return !condition(builder) ? builder.UseMiddleware(middleware, args) : builder;
        }
        
        public static IApplicationBuilder UseMiddlewareIf<[DynamicallyAccessedMembers(MiddlewareAccessibility)] TMiddleware>(this IApplicationBuilder builder, Boolean condition, params Object?[] args) where TMiddleware : class
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return condition ? builder.UseMiddleware<TMiddleware>(args) : builder;
        }

        public static IApplicationBuilder UseMiddlewareIf<[DynamicallyAccessedMembers(MiddlewareAccessibility)] TMiddleware>(this IApplicationBuilder builder, Func<Boolean> condition, params Object?[] args) where TMiddleware : class
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            return condition() ? builder.UseMiddleware<TMiddleware>(args) : builder;
        }

        public static IApplicationBuilder UseMiddlewareIf<[DynamicallyAccessedMembers(MiddlewareAccessibility)] TMiddleware>(this IApplicationBuilder builder, Func<IApplicationBuilder, Boolean> condition, params Object?[] args) where TMiddleware : class
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            return condition(builder) ? builder.UseMiddleware<TMiddleware>(args) : builder;
        }

        public static IApplicationBuilder UseMiddlewareIfNot<[DynamicallyAccessedMembers(MiddlewareAccessibility)] TMiddleware>(this IApplicationBuilder builder, Boolean condition, params Object?[] args) where TMiddleware : class
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return !condition ? builder.UseMiddleware<TMiddleware>(args) : builder;
        }

        public static IApplicationBuilder UseMiddlewareIfNot<[DynamicallyAccessedMembers(MiddlewareAccessibility)] TMiddleware>(this IApplicationBuilder builder, Func<Boolean> condition, params Object?[] args) where TMiddleware : class
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            return !condition() ? builder.UseMiddleware<TMiddleware>(args) : builder;
        }

        public static IApplicationBuilder UseMiddlewareIfNot<[DynamicallyAccessedMembers(MiddlewareAccessibility)] TMiddleware>(this IApplicationBuilder builder, Func<IApplicationBuilder, Boolean> condition, params Object?[] args) where TMiddleware : class
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            return !condition(builder) ? builder.UseMiddleware<TMiddleware>(args) : builder;
        }
        
        public static IApplicationBuilder UseMiddlewareIf(this IApplicationBuilder builder, Boolean condition, Func<IApplicationBuilder, IApplicationBuilder> middleware)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (middleware is null)
            {
                throw new ArgumentNullException(nameof(middleware));
            }

            return condition ? middleware(builder) : builder;
        }

        public static IApplicationBuilder UseMiddlewareIf(this IApplicationBuilder builder, Func<Boolean> condition, Func<IApplicationBuilder, IApplicationBuilder> middleware)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            if (middleware is null)
            {
                throw new ArgumentNullException(nameof(middleware));
            }

            return condition() ? middleware(builder) : builder;
        }

        public static IApplicationBuilder UseMiddlewareIf(this IApplicationBuilder builder, Func<IApplicationBuilder, Boolean> condition, Func<IApplicationBuilder, IApplicationBuilder> middleware)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            if (middleware is null)
            {
                throw new ArgumentNullException(nameof(middleware));
            }

            return condition(builder) ? middleware(builder) : builder;
        }
        
        public static IApplicationBuilder UseMiddlewareIf<T>(this IApplicationBuilder builder, Boolean condition, Func<IApplicationBuilder, T, IApplicationBuilder> middleware, T argument)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (middleware is null)
            {
                throw new ArgumentNullException(nameof(middleware));
            }

            return condition ? middleware(builder, argument) : builder;
        }

        public static IApplicationBuilder UseMiddlewareIf<T>(this IApplicationBuilder builder, Func<Boolean> condition, Func<IApplicationBuilder, T, IApplicationBuilder> middleware, T argument)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            if (middleware is null)
            {
                throw new ArgumentNullException(nameof(middleware));
            }

            return condition() ? middleware(builder, argument) : builder;
        }

        public static IApplicationBuilder UseMiddlewareIf<T>(this IApplicationBuilder builder, Func<IApplicationBuilder, Boolean> condition, Func<IApplicationBuilder, T, IApplicationBuilder> middleware, T argument)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            if (middleware is null)
            {
                throw new ArgumentNullException(nameof(middleware));
            }

            return condition(builder) ? middleware(builder, argument) : builder;
        }
        
        public static IApplicationBuilder UseMiddlewareIf<T1, T2>(this IApplicationBuilder builder, Boolean condition, Func<IApplicationBuilder, T1, T2, IApplicationBuilder> middleware, T1 first, T2 second)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (middleware is null)
            {
                throw new ArgumentNullException(nameof(middleware));
            }

            return condition ? middleware(builder, first, second) : builder;
        }

        public static IApplicationBuilder UseMiddlewareIf<T1, T2>(this IApplicationBuilder builder, Func<Boolean> condition, Func<IApplicationBuilder, T1, T2, IApplicationBuilder> middleware, T1 first, T2 second)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            if (middleware is null)
            {
                throw new ArgumentNullException(nameof(middleware));
            }

            return condition() ? middleware(builder, first, second) : builder;
        }

        public static IApplicationBuilder UseMiddlewareIf<T1, T2>(this IApplicationBuilder builder, Func<IApplicationBuilder, Boolean> condition, Func<IApplicationBuilder, T1, T2, IApplicationBuilder> middleware, T1 first, T2 second)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            if (middleware is null)
            {
                throw new ArgumentNullException(nameof(middleware));
            }

            return condition(builder) ? middleware(builder, first, second) : builder;
        }
        
        public static IApplicationBuilder UseMiddlewareIf<T1, T2, T3>(this IApplicationBuilder builder, Boolean condition, Func<IApplicationBuilder, T1, T2, T3, IApplicationBuilder> middleware, T1 first, T2 second, T3 third)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (middleware is null)
            {
                throw new ArgumentNullException(nameof(middleware));
            }

            return condition ? middleware(builder, first, second, third) : builder;
        }

        public static IApplicationBuilder UseMiddlewareIf<T1, T2, T3>(this IApplicationBuilder builder, Func<Boolean> condition, Func<IApplicationBuilder, T1, T2, T3, IApplicationBuilder> middleware, T1 first, T2 second, T3 third)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            if (middleware is null)
            {
                throw new ArgumentNullException(nameof(middleware));
            }

            return condition() ? middleware(builder, first, second, third) : builder;
        }

        public static IApplicationBuilder UseMiddlewareIf<T1, T2, T3>(this IApplicationBuilder builder, Func<IApplicationBuilder, Boolean> condition, Func<IApplicationBuilder, T1, T2, T3, IApplicationBuilder> middleware, T1 first, T2 second, T3 third)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            if (middleware is null)
            {
                throw new ArgumentNullException(nameof(middleware));
            }

            return condition(builder) ? middleware(builder, first, second, third) : builder;
        }
        
        public static IApplicationBuilder UseMiddlewareIfNot(this IApplicationBuilder builder, Boolean condition, Func<IApplicationBuilder, IApplicationBuilder> middleware)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (middleware is null)
            {
                throw new ArgumentNullException(nameof(middleware));
            }

            return !condition ? middleware(builder) : builder;
        }

        public static IApplicationBuilder UseMiddlewareIfNot(this IApplicationBuilder builder, Func<Boolean> condition, Func<IApplicationBuilder, IApplicationBuilder> middleware)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            if (middleware is null)
            {
                throw new ArgumentNullException(nameof(middleware));
            }

            return !condition() ? middleware(builder) : builder;
        }

        public static IApplicationBuilder UseMiddlewareIfNot(this IApplicationBuilder builder, Func<IApplicationBuilder, Boolean> condition, Func<IApplicationBuilder, IApplicationBuilder> middleware)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            if (middleware is null)
            {
                throw new ArgumentNullException(nameof(middleware));
            }

            return !condition(builder) ? middleware(builder) : builder;
        }
        
        public static IApplicationBuilder UseMiddlewareIfNot<T>(this IApplicationBuilder builder, Boolean condition, Func<IApplicationBuilder, T, IApplicationBuilder> middleware, T argument)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (middleware is null)
            {
                throw new ArgumentNullException(nameof(middleware));
            }

            return !condition ? middleware(builder, argument) : builder;
        }

        public static IApplicationBuilder UseMiddlewareIfNot<T>(this IApplicationBuilder builder, Func<Boolean> condition, Func<IApplicationBuilder, T, IApplicationBuilder> middleware, T argument)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            if (middleware is null)
            {
                throw new ArgumentNullException(nameof(middleware));
            }

            return !condition() ? middleware(builder, argument) : builder;
        }

        public static IApplicationBuilder UseMiddlewareIfNot<T>(this IApplicationBuilder builder, Func<IApplicationBuilder, Boolean> condition, Func<IApplicationBuilder, T, IApplicationBuilder> middleware, T argument)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            if (middleware is null)
            {
                throw new ArgumentNullException(nameof(middleware));
            }

            return !condition(builder) ? middleware(builder, argument) : builder;
        }
        
        public static IApplicationBuilder UseMiddlewareIfNot<T1, T2>(this IApplicationBuilder builder, Boolean condition, Func<IApplicationBuilder, T1, T2, IApplicationBuilder> middleware, T1 first, T2 second)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (middleware is null)
            {
                throw new ArgumentNullException(nameof(middleware));
            }

            return !condition ? middleware(builder, first, second) : builder;
        }

        public static IApplicationBuilder UseMiddlewareIfNot<T1, T2>(this IApplicationBuilder builder, Func<Boolean> condition, Func<IApplicationBuilder, T1, T2, IApplicationBuilder> middleware, T1 first, T2 second)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            if (middleware is null)
            {
                throw new ArgumentNullException(nameof(middleware));
            }

            return !condition() ? middleware(builder, first, second) : builder;
        }

        public static IApplicationBuilder UseMiddlewareIfNot<T1, T2>(this IApplicationBuilder builder, Func<IApplicationBuilder, Boolean> condition, Func<IApplicationBuilder, T1, T2, IApplicationBuilder> middleware, T1 first, T2 second)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            if (middleware is null)
            {
                throw new ArgumentNullException(nameof(middleware));
            }

            return !condition(builder) ? middleware(builder, first, second) : builder;
        }
        
        public static IApplicationBuilder UseMiddlewareIfNot<T1, T2, T3>(this IApplicationBuilder builder, Boolean condition, Func<IApplicationBuilder, T1, T2, T3, IApplicationBuilder> middleware, T1 first, T2 second, T3 third)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (middleware is null)
            {
                throw new ArgumentNullException(nameof(middleware));
            }

            return !condition ? middleware(builder, first, second, third) : builder;
        }

        public static IApplicationBuilder UseMiddlewareIfNot<T1, T2, T3>(this IApplicationBuilder builder, Func<Boolean> condition, Func<IApplicationBuilder, T1, T2, T3, IApplicationBuilder> middleware, T1 first, T2 second, T3 third)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            if (middleware is null)
            {
                throw new ArgumentNullException(nameof(middleware));
            }

            return !condition() ? middleware(builder, first, second, third) : builder;
        }

        public static IApplicationBuilder UseMiddlewareIfNot<T1, T2, T3>(this IApplicationBuilder builder, Func<IApplicationBuilder, Boolean> condition, Func<IApplicationBuilder, T1, T2, T3, IApplicationBuilder> middleware, T1 first, T2 second, T3 third)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            if (middleware is null)
            {
                throw new ArgumentNullException(nameof(middleware));
            }

            return !condition(builder) ? middleware(builder, first, second, third) : builder;
        }

        public static Func<HttpContext, Task>? GetMiddlewareInvokeAsync<[DynamicallyAccessedMembers(MiddlewareAccessibility)] TMiddleware>(TMiddleware middleware) where TMiddleware : class
        {
            if (middleware is null)
            {
                throw new ArgumentNullException(nameof(middleware));
            }

            MethodInfo? method = typeof(TMiddleware).GetMethod("InvokeAsync", new[] {typeof(HttpContext)});
            return method is not null ? context => (Task) method.Invoke(middleware, new Object[]{ context })! : null;
        }

        public static Func<HttpContext, Task> RequireMiddlewareInvokeAsync<[DynamicallyAccessedMembers(MiddlewareAccessibility)] TMiddleware>(TMiddleware middleware) where TMiddleware : class
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

        public static IApplicationBuilder UseUserAgentAccessRestrictionMiddleware(this IApplicationBuilder builder, String? agent)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.UseMiddleware<UserAgentAccessRestrictionMiddleware>(agent);
        }

        public static IApplicationBuilder UseUserAgentAccessRestrictionMiddleware(this IApplicationBuilder builder, String? agent, HttpStatusCode code)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.UseMiddleware<UserAgentAccessRestrictionMiddleware>(agent, code);
        }

        public static IApplicationBuilder UseUserAgentAccessRestrictionMiddleware(this IApplicationBuilder builder, String? agent, Int32 code)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.UseMiddleware<UserAgentAccessRestrictionMiddleware>(agent, code);
        }

        public static IApplicationBuilder UseUserAgentAccessRestrictionMiddleware(this IApplicationBuilder builder, IEnumerable<String?> agents)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (agents is null)
            {
                throw new ArgumentNullException(nameof(agents));
            }

            return builder.UseMiddleware<UserAgentAccessRestrictionMiddleware>(agents);
        }

        public static IApplicationBuilder UseUserAgentAccessRestrictionMiddleware(this IApplicationBuilder builder, IEnumerable<String?> agents, HttpStatusCode code)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (agents is null)
            {
                throw new ArgumentNullException(nameof(agents));
            }

            return builder.UseMiddleware<UserAgentAccessRestrictionMiddleware>(agents, code);
        }

        public static IApplicationBuilder UseUserAgentAccessRestrictionMiddleware(this IApplicationBuilder builder, IEnumerable<String?> agents, Int32 code)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (agents is null)
            {
                throw new ArgumentNullException(nameof(agents));
            }

            return builder.UseMiddleware<UserAgentAccessRestrictionMiddleware>(agents, code);
        }

        public static IApplicationBuilder UseUserAgentExternalAccessRestrictionMiddleware(this IApplicationBuilder builder, String? agent)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.UseExternalAccessRestrictionMiddleware().UseUserAgentAccessRestrictionMiddleware(agent);
        }

        public static IApplicationBuilder UseUserAgentExternalAccessRestrictionMiddleware(this IApplicationBuilder builder, String? agent, HttpStatusCode code)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.UseExternalAccessRestrictionMiddleware(code).UseUserAgentAccessRestrictionMiddleware(agent, code);
        }

        public static IApplicationBuilder UseUserAgentExternalAccessRestrictionMiddleware(this IApplicationBuilder builder, String? agent, Int32 code)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.UseExternalAccessRestrictionMiddleware(code).UseUserAgentAccessRestrictionMiddleware(agent, code);
        }

        public static IApplicationBuilder UseUserAgentExternalAccessRestrictionMiddleware(this IApplicationBuilder builder, IEnumerable<String?> agents)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (agents is null)
            {
                throw new ArgumentNullException(nameof(agents));
            }

            return builder.UseExternalAccessRestrictionMiddleware().UseUserAgentAccessRestrictionMiddleware(agents);
        }

        public static IApplicationBuilder UseUserAgentExternalAccessRestrictionMiddleware(this IApplicationBuilder builder, IEnumerable<String?> agents, HttpStatusCode code)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (agents is null)
            {
                throw new ArgumentNullException(nameof(agents));
            }

            return builder.UseExternalAccessRestrictionMiddleware(code).UseUserAgentAccessRestrictionMiddleware(agents, code);
        }

        public static IApplicationBuilder UseUserAgentExternalAccessRestrictionMiddleware(this IApplicationBuilder builder, IEnumerable<String?> agents, Int32 code)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (agents is null)
            {
                throw new ArgumentNullException(nameof(agents));
            }

            return builder.UseExternalAccessRestrictionMiddleware(code).UseUserAgentAccessRestrictionMiddleware(agents, code);
        }

        /// <inheritdoc cref="UseMiddlewareExtensions.UseMiddleware{TMiddleware}"/>
        public static IApplicationBuilder UseMiddlewareWhen<[DynamicallyAccessedMembers(MiddlewareAccessibility)] TMiddleware>(this IApplicationBuilder builder, Func<HttpContext, Boolean> predicate, params Object[]? args) where TMiddleware : class
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