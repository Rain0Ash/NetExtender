// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.CompilerServices;
using NetExtender.Types.Exceptions;
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

        UpLeft = Up | Left,
        DownLeft = Down | Left,
        UpRight = Up | Right,
        DownRight = Down | Right
    }

    public readonly struct Point2<T> : IEquatable<Point2<T>> where T : unmanaged, IEquatable<T>, IComparable<T>, IConvertible
    {
        public static Boolean operator ==(Point2<T> fisrt, Point2<T> second)
        {
            return fisrt.Equals(second);
        }

        public static Boolean operator !=(Point2<T> first, Point2<T> second)
        {
            return !(first == second);
        }

        public static Point2<T> operator +(Point2<T> first, Point2<T> second)
        {
            return new Point2<T>(MathUnsafe.Add(first.X, second.X), MathUnsafe.Add(first.Y, second.Y));
        }

        public static Point2<T> operator -(Point2<T> first, Point2<T> second)
        {
            return new Point2<T>(MathUnsafe.Subtract(first.X, second.X), MathUnsafe.Subtract(first.Y, second.Y));
        }

        public static Point2<T> operator *(Point2<T> first, Point2<T> second)
        {
            return new Point2<T>(MathUnsafe.Multiply(first.X, second.X), MathUnsafe.Multiply(first.Y, second.Y));
        }

        public static Point2<T> operator /(Point2<T> first, Point2<T> second)
        {
            return new Point2<T>(MathUnsafe.Divide(first.X, second.X), MathUnsafe.Divide(first.Y, second.Y));
        }

        public static Point2<T> operator %(Point2<T> first, Point2<T> second)
        {
            return new Point2<T>(MathUnsafe.Modulo(first.X, second.X), MathUnsafe.Modulo(first.Y, second.Y));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static T ToGeneric(Int32 value)
        {
            return (T) Convert.ChangeType(value, typeof(T));
        }

        public T X { get; }
        public T Y { get; }

        public static Point2<T> Zero { get; } = new Point2<T>(0, 0);
        public static Point2<T> One { get; } = new Point2<T>(1, 1);

        public Point2(T x, T y)
        {
            X = x;
            Y = y;
        }

        public Point2(T x, Int32 y)
            : this(x, ToGeneric(y))
        {
        }

        public Point2(Int32 x, T y)
            : this(ToGeneric(x), y)
        {
        }

        public Point2(Int32 x, Int32 y)
            : this(ToGeneric(x), ToGeneric(y))
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Point2<T> Offset(Point2<T> point)
        {
            return this + point;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Point2<T> Offset(T x, T y)
        {
            return this + new Point2<T>(x, y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Point2<T> Offset(PointOffset offset)
        {
            return Offset(offset, (T) Convert.ChangeType(1, typeof(T)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Point2<T> Offset(PointOffset offset, T count)
        {
            return offset switch
            {
                PointOffset.None => this,
                PointOffset.Up => this - new Point2<T>(0, count),
                PointOffset.Down => this + new Point2<T>(0, count),
                PointOffset.Left => this - new Point2<T>(count, 0),
                PointOffset.Right => this + new Point2<T>(count, 0),
                PointOffset.UpLeft => this - new Point2<T>(count, count),
                PointOffset.DownLeft => this + new Point2<T>(MathUnsafe.Negative(count), count),
                PointOffset.UpRight => this - new Point2<T>(MathUnsafe.Negative(count), count),
                PointOffset.DownRight => this + new Point2<T>(count, count),
                _ => throw new EnumUndefinedOrNotSupportedException<PointOffset>(offset, nameof(offset), null)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Point2<T> Delta(Point2<T> point)
        {
            return this - point;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Point2<T> Delta(T x, T y)
        {
            return this - new Point2<T>(x, y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean IsPositive()
        {
            return MathUnsafe.GreaterEqual(X, default) && MathUnsafe.GreaterEqual(Y, default);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean IsNegative()
        {
            return MathUnsafe.LessEqual(X, default) && MathUnsafe.LessEqual(Y, default);
        }

        public override Int32 GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        public override Boolean Equals(Object? other)
        {
            return other is Point2<T> point && Equals(point);
        }

        public Boolean Equals(Point2<T> other)
        {
            return MathUnsafe.Equal(X, other.X) && MathUnsafe.Equal(Y, other.Y);
        }

        public override String ToString()
        {
            return $"X:{X}, Y:{Y}";
        }
    }

    public readonly struct Point3<T> : IEquatable<Point3<T>> where T : unmanaged, IEquatable<T>, IComparable<T>, IConvertible
    {
        public static Boolean operator ==(Point3<T> fisrt, Point3<T> second)
        {
            return fisrt.Equals(second);
        }

        public static Boolean operator !=(Point3<T> first, Point3<T> second)
        {
            return !(first == second);
        }

        public static Point3<T> operator +(Point3<T> first, Point3<T> second)
        {
            return new Point3<T>(MathUnsafe.Add(first.X, second.X), MathUnsafe.Add(first.Y, second.Y), MathUnsafe.Add(first.Z, second.Z));
        }

        public static Point3<T> operator -(Point3<T> first, Point3<T> second)
        {
            return new Point3<T>(MathUnsafe.Subtract(first.X, second.X), MathUnsafe.Subtract(first.Y, second.Y), MathUnsafe.Subtract(first.Z, second.Z));
        }

        public static Point3<T> operator *(Point3<T> first, Point3<T> second)
        {
            return new Point3<T>(MathUnsafe.Multiply(first.X, second.X), MathUnsafe.Multiply(first.Y, second.Y), MathUnsafe.Multiply(first.Z, second.Z));
        }

        public static Point3<T> operator /(Point3<T> first, Point3<T> second)
        {
            return new Point3<T>(MathUnsafe.Divide(first.X, second.X), MathUnsafe.Divide(first.Y, second.Y), MathUnsafe.Divide(first.Z, second.Z));
        }

        public static Point3<T> operator %(Point3<T> first, Point3<T> second)
        {
            return new Point3<T>(MathUnsafe.Modulo(first.X, second.X), MathUnsafe.Modulo(first.Y, second.Y), MathUnsafe.Modulo(first.Z, second.Z));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static T ToGeneric(Int32 value)
        {
            return (T) Convert.ChangeType(value, typeof(T));
        }

        public T X { get; }
        public T Y { get; }
        public T Z { get; }

        public static Point3<T> Zero { get; } = new Point3<T>(0, 0, 0);
        public static Point3<T> One { get; } = new Point3<T>(1, 1, 1);

        public Point3(T x, T y, T z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Point3(Int32 x, Int32 y, Int32 z)
            : this(ToGeneric(x), ToGeneric(y), ToGeneric(z))
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Point3<T> Offset(Point3<T> point)
        {
            return this + point;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Point3<T> Offset(T x, T y, T z)
        {
            return this + new Point3<T>(x, y, z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Point3<T> Delta(Point3<T> point)
        {
            return this - point;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Point3<T> Delta(T x, T y, T z)
        {
            return this - new Point3<T>(x, y, z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean IsPositive()
        {
            return MathUnsafe.GreaterEqual(X, default) && MathUnsafe.GreaterEqual(Y, default) && MathUnsafe.GreaterEqual(Z, default);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean IsNegative()
        {
            return MathUnsafe.LessEqual(X, default) && MathUnsafe.LessEqual(Y, default) && MathUnsafe.LessEqual(Z, default);
        }

        public override Int32 GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        public override Boolean Equals(Object? other)
        {
            return other is Point3<T> point && Equals(point);
        }

        public Boolean Equals(Point3<T> other)
        {
            return MathUnsafe.Equal(X, other.X) && MathUnsafe.Equal(Y, other.Y) && MathUnsafe.Equal(Z, other.Z);
        }

        public override String ToString()
        {
            return $"X:{X}, Y:{Y}, Z:{Z}";
        }
    }
}