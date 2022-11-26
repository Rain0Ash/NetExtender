// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using NetExtender.Types.Concurrent.Dictionaries;
using NetExtender.Types.Dictionaries.Interfaces;
using NetExtender.Types.Monads;
using NetExtender.Types.Stores.Interfaces;

namespace NetExtender.Types.Stores
{
    public class WeakStoreGeneric<TKey, TValue> : IStore<TKey, TValue>, IReadOnlyStore<TKey, TValue> where TKey : class
    {
        private IWeakDictionary<TKey, Box<TValue>> Internal { get; }

        public WeakStoreGeneric()
        {
            Internal = new ConcurrentWeakDictionary<TKey, Box<TValue>>();
        }

        public Boolean Contains(TKey key)
        {
            return Internal.Contains(key);
        }

        public Boolean TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
        {
            if (Internal.TryGetValue(key, out Box<TValue>? result))
            {
                value = result;
                return true;
            }

            value = default;
            return false;
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
            return Internal.GetOrAdd(key, _ => value);
        }

        public TValue GetOrAdd(TKey key, Func<TValue> factory)
        {
            return Internal.GetOrAdd(key, () => factory());
        }

        public TValue GetOrAdd(TKey key, Func<TKey, TValue> factory)
        {
            return Internal.GetOrAdd(key, callback => factory(callback));
        }

        public Boolean Remove(TKey key)
        {
            return Internal.Remove(key);
        }

        public void Clear()
        {
            Internal.Clear();
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return new Enumerator(Internal.GetEnumerator());
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public TValue this[TKey key]
        {
            get
            {
                return Internal[key];
            }
            set
            {
                Internal.AddOrUpdate(key, value);
            }
        }

        private class Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>
        {
            private IEnumerator<KeyValuePair<TKey, Box<TValue>>> Internal { get; }

            public KeyValuePair<TKey, TValue> Current
            {
                get
                {
                    (TKey key, Box<TValue> value) = Internal.Current;
                    return new KeyValuePair<TKey, TValue>(key, value);
                }
            }

            Object IEnumerator.Current
            {
                get
                {
                    return Current;
                }
            }

            public Enumerator(IEnumerator<KeyValuePair<TKey, Box<TValue>>> @internal)
            {
                Internal = @internal ?? throw new ArgumentNullException(nameof(@internal));
            }

            public Boolean MoveNext()
            {
                return Internal.MoveNext();
            }

            public void Reset()
            {
                Internal.Reset();
            }

            public void Dispose()
            {
                Internal.Dispose();
            }
        }
    }
}