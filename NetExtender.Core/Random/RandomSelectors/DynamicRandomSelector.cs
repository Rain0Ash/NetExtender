// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using NetExtender.Exceptions;
using NetExtender.Random.Interfaces;
using NetExtender.Utils.Numerics;

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
        private IRandom Random { get; set; }

        // internal buffers
        private readonly List<T> _items;
        private readonly List<Double> _weights;
        private readonly List<Double> _cdl; // Cummulative Distribution List

        // internal function that gets dynamically swapped inside Build
        private Func<List<Double>, Double, Int32>? _select;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="seed">Leave it -1 if you want seed to be randomly picked</param>
        /// <param name="capacity">Set this if you know how much items the collection will hold, to minimize Garbage Collection</param>
        public DynamicRandomSelector(Int32 seed = -1, Int32 capacity = 32)
        {
            Random = seed == -1 ? RandomUtils.Create() : RandomUtils.Create(seed);

            _items = new List<T>(capacity);
            _weights = new List<Double>(capacity);
            _cdl = new List<Double>(capacity);
        }

        /// <summary>
        /// Constructor, where you can preload collection with items/weights list. 
        /// </summary>
        /// <param name="items">Items that will get returned on random selections</param>
        /// <param name="weights">Un-normalized weights/chances of items, should be same length as items array</param>
        /// /// <param name="seed">Leave it -1 if you want seed to be randomly picked</param>
        /// <param name="capacity">Set this if you know how much items the collection will hold, to minimize Garbage Collection</param>
        public DynamicRandomSelector(IReadOnlyList<T> items, IReadOnlyList<Double> weights, Int32 seed = -1, Int32 capacity = 32)
            : this(seed, capacity)
        {
            for (Int32 i = 0; i < items.Count; i++)
            {
                Add(items[i], weights[i]);
            }

            Build();
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

            _items.Add(item);
            _weights.Add(weight);
        }

        /// <summary>
        /// Remove existing item with weight into collection.
        /// Be sure to call Build() after you are done removing items.
        /// </summary>
        /// <param name="item">Item that will be removed out of collection, if found</param>
        public void Remove(T item)
        {
            Int32 index = _items.IndexOf(item);

            // nothing was found
            if (index == -1)
            {
                return;
            }

            _items.RemoveAt(index);
            _weights.RemoveAt(index);
            // no need to remove from CDL, should be rebuilt instead
        }

        /// <inheritdoc cref="Build(int)"/>
        public IRandomSelector<T> Build()
        {
            return Build(-1);
        }

        /// <summary>
        /// Re/Builds internal CDL (Cummulative Distribution List)
        /// Must be called after modifying (calling Add or Remove), or it will break. 
        /// Switches between linear or binary search, depending on which one will be faster.
        /// Might generate some garbage (list resize) on first few builds.
        /// </summary>
        /// <param name="seed">You can specify seed for internal random gen or leave it alone</param>
        /// <returns>Returns itself</returns>
        public IRandomSelector<T> Build(Int32 seed)
        {
            if (_items.Count == 0)
            {
                throw new Exception("Cannot build with no items.");
            }

            // clear list and then transfer weights
            _cdl.Clear();
            foreach (Double weight in _weights)
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
                    seed = Random.Next();
                }

                Random = RandomUtils.Create(seed);
            }

            // RandomMath.ListBreakpoint decides where to use Linear or Binary search, based on internal buffer size
            // if CDL list is smaller than breakpoint, then pick linear search random selector, else pick binary search selector
            if (_cdl.Count < RandomMath.ListBreakpoint)
            {
                _select = RandomMath.SelectIndexLinearSearch;
            }
            else
            {
                _select = RandomMath.SelectIndexBinarySearch;
            }

            return this;
        }

        /// <summary>
        /// Selects random item based on its probability.
        /// Uses linear search or binary search, depending on internal list size.
        /// </summary>
        /// <param name="value">Random value from your uniform generator</param>
        /// <returns>Returns item</returns>
        public T SelectRandomItem(Double value)
        {
            if (_select is null)
            {
                throw new NotInitializedException();
            }

            return _items[_select(_cdl, value)];
        }

        /// <summary>
        /// Selects random item based on its probability.
        /// Uses linear search or binary search, depending on internal list size.
        /// </summary>
        /// <returns>Returns item</returns>
        public T SelectRandomItem()
        {
            if (_select is null)
            {
                throw new NotInitializedException();
            }

            return _items[_select(_cdl, Random.NextDouble())];
        }
        
        /// <summary>
        /// Clears internal buffers, should make no garbage (unless internal lists hold objects that aren't referenced anywhere else)
        /// </summary>
        public void Clear()
        {
            _items.Clear();
            _weights.Clear();
            _cdl.Clear();
        }
    }
}