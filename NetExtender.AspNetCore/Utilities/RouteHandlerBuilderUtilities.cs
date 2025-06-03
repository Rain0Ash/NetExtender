// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Net.Http;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using NetExtender.Utilities.Network;

namespace NetExtender.Utilities.AspNetCore.Types
{
    public record RouteHandler
    {
        private readonly String? _route;
        public String Endpoint
        {
            get
            {
                return _route ?? Handler?.Method.Name ?? throw new InvalidOperationException();
            }
            init
            {
                _route = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        private readonly HttpMethod _method;
        public HttpMethod Method
        {
            get
            {
                return _method;
            }
            init
            {
                _method = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        public Delegate? Handler { get; init; }

        public RouteHandler(HttpMethod method, [CallerMemberName] String? route = null)
        {
            _route = route;
            _method = method ?? throw new ArgumentNullException(nameof(method));
        }

        public RouteHandler(String? route, HttpMethod method)
        {
            _route = route;
            _method = method ?? throw new ArgumentNullException(nameof(method));
        }
    }
    
    public static class RouteHandlerBuilderUtilities
    {
#if !NET7_0_OR_GREATER
        private static String[] PatchVerb { get; } =
        {
            HttpMethod.Patch.Method
        };
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEndpointConventionBuilder MapPatch(this IEndpointRouteBuilder endpoints, String pattern, Delegate @delegate)
        {
            if (endpoints is null)
            {
                throw new ArgumentNullException(nameof(endpoints));
            }

            if (pattern is null)
            {
                throw new ArgumentNullException(nameof(pattern));
            }

            if (@delegate is null)
            {
                throw new ArgumentNullException(nameof(@delegate));
            }

            return endpoints.MapMethods(pattern, PatchVerb, @delegate);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEndpointConventionBuilder MapPatch(this IEndpointRouteBuilder endpoints, String pattern, RequestDelegate @delegate)
        {
            if (endpoints is null)
            {
                throw new ArgumentNullException(nameof(endpoints));
            }

            if (pattern is null)
            {
                throw new ArgumentNullException(nameof(pattern));
            }

            if (@delegate is null)
            {
                throw new ArgumentNullException(nameof(@delegate));
            }

            return endpoints.MapMethods(pattern, PatchVerb, @delegate);
        }
#endif
        
        public static IEndpointConventionBuilder? Map(this IEndpointRouteBuilder builder, RouteHandler handler)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (handler is null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            return handler.Handler switch
            {
                null => null,
                RequestDelegate @delegate => Map(builder, handler.Endpoint, handler.Method, @delegate),
                { } @delegate => Map(builder, handler.Endpoint, handler.Method, @delegate)
            };
        }
        
#if NET7_0_OR_GREATER
        public static IEndpointConventionBuilder Map(this IEndpointRouteBuilder builder, [System.Diagnostics.CodeAnalysis.StringSyntax("Route")] String pattern, HttpMethod method, Delegate @delegate)
#else
        public static IEndpointConventionBuilder Map(this IEndpointRouteBuilder builder, String pattern, HttpMethod method, Delegate @delegate)
#endif
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (pattern is null)
            {
                throw new ArgumentNullException(nameof(pattern));
            }

            if (method is null)
            {
                throw new ArgumentNullException(nameof(method));
            }

            if (@delegate is null)
            {
                throw new ArgumentNullException(nameof(@delegate));
            }

            return method.Method switch
            {
                HttpMethodUtilities.Methods.Get => builder.MapGet(pattern, @delegate),
                HttpMethodUtilities.Methods.Post => builder.MapPost(pattern, @delegate),
                HttpMethodUtilities.Methods.Put => builder.MapPut(pattern, @delegate),
                HttpMethodUtilities.Methods.Patch => builder.MapPatch(pattern, @delegate),
                HttpMethodUtilities.Methods.Delete => builder.MapDelete(pattern, @delegate),
                _ => throw new NotSupportedException($"Http method '{method.Method}' is not support mapping.")
            };
        }
        
#if NET7_0_OR_GREATER
        public static IEndpointConventionBuilder Map(this IEndpointRouteBuilder builder, [System.Diagnostics.CodeAnalysis.StringSyntax("Route")] String pattern, HttpMethod method, RequestDelegate @delegate)
#else
        public static IEndpointConventionBuilder Map(this IEndpointRouteBuilder builder, String pattern, HttpMethod method, RequestDelegate @delegate)
#endif
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (pattern is null)
            {
                throw new ArgumentNullException(nameof(pattern));
            }

            if (method is null)
            {
                throw new ArgumentNullException(nameof(method));
            }

            if (@delegate is null)
            {
                throw new ArgumentNullException(nameof(@delegate));
            }

            return method.Method switch
            {
                HttpMethodUtilities.Methods.Get => builder.MapGet(pattern, @delegate),
                HttpMethodUtilities.Methods.Post => builder.MapPost(pattern, @delegate),
                HttpMethodUtilities.Methods.Put => builder.MapPut(pattern, @delegate),
                HttpMethodUtilities.Methods.Patch => builder.MapPatch(pattern, @delegate),
                HttpMethodUtilities.Methods.Delete => builder.MapDelete(pattern, @delegate),
                _ => throw new NotSupportedException($"Http method '{method.Method}' is not support mapping.")
            };
        }
    }
}