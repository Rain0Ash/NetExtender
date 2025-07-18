// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using NetExtender.Types.Indexers;
using NetExtender.Types.Indexers.Interfaces;
using NetExtender.Types.Lists.Interfaces;

namespace NetExtender.Types.Lists
{
    public class IndexList<T> : IIndexList<T>, IReadOnlyIndexList<T>, IList
    {
        protected const Int32 IndexerBound = 4;

        protected List<T> Internal { get; }
        protected Indexer<T> Indexer { get; }

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

        public Int32 Capacity
        {
            get
            {
                return Internal.Capacity;
            }
            set
            {
                Internal.Capacity = value;
            }
        }

        public Int32 Count
        {
            get
            {
                return Internal.Count;
            }
        }

        public IEqualityComparer<T> Comparer
        {
            get
            {
                return Indexer.Comparer;
            }
        }

        Boolean ICollection<T>.IsReadOnly
        {
            get
            {
                return ((ICollection<T>) Internal).IsReadOnly;
            }
        }

        Boolean IList.IsReadOnly
        {
            get
            {
                return ((IList) Internal).IsReadOnly;
            }
        }

        Boolean IList.IsFixedSize
        {
            get
            {
                return ((IList) Internal).IsFixedSize;
            }
        }

        Object ICollection.SyncRoot
        {
            get
            {
                return ((ICollection) Internal).SyncRoot;
            }
        }

        Boolean ICollection.IsSynchronized
        {
            get
            {
                return ((ICollection) Internal).IsSynchronized;
            }
        }

        public IndexList()
        {
            Internal = new List<T>();
            Indexer = new Indexer<T>();
        }

        public IndexList(IEqualityComparer<T>? comparer)
        {
            Internal = new List<T>();
            Indexer = new Indexer<T>(comparer);
        }

        public IndexList(Int32 capacity)
        {
            Internal = new List<T>(capacity);
            Indexer = new Indexer<T>();
        }

        public IndexList(Int32 capacity, IEqualityComparer<T>? comparer)
        {
            Internal = new List<T>(capacity);
            Indexer = new Indexer<T>(comparer);
        }

        public IndexList(IEnumerable<T> collection)
            : this(collection, null)
        {
        }

