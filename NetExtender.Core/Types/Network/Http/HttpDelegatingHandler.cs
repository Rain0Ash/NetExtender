using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace NetExtender.Types.Network
{
    public abstract class HttpDelegatingHandler : DelegatingHandler
    {
        public static Func<HttpMessageHandler?>? Factory { get; set; }

        protected HttpDelegatingHandler()
            : this(null)
        {
        }

        protected HttpDelegatingHandler(HttpMessageHandler? handler)
            : base(handler ?? Factory?.Invoke() ?? new HttpClientHandler())
        {
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