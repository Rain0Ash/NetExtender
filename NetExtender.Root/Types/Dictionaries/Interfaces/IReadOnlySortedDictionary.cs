using System.Collections.Generic;

namespace NetExtender.Types.Dictionaries.Interfaces
{
    public interface IReadOnlySortedDictionary<TKey, TValue> : IReadOnlyDictionary<TKey, TValue>
    {
        public IComparer<TKey> Comparer { get; }
    }
}