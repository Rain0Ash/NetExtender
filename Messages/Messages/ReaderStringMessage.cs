// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using NetExtender.Messages.Interfaces;

namespace NetExtender.Messages
{
    public class ReaderStringMessage : ReaderMessage<String>, IReaderStringMessage
    {
        public ReaderStringMessage(String args, ReaderMessageOptions options = DefaultOptions)
            : this(args.Split(' ', StringSplitOptions.RemoveEmptyEntries), options)
        {
        }

        public ReaderStringMessage(IEnumerable<String> args, ReaderMessageOptions options = DefaultOptions)
            : base(args.ToImmutableList(), options)
        {
        }
    }
}