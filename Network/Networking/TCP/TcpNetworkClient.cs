using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Network.Networking.Common;

namespace NetExtender.Network.Networking.TCP
{
    /// <summary>
    ///     TCP client is used to read/write data from/into the connected TCP server
    /// </summary>
    /// <remarks>Thread-safe</remarks>
    public class TcpNetworkClient : NetworkConnectionClient
    {
        // Send buffer
        private SocketAsyncEventArgs _receiveEventArg;

        // Receive buffer
        private SocketAsyncEventArgs _sendEventArg;

        /// <summary>
        ///     Initialize TCP client with a given server IP address and port number
        /// </summary>
        /// <param name="address">IP address</param>
        /// <param name="port">Port number</param>
        public TcpNetworkClient(IPAddress address, Int32 port)
            : this(new IPEndPoint(address, port))
        {
        }

        /// <summary>
        ///     Initialize TCP client with a given server IP address and port number
        /// </summary>
        /// <param name="address">IP address</param>
        /// <param name="port">Port number</param>
        public TcpNetworkClient(String address, Int32 port)
            : this(new IPEndPoint(IPAddress.Parse(address), port))
        {
        }

        /// <summary>
        ///     Initialize TCP client with a given IP endpoint
        /// </summary>
        /// <param name="endpoint">IP endpoint</param>
        public TcpNetworkClient(IPEndPoint endpoint)
            : base(endpoint)
        {
        }

        /// <summary>
        ///     Connect the client (synchronous)
        /// </summary>
        /// <remarks>
        ///     Please note that synchronous connect will not receive data automatically!
        ///     You should use Receive() or ReceiveAsync() method manually after successful connection.
        /// </remarks>
        /// <returns>'true' if the client was successfully connected, 'false' if the client failed to connect</returns>
        public override Boolean Connect()
        {
            if (IsConnected || IsConnecting)
            {
                return false;
            }

            // Setup buffers
            receiveBuffer = new NetworkDataBuffer();
            sendBufferMain = new NetworkDataBuffer();
            sendBuffer = new NetworkDataBuffer();

            // Setup event args
            connectEventArg = new SocketAsyncEventArgs {RemoteEndPoint = Endpoint};
            connectEventArg.Completed += OnAsyncCompleted;
            _receiveEventArg = new SocketAsyncEventArgs();
            _receiveEventArg.Completed += OnAsyncCompleted;
            _sendEventArg = new SocketAsyncEventArgs();
            _sendEventArg.Completed += OnAsyncCompleted;

            // Create a new client socket
            Socket = new Socket(Endpoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            // Apply the option: dual mode (this option must be applied before connecting)
            if (Socket.AddressFamily == AddressFamily.InterNetworkV6)
            {
                Socket.DualMode = OptionDualMode;
            }

            try
            {
                // Connect to the server
                Socket.Connect(Endpoint);
            }
            catch (SocketException ex)
            {
                // Close the client socket
                Socket.Close();
                // Dispose the client socket
                Socket.Dispose();
                // Dispose event arguments
                connectEventArg.Dispose();
                _receiveEventArg.Dispose();
                _sendEventArg.Dispose();

                // Call the client disconnected handler
                SendError(ex.SocketErrorCode);
                OnDisconnected();
                return false;
            }

            // Update the client socket disposed flag
            IsSocketDisposed = false;

            // Apply the option: keep alive
            if (OptionKeepAlive)
            {
                Socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);
            }

            // Apply the option: no delay
            if (OptionNoDelay)
            {
                Socket.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.NoDelay, true);
            }

            // Prepare receive & send buffers
            receiveBuffer.Reserve(OptionReceiveBufferSize);
            sendBufferMain.Reserve(OptionSendBufferSize);
            sendBuffer.Reserve(OptionSendBufferSize);

            // Reset statistic
            BytesPending = 0;
            BytesSending = 0;
            BytesSent = 0;
            BytesReceived = 0;

            // Update the connected flag
            IsConnected = true;

            // Call the client connected handler
            OnConnected();

            // Call the empty send buffer handler
            if (sendBufferMain.IsEmpty)
            {
                OnEmpty();
            }

            return true;
        }

        /// <summary>
        ///     Disconnect the client (synchronous)
        /// </summary>
        /// <returns>'true' if the client was successfully disconnected, 'false' if the client is already disconnected</returns>
        public override Boolean Disconnect()
        {
            if (!IsConnected && !IsConnecting)
            {
                return false;
            }

            // Cancel connecting operation
            if (IsConnecting)
            {
                Socket.CancelConnectAsync(connectEventArg);
            }

            // Reset event args
            connectEventArg.Completed -= OnAsyncCompleted;
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

                // Close the client socket
                Socket.Close();

                // Dispose the client socket
                Socket.Dispose();

                // Dispose event arguments
                connectEventArg.Dispose();
                _receiveEventArg.Dispose();
                _sendEventArg.Dispose();

                // Update the client socket disposed flag
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

            // Call the client disconnected handler
            OnDisconnected();

            return true;
        }

