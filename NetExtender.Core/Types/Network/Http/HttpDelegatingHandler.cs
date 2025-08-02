using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace NetExtender.Types.Network
{
    public abstract class HttpDelegatingHandler : DelegatingHandler
    {
        public static Func<HttpMessageHandler?>? Factory { get; set; }
        protected ILogger? Logger { get; }

        protected HttpDelegatingHandler()
            : this((HttpMessageHandler?) null)
        {
        }

        protected HttpDelegatingHandler(ILogger? logger)
            : this(null, logger)
        {
        }

        protected HttpDelegatingHandler(HttpMessageHandler? handler)
            : this(handler, null)
        {
        }

        protected HttpDelegatingHandler(HttpMessageHandler? handler, ILogger? logger)
            : base(handler ?? Factory?.Invoke() ?? new HttpClientHandler())
        {
            Logger = logger;
        }

        protected override HttpResponseMessage Send(HttpRequestMessage request, CancellationToken token)
        {
            return base.Send(request, token);
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken token)
        {
            return base.SendAsync(request, token);
        }
    }
}