// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using NetExtender.Types.Maps;
using NetExtender.Types.Maps.Interfaces;

namespace NetExtender.Utilities.Types
{
    public static class MapUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Map<TKey, TValue> ToMap<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source) where TKey : notnull where TValue : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return new Map<TKey, TValue>(source);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Map<TKey, TValue> ToMap<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, IEqualityComparer<TKey>? comparer) where TKey : notnull where TValue : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return new Map<TKey, TValue>(source, comparer);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Map<TKey, TValue> ToMap<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, IEqualityComparer<TKey>? keyComparer, IEqualityComparer<TValue>? valueComparer) where TKey : notnull where TValue : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return new Map<TKey, TValue>(source, keyComparer, valueComparer);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Map<TKey, TElement> ToMap<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector) where TKey : notnull where TElement : notnull
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

            return new Map<TKey, TElement>(source.Select(item => new KeyValuePair<TKey, TElement>(keySelector(item), elementSelector(item))));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Map<TKey, TElement> ToMap<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey>? comparer) where TKey : notnull where TElement : notnull
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

            return new Map<TKey, TElement>(source.Select(item => new KeyValuePair<TKey, TElement>(keySelector(item), elementSelector(item))), comparer);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Map<TKey, TElement> ToMap<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector,
            IEqualityComparer<TKey>? keyComparer, IEqualityComparer<TElement>? valueComparer) where TKey : notnull where TElement : notnull
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

            return new Map<TKey, TElement>(source.Select(item => new KeyValuePair<TKey, TElement>(keySelector(item), elementSelector(item))), keyComparer, valueComparer);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexMap<TKey, TValue> ToIndexMap<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source) where TKey : notnull where TValue : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return new IndexMap<TKey, TValue>(source);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexMap<TKey, TValue> ToIndexMap<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, IEqualityComparer<TKey>? comparer) where TKey : notnull where TValue : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return new IndexMap<TKey, TValue>(source, comparer);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexMap<TKey, TValue> ToIndexMap<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, IEqualityComparer<TKey>? keyComparer, IEqualityComparer<TValue>? valueComparer) where TKey : notnull where TValue : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return new IndexMap<TKey, TValue>(source, keyComparer, valueComparer);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexMap<TKey, TElement> ToIndexMap<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector) where TKey : notnull where TElement : notnull
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

            return new IndexMap<TKey, TElement>(source.Select(item => new KeyValuePair<TKey, TElement>(keySelector(item), elementSelector(item))));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Map<TKey, TElement> ToIndexMap<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey>? comparer) where TKey : notnull where TElement : notnull
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

            return new IndexMap<TKey, TElement>(source.Select(item => new KeyValuePair<TKey, TElement>(keySelector(item), elementSelector(item))), comparer);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexMap<TKey, TElement> ToIndexMap<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector,
            IEqualityComparer<TKey>? keyComparer, IEqualityComparer<TElement>? valueComparer) where TKey : notnull where TElement : notnull
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

            return new IndexMap<TKey, TElement>(source.Select(item => new KeyValuePair<TKey, TElement>(keySelector(item), elementSelector(item))), keyComparer, valueComparer);
        }

        public static TValue GetOrAdd<TKey, TValue>(this IMap<TKey, TValue> dictionary, TKey key, TValue value)
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

        public static TValue GetOrAdd<TKey, TValue>(this IMap<TKey, TValue> dictionary, TKey key, Func<TValue> factory)
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
        
        public static TValue GetOrAdd<TKey, TValue>(this IMap<TKey, TValue> dictionary, TKey key, Func<TKey, TValue> factory)
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
    }
}