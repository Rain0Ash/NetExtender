// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using NetExtender.Random.Interfaces;
using NetExtender.Types.Dictionaries;
using NetExtender.Utilities.Numerics;
using NetExtender.Utilities.Types;

namespace NetExtender.Random
{
    public class RandomSelectorBuilder<T> : RandomSelector<T>, IRandomDictionarySelectorBuilder<T> where T : notnull
    {
        protected const Int32 DefaultCapacity = 4;
        protected IRandom Random { get; set; }
        protected IndexDictionary<T, Double> Items { get; }

        public override IReadOnlyCollection<T> Collection
        {
            get
            {
                return Items.Keys;
            }
        }

        public sealed override Int32 Count
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

        IEnumerable<T> IReadOnlyDictionary<T, Double>.Keys
        {
            get
            {
                return Keys;
            }
        }

        IEnumerable<Double> IReadOnlyDictionary<T, Double>.Values
        {
            get
            {
                return Values;
            }
        }

        Boolean ICollection<KeyValuePair<T, Double>>.IsReadOnly
        {
            get
            {
                return Items.IsReadOnly();
            }
        }

        public RandomSelectorBuilder()
            : this(null, RandomUtilities.Create())
        {
        }

        public RandomSelectorBuilder(Int32 capacity)
            : this(capacity, RandomUtilities.Create())
        {
        }
        
        public RandomSelectorBuilder(Int32 capacity, Int32 seed)
            : this(capacity, RandomUtilities.Create(seed))
        {
        }

        public RandomSelectorBuilder(IRandom random)
            : this(null, random)
        {
        }
        
        public RandomSelectorBuilder(Int32 capacity, IRandom random)
        {
            Random = random ?? throw new ArgumentNullException(nameof(random));
            Items = new IndexDictionary<T, Double>(capacity.Clamp(8, Int32.MaxValue));
        }
        
        public RandomSelectorBuilder(IEnumerable<KeyValuePair<T, Double>>? items)
            : this(items.Materialize())
        {
        }

        public RandomSelectorBuilder(IEnumerable<KeyValuePair<T, Double>>? items, Int32 seed)
            : this(items.Materialize(), seed)
        {
        }

        public RandomSelectorBuilder(IEnumerable<KeyValuePair<T, Double>>? items, IRandom random)
            : this(items.Materialize(), random)
        {
        }

        protected RandomSelectorBuilder(IReadOnlyCollection<KeyValuePair<T, Double>>? items)
            : this(items, RandomUtilities.Create())
        {
        }

        protected RandomSelectorBuilder(IReadOnlyCollection<KeyValuePair<T, Double>>? items, Int32 seed)
            : this(items, RandomUtilities.Create(seed))
        {
        }

        protected RandomSelectorBuilder(IReadOnlyCollection<KeyValuePair<T, Double>>? items, IRandom random)
        {
            Random = random ?? throw new ArgumentNullException(nameof(random));
            Items = new IndexDictionary<T, Double>(items?.Count ?? DefaultCapacity);

            if (items is null)
            {
                return;
            }

            Add(items);
            TrimExcess(Count.Clamp(8, Int32.MaxValue));
        }

        public Int32 EnsureCapacity(Int32 capacity)
        {
            return Items.EnsureCapacity(capacity);
        }

        public void TrimExcess()
        {
            Items.TrimExcess();
        }
        
        public void TrimExcess(Int32 capacity)
        {
            Items.TrimExcess(capacity);
        }

        public Boolean Contains(T key)
        {
            return ContainsKey(key);
        }

        public Boolean ContainsKey(T key)
        {
            return Items.ContainsKey(key);
        }
        
        public Boolean Contains(KeyValuePair<T, Double> item)
        {
            return Items.Contains(item);
        }
        
        public Int32 IndexOf(T key)
        {
            return Items.IndexOf(key);
        }
        
        public Int32 IndexOf(T key, Int32 index)
        {
            return Items.IndexOf(key, index);
        }
        
        public Int32 IndexOf(T key, Int32 index, Int32 count)
        {
            return Items.IndexOf(key, index, count);
        }
        
        public Int32 LastIndexOf(T key)
        {
            return Items.LastIndexOf(key);
        }
        
        public Int32 LastIndexOf(T key, Int32 index)
        {
            return Items.LastIndexOf(key, index);
        }
        
