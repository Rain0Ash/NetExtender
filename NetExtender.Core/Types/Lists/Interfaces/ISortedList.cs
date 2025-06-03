using System.Collections;
using System.Collections.Generic;

namespace NetExtender.Types.Lists.Interfaces
{
    public interface ISortedList<T> : IList<T>
    {
        public IComparer<T> Comparer { get; }
    }
    
    public interface ISortedList : IList
    {
    }
}