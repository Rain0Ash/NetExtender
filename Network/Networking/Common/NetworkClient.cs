// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using NetExtender.Network.Networking.Common.Interfaces;

namespace NetExtender.Network.Networking.Common
{
    public abstract class NetworkClient : INetworkClient
    {
        /// <summary>
        ///     Client Id
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        ///     IP endpoint
        /// </summary>
        public IPEndPoint Endpoint { get; }
        
        /// <summary>
        ///     Socket
        /// </summary>
        public Socket Socket { get; protected set; }
        
        /// <summary>
        ///     Number of bytes pending sent by the client
        /// </summary>
        public Int64 BytesPending { get; protected set; }

        /// <summary>
        ///     Number of bytes sending by the client
        /// </summary>
        public Int64 BytesSending { get; protected set; }

        /// <summary>
        ///     Number of bytes sent by the client
        /// </summary>
        public Int64 BytesSent { get; protected set; }

        /// <summary>
        ///     Number of bytes received by the client
        /// </summary>
        public Int64 BytesReceived { get; protected set; }
        
        /// <summary>
        ///     Option: dual mode socket
        /// </summary>
        /// <remarks>
        ///     Specifies whether the Socket is a dual-mode socket used for both IPv4 and IPv6.
        ///     Will work only if socket is bound on IPv6 address.
        /// </remarks>
        public Boolean OptionDualMode { get; set; }
        
        /// <summary>
        ///     Option: receive buffer size
        /// </summary>
        public Int32 OptionReceiveBufferSize { get; set; } = 8192;

        /// <summary>
        ///     Option: send buffer size
        /// </summary>
        public Int32 OptionSendBufferSize { get; set; } = 8192;
        
        /// <summary>
        ///     Is the client connected?
        /// </summary>
        public Boolean IsConnected { get; protected set; }

        /// <summary>
        ///     Disposed flag
        /// </summary>
        public Boolean IsDisposed { get; protected set; }

        /// <summary>
        ///     Client socket disposed flag
        /// </summary>
        public Boolean IsSocketDisposed { get; protected set; } = true;
        
        // Receive buffer
        protected NetworkDataBuffer receiveBuffer;

        protected Boolean receiving;
        protected NetworkDataBuffer sendBuffer;
        
        // Send buffer
        protected Boolean sending;

        public NetworkClient(IPEndPoint endpoint)
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
        
        /// <summary>
        ///     Connect the client (synchronous)
        /// </summary>
        /// <remarks>
        ///     Please note that synchronous connect will not receive data automatically!
        ///     You should use Receive() or ReceiveAsync() method manually after successful connection.
        /// </remarks>
        /// <returns>'true' if the client was successfully connected, 'false' if the client failed to connect</returns>
        public abstract Boolean Connect();
        
        /// <summary>
        ///     Disconnect the client (synchronous)
        /// </summary>
        /// <returns>'true' if the client was successfully disconnected, 'false' if the client is already disconnected</returns>
        public abstract Boolean Disconnect();
        
        /// <summary>
        ///     Reconnect the client (synchronous)
        /// </summary>
        /// <returns>'true' if the client was successfully reconnected, 'false' if the client is already reconnected</returns>
        public virtual Boolean Reconnect()
        {
            return Disconnect() && Connect();
        }
        
        /// <summary>
        ///     Send data to the server (synchronous)
        /// </summary>
        /// <param name="buffer">Buffer to send</param>
        /// <returns>Size of sent data</returns>
        public virtual Int64 Send(Byte[] buffer)
        {
            return Send(buffer, 0, buffer.Length);
        }
        
        /// <summary>
        ///     Send data to the server (synchronous)
        /// </summary>
        /// <param name="buffer">Buffer to send</param>
        /// <param name="offset">Buffer offset</param>
        /// <param name="size">Buffer size</param>
        /// <returns>Size of sent data</returns>
        public abstract Int64 Send(Byte[] buffer, Int64 offset, Int64 size);
        
        /// <summary>
        ///     Send text to the server (synchronous)
        /// </summary>
        /// <param name="text">Text string to send</param>
        /// <returns>Size of sent text</returns>
        public virtual Int64 Send(String text)
        {
            return Send(Encoding.UTF8.GetBytes(text));
        }
        
        /// <summary>
        ///     Send data to the server (asynchronous)
        /// </summary>
        /// <param name="buffer">Buffer to send</param>
        /// <returns>'true' if the data was successfully sent, 'false' if the client is not connected</returns>
        public virtual Boolean SendAsync(Byte[] buffer)
        {
            return SendAsync(buffer, 0, buffer.Length);
        }

        /// <summary>
        ///     Send data to the server (asynchronous)
        /// </summary>
        /// <param name="buffer">Buffer to send</param>
        /// <param name="offset">Buffer offset</param>
        /// <param name="size">Buffer size</param>
        /// <returns>'true' if the data was successfully sent, 'false' if the client is not connected</returns>
        public abstract Boolean SendAsync(Byte[] buffer, Int64 offset, Int64 size);
        
        /// <summary>
        ///     Send text to the server (asynchronous)
        /// </summary>
        /// <param name="text">Text string to send</param>
        /// <returns>'true' if the text was successfully sent, 'false' if the client is not connected</returns>
        public virtual Boolean SendAsync(String text)
        {
            return SendAsync(Encoding.UTF8.GetBytes(text));
        }
        
        /// <summary>
        ///     Receive data from the server (asynchronous)
        /// </summary>
        public virtual void ReceiveAsync()
        {
            // Try to receive data from the server
            TryReceive();
        }
        
        /// <summary>
        ///     Try to receive new data
        /// </summary>
        protected abstract void TryReceive();

        /// <summary>
        ///     Try to send pending data
        /// </summary>
        protected abstract void TrySend();

        protected abstract void ClearBuffers();
        
        /// <summary>
        ///     This method is called whenever a receive or send operation is completed on a socket
        /// </summary>
        protected abstract void OnAsyncCompleted(Object sender, SocketAsyncEventArgs e);
        
        /// <summary>
        ///     Handle client connected notification
        /// </summary>
        protected virtual void OnConnected()
        {
        }

        /// <summary>
        ///     Handle client disconnected notification
        /// </summary>
        protected virtual void OnDisconnected()
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
            // The idea here is that Dispose(Boolean) knows whether it is
            // being called to do explicit cleanup (the Boolean is true)
            // versus being called due to a garbage collection (the Boolean
            // is false). This distinction is useful because, when being
            // disposed explicitly, the Dispose(Boolean) method can safely
            // execute code using reference type fields that refer to other
            // objects knowing for sure that these other objects have not been
            // finalized or disposed of yet. When the Boolean is false,
            // the Dispose(Boolean) method should not execute code that
            // refer to reference type fields because those objects may
            // have already been finalized."

            if (IsDisposed)
            {
                return;
            }

            if (disposing)
            {
                // Dispose managed resources here...
                Disconnect();
            }

            // Dispose unmanaged resources here...

            // Set large fields to null here...

            // Mark as disposed.
            IsDisposed = true;
        }
        
        // Use C# destructor syntax for finalization code.
        ~NetworkClient()
        {
            // Simply call Dispose(false).
            Dispose(false);
        }
    }
}