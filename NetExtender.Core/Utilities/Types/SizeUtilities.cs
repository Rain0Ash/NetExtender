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
        public static Int32 Count(this Size size)
        {
            return checked(size.Width * size.Height);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryCount(this Size size, out Int32 result)
        {
            try
            {
                result = checked(size.Width * size.Height);
                return true;
            }
            catch (OverflowException)
            {
                result = default;
                return false;
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Count(this SizeF size)
        {
            return checked((Int32) size.Width * (Int32) size.Height);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryCount(this SizeF size, out Int32 result)
        {
            try
            {
                result = checked((Int32) size.Width * (Int32) size.Height);
                return true;
            }
            catch (OverflowException)
            {
                result = default;
                return false;
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 LongCount(this Size size)
        {
            return checked((Int64) size.Width * size.Height);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryLongCount(this Size size, out Int64 result)
        {
            try
            {
                result = checked((Int64) size.Width * size.Height);
                return true;
            }
            catch (OverflowException)
            {
                result = default;
                return false;
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 LongCount(this SizeF size)
        {
            return checked((Int64) size.Width * (Int64) size.Height);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryLongCount(this SizeF size, out Int64 result)
        {
            try
            {
                result = checked((Int64) size.Width * (Int64) size.Height);
                return true;
            }
            catch (OverflowException)
            {
                result = default;
                return false;
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SizeEnumerator GetEnumerator(this Size size)
        {
            return GetEnumerator(size, GeometryRotationType.Default);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SizeEnumerator GetEnumerator(this Size size, GeometryBoundsType bounds)
        {
            return GetEnumerator(size, bounds, GeometryRotationType.Default);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SizeEnumerator GetEnumerator(this Size size, GeometryRotationType rotation)
        {
            return GetEnumerator(size, GeometryBoundsType.Bound, rotation);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SizeEnumerator GetEnumerator(this Size size, GeometryBoundsType bounds, GeometryRotationType rotation)
        {
            return new SizeEnumerator(size, bounds, rotation);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SizeEnumerator GetEnumerator(this Size size, Size step)
        {
            return GetEnumerator(size, step, GeometryRotationType.Default);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SizeEnumerator GetEnumerator(this Size size, Size step, GeometryBoundsType bounds)
        {
            return GetEnumerator(size, step, bounds, GeometryRotationType.Default);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SizeEnumerator GetEnumerator(this Size size, Size step, GeometryRotationType rotation)
        {
            return GetEnumerator(size, step, GeometryBoundsType.Bound, rotation);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SizeEnumerator GetEnumerator(this Size size, Size step, GeometryBoundsType bounds, GeometryRotationType rotation)
        {
            return new SizeEnumerator(size, step, bounds, rotation);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SizeFEnumerator GetEnumerator(this SizeF size)
        {
            return GetEnumerator(size, GeometryRotationType.Default);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SizeFEnumerator GetEnumerator(this SizeF size, GeometryBoundsType bounds)
        {
            return GetEnumerator(size, bounds, GeometryRotationType.Default);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SizeFEnumerator GetEnumerator(this SizeF size, GeometryRotationType rotation)
        {
            return GetEnumerator(size, GeometryBoundsType.Bound, rotation);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SizeFEnumerator GetEnumerator(this SizeF size, GeometryBoundsType bounds, GeometryRotationType rotation)
        {
            return new SizeFEnumerator(size, bounds, rotation);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SizeFEnumerator GetEnumerator(this SizeF size, SizeF step)
        {
            return GetEnumerator(size, step, GeometryRotationType.Default);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SizeFEnumerator GetEnumerator(this SizeF size, SizeF step, GeometryBoundsType bounds)
        {
            return GetEnumerator(size, step, bounds, GeometryRotationType.Default);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SizeFEnumerator GetEnumerator(this SizeF size, SizeF step, GeometryRotationType rotation)
        {
            return GetEnumerator(size, step, GeometryBoundsType.Bound, rotation);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SizeFEnumerator GetEnumerator(this SizeF size, SizeF step, GeometryBoundsType bounds, GeometryRotationType rotation)
        {
            return new SizeFEnumerator(size, step, bounds, rotation);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SizeEnumerator Horizontal(this Size size)
        {
            return Horizontal(size, GeometryBoundsType.Bound);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SizeEnumerator Horizontal(this Size size, GeometryBoundsType bounds)
        {
            return GetEnumerator(size, bounds, GeometryRotationType.Horizontal);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SizeEnumerator Horizontal(this Size size, Size step)
        {
            return Horizontal(size, step, GeometryBoundsType.Bound);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SizeEnumerator Horizontal(this Size size, Size step, GeometryBoundsType bounds)
        {
            return GetEnumerator(size, step, bounds, GeometryRotationType.Horizontal);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SizeFEnumerator Horizontal(this SizeF size)
        {
            return Horizontal(size, GeometryBoundsType.Bound);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SizeFEnumerator Horizontal(this SizeF size, GeometryBoundsType bounds)
        {
            return GetEnumerator(size, bounds, GeometryRotationType.Horizontal);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SizeFEnumerator Horizontal(this SizeF size, SizeF step)
        {
            return Horizontal(size, step, GeometryBoundsType.Bound);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SizeFEnumerator Horizontal(this SizeF size, SizeF step, GeometryBoundsType bounds)
        {
            return GetEnumerator(size, step, bounds, GeometryRotationType.Horizontal);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SizeEnumerator Vertical(this Size size)
        {
            return Vertical(size, GeometryBoundsType.Bound);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SizeEnumerator Vertical(this Size size, GeometryBoundsType bounds)
        {
            return GetEnumerator(size, bounds, GeometryRotationType.Vertical);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SizeEnumerator Vertical(this Size size, Size step)
        {
            return Vertical(size, step, GeometryBoundsType.Bound);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SizeEnumerator Vertical(this Size size, Size step, GeometryBoundsType bounds)
        {
            return GetEnumerator(size, step, bounds, GeometryRotationType.Vertical);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SizeFEnumerator Vertical(this SizeF size)
        {
            return Vertical(size, GeometryBoundsType.Bound);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SizeFEnumerator Vertical(this SizeF size, GeometryBoundsType bounds)
        {
            return GetEnumerator(size, bounds, GeometryRotationType.Vertical);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SizeFEnumerator Vertical(this SizeF size, SizeF step)
        {
            return Vertical(size, step, GeometryBoundsType.Bound);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SizeFEnumerator Vertical(this SizeF size, SizeF step, GeometryBoundsType bounds)
        {
            return GetEnumerator(size, step, bounds, GeometryRotationType.Vertical);
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
    }
}