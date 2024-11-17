// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Buffers;
using System.Runtime.CompilerServices;

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
            return ArrayPool<T>.Shared.Rent(length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] Rent<T>(T[] source, Int32 count, out Int32 exceed)
        {
            return Rent(ArrayPool<T>.Shared, source, count, out exceed);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] Rent<T>(this ArrayPool<T> pool, T[] source, Int32 count, out Int32 exceed)
        {
            return Rent(pool, source, 0, count, out exceed);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] Rent<T>(T[] source, Int32 offset, Int32 count, out Int32 exceed)
        {
            return Rent(ArrayPool<T>.Shared, source, offset, count, out exceed);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static T[] Rent<T>(this ArrayPool<T> pool, T[] source, Int32 offset, Int32 count, out Int32 exceed)
        {
            if (pool is null)
            {
                throw new ArgumentNullException(nameof(pool));
            }

            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            T[] rent = pool.Rent(count);

            try
            {
                Array.Copy(source, offset, rent, 0, count);
                exceed = rent.Length - count;
                return rent;
            }
            catch (Exception)
            {
                pool.Return(rent);
                throw;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] Rent<T>(T[] source, Int32 count, out Memory<T> memory)
        {
            return Rent(ArrayPool<T>.Shared, source, count, out memory);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] Rent<T>(this ArrayPool<T> pool, T[] source, Int32 count, out Memory<T> memory)
        {
            return Rent(pool, source, 0, count, out memory);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] Rent<T>(T[] source, Int32 offset, Int32 count, out Memory<T> memory)
        {
            return Rent(ArrayPool<T>.Shared, source, offset, count, out memory);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static T[] Rent<T>(this ArrayPool<T> pool, T[] source, Int32 offset, Int32 count, out Memory<T> memory)
        {
            if (pool is null)
            {
                throw new ArgumentNullException(nameof(pool));
            }

            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            T[] rent = pool.Rent(count);
            
            try
            {
                Array.Copy(source, offset, rent, 0, count);
                memory = new Memory<T>(rent, 0, count);
                return rent;
            }
            catch (Exception)
            {
                pool.Return(rent);
                throw;
            }
        }
    }
}