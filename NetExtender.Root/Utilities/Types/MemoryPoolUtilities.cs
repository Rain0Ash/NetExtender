// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Buffers;

namespace NetExtender.Utilities.Types
{
    public static class MemoryPoolUtilities
    {
        /// <inheritdoc cref="MemoryPool{T}.Shared"/>
        public static MemoryPool<T> GetShared<T>()
        {
            return MemoryPool<T>.Shared;
        }

        /// <inheritdoc cref="MemoryPool{T}.Rent"/>
        public static IMemoryOwner<T> Rent<T>()
        {
            return GetShared<T>().Rent();
        }

        /// <inheritdoc cref="MemoryPool{T}.Rent"/>
        public static IMemoryOwner<T> Rent<T>(Int32 length)
        {
            return GetShared<T>().Rent(length);
        }
    }
}