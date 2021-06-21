// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.ComponentModel;
using System.Linq;

namespace NetExtender.Types.BindingLists
{
    public class EventBindingList<T> : BindingList<T>
    {
        public event TypeHandler<T> OnAdd;
        public event TypeHandler<T> OnRemove;
        public event EmptyHandler OnClear;
        public event EmptyHandler ItemsChanged;

        public new void Add(T item)
        {
            base.Add(item);
            OnAdd?.Invoke(item);
            ItemsChanged?.Invoke();
        }

        public new void Remove(T item)
        {
            if (!Contains(item))
            {
                return;
            }

            base.Remove(item);
            OnRemove?.Invoke(item);
            ItemsChanged?.Invoke();
        }

        public new void RemoveAt(Int32 index)
        {
            T item = this[index];
            base.RemoveAt(index);
            OnRemove?.Invoke(item);
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

        public void RemoveRange(Int32 index, Int32 count = 1)
        {
            if (index < 0 || index + count >= Count)
            {
                throw new IndexOutOfRangeException();
            }

            foreach (T item in this.Skip(index).Take(count))
            {
                Remove(item);
            }
        }
    }
}