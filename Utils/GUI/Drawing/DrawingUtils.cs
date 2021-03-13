// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Drawing;
using JetBrains.Annotations;

namespace NetExtender.Utils.GUI.Drawing
{
    public static class DrawingUtils
    {
        public static void DrawCircle([NotNull] this Graphics graphics, Pen pen, Single x, Single y, Single d)
        {
            if (graphics is null)
            {
                throw new ArgumentNullException(nameof(graphics));
            }

            graphics.DrawEllipse(pen, x, y, d, d);
        }

        public static void DrawCircle([NotNull] this Graphics graphics, Pen pen, Int32 x, Int32 y, Int32 d)
        {
            if (graphics is null)
            {
                throw new ArgumentNullException(nameof(graphics));
            }

            graphics.DrawEllipse(pen, x, y, d, d);
        }

        public static void FillCircle([NotNull] this Graphics graphics, Brush brush, Single x, Single y, Single d)
        {
            if (graphics is null)
            {
                throw new ArgumentNullException(nameof(graphics));
            }
            
            graphics.FillEllipse(brush, x, y, d, d);
        }

        public static void FillCircle([NotNull] this Graphics graphics, Brush brush, Int32 x, Int32 y, Int32 d)
        {
            if (graphics is null)
            {
                throw new ArgumentNullException(nameof(graphics));
            }

            graphics.FillEllipse(brush, x, y, d, d);
        }
        
        public static Double GetAspectRatio(this Size size)
        {
            return GetAspectRatio(size.Width, size.Height);
        }
        
        public static Double GetAspectRatio(Int32 width, Int32 height)
        {
            return GetAspectRatio((Double) width, height);
        }
        
        public static Double GetAspectRatio(Double width, Double height)
        {
            return width / height;
        }
        
        public static Size AspectRatioBoundsSize(this Size original, Size bounds)
        {
            Size result = new Size();
            Double difference = GetAspectRatio(original);

            result.Width = bounds.Width;
            result.Height = (Int32) GetAspectRatio(original.Height, difference);

            // The aspect ratios of the original and bounding rectangles are on opposite sides of 1.0.
            // That means the result height is too big to fit in the bounding rectangle.
            // The result needs to be calculated again to make the height fit, and the
            // width needs to be scaled down to maintain the original rectangle's aspect ratio.
            if (result.Height <= bounds.Height)
            {
                return result;
            }

            Double heightdifference = GetAspectRatio(result.Height, bounds.Height);
            result.Width = (Int32) GetAspectRatio(bounds.Width, heightdifference);
            result.Height = bounds.Height;

            return result;
        }
    }
}