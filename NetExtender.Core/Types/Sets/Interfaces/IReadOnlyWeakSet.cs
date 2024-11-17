using System;
using System.Collections.Generic;

namespace NetExtender.Types.Sets.Interfaces
{
    public interface IReadOnlyWeakSet<T> : IEnumerable<T> where T : class
    {
        public Boolean Contains(T item);
    }
}