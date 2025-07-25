// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using NetExtender.Types.Dictionaries;
using NetExtender.Types.Indexers.Interfaces;
using NetExtender.Utilities.Numerics;

namespace NetExtender.Utilities.Types
{
    public static partial class EnumerableUtilities
    {
        public static Boolean Any<T>(this IEnumerable<T> source, Func<T, Int32, Boolean> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            Int32 i = 0;
            return source.Any(item => predicate(item, i++));
        }

        public static Boolean All<T>(this IEnumerable<T> source, Func<T, Int32, Boolean> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            Int32 i = 0;
            return source.All(item => predicate(item, i++));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Contains<T>(this IEnumerable<T> source, params T[]? values)
        {
            return Contains(source, (IEnumerable<T>?) values);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Contains<T>(this IEnumerable<T> source, IEnumerable<T>? values)
        {
            return Contains(source, values, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Contains<T>(this IEnumerable<T> source, IEqualityComparer<T>? comparer, params T[]? values)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (values is null)
            {
                return false;
            }

            return values.Length > 0 && Contains(source, values, comparer);
        }

        public static Boolean Contains<T>(this IEnumerable<T> source, IEnumerable<T>? values, IEqualityComparer<T>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (values is null)
            {
                return false;
            }

            HashSet<T> set = new HashSet<T>(values, comparer);
            return source.Any(item => set.Contains(item));
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

        public static Int32 CountWhile<T>(this IEnumerable<T> source, Func<T, Int32, Boolean> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            Int32 i = 0;
            Int32 count = 0;
            foreach (T item in source)
            {
                if (!predicate(item, i++))
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

        public static Int64 LongCountWhile<T>(this IEnumerable<T> source, Func<T, Int64, Boolean> predicate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            Int64 i = 0;
            Int64 count = 0;
            foreach (T item in source)
            {
                if (!predicate(item, i++))
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

        public static Int32 ReverseCountWhile<T>(this IEnumerable<T> source, Func<T, Int32, Boolean> predicate)
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

        public static Int64 ReverseLongCountWhile<T>(this IEnumerable<T> source, Func<T, Int64, Boolean> predicate)
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

        public static Int32 IndexOf<T>(this IEnumerable<T> source, T value)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source switch
            {
                IList<T> list => list.IndexOf(value),
                IIndexer<T> list => list.IndexOf(value),
                _ => IndexOf(source, value, null)
            };
        }

        public static Int32 IndexOf<T>(this IEnumerable<T> source, T value, IEqualityComparer<T>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            comparer ??= EqualityComparer<T>.Default;

            Int32 index = 0;
            foreach (T item in source)
            {
                if (comparer.Equals(item, value))
                {
                    return index;
                }

                index++;
            }

            return -1;
        }

        public static Boolean IndexOf<T>(this IEnumerable<T> source, T value, out Int32 index)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            index = IndexOf(source, value);
            return index >= 0;
        }

        public static Boolean IndexOf<T>(this IEnumerable<T> source, T value, IEqualityComparer<T>? comparer, out Int32 index)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            index = IndexOf(source, value, comparer);
            return index >= 0;
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

        public static Boolean FindIndex<T>(this IEnumerable<T> source, Func<T, Boolean> predicate, out Int32 index)
        {
            index = FindIndex(source, predicate);
            return index >= 0;
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

        public static Boolean LongFindIndex<T>(this IEnumerable<T> source, Func<T, Boolean> predicate, out Int64 index)
        {
            index = LongFindIndex(source, predicate);
            return index >= 0;
        }

        public static T ElementAtOrDefault<T>(this IEnumerable<T> source, Int32 index, T alternate)
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 BinarySearch<T>(this IEnumerable<T> source, T value)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source switch
            {
                T[] array => Array.BinarySearch(array, value),
                IList<T> collection => ListUtilities.BinarySearch(collection, value),
                IReadOnlyList<T> collection => ListUtilities.BinarySearch(collection, value),
                _ => Array.BinarySearch(source.ToArray(), value)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 BinarySearch<T>(this IEnumerable<T> source, T value, IComparer<T>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source switch
            {
                T[] array => Array.BinarySearch(array, value, comparer),
                IList<T> collection => ListUtilities.BinarySearch(collection, value, comparer),
                IReadOnlyList<T> collection => ListUtilities.BinarySearch(collection, value, comparer),
                _ => Array.BinarySearch(source.ToArray(), value, comparer)
            };
        }

        public static T ElementAtOrDefault<T>(this IEnumerable<T> source, Int32 index, Func<T> alternate)
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
            
            (value, Boolean result) = source switch
            {
                IList<T> collection => index >= 0 && index < collection.Count ? (collection[index], true) : (alternate, false),
                IReadOnlyList<T> collection => index >= 0 && index < collection.Count ? (collection[index], true) : (alternate, false),
                ICollection<T> collection => index >= 0 && index < collection.Count ? (collection.ElementAt(index), true) : (alternate, false),
                IReadOnlyCollection<T> collection => index >= 0 && index < collection.Count ? (collection.ElementAt(index), true) : (alternate, false),
                _ => TryGetValueCore(source, index, out value!) ? (value, true) : (alternate, false)
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
            
            (value, Boolean result) = source switch
            {
                IList<T> collection => index >= 0 && index < collection.Count ? (collection[index], true) : (alternate(), false),
                IReadOnlyList<T> collection => index >= 0 && index < collection.Count ? (collection[index], true) : (alternate(), false),
                ICollection<T> collection => index >= 0 && index < collection.Count ? (collection.ElementAt(index), true) : (alternate(), false),
                IReadOnlyCollection<T> collection => index >= 0 && index < collection.Count ? (collection.ElementAt(index), true) : (alternate(), false),
                _ => TryGetValueCore(source, index, out value!) ? (value, true) : (alternate(), false)
            };

            return result;
        }

        private static Boolean TryGetValueCore<T>(IEnumerable<T> source, Int32 index, [MaybeNullWhen(false)] out T value)
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
                _ => Random(source, out T? result) ? result : throw new InvalidOperationException()
            };
        }

        public static T GetRandomOrDefault<T>(this IEnumerable<T> source, T alternate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source switch
            {
                IList<T> list => list.GetRandomOrDefault(alternate),
                IReadOnlyList<T> list => list.Count > 0 ? list[RandomUtilities.NextNonNegative(list.Count - 1)] : alternate,
                ICollection<T> collection => collection.Count > 0 ? collection.ElementAt(RandomUtilities.NextNonNegative(collection.Count - 1)) : alternate,
                IReadOnlyCollection<T> collection => collection.Count > 0 ? collection.ElementAt(RandomUtilities.NextNonNegative(collection.Count - 1)) : alternate,
                _ => Random(source, out T? result) ? result : alternate
            };
        }

        public static T GetRandomOrDefault<T>(this IEnumerable<T> source, Func<T> alternate)
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
                IList<T> list => list.GetRandomOrDefault(alternate),
                IReadOnlyList<T> list => list.Count > 0 ? list[RandomUtilities.NextNonNegative(list.Count - 1)] : alternate(),
                ICollection<T> collection => collection.Count > 0 ? collection.ElementAt(RandomUtilities.NextNonNegative(collection.Count - 1)) : alternate(),
                IReadOnlyCollection<T> collection => collection.Count > 0 ? collection.ElementAt(RandomUtilities.NextNonNegative(collection.Count - 1)) : alternate(),
                _ => Random(source, out T? result) ? result : alternate()
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
                _ => Random(source, out T? result) ? result : default
            };
        }
        
        private static Boolean Random<T>(IEnumerable<T> source, [MaybeNullWhen(false)] out T result)
        {
            return Random(source, null, out result);
        }
        
        private static Boolean Random<T>(IEnumerable<T> source, Random? random, [MaybeNullWhen(false)] out T result)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            random ??= new Random();
            
            Int64 count = 0;
            result = default!;
            foreach (T element in source)
            {
                if (random.NextInt64(count++) == 0)
                {
                    result = element;
                }
            }
            
            return count > 0;
        }

        public static T Aggregate<T>(this IEnumerable<T> source, T seed, Func<T, T, T> selector)
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

            T result = seed;
            while (enumerator.MoveNext())
            {
                result = selector(result, enumerator.Current);
            }

            return result;
        }

#if !NET6_0_OR_GREATER
        public static T SingleOrDefault<T>(this IEnumerable<T> source, T alternate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            using IEnumerator<T> enumerator = source.GetEnumerator();

            if (!enumerator.MoveNext())
            {
                return alternate;
            }

            T item = enumerator.Current;

            if (enumerator.MoveNext())
            {
                return alternate;
            }

            return item;
        }

        // ReSharper disable once CognitiveComplexity
        public static T SingleOrDefault<T>(this IEnumerable<T> source, Func<T, Boolean> predicate, T alternate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            using IEnumerator<T> enumerator = source.GetEnumerator();

            if (!enumerator.MoveNext())
            {
                return alternate;
            }

            do
            {
                T item = enumerator.Current;

                if (!predicate(item))
                {
                    continue;
                }

                while (enumerator.MoveNext())
                {
                    if (predicate(enumerator.Current))
                    {
                        return alternate;
                    }
                }

                return item;

            } while (enumerator.MoveNext());

            return alternate;
        }
#endif

        public static T SingleOrDefault<T>(this IEnumerable<T> source, Func<T> alternate)
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

            if (!enumerator.MoveNext())
            {
                return alternate.Invoke();
            }

            T item = enumerator.Current;

            return enumerator.MoveNext() ? alternate() : item;
        }

        // ReSharper disable once CognitiveComplexity
        public static T SingleOrDefault<T>(this IEnumerable<T> source, Func<T, Boolean> predicate, Func<T> alternate)
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

            using IEnumerator<T> enumerator = source.GetEnumerator();

            if (!enumerator.MoveNext())
            {
                return alternate.Invoke();
            }

            do
            {
                T item = enumerator.Current;

                if (!predicate(item))
                {
                    continue;
                }

                while (enumerator.MoveNext())
                {
                    if (predicate(enumerator.Current))
                    {
                        return alternate();
                    }
                }

                return item;

            } while (enumerator.MoveNext());

            return alternate.Invoke();
        }
        
