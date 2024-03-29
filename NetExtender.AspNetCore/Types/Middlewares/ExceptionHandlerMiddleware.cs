// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace NetExtender.AspNetCore.Types.Middlewares
{
    public class ExceptionHandlerMiddleware : ExceptionHandlerMiddleware<Exception>
    {
        public ExceptionHandlerMiddleware(RequestDelegate next, ExceptionHandlerMiddlewareHandler handler)
            : base(next, handler)
        {
        }
    }

    public class ExceptionHandlerMiddleware<T> : AsyncMiddleware where T : Exception
    {
        public delegate Boolean ExceptionHandlerMiddlewareHandler(T exception, out HttpStatusCode? code);

        protected ExceptionHandlerMiddlewareHandler Handler { get; }

        public ExceptionHandlerMiddleware(RequestDelegate next, ExceptionHandlerMiddlewareHandler handler)
            : base(next)
        {
            Handler = handler ?? throw new ArgumentNullException(nameof(handler));
        }

        public override async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await Next.Invoke(context).ConfigureAwait(false);
            }
            catch (T exception)
            {
                if (!Handler(exception, out HttpStatusCode? code))
                {
                    throw;
                }

                if (code is not null)
                {
                    context.Response.StatusCode = (Int32) code;
                }
            }
        }
    }
}