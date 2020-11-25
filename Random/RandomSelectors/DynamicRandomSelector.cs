﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;

namespace NetExtender.Random
{
    /// <summary>
    /// DynamicRandomSelector allows you adding or removing items.
    /// Call "Build" after you finished modification.
    /// Switches between linear or binary search after each Build(), 
    /// depending on count of items, making it more performant for general use case.
    /// </summary>
    /// <typeparam name="T">Type of items you wish this selector returns</typeparam>
    public class DynamicRandomSelector<T> : IRandomSelector<T>, IRandomSelectorBuilder<T>
    {
        private System.Random _random;

        // internal buffers
        private readonly List<T> _itemsList;
        private readonly List<Double> _weightsList;
        private readonly List<Double> _cdl; // Cummulative Distribution List

        // internal function that gets dynamically swapped inside Build
        private Func<List<Double>, Double, Int32> _selectFunction;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="seed">Leave it -1 if you want seed to be randomly picked</param>
        /// <param name="expectedNumberOfItems">Set this if you know how much items the collection will hold, to minimize Garbage Collection</param>
        public DynamicRandomSelector(Int32 seed = -1, Int32 expectedNumberOfItems = 32)
        {
            _random = seed == -1 ? new System.Random() : new System.Random(seed);

            _itemsList = new List<T>(expectedNumberOfItems);
            _weightsList = new List<Double>(expectedNumberOfItems);
            _cdl = new List<Double>(expectedNumberOfItems);
        }

        /// <summary>
        /// Constructor, where you can preload collection with items/weights list. 
        /// </summary>
        /// <param name="items">Items that will get returned on random selections</param>
        /// <param name="weights">Un-normalized weights/chances of items, should be same length as items array</param>
        /// /// <param name="seed">Leave it -1 if you want seed to be randomly picked</param>
        /// <param name="expectedNumberOfItems">Set this if you know how much items the collection will hold, to minimize Garbage Collection</param>
        public DynamicRandomSelector(IReadOnlyList<T> items, IReadOnlyList<Double> weights, Int32 seed = -1,
            Int32 expectedNumberOfItems = 32)
            : this(seed, expectedNumberOfItems)
        {
            for (Int32 i = 0; i < items.Count; i++)
            {
                Add(items[i], weights[i]);
            }

            Build();
        }

        /// <summary>
        /// Clears internal buffers, should make no garbage (unless internal lists hold objects that aren't referenced anywhere else)
        /// </summary>
        public void Clear()
        {
            _itemsList.Clear();
            _weightsList.Clear();
            _cdl.Clear();
        }

        /// <summary>
        /// Add new item with weight into collection. Items with zero weight will be ignored.
        /// Do not add duplicate items, because removing them will be buggy (you will need to call remove for duplicates too!).
        /// Be sure to call Build() after you are done adding items.
        /// </summary>
        /// <param name="item">Item that will be returned on random selection</param>
        /// <param name="weight">Non-zero non-normalized weight</param>
        public void Add(T item, Double weight)
        {
            // ignore zero weight items
            if (Math.Abs(weight) < Double.Epsilon)
            {
                return;
            }

            _itemsList.Add(item);
            _weightsList.Add(weight);
        }

        /// <summary>
        /// Remove existing item with weight into collection.
        /// Be sure to call Build() after you are done removing items.
        /// </summary>
        /// <param name="item">Item that will be removed out of collection, if found</param>
        public void Remove(T item)
        {
            Int32 index = _itemsList.IndexOf(item);

            // nothing was found
            if (index == -1)
            {
                return;
            }

            _itemsList.RemoveAt(index);
            _weightsList.RemoveAt(index);
            // no need to remove from CDL, should be rebuilt instead
        }

        /// <summary>
        /// Re/Builds internal CDL (Cummulative Distribution List)
        /// Must be called after modifying (calling Add or Remove), or it will break. 
        /// Switches between linear or binary search, depending on which one will be faster.
        /// Might generate some garbage (list resize) on first few builds.
        /// </summary>
        /// <param name="seed">You can specify seed for internal random gen or leave it alone</param>
        /// <returns>Returns itself</returns>
        public IRandomSelector<T> Build(Int32 seed = -1)
        {
            if (_itemsList.Count == 0)
            {
                throw new Exception("Cannot build with no items.");
            }

            // clear list and then transfer weights
            _cdl.Clear();
            foreach (Double weight in _weightsList)
            {
                _cdl.Add(weight);
            }

            RandomMath.BuildCumulativeDistribution(_cdl);

            // default behavior
            // if seed wasn't specified (it is seed==-1), keep same seed - avoids garbage collection from making new random
            if (seed != -1)
            {
                // input -2 if you want to randomize seed
                if (seed == -2)
                {
                    seed = _random.Next();
                    _random = new System.Random(seed);
                }
                else
                {
                    _random = new System.Random(seed);
                }
            }

            // RandomMath.ListBreakpoint decides where to use Linear or Binary search, based on internal buffer size
            // if CDL list is smaller than breakpoint, then pick linear search random selector, else pick binary search selector
            if (_cdl.Count < RandomMath.ListBreakpoint)
            {
                _selectFunction = RandomMath.SelectIndexLinearSearch;
            }
            else
            {
                _selectFunction = RandomMath.SelectIndexBinarySearch;
            }

            return this;
        }

        /// <summary>
        /// Selects random item based on its probability.
        /// Uses linear search or binary search, depending on internal list size.
        /// </summary>
        /// <param name="randomValue">Random value from your uniform generator</param>
        /// <returns>Returns item</returns>
        public T SelectRandomItem(Double randomValue)
        {
            return _itemsList[_selectFunction(_cdl, randomValue)];
        }

        /// <summary>
        /// Selects random item based on its probability.
        /// Uses linear search or binary search, depending on internal list size.
        /// </summary>
        /// <returns>Returns item</returns>
        public T SelectRandomItem()
        {
            Double randomValue = _random.NextDouble();
            return _itemsList[_selectFunction(_cdl, randomValue)];
        }
    }
}