        public static T? SingleOrNullable<T>(this IEnumerable<T> source) where T : struct
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            using IEnumerator<T> enumerator = source.GetEnumerator();
            
            if (!enumerator.MoveNext())
            {
                return null;
            }
            
            T item = enumerator.Current;
            
            if (enumerator.MoveNext())
            {
                return null;
            }
            
            return item;
        }
        
        // ReSharper disable once CognitiveComplexity
        public static T? SingleOrNullable<T>(this IEnumerable<T> source, Func<T, Boolean> predicate) where T : struct
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            
            using IEnumerator<T> enumerator = source.GetEnumerator();
            
            if (!enumerator.MoveNext())
            {
                return null;
            }
            
            do
            {
                T item = enumerator.Current;
                
                if (!predicate(item))
                {
                    continue;
                }
                
                while (enumerator.MoveNext())
                {
                    if (predicate(enumerator.Current))
                    {
                        return null;
                    }
                }
                
                return item;
                
            } while (enumerator.MoveNext());
            
            return null;
        }

        public static Boolean TryGetSingle<T>(this IEnumerable<T> source, [MaybeNullWhen(false)] out T result)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            using IEnumerator<T> enumerator = source.GetEnumerator();

            if (!enumerator.MoveNext())
            {
                result = default;
                return false;
            }

            T item = enumerator.Current;

            if (enumerator.MoveNext())
            {
                result = default;
                return false;
            }

            result = item;
            return true;
        }

        // ReSharper disable once CognitiveComplexity
        public static Boolean TryGetSingle<T>(this IEnumerable<T> source, Func<T, Boolean> predicate, [MaybeNullWhen(false)] out T result)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            using IEnumerator<T> enumerator = source.GetEnumerator();

            if (!enumerator.MoveNext())
            {
                result = default;
                return false;
            }

            do
            {
                T item = enumerator.Current;

                if (!predicate(item))
                {
                    continue;
                }

                while (enumerator.MoveNext())
                {
                    if (!predicate(enumerator.Current))
                    {
                        continue;
                    }

                    result = default;
                    return false;
                }

                result = item;
                return true;

            } while (enumerator.MoveNext());

            result = default;
            return false;
        }

