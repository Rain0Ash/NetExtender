// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.CompilerServices;

namespace NetExtender.Utilities.Types
{
    public static class IntPtrUtilities
    {
        public static IntPtr Invalid { get; } = (IntPtr) (-1);

        public static IntPtr Zero
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return IntPtr.Zero;
            }
        }
        
        public static IntPtr Null
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Zero;
            }
        }

        public static IntPtr ToIntPtr(this Int32 value)
        {
            return (IntPtr) value;
        }
        
        public static IntPtr ToIntPtr(this Int64 value)
        {
            // ReSharper disable once RedundantOverflowCheckingContext
            checked
            {
                return (IntPtr) value;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNull(this IntPtr value)
        {
            return value == Null;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotNull(this IntPtr value)
        {
            return !IsNull(value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsInvalid(this IntPtr value)
        {
            return value.ToInt64() < 0;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNullOrInvalid(this IntPtr value)
        {
            return value.ToInt64() <= 0;
        }
    }
}