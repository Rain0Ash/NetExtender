using NetExtender.Interfaces.Notify;

namespace NetExtender.Types.Collections.Interfaces
{
    public interface IItemObservableCollection<T> : IObservableCollection<T>, INotifyItemCollection
    {
    }
}