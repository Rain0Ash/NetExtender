// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NetExtender.Types.Lists;
using NetExtender.Types.Lists.Interfaces;

namespace NetExtender.Utilities.Types
{
    public static class AsyncQueryableUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> HasAtLeastAsync<T>(this IQueryable<T> source, Int32 count)
        {
            return HasAtLeastAsync(source, count, CancellationToken.None);
        }

        public static async Task<Boolean> HasAtLeastAsync<T>(this IQueryable<T> source, Int32 count, CancellationToken token)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return await source.Take(count).CountAsync(token).ConfigureAwait(false) >= count;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> HasAtLeastAsync<T>(this IQueryable<T> source, Expression<Func<T, Boolean>> predicate, Int32 count)
        {
            return HasAtLeastAsync(source, predicate, count, CancellationToken.None);
        }

        public static Task<Boolean> HasAtLeastAsync<T>(this IQueryable<T> source, Expression<Func<T, Boolean>> predicate, Int32 count, CancellationToken token)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return HasAtLeastAsync(source.Where(predicate), count, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> HasAtLeastAsync<T>(this IQueryable<T> source, Expression<Func<T, Int32, Boolean>> predicate, Int32 count)
        {
            return HasAtLeastAsync(source, predicate, count, CancellationToken.None);
        }

        public static Task<Boolean> HasAtLeastAsync<T>(this IQueryable<T> source, Expression<Func<T, Int32, Boolean>> predicate, Int32 count, CancellationToken token)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return HasAtLeastAsync(source.Where(predicate), count, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> HasAtMostAsync<T>(this IQueryable<T> source, Int32 count)
        {
            return HasAtMostAsync(source, count, CancellationToken.None);
        }

        public static async Task<Boolean> HasAtMostAsync<T>(this IQueryable<T> source, Int32 count, CancellationToken token)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return await source.Take(count).CountAsync(token).ConfigureAwait(false) <= count;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> HasAtMostAsync<T>(this IQueryable<T> source, Expression<Func<T, Boolean>> predicate, Int32 count)
        {
            return HasAtMostAsync(source, predicate, count, CancellationToken.None);
        }

        public static Task<Boolean> HasAtMostAsync<T>(this IQueryable<T> source, Expression<Func<T, Boolean>> predicate, Int32 count, CancellationToken token)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return HasAtMostAsync(source.Where(predicate), count, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> HasAtMostAsync<T>(this IQueryable<T> source, Expression<Func<T, Int32, Boolean>> predicate, Int32 count)
        {
            return HasAtMostAsync(source, predicate, count, CancellationToken.None);
        }

        public static Task<Boolean> HasAtMostAsync<T>(this IQueryable<T> source, Expression<Func<T, Int32, Boolean>> predicate, Int32 count, CancellationToken token)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return HasAtMostAsync(source.Where(predicate), count, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> HasExactlyAsync<T>(this IQueryable<T> source, Int32 count)
        {
            return HasExactlyAsync(source, count, CancellationToken.None);
        }

        public static async Task<Boolean> HasExactlyAsync<T>(this IQueryable<T> source, Int32 count, CancellationToken token)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return await source.Take(count + 1).CountAsync(token).ConfigureAwait(false) == count;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> HasExactlyAsync<T>(this IQueryable<T> source, Expression<Func<T, Boolean>> predicate, Int32 count)
        {
            return HasExactlyAsync(source, predicate, count, CancellationToken.None);
        }

        public static Task<Boolean> HasExactlyAsync<T>(this IQueryable<T> source, Expression<Func<T, Boolean>> predicate, Int32 count, CancellationToken token)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return HasExactlyAsync(source.Where(predicate), count, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> HasExactlyAsync<T>(this IQueryable<T> source, Expression<Func<T, Int32, Boolean>> predicate, Int32 count)
        {
            return HasExactlyAsync(source, predicate, count, CancellationToken.None);
        }

        public static Task<Boolean> HasExactlyAsync<T>(this IQueryable<T> source, Expression<Func<T, Int32, Boolean>> predicate, Int32 count, CancellationToken token)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return HasExactlyAsync(source.Where(predicate), count, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IPaginationList<T, T[]>> PaginationAsync<T>(this IQueryable<T> source, Int32 index, Int32 size)
        {
            return PaginationAsync(source, index, size, CancellationToken.None);
        }

        public static async Task<IPaginationList<T, T[]>> PaginationAsync<T>(this IQueryable<T> source, Int32 index, Int32 size, CancellationToken token)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            Int32 count = await source.CountAsync(token).ConfigureAwait(false);
            T[] items = await source.Page(index, size).ToArrayAsync(token).ConfigureAwait(false);
            return new PaginationListWrapper<T, T[]>(items, index, size, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<TCollection> PaginationAsync<T, TCollection>(this IQueryable<T> source, Int32 index, Int32 size, Func<IPaginationList<T, T[]>, TCollection> converter) where TCollection : class, IEnumerable<T>
        {
            return PaginationAsync(source, index, size, converter, CancellationToken.None);
        }

        public static async Task<TCollection> PaginationAsync<T, TCollection>(this IQueryable<T> source, Int32 index, Int32 size, Func<IPaginationList<T, T[]>, TCollection> converter, CancellationToken token) where TCollection : class, IEnumerable<T>
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (converter is null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            IPaginationList<T, T[]> result = await PaginationAsync(source, index, size, token).ConfigureAwait(false);
            return converter(result);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<TCollection> PaginationAsync<T, TCollection>(this IQueryable<T> source, Int32 index, Int32 size, Func<IPaginationList<T, T[]>, Task<TCollection>> converter) where TCollection : class, IEnumerable<T>
        {
            return PaginationAsync(source, index, size, converter, CancellationToken.None);
        }

        public static async Task<TCollection> PaginationAsync<T, TCollection>(this IQueryable<T> source, Int32 index, Int32 size, Func<IPaginationList<T, T[]>, Task<TCollection>> converter, CancellationToken token) where TCollection : class, IEnumerable<T>
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (converter is null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            IPaginationList<T, T[]> result = await PaginationAsync(source, index, size, token).ConfigureAwait(false);
            return await converter(result).ConfigureAwait(false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<TCollection> PaginationAsync<T, TCollection>(this IQueryable<T> source, Int32 index, Int32 size, Func<IPaginationList<T, T[]>, ValueTask<TCollection>> converter) where TCollection : class, IEnumerable<T>
        {
            return PaginationAsync(source, index, size, converter, CancellationToken.None);
        }

        public static async Task<TCollection> PaginationAsync<T, TCollection>(this IQueryable<T> source, Int32 index, Int32 size, Func<IPaginationList<T, T[]>, ValueTask<TCollection>> converter, CancellationToken token) where TCollection : class, IEnumerable<T>
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (converter is null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            IPaginationList<T, T[]> result = await PaginationAsync(source, index, size, token).ConfigureAwait(false);
            return await converter(result).ConfigureAwait(false);
        }
    }
}