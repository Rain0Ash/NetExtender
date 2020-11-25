﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Random
{
    /// <summary>
    /// Uses Linear Search for picking random items
    /// Good for small sized number of items
    /// </summary>
    /// <typeparam name="T">Type of items you wish this selector returns</typeparam>
    public class StaticRandomSelectorLinear<T> : IRandomSelector<T>
    {
        private readonly System.Random _random;

        // internal buffers
        private readonly T[] _items;
        private readonly Double[] _cda;

        /// <summary>
        /// Constructor, used by StaticRandomSelectorBuilder
        /// Needs array of items and CDA (Cummulative Distribution Array). 
        /// </summary>
        /// <param name="items">Items of type T</param>
        /// <param name="cda">Cummulative Distribution Array</param>
        /// <param name="seed">Seed for internal random generator</param>
        public StaticRandomSelectorLinear(T[] items, Double[] cda, Int32 seed)
        {
            _items = items;
            _cda = cda;
            _random = new System.Random(seed);
        }

        /// <summary>
        /// Selects random item based on their weights.
        /// Uses linear search for random selection.
        /// </summary>
        /// <returns>Returns item</returns>
        public T SelectRandomItem(Double randomValue)
        {
            return _items[_cda.SelectIndexBinarySearch(randomValue)];
        }

        /// <summary>
        /// Selects random item based on their weights.
        /// Uses linear search for random selection.
        /// </summary>
        /// <returns>Returns item</returns>
        public T SelectRandomItem()
        {
            Double randomValue = _random.NextDouble();

            return _items[_cda.SelectIndexBinarySearch(randomValue)];
        }
    }
}