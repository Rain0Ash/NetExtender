// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NetExtender.Utils.AspNetCore.Types;

namespace NetExtender.AspNet.Core.Middlewares
{
    public class LocalhostMiddleware
    {
        protected RequestDelegate Next { get; }
        
        public Int32 RestrictStatusCode { get; }

        public LocalhostMiddleware(RequestDelegate next)
            : this(next, 403)
        {
        }

        public LocalhostMiddleware(RequestDelegate next, Int32 code)
        {
            Next = next ?? throw new ArgumentNullException(nameof(next));
            RestrictStatusCode = code;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.IsLocalHost())
            {
                context.Response.StatusCode = RestrictStatusCode;
                return;
            }

            await Next(context);
        }
    }
}