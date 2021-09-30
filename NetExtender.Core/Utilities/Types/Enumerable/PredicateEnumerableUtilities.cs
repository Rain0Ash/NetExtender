// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using NetExtender.Utilities.Numerics;

namespace NetExtender.Utilities.Types
{
    public static partial class EnumerableUtilities
    {
        public static IEnumerable<T> WhereIs<T, TCheck>(this IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            foreach (T item in source)
            {
                if (item is TCheck)
                {
                    yield return item;
                }
            }
        }
        
        public static IEnumerable<T> WhereIs<T>(this IEnumerable<T> source, Type type)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            foreach (T item in source)
            {
                if (item is not null && item.GetType() == type)
                {
                    yield return item;
                }
            }
        }

        public static IEnumerable<T> WhereNot<T>(this IEnumerable<T> source, Func<T, Int32, Boolean> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return source.Where((item, index) => !predicate(item, index));
        }

        public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T?> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(item => item is not null)!;
        }

        public static IEnumerable<T?> WhereNotNull<T>(this IEnumerable<T?> source) where T : struct
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(item => item.HasValue);
        }

        public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T> source, Func<T, Boolean> predicate)
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
        
        public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T> source, Func<T, Int32, Boolean> predicate)
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
        
        public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> source, Func<T, Boolean> predicate, Boolean condition)
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
        
        public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> source, Func<T, Int32, Boolean> predicate, Boolean condition)
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
        
        public static IEnumerable<T> WhereIfNot<T>(this IEnumerable<T> source, Func<T, Boolean> predicate, Boolean condition)
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
        
        public static IEnumerable<T> WhereIfNot<T>(this IEnumerable<T> source, Func<T, Int32, Boolean> predicate, Boolean condition)
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

        public static IEnumerable<T> WhereNot<T>(this IEnumerable<T> source, Func<T, Boolean> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return source.Where(item => !predicate(item));
        }

        public static IEnumerable<T> WhereSame<T>(this IEnumerable<T> source)
        {
            return WhereSame(source, null);
        }

        public static IEnumerable<T> WhereSame<T>(this IEnumerable<T> source, IEqualityComparer<T>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            using IEnumerator<T> enumerator = source.GetEnumerator();

            if (!enumerator.MoveNext())
            {
                yield break;
            }
            
            comparer ??= EqualityComparer<T>.Default;
            T first = enumerator.Current;
            yield return first;

            while (enumerator.MoveNext())
            {
                T item = enumerator.Current;
                if (comparer.Equals(first, item))
                {
                    yield return item;
                }
            }
        }
        
        public static IEnumerable<T> WhereSameBy<T, TComparable>(this IEnumerable<T> source, Func<T, TComparable> selector)
        {
            return WhereSameBy(source, selector, (IEqualityComparer<TComparable>?) null);
        }

        public static IEnumerable<T> WhereSameBy<T, TComparable>(this IEnumerable<T> source, Func<T, TComparable> selector, IEqualityComparer<TComparable>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            using IEnumerator<T> enumerator = source.GetEnumerator();

            if (!enumerator.MoveNext())
            {
                yield break;
            }
            
            comparer ??= EqualityComparer<TComparable>.Default;
            TComparable comparable = selector(enumerator.Current);
            yield return enumerator.Current;

            while (enumerator.MoveNext())
            {
                T item = enumerator.Current;
                if (comparer.Equals(comparable, selector(item)))
                {
                    yield return item;
                }
            }
        }
        
        public static IEnumerable<T> WhereSameBy<T, TComparable>(this IEnumerable<T> source, T sample, Func<T, TComparable> selector)
        {
            return WhereSameBy(source, sample, selector, null);
        }

        public static IEnumerable<T> WhereSameBy<T, TComparable>(this IEnumerable<T> source, T sample, Func<T, TComparable> selector, IEqualityComparer<TComparable>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (sample is null)
            {
                throw new ArgumentNullException(nameof(sample));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return WhereSameBy(source, selector(sample), selector, comparer);
        }
        
        public static IEnumerable<T> WhereSameBy<T, TComparable>(this IEnumerable<T> source, TComparable sample, Func<T, TComparable> selector)
        {
            return WhereSameBy(source, sample, selector, null);
        }

        public static IEnumerable<T> WhereSameBy<T, TComparable>(this IEnumerable<T> source, TComparable sample, Func<T, TComparable> selector, IEqualityComparer<TComparable>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }
            
            comparer ??= EqualityComparer<TComparable>.Default;
            
            using IEnumerator<T> enumerator = source.GetEnumerator();

            while (enumerator.MoveNext())
            {
                T item = enumerator.Current;
                if (comparer.Equals(sample, selector(item)))
                {
                    yield return item;
                }
            }
        }
        
        public static IEnumerable<T> WhereNotSame<T>(this IEnumerable<T> source)
        {
            return WhereNotSame(source, null);
        }

        public static IEnumerable<T> WhereNotSame<T>(this IEnumerable<T> source, IEqualityComparer<T>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            using IEnumerator<T> enumerator = source.GetEnumerator();

            if (!enumerator.MoveNext())
            {
                yield break;
            }
            
            comparer ??= EqualityComparer<T>.Default;
            T first = enumerator.Current;
            yield return first;

            while (enumerator.MoveNext())
            {
                T item = enumerator.Current;
                if (!comparer.Equals(first, item))
                {
                    yield return item;
                }
            }
        }
        
        public static IEnumerable<T> WhereNotSameBy<T, TComparable>(this IEnumerable<T> source, Func<T, TComparable> selector)
        {
            return WhereNotSameBy(source, selector, (IEqualityComparer<TComparable>?) null);
        }

        public static IEnumerable<T> WhereNotSameBy<T, TComparable>(this IEnumerable<T> source, Func<T, TComparable> selector, IEqualityComparer<TComparable>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            using IEnumerator<T> enumerator = source.GetEnumerator();

            if (!enumerator.MoveNext())
            {
                yield break;
            }
            
            comparer ??= EqualityComparer<TComparable>.Default;
            TComparable comparable = selector(enumerator.Current);
            yield return enumerator.Current;

            while (enumerator.MoveNext())
            {
                T item = enumerator.Current;
                if (!comparer.Equals(comparable, selector(item)))
                {
                    yield return item;
                }
            }
        }
        
        public static IEnumerable<T> WhereNotSameBy<T, TComparable>(this IEnumerable<T> source, T sample, Func<T, TComparable> selector)
        {
            return WhereNotSameBy(source, sample, selector, null);
        }

        public static IEnumerable<T> WhereNotSameBy<T, TComparable>(this IEnumerable<T> source, T sample, Func<T, TComparable> selector, IEqualityComparer<TComparable>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (sample is null)
            {
                throw new ArgumentNullException(nameof(sample));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return WhereNotSameBy(source, selector(sample), selector, comparer);
        }
        
        public static IEnumerable<T> WhereNotSameBy<T, TComparable>(this IEnumerable<T> source, TComparable sample, Func<T, TComparable> selector)
        {
            return WhereNotSameBy(source, sample, selector, null);
        }

        public static IEnumerable<T> WhereNotSameBy<T, TComparable>(this IEnumerable<T> source, TComparable sample, Func<T, TComparable> selector, IEqualityComparer<TComparable>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }
            
            comparer ??= EqualityComparer<TComparable>.Default;
            
            using IEnumerator<T> enumerator = source.GetEnumerator();

            while (enumerator.MoveNext())
            {
                T item = enumerator.Current;
                if (!comparer.Equals(sample, selector(item)))
                {
                    yield return item;
                }
            }
        }
        
        public static IEnumerable<Int32> IndexWhere<T>(this IEnumerable<T> source, Func<T, Boolean> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            Int32 index = 0;
            foreach (T item in source)
            {
                if (predicate(item))
                {
                    yield return index;
                }

                ++index;
            }
        }

        public static IEnumerable<Int32> IndexWhere<T>(this IEnumerable<T> source, Func<Int32, T, Boolean> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            Int32 index = 0;
            foreach (T item in source)
            {
                if (predicate(index, item))
                {
                    yield return index;
                }

                ++index;
            }
        }

        public static IEnumerable<(Int32 index, T item)> IndexItemWhere<T>(this IEnumerable<T> source, Func<T, Boolean> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            Int32 index = 0;
            foreach (T item in source)
            {
                if (predicate(item))
                {
                    yield return (index, item);
                }

                ++index;
            }
        }

        public static IEnumerable<(Int32 index, T item)> IndexItemWhere<T>(this IEnumerable<T> source, Func<Int32, T, Boolean> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            Int32 index = 0;
            foreach (T item in source)
            {
                if (predicate(index, item))
                {
                    yield return (index, item);
                }

                ++index;
            }
        }

        /// <inheritdoc cref="Every{T}(System.Collections.Generic.IEnumerable{T},int,bool)"/>>
        public static IEnumerable<T> Every<T>(this IEnumerable<T> source, Int32 every)
        {
            return Every(source, every, false);
        }

        /// <summary>
        /// Return first item and then every n-th item
        /// </summary>
        /// <param name="source">Source</param>
        /// <param name="every">Every n-th item</param>
        /// <param name="first">Start from first value</param>
        /// <typeparam name="T">Type</typeparam>
        /// <returns></returns>
        public static IEnumerable<T> Every<T>(this IEnumerable<T> source, Int32 every, Boolean first)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (every <= 1)
            {
                return source;
            }

            Int32 counter = first ? 0 : 1;
            return source.Where(_ => counter++ % every == 0);
        }

        public static IEnumerable<T> Except<T>(this IEnumerable<T> source, ISet<T> except)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (except is null)
            {
                throw new ArgumentNullException(nameof(except));
            }

            foreach (T item in source)
            {
                if (except.Contains(item))
                {
                    continue;
                }

                yield return item;
            }
        }
        
        public static IEnumerable<T> WhereCompareInRange<T>(this IEnumerable<T> source, T minimum, T maximum, IComparer<T>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(item => item.CompareInRange(minimum, maximum, comparer));
        }

        public static IEnumerable<T> WhereCompareInRange<T>(this IEnumerable<T> source, T minimum, T maximum, MathPositionType comparison) where T : IComparable<T>
        {
            return WhereCompareInRange(source, minimum, maximum, Comparer<T>.Default, comparison);
        }

        public static IEnumerable<T> WhereCompareInRange<T>(this IEnumerable<T> source, T minimum, T maximum, IComparer<T>? comparer, MathPositionType comparison)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(item => item.CompareInRange(minimum, maximum, comparer, comparison));
        }
        
        public static IEnumerable<T> WhereCompareInRangeBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector, TKey minimum, TKey maximum) where TKey : IComparable<TKey>
        {
            return WhereCompareInRangeBy(source, selector, minimum, maximum, Comparer<TKey>.Default);
        }

        public static IEnumerable<T> WhereCompareInRangeBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector, TKey minimum, TKey maximum, IComparer<TKey>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            foreach (T item in source)
            {
                if (selector(item).CompareInRange(minimum, maximum, comparer))
                {
                    yield return item;
                }
            }
        }

        public static IEnumerable<T> WhereCompareInRangeBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector, TKey minimum, TKey maximum, MathPositionType comparison) where TKey : IComparable<TKey>
        {
            return WhereCompareInRangeBy(source, selector, minimum, maximum, Comparer<TKey>.Default, comparison);
        }

        public static IEnumerable<T> WhereCompareInRangeBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector, TKey minimum, TKey maximum, IComparer<TKey>? comparer, MathPositionType comparison)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            foreach (T item in source)
            {
                if (selector(item).CompareInRange(minimum, maximum, comparer, comparison))
                {
                    yield return item;
                }
            }
        }
        
        /// <summary>
        /// Returns all elements of <paramref name="source"/> without <paramref name="without"/> using the specified equality comparer to compare values.
        /// Does not throw an exception if <paramref name="source"/> does not contain <paramref name="without"/>.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{TSource}"/> to remove the specified elements from.</param>
        /// <param name="without">The elements to remove.</param>
        /// <returns>
        /// All elements of <paramref name="source"/> except <paramref name="without"/>.
        /// </returns>
        public static IEnumerable<T> Without<T>(IEnumerable<T> source, params T[] without)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return Without(source, without, null);
        }

        /// <summary>
        /// Returns all elements of <paramref name="source"/> without <paramref name="without"/> using the specified equality comparer to compare values.
        /// Does not throw an exception if <paramref name="source"/> does not contain <paramref name="without"/>.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{TSource}"/> to remove the specified elements from.</param>
        /// <param name="without">The elements to remove.</param>
        /// <param name="comparer">The equality comparer to use.</param>
        /// <returns>
        /// All elements of <paramref name="source"/> except <paramref name="without"/>.
        /// </returns>
        public static IEnumerable<T> Without<T>(IEnumerable<T> source, IEqualityComparer<T>? comparer, params T[] without)
        {
            return Without(source, without, comparer);
        }

        public static IEnumerable<T> Without<T>(IEnumerable<T> source, IEnumerable<T> without)
        {
            return Without(source, without, EqualityComparer<T>.Default);
        }

        /// <summary>
        /// Returns all elements of <paramref name="source"/> without <paramref name="without"/> using the specified equality comparer to compare values.
        /// Does not throw an exception if <paramref name="source"/> does not contain <paramref name="without"/>.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{TSource}"/> to remove the specified elements from.</param>
        /// <param name="without">The elements to remove.</param>
        /// <param name="comparer">The equality comparer to use.</param>
        /// <returns>
        /// All elements of <paramref name="source"/> except <paramref name="without"/>.
        /// </returns>
        public static IEnumerable<T> Without<T>(IEnumerable<T> source, IEnumerable<T> without, IEqualityComparer<T>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (without is null)
            {
                throw new ArgumentNullException(nameof(without));
            }

            HashSet<T> remove = new HashSet<T>(without, comparer);

            return source.Where(item => !remove.Contains(item));
        }
    }
}