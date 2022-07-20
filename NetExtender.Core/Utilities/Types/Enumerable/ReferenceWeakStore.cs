// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using NetExtender.Types.Stores;
using NetExtender.Types.Stores.Interfaces;

namespace NetExtender.Utilities.Types
{
    public static partial class StoreUtilities
    {
        public static class Instance<TKey, TValue> where TKey : class
        {
            private static IStore<TKey, TValue> Store { get; } = new WeakStoreGeneric<TKey, TValue>();

            public static Boolean Contains(TKey key)
            {
                return Store.Contains(key);
            }

            public static Boolean TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
            {
                return Store.TryGetValue(key, out value);
            }

            public static void Add(TKey key, TValue value)
            {
                Store.Add(key, value);
            }

            public static void Update(TKey key, TValue value)
            {
                Store.AddOrUpdate(key, value);
            }

            public static TValue Register(TKey key, TValue value)
            {
                return Store.GetOrAdd(key, value);
            }

            public static TValue Register(TKey key, Func<TValue> factory)
            {
                return Store.GetOrAdd(key, factory);
            }

            public static TValue Register(TKey key, Func<TKey, TValue> factory)
            {
                return Store.GetOrAdd(key, factory);
            }

            public static Boolean Remove(TKey key)
            {
                return Store.Remove(key);
            }
        }
    }
}