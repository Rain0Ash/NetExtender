// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using NetExtender.Types.Sets.Interfaces;

namespace NetExtender.Types.Concurrent.Observable.Interfaces
{
    public interface IConcurrentObservableSortedSet<T> : IConcurrentObservableSet<T>, ISortedSet<T>
    {
        public new ImmutableSortedSet<T> Immutable { get; }
        public new ISet<T> View { get; }

        public Int32 IndexOf(T item);

        public T this[Int32 index] { get; }
        public T this[Index index] { get; }
    }
}