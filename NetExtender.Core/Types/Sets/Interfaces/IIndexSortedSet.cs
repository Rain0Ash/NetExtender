// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Types.Sets.Interfaces
{
    public interface IIndexSortedSet<T> : ISortedSet<T>
    {
        public new Int32 Count { get; }
        public new Boolean Contains(T item);
        public new IIndexSortedSet<T> GetViewBetween(T? lower, T? upper);
        public new void Clear();
        public new void CopyTo(T[] array, Int32 index);
    }
}