// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Types.Storages.Interfaces
{
    public interface IRegisterStorage<TKey> : IRegisterStorage<TKey, UInt64> where TKey : class
    {
    }

    public interface IRegisterStorage<TKey, TValue> : IReadOnlyMemoryStorage<TKey, TValue> where TKey : class where TValue : struct, IEquatable<TValue>
    {
        public TValue Register(TKey key);
        public Boolean Remove(TKey key);
        public void Clear();
    }
}