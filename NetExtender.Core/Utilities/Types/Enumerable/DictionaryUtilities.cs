// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using NetExtender.Types.Dictionaries;
using NetExtender.Types.Immutable.Dictionaries;
using NetExtender.Utilities.Numerics;

namespace NetExtender.Utilities.Types
{
    public static class DictionaryUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReadOnlyDictionary<TKey, TValue> AsReadOnly<TKey, TValue>(this Dictionary<TKey, TValue> dictionary) where TKey : notnull
        {
            if (dictionary is null)
            {
                throw new ArgumentNullException(nameof(dictionary));
            }

            return new ReadOnlyDictionary<TKey, TValue>(dictionary);
        }

        public static ConcurrentDictionary<TKey, TValue> ToConcurrent<TKey, TValue>(this IDictionary<TKey, TValue> dictionary) where TKey : notnull
        {
            return ToConcurrent(dictionary, null);
        }

        public static ConcurrentDictionary<TKey, TValue> ToConcurrent<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey>? comparer) where TKey : notnull
        {
            if (dictionary is null)
            {
                throw new ArgumentNullException(nameof(dictionary));
            }

            return new ConcurrentDictionary<TKey, TValue>(dictionary, comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return new Dictionary<TKey, TValue>(source);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, IEqualityComparer<TKey>? comparer) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return new Dictionary<TKey, TValue>(source, comparer);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<(TKey, TValue)> source) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.ToKeyValuePairs().ToDictionary();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<(TKey, TValue)> source, IEqualityComparer<TKey>? comparer) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.ToKeyValuePairs().ToDictionary(comparer);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SortedDictionary<TKey, TValue> ToSortedDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source) where TKey : notnull
        {
            return ToSortedDictionary(source, (IComparer<TKey>?) null);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SortedDictionary<TKey, TValue> ToSortedDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, IEqualityComparer<TKey>? equality) where TKey : notnull
        {
            return ToSortedDictionary(source, equality, null);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SortedDictionary<TKey, TValue> ToSortedDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, IComparer<TKey>? comparer) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            SortedDictionary<TKey, TValue> sorted = new SortedDictionary<TKey, TValue>(comparer);
            sorted.AddRange(source);
            
            return sorted;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SortedDictionary<TKey, TValue> ToSortedDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, IEqualityComparer<TKey>? equality, IComparer<TKey>? comparer) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return new SortedDictionary<TKey, TValue>(source.ToDictionary(equality), comparer);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SortedDictionary<TKey, TValue> ToSortedDictionary<TKey, TValue>(this IEnumerable<(TKey, TValue)> source) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.ToKeyValuePairs().ToSortedDictionary();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SortedDictionary<TKey, TValue> ToSortedDictionary<TKey, TValue>(this IEnumerable<(TKey, TValue)> source, IEqualityComparer<TKey>? equality) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.ToKeyValuePairs().ToSortedDictionary(equality);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SortedDictionary<TKey, TValue> ToSortedDictionary<TKey, TValue>(this IEnumerable<(TKey, TValue)> source, IComparer<TKey>? comparer) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.ToKeyValuePairs().ToSortedDictionary(comparer);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SortedDictionary<TKey, TValue> ToSortedDictionary<TKey, TValue>(this IEnumerable<(TKey, TValue)> source, IEqualityComparer<TKey>? equality, IComparer<TKey>? comparer) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.ToKeyValuePairs().ToSortedDictionary(equality, comparer);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SortedDictionary<TKey, TSource> ToSortedDictionary<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (keySelector is null)
            {
                throw new ArgumentNullException(nameof(keySelector));
            }

            return new SortedDictionary<TKey, TSource>(source.ToDictionary(keySelector));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SortedDictionary<TKey, TSource> ToSortedDictionary<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey>? equality) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (keySelector is null)
            {
                throw new ArgumentNullException(nameof(keySelector));
            }

            return new SortedDictionary<TKey, TSource>(source.ToDictionary(keySelector, equality));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SortedDictionary<TKey, TSource> ToSortedDictionary<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey>? comparer) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (keySelector is null)
            {
                throw new ArgumentNullException(nameof(keySelector));
            }

            return new SortedDictionary<TKey, TSource>(source.ToDictionary(keySelector), comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SortedDictionary<TKey, TSource> ToSortedDictionary<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector,
            IEqualityComparer<TKey>? equality, IComparer<TKey>? comparer) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (keySelector is null)
            {
                throw new ArgumentNullException(nameof(keySelector));
            }

            return new SortedDictionary<TKey, TSource>(source.ToDictionary(keySelector, equality), comparer);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SortedDictionary<TKey, TElement> ToSortedDictionary<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector,
            Func<TSource, TElement> elementSelector) where TKey : notnull
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

            return new SortedDictionary<TKey, TElement>(source.ToDictionary(keySelector, elementSelector));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SortedDictionary<TKey, TElement> ToSortedDictionary<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector,
            Func<TSource, TElement> elementSelector, IEqualityComparer<TKey>? equality) where TKey : notnull
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

            return new SortedDictionary<TKey, TElement>(source.ToDictionary(keySelector, elementSelector, equality));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SortedDictionary<TKey, TElement> ToSortedDictionary<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector,
            Func<TSource, TElement> elementSelector, IComparer<TKey>? comparer) where TKey : notnull
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

            return new SortedDictionary<TKey, TElement>(source.ToDictionary(keySelector, elementSelector), comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SortedDictionary<TKey, TElement> ToSortedDictionary<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector,
            Func<TSource, TElement> elementSelector, IEqualityComparer<TKey>? equality, IComparer<TKey>? comparer) where TKey : notnull
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

            return new SortedDictionary<TKey, TElement>(source.ToDictionary(keySelector, elementSelector, equality), comparer);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexDictionary<TKey, TValue> ToIndexDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source) where TKey : notnull
        {
            return ToIndexDictionary(source, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexDictionary<TKey, TValue> ToIndexDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, IEqualityComparer<TKey>? comparer) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return new IndexDictionary<TKey, TValue>(source, comparer);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexDictionary<TKey, TValue> ToIndexDictionary<TKey, TValue>(this IEnumerable<(TKey, TValue)> source) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.ToKeyValuePairs().ToIndexDictionary();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexDictionary<TKey, TValue> ToIndexDictionary<TKey, TValue>(this IEnumerable<(TKey, TValue)> source, IEqualityComparer<TKey>? comparer) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.ToKeyValuePairs().ToIndexDictionary(comparer);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexDictionary<TKey, TSource> ToIndexDictionary<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (keySelector is null)
            {
                throw new ArgumentNullException(nameof(keySelector));
            }

            return new IndexDictionary<TKey, TSource>(source.Select(item => new KeyValuePair<TKey, TSource>(keySelector(item), item)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexDictionary<TKey, TSource> ToIndexDictionary<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey>? comparer) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (keySelector is null)
            {
                throw new ArgumentNullException(nameof(keySelector));
            }

            return new IndexDictionary<TKey, TSource>(source.Select(item => new KeyValuePair<TKey, TSource>(keySelector(item), item)), comparer);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexDictionary<TKey, TElement> ToIndexDictionary<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector,
            Func<TSource, TElement> elementSelector) where TKey : notnull
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

            return new IndexDictionary<TKey, TElement>(source.Select(item => new KeyValuePair<TKey, TElement>(keySelector(item), elementSelector(item))));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexDictionary<TKey, TElement> ToIndexDictionary<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector,
            Func<TSource, TElement> elementSelector, IEqualityComparer<TKey>? comparer) where TKey : notnull
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

            return new IndexDictionary<TKey, TElement>(source.Select(item => new KeyValuePair<TKey, TElement>(keySelector(item), elementSelector(item))), comparer);
        }
        
        public static ImmutableMultiDictionary<TKey, TValue> ToImmutableMultiDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, ImmutableHashSet<TValue>>> source) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            return ImmutableMultiDictionary<TKey, TValue>.Empty.AddRange(source);
        }

        public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (dictionary is null)
            {
                throw new ArgumentNullException(nameof(dictionary));
            }

            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (dictionary.TryGetValue(key, out TValue? result))
            {
                return result;
            }

            dictionary.Add(key, value);
            return value;
        }

        public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TValue> factory)
        {
            if (dictionary is null)
            {
                throw new ArgumentNullException(nameof(dictionary));
            }

            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            if (dictionary.TryGetValue(key, out TValue? value))
            {
                return value;
            }

            value = factory.Invoke();
            dictionary.Add(key, value);
            return value;
        }
        
        public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TKey, TValue> factory)
        {
            if (dictionary is null)
            {
                throw new ArgumentNullException(nameof(dictionary));
            }

            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            if (dictionary.TryGetValue(key, out TValue? value))
            {
                return value;
            }

            value = factory.Invoke(key);
            dictionary.Add(key, value);
            return value;
        }

        public static IDictionary<TValue, TKey> Reverse<TKey, TValue>(this IDictionary<TKey, TValue> source) where TKey : notnull where TValue : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            Dictionary<TValue, TKey> dictionary = new Dictionary<TValue, TKey>();

            foreach ((TKey key, TValue value) in source)
            {
                if (!dictionary.ContainsKey(value))
                {
                    dictionary.Add(value, key);
                }
            }

            return dictionary;
        }

        public static KeyValuePair<TKey, TValue>? NearestLower<TKey, TValue>(this SortedDictionary<TKey, TValue> source, TKey value) where TKey : notnull
        {
            return NearestLower(source, value, out KeyValuePair<TKey, TValue>? result) ? result : default;
        }

        public static Boolean NearestLower<TKey, TValue>(this SortedDictionary<TKey, TValue> source, TKey value, out KeyValuePair<TKey, TValue>? result) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return NearestLower(source, value, source.Comparer, out result);
        }

        public static KeyValuePair<TKey, TValue>? NearestLower<TKey, TValue>(this SortedDictionary<TKey, TValue> source, TKey value, IComparer<TKey>? comparer) where TKey : notnull
        {
            return NearestLower(source, value, comparer, out KeyValuePair<TKey, TValue>? result) ? result : default;
        }

        public static Boolean NearestLower<TKey, TValue>(this SortedDictionary<TKey, TValue> source, TKey value, IComparer<TKey>? comparer, out KeyValuePair<TKey, TValue>? result) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            comparer ??= source.Comparer;

            switch (source.Count)
            {
                case 0:
                    result = default;
                    return false;
                case 1:
                    result = source.First();
                    return true;
                default:
                    Boolean successful = source.TryGetLast(item => comparer.Compare(item.Key, value) < 0, out KeyValuePair<TKey, TValue> pair);
                    result = pair;
                    return successful;
            }
        }

        public static KeyValuePair<TKey, TValue>? NearestHigher<TKey, TValue>(this SortedDictionary<TKey, TValue> source, TKey value) where TKey : notnull
        {
            return NearestHigher(source, value, out KeyValuePair<TKey, TValue>? result) ? result : default;
        }
        
        public static Boolean NearestHigher<TKey, TValue>(this SortedDictionary<TKey, TValue> source, TKey value, out KeyValuePair<TKey, TValue>? result) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return NearestHigher(source, value, source.Comparer, out result);
        }

        public static KeyValuePair<TKey, TValue>? NearestHigher<TKey, TValue>(this SortedDictionary<TKey, TValue> source, TKey value, IComparer<TKey>? comparer) where TKey : notnull
        {
            return NearestHigher(source, value, comparer, out KeyValuePair<TKey, TValue>? result) ? result : default;
        }

        public static Boolean NearestHigher<TKey, TValue>(this SortedDictionary<TKey, TValue> source, TKey value, IComparer<TKey>? comparer, out KeyValuePair<TKey, TValue>? result) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            switch (source.Count)
            {
                case 0:
                    result = default;
                    return false;
                case 1:
                    result = source.First();
                    return true;
                default:
                    comparer ??= source.Comparer;
                    Boolean successful = source.TryGetFirst(item => comparer.Compare(item.Key, value) > 0, out KeyValuePair<TKey, TValue> pair);
                    result = pair;
                    return successful;
            }
        }

        public static (KeyValuePair<TKey, TValue>?, KeyValuePair<TKey, TValue>?) Nearest<TKey, TValue>(this SortedDictionary<TKey, TValue> source, TKey value) where TKey : notnull
        {
            return Nearest(source, value, out _);
        }

        public static (KeyValuePair<TKey, TValue>?, KeyValuePair<TKey, TValue>?) Nearest<TKey, TValue>(this SortedDictionary<TKey, TValue> source, TKey value, out MathPositionType result) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return Nearest(source, value, source.Comparer, out result);
        }

        public static (KeyValuePair<TKey, TValue>?, KeyValuePair<TKey, TValue>?) Nearest<TKey, TValue>(this SortedDictionary<TKey, TValue> source, TKey value, IComparer<TKey>? comparer) where TKey : notnull
        {
            return Nearest(source, value, comparer, out _);
        }

        public static (KeyValuePair<TKey, TValue>?, KeyValuePair<TKey, TValue>?) Nearest<TKey, TValue>(this SortedDictionary<TKey, TValue> source, TKey value, IComparer<TKey>? comparer, out MathPositionType result) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            comparer ??= source.Comparer;

            if (source.Count <= 0)
            {
                result = MathPositionType.None;
                return default;
            }
            
            Boolean first = source.TryGetLast(item => comparer.Compare(item.Key, value) < 0, out KeyValuePair<TKey, TValue> left);
            Boolean second = source.TryGetFirst(item => comparer.Compare(item.Key, value) > 0, out KeyValuePair<TKey, TValue> right);

            result = first switch
            {
                true when second => MathPositionType.Both,
                true => MathPositionType.Left,
                _ => second ? MathPositionType.Right : MathPositionType.None
            };

            return (left, right);
        }

        public static Boolean NearestLower<TKey, TValue>(this ImmutableSortedDictionary<TKey, TValue> source, TKey value, out KeyValuePair<TKey, TValue>? result) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return NearestLower(source, value, source.KeyComparer, out result);
        }

        public static KeyValuePair<TKey, TValue>? NearestLower<TKey, TValue>(this ImmutableSortedDictionary<TKey, TValue> source, TKey value, IComparer<TKey>? comparer) where TKey : notnull
        {
            return NearestLower(source, value, comparer, out KeyValuePair<TKey, TValue>? result) ? result : default;
        }

        public static Boolean NearestLower<TKey, TValue>(this ImmutableSortedDictionary<TKey, TValue> source, TKey value, IComparer<TKey>? comparer, out KeyValuePair<TKey, TValue>? result) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            comparer ??= source.KeyComparer;

            switch (source.Count)
            {
                case 0:
                    result = default;
                    return false;
                case 1:
                    result = source.First();
                    return true;
                default:
                    Boolean successful = source.TryGetLast(item => comparer.Compare(item.Key, value) < 0, out KeyValuePair<TKey, TValue> pair);
                    result = pair;
                    return successful;
            }
        }
        
        public static KeyValuePair<TKey, TValue>? NearestHigher<TKey, TValue>(this ImmutableSortedDictionary<TKey, TValue> source, TKey value) where TKey : notnull
        {
            return NearestHigher(source, value, out KeyValuePair<TKey, TValue>? result) ? result : default;
        }
        
        public static Boolean NearestHigher<TKey, TValue>(this ImmutableSortedDictionary<TKey, TValue> source, TKey value, out KeyValuePair<TKey, TValue>? result) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return NearestHigher(source, value, source.KeyComparer, out result);
        }

        public static KeyValuePair<TKey, TValue>? NearestHigher<TKey, TValue>(this ImmutableSortedDictionary<TKey, TValue> source, TKey value, IComparer<TKey>? comparer) where TKey : notnull
        {
            return NearestHigher(source, value, comparer, out KeyValuePair<TKey, TValue>? result) ? result : default;
        }

        public static Boolean NearestHigher<TKey, TValue>(this ImmutableSortedDictionary<TKey, TValue> source, TKey value, IComparer<TKey>? comparer, out KeyValuePair<TKey, TValue>? result) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            switch (source.Count)
            {
                case 0:
                    result = default;
                    return false;
                case 1:
                    result = source.First();
                    return true;
                default:
                    comparer ??= source.KeyComparer;
                    Boolean successful = source.TryGetFirst(item => comparer.Compare(item.Key, value) > 0, out KeyValuePair<TKey, TValue> pair);
                    result = pair;
                    return successful;
            }
        }

        public static (KeyValuePair<TKey, TValue>?, KeyValuePair<TKey, TValue>?) Nearest<TKey, TValue>(this ImmutableSortedDictionary<TKey, TValue> source, TKey value) where TKey : notnull
        {
            return Nearest(source, value, out _);
        }

        public static (KeyValuePair<TKey, TValue>?, KeyValuePair<TKey, TValue>?) Nearest<TKey, TValue>(this ImmutableSortedDictionary<TKey, TValue> source, TKey value, out MathPositionType result) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return Nearest(source, value, source.KeyComparer, out result);
        }

        public static (KeyValuePair<TKey, TValue>?, KeyValuePair<TKey, TValue>?) Nearest<TKey, TValue>(this ImmutableSortedDictionary<TKey, TValue> source, TKey value, IComparer<TKey>? comparer) where TKey : notnull
        {
            return Nearest(source, value, comparer, out _);
        }

        public static (KeyValuePair<TKey, TValue>?, KeyValuePair<TKey, TValue>?) Nearest<TKey, TValue>(this ImmutableSortedDictionary<TKey, TValue> source, TKey value, IComparer<TKey>? comparer, out MathPositionType result) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (source.Count <= 0)
            {
                result = MathPositionType.None;
                return default;
            }
            
            comparer ??= source.KeyComparer;
            
            Boolean first = source.TryGetLast(item => comparer.Compare(item.Key, value) < 0, out KeyValuePair<TKey, TValue> left);
            Boolean second = source.TryGetFirst(item => comparer.Compare(item.Key, value) > 0, out KeyValuePair<TKey, TValue> right);

            result = first switch
            {
                true when second => MathPositionType.Both,
                true => MathPositionType.Left,
                _ => second ? MathPositionType.Right : MathPositionType.None
            };

            return (left, right);
        }
        
        public static TKey? NearestLowerKey<TKey, TValue>(this SortedDictionary<TKey, TValue> source, TKey value) where TKey : notnull
        {
            return NearestLowerKey(source, value, out TKey? result) ? result : default;
        }

        public static Boolean NearestLowerKey<TKey, TValue>(this SortedDictionary<TKey, TValue> source, TKey value, [MaybeNullWhen(false)] out TKey result) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return NearestLowerKey(source, value, source.Comparer, out result);
        }

        public static TKey? NearestLowerKey<TKey, TValue>(this SortedDictionary<TKey, TValue> source, TKey value, IComparer<TKey>? comparer) where TKey : notnull
        {
            return NearestLowerKey(source, value, comparer, out TKey? result) ? result : default;
        }

        public static Boolean NearestLowerKey<TKey, TValue>(this SortedDictionary<TKey, TValue> source, TKey value, IComparer<TKey>? comparer, [MaybeNullWhen(false)] out TKey result) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            comparer ??= source.Comparer;

            switch (source.Count)
            {
                case 0:
                    result = default;
                    return false;
                case 1:
                    result = source.Keys.First();
                    return true;
                default:
                    return source.Keys.TryGetLast(item => comparer.Compare(item, value) < 0, out result);
            }
        }

        public static TKey? NearestHigherKey<TKey, TValue>(this SortedDictionary<TKey, TValue> source, TKey value) where TKey : notnull
        {
            return NearestHigherKey(source, value, out TKey? result) ? result : default;
        }
        
        public static Boolean NearestHigherKey<TKey, TValue>(this SortedDictionary<TKey, TValue> source, TKey value, [MaybeNullWhen(false)] out TKey result) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return NearestHigherKey(source, value, source.Comparer, out result);
        }

        public static TKey? NearestHigherKey<TKey, TValue>(this SortedDictionary<TKey, TValue> source, TKey value, IComparer<TKey>? comparer) where TKey : notnull
        {
            return NearestHigherKey(source, value, comparer, out TKey? result) ? result : default;
        }

        public static Boolean NearestHigherKey<TKey, TValue>(this SortedDictionary<TKey, TValue> source, TKey value, IComparer<TKey>? comparer, [MaybeNullWhen(false)] out TKey result) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            switch (source.Count)
            {
                case 0:
                    result = default;
                    return false;
                case 1:
                    result = source.Keys.First();
                    return true;
                default:
                    comparer ??= source.Comparer;
                    return source.Keys.TryGetFirst(item => comparer.Compare(item, value) > 0, out result);
            }
        }

        public static (TKey?, TKey?) NearestKey<TKey, TValue>(this SortedDictionary<TKey, TValue> source, TKey value) where TKey : notnull
        {
            return NearestKey(source, value, out _);
        }

        public static (TKey?, TKey?) NearestKey<TKey, TValue>(this SortedDictionary<TKey, TValue> source, TKey value, out MathPositionType result) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return NearestKey(source, value, source.Comparer, out result);
        }

        public static (TKey?, TKey?) NearestKey<TKey, TValue>(this SortedDictionary<TKey, TValue> source, TKey value, IComparer<TKey>? comparer) where TKey : notnull
        {
            return NearestKey(source, value, comparer, out _);
        }

        public static (TKey?, TKey?) NearestKey<TKey, TValue>(this SortedDictionary<TKey, TValue> source, TKey value, IComparer<TKey>? comparer, out MathPositionType result) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            comparer ??= source.Comparer;

            if (source.Count <= 0)
            {
                result = MathPositionType.None;
                return default;
            }
            
            Boolean first = source.Keys.TryGetLast(item => comparer.Compare(item, value) < 0, out TKey? left);
            Boolean second = source.Keys.TryGetFirst(item => comparer.Compare(item, value) > 0, out TKey? right);

            result = first switch
            {
                true when second => MathPositionType.Both,
                true => MathPositionType.Left,
                _ => second ? MathPositionType.Right : MathPositionType.None
            };

            return (left, right);
        }

        public static Boolean NearestLowerKey<TKey, TValue>(this ImmutableSortedDictionary<TKey, TValue> source, TKey value, [MaybeNullWhen(false)] out TKey result) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return NearestLowerKey(source, value, source.KeyComparer, out result);
        }

        public static TKey? NearestLowerKey<TKey, TValue>(this ImmutableSortedDictionary<TKey, TValue> source, TKey value, IComparer<TKey>? comparer) where TKey : notnull
        {
            return NearestLowerKey(source, value, comparer, out TKey? result) ? result : default;
        }

        public static Boolean NearestLowerKey<TKey, TValue>(this ImmutableSortedDictionary<TKey, TValue> source, TKey value, IComparer<TKey>? comparer, [MaybeNullWhen(false)] out TKey result) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            comparer ??= source.KeyComparer;

            switch (source.Count)
            {
                case 0:
                    result = default;
                    return false;
                case 1:
                    result = source.Keys.First();
                    return true;
                default:
                    return source.Keys.TryGetLast(item => comparer.Compare(item, value) < 0, out result);
            }
        }

        public static TKey? NearestHigherKey<TKey, TValue>(this ImmutableSortedDictionary<TKey, TValue> source, TKey value) where TKey : notnull
        {
            return NearestHigherKey(source, value, out TKey? result) ? result : default;
        }
        
        public static Boolean NearestHigherKey<TKey, TValue>(this ImmutableSortedDictionary<TKey, TValue> source, TKey value, [MaybeNullWhen(false)] out TKey result) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return NearestHigherKey(source, value, source.KeyComparer, out result);
        }

        public static TKey? NearestHigherKey<TKey, TValue>(this ImmutableSortedDictionary<TKey, TValue> source, TKey value, IComparer<TKey>? comparer) where TKey : notnull
        {
            return NearestHigherKey(source, value, comparer, out TKey? result) ? result : default;
        }

        public static Boolean NearestHigherKey<TKey, TValue>(this ImmutableSortedDictionary<TKey, TValue> source, TKey value, IComparer<TKey>? comparer, [MaybeNullWhen(false)] out TKey result) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            switch (source.Count)
            {
                case 0:
                    result = default;
                    return false;
                case 1:
                    result = source.Keys.First();
                    return true;
                default:
                    comparer ??= source.KeyComparer;
                    return source.Keys.TryGetFirst(item => comparer.Compare(item, value) > 0, out result);
            }
        }

        public static (TKey?, TKey?) NearestKey<TKey, TValue>(this ImmutableSortedDictionary<TKey, TValue> source, TKey value) where TKey : notnull
        {
            return NearestKey(source, value, out _);
        }

        public static (TKey?, TKey?) NearestKey<TKey, TValue>(this ImmutableSortedDictionary<TKey, TValue> source, TKey value, out MathPositionType result) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return NearestKey(source, value, source.KeyComparer, out result);
        }

        public static (TKey?, TKey?) NearestKey<TKey, TValue>(this ImmutableSortedDictionary<TKey, TValue> source, TKey value, IComparer<TKey>? comparer) where TKey : notnull
        {
            return NearestKey(source, value, comparer, out _);
        }

        public static (TKey?, TKey?) NearestKey<TKey, TValue>(this ImmutableSortedDictionary<TKey, TValue> source, TKey value, IComparer<TKey>? comparer, out MathPositionType result) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (source.Count <= 0)
            {
                result = MathPositionType.None;
                return default;
            }
            
            comparer ??= source.KeyComparer;
            
            Boolean first = source.Keys.TryGetLast(item => comparer.Compare(item, value) < 0, out TKey? left);
            Boolean second = source.Keys.TryGetFirst(item => comparer.Compare(item, value) > 0, out TKey? right);

            result = first switch
            {
                true when second => MathPositionType.Both,
                true => MathPositionType.Left,
                _ => second ? MathPositionType.Right : MathPositionType.None
            };

            return (left, right);
        }

        public static void RemoveRange<TKey, TValue>(this IDictionary<TKey, TValue> source, IEnumerable<TKey> other) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            foreach (TKey key in other)
            {
                source.Remove(key);
            }
        }
        
        public static void RemoveRange<TKey, TValue>(this IDictionary<TKey, TValue> source, IEnumerable<KeyValuePair<TKey, TValue>> other) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            foreach (KeyValuePair<TKey, TValue> pair in other)
            {
                source.Remove(pair);
            }
        }
        
        public static void Union<TKey, TValue>(this IDictionary<TKey, TValue> source, IEnumerable<KeyValuePair<TKey, TValue>> other) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            switch (source)
            {
                case Dictionary<TKey, TValue> dictionary:
                    Union(dictionary, other);
                    return;
                case SortedDictionary<TKey, TValue> dictionary:
                    Union(dictionary, other);
                    return;
                default:
                    source.AddRange(other.Where(pair => !source.ContainsKey(pair.Key)));
                    break;
            }
        }
        
        public static void Union<TKey, TValue>(this Dictionary<TKey, TValue> source, IEnumerable<KeyValuePair<TKey, TValue>> other) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            source.AddRange(other.WhereNotNull(pair => !source.ContainsKey(pair.Key)));
        }
        
        public static void Union<TKey, TValue>(this SortedDictionary<TKey, TValue> source, IEnumerable<KeyValuePair<TKey, TValue>> other) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            source.AddRange(other.WhereNotNull(pair => !source.ContainsKey(pair.Key)));
        }
        
        public static IImmutableDictionary<TKey, TValue> Union<TKey, TValue>(this IImmutableDictionary<TKey, TValue> source, IEnumerable<KeyValuePair<TKey, TValue>> other) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            return source switch
            {
                ImmutableDictionary<TKey, TValue> dictionary => Union(dictionary, other),
                ImmutableSortedDictionary<TKey, TValue> dictionary => Union(dictionary, other),
                _ => source.AddRange(other.Where(pair => !source.ContainsKey(pair.Key)))
            };
        }
        
        public static ImmutableDictionary<TKey, TValue> Union<TKey, TValue>(this ImmutableDictionary<TKey, TValue> source, IEnumerable<KeyValuePair<TKey, TValue>> other) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            return source.AddRange(other.WhereNotNull(pair => !source.ContainsKey(pair.Key)));
        }
        
        public static ImmutableSortedDictionary<TKey, TValue> Union<TKey, TValue>(this ImmutableSortedDictionary<TKey, TValue> source, IEnumerable<KeyValuePair<TKey, TValue>> other) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            return source.AddRange(other.WhereNotNull(pair => !source.ContainsKey(pair.Key)));
        }

        public static void Intersect<TKey, TValue>(this IDictionary<TKey, TValue> source, IEnumerable<TKey> other) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            switch (source)
            {
                case Dictionary<TKey, TValue> dictionary:
                    Intersect(dictionary, other);
                    return;
                case SortedDictionary<TKey, TValue> dictionary:
                    Intersect(dictionary, other);
                    return;
            }

            Dictionary<TKey, TValue> intersect = new Dictionary<TKey, TValue>(source.Count);

            foreach (TKey key in other)
            {
                if (source.TryGetValue(key, out TValue? value))
                {
                    intersect.Add(key, value);
                }
            }
            
            source.Clear();

            foreach ((TKey key, TValue value) in intersect)
            {
                source.TryAdd(key, value);
            }
        }
        
        public static void Intersect<TKey, TValue>(this IDictionary<TKey, TValue> source, IEnumerable<KeyValuePair<TKey, TValue>> other) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            Intersect(source, other.Select(item => item.Key));
        }

        public static void Intersect<TKey, TValue>(this Dictionary<TKey, TValue> source, IEnumerable<TKey> other) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            Dictionary<TKey, TValue> intersect = new Dictionary<TKey, TValue>(source.Count, source.Comparer);

            foreach (TKey key in other.WhereNotNull())
            {
                if (source.TryGetValue(key, out TValue? value))
                {
                    intersect.Add(key, value);
                }
            }
            
            source.Clear();
            source.EnsureCapacity(intersect.Count);

            foreach ((TKey key, TValue value) in intersect)
            {
                source.TryAdd(key, value);
            }
        }
        
        public static void Intersect<TKey, TValue>(this Dictionary<TKey, TValue> source, IEnumerable<KeyValuePair<TKey, TValue>> other) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            Intersect(source, other.Select(item => item.Key));
        }

        public static void Intersect<TKey, TValue>(this SortedDictionary<TKey, TValue> source, IEnumerable<TKey> other) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            SortedDictionary<TKey, TValue> intersect = new SortedDictionary<TKey, TValue>(source.Comparer);

            foreach (TKey key in other.WhereNotNull())
            {
                if (source.TryGetValue(key, out TValue? value))
                {
                    intersect.TryAdd(key, value);
                }
            }
            
            source.Clear();

            foreach ((TKey key, TValue value) in intersect)
            {
                source.Add(key, value);
            }
        }
        
        public static void Intersect<TKey, TValue>(this SortedDictionary<TKey, TValue> source, IEnumerable<KeyValuePair<TKey, TValue>> other) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            Intersect(source, other.Select(item => item.Key));
        }

        public static IImmutableDictionary<TKey, TValue> Intersect<TKey, TValue>(this IImmutableDictionary<TKey, TValue> source, IEnumerable<TKey> other) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            switch (source)
            {
                case ImmutableDictionary<TKey, TValue> dictionary:
                    return Intersect(dictionary, other);
                case ImmutableSortedDictionary<TKey, TValue> dictionary:
                    return Intersect(dictionary, other);
            }

            Dictionary<TKey, TValue> intersect = new Dictionary<TKey, TValue>(source.Count);

            foreach (TKey key in other)
            {
                if (source.TryGetValue(key, out TValue? value))
                {
                    intersect.TryAdd(key, value);
                }
            }

            return intersect.ToImmutableDictionary();
        }
        
        public static IImmutableDictionary<TKey, TValue> Intersect<TKey, TValue>(this IImmutableDictionary<TKey, TValue> source, IEnumerable<KeyValuePair<TKey, TValue>> other) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            return Intersect(source, other.Select(item => item.Key));
        }

        public static ImmutableDictionary<TKey, TValue> Intersect<TKey, TValue>(this ImmutableDictionary<TKey, TValue> source, IEnumerable<TKey> other) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            Dictionary<TKey, TValue> intersect = new Dictionary<TKey, TValue>(source.Count, source.KeyComparer);

            foreach (TKey key in other.WhereNotNull())
            {
                if (source.TryGetValue(key, out TValue? value))
                {
                    intersect.TryAdd(key, value);
                }
            }

            return intersect.ToImmutableDictionary(source.KeyComparer, source.ValueComparer);
        }
        
        public static ImmutableDictionary<TKey, TValue> Intersect<TKey, TValue>(this ImmutableDictionary<TKey, TValue> source, IEnumerable<KeyValuePair<TKey, TValue>> other) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            return Intersect(source, other.Select(item => item.Key));
        }

        public static ImmutableSortedDictionary<TKey, TValue> Intersect<TKey, TValue>(this ImmutableSortedDictionary<TKey, TValue> source, IEnumerable<TKey> other) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            SortedDictionary<TKey, TValue> intersect = new SortedDictionary<TKey, TValue>(source.KeyComparer);

            foreach (TKey key in other.WhereNotNull())
            {
                if (source.TryGetValue(key, out TValue? value))
                {
                    intersect.TryAdd(key, value);
                }
            }

            return intersect.ToImmutableSortedDictionary(source.KeyComparer, source.ValueComparer);
        }
        
        public static ImmutableSortedDictionary<TKey, TValue> Intersect<TKey, TValue>(this ImmutableSortedDictionary<TKey, TValue> source, IEnumerable<KeyValuePair<TKey, TValue>> other) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            return Intersect(source, other.Select(item => item.Key));
        }

        public static void Except<TKey, TValue>(this IDictionary<TKey, TValue> source, IEnumerable<TKey> other) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            switch (source)
            {
                case Dictionary<TKey, TValue> dictionary:
                    Except(dictionary, other);
                    return;
                case SortedDictionary<TKey, TValue> dictionary:
                    Except(dictionary, other);
                    return;
                default:
                    source.RemoveRange(other);
                    return;
            }
        }
        
        public static void Except<TKey, TValue>(this IDictionary<TKey, TValue> source, IEnumerable<KeyValuePair<TKey, TValue>> other) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            switch (source)
            {
                case Dictionary<TKey, TValue> dictionary:
                    Except(dictionary, other);
                    return;
                case SortedDictionary<TKey, TValue> dictionary:
                    Except(dictionary, other);
                    return;
                default:
                    source.RemoveRange(other);
                    return;
            }
        }
        
        public static void Except<TKey, TValue>(this Dictionary<TKey, TValue> source, IEnumerable<TKey> other) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            source.RemoveRange(other);
        }
        
        public static void Except<TKey, TValue>(this Dictionary<TKey, TValue> source, IEnumerable<KeyValuePair<TKey, TValue>> other) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            source.RemoveRange(other);
        }
        
        public static void Except<TKey, TValue>(this SortedDictionary<TKey, TValue> source, IEnumerable<TKey> other) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            source.RemoveRange(other);
        }
        
        public static void Except<TKey, TValue>(this SortedDictionary<TKey, TValue> source, IEnumerable<KeyValuePair<TKey, TValue>> other) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            source.RemoveRange(other);
        }

        public static IImmutableDictionary<TKey, TValue> Except<TKey, TValue>(this IImmutableDictionary<TKey, TValue> source, IEnumerable<TKey> other) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            return source switch
            {
                ImmutableDictionary<TKey, TValue> dictionary => Except(dictionary, other),
                ImmutableSortedDictionary<TKey, TValue> dictionary => Except(dictionary, other),
                _ => source.RemoveRange(other)
            };
        }

        public static ImmutableDictionary<TKey, TValue> Except<TKey, TValue>(this ImmutableDictionary<TKey, TValue> source, IEnumerable<TKey> other) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            return source.RemoveRange(other.WhereNotNull());
        }
        
        public static ImmutableSortedDictionary<TKey, TValue> Except<TKey, TValue>(this ImmutableSortedDictionary<TKey, TValue> source, IEnumerable<TKey> other) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            return source.RemoveRange(other.WhereNotNull());
        }

        public static Dictionary<TKey, TValue> Clone<TKey, TValue>(this Dictionary<TKey, TValue> source) where TKey : notnull where TValue : ICloneable
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>(source.Count, source.Comparer);

            foreach ((TKey key, TValue value) in source)
            {
                dictionary.Add(key, (TValue) value.Clone());
            }

            return dictionary;
        }
        
        public static SortedDictionary<TKey, TValue> Clone<TKey, TValue>(this SortedDictionary<TKey, TValue> source) where TKey : notnull where TValue : ICloneable
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            SortedDictionary<TKey, TValue> dictionary = new SortedDictionary<TKey, TValue>(source.Comparer);

            foreach ((TKey key, TValue value) in source)
            {
                dictionary.Add(key, (TValue) value.Clone());
            }

            return dictionary;
        }
    }
}