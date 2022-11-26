// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Net;
using NetExtender.Types.Tasks.Interfaces;

namespace NetExtender.Types.Network.Tcp
{
    public sealed class TcpReader : ITask
    {
        private ActiveTcpListener Listener { get; }

        public Int32 RequestCount { get; set; } = 1;
        public Boolean IsStarted { get; private set; }
        public Boolean IsPaused { get; private set; }

        [Obsolete]
        public TcpReader(Int32 port)
        {
            Listener = new ActiveTcpListener(port);
        }

        public TcpReader(IPAddress? address, Int32 port)
        {
            Listener = new ActiveTcpListener(address ?? IPAddress.Loopback, port);
        }

        public TcpReader(IPEndPoint endpoint)
        {
            if (endpoint is null)
            {
                throw new ArgumentNullException(nameof(endpoint));
            }

            Listener = new ActiveTcpListener(endpoint);
        }

        public TcpReader(ActiveTcpListener listener)
        {
            Listener = listener ?? throw new ArgumentNullException(nameof(listener));
        }

        public void Start()
        {
            Listener.Active = true;
            IsStarted = true;
        }

        public void Stop()
        {
            Listener.Active = false;
            IsStarted = false;
        }

        public void Pause()
        {
            Listener.Active = false;
            IsPaused = true;
        }

        public void Resume()
        {
            Listener.Active = true;
            IsPaused = false;
        }

        public void Cancel()
        {
            Stop();
        }
    }
}