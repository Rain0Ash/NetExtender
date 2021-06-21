// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using NetExtender.Random.Interfaces;
using NetExtender.Types.Collections;
using NetExtender.Types.Sets.Interfaces;
using NetExtender.Utils.Numerics;
using NetExtender.Utils.Core;

namespace NetExtender.Utils.Types
{
    [SuppressMessage("ReSharper", "IteratorNeverReturns")]
    public static class EnumerableUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerator<T> GetEmptyEnumerator<T>()
        {
            return Enumerable.Empty<T>().GetEnumerator();
        }

        public static IEnumerable<T> GetEnumerableFrom<T>(T value)
        {
            yield return value;
        }

        public static IEnumerable<T> GetEnumerableFrom<T>(T value, params T[]? other)
        {
            yield return value;

            if (other is null || other.Length <= 0)
            {
                yield break;
            }
            
            foreach (T item in other)
            {
                yield return item;
            }
        }

        public static IEnumerable<T> GetEnumerableFrom<T>(T first, T second)
        {
            yield return first;
            yield return second;
        }

        public static IEnumerable<T> GetEnumerableFrom<T>(T first, T second, params T[]? other)
        {
            yield return first;
            yield return second;
            
            if (other is null || other.Length <= 0)
            {
                yield break;
            }

            foreach (T item in other)
            {
                yield return item;
            }
        }

        public static IEnumerable<T> GetEnumerableFrom<T>(T first, T second, T third)
        {
            yield return first;
            yield return second;
            yield return third;
        }

        public static IEnumerable<T> GetEnumerableFrom<T>(T first, T second, T third, params T[]? other)
        {
            yield return first;
            yield return second;
            yield return third;

            if (other is null || other.Length <= 0)
            {
                yield break;
            }
            
            foreach (T item in other)
            {
                yield return item;
            }
        }

        public static IEnumerable<T> GetEnumerableFrom<T>(Func<T> generator)
        {
            if (generator is null)
            {
                throw new ArgumentNullException(nameof(generator));
            }

            while (true)
            {
                yield return generator();
            }
        }

        public static IEnumerable<(Int32 counter, T item)> Enumerate<T>(this IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            Int32 counter = 0;
            return source.Select(item => (counter++, item));
        }

        public static IEnumerable<(Int64 counter, T item)> LongEnumerate<T>(this IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            Int64 counter = 0;
            return source.Select(item => (counter++, item));
        }

        public static Int32 CountWhile<T>(this IEnumerable<T> source, Func<T, Boolean> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            Int32 count = 0;
            foreach (T item in source)
            {
                if (!predicate(item))
                {
                    return count;
                }

                count++;
            }

            return count;
        }

        public static Int64 LongCountWhile<T>(this IEnumerable<T> source, Func<T, Boolean> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            Int64 count = 0;
            foreach (T item in source)
            {
                if (!predicate(item))
                {
                    return count;
                }

                count++;
            }

            return count;
        }

        public static Int32 ReverseCountWhile<T>(this IEnumerable<T> source, Func<T, Boolean> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return source.Reverse().CountWhile(predicate);
        }

        public static Int64 ReverseLongCountWhile<T>(this IEnumerable<T> source, Func<T, Boolean> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return source.Reverse().LongCountWhile(predicate);
        }

        public static Int32 FindIndex<T>(this IEnumerable<T> source, Func<T, Boolean> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            foreach ((Int32 counter, T item) in source.Enumerate())
            {
                if (predicate(item))
                {
                    return counter;
                }
            }

            return -1;
        }

        public static Int64 LongFindIndex<T>(this IEnumerable<T> source, Func<T, Boolean> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            foreach ((Int64 counter, T item) in source.LongEnumerate())
            {
                if (predicate(item))
                {
                    return counter;
                }
            }

            return -1;
        }

