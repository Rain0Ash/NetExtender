// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Collections.Generic;
using NetExtender.Interfaces.Notify;

namespace NetExtender.Types.Sets.Interfaces
{
    public interface IReadOnlyObservableSortedSet<T> : IReadOnlyObservableSet<T>, IReadOnlySortedSet<T>
    {
    }
    
    public interface IReadOnlyIndexObservableSortedSet<T> : IReadOnlyObservableSortedSet<T>, IReadOnlyIndexSortedSet<T>
    {
    }
    
    public interface IReadOnlyObservableHashSet<T> : IReadOnlyObservableSet<T>, IReadOnlyHashSet<T>
    {
    }
    
    public interface IReadOnlyIndexObservableHashSet<T> : IReadOnlyObservableHashSet<T>
    {
    }
    
    public interface IReadOnlyObservableSet<T> : IReadOnlySet<T>, INotifyCollection
    {
    }
    
    public interface IReadOnlyIndexObservableIndexSortedSet<T> : IReadOnlyObservableSet<T>, IReadOnlyIndexSortedSet<T>
    {
    }
}