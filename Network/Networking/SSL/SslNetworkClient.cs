using System;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Network.Networking.Common;

namespace NetExtender.Network.Networking.SSL
{
    /// <summary>
    ///     SSL client is used to read/write data from/into the connected SSL server
    /// </summary>
    /// <remarks>Thread-safe</remarks>
    public class SslNetworkClient : NetworkConnectionClient
    {
        // Send buffer
        private Boolean _disconnecting;

        // Receive buffer
        private SslStream _sslStream;
        private Guid? _sslStreamId;

        /// <summary>
        ///     Initialize SSL client with a given server IP address and port number
        /// </summary>
        /// <param name="context">SSL context</param>
        /// <param name="address">IP address</param>
        /// <param name="port">Port number</param>
        public SslNetworkClient(SslNetworkContext context, IPAddress address, Int32 port)
            : this(context, new IPEndPoint(address, port))
        {
        }

        /// <summary>
        ///     Initialize SSL client with a given server IP address and port number
        /// </summary>
        /// <param name="context">SSL context</param>
        /// <param name="address">IP address</param>
        /// <param name="port">Port number</param>
        public SslNetworkClient(SslNetworkContext context, String address, Int32 port)
            : this(context, new IPEndPoint(IPAddress.Parse(address), port))
        {
            Address = address;
        }

        /// <summary>
        ///     Initialize SSL client with a given IP endpoint
        /// </summary>
        /// <param name="context">SSL context</param>
        /// <param name="endpoint">IP endpoint</param>
        public SslNetworkClient(SslNetworkContext context, IPEndPoint endpoint)
            : base(endpoint)
        {
            Address = endpoint.Address.ToString();
            Context = context;
        }

        /// <summary>
        ///     SSL server DNS address
        /// </summary>
        public String Address { get; set; }

        /// <summary>
        ///     SSL context
        /// </summary>
        public SslNetworkContext Context { get; }

        /// <summary>
        ///     Is the client handshaking?
        /// </summary>
        public Boolean IsHandshaking { get; private set; }

        /// <summary>
        ///     Is the client handshaked?
        /// </summary>
        public Boolean IsHandshaked { get; private set; }

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
            if (IsConnected || IsHandshaked || IsConnecting || IsHandshaking)
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

            try
            {
                // Create SSL stream
                _sslStreamId = Guid.NewGuid();
                _sslStream = Context.CertificateValidationCallback is not null
                    ? new SslStream(new NetworkStream(Socket, false), false, Context.CertificateValidationCallback)
                    : new SslStream(new NetworkStream(Socket, false), false);

                // SSL handshake
                _sslStream.AuthenticateAsClient(Address, Context.Certificates ?? new X509CertificateCollection(new[] {Context.Certificate}), Context.Protocols, true);
            }
            catch (Exception)
            {
                SendError(SocketError.NotConnected);
                DisconnectAsync();
                return false;
            }

            // Update the handshaked flag
            IsHandshaked = true;

            // Call the session handshaked handler
            OnHandshaked();

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

            if (_disconnecting)
            {
                return false;
            }

            // Reset connecting & handshaking flags
            IsConnecting = false;
            IsHandshaking = false;

            // Update the disconnecting flag
            _disconnecting = true;

            // Reset event args
            connectEventArg.Completed -= OnAsyncCompleted;

            try
            {
                try
                {
                    // Shutdown the SSL stream
                    // ReSharper disable once AsyncConverter.AsyncWait
                    _sslStream.ShutdownAsync().Wait();
                }
                catch (Exception)
                {
                    // ignored
                }

                // Dispose the SSL stream & buffer
                _sslStream.Dispose();
                _sslStreamId = null;

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

                // Update the client socket disposed flag
                IsSocketDisposed = true;
            }
            catch (ObjectDisposedException)
            {
            }

            // Update the handshaked flag
            IsHandshaked = false;

            // Update the connected flag
            IsConnected = false;

            // Update sending/receiving flags
            receiving = false;
            sending = false;

            // Clear send/receive buffers
            ClearBuffers();

            // Call the client disconnected handler
            OnDisconnected();

            // Reset the disconnecting flag
            _disconnecting = false;

            return true;
        }

