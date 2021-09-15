// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using NetExtender.Types.Maps;
using NetExtender.Types.Maps.Interfaces;

namespace NetExtender.Utilities.Types
{
    public static class MapUtilities
    {
        public static Boolean TryGetKey<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, TValue key, [MaybeNullWhen(false)] out TKey result)
        {
            return TryGetKey(source, key, default(TKey)!, out result);
        }
        
        public static Boolean TryGetKey<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, TValue key, TKey @default, [MaybeNullWhen(false)] out TKey result)
        {
            switch (source)
            {
                case IMap<TKey, TValue> map when map.TryGetKey(key, out result):
                    return true;
                case IMap<TKey, TValue>:
                    result = @default;
                    return false;
                case IReadOnlyMap<TKey, TValue> map when map.TryGetKey(key, out result):
                    return true;
                case IReadOnlyMap<TKey, TValue>:
                    result = @default;
                    return false;
                default:
                    return source.ReversePairs().TryGetValue(key, @default, out result);
            }
        }
        
        public static Boolean TryGetKey<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, TValue key, Func<TKey> factory, [MaybeNullWhen(false)] out TKey result)
        {
            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            switch (source)
            {
                case IMap<TKey, TValue> map when map.TryGetKey(key, out result):
                    return true;
                case IMap<TKey, TValue>:
                    result = factory.Invoke();
                    return false;
                case IReadOnlyMap<TKey, TValue> map when map.TryGetKey(key, out result):
                    return true;
                case IReadOnlyMap<TKey, TValue>:
                    result = factory.Invoke();
                    return false;
                default:
                    return source.ReversePairs().TryGetValue(key, factory, out result);
            }
        }
        
        public static Boolean TryGetKey<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, TValue key, Func<TValue, TKey> factory, [MaybeNullWhen(false)] out TKey result)
        {
            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            switch (source)
            {
                case IMap<TKey, TValue> map when map.TryGetKey(key, out result):
                    return true;
                case IMap<TKey, TValue>:
                    result = factory.Invoke(key);
                    return false;
                case IReadOnlyMap<TKey, TValue> map when map.TryGetKey(key, out result):
                    return true;
                case IReadOnlyMap<TKey, TValue>:
                    result = factory.Invoke(key);
                    return false;
                default:
                    return source.ReversePairs().TryGetValue(key, factory, out result);
            }
        }
        
        public static TKey? TryGetKey<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, TValue key)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return TryGetKey(source!, key, default(TKey));
        }
        
        public static TKey TryGetKey<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, TValue key, TKey @default)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return TryGetKey(source, key, @default, out TKey? result) ? result : @default;
        }
        
        public static TKey TryGetKey<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, TValue key, Func<TKey> factory)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return TryGetKey(source, key, out TKey? result) ? result : factory.Invoke();
        }
        
        public static TKey TryGetKey<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, TValue key, Func<TValue, TKey> factory)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return TryGetKey(source, key, out TKey? result) ? result : factory.Invoke(key);
        }

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
    }
}