// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;

namespace NetExtender.Utilities.Types
{
    [SuppressMessage("ReSharper", "IteratorNeverReturns")]
    [SuppressMessage("ReSharper", "CognitiveComplexity")]
    public static partial class EnumerableUtilities
    {
        public static IOrderedEnumerable<T> OrderBy<T>(this IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return OrderBy(source, Comparer<T>.Default);
        }

        public static IOrderedEnumerable<T> OrderBy<T>(this IEnumerable<T> source, IComparer<T>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.OrderBy(item => item, comparer);
        }

        public static IOrderedEnumerable<T> OrderByDescending<T>(this IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return OrderByDescending(source, Comparer<T>.Default);
        }

        public static IOrderedEnumerable<T> OrderByDescending<T>(this IEnumerable<T> source, IComparer<T>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.OrderByDescending(item => item, comparer);
        }

        public static IOrderedEnumerable<T> Sort<T>(this IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return Sort(source, Comparer<T>.Default);
        }

        public static IOrderedEnumerable<T> Sort<T>(this IEnumerable<T> source, IComparer<T>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return OrderBy(source, comparer);
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

        public static IOrderedEnumerable<T> SortDescending<T>(this IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return SortDescending(source, Comparer<T>.Default);
        }

        public static IOrderedEnumerable<T> SortDescending<T>(this IEnumerable<T> source, IComparer<T>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return OrderByDescending(source, comparer);
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
        
        public static IEnumerable<T> ThrowIfNull<T>(this IEnumerable<T> source)
        {
            return ThrowIfNull<T, ArgumentNullException>(source);
        }

        public static IEnumerable<T> ThrowIfNull<T, TException>(this IEnumerable<T> source) where TException : Exception, new()
        {
            return ThrowIf<T, TException>(source, item => item is null);
        }
        
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
                T value;

                try
                {
                    value = item!.Value;
                }
                catch (InvalidOperationException)
                {
                    if (typeof(TException) == typeof(InvalidOperationException))
                    {
                        throw;
                    }

                    throw new TException();
                }

                yield return value;
            }
        }

        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            foreach (T item in source)
            {
                action(item);
                yield return item;
            }
        }

