// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using NetExtender.Interfaces.Notify;
using NetExtender.Types.Collections.Interfaces;
using NetExtender.Types.Handlers.Chain.Interfaces;

namespace NetExtender.Types.Collections
{
    public class ItemObservableChainHandlerCollection<T> : ItemObservableChainHandlerCollection<T, IChainHandler<T>>
    {
        public ItemObservableChainHandlerCollection()
        {
        }
        
        public ItemObservableChainHandlerCollection(IEnumerable<IChainHandler<T>> collection)
            : base(collection)
        {
        }
        
        public ItemObservableChainHandlerCollection(List<IChainHandler<T>> list)
            : base(list)
        {
        }
    }

    public class ItemObservableChainHandlerCollection<T, THandler> : ItemObservableChainHandlerCollection<T, THandler, ItemObservableCollection<THandler>> where THandler : class, IChainHandler<T>
    {
        public ItemObservableChainHandlerCollection()
            : base(new ItemObservableCollection<THandler>())
        {
        }
        
        public ItemObservableChainHandlerCollection(IEnumerable<THandler> collection)
            : base(collection is not null ? new ItemObservableCollection<THandler>(collection) : throw new ArgumentNullException(nameof(collection)))
        {
        }
        
        public ItemObservableChainHandlerCollection(List<THandler> list)
            : base(list is not null ? new ItemObservableCollection<THandler>(list) : throw new ArgumentNullException(nameof(list)))
        {
        }
    }
    
    public class SuppressObservableChainHandlerCollection<T> : SuppressObservableChainHandlerCollection<T, IChainHandler<T>>
    {
        public SuppressObservableChainHandlerCollection()
        {
        }
        
        public SuppressObservableChainHandlerCollection(IEnumerable<IChainHandler<T>> collection)
            : base(collection)
        {
        }
        
        public SuppressObservableChainHandlerCollection(List<IChainHandler<T>> list)
            : base(list)
        {
        }
    }
    
    public class SuppressObservableChainHandlerCollection<T, THandler> : SuppressObservableChainHandlerCollection<T, THandler, SuppressObservableCollection<THandler>> where THandler : IChainHandler<T>
    {
        public SuppressObservableChainHandlerCollection()
            : base(new SuppressObservableCollection<THandler>())
        {
        }
        
        public SuppressObservableChainHandlerCollection(IEnumerable<THandler> collection)
            : base(collection is not null ? new SuppressObservableCollection<THandler>(collection) : throw new ArgumentNullException(nameof(collection)))
        {
        }
        
        public SuppressObservableChainHandlerCollection(List<THandler> list)
            : base(list is not null ? new SuppressObservableCollection<THandler>(list) : throw new ArgumentNullException(nameof(list)))
        {
        }
    }
    
    public abstract class ItemObservableChainHandlerCollection<T, THandler, TCollection> : SuppressObservableChainHandlerCollection<T, THandler, TCollection>, INotifyItemCollection where THandler : IChainHandler<T> where TCollection : class, ISuppressObservableCollection<THandler>, INotifyItemCollection
    {
        public event PropertyChangingEventHandler? ItemChanging;
        public event PropertyChangedEventHandler? ItemChanged;
        
        protected ItemObservableChainHandlerCollection(TCollection collection)
            : base(collection)
        {
            Internal.ItemChanging += OnItemChanging;
            Internal.ItemChanged += OnItemChanged;
        }
        
        private void OnItemChanging(Object? sender, PropertyChangingEventArgs args)
        {
            ItemChanging?.Invoke(this, args);
        }
        
        private void OnItemChanged(Object? sender, PropertyChangedEventArgs args)
        {
            ItemChanged?.Invoke(this, args);
        }
    }
    
    public abstract class SuppressObservableChainHandlerCollection<T, THandler, TCollection> : ObservableChainHandlerCollection<T, THandler, TCollection>, ISuppressObservableChainHandlerCollection<T, THandler> where THandler : IChainHandler<T> where TCollection : class, ISuppressObservableCollection<THandler>
    {
        public Boolean IsAllowSuppress
        {
            get
            {
                return Internal.IsAllowSuppress;
            }
        }
        
        public Boolean IsSuppressed
        {
            get
            {
                return Internal.IsSuppressed;
            }
        }
        
        public Int32 SuppressDepth
        {
            get
            {
                return Internal.SuppressDepth;
            }
        }
        
        protected SuppressObservableChainHandlerCollection(TCollection collection)
            : base(collection)
        {
        }
        
        public void Move(Int32 oldIndex, Int32 newIndex)
        {
            Internal.Move(oldIndex, newIndex);
        }
        
        public IDisposable? Suppress()
        {
            return Internal.Suppress();
        }
    }
    
    public abstract class ObservableChainHandlerCollection<T, THandler, TCollection> : IObservableChainHandlerCollection<T, THandler> where THandler : IChainHandler<T> where TCollection : class, IObservableCollection<THandler>
    {
        protected TCollection Internal { get; }
        
        public event NotifyCollectionChangedEventHandler? CollectionChanged;
        public event PropertyChangingEventHandler? PropertyChanging;
        public event PropertyChangedEventHandler? PropertyChanged;
        
        public Int32 Count
        {
            get
            {
                return Internal.Count;
            }
        }
        
        public Boolean IsReadOnly
        {
            get
            {
                return Internal.IsReadOnly;
            }
        }
        
        protected ObservableChainHandlerCollection(TCollection collection)
        {
            Internal = collection ?? throw new ArgumentNullException(nameof(collection));
            
            Internal.CollectionChanged += OnCollectionChanged;
            Internal.PropertyChanging += OnPropertyChanging;
            Internal.PropertyChanged += OnPropertyChanged;
        }
        
        private void OnCollectionChanged(Object? sender, NotifyCollectionChangedEventArgs args)
        {
            CollectionChanged?.Invoke(this, args);
        }
        
        private void OnPropertyChanging(Object? sender, PropertyChangingEventArgs args)
        {
            PropertyChanging?.Invoke(this, args);
        }
        
        private void OnPropertyChanged(Object? sender, PropertyChangedEventArgs args)
        {
            PropertyChanged?.Invoke(this, args);
        }
        
        public T Handle(T value)
        {
            return Internal.Aggregate(value, (current, handler) => handler.Handle(current));
        }
        
        public Boolean Contains(THandler item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            
            return Internal.Contains(item);
        }
        
        public Int32 IndexOf(THandler item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            
            return Internal.IndexOf(item);
        }
        
        public void Add(THandler item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            
            Internal.Add(item);
        }
        
        public void Insert(Int32 index, THandler item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            
            Internal.Insert(index, item);
        }
        
        public Boolean Remove(THandler item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            
            return Internal.Remove(item);
        }
        
        public void RemoveAt(Int32 index)
        {
            Internal.RemoveAt(index);
        }
        
        public void Clear()
        {
            Internal.Clear();
        }
        
        public void CopyTo(THandler[] array, Int32 index)
        {
            Internal.CopyTo(array, index);
        }
        
        public IEnumerator<THandler> GetEnumerator()
        {
            return Internal.GetEnumerator();
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        
        public THandler this[Int32 index]
        {
            get
            {
                return Internal[index];
            }
            set
            {
                Internal[index] = value;
            }
        }
    }
}