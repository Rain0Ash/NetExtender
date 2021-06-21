// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Drawing;

namespace NetExtender.Utils.Types
{
    public static class SizeUtils
    {
        public static Double GetAspectRatio(this Size size)
        {
            return DrawingUtils.GetAspectRatio(size.Width, size.Height);
        }

        public static Size AspectRatioBoundsSize(this Size original, Size bounds)
        {
            Size result = new Size();
            Double difference = GetAspectRatio(original);

            result.Width = bounds.Width;
            result.Height = (Int32) DrawingUtils.GetAspectRatio(original.Height, difference);

            // The aspect ratios of the original and bounding rectangles are on opposite sides of 1.0.
            // That means the result height is too big to fit in the bounding rectangle.
            // The result needs to be calculated again to make the height fit, and the
            // width needs to be scaled down to maintain the original rectangle's aspect ratio.
            if (result.Height <= bounds.Height)
            {
                return result;
            }

            difference = DrawingUtils.GetAspectRatio(result.Height, bounds.Height);
            result.Width = (Int32) DrawingUtils.GetAspectRatio(bounds.Width, difference);
            result.Height = bounds.Height;

            return result;
        }
    }
}