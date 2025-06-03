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
    public class PaginationItemObservableCollection<T> : PaginationItemObservableCollection<T, ItemObservableCollection<T>> where T : class
    {
        public PaginationItemObservableCollection(Int32 size)
            : this(0, size)
        {
        }
        
        public PaginationItemObservableCollection(Int32 index, Int32 size)
            : base(new ItemObservableCollection<T>(), index, size)
        {
        }
        
        public PaginationItemObservableCollection(IEnumerable<T> collection, Int32 size)
            : this(collection, 0, size)
        {
        }
        
        public PaginationItemObservableCollection(IEnumerable<T> collection, Int32 index, Int32 size)
            : base(collection is not null ? new ItemObservableCollection<T>(collection) : throw new ArgumentNullException(nameof(collection)), index, size)
        {
        }
        
        public PaginationItemObservableCollection(List<T> list, Int32 size)
            : this(list, 0, size)
        {
        }
        
        public PaginationItemObservableCollection(List<T> list,Int32 index, Int32 size)
            : base(list is not null ? new ItemObservableCollection<T>(list) : throw new ArgumentNullException(nameof(list)), index, size)
        {
        }
        
        public override Pagination View()
        {
            return View(Index, Size);
        }
        
        public override Pagination View(Int32 size)
        {
            return View(0, size);
        }
        
        public override Pagination View(Int32 index, Int32 size)
        {
            return new Pagination(this, index, size);
        }
        
        public new class Pagination : PaginationItemObservableCollection<T, ItemObservableCollection<T>>.Pagination
        {
            protected override PaginationItemObservableCollection<T, ItemObservableCollection<T>> Internal { get; }
            
            public Pagination(PaginationItemObservableCollection<T, ItemObservableCollection<T>> @internal, Int32 index, Int32 size)
                : base(@internal, index, size)
            {
                Internal = @internal ?? throw new ArgumentNullException(nameof(@internal));
            }
        }
    }
    
    public class PaginationSuppressObservableCollection<T> : PaginationSuppressObservableCollection<T, SuppressObservableCollection<T>>
    {
        public PaginationSuppressObservableCollection(Int32 size)
            : this(0, size)
        {
        }
        
        public PaginationSuppressObservableCollection(Int32 index, Int32 size)
            : base(new SuppressObservableCollection<T>(), index, size)
        {
        }
        
        public PaginationSuppressObservableCollection(IEnumerable<T> collection, Int32 size)
            : this(collection, 0, size)
        {
        }
        
        public PaginationSuppressObservableCollection(IEnumerable<T> collection, Int32 index, Int32 size)
            : base(collection is not null ? new SuppressObservableCollection<T>(collection) : throw new ArgumentNullException(nameof(collection)), index, size)
        {
        }
        
        public PaginationSuppressObservableCollection(List<T> list, Int32 size)
            : this(list, 0, size)
        {
        }
        
        public PaginationSuppressObservableCollection(List<T> list,Int32 index, Int32 size)
            : base(list is not null ? new SuppressObservableCollection<T>(list) : throw new ArgumentNullException(nameof(list)), index, size)
        {
        }
        
        public override Pagination View()
        {
            return View(Index, Size);
        }
        
        public override Pagination View(Int32 size)
        {
            return View(0, size);
        }
        
        public override Pagination View(Int32 index, Int32 size)
        {
            return new Pagination(this, index, size);
        }
        
        public new class Pagination : PaginationSuppressObservableCollection<T, SuppressObservableCollection<T>>.Pagination
        {
            protected override PaginationSuppressObservableCollection<T, SuppressObservableCollection<T>> Internal { get; }
            
            public Pagination(PaginationSuppressObservableCollection<T, SuppressObservableCollection<T>> @internal, Int32 index, Int32 size)
                : base(@internal, index, size)
            {
                Internal = @internal ?? throw new ArgumentNullException(nameof(@internal));
            }
        }
    }
    
    public abstract class PaginationItemObservableCollection<T, TCollection> : PaginationSuppressObservableCollection<T, TCollection>, INotifyItemCollection where TCollection : class, ISuppressObservableCollection<T>, INotifyItemCollection
    {
        public event PropertyChangingEventHandler? ItemChanging;
        public event PropertyChangedEventHandler? ItemChanged;
        
        protected PaginationItemObservableCollection(TCollection collection, Int32 index, Int32 size)
            : base(collection, index, size)
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
        
        public override Pagination View()
        {
            return View(Index, Size);
        }
        
        public override Pagination View(Int32 size)
        {
            return View(0, size);
        }
        
        public abstract override Pagination View(Int32 index, Int32 size);
        
        public new abstract class Pagination : PaginationSuppressObservableCollection<T, TCollection>.Pagination, INotifyItemCollection
        {
            protected abstract override PaginationItemObservableCollection<T, TCollection> Internal { get; }
            
            public event PropertyChangingEventHandler? ItemChanging;
            public event PropertyChangedEventHandler? ItemChanged;
            
            protected Pagination(PaginationItemObservableCollection<T, TCollection> @internal, Int32 index, Int32 size)
                : base(@internal, index, size)
            {
                @internal.ItemChanging += OnItemChanging;
                @internal.ItemChanged += OnItemChanged;
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
    }

    public abstract class PaginationSuppressObservableCollection<T, TCollection> : PaginationObservableCollection<T, TCollection> where TCollection : class, ISuppressObservableCollection<T>
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
        
        protected PaginationSuppressObservableCollection(TCollection collection, Int32 index, Int32 size)
            : base(collection, index, size)
        {
        }
        
        public override Pagination View()
        {
            return View(Index, Size);
        }
        
        public override Pagination View(Int32 size)
        {
            return View(0, size);
        }
        
        public abstract override Pagination View(Int32 index, Int32 size);
        
        public void Move(Int32 oldIndex, Int32 newIndex)
        {
            Internal.Move(oldIndex, newIndex);
        }
        
        public IDisposable? Suppress()
        {
            return Internal.Suppress();
        }
        
        public new abstract class Pagination : PaginationObservableCollection<T, TCollection>.Pagination, IReadOnlySuppressObservableCollection<T>
        {
            protected abstract override PaginationSuppressObservableCollection<T, TCollection> Internal { get; }
            
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
            
            protected Pagination(PaginationSuppressObservableCollection<T, TCollection> @internal, Int32 index, Int32 size)
                : base(@internal, index, size)
            {
            }
            
            //TODO:
            public IDisposable? Suppress()
            {
                return null;
            }
        }
    }
    
    public abstract class PaginationObservableCollection<T, TCollection> : PaginationCollection, IPaginationCollection<T>, IObservableCollection<T> where TCollection : class, IObservableCollection<T>
    {
        protected TCollection Internal { get; }
        
        public event NotifyCollectionChangedEventHandler? CollectionChanged;
        public event PropertyChangingEventHandler? PropertyChanging;
        public event PropertyChangedEventHandler? PropertyChanged;
        
        public override Int32 Index
        {
            get
            {
                return base.Index;
            }
            protected set
            {
                if (Index == value)
                {
                    return;
                }
                
                this.RaisePropertyChanging();
                this.RaisePropertyChanging(nameof(Page));
                this.RaisePropertyChanging(nameof(Items));
                this.RaisePropertyChanging(nameof(HasPrevious));
                this.RaisePropertyChanging(nameof(HasNext));
                base.Index = value;
                this.RaisePropertyChanged();
                this.RaisePropertyChanged(nameof(Page));
                this.RaisePropertyChanged(nameof(Items));
                this.RaisePropertyChanged(nameof(HasPrevious));
                this.RaisePropertyChanged(nameof(HasNext));
            }
        }
        
        public sealed override Int32 Total
        {
            get
            {
                return Math.Abs((Int32) Math.Ceiling(Count / (Double) Size));
            }
        }
        
        public sealed override Int32 Items
        {
            get
            {
                return Math.Max(Math.Min(Size, Count - Index * Size), 0);
            }
        }
        
        public override Int32 Size
        {
            get
            {
                return base.Size;
            }
            protected set
            {
                if (Size == value)
                {
                    return;
                }
                
                this.RaisePropertyChanging();
                this.RaisePropertyChanging(nameof(Index));
                this.RaisePropertyChanging(nameof(Page));
                this.RaisePropertyChanging(nameof(Total));
                this.RaisePropertyChanging(nameof(Items));
                this.RaisePropertyChanging(nameof(HasPrevious));
                this.RaisePropertyChanging(nameof(HasNext));
                base.Size = value;
                this.RaisePropertyChanged();
                this.RaisePropertyChanged(nameof(Index));
                this.RaisePropertyChanged(nameof(Page));
                this.RaisePropertyChanged(nameof(Total));
                this.RaisePropertyChanged(nameof(Items));
                this.RaisePropertyChanged(nameof(HasPrevious));
                this.RaisePropertyChanged(nameof(HasNext));
            }
        }
        
        public override Boolean CanResize
        {
            get
            {
                return true;
            }
        }
        
        public override Int32 Count
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
        
        protected PaginationObservableCollection(TCollection collection, Int32 index, Int32 size)
            : base(index, size)
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
            
            if (args.PropertyName != nameof(Count))
            {
                return;
            }
            
            this.RaisePropertyChanging(nameof(Total));
            this.RaisePropertyChanging(nameof(Items));
            this.RaisePropertyChanging(nameof(HasPrevious));
            this.RaisePropertyChanging(nameof(HasNext));
        }
        
        private void OnPropertyChanged(Object? sender, PropertyChangedEventArgs args)
        {
            PropertyChanged?.Invoke(this, args);
            
            if (args.PropertyName != nameof(Count))
            {
                return;
            }
            
            this.RaisePropertyChanged(nameof(Total));
            this.RaisePropertyChanged(nameof(Items));
            this.RaisePropertyChanged(nameof(HasPrevious));
            this.RaisePropertyChanged(nameof(HasNext));
        }
        
        public virtual Pagination View()
        {
            return View(Index, Size);
        }
        
        public virtual Pagination View(Int32 size)
        {
            return View(0, size);
        }
        
        public abstract Pagination View(Int32 index, Int32 size);
        
        public Boolean Contains(T item)
        {
            return Internal.Contains(item);
        }
        
        public Int32 IndexOf(T item)
        {
            return Internal.IndexOf(item);
        }
        
        public void Add(T item)
        {
            Int32 items = Items;
            Int32 total = Total;
            
            Internal.Add(item);
            
            if (items != Items)
            {
                this.RaiseProperty(nameof(Items));
            }
            
            if (total != Total)
            {
                this.RaiseProperty(nameof(Total));
            }
        }
        
        public void Insert(Int32 index, T item)
        {
            Int32 items = Items;
            Int32 total = Total;
            
            Internal.Insert(index, item);
            
            if (items != Items)
            {
                this.RaiseProperty(nameof(Items));
            }
            
            if (total != Total)
            {
                this.RaiseProperty(nameof(Total));
            }
        }
        
        public Boolean Remove(T item)
        {
            Int32 items = Items;
            Int32 total = Total;
            
            if (!Internal.Remove(item))
            {
                return false;
            }
            
            if (items != Items)
            {
                this.RaiseProperty(nameof(Items));
            }
            
            if (total != Total)
            {
                this.RaiseProperty(nameof(Total));
            }
            
            return true;
        }
        
        public void RemoveAt(Int32 index)
        {
            Int32 items = Items;
            Int32 total = Total;
            
            Internal.RemoveAt(index);
            
            if (items != Items)
            {
                this.RaiseProperty(nameof(Items));
            }
            
            if (total != Total)
            {
                this.RaiseProperty(nameof(Total));
            }
        }
        
        public void Clear()
        {
            Int32 items = Items;
            Int32 total = Total;
            
            Internal.Clear();
            
            if (items != Items)
            {
                this.RaiseProperty(nameof(Items));
            }
            
            if (total != Total)
            {
                this.RaiseProperty(nameof(Total));
            }
        }
        
        public void CopyTo(T[] array, Int32 index)
        {
            Internal.CopyTo(array, index);
        }
        
        public override IEnumerator<T> GetEnumerator()
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
                return Internal[index];
            }
            set
            {
                Internal[index] = value;
            }
        }
        
        public abstract class Pagination : PaginationCollection, IPaginationReadOnlyCollection<T>, IReadOnlyObservableCollection<T>
        {
            protected abstract PaginationObservableCollection<T, TCollection> Internal { get; }
            
            public event NotifyCollectionChangedEventHandler? CollectionChanged;
            public event PropertyChangingEventHandler? PropertyChanging;
            public event PropertyChangedEventHandler? PropertyChanged;
            
            public override Int32 Index
            {
                get
                {
                    return base.Index;
                }
                protected set
                {
                    if (Index == value)
                    {
                        return;
                    }
                    
                    this.RaisePropertyChanging();
                    this.RaisePropertyChanging(nameof(Page));
                    this.RaisePropertyChanging(nameof(Items));
                    this.RaisePropertyChanging(nameof(HasPrevious));
                    this.RaisePropertyChanging(nameof(HasNext));
                    base.Index = value;
                    this.RaisePropertyChanged();
                    this.RaisePropertyChanged(nameof(Page));
                    this.RaisePropertyChanged(nameof(Items));
                    this.RaisePropertyChanged(nameof(HasPrevious));
                    this.RaisePropertyChanged(nameof(HasNext));
                }
            }
            
            public sealed override Int32 Total
            {
                get
                {
                    return Math.Abs((Int32) Math.Ceiling(Count / (Double) Size));
                }
            }
            
            //TODO: fix, неправильное количество на странице
            public sealed override Int32 Items
            {
                get
                {
                    return Math.Max(Math.Min(Size, Count - Index * Size), 0);
                }
            }
            
            public override Int32 Size
            {
                get
                {
                    return base.Size;
                }
                protected set
                {
                    if (Size == value)
                    {
                        return;
                    }
                    
                    this.RaisePropertyChanging();
                    this.RaisePropertyChanging(nameof(Index));
                    this.RaisePropertyChanging(nameof(Page));
                    this.RaisePropertyChanging(nameof(Total));
                    this.RaisePropertyChanging(nameof(Items));
                    this.RaisePropertyChanging(nameof(HasPrevious));
                    this.RaisePropertyChanging(nameof(HasNext));
                    base.Size = value;
                    this.RaisePropertyChanged();
                    this.RaisePropertyChanged(nameof(Index));
                    this.RaisePropertyChanged(nameof(Page));
                    this.RaisePropertyChanged(nameof(Total));
                    this.RaisePropertyChanged(nameof(Items));
                    this.RaisePropertyChanged(nameof(HasPrevious));
                    this.RaisePropertyChanged(nameof(HasNext));
                }
            }
            
            public override Boolean CanResize
            {
                get
                {
                    return true;
                }
            }
            
            public sealed override Int32 Count
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
            
            protected Pagination(PaginationObservableCollection<T, TCollection> @internal, Int32 index, Int32 size)
                : base(index, size)
            {
                if (@internal is null)
                {
                    throw new ArgumentNullException(nameof(@internal));
                }
                
                @internal.CollectionChanged += OnCollectionChanged;
                @internal.PropertyChanging += OnPropertyChanging;
                @internal.PropertyChanged += OnPropertyChanged;
            }
            
            //TODO:
            protected void OnCollectionChanged(Object? sender, NotifyCollectionChangedEventArgs args)
            {
                CollectionChanged?.Invoke(this, args);
            }
            
            private void OnPropertyChanging(Object? sender, PropertyChangingEventArgs args)
            {
                if (args.PropertyName != nameof(Count))
                {
                    return;
                }
                
                PropertyChanging?.Invoke(this, args);
                
                this.RaisePropertyChanged(nameof(Total));
                this.RaisePropertyChanged(nameof(Items));
                this.RaisePropertyChanged(nameof(HasPrevious));
                this.RaisePropertyChanged(nameof(HasNext));
            }
            
            private void OnPropertyChanged(Object? sender, PropertyChangedEventArgs args)
            {
                if (args.PropertyName != nameof(Count))
                {
                    return;
                }
                
                PropertyChanged?.Invoke(this, args);
                
                this.RaisePropertyChanged(nameof(Total));
                this.RaisePropertyChanged(nameof(Items));
                this.RaisePropertyChanged(nameof(HasPrevious));
                this.RaisePropertyChanged(nameof(HasNext));
            }
            
            public Boolean MovePrevious()
            {
                if (!HasPrevious)
                {
                    return false;
                }
                
                Index--;
                return true;
            }
            
            public Boolean MoveNext()
            {
                if (!HasNext)
                {
                    return false;
                }
                
                Index++;
                return true;
            }
            
            public Boolean Contains(T item)
            {
                return IndexOf(item) != -1;
            }
            
            public Int32 IndexOf(T item)
            {
                Int32 start = Index * Size;
                for (Int32 i = start; i < start + Items - 1; i++)
                {
                    if (EqualityComparer<T>.Default.Equals(Internal[i], item))
                    {
                        return i - start;
                    }
                }
                
                return -1;
            }
            
            public T this[Int32 index]
            {
                get
                {
                    if (index < 0 || index >= Items)
                    {
                        throw new ArgumentOutOfRangeException(nameof(index), index, null);
                    }
                    
                    return Internal[index + Index * Size];
                }
                set
                {
                    if (index < 0 || index >= Items)
                    {
                        throw new ArgumentOutOfRangeException(nameof(index), index, null);
                    }
                    
                    Internal[index + Index * Size] = value;
                }
            }
            
            public override IEnumerator<T> GetEnumerator()
            {
                Int32 start = Index * Size;
                for (Int32 i = start; i < start + Items; i++)
                {
                    yield return Internal[i];
                }
            }
            
            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
    }
}