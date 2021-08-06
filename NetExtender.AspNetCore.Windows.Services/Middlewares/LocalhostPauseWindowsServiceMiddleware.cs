// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NetExtender.AspNetCore.Windows.Services.Services.Interfaces;
using NetExtender.Utils.AspNetCore.Types;

namespace NetExtender.AspNetCore.Windows.Services.Middlewares
{
    public class LocalhostPauseWindowsServiceMiddleware
    {
        protected RequestDelegate Next { get; }
        
        public Int32 RestrictStatusCode { get; }

        public LocalhostPauseWindowsServiceMiddleware(RequestDelegate next)
            : this(next, 403)
        {
        }

        public LocalhostPauseWindowsServiceMiddleware(RequestDelegate next, Int32 code)
        {
            Next = next ?? throw new ArgumentNullException(nameof(next));
            RestrictStatusCode = code;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            IWindowsServicePauseService? service = context.RequestServices.GetService<IWindowsServicePauseService>();

            if (service is null)
            {
                await Next(context);
                return;
            }
            
            if (service.IsPaused && !context.Request.IsLocalHost())
            {
                context.Response.StatusCode = RestrictStatusCode;
                return;
            }

            await Next(context);
        }
    }
}