// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using JetBrains.Annotations;
using NetExtender.Types.Immutable.Maps.Interfaces;
using NetExtender.Utils.Types;

namespace NetExtender.Types.Immutable.Maps
{
    public class ImmutableMap<TKey, TValue> : IImmutableMap<TKey, TValue>
    {
        protected IImmutableDictionary<TKey, TValue> Base { get; }
        protected IImmutableDictionary<TValue, TKey> Reversed { get; }

        public Int32 Count
        {
            get
            {
                return Base.Count;
            }
        }
        
        public IEnumerable<TKey> Keys
        {
            get
            {
                return Base.Keys;
            }
        }

        public IEnumerable<TValue> Values
        {
            get
            {
                return Reversed.Keys;
            }
        }

        protected ImmutableMap([NotNull] IImmutableDictionary<TKey, TValue> @base, [NotNull] IImmutableDictionary<TValue, TKey> reversed)
        {
            Base = @base ?? throw new ArgumentNullException(nameof(@base));
            Reversed = reversed ?? throw new ArgumentNullException(nameof(reversed));
        }

        public Boolean ContainsKey(TKey key)
        {
            return Base.ContainsKey(key);
        }

        public Boolean ContainsValue(TValue key)
        {
            return Reversed.ContainsKey(key);
        }

        public Boolean ContainsByValue(TValue key, TKey value)
        {
            return Reversed.Contains(key, value);
        }

        public Boolean ContainsByValue(KeyValuePair<TValue, TKey> item)
        {
            return Reversed.Contains(item);
        }
        
        public Boolean Contains(KeyValuePair<TKey, TValue> pair)
        {
            return Base.Contains(pair);
        }

        public Boolean TryGetValue(TKey key, out TValue value)
        {
            return Base.TryGetValue(key, out value);
        }
        
        public Boolean TryGetKey(TKey equalKey, out TKey actualKey)
        {
            return Base.TryGetKey(equalKey, out actualKey);
        }
        
        public Boolean TryGetValue(TValue equalValue, out TValue actualValue)
        {
            return Reversed.TryGetKey(equalValue, out actualValue);
        }
        
        public Boolean TryGetKey(TValue key, out TKey value)
        {
            return Reversed.TryGetValue(key, out value);
        }

        public IImmutableMap<TKey, TValue> Add(TKey key, TValue value)
        {
            return new ImmutableMap<TKey, TValue>(Base.Add(key, value), Reversed.Add(value, key));
        }
        
        public IImmutableMap<TKey, TValue> AddRange([NotNull] IEnumerable<KeyValuePair<TKey, TValue>> pairs)
        {
            if (pairs is null)
            {
                throw new ArgumentNullException(nameof(pairs));
            }

            pairs = pairs.Materialize();
            
            return new ImmutableMap<TKey, TValue>(Base.AddRange(pairs), Reversed.AddRange(pairs.ReversePairs()));
        }
        
        public IImmutableMap<TKey, TValue> AddByValue(TValue key, TKey value)
        {
            return Add(value, key);
        }

        public IImmutableMap<TKey, TValue> AddByValue(KeyValuePair<TValue, TKey> item)
        {
            return Add(item.Value, item.Key);
        }

        public IImmutableMap<TKey, TValue> Remove(TKey key)
        {
            return Base.ContainsKey(key) ? new ImmutableMap<TKey, TValue>(Base.Remove(key), Reversed.Remove(Base[key])) : this;
        }
        
        public IImmutableMap<TKey, TValue> Remove(TKey key, TValue value)
        {
            return new ImmutableMap<TKey, TValue>(Base.Remove(key), Reversed.Remove(value));
        }
        
        public IImmutableMap<TKey, TValue> Remove(KeyValuePair<TKey, TValue> item)
        {
            return Remove(item.Key, item.Value);
        }
        
        public IImmutableMap<TKey, TValue> RemoveByValue(TValue key)
        {
            return Reversed.ContainsKey(key) ? new ImmutableMap<TKey, TValue>(Base.Remove(Reversed[key]), Reversed.Remove(key)) : this;
        }

        public IImmutableMap<TKey, TValue> RemoveByValue(TValue key, TKey value)
        {
            return Remove(value, key);
        }

        public IImmutableMap<TKey, TValue> RemoveByValue(KeyValuePair<TValue, TKey> item)
        {
            return Remove(item.Value, item.Key);
        }
        
        public IImmutableMap<TKey, TValue> RemoveRange([NotNull] IEnumerable<TKey> keys)
        {
            if (keys is null)
            {
                throw new ArgumentNullException(nameof(keys));
            }

            keys = keys.Materialize();
            
            return new ImmutableMap<TKey, TValue>(Base.RemoveRange(keys),
                Reversed.RemoveRange(keys.Where(key => key is not null && Base.ContainsKey(key)).Select(key => Base[key])));
        }

        public IImmutableMap<TKey, TValue> SetItem(TKey key, TValue value)
        {
            return new ImmutableMap<TKey, TValue>(Base.SetItem(key, value), Reversed.SetItem(value, key));
        }

        public IImmutableMap<TKey, TValue> SetItems([NotNull] IEnumerable<KeyValuePair<TKey, TValue>> items)
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            items = items.Materialize();
            
            return new ImmutableMap<TKey, TValue>(Base.SetItems(items), Reversed.SetItems(items.ReversePairs()));
        }

        public IImmutableMap<TKey, TValue> Clear()
        {
            return new ImmutableMap<TKey, TValue>(Base.Clear(), Reversed.Clear());
        }

        public TKey this[TValue key]
        {
            get
            {
                return Reversed[key];
            }
        }
        
        IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.Add(TKey key, TValue value)
        {
            return Add(key, value);
        }

        IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.AddRange(IEnumerable<KeyValuePair<TKey, TValue>> pairs)
        {
            return AddRange(pairs);
        }

        IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.Remove(TKey key)
        {
            return Remove(key);
        }

        IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.RemoveRange(IEnumerable<TKey> keys)
        {
            return RemoveRange(keys);
        }

        IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.SetItem(TKey key, TValue value)
        {
            return SetItem(key, value);
        }

        IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.SetItems(IEnumerable<KeyValuePair<TKey, TValue>> items)
        {
            return SetItems(items);
        }
        
        IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.Clear()
        {
            return Clear();
        }
        
        public TValue this[TKey key]
        {
            get
            {
                return Base[key];
            }
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return Base.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        
        public IEnumerator<KeyValuePair<TValue, TKey>> GetValuesEnumerator()
        {
            return Reversed.GetEnumerator();
        }
    }
}