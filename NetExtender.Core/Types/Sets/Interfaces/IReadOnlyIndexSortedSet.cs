// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using NetExtender.Initializer.Types.Indexers.Interfaces;

namespace NetExtender.Types.Sets.Interfaces
{
    public interface IReadOnlyIndexSortedSet<T> : IReadOnlyMapIndexer<T>, IReadOnlySet<T>
    {
        public new IComparer<T> Comparer { get; }
        public new Boolean Contains(T item);
    }
}