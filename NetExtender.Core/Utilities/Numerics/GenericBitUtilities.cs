// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace NetExtender.Utilities.Numerics
{
    [SuppressMessage("ReSharper", "RedundantOverflowCheckingContext")]
    public static partial class BitUtilities
    {
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static SByte BitwiseShiftLeft(this SByte value, Int32 shift)
		{
			unchecked
			{
				return (SByte) (shift >= 0 ? value << shift : value >> -shift);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static SByte BitwiseShiftLeftTrue(this SByte value, Int32 shift)
		{
			unchecked
			{
				return shift switch
				{
					0 => value,
					< 0 => BitwiseShiftRightTrue(value, -shift),
					>= sizeof(Byte) * BitInByte => (SByte) Byte.MaxValue,
					_ => (SByte) (value << shift | (Byte.MaxValue >> (sizeof(Byte) * BitInByte - shift)))
				};
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static SByte BitwiseShiftRight(this SByte value, Int32 shift)
		{
			unchecked
			{
				return (SByte) (shift >= 0 ? value >> shift : value << -shift);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static SByte BitwiseShiftRightTrue(this SByte value, Int32 shift)
		{
			unchecked
			{
				return shift switch
				{
					0 => value,
					< 0 => BitwiseShiftLeftTrue(value, -shift),
					>= sizeof(Byte) * BitInByte => (SByte) Byte.MaxValue,
					_ => (SByte) (value >> shift | (Byte.MaxValue << (sizeof(Byte) * BitInByte - shift)))
				};
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Byte BitwiseShiftLeft(this Byte value, Int32 shift)
		{
			unchecked
			{
				return (Byte) (shift >= 0 ? value << shift : value >> -shift);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static Byte BitwiseShiftLeftTrue(this Byte value, Int32 shift)
		{
			unchecked
			{
				return shift switch
				{
					0 => value,
					< 0 => BitwiseShiftRightTrue(value, -shift),
					>= sizeof(Byte) * BitInByte => Byte.MaxValue,
					_ => (Byte) (value << shift | (Byte.MaxValue >> (sizeof(Byte) * BitInByte - shift)))
				};
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Byte BitwiseShiftRight(this Byte value, Int32 shift)
		{
			unchecked
			{
				return (Byte) (shift >= 0 ? value >> shift : value << -shift);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static Byte BitwiseShiftRightTrue(this Byte value, Int32 shift)
		{
			unchecked
			{
				return shift switch
				{
					0 => value,
					< 0 => BitwiseShiftLeftTrue(value, -shift),
					>= sizeof(Byte) * BitInByte => Byte.MaxValue,
					_ => (Byte) (value >> shift | (Byte.MaxValue << (sizeof(Byte) * BitInByte - shift)))
				};
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int16 BitwiseShiftLeft(this Int16 value, Int32 shift)
		{
			unchecked
			{
				return (Int16) (shift >= 0 ? value << shift : value >> -shift);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static Int16 BitwiseShiftLeftTrue(this Int16 value, Int32 shift)
		{
			unchecked
			{
				return shift switch
				{
					0 => value,
					< 0 => BitwiseShiftRightTrue(value, -shift),
					>= sizeof(UInt16) * BitInByte => (Int16) UInt16.MaxValue,
					_ => (Int16) (value << shift | (UInt16.MaxValue >> (sizeof(UInt16) * BitInByte - shift)))
				};
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int16 BitwiseShiftRight(this Int16 value, Int32 shift)
		{
			unchecked
			{
				return (Int16) (shift >= 0 ? value >> shift : value << -shift);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static Int16 BitwiseShiftRightTrue(this Int16 value, Int32 shift)
		{
			unchecked
			{
				return shift switch
				{
					0 => value,
					< 0 => BitwiseShiftLeftTrue(value, -shift),
					>= sizeof(UInt16) * BitInByte => (Int16) UInt16.MaxValue,
					_ => (Int16) (value >> shift | (UInt16.MaxValue << (sizeof(UInt16) * BitInByte - shift)))
				};
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static UInt16 BitwiseShiftLeft(this UInt16 value, Int32 shift)
		{
			unchecked
			{
				return (UInt16) (shift >= 0 ? value << shift : value >> -shift);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static UInt16 BitwiseShiftLeftTrue(this UInt16 value, Int32 shift)
		{
			unchecked
			{
				return shift switch
				{
					0 => value,
					< 0 => BitwiseShiftRightTrue(value, -shift),
					>= sizeof(UInt16) * BitInByte => UInt16.MaxValue,
					_ => (UInt16) (value << shift | (UInt16.MaxValue >> (sizeof(UInt16) * BitInByte - shift)))
				};
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static UInt16 BitwiseShiftRight(this UInt16 value, Int32 shift)
		{
			unchecked
			{
				return (UInt16) (shift >= 0 ? value >> shift : value << -shift);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static UInt16 BitwiseShiftRightTrue(this UInt16 value, Int32 shift)
		{
			unchecked
			{
				return shift switch
				{
					0 => value,
					< 0 => BitwiseShiftLeftTrue(value, -shift),
					>= sizeof(UInt16) * BitInByte => UInt16.MaxValue,
					_ => (UInt16) (value >> shift | (UInt16.MaxValue << (sizeof(UInt16) * BitInByte - shift)))
				};
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int32 BitwiseShiftLeft(this Int32 value, Int32 shift)
		{
			unchecked
			{
				return shift >= 0 ? value << shift : value >> -shift;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static Int32 BitwiseShiftLeftTrue(this Int32 value, Int32 shift)
		{
			unchecked
			{
				return shift switch
				{
					0 => value,
					< 0 => BitwiseShiftRightTrue(value, -shift),
					>= sizeof(UInt32) * BitInByte => (Int32) UInt32.MaxValue,
					_ => (Int32) ((UInt32) value << shift | (UInt32.MaxValue >> (sizeof(UInt32) * BitInByte - shift)))
				};
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int32 BitwiseShiftRight(this Int32 value, Int32 shift)
		{
			unchecked
			{
				return shift >= 0 ? value >> shift : value << -shift;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static Int32 BitwiseShiftRightTrue(this Int32 value, Int32 shift)
		{
			unchecked
			{
				return shift switch
				{
					0 => value,
					< 0 => BitwiseShiftLeftTrue(value, -shift),
					>= sizeof(UInt32) * BitInByte => (Int32) UInt32.MaxValue,
					_ => (Int32) ((UInt32) value >> shift | (UInt32.MaxValue << (sizeof(UInt32) * BitInByte - shift)))
				};
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static UInt32 BitwiseShiftLeft(this UInt32 value, Int32 shift)
		{
			unchecked
			{
				return shift >= 0 ? value << shift : value >> -shift;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static UInt32 BitwiseShiftLeftTrue(this UInt32 value, Int32 shift)
		{
			unchecked
			{
				return shift switch
				{
					0 => value,
					< 0 => BitwiseShiftRightTrue(value, -shift),
					>= sizeof(UInt32) * BitInByte => UInt32.MaxValue,
					_ => value << shift | (UInt32.MaxValue >> (sizeof(UInt32) * BitInByte - shift))
				};
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static UInt32 BitwiseShiftRight(this UInt32 value, Int32 shift)
		{
			unchecked
			{
				return shift >= 0 ? value >> shift : value << -shift;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static UInt32 BitwiseShiftRightTrue(this UInt32 value, Int32 shift)
		{
			unchecked
			{
				return shift switch
				{
					0 => value,
					< 0 => BitwiseShiftLeftTrue(value, -shift),
					>= sizeof(UInt32) * BitInByte => UInt32.MaxValue,
					_ => value >> shift | (UInt32.MaxValue << (sizeof(UInt32) * BitInByte - shift))
				};
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int64 BitwiseShiftLeft(this Int64 value, Int32 shift)
		{
			unchecked
			{
				return shift >= 0 ? value << shift : value >> -shift;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static Int64 BitwiseShiftLeftTrue(this Int64 value, Int32 shift)
		{
			unchecked
			{
				return shift switch
				{
					0 => value,
					< 0 => BitwiseShiftRightTrue(value, -shift),
					>= sizeof(UInt64) * BitInByte => (Int64) UInt64.MaxValue,
					_ => (Int64) ((UInt64) value << shift | (UInt64.MaxValue >> (sizeof(UInt64) * BitInByte - shift)))
				};
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int64 BitwiseShiftRight(this Int64 value, Int32 shift)
		{
			unchecked
			{
				return shift >= 0 ? value >> shift : value << -shift;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static Int64 BitwiseShiftRightTrue(this Int64 value, Int32 shift)
		{
			unchecked
			{
				return shift switch
				{
					0 => value,
					< 0 => BitwiseShiftLeftTrue(value, -shift),
					>= sizeof(UInt64) * BitInByte => (Int64) UInt64.MaxValue,
					_ => (Int64) ((UInt64) value >> shift | (UInt64.MaxValue << (sizeof(UInt64) * BitInByte - shift)))
				};
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static UInt64 BitwiseShiftLeft(this UInt64 value, Int32 shift)
		{
			unchecked
			{
				return shift >= 0 ? value << shift : value >> -shift;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static UInt64 BitwiseShiftLeftTrue(this UInt64 value, Int32 shift)
		{
			unchecked
			{
				return shift switch
				{
					0 => value,
					< 0 => BitwiseShiftRightTrue(value, -shift),
					>= sizeof(UInt64) * BitInByte => UInt64.MaxValue,
					_ => value << shift | (UInt64.MaxValue >> (sizeof(UInt64) * BitInByte - shift))
				};
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static UInt64 BitwiseShiftRight(this UInt64 value, Int32 shift)
		{
			unchecked
			{
				return shift >= 0 ? value >> shift : value << -shift;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static UInt64 BitwiseShiftRightTrue(this UInt64 value, Int32 shift)
		{
			unchecked
			{
				return shift switch
				{
					0 => value,
					< 0 => BitwiseShiftLeftTrue(value, -shift),
					>= sizeof(UInt64) * BitInByte => UInt64.MaxValue,
					_ => value >> shift | (UInt64.MaxValue << (sizeof(UInt64) * BitInByte - shift))
				};
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static SByte BitwiseRotateLeft(this SByte value, Int32 offset)
		{
			const Int32 size = sizeof(SByte) * BitInByte;

			unchecked
			{
				switch (offset)
				{
					case 0:
						return value;
					case > 0:
						offset %= size;
						return (SByte) ((value << offset) | (value >> (size - offset)));
					default:
						offset = -offset % size;
						return (SByte) ((value >> offset) | (value << (size - offset)));
				}
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static SByte BitwiseRotateRight(this SByte value, Int32 offset)
		{
			const Int32 size = sizeof(SByte) * BitInByte;

			unchecked
			{
				switch (offset)
				{
					case 0:
						return value;
					case > 0:
						offset %= size;
						return (SByte) ((value >> offset) | (value << (size - offset)));
					default:
						offset = -offset % size;
						return (SByte) ((value << offset) | (value >> (size - offset)));
				}
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static Byte BitwiseRotateLeft(this Byte value, Int32 offset)
		{
			const Int32 size = sizeof(Byte) * BitInByte;

			unchecked
			{
				switch (offset)
				{
					case 0:
						return value;
					case > 0:
						offset %= size;
						return (Byte) ((value << offset) | (value >> (size - offset)));
					default:
						offset = -offset % size;
						return (Byte) ((value >> offset) | (value << (size - offset)));
				}
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static Byte BitwiseRotateRight(this Byte value, Int32 offset)
		{
			const Int32 size = sizeof(Byte) * BitInByte;

			unchecked
			{
				switch (offset)
				{
					case 0:
						return value;
					case > 0:
						offset %= size;
						return (Byte) ((value >> offset) | (value << (size - offset)));
					default:
						offset = -offset % size;
						return (Byte) ((value << offset) | (value >> (size - offset)));
				}
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static Int16 BitwiseRotateLeft(this Int16 value, Int32 offset)
		{
			const Int32 size = sizeof(Int16) * BitInByte;

			unchecked
			{
				switch (offset)
				{
					case 0:
						return value;
					case > 0:
						offset %= size;
						return (Int16) ((value << offset) | (value >> (size - offset)));
					default:
						offset = -offset % size;
						return (Int16) ((value >> offset) | (value << (size - offset)));
				}
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static Int16 BitwiseRotateRight(this Int16 value, Int32 offset)
		{
			const Int32 size = sizeof(Int16) * BitInByte;

			unchecked
			{
				switch (offset)
				{
					case 0:
						return value;
					case > 0:
						offset %= size;
						return (Int16) ((value >> offset) | (value << (size - offset)));
					default:
						offset = -offset % size;
						return (Int16) ((value << offset) | (value >> (size - offset)));
				}
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static UInt16 BitwiseRotateLeft(this UInt16 value, Int32 offset)
		{
			const Int32 size = sizeof(UInt16) * BitInByte;

			unchecked
			{
				switch (offset)
				{
					case 0:
						return value;
					case > 0:
						offset %= size;
						return (UInt16) ((value << offset) | (value >> (size - offset)));
					default:
						offset = -offset % size;
						return (UInt16) ((value >> offset) | (value << (size - offset)));
				}
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static UInt16 BitwiseRotateRight(this UInt16 value, Int32 offset)
		{
			const Int32 size = sizeof(UInt16) * BitInByte;

			unchecked
			{
				switch (offset)
				{
					case 0:
						return value;
					case > 0:
						offset %= size;
						return (UInt16) ((value >> offset) | (value << (size - offset)));
					default:
						offset = -offset % size;
						return (UInt16) ((value << offset) | (value >> (size - offset)));
				}
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static Int32 BitwiseRotateLeft(this Int32 value, Int32 offset)
		{
			const Int32 size = sizeof(Int32) * BitInByte;

			unchecked
			{
				switch (offset)
				{
					case 0:
						return value;
					case > 0:
						offset %= size;
						return (value << offset) | (value >> (size - offset));
					default:
						offset = -offset % size;
						return (value >> offset) | (value << (size - offset));
				}
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static Int32 BitwiseRotateRight(this Int32 value, Int32 offset)
		{
			const Int32 size = sizeof(Int32) * BitInByte;

			unchecked
			{
				switch (offset)
				{
					case 0:
						return value;
					case > 0:
						offset %= size;
						return (value >> offset) | (value << (size - offset));
					default:
						offset = -offset % size;
						return (value << offset) | (value >> (size - offset));
				}
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static UInt32 BitwiseRotateLeft(this UInt32 value, Int32 offset)
		{
			const Int32 size = sizeof(UInt32) * BitInByte;

			unchecked
			{
				switch (offset)
				{
					case 0:
						return value;
					case > 0:
						offset %= size;
						return (value << offset) | (value >> (size - offset));
					default:
						offset = -offset % size;
						return (value >> offset) | (value << (size - offset));
				}
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static UInt32 BitwiseRotateRight(this UInt32 value, Int32 offset)
		{
			const Int32 size = sizeof(UInt32) * BitInByte;

			unchecked
			{
				switch (offset)
				{
					case 0:
						return value;
					case > 0:
						offset %= size;
						return (value >> offset) | (value << (size - offset));
					default:
						offset = -offset % size;
						return (value << offset) | (value >> (size - offset));
				}
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static Int64 BitwiseRotateLeft(this Int64 value, Int32 offset)
		{
			const Int32 size = sizeof(Int64) * BitInByte;

			unchecked
			{
				switch (offset)
				{
					case 0:
						return value;
					case > 0:
						offset %= size;
						return (value << offset) | (value >> (size - offset));
					default:
						offset = -offset % size;
						return (value >> offset) | (value << (size - offset));
				}
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static Int64 BitwiseRotateRight(this Int64 value, Int32 offset)
		{
			const Int32 size = sizeof(Int64) * BitInByte;

			unchecked
			{
				switch (offset)
				{
					case 0:
						return value;
					case > 0:
						offset %= size;
						return (value >> offset) | (value << (size - offset));
					default:
						offset = -offset % size;
						return (value << offset) | (value >> (size - offset));
				}
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static UInt64 BitwiseRotateLeft(this UInt64 value, Int32 offset)
		{
			const Int32 size = sizeof(UInt64) * BitInByte;

			unchecked
			{
				switch (offset)
				{
					case 0:
						return value;
					case > 0:
						offset %= size;
						return (value << offset) | (value >> (size - offset));
					default:
						offset = -offset % size;
						return (value >> offset) | (value << (size - offset));
				}
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static UInt64 BitwiseRotateRight(this UInt64 value, Int32 offset)
		{
			const Int32 size = sizeof(UInt64) * BitInByte;

			unchecked
			{
				switch (offset)
				{
					case 0:
						return value;
					case > 0:
						offset %= size;
						return (value >> offset) | (value << (size - offset));
					default:
						offset = -offset % size;
						return (value << offset) | (value >> (size - offset));
				}
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int32 BitwisePopCount(this SByte value)
		{
			unchecked
			{
				return BitwisePopCount((UInt32) value);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int32 BitwisePopCount(this Byte value)
		{
			return BitOperations.PopCount(value);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int32 BitwisePopCount(this Int16 value)
		{
			unchecked
			{
				return BitwisePopCount((UInt32) value);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int32 BitwisePopCount(this UInt16 value)
		{
			return BitOperations.PopCount(value);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int32 BitwisePopCount(this Int32 value)
		{
			unchecked
			{
				return BitwisePopCount((UInt32) value);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int32 BitwisePopCount(this UInt32 value)
		{
			return BitOperations.PopCount(value);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int32 BitwisePopCount(this Int64 value)
		{
			unchecked
			{
				return BitwisePopCount((UInt64) value);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int32 BitwisePopCount(this UInt64 value)
		{
			return BitOperations.PopCount(value);
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static SByte BitwiseFillLeft(this SByte value, Int32 count, Boolean bit)
		{
			if (count <= 0)
			{
				return value;
			}

			unchecked
			{
				if (count >= sizeof(Byte) * BitInByte)
				{
					return bit ? (SByte) Byte.MaxValue : default;
				}

				return bit
					? (SByte) ((Byte) value | (Byte.MaxValue << (sizeof(Byte) * BitInByte - count)))
					: (SByte) ((Byte) value & (Byte.MaxValue >> (sizeof(Byte) * BitInByte - count)));
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static SByte BitwiseFillRight(this SByte value, Int32 count, Boolean bit)
		{
			if (count <= 0)
			{
				return value;
			}

			unchecked
			{
				if (count >= sizeof(Byte) * BitInByte)
				{
					return bit ? (SByte) Byte.MaxValue : default;
				}

				return bit
					? (SByte) ((Byte) value | (Byte.MaxValue >> (sizeof(Byte) * BitInByte - count)))
					: (SByte) ((Byte) value & (Byte.MaxValue << (sizeof(Byte) * BitInByte - count)));
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static Byte BitwiseFillLeft(this Byte value, Int32 count, Boolean bit)
		{
			if (count <= 0)
			{
				return value;
			}

			unchecked
			{
				if (count >= sizeof(Byte) * BitInByte)
				{
					return bit ? Byte.MaxValue : default;
				}

				return bit
					? (Byte) (value | (Byte.MaxValue << (sizeof(Byte) * BitInByte - count)))
					: (Byte) (value & (Byte.MaxValue >> (sizeof(Byte) * BitInByte - count)));
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static Byte BitwiseFillRight(this Byte value, Int32 count, Boolean bit)
		{
			if (count <= 0)
			{
				return value;
			}

			unchecked
			{
				if (count >= sizeof(Byte) * BitInByte)
				{
					return bit ? Byte.MaxValue : default;
				}

				return bit
					? (Byte) (value | (Byte.MaxValue >> (sizeof(Byte) * BitInByte - count)))
					: (Byte) (value & (Byte.MaxValue << (sizeof(Byte) * BitInByte - count)));
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static Int16 BitwiseFillLeft(this Int16 value, Int32 count, Boolean bit)
		{
			if (count <= 0)
			{
				return value;
			}

			unchecked
			{
				if (count >= sizeof(UInt16) * BitInByte)
				{
					return bit ? (Int16) UInt16.MaxValue : default;
				}

				return bit
					? (Int16) ((UInt16) value | (UInt16.MaxValue << (sizeof(UInt16) * BitInByte - count)))
					: (Int16) ((UInt16) value & (UInt16.MaxValue >> (sizeof(UInt16) * BitInByte - count)));
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static Int16 BitwiseFillRight(this Int16 value, Int32 count, Boolean bit)
		{
			if (count <= 0)
			{
				return value;
			}

			unchecked
			{
				if (count >= sizeof(UInt16) * BitInByte)
				{
					return bit ? (Int16) UInt16.MaxValue : default;
				}

				return bit
					? (Int16) ((UInt16) value | (UInt16.MaxValue >> (sizeof(UInt16) * BitInByte - count)))
					: (Int16) ((UInt16) value & (UInt16.MaxValue << (sizeof(UInt16) * BitInByte - count)));
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static UInt16 BitwiseFillLeft(this UInt16 value, Int32 count, Boolean bit)
		{
			if (count <= 0)
			{
				return value;
			}

			unchecked
			{
				if (count >= sizeof(UInt16) * BitInByte)
				{
					return bit ? UInt16.MaxValue : default;
				}

				return bit
					? (UInt16) (value | (UInt16.MaxValue << (sizeof(UInt16) * BitInByte - count)))
					: (UInt16) (value & (UInt16.MaxValue >> (sizeof(UInt16) * BitInByte - count)));
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static UInt16 BitwiseFillRight(this UInt16 value, Int32 count, Boolean bit)
		{
			if (count <= 0)
			{
				return value;
			}

			unchecked
			{
				if (count >= sizeof(UInt16) * BitInByte)
				{
					return bit ? UInt16.MaxValue : default;
				}

				return bit
					? (UInt16) (value | (UInt16.MaxValue >> (sizeof(UInt16) * BitInByte - count)))
					: (UInt16) (value & (UInt16.MaxValue << (sizeof(UInt16) * BitInByte - count)));
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static Int32 BitwiseFillLeft(this Int32 value, Int32 count, Boolean bit)
		{
			if (count <= 0)
			{
				return value;
			}

			unchecked
			{
				if (count >= sizeof(UInt32) * BitInByte)
				{
					return bit ? (Int32) UInt32.MaxValue : default;
				}

				return bit
					? (Int32) ((UInt32) value | (UInt32.MaxValue << (sizeof(UInt32) * BitInByte - count)))
					: (Int32) ((UInt32) value & (UInt32.MaxValue >> (sizeof(UInt32) * BitInByte - count)));
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static Int32 BitwiseFillRight(this Int32 value, Int32 count, Boolean bit)
		{
			if (count <= 0)
			{
				return value;
			}

			unchecked
			{
				if (count >= sizeof(UInt32) * BitInByte)
				{
					return bit ? (Int32) UInt32.MaxValue : default;
				}

				return bit
					? (Int32) ((UInt32) value | (UInt32.MaxValue >> (sizeof(UInt32) * BitInByte - count)))
					: (Int32) ((UInt32) value & (UInt32.MaxValue << (sizeof(UInt32) * BitInByte - count)));
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static UInt32 BitwiseFillLeft(this UInt32 value, Int32 count, Boolean bit)
		{
			if (count <= 0)
			{
				return value;
			}

			unchecked
			{
				if (count >= sizeof(UInt32) * BitInByte)
				{
					return bit ? UInt32.MaxValue : default;
				}

				return bit
					? value | (UInt32.MaxValue << (sizeof(UInt32) * BitInByte - count))
					: value & (UInt32.MaxValue >> (sizeof(UInt32) * BitInByte - count));
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static UInt32 BitwiseFillRight(this UInt32 value, Int32 count, Boolean bit)
		{
			if (count <= 0)
			{
				return value;
			}

			unchecked
			{
				if (count >= sizeof(UInt32) * BitInByte)
				{
					return bit ? UInt32.MaxValue : default;
				}

				return bit
					? value | (UInt32.MaxValue >> (sizeof(UInt32) * BitInByte - count))
					: value & (UInt32.MaxValue << (sizeof(UInt32) * BitInByte - count));
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static Int64 BitwiseFillLeft(this Int64 value, Int32 count, Boolean bit)
		{
			if (count <= 0)
			{
				return value;
			}

			unchecked
			{
				if (count >= sizeof(UInt64) * BitInByte)
				{
					return bit ? (Int64) UInt64.MaxValue : default;
				}

				return bit
					? (Int64) ((UInt64) value | (UInt64.MaxValue << (sizeof(UInt64) * BitInByte - count)))
					: (Int64) ((UInt64) value & (UInt64.MaxValue >> (sizeof(UInt64) * BitInByte - count)));
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static Int64 BitwiseFillRight(this Int64 value, Int32 count, Boolean bit)
		{
			if (count <= 0)
			{
				return value;
			}

			unchecked
			{
				if (count >= sizeof(UInt64) * BitInByte)
				{
					return bit ? (Int64) UInt64.MaxValue : default;
				}

				return bit
					? (Int64) ((UInt64) value | (UInt64.MaxValue >> (sizeof(UInt64) * BitInByte - count)))
					: (Int64) ((UInt64) value & (UInt64.MaxValue << (sizeof(UInt64) * BitInByte - count)));
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static UInt64 BitwiseFillLeft(this UInt64 value, Int32 count, Boolean bit)
		{
			if (count <= 0)
			{
				return value;
			}

			unchecked
			{
				if (count >= sizeof(UInt64) * BitInByte)
				{
					return bit ? UInt64.MaxValue : default;
				}

				return bit
					? value | (UInt64.MaxValue << (sizeof(UInt64) * BitInByte - count))
					: value & (UInt64.MaxValue >> (sizeof(UInt64) * BitInByte - count));
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static UInt64 BitwiseFillRight(this UInt64 value, Int32 count, Boolean bit)
		{
			if (count <= 0)
			{
				return value;
			}

			unchecked
			{
				if (count >= sizeof(UInt64) * BitInByte)
				{
					return bit ? UInt64.MaxValue : default;
				}

				return bit
					? value | (UInt64.MaxValue >> (sizeof(UInt64) * BitInByte - count))
					: value & (UInt64.MaxValue << (sizeof(UInt64) * BitInByte - count));
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static SByte High(this Int16 value)
		{
			unchecked
			{
				return (SByte) (value >> sizeof(SByte) * BitInByte);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Byte High(this UInt16 value)
		{
			unchecked
			{
				return (Byte) (value >> sizeof(Byte) * BitInByte);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int16 High(this Int32 value)
		{
			unchecked
			{
				return (Int16) (value >> sizeof(Int16) * BitInByte);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static UInt16 High(this UInt32 value)
		{
			unchecked
			{
				return (UInt16) (value >> sizeof(UInt16) * BitInByte);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Int32 High(this Int64 value)
		{
			unchecked
			{
				return (Int32) (value >> sizeof(Int32) * BitInByte);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static UInt32 High(this UInt64 value)
		{
			unchecked
			{
				return (UInt32) (value >> sizeof(UInt32) * BitInByte);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Byte Low(this Int16 value)
		{
			unchecked
			{
				return (Byte) (value & Byte.MaxValue);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Byte Low(this UInt16 value)
		{
			unchecked
			{
				return (Byte) (value & Byte.MaxValue);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static UInt16 Low(this Int32 value)
		{
			unchecked
			{
				return (UInt16) (value & UInt16.MaxValue);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static UInt16 Low(this UInt32 value)
		{
			unchecked
			{
				return (UInt16) (value & UInt16.MaxValue);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static UInt32 Low(this Int64 value)
		{
			unchecked
			{
				return (UInt32) (value & UInt32.MaxValue);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static UInt32 Low(this UInt64 value)
		{
			unchecked
			{
				return (UInt32) (value & UInt32.MaxValue);
			}
		}
    }
}