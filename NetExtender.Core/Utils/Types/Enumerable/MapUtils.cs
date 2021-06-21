// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using NetExtender.Types.Maps;
using NetExtender.Types.Maps.Interfaces;

namespace NetExtender.Utils.Types
{
    public static class MapUtils
    {
        public static Boolean TryGetKey<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, TValue key, [MaybeNullWhen(false)] out TKey result)
        {
            return TryGetKey(source!, key, default(TKey), out result);
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
            return TryGetKey(source!, key, default(TKey));
        }
        
        public static TKey TryGetKey<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, TValue key, TKey @default)
        {
            return TryGetKey(source, key, @default, out TKey? result) ? result : @default;
        }
        
        public static TKey TryGetKey<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, TValue key, Func<TKey> factory)
        {
            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return TryGetKey(source, key, out TKey? result) ? result : factory.Invoke();
        }
        
        public static TKey TryGetKey<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, TValue key, Func<TValue, TKey> factory)
        {
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
        public static Map<TKey, TValue> ToMap<T, TKey, TValue>(this IEnumerable<T> source, Func<T, TKey> keySelector, Func<T, TValue> selector) where TKey : notnull where TValue : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return new Map<TKey, TValue>(source.ToDictionary(keySelector, selector));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Map<TKey, TValue> ToMap<T, TKey, TValue>(this IEnumerable<T> source, Func<T, TKey> keySelector, Func<T, TValue> valueSelector, IEqualityComparer<TKey>? comparer) where TKey : notnull where TValue : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return new Map<TKey, TValue>(source.ToDictionary(keySelector, valueSelector, comparer));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Map<TKey, TValue> ToMap<T, TKey, TValue>(this IEnumerable<T> source, Func<T, TKey> keySelector, Func<T, TValue> valueSelector,
            IEqualityComparer<TKey>? keyComparer, IEqualityComparer<TValue>? valueComparer) where TKey : notnull where TValue : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return new Map<TKey, TValue>(source.ToDictionary(keySelector, valueSelector, keyComparer), keyComparer, valueComparer);
        }
    }
}