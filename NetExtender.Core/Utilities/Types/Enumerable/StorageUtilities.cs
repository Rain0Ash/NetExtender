// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;

namespace NetExtender.Utilities.Types
{
    public static partial class StorageUtilities
    {
        public static class Instance
        {
            public static Boolean Contains<TKey, TValue>(TKey key) where TKey : class
            {
                return Instance<TKey, TValue>.Contains(key);
            }

            public static Boolean TryGetValue<TKey, TValue>(TKey key, [MaybeNullWhen(false)] out TValue value) where TKey : class
            {
                return Instance<TKey, TValue>.TryGetValue(key, out value);
            }

            public static void Add<TKey, TValue>(TKey key, TValue value) where TKey : class
            {
                Instance<TKey, TValue>.Add(key, value);
            }

            public static void Update<TKey, TValue>(TKey key, TValue value) where TKey : class
            {
                Instance<TKey, TValue>.Update(key, value);
            }

            public static TValue Register<TKey, TValue>(TKey key, TValue value) where TKey : class
            {
                return Instance<TKey, TValue>.Register(key, value);
            }

            public static TValue Register<TKey, TValue>(TKey key, Func<TValue> factory) where TKey : class
            {
                return Instance<TKey, TValue>.Register(key, factory);
            }

            public static TValue Register<TKey, TValue>(TKey key, Func<TKey, TValue> factory) where TKey : class
            {
                return Instance<TKey, TValue>.Register(key, factory);
            }

            public static Boolean Remove<TKey, TValue>(TKey key) where TKey : class
            {
                return Instance<TKey, TValue>.Remove(key);
            }
        }
    }
}