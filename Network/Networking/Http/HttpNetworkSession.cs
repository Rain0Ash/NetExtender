using System;
using NetExtender.Network.Networking.Common;
using NetExtender.Network.Networking.TCP;

namespace NetExtender.Network.Networking.Http
{
    /// <summary>
    ///     HTTP session is used to receive/send HTTP requests/responses from the connected HTTP client.
    /// </summary>
    /// <remarks>Thread-safe.</remarks>
    public class HttpNetworkSession : TcpNetworkSession
    {
        public HttpNetworkSession(HttpNetworkServer server)
            : base(server)
        {
            Cache = server.Cache;
            NetworkRequest = new HttpNetworkRequest();
            NetworkResponse = new HttpNetworkResponse();
        }

        /// <summary>
        ///     Get the static content cache
        /// </summary>
        public NetworkFileCache Cache { get; }

        /// <summary>
        ///     Get the HTTP request
        /// </summary>
        protected HttpNetworkRequest NetworkRequest { get; }

        /// <summary>
        ///     Get the HTTP response
        /// </summary>
        public HttpNetworkResponse NetworkResponse { get; }

        private void OnReceivedRequestInternal(HttpNetworkRequest request)
        {
            // Try to get the cached response
            if (request.Method == "GET")
            {
                Tuple<Boolean, Byte[]> response = Cache.Find(request.Url);
                if (response.Item1)
                {
                    SendAsync(response.Item2);
                    return;
                }
            }

            // Process the request
            OnReceivedRequest(request);
        }

        /// <summary>
        ///     Send the current HTTP response (synchronous)
        /// </summary>
        /// <returns>Size of sent data</returns>
        public Int64 SendResponse()
        {
            return SendResponse(NetworkResponse);
        }

        /// <summary>
        ///     Send the HTTP response (synchronous)
        /// </summary>
        /// <param name="response">HTTP response</param>
        /// <returns>Size of sent data</returns>
        public Int64 SendResponse(HttpNetworkResponse response)
        {
            return Send(response.Cache.Data, response.Cache.Offset, response.Cache.Size);
        }

        /// <summary>
        ///     Send the HTTP response body (synchronous)
        /// </summary>
        /// <param name="body">HTTP response body</param>
        /// <returns>Size of sent data</returns>
        public Int64 SendResponseBody(String body)
        {
            return Send(body);
        }

        /// <summary>
        ///     Send the HTTP response body (synchronous)
        /// </summary>
        /// <param name="buffer">HTTP response body buffer</param>
        /// <returns>Size of sent data</returns>
        public Int64 SendResponseBody(Byte[] buffer)
        {
            return Send(buffer);
        }

        /// <summary>
        ///     Send the HTTP response body (synchronous)
        /// </summary>
        /// <param name="buffer">HTTP response body buffer</param>
        /// <param name="offset">HTTP response body buffer offset</param>
        /// <param name="size">HTTP response body size</param>
        /// <returns>Size of sent data</returns>
        public Int64 SendResponseBody(Byte[] buffer, Int64 offset, Int64 size)
        {
            return Send(buffer, offset, size);
        }

        /// <summary>
        ///     Send the current HTTP response (asynchronous)
        /// </summary>
        /// <returns>'true' if the current HTTP response was successfully sent, 'false' if the session is not connected</returns>
        public Boolean SendResponseAsync()
        {
            return SendResponseAsync(NetworkResponse);
        }

        /// <summary>
        ///     Send the HTTP response (asynchronous)
        /// </summary>
        /// <param name="response">HTTP response</param>
        /// <returns>'true' if the current HTTP response was successfully sent, 'false' if the session is not connected</returns>
        public Boolean SendResponseAsync(HttpNetworkResponse response)
        {
            return SendAsync(response.Cache.Data, response.Cache.Offset, response.Cache.Size);
        }

        /// <summary>
        ///     Send the HTTP response body (asynchronous)
        /// </summary>
        /// <param name="body">HTTP response body</param>
        /// <returns>'true' if the HTTP response body was successfully sent, 'false' if the session is not connected</returns>
        public Boolean SendResponseBodyAsync(String body)
        {
            return SendAsync(body);
        }

        /// <summary>
        ///     Send the HTTP response body (asynchronous)
        /// </summary>
        /// <param name="buffer">HTTP response body buffer</param>
        /// <returns>'true' if the HTTP response body was successfully sent, 'false' if the session is not connected</returns>
        public Boolean SendResponseBodyAsync(Byte[] buffer)
        {
            return SendAsync(buffer);
        }

        /// <summary>
        ///     Send the HTTP response body (asynchronous)
        /// </summary>
        /// <param name="buffer">HTTP response body buffer</param>
        /// <param name="offset">HTTP response body buffer offset</param>
        /// <param name="size">HTTP response body size</param>
        /// <returns>'true' if the HTTP response body was successfully sent, 'false' if the session is not connected</returns>
        public Boolean SendResponseBodyAsync(Byte[] buffer, Int64 offset, Int64 size)
        {
            return SendAsync(buffer, offset, size);
        }

        protected override void OnReceived(Byte[] buffer, Int64 offset, Int64 size)
        {
            // Receive HTTP request header
            if (NetworkRequest.IsPendingHeader())
            {
                if (NetworkRequest.ReceiveHeader(buffer, (Int32) offset, (Int32) size))
                {
                    OnReceivedRequestHeader(NetworkRequest);
                }

                size = 0;
            }

            // Check for HTTP request error
            if (NetworkRequest.IsErrorSet)
            {
                OnReceivedRequestError(NetworkRequest, "Invalid HTTP request!");
                NetworkRequest.Clear();
                Disconnect();
                return;
            }

            // Receive HTTP request body
            if (NetworkRequest.ReceiveBody(buffer, (Int32) offset, (Int32) size))
            {
                OnReceivedRequestInternal(NetworkRequest);
                NetworkRequest.Clear();
                return;
            }

            // Check for HTTP request error
            if (!NetworkRequest.IsErrorSet)
            {
                return;
            }

            OnReceivedRequestError(NetworkRequest, "Invalid HTTP request!");
            NetworkRequest.Clear();
            Disconnect();
        }

        protected override void OnDisconnected()
        {
            // Receive HTTP request body
            if (!NetworkRequest.IsPendingBody())
            {
                return;
            }

            OnReceivedRequestInternal(NetworkRequest);
            NetworkRequest.Clear();
        }

        /// <summary>
        ///     Handle HTTP request header received notification
        /// </summary>
        /// <remarks>Notification is called when HTTP request header was received from the client.</remarks>
        /// <param name="request">HTTP request</param>
        protected virtual void OnReceivedRequestHeader(HttpNetworkRequest request)
        {
        }

        /// <summary>
        ///     Handle HTTP request received notification
        /// </summary>
        /// <remarks>Notification is called when HTTP request was received from the client.</remarks>
        /// <param name="request">HTTP request</param>
        protected virtual void OnReceivedRequest(HttpNetworkRequest request)
        {
        }

        /// <summary>
        ///     Handle HTTP request error notification
        /// </summary>
        /// <remarks>Notification is called when HTTP request error was received from the client.</remarks>
        /// <param name="request">HTTP request</param>
        /// <param name="error">HTTP request error</param>
        // ReSharper disable once UnusedParameter.Global
        protected virtual void OnReceivedRequestError(HttpNetworkRequest request, String error)
        {
        }
    }
}