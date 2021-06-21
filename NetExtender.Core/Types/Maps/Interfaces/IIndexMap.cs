// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using NetExtender.Types.Dictionaries.Interfaces;

namespace NetExtender.Types.Maps.Interfaces
{
    public interface IIndexMap<TKey, TValue> : IMap<TKey, TValue>, IIndexDictionary<TKey, TValue>
    {
        public Int32 IndexOfValue(TValue key);

        public TKey GetKeyByIndex(Int32 index);

        public KeyValuePair<TValue, TKey> GetValueKeyPairByIndex(Int32 index);

        public Boolean TryGetValueKeyPairByIndex(Int32 index, out KeyValuePair<TValue, TKey> pair);
        
        public void Insert(TValue key, TKey value);

        public void Insert(Int32 index, TValue key, TKey value);

        public Boolean TryInsert(TValue key, TKey value);

        public Boolean TryInsert(Int32 index, TValue key, TKey value);
    }
}