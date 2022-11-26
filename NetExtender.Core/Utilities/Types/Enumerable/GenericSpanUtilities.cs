// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Runtime.CompilerServices;
using NetExtender.Utilities.Numerics;

namespace NetExtender.Utilities.Types
{
    [SuppressMessage("ReSharper", "RedundantOverflowCheckingContext")]
    public static partial class SpanUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SByte Sum(this Memory<SByte> source)
        {
            return Sum(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SByte Sum(this Span<SByte> source)
        {
            return Sum((ReadOnlySpan<SByte>) source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SByte Sum(this ReadOnlyMemory<SByte> source)
        {
            return Sum(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SByte Sum(this ReadOnlySpan<SByte> source)
        {
            checked
            {
                return source.Aggregate<SByte, SByte>(0, (current, value) => (SByte) (current + value));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SByte Sum(this Memory<SByte> source, SByte overflow)
        {
            return Sum(source.Span, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SByte Sum(this Span<SByte> source, SByte overflow)
        {
            return Sum((ReadOnlySpan<SByte>) source, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SByte Sum(this ReadOnlyMemory<SByte> source, SByte overflow)
        {
            return Sum(source.Span, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SByte Sum(this ReadOnlySpan<SByte> source, SByte overflow)
        {
            try
            {
                return source.Sum();
            }
            catch (OverflowException)
            {
                return overflow;
            }
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Byte Sum(this Memory<Byte> source)
        {
            return Sum(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Byte Sum(this Span<Byte> source)
        {
            return Sum((ReadOnlySpan<Byte>) source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Byte Sum(this ReadOnlyMemory<Byte> source)
        {
            return Sum(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Byte Sum(this ReadOnlySpan<Byte> source)
        {
            checked
            {
                return source.Aggregate<Byte, Byte>(0, (current, value) => (Byte) (current + value));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Byte Sum(this Memory<Byte> source, Byte overflow)
        {
            return Sum(source.Span, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Byte Sum(this Span<Byte> source, Byte overflow)
        {
            return Sum((ReadOnlySpan<Byte>) source, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Byte Sum(this ReadOnlyMemory<Byte> source, Byte overflow)
        {
            return Sum(source.Span, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Byte Sum(this ReadOnlySpan<Byte> source, Byte overflow)
        {
            try
            {
                return source.Sum();
            }
            catch (OverflowException)
            {
                return overflow;
            }
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int16 Sum(this Memory<Int16> source)
        {
            return Sum(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int16 Sum(this Span<Int16> source)
        {
            return Sum((ReadOnlySpan<Int16>) source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int16 Sum(this ReadOnlyMemory<Int16> source)
        {
            return Sum(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int16 Sum(this ReadOnlySpan<Int16> source)
        {
            checked
            {
                return source.Aggregate<Int16, Int16>(0, (current, value) => (Int16) (current + value));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int16 Sum(this Memory<Int16> source, Int16 overflow)
        {
            return Sum(source.Span, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int16 Sum(this Span<Int16> source, Int16 overflow)
        {
            return Sum((ReadOnlySpan<Int16>) source, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int16 Sum(this ReadOnlyMemory<Int16> source, Int16 overflow)
        {
            return Sum(source.Span, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int16 Sum(this ReadOnlySpan<Int16> source, Int16 overflow)
        {
            try
            {
                return source.Sum();
            }
            catch (OverflowException)
            {
                return overflow;
            }
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt16 Sum(this Memory<UInt16> source)
        {
            return Sum(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt16 Sum(this Span<UInt16> source)
        {
            return Sum((ReadOnlySpan<UInt16>) source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt16 Sum(this ReadOnlyMemory<UInt16> source)
        {
            return Sum(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt16 Sum(this ReadOnlySpan<UInt16> source)
        {
            checked
            {
                return source.Aggregate<UInt16, UInt16>(0, (current, value) => (UInt16) (current + value));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt16 Sum(this Memory<UInt16> source, UInt16 overflow)
        {
            return Sum(source.Span, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt16 Sum(this Span<UInt16> source, UInt16 overflow)
        {
            return Sum((ReadOnlySpan<UInt16>) source, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt16 Sum(this ReadOnlyMemory<UInt16> source, UInt16 overflow)
        {
            return Sum(source.Span, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt16 Sum(this ReadOnlySpan<UInt16> source, UInt16 overflow)
        {
            try
            {
                return source.Sum();
            }
            catch (OverflowException)
            {
                return overflow;
            }
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Sum(this Memory<Int32> source)
        {
            return Sum(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Sum(this Span<Int32> source)
        {
            return Sum((ReadOnlySpan<Int32>) source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Sum(this ReadOnlyMemory<Int32> source)
        {
            return Sum(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Sum(this ReadOnlySpan<Int32> source)
        {
            checked
            {
                return source.Aggregate<Int32, Int32>(0, (current, value) => current + value);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Sum(this Memory<Int32> source, Int32 overflow)
        {
            return Sum(source.Span, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Sum(this Span<Int32> source, Int32 overflow)
        {
            return Sum((ReadOnlySpan<Int32>) source, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Sum(this ReadOnlyMemory<Int32> source, Int32 overflow)
        {
            return Sum(source.Span, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Sum(this ReadOnlySpan<Int32> source, Int32 overflow)
        {
            try
            {
                return source.Sum();
            }
            catch (OverflowException)
            {
                return overflow;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Sum<T>(this Memory<T> source, Func<T, Int32> selector)
        {
            return Sum(source.Span, selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Sum<T>(this Span<T> source, Func<T, Int32> selector)
        {
            return Sum((ReadOnlySpan<T>) source, selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Sum<T>(this ReadOnlyMemory<T> source, Func<T, Int32> selector)
        {
            return Sum(source.Span, selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Sum<T>(this ReadOnlySpan<T> source, Func<T, Int32> selector)
        {
            checked
            {
                return source.Aggregate(0, (current, item) => current + selector(item));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Sum<T>(this Memory<T> source, Func<T, Int32> selector, Int32 overflow)
        {
            return Sum(source.Span, selector, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Sum<T>(this Span<T> source, Func<T, Int32> selector, Int32 overflow)
        {
            return Sum((ReadOnlySpan<T>) source, selector, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Sum<T>(this ReadOnlyMemory<T> source, Func<T, Int32> selector, Int32 overflow)
        {
            return Sum(source.Span, selector, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Sum<T>(this ReadOnlySpan<T> source, Func<T, Int32> selector, Int32 overflow)
        {
            try
            {
                return source.Sum(selector);
            }
            catch (OverflowException)
            {
                return overflow;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 Sum(this Memory<UInt32> source)
        {
            return Sum(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 Sum(this Span<UInt32> source)
        {
            return Sum((ReadOnlySpan<UInt32>) source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 Sum(this ReadOnlyMemory<UInt32> source)
        {
            return Sum(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 Sum(this ReadOnlySpan<UInt32> source)
        {
            checked
            {
                return source.Aggregate<UInt32, UInt32>(0, (current, value) => current + value);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 Sum(this Memory<UInt32> source, UInt32 overflow)
        {
            return Sum(source.Span, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 Sum(this Span<UInt32> source, UInt32 overflow)
        {
            return Sum((ReadOnlySpan<UInt32>) source, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 Sum(this ReadOnlyMemory<UInt32> source, UInt32 overflow)
        {
            return Sum(source.Span, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 Sum(this ReadOnlySpan<UInt32> source, UInt32 overflow)
        {
            try
            {
                return source.Sum();
            }
            catch (OverflowException)
            {
                return overflow;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 Sum<T>(this Memory<T> source, Func<T, UInt32> selector)
        {
            return Sum(source.Span, selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 Sum<T>(this Span<T> source, Func<T, UInt32> selector)
        {
            return Sum((ReadOnlySpan<T>) source, selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 Sum<T>(this ReadOnlyMemory<T> source, Func<T, UInt32> selector)
        {
            return Sum(source.Span, selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 Sum<T>(this ReadOnlySpan<T> source, Func<T, UInt32> selector)
        {
            checked
            {
                return source.Aggregate<T, UInt32>(0, (current, item) => current + selector(item));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 Sum<T>(this Memory<T> source, Func<T, UInt32> selector, UInt32 overflow)
        {
            return Sum(source.Span, selector, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 Sum<T>(this Span<T> source, Func<T, UInt32> selector, UInt32 overflow)
        {
            return Sum((ReadOnlySpan<T>) source, selector, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 Sum<T>(this ReadOnlyMemory<T> source, Func<T, UInt32> selector, UInt32 overflow)
        {
            return Sum(source.Span, selector, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 Sum<T>(this ReadOnlySpan<T> source, Func<T, UInt32> selector, UInt32 overflow)
        {
            try
            {
                return source.Sum(selector);
            }
            catch (OverflowException)
            {
                return overflow;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 Sum(this Memory<Int64> source)
        {
            return Sum(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 Sum(this Span<Int64> source)
        {
            return Sum((ReadOnlySpan<Int64>) source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 Sum(this ReadOnlyMemory<Int64> source)
        {
            return Sum(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 Sum(this ReadOnlySpan<Int64> source)
        {
            checked
            {
                return source.Aggregate<Int64, Int64>(0, (current, value) => current + value);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 Sum(this Memory<Int64> source, Int64 overflow)
        {
            return Sum(source.Span, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 Sum(this Span<Int64> source, Int64 overflow)
        {
            return Sum((ReadOnlySpan<Int64>) source, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 Sum(this ReadOnlyMemory<Int64> source, Int64 overflow)
        {
            return Sum(source.Span, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 Sum(this ReadOnlySpan<Int64> source, Int64 overflow)
        {
            try
            {
                return source.Sum();
            }
            catch (OverflowException)
            {
                return overflow;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 Sum<T>(this Memory<T> source, Func<T, Int64> selector)
        {
            return Sum(source.Span, selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 Sum<T>(this Span<T> source, Func<T, Int64> selector)
        {
            return Sum((ReadOnlySpan<T>) source, selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 Sum<T>(this ReadOnlyMemory<T> source, Func<T, Int64> selector)
        {
            return Sum(source.Span, selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 Sum<T>(this ReadOnlySpan<T> source, Func<T, Int64> selector)
        {
            checked
            {
                return source.Aggregate<T, Int64>(0, (current, item) => current + selector(item));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 Sum<T>(this Memory<T> source, Func<T, Int64> selector, Int64 overflow)
        {
            return Sum(source.Span, selector, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 Sum<T>(this Span<T> source, Func<T, Int64> selector, Int64 overflow)
        {
            return Sum((ReadOnlySpan<T>) source, selector, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 Sum<T>(this ReadOnlyMemory<T> source, Func<T, Int64> selector, Int64 overflow)
        {
            return Sum(source.Span, selector, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 Sum<T>(this ReadOnlySpan<T> source, Func<T, Int64> selector, Int64 overflow)
        {
            try
            {
                return source.Sum(selector);
            }
            catch (OverflowException)
            {
                return overflow;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 Sum(this Memory<UInt64> source)
        {
            return Sum(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 Sum(this Span<UInt64> source)
        {
            return Sum((ReadOnlySpan<UInt64>) source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 Sum(this ReadOnlyMemory<UInt64> source)
        {
            return Sum(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 Sum(this ReadOnlySpan<UInt64> source)
        {
            checked
            {
                return source.Aggregate<UInt64, UInt64>(0, (current, value) => current + value);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 Sum(this Memory<UInt64> source, UInt64 overflow)
        {
            return Sum(source.Span, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 Sum(this Span<UInt64> source, UInt64 overflow)
        {
            return Sum((ReadOnlySpan<UInt64>) source, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 Sum(this ReadOnlyMemory<UInt64> source, UInt64 overflow)
        {
            return Sum(source.Span, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 Sum(this ReadOnlySpan<UInt64> source, UInt64 overflow)
        {
            try
            {
                return source.Sum();
            }
            catch (OverflowException)
            {
                return overflow;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 Sum<T>(this Memory<T> source, Func<T, UInt64> selector)
        {
            return Sum(source.Span, selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 Sum<T>(this Span<T> source, Func<T, UInt64> selector)
        {
            return Sum((ReadOnlySpan<T>) source, selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 Sum<T>(this ReadOnlyMemory<T> source, Func<T, UInt64> selector)
        {
            return Sum(source.Span, selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 Sum<T>(this ReadOnlySpan<T> source, Func<T, UInt64> selector)
        {
            checked
            {
                return source.Aggregate<T, UInt64>(0, (current, item) => current + selector(item));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 Sum<T>(this Memory<T> source, Func<T, UInt64> selector, UInt64 overflow)
        {
            return Sum(source.Span, selector, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 Sum<T>(this Span<T> source, Func<T, UInt64> selector, UInt64 overflow)
        {
            return Sum((ReadOnlySpan<T>) source, selector, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 Sum<T>(this ReadOnlyMemory<T> source, Func<T, UInt64> selector, UInt64 overflow)
        {
            return Sum(source.Span, selector, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 Sum<T>(this ReadOnlySpan<T> source, Func<T, UInt64> selector, UInt64 overflow)
        {
            try
            {
                return source.Sum(selector);
            }
            catch (OverflowException)
            {
                return overflow;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Sum(this Memory<Single> source)
        {
            return Sum(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Sum(this Span<Single> source)
        {
            return Sum((ReadOnlySpan<Single>) source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Sum(this ReadOnlyMemory<Single> source)
        {
            return Sum(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Sum(this ReadOnlySpan<Single> source)
        {
            checked
            {
                return source.Aggregate<Single, Single>(0, (current, value) => current + value);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Sum(this Memory<Single> source, Single overflow)
        {
            return Sum(source.Span, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Sum(this Span<Single> source, Single overflow)
        {
            return Sum((ReadOnlySpan<Single>) source, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Sum(this ReadOnlyMemory<Single> source, Single overflow)
        {
            return Sum(source.Span, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Sum(this ReadOnlySpan<Single> source, Single overflow)
        {
            try
            {
                return source.Sum();
            }
            catch (OverflowException)
            {
                return overflow;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Sum<T>(this Memory<T> source, Func<T, Single> selector)
        {
            return Sum(source.Span, selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Sum<T>(this Span<T> source, Func<T, Single> selector)
        {
            return Sum((ReadOnlySpan<T>) source, selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Sum<T>(this ReadOnlyMemory<T> source, Func<T, Single> selector)
        {
            return Sum(source.Span, selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Sum<T>(this ReadOnlySpan<T> source, Func<T, Single> selector)
        {
            checked
            {
                return source.Aggregate<T, Single>(0, (current, item) => current + selector(item));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Sum<T>(this Memory<T> source, Func<T, Single> selector, Single overflow)
        {
            return Sum(source.Span, selector, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Sum<T>(this Span<T> source, Func<T, Single> selector, Single overflow)
        {
            return Sum((ReadOnlySpan<T>) source, selector, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Sum<T>(this ReadOnlyMemory<T> source, Func<T, Single> selector, Single overflow)
        {
            return Sum(source.Span, selector, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Sum<T>(this ReadOnlySpan<T> source, Func<T, Single> selector, Single overflow)
        {
            try
            {
                return source.Sum(selector);
            }
            catch (OverflowException)
            {
                return overflow;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Sum(this Memory<Double> source)
        {
            return Sum(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Sum(this Span<Double> source)
        {
            return Sum((ReadOnlySpan<Double>) source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Sum(this ReadOnlyMemory<Double> source)
        {
            return Sum(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Sum(this ReadOnlySpan<Double> source)
        {
            checked
            {
                return source.Aggregate<Double, Double>(0, (current, value) => current + value);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Sum(this Memory<Double> source, Double overflow)
        {
            return Sum(source.Span, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Sum(this Span<Double> source, Double overflow)
        {
            return Sum((ReadOnlySpan<Double>) source, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Sum(this ReadOnlyMemory<Double> source, Double overflow)
        {
            return Sum(source.Span, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Sum(this ReadOnlySpan<Double> source, Double overflow)
        {
            try
            {
                return source.Sum();
            }
            catch (OverflowException)
            {
                return overflow;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Sum<T>(this Memory<T> source, Func<T, Double> selector)
        {
            return Sum(source.Span, selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Sum<T>(this Span<T> source, Func<T, Double> selector)
        {
            return Sum((ReadOnlySpan<T>) source, selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Sum<T>(this ReadOnlyMemory<T> source, Func<T, Double> selector)
        {
            return Sum(source.Span, selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Sum<T>(this ReadOnlySpan<T> source, Func<T, Double> selector)
        {
            checked
            {
                return source.Aggregate<T, Double>(0, (current, item) => current + selector(item));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Sum<T>(this Memory<T> source, Func<T, Double> selector, Double overflow)
        {
            return Sum(source.Span, selector, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Sum<T>(this Span<T> source, Func<T, Double> selector, Double overflow)
        {
            return Sum((ReadOnlySpan<T>) source, selector, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Sum<T>(this ReadOnlyMemory<T> source, Func<T, Double> selector, Double overflow)
        {
            return Sum(source.Span, selector, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Sum<T>(this ReadOnlySpan<T> source, Func<T, Double> selector, Double overflow)
        {
            try
            {
                return source.Sum(selector);
            }
            catch (OverflowException)
            {
                return overflow;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Sum(this Memory<Decimal> source)
        {
            return Sum(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Sum(this Span<Decimal> source)
        {
            return Sum((ReadOnlySpan<Decimal>) source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Sum(this ReadOnlyMemory<Decimal> source)
        {
            return Sum(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Sum(this ReadOnlySpan<Decimal> source)
        {
            checked
            {
                return source.Aggregate<Decimal, Decimal>(0, (current, value) => current + value);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Sum(this Memory<Decimal> source, Decimal overflow)
        {
            return Sum(source.Span, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Sum(this Span<Decimal> source, Decimal overflow)
        {
            return Sum((ReadOnlySpan<Decimal>) source, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Sum(this ReadOnlyMemory<Decimal> source, Decimal overflow)
        {
            return Sum(source.Span, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Sum(this ReadOnlySpan<Decimal> source, Decimal overflow)
        {
            try
            {
                return source.Sum();
            }
            catch (OverflowException)
            {
                return overflow;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Sum<T>(this Memory<T> source, Func<T, Decimal> selector)
        {
            return Sum(source.Span, selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Sum<T>(this Span<T> source, Func<T, Decimal> selector)
        {
            return Sum((ReadOnlySpan<T>) source, selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Sum<T>(this ReadOnlyMemory<T> source, Func<T, Decimal> selector)
        {
            return Sum(source.Span, selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Sum<T>(this ReadOnlySpan<T> source, Func<T, Decimal> selector)
        {
            checked
            {
                return source.Aggregate<T, Decimal>(0, (current, item) => current + selector(item));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Sum<T>(this Memory<T> source, Func<T, Decimal> selector, Decimal overflow)
        {
            return Sum(source.Span, selector, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Sum<T>(this Span<T> source, Func<T, Decimal> selector, Decimal overflow)
        {
            return Sum((ReadOnlySpan<T>) source, selector, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Sum<T>(this ReadOnlyMemory<T> source, Func<T, Decimal> selector, Decimal overflow)
        {
            return Sum(source.Span, selector, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Sum<T>(this ReadOnlySpan<T> source, Func<T, Decimal> selector, Decimal overflow)
        {
            try
            {
                return source.Sum(selector);
            }
            catch (OverflowException)
            {
                return overflow;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInteger Sum(this Memory<BigInteger> source)
        {
            return Sum(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInteger Sum(this Span<BigInteger> source)
        {
            return Sum((ReadOnlySpan<BigInteger>) source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInteger Sum(this ReadOnlyMemory<BigInteger> source)
        {
            return Sum(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInteger Sum(this ReadOnlySpan<BigInteger> source)
        {
            checked
            {
                return source.Aggregate<BigInteger, BigInteger>(0, (current, value) => current + value);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInteger Sum(this Memory<BigInteger> source, BigInteger overflow)
        {
            return Sum(source.Span, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInteger Sum(this Span<BigInteger> source, BigInteger overflow)
        {
            return Sum((ReadOnlySpan<BigInteger>) source, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInteger Sum(this ReadOnlyMemory<BigInteger> source, BigInteger overflow)
        {
            return Sum(source.Span, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInteger Sum(this ReadOnlySpan<BigInteger> source, BigInteger overflow)
        {
            try
            {
                return source.Sum();
            }
            catch (OverflowException)
            {
                return overflow;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInteger Sum<T>(this Memory<T> source, Func<T, BigInteger> selector)
        {
            return Sum(source.Span, selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInteger Sum<T>(this Span<T> source, Func<T, BigInteger> selector)
        {
            return Sum((ReadOnlySpan<T>) source, selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInteger Sum<T>(this ReadOnlyMemory<T> source, Func<T, BigInteger> selector)
        {
            return Sum(source.Span, selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInteger Sum<T>(this ReadOnlySpan<T> source, Func<T, BigInteger> selector)
        {
            checked
            {
                return source.Aggregate<T, BigInteger>(0, (current, item) => current + selector(item));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInteger Sum<T>(this Memory<T> source, Func<T, BigInteger> selector, BigInteger overflow)
        {
            return Sum(source.Span, selector, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInteger Sum<T>(this Span<T> source, Func<T, BigInteger> selector, BigInteger overflow)
        {
            return Sum((ReadOnlySpan<T>) source, selector, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInteger Sum<T>(this ReadOnlyMemory<T> source, Func<T, BigInteger> selector, BigInteger overflow)
        {
            return Sum(source.Span, selector, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInteger Sum<T>(this ReadOnlySpan<T> source, Func<T, BigInteger> selector, BigInteger overflow)
        {
            try
            {
                return source.Sum(selector);
            }
            catch (OverflowException)
            {
                return overflow;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Sum(this Memory<Complex> source)
        {
            return Sum(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Sum(this Span<Complex> source)
        {
            return Sum((ReadOnlySpan<Complex>) source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Sum(this ReadOnlyMemory<Complex> source)
        {
            return Sum(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Sum(this ReadOnlySpan<Complex> source)
        {
            checked
            {
                return source.Aggregate<Complex, Complex>(0, (current, value) => current + value);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Sum(this Memory<Complex> source, Complex overflow)
        {
            return Sum(source.Span, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Sum(this Span<Complex> source, Complex overflow)
        {
            return Sum((ReadOnlySpan<Complex>) source, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Sum(this ReadOnlyMemory<Complex> source, Complex overflow)
        {
            return Sum(source.Span, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Sum(this ReadOnlySpan<Complex> source, Complex overflow)
        {
            try
            {
                return source.Sum();
            }
            catch (OverflowException)
            {
                return overflow;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Sum<T>(this Memory<T> source, Func<T, Complex> selector)
        {
            return Sum(source.Span, selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Sum<T>(this Span<T> source, Func<T, Complex> selector)
        {
            return Sum((ReadOnlySpan<T>) source, selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Sum<T>(this ReadOnlyMemory<T> source, Func<T, Complex> selector)
        {
            return Sum(source.Span, selector);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Sum<T>(this ReadOnlySpan<T> source, Func<T, Complex> selector)
        {
            checked
            {
                return source.Aggregate<T, Complex>(0, (current, item) => current + selector(item));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Sum<T>(this Memory<T> source, Func<T, Complex> selector, Complex overflow)
        {
            return Sum(source.Span, selector, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Sum<T>(this Span<T> source, Func<T, Complex> selector, Complex overflow)
        {
            return Sum((ReadOnlySpan<T>) source, selector, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Sum<T>(this ReadOnlyMemory<T> source, Func<T, Complex> selector, Complex overflow)
        {
            return Sum(source.Span, selector, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Sum<T>(this ReadOnlySpan<T> source, Func<T, Complex> selector, Complex overflow)
        {
            try
            {
                return source.Sum(selector);
            }
            catch (OverflowException)
            {
                return overflow;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SByte Multiply(this Memory<SByte> source)
        {
            return Multiply(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SByte Multiply(this Span<SByte> source)
        {
            return Multiply((ReadOnlySpan<SByte>) source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SByte Multiply(this ReadOnlyMemory<SByte> source)
        {
            return Multiply(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SByte Multiply(this ReadOnlySpan<SByte> source)
        {
            checked
            {
                ReadOnlySpan<SByte>.Enumerator enumerator = source.GetEnumerator();

                if (!enumerator.MoveNext() || enumerator.Current == 0)
                {
                    return 0;
                }

                SByte result = enumerator.Current;

                while (enumerator.MoveNext())
                {
                    switch (enumerator.Current)
                    {
                        case 0:
                            return 0;
                        case 1:
                            continue;
                        default:
                            result *= enumerator.Current;
                            break;
                    }
                }

                return result;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SByte Multiply(this Memory<SByte> source, SByte overflow)
        {
            return Multiply(source.Span, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SByte Multiply(this Span<SByte> source, SByte overflow)
        {
            return Multiply((ReadOnlySpan<SByte>) source, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SByte Multiply(this ReadOnlyMemory<SByte> source, SByte overflow)
        {
            return Multiply(source.Span, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SByte Multiply(this ReadOnlySpan<SByte> source, SByte overflow)
        {
            try
            {
                return source.Multiply();
            }
            catch (OverflowException)
            {
                return overflow;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Byte Multiply(this Memory<Byte> source)
        {
            return Multiply(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Byte Multiply(this Span<Byte> source)
        {
            return Multiply((ReadOnlySpan<Byte>) source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Byte Multiply(this ReadOnlyMemory<Byte> source)
        {
            return Multiply(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Byte Multiply(this ReadOnlySpan<Byte> source)
        {
            checked
            {
                ReadOnlySpan<Byte>.Enumerator enumerator = source.GetEnumerator();

                if (!enumerator.MoveNext() || enumerator.Current == 0)
                {
                    return 0;
                }

                Byte result = enumerator.Current;

                while (enumerator.MoveNext())
                {
                    switch (enumerator.Current)
                    {
                        case 0:
                            return 0;
                        case 1:
                            continue;
                        default:
                            result *= enumerator.Current;
                            break;
                    }
                }

                return result;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Byte Multiply(this Memory<Byte> source, Byte overflow)
        {
            return Multiply(source.Span, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Byte Multiply(this Span<Byte> source, Byte overflow)
        {
            return Multiply((ReadOnlySpan<Byte>) source, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Byte Multiply(this ReadOnlyMemory<Byte> source, Byte overflow)
        {
            return Multiply(source.Span, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Byte Multiply(this ReadOnlySpan<Byte> source, Byte overflow)
        {
            try
            {
                return source.Multiply();
            }
            catch (OverflowException)
            {
                return overflow;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int16 Multiply(this Memory<Int16> source)
        {
            return Multiply(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int16 Multiply(this Span<Int16> source)
        {
            return Multiply((ReadOnlySpan<Int16>) source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int16 Multiply(this ReadOnlyMemory<Int16> source)
        {
            return Multiply(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int16 Multiply(this ReadOnlySpan<Int16> source)
        {
            checked
            {
                ReadOnlySpan<Int16>.Enumerator enumerator = source.GetEnumerator();

                if (!enumerator.MoveNext() || enumerator.Current == 0)
                {
                    return 0;
                }

                Int16 result = enumerator.Current;

                while (enumerator.MoveNext())
                {
                    switch (enumerator.Current)
                    {
                        case 0:
                            return 0;
                        case 1:
                            continue;
                        default:
                            result *= enumerator.Current;
                            break;
                    }
                }

                return result;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int16 Multiply(this Memory<Int16> source, Int16 overflow)
        {
            return Multiply(source.Span, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int16 Multiply(this Span<Int16> source, Int16 overflow)
        {
            return Multiply((ReadOnlySpan<Int16>) source, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int16 Multiply(this ReadOnlyMemory<Int16> source, Int16 overflow)
        {
            return Multiply(source.Span, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int16 Multiply(this ReadOnlySpan<Int16> source, Int16 overflow)
        {
            try
            {
                return source.Multiply();
            }
            catch (OverflowException)
            {
                return overflow;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt16 Multiply(this Memory<UInt16> source)
        {
            return Multiply(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt16 Multiply(this Span<UInt16> source)
        {
            return Multiply((ReadOnlySpan<UInt16>) source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt16 Multiply(this ReadOnlyMemory<UInt16> source)
        {
            return Multiply(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt16 Multiply(this ReadOnlySpan<UInt16> source)
        {
            checked
            {
                ReadOnlySpan<UInt16>.Enumerator enumerator = source.GetEnumerator();

                if (!enumerator.MoveNext() || enumerator.Current == 0)
                {
                    return 0;
                }

                UInt16 result = enumerator.Current;

                while (enumerator.MoveNext())
                {
                    switch (enumerator.Current)
                    {
                        case 0:
                            return 0;
                        case 1:
                            continue;
                        default:
                            result *= enumerator.Current;
                            break;
                    }
                }

                return result;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt16 Multiply(this Memory<UInt16> source, UInt16 overflow)
        {
            return Multiply(source.Span, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt16 Multiply(this Span<UInt16> source, UInt16 overflow)
        {
            return Multiply((ReadOnlySpan<UInt16>) source, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt16 Multiply(this ReadOnlyMemory<UInt16> source, UInt16 overflow)
        {
            return Multiply(source.Span, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt16 Multiply(this ReadOnlySpan<UInt16> source, UInt16 overflow)
        {
            try
            {
                return source.Multiply();
            }
            catch (OverflowException)
            {
                return overflow;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Multiply(this Memory<Int32> source)
        {
            return Multiply(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Multiply(this Span<Int32> source)
        {
            return Multiply((ReadOnlySpan<Int32>) source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Multiply(this ReadOnlyMemory<Int32> source)
        {
            return Multiply(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Multiply(this ReadOnlySpan<Int32> source)
        {
            checked
            {
                ReadOnlySpan<Int32>.Enumerator enumerator = source.GetEnumerator();

                if (!enumerator.MoveNext() || enumerator.Current == 0)
                {
                    return 0;
                }

                Int32 result = enumerator.Current;

                while (enumerator.MoveNext())
                {
                    switch (enumerator.Current)
                    {
                        case 0:
                            return 0;
                        case 1:
                            continue;
                        default:
                            result *= enumerator.Current;
                            break;
                    }
                }

                return result;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Multiply(this Memory<Int32> source, Int32 overflow)
        {
            return Multiply(source.Span, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Multiply(this Span<Int32> source, Int32 overflow)
        {
            return Multiply((ReadOnlySpan<Int32>) source, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Multiply(this ReadOnlyMemory<Int32> source, Int32 overflow)
        {
            return Multiply(source.Span, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Multiply(this ReadOnlySpan<Int32> source, Int32 overflow)
        {
            try
            {
                return source.Multiply();
            }
            catch (OverflowException)
            {
                return overflow;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 Multiply(this Memory<UInt32> source)
        {
            return Multiply(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 Multiply(this Span<UInt32> source)
        {
            return Multiply((ReadOnlySpan<UInt32>) source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 Multiply(this ReadOnlyMemory<UInt32> source)
        {
            return Multiply(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 Multiply(this ReadOnlySpan<UInt32> source)
        {
            checked
            {
                ReadOnlySpan<UInt32>.Enumerator enumerator = source.GetEnumerator();

                if (!enumerator.MoveNext() || enumerator.Current == 0)
                {
                    return 0;
                }

                UInt32 result = enumerator.Current;

                while (enumerator.MoveNext())
                {
                    switch (enumerator.Current)
                    {
                        case 0:
                            return 0;
                        case 1:
                            continue;
                        default:
                            result *= enumerator.Current;
                            break;
                    }
                }

                return result;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 Multiply(this Memory<UInt32> source, UInt32 overflow)
        {
            return Multiply(source.Span, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 Multiply(this Span<UInt32> source, UInt32 overflow)
        {
            return Multiply((ReadOnlySpan<UInt32>) source, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 Multiply(this ReadOnlyMemory<UInt32> source, UInt32 overflow)
        {
            return Multiply(source.Span, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 Multiply(this ReadOnlySpan<UInt32> source, UInt32 overflow)
        {
            try
            {
                return source.Multiply();
            }
            catch (OverflowException)
            {
                return overflow;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 Multiply(this Memory<Int64> source)
        {
            return Multiply(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 Multiply(this Span<Int64> source)
        {
            return Multiply((ReadOnlySpan<Int64>) source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 Multiply(this ReadOnlyMemory<Int64> source)
        {
            return Multiply(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 Multiply(this ReadOnlySpan<Int64> source)
        {
            checked
            {
                ReadOnlySpan<Int64>.Enumerator enumerator = source.GetEnumerator();

                if (!enumerator.MoveNext() || enumerator.Current == 0)
                {
                    return 0;
                }

                Int64 result = enumerator.Current;

                while (enumerator.MoveNext())
                {
                    switch (enumerator.Current)
                    {
                        case 0:
                            return 0;
                        case 1:
                            continue;
                        default:
                            result *= enumerator.Current;
                            break;
                    }
                }

                return result;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 Multiply(this Memory<Int64> source, Int64 overflow)
        {
            return Multiply(source.Span, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 Multiply(this Span<Int64> source, Int64 overflow)
        {
            return Multiply((ReadOnlySpan<Int64>) source, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 Multiply(this ReadOnlyMemory<Int64> source, Int64 overflow)
        {
            return Multiply(source.Span, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 Multiply(this ReadOnlySpan<Int64> source, Int64 overflow)
        {
            try
            {
                return source.Multiply();
            }
            catch (OverflowException)
            {
                return overflow;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 Multiply(this Memory<UInt64> source)
        {
            return Multiply(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 Multiply(this Span<UInt64> source)
        {
            return Multiply((ReadOnlySpan<UInt64>) source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 Multiply(this ReadOnlyMemory<UInt64> source)
        {
            return Multiply(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 Multiply(this ReadOnlySpan<UInt64> source)
        {
            checked
            {
                ReadOnlySpan<UInt64>.Enumerator enumerator = source.GetEnumerator();

                if (!enumerator.MoveNext() || enumerator.Current == 0)
                {
                    return 0;
                }

                UInt64 result = enumerator.Current;

                while (enumerator.MoveNext())
                {
                    switch (enumerator.Current)
                    {
                        case 0:
                            return 0;
                        case 1:
                            continue;
                        default:
                            result *= enumerator.Current;
                            break;
                    }
                }

                return result;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 Multiply(this Memory<UInt64> source, UInt64 overflow)
        {
            return Multiply(source.Span, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 Multiply(this Span<UInt64> source, UInt64 overflow)
        {
            return Multiply((ReadOnlySpan<UInt64>) source, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 Multiply(this ReadOnlyMemory<UInt64> source, UInt64 overflow)
        {
            return Multiply(source.Span, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 Multiply(this ReadOnlySpan<UInt64> source, UInt64 overflow)
        {
            try
            {
                return source.Multiply();
            }
            catch (OverflowException)
            {
                return overflow;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Multiply(this Memory<Single> source)
        {
            return Multiply(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Multiply(this Span<Single> source)
        {
            return Multiply((ReadOnlySpan<Single>) source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Multiply(this ReadOnlyMemory<Single> source)
        {
            return Multiply(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Multiply(this ReadOnlySpan<Single> source)
        {
            checked
            {
                ReadOnlySpan<Single>.Enumerator enumerator = source.GetEnumerator();

                if (!enumerator.MoveNext() || enumerator.Current == 0)
                {
                    return 0;
                }

                Single result = enumerator.Current;

                while (enumerator.MoveNext())
                {
                    switch (enumerator.Current)
                    {
                        case 0:
                            return 0;
                        case 1:
                            continue;
                        default:
                            result *= enumerator.Current;
                            break;
                    }
                }

                return result;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Multiply(this Memory<Single> source, Single overflow)
        {
            return Multiply(source.Span, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Multiply(this Span<Single> source, Single overflow)
        {
            return Multiply((ReadOnlySpan<Single>) source, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Multiply(this ReadOnlyMemory<Single> source, Single overflow)
        {
            return Multiply(source.Span, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Multiply(this ReadOnlySpan<Single> source, Single overflow)
        {
            try
            {
                return source.Multiply();
            }
            catch (OverflowException)
            {
                return overflow;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Multiply(this Memory<Double> source)
        {
            return Multiply(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Multiply(this Span<Double> source)
        {
            return Multiply((ReadOnlySpan<Double>) source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Multiply(this ReadOnlyMemory<Double> source)
        {
            return Multiply(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Multiply(this ReadOnlySpan<Double> source)
        {
            checked
            {
                ReadOnlySpan<Double>.Enumerator enumerator = source.GetEnumerator();

                if (!enumerator.MoveNext() || enumerator.Current == 0)
                {
                    return 0;
                }

                Double result = enumerator.Current;

                while (enumerator.MoveNext())
                {
                    switch (enumerator.Current)
                    {
                        case 0:
                            return 0;
                        case 1:
                            continue;
                        default:
                            result *= enumerator.Current;
                            break;
                    }
                }

                return result;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Multiply(this Memory<Double> source, Double overflow)
        {
            return Multiply(source.Span, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Multiply(this Span<Double> source, Double overflow)
        {
            return Multiply((ReadOnlySpan<Double>) source, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Multiply(this ReadOnlyMemory<Double> source, Double overflow)
        {
            return Multiply(source.Span, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Multiply(this ReadOnlySpan<Double> source, Double overflow)
        {
            try
            {
                return source.Multiply();
            }
            catch (OverflowException)
            {
                return overflow;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Multiply(this Memory<Decimal> source)
        {
            return Multiply(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Multiply(this Span<Decimal> source)
        {
            return Multiply((ReadOnlySpan<Decimal>) source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Multiply(this ReadOnlyMemory<Decimal> source)
        {
            return Multiply(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Multiply(this ReadOnlySpan<Decimal> source)
        {
            checked
            {
                ReadOnlySpan<Decimal>.Enumerator enumerator = source.GetEnumerator();

                if (!enumerator.MoveNext() || enumerator.Current == 0)
                {
                    return 0;
                }

                Decimal result = enumerator.Current;

                while (enumerator.MoveNext())
                {
                    switch (enumerator.Current)
                    {
                        case 0:
                            return 0;
                        case 1:
                            continue;
                        default:
                            result *= enumerator.Current;
                            break;
                    }
                }

                return result;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Multiply(this Memory<Decimal> source, Decimal overflow)
        {
            return Multiply(source.Span, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Multiply(this Span<Decimal> source, Decimal overflow)
        {
            return Multiply((ReadOnlySpan<Decimal>) source, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Multiply(this ReadOnlyMemory<Decimal> source, Decimal overflow)
        {
            return Multiply(source.Span, overflow);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Multiply(this ReadOnlySpan<Decimal> source, Decimal overflow)
        {
            try
            {
                return source.Multiply();
            }
            catch (OverflowException)
            {
                return overflow;
            }
        }

        [SuppressMessage("ReSharper", "SuspiciousTypeConversion.Global")]
        public static Double Average(this ReadOnlySpan<SByte> source)
        {
            ReadOnlySpan<SByte>.Enumerator enumerator = source.GetEnumerator();

            if (!enumerator.MoveNext())
            {
                throw new InvalidOperationException();
            }

            Double result = enumerator.Current;
            Int64 count = 1;

            while (enumerator.MoveNext())
            {
                result += enumerator.Current;
                count++;
            }

            return result / count;
        }

        [SuppressMessage("ReSharper", "SuspiciousTypeConversion.Global")]
        public static Double Average(this ReadOnlySpan<Byte> source)
        {
            ReadOnlySpan<Byte>.Enumerator enumerator = source.GetEnumerator();

            if (!enumerator.MoveNext())
            {
                throw new InvalidOperationException();
            }

            Double result = enumerator.Current;
            Int64 count = 1;

            while (enumerator.MoveNext())
            {
                result += enumerator.Current;
                count++;
            }

            return result / count;
        }

        [SuppressMessage("ReSharper", "SuspiciousTypeConversion.Global")]
        public static Double Average(this ReadOnlySpan<Int16> source)
        {
            ReadOnlySpan<Int16>.Enumerator enumerator = source.GetEnumerator();

            if (!enumerator.MoveNext())
            {
                throw new InvalidOperationException();
            }

            Double result = enumerator.Current;
            Int64 count = 1;

            while (enumerator.MoveNext())
            {
                result += enumerator.Current;
                count++;
            }

            return result / count;
        }

        [SuppressMessage("ReSharper", "SuspiciousTypeConversion.Global")]
        public static Double Average(this ReadOnlySpan<UInt16> source)
        {
            ReadOnlySpan<UInt16>.Enumerator enumerator = source.GetEnumerator();

            if (!enumerator.MoveNext())
            {
                throw new InvalidOperationException();
            }

            Double result = enumerator.Current;
            Int64 count = 1;

            while (enumerator.MoveNext())
            {
                result += enumerator.Current;
                count++;
            }

            return result / count;
        }

        [SuppressMessage("ReSharper", "SuspiciousTypeConversion.Global")]
        public static Double Average(this ReadOnlySpan<Int32> source)
        {
            ReadOnlySpan<Int32>.Enumerator enumerator = source.GetEnumerator();

            if (!enumerator.MoveNext())
            {
                throw new InvalidOperationException();
            }

            Double result = enumerator.Current;
            Int64 count = 1;

            while (enumerator.MoveNext())
            {
                result += enumerator.Current;
                count++;
            }

            return result / count;
        }

        [SuppressMessage("ReSharper", "SuspiciousTypeConversion.Global")]
        public static Double Average(this ReadOnlySpan<UInt32> source)
        {
            ReadOnlySpan<UInt32>.Enumerator enumerator = source.GetEnumerator();

            if (!enumerator.MoveNext())
            {
                throw new InvalidOperationException();
            }

            Double result = enumerator.Current;
            Int64 count = 1;

            while (enumerator.MoveNext())
            {
                result += enumerator.Current;
                count++;
            }

            return result / count;
        }

        [SuppressMessage("ReSharper", "SuspiciousTypeConversion.Global")]
        public static Double Average(this ReadOnlySpan<Int64> source)
        {
            ReadOnlySpan<Int64>.Enumerator enumerator = source.GetEnumerator();

            if (!enumerator.MoveNext())
            {
                throw new InvalidOperationException();
            }

            Double result = enumerator.Current;
            Int64 count = 1;

            while (enumerator.MoveNext())
            {
                result += enumerator.Current;
                count++;
            }

            return result / count;
        }

        [SuppressMessage("ReSharper", "SuspiciousTypeConversion.Global")]
        public static Double Average(this ReadOnlySpan<UInt64> source)
        {
            ReadOnlySpan<UInt64>.Enumerator enumerator = source.GetEnumerator();

            if (!enumerator.MoveNext())
            {
                throw new InvalidOperationException();
            }

            Double result = enumerator.Current;
            Int64 count = 1;

            while (enumerator.MoveNext())
            {
                result += enumerator.Current;
                count++;
            }

            return result / count;
        }

        [SuppressMessage("ReSharper", "SuspiciousTypeConversion.Global")]
        public static Double Average(this ReadOnlySpan<Single> source)
        {
            ReadOnlySpan<Single>.Enumerator enumerator = source.GetEnumerator();

            if (!enumerator.MoveNext())
            {
                throw new InvalidOperationException();
            }

            Double result = enumerator.Current;
            Int64 count = 1;

            while (enumerator.MoveNext())
            {
                result += enumerator.Current;
                count++;
            }

            return result / count;
        }

        [SuppressMessage("ReSharper", "SuspiciousTypeConversion.Global")]
        public static Double Average(this ReadOnlySpan<Double> source)
        {
            ReadOnlySpan<Double>.Enumerator enumerator = source.GetEnumerator();

            if (!enumerator.MoveNext())
            {
                throw new InvalidOperationException();
            }

            Double result = enumerator.Current;
            Int64 count = 1;

            while (enumerator.MoveNext())
            {
                result += enumerator.Current;
                count++;
            }

            return result / count;
        }

        [SuppressMessage("ReSharper", "SuspiciousTypeConversion.Global")]
        public static Decimal Average(this ReadOnlySpan<Decimal> source)
        {
            ReadOnlySpan<Decimal>.Enumerator enumerator = source.GetEnumerator();

            if (!enumerator.MoveNext())
            {
                throw new InvalidOperationException();
            }

            Decimal result = enumerator.Current;
            Int64 count = 1;

            while (enumerator.MoveNext())
            {
                result += enumerator.Current;
                count++;
            }

            return result / count;
        }

        [SuppressMessage("ReSharper", "SuspiciousTypeConversion.Global")]
        public static Complex Average(this ReadOnlySpan<Complex> source)
        {
            ReadOnlySpan<Complex>.Enumerator enumerator = source.GetEnumerator();

            if (!enumerator.MoveNext())
            {
                throw new InvalidOperationException();
            }

            Complex result = enumerator.Current;
            Int64 count = 1;

            while (enumerator.MoveNext())
            {
                result += enumerator.Current;
                count++;
            }

            return result / count;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Variance(this Memory<SByte> source)
        {
            return Variance(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Variance(this Span<SByte> source)
        {
            return Variance((ReadOnlySpan<SByte>) source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Variance(this ReadOnlyMemory<SByte> source)
        {
            return Variance(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Variance(this ReadOnlySpan<SByte> source)
        {
            if (source.Length <= 0)
            {
                return 0;
            }

            Double mean = source.Average();
            Double sum = source.Sum(x => (x - mean).Pow(2));
            return sum / source.Length;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Variance(this Memory<Byte> source)
        {
            return Variance(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Variance(this Span<Byte> source)
        {
            return Variance((ReadOnlySpan<Byte>) source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Variance(this ReadOnlyMemory<Byte> source)
        {
            return Variance(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Variance(this ReadOnlySpan<Byte> source)
        {
            if (source.Length <= 0)
            {
                return 0;
            }

            Double mean = source.Average();
            Double sum = source.Sum(x => (x - mean).Pow(2));
            return sum / source.Length;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Variance(this Memory<Int16> source)
        {
            return Variance(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Variance(this Span<Int16> source)
        {
            return Variance((ReadOnlySpan<Int16>) source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Variance(this ReadOnlyMemory<Int16> source)
        {
            return Variance(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Variance(this ReadOnlySpan<Int16> source)
        {
            if (source.Length <= 0)
            {
                return 0;
            }

            Double mean = source.Average();
            Double sum = source.Sum(x => (x - mean).Pow(2));
            return sum / source.Length;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Variance(this Memory<UInt16> source)
        {
            return Variance(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Variance(this Span<UInt16> source)
        {
            return Variance((ReadOnlySpan<UInt16>) source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Variance(this ReadOnlyMemory<UInt16> source)
        {
            return Variance(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Variance(this ReadOnlySpan<UInt16> source)
        {
            if (source.Length <= 0)
            {
                return 0;
            }

            Double mean = source.Average();
            Double sum = source.Sum(x => (x - mean).Pow(2));
            return sum / source.Length;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Variance(this Memory<Int32> source)
        {
            return Variance(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Variance(this Span<Int32> source)
        {
            return Variance((ReadOnlySpan<Int32>) source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Variance(this ReadOnlyMemory<Int32> source)
        {
            return Variance(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Variance(this ReadOnlySpan<Int32> source)
        {
            if (source.Length <= 0)
            {
                return 0;
            }

            Double mean = source.Average();
            Double sum = source.Sum(x => (x - mean).Pow(2));
            return sum / source.Length;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Variance(this Memory<UInt32> source)
        {
            return Variance(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Variance(this Span<UInt32> source)
        {
            return Variance((ReadOnlySpan<UInt32>) source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Variance(this ReadOnlyMemory<UInt32> source)
        {
            return Variance(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Variance(this ReadOnlySpan<UInt32> source)
        {
            if (source.Length <= 0)
            {
                return 0;
            }

            Double mean = source.Average();
            Double sum = source.Sum(x => (x - mean).Pow(2));
            return sum / source.Length;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Variance(this Memory<Int64> source)
        {
            return Variance(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Variance(this Span<Int64> source)
        {
            return Variance((ReadOnlySpan<Int64>) source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Variance(this ReadOnlyMemory<Int64> source)
        {
            return Variance(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Variance(this ReadOnlySpan<Int64> source)
        {
            if (source.Length <= 0)
            {
                return 0;
            }

            Double mean = source.Average();
            Double sum = source.Sum(x => (x - mean).Pow(2));
            return sum / source.Length;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Variance(this Memory<UInt64> source)
        {
            return Variance(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Variance(this Span<UInt64> source)
        {
            return Variance((ReadOnlySpan<UInt64>) source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Variance(this ReadOnlyMemory<UInt64> source)
        {
            return Variance(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Variance(this ReadOnlySpan<UInt64> source)
        {
            if (source.Length <= 0)
            {
                return 0;
            }

            Double mean = source.Average();
            Double sum = source.Sum(x => (x - mean).Pow(2));
            return sum / source.Length;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Variance(this Memory<Single> source)
        {
            return Variance(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Variance(this Span<Single> source)
        {
            return Variance((ReadOnlySpan<Single>) source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Variance(this ReadOnlyMemory<Single> source)
        {
            return Variance(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Variance(this ReadOnlySpan<Single> source)
        {
            if (source.Length <= 0)
            {
                return 0;
            }

            Double mean = source.Average();
            Double sum = source.Sum(x => (x - mean).Pow(2));
            return sum / source.Length;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Variance(this Memory<Double> source)
        {
            return Variance(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Variance(this Span<Double> source)
        {
            return Variance((ReadOnlySpan<Double>) source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Variance(this ReadOnlyMemory<Double> source)
        {
            return Variance(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Variance(this ReadOnlySpan<Double> source)
        {
            if (source.Length <= 0)
            {
                return 0;
            }

            Double mean = source.Average();
            Double sum = source.Sum(x => (x - mean).Pow(2));
            return sum / source.Length;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Variance(this Memory<Decimal> source)
        {
            return Variance(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Variance(this Span<Decimal> source)
        {
            return Variance((ReadOnlySpan<Decimal>) source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Variance(this ReadOnlyMemory<Decimal> source)
        {
            return Variance(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Variance(this ReadOnlySpan<Decimal> source)
        {
            if (source.Length <= 0)
            {
                return 0;
            }

            Decimal mean = source.Average();
            Decimal sum = source.Sum(x => (x - mean).Pow(2));
            return sum / source.Length;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double StandardDeviation(this Memory<SByte> source)
        {
            return StandardDeviation(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double StandardDeviation(this Span<SByte> source)
        {
            return StandardDeviation((ReadOnlySpan<SByte>) source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double StandardDeviation(this ReadOnlyMemory<SByte> source)
        {
            return StandardDeviation(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double StandardDeviation(this ReadOnlySpan<SByte> source)
        {
            return source.Variance().Sqrt();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double StandardDeviation(this Memory<Byte> source)
        {
            return StandardDeviation(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double StandardDeviation(this Span<Byte> source)
        {
            return StandardDeviation((ReadOnlySpan<Byte>) source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double StandardDeviation(this ReadOnlyMemory<Byte> source)
        {
            return StandardDeviation(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double StandardDeviation(this ReadOnlySpan<Byte> source)
        {
            return source.Variance().Sqrt();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double StandardDeviation(this Memory<Int16> source)
        {
            return StandardDeviation(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double StandardDeviation(this Span<Int16> source)
        {
            return StandardDeviation((ReadOnlySpan<Int16>) source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double StandardDeviation(this ReadOnlyMemory<Int16> source)
        {
            return StandardDeviation(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double StandardDeviation(this ReadOnlySpan<Int16> source)
        {
            return source.Variance().Sqrt();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double StandardDeviation(this Memory<UInt16> source)
        {
            return StandardDeviation(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double StandardDeviation(this Span<UInt16> source)
        {
            return StandardDeviation((ReadOnlySpan<UInt16>) source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double StandardDeviation(this ReadOnlyMemory<UInt16> source)
        {
            return StandardDeviation(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double StandardDeviation(this ReadOnlySpan<UInt16> source)
        {
            return source.Variance().Sqrt();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double StandardDeviation(this Memory<Int32> source)
        {
            return StandardDeviation(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double StandardDeviation(this Span<Int32> source)
        {
            return StandardDeviation((ReadOnlySpan<Int32>) source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double StandardDeviation(this ReadOnlyMemory<Int32> source)
        {
            return StandardDeviation(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double StandardDeviation(this ReadOnlySpan<Int32> source)
        {
            return source.Variance().Sqrt();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double StandardDeviation(this Memory<UInt32> source)
        {
            return StandardDeviation(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double StandardDeviation(this Span<UInt32> source)
        {
            return StandardDeviation((ReadOnlySpan<UInt32>) source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double StandardDeviation(this ReadOnlyMemory<UInt32> source)
        {
            return StandardDeviation(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double StandardDeviation(this ReadOnlySpan<UInt32> source)
        {
            return source.Variance().Sqrt();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double StandardDeviation(this Memory<Int64> source)
        {
            return StandardDeviation(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double StandardDeviation(this Span<Int64> source)
        {
            return StandardDeviation((ReadOnlySpan<Int64>) source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double StandardDeviation(this ReadOnlyMemory<Int64> source)
        {
            return StandardDeviation(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double StandardDeviation(this ReadOnlySpan<Int64> source)
        {
            return source.Variance().Sqrt();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double StandardDeviation(this Memory<UInt64> source)
        {
            return StandardDeviation(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double StandardDeviation(this Span<UInt64> source)
        {
            return StandardDeviation((ReadOnlySpan<UInt64>) source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double StandardDeviation(this ReadOnlyMemory<UInt64> source)
        {
            return StandardDeviation(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double StandardDeviation(this ReadOnlySpan<UInt64> source)
        {
            return source.Variance().Sqrt();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double StandardDeviation(this Memory<Single> source)
        {
            return StandardDeviation(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double StandardDeviation(this Span<Single> source)
        {
            return StandardDeviation((ReadOnlySpan<Single>) source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double StandardDeviation(this ReadOnlyMemory<Single> source)
        {
            return StandardDeviation(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double StandardDeviation(this ReadOnlySpan<Single> source)
        {
            return source.Variance().Sqrt();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double StandardDeviation(this Memory<Double> source)
        {
            return StandardDeviation(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double StandardDeviation(this Span<Double> source)
        {
            return StandardDeviation((ReadOnlySpan<Double>) source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double StandardDeviation(this ReadOnlyMemory<Double> source)
        {
            return StandardDeviation(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double StandardDeviation(this ReadOnlySpan<Double> source)
        {
            return source.Variance().Sqrt();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal StandardDeviation(this Memory<Decimal> source)
        {
            return StandardDeviation(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal StandardDeviation(this Span<Decimal> source)
        {
            return StandardDeviation((ReadOnlySpan<Decimal>) source);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal StandardDeviation(this ReadOnlyMemory<Decimal> source)
        {
            return StandardDeviation(source.Span);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal StandardDeviation(this ReadOnlySpan<Decimal> source)
        {
            return source.Variance().Sqrt();
        }
    }
}

