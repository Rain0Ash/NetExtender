// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NetExtender.Initializer.Types.Tries
{
    public class StringTrieSet : ICollection<String>
    {
        protected TrieSet<Char> Trie { get; }

        Boolean ICollection<String>.IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public Int32 Count
        {
            get
            {
                return Trie.Count;
            }
        }

        public StringTrieSet()
            : this(null)
        {
        }

        public StringTrieSet(IEqualityComparer<Char>? comparer)
        {
            Trie = new TrieSet<Char>(comparer);
        }

        public IEnumerable<String> Get(String prefix)
        {
            if (prefix is null)
            {
                throw new ArgumentNullException(nameof(prefix));
            }

            return Trie.Get(prefix).Select(entry => new String(entry.ToArray()));
        }

        public Boolean Contains(String item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return Trie.Contains(item);
        }

        public void Add(String item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            Trie.Add(item);
        }

        public void AddRange(IEnumerable<String> item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            Trie.AddRange(item);
        }

        public Boolean Remove(String item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return Trie.Remove(item);
        }

        public void Clear()
        {
            Trie.Clear();
        }

        public void CopyTo(String[] array, Int32 arrayIndex)
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            String[] entries = Trie.Select(item => new String(item.ToArray())).ToArray();
            Array.Copy(entries, 0, array, arrayIndex, Count);
        }

        public IEnumerator<String> GetEnumerator()
        {
            return Trie.Select(item => new String(item.ToArray())).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}