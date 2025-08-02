using System;
using System.Collections.Immutable;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IO;
using NetExtender.Types.Exceptions;
using NetExtender.Types.Streams;
using NetExtender.Utilities.AspNetCore.Types;
using NetExtender.Utilities.Core;
using NetExtender.Utilities.Network;

namespace NetExtender.AspNetCore.Types.Middlewares
{
    public class RequestTracingMiddleware : AsyncMiddleware
    {
        public sealed record Settings
        {
            public ImmutableArray<Regex> Include { get; init; }
            public ImmutableArray<Regex> Exclude { get; init; }
        }

        private static Action<ILogger, String?, String?, String?, String, String, Exception?> DefaultRequestTraceHandler { get; }
        private static Action<ILogger, String?, HttpStatusCode, String, String, Exception?> DefaultResponseTraceHandler { get; }
        private static Action<ILogger, String?, HttpStatusCode, String, String, Exception?> DefaultResponseWarningHandler { get; }
        private static Action<ILogger, String?, HttpStatusCode, String, String, Exception?> DefaultResponseErrorHandler { get; }

        static RequestTracingMiddleware()
        {
            const String RequestMessageFormat = "{RequestMethod} {RequestUri} {RequestProtocol}\n{RequestHeaders}\n\n{RequestBody}";
            const String ResponseMessageFormat = "{ResponseProtocol} {ResponseStatusCode}\n{ResponseHeaders}\n\n{ResponseBody}";
            DefaultRequestTraceHandler = LoggerMessage.Define<String?, String?, String?, String, String>(LogLevel.Trace, Events.RequestTrace, RequestMessageFormat);
            DefaultResponseTraceHandler = LoggerMessage.Define<String?, HttpStatusCode, String, String>(LogLevel.Trace, Events.ResponseTrace, ResponseMessageFormat);
            DefaultResponseWarningHandler = LoggerMessage.Define<String?, HttpStatusCode, String, String>(LogLevel.Warning, Events.ResponseWarning, ResponseMessageFormat);
            DefaultResponseErrorHandler = LoggerMessage.Define<String?, HttpStatusCode, String, String>(LogLevel.Error, Events.ResponseError, ResponseMessageFormat);
        }

        protected RecyclableMemoryStreamManager Memory { get; } = new RecyclableMemoryStreamManager();
        protected ILogger? Logger { get; }
        protected IOptions<Settings>? Options { get; }

        public Action<ILogger, String?, String?, String?, String, String, Exception?>? RequestTraceHandler { get; init; } = DefaultRequestTraceHandler;
        public Action<ILogger, String?, HttpStatusCode, String, String, Exception?>? ResponseTraceHandler { get; init; } = DefaultResponseTraceHandler;
        public Action<ILogger, String?, HttpStatusCode, String, String, Exception?>? ResponseWarningHandler { get; init; } = DefaultResponseWarningHandler;
        public Action<ILogger, String?, HttpStatusCode, String, String, Exception?>? ResponseErrorHandler { get; init; } = DefaultResponseErrorHandler;

        public RequestTracingMiddleware(RequestDelegate next, ILogger<RequestTracingMiddleware>? logger, IOptions<Settings>? options)
            : base(next)
        {
            Logger = logger;
            Options = options;
        }

        public override async Task InvokeAsync(HttpContext context)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (Logger is null || Options is not { Value: { Include.IsEmpty: false } options } || !IsMatch(context.Request, options.Include) || options.Exclude.Length > 0 && IsMatch(context.Request, options.Exclude))
            {
                await base.InvokeAsync(context);
                return;
            }

            context.Request.EnableBuffering();

            await using Microsoft.IO.RecyclableMemoryStream stream = Memory.GetStream();
            DuplicateStream duplicate = new DuplicateStream(context.Response.Body, stream);
            context.Response.Body = duplicate;

            await Trace(Logger, context.Request);

            await base.InvokeAsync(context);

            HttpStatusCode status = (HttpStatusCode) context.Response.StatusCode;
            HttpStatusCategory category = status.Category();
            
