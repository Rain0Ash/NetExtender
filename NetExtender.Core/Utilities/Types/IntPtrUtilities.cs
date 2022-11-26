// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace NetExtender.Utilities.Types
{
    [SuppressMessage("ReSharper", "RedundantOverflowCheckingContext")]
    public static class IntPtrUtilities
    {
        public static IntPtr Invalid { get; } = (IntPtr) (-1);

        public static IntPtr ToIntPtr(this Int32 value)
        {
            return checked((IntPtr) value);
        }

        public static UIntPtr ToUIntPtr(this UInt32 value)
        {
            return checked((UIntPtr) value);
        }

        public static IntPtr ToIntPtr(this Int64 value)
        {
            return checked((IntPtr) value);
        }

        public static IntPtr ToIntPtr(this Int64 value, out Boolean overflow)
        {
            overflow = value > (Int64) IntPtr.MaxValue;
            return unchecked((IntPtr) value);
        }

        public static UIntPtr ToUIntPtr(this UInt64 value)
        {
            return checked((UIntPtr) value);
        }

        public static UIntPtr ToUIntPtr(this UInt64 value, out Boolean overflow)
        {
            overflow = value > (UInt64) IntPtr.MaxValue;
            return unchecked((UIntPtr) value);
        }

        public static Int16 ToInt16(this IntPtr value)
        {
            return checked((Int16) value);
        }

        public static Int16 ToInt16(this IntPtr value, out Boolean overflow)
        {
            Int64 result = (Int64) value;
            overflow = result > Int16.MaxValue || result < Int16.MinValue;
            return unchecked((Int16) result);
        }

        public static UInt16 ToUInt16(this UIntPtr value)
        {
            return checked((UInt16) value);
        }

        public static UInt16 ToUInt16(this UIntPtr value, out Boolean overflow)
        {
            UInt64 result = (UInt64) value;
            overflow = result > UInt16.MaxValue;
            return unchecked((UInt16) result);
        }

        public static Int32 ToInt32(this IntPtr value, out Boolean overflow)
        {
            Int64 result = (Int64) value;
            overflow = result > Int32.MaxValue || result < Int32.MinValue;
            return unchecked((Int32) result);
        }

        public static UInt32 ToUInt32(this UIntPtr value, out Boolean overflow)
        {
            UInt64 result = (UInt64) value;
            overflow = result > UInt32.MaxValue;
            return unchecked((UInt32) result);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNull(this IntPtr value)
        {
            return value == IntPtr.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNull(this UIntPtr value)
        {
            return value == UIntPtr.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotNull(this IntPtr value)
        {
            return !IsNull(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotNull(this UIntPtr value)
        {
            return !IsNull(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsInvalid(this IntPtr value)
        {
            return value.ToInt64() < 0;
        }

        // ReSharper disable once UnusedParameter.Global
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsInvalid(this UIntPtr value)
        {
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNullOrInvalid(this IntPtr value)
        {
            return value.ToInt64() <= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNullOrInvalid(this UIntPtr value)
        {
            return value.ToUInt64() <= 0;
        }
    }
}