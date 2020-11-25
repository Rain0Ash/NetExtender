using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using NetExtender.Network.Networking.Common;

namespace NetExtender.Network.Networking.UDP
{
    /// <summary>
    ///     UDP server is used to send or multicast datagrams to UDP endpoints
    /// </summary>
    /// <remarks>Thread-safe</remarks>
    public class UdpNetworkServer : NetworkServer
    {
        private NetworkDataBuffer _receiveBuffer;

        // Receive and send endpoints
        private EndPoint _receiveEndpoint;

        private SocketAsyncEventArgs _receiveEventArg;

        // Receive buffer
        private Boolean _receiving;
        private NetworkDataBuffer _sendBuffer;

        private EndPoint _sendEndpoint;
        private SocketAsyncEventArgs _sendEventArg;

        // Send buffer
        private Boolean _sending;

        /// <summary>
        ///     Initialize UDP server with a given IP address and port number
        /// </summary>
        /// <param name="address">IP address</param>
        /// <param name="port">Port number</param>
        public UdpNetworkServer(IPAddress address, Int32 port)
            : this(new IPEndPoint(address, port))
        {
        }

        /// <summary>
        ///     Initialize UDP server with a given IP address and port number
        /// </summary>
        /// <param name="address">IP address</param>
        /// <param name="port">Port number</param>
        public UdpNetworkServer(String address, Int32 port)
            : this(new IPEndPoint(IPAddress.Parse(address), port))
        {
        }

        /// <summary>
        ///     Initialize UDP server with a given IP endpoint
        /// </summary>
        /// <param name="endpoint">IP endpoint</param>
        public UdpNetworkServer(IPEndPoint endpoint)
            : base(endpoint)
        {
        }

        /// <summary>
        ///     Multicast IP endpoint
        /// </summary>
        public IPEndPoint MulticastEndpoint { get; private set; }

        /// <summary>
        ///     Socket
        /// </summary>
        public Socket Socket { get; private set; }

        /// <summary>
        ///     Number of bytes sending by the server
        /// </summary>
        public Int64 BytesSending { get; private set; }

        /// <summary>
        ///     Number of datagrams sent by the server
        /// </summary>
        public Int64 DatagramsSent { get; private set; }

        /// <summary>
        ///     Number of datagrams received by the server
        /// </summary>
        public Int64 DatagramsReceived { get; private set; }

        /// <summary>
        ///     Start the server (synchronous)
        /// </summary>
        /// <returns>'true' if the server was successfully started, 'false' if the server failed to start</returns>
        public override Boolean Start()
        {
            if (IsStarted)
            {
                return false;
            }

            // Setup buffers
            _receiveBuffer = new NetworkDataBuffer();
            _sendBuffer = new NetworkDataBuffer();

            // Setup event args
            _receiveEventArg = new SocketAsyncEventArgs();
            _receiveEventArg.Completed += OnAsyncCompleted;
            _sendEventArg = new SocketAsyncEventArgs();
            _sendEventArg.Completed += OnAsyncCompleted;

            // Create a new server socket
            Socket = new Socket(Endpoint.AddressFamily, SocketType.Dgram, ProtocolType.Udp);

            // Update the server socket disposed flag
            IsSocketDisposed = false;

            // Apply the option: reuse address
            Socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, OptionReuseAddress);
            // Apply the option: exclusive address use
            Socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ExclusiveAddressUse, OptionExclusiveAddressUse);
            // Apply the option: dual mode (this option must be applied before recieving)
            if (Socket.AddressFamily == AddressFamily.InterNetworkV6)
            {
                Socket.DualMode = OptionDualMode;
            }

            // Bind the server socket to the IP endpoint
            Socket.Bind(Endpoint);
            // Refresh the endpoint property based on the actual endpoint created
            Endpoint = (IPEndPoint) Socket.LocalEndPoint;

            // Prepare receive endpoint
            _receiveEndpoint = new IPEndPoint(Endpoint.AddressFamily == AddressFamily.InterNetworkV6 ? IPAddress.IPv6Any : IPAddress.Any, 0);

            // Prepare receive & send buffers
            _receiveBuffer.Reserve(OptionReceiveBufferSize);

            // Reset statistic
            BytesPending = 0;
            BytesSending = 0;
            BytesSent = 0;
            BytesReceived = 0;
            DatagramsSent = 0;
            DatagramsReceived = 0;

            // Update the started flag
            IsStarted = true;

            // Call the server started handler
            OnStarted();

