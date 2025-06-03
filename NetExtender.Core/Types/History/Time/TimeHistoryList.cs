using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using NetExtender.Types.History.Interfaces;
using NetExtender.Types.Lists;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.History
{
    [SuppressMessage("ReSharper", "ForCanBeConvertedToForeach")]
    [SuppressMessage("ReSharper", "LoopCanBeConvertedToQuery")]
    [SuppressMessage("ReSharper", "ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator")]
    public class TimeHistoryList<T> : TimeHistoryCollection<T>, ITimeHistoryList<T>, IReadOnlyTimeHistoryList<T>
    {
        protected sealed override SortedList<Node> Internal { get; }

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

        public sealed override DateTimeOffset? Min
        {
            get
            {
                return Internal.Count > 0 ? Internal[0].Time : null;
            }
        }

        public sealed override DateTimeOffset? Max
        {
            get
            {
                return Internal.Count > 0 ? Internal[^1].Time : null;
            }
        }

        public TimeHistoryList()
            : this(default(IComparer<T>))
        {
        }

        public TimeHistoryList(IComparer<T>? comparer)
            : base(comparer)
        {
            Internal = new SortedList<Node>(base.Comparer);
        }

        public TimeHistoryList(Comparison<T>? comparison)
            : base(comparison)
        {
            Internal = new SortedList<Node>(base.Comparer);
        }

        protected TimeHistoryList(NodeComparer? comparer)
            : base(comparer)
        {
            Internal = new SortedList<Node>(base.Comparer);
        }

        public TimeHistoryList(Int32 capacity)
            : this(capacity, default(IComparer<T>))
        {
        }

        public TimeHistoryList(Int32 capacity, IComparer<T>? comparer)
            : base(comparer)
        {
            Internal = new SortedList<Node>(capacity, base.Comparer);
        }

        protected TimeHistoryList(Int32 capacity, Comparison<T>? comparison)
            : base(comparison)
        {
            Internal = new SortedList<Node>(capacity, base.Comparer);
        }

        protected TimeHistoryList(Int32 capacity, NodeComparer? comparer)
            : base(comparer)
        {
            Internal = new SortedList<Node>(capacity, base.Comparer);
        }

        private TimeHistoryList(SortedList<Node> list)
            : base(list is not null ? list.Comparer as NodeComparer : throw new ArgumentNullException(nameof(list)))
        {
            Internal = list;

            if (!ReferenceEquals(Internal.Comparer, base.Comparer))
            {
                throw new ArgumentException("Argument comparer not equals real comparer.", nameof(list));
            }
        }

        public Int32 EnsureCapacity(Int32 capacity)
        {
            return Internal.EnsureCapacity(Math.Min(capacity, Limit));
        }

        public void TrimExcess()
        {
            Internal.TrimExcess();
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
            ((IList<Node>) Internal).Insert(index, Create(item));
        }

        public sealed override Boolean Remove(T item)
        {
            return Internal.Remove(Universal(item));
        }

        public void RemoveAt(Int32 index)
        {
            Internal.RemoveAt(index);
        }

        public Int32 RemoveAll(Predicate<Node> match)
        {
            if (match is null)
            {
                throw new ArgumentNullException(nameof(match));
            }

            return Internal.RemoveAll(match);
        }

        public void RemoveRange(Int32 index, Int32 count)
        {
            Internal.RemoveRange(index, count);
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
            
            Internal.RemoveRange(0, Math.Max(0, Internal.Count - size));
        }

        public sealed override void Clear()
        {
            Internal.Clear();
        }

        public TimeHistoryList<T> GetRange(Int32 index, Int32 count)
        {
            return new TimeHistoryList<T>(Internal.GetRange(index, count));
        }

        public TimeHistoryList<T> WithinRange(DateTimeOffset minimum, DateTimeOffset maximum)
        {
            if (minimum > maximum)
            {
                (minimum, maximum) = (maximum, minimum);
            }

            if (Internal.Count <= 0)
            {
                return new TimeHistoryList<T>(0, Comparer);
            }

            Int32 lower = FindLowerIndex(minimum);
            if (lower == -1 || lower >= Internal.Count)
            {
                return new TimeHistoryList<T>(0, Comparer);
            }

            Int32 upper = FindUpperIndex(maximum);
            if (upper < lower)
            {
                return new TimeHistoryList<T>(0, Comparer);
            }

            return GetRange(lower, upper - lower + 1);
        }

        public void ForEach(Action<Node>? action)
        {
            Internal.ForEach(action);
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
            
            using List<Node>.Enumerator enumerator = Internal.GetEnumerator();

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

        public ReadOnlyCollection<Node> AsReadOnly()
        {
            return Internal.AsReadOnly();
        }

        public List<Node>.Enumerator GetEnumerator()
        {
            return Internal.GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return new Enumerator(Internal.GetEnumerator());
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
        public struct Enumerator : IEnumerator<T>
        {
            private List<Node>.Enumerator _enumerator;

            public Node Node
            {
                get
                {
                    return _enumerator.Current;
                }
            }

            public T Current
            {
                get
                {
                    return Node;
                }
            }

            Object? IEnumerator.Current
            {
                get
                {
                    return Current;
                }
            }

            public Enumerator(List<Node>.Enumerator enumerator)
            {
                _enumerator = enumerator;
            }

            public Boolean MoveNext()
            {
                return _enumerator.MoveNext();
            }

            public void Reset()
            {
                EnumeratorUtilities.TryReset(ref _enumerator);
            }

            public void Dispose()
            {
                _enumerator.Dispose();
            }
        }
    }
}