using System;
using System.Net;
using System.Net.Sockets;
using NetExtender.Network.Networking.Common;

namespace NetExtender.Network.Networking.SSL
{
    /// <summary>
    ///     SSL server is used to connect, disconnect and manage SSL sessions
    /// </summary>
    /// <remarks>Thread-safe</remarks>
    public class SslNetworkServer : NetworkConnectionServer<SslNetworkSession>
    {
        /// <summary>
        ///     Initialize SSL server with a given IP address and port number
        /// </summary>
        /// <param name="context">SSL context</param>
        /// <param name="address">IP address</param>
        /// <param name="port">Port number</param>
        public SslNetworkServer(SslNetworkContext context, IPAddress address, Int32 port)
            : this(context, new IPEndPoint(address, port))
        {
        }

        /// <summary>
        ///     Initialize SSL server with a given IP address and port number
        /// </summary>
        /// <param name="context">SSL context</param>
        /// <param name="address">IP address</param>
        /// <param name="port">Port number</param>
        public SslNetworkServer(SslNetworkContext context, String address, Int32 port)
            : this(context, new IPEndPoint(IPAddress.Parse(address), port))
        {
        }

        /// <summary>
        ///     Initialize SSL server with a given IP endpoint
        /// </summary>
        /// <param name="context">SSL context</param>
        /// <param name="endpoint">IP endpoint</param>
        public SslNetworkServer(SslNetworkContext context, IPEndPoint endpoint)
            : base(endpoint)
        {
            Context = context;
        }

        /// <summary>
        ///     SSL context
        /// </summary>
        public SslNetworkContext Context { get; }

        /// <summary>
        ///     Create SSL session factory method
        /// </summary>
        /// <returns>SSL session</returns>
        protected override SslNetworkSession CreateSession()
        {
            return new SslNetworkSession(this);
        }

        /// <summary>
        ///     Start the server
        /// </summary>
        /// <returns>'true' if the server was successfully started, 'false' if the server failed to start</returns>
        public override Boolean Start()
        {
            if (IsStarted)
            {
                return false;
            }

            // Setup acceptor event arg
            acceptorEventArg = new SocketAsyncEventArgs();
            acceptorEventArg.Completed += OnAsyncCompleted;

            // Create a new acceptor socket
            acceptorSocket = new Socket(Endpoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            // Update the acceptor socket disposed flag
            IsSocketDisposed = false;

            // Apply the option: reuse address
            acceptorSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, OptionReuseAddress);
            // Apply the option: exclusive address use
            acceptorSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ExclusiveAddressUse, OptionExclusiveAddressUse);
            // Apply the option: dual mode (this option must be applied before listening)
            if (acceptorSocket.AddressFamily == AddressFamily.InterNetworkV6)
            {
                acceptorSocket.DualMode = OptionDualMode;
            }

            // Bind the acceptor socket to the IP endpoint
            acceptorSocket.Bind(Endpoint);
            // Refresh the endpoint property based on the actual endpoint created
            Endpoint = (IPEndPoint) acceptorSocket.LocalEndPoint;
            // Start listen to the acceptor socket with the given accepting backlog size
            acceptorSocket.Listen(OptionAcceptorBacklog);

            // Reset statistic
            bytesPending = 0;
            bytesSent = 0;
            bytesReceived = 0;

            // Update the started flag
            IsStarted = true;

            // Call the server started handler
            OnStarted();

            // Perform the first server accept
            IsAccepting = true;
            StartAccept(acceptorEventArg);

            return true;
        }

        /// <summary>
        ///     Stop the server
        /// </summary>
        /// <returns>'true' if the server was successfully stopped, 'false' if the server is already stopped</returns>
        public override Boolean Stop()
        {
            if (!IsStarted)
            {
                return false;
            }

            // Stop accepting new clients
            IsAccepting = false;

            // Reset acceptor event arg
            acceptorEventArg.Completed -= OnAsyncCompleted;

            // Close the acceptor socket
            acceptorSocket.Close();

            // Dispose the acceptor socket
            acceptorSocket.Dispose();

            // Dispose event arguments
            acceptorEventArg.Dispose();

            // Update the acceptor socket disposed flag
            IsSocketDisposed = true;

            // Disconnect all sessions
            DisconnectAll();

            // Update the started flag
            IsStarted = false;

            // Call the server stopped handler
            OnStopped();

            return true;
        }

        /// <summary>
        ///     Handle session handshaked notification
        /// </summary>
        /// <param name="session">Handshaked session</param>
        // ReSharper disable once UnusedParameter.Global
        protected virtual void OnHandshaked(SslNetworkSession session)
        {
        }

        internal void OnHandshakedInternal(SslNetworkSession session)
        {
            OnHandshaked(session);
        }
    }
}