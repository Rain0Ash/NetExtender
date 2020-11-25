// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Drawing;

namespace NetExtender.Utils.GUI.Drawing
{
    public static class DrawingUtils
    {
        public static void DrawCircle(this Graphics g, Pen pen, Single x, Single y, Single d)
        {
            g.DrawEllipse(pen, x, y, d, d);
        }

        public static void DrawCircle(this Graphics g, Pen pen, Int32 x, Int32 y, Int32 d)
        {
            g.DrawEllipse(pen, x, y, d, d);
        }

        public static void FillCircle(this Graphics g, Brush brush, Single x, Single y, Single d)
        {
            g.FillEllipse(brush, x, y, d, d);
        }

        public static void FillCircle(this Graphics g, Brush brush, Int32 x, Int32 y, Int32 d)
        {
            g.FillEllipse(brush, x, y, d, d);
        }
        
        public static Double GetAspectRatio(this Size size)
        {
            return GetAspectRatio(size.Width, size.Height);
        }
        
        public static Double GetAspectRatio(Int32 width, Int32 height)
        {
            return GetAspectRatio(Convert.ToDouble(width), Convert.ToDouble(height));
        }
        
        public static Double GetAspectRatio(Double width, Double height)
        {
            return width / height;
        }
        
        public static Size AspectRatioBoundSize(this Size original, Size bounds)
        {
            Size result = new Size();
            Double diff = GetAspectRatio(original);

            result.Width = bounds.Width;
            result.Height = Convert.ToInt32(GetAspectRatio(Convert.ToDouble(original.Height), diff));

            // The aspect ratios of the original and bounding rectangles are on opposite sides of 1.0.
            // That means the result height is too big to fit in the bounding rectangle.
            // The result needs to be calculated again to make the height fit, and the
            // width needs to be scaled down to maintain the original rectangle's aspect ratio.
            if (result.Height <= bounds.Height)
            {
                return result;
            }

            Double hdiff = GetAspectRatio(result.Height, bounds.Height);
            result.Width = Convert.ToInt32(GetAspectRatio(Convert.ToDouble(bounds.Width), hdiff));
            result.Height = bounds.Height;

            return result;
        }
    }
}