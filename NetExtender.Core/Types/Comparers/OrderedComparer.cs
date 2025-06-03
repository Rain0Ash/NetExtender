// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NetExtender.Types.Comparers.Interfaces;
using NetExtender.Types.Indexers.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Comparers
{
    public class OrderedComparer<T> : IOrderedComparer<T>, IReadOnlyOrderedComparer<T>, ICloneable
    {
        protected IList<T> Order { get; }
        protected IIndexer<T>? Indexer { get; set; }

        public Int32 Count
        {
            get
            {
                return Order.Count;
            }
        }

        public Boolean IsReadOnly
        {
            get
            {
                return Order.IsReadOnly;
            }
        }

        public IComparer<T> Comparer { get; }

        public OrderedComparer()
            : this((IEnumerable<T>?) null, null)
        {
        }

        public OrderedComparer(IComparer<T>? comparer)
            : this(null, comparer)
        {
        }

        public OrderedComparer(params T[]? order)
            : this((IEnumerable<T>?) order)
        {
        }

        public OrderedComparer(IEnumerable<T>? order)
            : this(order, null)
        {
        }

        public OrderedComparer(IComparer<T>? comparer, params T[]? order)
            : this(order, comparer)
        {
        }

        public OrderedComparer(IEnumerable<T>? order, IComparer<T>? comparer)
        {
            Order = order?.ToList() ?? new List<T>();
            Indexer = Order.Count > 8 ? Order.ToIndexer() : null;
            Comparer = comparer ?? Comparer<T>.Default;
        }

        public void Add(T item)
        {
            Order.Add(item);
            Indexer = Order.Count > 8 ? Order.ToIndexer() : null;
        }

        public void AddRange(IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            Order.AddRange(source);
            Indexer = Order.Count > 8 ? Order.ToIndexer() : null;
        }

        public void Insert(Int32 index, T item)
        {
            Order.Insert(index, item);
            Indexer = Order.Count > 8 ? Order.ToIndexer() : null;
        }

        public void InsertRange(Int32 index, IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            Order.InsertRange(index, source);
            Indexer = Order.Count > 8 ? Order.ToIndexer() : null;
        }

        public Boolean Contains(T item)
        {
            return Indexer?.Contains(item) ?? Order.Contains(item);
        }

        public Int32 GetOrder(T item)
        {
            return Indexer?.IndexOf(item) ?? Order.IndexOf(item);
        }

        public Boolean GetOrder(T item, out Int32 order)
        {
            return Indexer?.IndexOf(item, out order) ?? Order.IndexOf(item, out order);
        }

        public Boolean Remove(T item)
        {
            if (!Order.Remove(item))
            {
                return false;
            }

            Indexer = Order.Count > 8 ? Order.ToIndexer() : null;
            return true;
        }

        public void Clear()
        {
            Order.Clear();
            Indexer = null;
        }

        public void CopyTo(T[] array, Int32 index)
        {
            Order.CopyTo(array, index);
        }

        protected Int32 Compare(Int32 first, Int32 second)
        {
            Int32 count = Count;

            Int32 cx = first != -1 ? first : count;
            Int32 cy = second != -1 ? second : count;

            return cx.CompareTo(cy);
        }

        public virtual Int32 Compare(T? x, T? y)
        {
            if (EqualityComparer<T>.Default.Equals(x, y))
            {
                return 0;
            }

            Int32 compare = Compare(GetOrder(x!), GetOrder(y!));
            return compare == 0 ? Comparer.Compare(x, y) : compare;
        }

        public virtual OrderedComparer<T> Clone()
        {
            return new OrderedComparer<T>(Order, Comparer);
        }

        Object ICloneable.Clone()
        {
            return Clone();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Order.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public T this[Int32 index]
        {
            get
            {
                return Order[index];
            }
        }

        public T this[Index index]
        {
            get
            {
                return Order[index];
            }
        }
    }
}