// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Mapster;
using Microsoft.EntityFrameworkCore;
using NetExtender.Types.Lists;
using NetExtender.Types.Lists.Interfaces;

namespace NetExtender.Utilities.Types
{
    public static class MapsterAsyncQueryableUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IPaginationList<TDestination, TDestination[]>> PaginationAsync<TSource, TDestination>(IQueryable<TSource> source, Int32 index, Int32 size)
        {
            return PaginationAsync<TSource, TDestination>(source, index, size, CancellationToken.None);
        }

        public static async Task<IPaginationList<TDestination, TDestination[]>> PaginationAsync<TSource, TDestination>(IQueryable<TSource> source, Int32 index, Int32 size, CancellationToken token)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            Int32 count = await source.CountAsync(token).ConfigureAwait(false);
            TSource[] array = await source.Page(index, size).ToArrayAsync(token).ConfigureAwait(false);
            TDestination[] items = array.Adapt<TSource[], TDestination[]>();
            return new PaginationListWrapper<TDestination, TDestination[]>(items, index, size, count);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<TCollection> PaginationAsync<TSource, TDestination, TCollection>(IQueryable<TSource> source, Int32 index, Int32 size, Func<IPaginationList<TDestination, TDestination[]>, TCollection> converter) where TCollection : class, IEnumerable<TDestination>
        {
            return PaginationAsync(source, index, size, converter, CancellationToken.None);
        }

        public static async Task<TCollection> PaginationAsync<TSource, TDestination, TCollection>(IQueryable<TSource> source, Int32 index, Int32 size, Func<IPaginationList<TDestination, TDestination[]>, TCollection> converter, CancellationToken token) where TCollection : class, IEnumerable<TDestination>
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (converter is null)
            {
                throw new ArgumentNullException(nameof(converter));
            }
            
            IPaginationList<TDestination, TDestination[]> result = await PaginationAsync<TSource, TDestination>(source, index, size, token).ConfigureAwait(false);
            return converter(result);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<TCollection> PaginationAsync<TSource, TDestination, TCollection>(this IQueryable<TSource> source, Int32 index, Int32 size, Func<IPaginationList<TDestination, TDestination[]>, Task<TCollection>> converter) where TCollection : class, IEnumerable<TDestination>
        {
            return PaginationAsync(source, index, size, converter, CancellationToken.None);
        }

        public static async Task<TCollection> PaginationAsync<TSource, TDestination, TCollection>(this IQueryable<TSource> source, Int32 index, Int32 size, Func<IPaginationList<TDestination, TDestination[]>, Task<TCollection>> converter, CancellationToken token) where TCollection : class, IEnumerable<TDestination>
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (converter is null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            IPaginationList<TDestination, TDestination[]> result = await PaginationAsync<TSource, TDestination>(source, index, size, token).ConfigureAwait(false);
            return await converter(result).ConfigureAwait(false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<TCollection> PaginationAsync<TSource, TDestination, TCollection>(this IQueryable<TSource> source, Int32 index, Int32 size, Func<IPaginationList<TDestination, TDestination[]>, ValueTask<TCollection>> converter) where TCollection : class, IEnumerable<TDestination>
        {
            return PaginationAsync(source, index, size, converter, CancellationToken.None);
        }

        public static async Task<TCollection> PaginationAsync<TSource, TDestination, TCollection>(this IQueryable<TSource> source, Int32 index, Int32 size, Func<IPaginationList<TDestination, TDestination[]>, ValueTask<TCollection>> converter, CancellationToken token) where TCollection : class, IEnumerable<TDestination>
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (converter is null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            IPaginationList<TDestination, TDestination[]> result = await PaginationAsync<TSource, TDestination>(source, index, size, token).ConfigureAwait(false);
            return await converter(result).ConfigureAwait(false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IPaginationList<TDestination, TDestination[]>> PaginationProjectToTypeAsync<TSource, TDestination>(this IQueryable<TSource> source, Int32 index, Int32 size)
        {
            return PaginationProjectToTypeAsync<TSource, TDestination>(source, index, size, CancellationToken.None);
        }

        public static async Task<IPaginationList<TDestination, TDestination[]>> PaginationProjectToTypeAsync<TSource, TDestination>(this IQueryable<TSource> source, Int32 index, Int32 size, CancellationToken token)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            Int32 count = await source.CountAsync(token).ConfigureAwait(false);
            TDestination[] items = await source.Page(index, size).ProjectToType<TDestination>().ToArrayAsync(token).ConfigureAwait(false);
            return new PaginationListWrapper<TDestination, TDestination[]>(items, index, size, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<TCollection> PaginationProjectToTypeAsync<TSource, TDestination, TCollection>(this IQueryable<TSource> source, Int32 index, Int32 size, Func<IPaginationList<TDestination, TDestination[]>, TCollection> converter) where TCollection : class, IEnumerable<TDestination>
        {
            return PaginationProjectToTypeAsync(source, index, size, converter, CancellationToken.None);
        }

        public static async Task<TCollection> PaginationProjectToTypeAsync<TSource, TDestination, TCollection>(this IQueryable<TSource> source, Int32 index, Int32 size, Func<IPaginationList<TDestination, TDestination[]>, TCollection> converter, CancellationToken token) where TCollection : class, IEnumerable<TDestination>
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (converter is null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            IPaginationList<TDestination, TDestination[]> result = await PaginationProjectToTypeAsync<TSource, TDestination>(source, index, size, token).ConfigureAwait(false);
            return converter(result);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<TCollection> PaginationProjectToTypeAsync<TSource, TDestination, TCollection>(this IQueryable<TSource> source, Int32 index, Int32 size, Func<IPaginationList<TDestination, TDestination[]>, Task<TCollection>> converter) where TCollection : class, IEnumerable<TDestination>
        {
            return PaginationProjectToTypeAsync(source, index, size, converter, CancellationToken.None);
        }

        public static async Task<TCollection> PaginationProjectToTypeAsync<TSource, TDestination, TCollection>(this IQueryable<TSource> source, Int32 index, Int32 size, Func<IPaginationList<TDestination, TDestination[]>, Task<TCollection>> converter, CancellationToken token) where TCollection : class, IEnumerable<TDestination>
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (converter is null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            IPaginationList<TDestination, TDestination[]> result = await PaginationProjectToTypeAsync<TSource, TDestination>(source, index, size, token).ConfigureAwait(false);
            return await converter(result).ConfigureAwait(false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<TCollection> PaginationProjectToTypeAsync<TSource, TDestination, TCollection>(this IQueryable<TSource> source, Int32 index, Int32 size, Func<IPaginationList<TDestination, TDestination[]>, ValueTask<TCollection>> converter) where TCollection : class, IEnumerable<TDestination>
        {
            return PaginationProjectToTypeAsync(source, index, size, converter, CancellationToken.None);
        }

        public static async Task<TCollection> PaginationProjectToTypeAsync<TSource, TDestination, TCollection>(this IQueryable<TSource> source, Int32 index, Int32 size, Func<IPaginationList<TDestination, TDestination[]>, ValueTask<TCollection>> converter, CancellationToken token) where TCollection : class, IEnumerable<TDestination>
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (converter is null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            IPaginationList<TDestination, TDestination[]> result = await PaginationProjectToTypeAsync<TSource, TDestination>(source, index, size, token).ConfigureAwait(false);
            return await converter(result).ConfigureAwait(false);
        }
    }
}