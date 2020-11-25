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

		public static IEnumerable<SByte> Range(SByte max, Int32 count = Int32.MaxValue)
		{
			return Range(SByte.MinValue, max, count);
		}

		public static IEnumerable<SByte> Range(SByte min, SByte max, Int32 count = Int32.MaxValue)
		{
			if (count < 0)
			{
				throw new ArgumentException("Count can't be less than -1");
			}

			if (count == 0)
			{
				yield break;
			}

			for (Int32 i = 0; i < count; i++)
			{
				yield return NextSByte(min, max);
			}
		}

		public static IEnumerable<Byte> Range(Byte max, Int32 count = Int32.MaxValue)
		{
			return Range(Byte.MinValue, max, count);
		}

		public static IEnumerable<Byte> Range(Byte min, Byte max, Int32 count = Int32.MaxValue)
		{
			if (count < 0)
			{
				throw new ArgumentException("Count can't be less than -1");
			}

			if (count == 0)
			{
				yield break;
			}

			for (Int32 i = 0; i < count; i++)
			{
				yield return NextByte(min, max);
			}
		}

		public static IEnumerable<Int16> Range(Int16 max, Int32 count = Int32.MaxValue)
		{
			return Range(Int16.MinValue, max, count);
		}

		public static IEnumerable<Int16> Range(Int16 min, Int16 max, Int32 count = Int32.MaxValue)
		{
			if (count < 0)
			{
				throw new ArgumentException("Count can't be less than -1");
			}

			if (count == 0)
			{
				yield break;
			}

			for (Int32 i = 0; i < count; i++)
			{
				yield return NextInt16(min, max);
			}
		}

		public static IEnumerable<UInt16> Range(UInt16 max, Int32 count = Int32.MaxValue)
		{
			return Range(UInt16.MinValue, max, count);
		}

		public static IEnumerable<UInt16> Range(UInt16 min, UInt16 max, Int32 count = Int32.MaxValue)
		{
			if (count < 0)
			{
				throw new ArgumentException("Count can't be less than -1");
			}

			if (count == 0)
			{
				yield break;
			}

			for (Int32 i = 0; i < count; i++)
			{
				yield return NextUInt16(min, max);
			}
		}

		public static IEnumerable<Int32> Range(Int32 count = Int32.MaxValue)
		{
			return Range(Int32.MinValue, Int32.MaxValue, count);
		}

		public static IEnumerable<Int32> Range(Int32 max, Int32 count)
		{
			return Range(Int32.MinValue, max, count);
		}

		public static IEnumerable<Int32> Range(Int32 min, Int32 max, Int32 count)
		{
			if (count < 0)
			{
				throw new ArgumentException("Count can't be less than -1");
			}

			if (count == 0)
			{
				yield break;
			}

			for (Int32 i = 0; i < count; i++)
			{
				yield return NextInt32(min, max);
			}
		}

		public static IEnumerable<UInt32> Range(UInt32 max, Int32 count = Int32.MaxValue)
		{
			return Range(UInt32.MinValue, max, count);
		}

		public static IEnumerable<UInt32> Range(UInt32 min, UInt32 max, Int32 count = Int32.MaxValue)
		{
			if (count < 0)
			{
				throw new ArgumentException("Count can't be less than -1");
			}

			if (count == 0)
			{
				yield break;
			}

			for (Int32 i = 0; i < count; i++)
			{
				yield return NextUInt32(min, max);
			}
		}

		public static IEnumerable<Int64> Range(Int64 max, Int32 count = Int32.MaxValue)
		{
			return Range(Int64.MinValue, max, count);
		}

		public static IEnumerable<Int64> Range(Int64 min, Int64 max, Int32 count = Int32.MaxValue)
		{
			if (count < 0)
			{
				throw new ArgumentException("Count can't be less than -1");
			}

			if (count == 0)
			{
				yield break;
			}

			for (Int32 i = 0; i < count; i++)
			{
				yield return NextInt64(min, max);
			}
		}

		public static IEnumerable<UInt64> Range(UInt64 max, Int32 count = Int32.MaxValue)
		{
			return Range(UInt64.MinValue, max, count);
		}

		public static IEnumerable<UInt64> Range(UInt64 min, UInt64 max, Int32 count = Int32.MaxValue)
		{
			if (count < 0)
			{
				throw new ArgumentException("Count can't be less than -1");
			}

			if (count == 0)
			{
				yield break;
			}

			for (Int32 i = 0; i < count; i++)
			{
				yield return NextUInt64(min, max);
			}
		}

		public static IEnumerable<Single> Range(Single max, Int32 count = Int32.MaxValue)
		{
			return Range(Single.MinValue, max, count);
		}

		public static IEnumerable<Single> Range(Single min, Single max, Int32 count = Int32.MaxValue)
		{
			if (count < 0)
			{
				throw new ArgumentException("Count can't be less than -1");
			}

			if (count == 0)
			{
				yield break;
			}

			for (Int32 i = 0; i < count; i++)
			{
				yield return NextSingle(min, max);
			}
		}

		public static IEnumerable<Double> Range(Double max, Int32 count = Int32.MaxValue)
		{
			return Range(Double.MinValue, max, count);
		}

		public static IEnumerable<Double> Range(Double min, Double max, Int32 count = Int32.MaxValue)
		{
			if (count < 0)
			{
				throw new ArgumentException("Count can't be less than -1");
			}

			if (count == 0)
			{
				yield break;
			}

			for (Int32 i = 0; i < count; i++)
			{
				yield return NextDouble(min, max);
			}
		}

		public static IEnumerable<Decimal> Range(Decimal max, Int32 count = Int32.MaxValue)
		{
			return Range(Decimal.MinValue, max, count);
		}

		public static IEnumerable<Decimal> Range(Decimal min, Decimal max, Int32 count = Int32.MaxValue)
		{
			if (count < 0)
			{
				throw new ArgumentException("Count can't be less than -1");
			}

			if (count == 0)
			{
				yield break;
			}

			for (Int32 i = 0; i < count; i++)
			{
				yield return NextDecimal(min, max);
			}
		}
    }  
}