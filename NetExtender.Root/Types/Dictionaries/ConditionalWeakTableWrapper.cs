// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using NetExtender.Types.Dictionaries.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Dictionaries
{
    public sealed class ConditionalWeakTableWrapper<TKey, TValue> : IWeakDictionary<TKey, TValue>, IReadOnlyWeakDictionary<TKey, TValue> where TKey : class where TValue : class?
    {
        private ConditionalWeakTable<TKey, TValue> Internal { get; } = new ConditionalWeakTable<TKey, TValue>();

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

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return ((IEnumerable<KeyValuePair<TKey, TValue>>) Internal).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public TValue this[TKey key]
        {
            get
            {
                return Internal.TryGetValue(key, out TValue? result) ? result : throw new KeyNotFoundException();
            }
            set
            {
                Internal.AddOrUpdate(key, value);
            }
        }
    }
}