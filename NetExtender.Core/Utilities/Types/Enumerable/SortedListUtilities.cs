// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace NetExtender.Utilities.Types
{
    
    [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
    public static class SortedListUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SortedList<TKey, TValue> ToSortedList<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source) where TKey : notnull
        {
            return ToSortedList(source, null);
        }
        
        public static SortedList<TKey, TValue> ToSortedList<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, IComparer<TKey>? comparer) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            SortedList<TKey, TValue> sorted = source.CountIfMaterialized() is Int32 count ? new SortedList<TKey, TValue>(count, comparer) : new SortedList<TKey, TValue>(comparer);
            sorted.AddRange(source);
            sorted.TrimExcess();
            
            return sorted;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SortedList<TKey, TValue> ToSortedList<TKey, TValue>(this IEnumerable<(TKey, TValue)> source) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.ToKeyValuePairs().ToSortedList();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SortedList<TKey, TValue> ToSortedList<TKey, TValue>(this IEnumerable<(TKey, TValue)> source, IComparer<TKey>? comparer) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.ToKeyValuePairs().ToSortedList(comparer);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SortedList<TKey, TSource> ToSortedList<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector) where TKey : notnull
        {
            return ToSortedList(source, keySelector, null);
        }
        
        public static SortedList<TKey, TSource> ToSortedList<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey>? comparer) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (keySelector is null)
            {
                throw new ArgumentNullException(nameof(keySelector));
            }

            SortedList<TKey, TSource> sorted = source.CountIfMaterialized() is Int32 count ? new SortedList<TKey, TSource>(count, comparer) : new SortedList<TKey, TSource>(comparer);
            foreach (TSource item in source)
            {
                sorted.Add(keySelector(item), item);
            }

            sorted.TrimExcess();
            return sorted;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SortedList<TKey, TElement> ToSortedList<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector) where TKey : notnull
        {
            return ToSortedList(source, keySelector, elementSelector, null);
        }
        
        public static SortedList<TKey, TElement> ToSortedList<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IComparer<TKey>? comparer) where TKey : notnull
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

            SortedList<TKey, TElement> sorted = source.CountIfMaterialized() is Int32 count ? new SortedList<TKey, TElement>(count, comparer) : new SortedList<TKey, TElement>(comparer);
            foreach (TSource item in source)
            {
                sorted.Add(keySelector(item), elementSelector(item));
            }

            sorted.TrimExcess();
            return sorted;
        }
        
        private static class SortedListBinarySearchExpression<TKey, TValue> where TKey : notnull
        {
            private static Func<SortedList<TKey, TValue>, TKey[]> Keys { get; }

            static SortedListBinarySearchExpression()
            {
                ParameterExpression parameter = Expression.Parameter(typeof(SortedList<TKey, TValue>));
                MemberExpression member = Expression.Field(parameter, "keys");
                LambdaExpression lambda = Expression.Lambda(typeof(Func<SortedList<TKey, TValue>, TKey[]>), member, parameter);
                Keys = (Func<SortedList<TKey, TValue>, TKey[]>) lambda.Compile();
            }
            
            [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
            public static Int32 BinarySearch(SortedList<TKey, TValue> collection, TKey value)
            {
                return Array.BinarySearch(Keys.Invoke(collection), 0, collection.Count, value, collection.Comparer);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static Int32 BinarySearch<TKey, TValue>(this SortedList<TKey, TValue> collection, TKey value) where TKey : notnull
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            return SortedListBinarySearchExpression<TKey, TValue>.BinarySearch(collection, value);
        }
    }
}