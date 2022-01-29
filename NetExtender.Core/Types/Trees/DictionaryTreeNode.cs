// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using NetExtender.Types.Trees.Interfaces;
using NetExtender.Types.Trees.Json;
using NetExtender.Utilities.Types;
using Newtonsoft.Json;

namespace NetExtender.Types.Trees
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn, MissingMemberHandling = MissingMemberHandling.Ignore, ItemNullValueHandling = NullValueHandling.Ignore, ItemConverterType = typeof(DictionaryTreeJsonConverter))]
    public class DictionaryTreeNode<TKey, TValue> : IDictionaryTreeNode<TKey, TValue>, IReadOnlyDictionaryTreeNode<TKey, TValue> where TKey : notnull
    {
        [JsonProperty(Order = 0, DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
        public TValue? Value { get; set; }

        [JsonProperty(PropertyName = nameof(Tree), Order = 1)]
        private IDictionaryTree<TKey, TValue>? _tree;

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
                return !HasTree || Tree.IsReadOnly;
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
        IEnumerable<TKey> IReadOnlyDictionary<TKey, IDictionaryTreeNode<TKey, TValue>>.Keys
        {
            get
            {
                return Keys;
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
        IEnumerable<IDictionaryTreeNode<TKey, TValue>> IReadOnlyDictionary<TKey, IDictionaryTreeNode<TKey, TValue>>.Values
        {
            get
            {
                return Values;
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
                if (!HasTree || Tree.Count <= 0)
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
            Value = default;
            Comparer = EqualityComparer<TKey>.Default;
        }

        public DictionaryTreeNode(IEqualityComparer<TKey>? comparer)
            : this(default, comparer)
        {
        }
        
        public DictionaryTreeNode(TValue value)
            : this(value, null, null)
        {
        }

        public DictionaryTreeNode(IDictionaryTree<TKey, TValue>? tree)
            : this(tree, null)
        {
        }

        public DictionaryTreeNode(IDictionaryTree<TKey, TValue>? tree, IEqualityComparer<TKey>? comparer)
            : this(default, tree, comparer)
        {
        }

        public DictionaryTreeNode(TValue? value, IDictionaryTree<TKey, TValue>? tree, IEqualityComparer<TKey>? comparer)
        {
            Value = value;
            _tree = tree;
            Comparer = comparer ?? _tree?.Comparer ?? EqualityComparer<TKey>.Default;
        }

        public virtual IDictionaryTree<TKey, TValue> CreateTree()
        {
            return new DictionaryTree<TKey, TValue>(Comparer);
        }

        protected virtual IDictionaryTreeNode<TKey, TValue> CreateNode()
        {
            return new DictionaryTreeNode<TKey, TValue>(Comparer);
        }
        
        public Boolean Contains(KeyValuePair<TKey, IDictionaryTreeNode<TKey, TValue>> item)
        {
            return HasTree && Tree.Contains(item);
        }

        public Boolean ContainsKey(TKey key)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return HasTree && Tree.ContainsKey(key);
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
            
            IDictionaryTreeNode<TKey, TValue>? node = this;
            return sections.WhereNotNull().Any(section => !node.TryGetValue(section, out node)) ? null : node;
        }

        public IDictionaryTreeNode<TKey, TValue>? GetChildSection(params TKey[]? sections)
        {
            return GetChildSection((IEnumerable<TKey>?) sections);
        }
        
        public void Add(KeyValuePair<TKey, IDictionaryTreeNode<TKey, TValue>> item)
        {
            Tree.Add(item);
        }

        public void Add(TKey key, IDictionaryTreeNode<TKey, TValue> value)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            Tree.Add(key, value);
        }

        public void Add(TKey key, TValue value)
        {
            Tree.Add(key, value);
        }

        public void Add(TKey key, IEnumerable<TKey>? sections, TValue value)
        {
            Tree.Add(key, sections, value);
        }

        public void Add(TKey key, TValue value, params TKey[]? sections)
        {
            Tree.Add(key, value, sections);
        }

        public Boolean TryAdd(TKey key, TValue value)
        {
            return Tree.TryAdd(key, value);
        }

        public Boolean TryAdd(TKey key, IEnumerable<TKey>? sections, TValue value)
        {
            return Tree.TryAdd(key, sections, value);
        }

        public Boolean TryAdd(TKey key, TValue value, params TKey[]? sections)
        {
            return Tree.TryAdd(key, value, sections);
        }

        public Boolean TryGetValue(TKey key, [MaybeNullWhen(false)] out IDictionaryTreeNode<TKey, TValue> value)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (HasTree)
            {
                return Tree.TryGetValue(key, out value);
            }

            value = default;
            return false;
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

        public Boolean Remove(TKey key)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return HasTree && Tree.Remove(key);
        }
        
        public Boolean Remove(TKey key, out TValue? value)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (HasTree && Tree.Remove(key, out value))
            {
                return true;
            }

            value = default;
            return false;
        }

        public Boolean Remove(TKey key, [MaybeNullWhen(false)] out IDictionaryTreeNode<TKey, TValue> value)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            
            if (HasTree && Tree.Remove(key, out value))
            {
                return true;
            }
            
            value = default;
            return false;
        }

        public Boolean Remove(KeyValuePair<TKey, IDictionaryTreeNode<TKey, TValue>> item)
        {
            return HasTree && Tree.Remove(item);
        }

        public Boolean Remove(TKey key, params TKey[]? sections)
        {
            return Remove(key, out _, sections);
        }
        
        public Boolean Remove(TKey key, IEnumerable<TKey>? sections)
        {
            return Remove(key, sections, out _);
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
                return;
            }

            Tree.CopyTo(array, index);
        }

        public Boolean Purge()
        {
            if (!HasTree || Tree.Count <= 0)
            {
                return false;
            }

            Boolean successful = false;
            foreach ((TKey key, IDictionaryTreeNode<TKey, TValue> value) in Tree)
            {
                if (value.IsEmpty)
                {
                    successful |= Tree.Remove(key);
                    continue;
                }

                successful |= value.Purge();
            }

            return successful;
        }

        public Boolean Purge(TKey key)
        {
            return Purge(key, Array.Empty<TKey>());
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
        
        public DictionaryTreeEntry<TKey, TValue>[]? Dump()
        {
            return HasTree ? Tree.Dump() : null;
        }
        
        public DictionaryTreeEntry<TKey, TValue>[]? Dump(params TKey[]? sections)
        {
            return HasTree ? Tree.Dump(sections) : null;
        }
        
        public DictionaryTreeEntry<TKey, TValue>[]? Dump(IEnumerable<TKey>? sections)
        {
            return HasTree ? Tree.Dump(sections) : null;
        }

        public IDictionaryTreeNode<TKey, TValue> this[TKey key]
        {
            get
            {
                if (key is null)
                {
                    throw new ArgumentNullException(nameof(key));
                }

                if (!ContainsKey(key))
                {
                    Tree[key] = CreateNode();
                }

                return Tree[key];
            }
            set
            {
                if (key is null)
                {
                    throw new ArgumentNullException(nameof(key));
                }

                Tree[key] = value;
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

                IDictionaryTreeNode<TKey, TValue> node = this;

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

                IDictionaryTreeNode<TKey, TValue> node = this;

                if (sections is null)
                {
                    node[key] = value;
                    return;
                }

                node = sections.WhereNotNull().Aggregate(node, (current, section) => current[section]);

                node[key] = value;
            }
        }

        IEnumerator<KeyValuePair<TKey, IDictionaryTreeNode<TKey, TValue>>> IEnumerable<KeyValuePair<TKey, IDictionaryTreeNode<TKey, TValue>>>.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<KeyValuePair<TKey, IDictionaryTreeNode<TKey, TValue>>> GetEnumerator()
        {
            return HasTree ? Tree.GetEnumerator() : EnumeratorUtilities.GetEmptyEnumerator<KeyValuePair<TKey, IDictionaryTreeNode<TKey, TValue>>>();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}