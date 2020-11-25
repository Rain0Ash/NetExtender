using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Sockets;
using System.Text;
using NetExtender.Network.Networking.Common;
using NetExtender.Network.Networking.Http;
using NetExtender.Network.Networking.Http.Secure;
using NetExtender.Network.Networking.WebSocket.Interfaces;

namespace NetExtender.Network.Networking.WebSocket.Secure
{
    /// <summary>
    ///     WebSocket secure session
    /// </summary>
    /// <remarks> WebSocket secure session is used to read and write data from the connected WebSocket client. Thread-safe.</remarks>
    [SuppressMessage("ReSharper", "InconsistentlySynchronizedField")]
    public class WebSocketSecureNetworkSession : HttpsNetworkSession, IWebSocketNetwork
    {
        internal readonly WebSocketNetwork webSocketNetwork;

        public WebSocketSecureNetworkSession(WebSocketSecureNetworkServer server)
            : base(server)
        {
            webSocketNetwork = new WebSocketNetwork(this);
        }

        /// <summary>
        ///     Handle WebSocket client connecting notification
        /// </summary>
        /// <remarks>
        ///     Notification is called when WebSocket client is connecting to the server.You can handle the connection and
        ///     change WebSocket upgrade HTTP request by providing your own headers.
        /// </remarks>
        /// <param name="request">WebSocket upgrade HTTP request</param>
        public virtual void OnWsConnecting(HttpNetworkRequest request)
        {
        }

        /// <summary>
        ///     Handle WebSocket client connected notification
        /// </summary>
        /// <param name="response">WebSocket upgrade HTTP response</param>
        public virtual void OnWsConnected(HttpNetworkResponse response)
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
        public virtual Boolean OnWsConnecting(HttpNetworkRequest request, HttpNetworkResponse response)
        {
            return true;
        }

        /// <summary>
        ///     Handle WebSocket server session connected notification
        /// </summary>
        /// <param name="request">WebSocket upgrade HTTP request</param>
        public virtual void OnWsConnected(HttpNetworkRequest request)
        {
        }

        /// <summary>
        ///     Handle WebSocket client disconnected notification
        /// </summary>
        public virtual void OnWsDisconnected()
        {
        }

        /// <summary>
        ///     Handle WebSocket received notification
        /// </summary>
        /// <param name="buffer">Received buffer</param>
        /// <param name="offset">Received buffer offset</param>
        /// <param name="size">Received buffer size</param>
        public virtual void OnWsReceived(Byte[] buffer, Int64 offset, Int64 size)
        {
        }

        /// <summary>
        ///     Handle WebSocket client close notification
        /// </summary>
        /// <param name="buffer">Received buffer</param>
        /// <param name="offset">Received buffer offset</param>
        /// <param name="size">Received buffer size</param>
        public virtual void OnWsClose(Byte[] buffer, Int64 offset, Int64 size)
        {
            Close(1000);
        }

        /// <summary>
        ///     Handle WebSocket ping notification
        /// </summary>
        /// <param name="buffer">Received buffer</param>
        /// <param name="offset">Received buffer offset</param>
        /// <param name="size">Received buffer size</param>
        public virtual void OnWsPing(Byte[] buffer, Int64 offset, Int64 size)
        {
            SendPongAsync(buffer, offset, size);
        }

        /// <summary>
        ///     Handle WebSocket pong notification
        /// </summary>
        /// <param name="buffer">Received buffer</param>
        /// <param name="offset">Received buffer offset</param>
        /// <param name="size">Received buffer size</param>
        public virtual void OnWsPong(Byte[] buffer, Int64 offset, Int64 size)
        {
        }

        /// <summary>
        ///     Handle WebSocket error notification
        /// </summary>
        /// <param name="error">Error message</param>
        public virtual void OnWsError(String error)
        {
            OnError(SocketError.SocketError);
        }

        /// <summary>
        ///     Handle socket error notification
        /// </summary>
        /// <param name="error">Socket error</param>
        public virtual void OnWsError(SocketError error)
        {
            OnError(error);
        }

        /// <summary>
        ///     Send WebSocket server upgrade response
        /// </summary>
        /// <param name="response">WebSocket upgrade HTTP response</param>
        public new virtual void SendResponse(HttpNetworkResponse response)
        {
            SendResponseAsync(response);
        }

