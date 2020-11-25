// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;

namespace NetExtender.Types.Trees.Interfaces
{
    public interface IDictionaryTree<TKey, TValue> : IDictionary<TKey, DictionaryTreeNode<TKey, TValue>>
    {
        public Int64 FullCount { get; }
        
        public Boolean ContainsKey(TKey key, params TKey[] sections);

        public DictionaryTreeNode<TKey, TValue> GetChild(TKey key);

        public DictionaryTreeNode<TKey, TValue> GetChild(TKey key, params TKey[] sections);

        public DictionaryTreeNode<TKey, TValue> GetChildSection(IEnumerable<TKey> sections);

        public DictionaryTreeNode<TKey, TValue> GetChildSection(params TKey[] sections);

        public Boolean Remove(TKey key, params TKey[] sections);

        public Boolean Remove(TKey key, IEnumerable<TKey> sections, out DictionaryTreeNode<TKey, TValue> value);

        public Boolean Remove(TKey key, out DictionaryTreeNode<TKey, TValue> value, params TKey[] sections);

        public DictionaryTreeNode<TKey, TValue> this[TKey key, params TKey[] sections] { get; set; }
    }
}