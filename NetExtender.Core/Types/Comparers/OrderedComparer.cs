// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NetExtender.Types.Comparers.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Comparers
{
    public class OrderedComparer<T> : IOrderedComparer<T>
    {
        protected static IComparer<T> Inner
        {
            get
            {
                return Comparer<T>.Default;
            }
        }

        protected IList<T> Order { get; }

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

        private readonly IComparer<T> _comparer = Inner;

        public IComparer<T> Comparer
        {
            get
            {
                return _comparer;
            }
            init
            {
                _comparer = value ?? Inner;
            }
        }

        public OrderedComparer()
            : this(null, null)
        {
        }

        public OrderedComparer(IComparer<T>? comparer)
            : this(null, comparer)
        {
        }

        public OrderedComparer(IEnumerable<T>? order)
            : this(order, null)
        {
        }

        public OrderedComparer(IEnumerable<T>? order, IComparer<T>? comparer)
        {
            Order = order?.ToList() ?? new List<T>();
            Comparer = comparer ?? Inner;
        }

        public void Add(T item)
        {
            Order.Add(item!);
        }

        public void AddRange(IEnumerable<T> items)
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            Order.AddRange(items);
        }
        
        public void Insert(Int32 index, T item)
        {
            Order.Insert(index, item!);
        }

        public void InsertRange(Int32 index, IEnumerable<T> items)
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            Order.InsertRange(index, items);
        }

        public Boolean Contains(T item)
        {
            return Order.Contains(item);
        }

        public Int32 GetOrder(T item)
        {
            return Order.IndexOf(item);
        }
        
        public Boolean GetOrder(T item, out Int32 order)
        {
            return Order.IndexOf(item!, out order);
        }
        
        public Boolean Remove(T item)
        {
            return Order.Remove(item);
        }
        
        public void Clear()
        {
            Order.Clear();
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