        // WebSocket connection methods
        public virtual Boolean Close(Int32 status)
        {
            SendCloseAsync(status, null, 0, 0);
            base.Disconnect();
            return true;
        }

        public Int64 SendText(Byte[] buffer, Int64 offset, Int64 size)
        {
            lock (webSocketNetwork.WsSendLock)
            {
                webSocketNetwork.PrepareSendFrame(WebSocketNetwork.WsFin | WebSocketNetwork.WsText, false, buffer, offset, size);
                return base.Send(webSocketNetwork.WsSendBuffer.ToArray());
            }
        }

        public Int64 SendText(String text)
        {
            lock (webSocketNetwork.WsSendLock)
            {
                Byte[] data = Encoding.UTF8.GetBytes(text);
                webSocketNetwork.PrepareSendFrame(WebSocketNetwork.WsFin | WebSocketNetwork.WsText, false, data, 0, data.Length);
                return base.Send(webSocketNetwork.WsSendBuffer.ToArray());
            }
        }

        public Boolean SendTextAsync(Byte[] buffer, Int64 offset, Int64 size)
        {
            lock (webSocketNetwork.WsSendLock)
            {
                webSocketNetwork.PrepareSendFrame(WebSocketNetwork.WsFin | WebSocketNetwork.WsText, false, buffer, offset, size);
                return base.SendAsync(webSocketNetwork.WsSendBuffer.ToArray());
            }
        }

        public Boolean SendTextAsync(String text)
        {
            lock (webSocketNetwork.WsSendLock)
            {
                Byte[] data = Encoding.UTF8.GetBytes(text);
                webSocketNetwork.PrepareSendFrame(WebSocketNetwork.WsFin | WebSocketNetwork.WsText, false, data, 0, data.Length);
                return base.SendAsync(webSocketNetwork.WsSendBuffer.ToArray());
            }
        }

        public Int64 SendBinary(Byte[] buffer, Int64 offset, Int64 size)
        {
            lock (webSocketNetwork.WsSendLock)
            {
                webSocketNetwork.PrepareSendFrame(WebSocketNetwork.WsFin | WebSocketNetwork.WsBinary, false, buffer, offset, size);
                return base.Send(webSocketNetwork.WsSendBuffer.ToArray());
            }
        }

        public Int64 SendBinary(String text)
        {
            lock (webSocketNetwork.WsSendLock)
            {
                Byte[] data = Encoding.UTF8.GetBytes(text);
                webSocketNetwork.PrepareSendFrame(WebSocketNetwork.WsFin | WebSocketNetwork.WsBinary, false, data, 0, data.Length);
                return base.Send(webSocketNetwork.WsSendBuffer.ToArray());
            }
        }

        public Boolean SendBinaryAsync(Byte[] buffer, Int64 offset, Int64 size)
        {
            lock (webSocketNetwork.WsSendLock)
            {
                webSocketNetwork.PrepareSendFrame(WebSocketNetwork.WsFin | WebSocketNetwork.WsBinary, false, buffer, offset, size);
                return base.SendAsync(webSocketNetwork.WsSendBuffer.ToArray());
            }
        }

        public Boolean SendBinaryAsync(String text)
        {
            lock (webSocketNetwork.WsSendLock)
            {
                Byte[] data = Encoding.UTF8.GetBytes(text);
                webSocketNetwork.PrepareSendFrame(WebSocketNetwork.WsFin | WebSocketNetwork.WsBinary, false, data, 0, data.Length);
                return base.SendAsync(webSocketNetwork.WsSendBuffer.ToArray());
            }
        }

        public Int64 SendClose(Int32 status, Byte[] buffer, Int64 offset, Int64 size)
        {
            lock (webSocketNetwork.WsSendLock)
            {
                webSocketNetwork.PrepareSendFrame(WebSocketNetwork.WsFin | WebSocketNetwork.WsClose, false, buffer, offset, size, status);
                return base.Send(webSocketNetwork.WsSendBuffer.ToArray());
            }
        }

        public Int64 SendClose(Int32 status, String text)
        {
            lock (webSocketNetwork.WsSendLock)
            {
                Byte[] data = Encoding.UTF8.GetBytes(text);
                webSocketNetwork.PrepareSendFrame(WebSocketNetwork.WsFin | WebSocketNetwork.WsClose, false, data, 0, data.Length, status);
                return base.Send(webSocketNetwork.WsSendBuffer.ToArray());
            }
        }

