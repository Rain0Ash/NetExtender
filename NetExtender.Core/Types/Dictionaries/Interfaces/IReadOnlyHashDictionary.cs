using System.Collections.Generic;

namespace NetExtender.Types.Dictionaries.Interfaces
{
    public interface IReadOnlyHashDictionary<TKey, TValue> : IReadOnlyDictionary<TKey, TValue>
    {
        public IEqualityComparer<TKey> Comparer { get; }
    }
}