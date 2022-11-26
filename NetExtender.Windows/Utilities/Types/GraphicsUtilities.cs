// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Drawing;

namespace NetExtender.Utilities.Types
{
    public static class GraphicsUtilities
    {
        public static void DrawCircle(this Graphics graphics, Pen pen, Single x, Single y, Single d)
        {
            if (graphics is null)
            {
                throw new ArgumentNullException(nameof(graphics));
            }

            graphics.DrawEllipse(pen, x, y, d, d);
        }

        public static void DrawCircle(this Graphics graphics, Pen pen, Int32 x, Int32 y, Int32 d)
        {
            if (graphics is null)
            {
                throw new ArgumentNullException(nameof(graphics));
            }

            graphics.DrawEllipse(pen, x, y, d, d);
        }

        public static void FillCircle(this Graphics graphics, Brush brush, Single x, Single y, Single d)
        {
            if (graphics is null)
            {
                throw new ArgumentNullException(nameof(graphics));
            }

            graphics.FillEllipse(brush, x, y, d, d);
        }

        public static void FillCircle(this Graphics graphics, Brush brush, Int32 x, Int32 y, Int32 d)
        {
            if (graphics is null)
            {
                throw new ArgumentNullException(nameof(graphics));
            }

            graphics.FillEllipse(brush, x, y, d, d);
        }
    }
}