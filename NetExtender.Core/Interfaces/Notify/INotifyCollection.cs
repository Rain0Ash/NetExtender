// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Collections.Specialized;
using System.ComponentModel;

namespace NetExtender.Interfaces.Notify
{
    public interface INotifyItemCollection : INotifyCollection
    {
        public event PropertyChangingEventHandler? ItemChanging;
        public event PropertyChangedEventHandler? ItemChanged;
    }
    
    public interface INotifyCollection : INotifyCollectionChanged, INotifyProperty
    {
    }
}