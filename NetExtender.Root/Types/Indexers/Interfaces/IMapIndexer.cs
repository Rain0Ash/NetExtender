// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace NetExtender.Types.Indexers.Interfaces
{
    public interface IMapIndexer<T> : IIndexer<T>, IReadOnlyList<T>
    {
        public Boolean ContainsIndex(Int32 index);
        public T? ValueOf(Int32 index);
        public Boolean ValueOf(Int32 index, [MaybeNullWhen(false)] out T value);
    }
}