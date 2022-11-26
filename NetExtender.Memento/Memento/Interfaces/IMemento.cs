// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace NetExtender.Types.Memento.Interfaces
{
    public interface IMemento<TSource> : ICollection<IMementoItem<TSource>>, INotifyPropertyChanged, IDisposable where TSource : class
    {
        public Boolean CanUndo { get; }
        public Boolean CanRedo { get; }

        public Boolean Remember(TSource item);
        public Boolean Remember(IMementoItem<TSource> item);

        public void Begin();
        public void End();

        public Boolean Undo();
        public Boolean Redo();
    }
}