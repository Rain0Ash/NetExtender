// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.CompilerServices;
using NetExtender.Utilities.Numerics;

namespace NetExtender.Types.Numerics
{
    public readonly struct Point3D<T> where T : unmanaged, IConvertible
    {
        public static Point3D<T> operator +(Point3D<T> first, Point3D<T> second)
        {
            return new Point3D<T>(MathUnsafe.Add(first.X, second.X), MathUnsafe.Add(first.Y, second.Y), MathUnsafe.Add(first.Z, second.Z));
        }

        public static Point3D<T> operator -(Point3D<T> first, Point3D<T> second)
        {
            return new Point3D<T>(MathUnsafe.Substract(first.X, second.X), MathUnsafe.Substract(first.Y, second.Y), MathUnsafe.Substract(first.Z, second.Z));
        }

        public static Point3D<T> operator *(Point3D<T> first, Point3D<T> second)
        {
            return new Point3D<T>(MathUnsafe.Multiply(first.X, second.X), MathUnsafe.Multiply(first.Y, second.Y), MathUnsafe.Multiply(first.Z, second.Z));
        }

        public static Point3D<T> operator /(Point3D<T> first, Point3D<T> second)
        {
            return new Point3D<T>(MathUnsafe.Divide(first.X, second.X), MathUnsafe.Divide(first.Y, second.Y), MathUnsafe.Divide(first.Z, second.Z));
        }

        public static Point3D<T> operator %(Point3D<T> first, Point3D<T> second)
        {
            return new Point3D<T>(MathUnsafe.Modulo(first.X, second.X), MathUnsafe.Modulo(first.Y, second.Y), MathUnsafe.Modulo(first.Z, second.Z));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static T ToGeneric(Int32 value)
        {
            return (T) Convert.ChangeType(value, typeof(T));
        }

        public T X { get; }
        public T Y { get; }
        public T Z { get; }

        public static Point3D<T> Zero { get; } = new Point3D<T>(0, 0, 0);
        public static Point3D<T> One { get; } = new Point3D<T>(1, 1, 1);

        public Point3D(T x, T y, T z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        
        public Point3D(Int32 x, Int32 y, Int32 z)
            : this(ToGeneric(x), ToGeneric(y), ToGeneric(z))
        {
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Point3D<T> Offset(Point3D<T> point)
        {
            return this + point;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Point3D<T> Offset(T x, T y, T z)
        {
            return this + new Point3D<T>(x, y, z);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Point3D<T> Delta(Point3D<T> point)
        {
            return this - point;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Point3D<T> Delta(T x, T y, T z)
        {
            return this - new Point3D<T>(x, y, z);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Boolean IsPositive()
        {
            return MathUnsafe.GreaterEqual(X, default) && MathUnsafe.GreaterEqual(Y, default) && MathUnsafe.GreaterEqual(Z, default);
        }
        
        public override String ToString()
        {
            return $"X:{X}, Y:{Y}, Z:{Z}";
        }
    }
}