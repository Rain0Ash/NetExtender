// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Net;
using NetExtender.Interfaces;

namespace NetExtender.Network.IPC.TCP
{
    public class TCPReader : ITask
    {
        private readonly FixedTcpListener _listener;

        public TCPReader(Int32 port = 60000)
            : this(new IPEndPoint(IPAddress.Loopback, port))
        {
        }

        public TCPReader(IPEndPoint point)
        {
            _listener = new FixedTcpListener(point);
        }
        
        public Int32 RequestCount { get; set; } = 1;

        private Boolean Listen
        {
            get
            {
                return _listener.Active;
            }
            set
            {
                _listener.Active = value;
            }
        }

        public Boolean IsStarted { get; private set; }
        public Boolean IsPaused { get; private set; }

        public void Start()
        {
            Listen = true;
            IsStarted = true;
        }

        public void Stop()
        {
            Listen = false;
            IsStarted = false;
        }

        public void Pause()
        {
            Listen = false;
            IsPaused = true;
        }

        public void Resume()
        {
            Listen = true;
            IsPaused = false;
        }
    }
}