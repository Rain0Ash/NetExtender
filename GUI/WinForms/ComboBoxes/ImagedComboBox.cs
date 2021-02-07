﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Drawing;
using System.Windows.Forms;
using NetExtender.Types.Strings;
using NetExtender.Types.Strings.Interfaces;

 namespace NetExtender.GUI.WinForms.ComboBoxes
{
    public class ImagedComboBox : LanguageComboBox
    {
        public ImagedComboBox()
        {
            DrawMode = DrawMode.OwnerDrawFixed;
            DropDownStyle = ComboBoxStyle.DropDownList;
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            e.DrawBackground();

            e.DrawFocusRectangle();

            if (e.Index >= 0 && e.Index < Items.Count)
            {
                DropDownItem item = (DropDownItem) Items[e.Index];

                e.Graphics.DrawImage(item.Image, e.Bounds.Left, e.Bounds.Top);

                e.Graphics.DrawString(item.Value.ToString(), e.Font, new SolidBrush(e.ForeColor), e.Bounds.Left + item.Image.Width, e.Bounds.Top + 2);
            }

            base.OnDrawItem(e);
        }
    }

    public sealed class DropDownItem
    {
        public IString Value { get; }
        public Image Image { get; init; }

        public DropDownItem(String text, Image image = null)
            : this(new StringAdapter(text), image)
        {
        }

        public DropDownItem(IString text, Image image = null)
        {
            Value = text;
            Image = image ?? new Bitmap(16, 16);
            using Graphics graphics = Graphics.FromImage(Image);
            using Brush brush = new SolidBrush(Color.FromName(text.ToString()));
            graphics.DrawRectangle(Pens.White, 0, 0, Image.Width, Image.Height);
            graphics.FillRectangle(brush, 1, 1, Image.Width - 1, Image.Height - 1);
        }

        public override String ToString()
        {
            return Value.ToString();
        }
    }
}