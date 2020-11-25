using System;
using System.Net;
using NetExtender.Network.Networking.SSL;

namespace NetExtender.Network.Networking.Http.Secure
{
    /// <summary>
    ///     HTTPS client is used to communicate with secured HTTPS Web server. It allows to send GET, POST, PUT, DELETE
    ///     requests and receive HTTP result using secure transport.
    /// </summary>
    /// <remarks>Thread-safe.</remarks>
    public class HttpsNetworkClient : SslNetworkClient
    {
        /// <summary>
        ///     Initialize HTTPS client with a given IP address and port number
        /// </summary>
        /// <param name="context">SSL context</param>
        /// <param name="address">IP address</param>
        /// <param name="port">Port number</param>
        public HttpsNetworkClient(SslNetworkContext context, IPAddress address, Int32 port)
            : base(context, address, port)
        {
            NetworkRequest = new HttpNetworkRequest();
            NetworkResponse = new HttpNetworkResponse();
        }

        /// <summary>
        ///     Initialize HTTPS client with a given IP address and port number
        /// </summary>
        /// <param name="context">SSL context</param>
        /// <param name="address">IP address</param>
        /// <param name="port">Port number</param>
        public HttpsNetworkClient(SslNetworkContext context, String address, Int32 port)
            : base(context, address, port)
        {
            NetworkRequest = new HttpNetworkRequest();
            NetworkResponse = new HttpNetworkResponse();
        }

        /// <summary>
        ///     Initialize HTTPS client with a given IP endpoint
        /// </summary>
        /// <param name="context">SSL context</param>
        /// <param name="endpoint">IP endpoint</param>
        public HttpsNetworkClient(SslNetworkContext context, IPEndPoint endpoint)
            : base(context, endpoint)
        {
            NetworkRequest = new HttpNetworkRequest();
            NetworkResponse = new HttpNetworkResponse();
        }

        /// <summary>
        ///     Get the HTTP request
        /// </summary>
        public HttpNetworkRequest NetworkRequest { get; protected set; }

        /// <summary>
        ///     Get the HTTP response
        /// </summary>
        protected HttpNetworkResponse NetworkResponse { get; set; }

        /// <summary>
        ///     Send the current HTTP request (synchronous)
        /// </summary>
        /// <returns>Size of sent data</returns>
        public Int64 SendRequest()
        {
            return SendRequest(NetworkRequest);
        }

        /// <summary>
        ///     Send the HTTP request (synchronous)
        /// </summary>
        /// <param name="request">HTTP request</param>
        /// <returns>Size of sent data</returns>
        public Int64 SendRequest(HttpNetworkRequest request)
        {
            return Send(request.Cache.Data, request.Cache.Offset, request.Cache.Size);
        }

        /// <summary>
        ///     Send the HTTP request body (synchronous)
        /// </summary>
        /// <param name="body">HTTP request body</param>
        /// <returns>Size of sent data</returns>
        public Int64 SendRequestBody(String body)
        {
            return Send(body);
        }

        /// <summary>
        ///     Send the HTTP request body (synchronous)
        /// </summary>
        /// <param name="buffer">HTTP request body buffer</param>
        /// <returns>Size of sent data</returns>
        public Int64 SendRequestBody(Byte[] buffer)
        {
            return Send(buffer);
        }

        /// <summary>
        ///     Send the HTTP request body (synchronous)
        /// </summary>
        /// <param name="buffer">HTTP request body buffer</param>
        /// <param name="offset">HTTP request body buffer offset</param>
        /// <param name="size">HTTP request body size</param>
        /// <returns>Size of sent data</returns>
        public Int64 SendRequestBody(Byte[] buffer, Int64 offset, Int64 size)
        {
            return Send(buffer, offset, size);
        }

        /// <summary>
        ///     Send the current HTTP request (asynchronous)
        /// </summary>
        /// <returns>'true' if the current HTTP request was successfully sent, 'false' if the session is not connected</returns>
        public Boolean SendRequestAsync()
        {
            return SendRequestAsync(NetworkRequest);
        }

