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
    public class ItemObservableCollection<T> : SuppressObservableCollection<T>, IItemObservableCollection<T>, IReadOnlyItemObservableCollection<T> where T : class
    {
        public event PropertyChangingEventHandler? ItemChanging;
        public event PropertyChangedEventHandler? ItemChanged;
        
        public ItemObservableCollection()
        {
        }

        public ItemObservableCollection(IEnumerable<T> collection)
            : base(collection)
        {
            foreach (T item in this)
            {
                Initialize(item);
            }
        }

        public ItemObservableCollection(List<T> list)
            : base(list)
        {
            foreach (T item in this)
            {
                Initialize(item);
            }
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

        private void Initialize(T? item)
        {
            if (item is null)
            {
                return;
            }

            if (item is INotifyPropertyChanging changing)
            {
                changing.PropertyChanging += ItemPropertyChanging;
            }

            if (item is INotifyPropertyChanged changed)
            {
                changed.PropertyChanged += ItemPropertyChanged;
            }
        }

        protected virtual void Subscribe<TItem>(TItem? item)
        {
            if (item is null)
            {
                return;
            }

            if (item is INotifyPropertyChanging changing)
            {
                changing.PropertyChanging += ItemPropertyChanging;
            }

            if (item is INotifyPropertyChanged changed)
            {
                changed.PropertyChanged += ItemPropertyChanged;
            }
        }

        protected void Subscribe(IList? items)
        {
            if (items is null)
            {
                return;
            }

            lock (SyncRoot)
            {
                foreach (Object item in items)
                {
                    Subscribe(item);
                }
            }
        }
        
        protected void Subscribe(IList<T>? items)
        {
            if (items is null)
            {
                return;
            }

            lock (SyncRoot)
            {
                foreach (T item in items)
                {
                    Subscribe(item);
                }
            }
        }
        
        protected virtual void Unsubscribe<TItem>(TItem? item)
        {
            if (item is null)
            {
                return;
            }

            if (item is INotifyPropertyChanging changing)
            {
                changing.PropertyChanging -= ItemPropertyChanging;
            }

            if (item is INotifyPropertyChanged changed)
            {
                changed.PropertyChanged -= ItemPropertyChanged;
            }
        }
        
        protected void Unsubscribe(IList? items)
        {
            if (items is null)
            {
                return;
            }

            lock (SyncRoot)
            {
                foreach (Object item in items)
                {
                    Unsubscribe(item);
                }
            }
        }
        
        protected void Unsubscribe(IList<T>? items)
        {
            if (items is null)
            {
                return;
            }

            lock (SyncRoot)
            {
                foreach (T item in items)
                {
                    Unsubscribe(item);
                }
            }
        }

        protected override void ClearItems()
        {
            lock (SyncRoot)
            {
                foreach(T item in this)
                {
                    Unsubscribe(item);
                }
            }

            base.ClearItems();
        }
    }
}