// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using NetExtender.Types.Indexers;
using NetExtender.Types.Indexers.Interfaces;
using NetExtender.Types.Sets.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Sets
{
    public class IndexSortedSet<T> : ISet, IIndexSortedSet<T>, IReadOnlyIndexSortedSet<T>
    {
        protected const Int32 IndexerBound = 4;

        protected SortedSet<T> Internal { get; }
        protected MapIndexer<T> Indexer { get; }

        protected Boolean Rebuild
        {
            get
            {
                return Internal.Count > 0 ^ Indexer.Count > 0;
            }
            set
            {
                if (value && Indexer.Count > 0)
                {
                    Indexer.Clear();
                }
            }
        }

        public IComparer<T> Comparer
        {
            get
            {
                return Internal.Comparer;
            }
        }

        IEqualityComparer<T> IReadOnlyIndexer<T>.Comparer
        {
            get
            {
                return Indexer.Comparer;
            }
        }

        public Int32 Count
        {
            get
            {
                return Internal.Count;
            }
        }

        public T? Min
        {
            get
            {
                return Internal.Min;
            }
        }

        public T? Max
        {
            get
            {
                return Internal.Max;
            }
        }

        Boolean ICollection.IsSynchronized
        {
            get
            {
                return ((ICollection) Internal).IsSynchronized;
            }
        }

        Object ICollection.SyncRoot
        {
            get
            {
                return ((ICollection) Internal).SyncRoot;
            }
        }

        Boolean ICollection<T>.IsReadOnly
        {
            get
            {
                return ((ICollection<T>) Internal).IsReadOnly;
            }
        }

        public IndexSortedSet()
        {
            Internal = new SortedSet<T>();
            Indexer = new MapIndexer<T>(Internal.Comparer.ToEqualityComparer());
        }

        public IndexSortedSet(IComparer<T>? comparer)
        {
            Internal = new SortedSet<T>(comparer);
            Indexer = new MapIndexer<T>(Internal.Comparer.ToEqualityComparer());
        }

        public IndexSortedSet(IEnumerable<T> collection)
            : this(collection, null)
        {
        }

        public IndexSortedSet(IEnumerable<T> collection, IComparer<T>? comparer)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            Internal = new SortedSet<T>(collection, comparer);
            Indexer = new MapIndexer<T>(Internal.Comparer.ToEqualityComparer());
        }

        protected IndexSortedSet(SortedSet<T> collection)
        {
            Internal = collection ?? throw new ArgumentNullException(nameof(collection));
            Indexer = new MapIndexer<T>(Internal.Comparer.ToEqualityComparer());
        }

        public Boolean Contains(T item)
        {
            return Internal.Contains(item);
        }

        public Boolean ContainsIndex(Int32 index)
        {
            return index >= 0 && index < Count;
        }

        public Int32 IndexOf(T item)
        {
            if (!Rebuild)
            {
                return Indexer.IndexOf(item);
            }

            if (Internal.Count <= IndexerBound)
            {
                return Internal.IndexOf(item);
            }

            Indexer.Rebuild(Internal);
            Rebuild = false;

            return Indexer.IndexOf(item);
        }

        public T? ValueOf(Int32 index)
        {
            return ((IReadOnlyMapIndexer<T>) this).ValueOf(index, out T? result) ? result : default;
        }

        public Boolean ValueOf(Int32 index, [MaybeNullWhen(false)] out T value)
        {
            if (Rebuild)
            {
                Indexer.Rebuild(Indexer);
                Rebuild = false;
            }

            return Indexer.ValueOf(index, out value);
        }
        
        public Boolean TryGetValue(T equal, [MaybeNullWhen(false)] out T actual)
        {
            return Internal.TryGetValue(equal, out actual);
        }

        public virtual IndexSortedSet<T> GetViewBetween(T? lower, T? upper)
        {
            return new IndexSortedSet<T>(Internal.GetViewBetween(lower, upper));
        }

        IIndexSortedSet<T> IIndexSortedSet<T>.GetViewBetween(T? lower, T? upper)
        {
            return GetViewBetween(lower, upper);
        }

        ISortedSet<T> ISortedSet<T>.GetViewBetween(T? lower, T? upper)
        {
            return GetViewBetween(lower, upper);
        }

        ISortedSet<T> IReadOnlySortedSet<T>.GetViewBetween(T? lower, T? upper)
        {
            return GetViewBetween(lower, upper);
        }

        public Boolean IsSubsetOf(IEnumerable<T> other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            return Internal.IsSubsetOf(other);
        }

        public Boolean IsProperSubsetOf(IEnumerable<T> other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            return Internal.IsProperSubsetOf(other);
        }

        public Boolean IsSupersetOf(IEnumerable<T> other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            return Internal.IsSupersetOf(other);
        }

        public Boolean IsProperSupersetOf(IEnumerable<T> other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            return Internal.IsProperSupersetOf(other);
        }

        public Boolean Overlaps(IEnumerable<T> other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            return Internal.Overlaps(other);
        }

        public Boolean SetEquals(IEnumerable<T> other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            return Internal.SetEquals(other);
        }

        public Boolean Add(T item)
        {
            Boolean successful = Internal.Add(item);
            Rebuild = successful;
            return successful;
        }

        void ICollection<T>.Add(T item)
        {
            Add(item);
        }

        public void UnionWith(IEnumerable<T> other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            Int32 count = Internal.Count;
            Internal.UnionWith(other);
            Rebuild = Internal.Count > count;
        }

        public void IntersectWith(IEnumerable<T> other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            Int32 count = Internal.Count;
            Internal.IntersectWith(other);
            Rebuild = Internal.Count < count;
        }

        public void ExceptWith(IEnumerable<T> other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            Int32 count = Internal.Count;
            Internal.ExceptWith(other);
            Rebuild = Internal.Count < count;
        }

        public void SymmetricExceptWith(IEnumerable<T> other)
        {
            if (other is null)
            {
                throw new ArgumentNullException(nameof(other));
            }

            Internal.SymmetricExceptWith(other);
            Rebuild = true;
        }

        public Boolean Remove(T item)
        {
            if (!Internal.Remove(item))
            {
                return false;
            }

            Rebuild = true;
            return true;
        }
        
        public virtual Int32 RemoveWhere(Predicate<T> match)
        {
            if (match is null)
            {
                throw new ArgumentNullException(nameof(match));
            }

            Int32 count = Internal.RemoveWhere(match);
            Rebuild = count > 0;
            return count;
        }

        public virtual IEnumerable<T> Reverse()
        {
            return Internal.Reverse();
        }

        public void Clear()
        {
            Internal.Clear();
            Indexer.Clear();
            Rebuild = false;
        }

        void ICollection.CopyTo(Array array, Int32 index)
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            ((ICollection) Internal).CopyTo(array, index);
        }

        public void CopyTo(T[] array)
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            Internal.CopyTo(array);
        }

        public void CopyTo(T[] array, Int32 index)
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            Internal.CopyTo(array, index);
        }

        public virtual void CopyTo(T[] array, Int32 index, Int32 count)
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            Internal.CopyTo(array, index, count);
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
                if (Rebuild)
                {
                    Indexer.Rebuild(Internal);
                    Rebuild = false;
                }

                return Indexer.ValueOf(index, out T? item) ? item : throw new ArgumentOutOfRangeException(nameof(index), index, null);
            }
            set
            {
                if (Rebuild)
                {
                    Indexer.Rebuild(Internal);
                    Rebuild = false;
                }

                if (!Indexer.ValueOf(index, out T? item))
                {
                    throw new ArgumentOutOfRangeException(nameof(index), index, null);
                }

                Internal.Remove(item);
                Internal.Add(value);
                Rebuild = true;
            }
        }

        Int32 IReadOnlyIndexer<T>.this[T item]
        {
            get
            {
                return IndexOf(item);
            }
        }
    }
}