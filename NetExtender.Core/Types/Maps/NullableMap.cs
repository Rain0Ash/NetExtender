// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using NetExtender.Types.Collections;
using NetExtender.Types.Maps.Interfaces;
using NetExtender.Types.Monads;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Maps
{
    public class NullableMap<TKey, TValue> : Map<NullMaybe<TKey>, NullMaybe<TValue>>, IMap<TKey, TValue>, IReadOnlyMap<TKey, TValue>
    {
        private ICollection<TKey>? _keys { get; set; }
        public new ICollection<TKey> Keys
        {
            get
            {
                return _keys ??= new SelectorCollectionWrapper<NullMaybe<TKey>, TKey>(base.Keys, nullable => nullable);
            }
        }
        
        private ICollection<TValue>? _values { get; set; }
        public new ICollection<TValue> Values
        {
            get
            {
                return _values ??= new SelectorCollectionWrapper<NullMaybe<TValue>, TValue>(base.Values, nullable => nullable);
            }
        }
        
        IEnumerable<TKey> IReadOnlyMap<TKey, TValue>.Keys
        {
            get
            {
                return Keys;
            }
        }

        IEnumerable<TValue> IReadOnlyMap<TKey, TValue>.Values
        {
            get
            {
                return Values;
            }
        }
        
        public new IEqualityComparer<TKey> Comparer
        {
            get
            {
                return KeyComparer;
            }
        }

        private IEqualityComparer<TKey>? _keycomparer { get; set; }
        public new IEqualityComparer<TKey> KeyComparer
        {
            get
            {
                return _keycomparer ??= base.KeyComparer.ToEqualityComparer();
            }
        }

        private IEqualityComparer<TValue>? _valuecomparer { get; set; }
        public new IEqualityComparer<TValue> ValueComparer
        {
            get
            {
                return _valuecomparer ??= base.ValueComparer.ToEqualityComparer();
            }
        }

        public NullableMap()
        {
        }

        public NullableMap(IDictionary<NullMaybe<TKey>, NullMaybe<TValue>> dictionary)
            : base(dictionary)
        {
        }

        public NullableMap(IDictionary<NullMaybe<TKey>, NullMaybe<TValue>> dictionary, IEqualityComparer<NullMaybe<TKey>>? comparer)
            : base(dictionary, comparer)
        {
        }

        public NullableMap(IDictionary<NullMaybe<TKey>, NullMaybe<TValue>> dictionary, IEqualityComparer<NullMaybe<TKey>>? keyComparer, IEqualityComparer<NullMaybe<TValue>>? valueComparer)
            : base(dictionary, keyComparer, valueComparer)
        {
        }

        public NullableMap(IEqualityComparer<NullMaybe<TKey>>? comparer)
            : base(comparer)
        {
        }
        
        public NullableMap(IEqualityComparer<NullMaybe<TKey>>? keyComparer, IEqualityComparer<NullMaybe<TValue>>? valueComparer)
            : base(keyComparer, valueComparer)
        {
        }

        public NullableMap(IEnumerable<KeyValuePair<NullMaybe<TKey>, NullMaybe<TValue>>> source)
            : base(source)
        {
        }

        public NullableMap(IEnumerable<KeyValuePair<NullMaybe<TValue>, NullMaybe<TKey>>> source)
            : base(source)
        {
        }

        public NullableMap(IEnumerable<KeyValuePair<NullMaybe<TKey>, NullMaybe<TValue>>> source, IEqualityComparer<NullMaybe<TKey>>? comparer)
            : base(source, comparer)
        {
        }
        
        public NullableMap(IEnumerable<KeyValuePair<NullMaybe<TValue>, NullMaybe<TKey>>> source, IEqualityComparer<NullMaybe<TKey>>? comparer)
            : base(source, comparer)
        {
        }

        public NullableMap(IEnumerable<KeyValuePair<NullMaybe<TKey>, NullMaybe<TValue>>> source, IEqualityComparer<NullMaybe<TKey>>? keyComparer, IEqualityComparer<NullMaybe<TValue>>? valueComparer)
            : base(source, keyComparer, valueComparer)
        {
        }

        public NullableMap(IEnumerable<KeyValuePair<NullMaybe<TValue>, NullMaybe<TKey>>> source, IEqualityComparer<NullMaybe<TKey>>? keyComparer, IEqualityComparer<NullMaybe<TValue>>? valueComparer)
            : base(source, keyComparer, valueComparer)
        {
        }

        public NullableMap(Int32 capacity)
            : base(capacity)
        {
        }

        public NullableMap(Int32 capacity, IEqualityComparer<NullMaybe<TKey>>? keyComparer, IEqualityComparer<NullMaybe<TValue>>? valueComparer)
            : base(capacity, keyComparer, valueComparer)
        {
        }

        public Boolean Contains(KeyValuePair<TKey, TValue> item)
        {
            return base.Contains(new KeyValuePair<NullMaybe<TKey>, NullMaybe<TValue>>(item.Key, item.Value));
        }
        
        public Boolean ContainsKey(TKey key)
        {
            return base.ContainsKey(key);
        }

        public Boolean ContainsValue(TValue key)
        {
            return base.ContainsValue(key);
        }

        public Boolean ContainsByValue(TValue key, TKey value)
        {
            return base.ContainsByValue(key, value);
        }

        public Boolean ContainsByValue(KeyValuePair<TValue, TKey> item)
        {
            return base.ContainsByValue(new KeyValuePair<NullMaybe<TValue>, NullMaybe<TKey>>(item.Key, item.Value));
        }

        public TValue GetValue(TKey key)
        {
            return base.GetValue(key);
        }

        public TKey GetKey(TValue value)
        {
            return base.GetKey(value);
        }

        public Boolean TryGetValue(TKey key, out TValue value)
        {
            Boolean successful = base.TryGetValue(key, out NullMaybe<TValue> result);
            value = result;
            return successful;
        }

        public Boolean TryGetKey(TValue key, out TKey value)
        {
            Boolean successful = base.TryGetKey(key, out NullMaybe<TKey> result);
            value = result;
            return successful;
        }
        
        public void Add(KeyValuePair<TKey, TValue> item)
        {
            base.Add(new KeyValuePair<NullMaybe<TKey>, NullMaybe<TValue>>(item.Key, item.Value));
        }

        public void Add(TKey key, TValue value)
        {
            base.Add(key, value);
        }

        public void AddByValue(TValue key, TKey value)
        {
            base.AddByValue(key, value);
        }

        public void AddByValue(KeyValuePair<TValue, TKey> item)
        {
            base.AddByValue(new KeyValuePair<NullMaybe<TValue>, NullMaybe<TKey>>(item.Key, item.Value));
        }

        public Boolean TryAdd(TKey key, TValue value)
        {
            return base.TryAdd(key, value);
        }

        public Boolean TryAdd(KeyValuePair<TKey, TValue> item)
        {
            return base.TryAdd(new KeyValuePair<NullMaybe<TKey>, NullMaybe<TValue>>(item.Key, item.Value));
        }

        public Boolean TryAddByValue(TValue key, TKey value)
        {
            return base.TryAddByValue(key, value);
        }

        public Boolean TryAddByValue(KeyValuePair<TValue, TKey> item)
        {
            return base.TryAddByValue(new KeyValuePair<NullMaybe<TValue>, NullMaybe<TKey>>(item.Key, item.Value));
        }
        
        public Boolean Remove(KeyValuePair<TKey, TValue> item)
        {
            return base.Remove(new KeyValuePair<NullMaybe<TKey>, NullMaybe<TValue>>(item.Key, item.Value));
        }

        public Boolean Remove(TKey key)
        {
            return base.Remove(key);
        }

        public Boolean Remove(TKey key, TValue value)
        {
            return base.Remove(key, value);
        }

        public Boolean RemoveByValue(TValue key)
        {
            return base.RemoveByValue(key);
        }

        public Boolean RemoveByValue(TValue key, TKey value)
        {
            return base.RemoveByValue(key, value);
        }

        public Boolean RemoveByValue(TValue key, out TKey value)
        {
            Boolean successful = base.RemoveByValue(key, out NullMaybe<TKey> result);
            value = result;
            return successful;
        }

        public Boolean RemoveByValue(KeyValuePair<TValue, TKey> item)
        {
            return base.RemoveByValue(new KeyValuePair<NullMaybe<TValue>, NullMaybe<TKey>>(item.Key, item.Value));
        }
        
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, Int32 arrayIndex)
        {
            CollectionUtilities.CopyTo(this, array, arrayIndex);
        }

        public void CopyTo(KeyValuePair<TValue, TKey>[] array, Int32 arrayIndex)
        {
            this.ReversePairs<TKey, TValue>().CopyTo(array, arrayIndex);
        }

        public new IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            foreach ((NullMaybe<TKey> key, NullMaybe<TValue> value) in (IEnumerable<KeyValuePair<NullMaybe<TKey>, NullMaybe<TValue>>>) this)
            {
                yield return new KeyValuePair<TKey, TValue>(key, value);
            }
        }
        
        public new IEnumerator<KeyValuePair<TValue, TKey>> GetValuesEnumerator()
        {
            foreach ((NullMaybe<TValue> key, NullMaybe<TKey> value) in (IEnumerable<KeyValuePair<NullMaybe<TValue>, NullMaybe<TKey>>>) this)
            {
                yield return new KeyValuePair<TValue, TKey>(key, value);
            }
        }

        public TValue this[TKey key]
        {
            get
            {
                return base[key];
            }
            set
            {
                base[key] = value;
            }
        }

        public TKey this[TValue key]
        {
            get
            {
                return base[key];
            }
            set
            {
                base[key] = value;
            }
        }
    }
}