using System;
using System.Collections.Generic;

namespace NetExtender.Types.History.Interfaces
{
    public interface ITimeHistoryCollection<T> : ITimeHistoryCollection<T, TimeHistoryCollection<T>.Node>
    {
    }
    
    public interface ITimeHistoryCollection<T, in TNode> : ICollection<T> where TNode : struct, ITimeHistoryCollectionNode<T>
    {
        public IComparer<TNode> Comparer { get; }
        public DateTimeOffset? Min { get; }
        public DateTimeOffset? Max { get; }
        public Int32 Limit { get; }
    }
}