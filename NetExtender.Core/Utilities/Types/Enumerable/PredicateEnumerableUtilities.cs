// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using NetExtender.Types.Counters;
using NetExtender.Types.Queues;
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

        public static IEnumerable<T> WhereBy<T, TResult>(this IEnumerable<T> source, Func<T, TResult> selector, Func<TResult, Boolean> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return source.Where(item => predicate(selector(item)));
        }

        public static IEnumerable<T> WhereBy<T, TResult>(this IEnumerable<T> source, Func<T, Int32, TResult> selector, Func<TResult, Boolean> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return source.Where((item, index) => predicate(selector(item, index)));
        }

        public static IEnumerable<T> WhereBy<T, TResult>(this IEnumerable<T> source, Func<T, TResult> selector, Func<TResult, Int32, Boolean> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return source.Where((item, index) => predicate(selector(item), index));
        }

        public static IEnumerable<T> WhereBy<T, TResult>(this IEnumerable<T> source, Func<T, Int32, TResult> selector, Func<TResult, Int32, Boolean> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return source.Where((item, index) => predicate(selector(item, index), index));
        }

        public static IEnumerable<T> WhereNotBy<T, TResult>(this IEnumerable<T> source, Func<T, TResult> selector, Func<TResult, Boolean> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return source.Where(item => !predicate(selector(item)));
        }

        public static IEnumerable<T> WhereNotBy<T, TResult>(this IEnumerable<T> source, Func<T, Int32, TResult> selector, Func<TResult, Boolean> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return source.Where((item, index) => !predicate(selector(item, index)));
        }

        public static IEnumerable<T> WhereNotBy<T, TResult>(this IEnumerable<T> source, Func<T, TResult> selector, Func<TResult, Int32, Boolean> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return source.Where((item, index) => !predicate(selector(item), index));
        }

        public static IEnumerable<T> WhereNotBy<T, TResult>(this IEnumerable<T> source, Func<T, Int32, TResult> selector, Func<TResult, Int32, Boolean> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return source.Where((item, index) => !predicate(selector(item, index), index));
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

            return source.Where(static item => item is not null)!;
        }

        public static IEnumerable<T?> WhereNotNull<T>(this IEnumerable<T?> source) where T : struct
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(static item => item.HasValue);
        }

        public static IEnumerable<T> WhereNotNull<T, TItem>(this IEnumerable<T?> source, Func<T, TItem> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            foreach (T? item in source)
            {
                if (item is not null && predicate(item) is not null)
                {
                    yield return item;
                }
            }
        }

        public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T?> source, Func<T, Boolean> predicate)
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

        public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T?> source, Func<T, Int32, Boolean> predicate)
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

        public static IEnumerable<Int32> IndexWhere<T>(this IEnumerable<T> source, Func<T, Int32, Boolean> predicate)
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
                if (predicate(item, index))
                {
                    yield return index;
                }

                ++index;
            }
        }

        public static IEnumerable<(T Item, Int32 Index)> IndexItemWhere<T>(this IEnumerable<T> source, Func<T, Boolean> predicate)
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
                    yield return (item, index);
                }

                ++index;
            }
        }

        public static IEnumerable<(T Item, Int32 Index)> IndexItemWhere<T>(this IEnumerable<T> source, Func<T, Int32, Boolean> predicate)
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
                if (predicate(item, index))
                {
                    yield return (item, index);
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
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> Without<T>(IEnumerable<T> source, params T[] without)
        {
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> Without<T>(IEnumerable<T> source, IEqualityComparer<T>? comparer, params T[] without)
        {
            return Without(source, without, comparer);
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> IntersectNotUnique<T>(this IEnumerable<T> source, IEnumerable<T> other)
        {
            return IntersectNotUnique(source, other, null);
        }
        
        // ReSharper disable once CognitiveComplexity
        public static IEnumerable<T> IntersectNotUnique<T>(this IEnumerable<T> source, IEnumerable<T> other, IEqualityComparer<T>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }
            
            if (source.CountIfMaterialized(out Int32 first) && first <= 0 || other.CountIfMaterialized(out Int32 second) && second <= 0)
            {
                yield break;
            }
            
            Counter64<T> counter = new Counter64<T>(other, comparer);
            
            foreach (T item in source)
            {
                if (counter.Count <= 0)
                {
                    yield break;
                }
                
                if (counter.Remove(item))
                {
                    yield return item;
                }
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TSource> IntersectByNotUnique<TSource, TResult>(this IEnumerable<TSource> source, IEnumerable<TSource> other, Func<TSource, TResult> selector)
        {
            return IntersectByNotUnique(source, other, selector, null);
        }
        
        // ReSharper disable once CognitiveComplexity
        public static IEnumerable<TSource> IntersectByNotUnique<TSource, TResult>(this IEnumerable<TSource> source, IEnumerable<TSource> other, Func<TSource, TResult> selector, IEqualityComparer<TResult>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }
            
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }
            
            if (source.CountIfMaterialized(out Int32 first) && first <= 0 || other.CountIfMaterialized(out Int32 second) && second <= 0)
            {
                yield break;
            }
            
            Counter64<TResult> counter = new Counter64<TResult>(other.Select(selector), comparer);
            
            foreach (TSource item in source)
            {
                if (counter.Count <= 0)
                {
                    yield break;
                }
                
                if (counter.Remove(selector(item)))
                {
                    yield return item;
                }
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> ExceptNotUnique<T>(this IEnumerable<T> source, IEnumerable<T> other)
        {
            return ExceptNotUnique(source, other, null);
        }
        
        // ReSharper disable once CognitiveComplexity
        public static IEnumerable<T> ExceptNotUnique<T>(this IEnumerable<T> source, IEnumerable<T> other, IEqualityComparer<T>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }
            
            if (source.CountIfMaterialized(out Int32 first) && first <= 0)
            {
                yield break;
            }
            
            if (other.CountIfMaterialized(out Int32 second) && second <= 0)
            {
                foreach (T item in source)
                {
                    yield return item;
                }
                
                yield break;
            }
            
            static IEnumerable<T> Core(IEnumerable<T> source, IEnumerable<T> other, IEqualityComparer<T>? comparer)
            {
                Counter64<T> counter = new Counter64<T>(other, comparer);
                
                foreach (T item in source.Reverse())
                {
                    if (counter.Count <= 0 || !counter.Remove(item))
                    {
                        yield return item;
                    }
                }
            }
            
            foreach (T item in Core(source, other, comparer).Reverse())
            {
                yield return item;
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TSource> ExceptNotUnique<TSource, TResult>(this IEnumerable<TSource> source, IEnumerable<TSource> other, Func<TSource, TResult> selector)
        {
            return ExceptNotUnique(source, other, selector, null);
        }
        
        // ReSharper disable once CognitiveComplexity
        public static IEnumerable<TSource> ExceptNotUnique<TSource, TResult>(this IEnumerable<TSource> source, IEnumerable<TSource> other, Func<TSource, TResult> selector, IEqualityComparer<TResult>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }
            
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }
            
            if (source.CountIfMaterialized(out Int32 first) && first <= 0)
            {
                yield break;
            }
            
            if (other.CountIfMaterialized(out Int32 second) && second <= 0)
            {
                foreach (TSource item in source)
                {
                    yield return item;
                }
                
                yield break;
            }
            
            static IEnumerable<TSource> Core(IEnumerable<TSource> source, IEnumerable<TSource> other, Func<TSource, TResult> selector, IEqualityComparer<TResult>? comparer)
            {
                Counter64<TResult> counter = new Counter64<TResult>(other.Select(selector), comparer);
                
                foreach (TSource item in source.Reverse())
                {
                    if (counter.Count <= 0 || !counter.Remove(selector(item)))
                    {
                        yield return item;
                    }
                }
            }
            
            foreach (TSource item in Core(source, other, selector, comparer).Reverse())
            {
                yield return item;
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> SymmetricExceptNotUnique<T>(this IEnumerable<T> source, IEnumerable<T> other)
        {
            return SymmetricExceptNotUnique(source, other, null);
        }
        
        // ReSharper disable once CognitiveComplexity
        public static IEnumerable<T> SymmetricExceptNotUnique<T>(this IEnumerable<T> source, IEnumerable<T> other, IEqualityComparer<T>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }
            
            if (source.CountIfMaterialized(out Int32 first) && first <= 0)
            {
                foreach (T item in other)
                {
                    yield return item;
                }
                
                yield break;
            }
            
            if (other.CountIfMaterialized(out Int32 second) && second <= 0)
            {
                foreach (T item in source)
                {
                    yield return item;
                }
                
                yield break;
            }
            
            Counter64<T> scounter = new Counter64<T>(source = source.Materialize(), comparer);
            Counter64<T> ocounter = new Counter64<T>(other = other.Materialize(), comparer);
            
            foreach (T key in scounter.Keys.Union(ocounter.Keys, comparer))
            {
                scounter[key] = Math.Abs(scounter[key] - ocounter[key]);
            }
            
            ocounter.Clear();
            
            foreach (T item in source.Concat(other))
            {
                if (!scounter.TryGetValue(item, out Int64 total) || ocounter[item] >= total)
                {
                    continue;
                }
                
                ocounter.Add(item);
                yield return item;
            }
        }

        public static IEnumerable<T> Slice<T>(this IEnumerable<T> source, Int32 index, Int32 count)
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
        public static IEnumerable<T> Index<T>(this IEnumerable<T> source, Int32 size)
        {
            return Index(source, 0, size);
        }
        
        public static IEnumerable<T> Index<T>(this IEnumerable<T> source, Int32 index, Int32 size)
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
        public static IEnumerable<T> Page<T>(this IEnumerable<T> source, Int32 size)
        {
            return Page(source, 1, size);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> Page<T>(this IEnumerable<T> source, Int32 page, Int32 size)
        {
            return Index(source, page - 1, size);
        }

        public static IEnumerable<T> ThrowIf<T, TException>(this IEnumerable<T> source, Func<T, Boolean> predicate) where TException : Exception, new()
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            foreach (T item in source)
            {
                if (predicate(item))
                {
                    throw new TException();
                }

                yield return item;
            }
        }

        public static IEnumerable<T> ThrowIf<T, TException>(this IEnumerable<T> source, Func<T, Int32, Boolean> predicate) where TException : Exception, new()
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            Int32 counter = 0;
            foreach (T item in source)
            {
                if (predicate(item, counter++))
                {
                    throw new TException();
                }

                yield return item;
            }
        }

        public static IEnumerable<T> ThrowIfNot<T, TException>(this IEnumerable<T> source, Func<T, Boolean> predicate) where TException : Exception, new()
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            foreach (T item in source)
            {
                if (!predicate(item))
                {
                    throw new TException();
                }

                yield return item;
            }
        }

        public static IEnumerable<T> ThrowIfNot<T, TException>(this IEnumerable<T> source, Func<T, Int32, Boolean> predicate) where TException : Exception, new()
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            Int32 counter = 0;
            foreach (T item in source)
            {
                if (!predicate(item, counter++))
                {
                    throw new TException();
                }

                yield return item;
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> ThrowIfNull<T>(this IEnumerable<T?> source)
        {
            return ThrowIfNull<T, ArgumentNullException>(source);
        }

        public static IEnumerable<T> ThrowIfNull<T, TException>(this IEnumerable<T?> source) where TException : Exception, new()
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            foreach (T? item in source)
            {
                if (item is null)
                {
                    throw new TException();
                }

                yield return item;
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> ThrowIfNull<T>(this IEnumerable<T?> source) where T : struct
        {
            return ThrowIfNull<T, InvalidOperationException>(source);
        }

        public static IEnumerable<T> ThrowIfNull<T, TException>(this IEnumerable<T?> source) where T : struct where TException : Exception, new()
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            foreach (T? item in source)
            {
                if (!item.HasValue)
                {
                    throw new TException();
                }

                yield return item.Value;
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> DistinctLast<T>(this IEnumerable<T> source)
        {
            return DistinctLast(source, null);
        }

        public static IEnumerable<T> DistinctLast<T>(this IEnumerable<T> source, IEqualityComparer<T>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            SetQueue<T> set = new SetQueue<T>(comparer);

            foreach (T item in source)
            {
                set.RotateEnqueue(item);
            }

            foreach (T item in set)
            {
                yield return item;
            }
        }

        private readonly struct DistinctLastByEntry<T, TKey> : IEquatable<DistinctLastByEntry<T, TKey>>
        {
            public static implicit operator DistinctLastByEntry<T, TKey>(TKey key)
            {
                return new DistinctLastByEntry<T, TKey>(key, default!);
            }

            public TKey Key { get; }
            public T Value { get; }

            public DistinctLastByEntry(TKey key, T value)
            {
                Key = key;
                Value = value;
            }

            public override Boolean Equals(Object? other)
            {
                return other is TKey key && Equals(key) || other is DistinctLastByEntry<T, TKey> entry && Equals(entry);
            }

            public Boolean Equals(DistinctLastByEntry<T, TKey> other)
            {
                return EqualityComparer<TKey>.Default.Equals(Key, other.Key);
            }

            public override Int32 GetHashCode()
            {
                return HashCode.Combine(Key);
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> DistinctLastBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector)
        {
            return DistinctLastBy(source, selector, null);
        }

        public static IEnumerable<T> DistinctLastBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector, IEqualityComparer<TKey>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            comparer ??= EqualityComparer<TKey>.Default;

            EqualityComparison<DistinctLastByEntry<T, TKey>> comparison = (x, y) => comparer.Equals(x.Key, y.Key);
            SetQueue<DistinctLastByEntry<T, TKey>> set = new SetQueue<DistinctLastByEntry<T, TKey>>(comparison.ToEqualityComparer());

            foreach (T item in source)
            {
                set.RotateEnqueue(new DistinctLastByEntry<T, TKey>(selector(item), item));
            }

            foreach (DistinctLastByEntry<T, TKey> item in set)
            {
                yield return item.Value;
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> DistinctThrow<T>(this IEnumerable<T> source)
        {
            return DistinctThrow(source, null);
        }

        public static IEnumerable<T> DistinctThrow<T>(this IEnumerable<T> source, IEqualityComparer<T>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            HashSet<T> already = new HashSet<T>(comparer);

            foreach (T item in source)
            {
                if (!already.Add(item))
                {
                    throw new ArgumentException($"Item {item} already in {nameof(source)}");
                }

                yield return item;
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<String> DistinctThrow(this IEnumerable<String> source, StringComparison comparison)
        {
            return DistinctThrow(source, comparison.ToComparer());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> DistinctByThrow<T, TDistinct>(this IEnumerable<T> source, Func<T, TDistinct> selector)
        {
            return DistinctByThrow(source, selector, null);
        }

        public static IEnumerable<T> DistinctByThrow<T, TDistinct>(this IEnumerable<T> source, Func<T, TDistinct> selector, IEqualityComparer<TDistinct>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            HashSet<TDistinct> already = new HashSet<TDistinct>(comparer);

            foreach (T item in source)
            {
                TDistinct distinct = selector(item);
                if (!already.Add(distinct))
                {
                    throw new ArgumentException($"Item \"{distinct}\" already in {nameof(source)}");
                }

                yield return item;
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> DistinctByThrow<T>(this IEnumerable<T> source, Func<T, String> selector, StringComparison comparison)
        {
            return DistinctByThrow(source, selector, comparison.ToComparer());
        }

#if !NET6_0_OR_GREATER
        /// <summary>
        /// Returns distinct elements from a sequence using the provided value selector for equality comparison.
        /// </summary>
        /// <typeparam name="T">The type of the elements of source.</typeparam>
        /// <typeparam name="TDistinct">The type of the value on which to distinct.</typeparam>
        /// <param name="source">The source collection.</param>
        /// <param name="selector">A transform function to apply to each element to select the value on which to distinct.</param>
        public static IEnumerable<T> DistinctBy<T, TDistinct>(this IEnumerable<T> source, Func<T, TDistinct> selector)
        {
            return DistinctBy(source, selector, null);
        }

        /// <summary>
        /// Returns distinct elements from a sequence using the provided value selector for equality comparison.
        /// </summary>
        /// <typeparam name="T">The type of the elements of source.</typeparam>
        /// <typeparam name="TDistinct">The type of the value on which to distinct.</typeparam>
        /// <param name="source">The source collection.</param>
        /// <param name="selector">A transform function to apply to each element to select the value on which to distinct.</param>
        /// <param name="comparer">Comparer</param>
        public static IEnumerable<T> DistinctBy<T, TDistinct>(this IEnumerable<T> source, Func<T, TDistinct> selector, IEqualityComparer<TDistinct>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            HashSet<TDistinct> already = new HashSet<TDistinct>(comparer);

            foreach (T item in source)
            {
                if (already.Add(selector(item)))
                {
                    yield return item;
                }
            }
        }
#endif
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> DistinctBy<T>(this IEnumerable<T> source, Func<T, String> selector, StringComparison comparison)
        {
            return source.DistinctBy(selector, comparison.ToComparer());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> DistinctCount<T>(this IEnumerable<T> source, Int32 count)
        {
            return DistinctCount(source, count, null);
        }

        public static IEnumerable<T> DistinctCount<T>(this IEnumerable<T> source, Int32 count, IEqualityComparer<T>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (count <= 0)
            {
                yield break;
            }

            SetQueue<T> set = new SetQueue<T>(count, comparer);

            foreach (T item in source)
            {
                if (set.Contains(item))
                {
                    continue;
                }

                if (set.Count >= count)
                {
                    set.Dequeue();
                }

                if (set.Enqueue(item))
                {
                    yield return item;
                }
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> DistinctCountBy<T, TDistinct>(this IEnumerable<T> source, Func<T, TDistinct> selector, Int32 count)
        {
            return DistinctCountBy(source, selector, count, null);
        }

        public static IEnumerable<T> DistinctCountBy<T, TDistinct>(this IEnumerable<T> source, Func<T, TDistinct> selector, Int32 count, IEqualityComparer<TDistinct>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            if (count <= 0)
            {
                yield break;
            }

            SetQueue<TDistinct> set = new SetQueue<TDistinct>(count, comparer);

            foreach (T item in source)
            {
                TDistinct distinct = selector(item);
                if (set.Contains(distinct))
                {
                    continue;
                }

                if (set.Count >= count)
                {
                    set.Dequeue();
                }

                if (set.Enqueue(distinct))
                {
                    yield return item;
                }
            }
        }

        public static IEnumerable<T> Cancellation<T>(this IEnumerable<T> source, CancellationToken token)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.TakeWhile(_ => !token.IsCancellationRequested);
        }
    }
}