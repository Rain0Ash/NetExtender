// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Collections.Generic;
using System.Linq;

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
    }
}