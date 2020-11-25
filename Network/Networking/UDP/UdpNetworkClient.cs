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
    ///     UDP client is used to read/write data from/into the connected UDP server
    /// </summary>
    /// <remarks>Thread-safe</remarks>
    public class UdpNetworkClient : NetworkClient
    {
        // Receive and send endpoints
        private EndPoint _receiveEndpoint;

        private SocketAsyncEventArgs _receiveEventArg;

        // Receive buffer

        private EndPoint _sendEndpoint;
        private SocketAsyncEventArgs _sendEventArg;

        /// <summary>
        ///     Initialize UDP client with a given server IP address and port number
        /// </summary>
        /// <param name="address">IP address</param>
        /// <param name="port">Port number</param>
        public UdpNetworkClient(IPAddress address, Int32 port)
            : this(new IPEndPoint(address, port))
        {
        }

        /// <summary>
        ///     Initialize UDP client with a given server IP address and port number
        /// </summary>
        /// <param name="address">IP address</param>
        /// <param name="port">Port number</param>
        public UdpNetworkClient(String address, Int32 port)
            : this(new IPEndPoint(IPAddress.Parse(address), port))
        {
        }

        /// <summary>
        ///     Initialize UDP client with a given IP endpoint
        /// </summary>
        /// <param name="endpoint">IP endpoint</param>
        public UdpNetworkClient(IPEndPoint endpoint)    
            : base(endpoint)
        {
        }

        /// <summary>
        ///     Number of datagrams sent by the client
        /// </summary>
        public Int64 DatagramsSent { get; private set; }

        /// <summary>
        ///     Number of datagrams received by the client
        /// </summary>
        public Int64 DatagramsReceived { get; private set; }

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
        ///     Option: bind the socket to the multicast UDP server
        /// </summary>
        public Boolean OptionMulticast { get; set; }

        /// <summary>
        ///     Connect the client (synchronous)
        /// </summary>
        /// <returns>'true' if the client was successfully connected, 'false' if the client failed to connect</returns>
        public override Boolean Connect()
        {
            if (IsConnected)
            {
                return false;
            }

            // Setup buffers
            receiveBuffer = new NetworkDataBuffer();
            sendBuffer = new NetworkDataBuffer();

            // Setup event args
            _receiveEventArg = new SocketAsyncEventArgs();
            _receiveEventArg.Completed += OnAsyncCompleted;
            _sendEventArg = new SocketAsyncEventArgs();
            _sendEventArg.Completed += OnAsyncCompleted;

            // Create a new client socket
            Socket = new Socket(Endpoint.AddressFamily, SocketType.Dgram, ProtocolType.Udp);

            // Update the client socket disposed flag
            IsSocketDisposed = false;

            // Apply the option: reuse address
            Socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, OptionReuseAddress);
            // Apply the option: exclusive address use
            Socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ExclusiveAddressUse, OptionExclusiveAddressUse);
            // Apply the option: dual mode (this option must be applied before recieving/sending)
            if (Socket.AddressFamily == AddressFamily.InterNetworkV6)
            {
                Socket.DualMode = OptionDualMode;
            }

            // Bind the acceptor socket to the IP endpoint
            if (OptionMulticast)
            {
                Socket.Bind(Endpoint);
            }
            else
            {
                IPEndPoint endpoint = new IPEndPoint(Endpoint.AddressFamily == AddressFamily.InterNetworkV6 ? IPAddress.IPv6Any : IPAddress.Any, 0);
                Socket.Bind(endpoint);
            }

            // Prepare receive endpoint
            _receiveEndpoint = new IPEndPoint(Endpoint.AddressFamily == AddressFamily.InterNetworkV6 ? IPAddress.IPv6Any : IPAddress.Any, 0);

            // Prepare receive & send buffers
            receiveBuffer.Reserve(OptionReceiveBufferSize);

            // Reset statistic
            BytesPending = 0;
            BytesSending = 0;
            BytesSent = 0;
            BytesReceived = 0;
            DatagramsSent = 0;
            DatagramsReceived = 0;

            // Update the connected flag
            IsConnected = true;

            // Call the client connected handler
            OnConnected();

            return true;
        }

        /// <summary>
        ///     Disconnect the client (synchronous)
        /// </summary>
        /// <returns>'true' if the client was successfully disconnected, 'false' if the client is already disconnected</returns>
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
                // Close the client socket
                Socket.Close();

                // Dispose the client socket
                Socket.Dispose();

                // Dispose event arguments
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
        ///     Setup multicast: bind the socket to the multicast UDP server
        /// </summary>
        /// <param name="enable">Enable/disable multicast</param>
        public virtual void SetupMulticast(Boolean enable)
        {
            OptionReuseAddress = enable;
            OptionMulticast = enable;
        }

        /// <summary>
        ///     Join multicast group with a given IP address (synchronous)
        /// </summary>
        /// <param name="address">IP address</param>
        public virtual void JoinMulticastGroup(IPAddress address)
        {
            if (Endpoint.AddressFamily == AddressFamily.InterNetworkV6)
            {
                Socket.SetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.AddMembership, new IPv6MulticastOption(address));
            }
            else
            {
                Socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, new MulticastOption(address));
            }

            // Call the client joined multicast group notification
            OnJoinedMulticastGroup(address);
        }

        /// <summary>
        ///     Join multicast group with a given IP address (synchronous)
        /// </summary>
        /// <param name="address">IP address</param>
        public virtual void JoinMulticastGroup(String address)
        {
            JoinMulticastGroup(IPAddress.Parse(address));
        }

        /// <summary>
        ///     Leave multicast group with a given IP address (synchronous)
        /// </summary>
        /// <param name="address">IP address</param>
        public virtual void LeaveMulticastGroup(IPAddress address)
        {
            if (Endpoint.AddressFamily == AddressFamily.InterNetworkV6)
            {
                Socket.SetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.DropMembership, new IPv6MulticastOption(address));
            }
            else
            {
                Socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.DropMembership, new MulticastOption(address));
            }

            // Call the client left multicast group notification
            OnLeftMulticastGroup(address);
        }

        /// <summary>
        ///     Leave multicast group with a given IP address (synchronous)
        /// </summary>
        /// <param name="address">IP address</param>
        public virtual void LeaveMulticastGroup(String address)
        {
            LeaveMulticastGroup(IPAddress.Parse(address));
        }

        /// <summary>
        ///     Send datagram to the connected server (synchronous)
        /// </summary>
        /// <param name="buffer">Datagram buffer to send</param>
        /// <param name="offset">Datagram buffer offset</param>
        /// <param name="size">Datagram buffer size</param>
        /// <returns>Size of sent datagram</returns>
        public override Int64 Send(Byte[] buffer, Int64 offset, Int64 size)
        {
            return Send(Endpoint, buffer, offset, size);
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
            if (!IsConnected)
            {
                return 0;
            }

            if (size == 0)
            {
                return 0;
            }

            try
            {
                // Sent datagram to the server
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
                Disconnect();
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
        ///     Send datagram to the connected server (asynchronous)
        /// </summary>
        /// <param name="buffer">Datagram buffer to send</param>
        /// <param name="offset">Datagram buffer offset</param>
        /// <param name="size">Datagram buffer size</param>
        /// <returns>'true' if the datagram was successfully sent, 'false' if the datagram was not sent</returns>
        public override Boolean SendAsync(Byte[] buffer, Int64 offset, Int64 size)
        {
            return SendAsync(Endpoint, buffer, offset, size);
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
            if (sending)
            {
                return false;
            }

            if (!IsConnected)
            {
                return false;
            }

            if (size == 0)
            {
                return true;
            }

            // Fill the main send buffer
            sendBuffer.Append(buffer, offset, size);

            // Update statistic
            BytesSending = sendBuffer.Size;

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
            if (!IsConnected)
            {
                return 0;
            }

            if (size == 0)
            {
                return 0;
            }

            try
            {
                // Receive datagram from the server
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
                Disconnect();
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

            try
            {
                // Async receive with the receive handler
                receiving = true;
                _receiveEventArg.RemoteEndPoint = _receiveEndpoint;
                _receiveEventArg.SetBuffer(receiveBuffer.Data, 0, (Int32) receiveBuffer.Capacity);
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

            try
            {
                // Async write with the write handler
                sending = true;
                _sendEventArg.RemoteEndPoint = _sendEndpoint;
                _sendEventArg.SetBuffer(sendBuffer.Data, 0, (Int32) sendBuffer.Size);
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
        protected sealed override void ClearBuffers()
        {
            // Clear send buffers
            sendBuffer.Clear();

            // Update statistic
            BytesPending = 0;
            BytesSending = 0;
        }

        /// <summary>
        ///     This method is called whenever a receive or send operation is completed on a socket
        /// </summary>
        protected override void OnAsyncCompleted(Object sender, SocketAsyncEventArgs e)
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
            receiving = false;

            if (!IsConnected)
            {
                return;
            }

            // Disconnect on error
            if (e.SocketError != SocketError.Success)
            {
                SendError(e.SocketError);
                Disconnect();
                return;
            }

            Int64 size = e.BytesTransferred;

            // Received some data from the server
            if (size <= 0)
            {
                return;
            }

            // Update statistic
            DatagramsReceived++;
            BytesReceived += size;

            // Call the datagram received handler
            OnReceived(e.RemoteEndPoint, receiveBuffer.Data, 0, size);

            // If the receive buffer is full increase its size
            if (receiveBuffer.Capacity == size)
            {
                receiveBuffer.Reserve(2 * size);
            }
        }

        /// <summary>
        ///     This method is invoked when an asynchronous send to operation completes
        /// </summary>
        private void ProcessSendTo(SocketAsyncEventArgs e)
        {
            sending = false;

            if (!IsConnected)
            {
                return;
            }

            // Disconnect on error
            if (e.SocketError != SocketError.Success)
            {
                SendError(e.SocketError);
                Disconnect();
                return;
            }

            Int64 sent = e.BytesTransferred;

            // Send some data to the server
            if (sent <= 0)
            {
                return;
            }

            // Update statistic
            BytesSending = 0;
            BytesSent += sent;

            // Clear the send buffer
            sendBuffer.Clear();

            // Call the buffer sent handler
            OnSent(_sendEndpoint, sent);
        }

        /// <summary>
        ///     Handle client joined multicast group notification
        /// </summary>
        /// <param name="address">IP address</param>
        [SuppressMessage("ReSharper", "UnusedParameter.Global")]
        protected virtual void OnJoinedMulticastGroup(IPAddress address)
        {
        }

        /// <summary>
        ///     Handle client left multicast group notification
        /// </summary>
        /// <param name="address">IP address</param>
        [SuppressMessage("ReSharper", "UnusedParameter.Global")]
        protected virtual void OnLeftMulticastGroup(IPAddress address)
        {
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
        [SuppressMessage("ReSharper", "UnusedParameter.Global")]
        protected virtual void OnReceived(EndPoint endpoint, Byte[] buffer, Int64 offset, Int64 size)
        {
        }

        /// <summary>
        ///     Handle datagram sent notification
        /// </summary>
        /// <param name="endpoint">Endpoint of sent datagram</param>
        /// <param name="sent">Size of sent datagram buffer</param>
        /// <remarks>
        ///     Notification is called when a datagram was sent to the server.
        ///     This handler could be used to send another datagram to the server for instance when the pending size is zero.
        /// </remarks>
        [SuppressMessage("ReSharper", "UnusedParameter.Global")]
        protected virtual void OnSent(EndPoint endpoint, Int64 sent)
        {
        }
    }
}