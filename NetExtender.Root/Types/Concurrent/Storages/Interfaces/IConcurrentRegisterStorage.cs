// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Types.Storages.Interfaces;

namespace NetExtender.Types.Concurrent.Storages.Interfaces
{
    public interface IConcurrentRegisterStorage<TKey> : IConcurrentRegisterStorage<TKey, UInt64>, IRegisterStorage<TKey> where TKey : class
    {
    }

    public interface IConcurrentRegisterStorage<TKey, TValue> : IRegisterStorage<TKey, TValue>, IReadOnlyConcurrentMemoryStorage<TKey, TValue> where TKey : class where TValue : struct, IEquatable<TValue>
    {
    }
}