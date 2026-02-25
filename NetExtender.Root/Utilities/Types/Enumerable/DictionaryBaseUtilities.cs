using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace NetExtender.Utilities.Types
{
    internal static class DictionaryBaseUtilities
    {
        [SuppressMessage("ReSharper", "NullCoalescingConditionIsAlwaysNotNullAccordingToAPIContract")]
        internal static Dictionary<Object, Object?> ToDictionary(IDictionary source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            Dictionary<Object, Object?> result = new Dictionary<Object, Object?>(source.Count);

            foreach (Object? key in source.Keys)
            {
                Object? value = source[key];
                result[key ?? ObjectUtilities.Null] = value;
            }

            return result;
        }

        internal static TValue GetOrAdd<TKey, TValue>(IDictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (dictionary is null)
            {
                throw new ArgumentNullException(nameof(dictionary));
            }

            if (dictionary.TryGetValue(key, out TValue? result))
            {
                return result;
            }

            dictionary.Add(key, value);
            return value;
        }

        internal static TValue GetOrAdd<TKey, TValue>(IDictionary<TKey, TValue> dictionary, TKey key, Func<TValue> factory)
        {
            if (dictionary is null)
            {
                throw new ArgumentNullException(nameof(dictionary));
            }

            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            if (dictionary.TryGetValue(key, out TValue? value))
            {
                return value;
            }

            value = factory.Invoke();
            dictionary.Add(key, value);
            return value;
        }

        internal static TValue GetOrAdd<TKey, TValue>(IDictionary<TKey, TValue> dictionary, TKey key, Func<TKey, TValue> factory)
        {
            if (dictionary is null)
            {
                throw new ArgumentNullException(nameof(dictionary));
            }

            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            if (dictionary.TryGetValue(key, out TValue? value))
            {
                return value;
            }

            value = factory.Invoke(key);
            dictionary.Add(key, value);
            return value;
        }

        internal static TValue GetOrNew<TKey, TValue>(Dictionary<TKey, TValue> dictionary, TKey key) where TKey : notnull where TValue : new()
        {
            if (dictionary is null)
            {
                throw new ArgumentNullException(nameof(dictionary));
            }

            return dictionary.TryGetValue(key, out TValue? value) ? value : dictionary[key] = new TValue();
        }
    }
}