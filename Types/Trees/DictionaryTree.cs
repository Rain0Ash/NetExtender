// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using NetExtender.Utils.Types;
using JetBrains.Annotations;
using NetExtender.Types.Trees.Interfaces;
using Newtonsoft.Json;

namespace NetExtender.Types.Trees
{
    public class DictionaryTree<TKey, TValue> : Dictionary<TKey, IDictionaryTreeNode<TKey, TValue>>, IDictionaryTree<TKey, TValue>, IReadOnlyDictionaryTree<TKey, TValue>
    {
        [JsonIgnore]
        private IDictionaryTreeNode<TKey, TValue> _node;
        
        [JsonIgnore]
        private IDictionaryTreeNode<TKey, TValue> Node
        {
            get
            {
                return _node ??= new DictionaryTreeNode<TKey, TValue>(this);
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

        public DictionaryTree([NotNull] IDictionary<TKey, IDictionaryTreeNode<TKey, TValue>> dictionary)
            : base(dictionary)
        {
        }

        public DictionaryTree([NotNull] IDictionary<TKey, IDictionaryTreeNode<TKey, TValue>> dictionary, [CanBeNull] IEqualityComparer<TKey> comparer)
            : base(dictionary, comparer)
        {
        }

        public DictionaryTree([NotNull] IEnumerable<KeyValuePair<TKey, IDictionaryTreeNode<TKey, TValue>>> collection)
            : base(collection)
        {
        }

        public DictionaryTree([NotNull] IEnumerable<KeyValuePair<TKey, IDictionaryTreeNode<TKey, TValue>>> collection, [CanBeNull] IEqualityComparer<TKey> comparer)
            : base(collection, comparer)
        {
        }

        public DictionaryTree([CanBeNull] IEqualityComparer<TKey> comparer)
            : base(comparer)
        {
        }

        public DictionaryTree(Int32 capacity)
            : base(capacity)
        {
        }

        public DictionaryTree(Int32 capacity, [CanBeNull] IEqualityComparer<TKey> comparer)
            : base(capacity, comparer)
        {
        }

        protected DictionaryTree([NotNull] SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        protected virtual IDictionaryTreeNode<TKey, TValue> CreateNode()
        {
            return new DictionaryTreeNode<TKey, TValue>(Comparer);
        }

        public new Boolean ContainsKey(TKey key)
        {
            return base.ContainsKey(key);
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
            IDictionaryTreeNode<TKey, TValue> node = Node;
            
            return sections.Any(section => !node.TryGetValue(section, out node)) ? null : node;
        }
        
        public IDictionaryTreeNode<TKey, TValue> GetChildSection(params TKey[] sections)
        {
            return GetChildSection(sections.AsEnumerable());
        }

        public TValue GetValue(TKey key)
        {
            if (!TryGetValue(key, out IDictionaryTreeNode<TKey, TValue> node))
            {
                throw new KeyNotFoundException();
            }

            return node.Value;
        }

        public TValue GetValue(TKey key, params TKey[] sections)
        {
            return (GetChild(key, sections) ?? throw new KeyNotFoundException()).Value;
        }

        public new Boolean Remove(TKey key)
        {
            return base.Remove(key);
        }
        
        public new Boolean Remove(TKey key, out IDictionaryTreeNode<TKey, TValue> value)
        {
            return base.Remove(key, out value);
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

        public void RemoveEmpty()
        {
            foreach (TKey key in Keys)
            {
                RemoveEmpty(key);
            }
        }
        
        public void RemoveEmpty(TKey key)
        {
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
        
        public void RemoveEmpty(TKey key, params TKey[] sections)
        {
            IDictionaryTreeNode<TKey, TValue> node = GetChildSection(sections);
            
            if (node is null || node.Count <= 0)
            {
                return;
            }

            node.RemoveEmpty(key);
        }

        public new IDictionaryTreeNode<TKey, TValue> this[TKey key]
        {
            get
            {
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
        
        public IDictionaryTreeNode<TKey, TValue> this[TKey key, IEnumerable<TKey> sections]
        {
            get
            {
                IDictionaryTreeNode<TKey, TValue> node = Node;

                if (sections is null)
                {
                    return node[key];
                }

                node = sections.Aggregate(node, (current, section) => current[section]);

                return node[key];
            }
            set
            {
                IDictionaryTreeNode<TKey, TValue> node = Node;

                if (sections is null)
                {
                    node[key] = value;
                    return;
                }

                node = sections.Aggregate(node, (current, section) => current[section]);

                node[key] = value;
            }
        }
    }
}