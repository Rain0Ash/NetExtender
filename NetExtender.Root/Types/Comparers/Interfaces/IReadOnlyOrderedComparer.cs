// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;

namespace NetExtender.Types.Comparers.Interfaces
{
    public interface IReadOnlyOrderedComparer<T> : IComparer<T>, IReadOnlyCollection<T>
    {
        public IComparer<T> Comparer { get; }
        public Boolean Contains(T item);
        public Int32 GetOrder(T item);
        public Boolean GetOrder(T item, out Int32 order);
        public T this[Int32 index] { get; }
        public T this[Index index] { get; }
    }
}