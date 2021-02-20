// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using NetExtender.Types.Maps;
using NetExtender.Types.Maps.Interfaces;

namespace NetExtender.Utils.Types
{
    public static class MapUtils
    {
        [Pure]
        public static Boolean TryGetKey<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, TValue key, out TKey result)
        {
            return TryGetKey(source, key, out result, default(TKey));
        }
        
        [Pure]
        public static Boolean TryGetKey<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, TValue key, out TKey result, TKey @default)
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
                    return source.ReversePairs().TryGetValue(key, out result, @default);
            }
        }
        
        [Pure]
        public static Boolean TryGetKey<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, TValue key, out TKey result, Func<TKey> factory)
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
                    return source.ReversePairs().TryGetValue(key, out result, factory);
            }
        }
        
        [Pure]
        public static Boolean TryGetKey<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, TValue key, out TKey result, Func<TValue, TKey> factory)
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
                    return source.ReversePairs().TryGetValue(key, out result, factory);
            }
        }
        
        [Pure]
        public static TKey TryGetKey<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, TValue key)
        {
            return TryGetKey(source, key, default(TKey));
        }
        
        [Pure]
        public static TKey TryGetKey<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, TValue key, TKey @default)
        {
            return TryGetKey(source, key, out TKey result, @default) ? result : @default;
        }
        
        [Pure]
        public static TKey TryGetKey<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, TValue key, Func<TKey> factory)
        {
            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return TryGetKey(source, key, out TKey result) ? result : factory.Invoke();
        }
        
        [Pure]
        public static TKey TryGetKey<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, TValue key, Func<TValue, TKey> factory)
        {
            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return TryGetKey(source, key, out TKey result) ? result : factory.Invoke(key);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Map<TKey, TValue> ToMap<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return new Map<TKey, TValue>(source);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Map<TKey, TValue> ToMap<T, TKey, TValue>(this IEnumerable<T> source, Func<T, TKey> keySelector, Func<T, TValue> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return new Map<TKey, TValue>(source.ToDictionary(keySelector, selector));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Map<TKey, TValue> ToMap<T, TKey, TValue>(this IEnumerable<T> source, Func<T, TKey> keySelector, Func<T, TValue> valueSelector, IEqualityComparer<TKey> comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return new Map<TKey, TValue>(source.ToDictionary(keySelector, valueSelector, comparer));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Map<TKey, TValue> ToMap<T, TKey, TValue>(this IEnumerable<T> source, Func<T, TKey> keySelector, Func<T, TValue> valueSelector,
            IEqualityComparer<TKey> keyComparer, IEqualityComparer<TValue> valueComparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return new Map<TKey, TValue>(source.ToDictionary(keySelector, valueSelector, keyComparer), keyComparer, valueComparer);
        }
    }
}