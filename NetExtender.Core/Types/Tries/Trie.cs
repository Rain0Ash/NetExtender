// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace NetExtender.Types.Tries
{
    /// <summary>
    /// Implementation of trie data structure.
    /// </summary>
    /// <typeparam name="TKey">The type of keys in the trie.</typeparam>
    /// <typeparam name="TValue">The type of values in the trie.</typeparam>
    public class Trie<TKey, TValue> : IDictionary<IEnumerable<TKey>, TValue> where TKey : notnull
    {
        protected TrieSet<TKey> Set { get; }

        Boolean ICollection<KeyValuePair<IEnumerable<TKey>, TValue>>.IsReadOnly
        {
            get
            {
                return ((ICollection<IEnumerable<TKey>>) Set).IsReadOnly;
            }
        }

        public Int32 Count
        {
            get
            {
                return Set.Count;
            }
        }

        public ICollection<IEnumerable<TKey>> Keys
        {
            get
            {
                return Set.ToList();
            }
        }

        public ICollection<TValue> Values
        {
            get
            {
                return Set.Cast<Entry>().Select(entry => entry.Value).ToArray();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Trie{TKey,TValue}"/>.
        /// </summary>
        public Trie()
            : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Trie{TKey,TValue}"/>.
        /// </summary>
        /// <param name="comparer">Comparer.</param>
        public Trie(IEqualityComparer<TKey>? comparer)
        {
            Set = new TrieSet<TKey>(comparer);
        }

        public Boolean ContainsKey(IEnumerable<TKey> key)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return Set.Contains(key);
        }

        public Boolean ContainsKey(ReadOnlySpan<TKey> key)
        {
            return Set.Contains(key);
        }

        Boolean ICollection<KeyValuePair<IEnumerable<TKey>, TValue>>.Contains(KeyValuePair<IEnumerable<TKey>, TValue> item)
        {
            if (!Set.TryGetItem(item.Key, out IEnumerable<TKey>? result) || result is not Entry entry)
            {
                return false;
            }

            return EqualityComparer<TValue>.Default.Equals(item.Value, entry.Value);
        }

        /// <summary>
        /// Gets items by key prefix.
        /// </summary>
        /// <param name="prefix">Key prefix.</param>
        /// <returns>Collection of <see cref="TrieEntry{TKey, TValue}"/> items which have key which starts from specified <see cref="prefix"/>.</returns>
        public IEnumerable<TrieEntry<TKey, TValue>> Get(IEnumerable<TKey> prefix)
        {
            if (prefix is null)
            {
                throw new ArgumentNullException(nameof(prefix));
            }

            return Set.Get(prefix).Cast<Entry>().Select(entry => new TrieEntry<TKey, TValue>(entry, entry.Value));
        }
        
        public IEnumerable<TrieEntry<TKey, TValue>> Get(ReadOnlySpan<TKey> prefix)
        {
            return Set.Get(prefix).Cast<Entry>().Select(entry => new TrieEntry<TKey, TValue>(entry, entry.Value));
        }

        public Boolean TryGetValue(IEnumerable<TKey> key, [MaybeNullWhen(false)] out TValue value)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (!Set.TryGetItem(key, out IEnumerable<TKey>? result) || result is not Entry entry)
            {
                value = default;
                return false;
            }

            value = entry.Value;
            return true;
        }

        public Boolean TryGetValue(ReadOnlySpan<TKey> key, [MaybeNullWhen(false)] out TValue value)
        {
            if (!Set.TryGetItem(key, out IEnumerable<TKey>? result) || result is not Entry entry)
            {
                value = default;
                return false;
            }

            value = entry.Value;
            return true;
        }

        public void Add(IEnumerable<TKey> key, TValue value)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            Entry entry = new Entry(key) { Value = value };
            Set.Add(entry);
        }

        public void Add(ReadOnlySpan<TKey> key, TValue value)
        {
            Entry entry = new Entry(key.ToArray()) { Value = value };
            Set.Add(entry);
        }

        void ICollection<KeyValuePair<IEnumerable<TKey>, TValue>>.Add(KeyValuePair<IEnumerable<TKey>, TValue> item)
        {
            Add(item.Key, item.Value);
        }

        public Boolean Remove(IEnumerable<TKey> key)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return Set.Remove(key);
        }

        public Boolean Remove(ReadOnlySpan<TKey> key)
        {
            return Set.Remove(key);
        }

        Boolean ICollection<KeyValuePair<IEnumerable<TKey>, TValue>>.Remove(KeyValuePair<IEnumerable<TKey>, TValue> item)
        {
            if (!Set.TryGetItem(item.Key, out IEnumerable<TKey>? result) || result is not Entry entry)
            {
                return false;
            }

            return EqualityComparer<TValue>.Default.Equals(item.Value, entry.Value) && Remove(item.Key);
        }

        public void Clear()
        {
            Set.Clear();
        }

        public void CopyTo(KeyValuePair<IEnumerable<TKey>, TValue>[] array, Int32 index)
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            KeyValuePair<IEnumerable<TKey>, TValue>[] entries = Set.Cast<Entry>().Select(entry => new KeyValuePair<IEnumerable<TKey>, TValue>(entry, entry.Value)).ToArray();
            Array.Copy(entries, 0, array, index, Count);
        }

        public IEnumerator<KeyValuePair<IEnumerable<TKey>, TValue>> GetEnumerator()
        {
            return Set.Cast<Entry>().Select(entry => new KeyValuePair<IEnumerable<TKey>, TValue>(entry, entry.Value)).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public TValue this[IEnumerable<TKey> key]
        {
            get
            {
                if (key is null)
                {
                    throw new ArgumentNullException(nameof(key));
                }

                return TryGetValue(key, out TValue? result) ? result : throw new KeyNotFoundException("The given key was not present in the trie.");
            }
            set
            {
                if (key is null)
                {
                    throw new ArgumentNullException(nameof(key));
                }

                if (!Set.TryGetItem(key, out IEnumerable<TKey>? result))
                {
                    Add(key, value);
                    return;
                }

                if (result is Entry entry)
                {
                    entry.Value = value;
                }
            }
        }

        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public TValue this[ReadOnlySpan<TKey> key]
        {
            get
            {
                return TryGetValue(key, out TValue? result) ? result : throw new KeyNotFoundException("The given key was not present in the trie.");
            }
            set
            {
                if (!Set.TryGetItem(key, out IEnumerable<TKey>? result))
                {
                    Add(key, value);
                    return;
                }

                if (result is Entry entry)
                {
                    entry.Value = value;
                }
            }
        }

        private sealed class Entry : IEnumerable<TKey>
        {
            private IEnumerable<TKey> Key { get; }
            public TValue Value { get; set; } = default!;

            public Entry(IEnumerable<TKey> key)
            {
                Key = key ?? throw new ArgumentNullException(nameof(key));
            }

            public IEnumerator<TKey> GetEnumerator()
            {
                return Key.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
    }
}