        /// <summary>
        ///     Send the HTTP request (asynchronous)
        /// </summary>
        /// <param name="request">HTTP request</param>
        /// <returns>'true' if the current HTTP request was successfully sent, 'false' if the session is not connected</returns>
        public Boolean SendRequestAsync(HttpNetworkRequest request)
        {
            return SendAsync(request.Cache.Data, request.Cache.Offset, request.Cache.Size);
        }

        /// <summary>
        ///     Send the HTTP request body (asynchronous)
        /// </summary>
        /// <param name="body">HTTP request body</param>
        /// <returns>'true' if the HTTP request body was successfully sent, 'false' if the session is not connected</returns>
        public Boolean SendRequestBodyAsync(String body)
        {
            return SendAsync(body);
        }

        /// <summary>
        ///     Send the HTTP request body (asynchronous)
        /// </summary>
        /// <param name="buffer">HTTP request body buffer</param>
        /// <returns>'true' if the HTTP request body was successfully sent, 'false' if the session is not connected</returns>
        public Boolean SendRequestBodyAsync(Byte[] buffer)
        {
            return SendAsync(buffer);
        }

        /// <summary>
        ///     Send the HTTP request body (asynchronous)
        /// </summary>
        /// <param name="buffer">HTTP request body buffer</param>
        /// <param name="offset">HTTP request body buffer offset</param>
        /// <param name="size">HTTP request body size</param>
        /// <returns>'true' if the HTTP request body was successfully sent, 'false' if the session is not connected</returns>
        public Boolean SendRequestBodyAsync(Byte[] buffer, Int64 offset, Int64 size)
        {
            return SendAsync(buffer, offset, size);
        }

        protected override void OnReceived(Byte[] buffer, Int64 offset, Int64 size)
        {
            // Receive HTTP response header
            if (NetworkResponse.IsPendingHeader())
            {
                if (NetworkResponse.ReceiveHeader(buffer, (Int32) offset, (Int32) size))
                {
                    OnReceivedResponseHeader(NetworkResponse);
                }

                size = 0;
            }

            // Check for HTTP response error
            if (NetworkResponse.IsErrorSet)
            {
                OnReceivedResponseError(NetworkResponse, "Invalid HTTP response!");
                NetworkResponse.Clear();
                Disconnect();
                return;
            }

            // Receive HTTP response body
            if (NetworkResponse.ReceiveBody(buffer, (Int32) offset, (Int32) size))
            {
                OnReceivedResponse(NetworkResponse);
                NetworkResponse.Clear();
                return;
            }

            // Check for HTTP response error
            if (!NetworkResponse.IsErrorSet)
            {
                return;
            }

            OnReceivedResponseError(NetworkResponse, "Invalid HTTP response!");
            NetworkResponse.Clear();
            Disconnect();
        }

        protected override void OnDisconnected()
        {
            // Receive HTTP response body
            if (!NetworkResponse.IsPendingBody())
            {
                return;
            }

            OnReceivedResponse(NetworkResponse);
            NetworkResponse.Clear();
        }

        /// <summary>
        ///     Handle HTTP response header received notification
        /// </summary>
        /// <remarks>Notification is called when HTTP response header was received from the server.</remarks>
        /// <param name="response">HTTP request</param>
        protected virtual void OnReceivedResponseHeader(HttpNetworkResponse response)
        {
        }

        /// <summary>
        ///     Handle HTTP response received notification
        /// </summary>
        /// <remarks>Notification is called when HTTP response was received from the server.</remarks>
        /// <param name="response">HTTP response</param>
        protected virtual void OnReceivedResponse(HttpNetworkResponse response)
        {
        }

        /// <summary>
        ///     Handle HTTP response error notification
        /// </summary>
        /// <remarks>Notification is called when HTTP response error was received from the server.</remarks>
        /// <param name="response">HTTP response</param>
        /// <param name="error">HTTP response error</param>
        protected virtual void OnReceivedResponseError(HttpNetworkResponse response, String error)
        {
        }
    }
}