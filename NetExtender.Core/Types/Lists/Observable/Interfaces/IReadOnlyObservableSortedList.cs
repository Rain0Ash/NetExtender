using NetExtender.Types.Collections.Interfaces;

namespace NetExtender.Types.Lists.Interfaces
{
    public interface IReadOnlyItemObservableSortedList<T> : IReadOnlyObservableSortedList<T>, IReadOnlyItemObservableCollection<T>
    {
    }
    
    public interface IReadOnlySuppressObservableSortedList<T> : IReadOnlyObservableSortedList<T>, IReadOnlySuppressObservableCollection<T>
    {
    }
    
    public interface IReadOnlyObservableSortedList<T> : IReadOnlySortedList<T>, IReadOnlyObservableCollection<T>
    {
    }
}