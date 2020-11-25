// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using NetExtender.Network.Networking.Common.Interfaces;

namespace NetExtender.Network.Networking.Common
{
    public abstract class NetworkServer : INetworkServer
    {
        /// <summary>
        ///     Server Id
        /// </summary>
        public Guid Id { get; }
        
        /// <summary>
        ///     IP endpoint
        /// </summary>
        public IPEndPoint Endpoint { get; protected set; }
        
        /// <summary>
        ///     Number of bytes pending sent by the server
        /// </summary>
        public Int64 BytesPending
        {
            get
            {
                return bytesPending;
            }
            protected set
            {
                bytesPending = value;
            }
        }

        /// <summary>
        ///     Number of bytes sent by the server
        /// </summary>
        public Int64 BytesSent
        {
            get
            {
                return bytesSent;
            }
            protected set
            {
                bytesSent = value;
            }
        }

        /// <summary>
        ///     Number of bytes received by the server
        /// </summary>
        public Int64 BytesReceived
        {
            get
            {
                return bytesReceived;
            }
            protected set
            {
                bytesReceived = value;
            }
        }
        
        /// <summary>
        ///     Option: dual mode socket
        /// </summary>
        /// <remarks>
        ///     Specifies whether the Socket is a dual-mode socket used for both IPv4 and IPv6.
        ///     Will work only if socket is bound on IPv6 address.
        /// </remarks>
        public Boolean OptionDualMode { get; set; }

        /// <summary>
        ///     Option: reuse address
        /// </summary>
        /// <remarks>
        ///     This option will enable/disable SO_REUSEADDR if the OS support this feature
        /// </remarks>
        public Boolean OptionReuseAddress { get; set; }

        /// <summary>
        ///     Option: enables a socket to be bound for exclusive access
        /// </summary>
        /// <remarks>
        ///     This option will enable/disable SO_EXCLUSIVEADDRUSE if the OS support this feature
        /// </remarks>
        public Boolean OptionExclusiveAddressUse { get; set; }
        
        /// <summary>
        ///     Option: receive buffer size
        /// </summary>
        public Int32 OptionReceiveBufferSize { get; set; } = 8192;

        /// <summary>
        ///     Option: send buffer size
        /// </summary>
        public Int32 OptionSendBufferSize { get; set; } = 8192;

        /// <summary>
        ///     Is the server started?
        /// </summary>
        public Boolean IsStarted { get; protected set; }

        /// <summary>
        ///     Disposed flag
        /// </summary>
        public Boolean IsDisposed { get; protected set; }

        /// <summary>
        ///     Server socket disposed flag
        /// </summary>
        public Boolean IsSocketDisposed { get; protected set; } = true;
        
        // Server statistic
        internal Int64 bytesPending;
        internal Int64 bytesReceived;
        internal Int64 bytesSent;
        
        public NetworkServer(IPEndPoint endpoint)
        {
            Id = Guid.NewGuid();
            Endpoint = endpoint;
        }
        
        /// <summary>
        ///     Send error notification
        /// </summary>
        /// <param name="error">Socket error code</param>
        protected void SendError(SocketError error)
        {
            // Skip disconnect errors
            if (error == SocketError.ConnectionAborted ||
                error == SocketError.ConnectionRefused ||
                error == SocketError.ConnectionReset ||
                error == SocketError.OperationAborted ||
                error == SocketError.Shutdown)
            {
                return;
            }

            OnError(error);
        }
        
        public abstract Boolean Start();
        public abstract Boolean Stop();
        
        /// <summary>
        ///     Restart the server
        /// </summary>
        /// <returns>'true' if the server was successfully restarted, 'false' if the server failed to restart</returns>
        public virtual Boolean Restart()
        {
            if (!Stop())
            {
                return false;
            }

            while (IsStarted)
            {
                Thread.Yield();
            }

            return Start();
        }

        /// <summary>
        ///     Handle server started notification
        /// </summary>
        protected virtual void OnStarted()
        {
        }

        /// <summary>
        ///     Handle server stopped notification
        /// </summary>
        protected virtual void OnStopped()
        {
        }
        
        /// <summary>
        ///     Handle error notification
        /// </summary>
        /// <param name="error">Socket error code</param>
        protected virtual void OnError(SocketError error)
        {
        }
        
        // Implement IDisposable.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        
        protected virtual void Dispose(Boolean disposing)
        {
            if (IsDisposed)
            {
                return;
            }

            if (disposing)
            {
                // Dispose managed resources here...
                Stop();
            }

            // Dispose unmanaged resources here...

            // Set large fields to null here...

            // Mark as disposed.
            IsDisposed = true;
        }

        // Use C# destructor syntax for finalization code.
        ~NetworkServer()
        {
            // Simply call Dispose(false).
            Dispose(false);
        }
    }
}