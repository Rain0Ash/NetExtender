// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using NetExtender.Utilities.Numerics;

namespace NetExtender.Utilities.Types
{
    public static partial class EnumerableUtilities
    {
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

            Int32 index = 0;
            foreach (T item in source)
            {
                if (predicate(item))
                {
                    return index;
                }
                
                ++index;
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

            Int64 index = 0;
            foreach (T item in source)
            {
                if (predicate(item))
                {
                    return index;
                }

                ++index;
            }

            return -1;
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

            using IEnumerator<T> enumerator = source.GetEnumerator();

            while (enumerator.MoveNext())
            {
                if (index-- == 0)
                {
                    return enumerator.Current;
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
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

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

        public static T GetRandom<T>(this IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source switch
            {
                IList<T> list => list.GetRandom(),
                IReadOnlyList<T> list => list.Count > 0 ? list[RandomUtilities.NextNonNegative(list.Count - 1)] : throw new InvalidOperationException(),
                ICollection<T> collection => collection.Count > 0 ? collection.ElementAt(RandomUtilities.NextNonNegative(collection.Count - 1)) : throw new InvalidOperationException(),
                IReadOnlyCollection<T> collection => collection.Count > 0 ? collection.ElementAt(RandomUtilities.NextNonNegative(collection.Count - 1)) : throw new InvalidOperationException(),
                _ => source.ToList().GetRandom()
            };
        }
        
        public static T GetRandomOr<T>(this IEnumerable<T> source, T alternate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source switch
            {
                IList<T> list => list.GetRandomOr(alternate),
                IReadOnlyList<T> list => list.Count > 0 ? list[RandomUtilities.NextNonNegative(list.Count - 1)] : alternate,
                ICollection<T> collection => collection.Count > 0 ? collection.ElementAt(RandomUtilities.NextNonNegative(collection.Count - 1)) : alternate,
                IReadOnlyCollection<T> collection => collection.Count > 0 ? collection.ElementAt(RandomUtilities.NextNonNegative(collection.Count - 1)) : alternate,
                _ => source.ToList().GetRandomOr(alternate)
            };
        }
        
        public static T GetRandomOr<T>(this IEnumerable<T> source, Func<T> alternate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            return source switch
            {
                IList<T> list => list.GetRandomOr(alternate),
                IReadOnlyList<T> list => list.Count > 0 ? list[RandomUtilities.NextNonNegative(list.Count - 1)] : alternate(),
                ICollection<T> collection => collection.Count > 0 ? collection.ElementAt(RandomUtilities.NextNonNegative(collection.Count - 1)) : alternate(),
                IReadOnlyCollection<T> collection => collection.Count > 0 ? collection.ElementAt(RandomUtilities.NextNonNegative(collection.Count - 1)) : alternate(),
                _ => source.ToList().GetRandomOr(alternate)
            };
        }

        public static T? GetRandomOrDefault<T>(this IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source switch
            {
                IList<T> list => list.GetRandomOrDefault(),
                IReadOnlyList<T> list => list.Count > 0 ? list[RandomUtilities.NextNonNegative(list.Count - 1)] : default,
                ICollection<T> collection => collection.Count > 0 ? collection.ElementAtOrDefault(RandomUtilities.NextNonNegative(collection.Count - 1)) : default,
                IReadOnlyCollection<T> collection => collection.Count > 0 ? collection.ElementAtOrDefault(RandomUtilities.NextNonNegative(collection.Count - 1)) : default,
                _ => source.ToList().GetRandomOrDefault()
            };
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
        
        public static IEnumerable<T> Max<T>(this IEnumerable<T> source, IComparer<T>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.OrderByDescending(comparer);
        }
        
        public static IEnumerable<T> Max<T>(this IEnumerable<T> source, Int32 count)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.OrderByDescending().Take(count);
        }
        public static IEnumerable<T> Max<T>(this IEnumerable<T> source, IComparer<T>? comparer, Int32 count)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.OrderByDescending(comparer).Take(count);
        }

        public static T MaxOr<T>(this IEnumerable<T> source, T alternate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            return source.OrderByDescending().FirstOr(alternate);
        }

        public static T MaxOr<T>(this IEnumerable<T> source, T alternate, IComparer<T>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            return source.OrderByDescending(comparer).FirstOr(alternate);
        }
        
        public static T MaxOr<T>(this IEnumerable<T> source, Func<T> alternate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            return source.OrderByDescending().FirstOr(alternate);
        }
        
        public static T MaxOr<T>(this IEnumerable<T> source, Func<T> alternate, IComparer<T>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            return source.OrderByDescending(comparer).FirstOr(alternate);
        }
        
        public static T? MaxOrDefault<T>(this IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.OrderByDescending().FirstOrDefault();
        }
        
        public static T? MaxOrDefault<T>(this IEnumerable<T> source, IComparer<T>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.OrderByDescending(comparer).FirstOrDefault();
        }
        
        public static T MaxBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.OrderByDescending(selector).First();
        }

        public static T MaxBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector, IComparer<TKey>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.OrderByDescending(selector, comparer).First();
        }
        
