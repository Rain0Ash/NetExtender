// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;

namespace NetExtender.Messages.Interfaces
{
    public interface IReaderMessage<out T> : IReadOnlyList<T>
    {
        public T Value { get; }
        public IReaderMessage<T> Next { get; }
        public ReaderMessageOptions Options { get; }
        public Boolean IsExternal { get; }
        public Boolean IsProtocol { get; }
    }
}