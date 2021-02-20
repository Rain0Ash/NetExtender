// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using NetExtender.Types.Immutable.Dictionaries.Interfaces;
using NetExtender.Utils.Types;

namespace NetExtender.Types.Immutable.Dictionaries
{
    public sealed class ImmutableIndexDictionary<TKey, TValue> : IImmutableIndexDictionary<TKey, TValue>
    {
        private readonly ImmutableDictionary<TKey, TValue> _dictionary;
        private readonly ImmutableList<TKey> _order;

        public Boolean IsEmpty
        {
            get
            {
                return _dictionary.IsEmpty;
            }
        }
        
        public Int32 Count
        {
            get
            {
                return _dictionary.Count;
            }
        }

        public IEqualityComparer<TKey> KeyComparer
        {
            get
            {
                return _dictionary.KeyComparer;
            }
        }
        
        public IEqualityComparer<TValue> ValueComparer
        {
            get
            {
                return _dictionary.ValueComparer;
            }
        }

        public IEnumerable<TKey> Keys
        {
            get
            {
                return _dictionary.Keys;
            }
        }

        public IEnumerable<TValue> Values
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

        public ImmutableIndexDictionary([JetBrains.Annotations.NotNull] IDictionary<TKey, TValue> dictionary)
        {
            if (dictionary is null)
            {
                throw new ArgumentNullException(nameof(dictionary));
            }

            _dictionary = dictionary.ToImmutableDictionary();
            _order = dictionary.Keys.ToImmutableList();
        }

        public ImmutableIndexDictionary([JetBrains.Annotations.NotNull] IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey>? comparer)
        {
            if (dictionary is null)
            {
                throw new ArgumentNullException(nameof(dictionary));
            }

            _dictionary = dictionary.ToImmutableDictionary(comparer);
            _order = dictionary.Keys.ToImmutableList();
        }
        
        public ImmutableIndexDictionary([JetBrains.Annotations.NotNull] IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey>? keyComparer, IEqualityComparer<TValue>? valueComparer)
        {
            if (dictionary is null)
            {
                throw new ArgumentNullException(nameof(dictionary));
            }

            _dictionary = dictionary.ToImmutableDictionary(keyComparer, valueComparer);
            _order = dictionary.Keys.ToImmutableList();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public ImmutableIndexDictionary([JetBrains.Annotations.NotNull] IEnumerable<KeyValuePair<TKey, TValue>> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            source = source.Materialize();

            _dictionary = source.ToImmutableDictionary();
            _order = source.Select(pair => pair.Key).ToImmutableList();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public ImmutableIndexDictionary([JetBrains.Annotations.NotNull] IEnumerable<KeyValuePair<TKey, TValue>> source, IEqualityComparer<TKey>? keyComparer, IEqualityComparer<TValue>? valueComparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            source = source.Materialize();
            
            _dictionary = source.ToImmutableDictionary(keyComparer, valueComparer);
            _order = source.Select(pair => pair.Key).ToImmutableList();
        }

        private ImmutableIndexDictionary([JetBrains.Annotations.NotNull] ImmutableDictionary<TKey, TValue> dictionary, [JetBrains.Annotations.NotNull] ImmutableList<TKey> order)
        {
            _dictionary = dictionary ?? throw new ArgumentNullException(nameof(dictionary));
            _order = order ?? throw new ArgumentNullException(nameof(order));
        }
        
        public Boolean ContainsKey(TKey key)
        {
            return _dictionary.ContainsKey(key);
        }
        
        public Boolean Contains(KeyValuePair<TKey, TValue> pair)
        {
            return _dictionary.Contains(pair);
        }
        
        public Int32 IndexOf(TKey key)
        {
            return _order.IndexOf(key);
        }
        
        public Boolean TryGetKey(TKey equalKey, out TKey actualKey)
        {
            return _dictionary.TryGetKey(equalKey, out actualKey);
        }
        
        public Boolean TryGetValue(TKey key, out TValue value)
        {
            return _dictionary.TryGetValue(key, out value);
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

        public IImmutableIndexDictionary<TKey, TValue> RemoveRange([JetBrains.Annotations.NotNull] IEnumerable<TKey> keys)
        {
            if (keys is null)
            {
                throw new ArgumentNullException(nameof(keys));
            }

            ICollection<TKey> collection = keys as ICollection<TKey> ?? keys.ToArray();
            
            return new ImmutableIndexDictionary<TKey, TValue>(_dictionary.RemoveRange(collection), _order.RemoveRange(collection));
        }

        public IImmutableIndexDictionary<TKey, TValue> Remove(TKey key)
        {
            return _dictionary.ContainsKey(key) ? new ImmutableIndexDictionary<TKey, TValue>(_dictionary.Remove(key), _order.Remove(key)) : this;
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
            return new ImmutableIndexDictionary<TKey, TValue>(_dictionary.Clear(), _order.Clear());
        }

        public IImmutableIndexDictionary<TKey, TValue> Add([JetBrains.Annotations.NotNull] TKey key, TValue value)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return new ImmutableIndexDictionary<TKey, TValue>(_dictionary.Add(key, value), _order.Add(key));
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
            if (index < 0 || index >= _order.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }
            
            return new ImmutableIndexDictionary<TKey, TValue>(_dictionary.Add(key, value), _order.Insert(0, key));
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
            throw new NotImplementedException();
        }

        public IImmutableIndexDictionary<TKey, TValue> Reverse(Int32 index, Int32 count)
        {
            throw new NotImplementedException();
        }

        public IImmutableIndexDictionary<TKey, TValue> Sort()
        {
            throw new NotImplementedException();
        }

        public IImmutableIndexDictionary<TKey, TValue> Sort(Comparison<TKey> comparison)
        {
            throw new NotImplementedException();
        }

        public IImmutableIndexDictionary<TKey, TValue> Sort(IComparer<TKey>? comparer)
        {
            throw new NotImplementedException();
        }

        public IImmutableIndexDictionary<TKey, TValue> Sort(Int32 index, Int32 count, IComparer<TKey>? comparer)
        {
            throw new NotImplementedException();
        }

        IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.Clear()
        {
            throw new NotImplementedException();
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
                return _dictionary[key];
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