        /// <summary>
        ///     Connect the client (asynchronous)
        /// </summary>
        /// <returns>'true' if the client was successfully connected, 'false' if the client failed to connect</returns>
        public override Boolean ConnectAsync()
        {
            if (IsConnected || IsHandshaked || IsConnecting || IsHandshaking)
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
            if (!IsHandshaked)
            {
                return 0;
            }

            if (size == 0)
            {
                return 0;
            }

            try
            {
                // Sent data to the server
                _sslStream.Write(buffer, (Int32) offset, (Int32) size);

                // Update statistic
                BytesSent += size;

                // Call the buffer sent handler
                OnSent(size, BytesPending + BytesSending);

                return size;
            }
            catch (Exception)
            {
                SendError(SocketError.OperationAborted);
                Disconnect();
                return 0;
            }
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
            if (!IsHandshaked)
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
            if (!IsHandshaked)
            {
                return 0;
            }

            if (size == 0)
            {
                return 0;
            }

            try
            {
                // Receive data from the server
                Int64 received = _sslStream.Read(buffer, (Int32) offset, (Int32) size);
                if (received <= 0)
                {
                    return received;
                }

                // Update statistic
                BytesReceived += received;

                // Call the buffer received handler
                OnReceived(buffer, 0, received);

                return received;
            }
            catch (Exception)
            {
                SendError(SocketError.OperationAborted);
                Disconnect();
                return 0;
            }
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

            if (!IsHandshaked)
            {
                return;
            }

            try
            {
                // Async receive with the receive handler
                IAsyncResult result;
                do
                {
                    if (!IsHandshaked)
                    {
                        return;
                    }

                    receiving = true;
                    result = _sslStream.BeginRead(receiveBuffer.Data, 0, (Int32) receiveBuffer.Capacity, ProcessReceive, _sslStreamId);
                } while (result.CompletedSynchronously);
            }
            catch (ObjectDisposedException)
            {
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

            if (!IsHandshaked)
            {
                return;
            }

            lock (sendLock)
            {
                if (sending)
                {
                    return;
                }

                // Swap send buffers
                if (sendBuffer.IsEmpty)
                {
                    lock (sendLock)
                    {
                        // Swap flush and main buffers
                        sendBuffer = Interlocked.Exchange(ref sendBufferMain, sendBuffer);
                        sendBufferFlushOffset = 0;

                        // Update statistic
                        BytesPending = 0;
                        BytesSending += sendBuffer.Size;

                        sending = !sendBuffer.IsEmpty;
                    }
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
                _sslStream.BeginWrite(sendBuffer.Data, (Int32) sendBufferFlushOffset, (Int32) (sendBuffer.Size - sendBufferFlushOffset), ProcessSend, _sslStreamId);
            }
            catch (ObjectDisposedException)
            {
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

                try
                {
                    // Create SSL stream
                    _sslStreamId = Guid.NewGuid();
                    _sslStream = Context.CertificateValidationCallback is not null
                        ? new SslStream(new NetworkStream(Socket, false), false, Context.CertificateValidationCallback)
                        : new SslStream(new NetworkStream(Socket, false), false);

                    // Begin the SSL handshake
                    IsHandshaking = true;
                    _sslStream.BeginAuthenticateAsClient(Address, Context.Certificates ?? new X509CertificateCollection(new[] {Context.Certificate}), Context.Protocols, true,
                        ProcessHandshake, _sslStreamId);
                }
                catch (Exception)
                {
                    SendError(SocketError.NotConnected);
                    DisconnectAsync();
                }
            }
            else
            {
                // Call the client disconnected handler
                SendError(e.SocketError);
                OnDisconnected();
            }
        }

        /// <summary>
        ///     This method is invoked when an asynchronous handshake operation completes
        /// </summary>
        private void ProcessHandshake(IAsyncResult result)
        {
            try
            {
                IsHandshaking = false;

                if (IsHandshaked)
                {
                    return;
                }

                // Validate SSL stream Id
                Guid? sslStreamId = result.AsyncState as Guid?;
                if (_sslStreamId != sslStreamId)
                {
                    return;
                }

                // End the SSL handshake
                _sslStream.EndAuthenticateAsClient(result);

                // Update the handshaked flag
                IsHandshaked = true;

                // Call the session handshaked handler
                OnHandshaked();

                // Call the empty send buffer handler
                if (sendBufferMain.IsEmpty)
                {
                    OnEmpty();
                }

                // Try to receive something from the server
                TryReceive();
            }
            catch (Exception)
            {
                SendError(SocketError.NotConnected);
                DisconnectAsync();
            }
        }

        /// <summary>
        ///     This method is invoked when an asynchronous receive operation completes
        /// </summary>
        private void ProcessReceive(IAsyncResult result)
        {
            try
            {
                if (!IsHandshaked)
                {
                    return;
                }

                // Validate SSL stream Id
                Guid? sslStreamId = result.AsyncState as Guid?;
                if (_sslStreamId != sslStreamId)
                {
                    return;
                }

                // End the SSL read
                Int64 size = _sslStream.EndRead(result);

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

                // If zero is returned from a read operation, the remote end has closed the connection
                if (size > 0)
                {
                    if (!result.CompletedSynchronously)
                    {
                        TryReceive();
                    }
                }
                else
                {
                    DisconnectAsync();
                }
            }
            catch (Exception)
            {
                SendError(SocketError.OperationAborted);
                DisconnectAsync();
            }
        }

        /// <summary>
        ///     This method is invoked when an asynchronous send operation completes
        /// </summary>
        private void ProcessSend(IAsyncResult result)
        {
            try
            {
                if (!IsHandshaked)
                {
                    return;
                }

                // Validate SSL stream Id
                Guid? sslStreamId = result.AsyncState as Guid?;
                if (_sslStreamId != sslStreamId)
                {
                    return;
                }

                // End the SSL write
                _sslStream.EndWrite(result);

                Int64 size = sendBuffer.Size;

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
                TrySend();
            }
            catch (Exception)
            {
                SendError(SocketError.OperationAborted);
                DisconnectAsync();
            }
        }

        /// <summary>
        ///     Handle client handshaked notification
        /// </summary>
        protected virtual void OnHandshaked()
        {
        }
    }
}