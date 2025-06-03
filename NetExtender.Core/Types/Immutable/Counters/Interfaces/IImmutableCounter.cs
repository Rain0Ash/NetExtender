// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using NetExtender.Types.Counters.Interfaces;

namespace NetExtender.Types.Immutable.Counters.Interfaces
{
    public interface IImmutableCounter<T, TCount> : IReadOnlyCounter<T, TCount> where TCount : unmanaged, IConvertible
    {
        public IImmutableCounter<T, TCount> Add(T item);
        public IImmutableCounter<T, TCount> Add(T item, out TCount result);
        public IImmutableCounter<T, TCount> Add(T item, TCount count);
        public IImmutableCounter<T, TCount> Add(T item, TCount count, out TCount result);
        public IImmutableCounter<T, TCount> AddRange(IEnumerable<T> source);
        public IImmutableCounter<T, TCount> AddRange(IEnumerable<KeyValuePair<T, TCount>> source);
        public IImmutableCounter<T, TCount> SetItem(T item, TCount count);
        public IImmutableCounter<T, TCount> SetItemRange(IEnumerable<KeyValuePair<T, TCount>> source);
        public IImmutableCounter<T, TCount> Remove(T item);
        public IImmutableCounter<T, TCount> Remove(T item, TCount count);
        public IImmutableCounter<T, TCount> Remove(T item, out TCount result);
        public IImmutableCounter<T, TCount> Remove(T item, TCount count, out TCount result);
        public IImmutableCounter<T, TCount> RemoveRange(IEnumerable<T> source);
        public IImmutableCounter<T, TCount> RemoveRange(IEnumerable<KeyValuePair<T, TCount>> source);
        public IImmutableCounter<T, TCount> Clear(T item);
        public IImmutableCounter<T, TCount> Clear(IEnumerable<T> source);
    }
}