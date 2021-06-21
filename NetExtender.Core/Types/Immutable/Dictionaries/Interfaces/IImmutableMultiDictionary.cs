// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using NetExtender.Types.Dictionaries.Interfaces;

namespace NetExtender.Types.Immutable.Dictionaries.Interfaces
{
    public interface IImmutableMultiDictionary<TKey, TValue> : IReadOnlyMultiDictionary<TKey, TValue>, IImmutableDictionary<TKey, TValue>, IImmutableDictionary<TKey, ImmutableHashSet<TValue>>
    {
        public new IEnumerator<KeyValuePair<TKey, ImmutableHashSet<TValue>>> GetEnumerator();
        
        /// <summary>
        /// Gets an empty dictionary with equivalent ordering and key/value comparison rules.
        /// </summary>
        public new IImmutableMultiDictionary<TKey, TValue> Clear();

        /// <summary>
        /// Adds the specified key and value to the dictionary.
        /// </summary>
        /// <param name="key">The key of the entry to add.</param>
        /// <param name="value">The value of the entry to add.</param>
        /// <returns>The new dictionary containing the additional key-value pair.</returns>
        /// <exception cref="ArgumentException">Thrown when the given key already exists in the dictionary but has a different value.</exception>
        /// <remarks>
        /// If the given key-value pair are already in the dictionary, the existing instance is returned.
        /// </remarks>
        public new IImmutableMultiDictionary<TKey, TValue> Add(TKey key, TValue value);
        
        /// <summary>
        /// Adds the specified key and value to the dictionary.
        /// </summary>
        /// <param name="key">The key of the entry to add.</param>
        /// <param name="value">The value of the entry to add.</param>
        /// <returns>The new dictionary containing the additional key-value pair.</returns>
        /// <exception cref="ArgumentException">Thrown when the given key already exists in the dictionary but has a different value.</exception>
        /// <remarks>
        /// If the given key-value pair are already in the dictionary, the existing instance is returned.
        /// </remarks>
        public new IImmutableMultiDictionary<TKey, TValue> Add(TKey key, ImmutableHashSet<TValue> value);

        /// <summary>
        /// Adds the specified key-value pairs to the dictionary.
        /// </summary>
        /// <param name="pairs">The pairs.</param>
        /// <returns>The new dictionary containing the additional key-value pairs.</returns>
        /// <exception cref="ArgumentException">Thrown when one of the given keys already exists in the dictionary but has a different value.</exception>
        public new IImmutableMultiDictionary<TKey, TValue> AddRange(IEnumerable<KeyValuePair<TKey, TValue>> pairs);
        
        /// <summary>
        /// Adds the specified key-value pairs to the dictionary.
        /// </summary>
        /// <param name="pairs">The pairs.</param>
        /// <returns>The new dictionary containing the additional key-value pairs.</returns>
        /// <exception cref="ArgumentException">Thrown when one of the given keys already exists in the dictionary but has a different value.</exception>
        public new IImmutableMultiDictionary<TKey, TValue> AddRange(IEnumerable<KeyValuePair<TKey, ImmutableHashSet<TValue>>> pairs);

        /// <summary>
        /// Sets the specified key and value to the dictionary, possibly overwriting an existing value for the given key.
        /// </summary>
        /// <param name="key">The key of the entry to add.</param>
        /// <param name="value">The value of the entry to add.</param>
        /// <returns>The new dictionary containing the additional key-value pair.</returns>
        /// <remarks>
        /// If the given key-value pair are already in the dictionary, the existing instance is returned.
        /// If the key already exists but with a different value, a new instance with the overwritten value will be returned.
        /// </remarks>
        public new IImmutableMultiDictionary<TKey, TValue> SetItem(TKey key, TValue value);
        
        /// <summary>
        /// Sets the specified key and value to the dictionary, possibly overwriting an existing value for the given key.
        /// </summary>
        /// <param name="key">The key of the entry to add.</param>
        /// <param name="value">The value of the entry to add.</param>
        /// <returns>The new dictionary containing the additional key-value pair.</returns>
        /// <remarks>
        /// If the given key-value pair are already in the dictionary, the existing instance is returned.
        /// If the key already exists but with a different value, a new instance with the overwritten value will be returned.
        /// </remarks>
        public new IImmutableMultiDictionary<TKey, TValue> SetItem(TKey key, ImmutableHashSet<TValue> value);

        /// <summary>
        /// Applies a given set of key=value pairs to an immutable dictionary, replacing any conflicting keys in the resulting dictionary.
        /// </summary>
        /// <param name="items">The key=value pairs to set on the dictionary.  Any keys that conflict with existing keys will overwrite the previous values.</param>
        /// <returns>An immutable dictionary.</returns>
        public new IImmutableMultiDictionary<TKey, TValue> SetItems(IEnumerable<KeyValuePair<TKey, TValue>> items);
        
        /// <summary>
        /// Applies a given set of key=value pairs to an immutable dictionary, replacing any conflicting keys in the resulting dictionary.
        /// </summary>
        /// <param name="items">The key=value pairs to set on the dictionary.  Any keys that conflict with existing keys will overwrite the previous values.</param>
        /// <returns>An immutable dictionary.</returns>
        public new IImmutableMultiDictionary<TKey, TValue> SetItems(IEnumerable<KeyValuePair<TKey, ImmutableHashSet<TValue>>> items);

        /// <summary>
        /// Removes the specified keys from the dictionary with their associated values.
        /// </summary>
        /// <param name="keys">The keys to remove.</param>
        /// <returns>A new dictionary with those keys removed; or this instance if those keys are not in the dictionary.</returns>
        public new IImmutableMultiDictionary<TKey, TValue> RemoveRange(IEnumerable<TKey> keys);

        /// <summary>
        /// Removes the specified key from the dictionary with its associated value.
        /// </summary>
        /// <param name="key">The key to remove.</param>
        /// <returns>A new dictionary with the matching entry removed; or this instance if the key is not in the dictionary.</returns>
        public new IImmutableMultiDictionary<TKey, TValue> Remove(TKey key);

        /// <summary>
        /// Removes the specified key from the dictionary with its associated value.
        /// </summary>
        /// <param name="key">The key to remove.</param>
        /// <param name="value"></param>
        /// <returns>A new dictionary with the matching entry removed; or this instance if the key is not in the dictionary.</returns>
        public IImmutableMultiDictionary<TKey, TValue> Remove(TKey key, TValue value);
        
        /// <summary>
        /// Removes the specified key from the dictionary with its associated value.
        /// </summary>
        /// <param name="key">The key to remove.</param>
        /// <param name="value"></param>
        /// <returns>A new dictionary with the matching entry removed; or this instance if the key is not in the dictionary.</returns>
        public IImmutableMultiDictionary<TKey, TValue> Remove(TKey key, ImmutableHashSet<TValue> value);

        /// <summary>
        /// Searches the dictionary for a given key and returns the equal key it finds, if any.
        /// </summary>
        /// <param name="equalKey">The key to search for.</param>
        /// <param name="actualKey">The key from the dictionary that the search found, or <paramref name="equalKey"/> if the search yielded no match.</param>
        /// <returns>A value indicating whether the search was successful.</returns>
        /// <remarks>
        /// This can be useful when you want to reuse a previously stored reference instead of
        /// a newly constructed one (so that more sharing of references can occur) or to look up
        /// the canonical value, or a value that has more complete data than the value you currently have,
        /// although their comparer functions indicate they are equal.
        /// </remarks>
        public new Boolean TryGetKey(TKey equalKey, out TKey actualKey);
    }
}