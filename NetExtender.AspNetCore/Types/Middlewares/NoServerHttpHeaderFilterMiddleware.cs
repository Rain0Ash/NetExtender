// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace NetExtender.AspNetCore.Types.Middlewares
{
    public sealed class NoServerHttpHeaderFilterMiddleware
    {
        private const String ServerHttpHeaderName = "Server";
        
        private RequestDelegate Next { get; }

        public NoServerHttpHeaderFilterMiddleware(RequestDelegate next)
        {
            Next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public Task Invoke(HttpContext context)
        {
            context.Response.Headers.Remove(ServerHttpHeaderName);
            return Next.Invoke(context);
        }
    }
}