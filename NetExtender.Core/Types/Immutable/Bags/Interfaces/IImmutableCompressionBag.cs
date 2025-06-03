// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using NetExtender.Types.Bags.Interfaces;

namespace NetExtender.Types.Immutable.Bags.Interfaces
{
    public interface IImmutableCompressionBag<T> : IReadOnlyCompressionBag<T> where T : unmanaged, IConvertible
    {
        public new IImmutableDictionary<T, Int32> Constraints { get; }
        public new IImmutableList<T> Bag { get; }
        
        public IImmutableCompressionBag<T> Add(T item);
        public IImmutableCompressionBag<T> Add(T item, Int32 count);
        public IImmutableCompressionBag<T> AddRange(IEnumerable<T> source);
        public IImmutableCompressionBag<T> AddRange(IEnumerable<KeyValuePair<T, Int32>> source);
        public IImmutableCompressionBag<T> Remove(T item);
        public IImmutableCompressionBag<T> Remove(T item, Int32 count);
        public IImmutableCompressionBag<T> Remove(T item, out Int32 result);
        public IImmutableCompressionBag<T> Remove(T item, Int32 count, out Int32 result);
        public IImmutableCompressionBag<T> RemoveRange(IEnumerable<T> source);
        public IImmutableCompressionBag<T> RemoveRange(IEnumerable<KeyValuePair<T, Int32>> source);
    }
}