// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using NetExtender.Types.Maps.Interfaces;

namespace NetExtender.Types.Immutable.Maps.Interfaces
{
    public interface IImmutableIndexMap<TKey, TValue> : IImmutableMap<TKey, TValue>, IReadOnlyIndexMap<TKey, TValue>
    {
        public new TValue GetValueByIndex(Int32 index);
        public new KeyValuePair<TKey, TValue> GetKeyValuePairByIndex(Int32 index);
        public new Boolean TryGetKeyValuePairByIndex(Int32 index, out KeyValuePair<TKey, TValue> pair);
        public new Int32 IndexOf(TKey key);
        public new Int32 IndexOf(TKey key, Int32 index);
        public new Int32 IndexOf(TKey key, Int32 index, Int32 count);
        public new Int32 LastIndexOf(TKey key);
        public new Int32 LastIndexOf(TKey key, Int32 index);
        public new Int32 LastIndexOf(TKey key, Int32 index, Int32 count);
        public new IEnumerator<TKey> GetKeyEnumerator();
        public new IEnumerator<TValue> GetValueEnumerator();
        public IImmutableIndexMap<TKey, TValue> Insert(TValue key, TKey value);
        public IImmutableIndexMap<TKey, TValue> Insert(Int32 index, TValue key, TKey value);
        public IImmutableIndexMap<TKey, TValue> TryInsert(TValue key, TKey value);
        public IImmutableIndexMap<TKey, TValue> TryInsert(TValue key, TKey value, out Boolean result);
        public IImmutableIndexMap<TKey, TValue> TryInsert(Int32 index, TValue key, TKey value);
        public IImmutableIndexMap<TKey, TValue> TryInsert(Int32 index, TValue key, TKey value, out Boolean result);
        public new IImmutableIndexMap<TKey, TValue> AddByValue(TValue key, TKey value);
        public new IImmutableIndexMap<TKey, TValue> AddByValue(KeyValuePair<TValue, TKey> item);
        public new IImmutableIndexMap<TKey, TValue> Remove(TKey key, TValue value);
        public new IImmutableIndexMap<TKey, TValue> RemoveByValue(TValue key);
        public new IImmutableIndexMap<TKey, TValue> RemoveByValue(TValue key, TKey value);
        public new IImmutableIndexMap<TKey, TValue> RemoveByValue(KeyValuePair<TValue, TKey> item);
        public IImmutableIndexMap<TKey, TValue> Insert(TKey key, TValue value);
        public IImmutableIndexMap<TKey, TValue> Insert(Int32 index, TKey key, TValue value);
        public IImmutableIndexMap<TKey, TValue> TryInsert(TKey key, TValue value);
        public IImmutableIndexMap<TKey, TValue> TryInsert(TKey key, TValue value, out Boolean result);
        public IImmutableIndexMap<TKey, TValue> TryInsert(Int32 index, TKey key, TValue value);
        public IImmutableIndexMap<TKey, TValue> TryInsert(Int32 index, TKey key, TValue value, out Boolean result);
        public IImmutableIndexMap<TKey, TValue> Swap(Int32 index1, Int32 index2);
        public IImmutableIndexMap<TKey, TValue> Reverse();
        public IImmutableIndexMap<TKey, TValue> Reverse(Int32 index, Int32 count);
        public IImmutableIndexMap<TKey, TValue> Sort();
        public IImmutableIndexMap<TKey, TValue> Sort(Comparison<TKey> comparison);
        public IImmutableIndexMap<TKey, TValue> Sort(IComparer<TKey>? comparer);
        public IImmutableIndexMap<TKey, TValue> Sort(Int32 index, Int32 count, IComparer<TKey>? comparer);

        /// <inheritdoc cref="IImmutableMap{TKey,TValue}.Add"/>
        public new IImmutableIndexMap<TKey, TValue> Add(TKey key, TValue value);

        /// <inheritdoc cref="IImmutableMap{TKey,TValue}.AddRange"/>
        public new IImmutableIndexMap<TKey, TValue> AddRange(IEnumerable<KeyValuePair<TKey, TValue>> pairs);

        /// <inheritdoc cref="IImmutableMap{TKey,TValue}.SetItem"/>
        public new IImmutableIndexMap<TKey, TValue> SetItem(TKey key, TValue value);

        /// <inheritdoc cref="IImmutableMap{TKey,TValue}.SetItems"/>
        public new IImmutableIndexMap<TKey, TValue> SetItems(IEnumerable<KeyValuePair<TKey, TValue>> items);

        /// <inheritdoc cref="IImmutableMap{TKey,TValue}.Remove(TKey)"/>
        public new IImmutableIndexMap<TKey, TValue> Remove(TKey key);
        
        /// <inheritdoc cref="IImmutableMap{TKey,TValue}.RemoveRange"/>
        public new IImmutableIndexMap<TKey, TValue> RemoveRange(IEnumerable<TKey> keys);
        
        /// <inheritdoc cref="IImmutableMap{TKey,TValue}.Clear"/>
        public new IImmutableIndexMap<TKey, TValue> Clear();
    }
}