            switch (category)
            {
                case HttpStatusCategory.Unknown:
                case HttpStatusCategory.Informational:
                case HttpStatusCategory.Success:
                case HttpStatusCategory.Redirection:
                    await Trace(Logger, context.Response, stream, context.Request.Protocol);
                    return;
                case HttpStatusCategory.ClientError:
                    await Warning(Logger, context.Response, stream, context.Request.Protocol);
                    return;
                case HttpStatusCategory.ServerError:
                case HttpStatusCategory.CustomError:
                    await Error(Logger, context.Response, stream, context.Request.Protocol);
                    return;
                default:
                    throw new EnumUndefinedOrNotSupportedException<HttpStatusCategory>(category, nameof(category), null);
            }
        }
        
        protected virtual Boolean IsMatch(HttpRequest request, ImmutableArray<Regex> rules)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            foreach (Regex rule in rules)
            {
                try
                {
                    if (rule.IsMatch(request.Path))
                    {
                        return true;
                    }
                }
                catch (ArgumentException exception)
                {
                    Logger?.LogError("Error while matching with pattern {Pattern}: {Message}.", rule.ToString(), exception.Message);
                }
            }

            return false;
        }

        protected virtual async ValueTask Trace(ILogger? logger, HttpRequest? request)
        {
            if (logger is null || request is null || RequestTraceHandler is null)
            {
                return;
            }

            String address = $"{request.Scheme}://{request.Host}{request.Path}{request.QueryString}";
            
            if (request.Body is not { CanSeek: true } body)
            {
                RequestTraceHandler(logger, request.Method, address, request.Protocol, request.Headers.ToHeaderString(), String.Empty, null);
                return;
            }
            
            body.Seek(0, SeekOrigin.Begin);
            String content = await new StreamReader(body).ReadToEndAsync();
            RequestTraceHandler(logger, request.Method, address, request.Protocol, request.Headers.ToHeaderString(), content, null);
            body.Seek(0, SeekOrigin.Begin);
        }

        protected virtual async ValueTask Trace(ILogger? logger, HttpResponse? response, Stream? body, String? protocol)
        {
            if (logger is null || response is null || body is null || ResponseTraceHandler is null)
            {
                return;
            }

            body.Seek(0, SeekOrigin.Begin);
            ResponseTraceHandler(logger, protocol, (HttpStatusCode) response.StatusCode, response.Headers.ToHeaderString(), await new StreamReader(body).ReadToEndAsync(), null);
        }

        protected virtual async ValueTask Warning(ILogger? logger, HttpResponse? response, Stream? body, String? protocol)
        {
            if (logger is null || response is null || body is null || ResponseWarningHandler is null)
            {
                return;
            }

            body.Seek(0, SeekOrigin.Begin);
            ResponseWarningHandler(logger, protocol, (HttpStatusCode) response.StatusCode, response.Headers.ToHeaderString(), await new StreamReader(body).ReadToEndAsync(), null);
        }

        protected virtual async ValueTask Error(ILogger? logger, HttpResponse? response, Stream? body, String? protocol)
        {
            if (logger is null || response is null || body is null || ResponseErrorHandler is null)
            {
                return;
            }

            body.Seek(0, SeekOrigin.Begin);
            ResponseErrorHandler(logger, protocol, (HttpStatusCode) response.StatusCode, response.Headers.ToHeaderString(), await new StreamReader(body).ReadToEndAsync(), null);
        }
        
        private static class Events
        {
            [ReflectionSignature]
            public static EventId RequestTrace
            {
                get
                {
                    return new EventId(300, nameof(RequestTrace));
                }
            }

            [ReflectionSignature]
            public static EventId ResponseTrace
            {
                get
                {
                    return new EventId(310, nameof(ResponseTrace));
                }
            }

            [ReflectionSignature]
            public static EventId ResponseWarning
            {
                get
                {
                    return new EventId(311, nameof(ResponseWarning));
                }
            }

            [ReflectionSignature]
            public static EventId ResponseError
            {
                get
                {
                    return new EventId(312, nameof(ResponseError));
                }
            }
        }
    }
}