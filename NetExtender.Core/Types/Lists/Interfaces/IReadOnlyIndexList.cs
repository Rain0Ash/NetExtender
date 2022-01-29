// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Collections.Generic;
using NetExtender.Initializer.Types.Indexers.Interfaces;

namespace NetExtender.Initializer.Types.Lists.Interfaces
{
    public interface IReadOnlyIndexList<T> : IReadOnlyMapIndexer<T>
    {
        public new IEqualityComparer<T> Comparer { get; }
    }
}