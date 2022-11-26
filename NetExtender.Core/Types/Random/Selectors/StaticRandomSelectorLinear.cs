// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using NetExtender.Types.Dictionaries;
using NetExtender.Types.Random.Interfaces;
using NetExtender.Utilities.Numerics;

namespace NetExtender.Types.Random
{
    /// <summary>
    /// Uses Linear Search for picking random items
    /// Good for small sized number of items
    /// </summary>
    /// <typeparam name="T">Type of items you wish this selector returns</typeparam>
    public class StaticRandomSelectorLinear<T> : RandomSelector<T>
    {
        protected IRandom Random { get; }
        protected T[] Items { get; }
        protected Double[] Distribution { get; }

        public override IReadOnlyCollection<T> Collection
        {
            get
            {
                return Items;
            }
        }

        public sealed override Int32 Count
        {
            get
            {
                return Items.Length;
            }
        }

        /// <summary>
        /// Constructor, used by StaticRandomSelectorBuilder
        /// Needs array of items and CDA (Cummulative Distribution Array). 
        /// </summary>
        /// <param name="items">Items of type T</param>
        /// <param name="cda">Cummulative Distribution Array</param>
        /// <param name="seed">Seed for internal random generator</param>
        public StaticRandomSelectorLinear(T[] items, Double[] cda, Int32 seed)
        {
            Items = items ?? throw new ArgumentNullException(nameof(items));
            Distribution = cda ?? throw new ArgumentNullException(nameof(cda));
            Random = RandomUtilities.Create(seed);
        }

        /// <summary>
        /// Selects random item based on their weights.
        /// Uses linear search for random selection.
        /// </summary>
        /// <returns>Returns item</returns>
        public override T GetRandom()
        {
            if (Items.Length <= 0)
            {
                throw new InvalidOperationException("Items is empty");
            }

            return Items[SelectIndexBinarySearch(Distribution, Random.NextDouble())];
        }

        /// <summary>
        /// Selects random item based on their weights.
        /// Uses linear search for random selection.
        /// </summary>
        /// <returns>Returns item</returns>
        public override T GetRandom(Double value)
        {
            if (Items.Length <= 0)
            {
                throw new InvalidOperationException("Items is empty");
            }

            return Items[SelectIndexBinarySearch(Distribution, value)];
        }

        [return: NotNullIfNotNull("alternate")]
        public override T? GetRandomOrDefault(T? alternate)
        {
            return Items.Length > 0 ? Items[SelectIndexBinarySearch(Distribution, Random.NextDouble())] : alternate;
        }

        [return: NotNullIfNotNull("alternate")]
        public override T? GetRandomOrDefault(Double value, T? alternate)
        {
            return Items.Length > 0 ? Items[SelectIndexBinarySearch(Distribution, value)] : alternate;
        }

        public override T[] ToItemArray()
        {
            return Collection.ToArray();
        }

        public override List<T> ToItemList()
        {
            return Collection.ToList();
        }

        public override NullableDictionary<T, Double> ToItemDictionary()
        {
            NullableDictionary<T, Double> dictionary = new NullableDictionary<T, Double>(Count);

            foreach ((T? item, Double weight) in Items.Zip(Distribution))
            {
                if (dictionary.ContainsKey(item))
                {
                    dictionary[item] += weight;
                    continue;
                }

                dictionary.Add(item, weight);
            }

            return dictionary;
        }
    }
}