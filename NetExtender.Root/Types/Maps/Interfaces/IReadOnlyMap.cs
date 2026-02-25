// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace NetExtender.Types.Maps.Interfaces
{
    public interface IReadOnlyMap<TKey, TValue> : IReadOnlyCollection<KeyValuePair<TKey, TValue>>
    {
        public IEnumerable<TKey> Keys { get; }
        public IEnumerable<TValue> Values { get; }
        public IEqualityComparer<TKey> Comparer { get; }
        public IEqualityComparer<TKey> KeyComparer { get; }
        public IEqualityComparer<TValue> ValueComparer { get; }
        public Boolean ContainsKey(TKey key);
        public Boolean ContainsValue(TValue key);
        public Boolean ContainsByValue(TValue key, TKey value);
        public Boolean ContainsByValue(KeyValuePair<TValue, TKey> item);
        public TValue GetValue(TKey key);
        public TKey GetKey(TValue value);
        public Boolean TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value);
        public Boolean TryGetKey(TValue key, [MaybeNullWhen(false)] out TKey value);
        public TValue this[TKey key] { get; }
        public TKey this[TValue key] { get; }
        public IEnumerator<KeyValuePair<TValue, TKey>> GetValuesEnumerator();
    }
}