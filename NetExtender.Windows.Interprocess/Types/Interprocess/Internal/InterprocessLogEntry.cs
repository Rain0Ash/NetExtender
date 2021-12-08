// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.IO;
using ProtoBuf;

namespace NetExtender.Types.Interprocess
{
    [ProtoContract]
    internal sealed class InterprocessLogEntry
    {
        public static Int64 Overhead { get; }

        [ProtoMember(1)]
        public Int64 Id { get; set; }

        [ProtoMember(2)]
        public Guid Instance { get; set; }

        [ProtoMember(3)]
        public DateTime Timestamp { get; set; }

        [ProtoMember(4)]
        public Byte[] Message { get; set; } = Array.Empty<Byte>();

        static InterprocessLogEntry()
        {
            using MemoryStream stream = new MemoryStream();
            Serializer.Serialize(stream, new InterprocessLogEntry { Id = Int64.MaxValue, Instance = Guid.Empty, Timestamp = DateTime.UtcNow });
            Overhead = stream.Length;
        }
    }
}