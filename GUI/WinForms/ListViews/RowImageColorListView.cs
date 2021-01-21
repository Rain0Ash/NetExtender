// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using NetExtender.Utils.Types;
using NetExtender.Crypto;
using NetExtender.Exceptions;
using NetExtender.GUI.WinForms.ListViews.Items;
using NetExtender.Types.Dictionaries;
using NetExtender.Types.Drawing;

namespace NetExtender.GUI.WinForms.ListViews
{
    public class RowImageColorListView<T> : ValidableListView<T>
    {
        public Boolean OverlapAllowed { get; set; } = false;
        public Boolean OverlapCheckByText { get; set; } = true;

        public Boolean OverlapCheckByItem { get; set; } = true;
        
        private Color _defaultForegroundColor = Color.Black;

        public Color DefaultForegroundColor
        {
            get
            {
                return _defaultForegroundColor;
            }
            set
            {
                if (_defaultForegroundColor == value)
                {
                    return;
                }

                _defaultForegroundColor = value.IsEmpty ? Color.Black : value;

                Refresh();
            }
        }

        private Color _defaultBackgroundColor = Color.White;

        public Color DefaultBackgroundColor
        {
            get
            {
                return _defaultBackgroundColor;
            }
            set
            {
                if (_defaultBackgroundColor == value)
                {
                    return;
                }

                _defaultBackgroundColor = value.IsEmpty ? Color.White : value;

                Refresh();
            }
        }
        
        protected readonly ImageList Images = new ImageList();
        protected readonly EventDictionary<T, String> ImageDictionary = new EventDictionary<T, String>();
        protected readonly EventDictionary<T, DrawingData> ColorDictionary = new EventDictionary<T, DrawingData>();

        public RowImageColorListView()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            Columns.Add("default");
            OwnerDraw = true;
            GridLines = true;
            FullRowSelect = true;
            Images.Images.Add("null", NetExtender.Images.Images.Basic.Null);
            SmallImageList = Images;
            DrawColumnHeader += OnDrawColumnHeader;
            DrawItem += OnDrawItem;
            DrawSubItem += OnDrawSubItem;
            SizeChanged += OnSizeChanged;
            ClientSizeChanged += OnSizeChanged;
            ImageDictionary.ItemsChanged += Refresh;
            ColorDictionary.ItemsChanged += Refresh;
        }

        protected virtual void OnDrawItem(Object sender, DrawListViewItemEventArgs e)
        {
            e.DrawDefault = true;
            Int32 index = e.ItemIndex;

            if (!IndexInItems(index))
            {
                return;
            }

            if (e.Item is not GenericListViewItem<T> lvitem)
            {
                throw new CollectionSyncException();
            }

            DrawingData data = ColorDictionary.TryGetValue(lvitem.Item, new DrawingData(DefaultBackgroundColor, DefaultForegroundColor, lvitem.Font));

            lvitem.ImageKey = GetItemImageKey(lvitem);
            lvitem.BackColor = GetItemBackColor(lvitem, data);
            lvitem.ForeColor = GetItemForeColor(lvitem, data);
            lvitem.Font = GetItemFont(lvitem, data);
            e.Graphics.FillRectangle(new SolidBrush(lvitem.BackColor), e.Item.Bounds);
        }

        protected virtual String GetItemImageKey(GenericListViewItem<T> lvitem)
        {
            return ImageDictionary.TryGetValue(lvitem.Item, "null");
        }
        
        protected virtual Color GetItemBackColor(GenericListViewItem<T> lvitem, DrawingData data)
        {
            return !IsValidItem(lvitem) ? InvalidColor : data.BackgroundColor.IsEmpty ? DefaultBackgroundColor : data.BackgroundColor;
        }
        
        protected virtual Color GetItemForeColor(GenericListViewItem<T> lvitem, DrawingData data)
        {
            return data.ForegroundColor.IsEmpty ? DefaultForegroundColor : data.ForegroundColor;
        }
        
        protected virtual Font GetItemFont(GenericListViewItem<T> lvitem, DrawingData data)
        {
            return data.Font ?? lvitem.Font;
        }

        protected virtual void OnDrawColumnHeader(Object sender, DrawListViewColumnHeaderEventArgs e)
        {
            e.DrawDefault = true;
        }
        
        protected virtual void OnDrawSubItem(Object sender, DrawListViewSubItemEventArgs e)
        {
            e.DrawDefault = true;
        }

        public void SetColor(T item, Color background, Color foreground, Font font = null)
        {
            ColorDictionary.Set(item, new DrawingData(background, foreground, font));
        }
        
