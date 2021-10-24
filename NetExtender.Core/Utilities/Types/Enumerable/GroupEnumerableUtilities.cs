// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;

namespace NetExtender.Utilities.Types
{
    public static partial class EnumerableUtilities
    {
        public static IEnumerable<IGrouping<T, Int32>> CountGroup<T>(this IEnumerable<T> source) where T : notnull
        {
            return CountGroup(source, null);
        }

        public static IEnumerable<IGrouping<T, Int32>> CountGroup<T>(this IEnumerable<T> source, IEqualityComparer<T>? comparer) where T : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            IDictionary<T, Int32> counter = new Dictionary<T, Int32>(comparer);
            
            foreach (T item in source)
            {
                if (counter.ContainsKey(item))
                {
                    counter[item]++;
                    continue;
                }

                counter[item] = 1;
            }

            return counter.GroupManyByKey();
        }
        
        public static IEnumerable<IGrouping<TKey, Int32>> CountGroupBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector) where TKey : notnull
        {
            return CountGroupBy(source, selector, null);
        }

        public static IEnumerable<IGrouping<TKey, Int32>> CountGroupBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector, IEqualityComparer<TKey>? comparer) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.Select(selector).CountGroup(comparer);
        }
        
        public static IEnumerable<IGrouping<T, Int64>> LongCountGroup<T>(this IEnumerable<T> source) where T : notnull
        {
            return LongCountGroup(source, null);
        }

        public static IEnumerable<IGrouping<T, Int64>> LongCountGroup<T>(this IEnumerable<T> source, IEqualityComparer<T>? comparer) where T : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            IDictionary<T, Int64> counter = new Dictionary<T, Int64>(comparer);
            
            foreach (T item in source)
            {
                if (counter.ContainsKey(item))
                {
                    counter[item]++;
                    continue;
                }

                counter[item] = 1;
            }

            return counter.GroupManyByKey();
        }
        
        public static IEnumerable<IGrouping<TKey, Int64>> LongCountGroupBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector) where TKey : notnull
        {
            return LongCountGroupBy(source, selector, null);
        }

        public static IEnumerable<IGrouping<TKey, Int64>> LongCountGroupBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector, IEqualityComparer<TKey>? comparer) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.Select(selector).LongCountGroup(comparer);
        }
        
        public static IEnumerable<IGrouping<Type, T>> GroupByType<T>(this IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.WhereNotNull().GroupBy(item => item!.GetType());
        }
    }
}