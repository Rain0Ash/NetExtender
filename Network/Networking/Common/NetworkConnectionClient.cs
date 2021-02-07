// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace NetExtender.Network.Networking.Common
{
    public abstract class NetworkConnectionClient : NetworkClient
    {
        /// <summary>
        ///     Option: keep alive
        /// </summary>
        /// <remarks>
        ///     This option will setup SO_KEEPALIVE if the OS support this feature
        /// </remarks>
        public Boolean OptionKeepAlive { get; set; }

        /// <summary>
        ///     Option: no delay
        /// </summary>
        /// <remarks>
        ///     This option will enable/disable Nagle's algorithm for TCP protocol
        /// </remarks>
        public Boolean OptionNoDelay { get; set; }

        /// <summary>
        ///     Is the client connecting?
        /// </summary>
        public Boolean IsConnecting { get; protected set; }

        // Send buffer
        protected readonly Object sendLock = new Object();
        protected SocketAsyncEventArgs connectEventArg;

        // Receive buffer
        protected Int64 sendBufferFlushOffset;
        protected NetworkDataBuffer sendBufferMain;
        
        /// <summary>
        ///     Initialize client with a given IP endpoint
        /// </summary>
        /// <param name="endpoint">IP endpoint</param>
        protected NetworkConnectionClient(IPEndPoint endpoint)
            : base(endpoint)
        {
        }

        /// <summary>
        ///     Connect the client (asynchronous)
        /// </summary>
        /// <returns>'true' if the client was successfully connected, 'false' if the client failed to connect</returns>
        public abstract Boolean ConnectAsync();
        
        /// <summary>
        ///     Disconnect the client (asynchronous)
        /// </summary>
        /// <returns>'true' if the client was successfully disconnected, 'false' if the client is already disconnected</returns>
        public virtual Boolean DisconnectAsync()
        {
            return Disconnect();
        }

        /// <summary>
        ///     Reconnect the client (asynchronous)
        /// </summary>
        /// <returns>'true' if the client was successfully reconnected, 'false' if the client is already reconnected</returns>
        public virtual Boolean ReconnectAsync()
        {
            if (!DisconnectAsync())
            {
                return false;
            }

            while (IsConnected)
            {
                Thread.Yield();
            }

            return ConnectAsync();
        }

        /// <summary>
        ///     Receive data from the server (synchronous)
        /// </summary>
        /// <param name="buffer">Buffer to receive</param>
        /// <returns>Size of received data</returns>
        public virtual Int64 Receive(Byte[] buffer)
        {
            return Receive(buffer, 0, buffer.Length);
        }

        /// <summary>
        ///     Receive data from the server (synchronous)
        /// </summary>
        /// <param name="buffer">Buffer to receive</param>
        /// <param name="offset">Buffer offset</param>
        /// <param name="size">Buffer size</param>
        /// <returns>Size of received data</returns>
        public abstract Int64 Receive(Byte[] buffer, Int64 offset, Int64 size);
        
        /// <summary>
        ///     Receive text from the server (synchronous)
        /// </summary>
        /// <param name="size">Text size to receive</param>
        /// <returns>Received text</returns>
        public virtual String Receive(Int64 size)
        {
            Byte[] buffer = new Byte[size];
            Int64 length = Receive(buffer);
            return Encoding.UTF8.GetString(buffer, 0, (Int32) length);
        }

        /// <summary>
        ///     Clear send/receive buffers
        /// </summary>
        protected sealed override void ClearBuffers()
        {
            lock (sendLock)
            {
                // Clear send buffers
                sendBufferMain.Clear();
                sendBuffer.Clear();
                sendBufferFlushOffset = 0;

                // Update statistic
                BytesPending = 0;
                BytesSending = 0;
            }
        }

        /// <summary>
        ///     This method is invoked when an asynchronous connect operation completes
        /// </summary>
        protected abstract void ProcessConnect(SocketAsyncEventArgs e);

        /// <summary>
        ///     Handle buffer received notification
        /// </summary>
        /// <param name="buffer">Received buffer</param>
        /// <param name="offset">Received buffer offset</param>
        /// <param name="size">Received buffer size</param>
        /// <remarks>
        ///     Notification is called when another chunk of buffer was received from the server
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
        ///     Notification is called when another chunk of buffer was sent to the server.
        ///     This handler could be used to send another buffer to the server for instance when the pending size is zero.
        /// </remarks>
        protected virtual void OnSent(Int64 sent, Int64 pending)
        {
        }

        /// <summary>
        ///     Handle empty send buffer notification
        /// </summary>
        /// <remarks>
        ///     Notification is called when the send buffer is empty and ready for a new data to send.
        ///     This handler could be used to send another buffer to the server.
        /// </remarks>
        protected virtual void OnEmpty()
        {
        }

        protected override void Dispose(Boolean disposing)
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
                DisconnectAsync();
            }

            // Dispose unmanaged resources here...

            // Set large fields to null here...

            // Mark as disposed.
            IsDisposed = true;
        }
    }
}