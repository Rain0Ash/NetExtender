// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace NetExtender.GUI.WinForms.ListViews.Items
{
    public class SelectedListViewItemList<T> : IReadOnlyList<GenericListViewItem<T>>
    {
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

        private readonly ListView.SelectedListViewItemCollection _items;

        public SelectedListViewItemList(ListView.SelectedListViewItemCollection items)
        {
            _items = items;
        }

        public Boolean Contains(GenericListViewItem<T> item)
        {
            return _items.Contains(item);
        }

        public Int32 IndexOf(GenericListViewItem<T> item)
        {
            return _items.IndexOf(item);
        }

        public void Clear()
        {
            _items.Clear();
        }

        public void CopyTo(GenericListViewItem<T>[] items, Int32 index)
        {
            _items.CopyTo(items, index);
        }

        public GenericListViewItem<T> this[Int32 index]
        {
            get
            {
                return (GenericListViewItem<T>) _items[index];
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