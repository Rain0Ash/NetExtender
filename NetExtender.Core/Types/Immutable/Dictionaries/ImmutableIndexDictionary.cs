// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

#if ImmutableIndexDictionary
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using NetExtender.Types.Immutable.Dictionaries.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Immutable.Dictionaries
{
    public sealed class ImmutableIndexDictionary<TKey, TValue> : IImmutableIndexDictionary<TKey, TValue>
    {
        public static ImmutableIndexDictionary<TKey, TValue> Empty { get; } = new ImmutableIndexDictionary<TKey, TValue>(ImmutableDictionary<TKey, TValue>.Empty, ImmutableList<TKey>.Empty);

        private ImmutableDictionary<TKey, TValue> Dictionary { get; }
        private ImmutableList<TKey> Order { get; }

        public Boolean IsEmpty
        {
            get
            {
                return Dictionary.IsEmpty;
            }
        }

        public Int32 Count
        {
            get
            {
                return Dictionary.Count;
            }
        }

        public IEqualityComparer<TKey> KeyComparer
        {
            get
            {
                return Dictionary.KeyComparer;
            }
        }

        public IEqualityComparer<TValue> ValueComparer
        {
            get
            {
                return Dictionary.ValueComparer;
            }
        }

        public IEnumerable<TKey> Keys
        {
            get
            {
                return Dictionary.Keys;
            }
        }

        public IEnumerable<TValue> Values
        {
            get
            {
                return Dictionary.Values;
            }
        }

        public IReadOnlyList<TKey> OrderedKeys
        {
            get
            {
                return Order;
            }
        }

        private ImmutableIndexDictionary([NotNull] ImmutableDictionary<TKey, TValue> dictionary, [NotNull] ImmutableList<TKey> order)
        {
            Dictionary = dictionary ?? throw new ArgumentNullException(nameof(dictionary));
            Order = order ?? throw new ArgumentNullException(nameof(order));
        }

        public Boolean ContainsKey(TKey key)
        {
            return Dictionary.ContainsKey(key);
        }

        public Boolean Contains(KeyValuePair<TKey, TValue> pair)
        {
            return Dictionary.Contains(pair);
        }

        public Int32 IndexOf(TKey key)
        {
            return Order.IndexOf(key);
        }

        public Boolean TryGetKey(TKey equalKey, out TKey actualKey)
        {
            return Dictionary.TryGetKey(equalKey, out actualKey);
        }

        public Boolean TryGetValue(TKey key, out TValue value)
        {
            return Dictionary.TryGetValue(key, out value);
        }

        public TValue GetValueByIndex(Int32 index)
        {
            if (index < 0 || index >= Order.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, null);
            }

            return this[Order[index]];
        }

        public KeyValuePair<TKey, TValue> GetKeyValuePairByIndex(Int32 index)
        {
            if (index < 0 || index >= Order.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, null);
            }

            return this.GetPair(Order[index]);
        }

        public Boolean TryGetKeyValuePairByIndex(Int32 index, out KeyValuePair<TKey, TValue> pair)
        {
            if (index >= 0 && index < Order.Count)
            {
                return this.TryGetPair(Order[index], out pair);
            }

            pair = default;
            return false;
        }

        public IImmutableIndexDictionary<TKey, TValue> AddRange(IEnumerable<KeyValuePair<TKey, TValue>> pairs)
        {
            return null;
        }

        public IImmutableIndexDictionary<TKey, TValue> InsertRange(IEnumerable<KeyValuePair<TKey, TValue>> pairs)
        {
            return InsertRange(0, pairs);
        }

        public IImmutableIndexDictionary<TKey, TValue> InsertRange(Int32 index, IEnumerable<KeyValuePair<TKey, TValue>> pairs)
        {
            throw new NotImplementedException();
        }

        public IImmutableIndexDictionary<TKey, TValue> RemoveRange([NotNull] IEnumerable<TKey> keys)
        {
            if (keys is null)
            {
                throw new ArgumentNullException(nameof(keys));
            }

            ICollection<TKey> collection = keys as ICollection<TKey> ?? keys.ToArray();

            return new ImmutableIndexDictionary<TKey, TValue>(Dictionary.RemoveRange(collection), Order.RemoveRange(collection));
        }

        public IImmutableIndexDictionary<TKey, TValue> Remove(TKey key)
        {
            return Dictionary.ContainsKey(key) ? new ImmutableIndexDictionary<TKey, TValue>(Dictionary.Remove(key), Order.Remove(key)) : this;
        }

        public IImmutableIndexDictionary<TKey, TValue> SetItem(TKey key, TValue value)
        {
            throw new NotImplementedException();
        }

        public IImmutableIndexDictionary<TKey, TValue> SetItems(IEnumerable<KeyValuePair<TKey, TValue>> items)
        {
            throw new NotImplementedException();
        }

        public IImmutableIndexDictionary<TKey, TValue> Clear()
        {
            return new ImmutableIndexDictionary<TKey, TValue>(Dictionary.Clear(), Order.Clear());
        }

        public IImmutableIndexDictionary<TKey, TValue> Add([NotNull] TKey key, TValue value)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return new ImmutableIndexDictionary<TKey, TValue>(Dictionary.Add(key, value), Order.Add(key));
        }

        IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.Add(TKey key, TValue value)
        {
            return Add(key, value);
        }

        IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.AddRange(IEnumerable<KeyValuePair<TKey, TValue>> pairs)
        {
            return AddRange(pairs);
        }

        public IImmutableIndexDictionary<TKey, TValue> Insert(TKey key, TValue value)
        {
            return Insert(0, key, value);
        }

        public IImmutableIndexDictionary<TKey, TValue> Insert(Int32 index, TKey key, TValue value)
        {
            if (index < 0 || index >= Order.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, null);
            }

            return new ImmutableIndexDictionary<TKey, TValue>(Dictionary.Add(key, value), Order.Insert(0, key));
        }

        public IImmutableIndexDictionary<TKey, TValue> TryInsert(TKey key, TValue value)
        {
            throw new NotImplementedException();
        }

        public IImmutableIndexDictionary<TKey, TValue> TryInsert(TKey key, TValue value, out Boolean result)
        {
            throw new NotImplementedException();
        }

        public IImmutableIndexDictionary<TKey, TValue> TryInsert(Int32 index, TKey key, TValue value)
        {
            throw new NotImplementedException();
        }

        public IImmutableIndexDictionary<TKey, TValue> TryInsert(Int32 index, TKey key, TValue value, out Boolean result)
        {
            throw new NotImplementedException();
        }

        public IImmutableIndexDictionary<TKey, TValue> Swap(Int32 index1, Int32 index2)
        {
            throw new NotImplementedException();
        }

        public IImmutableIndexDictionary<TKey, TValue> Reverse()
        {
            return new ImmutableIndexDictionary<TKey, TValue>(Dictionary, Order.Reverse());
        }

        public IImmutableIndexDictionary<TKey, TValue> Reverse(Int32 index, Int32 count)
        {
            return new ImmutableIndexDictionary<TKey, TValue>(Dictionary, Order.Reverse(index, count));
        }

        public IImmutableIndexDictionary<TKey, TValue> Sort()
        {
            return new ImmutableIndexDictionary<TKey, TValue>(Dictionary, Order.Sort());
        }

        public IImmutableIndexDictionary<TKey, TValue> Sort(Comparison<TKey> comparison)
        {
            return new ImmutableIndexDictionary<TKey, TValue>(Dictionary, Order.Sort(comparison));
        }

        public IImmutableIndexDictionary<TKey, TValue> Sort(IComparer<TKey>? comparer)
        {
            return new ImmutableIndexDictionary<TKey, TValue>(Dictionary, Order.Sort(comparer));
        }

        public IImmutableIndexDictionary<TKey, TValue> Sort(Int32 index, Int32 count, IComparer<TKey>? comparer)
        {
            return new ImmutableIndexDictionary<TKey, TValue>(Dictionary, Order.Sort(index, count, comparer));
        }

        IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.Clear()
        {
            return Clear();
        }

        IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.Remove(TKey key)
        {
            throw new NotImplementedException();
        }

        IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.RemoveRange(IEnumerable<TKey> keys)
        {
            throw new NotImplementedException();
        }

        IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.SetItem(TKey key, TValue value)
        {
            throw new NotImplementedException();
        }

        IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.SetItems(IEnumerable<KeyValuePair<TKey, TValue>> items)
        {
            throw new NotImplementedException();
        }

        public TValue this[TKey key]
        {
            get
            {
                return Dictionary[key];
            }
        }

        public IEnumerator<TKey> GetKeyEnumerator()
        {
            return Order.GetEnumerator();
        }

        public IEnumerator<TValue> GetValueEnumerator()
        {
            return Order.Select(key => this[key]).GetEnumerator();
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return Order.Select(key => new KeyValuePair<TKey, TValue>(key, this[key])).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
#endif