// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;

namespace NetExtender.Types.Maps.Interfaces
{
    public interface IIndexMap<TKey, TValue> : IMap<TKey, TValue>
    {
        public TKey GetKeyByIndex(Int32 index);
        public TValue GetValueByIndex(Int32 index);
        public KeyValuePair<TKey, TValue> GetKeyValuePairByIndex(Int32 index);
        public Boolean TryGetKeyValuePairByIndex(Int32 index, out KeyValuePair<TKey, TValue> pair);
        public KeyValuePair<TValue, TKey> GetValueKeyPairByIndex(Int32 index);
        public Boolean TryGetValueKeyPairByIndex(Int32 index, out KeyValuePair<TValue, TKey> pair);
        public Int32 IndexOf(TKey key);
        public Int32 IndexOf(TKey key, Int32 index);
        public Int32 IndexOf(TKey key, Int32 index, Int32 count);
        public Int32 LastIndexOf(TKey key);
        public Int32 LastIndexOf(TKey key, Int32 index);
        public Int32 LastIndexOf(TKey key, Int32 index, Int32 count);
        public Int32 IndexOfValue(TValue key);
        public Int32 IndexOfValue(TValue key, Int32 index);
        public Int32 IndexOfValue(TValue key, Int32 index, Int32 count);
        public Int32 LastIndexOfValue(TValue key);
        public Int32 LastIndexOfValue(TValue key, Int32 index);
        public Int32 LastIndexOfValue(TValue key, Int32 index, Int32 count);
        public IEnumerator<TKey> GetKeyEnumerator();
        public IEnumerator<TValue> GetValueEnumerator();
        public void Insert(KeyValuePair<TKey, TValue> item);
        public void Insert(Int32 index, KeyValuePair<TKey, TValue> item);
        public void Insert(TKey key, TValue value);
        public void Insert(Int32 index, TKey key, TValue value);
        public void InsertByValue(KeyValuePair<TValue, TKey> item);
        public void InsertByValue(Int32 index, KeyValuePair<TValue, TKey> item);
        public void InsertByValue(TValue key, TKey value);
        public void InsertByValue(Int32 index, TValue key, TKey value);
        public Boolean TryInsert(KeyValuePair<TKey, TValue> item);
        public Boolean TryInsert(Int32 index, KeyValuePair<TKey, TValue> item);
        public Boolean TryInsert(TKey key, TValue value);
        public Boolean TryInsert(Int32 index, TKey key, TValue value);
        public Boolean TryInsertByValue(KeyValuePair<TValue, TKey> item);
        public Boolean TryInsertByValue(Int32 index, KeyValuePair<TValue, TKey> item);
        public Boolean TryInsertByValue(TValue key, TKey value);
        public Boolean TryInsertByValue(Int32 index, TValue key, TKey value);
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