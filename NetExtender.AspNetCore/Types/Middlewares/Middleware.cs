// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NetExtender.AspNetCore.Types.Middlewares.Interfaces;

namespace NetExtender.AspNetCore.Types.Middlewares
{
    public abstract class Middleware : Interfaces.IMiddleware
    {
        protected RequestDelegate Next { get; }

        protected Middleware(RequestDelegate next)
        {
            Next = next ?? throw new ArgumentNullException(nameof(next));
        }
        
        public virtual void Invoke(HttpContext context)
        {
            Next.Invoke(context).ConfigureAwait(false);
        }
    }
    
    public abstract class AsyncMiddleware : IAsyncMiddleware
    {
        protected RequestDelegate Next { get; }

        protected AsyncMiddleware(RequestDelegate next)
        {
            Next = next ?? throw new ArgumentNullException(nameof(next));
        }
        
        public virtual async Task InvokeAsync(HttpContext context)
        {
            await Next.Invoke(context).ConfigureAwait(false);
        }
    }
}