        public Boolean SendCloseAsync(Int32 status, Byte[] buffer, Int64 offset, Int64 size)
        {
            lock (webSocketNetwork.WsSendLock)
            {
                webSocketNetwork.PrepareSendFrame(WebSocketNetwork.WsFin | WebSocketNetwork.WsClose, false, buffer, offset, size, status);
                return base.SendAsync(webSocketNetwork.WsSendBuffer.ToArray());
            }
        }

        public Boolean SendCloseAsync(Int32 status, String text)
        {
            lock (webSocketNetwork.WsSendLock)
            {
                Byte[] data = Encoding.UTF8.GetBytes(text);
                webSocketNetwork.PrepareSendFrame(WebSocketNetwork.WsFin | WebSocketNetwork.WsClose, false, data, 0, data.Length, status);
                return base.SendAsync(webSocketNetwork.WsSendBuffer.ToArray());
            }
        }

        public Int64 SendPing(Byte[] buffer, Int64 offset, Int64 size)
        {
            lock (webSocketNetwork.WsSendLock)
            {
                webSocketNetwork.PrepareSendFrame(WebSocketNetwork.WsFin | WebSocketNetwork.WsPing, false, buffer, offset, size);
                return base.Send(webSocketNetwork.WsSendBuffer.ToArray());
            }
        }

        public Int64 SendPing(String text)
        {
            lock (webSocketNetwork.WsSendLock)
            {
                Byte[] data = Encoding.UTF8.GetBytes(text);
                webSocketNetwork.PrepareSendFrame(WebSocketNetwork.WsFin | WebSocketNetwork.WsPing, false, data, 0, data.Length);
                return base.Send(webSocketNetwork.WsSendBuffer.ToArray());
            }
        }

        public Boolean SendPingAsync(Byte[] buffer, Int64 offset, Int64 size)
        {
            lock (webSocketNetwork.WsSendLock)
            {
                webSocketNetwork.PrepareSendFrame(WebSocketNetwork.WsFin | WebSocketNetwork.WsPing, false, buffer, offset, size);
                return base.SendAsync(webSocketNetwork.WsSendBuffer.ToArray());
            }
        }

        public Boolean SendPingAsync(String text)
        {
            lock (webSocketNetwork.WsSendLock)
            {
                Byte[] data = Encoding.UTF8.GetBytes(text);
                webSocketNetwork.PrepareSendFrame(WebSocketNetwork.WsFin | WebSocketNetwork.WsPing, false, data, 0, data.Length);
                return base.SendAsync(webSocketNetwork.WsSendBuffer.ToArray());
            }
        }

        public Int64 SendPong(Byte[] buffer, Int64 offset, Int64 size)
        {
            lock (webSocketNetwork.WsSendLock)
            {
                webSocketNetwork.PrepareSendFrame(WebSocketNetwork.WsFin | WebSocketNetwork.WsPong, false, buffer, offset, size);
                return base.Send(webSocketNetwork.WsSendBuffer.ToArray());
            }
        }

        public Int64 SendPong(String text)
        {
            lock (webSocketNetwork.WsSendLock)
            {
                Byte[] data = Encoding.UTF8.GetBytes(text);
                webSocketNetwork.PrepareSendFrame(WebSocketNetwork.WsFin | WebSocketNetwork.WsPong, false, data, 0, data.Length);
                return base.Send(webSocketNetwork.WsSendBuffer.ToArray());
            }
        }

        public Boolean SendPongAsync(Byte[] buffer, Int64 offset, Int64 size)
        {
            lock (webSocketNetwork.WsSendLock)
            {
                webSocketNetwork.PrepareSendFrame(WebSocketNetwork.WsFin | WebSocketNetwork.WsPong, false, buffer, offset, size);
                return base.SendAsync(webSocketNetwork.WsSendBuffer.ToArray());
            }
        }

        public Boolean SendPongAsync(String text)
        {
            lock (webSocketNetwork.WsSendLock)
            {
                Byte[] data = Encoding.UTF8.GetBytes(text);
                webSocketNetwork.PrepareSendFrame(WebSocketNetwork.WsFin | WebSocketNetwork.WsPong, false, data, 0, data.Length);
                return base.SendAsync(webSocketNetwork.WsSendBuffer.ToArray());
            }
        }

