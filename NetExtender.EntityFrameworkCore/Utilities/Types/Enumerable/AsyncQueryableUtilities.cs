// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace NetExtender.Utilities.Types
{
    public static class AsyncQueryableUtilities
    {
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
    }
}