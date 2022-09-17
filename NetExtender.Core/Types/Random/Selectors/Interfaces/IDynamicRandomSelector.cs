// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using NetExtender.Types.Dictionaries.Interfaces;

namespace NetExtender.Types.Random
{
    public interface IDynamicRandomSelector<T> : IRandomSelector<T>, IIndexDictionary<T, Double> where T : notnull
    {
        public new Int32 Count { get; }
        public Boolean Contains(T item);
        public void Add(IEnumerable<KeyValuePair<T, Double>> items);
        public new IEnumerator<T> GetEnumerator();
    }
}