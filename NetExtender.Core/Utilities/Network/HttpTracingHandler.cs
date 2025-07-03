using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NetExtender.Types.Network;
using NetExtender.Utilities.Core;

namespace NetExtender.Utilities.Network
{
    public class HttpTracingHandler : HttpDelegatingHandler
    {
        public static String LogCategoryPrefix { get; set; } = "System.Net.Http.HttpClient";
        public static String LogCategorySuffix { get; set; } = "TraceHandler";
        
        private static Action<ILogger, HttpMethod, Uri?, String, String, String, Exception?> DefaultRequestSuccessHandler { get; }
        private static Action<ILogger, String, HttpStatusCode, String?, String, String, Exception?> DefaultResponseSuccessHandler { get; }
        private static Action<ILogger, HttpMethod, Uri?, String, String, String, Exception?> DefaultRequestFailHandler { get; }
        private static Action<ILogger, String, HttpStatusCode, String?, String, String, Exception?> DefaultResponseFailHandler { get; }

        static HttpTracingHandler()
        {
            const String RequestMessageFormat = "{RequestMethod} {RequestUri} {RequestHttpVersion}\n{RequestHeaders}\n\n{RequestBody}";
            const String ResponseMessageFormat = "{ResponseHttpVersion} {ResponseStatusCode} {ResponseReason}\n{ResponseHeaders}\n\n{ResponseBody}";
            DefaultRequestSuccessHandler = LoggerMessage.Define<HttpMethod, Uri?, String, String, String>(LogLevel.Trace, Events.RequestSuccess, RequestMessageFormat);
            DefaultResponseSuccessHandler = LoggerMessage.Define<String, HttpStatusCode, String?, String, String>(LogLevel.Trace, Events.ResponseSuccess, ResponseMessageFormat);
            DefaultRequestFailHandler = LoggerMessage.Define<HttpMethod, Uri?, String, String, String>(LogLevel.Warning, Events.RequestFail, RequestMessageFormat);
            DefaultResponseFailHandler = LoggerMessage.Define<String, HttpStatusCode, String?, String, String>(LogLevel.Warning, Events.ResponseFail, ResponseMessageFormat);
        }

        protected ILogger Logger { get; }
        protected Predicate<HttpResponseMessage>? Validator { get; }
        public Action<ILogger, HttpMethod, Uri?, String, String, String, Exception?>? RequestSuccessHandler { get; init; } = DefaultRequestSuccessHandler;
        public Action<ILogger, String, HttpStatusCode, String?, String, String, Exception?>? ResponseSuccessHandler { get; init; } = DefaultResponseSuccessHandler;
        public Action<ILogger, HttpMethod, Uri?, String, String, String, Exception?>? RequestFailHandler { get; init; } = DefaultRequestFailHandler;
        public Action<ILogger, String, HttpStatusCode, String?, String, String, Exception?>? ResponseFailHandler { get; init; } = DefaultResponseFailHandler;
        
        public HttpTracingHandler(ILogger logger)
            : this(logger, null)
        {
        }

        public HttpTracingHandler(ILogger logger, Predicate<HttpResponseMessage>? validator)
            : this(null, logger, validator)
        {
        }

        public HttpTracingHandler(HttpMessageHandler? handler, ILogger logger)
            : this(handler, logger, null)
        {
        }

        public HttpTracingHandler(HttpMessageHandler? handler, ILogger logger, Predicate<HttpResponseMessage>? validator)
            : base(logger is not null ? handler : throw new ArgumentNullException(nameof(logger)))
        {
            Logger = logger;
            Validator = validator;
        }

        protected virtual Boolean Validate(HttpResponseMessage response)
        {
            return Validator?.Invoke(response) ?? response.IsSuccessStatusCode;
        }

        public static String LoggerCategory(String name)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return $"{LogCategoryPrefix}.{name}.{LogCategorySuffix}";
        }

