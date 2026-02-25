using System.Collections.Generic;
using NetExtender.Types.Dictionaries.Interfaces;

namespace NetExtender.Types.Dictionaries
{
    public class SortedDictionaryCollection<TKey, TValue> : SortedDictionary<TKey, TValue>, ISortedDictionary<TKey, TValue>, IReadOnlySortedDictionary<TKey, TValue> where TKey : notnull
    {
        public SortedDictionaryCollection()
        {
        }

        public SortedDictionaryCollection(IComparer<TKey>? comparer)
            : base(comparer)
        {
        }

        public SortedDictionaryCollection(IDictionary<TKey, TValue> dictionary)
            : base(dictionary)
        {
        }

        public SortedDictionaryCollection(IDictionary<TKey, TValue> dictionary, IComparer<TKey>? comparer)
            : base(dictionary, comparer)
        {
        }
    }
}