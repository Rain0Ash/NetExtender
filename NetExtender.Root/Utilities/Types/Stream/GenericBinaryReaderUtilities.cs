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
		public static Boolean TryRead(this BinaryReader reader, out Boolean result)
		{
			return TryReadBoolean(reader, out result);
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static Boolean TryReadBoolean(this BinaryReader reader, out Boolean result)
		{
			if (reader is null)
			{
				throw new ArgumentNullException(nameof(reader));
			}

			try
			{
				result = reader.ReadBoolean();
				return true;
			}
			catch (Exception)
			{
				result = default;
				return false;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static Boolean TryRead(this BinaryReader reader, out SByte result)
		{
			return TryReadSByte(reader, out result);
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static Boolean TryReadSByte(this BinaryReader reader, out SByte result)
		{
			if (reader is null)
			{
				throw new ArgumentNullException(nameof(reader));
			}

			try
			{
				result = reader.ReadSByte();
				return true;
			}
			catch (Exception)
			{
				result = default;
				return false;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static Boolean TryRead(this BinaryReader reader, out Byte result)
		{
			return TryReadByte(reader, out result);
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static Boolean TryReadByte(this BinaryReader reader, out Byte result)
		{
			if (reader is null)
			{
				throw new ArgumentNullException(nameof(reader));
			}

			try
			{
				result = reader.ReadByte();
				return true;
			}
			catch (Exception)
			{
				result = default;
				return false;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static Boolean TryRead(this BinaryReader reader, out Int16 result)
		{
			return TryReadInt16(reader, out result);
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static Boolean TryReadInt16(this BinaryReader reader, out Int16 result)
		{
			if (reader is null)
			{
				throw new ArgumentNullException(nameof(reader));
			}

			try
			{
				result = reader.ReadInt16();
				return true;
			}
			catch (Exception)
			{
				result = default;
				return false;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static Boolean TryRead(this BinaryReader reader, out UInt16 result)
		{
			return TryReadUInt16(reader, out result);
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static Boolean TryReadUInt16(this BinaryReader reader, out UInt16 result)
		{
			if (reader is null)
			{
				throw new ArgumentNullException(nameof(reader));
			}

			try
			{
				result = reader.ReadUInt16();
				return true;
			}
			catch (Exception)
			{
				result = default;
				return false;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static Boolean TryRead(this BinaryReader reader, out Int32 result)
		{
			return TryReadInt32(reader, out result);
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static Boolean TryReadInt32(this BinaryReader reader, out Int32 result)
		{
			if (reader is null)
			{
				throw new ArgumentNullException(nameof(reader));
			}

			try
			{
				result = reader.ReadInt32();
				return true;
			}
			catch (Exception)
			{
				result = default;
				return false;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static Boolean TryRead(this BinaryReader reader, out UInt32 result)
		{
			return TryReadUInt32(reader, out result);
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static Boolean TryReadUInt32(this BinaryReader reader, out UInt32 result)
		{
			if (reader is null)
			{
				throw new ArgumentNullException(nameof(reader));
			}

			try
			{
				result = reader.ReadUInt32();
				return true;
			}
			catch (Exception)
			{
				result = default;
				return false;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static Boolean TryRead(this BinaryReader reader, out Int64 result)
		{
			return TryReadInt64(reader, out result);
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static Boolean TryReadInt64(this BinaryReader reader, out Int64 result)
		{
			if (reader is null)
			{
				throw new ArgumentNullException(nameof(reader));
			}

			try
			{
				result = reader.ReadInt64();
				return true;
			}
			catch (Exception)
			{
				result = default;
				return false;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static Boolean TryRead(this BinaryReader reader, out UInt64 result)
		{
			return TryReadUInt64(reader, out result);
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static Boolean TryReadUInt64(this BinaryReader reader, out UInt64 result)
		{
			if (reader is null)
			{
				throw new ArgumentNullException(nameof(reader));
			}

			try
			{
				result = reader.ReadUInt64();
				return true;
			}
			catch (Exception)
			{
				result = default;
				return false;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static Boolean TryRead(this BinaryReader reader, out Single result)
		{
			return TryReadSingle(reader, out result);
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static Boolean TryReadSingle(this BinaryReader reader, out Single result)
		{
			if (reader is null)
			{
				throw new ArgumentNullException(nameof(reader));
			}

			try
			{
				result = reader.ReadSingle();
				return true;
			}
			catch (Exception)
			{
				result = default;
				return false;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static Boolean TryRead(this BinaryReader reader, out Double result)
		{
			return TryReadDouble(reader, out result);
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static Boolean TryReadDouble(this BinaryReader reader, out Double result)
		{
			if (reader is null)
			{
				throw new ArgumentNullException(nameof(reader));
			}

			try
			{
				result = reader.ReadDouble();
				return true;
			}
			catch (Exception)
			{
				result = default;
				return false;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static Boolean TryRead(this BinaryReader reader, out Decimal result)
		{
			return TryReadDecimal(reader, out result);
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static Boolean TryReadDecimal(this BinaryReader reader, out Decimal result)
		{
			if (reader is null)
			{
				throw new ArgumentNullException(nameof(reader));
			}

			try
			{
				result = reader.ReadDecimal();
				return true;
			}
			catch (Exception)
			{
				result = default;
				return false;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static Boolean TryRead(this BinaryReader reader, [MaybeNullWhen(false)] out String result)
		{
			return TryReadString(reader, out result);
		}

		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public static Boolean TryReadString(this BinaryReader reader, [MaybeNullWhen(false)] out String result)
		{
			if (reader is null)
			{
				throw new ArgumentNullException(nameof(reader));
			}

			try
			{
				result = reader.ReadString();
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