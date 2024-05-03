using System;
using System.Collections.Generic;
using NetExtender.Types.Counters.Interfaces;

namespace NetExtender.Types.Bags.Interfaces
{
    public interface IReadOnlyCompressionBag<T> : IReadOnlyList<T>
    {
        public IReadOnlyCounter64<T> Constraints { get; }
        public IReadOnlyList<T> Bag { get; }
        public IReadOnlyCounter<T> Counter { get; }
        
        public Boolean Contains(T item);
        public Boolean Contains(T item, Int32 count);
        public Int32 IndexOf(T item);
        public Int32 LastIndexOf(T item);
        public Int32 CountOf(T item);
        public Boolean TryGetValue(Int32 index, out T value);
    }
}