        public String ReceiveText()
        {
            NetworkDataBuffer result = new NetworkDataBuffer();

            if (!webSocketNetwork.WsHandshaked)
            {
                return result.ExtractString(0, result.Data.Length);
            }

            NetworkDataBuffer cache = new NetworkDataBuffer();

            // Receive WebSocket frame data
            while (!webSocketNetwork.WsReceived)
            {
                Int32 required = webSocketNetwork.RequiredReceiveFrameSize();
                cache.Resize(required);
                Int32 received = (Int32) base.Receive(cache.Data, 0, required);
                if (received != required)
                {
                    return result.ExtractString(0, result.Data.Length);
                }

                webSocketNetwork.PrepareReceiveFrame(cache.Data, 0, received);
            }

            // Copy WebSocket frame data
            result.Append(webSocketNetwork.WsReceiveBuffer.ToArray(), webSocketNetwork.WsHeaderSize, webSocketNetwork.WsHeaderSize + webSocketNetwork.WsPayloadSize);
            webSocketNetwork.PrepareReceiveFrame(null, 0, 0);
            return result.ExtractString(0, result.Data.Length);
        }

        public NetworkDataBuffer ReceiveBinary()
        {
            NetworkDataBuffer result = new NetworkDataBuffer();

            if (!webSocketNetwork.WsHandshaked)
            {
                return result;
            }

            NetworkDataBuffer cache = new NetworkDataBuffer();

            // Receive WebSocket frame data
            while (!webSocketNetwork.WsReceived)
            {
                Int32 required = webSocketNetwork.RequiredReceiveFrameSize();
                cache.Resize(required);
                Int32 received = (Int32) base.Receive(cache.Data, 0, required);
                if (received != required)
                {
                    return result;
                }

                webSocketNetwork.PrepareReceiveFrame(cache.Data, 0, received);
            }

            // Copy WebSocket frame data
            result.Append(webSocketNetwork.WsReceiveBuffer.ToArray(), webSocketNetwork.WsHeaderSize, webSocketNetwork.WsHeaderSize + webSocketNetwork.WsPayloadSize);
            webSocketNetwork.PrepareReceiveFrame(null, 0, 0);
            return result;
        }

        protected override void OnDisconnected()
        {
            // Disconnect WebSocket
            if (webSocketNetwork.WsHandshaked)
            {
                webSocketNetwork.WsHandshaked = false;
                OnWsDisconnected();
            }

            // Reset WebSocket upgrade HTTP request and response
            NetworkRequest.Clear();
            NetworkResponse.Clear();

            // Clear WebSocket send/receive buffers
            webSocketNetwork.ClearWsBuffers();
        }

        protected override void OnReceived(Byte[] buffer, Int64 offset, Int64 size)
        {
            // Check for WebSocket handshaked status
            if (webSocketNetwork.WsHandshaked)
            {
                // Prepare receive frame
                webSocketNetwork.PrepareReceiveFrame(buffer, offset, size);
                return;
            }

            base.OnReceived(buffer, offset, size);
        }

        protected override void OnReceivedRequestHeader(HttpNetworkRequest request)
        {
            // Check for WebSocket handshaked status
            if (webSocketNetwork.WsHandshaked)
            {
                return;
            }

            // Try to perform WebSocket upgrade
            if (!webSocketNetwork.PerformServerUpgrade(request, NetworkResponse))
            {
                base.OnReceivedRequestHeader(request);
            }
        }

        protected override void OnReceivedRequest(HttpNetworkRequest request)
        {
            // Check for WebSocket handshaked status
            if (webSocketNetwork.WsHandshaked)
            {
                // Prepare receive frame from the remaining request body
                String body = NetworkRequest.Body;
                Byte[] data = Encoding.UTF8.GetBytes(body);
                webSocketNetwork.PrepareReceiveFrame(data, 0, data.Length);
                return;
            }

            base.OnReceivedRequest(request);
        }

        protected override void OnReceivedRequestError(HttpNetworkRequest request, String error)
        {
            // Check for WebSocket handshaked status
            if (webSocketNetwork.WsHandshaked)
            {
                OnError(new SocketError());
                return;
            }

            base.OnReceivedRequestError(request, error);
        }
    }
}