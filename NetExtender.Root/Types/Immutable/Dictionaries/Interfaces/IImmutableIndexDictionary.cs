// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace NetExtender.Types.Immutable.Dictionaries.Interfaces
{
    // ReSharper disable once PossibleInterfaceMemberAmbiguity
    public interface IImmutableIndexDictionary<TKey, TValue> : IImmutableDictionary<TKey, TValue>
    {
        public TValue GetValueByIndex(Int32 index);
        public KeyValuePair<TKey, TValue> GetKeyValuePairByIndex(Int32 index);
        public Boolean TryGetKeyValuePairByIndex(Int32 index, out KeyValuePair<TKey, TValue> pair);
        public Int32 IndexOf(TKey key);
        public Int32 IndexOf(TKey key, Int32 index);
        public Int32 IndexOf(TKey key, Int32 index, Int32 count);
        public Int32 LastIndexOf(TKey key);
        public Int32 LastIndexOf(TKey key, Int32 index);
        public Int32 LastIndexOf(TKey key, Int32 index, Int32 count);
        public IEnumerator<TKey> GetKeyEnumerator();
        public IEnumerator<TValue> GetValueEnumerator();
        public IImmutableIndexDictionary<TKey, TValue> Insert(TKey key, TValue value);
        public IImmutableIndexDictionary<TKey, TValue> Insert(Int32 index, TKey key, TValue value);
        public IImmutableIndexDictionary<TKey, TValue> TryInsert(TKey key, TValue value);
        public IImmutableIndexDictionary<TKey, TValue> TryInsert(TKey key, TValue value, out Boolean result);
        public IImmutableIndexDictionary<TKey, TValue> TryInsert(Int32 index, TKey key, TValue value);
        public IImmutableIndexDictionary<TKey, TValue> TryInsert(Int32 index, TKey key, TValue value, out Boolean result);
        public IImmutableIndexDictionary<TKey, TValue> InsertRange(IEnumerable<KeyValuePair<TKey, TValue>> pairs);
        public IImmutableIndexDictionary<TKey, TValue> InsertRange(Int32 index, IEnumerable<KeyValuePair<TKey, TValue>> pairs);
        public IImmutableIndexDictionary<TKey, TValue> Swap(Int32 index1, Int32 index2);
        public IImmutableIndexDictionary<TKey, TValue> Reverse();
        public IImmutableIndexDictionary<TKey, TValue> Reverse(Int32 index, Int32 count);
        public IImmutableIndexDictionary<TKey, TValue> Sort();
        public IImmutableIndexDictionary<TKey, TValue> Sort(Comparison<TKey> comparison);
        public IImmutableIndexDictionary<TKey, TValue> Sort(IComparer<TKey>? comparer);
        public IImmutableIndexDictionary<TKey, TValue> Sort(Int32 index, Int32 count, IComparer<TKey>? comparer);

        /// <inheritdoc cref="IImmutableDictionary{TKey,TValue}.Add"/>
        public new IImmutableIndexDictionary<TKey, TValue> Add(TKey key, TValue value);

        /// <inheritdoc cref="IImmutableDictionary{TKey,TValue}.AddRange"/>
        public new IImmutableIndexDictionary<TKey, TValue> AddRange(IEnumerable<KeyValuePair<TKey, TValue>> pairs);

        /// <inheritdoc cref="IImmutableDictionary{TKey,TValue}.SetItem"/>
        public new IImmutableIndexDictionary<TKey, TValue> SetItem(TKey key, TValue value);

        /// <inheritdoc cref="IImmutableDictionary{TKey,TValue}.SetItems"/>
        public new IImmutableIndexDictionary<TKey, TValue> SetItems(IEnumerable<KeyValuePair<TKey, TValue>> items);

        /// <inheritdoc cref="IImmutableDictionary{TKey,TValue}.Remove"/>
        public new IImmutableIndexDictionary<TKey, TValue> Remove(TKey key);

        /// <inheritdoc cref="IImmutableDictionary{TKey,TValue}.RemoveRange"/>
        public new IImmutableIndexDictionary<TKey, TValue> RemoveRange(IEnumerable<TKey> keys);

        /// <inheritdoc cref="IImmutableDictionary{TKey,TValue}.Clear"/>
        public new IImmutableIndexDictionary<TKey, TValue> Clear();
    }
}