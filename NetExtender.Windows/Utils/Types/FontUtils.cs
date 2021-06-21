// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Drawing;
using NetExtender.Utils.Numerics;

namespace NetExtender.Utils.Types
{
    public static class FontUtils
    {
        public static Font Resize(this Font font, Single emsize)
        {
            if (font is null)
            {
                throw new ArgumentNullException(nameof(font));
            }

            return new Font(font.FontFamily, emsize, font.Style, font.Unit, font.GdiCharSet, font.GdiVerticalFont);
        }

        public static Font ResizeDelta(this Font font, Single delta)
        {
            if (font is null)
            {
                throw new ArgumentNullException(nameof(font));
            }

            Single size = font.SizeInPoints + delta;

            return Resize(font, size.ToRange(0.01f));
        }
    }
}