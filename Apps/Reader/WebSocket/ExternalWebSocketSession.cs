// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Net.Sockets;
using System.Text;
using NetExtender.Apps.Domains;
using NetExtender.Network.Networking.Http;
using NetExtender.Network.Networking.WebSocket;

namespace NetExtender.Apps.Reader.WebSocket
{
    internal class ExternalWebSocketSession : WebSocketNetworkSession
    {
        private readonly ExternalReader _reader;
        
        public ExternalWebSocketSession(ExternalReader reader, WebSocketNetworkServer server)
            : base(server)
        {
            _reader = reader;
        }
        
        public override void OnWsConnected(HttpNetworkRequest request)
        {
        }

        public override void OnWsDisconnected()
        {
        }

        public override void OnWsReceived(Byte[] buffer, Int64 offset, Int64 size)
        {
            String message = Encoding.UTF8.GetString(buffer, (Int32) offset, (Int32) size);

            String[] protocol = { $"{Domain.UrlSchemeProtocolName}:\\{message}" };
            _reader.ProcessExternalInputAsync(Server, protocol);
        }

        protected override void OnError(SocketError error)
        {
        }
    }
}