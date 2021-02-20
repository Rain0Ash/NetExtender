using System;
using System.Net.Sockets;
using NetExtender.Network.Networking.Http;

namespace NetExtender.Network.Networking.WebSocket.Interfaces
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("ReSharper", "UnusedParameter.Global")]
    public interface IWebSocketNetwork
    {
        /// <summary>
        ///     Handle WebSocket client connecting notification
        /// </summary>
        /// <remarks>
        ///     Notification is called when WebSocket client is connecting to the server.You can handle the connection and
        ///     change WebSocket upgrade HTTP request by providing your own headers.
        /// </remarks>
        /// <param name="request">WebSocket upgrade HTTP request</param>
        public void OnWsConnecting(HttpNetworkRequest request)
        {
        }

        /// <summary>
        ///     Handle WebSocket client connected notification
        /// </summary>
        /// <param name="response">WebSocket upgrade HTTP response</param>
        public void OnWsConnected(HttpNetworkResponse response)
        {
        }

        /// <summary>
        ///     Handle WebSocket server session validating notification
        /// </summary>
        /// <remarks>
        ///     Notification is called when WebSocket client is connecting to the server.You can handle the connection and
        ///     validate WebSocket upgrade HTTP request.
        /// </remarks>
        /// <param name="request">WebSocket upgrade HTTP request</param>
        /// <param name="response">WebSocket upgrade HTTP response</param>
        /// <returns>return 'true' if the WebSocket update request is valid, 'false' if the WebSocket update request is not valid</returns>
        public Boolean OnWsConnecting(HttpNetworkRequest request, HttpNetworkResponse response)
        {
            return true;
        }

        /// <summary>
        ///     Handle WebSocket server session connected notification
        /// </summary>
        /// <param name="request">WebSocket upgrade HTTP request</param>
        public void OnWsConnected(HttpNetworkRequest request)
        {
        }

        /// <summary>
        ///     Handle WebSocket client disconnected notification
        /// </summary>
        public void OnWsDisconnected()
        {
        }

        /// <summary>
        ///     Handle WebSocket received notification
        /// </summary>
        /// <param name="buffer">Received buffer</param>
        /// <param name="offset">Received buffer offset</param>
        /// <param name="size">Received buffer size</param>
        public void OnWsReceived(Byte[] buffer, Int64 offset, Int64 size)
        {
        }

        /// <summary>
        ///     Handle WebSocket client close notification
        /// </summary>
        /// <param name="buffer">Received buffer</param>
        /// <param name="offset">Received buffer offset</param>
        /// <param name="size">Received buffer size</param>
        public void OnWsClose(Byte[] buffer, Int64 offset, Int64 size)
        {
        }

        /// <summary>
        ///     Handle WebSocket ping notification
        /// </summary>
        /// <param name="buffer">Received buffer</param>
        /// <param name="offset">Received buffer offset</param>
        /// <param name="size">Received buffer size</param>
        public void OnWsPing(Byte[] buffer, Int64 offset, Int64 size)
        {
        }

        /// <summary>
        ///     Handle WebSocket pong notification
        /// </summary>
        /// <param name="buffer">Received buffer</param>
        /// <param name="offset">Received buffer offset</param>
        /// <param name="size">Received buffer size</param>
        public void OnWsPong(Byte[] buffer, Int64 offset, Int64 size)
        {
        }

        /// <summary>
        ///     Handle WebSocket error notification
        /// </summary>
        /// <param name="error">Error message</param>
        public void OnWsError(String error)
        {
        }

        /// <summary>
        ///     Handle socket error notification
        /// </summary>
        /// <param name="error">Socket error</param>
        public void OnWsError(SocketError error)
        {
        }

        /// <summary>
        ///     Send WebSocket server upgrade response
        /// </summary>
        /// <param name="response">WebSocket upgrade HTTP response</param>
        public void SendResponse(HttpNetworkResponse response)
        {
        }
    }
}