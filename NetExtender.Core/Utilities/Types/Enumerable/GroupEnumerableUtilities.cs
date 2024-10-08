// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using NetExtender.Types.Dictionaries;
using NetExtender.Types.Monads;

namespace NetExtender.Utilities.Types
{
    public static partial class EnumerableUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Range[] LongestSubsequence<T>(this IEnumerable<T> source, T predicate)
        {
            return LongestSubsequence(source, item => EqualityComparer<T>.Default.Equals(item, predicate));
        }

        public static Range[] LongestSubsequence<T>(this IEnumerable<T> source, Predicate<T> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            List<Range> values = new List<Range>();

            Int32 index = 0;
            Int32 count = 0;
            foreach (T item in source)
            {
                ++index;
                if (predicate(item))
                {
                    ++count;
                    continue;
                }

                if (count <= 0)
                {
                    continue;
                }

                values.Add((index - count - 1)..(index - 1));
                count = 0;
            }

            if (count > 0)
            {
                values.Add((index - count - 1)..(index - 1));
            }

            values.Sort(RangeUtilities.CompareTo);
            return values.ToArray();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IGrouping<TKey, Int64>> LongCountGroupBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector) where TKey : notnull
        {
            return LongCountGroupBy(source, selector, null);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IGrouping<Type, T>> GroupByType<T>(this IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.WhereNotNull().GroupBy(item => item!.GetType());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IGrouping<TKey, TSource>> WhereKeyNotNull<TKey, TSource>(this IEnumerable<IGrouping<TKey?, TSource>?> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            return source.WhereNotNull().Where(static group => group.Key is not null)!;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IGrouping<TKey, TSource>> GroupNotNullBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey?> keySelector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            if (keySelector is null)
            {
                throw new ArgumentNullException(nameof(keySelector));
            }
            
            return source.GroupBy(keySelector).WhereKeyNotNull();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IGrouping<TKey, TSource>> GroupNotNullBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey?> keySelector, IEqualityComparer<TKey?>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            if (keySelector is null)
            {
                throw new ArgumentNullException(nameof(keySelector));
            }
            
            return source.GroupBy(keySelector, comparer).WhereKeyNotNull();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IGrouping<TKey, TElement>> GroupNotNullBy<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey?> keySelector, Func<TSource, TElement> elementSelector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            if (keySelector is null)
            {
                throw new ArgumentNullException(nameof(keySelector));
            }
            
            if (elementSelector is null)
            {
                throw new ArgumentNullException(nameof(elementSelector));
            }
            
            return source.GroupBy(keySelector, elementSelector).WhereKeyNotNull();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IGrouping<TKey, TElement>> GroupNotNullBy<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey?> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            if (keySelector is null)
            {
                throw new ArgumentNullException(nameof(keySelector));
            }
            
            if (elementSelector is null)
            {
                throw new ArgumentNullException(nameof(elementSelector));
            }
            
            return source.GroupBy(keySelector!, elementSelector, comparer).WhereKeyNotNull();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TResult> GroupNotNullBy<TSource, TKey, TResult>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TKey, IEnumerable<TSource>, TResult> resultSelector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            if (keySelector is null)
            {
                throw new ArgumentNullException(nameof(keySelector));
            }
            
            if (resultSelector is null)
            {
                throw new ArgumentNullException(nameof(resultSelector));
            }

            return source.GroupBy(keySelector, resultSelector).WhereNotNull();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TResult> GroupNotNullBy<TSource, TKey, TElement, TResult>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, Func<TKey, IEnumerable<TElement>, TResult> resultSelector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            if (keySelector is null)
            {
                throw new ArgumentNullException(nameof(keySelector));
            }
            
            if (elementSelector is null)
            {
                throw new ArgumentNullException(nameof(elementSelector));
            }
            
            if (resultSelector is null)
            {
                throw new ArgumentNullException(nameof(resultSelector));
            }

            return source.GroupBy(keySelector, elementSelector, resultSelector).WhereNotNull();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TResult> GroupNotNullBy<TSource, TKey, TResult>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TKey, IEnumerable<TSource>, TResult> resultSelector, IEqualityComparer<TKey>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            if (keySelector is null)
            {
                throw new ArgumentNullException(nameof(keySelector));
            }
            
            if (resultSelector is null)
            {
                throw new ArgumentNullException(nameof(resultSelector));
            }
            
            return source.GroupBy(keySelector, resultSelector, comparer).WhereNotNull();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TResult> GroupNotNullBy<TSource, TKey, TElement, TResult>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, Func<TKey, IEnumerable<TElement>, TResult> resultSelector, IEqualityComparer<TKey>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            if (keySelector is null)
            {
                throw new ArgumentNullException(nameof(keySelector));
            }
            
            if (elementSelector is null)
            {
                throw new ArgumentNullException(nameof(elementSelector));
            }
            
            if (resultSelector is null)
            {
                throw new ArgumentNullException(nameof(resultSelector));
            }
            
            return source.GroupBy(keySelector, elementSelector, resultSelector, comparer).WhereNotNull();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Dictionary<TKey, List<TValue>> ToDictionary<TKey, TValue>(this IEnumerable<IGrouping<TKey, TValue>> source) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.ToDictionary(static group => group.Key, static group => new List<TValue>(group));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Dictionary<TKey, List<TValue>> ToDictionary<TKey, TValue>(this IEnumerable<IGrouping<TKey, TValue>> source, IEqualityComparer<TKey>? comparer) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.ToDictionary(static group => group.Key, static group => new List<TValue>(group), comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static NullableDictionary<TKey, List<TValue>> ToNullableDictionary<TKey, TValue>(this IEnumerable<IGrouping<TKey, TValue>> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.ToNullableDictionary(static group => group.Key, static group => new List<TValue>(group));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static NullableDictionary<TKey, List<TValue>> ToNullableDictionary<TKey, TValue>(this IEnumerable<IGrouping<TKey, TValue>> source, IEqualityComparer<TKey>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.ToNullableDictionary(static group => group.Key, static group => new List<TValue>(group), comparer);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static NullableDictionary<TKey, List<TValue>> ToNullableDictionary<TKey, TValue>(this IEnumerable<IGrouping<TKey, TValue>> source, IEqualityComparer<NullMaybe<TKey>>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.ToNullableDictionary(static group => group.Key, static group => new List<TValue>(group), comparer);
        }
    }
}