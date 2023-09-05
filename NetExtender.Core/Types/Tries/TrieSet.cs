// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using NetExtender.Utilities.Types;

namespace NetExtender.Initializer.Types.Tries
{
    public class TrieSet<T> : ICollection<IEnumerable<T>> where T : notnull
    {
        private TrieNode Root { get; }
        private IEqualityComparer<T> Comparer { get; }

        Boolean ICollection<IEnumerable<T>>.IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public Int32 Count { get; private set; }

        public TrieSet()
            : this(null)
        {

        }

        public TrieSet(IEqualityComparer<T>? comparer)
        {
            Root = new TrieNode(default!, comparer);
            Comparer = comparer ?? EqualityComparer<T>.Default;
        }

        public Boolean Contains(IEnumerable<T> item)
        {
            TrieNode? node = GetNode(item);
            return node is not null && node.IsTerminal;
        }

        /// <summary>
        /// Gets items by key prefix.
        /// </summary>
        /// <param name="prefix">Key prefix.</param>
        /// <returns>Collection of <see cref="T"/> items.</returns>
        public IEnumerable<IEnumerable<T>> Get(IEnumerable<T> prefix)
        {
            if (prefix is null)
            {
                throw new ArgumentNullException(nameof(prefix));
            }

            TrieNode? node = Root;
            return prefix.All(item => node.Children.TryGetValue(item, out node)) ? Get(node) : Array.Empty<IEnumerable<T>>();
        }

        private static IEnumerable<IEnumerable<T>> Get(TrieNode node)
        {
            if (node is null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            Stack<TrieNode> stack = new Stack<TrieNode>();
            TrieNode? current = node;

            while (current is not null || stack.Count > 0)
            {
                if (current is null)
                {
                    current = stack.Pop();
                    continue;
                }

                if (current.IsTerminal && current.Item is not null)
                {
                    yield return current.Item;
                }

                using IEnumerator<KeyValuePair<T, TrieNode>> enumerator = current.Children.GetEnumerator();
                current = enumerator.MoveNext() ? enumerator.Current.Value : null;

                while (enumerator.MoveNext())
                {
                    stack.Push(enumerator.Current.Value);
                }
            }
        }

        private TrieNode? GetNode(IEnumerable<T> key)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            TrieNode? node = Root;
            return key.All(item => node.Children.TryGetValue(item, out node)) ? node : null;
        }

        internal Boolean TryGetNode(IEnumerable<T> key, [MaybeNullWhen(false)] out TrieNode node)
        {
            node = GetNode(key);
            return node is not null && node.IsTerminal;
        }

        private static IEnumerable<TrieNode> GetAllNodes(TrieNode node)
        {
            if (node is null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            foreach (KeyValuePair<T, TrieNode> child in node.Children)
            {
                if (child.Value.IsTerminal)
                {
                    yield return child.Value;
                }

                foreach (TrieNode item in GetAllNodes(child.Value))
                {
                    if (item.IsTerminal)
                    {
                        yield return item;
                    }
                }
            }
        }

        /// <summary>
        /// Gets an item by key.
        /// </summary>
        /// <param name="key">Key.</param>
        /// <param name="result">Output item.</param>
        /// <returns>true if trie contains an element with the specified key; otherwise, false.</returns>
        public Boolean TryGetItem(IEnumerable<T> key, [MaybeNullWhen(false)] out IEnumerable<T> result)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            TrieNode? node = GetNode(key);
            result = null;

            if (node is null || !node.IsTerminal)
            {
                return false;
            }

            result = node.Item;
            return result is not null;
        }

        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public void Add(IEnumerable<T> key)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            TrieNode node = key.Aggregate(Root, AddItem);

            if (node.IsTerminal)
            {
                throw new ArgumentException($"An element with the same key already exists: '{key}'", nameof(key));
            }

            node.IsTerminal = true;
            node.Item = key;
            Count++;
        }

        private TrieNode AddItem(TrieNode node, T key)
        {
            if (node is null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            if (node.Children.TryGetValue(key, out TrieNode? child))
            {
                return child;
            }

            child = new TrieNode(key, Comparer) { Parent = node };
            node.Children.Add(key, child);
            
            return child;
        }

        public void AddRange(IEnumerable<IEnumerable<T>> collection)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            foreach (IEnumerable<T> item in collection)
            {
                Add(item);
            }
        }

        public Boolean Remove(IEnumerable<T> key)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            TrieNode? node = GetNode(key);
            if (node is null || !node.IsTerminal)
            {
                return false;
            }

            RemoveNode(node);
            return true;
        }

        private void Remove(TrieNode node, T key)
        {
            if (node is null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            foreach (KeyValuePair<T, TrieNode> pair in node.Children)
            {
                if (!Comparer.Equals(key, pair.Key))
                {
                    continue;
                }

                node.Children.Remove(pair);
                return;
            }
        }

        private void Remove(TrieNode node)
        {
            if (node is null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            while (true)
            {
                node.IsTerminal = false;

                if (node.Parent is not null && node.Children.Count == 0)
                {
                    Remove(node.Parent, node.Key);

                    if (!node.Parent.IsTerminal)
                    {
                        node = node.Parent;
                        continue;
                    }
                }

                break;
            }
        }

        private void RemoveNode(TrieNode node)
        {
            if (node is null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            Remove(node);
            Count--;
        }

        public void Clear()
        {
            Root.Children.Clear();
            Count = 0;
        }

        public void CopyTo(IEnumerable<T>[] array, Int32 arrayIndex)
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            IEnumerable<T>[] entries = GetAllNodes(Root).Select(node => node.Item).WhereNotNull().ToArray();
            Array.Copy(entries, 0, array, arrayIndex, Count);
        }

        public IEnumerator<IEnumerable<T>> GetEnumerator()
        {
            return GetAllNodes(Root).Select(node => node.Item).WhereNotNull().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        internal sealed class TrieNode
        {
            public T Key { get; }
            public Boolean IsTerminal { get; set; }
            public IEnumerable<T>? Item { get; set; }
            public IDictionary<T, TrieNode> Children { get; }
            public TrieNode? Parent { get; set; }

            public TrieNode(T key, IEqualityComparer<T>? comparer)
            {
                Key = key;
                Children = new Dictionary<T, TrieNode>(comparer);
            }
        }
    }
}