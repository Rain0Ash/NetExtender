// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;

namespace NetExtender.Types.Immutable.Maps.Interfaces
{
    public interface IImmutableMap<TKey, TValue> : IImmutableDictionary<TKey, TValue>
    {
        public Boolean ContainsValue(TValue key);
        public Boolean ContainsByValue(TValue key, TKey value);
        public Boolean ContainsByValue(KeyValuePair<TValue, TKey> item);
        public Boolean TryGetKey(TValue key, [MaybeNullWhen(false)] out TKey value);
        public Boolean TryGetValue(TValue equalValue, out TValue actualValue);

        public IEnumerator<KeyValuePair<TValue, TKey>> GetValuesEnumerator();
        
        public IImmutableMap<TKey, TValue> AddByValue(TValue key, TKey value);
        public IImmutableMap<TKey, TValue> AddByValue(KeyValuePair<TValue, TKey> item);

        public IImmutableMap<TKey, TValue> Remove(TKey key, TValue value);
        public IImmutableMap<TKey, TValue> Remove(KeyValuePair<TKey, TValue> item);
        
        public IImmutableMap<TKey, TValue> RemoveByValue(TValue key);

        public IImmutableMap<TKey, TValue> RemoveByValue(TValue key, TKey value);
        
        public IImmutableMap<TKey, TValue> RemoveByValue(KeyValuePair<TValue, TKey> item);

        public TKey this[TValue key] { get; }

        /// <inheritdoc cref="IImmutableDictionary{TKey,TValue}.Clear"/>
        public new IImmutableMap<TKey, TValue> Clear();

        /// <inheritdoc cref="IImmutableDictionary{TKey,TValue}.Add"/>
        public new IImmutableMap<TKey, TValue> Add(TKey key, TValue value);

        /// <inheritdoc cref="IImmutableDictionary{TKey,TValue}.AddRange"/>
        public new IImmutableMap<TKey, TValue> AddRange(IEnumerable<KeyValuePair<TKey, TValue>> pairs);

        /// <inheritdoc cref="IImmutableDictionary{TKey,TValue}.SetItem"/>
        public new IImmutableMap<TKey, TValue> SetItem(TKey key, TValue value);

        /// <inheritdoc cref="IImmutableDictionary{TKey,TValue}.SetItems"/>
        public new IImmutableMap<TKey, TValue> SetItems(IEnumerable<KeyValuePair<TKey, TValue>> items);

        /// <inheritdoc cref="IImmutableDictionary{TKey,TValue}.RemoveRange"/>
        public new IImmutableMap<TKey, TValue> RemoveRange(IEnumerable<TKey> keys);

        /// <inheritdoc cref="IImmutableDictionary{TKey,TValue}.Remove"/>
        public new IImmutableMap<TKey, TValue> Remove(TKey key);
    }
}