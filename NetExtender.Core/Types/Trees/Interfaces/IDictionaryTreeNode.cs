// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace NetExtender.Types.Trees.Interfaces
{
    public interface IDictionaryTreeNode<TKey, TValue> : IDictionaryTree<TKey, TValue> where TKey : notnull
    {
        public TValue Value { get; set; }
        public IDictionaryTree<TKey, TValue> Tree { get; }
    }
}