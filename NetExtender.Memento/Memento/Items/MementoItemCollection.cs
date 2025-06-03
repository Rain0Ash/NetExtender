// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NetExtender.Types.Memento
{
    public sealed class MementoItemCollection<TSource> : IMementoItem<TSource>, IList<IMementoItem<TSource>> where TSource : class
    {
        private List<IMementoItem<TSource>> Internal { get; }

        public TSource Source
        {
            get
            {
                throw new InvalidOperationException($"{nameof(MementoItemCollection<TSource>)} can't get {nameof(Source)}");
            }
        }

        public Int32 Count
        {
            get
            {
                return Internal.Count;
            }
        }

        public Boolean HasValue
        {
            get
            {
                return true;
            }
        }

        Boolean ICollection<IMementoItem<TSource>>.IsReadOnly
        {
            get
            {
                return ((ICollection<IMementoItem<TSource>>) Internal).IsReadOnly;
            }
        }

        private Boolean ReverseOrder { get; set; }

        public MementoItemCollection()
        {
            Internal = new List<IMementoItem<TSource>>();
        }

        public MementoItemCollection(Int32 capacity)
        {
            Internal = new List<IMementoItem<TSource>>(capacity);
        }

        public MementoItemCollection(params IMementoItem<TSource>[] items)
            : this((IEnumerable<IMementoItem<TSource>>) items)
        {
        }

        public MementoItemCollection(IEnumerable<IMementoItem<TSource>> items)
        {
            Internal = new List<IMementoItem<TSource>>(items);
        }

        public Boolean Contains(IMementoItem<TSource> item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return Internal.Contains(item);
        }

        public Int32 IndexOf(IMementoItem<TSource> item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return Internal.IndexOf(item);
        }

        public void Add(IMementoItem<TSource> item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            Internal.Add(item);
        }

        public void AddRange(params IMementoItem<TSource>[] items)
        {
            AddRange((IEnumerable<IMementoItem<TSource>>) items);
        }

        public void AddRange(IEnumerable<IMementoItem<TSource>> items)
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            Internal.AddRange(items);
        }

        public void Insert(Int32 index, IMementoItem<TSource> item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            Internal.Insert(index, item);
        }

        public IMementoItem<TSource> Swap()
        {
            foreach (IMementoItem<TSource> item in this)
            {
                item.Swap();
            }

            ReverseOrder = !ReverseOrder;
            return this;
        }

        IMementoItem IMementoItem.Swap()
        {
            return Swap();
        }

        public IMementoItem<TSource> Update()
        {
            foreach (IMementoItem<TSource> item in this)
            {
                item.Update();
            }

            return this;
        }

        IMementoItem IMementoItem.Update()
        {
            return Update();
        }

        public Boolean Remove(IMementoItem<TSource> item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            return Internal.Remove(item);
        }

        public void RemoveAt(Int32 index)
        {
            Internal.RemoveAt(index);
        }

        public void Clear()
        {
            Internal.Clear();
        }

        public void CopyTo(IMementoItem<TSource>[] array, Int32 index)
        {
            Internal.CopyTo(array, index);
        }

        public IEnumerator<IMementoItem<TSource>> GetEnumerator()
        {
            IEnumerable<IMementoItem<TSource>> enumerator = Internal;

            if (ReverseOrder)
            {
                enumerator = enumerator.Reverse();
            }

            return enumerator.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IMementoItem<TSource> this[Int32 index]
        {
            get
            {
                return Internal[index];
            }
            set
            {
                Internal[index] = value ?? throw new ArgumentNullException(nameof(value));
            }
        }
    }
}
