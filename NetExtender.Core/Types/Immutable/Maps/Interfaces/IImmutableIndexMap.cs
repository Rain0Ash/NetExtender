// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using NetExtender.Types.Immutable.Dictionaries.Interfaces;

namespace NetExtender.Types.Immutable.Maps.Interfaces
{
    public interface IImmutableIndexMap<TKey, TValue> : IImmutableMap<TKey, TValue>, IImmutableIndexDictionary<TKey, TValue>
    {
        public Int32 IndexOfValue(TValue key);

        public TKey GetKeyByIndex(Int32 index);

        public KeyValuePair<TValue, TKey> GetValueKeyPairByIndex(Int32 index);

        public Boolean TryGetValueKeyPairByIndex(Int32 index, out KeyValuePair<TValue, TKey> pair);
        
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
        
        public new IImmutableIndexMap<TKey, TValue> Insert(TKey key, TValue value);

        public new IImmutableIndexMap<TKey, TValue> Insert(Int32 index, TKey key, TValue value);

        public new IImmutableIndexMap<TKey, TValue> TryInsert(TKey key, TValue value);
        
        public new IImmutableIndexMap<TKey, TValue> TryInsert(TKey key, TValue value, out Boolean result);

        public new IImmutableIndexMap<TKey, TValue> TryInsert(Int32 index, TKey key, TValue value);
        
        public new IImmutableIndexMap<TKey, TValue> TryInsert(Int32 index, TKey key, TValue value, out Boolean result);

        public new IImmutableIndexMap<TKey, TValue> Swap(Int32 index1, Int32 index2);

        public new IImmutableIndexMap<TKey, TValue> Reverse();

        public new IImmutableIndexMap<TKey, TValue> Reverse(Int32 index, Int32 count);

        public new IImmutableIndexMap<TKey, TValue> Sort();

        public new IImmutableIndexMap<TKey, TValue> Sort(Comparison<TKey> comparison);

        public new IImmutableIndexMap<TKey, TValue> Sort(IComparer<TKey>? comparer);

        public new IImmutableIndexMap<TKey, TValue> Sort(Int32 index, Int32 count, IComparer<TKey>? comparer);
        
        /// <inheritdoc cref="IImmutableMap{TKey,TValue}.Clear"/>
        public new IImmutableIndexMap<TKey, TValue> Clear();

        /// <inheritdoc cref="IImmutableMap{TKey,TValue}.Add"/>
        public new IImmutableIndexMap<TKey, TValue> Add(TKey key, TValue value);

        /// <inheritdoc cref="IImmutableMap{TKey,TValue}.AddRange"/>
        public new IImmutableIndexMap<TKey, TValue> AddRange(IEnumerable<KeyValuePair<TKey, TValue>> pairs);

        /// <inheritdoc cref="IImmutableMap{TKey,TValue}.SetItem"/>
        public new IImmutableIndexMap<TKey, TValue> SetItem(TKey key, TValue value);

        /// <inheritdoc cref="IImmutableMap{TKey,TValue}.SetItems"/>
        public new IImmutableIndexMap<TKey, TValue> SetItems(IEnumerable<KeyValuePair<TKey, TValue>> items);

        /// <inheritdoc cref="IImmutableMap{TKey,TValue}.RemoveRange"/>
        public new IImmutableIndexMap<TKey, TValue> RemoveRange(IEnumerable<TKey> keys);

        /// <inheritdoc cref="IImmutableMap{TKey,TValue}.Remove(TKey)"/>
        public new IImmutableIndexMap<TKey, TValue> Remove(TKey key);
    }
}