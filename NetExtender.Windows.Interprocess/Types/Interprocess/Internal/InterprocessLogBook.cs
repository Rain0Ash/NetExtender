// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using ProtoBuf;

namespace NetExtender.Types.Interprocess
{
    [ProtoContract]
    internal sealed class InterprocessLogBook
    {
        [ProtoMember(1)]
        public Int64 LastId { get; set; }

        [ProtoMember(2)]
        public List<InterprocessLogEntry> Entries { get; } = new List<InterprocessLogEntry>();

        public Int64 LogSize
        {
            get
            {
                return sizeof(Int64) + Entries.Sum(entry => InterprocessLogEntry.Overhead + entry.Message.Length);
            }
        }

        public void TrimEntries(DateTime point)
        {
            Entries.RemoveAll(entry => entry.Timestamp < point);
        }
    }
}