        /// <summary>
        ///     Connect the client (asynchronous)
        /// </summary>
        /// <returns>'true' if the client was successfully connected, 'false' if the client failed to connect</returns>
        public override Boolean ConnectAsync()
        {
            if (IsConnected || IsConnecting)
            {
                return false;
            }

            // Setup buffers
            receiveBuffer = new NetworkDataBuffer();
            sendBufferMain = new NetworkDataBuffer();
            sendBuffer = new NetworkDataBuffer();

            // Setup event args
            connectEventArg = new SocketAsyncEventArgs {RemoteEndPoint = Endpoint};
            connectEventArg.Completed += OnAsyncCompleted;
            _receiveEventArg = new SocketAsyncEventArgs();
            _receiveEventArg.Completed += OnAsyncCompleted;
            _sendEventArg = new SocketAsyncEventArgs();
            _sendEventArg.Completed += OnAsyncCompleted;

            // Create a new client socket
            Socket = new Socket(Endpoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            // Apply the option: dual mode (this option must be applied before connecting)
            if (Socket.AddressFamily == AddressFamily.InterNetworkV6)
            {
                Socket.DualMode = OptionDualMode;
            }

            // Async connect to the server
            IsConnecting = true;
            if (!Socket.ConnectAsync(connectEventArg))
            {
                ProcessConnect(connectEventArg);
            }

            return true;
        }

        /// <summary>
        ///     Send data to the server (synchronous)
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

            // Sent data to the server
            Int64 sent = Socket.Send(buffer, (Int32) offset, (Int32) size, SocketFlags.None, out SocketError ec);
            if (sent > 0)
            {
                // Update statistic
                BytesSent += sent;

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
        ///     Send data to the server (asynchronous)
        /// </summary>
        /// <param name="buffer">Buffer to send</param>
        /// <param name="offset">Buffer offset</param>
        /// <param name="size">Buffer size</param>
        /// <returns>'true' if the data was successfully sent, 'false' if the client is not connected</returns>
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
                Boolean sendRequired = sendBufferMain.IsEmpty || sendBuffer.IsEmpty;

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
        ///     Receive data from the server (synchronous)
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

            // Receive data from the server
            Int64 received = Socket.Receive(buffer, (Int32) offset, (Int32) size, SocketFlags.None, out SocketError ec);
            if (received > 0)
            {
                // Update statistic
                BytesReceived += received;

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
                    _receiveEventArg.SetBuffer(receiveBuffer.Data, 0, (Int32) receiveBuffer.Capacity);
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
                    if (sendBuffer.IsEmpty)
                    {
                        // Swap flush and main buffers
                        sendBuffer = Interlocked.Exchange(ref sendBufferMain, sendBuffer);
                        sendBufferFlushOffset = 0;

                        // Update statistic
                        BytesPending = 0;
                        BytesSending += sendBuffer.Size;

                        sending = !sendBuffer.IsEmpty;
                    }
                    else
                    {
                        return;
                    }
                }

                // Check if the flush buffer is empty
                if (sendBuffer.IsEmpty)
                {
                    // Call the empty send buffer handler
                    OnEmpty();
                    return;
                }

                try
                {
                    // Async write with the write handler
                    _sendEventArg.SetBuffer(sendBuffer.Data, (Int32) sendBufferFlushOffset, (Int32) (sendBuffer.Size - sendBufferFlushOffset));
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
        protected override void OnAsyncCompleted(Object sender, SocketAsyncEventArgs e)
        {
            // Determine which type of operation just completed and call the associated handler
            switch (e.LastOperation)
            {
                case SocketAsyncOperation.Connect:
                    ProcessConnect(e);
                    break;
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
        ///     This method is invoked when an asynchronous connect operation completes
        /// </summary>
        protected override void ProcessConnect(SocketAsyncEventArgs e)
        {
            IsConnecting = false;

            if (e.SocketError == SocketError.Success)
            {
                // Apply the option: keep alive
                if (OptionKeepAlive)
                {
                    Socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);
                }

                // Apply the option: no delay
                if (OptionNoDelay)
                {
                    Socket.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.NoDelay, true);
                }

                // Prepare receive & send buffers
                receiveBuffer.Reserve(OptionReceiveBufferSize);
                sendBufferMain.Reserve(OptionSendBufferSize);
                sendBuffer.Reserve(OptionSendBufferSize);

                // Reset statistic
                BytesPending = 0;
                BytesSending = 0;
                BytesSent = 0;
                BytesReceived = 0;

                // Update the connected flag
                IsConnected = true;

                // Call the client connected handler
                OnConnected();

                // Call the empty send buffer handler
                if (sendBufferMain.IsEmpty)
                {
                    OnEmpty();
                }

                // Try to receive something from the server
                TryReceive();
            }
            else
            {
                // Call the client disconnected handler
                SendError(e.SocketError);
                OnDisconnected();
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

            // Received some data from the server
            if (size > 0)
            {
                // Update statistic
                BytesReceived += size;

                // Call the buffer received handler
                OnReceived(receiveBuffer.Data, 0, size);

                // If the receive buffer is full increase its size
                if (receiveBuffer.Capacity == size)
                {
                    receiveBuffer.Reserve(2 * size);
                }
            }

            receiving = false;

            // Try to receive again if the client is valid
            if (e.SocketError == SocketError.Success)
            {
                // If zero is returned from a read operation, the remote end has closed the connection
                if (size > 0)
                {
                    return true;
                }

                DisconnectAsync();
            }
            else
            {
                SendError(e.SocketError);
                DisconnectAsync();
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

            // Send some data to the server
            if (size > 0)
            {
                // Update statistic
                BytesSending -= size;
                BytesSent += size;

                // Increase the flush buffer offset
                sendBufferFlushOffset += size;

                // Successfully send the whole flush buffer
                if (sendBufferFlushOffset == sendBuffer.Size)
                {
                    // Clear the flush buffer
                    sendBuffer.Clear();
                    sendBufferFlushOffset = 0;
                }

                // Call the buffer sent handler
                OnSent(size, BytesPending + BytesSending);
            }

            sending = false;

            // Try to send again if the client is valid
            if (e.SocketError == SocketError.Success)
            {
                return true;
            }

            SendError(e.SocketError);
            DisconnectAsync();
            return false;
        }
    }
}