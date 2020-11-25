// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Network.Networking.SSL;

namespace NetExtender.Network.Networking.Http.Secure
{
    /// <summary>
    ///     HTTPS extended client make requests to HTTPS Web server with returning Task as a synchronization primitive.
    /// </summary>
    /// <remarks>Thread-safe.</remarks>
    public class FixedHttpsNetworkClient : HttpsNetworkClient
    {
        // Disposed flag.
        private Boolean _disposed;
        private TaskCompletionSource<HttpNetworkResponse> _tcs = new TaskCompletionSource<HttpNetworkResponse>();
        private Timer _timer;

        /// <summary>
        ///     Initialize HTTPS client with a given IP address and port number
        /// </summary>
        /// <param name="context">SSL context</param>
        /// <param name="address">IP address</param>
        /// <param name="port">Port number</param>
        public FixedHttpsNetworkClient(SslNetworkContext context, IPAddress address, Int32 port)
            : base(context, address, port)
        {
        }

        /// <summary>
        ///     Initialize HTTPS client with a given IP address and port number
        /// </summary>
        /// <param name="context">SSL context</param>
        /// <param name="address">IP address</param>
        /// <param name="port">Port number</param>
        public FixedHttpsNetworkClient(SslNetworkContext context, String address, Int32 port)
            : base(context, address, port)
        {
        }

        /// <summary>
        ///     Initialize HTTPS client with a given IP endpoint
        /// </summary>
        /// <param name="context">SSL context</param>
        /// <param name="endpoint">IP endpoint</param>
        public FixedHttpsNetworkClient(SslNetworkContext context, IPEndPoint endpoint)
            : base(context, endpoint)
        {
        }

        private void SetResultValue(HttpNetworkResponse response)
        {
            NetworkResponse = new HttpNetworkResponse();
            _tcs.SetResult(response);
            NetworkRequest.Clear();
        }

        private void SetResultError(String error)
        {
            _tcs.SetException(new Exception(error));
            NetworkRequest.Clear();
        }

        /// <summary>
        ///     Send current HTTP request
        /// </summary>
        /// <param name="timeout">Current HTTP request timeout (default is 1 minute)</param>
        /// <returns>HTTP request Task</returns>
        public Task<HttpNetworkResponse> SendRequestAsync(TimeSpan? timeout = null)
        {
            return SendRequestAsync(NetworkRequest, timeout);
        }

        /// <summary>
        ///     Send HTTP request
        /// </summary>
        /// <param name="request">HTTP request</param>
        /// <param name="timeout">HTTP request timeout (default is 1 minute)</param>
        /// <returns>HTTP request Task</returns>
        public Task<HttpNetworkResponse> SendRequestAsync(HttpNetworkRequest request, TimeSpan? timeout = null)
        {
            timeout ??= TimeSpan.FromMinutes(1);

            _tcs = new TaskCompletionSource<HttpNetworkResponse>();
            NetworkRequest = request;

            // Check if the HTTP request is valid
            if (NetworkRequest.IsEmpty || NetworkRequest.IsErrorSet)
            {
                SetResultError("Invalid HTTP request!");
                return _tcs.Task;
            }

            if (!IsHandshaked)
            {
                // Connect to the Web server
                if (!ConnectAsync())
                {
                    SetResultError("Connection failed!");
                    return _tcs.Task;
                }
            }
            else
            {
                // Send prepared HTTP request
                if (!base.SendRequestAsync())
                {
                    SetResultError("Failed to send HTTP request!");
                    return _tcs.Task;
                }
            }

            void TimeoutHandler(Object state)
            {
                // Disconnect on timeout
                OnReceivedResponseError(NetworkResponse, "Timeout!");
                NetworkResponse.Clear();
                DisconnectAsync();
            }

            // Create a new timeout timer
            _timer ??= new Timer(TimeoutHandler, null, Timeout.Infinite, Timeout.Infinite);

            // Start the timeout timer
            _timer.Change((Int32) timeout.Value.TotalMilliseconds, Timeout.Infinite);

            return _tcs.Task;
        }

        /// <summary>
        ///     Send HEAD request
        /// </summary>
        /// <param name="url">URL to request</param>
        /// <param name="timeout">Current HTTP request timeout (default is 1 minute)</param>
        /// <returns>HTTP request Task</returns>
        public Task<HttpNetworkResponse> SendHeadRequestAsync(String url, TimeSpan? timeout = null)
        {
            return SendRequestAsync(NetworkRequest.MakeHeadRequest(url), timeout);
        }

