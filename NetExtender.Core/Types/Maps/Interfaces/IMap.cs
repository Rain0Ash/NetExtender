// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace NetExtender.Types.Maps.Interfaces
{
    // ReSharper disable once PossibleInterfaceMemberAmbiguity
    public interface IMap<TKey, TValue> : ICollection<KeyValuePair<TKey, TValue>>
    {
        public ICollection<TKey> Keys { get; }
        public ICollection<TValue> Values { get; }
        public Boolean ContainsKey(TKey key);
        public Boolean ContainsValue(TValue key);
        public Boolean ContainsByValue(TValue key, TKey value);
        public Boolean ContainsByValue(KeyValuePair<TValue, TKey> item);
        public TValue GetValue(TKey key);
        public TKey GetKey(TValue value);
        public Boolean TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value);
        public Boolean TryGetKey(TValue key, [MaybeNullWhen(false)] out TKey value);
        public IEnumerator<KeyValuePair<TValue, TKey>> GetValuesEnumerator();
        public void Add(TKey key, TValue value);
        public void AddByValue(TValue key, TKey value);
        public void AddByValue(KeyValuePair<TValue, TKey> item);
        public Boolean TryAddByValue(TValue key, TKey value);
        public Boolean TryAddByValue(KeyValuePair<TValue, TKey> item);
        public Boolean Remove(TKey key);
        public Boolean Remove(TKey key, TValue value);
        public Boolean RemoveByValue(TValue key);
        public Boolean RemoveByValue(TValue key, TKey value);
        public Boolean RemoveByValue(TValue key, [MaybeNullWhen(false)] out TKey value);
        public Boolean RemoveByValue(KeyValuePair<TValue, TKey> item);
        public void CopyTo(KeyValuePair<TValue, TKey>[] array, Int32 index);
        public TValue this[TKey key] { get; set; }
        public TKey this[TValue key] { get; set; }
    }
}