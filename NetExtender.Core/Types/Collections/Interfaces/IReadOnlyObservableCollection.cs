using System.Collections.Generic;
using NetExtender.Interfaces.Notify;

namespace NetExtender.Types.Collections.Interfaces
{
    public interface IReadOnlyObservableCollection<out T> : IReadOnlyObservableCollectionAbstraction<T>, IReadOnlyList<T>
    {
    }

    public interface IReadOnlyObservableCollectionAbstraction<out T> : IReadOnlyCollection<T>, INotifyCollection
    {
    }
}