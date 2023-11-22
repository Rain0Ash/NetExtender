// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Types.Tries
{
    /// <summary>
    /// Defines a key/value pair that can be set or retrieved from <see cref="StringTrie{TValue}"/>.
    /// </summary>
    public readonly struct StringTrieEntry<TValue>
    {
        /// <summary>
        /// Gets the key in the key/value pair.
        /// </summary>
        public String Key { get; }

        /// <summary>
        /// Gets the value in the key/value pair.
        /// </summary>
        public TValue Value { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringTrieEntry{TValue}"/> structure with the specified key and value.
        /// </summary>
        /// <param name="key">The <see cref="string"/> object defined in each key/value pair.</param>
        /// <param name="value">The definition associated with key.</param>
        public StringTrieEntry(String key, TValue value)
        {
            Key = key;
            Value = value;
        }
    }
}