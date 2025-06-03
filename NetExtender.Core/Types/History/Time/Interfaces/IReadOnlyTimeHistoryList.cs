using System.Collections.Generic;

namespace NetExtender.Types.History.Interfaces
{
    public interface IReadOnlyTimeHistoryList<T> : IReadOnlyTimeHistoryList<T, TimeHistoryCollection<T>.Node>, IReadOnlyTimeHistoryCollection<T>
    {
    }
    
    public interface IReadOnlyTimeHistoryList<out T, in TNode> : IReadOnlyTimeHistoryCollection<T, TNode>, IReadOnlyList<T> where TNode : struct, ITimeHistoryCollectionNode<T>
    {
    }
}