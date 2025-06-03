using System.Collections.Generic;

namespace NetExtender.Types.History.Interfaces
{
    public interface ITimeHistoryList<T> : ITimeHistoryList<T, TimeHistoryCollection<T>.Node>, ITimeHistoryCollection<T>
    {
    }
    
    public interface ITimeHistoryList<T, in TNode> : ITimeHistoryCollection<T, TNode>, IList<T> where TNode : struct, ITimeHistoryCollectionNode<T>
    {
    }
}