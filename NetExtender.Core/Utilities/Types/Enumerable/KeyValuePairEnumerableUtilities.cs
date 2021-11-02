// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using NetExtender.Types.Maps.Interfaces;

namespace NetExtender.Utilities.Types
{
    public static partial class EnumerableUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetValue<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, TKey key, [MaybeNullWhen(false)] out TValue result)
        {
            return TryGetValue(source, key, default(TValue)!, out result);
        }
        
        public static Boolean TryGetValue<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, TKey key, TValue @default, [MaybeNullWhen(false)] out TValue result)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            switch (source)
            {
                case IDictionary<TKey, TValue> dictionary when dictionary.TryGetValue(key, out result):
                    return true;
                case IDictionary<TKey, TValue>:
                    result = @default;
                    return false;
                case IReadOnlyDictionary<TKey, TValue> dictionary when dictionary.TryGetValue(key, out result):
                    return true;
                case IReadOnlyDictionary<TKey, TValue>:
                    result = @default;
                    return false;
                case IMap<TKey, TValue> map when map.TryGetValue(key, out result):
                    return true;
                case IMap<TKey, TValue>:
                    result = @default;
                    return false;
                case IReadOnlyMap<TKey, TValue> map when map.TryGetValue(key, out result):
                    return true;
                case IReadOnlyMap<TKey, TValue>:
                    result = @default;
                    return false;
                default:
                    foreach ((TKey pkey, TValue pvalue) in source)
                    {
                        if (!EqualityComparer<TKey>.Default.Equals(key, pkey))
                        {
                            continue;
                        }

                        result = pvalue;
                        return true;
                    }
                    
                    result = @default;
                    return false;
            }
        }
        
        public static Boolean TryGetValue<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, TKey key, Func<TValue> factory, [MaybeNullWhen(false)] out TValue result)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            switch (source)
            {
                case IDictionary<TKey, TValue> dictionary when dictionary.TryGetValue(key, out result):
                    return true;
                case IDictionary<TKey, TValue>:
                    result = factory.Invoke();
                    return false;
                case IReadOnlyDictionary<TKey, TValue> dictionary when dictionary.TryGetValue(key, out result):
                    return true;
                case IReadOnlyDictionary<TKey, TValue>:
                    result = factory.Invoke();
                    return false;
                case IMap<TKey, TValue> map when map.TryGetValue(key, out result):
                    return true;
                case IMap<TKey, TValue>:
                    result = factory.Invoke();
                    return false;
                case IReadOnlyMap<TKey, TValue> map when map.TryGetValue(key, out result):
                    return true;
                case IReadOnlyMap<TKey, TValue>:
                    result = factory.Invoke();
                    return false;
                default:
                    foreach ((TKey pkey, TValue pvalue) in source)
                    {
                        if (!EqualityComparer<TKey>.Default.Equals(key, pkey))
                        {
                            continue;
                        }

                        result = pvalue;
                        return true;
                    }
                    
                    result = factory.Invoke();
                    return false;
            }
        }

        public static Boolean TryGetValue<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, TKey key, Func<TKey, TValue> factory, [MaybeNullWhen(false)] out TValue result)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            switch (source)
            {
                case IDictionary<TKey, TValue> dictionary when dictionary.TryGetValue(key, out result):
                    return true;
                case IDictionary<TKey, TValue>:
                    result = factory.Invoke(key);
                    return false;
                case IReadOnlyDictionary<TKey, TValue> dictionary when dictionary.TryGetValue(key, out result):
                    return true;
                case IReadOnlyDictionary<TKey, TValue>:
                    result = factory.Invoke(key);
                    return false;
                case IMap<TKey, TValue> map when map.TryGetValue(key, out result):
                    return true;
                case IMap<TKey, TValue>:
                    result = factory.Invoke(key);
                    return false;
                case IReadOnlyMap<TKey, TValue> map when map.TryGetValue(key, out result):
                    return true;
                case IReadOnlyMap<TKey, TValue>:
                    result = factory.Invoke(key);
                    return false;
                default:
                    foreach ((TKey pkey, TValue pvalue) in source)
                    {
                        if (!EqualityComparer<TKey>.Default.Equals(key, pkey))
                        {
                            continue;
                        }

                        result = pvalue;
                        return true;
                    }
                    
                    result = factory.Invoke(key);
                    return false;
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TValue? TryGetValue<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, TKey key)
        {
            return TryGetValue(source!, key, default(TValue));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TValue TryGetValue<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, TKey key, TValue @default)
        {
            return TryGetValue(source, key, out TValue? result) ? result : @default;
        }
        
        public static TValue TryGetValue<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, TKey key, Func<TValue> factory)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return TryGetValue(source, key, out TValue? result) ? result : factory.Invoke();
        }
        
        public static TValue TryGetValue<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, TKey key, Func<TKey, TValue> factory)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return TryGetValue(source, key, out TValue? result) ? result : factory.Invoke(key);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TKey? TryGetKey<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, TValue key)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return TryGetKey(source!, key, default(TKey));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
        
        public static KeyValuePair<TKey, TValue> GetPair<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, TKey key)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            switch (source)
            {
                case IDictionary<TKey, TValue> dictionary:
                    return new KeyValuePair<TKey, TValue>(key, dictionary[key]);
                case IReadOnlyDictionary<TKey, TValue> dictionary:
                    return new KeyValuePair<TKey, TValue>(key, dictionary[key]);
                case IMap<TKey, TValue> map:
                    return new KeyValuePair<TKey, TValue>(key, map[key]);
                case IReadOnlyMap<TKey, TValue> map:
                    return new KeyValuePair<TKey, TValue>(key, map[key]);
                default:
                    foreach ((TKey pkey, TValue pvalue) in source)
                    {
                        if (!EqualityComparer<TKey>.Default.Equals(key, pkey))
                        {
                            continue;
                        }
                        
                        return new KeyValuePair<TKey, TValue>(pkey, pvalue);
                    }
                    
                    throw new KeyNotFoundException();
            }
        }

        [SuppressMessage("ReSharper", "CognitiveComplexity")]
        public static Boolean TryGetPair<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, TKey key, out KeyValuePair<TKey, TValue> pair)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            switch (source)
            {
                case IDictionary<TKey, TValue> dictionary:
                {
                    if (dictionary.TryGetValue(key, out TValue? value))
                    {
                        pair = new KeyValuePair<TKey, TValue>(key, value);
                        return true;
                    }

                    pair = default;
                    return false;
                }
                case IReadOnlyDictionary<TKey, TValue> dictionary:
                {
                    if (dictionary.TryGetValue(key, out TValue? value))
                    {
                        pair = new KeyValuePair<TKey, TValue>(key, value);
                        return true;
                    }

                    pair = default;
                    return false;
                }
                case IMap<TKey, TValue> map:
                {
                    if (map.TryGetValue(key, out TValue? value))
                    {
                        pair = new KeyValuePair<TKey, TValue>(key, value);
                        return true;
                    }

                    pair = default;
                    return false;
                }
                case IReadOnlyMap<TKey, TValue> map:
                {
                    if (map.TryGetValue(key, out TValue? value))
                    {
                        pair = new KeyValuePair<TKey, TValue>(key, value);
                        return true;
                    }

                    pair = default;
                    return false;
                }
                default:
                    foreach ((TKey pkey, TValue pvalue) in source)
                    {
                        if (!EqualityComparer<TKey>.Default.Equals(key, pkey))
                        {
                            continue;
                        }
                        
                        pair = new KeyValuePair<TKey, TValue>(pkey, pvalue);
                        return true;
                    }

                    pair = default;
                    return false;
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IGrouping<TKey, KeyValuePair<TKey, TValue>>> GroupBy<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.GroupBy(item => item.Key);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IGrouping<TKey, KeyValuePair<TKey, TValue>>> GroupByKey<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.GroupBy(item => item.Key);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IGrouping<TValue, KeyValuePair<TKey, TValue>>> GroupByValue<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.GroupBy(item => item.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IGrouping<TKey, TValue>> GroupManyByKey<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.GroupBy(item => item.Key, item => item.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<IGrouping<TValue, TKey>> GroupManyByValue<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.GroupBy(item => item.Value, item => item.Key);
        }
        
        public static IEnumerable<KeyValuePair<TValue, TKey>> ReversePairs<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Select(pair => pair.Reverse());
        }

        public static IEnumerable<TKey> Keys<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            foreach (KeyValuePair<TKey, TValue> item in source)
            {
                yield return item.Key;
            }
        }
        
        public static IEnumerable<TValue> Values<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            foreach (KeyValuePair<TKey, TValue> item in source)
            {
                yield return item.Value;
            }
        }
        
        public static IEnumerable<KeyValuePair<TKey, TValue>> ForEachKey<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, Action<TKey> action)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.ForEachBy(item => item.Key, action);
        }

        public static IEnumerable<KeyValuePair<TKey, TValue>> ForEachKeyWhere<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, Func<TKey, Boolean> where, Action<TKey> action)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.ForEachByWhere(item => item.Key, where, action);
        }
        
        public static IEnumerable<KeyValuePair<TKey, TValue>> ForEachKeyWhere<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, Func<TKey, Int32, Boolean> where, Action<TKey> action)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.ForEachByWhere(item => item.Key, where, action);
        }
        
        public static IEnumerable<KeyValuePair<TKey, TValue>> ForEachKeyWhereNot<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, Func<TKey, Boolean> where, Action<TKey> action)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.ForEachByWhereNot(item => item.Key, where, action);
        }
        
        public static IEnumerable<KeyValuePair<TKey, TValue>> ForEachKeyWhereNot<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, Func<TKey, Int32, Boolean> where, Action<TKey> action)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.ForEachByWhereNot(item => item.Key, where, action);
        }

        public static IEnumerable<KeyValuePair<TKey, TValue>> ForEachKey<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, Action<TKey, Int32> action)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.ForEachBy(item => item.Key, action);
        }

        public static IEnumerable<KeyValuePair<TKey, TValue>> ForEachKeyWhere<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, Func<TKey, Boolean> where, Action<TKey, Int32> action)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.ForEachByWhere(item => item.Key, where, action);
        }
        
        public static IEnumerable<KeyValuePair<TKey, TValue>> ForEachKeyWhere<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, Func<TKey, Int32, Boolean> where, Action<TKey, Int32> action)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.ForEachByWhere(item => item.Key, where, action);
        }

        public static IEnumerable<KeyValuePair<TKey, TValue>> ForEachKeyWhereNot<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, Func<TKey, Boolean> where, Action<TKey, Int32> action)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.ForEachByWhereNot(item => item.Key, where, action);
        }
        
        public static IEnumerable<KeyValuePair<TKey, TValue>> ForEachKeyWhereNot<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, Func<TKey, Int32, Boolean> where, Action<TKey, Int32> action)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.ForEachByWhereNot(item => item.Key, where, action);
        }
        
        public static IEnumerable<KeyValuePair<TKey, TValue>> ForEachValue<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, Action<TValue> action)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.ForEachBy(item => item.Value, action);
        }

        public static IEnumerable<KeyValuePair<TKey, TValue>> ForEachValueWhere<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, Func<TValue, Boolean> where, Action<TValue> action)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.ForEachByWhere(item => item.Value, where, action);
        }
        
        public static IEnumerable<KeyValuePair<TKey, TValue>> ForEachValueWhere<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, Func<TValue, Int32, Boolean> where, Action<TValue> action)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.ForEachByWhere(item => item.Value, where, action);
        }
        
        public static IEnumerable<KeyValuePair<TKey, TValue>> ForEachValueWhereNot<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, Func<TValue, Boolean> where, Action<TValue> action)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.ForEachByWhereNot(item => item.Value, where, action);
        }
        
        public static IEnumerable<KeyValuePair<TKey, TValue>> ForEachValueWhereNot<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, Func<TValue, Int32, Boolean> where, Action<TValue> action)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.ForEachByWhereNot(item => item.Value, where, action);
        }

        public static IEnumerable<KeyValuePair<TKey, TValue>> ForEachValue<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, Action<TValue, Int32> action)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.ForEachBy(item => item.Value, action);
        }

        public static IEnumerable<KeyValuePair<TKey, TValue>> ForEachValueWhere<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, Func<TValue, Boolean> where, Action<TValue, Int32> action)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.ForEachByWhere(item => item.Value, where, action);
        }
        
        public static IEnumerable<KeyValuePair<TKey, TValue>> ForEachValueWhere<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, Func<TValue, Int32, Boolean> where, Action<TValue, Int32> action)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.ForEachByWhere(item => item.Value, where, action);
        }

        public static IEnumerable<KeyValuePair<TKey, TValue>> ForEachValueWhereNot<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, Func<TValue, Boolean> where, Action<TValue, Int32> action)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.ForEachByWhereNot(item => item.Value, where, action);
        }
        
        public static IEnumerable<KeyValuePair<TKey, TValue>> ForEachValueWhereNot<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, Func<TValue, Int32, Boolean> where, Action<TValue, Int32> action)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.ForEachByWhereNot(item => item.Value, where, action);
        }

        public static IOrderedEnumerable<KeyValuePair<TKey, TValue>> OrderByKeys<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return OrderByKeys(source, Comparer<TKey>.Default);
        }

        public static IOrderedEnumerable<KeyValuePair<TKey, TValue>> OrderByKeys<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, IComparer<TKey>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.OrderBy(item => item.Key, comparer);
        }

        public static IOrderedEnumerable<KeyValuePair<TKey, TValue>> OrderByKeysDescending<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return OrderByKeysDescending(source, Comparer<TKey>.Default);
        }

        public static IOrderedEnumerable<KeyValuePair<TKey, TValue>> OrderByKeysDescending<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, IComparer<TKey>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.OrderByDescending(item => item.Key, comparer);
        }
        
        public static IOrderedEnumerable<KeyValuePair<TKey, TValue>> OrderByValues<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return OrderByValues(source, Comparer<TValue>.Default);
        }

        public static IOrderedEnumerable<KeyValuePair<TKey, TValue>> OrderByValues<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, IComparer<TValue>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.OrderBy(item => item.Value, comparer);
        }

        public static IOrderedEnumerable<KeyValuePair<TKey, TValue>> OrderByValuesDescending<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return OrderByValuesDescending(source, Comparer<TValue>.Default);
        }

        public static IOrderedEnumerable<KeyValuePair<TKey, TValue>> OrderByValuesDescending<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, IComparer<TValue>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.OrderByDescending(item => item.Value, comparer);
        }

        public static IEnumerable<KeyValuePair<TKey, TValue>> DistinctByKey<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return DistinctByKey(source, EqualityComparer<TKey>.Default);
        }
        
        public static IEnumerable<KeyValuePair<TKey, TValue>> DistinctByKey<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, IEqualityComparer<TKey>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.DistinctBy(item => item.Key, comparer);
        }
        
        public static IEnumerable<KeyValuePair<TKey, TValue>> DistinctByValue<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return DistinctByValue(source, EqualityComparer<TValue>.Default);
        }
        
        public static IEnumerable<KeyValuePair<TKey, TValue>> DistinctByValue<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, IEqualityComparer<TValue>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.DistinctBy(item => item.Value, comparer);
        }
        
        public static IEnumerable<(TKey key, TValue value)> ToTuple<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            foreach ((TKey key, TValue value) in source)
            {
                yield return (key, value);
            }
        }

        public static IEnumerable<KeyValuePair<TKey, TValue>> ToKeyValuePairs<TKey, TValue>(this IEnumerable<(TKey, TValue)> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            foreach ((TKey key, TValue value) in source)
            {
                yield return new KeyValuePair<TKey, TValue>(key, value);
            }
        }
        
        public static IEnumerable<KeyValuePair<TKey, TValue?>> DefaultPairs<TKey, TValue>(this IEnumerable<TKey> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            foreach (TKey key in source)
            {
                yield return new KeyValuePair<TKey, TValue?>(key, default);
            }
        }
    }
}