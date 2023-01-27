// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Serialization;
using NetExtender.NewtonSoft.Types.Trees;
using NetExtender.Types.Trees.Interfaces;
using NetExtender.Utilities.Types;
using Newtonsoft.Json;

namespace NetExtender.Types.Trees
{
    [JsonConverter(typeof(DictionaryTreeJsonConverter<,>))]
    public class DictionaryTree<TKey, TValue> : Dictionary<TKey, IDictionaryTreeNode<TKey, TValue>>, IDictionaryTree<TKey, TValue>, IReadOnlyDictionaryTree<TKey, TValue> where TKey : notnull
    {
        [JsonIgnore]
        private IDictionaryTreeNode<TKey, TValue>? _node;

        [JsonIgnore]
        public IDictionaryTreeNode<TKey, TValue> Node
        {
            get
            {
                return _node ??= new DictionaryTreeNode<TKey, TValue>(this, Comparer);
            }
        }

        [JsonIgnore]
        public Int64 FullCount
        {
            get
            {
                return Node.FullCount;
            }
        }

        [JsonIgnore]
        public Boolean IsEmpty
        {
            get
            {
                return Node.IsEmpty;
            }
        }

        [JsonIgnore]
        public Boolean HasValue
        {
            get
            {
                return Node.HasValue;
            }
        }

        [JsonIgnore]
        public Boolean HasTree
        {
            get
            {
                return Node.HasTree;
            }
        }

        [JsonIgnore]
        public Boolean TreeIsEmpty
        {
            get
            {
                return Node.TreeIsEmpty;
            }
        }

        [JsonConstructor]
        public DictionaryTree()
        {
        }

        public DictionaryTree(IDictionary<TKey, IDictionaryTreeNode<TKey, TValue>> dictionary)
            : base(dictionary)
        {
        }

        public DictionaryTree(IDictionary<TKey, IDictionaryTreeNode<TKey, TValue>> dictionary, IEqualityComparer<TKey>? comparer)
            : base(dictionary, comparer)
        {
        }

        public DictionaryTree(Dictionary<TKey, DictionaryTreeNode<TKey, TValue>> dictionary)
            : this(dictionary ?? throw new ArgumentNullException(nameof(dictionary)), dictionary.Comparer)
        {
        }

        public DictionaryTree(Dictionary<TKey, DictionaryTreeNode<TKey, TValue>> dictionary, IEqualityComparer<TKey>? comparer)
            : this(dictionary is not null ? dictionary.Select(item => new KeyValuePair<TKey, IDictionaryTreeNode<TKey, TValue>>(item.Key, item.Value))
                : throw new ArgumentNullException(nameof(dictionary)), comparer)
        {
        }

        public DictionaryTree(IEnumerable<KeyValuePair<TKey, IDictionaryTreeNode<TKey, TValue>>> collection)
            : base(collection)
        {
        }

        public DictionaryTree(IEnumerable<KeyValuePair<TKey, IDictionaryTreeNode<TKey, TValue>>> collection, IEqualityComparer<TKey>? comparer)
            : base(collection, comparer)
        {
        }

        public DictionaryTree(IEqualityComparer<TKey>? comparer)
            : base(comparer)
        {
        }

        public DictionaryTree(Int32 capacity)
            : base(capacity)
        {
        }

        public DictionaryTree(Int32 capacity, IEqualityComparer<TKey>? comparer)
            : base(capacity, comparer)
        {
        }

        protected DictionaryTree(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        protected virtual IDictionaryTreeNode<TKey, TValue> CreateNode()
        {
            return new DictionaryTreeNode<TKey, TValue>(Comparer);
        }

        public Boolean ContainsKey(TKey key, IEnumerable<TKey>? sections)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return GetChild(key, sections) is not null;
        }

        public Boolean ContainsKey(TKey key, params TKey[]? sections)
        {
            return ContainsKey(key, (IEnumerable<TKey>?) sections);
        }

        public IDictionaryTreeNode<TKey, TValue>? GetChild(TKey key)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            TryGetValue(key, out IDictionaryTreeNode<TKey, TValue>? value);
            return value;
        }

        public IDictionaryTreeNode<TKey, TValue>? GetChild(TKey key, IEnumerable<TKey>? sections)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            IDictionaryTreeNode<TKey, TValue>? node = GetChildSection(sections);

            if (node is null)
            {
                return null;
            }

