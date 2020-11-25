// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace NetExtender.GUI.WinForms.ListBoxes
{
    public class FixedListBox : ListBox
    {
        public FixedListBox()
        {
            DoubleBuffered = true;
        }

        protected Boolean IndexInItems(Int32 index)
        {
            return DataSource switch
            {
                null => (index >= 0 && index < Items.Count),
                List<Object> source => (index >= 0 && index < source.Count),
                _ => false
            };
        }
    }
}