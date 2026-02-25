// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;

namespace NetExtender.Types.Storages.Interfaces
{
    public interface IReadOnlyMemoryStorage<TKey, TValue> : IReadOnlyStorage<TKey, TValue> where TKey : class where TValue : struct, IEquatable<TValue>
    {
        public TKey? Current { get; }
        public TValue Value { get; }
        public Boolean TryGetKey(TValue value, [MaybeNullWhen(false)] out TKey key);
    }
}