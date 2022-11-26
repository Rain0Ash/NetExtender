// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace NetExtender.Utilities.Types
{
    public static class KeyValuePairUtilities
    {
        public static void Deconstruct(this DictionaryEntry entry, out Object key, out Object? value)
        {
            key = entry.Key;
            value = entry.Value;
        }

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