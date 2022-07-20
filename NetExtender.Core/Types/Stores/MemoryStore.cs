// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using NetExtender.Types.Stores.Interfaces;

namespace NetExtender.Types.Stores
{
    public class MemoryStore<TKey, TValue> : IMemoryStore<TKey, TValue>, IReadOnlyMemoryStore<TKey, TValue> where TKey : class where TValue : struct, IEquatable<TValue>
    {
        protected WeakStoreGeneric<TKey, TValue> Store { get; } = new WeakStoreGeneric<TKey, TValue>();

        public TKey? Current
        {
            get
            {
                return TryGetKey(Value, out TKey? key) ? key : default;
            }
        }

        public TValue Value { get; private set; }

        public Boolean Contains(TKey key)
        {
            return Store.Contains(key);
        }

        public Boolean TryGetValue(TKey key, out TValue value)
        {
            return Store.TryGetValue(key, out value);
        }

        public Boolean TryGetKey(TValue value, [MaybeNullWhen(false)] out TKey key)
        {
            foreach (KeyValuePair<TKey, TValue> item in Store)
            {
                if (!EqualityComparer<TValue>.Default.Equals(value, item.Value))
                {
                    continue;
                }

                key = item.Key;
                return true;
            }
            
            key = default;
            return false;
        }

        public void Add(TKey key, TValue value)
        {
            Store.Add(key, value);
            Value = value;
        }

        public void AddOrUpdate(TKey key, TValue value)
        {
            Store.AddOrUpdate(key, value);
            Value = value;
        }

        public TValue GetOrAdd(TKey key, TValue value)
        {
            return Value = Store.GetOrAdd(key, value);
        }

        public TValue GetOrAdd(TKey key, Func<TValue> factory)
        {
            return Value = Store.GetOrAdd(key, factory);
        }

        public TValue GetOrAdd(TKey key, Func<TKey, TValue> factory)
        {
            return Value = Store.GetOrAdd(key, factory);
        }

        public Boolean Remove(TKey key)
        {
            return Store.Remove(key);
        }

        public void Clear()
        {
            Store.Clear();
            Value = default;
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return Store.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        
        public TValue this[TKey key]
        {
            get
            {
                return Store[key];
            }
            set
            {
                Store[key] = value;
                Value = value;
            }
        }
    }
}