﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace NetExtender.Network.Packet
{
    public abstract class PacketMarshaler
    {
        public abstract void Read(PacketStream stream);

        public abstract PacketStream Write(PacketStream stream);
    }
}