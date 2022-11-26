// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using NetExtender.Types.Lists;
using NetExtender.Types.Maps.Interfaces;
using NetExtender.Utilities.Numerics;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Maps
{
    public class IndexMap<TKey, TValue> : Map<TKey, TValue>, IIndexMap<TKey, TValue>, IReadOnlyIndexMap<TKey, TValue> where TKey : notnull where TValue : notnull
    {
        private IndexList<TKey> Order { get; }

        public IndexMap()
        {
            Order = new IndexList<TKey>(Comparer);
        }

        public IndexMap(IDictionary<TKey, TValue> dictionary)
            : base(dictionary)
        {
            Order = new IndexList<TKey>(Base.Keys, Comparer);
        }

        public IndexMap(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey>? comparer)
            : base(dictionary, comparer)
        {
            Order = new IndexList<TKey>(Base.Keys, Comparer);
        }

        public IndexMap(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey>? keyComparer, IEqualityComparer<TValue>? valueComparer)
            : base(dictionary, keyComparer, valueComparer)
        {
            Order = new IndexList<TKey>(Base.Keys, Comparer);
        }

        public IndexMap(IEqualityComparer<TKey>? comparer)
            : base(comparer)
        {
            Order = new IndexList<TKey>(Comparer);
        }

        public IndexMap(IEqualityComparer<TKey>? keyComparer, IEqualityComparer<TValue>? valueComparer)
            : base(keyComparer, valueComparer)
        {
            Order = new IndexList<TKey>(Comparer);
        }

        public IndexMap(IEnumerable<KeyValuePair<TKey, TValue>> source)
            : base(source)
        {
            Order = new IndexList<TKey>(Base.Keys, Comparer);
        }

        public IndexMap(IEnumerable<KeyValuePair<TKey, TValue>> source, IEqualityComparer<TKey>? comparer)
            : base(source, comparer)
        {
            Order = new IndexList<TKey>(Base.Keys, Comparer);
        }

        public IndexMap(IEnumerable<KeyValuePair<TKey, TValue>> source, IEqualityComparer<TKey>? keyComparer, IEqualityComparer<TValue>? valueComparer)
            : base(source, keyComparer, valueComparer)
        {
            Order = new IndexList<TKey>(Base.Keys, Comparer);
        }

        public IndexMap(Int32 capacity)
            : base(capacity)
        {
            Order = new IndexList<TKey>(capacity, Comparer);
        }

        public IndexMap(Int32 capacity, IEqualityComparer<TKey>? keyComparer, IEqualityComparer<TValue>? valueComparer)
            : base(capacity, keyComparer, valueComparer)
        {
            Order = new IndexList<TKey>(capacity, Comparer);
        }

        public override Int32 EnsureCapacity(Int32 capacity)
        {
            if (Count >= capacity)
            {
                return Count;
            }

            Int32 ensure = base.EnsureCapacity(capacity);
            Order.Capacity = ensure;

            return ensure;
        }

        public override void TrimExcess(Int32 capacity)
        {
            Int32 count = Count;
            if (capacity < count)
            {
                throw new ArgumentOutOfRangeException(nameof(capacity));
            }

            if (capacity == count)
            {
                return;
            }

            base.TrimExcess(capacity);
            Order.Capacity = capacity;
        }

        public TKey GetKeyByIndex(Int32 index)
        {
            return Order[index];
        }

        public TValue GetValueByIndex(Int32 index)
        {
            return this[GetKeyByIndex(index)];
        }

        public KeyValuePair<TKey, TValue> GetKeyValuePairByIndex(Int32 index)
        {
            return this.GetPair(GetKeyByIndex(index));
        }

        public KeyValuePair<TValue, TKey> GetValueKeyPairByIndex(Int32 index)
        {
            return Reversed.GetPair(GetValueByIndex(index));
        }

        public Boolean TryGetKeyValuePairByIndex(Int32 index, out KeyValuePair<TKey, TValue> pair)
        {
            return this.TryGetPair(GetKeyByIndex(index), out pair);
        }

        public Boolean TryGetValueKeyPairByIndex(Int32 index, out KeyValuePair<TValue, TKey> pair)
        {
            return Reversed.TryGetPair(GetValueByIndex(index), out pair);
        }

        public Int32 IndexOf(TKey key)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return Order.IndexOf(key);
        }

        public Int32 IndexOf(TKey key, Int32 index)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return Order.IndexOf(key, index);
        }

        public Int32 IndexOf(TKey key, Int32 index, Int32 count)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return Order.IndexOf(key, index, count);
        }

        public Int32 LastIndexOf(TKey key)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return Order.LastIndexOf(key);
        }

        public Int32 LastIndexOf(TKey key, Int32 index)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return Order.LastIndexOf(key, index);
        }

        public Int32 LastIndexOf(TKey key, Int32 index, Int32 count)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return Order.LastIndexOf(key, index, count);
        }

        public Int32 IndexOfValue(TValue key)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return IndexOf(Reversed[key]);
        }

        public Int32 IndexOfValue(TValue key, Int32 index)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return IndexOf(Reversed[key], index);
        }

        public Int32 IndexOfValue(TValue key, Int32 index, Int32 count)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return IndexOf(Reversed[key], index, count);
        }

        public Int32 LastIndexOfValue(TValue key)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return LastIndexOf(Reversed[key]);
        }

        public Int32 LastIndexOfValue(TValue key, Int32 index)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return LastIndexOf(Reversed[key], index);
        }

        public Int32 LastIndexOfValue(TValue key, Int32 index, Int32 count)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return LastIndexOf(Reversed[key], index, count);
        }

        public override void Add(TKey key, TValue value)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            base.Add(key, value);
            Order.Add(key);
        }

        public void Insert(KeyValuePair<TKey, TValue> item)
        {
            Insert(0, item);
        }

        public void Insert(Int32 index, KeyValuePair<TKey, TValue> item)
        {
            Insert(index, item.Key, item.Value);
        }

        public void Insert(TKey key, TValue value)
        {
            Insert(0, key, value);
        }

        public virtual void Insert(Int32 index, TKey key, TValue value)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (index < 0 || index >= Order.Count)
            {
                throw new ArgumentException(@"Less than zero or more than count", nameof(index));
            }

            if (ContainsKey(key))
            {
                throw new ArgumentException(@"Already exists", nameof(key));
            }

            if (ContainsValue(value))
            {
                throw new ArgumentException(@"Already exists", nameof(value));
            }

            base.Add(key, value);
            Order.Insert(index, key);
        }

        public void InsertByValue(KeyValuePair<TValue, TKey> item)
        {
            InsertByValue(0, item);
        }

        public void InsertByValue(Int32 index, KeyValuePair<TValue, TKey> item)
        {
            InsertByValue(index, item.Key, item.Value);
        }

        public void InsertByValue(TValue key, TKey value)
        {
            InsertByValue(0, key, value);
        }

        public void InsertByValue(Int32 index, TValue key, TKey value)
        {
            Insert(index, value, key);
        }

        public Boolean TryInsert(KeyValuePair<TKey, TValue> item)
        {
            return TryInsert(0, item);
        }

        public Boolean TryInsert(Int32 index, KeyValuePair<TKey, TValue> item)
        {
            return TryInsert(index, item.Key, item.Value);
        }

        public Boolean TryInsert(TKey key, TValue value)
        {
            return TryInsert(0, key, value);
        }

        public Boolean TryInsert(Int32 index, TKey key, TValue value)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (index < 0 || index >= Order.Count)
            {
                return false;
            }

            if (ContainsKey(key) || ContainsValue(value))
            {
                return false;
            }

            Insert(index, key, value);
            return true;
        }

        public Boolean TryInsertByValue(KeyValuePair<TValue, TKey> item)
        {
            return TryInsertByValue(0, item);
        }

        public Boolean TryInsertByValue(Int32 index, KeyValuePair<TValue, TKey> item)
        {
            return TryInsertByValue(index, item.Key, item.Value);
        }

        public Boolean TryInsertByValue(TValue key, TKey value)
        {
            return TryInsertByValue(0, key, value);
        }

        public Boolean TryInsertByValue(Int32 index, TValue key, TKey value)
        {
            return TryInsert(index, value, key);
        }

        public void Swap(Int32 index1, Int32 index2)
        {
            Order.Swap(index1, index2);
        }

        public void Reverse()
        {
            Order.Reverse();
        }

        public void Reverse(Int32 index, Int32 count)
        {
            Order.Reverse(index, count);
        }

        public void Sort()
        {
            Order.Sort();
        }

        public void Sort(Comparison<TKey> comparison)
        {
            Order.Sort(comparison);
        }

        public void Sort(IComparer<TKey>? comparer)
        {
            Order.Sort(comparer);
        }

        public void Sort(Int32 index, Int32 count, IComparer<TKey>? comparer)
        {
            Order.Sort(index, count, comparer);
        }

        public override Boolean Remove(TKey key, TValue value)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (!base.Remove(key, value))
            {
                return false;
            }

            Order.Remove(key);
            return true;
        }

        public Boolean RemoveAt(Int32 index)
        {
            return RemoveAt(index, out _);
        }

        public Boolean RemoveAt(Int32 index, out KeyValuePair<TKey, TValue> pair)
        {
            if (!index.InRange(0, Count - 1))
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            if (!Order.TryGetValue(index, out TKey? key))
            {
                pair = default;
                return false;
            }

            Order.RemoveAt(index);
            if (base.Remove(key, out TValue? value))
            {
                pair = new KeyValuePair<TKey, TValue>(key, value);
                return true;
            }

            pair = default;
            return false;
        }

        public override void Clear()
        {
            base.Clear();
            Order.Clear();
        }

        public IEnumerator<TKey> GetKeyEnumerator()
        {
            return Order.GetEnumerator();
        }

        public IEnumerator<TValue> GetValueEnumerator()
        {
            return Order.Select(key => this[key]).GetEnumerator();
        }

        public override TValue this[TKey key]
        {
            get
            {
                if (key is null)
                {
                    throw new ArgumentNullException(nameof(key));
                }

                return base[key];
            }
            set
            {
                if (key is null)
                {
                    throw new ArgumentNullException(nameof(key));
                }

                if (value is null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                Boolean contains = ContainsKey(key);

                base[key] = value;

                if (!contains)
                {
                    Order.Add(key);
                }
            }
        }
    }
}