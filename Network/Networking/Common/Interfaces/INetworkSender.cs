// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Net.Sockets;

namespace NetExtender.Network.Networking.Common.Interfaces
{
    public interface INetworkSender : INetwork
    {
        /// <summary>
        ///     Socket
        /// </summary>
        public Socket Socket { get; }

        /// <summary>
        ///     Number of bytes sending by the client
        /// </summary>
        public Int64 BytesSending { get; }
        
        /// <summary>
        ///     Is the client connected?
        /// </summary>
        public Boolean IsConnected { get; }

        /// <summary>
        ///     Disconnect the client (synchronous)
        /// </summary>
        /// <returns>'true' if the client was successfully disconnected, 'false' if the client is already disconnected</returns>
        public Boolean Disconnect();
        
        /// <summary>
        ///     Send data to the server (synchronous)
        /// </summary>
        /// <param name="buffer">Buffer to send</param>
        /// <returns>Size of sent data</returns>
        public Int64 Send(Byte[] buffer);
        
        /// <summary>
        ///     Send data to the server (synchronous)
        /// </summary>
        /// <param name="buffer">Buffer to send</param>
        /// <param name="offset">Buffer offset</param>
        /// <param name="size">Buffer size</param>
        /// <returns>Size of sent data</returns>
        public Int64 Send(Byte[] buffer, Int64 offset, Int64 size);

        /// <summary>
        ///     Send text to the server (synchronous)
        /// </summary>
        /// <param name="text">Text string to send</param>
        /// <returns>Size of sent text</returns>
        public Int64 Send(String text);

        /// <summary>
        ///     Send data to the server (asynchronous)
        /// </summary>
        /// <param name="buffer">Buffer to send</param>
        /// <returns>'true' if the data was successfully sent, 'false' if the client is not connected</returns>
        public Boolean SendAsync(Byte[] buffer);

        /// <summary>
        ///     Send data to the server (asynchronous)
        /// </summary>
        /// <param name="buffer">Buffer to send</param>
        /// <param name="offset">Buffer offset</param>
        /// <param name="size">Buffer size</param>
        /// <returns>'true' if the data was successfully sent, 'false' if the client is not connected</returns>
        public Boolean SendAsync(Byte[] buffer, Int64 offset, Int64 size);
        
        /// <summary>
        ///     Send text to the server (asynchronous)
        /// </summary>
        /// <param name="text">Text string to send</param>
        /// <returns>'true' if the text was successfully sent, 'false' if the client is not connected</returns>
        public Boolean SendAsync(String text);

        /// <summary>
        ///     Receive data from the server (asynchronous)
        /// </summary>
        public void ReceiveAsync();
    }
}