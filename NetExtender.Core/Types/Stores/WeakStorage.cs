// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using NetExtender.Types.Concurrent.Dictionaries;
using NetExtender.Types.Dictionaries.Interfaces;
using NetExtender.Types.Sets.Interfaces;
using NetExtender.Types.Storages.Interfaces;

namespace NetExtender.Types.Storages
{
    public class WeakStorage<T> : IStorage<T>, IReadOnlyStorage<T> where T : class
    {
        protected IWeakSet<T> Internal { get; } = new ConcurrentWeakSet<T>();
        
        public Boolean Contains(T item)
        {
            return Internal.Contains(item);
        }

        public Boolean Add(T item)
        {
            return Internal.Add(item);
        }

        public Boolean Remove(T item)
        {
            return Internal.Remove(item);
        }

        public void Clear()
        {
            Internal.Clear();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Internal.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) Internal).GetEnumerator();
        }
    }
    
    public class WeakStorage<TKey, TValue> : IStorage<TKey, TValue>, IReadOnlyStorage<TKey, TValue> where TKey : class where TValue : class?
    {
        protected IWeakDictionary<TKey, TValue> Internal { get; } = new ConcurrentWeakDictionary<TKey, TValue>();
        
        public Boolean Contains(TKey key)
        {
            return Internal.Contains(key);
        }

        public Boolean TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
        {
            return Internal.TryGetValue(key, out value);
        }

        public void Add(TKey key, TValue value)
        {
            Internal.Add(key, value);
        }

        public void AddOrUpdate(TKey key, TValue value)
        {
            Internal.AddOrUpdate(key, value);
        }

        public TValue GetOrAdd(TKey key, TValue value)
        {
            return Internal.GetOrAdd(key, value);
        }

        public TValue GetOrAdd(TKey key, Func<TValue> factory)
        {
            return Internal.GetOrAdd(key, factory);
        }

        public TValue GetOrAdd(TKey key, Func<TKey, TValue> factory)
        {
            return Internal.GetOrAdd(key, factory);
        }

        public Boolean Remove(TKey key)
        {
            return Internal.Remove(key);
        }

        public void Clear()
        {
            Internal.Clear();
        }

        public TValue this[TKey key]
        {
            get
            {
                return Internal[key];
            }
            set
            {
                Internal[key] = value;
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
    }
}