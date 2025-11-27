// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.CompilerServices;
using NetExtender.Interfaces;
using NetExtender.Types.Exceptions;

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

    public readonly struct Point2<T> : IEquatableStruct<Point2<T>> where T : struct, IEquatable<T>, IComparable<T>, IConvertible
    {
        public static Boolean operator ==(Point2<T> first, Point2<T> second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(Point2<T> first, Point2<T> second)
        {
            return !(first == second);
        }

        public static Point2<T> operator +(Point2<T> first, Point2<T> second)
        {
            return new Point2<T>(INetExtenderAdditionOperators<T>.Addition(first.X, second.X), INetExtenderAdditionOperators<T>.Addition(first.Y, second.Y));
        }

        public static Point2<T> operator -(Point2<T> first, Point2<T> second)
        {
            return new Point2<T>(INetExtenderSubtractionOperators<T>.Subtraction(first.X, second.X), INetExtenderSubtractionOperators<T>.Subtraction(first.Y, second.Y));
        }

        public static Point2<T> operator *(Point2<T> first, Point2<T> second)
        {
            return new Point2<T>(INetExtenderMultiplyOperators<T>.Multiply(first.X, second.X), INetExtenderMultiplyOperators<T>.Multiply(first.Y, second.Y));
        }

        public static Point2<T> operator /(Point2<T> first, Point2<T> second)
        {
            return new Point2<T>(INetExtenderDivisionOperators<T>.Division(first.X, second.X), INetExtenderDivisionOperators<T>.Division(first.Y, second.Y));
        }

        public static Point2<T> operator %(Point2<T> first, Point2<T> second)
        {
            return new Point2<T>(INetExtenderModulusOperators<T>.Modulus(first.X, second.X), INetExtenderModulusOperators<T>.Modulus(first.Y, second.Y));
        }

        internal static readonly T zero = INetExtenderNumberConstantsBase<T>.Zero;
        internal static readonly T one = INetExtenderNumberConstantsBase<T>.One;

        public static Point2<T> Zero
        {
            get
            {
                return new Point2<T>(zero, zero);
            }
        }

        public static Point2<T> One
        {
            get
            {
                return new Point2<T>(one, one);
            }
        }

        public T X { get; }
        public T Y { get; }

        public Boolean IsPositive
        {
            get
            {
                return INetExtenderComparisonOperators<T>.GreaterThanOrEqual(X, zero) && INetExtenderComparisonOperators<T>.GreaterThanOrEqual(Y, zero);
            }
        }

        public Boolean IsNegative
        {
            get
            {
                return INetExtenderComparisonOperators<T>.LessThan(X, zero) && INetExtenderComparisonOperators<T>.LessThan(Y, zero);
            }
        }

        public Boolean IsEmpty
        {
            get
            {
                return INetExtenderEqualityOperators<T>.Equality(X, zero) && INetExtenderEqualityOperators<T>.Equality(Y, zero);
            }
        }

        public Point2(T x, T y)
        {
            X = x;
            Y = y;
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
            return Offset(offset, one);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Point2<T> Offset(PointOffset offset, T count)
        {
            return offset switch
            {
                PointOffset.None => this,
                PointOffset.Up => this - new Point2<T>(zero, count),
                PointOffset.Down => this + new Point2<T>(zero, count),
                PointOffset.Left => this - new Point2<T>(count, zero),
                PointOffset.Right => this + new Point2<T>(count, zero),
                PointOffset.UpLeft => this - new Point2<T>(count, count),
                PointOffset.DownLeft => this + new Point2<T>(zero, count) - new Point2<T>(count, zero),
                PointOffset.UpRight => this - new Point2<T>(zero, count) + new Point2<T>(count, zero),
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
            return X.Equals(other.X) && Y.Equals(other.Y);
        }

        public override String ToString()
        {
            return $"X: {X}, Y: {Y}";
        }
    }

    public readonly struct Point3<T> : IEquatable<Point3<T>> where T : unmanaged, IComparable<T>, IEquatable<T>, IConvertible
    {
        public static Boolean operator ==(Point3<T> first, Point3<T> second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(Point3<T> first, Point3<T> second)
        {
            return !(first == second);
        }

        public static Point3<T> operator +(Point3<T> first, Point3<T> second)
        {
            return new Point3<T>(INetExtenderAdditionOperators<T>.Addition(first.X, second.X), INetExtenderAdditionOperators<T>.Addition(first.Y, second.Y), INetExtenderAdditionOperators<T>.Addition(first.Z, second.Z));
        }

        public static Point3<T> operator -(Point3<T> first, Point3<T> second)
        {
            return new Point3<T>(INetExtenderSubtractionOperators<T>.Subtraction(first.X, second.X), INetExtenderSubtractionOperators<T>.Subtraction(first.Y, second.Y), INetExtenderSubtractionOperators<T>.Subtraction(first.Z, second.Z));
        }

        public static Point3<T> operator *(Point3<T> first, Point3<T> second)
        {
            return new Point3<T>(INetExtenderMultiplyOperators<T>.Multiply(first.X, second.X), INetExtenderMultiplyOperators<T>.Multiply(first.Y, second.Y), INetExtenderMultiplyOperators<T>.Multiply(first.Z, second.Z));
        }

        public static Point3<T> operator /(Point3<T> first, Point3<T> second)
        {
            return new Point3<T>(INetExtenderDivisionOperators<T>.Division(first.X, second.X), INetExtenderDivisionOperators<T>.Division(first.Y, second.Y), INetExtenderDivisionOperators<T>.Division(first.Z, second.Z));
        }

        public static Point3<T> operator %(Point3<T> first, Point3<T> second)
        {
            return new Point3<T>(INetExtenderModulusOperators<T>.Modulus(first.X, second.X), INetExtenderModulusOperators<T>.Modulus(first.Y, second.Y), INetExtenderModulusOperators<T>.Modulus(first.Z, second.Z));
        }

        private static T zero
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Point2<T>.zero;
            }
        }

        private static T one
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Point2<T>.one;
            }
        }

        public static Point3<T> Zero
        {
            get
            {
                return new Point3<T>(zero, zero, zero);
            }
        }

        public static Point3<T> One
        {
            get
            {
                return new Point3<T>(one, one, one);
            }
        }

        public T X { get; }
        public T Y { get; }
        public T Z { get; }

        public Boolean IsPositive
        {
            get
            {
                return INetExtenderComparisonOperators<T>.GreaterThanOrEqual(X, zero) && INetExtenderComparisonOperators<T>.GreaterThanOrEqual(Y, zero) && INetExtenderComparisonOperators<T>.GreaterThanOrEqual(Z, zero);
            }
        }

        public Boolean IsNegative
        {
            get
            {
                return INetExtenderComparisonOperators<T>.LessThan(X, zero) && INetExtenderComparisonOperators<T>.LessThan(Y, zero) && INetExtenderComparisonOperators<T>.LessThan(Z, zero);
            }
        }

        public Boolean IsEmpty
        {
            get
            {
                return INetExtenderEqualityOperators<T>.Equality(X, zero) && INetExtenderEqualityOperators<T>.Equality(Y, zero) && INetExtenderEqualityOperators<T>.Equality(Z, zero);
            }
        }

        public Point3(T x, T y, T z)
        {
            X = x;
            Y = y;
            Z = z;
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
            return X.Equals(other.X) && X.Equals(other.Y) && X.Equals(other.Z);
        }

        public override String ToString()
        {
            return $"X: {X}, Y: {Y}, Z: {Z}";
        }
    }
}