// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using NetExtender.Types.Trees.Interfaces;
using NetExtender.Utils.Types;
using Newtonsoft.Json;

namespace NetExtender.Types.Trees
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, MissingMemberHandling = MissingMemberHandling.Ignore, ItemNullValueHandling = NullValueHandling.Ignore)]
    public class DictionaryTreeNode<TKey, TValue> : IDictionaryTreeNode<TKey, TValue>, IReadOnlyDictionaryTreeNode<TKey, TValue>
    {
        [JsonProperty(Order = 0)]
        public TValue Value { get; set; }

        [JsonProperty(PropertyName = nameof(Tree), Order = 1)]
        private IDictionaryTree<TKey, TValue> _tree;
        
        [JsonIgnore]
        public IDictionaryTree<TKey, TValue> Tree
        {
            get
            {
                return _tree ??= CreateTree();
            }
        }

        public IEqualityComparer<TKey> Comparer { get; }
        
        [JsonIgnore]
        public Boolean IsReadOnly
        {
            get
            {
                return !HasTree || Tree.IsReadOnly();
            }
        }

        [JsonIgnore]
        public ICollection<TKey> Keys
        {
            get
            {
                return HasTree ? Tree.Keys : ImmutableList<TKey>.Empty;
            }
        }

        [JsonIgnore]
        public ICollection<IDictionaryTreeNode<TKey, TValue>> Values
        {
            get
            {
                return HasTree ? Tree.Values : ImmutableList<IDictionaryTreeNode<TKey, TValue>>.Empty;
            }
        }

        [JsonIgnore]
        public Int32 Count
        {
            get
            {
                return HasTree ? Tree.Count : 0;
            }
        }

        [JsonIgnore]
        public Int64 FullCount
        {
            get
            {
                if (_tree is null || Tree.Count <= 0)
                {
                    return 0;
                }

                try
                {
                    return Tree.Values.Sum(node => node.FullCount);
                }
                catch (OverflowException)
                {
                    return Int64.MaxValue;
                }
            }
        }

        [JsonIgnore]
        public Boolean IsEmpty
        {
            get
            {
                return !HasValue && !TreeIsEmpty;
            }
        }

        [JsonIgnore]
        public Boolean HasValue
        {
            get
            {
                return Value.IsNotDefault();
            }
        }

        [JsonIgnore]
        public Boolean HasTree
        {
            get
            {
                return _tree is not null;
            }
        }

        [JsonIgnore]
        public Boolean TreeIsEmpty
        {
            get
            {
                return !HasTree || Tree.Count <= 0 || Tree.Values.All(node => node.IsEmpty);
            }
        }

        [JsonConstructor]
        protected DictionaryTreeNode()
        {
        }
        
        public DictionaryTreeNode(IEqualityComparer<TKey> comparer = default)
            : this(default, comparer)
        {
        }
        
        public DictionaryTreeNode(IDictionaryTree<TKey, TValue> tree, IEqualityComparer<TKey> comparer = default)
            : this(default, tree, comparer)
        {
        }
        
        public DictionaryTreeNode(TValue value = default, IDictionaryTree<TKey, TValue> tree = default, IEqualityComparer<TKey> comparer = default)
        {
            Value = value;
            _tree = tree;
            Comparer = comparer ?? _tree?.Comparer;
        }

        public virtual IDictionaryTree<TKey, TValue> CreateTree()
        {
            return new DictionaryTree<TKey, TValue>(Comparer);
        }
        
        protected virtual IDictionaryTreeNode<TKey, TValue> CreateNode()
        {
            return new DictionaryTreeNode<TKey, TValue>(Comparer);
        }
        
        public void Add(KeyValuePair<TKey, IDictionaryTreeNode<TKey, TValue>> item)
        {
            Tree.Add(item);
        }

        public void Add(TKey key, IDictionaryTreeNode<TKey, TValue> value)
        {
            Tree.Add(key, value);
        }

        public Boolean Contains(KeyValuePair<TKey, IDictionaryTreeNode<TKey, TValue>> item)
        {
            return HasTree && Enumerable.Contains(Tree, item);
        }

        public Boolean ContainsKey(TKey key)
        {
            return HasTree && Tree.ContainsKey(key);
        }
        
        public Boolean ContainsKey(TKey key, params TKey[] sections)
        {
            return GetChild(key, sections) is not null;
        }
        
        public IDictionaryTreeNode<TKey, TValue> GetChild(TKey key)
        {
            TryGetValue(key, out IDictionaryTreeNode<TKey, TValue> value);
            return value;
        }

        public IDictionaryTreeNode<TKey, TValue> GetChild(TKey key, params TKey[] sections)
        {
            IDictionaryTreeNode<TKey, TValue> node = GetChildSection(sections);

            if (node is null)
            {
                return null;
            }

            node.TryGetValue(key, out IDictionaryTreeNode<TKey, TValue> value);
            return value;
        }

        public IDictionaryTreeNode<TKey, TValue> GetChildSection(IEnumerable<TKey> sections)
        {
            IDictionaryTreeNode<TKey, TValue> node = this;
            return sections.Any(section => !node.TryGetValue(section, out node)) ? null : node;
        }

        public IDictionaryTreeNode<TKey, TValue> GetChildSection(params TKey[] sections)
        {
            return GetChildSection(sections.AsEnumerable());
        }

        public Boolean TryGetValue(TKey key, out IDictionaryTreeNode<TKey, TValue> value)
        {
            if (HasTree)
            {
                return Tree.TryGetValue(key, out value);
            }

            value = default;
            return false;
        }

        public Boolean Remove(TKey key)
        {
            return HasTree && Tree.Remove(key);
        }
        
        public Boolean Remove(KeyValuePair<TKey, IDictionaryTreeNode<TKey, TValue>> item)
        {
            return HasTree && Tree.Remove(item);
        }
        
        public Boolean Remove(TKey key, params TKey[] sections)
        {
            return Remove(key, sections, out _);
        }

        public Boolean Remove(TKey key, IEnumerable<TKey> sections, out IDictionaryTreeNode<TKey, TValue> value)
        {
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

        public void Clear()
        {
            if (HasTree)
            {
                Tree.Clear();
            }
        }
        
        public void CopyTo(KeyValuePair<TKey, IDictionaryTreeNode<TKey, TValue>>[] array, Int32 index)
        {
            if (!HasTree)
            {
                throw new InvalidOperationException();
            }
            
            Tree.CopyTo(array, index);
        }
        
        public void RemoveEmpty()
        {
            if (_tree is null || Tree.Count <= 0)
            {
                return;
            }

            foreach ((TKey key, IDictionaryTreeNode<TKey, TValue> value) in Tree)
            {
                if (value.IsEmpty)
                {
                    Tree.Remove(key);
                }
                else
                {
                    value.RemoveEmpty();
                }
            }
        }

        public void RemoveEmpty(TKey key)
        {
            RemoveEmpty(key, Array.Empty<TKey>());
        }

        public void RemoveEmpty(TKey key, params TKey[] sections)
        {
            IDictionaryTreeNode<TKey, TValue> node = GetChildSection(sections);
            
            if (node is null || node.Count <= 0)
            {
                return;
            }

            node.RemoveEmpty(key);
        }

        public IDictionaryTreeNode<TKey, TValue> this[TKey key]
        {
            get
            {
                if (!ContainsKey(key))
                {
                    Tree[key] = CreateNode();
                }

                return Tree[key];
            }
            set
            {
                Tree[key] = value;
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

        public IDictionaryTreeNode<TKey, TValue> this[TKey key, IEnumerable<TKey> sections]
        {
            get
            {
                IDictionaryTreeNode<TKey, TValue> node = this;

                if (sections is null)
                {
                    return node[key];
                }

                node = sections.Aggregate(node, (current, section) => current[section]);

                return node[key];
            }
            set
            {
                IDictionaryTreeNode<TKey, TValue> node = this;

                if (sections is null)
                {
                    node[key] = value;
                    return;
                }

                node = sections.Aggregate(node, (current, section) => current[section]);

                node[key] = value;
            }
        }
        
        public IEnumerator<KeyValuePair<TKey, IDictionaryTreeNode<TKey, TValue>>> GetEnumerator()
        {
            return HasTree ? Tree.GetEnumerator() : EnumerableUtils.GetEmptyEnumerator<KeyValuePair<TKey, IDictionaryTreeNode<TKey, TValue>>>();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}