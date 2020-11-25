// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using NetExtender.Utils.Types;

// ReSharper disable PossibleMultipleEnumeration

namespace NetExtender.Types.Dictionaries
{
    /// <summary>
    ///     Provides a read-only dictionary that contents are fixed at the time of instance creation.
    /// </summary>
    /// <typeparam name="TKey">The type of keys in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of values in the dictionary.</typeparam>
    public sealed class FrozenDictionary<TKey, TValue> : IReadOnlyDictionary<TKey, TValue>
    {
        /// <summary>
        ///     Creates a <see cref="FrozenDictionary{TKey, TValue}" /> from an <see cref="IEnumerable{T}" /> according to a
        ///     specified key selector function.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static FrozenDictionary<TKey, TValue> Create(IEnumerable<TValue> source, Func<TValue, TKey> selector)
        {
            return Create(source, selector, PassThrough);
        }

        /// <summary>
        ///     Creates a <see cref="FrozenDictionary{TKey, TValue}" /> from an <see cref="IEnumerable{T}" /> according to
        ///     specified key selector and value selector functions.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <param name="valueSelector"></param>
        /// <returns></returns>
        public static FrozenDictionary<TKey, TValue> Create<TSource>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TValue> valueSelector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (keySelector is null)
            {
                throw new ArgumentNullException(nameof(keySelector));
            }

            if (valueSelector is null)
            {
                throw new ArgumentNullException(nameof(valueSelector));
            }

            const Int32 initialSize = 4;
            const Single loadFactor = 0.75f;
            Int32 size = source.CountIfMaterialized() ?? initialSize;
            Int32 bucketSize = CalculateCapacity(size, loadFactor);
            FrozenDictionary<TKey, TValue> result = new FrozenDictionary<TKey, TValue>(bucketSize, loadFactor);

            foreach (TSource x in source)
            {
                TKey key = keySelector(x);
                TValue value = valueSelector(x);
                if (!result.TryAddInternal(key, value, out _))
                {
                    throw new ArgumentException($"Key was already exists. Key:{key}");
                }
            }

            return result;
        }
        
        private static readonly Func<TValue, TValue> PassThrough = x => x;
        private readonly Single _factor;

        private Entry[] _buckets;

        /// <summary>
        ///     Gets an enumerable collection that contains the keys in the read-only dictionary.
        /// </summary>
        public IEnumerable<TKey> Keys
        {
            get
            {
                return _buckets.Select(entry => entry.Key);
            }
        }

        /// <summary>
        ///     Gets an enumerable collection that contains the values in the read-only dictionary.
        /// </summary>
        public IEnumerable<TValue> Values
        {
            get
            {
                return _buckets.Select(entry => entry.Value);
            }
        }

        /// <summary>
        ///     Gets the number of elements in the collection.
        /// </summary>
        public Int32 Count { get; private set; }
        
        /// <summary>
        ///     Creates instance.
        /// </summary>
        /// <param name="size"></param>
        /// <param name="factor"></param>
        private FrozenDictionary(Int32 size, Single factor)
        {
            _buckets = size == 0 ? Array.Empty<Entry>() : new Entry[size];
            _factor = factor;
        }

        /// <summary>
        ///     Determines whether the read-only dictionary contains an element that has the specified key.
        /// </summary>
        /// <param name="key">The key to locate.</param>
        /// <returns>
        ///     true if the read-only dictionary contains an element that has the specified key; otherwise, false.
        /// </returns>
        public Boolean ContainsKey(TKey key)
        {
            return TryGetValue(key, out _);
        }

        /// <summary>
        ///     Gets the value that is associated with the specified key.
        /// </summary>
        /// <param name="key">The key to locate.</param>
        /// <param name="value">
        ///     When this method returns, the value associated with the specified key, if the key is found;
        ///     otherwise, the default value for the type of the value parameter.
        ///     This parameter is passed uninitialized.
        /// </param>
        /// <returns>
        ///     true if the object that implements the <see cref="IReadOnlyDictionary{TKey, TValue}" /> interface contains an
        ///     element that has the specified key; otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean TryGetValue(TKey key, out TValue value)
        {
            Int32 hash = EqualityComparer<TKey>.Default.GetHashCode(key);
            Int32 index = hash & (_buckets.Length - 1);
            Entry next = _buckets[index];
            while (next is not null)
            {
                if (EqualityComparer<TKey>.Default.Equals(next.Key, key))
                {
                    value = next.Value;
                    return true;
                }

                next = next.Next;
            }

            value = default;
            return false;
        }
        
        /// <summary>
        ///     Gets the element that has the specified key in the read-only dictionary or the default value of the element type.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="default"></param>
        /// <returns></returns>
        public TValue GetValueOrDefault(TKey key, TValue @default = default)
        {
            return TryGetValue(key, out TValue value) ? value : @default;
        }

        /// <summary>
        ///     Add element.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        private Boolean TryAddInternal(TKey key, TValue value, out TValue result)
        {
            Int32 nextCapacity = CalculateCapacity(Count + 1, _factor);
            if (_buckets.Length < nextCapacity)
            {
                //--- rehash
                Entry[] nextBucket = new Entry[nextCapacity];
                // ReSharper disable once ForCanBeConvertedToForeach
                for (Int32 i = 0; i < _buckets.Length; i++)
                {
                    Entry entry = _buckets[i];
                    while (entry is not null)
                    {
                        Entry newEntry = new Entry(entry.Key, entry.Value, entry.Hash);
                        AddToBuckets(nextBucket, key, newEntry, default, out _);
                        entry = entry.Next;
                    }
                }

                Boolean success = AddToBuckets(nextBucket, key, null, value, out result);
                _buckets = nextBucket;
                if (success)
                {
                    Count++;
                }

                return success;
            }
            else
            {
                Boolean success = AddToBuckets(_buckets, key, null, value, out result);
                if (success)
                {
                    Count++;
                }

                return success;
            }

            //--- please pass 'key + newEntry' or 'key + value'.
            static Boolean AddToBuckets(Entry[] buckets, TKey newKey, Entry newEntry, TValue value, out TValue result)
            {
                Int32 hash = newEntry?.Hash ?? EqualityComparer<TKey>.Default.GetHashCode(newKey);
                Int32 index = hash & (buckets.Length - 1);
                if (buckets[index] is null)
                {
                    if (newEntry is null)
                    {
                        result = value;
                        buckets[index] = new Entry(newKey, result, hash);
                    }
                    else
                    {
                        result = newEntry.Value;
                        buckets[index] = newEntry;
                    }
                }
                else
                {
                    Entry lastEntry = buckets[index];
                    while (true)
                    {
                        if (EqualityComparer<TKey>.Default.Equals(lastEntry.Key, newKey))
                        {
                            result = lastEntry.Value;
                            return false;
                        }

                        if (lastEntry.Next is null)
                        {
                            if (newEntry is null)
                            {
                                result = value;
                                lastEntry.Next = new Entry(newKey, result, hash);
                            }
                            else
                            {
                                result = newEntry.Value;
                                lastEntry.Next = newEntry;
                            }

                            break;
                        }

                        lastEntry = lastEntry.Next;
                    }
                }

                return true;
            }
        }

        /// <summary>
        ///     Calculates bucket capacity.
        /// </summary>
        /// <param name="size"></param>
        /// <param name="factor"></param>
        /// <returns></returns>
        private static Int32 CalculateCapacity(Int32 size, Single factor)
        {
            Int32 initialCapacity = (Int32) (size / factor);
            Int32 capacity = 1;
            while (capacity < initialCapacity)
            {
                capacity <<= 1;
            }

            return capacity > 8 ? capacity : 8;
        }

        /// <summary>
        ///     Gets the element that has the specified key in the read-only dictionary.
        /// </summary>
        /// <param name="key">The key to locate.</param>
        /// <returns>The element that has the specified key in the read-only dictionary.</returns>
        public TValue this[TKey key]
        {
            get
            {
                return TryGetValue(key, out TValue value) ? value : throw new KeyNotFoundException();
            }
        }
        
