using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
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

        protected Predicate<HttpResponseMessage>? Validator { get; }
        public Boolean Buffering { get; init; }
        
        public Action<ILogger, HttpMethod, Uri?, String, String, String, Exception?>? RequestSuccessHandler { get; init; } = DefaultRequestSuccessHandler;
        public Action<ILogger, String, HttpStatusCode, String?, String, String, Exception?>? ResponseSuccessHandler { get; init; } = DefaultResponseSuccessHandler;
        public Action<ILogger, HttpMethod, Uri?, String, String, String, Exception?>? RequestFailHandler { get; init; } = DefaultRequestFailHandler;
        public Action<ILogger, String, HttpStatusCode, String?, String, String, Exception?>? ResponseFailHandler { get; init; } = DefaultResponseFailHandler;

        public HttpTracingHandler(ILogger? logger)
            : this(logger, false)
        {
        }

        public HttpTracingHandler(ILogger? logger, Boolean buffering)
            : this(logger, null, buffering)
        {
        }

        public HttpTracingHandler(ILogger? logger, Predicate<HttpResponseMessage>? validator)
            : this(logger, validator, false)
        {
        }

        public HttpTracingHandler(ILogger? logger, Predicate<HttpResponseMessage>? validator, Boolean buffering)
            : this(null, logger, validator, buffering)
        {
        }

        public HttpTracingHandler(HttpMessageHandler? handler, ILogger? logger)
            : this(handler, logger, false)
        {
        }

        public HttpTracingHandler(HttpMessageHandler? handler, ILogger? logger, Boolean buffering)
            : this(handler, logger, null, buffering)
        {
        }

        public HttpTracingHandler(HttpMessageHandler? handler, ILogger? logger, Predicate<HttpResponseMessage>? validator)
            : this(handler, logger, validator, false)
        {
        }

        public HttpTracingHandler(HttpMessageHandler? handler, ILogger? logger, Predicate<HttpResponseMessage>? validator, Boolean buffering)
            : base(handler, logger)
        {
            Validator = validator;
            Buffering = buffering;
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
                if (Buffering && request.Content is not null)
                {
                    Stream stream = await request.Content.ReadAsStreamAsync(token).ConfigureAwait(false);
                    request.Content = new StreamContent(stream).AddHeaders(request.Content.Headers);
                }
                
                HttpResponseMessage response = await base.SendAsync(request, token).ConfigureAwait(false);

                if (Logger is null)
                {
                    return response;
                }

                if (!Validate(response))
                {
                    await Fail(Logger, request).ConfigureAwait(false);
                    await Fail(Logger, response).ConfigureAwait(false);
                    return response;
                }

                if (!Logger.IsEnabled(LogLevel.Trace))
                {
                    return response;
                }

                await Success(Logger, request).ConfigureAwait(false);
                await Success(Logger, response).ConfigureAwait(false);
                return response;
            }
            catch (Exception exception)
            {
                await Fail(Logger, request, exception).ConfigureAwait(false);
                throw;
            }
        }

        protected virtual async ValueTask Success(ILogger? logger, HttpRequestMessage? request)
        {
            if (logger is null || request is null || RequestSuccessHandler is null)
            {
                return;
            }

            String content = request.Content is not null ? await request.Content.ReadAsStringAsync().ConfigureAwait(false) : String.Empty;
            RequestSuccessHandler(logger, request.Method, request.RequestUri, $"HTTP/{request.Version}", request.ToHeaderString(), content, null);
        }

        [SuppressMessage("ReSharper", "ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract")]
        protected virtual async ValueTask Success(ILogger? logger, HttpResponseMessage? response)
        {
            if (logger is null || response is null || ResponseSuccessHandler is null)
            {
                return;
            }

            String content = response.Content is not null ? await response.Content.ReadAsStringAsync().ConfigureAwait(false) : String.Empty;
            ResponseSuccessHandler(logger, $"HTTP/{response.Version}", response.StatusCode, response.ReasonPhrase, response.ToHeaderString(), content, null);
        }

        protected ValueTask Fail(ILogger? logger, HttpRequestMessage? request)
        {
            return Fail(logger, request, null);
        }

        protected virtual async ValueTask Fail(ILogger? logger, HttpRequestMessage? request, Exception? exception)
        {
            if (logger is null || request is null || RequestFailHandler is null)
            {
                return;
            }

            String content = request.Content is not null ? await request.Content.ReadAsStringAsync().ConfigureAwait(false) : String.Empty;
            RequestFailHandler(logger, request.Method, request.RequestUri, $"HTTP/{request.Version}", request.ToHeaderString(), content, exception);
        }

        protected ValueTask Fail(ILogger? logger, HttpResponseMessage response)
        {
            return Fail(logger, response, null);
        }

        [SuppressMessage("ReSharper", "ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract")]
        protected virtual async ValueTask Fail(ILogger? logger, HttpResponseMessage? response, Exception? exception)
        {
            if (logger is null || response is null || ResponseFailHandler is null)
            {
                return;
            }

            String content = response.Content is not null ? await response.Content.ReadAsStringAsync().ConfigureAwait(false) : String.Empty;
            ResponseFailHandler(logger, $"HTTP/{response.Version}", response.StatusCode, response.ReasonPhrase, response.ToHeaderString(), content, exception);
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
        public HttpTracingHandler(ILoggerFactory logger)
            : this(logger, null)
        {
        }

        public HttpTracingHandler(ILoggerFactory logger, Predicate<HttpResponseMessage>? validator)
            : base(logger is not null ? logger.CreateLogger(LoggerCategory<T>()) : throw new ArgumentNullException(nameof(logger)), validator)
        {
        }

        public HttpTracingHandler(HttpMessageHandler? handler, ILoggerFactory logger)
            : this(handler, logger, null)
        {
        }

        public HttpTracingHandler(HttpMessageHandler? handler, ILoggerFactory logger, Predicate<HttpResponseMessage>? validator)
            : base(handler, logger is not null ? logger.CreateLogger(LoggerCategory<T>()) : throw new ArgumentNullException(nameof(logger)), validator)
        {
        }
    }
}