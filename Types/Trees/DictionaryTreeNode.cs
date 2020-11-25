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
    public class DictionaryTreeNode<TKey, TValue> : IDictionaryTree<TKey, TValue>, IReadOnlyDictionaryTree<TKey, TValue>
    {
        public static explicit operator DictionaryTree<TKey, TValue>(DictionaryTreeNode<TKey, TValue> node)
        {
            return node.Tree;
        }
        
        [JsonProperty(Order = 0)]
        public TValue Value { get; set; }

        [JsonProperty(PropertyName = nameof(Tree), Order = 1)]
        private DictionaryTree<TKey, TValue> _tree;
        
        [JsonIgnore]
        public DictionaryTree<TKey, TValue> Tree
        {
            get
            {
                return _tree ??= new DictionaryTree<TKey, TValue>();
            }
        }

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
                if (HasTree)
                {
                    return Tree.Keys;
                }

                return ImmutableList<TKey>.Empty;
            }
        }

        [JsonIgnore]
        public ICollection<DictionaryTreeNode<TKey, TValue>> Values
        {
            get
            {
                if (HasTree)
                {
                    return Tree.Values;
                }

                return ImmutableList<DictionaryTreeNode<TKey, TValue>>.Empty;
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
        
        public DictionaryTreeNode(DictionaryTree<TKey, TValue> tree)
            : this(default, tree)
        {
        }
        
        public DictionaryTreeNode(TValue value = default, DictionaryTree<TKey, TValue> tree = default)
        {
            Value = value;
            _tree = tree;
        }
        
        public void Add(KeyValuePair<TKey, DictionaryTreeNode<TKey, TValue>> item)
        {
            Tree.Add(item);
        }

        public void Add(TKey key, DictionaryTreeNode<TKey, TValue> value)
        {
            Tree.Add(key, value);
        }

        public Boolean Contains(KeyValuePair<TKey, DictionaryTreeNode<TKey, TValue>> item)
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
        
        public DictionaryTreeNode<TKey, TValue> GetChild(TKey key)
        {
            TryGetValue(key, out DictionaryTreeNode<TKey, TValue> value);
            return value;
        }

        public DictionaryTreeNode<TKey, TValue> GetChild(TKey key, params TKey[] sections)
        {
            DictionaryTreeNode<TKey, TValue> node = GetChildSection(sections);

            if (node is null)
            {
                return null;
            }

            node.TryGetValue(key, out DictionaryTreeNode<TKey, TValue> value);
            return value;
        }

        public DictionaryTreeNode<TKey, TValue> GetChildSection(IEnumerable<TKey> sections)
        {
            DictionaryTreeNode<TKey, TValue> node = this;
            return sections.Any(section => !node.TryGetValue(section, out node)) ? null : node;
        }

        public DictionaryTreeNode<TKey, TValue> GetChildSection(params TKey[] sections)
        {
            return GetChildSection(sections.AsEnumerable());
        }

        public Boolean TryGetValue(TKey key, out DictionaryTreeNode<TKey, TValue> value)
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
        
        public Boolean Remove(KeyValuePair<TKey, DictionaryTreeNode<TKey, TValue>> item)
        {
            return HasTree && Tree.Remove(item);
        }
        
        public Boolean Remove(TKey key, params TKey[] sections)
        {
            return Remove(key, sections, out _);
        }

        public Boolean Remove(TKey key, IEnumerable<TKey> sections, out DictionaryTreeNode<TKey, TValue> value)
        {
            DictionaryTreeNode<TKey, TValue> child = GetChildSection(sections);

            if (child is not null)
            {
                return child.Tree.Remove(key, out value);
            }

            value = default;
            return false;
        }

        public Boolean Remove(TKey key, out DictionaryTreeNode<TKey, TValue> value, params TKey[] sections)
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
        
        public void CopyTo(KeyValuePair<TKey, DictionaryTreeNode<TKey, TValue>>[] array, Int32 index)
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

            foreach ((TKey key, DictionaryTreeNode<TKey, TValue> value) in Tree)
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

        public void RemoveEmpty(TKey key, params TKey[] sections)
        {
            DictionaryTreeNode<TKey, TValue> node = GetChildSection(sections);
            
            if (node is null || node.Count <= 0)
            {
                return;
            }

            node.RemoveEmpty(key);
        }

        public DictionaryTreeNode<TKey, TValue> this[TKey key]
        {
            get
            {
                if (!ContainsKey(key))
                {
                    Tree[key] = new DictionaryTreeNode<TKey, TValue>();
                }

                return Tree[key];
            }
            set
            {
                Tree[key] = value;
            }
        }

        public DictionaryTreeNode<TKey, TValue> this[TKey key, params TKey[] sections]
        {
            get
            {
                DictionaryTreeNode<TKey, TValue> node = this;

                if (sections.Length <= 0)
                {
                    return node[key];
                }

                node = sections.Aggregate(node, (current, section) => current[section]);

                return node[key];
            }
            set
            {
                DictionaryTreeNode<TKey, TValue> node = this;

                if (sections.Length <= 0)
                {
                    node[key] = value;
                }

                node = sections.Aggregate(node, (current, section) => current[section]);

                node[key] = value;
            }
        }
        
        public IEnumerator<KeyValuePair<TKey, DictionaryTreeNode<TKey, TValue>>> GetEnumerator()
        {
            return HasTree ? Tree.GetEnumerator() : EnumerableUtils.GetEmptyEnumerator<KeyValuePair<TKey, DictionaryTreeNode<TKey, TValue>>>();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}