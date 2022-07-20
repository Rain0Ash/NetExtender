// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace NetExtender.Types.Dictionaries.Interfaces
{
    public interface IReadOnlyWeakDictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>> where TKey : class
    {
        public Boolean Contains(TKey key);
        public Boolean TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value);
        public TValue this[TKey key] { get; }
    }
}