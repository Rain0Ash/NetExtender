// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace NetExtender.AspNetCore.Types.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        public delegate Boolean ExceptionHandlerMiddlewareHandler(Exception exception, out HttpStatusCode? code);
        protected RequestDelegate Next { get; }
        
        protected ExceptionHandlerMiddlewareHandler Handler { get; }

        public ExceptionHandlerMiddleware(RequestDelegate next, ExceptionHandlerMiddlewareHandler handler)
        {
            Next = next ?? throw new ArgumentNullException(nameof(next));
            Handler = handler ?? throw new ArgumentNullException(nameof(handler));
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await Next.Invoke(context).ConfigureAwait(false);
            }
            catch (Exception exception)
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