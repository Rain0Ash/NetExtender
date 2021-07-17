// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace NetExtender.Utils.Types
{
    public static class KeyValuePairUtils
    {
        // ReSharper disable once UseDeconstructionOnParameter
        public static KeyValuePair<TValue, TKey> Reverse<TKey, TValue>(this KeyValuePair<TKey, TValue> pair)
        {
            return new KeyValuePair<TValue, TKey>(pair.Value, pair.Key);
        }

        // ReSharper disable once UseDeconstructionOnParameter
        public static KeyValuePair<TKey, TKey> FlattenByKey<TKey, TValue>(this KeyValuePair<TKey, KeyValuePair<TKey, TValue>> pair)
        {
            return new KeyValuePair<TKey, TKey>(pair.Key, pair.Value.Key);
        }
        
        // ReSharper disable once UseDeconstructionOnParameter
        public static KeyValuePair<TKey, TValue> FlattenByValue<TKey, TValue>(this KeyValuePair<TKey, KeyValuePair<TKey, TValue>> pair)
        {
            return new KeyValuePair<TKey, TValue>(pair.Key, pair.Value.Value);
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

        // ReSharper disable once UseDeconstructionOnParameter
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static DictionaryEntry ToDictionaryEntry<TKey, TValue>(this KeyValuePair<TKey, TValue> pair)
        {
            return new DictionaryEntry(pair.Key!, pair.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<DictionaryEntry> ToDictionaryEntries<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Select(ToDictionaryEntry);
        }
    }
}