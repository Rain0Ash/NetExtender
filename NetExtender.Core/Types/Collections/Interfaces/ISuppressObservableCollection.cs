// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;

namespace NetExtender.Types.Collections.Interfaces
{
    public interface ISuppressObservableCollection<T> : IList<T>, INotifyCollectionChanged, INotifyPropertyChanged
    {
        public Boolean IsAllowSuppress { get; }
        public Boolean IsSuppressed { get; }
        public Int32 SuppressDepth { get; }

        public IDisposable? Suppress();
        public void Move(Int32 oldIndex, Int32 newIndex);
    }
}