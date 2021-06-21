// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace NetExtender.Utils.Types
{
    public static class SpanUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Span<T> Change<T>(this Memory<T> memory, Func<T, T> selector)
        {
            return Change(memory.Span, selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Span<T> Change<T>(this Span<T> span, Func<T, T> selector)
        {
            for (Int32 i = 0; i < span.Length; i++)
            {
                span[i] = selector(span[i]);
            }

            return span;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Span<T> ForEach<T>(this Memory<T> memory, Action<T> action)
        {
            return ForEach(memory.Span, action);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Span<T> ForEach<T>(this Span<T> span, Action<T> action)
        {
            foreach (T item in span)
            {
                action(item);
            }

            return span;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlySpan<T> ForEach<T>(this ReadOnlyMemory<T> memory, Action<T> action)
        {
            return ForEach(memory.Span, action);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlySpan<T> ForEach<T>(this ReadOnlySpan<T> span, Action<T> action)
        {
            foreach (T item in span)
            {
                action(item);
            }

            return span;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Any<T>(this Memory<T> memory)
        {
            return !memory.IsEmpty;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Any<T>(this Span<T> span)
        {
            return !span.IsEmpty;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Any<T>(this ReadOnlyMemory<T> memory)
        {
            return !memory.IsEmpty;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Any<T>(this ReadOnlySpan<T> span)
        {
            return !span.IsEmpty;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Any<T>(this Memory<T> memory, Func<T, Boolean> predicate)
        {
            return Any(memory.Span, predicate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Any<T>(this Span<T> span, Func<T, Boolean> predicate)
        {
            foreach (T item in span)
            {
                if (predicate(item))
                {
                    return true;
                }
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Any<T>(this ReadOnlyMemory<T> memory, Func<T, Boolean> predicate)
        {
            return Any(memory.Span, predicate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Any<T>(this ReadOnlySpan<T> span, Func<T, Boolean> predicate)
        {
            foreach (T item in span)
            {
                if (predicate(item))
                {
                    return true;
                }
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean All<T>(this Memory<T> memory, Func<T, Boolean> predicate)
        {
            return All(memory.Span, predicate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean All<T>(this Span<T> span, Func<T, Boolean> predicate)
        {
            return !Any(span, predicate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean All<T>(this ReadOnlyMemory<T> memory, Func<T, Boolean> predicate)
        {
            return All(memory.Span, predicate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean All<T>(this ReadOnlySpan<T> span, Func<T, Boolean> predicate)
        {
            return !Any(span, predicate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Count<T>(this Memory<T> memory)
        {
            return memory.Length;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Count<T>(this Span<T> span)
        {
            return span.Length;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Count<T>(this ReadOnlyMemory<T> memory)
        {
            return memory.Length;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Count<T>(this ReadOnlySpan<T> span)
        {
            return span.Length;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Count<T>(this Memory<T> memory, Func<T, Boolean> predicate)
        {
            return Count(memory.Span, predicate);
        }

        public static Int32 Count<T>(this Span<T> span, Func<T, Boolean> predicate)
        {
            Int32 count = 0;

            foreach (T item in span)
            {
                if (predicate(item))
                {
                    count++;
                }
            }

            return count;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Count<T>(this ReadOnlyMemory<T> memory, Func<T, Boolean> predicate)
        {
            return Count(memory.Span, predicate);
        }

        public static Int32 Count<T>(this ReadOnlySpan<T> span, Func<T, Boolean> predicate)
        {
            Int32 count = 0;

            foreach (T item in span)
            {
                if (predicate(item))
                {
                    count++;
                }
            }

            return count;
        }

        /// <summary>
        /// Determines whether all elements in this collection are equal to each other. Compares using the <see cref="object.Equals(object)"/> method.
        /// <para>This method returns true if the collection is empty or it contains only one element.</para>
        /// </summary>
        /// <typeparam name="T">The type of the elements.</typeparam>
        /// <param name="source">The collection.</param>
        /// <param name="selector">Selector</param>
        public static T Aggregate<T>(this ReadOnlySpan<T> source, Func<T, T, T> selector)
        {
            if (source.Length <= 0)
            {
                throw new ArgumentException(nameof(source));
            }
            
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            T result = source[0];

            for (Int32 i = 1; i < source.Length; i++)
            {
                result = selector(result, source[i]);
            }

            return result;
        }
        
        /// <summary>
        /// Determines whether all elements in this collection are equal to each other. Compares using the <see cref="object.Equals(object)"/> method.
        /// <para>This method returns true if the collection is empty or it contains only one element.</para>
        /// </summary>
        /// <typeparam name="T">The type of the elements.</typeparam>
        /// <param name="source">The collection.</param>
        /// <param name="selector">Selector</param>
        public static T Aggregate<T>(this Span<T> source, Func<T, T, T> selector)
        {
            if (source.Length <= 0)
            {
                throw new ArgumentException(nameof(source));
            }
            
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            T result = source[0];

            for (Int32 i = 1; i < source.Length; i++)
            {
                result = selector(result, source[i]);
            }

            return result;
        }
        
        /// <summary>
        /// Determines whether all elements in this collection are equal to each other. Compares using the <see cref="object.Equals(object)"/> method.
        /// <para>This method returns true if the collection is empty or it contains only one element.</para>
        /// </summary>
        /// <typeparam name="T">The type of the elements.</typeparam>
        /// <param name="source">The collection.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean AllSame<T>(this ReadOnlySpan<T> source)
        {
            return AllSame(source, EqualityComparer<T>.Default);
        }

        /// <summary>
        /// Determines whether all elements in this collection are equal to each other. Compares using the <see cref="object.Equals(object)"/> method.
        /// <para>This method returns true if the collection is empty or it contains only one element.</para>
        /// </summary>
        /// <typeparam name="T">The type of the elements.</typeparam>
        /// <param name="source">The collection.</param>
        /// <param name="comparer">The comparer</param>
        public static Boolean AllSame<T>(this ReadOnlySpan<T> source, IEqualityComparer<T>? comparer)
        {
            if (comparer is null)
            {
                throw new ArgumentNullException(nameof(comparer));
            }

            if (source.Length <= 0)
            {
                return true;
            }
            
            comparer ??= EqualityComparer<T>.Default;

            T first = source[0];
            return source.All(item => comparer.Equals(first, item));
        }
        
        /// <summary>
        /// Determines whether all elements in this collection are equal to each other. Compares using the <see cref="object.Equals(object)"/> method.
        /// <para>This method returns true if the collection is empty or it contains only one element.</para>
        /// </summary>
        /// <typeparam name="T">The type of the elements.</typeparam>
        /// <param name="source">The collection.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean AllSame<T>(this Span<T> source)
        {
            return AllSame(source, EqualityComparer<T>.Default);
        }

        /// <summary>
        /// Determines whether all elements in this collection are equal to each other. Compares using the <see cref="object.Equals(object)"/> method.
        /// <para>This method returns true if the collection is empty or it contains only one element.</para>
        /// </summary>
        /// <typeparam name="T">The type of the elements.</typeparam>
        /// <param name="source">The collection.</param>
        /// <param name="comparer">The comparer</param>
        public static Boolean AllSame<T>(this Span<T> source, IEqualityComparer<T>? comparer)
        {
            if (comparer is null)
            {
                throw new ArgumentNullException(nameof(comparer));
            }

            if (source.Length <= 0)
            {
                return true;
            }
            
            comparer ??= EqualityComparer<T>.Default;

            T first = source[0];
            return source.All(item => comparer.Equals(first, item));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> AsEnumerable<T>(this Memory<T> memory)
        {
            for (Int32 i = 0; i < memory.Length; i++)
            {
                yield return memory.Span[i];
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> AsEnumerable<T>(this ReadOnlyMemory<T> memory)
        {
            for (Int32 i = 0; i < memory.Length; i++)
            {
                yield return memory.Span[i];
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Span<T>.Enumerator GetEnumerator<T>(this Memory<T> memory)
        {
            return memory.Span.GetEnumerator();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlySpan<T>.Enumerator GetEnumerator<T>(this ReadOnlyMemory<T> memory)
        {
            return memory.Span.GetEnumerator();
        }
    }
}