using NetExtender.Types.Collections.Interfaces;

namespace NetExtender.Types.Lists.Interfaces
{
    public interface IItemObservableSortedList<T> : IObservableSortedList<T>, IItemObservableCollection<T>
    {
    }
    
    public interface ISuppressObservableSortedList<T> : IObservableSortedList<T>, ISuppressObservableCollection<T>
    {
    }
    
    public interface IObservableSortedList<T> : ISortedList<T>, IObservableCollection<T>
    {
    }
}