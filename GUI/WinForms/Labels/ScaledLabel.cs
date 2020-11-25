// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Drawing;
using System.Windows.Forms;

namespace NetExtender.GUI.WinForms.Labels
{
    public class ScaledLabel : FixedLabel
    {
        private event EmptyHandler ScaleFontChanged;
        private Boolean _scaleFont;

        public Boolean ScaleFont
        {
            get
            {
                return _scaleFont;
            }
            set
            {
                if (_scaleFont == value)
                {
                    return;
                }

                _scaleFont = value;
                ScaleFontChanged?.Invoke();
            }
        }

        public ScaledLabel()
        {
            ScaleFontChanged += Scale;
            TextChanged += (sender, args) => Scale();
        }

        private void Scale()
        {
            if (!ScaleFont)
            {
                return;
            }

            SizeF extent = TextRenderer.MeasureText(Text, Font);

            Single hRatio = Height / extent.Height;
            Single wRatio = Width / extent.Width;
            Single ratio = hRatio < wRatio ? hRatio : wRatio;

            Single newSize = Font.Size * ratio;

            if (newSize <= 0)
            {
                return;
            }

            Font = new Font(Font.FontFamily, newSize, Font.Style);
        }
    }
}