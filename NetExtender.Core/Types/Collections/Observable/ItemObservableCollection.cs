// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using NetExtender.Types.Collections.Interfaces;

namespace NetExtender.Types.Collections
{
    public class ItemObservableCollection<T> : SuppressObservableCollection<T>, IItemObservableCollection<T>
    {
        public event PropertyChangingEventHandler? ItemChanging;
        public event PropertyChangedEventHandler? ItemChanged;
        
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

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs args)
        {
            Unsubscribe(args.OldItems);
            Subscribe(args.NewItems);
            base.OnCollectionChanged(args);
        }

        protected virtual void ItemPropertyChanging(Object? sender, PropertyChangingEventArgs args)
        {
            ItemChanging?.Invoke(sender, args);
        }

        protected virtual void ItemPropertyChanged(Object? sender, PropertyChangedEventArgs args)
        {
            ItemChanged?.Invoke(sender, args);
        }
        
        protected virtual void Subscribe(IList? items)
        {
            if (items is null)
            {
                return;
            }

            foreach (Object item in items)
            {
                if (item is INotifyPropertyChanging changing)
                {
                    changing.PropertyChanging += ItemPropertyChanging;
                }
                
                if (item is INotifyPropertyChanged changed)
                {
                    changed.PropertyChanged += ItemPropertyChanged;
                }
            }
        }
        
        protected virtual void Unsubscribe(IList? items)
        {
            if (items is null)
            {
                return;
            }

            foreach (Object item in items)
            {
                if (item is INotifyPropertyChanging changing)
                {
                    changing.PropertyChanging -= ItemPropertyChanging;
                }

                if (item is INotifyPropertyChanged changed)
                {
                    changed.PropertyChanged -= ItemPropertyChanged;
                }
            }
        }

        protected override void ClearItems()
        {
            foreach(T item in this)
            {
                if (item is INotifyPropertyChanging changing)
                {
                    changing.PropertyChanging -= ItemPropertyChanging;
                }
                
                if (item is INotifyPropertyChanged changed)
                {
                    changed.PropertyChanged -= ItemPropertyChanged;
                }
            }

            base.ClearItems();
        }
    }
}