// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Collections.Generic;
using System;
using System.Collections;
using NetExtender.Random.Interfaces;
using NetExtender.Utils.Numerics;
using NetExtender.Utils.Types;

namespace NetExtender.Random
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RandomSelectorBuilder<T> : RandomSelector<T>, IRandomSelectorBuilder<T>, IDictionary<T, Double> where T : notnull
    {
        public Boolean IsReadOnly
        {
            get
            {
                return Items.IsReadOnly();
            }
        }
        
        protected IRandom Random { get; }
        protected Dictionary<T, Double> Items { get; }
        
        public Int32 Count
        {
            get
            {
                return Items.Count;
            }
        }

        public ICollection<T> Keys
        {
            get
            {
                return Items.Keys;
            }
        }

        public ICollection<Double> Values
        {
            get
            {
                return Items.Values;
            }
        }

        public RandomSelectorBuilder()
            : this(null, RandomUtils.Create())
        {
        }

        public RandomSelectorBuilder(Int32 seed)
            : this(RandomUtils.Create(seed))
        {
        }

        public RandomSelectorBuilder(IRandom random)
            : this(null, random)
        {
        }

        public RandomSelectorBuilder(IEnumerable<KeyValuePair<T, Double>>? items)
            : this(items, RandomUtils.Create())
        {
        }

        public RandomSelectorBuilder(IEnumerable<KeyValuePair<T, Double>>? items, Int32 seed)
            : this(items, RandomUtils.Create(seed))
        {
        }

        public RandomSelectorBuilder(IEnumerable<KeyValuePair<T, Double>>? items, IRandom random)
        {
            Random = random ?? throw new ArgumentNullException(nameof(random));
            Items = new Dictionary<T, Double>();

            if (items is not null)
            {
                Add(items);
            }
        }
        
        public Boolean ContainsKey(T key)
        {
            return Items.ContainsKey(key);
        }
        
        public Boolean Contains(KeyValuePair<T, Double> item)
        {
            return Items.Contains(item);
        }
        
        public Boolean TryGetValue(T key, out Double value)
        {
            return Items.TryGetValue(key, out value);
        }

        /// <summary>
        /// Add new item with weight into collection. Items with zero weight will be ignored.
        /// Be sure to call Build() after you are done adding items.
        /// </summary>
        /// <param name="item">Item that will be returned on random selection</param>
        /// <param name="weight">Non-zero non-normalized weight</param>
        public void Add(T item, Double weight)
        {
            if (Math.Abs(weight) < Double.Epsilon)
            {
                return;
            }

            lock (Items)
            {
                if (Items.ContainsKey(item))
                {
                    Items[item] += weight;
                    return;
                }
            
                Items.Add(item, weight);
            }
        }

        // ReSharper disable once UseDeconstructionOnParameter
        public void Add(KeyValuePair<T, Double> item)
        {
            Add(item.Key, item.Value);
        }
        
        public void Add(IEnumerable<KeyValuePair<T, Double>> items)
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            lock (Items)
            {
                foreach ((T item, Double weight) in items)
                {
                    Add(item, weight);
                }
            }
        }
        
        public Boolean Remove(T key)
        {
            return Items.Remove(key);
        }
        
        public Boolean Remove(KeyValuePair<T, Double> item)
        {
            return Items.Remove(item);
        }

        public void Clear()
        {
            Items.Clear();
        }

        /// <inheritdoc cref="Build(int)"/>
        public IRandomSelector<T> Build()
        {
            return Build(-1);
        }

        /// <summary>
        /// Builds StaticRandomSelector & clears internal buffers. Must be called after you finish Add-ing items.
        /// </summary>
        /// <param name="seed">Seed for random selector. If you leave it -1, the internal random will generate one.</param>
        /// <returns>Returns IRandomSelector, underlying objects are either StaticRandomSelectorLinear or StaticRandomSelectorBinary. Both are non-mutable.</returns>
        public IRandomSelector<T> Build(Int32 seed)
        {
            T[] items;
            Double[] cda;
            
            lock (Items)
            {
                Int32 count = Items.Count;
                items = new T[count];
                cda = new Double[count];

                foreach ((Int32 counter, (T key, Double value)) in Items.Enumerate())
                {
                    items[counter] = key;
                    cda[counter] = value;
                }
            }

            BuildCumulativeDistribution(cda);

            if (seed == -1)
            {
                seed = Random.Next();
            }

            // RandomMath.ArrayBreakpoint decides where to use Linear or Binary search, based on internal buffer size
            // if CDA array is smaller than breakpoint, then pick linear search random selector, else pick binary search selector
            if (cda.Length < ArrayBreakpoint)
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
        /// <param name="items">Array of items</param>
        /// <param name="weights">Array of non-zero non-normalized weights. Have to be same length as itemsArray.</param>
        /// <returns></returns>
        public static IRandomSelector<T> Build(T[] items, Double[] weights)
        {
            StaticBuilder.Clear();

            for (Int32 i = 0; i < items.Length; i++)
            {
                StaticBuilder.Add(items[i], weights[i]);
            }

            return StaticBuilder.Build();
        }

        /// <summary>
        /// non-instance based, Double threaded only. For ease of use. 
        /// Build from array of items/weights.
        /// </summary>
        /// <param name="items">List of weights</param>
        /// <param name="weights">List of non-zero non-normalized weights. Have to be same length as itemsList.</param>
        /// <returns></returns>
        public static IRandomSelector<T> Build(List<T> items, List<Double> weights)
        {
            StaticBuilder.Clear();

            for (Int32 i = 0; i < items.Count; i++)
            {
                StaticBuilder.Add(items[i], weights[i]);
            }

            return StaticBuilder.Build();
        }
        
        public void CopyTo(KeyValuePair<T, Double>[] array, Int32 arrayIndex)
        {
            Items.CopyTo(array, arrayIndex);
        }

        public override T GetRandom()
        {
            return Build().GetRandom();
        }

        public override T GetRandom(Double value)
        {
            return Build().GetRandom(value);
        }

        public override IEnumerator<T> GetEnumerator()
        {
            return Build().GetEnumerator();
        }

        IEnumerator<KeyValuePair<T, Double>> IEnumerable<KeyValuePair<T, Double>>.GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        
        public Double this[T key]
        {
            get
            {
                return Items[key];
            }
            set
            {
                if (Math.Abs(value) < Double.Epsilon)
                {
                    Items.Remove(key);
                    return;
                }
                
                Items[key] = value;
            }
        }
    }
}