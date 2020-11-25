// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Windows.Forms;
using NetExtender.Exceptions;
using NetExtender.GUI.WinForms.ListViews.Items;

namespace NetExtender.GUI.WinForms.ListViews
{
    public class EventListView<T> : RowImageColorListView<T>
    {
        public event KeyValueHandler<GenericListViewItem<T>, MouseEventArgs> ItemClick;
        public event KeyValueHandler<GenericListViewItem<T>, MouseEventArgs> ItemDoubleClick;
        public event TypeHandler<MouseEventArgs> EmptyClick; 
        public event TypeHandler<MouseEventArgs> EmptyDoubleClick; 
        
        public event IndexTypeHandler<GenericListViewItem<T>> ItemAdded;
        public event TypeHandler<T> ItemRemoved;

        public EventListView()
        {
            MouseDown += OnMouseClick;
        }
        
        public override Boolean TryInsert(Int32 index, GenericListViewItem<T> lvitem)
        {
            if (!base.TryInsert(index, lvitem))
            {
                return false;
            }
            
            ItemAdded?.Invoke(index, lvitem);
            return true;
        }

        public override Boolean Remove(T item)
        {
            if (!base.Remove(item))
            {
                return false;
            }

            ItemRemoved?.Invoke(item);
            return true;
        }

        private void OnItemClick(GenericListViewItem<T> item, MouseEventArgs e)
        {
            switch (e.Clicks)
            {
                case 1:
                    ItemClick?.Invoke(item, e);
                    break;
                case 2:
                    ItemDoubleClick?.Invoke(item, e);
                    break;
                default:
                    break;
            }
        }
        
        private void OnEmptyClick(MouseEventArgs e)
        {
            switch (e.Clicks)
            {
                case 1:
                    EmptyClick?.Invoke(e);
                    break;
                case 2:
                    EmptyDoubleClick?.Invoke(e);
                    break;
                default:
                    break;
            }
            
            SelectedItems.Clear();
        }
        
        public void OnMouseClick(Object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons)
            {
                return;
            }

            switch (HitTest(e.X, e.Y).Item)
            {
                case null:
                    OnEmptyClick(e);
                    return;
                case GenericListViewItem<T> lvitem:
                    OnItemClick(lvitem, e);
                    return;
                default:
                    throw new CollectionSyncException();
            }
        }
    }
}