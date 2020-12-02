// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;

namespace NetExtender.Utils.Numerics
{   
    public static partial class RandomUtils
    {
		public static SByte NextSByte(SByte max = SByte.MaxValue)
		{
			return NextSByte(SByte.MinValue, max);
		}

		public static SByte NextSByte(SByte min, SByte max)
		{
			if (max == min)
			{
				return min;
			}

			if (max < min)
			{
				(min, max) = (max, min);
			}

			Double value = NextDouble();
			return (SByte) (MathUtils.RoundBanking(value * max - value * min) + min);
		}

		public static Byte NextByte(Byte max = Byte.MaxValue)
		{
			return NextByte(Byte.MinValue, max);
		}

		public static Byte NextByte(Byte min, Byte max)
		{
			if (max == min)
			{
				return min;
			}

			if (max < min)
			{
				(min, max) = (max, min);
			}

			Double value = NextDouble();
			return (Byte) (MathUtils.RoundBanking(value * max - value * min) + min);
		}

		public static Int16 NextInt16(Int16 max = Int16.MaxValue)
		{
			return NextInt16(Int16.MinValue, max);
		}

		public static Int16 NextInt16(Int16 min, Int16 max)
		{
			if (max == min)
			{
				return min;
			}

			if (max < min)
			{
				(min, max) = (max, min);
			}

			Double value = NextDouble();
			return (Int16) (MathUtils.RoundBanking(value * max - value * min) + min);
		}

		public static UInt16 NextUInt16(UInt16 max = UInt16.MaxValue)
		{
			return NextUInt16(UInt16.MinValue, max);
		}

		public static UInt16 NextUInt16(UInt16 min, UInt16 max)
		{
			if (max == min)
			{
				return min;
			}

			if (max < min)
			{
				(min, max) = (max, min);
			}

			Double value = NextDouble();
			return (UInt16) (MathUtils.RoundBanking(value * max - value * min) + min);
		}

		public static Int32 NextInt32(Int32 max = Int32.MaxValue)
		{
			return NextInt32(Int32.MinValue, max);
		}

		public static Int32 NextInt32(Int32 min, Int32 max)
		{
			if (max == min)
			{
				return min;
			}

			if (max < min)
			{
				(min, max) = (max, min);
			}

			Double value = NextDouble();
			return (Int32) (MathUtils.RoundBanking(value * max - value * min) + min);
		}

		public static UInt32 NextUInt32(UInt32 max = UInt32.MaxValue)
		{
			return NextUInt32(UInt32.MinValue, max);
		}

		public static UInt32 NextUInt32(UInt32 min, UInt32 max)
		{
			if (max == min)
			{
				return min;
			}

			if (max < min)
			{
				(min, max) = (max, min);
			}

			Double value = NextDouble();
			return (UInt32) (MathUtils.RoundBanking(value * max - value * min) + min);
		}

		public static Int64 NextInt64(Int64 max = Int64.MaxValue)
		{
			return NextInt64(Int64.MinValue, max);
		}

		public static Int64 NextInt64(Int64 min, Int64 max)
		{
			if (max == min)
			{
				return min;
			}

			if (max < min)
			{
				(min, max) = (max, min);
			}

			Double value = NextDouble();
			return (Int64) (MathUtils.RoundBanking(value * max - value * min) + min);
		}

		public static UInt64 NextUInt64(UInt64 max = UInt64.MaxValue)
		{
			return NextUInt64(UInt64.MinValue, max);
		}

		public static UInt64 NextUInt64(UInt64 min, UInt64 max)
		{
			if (max == min)
			{
				return min;
			}

			if (max < min)
			{
				(min, max) = (max, min);
			}

			Double value = NextDouble();
			return (UInt64) (MathUtils.RoundBanking(value * max - value * min) + min);
		}

		public static SByte NextNonZeroSByte(SByte max = SByte.MaxValue)
		{
			return NextNonZeroSByte(SByte.MinValue, max);
		}

		public static SByte NextNonZeroSByte(SByte min, SByte max)
		{
			SByte value;

			do
			{
				value = NextSByte(min, max);

			} while (value == 0);

			return value;
		}

		public static Byte NextNonZeroByte(Byte max = Byte.MaxValue)
		{
			return NextNonZeroByte(Byte.MinValue, max);
		}

		public static Byte NextNonZeroByte(Byte min, Byte max)
		{
			Byte value;

			do
			{
				value = NextByte(min, max);

			} while (value == 0);

			return value;
		}

