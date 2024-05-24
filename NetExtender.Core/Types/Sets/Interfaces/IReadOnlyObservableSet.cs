// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Collections.Generic;
using NetExtender.Interfaces.Notify;

namespace NetExtender.Types.Sets.Interfaces
{
    public interface IReadOnlyObservableSet<T> : IReadOnlySet<T>, INotifyCollection
    {
    }
    
    public interface IReadOnlyIndexObservableIndexSortedSet<T> : IReadOnlyObservableSet<T>, IReadOnlyIndexSortedSet<T>
    {
    }
}