        public IndexList(IEnumerable<T> collection, IEqualityComparer<T>? comparer)
        {
            if (collection is null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            Internal = new List<T>(collection);
            Indexer = new Indexer<T>(comparer);
        }

        protected IndexList(List<T> collection, IEqualityComparer<T>? comparer)
        {
            Internal = collection ?? throw new ArgumentNullException(nameof(collection));
            Indexer = new Indexer<T>(comparer);
        }

        public Int32 EnsureCapacity(Int32 capacity)
        {
            return Internal.EnsureCapacity(capacity);
        }

        public void TrimExcess()
        {
            Internal.TrimExcess();
        }

        public Boolean Contains(T item)
        {
            if (!Rebuild)
            {
                return Indexer.Contains(item);
            }

            if (Internal.Count <= IndexerBound)
            {
                return Internal.Contains(item);
            }

            Indexer.Rebuild(Internal);
            Rebuild = false;

            return Indexer.Contains(item);
        }

        Boolean IList.Contains(Object? item)
        {
            return Contains((T) item!);
        }

        public Boolean Contains(T item, Boolean rebuild)
        {
            return rebuild || !Rebuild ? Contains(item) : Internal.Contains(item);
        }

        public Boolean ContainsIndex(Int32 index)
        {
            return index >= 0 && index < Count;
        }

        public Boolean Exists(Predicate<T> match)
        {
            if (match is null)
            {
                throw new ArgumentNullException(nameof(match));
            }

            return Internal.Exists(match);
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

        Int32 IList.IndexOf(Object? item)
        {
            return IndexOf((T) item!);
        }

        public Int32 IndexOf(T item, Boolean rebuild)
        {
            return Internal.Count <= IndexerBound || !rebuild && Rebuild ? Internal.IndexOf(item) : IndexOf(item);
        }

        public Int32 IndexOf(T item, Int32 index)
        {
            return index > 0 ? Internal.IndexOf(item, index) : IndexOf(item);
        }

        public Int32 IndexOf(T item, Int32 index, Int32 count)
        {
            return Internal.IndexOf(item, index, count);
        }

        public Int32 LastIndexOf(T item)
        {
            return Internal.LastIndexOf(item);
        }

        public Int32 LastIndexOf(T item, Boolean rebuild)
        {
            return Internal.Count <= IndexerBound || !rebuild && Rebuild ? Internal.LastIndexOf(item) : LastIndexOf(item);
        }

        public Int32 LastIndexOf(T item, Int32 index)
        {
            return Internal.LastIndexOf(item, index);
        }

        public Int32 LastIndexOf(T item, Int32 index, Int32 count)
        {
            return Internal.LastIndexOf(item, index, count);
        }

        T? IReadOnlyMapIndexer<T>.ValueOf(Int32 index)
        {
            return ((IReadOnlyMapIndexer<T>) this).ValueOf(index, out T? result) ? result : default;
        }

        Boolean IReadOnlyMapIndexer<T>.ValueOf(Int32 index, [MaybeNullWhen(false)] out T value)
        {
            if (!ContainsIndex(index))
            {
                value = default;
                return false;
            }

            value = Internal[index];
            return true;
        }

        public T? Find(Predicate<T> match)
        {
            if (match is null)
            {
                throw new ArgumentNullException(nameof(match));
            }

            return Internal.Find(match);
        }

        public IndexList<T> FindAll(Predicate<T> match)
        {
            if (match is null)
            {
                throw new ArgumentNullException(nameof(match));
            }

            return new IndexList<T>(Internal.FindAll(match));
        }

        public Int32 FindIndex(Predicate<T> match)
        {
            if (match is null)
            {
                throw new ArgumentNullException(nameof(match));
            }

            return Internal.FindIndex(match);
        }

        public Int32 FindIndex(Int32 index, Predicate<T> match)
        {
            if (match is null)
            {
                throw new ArgumentNullException(nameof(match));
            }

            return Internal.FindIndex(index, match);
        }

        public Int32 FindIndex(Int32 index, Int32 count, Predicate<T> match)
        {
            if (match is null)
            {
                throw new ArgumentNullException(nameof(match));
            }

            return Internal.FindIndex(index, count, match);
        }

        public T? FindLast(Predicate<T> match)
        {
            if (match is null)
            {
                throw new ArgumentNullException(nameof(match));
            }

            return Internal.FindLast(match);
        }

        public Int32 FindLastIndex(Predicate<T> match)
        {
            if (match is null)
            {
                throw new ArgumentNullException(nameof(match));
            }

            return Internal.FindLastIndex(match);
        }

        public Int32 FindLastIndex(Int32 index, Predicate<T> match)
        {
            if (match is null)
            {
                throw new ArgumentNullException(nameof(match));
            }

            return Internal.FindLastIndex(index, match);
        }

        public Int32 FindLastIndex(Int32 index, Int32 count, Predicate<T> match)
        {
            if (match is null)
            {
                throw new ArgumentNullException(nameof(match));
            }

            return Internal.FindLastIndex(index, count, match);
        }

        public Boolean TrueForAll(Predicate<T> match)
        {
            if (match is null)
            {
                throw new ArgumentNullException(nameof(match));
            }

            return Internal.TrueForAll(match);
        }

        public void Add(T item)
        {
            Internal.Add(item);
            Rebuild = true;
        }

        Int32 IList.Add(Object? item)
        {
            T value = (T) item!;
            Add(value);
            return IndexOf(value);
        }

        public void AddRange(IEnumerable<T> collection)
        {
            Int32 count = Internal.Count;
            Internal.AddRange(collection);
            Rebuild |= Internal.Count != count;
        }

        public void Insert(Int32 index, T item)
        {
            Internal.Insert(index, item);
            Rebuild = true;
        }

        void IList.Insert(Int32 index, Object? item)
        {
            Insert(index, (T) item!);
        }

        public void InsertRange(Int32 index, IEnumerable<T> collection)
        {
            Int32 count = Internal.Count;
            Internal.InsertRange(index, collection);
            Rebuild |= Internal.Count != count;
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

        void IList.Remove(Object? item)
        {
            Remove((T) item!);
        }

        public void RemoveRange(Int32 index, Int32 length)
        {
            Int32 count = Internal.Count;
            Internal.RemoveRange(index, length);
            Rebuild |= Internal.Count != count;
        }

        public void RemoveAt(Int32 index)
        {
            Internal.RemoveAt(index);
            Rebuild = true;
        }

        public Int32 RemoveAll(Predicate<T> match)
        {
            if (match is null)
            {
                throw new ArgumentNullException(nameof(match));
            }

            Int32 count = Internal.RemoveAll(match);
            Rebuild |= count > 0;
            return count;
        }

        public void Clear()
        {
            Internal.Clear();
            Indexer.Clear();
            Rebuild = false;
        }

        public void Sort()
        {
            Internal.Sort();
            Rebuild = true;
        }

        public void Sort(IComparer<T>? comparer)
        {
            Internal.Sort(comparer);
            Rebuild = true;
        }

        public void Sort(Int32 index, Int32 count, IComparer<T>? comparer)
        {
            Internal.Sort(index, count, comparer);
            Rebuild = true;
        }

        public void Sort(Comparison<T>? comparison)
        {
            if (comparison is null)
            {
                Sort(default(IComparer<T>));
                return;
            }
            
            Internal.Sort(comparison);
            Rebuild = true;
        }

        public void Reverse()
        {
            Internal.Reverse();
            Rebuild = true;
        }

        public void Reverse(Int32 index, Int32 count)
        {
            Internal.Reverse(index, count);
            Rebuild = true;
        }

        public IndexList<T> GetRange(Int32 index, Int32 count)
        {
            return new IndexList<T>(Internal.GetRange(index, count));
        }

        public IndexList<TOutput> ConvertAll<TOutput>(Converter<T, TOutput> converter)
        {
            if (converter is null)
            {
                throw new ArgumentNullException(nameof(converter));
            }

            return new IndexList<TOutput>(Internal.ConvertAll(converter));
        }

        public void ForEach(Action<T>? action)
        {
            if (action is not null)
            {
                Internal.ForEach(action);
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

        void ICollection.CopyTo(Array array, Int32 index)
        {
            ((ICollection) Internal).CopyTo(array, index);
        }

        public T[] ToArray()
        {
            return Internal.ToArray();
        }

        public ReadOnlyCollection<T> AsReadOnly()
        {
            return new ReadOnlyCollection<T>(this);
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
                Rebuild = true;
            }
        }

        Object? IList.this[Int32 index]
        {
            get
            {
                return this[index];
            }
            set
            {
                this[index] = (T) value!;
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