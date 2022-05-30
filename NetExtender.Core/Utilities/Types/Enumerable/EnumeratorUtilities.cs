// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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

        public static Boolean MoveNext<T>(this IEnumerator<T> enumerator, [MaybeNullWhen(false)] out T result)
        {
            if (enumerator is null)
            {
                throw new ArgumentNullException(nameof(enumerator));
            }

            if (!enumerator.MoveNext())
            {
                result = default;
                return false;
            }

            result = enumerator.Current;
            return true;
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

        public static void Evaluate(this IEnumerator enumerator)
        {
            if (enumerator is null)
            {
                throw new ArgumentNullException(nameof(enumerator));
            }

            while (enumerator.MoveNext())
            {
            }
        }
        
        public static void Evaluate<T>(this IEnumerator<T> enumerator)
        {
            if (enumerator is null)
            {
                throw new ArgumentNullException(nameof(enumerator));
            }
            
            while (enumerator.MoveNext())
            {
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerator GetThreadSafeEnumerator(this IEnumerable enumerable)
        {
            if (enumerable is null)
            {
                throw new ArgumentNullException(nameof(enumerable));
            }

            return new ThreadSafeEnumerator(enumerable);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerator GetThreadSafeEnumerator(this IEnumerable enumerable, Object synchronization)
        {
            if (enumerable is null)
            {
                throw new ArgumentNullException(nameof(enumerable));
            }

            if (synchronization is null)
            {
                throw new ArgumentNullException(nameof(synchronization));
            }

            return new ThreadSafeEnumerator(enumerable, synchronization);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerator GetThreadSafeEnumerator(this IEnumerator enumerator, Object synchronization)
        {
            if (enumerator is null)
            {
                throw new ArgumentNullException(nameof(enumerator));
            }

            if (synchronization is null)
            {
                throw new ArgumentNullException(nameof(synchronization));
            }

            return new ThreadSafeEnumerator(enumerator, synchronization);
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
        public static IEnumerator<T> GetThreadSafeEnumerator<T>(this IEnumerable<T> enumerable, Object synchronization)
        {
            if (enumerable is null)
            {
                throw new ArgumentNullException(nameof(enumerable));
            }

            if (synchronization is null)
            {
                throw new ArgumentNullException(nameof(synchronization));
            }

            return new ThreadSafeEnumerator<T>(enumerable, synchronization);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerator<T> GetThreadSafeEnumerator<T>(this IEnumerator<T> enumerator, Object synchronization)
        {
            if (enumerator is null)
            {
                throw new ArgumentNullException(nameof(enumerator));
            }

            if (synchronization is null)
            {
                throw new ArgumentNullException(nameof(synchronization));
            }

            return new ThreadSafeEnumerator<T>(enumerator, synchronization);
        }
    }
}