// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Types.Concurrent.Interfaces;
using NetExtender.Types.Storages.Interfaces;

namespace NetExtender.Types.Concurrent.Storages.Interfaces
{
    public interface IReadOnlyConcurrentMemoryStorage<TKey, TValue> : IReadOnlyMemoryStorage<TKey, TValue>, ILockable where TKey : class where TValue : struct, IEquatable<TValue>
    {
    }
}