        public void RemoveColor(T item)
        {
            ColorDictionary.Remove(item);
        }

        public Image GetImage(T item)
        {
            if (!ImageDictionary.TryGetValue(item, out String hash))
            {
                return null;
            }

            return Images.Images.ContainsKey(hash) ? Images.Images[hash] : null;
        }
        
        public void SetImage(T item, Image image)
        {
            String hash = image.GetHash(HashType.MD5).GetStringFromBytes();

            if (!Images.Images.ContainsKey(hash))
            {
                Images.Images.Add(hash, image);
            }
            
            ImageDictionary.Set(item, hash);
        }
        
        public void RemoveImage(T item)
        {
            if (!ImageDictionary.TryGetValue(item, out String hash))
            {
                return;
            }

            ImageDictionary.Remove(item);
            
            if (hash is not null && !hash.Equals("null", StringComparison.OrdinalIgnoreCase)
                             && !ImageDictionary.Any(pair => pair.Value.Equals(hash, StringComparison.OrdinalIgnoreCase)))
            {
                Images.Images.RemoveByKey(hash);
            }
        }

        public Boolean TryAdd(T item)
        {
            return TryInsert(Items.Count, item);
        }

        public Boolean TryAdd(T item, Image image)
        {
            return TryInsert(Items.Count, item, image);
        }

        public Boolean TryAdd(T item, Image image, Color background, Color foreground, Font font = null)
        {
            return TryInsert(Items.Count, item, image, background, foreground, font);
        }

        public Boolean TryAdd(GenericListViewItem<T> lvitem)
        {
            return TryInsert(Items.Count, lvitem);
        }

        public Boolean TryInsert(Int32 index, T item)
        {
            GenericListViewItem<T> lvitem = new GenericListViewItem<T>(item.ToString())
            {
                Item = item
            };
            
            return TryInsert(index, lvitem);
        }

        public Boolean TryInsert(Int32 index, T item, Image image)
        {
            if (!TryInsert(index, item))
            {
                return false;
            }
            
            SetImage(item, image);
            return true;
        }

        public Boolean TryInsert(Int32 index, T item, Image image, Color background, Color foreground, Font font = null)
        {
            if (!TryInsert(index, item, image))
            {
                return false;
            }
            
            SetColor(item, background, foreground, font);
            return true;
        }
        
        public virtual Boolean TryInsert(Int32 index, GenericListViewItem<T> lvitem)
        {
            if (lvitem is null)
            {
                throw new ArgumentNullException(nameof(lvitem));
            }

            if (!OverlapAllowed && Items.Any(item => OverlapCheckByText && item.Text.Equals(lvitem.Text) 
                                                     || OverlapCheckByItem && lvitem.Item?.Equals(default) == false && item.Item?.Equals(lvitem.Item) == true))
            {
                return false;
            }

            if (index < Items.Count)
            {
                Items.Insert(index, lvitem);
                return true;
            }

            Items.Add(lvitem);
            return true;
        }

        public virtual Boolean Remove(T item)
        {
            if (item.Equals(default))
            {
                return false;
            }

            return Items.Where(lvitem => lvitem.Item?.Equals(item) == true)
                .Aggregate(false, (any, lvitem) => any | Remove(lvitem));
        }
        
        public Boolean RemoveFirst(T item)
        {
            return item?.Equals(default) == false && Remove(Items.FirstOrDefault(lvitem => lvitem.Item.IsEquals(item)));
        }

        public virtual Boolean Remove(GenericListViewItem<T> lvitem)
        {
            if (lvitem is null)
            {
                return false;
            }

            Boolean removed = Items.Remove(lvitem);
            
            RemoveImage(lvitem.Item);
            RemoveColor(lvitem.Item);

            return removed;
        }
        
        public void RemoveAt(Int32 index)
        {
            Remove(Items[index].Item);
        }

        public void AddRange(IEnumerable<T> items)
        {
            foreach (T item in items)
            {
                TryAdd(item);
            }
        }

        public void AddRange(IEnumerable<GenericListViewItem<T>> items)
        {
            foreach (GenericListViewItem<T> lvitem in items)
            {
                TryAdd(lvitem);
            }
        }
        
        public void InsertRange(Int32 index, IEnumerable<T> items)
        {
            foreach (T item in items)
            {
                TryInsert(index++, item);
            }
        }
        
        public void OnSizeChanged(Object sender, EventArgs e)
        {
            if (Columns.Count == 1)
            {
                Columns[0].Width = ClientSize.Width;
            }
        }
    }
}