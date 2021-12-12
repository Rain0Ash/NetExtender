// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NetExtender.Utilities.Types;

namespace NetExtender.AspNetCore.Types.Middlewares
{
    public class HttpHeaderFilterMiddleware
    {
        protected RequestDelegate Next { get; }
        protected ISet<String>? Exclude { get; }

        public HttpHeaderFilterMiddleware(RequestDelegate next, IEnumerable<String?>? exclude)
            : this(next, exclude, null)
        {
        }

        public HttpHeaderFilterMiddleware(RequestDelegate next, IEnumerable<String?>? exclude, IEqualityComparer<String>? comparer)
        {
            Next = next ?? throw new ArgumentNullException(nameof(next));
            Exclude = exclude is not null ? new HashSet<String>(exclude.WhereNotNull(), comparer) : null;
        }

        public virtual Task Invoke(HttpContext context)
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
}