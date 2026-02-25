using System.Collections.Generic;

namespace NetExtender.Types.Dictionaries.Interfaces
{
    public interface IHashDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        public IEqualityComparer<TKey> Comparer { get; }
    }
}