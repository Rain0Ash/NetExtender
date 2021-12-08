// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Net;
using System.Net.Sockets;

namespace NetExtender.Types.Network.Tcp
{
    public class ActiveTcpListener : TcpListener
    {
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
        
        [Obsolete]
        public ActiveTcpListener(Int32 port)
            : base(port)
        {
        }
        
        public ActiveTcpListener(IPAddress localaddr, Int32 port)
            : base(localaddr, port)
        {
        }

        public ActiveTcpListener(IPEndPoint localEP)
            : base(localEP)
        {
        }
    }
}