// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using NetExtender.Types.Collections.Interfaces;

namespace NetExtender.Types.Collections
{
    internal interface ISuppressObservableCollectionHandler
    {
        public Boolean IsAllowSuppress { get; }
        public Boolean IsSuppressed { get; }
        public Int32 SuppressDepth { get; }

        public IDisposable? Suppress();
        public Boolean Unsuppress();
    }

    public class SuppressObservableCollection<T> : ObservableCollection<T>, ISuppressObservableCollection<T>, IReadOnlySuppressObservableCollection<T>, ISuppressObservableCollectionHandler
    {
        private event PropertyChangingEventHandler? PropertyChanging;
        event PropertyChangingEventHandler? INotifyPropertyChanging.PropertyChanging
        {
            add
            {
                PropertyChanging += value;
            }
            remove
            {
                PropertyChanging -= value;
            }
        }

        private ObservableCollectionSuppressionHandler Suppression { get; }
        protected Boolean IsChanged { get; set; }
        public Boolean IsAllowSuppress { get; set; } = true;

        public Boolean IsSuppressed
        {
            get
            {
                return IsAllowSuppress && Suppression.IsSuppressed;
            }
        }

        public Int32 SuppressDepth
        {
            get
            {
                return Suppression.Count;
            }
        }

        public SuppressObservableCollection()
        {
            Suppression = new ObservableCollectionSuppressionHandler(this);
        }

        public SuppressObservableCollection(IEnumerable<T> collection)
            : base(collection)
        {
            Suppression = new ObservableCollectionSuppressionHandler(this);
        }

        public SuppressObservableCollection(List<T> list)
            : base(list)
        {
            Suppression = new ObservableCollectionSuppressionHandler(this);
        }

        public void AddRange(IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            using IDisposable? suppress = Suppress();
            
            foreach (T item in source)
            {
                Add(item);
            }
        }
        
        public void RemoveRange(IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            using IDisposable? suppress = Suppress();
            
            foreach (T item in source)
            {
                Remove(item);
            }
        }

        public virtual IDisposable? Suppress()
        {
            return IsAllowSuppress ? Suppression.Suppress() : null;
        }

        Boolean ISuppressObservableCollectionHandler.Unsuppress()
        {
            return Unsuppress();
        }

        protected virtual Boolean Unsuppress()
        {
            if (!IsAllowSuppress)
            {
                return false;
            }

            if (IsSuppressed || !IsChanged)
            {
                return !IsSuppressed;
            }

            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            IsChanged = false;
            return true;
        }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs args)
        {
            if (IsSuppressed)
            {
                IsChanged = true;
                return;
            }

            base.OnCollectionChanged(args);
        }

        protected virtual void OnPropertyChanging(PropertyChangingEventArgs args)
        {
            if (IsSuppressed)
            {
                return;
            }

            PropertyChanging?.Invoke(this, args);
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            if (IsSuppressed)
            {
                return;
            }

            base.OnPropertyChanged(args);
        }
    }

    public sealed class SuppressObservableCollectionWrapper<T> : ISuppressObservableCollection<T>, IReadOnlySuppressObservableCollection<T>, ISuppressObservableCollectionHandler
    {
        private ObservableCollection<T> Internal { get; }
        public event NotifyCollectionChangedEventHandler? CollectionChanged;
        
        private event PropertyChangingEventHandler? PropertyChanging;
        event PropertyChangingEventHandler? INotifyPropertyChanging.PropertyChanging
        {
            add
            {
                PropertyChanging += value;
            }
            remove
            {
                PropertyChanging -= value;
            }
        }
        
        private event PropertyChangedEventHandler? PropertyChanged;
        event PropertyChangedEventHandler? INotifyPropertyChanged.PropertyChanged
        {
            add
            {
                PropertyChanged += value;
            }
            remove
            {
                PropertyChanged -= value;
            }
        }

        private ObservableCollectionSuppressionHandler Suppression { get; }
        private Boolean IsChanged { get; set; }

        public Boolean IsAllowSuppress { get; set; } = true;

        public Boolean IsSuppressed
        {
            get
            {
                return IsAllowSuppress && Suppression.IsSuppressed;
            }
        }

        public Int32 SuppressDepth
        {
            get
            {
                return Suppression.Count;
            }
        }

        public Int32 Count
        {
            get
            {
                return Internal.Count;
            }
        }

        Boolean ICollection<T>.IsReadOnly
        {
            get
            {
                return ((ICollection<T>) Internal).IsReadOnly;
            }
        }

        public SuppressObservableCollectionWrapper(ObservableCollection<T> collection)
        {
            Internal = collection ?? throw new ArgumentNullException(nameof(collection));
            Internal.CollectionChanged += OnCollectionChanged;

            if (Internal is INotifyPropertyChanging changing)
            {
                changing.PropertyChanging += OnPropertyChanging;
            }

            if (Internal is INotifyPropertyChanged changed)
            {
                changed.PropertyChanged += OnPropertyChanged;
            }
            
            Suppression = new ObservableCollectionSuppressionHandler(this);
        }

        private void OnCollectionChanged(Object? sender, NotifyCollectionChangedEventArgs args)
        {
            if (IsSuppressed)
            {
                return;
            }

            CollectionChanged?.Invoke(this, args);
        }

        private void OnPropertyChanging(Object? sender, PropertyChangingEventArgs args)
        {
            if (IsSuppressed)
            {
                return;
            }

            PropertyChanging?.Invoke(this, args);
        }

        private void OnPropertyChanged(Object? sender, PropertyChangedEventArgs args)
        {
            if (IsSuppressed)
            {
                return;
            }

            PropertyChanged?.Invoke(this, args);
        }

        public IDisposable? Suppress()
        {
            return IsAllowSuppress ? Suppression.Suppress() : null;
        }

        Boolean ISuppressObservableCollectionHandler.Unsuppress()
        {
            return Unsuppress();
        }

        private Boolean Unsuppress()
        {
            if (!IsAllowSuppress)
            {
                return false;
            }

            if (IsSuppressed || !IsChanged)
            {
                return !IsSuppressed;
            }

            OnCollectionChanged(Internal, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            IsChanged = false;
            return true;
        }

        public Boolean Contains(T item)
        {
            return Internal.Contains(item);
        }

        public Int32 IndexOf(T item)
        {
            return Internal.IndexOf(item);
        }

        public void Move(Int32 oldIndex, Int32 newIndex)
        {
            Internal.Move(oldIndex, newIndex);
        }

        public void Add(T item)
        {
            Internal.Add(item);
        }

        public void Insert(Int32 index, T item)
        {
            Internal.Insert(index, item);
        }

        public Boolean Remove(T item)
        {
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

        public void CopyTo(T[] array, Int32 arrayIndex)
        {
            Internal.CopyTo(array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Internal.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) Internal).GetEnumerator();
        }

        public T this[Int32 index]
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

    internal sealed class ObservableCollectionSuppressionHandler : IDisposable
    {
        private ISuppressObservableCollectionHandler Collection { get; }
        public Int32 Count { get; private set; }

        public Boolean IsSuppressed
        {
            get
            {
                return Count > 0;
            }
        }

        public ObservableCollectionSuppressionHandler(ISuppressObservableCollectionHandler collection)
        {
            Collection = collection ?? throw new ArgumentNullException(nameof(collection));
        }

        public IDisposable Suppress()
        {
            Count++;
            return this;
        }

        public void Dispose()
        {
            if (Count <= 0)
            {
                return;
            }

            Count--;
            Collection.Unsuppress();
        }
    }
}