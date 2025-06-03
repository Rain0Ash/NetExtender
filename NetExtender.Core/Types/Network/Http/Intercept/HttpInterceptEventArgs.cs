// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Net.Http;
using NetExtender.Types.Intercept;
using NetExtender.Types.Intercept.Interfaces;

namespace NetExtender.Types.Network
{
    public sealed class HttpInterceptEventArgs : InterceptEventArgs<HttpInterceptEventArgs.Information>, ISimpleInterceptEventArgs<HttpResponseMessage?>
    {
        public new static class Default
        {
            public static HttpResponseMessage Response { get; } = new HttpResponseMessage();

            public static Exception Exception
            {
                get
                {
                    return InterceptEventArgs.Default.Exception;
                }
            }
        }
        
        public readonly struct Information : IInterceptArgumentInfo
        {
            public HttpRequestMessage Request { get; }
            public HttpResponseMessage? Response { get; }
            public Exception? Exception { get; }

            public Information(HttpRequestMessage request, HttpResponseMessage? response, Exception? exception)
            {
                Request = request;
                Response = response;
                Exception = exception;
            }
        }

        public HttpRequestMessage Request
        {
            get
            {
                return Info.Request;
            }
        }

        private HttpResponseMessage? _response = Default.Response;
        public HttpResponseMessage? Response
        {
            get
            {
                return !ReferenceEquals(_response, Default.Response) ? _response : Info.Response;
            }
            set
            {
                _response = value;
            }
        }

        HttpResponseMessage? ISimpleInterceptEventArgs<HttpResponseMessage?>.Value
        {
            get
            {
                return Response;
            }
            set
            {
                Response = value;
            }
        }

        Object? ISimpleInterceptEventArgs.Value
        {
            get
            {
                return Response;
            }
            set
            {
                Response = (HttpResponseMessage?) value;
            }
        }

        public override Exception? Exception
        {
            get
            {
                return !ReferenceEquals(base.Exception, Default.Exception) ? base.Exception : Info.Exception;
            }
            set
            {
                base.Exception = value;
            }
        }

        public override Boolean IsCancel
        {
            get
            {
                return !ReferenceEquals(Response, Info.Response) || !ReferenceEquals(Exception, Info.Exception);
            }
        }
        
        public HttpInterceptEventArgs(HttpRequestMessage request)
            : this(request, null, null)
        {
        }
        
        public HttpInterceptEventArgs(HttpRequestMessage request, HttpResponseMessage response)
            : this(request, response ?? throw new ArgumentNullException(nameof(response)), null)
        {
        }

        public HttpInterceptEventArgs(HttpRequestMessage request, Exception exception)
            : this(request, null, exception ?? throw new ArgumentNullException(nameof(exception)))
        {
        }

        private HttpInterceptEventArgs(HttpRequestMessage request, HttpResponseMessage? response, Exception? exception)
            : base(new Information(request ?? throw new ArgumentNullException(nameof(request)), response, exception))
        {
        }

        public void Intercept(HttpResponseMessage? response)
        {
            Intercept();
            Info = new Information(Info.Request, response, null);
            Token.ThrowIfCancellationRequested();
            Invoke();
        }

        public void Intercept(Exception exception)
        {
            if (exception is null)
            {
                throw new ArgumentNullException(nameof(exception));
            }
            
            Intercept();
            Info = new Information(Info.Request, null, exception);
            Token.ThrowIfCancellationRequested();
            Invoke();
        }
    }
}