            return true;
        }

        /// <summary>
        ///     Start the server with a given multicast IP address and port number (synchronous)
        /// </summary>
        /// <param name="multicastAddress">Multicast IP address</param>
        /// <param name="multicastPort">Multicast port number</param>
        /// <returns>'true' if the server was successfully started, 'false' if the server failed to start</returns>
        public virtual Boolean Start(IPAddress multicastAddress, Int32 multicastPort)
        {
            return Start(new IPEndPoint(multicastAddress, multicastPort));
        }

        /// <summary>
        ///     Start the server with a given multicast IP address and port number (synchronous)
        /// </summary>
        /// <param name="multicastAddress">Multicast IP address</param>
        /// <param name="multicastPort">Multicast port number</param>
        /// <returns>'true' if the server was successfully started, 'false' if the server failed to start</returns>
        public virtual Boolean Start(String multicastAddress, Int32 multicastPort)
        {
            return Start(new IPEndPoint(IPAddress.Parse(multicastAddress), multicastPort));
        }

        /// <summary>
        ///     Start the server with a given multicast endpoint (synchronous)
        /// </summary>
        /// <param name="multicastEndpoint">Multicast IP endpoint</param>
        /// <returns>'true' if the server was successfully started, 'false' if the server failed to start</returns>
        public virtual Boolean Start(IPEndPoint multicastEndpoint)
        {
            MulticastEndpoint = multicastEndpoint;
            return Start();
        }

        /// <summary>
        ///     Stop the server (synchronous)
        /// </summary>
        /// <returns>'true' if the server was successfully stopped, 'false' if the server is already stopped</returns>
        public override Boolean Stop()
        {
            if (!IsStarted)
            {
                return false;
            }

            // Reset event args
            _receiveEventArg.Completed -= OnAsyncCompleted;
            _sendEventArg.Completed -= OnAsyncCompleted;

            try
            {
                // Close the server socket
                Socket.Close();

                // Dispose the server socket
                Socket.Dispose();

                // Dispose event arguments
                _receiveEventArg.Dispose();
                _sendEventArg.Dispose();

                // Update the server socket disposed flag
                IsSocketDisposed = false;
            }
            catch (ObjectDisposedException)
            {
            }

            // Update the started flag
            IsStarted = false;

            // Update sending/receiving flags
            _receiving = false;
            _sending = false;

            // Clear send/receive buffers
            ClearBuffers();

            // Call the server stopped handler
            OnStopped();

            return true;
        }

        /// <summary>
        ///     Restart the server (synchronous)
        /// </summary>
        /// <returns>'true' if the server was successfully restarted, 'false' if the server failed to restart</returns>
        public override Boolean Restart()
        {
            return Stop() && Start();
        }

        /// <summary>
        ///     Multicast datagram to the prepared mulicast endpoint (synchronous)
        /// </summary>
        /// <param name="buffer">Datagram buffer to multicast</param>
        /// <returns>Size of multicasted datagram</returns>
        public virtual Int64 Multicast(Byte[] buffer)
        {
            return Multicast(buffer, 0, buffer.Length);
        }

        /// <summary>
        ///     Multicast datagram to the prepared mulicast endpoint (synchronous)
        /// </summary>
        /// <param name="buffer">Datagram buffer to multicast</param>
        /// <param name="offset">Datagram buffer offset</param>
        /// <param name="size">Datagram buffer size</param>
        /// <returns>Size of multicasted datagram</returns>
        public virtual Int64 Multicast(Byte[] buffer, Int64 offset, Int64 size)
        {
            return Send(MulticastEndpoint, buffer, offset, size);
        }

        /// <summary>
        ///     Multicast text to the prepared mulicast endpoint (synchronous)
        /// </summary>
        /// <param name="text">Text string to multicast</param>
        /// <returns>Size of multicasted datagram</returns>
        public virtual Int64 Multicast(String text)
        {
            return Multicast(Encoding.UTF8.GetBytes(text));
        }

        /// <summary>
        ///     Multicast datagram to the prepared mulicast endpoint (asynchronous)
        /// </summary>
        /// <param name="buffer">Datagram buffer to multicast</param>
        /// <returns>'true' if the datagram was successfully multicasted, 'false' if the datagram was not multicasted</returns>
        public virtual Boolean MulticastAsync(Byte[] buffer)
        {
            return MulticastAsync(buffer, 0, buffer.Length);
        }

        /// <summary>
        ///     Multicast datagram to the prepared mulicast endpoint (asynchronous)
        /// </summary>
        /// <param name="buffer">Datagram buffer to multicast</param>
        /// <param name="offset">Datagram buffer offset</param>
        /// <param name="size">Datagram buffer size</param>
        /// <returns>'true' if the datagram was successfully multicasted, 'false' if the datagram was not multicasted</returns>
        public virtual Boolean MulticastAsync(Byte[] buffer, Int64 offset, Int64 size)
        {
            return SendAsync(MulticastEndpoint, buffer, offset, size);
        }

        /// <summary>
        ///     Multicast text to the prepared mulicast endpoint (asynchronous)
        /// </summary>
        /// <param name="text">Text string to multicast</param>
        /// <returns>'true' if the text was successfully multicasted, 'false' if the text was not multicasted</returns>
        public virtual Boolean MulticastAsync(String text)
        {
            return MulticastAsync(Encoding.UTF8.GetBytes(text));
        }

        /// <summary>
        ///     Send datagram to the connected server (synchronous)
        /// </summary>
        /// <param name="buffer">Datagram buffer to send</param>
        /// <returns>Size of sent datagram</returns>
        public virtual Int64 Send(Byte[] buffer)
        {
            return Send(buffer, 0, buffer.Length);
        }

        /// <summary>
        ///     Send datagram to the connected server (synchronous)
        /// </summary>
        /// <param name="buffer">Datagram buffer to send</param>
        /// <param name="offset">Datagram buffer offset</param>
        /// <param name="size">Datagram buffer size</param>
        /// <returns>Size of sent datagram</returns>
        public virtual Int64 Send(Byte[] buffer, Int64 offset, Int64 size)
        {
            return Send(Endpoint, buffer, offset, size);
        }

        /// <summary>
        ///     Send text to the connected server (synchronous)
        /// </summary>
        /// <param name="text">Text string to send</param>
        /// <returns>Size of sent datagram</returns>
        public virtual Int64 Send(String text)
        {
            return Send(Encoding.UTF8.GetBytes(text));
        }

        /// <summary>
        ///     Send datagram to the given endpoint (synchronous)
        /// </summary>
        /// <param name="endpoint">Endpoint to send</param>
        /// <param name="buffer">Datagram buffer to send</param>
        /// <returns>Size of sent datagram</returns>
        public virtual Int64 Send(EndPoint endpoint, Byte[] buffer)
        {
            return Send(endpoint, buffer, 0, buffer.Length);
        }

        /// <summary>
        ///     Send datagram to the given endpoint (synchronous)
        /// </summary>
        /// <param name="endpoint">Endpoint to send</param>
        /// <param name="buffer">Datagram buffer to send</param>
        /// <param name="offset">Datagram buffer offset</param>
        /// <param name="size">Datagram buffer size</param>
        /// <returns>Size of sent datagram</returns>
        public virtual Int64 Send(EndPoint endpoint, Byte[] buffer, Int64 offset, Int64 size)
        {
            if (!IsStarted)
            {
                return 0;
            }

            if (size == 0)
            {
                return 0;
            }

            try
            {
                // Sent datagram to the client
                Int32 sent = Socket.SendTo(buffer, (Int32) offset, (Int32) size, SocketFlags.None, endpoint);
                if (sent <= 0)
                {
                    return sent;
                }

                // Update statistic
                DatagramsSent++;
                BytesSent += sent;

                // Call the datagram sent handler
                OnSent(endpoint, sent);

                return sent;
            }
            catch (ObjectDisposedException)
            {
                return 0;
            }
            catch (SocketException ex)
            {
                SendError(ex.SocketErrorCode);
                return 0;
            }
        }

        /// <summary>
        ///     Send text to the given endpoint (synchronous)
        /// </summary>
        /// <param name="endpoint">Endpoint to send</param>
        /// <param name="text">Text string to send</param>
        /// <returns>Size of sent datagram</returns>
        public virtual Int64 Send(EndPoint endpoint, String text)
        {
            return Send(endpoint, Encoding.UTF8.GetBytes(text));
        }

        /// <summary>
        ///     Send datagram to the given endpoint (asynchronous)
        /// </summary>
        /// <param name="endpoint">Endpoint to send</param>
        /// <param name="buffer">Datagram buffer to send</param>
        /// <returns>'true' if the datagram was successfully sent, 'false' if the datagram was not sent</returns>
        public virtual Boolean SendAsync(EndPoint endpoint, Byte[] buffer)
        {
            return SendAsync(endpoint, buffer, 0, buffer.Length);
        }

        /// <summary>
        ///     Send datagram to the given endpoint (asynchronous)
        /// </summary>
        /// <param name="endpoint">Endpoint to send</param>
        /// <param name="buffer">Datagram buffer to send</param>
        /// <param name="offset">Datagram buffer offset</param>
        /// <param name="size">Datagram buffer size</param>
        /// <returns>'true' if the datagram was successfully sent, 'false' if the datagram was not sent</returns>
        public virtual Boolean SendAsync(EndPoint endpoint, Byte[] buffer, Int64 offset, Int64 size)
        {
            if (_sending)
            {
                return false;
            }

            if (!IsStarted)
            {
                return false;
            }

            if (size == 0)
            {
                return true;
            }

            // Fill the main send buffer
            _sendBuffer.Append(buffer, offset, size);

            // Update statistic
            BytesSending = _sendBuffer.Size;

            // Update send endpoint
            _sendEndpoint = endpoint;

            // Try to send the main buffer
            Task.Factory.StartNew(TrySend);

            return true;
        }

        /// <summary>
        ///     Send text to the given endpoint (asynchronous)
        /// </summary>
        /// <param name="endpoint">Endpoint to send</param>
        /// <param name="text">Text string to send</param>
        /// <returns>'true' if the text was successfully sent, 'false' if the text was not sent</returns>
        public virtual Boolean SendAsync(EndPoint endpoint, String text)
        {
            return SendAsync(endpoint, Encoding.UTF8.GetBytes(text));
        }

        /// <summary>
        ///     Receive a new datagram from the given endpoint (synchronous)
        /// </summary>
        /// <param name="endpoint">Endpoint to receive from</param>
        /// <param name="buffer">Datagram buffer to receive</param>
        /// <returns>Size of received datagram</returns>
        public virtual Int64 Receive(ref EndPoint endpoint, Byte[] buffer)
        {
            return Receive(ref endpoint, buffer, 0, buffer.Length);
        }

        /// <summary>
        ///     Receive a new datagram from the given endpoint (synchronous)
        /// </summary>
        /// <param name="endpoint">Endpoint to receive from</param>
        /// <param name="buffer">Datagram buffer to receive</param>
        /// <param name="offset">Datagram buffer offset</param>
        /// <param name="size">Datagram buffer size</param>
        /// <returns>Size of received datagram</returns>
        public virtual Int64 Receive(ref EndPoint endpoint, Byte[] buffer, Int64 offset, Int64 size)
        {
            if (!IsStarted)
            {
                return 0;
            }

            if (size == 0)
            {
                return 0;
            }

            try
            {
                // Receive datagram from the client
                Int32 received = Socket.ReceiveFrom(buffer, (Int32) offset, (Int32) size, SocketFlags.None, ref endpoint);
                if (received <= 0)
                {
                    return received;
                }

                // Update statistic
                DatagramsReceived++;
                BytesReceived += received;

                // Call the datagram received handler
                OnReceived(endpoint, buffer, offset, size);

                return received;
            }
            catch (ObjectDisposedException)
            {
                return 0;
            }
            catch (SocketException ex)
            {
                SendError(ex.SocketErrorCode);
                return 0;
            }
        }

        /// <summary>
        ///     Receive text from the given endpoint (synchronous)
        /// </summary>
        /// <param name="endpoint">Endpoint to receive from</param>
        /// <param name="size">Text size to receive</param>
        /// <returns>Received text</returns>
        public virtual String Receive(ref EndPoint endpoint, Int64 size)
        {
            Byte[] buffer = new Byte[size];
            Int64 length = Receive(ref endpoint, buffer);
            return Encoding.UTF8.GetString(buffer, 0, (Int32) length);
        }

        /// <summary>
        ///     Receive datagram from the client (asynchronous)
        /// </summary>
        public virtual void ReceiveAsync()
        {
            // Try to receive datagram
            TryReceive();
        }

        /// <summary>
        ///     Try to receive new data
        /// </summary>
        private void TryReceive()
        {
            if (_receiving)
            {
                return;
            }

            if (!IsStarted)
            {
                return;
            }

            try
            {
                // Async receive with the receive handler
                _receiving = true;
                _receiveEventArg.RemoteEndPoint = _receiveEndpoint;
                _receiveEventArg.SetBuffer(_receiveBuffer.Data, 0, (Int32) _receiveBuffer.Capacity);
                if (!Socket.ReceiveFromAsync(_receiveEventArg))
                {
                    ProcessReceiveFrom(_receiveEventArg);
                }
            }
            catch (ObjectDisposedException)
            {
            }
        }

        /// <summary>
        ///     Try to send pending data
        /// </summary>
        private void TrySend()
        {
            if (_sending)
            {
                return;
            }

            if (!IsStarted)
            {
                return;
            }

            try
            {
                // Async write with the write handler
                _sending = true;
                _sendEventArg.RemoteEndPoint = _sendEndpoint;
                _sendEventArg.SetBuffer(_sendBuffer.Data, 0, (Int32) _sendBuffer.Size);
                if (!Socket.SendToAsync(_sendEventArg))
                {
                    ProcessSendTo(_sendEventArg);
                }
            }
            catch (ObjectDisposedException)
            {
            }
        }

        /// <summary>
        ///     Clear send/receive buffers
        /// </summary>
        private void ClearBuffers()
        {
            // Clear send buffers
            _sendBuffer.Clear();

            // Update statistic
            BytesPending = 0;
            BytesSending = 0;
        }

        /// <summary>
        ///     This method is called whenever a receive or send operation is completed on a socket
        /// </summary>
        private void OnAsyncCompleted(Object sender, SocketAsyncEventArgs e)
        {
            // Determine which type of operation just completed and call the associated handler
            switch (e.LastOperation)
            {
                case SocketAsyncOperation.ReceiveFrom:
                    ProcessReceiveFrom(e);
                    break;
                case SocketAsyncOperation.SendTo:
                    ProcessSendTo(e);
                    break;
                default:
                    throw new ArgumentException("The last operation completed on the socket was not a receive or send");
            }
        }

        /// <summary>
        ///     This method is invoked when an asynchronous receive from operation completes
        /// </summary>
        private void ProcessReceiveFrom(SocketAsyncEventArgs e)
        {
            _receiving = false;

            if (!IsStarted)
            {
                return;
            }

            // Check for error
            if (e.SocketError != SocketError.Success)
            {
                SendError(e.SocketError);

                // Call the datagram received zero handler
                OnReceived(e.RemoteEndPoint, _receiveBuffer.Data, 0, 0);

                return;
            }

            Int64 size = e.BytesTransferred;

            // Received some data from the client
            if (size <= 0)
            {
                return;
            }

            // Update statistic
            DatagramsReceived++;
            BytesReceived += size;

            // Call the datagram received handler
            OnReceived(e.RemoteEndPoint, _receiveBuffer.Data, 0, size);

            // If the receive buffer is full increase its size
            if (_receiveBuffer.Capacity == size)
            {
                _receiveBuffer.Reserve(2 * size);
            }
        }

        /// <summary>
        ///     This method is invoked when an asynchronous send to operation completes
        /// </summary>
        private void ProcessSendTo(SocketAsyncEventArgs e)
        {
            _sending = false;

            if (!IsStarted)
            {
                return;
            }

            // Check for error
            if (e.SocketError != SocketError.Success)
            {
                SendError(e.SocketError);

                // Call the buffer sent zero handler
                OnSent(_sendEndpoint, 0);

                return;
            }

            Int64 sent = e.BytesTransferred;

            // Send some data to the client
            if (sent <= 0)
            {
                return;
            }

            // Update statistic
            BytesSending = 0;
            BytesSent += sent;

            // Clear the send buffer
            _sendBuffer.Clear();

            // Call the buffer sent handler
            OnSent(_sendEndpoint, sent);
        }

        /// <summary>
        ///     Handle datagram received notification
        /// </summary>
        /// <param name="endpoint">Received endpoint</param>
        /// <param name="buffer">Received datagram buffer</param>
        /// <param name="offset">Received datagram buffer offset</param>
        /// <param name="size">Received datagram buffer size</param>
        /// <remarks>
        ///     Notification is called when another datagram was received from some endpoint
        /// </remarks>
        protected virtual void OnReceived(EndPoint endpoint, Byte[] buffer, Int64 offset, Int64 size)
        {
        }

        /// <summary>
        ///     Handle datagram sent notification
        /// </summary>
        /// <param name="endpoint">Endpoint of sent datagram</param>
        /// <param name="sent">Size of sent datagram buffer</param>
        /// <remarks>
        ///     Notification is called when a datagram was sent to the client.
        ///     This handler could be used to send another datagram to the client for instance when the pending size is zero.
        /// </remarks>
        [SuppressMessage("ReSharper", "UnusedParameter.Global")]
        protected virtual void OnSent(EndPoint endpoint, Int64 sent)
        {
        }
    }
}