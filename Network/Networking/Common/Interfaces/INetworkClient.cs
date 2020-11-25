// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Network.Networking.Common.Interfaces
{
    public interface INetworkClient : INetworkConnect, INetworkSender
    {
        /// <summary>
        ///     Option: dual mode socket
        /// </summary>
        /// <remarks>
        ///     Specifies whether the Socket is a dual-mode socket used for both IPv4 and IPv6.
        ///     Will work only if socket is bound on IPv6 address.
        /// </remarks>
        public Boolean OptionDualMode { get; set; }

        /// <summary>
        ///     Connect the client (synchronous)
        /// </summary>
        /// <remarks>
        ///     Please note that synchronous connect will not receive data automatically!
        ///     You should use Receive() or ReceiveAsync() method manually after successful connection.
        /// </remarks>
        /// <returns>'true' if the client was successfully connected, 'false' if the client failed to connect</returns>
        public Boolean Connect();

        /// <summary>
        ///     Reconnect the client (synchronous)
        /// </summary>
        /// <returns>'true' if the client was successfully reconnected, 'false' if the client is already reconnected</returns>
        public Boolean Reconnect();
    }
}