// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using NetExtender.Types.Dictionaries;

namespace NetExtender.Types.Random
{
    /// <summary>
    /// Interface for Random selector
    /// </summary>
    /// <typeparam name="T">Type of items that gets randomly returned</typeparam>
    public interface IRandomSelector<T> : IReadOnlyCollection<T>
    {
        public IReadOnlyCollection<T> Collection { get; }
        public T GetRandom();
        public T GetRandom(Double value);
        public T? GetRandomOrDefault();

        [return: NotNullIfNotNull("alternate")]
        public T? GetRandomOrDefault(T? alternate);
        public T? GetRandomOrDefault(Double value);

        [return: NotNullIfNotNull("alternate")]
        public T? GetRandomOrDefault(Double value, T? alternate);

        public T[] ToItemArray();
        public List<T> ToItemList();
        public NullableDictionary<T, Double> ToItemDictionary();
    }
}