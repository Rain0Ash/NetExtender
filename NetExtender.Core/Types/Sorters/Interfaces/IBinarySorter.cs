using System;
using System.Collections.Generic;

namespace NetExtender.Types.Sorters.Interfaces
{
    public interface IBinarySorter<in T> : IComparer<T>
    {
        public Int32 GetInsertIndex<TSource>(TSource source, T item, Int32 count, Func<TSource, Int32, T> search);
        public Int32 GetMatchIndex<TSource>(TSource source, T item, Int32 count, Func<TSource, Int32, T> search);
    }
}