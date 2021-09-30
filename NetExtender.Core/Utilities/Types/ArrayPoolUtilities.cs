// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Buffers;

namespace NetExtender.Utilities.Types
{
    public static class ArrayPoolUtilities
    {
        /// <inheritdoc cref="ArrayPool{T}.Shared"/>
        public static ArrayPool<T> GetShared<T>()
        {
            return ArrayPool<T>.Shared;
        }
        
        /// <inheritdoc cref="ArrayPool{T}.Create()"/>
        public static ArrayPool<T> Create<T>()
        {
            return ArrayPool<T>.Create();
        }
        
        /// <inheritdoc cref="ArrayPool{T}.Create(Int32,Int32)"/>
        public static ArrayPool<T> Create<T>(Int32 maxlength, Int32 bucket)
        {
            return ArrayPool<T>.Create(maxlength, bucket);
        }

        /// <inheritdoc cref="ArrayPool{T}.Rent"/>
        public static T[] Rent<T>(Int32 length)
        {
            return GetShared<T>().Rent(length);
        }
    }
}