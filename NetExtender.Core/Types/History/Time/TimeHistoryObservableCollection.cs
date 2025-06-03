using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using NetExtender.Types.History.Interfaces;
using NetExtender.Types.Lists;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.History
{
    public class TimeHistoryObservableCollection<T> : TimeHistoryCollection<T>, ITimeHistoryObservableCollection<T>, IReadOnlyTimeHistoryList<T>
    {
        protected sealed override ObservableSortedList<Node> Internal { get; }

        public event NotifyCollectionChangedEventHandler? CollectionChanged;
        public event PropertyChangingEventHandler? PropertyChanging;
        public event PropertyChangedEventHandler? PropertyChanged;

        protected sealed override NodeComparer Comparer
        {
            get
            {
                return (NodeComparer) Internal.Comparer;
            }
        }

        public sealed override Int32 Count
        {
            get
            {
                return Internal.Count;
            }
        }

        private DateTimeOffset? _min;
        public sealed override DateTimeOffset? Min
        {
            get
            {
                return Internal.Count > 0 ? Internal[0].Time : null;
            }
        }

        private DateTimeOffset? _max;
        public sealed override DateTimeOffset? Max
        {
            get
            {
                return Internal.Count > 0 ? Internal[^1].Time : null;
            }
        }

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

        public TimeHistoryObservableCollection()
            : this(default(IComparer<T>))
        {
        }

        public TimeHistoryObservableCollection(IComparer<T>? comparer)
            : base(comparer)
        {
            Internal = new ObservableSortedList<Node>(base.Comparer);

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

        public TimeHistoryObservableCollection(Comparison<T>? comparison)
            : base(comparison)
        {
            Internal = new ObservableSortedList<Node>(base.Comparer);

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

        protected TimeHistoryObservableCollection(NodeComparer? comparer)
            : base(comparer)
        {
            Internal = new ObservableSortedList<Node>(base.Comparer);

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

        private TimeHistoryObservableCollection(ObservableSortedList<Node> list)
            : base(list is not null ? list.Comparer as NodeComparer : throw new ArgumentNullException(nameof(list)))
        {
            Internal = list;

            Internal.CollectionChanged += OnCollectionChanged;

            if (Internal is INotifyPropertyChanging changing)
            {
                changing.PropertyChanging += OnPropertyChanging;
            }

            if (Internal is INotifyPropertyChanged changed)
            {
                changed.PropertyChanged += OnPropertyChanged;
            }

            if (!ReferenceEquals(Internal.Comparer, base.Comparer))
            {
                throw new ArgumentException("Argument comparer not equals real comparer.", nameof(list));
            }
        }

        private void OnCollectionChanged(Object? sender, NotifyCollectionChangedEventArgs args)
        {
            CollectionChanged?.Invoke(this, args);

            DateTimeOffset? min = Min;
            DateTimeOffset? max = Min;

            Boolean minimum = _min != min;
            Boolean maximum = _max != max;
            
            if (minimum)
            {
                PropertyChanging?.Invoke(this, NotifyUtilities.Changing.Min);
                _min = min;
            }

            if (maximum)
            {
                PropertyChanging?.Invoke(this, NotifyUtilities.Changing.Max);
                _max = max;
            }

            if (minimum)
            {
                PropertyChanged?.Invoke(this, NotifyUtilities.Changed.Min);
            }

            if (maximum)
            {
                PropertyChanged?.Invoke(this, NotifyUtilities.Changed.Max);
            }
        }

        private void OnPropertyChanging(Object? sender, PropertyChangingEventArgs args)
        {
            PropertyChanging?.Invoke(this, args);
        }

        private void OnPropertyChanged(Object? sender, PropertyChangedEventArgs args)
        {
            PropertyChanged?.Invoke(this, args);
        }

        public IDisposable? Suppress()
        {
            return Internal.Suppress();
        }

        public sealed override Boolean Contains(T item)
        {
            return Internal.Contains(Universal(item));
        }

        public Boolean Exists(Predicate<Node> match)
        {
            return Internal.Exists(match);
        }

        public Int32 IndexOf(T item)
        {
            return Internal.IndexOf(Universal(item));
        }

        public Int32 IndexOf(T item, Int32 index)
        {
            return Internal.IndexOf(Universal(item), index);
        }

        public Int32 IndexOf(T item, Int32 index, Int32 count)
        {
            return Internal.IndexOf(Universal(item), index, count);
        }

        public Int32 LastIndexOf(T item)
        {
            return Internal.LastIndexOf(Universal(item));
        }

        public Int32 LastIndexOf(T item, Int32 index)
        {
            return Internal.LastIndexOf(Universal(item), index);
        }

        public Int32 LastIndexOf(T item, Int32 index, Int32 count)
        {
            return Internal.LastIndexOf(Universal(item), index, count);
        }
        
        public Node Find(Predicate<Node> match)
        {
            return Internal.Find(match);
        }

        public Node[] FindAll(Predicate<Node> match)
        {
            return Internal.FindAll(match).ToArray();
        }

        public Int32 FindIndex(Predicate<Node> match)
        {
            return Internal.FindIndex(match);
        }

        public Int32 FindIndex(Int32 index, Predicate<Node> match)
        {
            return Internal.FindIndex(index, match);
        }

        public Int32 FindIndex(Int32 index, Int32 count, Predicate<Node> match)
        {
            return Internal.FindIndex(index, count, match);
        }

        public Node FindLast(Predicate<Node> match)
        {
            return Internal.FindLast(match);
        }

        public Int32 FindLastIndex(Predicate<Node> match)
        {
            return Internal.FindLastIndex(match);
        }

        public Int32 FindLastIndex(Int32 index, Predicate<Node> match)
        {
            return Internal.FindLastIndex(index, match);
        }

        public Int32 FindLastIndex(Int32 index, Int32 count, Predicate<Node> match)
        {
            return Internal.FindLastIndex(index, count, match);
        }

        public Int32 FindLowerIndex(DateTimeOffset minimum)
        {
            Int32 low = 0;
            Int32 high = Internal.Count - 1;
            Int32 result = Internal.Count;

            while (low <= high)
            {
                Int32 middle = (low + high) / 2;
                if (Comparer.Compare(Internal[middle].Time, minimum) >= 0)
                {
                    result = middle;
                    high = middle - 1;
                }
                else
                {
                    low = middle + 1;
                }
            }

            return result;
        }

        public Int32 FindUpperIndex(DateTimeOffset maximum)
        {
            Int32 low = 0;
            Int32 high = Internal.Count - 1;
            Int32 result = -1;

            while (low <= high)
            {
                Int32 middle = (low + high) / 2;
                if (Comparer.Compare(Internal[middle].Time, maximum) <= 0)
                {
                    result = middle;
                    low = middle + 1;
                }
                else
                {
                    high = middle - 1;
                }
            }

            return result;
        }

        public Boolean TrueForAll(Predicate<Node> match)
        {
            return Internal.TrueForAll(match);
        }

        public Int32 BinarySearch(T item)
        {
            return Internal.BinarySearch(Universal(item));
        }

        public Int32 BinarySearch(Int32 index, T item)
        {
            return Internal.BinarySearch(index, Universal(item));
        }

        public Int32 BinarySearch(Int32 index, Int32 count, T item)
        {
            return Internal.BinarySearch(index, count, Universal(item));
        }

        public sealed override Int32 Add(T item)
        {
            Trim(Limit - 1);
            return Internal.Add(Create(item));
        }

        void IList<T>.Insert(Int32 index, T item)
        {
            Trim(Limit - 1);
            ((IList<T>) Internal).Insert(index, item);
        }

        public override Boolean Remove(T item)
        {
            return Internal.Remove(Universal(item));
        }

        public void RemoveAt(Int32 index)
        {
            Internal.RemoveAt(index);
        }

        public void Move(Int32 oldIndex, Int32 newIndex)
        {
            Internal.Move(oldIndex, newIndex);
        }

        public sealed override void Trim()
        {
            base.Trim();
        }

        public sealed override void Trim(Int32 size)
        {
            if (size <= 0)
            {
                Clear();
                return;
            }

            using IDisposable? suppress = Suppress();

            while (Internal.Count >= size)
            {
                Internal.RemoveAt(0);
            }
        }

        public sealed override void Clear()
        {
            Internal.Clear();
        }

        public TimeHistoryObservableCollection<T> GetRange(Int32 index, Int32 count)
        {
            return new TimeHistoryObservableCollection<T>(Internal.GetRange(index, count));
        }

        public TimeHistoryObservableCollection<T> WithinRange(DateTimeOffset minimum, DateTimeOffset maximum)
        {
            if (minimum > maximum)
            {
                (minimum, maximum) = (maximum, minimum);
            }

            if (Internal.Count <= 0)
            {
                return new TimeHistoryObservableCollection<T>(Comparer);
            }

            Int32 lower = FindLowerIndex(minimum);
            if (lower == -1 || lower >= Internal.Count)
            {
                return new TimeHistoryObservableCollection<T>(Comparer);
            }

            Int32 upper = FindUpperIndex(maximum);
            if (upper < lower)
            {
                return new TimeHistoryObservableCollection<T>(Comparer);
            }

            return GetRange(lower, upper - lower + 1);
        }

        public sealed override void CopyTo(T[] array)
        {
            base.CopyTo(array);
        }

        public sealed override void CopyTo(T[] array, Int32 index)
        {
            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, null);
            }
            
            using IEnumerator<Node> enumerator = Internal.GetEnumerator();

            for (Int32 i = index; i < array.Length && enumerator.MoveNext(); i++)
            {
                array[i] = enumerator.Current;
            }
        }

        public sealed override void CopyTo(Node[] array)
        {
            Internal.CopyTo(array);
        }

        public sealed override void CopyTo(Node[] array, Int32 index)
        {
            Internal.CopyTo(array, index);
        }

        public Node[] ToArray()
        {
            return Internal.ToArray();
        }

        public Enumerator GetEnumerator()
        {
            return new Enumerator(Internal.GetEnumerator());
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
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
                Internal[index] = Create(value);
            }
        }

        [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
        public readonly struct Enumerator : IEnumerator<T>, IEnumerator<Node>
        {
            private readonly IEnumerator<Node> _enumerator;

            public Node Current
            {
                get
                {
                    return _enumerator.Current;
                }
            }

            T IEnumerator<T>.Current
            {
                get
                {
                    return Current;
                }
            }

            Object IEnumerator.Current
            {
                get
                {
                    return Current;
                }
            }

            public Enumerator(IEnumerator<Node> enumerator)
            {
                _enumerator = enumerator;
            }

            public Boolean MoveNext()
            {
                return _enumerator.MoveNext();
            }

            public void Reset()
            {
                _enumerator.Reset();
            }

            public void Dispose()
            {
                _enumerator.Dispose();
            }
        }
    }
}