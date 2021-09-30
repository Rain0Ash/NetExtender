// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using NetExtender.Types.Collections;

namespace NetExtender.Utilities.Types
{
    public static class EnumeratorUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerator<T> GetEmptyEnumerator<T>()
        {
            return Enumerable.Empty<T>().GetEnumerator();
        }
        
        public static Boolean TryReset(this IEnumerator enumerator)
        {
            if (enumerator is null)
            {
                throw new ArgumentNullException(nameof(enumerator));
            }

            try
            {
                enumerator.Reset();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        
        public static Boolean TryReset<T>(this IEnumerator<T> enumerator)
        {
            if (enumerator is null)
            {
                throw new ArgumentNullException(nameof(enumerator));
            }

            try
            {
                enumerator.Reset();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        
        public static IEnumerable<Object?> AsEnumerable(this IEnumerator enumerator)
        {
            if (enumerator is null)
            {
                throw new ArgumentNullException(nameof(enumerator));
            }

            while (enumerator.MoveNext())
            {
                yield return enumerator.Current;
            }
        }
        
        public static IEnumerable<T> AsEnumerable<T>(this IEnumerator<T> enumerator)
        {
            if (enumerator is null)
            {
                throw new ArgumentNullException(nameof(enumerator));
            }

            while (enumerator.MoveNext())
            {
                yield return enumerator.Current;
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerator<T> GetThreadSafeEnumerator<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable is null)
            {
                throw new ArgumentNullException(nameof(enumerable));
            }

            return new ThreadSafeEnumerator<T>(enumerable);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerator<T> GetThreadSafeEnumerator<T>(this IEnumerable<T> enumerable, Object sync)
        {
            if (enumerable is null)
            {
                throw new ArgumentNullException(nameof(enumerable));
            }

            if (sync is null)
            {
                throw new ArgumentNullException(nameof(sync));
            }

            return new ThreadSafeEnumerator<T>(enumerable, sync);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerator<T> GetThreadSafeEnumerator<T>(this IEnumerator<T> enumerator, Object sync)
        {
            if (enumerator is null)
            {
                throw new ArgumentNullException(nameof(enumerator));
            }

            if (sync is null)
            {
                throw new ArgumentNullException(nameof(sync));
            }

            return new ThreadSafeEnumerator<T>(enumerator, sync);
        }
    }
}