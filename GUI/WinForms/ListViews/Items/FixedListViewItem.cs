// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Drawing;
using System.Runtime.Serialization;
using System.Windows.Forms;
using NetExtender.Utils.GUI.Winforms.ListView;
using DynamicData.Annotations;
using NetExtender.Interfaces;

namespace NetExtender.GUI.WinForms.ListViews.Items
{
    public class FixedListViewItem : ListViewItem, ICloneable<ListViewItem>
    {
        public FixedListViewItem()
        {
        }

        protected FixedListViewItem([NotNull] SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public FixedListViewItem(String text)
            : base(text)
        {
        }

        public FixedListViewItem(String text, Int32 imageIndex)
            : base(text, imageIndex)
        {
        }

        public FixedListViewItem(String[] items)
            : base(items)
        {
        }

        public FixedListViewItem(String[] items, Int32 imageIndex)
            : base(items, imageIndex)
        {
        }

        public FixedListViewItem(String[] items, Int32 imageIndex, Color foreColor, Color backColor, Font font)
            : base(items, imageIndex, foreColor, backColor, font)
        {
        }

        public FixedListViewItem([NotNull] ListViewSubItem[] subItems, Int32 imageIndex)
            : base(subItems, imageIndex)
        {
        }

        public FixedListViewItem(ListViewGroup @group)
            : base(@group)
        {
        }

        public FixedListViewItem(String text, ListViewGroup @group)
            : base(text, @group)
        {
        }

        public FixedListViewItem(String text, Int32 imageIndex, ListViewGroup @group)
            : base(text, imageIndex, @group)
        {
        }

        public FixedListViewItem(String[] items, ListViewGroup @group)
            : base(items, @group)
        {
        }

        public FixedListViewItem(String[] items, Int32 imageIndex, ListViewGroup @group)
            : base(items, imageIndex, @group)
        {
        }

        public FixedListViewItem(String[] items, Int32 imageIndex, Color foreColor, Color backColor, Font font, ListViewGroup @group)
            : base(items, imageIndex, foreColor, backColor, font, @group)
        {
        }

        public FixedListViewItem([NotNull] ListViewSubItem[] subItems, Int32 imageIndex, ListViewGroup @group)
            : base(subItems, imageIndex, @group)
        {
        }

        public FixedListViewItem(String text, String imageKey)
            : base(text, imageKey)
        {
        }

        public FixedListViewItem(String[] items, String imageKey)
            : base(items, imageKey)
        {
        }

        public FixedListViewItem(String[] items, String imageKey, Color foreColor, Color backColor, Font font)
            : base(items, imageKey, foreColor, backColor, font)
        {
        }

        public FixedListViewItem([NotNull] ListViewSubItem[] subItems, String imageKey)
            : base(subItems, imageKey)
        {
        }

        public FixedListViewItem(String text, String imageKey, ListViewGroup @group)
            : base(text, imageKey, @group)
        {
        }

        public FixedListViewItem(String[] items, String imageKey, ListViewGroup @group)
            : base(items, imageKey, @group)
        {
        }

        public FixedListViewItem(String[] items, String imageKey, Color foreColor, Color backColor, Font font, ListViewGroup @group)
            : base(items, imageKey, foreColor, backColor, font, @group)
        {
        }

        public FixedListViewItem([NotNull] ListViewSubItem[] subItems, String imageKey, ListViewGroup @group)
            : base(subItems, imageKey, @group)
        {
        }

        public Image Image
        {
            get
            {
                return this.GetImage();
            }
            set
            {
                this.SetImage(value);
            }
        }
    }
}