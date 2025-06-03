// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using NetExtender.Interfaces.Notify;
using NetExtender.Types.Collections.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Collections
{
    public class ItemObservableAutoCollection<T> : ItemObservableAutoCollection<T, ItemObservableCollection<T>> where T : class
    {
        public ItemObservableAutoCollection(Func<Int32, T> generator)
            : base(new ItemObservableCollection<T>(), generator)
        {
        }
        
        public ItemObservableAutoCollection(IEnumerable<T> collection, Func<Int32, T> generator)
            : base(new ItemObservableCollection<T>(collection), generator)
        {
        }
        
        public ItemObservableAutoCollection(List<T> list, Func<Int32, T> generator)
            : base(new ItemObservableCollection<T>(list), generator)
        {
        }
    }
    
    public class SuppressObservableAutoCollection<T> : SuppressObservableAutoCollection<T, SuppressObservableCollection<T>>
    {
        public SuppressObservableAutoCollection(Func<Int32, T> generator)
            : base(new SuppressObservableCollection<T>(), generator)
        {
        }
        
        public SuppressObservableAutoCollection(IEnumerable<T> collection, Func<Int32, T> generator)
            : base(new SuppressObservableCollection<T>(collection), generator)
        {
        }
        
        public SuppressObservableAutoCollection(List<T> list, Func<Int32, T> generator)
            : base(new SuppressObservableCollection<T>(list), generator)
        {
        }
    }
    
    public abstract class ItemObservableAutoCollection<T, TCollection> : SuppressObservableAutoCollection<T, TCollection>, INotifyItemCollection where TCollection : class, ISuppressObservableCollection<T>, INotifyItemCollection
    {
        public event PropertyChangingEventHandler? ItemChanging;
        public event PropertyChangedEventHandler? ItemChanged;
        
        protected ItemObservableAutoCollection(TCollection collection, Func<Int32, T> generator)
            : base(collection, generator)
        {
            collection.ItemChanging += OnItemChanging;
            collection.ItemChanged += OnItemChanged;
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
    
    public abstract class SuppressObservableAutoCollection<T, TCollection> : ObservableAutoCollection<T, TCollection>, ISuppressObservableAutoCollection<T> where TCollection : class, ISuppressObservableCollection<T>
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
        
        protected SuppressObservableAutoCollection(TCollection collection, Func<Int32, T> generator)
            : base(collection, generator)
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
    
    public abstract class ObservableAutoCollection<T, TCollection> : IObservableAutoCollection<T> where TCollection : class, IObservableCollection<T>
    {
        protected TCollection Internal { get; }
        public Func<Int32, T> Generator { get; }
        
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

        protected ObservableAutoCollection(TCollection collection, Func<Int32, T> generator)
        {
            Internal = collection ?? throw new ArgumentNullException(nameof(collection));
            Generator = generator ?? throw new ArgumentNullException(nameof(generator));
            
            collection.CollectionChanged += OnCollectionChanged;
            collection.PropertyChanging += OnPropertyChanging;
            collection.PropertyChanged += OnPropertyChanged;
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
            PropertyChanged?.Invoke(sender, args);
        }
        
        protected virtual IEnumerable<T> Factory(Int32 start, Int32 count)
        {
            for (Int32 i = start; i < start + count; i++)
            {
                yield return Generator(i);
            }
        }

        public Boolean Contains(T item)
        {
            return Internal.Contains(item);
        }

        public Int32 IndexOf(T item)
        {
            return Internal.IndexOf(item);
        }

        public void Set(Int32 count)
        {
            if (count <= Count)
            {
                return;
            }
            
            Internal.AddRange(Factory(Count, Count - count));
        }

        public void Fill(Int32 count)
        {
            Fill(0, count);
        }

        public void Fill(Int32 start, Int32 count)
        {
            Internal.Replace(start, Factory(start, count));
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
        
        public void Reset()
        {
            Reset(Count);
        }
        
        public void Reset(Int32 count)
        {
            Internal.Reload(Factory(0, count));
        }
        
        public void CopyTo(T[] array, Int32 index)
        {
            Internal.CopyTo(array, index);
        }
        
        public IEnumerator<T> GetEnumerator()
        {
            return Internal.GetEnumerator();
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        
        public T this[Int32 index]
        {
            get
            {
                if (index < 0 || index > Count * 3 && Count > UInt16.MaxValue)
                {
                    throw new ArgumentOutOfRangeException(nameof(index), index, null);
                }
                
                if (index >= Count)
                {
                    Set(index + 1);
                }
                
                return Internal[index];
            }
            set
            {
                if (index < 0 || index > Count * 3 && Count > UInt16.MaxValue)
                {
                    throw new ArgumentOutOfRangeException(nameof(index), index, null);
                }
                
                if (index >= Count)
                {
                    Set(index + 1);
                }
                
                Internal[index] = value;
            }
        }
    }
}