using System.Collections.Generic;

namespace NetExtender.Types.Lists.Interfaces
{
    public interface IReadOnlySortedList<T> : IReadOnlyList<T>
    {
        public IComparer<T> Comparer { get; }
    }
}