// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;

namespace NetExtender.Types.Counters.Interfaces
{
    public interface IReadOnlyCounter<T> : IReadOnlyCounter<T, Int32>
    {
    }
    
    public interface IReadOnlyCounter64<T> : IReadOnlyCounter<T, Int64>
    {
    }

    public interface IReadOnlyCounter<T, TCount> : IReadOnlyDictionary<T, TCount> where TCount : unmanaged, IConvertible
    {
        public Boolean Contains(T item);
        public Boolean Contains(T item, TCount count);

        public IEnumerable<T> Enumerate();
        public T[]? ToItemsArray();
        public T[]? ToItemsArray(Int32 length);
    }
}