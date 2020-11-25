using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Exceptions;
using NetExtender.Network.Networking.Common;

namespace NetExtender.Network.Networking.TCP
{
    /// <summary>
    ///     TCP session is used to read and write data from the connected TCP client
    /// </summary>
    /// <remarks>Thread-safe</remarks>
    public class TcpNetworkSession : NetworkSession
    {
        private NetworkDataBuffer _receiveBuffer;

        private SocketAsyncEventArgs _receiveEventArg;

        // Receive buffer
        private SocketAsyncEventArgs _sendEventArg;

        /// <summary>
        ///     Initialize the session with a given server
        /// </summary>
        /// <param name="server">TCP server</param>
        public TcpNetworkSession(TcpNetworkServer server)
            : base(server.OptionReceiveBufferSize, server.OptionSendBufferSize)
        {
            Server = server;
        }

        /// <summary>
        ///     Server
        /// </summary>
        public TcpNetworkServer Server { get; }

        /// <summary>
        ///     Connect the session
        /// </summary>
        /// <param name="socket">Session socket</param>
        public override void Connect(Socket socket)
        {
            if (IsConnected)
            {
                throw new AlreadyInitializedException();
            }
            
            Socket = socket;

            // Update the session socket disposed flag
            IsSocketDisposed = false;

            // Setup buffers
            _receiveBuffer = new NetworkDataBuffer();
            sendBufferMain = new NetworkDataBuffer();
            sendBufferFlush = new NetworkDataBuffer();

            // Setup event args
            _receiveEventArg = new SocketAsyncEventArgs();
            _receiveEventArg.Completed += OnAsyncCompleted;
            _sendEventArg = new SocketAsyncEventArgs();
            _sendEventArg.Completed += OnAsyncCompleted;

            // Apply the option: keep alive
            if (Server.OptionKeepAlive)
            {
                Socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);
            }

            // Apply the option: no delay
            if (Server.OptionNoDelay)
            {
                Socket.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.NoDelay, true);
            }

            // Prepare receive & send buffers
            _receiveBuffer.Reserve(OptionReceiveBufferSize);
            sendBufferMain.Reserve(OptionSendBufferSize);
            sendBufferFlush.Reserve(OptionSendBufferSize);

            // Reset statistic
            BytesPending = 0;
            BytesSending = 0;
            BytesSent = 0;
            BytesReceived = 0;

            // Update the connected flag
            IsConnected = true;

            // Call the session connected handler
            OnConnected();

            // Call the session connected handler in the server
            Server.OnConnectedInternal(this);

            // Call the empty send buffer handler
            if (sendBufferMain.IsEmpty)
            {
                OnEmpty();
            }

