// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;

namespace NetExtender.Types.Maps
{
    public interface IReadOnlyMap<TKey, TValue> : IReadOnlyDictionary<TKey, TValue>
    {
        public Boolean ContainsByValue(TValue key, TKey value);
        public Boolean ContainsByValue(KeyValuePair<TValue, TKey> item);
        public Boolean TryGetKey(TValue key, out TKey value);
        public TKey this[TValue key] { get; }

        public IEnumerator<KeyValuePair<TValue, TKey>> GetValuesEnumerator();
    }
}