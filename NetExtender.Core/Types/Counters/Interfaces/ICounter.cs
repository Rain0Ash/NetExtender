// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;

namespace NetExtender.Types.Counters.Interfaces
{
    public interface ICounter<T> : IReadOnlyDictionary<T, Int32> where T : notnull
    {
        public Int32 Add(T item);
        public Boolean TryAdd(T item);
        public Boolean TryAdd(T item, out Int32 count);
        public void AddRange(IEnumerable<T> source);
        public Boolean Remove(T item);
        public Boolean Remove(T item, out Int32 count);
        public void RemoveRange(IEnumerable<T> source);
    }
}