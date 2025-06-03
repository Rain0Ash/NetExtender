using System;
using System.Collections.Generic;

namespace NetExtender.Types.History.Interfaces
{
    public interface IReadOnlyTimeHistoryCollection<T> : IReadOnlyTimeHistoryCollection<T, TimeHistoryCollection<T>.Node>
    {
    }
    
    public interface IReadOnlyTimeHistoryCollection<out T, in TNode> : IReadOnlyCollection<T> where TNode : struct, ITimeHistoryCollectionNode<T>
    {
        public IComparer<TNode> Comparer { get; }
        public DateTimeOffset? Min { get; }
        public DateTimeOffset? Max { get; }
        public Int32 Limit { get; }
    }
}