// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;

namespace NetExtender.Types.Dictionaries.Interfaces
{
    public interface IReadOnlyIndexDictionary<TKey, TValue> : IReadOnlyDictionary<TKey, TValue>
    {
        public IReadOnlyList<TKey> OrderedKeys { get; }
        public TKey GetKeyByIndex(Int32 index);
        public TValue GetValueByIndex(Int32 index);
        public KeyValuePair<TKey, TValue> GetKeyValuePairByIndex(Int32 index);
        public Boolean TryGetKeyValuePairByIndex(Int32 index, out KeyValuePair<TKey, TValue> pair);
        public Int32 IndexOf(TKey key);
        public IEnumerator<TKey> GetKeyEnumerator();
        public IEnumerator<TValue> GetValueEnumerator();
    }
}