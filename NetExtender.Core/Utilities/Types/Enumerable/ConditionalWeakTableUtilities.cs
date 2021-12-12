// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.CompilerServices;

namespace NetExtender.Utilities.Types
{
    public static class ConditionalWeakTableUtilities
    {
        public static Boolean Contains<TKey, TValue>(this ConditionalWeakTable<TKey, TValue> collection, TKey key) where TKey : class where TValue : class?
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            return collection.TryGetValue(key, out _);
        }

        public static TValue GetOrAdd<TKey, TValue>(this ConditionalWeakTable<TKey, TValue> collection, TKey key, TValue value) where TKey : class where TValue : class?
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            return collection.GetValue(key, _ => value);
        }
        
        public static TValue GetOrAdd<TKey, TValue>(this ConditionalWeakTable<TKey, TValue> collection, TKey key, Func<TValue> factory) where TKey : class where TValue : class?
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return collection.GetValue(key, _ => factory());
        }
        
        public static TValue GetOrAdd<TKey, TValue>(this ConditionalWeakTable<TKey, TValue> collection, TKey key, Func<TKey, TValue> factory) where TKey : class where TValue : class?
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return collection.GetValue(key, callback => factory(callback));
        }
    }
}