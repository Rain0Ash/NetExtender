using System.Collections.Generic;

namespace NetExtender.Types.Dictionaries.Interfaces
{
    public interface ISortedDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        public IComparer<TKey> Comparer { get; }
    }
}