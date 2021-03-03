// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Network.Packet
{
    public abstract class PacketBase<T> : PacketMarshaler
    {
        protected UInt16 TypeId { get; }

        public T Connection { protected get; set; }

        protected PacketBase(UInt16 typeId)
        {
            TypeId = typeId;
        }

        public abstract PacketStream Encode();
        public abstract PacketBase<T> Decode(PacketStream ps);
    }
}