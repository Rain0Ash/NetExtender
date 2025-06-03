// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Collections.Generic;
using NetExtender.Interfaces.Notify;

namespace NetExtender.Types.Sets.Interfaces
{
    public interface IObservableSortedSet<T> : IObservableSet<T>, ISortedSet<T>
    {
    }
    
    public interface IIndexObservableSortedSet<T> : IObservableSortedSet<T>, IIndexSortedSet<T>
    {
    }
    
    public interface IObservableHashSet<T> : IObservableSet<T>, IHashSet<T>
    {
    }
    
    public interface IIndexObservableHashSet<T> : IObservableHashSet<T>
    {
    }
    
    public interface IObservableSet<T> : ISet<T>, INotifyCollection
    {
    }
    
    public interface IIndexObservableSet<T> : IObservableSet<T>, IIndexSortedSet<T>
    {
    }
}