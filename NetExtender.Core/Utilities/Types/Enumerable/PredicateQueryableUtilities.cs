// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using NetExtender.Utilities.Core;

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
        public static IQueryable<T> WhereNotNull<T>(this IQueryable<T?> source)
        {
            return QueryableBaseUtilities.WhereNotNull(source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IQueryable<T> WhereNotNull<T>(this IQueryable<T?> source, Expression<Func<T, Boolean>> predicate)
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
        public static IQueryable<T> WhereNotNull<T>(this IQueryable<T?> source, Expression<Func<T, Int32, Boolean>> predicate)
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

        public static IQueryable<T> Skip<T>(this IQueryable<T> source, UInt32 count)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return count switch
            {
                0 => source,
                UInt32.MaxValue => Queryable.Skip(Skip(source, UInt32.MaxValue - 1), 1),
                <= Int32.MaxValue => source.Skip((Int32) count),
                _ => Queryable.Skip(source, Int32.MaxValue).Skip((Int32) (count - Int32.MaxValue))
            };
        }

        public static IQueryable<T> Take<T>(this IQueryable<T> source, UInt32 count)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return count switch
            {
                0 => Queryable.Take(source, 0),
                <= Int32.MaxValue => source.Take((Int32) count),
                _ => throw new NotSupportedException($"Standard IQueryable providers do not support Take with a count larger than int.MaxValue ({Int32.MaxValue}). Requested: {count}.")
            };
        }

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

        public static IQueryable<T> Slice<T>(this IQueryable<T> source, UInt32 index, UInt32 count)
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IQueryable<T> Index<T>(this IQueryable<T> source, Int32 size)
        {
            return Index(source, 0, size);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IQueryable<T> Index<T>(this IQueryable<T> source, UInt32 size)
        {
            return Index(source, 0, size);
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
        public static IQueryable<T> Index<T>(this IQueryable<T> source, Int32 index, Int32 size)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (index > 0 && size > 0)
            {
                source = source.Skip(index * size);
            }

            if (size > 0)
            {
                source = source.Take(size);
            }

            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IQueryable<T> Index<T>(this IQueryable<T> source, UInt32 index, UInt32 size)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (index > 0 && size > 0)
            {
                source = source.Skip(index * size);
            }

            if (size > 0)
            {
                source = source.Take(size);
            }

            return source;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IQueryable<T> Page<T>(this IQueryable<T> source, Int32 size)
        {
            return Page(source, 1, size);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IQueryable<T> Page<T>(this IQueryable<T> source, UInt32 size)
        {
            return Page(source, 1, size);
        }

        /// <summary>
        /// Extracts <paramref name="size"/> elements from a sequence at a particular one-based page number.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the source sequence.</typeparam>
        /// <param name="source">The sequence from which to page.</param>
        /// <param name="page">The one-based page number.</param>
        /// <param name="size">The size of the page.</param>
        /// <returns>
        /// A new sequence containing elements are at the specified <paramref name="page"/> from the source sequence.
        /// </returns>
        public static IQueryable<T> Page<T>(this IQueryable<T> source, Int32 page, Int32 size)
        {
            return Index(source, page - 1, size);
        }

        public static IQueryable<T> Page<T>(this IQueryable<T> source, UInt32 page, UInt32 size)
        {
            return Index(source, page - 1, size);
        }
    }
}