// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using NetExtender.Utils.Types;
using DynamicData.Annotations;
using NetExtender.Types.Trees.Interfaces;
using Newtonsoft.Json;

namespace NetExtender.Types.Trees
{
    public class DictionaryTree<TKey, TValue> : Dictionary<TKey, DictionaryTreeNode<TKey, TValue>>, IDictionaryTree<TKey, TValue>, IReadOnlyDictionaryTree<TKey, TValue>
    {
        [JsonIgnore]
        private DictionaryTreeNode<TKey, TValue> _node;
        
        [JsonIgnore]
        private DictionaryTreeNode<TKey, TValue> Node
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

        public DictionaryTree([NotNull] IDictionary<TKey, DictionaryTreeNode<TKey, TValue>> dictionary)
            : base(dictionary)
        {
        }

        public DictionaryTree([NotNull] IDictionary<TKey, DictionaryTreeNode<TKey, TValue>> dictionary, [CanBeNull] IEqualityComparer<TKey> comparer)
            : base(dictionary, comparer)
        {
        }

        public DictionaryTree([NotNull] IEnumerable<KeyValuePair<TKey, DictionaryTreeNode<TKey, TValue>>> collection)
            : base(collection)
        {
        }

        public DictionaryTree([NotNull] IEnumerable<KeyValuePair<TKey, DictionaryTreeNode<TKey, TValue>>> collection, [CanBeNull] IEqualityComparer<TKey> comparer)
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

        public new Boolean ContainsKey(TKey key)
        {
            return base.ContainsKey(key);
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
            DictionaryTreeNode<TKey, TValue> node = Node;
            
            return sections.Any(section => !node.TryGetValue(section, out node)) ? null : node;
        }
        
        public DictionaryTreeNode<TKey, TValue> GetChildSection(params TKey[] sections)
        {
            return GetChildSection(sections.AsEnumerable());
        }

        public TValue GetValue(TKey key)
        {
            if (!TryGetValue(key, out DictionaryTreeNode<TKey, TValue> node))
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
        
        public new Boolean Remove(TKey key, out DictionaryTreeNode<TKey, TValue> value)
        {
            return base.Remove(key, out value);
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

        public void RemoveEmpty()
        {
            foreach (TKey key in Keys)
            {
                RemoveEmpty(key);
            }
        }
        
        public void RemoveEmpty(TKey key)
        {
            DictionaryTreeNode<TKey, TValue> node = GetChild(key);
            
            if (node is null)
            {
                return;
            }

            if (node.FullCount <= 0 && node.Value.IsDefault())
            {
                Remove(key);
            }
            else if (node.FullCount > 0)
            {
                foreach ((TKey dictkey, DictionaryTreeNode<TKey, TValue> dict) in node)
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

        public new DictionaryTreeNode<TKey, TValue> this[TKey key]
        {
            get
            {
                if (TryGetValue(key, out DictionaryTreeNode<TKey, TValue> node))
                {
                    return node;
                }

                node = new DictionaryTreeNode<TKey, TValue>();
                base[key] = node;
                return node;
            }
            set
            {
                base[key] = value;
            }
        }

        public DictionaryTreeNode<TKey, TValue> this[TKey key, params TKey[] sections]
        {
            get
            {
                DictionaryTreeNode<TKey, TValue> node = Node;

                if (sections.Length <= 0)
                {
                    return node[key];
                }

                node = sections.Aggregate(node, (current, section) => current[section]);

                return node[key];
            }
            set
            {
                DictionaryTreeNode<TKey, TValue> node = Node;

                if (sections.Length <= 0)
                {
                    node[key] = value;
                }

                node = sections.Aggregate(node, (current, section) => current[section]);

                node[key] = value;
            }
        }
    }
}