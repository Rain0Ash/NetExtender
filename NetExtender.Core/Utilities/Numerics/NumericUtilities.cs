// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using NetExtender.Types.Numerics;
using NetExtender.Types.Numerics.Interfaces;

namespace NetExtender.Utilities.Numerics
{
    [SuppressMessage("ReSharper", "StaticMemberInGenericType")]
    public static class NumericUtilities
    {
        private static ImmutableHashSet<Type> Numeric { get; } = ImmutableHashSet.Create
        (
            typeof(Char),
            typeof(SByte),
            typeof(Byte),
            typeof(Int16),
            typeof(UInt16),
            typeof(Int32),
            typeof(UInt32),
            typeof(Int64),
            typeof(UInt64),
            typeof(Half),
            typeof(Single),
            typeof(Double),
            typeof(Decimal),
            typeof(Complex),
            typeof(BigInteger),
            typeof(IntPtr),
            typeof(UIntPtr)
        );

        private static ImmutableHashSet<Type> Signed { get; } = ImmutableHashSet.Create
        (
            typeof(SByte),
            typeof(Int16),
            typeof(Int32),
            typeof(Int64),
            typeof(Half),
            typeof(Single),
            typeof(Double),
            typeof(Decimal),
            typeof(Complex),
            typeof(BigInteger),
            typeof(IntPtr)
        );

        private static ImmutableHashSet<Type> Unsigned { get; } = ImmutableHashSet.Create
        (
            typeof(Char),
            typeof(Byte),
            typeof(UInt16),
            typeof(UInt32),
            typeof(UInt64),
            typeof(UIntPtr)
        );

        private static ImmutableHashSet<Type> Floating { get; } = ImmutableHashSet.Create
        (
            typeof(Half),
            typeof(Single),
            typeof(Double),
            typeof(Decimal),
            typeof(Complex)
        );

        private static ImmutableHashSet<Type> Integer { get; } = ImmutableHashSet.Create
        (
            typeof(Char),
            typeof(SByte),
            typeof(Byte),
            typeof(Int16),
            typeof(UInt16),
            typeof(Int32),
            typeof(UInt32),
            typeof(Int64),
            typeof(UInt64),
            typeof(BigInteger),
            typeof(IntPtr),
            typeof(UIntPtr)
        );

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNumeric<T>() where T : unmanaged
        {
            return Numeric.Contains(typeof(T));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNumeric(this Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return Numeric.Contains(type);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNumeric(this IUnderlyingType type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return IsNumeric(type.UnderlyingType);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsSmall<T>() where T : unmanaged
        {
            unsafe
            {
                return sizeof(T) < sizeof(IntPtr);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsSmall(this Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (!type.IsValueType)
            {
                return false;
            }

            try
            {
                unsafe
                {
                    return Marshal.SizeOf(type) < sizeof(IntPtr);
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsSmall(this IUnderlyingType type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            unsafe
            {
                return type.Size < sizeof(IntPtr);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsSigned<T>() where T : unmanaged
        {
            return Signed.Contains(typeof(T));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsSigned(this Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return Signed.Contains(type);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsSigned(this IUnderlyingType type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return IsSigned(type.UnderlyingType);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsUnsigned<T>() where T : unmanaged
        {
            return Unsigned.Contains(typeof(T));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsUnsigned(this Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return Unsigned.Contains(type);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsUnsigned(this IUnderlyingType type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return IsUnsigned(type.UnderlyingType);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsFloating<T>() where T : unmanaged
        {
            return Floating.Contains(typeof(T));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsFloating(this Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return Floating.Contains(type);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsFloating(this IUnderlyingType type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return IsFloating(type.UnderlyingType);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsInteger<T>() where T : unmanaged
        {
            return Integer.Contains(typeof(T));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsInteger(this Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return Integer.Contains(type);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsInteger(this IUnderlyingType type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return IsInteger(type.UnderlyingType);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GenericNumber ToGenericNumber<T>(this T value) where T : struct, IConvertible
        {
            return new GenericNumber<T>(value);
        }
    }
}