        public Int32 LastIndexOf(T key, Int32 index, Int32 count)
        {
            return Items.LastIndexOf(key, index, count);
        }
        
        public Boolean TryGetValue(T key, out Double value)
        {
            return Items.TryGetValue(key, out value);
        }
        
        public T GetKeyByIndex(Int32 index)
        {
            return Items.GetKeyByIndex(index);
        }

        public Double GetValueByIndex(Int32 index)
        {
            return Items.GetValueByIndex(index);
        }

        public KeyValuePair<T, Double> GetKeyValuePairByIndex(Int32 index)
        {
            return Items.GetKeyValuePairByIndex(index);
        }

        public Boolean TryGetKeyValuePairByIndex(Int32 index, out KeyValuePair<T, Double> pair)
        {
            return Items.TryGetKeyValuePairByIndex(index, out pair);
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
        
        public void Insert(T item, Double weight)
        {
            Insert(0, item, weight);
        }

        public void Insert(Int32 index, T item, Double weight)
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
            
                Items.Insert(item, weight);
            }
        }

        public Boolean TryInsert(T item, Double weight)
        {
            return TryInsert(0, item, weight);
        }

        public Boolean TryInsert(Int32 index, T item, Double weight)
        {
            if (Math.Abs(weight) < Double.Epsilon)
            {
                return false;
            }

            lock (Items)
            {
                if (Items.ContainsKey(item))
                {
                    Items[item] += weight;
                    return true;
                }
            
                Items.Insert(item, weight);
                return true;
            }
        }

        public void SetValueByIndex(Int32 index, Double value)
        {
            Items.SetValueByIndex(index, value);
        }

        public Boolean TrySetValueByIndex(Int32 index, Double value)
        {
            return Items.TrySetValueByIndex(index, value);
        }

        public Boolean Remove(T key)
        {
            return Items.Remove(key);
        }
        
        public Boolean Remove(KeyValuePair<T, Double> item)
        {
            return Items.Remove(item);
        }
        
        public Boolean RemoveAt(Int32 index)
        {
            return Items.RemoveAt(index);
        }

        public Boolean RemoveAt(Int32 index, out KeyValuePair<T, Double> pair)
        {
            return Items.RemoveAt(index, out pair);
        }
        
        public void Swap(Int32 index1, Int32 index2)
        {
            Items.Swap(index1, index2);
        }

        public void Reverse()
        {
            Items.Reverse();
        }

        public void Reverse(Int32 index, Int32 count)
        {
            Items.Reverse(index, count);
        }

        public void Sort()
        {
            Items.Sort();
        }

        public void Sort(Comparison<T> comparison)
        {
            Items.Sort(comparison);
        }

        public void Sort(IComparer<T>? comparer)
        {
            Items.Sort(comparer);
        }

        public void Sort(Int32 index, Int32 count, IComparer<T>? comparer)
        {
            Items.Sort(index, count, comparer);
        }

        public virtual void Clear()
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
        public virtual IRandomSelector<T> Build(Int32 seed)
        {
            if (Items.Count <= 0)
            {
                return Default;
            }
            
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

        [return: NotNullIfNotNull("alternate")]
        public override T? GetRandomOrDefault(T? alternate)
        {
            return Build().GetRandomOrDefault(alternate);
        }
        
        [return: NotNullIfNotNull("alternate")]
        public override T? GetRandomOrDefault(Double value, T? alternate)
        {
            return Build().GetRandomOrDefault(value, alternate);
        }

        public override T[] ToItemArray()
        {
            return Build().ToItemArray();
        }

        public override List<T> ToItemList()
        {
            return Build().ToItemList();
        }

        public override NullableDictionary<T, Double> ToItemDictionary()
        {
            return Build().ToItemDictionary();
        }

        public override IEnumerator<T> GetEnumerator()
        {
            return Build().GetEnumerator();
        }

        IEnumerator<KeyValuePair<T, Double>> IEnumerable<KeyValuePair<T, Double>>.GetEnumerator()
        {
            return Items.GetEnumerator();
        }
        
        public IEnumerator<T> GetKeyEnumerator()
        {
            return Items.GetKeyEnumerator();
        }

        public IEnumerator<Double> GetValueEnumerator()
        {
            return Items.GetValueEnumerator();
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