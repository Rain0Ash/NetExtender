// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Types.Stores.Interfaces
{
    public interface IRegisterStore<TKey> : IRegisterStore<TKey, UInt64> where TKey : class
    {
    }

    public interface IRegisterStore<TKey, TValue> : IReadOnlyMemoryStore<TKey, TValue> where TKey : class where TValue : struct, IEquatable<TValue>
    {
        public TValue Register(TKey key);
        public Boolean Remove(TKey key);
        public void Clear();
    }
}