// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Drawing;
using System.Windows.Forms;

namespace NetExtender.GUI.WinForms.ToolStrips.Items
{
    public class FixedToolStripMenuItem : ToolStripMenuItem
    {
        public Int32 AllowOnMinItems { get; set; } = 0;
        public Int32 AllowOnMaxItems { get; set; } = Int32.MaxValue;

        public FixedToolStripMenuItem()
        {
        }

        public FixedToolStripMenuItem(String text)
            : base(text)
        {
        }

        public FixedToolStripMenuItem(Image image)
            : base(image)
        {
        }

        public FixedToolStripMenuItem(String text, Image image)
            : base(text, image)
        {
        }

        public FixedToolStripMenuItem(String text, Image image, EventHandler onClick)
            : base(text, image, onClick)
        {
        }

        public FixedToolStripMenuItem(String text, Image image, EventHandler onClick, String name)
            : base(text, image, onClick, name)
        {
        }

        public FixedToolStripMenuItem(String text, Image image, params ToolStripItem[] dropDownItems)
            : base(text, image, dropDownItems)
        {
        }

        public FixedToolStripMenuItem(String text, Image image, EventHandler onClick, Keys shortcutKeys)
            : base(text, image, onClick, shortcutKeys)
        {
        }

        public Boolean SelectedCountInRange(Int32 selectedCount)
        {
            return selectedCount >= AllowOnMinItems && selectedCount <= AllowOnMaxItems;
        }
    }
}