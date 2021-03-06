// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using NetExtender.Comparers;
using NetExtender.Types.Sets.Interfaces;
using NetExtender.Utils.Types;

namespace NetExtender.Types.Sets
{
    public class OrderedSortedSet<T> : ISet, ISet<T>, IReadOnlySet<T>
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
            Set = new FixedSortedSet<T>((IComparer<T>) Inner);
        }
        
        public OrderedSortedSet(IComparer<T>? comparer)
        {
            Inner = new OrderedComparer<T>(comparer);
            Set = new FixedSortedSet<T>((IComparer<T>) Inner);
        }
        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public OrderedSortedSet([NotNull] IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            source = source.Materialize();

            Inner = new OrderedComparer<T>(source);
            Set = new FixedSortedSet<T>(source, Inner);
        }
        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public OrderedSortedSet([NotNull] IEnumerable<T> source, IComparer<T>? comparer)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            source = source.Materialize();

            Inner = new OrderedComparer<T>(source, comparer);
            Set = new FixedSortedSet<T>(source, Inner);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public OrderedSortedSet([NotNull] IEnumerable<T> source, IEnumerable<T> order)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            source = source.Materialize();

            Inner = new OrderedComparer<T>(order?.Append(source) ?? source);
            Set = new FixedSortedSet<T>(source, Inner);
        }

        /// <inheritdoc cref="SortedSet{T}.Contains"/>
        public Boolean Contains(T item)
        {
            return item is not null && Set.Contains(item);
        }
        
        /// <inheritdoc cref="SortedSet{T}.Add"/>
        void ICollection<T>.Add(T item)
        {
            Add(item);
        }
        
        /// <inheritdoc cref="SortedSet{T}.Add"/>
        public Boolean Add([CanBeNull] T item)
        {
            if (!Set.Add(item!))
            {
                return false;
            }

            Inner.Add(item);
            return true;
        }

        public Boolean Insert(T? item)
        {
            return Insert(0, item);
        }

        public Boolean Insert(Int32 index, T? item)
        {
            if (index < 0 || index >= Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }
            
            if (!Set.Add(item!))
            {
                return false;
            }

            Inner.Insert(index, item);
            return true;
        }

        /// <inheritdoc cref="SortedSet{T}.ExceptWith"/>
        public void ExceptWith([NotNull] IEnumerable<T> other)
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

        /// <inheritdoc cref="SortedSet{T}.IntersectWith"/>
        public void IntersectWith([NotNull] IEnumerable<T> other)
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
        
        /// <inheritdoc cref="SortedSet{T}.SymmetricExceptWith"/>
        public void SymmetricExceptWith([NotNull] IEnumerable<T> other)
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

        /// <inheritdoc cref="SortedSet{T}.UnionWith"/>
        public void UnionWith([NotNull] IEnumerable<T> other)
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

        /// <inheritdoc cref="SortedSet{T}.IsProperSubsetOf"/>
        public Boolean IsProperSubsetOf([NotNull] IEnumerable<T> other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            return Set.IsProperSubsetOf(other);
        }

        /// <inheritdoc cref="SortedSet{T}.IsProperSupersetOf"/>
        public Boolean IsProperSupersetOf([NotNull] IEnumerable<T> other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            return Set.IsProperSupersetOf(other);
        }

        /// <inheritdoc cref="SortedSet{T}.IsSubsetOf"/>
        public Boolean IsSubsetOf([NotNull] IEnumerable<T> other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            return Set.IsSubsetOf(other);
        }

        /// <inheritdoc cref="SortedSet{T}.IsSupersetOf"/>
        public Boolean IsSupersetOf([NotNull] IEnumerable<T> other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            return Set.IsSupersetOf(other);
        }

        /// <inheritdoc cref="SortedSet{T}.Overlaps"/>
        public Boolean Overlaps([NotNull] IEnumerable<T> other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            return Set.Overlaps(other);
        }

        /// <inheritdoc cref="SortedSet{T}.SetEquals"/>
        public Boolean SetEquals([NotNull] IEnumerable<T> other)
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
            if (!Set.Remove(item!))
            {
                return false;
            }

            Inner.Remove(item);
            return true;
        }

        /// <inheritdoc cref="SortedSet{T}.Clear"/>
        public void Clear()
        {
            Set.Clear();
            Inner.Clear();
        }

        /// <inheritdoc cref="SortedSet{T}.GetEnumerator"/>
        public IEnumerator<T> GetEnumerator()
        {
            return Set.OrderBy(Inner).GetEnumerator();
        }

        /// <inheritdoc cref="SortedSet{T}.GetEnumerator"/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <inheritdoc cref="SortedSet{T}.CopyTo(T[],Int32)"/>
        public void CopyTo(T[] array, Int32 arrayIndex)
        {
            Set.CopyTo(array, arrayIndex);
        }
        
        /// <inheritdoc cref="SortedSet{T}.CopyTo(T[],Int32)"/>
        void ICollection.CopyTo(Array array, Int32 index)
        {
            ((ICollection) Set).CopyTo(array, index);
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