        /// <summary>
        ///     Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _buckets.Select(entry => new KeyValuePair<TKey, TValue>(entry.Key, entry.Value)).GetEnumerator();
        }
        
        /// <summary>
        ///     Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>An <see cref="IEnumerator" /> object that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        ///     Represents <see cref="FrozenDictionary{TKey, TValue}" /> entry.
        /// </summary>
        private class Entry
        {
            public readonly Int32 Hash;
            public readonly TKey Key;
            public readonly TValue Value;
            public Entry Next;

            public Entry(TKey key, TValue value, Int32 hash)
            {
                Key = key;
                Value = value;
                Hash = hash;
            }
        }
    }

    /// <summary>
    ///     Provides a read-only dictionary that contents are fixed at the time of instance creation.
    /// </summary>
    /// <typeparam name="TValue">The type of values in the dictionary.</typeparam>
    public sealed class FrozenStringKeyDictionary<TValue> : IReadOnlyDictionary<String, TValue>
    {
        /// <summary>
        ///     Creates a <see cref="FrozenStringKeyDictionary{TValue}" /> from an <see cref="IEnumerable{T}" /> according to a
        ///     specified key selector function.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static FrozenStringKeyDictionary<TValue> Create(IEnumerable<TValue> source, Func<TValue, String> selector)
        {
            return Create(source, selector, PassThrough);
        }

        /// <summary>
        ///     Creates a <see cref="FrozenStringKeyDictionary{TValue}" /> from an <see cref="IEnumerable{T}" /> according to
        ///     specified key selector and value selector functions.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <param name="valueSelector"></param>
        /// <returns></returns>
        public static FrozenStringKeyDictionary<TValue> Create<TSource>(IEnumerable<TSource> source, Func<TSource, String> keySelector, Func<TSource, TValue> valueSelector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (keySelector is null)
            {
                throw new ArgumentNullException(nameof(keySelector));
            }

            if (valueSelector is null)
            {
                throw new ArgumentNullException(nameof(valueSelector));
            }

            const Int32 initialSize = 4;
            const Single loadFactor = 0.75f;
            Int32 size = source.CountIfMaterialized() ?? initialSize;
            Int32 bucketSize = CalculateCapacity(size, loadFactor);
            FrozenStringKeyDictionary<TValue> result = new FrozenStringKeyDictionary<TValue>(bucketSize, loadFactor);

            foreach (TSource x in source)
            {
                String key = keySelector(x);
                TValue value = valueSelector(x);
                if (!result.TryAddInternal(key, value, out _))
                {
                    throw new ArgumentException($"Key was already exists. Key:{key}");
                }
            }

            return result;
        }
        
        private static readonly Func<TValue, TValue> PassThrough = x => x;
        private readonly Single _factor;

        private Entry[] _buckets;

        /// <summary>
        ///     Gets an enumerable collection that contains the keys in the read-only dictionary.
        /// </summary>
        public IEnumerable<String> Keys
        {
            get
            {
                return _buckets.Select(entry => entry.Key);
            }
        }

        /// <summary>
        ///     Gets an enumerable collection that contains the values in the read-only dictionary.
        /// </summary>
        public IEnumerable<TValue> Values
        {
            get
            {
                return _buckets.Select(entry => entry.Value);
            }
        }

        /// <summary>
        ///     Gets the number of elements in the collection.
        /// </summary>
        public Int32 Count { get; private set; }
        
        /// <summary>
        ///     Creates instance.
        /// </summary>
        /// <param name="size"></param>
        /// <param name="factor"></param>
        private FrozenStringKeyDictionary(Int32 size, Single factor)
        {
            _buckets = size == 0 ? Array.Empty<Entry>() : new Entry[size];
            _factor = factor;
        }

        /// <summary>
        ///     Determines whether the read-only dictionary contains an element that has the specified key.
        /// </summary>
        /// <param name="key">The key to locate.</param>
        /// <returns>
        ///     true if the read-only dictionary contains an element that has the specified key; otherwise, false.
        /// </returns>
        public Boolean ContainsKey(String key)
        {
            return TryGetValue(key, out _);
        }

        /// <summary>
        ///     Gets the value that is associated with the specified key.
        /// </summary>
        /// <param name="key">The key to locate.</param>
        /// <param name="value">
        ///     When this method returns, the value associated with the specified key, if the key is found;
        ///     otherwise, the default value for the type of the value parameter.
        ///     This parameter is passed uninitialized.
        /// </param>
        /// <returns>
        ///     true if the object that implements the <see cref="IReadOnlyDictionary{TKey, TValue}" /> interface contains an
        ///     element that has the specified key; otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean TryGetValue(String key, out TValue value)
        {
            Int32 hash = key.GetHashCode();
            Int32 index = hash & (_buckets.Length - 1);
            Entry next = _buckets[index];
            while (next is not null)
            {
                if (next.Key == key)
                {
                    value = next.Value;
                    return true;
                }

                next = next.Next;
            }

            value = default;
            return false;
        }
        
        /// <summary>
        ///     Gets the element that has the specified key in the read-only dictionary or the default value of the element type.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="default"></param>
        /// <returns></returns>
        public TValue GetValueOrDefault(String key, TValue @default = default)
        {
            return TryGetValue(key, out TValue value) ? value : @default;
        }

        /// <summary>
        ///     Add element.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        private Boolean TryAddInternal(String key, TValue value, out TValue result)
        {
            Int32 nextCapacity = CalculateCapacity(Count + 1, _factor);
            if (_buckets.Length < nextCapacity)
            {
                //--- rehash
                Entry[] nextBucket = new Entry[nextCapacity];
                // ReSharper disable once ForCanBeConvertedToForeach
                for (Int32 i = 0; i < _buckets.Length; i++)
                {
                    Entry entry = _buckets[i];
                    while (entry is not null)
                    {
                        Entry newEntry = new Entry(entry.Key, entry.Value, entry.Hash);
                        AddToBuckets(nextBucket, key, newEntry, default, out _);
                        entry = entry.Next;
                    }
                }

                Boolean success = AddToBuckets(nextBucket, key, null, value, out result);
                _buckets = nextBucket;
                if (success)
                {
                    Count++;
                }

                return success;
            }
            else
            {
                Boolean success = AddToBuckets(_buckets, key, null, value, out result);
                if (success)
                {
                    Count++;
                }

                return success;
            }

            //--- please pass 'key + newEntry' or 'key + value'.
            static Boolean AddToBuckets(Entry[] buckets, String newKey, Entry newEntry, TValue value, out TValue result)
            {
                Int32 hash = newEntry?.Hash ?? newKey.GetHashCode();
                Int32 index = hash & (buckets.Length - 1);
                if (buckets[index] is null)
                {
                    if (newEntry is null)
                    {
                        result = value;
                        buckets[index] = new Entry(newKey, result, hash);
                    }
                    else
                    {
                        result = newEntry.Value;
                        buckets[index] = newEntry;
                    }
                }
                else
                {
                    Entry lastEntry = buckets[index];
                    while (true)
                    {
                        if (lastEntry.Key == newKey)
                        {
                            result = lastEntry.Value;
                            return false;
                        }

                        if (lastEntry.Next is null)
                        {
                            if (newEntry is null)
                            {
                                result = value;
                                lastEntry.Next = new Entry(newKey, result, hash);
                            }
                            else
                            {
                                result = newEntry.Value;
                                lastEntry.Next = newEntry;
                            }

                            break;
                        }

                        lastEntry = lastEntry.Next;
                    }
                }

                return true;
            }
        }

        /// <summary>
        ///     Calculates bucket capacity.
        /// </summary>
        /// <param name="size"></param>
        /// <param name="factor"></param>
        /// <returns></returns>
        private static Int32 CalculateCapacity(Int32 size, Single factor)
        {
            Int32 initialCapacity = (Int32) (size / factor);
            Int32 capacity = 1;
            while (capacity < initialCapacity)
            {
                capacity <<= 1;
            }

            return capacity > 8 ? capacity : 8;
        }

        /// <summary>
        ///     Gets the element that has the specified key in the read-only dictionary.
        /// </summary>
        /// <param name="key">The key to locate.</param>
        /// <returns>The element that has the specified key in the read-only dictionary.</returns>
        public TValue this[String key]
        {
            get
            {
                return TryGetValue(key, out TValue value) ? value : throw new KeyNotFoundException();
            }
        }
        
        /// <summary>
        ///     Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator<KeyValuePair<String, TValue>> GetEnumerator()
        {
            return _buckets.Select(entry => new KeyValuePair<String, TValue>(entry.Key, entry.Value)).GetEnumerator();
        }

        /// <summary>
        ///     Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>An <see cref="IEnumerator" /> object that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        ///     Represents <see cref="FrozenStringKeyDictionary{TValue}" /> entry.
        /// </summary>
        private class Entry
        {
            public readonly Int32 Hash;
            public readonly String Key;
            public readonly TValue Value;
            public Entry Next;

            public Entry(String key, TValue value, Int32 hash)
            {
                Key = key;
                Value = value;
                Hash = hash;
            }
        }
    }

    /// <summary>
    ///     Provides a read-only dictionary that contents are fixed at the time of instance creation.
    /// </summary>
    /// <typeparam name="TValue">The type of values in the dictionary.</typeparam>
    public sealed class FrozenSByteKeyDictionary<TValue> : IReadOnlyDictionary<SByte, TValue>
    {
        /// <summary>
        ///     Creates a <see cref="FrozenSByteKeyDictionary{TValue}" /> from an <see cref="IEnumerable{T}" /> according to a
        ///     specified key selector function.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static FrozenSByteKeyDictionary<TValue> Create(IEnumerable<TValue> source, Func<TValue, SByte> selector)
        {
            return Create(source, selector, PassThrough);
        }

        /// <summary>
        ///     Creates a <see cref="FrozenSByteKeyDictionary{TValue}" /> from an <see cref="IEnumerable{T}" /> according to
        ///     specified key selector and value selector functions.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <param name="valueSelector"></param>
        /// <returns></returns>
        public static FrozenSByteKeyDictionary<TValue> Create<TSource>(IEnumerable<TSource> source, Func<TSource, SByte> keySelector, Func<TSource, TValue> valueSelector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (keySelector is null)
            {
                throw new ArgumentNullException(nameof(keySelector));
            }

            if (valueSelector is null)
            {
                throw new ArgumentNullException(nameof(valueSelector));
            }

            const Int32 initialSize = 4;
            const Single loadFactor = 0.75f;
            Int32 size = source.CountIfMaterialized() ?? initialSize;
            Int32 bucketSize = CalculateCapacity(size, loadFactor);
            FrozenSByteKeyDictionary<TValue> result = new FrozenSByteKeyDictionary<TValue>(bucketSize, loadFactor);

            foreach (TSource x in source)
            {
                SByte key = keySelector(x);
                TValue value = valueSelector(x);
                if (!result.TryAddInternal(key, value, out _))
                {
                    throw new ArgumentException($"Key was already exists. Key:{key}");
                }
            }

            return result;
        }
        
        private static readonly Func<TValue, TValue> PassThrough = x => x;
        private readonly Single _factor;

        private Entry[] _buckets;

        /// <summary>
        ///     Gets an enumerable collection that contains the keys in the read-only dictionary.
        /// </summary>
        public IEnumerable<SByte> Keys
        {
            get
            {
                return _buckets.Select(entry => entry.Key);
            }
        }

        /// <summary>
        ///     Gets an enumerable collection that contains the values in the read-only dictionary.
        /// </summary>
        public IEnumerable<TValue> Values
        {
            get
            {
                return _buckets.Select(entry => entry.Value);
            }
        }

        /// <summary>
        ///     Gets the number of elements in the collection.
        /// </summary>
        public Int32 Count { get; private set; }
        
        /// <summary>
        ///     Creates instance.
        /// </summary>
        /// <param name="size"></param>
        /// <param name="factor"></param>
        private FrozenSByteKeyDictionary(Int32 size, Single factor)
        {
            _buckets = size == 0 ? Array.Empty<Entry>() : new Entry[size];
            _factor = factor;
        }

        /// <summary>
        ///     Determines whether the read-only dictionary contains an element that has the specified key.
        /// </summary>
        /// <param name="key">The key to locate.</param>
        /// <returns>
        ///     true if the read-only dictionary contains an element that has the specified key; otherwise, false.
        /// </returns>
        public Boolean ContainsKey(SByte key)
        {
            return TryGetValue(key, out _);
        }

        /// <summary>
        ///     Gets the value that is associated with the specified key.
        /// </summary>
        /// <param name="key">The key to locate.</param>
        /// <param name="value">
        ///     When this method returns, the value associated with the specified key, if the key is found;
        ///     otherwise, the default value for the type of the value parameter.
        ///     This parameter is passed uninitialized.
        /// </param>
        /// <returns>
        ///     true if the object that implements the <see cref="IReadOnlyDictionary{TKey, TValue}" /> interface contains an
        ///     element that has the specified key; otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean TryGetValue(SByte key, out TValue value)
        {
            Int32 hash = key.GetHashCode();
            Int32 index = hash & (_buckets.Length - 1);
            Entry next = _buckets[index];
            while (next is not null)
            {
                if (next.Key == key)
                {
                    value = next.Value;
                    return true;
                }

                next = next.Next;
            }

            value = default;
            return false;
        }
        
        /// <summary>
        ///     Gets the element that has the specified key in the read-only dictionary or the default value of the element type.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="default"></param>
        /// <returns></returns>
        public TValue GetValueOrDefault(SByte key, TValue @default = default)
        {
            return TryGetValue(key, out TValue value) ? value : @default;
        }

        /// <summary>
        ///     Add element.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        private Boolean TryAddInternal(SByte key, TValue value, out TValue result)
        {
            Int32 nextCapacity = CalculateCapacity(Count + 1, _factor);
            if (_buckets.Length < nextCapacity)
            {
                //--- rehash
                Entry[] nextBucket = new Entry[nextCapacity];
                // ReSharper disable once ForCanBeConvertedToForeach
                for (Int32 i = 0; i < _buckets.Length; i++)
                {
                    Entry e = _buckets[i];
                    while (e is not null)
                    {
                        Entry newEntry = new Entry(e.Key, e.Value, e.Hash);
                        AddToBuckets(nextBucket, key, newEntry, default, out _);
                        e = e.Next;
                    }
                }

                Boolean success = AddToBuckets(nextBucket, key, null, value, out result);
                _buckets = nextBucket;
                if (success)
                {
                    Count++;
                }

                return success;
            }
            else
            {
                Boolean success = AddToBuckets(_buckets, key, null, value, out result);
                if (success)
                {
                    Count++;
                }

                return success;
            }
            
            //--- please pass 'key + newEntry' or 'key + value'.
            static Boolean AddToBuckets(Entry[] buckets, SByte newKey, Entry newEntry, TValue value, out TValue result)
            {
                Int32 hash = newEntry?.Hash ?? newKey.GetHashCode();
                Int32 index = hash & (buckets.Length - 1);
                if (buckets[index] is null)
                {
                    if (newEntry is null)
                    {
                        result = value;
                        buckets[index] = new Entry(newKey, result, hash);
                    }
                    else
                    {
                        result = newEntry.Value;
                        buckets[index] = newEntry;
                    }
                }
                else
                {
                    Entry lastEntry = buckets[index];
                    while (true)
                    {
                        if (lastEntry.Key == newKey)
                        {
                            result = lastEntry.Value;
                            return false;
                        }

                        if (lastEntry.Next is null)
                        {
                            if (newEntry is null)
                            {
                                result = value;
                                lastEntry.Next = new Entry(newKey, result, hash);
                            }
                            else
                            {
                                result = newEntry.Value;
                                lastEntry.Next = newEntry;
                            }

                            break;
                        }

                        lastEntry = lastEntry.Next;
                    }
                }

                return true;
            }
        }

        /// <summary>
        ///     Calculates bucket capacity.
        /// </summary>
        /// <param name="size"></param>
        /// <param name="factor"></param>
        /// <returns></returns>
        private static Int32 CalculateCapacity(Int32 size, Single factor)
        {
            Int32 initialCapacity = (Int32) (size / factor);
            Int32 capacity = 1;
            while (capacity < initialCapacity)
            {
                capacity <<= 1;
            }

            return capacity > 8 ? capacity : 8;
        }

        /// <summary>
        ///     Gets the element that has the specified key in the read-only dictionary.
        /// </summary>
        /// <param name="key">The key to locate.</param>
        /// <returns>The element that has the specified key in the read-only dictionary.</returns>
        public TValue this[SByte key]
        {
            get
            {
                return TryGetValue(key, out TValue value) ? value : throw new KeyNotFoundException();
            }
        }

        /// <summary>
        ///     Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator<KeyValuePair<SByte, TValue>> GetEnumerator()
        {
            return _buckets.Select(entry => new KeyValuePair<SByte, TValue>(entry.Key, entry.Value)).GetEnumerator();
        }

        /// <summary>
        ///     Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>An <see cref="IEnumerator" /> object that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        ///     Represents <see cref="FrozenSByteKeyDictionary{TValue}" /> entry.
        /// </summary>
        private class Entry
        {
            public readonly Int32 Hash;
            public readonly SByte Key;
            public readonly TValue Value;
            public Entry Next;

            public Entry(SByte key, TValue value, Int32 hash)
            {
                Key = key;
                Value = value;
                Hash = hash;
            }
        }
    }
    
    /// <summary>
    ///     Provides a read-only dictionary that contents are fixed at the time of instance creation.
    /// </summary>
    /// <typeparam name="TValue">The type of values in the dictionary.</typeparam>
    public sealed class FrozenByteKeyDictionary<TValue> : IReadOnlyDictionary<Byte, TValue>
    {
        /// <summary>
        ///     Creates a <see cref="FrozenByteKeyDictionary{TValue}" /> from an <see cref="IEnumerable{T}" /> according to a
        ///     specified key selector function.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static FrozenByteKeyDictionary<TValue> Create(IEnumerable<TValue> source, Func<TValue, Byte> selector)
        {
            return Create(source, selector, PassThrough);
        }

        /// <summary>
        ///     Creates a <see cref="FrozenByteKeyDictionary{TValue}" /> from an <see cref="IEnumerable{T}" /> according to
        ///     specified key selector and value selector functions.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <param name="valueSelector"></param>
        /// <returns></returns>
        public static FrozenByteKeyDictionary<TValue> Create<TSource>(IEnumerable<TSource> source, Func<TSource, Byte> keySelector, Func<TSource, TValue> valueSelector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (keySelector is null)
            {
                throw new ArgumentNullException(nameof(keySelector));
            }

            if (valueSelector is null)
            {
                throw new ArgumentNullException(nameof(valueSelector));
            }

            const Int32 initialSize = 4;
            const Single loadFactor = 0.75f;
            Int32 size = source.CountIfMaterialized() ?? initialSize;
            Int32 bucketSize = CalculateCapacity(size, loadFactor);
            FrozenByteKeyDictionary<TValue> result = new FrozenByteKeyDictionary<TValue>(bucketSize, loadFactor);

            foreach (TSource x in source)
            {
                Byte key = keySelector(x);
                TValue value = valueSelector(x);
                if (!result.TryAddInternal(key, value, out _))
                {
                    throw new ArgumentException($"Key was already exists. Key:{key}");
                }
            }

            return result;
        }
        
        private static readonly Func<TValue, TValue> PassThrough = x => x;
        private readonly Single _factor;
        
        private Entry[] _buckets;
        
        /// <summary>
        ///     Gets an enumerable collection that contains the keys in the read-only dictionary.
        /// </summary>
        public IEnumerable<Byte> Keys
        {
            get
            {
                return _buckets.Select(entry => entry.Key);
            }
        }

        /// <summary>
        ///     Gets an enumerable collection that contains the values in the read-only dictionary.
        /// </summary>
        public IEnumerable<TValue> Values
        {
            get
            {
                return _buckets.Select(entry => entry.Value);
            }
        }

        /// <summary>
        ///     Gets the number of elements in the collection.
        /// </summary>
        public Int32 Count { get; private set; }

        /// <summary>
        ///     Creates instance.
        /// </summary>
        /// <param name="size"></param>
        /// <param name="factor"></param>
        private FrozenByteKeyDictionary(Int32 size, Single factor)
        {
            _buckets = size == 0 ? Array.Empty<Entry>() : new Entry[size];
            _factor = factor;
        }

        /// <summary>
        ///     Determines whether the read-only dictionary contains an element that has the specified key.
        /// </summary>
        /// <param name="key">The key to locate.</param>
        /// <returns>
        ///     true if the read-only dictionary contains an element that has the specified key; otherwise, false.
        /// </returns>
        public Boolean ContainsKey(Byte key)
        {
            return TryGetValue(key, out _);
        }

        /// <summary>
        ///     Gets the value that is associated with the specified key.
        /// </summary>
        /// <param name="key">The key to locate.</param>
        /// <param name="value">
        ///     When this method returns, the value associated with the specified key, if the key is found;
        ///     otherwise, the default value for the type of the value parameter.
        ///     This parameter is passed uninitialized.
        /// </param>
        /// <returns>
        ///     true if the object that implements the <see cref="IReadOnlyDictionary{TKey, TValue}" /> interface contains an
        ///     element that has the specified key; otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean TryGetValue(Byte key, out TValue value)
        {
            Int32 hash = key.GetHashCode();
            Int32 index = hash & (_buckets.Length - 1);
            Entry next = _buckets[index];
            while (next is not null)
            {
                if (next.Key == key)
                {
                    value = next.Value;
                    return true;
                }

                next = next.Next;
            }

            value = default;
            return false;
        }
        
        /// <summary>
        ///     Gets the element that has the specified key in the read-only dictionary or the default value of the element type.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="default"></param>
        /// <returns></returns>
        public TValue GetValueOrDefault(Byte key, TValue @default = default)
        {
            return TryGetValue(key, out TValue value) ? value : @default;
        }

        /// <summary>
        ///     Add element.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        private Boolean TryAddInternal(Byte key, TValue value, out TValue result)
        {
            Int32 nextCapacity = CalculateCapacity(Count + 1, _factor);
            if (_buckets.Length < nextCapacity)
            {
                //--- rehash
                Entry[] nextBucket = new Entry[nextCapacity];
                // ReSharper disable once ForCanBeConvertedToForeach
                for (Int32 i = 0; i < _buckets.Length; i++)
                {
                    Entry e = _buckets[i];
                    while (e is not null)
                    {
                        Entry newEntry = new Entry(e.Key, e.Value, e.Hash);
                        AddToBuckets(nextBucket, key, newEntry, default, out _);
                        e = e.Next;
                    }
                }

                Boolean success = AddToBuckets(nextBucket, key, null, value, out result);
                _buckets = nextBucket;
                if (success)
                {
                    Count++;
                }

                return success;
            }
            else
            {
                Boolean success = AddToBuckets(_buckets, key, null, value, out result);
                if (success)
                {
                    Count++;
                }

                return success;
            }
            
            //--- please pass 'key + newEntry' or 'key + value'.
            static Boolean AddToBuckets(Entry[] buckets, Byte newKey, Entry newEntry, TValue value, out TValue result)
            {
                Int32 hash = newEntry?.Hash ?? newKey.GetHashCode();
                Int32 index = hash & (buckets.Length - 1);
                if (buckets[index] is null)
                {
                    if (newEntry is null)
                    {
                        result = value;
                        buckets[index] = new Entry(newKey, result, hash);
                    }
                    else
                    {
                        result = newEntry.Value;
                        buckets[index] = newEntry;
                    }
                }
                else
                {
                    Entry lastEntry = buckets[index];
                    while (true)
                    {
                        if (lastEntry.Key == newKey)
                        {
                            result = lastEntry.Value;
                            return false;
                        }

                        if (lastEntry.Next is null)
                        {
                            if (newEntry is null)
                            {
                                result = value;
                                lastEntry.Next = new Entry(newKey, result, hash);
                            }
                            else
                            {
                                result = newEntry.Value;
                                lastEntry.Next = newEntry;
                            }

                            break;
                        }

                        lastEntry = lastEntry.Next;
                    }
                }

                return true;
            }
        }

        /// <summary>
        ///     Calculates bucket capacity.
        /// </summary>
        /// <param name="size"></param>
        /// <param name="factor"></param>
        /// <returns></returns>
        private static Int32 CalculateCapacity(Int32 size, Single factor)
        {
            Int32 initialCapacity = (Int32) (size / factor);
            Int32 capacity = 1;
            while (capacity < initialCapacity)
            {
                capacity <<= 1;
            }

            return capacity > 8 ? capacity : 8;
        }

        /// <summary>
        ///     Gets the element that has the specified key in the read-only dictionary.
        /// </summary>
        /// <param name="key">The key to locate.</param>
        /// <returns>The element that has the specified key in the read-only dictionary.</returns>
        public TValue this[Byte key]
        {
            get
            {
                return TryGetValue(key, out TValue value) ? value : throw new KeyNotFoundException();
            }
        }

        /// <summary>
        ///     Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator<KeyValuePair<Byte, TValue>> GetEnumerator()
        {
            return _buckets.Select(entry => new KeyValuePair<Byte, TValue>(entry.Key, entry.Value)).GetEnumerator();
        }

        /// <summary>
        ///     Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>An <see cref="IEnumerator" /> object that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        ///     Represents <see cref="FrozenByteKeyDictionary{TValue}" /> entry.
        /// </summary>
        private class Entry
        {
            public readonly Int32 Hash;
            public readonly Byte Key;
            public readonly TValue Value;
            public Entry Next;

            public Entry(Byte key, TValue value, Int32 hash)
            {
                Key = key;
                Value = value;
                Hash = hash;
            }
        }
    }
    
    /// <summary>
    ///     Provides a read-only dictionary that contents are fixed at the time of instance creation.
    /// </summary>
    /// <typeparam name="TValue">The type of values in the dictionary.</typeparam>
    public sealed class FrozenInt16KeyDictionary<TValue> : IReadOnlyDictionary<Int16, TValue>
    {
        /// <summary>
        ///     Creates a <see cref="FrozenInt16KeyDictionary{TValue}" /> from an <see cref="IEnumerable{T}" /> according to a
        ///     specified key selector function.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static FrozenInt16KeyDictionary<TValue> Create(IEnumerable<TValue> source, Func<TValue, Int16> selector)
        {
            return Create(source, selector, PassThrough);
        }

        /// <summary>
        ///     Creates a <see cref="FrozenInt16KeyDictionary{TValue}" /> from an <see cref="IEnumerable{T}" /> according to
        ///     specified key selector and value selector functions.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <param name="valueSelector"></param>
        /// <returns></returns>
        public static FrozenInt16KeyDictionary<TValue> Create<TSource>(IEnumerable<TSource> source, Func<TSource, Int16> keySelector, Func<TSource, TValue> valueSelector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (keySelector is null)
            {
                throw new ArgumentNullException(nameof(keySelector));
            }

            if (valueSelector is null)
            {
                throw new ArgumentNullException(nameof(valueSelector));
            }

            const Int32 initialSize = 4;
            const Single loadFactor = 0.75f;
            Int32 size = source.CountIfMaterialized() ?? initialSize;
            Int32 bucketSize = CalculateCapacity(size, loadFactor);
            FrozenInt16KeyDictionary<TValue> result = new FrozenInt16KeyDictionary<TValue>(bucketSize, loadFactor);

            foreach (TSource x in source)
            {
                Int16 key = keySelector(x);
                TValue value = valueSelector(x);
                if (!result.TryAddInternal(key, value, out _))
                {
                    throw new ArgumentException($"Key was already exists. Key:{key}");
                }
            }

            return result;
        }
        
        private static readonly Func<TValue, TValue> PassThrough = x => x;
        private readonly Single _factor;
        
        private Entry[] _buckets;

        /// <summary>
        ///     Gets an enumerable collection that contains the keys in the read-only dictionary.
        /// </summary>
        public IEnumerable<Int16> Keys
        {
            get
            {
                return _buckets.Select(entry => entry.Key);
            }
        }

        /// <summary>
        ///     Gets an enumerable collection that contains the values in the read-only dictionary.
        /// </summary>
        public IEnumerable<TValue> Values
        {
            get
            {
                return _buckets.Select(entry => entry.Value);
            }
        }

        /// <summary>
        ///     Gets the number of elements in the collection.
        /// </summary>
        public Int32 Count { get; private set; }
        
        /// <summary>
        ///     Creates instance.
        /// </summary>
        /// <param name="size"></param>
        /// <param name="factor"></param>
        private FrozenInt16KeyDictionary(Int32 size, Single factor)
        {
            _buckets = size == 0 ? Array.Empty<Entry>() : new Entry[size];
            _factor = factor;
        }

        /// <summary>
        ///     Determines whether the read-only dictionary contains an element that has the specified key.
        /// </summary>
        /// <param name="key">The key to locate.</param>
        /// <returns>
        ///     true if the read-only dictionary contains an element that has the specified key; otherwise, false.
        /// </returns>
        public Boolean ContainsKey(Int16 key)
        {
            return TryGetValue(key, out _);
        }

        /// <summary>
        ///     Gets the value that is associated with the specified key.
        /// </summary>
        /// <param name="key">The key to locate.</param>
        /// <param name="value">
        ///     When this method returns, the value associated with the specified key, if the key is found;
        ///     otherwise, the default value for the type of the value parameter.
        ///     This parameter is passed uninitialized.
        /// </param>
        /// <returns>
        ///     true if the object that implements the <see cref="IReadOnlyDictionary{TKey, TValue}" /> interface contains an
        ///     element that has the specified key; otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean TryGetValue(Int16 key, out TValue value)
        {
            Int32 hash = key.GetHashCode();
            Int32 index = hash & (_buckets.Length - 1);
            Entry next = _buckets[index];
            while (next is not null)
            {
                if (next.Key == key)
                {
                    value = next.Value;
                    return true;
                }

                next = next.Next;
            }

            value = default;
            return false;
        }
        
        /// <summary>
        ///     Gets the element that has the specified key in the read-only dictionary or the default value of the element type.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="default"></param>
        /// <returns></returns>
        public TValue GetValueOrDefault(Int16 key, TValue @default = default)
        {
            return TryGetValue(key, out TValue value) ? value : @default;
        }

        /// <summary>
        ///     Add element.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        private Boolean TryAddInternal(Int16 key, TValue value, out TValue result)
        {
            Int32 nextCapacity = CalculateCapacity(Count + 1, _factor);
            if (_buckets.Length < nextCapacity)
            {
                //--- rehash
                Entry[] nextBucket = new Entry[nextCapacity];
                // ReSharper disable once ForCanBeConvertedToForeach
                for (Int32 i = 0; i < _buckets.Length; i++)
                {
                    Entry e = _buckets[i];
                    while (e is not null)
                    {
                        Entry newEntry = new Entry(e.Key, e.Value, e.Hash);
                        AddToBuckets(nextBucket, key, newEntry, default, out _);
                        e = e.Next;
                    }
                }

                Boolean success = AddToBuckets(nextBucket, key, null, value, out result);
                _buckets = nextBucket;
                if (success)
                {
                    Count++;
                }

                return success;
            }
            else
            {
                Boolean success = AddToBuckets(_buckets, key, null, value, out result);
                if (success)
                {
                    Count++;
                }

                return success;
            }

            //--- please pass 'key + newEntry' or 'key + value'.
            static Boolean AddToBuckets(Entry[] buckets, Int16 newKey, Entry newEntry, TValue value, out TValue result)
            {
                Int32 hash = newEntry?.Hash ?? newKey.GetHashCode();
                Int32 index = hash & (buckets.Length - 1);
                if (buckets[index] is null)
                {
                    if (newEntry is null)
                    {
                        result = value;
                        buckets[index] = new Entry(newKey, result, hash);
                    }
                    else
                    {
                        result = newEntry.Value;
                        buckets[index] = newEntry;
                    }
                }
                else
                {
                    Entry lastEntry = buckets[index];
                    while (true)
                    {
                        if (lastEntry.Key == newKey)
                        {
                            result = lastEntry.Value;
                            return false;
                        }

                        if (lastEntry.Next is null)
                        {
                            if (newEntry is null)
                            {
                                result = value;
                                lastEntry.Next = new Entry(newKey, result, hash);
                            }
                            else
                            {
                                result = newEntry.Value;
                                lastEntry.Next = newEntry;
                            }

                            break;
                        }

                        lastEntry = lastEntry.Next;
                    }
                }

                return true;
            }
        }

        /// <summary>
        ///     Calculates bucket capacity.
        /// </summary>
        /// <param name="size"></param>
        /// <param name="factor"></param>
        /// <returns></returns>
        private static Int32 CalculateCapacity(Int32 size, Single factor)
        {
            Int32 initialCapacity = (Int32) (size / factor);
            Int32 capacity = 1;
            while (capacity < initialCapacity)
            {
                capacity <<= 1;
            }

            return capacity > 8 ? capacity : 8;
        }

        /// <summary>
        ///     Gets the element that has the specified key in the read-only dictionary.
        /// </summary>
        /// <param name="key">The key to locate.</param>
        /// <returns>The element that has the specified key in the read-only dictionary.</returns>
        public TValue this[Int16 key]
        {
            get
            {
                return TryGetValue(key, out TValue value) ? value : throw new KeyNotFoundException();
            }
        }
        
        /// <summary>
        ///     Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator<KeyValuePair<Int16, TValue>> GetEnumerator()
        {
            return _buckets.Select(entry => new KeyValuePair<Int16, TValue>(entry.Key, entry.Value)).GetEnumerator();
        }

        /// <summary>
        ///     Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>An <see cref="IEnumerator" /> object that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        ///     Represents <see cref="FrozenInt16KeyDictionary{TValue}" /> entry.
        /// </summary>
        private class Entry
        {
            public readonly Int32 Hash;
            public readonly Int16 Key;
            public readonly TValue Value;
            public Entry Next;

            public Entry(Int16 key, TValue value, Int32 hash)
            {
                Key = key;
                Value = value;
                Hash = hash;
            }
        }
    }
    
    /// <summary>
    ///     Provides a read-only dictionary that contents are fixed at the time of instance creation.
    /// </summary>
    /// <typeparam name="TValue">The type of values in the dictionary.</typeparam>
    public sealed class FrozenUInt16KeyDictionary<TValue> : IReadOnlyDictionary<UInt16, TValue>
    {
        /// <summary>
        ///     Creates a <see cref="FrozenUInt16KeyDictionary{TValue}" /> from an <see cref="IEnumerable{T}" /> according to a
        ///     specified key selector function.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static FrozenUInt16KeyDictionary<TValue> Create(IEnumerable<TValue> source, Func<TValue, UInt16> selector)
        {
            return Create(source, selector, PassThrough);
        }

        /// <summary>
        ///     Creates a <see cref="FrozenUInt16KeyDictionary{TValue}" /> from an <see cref="IEnumerable{T}" /> according to
        ///     specified key selector and value selector functions.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <param name="valueSelector"></param>
        /// <returns></returns>
        public static FrozenUInt16KeyDictionary<TValue> Create<TSource>(IEnumerable<TSource> source, Func<TSource, UInt16> keySelector, Func<TSource, TValue> valueSelector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (keySelector is null)
            {
                throw new ArgumentNullException(nameof(keySelector));
            }

            if (valueSelector is null)
            {
                throw new ArgumentNullException(nameof(valueSelector));
            }

            const Int32 initialSize = 4;
            const Single loadFactor = 0.75f;
            Int32 size = source.CountIfMaterialized() ?? initialSize;
            Int32 bucketSize = CalculateCapacity(size, loadFactor);
            FrozenUInt16KeyDictionary<TValue> result = new FrozenUInt16KeyDictionary<TValue>(bucketSize, loadFactor);

            foreach (TSource x in source)
            {
                UInt16 key = keySelector(x);
                TValue value = valueSelector(x);
                if (!result.TryAddInternal(key, value, out _))
                {
                    throw new ArgumentException($"Key was already exists. Key:{key}");
                }
            }

            return result;
        }

        private static readonly Func<TValue, TValue> PassThrough = x => x;
        private readonly Single _factor;
        
        private Entry[] _buckets;

        /// <summary>
        ///     Gets an enumerable collection that contains the keys in the read-only dictionary.
        /// </summary>
        public IEnumerable<UInt16> Keys
        {
            get
            {
                return _buckets.Select(entry => entry.Key);
            }
        }

        /// <summary>
        ///     Gets an enumerable collection that contains the values in the read-only dictionary.
        /// </summary>
        public IEnumerable<TValue> Values
        {
            get
            {
                return _buckets.Select(entry => entry.Value);
            }
        }

        /// <summary>
        ///     Gets the number of elements in the collection.
        /// </summary>
        public Int32 Count { get; private set; }

        /// <summary>
        ///     Creates instance.
        /// </summary>
        /// <param name="size"></param>
        /// <param name="factor"></param>
        private FrozenUInt16KeyDictionary(Int32 size, Single factor)
        {
            _buckets = size == 0 ? Array.Empty<Entry>() : new Entry[size];
            _factor = factor;
        }

        /// <summary>
        ///     Determines whether the read-only dictionary contains an element that has the specified key.
        /// </summary>
        /// <param name="key">The key to locate.</param>
        /// <returns>
        ///     true if the read-only dictionary contains an element that has the specified key; otherwise, false.
        /// </returns>
        public Boolean ContainsKey(UInt16 key)
        {
            return TryGetValue(key, out _);
        }

        /// <summary>
        ///     Gets the value that is associated with the specified key.
        /// </summary>
        /// <param name="key">The key to locate.</param>
        /// <param name="value">
        ///     When this method returns, the value associated with the specified key, if the key is found;
        ///     otherwise, the default value for the type of the value parameter.
        ///     This parameter is passed uninitialized.
        /// </param>
        /// <returns>
        ///     true if the object that implements the <see cref="IReadOnlyDictionary{TKey, TValue}" /> interface contains an
        ///     element that has the specified key; otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean TryGetValue(UInt16 key, out TValue value)
        {
            Int32 hash = key.GetHashCode();
            Int32 index = hash & (_buckets.Length - 1);
            Entry next = _buckets[index];
            while (next is not null)
            {
                if (next.Key == key)
                {
                    value = next.Value;
                    return true;
                }

                next = next.Next;
            }

            value = default;
            return false;
        }

        /// <summary>
        ///     Gets the element that has the specified key in the read-only dictionary or the default value of the element type.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="default"></param>
        /// <returns></returns>
        public TValue GetValueOrDefault(UInt16 key, TValue @default = default)
        {
            return TryGetValue(key, out TValue value) ? value : @default;
        }
        
        /// <summary>
        ///     Add element.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        private Boolean TryAddInternal(UInt16 key, TValue value, out TValue result)
        {
            Int32 nextCapacity = CalculateCapacity(Count + 1, _factor);
            if (_buckets.Length < nextCapacity)
            {
                //--- rehash
                Entry[] nextBucket = new Entry[nextCapacity];
                // ReSharper disable once ForCanBeConvertedToForeach
                for (Int32 i = 0; i < _buckets.Length; i++)
                {
                    Entry entry = _buckets[i];
                    while (entry is not null)
                    {
                        Entry newEntry = new Entry(entry.Key, entry.Value, entry.Hash);
                        AddToBuckets(nextBucket, key, newEntry, default, out _);
                        entry = entry.Next;
                    }
                }

                Boolean success = AddToBuckets(nextBucket, key, null, value, out result);
                _buckets = nextBucket;
                if (success)
                {
                    Count++;
                }

                return success;
            }
            else
            {
                Boolean success = AddToBuckets(_buckets, key, null, value, out result);
                if (success)
                {
                    Count++;
                }

                return success;
            }

            //--- please pass 'key + newEntry' or 'key + value'.
            static Boolean AddToBuckets(Entry[] buckets, UInt16 newKey, Entry newEntry, TValue value, out TValue result)
            {
                Int32 hash = newEntry?.Hash ?? newKey.GetHashCode();
                Int32 index = hash & (buckets.Length - 1);
                if (buckets[index] is null)
                {
                    if (newEntry is null)
                    {
                        result = value;
                        buckets[index] = new Entry(newKey, result, hash);
                    }
                    else
                    {
                        result = newEntry.Value;
                        buckets[index] = newEntry;
                    }
                }
                else
                {
                    Entry lastEntry = buckets[index];
                    while (true)
                    {
                        if (lastEntry.Key == newKey)
                        {
                            result = lastEntry.Value;
                            return false;
                        }

                        if (lastEntry.Next is null)
                        {
                            if (newEntry is null)
                            {
                                result = value;
                                lastEntry.Next = new Entry(newKey, result, hash);
                            }
                            else
                            {
                                result = newEntry.Value;
                                lastEntry.Next = newEntry;
                            }

                            break;
                        }

                        lastEntry = lastEntry.Next;
                    }
                }

                return true;
            }
        }

        /// <summary>
        ///     Calculates bucket capacity.
        /// </summary>
        /// <param name="size"></param>
        /// <param name="factor"></param>
        /// <returns></returns>
        private static Int32 CalculateCapacity(Int32 size, Single factor)
        {
            Int32 initialCapacity = (Int32) (size / factor);
            Int32 capacity = 1;
            while (capacity < initialCapacity)
            {
                capacity <<= 1;
            }

            return capacity > 8 ? capacity : 8;
        }

        /// <summary>
        ///     Gets the element that has the specified key in the read-only dictionary.
        /// </summary>
        /// <param name="key">The key to locate.</param>
        /// <returns>The element that has the specified key in the read-only dictionary.</returns>
        public TValue this[UInt16 key]
        {
            get
            {
                return TryGetValue(key, out TValue value) ? value : throw new KeyNotFoundException();
            }
        }
        
        /// <summary>
        ///     Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator<KeyValuePair<UInt16, TValue>> GetEnumerator()
        {
            return _buckets.Select(entry => new KeyValuePair<UInt16, TValue>(entry.Key, entry.Value)).GetEnumerator();
        }

        /// <summary>
        ///     Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>An <see cref="IEnumerator" /> object that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        
        /// <summary>
        ///     Represents <see cref="FrozenUInt16KeyDictionary{TValue}" /> entry.
        /// </summary>
        private class Entry
        {
            public readonly Int32 Hash;
            public readonly UInt16 Key;
            public readonly TValue Value;
            public Entry Next;

            public Entry(UInt16 key, TValue value, Int32 hash)
            {
                Key = key;
                Value = value;
                Hash = hash;
            }
        }
    }

    /// <summary>
    ///     Provides a read-only dictionary that contents are fixed at the time of instance creation.
    /// </summary>
    /// <typeparam name="TValue">The type of values in the dictionary.</typeparam>
    public sealed class FrozenInt32KeyDictionary<TValue> : IReadOnlyDictionary<Int32, TValue>
    {
        /// <summary>
        ///     Creates a <see cref="FrozenInt32KeyDictionary{TValue}" /> from an <see cref="IEnumerable{T}" /> according to a
        ///     specified key selector function.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static FrozenInt32KeyDictionary<TValue> Create(IEnumerable<TValue> source, Func<TValue, Int32> selector)
        {
            return Create(source, selector, PassThrough);
        }

        /// <summary>
        ///     Creates a <see cref="FrozenInt32KeyDictionary{TValue}" /> from an <see cref="IEnumerable{T}" /> according to
        ///     specified key selector and value selector functions.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <param name="valueSelector"></param>
        /// <returns></returns>
        public static FrozenInt32KeyDictionary<TValue> Create<TSource>(IEnumerable<TSource> source, Func<TSource, Int32> keySelector, Func<TSource, TValue> valueSelector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (keySelector is null)
            {
                throw new ArgumentNullException(nameof(keySelector));
            }

            if (valueSelector is null)
            {
                throw new ArgumentNullException(nameof(valueSelector));
            }

            const Int32 initialSize = 4;
            const Single loadFactor = 0.75f;
            Int32 size = source.CountIfMaterialized() ?? initialSize;
            Int32 bucketSize = CalculateCapacity(size, loadFactor);
            FrozenInt32KeyDictionary<TValue> result = new FrozenInt32KeyDictionary<TValue>(bucketSize, loadFactor);

            foreach (TSource x in source)
            {
                Int32 key = keySelector(x);
                TValue value = valueSelector(x);
                if (!result.TryAddInternal(key, value, out _))
                {
                    throw new ArgumentException($"Key was already exists. Key:{key}");
                }
            }

            return result;
        }
        
        private static readonly Func<TValue, TValue> PassThrough = x => x;
        private readonly Single _factor;
        
        private Entry[] _buckets;

        /// <summary>
        ///     Gets an enumerable collection that contains the keys in the read-only dictionary.
        /// </summary>
        public IEnumerable<Int32> Keys
        {
            get
            {
                return _buckets.Select(entry => entry.Key);
            }
        }

        /// <summary>
        ///     Gets an enumerable collection that contains the values in the read-only dictionary.
        /// </summary>
        public IEnumerable<TValue> Values
        {
            get
            {
                return _buckets.Select(entry => entry.Value);
            }
        }

        /// <summary>
        ///     Gets the number of elements in the collection.
        /// </summary>
        public Int32 Count { get; private set; }
        
        /// <summary>
        ///     Creates instance.
        /// </summary>
        /// <param name="size"></param>
        /// <param name="factor"></param>
        private FrozenInt32KeyDictionary(Int32 size, Single factor)
        {
            _buckets = size == 0 ? Array.Empty<Entry>() : new Entry[size];
            _factor = factor;
        }

        /// <summary>
        ///     Determines whether the read-only dictionary contains an element that has the specified key.
        /// </summary>
        /// <param name="key">The key to locate.</param>
        /// <returns>
        ///     true if the read-only dictionary contains an element that has the specified key; otherwise, false.
        /// </returns>
        public Boolean ContainsKey(Int32 key)
        {
            return TryGetValue(key, out _);
        }

        /// <summary>
        ///     Gets the value that is associated with the specified key.
        /// </summary>
        /// <param name="key">The key to locate.</param>
        /// <param name="value">
        ///     When this method returns, the value associated with the specified key, if the key is found;
        ///     otherwise, the default value for the type of the value parameter.
        ///     This parameter is passed uninitialized.
        /// </param>
        /// <returns>
        ///     true if the object that implements the <see cref="IReadOnlyDictionary{TKey, TValue}" /> interface contains an
        ///     element that has the specified key; otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean TryGetValue(Int32 key, out TValue value)
        {
            Int32 hash = key.GetHashCode();
            Int32 index = hash & (_buckets.Length - 1);
            Entry next = _buckets[index];
            while (next is not null)
            {
                if (next.Key == key)
                {
                    value = next.Value;
                    return true;
                }

                next = next.Next;
            }

            value = default;
            return false;
        }
        
        /// <summary>
        ///     Gets the element that has the specified key in the read-only dictionary or the default value of the element type.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="default"></param>
        /// <returns></returns>
        public TValue GetValueOrDefault(Int32 key, TValue @default = default)
        {
            return TryGetValue(key, out TValue value) ? value : @default;
        }

        /// <summary>
        ///     Add element.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        private Boolean TryAddInternal(Int32 key, TValue value, out TValue result)
        {
            Int32 nextCapacity = CalculateCapacity(Count + 1, _factor);
            if (_buckets.Length < nextCapacity)
            {
                //--- rehash
                Entry[] nextBucket = new Entry[nextCapacity];
                // ReSharper disable once ForCanBeConvertedToForeach
                for (Int32 i = 0; i < _buckets.Length; i++)
                {
                    Entry entry = _buckets[i];
                    while (entry is not null)
                    {
                        Entry newEntry = new Entry(entry.Key, entry.Value, entry.Hash);
                        AddToBuckets(nextBucket, key, newEntry, default, out _);
                        entry = entry.Next;
                    }
                }

                Boolean success = AddToBuckets(nextBucket, key, null, value, out result);
                _buckets = nextBucket;
                if (success)
                {
                    Count++;
                }

                return success;
            }
            else
            {
                Boolean success = AddToBuckets(_buckets, key, null, value, out result);
                if (success)
                {
                    Count++;
                }

                return success;
            }

            //--- please pass 'key + newEntry' or 'key + value'.
            static Boolean AddToBuckets(Entry[] buckets, Int32 newKey, Entry newEntry, TValue value, out TValue result)
            {
                Int32 hash = newEntry?.Hash ?? newKey.GetHashCode();
                Int32 index = hash & (buckets.Length - 1);
                if (buckets[index] is null)
                {
                    if (newEntry is null)
                    {
                        result = value;
                        buckets[index] = new Entry(newKey, result, hash);
                    }
                    else
                    {
                        result = newEntry.Value;
                        buckets[index] = newEntry;
                    }
                }
                else
                {
                    Entry lastEntry = buckets[index];
                    while (true)
                    {
                        if (lastEntry.Key == newKey)
                        {
                            result = lastEntry.Value;
                            return false;
                        }

                        if (lastEntry.Next is null)
                        {
                            if (newEntry is null)
                            {
                                result = value;
                                lastEntry.Next = new Entry(newKey, result, hash);
                            }
                            else
                            {
                                result = newEntry.Value;
                                lastEntry.Next = newEntry;
                            }

                            break;
                        }

                        lastEntry = lastEntry.Next;
                    }
                }

                return true;
            }
        }

        /// <summary>
        ///     Calculates bucket capacity.
        /// </summary>
        /// <param name="size"></param>
        /// <param name="factor"></param>
        /// <returns></returns>
        private static Int32 CalculateCapacity(Int32 size, Single factor)
        {
            Int32 initialCapacity = (Int32) (size / factor);
            Int32 capacity = 1;
            while (capacity < initialCapacity)
            {
                capacity <<= 1;
            }

            return capacity > 8 ? capacity : 8;
        }

        /// <summary>
        ///     Gets the element that has the specified key in the read-only dictionary.
        /// </summary>
        /// <param name="key">The key to locate.</param>
        /// <returns>The element that has the specified key in the read-only dictionary.</returns>
        public TValue this[Int32 key]
        {
            get
            {
                return TryGetValue(key, out TValue value) ? value : throw new KeyNotFoundException();
            }
        }

        /// <summary>
        ///     Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator<KeyValuePair<Int32, TValue>> GetEnumerator()
        {
            return _buckets.Select(entry => new KeyValuePair<Int32, TValue>(entry.Key, entry.Value)).GetEnumerator();
        }

        /// <summary>
        ///     Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>An <see cref="IEnumerator" /> object that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        ///     Represents <see cref="FrozenInt32KeyDictionary{TValue}" /> entry.
        /// </summary>
        private class Entry
        {
            public readonly Int32 Hash;
            public readonly Int32 Key;
            public readonly TValue Value;
            public Entry Next;

            public Entry(Int32 key, TValue value, Int32 hash)
            {
                Key = key;
                Value = value;
                Hash = hash;
            }
        }
    }
    
    /// <summary>
    ///     Provides a read-only dictionary that contents are fixed at the time of instance creation.
    /// </summary>
    /// <typeparam name="TValue">The type of values in the dictionary.</typeparam>
    public sealed class FrozenUInt32KeyDictionary<TValue> : IReadOnlyDictionary<UInt32, TValue>
    {
        /// <summary>
        ///     Creates a <see cref="FrozenUInt32KeyDictionary{TValue}" /> from an <see cref="IEnumerable{T}" /> according to a
        ///     specified key selector function.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static FrozenUInt32KeyDictionary<TValue> Create(IEnumerable<TValue> source, Func<TValue, UInt32> selector)
        {
            return Create(source, selector, PassThrough);
        }

        /// <summary>
        ///     Creates a <see cref="FrozenUInt32KeyDictionary{TValue}" /> from an <see cref="IEnumerable{T}" /> according to
        ///     specified key selector and value selector functions.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <param name="valueSelector"></param>
        /// <returns></returns>
        public static FrozenUInt32KeyDictionary<TValue> Create<TSource>(IEnumerable<TSource> source, Func<TSource, UInt32> keySelector, Func<TSource, TValue> valueSelector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (keySelector is null)
            {
                throw new ArgumentNullException(nameof(keySelector));
            }

            if (valueSelector is null)
            {
                throw new ArgumentNullException(nameof(valueSelector));
            }

            const Int32 initialSize = 4;
            const Single loadFactor = 0.75f;
            Int32 size = source.CountIfMaterialized() ?? initialSize;
            Int32 bucketSize = CalculateCapacity(size, loadFactor);
            FrozenUInt32KeyDictionary<TValue> result = new FrozenUInt32KeyDictionary<TValue>(bucketSize, loadFactor);

            foreach (TSource x in source)
            {
                UInt32 key = keySelector(x);
                TValue value = valueSelector(x);
                if (!result.TryAddInternal(key, value, out _))
                {
                    throw new ArgumentException($"Key was already exists. Key:{key}");
                }
            }

            return result;
        }
        
        private static readonly Func<TValue, TValue> PassThrough = x => x;
        private readonly Single _factor;
        
        private Entry[] _buckets;

        /// <summary>
        ///     Gets an enumerable collection that contains the keys in the read-only dictionary.
        /// </summary>
        public IEnumerable<UInt32> Keys
        {
            get
            {
                return _buckets.Select(entry => entry.Key);
            }
        }

        /// <summary>
        ///     Gets an enumerable collection that contains the values in the read-only dictionary.
        /// </summary>
        public IEnumerable<TValue> Values
        {
            get
            {
                return _buckets.Select(entry => entry.Value);
            }
        }

        /// <summary>
        ///     Gets the number of elements in the collection.
        /// </summary>
        public Int32 Count { get; private set; }
        
        /// <summary>
        ///     Creates instance.
        /// </summary>
        /// <param name="size"></param>
        /// <param name="factor"></param>
        private FrozenUInt32KeyDictionary(Int32 size, Single factor)
        {
            _buckets = size == 0 ? Array.Empty<Entry>() : new Entry[size];
            _factor = factor;
        }

        /// <summary>
        ///     Determines whether the read-only dictionary contains an element that has the specified key.
        /// </summary>
        /// <param name="key">The key to locate.</param>
        /// <returns>
        ///     true if the read-only dictionary contains an element that has the specified key; otherwise, false.
        /// </returns>
        public Boolean ContainsKey(UInt32 key)
        {
            return TryGetValue(key, out _);
        }

        /// <summary>
        ///     Gets the value that is associated with the specified key.
        /// </summary>
        /// <param name="key">The key to locate.</param>
        /// <param name="value">
        ///     When this method returns, the value associated with the specified key, if the key is found;
        ///     otherwise, the default value for the type of the value parameter.
        ///     This parameter is passed uninitialized.
        /// </param>
        /// <returns>
        ///     true if the object that implements the <see cref="IReadOnlyDictionary{TKey, TValue}" /> interface contains an
        ///     element that has the specified key; otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean TryGetValue(UInt32 key, out TValue value)
        {
            Int32 hash = key.GetHashCode();
            Int32 index = hash & (_buckets.Length - 1);
            Entry next = _buckets[index];
            while (next is not null)
            {
                if (next.Key == key)
                {
                    value = next.Value;
                    return true;
                }

                next = next.Next;
            }

            value = default;
            return false;
        }
        
        /// <summary>
        ///     Gets the element that has the specified key in the read-only dictionary or the default value of the element type.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="default"></param>
        /// <returns></returns>
        public TValue GetValueOrDefault(UInt32 key, TValue @default = default)
        {
            return TryGetValue(key, out TValue value) ? value : @default;
        }

        /// <summary>
        ///     Add element.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        private Boolean TryAddInternal(UInt32 key, TValue value, out TValue result)
        {
            Int32 nextCapacity = CalculateCapacity(Count + 1, _factor);
            if (_buckets.Length < nextCapacity)
            {
                //--- rehash
                Entry[] nextBucket = new Entry[nextCapacity];
                // ReSharper disable once ForCanBeConvertedToForeach
                for (Int32 i = 0; i < _buckets.Length; i++)
                {
                    Entry e = _buckets[i];
                    while (e is not null)
                    {
                        Entry newEntry = new Entry(e.Key, e.Value, e.Hash);
                        AddToBuckets(nextBucket, key, newEntry, default, out _);
                        e = e.Next;
                    }
                }

                Boolean success = AddToBuckets(nextBucket, key, null, value, out result);
                _buckets = nextBucket;
                if (success)
                {
                    Count++;
                }

                return success;
            }
            else
            {
                Boolean success = AddToBuckets(_buckets, key, null, value, out result);
                if (success)
                {
                    Count++;
                }

                return success;
            }

            //--- please pass 'key + newEntry' or 'key + value'.
            static Boolean AddToBuckets(Entry[] buckets, UInt32 newKey, Entry newEntry, TValue value, out TValue result)
            {
                Int32 hash = newEntry?.Hash ?? newKey.GetHashCode();
                Int32 index = hash & (buckets.Length - 1);
                if (buckets[index] is null)
                {
                    if (newEntry is null)
                    {
                        result = value;
                        buckets[index] = new Entry(newKey, result, hash);
                    }
                    else
                    {
                        result = newEntry.Value;
                        buckets[index] = newEntry;
                    }
                }
                else
                {
                    Entry lastEntry = buckets[index];
                    while (true)
                    {
                        if (lastEntry.Key == newKey)
                        {
                            result = lastEntry.Value;
                            return false;
                        }

                        if (lastEntry.Next is null)
                        {
                            if (newEntry is null)
                            {
                                result = value;
                                lastEntry.Next = new Entry(newKey, result, hash);
                            }
                            else
                            {
                                result = newEntry.Value;
                                lastEntry.Next = newEntry;
                            }

                            break;
                        }

                        lastEntry = lastEntry.Next;
                    }
                }

                return true;
            }
        }

        /// <summary>
        ///     Calculates bucket capacity.
        /// </summary>
        /// <param name="size"></param>
        /// <param name="factor"></param>
        /// <returns></returns>
        private static Int32 CalculateCapacity(Int32 size, Single factor)
        {
            Int32 initialCapacity = (Int32) (size / factor);
            Int32 capacity = 1;
            while (capacity < initialCapacity)
            {
                capacity <<= 1;
            }

            return capacity > 8 ? capacity : 8;
        }

        /// <summary>
        ///     Gets the element that has the specified key in the read-only dictionary.
        /// </summary>
        /// <param name="key">The key to locate.</param>
        /// <returns>The element that has the specified key in the read-only dictionary.</returns>
        public TValue this[UInt32 key]
        {
            get
            {
                return TryGetValue(key, out TValue value) ? value : throw new KeyNotFoundException();
            }
        }
        
        /// <summary>
        ///     Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator<KeyValuePair<UInt32, TValue>> GetEnumerator()
        {
            return _buckets.Select(entry => new KeyValuePair<UInt32, TValue>(entry.Key, entry.Value)).GetEnumerator();
        }

        /// <summary>
        ///     Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>An <see cref="IEnumerator" /> object that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        ///     Represents <see cref="FrozenUInt32KeyDictionary{TValue}" /> entry.
        /// </summary>
        private class Entry
        {
            public readonly Int32 Hash;
            public readonly UInt32 Key;
            public readonly TValue Value;
            public Entry Next;

            public Entry(UInt32 key, TValue value, Int32 hash)
            {
                Key = key;
                Value = value;
                Hash = hash;
            }
        }
    }
    
    /// <summary>
    ///     Provides a read-only dictionary that contents are fixed at the time of instance creation.
    /// </summary>
    /// <typeparam name="TValue">The type of values in the dictionary.</typeparam>
    public sealed class FrozenInt64KeyDictionary<TValue> : IReadOnlyDictionary<Int64, TValue>
    {
        /// <summary>
        ///     Creates a <see cref="FrozenInt64KeyDictionary{TValue}" /> from an <see cref="IEnumerable{T}" /> according to a
        ///     specified key selector function.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static FrozenInt64KeyDictionary<TValue> Create(IEnumerable<TValue> source, Func<TValue, Int64> selector)
        {
            return Create(source, selector, PassThrough);
        }

        /// <summary>
        ///     Creates a <see cref="FrozenInt64KeyDictionary{TValue}" /> from an <see cref="IEnumerable{T}" /> according to
        ///     specified key selector and value selector functions.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <param name="valueSelector"></param>
        /// <returns></returns>
        public static FrozenInt64KeyDictionary<TValue> Create<TSource>(IEnumerable<TSource> source, Func<TSource, Int64> keySelector, Func<TSource, TValue> valueSelector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (keySelector is null)
            {
                throw new ArgumentNullException(nameof(keySelector));
            }

            if (valueSelector is null)
            {
                throw new ArgumentNullException(nameof(valueSelector));
            }

            const Int32 initialSize = 4;
            const Single loadFactor = 0.75f;
            Int32 size = source.CountIfMaterialized() ?? initialSize;
            Int32 bucketSize = CalculateCapacity(size, loadFactor);
            FrozenInt64KeyDictionary<TValue> result = new FrozenInt64KeyDictionary<TValue>(bucketSize, loadFactor);

            foreach (TSource x in source)
            {
                Int64 key = keySelector(x);
                TValue value = valueSelector(x);
                if (!result.TryAddInternal(key, value, out _))
                {
                    throw new ArgumentException($"Key was already exists. Key:{key}");
                }
            }

            return result;
        }
        
        private static readonly Func<TValue, TValue> PassThrough = x => x;
        private readonly Single _factor;
        
        private Entry[] _buckets;

        /// <summary>
        ///     Gets an enumerable collection that contains the keys in the read-only dictionary.
        /// </summary>
        public IEnumerable<Int64> Keys
        {
            get
            {
                return _buckets.Select(entry => entry.Key);
            }
        }

        /// <summary>
        ///     Gets an enumerable collection that contains the values in the read-only dictionary.
        /// </summary>
        public IEnumerable<TValue> Values
        {
            get
            {
                return _buckets.Select(entry => entry.Value);
            }
        }

        /// <summary>
        ///     Gets the number of elements in the collection.
        /// </summary>
        public Int32 Count { get; private set; }
        
        /// <summary>
        ///     Creates instance.
        /// </summary>
        /// <param name="size"></param>
        /// <param name="factor"></param>
        private FrozenInt64KeyDictionary(Int32 size, Single factor)
        {
            _buckets = size == 0 ? Array.Empty<Entry>() : new Entry[size];
            _factor = factor;
        }

        /// <summary>
        ///     Determines whether the read-only dictionary contains an element that has the specified key.
        /// </summary>
        /// <param name="key">The key to locate.</param>
        /// <returns>
        ///     true if the read-only dictionary contains an element that has the specified key; otherwise, false.
        /// </returns>
        public Boolean ContainsKey(Int64 key)
        {
            return TryGetValue(key, out _);
        }

        /// <summary>
        ///     Gets the value that is associated with the specified key.
        /// </summary>
        /// <param name="key">The key to locate.</param>
        /// <param name="value">
        ///     When this method returns, the value associated with the specified key, if the key is found;
        ///     otherwise, the default value for the type of the value parameter.
        ///     This parameter is passed uninitialized.
        /// </param>
        /// <returns>
        ///     true if the object that implements the <see cref="IReadOnlyDictionary{TKey, TValue}" /> interface contains an
        ///     element that has the specified key; otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean TryGetValue(Int64 key, out TValue value)
        {
            Int32 hash = key.GetHashCode();
            Int32 index = hash & (_buckets.Length - 1);
            Entry next = _buckets[index];
            while (next is not null)
            {
                if (next.Key == key)
                {
                    value = next.Value;
                    return true;
                }

                next = next.Next;
            }

            value = default;
            return false;
        }
        
        /// <summary>
        ///     Gets the element that has the specified key in the read-only dictionary or the default value of the element type.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="default"></param>
        /// <returns></returns>
        public TValue GetValueOrDefault(Int64 key, TValue @default = default)
        {
            return TryGetValue(key, out TValue value) ? value : @default;
        }

        /// <summary>
        ///     Add element.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        private Boolean TryAddInternal(Int64 key, TValue value, out TValue result)
        {
            Int32 nextCapacity = CalculateCapacity(Count + 1, _factor);
            if (_buckets.Length < nextCapacity)
            {
                //--- rehash
                Entry[] nextBucket = new Entry[nextCapacity];
                // ReSharper disable once ForCanBeConvertedToForeach
                for (Int32 i = 0; i < _buckets.Length; i++)
                {
                    Entry e = _buckets[i];
                    while (e is not null)
                    {
                        Entry newEntry = new Entry(e.Key, e.Value, e.Hash);
                        AddToBuckets(nextBucket, key, newEntry, default, out _);
                        e = e.Next;
                    }
                }

                Boolean success = AddToBuckets(nextBucket, key, null, value, out result);
                _buckets = nextBucket;
                if (success)
                {
                    Count++;
                }

                return success;
            }
            else
            {
                Boolean success = AddToBuckets(_buckets, key, null, value, out result);
                if (success)
                {
                    Count++;
                }

                return success;
            }

            //--- please pass 'key + newEntry' or 'key + value'.
            static Boolean AddToBuckets(Entry[] buckets, Int64 newKey, Entry newEntry, TValue value, out TValue result)
            {
                Int32 hash = newEntry?.Hash ?? newKey.GetHashCode();
                Int32 index = hash & (buckets.Length - 1);
                if (buckets[index] is null)
                {
                    if (newEntry is null)
                    {
                        result = value;
                        buckets[index] = new Entry(newKey, result, hash);
                    }
                    else
                    {
                        result = newEntry.Value;
                        buckets[index] = newEntry;
                    }
                }
                else
                {
                    Entry lastEntry = buckets[index];
                    while (true)
                    {
                        if (lastEntry.Key == newKey)
                        {
                            result = lastEntry.Value;
                            return false;
                        }

                        if (lastEntry.Next is null)
                        {
                            if (newEntry is null)
                            {
                                result = value;
                                lastEntry.Next = new Entry(newKey, result, hash);
                            }
                            else
                            {
                                result = newEntry.Value;
                                lastEntry.Next = newEntry;
                            }

                            break;
                        }

                        lastEntry = lastEntry.Next;
                    }
                }

                return true;
            }
        }

        /// <summary>
        ///     Calculates bucket capacity.
        /// </summary>
        /// <param name="size"></param>
        /// <param name="factor"></param>
        /// <returns></returns>
        private static Int32 CalculateCapacity(Int32 size, Single factor)
        {
            Int32 initialCapacity = (Int32) (size / factor);
            Int32 capacity = 1;
            while (capacity < initialCapacity)
            {
                capacity <<= 1;
            }

            return capacity > 8 ? capacity : 8;
        }

        /// <summary>
        ///     Gets the element that has the specified key in the read-only dictionary.
        /// </summary>
        /// <param name="key">The key to locate.</param>
        /// <returns>The element that has the specified key in the read-only dictionary.</returns>
        public TValue this[Int64 key]
        {
            get
            {
                return TryGetValue(key, out TValue value) ? value : throw new KeyNotFoundException();
            }
        }

        /// <summary>
        ///     Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator<KeyValuePair<Int64, TValue>> GetEnumerator()
        {
            return _buckets.Select(entry => new KeyValuePair<Int64, TValue>(entry.Key, entry.Value)).GetEnumerator();
        }

        /// <summary>
        ///     Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>An <see cref="IEnumerator" /> object that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        
        /// <summary>
        ///     Represents <see cref="FrozenInt64KeyDictionary{TValue}" /> entry.
        /// </summary>
        private class Entry
        {
            public readonly Int32 Hash;
            public readonly Int64 Key;
            public readonly TValue Value;
            public Entry Next;

            public Entry(Int64 key, TValue value, Int32 hash)
            {
                Key = key;
                Value = value;
                Hash = hash;
            }
        }
    }
    
    /// <summary>
    ///     Provides a read-only dictionary that contents are fixed at the time of instance creation.
    /// </summary>
    /// <typeparam name="TValue">The type of values in the dictionary.</typeparam>
    public sealed class FrozenUInt64KeyDictionary<TValue> : IReadOnlyDictionary<UInt64, TValue>
    {
        /// <summary>
        ///     Creates a <see cref="FrozenUInt64KeyDictionary{TValue}" /> from an <see cref="IEnumerable{T}" /> according to a
        ///     specified key selector function.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static FrozenUInt64KeyDictionary<TValue> Create(IEnumerable<TValue> source, Func<TValue, UInt64> selector)
        {
            return Create(source, selector, PassThrough);
        }

        /// <summary>
        ///     Creates a <see cref="FrozenUInt64KeyDictionary{TValue}" /> from an <see cref="IEnumerable{T}" /> according to
        ///     specified key selector and value selector functions.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <param name="valueSelector"></param>
        /// <returns></returns>
        public static FrozenUInt64KeyDictionary<TValue> Create<TSource>(IEnumerable<TSource> source, Func<TSource, UInt64> keySelector, Func<TSource, TValue> valueSelector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (keySelector is null)
            {
                throw new ArgumentNullException(nameof(keySelector));
            }

            if (valueSelector is null)
            {
                throw new ArgumentNullException(nameof(valueSelector));
            }

            const Int32 initialSize = 4;
            const Single loadFactor = 0.75f;
            Int32 size = source.CountIfMaterialized() ?? initialSize;
            Int32 bucketSize = CalculateCapacity(size, loadFactor);
            FrozenUInt64KeyDictionary<TValue> result = new FrozenUInt64KeyDictionary<TValue>(bucketSize, loadFactor);

            foreach (TSource x in source)
            {
                UInt64 key = keySelector(x);
                TValue value = valueSelector(x);
                if (!result.TryAddInternal(key, value, out _))
                {
                    throw new ArgumentException($"Key was already exists. Key:{key}");
                }
            }

            return result;
        }
        
        private static readonly Func<TValue, TValue> PassThrough = x => x;
        private readonly Single _factor;

        private Entry[] _buckets;

        /// <summary>
        ///     Gets an enumerable collection that contains the keys in the read-only dictionary.
        /// </summary>
        public IEnumerable<UInt64> Keys
        {
            get
            {
                return _buckets.Select(entry => entry.Key);
            }
        }

        /// <summary>
        ///     Gets an enumerable collection that contains the values in the read-only dictionary.
        /// </summary>
        public IEnumerable<TValue> Values
        {
            get
            {
                return _buckets.Select(entry => entry.Value);
            }
        }

        /// <summary>
        ///     Gets the number of elements in the collection.
        /// </summary>
        public Int32 Count { get; private set; }

        /// <summary>
        ///     Creates instance.
        /// </summary>
        /// <param name="size"></param>
        /// <param name="factor"></param>
        private FrozenUInt64KeyDictionary(Int32 size, Single factor)
        {
            _buckets = size == 0 ? Array.Empty<Entry>() : new Entry[size];
            _factor = factor;
        }

        /// <summary>
        ///     Determines whether the read-only dictionary contains an element that has the specified key.
        /// </summary>
        /// <param name="key">The key to locate.</param>
        /// <returns>
        ///     true if the read-only dictionary contains an element that has the specified key; otherwise, false.
        /// </returns>
        public Boolean ContainsKey(UInt64 key)
        {
            return TryGetValue(key, out _);
        }

        /// <summary>
        ///     Gets the value that is associated with the specified key.
        /// </summary>
        /// <param name="key">The key to locate.</param>
        /// <param name="value">
        ///     When this method returns, the value associated with the specified key, if the key is found;
        ///     otherwise, the default value for the type of the value parameter.
        ///     This parameter is passed uninitialized.
        /// </param>
        /// <returns>
        ///     true if the object that implements the <see cref="IReadOnlyDictionary{TKey, TValue}" /> interface contains an
        ///     element that has the specified key; otherwise, false.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean TryGetValue(UInt64 key, out TValue value)
        {
            Int32 hash = key.GetHashCode();
            Int32 index = hash & (_buckets.Length - 1);
            Entry next = _buckets[index];
            while (next is not null)
            {
                if (next.Key == key)
                {
                    value = next.Value;
                    return true;
                }

                next = next.Next;
            }

            value = default;
            return false;
        }

        /// <summary>
        ///     Gets the element that has the specified key in the read-only dictionary or the default value of the element type.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="default"></param>
        /// <returns></returns>
        public TValue GetValueOrDefault(UInt64 key, TValue @default = default)
        {
            return TryGetValue(key, out TValue value) ? value : @default;
        }

        /// <summary>
        ///     Add element.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        private Boolean TryAddInternal(UInt64 key, TValue value, out TValue result)
        {
            Int32 nextCapacity = CalculateCapacity(Count + 1, _factor);
            if (_buckets.Length < nextCapacity)
            {
                //--- rehash
                Entry[] nextBucket = new Entry[nextCapacity];
                // ReSharper disable once ForCanBeConvertedToForeach
                for (Int32 i = 0; i < _buckets.Length; i++)
                {
                    Entry e = _buckets[i];
                    while (e is not null)
                    {
                        Entry newEntry = new Entry(e.Key, e.Value, e.Hash);
                        AddToBuckets(nextBucket, key, newEntry, default, out _);
                        e = e.Next;
                    }
                }

                Boolean success = AddToBuckets(nextBucket, key, null, value, out result);
                _buckets = nextBucket;
                if (success)
                {
                    Count++;
                }

                return success;
            }
            else
            {
                Boolean success = AddToBuckets(_buckets, key, null, value, out result);
                if (success)
                {
                    Count++;
                }

                return success;
            }
            
            //--- please pass 'key + newEntry' or 'key + value'.
            static Boolean AddToBuckets(Entry[] buckets, UInt64 newKey, Entry newEntry, TValue value, out TValue result)
            {
                Int32 hash = newEntry?.Hash ?? newKey.GetHashCode();
                Int32 index = hash & (buckets.Length - 1);
                if (buckets[index] is null)
                {
                    if (newEntry is null)
                    {
                        result = value;
                        buckets[index] = new Entry(newKey, result, hash);
                    }
                    else
                    {
                        result = newEntry.Value;
                        buckets[index] = newEntry;
                    }
                }
                else
                {
                    Entry lastEntry = buckets[index];
                    while (true)
                    {
                        if (lastEntry.Key == newKey)
                        {
                            result = lastEntry.Value;
                            return false;
                        }

                        if (lastEntry.Next is null)
                        {
                            if (newEntry is null)
                            {
                                result = value;
                                lastEntry.Next = new Entry(newKey, result, hash);
                            }
                            else
                            {
                                result = newEntry.Value;
                                lastEntry.Next = newEntry;
                            }

                            break;
                        }

                        lastEntry = lastEntry.Next;
                    }
                }

                return true;
            }
        }

        /// <summary>
        ///     Calculates bucket capacity.
        /// </summary>
        /// <param name="size"></param>
        /// <param name="factor"></param>
        /// <returns></returns>
        private static Int32 CalculateCapacity(Int32 size, Single factor)
        {
            Int32 initialCapacity = (Int32) (size / factor);
            Int32 capacity = 1;
            while (capacity < initialCapacity)
            {
                capacity <<= 1;
            }

            return capacity > 8 ? capacity : 8;
        }

        /// <summary>
        ///     Gets the element that has the specified key in the read-only dictionary.
        /// </summary>
        /// <param name="key">The key to locate.</param>
        /// <returns>The element that has the specified key in the read-only dictionary.</returns>
        public TValue this[UInt64 key]
        {
            get
            {
                return TryGetValue(key, out TValue value) ? value : throw new KeyNotFoundException();
            }
        }

        /// <summary>
        ///     Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator<KeyValuePair<UInt64, TValue>> GetEnumerator()
        {
            return _buckets.Select(entry => new KeyValuePair<UInt64, TValue>(entry.Key, entry.Value)).GetEnumerator();
        }

        /// <summary>
        ///     Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>An <see cref="IEnumerator" /> object that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        ///     Represents <see cref="FrozenUInt64KeyDictionary{TValue}" /> entry.
        /// </summary>
        private class Entry
        {
            public readonly Int32 Hash;
            public readonly UInt64 Key;
            public readonly TValue Value;
            public Entry Next;

            public Entry(UInt64 key, TValue value, Int32 hash)
            {
                Key = key;
                Value = value;
                Hash = hash;
            }
        }
    }
}