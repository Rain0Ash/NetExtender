// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using NetExtender.Types.Immutable.Dictionaries;

namespace NetExtender.Utils.Types
{
    public static class DictionaryUtils
    {
        [Pure]
        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>([NotNull] this IEnumerable<KeyValuePair<TKey, TValue>> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return new Dictionary<TKey, TValue>(source);
        }
        
        [Pure]
        public static Boolean Contains<TKey, TValue>([NotNull] this ICollection<KeyValuePair<TKey, TValue>> source, KeyValuePair<TKey, TValue> pair)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Contains(pair);
        }

        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetValue<TKey, TValue>([NotNull] this IEnumerable<KeyValuePair<TKey, TValue>> source, TKey key, out TValue result)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return TryGetValue(source, key, out result, default(TValue));
        }
        
        [Pure]
        public static Boolean TryGetValue<TKey, TValue>([NotNull] this IEnumerable<KeyValuePair<TKey, TValue>> source, [NotNull] TKey key, out TValue result, TValue @default)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            switch (source)
            {
                case IDictionary<TKey, TValue> dict when dict.TryGetValue(key, out result):
                    return true;
                case IDictionary<TKey, TValue>:
                    result = @default;
                    return false;
                case IReadOnlyDictionary<TKey, TValue> dict when dict.TryGetValue(key, out result):
                    return true;
                case IReadOnlyDictionary<TKey, TValue>:
                    result = @default;
                    return false;
                default:
                    foreach ((TKey pkey, TValue pvalue) in source)
                    {
                        if (!EqualityComparer<TKey>.Default.Equals(key, pkey))
                        {
                            continue;
                        }

                        result = pvalue;
                        return true;
                    }
                    
                    result = @default;
                    return false;
            }
        }
        
        [Pure]
        public static Boolean TryGetValue<TKey, TValue>([NotNull] this IEnumerable<KeyValuePair<TKey, TValue>> source, [NotNull] TKey key, out TValue result, [NotNull] Func<TValue> factory)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            switch (source)
            {
                case IDictionary<TKey, TValue> dict when dict.TryGetValue(key, out result):
                    return true;
                case IDictionary<TKey, TValue>:
                    result = factory.Invoke();
                    return false;
                case IReadOnlyDictionary<TKey, TValue> dict when dict.TryGetValue(key, out result):
                    return true;
                case IReadOnlyDictionary<TKey, TValue>:
                    result = factory.Invoke();
                    return false;
                default:
                    foreach ((TKey pkey, TValue pvalue) in source)
                    {
                        if (!EqualityComparer<TKey>.Default.Equals(key, pkey))
                        {
                            continue;
                        }

                        result = pvalue;
                        return true;
                    }
                    
                    result = factory.Invoke();
                    return false;
            }
        }

        [Pure]
        public static Boolean TryGetValue<TKey, TValue>([NotNull] this IEnumerable<KeyValuePair<TKey, TValue>> source, [NotNull] TKey key, out TValue result, [NotNull] Func<TKey, TValue> factory)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            switch (source)
            {
                case IDictionary<TKey, TValue> dict when dict.TryGetValue(key, out result):
                    return true;
                case IDictionary<TKey, TValue>:
                    result = factory.Invoke(key);
                    return false;
                case IReadOnlyDictionary<TKey, TValue> dict when dict.TryGetValue(key, out result):
                    return true;
                case IReadOnlyDictionary<TKey, TValue>:
                    result = factory.Invoke(key);
                    return false;
                default:
                    foreach ((TKey pkey, TValue pvalue) in source)
                    {
                        if (!EqualityComparer<TKey>.Default.Equals(key, pkey))
                        {
                            continue;
                        }

                        result = pvalue;
                        return true;
                    }
                    
                    result = factory.Invoke(key);
                    return false;
            }
        }
        
        [Pure]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TValue TryGetValue<TKey, TValue>([NotNull] this IEnumerable<KeyValuePair<TKey, TValue>> source, [NotNull] TKey key)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return TryGetValue(source, key, default(TValue));
        }
        
        [Pure]
        public static TValue TryGetValue<TKey, TValue>([NotNull] this IEnumerable<KeyValuePair<TKey, TValue>> source, [NotNull] TKey key, TValue @default)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return TryGetValue(source, key, out TValue result) ? result : @default;
        }
        
        [Pure]
        public static TValue TryGetValue<TKey, TValue>([NotNull] this IEnumerable<KeyValuePair<TKey, TValue>> source, [NotNull] TKey key, [NotNull] Func<TValue> factory)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return TryGetValue(source, key, out TValue result) ? result : factory.Invoke();
        }
        
        [Pure]
        public static TValue TryGetValue<TKey, TValue>([NotNull] this IEnumerable<KeyValuePair<TKey, TValue>> source, [NotNull] TKey key, [NotNull] Func<TKey, TValue> factory)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            return TryGetValue(source, key, out TValue result) ? result : factory.Invoke(key);
        }

        public static void Add<TKey, TValue>([NotNull] this ICollection<KeyValuePair<TKey, TValue>> source, KeyValuePair<TKey, TValue> pair)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            source.Add(pair);
        }

        public static TValue GetOrAdd<TKey, TValue>([NotNull] this IDictionary<TKey, TValue> dictionary, [NotNull] TKey key, TValue value)
        {
            if (dictionary is null)
            {
                throw new ArgumentNullException(nameof(dictionary));
            }

            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (dictionary.TryGetValue(key, out TValue val))
            {
                return val;
            }

            dictionary.Add(key, value);
            return value;
        }

        public static TValue GetOrAdd<TKey, TValue>([NotNull] this IDictionary<TKey, TValue> dictionary, [NotNull] TKey key, [NotNull] Func<TValue> factory)
        {
            if (dictionary is null)
            {
                throw new ArgumentNullException(nameof(dictionary));
            }

            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            if (dictionary.TryGetValue(key, out TValue value))
            {
                return value;
            }

            value = factory.Invoke();
            dictionary.Add(key, value);
            return value;
        }
        
        public static TValue GetOrAdd<TKey, TValue>([NotNull] this IDictionary<TKey, TValue> dictionary, [NotNull] TKey key, [NotNull] Func<TKey, TValue> factory)
        {
            if (dictionary is null)
            {
                throw new ArgumentNullException(nameof(dictionary));
            }

            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            if (dictionary.TryGetValue(key, out TValue value))
            {
                return value;
            }

            value = factory.Invoke(key);
            dictionary.Add(key, value);
            return value;
        }
        
        public static Boolean Remove<TKey, TValue>([NotNull] this ICollection<KeyValuePair<TKey, TValue>> source, KeyValuePair<TKey, TValue> pair)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Remove(pair);
        }
        
        public static Boolean IsReadOnly<TKey, TValue>([NotNull] this ICollection<KeyValuePair<TKey, TValue>> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.IsReadOnly;
        }

        [Pure]
        public static KeyValuePair<TKey, TValue> GetPair<TKey, TValue>([NotNull] this IEnumerable<KeyValuePair<TKey, TValue>> source, [NotNull] TKey key)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            switch (source)
            {
                case IDictionary<TKey, TValue> dict:
                    return new KeyValuePair<TKey, TValue>(key, dict[key]);
                case IReadOnlyDictionary<TKey, TValue> dict:
                    return new KeyValuePair<TKey, TValue>(key, dict[key]);
                default:
                    foreach ((TKey pkey, TValue pvalue) in source)
                    {
                        if (!EqualityComparer<TKey>.Default.Equals(key, pkey))
                        {
                            continue;
                        }
                        
                        return new KeyValuePair<TKey, TValue>(pkey, pvalue);
                    }
                    
                    throw new KeyNotFoundException();
            }
        }

        [Pure]
        public static Boolean TryGetPair<TKey, TValue>([NotNull] this IEnumerable<KeyValuePair<TKey, TValue>> source, [NotNull] TKey key,
            out KeyValuePair<TKey, TValue> pair)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            switch (source)
            {
                case IDictionary<TKey, TValue> dict:
                {
                    if (dict.TryGetValue(key, out TValue value))
                    {
                        pair = new KeyValuePair<TKey, TValue>(key, value);
                        return true;
                    }

                    pair = default;
                    return false;
                }
                case IReadOnlyDictionary<TKey, TValue> dict:
                {
                    if (dict.TryGetValue(key, out TValue value))
                    {
                        pair = new KeyValuePair<TKey, TValue>(key, value);
                        return true;
                    }

                    pair = default;
                    return false;
                }
                default:
                    foreach ((TKey pkey, TValue pvalue) in source)
                    {
                        if (!EqualityComparer<TKey>.Default.Equals(key, pkey))
                        {
                            continue;
                        }
                        
                        pair = new KeyValuePair<TKey, TValue>(pkey, pvalue);
                        return true;
                    }

                    pair = default;
                    return false;
            }
        }

        public static IDictionary<TValue, TKey> Reverse<TKey, TValue>([NotNull] this IDictionary<TKey, TValue> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            Dictionary<TValue, TKey> dictionary = new Dictionary<TValue, TKey>();

            foreach ((TKey key, TValue value) in source)
            {
                if (!dictionary.ContainsKey(value))
                {
                    dictionary.Add(value, key);
                }
            }

            return dictionary;
        }

        public static Dictionary<TKey, TValue> Clone<TKey, TValue>([NotNull] this Dictionary<TKey, TValue> source) where TValue : ICloneable
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>(source.Count, source.Comparer);

            foreach ((TKey key, TValue value) in source)
            {
                dictionary.Add(key, (TValue) value.Clone());
            }

            return dictionary;
        }

        public static void CopyTo<TKey, TValue>([NotNull] this ICollection<KeyValuePair<TKey, TValue>> source, [NotNull] KeyValuePair<TKey, TValue>[] array, Int32 index)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            source.CopyTo(array, index);
        }

        public static ImmutableMultiDictionary<TKey, TValue> ToImmutableMultiDictionary<TKey, TValue>([NotNull] this IEnumerable<KeyValuePair<TKey, ImmutableHashSet<TValue>>> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            return ImmutableMultiDictionary<TKey, TValue>.Empty.AddRange(source);
        } 
    }
}