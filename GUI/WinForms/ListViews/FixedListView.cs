// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Windows.Forms;

namespace NetExtender.GUI.WinForms.ListViews
{
    public class FixedListView : ListView
    {
        public FixedListView()
        {
            DoubleBuffered = true;
        }

        public event EmptyHandler ItemsChanged;
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x1007:
                    ItemsChanged?.Invoke();
                    break;
                case 0x104D:
                    ItemsChanged?.Invoke();
                    break;
                case 0x1008:
                    ItemsChanged?.Invoke();
                    break;
                case 0x1009:
                    ItemsChanged?.Invoke();
                    break;
                default:
                    break;
            }
            
            base.WndProc(ref m);
        }

        protected void SwapItems(Int32 index1, Int32 index2)
        {
            ListViewItem item = Items[index1];
            Items.Remove(item);
            Items.Insert(index2, item);
        }
        
        public Boolean IndexInItems(Int32 index)
        {
            return index >= 0 && index < Items.Count;
        }
    }
}