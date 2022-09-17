// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using NetExtender.Types.Dictionaries.Interfaces;
using NetExtender.Types.Lists;
using NetExtender.Utilities.Numerics;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Dictionaries
{
    [Serializable]
    public class IndexDictionary<TKey, TValue> : IIndexDictionary<TKey, TValue>, IReadOnlyIndexDictionary<TKey, TValue> where TKey : notnull
    {
        private Dictionary<TKey, TValue> Dictionary { get; }
        private IndexList<TKey> Order { get; }

        public Int32 Count
        {
            get
            {
                return Dictionary.Count;
            }
        }

        Boolean ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
        {
            get
            {
                return ((ICollection<KeyValuePair<TKey, TValue>>) Dictionary).IsReadOnly;
            }
        }

        public IEqualityComparer<TKey> Comparer
        {
            get
            {
                return Dictionary.Comparer;
            }
        }

        public Dictionary<TKey, TValue>.KeyCollection Keys
        {
            get
            {
                return Dictionary.Keys;
            }
        }
        
        public Dictionary<TKey, TValue>.ValueCollection Values
        {
            get
            {
                return Dictionary.Values;
            }
        }
        
        IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys
        {
            get
            {
                return Dictionary.Keys;
            }
        }

        IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values
        {
            get
            {
                return Dictionary.Values;
            }
        }

        ICollection<TKey> IDictionary<TKey, TValue>.Keys
        {
            get
            {
                return Dictionary.Keys;
            }
        }

        ICollection<TValue> IDictionary<TKey, TValue>.Values
        {
            get
            {
                return Dictionary.Values;
            }
        }

        public IndexDictionary()
        {
            Dictionary = new Dictionary<TKey, TValue>();
            Order = new IndexList<TKey>(Comparer);
        }

        public IndexDictionary(IDictionary<TKey, TValue> dictionary)
        {
            if (dictionary is null)
            {
                throw new ArgumentNullException(nameof(dictionary));
            }

            Dictionary = new Dictionary<TKey, TValue>(dictionary);
            Order = new IndexList<TKey>(Dictionary.Keys, Comparer);
        }

        public IndexDictionary(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey>? comparer)
        {
            if (dictionary is null)
            {
                throw new ArgumentNullException(nameof(dictionary));
            }

            Dictionary = new Dictionary<TKey, TValue>(dictionary, comparer);
            Order = new IndexList<TKey>(Dictionary.Keys, Comparer);
        }

        public IndexDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }
            
            Dictionary = new Dictionary<TKey, TValue>(collection);
            Order = new IndexList<TKey>(Dictionary.Keys, Comparer);
        }

        public IndexDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection, IEqualityComparer<TKey>? comparer)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            Dictionary = new Dictionary<TKey, TValue>(collection, comparer);
            Order = new IndexList<TKey>(Dictionary.Keys, Comparer);
        }

        public IndexDictionary(IEqualityComparer<TKey>? comparer)
        {
            Dictionary = new Dictionary<TKey, TValue>(comparer);
            Order = new IndexList<TKey>(Comparer);
        }

        public IndexDictionary(Int32 capacity)
        {
            Dictionary = new Dictionary<TKey, TValue>(capacity);
            Order = new IndexList<TKey>(capacity, Comparer);
        }

        public IndexDictionary(Int32 capacity, IEqualityComparer<TKey>? comparer)
        {
            Dictionary = new Dictionary<TKey, TValue>(capacity, comparer);
            Order = new IndexList<TKey>(capacity, Comparer);
        }

        public Int32 EnsureCapacity(Int32 capacity)
        {
            if (Count >= capacity)
            {
                return Count;
            }
            
            Int32 ensure = Dictionary.EnsureCapacity(capacity);
            Order.Capacity = ensure;
            return ensure;
        }

        public void TrimExcess()
        {
            TrimExcess(Count);
        }
        
        public void TrimExcess(Int32 capacity)
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
            
            Dictionary.TrimExcess(capacity);
            Order.Capacity = capacity;
        }
        
        public TKey GetKeyByIndex(Int32 index)
        {
            if (index < 0 || index >= Order.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }
            
            return Order[index];
        }

        public TValue GetValueByIndex(Int32 index)
        {
            return this[GetKeyByIndex(index)];
        }

        public KeyValuePair<TKey, TValue> GetKeyValuePairByIndex(Int32 index)
        {
            if (index < 0 || index >= Order.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
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
        
        public Boolean ContainsKey(TKey key)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return Dictionary.ContainsKey(key);
        }
        
        Boolean ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item)
        {
            return ((ICollection<KeyValuePair<TKey, TValue>>) Dictionary).Contains(item);
        }

        public Boolean TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return Dictionary.TryGetValue(key, out value);
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

        public void Add(TKey key, TValue value)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            Dictionary.Add(key, value);
            Order.Add(key);
        }
        
        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
        {
            Add(item.Key, item.Value);
        }

        public Boolean TryAdd(TKey key, TValue value)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (!Dictionary.TryAdd(key, value))
            {
                return false;
            }

            Order.Add(key);
            return true;
        }

        public void Insert(TKey key, TValue value)
        {
            Insert(0, key, value);
        }

        public void Insert(Int32 index, TKey key, TValue value)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (index < 0 || index >= Order.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            Dictionary.Add(key, value);
            Order.Insert(index, key);
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

            if (index < 0 || index >= Order.Count)
            {
                return false;
            }

            if (!Dictionary.TryAdd(key, value))
            {
                return false;
            }

            Order.Insert(index, key);
            return true;
        }
        
        public void SetValueByIndex(Int32 index, TValue value)
        {
            if (index < 0 || index >= Order.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            Dictionary[Order[index]] = value;
        }

        public Boolean TrySetValueByIndex(Int32 index, TValue value)
        {
            if (index < 0 || index >= Order.Count)
            {
                return false;
            }

            Dictionary[Order[index]] = value;
            return true;
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

        public Boolean Remove(TKey key)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return Dictionary.Remove(key) | Order.Remove(key);
        }

        public Boolean Remove(TKey key, [MaybeNullWhen(false)] out TValue value)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return Dictionary.Remove(key, out value) | Order.Remove(key);
        }
        
        Boolean ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
        {
            (TKey key, TValue value) = item;
            if (Dictionary.TryGetValue(key, out TValue? result) && Equals(result, value))
            {
                return Remove(key);
            }

            return false;
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
            if (Dictionary.Remove(key, out TValue? value))
            {
                pair = new KeyValuePair<TKey, TValue>(key, value);
                return true;
            }

            pair = default;
            return false;
        }

        public void Clear()
        {
            Dictionary.Clear();
            Order.Clear();
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, Int32 arrayIndex)
        {
            ((ICollection<KeyValuePair<TKey, TValue>>) Dictionary).CopyTo(array, arrayIndex);
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
        
        public TValue this[TKey key]
        {
            get
            {
                if (key is null)
                {
                    throw new ArgumentNullException(nameof(key));
                }

                return Dictionary[key];
            }
            set
            {
                if (key is null)
                {
                    throw new ArgumentNullException(nameof(key));
                }

                if (key is null)
                {
                    throw new ArgumentNullException(nameof(key));
                }
                
                if (!ContainsKey(key))
                {
                    Add(key, value);
                    return;
                }

                Dictionary[key] = value;
            }
        }
    }
}