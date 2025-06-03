using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace NetExtender.Types.Sets.Interfaces
{
    public interface ISortedSet<T> : ISet<T>
    {
        public IComparer<T> Comparer { get; }
        public T? Min { get; }
        public T? Max { get; }

        public Boolean TryGetValue(T equalValue, [MaybeNullWhen(false)] out T actualValue);
        public ISortedSet<T> GetViewBetween(T? lower, T? upper);
        public IEnumerable<T> Reverse();
    }

    public interface ISortedSet : ISet
    {
        public Object? Min { get; }
        public Object? Max { get; }
        
        public IEnumerable Reverse();
    }
}