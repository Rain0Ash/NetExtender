// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using NetExtender.Utils.Types;
using NetExtender.Types.Maps.Interfaces;

namespace NetExtender.Types.Maps
{
    public class IndexMap<TKey, TValue> : Map<TKey, TValue>, IIndexMap<TKey, TValue>, IReadOnlyIndexMap<TKey, TValue> where TKey : notnull where TValue : notnull
    {
        private readonly List<TKey> _order;
        public IReadOnlyList<TKey> OrderedKeys
        {
            get
            {
                return _order;
            }
        }

        public IndexMap()
        {
            _order = new List<TKey>();
        }

        public IndexMap(IDictionary<TKey, TValue> dictionary)
            : base(dictionary ?? throw new ArgumentNullException(nameof(dictionary)))
        {
            _order = new List<TKey>(dictionary.Keys);
        }

        public IndexMap(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey>? keyComparer,
            IEqualityComparer<TValue>? valueComparer)
            : base(dictionary ?? throw new ArgumentNullException(nameof(dictionary)), keyComparer, valueComparer)
        {
            _order = new List<TKey>(dictionary.Keys);
        }

        public IndexMap(IEqualityComparer<TKey>? keyComparer, IEqualityComparer<TValue>? valueComparer)
            : base(keyComparer, valueComparer)
        {
            _order = new List<TKey>();
        }

        public IndexMap(IEnumerable<KeyValuePair<TKey, TValue>> collection)
            : base(collection = collection?.Materialize() ?? throw new ArgumentNullException(nameof(collection)))
        {
            _order = new List<TKey>(collection.Select(pair => pair.Key));
        }

        public IndexMap(IEnumerable<KeyValuePair<TKey, TValue>> collection, IEqualityComparer<TKey>? keyComparer,
            IEqualityComparer<TValue>? valueComparer)
            : base(collection = collection?.Materialize() ?? throw new ArgumentNullException(nameof(collection)), keyComparer, valueComparer)
        {
            _order = new List<TKey>(collection.Select(pair => pair.Key));
        }

        public IndexMap(Int32 capacity)
            : base(capacity)
        {
            _order = new List<TKey>(capacity);
        }

        public IndexMap(Int32 capacity, IEqualityComparer<TKey>? keyComparer, IEqualityComparer<TValue>? valueComparer)
            : base(capacity, keyComparer, valueComparer)
        {
            _order = new List<TKey>(capacity);
        }
        
        public TKey GetKeyByIndex(Int32 index)
        {
            return _order[index];
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
            return _order.IndexOf(key);
        }

        public Int32 IndexOfValue(TValue key)
        {
            return IndexOf(Reversed[key]);
        }

        public override void Add(TKey key, TValue value)
        {
            base.Add(key, value);
            _order.Add(key);
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
            
            if (index < 0 || index >= _order.Count)
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
            _order.Insert(index, key);
        }

        public void Insert(TValue key, TKey value)
        {
            Insert(0, key, value);
        }

        public void Insert(Int32 index, TValue key, TKey value)
        {
            Insert(index, value, key);
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
            
            if (index < 0 || index >= _order.Count)
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

        public Boolean TryInsert(TValue key, TKey value)
        {
            return TryInsert(0, key, value);
        }

        public Boolean TryInsert(Int32 index, TValue key, TKey value)
        {
            return TryInsert(index, value, key);
        }

        public void Swap(Int32 index1, Int32 index2)
        {
            _order.Swap(index1, index2);
        }

        public override Boolean Remove(TKey key, TValue value)
        {
            if (!base.Remove(key, value))
            {
                return false;
            }
            
            _order.Remove(key);
            return true;
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

        public override void Clear()
        {
            base.Clear();
            _order.Clear();
        }

        public override TValue this[TKey key]
        {
            get
            {
                return base[key];
            }
            set
            {
                Boolean contains = ContainsKey(key);

                base[key] = value;
                
                if (!contains)
                {
                    _order.Add(key);
                }
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
    }
}