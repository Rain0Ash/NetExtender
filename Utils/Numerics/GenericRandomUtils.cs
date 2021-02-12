// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;
using NetExtender.Random.Interfaces;

namespace NetExtender.Utils.Numerics
{
    public static partial class RandomUtils
    {
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static SByte NextSByte()
		{
			return NextSByte(Generator);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static SByte NextSByte(this System.Random random)
		{
			return NextSByte(random, SByte.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static SByte NextSByte(this IRandom random)
		{
			return NextSByte(random, SByte.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static SByte NextSByte(SByte max)
		{
			return NextSByte(Generator, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static SByte NextSByte(this System.Random random, SByte max)
		{
			return NextSByte(random, SByte.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static SByte NextSByte(this IRandom random, SByte max)
		{
			return NextSByte(random, SByte.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static SByte NextSByte(SByte min, SByte max)
		{
			return NextSByte(Generator, min, max);
		}

		public static SByte NextSByte(this System.Random random, SByte min, SByte max)
		{
			if (max == min)
			{
				return min;
			}

			if (max < min)
			{
				(min, max) = (max, min);
			}

			Double value = NextDouble(random);
			return (SByte) (MathUtils.RoundBanking(value * max - value * min) + min);
		}

		public static SByte NextSByte(this IRandom random, SByte min, SByte max)
		{
			if (max == min)
			{
				return min;
			}

			if (max < min)
			{
				(min, max) = (max, min);
			}

			Double value = NextDouble(random);
			return (SByte) (MathUtils.RoundBanking(value * max - value * min) + min);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Byte NextByte()
		{
			return NextByte(Generator);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Byte NextByte(this System.Random random)
		{
			return NextByte(random, Byte.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Byte NextByte(this IRandom random)
		{
			return NextByte(random, Byte.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Byte NextByte(Byte max)
		{
			return NextByte(Generator, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Byte NextByte(this System.Random random, Byte max)
		{
			return NextByte(random, Byte.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Byte NextByte(this IRandom random, Byte max)
		{
			return NextByte(random, Byte.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Byte NextByte(Byte min, Byte max)
		{
			return NextByte(Generator, min, max);
		}

		public static Byte NextByte(this System.Random random, Byte min, Byte max)
		{
			if (max == min)
			{
				return min;
			}

			if (max < min)
			{
				(min, max) = (max, min);
			}

			Double value = NextDouble(random);
			return (Byte) (MathUtils.RoundBanking(value * max - value * min) + min);
		}

		public static Byte NextByte(this IRandom random, Byte min, Byte max)
		{
			if (max == min)
			{
				return min;
			}

			if (max < min)
			{
				(min, max) = (max, min);
			}

			Double value = NextDouble(random);
			return (Byte) (MathUtils.RoundBanking(value * max - value * min) + min);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int16 NextInt16()
		{
			return NextInt16(Generator);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int16 NextInt16(this System.Random random)
		{
			return NextInt16(random, Int16.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int16 NextInt16(this IRandom random)
		{
			return NextInt16(random, Int16.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int16 NextInt16(Int16 max)
		{
			return NextInt16(Generator, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int16 NextInt16(this System.Random random, Int16 max)
		{
			return NextInt16(random, Int16.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int16 NextInt16(this IRandom random, Int16 max)
		{
			return NextInt16(random, Int16.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int16 NextInt16(Int16 min, Int16 max)
		{
			return NextInt16(Generator, min, max);
		}

		public static Int16 NextInt16(this System.Random random, Int16 min, Int16 max)
		{
			if (max == min)
			{
				return min;
			}

			if (max < min)
			{
				(min, max) = (max, min);
			}

			Double value = NextDouble(random);
			return (Int16) (MathUtils.RoundBanking(value * max - value * min) + min);
		}

		public static Int16 NextInt16(this IRandom random, Int16 min, Int16 max)
		{
			if (max == min)
			{
				return min;
			}

			if (max < min)
			{
				(min, max) = (max, min);
			}

			Double value = NextDouble(random);
			return (Int16) (MathUtils.RoundBanking(value * max - value * min) + min);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static UInt16 NextUInt16()
		{
			return NextUInt16(Generator);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static UInt16 NextUInt16(this System.Random random)
		{
			return NextUInt16(random, UInt16.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static UInt16 NextUInt16(this IRandom random)
		{
			return NextUInt16(random, UInt16.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static UInt16 NextUInt16(UInt16 max)
		{
			return NextUInt16(Generator, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static UInt16 NextUInt16(this System.Random random, UInt16 max)
		{
			return NextUInt16(random, UInt16.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static UInt16 NextUInt16(this IRandom random, UInt16 max)
		{
			return NextUInt16(random, UInt16.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static UInt16 NextUInt16(UInt16 min, UInt16 max)
		{
			return NextUInt16(Generator, min, max);
		}

		public static UInt16 NextUInt16(this System.Random random, UInt16 min, UInt16 max)
		{
			if (max == min)
			{
				return min;
			}

			if (max < min)
			{
				(min, max) = (max, min);
			}

			Double value = NextDouble(random);
			return (UInt16) (MathUtils.RoundBanking(value * max - value * min) + min);
		}

		public static UInt16 NextUInt16(this IRandom random, UInt16 min, UInt16 max)
		{
			if (max == min)
			{
				return min;
			}

			if (max < min)
			{
				(min, max) = (max, min);
			}

			Double value = NextDouble(random);
			return (UInt16) (MathUtils.RoundBanking(value * max - value * min) + min);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int32 NextInt32()
		{
			return NextInt32(Generator);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int32 NextInt32(this System.Random random)
		{
			return NextInt32(random, Int32.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int32 NextInt32(this IRandom random)
		{
			return NextInt32(random, Int32.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int32 NextInt32(Int32 max)
		{
			return NextInt32(Generator, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int32 NextInt32(this System.Random random, Int32 max)
		{
			return NextInt32(random, Int32.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int32 NextInt32(this IRandom random, Int32 max)
		{
			return NextInt32(random, Int32.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int32 NextInt32(Int32 min, Int32 max)
		{
			return NextInt32(Generator, min, max);
		}

		public static Int32 NextInt32(this System.Random random, Int32 min, Int32 max)
		{
			if (max == min)
			{
				return min;
			}

			if (max < min)
			{
				(min, max) = (max, min);
			}

			Double value = NextDouble(random);
			return (Int32) (MathUtils.RoundBanking(value * max - value * min) + min);
		}

		public static Int32 NextInt32(this IRandom random, Int32 min, Int32 max)
		{
			if (max == min)
			{
				return min;
			}

			if (max < min)
			{
				(min, max) = (max, min);
			}

			Double value = NextDouble(random);
			return (Int32) (MathUtils.RoundBanking(value * max - value * min) + min);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static UInt32 NextUInt32()
		{
			return NextUInt32(Generator);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static UInt32 NextUInt32(this System.Random random)
		{
			return NextUInt32(random, UInt32.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static UInt32 NextUInt32(this IRandom random)
		{
			return NextUInt32(random, UInt32.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static UInt32 NextUInt32(UInt32 max)
		{
			return NextUInt32(Generator, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static UInt32 NextUInt32(this System.Random random, UInt32 max)
		{
			return NextUInt32(random, UInt32.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static UInt32 NextUInt32(this IRandom random, UInt32 max)
		{
			return NextUInt32(random, UInt32.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static UInt32 NextUInt32(UInt32 min, UInt32 max)
		{
			return NextUInt32(Generator, min, max);
		}

		public static UInt32 NextUInt32(this System.Random random, UInt32 min, UInt32 max)
		{
			if (max == min)
			{
				return min;
			}

			if (max < min)
			{
				(min, max) = (max, min);
			}

			Double value = NextDouble(random);
			return (UInt32) (MathUtils.RoundBanking(value * max - value * min) + min);
		}

		public static UInt32 NextUInt32(this IRandom random, UInt32 min, UInt32 max)
		{
			if (max == min)
			{
				return min;
			}

			if (max < min)
			{
				(min, max) = (max, min);
			}

			Double value = NextDouble(random);
			return (UInt32) (MathUtils.RoundBanking(value * max - value * min) + min);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int64 NextInt64()
		{
			return NextInt64(Generator);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int64 NextInt64(this System.Random random)
		{
			return NextInt64(random, Int64.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int64 NextInt64(this IRandom random)
		{
			return NextInt64(random, Int64.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int64 NextInt64(Int64 max)
		{
			return NextInt64(Generator, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int64 NextInt64(this System.Random random, Int64 max)
		{
			return NextInt64(random, Int64.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int64 NextInt64(this IRandom random, Int64 max)
		{
			return NextInt64(random, Int64.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int64 NextInt64(Int64 min, Int64 max)
		{
			return NextInt64(Generator, min, max);
		}

		public static Int64 NextInt64(this System.Random random, Int64 min, Int64 max)
		{
			if (max == min)
			{
				return min;
			}

			if (max < min)
			{
				(min, max) = (max, min);
			}

			Double value = NextDouble(random);
			return (Int64) (MathUtils.RoundBanking(value * max - value * min) + min);
		}

		public static Int64 NextInt64(this IRandom random, Int64 min, Int64 max)
		{
			if (max == min)
			{
				return min;
			}

			if (max < min)
			{
				(min, max) = (max, min);
			}

			Double value = NextDouble(random);
			return (Int64) (MathUtils.RoundBanking(value * max - value * min) + min);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static UInt64 NextUInt64()
		{
			return NextUInt64(Generator);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static UInt64 NextUInt64(this System.Random random)
		{
			return NextUInt64(random, UInt64.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static UInt64 NextUInt64(this IRandom random)
		{
			return NextUInt64(random, UInt64.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static UInt64 NextUInt64(UInt64 max)
		{
			return NextUInt64(Generator, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static UInt64 NextUInt64(this System.Random random, UInt64 max)
		{
			return NextUInt64(random, UInt64.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static UInt64 NextUInt64(this IRandom random, UInt64 max)
		{
			return NextUInt64(random, UInt64.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static UInt64 NextUInt64(UInt64 min, UInt64 max)
		{
			return NextUInt64(Generator, min, max);
		}

		public static UInt64 NextUInt64(this System.Random random, UInt64 min, UInt64 max)
		{
			if (max == min)
			{
				return min;
			}

			if (max < min)
			{
				(min, max) = (max, min);
			}

			Double value = NextDouble(random);
			return (UInt64) (MathUtils.RoundBanking(value * max - value * min) + min);
		}

		public static UInt64 NextUInt64(this IRandom random, UInt64 min, UInt64 max)
		{
			if (max == min)
			{
				return min;
			}

			if (max < min)
			{
				(min, max) = (max, min);
			}

			Double value = NextDouble(random);
			return (UInt64) (MathUtils.RoundBanking(value * max - value * min) + min);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static SByte NextNonNegativeSByte()
		{
			return NextNonNegativeSByte(Generator);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static SByte NextNonNegativeSByte(this System.Random random)
		{
			return NextNonNegativeSByte(random, SByte.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static SByte NextNonNegativeSByte(this IRandom random)
		{
			return NextNonNegativeSByte(random, SByte.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static SByte NextNonNegativeSByte(SByte max)
		{
			return NextNonNegativeSByte(Generator, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static SByte NextNonNegativeSByte(this System.Random random, SByte max)
		{
			return NextNonNegativeSByte(random, default, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static SByte NextNonNegativeSByte(this IRandom random, SByte max)
		{
			return NextNonNegativeSByte(random, default, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static SByte NextNonNegativeSByte(SByte min, SByte max)
		{
			return NextSByte(Generator, min, max);
		}

		public static SByte NextNonNegativeSByte(this System.Random random, SByte min, SByte max)
		{
			return NextSByte(random, min, max);
		}

		public static SByte NextNonNegativeSByte(this IRandom random, SByte min, SByte max)
		{
			return NextSByte(random, min, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Byte NextNonNegativeByte()
		{
			return NextNonNegativeByte(Generator);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Byte NextNonNegativeByte(this System.Random random)
		{
			return NextNonNegativeByte(random, Byte.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Byte NextNonNegativeByte(this IRandom random)
		{
			return NextNonNegativeByte(random, Byte.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Byte NextNonNegativeByte(Byte max)
		{
			return NextNonNegativeByte(Generator, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Byte NextNonNegativeByte(this System.Random random, Byte max)
		{
			return NextNonNegativeByte(random, default, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Byte NextNonNegativeByte(this IRandom random, Byte max)
		{
			return NextNonNegativeByte(random, default, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Byte NextNonNegativeByte(Byte min, Byte max)
		{
			return NextByte(Generator, min, max);
		}

		public static Byte NextNonNegativeByte(this System.Random random, Byte min, Byte max)
		{
			return NextByte(random, min, max);
		}

		public static Byte NextNonNegativeByte(this IRandom random, Byte min, Byte max)
		{
			return NextByte(random, min, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int16 NextNonNegativeInt16()
		{
			return NextNonNegativeInt16(Generator);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int16 NextNonNegativeInt16(this System.Random random)
		{
			return NextNonNegativeInt16(random, Int16.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int16 NextNonNegativeInt16(this IRandom random)
		{
			return NextNonNegativeInt16(random, Int16.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int16 NextNonNegativeInt16(Int16 max)
		{
			return NextNonNegativeInt16(Generator, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int16 NextNonNegativeInt16(this System.Random random, Int16 max)
		{
			return NextNonNegativeInt16(random, default, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int16 NextNonNegativeInt16(this IRandom random, Int16 max)
		{
			return NextNonNegativeInt16(random, default, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int16 NextNonNegativeInt16(Int16 min, Int16 max)
		{
			return NextInt16(Generator, min, max);
		}

		public static Int16 NextNonNegativeInt16(this System.Random random, Int16 min, Int16 max)
		{
			return NextInt16(random, min, max);
		}

		public static Int16 NextNonNegativeInt16(this IRandom random, Int16 min, Int16 max)
		{
			return NextInt16(random, min, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static UInt16 NextNonNegativeUInt16()
		{
			return NextNonNegativeUInt16(Generator);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static UInt16 NextNonNegativeUInt16(this System.Random random)
		{
			return NextNonNegativeUInt16(random, UInt16.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static UInt16 NextNonNegativeUInt16(this IRandom random)
		{
			return NextNonNegativeUInt16(random, UInt16.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static UInt16 NextNonNegativeUInt16(UInt16 max)
		{
			return NextNonNegativeUInt16(Generator, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static UInt16 NextNonNegativeUInt16(this System.Random random, UInt16 max)
		{
			return NextNonNegativeUInt16(random, default, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static UInt16 NextNonNegativeUInt16(this IRandom random, UInt16 max)
		{
			return NextNonNegativeUInt16(random, default, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static UInt16 NextNonNegativeUInt16(UInt16 min, UInt16 max)
		{
			return NextUInt16(Generator, min, max);
		}

		public static UInt16 NextNonNegativeUInt16(this System.Random random, UInt16 min, UInt16 max)
		{
			return NextUInt16(random, min, max);
		}

		public static UInt16 NextNonNegativeUInt16(this IRandom random, UInt16 min, UInt16 max)
		{
			return NextUInt16(random, min, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int32 NextNonNegativeInt32()
		{
			return NextNonNegativeInt32(Generator);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int32 NextNonNegativeInt32(this System.Random random)
		{
			return NextNonNegativeInt32(random, Int32.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int32 NextNonNegativeInt32(this IRandom random)
		{
			return NextNonNegativeInt32(random, Int32.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int32 NextNonNegativeInt32(Int32 max)
		{
			return NextNonNegativeInt32(Generator, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int32 NextNonNegativeInt32(this System.Random random, Int32 max)
		{
			return NextNonNegativeInt32(random, default, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int32 NextNonNegativeInt32(this IRandom random, Int32 max)
		{
			return NextNonNegativeInt32(random, default, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int32 NextNonNegativeInt32(Int32 min, Int32 max)
		{
			return NextInt32(Generator, min, max);
		}

		public static Int32 NextNonNegativeInt32(this System.Random random, Int32 min, Int32 max)
		{
			return NextInt32(random, min, max);
		}

		public static Int32 NextNonNegativeInt32(this IRandom random, Int32 min, Int32 max)
		{
			return NextInt32(random, min, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static UInt32 NextNonNegativeUInt32()
		{
			return NextNonNegativeUInt32(Generator);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static UInt32 NextNonNegativeUInt32(this System.Random random)
		{
			return NextNonNegativeUInt32(random, UInt32.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static UInt32 NextNonNegativeUInt32(this IRandom random)
		{
			return NextNonNegativeUInt32(random, UInt32.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static UInt32 NextNonNegativeUInt32(UInt32 max)
		{
			return NextNonNegativeUInt32(Generator, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static UInt32 NextNonNegativeUInt32(this System.Random random, UInt32 max)
		{
			return NextNonNegativeUInt32(random, default, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static UInt32 NextNonNegativeUInt32(this IRandom random, UInt32 max)
		{
			return NextNonNegativeUInt32(random, default, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static UInt32 NextNonNegativeUInt32(UInt32 min, UInt32 max)
		{
			return NextUInt32(Generator, min, max);
		}

		public static UInt32 NextNonNegativeUInt32(this System.Random random, UInt32 min, UInt32 max)
		{
			return NextUInt32(random, min, max);
		}

		public static UInt32 NextNonNegativeUInt32(this IRandom random, UInt32 min, UInt32 max)
		{
			return NextUInt32(random, min, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int64 NextNonNegativeInt64()
		{
			return NextNonNegativeInt64(Generator);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int64 NextNonNegativeInt64(this System.Random random)
		{
			return NextNonNegativeInt64(random, Int64.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int64 NextNonNegativeInt64(this IRandom random)
		{
			return NextNonNegativeInt64(random, Int64.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int64 NextNonNegativeInt64(Int64 max)
		{
			return NextNonNegativeInt64(Generator, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int64 NextNonNegativeInt64(this System.Random random, Int64 max)
		{
			return NextNonNegativeInt64(random, default, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int64 NextNonNegativeInt64(this IRandom random, Int64 max)
		{
			return NextNonNegativeInt64(random, default, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int64 NextNonNegativeInt64(Int64 min, Int64 max)
		{
			return NextInt64(Generator, min, max);
		}

		public static Int64 NextNonNegativeInt64(this System.Random random, Int64 min, Int64 max)
		{
			return NextInt64(random, min, max);
		}

		public static Int64 NextNonNegativeInt64(this IRandom random, Int64 min, Int64 max)
		{
			return NextInt64(random, min, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static UInt64 NextNonNegativeUInt64()
		{
			return NextNonNegativeUInt64(Generator);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static UInt64 NextNonNegativeUInt64(this System.Random random)
		{
			return NextNonNegativeUInt64(random, UInt64.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static UInt64 NextNonNegativeUInt64(this IRandom random)
		{
			return NextNonNegativeUInt64(random, UInt64.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static UInt64 NextNonNegativeUInt64(UInt64 max)
		{
			return NextNonNegativeUInt64(Generator, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static UInt64 NextNonNegativeUInt64(this System.Random random, UInt64 max)
		{
			return NextNonNegativeUInt64(random, default, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static UInt64 NextNonNegativeUInt64(this IRandom random, UInt64 max)
		{
			return NextNonNegativeUInt64(random, default, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static UInt64 NextNonNegativeUInt64(UInt64 min, UInt64 max)
		{
			return NextUInt64(Generator, min, max);
		}

		public static UInt64 NextNonNegativeUInt64(this System.Random random, UInt64 min, UInt64 max)
		{
			return NextUInt64(random, min, max);
		}

		public static UInt64 NextNonNegativeUInt64(this IRandom random, UInt64 min, UInt64 max)
		{
			return NextUInt64(random, min, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Single NextNonNegativeSingle()
		{
			return NextNonNegativeSingle(Generator);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Single NextNonNegativeSingle(this System.Random random)
		{
			return NextNonNegativeSingle(random, Single.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Single NextNonNegativeSingle(this IRandom random)
		{
			return NextNonNegativeSingle(random, Single.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Single NextNonNegativeSingle(Single max)
		{
			return NextNonNegativeSingle(Generator, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Single NextNonNegativeSingle(this System.Random random, Single max)
		{
			return NextNonNegativeSingle(random, default, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Single NextNonNegativeSingle(this IRandom random, Single max)
		{
			return NextNonNegativeSingle(random, default, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Single NextNonNegativeSingle(Single min, Single max)
		{
			return NextSingle(Generator, min, max);
		}

		public static Single NextNonNegativeSingle(this System.Random random, Single min, Single max)
		{
			return NextSingle(random, min, max);
		}

		public static Single NextNonNegativeSingle(this IRandom random, Single min, Single max)
		{
			return NextSingle(random, min, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Double NextNonNegativeDouble()
		{
			return NextNonNegativeDouble(Generator);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Double NextNonNegativeDouble(this System.Random random)
		{
			return NextNonNegativeDouble(random, Double.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Double NextNonNegativeDouble(this IRandom random)
		{
			return NextNonNegativeDouble(random, Double.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Double NextNonNegativeDouble(Double max)
		{
			return NextNonNegativeDouble(Generator, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Double NextNonNegativeDouble(this System.Random random, Double max)
		{
			return NextNonNegativeDouble(random, default, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Double NextNonNegativeDouble(this IRandom random, Double max)
		{
			return NextNonNegativeDouble(random, default, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Double NextNonNegativeDouble(Double min, Double max)
		{
			return NextDouble(Generator, min, max);
		}

		public static Double NextNonNegativeDouble(this System.Random random, Double min, Double max)
		{
			return NextDouble(random, min, max);
		}

		public static Double NextNonNegativeDouble(this IRandom random, Double min, Double max)
		{
			return NextDouble(random, min, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Decimal NextNonNegativeDecimal()
		{
			return NextNonNegativeDecimal(Generator);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Decimal NextNonNegativeDecimal(this System.Random random)
		{
			return NextNonNegativeDecimal(random, Decimal.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Decimal NextNonNegativeDecimal(this IRandom random)
		{
			return NextNonNegativeDecimal(random, Decimal.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Decimal NextNonNegativeDecimal(Decimal max)
		{
			return NextNonNegativeDecimal(Generator, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Decimal NextNonNegativeDecimal(this System.Random random, Decimal max)
		{
			return NextNonNegativeDecimal(random, default, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Decimal NextNonNegativeDecimal(this IRandom random, Decimal max)
		{
			return NextNonNegativeDecimal(random, default, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Decimal NextNonNegativeDecimal(Decimal min, Decimal max)
		{
			return NextDecimal(Generator, min, max);
		}

		public static Decimal NextNonNegativeDecimal(this System.Random random, Decimal min, Decimal max)
		{
			return NextDecimal(random, min, max);
		}

		public static Decimal NextNonNegativeDecimal(this IRandom random, Decimal min, Decimal max)
		{
			return NextDecimal(random, min, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static SByte NextNonZeroSByte()
		{
			return NextNonZeroSByte(Generator);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static SByte NextNonZeroSByte(this System.Random random)
		{
			return NextNonZeroSByte(random, SByte.MinValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static SByte NextNonZeroSByte(this IRandom random)
		{
			return NextNonZeroSByte(random, SByte.MinValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static SByte NextNonZeroSByte(SByte max)
		{
			return NextNonZeroSByte(Generator, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static SByte NextNonZeroSByte(this System.Random random, SByte max)
		{
			return NextNonZeroSByte(random, SByte.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static SByte NextNonZeroSByte(this IRandom random, SByte max)
		{
			return NextNonZeroSByte(random, SByte.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static SByte NextNonZeroSByte(SByte min, SByte max)
		{
			return NextNonZeroSByte(Generator, min, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static SByte NextNonZeroSByte(this System.Random random, SByte min, SByte max)
		{
			SByte value;

			do
			{
				value = NextSByte(random, min, max);

			} while (value == 0);

			return value;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static SByte NextNonZeroSByte(this IRandom random, SByte min, SByte max)
		{
			SByte value;

			do
			{
				value = NextSByte(random, min, max);

			} while (value == 0);

			return value;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Byte NextNonZeroByte()
		{
			return NextNonZeroByte(Generator);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Byte NextNonZeroByte(this System.Random random)
		{
			return NextNonZeroByte(random, Byte.MinValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Byte NextNonZeroByte(this IRandom random)
		{
			return NextNonZeroByte(random, Byte.MinValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Byte NextNonZeroByte(Byte max)
		{
			return NextNonZeroByte(Generator, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Byte NextNonZeroByte(this System.Random random, Byte max)
		{
			return NextNonZeroByte(random, Byte.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Byte NextNonZeroByte(this IRandom random, Byte max)
		{
			return NextNonZeroByte(random, Byte.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Byte NextNonZeroByte(Byte min, Byte max)
		{
			return NextNonZeroByte(Generator, min, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Byte NextNonZeroByte(this System.Random random, Byte min, Byte max)
		{
			Byte value;

			do
			{
				value = NextByte(random, min, max);

			} while (value == 0);

			return value;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Byte NextNonZeroByte(this IRandom random, Byte min, Byte max)
		{
			Byte value;

			do
			{
				value = NextByte(random, min, max);

			} while (value == 0);

			return value;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int16 NextNonZeroInt16()
		{
			return NextNonZeroInt16(Generator);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int16 NextNonZeroInt16(this System.Random random)
		{
			return NextNonZeroInt16(random, Int16.MinValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int16 NextNonZeroInt16(this IRandom random)
		{
			return NextNonZeroInt16(random, Int16.MinValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int16 NextNonZeroInt16(Int16 max)
		{
			return NextNonZeroInt16(Generator, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int16 NextNonZeroInt16(this System.Random random, Int16 max)
		{
			return NextNonZeroInt16(random, Int16.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int16 NextNonZeroInt16(this IRandom random, Int16 max)
		{
			return NextNonZeroInt16(random, Int16.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int16 NextNonZeroInt16(Int16 min, Int16 max)
		{
			return NextNonZeroInt16(Generator, min, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int16 NextNonZeroInt16(this System.Random random, Int16 min, Int16 max)
		{
			Int16 value;

			do
			{
				value = NextInt16(random, min, max);

			} while (value == 0);

			return value;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int16 NextNonZeroInt16(this IRandom random, Int16 min, Int16 max)
		{
			Int16 value;

			do
			{
				value = NextInt16(random, min, max);

			} while (value == 0);

			return value;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static UInt16 NextNonZeroUInt16()
		{
			return NextNonZeroUInt16(Generator);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static UInt16 NextNonZeroUInt16(this System.Random random)
		{
			return NextNonZeroUInt16(random, UInt16.MinValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static UInt16 NextNonZeroUInt16(this IRandom random)
		{
			return NextNonZeroUInt16(random, UInt16.MinValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static UInt16 NextNonZeroUInt16(UInt16 max)
		{
			return NextNonZeroUInt16(Generator, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static UInt16 NextNonZeroUInt16(this System.Random random, UInt16 max)
		{
			return NextNonZeroUInt16(random, UInt16.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static UInt16 NextNonZeroUInt16(this IRandom random, UInt16 max)
		{
			return NextNonZeroUInt16(random, UInt16.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static UInt16 NextNonZeroUInt16(UInt16 min, UInt16 max)
		{
			return NextNonZeroUInt16(Generator, min, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static UInt16 NextNonZeroUInt16(this System.Random random, UInt16 min, UInt16 max)
		{
			UInt16 value;

			do
			{
				value = NextUInt16(random, min, max);

			} while (value == 0);

			return value;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static UInt16 NextNonZeroUInt16(this IRandom random, UInt16 min, UInt16 max)
		{
			UInt16 value;

			do
			{
				value = NextUInt16(random, min, max);

			} while (value == 0);

			return value;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int32 NextNonZeroInt32()
		{
			return NextNonZeroInt32(Generator);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int32 NextNonZeroInt32(this System.Random random)
		{
			return NextNonZeroInt32(random, Int32.MinValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int32 NextNonZeroInt32(this IRandom random)
		{
			return NextNonZeroInt32(random, Int32.MinValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int32 NextNonZeroInt32(Int32 max)
		{
			return NextNonZeroInt32(Generator, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int32 NextNonZeroInt32(this System.Random random, Int32 max)
		{
			return NextNonZeroInt32(random, Int32.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int32 NextNonZeroInt32(this IRandom random, Int32 max)
		{
			return NextNonZeroInt32(random, Int32.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int32 NextNonZeroInt32(Int32 min, Int32 max)
		{
			return NextNonZeroInt32(Generator, min, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int32 NextNonZeroInt32(this System.Random random, Int32 min, Int32 max)
		{
			Int32 value;

			do
			{
				value = NextInt32(random, min, max);

			} while (value == 0);

			return value;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int32 NextNonZeroInt32(this IRandom random, Int32 min, Int32 max)
		{
			Int32 value;

			do
			{
				value = NextInt32(random, min, max);

			} while (value == 0);

			return value;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static UInt32 NextNonZeroUInt32()
		{
			return NextNonZeroUInt32(Generator);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static UInt32 NextNonZeroUInt32(this System.Random random)
		{
			return NextNonZeroUInt32(random, UInt32.MinValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static UInt32 NextNonZeroUInt32(this IRandom random)
		{
			return NextNonZeroUInt32(random, UInt32.MinValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static UInt32 NextNonZeroUInt32(UInt32 max)
		{
			return NextNonZeroUInt32(Generator, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static UInt32 NextNonZeroUInt32(this System.Random random, UInt32 max)
		{
			return NextNonZeroUInt32(random, UInt32.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static UInt32 NextNonZeroUInt32(this IRandom random, UInt32 max)
		{
			return NextNonZeroUInt32(random, UInt32.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static UInt32 NextNonZeroUInt32(UInt32 min, UInt32 max)
		{
			return NextNonZeroUInt32(Generator, min, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static UInt32 NextNonZeroUInt32(this System.Random random, UInt32 min, UInt32 max)
		{
			UInt32 value;

			do
			{
				value = NextUInt32(random, min, max);

			} while (value == 0);

			return value;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static UInt32 NextNonZeroUInt32(this IRandom random, UInt32 min, UInt32 max)
		{
			UInt32 value;

			do
			{
				value = NextUInt32(random, min, max);

			} while (value == 0);

			return value;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int64 NextNonZeroInt64()
		{
			return NextNonZeroInt64(Generator);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int64 NextNonZeroInt64(this System.Random random)
		{
			return NextNonZeroInt64(random, Int64.MinValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int64 NextNonZeroInt64(this IRandom random)
		{
			return NextNonZeroInt64(random, Int64.MinValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int64 NextNonZeroInt64(Int64 max)
		{
			return NextNonZeroInt64(Generator, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int64 NextNonZeroInt64(this System.Random random, Int64 max)
		{
			return NextNonZeroInt64(random, Int64.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int64 NextNonZeroInt64(this IRandom random, Int64 max)
		{
			return NextNonZeroInt64(random, Int64.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int64 NextNonZeroInt64(Int64 min, Int64 max)
		{
			return NextNonZeroInt64(Generator, min, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int64 NextNonZeroInt64(this System.Random random, Int64 min, Int64 max)
		{
			Int64 value;

			do
			{
				value = NextInt64(random, min, max);

			} while (value == 0);

			return value;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int64 NextNonZeroInt64(this IRandom random, Int64 min, Int64 max)
		{
			Int64 value;

			do
			{
				value = NextInt64(random, min, max);

			} while (value == 0);

			return value;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static UInt64 NextNonZeroUInt64()
		{
			return NextNonZeroUInt64(Generator);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static UInt64 NextNonZeroUInt64(this System.Random random)
		{
			return NextNonZeroUInt64(random, UInt64.MinValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static UInt64 NextNonZeroUInt64(this IRandom random)
		{
			return NextNonZeroUInt64(random, UInt64.MinValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static UInt64 NextNonZeroUInt64(UInt64 max)
		{
			return NextNonZeroUInt64(Generator, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static UInt64 NextNonZeroUInt64(this System.Random random, UInt64 max)
		{
			return NextNonZeroUInt64(random, UInt64.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static UInt64 NextNonZeroUInt64(this IRandom random, UInt64 max)
		{
			return NextNonZeroUInt64(random, UInt64.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static UInt64 NextNonZeroUInt64(UInt64 min, UInt64 max)
		{
			return NextNonZeroUInt64(Generator, min, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static UInt64 NextNonZeroUInt64(this System.Random random, UInt64 min, UInt64 max)
		{
			UInt64 value;

			do
			{
				value = NextUInt64(random, min, max);

			} while (value == 0);

			return value;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static UInt64 NextNonZeroUInt64(this IRandom random, UInt64 min, UInt64 max)
		{
			UInt64 value;

			do
			{
				value = NextUInt64(random, min, max);

			} while (value == 0);

			return value;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Single NextNonZeroSingle()
		{
			return NextNonZeroSingle(Generator);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Single NextNonZeroSingle(this System.Random random)
		{
			return NextNonZeroSingle(random, Single.MinValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Single NextNonZeroSingle(this IRandom random)
		{
			return NextNonZeroSingle(random, Single.MinValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Single NextNonZeroSingle(Single max)
		{
			return NextNonZeroSingle(Generator, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Single NextNonZeroSingle(this System.Random random, Single max)
		{
			return NextNonZeroSingle(random, Single.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Single NextNonZeroSingle(this IRandom random, Single max)
		{
			return NextNonZeroSingle(random, Single.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Single NextNonZeroSingle(Single min, Single max)
		{
			return NextNonZeroSingle(Generator, min, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Single NextNonZeroSingle(this System.Random random, Single min, Single max)
		{
			Single value;

			do
			{
				value = NextSingle(random, min, max);

			} while (value < Single.Epsilon);

			return value;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Single NextNonZeroSingle(this IRandom random, Single min, Single max)
		{
			Single value;

			do
			{
				value = NextSingle(random, min, max);

			} while (value < Single.Epsilon);

			return value;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Double NextNonZeroDouble()
		{
			return NextNonZeroDouble(Generator);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Double NextNonZeroDouble(this System.Random random)
		{
			return NextNonZeroDouble(random, Double.MinValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Double NextNonZeroDouble(this IRandom random)
		{
			return NextNonZeroDouble(random, Double.MinValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Double NextNonZeroDouble(Double max)
		{
			return NextNonZeroDouble(Generator, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Double NextNonZeroDouble(this System.Random random, Double max)
		{
			return NextNonZeroDouble(random, Double.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Double NextNonZeroDouble(this IRandom random, Double max)
		{
			return NextNonZeroDouble(random, Double.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Double NextNonZeroDouble(Double min, Double max)
		{
			return NextNonZeroDouble(Generator, min, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Double NextNonZeroDouble(this System.Random random, Double min, Double max)
		{
			Double value;

			do
			{
				value = NextDouble(random, min, max);

			} while (value < Double.Epsilon);

			return value;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Double NextNonZeroDouble(this IRandom random, Double min, Double max)
		{
			Double value;

			do
			{
				value = NextDouble(random, min, max);

			} while (value < Double.Epsilon);

			return value;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Decimal NextNonZeroDecimal()
		{
			return NextNonZeroDecimal(Generator);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Decimal NextNonZeroDecimal(this System.Random random)
		{
			return NextNonZeroDecimal(random, Decimal.MinValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Decimal NextNonZeroDecimal(this IRandom random)
		{
			return NextNonZeroDecimal(random, Decimal.MinValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Decimal NextNonZeroDecimal(Decimal max)
		{
			return NextNonZeroDecimal(Generator, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Decimal NextNonZeroDecimal(this System.Random random, Decimal max)
		{
			return NextNonZeroDecimal(random, Decimal.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Decimal NextNonZeroDecimal(this IRandom random, Decimal max)
		{
			return NextNonZeroDecimal(random, Decimal.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Decimal NextNonZeroDecimal(Decimal min, Decimal max)
		{
			return NextNonZeroDecimal(Generator, min, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Decimal NextNonZeroDecimal(this System.Random random, Decimal min, Decimal max)
		{
			Decimal value;

			do
			{
				value = NextDecimal(random, min, max);

			} while (value == 0);

			return value;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Decimal NextNonZeroDecimal(this IRandom random, Decimal min, Decimal max)
		{
			Decimal value;

			do
			{
				value = NextDecimal(random, min, max);

			} while (value == 0);

			return value;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(Span<SByte> span)
		{
			Next(Generator, span);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(this System.Random random, Span<SByte> span)
		{
			Next(random, span, SByte.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(this IRandom random, Span<SByte> span)
		{
			Next(random, span, SByte.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(Span<SByte> span, SByte max)
		{
			Next(Generator, span, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(this System.Random random, Span<SByte> span, SByte max)
		{
			Next(random, span, SByte.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(this IRandom random, Span<SByte> span, SByte max)
		{
			Next(random, span, SByte.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(Span<SByte> span, SByte min, SByte max)
		{
			Next(Generator, span, min, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(this System.Random random, Span<SByte> span, SByte min, SByte max)
		{
			for (Int32 i = 0; i < span.Length; i++)
			{
				span[i] = NextSByte(random, min, max);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(this IRandom random, Span<SByte> span, SByte min, SByte max)
		{
			for (Int32 i = 0; i < span.Length; i++)
			{
				span[i] = NextSByte(random, min, max);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(Span<Byte> span)
		{
			Next(Generator, span);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(this System.Random random, Span<Byte> span)
		{
			Next(random, span, Byte.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(this IRandom random, Span<Byte> span)
		{
			Next(random, span, Byte.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(Span<Byte> span, Byte max)
		{
			Next(Generator, span, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(this System.Random random, Span<Byte> span, Byte max)
		{
			Next(random, span, Byte.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(this IRandom random, Span<Byte> span, Byte max)
		{
			Next(random, span, Byte.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(Span<Byte> span, Byte min, Byte max)
		{
			Next(Generator, span, min, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(this System.Random random, Span<Byte> span, Byte min, Byte max)
		{
			for (Int32 i = 0; i < span.Length; i++)
			{
				span[i] = NextByte(random, min, max);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(this IRandom random, Span<Byte> span, Byte min, Byte max)
		{
			for (Int32 i = 0; i < span.Length; i++)
			{
				span[i] = NextByte(random, min, max);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(Span<Int16> span)
		{
			Next(Generator, span);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(this System.Random random, Span<Int16> span)
		{
			Next(random, span, Int16.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(this IRandom random, Span<Int16> span)
		{
			Next(random, span, Int16.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(Span<Int16> span, Int16 max)
		{
			Next(Generator, span, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(this System.Random random, Span<Int16> span, Int16 max)
		{
			Next(random, span, Int16.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(this IRandom random, Span<Int16> span, Int16 max)
		{
			Next(random, span, Int16.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(Span<Int16> span, Int16 min, Int16 max)
		{
			Next(Generator, span, min, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(this System.Random random, Span<Int16> span, Int16 min, Int16 max)
		{
			for (Int32 i = 0; i < span.Length; i++)
			{
				span[i] = NextInt16(random, min, max);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(this IRandom random, Span<Int16> span, Int16 min, Int16 max)
		{
			for (Int32 i = 0; i < span.Length; i++)
			{
				span[i] = NextInt16(random, min, max);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(Span<UInt16> span)
		{
			Next(Generator, span);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(this System.Random random, Span<UInt16> span)
		{
			Next(random, span, UInt16.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(this IRandom random, Span<UInt16> span)
		{
			Next(random, span, UInt16.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(Span<UInt16> span, UInt16 max)
		{
			Next(Generator, span, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(this System.Random random, Span<UInt16> span, UInt16 max)
		{
			Next(random, span, UInt16.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(this IRandom random, Span<UInt16> span, UInt16 max)
		{
			Next(random, span, UInt16.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(Span<UInt16> span, UInt16 min, UInt16 max)
		{
			Next(Generator, span, min, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(this System.Random random, Span<UInt16> span, UInt16 min, UInt16 max)
		{
			for (Int32 i = 0; i < span.Length; i++)
			{
				span[i] = NextUInt16(random, min, max);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(this IRandom random, Span<UInt16> span, UInt16 min, UInt16 max)
		{
			for (Int32 i = 0; i < span.Length; i++)
			{
				span[i] = NextUInt16(random, min, max);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(Span<Int32> span)
		{
			Next(Generator, span);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(this System.Random random, Span<Int32> span)
		{
			Next(random, span, Int32.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(this IRandom random, Span<Int32> span)
		{
			Next(random, span, Int32.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(Span<Int32> span, Int32 max)
		{
			Next(Generator, span, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(this System.Random random, Span<Int32> span, Int32 max)
		{
			Next(random, span, Int32.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(this IRandom random, Span<Int32> span, Int32 max)
		{
			Next(random, span, Int32.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(Span<Int32> span, Int32 min, Int32 max)
		{
			Next(Generator, span, min, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(this System.Random random, Span<Int32> span, Int32 min, Int32 max)
		{
			for (Int32 i = 0; i < span.Length; i++)
			{
				span[i] = NextInt32(random, min, max);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(this IRandom random, Span<Int32> span, Int32 min, Int32 max)
		{
			for (Int32 i = 0; i < span.Length; i++)
			{
				span[i] = NextInt32(random, min, max);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(Span<UInt32> span)
		{
			Next(Generator, span);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(this System.Random random, Span<UInt32> span)
		{
			Next(random, span, UInt32.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(this IRandom random, Span<UInt32> span)
		{
			Next(random, span, UInt32.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(Span<UInt32> span, UInt32 max)
		{
			Next(Generator, span, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(this System.Random random, Span<UInt32> span, UInt32 max)
		{
			Next(random, span, UInt32.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(this IRandom random, Span<UInt32> span, UInt32 max)
		{
			Next(random, span, UInt32.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(Span<UInt32> span, UInt32 min, UInt32 max)
		{
			Next(Generator, span, min, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(this System.Random random, Span<UInt32> span, UInt32 min, UInt32 max)
		{
			for (Int32 i = 0; i < span.Length; i++)
			{
				span[i] = NextUInt32(random, min, max);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(this IRandom random, Span<UInt32> span, UInt32 min, UInt32 max)
		{
			for (Int32 i = 0; i < span.Length; i++)
			{
				span[i] = NextUInt32(random, min, max);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(Span<Int64> span)
		{
			Next(Generator, span);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(this System.Random random, Span<Int64> span)
		{
			Next(random, span, Int64.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(this IRandom random, Span<Int64> span)
		{
			Next(random, span, Int64.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(Span<Int64> span, Int64 max)
		{
			Next(Generator, span, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(this System.Random random, Span<Int64> span, Int64 max)
		{
			Next(random, span, Int64.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(this IRandom random, Span<Int64> span, Int64 max)
		{
			Next(random, span, Int64.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(Span<Int64> span, Int64 min, Int64 max)
		{
			Next(Generator, span, min, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(this System.Random random, Span<Int64> span, Int64 min, Int64 max)
		{
			for (Int32 i = 0; i < span.Length; i++)
			{
				span[i] = NextInt64(random, min, max);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(this IRandom random, Span<Int64> span, Int64 min, Int64 max)
		{
			for (Int32 i = 0; i < span.Length; i++)
			{
				span[i] = NextInt64(random, min, max);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(Span<UInt64> span)
		{
			Next(Generator, span);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(this System.Random random, Span<UInt64> span)
		{
			Next(random, span, UInt64.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(this IRandom random, Span<UInt64> span)
		{
			Next(random, span, UInt64.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(Span<UInt64> span, UInt64 max)
		{
			Next(Generator, span, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(this System.Random random, Span<UInt64> span, UInt64 max)
		{
			Next(random, span, UInt64.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(this IRandom random, Span<UInt64> span, UInt64 max)
		{
			Next(random, span, UInt64.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(Span<UInt64> span, UInt64 min, UInt64 max)
		{
			Next(Generator, span, min, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(this System.Random random, Span<UInt64> span, UInt64 min, UInt64 max)
		{
			for (Int32 i = 0; i < span.Length; i++)
			{
				span[i] = NextUInt64(random, min, max);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(this IRandom random, Span<UInt64> span, UInt64 min, UInt64 max)
		{
			for (Int32 i = 0; i < span.Length; i++)
			{
				span[i] = NextUInt64(random, min, max);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(Span<Single> span)
		{
			Next(Generator, span);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(this System.Random random, Span<Single> span)
		{
			Next(random, span, Single.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(this IRandom random, Span<Single> span)
		{
			Next(random, span, Single.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(Span<Single> span, Single max)
		{
			Next(Generator, span, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(this System.Random random, Span<Single> span, Single max)
		{
			Next(random, span, Single.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(this IRandom random, Span<Single> span, Single max)
		{
			Next(random, span, Single.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(Span<Single> span, Single min, Single max)
		{
			Next(Generator, span, min, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(this System.Random random, Span<Single> span, Single min, Single max)
		{
			for (Int32 i = 0; i < span.Length; i++)
			{
				span[i] = NextSingle(random, min, max);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(this IRandom random, Span<Single> span, Single min, Single max)
		{
			for (Int32 i = 0; i < span.Length; i++)
			{
				span[i] = NextSingle(random, min, max);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(Span<Double> span)
		{
			Next(Generator, span);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(this System.Random random, Span<Double> span)
		{
			Next(random, span, Double.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(this IRandom random, Span<Double> span)
		{
			Next(random, span, Double.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(Span<Double> span, Double max)
		{
			Next(Generator, span, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(this System.Random random, Span<Double> span, Double max)
		{
			Next(random, span, Double.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(this IRandom random, Span<Double> span, Double max)
		{
			Next(random, span, Double.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(Span<Double> span, Double min, Double max)
		{
			Next(Generator, span, min, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(this System.Random random, Span<Double> span, Double min, Double max)
		{
			for (Int32 i = 0; i < span.Length; i++)
			{
				span[i] = NextDouble(random, min, max);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(this IRandom random, Span<Double> span, Double min, Double max)
		{
			for (Int32 i = 0; i < span.Length; i++)
			{
				span[i] = NextDouble(random, min, max);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(Span<Decimal> span)
		{
			Next(Generator, span);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(this System.Random random, Span<Decimal> span)
		{
			Next(random, span, Decimal.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(this IRandom random, Span<Decimal> span)
		{
			Next(random, span, Decimal.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(Span<Decimal> span, Decimal max)
		{
			Next(Generator, span, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(this System.Random random, Span<Decimal> span, Decimal max)
		{
			Next(random, span, Decimal.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(this IRandom random, Span<Decimal> span, Decimal max)
		{
			Next(random, span, Decimal.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(Span<Decimal> span, Decimal min, Decimal max)
		{
			Next(Generator, span, min, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(this System.Random random, Span<Decimal> span, Decimal min, Decimal max)
		{
			for (Int32 i = 0; i < span.Length; i++)
			{
				span[i] = NextDecimal(random, min, max);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Next(this IRandom random, Span<Decimal> span, Decimal min, Decimal max)
		{
			for (Int32 i = 0; i < span.Length; i++)
			{
				span[i] = NextDecimal(random, min, max);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(Span<SByte> span)
		{
			NextNonNegative(Generator, span);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(this System.Random random, Span<SByte> span)
		{
			NextNonNegative(random, span, SByte.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(this IRandom random, Span<SByte> span)
		{
			NextNonNegative(random, span, SByte.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(Span<SByte> span, SByte max)
		{
			NextNonNegative(Generator, span, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(this System.Random random, Span<SByte> span, SByte max)
		{
			NextNonNegative(random, span, default, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(this IRandom random, Span<SByte> span, SByte max)
		{
			NextNonNegative(random, span, default, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(Span<SByte> span, SByte min, SByte max)
		{
			Next(Generator, span, min, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(this System.Random random, Span<SByte> span, SByte min, SByte max)
		{
			Next(random, span, min, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(this IRandom random, Span<SByte> span, SByte min, SByte max)
		{
			Next(random, span, min, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(Span<Byte> span)
		{
			NextNonNegative(Generator, span);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(this System.Random random, Span<Byte> span)
		{
			NextNonNegative(random, span, Byte.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(this IRandom random, Span<Byte> span)
		{
			NextNonNegative(random, span, Byte.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(Span<Byte> span, Byte max)
		{
			NextNonNegative(Generator, span, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(this System.Random random, Span<Byte> span, Byte max)
		{
			NextNonNegative(random, span, default, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(this IRandom random, Span<Byte> span, Byte max)
		{
			NextNonNegative(random, span, default, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(Span<Byte> span, Byte min, Byte max)
		{
			Next(Generator, span, min, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(this System.Random random, Span<Byte> span, Byte min, Byte max)
		{
			Next(random, span, min, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(this IRandom random, Span<Byte> span, Byte min, Byte max)
		{
			Next(random, span, min, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(Span<Int16> span)
		{
			NextNonNegative(Generator, span);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(this System.Random random, Span<Int16> span)
		{
			NextNonNegative(random, span, Int16.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(this IRandom random, Span<Int16> span)
		{
			NextNonNegative(random, span, Int16.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(Span<Int16> span, Int16 max)
		{
			NextNonNegative(Generator, span, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(this System.Random random, Span<Int16> span, Int16 max)
		{
			NextNonNegative(random, span, default, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(this IRandom random, Span<Int16> span, Int16 max)
		{
			NextNonNegative(random, span, default, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(Span<Int16> span, Int16 min, Int16 max)
		{
			Next(Generator, span, min, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(this System.Random random, Span<Int16> span, Int16 min, Int16 max)
		{
			Next(random, span, min, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(this IRandom random, Span<Int16> span, Int16 min, Int16 max)
		{
			Next(random, span, min, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(Span<UInt16> span)
		{
			NextNonNegative(Generator, span);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(this System.Random random, Span<UInt16> span)
		{
			NextNonNegative(random, span, UInt16.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(this IRandom random, Span<UInt16> span)
		{
			NextNonNegative(random, span, UInt16.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(Span<UInt16> span, UInt16 max)
		{
			NextNonNegative(Generator, span, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(this System.Random random, Span<UInt16> span, UInt16 max)
		{
			NextNonNegative(random, span, default, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(this IRandom random, Span<UInt16> span, UInt16 max)
		{
			NextNonNegative(random, span, default, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(Span<UInt16> span, UInt16 min, UInt16 max)
		{
			Next(Generator, span, min, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(this System.Random random, Span<UInt16> span, UInt16 min, UInt16 max)
		{
			Next(random, span, min, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(this IRandom random, Span<UInt16> span, UInt16 min, UInt16 max)
		{
			Next(random, span, min, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(Span<Int32> span)
		{
			NextNonNegative(Generator, span);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(this System.Random random, Span<Int32> span)
		{
			NextNonNegative(random, span, Int32.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(this IRandom random, Span<Int32> span)
		{
			NextNonNegative(random, span, Int32.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(Span<Int32> span, Int32 max)
		{
			NextNonNegative(Generator, span, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(this System.Random random, Span<Int32> span, Int32 max)
		{
			NextNonNegative(random, span, default, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(this IRandom random, Span<Int32> span, Int32 max)
		{
			NextNonNegative(random, span, default, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(Span<Int32> span, Int32 min, Int32 max)
		{
			Next(Generator, span, min, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(this System.Random random, Span<Int32> span, Int32 min, Int32 max)
		{
			Next(random, span, min, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(this IRandom random, Span<Int32> span, Int32 min, Int32 max)
		{
			Next(random, span, min, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(Span<UInt32> span)
		{
			NextNonNegative(Generator, span);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(this System.Random random, Span<UInt32> span)
		{
			NextNonNegative(random, span, UInt32.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(this IRandom random, Span<UInt32> span)
		{
			NextNonNegative(random, span, UInt32.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(Span<UInt32> span, UInt32 max)
		{
			NextNonNegative(Generator, span, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(this System.Random random, Span<UInt32> span, UInt32 max)
		{
			NextNonNegative(random, span, default, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(this IRandom random, Span<UInt32> span, UInt32 max)
		{
			NextNonNegative(random, span, default, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(Span<UInt32> span, UInt32 min, UInt32 max)
		{
			Next(Generator, span, min, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(this System.Random random, Span<UInt32> span, UInt32 min, UInt32 max)
		{
			Next(random, span, min, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(this IRandom random, Span<UInt32> span, UInt32 min, UInt32 max)
		{
			Next(random, span, min, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(Span<Int64> span)
		{
			NextNonNegative(Generator, span);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(this System.Random random, Span<Int64> span)
		{
			NextNonNegative(random, span, Int64.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(this IRandom random, Span<Int64> span)
		{
			NextNonNegative(random, span, Int64.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(Span<Int64> span, Int64 max)
		{
			NextNonNegative(Generator, span, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(this System.Random random, Span<Int64> span, Int64 max)
		{
			NextNonNegative(random, span, default, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(this IRandom random, Span<Int64> span, Int64 max)
		{
			NextNonNegative(random, span, default, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(Span<Int64> span, Int64 min, Int64 max)
		{
			Next(Generator, span, min, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(this System.Random random, Span<Int64> span, Int64 min, Int64 max)
		{
			Next(random, span, min, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(this IRandom random, Span<Int64> span, Int64 min, Int64 max)
		{
			Next(random, span, min, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(Span<UInt64> span)
		{
			NextNonNegative(Generator, span);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(this System.Random random, Span<UInt64> span)
		{
			NextNonNegative(random, span, UInt64.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(this IRandom random, Span<UInt64> span)
		{
			NextNonNegative(random, span, UInt64.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(Span<UInt64> span, UInt64 max)
		{
			NextNonNegative(Generator, span, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(this System.Random random, Span<UInt64> span, UInt64 max)
		{
			NextNonNegative(random, span, default, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(this IRandom random, Span<UInt64> span, UInt64 max)
		{
			NextNonNegative(random, span, default, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(Span<UInt64> span, UInt64 min, UInt64 max)
		{
			Next(Generator, span, min, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(this System.Random random, Span<UInt64> span, UInt64 min, UInt64 max)
		{
			Next(random, span, min, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(this IRandom random, Span<UInt64> span, UInt64 min, UInt64 max)
		{
			Next(random, span, min, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(Span<Single> span)
		{
			NextNonNegative(Generator, span);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(this System.Random random, Span<Single> span)
		{
			NextNonNegative(random, span, Single.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(this IRandom random, Span<Single> span)
		{
			NextNonNegative(random, span, Single.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(Span<Single> span, Single max)
		{
			NextNonNegative(Generator, span, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(this System.Random random, Span<Single> span, Single max)
		{
			NextNonNegative(random, span, default, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(this IRandom random, Span<Single> span, Single max)
		{
			NextNonNegative(random, span, default, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(Span<Single> span, Single min, Single max)
		{
			Next(Generator, span, min, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(this System.Random random, Span<Single> span, Single min, Single max)
		{
			Next(random, span, min, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(this IRandom random, Span<Single> span, Single min, Single max)
		{
			Next(random, span, min, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(Span<Double> span)
		{
			NextNonNegative(Generator, span);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(this System.Random random, Span<Double> span)
		{
			NextNonNegative(random, span, Double.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(this IRandom random, Span<Double> span)
		{
			NextNonNegative(random, span, Double.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(Span<Double> span, Double max)
		{
			NextNonNegative(Generator, span, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(this System.Random random, Span<Double> span, Double max)
		{
			NextNonNegative(random, span, default, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(this IRandom random, Span<Double> span, Double max)
		{
			NextNonNegative(random, span, default, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(Span<Double> span, Double min, Double max)
		{
			Next(Generator, span, min, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(this System.Random random, Span<Double> span, Double min, Double max)
		{
			Next(random, span, min, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(this IRandom random, Span<Double> span, Double min, Double max)
		{
			Next(random, span, min, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(Span<Decimal> span)
		{
			NextNonNegative(Generator, span);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(this System.Random random, Span<Decimal> span)
		{
			NextNonNegative(random, span, Decimal.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(this IRandom random, Span<Decimal> span)
		{
			NextNonNegative(random, span, Decimal.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(Span<Decimal> span, Decimal max)
		{
			NextNonNegative(Generator, span, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(this System.Random random, Span<Decimal> span, Decimal max)
		{
			NextNonNegative(random, span, default, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(this IRandom random, Span<Decimal> span, Decimal max)
		{
			NextNonNegative(random, span, default, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(Span<Decimal> span, Decimal min, Decimal max)
		{
			Next(Generator, span, min, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(this System.Random random, Span<Decimal> span, Decimal min, Decimal max)
		{
			Next(random, span, min, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonNegative(this IRandom random, Span<Decimal> span, Decimal min, Decimal max)
		{
			Next(random, span, min, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(Span<SByte> span)
		{
			NextNonZero(Generator, span);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(this System.Random random, Span<SByte> span)
		{
			NextNonZero(random, span, SByte.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(this IRandom random, Span<SByte> span)
		{
			NextNonZero(random, span, SByte.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(Span<SByte> span, SByte max)
		{
			NextNonZero(Generator, span, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(this System.Random random, Span<SByte> span, SByte max)
		{
			NextNonZero(random, span, SByte.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(this IRandom random, Span<SByte> span, SByte max)
		{
			NextNonZero(random, span, SByte.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(Span<SByte> span, SByte min, SByte max)
		{
			NextNonZero(Generator, span, min, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(this System.Random random, Span<SByte> span, SByte min, SByte max)
		{
			for (Int32 i = 0; i < span.Length; i++)
			{
				span[i] = NextNonZeroSByte(random, min, max);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(this IRandom random, Span<SByte> span, SByte min, SByte max)
		{
			for (Int32 i = 0; i < span.Length; i++)
			{
				span[i] = NextNonZeroSByte(random, min, max);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(Span<Byte> span)
		{
			NextNonZero(Generator, span);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(this System.Random random, Span<Byte> span)
		{
			NextNonZero(random, span, Byte.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(this IRandom random, Span<Byte> span)
		{
			NextNonZero(random, span, Byte.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(Span<Byte> span, Byte max)
		{
			NextNonZero(Generator, span, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(this System.Random random, Span<Byte> span, Byte max)
		{
			NextNonZero(random, span, Byte.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(this IRandom random, Span<Byte> span, Byte max)
		{
			NextNonZero(random, span, Byte.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(Span<Byte> span, Byte min, Byte max)
		{
			NextNonZero(Generator, span, min, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(this System.Random random, Span<Byte> span, Byte min, Byte max)
		{
			for (Int32 i = 0; i < span.Length; i++)
			{
				span[i] = NextNonZeroByte(random, min, max);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(this IRandom random, Span<Byte> span, Byte min, Byte max)
		{
			for (Int32 i = 0; i < span.Length; i++)
			{
				span[i] = NextNonZeroByte(random, min, max);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(Span<Int16> span)
		{
			NextNonZero(Generator, span);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(this System.Random random, Span<Int16> span)
		{
			NextNonZero(random, span, Int16.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(this IRandom random, Span<Int16> span)
		{
			NextNonZero(random, span, Int16.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(Span<Int16> span, Int16 max)
		{
			NextNonZero(Generator, span, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(this System.Random random, Span<Int16> span, Int16 max)
		{
			NextNonZero(random, span, Int16.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(this IRandom random, Span<Int16> span, Int16 max)
		{
			NextNonZero(random, span, Int16.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(Span<Int16> span, Int16 min, Int16 max)
		{
			NextNonZero(Generator, span, min, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(this System.Random random, Span<Int16> span, Int16 min, Int16 max)
		{
			for (Int32 i = 0; i < span.Length; i++)
			{
				span[i] = NextNonZeroInt16(random, min, max);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(this IRandom random, Span<Int16> span, Int16 min, Int16 max)
		{
			for (Int32 i = 0; i < span.Length; i++)
			{
				span[i] = NextNonZeroInt16(random, min, max);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(Span<UInt16> span)
		{
			NextNonZero(Generator, span);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(this System.Random random, Span<UInt16> span)
		{
			NextNonZero(random, span, UInt16.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(this IRandom random, Span<UInt16> span)
		{
			NextNonZero(random, span, UInt16.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(Span<UInt16> span, UInt16 max)
		{
			NextNonZero(Generator, span, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(this System.Random random, Span<UInt16> span, UInt16 max)
		{
			NextNonZero(random, span, UInt16.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(this IRandom random, Span<UInt16> span, UInt16 max)
		{
			NextNonZero(random, span, UInt16.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(Span<UInt16> span, UInt16 min, UInt16 max)
		{
			NextNonZero(Generator, span, min, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(this System.Random random, Span<UInt16> span, UInt16 min, UInt16 max)
		{
			for (Int32 i = 0; i < span.Length; i++)
			{
				span[i] = NextNonZeroUInt16(random, min, max);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(this IRandom random, Span<UInt16> span, UInt16 min, UInt16 max)
		{
			for (Int32 i = 0; i < span.Length; i++)
			{
				span[i] = NextNonZeroUInt16(random, min, max);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(Span<Int32> span)
		{
			NextNonZero(Generator, span);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(this System.Random random, Span<Int32> span)
		{
			NextNonZero(random, span, Int32.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(this IRandom random, Span<Int32> span)
		{
			NextNonZero(random, span, Int32.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(Span<Int32> span, Int32 max)
		{
			NextNonZero(Generator, span, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(this System.Random random, Span<Int32> span, Int32 max)
		{
			NextNonZero(random, span, Int32.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(this IRandom random, Span<Int32> span, Int32 max)
		{
			NextNonZero(random, span, Int32.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(Span<Int32> span, Int32 min, Int32 max)
		{
			NextNonZero(Generator, span, min, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(this System.Random random, Span<Int32> span, Int32 min, Int32 max)
		{
			for (Int32 i = 0; i < span.Length; i++)
			{
				span[i] = NextNonZeroInt32(random, min, max);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(this IRandom random, Span<Int32> span, Int32 min, Int32 max)
		{
			for (Int32 i = 0; i < span.Length; i++)
			{
				span[i] = NextNonZeroInt32(random, min, max);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(Span<UInt32> span)
		{
			NextNonZero(Generator, span);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(this System.Random random, Span<UInt32> span)
		{
			NextNonZero(random, span, UInt32.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(this IRandom random, Span<UInt32> span)
		{
			NextNonZero(random, span, UInt32.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(Span<UInt32> span, UInt32 max)
		{
			NextNonZero(Generator, span, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(this System.Random random, Span<UInt32> span, UInt32 max)
		{
			NextNonZero(random, span, UInt32.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(this IRandom random, Span<UInt32> span, UInt32 max)
		{
			NextNonZero(random, span, UInt32.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(Span<UInt32> span, UInt32 min, UInt32 max)
		{
			NextNonZero(Generator, span, min, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(this System.Random random, Span<UInt32> span, UInt32 min, UInt32 max)
		{
			for (Int32 i = 0; i < span.Length; i++)
			{
				span[i] = NextNonZeroUInt32(random, min, max);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(this IRandom random, Span<UInt32> span, UInt32 min, UInt32 max)
		{
			for (Int32 i = 0; i < span.Length; i++)
			{
				span[i] = NextNonZeroUInt32(random, min, max);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(Span<Int64> span)
		{
			NextNonZero(Generator, span);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(this System.Random random, Span<Int64> span)
		{
			NextNonZero(random, span, Int64.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(this IRandom random, Span<Int64> span)
		{
			NextNonZero(random, span, Int64.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(Span<Int64> span, Int64 max)
		{
			NextNonZero(Generator, span, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(this System.Random random, Span<Int64> span, Int64 max)
		{
			NextNonZero(random, span, Int64.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(this IRandom random, Span<Int64> span, Int64 max)
		{
			NextNonZero(random, span, Int64.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(Span<Int64> span, Int64 min, Int64 max)
		{
			NextNonZero(Generator, span, min, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(this System.Random random, Span<Int64> span, Int64 min, Int64 max)
		{
			for (Int32 i = 0; i < span.Length; i++)
			{
				span[i] = NextNonZeroInt64(random, min, max);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(this IRandom random, Span<Int64> span, Int64 min, Int64 max)
		{
			for (Int32 i = 0; i < span.Length; i++)
			{
				span[i] = NextNonZeroInt64(random, min, max);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(Span<UInt64> span)
		{
			NextNonZero(Generator, span);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(this System.Random random, Span<UInt64> span)
		{
			NextNonZero(random, span, UInt64.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(this IRandom random, Span<UInt64> span)
		{
			NextNonZero(random, span, UInt64.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(Span<UInt64> span, UInt64 max)
		{
			NextNonZero(Generator, span, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(this System.Random random, Span<UInt64> span, UInt64 max)
		{
			NextNonZero(random, span, UInt64.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(this IRandom random, Span<UInt64> span, UInt64 max)
		{
			NextNonZero(random, span, UInt64.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(Span<UInt64> span, UInt64 min, UInt64 max)
		{
			NextNonZero(Generator, span, min, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(this System.Random random, Span<UInt64> span, UInt64 min, UInt64 max)
		{
			for (Int32 i = 0; i < span.Length; i++)
			{
				span[i] = NextNonZeroUInt64(random, min, max);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(this IRandom random, Span<UInt64> span, UInt64 min, UInt64 max)
		{
			for (Int32 i = 0; i < span.Length; i++)
			{
				span[i] = NextNonZeroUInt64(random, min, max);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(Span<Single> span)
		{
			NextNonZero(Generator, span);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(this System.Random random, Span<Single> span)
		{
			NextNonZero(random, span, Single.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(this IRandom random, Span<Single> span)
		{
			NextNonZero(random, span, Single.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(Span<Single> span, Single max)
		{
			NextNonZero(Generator, span, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(this System.Random random, Span<Single> span, Single max)
		{
			NextNonZero(random, span, Single.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(this IRandom random, Span<Single> span, Single max)
		{
			NextNonZero(random, span, Single.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(Span<Single> span, Single min, Single max)
		{
			NextNonZero(Generator, span, min, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(this System.Random random, Span<Single> span, Single min, Single max)
		{
			for (Int32 i = 0; i < span.Length; i++)
			{
				span[i] = NextNonZeroSingle(random, min, max);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(this IRandom random, Span<Single> span, Single min, Single max)
		{
			for (Int32 i = 0; i < span.Length; i++)
			{
				span[i] = NextNonZeroSingle(random, min, max);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(Span<Double> span)
		{
			NextNonZero(Generator, span);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(this System.Random random, Span<Double> span)
		{
			NextNonZero(random, span, Double.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(this IRandom random, Span<Double> span)
		{
			NextNonZero(random, span, Double.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(Span<Double> span, Double max)
		{
			NextNonZero(Generator, span, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(this System.Random random, Span<Double> span, Double max)
		{
			NextNonZero(random, span, Double.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(this IRandom random, Span<Double> span, Double max)
		{
			NextNonZero(random, span, Double.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(Span<Double> span, Double min, Double max)
		{
			NextNonZero(Generator, span, min, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(this System.Random random, Span<Double> span, Double min, Double max)
		{
			for (Int32 i = 0; i < span.Length; i++)
			{
				span[i] = NextNonZeroDouble(random, min, max);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(this IRandom random, Span<Double> span, Double min, Double max)
		{
			for (Int32 i = 0; i < span.Length; i++)
			{
				span[i] = NextNonZeroDouble(random, min, max);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(Span<Decimal> span)
		{
			NextNonZero(Generator, span);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(this System.Random random, Span<Decimal> span)
		{
			NextNonZero(random, span, Decimal.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(this IRandom random, Span<Decimal> span)
		{
			NextNonZero(random, span, Decimal.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(Span<Decimal> span, Decimal max)
		{
			NextNonZero(Generator, span, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(this System.Random random, Span<Decimal> span, Decimal max)
		{
			NextNonZero(random, span, Decimal.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(this IRandom random, Span<Decimal> span, Decimal max)
		{
			NextNonZero(random, span, Decimal.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(Span<Decimal> span, Decimal min, Decimal max)
		{
			NextNonZero(Generator, span, min, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(this System.Random random, Span<Decimal> span, Decimal min, Decimal max)
		{
			for (Int32 i = 0; i < span.Length; i++)
			{
				span[i] = NextNonZeroDecimal(random, min, max);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void NextNonZero(this IRandom random, Span<Decimal> span, Decimal min, Decimal max)
		{
			for (Int32 i = 0; i < span.Length; i++)
			{
				span[i] = NextNonZeroDecimal(random, min, max);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static SByte NextSignSByte(Double chance = 0.5)
		{
			return NextSignSByte(Generator, chance);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static SByte NextSignSByte(this System.Random random, Double chance = 0.5)
		{
			return NextBoolean(random, chance) ? 1 : -1;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static SByte NextSignSByte(this IRandom random, Double chance = 0.5)
		{
			return NextBoolean(random, chance) ? 1 : -1;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int16 NextSignInt16(Double chance = 0.5)
		{
			return NextSignInt16(Generator, chance);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int16 NextSignInt16(this System.Random random, Double chance = 0.5)
		{
			return NextBoolean(random, chance) ? 1 : -1;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int16 NextSignInt16(this IRandom random, Double chance = 0.5)
		{
			return NextBoolean(random, chance) ? 1 : -1;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int32 NextSignInt32(Double chance = 0.5)
		{
			return NextSignInt32(Generator, chance);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int32 NextSignInt32(this System.Random random, Double chance = 0.5)
		{
			return NextBoolean(random, chance) ? 1 : -1;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int32 NextSignInt32(this IRandom random, Double chance = 0.5)
		{
			return NextBoolean(random, chance) ? 1 : -1;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int64 NextSignInt64(Double chance = 0.5)
		{
			return NextSignInt64(Generator, chance);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int64 NextSignInt64(this System.Random random, Double chance = 0.5)
		{
			return NextBoolean(random, chance) ? 1 : -1;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int64 NextSignInt64(this IRandom random, Double chance = 0.5)
		{
			return NextBoolean(random, chance) ? 1 : -1;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Single NextSignSingle(Double chance = 0.5)
		{
			return NextSignSingle(Generator, chance);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Single NextSignSingle(this System.Random random, Double chance = 0.5)
		{
			return NextBoolean(random, chance) ? 1 : -1;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Single NextSignSingle(this IRandom random, Double chance = 0.5)
		{
			return NextBoolean(random, chance) ? 1 : -1;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Double NextSignDouble(Double chance = 0.5)
		{
			return NextSignDouble(Generator, chance);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Double NextSignDouble(this System.Random random, Double chance = 0.5)
		{
			return NextBoolean(random, chance) ? 1 : -1;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Double NextSignDouble(this IRandom random, Double chance = 0.5)
		{
			return NextBoolean(random, chance) ? 1 : -1;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Decimal NextSignDecimal(Double chance = 0.5)
		{
			return NextSignDecimal(Generator, chance);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Decimal NextSignDecimal(this System.Random random, Double chance = 0.5)
		{
			return NextBoolean(random, chance) ? 1 : -1;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Decimal NextSignDecimal(this IRandom random, Double chance = 0.5)
		{
			return NextBoolean(random, chance) ? 1 : -1;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IEnumerable<T> Range<T>(T min, T max, Func<T, T, T> generator)
		{
			if (generator is null)
			{
				throw new ArgumentNullException(nameof(generator));
			}

			while (true)
			{
				yield return generator(min, max);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IEnumerable<T> Range<T>(System.Random random, T min, T max, Func<System.Random, T, T, T> generator)
		{
			if (generator is null)
			{
				throw new ArgumentNullException(nameof(generator));
			}

			while (true)
			{
				yield return generator(random, min, max);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IEnumerable<T> Range<T>(IRandom random, T min, T max, Func<IRandom, T, T, T> generator)
		{
			if (generator is null)
			{
				throw new ArgumentNullException(nameof(generator));
			}

			while (true)
			{
				yield return generator(random, min, max);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IEnumerable<T> Range<T>(T min, T max, Int32 count, Func<T, T, T> generator)
		{
			if (generator is null)
			{
				throw new ArgumentNullException(nameof(generator));
			}

			return count <= 0 ? Enumerable.Empty<T>() : Range(min, max, generator).Take(count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IEnumerable<T> Range<T>(System.Random random, T min, T max, Int32 count, Func<System.Random, T, T, T> generator)
		{
			if (generator is null)
			{
				throw new ArgumentNullException(nameof(generator));
			}

			return count <= 0 ? Enumerable.Empty<T>() : Range(random, min, max, generator).Take(count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IEnumerable<T> Range<T>(IRandom random, T min, T max, Int32 count, Func<IRandom, T, T, T> generator)
		{
			if (generator is null)
			{
				throw new ArgumentNullException(nameof(generator));
			}

			return count <= 0 ? Enumerable.Empty<T>() : Range(random, min, max, generator).Take(count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<SByte> SByteRange(SByte max)
		{
			return SByteRange(Generator, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<SByte> SByteRange(this System.Random random, SByte max)
		{
			return SByteRange(random, SByte.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<SByte> SByteRange(this IRandom random, SByte max)
		{
			return SByteRange(random, SByte.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<SByte> SByteRange(SByte max, Int32 count)
		{
			return SByteRange(Generator, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<SByte> SByteRange(this System.Random random, SByte max, Int32 count)
		{
			return SByteRange(random, SByte.MinValue, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<SByte> SByteRange(this IRandom random, SByte max, Int32 count)
		{
			return SByteRange(random, SByte.MinValue, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<SByte> SByteRange(SByte min, SByte max)
		{
			return Range(min, max, NextSByte);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<SByte> SByteRange(this System.Random random, SByte min, SByte max)
		{
			return Range(random, min, max, NextSByte);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<SByte> SByteRange(this IRandom random, SByte min, SByte max)
		{
			return Range(random, min, max, NextSByte);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<SByte> SByteRange(SByte min, SByte max, Int32 count)
		{
			return Range(min, max, count, NextSByte);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<SByte> SByteRange(this System.Random random, SByte min, SByte max, Int32 count)
		{
			return Range(random, min, max, count, NextSByte);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<SByte> SByteRange(this IRandom random, SByte min, SByte max, Int32 count)
		{
			return Range(random, min, max, count, NextSByte);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Byte> ByteRange(Byte max)
		{
			return ByteRange(Generator, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Byte> ByteRange(this System.Random random, Byte max)
		{
			return ByteRange(random, Byte.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Byte> ByteRange(this IRandom random, Byte max)
		{
			return ByteRange(random, Byte.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Byte> ByteRange(Byte max, Int32 count)
		{
			return ByteRange(Generator, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Byte> ByteRange(this System.Random random, Byte max, Int32 count)
		{
			return ByteRange(random, Byte.MinValue, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Byte> ByteRange(this IRandom random, Byte max, Int32 count)
		{
			return ByteRange(random, Byte.MinValue, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Byte> ByteRange(Byte min, Byte max)
		{
			return Range(min, max, NextByte);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Byte> ByteRange(this System.Random random, Byte min, Byte max)
		{
			return Range(random, min, max, NextByte);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Byte> ByteRange(this IRandom random, Byte min, Byte max)
		{
			return Range(random, min, max, NextByte);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Byte> ByteRange(Byte min, Byte max, Int32 count)
		{
			return Range(min, max, count, NextByte);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Byte> ByteRange(this System.Random random, Byte min, Byte max, Int32 count)
		{
			return Range(random, min, max, count, NextByte);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Byte> ByteRange(this IRandom random, Byte min, Byte max, Int32 count)
		{
			return Range(random, min, max, count, NextByte);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int16> Int16Range(Int16 max)
		{
			return Int16Range(Generator, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int16> Int16Range(this System.Random random, Int16 max)
		{
			return Int16Range(random, Int16.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int16> Int16Range(this IRandom random, Int16 max)
		{
			return Int16Range(random, Int16.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int16> Int16Range(Int16 max, Int32 count)
		{
			return Int16Range(Generator, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int16> Int16Range(this System.Random random, Int16 max, Int32 count)
		{
			return Int16Range(random, Int16.MinValue, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int16> Int16Range(this IRandom random, Int16 max, Int32 count)
		{
			return Int16Range(random, Int16.MinValue, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int16> Int16Range(Int16 min, Int16 max)
		{
			return Range(min, max, NextInt16);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int16> Int16Range(this System.Random random, Int16 min, Int16 max)
		{
			return Range(random, min, max, NextInt16);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int16> Int16Range(this IRandom random, Int16 min, Int16 max)
		{
			return Range(random, min, max, NextInt16);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int16> Int16Range(Int16 min, Int16 max, Int32 count)
		{
			return Range(min, max, count, NextInt16);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int16> Int16Range(this System.Random random, Int16 min, Int16 max, Int32 count)
		{
			return Range(random, min, max, count, NextInt16);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int16> Int16Range(this IRandom random, Int16 min, Int16 max, Int32 count)
		{
			return Range(random, min, max, count, NextInt16);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt16> UInt16Range(UInt16 max)
		{
			return UInt16Range(Generator, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt16> UInt16Range(this System.Random random, UInt16 max)
		{
			return UInt16Range(random, UInt16.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt16> UInt16Range(this IRandom random, UInt16 max)
		{
			return UInt16Range(random, UInt16.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt16> UInt16Range(UInt16 max, Int32 count)
		{
			return UInt16Range(Generator, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt16> UInt16Range(this System.Random random, UInt16 max, Int32 count)
		{
			return UInt16Range(random, UInt16.MinValue, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt16> UInt16Range(this IRandom random, UInt16 max, Int32 count)
		{
			return UInt16Range(random, UInt16.MinValue, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt16> UInt16Range(UInt16 min, UInt16 max)
		{
			return Range(min, max, NextUInt16);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt16> UInt16Range(this System.Random random, UInt16 min, UInt16 max)
		{
			return Range(random, min, max, NextUInt16);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt16> UInt16Range(this IRandom random, UInt16 min, UInt16 max)
		{
			return Range(random, min, max, NextUInt16);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt16> UInt16Range(UInt16 min, UInt16 max, Int32 count)
		{
			return Range(min, max, count, NextUInt16);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt16> UInt16Range(this System.Random random, UInt16 min, UInt16 max, Int32 count)
		{
			return Range(random, min, max, count, NextUInt16);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt16> UInt16Range(this IRandom random, UInt16 min, UInt16 max, Int32 count)
		{
			return Range(random, min, max, count, NextUInt16);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int32> Int32Range()
		{
			return Int32Range(Generator);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int32> Int32Range(this System.Random random)
		{
			return Int32Range(random, Int32.MinValue, Int32.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int32> Int32Range(this IRandom random)
		{
			return Int32Range(random, Int32.MinValue, Int32.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int32> Int32Range(Int32 max)
		{
			return Int32Range(Generator, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int32> Int32Range(this System.Random random, Int32 max)
		{
			return Int32Range(random, Int32.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int32> Int32Range(this IRandom random, Int32 max)
		{
			return Int32Range(random, Int32.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int32> Int32Range(Int32 min, Int32 max)
		{
			return Range(min, max, NextInt32);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int32> Int32Range(this System.Random random, Int32 min, Int32 max)
		{
			return Range(random, min, max, NextInt32);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int32> Int32Range(this IRandom random, Int32 min, Int32 max)
		{
			return Range(random, min, max, NextInt32);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int32> Int32Range(Int32 min, Int32 max, Int32 count)
		{
			return Range(min, max, count, NextInt32);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int32> Int32Range(this System.Random random, Int32 min, Int32 max, Int32 count)
		{
			return Range(random, min, max, count, NextInt32);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int32> Int32Range(this IRandom random, Int32 min, Int32 max, Int32 count)
		{
			return Range(random, min, max, count, NextInt32);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt32> UInt32Range(UInt32 max)
		{
			return UInt32Range(Generator, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt32> UInt32Range(this System.Random random, UInt32 max)
		{
			return UInt32Range(random, UInt32.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt32> UInt32Range(this IRandom random, UInt32 max)
		{
			return UInt32Range(random, UInt32.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt32> UInt32Range(UInt32 max, Int32 count)
		{
			return UInt32Range(Generator, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt32> UInt32Range(this System.Random random, UInt32 max, Int32 count)
		{
			return UInt32Range(random, UInt32.MinValue, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt32> UInt32Range(this IRandom random, UInt32 max, Int32 count)
		{
			return UInt32Range(random, UInt32.MinValue, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt32> UInt32Range(UInt32 min, UInt32 max)
		{
			return Range(min, max, NextUInt32);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt32> UInt32Range(this System.Random random, UInt32 min, UInt32 max)
		{
			return Range(random, min, max, NextUInt32);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt32> UInt32Range(this IRandom random, UInt32 min, UInt32 max)
		{
			return Range(random, min, max, NextUInt32);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt32> UInt32Range(UInt32 min, UInt32 max, Int32 count)
		{
			return Range(min, max, count, NextUInt32);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt32> UInt32Range(this System.Random random, UInt32 min, UInt32 max, Int32 count)
		{
			return Range(random, min, max, count, NextUInt32);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt32> UInt32Range(this IRandom random, UInt32 min, UInt32 max, Int32 count)
		{
			return Range(random, min, max, count, NextUInt32);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int64> Int64Range(Int64 max)
		{
			return Int64Range(Generator, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int64> Int64Range(this System.Random random, Int64 max)
		{
			return Int64Range(random, Int64.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int64> Int64Range(this IRandom random, Int64 max)
		{
			return Int64Range(random, Int64.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int64> Int64Range(Int64 max, Int32 count)
		{
			return Int64Range(Generator, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int64> Int64Range(this System.Random random, Int64 max, Int32 count)
		{
			return Int64Range(random, Int64.MinValue, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int64> Int64Range(this IRandom random, Int64 max, Int32 count)
		{
			return Int64Range(random, Int64.MinValue, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int64> Int64Range(Int64 min, Int64 max)
		{
			return Range(min, max, NextInt64);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int64> Int64Range(this System.Random random, Int64 min, Int64 max)
		{
			return Range(random, min, max, NextInt64);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int64> Int64Range(this IRandom random, Int64 min, Int64 max)
		{
			return Range(random, min, max, NextInt64);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int64> Int64Range(Int64 min, Int64 max, Int32 count)
		{
			return Range(min, max, count, NextInt64);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int64> Int64Range(this System.Random random, Int64 min, Int64 max, Int32 count)
		{
			return Range(random, min, max, count, NextInt64);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int64> Int64Range(this IRandom random, Int64 min, Int64 max, Int32 count)
		{
			return Range(random, min, max, count, NextInt64);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt64> UInt64Range(UInt64 max)
		{
			return UInt64Range(Generator, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt64> UInt64Range(this System.Random random, UInt64 max)
		{
			return UInt64Range(random, UInt64.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt64> UInt64Range(this IRandom random, UInt64 max)
		{
			return UInt64Range(random, UInt64.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt64> UInt64Range(UInt64 max, Int32 count)
		{
			return UInt64Range(Generator, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt64> UInt64Range(this System.Random random, UInt64 max, Int32 count)
		{
			return UInt64Range(random, UInt64.MinValue, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt64> UInt64Range(this IRandom random, UInt64 max, Int32 count)
		{
			return UInt64Range(random, UInt64.MinValue, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt64> UInt64Range(UInt64 min, UInt64 max)
		{
			return Range(min, max, NextUInt64);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt64> UInt64Range(this System.Random random, UInt64 min, UInt64 max)
		{
			return Range(random, min, max, NextUInt64);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt64> UInt64Range(this IRandom random, UInt64 min, UInt64 max)
		{
			return Range(random, min, max, NextUInt64);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt64> UInt64Range(UInt64 min, UInt64 max, Int32 count)
		{
			return Range(min, max, count, NextUInt64);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt64> UInt64Range(this System.Random random, UInt64 min, UInt64 max, Int32 count)
		{
			return Range(random, min, max, count, NextUInt64);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt64> UInt64Range(this IRandom random, UInt64 min, UInt64 max, Int32 count)
		{
			return Range(random, min, max, count, NextUInt64);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Single> SingleRange(Single max)
		{
			return SingleRange(Generator, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Single> SingleRange(this System.Random random, Single max)
		{
			return SingleRange(random, Single.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Single> SingleRange(this IRandom random, Single max)
		{
			return SingleRange(random, Single.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Single> SingleRange(Single max, Int32 count)
		{
			return SingleRange(Generator, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Single> SingleRange(this System.Random random, Single max, Int32 count)
		{
			return SingleRange(random, Single.MinValue, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Single> SingleRange(this IRandom random, Single max, Int32 count)
		{
			return SingleRange(random, Single.MinValue, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Single> SingleRange(Single min, Single max)
		{
			return Range(min, max, NextSingle);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Single> SingleRange(this System.Random random, Single min, Single max)
		{
			return Range(random, min, max, NextSingle);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Single> SingleRange(this IRandom random, Single min, Single max)
		{
			return Range(random, min, max, NextSingle);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Single> SingleRange(Single min, Single max, Int32 count)
		{
			return Range(min, max, count, NextSingle);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Single> SingleRange(this System.Random random, Single min, Single max, Int32 count)
		{
			return Range(random, min, max, count, NextSingle);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Single> SingleRange(this IRandom random, Single min, Single max, Int32 count)
		{
			return Range(random, min, max, count, NextSingle);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Double> DoubleRange(Double max)
		{
			return DoubleRange(Generator, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Double> DoubleRange(this System.Random random, Double max)
		{
			return DoubleRange(random, Double.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Double> DoubleRange(this IRandom random, Double max)
		{
			return DoubleRange(random, Double.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Double> DoubleRange(Double max, Int32 count)
		{
			return DoubleRange(Generator, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Double> DoubleRange(this System.Random random, Double max, Int32 count)
		{
			return DoubleRange(random, Double.MinValue, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Double> DoubleRange(this IRandom random, Double max, Int32 count)
		{
			return DoubleRange(random, Double.MinValue, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Double> DoubleRange(Double min, Double max)
		{
			return Range(min, max, NextDouble);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Double> DoubleRange(this System.Random random, Double min, Double max)
		{
			return Range(random, min, max, NextDouble);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Double> DoubleRange(this IRandom random, Double min, Double max)
		{
			return Range(random, min, max, NextDouble);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Double> DoubleRange(Double min, Double max, Int32 count)
		{
			return Range(min, max, count, NextDouble);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Double> DoubleRange(this System.Random random, Double min, Double max, Int32 count)
		{
			return Range(random, min, max, count, NextDouble);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Double> DoubleRange(this IRandom random, Double min, Double max, Int32 count)
		{
			return Range(random, min, max, count, NextDouble);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Decimal> DecimalRange(Decimal max)
		{
			return DecimalRange(Generator, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Decimal> DecimalRange(this System.Random random, Decimal max)
		{
			return DecimalRange(random, Decimal.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Decimal> DecimalRange(this IRandom random, Decimal max)
		{
			return DecimalRange(random, Decimal.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Decimal> DecimalRange(Decimal max, Int32 count)
		{
			return DecimalRange(Generator, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Decimal> DecimalRange(this System.Random random, Decimal max, Int32 count)
		{
			return DecimalRange(random, Decimal.MinValue, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Decimal> DecimalRange(this IRandom random, Decimal max, Int32 count)
		{
			return DecimalRange(random, Decimal.MinValue, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Decimal> DecimalRange(Decimal min, Decimal max)
		{
			return Range(min, max, NextDecimal);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Decimal> DecimalRange(this System.Random random, Decimal min, Decimal max)
		{
			return Range(random, min, max, NextDecimal);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Decimal> DecimalRange(this IRandom random, Decimal min, Decimal max)
		{
			return Range(random, min, max, NextDecimal);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Decimal> DecimalRange(Decimal min, Decimal max, Int32 count)
		{
			return Range(min, max, count, NextDecimal);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Decimal> DecimalRange(this System.Random random, Decimal min, Decimal max, Int32 count)
		{
			return Range(random, min, max, count, NextDecimal);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Decimal> DecimalRange(this IRandom random, Decimal min, Decimal max, Int32 count)
		{
			return Range(random, min, max, count, NextDecimal);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<TimeSpan> TimeSpanRange(TimeSpan max)
		{
			return TimeSpanRange(Generator, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<TimeSpan> TimeSpanRange(this System.Random random, TimeSpan max)
		{
			return TimeSpanRange(random, TimeSpan.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<TimeSpan> TimeSpanRange(this IRandom random, TimeSpan max)
		{
			return TimeSpanRange(random, TimeSpan.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<TimeSpan> TimeSpanRange(TimeSpan max, Int32 count)
		{
			return TimeSpanRange(Generator, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<TimeSpan> TimeSpanRange(this System.Random random, TimeSpan max, Int32 count)
		{
			return TimeSpanRange(random, TimeSpan.MinValue, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<TimeSpan> TimeSpanRange(this IRandom random, TimeSpan max, Int32 count)
		{
			return TimeSpanRange(random, TimeSpan.MinValue, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<TimeSpan> TimeSpanRange(TimeSpan min, TimeSpan max)
		{
			return Range(min, max, NextTimeSpan);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<TimeSpan> TimeSpanRange(this System.Random random, TimeSpan min, TimeSpan max)
		{
			return Range(random, min, max, NextTimeSpan);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<TimeSpan> TimeSpanRange(this IRandom random, TimeSpan min, TimeSpan max)
		{
			return Range(random, min, max, NextTimeSpan);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<TimeSpan> TimeSpanRange(TimeSpan min, TimeSpan max, Int32 count)
		{
			return Range(min, max, count, NextTimeSpan);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<TimeSpan> TimeSpanRange(this System.Random random, TimeSpan min, TimeSpan max, Int32 count)
		{
			return Range(random, min, max, count, NextTimeSpan);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<TimeSpan> TimeSpanRange(this IRandom random, TimeSpan min, TimeSpan max, Int32 count)
		{
			return Range(random, min, max, count, NextTimeSpan);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<DateTime> DateTimeRange(DateTime max)
		{
			return DateTimeRange(Generator, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<DateTime> DateTimeRange(this System.Random random, DateTime max)
		{
			return DateTimeRange(random, DateTime.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<DateTime> DateTimeRange(this IRandom random, DateTime max)
		{
			return DateTimeRange(random, DateTime.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<DateTime> DateTimeRange(DateTime max, Int32 count)
		{
			return DateTimeRange(Generator, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<DateTime> DateTimeRange(this System.Random random, DateTime max, Int32 count)
		{
			return DateTimeRange(random, DateTime.MinValue, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<DateTime> DateTimeRange(this IRandom random, DateTime max, Int32 count)
		{
			return DateTimeRange(random, DateTime.MinValue, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<DateTime> DateTimeRange(DateTime min, DateTime max)
		{
			return Range(min, max, NextDateTime);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<DateTime> DateTimeRange(this System.Random random, DateTime min, DateTime max)
		{
			return Range(random, min, max, NextDateTime);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<DateTime> DateTimeRange(this IRandom random, DateTime min, DateTime max)
		{
			return Range(random, min, max, NextDateTime);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<DateTime> DateTimeRange(DateTime min, DateTime max, Int32 count)
		{
			return Range(min, max, count, NextDateTime);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<DateTime> DateTimeRange(this System.Random random, DateTime min, DateTime max, Int32 count)
		{
			return Range(random, min, max, count, NextDateTime);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<DateTime> DateTimeRange(this IRandom random, DateTime min, DateTime max, Int32 count)
		{
			return Range(random, min, max, count, NextDateTime);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<SByte> SByteNonNegativeRange(SByte max)
		{
			return SByteNonNegativeRange(Generator, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<SByte> SByteNonNegativeRange(this System.Random random, SByte max)
		{
			return SByteNonNegativeRange(random, default, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<SByte> SByteNonNegativeRange(this IRandom random, SByte max)
		{
			return SByteNonNegativeRange(random, default, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<SByte> SByteNonNegativeRange(SByte max, Int32 count)
		{
			return SByteNonNegativeRange(Generator, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<SByte> SByteNonNegativeRange(this System.Random random, SByte max, Int32 count)
		{
			return SByteNonNegativeRange(random, default, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<SByte> SByteNonNegativeRange(this IRandom random, SByte max, Int32 count)
		{
			return SByteNonNegativeRange(random, default, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<SByte> SByteNonNegativeRange(SByte min, SByte max)
		{
			return Range(min, max, NextNonNegativeSByte);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<SByte> SByteNonNegativeRange(this System.Random random, SByte min, SByte max)
		{
			return Range(random, min, max, NextNonNegativeSByte);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<SByte> SByteNonNegativeRange(this IRandom random, SByte min, SByte max)
		{
			return Range(random, min, max, NextNonNegativeSByte);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<SByte> SByteNonNegativeRange(SByte min, SByte max, Int32 count)
		{
			return Range(min, max, count, NextNonNegativeSByte);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<SByte> SByteNonNegativeRange(this System.Random random, SByte min, SByte max, Int32 count)
		{
			return Range(random, min, max, count, NextNonNegativeSByte);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<SByte> SByteNonNegativeRange(this IRandom random, SByte min, SByte max, Int32 count)
		{
			return Range(random, min, max, count, NextNonNegativeSByte);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Byte> ByteNonNegativeRange(Byte max)
		{
			return ByteNonNegativeRange(Generator, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Byte> ByteNonNegativeRange(this System.Random random, Byte max)
		{
			return ByteNonNegativeRange(random, default, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Byte> ByteNonNegativeRange(this IRandom random, Byte max)
		{
			return ByteNonNegativeRange(random, default, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Byte> ByteNonNegativeRange(Byte max, Int32 count)
		{
			return ByteNonNegativeRange(Generator, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Byte> ByteNonNegativeRange(this System.Random random, Byte max, Int32 count)
		{
			return ByteNonNegativeRange(random, default, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Byte> ByteNonNegativeRange(this IRandom random, Byte max, Int32 count)
		{
			return ByteNonNegativeRange(random, default, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Byte> ByteNonNegativeRange(Byte min, Byte max)
		{
			return Range(min, max, NextNonNegativeByte);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Byte> ByteNonNegativeRange(this System.Random random, Byte min, Byte max)
		{
			return Range(random, min, max, NextNonNegativeByte);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Byte> ByteNonNegativeRange(this IRandom random, Byte min, Byte max)
		{
			return Range(random, min, max, NextNonNegativeByte);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Byte> ByteNonNegativeRange(Byte min, Byte max, Int32 count)
		{
			return Range(min, max, count, NextNonNegativeByte);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Byte> ByteNonNegativeRange(this System.Random random, Byte min, Byte max, Int32 count)
		{
			return Range(random, min, max, count, NextNonNegativeByte);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Byte> ByteNonNegativeRange(this IRandom random, Byte min, Byte max, Int32 count)
		{
			return Range(random, min, max, count, NextNonNegativeByte);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int16> Int16NonNegativeRange(Int16 max)
		{
			return Int16NonNegativeRange(Generator, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int16> Int16NonNegativeRange(this System.Random random, Int16 max)
		{
			return Int16NonNegativeRange(random, default, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int16> Int16NonNegativeRange(this IRandom random, Int16 max)
		{
			return Int16NonNegativeRange(random, default, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int16> Int16NonNegativeRange(Int16 max, Int32 count)
		{
			return Int16NonNegativeRange(Generator, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int16> Int16NonNegativeRange(this System.Random random, Int16 max, Int32 count)
		{
			return Int16NonNegativeRange(random, default, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int16> Int16NonNegativeRange(this IRandom random, Int16 max, Int32 count)
		{
			return Int16NonNegativeRange(random, default, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int16> Int16NonNegativeRange(Int16 min, Int16 max)
		{
			return Range(min, max, NextNonNegativeInt16);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int16> Int16NonNegativeRange(this System.Random random, Int16 min, Int16 max)
		{
			return Range(random, min, max, NextNonNegativeInt16);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int16> Int16NonNegativeRange(this IRandom random, Int16 min, Int16 max)
		{
			return Range(random, min, max, NextNonNegativeInt16);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int16> Int16NonNegativeRange(Int16 min, Int16 max, Int32 count)
		{
			return Range(min, max, count, NextNonNegativeInt16);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int16> Int16NonNegativeRange(this System.Random random, Int16 min, Int16 max, Int32 count)
		{
			return Range(random, min, max, count, NextNonNegativeInt16);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int16> Int16NonNegativeRange(this IRandom random, Int16 min, Int16 max, Int32 count)
		{
			return Range(random, min, max, count, NextNonNegativeInt16);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt16> UInt16NonNegativeRange(UInt16 max)
		{
			return UInt16NonNegativeRange(Generator, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt16> UInt16NonNegativeRange(this System.Random random, UInt16 max)
		{
			return UInt16NonNegativeRange(random, default, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt16> UInt16NonNegativeRange(this IRandom random, UInt16 max)
		{
			return UInt16NonNegativeRange(random, default, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt16> UInt16NonNegativeRange(UInt16 max, Int32 count)
		{
			return UInt16NonNegativeRange(Generator, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt16> UInt16NonNegativeRange(this System.Random random, UInt16 max, Int32 count)
		{
			return UInt16NonNegativeRange(random, default, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt16> UInt16NonNegativeRange(this IRandom random, UInt16 max, Int32 count)
		{
			return UInt16NonNegativeRange(random, default, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt16> UInt16NonNegativeRange(UInt16 min, UInt16 max)
		{
			return Range(min, max, NextNonNegativeUInt16);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt16> UInt16NonNegativeRange(this System.Random random, UInt16 min, UInt16 max)
		{
			return Range(random, min, max, NextNonNegativeUInt16);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt16> UInt16NonNegativeRange(this IRandom random, UInt16 min, UInt16 max)
		{
			return Range(random, min, max, NextNonNegativeUInt16);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt16> UInt16NonNegativeRange(UInt16 min, UInt16 max, Int32 count)
		{
			return Range(min, max, count, NextNonNegativeUInt16);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt16> UInt16NonNegativeRange(this System.Random random, UInt16 min, UInt16 max, Int32 count)
		{
			return Range(random, min, max, count, NextNonNegativeUInt16);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt16> UInt16NonNegativeRange(this IRandom random, UInt16 min, UInt16 max, Int32 count)
		{
			return Range(random, min, max, count, NextNonNegativeUInt16);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int32> Int32NonNegativeRange()
		{
			return Int32NonNegativeRange(Generator);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int32> Int32NonNegativeRange(this System.Random random)
		{
			return Int32NonNegativeRange(random, default, Int32.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int32> Int32NonNegativeRange(this IRandom random)
		{
			return Int32NonNegativeRange(random, default, Int32.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int32> Int32NonNegativeRange(Int32 max)
		{
			return Int32NonNegativeRange(Generator, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int32> Int32NonNegativeRange(this System.Random random, Int32 max)
		{
			return Int32NonNegativeRange(random, default, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int32> Int32NonNegativeRange(this IRandom random, Int32 max)
		{
			return Int32NonNegativeRange(random, default, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int32> Int32NonNegativeRange(Int32 min, Int32 max)
		{
			return Range(min, max, NextNonNegativeInt32);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int32> Int32NonNegativeRange(this System.Random random, Int32 min, Int32 max)
		{
			return Range(random, min, max, NextNonNegativeInt32);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int32> Int32NonNegativeRange(this IRandom random, Int32 min, Int32 max)
		{
			return Range(random, min, max, NextNonNegativeInt32);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int32> Int32NonNegativeRange(Int32 min, Int32 max, Int32 count)
		{
			return Range(min, max, count, NextNonNegativeInt32);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int32> Int32NonNegativeRange(this System.Random random, Int32 min, Int32 max, Int32 count)
		{
			return Range(random, min, max, count, NextNonNegativeInt32);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int32> Int32NonNegativeRange(this IRandom random, Int32 min, Int32 max, Int32 count)
		{
			return Range(random, min, max, count, NextNonNegativeInt32);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt32> UInt32NonNegativeRange(UInt32 max)
		{
			return UInt32NonNegativeRange(Generator, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt32> UInt32NonNegativeRange(this System.Random random, UInt32 max)
		{
			return UInt32NonNegativeRange(random, default, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt32> UInt32NonNegativeRange(this IRandom random, UInt32 max)
		{
			return UInt32NonNegativeRange(random, default, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt32> UInt32NonNegativeRange(UInt32 max, Int32 count)
		{
			return UInt32NonNegativeRange(Generator, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt32> UInt32NonNegativeRange(this System.Random random, UInt32 max, Int32 count)
		{
			return UInt32NonNegativeRange(random, default, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt32> UInt32NonNegativeRange(this IRandom random, UInt32 max, Int32 count)
		{
			return UInt32NonNegativeRange(random, default, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt32> UInt32NonNegativeRange(UInt32 min, UInt32 max)
		{
			return Range(min, max, NextNonNegativeUInt32);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt32> UInt32NonNegativeRange(this System.Random random, UInt32 min, UInt32 max)
		{
			return Range(random, min, max, NextNonNegativeUInt32);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt32> UInt32NonNegativeRange(this IRandom random, UInt32 min, UInt32 max)
		{
			return Range(random, min, max, NextNonNegativeUInt32);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt32> UInt32NonNegativeRange(UInt32 min, UInt32 max, Int32 count)
		{
			return Range(min, max, count, NextNonNegativeUInt32);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt32> UInt32NonNegativeRange(this System.Random random, UInt32 min, UInt32 max, Int32 count)
		{
			return Range(random, min, max, count, NextNonNegativeUInt32);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt32> UInt32NonNegativeRange(this IRandom random, UInt32 min, UInt32 max, Int32 count)
		{
			return Range(random, min, max, count, NextNonNegativeUInt32);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int64> Int64NonNegativeRange(Int64 max)
		{
			return Int64NonNegativeRange(Generator, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int64> Int64NonNegativeRange(this System.Random random, Int64 max)
		{
			return Int64NonNegativeRange(random, default, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int64> Int64NonNegativeRange(this IRandom random, Int64 max)
		{
			return Int64NonNegativeRange(random, default, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int64> Int64NonNegativeRange(Int64 max, Int32 count)
		{
			return Int64NonNegativeRange(Generator, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int64> Int64NonNegativeRange(this System.Random random, Int64 max, Int32 count)
		{
			return Int64NonNegativeRange(random, default, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int64> Int64NonNegativeRange(this IRandom random, Int64 max, Int32 count)
		{
			return Int64NonNegativeRange(random, default, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int64> Int64NonNegativeRange(Int64 min, Int64 max)
		{
			return Range(min, max, NextNonNegativeInt64);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int64> Int64NonNegativeRange(this System.Random random, Int64 min, Int64 max)
		{
			return Range(random, min, max, NextNonNegativeInt64);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int64> Int64NonNegativeRange(this IRandom random, Int64 min, Int64 max)
		{
			return Range(random, min, max, NextNonNegativeInt64);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int64> Int64NonNegativeRange(Int64 min, Int64 max, Int32 count)
		{
			return Range(min, max, count, NextNonNegativeInt64);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int64> Int64NonNegativeRange(this System.Random random, Int64 min, Int64 max, Int32 count)
		{
			return Range(random, min, max, count, NextNonNegativeInt64);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int64> Int64NonNegativeRange(this IRandom random, Int64 min, Int64 max, Int32 count)
		{
			return Range(random, min, max, count, NextNonNegativeInt64);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt64> UInt64NonNegativeRange(UInt64 max)
		{
			return UInt64NonNegativeRange(Generator, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt64> UInt64NonNegativeRange(this System.Random random, UInt64 max)
		{
			return UInt64NonNegativeRange(random, default, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt64> UInt64NonNegativeRange(this IRandom random, UInt64 max)
		{
			return UInt64NonNegativeRange(random, default, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt64> UInt64NonNegativeRange(UInt64 max, Int32 count)
		{
			return UInt64NonNegativeRange(Generator, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt64> UInt64NonNegativeRange(this System.Random random, UInt64 max, Int32 count)
		{
			return UInt64NonNegativeRange(random, default, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt64> UInt64NonNegativeRange(this IRandom random, UInt64 max, Int32 count)
		{
			return UInt64NonNegativeRange(random, default, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt64> UInt64NonNegativeRange(UInt64 min, UInt64 max)
		{
			return Range(min, max, NextNonNegativeUInt64);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt64> UInt64NonNegativeRange(this System.Random random, UInt64 min, UInt64 max)
		{
			return Range(random, min, max, NextNonNegativeUInt64);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt64> UInt64NonNegativeRange(this IRandom random, UInt64 min, UInt64 max)
		{
			return Range(random, min, max, NextNonNegativeUInt64);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt64> UInt64NonNegativeRange(UInt64 min, UInt64 max, Int32 count)
		{
			return Range(min, max, count, NextNonNegativeUInt64);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt64> UInt64NonNegativeRange(this System.Random random, UInt64 min, UInt64 max, Int32 count)
		{
			return Range(random, min, max, count, NextNonNegativeUInt64);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt64> UInt64NonNegativeRange(this IRandom random, UInt64 min, UInt64 max, Int32 count)
		{
			return Range(random, min, max, count, NextNonNegativeUInt64);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Single> SingleNonNegativeRange(Single max)
		{
			return SingleNonNegativeRange(Generator, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Single> SingleNonNegativeRange(this System.Random random, Single max)
		{
			return SingleNonNegativeRange(random, default, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Single> SingleNonNegativeRange(this IRandom random, Single max)
		{
			return SingleNonNegativeRange(random, default, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Single> SingleNonNegativeRange(Single max, Int32 count)
		{
			return SingleNonNegativeRange(Generator, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Single> SingleNonNegativeRange(this System.Random random, Single max, Int32 count)
		{
			return SingleNonNegativeRange(random, default, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Single> SingleNonNegativeRange(this IRandom random, Single max, Int32 count)
		{
			return SingleNonNegativeRange(random, default, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Single> SingleNonNegativeRange(Single min, Single max)
		{
			return Range(min, max, NextNonNegativeSingle);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Single> SingleNonNegativeRange(this System.Random random, Single min, Single max)
		{
			return Range(random, min, max, NextNonNegativeSingle);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Single> SingleNonNegativeRange(this IRandom random, Single min, Single max)
		{
			return Range(random, min, max, NextNonNegativeSingle);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Single> SingleNonNegativeRange(Single min, Single max, Int32 count)
		{
			return Range(min, max, count, NextNonNegativeSingle);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Single> SingleNonNegativeRange(this System.Random random, Single min, Single max, Int32 count)
		{
			return Range(random, min, max, count, NextNonNegativeSingle);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Single> SingleNonNegativeRange(this IRandom random, Single min, Single max, Int32 count)
		{
			return Range(random, min, max, count, NextNonNegativeSingle);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Double> DoubleNonNegativeRange(Double max)
		{
			return DoubleNonNegativeRange(Generator, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Double> DoubleNonNegativeRange(this System.Random random, Double max)
		{
			return DoubleNonNegativeRange(random, default, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Double> DoubleNonNegativeRange(this IRandom random, Double max)
		{
			return DoubleNonNegativeRange(random, default, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Double> DoubleNonNegativeRange(Double max, Int32 count)
		{
			return DoubleNonNegativeRange(Generator, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Double> DoubleNonNegativeRange(this System.Random random, Double max, Int32 count)
		{
			return DoubleNonNegativeRange(random, default, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Double> DoubleNonNegativeRange(this IRandom random, Double max, Int32 count)
		{
			return DoubleNonNegativeRange(random, default, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Double> DoubleNonNegativeRange(Double min, Double max)
		{
			return Range(min, max, NextNonNegativeDouble);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Double> DoubleNonNegativeRange(this System.Random random, Double min, Double max)
		{
			return Range(random, min, max, NextNonNegativeDouble);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Double> DoubleNonNegativeRange(this IRandom random, Double min, Double max)
		{
			return Range(random, min, max, NextNonNegativeDouble);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Double> DoubleNonNegativeRange(Double min, Double max, Int32 count)
		{
			return Range(min, max, count, NextNonNegativeDouble);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Double> DoubleNonNegativeRange(this System.Random random, Double min, Double max, Int32 count)
		{
			return Range(random, min, max, count, NextNonNegativeDouble);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Double> DoubleNonNegativeRange(this IRandom random, Double min, Double max, Int32 count)
		{
			return Range(random, min, max, count, NextNonNegativeDouble);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Decimal> DecimalNonNegativeRange(Decimal max)
		{
			return DecimalNonNegativeRange(Generator, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Decimal> DecimalNonNegativeRange(this System.Random random, Decimal max)
		{
			return DecimalNonNegativeRange(random, default, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Decimal> DecimalNonNegativeRange(this IRandom random, Decimal max)
		{
			return DecimalNonNegativeRange(random, default, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Decimal> DecimalNonNegativeRange(Decimal max, Int32 count)
		{
			return DecimalNonNegativeRange(Generator, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Decimal> DecimalNonNegativeRange(this System.Random random, Decimal max, Int32 count)
		{
			return DecimalNonNegativeRange(random, default, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Decimal> DecimalNonNegativeRange(this IRandom random, Decimal max, Int32 count)
		{
			return DecimalNonNegativeRange(random, default, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Decimal> DecimalNonNegativeRange(Decimal min, Decimal max)
		{
			return Range(min, max, NextNonNegativeDecimal);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Decimal> DecimalNonNegativeRange(this System.Random random, Decimal min, Decimal max)
		{
			return Range(random, min, max, NextNonNegativeDecimal);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Decimal> DecimalNonNegativeRange(this IRandom random, Decimal min, Decimal max)
		{
			return Range(random, min, max, NextNonNegativeDecimal);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Decimal> DecimalNonNegativeRange(Decimal min, Decimal max, Int32 count)
		{
			return Range(min, max, count, NextNonNegativeDecimal);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Decimal> DecimalNonNegativeRange(this System.Random random, Decimal min, Decimal max, Int32 count)
		{
			return Range(random, min, max, count, NextNonNegativeDecimal);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Decimal> DecimalNonNegativeRange(this IRandom random, Decimal min, Decimal max, Int32 count)
		{
			return Range(random, min, max, count, NextNonNegativeDecimal);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<SByte> SByteNonZeroRange(SByte max)
		{
			return SByteNonZeroRange(SByte.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<SByte> SByteNonZeroRange(this System.Random random, SByte max)
		{
			return SByteNonZeroRange(random, SByte.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<SByte> SByteNonZeroRange(this IRandom random, SByte max)
		{
			return SByteNonZeroRange(random, SByte.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<SByte> SByteNonZeroRange(SByte min, SByte max)
		{
			return Range(min, max, NextNonZeroSByte);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<SByte> SByteNonZeroRange(this System.Random random, SByte min, SByte max)
		{
			return Range(random, min, max, NextNonZeroSByte);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<SByte> SByteNonZeroRange(this IRandom random, SByte min, SByte max)
		{
			return Range(random, min, max, NextNonZeroSByte);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<SByte> SByteNonZeroRange(SByte min, SByte max, Int32 count)
		{
			return Range(min, max, count, NextNonZeroSByte);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<SByte> SByteNonZeroRange(this System.Random random, SByte min, SByte max, Int32 count)
		{
			return Range(random, min, max, count, NextNonZeroSByte);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<SByte> SByteNonZeroRange(this IRandom random, SByte min, SByte max, Int32 count)
		{
			return Range(random, min, max, count, NextNonZeroSByte);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Byte> ByteNonZeroRange(Byte max)
		{
			return ByteNonZeroRange(Byte.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Byte> ByteNonZeroRange(this System.Random random, Byte max)
		{
			return ByteNonZeroRange(random, Byte.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Byte> ByteNonZeroRange(this IRandom random, Byte max)
		{
			return ByteNonZeroRange(random, Byte.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Byte> ByteNonZeroRange(Byte min, Byte max)
		{
			return Range(min, max, NextNonZeroByte);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Byte> ByteNonZeroRange(this System.Random random, Byte min, Byte max)
		{
			return Range(random, min, max, NextNonZeroByte);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Byte> ByteNonZeroRange(this IRandom random, Byte min, Byte max)
		{
			return Range(random, min, max, NextNonZeroByte);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Byte> ByteNonZeroRange(Byte min, Byte max, Int32 count)
		{
			return Range(min, max, count, NextNonZeroByte);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Byte> ByteNonZeroRange(this System.Random random, Byte min, Byte max, Int32 count)
		{
			return Range(random, min, max, count, NextNonZeroByte);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Byte> ByteNonZeroRange(this IRandom random, Byte min, Byte max, Int32 count)
		{
			return Range(random, min, max, count, NextNonZeroByte);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int16> Int16NonZeroRange(Int16 max)
		{
			return Int16NonZeroRange(Int16.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int16> Int16NonZeroRange(this System.Random random, Int16 max)
		{
			return Int16NonZeroRange(random, Int16.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int16> Int16NonZeroRange(this IRandom random, Int16 max)
		{
			return Int16NonZeroRange(random, Int16.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int16> Int16NonZeroRange(Int16 min, Int16 max)
		{
			return Range(min, max, NextNonZeroInt16);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int16> Int16NonZeroRange(this System.Random random, Int16 min, Int16 max)
		{
			return Range(random, min, max, NextNonZeroInt16);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int16> Int16NonZeroRange(this IRandom random, Int16 min, Int16 max)
		{
			return Range(random, min, max, NextNonZeroInt16);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int16> Int16NonZeroRange(Int16 min, Int16 max, Int32 count)
		{
			return Range(min, max, count, NextNonZeroInt16);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int16> Int16NonZeroRange(this System.Random random, Int16 min, Int16 max, Int32 count)
		{
			return Range(random, min, max, count, NextNonZeroInt16);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int16> Int16NonZeroRange(this IRandom random, Int16 min, Int16 max, Int32 count)
		{
			return Range(random, min, max, count, NextNonZeroInt16);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt16> UInt16NonZeroRange(UInt16 max)
		{
			return UInt16NonZeroRange(UInt16.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt16> UInt16NonZeroRange(this System.Random random, UInt16 max)
		{
			return UInt16NonZeroRange(random, UInt16.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt16> UInt16NonZeroRange(this IRandom random, UInt16 max)
		{
			return UInt16NonZeroRange(random, UInt16.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt16> UInt16NonZeroRange(UInt16 min, UInt16 max)
		{
			return Range(min, max, NextNonZeroUInt16);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt16> UInt16NonZeroRange(this System.Random random, UInt16 min, UInt16 max)
		{
			return Range(random, min, max, NextNonZeroUInt16);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt16> UInt16NonZeroRange(this IRandom random, UInt16 min, UInt16 max)
		{
			return Range(random, min, max, NextNonZeroUInt16);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt16> UInt16NonZeroRange(UInt16 min, UInt16 max, Int32 count)
		{
			return Range(min, max, count, NextNonZeroUInt16);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt16> UInt16NonZeroRange(this System.Random random, UInt16 min, UInt16 max, Int32 count)
		{
			return Range(random, min, max, count, NextNonZeroUInt16);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt16> UInt16NonZeroRange(this IRandom random, UInt16 min, UInt16 max, Int32 count)
		{
			return Range(random, min, max, count, NextNonZeroUInt16);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int32> Int32NonZeroRange()
		{
			return Int32NonZeroRange(Int32.MinValue, Int32.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int32> Int32NonZeroRange(this System.Random random)
		{
			return Int32NonZeroRange(random, Int32.MinValue, Int32.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int32> Int32NonZeroRange(this IRandom random)
		{
			return Int32NonZeroRange(random, Int32.MinValue, Int32.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int32> Int32NonZeroRange(Int32 max)
		{
			return Int32NonZeroRange(Int32.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int32> Int32NonZeroRange(this System.Random random, Int32 max)
		{
			return Int32NonZeroRange(random, Int32.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int32> Int32NonZeroRange(this IRandom random, Int32 max)
		{
			return Int32NonZeroRange(random, Int32.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int32> Int32NonZeroRange(Int32 min, Int32 max)
		{
			return Range(min, max, NextNonZeroInt32);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int32> Int32NonZeroRange(this System.Random random, Int32 min, Int32 max)
		{
			return Range(random, min, max, NextNonZeroInt32);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int32> Int32NonZeroRange(this IRandom random, Int32 min, Int32 max)
		{
			return Range(random, min, max, NextNonZeroInt32);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int32> Int32NonZeroRange(Int32 min, Int32 max, Int32 count)
		{
			return Range(min, max, count, NextNonZeroInt32);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int32> Int32NonZeroRange(this System.Random random, Int32 min, Int32 max, Int32 count)
		{
			return Range(random, min, max, count, NextNonZeroInt32);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int32> Int32NonZeroRange(this IRandom random, Int32 min, Int32 max, Int32 count)
		{
			return Range(random, min, max, count, NextNonZeroInt32);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt32> UInt32NonZeroRange(UInt32 max)
		{
			return UInt32NonZeroRange(UInt32.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt32> UInt32NonZeroRange(this System.Random random, UInt32 max)
		{
			return UInt32NonZeroRange(random, UInt32.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt32> UInt32NonZeroRange(this IRandom random, UInt32 max)
		{
			return UInt32NonZeroRange(random, UInt32.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt32> UInt32NonZeroRange(UInt32 min, UInt32 max)
		{
			return Range(min, max, NextNonZeroUInt32);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt32> UInt32NonZeroRange(this System.Random random, UInt32 min, UInt32 max)
		{
			return Range(random, min, max, NextNonZeroUInt32);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt32> UInt32NonZeroRange(this IRandom random, UInt32 min, UInt32 max)
		{
			return Range(random, min, max, NextNonZeroUInt32);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt32> UInt32NonZeroRange(UInt32 min, UInt32 max, Int32 count)
		{
			return Range(min, max, count, NextNonZeroUInt32);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt32> UInt32NonZeroRange(this System.Random random, UInt32 min, UInt32 max, Int32 count)
		{
			return Range(random, min, max, count, NextNonZeroUInt32);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt32> UInt32NonZeroRange(this IRandom random, UInt32 min, UInt32 max, Int32 count)
		{
			return Range(random, min, max, count, NextNonZeroUInt32);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int64> Int64NonZeroRange(Int64 max)
		{
			return Int64NonZeroRange(Int64.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int64> Int64NonZeroRange(this System.Random random, Int64 max)
		{
			return Int64NonZeroRange(random, Int64.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int64> Int64NonZeroRange(this IRandom random, Int64 max)
		{
			return Int64NonZeroRange(random, Int64.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int64> Int64NonZeroRange(Int64 min, Int64 max)
		{
			return Range(min, max, NextNonZeroInt64);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int64> Int64NonZeroRange(this System.Random random, Int64 min, Int64 max)
		{
			return Range(random, min, max, NextNonZeroInt64);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int64> Int64NonZeroRange(this IRandom random, Int64 min, Int64 max)
		{
			return Range(random, min, max, NextNonZeroInt64);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int64> Int64NonZeroRange(Int64 min, Int64 max, Int32 count)
		{
			return Range(min, max, count, NextNonZeroInt64);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int64> Int64NonZeroRange(this System.Random random, Int64 min, Int64 max, Int32 count)
		{
			return Range(random, min, max, count, NextNonZeroInt64);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int64> Int64NonZeroRange(this IRandom random, Int64 min, Int64 max, Int32 count)
		{
			return Range(random, min, max, count, NextNonZeroInt64);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt64> UInt64NonZeroRange(UInt64 max)
		{
			return UInt64NonZeroRange(UInt64.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt64> UInt64NonZeroRange(this System.Random random, UInt64 max)
		{
			return UInt64NonZeroRange(random, UInt64.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt64> UInt64NonZeroRange(this IRandom random, UInt64 max)
		{
			return UInt64NonZeroRange(random, UInt64.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt64> UInt64NonZeroRange(UInt64 min, UInt64 max)
		{
			return Range(min, max, NextNonZeroUInt64);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt64> UInt64NonZeroRange(this System.Random random, UInt64 min, UInt64 max)
		{
			return Range(random, min, max, NextNonZeroUInt64);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt64> UInt64NonZeroRange(this IRandom random, UInt64 min, UInt64 max)
		{
			return Range(random, min, max, NextNonZeroUInt64);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt64> UInt64NonZeroRange(UInt64 min, UInt64 max, Int32 count)
		{
			return Range(min, max, count, NextNonZeroUInt64);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt64> UInt64NonZeroRange(this System.Random random, UInt64 min, UInt64 max, Int32 count)
		{
			return Range(random, min, max, count, NextNonZeroUInt64);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt64> UInt64NonZeroRange(this IRandom random, UInt64 min, UInt64 max, Int32 count)
		{
			return Range(random, min, max, count, NextNonZeroUInt64);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Single> SingleNonZeroRange(Single max)
		{
			return SingleNonZeroRange(Single.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Single> SingleNonZeroRange(this System.Random random, Single max)
		{
			return SingleNonZeroRange(random, Single.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Single> SingleNonZeroRange(this IRandom random, Single max)
		{
			return SingleNonZeroRange(random, Single.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Single> SingleNonZeroRange(Single min, Single max)
		{
			return Range(min, max, NextNonZeroSingle);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Single> SingleNonZeroRange(this System.Random random, Single min, Single max)
		{
			return Range(random, min, max, NextNonZeroSingle);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Single> SingleNonZeroRange(this IRandom random, Single min, Single max)
		{
			return Range(random, min, max, NextNonZeroSingle);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Single> SingleNonZeroRange(Single min, Single max, Int32 count)
		{
			return Range(min, max, count, NextNonZeroSingle);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Single> SingleNonZeroRange(this System.Random random, Single min, Single max, Int32 count)
		{
			return Range(random, min, max, count, NextNonZeroSingle);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Single> SingleNonZeroRange(this IRandom random, Single min, Single max, Int32 count)
		{
			return Range(random, min, max, count, NextNonZeroSingle);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Double> DoubleNonZeroRange(Double max)
		{
			return DoubleNonZeroRange(Double.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Double> DoubleNonZeroRange(this System.Random random, Double max)
		{
			return DoubleNonZeroRange(random, Double.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Double> DoubleNonZeroRange(this IRandom random, Double max)
		{
			return DoubleNonZeroRange(random, Double.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Double> DoubleNonZeroRange(Double min, Double max)
		{
			return Range(min, max, NextNonZeroDouble);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Double> DoubleNonZeroRange(this System.Random random, Double min, Double max)
		{
			return Range(random, min, max, NextNonZeroDouble);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Double> DoubleNonZeroRange(this IRandom random, Double min, Double max)
		{
			return Range(random, min, max, NextNonZeroDouble);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Double> DoubleNonZeroRange(Double min, Double max, Int32 count)
		{
			return Range(min, max, count, NextNonZeroDouble);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Double> DoubleNonZeroRange(this System.Random random, Double min, Double max, Int32 count)
		{
			return Range(random, min, max, count, NextNonZeroDouble);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Double> DoubleNonZeroRange(this IRandom random, Double min, Double max, Int32 count)
		{
			return Range(random, min, max, count, NextNonZeroDouble);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Decimal> DecimalNonZeroRange(Decimal max)
		{
			return DecimalNonZeroRange(Decimal.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Decimal> DecimalNonZeroRange(this System.Random random, Decimal max)
		{
			return DecimalNonZeroRange(random, Decimal.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Decimal> DecimalNonZeroRange(this IRandom random, Decimal max)
		{
			return DecimalNonZeroRange(random, Decimal.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Decimal> DecimalNonZeroRange(Decimal min, Decimal max)
		{
			return Range(min, max, NextNonZeroDecimal);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Decimal> DecimalNonZeroRange(this System.Random random, Decimal min, Decimal max)
		{
			return Range(random, min, max, NextNonZeroDecimal);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Decimal> DecimalNonZeroRange(this IRandom random, Decimal min, Decimal max)
		{
			return Range(random, min, max, NextNonZeroDecimal);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Decimal> DecimalNonZeroRange(Decimal min, Decimal max, Int32 count)
		{
			return Range(min, max, count, NextNonZeroDecimal);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Decimal> DecimalNonZeroRange(this System.Random random, Decimal min, Decimal max, Int32 count)
		{
			return Range(random, min, max, count, NextNonZeroDecimal);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Decimal> DecimalNonZeroRange(this IRandom random, Decimal min, Decimal max, Int32 count)
		{
			return Range(random, min, max, count, NextNonZeroDecimal);
		}

    }
}