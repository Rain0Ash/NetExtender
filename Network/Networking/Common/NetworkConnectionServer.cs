// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace NetExtender.Network.Networking.Common
{
    public abstract class NetworkConnectionServer<T> : NetworkServer where T : NetworkSession
    {
        /// <summary>
        ///     Number of sessions connected to the server
        /// </summary>
        public Int64 ConnectedSessions
        {
            get
            {
                return Sessions.Count;
            }
        }

        /// <summary>
        ///     Option: acceptor backlog size
        /// </summary>
        /// <remarks>
        ///     This option will set the listening socket's backlog size
        /// </remarks>
        public Int32 OptionAcceptorBacklog { get; set; } = 1024;

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
        ///     Is the server accepting new clients?
        /// </summary>
        public Boolean IsAccepting { get; protected set; }

        // Server sessions
        protected readonly ConcurrentDictionary<Guid, T> Sessions = new ConcurrentDictionary<Guid, T>();
        
        // Server sessions
        protected SocketAsyncEventArgs acceptorEventArg;

        // Server acceptor
        protected Socket acceptorSocket;

        /// <summary>
        ///     Initialize network server with a given IP endpoint
        /// </summary>
        /// <param name="endpoint">IP endpoint</param>
        protected NetworkConnectionServer(IPEndPoint endpoint)
            : base(endpoint)
        {
        }

        /// <summary>
        ///     Create SSL session factory method
        /// </summary>
        /// <returns>SSL session</returns>
        protected abstract T CreateSession();

        /// <summary>
        ///     Start accept a new client connection
        /// </summary>
        protected virtual void StartAccept(SocketAsyncEventArgs e)
        {
            // Socket must be cleared since the context object is being reused
            e.AcceptSocket = null;

            // Async accept a new client connection
            if (!acceptorSocket.AcceptAsync(e))
            {
                ProcessAccept(e);
            }
        }
        
        /// <summary>
        ///     Process accepted client connection
        /// </summary>
        protected void ProcessAccept(SocketAsyncEventArgs e)
        {
            if (e.SocketError == SocketError.Success)
            {
                // Create a new session to register
                T session = CreateSession();

                // Register the session
                RegisterSession(session);

                // Connect new session
                session.Connect(e.AcceptSocket);
            }
            else
            {
                SendError(e.SocketError);
            }

            // Accept the next client connection
            if (IsAccepting)
            {
                StartAccept(e);
            }
        }
        
        /// <summary>
        ///     This method is the callback method associated with Socket.AcceptAsync()
        ///     operations and is invoked when an accept operation is complete
        /// </summary>
        protected void OnAsyncCompleted(Object sender, SocketAsyncEventArgs e)
        {
            ProcessAccept(e);
        }

        /// <summary>
        ///     Disconnect all connected sessions
        /// </summary>
        /// <returns>'true' if all sessions were successfully disconnected, 'false' if the server is not started</returns>
        public virtual Boolean DisconnectAll()
        {
            if (!IsStarted)
            {
                return false;
            }

            // Disconnect all sessions
            foreach (T session in Sessions.Values)
            {
                session.Disconnect();
            }

            return true;
        }
        
        /// <summary>
        ///     Find a session with a given Id
        /// </summary>
        /// <param name="id">Session Id</param>
        /// <returns>Session with a given Id or null if the session it not connected</returns>
        public T FindSession(Guid id)
        {
            // Try to find the required session
            return Sessions.TryGetValue(id, out T result) ? result : null;
        }

        /// <summary>
        ///     Register a new session
        /// </summary>
        /// <param name="session">Session to register</param>
        internal void RegisterSession(T session)
        {
            // Register a new session
            Sessions.TryAdd(session.Id, session);
        }

        /// <summary>
        ///     Unregister session by Id
        /// </summary>
        /// <param name="id">Session Id</param>
        internal void UnregisterSession(Guid id)
        {
            // Unregister session by Id
            Sessions.TryRemove(id, out T temp);
        }

        /// <summary>
        ///     Multicast data to all connected sessions
        /// </summary>
        /// <param name="buffer">Buffer to multicast</param>
        /// <returns>'true' if the data was successfully multicasted, 'false' if the data was not multicasted</returns>
        public virtual Boolean Multicast(Byte[] buffer)
        {
            return Multicast(buffer, 0, buffer.Length);
        }

        /// <summary>
        ///     Multicast text to all connected clients
        /// </summary>
        /// <param name="text">Text string to multicast</param>
        /// <returns>'true' if the text was successfully multicasted, 'false' if the text was not multicasted</returns>
        public virtual Boolean Multicast(String text)
        {
            return Multicast(Encoding.UTF8.GetBytes(text));
        }
        
        /// <summary>
        ///     Multicast data to all connected clients
        /// </summary>
        /// <param name="buffer">Buffer to multicast</param>
        /// <param name="offset">Buffer offset</param>
        /// <param name="size">Buffer size</param>
        /// <returns>'true' if the data was successfully multicasted, 'false' if the data was not multicasted</returns>
        public virtual Boolean Multicast(Byte[] buffer, Int64 offset, Int64 size)
        {
            if (!IsStarted)
            {
                return false;
            }

            if (size == 0)
            {
                return true;
            }

            // Multicast data to all sessions
            foreach (T session in Sessions.Values)
            {
                session.SendAsync(buffer, offset, size);
            }

            return true;
        }

        /// <summary>
        ///     Handle session connected notification
        /// </summary>
        /// <param name="session">Connected session</param>
        protected virtual void OnConnected(T session)
        {
        }

        /// <summary>
        ///     Handle session disconnected notification
        /// </summary>
        /// <param name="session">Disconnected session</param>
        protected virtual void OnDisconnected(T session)
        {
        }

        internal void OnConnectedInternal(T session)
        {
            OnConnected(session);
        }

        internal void OnDisconnectedInternal(T session)
        {
            OnDisconnected(session);
        }
    }
}