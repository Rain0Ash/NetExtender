// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;

namespace NetExtender.Initializer.Types.Indexers.Interfaces
{
    public interface IIndexer<T> : IReadOnlyCollection<T>
    {
        public IEqualityComparer<T> Comparer { get; }
        public Boolean Contains(T item);
        public Int32 IndexOf(T item);
        public void Clear();
        public Boolean Rebuild(IEnumerable<T> source);
        public void CopyTo(T[] array, Int32 index);
        public Int32 this[T item] { get; }
    }
}