// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;

namespace NetExtender.Types.Trees.Interfaces
{
    public interface IDictionaryTree<TKey, TValue> : IDictionary<TKey, IDictionaryTreeNode<TKey, TValue>>
    {
        public IEqualityComparer<TKey> Comparer { get; }
        
        public Int64 FullCount { get; }
        
        public Boolean IsEmpty { get; }

        public Boolean HasValue { get; }

        public Boolean HasTree { get; }
        
        public Boolean TreeIsEmpty { get; }
        
        public Boolean ContainsKey(TKey key, params TKey[] sections);

        public IDictionaryTreeNode<TKey, TValue> GetChild(TKey key);

        public IDictionaryTreeNode<TKey, TValue> GetChild(TKey key, params TKey[] sections);

        public IDictionaryTreeNode<TKey, TValue> GetChildSection(IEnumerable<TKey> sections);

        public IDictionaryTreeNode<TKey, TValue> GetChildSection(params TKey[] sections);

        public Boolean Remove(TKey key, params TKey[] sections);

        public Boolean Remove(TKey key, IEnumerable<TKey> sections, out IDictionaryTreeNode<TKey, TValue> value);

        public Boolean Remove(TKey key, out IDictionaryTreeNode<TKey, TValue> value, params TKey[] sections);

        public void RemoveEmpty();

        public void RemoveEmpty(TKey key);

        public void RemoveEmpty(TKey key, params TKey[] sections);
        
        public new IDictionaryTreeNode<TKey, TValue> this[TKey key] { get; set; }
        
        public IDictionaryTreeNode<TKey, TValue> this[TKey key, params TKey[] sections] { get; set; }
    }
}