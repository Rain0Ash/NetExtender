// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using NetExtender.Initializer.Types.Enumerables;

namespace NetExtender.Utilities.Types
{
    public static partial class EnumerableUtilities
    {
        public static IOrderedEnumerable<T> AsOrderedEnumerable<T>(this IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return new OrderedEnumerableWrapper<T>(source);
        }

        public static IOrderedEnumerable<T> OrderBy<T>(this IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return OrderBy(source, (IComparer<T>?) null);
        }

        public static IOrderedEnumerable<T> OrderBy<T>(this IEnumerable<T> source, IComparer<T>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.OrderBy(item => item, comparer);
        }
        
        public static IOrderedEnumerable<T> OrderBy<T>(this IEnumerable<T> source, Comparison<T> comparison)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (comparison is null)
            {
                throw new ArgumentNullException(nameof(comparison));
            }

            return source.OrderBy(item => item, comparison.ToComparer());
        }
        
        public static IOrderedEnumerable<T> OrderBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector, Comparison<TKey> comparison)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            if (comparison is null)
            {
                throw new ArgumentNullException(nameof(comparison));
            }

            return source.OrderBy(selector, comparison.ToComparer());
        }

        public static IOrderedEnumerable<T> OrderByDescending<T>(this IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return OrderByDescending(source, (IComparer<T>?) null);
        }

        public static IOrderedEnumerable<T> OrderByDescending<T>(this IEnumerable<T> source, IComparer<T>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.OrderByDescending(item => item, comparer);
        }
        
        public static IOrderedEnumerable<T> OrderByDescending<T>(this IEnumerable<T> source, Comparison<T> comparison)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (comparison is null)
            {
                throw new ArgumentNullException(nameof(comparison));
            }

            return source.OrderByDescending(item => item, comparison.ToComparer());
        }
        
        public static IOrderedEnumerable<T> OrderByDescending<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector, Comparison<TKey> comparison)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            if (comparison is null)
            {
                throw new ArgumentNullException(nameof(comparison));
            }

            return source.OrderByDescending(selector, comparison.ToComparer());
        }
        
        public static IOrderedEnumerable<T> ThenBy<T>(this IOrderedEnumerable<T> source, IComparer<T>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.ThenBy(item => item, comparer);
        }
        
        public static IOrderedEnumerable<T> ThenBy<T>(this IOrderedEnumerable<T> source, Comparison<T> comparison)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (comparison is null)
            {
                throw new ArgumentNullException(nameof(comparison));
            }

            return source.ThenBy(item => item, comparison.ToComparer());
        }

        public static IOrderedEnumerable<T> ThenByDescending<T>(this IOrderedEnumerable<T> source, IComparer<T>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.ThenByDescending(item => item, comparer);
        }

        public static IOrderedEnumerable<T> ThenByDescending<T>(this IOrderedEnumerable<T> source, Comparison<T> comparison)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (comparison is null)
            {
                throw new ArgumentNullException(nameof(comparison));
            }

            return source.ThenByDescending(item => item, comparison.ToComparer());
        }

        public static IOrderedEnumerable<T> Sort<T>(this IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return Sort(source, (IComparer<T>?) null);
        }

        public static IOrderedEnumerable<T> Sort<T>(this IEnumerable<T> source, IComparer<T>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return OrderBy(source, comparer);
        }

        public static IOrderedEnumerable<T> Sort<T>(this IEnumerable<T> source, Comparison<T> comparison)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (comparison is null)
            {
                throw new ArgumentNullException(nameof(comparison));
            }

            return OrderBy(source, comparison);
        }

        public static IOrderedEnumerable<T> SortBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.OrderBy(selector);
        }

        public static IOrderedEnumerable<T> SortBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector, IComparer<TKey>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.OrderBy(selector, comparer);
        }
        
        public static IOrderedEnumerable<T> SortBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector, Comparison<TKey> comparison)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            if (comparison is null)
            {
                throw new ArgumentNullException(nameof(comparison));
            }

            return source.OrderBy(selector, comparison.ToComparer());
        }

        public static IOrderedEnumerable<T> SortDescending<T>(this IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return SortDescending(source, (IComparer<T>?) null);
        }

        public static IOrderedEnumerable<T> SortDescending<T>(this IEnumerable<T> source, IComparer<T>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return OrderByDescending(source, comparer);
        }

        public static IOrderedEnumerable<T> SortDescending<T>(this IEnumerable<T> source, Comparison<T> comparison)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (comparison is null)
            {
                throw new ArgumentNullException(nameof(comparison));
            }

            return OrderByDescending(source, comparison);
        }

        public static IOrderedEnumerable<T> SortByDescending<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.OrderByDescending(selector);
        }

        public static IOrderedEnumerable<T> SortByDescending<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector, IComparer<TKey>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.OrderByDescending(selector, comparer);
        }

        public static IOrderedEnumerable<T> SortByDescending<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector, Comparison<TKey> comparison)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            if (comparison is null)
            {
                throw new ArgumentNullException(nameof(comparison));
            }

            return source.OrderByDescending(selector, comparison.ToComparer());
        }
    }
}