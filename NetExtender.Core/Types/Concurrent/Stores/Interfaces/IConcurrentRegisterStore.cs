// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Types.Stores.Interfaces;

namespace NetExtender.Types.Concurrent.Stores.Interfaces
{
    public interface IConcurrentRegisterStore<TKey> : IConcurrentRegisterStore<TKey, UInt64>, IRegisterStore<TKey> where TKey : class
    {
    }

    public interface IConcurrentRegisterStore<TKey, TValue> : IRegisterStore<TKey, TValue>, IReadOnlyConcurrentMemoryStore<TKey, TValue> where TKey : class where TValue : struct, IEquatable<TValue>
    {
    }
}