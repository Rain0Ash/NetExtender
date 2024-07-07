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