#if !NET6_0_OR_GREATER
        public static T FirstOrDefault<T>(this IEnumerable<T> source, T alternate)
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

        public static T FirstOrDefault<T>(this IEnumerable<T> source, Func<T, Boolean> predicate, T alternate)
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
#endif

        public static T FirstOrDefault<T>(this IEnumerable<T> source, Func<T> alternate)
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

        public static T FirstOrDefault<T>(this IEnumerable<T> source, Func<T, Boolean> predicate, Func<T> alternate)
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
        
        public static T? FirstOrNullable<T>(this IEnumerable<T> source) where T : struct
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            foreach (T item in source)
            {
                return item;
            }
            
            return null;
        }
        
        public static T? FirstOrNullable<T>(this IEnumerable<T> source, Func<T, Boolean> predicate) where T : struct
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
            
            return null;
        }

#if !NET6_0_OR_GREATER
        public static T LastOrDefault<T>(this IEnumerable<T> source, T alternate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Reverse().FirstOrDefault(alternate);
        }

        public static T LastOrDefault<T>(this IEnumerable<T> source, Func<T, Boolean> predicate, T alternate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return source.Reverse().FirstOrDefault(predicate, alternate);
        }
#endif

        public static T LastOrDefault<T>(this IEnumerable<T> source, Func<T> alternate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            return FirstOrDefault(source.Reverse(), alternate);
        }

        public static T LastOrDefault<T>(this IEnumerable<T> source, Func<T, Boolean> predicate, Func<T> alternate)
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

            return FirstOrDefault(source.Reverse(), predicate, alternate);
        }
        
        public static T? LastOrNullable<T>(this IEnumerable<T> source) where T : struct
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            return source.Reverse().FirstOrNullable();
        }
        
        public static T? LastOrNullable<T>(this IEnumerable<T> source, Func<T, Boolean> predicate) where T : struct
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            
            return source.Reverse().FirstOrNullable(predicate);
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

        public static T MaxOrDefault<T>(this IEnumerable<T> source, T alternate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.OrderByDescending().FirstOrDefault(alternate);
        }

        public static T MaxOrDefault<T>(this IEnumerable<T> source, T alternate, IComparer<T>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.OrderByDescending(comparer).FirstOrDefault(alternate);
        }

        public static T MaxOrDefault<T>(this IEnumerable<T> source, Func<T> alternate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            return source.OrderByDescending().FirstOrDefault(alternate);
        }

        public static T MaxOrDefault<T>(this IEnumerable<T> source, Func<T> alternate, IComparer<T>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            return source.OrderByDescending(comparer).FirstOrDefault(alternate);
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

#if !NET6_0_OR_GREATER
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
#endif

        public static T MaxByOrDefault<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector, T alternate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.OrderByDescending(selector).FirstOrDefault(alternate);
        }

        public static T MaxByOrDefault<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector, T alternate, IComparer<TKey>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.OrderByDescending(selector, comparer).FirstOrDefault(alternate);
        }

        public static T MaxByOrDefault<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector, Func<T> alternate)
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

            return source.OrderByDescending(selector).FirstOrDefault(alternate);
        }

        public static T MaxByOrDefault<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector, Func<T> alternate, IComparer<TKey>? comparer)
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

            return source.OrderByDescending(selector, comparer).FirstOrDefault(alternate);
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

        public static T MinOrDefault<T>(this IEnumerable<T> source, T alternate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.OrderBy().FirstOrDefault(alternate);
        }

        public static T MinOrDefault<T>(this IEnumerable<T> source, T alternate, IComparer<T>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.OrderBy(comparer).FirstOrDefault(alternate);
        }

        public static T MinOrDefault<T>(this IEnumerable<T> source, Func<T> alternate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            return source.OrderBy().FirstOrDefault(alternate);
        }

        public static T MinOrDefault<T>(this IEnumerable<T> source, Func<T> alternate, IComparer<T>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (alternate is null)
            {
                throw new ArgumentNullException(nameof(alternate));
            }

            return source.OrderBy(comparer).FirstOrDefault(alternate);
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

#if !NET6_0_OR_GREATER
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
#endif

        public static T MinByOrDefault<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector, T alternate)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.OrderBy(selector).FirstOrDefault(alternate);
        }

        public static T MinByOrDefault<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector, T alternate, IComparer<TKey>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return source.OrderBy(selector, comparer).FirstOrDefault(alternate);
        }

        public static T MinByOrDefault<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector, Func<T> alternate)
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

            return source.OrderBy(selector).FirstOrDefault(alternate);
        }

        public static T MinByOrDefault<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector, Func<T> alternate, IComparer<TKey>? comparer)
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

            return source.OrderBy(selector, comparer).FirstOrDefault(alternate);
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

        public static (T? Min, T? Max) MinMaxOrDefault<T>(this IEnumerable<T> source)
        {
            return MinMaxOrDefault(source, (IComparer<T>?) null);
        }

        public static (T? Min, T? Max) MinMaxOrDefault<T>(this IEnumerable<T> source, IComparer<T>? comparer)
        {
            return MinMaxOrDefault(source, default(T), comparer!);
        }

        public static (T Min, T Max) MinMaxOrDefault<T>(this IEnumerable<T> source, T alternate)
        {
            return MinMaxOrDefault(source, alternate, null);
        }

        public static (T Min, T Max) MinMaxOrDefault<T>(this IEnumerable<T> source, T alternate, IComparer<T>? comparer)
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

        public static (T Min, T Max) MinMaxOrDefault<T>(this IEnumerable<T> source, Func<T> alternate)
        {
            return MinMaxOrDefault(source, alternate, null);
        }

        public static (T Min, T Max) MinMaxOrDefault<T>(this IEnumerable<T> source, Func<T> alternate, IComparer<T>? comparer)
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

        public static (T Min, T Max) MinMaxBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector)
        {
            return MinMaxBy(source, selector, null);
        }

        public static (T Min, T Max) MinMaxBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector, IComparer<TKey>? comparer)
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
                throw new InvalidOperationException();
            }

            comparer ??= Comparer<TKey>.Default;

            T current = enumerator.Current;

            T min = current;
            T max = current;
            TKey key = selector(current);
            TKey minkey = key;
            TKey maxkey = key;

            while (enumerator.MoveNext())
            {
                current = enumerator.Current;
                key = selector(current);

                if (comparer.Compare(key, minkey) < 0)
                {
                    min = current;
                    minkey = selector(min);
                }

                if (comparer.Compare(key, maxkey) > 0)
                {
                    max = current;
                    maxkey = selector(max);
                }
            }

            return (min, max);
        }

        public static (T? Min, T? Max) MinMaxByOrDefault<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector)
        {
            return MinMaxByOrDefault(source, selector, (IComparer<TKey>?) null);
        }

        public static (T? Min, T? Max) MinMaxByOrDefault<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector, IComparer<TKey>? comparer)
        {
            return MinMaxByOrDefault(source, selector!, default(T), comparer!);
        }

        public static (T Min, T Max) MinMaxByOrDefault<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector, T alternate)
        {
            return MinMaxByOrDefault(source, selector, alternate, null);
        }

        public static (T Min, T Max) MinMaxByOrDefault<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector, T alternate, IComparer<TKey>? comparer)
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
                return (alternate, alternate);
            }

            comparer ??= Comparer<TKey>.Default;

            T current = enumerator.Current;

            T min = current;
            T max = current;
            TKey key = selector(current);
            TKey minkey = key;
            TKey maxkey = key;

            while (enumerator.MoveNext())
            {
                current = enumerator.Current;
                key = selector(current);

                if (comparer.Compare(key, minkey) < 0)
                {
                    min = current;
                    minkey = selector(min);
                }

                if (comparer.Compare(key, maxkey) > 0)
                {
                    max = current;
                    maxkey = selector(max);
                }
            }

            return (min, max);
        }

        public static (T Min, T Max) MinMaxByOrDefault<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector, Func<T> alternate)
        {
            return MinMaxByOrDefault(source, selector, alternate, null);
        }

        public static (T Min, T Max) MinMaxByOrDefault<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector, Func<T> alternate, IComparer<TKey>? comparer)
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

            using IEnumerator<T> enumerator = source.GetEnumerator();

            T current;

            if (!enumerator.MoveNext())
            {
                current = alternate();
                return (current, current);
            }

            comparer ??= Comparer<TKey>.Default;

            current = enumerator.Current;

            T min = current;
            T max = current;
            TKey key = selector(current);
            TKey minkey = key;
            TKey maxkey = key;

            while (enumerator.MoveNext())
            {
                current = enumerator.Current;
                key = selector(current);

                if (comparer.Compare(key, minkey) < 0)
                {
                    min = current;
                    minkey = selector(min);
                }

                if (comparer.Compare(key, maxkey) > 0)
                {
                    max = current;
                    maxkey = selector(max);
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

        public static Boolean IsDistinct<T>(this IEnumerable<T> source)
        {
            return IsDistinct(source, null);
        }

        public static Boolean IsDistinct<T>(this IEnumerable<T> source, IEqualityComparer<T>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            HashSet<T> set = new HashSet<T>(comparer);
            return source.All(item => set.Add(item));
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

            if (source.CountIfMaterialized() >= count)
            {
                return true;
            }

            if (count <= 0)
            {
                return true;
            }

            Int32 matches = 0;
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

            if (source.CountIfMaterialized() <= count)
            {
                return true;
            }

            if (count <= 0)
            {
                return false;
            }

            Int32 matches = 0;
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

            if (source.CountIfMaterialized() == count)
            {
                return true;
            }

            Int32 matches = 0;
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

        public static Double Difference<T>(this IEnumerable<T> source, IEnumerable<T> other)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            NullableDictionary<T, Int32> dictionary = new NullableDictionary<T, Int32>();

            Int32 count = 0;
            foreach (T item in source)
            {
                count++;
                if (dictionary.ContainsKey(item))
                {
                    dictionary[item]++;
                    continue;
                }

                dictionary.Add(item, 1);
            }

            foreach (T item in other)
            {
                count++;
                if (dictionary.ContainsKey(item))
                {
                    dictionary[item]--;
                    continue;
                }

                dictionary.Add(item, -1);
            }

            if (count <= 0)
            {
                return 0;
            }

            return dictionary.Values.Sum(Math.Abs) / (Double) count;
        }

        public static void Invoke<T>(this IEnumerable<T> source, Action<T> action)
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
            }
        }

        public static void InvokeWhere<T>(this IEnumerable<T> source, Func<T, Boolean> where, Action<T> action)
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
            }
        }

        public static void InvokeWhere<T>(this IEnumerable<T> source, Func<T, Int32, Boolean> where, Action<T> action)
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
            }
        }

        public static void InvokeWhereNot<T>(this IEnumerable<T> source, Func<T, Boolean> where, Action<T> action)
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

            InvokeWhere(source, item => !where(item), action);
        }

        public static void InvokeWhereNot<T>(this IEnumerable<T> source, Func<T, Int32, Boolean> where, Action<T> action)
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

            InvokeWhere(source, (item, index) => !where(item, index), action);
        }

        public static void Invoke<T>(this IEnumerable<T> source, Action<T, Int32> action)
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
            }
        }

        public static void InvokeWhere<T>(this IEnumerable<T> source, Func<T, Boolean> where, Action<T, Int32> action)
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
            }
        }

        public static void InvokeWhere<T>(this IEnumerable<T> source, Func<T, Int32, Boolean> where, Action<T, Int32> action)
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
            }
        }

        public static void InvokeWhereNot<T>(this IEnumerable<T> source, Func<T, Boolean> where, Action<T, Int32> action)
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

            InvokeWhere(source, item => !where(item), action);
        }

        public static void InvokeWhereNot<T>(this IEnumerable<T> source, Func<T, Int32, Boolean> where, Action<T, Int32> action)
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

            InvokeWhere(source, (item, index) => !where(item, index), action);
        }

        public static void InvokeBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector, Action<TKey> action)
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
            }
        }

        public static void InvokeByWhere<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector, Func<TKey, Boolean> where, Action<TKey> action)
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
            }
        }

        public static void InvokeByWhere<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector, Func<TKey, Int32, Boolean> where, Action<TKey> action)
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
            }
        }

        public static void InvokeByWhereNot<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector, Func<TKey, Boolean> where, Action<TKey> action)
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

            InvokeByWhere(source, selector, item => !where(item), action);
        }

        public static void InvokeByWhereNot<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector, Func<TKey, Int32, Boolean> where, Action<TKey> action)
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

            InvokeByWhere(source, selector, (item, index) => !where(item, index), action);
        }

        public static void InvokeBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector, Action<TKey, Int32> action)
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
            }
        }

        public static void InvokeByWhere<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector, Func<TKey, Boolean> where, Action<TKey, Int32> action)
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
            }
        }

        public static void InvokeByWhere<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector, Func<TKey, Int32, Boolean> where, Action<TKey, Int32> action)
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
            }
        }

        public static void InvokeByWhereNot<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector, Func<TKey, Boolean> where, Action<TKey, Int32> action)
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

            InvokeByWhere(source, selector, item => !where(item), action);
        }

        public static void InvokeByWhereNot<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector, Func<TKey, Int32, Boolean> where, Action<TKey, Int32> action)
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

            InvokeByWhere(source, selector, (item, index) => !where(item, index), action);
        }

        public static void InvokeEvery<T>(this IEnumerable<T> source, Action<T> action, Int32 every)
        {
            InvokeEvery(source, action, every, false);
        }

        public static void InvokeEvery<T>(this IEnumerable<T> source, Action<T> action, Int32 every, Boolean first)
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
                Invoke(source, action);
                return;
            }

            Int32 counter = first ? 0 : 1;
            source.InvokeWhere(_ => counter++ % every == 0, action);
        }

        public static void Evaluate(this IEnumerable source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (source is ICollection)
            {
                return;
            }

            IEnumerator enumerator = source.GetEnumerator();

            while (enumerator.MoveNext())
            {
            }
            
            if (enumerator is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }

        public static void Evaluate<T>(this IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (source.IsMaterialized())
            {
                return;
            }

            using IEnumerator<T> enumerator = source.GetEnumerator();

            while (enumerator.MoveNext())
            {
            }
        }

        public static Int32 SequenceHashCode<T>(this IEnumerable<T> source)
        {
            return SequenceHashCode(source, null);
        }

        public static Int32 SequenceHashCode<T>(this IEnumerable<T> source, IEqualityComparer<T>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return HashCodeUtilities.Combine(source, comparer);
        }

        public static Boolean SequencePartialEqual<T>(this IEnumerable<T> source, IEnumerable<T> sequence)
        {
            return SequencePartialEqual(source, sequence, null);
        }

        public static Boolean SequencePartialEqual<T>(this IEnumerable<T> source, IEnumerable<T> sequence, IEqualityComparer<T>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (sequence is null)
            {
                throw new ArgumentNullException(nameof(sequence));
            }

            comparer ??= EqualityComparer<T>.Default;

            using IEnumerator<T> enumerator1 = source.GetEnumerator();
            using IEnumerator<T> enumerator2 = sequence.GetEnumerator();

            do
            {
                if (!enumerator1.MoveNext())
                {
                    return !enumerator2.MoveNext();
                }

                if (!enumerator2.MoveNext())
                {
                    return true;
                }

                if (!comparer.Equals(enumerator1.Current, enumerator2.Current))
                {
                    return false;
                }

            } while (true);
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
                throw new ArgumentOutOfRangeException(nameof(size), size, null);
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
                throw new ArgumentOutOfRangeException(nameof(size), size, null);
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
                    // ReSharper disable once ConditionIsAlwaysTrueOrFalse
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