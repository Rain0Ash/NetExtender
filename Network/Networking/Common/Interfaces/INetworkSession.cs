// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Net.Sockets;

namespace NetExtender.Network.Networking.Common.Interfaces
{
    public interface INetworkSession : INetworkSender
    {
        /// <summary>
        ///     Connect the session
        /// </summary>
        /// <param name="socket">Session socket</param>
        public void Connect(Socket socket);

        /// <summary>
        ///     Receive data from the client (synchronous)
        /// </summary>
        /// <param name="buffer">Buffer to receive</param>
        /// <returns>Size of received data</returns>
        public Int64 Receive(Byte[] buffer);

        /// <summary>
        ///     Receive data from the client (synchronous)
        /// </summary>
        /// <param name="buffer">Buffer to receive</param>
        /// <param name="offset">Buffer offset</param>
        /// <param name="size">Buffer size</param>
        /// <returns>Size of received data</returns>
        public Int64 Receive(Byte[] buffer, Int64 offset, Int64 size);
    }
}