// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Net;
using System.Net.Sockets;

namespace NetExtender.Network.IPC.TCP
{
    public class FixedTcpListener : TcpListener
    {
        public FixedTcpListener(IPAddress localaddr, Int32 port)
            : base(localaddr, port)
        {
        }

        public FixedTcpListener(IPEndPoint localEP)
            : base(localEP)
        {
        }

        public new Boolean Active
        {
            get
            {
                return base.Active;
            }
            set
            {
                if (Active == value)
                {
                    return;
                }
                
                if (value)
                {
                    Start();
                    return;
                }

                Stop();
            }
        }
    }
}