		public static Int16 NextNonZeroInt16(Int16 max = Int16.MaxValue)
		{
			return NextNonZeroInt16(Int16.MinValue, max);
		}

		public static Int16 NextNonZeroInt16(Int16 min, Int16 max)
		{
			Int16 value;

			do
			{
				value = NextInt16(min, max);

			} while (value == 0);

			return value;
		}

		public static UInt16 NextNonZeroUInt16(UInt16 max = UInt16.MaxValue)
		{
			return NextNonZeroUInt16(UInt16.MinValue, max);
		}

		public static UInt16 NextNonZeroUInt16(UInt16 min, UInt16 max)
		{
			UInt16 value;

			do
			{
				value = NextUInt16(min, max);

			} while (value == 0);

			return value;
		}

		public static Int32 NextNonZeroInt32(Int32 max = Int32.MaxValue)
		{
			return NextNonZeroInt32(Int32.MinValue, max);
		}

		public static Int32 NextNonZeroInt32(Int32 min, Int32 max)
		{
			Int32 value;

			do
			{
				value = NextInt32(min, max);

			} while (value == 0);

			return value;
		}

		public static UInt32 NextNonZeroUInt32(UInt32 max = UInt32.MaxValue)
		{
			return NextNonZeroUInt32(UInt32.MinValue, max);
		}

		public static UInt32 NextNonZeroUInt32(UInt32 min, UInt32 max)
		{
			UInt32 value;

			do
			{
				value = NextUInt32(min, max);

			} while (value == 0);

			return value;
		}

		public static Int64 NextNonZeroInt64(Int64 max = Int64.MaxValue)
		{
			return NextNonZeroInt64(Int64.MinValue, max);
		}

		public static Int64 NextNonZeroInt64(Int64 min, Int64 max)
		{
			Int64 value;

			do
			{
				value = NextInt64(min, max);

			} while (value == 0);

			return value;
		}

		public static UInt64 NextNonZeroUInt64(UInt64 max = UInt64.MaxValue)
		{
			return NextNonZeroUInt64(UInt64.MinValue, max);
		}

		public static UInt64 NextNonZeroUInt64(UInt64 min, UInt64 max)
		{
			UInt64 value;

			do
			{
				value = NextUInt64(min, max);

			} while (value == 0);

			return value;
		}

		public static Single NextNonZeroSingle(Single max = Single.MaxValue)
		{
			return NextNonZeroSingle(Single.MinValue, max);
		}

		public static Single NextNonZeroSingle(Single min, Single max)
		{
			Single value;

			do
			{
				value = NextSingle(min, max);

			} while (value == 0);

			return value;
		}

		public static Double NextNonZeroDouble(Double max = Double.MaxValue)
		{
			return NextNonZeroDouble(Double.MinValue, max);
		}

		public static Double NextNonZeroDouble(Double min, Double max)
		{
			Double value;

			do
			{
				value = NextDouble(min, max);

			} while (value == 0);

			return value;
		}

		public static Decimal NextNonZeroDecimal(Decimal max = Decimal.MaxValue)
		{
			return NextNonZeroDecimal(Decimal.MinValue, max);
		}

		public static Decimal NextNonZeroDecimal(Decimal min, Decimal max)
		{
			Decimal value;

			do
			{
				value = NextDecimal(min, max);

			} while (value == 0);

			return value;
		}

		public static SByte NextSignSByte(Double chance = 0.5)
		{
			return NextBoolean(chance) ? 1 : -1;
		}

		public static Int16 NextSignInt16(Double chance = 0.5)
		{
			return NextBoolean(chance) ? 1 : -1;
		}

		public static Int32 NextSignInt32(Double chance = 0.5)
		{
			return NextBoolean(chance) ? 1 : -1;
		}

		public static Int64 NextSignInt64(Double chance = 0.5)
		{
			return NextBoolean(chance) ? 1 : -1;
		}

		public static Single NextSignSingle(Double chance = 0.5)
		{
			return NextBoolean(chance) ? 1 : -1;
		}

		public static Double NextSignDouble(Double chance = 0.5)
		{
			return NextBoolean(chance) ? 1 : -1;
		}

		public static Decimal NextSignDecimal(Double chance = 0.5)
		{
			return NextBoolean(chance) ? 1 : -1;
		}

