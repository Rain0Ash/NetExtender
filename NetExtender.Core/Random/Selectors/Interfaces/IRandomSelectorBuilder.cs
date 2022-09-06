// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;

namespace NetExtender.Random
{
    /// <summary>
    /// Interface for Random Selector Builders.
    /// </summary>
    /// <typeparam name="T">Type of items that gets randomly returned</typeparam>
    public interface IRandomSelectorBuilder<T> : IReadOnlyDictionary<T, Double>
    {
        public Boolean Contains(T item);
        public void Add(T item, Double weight);
        public void Add(KeyValuePair<T, Double> item);
        public void Add(IEnumerable<KeyValuePair<T, Double>> items);
        public Boolean Remove(T item);

        public IRandomSelector<T> Build();
        public IRandomSelector<T> Build(Int32 seed);
    }
}