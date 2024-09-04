// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using NetExtender.Types.Dictionaries;
using NetExtender.Types.Dictionaries.Interfaces;

namespace NetExtender.Types.Concurrent.Dictionaries
{
    public class ConcurrentWeakDictionary<TKey, TValue> : IWeakDictionary<TKey, TValue>, IReadOnlyWeakDictionary<TKey, TValue> where TKey : class where TValue : class?
    {
        protected IWeakDictionary<TKey, TValue> Internal { get; } = new ConditionalWeakTableWrapper<TKey, TValue>();
        
        public Boolean Contains(TKey key)
        {
            lock (Internal)
            {
                return Internal.Contains(key);
            }
        }

        public Boolean TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
        {
            lock (Internal)
            {
                return Internal.TryGetValue(key, out value);
            }
        }

        public void Add(TKey key, TValue value)
        {
            lock (Internal)
            {
                Internal.Add(key, value);
            }
        }

        public void AddOrUpdate(TKey key, TValue value)
        {
            lock (Internal)
            {
                Internal.AddOrUpdate(key, value);
            }
        }

        public TValue GetOrAdd(TKey key, TValue value)
        {
            lock (Internal)
            {
                return Internal.GetOrAdd(key, value);
            }
        }

        public TValue GetOrAdd(TKey key, Func<TValue> factory)
        {
            lock (Internal)
            {
                return Internal.GetOrAdd(key, factory);
            }
        }

        public TValue GetOrAdd(TKey key, Func<TKey, TValue> factory)
        {
            lock (Internal)
            {
                return Internal.GetOrAdd(key, factory);
            }
        }

        public Boolean Remove(TKey key)
        {
            lock (Internal)
            {
                return Internal.Remove(key);
            }
        }

        public void Clear()
        {
            lock (Internal)
            {
                Internal.Clear();
            }
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return Internal.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) Internal).GetEnumerator();
        }

        public TValue this[TKey key]
        {
            get
            {
                lock (Internal)
                {
                    return Internal[key];
                }
            }
            set
            {
                lock (Internal)
                {
                    Internal[key] = value;
                }
            }
        }
    }
}