        public static String LoggerCategory<T>()
        {
            return LoggerCategory(typeof(T).Name);
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken token)
        {
            try
            {
                HttpResponseMessage response = await base.SendAsync(request, token);

                if (!Validate(response))
                {
                    await Fail(request);
                    await Fail(response);
                    return response;
                }

                if (!Logger.IsEnabled(LogLevel.Trace))
                {
                    return response;
                }

                await Success(request);
                await Success(response);
                return response;
            }
            catch (Exception exception)
            {
                await Fail(request, exception);
                throw;
            }
        }

        protected virtual async Task Success(HttpRequestMessage? request)
        {
            if (request is null || RequestSuccessHandler is null)
            {
                return;
            }

            String content = request.Content is not null ? await request.Content.ReadAsStringAsync() : String.Empty;
            RequestSuccessHandler(Logger, request.Method, request.RequestUri, $"HTTP/{request.Version}", request.ToHeaderString(), content, null);
        }

        [SuppressMessage("ReSharper", "ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract")]
        protected virtual async Task Success(HttpResponseMessage? response)
        {
            if (response is null || ResponseSuccessHandler is null)
            {
                return;
            }

            String content = response.Content is not null ? await response.Content.ReadAsStringAsync() : String.Empty;
            ResponseSuccessHandler(Logger, $"HTTP/{response.Version}", response.StatusCode, response.ReasonPhrase, response.ToHeaderString(), content, null);
        }

        protected Task Fail(HttpRequestMessage? request)
        {
            return Fail(request, null);
        }

        protected virtual async Task Fail(HttpRequestMessage? request, Exception? exception)
        {
            if (request is null || RequestFailHandler is null)
            {
                return;
            }

            String content = request.Content is not null ? await request.Content.ReadAsStringAsync() : String.Empty;
            RequestFailHandler(Logger, request.Method, request.RequestUri, $"HTTP/{request.Version}", request.ToHeaderString(), content, exception);
        }

        protected Task Fail(HttpResponseMessage response)
        {
            return Fail(response, null);
        }

        [SuppressMessage("ReSharper", "ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract")]
        protected virtual async Task Fail(HttpResponseMessage? response, Exception? exception)
        {
            if (response is null || ResponseFailHandler is null)
            {
                return;
            }

            String content = response.Content is not null ? await response.Content.ReadAsStringAsync() : String.Empty;
            ResponseFailHandler(Logger, $"HTTP/{response.Version}", response.StatusCode, response.ReasonPhrase, response.ToHeaderString(), content, exception);
        }
        
        private static class Events
        {
            [ReflectionSignature]
            public static EventId RequestSuccess
            {
                get
                {
                    return new EventId(200, nameof(RequestSuccess));
                }
            }

            [ReflectionSignature]
            public static EventId ResponseSuccess
            {
                get
                {
                    return new EventId(201, nameof(ResponseSuccess));
                }
            }

            [ReflectionSignature]
            public static EventId RequestFail
            {
                get
                {
                    return new EventId(210, nameof(RequestFail));
                }
            }

            [ReflectionSignature]
            public static EventId ResponseFail
            {
                get
                {
                    return new EventId(211, nameof(ResponseFail));
                }
            }
        }
    }
    
    public class HttpTracingHandler<T> : HttpTracingHandler
    {
        public HttpTracingHandler(ILoggerFactory factory)
            : this(factory, null)
        {
        }

        public HttpTracingHandler(ILoggerFactory factory, Predicate<HttpResponseMessage>? validator)
            : base(factory is not null ? factory.CreateLogger(LoggerCategory<T>()) : throw new ArgumentNullException(nameof(factory)), validator)
        {
        }

        public HttpTracingHandler(HttpMessageHandler? handler, ILoggerFactory factory)
            : this(handler, factory, null)
        {
        }

        public HttpTracingHandler(HttpMessageHandler? handler, ILoggerFactory factory, Predicate<HttpResponseMessage>? validator)
            : base(handler, factory is not null ? factory.CreateLogger(LoggerCategory<T>()) : throw new ArgumentNullException(nameof(factory)), validator)
        {
        }
    }
}