        public static IEnumerable<T> Append<T>(this IEnumerable<T>? source, IEnumerable<T>? additional)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return additional is null ? source : source.Concat(additional);
        }

        public static IEnumerable<T> Append<T>(this IEnumerable<T>? source, params IEnumerable<T>?[]? additionals)
        {
            return Concat(source, additionals);
        }

        public static IEnumerable<T> Append<T>(this IEnumerable<T>? source, T value, params T[]? values)
        {
            if (source is not null)
            {
                foreach (T item in source)
                {
                    yield return item;
                }
            }

            yield return value;

            if (values is null)
            {
                yield break;
            }

            foreach (T item in values)
            {
                yield return item;
            }
        }

        public static IEnumerable<T> Prepend<T>(this IEnumerable<T>? source, IEnumerable<T>? additional)
        {
            return source.Preconcat(additional);
        }

        public static IEnumerable<T> Prepend<T>(this IEnumerable<T>? source, params IEnumerable<T>?[]? additionals)
        {
            return source.Preconcat(additionals);
        }

        public static IEnumerable<T> Prepend<T>(this IEnumerable<T>? source, T value, params T[]? values)
        {
            yield return value;

            if (values is not null)
            {
                foreach (T item in values)
                {
                    yield return item;
                }
            }

            if (source is null)
            {
                yield break;
            }

            foreach (T item in source)
            {
                yield return item;
            }
        }

        public static IEnumerable<T> Concat<T>(this IEnumerable<T>? source, params IEnumerable<T>?[]? additionals)
        {
            if (source is not null)
            {
                foreach (T item in source)
                {
                    yield return item;
                }
            }

            if (additionals is null)
            {
                yield break;
            }

            foreach (IEnumerable<T>? next in additionals)
            {
                if (next is null)
                {
                    continue;
                }

                foreach (T item in next)
                {
                    yield return item;
                }
            }
        }

        public static IEnumerable<T> Preconcat<T>(this IEnumerable<T>? source, IEnumerable<T>? additional)
        {
            if (additional is not null)
            {
                foreach (T item in additional)
                {
                    yield return item;
                }
            }

            if (source is null)
            {
                yield break;
            }

            foreach (T item in source)
            {
                yield return item;
            }
        }

        public static IEnumerable<T> Preconcat<T>(this IEnumerable<T>? source, params IEnumerable<T>?[]? additionals)
        {
            if (additionals is not null)
            {
                foreach (IEnumerable<T>? next in additionals)
                {
                    if (next is null)
                    {
                        continue;
                    }

                    foreach (T item in next)
                    {
                        yield return item;
                    }
                }
            }

            if (source is null)
            {
                yield break;
            }

            foreach (T item in source)
            {
                yield return item;
            }
        }

        public static T ElementAtOr<T>(this IEnumerable<T> source, Int32 index, T alternate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (index < 0)
            {
                return alternate;
            }

            if (source is IList<T> list)
            {
                return index < list.Count ? list[index] : alternate;
            }

            using IEnumerator<T> enumerator = source.GetEnumerator();

            while (enumerator.MoveNext())
            {
                if (index-- == 0)
                {
                    return enumerator.Current;
                }
            }

            return alternate;
        }
        
        public static T ElementAtOr<T>(this IEnumerable<T> source, Int32 index, Func<T> alternate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            if (index < 0)
            {
                return alternate();
            }

            if (source is IList<T> list)
            {
                return index < list.Count ? list[index] : alternate();
            }

            using IEnumerator<T> e = source.GetEnumerator();

            while (e.MoveNext())
            {
                if (index-- == 0)
                {
                    return e.Current;
                }
            }

            return alternate();
        }

        public static Boolean InBounds<T>(this IEnumerable<T> source, Int32 index)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source switch
            {
                ICollection<T> collection => index >= 0 && index < collection.Count,
                IReadOnlyCollection<T> collection => index >= 0 && index < collection.Count,
                ICollection collection => index >= 0 && index < collection.Count,
                _ => index >= 0 && index < source.Count()
            };
        }
        
        public static T? TryGetValue<T>(this IEnumerable<T> source, Int32 index)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return TryGetValue(source, index, out T? value) ? value : default;
        }
        
        public static T TryGetValue<T>(this IEnumerable<T> source, Int32 index, T alternate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return TryGetValue(source, index, out T? value) ? value : alternate;
        }
        
        public static T TryGetValue<T>(this IEnumerable<T> source, Int32 index, Func<T> alternate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            TryGetValue(source, index, alternate, out T value);
            return value;
        }

        public static Boolean TryGetValue<T>(this IEnumerable<T> source, Int32 index, [MaybeNullWhen(false)] out T value)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return TryGetValue(source, index, default(T), out value);
        }

        public static Boolean TryGetValue<T>(this IEnumerable<T> source, Int32 index, T alternate, out T value)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            Boolean result;
            (value, result) = source switch
            {
                IList<T> collection => index >= 0 && index < collection.Count ? (collection[index], true) : (alternate, false),
                IReadOnlyList<T> collection => index >= 0 && index < collection.Count ? (collection[index], true) : (alternate, false),
                ICollection<T> collection => index >= 0 && index < collection.Count ? (collection.ElementAt(index), true) : (alternate, false),
                IReadOnlyCollection<T> collection => index >= 0 && index < collection.Count ? (collection.ElementAt(index), true) : (alternate, false),
                _ => TryGetValueInternal(source, index, out value!) ? (value, true) : (alternate, false)
            };

            return result;
        }

        public static Boolean TryGetValue<T>(this IEnumerable<T> source, Int32 index, Func<T> alternate, out T value)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            Boolean result;
            (value, result) = source switch
            {
                IList<T> collection => index >= 0 && index < collection.Count ? (collection[index], true) : (alternate(), false),
                IReadOnlyList<T> collection => index >= 0 && index < collection.Count ? (collection[index], true) : (alternate(), false),
                ICollection<T> collection => index >= 0 && index < collection.Count ? (collection.ElementAt(index), true) : (alternate(), false),
                IReadOnlyCollection<T> collection => index >= 0 && index < collection.Count ? (collection.ElementAt(index), true) : (alternate(), false),
                _ => TryGetValueInternal(source, index, out value!) ? (value, true) : (alternate(), false)
            };

            return result;
        }
        
        private static Boolean TryGetValueInternal<T>(IEnumerable<T> source, Int32 index, [MaybeNullWhen(false)] out T value)
        {
            using IEnumerator<T> enumerator = source.GetEnumerator();

            if (index < 0)
            {
                value = default;
                return false;
            }

            Int32 i = 0;
            while (enumerator.MoveNext() && i++ < index) { }

            if (i == index)
            {
                value = enumerator.Current;
                return true;
            }

            value = default;
            return false;
        }

        public static T? GetRandom<T>(this IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source switch
            {
                IList<T> list => list.GetRandom(),
                IReadOnlyList<T> list => list.Count <= 0 ? default : list[RandomUtils.NextNonNegative(list.Count - 1)],
                ICollection<T> collection => collection.Count <= 0 ? default : collection.ElementAtOrDefault(RandomUtils.NextNonNegative(collection.Count - 1)),
                IReadOnlyCollection<T> collection => collection.Count <= 0 ? default : collection.ElementAtOrDefault(RandomUtils.NextNonNegative(collection.Count - 1)),
                _ => source.ToList().GetRandom()
            };
        }

        public static Boolean TryGetFirst<T>(this IEnumerable<T> source, [MaybeNullWhen(false)] out T result)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            foreach (T item in source)
            {
                result = item;
                return true;
            }
            
            result = default;
            return false;
        }

        public static Boolean TryGetFirst<T>(this IEnumerable<T> source, Func<T, Boolean> predicate, [MaybeNullWhen(false)] out T result)
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
                    continue;
                }
                
                result = item;
                return true;
            }
            
            result = default;
            return false;
        }

        public static Boolean TryGetLast<T>(this IEnumerable<T> source, [MaybeNullWhen(false)] out T result)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            try
            {
                result = source.Last();
                return true;
            }
            catch (InvalidOperationException)
            {
                result = default;
                return false;
            }
        }

        public static Boolean TryGetLast<T>(this IEnumerable<T> source, Func<T, Boolean> predicate, [MaybeNullWhen(false)] out T result)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            try
            {
                result = source.Last(predicate);
                return true;
            }
            catch (InvalidOperationException)
            {
                result = default;
                return false;
            }
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

        public static IEnumerable<T> AppendAggregate<T>(this IEnumerable<T> source, Func<T, T, T> aggregate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (aggregate is null)
            {
                throw new ArgumentNullException(nameof(aggregate));
            }

            using IEnumerator<T> e = source.GetEnumerator();
            if (!e.MoveNext())
            {
                throw new ArgumentException("");
            }

            T result = e.Current;
            yield return result;
            
            while (e.MoveNext())
            {
                result = aggregate(result, e.Current);
                yield return e.Current;
            }

            yield return result;
        }
        
        public static IEnumerable<T> AppendAggregate<T>(this IEnumerable<T> source, T seed, Func<T, T, T> aggregate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (aggregate is null)
            {
                throw new ArgumentNullException(nameof(aggregate));
            }

            T result = seed;
            
            using IEnumerator<T> e = source.GetEnumerator();
            if (!e.MoveNext())
            {
                yield return result;
                yield break;
            }
            
            yield return e.Current;
            
            while (e.MoveNext())
            {
                result = aggregate(result, e.Current);
                yield return e.Current;
            }

            yield return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<T> SelectMany<T>(this IEnumerable<IEnumerable<T>> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.SelectMany(item => item);
        }

        public static IEnumerable<TResult> SelectManyWhere<T, TResult>(this IEnumerable<T> source, Func<T, Boolean> where, Func<T, IEnumerable<TResult>> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.Where(where).SelectMany(selector);
        }

        public static IEnumerable<TResult> SelectManyWhereNot<T, TResult>(this IEnumerable<T> source, Func<T, Boolean> where, Func<T, IEnumerable<TResult>> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.WhereNot(where).SelectMany(selector);
        }

        public static IEnumerable<TResult?> TrySelect<T, TResult>(this IEnumerable<T> source, Func<T, TResult> predicate)
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
                TResult? value;
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
        public static IEnumerable<TResult?> TrySelectWhere<T, TResult>(this IEnumerable<T> source, Func<T, Boolean> where, Func<T, TResult> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return source.Where(where).TrySelect(predicate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TResult?> TrySelectWhereNot<T, TResult>(this IEnumerable<T> source, Func<T, Boolean> where, Func<T, TResult> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return TrySelectWhere(source, item => !where(item), predicate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TResult> TrySelectWhere<T, TResult>(this IEnumerable<T> source, Func<T, Boolean> where, TryParseHandler<T, TResult> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return SelectWhere(source, where, predicate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TResult> TrySelectWhereNot<T, TResult>(this IEnumerable<T> source, Func<T, Boolean> where, TryParseHandler<T, TResult> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return TrySelectWhere(source, item => !where(item), predicate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TResult> TryParse<T, TResult>(this IEnumerable<T> source, TryParseHandler<T, TResult> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return SelectWhere(source, predicate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TResult> TryParseWhere<T, TResult>(this IEnumerable<T> source, Func<T, Boolean> where, TryParseHandler<T, TResult> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return TrySelectWhere(source, where, predicate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TOutput> TryParseWhereNot<T, TOutput>(this IEnumerable<T> source, Func<T, Boolean> where, TryParseHandler<T, TOutput> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return TrySelectWhereNot(source, where, predicate);
        }

        public static IEnumerable<TOutput> SelectWhere<T, TOutput>(this IEnumerable<T> source, TryParseHandler<T, TOutput> predicate)
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
                if (predicate(item, out TOutput value))
                {
                    yield return value;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TOutput> SelectWhere<T, TOutput>(this IEnumerable<T> source, Func<T, Boolean> where, TryParseHandler<T, TOutput> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return source.Where(where).SelectWhere(predicate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TOut> SelectWhereNot<T, TOut>(this IEnumerable<T> source, Func<T, Boolean> where, TryParseHandler<T, TOut> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return source.WhereNot(where).SelectWhere(predicate);
        }

        public static IEnumerable<TOut> SelectWhere<T, TOut>(this IEnumerable<T> source, Func<T, (Boolean, TOut)> predicate)
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
                (Boolean successful, TOut output) = predicate(item);

                if (successful)
                {
                    yield return output;
                }
            }
        }

        public static IEnumerable<TOut> SelectWhere<T, TOut>(this IEnumerable<T> source, Func<T, Boolean> where, Func<T, TOut> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.Where(where).Select(selector);
        }

        public static IEnumerable<TOut> SelectWhereNot<T, TOut>(this IEnumerable<T> source, Func<T, Boolean> where, Func<T, TOut> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.WhereNot(where).Select(selector);
        }
        
        public static IEnumerable<TOut> SelectWhere<T, TOut>(this IEnumerable<T> source, Func<T, Int32, Boolean> where, Func<T, TOut> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.Where(where).Select(selector);
        }

        public static IEnumerable<TOut> SelectWhereNot<T, TOut>(this IEnumerable<T> source, Func<T, Int32, Boolean> where, Func<T, TOut> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.WhereNot(where).Select(selector);
        }
        
        public static IEnumerable<TOut> SelectWhere<T, TOut>(this IEnumerable<T> source, Func<T, Boolean> where, Func<T, Int32, TOut> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.Where(where).Select(selector);
        }

        public static IEnumerable<TOut> SelectWhereNot<T, TOut>(this IEnumerable<T> source, Func<T, Boolean> where, Func<T, Int32, TOut> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.WhereNot(where).Select(selector);
        }
        
        public static IEnumerable<TOut> SelectWhere<T, TOut>(this IEnumerable<T> source, Func<T, Int32, Boolean> where, Func<T, Int32, TOut> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.Where(where).Select(selector);
        }

        public static IEnumerable<TOut> SelectWhereNot<T, TOut>(this IEnumerable<T> source, Func<T, Int32, Boolean> where, Func<T, Int32, TOut> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.WhereNot(where).Select(selector);
        }

        public static IEnumerable<T> Change<T>(this IEnumerable<T> source, Func<T, T> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.Select(selector);
        }
        
        public static IEnumerable<T> Change<T>(this IEnumerable<T> source, Func<T, Int32, T> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.Select(selector);
        }

        public static IEnumerable<T> ChangeWhere<T>(this IEnumerable<T> source, Func<T, (Boolean, T)> selector)
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
                (Boolean successful, T output) = selector(item);

                yield return successful ? output : item;
            }
        }

        public static IEnumerable<T> ChangeWhere<T>(this IEnumerable<T> source, Func<T, Boolean> where, Func<T, T> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.Select(item => where(item) ? selector(item) : item);
        }

        public static IEnumerable<T> ChangeWhereNot<T>(this IEnumerable<T> source, Func<T, Boolean> where, Func<T, T> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return ChangeWhere(source, item => !where(item), selector);
        }
        
        public static IEnumerable<T> ChangeWhere<T>(this IEnumerable<T> source, Func<T, Int32, Boolean> where, Func<T, T> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.Select((item, index) => !where(item, index) ? selector(item) : item);
        }

        public static IEnumerable<T> ChangeWhereNot<T>(this IEnumerable<T> source, Func<T, Int32, Boolean> where, Func<T, T> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return ChangeWhere(source, (item, index) => !where(item, index), selector);
        }
        
        public static IEnumerable<T> ChangeWhere<T>(this IEnumerable<T> source, Func<T, Boolean> where, Func<T, Int32, T> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.Select((item, index) => where(item) ? selector(item, index) : item);
        }

        public static IEnumerable<T> ChangeWhereNot<T>(this IEnumerable<T> source, Func<T, Boolean> where, Func<T, Int32, T> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return ChangeWhere(source, item => !where(item), selector);
        }
        
        public static IEnumerable<T> ChangeWhere<T>(this IEnumerable<T> source, Func<T, Int32, Boolean> where, Func<T, Int32, T> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.Select((item, index) => where(item, index) ? selector(item, index) : item);
        }

        public static IEnumerable<T> ChangeWhereNot<T>(this IEnumerable<T> source, Func<T, Int32, Boolean> where, Func<T, Int32, T> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return ChangeWhere(source, (item, index) => !where(item, index), selector);
        }

        public static IEnumerable<TTo?> SelectAs<TFrom, TTo>(this IEnumerable<TFrom> source) where TTo : class
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            foreach (TFrom item in source)
            {
                yield return item as TTo;
            }
        }

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

        public static IEnumerable<TTo> SelectWhereIs<TFrom, TTo>(this IEnumerable<TFrom> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            foreach (TFrom item in source)
            {
                if (item is TTo convert)
                {
                    yield return convert;
                }
            }
        }

        public static IEnumerable<TResult> SelectWhereNotNull<T, TResult>(this IEnumerable<T> source, Func<T, TResult> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.WhereNotNull().Select(selector);
        }

        public static IEnumerable<TResult> SelectManyWhereNotNull<T, TResult>(this IEnumerable<T> source, Func<T, IEnumerable<TResult>> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.SelectManyWhere(item => item is not null, selector);
        }

        public static IEnumerable<T> SelectWhereNotNull<T>(this IEnumerable<T?> source) where T : struct
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Where(item => item.HasValue).Select(item => item!.Value);
        }

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

        public static IOrderedEnumerable<T> Sort<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector)
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

        public static IOrderedEnumerable<T> Sort<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector, IComparer<TKey>? comparer)
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

        public static IOrderedEnumerable<T> SortDescending<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector)
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

        public static IOrderedEnumerable<T> SortDescending<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector, IComparer<TKey>? comparer)
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

        public static T FirstOr<T>(this IEnumerable<T> source, T alternate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            foreach (T item in source)
            {
                return item;
            }

            return alternate;
        }

        public static T FirstOr<T>(this IEnumerable<T> source, Func<T, Boolean> predicate, T alternate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

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
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

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
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

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
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return FirstOr(source.Reverse(), alternate);
        }

        public static T LastOr<T>(this IEnumerable<T> source, Func<T, Boolean> predicate, T alternate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return FirstOr(source.Reverse(), predicate, alternate);
        }

        public static T LastOr<T>(this IEnumerable<T> source, Func<T> alternate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            return FirstOr(source.Reverse(), alternate);
        }

        public static T LastOr<T>(this IEnumerable<T> source, Func<T, Boolean> predicate, Func<T> alternate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

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
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

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
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

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

        public static IEnumerable<T> Replace<T>(this IEnumerable<T> source, T what, T to, IEqualityComparer<T>? comparer = null)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

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
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            switch (size)
            {
                case <= 0:
                    throw new ArgumentOutOfRangeException(nameof(size));
                case 1:
                {
                    foreach (T item in source)
                    {
                        yield return new[] {item};
                    }

                    yield break;
                }
            }

            T[]? chunk = null;
            Int32 index = 0;

            foreach (T element in source)
            {
                chunk ??= new T[size];
                chunk[index++] = element;

                if (index < size)
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
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
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
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (next is null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
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
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

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
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

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
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

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
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

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
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return Shuffle(source, RandomUtils.Generator);
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, System.Random random)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (random is null)
            {
                throw new ArgumentNullException(nameof(random));
            }

            T[] buffer = source.ToArray();

            for (Int32 i = 0; i < buffer.Length; i++)
            {
                Int32 j = random.Next(i, buffer.Length);
                yield return buffer[j];

                buffer[j] = buffer[i];
            }
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, IRandom random)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (random is null)
            {
                throw new ArgumentNullException(nameof(random));
            }

            T[] buffer = source.ToArray();

            for (Int32 i = 0; i < buffer.Length; i++)
            {
                Int32 j = random.Next(i, buffer.Length);
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
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            // ReSharper disable once PossibleMultipleEnumeration
            if (source.CountIfMaterialized() >= count)
            {
                return true;
            }

            if (count <= 0)
            {
                return true;
            }

            Int32 matches = 0;
            // ReSharper disable once PossibleMultipleEnumeration
            return source.Any(_ => ++matches >= count);
        }

        /// <summary>
        /// Determines whether the specified sequence's element count is equal to or greater than <paramref name="count"/>.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{TSource}"/> whose elements to count.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="count">The minimum number of elements the specified sequence is expected to contain.</param>
        /// <returns>
        ///   <c>true</c> if the element count of <paramref name="source"/> is equal to or greater than <paramref name="count"/>; otherwise, <c>false</c>.
        /// </returns>
        public static Boolean HasAtLeast<T>(this IEnumerable<T> source, Func<T, Boolean> predicate, Int32 count)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            if (count <= 0)
            {
                return true;
            }

            Int32 matches = 0;
            return source.Where(predicate).Any(_ => ++matches >= count);
        }
        
        /// <summary>
        /// Determines whether the specified sequence's element count is equal to or greater than <paramref name="count"/>.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{TSource}"/> whose elements to count.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="count">The minimum number of elements the specified sequence is expected to contain.</param>
        /// <returns>
        ///   <c>true</c> if the element count of <paramref name="source"/> is equal to or greater than <paramref name="count"/>; otherwise, <c>false</c>.
        /// </returns>
        public static Boolean HasAtLeast<T>(this IEnumerable<T> source, Func<T, Int32, Boolean> predicate, Int32 count)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            if (count <= 0)
            {
                return true;
            }

            Int32 matches = 0;
            return source.Where(predicate).Any(_ => ++matches >= count);
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
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            // ReSharper disable once PossibleMultipleEnumeration
            if (source.CountIfMaterialized() <= count)
            {
                return true;
            }

            if (count <= 0)
            {
                return false;
            }

            Int32 matches = 0;
            // ReSharper disable once PossibleMultipleEnumeration
            return source.All(_ => ++matches <= count);
        }

        /// <summary>
        /// Determines whether the specified sequence contains at most <paramref name="count"/> elements satisfying a condition.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{TSource}"/> whose elements to count.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="count">The maximum number of elements satisfying the specified condition the specified sequence is expected to contain.</param>
        /// <returns>
        ///   <c>true</c> if the element count of satisfying elements is equal to or less than <paramref name="count"/>; otherwise, <c>false</c>.
        /// </returns>
        public static Boolean HasAtMost<T>(this IEnumerable<T> source, Func<T, Boolean> predicate, Int32 count)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            if (count <= 0)
            {
                return false;
            }

            Int32 matches = 0;
            return source.Where(predicate).All(_ => ++matches <= count);
        }
        
        /// <summary>
        /// Determines whether the specified sequence contains at most <paramref name="count"/> elements satisfying a condition.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{TSource}"/> whose elements to count.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="count">The maximum number of elements satisfying the specified condition the specified sequence is expected to contain.</param>
        /// <returns>
        ///   <c>true</c> if the element count of satisfying elements is equal to or less than <paramref name="count"/>; otherwise, <c>false</c>.
        /// </returns>
        public static Boolean HasAtMost<T>(this IEnumerable<T> source, Func<T, Int32, Boolean> predicate, Int32 count)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            if (count <= 0)
            {
                return false;
            }

            Int32 matches = 0;
            return source.Where(predicate).All(_ => ++matches <= count);
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
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            // ReSharper disable once PossibleMultipleEnumeration
            if (source.CountIfMaterialized() == count)
            {
                return true;
            }

            Int32 matches = 0;

            // ReSharper disable once PossibleMultipleEnumeration
            if (source.Any(_ => ++matches > count))
            {
                return false;
            }

            return matches == count;
        }

        /// <summary>
        /// Determines whether the specified sequence contains exactly the specified number of elements satisfying the specified condition.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{TSource}"/> to count satisfying elements.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="count">The number of matching elements the specified sequence is expected to contain.</param>
        /// <returns>
        ///   <c>true</c> if <paramref name="source"/> contains exactly <paramref name="count"/> elements satisfying the condition; otherwise, <c>false</c>.
        /// </returns>
        public static Boolean HasExactly<T>(this IEnumerable<T> source, Func<T, Boolean> predicate, Int32 count)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            Int32 matches = 0;

            if (source.Where(predicate).Any(_ => ++matches > count))
            {
                return false;
            }

            return matches == count;
        }
        
        /// <summary>
        /// Determines whether the specified sequence contains exactly the specified number of elements satisfying the specified condition.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{TSource}"/> to count satisfying elements.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="count">The number of matching elements the specified sequence is expected to contain.</param>
        /// <returns>
        ///   <c>true</c> if <paramref name="source"/> contains exactly <paramref name="count"/> elements satisfying the condition; otherwise, <c>false</c>.
        /// </returns>
        public static Boolean HasExactly<T>(this IEnumerable<T> source, Func<T, Int32, Boolean> predicate, Int32 count)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
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
        public static IEnumerable<T> Without<T>(IEnumerable<T> source, IEnumerable<T> without, IEqualityComparer<T>? comparer = null)
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

        public static Boolean ContainsAny<T>(this IEnumerable<T> source, T value, params T[] values) where T : IComparable<T>
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Any(item => values.Prepend(value).Any(comparable => comparable.CompareTo(item) == 0));
        }

        public static Boolean ContainsAny<T>(this IEnumerable<T> source, T[] values, IComparer<T>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (values is null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            comparer ??= Comparer<T>.Default;
            return values.Length == 0 || source.Any(item => values.Any(comparable => comparer.Compare(comparable, item) == 0));
        }

        public static IEnumerable<T> NotEmptyOr<T>(this IEnumerable<T> source, params T[] alternate)
        {
            return alternate?.Length > 0 ? NotEmptyOr(source, (IEnumerable<T>) alternate) : source;
        }

        public static IEnumerable<T> NotEmptyOr<T>(this IEnumerable<T>? source, IEnumerable<T> alternate)
        {
            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            if (source is null)
            {
                foreach (T item in alternate)
                {
                    yield return item;
                }
                
                yield break;
            }

            using IEnumerator<T> enumerator = source.GetEnumerator();

            if (!enumerator.MoveNext())
            {
                foreach (T item in alternate)
                {
                    yield return item;
                }
                
                yield break;
            }

            do
            {
                yield return enumerator.Current;
                
            } while (enumerator.MoveNext());
        }

        /// <summary>
        /// Determines whether the given sequence is not empty.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{TSource}"/> to check for emptiness.</param>
        /// <returns>
        ///   <c>true</c> if <paramref name="source"/> is empty; otherwise, <c>false</c>.
        /// </returns>
        public static Boolean IsNotEmpty<T>(this IEnumerable<T>? source)
        {
            return source is not null && source.Any();
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
        public static Boolean IsNotEmpty<T>(this IEnumerable<T>? source, Func<T, Boolean> predicate)
        {
            if (source is null)
            {
                return false;
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return source.Any(predicate);
        }

        /// <summary>
        /// Determines whether the given sequence is empty.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{TSource}"/> to check for emptiness.</param>
        /// <returns>
        ///   <c>true</c> if <paramref name="source"/> is empty; otherwise, <c>false</c>.
        /// </returns>
        public static Boolean IsEmpty<T>(this IEnumerable<T>? source)
        {
            if (source is null)
            {
                return false;
            }

            return !source.Any();
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
        public static Boolean IsEmpty<T>(this IEnumerable<T>? source, Func<T, Boolean> predicate)
        {
            if (source is null)
            {
                return false;
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return !source.Any(predicate);
        }

        /// <summary>
        /// Determines whether the given sequence is null or empty.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{TSource}"/> to check for null or emptiness.</param>
        /// <returns>
        ///   <c>true</c> if <paramref name="source"/> is null or empty; otherwise, <c>false</c>.
        /// </returns>
        public static Boolean IsNullOrEmpty<T>(this IEnumerable<T>? source)
        {
            if (source is null)
            {
                return true;
            }

            return !source.Any();
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
        public static Boolean IsNullOrEmpty<T>(this IEnumerable<T>? source, Func<T, Boolean> predicate)
        {
            if (source is null)
            {
                return true;
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return !source.Any(predicate);
        }

        public static T? MaxBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.OrderByDescending(selector).FirstOrDefault();
        }

        public static T? MaxBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector, IComparer<TKey> comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.OrderByDescending(selector, comparer).FirstOrDefault();
        }

        public static IEnumerable<T> MaxBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector, Int32 count)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.OrderByDescending(selector).Take(count);
        }

        public static IEnumerable<T> MaxBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector, IComparer<TKey> comparer, Int32 count)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.OrderByDescending(selector, comparer).Take(count);
        }

        public static T? MinBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.OrderBy(selector).FirstOrDefault();
        }

        public static T? MinBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector, IComparer<TKey> comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.OrderBy(selector, comparer).FirstOrDefault();
        }

        public static IEnumerable<T> MinBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector, Int32 count)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.OrderBy(selector).Take(count);
        }

        public static IEnumerable<T> MinBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector, IComparer<TKey> comparer, Int32 count)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.OrderBy(selector, comparer).Take(count);
        }

        public static Double AverageOr(this IEnumerable<Int32> source, Double seed)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            using IEnumerator<Int32> e = source.GetEnumerator();
            if (!e.MoveNext())
            {
                return seed;
            }

            Int64 sum = e.Current;
            Int64 count = 1;
            checked
            {
                while (e.MoveNext())
                {
                    sum += e.Current;
                    ++count;
                }
            }

            return (Double) sum / count;
        }

        public static Double? AverageOrDefault(this IEnumerable<Int32> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            using IEnumerator<Int32> enumerator = source.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                return null;
            }

            Int64 sum = enumerator.Current;
            Int64 count = 1;
            checked
            {
                while (enumerator.MoveNext())
                {
                    sum += enumerator.Current;
                    ++count;
                }
            }

            return (Double) sum / count;
        }

        public static Double AverageOr(this IEnumerable<Int64> source, Double seed)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            using IEnumerator<Int64> enumerator = source.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                return seed;
            }

            Int64 sum = enumerator.Current;
            Int64 count = 1;
            checked
            {
                while (enumerator.MoveNext())
                {
                    sum += enumerator.Current;
                    ++count;
                }
            }

            return (Double) sum / count;
        }

        public static Double? AverageOrDefault(this IEnumerable<Int64> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            using IEnumerator<Int64> enumerator = source.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                return null;
            }

            Int64 sum = enumerator.Current;
            Int64 count = 1;
            checked
            {
                while (enumerator.MoveNext())
                {
                    sum += enumerator.Current;
                    ++count;
                }
            }

            return (Double) sum / count;
        }

        public static Single AverageOr(this IEnumerable<Single> source, Single seed)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            using IEnumerator<Single> enumerator = source.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                return seed;
            }

            Double sum = enumerator.Current;
            Int64 count = 1;
            while (enumerator.MoveNext())
            {
                sum += enumerator.Current;
                ++count;
            }

            return (Single) (sum / count);
        }

        public static Single? AverageOrDefault(this IEnumerable<Single> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            using IEnumerator<Single> enumerator = source.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                return null;
            }

            Double sum = enumerator.Current;
            Int64 count = 1;
            while (enumerator.MoveNext())
            {
                sum += enumerator.Current;
                ++count;
            }

            return (Single) (sum / count);
        }

        public static Double AverageOr(this IEnumerable<Double> source, Double seed)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            using IEnumerator<Double> enumerator = source.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                return seed;
            }

            Double sum = enumerator.Current;
            Int64 count = 1;
            while (enumerator.MoveNext())
            {
                // There is an opportunity to short-circuit here, in that if e.Current is
                // ever NaN then the result will always be NaN. Assuming that this case is
                // rare enough that not checking is the better approach generally.
                sum += enumerator.Current;
                ++count;
            }

            return sum / count;
        }

        public static Double? AverageOrDefault(this IEnumerable<Double> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            using IEnumerator<Double> enumerator = source.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                return null;
            }

            Double sum = enumerator.Current;
            Int64 count = 1;
            while (enumerator.MoveNext())
            {
                // There is an opportunity to short-circuit here, in that if e.Current is
                // ever NaN then the result will always be NaN. Assuming that this case is
                // rare enough that not checking is the better approach generally.
                sum += enumerator.Current;
                ++count;
            }

            return sum / count;
        }

        public static Decimal AverageOr(this IEnumerable<Decimal> source, Decimal seed)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            using IEnumerator<Decimal> enumerator = source.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                return seed;
            }

            Decimal sum = enumerator.Current;
            Int64 count = 1;
            while (enumerator.MoveNext())
            {
                sum += enumerator.Current;
                ++count;
            }

            return sum / count;
        }

        public static Decimal? AverageOrDefault(this IEnumerable<Decimal> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            using IEnumerator<Decimal> enumerator = source.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                return null;
            }

            Decimal sum = enumerator.Current;
            Int64 count = 1;
            while (enumerator.MoveNext())
            {
                sum += enumerator.Current;
                ++count;
            }

            return sum / count;
        }

        public static Double AverageOr<TSource>(this IEnumerable<TSource> source, Func<TSource, Int32> selector, Double seed)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            using IEnumerator<TSource> enumerator = source.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                return seed;
            }

            Int64 sum = selector(enumerator.Current);
            Int64 count = 1;
            checked
            {
                while (enumerator.MoveNext())
                {
                    sum += selector(enumerator.Current);
                    ++count;
                }
            }

            return (Double) sum / count;
        }

        public static Double? AverageOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, Int32> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            using IEnumerator<TSource> enumerator = source.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                return null;
            }

            Int64 sum = selector(enumerator.Current);
            Int64 count = 1;
            checked
            {
                while (enumerator.MoveNext())
                {
                    sum += selector(enumerator.Current);
                    ++count;
                }
            }

            return (Double) sum / count;
        }

        public static Double AverageOr<TSource>(this IEnumerable<TSource> source, Func<TSource, Int64> selector, Double seed)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            using IEnumerator<TSource> enumerator = source.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                return seed;
            }

            Int64 sum = selector(enumerator.Current);
            Int64 count = 1;
            checked
            {
                while (enumerator.MoveNext())
                {
                    sum += selector(enumerator.Current);
                    ++count;
                }
            }

            return (Double) sum / count;
        }

        public static Double? AverageOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, Int64> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            using IEnumerator<TSource> enumerator = source.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                return null;
            }

            Int64 sum = selector(enumerator.Current);
            Int64 count = 1;
            checked
            {
                while (enumerator.MoveNext())
                {
                    sum += selector(enumerator.Current);
                    ++count;
                }
            }

            return (Double) sum / count;
        }

        public static Single AverageOr<TSource>(this IEnumerable<TSource> source, Func<TSource, Single> selector, Single seed)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            using IEnumerator<TSource> enumerator = source.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                return seed;
            }

            Double sum = selector(enumerator.Current);
            Int64 count = 1;
            while (enumerator.MoveNext())
            {
                sum += selector(enumerator.Current);
                ++count;
            }

            return (Single) (sum / count);
        }

        public static Single? AverageOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, Single> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            using IEnumerator<TSource> enumerator = source.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                return null;
            }

            Double sum = selector(enumerator.Current);
            Int64 count = 1;
            while (enumerator.MoveNext())
            {
                sum += selector(enumerator.Current);
                ++count;
            }

            return (Single) (sum / count);
        }

        public static Double AverageOr<TSource>(this IEnumerable<TSource> source, Func<TSource, Double> selector, Double seed)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            using IEnumerator<TSource> enumerator = source.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                return seed;
            }

            Double sum = selector(enumerator.Current);
            Int64 count = 1;
            while (enumerator.MoveNext())
            {
                // There is an opportunity to short-circuit here, in that if e.Current is
                // ever NaN then the result will always be NaN. Assuming that this case is
                // rare enough that not checking is the better approach generally.
                sum += selector(enumerator.Current);
                ++count;
            }

            return sum / count;
        }

        public static Double? AverageOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, Double> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            using IEnumerator<TSource> enumerator = source.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                return null;
            }

            Double sum = selector(enumerator.Current);
            Int64 count = 1;
            while (enumerator.MoveNext())
            {
                // There is an opportunity to short-circuit here, in that if e.Current is
                // ever NaN then the result will always be NaN. Assuming that this case is
                // rare enough that not checking is the better approach generally.
                sum += selector(enumerator.Current);
                ++count;
            }

            return sum / count;
        }

        public static Decimal AverageOr<TSource>(this IEnumerable<TSource> source, Func<TSource, Decimal> selector, Decimal seed)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            using IEnumerator<TSource> enumerator = source.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                return seed;
            }

            Decimal sum = selector(enumerator.Current);
            Int64 count = 1;
            while (enumerator.MoveNext())
            {
                sum += selector(enumerator.Current);
                ++count;
            }

            return sum / count;
        }

        public static Decimal? AverageOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, Decimal> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            using IEnumerator<TSource> enumerator = source.GetEnumerator();
            if (!enumerator.MoveNext())
            {
                return null;
            }

            Decimal sum = selector(enumerator.Current);
            Int64 count = 1;
            while (enumerator.MoveNext())
            {
                sum += selector(enumerator.Current);
                ++count;
            }

            return sum / count;
        }

        /// <summary>
        /// Creates a sequence from start value and next element factory till factory returns null.
        /// </summary>
        /// <typeparam name="T">The type of element.</typeparam>
        /// <param name="value">Start value.</param>
        /// <param name="next">Next element factory.</param>
        /// <returns>Generated sequence.</returns>
        public static IEnumerable<T> CreateWhileNotNull<T>(T? value, Func<T, T> next) where T : class
        {
            if (next is null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            while (value is not null)
            {
                yield return value;
                value = next(value);
            }
        }

        /// <summary>
        /// Creates a sequence from start value and next element factory till factory returns null.
        /// </summary>
        /// <typeparam name="T">The type of source element.</typeparam>
        /// <typeparam name="TResult">The type of result element</typeparam>
        /// <param name="value">Start value.</param>
        /// <param name="next">Next element factory.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>Generated sequence.</returns>
        public static IEnumerable<TResult> CreateWhileNotNull<T, TResult>(T? value, Func<T, T> next, Func<T, TResult> selector) where T : class
        {
            if (next is null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            while (value is not null)
            {
                yield return selector(value);
                value = next(value);
            }
        }

        /// <summary>
        /// Determines whether all elements in this collection are equal to each other. Compares using the <see cref="object.Equals(object)"/> method.
        /// <para>This method returns true if the collection is empty or it contains only one element.</para>
        /// </summary>
        /// <typeparam name="T">The type of the elements.</typeparam>
        /// <param name="source">The collection.</param>
        public static Boolean AllSame<T>(this IEnumerable<T> source)
        {
            return AllSame(source, out _);
        }

        /// <summary>
        /// Determines whether all elements in this collection are equal to each other. Compares using the <see cref="object.Equals(object)"/> method.
        /// <para>This method returns true if the collection is empty or it contains only one element.</para>
        /// </summary>
        /// <typeparam name="T">The type of the elements.</typeparam>
        /// <param name="source">The collection.</param>
        /// <param name="value">First item</param>
        public static Boolean AllSame<T>(this IEnumerable<T> source, [MaybeNullWhen(true)] out T value)
        {
            return AllSame(source, EqualityComparer<T>.Default, out value);
        }

        /// <summary>
        /// Determines whether all elements in this collection are equal to each other. Compares using the <see cref="object.Equals(object)"/> method.
        /// <para>This method returns true if the collection is empty or it contains only one element.</para>
        /// </summary>
        /// <typeparam name="T">The type of the elements.</typeparam>
        /// <param name="source">The collection.</param>
        /// <param name="comparer">The comparer</param>
        public static Boolean AllSame<T>(this IEnumerable<T> source, IEqualityComparer<T>? comparer)
        {
            return AllSame(source, comparer, out _);
        }

        /// <summary>
        /// Determines whether all elements in this collection are equal to each other. Compares using the <see cref="object.Equals(object)"/> method.
        /// <para>This method returns true if the collection is empty or it contains only one element.</para>
        /// </summary>
        /// <typeparam name="T">The type of the elements.</typeparam>
        /// <param name="source">The collection.</param>
        /// <param name="comparer">The comparer</param>
        /// <param name="value">First item</param>
        public static Boolean AllSame<T>(this IEnumerable<T> source, IEqualityComparer<T>? comparer, [MaybeNullWhen(true)] out T value)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            comparer ??= EqualityComparer<T>.Default;

            using IEnumerator<T> enumerator = source.GetEnumerator();

            if (!enumerator.MoveNext())
            {
                value = default;
                return true;
            }

            value = enumerator.Current;

            while (enumerator.MoveNext())
            {
                if (!comparer.Equals(value, enumerator.Current))
                {
                    return false;
                }
            }

            return true;
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
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

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

            if (comparer is null)
            {
                throw new ArgumentNullException(nameof(comparer));
            }

            return source.Select(selector).AllSame(comparer);
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

        public static IImmutableDictionary<T, Int32> CountGroup<T>(this IEnumerable<T> source) where T : notnull
        {
            return CountGroup(source, null);
        }

        public static IImmutableDictionary<T, Int32> CountGroup<T>(this IEnumerable<T> source, IEqualityComparer<T>? comparer) where T : notnull
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

            return counter.ToImmutableDictionary();
        }
        
        public static IImmutableDictionary<TKey, Int32> CountGroupBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector) where TKey : notnull
        {
            return CountGroupBy(source, selector, null);
        }

        public static IImmutableDictionary<TKey, Int32> CountGroupBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector, IEqualityComparer<TKey>? comparer) where TKey : notnull
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
        
        public static IImmutableDictionary<T, Int64> LongCountGroup<T>(this IEnumerable<T> source) where T : notnull
        {
            return LongCountGroup(source, null);
        }

        public static IImmutableDictionary<T, Int64> LongCountGroup<T>(this IEnumerable<T> source, IEqualityComparer<T>? comparer) where T : notnull
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

            return counter.ToImmutableDictionary();
        }
        
        public static IImmutableDictionary<TKey, Int64> LongCountGroupBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector) where TKey : notnull
        {
            return LongCountGroupBy(source, selector, null);
        }

        public static IImmutableDictionary<TKey, Int64> LongCountGroupBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector, IEqualityComparer<TKey>? comparer) where TKey : notnull
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

        /// <summary>
        /// Gets collection count if <see cref="source"/> is materialized, otherwise null.
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

            return source switch
            {
                ICollection<T> collection => collection.Count,
                IReadOnlyCollection<T> collection => collection.Count,
                ICollection collection => collection.Count,
                _ => null
            };
        }
        
        public static Int32? CountIfMaterializedByReflection(this IEnumerable source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.TryGetPropertyValue("Length", out Int32 length) ? length :
                source.TryGetPropertyValue("Count", out length) ? length :
                source.TryGetPropertyValue("LongLength", out length) ? length :
                source.TryGetPropertyValue("LongCount", out length) ? length : default;
        }
        
        public static Int64? LongCountIfMaterializedByReflection(this IEnumerable source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.TryGetPropertyValue("LongLength", out Int64 length) ? length :
                source.TryGetPropertyValue("LongCount", out length) ? length :
                source.TryGetPropertyValue("Length", out length) ? length :
                source.TryGetPropertyValue("Count", out length) ? length : default;
        }

        public static Boolean IsMaterialized<T>(this IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return IsMaterialized(source, out _);
        }

        public static Boolean IsMaterialized<T>(this IEnumerable<T> source, out Int32? count)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            count = CountIfMaterialized(source);
            return count is not null;
        }

        public static IEnumerable<T> Materialize<T>(this IEnumerable<T>? source)
        {
            return source switch
            {
                null => Enumerable.Empty<T>(),
                ICollection<T> => source,
                IReadOnlyCollection<T> => source,
                _ => source.ToArray()
            };
        }

        public static ICollection<T> AsCollection<T>(this IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return AsCollection(source, out _);
        }
        
        public static ICollection<T> AsCollection<T>(this IEnumerable<T> source, out Int32 count)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            ICollection<T> collection = source as ICollection<T> ?? source.ToArray();
            count = collection.Count;
            
            return collection;
        }
        
        public static IReadOnlyCollection<T> AsReadOnlyCollection<T>(this IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return AsReadOnlyCollection(source, out _);
        }
        
        public static IReadOnlyCollection<T> AsReadOnlyCollection<T>(this IEnumerable<T> source, out Int32 count)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            IReadOnlyCollection<T> result = source switch
            {
                IReadOnlyCollection<T> collection => collection,
                ICollection<T> collection => new CollectionWrapper<T>(collection),
                ICollection collection => new NonGenericCollectionWrapper<T>(collection),
                _ => source.ToArray()
            };

            count = result.Count;
            return result;
        }

        /// <summary>
        /// Gets collection if <see cref="source"/> is materialized, otherwise ToArray();ed collection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="nullToEmpty"></param>
        public static IEnumerable<T> Materialize<T>(this IEnumerable<T> source, Boolean nullToEmpty)
        {
            return source switch
            {
                null when nullToEmpty => Enumerable.Empty<T>(),
                null => throw new ArgumentNullException(nameof(source)),
                ICollection<T> => source,
                IReadOnlyCollection<T> => source,
                _ => source.ToArray()
            };
        }
        
        public static IEnumerable<T> Dematerialize<T>(this IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            using IEnumerator<T> enumerator = source.GetEnumerator();
            return enumerator.AsEnumerable();
        }

        public static Boolean TryReset(this IEnumerator enumerator)
        {
            if (enumerator is null)
            {
                throw new ArgumentNullException(nameof(enumerator));
            }

            try
            {
                enumerator.Reset();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        
        public static Boolean TryReset<T>(this IEnumerator<T> enumerator)
        {
            if (enumerator is null)
            {
                throw new ArgumentNullException(nameof(enumerator));
            }

            try
            {
                enumerator.Reset();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static IEnumerable<Object?> AsEnumerable(this IEnumerator enumerator)
        {
            if (enumerator is null)
            {
                throw new ArgumentNullException(nameof(enumerator));
            }

            while (enumerator.MoveNext())
            {
                yield return enumerator.Current;
            }
        }
        
        public static IEnumerable<T> AsEnumerable<T>(this IEnumerator<T> enumerator)
        {
            if (enumerator is null)
            {
                throw new ArgumentNullException(nameof(enumerator));
            }

            while (enumerator.MoveNext())
            {
                yield return enumerator.Current;
            }
        }

        public static IEnumerator<T> GetThreadSafeEnumerator<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable is null)
            {
                throw new ArgumentNullException(nameof(enumerable));
            }

            return new ThreadSafeEnumerator<T>(enumerable);
        }
        
        public static IEnumerator<T> GetThreadSafeEnumerator<T>(this IEnumerable<T> enumerable, Object sync)
        {
            if (enumerable is null)
            {
                throw new ArgumentNullException(nameof(enumerable));
            }

            if (sync is null)
            {
                throw new ArgumentNullException(nameof(sync));
            }

            return new ThreadSafeEnumerator<T>(enumerable, sync);
        }
        
        public static IEnumerator<T> GetThreadSafeEnumerator<T>(this IEnumerator<T> enumerator, Object sync)
        {
            if (enumerator is null)
            {
                throw new ArgumentNullException(nameof(enumerator));
            }

            if (sync is null)
            {
                throw new ArgumentNullException(nameof(sync));
            }

            return new ThreadSafeEnumerator<T>(enumerator, sync);
        }

        public static Boolean Is<T>(CollectionType type) where T : IEnumerable
        {
            return Is(typeof(T), type);
        }
        
        public static Boolean Is<T>(CollectionType type, Boolean strict) where T : IEnumerable
        {
            return Is(typeof(T), type, strict);
        }

        public static Boolean Is(this IEnumerable enumerable, CollectionType type)
        {
            return Is(enumerable?.GetType(), type);
        }
        
        public static Boolean Is(this IEnumerable enumerable, CollectionType type, Boolean strict)
        {
            return Is(enumerable?.GetType(), type, strict);
        }

        public static Boolean Is(this Type? type, CollectionType ctype)
        {
            return type is not null && type.GetCollectionType().HasFlag(ctype);
        }
        
        public static Boolean Is(this Type? type, CollectionType ctype, Boolean strict)
        {
            if (type is null)
            {
                return false;
            }

            Boolean istype = ctype switch
            {
                CollectionType.None => true,
                CollectionType.Generic => type.IsGenericType,
                CollectionType.Enumerable => TypeCache.IsEnumerable(type),
                CollectionType.GenericEnumerable => TypeCache.IsGenericEnumerable(type),
                CollectionType.Collection => TypeCache.IsCollection(type),
                CollectionType.GenericCollection => TypeCache.IsGenericCollection(type),
                CollectionType.Array => TypeCache.IsArray(type),
                CollectionType.GenericArray => TypeCache.IsGenericArray(type),
                CollectionType.List => TypeCache.IsList(type),
                CollectionType.GenericList => TypeCache.IsGenericList(type),
                CollectionType.Set => TypeCache.IsSet(type),
                CollectionType.GenericSet => TypeCache.IsGenericSet(type),
                CollectionType.Dictionary => TypeCache.IsDictionary(type),
                CollectionType.GenericDictionary => TypeCache.IsGenericDictionary(type),
                CollectionType.Stack => TypeCache.IsStack(type),
                CollectionType.GenericStack => TypeCache.IsGenericStack(type),
                CollectionType.Queue => TypeCache.IsQueue(type),
                CollectionType.GenericQueue => TypeCache.IsGenericQueue(type),
                _ => throw new NotSupportedException()
            };
            
            if (strict || istype)
            {
                return istype;
            }

            return Is(type, ctype);
        }

        public static CollectionType GetCollectionType<T>() where T : IEnumerable
        {
            return GetCollectionType(typeof(T));
        }

        public static CollectionType GetCollectionType(this IEnumerable enumerable)
        {
            return GetCollectionType(enumerable?.GetType());
        }

        public static CollectionType GetCollectionType(this Type? type)
        {
            if (type is null)
            {
                return CollectionType.None;
            }

            CollectionType collection = CollectionType.None;

            if (TypeCache.IsGenericArray(type))
            {
                collection |= CollectionType.GenericArray;
            }
            else if (TypeCache.IsArray(type))
            {
                collection |= CollectionType.Array;
            }
            
            if (TypeCache.IsGenericList(type))
            {
                collection |= CollectionType.GenericList;
            }
            else if (TypeCache.IsList(type))
            {
                collection |= CollectionType.List;
            }
            
            if (TypeCache.IsGenericSet(type))
            {
                collection |= CollectionType.GenericSet;
            }
            else if (TypeCache.IsSet(type))
            {
                collection |= CollectionType.Set;
            }
            
            if (TypeCache.IsGenericDictionary(type))
            {
                collection |= CollectionType.GenericDictionary;
            }
            else if (TypeCache.IsDictionary(type))
            {
                collection |= CollectionType.Dictionary;
            }
            
            if (TypeCache.IsGenericStack(type))
            {
                collection |= CollectionType.GenericStack;
            }
            else if (TypeCache.IsStack(type))
            {
                collection |= CollectionType.Stack;
            }
            
            if (TypeCache.IsGenericQueue(type))
            {
                collection |= CollectionType.GenericQueue;
            }
            else if (TypeCache.IsQueue(type))
            {
                collection |= CollectionType.Queue;
            }

            if (collection.HasFlag(CollectionType.Collection))
            {
                return collection;
            }

            if (TypeCache.IsGenericCollection(type))
            {
                collection |= CollectionType.GenericCollection;
            }
            else if (TypeCache.IsCollection(type))
            {
                collection |= CollectionType.Collection;
            }

            if (collection.HasFlag(CollectionType.Enumerable))
            {
                return collection;
            }
            
            if (TypeCache.IsGenericEnumerable(type))
            {
                collection |= CollectionType.GenericEnumerable;
            }
            else if (TypeCache.IsEnumerable(type))
            {
                collection |= CollectionType.Enumerable;
            }

            return collection;
        }

        internal static class TypeCache
        {
            private readonly struct Status
            {
                public Boolean IsType { get; init; }
                public Boolean IsNonGenericType { get; init; }
                public Boolean IsGenericType { get; init; }

                public Status(Boolean type, Boolean nongeneric, Boolean generic)
                {
                    IsType = type;
                    IsNonGenericType = nongeneric;
                    IsGenericType = generic;
                }
            }
            
            private static ConcurrentDictionary<Type, Status> EnumerableCache { get; } = new ConcurrentDictionary<Type, Status>();
            private static ConcurrentDictionary<Type, Status> CollectionCache { get; } = new ConcurrentDictionary<Type, Status>();
            private static ConcurrentDictionary<Type, Status> ListCache { get; } = new ConcurrentDictionary<Type, Status>();
            private static ConcurrentDictionary<Type, Status> SetCache { get; } = new ConcurrentDictionary<Type, Status>();
            private static ConcurrentDictionary<Type, Status> DictionaryCache { get; } = new ConcurrentDictionary<Type, Status>();
            private static ConcurrentDictionary<Type, Status> StackCache { get; } = new ConcurrentDictionary<Type, Status>();
            private static ConcurrentDictionary<Type, Status> QueueCache { get; } = new ConcurrentDictionary<Type, Status>();
            
            public static IImmutableSet<Type> EnumerableTypes { get; } = new HashSet<Type>{typeof(IEnumerable)}.ToImmutableHashSet();
            public static IImmutableSet<Type> GenericEnumerableTypes { get; } = new HashSet<Type>{typeof(IEnumerable<>)}.ToImmutableHashSet();
            public static IImmutableSet<Type> CollectionTypes { get; } = new HashSet<Type>{typeof(ICollection)}.ToImmutableHashSet();
            public static IImmutableSet<Type> GenericCollectionTypes { get; } = new HashSet<Type>{typeof(ICollection<>), typeof(IReadOnlyCollection<>)}.ToImmutableHashSet();
            public static IImmutableSet<Type> ListTypes { get; } = new HashSet<Type>{typeof(IList)}.ToImmutableHashSet();
            public static IImmutableSet<Type> GenericListTypes { get; } = new HashSet<Type>{typeof(IList<>), typeof(IReadOnlyList<>), typeof(IImmutableList<>)}.ToImmutableHashSet();
            public static IImmutableSet<Type> SetTypes { get; } = new HashSet<Type>{typeof(ISet)}.ToImmutableHashSet();
            public static IImmutableSet<Type> GenericSetTypes { get; } = new HashSet<Type>{typeof(ISet<>), typeof(IReadOnlySet<>), typeof(IImmutableSet<>)}.ToImmutableHashSet();
            public static IImmutableSet<Type> DictionaryTypes { get; } = new HashSet<Type>{typeof(IDictionary)}.ToImmutableHashSet();
            public static IImmutableSet<Type> GenericDictionaryTypes { get; } = new HashSet<Type>{typeof(IDictionary<,>), typeof(IReadOnlyDictionary<,>), typeof(IImmutableDictionary<,>)}.ToImmutableHashSet();
            public static IImmutableSet<Type> StackTypes { get; } = new HashSet<Type>{typeof(Stack)}.ToImmutableHashSet();
            public static IImmutableSet<Type> GenericStackTypes { get; } = new HashSet<Type>{typeof(Stack<>), typeof(ConcurrentStack<>), typeof(IImmutableStack<>)}.ToImmutableHashSet();
            public static IImmutableSet<Type> QueueTypes { get; } = new HashSet<Type>{typeof(Queue)}.ToImmutableHashSet();
            public static IImmutableSet<Type> GenericQueueTypes { get; } = new HashSet<Type>{typeof(Queue<>), typeof(ConcurrentQueue<>), typeof(IImmutableQueue<>)}.ToImmutableHashSet();

            private static Status Create(Type type, IImmutableSet<Type> types, IImmutableSet<Type> generics)
            {
                if (type is null)
                {
                    throw new ArgumentNullException(nameof(type));
                }

                type = type.TryGetGenericTypeDefinition();

                Type[] interfaces = type.GetGenericTypeDefinitionInterfaces();
                Boolean isnongeneric = types.Contains(type) || types.Overlaps(interfaces);
                Boolean isgeneric = generics.Contains(type) || generics.Overlaps(interfaces);

                Boolean istype = isnongeneric || isgeneric;

                return new Status(istype, isnongeneric, isgeneric);
            }
            
            private static Status CreateEnumerable(Type type)
            {
                return Create(type, EnumerableTypes, GenericEnumerableTypes);
            }

            private static Status CreateCollection(Type type)
            {
                return Create(type, CollectionTypes, GenericCollectionTypes);
            }
            
            private static Status CreateList(Type type)
            {
                return Create(type, ListTypes, GenericListTypes);
            }
            
            private static Status CreateSet(Type type)
            {
                return Create(type, SetTypes, GenericSetTypes);
            }

            private static Status CreateDictionary(Type type)
            {
                return Create(type, DictionaryTypes, GenericDictionaryTypes);
            }
            
            private static Status CreateStack(Type type)
            {
                return Create(type, StackTypes, GenericStackTypes);
            }
            
            private static Status CreateQueue(Type type)
            {
                return Create(type, QueueTypes, GenericQueueTypes);
            }

            private static Boolean Is(Type? type, ConcurrentDictionary<Type, Status> cache, Func<Type, Status> factory)
            {
                if (type is null)
                {
                    return false;
                }
                
                if (type.IsAbstract && type.IsSealed)
                {
                    return false;
                }

                return cache.GetOrAdd(type.TryGetGenericTypeDefinition(), factory).IsType;
            }
            
            private static Boolean IsNonGeneric(Type? type, ConcurrentDictionary<Type, Status> cache, Func<Type, Status> factory)
            {
                if (type is null)
                {
                    return false;
                }
                
                if (type.IsGenericType || type.IsAbstract && type.IsSealed)
                {
                    return false;
                }

                return cache.GetOrAdd(type, factory).IsNonGenericType;
            }
            
            private static Boolean IsGeneric(Type? type, ConcurrentDictionary<Type, Status> cache, Func<Type, Status> factory)
            {
                if (type is null)
                {
                    return false;
                }
                
                if (!type.IsGenericType || type.IsAbstract && type.IsSealed)
                {
                    return false;
                }

                return cache.GetOrAdd(type.GetGenericTypeDefinition(), factory).IsGenericType;
            }

            public static Boolean IsArray(Type? type)
            {
                return type is not null && type.IsArray;
            }
            
            public static Boolean IsNonGenericArray(Type? type)
            {
                return type is not null && type.IsArray && !type.IsGenericType;
            }
            
            public static Boolean IsGenericArray(Type? type)
            {
                return type is not null && type.IsArray && type.IsGenericType;
            }
            
            public static Boolean IsEnumerable(Type? type)
            {
                return type is not null && Is(type, EnumerableCache, CreateEnumerable);
            }
            
            public static Boolean IsNonGenericEnumerable(Type? type)
            {
                return type is not null && IsNonGeneric(type, EnumerableCache, CreateEnumerable);
            }
            
            public static Boolean IsGenericEnumerable(Type? type)
            {
                return type is not null && IsGeneric(type, EnumerableCache, CreateEnumerable);
            }
            
            public static Boolean IsCollection(Type? type)
            {
                return type is not null && Is(type, CollectionCache, CreateCollection);
            }
            
            public static Boolean IsNonGenericCollection(Type? type)
            {
                return type is not null && IsNonGeneric(type, CollectionCache, CreateCollection);
            }
            
            public static Boolean IsGenericCollection(Type? type)
            {
                return type is not null && IsGeneric(type, CollectionCache, CreateCollection);
            }
            
            public static Boolean IsList(Type? type)
            {
                return type is not null && Is(type, ListCache, CreateList);
            }
            
            public static Boolean IsNonGenericList(Type? type)
            {
                return type is not null && IsNonGeneric(type, ListCache, CreateList);
            }
            
            public static Boolean IsGenericList(Type? type)
            {
                return type is not null && IsGeneric(type, ListCache, CreateList);
            }

            public static Boolean IsSet(Type? type)
            {
                return type is not null && Is(type, SetCache, CreateSet);
            }
            
            public static Boolean IsNonGenericSet(Type? type)
            {
                return type is not null && IsNonGeneric(type, SetCache, CreateSet);
            }
            
            public static Boolean IsGenericSet(Type? type)
            {
                return type is not null && IsGeneric(type, SetCache, CreateSet);
            }
            
            public static Boolean IsDictionary(Type? type)
            {
                return type is not null && Is(type, DictionaryCache, CreateDictionary);
            }
            
            public static Boolean IsNonGenericDictionary(Type? type)
            {
                return type is not null && IsNonGeneric(type, DictionaryCache, CreateDictionary);
            }
            
            public static Boolean IsGenericDictionary(Type? type)
            {
                return type is not null && IsGeneric(type, DictionaryCache, CreateDictionary);
            }
            
            public static Boolean IsStack(Type? type)
            {
                return type is not null && Is(type, StackCache, CreateStack);
            }
            
            public static Boolean IsNonGenericStack(Type? type)
            {
                return type is not null && IsNonGeneric(type, StackCache, CreateStack);
            }
            
            public static Boolean IsGenericStack(Type? type)
            {
                return type is not null && IsGeneric(type, StackCache, CreateStack);
            }
            
            public static Boolean IsQueue(Type? type)
            {
                return type is not null && Is(type, QueueCache, CreateQueue);
            }
            
            public static Boolean IsNonGenericQueue(Type? type)
            {
                return type is not null && IsNonGeneric(type, QueueCache, CreateQueue);
            }
            
            public static Boolean IsGenericQueue(Type? type)
            {
                return type is not null && IsGeneric(type, QueueCache, CreateQueue);
            }
        }
    }
}