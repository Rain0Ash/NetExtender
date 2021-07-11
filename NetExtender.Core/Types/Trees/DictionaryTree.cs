// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.Serialization;
using NetExtender.Utils.Types;
using NetExtender.Types.Trees.Interfaces;
using NetExtender.Types.Trees.Json;
using Newtonsoft.Json;

namespace NetExtender.Types.Trees
{
    [JsonConverter(typeof(DictionaryTreeJsonConverter))]
    public class DictionaryTree<TKey, TValue> : Dictionary<TKey, IDictionaryTreeNode<TKey, TValue?>>, IDictionaryTree<TKey, TValue?>, IReadOnlyDictionaryTree<TKey, TValue?> where TKey : notnull
    {
        [JsonIgnore]
        private IDictionaryTreeNode<TKey, TValue?>? _node;
        
        [JsonIgnore]
        private IDictionaryTreeNode<TKey, TValue?> Node
        {
            get
            {
                return _node ??= new DictionaryTreeNode<TKey, TValue?>(this);
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

        public DictionaryTree(IDictionary<TKey, IDictionaryTreeNode<TKey, TValue?>> dictionary)
            : base(dictionary)
        {
        }

        public DictionaryTree(IDictionary<TKey, IDictionaryTreeNode<TKey, TValue?>> dictionary, IEqualityComparer<TKey>? comparer)
            : base(dictionary, comparer)
        {
        }
        
        public DictionaryTree(Dictionary<TKey, DictionaryTreeNode<TKey, TValue?>> dictionary)
            : this(dictionary, dictionary?.Comparer)
        {
        }

        public DictionaryTree(Dictionary<TKey, DictionaryTreeNode<TKey, TValue?>> dictionary, IEqualityComparer<TKey>? comparer)
            : this(dictionary?.Select(item => new KeyValuePair<TKey, IDictionaryTreeNode<TKey, TValue?>>(item.Key, item.Value))
                ?? throw new ArgumentNullException(nameof(dictionary)), comparer)
        {
        }

        public DictionaryTree(IEnumerable<KeyValuePair<TKey, IDictionaryTreeNode<TKey, TValue?>>> collection)
            : base(collection)
        {
        }

        public DictionaryTree(IEnumerable<KeyValuePair<TKey, IDictionaryTreeNode<TKey, TValue?>>> collection, IEqualityComparer<TKey>? comparer)
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

        protected virtual IDictionaryTreeNode<TKey, TValue?> CreateNode()
        {
            return new DictionaryTreeNode<TKey, TValue?>(Comparer);
        }
        
        public Boolean ContainsKey(TKey key, IEnumerable<TKey> sections)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return GetChild(key, sections) is not null;
        }
        
        public Boolean ContainsKey(TKey key, params TKey[] sections)
        {
            return ContainsKey(key, (IEnumerable<TKey>) sections);
        }

        public IDictionaryTreeNode<TKey, TValue?>? GetChild(TKey key)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            TryGetValue(key, out IDictionaryTreeNode<TKey, TValue?>? value);
            return value;
        }

        public IDictionaryTreeNode<TKey, TValue?>? GetChild(TKey key, IEnumerable<TKey> sections)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            IDictionaryTreeNode<TKey, TValue?>? node = GetChildSection(sections);

            if (node is null)
            {
                return null;
            }

            node.TryGetValue(key, out IDictionaryTreeNode<TKey, TValue?>? value);
            return value;
        }

        public IDictionaryTreeNode<TKey, TValue?>? GetChild(TKey key, params TKey[] sections)
        {
            return GetChild(key, (IEnumerable<TKey>) sections);
        }

        public IDictionaryTreeNode<TKey, TValue?>? GetChildSection(IEnumerable<TKey>? sections)
        {
            if (sections is null)
            {
                return null;
            }
            
            IDictionaryTreeNode<TKey, TValue?>? node = Node;
            return sections.WhereNotNull().Any(section => !node.TryGetValue(section, out node)) ? null : node;
        }
        
        public IDictionaryTreeNode<TKey, TValue?>? GetChildSection(params TKey[] sections)
        {
            return GetChildSection((IEnumerable<TKey>) sections);
        }

        public void Add(TKey key, TValue? value)
        {
            if (!TryAdd(key, value))
            {
                throw new ArgumentException($"An item with the same key has already been added. Key: {key}");
            }
        }

