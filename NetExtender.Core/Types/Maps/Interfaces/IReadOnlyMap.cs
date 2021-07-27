// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace NetExtender.Types.Maps.Interfaces
{
    public interface IReadOnlyMap<TKey, TValue> : IReadOnlyDictionary<TKey, TValue>
    {
        public Boolean ContainsValue(TValue key);
        public Boolean ContainsByValue(TValue key, TKey value);
        public Boolean ContainsByValue(KeyValuePair<TValue, TKey> item);
        public TValue GetValue(TKey key);
        public TKey GetKey(TValue key);
        public Boolean TryGetKey(TValue key, [MaybeNullWhen(false)] out TKey value);
        public new TValue this[TKey key] { get; }
        public TKey this[TValue key] { get; }

        public IEnumerator<KeyValuePair<TValue, TKey>> GetValuesEnumerator();
    }
}