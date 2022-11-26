// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NetExtender.Utilities.Types;

namespace NetExtender.AspNetCore.Types.Middlewares
{
    public class HttpHeaderFilterMiddleware : AsyncMiddleware
    {
        protected ISet<String>? Exclude { get; }
        
        public HttpHeaderFilterMiddleware(RequestDelegate next, params String[]? exclude)
            : this(next, (IEnumerable<String>?) exclude)
        {
        }

        public HttpHeaderFilterMiddleware(RequestDelegate next, IEnumerable<String>? exclude)
            : this(next, exclude, null)
        {
        }

        public HttpHeaderFilterMiddleware(RequestDelegate next, IEqualityComparer<String>? comparer, params String[]? exclude)
            : this(next, exclude, comparer)
        {
        }

        public HttpHeaderFilterMiddleware(RequestDelegate next, IEnumerable<String>? exclude, IEqualityComparer<String>? comparer)
            : base(next)
        {
            Exclude = exclude is not null ? new HashSet<String>(exclude.WhereNotNull(), comparer) : null;
        }

        public override Task InvokeAsync(HttpContext context)
        {
            if (Exclude is null)
            {
                return Next.Invoke(context);
            }
            
            foreach (String exclude in Exclude)
            {
                context.Response.Headers.Remove(exclude);
            }
            
            return Next.Invoke(context);
        }
    }
    
    public sealed class NoServerHttpHeaderFilterMiddleware : HttpHeaderFilterMiddleware
    {
        private const String ServerHttpHeaderName = "Server";
        
        public NoServerHttpHeaderFilterMiddleware(RequestDelegate next)
            : base(next)
        {
        }

        public override async Task InvokeAsync(HttpContext context)
        {
            context.Response.Headers.Remove(ServerHttpHeaderName);
            await Next.Invoke(context);
        }
    }
}