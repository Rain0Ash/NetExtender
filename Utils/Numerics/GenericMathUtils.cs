// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Numerics;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;

namespace NetExtender.Utils.Numerics
{
    [SuppressMessage("ReSharper", "RedundantOverflowCheckingContext")]
    public static partial class MathUtils
    {
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
			return Range((SByte)0, stop);
		}

		public static IEnumerable<SByte> Range(SByte start, SByte stop, SByte step = 1)
		{
			if (step == 0)
			{
				throw new ArgumentException("Step cannot be equals zero.");
			}

			if (start < stop && step > 0)
			{
				for (SByte i = start; i < stop; i += step)
				{
					yield return i;
				}
			}
			else if (start > stop && step < 0)
			{
				for (SByte i = start; i > stop; i += step)
				{
					yield return i;
				}
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Byte> Range(Byte stop)
		{
			return Range((Byte)0, stop);
		}

		public static IEnumerable<Byte> Range(Byte start, Byte stop, Byte step = 1)
		{
			if (step == 0)
			{
				throw new ArgumentException("Step cannot be equals zero.");
			}

			for (Byte i = start; i < stop; i += step)
			{
				yield return i;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int16> Range(Int16 stop)
		{
			return Range((Int16)0, stop);
		}

		public static IEnumerable<Int16> Range(Int16 start, Int16 stop, Int16 step = 1)
		{
			if (step == 0)
			{
				throw new ArgumentException("Step cannot be equals zero.");
			}

			if (start < stop && step > 0)
			{
				for (Int16 i = start; i < stop; i += step)
				{
					yield return i;
				}
			}
			else if (start > stop && step < 0)
			{
				for (Int16 i = start; i > stop; i += step)
				{
					yield return i;
				}
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt16> Range(UInt16 stop)
		{
			return Range((UInt16)0, stop);
		}

		public static IEnumerable<UInt16> Range(UInt16 start, UInt16 stop, UInt16 step = 1)
		{
			if (step == 0)
			{
				throw new ArgumentException("Step cannot be equals zero.");
			}

			for (UInt16 i = start; i < stop; i += step)
			{
				yield return i;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int32> Range(Int32 stop)
		{
			return Range(0, stop);
		}

		public static IEnumerable<Int32> Range(Int32 start, Int32 stop, Int32 step = 1)
		{
			if (step == 0)
			{
				throw new ArgumentException("Step cannot be equals zero.");
			}

			if (start < stop && step > 0)
			{
				for (Int32 i = start; i < stop; i += step)
				{
					yield return i;
				}
			}
			else if (start > stop && step < 0)
			{
				for (Int32 i = start; i > stop; i += step)
				{
					yield return i;
				}
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt32> Range(UInt32 stop)
		{
			return Range(0, stop);
		}

		public static IEnumerable<UInt32> Range(UInt32 start, UInt32 stop, UInt32 step = 1)
		{
			if (step == 0)
			{
				throw new ArgumentException("Step cannot be equals zero.");
			}

			for (UInt32 i = start; i < stop; i += step)
			{
				yield return i;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Int64> Range(Int64 stop)
		{
			return Range(0, stop);
		}

		public static IEnumerable<Int64> Range(Int64 start, Int64 stop, Int64 step = 1)
		{
			if (step == 0)
			{
				throw new ArgumentException("Step cannot be equals zero.");
			}

			if (start < stop && step > 0)
			{
				for (Int64 i = start; i < stop; i += step)
				{
					yield return i;
				}
			}
			else if (start > stop && step < 0)
			{
				for (Int64 i = start; i > stop; i += step)
				{
					yield return i;
				}
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<UInt64> Range(UInt64 stop)
		{
			return Range(0, stop);
		}

		public static IEnumerable<UInt64> Range(UInt64 start, UInt64 stop, UInt64 step = 1)
		{
			if (step == 0)
			{
				throw new ArgumentException("Step cannot be equals zero.");
			}

			for (UInt64 i = start; i < stop; i += step)
			{
				yield return i;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Single> Range(Single stop)
		{
			return Range(0, stop);
		}

		public static IEnumerable<Single> Range(Single start, Single stop, Single step = 1)
		{
			if (Math.Abs(step) < Single.Epsilon)
			{
				throw new ArgumentException("Step cannot be equals zero.");
			}

			if (start < stop && step > 0)
			{
				for (Single i = start; i < stop; i += step)
				{
					yield return i;
				}
			}
			else if (start > stop && step < 0)
			{
				for (Single i = start; i > stop; i += step)
				{
					yield return i;
				}
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Double> Range(Double stop)
		{
			return Range(0, stop);
		}

		public static IEnumerable<Double> Range(Double start, Double stop, Double step = 1)
		{
			if (Math.Abs(step) < Double.Epsilon)
			{
				throw new ArgumentException("Step cannot be equals zero.");
			}

			if (start < stop && step > 0)
			{
				for (Double i = start; i < stop; i += step)
				{
					yield return i;
				}
			}
			else if (start > stop && step < 0)
			{
				for (Double i = start; i > stop; i += step)
				{
					yield return i;
				}
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<Decimal> Range(Decimal stop)
		{
			return Range(0, stop);
		}

		public static IEnumerable<Decimal> Range(Decimal start, Decimal stop, Decimal step = 1)
		{
			if (step == 0)
			{
				throw new ArgumentException("Step cannot be equals zero.");
			}

			if (start < stop && step > 0)
			{
				for (Decimal i = start; i < stop; i += step)
				{
					yield return i;
				}
			}
			else if (start > stop && step < 0)
			{
				for (Decimal i = start; i > stop; i += step)
				{
					yield return i;
				}
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void ToRange(ref Char value, Char minimum = Char.MinValue, Char maximum = Char.MaxValue, Boolean looped = false)
		{
			 value = ToRange(value, minimum, maximum, looped);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Char ToRange(this Char value, Char minimum = Char.MinValue, Char maximum = Char.MaxValue, Boolean looped = false)
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
		public static void ToRange(ref SByte value, SByte minimum = 0, SByte maximum = SByte.MaxValue, Boolean looped = false)
		{
			 value = ToRange(value, minimum, maximum, looped);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static SByte ToRange(this SByte value, SByte minimum = 0, SByte maximum = SByte.MaxValue, Boolean looped = false)
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
		public static void ToRange(ref Byte value, Byte minimum = 0, Byte maximum = Byte.MaxValue, Boolean looped = false)
		{
			 value = ToRange(value, minimum, maximum, looped);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Byte ToRange(this Byte value, Byte minimum = 0, Byte maximum = Byte.MaxValue, Boolean looped = false)
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
		public static void ToRange(ref Int16 value, Int16 minimum = 0, Int16 maximum = Int16.MaxValue, Boolean looped = false)
		{
			 value = ToRange(value, minimum, maximum, looped);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int16 ToRange(this Int16 value, Int16 minimum = 0, Int16 maximum = Int16.MaxValue, Boolean looped = false)
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
		public static void ToRange(ref UInt16 value, UInt16 minimum = 0, UInt16 maximum = UInt16.MaxValue, Boolean looped = false)
		{
			 value = ToRange(value, minimum, maximum, looped);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static UInt16 ToRange(this UInt16 value, UInt16 minimum = 0, UInt16 maximum = UInt16.MaxValue, Boolean looped = false)
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
		public static void ToRange(ref Int32 value, Int32 minimum = 0, Int32 maximum = Int32.MaxValue, Boolean looped = false)
		{
			 value = ToRange(value, minimum, maximum, looped);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int32 ToRange(this Int32 value, Int32 minimum = 0, Int32 maximum = Int32.MaxValue, Boolean looped = false)
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
		public static void ToRange(ref UInt32 value, UInt32 minimum = 0, UInt32 maximum = UInt32.MaxValue, Boolean looped = false)
		{
			 value = ToRange(value, minimum, maximum, looped);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static UInt32 ToRange(this UInt32 value, UInt32 minimum = 0, UInt32 maximum = UInt32.MaxValue, Boolean looped = false)
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
		public static void ToRange(ref Int64 value, Int64 minimum = 0, Int64 maximum = Int64.MaxValue, Boolean looped = false)
		{
			 value = ToRange(value, minimum, maximum, looped);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int64 ToRange(this Int64 value, Int64 minimum = 0, Int64 maximum = Int64.MaxValue, Boolean looped = false)
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
		public static void ToRange(ref UInt64 value, UInt64 minimum = 0, UInt64 maximum = UInt64.MaxValue, Boolean looped = false)
		{
			 value = ToRange(value, minimum, maximum, looped);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static UInt64 ToRange(this UInt64 value, UInt64 minimum = 0, UInt64 maximum = UInt64.MaxValue, Boolean looped = false)
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
		public static void ToRange(ref Single value, Single minimum = 0, Single maximum = Single.MaxValue, Boolean looped = false)
		{
			 value = ToRange(value, minimum, maximum, looped);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Single ToRange(this Single value, Single minimum = 0, Single maximum = Single.MaxValue, Boolean looped = false)
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
		public static void ToRange(ref Double value, Double minimum = 0, Double maximum = Double.MaxValue, Boolean looped = false)
		{
			 value = ToRange(value, minimum, maximum, looped);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Double ToRange(this Double value, Double minimum = 0, Double maximum = Double.MaxValue, Boolean looped = false)
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
		public static void ToRange(ref Decimal value, Decimal minimum = 0, Decimal maximum = Decimal.MaxValue, Boolean looped = false)
		{
			 value = ToRange(value, minimum, maximum, looped);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Decimal ToRange(this Decimal value, Decimal minimum = 0, Decimal maximum = Decimal.MaxValue, Boolean looped = false)
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
		public static Boolean InRange(this Char value, MathPositionType comparison = MathPositionType.Both)
		{
			return InRange(value, default, Char.MaxValue, comparison);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Boolean InRange(this Char value, Char maximum, MathPositionType comparison = MathPositionType.Both)
		{
			return InRange(value, default, maximum, comparison);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Boolean InRange(this Char value, Char minimum, Char maximum, MathPositionType comparison = MathPositionType.Both)
		{
			return comparison switch
			{
				MathPositionType.None => value > minimum && value < maximum,
				MathPositionType.Left => value >= minimum && value < maximum,
				MathPositionType.Right => value > minimum && value <= maximum,
				MathPositionType.Both => value >= minimum && value <= maximum,
				_ => throw new NotSupportedException(comparison.ToString())
			};
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Boolean InRange(this SByte value, MathPositionType comparison = MathPositionType.Both)
		{
			return InRange(value, default, SByte.MaxValue, comparison);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Boolean InRange(this SByte value, SByte maximum, MathPositionType comparison = MathPositionType.Both)
		{
			return InRange(value, default, maximum, comparison);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Boolean InRange(this SByte value, SByte minimum, SByte maximum, MathPositionType comparison = MathPositionType.Both)
		{
			return comparison switch
			{
				MathPositionType.None => value > minimum && value < maximum,
				MathPositionType.Left => value >= minimum && value < maximum,
				MathPositionType.Right => value > minimum && value <= maximum,
				MathPositionType.Both => value >= minimum && value <= maximum,
				_ => throw new NotSupportedException(comparison.ToString())
			};
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Boolean InRange(this Byte value, MathPositionType comparison = MathPositionType.Both)
		{
			return InRange(value, default, Byte.MaxValue, comparison);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Boolean InRange(this Byte value, Byte maximum, MathPositionType comparison = MathPositionType.Both)
		{
			return InRange(value, default, maximum, comparison);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Boolean InRange(this Byte value, Byte minimum, Byte maximum, MathPositionType comparison = MathPositionType.Both)
		{
			return comparison switch
			{
				MathPositionType.None => value > minimum && value < maximum,
				MathPositionType.Left => value >= minimum && value < maximum,
				MathPositionType.Right => value > minimum && value <= maximum,
				MathPositionType.Both => value >= minimum && value <= maximum,
				_ => throw new NotSupportedException(comparison.ToString())
			};
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Boolean InRange(this Int16 value, MathPositionType comparison = MathPositionType.Both)
		{
			return InRange(value, default, Int16.MaxValue, comparison);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Boolean InRange(this Int16 value, Int16 maximum, MathPositionType comparison = MathPositionType.Both)
		{
			return InRange(value, default, maximum, comparison);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Boolean InRange(this Int16 value, Int16 minimum, Int16 maximum, MathPositionType comparison = MathPositionType.Both)
		{
			return comparison switch
			{
				MathPositionType.None => value > minimum && value < maximum,
				MathPositionType.Left => value >= minimum && value < maximum,
				MathPositionType.Right => value > minimum && value <= maximum,
				MathPositionType.Both => value >= minimum && value <= maximum,
				_ => throw new NotSupportedException(comparison.ToString())
			};
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Boolean InRange(this UInt16 value, MathPositionType comparison = MathPositionType.Both)
		{
			return InRange(value, default, UInt16.MaxValue, comparison);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Boolean InRange(this UInt16 value, UInt16 maximum, MathPositionType comparison = MathPositionType.Both)
		{
			return InRange(value, default, maximum, comparison);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Boolean InRange(this UInt16 value, UInt16 minimum, UInt16 maximum, MathPositionType comparison = MathPositionType.Both)
		{
			return comparison switch
			{
				MathPositionType.None => value > minimum && value < maximum,
				MathPositionType.Left => value >= minimum && value < maximum,
				MathPositionType.Right => value > minimum && value <= maximum,
				MathPositionType.Both => value >= minimum && value <= maximum,
				_ => throw new NotSupportedException(comparison.ToString())
			};
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Boolean InRange(this Int32 value, MathPositionType comparison = MathPositionType.Both)
		{
			return InRange(value, default, Int32.MaxValue, comparison);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Boolean InRange(this Int32 value, Int32 maximum, MathPositionType comparison = MathPositionType.Both)
		{
			return InRange(value, default, maximum, comparison);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Boolean InRange(this Int32 value, Int32 minimum, Int32 maximum, MathPositionType comparison = MathPositionType.Both)
		{
			return comparison switch
			{
				MathPositionType.None => value > minimum && value < maximum,
				MathPositionType.Left => value >= minimum && value < maximum,
				MathPositionType.Right => value > minimum && value <= maximum,
				MathPositionType.Both => value >= minimum && value <= maximum,
				_ => throw new NotSupportedException(comparison.ToString())
			};
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Boolean InRange(this UInt32 value, MathPositionType comparison = MathPositionType.Both)
		{
			return InRange(value, default, UInt32.MaxValue, comparison);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Boolean InRange(this UInt32 value, UInt32 maximum, MathPositionType comparison = MathPositionType.Both)
		{
			return InRange(value, default, maximum, comparison);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Boolean InRange(this UInt32 value, UInt32 minimum, UInt32 maximum, MathPositionType comparison = MathPositionType.Both)
		{
			return comparison switch
			{
				MathPositionType.None => value > minimum && value < maximum,
				MathPositionType.Left => value >= minimum && value < maximum,
				MathPositionType.Right => value > minimum && value <= maximum,
				MathPositionType.Both => value >= minimum && value <= maximum,
				_ => throw new NotSupportedException(comparison.ToString())
			};
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Boolean InRange(this Int64 value, MathPositionType comparison = MathPositionType.Both)
		{
			return InRange(value, default, Int64.MaxValue, comparison);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Boolean InRange(this Int64 value, Int64 maximum, MathPositionType comparison = MathPositionType.Both)
		{
			return InRange(value, default, maximum, comparison);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Boolean InRange(this Int64 value, Int64 minimum, Int64 maximum, MathPositionType comparison = MathPositionType.Both)
		{
			return comparison switch
			{
				MathPositionType.None => value > minimum && value < maximum,
				MathPositionType.Left => value >= minimum && value < maximum,
				MathPositionType.Right => value > minimum && value <= maximum,
				MathPositionType.Both => value >= minimum && value <= maximum,
				_ => throw new NotSupportedException(comparison.ToString())
			};
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Boolean InRange(this UInt64 value, MathPositionType comparison = MathPositionType.Both)
		{
			return InRange(value, default, UInt64.MaxValue, comparison);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Boolean InRange(this UInt64 value, UInt64 maximum, MathPositionType comparison = MathPositionType.Both)
		{
			return InRange(value, default, maximum, comparison);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Boolean InRange(this UInt64 value, UInt64 minimum, UInt64 maximum, MathPositionType comparison = MathPositionType.Both)
		{
			return comparison switch
			{
				MathPositionType.None => value > minimum && value < maximum,
				MathPositionType.Left => value >= minimum && value < maximum,
				MathPositionType.Right => value > minimum && value <= maximum,
				MathPositionType.Both => value >= minimum && value <= maximum,
				_ => throw new NotSupportedException(comparison.ToString())
			};
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Boolean InRange(this Single value, MathPositionType comparison = MathPositionType.Both)
		{
			return InRange(value, default, Single.MaxValue, comparison);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Boolean InRange(this Single value, Single maximum, MathPositionType comparison = MathPositionType.Both)
		{
			return InRange(value, default, maximum, comparison);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Boolean InRange(this Single value, Single minimum, Single maximum, MathPositionType comparison = MathPositionType.Both)
		{
			return comparison switch
			{
				MathPositionType.None => value > minimum && value < maximum,
				MathPositionType.Left => value >= minimum && value < maximum,
				MathPositionType.Right => value > minimum && value <= maximum,
				MathPositionType.Both => value >= minimum && value <= maximum,
				_ => throw new NotSupportedException(comparison.ToString())
			};
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Boolean InRange(this Double value, MathPositionType comparison = MathPositionType.Both)
		{
			return InRange(value, default, Double.MaxValue, comparison);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Boolean InRange(this Double value, Double maximum, MathPositionType comparison = MathPositionType.Both)
		{
			return InRange(value, default, maximum, comparison);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Boolean InRange(this Double value, Double minimum, Double maximum, MathPositionType comparison = MathPositionType.Both)
		{
			return comparison switch
			{
				MathPositionType.None => value > minimum && value < maximum,
				MathPositionType.Left => value >= minimum && value < maximum,
				MathPositionType.Right => value > minimum && value <= maximum,
				MathPositionType.Both => value >= minimum && value <= maximum,
				_ => throw new NotSupportedException(comparison.ToString())
			};
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Boolean InRange(this Decimal value, MathPositionType comparison = MathPositionType.Both)
		{
			return InRange(value, default, Decimal.MaxValue, comparison);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Boolean InRange(this Decimal value, Decimal maximum, MathPositionType comparison = MathPositionType.Both)
		{
			return InRange(value, default, maximum, comparison);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Boolean InRange(this Decimal value, Decimal minimum, Decimal maximum, MathPositionType comparison = MathPositionType.Both)
		{
			return comparison switch
			{
				MathPositionType.None => value > minimum && value < maximum,
				MathPositionType.Left => value >= minimum && value < maximum,
				MathPositionType.Right => value > minimum && value <= maximum,
				MathPositionType.Both => value >= minimum && value <= maximum,
				_ => throw new NotSupportedException(comparison.ToString())
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
			Double average = multiplier / 2d;

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
		public static SByte RoundAwayFromZeroToMultiplier(SByte value, SByte multiplier)
		{
			if (value > 0)
			{
				return multiplier > 0 ? RoundUpToMultiplier(value, multiplier) : RoundUpToMultiplier(value, (SByte)(-multiplier));
			}

			return multiplier > 0 ? RoundUpToMultiplier(value, (SByte)(-multiplier)) : RoundUpToMultiplier(value, multiplier);
		}

		public static SByte RoundToMultiplier(this SByte value, SByte multiplier, RoundType round = RoundType.Banking)
		{
			return round switch
			{
				RoundType.Banking => RoundBankingToMultiplier(value, multiplier),
				RoundType.AwayFromZero => RoundAwayFromZeroToMultiplier(value, multiplier),
				RoundType.Ceil => RoundUpToMultiplier(value, multiplier),
				RoundType.Floor => RoundDownToMultiplier(value, multiplier),
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

			return value % multiplier >= multiplier / 2d ? RoundUpToMultiplierOriginal(value, multiplier) : RoundDownToMultiplierOriginal(value, multiplier);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Byte RoundAwayFromZeroToMultiplier(Byte value, Byte multiplier)
		{
			return RoundUpToMultiplier(value, multiplier);
		}

		public static Byte RoundToMultiplier(this Byte value, Byte multiplier, RoundType round = RoundType.Banking)
		{
			return round switch
			{
				RoundType.Banking => RoundBankingToMultiplier(value, multiplier),
				RoundType.AwayFromZero => RoundAwayFromZeroToMultiplier(value, multiplier),
				RoundType.Ceil => RoundUpToMultiplier(value, multiplier),
				RoundType.Floor => RoundDownToMultiplier(value, multiplier),
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
			Double average = multiplier / 2d;

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
		public static Int16 RoundAwayFromZeroToMultiplier(Int16 value, Int16 multiplier)
		{
			if (value > 0)
			{
				return multiplier > 0 ? RoundUpToMultiplier(value, multiplier) : RoundUpToMultiplier(value, (Int16)(-multiplier));
			}

			return multiplier > 0 ? RoundUpToMultiplier(value, (Int16)(-multiplier)) : RoundUpToMultiplier(value, multiplier);
		}

		public static Int16 RoundToMultiplier(this Int16 value, Int16 multiplier, RoundType round = RoundType.Banking)
		{
			return round switch
			{
				RoundType.Banking => RoundBankingToMultiplier(value, multiplier),
				RoundType.AwayFromZero => RoundAwayFromZeroToMultiplier(value, multiplier),
				RoundType.Ceil => RoundUpToMultiplier(value, multiplier),
				RoundType.Floor => RoundDownToMultiplier(value, multiplier),
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

			return value % multiplier >= multiplier / 2d ? RoundUpToMultiplierOriginal(value, multiplier) : RoundDownToMultiplierOriginal(value, multiplier);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static UInt16 RoundAwayFromZeroToMultiplier(UInt16 value, UInt16 multiplier)
		{
			return RoundUpToMultiplier(value, multiplier);
		}

		public static UInt16 RoundToMultiplier(this UInt16 value, UInt16 multiplier, RoundType round = RoundType.Banking)
		{
			return round switch
			{
				RoundType.Banking => RoundBankingToMultiplier(value, multiplier),
				RoundType.AwayFromZero => RoundAwayFromZeroToMultiplier(value, multiplier),
				RoundType.Ceil => RoundUpToMultiplier(value, multiplier),
				RoundType.Floor => RoundDownToMultiplier(value, multiplier),
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
			Double average = multiplier / 2d;

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
		public static Int32 RoundAwayFromZeroToMultiplier(Int32 value, Int32 multiplier)
		{
			if (value > 0)
			{
				return multiplier > 0 ? RoundUpToMultiplier(value, multiplier) : RoundUpToMultiplier(value, -multiplier);
			}

			return multiplier > 0 ? RoundUpToMultiplier(value, -multiplier) : RoundUpToMultiplier(value, multiplier);
		}

		public static Int32 RoundToMultiplier(this Int32 value, Int32 multiplier, RoundType round = RoundType.Banking)
		{
			return round switch
			{
				RoundType.Banking => RoundBankingToMultiplier(value, multiplier),
				RoundType.AwayFromZero => RoundAwayFromZeroToMultiplier(value, multiplier),
				RoundType.Ceil => RoundUpToMultiplier(value, multiplier),
				RoundType.Floor => RoundDownToMultiplier(value, multiplier),
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

			return value % multiplier >= multiplier / 2d ? RoundUpToMultiplierOriginal(value, multiplier) : RoundDownToMultiplierOriginal(value, multiplier);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static UInt32 RoundAwayFromZeroToMultiplier(UInt32 value, UInt32 multiplier)
		{
			return RoundUpToMultiplier(value, multiplier);
		}

		public static UInt32 RoundToMultiplier(this UInt32 value, UInt32 multiplier, RoundType round = RoundType.Banking)
		{
			return round switch
			{
				RoundType.Banking => RoundBankingToMultiplier(value, multiplier),
				RoundType.AwayFromZero => RoundAwayFromZeroToMultiplier(value, multiplier),
				RoundType.Ceil => RoundUpToMultiplier(value, multiplier),
				RoundType.Floor => RoundDownToMultiplier(value, multiplier),
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
			Double average = multiplier / 2d;

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
		public static Int64 RoundAwayFromZeroToMultiplier(Int64 value, Int64 multiplier)
		{
			if (value > 0)
			{
				return multiplier > 0 ? RoundUpToMultiplier(value, multiplier) : RoundUpToMultiplier(value, -multiplier);
			}

			return multiplier > 0 ? RoundUpToMultiplier(value, -multiplier) : RoundUpToMultiplier(value, multiplier);
		}

		public static Int64 RoundToMultiplier(this Int64 value, Int64 multiplier, RoundType round = RoundType.Banking)
		{
			return round switch
			{
				RoundType.Banking => RoundBankingToMultiplier(value, multiplier),
				RoundType.AwayFromZero => RoundAwayFromZeroToMultiplier(value, multiplier),
				RoundType.Ceil => RoundUpToMultiplier(value, multiplier),
				RoundType.Floor => RoundDownToMultiplier(value, multiplier),
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

			return value % multiplier >= multiplier / 2d ? RoundUpToMultiplierOriginal(value, multiplier) : RoundDownToMultiplierOriginal(value, multiplier);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static UInt64 RoundAwayFromZeroToMultiplier(UInt64 value, UInt64 multiplier)
		{
			return RoundUpToMultiplier(value, multiplier);
		}

		public static UInt64 RoundToMultiplier(this UInt64 value, UInt64 multiplier, RoundType round = RoundType.Banking)
		{
			return round switch
			{
				RoundType.Banking => RoundBankingToMultiplier(value, multiplier),
				RoundType.AwayFromZero => RoundAwayFromZeroToMultiplier(value, multiplier),
				RoundType.Ceil => RoundUpToMultiplier(value, multiplier),
				RoundType.Floor => RoundDownToMultiplier(value, multiplier),
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
			Double average = multiplier / 2d;

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
		public static Single RoundAwayFromZeroToMultiplier(Single value, Single multiplier)
		{
			if (value > 0)
			{
				return multiplier > 0 ? RoundUpToMultiplier(value, multiplier) : RoundUpToMultiplier(value, -multiplier);
			}

			return multiplier > 0 ? RoundUpToMultiplier(value, -multiplier) : RoundUpToMultiplier(value, multiplier);
		}

		public static Single RoundToMultiplier(this Single value, Single multiplier, RoundType round = RoundType.Banking)
		{
			return round switch
			{
				RoundType.Banking => RoundBankingToMultiplier(value, multiplier),
				RoundType.AwayFromZero => RoundAwayFromZeroToMultiplier(value, multiplier),
				RoundType.Ceil => RoundUpToMultiplier(value, multiplier),
				RoundType.Floor => RoundDownToMultiplier(value, multiplier),
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
			Double average = multiplier / 2d;

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
		public static Double RoundAwayFromZeroToMultiplier(Double value, Double multiplier)
		{
			if (value > 0)
			{
				return multiplier > 0 ? RoundUpToMultiplier(value, multiplier) : RoundUpToMultiplier(value, -multiplier);
			}

			return multiplier > 0 ? RoundUpToMultiplier(value, -multiplier) : RoundUpToMultiplier(value, multiplier);
		}

		public static Double RoundToMultiplier(this Double value, Double multiplier, RoundType round = RoundType.Banking)
		{
			return round switch
			{
				RoundType.Banking => RoundBankingToMultiplier(value, multiplier),
				RoundType.AwayFromZero => RoundAwayFromZeroToMultiplier(value, multiplier),
				RoundType.Ceil => RoundUpToMultiplier(value, multiplier),
				RoundType.Floor => RoundDownToMultiplier(value, multiplier),
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
			Decimal average = multiplier / 2m;

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
		public static Decimal RoundAwayFromZeroToMultiplier(Decimal value, Decimal multiplier)
		{
			if (value > 0)
			{
				return multiplier > 0 ? RoundUpToMultiplier(value, multiplier) : RoundUpToMultiplier(value, -multiplier);
			}

			return multiplier > 0 ? RoundUpToMultiplier(value, -multiplier) : RoundUpToMultiplier(value, multiplier);
		}

		public static Decimal RoundToMultiplier(this Decimal value, Decimal multiplier, RoundType round = RoundType.Banking)
		{
			return round switch
			{
				RoundType.Banking => RoundBankingToMultiplier(value, multiplier),
				RoundType.AwayFromZero => RoundAwayFromZeroToMultiplier(value, multiplier),
				RoundType.Ceil => RoundUpToMultiplier(value, multiplier),
				RoundType.Floor => RoundDownToMultiplier(value, multiplier),
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
		public static Double Sqrt(this Single value)
		{
			return Math.Sqrt(value);
		}

		/// <inheritdoc cref="Math.Sqrt"/>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Double Sqrt(this Double value)
		{
			return Math.Sqrt(value);
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
		public static SByte ToNonZero(this SByte value, SByte alternate = 1)
		{
			return value == 0 ? alternate : value;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Byte ToNonZero(this Byte value, Byte alternate = 1)
		{
			return value == 0 ? alternate : value;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int16 ToNonZero(this Int16 value, Int16 alternate = 1)
		{
			return value == 0 ? alternate : value;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static UInt16 ToNonZero(this UInt16 value, UInt16 alternate = 1)
		{
			return value == 0 ? alternate : value;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int32 ToNonZero(this Int32 value, Int32 alternate = 1)
		{
			return value == 0 ? alternate : value;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static UInt32 ToNonZero(this UInt32 value, UInt32 alternate = 1)
		{
			return value == 0 ? alternate : value;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int64 ToNonZero(this Int64 value, Int64 alternate = 1)
		{
			return value == 0 ? alternate : value;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static UInt64 ToNonZero(this UInt64 value, UInt64 alternate = 1)
		{
			return value == 0 ? alternate : value;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Single ToNonZero(this Single value, Single alternate = 1)
		{
			return IsEpsilon(value) ? alternate : value;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Double ToNonZero(this Double value, Double alternate = 1)
		{
			return IsEpsilon(value) ? alternate : value;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Decimal ToNonZero(this Decimal value, Decimal alternate = 1)
		{
			return value == 0 ? alternate : value;
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

		private const Byte MinBase = 2;
		private const Byte MaxBase = 36;
		private const Int32 ZeroChar = '0';
		private const Int32 AlphabetStart = '9' - MinBase;

		public static String ToBase(this SByte value, Byte @base)
		{
			if (!@base.InRange(MinBase, MaxBase))
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
			if (!@base.InRange(MinBase, MaxBase))
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
			if (!@base.InRange(MinBase, MaxBase))
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
			if (!@base.InRange(MinBase, MaxBase))
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
			if (!@base.InRange(MinBase, MaxBase))
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
			if (!@base.InRange(MinBase, MaxBase))
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
			if (!@base.InRange(MinBase, MaxBase))
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
			if (!@base.InRange(MinBase, MaxBase))
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
			if (!@base.InRange(MinBase, MaxBase))
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

			Int32 max = (Int32) RoundCeil(Log(value, 2));
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
    }

    [SuppressMessage("ReSharper", "InvertIf")]
    public static class MathUnsafe
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