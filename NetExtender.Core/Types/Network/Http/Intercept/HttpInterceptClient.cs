using System;
using System.Net.Http;
using NetExtender.Types.Interception.Interfaces;
using NetExtender.Types.Network.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Network
{
    public delegate void HttpInterceptEventHandler(HttpInterceptClient sender, HttpInterceptEventArgs args);

    public class HttpInterceptStaticClient : HttpInterceptClient
    {
        public new static event HttpInterceptEventHandler? Intercept;
        public new static event HttpInterceptEventHandler? SendingRequest;
        public new static event HttpInterceptEventHandler? ResponseReceived;

        protected HttpInterceptStaticClient()
        {
            base.SendingRequest += OnSendingRequest;
            base.ResponseReceived += OnResponseReceived;
        }

        protected HttpInterceptStaticClient(HttpClientHandler? handler)
            : base(handler)
        {
            base.SendingRequest += OnSendingRequest;
            base.ResponseReceived += OnResponseReceived;
        }

        protected HttpInterceptStaticClient(HttpClientInterceptHandler? handler)
            : base(handler)
        {
            base.SendingRequest += OnSendingRequest;
            base.ResponseReceived += OnResponseReceived;
        }

        private static void OnIntercept(HttpInterceptClient sender, HttpInterceptEventArgs args)
        {
            if (!args.IsCancel)
            {
                Intercept?.Invoke(sender, args);
            }
        }

        private static void OnSendingRequest(HttpInterceptClient sender, HttpInterceptEventArgs args)
        {
            if (!args.IsCancel)
            {
                SendingRequest?.Invoke(sender, args);
            }
        }

        private static void OnResponseReceived(HttpInterceptClient sender, HttpInterceptEventArgs args)
        {
            if (!args.IsCancel)
            {
                ResponseReceived?.Invoke(sender, args);
            }
        }

        protected override void Dispose(Boolean disposing)
        {
            SendingRequest -= OnSendingRequest;
            ResponseReceived -= OnResponseReceived;
            base.Dispose(disposing);
        }
    }

    public class HttpInterceptClient : HttpClient, IHttpInterceptClient
    {
        protected HttpClientInterceptHandler Handler { get; }

        public event HttpInterceptEventHandler? Intercept;
        public event HttpInterceptEventHandler? SendingRequest;
        public event HttpInterceptEventHandler? ResponseReceived;

        event EventHandler<HttpInterceptClient, HttpInterceptEventArgs>? IIntercept<HttpInterceptClient, HttpInterceptEventArgs>.Intercept
        {
            add
            {
                Intercept += value.Unsafe<HttpInterceptEventHandler>();
            }
            remove
            {
                Intercept -= value.Unsafe<HttpInterceptEventHandler>();
            }
        }

        event EventHandler<HttpInterceptClient, HttpInterceptEventArgs>? IIntercept<HttpInterceptClient, HttpInterceptEventArgs>.Intercepting
        {
            add
            {
                SendingRequest += value.Unsafe<HttpInterceptEventHandler>();
            }
            remove
            {
                SendingRequest -= value.Unsafe<HttpInterceptEventHandler>();
            }
        }

        event EventHandler<HttpInterceptClient, HttpInterceptEventArgs>? IIntercept<HttpInterceptClient, HttpInterceptEventArgs>.Intercepted
        {
            add
            {
                ResponseReceived += value.Unsafe<HttpInterceptEventHandler>();
            }
            remove
            {
                ResponseReceived -= value.Unsafe<HttpInterceptEventHandler>();
            }
        }

        private protected String? _name;
        public virtual String Name
        {
            get
            {
                return _name ??= GetType().Name;
            }
            init
            {
                _name = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        public HttpInterceptClient()
            : this((HttpClientInterceptHandler?) null)
        {
        }

        public HttpInterceptClient(HttpClientHandler? handler)
            : this(new HttpClientInterceptHandler(handler))
        {
        }

        protected HttpInterceptClient(HttpClientInterceptHandler? handler)
            : base(handler ??= new HttpClientInterceptHandler())
        {
            Handler = handler;
            Handler.Intercept += OnIntercept;
            Handler.SendingRequest += OnSendingRequest;
            Handler.ResponseReceived += OnResponseReceived;
        }

        private void OnIntercept(Object? sender, HttpInterceptEventArgs args)
        {
            Intercept?.Invoke(this, args);
        }

        private void OnSendingRequest(Object? sender, HttpInterceptEventArgs args)
        {
            SendingRequest?.Invoke(this, args);
        }

        private void OnResponseReceived(Object? sender, HttpInterceptEventArgs args)
        {
            ResponseReceived?.Invoke(this, args);
        }

        protected override void Dispose(Boolean disposing)
        {
            Intercept = null;
            SendingRequest = null;
            ResponseReceived = null;
            base.Dispose(disposing);
        }
    }
}