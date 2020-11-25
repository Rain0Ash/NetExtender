// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Drawing;
using System.Runtime.Serialization;
using System.Windows.Forms;
using DynamicData.Annotations;

namespace NetExtender.GUI.WinForms.ListViews.Items
{
    public class GenericListViewItem : GenericListViewItem<Object>
    {
    }
    
    public class GenericListViewItem<T> : FixedListViewItem
    {
        #region ctor

        public GenericListViewItem()
        {
        }

        protected GenericListViewItem([NotNull] SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public GenericListViewItem(String text)
            : base(text)
        {
        }

        public GenericListViewItem(String text, Int32 imageIndex)
            : base(text, imageIndex)
        {
        }

        public GenericListViewItem(String[] items)
            : base(items)
        {
        }

        public GenericListViewItem(String[] items, Int32 imageIndex)
            : base(items, imageIndex)
        {
        }

        public GenericListViewItem(String[] items, Int32 imageIndex, Color foreColor, Color backColor, Font font)
            : base(items, imageIndex, foreColor, backColor, font)
        {
        }

        public GenericListViewItem([NotNull] ListViewSubItem[] subItems, Int32 imageIndex)
            : base(subItems, imageIndex)
        {
        }

        public GenericListViewItem(ListViewGroup @group)
            : base(@group)
        {
        }

        public GenericListViewItem(String text, ListViewGroup @group)
            : base(text, @group)
        {
        }

        public GenericListViewItem(String text, Int32 imageIndex, ListViewGroup @group)
            : base(text, imageIndex, @group)
        {
        }

        public GenericListViewItem(String[] items, ListViewGroup @group)
            : base(items, @group)
        {
        }

        public GenericListViewItem(String[] items, Int32 imageIndex, ListViewGroup @group)
            : base(items, imageIndex, @group)
        {
        }

        public GenericListViewItem(String[] items, Int32 imageIndex, Color foreColor, Color backColor, Font font, ListViewGroup @group)
            : base(items, imageIndex, foreColor, backColor, font, @group)
        {
        }

        public GenericListViewItem([NotNull] ListViewSubItem[] subItems, Int32 imageIndex, ListViewGroup @group)
            : base(subItems, imageIndex, @group)
        {
        }

        public GenericListViewItem(String text, String imageKey)
            : base(text, imageKey)
        {
        }

        public GenericListViewItem(String[] items, String imageKey)
            : base(items, imageKey)
        {
        }

        public GenericListViewItem(String[] items, String imageKey, Color foreColor, Color backColor, Font font)
            : base(items, imageKey, foreColor, backColor, font)
        {
        }

        public GenericListViewItem([NotNull] ListViewSubItem[] subItems, String imageKey)
            : base(subItems, imageKey)
        {
        }

        public GenericListViewItem(String text, String imageKey, ListViewGroup @group)
            : base(text, imageKey, @group)
        {
        }

        public GenericListViewItem(String[] items, String imageKey, ListViewGroup @group)
            : base(items, imageKey, @group)
        {
        }

        public GenericListViewItem(String[] items, String imageKey, Color foreColor, Color backColor, Font font, ListViewGroup @group)
            : base(items, imageKey, foreColor, backColor, font, @group)
        {
        }

        public GenericListViewItem([NotNull] ListViewSubItem[] subItems, String imageKey, ListViewGroup @group)
            : base(subItems, imageKey, @group)
        {
        }

        #endregion

        public event EmptyHandler ItemChanged;

        private T _item;
        
        public T Item
        {
            get
            {
                return _item;
            }
            set
            {
                if (_item?.Equals(value) == true)
                {
                    return;
                }

                _item = value;
                
                ItemChanged?.Invoke();
            }
        }
    }
}