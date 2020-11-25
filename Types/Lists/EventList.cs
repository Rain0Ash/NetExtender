// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using NetExtender.Types.Lists.Interfaces;

namespace NetExtender.Types.Lists
{
    public class EventList<T> : List<T>, IEventList<T>, IReadOnlyEventList<T>
    {
        public event RTypeHandler<T> OnAdd;
        public event IndexRTypeHandler<T> OnInsert;
        public event RTypeHandler<T> OnSet;
        public event RTypeHandler<T> OnRemove;
        public event RTypeHandler<T> OnChange;

        public event IndexRTypeHandler<T> OnChangeIndex;
        public event EmptyHandler OnClear;
        public event EmptyHandler ItemsChanged;

        public EventList()
        {
        }

        public EventList(IEnumerable<T> collection)
            : base(collection)
        {
        }

        public EventList(Int32 capacity)
            : base(capacity)
        {
        }

        public new void Add(T item)
        {
            base.Add(item);
            OnAdd?.Invoke(ref item);
            ItemsChanged?.Invoke();
        }

        public new void Remove(T item)
        {
            if (!Contains(item))
            {
                return;
            }

            base.Remove(item);
            OnRemove?.Invoke(ref item);
            ItemsChanged?.Invoke();
        }

        public new void RemoveAt(Int32 index)
        {
            T item = this[index];
            base.RemoveAt(index);
            OnRemove?.Invoke(ref item);
            OnChangeIndex?.Invoke(index, ref item);
            ItemsChanged?.Invoke();
        }

        public new void Insert(Int32 index, T item)
        {
            base.Insert(index, item);
            OnAdd?.Invoke(ref item);
            OnInsert?.Invoke(index, ref item);
            ItemsChanged?.Invoke();
        }

        public new void Clear()
        {
            Boolean any = this.Any();
            base.Clear();
            if (!any)
            {
                return;
            }

            OnClear?.Invoke();
            ItemsChanged?.Invoke();
        }

        public new T this[Int32 index]
        {
            get
            {
                return base[index];
            }
            set
            {
                base[index] = value;
                OnSet?.Invoke(ref value);
            }
        }
    }
}