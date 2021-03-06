// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Collections.Generic;

namespace NetExtender.Utils.Types
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("ReSharper", "UseDeconstructionOnParameter")]
    public static class TupleUtils
    {
        public static IEnumerable<T> AsEnumerable<T>(this (T, T) value)
        {
            yield return value.Item1;
            yield return value.Item2;
        }
        
        public static IEnumerable<T> AsEnumerable<T>(this (T, T, T) value)
        {
            yield return value.Item1;
            yield return value.Item2;
            yield return value.Item3;
        }
        
        public static IEnumerable<T> AsEnumerable<T>(this (T, T, T, T) value)
        {
            yield return value.Item1;
            yield return value.Item2;
            yield return value.Item3;
            yield return value.Item4;
        }
        
        public static IEnumerable<T> AsEnumerable<T>(this (T, T, T, T, T) value)
        {
            yield return value.Item1;
            yield return value.Item2;
            yield return value.Item3;
            yield return value.Item4;
            yield return value.Item5;
        }

        public static KeyValuePair<TKey, TValue> ToPair<TKey, TValue>(this (TKey, TValue) value)
        {
            return new KeyValuePair<TKey, TValue>(value.Item1, value.Item2);
        }
    }
}