// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Network.Networking.Common.Interfaces
{
    public interface INetwork : IDisposable
    {
        /// <summary>
        ///     Client Id
        /// </summary>
        public Guid Id { get; }
        
        /// <summary>
        ///     Number of bytes pending sent by the client
        /// </summary>
        public Int64 BytesPending { get; }
        
        /// <summary>
        ///     Number of bytes sent by the client
        /// </summary>
        public Int64 BytesSent { get; }
        
        /// <summary>
        ///     Number of bytes received by the client
        /// </summary>
        public Int64 BytesReceived { get; }
        
        /// <summary>
        ///     Option: receive buffer size
        /// </summary>
        public Int32 OptionReceiveBufferSize { get; set; }

        /// <summary>
        ///     Option: send buffer size
        /// </summary>
        public Int32 OptionSendBufferSize { get; set; }
        
        /// <summary>
        ///     Disposed flag
        /// </summary>
        public Boolean IsDisposed { get; }

        /// <summary>
        ///     Client socket disposed flag
        /// </summary>
        public Boolean IsSocketDisposed { get; }
    }
}