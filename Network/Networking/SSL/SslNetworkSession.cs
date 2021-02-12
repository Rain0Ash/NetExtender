using System;
using System.Net.Security;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Exceptions;
using NetExtender.Network.Networking.Common;

namespace NetExtender.Network.Networking.SSL
{
    /// <summary>
    ///     SSL session is used to read and write data from the connected SSL client
    /// </summary>
    /// <remarks>Thread-safe</remarks>
    public class SslNetworkSession : NetworkSession
    {
        private Boolean _disconnecting;

        private NetworkDataBuffer _receiveBuffer;

        // Receive buffer
        private SslStream _sslStream;
        private Guid? _sslStreamId;

        /// <summary>
        ///     Initialize the session with a given server
        /// </summary>
        /// <param name="server">SSL server</param>
        public SslNetworkSession(SslNetworkServer server)
            : base(server.OptionReceiveBufferSize, server.OptionSendBufferSize)
        {
            Server = server;
        }
        
        /// <summary>
        ///     Server
        /// </summary>
        public SslNetworkServer Server { get; }

        /// <summary>
        ///     Is the session handshaked?
        /// </summary>
        public Boolean IsHandshaked { get; private set; }

        /// <summary>
        ///     Connect the session
        /// </summary>
        /// <param name="socket">Session socket</param>
        public override void Connect(Socket socket)
        {
            if (IsConnected)
            {
                throw new AlreadyInitializedException("Already connected");
            }
            
            Socket = socket;

            // Update the session socket disposed flag
            IsSocketDisposed = false;

            // Setup buffers
            _receiveBuffer = new NetworkDataBuffer();
            sendBufferMain = new NetworkDataBuffer();
            sendBufferFlush = new NetworkDataBuffer();

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

            try
            {
                // Create SSL stream
                _sslStreamId = Guid.NewGuid();
                _sslStream = Server.Context.CertificateValidationCallback is not null
                    ? new SslStream(new NetworkStream(Socket, false), false, Server.Context.CertificateValidationCallback)
                    : new SslStream(new NetworkStream(Socket, false), false);

                // Begin the SSL handshake
                _sslStream.BeginAuthenticateAsServer(Server.Context.Certificate, Server.Context.ClientCertificateRequired, Server.Context.Protocols, false, ProcessHandshake,
                    _sslStreamId);
            }
            catch (Exception)
            {
                SendError(SocketError.NotConnected);
                Disconnect();
            }
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

            if (_disconnecting)
            {
                return false;
            }

            // Update the disconnecting flag
            _disconnecting = true;

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

                // Close the session socket
                Socket.Close();

                // Dispose the session socket
                Socket.Dispose();

                // Update the session socket disposed flag
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

            // Call the session disconnected handler
            OnDisconnected();

            // Call the session disconnected handler in the server
            Server.OnDisconnectedInternal(this);

            // Unregister session
            Server.UnregisterSession(Id);

            // Reset the disconnecting flag
            _disconnecting = false;

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
                Interlocked.Add(ref Server.bytesSent, size);

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
        ///     Send data to the client (asynchronous)
        /// </summary>
        /// <param name="buffer">Buffer to send</param>
        /// <param name="offset">Buffer offset</param>
        /// <param name="size">Buffer size</param>
        /// <returns>'true' if the data was successfully sent, 'false' if the session is not connected</returns>
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
                // Receive data from the client
                Int64 received = _sslStream.Read(buffer, (Int32) offset, (Int32) size);
                if (received <= 0)
                {
                    return received;
                }

                // Update statistic
                BytesReceived += received;
                Interlocked.Add(ref Server.bytesReceived, received);

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
                    result = _sslStream.BeginRead(_receiveBuffer.Data, 0, (Int32) _receiveBuffer.Capacity, ProcessReceive, _sslStreamId);
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
                if (sendBufferFlush.IsEmpty)
                {
                    lock (sendLock)
                    {
                        // Swap flush and main buffers
                        sendBufferFlush = Interlocked.Exchange(ref sendBufferMain, sendBufferFlush);
                        sendBufferFlushOffset = 0;

                        // Update statistic
                        BytesPending = 0;
                        BytesSending += sendBufferFlush.Size;

                        sending = !sendBufferFlush.IsEmpty;
                    }
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
                _sslStream.BeginWrite(sendBufferFlush.Data, (Int32) sendBufferFlushOffset, (Int32) (sendBufferFlush.Size - sendBufferFlushOffset), ProcessSend, _sslStreamId);
            }
            catch (ObjectDisposedException)
            {
            }
        }

        /// <summary>
        ///     This method is invoked when an asynchronous handshake operation completes
        /// </summary>
        private void ProcessHandshake(IAsyncResult result)
        {
            try
            {
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
                _sslStream.EndAuthenticateAsServer(result);

                // Update the handshaked flag
                IsHandshaked = true;

                // Call the session handshaked handler
                OnHandshaked();

                // Call the session handshaked handler in the server
                Server.OnHandshakedInternal(this);

                // Call the empty send buffer handler
                if (sendBufferMain.IsEmpty)
                {
                    OnEmpty();
                }

                // Try to receive something from the client
                TryReceive();
            }
            catch (Exception)
            {
                SendError(SocketError.NotConnected);
                Disconnect();
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
                    Disconnect();
                }
            }
            catch (Exception)
            {
                SendError(SocketError.OperationAborted);
                Disconnect();
            }
        }

        /// <summary>
        ///     This method is invoked when an asynchronous send operation completes
        /// </summary>
        private void ProcessSend(IAsyncResult result)
        {
            try
            {
                // Validate SSL stream Id
                Guid? sslStreamId = result.AsyncState as Guid?;
                if (_sslStreamId != sslStreamId)
                {
                    return;
                }

                if (!IsHandshaked)
                {
                    return;
                }

                // End the SSL write
                _sslStream.EndWrite(result);

                Int64 size = sendBufferFlush.Size;

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
                TrySend();
            }
            catch (Exception)
            {
                SendError(SocketError.OperationAborted);
                Disconnect();
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