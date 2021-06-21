// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using NetExtender.Types.Dictionaries.Interfaces;

namespace NetExtender.Types.Maps.Interfaces
{
    public interface IReadOnlyIndexMap<TKey, TValue> : IReadOnlyMap<TKey, TValue>, IReadOnlyIndexDictionary<TKey, TValue>
    {
        public Int32 IndexOfValue(TValue key);
        public TKey GetKeyByIndex(Int32 index);
        public KeyValuePair<TValue, TKey> GetValueKeyPairByIndex(Int32 index);
        public Boolean TryGetValueKeyPairByIndex(Int32 index, out KeyValuePair<TValue, TKey> pair);
    }
}