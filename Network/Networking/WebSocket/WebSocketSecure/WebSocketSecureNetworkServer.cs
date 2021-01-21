using System;
using System.Net;
using System.Text;
using NetExtender.Network.Networking.Http.Secure;
using NetExtender.Network.Networking.SSL;
using NetExtender.Network.Networking.WebSocket.Interfaces;

namespace NetExtender.Network.Networking.WebSocket.Secure
{
    /// <summary>
    ///     WebSocket secure server
    /// </summary>
    /// <remarks> WebSocket secure server is used to communicate with clients using WebSocket protocol. Thread-safe.</remarks>
    public class WebSocketSecureNetworkServer : HttpsNetworkServer, IWebSocketNetwork
    {
        internal readonly WebSocketNetwork webSocketNetwork;

        /// <summary>
        ///     Initialize WebSocket server with a given IP address and port number
        /// </summary>
        /// <param name="context">SSL context</param>
        /// <param name="address">IP address</param>
        /// <param name="port">Port number</param>
        public WebSocketSecureNetworkServer(SslNetworkContext context, IPAddress address, Int32 port)
            : base(context, address, port)
        {
            webSocketNetwork = new WebSocketNetwork(this);
        }

        /// <summary>
        ///     Initialize WebSocket server with a given IP address and port number
        /// </summary>
        /// <param name="context">SSL context</param>
        /// <param name="address">IP address</param>
        /// <param name="port">Port number</param>
        public WebSocketSecureNetworkServer(SslNetworkContext context, String address, Int32 port)
            : base(context, address, port)
        {
            webSocketNetwork = new WebSocketNetwork(this);
        }

        /// <summary>
        ///     Initialize WebSocket server with a given IP endpoint
        /// </summary>
        /// <param name="context">SSL context</param>
        /// <param name="endpoint">IP endpoint</param>
        public WebSocketSecureNetworkServer(SslNetworkContext context, IPEndPoint endpoint)
            : base(context, endpoint)
        {
            webSocketNetwork = new WebSocketNetwork(this);
        }

        public virtual Boolean CloseAll(Int32 status)
        {
            lock (webSocketNetwork.WsSendLock)
            {
                webSocketNetwork.PrepareSendFrame(WebSocketNetwork.WsFin | WebSocketNetwork.WsClose, false, null, 0, 0, status);
                return Multicast(webSocketNetwork.WsSendBuffer.ToArray()) && base.DisconnectAll();
            }
        }

        public override Boolean Multicast(Byte[] buffer, Int64 offset, Int64 size)
        {
            if (!IsStarted)
            {
                return false;
            }

            if (size == 0)
            {
                return true;
            }

            // Multicast data to all WebSocket sessions
            foreach (SslNetworkSession session in Sessions.Values)
            {
                if (session is not WebSocketSecureNetworkSession wssSession)
                {
                    continue;
                }

                if (wssSession.webSocketNetwork.WsHandshaked)
                {
                    wssSession.SendAsync(buffer, offset, size);
                }
            }

            return true;
        }

        protected override SslNetworkSession CreateSession()
        {
            return new WebSocketSecureNetworkSession(this);
        }

        public Boolean MulticastText(Byte[] buffer, Int64 offset, Int64 size)
        {
            lock (webSocketNetwork.WsSendLock)
            {
                webSocketNetwork.PrepareSendFrame(WebSocketNetwork.WsFin | WebSocketNetwork.WsText, false, buffer, offset, size);
                return Multicast(webSocketNetwork.WsSendBuffer.ToArray());
            }
        }

        public Boolean MulticastText(String text)
        {
            lock (webSocketNetwork.WsSendLock)
            {
                Byte[] data = Encoding.UTF8.GetBytes(text);
                webSocketNetwork.PrepareSendFrame(WebSocketNetwork.WsFin | WebSocketNetwork.WsText, false, data, 0, data.Length);
                return Multicast(webSocketNetwork.WsSendBuffer.ToArray());
            }
        }

        public Boolean MulticastBinary(Byte[] buffer, Int64 offset, Int64 size)
        {
            lock (webSocketNetwork.WsSendLock)
            {
                webSocketNetwork.PrepareSendFrame(WebSocketNetwork.WsFin | WebSocketNetwork.WsBinary, false, buffer, offset, size);
                return Multicast(webSocketNetwork.WsSendBuffer.ToArray());
            }
        }

        public Boolean MulticastBinary(String text)
        {
            lock (webSocketNetwork.WsSendLock)
            {
                Byte[] data = Encoding.UTF8.GetBytes(text);
                webSocketNetwork.PrepareSendFrame(WebSocketNetwork.WsFin | WebSocketNetwork.WsBinary, false, data, 0, data.Length);
                return Multicast(webSocketNetwork.WsSendBuffer.ToArray());
            }
        }

        public Boolean SendPing(Byte[] buffer, Int64 offset, Int64 size)
        {
            lock (webSocketNetwork.WsSendLock)
            {
                webSocketNetwork.PrepareSendFrame(WebSocketNetwork.WsFin | WebSocketNetwork.WsPing, false, buffer, offset, size);
                return Multicast(webSocketNetwork.WsSendBuffer.ToArray());
            }
        }

        public Boolean SendPing(String text)
        {
            lock (webSocketNetwork.WsSendLock)
            {
                Byte[] data = Encoding.UTF8.GetBytes(text);
                webSocketNetwork.PrepareSendFrame(WebSocketNetwork.WsFin | WebSocketNetwork.WsPing, false, data, 0, data.Length);
                return Multicast(webSocketNetwork.WsSendBuffer.ToArray());
            }
        }

        public Boolean SendPong(Byte[] buffer, Int64 offset, Int64 size)
        {
            lock (webSocketNetwork.WsSendLock)
            {
                webSocketNetwork.PrepareSendFrame(WebSocketNetwork.WsFin | WebSocketNetwork.WsPong, false, buffer, offset, size);
                return Multicast(webSocketNetwork.WsSendBuffer.ToArray());
            }
        }

        public Boolean SendPong(String text)
        {
            lock (webSocketNetwork.WsSendLock)
            {
                Byte[] data = Encoding.UTF8.GetBytes(text);
                webSocketNetwork.PrepareSendFrame(WebSocketNetwork.WsFin | WebSocketNetwork.WsPong, false, data, 0, data.Length);
                return Multicast(webSocketNetwork.WsSendBuffer.ToArray());
            }
        }
    }
}