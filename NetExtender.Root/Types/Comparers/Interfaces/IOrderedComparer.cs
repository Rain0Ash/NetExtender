// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;

namespace NetExtender.Types.Comparers.Interfaces
{
    // ReSharper disable once PossibleInterfaceMemberAmbiguity
    public interface IOrderedComparer<T> : ICollection<T>
    {
        public IComparer<T> Comparer { get; }
        public Int32 GetOrder(T item);
        public Boolean GetOrder(T item, out Int32 order);
        public void AddRange(IEnumerable<T> source);
        public void Insert(Int32 index, T item);
        public void InsertRange(Int32 index, IEnumerable<T> source);
        public T this[Int32 index] { get; }
        public T this[Index index] { get; }
    }
}