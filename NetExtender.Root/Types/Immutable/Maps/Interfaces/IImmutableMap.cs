// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using NetExtender.Types.Maps.Interfaces;

namespace NetExtender.Types.Immutable.Maps.Interfaces
{
    public interface IImmutableMap<TKey, TValue> : IReadOnlyMap<TKey, TValue>
    {
        public Boolean TryGetValue(TValue equalValue, out TValue actualValue);

        public IImmutableMap<TKey, TValue> AddByValue(TValue key, TKey value);
        public IImmutableMap<TKey, TValue> AddByValue(KeyValuePair<TValue, TKey> item);

        public IImmutableMap<TKey, TValue> Remove(TKey key, TValue value);
        public IImmutableMap<TKey, TValue> Remove(KeyValuePair<TKey, TValue> item);

        public IImmutableMap<TKey, TValue> RemoveByValue(TValue key);

        public IImmutableMap<TKey, TValue> RemoveByValue(TValue key, TKey value);

        public IImmutableMap<TKey, TValue> RemoveByValue(KeyValuePair<TValue, TKey> item);

        /// <inheritdoc cref="IImmutableDictionary{TKey,TValue}.Add"/>
        public IImmutableMap<TKey, TValue> Add(TKey key, TValue value);

        /// <inheritdoc cref="IImmutableDictionary{TKey,TValue}.AddRange"/>
        public IImmutableMap<TKey, TValue> AddRange(IEnumerable<KeyValuePair<TKey, TValue>> pairs);

        /// <inheritdoc cref="IImmutableDictionary{TKey,TValue}.SetItem"/>
        public IImmutableMap<TKey, TValue> SetItem(TKey key, TValue value);

        /// <inheritdoc cref="IImmutableDictionary{TKey,TValue}.SetItems"/>
        public IImmutableMap<TKey, TValue> SetItems(IEnumerable<KeyValuePair<TKey, TValue>> items);

        /// <inheritdoc cref="IImmutableDictionary{TKey,TValue}.Remove"/>
        public IImmutableMap<TKey, TValue> Remove(TKey key);

        /// <inheritdoc cref="IImmutableDictionary{TKey,TValue}.RemoveRange"/>
        public IImmutableMap<TKey, TValue> RemoveRange(IEnumerable<TKey> keys);

        /// <inheritdoc cref="IImmutableDictionary{TKey,TValue}.Clear"/>
        public IImmutableMap<TKey, TValue> Clear();
    }
}