		public static IEnumerable<SByte> SByteRange(SByte max, Int32 count = Int32.MaxValue)
		{
			return SByteRange(SByte.MinValue, max, count);
		}

		public static IEnumerable<SByte> SByteRange(SByte min, SByte max, Int32 count = Int32.MaxValue)
		{
			return Range(min, max, count, NextSByte);
		}

		public static IEnumerable<Byte> ByteRange(Byte max, Int32 count = Int32.MaxValue)
		{
			return ByteRange(Byte.MinValue, max, count);
		}

		public static IEnumerable<Byte> ByteRange(Byte min, Byte max, Int32 count = Int32.MaxValue)
		{
			return Range(min, max, count, NextByte);
		}

		public static IEnumerable<Int16> Int16Range(Int16 max, Int32 count = Int32.MaxValue)
		{
			return Int16Range(Int16.MinValue, max, count);
		}

		public static IEnumerable<Int16> Int16Range(Int16 min, Int16 max, Int32 count = Int32.MaxValue)
		{
			return Range(min, max, count, NextInt16);
		}

		public static IEnumerable<UInt16> UInt16Range(UInt16 max, Int32 count = Int32.MaxValue)
		{
			return UInt16Range(UInt16.MinValue, max, count);
		}

		public static IEnumerable<UInt16> UInt16Range(UInt16 min, UInt16 max, Int32 count = Int32.MaxValue)
		{
			return Range(min, max, count, NextUInt16);
		}

		public static IEnumerable<Int32> Int32Range(Int32 count = Int32.MaxValue)
		{
			return Int32Range(Int32.MinValue, Int32.MaxValue, count);
		}

		public static IEnumerable<Int32> Int32Range(Int32 max, Int32 count)
		{
			return Int32Range(Int32.MinValue, max, count);
		}

		public static IEnumerable<Int32> Int32Range(Int32 min, Int32 max, Int32 count)
		{
			return Range(min, max, count, NextInt32);
		}

		public static IEnumerable<UInt32> UInt32Range(UInt32 max, Int32 count = Int32.MaxValue)
		{
			return UInt32Range(UInt32.MinValue, max, count);
		}

		public static IEnumerable<UInt32> UInt32Range(UInt32 min, UInt32 max, Int32 count = Int32.MaxValue)
		{
			return Range(min, max, count, NextUInt32);
		}

		public static IEnumerable<Int64> Int64Range(Int64 max, Int32 count = Int32.MaxValue)
		{
			return Int64Range(Int64.MinValue, max, count);
		}

		public static IEnumerable<Int64> Int64Range(Int64 min, Int64 max, Int32 count = Int32.MaxValue)
		{
			return Range(min, max, count, NextInt64);
		}

		public static IEnumerable<UInt64> UInt64Range(UInt64 max, Int32 count = Int32.MaxValue)
		{
			return UInt64Range(UInt64.MinValue, max, count);
		}

		public static IEnumerable<UInt64> UInt64Range(UInt64 min, UInt64 max, Int32 count = Int32.MaxValue)
		{
			return Range(min, max, count, NextUInt64);
		}

		public static IEnumerable<Single> SingleRange(Single max, Int32 count = Int32.MaxValue)
		{
			return SingleRange(Single.MinValue, max, count);
		}

		public static IEnumerable<Single> SingleRange(Single min, Single max, Int32 count = Int32.MaxValue)
		{
			return Range(min, max, count, NextSingle);
		}

		public static IEnumerable<Double> DoubleRange(Double max, Int32 count = Int32.MaxValue)
		{
			return DoubleRange(Double.MinValue, max, count);
		}

		public static IEnumerable<Double> DoubleRange(Double min, Double max, Int32 count = Int32.MaxValue)
		{
			return Range(min, max, count, NextDouble);
		}

		public static IEnumerable<Decimal> DecimalRange(Decimal max, Int32 count = Int32.MaxValue)
		{
			return DecimalRange(Decimal.MinValue, max, count);
		}

		public static IEnumerable<Decimal> DecimalRange(Decimal min, Decimal max, Int32 count = Int32.MaxValue)
		{
			return Range(min, max, count, NextDecimal);
		}

		public static IEnumerable<SByte> SByteNonZeroRange(SByte max, Int32 count = Int32.MaxValue)
		{
			return SByteNonZeroRange(SByte.MinValue, max, count);
		}

		public static IEnumerable<SByte> SByteNonZeroRange(SByte min, SByte max, Int32 count = Int32.MaxValue)
		{
			return Range(min, max, count, NextNonZeroSByte);
		}

