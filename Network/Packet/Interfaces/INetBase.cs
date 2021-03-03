// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Network.Packet.Interfaces
{
    public interface INetBase
    {
        void OnConnect(Session session);
        void OnReceive(Session session, Byte[] buf, Int32 bytes);
        void OnSend(Session session, Byte[] buf, Int32 offset, Int32 bytes);
        void OnDisconnect(Session session);
        void RemoveSession(Session session);
    }
}