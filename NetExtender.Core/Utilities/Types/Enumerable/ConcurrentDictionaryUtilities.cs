// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading;

namespace NetExtender.Utilities.Types
{
    public static class ConcurrentDictionaryUtilities
    {
        public static Boolean TryGetOrAdd<TKey, TValue>(this ConcurrentDictionary<TKey, TValue> dictionary, TKey key, TValue value, [MaybeNullWhen(false)] out TValue result) where TKey : notnull
        {
            if (dictionary is null)
            {
                throw new ArgumentNullException(nameof(dictionary));
            }

            try
            {
                result = dictionary.GetOrAdd(key, value);
                return true;
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
        }
        
        public static Boolean TryGetOrAdd<TKey, TValue>(this ConcurrentDictionary<TKey, TValue> dictionary, TKey key, Func<TValue> factory, [MaybeNullWhen(false)] out TValue result) where TKey : notnull
        {
            if (dictionary is null)
            {
                throw new ArgumentNullException(nameof(dictionary));
            }

            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            try
            {
                result = dictionary.GetOrAdd(key, factory);
                return true;
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
        }
        
        public static Boolean TryGetOrAdd<TKey, TValue>(this ConcurrentDictionary<TKey, TValue> dictionary, TKey key, Func<TKey, TValue> factory, [MaybeNullWhen(false)] out TValue result) where TKey : notnull
        {
            if (dictionary is null)
            {
                throw new ArgumentNullException(nameof(dictionary));
            }

            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            try
            {
                result = dictionary.GetOrAdd(key, factory);
                return true;
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
        }
        
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

        public static Boolean TryGetWeakValue<TKey, TValue>(this ConcurrentDictionary<TKey, WeakReference<TValue>> dictionary, TKey key, [MaybeNullWhen(false)] out TValue result) where TKey : notnull where TValue : class
        {
            if (dictionary is null)
            {
                throw new ArgumentNullException(nameof(dictionary));
            }
            
            if (!dictionary.TryGetValue(key, out WeakReference<TValue>? reference))
            {
                result = null;
                return false;
            }

            if (reference.TryGetTarget(out result))
            {
                return true;
            }

            dictionary.TryRemove(key, reference);
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryRemove<TKey, TValue>(this ConcurrentDictionary<TKey, TValue> dictionary, TKey key) where TKey : notnull
        {
            if (dictionary is null)
            {
                throw new ArgumentNullException(nameof(dictionary));
            }

            return dictionary.TryRemove(key, out _);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryRemove<TKey, TValue>(this ConcurrentDictionary<TKey, TValue> dictionary, TKey key, TValue value) where TKey : notnull
        {
            if (dictionary is null)
            {
                throw new ArgumentNullException(nameof(dictionary));
            }

            return dictionary.TryRemove(new KeyValuePair<TKey, TValue>(key, value));
        }

        public static Boolean TryRemove<TKey, TValue>(this ConcurrentDictionary<TKey, TValue> dictionary, TKey key, [MaybeNullWhen(false)] ref TValue value) where TKey : notnull
        {
            if (dictionary is null)
            {
                throw new ArgumentNullException(nameof(dictionary));
            }

            if (TryRemove(dictionary, key, value))
            {
                return true;
            }

            dictionary.TryGetValue(key, out value);
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryWeakRemove<TKey, TValue>(this ConcurrentDictionary<TKey, WeakReference<TValue>> dictionary, TKey key) where TKey : notnull where TValue : class
        {
            return TryWeakRemove(dictionary, key, out _);
        }

        public static Boolean TryWeakRemove<TKey, TValue>(this ConcurrentDictionary<TKey, WeakReference<TValue>> dictionary, TKey key, [MaybeNullWhen(false)] out TValue result) where TKey : notnull where TValue : class
        {
            if (dictionary is null)
            {
                throw new ArgumentNullException(nameof(dictionary));
            }

            if (!dictionary.TryGetValue(key, out WeakReference<TValue>? reference))
            {
                result = null;
                return false;
            }

            if (!reference.TryGetTarget(out result))
            {
                dictionary.TryRemove(key, reference);
                return false;
            }

            if (dictionary.TryRemove(key, reference))
            {
                return true;
            }

            result = null;
            return false;
        }
    }
}