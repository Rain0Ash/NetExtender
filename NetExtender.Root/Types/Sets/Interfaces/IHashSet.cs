using System.Collections.Generic;

namespace NetExtender.Types.Sets.Interfaces
{
    public interface IHashSet<T> : ISet<T>
    {
        public IEqualityComparer<T> Comparer { get; }
    }
}