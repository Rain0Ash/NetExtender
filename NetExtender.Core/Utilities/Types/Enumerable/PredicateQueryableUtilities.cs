// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using NetExtender.Utilities.Numerics;

namespace NetExtender.Utilities.Types
{
    public static partial class QueryableUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IQueryable<T> WhereNot<T>(this IQueryable<T> source, Expression<Func<T, Boolean>> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return source.Where(predicate.Not());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IQueryable<T> WhereNot<T>(this IQueryable<T> source, Expression<Func<T, Int32, Boolean>> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return source.Where(predicate.Not());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IQueryable<T> WhereNotNull<T>(this IQueryable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(item => item != null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IQueryable<T> WhereNotNull<T>(this IQueryable<T> source, Expression<Func<T, Boolean>> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return source.WhereNotNull().Where(predicate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IQueryable<T> WhereNotNull<T>(this IQueryable<T> source, Expression<Func<T, Int32, Boolean>> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return source.WhereNotNull().Where(predicate);
        }

        public static IQueryable<T> WhereIf<T>(this IQueryable<T> source, Expression<Func<T, Boolean>> predicate, Boolean condition)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return condition ? source.Where(predicate) : source;
        }

        public static IQueryable<T> WhereIf<T>(this IQueryable<T> source, Expression<Func<T, Int32, Boolean>> predicate, Boolean condition)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return condition ? source.Where(predicate) : source;
        }

        public static IQueryable<T> WhereIfNot<T>(this IQueryable<T> source, Expression<Func<T, Boolean>> predicate, Boolean condition)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return condition ? source.WhereNot(predicate) : source;
        }

        public static IQueryable<T> WhereIfNot<T>(this IQueryable<T> source, Expression<Func<T, Int32, Boolean>> predicate, Boolean condition)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return condition ? source.WhereNot(predicate) : source;
        }

        /// <summary>
        /// Extracts <paramref name="count"/> elements from a sequence at a particular zero-based starting index.
        /// </summary>
        /// <remarks>
        /// If the starting position or count specified result in slice extending past the end of the sequence,
        /// it will return all elements up to that point. There is no guarantee that the resulting sequence will
        /// contain the number of elements requested - it may have anywhere from 0 to <paramref name="count"/>.
        /// </remarks>
        /// <typeparam name="T">The type of the elements in the source sequence.</typeparam>
        /// <param name="source">The sequence from which to extract elements.</param>
        /// <param name="index">The zero-based index at which to begin slicing.</param>
        /// <param name="count">The number of items to slice out of the index.</param>
        /// <returns>
        /// A new sequence containing any elements sliced out from the source sequence.
        /// </returns>
        public static IQueryable<T> Slice<T>(this IQueryable<T> source, Int32 index, Int32 count)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (index > 0)
            {
                source = source.Skip(index);
            }

            if (count > 0)
            {
                source = source.Take(count);
            }

            return source;
        }

        /// <summary>
        /// Extracts <paramref name="size"/> elements from a sequence at a particular one-based page number.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the source sequence.</typeparam>
        /// <param name="source">The sequence from which to page.</param>
        /// <param name="index">The one-based page number.</param>
        /// <param name="size">The size of the page.</param>
        /// <returns>
        /// A new sequence containing elements are at the specified <paramref name="index"/> from the source sequence.
        /// </returns>
        public static IQueryable<T> Page<T>(this IQueryable<T> source, Int32 index, Int32 size)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (index > 1 && size > 0)
            {
                source = source.Skip((index - 1) * size);
            }

            if (size > 0)
            {
                source = source.Take(size);
            }

            return source;
        }
    }
}