// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Network.Packet
{
    public abstract class BaseProtocolHandler
    {
        public virtual void OnConnect(Session session)
        {
        }

        public virtual void OnReceive(Session session, Byte[] buf, Int32 bytes)
        {
        }

        public virtual void OnSend(Session session, Byte[] buf, Int32 offset, Int32 bytes)
        {
        }

        public virtual void OnDisconnect(Session session)
        {
        }
    }
}