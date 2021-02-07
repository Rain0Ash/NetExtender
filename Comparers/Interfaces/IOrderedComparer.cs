// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace NetExtender.Comparers.Interfaces
{
    public interface IOrderedComparer<T> : IReadOnlyOrderedComparer<T>
    {
        public new IComparer<T> Comparer { get; init; }
        public void Add(T item);
        public void AddRange([NotNull] IEnumerable<T> items);
        public void Insert(Int32 index, T item);
        public void InsertRange(Int32 index, [NotNull] IEnumerable<T> items);
        public Boolean Remove(T item);
    }
}