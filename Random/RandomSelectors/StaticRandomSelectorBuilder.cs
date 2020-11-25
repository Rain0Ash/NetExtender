﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Collections.Generic;
using System;
using NetExtender.Utils.Types;

 namespace NetExtender.Random
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RandomSelectorBuilder<T> : IRandomSelectorBuilder<T>
    {
        private readonly System.Random _random;
        private readonly List<T> _itemBuffer;
        private readonly List<Double> _weightBuffer;

        public RandomSelectorBuilder(IDictionary<T, Double> items = null)
        {
            _random = new System.Random();
            _itemBuffer = new List<T>();
            _weightBuffer = new List<Double>();

            if (items.IsNotEmpty())
            {
                Add(items);
            }
        }

        /// <summary>
        /// Add new item with weight into collection. Items with zero weight will be ignored.
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

            _itemBuffer.Add(item);
            _weightBuffer.Add(weight);
        }

        public void Add(IDictionary<T, Double> items)
        {
            foreach ((T item, Double weight) in items)
            {
                Add(item, weight);
            }
        }

        /// <summary>
        /// Builds StaticRandomSelector & clears internal buffers. Must be called after you finish Add-ing items.
        /// </summary>
        /// <param name="seed">Seed for random selector. If you leave it -1, the internal random will generate one.</param>
        /// <returns>Returns IRandomSelector, underlying objects are either StaticRandomSelectorLinear or StaticRandomSelectorBinary. Both are non-mutable.</returns>
        public IRandomSelector<T> Build(Int32 seed = -1)
        {
            T[] items = _itemBuffer.ToArray();
            Double[] cda = _weightBuffer.ToArray();

            _itemBuffer.Clear();
            _weightBuffer.Clear();

            RandomMath.BuildCumulativeDistribution(cda);

            if (seed == -1)
            {
                seed = _random.Next();
            }

            // RandomMath.ArrayBreakpoint decides where to use Linear or Binary search, based on internal buffer size
            // if CDA array is smaller than breakpoint, then pick linear search random selector, else pick binary search selector
            if (cda.Length < RandomMath.ArrayBreakpoint)
            {
                return new StaticRandomSelectorLinear<T>(items, cda, seed);
            }

            return new StaticRandomSelectorBinary<T>(items, cda, seed);
        }

        private static readonly RandomSelectorBuilder<T> StaticBuilder = new RandomSelectorBuilder<T>();

        /// <summary>
        /// non-instance based, Double threaded only. For ease of use. 
        /// Build from array of items/weights.
        /// </summary>
        /// <param name="itemsArray">Array of items</param>
        /// <param name="weightsArray">Array of non-zero non-normalized weights. Have to be same length as itemsArray.</param>
        /// <returns></returns>
        public static IRandomSelector<T> Build(T[] itemsArray, Double[] weightsArray)
        {
            StaticBuilder.Clear();

            for (Int32 i = 0; i < itemsArray.Length; i++)
            {
                StaticBuilder.Add(itemsArray[i], weightsArray[i]);
            }

            return StaticBuilder.Build();
        }


        /// <summary>
        /// non-instance based, Double threaded only. For ease of use. 
        /// Build from array of items/weights.
        /// </summary>
        /// <param name="itemsList">List of weights</param>
        /// <param name="weightsList">List of non-zero non-normalized weights. Have to be same length as itemsList.</param>
        /// <returns></returns>
        public static IRandomSelector<T> Build(List<T> itemsList, List<Double> weightsList)
        {
            StaticBuilder.Clear();

            for (Int32 i = 0; i < itemsList.Count; i++)
            {
                StaticBuilder.Add(itemsList[i], weightsList[i]);
            }

            return StaticBuilder.Build();
        }

        private void Clear()
        {
            _itemBuffer.Clear();
            _weightBuffer.Clear();
        }
    }
}