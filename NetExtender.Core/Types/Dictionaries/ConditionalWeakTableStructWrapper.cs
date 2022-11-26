// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using NetExtender.Types.Dictionaries.Interfaces;
using NetExtender.Types.Monads;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Dictionaries
{
    public class ConditionalWeakTableStructWrapper<TKey, TValue> : IWeakDictionary<TKey, TValue>, IReadOnlyWeakDictionary<TKey, TValue> where TKey : class where TValue : struct
    {
        private ConditionalWeakTable<TKey, Box<TValue>> Internal { get; } = new ConditionalWeakTable<TKey, Box<TValue>>();

        public Boolean Contains(TKey key)
        {
            return Internal.Contains(key);
        }

        public Boolean TryGetValue(TKey key, out TValue value)
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
            return Internal.GetValue(key, _ => value);
        }

        public TValue GetOrAdd(TKey key, Func<TValue> factory)
        {
            return Internal.GetValue(key, _ => factory());
        }

        public TValue GetOrAdd(TKey key, Func<TKey, TValue> factory)
        {
            return Internal.GetValue(key, callback => factory(callback));
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
            return new Enumerator(((IEnumerable<KeyValuePair<TKey, Box<TValue>>>) Internal).GetEnumerator());
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public TValue this[TKey key]
        {
            get
            {
                return Internal.TryGetValue(key, out Box<TValue>? result) ? result : throw new KeyNotFoundException();
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