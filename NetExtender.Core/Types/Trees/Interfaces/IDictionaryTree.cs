// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace NetExtender.Types.Trees.Interfaces
{
    public interface IDictionaryTree<TKey, TValue> : IDictionary<TKey, IDictionaryTreeNode<TKey, TValue>> where TKey : notnull
    {
        public IEqualityComparer<TKey> Comparer { get; }
        public Int64 FullCount { get; }
        public Boolean IsEmpty { get; }
        public Boolean HasValue { get; }
        public Boolean HasTree { get; }
        public Boolean TreeIsEmpty { get; }
        public new Boolean ContainsKey(TKey key);
        public Boolean ContainsKey(TKey key, IEnumerable<TKey>? sections);
        public Boolean ContainsKey(TKey key, params TKey[]? sections);
        public TValue? GetValue(TKey key);
        public TValue? GetValue(TKey key, IEnumerable<TKey>? sections);
        public TValue? GetValue(TKey key, params TKey[]? sections);
        public IDictionaryTreeNode<TKey, TValue>? GetChild(TKey key);
        public IDictionaryTreeNode<TKey, TValue>? GetChild(TKey key, IEnumerable<TKey>? sections);
        public IDictionaryTreeNode<TKey, TValue>? GetChild(TKey key, params TKey[]? sections);
        public IDictionaryTreeNode<TKey, TValue>? GetChildSection(IEnumerable<TKey>? sections);
        public IDictionaryTreeNode<TKey, TValue>? GetChildSection(params TKey[]? sections);
        public void Add(TKey key, TValue value);
        public void Add(TKey key, IEnumerable<TKey>? sections, TValue value);
        public void Add(TKey key, TValue value, params TKey[]? sections);
        public Boolean TryAdd(TKey key, TValue value);
        public Boolean TryAdd(TKey key, IEnumerable<TKey>? sections, TValue value);
        public Boolean TryAdd(TKey key, TValue value, params TKey[]? sections);
        public new Boolean Remove(TKey key);
        public Boolean Remove(TKey key, out TValue? value);
        public Boolean Remove(TKey key, [MaybeNullWhen(false)] out IDictionaryTreeNode<TKey, TValue> value);
        public Boolean Remove(TKey key, IEnumerable<TKey>? sections);
        public Boolean Remove(TKey key, params TKey[]? sections);
        public Boolean Remove(TKey key, IEnumerable<TKey> sections, [MaybeNullWhen(false)] out IDictionaryTreeNode<TKey, TValue> value);
        public Boolean Remove(TKey key, [MaybeNullWhen(false)] out IDictionaryTreeNode<TKey, TValue> value, params TKey[] sections);
        public Boolean Purge();
        public Boolean Purge(TKey key);
        public Boolean Purge(TKey key, IEnumerable<TKey> sections);
        public Boolean Purge(TKey key, params TKey[] sections);
        public DictionaryTreeEntry<TKey, TValue>[]? Dump();
        public DictionaryTreeEntry<TKey, TValue>[]? Dump(params TKey[]? sections);
        public DictionaryTreeEntry<TKey, TValue>[]? Dump(IEnumerable<TKey>? sections);
        public new IDictionaryTreeNode<TKey, TValue> this[TKey key] { get; set; }
        public IDictionaryTreeNode<TKey, TValue> this[TKey key, IEnumerable<TKey> sections] { get; set; }
        public IDictionaryTreeNode<TKey, TValue> this[TKey key, params TKey[] sections] { get; set; }
    }
}