        public static T MaxByOr<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector, T alternate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.OrderByDescending(selector).FirstOr(alternate);
        }

        public static T MaxByOr<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector, T alternate, IComparer<TKey>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.OrderByDescending(selector, comparer).FirstOr(alternate);
        }
        
        public static T MaxByOr<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector, Func<T> alternate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            return source.OrderByDescending(selector).FirstOr(alternate);
        }

        public static T MaxByOr<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector, Func<T> alternate, IComparer<TKey>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            return source.OrderByDescending(selector, comparer).FirstOr(alternate);
        }
        
        public static T? MaxByOrDefault<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector)
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

        public static T? MaxByOrDefault<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector, IComparer<TKey>? comparer)
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

        public static IEnumerable<T> MaxBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector, IComparer<TKey>? comparer, Int32 count)
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
        
        public static IEnumerable<T> Min<T>(this IEnumerable<T> source, IComparer<T>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.OrderBy(comparer);
        }
        
        public static IEnumerable<T> Min<T>(this IEnumerable<T> source, Int32 count)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.OrderBy().Take(count);
        }
        
        public static IEnumerable<T> Min<T>(this IEnumerable<T> source, IComparer<T>? comparer, Int32 count)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.OrderBy(comparer).Take(count);
        }
        
        public static T MinOr<T>(this IEnumerable<T> source, T alternate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.OrderBy().FirstOr(alternate);
        }
        
        public static T MinOr<T>(this IEnumerable<T> source, T alternate, IComparer<T>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.OrderBy(comparer).FirstOr(alternate);
        }

        public static T MinOr<T>(this IEnumerable<T> source, Func<T> alternate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            return source.OrderBy().FirstOr(alternate);
        }

        public static T MinOr<T>(this IEnumerable<T> source, Func<T> alternate, IComparer<T>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            return source.OrderBy(comparer).FirstOr(alternate);
        }
        
        public static T? MinOrDefault<T>(this IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.OrderBy().FirstOrDefault();
        }
        
        public static T? MinOrDefault<T>(this IEnumerable<T> source, IComparer<T>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.OrderBy(comparer).FirstOrDefault();
        }

        public static T MinBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.OrderBy(selector).First();
        }

        public static T MinBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector, IComparer<TKey>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.OrderBy(selector, comparer).First();
        }

        public static T MinByOr<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector, T alternate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.OrderBy(selector).FirstOr(alternate);
        }

        public static T MinByOr<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector, T alternate, IComparer<TKey>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.OrderBy(selector, comparer).FirstOr(alternate);
        }

        public static T MinByOr<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector, Func<T> alternate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            return source.OrderBy(selector).FirstOr(alternate);
        }

        public static T MinByOr<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector, Func<T> alternate, IComparer<TKey>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            return source.OrderBy(selector, comparer).FirstOr(alternate);
        }

        public static T? MinByOrDefault<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector)
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

        public static T? MinByOrDefault<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector, IComparer<TKey>? comparer)
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

        public static IEnumerable<T> MinBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector, IComparer<TKey>? comparer, Int32 count)
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

        public static (T Min, T Max) MinMax<T>(this IEnumerable<T> source)
        {
            return MinMax(source, null);
        }

        public static (T Min, T Max) MinMax<T>(this IEnumerable<T> source, IComparer<T>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            using IEnumerator<T> enumerator = source.GetEnumerator();

            if (!enumerator.MoveNext())
            {
                throw new InvalidOperationException();
            }
            
            comparer ??= Comparer<T>.Default;

            T current = enumerator.Current;

            T min = current;
            T max = current;

            while (enumerator.MoveNext())
            {
                current = enumerator.Current;

                if (comparer.Compare(current, min) < 0)
                {
                    min = current;
                }

                if (comparer.Compare(current, max) > 0)
                {
                    max = current;
                }
            }

            return (min, max);
        }

        public static (T Min, T Max) MinMaxOr<T>(this IEnumerable<T> source, T alternate)
        {
            return MinMaxOr(source, alternate, null);
        }

        public static (T Min, T Max) MinMaxOr<T>(this IEnumerable<T> source, T alternate, IComparer<T>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            using IEnumerator<T> enumerator = source.GetEnumerator();

            if (!enumerator.MoveNext())
            {
                return (alternate, alternate);
            }
            
            comparer ??= Comparer<T>.Default;

            T current = enumerator.Current;

            T min = current;
            T max = current;

            while (enumerator.MoveNext())
            {
                current = enumerator.Current;

                if (comparer.Compare(current, min) < 0)
                {
                    min = current;
                }

                if (comparer.Compare(current, max) > 0)
                {
                    max = current;
                }
            }

            return (min, max);
        }

        public static (T Min, T Max) MinMaxOr<T>(this IEnumerable<T> source, Func<T> alternate)
        {
            return MinMaxOr(source, alternate, null);
        }

        public static (T Min, T Max) MinMaxOr<T>(this IEnumerable<T> source, Func<T> alternate, IComparer<T>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            using IEnumerator<T> enumerator = source.GetEnumerator();

            T current;
            
            if (!enumerator.MoveNext())
            {
                current = alternate();
                return (current, current);
            }
            
            comparer ??= Comparer<T>.Default;

            current = enumerator.Current;

            T min = current;
            T max = current;

            while (enumerator.MoveNext())
            {
                current = enumerator.Current;

                if (comparer.Compare(current, min) < 0)
                {
                    min = current;
                }

                if (comparer.Compare(current, max) > 0)
                {
                    max = current;
                }
            }

            return (min, max);
        }

        public static (T? Min, T? Max) MinMaxOrDefault<T>(this IEnumerable<T> source)
        {
            return MinMaxOrDefault(source, null);
        }

        public static (T? Min, T? Max) MinMaxOrDefault<T>(this IEnumerable<T> source, IComparer<T>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            using IEnumerator<T> enumerator = source.GetEnumerator();

            if (!enumerator.MoveNext())
            {
                return (default, default);
            }
            
            comparer ??= Comparer<T>.Default;

            T current = enumerator.Current;

            T min = current;
            T max = current;

            while (enumerator.MoveNext())
            {
                current = enumerator.Current;

                if (comparer.Compare(current, min) < 0)
                {
                    min = current;
                }

                if (comparer.Compare(current, max) > 0)
                {
                    max = current;
                }
            }

            return (min, max);
        }
        
        public static T? Median<T>(this IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            return Median(source, (x, _) => x, x => x);
        }

        public static T? Median<T>(this IEnumerable<T> source, Func<T, T> order)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (order is null)
            {
                throw new ArgumentNullException(nameof(order));
            }
            
            return Median(source, (x, _) => x, order);
        }
        
        public static T? Median<T>(this IEnumerable<T> source, Func<T, T, T> average)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (average is null)
            {
                throw new ArgumentNullException(nameof(average));
            }
            
            return Median(source, average, x => x);
        }

        /// <summary>
        /// Gets the median from the list
        /// </summary>
        /// <typeparam name="T">The data type of the list</typeparam>
        /// <param name="source">The list of values</param>
        /// <param name="average">
        /// Function used to find the average of two values if the number of values is even.
        /// </param>
        /// <param name="order">Function used to order the values</param>
        /// <returns>The median value</returns>
        public static T? Median<T>(this IEnumerable<T> source, Func<T, T, T> average, Func<T, T> order)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (average is null)
            {
                throw new ArgumentNullException(nameof(average));
            }

            if (order is null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            List<T> values = source.OrderBy(order).ToList();
            
            if (values.Count <= 0)
            {
                return default;
            }

            if (values.Count % 2 != 0)
            {
                return values[values.Count / 2];
            }

            T first = values[values.Count / 2];
            T second = values[values.Count / 2 - 1];

            return average(first, second);
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
        public static Boolean AllSame<T, TValue>(this IEnumerable<T> source, Func<T, TValue> selector, IEqualityComparer<TValue>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.Select(selector).AllSame(comparer);
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

        // ReSharper disable once CognitiveComplexity
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

        // ReSharper disable once CognitiveComplexity
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
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            
            return source is not null && source.Any(predicate);
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
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            
            if (source is null)
            {
                return false;
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
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            
            if (source is null)
            {
                return true;
            }

            return !source.Any(predicate);
        }
    }
}