		public static IEnumerable<Byte> ByteNonZeroRange(Byte max, Int32 count = Int32.MaxValue)
		{
			return ByteNonZeroRange(Byte.MinValue, max, count);
		}

		public static IEnumerable<Byte> ByteNonZeroRange(Byte min, Byte max, Int32 count = Int32.MaxValue)
		{
			return Range(min, max, count, NextNonZeroByte);
		}

		public static IEnumerable<Int16> Int16NonZeroRange(Int16 max, Int32 count = Int32.MaxValue)
		{
			return Int16NonZeroRange(Int16.MinValue, max, count);
		}

		public static IEnumerable<Int16> Int16NonZeroRange(Int16 min, Int16 max, Int32 count = Int32.MaxValue)
		{
			return Range(min, max, count, NextNonZeroInt16);
		}

		public static IEnumerable<UInt16> UInt16NonZeroRange(UInt16 max, Int32 count = Int32.MaxValue)
		{
			return UInt16NonZeroRange(UInt16.MinValue, max, count);
		}

		public static IEnumerable<UInt16> UInt16NonZeroRange(UInt16 min, UInt16 max, Int32 count = Int32.MaxValue)
		{
			return Range(min, max, count, NextNonZeroUInt16);
		}

		public static IEnumerable<Int32> Int32NonZeroRange(Int32 count = Int32.MaxValue)
		{
			return Int32NonZeroRange(Int32.MinValue, Int32.MaxValue, count);
		}

		public static IEnumerable<Int32> Int32NonZeroRange(Int32 max, Int32 count)
		{
			return Int32NonZeroRange(Int32.MinValue, max, count);
		}

		public static IEnumerable<Int32> Int32NonZeroRange(Int32 min, Int32 max, Int32 count)
		{
			return Range(min, max, count, NextNonZeroInt32);
		}

		public static IEnumerable<UInt32> UInt32NonZeroRange(UInt32 max, Int32 count = Int32.MaxValue)
		{
			return UInt32NonZeroRange(UInt32.MinValue, max, count);
		}

		public static IEnumerable<UInt32> UInt32NonZeroRange(UInt32 min, UInt32 max, Int32 count = Int32.MaxValue)
		{
			return Range(min, max, count, NextNonZeroUInt32);
		}

		public static IEnumerable<Int64> Int64NonZeroRange(Int64 max, Int32 count = Int32.MaxValue)
		{
			return Int64NonZeroRange(Int64.MinValue, max, count);
		}

		public static IEnumerable<Int64> Int64NonZeroRange(Int64 min, Int64 max, Int32 count = Int32.MaxValue)
		{
			return Range(min, max, count, NextNonZeroInt64);
		}

		public static IEnumerable<UInt64> UInt64NonZeroRange(UInt64 max, Int32 count = Int32.MaxValue)
		{
			return UInt64NonZeroRange(UInt64.MinValue, max, count);
		}

		public static IEnumerable<UInt64> UInt64NonZeroRange(UInt64 min, UInt64 max, Int32 count = Int32.MaxValue)
		{
			return Range(min, max, count, NextNonZeroUInt64);
		}

		public static IEnumerable<Single> SingleNonZeroRange(Single max, Int32 count = Int32.MaxValue)
		{
			return SingleNonZeroRange(Single.MinValue, max, count);
		}

		public static IEnumerable<Single> SingleNonZeroRange(Single min, Single max, Int32 count = Int32.MaxValue)
		{
			return Range(min, max, count, NextNonZeroSingle);
		}

		public static IEnumerable<Double> DoubleNonZeroRange(Double max, Int32 count = Int32.MaxValue)
		{
			return DoubleNonZeroRange(Double.MinValue, max, count);
		}

		public static IEnumerable<Double> DoubleNonZeroRange(Double min, Double max, Int32 count = Int32.MaxValue)
		{
			return Range(min, max, count, NextNonZeroDouble);
		}

		public static IEnumerable<Decimal> DecimalNonZeroRange(Decimal max, Int32 count = Int32.MaxValue)
		{
			return DecimalNonZeroRange(Decimal.MinValue, max, count);
		}

		public static IEnumerable<Decimal> DecimalNonZeroRange(Decimal min, Decimal max, Int32 count = Int32.MaxValue)
		{
			return Range(min, max, count, NextNonZeroDecimal);
		}
    }  
}