// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Net;
using NetExtender.Network.Networking.TCP;
using NetExtender.Network.Networking.WebSocket;

namespace NetExtender.Apps.Reader.WebSocket
{
    internal class ExternalWebSocketNetworkServer : WebSocketNetworkServer
    {
        public const UInt16 DefaultPort = 60000;
        
        private readonly ExternalReader _reader;
        
        public ExternalWebSocketNetworkServer(ExternalReader reader, Int32 port)
            : base(IPAddress.Loopback, port)
        {
            _reader = reader;
        }

        protected override TcpNetworkSession CreateSession() { return new ExternalWebSocketSession(_reader, this); }
    }
}