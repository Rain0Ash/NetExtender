// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace NetExtender.Utilities.Numerics
{
    public static class MatrixUtilities
    {
        /// <inheritdoc cref="Matrix3x2.Add"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix3x2 Add(this Matrix3x2 first, Matrix3x2 second)
        {
            return Matrix3x2.Add(first, second);
        }
        
        /// <inheritdoc cref="Matrix3x2.Invert"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix3x2 Invert(this Matrix3x2 value)
        {
            return Matrix3x2.Invert(value, out value) ? value : throw new InvalidOperationException("Can't invert matrix.");
        }
        
        /// <inheritdoc cref="Matrix3x2.Invert"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Invert(this Matrix3x2 value, out Matrix3x2 result)
        {
            return Matrix3x2.Invert(value, out result);
        }
        
        /// <inheritdoc cref="Matrix3x2.Lerp"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix3x2 Lerp(this Matrix3x2 first, Matrix3x2 second, Single amount)
        {
            return Matrix3x2.Lerp(first, second, amount);
        }

        /// <inheritdoc cref="Matrix3x2.Multiply(System.Numerics.Matrix3x2,System.Numerics.Matrix3x2)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix3x2 Multiply(this Matrix3x2 first, Matrix3x2 second)
        {
            return Matrix3x2.Multiply(first, second);
        }
        
        /// <inheritdoc cref="Matrix3x2.Multiply(System.Numerics.Matrix3x2,Single)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix3x2 Multiply(this Matrix3x2 value, Single multiplier)
        {
            return Matrix3x2.Multiply(value, multiplier);
        }
        
        /// <inheritdoc cref="Matrix3x2.Negate"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix3x2 Negate(this Matrix3x2 value)
        {
            return Matrix3x2.Negate(value);
        }
        
        /// <inheritdoc cref="Matrix3x2.Subtract"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix3x2 Subtract(this Matrix3x2 first, Matrix3x2 second)
        {
            return Matrix3x2.Subtract(first, second);
        }
        
        /// <inheritdoc cref="Matrix4x4.Add"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix4x4 Add(this Matrix4x4 first, Matrix4x4 second)
        {
            return Matrix4x4.Add(first, second);
        }
        
        /// <inheritdoc cref="Matrix4x4.Invert"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix4x4 Invert(this Matrix4x4 value)
        {
            return Matrix4x4.Invert(value, out value) ? value : throw new InvalidOperationException("Can't invert matrix.");
        }
        
        /// <inheritdoc cref="Matrix4x4.Invert"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Invert(this Matrix4x4 value, out Matrix4x4 result)
        {
            return Matrix4x4.Invert(value, out result);
        }
        
        /// <inheritdoc cref="Matrix4x4.Lerp"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix4x4 Lerp(this Matrix4x4 first, Matrix4x4 second, Single amount)
        {
            return Matrix4x4.Lerp(first, second, amount);
        }

        /// <inheritdoc cref="Matrix4x4.Multiply(System.Numerics.Matrix4x4,System.Numerics.Matrix4x4)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix4x4 Multiply(this Matrix4x4 first, Matrix4x4 second)
        {
            return Matrix4x4.Multiply(first, second);
        }
        
        /// <inheritdoc cref="Matrix4x4.Multiply(System.Numerics.Matrix4x4,Single)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix4x4 Multiply(this Matrix4x4 value, Single multiplier)
        {
            return Matrix4x4.Multiply(value, multiplier);
        }
        
        /// <inheritdoc cref="Matrix4x4.Negate"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix4x4 Negate(this Matrix4x4 value)
        {
            return Matrix4x4.Negate(value);
        }
        
        /// <inheritdoc cref="Matrix4x4.Subtract"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix4x4 Subtract(this Matrix4x4 first, Matrix4x4 second)
        {
            return Matrix4x4.Subtract(first, second);
        }
        
        /// <inheritdoc cref="Matrix4x4.Transform"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix4x4 Transform(this Matrix4x4 value, Quaternion rotation)
        {
            return Matrix4x4.Transform(value, rotation);
        }
        
        /// <inheritdoc cref="Matrix4x4.Transpose"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix4x4 Transpose(this Matrix4x4 value)
        {
            return Matrix4x4.Transpose(value);
        }
        
        /// <inheritdoc cref="Plane.Dot"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Dot(this Plane value, Vector4 vector)
        {
            return Plane.Dot(value, vector);
        }
        
        /// <inheritdoc cref="Plane.DotCoordinate"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single DotCoordinate(this Plane value, Vector3 vector)
        {
            return Plane.DotCoordinate(value, vector);
        }
        
        /// <inheritdoc cref="Plane.DotNormal"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single DotNormal(this Plane value, Vector3 vector)
        {
            return Plane.DotNormal(value, vector);
        }
        
        /// <inheritdoc cref="Plane.Normalize"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Plane Normalize(this Plane value)
        {
            return Plane.Normalize(value);
        }
        
        /// <inheritdoc cref="Plane.Transform(System.Numerics.Plane,System.Numerics.Matrix4x4)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Plane Transform(this Plane value, Matrix4x4 matrix)
        {
            return Plane.Transform(value, matrix);
        }
        
        /// <inheritdoc cref="Plane.Transform(System.Numerics.Plane,System.Numerics.Quaternion)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Plane Transform(this Plane value, Quaternion rotation)
        {
            return Plane.Transform(value, rotation);
        }
    }
}