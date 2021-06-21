// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;

namespace NetExtender.Comparers.Interfaces
{
    // ReSharper disable once PossibleInterfaceMemberAmbiguity
    public interface IOrderedComparer<T> : IReadOnlyOrderedComparer<T>, ICollection<T>
    {
        public void AddRange(IEnumerable<T> items);
        public void Insert(Int32 index, T item);
        public void InsertRange(Int32 index, IEnumerable<T> items);
    }
}