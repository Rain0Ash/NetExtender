// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;

namespace NetExtender.Types.Trees.Interfaces
{
    public interface IReadOnlyDictionaryTree<TKey, TValue> : IReadOnlyDictionary<TKey, IDictionaryTreeNode<TKey, TValue?>> where TKey : notnull
    {
        public IEqualityComparer<TKey> Comparer { get; }
        
        public Int64 FullCount { get; }

        public Boolean IsEmpty { get; }

        public Boolean HasValue { get; }

        public Boolean HasTree { get; }
        
        public Boolean TreeIsEmpty { get; }
        
        public Boolean ContainsKey(TKey key, IEnumerable<TKey> sections);
        
        public Boolean ContainsKey(TKey key, params TKey[] sections);

        public TValue? GetValue(TKey key);
        
        public TValue? GetValue(TKey key, IEnumerable<TKey> sections);
        
        public TValue? GetValue(TKey key, params TKey[] sections);

        public IDictionaryTreeNode<TKey, TValue?>? GetChild(TKey key);

        public IDictionaryTreeNode<TKey, TValue?>? GetChild(TKey key, IEnumerable<TKey> sections);
        public IDictionaryTreeNode<TKey, TValue?>? GetChild(TKey key, params TKey[] sections);

        public IDictionaryTreeNode<TKey, TValue?>? GetChildSection(IEnumerable<TKey> sections);

        public IDictionaryTreeNode<TKey, TValue?>? GetChildSection(params TKey[] sections);
        
        public new IDictionaryTreeNode<TKey, TValue?>? this[TKey key] { get; }

        public IDictionaryTreeNode<TKey, TValue?>? this[TKey key, IEnumerable<TKey> sections] { get; }
        
        public IDictionaryTreeNode<TKey, TValue?>? this[TKey key, params TKey[] sections] { get; }
    }
}