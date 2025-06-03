// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using NetExtender.Types.Comparers;
using NetExtender.Types.Sets.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Sets
{
    public class OrderedSortedSet<T> : ISortedSet, ISortedSet<T>, IReadOnlySortedSet<T>
    {
        private SortedSet<T> Set { get; }
        private OrderedComparer<T> Inner { get; }

        /// <inheritdoc cref="SortedSet{T}.Comparer"/>
        public IComparer<T> Comparer
        {
            get
            {
                return Set.Comparer;
            }
        }

        /// <inheritdoc cref="SortedSet{T}.Count"/>
        public Int32 Count
        {
            get
            {
                return Set.Count;
            }
        }


        /// <inheritdoc cref="SortedSet{T}.Min"/>
        public T? Min
        {
            get
            {
                return Set.Min;
            }
        }

        Object? ISortedSet.Min
        {
            get
            {
                return Min;
            }
        }

        /// <inheritdoc cref="SortedSet{T}.Max"/>
        public T? Max
        {
            get
            {
                return Set.Max;
            }
        }

        Object? ISortedSet.Max
        {
            get
            {
                return Max;
            }
        }

        Boolean ICollection<T>.IsReadOnly
        {
            get
            {
                return ((ICollection<T>) Set).IsReadOnly;
            }
        }

        Boolean ICollection.IsSynchronized
        {
            get
            {
                return ((ICollection) Set).IsSynchronized;
            }
        }

        Object ICollection.SyncRoot
        {
            get
            {
                return ((ICollection) Set).SyncRoot;
            }
        }

        public OrderedSortedSet()
        {
            Inner = new OrderedComparer<T>();
            Set = new SortedSetCollection<T>((IComparer<T>) Inner);
        }

        public OrderedSortedSet(IComparer<T>? comparer)
        {
            Inner = new OrderedComparer<T>(comparer);
            Set = new SortedSetCollection<T>((IComparer<T>) Inner);
        }

        public OrderedSortedSet(IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            source = source.Materialize();

            Inner = new OrderedComparer<T>(source);
            Set = new SortedSetCollection<T>(source, Inner);
        }

        public OrderedSortedSet(IEnumerable<T> source, IComparer<T>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            source = source.Materialize();

            Inner = new OrderedComparer<T>(source, comparer);
            Set = new SortedSetCollection<T>(source, Inner);
        }

        public OrderedSortedSet(IEnumerable<T> source, IEnumerable<T>? order)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            source = source.Materialize();

            Inner = new OrderedComparer<T>(order?.Append(source) ?? source);
            Set = new SortedSetCollection<T>(source, Inner);
        }

        /// <inheritdoc cref="SortedSet{T}.Contains"/>
        public Boolean Contains(T item)
        {
            return item is not null && Set.Contains(item);
        }

        /// <inheritdoc cref="SortedSet{T}.TryGetValue"/>
        public Boolean TryGetValue(T equalValue, [MaybeNullWhen(false)] out T actualValue)
        {
            return Set.TryGetValue(equalValue, out actualValue);
        }

        /// <inheritdoc cref="SortedSet{T}.Add"/>
        void ICollection<T>.Add(T item)
        {
            Add(item);
        }

        /// <inheritdoc cref="SortedSet{T}.Add"/>
        public Boolean Add(T item)
        {
            if (!Set.Add(item))
            {
                return false;
            }

            Inner.Add(item);
            return true;
        }

        public Boolean Insert(T item)
        {
            return Insert(0, item);
        }

        public Boolean Insert(Int32 index, T item)
        {
            if (index < 0 || index >= Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, null);
            }

            if (!Set.Add(item))
            {
                return false;
            }

            Inner.Insert(index, item);
            return true;
        }

        /// <inheritdoc cref="SortedSet{T}.UnionWith"/>
        public void UnionWith(IEnumerable<T> other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            if (ReferenceEquals(this, other))
            {
                return;
            }

            foreach (T item in other)
            {
                Add(item);
            }
        }

        /// <inheritdoc cref="SortedSet{T}.IntersectWith"/>
        public void IntersectWith(IEnumerable<T> other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            if (Count <= 0 || ReferenceEquals(this, other))
            {
                return;
            }

            foreach (T item in other)
            {
                if (Contains(item))
                {
                    continue;
                }

                Remove(item);
            }
        }

        /// <inheritdoc cref="SortedSet{T}.ExceptWith"/>
        public void ExceptWith(IEnumerable<T> other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            if (Count <= 0)
            {
                return;
            }

            if (ReferenceEquals(this, other))
            {
                Clear();
                return;
            }

            foreach (T item in other)
            {
                Remove(item);
            }
        }

        /// <inheritdoc cref="SortedSet{T}.SymmetricExceptWith"/>
        public void SymmetricExceptWith(IEnumerable<T> other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            if (ReferenceEquals(this, other))
            {
                Clear();
                return;
            }

            if (Count <= 0)
            {
                UnionWith(other);
                return;
            }

            foreach (T item in other.Distinct())
            {
                if (Contains(item))
                {
                    Remove(item);
                    continue;
                }

                Add(item);
            }
        }

        /// <inheritdoc cref="SortedSet{T}.IsSubsetOf"/>
        public Boolean IsSubsetOf(IEnumerable<T> other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            return Set.IsSubsetOf(other);
        }

        /// <inheritdoc cref="SortedSet{T}.IsProperSubsetOf"/>
        public Boolean IsProperSubsetOf(IEnumerable<T> other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            return Set.IsProperSubsetOf(other);
        }

        /// <inheritdoc cref="SortedSet{T}.IsSupersetOf"/>
        public Boolean IsSupersetOf(IEnumerable<T> other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            return Set.IsSupersetOf(other);
        }

        /// <inheritdoc cref="SortedSet{T}.IsProperSupersetOf"/>
        public Boolean IsProperSupersetOf(IEnumerable<T> other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            return Set.IsProperSupersetOf(other);
        }

        /// <inheritdoc cref="SortedSet{T}.Overlaps"/>
        public Boolean Overlaps(IEnumerable<T> other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            return Set.Overlaps(other);
        }

        /// <inheritdoc cref="SortedSet{T}.SetEquals"/>
        public Boolean SetEquals(IEnumerable<T> other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            return Set.SetEquals(other);
        }

        /// <inheritdoc cref="SortedSet{T}.Remove"/>
        public Boolean Remove(T item)
        {
            if (!Set.Remove(item))
            {
                return false;
            }

            Inner.Remove(item);
            return true;
        }

        /// <inheritdoc cref="SortedSet{T}.RemoveWhere"/>
        [SuppressMessage("ReSharper", "ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator")]
        public Int32 RemoveWhere(Predicate<T> match)
        {
            if (match is null)
            {
                throw new ArgumentNullException(nameof(match));
            }

            List<T> items = new List<T>();

            foreach (T item in Set)
            {
                if (match(item))
                {
                    items.Add(item);
                }
            }

            Int32 count = 0;
            foreach (T item in items)
            {
                if (Remove(item))
                {
                    count++;
                }
            }

            return count;
        }

        /// <inheritdoc cref="SortedSet{T}.Clear"/>
        public void Clear()
        {
            Set.Clear();
            Inner.Clear();
        }

        /// <inheritdoc cref="SortedSet{T}.GetViewBetween"/>
        public OrderedSortedSet<T> GetViewBetween(T? lower, T? upper)
        {
            return new OrderedSortedSet<T>(Set.GetViewBetween(lower, upper), Comparer.Clone());
        }

        ISortedSet<T> IReadOnlySortedSet<T>.GetViewBetween(T? lower, T? upper)
        {
            return GetViewBetween(lower, upper);
        }

        ISortedSet<T> ISortedSet<T>.GetViewBetween(T? lower, T? upper)
        {
            return GetViewBetween(lower, upper);
        }

        private IEnumerable<T> Enumerate()
        {
            return Set.OrderBy(Inner);
        }

        /// <inheritdoc cref="SortedSet{T}.Reverse"/>
        public IEnumerable<T> Reverse()
        {
            return Enumerate().Reverse();
        }

        IEnumerable ISortedSet.Reverse()
        {
            return Reverse();
        }

        /// <inheritdoc cref="SortedSet{T}.GetEnumerator"/>
        public IEnumerator<T> GetEnumerator()
        {
            return Enumerate().GetEnumerator();
        }

        /// <inheritdoc cref="SortedSet{T}.GetEnumerator"/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <inheritdoc cref="SortedSet{T}.CopyTo(T[],Int32)"/>
        void ICollection.CopyTo(Array array, Int32 index)
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            ((ICollection) Set).CopyTo(array, index);
        }

        /// <inheritdoc cref="SortedSet{T}.CopyTo(T[],Int32)"/>
        public void CopyTo(T[] array, Int32 index)
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            Set.CopyTo(array, index);
        }

        public T this[Int32 index]
        {
            get
            {
                return Inner[index];
            }
        }

        public T this[Index index]
        {
            get
            {
                return Inner[index];
            }
        }
    }
}