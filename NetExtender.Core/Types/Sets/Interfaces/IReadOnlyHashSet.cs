using System.Collections.Generic;

namespace NetExtender.Types.Sets.Interfaces
{
    public interface IReadOnlyHashSet<T> : IReadOnlySet<T>
    {
        public IEqualityComparer<T> Comparer { get; }
    }
}