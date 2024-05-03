using System;
using System.Collections.Generic;
using NetExtender.Types.Counters.Interfaces;

namespace NetExtender.Types.Bags.Interfaces
{
    public interface ICompressionBag<T> : IList<T>
    {
        public ICounter64<T> Constraints { get; }
        public IReadOnlyList<T> Bag { get; }
        public IReadOnlyCounter<T> Counter { get; }
        
        public Boolean Contains(T item, Int32 count);
        public Int32 LastIndexOf(T item);
        public Int32 CountOf(T item);
        public Boolean TryGetValue(Int32 index, out T value);
        public Boolean Add(T item, Int32 count);
        public Boolean AddRange(IEnumerable<T> source);
        public Boolean AddRange(IEnumerable<KeyValuePair<T, Int32>> source);
        public Boolean Remove(T item, Int32 count);
        public Boolean RemoveRange(IEnumerable<T> source);
        public Boolean RemoveRange(IEnumerable<KeyValuePair<T, Int32>> source);
    }
}