        /// <summary>
        ///     Send GET request
        /// </summary>
        /// <param name="url">URL to request</param>
        /// <param name="timeout">Current HTTP request timeout (default is 1 minute)</param>
        /// <returns>HTTP request Task</returns>
        public Task<HttpNetworkResponse> SendGetRequestAsync(String url, TimeSpan? timeout = null)
        {
            return SendRequestAsync(NetworkRequest.MakeGetRequest(url), timeout);
        }

        /// <summary>
        ///     Send POST request
        /// </summary>
        /// <param name="url">URL to request</param>
        /// <param name="content">Content</param>
        /// <param name="timeout">Current HTTP request timeout (default is 1 minute)</param>
        /// <returns>HTTP request Task</returns>
        public Task<HttpNetworkResponse> SendPostRequestAsync(String url, String content, TimeSpan? timeout = null)
        {
            return SendRequestAsync(NetworkRequest.MakePostRequest(url, content), timeout);
        }

        /// <summary>
        ///     Send PUT request
        /// </summary>
        /// <param name="url">URL to request</param>
        /// <param name="content">Content</param>
        /// <param name="timeout">Current HTTP request timeout (default is 1 minute)</param>
        /// <returns>HTTP request Task</returns>
        public Task<HttpNetworkResponse> SendPutRequestAsync(String url, String content, TimeSpan? timeout = null)
        {
            return SendRequestAsync(NetworkRequest.MakePutRequest(url, content), timeout);
        }

        /// <summary>
        ///     Send DELETE request
        /// </summary>
        /// <param name="url">URL to request</param>
        /// <param name="timeout">Current HTTP request timeout (default is 1 minute)</param>
        /// <returns>HTTP request Task</returns>
        public Task<HttpNetworkResponse> SendDeleteRequestAsync(String url, TimeSpan? timeout = null)
        {
            return SendRequestAsync(NetworkRequest.MakeDeleteRequest(url), timeout);
        }

        /// <summary>
        ///     Send OPTIONS request
        /// </summary>
        /// <param name="url">URL to request</param>
        /// <param name="timeout">Current HTTP request timeout (default is 1 minute)</param>
        /// <returns>HTTP request Task</returns>
        public Task<HttpNetworkResponse> SendOptionsRequestAsync(String url, TimeSpan? timeout = null)
        {
            return SendRequestAsync(NetworkRequest.MakeOptionsRequest(url), timeout);
        }

        /// <summary>
        ///     Send TRACE request
        /// </summary>
        /// <param name="url">URL to request</param>
        /// <param name="timeout">Current HTTP request timeout (default is 1 minute)</param>
        /// <returns>HTTP request Task</returns>
        public Task<HttpNetworkResponse> SendTraceRequestAsync(String url, TimeSpan? timeout = null)
        {
            return SendRequestAsync(NetworkRequest.MakeTraceRequest(url), timeout);
        }

        protected override void OnHandshaked()
        {
            // Send prepared HTTP request on connect
            if (NetworkRequest.IsEmpty || NetworkRequest.IsErrorSet)
            {
                return;
            }

            if (!base.SendRequestAsync())
            {
                SetResultError("Failed to send HTTP request!");
            }
        }

        protected override void OnDisconnected()
        {
            // Cancel timeout check timer
            _timer?.Change(Timeout.Infinite, Timeout.Infinite);

            base.OnDisconnected();
        }

        protected override void OnReceivedResponse(HttpNetworkResponse response)
        {
            // Cancel timeout check timer
            _timer?.Change(Timeout.Infinite, Timeout.Infinite);

            SetResultValue(response);
        }

        protected override void OnReceivedResponseError(HttpNetworkResponse response, String error)
        {
            // Cancel timeout check timer
            _timer?.Change(Timeout.Infinite, Timeout.Infinite);

            SetResultError(error);
        }

        protected override void Dispose(Boolean disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Dispose managed resources here...
                    _timer?.Dispose();
                }

                // Dispose unmanaged resources here...

                // Set large fields to null here...

                // Mark as disposed.
                _disposed = true;
            }

            // Call Dispose in the base class.
            base.Dispose(disposing);
        }

        // The derived class does not have a Finalize method
        // or a Dispose method without parameters because it inherits
        // them from the base class.
    }
}