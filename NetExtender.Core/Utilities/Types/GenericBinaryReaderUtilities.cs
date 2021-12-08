// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.CompilerServices;

namespace NetExtender.Utilities.Types
{
    public static partial class BinaryReaderUtilities
    {
		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static Boolean TryRead(this BinaryReader value, out Boolean result)
		{
			return TryReadBoolean(value, out result);
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static Boolean TryReadBoolean(this BinaryReader value, out Boolean result)
		{
			try
			{
				result = value.ReadBoolean();
				return true;
			}
			catch (Exception)
			{
				result = default;
				return false;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static Boolean TryRead(this BinaryReader value, out SByte result)
		{
			return TryReadSByte(value, out result);
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static Boolean TryReadSByte(this BinaryReader value, out SByte result)
		{
			try
			{
				result = value.ReadSByte();
				return true;
			}
			catch (Exception)
			{
				result = default;
				return false;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static Boolean TryRead(this BinaryReader value, out Byte result)
		{
			return TryReadByte(value, out result);
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static Boolean TryReadByte(this BinaryReader value, out Byte result)
		{
			try
			{
				result = value.ReadByte();
				return true;
			}
			catch (Exception)
			{
				result = default;
				return false;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static Boolean TryRead(this BinaryReader value, out Int16 result)
		{
			return TryReadInt16(value, out result);
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static Boolean TryReadInt16(this BinaryReader value, out Int16 result)
		{
			try
			{
				result = value.ReadInt16();
				return true;
			}
			catch (Exception)
			{
				result = default;
				return false;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static Boolean TryRead(this BinaryReader value, out UInt16 result)
		{
			return TryReadUInt16(value, out result);
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static Boolean TryReadUInt16(this BinaryReader value, out UInt16 result)
		{
			try
			{
				result = value.ReadUInt16();
				return true;
			}
			catch (Exception)
			{
				result = default;
				return false;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static Boolean TryRead(this BinaryReader value, out Int32 result)
		{
			return TryReadInt32(value, out result);
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static Boolean TryReadInt32(this BinaryReader value, out Int32 result)
		{
			try
			{
				result = value.ReadInt32();
				return true;
			}
			catch (Exception)
			{
				result = default;
				return false;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static Boolean TryRead(this BinaryReader value, out UInt32 result)
		{
			return TryReadUInt32(value, out result);
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static Boolean TryReadUInt32(this BinaryReader value, out UInt32 result)
		{
			try
			{
				result = value.ReadUInt32();
				return true;
			}
			catch (Exception)
			{
				result = default;
				return false;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static Boolean TryRead(this BinaryReader value, out Int64 result)
		{
			return TryReadInt64(value, out result);
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static Boolean TryReadInt64(this BinaryReader value, out Int64 result)
		{
			try
			{
				result = value.ReadInt64();
				return true;
			}
			catch (Exception)
			{
				result = default;
				return false;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static Boolean TryRead(this BinaryReader value, out UInt64 result)
		{
			return TryReadUInt64(value, out result);
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static Boolean TryReadUInt64(this BinaryReader value, out UInt64 result)
		{
			try
			{
				result = value.ReadUInt64();
				return true;
			}
			catch (Exception)
			{
				result = default;
				return false;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static Boolean TryRead(this BinaryReader value, out Single result)
		{
			return TryReadSingle(value, out result);
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static Boolean TryReadSingle(this BinaryReader value, out Single result)
		{
			try
			{
				result = value.ReadSingle();
				return true;
			}
			catch (Exception)
			{
				result = default;
				return false;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static Boolean TryRead(this BinaryReader value, out Double result)
		{
			return TryReadDouble(value, out result);
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static Boolean TryReadDouble(this BinaryReader value, out Double result)
		{
			try
			{
				result = value.ReadDouble();
				return true;
			}
			catch (Exception)
			{
				result = default;
				return false;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static Boolean TryRead(this BinaryReader value, out Decimal result)
		{
			return TryReadDecimal(value, out result);
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static Boolean TryReadDecimal(this BinaryReader value, out Decimal result)
		{
			try
			{
				result = value.ReadDecimal();
				return true;
			}
			catch (Exception)
			{
				result = default;
				return false;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static Boolean TryRead(this BinaryReader value, [MaybeNullWhen(false)] out String result)
		{
			return TryReadString(value, out result);
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static Boolean TryReadString(this BinaryReader value, [MaybeNullWhen(false)] out String result)
		{
			try
            {
				result = value.ReadString();
				return true;
			}
			catch (Exception)
			{
				result = default;
				return false;
			}
		}
    }
}