// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace NetExtender.Types.Dictionaries.Interfaces
{
    public interface IReadOnlyMultiDictionary<TKey, TValue> : IReadOnlyDictionary<TKey, ImmutableHashSet<TValue>>, IReadOnlyDictionary<TKey, TValue>
    {
        public new Int32 Count { get; }
        public new IEnumerable<TKey> Keys { get; }
        public new IEnumerable<ImmutableHashSet<TValue>> Values { get; }
        public new Boolean ContainsKey(TKey key);
        public Boolean Contains(TKey key, TValue value);
        public new IEnumerator<KeyValuePair<TKey, ImmutableHashSet<TValue>>> GetEnumerator();
        public new ImmutableHashSet<TValue> this[TKey key] { get; }
    }
}