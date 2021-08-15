// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;
using NetExtender.Random.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.Utilities.Numerics
{
    public static partial class RandomUtilities
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
		public static SByte NextSByte<T>(this T random) where T : IRandom
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
		public static SByte NextSByte<T>(this T random, SByte max) where T : IRandom
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
			return (SByte) ((value * max - value * min).Round() + min);
		}

		public static SByte NextSByte<T>(this T random, SByte min, SByte max) where T : IRandom
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
			return (SByte) ((value * max - value * min).Round() + min);
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
		public static Byte NextByte<T>(this T random) where T : IRandom
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
		public static Byte NextByte<T>(this T random, Byte max) where T : IRandom
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
			return (Byte) ((value * max - value * min).Round() + min);
		}

		public static Byte NextByte<T>(this T random, Byte min, Byte max) where T : IRandom
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
			return (Byte) ((value * max - value * min).Round() + min);
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
		public static Int16 NextInt16<T>(this T random) where T : IRandom
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
		public static Int16 NextInt16<T>(this T random, Int16 max) where T : IRandom
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
			return (Int16) ((value * max - value * min).Round() + min);
		}

		public static Int16 NextInt16<T>(this T random, Int16 min, Int16 max) where T : IRandom
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
			return (Int16) ((value * max - value * min).Round() + min);
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
		public static UInt16 NextUInt16<T>(this T random) where T : IRandom
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
		public static UInt16 NextUInt16<T>(this T random, UInt16 max) where T : IRandom
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
			return (UInt16) ((value * max - value * min).Round() + min);
		}

		public static UInt16 NextUInt16<T>(this T random, UInt16 min, UInt16 max) where T : IRandom
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
			return (UInt16) ((value * max - value * min).Round() + min);
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
		public static Int32 NextInt32<T>(this T random) where T : IRandom
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
		public static Int32 NextInt32<T>(this T random, Int32 max) where T : IRandom
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
			return (Int32) ((value * max - value * min).Round() + min);
		}

		public static Int32 NextInt32<T>(this T random, Int32 min, Int32 max) where T : IRandom
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
			return (Int32) ((value * max - value * min).Round() + min);
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
		public static UInt32 NextUInt32<T>(this T random) where T : IRandom
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
		public static UInt32 NextUInt32<T>(this T random, UInt32 max) where T : IRandom
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
			return (UInt32) ((value * max - value * min).Round() + min);
		}

		public static UInt32 NextUInt32<T>(this T random, UInt32 min, UInt32 max) where T : IRandom
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
			return (UInt32) ((value * max - value * min).Round() + min);
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
		public static Int64 NextInt64<T>(this T random) where T : IRandom
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
		public static Int64 NextInt64<T>(this T random, Int64 max) where T : IRandom
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
			return (Int64) ((value * max - value * min).Round() + min);
		}

		public static Int64 NextInt64<T>(this T random, Int64 min, Int64 max) where T : IRandom
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
			return (Int64) ((value * max - value * min).Round() + min);
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
		public static UInt64 NextUInt64<T>(this T random) where T : IRandom
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
		public static UInt64 NextUInt64<T>(this T random, UInt64 max) where T : IRandom
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
			return (UInt64) ((value * max - value * min).Round() + min);
		}

		public static UInt64 NextUInt64<T>(this T random, UInt64 min, UInt64 max) where T : IRandom
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
			return (UInt64) ((value * max - value * min).Round() + min);
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
		public static SByte NextNonNegativeSByte<T>(this T random) where T : IRandom
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
		public static SByte NextNonNegativeSByte<T>(this T random, SByte max) where T : IRandom
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

		public static SByte NextNonNegativeSByte<T>(this T random, SByte min, SByte max) where T : IRandom
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
		public static Byte NextNonNegativeByte<T>(this T random) where T : IRandom
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
		public static Byte NextNonNegativeByte<T>(this T random, Byte max) where T : IRandom
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

		public static Byte NextNonNegativeByte<T>(this T random, Byte min, Byte max) where T : IRandom
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
		public static Int16 NextNonNegativeInt16<T>(this T random) where T : IRandom
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
		public static Int16 NextNonNegativeInt16<T>(this T random, Int16 max) where T : IRandom
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

		public static Int16 NextNonNegativeInt16<T>(this T random, Int16 min, Int16 max) where T : IRandom
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
		public static UInt16 NextNonNegativeUInt16<T>(this T random) where T : IRandom
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
		public static UInt16 NextNonNegativeUInt16<T>(this T random, UInt16 max) where T : IRandom
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

		public static UInt16 NextNonNegativeUInt16<T>(this T random, UInt16 min, UInt16 max) where T : IRandom
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
		public static Int32 NextNonNegativeInt32<T>(this T random) where T : IRandom
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
		public static Int32 NextNonNegativeInt32<T>(this T random, Int32 max) where T : IRandom
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

		public static Int32 NextNonNegativeInt32<T>(this T random, Int32 min, Int32 max) where T : IRandom
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
		public static UInt32 NextNonNegativeUInt32<T>(this T random) where T : IRandom
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
		public static UInt32 NextNonNegativeUInt32<T>(this T random, UInt32 max) where T : IRandom
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

		public static UInt32 NextNonNegativeUInt32<T>(this T random, UInt32 min, UInt32 max) where T : IRandom
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
		public static Int64 NextNonNegativeInt64<T>(this T random) where T : IRandom
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
		public static Int64 NextNonNegativeInt64<T>(this T random, Int64 max) where T : IRandom
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

		public static Int64 NextNonNegativeInt64<T>(this T random, Int64 min, Int64 max) where T : IRandom
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
		public static UInt64 NextNonNegativeUInt64<T>(this T random) where T : IRandom
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
		public static UInt64 NextNonNegativeUInt64<T>(this T random, UInt64 max) where T : IRandom
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

		public static UInt64 NextNonNegativeUInt64<T>(this T random, UInt64 min, UInt64 max) where T : IRandom
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
		public static Single NextNonNegativeSingle<T>(this T random) where T : IRandom
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
		public static Single NextNonNegativeSingle<T>(this T random, Single max) where T : IRandom
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

		public static Single NextNonNegativeSingle<T>(this T random, Single min, Single max) where T : IRandom
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
		public static Double NextNonNegativeDouble<T>(this T random) where T : IRandom
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
		public static Double NextNonNegativeDouble<T>(this T random, Double max) where T : IRandom
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

		public static Double NextNonNegativeDouble<T>(this T random, Double min, Double max) where T : IRandom
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
		public static Decimal NextNonNegativeDecimal<T>(this T random) where T : IRandom
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
		public static Decimal NextNonNegativeDecimal<T>(this T random, Decimal max) where T : IRandom
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

		public static Decimal NextNonNegativeDecimal<T>(this T random, Decimal min, Decimal max) where T : IRandom
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
		public static SByte NextNonZeroSByte<T>(this T random) where T : IRandom
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
		public static SByte NextNonZeroSByte<T>(this T random, SByte max) where T : IRandom
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
		public static SByte NextNonZeroSByte<T>(this T random, SByte min, SByte max) where T : IRandom
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
		public static Byte NextNonZeroByte<T>(this T random) where T : IRandom
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
		public static Byte NextNonZeroByte<T>(this T random, Byte max) where T : IRandom
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
		public static Byte NextNonZeroByte<T>(this T random, Byte min, Byte max) where T : IRandom
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
		public static Int16 NextNonZeroInt16<T>(this T random) where T : IRandom
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
		public static Int16 NextNonZeroInt16<T>(this T random, Int16 max) where T : IRandom
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
		public static Int16 NextNonZeroInt16<T>(this T random, Int16 min, Int16 max) where T : IRandom
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
		public static UInt16 NextNonZeroUInt16<T>(this T random) where T : IRandom
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
		public static UInt16 NextNonZeroUInt16<T>(this T random, UInt16 max) where T : IRandom
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
		public static UInt16 NextNonZeroUInt16<T>(this T random, UInt16 min, UInt16 max) where T : IRandom
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
		public static Int32 NextNonZeroInt32<T>(this T random) where T : IRandom
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
		public static Int32 NextNonZeroInt32<T>(this T random, Int32 max) where T : IRandom
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
		public static Int32 NextNonZeroInt32<T>(this T random, Int32 min, Int32 max) where T : IRandom
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
		public static UInt32 NextNonZeroUInt32<T>(this T random) where T : IRandom
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
		public static UInt32 NextNonZeroUInt32<T>(this T random, UInt32 max) where T : IRandom
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
		public static UInt32 NextNonZeroUInt32<T>(this T random, UInt32 min, UInt32 max) where T : IRandom
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
		public static Int64 NextNonZeroInt64<T>(this T random) where T : IRandom
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
		public static Int64 NextNonZeroInt64<T>(this T random, Int64 max) where T : IRandom
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
		public static Int64 NextNonZeroInt64<T>(this T random, Int64 min, Int64 max) where T : IRandom
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
		public static UInt64 NextNonZeroUInt64<T>(this T random) where T : IRandom
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
		public static UInt64 NextNonZeroUInt64<T>(this T random, UInt64 max) where T : IRandom
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
		public static UInt64 NextNonZeroUInt64<T>(this T random, UInt64 min, UInt64 max) where T : IRandom
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
		public static Single NextNonZeroSingle<T>(this T random) where T : IRandom
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
		public static Single NextNonZeroSingle<T>(this T random, Single max) where T : IRandom
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
		public static Single NextNonZeroSingle<T>(this T random, Single min, Single max) where T : IRandom
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
		public static Double NextNonZeroDouble<T>(this T random) where T : IRandom
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
		public static Double NextNonZeroDouble<T>(this T random, Double max) where T : IRandom
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
		public static Double NextNonZeroDouble<T>(this T random, Double min, Double max) where T : IRandom
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
		public static Decimal NextNonZeroDecimal<T>(this T random) where T : IRandom
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
		public static Decimal NextNonZeroDecimal<T>(this T random, Decimal max) where T : IRandom
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
		public static Decimal NextNonZeroDecimal<T>(this T random, Decimal min, Decimal max) where T : IRandom
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
		public static void Next<T>(this T random, Span<SByte> span) where T : IRandom
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
		public static void Next<T>(this T random, Span<SByte> span, SByte max) where T : IRandom
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
		public static void Next<T>(this T random, Span<SByte> span, SByte min, SByte max) where T : IRandom
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
		public static void Next<T>(this T random, Span<Byte> span) where T : IRandom
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
		public static void Next<T>(this T random, Span<Byte> span, Byte max) where T : IRandom
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
		public static void Next<T>(this T random, Span<Byte> span, Byte min, Byte max) where T : IRandom
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
		public static void Next<T>(this T random, Span<Int16> span) where T : IRandom
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
		public static void Next<T>(this T random, Span<Int16> span, Int16 max) where T : IRandom
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
		public static void Next<T>(this T random, Span<Int16> span, Int16 min, Int16 max) where T : IRandom
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
		public static void Next<T>(this T random, Span<UInt16> span) where T : IRandom
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
		public static void Next<T>(this T random, Span<UInt16> span, UInt16 max) where T : IRandom
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
		public static void Next<T>(this T random, Span<UInt16> span, UInt16 min, UInt16 max) where T : IRandom
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
		public static void Next<T>(this T random, Span<Int32> span) where T : IRandom
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
		public static void Next<T>(this T random, Span<Int32> span, Int32 max) where T : IRandom
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
		public static void Next<T>(this T random, Span<Int32> span, Int32 min, Int32 max) where T : IRandom
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
		public static void Next<T>(this T random, Span<UInt32> span) where T : IRandom
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
		public static void Next<T>(this T random, Span<UInt32> span, UInt32 max) where T : IRandom
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
		public static void Next<T>(this T random, Span<UInt32> span, UInt32 min, UInt32 max) where T : IRandom
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
		public static void Next<T>(this T random, Span<Int64> span) where T : IRandom
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
		public static void Next<T>(this T random, Span<Int64> span, Int64 max) where T : IRandom
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
		public static void Next<T>(this T random, Span<Int64> span, Int64 min, Int64 max) where T : IRandom
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
		public static void Next<T>(this T random, Span<UInt64> span) where T : IRandom
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
		public static void Next<T>(this T random, Span<UInt64> span, UInt64 max) where T : IRandom
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
		public static void Next<T>(this T random, Span<UInt64> span, UInt64 min, UInt64 max) where T : IRandom
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
		public static void Next<T>(this T random, Span<Single> span) where T : IRandom
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
		public static void Next<T>(this T random, Span<Single> span, Single max) where T : IRandom
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
		public static void Next<T>(this T random, Span<Single> span, Single min, Single max) where T : IRandom
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
		public static void Next<T>(this T random, Span<Double> span) where T : IRandom
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
		public static void Next<T>(this T random, Span<Double> span, Double max) where T : IRandom
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
		public static void Next<T>(this T random, Span<Double> span, Double min, Double max) where T : IRandom
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
		public static void Next<T>(this T random, Span<Decimal> span) where T : IRandom
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
		public static void Next<T>(this T random, Span<Decimal> span, Decimal max) where T : IRandom
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
		public static void Next<T>(this T random, Span<Decimal> span, Decimal min, Decimal max) where T : IRandom
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
		public static void NextNonNegative<T>(this T random, Span<SByte> span) where T : IRandom
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
		public static void NextNonNegative<T>(this T random, Span<SByte> span, SByte max) where T : IRandom
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
		public static void NextNonNegative<T>(this T random, Span<SByte> span, SByte min, SByte max) where T : IRandom
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
		public static void NextNonNegative<T>(this T random, Span<Byte> span) where T : IRandom
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
		public static void NextNonNegative<T>(this T random, Span<Byte> span, Byte max) where T : IRandom
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
		public static void NextNonNegative<T>(this T random, Span<Byte> span, Byte min, Byte max) where T : IRandom
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
		public static void NextNonNegative<T>(this T random, Span<Int16> span) where T : IRandom
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
		public static void NextNonNegative<T>(this T random, Span<Int16> span, Int16 max) where T : IRandom
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
		public static void NextNonNegative<T>(this T random, Span<Int16> span, Int16 min, Int16 max) where T : IRandom
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
		public static void NextNonNegative<T>(this T random, Span<UInt16> span) where T : IRandom
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
		public static void NextNonNegative<T>(this T random, Span<UInt16> span, UInt16 max) where T : IRandom
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
		public static void NextNonNegative<T>(this T random, Span<UInt16> span, UInt16 min, UInt16 max) where T : IRandom
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
		public static void NextNonNegative<T>(this T random, Span<Int32> span) where T : IRandom
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
		public static void NextNonNegative<T>(this T random, Span<Int32> span, Int32 max) where T : IRandom
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
		public static void NextNonNegative<T>(this T random, Span<Int32> span, Int32 min, Int32 max) where T : IRandom
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
		public static void NextNonNegative<T>(this T random, Span<UInt32> span) where T : IRandom
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
		public static void NextNonNegative<T>(this T random, Span<UInt32> span, UInt32 max) where T : IRandom
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
		public static void NextNonNegative<T>(this T random, Span<UInt32> span, UInt32 min, UInt32 max) where T : IRandom
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
		public static void NextNonNegative<T>(this T random, Span<Int64> span) where T : IRandom
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
		public static void NextNonNegative<T>(this T random, Span<Int64> span, Int64 max) where T : IRandom
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
		public static void NextNonNegative<T>(this T random, Span<Int64> span, Int64 min, Int64 max) where T : IRandom
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
		public static void NextNonNegative<T>(this T random, Span<UInt64> span) where T : IRandom
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
		public static void NextNonNegative<T>(this T random, Span<UInt64> span, UInt64 max) where T : IRandom
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
		public static void NextNonNegative<T>(this T random, Span<UInt64> span, UInt64 min, UInt64 max) where T : IRandom
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
		public static void NextNonNegative<T>(this T random, Span<Single> span) where T : IRandom
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
		public static void NextNonNegative<T>(this T random, Span<Single> span, Single max) where T : IRandom
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
		public static void NextNonNegative<T>(this T random, Span<Single> span, Single min, Single max) where T : IRandom
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
		public static void NextNonNegative<T>(this T random, Span<Double> span) where T : IRandom
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
		public static void NextNonNegative<T>(this T random, Span<Double> span, Double max) where T : IRandom
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
		public static void NextNonNegative<T>(this T random, Span<Double> span, Double min, Double max) where T : IRandom
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
		public static void NextNonNegative<T>(this T random, Span<Decimal> span) where T : IRandom
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
		public static void NextNonNegative<T>(this T random, Span<Decimal> span, Decimal max) where T : IRandom
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
		public static void NextNonNegative<T>(this T random, Span<Decimal> span, Decimal min, Decimal max) where T : IRandom
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
		public static void NextNonZero<T>(this T random, Span<SByte> span) where T : IRandom
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
		public static void NextNonZero<T>(this T random, Span<SByte> span, SByte max) where T : IRandom
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
		public static void NextNonZero<T>(this T random, Span<SByte> span, SByte min, SByte max) where T : IRandom
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
		public static void NextNonZero<T>(this T random, Span<Byte> span) where T : IRandom
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
		public static void NextNonZero<T>(this T random, Span<Byte> span, Byte max) where T : IRandom
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
		public static void NextNonZero<T>(this T random, Span<Byte> span, Byte min, Byte max) where T : IRandom
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
		public static void NextNonZero<T>(this T random, Span<Int16> span) where T : IRandom
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
		public static void NextNonZero<T>(this T random, Span<Int16> span, Int16 max) where T : IRandom
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
		public static void NextNonZero<T>(this T random, Span<Int16> span, Int16 min, Int16 max) where T : IRandom
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
		public static void NextNonZero<T>(this T random, Span<UInt16> span) where T : IRandom
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
		public static void NextNonZero<T>(this T random, Span<UInt16> span, UInt16 max) where T : IRandom
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
		public static void NextNonZero<T>(this T random, Span<UInt16> span, UInt16 min, UInt16 max) where T : IRandom
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
		public static void NextNonZero<T>(this T random, Span<Int32> span) where T : IRandom
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
		public static void NextNonZero<T>(this T random, Span<Int32> span, Int32 max) where T : IRandom
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
		public static void NextNonZero<T>(this T random, Span<Int32> span, Int32 min, Int32 max) where T : IRandom
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
		public static void NextNonZero<T>(this T random, Span<UInt32> span) where T : IRandom
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
		public static void NextNonZero<T>(this T random, Span<UInt32> span, UInt32 max) where T : IRandom
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
		public static void NextNonZero<T>(this T random, Span<UInt32> span, UInt32 min, UInt32 max) where T : IRandom
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
		public static void NextNonZero<T>(this T random, Span<Int64> span) where T : IRandom
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
		public static void NextNonZero<T>(this T random, Span<Int64> span, Int64 max) where T : IRandom
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
		public static void NextNonZero<T>(this T random, Span<Int64> span, Int64 min, Int64 max) where T : IRandom
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
		public static void NextNonZero<T>(this T random, Span<UInt64> span) where T : IRandom
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
		public static void NextNonZero<T>(this T random, Span<UInt64> span, UInt64 max) where T : IRandom
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
		public static void NextNonZero<T>(this T random, Span<UInt64> span, UInt64 min, UInt64 max) where T : IRandom
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
		public static void NextNonZero<T>(this T random, Span<Single> span) where T : IRandom
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
		public static void NextNonZero<T>(this T random, Span<Single> span, Single max) where T : IRandom
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
		public static void NextNonZero<T>(this T random, Span<Single> span, Single min, Single max) where T : IRandom
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
		public static void NextNonZero<T>(this T random, Span<Double> span) where T : IRandom
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
		public static void NextNonZero<T>(this T random, Span<Double> span, Double max) where T : IRandom
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
		public static void NextNonZero<T>(this T random, Span<Double> span, Double min, Double max) where T : IRandom
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
		public static void NextNonZero<T>(this T random, Span<Decimal> span) where T : IRandom
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
		public static void NextNonZero<T>(this T random, Span<Decimal> span, Decimal max) where T : IRandom
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
		public static void NextNonZero<T>(this T random, Span<Decimal> span, Decimal min, Decimal max) where T : IRandom
		{
			for (Int32 i = 0; i < span.Length; i++)
			{
				span[i] = NextNonZeroDecimal(random, min, max);
			}
		}

		private const SByte PositiveSByteSign = 1;
		private const SByte NegativeSByteSign = -1;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static SByte NextSignSByte(Double chance = 0.5)
		{
			return NextSignSByte(Generator, chance);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static SByte NextSignSByte(this System.Random random, Double chance = 0.5)
		{
			return NextBoolean(random, chance) ? PositiveSByteSign : NegativeSByteSign;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static SByte NextSignSByte<T>(this T random, Double chance = 0.5) where T : IRandom
		{
			return NextBoolean(random, chance) ? PositiveSByteSign : NegativeSByteSign;
		}

		private const Int16 PositiveInt16Sign = 1;
		private const Int16 NegativeInt16Sign = -1;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int16 NextSignInt16(Double chance = 0.5)
		{
			return NextSignInt16(Generator, chance);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int16 NextSignInt16(this System.Random random, Double chance = 0.5)
		{
			return NextBoolean(random, chance) ? PositiveInt16Sign : NegativeInt16Sign;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int16 NextSignInt16<T>(this T random, Double chance = 0.5) where T : IRandom
		{
			return NextBoolean(random, chance) ? PositiveInt16Sign : NegativeInt16Sign;
		}

		private const Int32 PositiveInt32Sign = 1;
		private const Int32 NegativeInt32Sign = -1;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int32 NextSignInt32(Double chance = 0.5)
		{
			return NextSignInt32(Generator, chance);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int32 NextSignInt32(this System.Random random, Double chance = 0.5)
		{
			return NextBoolean(random, chance) ? PositiveInt32Sign : NegativeInt32Sign;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int32 NextSignInt32<T>(this T random, Double chance = 0.5) where T : IRandom
		{
			return NextBoolean(random, chance) ? PositiveInt32Sign : NegativeInt32Sign;
		}

		private const Int64 PositiveInt64Sign = 1;
		private const Int64 NegativeInt64Sign = -1;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int64 NextSignInt64(Double chance = 0.5)
		{
			return NextSignInt64(Generator, chance);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int64 NextSignInt64(this System.Random random, Double chance = 0.5)
		{
			return NextBoolean(random, chance) ? PositiveInt64Sign : NegativeInt64Sign;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int64 NextSignInt64<T>(this T random, Double chance = 0.5) where T : IRandom
		{
			return NextBoolean(random, chance) ? PositiveInt64Sign : NegativeInt64Sign;
		}

		private const Single PositiveSingleSign = 1;
		private const Single NegativeSingleSign = -1;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Single NextSignSingle(Double chance = 0.5)
		{
			return NextSignSingle(Generator, chance);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Single NextSignSingle(this System.Random random, Double chance = 0.5)
		{
			return NextBoolean(random, chance) ? PositiveSingleSign : NegativeSingleSign;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Single NextSignSingle<T>(this T random, Double chance = 0.5) where T : IRandom
		{
			return NextBoolean(random, chance) ? PositiveSingleSign : NegativeSingleSign;
		}

		private const Double PositiveDoubleSign = 1;
		private const Double NegativeDoubleSign = -1;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Double NextSignDouble(Double chance = 0.5)
		{
			return NextSignDouble(Generator, chance);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Double NextSignDouble(this System.Random random, Double chance = 0.5)
		{
			return NextBoolean(random, chance) ? PositiveDoubleSign : NegativeDoubleSign;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Double NextSignDouble<T>(this T random, Double chance = 0.5) where T : IRandom
		{
			return NextBoolean(random, chance) ? PositiveDoubleSign : NegativeDoubleSign;
		}

		private const Decimal PositiveDecimalSign = 1;
		private const Decimal NegativeDecimalSign = -1;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Decimal NextSignDecimal(Double chance = 0.5)
		{
			return NextSignDecimal(Generator, chance);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Decimal NextSignDecimal(this System.Random random, Double chance = 0.5)
		{
			return NextBoolean(random, chance) ? PositiveDecimalSign : NegativeDecimalSign;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Decimal NextSignDecimal<T>(this T random, Double chance = 0.5) where T : IRandom
		{
			return NextBoolean(random, chance) ? PositiveDecimalSign : NegativeDecimalSign;
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
		private static IEnumerable<T> Range<T, TRandom>(TRandom random, T min, T max, Func<TRandom, T, T, T> generator) where TRandom : IRandom
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
		private static IEnumerable<T> Range<T, TRandom>(TRandom random, T min, T max, Int32 count, Func<TRandom, T, T, T> generator) where TRandom : IRandom
		{
			if (generator is null)
			{
				throw new ArgumentNullException(nameof(generator));
			}

			return count <= 0 ? Enumerable.Empty<T>() : Range(random, min, max, generator).Take(count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<SByte> Range(SByte max)
		{
			return Range(Generator, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<SByte> Range(this System.Random random, SByte max)
		{
			return Range(random, SByte.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<SByte> Range<T>(this T random, SByte max) where T : IRandom
		{
			return Range(random, SByte.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<SByte> Range(SByte max, Int32 count)
		{
			return Range(Generator, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<SByte> Range(this System.Random random, SByte max, Int32 count)
		{
			return Range(random, SByte.MinValue, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<SByte> Range<T>(this T random, SByte max, Int32 count) where T : IRandom
		{
			return Range(random, SByte.MinValue, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<SByte> Range(SByte min, SByte max)
		{
			return Range(min, max, NextSByte);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<SByte> Range(this System.Random random, SByte min, SByte max)
		{
			return Range(random, min, max, NextSByte);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<SByte> Range<T>(this T random, SByte min, SByte max) where T : IRandom
		{
			return Range(random, min, max, NextSByte);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<SByte> Range(SByte min, SByte max, Int32 count)
		{
			return Range(min, max, count, NextSByte);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<SByte> Range(this System.Random random, SByte min, SByte max, Int32 count)
		{
			return Range(random, min, max, count, NextSByte);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<SByte> Range<T>(this T random, SByte min, SByte max, Int32 count) where T : IRandom
		{
			return Range(random, min, max, count, NextSByte);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Byte> Range(Byte max)
		{
			return Range(Generator, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Byte> Range(this System.Random random, Byte max)
		{
			return Range(random, Byte.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Byte> Range<T>(this T random, Byte max) where T : IRandom
		{
			return Range(random, Byte.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Byte> Range(Byte max, Int32 count)
		{
			return Range(Generator, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Byte> Range(this System.Random random, Byte max, Int32 count)
		{
			return Range(random, Byte.MinValue, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Byte> Range<T>(this T random, Byte max, Int32 count) where T : IRandom
		{
			return Range(random, Byte.MinValue, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Byte> Range(Byte min, Byte max)
		{
			return Range(min, max, NextByte);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Byte> Range(this System.Random random, Byte min, Byte max)
		{
			return Range(random, min, max, NextByte);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Byte> Range<T>(this T random, Byte min, Byte max) where T : IRandom
		{
			return Range(random, min, max, NextByte);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Byte> Range(Byte min, Byte max, Int32 count)
		{
			return Range(min, max, count, NextByte);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Byte> Range(this System.Random random, Byte min, Byte max, Int32 count)
		{
			return Range(random, min, max, count, NextByte);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Byte> Range<T>(this T random, Byte min, Byte max, Int32 count) where T : IRandom
		{
			return Range(random, min, max, count, NextByte);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int16> Range(Int16 max)
		{
			return Range(Generator, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int16> Range(this System.Random random, Int16 max)
		{
			return Range(random, Int16.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int16> Range<T>(this T random, Int16 max) where T : IRandom
		{
			return Range(random, Int16.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int16> Range(Int16 max, Int32 count)
		{
			return Range(Generator, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int16> Range(this System.Random random, Int16 max, Int32 count)
		{
			return Range(random, Int16.MinValue, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int16> Range<T>(this T random, Int16 max, Int32 count) where T : IRandom
		{
			return Range(random, Int16.MinValue, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int16> Range(Int16 min, Int16 max)
		{
			return Range(min, max, NextInt16);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int16> Range(this System.Random random, Int16 min, Int16 max)
		{
			return Range(random, min, max, NextInt16);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int16> Range<T>(this T random, Int16 min, Int16 max) where T : IRandom
		{
			return Range(random, min, max, NextInt16);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int16> Range(Int16 min, Int16 max, Int32 count)
		{
			return Range(min, max, count, NextInt16);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int16> Range(this System.Random random, Int16 min, Int16 max, Int32 count)
		{
			return Range(random, min, max, count, NextInt16);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int16> Range<T>(this T random, Int16 min, Int16 max, Int32 count) where T : IRandom
		{
			return Range(random, min, max, count, NextInt16);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt16> Range(UInt16 max)
		{
			return Range(Generator, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt16> Range(this System.Random random, UInt16 max)
		{
			return Range(random, UInt16.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt16> Range<T>(this T random, UInt16 max) where T : IRandom
		{
			return Range(random, UInt16.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt16> Range(UInt16 max, Int32 count)
		{
			return Range(Generator, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt16> Range(this System.Random random, UInt16 max, Int32 count)
		{
			return Range(random, UInt16.MinValue, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt16> Range<T>(this T random, UInt16 max, Int32 count) where T : IRandom
		{
			return Range(random, UInt16.MinValue, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt16> Range(UInt16 min, UInt16 max)
		{
			return Range(min, max, NextUInt16);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt16> Range(this System.Random random, UInt16 min, UInt16 max)
		{
			return Range(random, min, max, NextUInt16);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt16> Range<T>(this T random, UInt16 min, UInt16 max) where T : IRandom
		{
			return Range(random, min, max, NextUInt16);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt16> Range(UInt16 min, UInt16 max, Int32 count)
		{
			return Range(min, max, count, NextUInt16);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt16> Range(this System.Random random, UInt16 min, UInt16 max, Int32 count)
		{
			return Range(random, min, max, count, NextUInt16);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt16> Range<T>(this T random, UInt16 min, UInt16 max, Int32 count) where T : IRandom
		{
			return Range(random, min, max, count, NextUInt16);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int32> Range()
		{
			return Range(Generator);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int32> Range(this System.Random random)
		{
			return Range(random, Int32.MinValue, Int32.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int32> Range<T>(this T random) where T : IRandom
		{
			return Range(random, Int32.MinValue, Int32.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int32> Range(Int32 max)
		{
			return Range(Generator, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int32> Range(this System.Random random, Int32 max)
		{
			return Range(random, Int32.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int32> Range<T>(this T random, Int32 max) where T : IRandom
		{
			return Range(random, Int32.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int32> Range(Int32 min, Int32 max)
		{
			return Range(min, max, NextInt32);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int32> Range(this System.Random random, Int32 min, Int32 max)
		{
			return Range(random, min, max, NextInt32);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int32> Range<T>(this T random, Int32 min, Int32 max) where T : IRandom
		{
			return Range(random, min, max, NextInt32);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int32> Range(Int32 min, Int32 max, Int32 count)
		{
			return Range(min, max, count, NextInt32);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int32> Range(this System.Random random, Int32 min, Int32 max, Int32 count)
		{
			return Range(random, min, max, count, NextInt32);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int32> Range<T>(this T random, Int32 min, Int32 max, Int32 count) where T : IRandom
		{
			return Range(random, min, max, count, NextInt32);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt32> Range(UInt32 max)
		{
			return Range(Generator, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt32> Range(this System.Random random, UInt32 max)
		{
			return Range(random, UInt32.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt32> Range<T>(this T random, UInt32 max) where T : IRandom
		{
			return Range(random, UInt32.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt32> Range(UInt32 max, Int32 count)
		{
			return Range(Generator, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt32> Range(this System.Random random, UInt32 max, Int32 count)
		{
			return Range(random, UInt32.MinValue, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt32> Range<T>(this T random, UInt32 max, Int32 count) where T : IRandom
		{
			return Range(random, UInt32.MinValue, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt32> Range(UInt32 min, UInt32 max)
		{
			return Range(min, max, NextUInt32);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt32> Range(this System.Random random, UInt32 min, UInt32 max)
		{
			return Range(random, min, max, NextUInt32);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt32> Range<T>(this T random, UInt32 min, UInt32 max) where T : IRandom
		{
			return Range(random, min, max, NextUInt32);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt32> Range(UInt32 min, UInt32 max, Int32 count)
		{
			return Range(min, max, count, NextUInt32);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt32> Range(this System.Random random, UInt32 min, UInt32 max, Int32 count)
		{
			return Range(random, min, max, count, NextUInt32);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt32> Range<T>(this T random, UInt32 min, UInt32 max, Int32 count) where T : IRandom
		{
			return Range(random, min, max, count, NextUInt32);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int64> Range(Int64 max)
		{
			return Range(Generator, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int64> Range(this System.Random random, Int64 max)
		{
			return Range(random, Int64.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int64> Range<T>(this T random, Int64 max) where T : IRandom
		{
			return Range(random, Int64.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int64> Range(Int64 max, Int32 count)
		{
			return Range(Generator, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int64> Range(this System.Random random, Int64 max, Int32 count)
		{
			return Range(random, Int64.MinValue, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int64> Range<T>(this T random, Int64 max, Int32 count) where T : IRandom
		{
			return Range(random, Int64.MinValue, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int64> Range(Int64 min, Int64 max)
		{
			return Range(min, max, NextInt64);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int64> Range(this System.Random random, Int64 min, Int64 max)
		{
			return Range(random, min, max, NextInt64);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int64> Range<T>(this T random, Int64 min, Int64 max) where T : IRandom
		{
			return Range(random, min, max, NextInt64);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int64> Range(Int64 min, Int64 max, Int32 count)
		{
			return Range(min, max, count, NextInt64);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int64> Range(this System.Random random, Int64 min, Int64 max, Int32 count)
		{
			return Range(random, min, max, count, NextInt64);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int64> Range<T>(this T random, Int64 min, Int64 max, Int32 count) where T : IRandom
		{
			return Range(random, min, max, count, NextInt64);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt64> Range(UInt64 max)
		{
			return Range(Generator, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt64> Range(this System.Random random, UInt64 max)
		{
			return Range(random, UInt64.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt64> Range<T>(this T random, UInt64 max) where T : IRandom
		{
			return Range(random, UInt64.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt64> Range(UInt64 max, Int32 count)
		{
			return Range(Generator, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt64> Range(this System.Random random, UInt64 max, Int32 count)
		{
			return Range(random, UInt64.MinValue, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt64> Range<T>(this T random, UInt64 max, Int32 count) where T : IRandom
		{
			return Range(random, UInt64.MinValue, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt64> Range(UInt64 min, UInt64 max)
		{
			return Range(min, max, NextUInt64);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt64> Range(this System.Random random, UInt64 min, UInt64 max)
		{
			return Range(random, min, max, NextUInt64);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt64> Range<T>(this T random, UInt64 min, UInt64 max) where T : IRandom
		{
			return Range(random, min, max, NextUInt64);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt64> Range(UInt64 min, UInt64 max, Int32 count)
		{
			return Range(min, max, count, NextUInt64);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt64> Range(this System.Random random, UInt64 min, UInt64 max, Int32 count)
		{
			return Range(random, min, max, count, NextUInt64);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt64> Range<T>(this T random, UInt64 min, UInt64 max, Int32 count) where T : IRandom
		{
			return Range(random, min, max, count, NextUInt64);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Single> Range(Single max)
		{
			return Range(Generator, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Single> Range(this System.Random random, Single max)
		{
			return Range(random, Single.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Single> Range<T>(this T random, Single max) where T : IRandom
		{
			return Range(random, Single.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Single> Range(Single max, Int32 count)
		{
			return Range(Generator, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Single> Range(this System.Random random, Single max, Int32 count)
		{
			return Range(random, Single.MinValue, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Single> Range<T>(this T random, Single max, Int32 count) where T : IRandom
		{
			return Range(random, Single.MinValue, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Single> Range(Single min, Single max)
		{
			return Range(min, max, NextSingle);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Single> Range(this System.Random random, Single min, Single max)
		{
			return Range(random, min, max, NextSingle);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Single> Range<T>(this T random, Single min, Single max) where T : IRandom
		{
			return Range(random, min, max, NextSingle);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Single> Range(Single min, Single max, Int32 count)
		{
			return Range(min, max, count, NextSingle);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Single> Range(this System.Random random, Single min, Single max, Int32 count)
		{
			return Range(random, min, max, count, NextSingle);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Single> Range<T>(this T random, Single min, Single max, Int32 count) where T : IRandom
		{
			return Range(random, min, max, count, NextSingle);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Double> Range(Double max)
		{
			return Range(Generator, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Double> Range(this System.Random random, Double max)
		{
			return Range(random, Double.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Double> Range<T>(this T random, Double max) where T : IRandom
		{
			return Range(random, Double.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Double> Range(Double max, Int32 count)
		{
			return Range(Generator, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Double> Range(this System.Random random, Double max, Int32 count)
		{
			return Range(random, Double.MinValue, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Double> Range<T>(this T random, Double max, Int32 count) where T : IRandom
		{
			return Range(random, Double.MinValue, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Double> Range(Double min, Double max)
		{
			return Range(min, max, NextDouble);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Double> Range(this System.Random random, Double min, Double max)
		{
			return Range(random, min, max, NextDouble);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Double> Range<T>(this T random, Double min, Double max) where T : IRandom
		{
			return Range(random, min, max, NextDouble);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Double> Range(Double min, Double max, Int32 count)
		{
			return Range(min, max, count, NextDouble);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Double> Range(this System.Random random, Double min, Double max, Int32 count)
		{
			return Range(random, min, max, count, NextDouble);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Double> Range<T>(this T random, Double min, Double max, Int32 count) where T : IRandom
		{
			return Range(random, min, max, count, NextDouble);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Decimal> Range(Decimal max)
		{
			return Range(Generator, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Decimal> Range(this System.Random random, Decimal max)
		{
			return Range(random, Decimal.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Decimal> Range<T>(this T random, Decimal max) where T : IRandom
		{
			return Range(random, Decimal.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Decimal> Range(Decimal max, Int32 count)
		{
			return Range(Generator, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Decimal> Range(this System.Random random, Decimal max, Int32 count)
		{
			return Range(random, Decimal.MinValue, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Decimal> Range<T>(this T random, Decimal max, Int32 count) where T : IRandom
		{
			return Range(random, Decimal.MinValue, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Decimal> Range(Decimal min, Decimal max)
		{
			return Range(min, max, NextDecimal);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Decimal> Range(this System.Random random, Decimal min, Decimal max)
		{
			return Range(random, min, max, NextDecimal);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Decimal> Range<T>(this T random, Decimal min, Decimal max) where T : IRandom
		{
			return Range(random, min, max, NextDecimal);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Decimal> Range(Decimal min, Decimal max, Int32 count)
		{
			return Range(min, max, count, NextDecimal);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Decimal> Range(this System.Random random, Decimal min, Decimal max, Int32 count)
		{
			return Range(random, min, max, count, NextDecimal);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Decimal> Range<T>(this T random, Decimal min, Decimal max, Int32 count) where T : IRandom
		{
			return Range(random, min, max, count, NextDecimal);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<TimeSpan> Range(TimeSpan max)
		{
			return Range(Generator, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<TimeSpan> Range(this System.Random random, TimeSpan max)
		{
			return Range(random, TimeSpan.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<TimeSpan> Range<T>(this T random, TimeSpan max) where T : IRandom
		{
			return Range(random, TimeSpan.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<TimeSpan> Range(TimeSpan max, Int32 count)
		{
			return Range(Generator, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<TimeSpan> Range(this System.Random random, TimeSpan max, Int32 count)
		{
			return Range(random, TimeSpan.MinValue, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<TimeSpan> Range<T>(this T random, TimeSpan max, Int32 count) where T : IRandom
		{
			return Range(random, TimeSpan.MinValue, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<TimeSpan> Range(TimeSpan min, TimeSpan max)
		{
			return Range(min, max, NextTimeSpan);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<TimeSpan> Range(this System.Random random, TimeSpan min, TimeSpan max)
		{
			return Range(random, min, max, NextTimeSpan);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<TimeSpan> Range<T>(this T random, TimeSpan min, TimeSpan max) where T : IRandom
		{
			return Range(random, min, max, NextTimeSpan);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<TimeSpan> Range(TimeSpan min, TimeSpan max, Int32 count)
		{
			return Range(min, max, count, NextTimeSpan);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<TimeSpan> Range(this System.Random random, TimeSpan min, TimeSpan max, Int32 count)
		{
			return Range(random, min, max, count, NextTimeSpan);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<TimeSpan> Range<T>(this T random, TimeSpan min, TimeSpan max, Int32 count) where T : IRandom
		{
			return Range(random, min, max, count, NextTimeSpan);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<DateTime> Range(DateTime max)
		{
			return Range(Generator, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<DateTime> Range(this System.Random random, DateTime max)
		{
			return Range(random, DateTime.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<DateTime> Range<T>(this T random, DateTime max) where T : IRandom
		{
			return Range(random, DateTime.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<DateTime> Range(DateTime max, Int32 count)
		{
			return Range(Generator, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<DateTime> Range(this System.Random random, DateTime max, Int32 count)
		{
			return Range(random, DateTime.MinValue, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<DateTime> Range<T>(this T random, DateTime max, Int32 count) where T : IRandom
		{
			return Range(random, DateTime.MinValue, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<DateTime> Range(DateTime min, DateTime max)
		{
			return Range(min, max, NextDateTime);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<DateTime> Range(this System.Random random, DateTime min, DateTime max)
		{
			return Range(random, min, max, NextDateTime);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<DateTime> Range<T>(this T random, DateTime min, DateTime max) where T : IRandom
		{
			return Range(random, min, max, NextDateTime);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<DateTime> Range(DateTime min, DateTime max, Int32 count)
		{
			return Range(min, max, count, NextDateTime);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<DateTime> Range(this System.Random random, DateTime min, DateTime max, Int32 count)
		{
			return Range(random, min, max, count, NextDateTime);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<DateTime> Range<T>(this T random, DateTime min, DateTime max, Int32 count) where T : IRandom
		{
			return Range(random, min, max, count, NextDateTime);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<SByte> NonNegativeRange(SByte max)
		{
			return NonNegativeRange(Generator, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<SByte> NonNegativeRange(this System.Random random, SByte max)
		{
			return NonNegativeRange(random, default(SByte), max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<SByte> NonNegativeRange<T>(this T random, SByte max) where T : IRandom
		{
			return NonNegativeRange(random, default(SByte), max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<SByte> NonNegativeRange(SByte max, Int32 count)
		{
			return NonNegativeRange(Generator, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<SByte> NonNegativeRange(this System.Random random, SByte max, Int32 count)
		{
			return NonNegativeRange(random, default(SByte), max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<SByte> NonNegativeRange<T>(this T random, SByte max, Int32 count) where T : IRandom
		{
			return NonNegativeRange(random, default(SByte), max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<SByte> NonNegativeRange(SByte min, SByte max)
		{
			return Range(min, max, NextNonNegativeSByte);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<SByte> NonNegativeRange(this System.Random random, SByte min, SByte max)
		{
			return Range(random, min, max, NextNonNegativeSByte);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<SByte> NonNegativeRange<T>(this T random, SByte min, SByte max) where T : IRandom
		{
			return Range(random, min, max, NextNonNegativeSByte);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<SByte> NonNegativeRange(SByte min, SByte max, Int32 count)
		{
			return Range(min, max, count, NextNonNegativeSByte);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<SByte> NonNegativeRange(this System.Random random, SByte min, SByte max, Int32 count)
		{
			return Range(random, min, max, count, NextNonNegativeSByte);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<SByte> NonNegativeRange<T>(this T random, SByte min, SByte max, Int32 count) where T : IRandom
		{
			return Range(random, min, max, count, NextNonNegativeSByte);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Byte> NonNegativeRange(Byte max)
		{
			return NonNegativeRange(Generator, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Byte> NonNegativeRange(this System.Random random, Byte max)
		{
			return NonNegativeRange(random, default(Byte), max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Byte> NonNegativeRange<T>(this T random, Byte max) where T : IRandom
		{
			return NonNegativeRange(random, default(Byte), max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Byte> NonNegativeRange(Byte max, Int32 count)
		{
			return NonNegativeRange(Generator, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Byte> NonNegativeRange(this System.Random random, Byte max, Int32 count)
		{
			return NonNegativeRange(random, default(Byte), max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Byte> NonNegativeRange<T>(this T random, Byte max, Int32 count) where T : IRandom
		{
			return NonNegativeRange(random, default(Byte), max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Byte> NonNegativeRange(Byte min, Byte max)
		{
			return Range(min, max, NextNonNegativeByte);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Byte> NonNegativeRange(this System.Random random, Byte min, Byte max)
		{
			return Range(random, min, max, NextNonNegativeByte);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Byte> NonNegativeRange<T>(this T random, Byte min, Byte max) where T : IRandom
		{
			return Range(random, min, max, NextNonNegativeByte);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Byte> NonNegativeRange(Byte min, Byte max, Int32 count)
		{
			return Range(min, max, count, NextNonNegativeByte);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Byte> NonNegativeRange(this System.Random random, Byte min, Byte max, Int32 count)
		{
			return Range(random, min, max, count, NextNonNegativeByte);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Byte> NonNegativeRange<T>(this T random, Byte min, Byte max, Int32 count) where T : IRandom
		{
			return Range(random, min, max, count, NextNonNegativeByte);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int16> NonNegativeRange(Int16 max)
		{
			return NonNegativeRange(Generator, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int16> NonNegativeRange(this System.Random random, Int16 max)
		{
			return NonNegativeRange(random, default(Int16), max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int16> NonNegativeRange<T>(this T random, Int16 max) where T : IRandom
		{
			return NonNegativeRange(random, default(Int16), max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int16> NonNegativeRange(Int16 max, Int32 count)
		{
			return NonNegativeRange(Generator, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int16> NonNegativeRange(this System.Random random, Int16 max, Int32 count)
		{
			return NonNegativeRange(random, default(Int16), max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int16> NonNegativeRange<T>(this T random, Int16 max, Int32 count) where T : IRandom
		{
			return NonNegativeRange(random, default(Int16), max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int16> NonNegativeRange(Int16 min, Int16 max)
		{
			return Range(min, max, NextNonNegativeInt16);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int16> NonNegativeRange(this System.Random random, Int16 min, Int16 max)
		{
			return Range(random, min, max, NextNonNegativeInt16);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int16> NonNegativeRange<T>(this T random, Int16 min, Int16 max) where T : IRandom
		{
			return Range(random, min, max, NextNonNegativeInt16);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int16> NonNegativeRange(Int16 min, Int16 max, Int32 count)
		{
			return Range(min, max, count, NextNonNegativeInt16);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int16> NonNegativeRange(this System.Random random, Int16 min, Int16 max, Int32 count)
		{
			return Range(random, min, max, count, NextNonNegativeInt16);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int16> NonNegativeRange<T>(this T random, Int16 min, Int16 max, Int32 count) where T : IRandom
		{
			return Range(random, min, max, count, NextNonNegativeInt16);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt16> NonNegativeRange(UInt16 max)
		{
			return NonNegativeRange(Generator, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt16> NonNegativeRange(this System.Random random, UInt16 max)
		{
			return NonNegativeRange(random, default(UInt16), max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt16> NonNegativeRange<T>(this T random, UInt16 max) where T : IRandom
		{
			return NonNegativeRange(random, default(UInt16), max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt16> NonNegativeRange(UInt16 max, Int32 count)
		{
			return NonNegativeRange(Generator, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt16> NonNegativeRange(this System.Random random, UInt16 max, Int32 count)
		{
			return NonNegativeRange(random, default(UInt16), max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt16> NonNegativeRange<T>(this T random, UInt16 max, Int32 count) where T : IRandom
		{
			return NonNegativeRange(random, default(UInt16), max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt16> NonNegativeRange(UInt16 min, UInt16 max)
		{
			return Range(min, max, NextNonNegativeUInt16);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt16> NonNegativeRange(this System.Random random, UInt16 min, UInt16 max)
		{
			return Range(random, min, max, NextNonNegativeUInt16);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt16> NonNegativeRange<T>(this T random, UInt16 min, UInt16 max) where T : IRandom
		{
			return Range(random, min, max, NextNonNegativeUInt16);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt16> NonNegativeRange(UInt16 min, UInt16 max, Int32 count)
		{
			return Range(min, max, count, NextNonNegativeUInt16);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt16> NonNegativeRange(this System.Random random, UInt16 min, UInt16 max, Int32 count)
		{
			return Range(random, min, max, count, NextNonNegativeUInt16);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt16> NonNegativeRange<T>(this T random, UInt16 min, UInt16 max, Int32 count) where T : IRandom
		{
			return Range(random, min, max, count, NextNonNegativeUInt16);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int32> NonNegativeRange()
		{
			return NonNegativeRange(Generator);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int32> NonNegativeRange(this System.Random random)
		{
			return NonNegativeRange(random, Int32.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int32> NonNegativeRange<T>(this T random) where T : IRandom
		{
			return NonNegativeRange(random, Int32.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int32> NonNegativeRange(Int32 max)
		{
			return NonNegativeRange(Generator, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int32> NonNegativeRange(this System.Random random, Int32 max)
		{
			return NonNegativeRange(random, default(Int32), max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int32> NonNegativeRange<T>(this T random, Int32 max) where T : IRandom
		{
			return NonNegativeRange(random, default(Int32), max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int32> NonNegativeRange(Int32 min, Int32 max)
		{
			return Range(min, max, NextNonNegativeInt32);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int32> NonNegativeRange(this System.Random random, Int32 min, Int32 max)
		{
			return Range(random, min, max, NextNonNegativeInt32);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int32> NonNegativeRange<T>(this T random, Int32 min, Int32 max) where T : IRandom
		{
			return Range(random, min, max, NextNonNegativeInt32);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int32> NonNegativeRange(Int32 min, Int32 max, Int32 count)
		{
			return Range(min, max, count, NextNonNegativeInt32);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int32> NonNegativeRange(this System.Random random, Int32 min, Int32 max, Int32 count)
		{
			return Range(random, min, max, count, NextNonNegativeInt32);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int32> NonNegativeRange<T>(this T random, Int32 min, Int32 max, Int32 count) where T : IRandom
		{
			return Range(random, min, max, count, NextNonNegativeInt32);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt32> NonNegativeRange(UInt32 max)
		{
			return NonNegativeRange(Generator, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt32> NonNegativeRange(this System.Random random, UInt32 max)
		{
			return NonNegativeRange(random, default, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt32> NonNegativeRange<T>(this T random, UInt32 max) where T : IRandom
		{
			return NonNegativeRange(random, default, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt32> NonNegativeRange(UInt32 max, Int32 count)
		{
			return NonNegativeRange(Generator, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt32> NonNegativeRange(this System.Random random, UInt32 max, Int32 count)
		{
			return NonNegativeRange(random, default, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt32> NonNegativeRange<T>(this T random, UInt32 max, Int32 count) where T : IRandom
		{
			return NonNegativeRange(random, default, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt32> NonNegativeRange(UInt32 min, UInt32 max)
		{
			return Range(min, max, NextNonNegativeUInt32);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt32> NonNegativeRange(this System.Random random, UInt32 min, UInt32 max)
		{
			return Range(random, min, max, NextNonNegativeUInt32);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt32> NonNegativeRange<T>(this T random, UInt32 min, UInt32 max) where T : IRandom
		{
			return Range(random, min, max, NextNonNegativeUInt32);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt32> NonNegativeRange(UInt32 min, UInt32 max, Int32 count)
		{
			return Range(min, max, count, NextNonNegativeUInt32);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt32> NonNegativeRange(this System.Random random, UInt32 min, UInt32 max, Int32 count)
		{
			return Range(random, min, max, count, NextNonNegativeUInt32);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt32> NonNegativeRange<T>(this T random, UInt32 min, UInt32 max, Int32 count) where T : IRandom
		{
			return Range(random, min, max, count, NextNonNegativeUInt32);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int64> NonNegativeRange(Int64 max)
		{
			return NonNegativeRange(Generator, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int64> NonNegativeRange(this System.Random random, Int64 max)
		{
			return NonNegativeRange(random, default, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int64> NonNegativeRange<T>(this T random, Int64 max) where T : IRandom
		{
			return NonNegativeRange(random, default, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int64> NonNegativeRange(Int64 max, Int32 count)
		{
			return NonNegativeRange(Generator, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int64> NonNegativeRange(this System.Random random, Int64 max, Int32 count)
		{
			return NonNegativeRange(random, default, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int64> NonNegativeRange<T>(this T random, Int64 max, Int32 count) where T : IRandom
		{
			return NonNegativeRange(random, default, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int64> NonNegativeRange(Int64 min, Int64 max)
		{
			return Range(min, max, NextNonNegativeInt64);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int64> NonNegativeRange(this System.Random random, Int64 min, Int64 max)
		{
			return Range(random, min, max, NextNonNegativeInt64);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int64> NonNegativeRange<T>(this T random, Int64 min, Int64 max) where T : IRandom
		{
			return Range(random, min, max, NextNonNegativeInt64);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int64> NonNegativeRange(Int64 min, Int64 max, Int32 count)
		{
			return Range(min, max, count, NextNonNegativeInt64);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int64> NonNegativeRange(this System.Random random, Int64 min, Int64 max, Int32 count)
		{
			return Range(random, min, max, count, NextNonNegativeInt64);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int64> NonNegativeRange<T>(this T random, Int64 min, Int64 max, Int32 count) where T : IRandom
		{
			return Range(random, min, max, count, NextNonNegativeInt64);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt64> NonNegativeRange(UInt64 max)
		{
			return NonNegativeRange(Generator, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt64> NonNegativeRange(this System.Random random, UInt64 max)
		{
			return NonNegativeRange(random, default, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt64> NonNegativeRange<T>(this T random, UInt64 max) where T : IRandom
		{
			return NonNegativeRange(random, default, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt64> NonNegativeRange(UInt64 max, Int32 count)
		{
			return NonNegativeRange(Generator, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt64> NonNegativeRange(this System.Random random, UInt64 max, Int32 count)
		{
			return NonNegativeRange(random, default, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt64> NonNegativeRange<T>(this T random, UInt64 max, Int32 count) where T : IRandom
		{
			return NonNegativeRange(random, default, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt64> NonNegativeRange(UInt64 min, UInt64 max)
		{
			return Range(min, max, NextNonNegativeUInt64);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt64> NonNegativeRange(this System.Random random, UInt64 min, UInt64 max)
		{
			return Range(random, min, max, NextNonNegativeUInt64);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt64> NonNegativeRange<T>(this T random, UInt64 min, UInt64 max) where T : IRandom
		{
			return Range(random, min, max, NextNonNegativeUInt64);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt64> NonNegativeRange(UInt64 min, UInt64 max, Int32 count)
		{
			return Range(min, max, count, NextNonNegativeUInt64);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt64> NonNegativeRange(this System.Random random, UInt64 min, UInt64 max, Int32 count)
		{
			return Range(random, min, max, count, NextNonNegativeUInt64);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt64> NonNegativeRange<T>(this T random, UInt64 min, UInt64 max, Int32 count) where T : IRandom
		{
			return Range(random, min, max, count, NextNonNegativeUInt64);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Single> NonNegativeRange(Single max)
		{
			return NonNegativeRange(Generator, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Single> NonNegativeRange(this System.Random random, Single max)
		{
			return NonNegativeRange(random, default, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Single> NonNegativeRange<T>(this T random, Single max) where T : IRandom
		{
			return NonNegativeRange(random, default, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Single> NonNegativeRange(Single max, Int32 count)
		{
			return NonNegativeRange(Generator, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Single> NonNegativeRange(this System.Random random, Single max, Int32 count)
		{
			return NonNegativeRange(random, default, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Single> NonNegativeRange<T>(this T random, Single max, Int32 count) where T : IRandom
		{
			return NonNegativeRange(random, default, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Single> NonNegativeRange(Single min, Single max)
		{
			return Range(min, max, NextNonNegativeSingle);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Single> NonNegativeRange(this System.Random random, Single min, Single max)
		{
			return Range(random, min, max, NextNonNegativeSingle);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Single> NonNegativeRange<T>(this T random, Single min, Single max) where T : IRandom
		{
			return Range(random, min, max, NextNonNegativeSingle);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Single> NonNegativeRange(Single min, Single max, Int32 count)
		{
			return Range(min, max, count, NextNonNegativeSingle);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Single> NonNegativeRange(this System.Random random, Single min, Single max, Int32 count)
		{
			return Range(random, min, max, count, NextNonNegativeSingle);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Single> NonNegativeRange<T>(this T random, Single min, Single max, Int32 count) where T : IRandom
		{
			return Range(random, min, max, count, NextNonNegativeSingle);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Double> NonNegativeRange(Double max)
		{
			return NonNegativeRange(Generator, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Double> NonNegativeRange(this System.Random random, Double max)
		{
			return NonNegativeRange(random, default, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Double> NonNegativeRange<T>(this T random, Double max) where T : IRandom
		{
			return NonNegativeRange(random, default, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Double> NonNegativeRange(Double max, Int32 count)
		{
			return NonNegativeRange(Generator, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Double> NonNegativeRange(this System.Random random, Double max, Int32 count)
		{
			return NonNegativeRange(random, default, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Double> NonNegativeRange<T>(this T random, Double max, Int32 count) where T : IRandom
		{
			return NonNegativeRange(random, default, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Double> NonNegativeRange(Double min, Double max)
		{
			return Range(min, max, NextNonNegativeDouble);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Double> NonNegativeRange(this System.Random random, Double min, Double max)
		{
			return Range(random, min, max, NextNonNegativeDouble);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Double> NonNegativeRange<T>(this T random, Double min, Double max) where T : IRandom
		{
			return Range(random, min, max, NextNonNegativeDouble);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Double> NonNegativeRange(Double min, Double max, Int32 count)
		{
			return Range(min, max, count, NextNonNegativeDouble);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Double> NonNegativeRange(this System.Random random, Double min, Double max, Int32 count)
		{
			return Range(random, min, max, count, NextNonNegativeDouble);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Double> NonNegativeRange<T>(this T random, Double min, Double max, Int32 count) where T : IRandom
		{
			return Range(random, min, max, count, NextNonNegativeDouble);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Decimal> NonNegativeRange(Decimal max)
		{
			return NonNegativeRange(Generator, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Decimal> NonNegativeRange(this System.Random random, Decimal max)
		{
			return NonNegativeRange(random, default, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Decimal> NonNegativeRange<T>(this T random, Decimal max) where T : IRandom
		{
			return NonNegativeRange(random, default, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Decimal> NonNegativeRange(Decimal max, Int32 count)
		{
			return NonNegativeRange(Generator, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Decimal> NonNegativeRange(this System.Random random, Decimal max, Int32 count)
		{
			return NonNegativeRange(random, default, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Decimal> NonNegativeRange<T>(this T random, Decimal max, Int32 count) where T : IRandom
		{
			return NonNegativeRange(random, default, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Decimal> NonNegativeRange(Decimal min, Decimal max)
		{
			return Range(min, max, NextNonNegativeDecimal);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Decimal> NonNegativeRange(this System.Random random, Decimal min, Decimal max)
		{
			return Range(random, min, max, NextNonNegativeDecimal);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Decimal> NonNegativeRange<T>(this T random, Decimal min, Decimal max) where T : IRandom
		{
			return Range(random, min, max, NextNonNegativeDecimal);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Decimal> NonNegativeRange(Decimal min, Decimal max, Int32 count)
		{
			return Range(min, max, count, NextNonNegativeDecimal);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Decimal> NonNegativeRange(this System.Random random, Decimal min, Decimal max, Int32 count)
		{
			return Range(random, min, max, count, NextNonNegativeDecimal);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Decimal> NonNegativeRange<T>(this T random, Decimal min, Decimal max, Int32 count) where T : IRandom
		{
			return Range(random, min, max, count, NextNonNegativeDecimal);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<SByte> NonZeroRange(SByte max)
		{
			return NonZeroRange(SByte.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<SByte> NonZeroRange(this System.Random random, SByte max)
		{
			return NonZeroRange(random, SByte.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<SByte> NonZeroRange<T>(this T random, SByte max) where T : IRandom
		{
			return NonZeroRange(random, SByte.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<SByte> NonZeroRange(SByte min, SByte max)
		{
			return Range(min, max, NextNonZeroSByte);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<SByte> NonZeroRange(this System.Random random, SByte min, SByte max)
		{
			return Range(random, min, max, NextNonZeroSByte);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<SByte> NonZeroRange<T>(this T random, SByte min, SByte max) where T : IRandom
		{
			return Range(random, min, max, NextNonZeroSByte);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<SByte> NonZeroRange(SByte min, SByte max, Int32 count)
		{
			return Range(min, max, count, NextNonZeroSByte);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<SByte> NonZeroRange(this System.Random random, SByte min, SByte max, Int32 count)
		{
			return Range(random, min, max, count, NextNonZeroSByte);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<SByte> NonZeroRange<T>(this T random, SByte min, SByte max, Int32 count) where T : IRandom
		{
			return Range(random, min, max, count, NextNonZeroSByte);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Byte> NonZeroRange(Byte max)
		{
			return NonZeroRange(Byte.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Byte> NonZeroRange(this System.Random random, Byte max)
		{
			return NonZeroRange(random, Byte.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Byte> NonZeroRange<T>(this T random, Byte max) where T : IRandom
		{
			return NonZeroRange(random, Byte.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Byte> NonZeroRange(Byte min, Byte max)
		{
			return Range(min, max, NextNonZeroByte);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Byte> NonZeroRange(this System.Random random, Byte min, Byte max)
		{
			return Range(random, min, max, NextNonZeroByte);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Byte> NonZeroRange<T>(this T random, Byte min, Byte max) where T : IRandom
		{
			return Range(random, min, max, NextNonZeroByte);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Byte> NonZeroRange(Byte min, Byte max, Int32 count)
		{
			return Range(min, max, count, NextNonZeroByte);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Byte> NonZeroRange(this System.Random random, Byte min, Byte max, Int32 count)
		{
			return Range(random, min, max, count, NextNonZeroByte);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Byte> NonZeroRange<T>(this T random, Byte min, Byte max, Int32 count) where T : IRandom
		{
			return Range(random, min, max, count, NextNonZeroByte);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int16> NonZeroRange(Int16 max)
		{
			return NonZeroRange(Int16.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int16> NonZeroRange(this System.Random random, Int16 max)
		{
			return NonZeroRange(random, Int16.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int16> NonZeroRange<T>(this T random, Int16 max) where T : IRandom
		{
			return NonZeroRange(random, Int16.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int16> NonZeroRange(Int16 min, Int16 max)
		{
			return Range(min, max, NextNonZeroInt16);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int16> NonZeroRange(this System.Random random, Int16 min, Int16 max)
		{
			return Range(random, min, max, NextNonZeroInt16);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int16> NonZeroRange<T>(this T random, Int16 min, Int16 max) where T : IRandom
		{
			return Range(random, min, max, NextNonZeroInt16);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int16> NonZeroRange(Int16 min, Int16 max, Int32 count)
		{
			return Range(min, max, count, NextNonZeroInt16);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int16> NonZeroRange(this System.Random random, Int16 min, Int16 max, Int32 count)
		{
			return Range(random, min, max, count, NextNonZeroInt16);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int16> NonZeroRange<T>(this T random, Int16 min, Int16 max, Int32 count) where T : IRandom
		{
			return Range(random, min, max, count, NextNonZeroInt16);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt16> NonZeroRange(UInt16 max)
		{
			return NonZeroRange(UInt16.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt16> NonZeroRange(this System.Random random, UInt16 max)
		{
			return NonZeroRange(random, UInt16.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt16> NonZeroRange<T>(this T random, UInt16 max) where T : IRandom
		{
			return NonZeroRange(random, UInt16.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt16> NonZeroRange(UInt16 min, UInt16 max)
		{
			return Range(min, max, NextNonZeroUInt16);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt16> NonZeroRange(this System.Random random, UInt16 min, UInt16 max)
		{
			return Range(random, min, max, NextNonZeroUInt16);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt16> NonZeroRange<T>(this T random, UInt16 min, UInt16 max) where T : IRandom
		{
			return Range(random, min, max, NextNonZeroUInt16);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt16> NonZeroRange(UInt16 min, UInt16 max, Int32 count)
		{
			return Range(min, max, count, NextNonZeroUInt16);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt16> NonZeroRange(this System.Random random, UInt16 min, UInt16 max, Int32 count)
		{
			return Range(random, min, max, count, NextNonZeroUInt16);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt16> NonZeroRange<T>(this T random, UInt16 min, UInt16 max, Int32 count) where T : IRandom
		{
			return Range(random, min, max, count, NextNonZeroUInt16);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int32> NonZeroRange()
		{
			return NonZeroRange(Int32.MinValue, Int32.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int32> NonZeroRange(this System.Random random)
		{
			return NonZeroRange(random, Int32.MinValue, Int32.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int32> NonZeroRange<T>(this T random) where T : IRandom
		{
			return NonZeroRange(random, Int32.MinValue, Int32.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int32> NonZeroRange(Int32 max)
		{
			return NonZeroRange(Int32.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int32> NonZeroRange(this System.Random random, Int32 max)
		{
			return NonZeroRange(random, Int32.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int32> NonZeroRange<T>(this T random, Int32 max) where T : IRandom
		{
			return NonZeroRange(random, Int32.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int32> NonZeroRange(Int32 min, Int32 max)
		{
			return Range(min, max, NextNonZeroInt32);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int32> NonZeroRange(this System.Random random, Int32 min, Int32 max)
		{
			return Range(random, min, max, NextNonZeroInt32);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int32> NonZeroRange<T>(this T random, Int32 min, Int32 max) where T : IRandom
		{
			return Range(random, min, max, NextNonZeroInt32);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int32> NonZeroRange(Int32 min, Int32 max, Int32 count)
		{
			return Range(min, max, count, NextNonZeroInt32);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int32> NonZeroRange(this System.Random random, Int32 min, Int32 max, Int32 count)
		{
			return Range(random, min, max, count, NextNonZeroInt32);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int32> NonZeroRange<T>(this T random, Int32 min, Int32 max, Int32 count) where T : IRandom
		{
			return Range(random, min, max, count, NextNonZeroInt32);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt32> NonZeroRange(UInt32 max)
		{
			return NonZeroRange(UInt32.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt32> NonZeroRange(this System.Random random, UInt32 max)
		{
			return NonZeroRange(random, UInt32.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt32> NonZeroRange<T>(this T random, UInt32 max) where T : IRandom
		{
			return NonZeroRange(random, UInt32.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt32> NonZeroRange(UInt32 min, UInt32 max)
		{
			return Range(min, max, NextNonZeroUInt32);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt32> NonZeroRange(this System.Random random, UInt32 min, UInt32 max)
		{
			return Range(random, min, max, NextNonZeroUInt32);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt32> NonZeroRange<T>(this T random, UInt32 min, UInt32 max) where T : IRandom
		{
			return Range(random, min, max, NextNonZeroUInt32);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt32> NonZeroRange(UInt32 min, UInt32 max, Int32 count)
		{
			return Range(min, max, count, NextNonZeroUInt32);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt32> NonZeroRange(this System.Random random, UInt32 min, UInt32 max, Int32 count)
		{
			return Range(random, min, max, count, NextNonZeroUInt32);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt32> NonZeroRange<T>(this T random, UInt32 min, UInt32 max, Int32 count) where T : IRandom
		{
			return Range(random, min, max, count, NextNonZeroUInt32);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int64> NonZeroRange(Int64 max)
		{
			return NonZeroRange(Int64.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int64> NonZeroRange(this System.Random random, Int64 max)
		{
			return NonZeroRange(random, Int64.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int64> NonZeroRange<T>(this T random, Int64 max) where T : IRandom
		{
			return NonZeroRange(random, Int64.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int64> NonZeroRange(Int64 min, Int64 max)
		{
			return Range(min, max, NextNonZeroInt64);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int64> NonZeroRange(this System.Random random, Int64 min, Int64 max)
		{
			return Range(random, min, max, NextNonZeroInt64);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int64> NonZeroRange<T>(this T random, Int64 min, Int64 max) where T : IRandom
		{
			return Range(random, min, max, NextNonZeroInt64);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int64> NonZeroRange(Int64 min, Int64 max, Int32 count)
		{
			return Range(min, max, count, NextNonZeroInt64);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int64> NonZeroRange(this System.Random random, Int64 min, Int64 max, Int32 count)
		{
			return Range(random, min, max, count, NextNonZeroInt64);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int64> NonZeroRange<T>(this T random, Int64 min, Int64 max, Int32 count) where T : IRandom
		{
			return Range(random, min, max, count, NextNonZeroInt64);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt64> NonZeroRange(UInt64 max)
		{
			return NonZeroRange(UInt64.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt64> NonZeroRange(this System.Random random, UInt64 max)
		{
			return NonZeroRange(random, UInt64.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt64> NonZeroRange<T>(this T random, UInt64 max) where T : IRandom
		{
			return NonZeroRange(random, UInt64.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt64> NonZeroRange(UInt64 min, UInt64 max)
		{
			return Range(min, max, NextNonZeroUInt64);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt64> NonZeroRange(this System.Random random, UInt64 min, UInt64 max)
		{
			return Range(random, min, max, NextNonZeroUInt64);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt64> NonZeroRange<T>(this T random, UInt64 min, UInt64 max) where T : IRandom
		{
			return Range(random, min, max, NextNonZeroUInt64);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt64> NonZeroRange(UInt64 min, UInt64 max, Int32 count)
		{
			return Range(min, max, count, NextNonZeroUInt64);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt64> NonZeroRange(this System.Random random, UInt64 min, UInt64 max, Int32 count)
		{
			return Range(random, min, max, count, NextNonZeroUInt64);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt64> NonZeroRange<T>(this T random, UInt64 min, UInt64 max, Int32 count) where T : IRandom
		{
			return Range(random, min, max, count, NextNonZeroUInt64);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Single> NonZeroRange(Single max)
		{
			return NonZeroRange(Single.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Single> NonZeroRange(this System.Random random, Single max)
		{
			return NonZeroRange(random, Single.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Single> NonZeroRange<T>(this T random, Single max) where T : IRandom
		{
			return NonZeroRange(random, Single.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Single> NonZeroRange(Single min, Single max)
		{
			return Range(min, max, NextNonZeroSingle);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Single> NonZeroRange(this System.Random random, Single min, Single max)
		{
			return Range(random, min, max, NextNonZeroSingle);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Single> NonZeroRange<T>(this T random, Single min, Single max) where T : IRandom
		{
			return Range(random, min, max, NextNonZeroSingle);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Single> NonZeroRange(Single min, Single max, Int32 count)
		{
			return Range(min, max, count, NextNonZeroSingle);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Single> NonZeroRange(this System.Random random, Single min, Single max, Int32 count)
		{
			return Range(random, min, max, count, NextNonZeroSingle);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Single> NonZeroRange<T>(this T random, Single min, Single max, Int32 count) where T : IRandom
		{
			return Range(random, min, max, count, NextNonZeroSingle);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Double> NonZeroRange(Double max)
		{
			return NonZeroRange(Double.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Double> NonZeroRange(this System.Random random, Double max)
		{
			return NonZeroRange(random, Double.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Double> NonZeroRange<T>(this T random, Double max) where T : IRandom
		{
			return NonZeroRange(random, Double.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Double> NonZeroRange(Double min, Double max)
		{
			return Range(min, max, NextNonZeroDouble);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Double> NonZeroRange(this System.Random random, Double min, Double max)
		{
			return Range(random, min, max, NextNonZeroDouble);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Double> NonZeroRange<T>(this T random, Double min, Double max) where T : IRandom
		{
			return Range(random, min, max, NextNonZeroDouble);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Double> NonZeroRange(Double min, Double max, Int32 count)
		{
			return Range(min, max, count, NextNonZeroDouble);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Double> NonZeroRange(this System.Random random, Double min, Double max, Int32 count)
		{
			return Range(random, min, max, count, NextNonZeroDouble);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Double> NonZeroRange<T>(this T random, Double min, Double max, Int32 count) where T : IRandom
		{
			return Range(random, min, max, count, NextNonZeroDouble);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Decimal> NonZeroRange(Decimal max)
		{
			return NonZeroRange(Decimal.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Decimal> NonZeroRange(this System.Random random, Decimal max)
		{
			return NonZeroRange(random, Decimal.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Decimal> NonZeroRange<T>(this T random, Decimal max) where T : IRandom
		{
			return NonZeroRange(random, Decimal.MinValue, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Decimal> NonZeroRange(Decimal min, Decimal max)
		{
			return Range(min, max, NextNonZeroDecimal);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Decimal> NonZeroRange(this System.Random random, Decimal min, Decimal max)
		{
			return Range(random, min, max, NextNonZeroDecimal);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Decimal> NonZeroRange<T>(this T random, Decimal min, Decimal max) where T : IRandom
		{
			return Range(random, min, max, NextNonZeroDecimal);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Decimal> NonZeroRange(Decimal min, Decimal max, Int32 count)
		{
			return Range(min, max, count, NextNonZeroDecimal);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Decimal> NonZeroRange(this System.Random random, Decimal min, Decimal max, Int32 count)
		{
			return Range(random, min, max, count, NextNonZeroDecimal);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Decimal> NonZeroRange<T>(this T random, Decimal min, Decimal max, Int32 count) where T : IRandom
		{
			return Range(random, min, max, count, NextNonZeroDecimal);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IEnumerable<SByte> UniqueSetRange(System.Random random, SByte min, SByte max)
		{
			Int32 difference = Math.Min(max.DiscreteIncludeDifference(min, Byte.MaxValue), Int32.MaxValue);
			HashSet<SByte> set = new HashSet<SByte>(Math.Min(difference, Byte.MaxValue));

			while (set.Count < difference)
			{
				SByte item = NextSByte(random, min, max);
				if (set.Add(item))
				{
					yield return item;
				}
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IEnumerable<SByte> UniqueSetRange<T>(T random, SByte min, SByte max) where T : IRandom
		{
			Int32 difference = Math.Min(max.DiscreteIncludeDifference(min, Byte.MaxValue), Int32.MaxValue);
			HashSet<SByte> set = new HashSet<SByte>(Math.Min(difference, Byte.MaxValue));

			while (set.Count < difference)
			{
				SByte item = NextSByte(random, min, max);
				if (set.Add(item))
				{
					yield return item;
				}
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IEnumerable<SByte> UniqueSetRange(System.Random random, SByte min, SByte max, Int32 count)
		{
			HashSet<SByte> set = new HashSet<SByte>(count);

			while (set.Count < count)
			{
				SByte item = NextSByte(random, min, max);
				if (set.Add(item))
				{
					yield return item;
				}
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IEnumerable<SByte> UniqueSetRange<T>(T random, SByte min, SByte max, Int32 count) where T : IRandom
		{
			HashSet<SByte> set = new HashSet<SByte>(count);

			while (set.Count < count)
			{
				SByte item = NextSByte(random, min, max);
				if (set.Add(item))
				{
					yield return item;
				}
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IEnumerable<SByte> UniqueShuffleRange(System.Random random, SByte min, SByte max)
		{
			return MathUtilities.RangeInclude(min, max).Shuffle(random);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IEnumerable<SByte> UniqueShuffleRange<T>(T random, SByte min, SByte max) where T : IRandom
		{
			return MathUtilities.RangeInclude(min, max).Shuffle(random);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IEnumerable<SByte> UniqueShuffleRange(System.Random random, SByte min, SByte max, Int32 count)
		{
			return UniqueShuffleRange(random, min, max).Take(count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IEnumerable<SByte> UniqueShuffleRange<T>(T random, SByte min, SByte max, Int32 count) where T : IRandom
		{
			return UniqueShuffleRange(random, min, max).Take(count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<SByte> UniqueRange(SByte min, SByte max)
		{
			return UniqueRange(Generator, min, max);
		}

		public static IEnumerable<SByte> UniqueRange(this System.Random random, SByte min, SByte max)
		{
			if (random is null)
			{
				throw new ArgumentNullException(nameof(random));
			}

			if (max == min)
			{
				return EnumerableUtilities.GetEnumerableFrom(min);
			}

			if (max < min)
			{
				(min, max) = (max, min);
			}

			Byte difference = max.DiscreteIncludeDifference(min, Byte.MaxValue);
			return UniqueShuffleRange(random, min, max, difference);
		}

		public static IEnumerable<SByte> UniqueRange<T>(this T random, SByte min, SByte max) where T : IRandom
		{
			if (random is null)
			{
				throw new ArgumentNullException(nameof(random));
			}

			if (max == min)
			{
				return EnumerableUtilities.GetEnumerableFrom(min);
			}

			if (max < min)
			{
				(min, max) = (max, min);
			}

			Byte difference = max.DiscreteIncludeDifference(min, Byte.MaxValue);
			return UniqueShuffleRange(random, min, max, difference);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<SByte> UniqueRange(SByte min, SByte max, Int32 count)
		{
			return UniqueRange(Generator, min, max, count);
		}

		public static IEnumerable<SByte> UniqueRange(this System.Random random, SByte min, SByte max, Int32 count)
		{
			if (random is null)
			{
				throw new ArgumentNullException(nameof(random));
			}

			switch (count)
			{
				case <= 0:
					return Enumerable.Empty<SByte>();
				case 1:
					return EnumerableUtilities.GetEnumerableFrom(NextSByte(random, min, max));
			}

			if (max == min)
			{
				return EnumerableUtilities.GetEnumerableFrom(min);
			}

			if (max < min)
			{
				(min, max) = (max, min);
			}

			Byte difference = max.DiscreteIncludeDifference(min, Byte.MaxValue);
			count = (Int32) Math.Min((UInt32) count, difference);
			return count <= SByte.MaxValue + 1 || count / (Double) difference >= 0.8 ?
				UniqueShuffleRange(random, min, max, count) :
				UniqueSetRange(random, min, max, count);
		}

		public static IEnumerable<SByte> UniqueRange<T>(this T random, SByte min, SByte max, Int32 count) where T : IRandom
		{
			if (random is null)
			{
				throw new ArgumentNullException(nameof(random));
			}

			switch (count)
			{
				case <= 0:
					return Enumerable.Empty<SByte>();
				case 1:
					return EnumerableUtilities.GetEnumerableFrom(NextSByte(random, min, max));
			}

			if (max == min)
			{
				return EnumerableUtilities.GetEnumerableFrom(min);
			}

			if (max < min)
			{
				(min, max) = (max, min);
			}

			Byte difference = max.DiscreteIncludeDifference(min, Byte.MaxValue);
			count = (Int32) Math.Min((UInt32) count, difference);
			return count <= SByte.MaxValue + 1 || count / (Double) difference >= 0.8 ?
				UniqueShuffleRange(random, min, max, count) :
				UniqueSetRange(random, min, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IEnumerable<Byte> UniqueSetRange(System.Random random, Byte min, Byte max)
		{
			Int32 difference = Math.Min(max.DiscreteIncludeDifference(min, Byte.MaxValue), Int32.MaxValue);
			HashSet<Byte> set = new HashSet<Byte>(Math.Min(difference, Byte.MaxValue));

			while (set.Count < difference)
			{
				Byte item = NextByte(random, min, max);
				if (set.Add(item))
				{
					yield return item;
				}
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IEnumerable<Byte> UniqueSetRange<T>(T random, Byte min, Byte max) where T : IRandom
		{
			Int32 difference = Math.Min(max.DiscreteIncludeDifference(min, Byte.MaxValue), Int32.MaxValue);
			HashSet<Byte> set = new HashSet<Byte>(Math.Min(difference, Byte.MaxValue));

			while (set.Count < difference)
			{
				Byte item = NextByte(random, min, max);
				if (set.Add(item))
				{
					yield return item;
				}
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IEnumerable<Byte> UniqueSetRange(System.Random random, Byte min, Byte max, Int32 count)
		{
			HashSet<Byte> set = new HashSet<Byte>(count);

			while (set.Count < count)
			{
				Byte item = NextByte(random, min, max);
				if (set.Add(item))
				{
					yield return item;
				}
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IEnumerable<Byte> UniqueSetRange<T>(T random, Byte min, Byte max, Int32 count) where T : IRandom
		{
			HashSet<Byte> set = new HashSet<Byte>(count);

			while (set.Count < count)
			{
				Byte item = NextByte(random, min, max);
				if (set.Add(item))
				{
					yield return item;
				}
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IEnumerable<Byte> UniqueShuffleRange(System.Random random, Byte min, Byte max)
		{
			return MathUtilities.RangeInclude(min, max).Shuffle(random);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IEnumerable<Byte> UniqueShuffleRange<T>(T random, Byte min, Byte max) where T : IRandom
		{
			return MathUtilities.RangeInclude(min, max).Shuffle(random);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IEnumerable<Byte> UniqueShuffleRange(System.Random random, Byte min, Byte max, Int32 count)
		{
			return UniqueShuffleRange(random, min, max).Take(count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IEnumerable<Byte> UniqueShuffleRange<T>(T random, Byte min, Byte max, Int32 count) where T : IRandom
		{
			return UniqueShuffleRange(random, min, max).Take(count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Byte> UniqueRange(Byte min, Byte max)
		{
			return UniqueRange(Generator, min, max);
		}

		public static IEnumerable<Byte> UniqueRange(this System.Random random, Byte min, Byte max)
		{
			if (random is null)
			{
				throw new ArgumentNullException(nameof(random));
			}

			if (max == min)
			{
				return EnumerableUtilities.GetEnumerableFrom(min);
			}

			if (max < min)
			{
				(min, max) = (max, min);
			}

			Byte difference = max.DiscreteIncludeDifference(min, Byte.MaxValue);
			return UniqueShuffleRange(random, min, max, difference);
		}

		public static IEnumerable<Byte> UniqueRange<T>(this T random, Byte min, Byte max) where T : IRandom
		{
			if (random is null)
			{
				throw new ArgumentNullException(nameof(random));
			}

			if (max == min)
			{
				return EnumerableUtilities.GetEnumerableFrom(min);
			}

			if (max < min)
			{
				(min, max) = (max, min);
			}

			Byte difference = max.DiscreteIncludeDifference(min, Byte.MaxValue);
			return UniqueShuffleRange(random, min, max, difference);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Byte> UniqueRange(Byte min, Byte max, Int32 count)
		{
			return UniqueRange(Generator, min, max, count);
		}

		public static IEnumerable<Byte> UniqueRange(this System.Random random, Byte min, Byte max, Int32 count)
		{
			if (random is null)
			{
				throw new ArgumentNullException(nameof(random));
			}

			switch (count)
			{
				case <= 0:
					return Enumerable.Empty<Byte>();
				case 1:
					return EnumerableUtilities.GetEnumerableFrom(NextByte(random, min, max));
			}

			if (max == min)
			{
				return EnumerableUtilities.GetEnumerableFrom(min);
			}

			if (max < min)
			{
				(min, max) = (max, min);
			}

			Byte difference = max.DiscreteIncludeDifference(min, Byte.MaxValue);
			count = (Int32) Math.Min((UInt32) count, difference);
			return count <= SByte.MaxValue + 1 || count / (Double) difference >= 0.8 ?
				UniqueShuffleRange(random, min, max, count) :
				UniqueSetRange(random, min, max, count);
		}

		public static IEnumerable<Byte> UniqueRange<T>(this T random, Byte min, Byte max, Int32 count) where T : IRandom
		{
			if (random is null)
			{
				throw new ArgumentNullException(nameof(random));
			}

			switch (count)
			{
				case <= 0:
					return Enumerable.Empty<Byte>();
				case 1:
					return EnumerableUtilities.GetEnumerableFrom(NextByte(random, min, max));
			}

			if (max == min)
			{
				return EnumerableUtilities.GetEnumerableFrom(min);
			}

			if (max < min)
			{
				(min, max) = (max, min);
			}

			Byte difference = max.DiscreteIncludeDifference(min, Byte.MaxValue);
			count = (Int32) Math.Min((UInt32) count, difference);
			return count <= SByte.MaxValue + 1 || count / (Double) difference >= 0.8 ?
				UniqueShuffleRange(random, min, max, count) :
				UniqueSetRange(random, min, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IEnumerable<Int16> UniqueSetRange(System.Random random, Int16 min, Int16 max)
		{
			Int32 difference = Math.Min(max.DiscreteIncludeDifference(min, UInt16.MaxValue), Int32.MaxValue);
			HashSet<Int16> set = new HashSet<Int16>(Math.Min(difference, Byte.MaxValue));

			while (set.Count < difference)
			{
				Int16 item = NextInt16(random, min, max);
				if (set.Add(item))
				{
					yield return item;
				}
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IEnumerable<Int16> UniqueSetRange<T>(T random, Int16 min, Int16 max) where T : IRandom
		{
			Int32 difference = Math.Min(max.DiscreteIncludeDifference(min, UInt16.MaxValue), Int32.MaxValue);
			HashSet<Int16> set = new HashSet<Int16>(Math.Min(difference, Byte.MaxValue));

			while (set.Count < difference)
			{
				Int16 item = NextInt16(random, min, max);
				if (set.Add(item))
				{
					yield return item;
				}
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IEnumerable<Int16> UniqueSetRange(System.Random random, Int16 min, Int16 max, Int32 count)
		{
			HashSet<Int16> set = new HashSet<Int16>(count);

			while (set.Count < count)
			{
				Int16 item = NextInt16(random, min, max);
				if (set.Add(item))
				{
					yield return item;
				}
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IEnumerable<Int16> UniqueSetRange<T>(T random, Int16 min, Int16 max, Int32 count) where T : IRandom
		{
			HashSet<Int16> set = new HashSet<Int16>(count);

			while (set.Count < count)
			{
				Int16 item = NextInt16(random, min, max);
				if (set.Add(item))
				{
					yield return item;
				}
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IEnumerable<Int16> UniqueShuffleRange(System.Random random, Int16 min, Int16 max)
		{
			return MathUtilities.RangeInclude(min, max).Shuffle(random);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IEnumerable<Int16> UniqueShuffleRange<T>(T random, Int16 min, Int16 max) where T : IRandom
		{
			return MathUtilities.RangeInclude(min, max).Shuffle(random);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IEnumerable<Int16> UniqueShuffleRange(System.Random random, Int16 min, Int16 max, Int32 count)
		{
			return UniqueShuffleRange(random, min, max).Take(count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IEnumerable<Int16> UniqueShuffleRange<T>(T random, Int16 min, Int16 max, Int32 count) where T : IRandom
		{
			return UniqueShuffleRange(random, min, max).Take(count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int16> UniqueRange(Int16 min, Int16 max)
		{
			return UniqueRange(Generator, min, max);
		}

		public static IEnumerable<Int16> UniqueRange(this System.Random random, Int16 min, Int16 max)
		{
			if (random is null)
			{
				throw new ArgumentNullException(nameof(random));
			}

			if (max == min)
			{
				return EnumerableUtilities.GetEnumerableFrom(min);
			}

			if (max < min)
			{
				(min, max) = (max, min);
			}

			UInt16 difference = max.DiscreteIncludeDifference(min, UInt16.MaxValue);
			return difference <= (Byte.MaxValue + 1) * 4 ?
				UniqueShuffleRange(random, min, max, difference) :
				UniqueSetRange(random, min, max);
		}

		public static IEnumerable<Int16> UniqueRange<T>(this T random, Int16 min, Int16 max) where T : IRandom
		{
			if (random is null)
			{
				throw new ArgumentNullException(nameof(random));
			}

			if (max == min)
			{
				return EnumerableUtilities.GetEnumerableFrom(min);
			}

			if (max < min)
			{
				(min, max) = (max, min);
			}

			UInt16 difference = max.DiscreteIncludeDifference(min, UInt16.MaxValue);
			return difference <= (Byte.MaxValue + 1) * 4 ?
				UniqueShuffleRange(random, min, max, difference) :
				UniqueSetRange(random, min, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int16> UniqueRange(Int16 min, Int16 max, Int32 count)
		{
			return UniqueRange(Generator, min, max, count);
		}

		public static IEnumerable<Int16> UniqueRange(this System.Random random, Int16 min, Int16 max, Int32 count)
		{
			if (random is null)
			{
				throw new ArgumentNullException(nameof(random));
			}

			switch (count)
			{
				case <= 0:
					return Enumerable.Empty<Int16>();
				case 1:
					return EnumerableUtilities.GetEnumerableFrom(NextInt16(random, min, max));
			}

			if (max == min)
			{
				return EnumerableUtilities.GetEnumerableFrom(min);
			}

			if (max < min)
			{
				(min, max) = (max, min);
			}

			UInt16 difference = max.DiscreteIncludeDifference(min, UInt16.MaxValue);
			count = (Int32) Math.Min((UInt32) count, difference);
			return count <= SByte.MaxValue + 1 || count / (Double) difference >= 0.8 ?
				UniqueShuffleRange(random, min, max, count) :
				UniqueSetRange(random, min, max, count);
		}

		public static IEnumerable<Int16> UniqueRange<T>(this T random, Int16 min, Int16 max, Int32 count) where T : IRandom
		{
			if (random is null)
			{
				throw new ArgumentNullException(nameof(random));
			}

			switch (count)
			{
				case <= 0:
					return Enumerable.Empty<Int16>();
				case 1:
					return EnumerableUtilities.GetEnumerableFrom(NextInt16(random, min, max));
			}

			if (max == min)
			{
				return EnumerableUtilities.GetEnumerableFrom(min);
			}

			if (max < min)
			{
				(min, max) = (max, min);
			}

			UInt16 difference = max.DiscreteIncludeDifference(min, UInt16.MaxValue);
			count = (Int32) Math.Min((UInt32) count, difference);
			return count <= SByte.MaxValue + 1 || count / (Double) difference >= 0.8 ?
				UniqueShuffleRange(random, min, max, count) :
				UniqueSetRange(random, min, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IEnumerable<UInt16> UniqueSetRange(System.Random random, UInt16 min, UInt16 max)
		{
			Int32 difference = Math.Min(max.DiscreteIncludeDifference(min, UInt16.MaxValue), Int32.MaxValue);
			HashSet<UInt16> set = new HashSet<UInt16>(Math.Min(difference, Byte.MaxValue));

			while (set.Count < difference)
			{
				UInt16 item = NextUInt16(random, min, max);
				if (set.Add(item))
				{
					yield return item;
				}
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IEnumerable<UInt16> UniqueSetRange<T>(T random, UInt16 min, UInt16 max) where T : IRandom
		{
			Int32 difference = Math.Min(max.DiscreteIncludeDifference(min, UInt16.MaxValue), Int32.MaxValue);
			HashSet<UInt16> set = new HashSet<UInt16>(Math.Min(difference, Byte.MaxValue));

			while (set.Count < difference)
			{
				UInt16 item = NextUInt16(random, min, max);
				if (set.Add(item))
				{
					yield return item;
				}
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IEnumerable<UInt16> UniqueSetRange(System.Random random, UInt16 min, UInt16 max, Int32 count)
		{
			HashSet<UInt16> set = new HashSet<UInt16>(count);

			while (set.Count < count)
			{
				UInt16 item = NextUInt16(random, min, max);
				if (set.Add(item))
				{
					yield return item;
				}
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IEnumerable<UInt16> UniqueSetRange<T>(T random, UInt16 min, UInt16 max, Int32 count) where T : IRandom
		{
			HashSet<UInt16> set = new HashSet<UInt16>(count);

			while (set.Count < count)
			{
				UInt16 item = NextUInt16(random, min, max);
				if (set.Add(item))
				{
					yield return item;
				}
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IEnumerable<UInt16> UniqueShuffleRange(System.Random random, UInt16 min, UInt16 max)
		{
			return MathUtilities.RangeInclude(min, max).Shuffle(random);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IEnumerable<UInt16> UniqueShuffleRange<T>(T random, UInt16 min, UInt16 max) where T : IRandom
		{
			return MathUtilities.RangeInclude(min, max).Shuffle(random);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IEnumerable<UInt16> UniqueShuffleRange(System.Random random, UInt16 min, UInt16 max, Int32 count)
		{
			return UniqueShuffleRange(random, min, max).Take(count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IEnumerable<UInt16> UniqueShuffleRange<T>(T random, UInt16 min, UInt16 max, Int32 count) where T : IRandom
		{
			return UniqueShuffleRange(random, min, max).Take(count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt16> UniqueRange(UInt16 min, UInt16 max)
		{
			return UniqueRange(Generator, min, max);
		}

		public static IEnumerable<UInt16> UniqueRange(this System.Random random, UInt16 min, UInt16 max)
		{
			if (random is null)
			{
				throw new ArgumentNullException(nameof(random));
			}

			if (max == min)
			{
				return EnumerableUtilities.GetEnumerableFrom(min);
			}

			if (max < min)
			{
				(min, max) = (max, min);
			}

			UInt16 difference = max.DiscreteIncludeDifference(min, UInt16.MaxValue);
			return difference <= (Byte.MaxValue + 1) * 4 ?
				UniqueShuffleRange(random, min, max, difference) :
				UniqueSetRange(random, min, max);
		}

		public static IEnumerable<UInt16> UniqueRange<T>(this T random, UInt16 min, UInt16 max) where T : IRandom
		{
			if (random is null)
			{
				throw new ArgumentNullException(nameof(random));
			}

			if (max == min)
			{
				return EnumerableUtilities.GetEnumerableFrom(min);
			}

			if (max < min)
			{
				(min, max) = (max, min);
			}

			UInt16 difference = max.DiscreteIncludeDifference(min, UInt16.MaxValue);
			return difference <= (Byte.MaxValue + 1) * 4 ?
				UniqueShuffleRange(random, min, max, difference) :
				UniqueSetRange(random, min, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt16> UniqueRange(UInt16 min, UInt16 max, Int32 count)
		{
			return UniqueRange(Generator, min, max, count);
		}

		public static IEnumerable<UInt16> UniqueRange(this System.Random random, UInt16 min, UInt16 max, Int32 count)
		{
			if (random is null)
			{
				throw new ArgumentNullException(nameof(random));
			}

			switch (count)
			{
				case <= 0:
					return Enumerable.Empty<UInt16>();
				case 1:
					return EnumerableUtilities.GetEnumerableFrom(NextUInt16(random, min, max));
			}

			if (max == min)
			{
				return EnumerableUtilities.GetEnumerableFrom(min);
			}

			if (max < min)
			{
				(min, max) = (max, min);
			}

			UInt16 difference = max.DiscreteIncludeDifference(min, UInt16.MaxValue);
			count = (Int32) Math.Min((UInt32) count, difference);
			return count <= SByte.MaxValue + 1 || count / (Double) difference >= 0.8 ?
				UniqueShuffleRange(random, min, max, count) :
				UniqueSetRange(random, min, max, count);
		}

		public static IEnumerable<UInt16> UniqueRange<T>(this T random, UInt16 min, UInt16 max, Int32 count) where T : IRandom
		{
			if (random is null)
			{
				throw new ArgumentNullException(nameof(random));
			}

			switch (count)
			{
				case <= 0:
					return Enumerable.Empty<UInt16>();
				case 1:
					return EnumerableUtilities.GetEnumerableFrom(NextUInt16(random, min, max));
			}

			if (max == min)
			{
				return EnumerableUtilities.GetEnumerableFrom(min);
			}

			if (max < min)
			{
				(min, max) = (max, min);
			}

			UInt16 difference = max.DiscreteIncludeDifference(min, UInt16.MaxValue);
			count = (Int32) Math.Min((UInt32) count, difference);
			return count <= SByte.MaxValue + 1 || count / (Double) difference >= 0.8 ?
				UniqueShuffleRange(random, min, max, count) :
				UniqueSetRange(random, min, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IEnumerable<Int32> UniqueSetRange(System.Random random, Int32 min, Int32 max)
		{
			Int32 difference = (Int32) Math.Min(max.DiscreteIncludeDifference(min, UInt32.MaxValue), Int32.MaxValue);
			HashSet<Int32> set = new HashSet<Int32>(Math.Min(difference, Byte.MaxValue));

			while (set.Count < difference)
			{
				Int32 item = NextInt32(random, min, max);
				if (set.Add(item))
				{
					yield return item;
				}
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IEnumerable<Int32> UniqueSetRange<T>(T random, Int32 min, Int32 max) where T : IRandom
		{
			Int32 difference = (Int32) Math.Min(max.DiscreteIncludeDifference(min, UInt32.MaxValue), Int32.MaxValue);
			HashSet<Int32> set = new HashSet<Int32>(Math.Min(difference, Byte.MaxValue));

			while (set.Count < difference)
			{
				Int32 item = NextInt32(random, min, max);
				if (set.Add(item))
				{
					yield return item;
				}
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IEnumerable<Int32> UniqueSetRange(System.Random random, Int32 min, Int32 max, Int32 count)
		{
			HashSet<Int32> set = new HashSet<Int32>(count);

			while (set.Count < count)
			{
				Int32 item = NextInt32(random, min, max);
				if (set.Add(item))
				{
					yield return item;
				}
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IEnumerable<Int32> UniqueSetRange<T>(T random, Int32 min, Int32 max, Int32 count) where T : IRandom
		{
			HashSet<Int32> set = new HashSet<Int32>(count);

			while (set.Count < count)
			{
				Int32 item = NextInt32(random, min, max);
				if (set.Add(item))
				{
					yield return item;
				}
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IEnumerable<Int32> UniqueShuffleRange(System.Random random, Int32 min, Int32 max)
		{
			return MathUtilities.RangeInclude(min, max).Shuffle(random);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IEnumerable<Int32> UniqueShuffleRange<T>(T random, Int32 min, Int32 max) where T : IRandom
		{
			return MathUtilities.RangeInclude(min, max).Shuffle(random);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IEnumerable<Int32> UniqueShuffleRange(System.Random random, Int32 min, Int32 max, Int32 count)
		{
			return UniqueShuffleRange(random, min, max).Take(count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IEnumerable<Int32> UniqueShuffleRange<T>(T random, Int32 min, Int32 max, Int32 count) where T : IRandom
		{
			return UniqueShuffleRange(random, min, max).Take(count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int32> UniqueRange(Int32 min, Int32 max)
		{
			return UniqueRange(Generator, min, max);
		}

		public static IEnumerable<Int32> UniqueRange(this System.Random random, Int32 min, Int32 max)
		{
			if (random is null)
			{
				throw new ArgumentNullException(nameof(random));
			}

			if (max == min)
			{
				return EnumerableUtilities.GetEnumerableFrom(min);
			}

			if (max < min)
			{
				(min, max) = (max, min);
			}

			UInt32 difference = max.DiscreteIncludeDifference(min, UInt32.MaxValue);
			return difference <= (Byte.MaxValue + 1) * 4 ?
				UniqueShuffleRange(random, min, max, (Int32) difference) :
				UniqueSetRange(random, min, max);
		}

		public static IEnumerable<Int32> UniqueRange<T>(this T random, Int32 min, Int32 max) where T : IRandom
		{
			if (random is null)
			{
				throw new ArgumentNullException(nameof(random));
			}

			if (max == min)
			{
				return EnumerableUtilities.GetEnumerableFrom(min);
			}

			if (max < min)
			{
				(min, max) = (max, min);
			}

			UInt32 difference = max.DiscreteIncludeDifference(min, UInt32.MaxValue);
			return difference <= (Byte.MaxValue + 1) * 4 ?
				UniqueShuffleRange(random, min, max, (Int32) difference) :
				UniqueSetRange(random, min, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int32> UniqueRange(Int32 min, Int32 max, Int32 count)
		{
			return UniqueRange(Generator, min, max, count);
		}

		public static IEnumerable<Int32> UniqueRange(this System.Random random, Int32 min, Int32 max, Int32 count)
		{
			if (random is null)
			{
				throw new ArgumentNullException(nameof(random));
			}

			switch (count)
			{
				case <= 0:
					return Enumerable.Empty<Int32>();
				case 1:
					return EnumerableUtilities.GetEnumerableFrom(NextInt32(random, min, max));
			}

			if (max == min)
			{
				return EnumerableUtilities.GetEnumerableFrom(min);
			}

			if (max < min)
			{
				(min, max) = (max, min);
			}

			UInt32 difference = max.DiscreteIncludeDifference(min, UInt32.MaxValue);
			count = (Int32) Math.Min((UInt32) count, difference);
			return count <= SByte.MaxValue + 1 || count / (Double) difference >= 0.8 ?
				UniqueShuffleRange(random, min, max, count) :
				UniqueSetRange(random, min, max, count);
		}

		public static IEnumerable<Int32> UniqueRange<T>(this T random, Int32 min, Int32 max, Int32 count) where T : IRandom
		{
			if (random is null)
			{
				throw new ArgumentNullException(nameof(random));
			}

			switch (count)
			{
				case <= 0:
					return Enumerable.Empty<Int32>();
				case 1:
					return EnumerableUtilities.GetEnumerableFrom(NextInt32(random, min, max));
			}

			if (max == min)
			{
				return EnumerableUtilities.GetEnumerableFrom(min);
			}

			if (max < min)
			{
				(min, max) = (max, min);
			}

			UInt32 difference = max.DiscreteIncludeDifference(min, UInt32.MaxValue);
			count = (Int32) Math.Min((UInt32) count, difference);
			return count <= SByte.MaxValue + 1 || count / (Double) difference >= 0.8 ?
				UniqueShuffleRange(random, min, max, count) :
				UniqueSetRange(random, min, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IEnumerable<UInt32> UniqueSetRange(System.Random random, UInt32 min, UInt32 max)
		{
			Int32 difference = (Int32) Math.Min(max.DiscreteIncludeDifference(min, UInt32.MaxValue), Int32.MaxValue);
			HashSet<UInt32> set = new HashSet<UInt32>(Math.Min(difference, Byte.MaxValue));

			while (set.Count < difference)
			{
				UInt32 item = NextUInt32(random, min, max);
				if (set.Add(item))
				{
					yield return item;
				}
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IEnumerable<UInt32> UniqueSetRange<T>(T random, UInt32 min, UInt32 max) where T : IRandom
		{
			Int32 difference = (Int32) Math.Min(max.DiscreteIncludeDifference(min, UInt32.MaxValue), Int32.MaxValue);
			HashSet<UInt32> set = new HashSet<UInt32>(Math.Min(difference, Byte.MaxValue));

			while (set.Count < difference)
			{
				UInt32 item = NextUInt32(random, min, max);
				if (set.Add(item))
				{
					yield return item;
				}
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IEnumerable<UInt32> UniqueSetRange(System.Random random, UInt32 min, UInt32 max, Int32 count)
		{
			HashSet<UInt32> set = new HashSet<UInt32>(count);

			while (set.Count < count)
			{
				UInt32 item = NextUInt32(random, min, max);
				if (set.Add(item))
				{
					yield return item;
				}
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IEnumerable<UInt32> UniqueSetRange<T>(T random, UInt32 min, UInt32 max, Int32 count) where T : IRandom
		{
			HashSet<UInt32> set = new HashSet<UInt32>(count);

			while (set.Count < count)
			{
				UInt32 item = NextUInt32(random, min, max);
				if (set.Add(item))
				{
					yield return item;
				}
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IEnumerable<UInt32> UniqueShuffleRange(System.Random random, UInt32 min, UInt32 max)
		{
			return MathUtilities.RangeInclude(min, max).Shuffle(random);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IEnumerable<UInt32> UniqueShuffleRange<T>(T random, UInt32 min, UInt32 max) where T : IRandom
		{
			return MathUtilities.RangeInclude(min, max).Shuffle(random);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IEnumerable<UInt32> UniqueShuffleRange(System.Random random, UInt32 min, UInt32 max, Int32 count)
		{
			return UniqueShuffleRange(random, min, max).Take(count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IEnumerable<UInt32> UniqueShuffleRange<T>(T random, UInt32 min, UInt32 max, Int32 count) where T : IRandom
		{
			return UniqueShuffleRange(random, min, max).Take(count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt32> UniqueRange(UInt32 min, UInt32 max)
		{
			return UniqueRange(Generator, min, max);
		}

		public static IEnumerable<UInt32> UniqueRange(this System.Random random, UInt32 min, UInt32 max)
		{
			if (random is null)
			{
				throw new ArgumentNullException(nameof(random));
			}

			if (max == min)
			{
				return EnumerableUtilities.GetEnumerableFrom(min);
			}

			if (max < min)
			{
				(min, max) = (max, min);
			}

			UInt32 difference = max.DiscreteIncludeDifference(min, UInt32.MaxValue);
			return difference <= (Byte.MaxValue + 1) * 4 ?
				UniqueShuffleRange(random, min, max, (Int32) difference) :
				UniqueSetRange(random, min, max);
		}

		public static IEnumerable<UInt32> UniqueRange<T>(this T random, UInt32 min, UInt32 max) where T : IRandom
		{
			if (random is null)
			{
				throw new ArgumentNullException(nameof(random));
			}

			if (max == min)
			{
				return EnumerableUtilities.GetEnumerableFrom(min);
			}

			if (max < min)
			{
				(min, max) = (max, min);
			}

			UInt32 difference = max.DiscreteIncludeDifference(min, UInt32.MaxValue);
			return difference <= (Byte.MaxValue + 1) * 4 ?
				UniqueShuffleRange(random, min, max, (Int32) difference) :
				UniqueSetRange(random, min, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt32> UniqueRange(UInt32 min, UInt32 max, Int32 count)
		{
			return UniqueRange(Generator, min, max, count);
		}

		public static IEnumerable<UInt32> UniqueRange(this System.Random random, UInt32 min, UInt32 max, Int32 count)
		{
			if (random is null)
			{
				throw new ArgumentNullException(nameof(random));
			}

			switch (count)
			{
				case <= 0:
					return Enumerable.Empty<UInt32>();
				case 1:
					return EnumerableUtilities.GetEnumerableFrom(NextUInt32(random, min, max));
			}

			if (max == min)
			{
				return EnumerableUtilities.GetEnumerableFrom(min);
			}

			if (max < min)
			{
				(min, max) = (max, min);
			}

			UInt32 difference = max.DiscreteIncludeDifference(min, UInt32.MaxValue);
			count = (Int32) Math.Min((UInt32) count, difference);
			return count <= SByte.MaxValue + 1 || count / (Double) difference >= 0.8 ?
				UniqueShuffleRange(random, min, max, count) :
				UniqueSetRange(random, min, max, count);
		}

		public static IEnumerable<UInt32> UniqueRange<T>(this T random, UInt32 min, UInt32 max, Int32 count) where T : IRandom
		{
			if (random is null)
			{
				throw new ArgumentNullException(nameof(random));
			}

			switch (count)
			{
				case <= 0:
					return Enumerable.Empty<UInt32>();
				case 1:
					return EnumerableUtilities.GetEnumerableFrom(NextUInt32(random, min, max));
			}

			if (max == min)
			{
				return EnumerableUtilities.GetEnumerableFrom(min);
			}

			if (max < min)
			{
				(min, max) = (max, min);
			}

			UInt32 difference = max.DiscreteIncludeDifference(min, UInt32.MaxValue);
			count = (Int32) Math.Min((UInt32) count, difference);
			return count <= SByte.MaxValue + 1 || count / (Double) difference >= 0.8 ?
				UniqueShuffleRange(random, min, max, count) :
				UniqueSetRange(random, min, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IEnumerable<Int64> UniqueSetRange(System.Random random, Int64 min, Int64 max)
		{
			Int32 difference = (Int32) Math.Min(max.DiscreteIncludeDifference(min, UInt64.MaxValue), Int32.MaxValue);
			HashSet<Int64> set = new HashSet<Int64>(Math.Min(difference, Byte.MaxValue));

			while (set.Count < difference)
			{
				Int64 item = NextInt64(random, min, max);
				if (set.Add(item))
				{
					yield return item;
				}
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IEnumerable<Int64> UniqueSetRange<T>(T random, Int64 min, Int64 max) where T : IRandom
		{
			Int32 difference = (Int32) Math.Min(max.DiscreteIncludeDifference(min, UInt64.MaxValue), Int32.MaxValue);
			HashSet<Int64> set = new HashSet<Int64>(Math.Min(difference, Byte.MaxValue));

			while (set.Count < difference)
			{
				Int64 item = NextInt64(random, min, max);
				if (set.Add(item))
				{
					yield return item;
				}
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IEnumerable<Int64> UniqueSetRange(System.Random random, Int64 min, Int64 max, Int32 count)
		{
			HashSet<Int64> set = new HashSet<Int64>(count);

			while (set.Count < count)
			{
				Int64 item = NextInt64(random, min, max);
				if (set.Add(item))
				{
					yield return item;
				}
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IEnumerable<Int64> UniqueSetRange<T>(T random, Int64 min, Int64 max, Int32 count) where T : IRandom
		{
			HashSet<Int64> set = new HashSet<Int64>(count);

			while (set.Count < count)
			{
				Int64 item = NextInt64(random, min, max);
				if (set.Add(item))
				{
					yield return item;
				}
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IEnumerable<Int64> UniqueShuffleRange(System.Random random, Int64 min, Int64 max)
		{
			return MathUtilities.RangeInclude(min, max).Shuffle(random);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IEnumerable<Int64> UniqueShuffleRange<T>(T random, Int64 min, Int64 max) where T : IRandom
		{
			return MathUtilities.RangeInclude(min, max).Shuffle(random);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IEnumerable<Int64> UniqueShuffleRange(System.Random random, Int64 min, Int64 max, Int32 count)
		{
			return UniqueShuffleRange(random, min, max).Take(count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IEnumerable<Int64> UniqueShuffleRange<T>(T random, Int64 min, Int64 max, Int32 count) where T : IRandom
		{
			return UniqueShuffleRange(random, min, max).Take(count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int64> UniqueRange(Int64 min, Int64 max)
		{
			return UniqueRange(Generator, min, max);
		}

		public static IEnumerable<Int64> UniqueRange(this System.Random random, Int64 min, Int64 max)
		{
			if (random is null)
			{
				throw new ArgumentNullException(nameof(random));
			}

			if (max == min)
			{
				return EnumerableUtilities.GetEnumerableFrom(min);
			}

			if (max < min)
			{
				(min, max) = (max, min);
			}

			UInt64 difference = max.DiscreteIncludeDifference(min, UInt64.MaxValue);
			return difference <= (Byte.MaxValue + 1) * 4 ?
				UniqueShuffleRange(random, min, max, (Int32) difference) :
				UniqueSetRange(random, min, max);
		}

		public static IEnumerable<Int64> UniqueRange<T>(this T random, Int64 min, Int64 max) where T : IRandom
		{
			if (random is null)
			{
				throw new ArgumentNullException(nameof(random));
			}

			if (max == min)
			{
				return EnumerableUtilities.GetEnumerableFrom(min);
			}

			if (max < min)
			{
				(min, max) = (max, min);
			}

			UInt64 difference = max.DiscreteIncludeDifference(min, UInt64.MaxValue);
			return difference <= (Byte.MaxValue + 1) * 4 ?
				UniqueShuffleRange(random, min, max, (Int32) difference) :
				UniqueSetRange(random, min, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int64> UniqueRange(Int64 min, Int64 max, Int32 count)
		{
			return UniqueRange(Generator, min, max, count);
		}

		public static IEnumerable<Int64> UniqueRange(this System.Random random, Int64 min, Int64 max, Int32 count)
		{
			if (random is null)
			{
				throw new ArgumentNullException(nameof(random));
			}

			switch (count)
			{
				case <= 0:
					return Enumerable.Empty<Int64>();
				case 1:
					return EnumerableUtilities.GetEnumerableFrom(NextInt64(random, min, max));
			}

			if (max == min)
			{
				return EnumerableUtilities.GetEnumerableFrom(min);
			}

			if (max < min)
			{
				(min, max) = (max, min);
			}

			UInt64 difference = max.DiscreteIncludeDifference(min, UInt64.MaxValue);
			count = (Int32) Math.Min((UInt32) count, difference);
			return count <= SByte.MaxValue + 1 || count / (Double) difference >= 0.8 ?
				UniqueShuffleRange(random, min, max, count) :
				UniqueSetRange(random, min, max, count);
		}

		public static IEnumerable<Int64> UniqueRange<T>(this T random, Int64 min, Int64 max, Int32 count) where T : IRandom
		{
			if (random is null)
			{
				throw new ArgumentNullException(nameof(random));
			}

			switch (count)
			{
				case <= 0:
					return Enumerable.Empty<Int64>();
				case 1:
					return EnumerableUtilities.GetEnumerableFrom(NextInt64(random, min, max));
			}

			if (max == min)
			{
				return EnumerableUtilities.GetEnumerableFrom(min);
			}

			if (max < min)
			{
				(min, max) = (max, min);
			}

			UInt64 difference = max.DiscreteIncludeDifference(min, UInt64.MaxValue);
			count = (Int32) Math.Min((UInt32) count, difference);
			return count <= SByte.MaxValue + 1 || count / (Double) difference >= 0.8 ?
				UniqueShuffleRange(random, min, max, count) :
				UniqueSetRange(random, min, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IEnumerable<UInt64> UniqueSetRange(System.Random random, UInt64 min, UInt64 max)
		{
			Int32 difference = (Int32) Math.Min(max.DiscreteIncludeDifference(min, UInt64.MaxValue), Int32.MaxValue);
			HashSet<UInt64> set = new HashSet<UInt64>(Math.Min(difference, Byte.MaxValue));

			while (set.Count < difference)
			{
				UInt64 item = NextUInt64(random, min, max);
				if (set.Add(item))
				{
					yield return item;
				}
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IEnumerable<UInt64> UniqueSetRange<T>(T random, UInt64 min, UInt64 max) where T : IRandom
		{
			Int32 difference = (Int32) Math.Min(max.DiscreteIncludeDifference(min, UInt64.MaxValue), Int32.MaxValue);
			HashSet<UInt64> set = new HashSet<UInt64>(Math.Min(difference, Byte.MaxValue));

			while (set.Count < difference)
			{
				UInt64 item = NextUInt64(random, min, max);
				if (set.Add(item))
				{
					yield return item;
				}
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IEnumerable<UInt64> UniqueSetRange(System.Random random, UInt64 min, UInt64 max, Int32 count)
		{
			HashSet<UInt64> set = new HashSet<UInt64>(count);

			while (set.Count < count)
			{
				UInt64 item = NextUInt64(random, min, max);
				if (set.Add(item))
				{
					yield return item;
				}
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IEnumerable<UInt64> UniqueSetRange<T>(T random, UInt64 min, UInt64 max, Int32 count) where T : IRandom
		{
			HashSet<UInt64> set = new HashSet<UInt64>(count);

			while (set.Count < count)
			{
				UInt64 item = NextUInt64(random, min, max);
				if (set.Add(item))
				{
					yield return item;
				}
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IEnumerable<UInt64> UniqueShuffleRange(System.Random random, UInt64 min, UInt64 max)
		{
			return MathUtilities.RangeInclude(min, max).Shuffle(random);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IEnumerable<UInt64> UniqueShuffleRange<T>(T random, UInt64 min, UInt64 max) where T : IRandom
		{
			return MathUtilities.RangeInclude(min, max).Shuffle(random);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IEnumerable<UInt64> UniqueShuffleRange(System.Random random, UInt64 min, UInt64 max, Int32 count)
		{
			return UniqueShuffleRange(random, min, max).Take(count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IEnumerable<UInt64> UniqueShuffleRange<T>(T random, UInt64 min, UInt64 max, Int32 count) where T : IRandom
		{
			return UniqueShuffleRange(random, min, max).Take(count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt64> UniqueRange(UInt64 min, UInt64 max)
		{
			return UniqueRange(Generator, min, max);
		}

		public static IEnumerable<UInt64> UniqueRange(this System.Random random, UInt64 min, UInt64 max)
		{
			if (random is null)
			{
				throw new ArgumentNullException(nameof(random));
			}

			if (max == min)
			{
				return EnumerableUtilities.GetEnumerableFrom(min);
			}

			if (max < min)
			{
				(min, max) = (max, min);
			}

			UInt64 difference = max.DiscreteIncludeDifference(min, UInt64.MaxValue);
			return difference <= (Byte.MaxValue + 1) * 4 ?
				UniqueShuffleRange(random, min, max, (Int32) difference) :
				UniqueSetRange(random, min, max);
		}

		public static IEnumerable<UInt64> UniqueRange<T>(this T random, UInt64 min, UInt64 max) where T : IRandom
		{
			if (random is null)
			{
				throw new ArgumentNullException(nameof(random));
			}

			if (max == min)
			{
				return EnumerableUtilities.GetEnumerableFrom(min);
			}

			if (max < min)
			{
				(min, max) = (max, min);
			}

			UInt64 difference = max.DiscreteIncludeDifference(min, UInt64.MaxValue);
			return difference <= (Byte.MaxValue + 1) * 4 ?
				UniqueShuffleRange(random, min, max, (Int32) difference) :
				UniqueSetRange(random, min, max);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt64> UniqueRange(UInt64 min, UInt64 max, Int32 count)
		{
			return UniqueRange(Generator, min, max, count);
		}

		public static IEnumerable<UInt64> UniqueRange(this System.Random random, UInt64 min, UInt64 max, Int32 count)
		{
			if (random is null)
			{
				throw new ArgumentNullException(nameof(random));
			}

			switch (count)
			{
				case <= 0:
					return Enumerable.Empty<UInt64>();
				case 1:
					return EnumerableUtilities.GetEnumerableFrom(NextUInt64(random, min, max));
			}

			if (max == min)
			{
				return EnumerableUtilities.GetEnumerableFrom(min);
			}

			if (max < min)
			{
				(min, max) = (max, min);
			}

			UInt64 difference = max.DiscreteIncludeDifference(min, UInt64.MaxValue);
			count = (Int32) Math.Min((UInt32) count, difference);
			return count <= SByte.MaxValue + 1 || count / (Double) difference >= 0.8 ?
				UniqueShuffleRange(random, min, max, count) :
				UniqueSetRange(random, min, max, count);
		}

		public static IEnumerable<UInt64> UniqueRange<T>(this T random, UInt64 min, UInt64 max, Int32 count) where T : IRandom
		{
			if (random is null)
			{
				throw new ArgumentNullException(nameof(random));
			}

			switch (count)
			{
				case <= 0:
					return Enumerable.Empty<UInt64>();
				case 1:
					return EnumerableUtilities.GetEnumerableFrom(NextUInt64(random, min, max));
			}

			if (max == min)
			{
				return EnumerableUtilities.GetEnumerableFrom(min);
			}

			if (max < min)
			{
				(min, max) = (max, min);
			}

			UInt64 difference = max.DiscreteIncludeDifference(min, UInt64.MaxValue);
			count = (Int32) Math.Min((UInt32) count, difference);
			return count <= SByte.MaxValue + 1 || count / (Double) difference >= 0.8 ?
				UniqueShuffleRange(random, min, max, count) :
				UniqueSetRange(random, min, max, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IEnumerable<Single> UniqueSetRange(System.Random random, Single min, Single max, Byte digits, MidpointRounding rounding)
		{
			Int32 difference = (Int32) Math.Min(max.DiscreteIncludeDifference(min, digits, Decimal.MaxValue), Int32.MaxValue);
			HashSet<Single> set = new HashSet<Single>(Math.Min(difference, Byte.MaxValue));

			while (set.Count < difference)
			{
				Single item = NextSingle(random, min, max).Round(digits, rounding);
				if (set.Add(item))
				{
					yield return item;
				}
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IEnumerable<Single> UniqueSetRange<T>(T random, Single min, Single max, Byte digits, MidpointRounding rounding) where T : IRandom
		{
			Int32 difference = (Int32) Math.Min(max.DiscreteIncludeDifference(min, digits, Decimal.MaxValue), Int32.MaxValue);
			HashSet<Single> set = new HashSet<Single>(Math.Min(difference, Byte.MaxValue));

			while (set.Count < difference)
			{
				Single item = NextSingle(random, min, max).Round(digits, rounding);
				if (set.Add(item))
				{
					yield return item;
				}
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IEnumerable<Single> UniqueSetRange(System.Random random, Single min, Single max, Byte digits, MidpointRounding rounding, Int32 count)
		{
			HashSet<Single> set = new HashSet<Single>(count);

			while (set.Count < count)
			{
				Single item = NextSingle(random, min, max).Round(digits, rounding);
				if (set.Add(item))
				{
					yield return item;
				}
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IEnumerable<Single> UniqueSetRange<T>(T random, Single min, Single max, Byte digits, MidpointRounding rounding, Int32 count) where T : IRandom
		{
			HashSet<Single> set = new HashSet<Single>(count);

			while (set.Count < count)
			{
				Single item = NextSingle(random, min, max).Round(digits, rounding);
				if (set.Add(item))
				{
					yield return item;
				}
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Single> UniqueRange(Single min, Single max)
		{
			return UniqueRange(min, max, 0);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Single> UniqueRange(Single min, Single max, Byte digits)
		{
			return UniqueRange(min, max, digits, MathUtilities.DefaultRoundType);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Single> UniqueRange(Single min, Single max, Byte digits, MidpointRounding rounding)
		{
			return UniqueRange(Generator, min, max, digits, rounding);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Single> UniqueRange(this System.Random random, Single min, Single max)
		{
			return UniqueRange(random, min, max, 0);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Single> UniqueRange(this System.Random random, Single min, Single max, Byte digits)
		{
			return UniqueRange(random, min, max, digits, MathUtilities.DefaultRoundType);
		}

		public static IEnumerable<Single> UniqueRange(this System.Random random, Single min, Single max, Byte digits, MidpointRounding rounding)
		{
			if (random is null)
			{
				throw new ArgumentNullException(nameof(random));
			}

			if (Math.Abs(max - min) < Single.Epsilon)
			{
				return EnumerableUtilities.GetEnumerableFrom(min);
			}

			if (max < min)
			{
				(min, max) = (max, min);
			}

			MathUtilities.ToRange(ref digits, 0, 15);

			Decimal difference = max.DiscreteIncludeDifference(min, digits, Decimal.MaxValue);
			return difference <= (Byte.MaxValue + 1) * 4 ? UniqueSetRange(random, min, max, digits, rounding, (Int32) difference) : UniqueSetRange(random, min, max, digits, rounding);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Single> UniqueRange<T>(this T random, Single min, Single max) where T : IRandom
		{
			return UniqueRange(random, min, max, 0);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Single> UniqueRange<T>(this T random, Single min, Single max, Byte digits) where T : IRandom
		{
			return UniqueRange(random, min, max, digits, MathUtilities.DefaultRoundType);
		}

		public static IEnumerable<Single> UniqueRange<T>(this T random, Single min, Single max, Byte digits, MidpointRounding rounding) where T : IRandom
		{
			if (random is null)
			{
				throw new ArgumentNullException(nameof(random));
			}

			if (Math.Abs(max - min) < Single.Epsilon)
			{
				return EnumerableUtilities.GetEnumerableFrom(min);
			}

			if (max < min)
			{
				(min, max) = (max, min);
			}

			MathUtilities.ToRange(ref digits, 0, 15);

			Decimal difference = max.DiscreteIncludeDifference(min, digits, Decimal.MaxValue);
			return difference <= (Byte.MaxValue + 1) * 4 ? UniqueSetRange(random, min, max, digits, rounding, (Int32) difference) : UniqueSetRange(random, min, max, digits, rounding);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Single> UniqueRange(Single min, Single max, Int32 count)
		{
			return UniqueRange(min, max, 0, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Single> UniqueRange(Single min, Single max, Byte digits, Int32 count)
		{
			return UniqueRange(min, max, digits, MathUtilities.DefaultRoundType, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Single> UniqueRange(Single min, Single max, Byte digits, MidpointRounding rounding, Int32 count)
		{
			return UniqueRange(Generator, min, max, digits, rounding, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Single> UniqueRange(this System.Random random, Single min, Single max, Int32 count)
		{
			return UniqueRange(random, min, max, 0, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Single> UniqueRange(this System.Random random, Single min, Single max, Byte digits, Int32 count)
		{
			return UniqueRange(random, min, max, digits, MathUtilities.DefaultRoundType, count);
		}

		public static IEnumerable<Single> UniqueRange(this System.Random random, Single min, Single max, Byte digits, MidpointRounding rounding, Int32 count)
		{
			if (random is null)
			{
				throw new ArgumentNullException(nameof(random));
			}

			switch (count)
			{
				case <= 0:
					return Enumerable.Empty<Single>();
				case 1:
					return EnumerableUtilities.GetEnumerableFrom(NextSingle(random, min, max));
			}

			if (Math.Abs(max - min) < Single.Epsilon)
			{
				return EnumerableUtilities.GetEnumerableFrom(min);
			}

			if (max < min)
			{
				(min, max) = (max, min);
			}

			MathUtilities.ToRange(ref digits, 0, 15);

			return UniqueSetRange(random, min, max, digits, rounding, (Int32) Math.Min(count, max.DiscreteIncludeDifference(min, digits, Decimal.MaxValue)));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Single> UniqueRange<T>(this T random, Single min, Single max, Int32 count) where T : IRandom
		{
			return UniqueRange(random, min, max, 0, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Single> UniqueRange<T>(this T random, Single min, Single max, Byte digits, Int32 count) where T : IRandom
		{
			return UniqueRange(random, min, max, digits, MathUtilities.DefaultRoundType, count);
		}

		public static IEnumerable<Single> UniqueRange<T>(this T random, Single min, Single max, Byte digits, MidpointRounding rounding, Int32 count) where T : IRandom
		{
			if (random is null)
			{
				throw new ArgumentNullException(nameof(random));
			}

			switch (count)
			{
				case <= 0:
					return Enumerable.Empty<Single>();
				case 1:
					return EnumerableUtilities.GetEnumerableFrom(NextSingle(random, min, max));
			}

			if (Math.Abs(max - min) < Single.Epsilon)
			{
				return EnumerableUtilities.GetEnumerableFrom(min);
			}

			if (max < min)
			{
				(min, max) = (max, min);
			}

			MathUtilities.ToRange(ref digits, 0, 15);

			return UniqueSetRange(random, min, max, digits, rounding, (Int32) Math.Min(count, max.DiscreteIncludeDifference(min, digits, Decimal.MaxValue)));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IEnumerable<Double> UniqueSetRange(System.Random random, Double min, Double max, Byte digits, MidpointRounding rounding)
		{
			Int32 difference = (Int32) Math.Min(max.DiscreteIncludeDifference(min, digits, Decimal.MaxValue), Int32.MaxValue);
			HashSet<Double> set = new HashSet<Double>(Math.Min(difference, Byte.MaxValue));

			while (set.Count < difference)
			{
				Double item = NextDouble(random, min, max).Round(digits, rounding);
				if (set.Add(item))
				{
					yield return item;
				}
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IEnumerable<Double> UniqueSetRange<T>(T random, Double min, Double max, Byte digits, MidpointRounding rounding) where T : IRandom
		{
			Int32 difference = (Int32) Math.Min(max.DiscreteIncludeDifference(min, digits, Decimal.MaxValue), Int32.MaxValue);
			HashSet<Double> set = new HashSet<Double>(Math.Min(difference, Byte.MaxValue));

			while (set.Count < difference)
			{
				Double item = NextDouble(random, min, max).Round(digits, rounding);
				if (set.Add(item))
				{
					yield return item;
				}
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IEnumerable<Double> UniqueSetRange(System.Random random, Double min, Double max, Byte digits, MidpointRounding rounding, Int32 count)
		{
			HashSet<Double> set = new HashSet<Double>(count);

			while (set.Count < count)
			{
				Double item = NextDouble(random, min, max).Round(digits, rounding);
				if (set.Add(item))
				{
					yield return item;
				}
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IEnumerable<Double> UniqueSetRange<T>(T random, Double min, Double max, Byte digits, MidpointRounding rounding, Int32 count) where T : IRandom
		{
			HashSet<Double> set = new HashSet<Double>(count);

			while (set.Count < count)
			{
				Double item = NextDouble(random, min, max).Round(digits, rounding);
				if (set.Add(item))
				{
					yield return item;
				}
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Double> UniqueRange(Double min, Double max)
		{
			return UniqueRange(min, max, 0);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Double> UniqueRange(Double min, Double max, Byte digits)
		{
			return UniqueRange(min, max, digits, MathUtilities.DefaultRoundType);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Double> UniqueRange(Double min, Double max, Byte digits, MidpointRounding rounding)
		{
			return UniqueRange(Generator, min, max, digits, rounding);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Double> UniqueRange(this System.Random random, Double min, Double max)
		{
			return UniqueRange(random, min, max, 0);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Double> UniqueRange(this System.Random random, Double min, Double max, Byte digits)
		{
			return UniqueRange(random, min, max, digits, MathUtilities.DefaultRoundType);
		}

		public static IEnumerable<Double> UniqueRange(this System.Random random, Double min, Double max, Byte digits, MidpointRounding rounding)
		{
			if (random is null)
			{
				throw new ArgumentNullException(nameof(random));
			}

			if (Math.Abs(max - min) < Double.Epsilon)
			{
				return EnumerableUtilities.GetEnumerableFrom(min);
			}

			if (max < min)
			{
				(min, max) = (max, min);
			}

			MathUtilities.ToRange(ref digits, 0, 15);

			Decimal difference = max.DiscreteIncludeDifference(min, digits, Decimal.MaxValue);
			return difference <= (Byte.MaxValue + 1) * 4 ? UniqueSetRange(random, min, max, digits, rounding, (Int32) difference) : UniqueSetRange(random, min, max, digits, rounding);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Double> UniqueRange<T>(this T random, Double min, Double max) where T : IRandom
		{
			return UniqueRange(random, min, max, 0);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Double> UniqueRange<T>(this T random, Double min, Double max, Byte digits) where T : IRandom
		{
			return UniqueRange(random, min, max, digits, MathUtilities.DefaultRoundType);
		}

		public static IEnumerable<Double> UniqueRange<T>(this T random, Double min, Double max, Byte digits, MidpointRounding rounding) where T : IRandom
		{
			if (random is null)
			{
				throw new ArgumentNullException(nameof(random));
			}

			if (Math.Abs(max - min) < Double.Epsilon)
			{
				return EnumerableUtilities.GetEnumerableFrom(min);
			}

			if (max < min)
			{
				(min, max) = (max, min);
			}

			MathUtilities.ToRange(ref digits, 0, 15);

			Decimal difference = max.DiscreteIncludeDifference(min, digits, Decimal.MaxValue);
			return difference <= (Byte.MaxValue + 1) * 4 ? UniqueSetRange(random, min, max, digits, rounding, (Int32) difference) : UniqueSetRange(random, min, max, digits, rounding);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Double> UniqueRange(Double min, Double max, Int32 count)
		{
			return UniqueRange(min, max, 0, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Double> UniqueRange(Double min, Double max, Byte digits, Int32 count)
		{
			return UniqueRange(min, max, digits, MathUtilities.DefaultRoundType, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Double> UniqueRange(Double min, Double max, Byte digits, MidpointRounding rounding, Int32 count)
		{
			return UniqueRange(Generator, min, max, digits, rounding, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Double> UniqueRange(this System.Random random, Double min, Double max, Int32 count)
		{
			return UniqueRange(random, min, max, 0, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Double> UniqueRange(this System.Random random, Double min, Double max, Byte digits, Int32 count)
		{
			return UniqueRange(random, min, max, digits, MathUtilities.DefaultRoundType, count);
		}

		public static IEnumerable<Double> UniqueRange(this System.Random random, Double min, Double max, Byte digits, MidpointRounding rounding, Int32 count)
		{
			if (random is null)
			{
				throw new ArgumentNullException(nameof(random));
			}

			switch (count)
			{
				case <= 0:
					return Enumerable.Empty<Double>();
				case 1:
					return EnumerableUtilities.GetEnumerableFrom(NextDouble(random, min, max));
			}

			if (Math.Abs(max - min) < Double.Epsilon)
			{
				return EnumerableUtilities.GetEnumerableFrom(min);
			}

			if (max < min)
			{
				(min, max) = (max, min);
			}

			MathUtilities.ToRange(ref digits, 0, 15);

			return UniqueSetRange(random, min, max, digits, rounding, (Int32) Math.Min(count, max.DiscreteIncludeDifference(min, digits, Decimal.MaxValue)));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Double> UniqueRange<T>(this T random, Double min, Double max, Int32 count) where T : IRandom
		{
			return UniqueRange(random, min, max, 0, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Double> UniqueRange<T>(this T random, Double min, Double max, Byte digits, Int32 count) where T : IRandom
		{
			return UniqueRange(random, min, max, digits, MathUtilities.DefaultRoundType, count);
		}

		public static IEnumerable<Double> UniqueRange<T>(this T random, Double min, Double max, Byte digits, MidpointRounding rounding, Int32 count) where T : IRandom
		{
			if (random is null)
			{
				throw new ArgumentNullException(nameof(random));
			}

			switch (count)
			{
				case <= 0:
					return Enumerable.Empty<Double>();
				case 1:
					return EnumerableUtilities.GetEnumerableFrom(NextDouble(random, min, max));
			}

			if (Math.Abs(max - min) < Double.Epsilon)
			{
				return EnumerableUtilities.GetEnumerableFrom(min);
			}

			if (max < min)
			{
				(min, max) = (max, min);
			}

			MathUtilities.ToRange(ref digits, 0, 15);

			return UniqueSetRange(random, min, max, digits, rounding, (Int32) Math.Min(count, max.DiscreteIncludeDifference(min, digits, Decimal.MaxValue)));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IEnumerable<Decimal> UniqueSetRange(System.Random random, Decimal min, Decimal max, Byte digits, MidpointRounding rounding)
		{
			Int32 difference = (Int32) Math.Min(max.DiscreteIncludeDifference(min, digits, Decimal.MaxValue), Int32.MaxValue);
			HashSet<Decimal> set = new HashSet<Decimal>(Math.Min(difference, Byte.MaxValue));

			while (set.Count < difference)
			{
				Decimal item = NextDecimal(random, min, max).Round(digits, rounding);
				if (set.Add(item))
				{
					yield return item;
				}
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IEnumerable<Decimal> UniqueSetRange<T>(T random, Decimal min, Decimal max, Byte digits, MidpointRounding rounding) where T : IRandom
		{
			Int32 difference = (Int32) Math.Min(max.DiscreteIncludeDifference(min, digits, Decimal.MaxValue), Int32.MaxValue);
			HashSet<Decimal> set = new HashSet<Decimal>(Math.Min(difference, Byte.MaxValue));

			while (set.Count < difference)
			{
				Decimal item = NextDecimal(random, min, max).Round(digits, rounding);
				if (set.Add(item))
				{
					yield return item;
				}
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IEnumerable<Decimal> UniqueSetRange(System.Random random, Decimal min, Decimal max, Byte digits, MidpointRounding rounding, Int32 count)
		{
			HashSet<Decimal> set = new HashSet<Decimal>(count);

			while (set.Count < count)
			{
				Decimal item = NextDecimal(random, min, max).Round(digits, rounding);
				if (set.Add(item))
				{
					yield return item;
				}
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IEnumerable<Decimal> UniqueSetRange<T>(T random, Decimal min, Decimal max, Byte digits, MidpointRounding rounding, Int32 count) where T : IRandom
		{
			HashSet<Decimal> set = new HashSet<Decimal>(count);

			while (set.Count < count)
			{
				Decimal item = NextDecimal(random, min, max).Round(digits, rounding);
				if (set.Add(item))
				{
					yield return item;
				}
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Decimal> UniqueRange(Decimal min, Decimal max)
		{
			return UniqueRange(min, max, 0);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Decimal> UniqueRange(Decimal min, Decimal max, Byte digits)
		{
			return UniqueRange(min, max, digits, MathUtilities.DefaultRoundType);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Decimal> UniqueRange(Decimal min, Decimal max, Byte digits, MidpointRounding rounding)
		{
			return UniqueRange(Generator, min, max, digits, rounding);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Decimal> UniqueRange(this System.Random random, Decimal min, Decimal max)
		{
			return UniqueRange(random, min, max, 0);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Decimal> UniqueRange(this System.Random random, Decimal min, Decimal max, Byte digits)
		{
			return UniqueRange(random, min, max, digits, MathUtilities.DefaultRoundType);
		}

		public static IEnumerable<Decimal> UniqueRange(this System.Random random, Decimal min, Decimal max, Byte digits, MidpointRounding rounding)
		{
			if (random is null)
			{
				throw new ArgumentNullException(nameof(random));
			}

			if (max == min)
			{
				return EnumerableUtilities.GetEnumerableFrom(min);
			}

			if (max < min)
			{
				(min, max) = (max, min);
			}

			MathUtilities.ToRange(ref digits, 0, 15);

			Decimal difference = max.DiscreteIncludeDifference(min, digits, Decimal.MaxValue);
			return difference <= (Byte.MaxValue + 1) * 4 ? UniqueSetRange(random, min, max, digits, rounding, (Int32) difference) : UniqueSetRange(random, min, max, digits, rounding);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Decimal> UniqueRange<T>(this T random, Decimal min, Decimal max) where T : IRandom
		{
			return UniqueRange(random, min, max, 0);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Decimal> UniqueRange<T>(this T random, Decimal min, Decimal max, Byte digits) where T : IRandom
		{
			return UniqueRange(random, min, max, digits, MathUtilities.DefaultRoundType);
		}

		public static IEnumerable<Decimal> UniqueRange<T>(this T random, Decimal min, Decimal max, Byte digits, MidpointRounding rounding) where T : IRandom
		{
			if (random is null)
			{
				throw new ArgumentNullException(nameof(random));
			}

			if (max == min)
			{
				return EnumerableUtilities.GetEnumerableFrom(min);
			}

			if (max < min)
			{
				(min, max) = (max, min);
			}

			MathUtilities.ToRange(ref digits, 0, 15);

			Decimal difference = max.DiscreteIncludeDifference(min, digits, Decimal.MaxValue);
			return difference <= (Byte.MaxValue + 1) * 4 ? UniqueSetRange(random, min, max, digits, rounding, (Int32) difference) : UniqueSetRange(random, min, max, digits, rounding);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Decimal> UniqueRange(Decimal min, Decimal max, Int32 count)
		{
			return UniqueRange(min, max, 0, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Decimal> UniqueRange(Decimal min, Decimal max, Byte digits, Int32 count)
		{
			return UniqueRange(min, max, digits, MathUtilities.DefaultRoundType, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Decimal> UniqueRange(Decimal min, Decimal max, Byte digits, MidpointRounding rounding, Int32 count)
		{
			return UniqueRange(Generator, min, max, digits, rounding, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Decimal> UniqueRange(this System.Random random, Decimal min, Decimal max, Int32 count)
		{
			return UniqueRange(random, min, max, 0, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Decimal> UniqueRange(this System.Random random, Decimal min, Decimal max, Byte digits, Int32 count)
		{
			return UniqueRange(random, min, max, digits, MathUtilities.DefaultRoundType, count);
		}

		public static IEnumerable<Decimal> UniqueRange(this System.Random random, Decimal min, Decimal max, Byte digits, MidpointRounding rounding, Int32 count)
		{
			if (random is null)
			{
				throw new ArgumentNullException(nameof(random));
			}

			switch (count)
			{
				case <= 0:
					return Enumerable.Empty<Decimal>();
				case 1:
					return EnumerableUtilities.GetEnumerableFrom(NextDecimal(random, min, max));
			}

			if (max == min)
			{
				return EnumerableUtilities.GetEnumerableFrom(min);
			}

			if (max < min)
			{
				(min, max) = (max, min);
			}

			MathUtilities.ToRange(ref digits, 0, 15);

			return UniqueSetRange(random, min, max, digits, rounding, (Int32) Math.Min(count, max.DiscreteIncludeDifference(min, digits, Decimal.MaxValue)));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Decimal> UniqueRange<T>(this T random, Decimal min, Decimal max, Int32 count) where T : IRandom
		{
			return UniqueRange(random, min, max, 0, count);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Decimal> UniqueRange<T>(this T random, Decimal min, Decimal max, Byte digits, Int32 count) where T : IRandom
		{
			return UniqueRange(random, min, max, digits, MathUtilities.DefaultRoundType, count);
		}

		public static IEnumerable<Decimal> UniqueRange<T>(this T random, Decimal min, Decimal max, Byte digits, MidpointRounding rounding, Int32 count) where T : IRandom
		{
			if (random is null)
			{
				throw new ArgumentNullException(nameof(random));
			}

			switch (count)
			{
				case <= 0:
					return Enumerable.Empty<Decimal>();
				case 1:
					return EnumerableUtilities.GetEnumerableFrom(NextDecimal(random, min, max));
			}

			if (max == min)
			{
				return EnumerableUtilities.GetEnumerableFrom(min);
			}

			if (max < min)
			{
				(min, max) = (max, min);
			}

			MathUtilities.ToRange(ref digits, 0, 15);

			return UniqueSetRange(random, min, max, digits, rounding, (Int32) Math.Min(count, max.DiscreteIncludeDifference(min, digits, Decimal.MaxValue)));
		}

    }
}