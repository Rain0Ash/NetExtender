// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Net.Http;
using System.Reflection;
using System.Runtime.CompilerServices;
using NetExtender.Types.Exceptions;

namespace NetExtender.Utilities.Network
{
    public enum HttpMethodType : Byte
    {
        None,
        Connect,
        Head,
        Trace,
        Options,
        Get,
        Query,
        Post,
        Put,
        Patch,
        Delete
    }
    
    public static class HttpMethodUtilities
    {
        [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
        public static class Methods
        {
            public const String Connect = "CONNECT";
            public const String Head = "HEAD";
            public const String Trace = "TRACE";
            public const String Options = "OPTIONS";
            public const String Get = "GET";
            public const String Query = "QUERY";
            public const String Post = "POST";
            public const String Put = "PUT";
            public const String Patch = "PATCH";
            public const String Delete = "DELETE";
        }
        
        static HttpMethodUtilities()
        {
            connect = GetUndefinedMethod(nameof(Connect)) ?? (static () => throw new InvalidOperationException());
            query = GetUndefinedMethod(nameof(Query)) ?? (static () => throw new InvalidOperationException());
        }

        private static Func<HttpMethod>? GetUndefinedMethod(String name)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            
            const BindingFlags binding = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

            try
            {
                PropertyInfo? property = typeof(HttpMethod).GetProperty(name, binding);
                return property is not null ? Expression.Lambda<Func<HttpMethod>>(Expression.Property(null, property)).Compile() : null;
            }
            catch (Exception)
            {
                return null;
            }
        }
        
        private static readonly Func<HttpMethod> connect;
        public static HttpMethod Connect
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return connect();
            }
        }
        
        public static HttpMethod Head
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return HttpMethod.Head;
            }
        }
        
        public static HttpMethod Trace
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return HttpMethod.Trace;
            }
        }
        
        public static HttpMethod Options
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return HttpMethod.Options;
            }
        }

        public static HttpMethod Get
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return HttpMethod.Get;
            }
        }

        private static readonly Func<HttpMethod> query;
        public static HttpMethod Query
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return query();
            }
        }
        
        public static HttpMethod Post
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return HttpMethod.Post;
            }
        }
        
        public static HttpMethod Put
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return HttpMethod.Put;
            }
        }
        
        public static HttpMethod Patch
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return HttpMethod.Patch;
            }
        }
        
        public static HttpMethod Delete
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return HttpMethod.Delete;
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Boolean TryParse(String? value, out HttpMethodType result)
        {
            result = value switch
            {
                Methods.Connect => HttpMethodType.Connect,
                Methods.Head => HttpMethodType.Head,
                Methods.Trace => HttpMethodType.Trace,
                Methods.Options => HttpMethodType.Options,
                Methods.Get => HttpMethodType.Get,
                Methods.Query => HttpMethodType.Query,
                Methods.Post => HttpMethodType.Post,
                Methods.Put => HttpMethodType.Put,
                Methods.Patch => HttpMethodType.Patch,
                Methods.Delete => HttpMethodType.Delete,
                _ => HttpMethodType.None
            };
            
            return result != HttpMethodType.None;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Boolean TryParse(String? value, [MaybeNullWhen(false)] out HttpMethod result)
        {
            result = value switch
            {
                Methods.Connect => Connect,
                Methods.Head => Head,
                Methods.Trace => Trace,
                Methods.Options => Options,
                Methods.Get => Get,
                Methods.Query => Query,
                Methods.Post => Post,
                Methods.Put => Put,
                Methods.Patch => Patch,
                Methods.Delete => Delete,
                _ => null
            };
            
            return result is not null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static HttpMethod Method(this HttpMethodType method)
        {
            return method switch
            {
                HttpMethodType.None => throw EnumNotSupportedException<HttpMethodType>.Create(method),
                HttpMethodType.Connect => Connect,
                HttpMethodType.Head => Head,
                HttpMethodType.Trace => Trace,
                HttpMethodType.Options => Options,
                HttpMethodType.Get => Get,
                HttpMethodType.Query => Query,
                HttpMethodType.Post => Post,
                HttpMethodType.Put => Put,
                HttpMethodType.Patch => Patch,
                HttpMethodType.Delete => Delete,
                _ => throw new EnumUndefinedOrNotSupportedException<HttpMethodType>(method, nameof(method), null)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static HttpMethodType Method(this HttpMethod method)
        {
            if (method is null)
            {
                throw new ArgumentNullException(nameof(method));
            }

            return TryParse(method.Method, out HttpMethodType result) ? result : throw new ArgumentException($"Method '{method.Method}' not supported.");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Boolean IsSafe(String method)
        {
            if (method is null)
            {
                throw new ArgumentNullException(nameof(method));
            }
            
            return method switch
            {
                Methods.Connect => false,
                Methods.Head => true,
                Methods.Trace => true,
                Methods.Options => true,
                Methods.Get => true,
                Methods.Query => true,
                Methods.Post => false,
                Methods.Put => false,
                Methods.Patch => false,
                Methods.Delete => false,
                _ => throw new ArgumentException($"Invalid HTTP method '{method}'.", nameof(method))
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Boolean IsSafe(this HttpMethodType method)
        {
            return method switch
            {
                HttpMethodType.None => throw EnumNotSupportedException<HttpMethodType>.Create(method),
                HttpMethodType.Connect => false,
                HttpMethodType.Head => true,
                HttpMethodType.Trace => true,
                HttpMethodType.Options => true,
                HttpMethodType.Get => true,
                HttpMethodType.Query => true,
                HttpMethodType.Post => false,
                HttpMethodType.Put => false,
                HttpMethodType.Patch => false,
                HttpMethodType.Delete => false,
                _ => throw new EnumUndefinedOrNotSupportedException<HttpMethodType>(method, nameof(method), null)
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Boolean IsSafe(this HttpMethod method)
        {
            if (method is null)
            {
                throw new ArgumentNullException(nameof(method));
            }
            
            return IsSafe(method.Method);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Boolean IsIdempotent(String method)
        {
            if (method is null)
            {
                throw new ArgumentNullException(nameof(method));
            }
            
            return method switch
            {
                Methods.Connect => false,
                Methods.Head => true,
                Methods.Trace => true,
                Methods.Options => true,
                Methods.Get => true,
                Methods.Query => true,
                Methods.Post => false,
                Methods.Put => true,
                Methods.Patch => false,
                Methods.Delete => true,
                _ => throw new ArgumentException($"Invalid HTTP method '{method}'.", nameof(method))
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Boolean IsIdempotent(this HttpMethodType method)
        {
            return method switch
            {
                HttpMethodType.None => throw EnumNotSupportedException<HttpMethodType>.Create(method),
                HttpMethodType.Connect => false,
                HttpMethodType.Head => true,
                HttpMethodType.Trace => true,
                HttpMethodType.Options => true,
                HttpMethodType.Get => true,
                HttpMethodType.Query => true,
                HttpMethodType.Post => false,
                HttpMethodType.Put => true,
                HttpMethodType.Patch => false,
                HttpMethodType.Delete => true,
                _ => throw new EnumUndefinedOrNotSupportedException<HttpMethodType>(method, nameof(method), null)
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Boolean IsIdempotent(this HttpMethod method)
        {
            if (method is null)
            {
                throw new ArgumentNullException(nameof(method));
            }
            
            return IsIdempotent(method.Method);
        }
    }
}