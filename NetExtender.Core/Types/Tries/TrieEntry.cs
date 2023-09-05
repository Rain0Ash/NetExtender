// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;

namespace NetExtender.Initializer.Types.Tries
{
    /// <summary>
    /// Defines a key/value pair that can be set or retrieved from <see cref="StringTrie{TValue}"/>.
    /// </summary>
    public readonly struct TrieEntry<TKey, TValue>
    {
        /// <summary>
        /// Gets the key in the key/value pair.
        /// </summary>
        public IEnumerable<TKey> Key { get; }

        /// <summary>
        /// Gets the value in the key/value pair.
        /// </summary>
        public TValue Value { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringEntry{TValue}"/> structure with the specified key and value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The definition associated with key.</param>
        public TrieEntry(IEnumerable<TKey> key, TValue value)
        {
            Key = key ?? throw new ArgumentNullException(nameof(key));
            Value = value;
        }
    }
}