// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using Mapster;
using NetExtender.Types.Lists;
using NetExtender.Types.Lists.Interfaces;

namespace NetExtender.Utilities.Types
{
    public static class MapsterEnumerableUtilities
    {
        public static IPaginationList<TDestination, TDestination[]> Pagination<TSource, TDestination>(this IEnumerable<TSource> source, Int32 index, Int32 size)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            TSource[] array = source.Page(index, size).ToArray();
            TDestination[] items = array.Adapt<TSource[], TDestination[]>();
            return new PaginationListWrapper<TDestination, TDestination[]>(items, index, size, array.Length);
        }
        
        public static TCollection Pagination<TSource, TDestination, TCollection>(this IEnumerable<TSource> source, Int32 index, Int32 size, Func<IPaginationList<TDestination, TDestination[]>, TCollection> converter) where TCollection : class, ICollection<TDestination>
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (converter is null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            IPaginationList<TDestination, TDestination[]> result = Pagination<TSource, TDestination>(source, index, size);
            return converter(result);
        }
    }
}