// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using NetExtender.Types.Storages.Interfaces;

namespace NetExtender.Types.Storages
{
    public class MemoryStorage<TKey, TValue> : IMemoryStorage<TKey, TValue>, IReadOnlyMemoryStorage<TKey, TValue> where TKey : class where TValue : struct, IEquatable<TValue>
    {
        protected WeakStorageGeneric<TKey, TValue> Storage { get; } = new WeakStorageGeneric<TKey, TValue>();

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
            return Storage.Contains(key);
        }

        public Boolean TryGetValue(TKey key, out TValue value)
        {
            return Storage.TryGetValue(key, out value);
        }

        public Boolean TryGetKey(TValue value, [MaybeNullWhen(false)] out TKey key)
        {
            foreach (KeyValuePair<TKey, TValue> item in Storage)
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
            Storage.Add(key, value);
            Value = value;
        }

        public void AddOrUpdate(TKey key, TValue value)
        {
            Storage.AddOrUpdate(key, value);
            Value = value;
        }

        public TValue GetOrAdd(TKey key, TValue value)
        {
            return Value = Storage.GetOrAdd(key, value);
        }

        public TValue GetOrAdd(TKey key, Func<TValue> factory)
        {
            return Value = Storage.GetOrAdd(key, factory);
        }

        public TValue GetOrAdd(TKey key, Func<TKey, TValue> factory)
        {
            return Value = Storage.GetOrAdd(key, factory);
        }

        public Boolean Remove(TKey key)
        {
            return Storage.Remove(key);
        }

        public void Clear()
        {
            Storage.Clear();
            Value = default;
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return Storage.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public TValue this[TKey key]
        {
            get
            {
                return Storage[key];
            }
            set
            {
                Storage[key] = value;
                Value = value;
            }
        }
    }
}