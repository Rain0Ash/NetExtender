// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using NetExtender.Types.Dictionaries;
using NetExtender.Types.Immutable.Dictionaries;

namespace NetExtender.Utilities.Types
{
    public static class DictionaryUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return new Dictionary<TKey, TValue>(source);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, IEqualityComparer<TKey>? comparer) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return new Dictionary<TKey, TValue>(source, comparer);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<(TKey, TValue)> source) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.ToKeyValuePairs().ToDictionary();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<(TKey, TValue)> source, IEqualityComparer<TKey>? comparer) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.ToKeyValuePairs().ToDictionary(comparer);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SortedDictionary<TKey, TValue> ToSortedDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source) where TKey : notnull
        {
            return ToSortedDictionary(source, (IComparer<TKey>?) null);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SortedDictionary<TKey, TValue> ToSortedDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, IEqualityComparer<TKey>? equality) where TKey : notnull
        {
            return ToSortedDictionary(source, equality, null);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SortedDictionary<TKey, TValue> ToSortedDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, IComparer<TKey>? comparer) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            SortedDictionary<TKey, TValue> sorted = new SortedDictionary<TKey, TValue>(comparer);
            sorted.AddRange(source);
            
            return sorted;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SortedDictionary<TKey, TValue> ToSortedDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, IEqualityComparer<TKey>? equality, IComparer<TKey>? comparer) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return new SortedDictionary<TKey, TValue>(source.ToDictionary(equality), comparer);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SortedDictionary<TKey, TValue> ToSortedDictionary<TKey, TValue>(this IEnumerable<(TKey, TValue)> source) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.ToKeyValuePairs().ToSortedDictionary();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SortedDictionary<TKey, TValue> ToSortedDictionary<TKey, TValue>(this IEnumerable<(TKey, TValue)> source, IEqualityComparer<TKey>? equality) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.ToKeyValuePairs().ToSortedDictionary(equality);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SortedDictionary<TKey, TValue> ToSortedDictionary<TKey, TValue>(this IEnumerable<(TKey, TValue)> source, IComparer<TKey>? comparer) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.ToKeyValuePairs().ToSortedDictionary(comparer);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SortedDictionary<TKey, TValue> ToSortedDictionary<TKey, TValue>(this IEnumerable<(TKey, TValue)> source, IEqualityComparer<TKey>? equality, IComparer<TKey>? comparer) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.ToKeyValuePairs().ToSortedDictionary(equality, comparer);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SortedDictionary<TKey, TSource> ToSortedDictionary<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (keySelector is null)
            {
                throw new ArgumentNullException(nameof(keySelector));
            }

            return new SortedDictionary<TKey, TSource>(source.ToDictionary(keySelector));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SortedDictionary<TKey, TSource> ToSortedDictionary<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey>? equality) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (keySelector is null)
            {
                throw new ArgumentNullException(nameof(keySelector));
            }

            return new SortedDictionary<TKey, TSource>(source.ToDictionary(keySelector, equality));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SortedDictionary<TKey, TSource> ToSortedDictionary<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey>? comparer) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (keySelector is null)
            {
                throw new ArgumentNullException(nameof(keySelector));
            }

            return new SortedDictionary<TKey, TSource>(source.ToDictionary(keySelector), comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SortedDictionary<TKey, TSource> ToSortedDictionary<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector,
            IEqualityComparer<TKey>? equality, IComparer<TKey>? comparer) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (keySelector is null)
            {
                throw new ArgumentNullException(nameof(keySelector));
            }

            return new SortedDictionary<TKey, TSource>(source.ToDictionary(keySelector, equality), comparer);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SortedDictionary<TKey, TElement> ToSortedDictionary<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector,
            Func<TSource, TElement> elementSelector) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (keySelector is null)
            {
                throw new ArgumentNullException(nameof(keySelector));
            }

            if (elementSelector is null)
            {
                throw new ArgumentNullException(nameof(elementSelector));
            }

            return new SortedDictionary<TKey, TElement>(source.ToDictionary(keySelector, elementSelector));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SortedDictionary<TKey, TElement> ToSortedDictionary<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector,
            Func<TSource, TElement> elementSelector, IEqualityComparer<TKey>? equality) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (keySelector is null)
            {
                throw new ArgumentNullException(nameof(keySelector));
            }

            if (elementSelector is null)
            {
                throw new ArgumentNullException(nameof(elementSelector));
            }

            return new SortedDictionary<TKey, TElement>(source.ToDictionary(keySelector, elementSelector, equality));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SortedDictionary<TKey, TElement> ToSortedDictionary<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector,
            Func<TSource, TElement> elementSelector, IComparer<TKey>? comparer) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (keySelector is null)
            {
                throw new ArgumentNullException(nameof(keySelector));
            }

            if (elementSelector is null)
            {
                throw new ArgumentNullException(nameof(elementSelector));
            }

            return new SortedDictionary<TKey, TElement>(source.ToDictionary(keySelector, elementSelector), comparer);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SortedDictionary<TKey, TElement> ToSortedDictionary<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector,
            Func<TSource, TElement> elementSelector, IEqualityComparer<TKey>? equality, IComparer<TKey>? comparer) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (keySelector is null)
            {
                throw new ArgumentNullException(nameof(keySelector));
            }

            if (elementSelector is null)
            {
                throw new ArgumentNullException(nameof(elementSelector));
            }

            return new SortedDictionary<TKey, TElement>(source.ToDictionary(keySelector, elementSelector, equality), comparer);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexDictionary<TKey, TValue> ToIndexDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source) where TKey : notnull
        {
            return ToIndexDictionary(source, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexDictionary<TKey, TValue> ToIndexDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, IEqualityComparer<TKey>? comparer) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return new IndexDictionary<TKey, TValue>(source, comparer);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexDictionary<TKey, TValue> ToIndexDictionary<TKey, TValue>(this IEnumerable<(TKey, TValue)> source) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.ToKeyValuePairs().ToIndexDictionary();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexDictionary<TKey, TValue> ToIndexDictionary<TKey, TValue>(this IEnumerable<(TKey, TValue)> source, IEqualityComparer<TKey>? comparer) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.ToKeyValuePairs().ToIndexDictionary(comparer);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexDictionary<TKey, TSource> ToIndexDictionary<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (keySelector is null)
            {
                throw new ArgumentNullException(nameof(keySelector));
            }

            return new IndexDictionary<TKey, TSource>(source.Select(item => new KeyValuePair<TKey, TSource>(keySelector(item), item)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexDictionary<TKey, TSource> ToIndexDictionary<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey>? comparer) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (keySelector is null)
            {
                throw new ArgumentNullException(nameof(keySelector));
            }

            return new IndexDictionary<TKey, TSource>(source.Select(item => new KeyValuePair<TKey, TSource>(keySelector(item), item)), comparer);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexDictionary<TKey, TElement> ToIndexDictionary<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector,
            Func<TSource, TElement> elementSelector) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (keySelector is null)
            {
                throw new ArgumentNullException(nameof(keySelector));
            }

            if (elementSelector is null)
            {
                throw new ArgumentNullException(nameof(elementSelector));
            }

            return new IndexDictionary<TKey, TElement>(source.Select(item => new KeyValuePair<TKey, TElement>(keySelector(item), elementSelector(item))));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IndexDictionary<TKey, TElement> ToIndexDictionary<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector,
            Func<TSource, TElement> elementSelector, IEqualityComparer<TKey>? comparer) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (keySelector is null)
            {
                throw new ArgumentNullException(nameof(keySelector));
            }

            if (elementSelector is null)
            {
                throw new ArgumentNullException(nameof(elementSelector));
            }

            return new IndexDictionary<TKey, TElement>(source.Select(item => new KeyValuePair<TKey, TElement>(keySelector(item), elementSelector(item))), comparer);
        }
        
        public static Boolean Contains<TKey, TValue>(this ICollection<KeyValuePair<TKey, TValue>> source, KeyValuePair<TKey, TValue> pair)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Contains(pair);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGetValue<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, TKey key, [MaybeNullWhen(false)] out TValue result)
        {
            return TryGetValue(source!, key, default(TValue), out result);
        }
        
        public static Boolean TryGetValue<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, TKey key, TValue @default, [MaybeNullWhen(false)] out TValue result)
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
        
        public static Boolean TryGetValue<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, TKey key, Func<TValue> factory, [MaybeNullWhen(false)] out TValue result)
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

        public static Boolean TryGetValue<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, TKey key, Func<TKey, TValue> factory, [MaybeNullWhen(false)] out TValue? result)
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
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TValue? TryGetValue<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, TKey key)
        {
            return TryGetValue(source!, key, default(TValue));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TValue TryGetValue<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, TKey key, TValue @default)
        {
            return TryGetValue(source, key, out TValue? result) ? result : @default;
        }
        
        public static TValue TryGetValue<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, TKey key, Func<TValue> factory)
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

            return TryGetValue(source, key, out TValue? result) ? result : factory.Invoke();
        }
        
        public static TValue TryGetValue<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, TKey key, Func<TKey, TValue> factory)
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

            return TryGetValue(source, key, out TValue? result) ? result : factory.Invoke(key);
        }

        public static void Add<TKey, TValue>(this ICollection<KeyValuePair<TKey, TValue>> source, KeyValuePair<TKey, TValue> pair)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            source.Add(pair);
        }

        public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (dictionary is null)
            {
                throw new ArgumentNullException(nameof(dictionary));
            }

            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (dictionary.TryGetValue(key, out TValue? result))
            {
                return result;
            }

            dictionary.Add(key, value);
            return value;
        }

        public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TValue> factory)
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

            if (dictionary.TryGetValue(key, out TValue? value))
            {
                return value;
            }

            value = factory.Invoke();
            dictionary.Add(key, value);
            return value;
        }
        
        public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TKey, TValue> factory)
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

            if (dictionary.TryGetValue(key, out TValue? value))
            {
                return value;
            }

            value = factory.Invoke(key);
            dictionary.Add(key, value);
            return value;
        }
        
        public static Boolean Remove<TKey, TValue>(this ICollection<KeyValuePair<TKey, TValue>> source, KeyValuePair<TKey, TValue> pair)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Remove(pair);
        }
        
        public static Boolean IsReadOnly<TKey, TValue>(this ICollection<KeyValuePair<TKey, TValue>> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.IsReadOnly;
        }

        public static KeyValuePair<TKey, TValue> GetPair<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, TKey key)
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

        [SuppressMessage("ReSharper", "CognitiveComplexity")]
        public static Boolean TryGetPair<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, TKey key, out KeyValuePair<TKey, TValue> pair)
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
                    if (dict.TryGetValue(key, out TValue? value))
                    {
                        pair = new KeyValuePair<TKey, TValue>(key, value);
                        return true;
                    }

                    pair = default;
                    return false;
                }
                case IReadOnlyDictionary<TKey, TValue> dict:
                {
                    if (dict.TryGetValue(key, out TValue? value))
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

        public static IDictionary<TValue, TKey> Reverse<TKey, TValue>(this IDictionary<TKey, TValue> source) where TKey : notnull where TValue : notnull
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

        public static Dictionary<TKey, TValue> Clone<TKey, TValue>(this Dictionary<TKey, TValue> source) where TKey : notnull where TValue : ICloneable
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

        public static void CopyTo<TKey, TValue>(this ICollection<KeyValuePair<TKey, TValue>> source, KeyValuePair<TKey, TValue>[] array, Int32 index)
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

        public static ImmutableMultiDictionary<TKey, TValue> ToImmutableMultiDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, ImmutableHashSet<TValue>>> source) where TKey : notnull
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            return ImmutableMultiDictionary<TKey, TValue>.Empty.AddRange(source);
        } 
    }
}