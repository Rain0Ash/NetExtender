// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace NetExtender.Types.Trees.Interfaces
{
    public interface IReadOnlyDictionaryTreeNode<TKey, TValue> : IReadOnlyDictionaryTree<TKey, TValue?> where TKey : notnull
    {
        public TValue Value { get; }
        public IDictionaryTree<TKey, TValue?> Tree { get; }
    }
}