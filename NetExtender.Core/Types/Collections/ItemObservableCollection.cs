// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace NetExtender.Types.Collections
{
    public class ItemObservableCollection<T> : SuppressObservableCollection<T>
    {
        public ItemObservableCollection()
        {
        }

        public ItemObservableCollection(IEnumerable<T> collection)
            : base(collection)
        {
        }

        public ItemObservableCollection(List<T> list)
            : base(list)
        {
        }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            Unsubscribe(e.OldItems);
            Subscribe(e.NewItems);
            base.OnCollectionChanged(e);
        }

        private void ItemPropertyChanged(Object? sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e);
        }

        private void Subscribe(IList? items)
        {
            if (items is null)
            {
                return;
            }

            foreach (INotifyPropertyChanged item in items.OfType<INotifyPropertyChanged>())
            {
                item.PropertyChanged += ItemPropertyChanged;
            }
        }

        private void Unsubscribe(IList? items)
        {
            if (items is null)
            {
                return;
            }

            foreach (INotifyPropertyChanged item in items.OfType<INotifyPropertyChanged>())
            {
                item.PropertyChanged -= ItemPropertyChanged;
            }
        }

        protected override void ClearItems()
        {
            foreach(INotifyPropertyChanged item in this.OfType<INotifyPropertyChanged>())
            {
                item.PropertyChanged -= ItemPropertyChanged;
            }

            base.ClearItems();
        }
    }
}