// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using NetExtender.Types.Dictionaries.Interfaces;

namespace NetExtender.Types.Immutable.Dictionaries.Interfaces
{
    public interface IImmutableMultiDictionary<TKey, TValue> : IReadOnlyMultiDictionary<TKey, TValue>, IImmutableDictionary<TKey, TValue>, IImmutableDictionary<TKey, ImmutableHashSet<TValue>>
    {
        public new Boolean TryGetKey(TKey equalKey, out TKey actualKey);
        public new IImmutableMultiDictionary<TKey, TValue> Clear();
        public new IImmutableMultiDictionary<TKey, TValue> Add(TKey key, TValue value);
        public new IImmutableMultiDictionary<TKey, TValue> Add(TKey key, ImmutableHashSet<TValue> value);
        public new IImmutableMultiDictionary<TKey, TValue> AddRange(IEnumerable<KeyValuePair<TKey, TValue>> pairs);
        public new IImmutableMultiDictionary<TKey, TValue> AddRange(IEnumerable<KeyValuePair<TKey, ImmutableHashSet<TValue>>> pairs);
        public new IImmutableMultiDictionary<TKey, TValue> SetItem(TKey key, TValue value);
        public new IImmutableMultiDictionary<TKey, TValue> SetItem(TKey key, ImmutableHashSet<TValue> value);
        public new IImmutableMultiDictionary<TKey, TValue> SetItems(IEnumerable<KeyValuePair<TKey, TValue>> items);
        public new IImmutableMultiDictionary<TKey, TValue> SetItems(IEnumerable<KeyValuePair<TKey, ImmutableHashSet<TValue>>> items);
        public new IImmutableMultiDictionary<TKey, TValue> RemoveRange(IEnumerable<TKey> keys);
        public new IImmutableMultiDictionary<TKey, TValue> Remove(TKey key);
        public IImmutableMultiDictionary<TKey, TValue> Remove(TKey key, TValue value);
        public IImmutableMultiDictionary<TKey, TValue> Remove(TKey key, ImmutableHashSet<TValue> value);
        public new IEnumerator<KeyValuePair<TKey, ImmutableHashSet<TValue>>> GetEnumerator();
    }
}