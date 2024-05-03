// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;

namespace NetExtender.Types.Counters.Interfaces
{
    public interface ICounter<T> : ICounter<T, Int32>
    {
    }
    
    public interface ICounter64<T> : ICounter<T, Int64>
    {
    }
    
    public interface ICounter<T, TCount> : IDictionary<T, TCount> where TCount : unmanaged, IConvertible
    {
        public Boolean Contains(T item);
        public Boolean Contains(T item, TCount count);
        public TCount Add(T item);
        public new TCount Add(T item, TCount count);
        public Boolean TryAdd(T item);
        public Boolean TryAdd(T item, TCount count);
        public Boolean TryAdd(T item, out TCount result);
        public Boolean TryAdd(T item, TCount count, out TCount result);
        public Boolean AddRange(IEnumerable<T> source);
        public Boolean AddRange(IEnumerable<KeyValuePair<T, TCount>> source);
        public new Boolean Remove(T item);
        public Boolean Remove(T item, TCount count);
        public Boolean Remove(T item, out TCount result);
        public Boolean Remove(T item, TCount count, out TCount result);
        public Boolean RemoveRange(IEnumerable<T> source);
        public Boolean RemoveRange(IEnumerable<KeyValuePair<T, TCount>> source);
        public Boolean Clear(T item);
        public Boolean Clear(IEnumerable<T> source);

        public IEnumerable<T> Enumerate();
        public T[]? ToItemsArray();
        public T[]? ToItemsArray(Int32 length);
    }
}