using System.Collections.Generic;
using NetExtender.Interfaces.Notify;

namespace NetExtender.Types.Collections.Interfaces
{
    public interface IObservableCollection<T> : IObservableCollectionAbstraction<T>, IList<T>
    {
    }

    public interface IObservableCollectionAbstraction<T> : ICollection<T>, INotifyCollection
    {
    }
}