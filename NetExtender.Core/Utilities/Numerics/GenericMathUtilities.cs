// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace NetExtender.Utilities.Numerics
{
    [SuppressMessage("ReSharper", "RedundantOverflowCheckingContext")]
    public static partial class MathUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Char Min(this Char value, Char compare)
        {
            return value <= compare ? value : compare;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Min(ref Char max, ref Char min)
        {
            if (max > min)
            {
                (max, min) = (min, max);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SByte Min(this SByte value, SByte compare)
        {
            return value <= compare ? value : compare;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Min(ref SByte max, ref SByte min)
        {
            if (max > min)
            {
                (max, min) = (min, max);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Byte Min(this Byte value, Byte compare)
        {
            return value <= compare ? value : compare;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Min(ref Byte max, ref Byte min)
        {
            if (max > min)
            {
                (max, min) = (min, max);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int16 Min(this Int16 value, Int16 compare)
        {
            return value <= compare ? value : compare;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Min(ref Int16 max, ref Int16 min)
        {
            if (max > min)
            {
                (max, min) = (min, max);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt16 Min(this UInt16 value, UInt16 compare)
        {
            return value <= compare ? value : compare;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Min(ref UInt16 max, ref UInt16 min)
        {
            if (max > min)
            {
                (max, min) = (min, max);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Min(this Int32 value, Int32 compare)
        {
            return value <= compare ? value : compare;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Min(ref Int32 max, ref Int32 min)
        {
            if (max > min)
            {
                (max, min) = (min, max);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 Min(this UInt32 value, UInt32 compare)
        {
            return value <= compare ? value : compare;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Min(ref UInt32 max, ref UInt32 min)
        {
            if (max > min)
            {
                (max, min) = (min, max);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 Min(this Int64 value, Int64 compare)
        {
            return value <= compare ? value : compare;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Min(ref Int64 max, ref Int64 min)
        {
            if (max > min)
            {
                (max, min) = (min, max);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 Min(this UInt64 value, UInt64 compare)
        {
            return value <= compare ? value : compare;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Min(ref UInt64 max, ref UInt64 min)
        {
            if (max > min)
            {
                (max, min) = (min, max);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Min(this Single value, Single compare)
        {
            return value <= compare ? value : compare;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Min(ref Single max, ref Single min)
        {
            if (max > min)
            {
                (max, min) = (min, max);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Min(this Double value, Double compare)
        {
            return value <= compare ? value : compare;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Min(ref Double max, ref Double min)
        {
            if (max > min)
            {
                (max, min) = (min, max);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Min(this Decimal value, Decimal compare)
        {
            return value <= compare ? value : compare;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Min(ref Decimal max, ref Decimal min)
        {
            if (max > min)
            {
                (max, min) = (min, max);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInteger Min(this BigInteger value, BigInteger compare)
        {
            return value <= compare ? value : compare;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Min(ref BigInteger max, ref BigInteger min)
        {
            if (max > min)
            {
                (max, min) = (min, max);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Char Max(this Char value, Char compare)
        {
            return value >= compare ? value : compare;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Max(ref Char max, ref Char min)
        {
            if (max < min)
            {
                (max, min) = (min, max);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SByte Max(this SByte value, SByte compare)
        {
            return value >= compare ? value : compare;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Max(ref SByte max, ref SByte min)
        {
            if (max < min)
            {
                (max, min) = (min, max);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Byte Max(this Byte value, Byte compare)
        {
            return value >= compare ? value : compare;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Max(ref Byte max, ref Byte min)
        {
            if (max < min)
            {
                (max, min) = (min, max);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int16 Max(this Int16 value, Int16 compare)
        {
            return value >= compare ? value : compare;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Max(ref Int16 max, ref Int16 min)
        {
            if (max < min)
            {
                (max, min) = (min, max);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt16 Max(this UInt16 value, UInt16 compare)
        {
            return value >= compare ? value : compare;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Max(ref UInt16 max, ref UInt16 min)
        {
            if (max < min)
            {
                (max, min) = (min, max);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Max(this Int32 value, Int32 compare)
        {
            return value >= compare ? value : compare;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Max(ref Int32 max, ref Int32 min)
        {
            if (max < min)
            {
                (max, min) = (min, max);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 Max(this UInt32 value, UInt32 compare)
        {
            return value >= compare ? value : compare;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Max(ref UInt32 max, ref UInt32 min)
        {
            if (max < min)
            {
                (max, min) = (min, max);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 Max(this Int64 value, Int64 compare)
        {
            return value >= compare ? value : compare;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Max(ref Int64 max, ref Int64 min)
        {
            if (max < min)
            {
                (max, min) = (min, max);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 Max(this UInt64 value, UInt64 compare)
        {
            return value >= compare ? value : compare;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Max(ref UInt64 max, ref UInt64 min)
        {
            if (max < min)
            {
                (max, min) = (min, max);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Max(this Single value, Single compare)
        {
            return value >= compare ? value : compare;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Max(ref Single max, ref Single min)
        {
            if (max < min)
            {
                (max, min) = (min, max);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Max(this Double value, Double compare)
        {
            return value >= compare ? value : compare;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Max(ref Double max, ref Double min)
        {
            if (max < min)
            {
                (max, min) = (min, max);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Max(this Decimal value, Decimal compare)
        {
            return value >= compare ? value : compare;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Max(ref Decimal max, ref Decimal min)
        {
            if (max < min)
            {
                (max, min) = (min, max);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInteger Max(this BigInteger value, BigInteger compare)
        {
            return value >= compare ? value : compare;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Max(ref BigInteger max, ref BigInteger min)
        {
            if (max < min)
            {
                (max, min) = (min, max);
            }
        }

        public static BigInteger Factorial(this SByte value)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            return Factorial((UInt32)value);
        }

        public static BigInteger Factorial(this Byte value)
        {
            return Factorial((UInt32)value);
        }

        public static BigInteger Factorial(this Int16 value)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            return Factorial((UInt32)value);
        }

        public static BigInteger Factorial(this UInt16 value)
        {
            return Factorial((UInt32)value);
        }

        public static BigInteger Factorial(this Int32 value)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            return Factorial((UInt32)value);
        }

        public static BigInteger Factorial(this Int64 value)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (value > UInt32.MaxValue)
            {
                throw new ArgumentOutOfRangeException();
            }

            return Factorial((UInt32)value);
        }

        public static BigInteger Factorial(this UInt64 value)
        {
            if (value > UInt32.MaxValue)
            {
                throw new ArgumentOutOfRangeException();
            }

            return Factorial((UInt32)value);
        }

        public static BigInteger Factorial(this BigInteger value)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (value > UInt32.MaxValue)
            {
                throw new ArgumentOutOfRangeException();
            }

            return Factorial((UInt32)value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<SByte> Range(SByte stop)
        {
            return Range(default, stop);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<SByte> Range(SByte start, SByte stop)
        {
            return Range(start, stop, (SByte) 1);
        }

        public static IEnumerable<SByte> Range(SByte start, SByte stop, SByte step)
        {
            if (step == 0)
            {
                throw new ArgumentException("Step cannot be equals zero.");
            }

            if (start < stop && step > 0)
            {
                stop = (SByte) Math.Min(stop, SByte.MaxValue - step);

                SByte i;
                for (i = start; i < stop; i += step)
                {
                    yield return i;
                }

                if (i >= SByte.MaxValue - step)
                {
                    yield return i;
                }
            }
            else if (start > stop && step < 0)
            {
                stop = (SByte) Math.Max(stop, SByte.MinValue - step);

                SByte i;
                for (i = start; i > stop; i += step)
                {
                    yield return i;
                }

                if (i <= SByte.MinValue - step)
                {
                    yield return i;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Byte> Range(Byte stop)
        {
            return Range(default, stop);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Byte> Range(Byte start, Byte stop)
        {
            return Range(start, stop, (Byte) 1);
        }

        public static IEnumerable<Byte> Range(Byte start, Byte stop, Byte step)
        {
            if (step == 0)
            {
                throw new ArgumentException("Step cannot be equals zero.");
            }

            stop = (Byte) Math.Min(stop, Byte.MaxValue - step);

            Byte i;
            for (i = start; i < stop; i += step)
            {
                yield return i;
            }

            if (i >= Byte.MaxValue - step)
            {
                yield return i;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Int16> Range(Int16 stop)
        {
            return Range(default, stop);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Int16> Range(Int16 start, Int16 stop)
        {
            return Range(start, stop, (Int16) 1);
        }

        public static IEnumerable<Int16> Range(Int16 start, Int16 stop, Int16 step)
        {
            if (step == 0)
            {
                throw new ArgumentException("Step cannot be equals zero.");
            }

            if (start < stop && step > 0)
            {
                stop = (Int16) Math.Min(stop, Int16.MaxValue - step);

                Int16 i;
                for (i = start; i < stop; i += step)
                {
                    yield return i;
                }

                if (i >= Int16.MaxValue - step)
                {
                    yield return i;
                }
            }
            else if (start > stop && step < 0)
            {
                stop = (Int16) Math.Max(stop, Int16.MinValue - step);

                Int16 i;
                for (i = start; i > stop; i += step)
                {
                    yield return i;
                }

                if (i <= Int16.MinValue - step)
                {
                    yield return i;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<UInt16> Range(UInt16 stop)
        {
            return Range(default, stop);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<UInt16> Range(UInt16 start, UInt16 stop)
        {
            return Range(start, stop, (UInt16) 1);
        }

        public static IEnumerable<UInt16> Range(UInt16 start, UInt16 stop, UInt16 step)
        {
            if (step == 0)
            {
                throw new ArgumentException("Step cannot be equals zero.");
            }

            stop = (UInt16) Math.Min(stop, UInt16.MaxValue - step);

            UInt16 i;
            for (i = start; i < stop; i += step)
            {
                yield return i;
            }

            if (i >= UInt16.MaxValue - step)
            {
                yield return i;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Int32> Range(Int32 stop)
        {
            return Range(default, stop);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Int32> Range(Int32 start, Int32 stop)
        {
            return Range(start, stop, 1);
        }

        public static IEnumerable<Int32> Range(Int32 start, Int32 stop, Int32 step)
        {
            if (step == 0)
            {
                throw new ArgumentException("Step cannot be equals zero.");
            }

            if (start < stop && step > 0)
            {
                stop = Math.Min(stop, Int32.MaxValue - step);

                Int32 i;
                for (i = start; i < stop; i += step)
                {
                    yield return i;
                }

                if (i >= Int32.MaxValue - step)
                {
                    yield return i;
                }
            }
            else if (start > stop && step < 0)
            {
                stop = Math.Max(stop, Int32.MinValue - step);

                Int32 i;
                for (i = start; i > stop; i += step)
                {
                    yield return i;
                }

                if (i <= Int32.MinValue - step)
                {
                    yield return i;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<UInt32> Range(UInt32 stop)
        {
            return Range(default, stop);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<UInt32> Range(UInt32 start, UInt32 stop)
        {
            return Range(start, stop, 1);
        }

        public static IEnumerable<UInt32> Range(UInt32 start, UInt32 stop, UInt32 step)
        {
            if (step == 0)
            {
                throw new ArgumentException("Step cannot be equals zero.");
            }

            stop = Math.Min(stop, UInt32.MaxValue - step);

            UInt32 i;
            for (i = start; i < stop; i += step)
            {
                yield return i;
            }

            if (i >= UInt32.MaxValue - step)
            {
                yield return i;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Int64> Range(Int64 stop)
        {
            return Range(default, stop);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Int64> Range(Int64 start, Int64 stop)
        {
            return Range(start, stop, 1);
        }

        public static IEnumerable<Int64> Range(Int64 start, Int64 stop, Int64 step)
        {
            if (step == 0)
            {
                throw new ArgumentException("Step cannot be equals zero.");
            }

            if (start < stop && step > 0)
            {
                stop = Math.Min(stop, Int64.MaxValue - step);

                Int64 i;
                for (i = start; i < stop; i += step)
                {
                    yield return i;
                }

                if (i >= Int64.MaxValue - step)
                {
                    yield return i;
                }
            }
            else if (start > stop && step < 0)
            {
                stop = Math.Max(stop, Int64.MinValue - step);

                Int64 i;
                for (i = start; i > stop; i += step)
                {
                    yield return i;
                }

                if (i <= Int64.MinValue - step)
                {
                    yield return i;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<UInt64> Range(UInt64 stop)
        {
            return Range(default, stop);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<UInt64> Range(UInt64 start, UInt64 stop)
        {
            return Range(start, stop, 1);
        }

        public static IEnumerable<UInt64> Range(UInt64 start, UInt64 stop, UInt64 step)
        {
            if (step == 0)
            {
                throw new ArgumentException("Step cannot be equals zero.");
            }

            stop = Math.Min(stop, UInt64.MaxValue - step);

            UInt64 i;
            for (i = start; i < stop; i += step)
            {
                yield return i;
            }

            if (i >= UInt64.MaxValue - step)
            {
                yield return i;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<SByte> RangeInclude(SByte stop)
        {
            return RangeInclude(default, stop);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<SByte> RangeInclude(SByte start, SByte stop)
        {
            return RangeInclude(start, stop, (SByte) 1);
        }

        public static IEnumerable<SByte> RangeInclude(SByte start, SByte stop, SByte step)
        {
            if (step == 0)
            {
                throw new ArgumentException("Step cannot be equals zero.");
            }

            if (start < stop && step > 0)
            {
                stop = (SByte) Math.Min(stop, SByte.MaxValue - step + 1);

                SByte i;
                for (i = start; i < stop; i += step)
                {
                    yield return i;
                }

                yield return i;
            }
            else if (start > stop && step < 0)
            {
                stop = (SByte) Math.Max(stop, SByte.MinValue - step - 1);

                SByte i;
                for (i = start; i > stop; i += step)
                {
                    yield return i;
                }

                yield return i;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Byte> RangeInclude(Byte stop)
        {
            return RangeInclude(default, stop);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Byte> RangeInclude(Byte start, Byte stop)
        {
            return RangeInclude(start, stop, (Byte) 1);
        }

        public static IEnumerable<Byte> RangeInclude(Byte start, Byte stop, Byte step)
        {
            if (step == 0)
            {
                throw new ArgumentException("Step cannot be equals zero.");
            }

            stop = (Byte) Math.Min(stop, Byte.MaxValue - step + 1);

            Byte i;
            for (i = start; i < stop; i += step)
            {
                yield return i;
            }

            yield return i;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Int16> RangeInclude(Int16 stop)
        {
            return RangeInclude(default, stop);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Int16> RangeInclude(Int16 start, Int16 stop)
        {
            return RangeInclude(start, stop, (Int16) 1);
        }

        public static IEnumerable<Int16> RangeInclude(Int16 start, Int16 stop, Int16 step)
        {
            if (step == 0)
            {
                throw new ArgumentException("Step cannot be equals zero.");
            }

            if (start < stop && step > 0)
            {
                stop = (Int16) Math.Min(stop, Int16.MaxValue - step + 1);

                Int16 i;
                for (i = start; i < stop; i += step)
                {
                    yield return i;
                }

                yield return i;
            }
            else if (start > stop && step < 0)
            {
                stop = (Int16) Math.Max(stop, Int16.MinValue - step - 1);

                Int16 i;
                for (i = start; i > stop; i += step)
                {
                    yield return i;
                }

                yield return i;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<UInt16> RangeInclude(UInt16 stop)
        {
            return RangeInclude(default, stop);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<UInt16> RangeInclude(UInt16 start, UInt16 stop)
        {
            return RangeInclude(start, stop, (UInt16) 1);
        }

        public static IEnumerable<UInt16> RangeInclude(UInt16 start, UInt16 stop, UInt16 step)
        {
            if (step == 0)
            {
                throw new ArgumentException("Step cannot be equals zero.");
            }

            stop = (UInt16) Math.Min(stop, UInt16.MaxValue - step + 1);

            UInt16 i;
            for (i = start; i < stop; i += step)
            {
                yield return i;
            }

            yield return i;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Int32> RangeInclude(Int32 stop)
        {
            return RangeInclude(default, stop);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Int32> RangeInclude(Int32 start, Int32 stop)
        {
            return RangeInclude(start, stop, 1);
        }

        public static IEnumerable<Int32> RangeInclude(Int32 start, Int32 stop, Int32 step)
        {
            if (step == 0)
            {
                throw new ArgumentException("Step cannot be equals zero.");
            }

            if (start < stop && step > 0)
            {
                stop = Math.Min(stop, Int32.MaxValue - step + 1);

                Int32 i;
                for (i = start; i < stop; i += step)
                {
                    yield return i;
                }

                yield return i;
            }
            else if (start > stop && step < 0)
            {
                stop = Math.Max(stop, Int32.MinValue - step - 1);

                Int32 i;
                for (i = start; i > stop; i += step)
                {
                    yield return i;
                }

                yield return i;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<UInt32> RangeInclude(UInt32 stop)
        {
            return RangeInclude(default, stop);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<UInt32> RangeInclude(UInt32 start, UInt32 stop)
        {
            return RangeInclude(start, stop, 1);
        }

        public static IEnumerable<UInt32> RangeInclude(UInt32 start, UInt32 stop, UInt32 step)
        {
            if (step == 0)
            {
                throw new ArgumentException("Step cannot be equals zero.");
            }

            stop = Math.Min(stop, UInt32.MaxValue - step + 1);

            UInt32 i;
            for (i = start; i < stop; i += step)
            {
                yield return i;
            }

            yield return i;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Int64> RangeInclude(Int64 stop)
        {
            return RangeInclude(default, stop);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<Int64> RangeInclude(Int64 start, Int64 stop)
        {
            return RangeInclude(start, stop, 1);
        }

        public static IEnumerable<Int64> RangeInclude(Int64 start, Int64 stop, Int64 step)
        {
            if (step == 0)
            {
                throw new ArgumentException("Step cannot be equals zero.");
            }

            if (start < stop && step > 0)
            {
                stop = Math.Min(stop, Int64.MaxValue - step + 1);

                Int64 i;
                for (i = start; i < stop; i += step)
                {
                    yield return i;
                }

                yield return i;
            }
            else if (start > stop && step < 0)
            {
                stop = Math.Max(stop, Int64.MinValue - step - 1);

                Int64 i;
                for (i = start; i > stop; i += step)
                {
                    yield return i;
                }

                yield return i;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<UInt64> RangeInclude(UInt64 stop)
        {
            return RangeInclude(default, stop);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<UInt64> RangeInclude(UInt64 start, UInt64 stop)
        {
            return RangeInclude(start, stop, 1);
        }

        public static IEnumerable<UInt64> RangeInclude(UInt64 start, UInt64 stop, UInt64 step)
        {
            if (step == 0)
            {
                throw new ArgumentException("Step cannot be equals zero.");
            }

            stop = Math.Min(stop, UInt64.MaxValue - step + 1);

            UInt64 i;
            for (i = start; i < stop; i += step)
            {
                yield return i;
            }

            yield return i;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Char ToRange(this Char value)
        {
            return Clamp(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Char ToRange(this Char value, Boolean looped)
        {
            return Clamp(value, looped);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Char ToRange(this Char value, Char maximum)
        {
            return Clamp(value, maximum);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Char ToRange(this Char value, Char maximum, Boolean looped)
        {
            return Clamp(value, maximum, looped);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Char ToRange(this Char value, Char minimum, Char maximum)
        {
            return Clamp(value, minimum, maximum);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Char ToRange(this Char value, Char minimum, Char maximum, Boolean looped)
        {
            return Clamp(value, minimum, maximum, looped);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SByte ToRange(this SByte value)
        {
            return Clamp(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SByte ToRange(this SByte value, Boolean looped)
        {
            return Clamp(value, looped);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SByte ToRange(this SByte value, SByte maximum)
        {
            return Clamp(value, maximum);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SByte ToRange(this SByte value, SByte maximum, Boolean looped)
        {
            return Clamp(value, maximum, looped);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SByte ToRange(this SByte value, SByte minimum, SByte maximum)
        {
            return Clamp(value, minimum, maximum);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SByte ToRange(this SByte value, SByte minimum, SByte maximum, Boolean looped)
        {
            return Clamp(value, minimum, maximum, looped);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Byte ToRange(this Byte value)
        {
            return Clamp(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Byte ToRange(this Byte value, Boolean looped)
        {
            return Clamp(value, looped);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Byte ToRange(this Byte value, Byte maximum)
        {
            return Clamp(value, maximum);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Byte ToRange(this Byte value, Byte maximum, Boolean looped)
        {
            return Clamp(value, maximum, looped);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Byte ToRange(this Byte value, Byte minimum, Byte maximum)
        {
            return Clamp(value, minimum, maximum);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Byte ToRange(this Byte value, Byte minimum, Byte maximum, Boolean looped)
        {
            return Clamp(value, minimum, maximum, looped);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int16 ToRange(this Int16 value)
        {
            return Clamp(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int16 ToRange(this Int16 value, Boolean looped)
        {
            return Clamp(value, looped);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int16 ToRange(this Int16 value, Int16 maximum)
        {
            return Clamp(value, maximum);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int16 ToRange(this Int16 value, Int16 maximum, Boolean looped)
        {
            return Clamp(value, maximum, looped);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int16 ToRange(this Int16 value, Int16 minimum, Int16 maximum)
        {
            return Clamp(value, minimum, maximum);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int16 ToRange(this Int16 value, Int16 minimum, Int16 maximum, Boolean looped)
        {
            return Clamp(value, minimum, maximum, looped);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt16 ToRange(this UInt16 value)
        {
            return Clamp(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt16 ToRange(this UInt16 value, Boolean looped)
        {
            return Clamp(value, looped);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt16 ToRange(this UInt16 value, UInt16 maximum)
        {
            return Clamp(value, maximum);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt16 ToRange(this UInt16 value, UInt16 maximum, Boolean looped)
        {
            return Clamp(value, maximum, looped);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt16 ToRange(this UInt16 value, UInt16 minimum, UInt16 maximum)
        {
            return Clamp(value, minimum, maximum);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt16 ToRange(this UInt16 value, UInt16 minimum, UInt16 maximum, Boolean looped)
        {
            return Clamp(value, minimum, maximum, looped);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 ToRange(this Int32 value)
        {
            return Clamp(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 ToRange(this Int32 value, Boolean looped)
        {
            return Clamp(value, looped);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 ToRange(this Int32 value, Int32 maximum)
        {
            return Clamp(value, maximum);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 ToRange(this Int32 value, Int32 maximum, Boolean looped)
        {
            return Clamp(value, maximum, looped);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 ToRange(this Int32 value, Int32 minimum, Int32 maximum)
        {
            return Clamp(value, minimum, maximum);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 ToRange(this Int32 value, Int32 minimum, Int32 maximum, Boolean looped)
        {
            return Clamp(value, minimum, maximum, looped);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 ToRange(this UInt32 value)
        {
            return Clamp(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 ToRange(this UInt32 value, Boolean looped)
        {
            return Clamp(value, looped);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 ToRange(this UInt32 value, UInt32 maximum)
        {
            return Clamp(value, maximum);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 ToRange(this UInt32 value, UInt32 maximum, Boolean looped)
        {
            return Clamp(value, maximum, looped);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 ToRange(this UInt32 value, UInt32 minimum, UInt32 maximum)
        {
            return Clamp(value, minimum, maximum);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 ToRange(this UInt32 value, UInt32 minimum, UInt32 maximum, Boolean looped)
        {
            return Clamp(value, minimum, maximum, looped);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 ToRange(this Int64 value)
        {
            return Clamp(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 ToRange(this Int64 value, Boolean looped)
        {
            return Clamp(value, looped);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 ToRange(this Int64 value, Int64 maximum)
        {
            return Clamp(value, maximum);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 ToRange(this Int64 value, Int64 maximum, Boolean looped)
        {
            return Clamp(value, maximum, looped);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 ToRange(this Int64 value, Int64 minimum, Int64 maximum)
        {
            return Clamp(value, minimum, maximum);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 ToRange(this Int64 value, Int64 minimum, Int64 maximum, Boolean looped)
        {
            return Clamp(value, minimum, maximum, looped);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 ToRange(this UInt64 value)
        {
            return Clamp(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 ToRange(this UInt64 value, Boolean looped)
        {
            return Clamp(value, looped);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 ToRange(this UInt64 value, UInt64 maximum)
        {
            return Clamp(value, maximum);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 ToRange(this UInt64 value, UInt64 maximum, Boolean looped)
        {
            return Clamp(value, maximum, looped);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 ToRange(this UInt64 value, UInt64 minimum, UInt64 maximum)
        {
            return Clamp(value, minimum, maximum);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 ToRange(this UInt64 value, UInt64 minimum, UInt64 maximum, Boolean looped)
        {
            return Clamp(value, minimum, maximum, looped);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single ToRange(this Single value)
        {
            return Clamp(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single ToRange(this Single value, Boolean looped)
        {
            return Clamp(value, looped);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single ToRange(this Single value, Single maximum)
        {
            return Clamp(value, maximum);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single ToRange(this Single value, Single maximum, Boolean looped)
        {
            return Clamp(value, maximum, looped);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single ToRange(this Single value, Single minimum, Single maximum)
        {
            return Clamp(value, minimum, maximum);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single ToRange(this Single value, Single minimum, Single maximum, Boolean looped)
        {
            return Clamp(value, minimum, maximum, looped);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double ToRange(this Double value)
        {
            return Clamp(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double ToRange(this Double value, Boolean looped)
        {
            return Clamp(value, looped);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double ToRange(this Double value, Double maximum)
        {
            return Clamp(value, maximum);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double ToRange(this Double value, Double maximum, Boolean looped)
        {
            return Clamp(value, maximum, looped);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double ToRange(this Double value, Double minimum, Double maximum)
        {
            return Clamp(value, minimum, maximum);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double ToRange(this Double value, Double minimum, Double maximum, Boolean looped)
        {
            return Clamp(value, minimum, maximum, looped);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal ToRange(this Decimal value)
        {
            return Clamp(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal ToRange(this Decimal value, Boolean looped)
        {
            return Clamp(value, looped);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal ToRange(this Decimal value, Decimal maximum)
        {
            return Clamp(value, maximum);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal ToRange(this Decimal value, Decimal maximum, Boolean looped)
        {
            return Clamp(value, maximum, looped);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal ToRange(this Decimal value, Decimal minimum, Decimal maximum)
        {
            return Clamp(value, minimum, maximum);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal ToRange(this Decimal value, Decimal minimum, Decimal maximum, Boolean looped)
        {
            return Clamp(value, minimum, maximum, looped);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInteger ToRange(this BigInteger value)
        {
            return Clamp(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInteger ToRange(this BigInteger value, Boolean looped)
        {
            return Clamp(value, looped);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInteger ToRange(this BigInteger value, BigInteger maximum)
        {
            return Clamp(value, maximum);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInteger ToRange(this BigInteger value, BigInteger maximum, Boolean looped)
        {
            return Clamp(value, maximum, looped);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInteger ToRange(this BigInteger value, BigInteger minimum, BigInteger maximum)
        {
            return Clamp(value, minimum, maximum);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInteger ToRange(this BigInteger value, BigInteger minimum, BigInteger maximum, Boolean looped)
        {
            return Clamp(value, minimum, maximum, looped);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean InRange(this Char value)
        {
            return InRange(value, MathPositionType.Both);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean InRange(this Char value, MathPositionType comparison)
        {
            return InRange(value, default, Char.MaxValue, comparison);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean InRange(this Char value, Char maximum)
        {
            return InRange(value, maximum, MathPositionType.Both);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean InRange(this Char value, Char maximum, MathPositionType comparison)
        {
            return InRange(value, default, maximum, comparison);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean InRange(this Char value, Char minimum, Char maximum)
        {
            return InRange(value, minimum, maximum, MathPositionType.Both);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean InRange(this Char value, Char minimum, Char maximum, MathPositionType comparison)
        {
            return comparison switch
            {
                MathPositionType.None => value > minimum && value < maximum,
                MathPositionType.Left => value >= minimum && value < maximum,
                MathPositionType.Right => value > minimum && value <= maximum,
                MathPositionType.Both => value >= minimum && value <= maximum,
                _ => throw new NotSupportedException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean InRange(this SByte value)
        {
            return InRange(value, MathPositionType.Both);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean InRange(this SByte value, MathPositionType comparison)
        {
            return InRange(value, default, SByte.MaxValue, comparison);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean InRange(this SByte value, SByte maximum)
        {
            return InRange(value, maximum, MathPositionType.Both);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean InRange(this SByte value, SByte maximum, MathPositionType comparison)
        {
            return InRange(value, default, maximum, comparison);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean InRange(this SByte value, SByte minimum, SByte maximum)
        {
            return InRange(value, minimum, maximum, MathPositionType.Both);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean InRange(this SByte value, SByte minimum, SByte maximum, MathPositionType comparison)
        {
            return comparison switch
            {
                MathPositionType.None => value > minimum && value < maximum,
                MathPositionType.Left => value >= minimum && value < maximum,
                MathPositionType.Right => value > minimum && value <= maximum,
                MathPositionType.Both => value >= minimum && value <= maximum,
                _ => throw new NotSupportedException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean InRange(this Byte value)
        {
            return InRange(value, MathPositionType.Both);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean InRange(this Byte value, MathPositionType comparison)
        {
            return InRange(value, default, Byte.MaxValue, comparison);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean InRange(this Byte value, Byte maximum)
        {
            return InRange(value, maximum, MathPositionType.Both);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean InRange(this Byte value, Byte maximum, MathPositionType comparison)
        {
            return InRange(value, default, maximum, comparison);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean InRange(this Byte value, Byte minimum, Byte maximum)
        {
            return InRange(value, minimum, maximum, MathPositionType.Both);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean InRange(this Byte value, Byte minimum, Byte maximum, MathPositionType comparison)
        {
            return comparison switch
            {
                MathPositionType.None => value > minimum && value < maximum,
                MathPositionType.Left => value >= minimum && value < maximum,
                MathPositionType.Right => value > minimum && value <= maximum,
                MathPositionType.Both => value >= minimum && value <= maximum,
                _ => throw new NotSupportedException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean InRange(this Int16 value)
        {
            return InRange(value, MathPositionType.Both);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean InRange(this Int16 value, MathPositionType comparison)
        {
            return InRange(value, default, Int16.MaxValue, comparison);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean InRange(this Int16 value, Int16 maximum)
        {
            return InRange(value, maximum, MathPositionType.Both);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean InRange(this Int16 value, Int16 maximum, MathPositionType comparison)
        {
            return InRange(value, default, maximum, comparison);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean InRange(this Int16 value, Int16 minimum, Int16 maximum)
        {
            return InRange(value, minimum, maximum, MathPositionType.Both);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean InRange(this Int16 value, Int16 minimum, Int16 maximum, MathPositionType comparison)
        {
            return comparison switch
            {
                MathPositionType.None => value > minimum && value < maximum,
                MathPositionType.Left => value >= minimum && value < maximum,
                MathPositionType.Right => value > minimum && value <= maximum,
                MathPositionType.Both => value >= minimum && value <= maximum,
                _ => throw new NotSupportedException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean InRange(this UInt16 value)
        {
            return InRange(value, MathPositionType.Both);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean InRange(this UInt16 value, MathPositionType comparison)
        {
            return InRange(value, default, UInt16.MaxValue, comparison);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean InRange(this UInt16 value, UInt16 maximum)
        {
            return InRange(value, maximum, MathPositionType.Both);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean InRange(this UInt16 value, UInt16 maximum, MathPositionType comparison)
        {
            return InRange(value, default, maximum, comparison);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean InRange(this UInt16 value, UInt16 minimum, UInt16 maximum)
        {
            return InRange(value, minimum, maximum, MathPositionType.Both);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean InRange(this UInt16 value, UInt16 minimum, UInt16 maximum, MathPositionType comparison)
        {
            return comparison switch
            {
                MathPositionType.None => value > minimum && value < maximum,
                MathPositionType.Left => value >= minimum && value < maximum,
                MathPositionType.Right => value > minimum && value <= maximum,
                MathPositionType.Both => value >= minimum && value <= maximum,
                _ => throw new NotSupportedException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean InRange(this Int32 value)
        {
            return InRange(value, MathPositionType.Both);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean InRange(this Int32 value, MathPositionType comparison)
        {
            return InRange(value, default, Int32.MaxValue, comparison);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean InRange(this Int32 value, Int32 maximum)
        {
            return InRange(value, maximum, MathPositionType.Both);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean InRange(this Int32 value, Int32 maximum, MathPositionType comparison)
        {
            return InRange(value, default, maximum, comparison);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean InRange(this Int32 value, Int32 minimum, Int32 maximum)
        {
            return InRange(value, minimum, maximum, MathPositionType.Both);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean InRange(this Int32 value, Int32 minimum, Int32 maximum, MathPositionType comparison)
        {
            return comparison switch
            {
                MathPositionType.None => value > minimum && value < maximum,
                MathPositionType.Left => value >= minimum && value < maximum,
                MathPositionType.Right => value > minimum && value <= maximum,
                MathPositionType.Both => value >= minimum && value <= maximum,
                _ => throw new NotSupportedException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean InRange(this UInt32 value)
        {
            return InRange(value, MathPositionType.Both);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean InRange(this UInt32 value, MathPositionType comparison)
        {
            return InRange(value, default, UInt32.MaxValue, comparison);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean InRange(this UInt32 value, UInt32 maximum)
        {
            return InRange(value, maximum, MathPositionType.Both);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean InRange(this UInt32 value, UInt32 maximum, MathPositionType comparison)
        {
            return InRange(value, default, maximum, comparison);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean InRange(this UInt32 value, UInt32 minimum, UInt32 maximum)
        {
            return InRange(value, minimum, maximum, MathPositionType.Both);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean InRange(this UInt32 value, UInt32 minimum, UInt32 maximum, MathPositionType comparison)
        {
            return comparison switch
            {
                MathPositionType.None => value > minimum && value < maximum,
                MathPositionType.Left => value >= minimum && value < maximum,
                MathPositionType.Right => value > minimum && value <= maximum,
                MathPositionType.Both => value >= minimum && value <= maximum,
                _ => throw new NotSupportedException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean InRange(this Int64 value)
        {
            return InRange(value, MathPositionType.Both);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean InRange(this Int64 value, MathPositionType comparison)
        {
            return InRange(value, default, Int64.MaxValue, comparison);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean InRange(this Int64 value, Int64 maximum)
        {
            return InRange(value, maximum, MathPositionType.Both);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean InRange(this Int64 value, Int64 maximum, MathPositionType comparison)
        {
            return InRange(value, default, maximum, comparison);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean InRange(this Int64 value, Int64 minimum, Int64 maximum)
        {
            return InRange(value, minimum, maximum, MathPositionType.Both);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean InRange(this Int64 value, Int64 minimum, Int64 maximum, MathPositionType comparison)
        {
            return comparison switch
            {
                MathPositionType.None => value > minimum && value < maximum,
                MathPositionType.Left => value >= minimum && value < maximum,
                MathPositionType.Right => value > minimum && value <= maximum,
                MathPositionType.Both => value >= minimum && value <= maximum,
                _ => throw new NotSupportedException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean InRange(this UInt64 value)
        {
            return InRange(value, MathPositionType.Both);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean InRange(this UInt64 value, MathPositionType comparison)
        {
            return InRange(value, default, UInt64.MaxValue, comparison);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean InRange(this UInt64 value, UInt64 maximum)
        {
            return InRange(value, maximum, MathPositionType.Both);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean InRange(this UInt64 value, UInt64 maximum, MathPositionType comparison)
        {
            return InRange(value, default, maximum, comparison);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean InRange(this UInt64 value, UInt64 minimum, UInt64 maximum)
        {
            return InRange(value, minimum, maximum, MathPositionType.Both);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean InRange(this UInt64 value, UInt64 minimum, UInt64 maximum, MathPositionType comparison)
        {
            return comparison switch
            {
                MathPositionType.None => value > minimum && value < maximum,
                MathPositionType.Left => value >= minimum && value < maximum,
                MathPositionType.Right => value > minimum && value <= maximum,
                MathPositionType.Both => value >= minimum && value <= maximum,
                _ => throw new NotSupportedException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean InRange(this Single value)
        {
            return InRange(value, MathPositionType.Both);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean InRange(this Single value, MathPositionType comparison)
        {
            return InRange(value, default, Single.MaxValue, comparison);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean InRange(this Single value, Single maximum)
        {
            return InRange(value, maximum, MathPositionType.Both);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean InRange(this Single value, Single maximum, MathPositionType comparison)
        {
            return InRange(value, default, maximum, comparison);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean InRange(this Single value, Single minimum, Single maximum)
        {
            return InRange(value, minimum, maximum, MathPositionType.Both);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean InRange(this Single value, Single minimum, Single maximum, MathPositionType comparison)
        {
            return comparison switch
            {
                MathPositionType.None => value > minimum && value < maximum,
                MathPositionType.Left => value >= minimum && value < maximum,
                MathPositionType.Right => value > minimum && value <= maximum,
                MathPositionType.Both => value >= minimum && value <= maximum,
                _ => throw new NotSupportedException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean InRange(this Double value)
        {
            return InRange(value, MathPositionType.Both);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean InRange(this Double value, MathPositionType comparison)
        {
            return InRange(value, default, Double.MaxValue, comparison);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean InRange(this Double value, Double maximum)
        {
            return InRange(value, maximum, MathPositionType.Both);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean InRange(this Double value, Double maximum, MathPositionType comparison)
        {
            return InRange(value, default, maximum, comparison);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean InRange(this Double value, Double minimum, Double maximum)
        {
            return InRange(value, minimum, maximum, MathPositionType.Both);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean InRange(this Double value, Double minimum, Double maximum, MathPositionType comparison)
        {
            return comparison switch
            {
                MathPositionType.None => value > minimum && value < maximum,
                MathPositionType.Left => value >= minimum && value < maximum,
                MathPositionType.Right => value > minimum && value <= maximum,
                MathPositionType.Both => value >= minimum && value <= maximum,
                _ => throw new NotSupportedException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean InRange(this Decimal value)
        {
            return InRange(value, MathPositionType.Both);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean InRange(this Decimal value, MathPositionType comparison)
        {
            return InRange(value, default, Decimal.MaxValue, comparison);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean InRange(this Decimal value, Decimal maximum)
        {
            return InRange(value, maximum, MathPositionType.Both);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean InRange(this Decimal value, Decimal maximum, MathPositionType comparison)
        {
            return InRange(value, default, maximum, comparison);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean InRange(this Decimal value, Decimal minimum, Decimal maximum)
        {
            return InRange(value, minimum, maximum, MathPositionType.Both);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean InRange(this Decimal value, Decimal minimum, Decimal maximum, MathPositionType comparison)
        {
            return comparison switch
            {
                MathPositionType.None => value > minimum && value < maximum,
                MathPositionType.Left => value >= minimum && value < maximum,
                MathPositionType.Right => value > minimum && value <= maximum,
                MathPositionType.Both => value >= minimum && value <= maximum,
                _ => throw new NotSupportedException()
            };
        }

        [SuppressMessage("ReSharper", "UnusedParameter.Global")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsPositive(this Char value)
        {
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsPositive(this SByte value)
        {
            return value >= 0;
        }

        [SuppressMessage("ReSharper", "UnusedParameter.Global")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsPositive(this Byte value)
        {
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsPositive(this Int16 value)
        {
            return value >= 0;
        }

        [SuppressMessage("ReSharper", "UnusedParameter.Global")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsPositive(this UInt16 value)
        {
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsPositive(this Int32 value)
        {
            return value >= 0;
        }

        [SuppressMessage("ReSharper", "UnusedParameter.Global")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsPositive(this UInt32 value)
        {
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsPositive(this Int64 value)
        {
            return value >= 0;
        }

        [SuppressMessage("ReSharper", "UnusedParameter.Global")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsPositive(this UInt64 value)
        {
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsPositive(this Single value)
        {
            return value >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsPositive(this Double value)
        {
            return value >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsPositive(this Decimal value)
        {
            return value >= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsPositive(this BigInteger value)
        {
            return value >= 0;
        }

        [SuppressMessage("ReSharper", "UnusedParameter.Global")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNegative(this Char value)
        {
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNegative(this SByte value)
        {
            return value < 0;
        }

        [SuppressMessage("ReSharper", "UnusedParameter.Global")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNegative(this Byte value)
        {
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNegative(this Int16 value)
        {
            return value < 0;
        }

        [SuppressMessage("ReSharper", "UnusedParameter.Global")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNegative(this UInt16 value)
        {
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNegative(this Int32 value)
        {
            return value < 0;
        }

        [SuppressMessage("ReSharper", "UnusedParameter.Global")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNegative(this UInt32 value)
        {
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNegative(this Int64 value)
        {
            return value < 0;
        }

        [SuppressMessage("ReSharper", "UnusedParameter.Global")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNegative(this UInt64 value)
        {
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNegative(this Single value)
        {
            return value < 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNegative(this Double value)
        {
            return value < 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNegative(this Decimal value)
        {
            return value < 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNegative(this BigInteger value)
        {
            return value < 0;
        }

        [SuppressMessage("ReSharper", "UnusedParameter.Global")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Char ToSign(this Char value)
        {
            return '+';
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Char ToSign(this SByte value)
        {
            return value >= 0 ? '+' : '-';
        }

        [SuppressMessage("ReSharper", "UnusedParameter.Global")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Char ToSign(this Byte value)
        {
            return '+';
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Char ToSign(this Int16 value)
        {
            return value >= 0 ? '+' : '-';
        }

        [SuppressMessage("ReSharper", "UnusedParameter.Global")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Char ToSign(this UInt16 value)
        {
            return '+';
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Char ToSign(this Int32 value)
        {
            return value >= 0 ? '+' : '-';
        }

        [SuppressMessage("ReSharper", "UnusedParameter.Global")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Char ToSign(this UInt32 value)
        {
            return '+';
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Char ToSign(this Int64 value)
        {
            return value >= 0 ? '+' : '-';
        }

        [SuppressMessage("ReSharper", "UnusedParameter.Global")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Char ToSign(this UInt64 value)
        {
            return '+';
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Char ToSign(this Single value)
        {
            return value >= 0 ? '+' : '-';
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Char ToSign(this Double value)
        {
            return value >= 0 ? '+' : '-';
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Char ToSign(this Decimal value)
        {
            return value >= 0 ? '+' : '-';
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Char ToSign(this BigInteger value)
        {
            return value >= 0 ? '+' : '-';
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static SByte RoundUpToMultiplierOriginal(SByte value, SByte multiplier)
        {
            return RoundUpToMultiplierOriginal(value, multiplier, value % multiplier);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static SByte RoundUpToMultiplierOriginal(SByte value, SByte multiplier, Int32 remainder)
        {
            return remainder == 0 ? value : (SByte)(multiplier - remainder + value);
        }

        public static SByte RoundUpToMultiplier(SByte value, SByte multiplier = 10)
        {
            if (multiplier == 0)
            {
                throw new ArgumentException(nameof(multiplier));
            }

            if (value == 0 || value == multiplier || multiplier == 1 || multiplier == -1)
            {
                return value;
            }

            if (value < 0 ^ multiplier < 0)
            {
                return RoundDownToMultiplierOriginal(value, multiplier.Abs());
            }

            Int32 remainder = value % multiplier;
            return remainder == 0 ? value : RoundUpToMultiplierOriginal(value, multiplier, remainder);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static SByte RoundDownToMultiplierOriginal(SByte value, SByte multiplier)
        {
            Int32 remainder = value % multiplier;
            return remainder == 0 ? value : (SByte)(value - remainder);
        }

        public static SByte RoundDownToMultiplier(SByte value, SByte multiplier = 10)
        {
            if (multiplier == 0)
            {
                throw new ArgumentException(nameof(multiplier));
            }

            if (value == 0 || value == multiplier || multiplier == 1 || multiplier == -1)
            {
                return value;
            }

            if (value < 0 && multiplier < 0)
            {
                if (value > multiplier)
                {
                    return 0;
                }

                multiplier = (SByte)(-multiplier);
            }

            if (value < 0 || multiplier < 0)
            {
                return (SByte)(-RoundUpToMultiplierOriginal(-value, multiplier));
            }

            return RoundDownToMultiplierOriginal(value, multiplier);
        }

        public static SByte RoundBankingToMultiplier(SByte value, SByte multiplier = 10)
        {
            if (multiplier == 0)
            {
                throw new ArgumentException(nameof(multiplier));
            }

            if (value == 0 || value == multiplier || multiplier == 1 || multiplier == -1)
            {
                return value;
            }

            Int32 remainder = value % multiplier;
            Double average = multiplier / 2D;

            if (value > 0)
            {
                if (multiplier > 0)
                {
                    return remainder >= average ? RoundUpToMultiplierOriginal(value, multiplier) : RoundDownToMultiplierOriginal(value, multiplier);
                }

                return remainder <= -average ? RoundUpToMultiplier(value, multiplier) : RoundDownToMultiplier(value, multiplier);
            }

            if (multiplier > 0)
            {
                return remainder >= -average ? RoundUpToMultiplier(value, multiplier) : RoundDownToMultiplier(value, multiplier);
            }

            return remainder <= average ? RoundUpToMultiplierOriginal(value, multiplier) : RoundDownToMultiplierOriginal(value, multiplier);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SByte RoundToZeroMultiplier(SByte value, SByte multiplier)
        {
            if (value > 0)
            {
                return multiplier > 0 ? RoundDownToMultiplier(value, multiplier) : RoundDownToMultiplier(value, (SByte)(-multiplier));
            }

            return multiplier > 0 ? RoundDownToMultiplier(value, (SByte)(-multiplier)) : RoundDownToMultiplier(value, multiplier);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SByte RoundAwayFromZeroToMultiplier(SByte value, SByte multiplier)
        {
            if (value > 0)
            {
                return multiplier > 0 ? RoundUpToMultiplier(value, multiplier) : RoundUpToMultiplier(value, (SByte)(-multiplier));
            }

            return multiplier > 0 ? RoundUpToMultiplier(value, (SByte)(-multiplier)) : RoundUpToMultiplier(value, multiplier);
        }

        public static SByte RoundToMultiplier(this SByte value, SByte multiplier, MidpointRounding rounding = MidpointRounding.ToEven)
        {
            return rounding switch
            {
                MidpointRounding.ToEven => RoundBankingToMultiplier(value, multiplier),
                MidpointRounding.ToZero => RoundToZeroMultiplier(value, multiplier),
                MidpointRounding.AwayFromZero => RoundAwayFromZeroToMultiplier(value, multiplier),
                MidpointRounding.ToPositiveInfinity => RoundUpToMultiplier(value, multiplier),
                MidpointRounding.ToNegativeInfinity => RoundDownToMultiplier(value, multiplier),
                _ => throw new NotSupportedException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Byte RoundUpToMultiplierOriginal(Byte value, Byte multiplier)
        {
            return RoundUpToMultiplierOriginal(value, multiplier, value % multiplier);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Byte RoundUpToMultiplierOriginal(Byte value, Byte multiplier, Int32 remainder)
        {
            return remainder == 0 ? value : (Byte)(multiplier - remainder + value);
        }

        public static Byte RoundUpToMultiplier(Byte value, Byte multiplier = 10)
        {
            if (multiplier == 0)
            {
                throw new ArgumentException(nameof(multiplier));
            }

            if (value == 0 || value == multiplier || multiplier == 1)
            {
                return value;
            }

            Int32 remainder = value % multiplier;
            return remainder == 0 ? value : RoundUpToMultiplierOriginal(value, multiplier, remainder);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Byte RoundDownToMultiplierOriginal(Byte value, Byte multiplier)
        {
            Int32 remainder = value % multiplier;
            return remainder == 0 ? value : (Byte)(value - remainder);
        }

        public static Byte RoundDownToMultiplier(Byte value, Byte multiplier = 10)
        {
            if (multiplier == 0)
            {
                throw new ArgumentException(nameof(multiplier));
            }

            if (value == 0 || value == multiplier || multiplier == 1)
            {
                return value;
            }

            return RoundDownToMultiplierOriginal(value, multiplier);
        }

        public static Byte RoundBankingToMultiplier(Byte value, Byte multiplier = 10)
        {
            if (multiplier == 0)
            {
                throw new ArgumentException(nameof(multiplier));
            }

            if (value == 0 || value == multiplier || multiplier == 1)
            {
                return value;
            }

            return value % multiplier >= multiplier / 2D ? RoundUpToMultiplierOriginal(value, multiplier) : RoundDownToMultiplierOriginal(value, multiplier);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Byte RoundToZeroMultiplier(Byte value, Byte multiplier)
        {
            return RoundDownToMultiplier(value, multiplier);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Byte RoundAwayFromZeroToMultiplier(Byte value, Byte multiplier)
        {
            return RoundUpToMultiplier(value, multiplier);
        }

        public static Byte RoundToMultiplier(this Byte value, Byte multiplier, MidpointRounding rounding = MidpointRounding.ToEven)
        {
            return rounding switch
            {
                MidpointRounding.ToEven => RoundBankingToMultiplier(value, multiplier),
                MidpointRounding.ToZero => RoundToZeroMultiplier(value, multiplier),
                MidpointRounding.AwayFromZero => RoundAwayFromZeroToMultiplier(value, multiplier),
                MidpointRounding.ToPositiveInfinity => RoundUpToMultiplier(value, multiplier),
                MidpointRounding.ToNegativeInfinity => RoundDownToMultiplier(value, multiplier),
                _ => throw new NotSupportedException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Int16 RoundUpToMultiplierOriginal(Int16 value, Int16 multiplier)
        {
            return RoundUpToMultiplierOriginal(value, multiplier, value % multiplier);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Int16 RoundUpToMultiplierOriginal(Int16 value, Int16 multiplier, Int32 remainder)
        {
            return remainder == 0 ? value : (Int16)(multiplier - remainder + value);
        }

        public static Int16 RoundUpToMultiplier(Int16 value, Int16 multiplier = 10)
        {
            if (multiplier == 0)
            {
                throw new ArgumentException(nameof(multiplier));
            }

            if (value == 0 || value == multiplier || multiplier == 1 || multiplier == -1)
            {
                return value;
            }

            if (value < 0 ^ multiplier < 0)
            {
                return RoundDownToMultiplierOriginal(value, multiplier.Abs());
            }

            Int32 remainder = value % multiplier;
            return remainder == 0 ? value : RoundUpToMultiplierOriginal(value, multiplier, remainder);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Int16 RoundDownToMultiplierOriginal(Int16 value, Int16 multiplier)
        {
            Int32 remainder = value % multiplier;
            return remainder == 0 ? value : (Int16)(value - remainder);
        }

        public static Int16 RoundDownToMultiplier(Int16 value, Int16 multiplier = 10)
        {
            if (multiplier == 0)
            {
                throw new ArgumentException(nameof(multiplier));
            }

            if (value == 0 || value == multiplier || multiplier == 1 || multiplier == -1)
            {
                return value;
            }

            if (value < 0 && multiplier < 0)
            {
                if (value > multiplier)
                {
                    return 0;
                }

                multiplier = (Int16)(-multiplier);
            }

            if (value < 0 || multiplier < 0)
            {
                return (Int16)(-RoundUpToMultiplierOriginal(-value, multiplier));
            }

            return RoundDownToMultiplierOriginal(value, multiplier);
        }

        public static Int16 RoundBankingToMultiplier(Int16 value, Int16 multiplier = 10)
        {
            if (multiplier == 0)
            {
                throw new ArgumentException(nameof(multiplier));
            }

            if (value == 0 || value == multiplier || multiplier == 1 || multiplier == -1)
            {
                return value;
            }

            Int32 remainder = value % multiplier;
            Double average = multiplier / 2D;

            if (value > 0)
            {
                if (multiplier > 0)
                {
                    return remainder >= average ? RoundUpToMultiplierOriginal(value, multiplier) : RoundDownToMultiplierOriginal(value, multiplier);
                }

                return remainder <= -average ? RoundUpToMultiplier(value, multiplier) : RoundDownToMultiplier(value, multiplier);
            }

            if (multiplier > 0)
            {
                return remainder >= -average ? RoundUpToMultiplier(value, multiplier) : RoundDownToMultiplier(value, multiplier);
            }

            return remainder <= average ? RoundUpToMultiplierOriginal(value, multiplier) : RoundDownToMultiplierOriginal(value, multiplier);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int16 RoundToZeroMultiplier(Int16 value, Int16 multiplier)
        {
            if (value > 0)
            {
                return multiplier > 0 ? RoundDownToMultiplier(value, multiplier) : RoundDownToMultiplier(value, (Int16)(-multiplier));
            }

            return multiplier > 0 ? RoundDownToMultiplier(value, (Int16)(-multiplier)) : RoundDownToMultiplier(value, multiplier);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int16 RoundAwayFromZeroToMultiplier(Int16 value, Int16 multiplier)
        {
            if (value > 0)
            {
                return multiplier > 0 ? RoundUpToMultiplier(value, multiplier) : RoundUpToMultiplier(value, (Int16)(-multiplier));
            }

            return multiplier > 0 ? RoundUpToMultiplier(value, (Int16)(-multiplier)) : RoundUpToMultiplier(value, multiplier);
        }

        public static Int16 RoundToMultiplier(this Int16 value, Int16 multiplier, MidpointRounding rounding = MidpointRounding.ToEven)
        {
            return rounding switch
            {
                MidpointRounding.ToEven => RoundBankingToMultiplier(value, multiplier),
                MidpointRounding.ToZero => RoundToZeroMultiplier(value, multiplier),
                MidpointRounding.AwayFromZero => RoundAwayFromZeroToMultiplier(value, multiplier),
                MidpointRounding.ToPositiveInfinity => RoundUpToMultiplier(value, multiplier),
                MidpointRounding.ToNegativeInfinity => RoundDownToMultiplier(value, multiplier),
                _ => throw new NotSupportedException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static UInt16 RoundUpToMultiplierOriginal(UInt16 value, UInt16 multiplier)
        {
            return RoundUpToMultiplierOriginal(value, multiplier, value % multiplier);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static UInt16 RoundUpToMultiplierOriginal(UInt16 value, UInt16 multiplier, Int32 remainder)
        {
            return remainder == 0 ? value : (UInt16)(multiplier - remainder + value);
        }

        public static UInt16 RoundUpToMultiplier(UInt16 value, UInt16 multiplier = 10)
        {
            if (multiplier == 0)
            {
                throw new ArgumentException(nameof(multiplier));
            }

            if (value == 0 || value == multiplier || multiplier == 1)
            {
                return value;
            }

            Int32 remainder = value % multiplier;
            return remainder == 0 ? value : RoundUpToMultiplierOriginal(value, multiplier, remainder);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static UInt16 RoundDownToMultiplierOriginal(UInt16 value, UInt16 multiplier)
        {
            Int32 remainder = value % multiplier;
            return remainder == 0 ? value : (UInt16)(value - remainder);
        }

        public static UInt16 RoundDownToMultiplier(UInt16 value, UInt16 multiplier = 10)
        {
            if (multiplier == 0)
            {
                throw new ArgumentException(nameof(multiplier));
            }

            if (value == 0 || value == multiplier || multiplier == 1)
            {
                return value;
            }

            return RoundDownToMultiplierOriginal(value, multiplier);
        }

        public static UInt16 RoundBankingToMultiplier(UInt16 value, UInt16 multiplier = 10)
        {
            if (multiplier == 0)
            {
                throw new ArgumentException(nameof(multiplier));
            }

            if (value == 0 || value == multiplier || multiplier == 1)
            {
                return value;
            }

            return value % multiplier >= multiplier / 2D ? RoundUpToMultiplierOriginal(value, multiplier) : RoundDownToMultiplierOriginal(value, multiplier);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt16 RoundToZeroMultiplier(UInt16 value, UInt16 multiplier)
        {
            return RoundDownToMultiplier(value, multiplier);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt16 RoundAwayFromZeroToMultiplier(UInt16 value, UInt16 multiplier)
        {
            return RoundUpToMultiplier(value, multiplier);
        }

        public static UInt16 RoundToMultiplier(this UInt16 value, UInt16 multiplier, MidpointRounding rounding = MidpointRounding.ToEven)
        {
            return rounding switch
            {
                MidpointRounding.ToEven => RoundBankingToMultiplier(value, multiplier),
                MidpointRounding.ToZero => RoundToZeroMultiplier(value, multiplier),
                MidpointRounding.AwayFromZero => RoundAwayFromZeroToMultiplier(value, multiplier),
                MidpointRounding.ToPositiveInfinity => RoundUpToMultiplier(value, multiplier),
                MidpointRounding.ToNegativeInfinity => RoundDownToMultiplier(value, multiplier),
                _ => throw new NotSupportedException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Int32 RoundUpToMultiplierOriginal(Int32 value, Int32 multiplier)
        {
            return RoundUpToMultiplierOriginal(value, multiplier, value % multiplier);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Int32 RoundUpToMultiplierOriginal(Int32 value, Int32 multiplier, Int32 remainder)
        {
            return remainder == 0 ? value : multiplier - remainder + value;
        }

        public static Int32 RoundUpToMultiplier(Int32 value, Int32 multiplier = 10)
        {
            if (multiplier == 0)
            {
                throw new ArgumentException(nameof(multiplier));
            }

            if (value == 0 || value == multiplier || multiplier == 1 || multiplier == -1)
            {
                return value;
            }

            if (value < 0 ^ multiplier < 0)
            {
                return RoundDownToMultiplierOriginal(value, multiplier.Abs());
            }

            Int32 remainder = value % multiplier;
            return remainder == 0 ? value : RoundUpToMultiplierOriginal(value, multiplier, remainder);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Int32 RoundDownToMultiplierOriginal(Int32 value, Int32 multiplier)
        {
            Int32 remainder = value % multiplier;
            return remainder == 0 ? value : value - remainder;
        }

        public static Int32 RoundDownToMultiplier(Int32 value, Int32 multiplier = 10)
        {
            if (multiplier == 0)
            {
                throw new ArgumentException(nameof(multiplier));
            }

            if (value == 0 || value == multiplier || multiplier == 1 || multiplier == -1)
            {
                return value;
            }

            if (value < 0 && multiplier < 0)
            {
                if (value > multiplier)
                {
                    return 0;
                }

                multiplier = -multiplier;
            }

            if (value < 0 || multiplier < 0)
            {
                return -RoundUpToMultiplierOriginal(-value, multiplier);
            }

            return RoundDownToMultiplierOriginal(value, multiplier);
        }

        public static Int32 RoundBankingToMultiplier(Int32 value, Int32 multiplier = 10)
        {
            if (multiplier == 0)
            {
                throw new ArgumentException(nameof(multiplier));
            }

            if (value == 0 || value == multiplier || multiplier == 1 || multiplier == -1)
            {
                return value;
            }

            Int32 remainder = value % multiplier;
            Double average = multiplier / 2D;

            if (value > 0)
            {
                if (multiplier > 0)
                {
                    return remainder >= average ? RoundUpToMultiplierOriginal(value, multiplier) : RoundDownToMultiplierOriginal(value, multiplier);
                }

                return remainder <= -average ? RoundUpToMultiplier(value, multiplier) : RoundDownToMultiplier(value, multiplier);
            }

            if (multiplier > 0)
            {
                return remainder >= -average ? RoundUpToMultiplier(value, multiplier) : RoundDownToMultiplier(value, multiplier);
            }

            return remainder <= average ? RoundUpToMultiplierOriginal(value, multiplier) : RoundDownToMultiplierOriginal(value, multiplier);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 RoundToZeroMultiplier(Int32 value, Int32 multiplier)
        {
            if (value > 0)
            {
                return multiplier > 0 ? RoundDownToMultiplier(value, multiplier) : RoundDownToMultiplier(value, -multiplier);
            }

            return multiplier > 0 ? RoundDownToMultiplier(value, -multiplier) : RoundDownToMultiplier(value, multiplier);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 RoundAwayFromZeroToMultiplier(Int32 value, Int32 multiplier)
        {
            if (value > 0)
            {
                return multiplier > 0 ? RoundUpToMultiplier(value, multiplier) : RoundUpToMultiplier(value, -multiplier);
            }

            return multiplier > 0 ? RoundUpToMultiplier(value, -multiplier) : RoundUpToMultiplier(value, multiplier);
        }

        public static Int32 RoundToMultiplier(this Int32 value, Int32 multiplier, MidpointRounding rounding = MidpointRounding.ToEven)
        {
            return rounding switch
            {
                MidpointRounding.ToEven => RoundBankingToMultiplier(value, multiplier),
                MidpointRounding.ToZero => RoundToZeroMultiplier(value, multiplier),
                MidpointRounding.AwayFromZero => RoundAwayFromZeroToMultiplier(value, multiplier),
                MidpointRounding.ToPositiveInfinity => RoundUpToMultiplier(value, multiplier),
                MidpointRounding.ToNegativeInfinity => RoundDownToMultiplier(value, multiplier),
                _ => throw new NotSupportedException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static UInt32 RoundUpToMultiplierOriginal(UInt32 value, UInt32 multiplier)
        {
            return RoundUpToMultiplierOriginal(value, multiplier, value % multiplier);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static UInt32 RoundUpToMultiplierOriginal(UInt32 value, UInt32 multiplier, UInt32 remainder)
        {
            return remainder == 0 ? value : multiplier - remainder + value;
        }

        public static UInt32 RoundUpToMultiplier(UInt32 value, UInt32 multiplier = 10)
        {
            if (multiplier == 0)
            {
                throw new ArgumentException(nameof(multiplier));
            }

            if (value == 0 || value == multiplier || multiplier == 1)
            {
                return value;
            }

            UInt32 remainder = value % multiplier;
            return remainder == 0 ? value : RoundUpToMultiplierOriginal(value, multiplier, remainder);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static UInt32 RoundDownToMultiplierOriginal(UInt32 value, UInt32 multiplier)
        {
            UInt32 remainder = value % multiplier;
            return remainder == 0 ? value : value - remainder;
        }

        public static UInt32 RoundDownToMultiplier(UInt32 value, UInt32 multiplier = 10)
        {
            if (multiplier == 0)
            {
                throw new ArgumentException(nameof(multiplier));
            }

            if (value == 0 || value == multiplier || multiplier == 1)
            {
                return value;
            }

            return RoundDownToMultiplierOriginal(value, multiplier);
        }

        public static UInt32 RoundBankingToMultiplier(UInt32 value, UInt32 multiplier = 10)
        {
            if (multiplier == 0)
            {
                throw new ArgumentException(nameof(multiplier));
            }

            if (value == 0 || value == multiplier || multiplier == 1)
            {
                return value;
            }

            return value % multiplier >= multiplier / 2D ? RoundUpToMultiplierOriginal(value, multiplier) : RoundDownToMultiplierOriginal(value, multiplier);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 RoundToZeroMultiplier(UInt32 value, UInt32 multiplier)
        {
            return RoundDownToMultiplier(value, multiplier);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 RoundAwayFromZeroToMultiplier(UInt32 value, UInt32 multiplier)
        {
            return RoundUpToMultiplier(value, multiplier);
        }

        public static UInt32 RoundToMultiplier(this UInt32 value, UInt32 multiplier, MidpointRounding rounding = MidpointRounding.ToEven)
        {
            return rounding switch
            {
                MidpointRounding.ToEven => RoundBankingToMultiplier(value, multiplier),
                MidpointRounding.ToZero => RoundToZeroMultiplier(value, multiplier),
                MidpointRounding.AwayFromZero => RoundAwayFromZeroToMultiplier(value, multiplier),
                MidpointRounding.ToPositiveInfinity => RoundUpToMultiplier(value, multiplier),
                MidpointRounding.ToNegativeInfinity => RoundDownToMultiplier(value, multiplier),
                _ => throw new NotSupportedException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Int64 RoundUpToMultiplierOriginal(Int64 value, Int64 multiplier)
        {
            return RoundUpToMultiplierOriginal(value, multiplier, value % multiplier);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Int64 RoundUpToMultiplierOriginal(Int64 value, Int64 multiplier, Int64 remainder)
        {
            return remainder == 0 ? value : multiplier - remainder + value;
        }

        public static Int64 RoundUpToMultiplier(Int64 value, Int64 multiplier = 10)
        {
            if (multiplier == 0)
            {
                throw new ArgumentException(nameof(multiplier));
            }

            if (value == 0 || value == multiplier || multiplier == 1 || multiplier == -1)
            {
                return value;
            }

            if (value < 0 ^ multiplier < 0)
            {
                return RoundDownToMultiplierOriginal(value, multiplier.Abs());
            }

            Int64 remainder = value % multiplier;
            return remainder == 0 ? value : RoundUpToMultiplierOriginal(value, multiplier, remainder);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Int64 RoundDownToMultiplierOriginal(Int64 value, Int64 multiplier)
        {
            Int64 remainder = value % multiplier;
            return remainder == 0 ? value : value - remainder;
        }

        public static Int64 RoundDownToMultiplier(Int64 value, Int64 multiplier = 10)
        {
            if (multiplier == 0)
            {
                throw new ArgumentException(nameof(multiplier));
            }

            if (value == 0 || value == multiplier || multiplier == 1 || multiplier == -1)
            {
                return value;
            }

            if (value < 0 && multiplier < 0)
            {
                if (value > multiplier)
                {
                    return 0;
                }

                multiplier = -multiplier;
            }

            if (value < 0 || multiplier < 0)
            {
                return -RoundUpToMultiplierOriginal(-value, multiplier);
            }

            return RoundDownToMultiplierOriginal(value, multiplier);
        }

        public static Int64 RoundBankingToMultiplier(Int64 value, Int64 multiplier = 10)
        {
            if (multiplier == 0)
            {
                throw new ArgumentException(nameof(multiplier));
            }

            if (value == 0 || value == multiplier || multiplier == 1 || multiplier == -1)
            {
                return value;
            }

            Int64 remainder = value % multiplier;
            Double average = multiplier / 2D;

            if (value > 0)
            {
                if (multiplier > 0)
                {
                    return remainder >= average ? RoundUpToMultiplierOriginal(value, multiplier) : RoundDownToMultiplierOriginal(value, multiplier);
                }

                return remainder <= -average ? RoundUpToMultiplier(value, multiplier) : RoundDownToMultiplier(value, multiplier);
            }

            if (multiplier > 0)
            {
                return remainder >= -average ? RoundUpToMultiplier(value, multiplier) : RoundDownToMultiplier(value, multiplier);
            }

            return remainder <= average ? RoundUpToMultiplierOriginal(value, multiplier) : RoundDownToMultiplierOriginal(value, multiplier);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 RoundToZeroMultiplier(Int64 value, Int64 multiplier)
        {
            if (value > 0)
            {
                return multiplier > 0 ? RoundDownToMultiplier(value, multiplier) : RoundDownToMultiplier(value, -multiplier);
            }

            return multiplier > 0 ? RoundDownToMultiplier(value, -multiplier) : RoundDownToMultiplier(value, multiplier);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 RoundAwayFromZeroToMultiplier(Int64 value, Int64 multiplier)
        {
            if (value > 0)
            {
                return multiplier > 0 ? RoundUpToMultiplier(value, multiplier) : RoundUpToMultiplier(value, -multiplier);
            }

            return multiplier > 0 ? RoundUpToMultiplier(value, -multiplier) : RoundUpToMultiplier(value, multiplier);
        }

        public static Int64 RoundToMultiplier(this Int64 value, Int64 multiplier, MidpointRounding rounding = MidpointRounding.ToEven)
        {
            return rounding switch
            {
                MidpointRounding.ToEven => RoundBankingToMultiplier(value, multiplier),
                MidpointRounding.ToZero => RoundToZeroMultiplier(value, multiplier),
                MidpointRounding.AwayFromZero => RoundAwayFromZeroToMultiplier(value, multiplier),
                MidpointRounding.ToPositiveInfinity => RoundUpToMultiplier(value, multiplier),
                MidpointRounding.ToNegativeInfinity => RoundDownToMultiplier(value, multiplier),
                _ => throw new NotSupportedException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static UInt64 RoundUpToMultiplierOriginal(UInt64 value, UInt64 multiplier)
        {
            return RoundUpToMultiplierOriginal(value, multiplier, value % multiplier);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static UInt64 RoundUpToMultiplierOriginal(UInt64 value, UInt64 multiplier, UInt64 remainder)
        {
            return remainder == 0 ? value : multiplier - remainder + value;
        }

        public static UInt64 RoundUpToMultiplier(UInt64 value, UInt64 multiplier = 10)
        {
            if (multiplier == 0)
            {
                throw new ArgumentException(nameof(multiplier));
            }

            if (value == 0 || value == multiplier || multiplier == 1)
            {
                return value;
            }

            UInt64 remainder = value % multiplier;
            return remainder == 0 ? value : RoundUpToMultiplierOriginal(value, multiplier, remainder);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static UInt64 RoundDownToMultiplierOriginal(UInt64 value, UInt64 multiplier)
        {
            UInt64 remainder = value % multiplier;
            return remainder == 0 ? value : value - remainder;
        }

        public static UInt64 RoundDownToMultiplier(UInt64 value, UInt64 multiplier = 10)
        {
            if (multiplier == 0)
            {
                throw new ArgumentException(nameof(multiplier));
            }

            if (value == 0 || value == multiplier || multiplier == 1)
            {
                return value;
            }

            return RoundDownToMultiplierOriginal(value, multiplier);
        }

        public static UInt64 RoundBankingToMultiplier(UInt64 value, UInt64 multiplier = 10)
        {
            if (multiplier == 0)
            {
                throw new ArgumentException(nameof(multiplier));
            }

            if (value == 0 || value == multiplier || multiplier == 1)
            {
                return value;
            }

            return value % multiplier >= multiplier / 2D ? RoundUpToMultiplierOriginal(value, multiplier) : RoundDownToMultiplierOriginal(value, multiplier);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 RoundToZeroMultiplier(UInt64 value, UInt64 multiplier)
        {
            return RoundDownToMultiplier(value, multiplier);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 RoundAwayFromZeroToMultiplier(UInt64 value, UInt64 multiplier)
        {
            return RoundUpToMultiplier(value, multiplier);
        }

        public static UInt64 RoundToMultiplier(this UInt64 value, UInt64 multiplier, MidpointRounding rounding = MidpointRounding.ToEven)
        {
            return rounding switch
            {
                MidpointRounding.ToEven => RoundBankingToMultiplier(value, multiplier),
                MidpointRounding.ToZero => RoundToZeroMultiplier(value, multiplier),
                MidpointRounding.AwayFromZero => RoundAwayFromZeroToMultiplier(value, multiplier),
                MidpointRounding.ToPositiveInfinity => RoundUpToMultiplier(value, multiplier),
                MidpointRounding.ToNegativeInfinity => RoundDownToMultiplier(value, multiplier),
                _ => throw new NotSupportedException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Single RoundUpToMultiplierOriginal(Single value, Single multiplier)
        {
            return RoundUpToMultiplierOriginal(value, multiplier, value % multiplier);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Single RoundUpToMultiplierOriginal(Single value, Single multiplier, Single remainder)
        {
            return IsEpsilon(remainder) ? value : multiplier - remainder + value;
        }

        public static Single RoundUpToMultiplier(Single value, Single multiplier = 10)
        {
            if (IsEpsilon(multiplier))
            {
                throw new ArgumentException(nameof(multiplier));
            }

            if (IsEpsilon(value) || IsEpsilon(value - multiplier))
            {
                return value;
            }

            if (value < 0 ^ multiplier < 0)
            {
                return RoundDownToMultiplierOriginal(value, multiplier.Abs());
            }

            Single remainder = value % multiplier;
            return IsEpsilon(remainder) ? value : RoundUpToMultiplierOriginal(value, multiplier, remainder);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Single RoundDownToMultiplierOriginal(Single value, Single multiplier)
        {
            Single remainder = value % multiplier;
            return IsEpsilon(remainder) ? value : value - remainder;
        }

        public static Single RoundDownToMultiplier(Single value, Single multiplier = 10)
        {
            if (IsEpsilon(multiplier))
            {
                throw new ArgumentException(nameof(multiplier));
            }

            if (IsEpsilon(value) || IsEpsilon(value - multiplier))
            {
                return value;
            }

            if (value < 0 && multiplier < 0)
            {
                if (value > multiplier)
                {
                    return 0;
                }

                multiplier = -multiplier;
            }

            if (value < 0 || multiplier < 0)
            {
                return -RoundUpToMultiplierOriginal(-value, multiplier);
            }

            return RoundDownToMultiplierOriginal(value, multiplier);
        }

        public static Single RoundBankingToMultiplier(Single value, Single multiplier = 10)
        {
            if (IsEpsilon(multiplier))
            {
                throw new ArgumentException(nameof(multiplier));
            }

            if (IsEpsilon(value) || IsEpsilon(value - multiplier))
            {
                return value;
            }

            Single remainder = value % multiplier;
            Double average = multiplier / 2D;

            if (value > 0)
            {
                if (multiplier > 0)
                {
                    return remainder >= average ? RoundUpToMultiplierOriginal(value, multiplier) : RoundDownToMultiplierOriginal(value, multiplier);
                }

                return remainder <= -average ? RoundUpToMultiplier(value, multiplier) : RoundDownToMultiplier(value, multiplier);
            }

            if (multiplier > 0)
            {
                return remainder >= -average ? RoundUpToMultiplier(value, multiplier) : RoundDownToMultiplier(value, multiplier);
            }

            return remainder <= average ? RoundUpToMultiplierOriginal(value, multiplier) : RoundDownToMultiplierOriginal(value, multiplier);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single RoundToZeroMultiplier(Single value, Single multiplier)
        {
            if (value > 0)
            {
                return multiplier > 0 ? RoundDownToMultiplier(value, multiplier) : RoundDownToMultiplier(value, -multiplier);
            }

            return multiplier > 0 ? RoundDownToMultiplier(value, -multiplier) : RoundDownToMultiplier(value, multiplier);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single RoundAwayFromZeroToMultiplier(Single value, Single multiplier)
        {
            if (value > 0)
            {
                return multiplier > 0 ? RoundUpToMultiplier(value, multiplier) : RoundUpToMultiplier(value, -multiplier);
            }

            return multiplier > 0 ? RoundUpToMultiplier(value, -multiplier) : RoundUpToMultiplier(value, multiplier);
        }

        public static Single RoundToMultiplier(this Single value, Single multiplier, MidpointRounding rounding = MidpointRounding.ToEven)
        {
            return rounding switch
            {
                MidpointRounding.ToEven => RoundBankingToMultiplier(value, multiplier),
                MidpointRounding.ToZero => RoundToZeroMultiplier(value, multiplier),
                MidpointRounding.AwayFromZero => RoundAwayFromZeroToMultiplier(value, multiplier),
                MidpointRounding.ToPositiveInfinity => RoundUpToMultiplier(value, multiplier),
                MidpointRounding.ToNegativeInfinity => RoundDownToMultiplier(value, multiplier),
                _ => throw new NotSupportedException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Double RoundUpToMultiplierOriginal(Double value, Double multiplier)
        {
            return RoundUpToMultiplierOriginal(value, multiplier, value % multiplier);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Double RoundUpToMultiplierOriginal(Double value, Double multiplier, Double remainder)
        {
            return IsEpsilon(remainder) ? value : multiplier - remainder + value;
        }

        public static Double RoundUpToMultiplier(Double value, Double multiplier = 10)
        {
            if (IsEpsilon(multiplier))
            {
                throw new ArgumentException(nameof(multiplier));
            }

            if (IsEpsilon(value) || IsEpsilon(value - multiplier))
            {
                return value;
            }

            if (value < 0 ^ multiplier < 0)
            {
                return RoundDownToMultiplierOriginal(value, multiplier.Abs());
            }

            Double remainder = value % multiplier;
            return IsEpsilon(remainder) ? value : RoundUpToMultiplierOriginal(value, multiplier, remainder);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Double RoundDownToMultiplierOriginal(Double value, Double multiplier)
        {
            Double remainder = value % multiplier;
            return IsEpsilon(remainder) ? value : value - remainder;
        }

        public static Double RoundDownToMultiplier(Double value, Double multiplier = 10)
        {
            if (IsEpsilon(multiplier))
            {
                throw new ArgumentException(nameof(multiplier));
            }

            if (IsEpsilon(value) || IsEpsilon(value - multiplier))
            {
                return value;
            }

            if (value < 0 && multiplier < 0)
            {
                if (value > multiplier)
                {
                    return 0;
                }

                multiplier = -multiplier;
            }

            if (value < 0 || multiplier < 0)
            {
                return -RoundUpToMultiplierOriginal(-value, multiplier);
            }

            return RoundDownToMultiplierOriginal(value, multiplier);
        }

        public static Double RoundBankingToMultiplier(Double value, Double multiplier = 10)
        {
            if (IsEpsilon(multiplier))
            {
                throw new ArgumentException(nameof(multiplier));
            }

            if (IsEpsilon(value) || IsEpsilon(value - multiplier))
            {
                return value;
            }

            Double remainder = value % multiplier;
            Double average = multiplier / 2D;

            if (value > 0)
            {
                if (multiplier > 0)
                {
                    return remainder >= average ? RoundUpToMultiplierOriginal(value, multiplier) : RoundDownToMultiplierOriginal(value, multiplier);
                }

                return remainder <= -average ? RoundUpToMultiplier(value, multiplier) : RoundDownToMultiplier(value, multiplier);
            }

            if (multiplier > 0)
            {
                return remainder >= -average ? RoundUpToMultiplier(value, multiplier) : RoundDownToMultiplier(value, multiplier);
            }

            return remainder <= average ? RoundUpToMultiplierOriginal(value, multiplier) : RoundDownToMultiplierOriginal(value, multiplier);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double RoundToZeroMultiplier(Double value, Double multiplier)
        {
            if (value > 0)
            {
                return multiplier > 0 ? RoundDownToMultiplier(value, multiplier) : RoundDownToMultiplier(value, -multiplier);
            }

            return multiplier > 0 ? RoundDownToMultiplier(value, -multiplier) : RoundDownToMultiplier(value, multiplier);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double RoundAwayFromZeroToMultiplier(Double value, Double multiplier)
        {
            if (value > 0)
            {
                return multiplier > 0 ? RoundUpToMultiplier(value, multiplier) : RoundUpToMultiplier(value, -multiplier);
            }

            return multiplier > 0 ? RoundUpToMultiplier(value, -multiplier) : RoundUpToMultiplier(value, multiplier);
        }

        public static Double RoundToMultiplier(this Double value, Double multiplier, MidpointRounding rounding = MidpointRounding.ToEven)
        {
            return rounding switch
            {
                MidpointRounding.ToEven => RoundBankingToMultiplier(value, multiplier),
                MidpointRounding.ToZero => RoundToZeroMultiplier(value, multiplier),
                MidpointRounding.AwayFromZero => RoundAwayFromZeroToMultiplier(value, multiplier),
                MidpointRounding.ToPositiveInfinity => RoundUpToMultiplier(value, multiplier),
                MidpointRounding.ToNegativeInfinity => RoundDownToMultiplier(value, multiplier),
                _ => throw new NotSupportedException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Decimal RoundUpToMultiplierOriginal(Decimal value, Decimal multiplier)
        {
            return RoundUpToMultiplierOriginal(value, multiplier, value % multiplier);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Decimal RoundUpToMultiplierOriginal(Decimal value, Decimal multiplier, Decimal remainder)
        {
            return remainder == 0 ? value : multiplier - remainder + value;
        }

        public static Decimal RoundUpToMultiplier(Decimal value, Decimal multiplier = 10)
        {
            if (multiplier == 0)
            {
                throw new ArgumentException(nameof(multiplier));
            }

            if (value == 0 || value == multiplier || multiplier == 1 || multiplier == -1)
            {
                return value;
            }

            if (value < 0 ^ multiplier < 0)
            {
                return RoundDownToMultiplierOriginal(value, multiplier.Abs());
            }

            Decimal remainder = value % multiplier;
            return remainder == 0 ? value : RoundUpToMultiplierOriginal(value, multiplier, remainder);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Decimal RoundDownToMultiplierOriginal(Decimal value, Decimal multiplier)
        {
            Decimal remainder = value % multiplier;
            return remainder == 0 ? value : value - remainder;
        }

        public static Decimal RoundDownToMultiplier(Decimal value, Decimal multiplier = 10)
        {
            if (multiplier == 0)
            {
                throw new ArgumentException(nameof(multiplier));
            }

            if (value == 0 || value == multiplier || multiplier == 1 || multiplier == -1)
            {
                return value;
            }

            if (value < 0 && multiplier < 0)
            {
                if (value > multiplier)
                {
                    return 0;
                }

                multiplier = -multiplier;
            }

            if (value < 0 || multiplier < 0)
            {
                return -RoundUpToMultiplierOriginal(-value, multiplier);
            }

            return RoundDownToMultiplierOriginal(value, multiplier);
        }

        public static Decimal RoundBankingToMultiplier(Decimal value, Decimal multiplier = 10)
        {
            if (multiplier == 0)
            {
                throw new ArgumentException(nameof(multiplier));
            }

            if (value == 0 || value == multiplier || multiplier == 1 || multiplier == -1)
            {
                return value;
            }

            Decimal remainder = value % multiplier;
            Decimal average = multiplier / 2M;

            if (value > 0)
            {
                if (multiplier > 0)
                {
                    return remainder >= average ? RoundUpToMultiplierOriginal(value, multiplier) : RoundDownToMultiplierOriginal(value, multiplier);
                }

                return remainder <= -average ? RoundUpToMultiplier(value, multiplier) : RoundDownToMultiplier(value, multiplier);
            }

            if (multiplier > 0)
            {
                return remainder >= -average ? RoundUpToMultiplier(value, multiplier) : RoundDownToMultiplier(value, multiplier);
            }

            return remainder <= average ? RoundUpToMultiplierOriginal(value, multiplier) : RoundDownToMultiplierOriginal(value, multiplier);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal RoundToZeroMultiplier(Decimal value, Decimal multiplier)
        {
            if (value > 0)
            {
                return multiplier > 0 ? RoundDownToMultiplier(value, multiplier) : RoundDownToMultiplier(value, -multiplier);
            }

            return multiplier > 0 ? RoundDownToMultiplier(value, -multiplier) : RoundDownToMultiplier(value, multiplier);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal RoundAwayFromZeroToMultiplier(Decimal value, Decimal multiplier)
        {
            if (value > 0)
            {
                return multiplier > 0 ? RoundUpToMultiplier(value, multiplier) : RoundUpToMultiplier(value, -multiplier);
            }

            return multiplier > 0 ? RoundUpToMultiplier(value, -multiplier) : RoundUpToMultiplier(value, multiplier);
        }

        public static Decimal RoundToMultiplier(this Decimal value, Decimal multiplier, MidpointRounding rounding = MidpointRounding.ToEven)
        {
            return rounding switch
            {
                MidpointRounding.ToEven => RoundBankingToMultiplier(value, multiplier),
                MidpointRounding.ToZero => RoundToZeroMultiplier(value, multiplier),
                MidpointRounding.AwayFromZero => RoundAwayFromZeroToMultiplier(value, multiplier),
                MidpointRounding.ToPositiveInfinity => RoundUpToMultiplier(value, multiplier),
                MidpointRounding.ToNegativeInfinity => RoundDownToMultiplier(value, multiplier),
                _ => throw new NotSupportedException()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Abs(ref Char value)
        {
            value = Abs(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Char Abs(this Char value)
        {
            return value;
        }

        /// <inheritdoc cref="Math.Abs(SByte)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Abs(ref SByte value)
        {
            value = Abs(value);
        }

        /// <inheritdoc cref="Math.Abs(SByte)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SByte Abs(this SByte value)
        {
            return Math.Abs(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Abs(ref Byte value)
        {
            value = Abs(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Byte Abs(this Byte value)
        {
            return value;
        }

        /// <inheritdoc cref="Math.Abs(Int16)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Abs(ref Int16 value)
        {
            value = Abs(value);
        }

        /// <inheritdoc cref="Math.Abs(Int16)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int16 Abs(this Int16 value)
        {
            return Math.Abs(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Abs(ref UInt16 value)
        {
            value = Abs(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt16 Abs(this UInt16 value)
        {
            return value;
        }

        /// <inheritdoc cref="Math.Abs(Int32)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Abs(ref Int32 value)
        {
            value = Abs(value);
        }

        /// <inheritdoc cref="Math.Abs(Int32)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Abs(this Int32 value)
        {
            return Math.Abs(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Abs(ref UInt32 value)
        {
            value = Abs(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 Abs(this UInt32 value)
        {
            return value;
        }

        /// <inheritdoc cref="Math.Abs(Int64)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Abs(ref Int64 value)
        {
            value = Abs(value);
        }

        /// <inheritdoc cref="Math.Abs(Int64)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 Abs(this Int64 value)
        {
            return Math.Abs(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Abs(ref UInt64 value)
        {
            value = Abs(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 Abs(this UInt64 value)
        {
            return value;
        }

        /// <inheritdoc cref="Math.Abs(Single)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Abs(ref Single value)
        {
            value = Abs(value);
        }

        /// <inheritdoc cref="Math.Abs(Single)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Abs(this Single value)
        {
            return Math.Abs(value);
        }

        /// <inheritdoc cref="Math.Abs(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Abs(ref Double value)
        {
            value = Abs(value);
        }

        /// <inheritdoc cref="Math.Abs(Double)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Abs(this Double value)
        {
            return Math.Abs(value);
        }

        /// <inheritdoc cref="Math.Abs(Decimal)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Abs(ref Decimal value)
        {
            value = Abs(value);
        }

        /// <inheritdoc cref="Math.Abs(Decimal)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Abs(this Decimal value)
        {
            return Math.Abs(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SByte MathematicalModulo(this SByte value, SByte modulo)
        {
            return (SByte) ((Math.Abs(value * modulo) + value) % modulo);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Byte MathematicalModulo(this Byte value, Byte modulo)
        {
            return (Byte) (value % modulo);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int16 MathematicalModulo(this Int16 value, Int16 modulo)
        {
            return (Int16) ((Math.Abs(value * modulo) + value) % modulo);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt16 MathematicalModulo(this UInt16 value, UInt16 modulo)
        {
            return (UInt16) (value % modulo);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 MathematicalModulo(this Int32 value, Int32 modulo)
        {
            return (Math.Abs(value * modulo) + value) % modulo;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 MathematicalModulo(this UInt32 value, UInt32 modulo)
        {
            return value % modulo;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 MathematicalModulo(this Int64 value, Int64 modulo)
        {
            return (Math.Abs(value * modulo) + value) % modulo;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 MathematicalModulo(this UInt64 value, UInt64 modulo)
        {
            return value % modulo;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single MathematicalModulo(this Single value, Single modulo)
        {
            return (Math.Abs(value * modulo) + value) % modulo;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double MathematicalModulo(this Double value, Double modulo)
        {
            return (Math.Abs(value * modulo) + value) % modulo;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal MathematicalModulo(this Decimal value, Decimal modulo)
        {
            return (Math.Abs(value * modulo) + value) % modulo;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Pow(this Int32 value, Byte pow)
        {
            Int32 ret = 1;
            while (pow != 0)
            {
                if ((pow & 1) == 1)
                {
                    ret *= value;
                }

                value *= value;
                pow >>= 1;
            }

            return ret;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 Pow(this UInt32 value, Byte pow)
        {
            UInt32 ret = 1;
            while (pow != 0)
            {
                if ((pow & 1) == 1)
                {
                    ret *= value;
                }

                value *= value;
                pow >>= 1;
            }

            return ret;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 Pow(this Int64 value, Byte pow)
        {
            Int64 ret = 1;
            while (pow != 0)
            {
                if ((pow & 1) == 1)
                {
                    ret *= value;
                }

                value *= value;
                pow >>= 1;
            }

            return ret;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 Pow(this UInt64 value, Byte pow)
        {
            UInt64 ret = 1;
            while (pow != 0)
            {
                if ((pow & 1) == 1)
                {
                    ret *= value;
                }

                value *= value;
                pow >>= 1;
            }

            return ret;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInteger Pow(this BigInteger value, Byte pow)
        {
            BigInteger ret = 1;
            while (pow != 0)
            {
                if ((pow & 1) == 1)
                {
                    ret *= value;
                }

                value *= value;
                pow >>= 1;
            }

            return ret;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Byte RoundUpToPowerOf2(this SByte value)
        {
            unchecked
            {
                return (Byte) BitOperations.RoundUpToPowerOf2((Byte) value);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Byte RoundUpToPowerOf2(this Byte value)
        {
            unchecked
            {
                return (Byte) BitOperations.RoundUpToPowerOf2(value);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt16 RoundUpToPowerOf2(this Int16 value)
        {
            unchecked
            {
                return (UInt16) BitOperations.RoundUpToPowerOf2((UInt16) value);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt16 RoundUpToPowerOf2(this UInt16 value)
        {
            unchecked
            {
                return (UInt16) BitOperations.RoundUpToPowerOf2(value);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 RoundUpToPowerOf2(this Int32 value)
        {
            unchecked
            {
                return BitOperations.RoundUpToPowerOf2((UInt32) value);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 RoundUpToPowerOf2(this UInt32 value)
        {
            unchecked
            {
                return BitOperations.RoundUpToPowerOf2(value);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 RoundUpToPowerOf2(this Int64 value)
        {
            unchecked
            {
                return BitOperations.RoundUpToPowerOf2((UInt64) value);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 RoundUpToPowerOf2(this UInt64 value)
        {
            unchecked
            {
                return BitOperations.RoundUpToPowerOf2(value);
            }
        }

        /// <inheritdoc cref="Math.Sqrt"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Sqrt(this SByte value)
        {
            return Math.Sqrt(value);
        }

        /// <inheritdoc cref="Math.Sqrt"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Sqrt(this Byte value)
        {
            return Math.Sqrt(value);
        }

        /// <inheritdoc cref="Math.Sqrt"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Sqrt(this Int16 value)
        {
            return Math.Sqrt(value);
        }

        /// <inheritdoc cref="Math.Sqrt"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Sqrt(this UInt16 value)
        {
            return Math.Sqrt(value);
        }

        /// <inheritdoc cref="Math.Sqrt"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Sqrt(this Int32 value)
        {
            return Math.Sqrt(value);
        }

        /// <inheritdoc cref="Math.Sqrt"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Sqrt(this UInt32 value)
        {
            return Math.Sqrt(value);
        }

        /// <inheritdoc cref="Math.Sqrt"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Sqrt(this Int64 value)
        {
            return Math.Sqrt(value);
        }

        /// <inheritdoc cref="Math.Sqrt"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Sqrt(this UInt64 value)
        {
            return Math.Sqrt(value);
        }

        /// <inheritdoc cref="Math.Sqrt"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Sqrt(this Single value)
        {
            return MathF.Sqrt(value);
        }

        /// <inheritdoc cref="Math.Sqrt"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Sqrt(this Double value)
        {
            return Math.Sqrt(value);
        }

        /// <inheritdoc cref="Math.Sqrt"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Log(this SByte value)
        {
            return Math.Log(value);
        }

        /// <inheritdoc cref="Math.Sqrt"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Log(this SByte value, Double @base)
        {
            return Math.Log(value, @base);
        }

        /// <inheritdoc cref="Math.Sqrt"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Log2(this SByte value)
        {
            return Math.Log2(value);
        }

        /// <inheritdoc cref="Math.Sqrt"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 ILogB(this SByte value)
        {
            unchecked
            {
                return BitOperations.Log2((Byte) value);
            }
        }

        /// <inheritdoc cref="Math.Sqrt"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Log10(this SByte value)
        {
            return Math.Log10(value);
        }

        /// <inheritdoc cref="Math.Sqrt"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Log(this Byte value)
        {
            return Math.Log(value);
        }

        /// <inheritdoc cref="Math.Sqrt"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Log(this Byte value, Double @base)
        {
            return Math.Log(value, @base);
        }

        /// <inheritdoc cref="Math.Sqrt"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Log2(this Byte value)
        {
            return Math.Log2(value);
        }

        /// <inheritdoc cref="Math.Sqrt"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 ILogB(this Byte value)
        {
            return BitOperations.Log2(value);
        }

        /// <inheritdoc cref="Math.Sqrt"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Log10(this Byte value)
        {
            return Math.Log10(value);
        }

        /// <inheritdoc cref="Math.Sqrt"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Log(this Int16 value)
        {
            return Math.Log(value);
        }

        /// <inheritdoc cref="Math.Sqrt"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Log(this Int16 value, Double @base)
        {
            return Math.Log(value, @base);
        }

        /// <inheritdoc cref="Math.Sqrt"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Log2(this Int16 value)
        {
            return Math.Log2(value);
        }

        /// <inheritdoc cref="Math.Sqrt"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 ILogB(this Int16 value)
        {
            unchecked
            {
                return BitOperations.Log2((UInt16) value);
            }
        }

        /// <inheritdoc cref="Math.Sqrt"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Log10(this Int16 value)
        {
            return Math.Log10(value);
        }

        /// <inheritdoc cref="Math.Sqrt"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Log(this UInt16 value)
        {
            return Math.Log(value);
        }

        /// <inheritdoc cref="Math.Sqrt"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Log(this UInt16 value, Double @base)
        {
            return Math.Log(value, @base);
        }

        /// <inheritdoc cref="Math.Sqrt"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Log2(this UInt16 value)
        {
            return Math.Log2(value);
        }

        /// <inheritdoc cref="Math.Sqrt"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 ILogB(this UInt16 value)
        {
            return BitOperations.Log2(value);
        }

        /// <inheritdoc cref="Math.Sqrt"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Log10(this UInt16 value)
        {
            return Math.Log10(value);
        }

        /// <inheritdoc cref="Math.Sqrt"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Log(this Int32 value)
        {
            return Math.Log(value);
        }

        /// <inheritdoc cref="Math.Sqrt"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Log(this Int32 value, Double @base)
        {
            return Math.Log(value, @base);
        }

        /// <inheritdoc cref="Math.Sqrt"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Log2(this Int32 value)
        {
            return Math.Log2(value);
        }

        /// <inheritdoc cref="Math.Sqrt"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 ILogB(this Int32 value)
        {
            unchecked
            {
                return BitOperations.Log2((UInt32) value);
            }
        }

        /// <inheritdoc cref="Math.Sqrt"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Log10(this Int32 value)
        {
            return Math.Log10(value);
        }

        /// <inheritdoc cref="Math.Sqrt"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Log(this UInt32 value)
        {
            return Math.Log(value);
        }

        /// <inheritdoc cref="Math.Sqrt"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Log(this UInt32 value, Double @base)
        {
            return Math.Log(value, @base);
        }

        /// <inheritdoc cref="Math.Sqrt"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Log2(this UInt32 value)
        {
            return Math.Log2(value);
        }

        /// <inheritdoc cref="Math.Sqrt"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 ILogB(this UInt32 value)
        {
            return BitOperations.Log2(value);
        }

        /// <inheritdoc cref="Math.Sqrt"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Log10(this UInt32 value)
        {
            return Math.Log10(value);
        }

        /// <inheritdoc cref="Math.Sqrt"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Log(this Int64 value)
        {
            return Math.Log(value);
        }

        /// <inheritdoc cref="Math.Sqrt"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Log(this Int64 value, Double @base)
        {
            return Math.Log(value, @base);
        }

        /// <inheritdoc cref="Math.Sqrt"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Log2(this Int64 value)
        {
            return Math.Log2(value);
        }

        /// <inheritdoc cref="Math.Sqrt"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 ILogB(this Int64 value)
        {
            unchecked
            {
                return BitOperations.Log2((UInt64) value);
            }
        }

        /// <inheritdoc cref="Math.Sqrt"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Log10(this Int64 value)
        {
            return Math.Log10(value);
        }

        /// <inheritdoc cref="Math.Sqrt"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Log(this UInt64 value)
        {
            return Math.Log(value);
        }

        /// <inheritdoc cref="Math.Sqrt"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Log(this UInt64 value, Double @base)
        {
            return Math.Log(value, @base);
        }

        /// <inheritdoc cref="Math.Sqrt"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Log2(this UInt64 value)
        {
            return Math.Log2(value);
        }

        /// <inheritdoc cref="Math.Sqrt"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 ILogB(this UInt64 value)
        {
            return BitOperations.Log2(value);
        }

        /// <inheritdoc cref="Math.Sqrt"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Log10(this UInt64 value)
        {
            return Math.Log10(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Char Clamp(this Char value)
        {
            return Clamp(value, default(Char));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Char Clamp(this Char value, Boolean looped)
        {
            return Clamp(value, default, Char.MaxValue, looped);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Char Clamp(this Char value, Char maximum)
        {
            return Clamp(value, default, maximum);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Char Clamp(this Char value, Char maximum, Boolean looped)
        {
            return Clamp(value, default, maximum, looped);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Char Clamp(this Char value, Char minimum, Char maximum)
        {
            return (Char) Math.Clamp(value, minimum, maximum);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Char Clamp(this Char value, Char minimum, Char maximum, Boolean looped)
        {
            if (value > maximum)
            {
                return looped ? minimum : maximum;
            }

            if (value < minimum)
            {
                return looped ? maximum : minimum;
            }

            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SByte Clamp(this SByte value)
        {
            return Clamp(value, default(SByte));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SByte Clamp(this SByte value, Boolean looped)
        {
            return Clamp(value, default, SByte.MaxValue, looped);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SByte Clamp(this SByte value, SByte maximum)
        {
            return Clamp(value, default, maximum);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SByte Clamp(this SByte value, SByte maximum, Boolean looped)
        {
            return Clamp(value, default, maximum, looped);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SByte Clamp(this SByte value, SByte minimum, SByte maximum)
        {
            return Math.Clamp(value, minimum, maximum);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SByte Clamp(this SByte value, SByte minimum, SByte maximum, Boolean looped)
        {
            if (value > maximum)
            {
                return looped ? minimum : maximum;
            }

            if (value < minimum)
            {
                return looped ? maximum : minimum;
            }

            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Byte Clamp(this Byte value)
        {
            return Clamp(value, default(Byte));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Byte Clamp(this Byte value, Boolean looped)
        {
            return Clamp(value, default, Byte.MaxValue, looped);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Byte Clamp(this Byte value, Byte maximum)
        {
            return Clamp(value, default, maximum);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Byte Clamp(this Byte value, Byte maximum, Boolean looped)
        {
            return Clamp(value, default, maximum, looped);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Byte Clamp(this Byte value, Byte minimum, Byte maximum)
        {
            return Math.Clamp(value, minimum, maximum);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Byte Clamp(this Byte value, Byte minimum, Byte maximum, Boolean looped)
        {
            if (value > maximum)
            {
                return looped ? minimum : maximum;
            }

            if (value < minimum)
            {
                return looped ? maximum : minimum;
            }

            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int16 Clamp(this Int16 value)
        {
            return Clamp(value, default(Int16));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int16 Clamp(this Int16 value, Boolean looped)
        {
            return Clamp(value, default, Int16.MaxValue, looped);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int16 Clamp(this Int16 value, Int16 maximum)
        {
            return Clamp(value, default, maximum);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int16 Clamp(this Int16 value, Int16 maximum, Boolean looped)
        {
            return Clamp(value, default, maximum, looped);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int16 Clamp(this Int16 value, Int16 minimum, Int16 maximum)
        {
            return Math.Clamp(value, minimum, maximum);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int16 Clamp(this Int16 value, Int16 minimum, Int16 maximum, Boolean looped)
        {
            if (value > maximum)
            {
                return looped ? minimum : maximum;
            }

            if (value < minimum)
            {
                return looped ? maximum : minimum;
            }

            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt16 Clamp(this UInt16 value)
        {
            return Clamp(value, default(UInt16));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt16 Clamp(this UInt16 value, Boolean looped)
        {
            return Clamp(value, default, UInt16.MaxValue, looped);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt16 Clamp(this UInt16 value, UInt16 maximum)
        {
            return Clamp(value, default, maximum);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt16 Clamp(this UInt16 value, UInt16 maximum, Boolean looped)
        {
            return Clamp(value, default, maximum, looped);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt16 Clamp(this UInt16 value, UInt16 minimum, UInt16 maximum)
        {
            return Math.Clamp(value, minimum, maximum);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt16 Clamp(this UInt16 value, UInt16 minimum, UInt16 maximum, Boolean looped)
        {
            if (value > maximum)
            {
                return looped ? minimum : maximum;
            }

            if (value < minimum)
            {
                return looped ? maximum : minimum;
            }

            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Clamp(this Int32 value)
        {
            return Clamp(value, default(Int32));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Clamp(this Int32 value, Boolean looped)
        {
            return Clamp(value, default, Int32.MaxValue, looped);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Clamp(this Int32 value, Int32 maximum)
        {
            return Clamp(value, default, maximum);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Clamp(this Int32 value, Int32 maximum, Boolean looped)
        {
            return Clamp(value, default, maximum, looped);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Clamp(this Int32 value, Int32 minimum, Int32 maximum)
        {
            return Math.Clamp(value, minimum, maximum);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Clamp(this Int32 value, Int32 minimum, Int32 maximum, Boolean looped)
        {
            if (value > maximum)
            {
                return looped ? minimum : maximum;
            }

            if (value < minimum)
            {
                return looped ? maximum : minimum;
            }

            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 Clamp(this UInt32 value)
        {
            return Clamp(value, default(UInt32));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 Clamp(this UInt32 value, Boolean looped)
        {
            return Clamp(value, default, UInt32.MaxValue, looped);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 Clamp(this UInt32 value, UInt32 maximum)
        {
            return Clamp(value, default, maximum);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 Clamp(this UInt32 value, UInt32 maximum, Boolean looped)
        {
            return Clamp(value, default, maximum, looped);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 Clamp(this UInt32 value, UInt32 minimum, UInt32 maximum)
        {
            return Math.Clamp(value, minimum, maximum);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 Clamp(this UInt32 value, UInt32 minimum, UInt32 maximum, Boolean looped)
        {
            if (value > maximum)
            {
                return looped ? minimum : maximum;
            }

            if (value < minimum)
            {
                return looped ? maximum : minimum;
            }

            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 Clamp(this Int64 value)
        {
            return Clamp(value, default(Int64));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 Clamp(this Int64 value, Boolean looped)
        {
            return Clamp(value, default, Int64.MaxValue, looped);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 Clamp(this Int64 value, Int64 maximum)
        {
            return Clamp(value, default, maximum);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 Clamp(this Int64 value, Int64 maximum, Boolean looped)
        {
            return Clamp(value, default, maximum, looped);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 Clamp(this Int64 value, Int64 minimum, Int64 maximum)
        {
            return Math.Clamp(value, minimum, maximum);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 Clamp(this Int64 value, Int64 minimum, Int64 maximum, Boolean looped)
        {
            if (value > maximum)
            {
                return looped ? minimum : maximum;
            }

            if (value < minimum)
            {
                return looped ? maximum : minimum;
            }

            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 Clamp(this UInt64 value)
        {
            return Clamp(value, default(UInt64));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 Clamp(this UInt64 value, Boolean looped)
        {
            return Clamp(value, default, UInt64.MaxValue, looped);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 Clamp(this UInt64 value, UInt64 maximum)
        {
            return Clamp(value, default, maximum);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 Clamp(this UInt64 value, UInt64 maximum, Boolean looped)
        {
            return Clamp(value, default, maximum, looped);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 Clamp(this UInt64 value, UInt64 minimum, UInt64 maximum)
        {
            return Math.Clamp(value, minimum, maximum);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 Clamp(this UInt64 value, UInt64 minimum, UInt64 maximum, Boolean looped)
        {
            if (value > maximum)
            {
                return looped ? minimum : maximum;
            }

            if (value < minimum)
            {
                return looped ? maximum : minimum;
            }

            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Clamp(this Single value)
        {
            return Clamp(value, default(Single));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Clamp(this Single value, Boolean looped)
        {
            return Clamp(value, default, Single.MaxValue, looped);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Clamp(this Single value, Single maximum)
        {
            return Clamp(value, default, maximum);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Clamp(this Single value, Single maximum, Boolean looped)
        {
            return Clamp(value, default, maximum, looped);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Clamp(this Single value, Single minimum, Single maximum)
        {
            return Math.Clamp(value, minimum, maximum);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Clamp(this Single value, Single minimum, Single maximum, Boolean looped)
        {
            if (value > maximum)
            {
                return looped ? minimum : maximum;
            }

            if (value < minimum)
            {
                return looped ? maximum : minimum;
            }

            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Clamp(this Double value)
        {
            return Clamp(value, default(Double));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Clamp(this Double value, Boolean looped)
        {
            return Clamp(value, default, Double.MaxValue, looped);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Clamp(this Double value, Double maximum)
        {
            return Clamp(value, default, maximum);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Clamp(this Double value, Double maximum, Boolean looped)
        {
            return Clamp(value, default, maximum, looped);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Clamp(this Double value, Double minimum, Double maximum)
        {
            return Math.Clamp(value, minimum, maximum);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Clamp(this Double value, Double minimum, Double maximum, Boolean looped)
        {
            if (value > maximum)
            {
                return looped ? minimum : maximum;
            }

            if (value < minimum)
            {
                return looped ? maximum : minimum;
            }

            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Clamp(this Decimal value)
        {
            return Clamp(value, default(Decimal));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Clamp(this Decimal value, Boolean looped)
        {
            return Clamp(value, default, Decimal.MaxValue, looped);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Clamp(this Decimal value, Decimal maximum)
        {
            return Clamp(value, default, maximum);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Clamp(this Decimal value, Decimal maximum, Boolean looped)
        {
            return Clamp(value, default, maximum, looped);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Clamp(this Decimal value, Decimal minimum, Decimal maximum)
        {
            return Math.Clamp(value, minimum, maximum);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Clamp(this Decimal value, Decimal minimum, Decimal maximum, Boolean looped)
        {
            if (value > maximum)
            {
                return looped ? minimum : maximum;
            }

            if (value < minimum)
            {
                return looped ? maximum : minimum;
            }

            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Char Difference(this Char value, Char between)
        {
            return value <= between ? (Char) (between - value) : (Char) (value - between);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Byte Difference(this SByte value, SByte between)
        {
            unchecked
            {
                return (Byte) (value >= between ? (Byte) value - (Byte) between : (Byte) between - (Byte) value);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Byte Difference(this Byte value, Byte between)
        {
            return value <= between ? (Byte) (between - value) : (Byte) (value - between);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt16 Difference(this Int16 value, Int16 between)
        {
            unchecked
            {
                return (UInt16) (value >= between ? (UInt16) value - (UInt16) between : (UInt16) between - (UInt16) value);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt16 Difference(this UInt16 value, UInt16 between)
        {
            return value <= between ? (UInt16) (between - value) : (UInt16) (value - between);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 Difference(this Int32 value, Int32 between)
        {
            unchecked
            {
                return value >= between ? (UInt32) value - (UInt32) between : (UInt32) between - (UInt32) value;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 Difference(this UInt32 value, UInt32 between)
        {
            return value <= between ? between - value : value - between;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 Difference(this Int64 value, Int64 between)
        {
            unchecked
            {
                return value >= between ? (UInt64) value - (UInt64) between : (UInt64) between - (UInt64) value;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 Difference(this UInt64 value, UInt64 between)
        {
            return value <= between ? between - value : value - between;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Difference(this Single value, Single between)
        {
            return value <= between ? Abs(between - value) : Abs(value - between);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Difference(this Double value, Double between)
        {
            return value <= between ? Abs(between - value) : Abs(value - between);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Difference(this Decimal value, Decimal between)
        {
            return value <= between ? Abs(between - value) : Abs(value - between);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInteger Difference(this BigInteger value, BigInteger between)
        {
            return value <= between ? Abs(between - value) : Abs(value - between);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Char DiscreteDifference(this Char value, Char between)
        {
            return Difference(value, between);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Byte DiscreteDifference(this SByte value, SByte between)
        {
            return Difference(value, between);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Byte DiscreteDifference(this Byte value, Byte between)
        {
            return Difference(value, between);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt16 DiscreteDifference(this Int16 value, Int16 between)
        {
            return Difference(value, between);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt16 DiscreteDifference(this UInt16 value, UInt16 between)
        {
            return Difference(value, between);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 DiscreteDifference(this Int32 value, Int32 between)
        {
            return Difference(value, between);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 DiscreteDifference(this UInt32 value, UInt32 between)
        {
            return Difference(value, between);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 DiscreteDifference(this Int64 value, Int64 between)
        {
            return Difference(value, between);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 DiscreteDifference(this UInt64 value, UInt64 between)
        {
            return Difference(value, between);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Char DiscreteIncludeDifference(this Char value, Char between)
        {
            return DiscreteIncludeDifference(value, between, Char.MaxValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Char DiscreteIncludeDifference(this Char value, Char between, Char overflow)
        {
            Char difference = DiscreteDifference(value, between);
            return difference < Char.MaxValue ? (Char) (difference + 1) : overflow;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Byte DiscreteIncludeDifference(this SByte value, SByte between)
        {
            return DiscreteIncludeDifference(value, between, Byte.MaxValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Byte DiscreteIncludeDifference(this SByte value, SByte between, Byte overflow)
        {
            Byte difference = DiscreteDifference(value, between);
            return difference < Byte.MaxValue ? (Byte) (difference + 1) : overflow;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Byte DiscreteIncludeDifference(this Byte value, Byte between)
        {
            return DiscreteIncludeDifference(value, between, Byte.MaxValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Byte DiscreteIncludeDifference(this Byte value, Byte between, Byte overflow)
        {
            Byte difference = DiscreteDifference(value, between);
            return difference < Byte.MaxValue ? (Byte) (difference + 1) : overflow;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt16 DiscreteIncludeDifference(this Int16 value, Int16 between)
        {
            return DiscreteIncludeDifference(value, between, UInt16.MaxValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt16 DiscreteIncludeDifference(this Int16 value, Int16 between, UInt16 overflow)
        {
            UInt16 difference = DiscreteDifference(value, between);
            return difference < UInt16.MaxValue ? (UInt16) (difference + 1) : overflow;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt16 DiscreteIncludeDifference(this UInt16 value, UInt16 between)
        {
            return DiscreteIncludeDifference(value, between, UInt16.MaxValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt16 DiscreteIncludeDifference(this UInt16 value, UInt16 between, UInt16 overflow)
        {
            UInt16 difference = DiscreteDifference(value, between);
            return difference < UInt16.MaxValue ? (UInt16) (difference + 1) : overflow;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 DiscreteIncludeDifference(this Int32 value, Int32 between)
        {
            return DiscreteIncludeDifference(value, between, UInt32.MaxValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 DiscreteIncludeDifference(this Int32 value, Int32 between, UInt32 overflow)
        {
            UInt32 difference = DiscreteDifference(value, between);
            return difference < UInt32.MaxValue ? difference + 1 : overflow;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 DiscreteIncludeDifference(this UInt32 value, UInt32 between)
        {
            return DiscreteIncludeDifference(value, between, UInt32.MaxValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 DiscreteIncludeDifference(this UInt32 value, UInt32 between, UInt32 overflow)
        {
            UInt32 difference = DiscreteDifference(value, between);
            return difference < UInt32.MaxValue ? difference + 1 : overflow;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 DiscreteIncludeDifference(this Int64 value, Int64 between)
        {
            return DiscreteIncludeDifference(value, between, UInt64.MaxValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 DiscreteIncludeDifference(this Int64 value, Int64 between, UInt64 overflow)
        {
            UInt64 difference = DiscreteDifference(value, between);
            return difference < UInt64.MaxValue ? difference + 1 : overflow;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 DiscreteIncludeDifference(this UInt64 value, UInt64 between)
        {
            return DiscreteIncludeDifference(value, between, UInt64.MaxValue);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 DiscreteIncludeDifference(this UInt64 value, UInt64 between, UInt64 overflow)
        {
            UInt64 difference = DiscreteDifference(value, between);
            return difference < UInt64.MaxValue ? difference + 1 : overflow;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double ToRadians(this SByte value)
        {
            return ToRadians((Double) value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double ToRadians(this Byte value)
        {
            return ToRadians((Double) value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double ToRadians(this Int16 value)
        {
            return ToRadians((Double) value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double ToRadians(this UInt16 value)
        {
            return ToRadians((Double) value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double ToRadians(this Int32 value)
        {
            return ToRadians((Double) value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double ToRadians(this UInt32 value)
        {
            return ToRadians((Double) value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double ToRadians(this Int64 value)
        {
            return ToRadians((Double) value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double ToRadians(this UInt64 value)
        {
            return ToRadians((Double) value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToNonZero(ref SByte value)
        {
            value = ToNonZero(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToNonZero(ref SByte value, SByte alternate)
        {
            value = ToNonZero(value, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SByte ToNonZero(this SByte value)
        {
            return ToNonZero(value, (SByte) 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SByte ToNonZero(this SByte value, SByte alternate)
        {
            return value == 0 ? alternate : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToNonZero(ref Byte value)
        {
            value = ToNonZero(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToNonZero(ref Byte value, Byte alternate)
        {
            value = ToNonZero(value, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Byte ToNonZero(this Byte value)
        {
            return ToNonZero(value, (Byte) 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Byte ToNonZero(this Byte value, Byte alternate)
        {
            return value == 0 ? alternate : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToNonZero(ref Int16 value)
        {
            value = ToNonZero(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToNonZero(ref Int16 value, Int16 alternate)
        {
            value = ToNonZero(value, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int16 ToNonZero(this Int16 value)
        {
            return ToNonZero(value, (Int16) 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int16 ToNonZero(this Int16 value, Int16 alternate)
        {
            return value == 0 ? alternate : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToNonZero(ref UInt16 value)
        {
            value = ToNonZero(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToNonZero(ref UInt16 value, UInt16 alternate)
        {
            value = ToNonZero(value, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt16 ToNonZero(this UInt16 value)
        {
            return ToNonZero(value, (UInt16) 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt16 ToNonZero(this UInt16 value, UInt16 alternate)
        {
            return value == 0 ? alternate : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToNonZero(ref Int32 value)
        {
            value = ToNonZero(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToNonZero(ref Int32 value, Int32 alternate)
        {
            value = ToNonZero(value, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 ToNonZero(this Int32 value)
        {
            return ToNonZero(value, 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 ToNonZero(this Int32 value, Int32 alternate)
        {
            return value == 0 ? alternate : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToNonZero(ref UInt32 value)
        {
            value = ToNonZero(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToNonZero(ref UInt32 value, UInt32 alternate)
        {
            value = ToNonZero(value, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 ToNonZero(this UInt32 value)
        {
            return ToNonZero(value, 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 ToNonZero(this UInt32 value, UInt32 alternate)
        {
            return value == 0 ? alternate : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToNonZero(ref Int64 value)
        {
            value = ToNonZero(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToNonZero(ref Int64 value, Int64 alternate)
        {
            value = ToNonZero(value, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 ToNonZero(this Int64 value)
        {
            return ToNonZero(value, 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 ToNonZero(this Int64 value, Int64 alternate)
        {
            return value == 0 ? alternate : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToNonZero(ref UInt64 value)
        {
            value = ToNonZero(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToNonZero(ref UInt64 value, UInt64 alternate)
        {
            value = ToNonZero(value, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 ToNonZero(this UInt64 value)
        {
            return ToNonZero(value, 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 ToNonZero(this UInt64 value, UInt64 alternate)
        {
            return value == 0 ? alternate : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToNonZero(ref Single value)
        {
            value = ToNonZero(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToNonZero(ref Single value, Single alternate)
        {
            value = ToNonZero(value, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single ToNonZero(this Single value)
        {
            return ToNonZero(value, 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single ToNonZero(this Single value, Single alternate)
        {
            return IsEpsilon(value) ? alternate : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToNonZero(ref Double value)
        {
            value = ToNonZero(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToNonZero(ref Double value, Double alternate)
        {
            value = ToNonZero(value, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double ToNonZero(this Double value)
        {
            return ToNonZero(value, 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double ToNonZero(this Double value, Double alternate)
        {
            return IsEpsilon(value) ? alternate : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToNonZero(ref Decimal value)
        {
            value = ToNonZero(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToNonZero(ref Decimal value, Decimal alternate)
        {
            value = ToNonZero(value, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal ToNonZero(this Decimal value)
        {
            return ToNonZero(value, 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal ToNonZero(this Decimal value, Decimal alternate)
        {
            return value == 0 ? alternate : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToNonNegative(ref SByte value)
        {
            value = ToNonNegative(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToNonNegative(ref SByte value, SByte alternate)
        {
            value = ToNonNegative(value, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SByte ToNonNegative(this SByte value)
        {
            return value < 0 ? (SByte) 0 : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SByte ToNonNegative(this SByte value, SByte alternate)
        {
            return value < 0 ? alternate : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToNonNegative(ref Byte value)
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToNonNegative(ref Byte value, Byte alternate)
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Byte ToNonNegative(this Byte value)
        {
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Byte ToNonNegative(this Byte value, Byte alternate)
        {
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToNonNegative(ref Int16 value)
        {
            value = ToNonNegative(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToNonNegative(ref Int16 value, Int16 alternate)
        {
            value = ToNonNegative(value, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int16 ToNonNegative(this Int16 value)
        {
            return value < 0 ? (Int16) 0 : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int16 ToNonNegative(this Int16 value, Int16 alternate)
        {
            return value < 0 ? alternate : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToNonNegative(ref UInt16 value)
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToNonNegative(ref UInt16 value, UInt16 alternate)
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt16 ToNonNegative(this UInt16 value)
        {
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt16 ToNonNegative(this UInt16 value, UInt16 alternate)
        {
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToNonNegative(ref Int32 value)
        {
            value = ToNonNegative(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToNonNegative(ref Int32 value, Int32 alternate)
        {
            value = ToNonNegative(value, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 ToNonNegative(this Int32 value)
        {
            return value < 0 ? 0 : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 ToNonNegative(this Int32 value, Int32 alternate)
        {
            return value < 0 ? alternate : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToNonNegative(ref UInt32 value)
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToNonNegative(ref UInt32 value, UInt32 alternate)
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 ToNonNegative(this UInt32 value)
        {
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 ToNonNegative(this UInt32 value, UInt32 alternate)
        {
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToNonNegative(ref Int64 value)
        {
            value = ToNonNegative(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToNonNegative(ref Int64 value, Int64 alternate)
        {
            value = ToNonNegative(value, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 ToNonNegative(this Int64 value)
        {
            return value < 0 ? 0 : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 ToNonNegative(this Int64 value, Int64 alternate)
        {
            return value < 0 ? alternate : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToNonNegative(ref UInt64 value)
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToNonNegative(ref UInt64 value, UInt64 alternate)
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 ToNonNegative(this UInt64 value)
        {
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 ToNonNegative(this UInt64 value, UInt64 alternate)
        {
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToNonNegative(ref Single value)
        {
            value = ToNonNegative(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToNonNegative(ref Single value, Single alternate)
        {
            value = ToNonNegative(value, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single ToNonNegative(this Single value)
        {
            return value < 0 ? 0 : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single ToNonNegative(this Single value, Single alternate)
        {
            return value < 0 ? alternate : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToNonNegative(ref Double value)
        {
            value = ToNonNegative(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToNonNegative(ref Double value, Double alternate)
        {
            value = ToNonNegative(value, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double ToNonNegative(this Double value)
        {
            return value < 0 ? 0 : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double ToNonNegative(this Double value, Double alternate)
        {
            return value < 0 ? alternate : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToNonNegative(ref Decimal value)
        {
            value = ToNonNegative(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToNonNegative(ref Decimal value, Decimal alternate)
        {
            value = ToNonNegative(value, alternate);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal ToNonNegative(this Decimal value)
        {
            return value < 0 ? 0 : value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal ToNonNegative(this Decimal value, Decimal alternate)
        {
            return value < 0 ? alternate : value;
        }

        /// <summary>
        /// Calculates the mean value of the provided values.
        /// </summary>
        /// <param name="values">The values.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SByte Mean(this IEnumerable<SByte> values)
        {
            UInt32 count = 0;
            Int64 sum = 0;
            foreach (SByte value in values)
            {
                sum += value;
                ++count;
            }

            if (count == 0 || count > sum)
            {
                return 0;
            }

            return (SByte)(sum / count);
        }

        /// <summary>
        /// Calculates the mean value of the provided values.
        /// </summary>
        /// <param name="values">The values.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Byte Mean(this IEnumerable<Byte> values)
        {
            UInt32 count = 0;
            UInt64 sum = 0;
            foreach (Byte value in values)
            {
                sum += value;
                ++count;
            }

            if (count == 0 || count > sum)
            {
                return 0;
            }

            return (Byte)(sum / count);
        }

        /// <summary>
        /// Calculates the mean value of the provided values.
        /// </summary>
        /// <param name="values">The values.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int16 Mean(this IEnumerable<Int16> values)
        {
            UInt32 count = 0;
            Int64 sum = 0;
            foreach (Int16 value in values)
            {
                sum += value;
                ++count;
            }

            if (count == 0 || count > sum)
            {
                return 0;
            }

            return (Int16)(sum / count);
        }

        /// <summary>
        /// Calculates the mean value of the provided values.
        /// </summary>
        /// <param name="values">The values.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt16 Mean(this IEnumerable<UInt16> values)
        {
            UInt32 count = 0;
            UInt64 sum = 0;
            foreach (UInt16 value in values)
            {
                sum += value;
                ++count;
            }

            if (count == 0 || count > sum)
            {
                return 0;
            }

            return (UInt16)(sum / count);
        }

        /// <summary>
        /// Calculates the mean value of the provided values.
        /// </summary>
        /// <param name="values">The values.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 Mean(this IEnumerable<Int32> values)
        {
            UInt32 count = 0;
            Int64 sum = 0;
            foreach (Int32 value in values)
            {
                sum += value;
                ++count;
            }

            if (count == 0 || count > sum)
            {
                return 0;
            }

            return (Int32)(sum / count);
        }

        /// <summary>
        /// Calculates the mean value of the provided values.
        /// </summary>
        /// <param name="values">The values.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 Mean(this IEnumerable<UInt32> values)
        {
            UInt32 count = 0;
            UInt64 sum = 0;
            foreach (UInt32 value in values)
            {
                sum += value;
                ++count;
            }

            if (count == 0 || count > sum)
            {
                return 0;
            }

            return (UInt32)(sum / count);
        }

        /// <summary>
        /// Calculates the mean value of the provided values.
        /// </summary>
        /// <param name="values">The values.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 Mean(this IEnumerable<Int64> values)
        {
            UInt32 count = 0;
            Int64 sum = 0;
            foreach (Int64 value in values)
            {
                sum += value;
                ++count;
            }

            if (count == 0 || count > sum)
            {
                return 0;
            }

            return sum / count;
        }

        /// <summary>
        /// Calculates the mean value of the provided values.
        /// </summary>
        /// <param name="values">The values.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 Mean(this IEnumerable<UInt64> values)
        {
            UInt32 count = 0;
            UInt64 sum = 0;
            foreach (UInt64 value in values)
            {
                sum += value;
                ++count;
            }

            if (count == 0 || count > sum)
            {
                return 0;
            }

            return sum / count;
        }

        /// <summary>
        /// Calculates the mean value of the provided values.
        /// </summary>
        /// <param name="values">The values.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Mean(this IEnumerable<Single> values)
        {
            UInt32 count = 0;
            Double sum = 0;
            foreach (Single value in values)
            {
                sum += value;
                ++count;
            }

            if (count == 0 || count > sum)
            {
                return 0;
            }

            return (Single)(sum / count);
        }

        /// <summary>
        /// Calculates the mean value of the provided values.
        /// </summary>
        /// <param name="values">The values.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Mean(this IEnumerable<Double> values)
        {
            UInt32 count = 0;
            Double sum = 0;
            foreach (Double value in values)
            {
                sum += value;
                ++count;
            }

            if (count == 0 || count > sum)
            {
                return 0;
            }

            return sum / count;
        }

        /// <summary>
        /// Calculates the mean value of the provided values.
        /// </summary>
        /// <param name="values">The values.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Mean(this IEnumerable<Decimal> values)
        {
            UInt32 count = 0;
            Decimal sum = 0;
            foreach (Decimal value in values)
            {
                sum += value;
                ++count;
            }

            if (count == 0 || count > sum)
            {
                return 0;
            }

            return sum / count;
        }

        /// <summary>
        /// Calculates the mean value of the provided values.
        /// </summary>
        /// <param name="values">The values.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInteger Mean(this IEnumerable<BigInteger> values)
        {
            UInt32 count = 0;
            BigInteger sum = 0;
            foreach (BigInteger value in values)
            {
                sum += value;
                ++count;
            }

            if (count == 0 || count > sum)
            {
                return 0;
            }

            return sum / count;
        }

        private const Byte MinimumBase = 2;
        private const Byte MaximumBase = 36;
        private const Int32 ZeroChar = '0';
        private const Int32 AlphabetStart = '9' - MinimumBase;

        public static String ToBase(this SByte value, Byte @base)
        {
            if (!@base.InRange(MinimumBase, MaximumBase))
            {
                throw new ArgumentOutOfRangeException(nameof(@base), @"Base out of range");
            }

            if (value == 0)
            {
                return "0";
            }

            Boolean negative = value < 0;
            if (negative)
            {
                Abs(ref value);
            }

            // 64 is the worst cast buffer size for base 2 and SByte.MaxValue
            const Int32 max = 8 * sizeof(SByte);
            Int32 i = max;
            Span<Char> buffer = stackalloc Char[max];

            do
            {
                Int32 number = value % @base;
                buffer[--i] = (Char)(number < 10 ? ZeroChar + number : AlphabetStart + number);

                value /= (SByte)@base;
            } while (value > 0);

            return $"{(negative ? "-" : String.Empty)}" + new String(buffer.Slice(i, max - i));
        }

        public static String ToBase(this Byte value, Byte @base)
        {
            if (!@base.InRange(MinimumBase, MaximumBase))
            {
                throw new ArgumentOutOfRangeException(nameof(@base), @"Base out of range");
            }

            if (value <= 0)
            {
                return "0";
            }

            // 64 is the worst cast buffer size for base 2 and Byte.MaxValue
            const Int32 max = 8 * sizeof(Byte);
            Int32 i = max;
            Span<Char> buffer = stackalloc Char[max];

            do
            {
                Int32 number = value % @base;
                buffer[--i] = (Char)(number < 10 ? ZeroChar + number : AlphabetStart + number);

                value /= @base;
            } while (value > 0);

            return new String(buffer.Slice(i, max - i));
        }

        public static String ToBase(this Int16 value, Byte @base)
        {
            if (!@base.InRange(MinimumBase, MaximumBase))
            {
                throw new ArgumentOutOfRangeException(nameof(@base), @"Base out of range");
            }

            if (value == 0)
            {
                return "0";
            }

            Boolean negative = value < 0;
            if (negative)
            {
                Abs(ref value);
            }

            // 64 is the worst cast buffer size for base 2 and Int16.MaxValue
            const Int32 max = 8 * sizeof(Int16);
            Int32 i = max;
            Span<Char> buffer = stackalloc Char[max];

            do
            {
                Int32 number = value % @base;
                buffer[--i] = (Char)(number < 10 ? ZeroChar + number : AlphabetStart + number);

                value /= @base;
            } while (value > 0);

            return $"{(negative ? "-" : String.Empty)}" + new String(buffer.Slice(i, max - i));
        }

        public static String ToBase(this UInt16 value, Byte @base)
        {
            if (!@base.InRange(MinimumBase, MaximumBase))
            {
                throw new ArgumentOutOfRangeException(nameof(@base), @"Base out of range");
            }

            if (value <= 0)
            {
                return "0";
            }

            // 64 is the worst cast buffer size for base 2 and UInt16.MaxValue
            const Int32 max = 8 * sizeof(UInt16);
            Int32 i = max;
            Span<Char> buffer = stackalloc Char[max];

            do
            {
                Int32 number = value % @base;
                buffer[--i] = (Char)(number < 10 ? ZeroChar + number : AlphabetStart + number);

                value /= @base;
            } while (value > 0);

            return new String(buffer.Slice(i, max - i));
        }

        public static String ToBase(this Int32 value, Byte @base)
        {
            if (!@base.InRange(MinimumBase, MaximumBase))
            {
                throw new ArgumentOutOfRangeException(nameof(@base), @"Base out of range");
            }

            if (value == 0)
            {
                return "0";
            }

            Boolean negative = value < 0;
            if (negative)
            {
                Abs(ref value);
            }

            // 64 is the worst cast buffer size for base 2 and Int32.MaxValue
            const Int32 max = 8 * sizeof(Int32);
            Int32 i = max;
            Span<Char> buffer = stackalloc Char[max];

            do
            {
                Int32 number = value % @base;
                buffer[--i] = (Char)(number < 10 ? ZeroChar + number : AlphabetStart + number);

                value /= @base;
            } while (value > 0);

            return $"{(negative ? "-" : String.Empty)}" + new String(buffer.Slice(i, max - i));
        }

        public static String ToBase(this UInt32 value, Byte @base)
        {
            if (!@base.InRange(MinimumBase, MaximumBase))
            {
                throw new ArgumentOutOfRangeException(nameof(@base), @"Base out of range");
            }

            if (value <= 0)
            {
                return "0";
            }

            // 64 is the worst cast buffer size for base 2 and UInt32.MaxValue
            const Int32 max = 8 * sizeof(UInt32);
            Int32 i = max;
            Span<Char> buffer = stackalloc Char[max];

            do
            {
                UInt32 number = value % @base;
                buffer[--i] = (Char)(number < 10 ? ZeroChar + number : AlphabetStart + number);

                value /= @base;
            } while (value > 0);

            return new String(buffer.Slice(i, max - i));
        }

        public static String ToBase(this Int64 value, Byte @base)
        {
            if (!@base.InRange(MinimumBase, MaximumBase))
            {
                throw new ArgumentOutOfRangeException(nameof(@base), @"Base out of range");
            }

            if (value == 0)
            {
                return "0";
            }

            Boolean negative = value < 0;
            if (negative)
            {
                Abs(ref value);
            }

            // 64 is the worst cast buffer size for base 2 and Int64.MaxValue
            const Int32 max = 8 * sizeof(Int64);
            Int32 i = max;
            Span<Char> buffer = stackalloc Char[max];

            do
            {
                Int64 number = value % @base;
                buffer[--i] = (Char)(number < 10 ? ZeroChar + number : AlphabetStart + number);

                value /= @base;
            } while (value > 0);

            return $"{(negative ? "-" : String.Empty)}" + new String(buffer.Slice(i, max - i));
        }

        public static String ToBase(this UInt64 value, Byte @base)
        {
            if (!@base.InRange(MinimumBase, MaximumBase))
            {
                throw new ArgumentOutOfRangeException(nameof(@base), @"Base out of range");
            }

            if (value <= 0)
            {
                return "0";
            }

            // 64 is the worst cast buffer size for base 2 and UInt64.MaxValue
            const Int32 max = 8 * sizeof(UInt64);
            Int32 i = max;
            Span<Char> buffer = stackalloc Char[max];

            do
            {
                UInt64 number = value % @base;
                buffer[--i] = (Char)(number < 10 ? ZeroChar + number : AlphabetStart + number);

                value /= @base;
            } while (value > 0);

            return new String(buffer.Slice(i, max - i));
        }

        public static String ToBase(this BigInteger value, Byte @base)
        {
            if (!@base.InRange(MinimumBase, MaximumBase))
            {
                throw new ArgumentOutOfRangeException(nameof(@base), @"Base out of range");
            }

            if (value == 0)
            {
                return "0";
            }

            Boolean negative = value < 0;
            if (negative)
            {
                Abs(ref value);
            }

            Int32 max = (Int32) Round(Log(value, 2), MidpointRounding.ToPositiveInfinity);
            Int32 i = max;
            Span<Char> buffer = stackalloc Char[max];

            do
            {
                BigInteger number = value % @base;
                buffer[--i] = (Char)(number < 10 ? ZeroChar + number : AlphabetStart + number);

                value /= @base;
            } while (value > 0);

            return $"{(negative ? "-" : String.Empty)}" + new String(buffer.Slice(i, max - i));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SByte Sum(this IEnumerable<SByte> source)
        {
            checked
            {
                return source.Aggregate<SByte, SByte>(0, (current, value) => (SByte) (current + value));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SByte Sum(this IEnumerable<SByte> source, SByte overflow)
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
        public static Byte Sum(this IEnumerable<Byte> source)
        {
            checked
            {
                return source.Aggregate<Byte, Byte>(0, (current, value) => (Byte) (current + value));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Byte Sum(this IEnumerable<Byte> source, Byte overflow)
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
        public static Int16 Sum(this IEnumerable<Int16> source)
        {
            checked
            {
                return source.Aggregate<Int16, Int16>(0, (current, value) => (Int16) (current + value));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int16 Sum(this IEnumerable<Int16> source, Int16 overflow)
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
        public static UInt16 Sum(this IEnumerable<UInt16> source)
        {
            checked
            {
                return source.Aggregate<UInt16, UInt16>(0, (current, value) => (UInt16) (current + value));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt16 Sum(this IEnumerable<UInt16> source, UInt16 overflow)
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
        public static Int32 Sum(this IEnumerable<Int32> source, Int32 overflow)
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
        public static UInt32 Sum(this IEnumerable<UInt32> source)
        {
            checked
            {
                return source.Aggregate<UInt32, UInt32>(0, (current, value) => current + value);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 Sum(this IEnumerable<UInt32> source, UInt32 overflow)
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
        public static Int64 Sum(this IEnumerable<Int64> source, Int64 overflow)
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
        public static UInt64 Sum(this IEnumerable<UInt64> source)
        {
            checked
            {
                return source.Aggregate<UInt64, UInt64>(0, (current, value) => current + value);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 Sum(this IEnumerable<UInt64> source, UInt64 overflow)
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
        public static Single Sum(this IEnumerable<Single> source, Single overflow)
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
        public static Double Sum(this IEnumerable<Double> source, Double overflow)
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
        public static Decimal Sum(this IEnumerable<Decimal> source, Decimal overflow)
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
        public static BigInteger Sum(this IEnumerable<BigInteger> source)
        {
            checked
            {
                return source.Aggregate<BigInteger, BigInteger>(0, (current, value) => current + value);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BigInteger Sum(this IEnumerable<BigInteger> source, BigInteger overflow)
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
        public static Complex Sum(this IEnumerable<Complex> source)
        {
            checked
            {
                return source.Aggregate<Complex, Complex>(0, (current, value) => current + value);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Complex Sum(this IEnumerable<Complex> source, Complex overflow)
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
        public static SByte Multiply(this IEnumerable<SByte> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            checked
            {
                using IEnumerator<SByte> enumerator = source.GetEnumerator();

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
        public static SByte Multiply(this IEnumerable<SByte> source, SByte overflow)
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
        public static Byte Multiply(this IEnumerable<Byte> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            checked
            {
                using IEnumerator<Byte> enumerator = source.GetEnumerator();

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
        public static Byte Multiply(this IEnumerable<Byte> source, Byte overflow)
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
        public static Int16 Multiply(this IEnumerable<Int16> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            checked
            {
                using IEnumerator<Int16> enumerator = source.GetEnumerator();

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
        public static Int16 Multiply(this IEnumerable<Int16> source, Int16 overflow)
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
        public static UInt16 Multiply(this IEnumerable<UInt16> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            checked
            {
                using IEnumerator<UInt16> enumerator = source.GetEnumerator();

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
        public static UInt16 Multiply(this IEnumerable<UInt16> source, UInt16 overflow)
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
        public static Int32 Multiply(this IEnumerable<Int32> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            checked
            {
                using IEnumerator<Int32> enumerator = source.GetEnumerator();

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
        public static Int32 Multiply(this IEnumerable<Int32> source, Int32 overflow)
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
        public static UInt32 Multiply(this IEnumerable<UInt32> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            checked
            {
                using IEnumerator<UInt32> enumerator = source.GetEnumerator();

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
        public static UInt32 Multiply(this IEnumerable<UInt32> source, UInt32 overflow)
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
        public static Int64 Multiply(this IEnumerable<Int64> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            checked
            {
                using IEnumerator<Int64> enumerator = source.GetEnumerator();

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
        public static Int64 Multiply(this IEnumerable<Int64> source, Int64 overflow)
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
        public static UInt64 Multiply(this IEnumerable<UInt64> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            checked
            {
                using IEnumerator<UInt64> enumerator = source.GetEnumerator();

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
        public static UInt64 Multiply(this IEnumerable<UInt64> source, UInt64 overflow)
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
        public static Single Multiply(this IEnumerable<Single> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            checked
            {
                using IEnumerator<Single> enumerator = source.GetEnumerator();

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
        public static Single Multiply(this IEnumerable<Single> source, Single overflow)
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
        public static Double Multiply(this IEnumerable<Double> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            checked
            {
                using IEnumerator<Double> enumerator = source.GetEnumerator();

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
        public static Double Multiply(this IEnumerable<Double> source, Double overflow)
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
        public static Decimal Multiply(this IEnumerable<Decimal> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            checked
            {
                using IEnumerator<Decimal> enumerator = source.GetEnumerator();

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
        public static Decimal Multiply(this IEnumerable<Decimal> source, Decimal overflow)
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Average(this IEnumerable<SByte> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Select(item => (Double) item).Average();
        }

        [SuppressMessage("ReSharper", "SuspiciousTypeConversion.Global")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Average(this IEnumerable<Byte> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Select(item => (Double) item).Average();
        }

        [SuppressMessage("ReSharper", "SuspiciousTypeConversion.Global")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Average(this IEnumerable<Int16> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Select(item => (Double) item).Average();
        }

        [SuppressMessage("ReSharper", "SuspiciousTypeConversion.Global")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Average(this IEnumerable<UInt16> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Select(item => (Double) item).Average();
        }

        [SuppressMessage("ReSharper", "SuspiciousTypeConversion.Global")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Average(this IEnumerable<UInt32> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Select(item => (Double) item).Average();
        }

        [SuppressMessage("ReSharper", "SuspiciousTypeConversion.Global")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Average(this IEnumerable<UInt64> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Select(item => (Double) item).Average();
        }

        public static Double Variance(this IEnumerable<SByte> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            ICollection<SByte> values = source as ICollection<SByte> ?? source.ToList();

            if (values.Count <= 0)
            {
                return 0;
            }

            Double mean = values.Average();
            Double sum = values.Sum(x => (x - mean).Pow(2));
            return sum / values.Count;
        }

        public static Double Variance(this IEnumerable<Byte> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            ICollection<Byte> values = source as ICollection<Byte> ?? source.ToList();

            if (values.Count <= 0)
            {
                return 0;
            }

            Double mean = values.Average();
            Double sum = values.Sum(x => (x - mean).Pow(2));
            return sum / values.Count;
        }

        public static Double Variance(this IEnumerable<Int16> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            ICollection<Int16> values = source as ICollection<Int16> ?? source.ToList();

            if (values.Count <= 0)
            {
                return 0;
            }

            Double mean = values.Average();
            Double sum = values.Sum(x => (x - mean).Pow(2));
            return sum / values.Count;
        }

        public static Double Variance(this IEnumerable<UInt16> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            ICollection<UInt16> values = source as ICollection<UInt16> ?? source.ToList();

            if (values.Count <= 0)
            {
                return 0;
            }

            Double mean = values.Average();
            Double sum = values.Sum(x => (x - mean).Pow(2));
            return sum / values.Count;
        }

        public static Double Variance(this IEnumerable<Int32> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            ICollection<Int32> values = source as ICollection<Int32> ?? source.ToList();

            if (values.Count <= 0)
            {
                return 0;
            }

            Double mean = values.Average();
            Double sum = values.Sum(x => (x - mean).Pow(2));
            return sum / values.Count;
        }

        public static Double Variance(this IEnumerable<UInt32> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            ICollection<UInt32> values = source as ICollection<UInt32> ?? source.ToList();

            if (values.Count <= 0)
            {
                return 0;
            }

            Double mean = values.Average();
            Double sum = values.Sum(x => (x - mean).Pow(2));
            return sum / values.Count;
        }

        public static Double Variance(this IEnumerable<Int64> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            ICollection<Int64> values = source as ICollection<Int64> ?? source.ToList();

            if (values.Count <= 0)
            {
                return 0;
            }

            Double mean = values.Average();
            Double sum = values.Sum(x => (x - mean).Pow(2));
            return sum / values.Count;
        }

        public static Double Variance(this IEnumerable<UInt64> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            ICollection<UInt64> values = source as ICollection<UInt64> ?? source.ToList();

            if (values.Count <= 0)
            {
                return 0;
            }

            Double mean = values.Average();
            Double sum = values.Sum(x => (x - mean).Pow(2));
            return sum / values.Count;
        }

        public static Double Variance(this IEnumerable<Single> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            ICollection<Single> values = source as ICollection<Single> ?? source.ToList();

            if (values.Count <= 0)
            {
                return 0;
            }

            Double mean = values.Average();
            Double sum = values.Sum(x => (x - mean).Pow(2));
            return sum / values.Count;
        }

        public static Double Variance(this IEnumerable<Double> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            ICollection<Double> values = source as ICollection<Double> ?? source.ToList();

            if (values.Count <= 0)
            {
                return 0;
            }

            Double mean = values.Average();
            Double sum = values.Sum(x => (x - mean).Pow(2));
            return sum / values.Count;
        }

        public static Decimal Variance(this IEnumerable<Decimal> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            ICollection<Decimal> values = source as ICollection<Decimal> ?? source.ToList();

            if (values.Count <= 0)
            {
                return 0;
            }

            Decimal mean = values.Average();
            Decimal sum = values.Sum(x => (x - mean).Pow(2));
            return sum / values.Count;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double StandardDeviation(this IEnumerable<SByte> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Variance().Sqrt();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double StandardDeviation(this IEnumerable<Byte> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Variance().Sqrt();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double StandardDeviation(this IEnumerable<Int16> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Variance().Sqrt();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double StandardDeviation(this IEnumerable<UInt16> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Variance().Sqrt();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double StandardDeviation(this IEnumerable<Int32> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Variance().Sqrt();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double StandardDeviation(this IEnumerable<UInt32> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Variance().Sqrt();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double StandardDeviation(this IEnumerable<Int64> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Variance().Sqrt();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double StandardDeviation(this IEnumerable<UInt64> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Variance().Sqrt();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double StandardDeviation(this IEnumerable<Single> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Variance().Sqrt();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double StandardDeviation(this IEnumerable<Double> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Variance().Sqrt();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal StandardDeviation(this IEnumerable<Decimal> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Variance().Sqrt();
        }

        [SuppressMessage("ReSharper", "SuspiciousTypeConversion.Global")]
        public static Int32 Reverse(this SByte value)
        {
            Int32 reverse = 0;

            while (value != 0)
            {
                reverse = reverse * 10 + value % 10;
                value /= 10;
            }

            return reverse;
        }

        [SuppressMessage("ReSharper", "SuspiciousTypeConversion.Global")]
        public static Int32 Reverse(this Byte value)
        {
            Int32 reverse = 0;

            while (value != 0)
            {
                reverse = reverse * 10 + value % 10;
                value /= 10;
            }

            return reverse;
        }

        [SuppressMessage("ReSharper", "SuspiciousTypeConversion.Global")]
        public static Int32 Reverse(this Int16 value)
        {
            Int32 reverse = 0;

            while (value != 0)
            {
                reverse = reverse * 10 + value % 10;
                value /= 10;
            }

            return reverse;
        }

        [SuppressMessage("ReSharper", "SuspiciousTypeConversion.Global")]
        public static Int32 Reverse(this UInt16 value)
        {
            Int32 reverse = 0;

            while (value != 0)
            {
                reverse = reverse * 10 + value % 10;
                value /= 10;
            }

            return reverse;
        }

        [SuppressMessage("ReSharper", "SuspiciousTypeConversion.Global")]
        public static Int32 Reverse(this Int32 value)
        {
            Int32 reverse = 0;

            while (value != 0)
            {
                reverse = reverse * 10 + value % 10;
                value /= 10;
            }

            return reverse;
        }

        [SuppressMessage("ReSharper", "SuspiciousTypeConversion.Global")]
        public static UInt32 Reverse(this UInt32 value)
        {
            UInt32 reverse = 0;

            while (value != 0)
            {
                reverse = reverse * 10 + value % 10;
                value /= 10;
            }

            return reverse;
        }

        [SuppressMessage("ReSharper", "SuspiciousTypeConversion.Global")]
        public static Int64 Reverse(this Int64 value)
        {
            Int64 reverse = 0;

            while (value != 0)
            {
                reverse = reverse * 10 + value % 10;
                value /= 10;
            }

            return reverse;
        }

        [SuppressMessage("ReSharper", "SuspiciousTypeConversion.Global")]
        public static UInt64 Reverse(this UInt64 value)
        {
            UInt64 reverse = 0;

            while (value != 0)
            {
                reverse = reverse * 10 + value % 10;
                value /= 10;
            }

            return reverse;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Percent(this SByte value, SByte percent)
        {
            return value * (percent / 100D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Percent(this SByte value, Byte percent)
        {
            return value * (percent / 100D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Percent(this SByte value, Int16 percent)
        {
            return value * (percent / 100D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Percent(this SByte value, UInt16 percent)
        {
            return value * (percent / 100D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Percent(this SByte value, Int32 percent)
        {
            return value * (percent / 100D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Percent(this SByte value, UInt32 percent)
        {
            return value * (percent / 100D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Percent(this SByte value, Int64 percent)
        {
            return value * (percent / 100D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Percent(this SByte value, UInt64 percent)
        {
            return value * (percent / 100D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Percent(this SByte value, Single percent)
        {
            return value * (percent / 100F);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Percent(this SByte value, Double percent)
        {
            return value * (percent / 100D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Percent(this SByte value, Decimal percent)
        {
            return value * (percent / 100M);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Percent(this Byte value, SByte percent)
        {
            return value * (percent / 100D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Percent(this Byte value, Byte percent)
        {
            return value * (percent / 100D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Percent(this Byte value, Int16 percent)
        {
            return value * (percent / 100D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Percent(this Byte value, UInt16 percent)
        {
            return value * (percent / 100D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Percent(this Byte value, Int32 percent)
        {
            return value * (percent / 100D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Percent(this Byte value, UInt32 percent)
        {
            return value * (percent / 100D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Percent(this Byte value, Int64 percent)
        {
            return value * (percent / 100D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Percent(this Byte value, UInt64 percent)
        {
            return value * (percent / 100D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Percent(this Byte value, Single percent)
        {
            return value * (percent / 100F);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Percent(this Byte value, Double percent)
        {
            return value * (percent / 100D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Percent(this Byte value, Decimal percent)
        {
            return value * (percent / 100M);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Percent(this Int16 value, SByte percent)
        {
            return value * (percent / 100D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Percent(this Int16 value, Byte percent)
        {
            return value * (percent / 100D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Percent(this Int16 value, Int16 percent)
        {
            return value * (percent / 100D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Percent(this Int16 value, UInt16 percent)
        {
            return value * (percent / 100D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Percent(this Int16 value, Int32 percent)
        {
            return value * (percent / 100D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Percent(this Int16 value, UInt32 percent)
        {
            return value * (percent / 100D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Percent(this Int16 value, Int64 percent)
        {
            return value * (percent / 100D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Percent(this Int16 value, UInt64 percent)
        {
            return value * (percent / 100D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Percent(this Int16 value, Single percent)
        {
            return value * (percent / 100F);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Percent(this Int16 value, Double percent)
        {
            return value * (percent / 100D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Percent(this Int16 value, Decimal percent)
        {
            return value * (percent / 100M);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Percent(this UInt16 value, SByte percent)
        {
            return value * (percent / 100D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Percent(this UInt16 value, Byte percent)
        {
            return value * (percent / 100D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Percent(this UInt16 value, Int16 percent)
        {
            return value * (percent / 100D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Percent(this UInt16 value, UInt16 percent)
        {
            return value * (percent / 100D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Percent(this UInt16 value, Int32 percent)
        {
            return value * (percent / 100D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Percent(this UInt16 value, UInt32 percent)
        {
            return value * (percent / 100D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Percent(this UInt16 value, Int64 percent)
        {
            return value * (percent / 100D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Percent(this UInt16 value, UInt64 percent)
        {
            return value * (percent / 100D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Percent(this UInt16 value, Single percent)
        {
            return value * (percent / 100F);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Percent(this UInt16 value, Double percent)
        {
            return value * (percent / 100D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Percent(this UInt16 value, Decimal percent)
        {
            return value * (percent / 100M);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Percent(this Int32 value, SByte percent)
        {
            return value * (percent / 100D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Percent(this Int32 value, Byte percent)
        {
            return value * (percent / 100D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Percent(this Int32 value, Int16 percent)
        {
            return value * (percent / 100D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Percent(this Int32 value, UInt16 percent)
        {
            return value * (percent / 100D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Percent(this Int32 value, Int32 percent)
        {
            return value * (percent / 100D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Percent(this Int32 value, UInt32 percent)
        {
            return value * (percent / 100D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Percent(this Int32 value, Int64 percent)
        {
            return value * (percent / 100D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Percent(this Int32 value, UInt64 percent)
        {
            return value * (percent / 100D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Percent(this Int32 value, Single percent)
        {
            return value * (percent / 100F);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Percent(this Int32 value, Double percent)
        {
            return value * (percent / 100D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Percent(this Int32 value, Decimal percent)
        {
            return value * (percent / 100M);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Percent(this UInt32 value, SByte percent)
        {
            return value * (percent / 100D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Percent(this UInt32 value, Byte percent)
        {
            return value * (percent / 100D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Percent(this UInt32 value, Int16 percent)
        {
            return value * (percent / 100D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Percent(this UInt32 value, UInt16 percent)
        {
            return value * (percent / 100D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Percent(this UInt32 value, Int32 percent)
        {
            return value * (percent / 100D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Percent(this UInt32 value, UInt32 percent)
        {
            return value * (percent / 100D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Percent(this UInt32 value, Int64 percent)
        {
            return value * (percent / 100D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Percent(this UInt32 value, UInt64 percent)
        {
            return value * (percent / 100D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Percent(this UInt32 value, Single percent)
        {
            return value * (percent / 100F);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Percent(this UInt32 value, Double percent)
        {
            return value * (percent / 100D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Percent(this UInt32 value, Decimal percent)
        {
            return value * (percent / 100M);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Percent(this Int64 value, SByte percent)
        {
            return value * (percent / 100D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Percent(this Int64 value, Byte percent)
        {
            return value * (percent / 100D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Percent(this Int64 value, Int16 percent)
        {
            return value * (percent / 100D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Percent(this Int64 value, UInt16 percent)
        {
            return value * (percent / 100D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Percent(this Int64 value, Int32 percent)
        {
            return value * (percent / 100D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Percent(this Int64 value, UInt32 percent)
        {
            return value * (percent / 100D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Percent(this Int64 value, Int64 percent)
        {
            return value * (percent / 100D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Percent(this Int64 value, UInt64 percent)
        {
            return value * (percent / 100D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Percent(this Int64 value, Single percent)
        {
            return value * (percent / 100F);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Percent(this Int64 value, Double percent)
        {
            return value * (percent / 100D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Percent(this Int64 value, Decimal percent)
        {
            return value * (percent / 100M);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Percent(this UInt64 value, SByte percent)
        {
            return value * (percent / 100D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Percent(this UInt64 value, Byte percent)
        {
            return value * (percent / 100D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Percent(this UInt64 value, Int16 percent)
        {
            return value * (percent / 100D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Percent(this UInt64 value, UInt16 percent)
        {
            return value * (percent / 100D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Percent(this UInt64 value, Int32 percent)
        {
            return value * (percent / 100D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Percent(this UInt64 value, UInt32 percent)
        {
            return value * (percent / 100D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Percent(this UInt64 value, Int64 percent)
        {
            return value * (percent / 100D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Percent(this UInt64 value, UInt64 percent)
        {
            return value * (percent / 100D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Percent(this UInt64 value, Single percent)
        {
            return value * (percent / 100F);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Percent(this UInt64 value, Double percent)
        {
            return value * (percent / 100D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Percent(this UInt64 value, Decimal percent)
        {
            return value * (percent / 100M);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Percent(this Single value, SByte percent)
        {
            return value * (percent / 100F);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Percent(this Single value, Byte percent)
        {
            return value * (percent / 100F);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Percent(this Single value, Int16 percent)
        {
            return value * (percent / 100F);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Percent(this Single value, UInt16 percent)
        {
            return value * (percent / 100F);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Percent(this Single value, Int32 percent)
        {
            return value * (percent / 100F);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Percent(this Single value, UInt32 percent)
        {
            return value * (percent / 100F);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Percent(this Single value, Int64 percent)
        {
            return value * (percent / 100F);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Percent(this Single value, UInt64 percent)
        {
            return value * (percent / 100F);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Percent(this Single value, Single percent)
        {
            return value * (percent / 100F);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Percent(this Single value, Double percent)
        {
            return value * (percent / 100D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Percent(this Single value, Decimal percent)
        {
            return (Decimal) value * (percent / 100M);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Percent(this Double value, SByte percent)
        {
            return value * (percent / 100D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Percent(this Double value, Byte percent)
        {
            return value * (percent / 100D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Percent(this Double value, Int16 percent)
        {
            return value * (percent / 100D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Percent(this Double value, UInt16 percent)
        {
            return value * (percent / 100D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Percent(this Double value, Int32 percent)
        {
            return value * (percent / 100D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Percent(this Double value, UInt32 percent)
        {
            return value * (percent / 100D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Percent(this Double value, Int64 percent)
        {
            return value * (percent / 100D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Percent(this Double value, UInt64 percent)
        {
            return value * (percent / 100D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Percent(this Double value, Single percent)
        {
            return value * (percent / 100D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Percent(this Double value, Double percent)
        {
            return value * (percent / 100D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Percent(this Double value, Decimal percent)
        {
            return (Decimal) value * (percent / 100M);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Percent(this Decimal value, SByte percent)
        {
            return value * (percent / 100M);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Percent(this Decimal value, Byte percent)
        {
            return value * (percent / 100M);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Percent(this Decimal value, Int16 percent)
        {
            return value * (percent / 100M);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Percent(this Decimal value, UInt16 percent)
        {
            return value * (percent / 100M);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Percent(this Decimal value, Int32 percent)
        {
            return value * (percent / 100M);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Percent(this Decimal value, UInt32 percent)
        {
            return value * (percent / 100M);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Percent(this Decimal value, Int64 percent)
        {
            return value * (percent / 100M);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Percent(this Decimal value, UInt64 percent)
        {
            return value * (percent / 100M);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Percent(this Decimal value, Single percent)
        {
            return value * ((Decimal) percent / 100M);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Percent(this Decimal value, Double percent)
        {
            return value * ((Decimal) percent / 100M);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Percent(this Decimal value, Decimal percent)
        {
            return value * (percent / 100M);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Promille(this SByte value, SByte promille)
        {
            return value * (promille / 1000D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Promille(this SByte value, Byte promille)
        {
            return value * (promille / 1000D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Promille(this SByte value, Int16 promille)
        {
            return value * (promille / 1000D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Promille(this SByte value, UInt16 promille)
        {
            return value * (promille / 1000D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Promille(this SByte value, Int32 promille)
        {
            return value * (promille / 1000D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Promille(this SByte value, UInt32 promille)
        {
            return value * (promille / 1000D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Promille(this SByte value, Int64 promille)
        {
            return value * (promille / 1000D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Promille(this SByte value, UInt64 promille)
        {
            return value * (promille / 1000D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Promille(this SByte value, Single promille)
        {
            return value * (promille / 1000F);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Promille(this SByte value, Double promille)
        {
            return value * (promille / 1000D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Promille(this SByte value, Decimal promille)
        {
            return value * (promille / 1000M);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Promille(this Byte value, SByte promille)
        {
            return value * (promille / 1000D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Promille(this Byte value, Byte promille)
        {
            return value * (promille / 1000D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Promille(this Byte value, Int16 promille)
        {
            return value * (promille / 1000D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Promille(this Byte value, UInt16 promille)
        {
            return value * (promille / 1000D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Promille(this Byte value, Int32 promille)
        {
            return value * (promille / 1000D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Promille(this Byte value, UInt32 promille)
        {
            return value * (promille / 1000D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Promille(this Byte value, Int64 promille)
        {
            return value * (promille / 1000D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Promille(this Byte value, UInt64 promille)
        {
            return value * (promille / 1000D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Promille(this Byte value, Single promille)
        {
            return value * (promille / 1000F);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Promille(this Byte value, Double promille)
        {
            return value * (promille / 1000D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Promille(this Byte value, Decimal promille)
        {
            return value * (promille / 1000M);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Promille(this Int16 value, SByte promille)
        {
            return value * (promille / 1000D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Promille(this Int16 value, Byte promille)
        {
            return value * (promille / 1000D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Promille(this Int16 value, Int16 promille)
        {
            return value * (promille / 1000D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Promille(this Int16 value, UInt16 promille)
        {
            return value * (promille / 1000D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Promille(this Int16 value, Int32 promille)
        {
            return value * (promille / 1000D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Promille(this Int16 value, UInt32 promille)
        {
            return value * (promille / 1000D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Promille(this Int16 value, Int64 promille)
        {
            return value * (promille / 1000D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Promille(this Int16 value, UInt64 promille)
        {
            return value * (promille / 1000D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Promille(this Int16 value, Single promille)
        {
            return value * (promille / 1000F);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Promille(this Int16 value, Double promille)
        {
            return value * (promille / 1000D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Promille(this Int16 value, Decimal promille)
        {
            return value * (promille / 1000M);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Promille(this UInt16 value, SByte promille)
        {
            return value * (promille / 1000D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Promille(this UInt16 value, Byte promille)
        {
            return value * (promille / 1000D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Promille(this UInt16 value, Int16 promille)
        {
            return value * (promille / 1000D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Promille(this UInt16 value, UInt16 promille)
        {
            return value * (promille / 1000D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Promille(this UInt16 value, Int32 promille)
        {
            return value * (promille / 1000D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Promille(this UInt16 value, UInt32 promille)
        {
            return value * (promille / 1000D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Promille(this UInt16 value, Int64 promille)
        {
            return value * (promille / 1000D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Promille(this UInt16 value, UInt64 promille)
        {
            return value * (promille / 1000D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Promille(this UInt16 value, Single promille)
        {
            return value * (promille / 1000F);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Promille(this UInt16 value, Double promille)
        {
            return value * (promille / 1000D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Promille(this UInt16 value, Decimal promille)
        {
            return value * (promille / 1000M);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Promille(this Int32 value, SByte promille)
        {
            return value * (promille / 1000D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Promille(this Int32 value, Byte promille)
        {
            return value * (promille / 1000D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Promille(this Int32 value, Int16 promille)
        {
            return value * (promille / 1000D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Promille(this Int32 value, UInt16 promille)
        {
            return value * (promille / 1000D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Promille(this Int32 value, Int32 promille)
        {
            return value * (promille / 1000D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Promille(this Int32 value, UInt32 promille)
        {
            return value * (promille / 1000D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Promille(this Int32 value, Int64 promille)
        {
            return value * (promille / 1000D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Promille(this Int32 value, UInt64 promille)
        {
            return value * (promille / 1000D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Promille(this Int32 value, Single promille)
        {
            return value * (promille / 1000F);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Promille(this Int32 value, Double promille)
        {
            return value * (promille / 1000D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Promille(this Int32 value, Decimal promille)
        {
            return value * (promille / 1000M);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Promille(this UInt32 value, SByte promille)
        {
            return value * (promille / 1000D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Promille(this UInt32 value, Byte promille)
        {
            return value * (promille / 1000D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Promille(this UInt32 value, Int16 promille)
        {
            return value * (promille / 1000D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Promille(this UInt32 value, UInt16 promille)
        {
            return value * (promille / 1000D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Promille(this UInt32 value, Int32 promille)
        {
            return value * (promille / 1000D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Promille(this UInt32 value, UInt32 promille)
        {
            return value * (promille / 1000D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Promille(this UInt32 value, Int64 promille)
        {
            return value * (promille / 1000D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Promille(this UInt32 value, UInt64 promille)
        {
            return value * (promille / 1000D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Promille(this UInt32 value, Single promille)
        {
            return value * (promille / 1000F);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Promille(this UInt32 value, Double promille)
        {
            return value * (promille / 1000D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Promille(this UInt32 value, Decimal promille)
        {
            return value * (promille / 1000M);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Promille(this Int64 value, SByte promille)
        {
            return value * (promille / 1000D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Promille(this Int64 value, Byte promille)
        {
            return value * (promille / 1000D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Promille(this Int64 value, Int16 promille)
        {
            return value * (promille / 1000D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Promille(this Int64 value, UInt16 promille)
        {
            return value * (promille / 1000D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Promille(this Int64 value, Int32 promille)
        {
            return value * (promille / 1000D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Promille(this Int64 value, UInt32 promille)
        {
            return value * (promille / 1000D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Promille(this Int64 value, Int64 promille)
        {
            return value * (promille / 1000D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Promille(this Int64 value, UInt64 promille)
        {
            return value * (promille / 1000D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Promille(this Int64 value, Single promille)
        {
            return value * (promille / 1000F);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Promille(this Int64 value, Double promille)
        {
            return value * (promille / 1000D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Promille(this Int64 value, Decimal promille)
        {
            return value * (promille / 1000M);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Promille(this UInt64 value, SByte promille)
        {
            return value * (promille / 1000D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Promille(this UInt64 value, Byte promille)
        {
            return value * (promille / 1000D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Promille(this UInt64 value, Int16 promille)
        {
            return value * (promille / 1000D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Promille(this UInt64 value, UInt16 promille)
        {
            return value * (promille / 1000D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Promille(this UInt64 value, Int32 promille)
        {
            return value * (promille / 1000D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Promille(this UInt64 value, UInt32 promille)
        {
            return value * (promille / 1000D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Promille(this UInt64 value, Int64 promille)
        {
            return value * (promille / 1000D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Promille(this UInt64 value, UInt64 promille)
        {
            return value * (promille / 1000D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Promille(this UInt64 value, Single promille)
        {
            return value * (promille / 1000F);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Promille(this UInt64 value, Double promille)
        {
            return value * (promille / 1000D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Promille(this UInt64 value, Decimal promille)
        {
            return value * (promille / 1000M);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Promille(this Single value, SByte promille)
        {
            return value * (promille / 1000F);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Promille(this Single value, Byte promille)
        {
            return value * (promille / 1000F);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Promille(this Single value, Int16 promille)
        {
            return value * (promille / 1000F);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Promille(this Single value, UInt16 promille)
        {
            return value * (promille / 1000F);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Promille(this Single value, Int32 promille)
        {
            return value * (promille / 1000F);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Promille(this Single value, UInt32 promille)
        {
            return value * (promille / 1000F);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Promille(this Single value, Int64 promille)
        {
            return value * (promille / 1000F);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Promille(this Single value, UInt64 promille)
        {
            return value * (promille / 1000F);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Single Promille(this Single value, Single promille)
        {
            return value * (promille / 1000F);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Promille(this Single value, Double promille)
        {
            return value * (promille / 1000D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Promille(this Single value, Decimal promille)
        {
            return (Decimal) value * (promille / 1000M);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Promille(this Double value, SByte promille)
        {
            return value * (promille / 1000D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Promille(this Double value, Byte promille)
        {
            return value * (promille / 1000D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Promille(this Double value, Int16 promille)
        {
            return value * (promille / 1000D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Promille(this Double value, UInt16 promille)
        {
            return value * (promille / 1000D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Promille(this Double value, Int32 promille)
        {
            return value * (promille / 1000D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Promille(this Double value, UInt32 promille)
        {
            return value * (promille / 1000D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Promille(this Double value, Int64 promille)
        {
            return value * (promille / 1000D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Promille(this Double value, UInt64 promille)
        {
            return value * (promille / 1000D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Promille(this Double value, Single promille)
        {
            return value * (promille / 1000D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Promille(this Double value, Double promille)
        {
            return value * (promille / 1000D);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Promille(this Double value, Decimal promille)
        {
            return (Decimal) value * (promille / 1000M);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Promille(this Decimal value, SByte promille)
        {
            return value * (promille / 1000M);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Promille(this Decimal value, Byte promille)
        {
            return value * (promille / 1000M);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Promille(this Decimal value, Int16 promille)
        {
            return value * (promille / 1000M);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Promille(this Decimal value, UInt16 promille)
        {
            return value * (promille / 1000M);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Promille(this Decimal value, Int32 promille)
        {
            return value * (promille / 1000M);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Promille(this Decimal value, UInt32 promille)
        {
            return value * (promille / 1000M);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Promille(this Decimal value, Int64 promille)
        {
            return value * (promille / 1000M);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Promille(this Decimal value, UInt64 promille)
        {
            return value * (promille / 1000M);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Promille(this Decimal value, Single promille)
        {
            return value * ((Decimal) promille / 1000M);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Promille(this Decimal value, Double promille)
        {
            return value * ((Decimal) promille / 1000M);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Decimal Promille(this Decimal value, Decimal promille)
        {
            return value * (promille / 1000M);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsChar(String? value)
        {
            return Char.TryParse(value, out _);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsSByte(String? value)
        {
            return SByte.TryParse(value, out _);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsByte(String? value)
        {
            return Byte.TryParse(value, out _);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsInt16(String? value)
        {
            return Int16.TryParse(value, out _);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsUInt16(String? value)
        {
            return UInt16.TryParse(value, out _);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsInt32(String? value)
        {
            return Int32.TryParse(value, out _);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsUInt32(String? value)
        {
            return UInt32.TryParse(value, out _);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsInt64(String? value)
        {
            return Int64.TryParse(value, out _);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsUInt64(String? value)
        {
            return UInt64.TryParse(value, out _);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsSingle(String? value)
        {
            return Single.TryParse(value, out _);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsDouble(String? value)
        {
            return Double.TryParse(value, out _);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsDecimal(String? value)
        {
            return Decimal.TryParse(value, out _);
        }
    }

    [SuppressMessage("ReSharper", "InvertIf")]
    [SuppressMessage("ReSharper", "CognitiveComplexity")]
    public static partial class MathUnsafe
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Add<T>(T left, T right) where T : unmanaged, IConvertible
        {
            if (typeof(T) == typeof(Char))
            {
                Char value = (Char)(Unsafe.As<T, Char>(ref left) + Unsafe.As<T, Char>(ref right));
                return Unsafe.As<Char, T>(ref value);
            }

            if (typeof(T) == typeof(SByte))
            {
                SByte value = (SByte)(Unsafe.As<T, SByte>(ref left) + Unsafe.As<T, SByte>(ref right));
                return Unsafe.As<SByte, T>(ref value);
            }

            if (typeof(T) == typeof(Byte))
            {
                Byte value = (Byte)(Unsafe.As<T, Byte>(ref left) + Unsafe.As<T, Byte>(ref right));
                return Unsafe.As<Byte, T>(ref value);
            }

            if (typeof(T) == typeof(Int16))
            {
                Int16 value = (Int16)(Unsafe.As<T, Int16>(ref left) + Unsafe.As<T, Int16>(ref right));
                return Unsafe.As<Int16, T>(ref value);
            }

            if (typeof(T) == typeof(UInt16))
            {
                UInt16 value = (UInt16)(Unsafe.As<T, UInt16>(ref left) + Unsafe.As<T, UInt16>(ref right));
                return Unsafe.As<UInt16, T>(ref value);
            }

            if (typeof(T) == typeof(Int32))
            {
                Int32 value = Unsafe.As<T, Int32>(ref left) + Unsafe.As<T, Int32>(ref right);
                return Unsafe.As<Int32, T>(ref value);
            }

            if (typeof(T) == typeof(UInt32))
            {
                UInt32 value = Unsafe.As<T, UInt32>(ref left) + Unsafe.As<T, UInt32>(ref right);
                return Unsafe.As<UInt32, T>(ref value);
            }

            if (typeof(T) == typeof(Int64))
            {
                Int64 value = Unsafe.As<T, Int64>(ref left) + Unsafe.As<T, Int64>(ref right);
                return Unsafe.As<Int64, T>(ref value);
            }

            if (typeof(T) == typeof(UInt64))
            {
                UInt64 value = Unsafe.As<T, UInt64>(ref left) + Unsafe.As<T, UInt64>(ref right);
                return Unsafe.As<UInt64, T>(ref value);
            }

            if (typeof(T) == typeof(Single))
            {
                Single value = Unsafe.As<T, Single>(ref left) + Unsafe.As<T, Single>(ref right);
                return Unsafe.As<Single, T>(ref value);
            }

            if (typeof(T) == typeof(Double))
            {
                Double value = Unsafe.As<T, Double>(ref left) + Unsafe.As<T, Double>(ref right);
                return Unsafe.As<Double, T>(ref value);
            }

            if (typeof(T) == typeof(Decimal))
            {
                Decimal value = Unsafe.As<T, Decimal>(ref left) + Unsafe.As<T, Decimal>(ref right);
                return Unsafe.As<Decimal, T>(ref value);
            }

            throw new NotSupportedException($"Operator + is not supported for {typeof(T)} type");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Substract<T>(T left, T right) where T : unmanaged, IConvertible
        {
            if (typeof(T) == typeof(Char))
            {
                Char value = (Char)(Unsafe.As<T, Char>(ref left) - Unsafe.As<T, Char>(ref right));
                return Unsafe.As<Char, T>(ref value);
            }

            if (typeof(T) == typeof(SByte))
            {
                SByte value = (SByte)(Unsafe.As<T, SByte>(ref left) - Unsafe.As<T, SByte>(ref right));
                return Unsafe.As<SByte, T>(ref value);
            }

            if (typeof(T) == typeof(Byte))
            {
                Byte value = (Byte)(Unsafe.As<T, Byte>(ref left) - Unsafe.As<T, Byte>(ref right));
                return Unsafe.As<Byte, T>(ref value);
            }

            if (typeof(T) == typeof(Int16))
            {
                Int16 value = (Int16)(Unsafe.As<T, Int16>(ref left) - Unsafe.As<T, Int16>(ref right));
                return Unsafe.As<Int16, T>(ref value);
            }

            if (typeof(T) == typeof(UInt16))
            {
                UInt16 value = (UInt16)(Unsafe.As<T, UInt16>(ref left) - Unsafe.As<T, UInt16>(ref right));
                return Unsafe.As<UInt16, T>(ref value);
            }

            if (typeof(T) == typeof(Int32))
            {
                Int32 value = Unsafe.As<T, Int32>(ref left) - Unsafe.As<T, Int32>(ref right);
                return Unsafe.As<Int32, T>(ref value);
            }

            if (typeof(T) == typeof(UInt32))
            {
                UInt32 value = Unsafe.As<T, UInt32>(ref left) - Unsafe.As<T, UInt32>(ref right);
                return Unsafe.As<UInt32, T>(ref value);
            }

            if (typeof(T) == typeof(Int64))
            {
                Int64 value = Unsafe.As<T, Int64>(ref left) - Unsafe.As<T, Int64>(ref right);
                return Unsafe.As<Int64, T>(ref value);
            }

            if (typeof(T) == typeof(UInt64))
            {
                UInt64 value = Unsafe.As<T, UInt64>(ref left) - Unsafe.As<T, UInt64>(ref right);
                return Unsafe.As<UInt64, T>(ref value);
            }

            if (typeof(T) == typeof(Single))
            {
                Single value = Unsafe.As<T, Single>(ref left) - Unsafe.As<T, Single>(ref right);
                return Unsafe.As<Single, T>(ref value);
            }

            if (typeof(T) == typeof(Double))
            {
                Double value = Unsafe.As<T, Double>(ref left) - Unsafe.As<T, Double>(ref right);
                return Unsafe.As<Double, T>(ref value);
            }

            if (typeof(T) == typeof(Decimal))
            {
                Decimal value = Unsafe.As<T, Decimal>(ref left) - Unsafe.As<T, Decimal>(ref right);
                return Unsafe.As<Decimal, T>(ref value);
            }

            throw new NotSupportedException($"Operator - is not supported for {typeof(T)} type");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Multiply<T>(T left, T right) where T : unmanaged, IConvertible
        {
            if (typeof(T) == typeof(Char))
            {
                Char value = (Char)(Unsafe.As<T, Char>(ref left) * Unsafe.As<T, Char>(ref right));
                return Unsafe.As<Char, T>(ref value);
            }

            if (typeof(T) == typeof(SByte))
            {
                SByte value = (SByte)(Unsafe.As<T, SByte>(ref left) * Unsafe.As<T, SByte>(ref right));
                return Unsafe.As<SByte, T>(ref value);
            }

            if (typeof(T) == typeof(Byte))
            {
                Byte value = (Byte)(Unsafe.As<T, Byte>(ref left) * Unsafe.As<T, Byte>(ref right));
                return Unsafe.As<Byte, T>(ref value);
            }

            if (typeof(T) == typeof(Int16))
            {
                Int16 value = (Int16)(Unsafe.As<T, Int16>(ref left) * Unsafe.As<T, Int16>(ref right));
                return Unsafe.As<Int16, T>(ref value);
            }

            if (typeof(T) == typeof(UInt16))
            {
                UInt16 value = (UInt16)(Unsafe.As<T, UInt16>(ref left) * Unsafe.As<T, UInt16>(ref right));
                return Unsafe.As<UInt16, T>(ref value);
            }

            if (typeof(T) == typeof(Int32))
            {
                Int32 value = Unsafe.As<T, Int32>(ref left) * Unsafe.As<T, Int32>(ref right);
                return Unsafe.As<Int32, T>(ref value);
            }

            if (typeof(T) == typeof(UInt32))
            {
                UInt32 value = Unsafe.As<T, UInt32>(ref left) * Unsafe.As<T, UInt32>(ref right);
                return Unsafe.As<UInt32, T>(ref value);
            }

            if (typeof(T) == typeof(Int64))
            {
                Int64 value = Unsafe.As<T, Int64>(ref left) * Unsafe.As<T, Int64>(ref right);
                return Unsafe.As<Int64, T>(ref value);
            }

            if (typeof(T) == typeof(UInt64))
            {
                UInt64 value = Unsafe.As<T, UInt64>(ref left) * Unsafe.As<T, UInt64>(ref right);
                return Unsafe.As<UInt64, T>(ref value);
            }

            if (typeof(T) == typeof(Single))
            {
                Single value = Unsafe.As<T, Single>(ref left) * Unsafe.As<T, Single>(ref right);
                return Unsafe.As<Single, T>(ref value);
            }

            if (typeof(T) == typeof(Double))
            {
                Double value = Unsafe.As<T, Double>(ref left) * Unsafe.As<T, Double>(ref right);
                return Unsafe.As<Double, T>(ref value);
            }

            if (typeof(T) == typeof(Decimal))
            {
                Decimal value = Unsafe.As<T, Decimal>(ref left) * Unsafe.As<T, Decimal>(ref right);
                return Unsafe.As<Decimal, T>(ref value);
            }

            throw new NotSupportedException($"Operator * is not supported for {typeof(T)} type");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Divide<T>(T left, T right) where T : unmanaged, IConvertible
        {
            if (typeof(T) == typeof(Char))
            {
                Char value = (Char)(Unsafe.As<T, Char>(ref left) / Unsafe.As<T, Char>(ref right));
                return Unsafe.As<Char, T>(ref value);
            }

            if (typeof(T) == typeof(SByte))
            {
                SByte value = (SByte)(Unsafe.As<T, SByte>(ref left) / Unsafe.As<T, SByte>(ref right));
                return Unsafe.As<SByte, T>(ref value);
            }

            if (typeof(T) == typeof(Byte))
            {
                Byte value = (Byte)(Unsafe.As<T, Byte>(ref left) / Unsafe.As<T, Byte>(ref right));
                return Unsafe.As<Byte, T>(ref value);
            }

            if (typeof(T) == typeof(Int16))
            {
                Int16 value = (Int16)(Unsafe.As<T, Int16>(ref left) / Unsafe.As<T, Int16>(ref right));
                return Unsafe.As<Int16, T>(ref value);
            }

            if (typeof(T) == typeof(UInt16))
            {
                UInt16 value = (UInt16)(Unsafe.As<T, UInt16>(ref left) / Unsafe.As<T, UInt16>(ref right));
                return Unsafe.As<UInt16, T>(ref value);
            }

            if (typeof(T) == typeof(Int32))
            {
                Int32 value = Unsafe.As<T, Int32>(ref left) / Unsafe.As<T, Int32>(ref right);
                return Unsafe.As<Int32, T>(ref value);
            }

            if (typeof(T) == typeof(UInt32))
            {
                UInt32 value = Unsafe.As<T, UInt32>(ref left) / Unsafe.As<T, UInt32>(ref right);
                return Unsafe.As<UInt32, T>(ref value);
            }

            if (typeof(T) == typeof(Int64))
            {
                Int64 value = Unsafe.As<T, Int64>(ref left) / Unsafe.As<T, Int64>(ref right);
                return Unsafe.As<Int64, T>(ref value);
            }

            if (typeof(T) == typeof(UInt64))
            {
                UInt64 value = Unsafe.As<T, UInt64>(ref left) / Unsafe.As<T, UInt64>(ref right);
                return Unsafe.As<UInt64, T>(ref value);
            }

            if (typeof(T) == typeof(Single))
            {
                Single value = Unsafe.As<T, Single>(ref left) / Unsafe.As<T, Single>(ref right);
                return Unsafe.As<Single, T>(ref value);
            }

            if (typeof(T) == typeof(Double))
            {
                Double value = Unsafe.As<T, Double>(ref left) / Unsafe.As<T, Double>(ref right);
                return Unsafe.As<Double, T>(ref value);
            }

            if (typeof(T) == typeof(Decimal))
            {
                Decimal value = Unsafe.As<T, Decimal>(ref left) / Unsafe.As<T, Decimal>(ref right);
                return Unsafe.As<Decimal, T>(ref value);
            }

            throw new NotSupportedException($"Operator / is not supported for {typeof(T)} type");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Modulo<T>(T left, T right) where T : unmanaged, IConvertible
        {
            if (typeof(T) == typeof(Char))
            {
                Char value = (Char)(Unsafe.As<T, Char>(ref left) % Unsafe.As<T, Char>(ref right));
                return Unsafe.As<Char, T>(ref value);
            }

            if (typeof(T) == typeof(SByte))
            {
                SByte value = (SByte)(Unsafe.As<T, SByte>(ref left) % Unsafe.As<T, SByte>(ref right));
                return Unsafe.As<SByte, T>(ref value);
            }

            if (typeof(T) == typeof(Byte))
            {
                Byte value = (Byte)(Unsafe.As<T, Byte>(ref left) % Unsafe.As<T, Byte>(ref right));
                return Unsafe.As<Byte, T>(ref value);
            }

            if (typeof(T) == typeof(Int16))
            {
                Int16 value = (Int16)(Unsafe.As<T, Int16>(ref left) % Unsafe.As<T, Int16>(ref right));
                return Unsafe.As<Int16, T>(ref value);
            }

            if (typeof(T) == typeof(UInt16))
            {
                UInt16 value = (UInt16)(Unsafe.As<T, UInt16>(ref left) % Unsafe.As<T, UInt16>(ref right));
                return Unsafe.As<UInt16, T>(ref value);
            }

            if (typeof(T) == typeof(Int32))
            {
                Int32 value = Unsafe.As<T, Int32>(ref left) % Unsafe.As<T, Int32>(ref right);
                return Unsafe.As<Int32, T>(ref value);
            }

            if (typeof(T) == typeof(UInt32))
            {
                UInt32 value = Unsafe.As<T, UInt32>(ref left) % Unsafe.As<T, UInt32>(ref right);
                return Unsafe.As<UInt32, T>(ref value);
            }

            if (typeof(T) == typeof(Int64))
            {
                Int64 value = Unsafe.As<T, Int64>(ref left) % Unsafe.As<T, Int64>(ref right);
                return Unsafe.As<Int64, T>(ref value);
            }

            if (typeof(T) == typeof(UInt64))
            {
                UInt64 value = Unsafe.As<T, UInt64>(ref left) % Unsafe.As<T, UInt64>(ref right);
                return Unsafe.As<UInt64, T>(ref value);
            }

            if (typeof(T) == typeof(Single))
            {
                Single value = Unsafe.As<T, Single>(ref left) % Unsafe.As<T, Single>(ref right);
                return Unsafe.As<Single, T>(ref value);
            }

            if (typeof(T) == typeof(Double))
            {
                Double value = Unsafe.As<T, Double>(ref left) % Unsafe.As<T, Double>(ref right);
                return Unsafe.As<Double, T>(ref value);
            }

            if (typeof(T) == typeof(Decimal))
            {
                Decimal value = Unsafe.As<T, Decimal>(ref left) % Unsafe.As<T, Decimal>(ref right);
                return Unsafe.As<Decimal, T>(ref value);
            }

            throw new NotSupportedException($"Operator % is not supported for {typeof(T)} type");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T And<T>(T left, T right) where T : unmanaged, IConvertible
        {
            if (typeof(T) == typeof(Char))
            {
                Char value = (Char)(Unsafe.As<T, Char>(ref left) & Unsafe.As<T, Char>(ref right));
                return Unsafe.As<Char, T>(ref value);
            }

            if (typeof(T) == typeof(SByte))
            {
                SByte value = (SByte)(Unsafe.As<T, SByte>(ref left) & Unsafe.As<T, SByte>(ref right));
                return Unsafe.As<SByte, T>(ref value);
            }

            if (typeof(T) == typeof(Byte))
            {
                Byte value = (Byte)(Unsafe.As<T, Byte>(ref left) & Unsafe.As<T, Byte>(ref right));
                return Unsafe.As<Byte, T>(ref value);
            }

            if (typeof(T) == typeof(Int16))
            {
                Int16 value = (Int16)(Unsafe.As<T, Int16>(ref left) & Unsafe.As<T, Int16>(ref right));
                return Unsafe.As<Int16, T>(ref value);
            }

            if (typeof(T) == typeof(UInt16))
            {
                UInt16 value = (UInt16)(Unsafe.As<T, UInt16>(ref left) & Unsafe.As<T, UInt16>(ref right));
                return Unsafe.As<UInt16, T>(ref value);
            }

            if (typeof(T) == typeof(Int32))
            {
                Int32 value = Unsafe.As<T, Int32>(ref left) & Unsafe.As<T, Int32>(ref right);
                return Unsafe.As<Int32, T>(ref value);
            }

            if (typeof(T) == typeof(UInt32))
            {
                UInt32 value = Unsafe.As<T, UInt32>(ref left) & Unsafe.As<T, UInt32>(ref right);
                return Unsafe.As<UInt32, T>(ref value);
            }

            if (typeof(T) == typeof(Int64))
            {
                Int64 value = Unsafe.As<T, Int64>(ref left) & Unsafe.As<T, Int64>(ref right);
                return Unsafe.As<Int64, T>(ref value);
            }

            if (typeof(T) == typeof(UInt64))
            {
                UInt64 value = Unsafe.As<T, UInt64>(ref left) & Unsafe.As<T, UInt64>(ref right);
                return Unsafe.As<UInt64, T>(ref value);
            }

            throw new NotSupportedException($"Operator & is not supported for {typeof(T)} type");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Or<T>(T left, T right) where T : unmanaged, IConvertible
        {
            if (typeof(T) == typeof(Char))
            {
                Char value = (Char)(Unsafe.As<T, Char>(ref left) | Unsafe.As<T, Char>(ref right));
                return Unsafe.As<Char, T>(ref value);
            }

            if (typeof(T) == typeof(SByte))
            {
                SByte value = (SByte)(Unsafe.As<T, SByte>(ref left) | Unsafe.As<T, SByte>(ref right));
                return Unsafe.As<SByte, T>(ref value);
            }

            if (typeof(T) == typeof(Byte))
            {
                Byte value = (Byte)(Unsafe.As<T, Byte>(ref left) | Unsafe.As<T, Byte>(ref right));
                return Unsafe.As<Byte, T>(ref value);
            }

            if (typeof(T) == typeof(Int16))
            {
                Int16 value = (Int16)(Unsafe.As<T, Int16>(ref left) | Unsafe.As<T, Int16>(ref right));
                return Unsafe.As<Int16, T>(ref value);
            }

            if (typeof(T) == typeof(UInt16))
            {
                UInt16 value = (UInt16)(Unsafe.As<T, UInt16>(ref left) | Unsafe.As<T, UInt16>(ref right));
                return Unsafe.As<UInt16, T>(ref value);
            }

            if (typeof(T) == typeof(Int32))
            {
                Int32 value = Unsafe.As<T, Int32>(ref left) | Unsafe.As<T, Int32>(ref right);
                return Unsafe.As<Int32, T>(ref value);
            }

            if (typeof(T) == typeof(UInt32))
            {
                UInt32 value = Unsafe.As<T, UInt32>(ref left) | Unsafe.As<T, UInt32>(ref right);
                return Unsafe.As<UInt32, T>(ref value);
            }

            if (typeof(T) == typeof(Int64))
            {
                Int64 value = Unsafe.As<T, Int64>(ref left) | Unsafe.As<T, Int64>(ref right);
                return Unsafe.As<Int64, T>(ref value);
            }

            if (typeof(T) == typeof(UInt64))
            {
                UInt64 value = Unsafe.As<T, UInt64>(ref left) | Unsafe.As<T, UInt64>(ref right);
                return Unsafe.As<UInt64, T>(ref value);
            }

            throw new NotSupportedException($"Operator | is not supported for {typeof(T)} type");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Xor<T>(T left, T right) where T : unmanaged, IConvertible
        {
            if (typeof(T) == typeof(Char))
            {
                Char value = (Char)(Unsafe.As<T, Char>(ref left) ^ Unsafe.As<T, Char>(ref right));
                return Unsafe.As<Char, T>(ref value);
            }

            if (typeof(T) == typeof(SByte))
            {
                SByte value = (SByte)(Unsafe.As<T, SByte>(ref left) ^ Unsafe.As<T, SByte>(ref right));
                return Unsafe.As<SByte, T>(ref value);
            }

            if (typeof(T) == typeof(Byte))
            {
                Byte value = (Byte)(Unsafe.As<T, Byte>(ref left) ^ Unsafe.As<T, Byte>(ref right));
                return Unsafe.As<Byte, T>(ref value);
            }

            if (typeof(T) == typeof(Int16))
            {
                Int16 value = (Int16)(Unsafe.As<T, Int16>(ref left) ^ Unsafe.As<T, Int16>(ref right));
                return Unsafe.As<Int16, T>(ref value);
            }

            if (typeof(T) == typeof(UInt16))
            {
                UInt16 value = (UInt16)(Unsafe.As<T, UInt16>(ref left) ^ Unsafe.As<T, UInt16>(ref right));
                return Unsafe.As<UInt16, T>(ref value);
            }

            if (typeof(T) == typeof(Int32))
            {
                Int32 value = Unsafe.As<T, Int32>(ref left) ^ Unsafe.As<T, Int32>(ref right);
                return Unsafe.As<Int32, T>(ref value);
            }

            if (typeof(T) == typeof(UInt32))
            {
                UInt32 value = Unsafe.As<T, UInt32>(ref left) ^ Unsafe.As<T, UInt32>(ref right);
                return Unsafe.As<UInt32, T>(ref value);
            }

            if (typeof(T) == typeof(Int64))
            {
                Int64 value = Unsafe.As<T, Int64>(ref left) ^ Unsafe.As<T, Int64>(ref right);
                return Unsafe.As<Int64, T>(ref value);
            }

            if (typeof(T) == typeof(UInt64))
            {
                UInt64 value = Unsafe.As<T, UInt64>(ref left) ^ Unsafe.As<T, UInt64>(ref right);
                return Unsafe.As<UInt64, T>(ref value);
            }

            throw new NotSupportedException($"Operator ^ is not supported for {typeof(T)} type");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Invert<T>(T value) where T : unmanaged, IConvertible
        {
            if (typeof(T) == typeof(Char))
            {
                Int32 i = ~Unsafe.As<T, Char>(ref value);
                Char val = Unsafe.As<Int32, Char>(ref i);
                return Unsafe.As<Char, T>(ref val);
            }

            if (typeof(T) == typeof(SByte))
            {
                Int32 i = ~Unsafe.As<T, SByte>(ref value);
                SByte val = Unsafe.As<Int32, SByte>(ref i);
                return Unsafe.As<SByte, T>(ref val);
            }

            if (typeof(T) == typeof(Byte))
            {
                Int32 i = ~Unsafe.As<T, Byte>(ref value);
                Byte val = Unsafe.As<Int32, Byte>(ref i);
                return Unsafe.As<Byte, T>(ref val);
            }

            if (typeof(T) == typeof(Int16))
            {
                Int32 i = ~Unsafe.As<T, Int16>(ref value);
                Int16 val = Unsafe.As<Int32, Int16>(ref i);
                return Unsafe.As<Int16, T>(ref val);
            }

            if (typeof(T) == typeof(UInt16))
            {
                Int32 i = ~Unsafe.As<T, UInt16>(ref value);
                UInt16 val = Unsafe.As<Int32, UInt16>(ref i);
                return Unsafe.As<UInt16, T>(ref val);
            }

            if (typeof(T) == typeof(Int32))
            {
                Int32 val = ~Unsafe.As<T, Int32>(ref value);
                return Unsafe.As<Int32, T>(ref val);
            }

            if (typeof(T) == typeof(UInt32))
            {
                UInt32 val = ~Unsafe.As<T, UInt32>(ref value);
                return Unsafe.As<UInt32, T>(ref val);
            }

            if (typeof(T) == typeof(Int64))
            {
                Int64 val = ~Unsafe.As<T, Int64>(ref value);
                return Unsafe.As<Int64, T>(ref val);
            }

            if (typeof(T) == typeof(UInt64))
            {
                UInt64 val = ~Unsafe.As<T, UInt64>(ref value);
                return Unsafe.As<UInt64, T>(ref val);
            }

            throw new NotSupportedException($"Operator ~ is not supported for {typeof(T)} type");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Abs<T>(T value) where T : unmanaged, IConvertible
        {
            if (typeof(T) == typeof(SByte))
            {
                SByte val = Math.Abs(Unsafe.As<T, SByte>(ref value));
                return Unsafe.As<SByte, T>(ref val);
            }

            if (typeof(T) == typeof(Int16))
            {
                Int16 val = Math.Abs(Unsafe.As<T, Int16>(ref value));
                return Unsafe.As<Int16, T>(ref val);
            }

            if (typeof(T) == typeof(Int32))
            {
                Int32 val = Math.Abs(Unsafe.As<T, Int32>(ref value));
                return Unsafe.As<Int32, T>(ref val);
            }

            if (typeof(T) == typeof(Int64))
            {
                Int64 val = Math.Abs(Unsafe.As<T, Int64>(ref value));
                return Unsafe.As<Int64, T>(ref val);
            }

            if (typeof(T) == typeof(Single))
            {
                Single val = Math.Abs(Unsafe.As<T, Single>(ref value));
                return Unsafe.As<Single, T>(ref val);
            }

            if (typeof(T) == typeof(Double))
            {
                Double val = Math.Abs(Unsafe.As<T, Double>(ref value));
                return Unsafe.As<Double, T>(ref val);
            }

            if (typeof(T) == typeof(Decimal))
            {
                Decimal val = Math.Abs(Unsafe.As<T, Decimal>(ref value));
                return Unsafe.As<Decimal, T>(ref val);
            }

            throw new NotSupportedException($"Operator | | is not supported for {typeof(T)} type");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Negative<T>(T value) where T : unmanaged, IConvertible
        {
            if (typeof(T) == typeof(SByte))
            {
                Int32 i = -Math.Abs(Unsafe.As<T, SByte>(ref value));
                SByte val = Unsafe.As<Int32, SByte>(ref i);
                return Unsafe.As<SByte, T>(ref val);
            }

            if (typeof(T) == typeof(Int16))
            {
                Int32 i = -Math.Abs(Unsafe.As<T, Int16>(ref value));
                Int16 val = Unsafe.As<Int32, Int16>(ref i);
                return Unsafe.As<Int16, T>(ref val);
            }

            if (typeof(T) == typeof(Int32))
            {
                Int32 val = -Math.Abs(Unsafe.As<T, Int32>(ref value));
                return Unsafe.As<Int32, T>(ref val);
            }

            if (typeof(T) == typeof(Int64))
            {
                Int64 val = -Math.Abs(Unsafe.As<T, Int64>(ref value));
                return Unsafe.As<Int64, T>(ref val);
            }

            if (typeof(T) == typeof(Single))
            {
                Single val = -Math.Abs(Unsafe.As<T, Single>(ref value));
                return Unsafe.As<Single, T>(ref val);
            }

            if (typeof(T) == typeof(Double))
            {
                Double val = -Math.Abs(Unsafe.As<T, Double>(ref value));
                return Unsafe.As<Double, T>(ref val);
            }

            if (typeof(T) == typeof(Decimal))
            {
                Decimal val = -Math.Abs(Unsafe.As<T, Decimal>(ref value));
                return Unsafe.As<Decimal, T>(ref val);
            }

            throw new NotSupportedException($"Operator -| | is not supported for {typeof(T)} type");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Equal<T>(T left, T right) where T : unmanaged, IConvertible
        {
            if (typeof(T) == typeof(Char))
            {
                return Unsafe.As<T, Char>(ref left) == Unsafe.As<T, Char>(ref right);
            }

            if (typeof(T) == typeof(SByte))
            {
                return Unsafe.As<T, SByte>(ref left) == Unsafe.As<T, SByte>(ref right);
            }

            if (typeof(T) == typeof(Byte))
            {
                return Unsafe.As<T, Byte>(ref left) == Unsafe.As<T, Byte>(ref right);
            }

            if (typeof(T) == typeof(Int16))
            {
                return Unsafe.As<T, Int16>(ref left) == Unsafe.As<T, Int16>(ref right);
            }

            if (typeof(T) == typeof(UInt16))
            {
                return Unsafe.As<T, UInt16>(ref left) == Unsafe.As<T, UInt16>(ref right);
            }

            if (typeof(T) == typeof(Int32))
            {
                return Unsafe.As<T, Int32>(ref left) == Unsafe.As<T, Int32>(ref right);
            }

            if (typeof(T) == typeof(UInt32))
            {
                return Unsafe.As<T, UInt32>(ref left) == Unsafe.As<T, UInt32>(ref right);
            }

            if (typeof(T) == typeof(Int64))
            {
                return Unsafe.As<T, Int64>(ref left) == Unsafe.As<T, Int64>(ref right);
            }

            if (typeof(T) == typeof(UInt64))
            {
                return Unsafe.As<T, UInt64>(ref left) == Unsafe.As<T, UInt64>(ref right);
            }

            if (typeof(T) == typeof(Single))
            {
                return Math.Abs(Unsafe.As<T, Single>(ref left) - Unsafe.As<T, Single>(ref right)) < Single.Epsilon;
            }

            if (typeof(T) == typeof(Double))
            {
                return Math.Abs(Unsafe.As<T, Double>(ref left) - Unsafe.As<T, Double>(ref right)) < Double.Epsilon;
            }

            if (typeof(T) == typeof(Decimal))
            {
                return Unsafe.As<T, Decimal>(ref left) == Unsafe.As<T, Decimal>(ref right);
            }

            throw new NotSupportedException($"Operator == is not supported for {typeof(T)} type");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean NotEqual<T>(T left, T right) where T : unmanaged, IConvertible
        {
            if (typeof(T) == typeof(Char))
            {
                return Unsafe.As<T, Char>(ref left) != Unsafe.As<T, Char>(ref right);
            }

            if (typeof(T) == typeof(SByte))
            {
                return Unsafe.As<T, SByte>(ref left) != Unsafe.As<T, SByte>(ref right);
            }

            if (typeof(T) == typeof(Byte))
            {
                return Unsafe.As<T, Byte>(ref left) != Unsafe.As<T, Byte>(ref right);
            }

            if (typeof(T) == typeof(Int16))
            {
                return Unsafe.As<T, Int16>(ref left) != Unsafe.As<T, Int16>(ref right);
            }

            if (typeof(T) == typeof(UInt16))
            {
                return Unsafe.As<T, UInt16>(ref left) != Unsafe.As<T, UInt16>(ref right);
            }

            if (typeof(T) == typeof(Int32))
            {
                return Unsafe.As<T, Int32>(ref left) != Unsafe.As<T, Int32>(ref right);
            }

            if (typeof(T) == typeof(UInt32))
            {
                return Unsafe.As<T, UInt32>(ref left) != Unsafe.As<T, UInt32>(ref right);
            }

            if (typeof(T) == typeof(Int64))
            {
                return Unsafe.As<T, Int64>(ref left) != Unsafe.As<T, Int64>(ref right);
            }

            if (typeof(T) == typeof(UInt64))
            {
                return Unsafe.As<T, UInt64>(ref left) != Unsafe.As<T, UInt64>(ref right);
            }

            if (typeof(T) == typeof(Single))
            {
                return Math.Abs(Unsafe.As<T, Single>(ref left) - Unsafe.As<T, Single>(ref right)) > Single.Epsilon;
            }

            if (typeof(T) == typeof(Double))
            {
                return Math.Abs(Unsafe.As<T, Double>(ref left) - Unsafe.As<T, Double>(ref right)) > Double.Epsilon;
            }

            if (typeof(T) == typeof(Decimal))
            {
                return Unsafe.As<T, Decimal>(ref left) != Unsafe.As<T, Decimal>(ref right);
            }

            throw new NotSupportedException($"Operator != is not supported for {typeof(T)} type");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Greater<T>(T left, T right) where T : unmanaged, IConvertible
        {
            if (typeof(T) == typeof(Char))
            {
                return Unsafe.As<T, Char>(ref left) > Unsafe.As<T, Char>(ref right);
            }

            if (typeof(T) == typeof(SByte))
            {
                return Unsafe.As<T, SByte>(ref left) > Unsafe.As<T, SByte>(ref right);
            }

            if (typeof(T) == typeof(Byte))
            {
                return Unsafe.As<T, Byte>(ref left) > Unsafe.As<T, Byte>(ref right);
            }

            if (typeof(T) == typeof(Int16))
            {
                return Unsafe.As<T, Int16>(ref left) > Unsafe.As<T, Int16>(ref right);
            }

            if (typeof(T) == typeof(UInt16))
            {
                return Unsafe.As<T, UInt16>(ref left) > Unsafe.As<T, UInt16>(ref right);
            }

            if (typeof(T) == typeof(Int32))
            {
                return Unsafe.As<T, Int32>(ref left) > Unsafe.As<T, Int32>(ref right);
            }

            if (typeof(T) == typeof(UInt32))
            {
                return Unsafe.As<T, UInt32>(ref left) > Unsafe.As<T, UInt32>(ref right);
            }

            if (typeof(T) == typeof(Int64))
            {
                return Unsafe.As<T, Int64>(ref left) > Unsafe.As<T, Int64>(ref right);
            }

            if (typeof(T) == typeof(UInt64))
            {
                return Unsafe.As<T, UInt64>(ref left) > Unsafe.As<T, UInt64>(ref right);
            }

            if (typeof(T) == typeof(Single))
            {
                return Unsafe.As<T, Single>(ref left) > Unsafe.As<T, Single>(ref right);
            }

            if (typeof(T) == typeof(Double))
            {
                return Unsafe.As<T, Double>(ref left) > Unsafe.As<T, Double>(ref right);
            }

            if (typeof(T) == typeof(Decimal))
            {
                return Unsafe.As<T, Decimal>(ref left) > Unsafe.As<T, Decimal>(ref right);
            }

            throw new NotSupportedException($"Operator > is not supported for {typeof(T)} type");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean GreaterEqual<T>(T left, T right) where T : unmanaged, IConvertible
        {
            if (typeof(T) == typeof(Char))
            {
                return Unsafe.As<T, Char>(ref left) >= Unsafe.As<T, Char>(ref right);
            }

            if (typeof(T) == typeof(SByte))
            {
                return Unsafe.As<T, SByte>(ref left) >= Unsafe.As<T, SByte>(ref right);
            }

            if (typeof(T) == typeof(Byte))
            {
                return Unsafe.As<T, Byte>(ref left) >= Unsafe.As<T, Byte>(ref right);
            }

            if (typeof(T) == typeof(Int16))
            {
                return Unsafe.As<T, Int16>(ref left) >= Unsafe.As<T, Int16>(ref right);
            }

            if (typeof(T) == typeof(UInt16))
            {
                return Unsafe.As<T, UInt16>(ref left) >= Unsafe.As<T, UInt16>(ref right);
            }

            if (typeof(T) == typeof(Int32))
            {
                return Unsafe.As<T, Int32>(ref left) >= Unsafe.As<T, Int32>(ref right);
            }

            if (typeof(T) == typeof(UInt32))
            {
                return Unsafe.As<T, UInt32>(ref left) >= Unsafe.As<T, UInt32>(ref right);
            }

            if (typeof(T) == typeof(Int64))
            {
                return Unsafe.As<T, Int64>(ref left) >= Unsafe.As<T, Int64>(ref right);
            }

            if (typeof(T) == typeof(UInt64))
            {
                return Unsafe.As<T, UInt64>(ref left) >= Unsafe.As<T, UInt64>(ref right);
            }

            if (typeof(T) == typeof(Single))
            {
                return Unsafe.As<T, Single>(ref left) >= Unsafe.As<T, Single>(ref right);
            }

            if (typeof(T) == typeof(Double))
            {
                return Unsafe.As<T, Double>(ref left) >= Unsafe.As<T, Double>(ref right);
            }

            if (typeof(T) == typeof(Decimal))
            {
                return Unsafe.As<T, Decimal>(ref left) >= Unsafe.As<T, Decimal>(ref right);
            }

            throw new NotSupportedException($"Operator >= is not supported for {typeof(T)} type");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Less<T>(T left, T right) where T : unmanaged, IConvertible
        {
            if (typeof(T) == typeof(Char))
            {
                return Unsafe.As<T, Char>(ref left) < Unsafe.As<T, Char>(ref right);
            }

            if (typeof(T) == typeof(SByte))
            {
                return Unsafe.As<T, SByte>(ref left) < Unsafe.As<T, SByte>(ref right);
            }

            if (typeof(T) == typeof(Byte))
            {
                return Unsafe.As<T, Byte>(ref left) < Unsafe.As<T, Byte>(ref right);
            }

            if (typeof(T) == typeof(Int16))
            {
                return Unsafe.As<T, Int16>(ref left) < Unsafe.As<T, Int16>(ref right);
            }

            if (typeof(T) == typeof(UInt16))
            {
                return Unsafe.As<T, UInt16>(ref left) < Unsafe.As<T, UInt16>(ref right);
            }

            if (typeof(T) == typeof(Int32))
            {
                return Unsafe.As<T, Int32>(ref left) < Unsafe.As<T, Int32>(ref right);
            }

            if (typeof(T) == typeof(UInt32))
            {
                return Unsafe.As<T, UInt32>(ref left) < Unsafe.As<T, UInt32>(ref right);
            }

            if (typeof(T) == typeof(Int64))
            {
                return Unsafe.As<T, Int64>(ref left) < Unsafe.As<T, Int64>(ref right);
            }

            if (typeof(T) == typeof(UInt64))
            {
                return Unsafe.As<T, UInt64>(ref left) < Unsafe.As<T, UInt64>(ref right);
            }

            if (typeof(T) == typeof(Single))
            {
                return Unsafe.As<T, Single>(ref left) < Unsafe.As<T, Single>(ref right);
            }

            if (typeof(T) == typeof(Double))
            {
                return Unsafe.As<T, Double>(ref left) < Unsafe.As<T, Double>(ref right);
            }

            if (typeof(T) == typeof(Decimal))
            {
                return Unsafe.As<T, Decimal>(ref left) < Unsafe.As<T, Decimal>(ref right);
            }

            throw new NotSupportedException($"Operator < is not supported for {typeof(T)} type");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean LessEqual<T>(T left, T right) where T : unmanaged, IConvertible
        {
            if (typeof(T) == typeof(Char))
            {
                return Unsafe.As<T, Char>(ref left) <= Unsafe.As<T, Char>(ref right);
            }

            if (typeof(T) == typeof(SByte))
            {
                return Unsafe.As<T, SByte>(ref left) <= Unsafe.As<T, SByte>(ref right);
            }

            if (typeof(T) == typeof(Byte))
            {
                return Unsafe.As<T, Byte>(ref left) <= Unsafe.As<T, Byte>(ref right);
            }

            if (typeof(T) == typeof(Int16))
            {
                return Unsafe.As<T, Int16>(ref left) <= Unsafe.As<T, Int16>(ref right);
            }

            if (typeof(T) == typeof(UInt16))
            {
                return Unsafe.As<T, UInt16>(ref left) <= Unsafe.As<T, UInt16>(ref right);
            }

            if (typeof(T) == typeof(Int32))
            {
                return Unsafe.As<T, Int32>(ref left) <= Unsafe.As<T, Int32>(ref right);
            }

            if (typeof(T) == typeof(UInt32))
            {
                return Unsafe.As<T, UInt32>(ref left) <= Unsafe.As<T, UInt32>(ref right);
            }

            if (typeof(T) == typeof(Int64))
            {
                return Unsafe.As<T, Int64>(ref left) <= Unsafe.As<T, Int64>(ref right);
            }

            if (typeof(T) == typeof(UInt64))
            {
                return Unsafe.As<T, UInt64>(ref left) <= Unsafe.As<T, UInt64>(ref right);
            }

            if (typeof(T) == typeof(Single))
            {
                return Unsafe.As<T, Single>(ref left) <= Unsafe.As<T, Single>(ref right);
            }

            if (typeof(T) == typeof(Double))
            {
                return Unsafe.As<T, Double>(ref left) <= Unsafe.As<T, Double>(ref right);
            }

            if (typeof(T) == typeof(Decimal))
            {
                return Unsafe.As<T, Decimal>(ref left) <= Unsafe.As<T, Decimal>(ref right);
            }

            throw new NotSupportedException($"Operator <= is not supported for {typeof(T)} type");
        }
    }
}