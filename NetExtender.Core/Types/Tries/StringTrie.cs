// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace NetExtender.Initializer.Types.Tries
{
    /// <summary>
    /// Implementation of trie data structure.
    /// </summary>
    /// <typeparam name="TValue">The type of values in the trie.</typeparam>
    public class StringTrie<TValue> : IDictionary<String, TValue>
    {
        protected Trie<Char, TValue> Trie { get; }

        Boolean ICollection<KeyValuePair<String, TValue>>.IsReadOnly
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Gets the number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <returns>
        /// The number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </returns>
        public Int32 Count
        {
            get
            {
                return Trie.Count;
            }
        }

        /// <summary>
        /// Gets an <see cref="T:System.Collections.Generic.ICollection`1"/> containing the keys of the <see cref="T:System.Collections.Generic.IDictionary`2"/>.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.Generic.ICollection`1"/> containing the keys of the object that implements <see cref="T:System.Collections.Generic.IDictionary`2"/>.
        /// </returns>
        public ICollection<String> Keys
        {
            get
            {
                return Trie.Keys.Select(item => new String(item.ToArray())).ToArray();
            }
        }

        /// <summary>
        /// Gets an <see cref="T:System.Collections.Generic.ICollection`1"/> containing the values in the <see cref="T:System.Collections.Generic.IDictionary`2"/>.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.Generic.ICollection`1"/> containing the values in the object that implements <see cref="T:System.Collections.Generic.IDictionary`2"/>.
        /// </returns>
        public ICollection<TValue> Values
        {
            get
            {
                return Trie.Values.ToArray();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringTrie{TValue}"/>.
        /// </summary>
        public StringTrie()
            : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringTrie{TValue}"/>.
        /// </summary>
        /// <param name="comparer">Comparer.</param>
        public StringTrie(IEqualityComparer<Char>? comparer)
        {
            Trie = new Trie<Char, TValue>(comparer);
        }

        /// <summary>
        /// Determines whether the <see cref="T:System.Collections.Generic.IDictionary`2"/> contains an element with the specified charKey.
        /// </summary>
        /// <returns>
        /// true if the <see cref="T:System.Collections.Generic.IDictionary`2"/> contains an element with the charKey; otherwise, false.
        /// </returns>
        /// <param name="key">The charKey to locate in the <see cref="T:System.Collections.Generic.IDictionary`2"/>.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="key"/> is null.</exception>
        public Boolean ContainsKey(String key)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return Trie.ContainsKey(key);
        }

        Boolean ICollection<KeyValuePair<String, TValue>>.Contains(KeyValuePair<String, TValue> item)
        {
            return ((IDictionary<IEnumerable<Char>, TValue>) Trie).Contains(new KeyValuePair<IEnumerable<Char>, TValue>(item.Key, item.Value));
        }

        /// <summary>
        /// Gets items by key prefix.
        /// </summary>
        /// <param name="prefix">Key prefix.</param>
        /// <returns>Collection of <see cref="StringEntry{TValue}"/> items which have key with specified key.</returns>
        public IEnumerable<StringEntry<TValue>> Get(String prefix)
        {
            if (prefix is null)
            {
                throw new ArgumentNullException(nameof(prefix));
            }

            return Trie.Get(prefix).Select(entry => new StringEntry<TValue>(new String(entry.Key.ToArray()), entry.Value));
        }

        /// <summary>
        /// Gets the value associated with the specified key.
        /// </summary>
        /// <returns>
        /// true if the object that implements <see cref="T:System.Collections.Generic.IDictionary`2"/> contains an element with the specified key; otherwise, false.
        /// </returns>
        /// <param name="key">The key whose value to get.</param>
        /// <param name="value">When this method returns, the value associated with the specified key, if the key is found; otherwise, the default value for the type of the <paramref name="value"/> parameter. This parameter is passed uninitialized.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="key"/> is null.</exception>
        public Boolean TryGetValue(String key, [MaybeNullWhen(false)] out TValue value)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return Trie.TryGetValue(key, out value);
        }

        /// <summary>
        /// Adds an element with the provided charKey and value to the <see cref="StringTrie{TValue}"/>.
        /// </summary>
        /// <param name="key">The object to use as the charKey of the element to add.</param>
        /// <param name="value">The object to use as the value of the element to add.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="key"/> is null.</exception>
        /// <exception cref="T:System.ArgumentException">An element with the same charKey already exists in the <see cref="StringTrie{TValue}"/>.</exception>
        public void Add(String key, TValue value)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            Trie.Add(key, value);
        }

        void ICollection<KeyValuePair<String, TValue>>.Add(KeyValuePair<String, TValue> item)
        {
            Add(item.Key, item.Value);
        }

        /// <summary>
        /// Adds the elements of the specified collection to the <see cref="StringTrie{TValue}"/>.
        /// </summary>
        /// <param name="collection">The collection whose elements should be added to the <see cref="StringTrie{TValue}"/>. The items should have unique keys.</param>
        /// <exception cref="T:System.ArgumentException">An element with the same charKey already exists in the <see cref="StringTrie{TValue}"/>.</exception>
        public void AddRange(IEnumerable<StringEntry<TValue>> collection)
        {
            foreach (StringEntry<TValue> item in collection)
            {
                Trie.Add(item.Key, item.Value);
            }
        }

        /// <summary>
        /// Removes the element with the specified charKey from the <see cref="T:System.Collections.Generic.IDictionary`2"/>.
        /// </summary>
        /// <returns>
        /// true if the element is successfully removed; otherwise, false.  This method also returns false if <paramref name="key"/> was not found in the original <see cref="T:System.Collections.Generic.IDictionary`2"/>.
        /// </returns>
        /// <param name="key">The charKey of the element to remove.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="key"/> is null.</exception>
        /// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.IDictionary`2"/> is read-only.</exception>
        public Boolean Remove(String key)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return Trie.Remove(key);
        }

        Boolean ICollection<KeyValuePair<String, TValue>>.Remove(KeyValuePair<String, TValue> item)
        {
            return ((IDictionary<IEnumerable<Char>, TValue>) Trie).Remove(new KeyValuePair<IEnumerable<Char>, TValue>(item.Key, item.Value));
        }

        /// <summary>
        /// Removes all items from the <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.ICollection`1"/> is read-only. </exception>
        public void Clear()
        {
            Trie.Clear();
        }

        public void CopyTo(KeyValuePair<String, TValue>[] array, Int32 arrayIndex)
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            KeyValuePair<String, TValue>[] entries = Trie.Select(node => new KeyValuePair<String, TValue>(new String(node.Key.ToArray()), node.Value)).ToArray();
            Array.Copy(entries, 0, array, arrayIndex, Count);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        public IEnumerator<KeyValuePair<String, TValue>> GetEnumerator()
        {
            return Trie.Select(node => new KeyValuePair<String, TValue>(new String(node.Key.ToArray()), node.Value)).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Gets or sets the element with the specified key.
        /// </summary>
        /// <returns>
        /// The element with the specified key.
        /// </returns>
        /// <param name="key">The key of the element to get or set.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="key"/> is null.</exception>
        /// <exception cref="T:System.Collections.Generic.KeyNotFoundException">The property is retrieved and <paramref name="key"/> is not found.</exception>
        public TValue this[String key]
        {
            get
            {
                if (key is null)
                {
                    throw new ArgumentNullException(nameof(key));
                }

                return Trie[key];
            }

            set
            {
                if (key is null)
                {
                    throw new ArgumentNullException(nameof(key));
                }

                Trie[key] = value;
            }
        }
    }
}