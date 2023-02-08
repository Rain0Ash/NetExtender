// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using NetExtender.Types.Geometry;

namespace NetExtender.Utilities.Types
{
    public static class SizeUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static HorizontalSize.Enumerator GetEnumerator(this Size size)
        {
            return new HorizontalSize.Enumerator(size);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static HorizontalSizeF.Enumerator GetEnumerator(this SizeF size)
        {
            return new HorizontalSizeF.Enumerator(size);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static HorizontalSize Horizontal(this Size size)
        {
            return size;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static HorizontalSizeF Horizontal(this SizeF size)
        {
            return size;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static HorizontalSize Horizontal(this VerticalSize size)
        {
            return size.Size;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static HorizontalSizeF Horizontal(this VerticalSizeF size)
        {
            return size.Size;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VerticalSize Vertical(this Size size)
        {
            return size;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VerticalSizeF Vertical(this SizeF size)
        {
            return size;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VerticalSize Vertical(this HorizontalSize size)
        {
            return size.Size;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VerticalSizeF Vertical(this HorizontalSizeF size)
        {
            return size.Size;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double GetAspectRatio(this Size size)
        {
            return DrawingUtilities.GetAspectRatio(size.Width, size.Height);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double GetAspectRatio(this SizeF size)
        {
            return DrawingUtilities.GetAspectRatio(size.Width, size.Height);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static Size AspectRatioBounds(this Size size, Size bounds)
        {
            Double difference = GetAspectRatio(size);
            
            Int32 height = (Int32) DrawingUtilities.GetAspectRatio(size.Height, difference);

            if (height <= bounds.Height)
            {
                return new Size(bounds.Width, height);
            }

            difference = DrawingUtilities.GetAspectRatio(height, bounds.Height);
            
            Int32 width = (Int32) DrawingUtilities.GetAspectRatio(bounds.Width, difference);
            return new Size(width, bounds.Height);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static HorizontalSize AspectRatioBounds(this HorizontalSize size, Size bounds)
        {
            return AspectRatioBounds(size.Size, bounds);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VerticalSize AspectRatioBounds(this VerticalSize size, Size bounds)
        {
            return AspectRatioBounds(size.Size, bounds);
        }

        [MethodImpl(MethodImplOptions.AggressiveOptimization)]
        public static SizeF AspectRatioBounds(this SizeF size, SizeF bounds)
        {
            Double difference = GetAspectRatio(size);

            Single height = (Single) DrawingUtilities.GetAspectRatio(size.Height, difference);

            if (height <= bounds.Height)
            {
                return new SizeF(bounds.Width, height);
            }

            difference = DrawingUtilities.GetAspectRatio(height, bounds.Height);
            
            Single width = (Single) DrawingUtilities.GetAspectRatio(bounds.Width, difference);
            return new SizeF(width, bounds.Height);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static HorizontalSizeF AspectRatioBounds(this HorizontalSizeF size, SizeF bounds)
        {
            return AspectRatioBounds(size.Size, bounds);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VerticalSizeF AspectRatioBounds(this VerticalSizeF size, SizeF bounds)
        {
            return AspectRatioBounds(size.Size, bounds);
        }
    }
}