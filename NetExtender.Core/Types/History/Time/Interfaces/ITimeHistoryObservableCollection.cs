using NetExtender.Types.Collections.Interfaces;

namespace NetExtender.Types.History.Interfaces
{
    public interface ITimeHistoryObservableCollection<T> : ITimeHistoryObservableCollection<T, TimeHistoryCollection<T>.Node>
    {
    }
    
    public interface ITimeHistoryObservableCollection<T, in TNode> : ITimeHistoryList<T, TNode>, ISuppressObservableCollection<T> where TNode : struct, ITimeHistoryCollectionNode<T>
    {
    }
}