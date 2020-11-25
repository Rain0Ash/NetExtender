// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Net.Sockets;
using System.Text;
using NetExtender.Network.Networking.Common.Interfaces;

namespace NetExtender.Network.Networking.Common
{
    public abstract class NetworkSession : INetworkSession
    {
        /// <summary>
        ///     Session Id
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        ///     Socket
        /// </summary>
        public Socket Socket { get; protected set; }

        /// <summary>
        ///     Number of bytes pending sent by the session
        /// </summary>
        public Int64 BytesPending { get; protected set; }

        /// <summary>
        ///     Number of bytes sending by the session
        /// </summary>
        public Int64 BytesSending { get; protected set; }

        /// <summary>
        ///     Number of bytes sent by the session
        /// </summary>
        public Int64 BytesSent { get; protected set; }

        /// <summary>
        ///     Number of bytes received by the session
        /// </summary>
        public Int64 BytesReceived { get; protected set; }

        /// <summary>
        ///     Option: receive buffer size
        /// </summary>
        public Int32 OptionReceiveBufferSize { get; set; }

        /// <summary>
        ///     Option: send buffer size
        /// </summary>
        public Int32 OptionSendBufferSize { get; set; }

        /// <summary>
        ///     Is the session connected?
        /// </summary>
        public Boolean IsConnected { get; protected set; }

        /// <summary>
        ///     Disposed flag
        /// </summary>
        public Boolean IsDisposed { get; protected set; }

        /// <summary>
        ///     Session socket disposed flag
        /// </summary>
        public Boolean IsSocketDisposed { get; protected set; } = true;

        // Send buffer
        protected readonly Object sendLock = new Object();
        
        protected Boolean receiving;
        protected NetworkDataBuffer sendBufferFlush;
        protected Int64 sendBufferFlushOffset;
        protected NetworkDataBuffer sendBufferMain;
        
        // Receive buffer
        protected Boolean sending;
        
        protected NetworkSession(Int32 receive, Int32 send)
        {
            Id = Guid.NewGuid();
            OptionReceiveBufferSize = receive;
            OptionSendBufferSize = send;
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
        ///     Connect the session
        /// </summary>
        /// <param name="socket">Session socket</param>
        public abstract void Connect(Socket socket);
        
        /// <summary>
        ///     Disconnect the session
        /// </summary>
        /// <returns>'true' if the section was successfully disconnected, 'false' if the section is already disconnected</returns>
        public abstract Boolean Disconnect();
        
        /// <summary>
        ///     Send data to the client (synchronous)
        /// </summary>
        /// <param name="buffer">Buffer to send</param>
        /// <returns>Size of sent data</returns>
        public virtual Int64 Send(Byte[] buffer)
        {
            return Send(buffer, 0, buffer.Length);
        }

        /// <summary>
        ///     Send data to the client (synchronous)
        /// </summary>
        /// <param name="buffer">Buffer to send</param>
        /// <param name="offset">Buffer offset</param>
        /// <param name="size">Buffer size</param>
        /// <returns>Size of sent data</returns>
        public abstract Int64 Send(Byte[] buffer, Int64 offset, Int64 size);
        
        /// <summary>
        ///     Send text to the client (synchronous)
        /// </summary>
        /// <param name="text">Text string to send</param>
        /// <returns>Size of sent data</returns>
        public virtual Int64 Send(String text)
        {
            return Send(Encoding.UTF8.GetBytes(text));
        }

        /// <summary>
        ///     Send data to the client (asynchronous)
        /// </summary>
        /// <param name="buffer">Buffer to send</param>
        /// <returns>'true' if the data was successfully sent, 'false' if the session is not connected</returns>
        public virtual Boolean SendAsync(Byte[] buffer)
        {
            return SendAsync(buffer, 0, buffer.Length);
        }

        /// <summary>
        ///     Send data to the client (asynchronous)
        /// </summary>
        /// <param name="buffer">Buffer to send</param>
        /// <param name="offset">Buffer offset</param>
        /// <param name="size">Buffer size</param>
        /// <returns>'true' if the data was successfully sent, 'false' if the session is not connected</returns>
        public abstract Boolean SendAsync(Byte[] buffer, Int64 offset, Int64 size);

        /// <summary>
        ///     Send text to the client (asynchronous)
        /// </summary>
        /// <param name="text">Text string to send</param>
        /// <returns>'true' if the text was successfully sent, 'false' if the session is not connected</returns>
        public virtual Boolean SendAsync(String text)
        {
            return SendAsync(Encoding.UTF8.GetBytes(text));
        }

        /// <summary>
        ///     Receive data from the client (synchronous)
        /// </summary>
        /// <param name="buffer">Buffer to receive</param>
        /// <returns>Size of received data</returns>
        public virtual Int64 Receive(Byte[] buffer)
        {
            return Receive(buffer, 0, buffer.Length);
        }

        /// <summary>
        ///     Receive data from the client (synchronous)
        /// </summary>
        /// <param name="buffer">Buffer to receive</param>
        /// <param name="offset">Buffer offset</param>
        /// <param name="size">Buffer size</param>
        /// <returns>Size of received data</returns>
        public abstract Int64 Receive(Byte[] buffer, Int64 offset, Int64 size);
        
        /// <summary>
        ///     Receive data from the client (asynchronous)
        /// </summary>
        public virtual void ReceiveAsync()
        {
            // Try to receive data from the client
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
        
        /// <summary>
        ///     Clear send/receive buffers
        /// </summary>
        protected virtual void ClearBuffers()
        {
            lock (sendLock)
            {
                // Clear send buffers
                sendBufferMain.Clear();
                sendBufferFlush.Clear();
                sendBufferFlushOffset = 0;

                // Update statistic
                BytesPending = 0;
                BytesSending = 0;
            }
        }
        
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
        ///     Handle buffer received notification
        /// </summary>
        /// <param name="buffer">Received buffer</param>
        /// <param name="offset">Received buffer offset</param>
        /// <param name="size">Received buffer size</param>
        /// <remarks>
        ///     Notification is called when another chunk of buffer was received from the client
        /// </remarks>
        protected virtual void OnReceived(Byte[] buffer, Int64 offset, Int64 size)
        {
        }

        /// <summary>
        ///     Handle buffer sent notification
        /// </summary>
        /// <param name="sent">Size of sent buffer</param>
        /// <param name="pending">Size of pending buffer</param>
        /// <remarks>
        ///     Notification is called when another chunk of buffer was sent to the client.
        ///     This handler could be used to send another buffer to the client for instance when the pending size is zero.
        /// </remarks>
        protected virtual void OnSent(Int64 sent, Int64 pending)
        {
        }

        /// <summary>
        ///     Handle empty send buffer notification
        /// </summary>
        /// <remarks>
        ///     Notification is called when the send buffer is empty and ready for a new data to send.
        ///     This handler could be used to send another buffer to the client.
        /// </remarks>
        protected virtual void OnEmpty()
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
                Disconnect();
            }

            // Dispose unmanaged resources here...

            // Set large fields to null here...

            // Mark as disposed.
            IsDisposed = true;
        }

        // Use C# destructor syntax for finalization code.
        ~NetworkSession()
        {
            // Simply call Dispose(false).
            Dispose(false);
        }
    }
}