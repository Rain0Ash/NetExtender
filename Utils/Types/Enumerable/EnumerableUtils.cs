// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DynamicData.Annotations;
using NetExtender.Random;
using NetExtender.Types.Dictionaries;
using NetExtender.Utils.Numerics;

namespace NetExtender.Utils.Types
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("ReSharper", "IteratorNeverReturns")]
    public static class EnumerableUtils
    {
        public static IEnumerator<T> GetEmptyEnumerator<T>()
        {
            return Enumerable.Empty<T>().GetEnumerator();
        }

        public static IEnumerable<T> GetEnumerableFrom<T>(T first)
        {
            yield return first;
        }
        
        public static IEnumerable<T> GetEnumerableFrom<T>(T first, T second)
        {
            yield return first;
            yield return second;
        }
        
        public static IEnumerable<T> GetEnumerableFrom<T>(T first, T second, T third)
        {
            yield return first;
            yield return second;
            yield return third;
        }
        
        public static IEnumerable<T> GetEnumerableFrom<T>(T first, T second, T third, params T[] other)
        {
            yield return first;
            yield return second;
            yield return third;

            // ReSharper disable once InvertIf
            if (other?.Length > 0)
            {
                foreach (T item in other)
                {
                    yield return item;
                }
            }
        }
        
        public static IEnumerable<T> Append<T>(this IEnumerable<T> source, IEnumerable<T> additional)
        {
            return source.Concat(additional);
        }
        
        public static IEnumerable<T> Append<T>(this IEnumerable<T> source, IEnumerable<T> additional, params IEnumerable<T>[] additionals)
        {
            return source.Concat(additional, additionals);
        }

        public static IEnumerable<T> Append<T>(this IEnumerable<T> source, T value, params T[] values)
        {
            foreach (T item in source)
            {
                yield return item;
            }

            yield return value;

            foreach (T item in values)
            {
                yield return item;
            }
        }
        
        public static IEnumerable<T> Prepend<T>(this IEnumerable<T> source, IEnumerable<T> additional)
        {
            return source.Preconcat(additional);
        }
        
        public static IEnumerable<T> Prepend<T>(this IEnumerable<T> source, IEnumerable<T> additional, params IEnumerable<T>[] additionals)
        {
            return source.Preconcat(additional, additionals);
        }

        public static IEnumerable<T> Prepend<T>(this IEnumerable<T> source, T value, params T[] values)
        {
            yield return value;

            foreach (T item in values)
            {
                yield return item;
            }

            foreach (T item in source)
            {
                yield return item;
            }
        }
        
        public static IEnumerable<T> Concat<T>(this IEnumerable<T> source, IEnumerable<T> additional)
        {
            foreach (T item in source)
            {
                yield return item;
            }

            foreach (T item in additional)
            {
                yield return item;
            }
        }

        public static IEnumerable<T> Concat<T>(this IEnumerable<T> source, IEnumerable<T> additional, params IEnumerable<T>[] additionals)
        {
            foreach (T item in source)
            {
                yield return item;
            }

            foreach (T item in additional)
            {
                yield return item;
            }

            foreach (IEnumerable<T> next in additionals)
            {
                foreach (T item in next)
                {
                    yield return item;
                }
            }
        }
        
        public static IEnumerable<T> Preconcat<T>(this IEnumerable<T> source, IEnumerable<T> additional)
        {
            foreach (T item in additional)
            {
                yield return item;
            }

            foreach (T item in source)
            {
                yield return item;
            }
        }

        public static IEnumerable<T> Preconcat<T>(this IEnumerable<T> source, IEnumerable<T> additional, params IEnumerable<T>[] additionals)
        {
            foreach (T item in additional)
            {
                yield return item;
            }
            
            foreach (IEnumerable<T> next in additionals)
            {
                foreach (T item in next)
                {
                    yield return item;
                }
            }

            foreach (T item in source)
            {
                yield return item;
            }
        }

        public static T ElementAtOr<T>(this IEnumerable<T> source, Int32 index, T alternate)
        {
            if (index < 0)
            {
                return alternate;
            }

            if (source is IList<T> list)
            {
                return index < list.Count ? list[index] : alternate;
            }

            using IEnumerator<T> e = source.GetEnumerator();

            while (e.MoveNext())
            {
                if (index-- == 0)
                {
                    return e.Current;
                }
            }

            return alternate;
        }

        public static Boolean InBounds<T>(this IEnumerable<T> source, Int32 index)
        {
            return source switch
            {
                ICollection<T> collection => index >= 0 && index < collection.Count,
                IReadOnlyCollection<T> collection => index >= 0 && index < collection.Count,
                _ => index >= 0 && index < source.Count()
            };
        }

        public static T GetRandom<T>(this IEnumerable<T> source)
        {
            return source switch
            {
                IList<T> list => list.Count <= 0 ? default : list[RandomUtils.Next(list.Count - 1)],
                IReadOnlyList<T> list => list.Count <= 0 ? default : list[RandomUtils.Next(list.Count - 1)],
                ICollection<T> collection => collection.Count <= 0 ? default : collection.ElementAtOrDefault(RandomUtils.Next(collection.Count - 1)),
                IReadOnlyCollection<T> collection => collection.Count <= 0 ? default : collection.ElementAtOrDefault(RandomUtils.Next(collection.Count - 1)),
                _ => source.ElementAtOrDefault(RandomUtils.Next(source.Count() - 1))
            };
        }

        public static IEnumerable<T> AggregateAdd<T>(this IEnumerable<T> source, Func<T, T, T> aggregate, Func<T, T> add,
            Boolean prepend = false)
        {
            source = source.ToList();

            T value = add(source.Aggregate(aggregate));

            return prepend ? source.Prepend(value) : source.Append(value);
        }

        public static IEnumerable<TOut> TrySelect<T, TOut>(this IEnumerable<T> source, Func<T, TOut> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            foreach (T item in source)
            {
                TOut value;
                Boolean successful;

                try
                {
                    value = predicate(item);
                    successful = true;
                }
                catch (Exception)
                {
                    value = default;
                    successful = false;
                }

                if (successful)
                {
                    yield return value;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TOut> TrySelectWhere<T, TOut>(this IEnumerable<T> source, Func<T, Boolean> where, Func<T, TOut> predicate)
        {
            return source.Where(where).TrySelect(predicate);
        }

        public static IEnumerable<TOut> SelectWhere<T, TOut>(this IEnumerable<T> source, TryParseHandler<T, TOut> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            foreach (T item in source)
            {
                if (predicate(item, out TOut value))
                {
                    yield return value;
                }
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TOut> SelectWhere<T, TOut>(this IEnumerable<T> source, Func<T, Boolean> where, TryParseHandler<T, TOut> predicate)
        {
            return source.Where(where).SelectWhere(predicate);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TOut> TrySelectWhere<T, TOut>(this IEnumerable<T> source, Func<T, Boolean> where, TryParseHandler<T, TOut> predicate)
        {
            return SelectWhere(source, where, predicate);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TOut> TryParse<T, TOut>(this IEnumerable<T> source, TryParseHandler<T, TOut> predicate)
        {
            return SelectWhere(source, predicate);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TOut> TryParseWhere<T, TOut>(this IEnumerable<T> source, Func<T, Boolean> where, TryParseHandler<T, TOut> predicate)
        {
            return TrySelectWhere(source, where, predicate);
        }
        
        public static IEnumerable<TOut> SelectWhere<T, TOut>(this IEnumerable<T> source, Func<T, (Boolean, TOut)> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            foreach (T item in source)
            {
                (Boolean successful, TOut output) = predicate(item);

                if (successful)
                {
                    yield return output;
                }
            }
        }

        public static IEnumerable<TOut> SelectWhere<T, TOut>(this IEnumerable<T> source, Func<T, Boolean> where, Func<T, TOut> select)
        {
            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (select is null)
            {
                throw new ArgumentNullException(nameof(select));
            }

            return source.Where(where).Select(select);
        }

        public static IEnumerable<T> Change<T>(this IEnumerable<T> source, Func<T, T> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return source.Select(predicate);
        }

        public static IEnumerable<T> ChangeWhere<T>(this IEnumerable<T> source, Func<T, (Boolean, T)> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            foreach (T item in source)
            {
                (Boolean successful, T output) = predicate(item);

                yield return successful ? output : item;
            }
        }

        public static IEnumerable<T> ChangeWhere<T>(this IEnumerable<T> source, Func<T, Boolean> where, Func<T, T> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return source.Select(item => where(item) ? predicate(item) : item);
        }

        public static IEnumerable<T> Sort<T>(this IEnumerable<T> source, IComparer<T> comparer = null)
        {
            return source.OrderBy(item => item, comparer);
        }

        public static IEnumerable<T> Sort<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector, IComparer<TKey> comparer = null)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.OrderBy(selector, comparer);
        }

        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            foreach (T item in source)
            {
                action(item);
            }

            return source;
        }

        public static IEnumerable<T> ForEachWhere<T>(this IEnumerable<T> source, Func<T, Boolean> where, Action<T> action)
        {
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
            }

            return source;
        }

        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> source, Action<T, Int32> action)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            Int32 index = 0;
            foreach (T item in source)
            {
                action(item, index);
                ++index;
            }

            return source;
        }

        public static IEnumerable<T> ForEachWhere<T>(this IEnumerable<T> source, Func<T, Boolean> where, Action<T, Int32> action)
        {
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
            }

            return source;
        }

        public static T FirstOr<T>(this IEnumerable<T> source, T alternate)
        {
            foreach (T item in source)
            {
                return item;
            }

            return alternate;
        }

        public static T FirstOr<T>(this IEnumerable<T> source, Func<T, Boolean> predicate, T alternate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            foreach (T item in source.Where(predicate))
            {
                return item;
            }

            return alternate;
        }
        
        public static T FirstOr<T>(this IEnumerable<T> source, Func<T> alternate)
        {
            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            foreach (T item in source)
            {
                return item;
            }

            return alternate.Invoke();
        }

        public static T FirstOr<T>(this IEnumerable<T> source, Func<T, Boolean> predicate, Func<T> alternate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            foreach (T item in source.Where(predicate))
            {
                return item;
            }

            return alternate.Invoke();
        }

        public static T LastOr<T>(this IEnumerable<T> source, T alternate)
        {
            return FirstOr(source.Reverse(), alternate);
        }

        public static T LastOr<T>(this IEnumerable<T> source, Func<T, Boolean> predicate, T alternate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return FirstOr(source.Reverse(), predicate, alternate);
        }
        
        public static T LastOr<T>(this IEnumerable<T> source, Func<T> alternate)
        {
            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            return FirstOr(source.Reverse(), alternate);
        }

        public static T LastOr<T>(this IEnumerable<T> source, Func<T, Boolean> predicate, Func<T> alternate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            return FirstOr(source.Reverse(), predicate, alternate);
        }
        
        /// <summary>
        /// Return item if source is empty
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="alternate">returned item</param>
        /// <typeparam name="T"></typeparam>
        public static IEnumerable<T> WhereOr<T>(this IEnumerable<T> source, T alternate)
        {
            using IEnumerator<T> enumerator = source.GetEnumerator();

            if (enumerator.MoveNext())
            {
                do
                {
                    yield return enumerator.Current;
                    
                } while (enumerator.MoveNext());
            }
            else
            {
                yield return alternate;
            }
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
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return WhereOr(source.Where(predicate), alternate);
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
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            foreach (T item in source)
            {
                yield return predicate(item) ? item : alternate;
            }
        }

        public static IEnumerable<Int32> IndexWhere<T>(this IEnumerable<T> source, Func<T, Boolean> predicate)
        {
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

        public static IEnumerable<T> Replace<T>(this IEnumerable<T> source, T what, T to, IEqualityComparer<T> comparer = null)
        {
            comparer ??= EqualityComparer<T>.Default;

            foreach (T item in source)
            {
                yield return comparer.Equals(item, what) ? to : item;
            }
        }

        /// <summary>
        /// Splits the given sequence into chunks of the given size.
        /// If the sequence length isn't evenly divisible by the chunk size,
        /// the last chunk will contain all remaining elements.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The sequence.</param>
        /// <param name="size">The number of elements per chunk.</param>
        /// <returns>The chunked sequence.</returns>
        public static IEnumerable<T[]> Chunk<T>(this IEnumerable<T> source, Int32 size)
        {
            if (size <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(size));
            }

            if (size == 1)
            {
                foreach (T item in source)
                {
                    yield return new[] {item};
                }
                
                yield break;
            }

            T[] chunk = null;
            Int32 index = 0;

            foreach (T element in source)
            {
                chunk ??= new T[size];
                chunk[index++] = element;

                if (index != size)
                {
                    continue;
                }

                yield return chunk;
                index = 0;
                chunk = null;
            }

            // Do we have an incomplete chunk of remaining elements?
            if (chunk is null)
            {
                yield break;
            }

            // This last chunk is incomplete, otherwise it would've been returned already.
            // Thus, we have to create a new, shorter array to hold the remaining elements.
            T[] result = new T[index];
            Array.Copy(chunk, result, index);

            yield return result;
        }

        /// <summary>
        /// Combines two Enumerable objects into a sequence of Tuples containing each element
        /// of the source Enumerable in the first position with the element that has the same
        /// index in the second Enumerable in the second position.
        /// </summary>
        /// <typeparam name="T1">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <typeparam name="T2">The type of the elements of <paramref name="other"/>.</typeparam>
        /// <param name="source">The first sequence.</param>
        /// <param name="other">The second sequence.</param>
        /// <returns>The output sequence will be as long as the shortest input sequence.</returns>
        public static IEnumerable<(T1, T2)> Zip<T1, T2>(this IEnumerable<T1> source, IEnumerable<T2> other)
        {
            if (source is null || other is null)
            {
                throw new ArgumentNullException();
            }

            using IEnumerator<T1> first = source.GetEnumerator();
            using IEnumerator<T2> second = other.GetEnumerator();

            while (first.MoveNext() && second.MoveNext())
            {
                yield return (first.Current, second.Current);
            }
        }

        /// <summary>
        /// Combines two Enumerable objects into a sequence of Tuples containing each element
        /// of the source Enumerable in the first position with the element that has the same
        /// index in the second Enumerable in the second position.
        /// </summary>
        /// <typeparam name="T1">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <typeparam name="T2">The type of the elements of <paramref name="next"/>.</typeparam>
        /// <typeparam name="T3">The type of the elements of <paramref name="other"/>.</typeparam>
        /// <param name="source">The first sequence.</param>
        /// <param name="next">The second sequence.</param>
        /// <param name="other">The third sequence.</param>
        /// <returns>The output sequence will be as long as the shortest input sequence.</returns>
        public static IEnumerable<(T1, T2, T3)> Zip<T1, T2, T3>(this IEnumerable<T1> source, IEnumerable<T2> next, IEnumerable<T3> other)
        {
            if (source is null || next is null || other is null)
            {
                throw new ArgumentNullException();
            }

            using IEnumerator<T1> first = source.GetEnumerator();
            using IEnumerator<T2> second = next.GetEnumerator();
            using IEnumerator<T3> third = other.GetEnumerator();

            while (first.MoveNext() && second.MoveNext() && third.MoveNext())
            {
                yield return (first.Current, second.Current, third.Current);
            }
        }

        /// <summary>
        /// Turns a finite sequence into a circular one, or equivalently,
        /// repeats the original sequence indefinitely.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">An <see cref="IEnumerable{T}"/> to cycle through.</param>
        /// <param name="count">Count of repeat, 0 is default IEnumerable</param>
        /// <returns>An infinite sequence cycling through the given sequence.</returns>
        public static IEnumerable<T> Cycle<T>(this IEnumerable<T> source, Int32 count = Int32.MaxValue)
        {
            if (count < 0)
            {
                throw new ArgumentException($"Value of {nameof(count)} argument = {count}, {count} < 0");
            }

            ICollection<T> values = source.ToArray();

            for (Int32 i = 0; i <= count; i++)
            {
                foreach (T item in values)
                {
                    yield return item;
                }
            }
        }

        /// <summary>
        /// Concatenates all elements of a sequence using the specified separator between each element.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">A sequence that contains the objects to concatenate.</param>
        /// <param name="separator">The string to use as a separator.</param>
        /// <param name="start">Start with</param>
        /// <param name="end">End with</param>
        /// <returns>A string holding the concatenated values.</returns>
        public static String Join<T>(this IEnumerable<T> source, String separator = "", String start = "", String end = "")
        {
            return $"{start}{String.Join(separator, source)}{end}";
        }

        /// <summary>
        /// Concatenates all elements of a sequence using the specified separator between each element.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <typeparam name="TOut">The type of the elements of <paramref name="predicate"/> which will be joined.</typeparam>
        /// <param name="source">A sequence that contains the objects to concatenate.</param>
        /// <param name="predicate">Convert <typeparam name="T"> to <typeparam name="TOut"></typeparam></typeparam>></param>
        /// <param name="separator">The string to use as a separator.</param>
        /// <param name="start">Start with</param>
        /// <param name="end">End with</param>
        /// <returns>A string holding the concatenated values.</returns>
        public static String Join<T, TOut>(this IEnumerable<T> source, Func<T, TOut> predicate, String separator = "", String start = "", String end = "")
        {
            return Join(source.Select(predicate), separator, start, end);
        }

        /// <summary>
        /// Returns a flattened OfType() sequence that contains the concatenation of all the nested sequences' elements.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">A sequence of sequences to be flattened.</param>
        /// <returns>The concatenation of all the nested sequences' elements.</returns>
        public static IEnumerable<T> Flatten<T>(this IEnumerable source)
        {
            switch (source)
            {
                case IEnumerable<T> generic:
                {
                    foreach (T item in generic)
                    {
                        yield return item;
                    }

                    break;
                }
                case IEnumerable<IEnumerable> jagged:
                {
                    foreach (IEnumerable js in jagged)
                    {
                        foreach (T item in Flatten<T>(js))
                        {
                            yield return item;
                        }
                    }

                    break;
                }
            }
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            return Shuffle(source, new MersenneTwister());
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, System.Random generator)
        {
            T[] buffer = source.ToArray();

            for (Int32 i = 0; i < buffer.Length; i++)
            {
                Int32 j = generator.Next(i, buffer.Length);
                yield return buffer[j];

                buffer[j] = buffer[i];
            }
        }

        /// <summary>
        /// Determines whether the specified sequence's element count is equal to or greater than <paramref name="count"/>.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{TSource}"/> whose elements to count.</param>
        /// <param name="count">The minimum number of elements the specified sequence is expected to contain.</param>
        /// <returns>
        ///   <c>true</c> if the element count of <paramref name="source"/> is equal to or greater than <paramref name="count"/>; otherwise, <c>false</c>.
        /// </returns>
        public static Boolean HasAtLeast<T>(this IEnumerable<T> source, Int32 count)
        {
            if (source is ICollection sc && sc.Count < count)
            {
                return false;
            }

            if (count <= 0)
            {
                return true;
            }

            Int32 matches = 0;

            using IEnumerator<T> enumerator = source.GetEnumerator();

            while (enumerator.MoveNext())
            {
                if (matches++ >= count)
                {
                    return true;
                }
            }

            return matches >= count;
        }

        /// <summary>
        /// Determines whether the specified sequence contains exactly <paramref name="count"/> or more elements satisfying a condition.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{TSource}"/> whose elements to count.</param>
        /// <param name="count">The minimum number of elements satisfying the specified condition the specified sequence is expected to contain.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>
        ///   <c>true</c> if the element count of satisfying elements is equal to or greater than <paramref name="count"/>; otherwise, <c>false</c>.
        /// </returns>
        public static Boolean HasAtLeast<T>(this IEnumerable<T> source, Int32 count, Func<T, Boolean> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            if (source is ICollection sc && sc.Count < count)
            {
                return false;
            }

            if (count <= 0)
            {
                return true;
            }

            Int32 matches = 0;

            if (source.Where(predicate).Any(_ => ++matches >= count))
            {
                return true;
            }

            return matches >= count;
        }

        /// <summary>
        /// Determines whether the specified sequence's element count is at most <paramref name="count"/>.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{TSource}"/> whose elements to count.</param>
        /// <param name="count">The maximum number of elements the specified sequence is expected to contain.</param>
        /// <returns>
        ///   <c>true</c> if the element count of <paramref name="source"/> is equal to or lower than <paramref name="count"/>; otherwise, <c>false</c>.
        /// </returns>
        public static Boolean HasAtMost<T>(this IEnumerable<T> source, Int32 count)
        {
            return !HasAtLeast(source, count);
        }

        /// <summary>
        /// Determines whether the specified sequence contains at most <paramref name="count"/> elements satisfying a condition.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{TSource}"/> whose elements to count.</param>
        /// <param name="count">The maximum number of elements satisfying the specified condition the specified sequence is expected to contain.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>
        ///   <c>true</c> if the element count of satisfying elements is equal to or less than <paramref name="count"/>; otherwise, <c>false</c>.
        /// </returns>
        public static Boolean HasAtMost<T>(this IEnumerable<T> source, Int32 count, Func<T, Boolean> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return !HasAtLeast(source, count, predicate);
        }

        /// <summary>
        /// Determines whether the specified sequence contains exactly the specified number of elements.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{TSource}"/> to count.</param>
        /// <param name="count">The number of elements the specified sequence is expected to contain.</param>
        /// <returns>
        ///   <c>true</c> if <paramref name="source"/> contains exactly <paramref name="count"/> elements; otherwise, <c>false</c>.
        /// </returns>
        public static Boolean HasExactly<T>(this IEnumerable<T> source, Int32 count)
        {
            if (source is ICollection collection)
            {
                return collection.Count == count;
            }

            Int32 matches = 0;

            using IEnumerator<T> enumerator = source.GetEnumerator();

            while (enumerator.MoveNext())
            {
                if (matches++ > count)
                {
                    return false;
                }
            }

            return matches == count;
        }

        /// <summary>
        /// Determines whether the specified sequence contains exactly the specified number of elements satisfying the specified condition.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{TSource}"/> to count satisfying elements.</param>
        /// <param name="count">The number of matching elements the specified sequence is expected to contain.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>
        ///   <c>true</c> if <paramref name="source"/> contains exactly <paramref name="count"/> elements satisfying the condition; otherwise, <c>false</c>.
        /// </returns>
        public static Boolean HasExactly<T>(this IEnumerable<T> source, Int32 count, Func<T, Boolean> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            if (source is ICollection collection && collection.Count < count)
            {
                return false;
            }

            Int32 matches = 0;

            if (source.Where(predicate).Any(_ => ++matches > count))
            {
                return false;
            }

            return matches == count;
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
        public static IEnumerable<T> Without<T>(IEnumerable<T> source, IEqualityComparer<T> comparer, params T[] without)
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
        /// <param name="comparer">The equality comparer to use.</param>
        /// <returns>
        /// All elements of <paramref name="source"/> except <paramref name="without"/>.
        /// </returns>
        public static IEnumerable<T> Without<T>(IEnumerable<T> source, IEnumerable<T> without, IEqualityComparer<T> comparer = null)
        {
            HashSet<T> remove = new HashSet<T>(without, comparer);

            return source.Where(item => !remove.Contains(item));
        }

        public static Boolean ContainsAny<T>(this IEnumerable<T> source, T value, params T[] values) where T : IComparable<T>
        {
            return source.Any(c => values.Prepend(value).Any(x => x.CompareTo(c) == 0));
        }

        public static Boolean ContainsAny<T>(this IEnumerable<T> source, T[] values, IComparer<T> comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (values is null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            return values.Length == 0 || source.Any(c => values.Any(x => comparer.Compare(x, c) == 0));
        }

        public static IEnumerable<T> NotEmptyOr<T>(this IEnumerable<T> source, params T[] alternate)
        {
            return alternate?.Length > 0 ? NotEmptyOr(source, (IEnumerable<T>) alternate) : source;
        }

        public static IEnumerable<T> NotEmptyOr<T>(this IEnumerable<T> source, IEnumerable<T> alternate)
        {
            return source.IsNotEmpty() ? source : alternate;
        }

        /// <summary>
        /// Determines whether the given sequence is not empty.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{TSource}"/> to check for emptiness.</param>
        /// <returns>
        ///   <c>true</c> if <paramref name="source"/> is empty; otherwise, <c>false</c>.
        /// </returns>
        public static Boolean IsNotEmpty<T>(this IEnumerable<T> source)
        {
            return source?.Any() == true;
        }

        /// <summary>
        /// Determines whether the given sequence is not empty.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{TSource}"/> to check for emptiness.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>
        ///   <c>true</c> if <paramref name="source"/> is empty; otherwise, <c>false</c>.
        /// </returns>
        public static Boolean IsNotEmpty<T>(this IEnumerable<T> source, Func<T, Boolean> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return source?.Any(predicate) == true;
        }

        /// <summary>
        /// Determines whether the given sequence is empty.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{TSource}"/> to check for emptiness.</param>
        /// <returns>
        ///   <c>true</c> if <paramref name="source"/> is empty; otherwise, <c>false</c>.
        /// </returns>
        public static Boolean IsEmpty<T>(this IEnumerable<T> source)
        {
            return source?.Any() == false;
        }

        /// <summary>
        /// Determines whether the given sequence is empty.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{TSource}"/> to check for emptiness.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>
        ///   <c>true</c> if <paramref name="source"/> is empty; otherwise, <c>false</c>.
        /// </returns>
        public static Boolean IsEmpty<T>(this IEnumerable<T> source, Func<T, Boolean> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return source?.Any(predicate) == false;
        }

        /// <summary>
        /// Determines whether the given sequence is null or empty.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{TSource}"/> to check for null or emptiness.</param>
        /// <returns>
        ///   <c>true</c> if <paramref name="source"/> is null or empty; otherwise, <c>false</c>.
        /// </returns>
        public static Boolean IsNullOrEmpty<T>(this IEnumerable<T> source)
        {
            return source?.Any() != true;
        }

        /// <summary>
        /// Determines whether the given sequence is null or empty.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{TSource}"/> to check for null or emptiness.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>
        ///   <c>true</c> if <paramref name="source"/> is null or empty; otherwise, <c>false</c>.
        /// </returns>
        public static Boolean IsNullOrEmpty<T>(this IEnumerable<T> source, Func<T, Boolean> predicate)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return source?.Any(predicate) != true;
        }

        public static T MaxBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.OrderByDescending(selector).FirstOrDefault();
        }

        public static T MaxBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector, IComparer<TKey> comparer)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.OrderByDescending(selector, comparer).FirstOrDefault();
        }

        public static IEnumerable<T> MaxBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector, Int32 count)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.OrderByDescending(selector).Take(count);
        }

        public static IEnumerable<T> MaxBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector, IComparer<TKey> comparer, Int32 count)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.OrderByDescending(selector, comparer).Take(count);
        }

        public static T MinBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.OrderBy(selector).FirstOrDefault();
        }

        public static T MinBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector, IComparer<TKey> comparer)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.OrderBy(selector, comparer).FirstOrDefault();
        }

        public static IEnumerable<T> MinBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector, Int32 count)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.OrderBy(selector).Take(count);
        }

        public static IEnumerable<T> MinBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector, IComparer<TKey> comparer, Int32 count)
        {
            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.OrderBy(selector, comparer).Take(count);
        }

        /// <summary>
        /// Creates a sequence from start value and next element factory.
        /// </summary>
        /// <typeparam name="T">The type of element.</typeparam>
        /// <param name="start">Start value.</param>
        /// <param name="next">Next element factory.</param>
        /// <returns>Generated sequence.</returns>
        [Pure]
        [NotNull]
        public static IEnumerable<T> Create<T>(T start, [NotNull] Func<T, T> next)
        {
            if (next is null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            T cur = start;
            while (true)
            {
                yield return cur;
                cur = next(cur);
            }
        }

        /// <summary>
        /// Creates a sequence from start value and next element factory.
        /// </summary>
        /// <typeparam name="T">The type of source element.</typeparam>
        /// <typeparam name="TResult">The type of result element</typeparam>
        /// <param name="start">Start value.</param>
        /// <param name="next">Next element factory.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>Generated sequence.</returns>
        [Pure]
        [NotNull]
        public static IEnumerable<TResult> Create<T, TResult>(T start, [NotNull] Func<T, T> next, [NotNull] Func<T, TResult> selector)
        {
            if (next is null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            T cur = start;
            while (true)
            {
                yield return selector(cur);
                cur = next(cur);
            }
        }

        /// <summary>
        /// Creates a sequence from start value and next element factory.
        /// </summary>
        /// <typeparam name="T">The type of element.</typeparam>
        /// <param name="start">Start value.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="next">Next element factory.</param>
        /// <returns>Generated sequence.</returns>
        [Pure]
        [NotNull]
        public static IEnumerable<T> Create<T>(T start, [NotNull] Func<T, Boolean> predicate, [NotNull] Func<T, T> next)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            if (next is null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            T cur = start;
            while (predicate(cur))
            {
                yield return cur;
                cur = next(cur);
            }
        }

        /// <summary>
        /// Creates a sequence from start value and next element factory.
        /// </summary>
        /// <typeparam name="T">The type of source element.</typeparam>
        /// <typeparam name="TResult">The type of result element</typeparam>
        /// <param name="start">Start value.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="next">Next element factory.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>Generated sequence.</returns>
        [Pure]
        [NotNull]
        public static IEnumerable<TResult> Create<T, TResult>(T start, [NotNull] Func<T, Boolean> predicate, [NotNull] Func<T, T> next, [NotNull] Func<T, TResult> selector)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            if (next is null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            T cur = start;
            while (predicate(cur))
            {
                yield return selector(cur);
                cur = next(cur);
            }
        }

        /// <summary>
        /// Creates a sequence from start value and next element factory till factory returns null.
        /// </summary>
        /// <typeparam name="T">The type of element.</typeparam>
        /// <param name="start">Start value.</param>
        /// <param name="next">Next element factory.</param>
        /// <returns>Generated sequence.</returns>
        [Pure]
        [NotNull]
        public static IEnumerable<T> CreateWhileNotNull<T>(T start, [NotNull] Func<T, T> next) where T : class
        {
            if (next is null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            T cur = start;
            while (cur is not null)
            {
                yield return cur;
                cur = next(cur);
            }
        }

        /// <summary>
        /// Creates a sequence from start value and next element factory till factory returns null.
        /// </summary>
        /// <typeparam name="T">The type of source element.</typeparam>
        /// <typeparam name="TResult">The type of result element</typeparam>
        /// <param name="start">Start value.</param>
        /// <param name="next">Next element factory.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>Generated sequence.</returns>
        [Pure]
        [NotNull]
        public static IEnumerable<TResult> CreateWhileNotNull<T, TResult>(T start, [NotNull] Func<T, T> next, [NotNull] Func<T, TResult> selector) where T : class
        {
            if (next is null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            T cur = start;
            while (cur is not null)
            {
                yield return selector(cur);
                cur = next(cur);
            }
        }

        /// <summary>
        /// Creates a single element sequence.
        /// </summary>
        /// <typeparam name="T">The type of element.</typeparam>
        /// <param name="element">Element instance to create sequence from.</param>
        /// <returns>Single element sequence</returns>
        [Pure]
        [NotNull]
        public static IEnumerable<T> CreateSingle<T>(T element)
        {
            yield return element;
        }

        /// <summary>
        /// Creates a single element sequence.
        /// </summary>
        /// <typeparam name="T">The type of element.</typeparam>
        /// <param name="factory">Element factory.</param>
        /// <returns>Single element sequence</returns>
        [Pure]
        [NotNull]
        public static IEnumerable<T> CreateSingle<T>([NotNull] Func<T> factory)
        {
            yield return factory();
        }

        /// <summary>
        /// Determines whether all elements in this collection are equal to each other. Compares using the <see cref="object.Equals(object)"/> method.
        /// <para>This method returns true if the collection is empty or it contains only one element.</para>
        /// </summary>
        /// <typeparam name="T">The type of the elements.</typeparam>
        /// <param name="source">The collection.</param>
        public static Boolean AllSame<T>(this IEnumerable<T> source)
        {
            return AllSame(source, EqualityComparer<T>.Default);
        }

        /// <summary>
        /// Determines whether all elements in this collection are equal to each other. Compares using the <see cref="object.Equals(object)"/> method.
        /// <para>This method returns true if the collection is empty or it contains only one element.</para>
        /// </summary>
        /// <typeparam name="T">The type of the elements.</typeparam>
        /// <param name="source">The collection.</param>
        /// <param name="comparer">The comparer</param>
        public static Boolean AllSame<T>(this IEnumerable<T> source, IEqualityComparer<T> comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (comparer is null)
            {
                throw new ArgumentNullException(nameof(comparer));
            }

            if (!source.Any())
            {
                return true;
            }

            T first = source.First();
            return source.Skip(1).All(e => comparer.Equals(first, e));
        }

        /// <summary>
        /// Determines whether all elements in this collection produce the same value with the provided value selector. Compares using the <see cref="object.Equals(object)"/> method.
        /// <para>This method returns true if the collection is empty or it contains only one element.</para>
        /// </summary>
        /// <typeparam name="T">The type of the elements.</typeparam>
        /// <typeparam name="TValue">The type of the values to compare.</typeparam>
        /// <param name="source">The collection.</param>
        /// <param name="selector">A transform function to apply to each element to select the value on which to compare elements.</param>
        public static Boolean AllSame<T, TValue>(this IEnumerable<T> source, Func<T, TValue> selector)
        {
            return AllSame(source, selector, EqualityComparer<TValue>.Default);
        }
        
        /// <summary>
        /// Determines whether all elements in this collection produce the same value with the provided value selector. Compares using the <see cref="object.Equals(object)"/> method.
        /// <para>This method returns true if the collection is empty or it contains only one element.</para>
        /// </summary>
        /// <typeparam name="T">The type of the elements.</typeparam>
        /// <typeparam name="TValue">The type of the values to compare.</typeparam>
        /// <param name="source">The collection.</param>
        /// <param name="selector">A transform function to apply to each element to select the value on which to compare elements.</param>
        /// <param name="comparer">The comparer</param>
        public static Boolean AllSame<T, TValue>(this IEnumerable<T> source, Func<T, TValue> selector, IEqualityComparer<TValue> comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            if (!source.Any())
            {
                return true;
            }

            TValue sample = selector(source.First());
            return source.Skip(1).All(e => comparer.Equals(sample, selector(e)));
        }

        /// <summary>
        /// Returns distinct elements from a sequence using the provided value selector for equality comparison.
        /// </summary>
        /// <typeparam name="T">The type of the elements of source.</typeparam>
        /// <typeparam name="TValue">The type of the value on which to distinct.</typeparam>
        /// <param name="source">The source collection.</param>
        /// <param name="selector">A transform function to apply to each element to select the value on which to distinct.</param>
        public static IEnumerable<T> DistinctBy<T, TValue>(this IEnumerable<T> source, Func<T, TValue> selector)
        {
            HashSet<TValue> already = new HashSet<TValue>();

            foreach (T item in source)
            {
                if (already.Add(selector(item)))
                {
                    yield return item;
                }
            }
        }

        /// <summary>
        /// Gets collection count if <see cref="source"/> is materialized, otherwise 0.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Int32? CountIfMaterialized<T>(this IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (Equals(source, Enumerable.Empty<T>()))
            {
                return 0;
            }

            if (source == Array.Empty<T>())
            {
                return 0;
            }

            return source switch
            {
                IReadOnlyCollection<T> collection => collection.Count,
                ICollection<T> collection => collection.Count,
                _ => null
            };
        }

        /// <summary>
        /// Gets collection if <see cref="source"/> is materialized, otherwise ToArray();ed collection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="nullToEmpty"></param>
        public static IEnumerable<T> Materialize<T>(this IEnumerable<T> source, Boolean nullToEmpty = true)
        {
            return source switch
            {
                null when nullToEmpty => Enumerable.Empty<T>(),
                null => throw new ArgumentNullException(nameof(source)),
                IReadOnlyCollection<T> _ => source,
                ICollection<T> _ => source,
                _ => source.ToArray()
            };
        }

        #region ToFrozenDictionary

        /// <summary>
        /// Converts to <see cref="FrozenDictionary{TKey,TValue}"/>.
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static FrozenDictionary<TKey, TValue> ToFrozenDictionary<TKey, TValue>(this IEnumerable<TValue> source, Func<TValue, TKey> selector)
        {
            return FrozenDictionary<TKey, TValue>.Create(source, selector);
        }

        /// <summary>
        /// Converts to <see cref="FrozenDictionary{TKey, TValue}"/>.
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <param name="valueSelector"></param>
        /// <returns></returns>
        public static FrozenDictionary<TKey, TValue> ToFrozenDictionary<TSource, TKey, TValue>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TValue> valueSelector)
        {
            return FrozenDictionary<TKey, TValue>.Create(source, keySelector, valueSelector);
        }
        
        /// <summary>
        /// Converts to <see cref="FrozenStringKeyDictionary{TValue}"/>.
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static FrozenStringKeyDictionary<TValue> ToFrozenStringKeyDictionary<TValue>(this IEnumerable<TValue> source, Func<TValue, String> selector)
        {
            return FrozenStringKeyDictionary<TValue>.Create(source, selector);
        }

        /// <summary>
        /// Converts to <see cref="FrozenStringKeyDictionary{TValue}"/>.
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <param name="valueSelector"></param>
        /// <returns></returns>
        public static FrozenStringKeyDictionary<TValue> ToFrozenStringKeyDictionary<TSource, TValue>(this IEnumerable<TSource> source, Func<TSource, String> keySelector,
            Func<TSource, TValue> valueSelector)
        {
            return FrozenStringKeyDictionary<TValue>.Create(source, keySelector, valueSelector);
        }

        /// <summary>
        /// Converts to <see cref="FrozenSByteKeyDictionary{TValue}"/>.
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static FrozenSByteKeyDictionary<TValue> ToFrozenSByteKeyDictionary<TValue>(this IEnumerable<TValue> source, Func<TValue, SByte> selector)
        {
            return FrozenSByteKeyDictionary<TValue>.Create(source, selector);
        }

        /// <summary>
        /// Converts to <see cref="FrozenSByteKeyDictionary{TValue}"/>.
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <param name="valueSelector"></param>
        /// <returns></returns>
        public static FrozenSByteKeyDictionary<TValue> ToFrozenSByteKeyDictionary<TSource, TValue>(this IEnumerable<TSource> source, Func<TSource, SByte> keySelector,
            Func<TSource, TValue> valueSelector)
        {
            return FrozenSByteKeyDictionary<TValue>.Create(source, keySelector, valueSelector);
        }

        /// <summary>
        /// Converts to <see cref="FrozenByteKeyDictionary{TValue}"/>.
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static FrozenByteKeyDictionary<TValue> ToFrozenByteKeyDictionary<TValue>(this IEnumerable<TValue> source, Func<TValue, Byte> selector)
        {
            return FrozenByteKeyDictionary<TValue>.Create(source, selector);
        }

        /// <summary>
        /// Converts to <see cref="FrozenByteKeyDictionary{TValue}"/>.
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <param name="valueSelector"></param>
        /// <returns></returns>
        public static FrozenByteKeyDictionary<TValue> ToFrozenByteKeyDictionary<TSource, TValue>(this IEnumerable<TSource> source, Func<TSource, Byte> keySelector,
            Func<TSource, TValue> valueSelector)
        {
            return FrozenByteKeyDictionary<TValue>.Create(source, keySelector, valueSelector);
        }

        /// <summary>
        /// Converts to <see cref="FrozenInt16KeyDictionary{TValue}"/>.
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static FrozenInt16KeyDictionary<TValue> ToFrozenInt16KeyDictionary<TValue>(this IEnumerable<TValue> source, Func<TValue, Int16> selector)
        {
            return FrozenInt16KeyDictionary<TValue>.Create(source, selector);
        }

        /// <summary>
        /// Converts to <see cref="FrozenInt16KeyDictionary{TValue}"/>.
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <param name="valueSelector"></param>
        /// <returns></returns>
        public static FrozenInt16KeyDictionary<TValue> ToFrozenInt16KeyDictionary<TSource, TValue>(this IEnumerable<TSource> source, Func<TSource, Int16> keySelector,
            Func<TSource, TValue> valueSelector)
        {
            return FrozenInt16KeyDictionary<TValue>.Create(source, keySelector, valueSelector);
        }
        
        /// <summary>
        /// Converts to <see cref="FrozenUInt16KeyDictionary{TValue}"/>.
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static FrozenUInt16KeyDictionary<TValue> ToFrozenUInt16KeyDictionary<TValue>(this IEnumerable<TValue> source, Func<TValue, UInt16> selector)
        {
            return FrozenUInt16KeyDictionary<TValue>.Create(source, selector);
        }

        /// <summary>
        /// Converts to <see cref="FrozenUInt16KeyDictionary{TValue}"/>.
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <param name="valueSelector"></param>
        /// <returns></returns>
        public static FrozenUInt16KeyDictionary<TValue> ToFrozenUInt16KeyDictionary<TSource, TValue>(this IEnumerable<TSource> source, Func<TSource, UInt16> keySelector,
            Func<TSource, TValue> valueSelector)
        {
            return FrozenUInt16KeyDictionary<TValue>.Create(source, keySelector, valueSelector);
        }

        /// <summary>
        /// Converts to <see cref="FrozenInt32KeyDictionary{TValue}"/>.
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static FrozenInt32KeyDictionary<TValue> ToFrozenInt32KeyDictionary<TValue>(this IEnumerable<TValue> source, Func<TValue, Int32> selector)
        {
            return FrozenInt32KeyDictionary<TValue>.Create(source, selector);
        }

        /// <summary>
        /// Converts to <see cref="FrozenInt32KeyDictionary{TValue}"/>.
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <param name="valueSelector"></param>
        /// <returns></returns>
        public static FrozenInt32KeyDictionary<TValue> ToFrozenInt32KeyDictionary<TSource, TValue>(this IEnumerable<TSource> source, Func<TSource, Int32> keySelector, 
            Func<TSource, TValue> valueSelector)
        {
            return FrozenInt32KeyDictionary<TValue>.Create(source, keySelector, valueSelector);
        }

        /// <summary>
        /// Converts to <see cref="FrozenUInt32KeyDictionary{TValue}"/>.
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static FrozenUInt32KeyDictionary<TValue> ToFrozenUInt32KeyDictionary<TValue>(this IEnumerable<TValue> source, Func<TValue, UInt32> selector)
        {
            return FrozenUInt32KeyDictionary<TValue>.Create(source, selector);
        }

        /// <summary>
        /// Converts to <see cref="FrozenUInt32KeyDictionary{TValue}"/>.
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <param name="valueSelector"></param>
        /// <returns></returns>
        public static FrozenUInt32KeyDictionary<TValue> ToFrozenUInt32KeyDictionary<TSource, TValue>(this IEnumerable<TSource> source, Func<TSource, UInt32> keySelector,
            Func<TSource, TValue> valueSelector)
        {
            return FrozenUInt32KeyDictionary<TValue>.Create(source, keySelector, valueSelector);
        }

        /// <summary>
        /// Converts to <see cref="FrozenInt64KeyDictionary{TValue}"/>.
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static FrozenInt64KeyDictionary<TValue> ToFrozenInt64KeyDictionary<TValue>(this IEnumerable<TValue> source, Func<TValue, Int64> selector)
        {
            return FrozenInt64KeyDictionary<TValue>.Create(source, selector);
        }

        /// <summary>
        /// Converts to <see cref="FrozenInt64KeyDictionary{TValue}"/>.
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <param name="valueSelector"></param>
        /// <returns></returns>
        public static FrozenInt64KeyDictionary<TValue> ToFrozenInt64KeyDictionary<TSource, TValue>(this IEnumerable<TSource> source, Func<TSource, Int64> keySelector,
            Func<TSource, TValue> valueSelector)
        {
            return FrozenInt64KeyDictionary<TValue>.Create(source, keySelector, valueSelector);
        }

        /// <summary>
        /// Converts to <see cref="FrozenUInt64KeyDictionary{TValue}"/>.
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static FrozenUInt64KeyDictionary<TValue> ToFrozenUInt64KeyDictionary<TValue>(this IEnumerable<TValue> source, Func<TValue, UInt64> selector)
        {
            return FrozenUInt64KeyDictionary<TValue>.Create(source, selector);
        }

        /// <summary>
        /// Converts to <see cref="FrozenUInt64KeyDictionary{TValue}"/>.
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <param name="valueSelector"></param>
        /// <returns></returns>
        public static FrozenUInt64KeyDictionary<TValue> ToFrozenUInt64KeyDictionary<TSource, TValue>(this IEnumerable<TSource> source, Func<TSource, UInt64> keySelector,
            Func<TSource, TValue> valueSelector)
        {
            return FrozenUInt64KeyDictionary<TValue>.Create(source, keySelector, valueSelector);
        }

        #endregion
    }
}