            node.TryGetValue(key, out IDictionaryTreeNode<TKey, TValue>? value);
            return value;
        }

        public IDictionaryTreeNode<TKey, TValue>? GetChild(TKey key, params TKey[]? sections)
        {
            return GetChild(key, (IEnumerable<TKey>?) sections);
        }

        public IDictionaryTreeNode<TKey, TValue>? GetChildSection(IEnumerable<TKey>? sections)
        {
            if (sections is null)
            {
                return null;
            }

            IDictionaryTreeNode<TKey, TValue>? node = Node;
            return sections.WhereNotNull().Any(section => !node.TryGetValue(section, out node)) ? null : node;
        }

        public IDictionaryTreeNode<TKey, TValue>? GetChildSection(params TKey[]? sections)
        {
            return GetChildSection((IEnumerable<TKey>?) sections);
        }

        public void Add(TKey key, TValue value)
        {
            if (!TryAdd(key, value))
            {
                throw new ArgumentException($"An item with the same key has already been added. Key: {key}");
            }
        }

        public void Add(TKey key, TValue value, IEnumerable<TKey>? sections)
        {
            if (!TryAdd(key, value, sections))
            {
                throw new ArgumentException($"An item with the same key has already been added. Key: {key}");
            }
        }

        public void Add(TKey key, TValue value, params TKey[]? sections)
        {
            Add(key, value, (IEnumerable<TKey>?) sections);
        }

        public Boolean TryAdd(TKey key, TValue value)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (ContainsKey(key))
            {
                return false;
            }

            IDictionaryTreeNode<TKey, TValue> node = CreateNode();
            node.Value = value;
            Add(key, node);
            return true;
        }

        public Boolean TryAdd(TKey key, TValue value, IEnumerable<TKey>? sections)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            sections = sections.AsIImmutableList();

            if (ContainsKey(key, sections))
            {
                return false;
            }

            this[key, sections].Value = value;
            return true;
        }

        public Boolean TryAdd(TKey key, TValue value, params TKey[]? sections)
        {
            return TryAdd(key, value, (IEnumerable<TKey>?) sections);
        }

        public TValue? GetValue(TKey key)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (!TryGetValue(key, out IDictionaryTreeNode<TKey, TValue>? node))
            {
                throw new KeyNotFoundException();
            }

            return node.Value;
        }

        public TValue? GetValue(TKey key, IEnumerable<TKey>? sections)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return (GetChild(key, sections) ?? throw new KeyNotFoundException()).Value;
        }

        public TValue? GetValue(TKey key, params TKey[]? sections)
        {
            return GetValue(key, (IEnumerable<TKey>?) sections);
        }

        public Boolean Remove(TKey key, out TValue? value)
        {
            if (!Remove(key, out IDictionaryTreeNode<TKey, TValue>? node))
            {
                value = default;
                return false;
            }

            value = node.Value;
            return true;
        }

        public new Boolean Remove(TKey key, [MaybeNullWhen(false)] out IDictionaryTreeNode<TKey, TValue> value)
        {
            return base.Remove(key, out value);
        }

        public Boolean Remove(TKey key, IEnumerable<TKey>? sections)
        {
            return Remove(key, sections, out _);
        }

        public Boolean Remove(TKey key, params TKey[]? sections)
        {
            return Remove(key, out _, sections);
        }

        public Boolean Remove(TKey key, IEnumerable<TKey>? sections, [MaybeNullWhen(false)] out IDictionaryTreeNode<TKey, TValue> value)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            IDictionaryTreeNode<TKey, TValue>? child = GetChildSection(sections);

            if (child is not null)
            {
                return child.Tree.Remove(key, out value);
            }

            value = default;
            return false;
        }

        public Boolean Remove(TKey key, [MaybeNullWhen(false)] out IDictionaryTreeNode<TKey, TValue> value, params TKey[]? sections)
        {
            return Remove(key, sections, out value);
        }

        public Boolean Purge()
        {
            return Keys.Aggregate(false, (current, key) => current | Purge(key));
        }

        // ReSharper disable once CognitiveComplexity
        public Boolean Purge(TKey key)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            IDictionaryTreeNode<TKey, TValue>? node = GetChild(key);

            if (node is null)
            {
                return false;
            }

            if (node.TreeIsEmpty)
            {
                if (!node.Value.IsDefault())
                {
                    return false;
                }

                Remove(key);
                return true;
            }

            Boolean successful = false;
            foreach ((TKey dictionarykey, IDictionaryTreeNode<TKey, TValue> dictionary) in node)
            {
                if (dictionary.Count <= 0 && dictionary.Value.IsDefault())
                {
                    successful |= node.Tree.Remove(dictionarykey);
                    continue;
                }

                if (dictionary.Count > 0)
                {
                    successful |= dictionary.Purge();
                }
            }

            return successful;
        }

        public Boolean Purge(TKey key, IEnumerable<TKey> sections)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            IDictionaryTreeNode<TKey, TValue>? node = GetChildSection(sections);

            if (node is null || node.Count <= 0)
            {
                return false;
            }

            return node.Purge(key);
        }

        public Boolean Purge(TKey key, params TKey[] sections)
        {
            return Purge(key, (IEnumerable<TKey>) sections);
        }

        private IEnumerable<DictionaryTreeEntry<TKey, TValue>> DumpInternal(IEnumerable<TKey>? sections)
        {
            ImmutableArray<TKey> section = sections.AsImmutableArray();

            foreach ((TKey key, IDictionaryTreeNode<TKey, TValue> node) in this)
            {
                if (node.HasValue)
                {
                    yield return new DictionaryTreeEntry<TKey, TValue>(key, node.Value, section);
                }

                DictionaryTreeEntry<TKey, TValue>[]? dump = node.Dump(section.Add(key));

                if (dump is null)
                {
                    continue;
                }

                foreach (DictionaryTreeEntry<TKey, TValue> entry in dump)
                {
                    yield return entry;
                }
            }
        }

        public FlattenDictionaryTreeEntry<TKey, TValue>[]? Flatten()
        {
            return Flatten(FlattenDictionaryTreeEntry<TKey, TValue>.DefaultSeparator);
        }

        public FlattenDictionaryTreeEntry<TKey, TValue>[]? Flatten(String? separator)
        {
            return Dump()?.Where(entry => entry.Value is not null).Select(entry => entry.Flatten(separator)).OrderBy(entry => entry.Section).ThenBy(entry => entry.Key).ToArray();
        }
        
        public FlattenDictionaryTreeEntry<TKey, TValue>[]? Flatten(params TKey[]? sections)
        {
            return Flatten(FlattenDictionaryTreeEntry<TKey, TValue>.DefaultSeparator, sections);
        }

        public FlattenDictionaryTreeEntry<TKey, TValue>[]? Flatten(String? separator, params TKey[]? sections)
        {
            return Dump(sections)?.Where(entry => entry.Value is not null).Select(entry => entry.Flatten(separator)).OrderBy(entry => entry.Section).ThenBy(entry => entry.Key).ToArray();
        }
        
        public FlattenDictionaryTreeEntry<TKey, TValue>[]? Flatten(IEnumerable<TKey>? sections)
        {
            return Flatten(FlattenDictionaryTreeEntry<TKey, TValue>.DefaultSeparator, sections);
        }

        public FlattenDictionaryTreeEntry<TKey, TValue>[]? Flatten(String? separator, IEnumerable<TKey>? sections)
        {
            return Dump(sections)?.Where(entry => entry.Value is not null).Select(entry => entry.Flatten(separator)).OrderBy(entry => entry.Section).ThenBy(entry => entry.Key).ToArray();
        }

        public DictionaryTreeEntry<TKey, TValue>[]? Dump()
        {
            return Dump(null);
        }

        public DictionaryTreeEntry<TKey, TValue>[]? Dump(params TKey[]? sections)
        {
            return Dump((IEnumerable<TKey>?) sections);
        }

        public DictionaryTreeEntry<TKey, TValue>[]? Dump(IEnumerable<TKey>? sections)
        {
            try
            {
                return DumpInternal(sections).ToArray();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public new IDictionaryTreeNode<TKey, TValue> this[TKey key]
        {
            get
            {
                if (key is null)
                {
                    throw new ArgumentNullException(nameof(key));
                }

                if (TryGetValue(key, out IDictionaryTreeNode<TKey, TValue>? node))
                {
                    return node;
                }

                node = CreateNode();
                base[key] = node;
                return node;
            }
            set
            {
                if (key is null)
                {
                    throw new ArgumentNullException(nameof(key));
                }

                base[key] = value;
            }
        }

        public IDictionaryTreeNode<TKey, TValue> this[TKey key, params TKey[]? sections]
        {
            get
            {
                return this[key, (IEnumerable<TKey>?) sections];
            }
            set
            {
                this[key, (IEnumerable<TKey>?) sections] = value;
            }
        }

        public IDictionaryTreeNode<TKey, TValue> this[TKey key, IEnumerable<TKey>? sections]
        {
            get
            {
                if (key is null)
                {
                    throw new ArgumentNullException(nameof(key));
                }

                IDictionaryTreeNode<TKey, TValue> node = Node;

                if (sections is null)
                {
                    return node[key];
                }

                node = sections.WhereNotNull().Aggregate(node, (current, section) => current[section]);

                return node[key];
            }
            set
            {
                if (key is null)
                {
                    throw new ArgumentNullException(nameof(key));
                }

                IDictionaryTreeNode<TKey, TValue> node = Node;

                if (sections is null)
                {
                    node[key] = value;
                    return;
                }

                node = sections.WhereNotNull().Aggregate(node, (current, section) => current[section]);

                node[key] = value;
            }
        }
    }
}