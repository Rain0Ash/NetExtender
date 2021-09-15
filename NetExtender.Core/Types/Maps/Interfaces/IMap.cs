// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace NetExtender.Types.Maps.Interfaces
{
    // ReSharper disable once PossibleInterfaceMemberAmbiguity
    public interface IMap<TKey, TValue> : IDictionary<TKey, TValue>
    {
        public Boolean ContainsValue(TValue key);
        public Boolean ContainsByValue(TValue key, TKey value);
        public Boolean ContainsByValue(KeyValuePair<TValue, TKey> item);
        public TValue GetValue(TKey key);
        public TKey GetKey(TValue value);
        public Boolean TryGetKey(TValue key, [MaybeNullWhen(false)] out TKey value);

        public IEnumerator<KeyValuePair<TValue, TKey>> GetValuesEnumerator();
        
        public void AddByValue(TValue key, TKey value);
        public void AddByValue(KeyValuePair<TValue, TKey> item);

        public Boolean TryAddByValue(TValue key, TKey value);
        public Boolean TryAddByValue(KeyValuePair<TValue, TKey> item);

        public Boolean Remove(TKey key, TValue value);
        
        public Boolean RemoveByValue(TValue key);

        public Boolean RemoveByValue(TValue key, TKey value);

        public Boolean RemoveByValue(TValue key, [MaybeNullWhen(false)] out TKey value);
        public Boolean RemoveByValue(KeyValuePair<TValue, TKey> item);

        public new TValue this[TKey key] { get; set; }
        public TKey this[TValue key] { get; set; }

        public void CopyTo(KeyValuePair<TValue, TKey>[] array, Int32 index);
    }
}