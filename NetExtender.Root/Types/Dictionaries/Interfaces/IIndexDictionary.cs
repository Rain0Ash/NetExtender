// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;

namespace NetExtender.Types.Dictionaries.Interfaces
{
    // ReSharper disable once PossibleInterfaceMemberAmbiguity
    public interface IIndexDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        public TKey GetKeyByIndex(Int32 index);
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
        public void Insert(TKey key, TValue value);
        public void Insert(Int32 index, TKey key, TValue value);
        public Boolean TryInsert(TKey key, TValue value);
        public Boolean TryInsert(Int32 index, TKey key, TValue value);
        public void SetValueByIndex(Int32 index, TValue value);
        public Boolean TrySetValueByIndex(Int32 index, TValue value);
        public Boolean RemoveAt(Int32 index);
        public Boolean RemoveAt(Int32 index, out KeyValuePair<TKey, TValue> pair);
        public void Swap(Int32 index1, Int32 index2);
        public void Reverse();
        public void Reverse(Int32 index, Int32 count);
        public void Sort();
        public void Sort(Comparison<TKey> comparison);
        public void Sort(IComparer<TKey>? comparer);
        public void Sort(Int32 index, Int32 count, IComparer<TKey>? comparer);
    }
}