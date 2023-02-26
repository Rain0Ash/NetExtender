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
        public static Int32 Count(this Rectangle rectangle)
        {
            return checked(rectangle.Width * rectangle.Height);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryCount(this Rectangle rectangle, out Int32 result)
        {
            try
            {
                result = checked(rectangle.Width * rectangle.Height);
                return true;
            }
            catch (OverflowException)
            {
                result = default;
                return false;
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Count(this RectangleF rectangle)
        {
            return checked((Int32) rectangle.Width * (Int32) rectangle.Height);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryCount(this RectangleF rectangle, out Int32 result)
        {
            try
            {
                result = checked((Int32) rectangle.Width * (Int32) rectangle.Height);
                return true;
            }
            catch (OverflowException)
            {
                result = default;
                return false;
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 LongCount(this Rectangle rectangle)
        {
            return checked((Int64) rectangle.Width * rectangle.Height);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryLongCount(this Rectangle rectangle, out Int64 result)
        {
            try
            {
                result = checked((Int64) rectangle.Width * rectangle.Height);
                return true;
            }
            catch (OverflowException)
            {
                result = default;
                return false;
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 LongCount(this RectangleF rectangle)
        {
            return checked((Int64) rectangle.Width * (Int64) rectangle.Height);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryLongCount(this RectangleF rectangle, out Int64 result)
        {
            try
            {
                result = checked((Int64) rectangle.Width * (Int64) rectangle.Height);
                return true;
            }
            catch (OverflowException)
            {
                result = default;
                return false;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle OffsetWith(this Rectangle rectangle, Int32 x, Int32 y)
        {
            rectangle.Offset(x, y);
            return rectangle;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Rectangle OffsetWith(this Rectangle rectangle, Point point)
        {
            rectangle.Offset(point);
            return rectangle;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RectangleF OffsetWith(this RectangleF rectangle, Single x, Single y)
        {
            rectangle.Offset(x, y);
            return rectangle;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RectangleF OffsetWith(this RectangleF rectangle, PointF point)
        {
            rectangle.Offset(point);
            return rectangle;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RectangleEnumerator GetEnumerator(this Rectangle rectangle)
        {
            return GetEnumerator(rectangle, GeometryRotationType.Default);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RectangleEnumerator GetEnumerator(this Rectangle rectangle, GeometryBoundsType bounds)
        {
            return GetEnumerator(rectangle, bounds, GeometryRotationType.Default);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RectangleEnumerator GetEnumerator(this Rectangle rectangle, GeometryRotationType rotation)
        {
            return GetEnumerator(rectangle, GeometryBoundsType.Bound, rotation);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RectangleEnumerator GetEnumerator(this Rectangle rectangle, GeometryBoundsType bounds, GeometryRotationType rotation)
        {
            return new RectangleEnumerator(rectangle, bounds, rotation);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RectangleEnumerator GetEnumerator(this Rectangle rectangle, Size step)
        {
            return GetEnumerator(rectangle, step, GeometryRotationType.Default);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RectangleEnumerator GetEnumerator(this Rectangle rectangle, Size step, GeometryBoundsType bounds)
        {
            return GetEnumerator(rectangle, step, bounds, GeometryRotationType.Default);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RectangleEnumerator GetEnumerator(this Rectangle rectangle, Size step, GeometryRotationType rotation)
        {
            return GetEnumerator(rectangle, step, GeometryBoundsType.Bound, rotation);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RectangleEnumerator GetEnumerator(this Rectangle rectangle, Size step, GeometryBoundsType bounds, GeometryRotationType rotation)
        {
            return new RectangleEnumerator(rectangle, step, bounds, rotation);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RectangleFEnumerator GetEnumerator(this RectangleF rectangle)
        {
            return GetEnumerator(rectangle, GeometryRotationType.Default);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RectangleFEnumerator GetEnumerator(this RectangleF rectangle, GeometryBoundsType bounds)
        {
            return GetEnumerator(rectangle, bounds, GeometryRotationType.Default);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RectangleFEnumerator GetEnumerator(this RectangleF rectangle, GeometryRotationType rotation)
        {
            return GetEnumerator(rectangle, GeometryBoundsType.Bound, rotation);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RectangleFEnumerator GetEnumerator(this RectangleF rectangle, GeometryBoundsType bounds, GeometryRotationType rotation)
        {
            return new RectangleFEnumerator(rectangle, bounds, rotation);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RectangleFEnumerator GetEnumerator(this RectangleF rectangle, SizeF step)
        {
            return GetEnumerator(rectangle, step, GeometryRotationType.Default);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RectangleFEnumerator GetEnumerator(this RectangleF rectangle, SizeF step, GeometryBoundsType bounds)
        {
            return GetEnumerator(rectangle, step, bounds, GeometryRotationType.Default);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RectangleFEnumerator GetEnumerator(this RectangleF rectangle, SizeF step, GeometryRotationType rotation)
        {
            return GetEnumerator(rectangle, step, GeometryBoundsType.Bound, rotation);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RectangleFEnumerator GetEnumerator(this RectangleF rectangle, SizeF step, GeometryBoundsType bounds, GeometryRotationType rotation)
        {
            return new RectangleFEnumerator(rectangle, step, bounds, rotation);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RectangleEnumerator Horizontal(this Rectangle rectangle)
        {
            return Horizontal(rectangle, GeometryBoundsType.Bound);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RectangleEnumerator Horizontal(this Rectangle rectangle, GeometryBoundsType bounds)
        {
            return GetEnumerator(rectangle, bounds, GeometryRotationType.Horizontal);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RectangleEnumerator Horizontal(this Rectangle rectangle, Size step)
        {
            return Horizontal(rectangle, step, GeometryBoundsType.Bound);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RectangleEnumerator Horizontal(this Rectangle rectangle, Size step, GeometryBoundsType bounds)
        {
            return GetEnumerator(rectangle, step, bounds, GeometryRotationType.Horizontal);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RectangleFEnumerator Horizontal(this RectangleF rectangle)
        {
            return Horizontal(rectangle, GeometryBoundsType.Bound);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RectangleFEnumerator Horizontal(this RectangleF rectangle, GeometryBoundsType bounds)
        {
            return GetEnumerator(rectangle, bounds, GeometryRotationType.Horizontal);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RectangleFEnumerator Horizontal(this RectangleF rectangle, SizeF step)
        {
            return Horizontal(rectangle, step, GeometryBoundsType.Bound);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RectangleFEnumerator Horizontal(this RectangleF rectangle, SizeF step, GeometryBoundsType bounds)
        {
            return GetEnumerator(rectangle, step, bounds, GeometryRotationType.Horizontal);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RectangleEnumerator Vertical(this Rectangle rectangle)
        {
            return Vertical(rectangle, GeometryBoundsType.Bound);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RectangleEnumerator Vertical(this Rectangle rectangle, GeometryBoundsType bounds)
        {
            return GetEnumerator(rectangle, bounds, GeometryRotationType.Vertical);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RectangleEnumerator Vertical(this Rectangle rectangle, Size step)
        {
            return Vertical(rectangle, step, GeometryBoundsType.Bound);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RectangleEnumerator Vertical(this Rectangle rectangle, Size step, GeometryBoundsType bounds)
        {
            return GetEnumerator(rectangle, step, bounds, GeometryRotationType.Vertical);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RectangleFEnumerator Vertical(this RectangleF rectangle)
        {
            return Vertical(rectangle, GeometryBoundsType.Bound);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RectangleFEnumerator Vertical(this RectangleF rectangle, GeometryBoundsType bounds)
        {
            return GetEnumerator(rectangle, bounds, GeometryRotationType.Vertical);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RectangleFEnumerator Vertical(this RectangleF rectangle, SizeF step)
        {
            return Vertical(rectangle, step, GeometryBoundsType.Bound);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RectangleFEnumerator Vertical(this RectangleF rectangle, SizeF step, GeometryBoundsType bounds)
        {
            return GetEnumerator(rectangle, step, bounds, GeometryRotationType.Vertical);
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
        public static SizeF AspectRatioBounds(this RectangleF rectangle, SizeF bounds)
        {
            return rectangle.Size.AspectRatioBounds(bounds);
        }
    }
}