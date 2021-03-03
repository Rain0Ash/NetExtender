// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace NetExtender.Utils.Types
{
    public static class KeyValuePairUtils
    {
        // ReSharper disable once UseDeconstructionOnParameter
        public static KeyValuePair<TValue, TKey> Reverse<TKey, TValue>(this KeyValuePair<TKey, TValue> pair)
        {
            return new KeyValuePair<TValue, TKey>(pair.Value, pair.Key);
        }

        public static IEnumerable<KeyValuePair<TValue, TKey>> ReversePairs<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source)
        {
            return source.Select(pair => pair.Reverse());
        }

        public static IEnumerable<TKey> Keys<TKey, TValue>([NotNull] this IEnumerable<KeyValuePair<TKey, TValue>> source)
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
        
        public static IEnumerable<TValue> Values<TKey, TValue>([NotNull] this IEnumerable<KeyValuePair<TKey, TValue>> source)
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
        
        public static IEnumerable<(TKey key, TValue value)> Tuple<TKey, TValue>([NotNull] this IEnumerable<KeyValuePair<TKey, TValue>> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            // ReSharper disable once UseDeconstruction
            foreach (KeyValuePair<TKey, TValue> item in source)
            {
                yield return (item.Key, item.Value);
            }
        }
        
        public static IEnumerable<KeyValuePair<TKey, TValue>> DefaultPairs<TKey, TValue>([NotNull] this IEnumerable<TKey> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            foreach (TKey key in source)
            {
                yield return new KeyValuePair<TKey, TValue>(key, default);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        // ReSharper disable once UseDeconstructionOnParameter
        public static DictionaryEntry ToDictionaryEntry<TKey, TValue>(this KeyValuePair<TKey, TValue> pair)
        {
            return new DictionaryEntry(pair.Key, pair.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<DictionaryEntry> ToDictionaryEntries<TKey, TValue>([NotNull] this IEnumerable<KeyValuePair<TKey, TValue>> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Select(ToDictionaryEntry);
        }
    }
}