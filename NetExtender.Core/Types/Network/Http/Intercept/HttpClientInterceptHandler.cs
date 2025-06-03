// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Net.Http;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Types.Intercept;
using NetExtender.Types.Intercept.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Network
{
    public sealed class HttpClientInterceptHandler : DelegatingHandler, IIntercept<HttpInterceptEventArgs>, IAnyInterceptTarget<HttpResponseMessage?, HttpInterceptEventArgs>
    {
        public IAnyInterceptor<HttpResponseMessage?, HttpClientInterceptHandler, HttpInterceptEventArgs> Interceptor { get; init; } = AnyInterceptor<HttpResponseMessage?, HttpClientInterceptHandler, HttpInterceptEventArgs>.Default;
        
        public event EventHandler<HttpInterceptEventArgs>? SendingRequest;
        public event EventHandler<HttpInterceptEventArgs>? ResponseReceived;
        
        public event EventHandler<HttpInterceptEventArgs>? Intercept
        {
            add
            {
                SendingRequest += value;
                ResponseReceived += value;
            }
            remove
            {
                SendingRequest -= value;
                ResponseReceived -= value;
            }
        }

        event EventHandler<Object?, HttpInterceptEventArgs>? IIntercept<Object?, HttpInterceptEventArgs>.Intercept
        {
            add
            {
                Intercept += value.Unsafe<EventHandler<HttpInterceptEventArgs>>();
            }
            remove
            {
                Intercept -= value.Unsafe<EventHandler<HttpInterceptEventArgs>>();
            }
        }

        event EventHandler<Object?, HttpInterceptEventArgs>? IIntercept<Object?, HttpInterceptEventArgs>.Intercepting
        {
            add
            {
                SendingRequest += value.Unsafe<EventHandler<HttpInterceptEventArgs>>();
            }
            remove
            {
                SendingRequest -= value.Unsafe<EventHandler<HttpInterceptEventArgs>>();
            }
        }

        event EventHandler<Object?, HttpInterceptEventArgs>? IIntercept<Object?, HttpInterceptEventArgs>.Intercepted
        {
            add
            {
                ResponseReceived += value.Unsafe<EventHandler<HttpInterceptEventArgs>>();
            }
            remove
            {
                ResponseReceived -= value.Unsafe<EventHandler<HttpInterceptEventArgs>>();
            }
        }

        public HttpClientInterceptHandler()
            : this(null)
        {
        }

        public HttpClientInterceptHandler(HttpMessageHandler? inner)
            : base(inner ?? new HttpClientHandler())
        {
        }

        protected override HttpResponseMessage Send(HttpRequestMessage request, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();
            using HttpInterceptEventArgs args = new HttpInterceptEventArgs(request) { IsAsync = false, Token = token };
            return Interceptor.Intercept(this, args) ?? throw new SocketException();
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();
            using HttpInterceptEventArgs args = new HttpInterceptEventArgs(request) { IsAsync = true, Token = token };
            return await Interceptor.InterceptAsync(this, args) ?? throw new SocketException();
        }

        HttpResponseMessage IInterceptTarget<HttpResponseMessage?, HttpInterceptEventArgs>.Invoke(HttpInterceptEventArgs argument)
        {
            return base.Send(argument.Request, argument.Token);
        }

        async ValueTask<HttpResponseMessage?> IAsyncInterceptTarget<HttpResponseMessage?, HttpInterceptEventArgs>.InvokeAsync(HttpInterceptEventArgs argument)
        {
            return await base.SendAsync(argument.Request, argument.Token);
        }

        Boolean IInterceptTargetResult<HttpResponseMessage?, HttpInterceptEventArgs>.HasResult(HttpInterceptEventArgs argument)
        {
            return argument.Response is not null;
        }

        HttpResponseMessage? IInterceptTargetResult<HttpResponseMessage?, HttpInterceptEventArgs>.Result(HttpInterceptEventArgs argument)
        {
            return argument.Response;
        }

        void IInterceptTargetRaise<HttpInterceptEventArgs>.RaiseIntercepting(HttpInterceptEventArgs args)
        {
            SendingRequest?.Invoke(this, args);
        }

        void IInterceptTargetRaise<HttpInterceptEventArgs>.RaiseIntercepted(HttpInterceptEventArgs args)
        {
            ResponseReceived?.Invoke(this, args);
        }

        protected override void Dispose(Boolean disposing)
        {
            SendingRequest = null;
            ResponseReceived = null;
            base.Dispose(disposing);
        }
    }
}