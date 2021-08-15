// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.CompilerServices;
using NetExtender.Utilities.Numerics;

namespace NetExtender.Types.Numerics
{
    [Flags]
    public enum PointOffset
    {
        None = 0,
        Up = 1,
        Down = 3,
        Left = 4,
        Right = 12,
        
        UpLeft = 5,
        DownLeft = 7,
        UpRight = 13,
        DownRight = 15
    }

    public readonly struct Point<T> where T : unmanaged, IConvertible
    {
        public static Point<T> operator +(Point<T> first, Point<T> second)
        {
            return new Point<T>(MathUnsafe.Add(first.X, second.X), MathUnsafe.Add(first.Y, second.Y));
        }

        public static Point<T> operator -(Point<T> first, Point<T> second)
        {
            return new Point<T>(MathUnsafe.Substract(first.X, second.X), MathUnsafe.Substract(first.Y, second.Y));
        }

        public static Point<T> operator *(Point<T> first, Point<T> second)
        {
            return new Point<T>(MathUnsafe.Multiply(first.X, second.X), MathUnsafe.Multiply(first.Y, second.Y));
        }

        public static Point<T> operator /(Point<T> first, Point<T> second)
        {
            return new Point<T>(MathUnsafe.Divide(first.X, second.X), MathUnsafe.Divide(first.Y, second.Y));
        }

        public static Point<T> operator %(Point<T> first, Point<T> second)
        {
            return new Point<T>(MathUnsafe.Modulo(first.X, second.X), MathUnsafe.Modulo(first.Y, second.Y));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static T ToGeneric(Int32 value)
        {
            return (T) Convert.ChangeType(value, typeof(T));
        }

        public T X { get; }
        public T Y { get; }

        public static Point<T> Zero { get; } = new Point<T>(0, 0);
        public static Point<T> One { get; } = new Point<T>(1, 1);

        public Point(T x, T y)
        {
            X = x;
            Y = y;
        }

        public Point(T x, Int32 y)
            : this(x, ToGeneric(y))
        {
        }

        public Point(Int32 x, T y)
            : this(ToGeneric(x), y)
        {
        }

        public Point(Int32 x, Int32 y)
            : this(ToGeneric(x), ToGeneric(y))
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Point<T> Offset(Point<T> point)
        {
            return this + point;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Point<T> Offset(T x, T y)
        {
            return this + new Point<T>(x, y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Point<T> Offset(PointOffset offset)
        {
            return Offset(offset, (T) Convert.ChangeType(1, typeof(T)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Point<T> Offset(PointOffset offset, T count)
        {
            return offset switch
            {
                PointOffset.None => this,
                PointOffset.Up => this - new Point<T>(0, count),
                PointOffset.Down => this + new Point<T>(0, count),
                PointOffset.Left => this - new Point<T>(count, 0),
                PointOffset.Right => this + new Point<T>(count, 0),
                PointOffset.UpLeft => this - new Point<T>(count, count),
                PointOffset.DownLeft => this + new Point<T>(MathUnsafe.Negative(count), count),
                PointOffset.UpRight => this - new Point<T>(MathUnsafe.Negative(count), count),
                PointOffset.DownRight => this + new Point<T>(count, count),
                _ => throw new ArgumentOutOfRangeException(nameof(offset), offset, null)
            };
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Point<T> Delta(Point<T> point)
        {
            return this - point;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Point<T> Delta(T x, T y)
        {
            return this - new Point<T>(x, y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean IsPositive()
        {
            return MathUnsafe.GreaterEqual(X, default) && MathUnsafe.GreaterEqual(Y, default);
        }

        public override String ToString()
        {
            return $"X:{X}, Y:{Y}";
        }
    }
}