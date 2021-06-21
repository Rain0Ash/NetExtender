// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using NetExtender.Utils.Types;
using NetExtender.Types.Dictionaries.Interfaces;

namespace NetExtender.Types.Dictionaries
{
    public class IndexDictionary<TKey, TValue> : IIndexDictionary<TKey, TValue>, IReadOnlyIndexDictionary<TKey, TValue> where TKey : notnull
    {
        private readonly List<TKey> _order;
        private readonly Dictionary<TKey, TValue> _dictionary;
        
        public Int32 Count
        {
            get
            {
                return _dictionary.Count;
            }
        }

        Boolean ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
        {
            get
            {
                return ((ICollection<KeyValuePair<TKey, TValue>>) _dictionary).IsReadOnly;
            }
        }

        public Dictionary<TKey, TValue>.KeyCollection Keys
        {
            get
            {
                return _dictionary.Keys;
            }
        }
        
        public Dictionary<TKey, TValue>.ValueCollection Values
        {
            get
            {
                return _dictionary.Values;
            }
        }
        
        IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys
        {
            get
            {
                return _dictionary.Keys;
            }
        }

        IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values
        {
            get
            {
                return _dictionary.Values;
            }
        }

        ICollection<TKey> IDictionary<TKey, TValue>.Keys
        {
            get
            {
                return _dictionary.Keys;
            }
        }

        ICollection<TValue> IDictionary<TKey, TValue>.Values
        {
            get
            {
                return _dictionary.Values;
            }
        }

        public IReadOnlyList<TKey> OrderedKeys
        {
            get
            {
                return _order;
            }
        }

        public IndexDictionary()
        {
            _dictionary = new Dictionary<TKey, TValue>();
            _order = new List<TKey>();
        }

        public IndexDictionary(IDictionary<TKey, TValue> dictionary)
        {
            if (dictionary is null)
            {
                throw new ArgumentNullException(nameof(dictionary));
            }

            _dictionary = new Dictionary<TKey, TValue>(dictionary);
            _order = new List<TKey>(dictionary.Keys);
        }

        public IndexDictionary(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey>? comparer)
        {
            if (dictionary is null)
            {
                throw new ArgumentNullException(nameof(dictionary));
            }

            _dictionary = new Dictionary<TKey, TValue>(dictionary, comparer);
            _order = new List<TKey>(dictionary.Keys);
        }

        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public IndexDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }
            
            collection = collection.Materialize();

            _dictionary = new Dictionary<TKey, TValue>(collection);
            _order = new List<TKey>(collection.Select(pair => pair.Key));
        }

        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public IndexDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection, IEqualityComparer<TKey>? comparer)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            collection = collection.Materialize();

            _dictionary = new Dictionary<TKey, TValue>(collection, comparer);
            _order = new List<TKey>(collection.Select(pair => pair.Key));
        }

        public IndexDictionary(IEqualityComparer<TKey>? comparer)
        {
            _dictionary = new Dictionary<TKey, TValue>(comparer);
            _order = new List<TKey>();
        }

        public IndexDictionary(Int32 capacity)
        {
            _dictionary = new Dictionary<TKey, TValue>(capacity);
            _order = new List<TKey>(capacity);
        }

        public IndexDictionary(Int32 capacity, IEqualityComparer<TKey>? comparer)
        {
            _dictionary = new Dictionary<TKey, TValue>(capacity, comparer);
            _order = new List<TKey>(capacity);
        }

        public TValue GetValueByIndex(Int32 index)
        {
            if (index < 0 || index >= _order.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }
            
            return this[_order[index]];
        }

        public KeyValuePair<TKey, TValue> GetKeyValuePairByIndex(Int32 index)
        {
            if (index < 0 || index >= _order.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }
            
            return this.GetPair(_order[index]);
        }

        public Boolean TryGetKeyValuePairByIndex(Int32 index, out KeyValuePair<TKey, TValue> pair)
        {
            if (index >= 0 && index < _order.Count)
            {
                return this.TryGetPair(_order[index], out pair);
            }

            pair = default;
            return false;
        }

        public Int32 IndexOf(TKey key)
        {
            return _order.IndexOf(key);
        }

        public void Add(TKey key, TValue value)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            _dictionary.Add(key, value);
            _order.Add(key);
        }

        public Boolean ContainsKey(TKey key)
        {
            return _dictionary.ContainsKey(key);
        }

        public Boolean TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
        {
            return _dictionary.TryGetValue(key, out value);
        }

        public Boolean TryAdd(TKey key, TValue value)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (!_dictionary.TryAdd(key, value))
            {
                return false;
            }

            _order.Add(key);
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

            if (index < 0 || index > _order.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            _dictionary.Add(key, value);
            _order.Insert(index, key);
        }

        public Boolean TryInsert(TKey key, TValue value)
        {
            return TryInsert(0, key, value);
        }

        public Boolean TryInsert(Int32 index, TKey key, TValue value)
        {
            if (index < 0 || index >= _order.Count)
            {
                return false;
            }

            if (!_dictionary.TryAdd(key, value))
            {
                return false;
            }

            _order.Insert(index, key);
            return true;
        }

        public void Swap(Int32 index1, Int32 index2)
        {
            _order.Swap(index1, index2);
        }

        public Boolean Remove(TKey key)
        {
            return _dictionary.Remove(key) | _order.Remove(key);
        }

        public Boolean Remove(TKey key, out TValue? value)
        {
            return _dictionary.Remove(key, out value) | _order.Remove(key);
        }

        public void Reverse()
        {
            _order.Reverse();
        }

        public void Reverse(Int32 index, Int32 count)
        {
            _order.Reverse(index, count);
        }

        public void Sort()
        {
            _order.Sort();
        }

        public void Sort(Comparison<TKey> comparison)
        {
            _order.Sort(comparison);
        }

        public void Sort(IComparer<TKey>? comparer)
        {
            _order.Sort(comparer);
        }

        public void Sort(Int32 index, Int32 count, IComparer<TKey>? comparer)
        {
            _order.Sort(index, count, comparer);
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            (TKey key, TValue value) = item;
            if (key is null)
            {
                return;
            }

            Add(key, value);
        }

        public void Clear()
        {
            _dictionary.Clear();
            _order.Clear();
        }

        public Boolean Contains(KeyValuePair<TKey, TValue> item)
        {
            return ((ICollection<KeyValuePair<TKey, TValue>>) _dictionary).Contains(item);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, Int32 arrayIndex)
        {
            ((ICollection<KeyValuePair<TKey, TValue>>) _dictionary).CopyTo(array, arrayIndex);
        }

        public Boolean Remove(KeyValuePair<TKey, TValue> item)
        {
            (TKey key, TValue value) = item;
            if (_dictionary.TryGetValue(key, out TValue? result) && Equals(result, value))
            {
                return Remove(key);
            }

            return false;
        }

        public TValue this[TKey key]
        {
            get
            {
                return _dictionary[key];
            }
            set
            {
                if (key is null)
                {
                    throw new ArgumentNullException(nameof(key));
                }
                
                if (!ContainsKey(key))
                {
                    Add(key, value);
                    return;
                }

                _dictionary[key] = value;
            }
        }

        public IEnumerator<TKey> GetKeyEnumerator()
        {
            return _order.GetEnumerator();
        }

        public IEnumerator<TValue> GetValueEnumerator()
        {
            return _order.Select(key => this[key]).GetEnumerator();
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _order.Select(key => new KeyValuePair<TKey, TValue>(key, this[key])).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}