        public void Add(TKey key, IEnumerable<TKey> sections, TValue? value)
        {
            if (!TryAdd(key, sections, value))
            {
                throw new ArgumentException($"An item with the same key has already been added. Key: {key}");
            }
        }

        public void Add(TKey key, TValue? value, params TKey[] sections)
        {
            Add(key, sections, value);
        }

        public Boolean TryAdd(TKey key, TValue? value)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (ContainsKey(key))
            {
                return false;
            }

            IDictionaryTreeNode<TKey, TValue?> node = CreateNode();
            node.Value = value;
            Add(key, node);
            return true;
        }

        public Boolean TryAdd(TKey key, IEnumerable<TKey> sections, TValue? value)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            IImmutableList<TKey> imsections = sections.AsIImmutableList();

            if (ContainsKey(key, imsections))
            {
                return false;
            }

            this[key, imsections].Value = value;
            return true;
        }

        public Boolean TryAdd(TKey key, TValue value, params TKey[] sections)
        {
            return TryAdd(key, sections, value);
        }

        public TValue GetValue(TKey key)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (!TryGetValue(key, out IDictionaryTreeNode<TKey, TValue> node))
            {
                throw new KeyNotFoundException();
            }

            return node.Value;
        }
        
        public TValue GetValue(TKey key, IEnumerable<TKey> sections)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return (GetChild(key, sections) ?? throw new KeyNotFoundException()).Value;
        }

        public TValue GetValue(TKey key, params TKey[] sections)
        {
            return GetValue(key, (IEnumerable<TKey>) sections);
        }
        
        public Boolean Remove(TKey key, IEnumerable<TKey> sections)
        {
            return Remove(key, sections, out _);
        }

        public Boolean Remove(TKey key, params TKey[] sections)
        {
            return Remove(key, out _, sections);
        }

        public Boolean Remove(TKey key, IEnumerable<TKey> sections, out IDictionaryTreeNode<TKey, TValue> value)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            IDictionaryTreeNode<TKey, TValue> child = GetChildSection(sections);

            if (child is not null)
            {
                return child.Tree.Remove(key, out value);
            }

            value = default;
            return false;
        }
        
        public Boolean Remove(TKey key, out IDictionaryTreeNode<TKey, TValue> value, params TKey[] sections)
        {
            return Remove(key, sections, out value);
        }

        public void RemoveEmpty()
        {
            foreach (TKey key in Keys)
            {
                RemoveEmpty(key);
            }
        }
        
        public void RemoveEmpty(TKey key)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            IDictionaryTreeNode<TKey, TValue> node = GetChild(key);
            
            if (node is null)
            {
                return;
            }

            if (node.FullCount <= 0 && node.Value.IsDefault())
            {
                Remove(key);
                return;
            }

            if (node.FullCount <= 0)
            {
                return;
            }

            foreach ((TKey dictkey, IDictionaryTreeNode<TKey, TValue> dict) in node)
            {
                if (dict is null || dict.Count <= 0 && dict.Value.IsDefault())
                {
                    node.Tree.Remove(dictkey);
                }
                else if (dict.Count > 0)
                {
                    dict.RemoveEmpty();
                }
            }
        }

        public void RemoveEmpty(TKey key, IEnumerable<TKey> sections)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            IDictionaryTreeNode<TKey, TValue> node = GetChildSection(sections);
            
            if (node is null || node.Count <= 0)
            {
                return;
            }

            node.RemoveEmpty(key);
        }

        public void RemoveEmpty(TKey key, params TKey[] sections)
        {
            RemoveEmpty(key, (IEnumerable<TKey>) sections);
        }

        public new IDictionaryTreeNode<TKey, TValue> this[TKey key]
        {
            get
            {
                if (key is null)
                {
                    throw new ArgumentNullException(nameof(key));
                }

                if (TryGetValue(key, out IDictionaryTreeNode<TKey, TValue> node))
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

        public IDictionaryTreeNode<TKey, TValue> this[TKey key, params TKey[] sections]
        {
            get
            {
                return this[key, (IEnumerable<TKey>) sections];
            }
            set
            {
                this[key, (IEnumerable<TKey>) sections] = value;
            }
        }
        
        public IDictionaryTreeNode<TKey, TValue?>? this[TKey key, IEnumerable<TKey> sections]
        {
            get
            {
                if (key is null)
                {
                    throw new ArgumentNullException(nameof(key));
                }

                IDictionaryTreeNode<TKey, TValue?>? node = Node;

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

                IDictionaryTreeNode<TKey, TValue?>? node = Node;

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