            // Try to receive something from the client
            TryReceive();
        }

        /// <summary>
        ///     Disconnect the session
        /// </summary>
        /// <returns>'true' if the section was successfully disconnected, 'false' if the section is already disconnected</returns>
        public override Boolean Disconnect()
        {
            if (!IsConnected)
            {
                return false;
            }

            // Reset event args
            _receiveEventArg.Completed -= OnAsyncCompleted;
            _sendEventArg.Completed -= OnAsyncCompleted;

            try
            {
                try
                {
                    // Shutdown the socket associated with the client
                    Socket.Shutdown(SocketShutdown.Both);
                }
                catch (SocketException)
                {
                }

                // Close the session socket
                Socket.Close();

                // Dispose the session socket
                Socket.Dispose();

                // Dispose event arguments
                _receiveEventArg.Dispose();
                _sendEventArg.Dispose();

                // Update the session socket disposed flag
                IsSocketDisposed = true;
            }
            catch (ObjectDisposedException)
            {
            }

            // Update the connected flag
            IsConnected = false;

            // Update sending/receiving flags
            receiving = false;
            sending = false;

            // Clear send/receive buffers
            ClearBuffers();

            // Call the session disconnected handler
            OnDisconnected();

            // Call the session disconnected handler in the server
            Server.OnDisconnectedInternal(this);

            // Unregister session
            Server.UnregisterSession(Id);

            return true;
        }

        /// <summary>
        ///     Send data to the client (synchronous)
        /// </summary>
        /// <param name="buffer">Buffer to send</param>
        /// <param name="offset">Buffer offset</param>
        /// <param name="size">Buffer size</param>
        /// <returns>Size of sent data</returns>
        public override Int64 Send(Byte[] buffer, Int64 offset, Int64 size)
        {
            if (!IsConnected)
            {
                return 0;
            }

            if (size == 0)
            {
                return 0;
            }

            // Sent data to the client
            Int64 sent = Socket.Send(buffer, (Int32) offset, (Int32) size, SocketFlags.None, out SocketError ec);
            if (sent > 0)
            {
                // Update statistic
                BytesSent += sent;
                Interlocked.Add(ref Server.bytesSent, size);

                // Call the buffer sent handler
                OnSent(sent, BytesPending + BytesSending);
            }

            // Check for socket error
            if (ec == SocketError.Success)
            {
                return sent;
            }

            SendError(ec);
            Disconnect();

            return sent;
        }

        /// <summary>
        ///     Send data to the client (asynchronous)
        /// </summary>
        /// <param name="buffer">Buffer to send</param>
        /// <param name="offset">Buffer offset</param>
        /// <param name="size">Buffer size</param>
        /// <returns>'true' if the data was successfully sent, 'false' if the session is not connected</returns>
        public override Boolean SendAsync(Byte[] buffer, Int64 offset, Int64 size)
        {
            if (!IsConnected)
            {
                return false;
            }

            if (size == 0)
            {
                return true;
            }

            lock (sendLock)
            {
                // Detect multiple send handlers
                Boolean sendRequired = sendBufferMain.IsEmpty || sendBufferFlush.IsEmpty;

                // Fill the main send buffer
                sendBufferMain.Append(buffer, offset, size);

                // Update statistic
                BytesPending = sendBufferMain.Size;

                // Avoid multiple send handlers
                if (!sendRequired)
                {
                    return true;
                }
            }

            // Try to send the main buffer
            Task.Factory.StartNew(TrySend);

            return true;
        }
        
        /// <summary>
        ///     Receive data from the client (synchronous)
        /// </summary>
        /// <param name="buffer">Buffer to receive</param>
        /// <param name="offset">Buffer offset</param>
        /// <param name="size">Buffer size</param>
        /// <returns>Size of received data</returns>
        public override Int64 Receive(Byte[] buffer, Int64 offset, Int64 size)
        {
            if (!IsConnected)
            {
                return 0;
            }

            if (size == 0)
            {
                return 0;
            }

            // Receive data from the client
            Int64 received = Socket.Receive(buffer, (Int32) offset, (Int32) size, SocketFlags.None, out SocketError ec);
            if (received > 0)
            {
                // Update statistic
                BytesReceived += received;
                Interlocked.Add(ref Server.bytesReceived, received);

                // Call the buffer received handler
                OnReceived(buffer, 0, received);
            }

            // Check for socket error
            if (ec == SocketError.Success)
            {
                return received;
            }

            SendError(ec);
            Disconnect();

            return received;
        }

        /// <summary>
        ///     Receive text from the client (synchronous)
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
        ///     Try to receive new data
        /// </summary>
        protected override void TryReceive()
        {
            if (receiving)
            {
                return;
            }

            if (!IsConnected)
            {
                return;
            }

            Boolean process = true;

            while (process)
            {
                process = false;

                try
                {
                    // Async receive with the receive handler
                    receiving = true;
                    _receiveEventArg.SetBuffer(_receiveBuffer.Data, 0, (Int32) _receiveBuffer.Capacity);
                    if (!Socket.ReceiveAsync(_receiveEventArg))
                    {
                        process = ProcessReceive(_receiveEventArg);
                    }
                }
                catch (ObjectDisposedException)
                {
                }
            }
        }

        /// <summary>
        ///     Try to send pending data
        /// </summary>
        protected override void TrySend()
        {
            if (sending)
            {
                return;
            }

            if (!IsConnected)
            {
                return;
            }

            Boolean process = true;

            while (process)
            {
                process = false;

                lock (sendLock)
                {
                    if (sending)
                    {
                        return;
                    }

                    // Swap send buffers
                    if (sendBufferFlush.IsEmpty)
                    {
                        // Swap flush and main buffers
                        sendBufferFlush = Interlocked.Exchange(ref sendBufferMain, sendBufferFlush);
                        sendBufferFlushOffset = 0;

                        // Update statistic
                        BytesPending = 0;
                        BytesSending += sendBufferFlush.Size;

                        sending = !sendBufferFlush.IsEmpty;
                    }
                    else
                    {
                        return;
                    }
                }

                // Check if the flush buffer is empty
                if (sendBufferFlush.IsEmpty)
                {
                    // Call the empty send buffer handler
                    OnEmpty();
                    return;
                }

                try
                {
                    // Async write with the write handler
                    _sendEventArg.SetBuffer(sendBufferFlush.Data, (Int32) sendBufferFlushOffset, (Int32) (sendBufferFlush.Size - sendBufferFlushOffset));
                    if (!Socket.SendAsync(_sendEventArg))
                    {
                        process = ProcessSend(_sendEventArg);
                    }
                }
                catch (ObjectDisposedException)
                {
                }
            }
        }

        /// <summary>
        ///     This method is called whenever a receive or send operation is completed on a socket
        /// </summary>
        private void OnAsyncCompleted(Object sender, SocketAsyncEventArgs e)
        {
            // Determine which type of operation just completed and call the associated handler
            switch (e.LastOperation)
            {
                case SocketAsyncOperation.Receive:
                    if (ProcessReceive(e))
                    {
                        TryReceive();
                    }

                    break;
                case SocketAsyncOperation.Send:
                    if (ProcessSend(e))
                    {
                        TrySend();
                    }

                    break;
                default:
                    throw new ArgumentException("The last operation completed on the socket was not a receive or send");
            }
        }

        /// <summary>
        ///     This method is invoked when an asynchronous receive operation completes
        /// </summary>
        private Boolean ProcessReceive(SocketAsyncEventArgs e)
        {
            if (!IsConnected)
            {
                return false;
            }

            Int64 size = e.BytesTransferred;

            // Received some data from the client
            if (size > 0)
            {
                // Update statistic
                BytesReceived += size;
                Interlocked.Add(ref Server.bytesReceived, size);

                // Call the buffer received handler
                OnReceived(_receiveBuffer.Data, 0, size);

                // If the receive buffer is full increase its size
                if (_receiveBuffer.Capacity == size)
                {
                    _receiveBuffer.Reserve(2 * size);
                }
            }

            receiving = false;

            // Try to receive again if the session is valid
            if (e.SocketError == SocketError.Success)
            {
                // If zero is returned from a read operation, the remote end has closed the connection
                if (size > 0)
                {
                    return true;
                }

                Disconnect();
            }
            else
            {
                SendError(e.SocketError);
                Disconnect();
            }

            return false;
        }

        /// <summary>
        ///     This method is invoked when an asynchronous send operation completes
        /// </summary>
        private Boolean ProcessSend(SocketAsyncEventArgs e)
        {
            if (!IsConnected)
            {
                return false;
            }

            Int64 size = e.BytesTransferred;

            // Send some data to the client
            if (size > 0)
            {
                // Update statistic
                BytesSending -= size;
                BytesSent += size;
                Interlocked.Add(ref Server.bytesSent, size);

                // Increase the flush buffer offset
                sendBufferFlushOffset += size;

                // Successfully send the whole flush buffer
                if (sendBufferFlushOffset == sendBufferFlush.Size)
                {
                    // Clear the flush buffer
                    sendBufferFlush.Clear();
                    sendBufferFlushOffset = 0;
                }

                // Call the buffer sent handler
                OnSent(size, BytesPending + BytesSending);
            }

            sending = false;

            // Try to send again if the session is valid
            if (e.SocketError == SocketError.Success)
            {
                return true;
            }

            SendError(e.SocketError);
            Disconnect();
            return false;
        }
    }
}