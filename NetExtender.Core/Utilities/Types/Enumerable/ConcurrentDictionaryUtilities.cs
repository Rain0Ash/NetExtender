// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Concurrent;
using System.Threading;

namespace NetExtender.Utilities.Types
{
    public static class ConcurrentDictionaryUtilities
    {
        public static TValue ConcurrentGetOrAdd<TKey, TValue>(this ConcurrentDictionary<TKey, TValue> dictionary, TKey key, Func<TValue> factory) where TKey : notnull
        {
            if (dictionary is null)
            {
                throw new ArgumentNullException(nameof(dictionary));
            }

            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            Lazy<TValue> lazy = new Lazy<TValue>(factory, LazyThreadSafetyMode.ExecutionAndPublication);
            return dictionary.GetOrAdd(key, () => lazy.Value);
        }
        
        public static TValue ConcurrentGetOrAdd<TKey, TValue>(this ConcurrentDictionary<TKey, TValue> dictionary, TKey key, Func<TKey, TValue> factory) where TKey : notnull
        {
            if (dictionary is null)
            {
                throw new ArgumentNullException(nameof(dictionary));
            }

            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }
            
            Lazy<TValue> lazy = new Lazy<TValue>(() => factory(key), LazyThreadSafetyMode.ExecutionAndPublication);
            return dictionary.GetOrAdd(key, () => lazy.Value);
        }
    }
}