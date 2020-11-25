// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using NetExtender.Types.Lists.Interfaces;

namespace NetExtender.GUI.WinForms.ListViews.Items
{
    public class ListViewGenericItemList<T> : IEventList<GenericListViewItem<T>>
    {
        public event RTypeHandler<GenericListViewItem<T>> OnAdd;
        public event RTypeHandler<GenericListViewItem<T>> OnSet;
        public event RTypeHandler<GenericListViewItem<T>> OnRemove;
        public event RTypeHandler<GenericListViewItem<T>> OnChange;
        public event IndexRTypeHandler<GenericListViewItem<T>> OnInsert;
        public event IndexRTypeHandler<GenericListViewItem<T>> OnChangeIndex;
        public event EmptyHandler OnClear;
        public event EmptyHandler ItemsChanged;

        private readonly ListView.ListViewItemCollection _items;

        public Int32 Count
        {
            get
            {
                return _items.Count;
            }
        }

        public Boolean IsReadOnly
        {
            get
            {
                return _items.IsReadOnly;
            }
        }

        public ListViewGenericItemList(ListView.ListViewItemCollection items)
        {
            _items = items;
        }
        
        public void Add(GenericListViewItem<T> item)
        {
            _items.Add(item ?? throw new ArgumentNullException(nameof(item)));
            OnAdd?.Invoke(ref item);
            ItemsChanged?.Invoke();
        }
        
        public void Insert(Int32 index, GenericListViewItem<T> item)
        {
            _items.Insert(index, item ?? throw new ArgumentNullException(nameof(item)));
            OnInsert?.Invoke(index, ref item);
            ItemsChanged?.Invoke();
        }

        public Boolean Remove(GenericListViewItem<T> item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            if (!Contains(item))
            {
                return false;
            }
            
            _items.Remove(item);
            OnRemove?.Invoke(ref item);
            ItemsChanged?.Invoke();

            return true;
        }
        
        public Int32 IndexOf(GenericListViewItem<T> item)
        {
            return _items.IndexOf(item);
        }

        public void RemoveAt(Int32 index)
        {
            if (index < 0 || index >= Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            GenericListViewItem<T> item = (GenericListViewItem<T>) _items[index];
            
            _items.RemoveAt(index);

            if (item is null)
            {
                return;
            }

            OnRemove?.Invoke(ref item);
            ItemsChanged?.Invoke();
        }
        
        public void Clear()
        {
            Boolean any = Count > 0;
            _items.Clear();
            if (!any)
            {
                return;
            }

            OnClear?.Invoke();
            ItemsChanged?.Invoke();
        }

        public Boolean Contains(GenericListViewItem<T> item)
        {
            return _items.Contains(item ?? throw new ArgumentNullException(nameof(item)));
        }

        public void CopyTo(GenericListViewItem<T>[] array, Int32 arrayIndex)
        {
            _items.CopyTo(array, arrayIndex);
        }

        public GenericListViewItem<T> this[Int32 index]
        {
            get
            {
                return (GenericListViewItem<T>) _items[index];
            }
            set
            {
                _items[index] = value;
            }
        }
        
        public IEnumerator<GenericListViewItem<T>> GetEnumerator()
        {
            return _items.Cast<GenericListViewItem<T>>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}