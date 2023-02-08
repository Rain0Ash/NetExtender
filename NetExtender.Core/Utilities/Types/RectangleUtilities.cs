// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using NetExtender.Types.Geometry;

namespace NetExtender.Utilities.Types
{
    public static class RectangleUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static HorizontalRectangle.Enumerator GetEnumerator(this Rectangle rectangle)
        {
            return new HorizontalRectangle.Enumerator(rectangle);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static HorizontalRectangleF.Enumerator GetEnumerator(this RectangleF rectangle)
        {
            return new HorizontalRectangleF.Enumerator(rectangle);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static HorizontalRectangle Horizontal(this Rectangle rectangle)
        {
            return rectangle;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static HorizontalRectangleF Horizontal(this RectangleF rectangle)
        {
            return rectangle;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static HorizontalRectangle Horizontal(this VerticalRectangle rectangle)
        {
            return rectangle.Rectangle;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static HorizontalRectangleF Horizontal(this VerticalRectangleF rectangle)
        {
            return rectangle.Rectangle;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VerticalRectangle Vertical(this Rectangle rectangle)
        {
            return rectangle;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VerticalRectangleF Vertical(this RectangleF rectangle)
        {
            return rectangle;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VerticalRectangle Vertical(this HorizontalRectangle rectangle)
        {
            return rectangle.Rectangle;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VerticalRectangleF Vertical(this HorizontalRectangleF rectangle)
        {
            return rectangle.Rectangle;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double GetAspectRatio(this Rectangle rectangle)
        {
            return rectangle.Size.GetAspectRatio();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double GetAspectRatio(this RectangleF rectangle)
        {
            return rectangle.Size.GetAspectRatio();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Size AspectRatioBounds(this Rectangle rectangle, Size bounds)
        {
            return rectangle.Size.AspectRatioBounds(bounds);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static HorizontalSize AspectRatioBounds(this HorizontalRectangle rectangle, Size bounds)
        {
            return rectangle.Size.AspectRatioBounds(bounds);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VerticalSize AspectRatioBounds(this VerticalRectangle rectangle, Size bounds)
        {
            return rectangle.Size.AspectRatioBounds(bounds);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SizeF AspectRatioBounds(this RectangleF rectangle, SizeF bounds)
        {
            return rectangle.Size.AspectRatioBounds(bounds);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static HorizontalSizeF AspectRatioBounds(this HorizontalRectangleF rectangle, SizeF bounds)
        {
            return rectangle.Size.AspectRatioBounds(bounds);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VerticalSizeF AspectRatioBounds(this VerticalRectangleF rectangle, SizeF bounds)
        {
            return rectangle.Size.AspectRatioBounds(bounds);
        }
    }
}