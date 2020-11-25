// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Serialization;
using NetExtender.Utils.Types;
using NetExtender.Types.Dictionaries.Interfaces;

namespace NetExtender.Types.Dictionaries
{
    [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
    public class IndexDictionary<TKey, TValue> : Dictionary<TKey, TValue>, IIndexDictionary<TKey, TValue>, IReadOnlyIndexDictionary<TKey, TValue>
    {
        private readonly List<TKey> _order;

        public IReadOnlyList<TKey> OrderedKeys
        {
            get
            {
                return _order;
            }
        }

        public IndexDictionary()
        {
            _order = new List<TKey>();
        }

        public IndexDictionary(IDictionary<TKey, TValue> dictionary)
            : base(dictionary)
        {
            _order = new List<TKey>(dictionary.Keys);
        }

        public IndexDictionary(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey>? comparer)
            : base(dictionary, comparer)
        {
            _order = new List<TKey>(dictionary.Keys);
        }

        public IndexDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection)
            : base(collection)
        {
            _order = new List<TKey>(collection.Select(pair => pair.Key));
        }

        public IndexDictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection, IEqualityComparer<TKey>? comparer)
            : base(collection, comparer)
        {
            _order = new List<TKey>(collection.Select(pair => pair.Key));
        }

        public IndexDictionary(IEqualityComparer<TKey>? comparer)
            : base(comparer)
        {
            _order = new List<TKey>();
        }

        public IndexDictionary(Int32 capacity)
            : base(capacity)
        {
            _order = new List<TKey>(capacity);
        }

        public IndexDictionary(Int32 capacity, IEqualityComparer<TKey>? comparer)
            : base(capacity, comparer)
        {
            _order = new List<TKey>(capacity);
        }

        protected IndexDictionary(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            _order = new List<TKey>();
        }

        public TValue GetValueByIndex(Int32 index)
        {
            return this[_order[index]];
        }

        public KeyValuePair<TKey, TValue> GetKeyValuePairByIndex(Int32 index)
        {
            return this.GetPair(_order[index]);
        }

        public Boolean TryGetKeyValuePairByIndex(Int32 index, out KeyValuePair<TKey, TValue> pair)
        {
            return this.TryGetPair(_order[index], out pair);
        }

        public Int32 IndexOf(TKey key)
        {
            return _order.IndexOf(key);
        }

        public new void Add(TKey key, TValue value)
        {
            base.Add(key, value);
            _order.Add(key);
        }

        public new Boolean TryAdd(TKey key, TValue value)
        {
            if (!base.TryAdd(key, value))
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
            if (ContainsKey(key))
            {
                return;
            }

            base.Add(key, value);
            _order.Insert(index, key);
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

            if (ContainsKey(key) || index < 0 || index >= _order.Count)
            {
                return false;
            }

            Insert(index, key, value);
            return true;
        }

        public void Swap(Int32 index1, Int32 index2)
        {
            _order.Swap(index1, index2);
        }

        public new Boolean Remove(TKey key)
        {
            Boolean removed = base.Remove(key);
            _order.Remove(key);
            return removed;
        }

        public new Boolean Remove(TKey key, out TValue value)
        {
            Boolean removed = base.Remove(key, out value);
            _order.Remove(key);
            return removed;
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

        public new void Clear()
        {
            base.Clear();
            _order.Clear();
        }

        public new TValue this[TKey key]
        {
            get
            {
                return base[key];
            }
            set
            {
                if (!ContainsKey(key))
                {
                    _order.Add(key);
                }

                base[key] = value;
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