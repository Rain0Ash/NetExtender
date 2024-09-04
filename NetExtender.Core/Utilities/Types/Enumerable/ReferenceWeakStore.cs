// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using NetExtender.Types.Storages;
using NetExtender.Types.Storages.Interfaces;

namespace NetExtender.Utilities.Types
{
    public static partial class StorageUtilities
    {
        public static class Instance<TKey, TValue> where TKey : class
        {
            private static IStorage<TKey, TValue> Storage { get; } = new WeakStorageGeneric<TKey, TValue>();

            public static Boolean Contains(TKey key)
            {
                return Storage.Contains(key);
            }

            public static Boolean TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
            {
                return Storage.TryGetValue(key, out value);
            }

            public static void Add(TKey key, TValue value)
            {
                Storage.Add(key, value);
            }

            public static void Update(TKey key, TValue value)
            {
                Storage.AddOrUpdate(key, value);
            }

            public static TValue Register(TKey key, TValue value)
            {
                return Storage.GetOrAdd(key, value);
            }

            public static TValue Register(TKey key, Func<TValue> factory)
            {
                return Storage.GetOrAdd(key, factory);
            }

            public static TValue Register(TKey key, Func<TKey, TValue> factory)
            {
                return Storage.GetOrAdd(key, factory);
            }

            public static Boolean Remove(TKey key)
            {
                return Storage.Remove(key);
            }
        }
    }
}