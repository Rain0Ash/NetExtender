// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Net;
using System.Net.Mime;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NetExtender.Types.Exceptions;

namespace NetExtender.AspNetCore.Types.Middlewares
{
    public class BusinessExceptionHandlerMiddleware : ExceptionHandlerMiddleware<BusinessException>
    {
        private static ActionDescriptor Descriptor { get; } = new ActionDescriptor
        {
            DisplayName = "Business Exception Handler Middleware"
        };
        
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
                if (!Handler(exception, out HttpStatusCode? code) || context.Response.HasStarted)
                {
                    throw;
                }

                IActionResultExecutor<ObjectResult>? executor = context.RequestServices.GetService<IActionResultExecutor<ObjectResult>>();
                await Handle(context, exception, code, executor).ConfigureAwait(false);
            }
        }

        protected virtual async ValueTask Handle(HttpContext context, BusinessException exception, HttpStatusCode? code)
        {
            if (code is not null)
            {
                context.Response.StatusCode = (Int32) code;
            }
                    
            context.Response.ContentType = MediaTypeNames.Application.Json;
            JsonSerializerOptions? options = context.RequestServices.GetService<IOptions<JsonOptions>>()?.Value.JsonSerializerOptions;
            await context.Response.WriteAsJsonAsync(exception.Business, options).ConfigureAwait(false);
        }

        protected virtual async ValueTask Handle(HttpContext context, BusinessException exception, HttpStatusCode? code, IActionResultExecutor<ObjectResult>? executor)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (exception is null)
            {
                throw new ArgumentNullException(nameof(exception));
            }

            if (executor is null)
            {
                await Handle(context, exception, code);
                return;
            }
            
            ActionDescriptor descriptor = context.GetEndpoint()?.Metadata.GetMetadata<ActionDescriptor>() ?? Descriptor;
            ObjectResult result = new ObjectResult(exception.Business) { StatusCode = (Int32?) code };
            await executor.ExecuteAsync(new ActionContext(context, context.GetRouteData(), descriptor), result);
        }
    }
}