// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NetExtender.Types.Exceptions;

namespace NetExtender.AspNetCore.Types.Middlewares
{
    public class BusinessExceptionHandlerMiddleware : ExceptionHandlerMiddleware<BusinessException>
    {
        public BusinessExceptionHandlerMiddleware(RequestDelegate next)
            : base(next, Handle)
        {
        }

        private static Boolean Handle(BusinessException exception, out HttpStatusCode? code)
        {
            code = exception.Status ?? HttpStatusCode.InternalServerError;
            return true;
        }

        public override async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await Next.Invoke(context).ConfigureAwait(false);
            }
            catch (BusinessException exception)
            {
                if (!Handler(exception, out HttpStatusCode? code))
                {
                    throw;
                }

                if (code is not null)
                {
                    context.Response.StatusCode = (Int32) code;
                }

                await context.Response.WriteAsJsonAsync(exception.Info).ConfigureAwait(false);
            }
        }
    }
}