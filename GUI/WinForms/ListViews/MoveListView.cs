// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Drawing;
using System.Windows.Forms;

namespace NetExtender.GUI.WinForms.ListViews
{
    public enum ListViewInsertionMode
    {
        Before,
        After
    }
    
    public class MoveListView : FixedListView
    {
        private const Int32 WmPaint = 0xF;
        
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            switch (m.Msg)
            {
                case WmPaint:
                    DrawInsertionLine();
                    break;
            }
        }
        
        public override Boolean AllowDrop
        {
            get
            {
                return true;
            }
            // ReSharper disable once ValueParameterNotUsed
            set
            {
                base.AllowDrop = true;
            }
        }

        public virtual Color InsertionLineColor { get; set; }

        protected Int32 InsertionIndex { get; set; }

        protected ListViewInsertionMode InsertionMode { get; set; }

        protected Boolean IsRowDragInProgress { get; set; }

        public MoveListView()
        {
            AllowDrop = true;
            InsertionLineColor = Color.Red;
            InsertionIndex = -1;
        }
        
        private void DrawInsertionLine()
        {
            if (InsertionIndex == -1)
            {
                return;
            }

            Int32 index = InsertionIndex;

            if (index < 0 || index >= Items.Count)
            {
                return;
            }

            Rectangle bounds = Items[index].GetBounds(ItemBoundsPortion.Entire);
            const Int32 x = 0;
            Int32 y = InsertionMode == ListViewInsertionMode.Before ? bounds.Top : bounds.Bottom;
            Int32 width = Math.Min(bounds.Width - bounds.Left, ClientSize.Width);

            DrawInsertionLine(x, y, width);
        }

        private void DrawInsertionLine(Int32 x1, Int32 y, Int32 width)
        {
            using Graphics g = CreateGraphics();
            
            Int32 x2 = x1 + width;
            const Int32 arrowHeadSize = 7;
            Point[] leftArrowHead = 
            {
                new Point(x1, y - arrowHeadSize / 2),
                new Point(x1 + arrowHeadSize, y),
                new Point(x1, y + arrowHeadSize / 2)
            };
                
            Point[] rightArrowHead = 
            {
                new Point(x2, y - arrowHeadSize / 2),
                new Point(x2 - arrowHeadSize, y),
                new Point(x2, y + arrowHeadSize / 2)
            };

            using (Pen pen = new Pen(InsertionLineColor))
            {
                g.DrawLine(pen, x1, y, x2 - 1, y);
            }

            using (Brush brush = new SolidBrush(InsertionLineColor))
            {
                g.FillPolygon(brush, leftArrowHead);
                g.FillPolygon(brush, rightArrowHead);
            }
        }
        
        protected override void OnItemDrag(ItemDragEventArgs e)
        {
            if (Items.Count > 1)
            {
                IsRowDragInProgress = true;
                DoDragDrop(e.Item, DragDropEffects.Move);
            }

            base.OnItemDrag(e);
        }
        
        protected override void OnDragOver(DragEventArgs drgevent)
        {
            if (IsRowDragInProgress)
            {
                Int32 insertionIndex;
                ListViewInsertionMode insertionMode;

                Point clientPoint = PointToClient(new Point(drgevent.X, drgevent.Y));
                ListViewItem dropItem = GetItemAt(0, Math.Min(clientPoint.Y, Items[^1].GetBounds(ItemBoundsPortion.Entire).Bottom - 1));

                if (dropItem is not null)
                {
                    Rectangle bounds = dropItem.GetBounds(ItemBoundsPortion.Entire);
                    insertionIndex = dropItem.Index;
                    insertionMode = clientPoint.Y < bounds.Top + bounds.Height / 2 ? ListViewInsertionMode.Before : ListViewInsertionMode.After;

                    drgevent.Effect = DragDropEffects.Move;
                }
                else
                {
                    insertionIndex = -1;
                    insertionMode = InsertionMode;

                    drgevent.Effect = DragDropEffects.None;
                }

                if (insertionIndex != InsertionIndex || insertionMode != InsertionMode)
                {
                    InsertionMode = insertionMode;
                    InsertionIndex = insertionIndex;
                    Invalidate();
                }
            }

            base.OnDragOver(drgevent);
        }
        
        protected override void OnDragLeave(EventArgs e)
        {
            InsertionIndex = -1;
            Invalidate();

            base.OnDragLeave(e);
        }
        
        protected override void OnDragDrop(DragEventArgs drgevent)
        {
            if (IsRowDragInProgress)
            {
                ListViewItem dropItem = InsertionIndex != -1 ? Items[InsertionIndex] : null;

                if (dropItem is not null)
                {
                    ListViewItem dragItem = (ListViewItem)drgevent.Data.GetData(typeof(ListViewItem));
                    Int32 dropIndex = dropItem.Index;

                    if (dragItem.Index < dropIndex)
                    {
                        dropIndex--;
                    }
                    if (InsertionMode == ListViewInsertionMode.After && dragItem.Index < Items.Count - 1)
                    {
                        dropIndex++;
                    }

                    if (dropIndex != dragItem.Index)
                    {
                        
                        Items.Remove(dragItem);
                        Items.Insert(dropIndex, dragItem);
                        SelectedItems.Clear();
                        dragItem.Selected = true;
                    }
                }

                InsertionIndex = -1;
                IsRowDragInProgress = false;
                Invalidate();
            }

            base.OnDragDrop(drgevent);
        }
    }
}