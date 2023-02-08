// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using NetExtender.Types.Exceptions;
using NetExtender.Types.Numerics;

namespace NetExtender.Utilities.Numerics
{
    //TODO: offset by mutation
    public static class PointUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Distance(this Point first, Point second)
        {
            return MathUtilities.Distance(first.X, first.Y, second.X, second.Y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Distance(this PointF first, PointF second)
        {
            return MathUtilities.Distance(first.X, first.Y, second.X, second.Y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point WithOffset(this PointOffset offset, Point value, Int32 count)
        {
            return WithOffset(value, offset, count);
        }

        public static Point WithOffset(this Point value, PointOffset offset, Int32 count)
        {
            return offset switch
            {
                PointOffset.None => value,
                PointOffset.Up => new Point(value.X, value.Y - count),
                PointOffset.Down => new Point(value.X, value.Y + count),
                PointOffset.Left => new Point(value.X - count, value.Y),
                PointOffset.Right => new Point(value.X + count, value.Y),
                PointOffset.UpLeft => new Point(value.X - count, value.Y - count),
                PointOffset.DownLeft => new Point(value.X - count, value.Y + count),
                PointOffset.UpRight => new Point(value.X + count, value.Y - count),
                PointOffset.DownRight => new Point(value.X + count, value.Y + count),
                _ => throw new EnumUndefinedOrNotSupportedException<PointOffset>(offset, nameof(offset), null)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Point WithOffset(this PointOffset offset, Point value, Size size)
        {
            return WithOffset(value, offset, size);
        }

        public static Point WithOffset(this Point value, PointOffset offset, Size size)
        {
            return offset switch
            {
                PointOffset.None => value,
                PointOffset.Up => new Point(value.X, value.Y - size.Height),
                PointOffset.Down => new Point(value.X, value.Y + size.Height),
                PointOffset.Left => new Point(value.X - size.Width, value.Y),
                PointOffset.Right => new Point(value.X + size.Width, value.Y),
                PointOffset.UpLeft => new Point(value.X - size.Width, value.Y - size.Height),
                PointOffset.DownLeft => new Point(value.X - size.Width, value.Y + size.Height),
                PointOffset.UpRight => new Point(value.X + size.Width, value.Y - size.Height),
                PointOffset.DownRight => new Point(value.X + size.Width, value.Y + size.Height),
                _ => throw new EnumUndefinedOrNotSupportedException<PointOffset>(offset, nameof(offset), null)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointF WithOffset(this PointOffset offset, PointF value, Int32 count)
        {
            return WithOffset(value, offset, count);
        }

        public static PointF WithOffset(this PointF value, PointOffset offset, Single count)
        {
            return offset switch
            {
                PointOffset.None => value,
                PointOffset.Up => new PointF(value.X, value.Y - count),
                PointOffset.Down => new PointF(value.X, value.Y + count),
                PointOffset.Left => new PointF(value.X - count, value.Y),
                PointOffset.Right => new PointF(value.X + count, value.Y),
                PointOffset.UpLeft => new PointF(value.X - count, value.Y - count),
                PointOffset.DownLeft => new PointF(value.X - count, value.Y + count),
                PointOffset.UpRight => new PointF(value.X + count, value.Y - count),
                PointOffset.DownRight => new PointF(value.X + count, value.Y + count),
                _ => throw new EnumUndefinedOrNotSupportedException<PointOffset>(offset, nameof(offset), null)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PointF WithOffset(this PointOffset offset, PointF value, SizeF size)
        {
            return WithOffset(value, offset, size);
        }

        public static PointF WithOffset(this PointF value, PointOffset offset, SizeF size)
        {
            return offset switch
            {
                PointOffset.None => value,
                PointOffset.Up => new PointF(value.X, value.Y - size.Height),
                PointOffset.Down => new PointF(value.X, value.Y + size.Height),
                PointOffset.Left => new PointF(value.X - size.Width, value.Y),
                PointOffset.Right => new PointF(value.X + size.Width, value.Y),
                PointOffset.UpLeft => new PointF(value.X - size.Width, value.Y - size.Height),
                PointOffset.DownLeft => new PointF(value.X - size.Width, value.Y + size.Height),
                PointOffset.UpRight => new PointF(value.X + size.Width, value.Y - size.Height),
                PointOffset.DownRight => new PointF(value.X + size.Width, value.Y + size.Height),
                _ => throw new EnumUndefinedOrNotSupportedException<PointOffset>(offset, nameof(offset), null)
            };
        }

        public static PointOffset Invert(this PointOffset offset)
        {
            return offset switch
            {
                PointOffset.None => PointOffset.None,
                PointOffset.Up => PointOffset.Down,
                PointOffset.Down => PointOffset.Up,
                PointOffset.Left => PointOffset.Right,
                PointOffset.Right => PointOffset.Left,
                PointOffset.UpLeft => PointOffset.DownRight,
                PointOffset.DownLeft => PointOffset.UpRight,
                PointOffset.UpRight => PointOffset.DownLeft,
                PointOffset.DownRight => PointOffset.UpLeft,
                _ => throw new EnumUndefinedOrNotSupportedException<PointOffset>(offset, nameof(offset), null)
            };
        }
    }
}