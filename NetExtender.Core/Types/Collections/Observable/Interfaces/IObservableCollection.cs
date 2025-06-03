// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

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