        public static IEnumerable<T> ForEachWhere<T>(this IEnumerable<T> source, Func<T, Boolean> where, Action<T> action)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            foreach (T item in source)
            {
                if (where(item))
                {
                    action(item);
                }

                yield return item;
            }
        }
        
        public static IEnumerable<T> ForEachWhere<T>(this IEnumerable<T> source, Func<T, Int32, Boolean> where, Action<T> action)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            Int32 index = 0;
            foreach (T item in source)
            {
                if (where(item, index))
                {
                    action(item);
                }

                ++index;
                yield return item;
            }
        }
        
        public static IEnumerable<T> ForEachWhereNot<T>(this IEnumerable<T> source, Func<T, Boolean> where, Action<T> action)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            return ForEachWhere(source, item => !where(item), action);
        }
        
        public static IEnumerable<T> ForEachWhereNot<T>(this IEnumerable<T> source, Func<T, Int32, Boolean> where, Action<T> action)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            return ForEachWhere(source, (item, index) => !where(item, index), action);
        }

        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> source, Action<T, Int32> action)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            Int32 index = 0;
            foreach (T item in source)
            {
                action(item, index);
                ++index;
                yield return item;
            }
        }

        public static IEnumerable<T> ForEachWhere<T>(this IEnumerable<T> source, Func<T, Boolean> where, Action<T, Int32> action)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            Int32 index = 0;
            foreach (T item in source)
            {
                if (where(item))
                {
                    action(item, index);
                }

                ++index;
                yield return item;
            }
        }
        
        public static IEnumerable<T> ForEachWhere<T>(this IEnumerable<T> source, Func<T, Int32, Boolean> where, Action<T, Int32> action)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            Int32 index = 0;
            foreach (T item in source)
            {
                if (where(item, index))
                {
                    action(item, index);
                }

                ++index;
                yield return item;
            }
        }

        public static IEnumerable<T> ForEachWhereNot<T>(this IEnumerable<T> source, Func<T, Boolean> where, Action<T, Int32> action)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            return ForEachWhere(source, item => !where(item), action);
        }
        
        public static IEnumerable<T> ForEachWhereNot<T>(this IEnumerable<T> source, Func<T, Int32, Boolean> where, Action<T, Int32> action)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            return ForEachWhere(source, (item, index) => !where(item, index), action);
        }
        
        public static IEnumerable<T> ForEachBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector, Action<TKey> action)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            foreach (T item in source)
            {
                action(selector(item));
                yield return item;
            }
        }

        public static IEnumerable<T> ForEachByWhere<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector, Func<TKey, Boolean> where, Action<TKey> action)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            foreach (T item in source)
            {
                TKey select = selector(item);
                if (where(select))
                {
                    action(select);
                }

                yield return item;
            }
        }
        
        public static IEnumerable<T> ForEachByWhere<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector, Func<TKey, Int32, Boolean> where, Action<TKey> action)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            Int32 index = 0;
            foreach (T item in source)
            {
                TKey select = selector(item);
                if (where(select, index))
                {
                    action(select);
                }

                ++index;
                yield return item;
            }
        }
        
        public static IEnumerable<T> ForEachByWhereNot<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector, Func<TKey, Boolean> where, Action<TKey> action)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            return ForEachByWhere(source, selector, item => !where(item), action);
        }
        
        public static IEnumerable<T> ForEachByWhereNot<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector, Func<TKey, Int32, Boolean> where, Action<TKey> action)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            return ForEachByWhere(source, selector, (item, index) => !where(item, index), action);
        }

        public static IEnumerable<T> ForEachBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector, Action<TKey, Int32> action)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            Int32 index = 0;
            foreach (T item in source)
            {
                action(selector(item), index);
                ++index;
                yield return item;
            }
        }

        public static IEnumerable<T> ForEachByWhere<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector, Func<TKey, Boolean> where, Action<TKey, Int32> action)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            Int32 index = 0;
            foreach (T item in source)
            {
                TKey select = selector(item);
                if (where(select))
                {
                    action(select, index);
                }

                ++index;
                yield return item;
            }
        }
        
        public static IEnumerable<T> ForEachByWhere<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector, Func<TKey, Int32, Boolean> where, Action<TKey, Int32> action)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            Int32 index = 0;
            foreach (T item in source)
            {
                TKey select = selector(item);
                if (where(select, index))
                {
                    action(select, index);
                }

                ++index;
                yield return item;
            }
        }

        public static IEnumerable<T> ForEachByWhereNot<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector, Func<TKey, Boolean> where, Action<TKey, Int32> action)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            return ForEachByWhere(source, selector, item => !where(item), action);
        }
        
        public static IEnumerable<T> ForEachByWhereNot<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector, Func<TKey, Int32, Boolean> where, Action<TKey, Int32> action)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            return ForEachByWhere(source, selector, (item, index) => !where(item, index), action);
        }

        public static IEnumerable<T> ForEachEvery<T>(this IEnumerable<T> source, Action<T> action, Int32 every)
        {
            return ForEachEvery(source, action, every, false);
        }

        public static IEnumerable<T> ForEachEvery<T>(this IEnumerable<T> source, Action<T> action, Int32 every, Boolean first)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            if (every <= 1)
            {
                return ForEach(source, action);
            }
            
            Int32 counter = first ? 0 : 1;
            return source.ForEachWhere(_ => counter++ % every == 0, action);
        }

        /// <summary>
        /// Return item if source is empty
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="alternate">returned item</param>
        /// <typeparam name="T"></typeparam>
        public static IEnumerable<T> WhereOr<T>(this IEnumerable<T> source, T alternate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            using IEnumerator<T> enumerator = source.GetEnumerator();

            if (!enumerator.MoveNext())
            {
                yield return alternate;
                yield break;
            }

            do
            {
                yield return enumerator.Current;
                
            } while (enumerator.MoveNext());
        }

        /// <summary>
        /// Return item if source is empty
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="predicate">predicate</param>
        /// <param name="alternate">returned item</param>
        /// <typeparam name="T"></typeparam>
        public static IEnumerable<T> WhereOr<T>(this IEnumerable<T> source, Func<T, Boolean> predicate, T alternate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return source.Where(predicate).WhereOr(alternate);
        }

        /// <summary>
        /// Return item if predicate else return alternate
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="predicate">predicate</param>
        /// <param name="alternate">returned item</param>
        /// <typeparam name="T"></typeparam>
        public static IEnumerable<T> WhereElse<T>(this IEnumerable<T> source, Func<T, Boolean> predicate, T alternate)
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
                yield return predicate(item) ? item : alternate;
            }
        }

        /// <summary>
        /// Combines two Enumerable objects into a sequence of Tuples containing each element
        /// of the source Enumerable in the first position with the element that has the same
        /// index in the second Enumerable in the second position.
        /// </summary>
        /// <typeparam name="T1">The type of the elements of <paramref name="first"/>.</typeparam>
        /// <typeparam name="T2">The type of the elements of <paramref name="second"/>.</typeparam>
        /// <typeparam name="T3">The type of the elements of <paramref name="third"/>.</typeparam>
        /// <param name="first">The first sequence.</param>
        /// <param name="second">The second sequence.</param>
        /// <param name="third">The third sequence.</param>
        /// <returns>The output sequence will be as long as the shortest input sequence.</returns>
        public static IEnumerable<(T1 First, T2 Second, T3 Third)> Zip<T1, T2, T3>(this IEnumerable<T1> first, IEnumerable<T2> second, IEnumerable<T3> third)
        {
            if (first is null)
            {
                throw new ArgumentNullException(nameof(first));
            }

            if (second is null)
            {
                throw new ArgumentNullException(nameof(second));
            }

            if (third is null)
            {
                throw new ArgumentNullException(nameof(third));
            }

            using IEnumerator<T1> fenumerator = first.GetEnumerator();
            using IEnumerator<T2> senumerator = second.GetEnumerator();
            using IEnumerator<T3> tenumerator = third.GetEnumerator();

            while (fenumerator.MoveNext() && senumerator.MoveNext() && tenumerator.MoveNext())
            {
                yield return (fenumerator.Current, senumerator.Current, tenumerator.Current);
            }
        }

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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> Progress<T>(this IEnumerable<T> source, IProgress<Int32> progress)
        {
            if (progress is null)
            {
                throw new ArgumentNullException(nameof(progress));
            }

            return Progress(source, progress.Report);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> Progress<T>(this IEnumerable<T> source, IProgress<Int32> progress, Int32 size)
        {
            if (progress is null)
            {
                throw new ArgumentNullException(nameof(progress));
            }

            return Progress(source, progress.Report, size);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> Progress<T>(this IEnumerable<T> source, Action callback)
        {
            if (callback is null)
            {
                throw new ArgumentNullException(nameof(callback));
            }

            return Progress(source, _ => callback.Invoke());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> Progress<T>(this IEnumerable<T> source, Action callback, Int32 size)
        {
            if (callback is null)
            {
                throw new ArgumentNullException(nameof(callback));
            }

            return Progress(source, _ => callback.Invoke(), size);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> Progress<T>(this IEnumerable<T> source, Action<Int32> callback)
        {
            return Progress(source, callback, 1);
        }

        public static IEnumerable<T> Progress<T>(this IEnumerable<T> source, Action<Int32> callback, Int32 size)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (callback is null)
            {
                throw new ArgumentNullException(nameof(callback));
            }

            if (size <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(size));
            }

            using IEnumerator<T> enumerator = source.GetEnumerator();

            if (!enumerator.MoveNext())
            {
                yield break;
            }
            
            Int32 count = 0;
            Int32 counter = 0;

            do
            {
                T item = enumerator.Current;
                
                if (++counter >= size)
                {
                    count += counter;
                    counter = 0;
                    callback.Invoke(count);
                }

                if (!enumerator.MoveNext())
                {
                    count += counter;
                    if (count % size > 0)
                    {
                        callback.Invoke(count);
                    }

                    yield return item;
                    yield break;
                }
                
                yield return item;

            } while (true);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> LongProgress<T>(this IEnumerable<T> source, IProgress<Int64> progress)
        {
            if (progress is null)
            {
                throw new ArgumentNullException(nameof(progress));
            }

            return LongProgress(source, progress.Report);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> LongProgress<T>(this IEnumerable<T> source, IProgress<Int64> progress, Int64 size)
        {
            if (progress is null)
            {
                throw new ArgumentNullException(nameof(progress));
            }

            return LongProgress(source, progress.Report, size);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> LongProgress<T>(this IEnumerable<T> source, Action callback)
        {
            if (callback is null)
            {
                throw new ArgumentNullException(nameof(callback));
            }

            return LongProgress(source, _ => callback.Invoke());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> LongProgress<T>(this IEnumerable<T> source, Action callback, Int64 size)
        {
            if (callback is null)
            {
                throw new ArgumentNullException(nameof(callback));
            }

            return LongProgress(source, _ => callback.Invoke(), size);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> LongProgress<T>(this IEnumerable<T> source, Action<Int64> callback)
        {
            return LongProgress(source, callback, 1);
        }

        public static IEnumerable<T> LongProgress<T>(this IEnumerable<T> source, Action<Int64> callback, Int64 size)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (callback is null)
            {
                throw new ArgumentNullException(nameof(callback));
            }

            if (size <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(size));
            }
            
            using IEnumerator<T> enumerator = source.GetEnumerator();

            if (!enumerator.MoveNext())
            {
                yield break;
            }
            
            Int64 count = 0;
            Int64 counter = 0;

            do
            {
                T item = enumerator.Current;
                
                if (++counter >= size)
                {
                    count += counter;
                    counter = 0;
                    callback.Invoke(count);
                }

                if (!enumerator.MoveNext())
                {
                    count += counter;
                    if (count % size > 0)
                    {
                        callback.Invoke(count);
                    }

                    yield return item;
                    yield break;
                }
                
                yield return item;

            } while (true);
        }
    }
}