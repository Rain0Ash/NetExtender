// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;

namespace NetExtender.Types.Trees.Interfaces
{
    public interface IReadOnlyDictionaryTree<TKey, TValue>
    {
        public Int64 FullCount { get; }
        
        public Boolean ContainsKey(TKey key, params TKey[] sections);

        public DictionaryTreeNode<TKey, TValue> GetChild(TKey key);

        public DictionaryTreeNode<TKey, TValue> GetChild(TKey key, params TKey[] sections);

        public DictionaryTreeNode<TKey, TValue> GetChildSection(IEnumerable<TKey> sections);

        public DictionaryTreeNode<TKey, TValue> GetChildSection(params TKey[] sections);

        public DictionaryTreeNode<TKey, TValue> this[TKey key, params TKey[] sections] { get; }
    }
}