// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using NetExtender.Types.Stores.Interfaces;

namespace NetExtender.Types.Stores
{
    public class RegisterStore<TKey> : RegisterStore<TKey, UInt64>, IRegisterStore<TKey> where TKey : class
    {
        public override UInt64 Register(TKey key)
        {
            return Store.GetOrAdd(key, () => Interlocked.Increment(ref Next));
        }
    }
    
    public abstract class RegisterStore<TKey, TValue> : IRegisterStore<TKey, TValue> where TKey : class where TValue : struct, IEquatable<TValue>
    {
        protected IMemoryStore<TKey, TValue> Store { get; } = new MemoryStore<TKey, TValue>();

        public TKey? Current
        {
            get
            {
                return Store.Current;
            }
        }

        public TValue Value
        {
            get
            {
                return Store.Value;
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
            return Store.Contains(key);
        }

        public Boolean TryGetValue(TKey key, out TValue value)
        {
            return Store.TryGetValue(key, out value);
        }

        public Boolean TryGetKey(TValue value, [MaybeNullWhen(false)] out TKey key)
        {
            return Store.TryGetKey(value, out key);
        }

        public abstract TValue Register(TKey key);

        public Boolean Remove(TKey key)
        {
            return Store.Remove(key);
        }

        public void Clear()
        {
            Store.Clear();
            Next = default;
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
        }
    }
}