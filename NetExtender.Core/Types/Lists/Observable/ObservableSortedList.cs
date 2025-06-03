using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using NetExtender.Types.Collections;
using NetExtender.Types.Exceptions;
using NetExtender.Types.Lists.Interfaces;
using NetExtender.Utilities.Core;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Lists
{
    public class ItemObservableSortedList<T> : ItemObservableSortedList<T, ItemObservableCollection<T>, ItemObservableSortedList<T>> where T : class
    {
        public ItemObservableSortedList()
        {
        }

        public ItemObservableSortedList(IComparer<T>? comparer)
            : base(comparer)
        {
        }

        public ItemObservableSortedList(IEnumerable<T>? collection)
            : base(collection)
        {
        }

        public ItemObservableSortedList(IEnumerable<T>? collection, IComparer<T>? comparer)
            : base(collection, comparer)
        {
        }

        public ItemObservableSortedList(List<T>? list)
            : base(list)
        {
        }

        public ItemObservableSortedList(List<T>? list, IComparer<T>? comparer)
            : base(list, comparer)
        {
        }

        public ItemObservableSortedList(Comparison<T>? comparison)
            : base(comparison)
        {
        }

        protected ItemObservableSortedList(ItemObservableCollection<T> collection, IComparer<T>? comparer)
            : base(collection, comparer)
        {
        }

        protected sealed override ItemObservableSortedList<T> Self(ItemObservableCollection<T> collection, IComparer<T>? comparer)
        {
            return new ItemObservableSortedList<T>(collection, comparer);
        }
    }
    
    public class ObservableSortedList<T> : SuppressObservableSortedList<T, SuppressObservableCollection<T>, ObservableSortedList<T>>
    {
        public ObservableSortedList()
        {
        }

        public ObservableSortedList(IComparer<T>? comparer)
            : base(comparer)
        {
        }

        public ObservableSortedList(Comparison<T>? comparison)
            : base(comparison)
        {
        }

        public ObservableSortedList(IEnumerable<T>? collection)
            : base(collection)
        {
        }

        public ObservableSortedList(IEnumerable<T>? collection, IComparer<T>? comparer)
            : base(collection, comparer)
        {
        }

        public ObservableSortedList(IEnumerable<T>? collection, Comparison<T>? comparison)
            : base(collection, comparison)
        {
        }

        public ObservableSortedList(List<T>? list)
            : base(list)
        {
        }

        public ObservableSortedList(List<T>? list, IComparer<T>? comparer)
            : base(list, comparer)
        {
        }

        public ObservableSortedList(List<T>? list, Comparison<T>? comparison)
            : base(list, comparison)
        {
        }

        protected ObservableSortedList(SuppressObservableCollection<T> collection, IComparer<T>? comparer)
            : base(collection, comparer)
        {
        }

        protected sealed override ObservableSortedList<T> Self(SuppressObservableCollection<T> collection, IComparer<T>? comparer)
        {
            return new ObservableSortedList<T>(collection, comparer);
        }
    }

    public class ItemObservableSortedList<T, TCollection> : ItemObservableSortedList<T, TCollection, ItemObservableSortedList<T, TCollection>> where T : class where TCollection : ItemObservableCollection<T>, new()
    {
        public ItemObservableSortedList()
        {
        }

        public ItemObservableSortedList(IComparer<T>? comparer)
            : base(comparer)
        {
        }

        public ItemObservableSortedList(Comparison<T>? comparison)
            : base(comparison)
        {
        }

        public ItemObservableSortedList(IEnumerable<T>? collection)
            : base(collection)
        {
        }

        public ItemObservableSortedList(IEnumerable<T>? collection, IComparer<T>? comparer)
            : base(collection, comparer)
        {
        }

        public ItemObservableSortedList(IEnumerable<T>? collection, Comparison<T>? comparison)
            : base(collection, comparison)
        {
        }

        public ItemObservableSortedList(List<T>? list)
            : base(list)
        {
        }

        public ItemObservableSortedList(List<T>? list, IComparer<T>? comparer)
            : base(list, comparer)
        {
        }

        public ItemObservableSortedList(List<T>? list, Comparison<T>? comparison)
            : base(list, comparison)
        {
        }

        protected ItemObservableSortedList(TCollection collection, IComparer<T>? comparer)
            : base(collection, comparer)
        {
        }

        protected sealed override ItemObservableSortedList<T, TCollection> Self(TCollection collection, IComparer<T>? comparer)
        {
            return new ItemObservableSortedList<T, TCollection>(collection, comparer);
        }
    }

    public abstract class ItemObservableSortedList<T, TCollection, TSelf> : SuppressObservableSortedList<T, TCollection, TSelf>, IItemObservableSortedList<T>, IReadOnlyItemObservableSortedList<T> where T : class where TCollection : ItemObservableCollection<T>, new() where TSelf : ItemObservableSortedList<T, TCollection, TSelf>
    {
        public event PropertyChangingEventHandler? ItemChanging;
        public event PropertyChangedEventHandler? ItemChanged;

        protected ItemObservableSortedList()
        {
            Internal.ItemChanging += OnItemChanging;
            Internal.ItemChanged += OnItemChanged;
        }

        protected ItemObservableSortedList(IComparer<T>? comparer)
            : base(comparer)
        {
            Internal.ItemChanging += OnItemChanging;
            Internal.ItemChanged += OnItemChanged;
        }

        protected ItemObservableSortedList(Comparison<T>? comparison)
            : base(comparison)
        {
            Internal.ItemChanging += OnItemChanging;
            Internal.ItemChanged += OnItemChanged;
        }

        protected ItemObservableSortedList(IEnumerable<T>? collection)
            : base(collection)
        {
            Internal.ItemChanging += OnItemChanging;
            Internal.ItemChanged += OnItemChanged;
        }

        protected ItemObservableSortedList(IEnumerable<T>? collection, IComparer<T>? comparer)
            : base(collection, comparer)
        {
            Internal.ItemChanging += OnItemChanging;
            Internal.ItemChanged += OnItemChanged;
        }

        protected ItemObservableSortedList(IEnumerable<T>? collection, Comparison<T>? comparison)
            : base(collection, comparison)
        {
            Internal.ItemChanging += OnItemChanging;
            Internal.ItemChanged += OnItemChanged;
        }

        protected ItemObservableSortedList(List<T>? list)
            : base(list)
        {
            Internal.ItemChanging += OnItemChanging;
            Internal.ItemChanged += OnItemChanged;
        }

        protected ItemObservableSortedList(List<T>? list, IComparer<T>? comparer)
            : base(list, comparer)
        {
            Internal.ItemChanging += OnItemChanging;
            Internal.ItemChanged += OnItemChanged;
        }

        protected ItemObservableSortedList(List<T>? list, Comparison<T>? comparison)
            : base(list, comparison)
        {
            Internal.ItemChanging += OnItemChanging;
            Internal.ItemChanged += OnItemChanged;
        }
        
        protected ItemObservableSortedList(TCollection collection, IComparer<T>? comparer)
            : base(collection, comparer)
        {
            Internal.ItemChanging += OnItemChanging;
            Internal.ItemChanged += OnItemChanged;
        }
        
        private void OnItemChanging(Object? sender, PropertyChangingEventArgs args)
        {
            ItemChanging?.Invoke(sender, args);
        }
        
        private void OnItemChanged(Object? sender, PropertyChangedEventArgs args)
        {
            ItemChanged?.Invoke(sender, args);
        }
    }

    public class SuppressObservableSortedList<T, TCollection> : SuppressObservableSortedList<T, TCollection, SuppressObservableSortedList<T, TCollection>> where TCollection : SuppressObservableCollection<T>, new()
    {
        public SuppressObservableSortedList()
        {
        }

        public SuppressObservableSortedList(IComparer<T>? comparer)
            : base(comparer)
        {
        }

        public SuppressObservableSortedList(Comparison<T>? comparison)
            : base(comparison)
        {
        }

        public SuppressObservableSortedList(IEnumerable<T>? collection)
            : base(collection)
        {
        }

        public SuppressObservableSortedList(IEnumerable<T>? collection, IComparer<T>? comparer)
            : base(collection, comparer)
        {
        }

        public SuppressObservableSortedList(IEnumerable<T>? collection, Comparison<T>? comparison)
            : base(collection, comparison)
        {
        }

        public SuppressObservableSortedList(List<T>? list)
            : base(list)
        {
        }

        public SuppressObservableSortedList(List<T>? list, IComparer<T>? comparer)
            : base(list, comparer)
        {
        }

        public SuppressObservableSortedList(List<T>? list, Comparison<T>? comparison)
            : base(list, comparison)
        {
        }

        protected SuppressObservableSortedList(TCollection collection, IComparer<T>? comparer)
            : base(collection, comparer)
        {
        }

        protected sealed override SuppressObservableSortedList<T, TCollection> Self(TCollection collection, IComparer<T>? comparer)
        {
            return new SuppressObservableSortedList<T, TCollection>(collection, comparer);
        }
    }

    public abstract class SuppressObservableSortedList<T, TCollection, TSelf> : ObservableSortedList<T, TCollection, TSelf>, ISuppressObservableSortedList<T>, IReadOnlySuppressObservableSortedList<T> where TCollection : SuppressObservableCollection<T>, new() where TSelf : SuppressObservableSortedList<T, TCollection, TSelf>
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

        protected SuppressObservableSortedList()
        {
        }

        protected SuppressObservableSortedList(IComparer<T>? comparer)
            : base(comparer)
        {
        }

        protected SuppressObservableSortedList(Comparison<T>? comparison)
            : base(comparison)
        {
        }

        protected SuppressObservableSortedList(IEnumerable<T>? collection)
            : base(collection)
        {
        }

        protected SuppressObservableSortedList(IEnumerable<T>? collection, IComparer<T>? comparer)
            : base(collection, comparer)
        {
        }

        protected SuppressObservableSortedList(IEnumerable<T>? collection, Comparison<T>? comparison)
            : base(collection, comparison)
        {
        }

        protected SuppressObservableSortedList(List<T>? list)
            : base(list)
        {
        }

        protected SuppressObservableSortedList(List<T>? list, IComparer<T>? comparer)
            : base(list, comparer)
        {
        }

        protected SuppressObservableSortedList(List<T>? list, Comparison<T>? comparison)
            : base(list, comparison)
        {
        }
        
        protected SuppressObservableSortedList(TCollection collection, IComparer<T>? comparer)
            : base(collection, comparer)
        {
        }

        public IDisposable? Suppress()
        {
            return Internal.Suppress();
        }
    }

    public class ObservableSortedList<T, TCollection> : ObservableSortedList<T, TCollection, ObservableSortedList<T, TCollection>> where TCollection : ObservableCollection<T>, new()
    {
        public ObservableSortedList()
        {
        }

        public ObservableSortedList(IComparer<T>? comparer)
            : base(comparer)
        {
        }

        public ObservableSortedList(Comparison<T>? comparison)
            : base(comparison)
        {
        }

        public ObservableSortedList(IEnumerable<T>? collection)
            : base(collection)
        {
        }

        public ObservableSortedList(IEnumerable<T>? collection, IComparer<T>? comparer)
            : base(collection, comparer)
        {
        }

        public ObservableSortedList(IEnumerable<T>? collection, Comparison<T>? comparison)
            : base(collection, comparison)
        {
        }

        public ObservableSortedList(List<T>? list)
            : base(list)
        {
        }

        public ObservableSortedList(List<T>? list, IComparer<T>? comparer)
            : base(list, comparer)
        {
        }

        public ObservableSortedList(List<T>? list, Comparison<T>? comparison)
            : base(list, comparison)
        {
        }

        protected ObservableSortedList(TCollection collection, IComparer<T>? comparer)
            : base(collection, comparer)
        {
        }

        protected sealed override ObservableSortedList<T, TCollection> Self(TCollection collection, IComparer<T>? comparer)
        {
            return new ObservableSortedList<T, TCollection>(collection, comparer);
        }
    }

    [SuppressMessage("ReSharper", "ForCanBeConvertedToForeach")]
    [SuppressMessage("ReSharper", "LoopCanBeConvertedToQuery")]
    public abstract class ObservableSortedList<T, TCollection, TSelf> : IObservableSortedList<T>, IReadOnlyObservableSortedList<T> where TCollection : ObservableCollection<T>, new() where TSelf : ObservableSortedList<T, TCollection, TSelf>
    {
        public event NotifyCollectionChangedEventHandler? CollectionChanged;
        public event PropertyChangingEventHandler? PropertyChanging;
        public event PropertyChangedEventHandler? PropertyChanged;
        
        private protected TCollection Internal { get; }

        private List<T> List
        {
            get
            {
                return Internal.InternalList() ?? throw new NeverOperationException();
            }
        }

        public IComparer<T> Comparer { get; }

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

        protected ObservableSortedList()
            : this(default(IComparer<T>))
        {
        }

        protected ObservableSortedList(IComparer<T>? comparer)
        {
            Internal = new TCollection();
            Comparer = comparer ?? Comparer<T>.Default;
            
            Internal.CollectionChanged += OnCollectionChanged;

            if (Internal is INotifyPropertyChanging changing)
            {
                changing.PropertyChanging += OnPropertyChanging;
            }

            if (Internal is INotifyPropertyChanged changed)
            {
                changed.PropertyChanged += OnPropertyChanged;
            }
        }

        protected ObservableSortedList(Comparison<T>? comparison)
            : this(comparison?.ToComparer())
        {
        }

        protected ObservableSortedList(IEnumerable<T>? collection)
            : this(collection, default(IComparer<T>))
        {
        }

        protected ObservableSortedList(IEnumerable<T>? collection, IComparer<T>? comparer)
        {
            Comparer = comparer ?? Comparer<T>.Default;
            Internal = Create(collection, Comparer);

            Internal.CollectionChanged += OnCollectionChanged;

            if (Internal is INotifyPropertyChanging changing)
            {
                changing.PropertyChanging += OnPropertyChanging;
            }

            if (Internal is INotifyPropertyChanged changed)
            {
                changed.PropertyChanged += OnPropertyChanged;
            }
        }

        protected ObservableSortedList(IEnumerable<T>? collection, Comparison<T>? comparison)
            : this(collection, comparison?.ToComparer())
        {
        }

        protected ObservableSortedList(List<T>? list)
            : this(list, default(IComparer<T>))
        {
        }

        protected ObservableSortedList(List<T>? list, IComparer<T>? comparer)
            : this((IEnumerable<T>?) list, comparer)
        {
        }

        protected ObservableSortedList(List<T>? list, Comparison<T>? comparison)
            : this(list, comparison?.ToComparer())
        {
        }

        protected ObservableSortedList(TCollection collection, IComparer<T>? comparer)
        {
            Internal = collection ?? throw new ArgumentNullException(nameof(collection));
            Comparer = comparer ?? Comparer<T>.Default;

            Internal.CollectionChanged += OnCollectionChanged;

            if (Internal is INotifyPropertyChanging changing)
            {
                changing.PropertyChanging += OnPropertyChanging;
            }

            if (Internal is INotifyPropertyChanged changed)
            {
                changed.PropertyChanged += OnPropertyChanged;
            }
        }

        private static TCollection Initialize(IEnumerable<T>? collection)
        {
            if (collection is null)
            {
                return new TCollection();
            }

            Func<IEnumerable<T>, TCollection> constructor;

            try
            {
                constructor = ReflectionUtilities.New<TCollection, IEnumerable<T>>();
            }
            catch (MissingMemberException)
            {
                return new TCollection().Rewrite(collection);
            }

            return constructor.Invoke(collection);
        }
        
        private static TCollection Initialize(List<T>? list)
        {
            if (list is null)
            {
                return new TCollection();
            }

            Func<List<T>, TCollection> constructor;

            try
            {
                constructor = ReflectionUtilities.New<TCollection, List<T>>();
            }
            catch (MissingMemberException)
            {
                return Initialize((IEnumerable<T>) list);
            }

            return constructor.Invoke(list);
        }

        protected TSelf Self(TCollection collection)
        {
            return Self(collection, Comparer);
        }

        protected abstract TSelf Self(TCollection collection, IComparer<T>? comparer);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static TCollection Create(IEnumerable<T>? collection, IComparer<T>? comparer)
        {
            return Initialize(collection?.OrderBy(comparer));
        }

        private void OnCollectionChanged(Object? _, NotifyCollectionChangedEventArgs args)
        {
            CollectionChanged?.Invoke(this, args);
        }

        private void OnPropertyChanging(Object? _, PropertyChangingEventArgs args)
        {
            PropertyChanging?.Invoke(this, args);
        }

        private void OnPropertyChanged(Object? _, PropertyChangedEventArgs args)
        {
            PropertyChanged?.Invoke(this, args);
        }

        public Boolean Contains(T item)
        {
            return BinarySearch(item) >= 0;
        }

        public Boolean Exists(Predicate<T> match)
        {
            if (match is null)
            {
                throw new ArgumentNullException(nameof(match));
            }

            for (Int32 i = 0; i < Internal.Count; i++)
            {
                if (match(Internal[i]))
                {
                    return true;
                }
            }

            return false;
        }

        public Int32 IndexOf(T item)
        {
            Int32 index = BinarySearch(item);
            
            if (index < 0)
            {
                return -1;
            }

            while (--index >= 0 && EqualityComparer<T>.Default.Equals(Internal[index], item))
            {
            }

            return index + 1;
        }

        public Int32 IndexOf(T item, Int32 index)
        {
            if (index < 0)
            {
                return IndexOf(item);
            }
            
            index = BinarySearch(index, item);

            if (index < 0)
            {
                return -1;
            }

            while (--index >= 0 && EqualityComparer<T>.Default.Equals(Internal[index], item))
            {
            }

            return index + 1;
        }

        public Int32 IndexOf(T item, Int32 index, Int32 count)
        {
            index = BinarySearch(index, count, item);
            
            if (index < 0)
            {
                return -1;
            }

            while (--index >= 0 && EqualityComparer<T>.Default.Equals(Internal[index], item))
            {
            }

            return index + 1;
        }

        public Int32 LastIndexOf(T item)
        {
            Int32 index = BinarySearch(item);
            
            if (index < 0)
            {
                return -1;
            }

            while (++index < Internal.Count && EqualityComparer<T>.Default.Equals(Internal[index], item))
            {
            }

            return index - 1;
        }

        public Int32 LastIndexOf(T item, Int32 index)
        {
            if (index < 0)
            {
                return IndexOf(item);
            }
            
            index = BinarySearch(index, item);
            
            if (index < 0)
            {
                return -1;
            }

            while (++index < Internal.Count && EqualityComparer<T>.Default.Equals(Internal[index], item))
            {
            }

            return index - 1;
        }

        public Int32 LastIndexOf(T item, Int32 index, Int32 count)
        {
            index = BinarySearch(index, count, item);
            
            if (index < 0)
            {
                return -1;
            }

            while (++index < Internal.Count && EqualityComparer<T>.Default.Equals(Internal[index], item))
            {
            }

            return index - 1;
        }

        public T? Find(Predicate<T> match)
        {
            if (match is null)
            {
                throw new ArgumentNullException(nameof(match));
            }

            for (Int32 i = 0; i < Internal.Count; i++)
            {
                T item = Internal[i];
                if (match(item))
                {
                    return item;
                }
            }

            return default;
        }

        public TSelf FindAll(Predicate<T> match)
        {
            if (match is null)
            {
                throw new ArgumentNullException(nameof(match));
            }

            TCollection collection = new TCollection();
            
            for (Int32 i = 0; i < Internal.Count; i++)
            {
                T item = Internal[i];
                if (match(item))
                {
                    collection.Add(item);
                }
            }

            return Self(collection);
        }

        public Int32 FindIndex(Predicate<T> match)
        {
            return FindIndex(0, match);
        }

        public Int32 FindIndex(Int32 index, Predicate<T> match)
        {
            if (match is null)
            {
                throw new ArgumentNullException(nameof(match));
            }

            if (index < 0 || index >= Internal.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, null);
            }

            for (Int32 i = index; i < Internal.Count; i++)
            {
                if (match(Internal[i]))
                {
                    return i;
                }
            }
            
            return -1;
        }

        public Int32 FindIndex(Int32 index, Int32 count, Predicate<T> match)
        {
            if (match is null)
            {
                throw new ArgumentNullException(nameof(match));
            }
            
            if (index < 0 || index >= Internal.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, null);
            }

            if (count < 0 || index > Internal.Count - count)
            {
                throw new ArgumentOutOfRangeException(nameof(count), count, null);
            }

            for (Int32 i = index; i < index + count; i++)
            {
                if (match(Internal[i]))
                {
                    return i;
                }
            }

            return -1;
        }

        public T? FindLast(Predicate<T> match)
        {
            if (match is null)
            {
                throw new ArgumentNullException(nameof(match));
            }

            for (Int32 i = Internal.Count - 1; i >= 0; i--)
            {
                T item = Internal[i];
                if (match(item))
                {
                    return item;
                }
            }
            
            return default;
        }

        public Int32 FindLastIndex(Predicate<T> match)
        {
            return FindLastIndex(0, match);
        }

        public Int32 FindLastIndex(Int32 index, Predicate<T> match)
        {
            if (match is null)
            {
                throw new ArgumentNullException(nameof(match));
            }
            
            if (index < 0 || index >= Internal.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, null);
            }

            for (Int32 i = index; i > index - Internal.Count; i--)
            {
                if (match(Internal[i]))
                {
                    return i;
                }
            }

            return -1;
        }

        public Int32 FindLastIndex(Int32 index, Int32 count, Predicate<T> match)
        {
            if (match is null)
            {
                throw new ArgumentNullException(nameof(match));
            }

            if (index < 0 || index >= Internal.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, null);
            }

            if (count < 0 || index - count + 1 < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count), count, null);
            }

            for (Int32 i = index; i > index - count; i--)
            {
                if (match(Internal[i]))
                {
                    return i;
                }
            }

            return -1;
        }

        public Boolean TrueForAll(Predicate<T> match)
        {
            if (match is null)
            {
                throw new ArgumentNullException(nameof(match));
            }

            for (Int32 i = 0; i < Internal.Count; i++)
            {
                if (!match(Internal[i]))
                {
                    return false;
                }
            }

            return true;
        }

        public Int32 BinarySearch(T item)
        {
            return Internal.BinarySearch(item, Comparer);
        }

        public Int32 BinarySearch(Int32 index, T item)
        {
            return Internal.BinarySearch(index, item, Comparer) ?? throw new NeverOperationException();
        }

        public Int32 BinarySearch(Int32 index, Int32 count, T item)
        {
            return Internal.BinarySearch(index, count, item, Comparer) ?? throw new NeverOperationException();
        }

        public Int32 Add(T item)
        {
            Int32 index = BinarySearch(item);
            if (index < 0)
            {
                index = ~index;
            }

            Internal.Insert(index, item);
            return index;
        }

        void ICollection<T>.Add(T item)
        {
            Add(item);
        }

        void IList<T>.Insert(Int32 index, T item)
        {
            if (index < 0 || index >= Internal.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, null);
            }
            
            Add(item);
        }

        public void Move(Int32 oldIndex, Int32 newIndex)
        {
        }

        public Boolean Remove(T item)
        {
            Int32 index = BinarySearch(item);
            if (index < 0)
            {
                return false;
            }

            Internal.RemoveAt(index);
            return true;
        }

        public void RemoveAt(Int32 index)
        {
            Internal.RemoveAt(index);
        }

        public Int32 RemoveAll(Predicate<T> match)
        {
            if (match is null)
            {
                throw new ArgumentNullException(nameof(match));
            }

            return Internal.RemoveAll(match);
        }

        public void Clear()
        {
            Internal.Clear();
        }

        public TSelf Sort()
        {
            return Sort(default(IComparer<T>));
        }

        public TSelf Sort(IComparer<T>? comparer)
        {
            comparer ??= Comparer<T>.Default;
            return Self(Create(Internal, comparer), comparer);
        }

        public TSelf Sort(Comparison<T>? comparison)
        {
            return Sort(comparison?.ToComparer());
        }

        public TSelf Reverse()
        {
            IComparer<T> comparer = Comparer.Reverse();
            return Self(Create(Internal, comparer), comparer);
        }

        public TSelf GetRange(Int32 index, Int32 count)
        {
            if (index < 0 || index >= Internal.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, null);
            }

            if (count < 0 || index > Internal.Count - count)
            {
                throw new ArgumentOutOfRangeException(nameof(count), count, null);
            }

            if (Internal.Internal() is List<T> list)
            {
                return Self(Initialize(list.GetRange(index, count)), Comparer);
            }

            TCollection collection = new TCollection();

            for (Int32 i = index; i < index + count; i++)
            {
                collection.Add(Internal[i]);
            }
            
            return Self(collection, Comparer);
        }

        // ReSharper disable once CognitiveComplexity
        public TSelf WithinRange(T minimum, T maximum)
        {
            if (Comparer.Compare(minimum, maximum) > 0)
            {
                (minimum, maximum) = (maximum, minimum);
            }

            Int32 search;
            Int32 maxindex = Internal.BinarySearch(maximum, Comparer);
            if (maxindex >= 0)
            {
                search = maxindex + 1;
                while (++maxindex < Internal.Count && Comparer.Compare(maximum, Internal[maxindex]) == 0)
                {
                }

                --maxindex;
            }
            else
            {
                search = ~maxindex;
                if (search <= 0)
                {
                    return Self(new TCollection(), Comparer);
                }

                maxindex = search - 1;
            }

            Int32 minindex = Internal.BinarySearch(0, search, minimum, Comparer) ?? throw new NeverOperationException();
            if (minindex >= 0)
            {
                while (--minindex >= 0 && Comparer.Compare(maximum, Internal[minindex]) == 0)
                {
                }

                ++minindex;
            }
            else
            {
                minindex = ~minindex;
                if (minindex > maxindex)
                {
                    return Self(new TCollection(), Comparer);
                }
            }

            TCollection result = new TCollection();

            for (Int32 i = minindex; i <= maxindex; i++)
            {
                result.Add(Internal[i]);
            }
            
            return Self(result, Comparer);
        }

        public void ForEach(Action<T>? action)
        {
            if (action is null)
            {
                return;
            }

            for (Int32 i = 0; i < Internal.Count; i++)
            {
                action(Internal[i]);
            }
        }

        public void CopyTo(T[] array)
        {
            Internal.CopyTo(array);
        }

        public void CopyTo(T[] array, Int32 index)
        {
            Internal.CopyTo(array, index);
        }

        public T[] ToArray()
        {
            return Internal.ToArray();
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
                return Internal[index];
            }
            set
            {
                if (index < 0 || index >= Internal.Count)
                {
                    throw new ArgumentOutOfRangeException(nameof(index), index, null);
                }
                
                RemoveAt(index);
                Add(value);
            }
        }
    }
}