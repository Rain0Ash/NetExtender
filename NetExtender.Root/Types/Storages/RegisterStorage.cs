// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using NetExtender.Types.Storages.Interfaces;

namespace NetExtender.Types.Storages
{
    public class RegisterStorage<TKey> : RegisterStorage<TKey, UInt64>, IRegisterStorage<TKey> where TKey : class
    {
        public override UInt64 Register(TKey key)
        {
            return Storage.GetOrAdd(key, () => Interlocked.Increment(ref Next));
        }
    }

    public abstract class RegisterStorage<TKey, TValue> : IRegisterStorage<TKey, TValue> where TKey : class where TValue : struct, IEquatable<TValue>
    {
        protected IMemoryStorage<TKey, TValue> Storage { get; } = new MemoryStorage<TKey, TValue>();

        public TKey? Current
        {
            get
            {
                return Storage.Current;
            }
        }

        public TValue Value
        {
            get
            {
                return Storage.Value;
            }
        }

        private TValue _next;
        protected ref TValue Next
        {
            get
            {
                return ref _next;
            }
        }

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
            return Storage.TryGetKey(value, out key);
        }

        public abstract TValue Register(TKey key);

        public Boolean Remove(TKey key)
        {
            return Storage.Remove(key);
        }

        public void Clear()
        {
            Storage.Clear();
            Next = default;
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
        }
    }
}