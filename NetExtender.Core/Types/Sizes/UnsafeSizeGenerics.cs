// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using NetExtender.Utilities.Core;
using NetExtender.Utilities.Types;

// ReSharper disable MemberCanBePrivate.Global
#pragma warning disable CS0169
#pragma warning disable CS0649
#pragma warning disable CS9084

namespace NetExtender.Types.Sizes
{
	[Serializable]
	public unsafe struct UnsafeSize8 : IUnsafeSize<UnsafeSize8>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize8 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize8 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		public static Boolean operator ==(in UnsafeSize8 first, ReadOnlySpan<Byte> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator ==(in UnsafeSize8 first, in UnsafeSize8 second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(in UnsafeSize8 first, ReadOnlySpan<Byte> second)
		{
			return !(first == second);
		}

		public static Boolean operator !=(in UnsafeSize8 first, in UnsafeSize8 second)
		{
			return !(first == second);
		}

		public static Boolean operator >(in UnsafeSize8 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >(in UnsafeSize8 first, in UnsafeSize8 second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >=(in UnsafeSize8 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator >=(in UnsafeSize8 first, in UnsafeSize8 second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator <(in UnsafeSize8 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <(in UnsafeSize8 first, in UnsafeSize8 second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <=(in UnsafeSize8 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) <= 0;
		}

		public static Boolean operator <=(in UnsafeSize8 first, in UnsafeSize8 second)
		{
			return first.CompareTo(second) <= 0;
		}

		private const String Data = UnsafeSize.Data;
		public const Int32 Size = 8;
		private fixed Byte Internal[Size];

		public readonly Type Type
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return typeof(UnsafeSize8);
			}
		}

		ref UnsafeSize8 IUnsafeSize<UnsafeSize8>.This
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return ref this;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size;
			}
		}

		readonly Span<Byte> IUnsafeSize<UnsafeSize8>.Full
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this;
			}
		}

		public readonly Boolean IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size <= 0 || AsReadOnlySpan().All(default(Byte));
			}
		}

		public UnsafeSize8(ReadOnlySpan<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}.");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, value.Length);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal UnsafeSize8(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal readonly void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue(Data, AsReadOnlySpan().ToArray(), typeof(Byte[]));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			GetObjectData(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ref UnsafeSize8 Deserialize(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}

			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize8 IUnsafeSize<UnsafeSize8>.Deserialize(SerializationInfo info, StreamingContext context)
		{
			return ref Deserialize(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Support<T>() where T : struct
		{
			return Unsafe.SizeOf<T>() is > 0 and <= Size;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ref T As<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<T> AsReadOnlySpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<Byte> AsSpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<T> AsSpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new Span<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize8 IUnsafeSize<UnsafeSize8>.SetFull()
		{
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize8 Fill(Byte value)
		{
			AsSpan().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize8 Fill<T>(T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize8 Fill<T>(in T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize8 Clear()
		{
			return ref Reset();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize8 Reset()
		{
			return ref Fill(default(Byte));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(Span<Byte> destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(ref UnsafeSize8 destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(Span<Byte> destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(ref UnsafeSize8 destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Byte[] ToArray()
		{
			return AsReadOnlySpan().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly Byte[] IUnsafeSize<UnsafeSize8>.ToFullArray()
		{
			return ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly T[] ToArray<T>() where T : struct
		{
			return AsReadOnlySpan<T>().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly T[] IUnsafeSize<UnsafeSize8>.ToFullArray<T>()
		{
			return ToArray<T>();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref *pointer;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(ReadOnlySpan<Byte> other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(UnsafeSize8 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(in UnsafeSize8 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly Int32 GetHashCode()
		{
			HashCode hash = new HashCode();
			hash.AddBytes(this);
			return hash.ToHashCode();
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other switch
			{
				null => IsEmpty,
				UnsafeSize8 value => Equals(value),
				Memory<Byte> value => Equals(value.Span),
				ReadOnlyMemory<Byte> value => Equals(value.Span),
				_ => false
			};
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(ReadOnlySpan<Byte> other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(UnsafeSize8 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(in UnsafeSize8 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly String ToString()
		{
			return Convert.ToHexString(this);
		}
	}

	[Serializable]
	public unsafe struct UnsafeSize16 : IUnsafeSize<UnsafeSize16>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize16 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize16 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		public static Boolean operator ==(in UnsafeSize16 first, ReadOnlySpan<Byte> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator ==(in UnsafeSize16 first, in UnsafeSize16 second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(in UnsafeSize16 first, ReadOnlySpan<Byte> second)
		{
			return !(first == second);
		}

		public static Boolean operator !=(in UnsafeSize16 first, in UnsafeSize16 second)
		{
			return !(first == second);
		}

		public static Boolean operator >(in UnsafeSize16 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >(in UnsafeSize16 first, in UnsafeSize16 second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >=(in UnsafeSize16 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator >=(in UnsafeSize16 first, in UnsafeSize16 second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator <(in UnsafeSize16 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <(in UnsafeSize16 first, in UnsafeSize16 second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <=(in UnsafeSize16 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) <= 0;
		}

		public static Boolean operator <=(in UnsafeSize16 first, in UnsafeSize16 second)
		{
			return first.CompareTo(second) <= 0;
		}

		private const String Data = UnsafeSize.Data;
		public const Int32 Size = 16;
		private fixed Byte Internal[Size];

		public readonly Type Type
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return typeof(UnsafeSize16);
			}
		}

		ref UnsafeSize16 IUnsafeSize<UnsafeSize16>.This
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return ref this;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size;
			}
		}

		readonly Span<Byte> IUnsafeSize<UnsafeSize16>.Full
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this;
			}
		}

		public readonly Boolean IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size <= 0 || AsReadOnlySpan().All(default(Byte));
			}
		}

		public UnsafeSize16(ReadOnlySpan<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}.");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, value.Length);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal UnsafeSize16(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal readonly void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue(Data, AsReadOnlySpan().ToArray(), typeof(Byte[]));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			GetObjectData(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ref UnsafeSize16 Deserialize(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}

			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize16 IUnsafeSize<UnsafeSize16>.Deserialize(SerializationInfo info, StreamingContext context)
		{
			return ref Deserialize(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Support<T>() where T : struct
		{
			return Unsafe.SizeOf<T>() is > 0 and <= Size;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ref T As<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<T> AsReadOnlySpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<Byte> AsSpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<T> AsSpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new Span<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize16 IUnsafeSize<UnsafeSize16>.SetFull()
		{
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize16 Fill(Byte value)
		{
			AsSpan().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize16 Fill<T>(T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize16 Fill<T>(in T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize16 Clear()
		{
			return ref Reset();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize16 Reset()
		{
			return ref Fill(default(Byte));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(Span<Byte> destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(ref UnsafeSize16 destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(Span<Byte> destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(ref UnsafeSize16 destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Byte[] ToArray()
		{
			return AsReadOnlySpan().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly Byte[] IUnsafeSize<UnsafeSize16>.ToFullArray()
		{
			return ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly T[] ToArray<T>() where T : struct
		{
			return AsReadOnlySpan<T>().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly T[] IUnsafeSize<UnsafeSize16>.ToFullArray<T>()
		{
			return ToArray<T>();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref *pointer;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(ReadOnlySpan<Byte> other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(UnsafeSize16 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(in UnsafeSize16 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly Int32 GetHashCode()
		{
			HashCode hash = new HashCode();
			hash.AddBytes(this);
			return hash.ToHashCode();
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other switch
			{
				null => IsEmpty,
				UnsafeSize16 value => Equals(value),
				Memory<Byte> value => Equals(value.Span),
				ReadOnlyMemory<Byte> value => Equals(value.Span),
				_ => false
			};
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(ReadOnlySpan<Byte> other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(UnsafeSize16 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(in UnsafeSize16 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly String ToString()
		{
			return Convert.ToHexString(this);
		}
	}

	[Serializable]
	public unsafe struct UnsafeSize24 : IUnsafeSize<UnsafeSize24>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize24 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize24 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		public static Boolean operator ==(in UnsafeSize24 first, ReadOnlySpan<Byte> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator ==(in UnsafeSize24 first, in UnsafeSize24 second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(in UnsafeSize24 first, ReadOnlySpan<Byte> second)
		{
			return !(first == second);
		}

		public static Boolean operator !=(in UnsafeSize24 first, in UnsafeSize24 second)
		{
			return !(first == second);
		}

		public static Boolean operator >(in UnsafeSize24 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >(in UnsafeSize24 first, in UnsafeSize24 second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >=(in UnsafeSize24 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator >=(in UnsafeSize24 first, in UnsafeSize24 second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator <(in UnsafeSize24 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <(in UnsafeSize24 first, in UnsafeSize24 second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <=(in UnsafeSize24 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) <= 0;
		}

		public static Boolean operator <=(in UnsafeSize24 first, in UnsafeSize24 second)
		{
			return first.CompareTo(second) <= 0;
		}

		private const String Data = UnsafeSize.Data;
		public const Int32 Size = 24;
		private fixed Byte Internal[Size];

		public readonly Type Type
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return typeof(UnsafeSize24);
			}
		}

		ref UnsafeSize24 IUnsafeSize<UnsafeSize24>.This
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return ref this;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size;
			}
		}

		readonly Span<Byte> IUnsafeSize<UnsafeSize24>.Full
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this;
			}
		}

		public readonly Boolean IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size <= 0 || AsReadOnlySpan().All(default(Byte));
			}
		}

		public UnsafeSize24(ReadOnlySpan<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}.");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, value.Length);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal UnsafeSize24(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal readonly void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue(Data, AsReadOnlySpan().ToArray(), typeof(Byte[]));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			GetObjectData(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ref UnsafeSize24 Deserialize(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}

			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize24 IUnsafeSize<UnsafeSize24>.Deserialize(SerializationInfo info, StreamingContext context)
		{
			return ref Deserialize(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Support<T>() where T : struct
		{
			return Unsafe.SizeOf<T>() is > 0 and <= Size;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ref T As<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<T> AsReadOnlySpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<Byte> AsSpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<T> AsSpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new Span<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize24 IUnsafeSize<UnsafeSize24>.SetFull()
		{
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize24 Fill(Byte value)
		{
			AsSpan().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize24 Fill<T>(T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize24 Fill<T>(in T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize24 Clear()
		{
			return ref Reset();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize24 Reset()
		{
			return ref Fill(default(Byte));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(Span<Byte> destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(ref UnsafeSize24 destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(Span<Byte> destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(ref UnsafeSize24 destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Byte[] ToArray()
		{
			return AsReadOnlySpan().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly Byte[] IUnsafeSize<UnsafeSize24>.ToFullArray()
		{
			return ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly T[] ToArray<T>() where T : struct
		{
			return AsReadOnlySpan<T>().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly T[] IUnsafeSize<UnsafeSize24>.ToFullArray<T>()
		{
			return ToArray<T>();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref *pointer;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(ReadOnlySpan<Byte> other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(UnsafeSize24 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(in UnsafeSize24 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly Int32 GetHashCode()
		{
			HashCode hash = new HashCode();
			hash.AddBytes(this);
			return hash.ToHashCode();
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other switch
			{
				null => IsEmpty,
				UnsafeSize24 value => Equals(value),
				Memory<Byte> value => Equals(value.Span),
				ReadOnlyMemory<Byte> value => Equals(value.Span),
				_ => false
			};
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(ReadOnlySpan<Byte> other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(UnsafeSize24 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(in UnsafeSize24 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly String ToString()
		{
			return Convert.ToHexString(this);
		}
	}

	[Serializable]
	public unsafe struct UnsafeSize32 : IUnsafeSize<UnsafeSize32>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize32 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize32 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		public static Boolean operator ==(in UnsafeSize32 first, ReadOnlySpan<Byte> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator ==(in UnsafeSize32 first, in UnsafeSize32 second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(in UnsafeSize32 first, ReadOnlySpan<Byte> second)
		{
			return !(first == second);
		}

		public static Boolean operator !=(in UnsafeSize32 first, in UnsafeSize32 second)
		{
			return !(first == second);
		}

		public static Boolean operator >(in UnsafeSize32 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >(in UnsafeSize32 first, in UnsafeSize32 second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >=(in UnsafeSize32 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator >=(in UnsafeSize32 first, in UnsafeSize32 second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator <(in UnsafeSize32 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <(in UnsafeSize32 first, in UnsafeSize32 second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <=(in UnsafeSize32 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) <= 0;
		}

		public static Boolean operator <=(in UnsafeSize32 first, in UnsafeSize32 second)
		{
			return first.CompareTo(second) <= 0;
		}

		private const String Data = UnsafeSize.Data;
		public const Int32 Size = 32;
		private fixed Byte Internal[Size];

		public readonly Type Type
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return typeof(UnsafeSize32);
			}
		}

		ref UnsafeSize32 IUnsafeSize<UnsafeSize32>.This
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return ref this;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size;
			}
		}

		readonly Span<Byte> IUnsafeSize<UnsafeSize32>.Full
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this;
			}
		}

		public readonly Boolean IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size <= 0 || AsReadOnlySpan().All(default(Byte));
			}
		}

		public UnsafeSize32(ReadOnlySpan<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}.");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, value.Length);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal UnsafeSize32(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal readonly void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue(Data, AsReadOnlySpan().ToArray(), typeof(Byte[]));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			GetObjectData(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ref UnsafeSize32 Deserialize(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}

			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize32 IUnsafeSize<UnsafeSize32>.Deserialize(SerializationInfo info, StreamingContext context)
		{
			return ref Deserialize(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Support<T>() where T : struct
		{
			return Unsafe.SizeOf<T>() is > 0 and <= Size;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ref T As<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<T> AsReadOnlySpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<Byte> AsSpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<T> AsSpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new Span<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize32 IUnsafeSize<UnsafeSize32>.SetFull()
		{
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize32 Fill(Byte value)
		{
			AsSpan().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize32 Fill<T>(T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize32 Fill<T>(in T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize32 Clear()
		{
			return ref Reset();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize32 Reset()
		{
			return ref Fill(default(Byte));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(Span<Byte> destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(ref UnsafeSize32 destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(Span<Byte> destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(ref UnsafeSize32 destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Byte[] ToArray()
		{
			return AsReadOnlySpan().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly Byte[] IUnsafeSize<UnsafeSize32>.ToFullArray()
		{
			return ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly T[] ToArray<T>() where T : struct
		{
			return AsReadOnlySpan<T>().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly T[] IUnsafeSize<UnsafeSize32>.ToFullArray<T>()
		{
			return ToArray<T>();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref *pointer;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(ReadOnlySpan<Byte> other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(UnsafeSize32 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(in UnsafeSize32 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly Int32 GetHashCode()
		{
			HashCode hash = new HashCode();
			hash.AddBytes(this);
			return hash.ToHashCode();
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other switch
			{
				null => IsEmpty,
				UnsafeSize32 value => Equals(value),
				Memory<Byte> value => Equals(value.Span),
				ReadOnlyMemory<Byte> value => Equals(value.Span),
				_ => false
			};
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(ReadOnlySpan<Byte> other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(UnsafeSize32 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(in UnsafeSize32 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly String ToString()
		{
			return Convert.ToHexString(this);
		}
	}

	[Serializable]
	public unsafe struct UnsafeSize40 : IUnsafeSize<UnsafeSize40>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize40 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize40 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		public static Boolean operator ==(in UnsafeSize40 first, ReadOnlySpan<Byte> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator ==(in UnsafeSize40 first, in UnsafeSize40 second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(in UnsafeSize40 first, ReadOnlySpan<Byte> second)
		{
			return !(first == second);
		}

		public static Boolean operator !=(in UnsafeSize40 first, in UnsafeSize40 second)
		{
			return !(first == second);
		}

		public static Boolean operator >(in UnsafeSize40 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >(in UnsafeSize40 first, in UnsafeSize40 second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >=(in UnsafeSize40 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator >=(in UnsafeSize40 first, in UnsafeSize40 second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator <(in UnsafeSize40 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <(in UnsafeSize40 first, in UnsafeSize40 second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <=(in UnsafeSize40 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) <= 0;
		}

		public static Boolean operator <=(in UnsafeSize40 first, in UnsafeSize40 second)
		{
			return first.CompareTo(second) <= 0;
		}

		private const String Data = UnsafeSize.Data;
		public const Int32 Size = 40;
		private fixed Byte Internal[Size];

		public readonly Type Type
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return typeof(UnsafeSize40);
			}
		}

		ref UnsafeSize40 IUnsafeSize<UnsafeSize40>.This
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return ref this;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size;
			}
		}

		readonly Span<Byte> IUnsafeSize<UnsafeSize40>.Full
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this;
			}
		}

		public readonly Boolean IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size <= 0 || AsReadOnlySpan().All(default(Byte));
			}
		}

		public UnsafeSize40(ReadOnlySpan<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}.");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, value.Length);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal UnsafeSize40(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal readonly void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue(Data, AsReadOnlySpan().ToArray(), typeof(Byte[]));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			GetObjectData(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ref UnsafeSize40 Deserialize(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}

			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize40 IUnsafeSize<UnsafeSize40>.Deserialize(SerializationInfo info, StreamingContext context)
		{
			return ref Deserialize(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Support<T>() where T : struct
		{
			return Unsafe.SizeOf<T>() is > 0 and <= Size;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ref T As<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<T> AsReadOnlySpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<Byte> AsSpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<T> AsSpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new Span<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize40 IUnsafeSize<UnsafeSize40>.SetFull()
		{
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize40 Fill(Byte value)
		{
			AsSpan().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize40 Fill<T>(T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize40 Fill<T>(in T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize40 Clear()
		{
			return ref Reset();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize40 Reset()
		{
			return ref Fill(default(Byte));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(Span<Byte> destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(ref UnsafeSize40 destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(Span<Byte> destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(ref UnsafeSize40 destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Byte[] ToArray()
		{
			return AsReadOnlySpan().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly Byte[] IUnsafeSize<UnsafeSize40>.ToFullArray()
		{
			return ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly T[] ToArray<T>() where T : struct
		{
			return AsReadOnlySpan<T>().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly T[] IUnsafeSize<UnsafeSize40>.ToFullArray<T>()
		{
			return ToArray<T>();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref *pointer;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(ReadOnlySpan<Byte> other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(UnsafeSize40 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(in UnsafeSize40 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly Int32 GetHashCode()
		{
			HashCode hash = new HashCode();
			hash.AddBytes(this);
			return hash.ToHashCode();
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other switch
			{
				null => IsEmpty,
				UnsafeSize40 value => Equals(value),
				Memory<Byte> value => Equals(value.Span),
				ReadOnlyMemory<Byte> value => Equals(value.Span),
				_ => false
			};
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(ReadOnlySpan<Byte> other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(UnsafeSize40 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(in UnsafeSize40 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly String ToString()
		{
			return Convert.ToHexString(this);
		}
	}

	[Serializable]
	public unsafe struct UnsafeSize48 : IUnsafeSize<UnsafeSize48>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize48 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize48 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		public static Boolean operator ==(in UnsafeSize48 first, ReadOnlySpan<Byte> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator ==(in UnsafeSize48 first, in UnsafeSize48 second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(in UnsafeSize48 first, ReadOnlySpan<Byte> second)
		{
			return !(first == second);
		}

		public static Boolean operator !=(in UnsafeSize48 first, in UnsafeSize48 second)
		{
			return !(first == second);
		}

		public static Boolean operator >(in UnsafeSize48 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >(in UnsafeSize48 first, in UnsafeSize48 second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >=(in UnsafeSize48 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator >=(in UnsafeSize48 first, in UnsafeSize48 second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator <(in UnsafeSize48 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <(in UnsafeSize48 first, in UnsafeSize48 second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <=(in UnsafeSize48 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) <= 0;
		}

		public static Boolean operator <=(in UnsafeSize48 first, in UnsafeSize48 second)
		{
			return first.CompareTo(second) <= 0;
		}

		private const String Data = UnsafeSize.Data;
		public const Int32 Size = 48;
		private fixed Byte Internal[Size];

		public readonly Type Type
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return typeof(UnsafeSize48);
			}
		}

		ref UnsafeSize48 IUnsafeSize<UnsafeSize48>.This
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return ref this;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size;
			}
		}

		readonly Span<Byte> IUnsafeSize<UnsafeSize48>.Full
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this;
			}
		}

		public readonly Boolean IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size <= 0 || AsReadOnlySpan().All(default(Byte));
			}
		}

		public UnsafeSize48(ReadOnlySpan<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}.");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, value.Length);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal UnsafeSize48(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal readonly void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue(Data, AsReadOnlySpan().ToArray(), typeof(Byte[]));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			GetObjectData(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ref UnsafeSize48 Deserialize(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}

			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize48 IUnsafeSize<UnsafeSize48>.Deserialize(SerializationInfo info, StreamingContext context)
		{
			return ref Deserialize(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Support<T>() where T : struct
		{
			return Unsafe.SizeOf<T>() is > 0 and <= Size;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ref T As<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<T> AsReadOnlySpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<Byte> AsSpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<T> AsSpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new Span<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize48 IUnsafeSize<UnsafeSize48>.SetFull()
		{
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize48 Fill(Byte value)
		{
			AsSpan().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize48 Fill<T>(T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize48 Fill<T>(in T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize48 Clear()
		{
			return ref Reset();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize48 Reset()
		{
			return ref Fill(default(Byte));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(Span<Byte> destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(ref UnsafeSize48 destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(Span<Byte> destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(ref UnsafeSize48 destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Byte[] ToArray()
		{
			return AsReadOnlySpan().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly Byte[] IUnsafeSize<UnsafeSize48>.ToFullArray()
		{
			return ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly T[] ToArray<T>() where T : struct
		{
			return AsReadOnlySpan<T>().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly T[] IUnsafeSize<UnsafeSize48>.ToFullArray<T>()
		{
			return ToArray<T>();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref *pointer;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(ReadOnlySpan<Byte> other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(UnsafeSize48 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(in UnsafeSize48 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly Int32 GetHashCode()
		{
			HashCode hash = new HashCode();
			hash.AddBytes(this);
			return hash.ToHashCode();
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other switch
			{
				null => IsEmpty,
				UnsafeSize48 value => Equals(value),
				Memory<Byte> value => Equals(value.Span),
				ReadOnlyMemory<Byte> value => Equals(value.Span),
				_ => false
			};
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(ReadOnlySpan<Byte> other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(UnsafeSize48 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(in UnsafeSize48 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly String ToString()
		{
			return Convert.ToHexString(this);
		}
	}

	[Serializable]
	public unsafe struct UnsafeSize56 : IUnsafeSize<UnsafeSize56>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize56 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize56 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		public static Boolean operator ==(in UnsafeSize56 first, ReadOnlySpan<Byte> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator ==(in UnsafeSize56 first, in UnsafeSize56 second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(in UnsafeSize56 first, ReadOnlySpan<Byte> second)
		{
			return !(first == second);
		}

		public static Boolean operator !=(in UnsafeSize56 first, in UnsafeSize56 second)
		{
			return !(first == second);
		}

		public static Boolean operator >(in UnsafeSize56 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >(in UnsafeSize56 first, in UnsafeSize56 second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >=(in UnsafeSize56 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator >=(in UnsafeSize56 first, in UnsafeSize56 second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator <(in UnsafeSize56 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <(in UnsafeSize56 first, in UnsafeSize56 second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <=(in UnsafeSize56 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) <= 0;
		}

		public static Boolean operator <=(in UnsafeSize56 first, in UnsafeSize56 second)
		{
			return first.CompareTo(second) <= 0;
		}

		private const String Data = UnsafeSize.Data;
		public const Int32 Size = 56;
		private fixed Byte Internal[Size];

		public readonly Type Type
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return typeof(UnsafeSize56);
			}
		}

		ref UnsafeSize56 IUnsafeSize<UnsafeSize56>.This
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return ref this;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size;
			}
		}

		readonly Span<Byte> IUnsafeSize<UnsafeSize56>.Full
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this;
			}
		}

		public readonly Boolean IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size <= 0 || AsReadOnlySpan().All(default(Byte));
			}
		}

		public UnsafeSize56(ReadOnlySpan<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}.");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, value.Length);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal UnsafeSize56(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal readonly void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue(Data, AsReadOnlySpan().ToArray(), typeof(Byte[]));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			GetObjectData(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ref UnsafeSize56 Deserialize(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}

			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize56 IUnsafeSize<UnsafeSize56>.Deserialize(SerializationInfo info, StreamingContext context)
		{
			return ref Deserialize(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Support<T>() where T : struct
		{
			return Unsafe.SizeOf<T>() is > 0 and <= Size;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ref T As<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<T> AsReadOnlySpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<Byte> AsSpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<T> AsSpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new Span<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize56 IUnsafeSize<UnsafeSize56>.SetFull()
		{
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize56 Fill(Byte value)
		{
			AsSpan().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize56 Fill<T>(T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize56 Fill<T>(in T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize56 Clear()
		{
			return ref Reset();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize56 Reset()
		{
			return ref Fill(default(Byte));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(Span<Byte> destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(ref UnsafeSize56 destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(Span<Byte> destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(ref UnsafeSize56 destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Byte[] ToArray()
		{
			return AsReadOnlySpan().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly Byte[] IUnsafeSize<UnsafeSize56>.ToFullArray()
		{
			return ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly T[] ToArray<T>() where T : struct
		{
			return AsReadOnlySpan<T>().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly T[] IUnsafeSize<UnsafeSize56>.ToFullArray<T>()
		{
			return ToArray<T>();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref *pointer;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(ReadOnlySpan<Byte> other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(UnsafeSize56 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(in UnsafeSize56 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly Int32 GetHashCode()
		{
			HashCode hash = new HashCode();
			hash.AddBytes(this);
			return hash.ToHashCode();
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other switch
			{
				null => IsEmpty,
				UnsafeSize56 value => Equals(value),
				Memory<Byte> value => Equals(value.Span),
				ReadOnlyMemory<Byte> value => Equals(value.Span),
				_ => false
			};
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(ReadOnlySpan<Byte> other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(UnsafeSize56 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(in UnsafeSize56 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly String ToString()
		{
			return Convert.ToHexString(this);
		}
	}

	[Serializable]
	public unsafe struct UnsafeSize64 : IUnsafeSize<UnsafeSize64>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize64 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize64 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		public static Boolean operator ==(in UnsafeSize64 first, ReadOnlySpan<Byte> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator ==(in UnsafeSize64 first, in UnsafeSize64 second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(in UnsafeSize64 first, ReadOnlySpan<Byte> second)
		{
			return !(first == second);
		}

		public static Boolean operator !=(in UnsafeSize64 first, in UnsafeSize64 second)
		{
			return !(first == second);
		}

		public static Boolean operator >(in UnsafeSize64 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >(in UnsafeSize64 first, in UnsafeSize64 second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >=(in UnsafeSize64 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator >=(in UnsafeSize64 first, in UnsafeSize64 second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator <(in UnsafeSize64 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <(in UnsafeSize64 first, in UnsafeSize64 second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <=(in UnsafeSize64 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) <= 0;
		}

		public static Boolean operator <=(in UnsafeSize64 first, in UnsafeSize64 second)
		{
			return first.CompareTo(second) <= 0;
		}

		private const String Data = UnsafeSize.Data;
		public const Int32 Size = 64;
		private fixed Byte Internal[Size];

		public readonly Type Type
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return typeof(UnsafeSize64);
			}
		}

		ref UnsafeSize64 IUnsafeSize<UnsafeSize64>.This
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return ref this;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size;
			}
		}

		readonly Span<Byte> IUnsafeSize<UnsafeSize64>.Full
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this;
			}
		}

		public readonly Boolean IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size <= 0 || AsReadOnlySpan().All(default(Byte));
			}
		}

		public UnsafeSize64(ReadOnlySpan<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}.");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, value.Length);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal UnsafeSize64(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal readonly void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue(Data, AsReadOnlySpan().ToArray(), typeof(Byte[]));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			GetObjectData(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ref UnsafeSize64 Deserialize(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}

			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize64 IUnsafeSize<UnsafeSize64>.Deserialize(SerializationInfo info, StreamingContext context)
		{
			return ref Deserialize(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Support<T>() where T : struct
		{
			return Unsafe.SizeOf<T>() is > 0 and <= Size;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ref T As<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<T> AsReadOnlySpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<Byte> AsSpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<T> AsSpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new Span<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize64 IUnsafeSize<UnsafeSize64>.SetFull()
		{
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize64 Fill(Byte value)
		{
			AsSpan().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize64 Fill<T>(T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize64 Fill<T>(in T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize64 Clear()
		{
			return ref Reset();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize64 Reset()
		{
			return ref Fill(default(Byte));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(Span<Byte> destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(ref UnsafeSize64 destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(Span<Byte> destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(ref UnsafeSize64 destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Byte[] ToArray()
		{
			return AsReadOnlySpan().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly Byte[] IUnsafeSize<UnsafeSize64>.ToFullArray()
		{
			return ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly T[] ToArray<T>() where T : struct
		{
			return AsReadOnlySpan<T>().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly T[] IUnsafeSize<UnsafeSize64>.ToFullArray<T>()
		{
			return ToArray<T>();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref *pointer;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(ReadOnlySpan<Byte> other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(UnsafeSize64 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(in UnsafeSize64 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly Int32 GetHashCode()
		{
			HashCode hash = new HashCode();
			hash.AddBytes(this);
			return hash.ToHashCode();
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other switch
			{
				null => IsEmpty,
				UnsafeSize64 value => Equals(value),
				Memory<Byte> value => Equals(value.Span),
				ReadOnlyMemory<Byte> value => Equals(value.Span),
				_ => false
			};
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(ReadOnlySpan<Byte> other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(UnsafeSize64 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(in UnsafeSize64 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly String ToString()
		{
			return Convert.ToHexString(this);
		}
	}

	[Serializable]
	public unsafe struct UnsafeSize80 : IUnsafeSize<UnsafeSize80>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize80 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize80 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		public static Boolean operator ==(in UnsafeSize80 first, ReadOnlySpan<Byte> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator ==(in UnsafeSize80 first, in UnsafeSize80 second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(in UnsafeSize80 first, ReadOnlySpan<Byte> second)
		{
			return !(first == second);
		}

		public static Boolean operator !=(in UnsafeSize80 first, in UnsafeSize80 second)
		{
			return !(first == second);
		}

		public static Boolean operator >(in UnsafeSize80 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >(in UnsafeSize80 first, in UnsafeSize80 second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >=(in UnsafeSize80 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator >=(in UnsafeSize80 first, in UnsafeSize80 second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator <(in UnsafeSize80 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <(in UnsafeSize80 first, in UnsafeSize80 second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <=(in UnsafeSize80 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) <= 0;
		}

		public static Boolean operator <=(in UnsafeSize80 first, in UnsafeSize80 second)
		{
			return first.CompareTo(second) <= 0;
		}

		private const String Data = UnsafeSize.Data;
		public const Int32 Size = 80;
		private fixed Byte Internal[Size];

		public readonly Type Type
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return typeof(UnsafeSize80);
			}
		}

		ref UnsafeSize80 IUnsafeSize<UnsafeSize80>.This
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return ref this;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size;
			}
		}

		readonly Span<Byte> IUnsafeSize<UnsafeSize80>.Full
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this;
			}
		}

		public readonly Boolean IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size <= 0 || AsReadOnlySpan().All(default(Byte));
			}
		}

		public UnsafeSize80(ReadOnlySpan<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}.");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, value.Length);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal UnsafeSize80(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal readonly void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue(Data, AsReadOnlySpan().ToArray(), typeof(Byte[]));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			GetObjectData(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ref UnsafeSize80 Deserialize(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}

			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize80 IUnsafeSize<UnsafeSize80>.Deserialize(SerializationInfo info, StreamingContext context)
		{
			return ref Deserialize(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Support<T>() where T : struct
		{
			return Unsafe.SizeOf<T>() is > 0 and <= Size;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ref T As<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<T> AsReadOnlySpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<Byte> AsSpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<T> AsSpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new Span<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize80 IUnsafeSize<UnsafeSize80>.SetFull()
		{
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize80 Fill(Byte value)
		{
			AsSpan().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize80 Fill<T>(T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize80 Fill<T>(in T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize80 Clear()
		{
			return ref Reset();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize80 Reset()
		{
			return ref Fill(default(Byte));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(Span<Byte> destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(ref UnsafeSize80 destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(Span<Byte> destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(ref UnsafeSize80 destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Byte[] ToArray()
		{
			return AsReadOnlySpan().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly Byte[] IUnsafeSize<UnsafeSize80>.ToFullArray()
		{
			return ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly T[] ToArray<T>() where T : struct
		{
			return AsReadOnlySpan<T>().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly T[] IUnsafeSize<UnsafeSize80>.ToFullArray<T>()
		{
			return ToArray<T>();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref *pointer;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(ReadOnlySpan<Byte> other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(UnsafeSize80 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(in UnsafeSize80 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly Int32 GetHashCode()
		{
			HashCode hash = new HashCode();
			hash.AddBytes(this);
			return hash.ToHashCode();
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other switch
			{
				null => IsEmpty,
				UnsafeSize80 value => Equals(value),
				Memory<Byte> value => Equals(value.Span),
				ReadOnlyMemory<Byte> value => Equals(value.Span),
				_ => false
			};
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(ReadOnlySpan<Byte> other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(UnsafeSize80 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(in UnsafeSize80 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly String ToString()
		{
			return Convert.ToHexString(this);
		}
	}

	[Serializable]
	public unsafe struct UnsafeSize96 : IUnsafeSize<UnsafeSize96>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize96 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize96 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		public static Boolean operator ==(in UnsafeSize96 first, ReadOnlySpan<Byte> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator ==(in UnsafeSize96 first, in UnsafeSize96 second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(in UnsafeSize96 first, ReadOnlySpan<Byte> second)
		{
			return !(first == second);
		}

		public static Boolean operator !=(in UnsafeSize96 first, in UnsafeSize96 second)
		{
			return !(first == second);
		}

		public static Boolean operator >(in UnsafeSize96 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >(in UnsafeSize96 first, in UnsafeSize96 second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >=(in UnsafeSize96 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator >=(in UnsafeSize96 first, in UnsafeSize96 second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator <(in UnsafeSize96 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <(in UnsafeSize96 first, in UnsafeSize96 second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <=(in UnsafeSize96 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) <= 0;
		}

		public static Boolean operator <=(in UnsafeSize96 first, in UnsafeSize96 second)
		{
			return first.CompareTo(second) <= 0;
		}

		private const String Data = UnsafeSize.Data;
		public const Int32 Size = 96;
		private fixed Byte Internal[Size];

		public readonly Type Type
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return typeof(UnsafeSize96);
			}
		}

		ref UnsafeSize96 IUnsafeSize<UnsafeSize96>.This
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return ref this;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size;
			}
		}

		readonly Span<Byte> IUnsafeSize<UnsafeSize96>.Full
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this;
			}
		}

		public readonly Boolean IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size <= 0 || AsReadOnlySpan().All(default(Byte));
			}
		}

		public UnsafeSize96(ReadOnlySpan<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}.");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, value.Length);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal UnsafeSize96(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal readonly void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue(Data, AsReadOnlySpan().ToArray(), typeof(Byte[]));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			GetObjectData(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ref UnsafeSize96 Deserialize(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}

			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize96 IUnsafeSize<UnsafeSize96>.Deserialize(SerializationInfo info, StreamingContext context)
		{
			return ref Deserialize(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Support<T>() where T : struct
		{
			return Unsafe.SizeOf<T>() is > 0 and <= Size;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ref T As<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<T> AsReadOnlySpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<Byte> AsSpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<T> AsSpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new Span<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize96 IUnsafeSize<UnsafeSize96>.SetFull()
		{
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize96 Fill(Byte value)
		{
			AsSpan().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize96 Fill<T>(T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize96 Fill<T>(in T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize96 Clear()
		{
			return ref Reset();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize96 Reset()
		{
			return ref Fill(default(Byte));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(Span<Byte> destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(ref UnsafeSize96 destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(Span<Byte> destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(ref UnsafeSize96 destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Byte[] ToArray()
		{
			return AsReadOnlySpan().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly Byte[] IUnsafeSize<UnsafeSize96>.ToFullArray()
		{
			return ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly T[] ToArray<T>() where T : struct
		{
			return AsReadOnlySpan<T>().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly T[] IUnsafeSize<UnsafeSize96>.ToFullArray<T>()
		{
			return ToArray<T>();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref *pointer;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(ReadOnlySpan<Byte> other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(UnsafeSize96 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(in UnsafeSize96 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly Int32 GetHashCode()
		{
			HashCode hash = new HashCode();
			hash.AddBytes(this);
			return hash.ToHashCode();
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other switch
			{
				null => IsEmpty,
				UnsafeSize96 value => Equals(value),
				Memory<Byte> value => Equals(value.Span),
				ReadOnlyMemory<Byte> value => Equals(value.Span),
				_ => false
			};
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(ReadOnlySpan<Byte> other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(UnsafeSize96 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(in UnsafeSize96 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly String ToString()
		{
			return Convert.ToHexString(this);
		}
	}

	[Serializable]
	public unsafe struct UnsafeSize112 : IUnsafeSize<UnsafeSize112>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize112 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize112 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		public static Boolean operator ==(in UnsafeSize112 first, ReadOnlySpan<Byte> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator ==(in UnsafeSize112 first, in UnsafeSize112 second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(in UnsafeSize112 first, ReadOnlySpan<Byte> second)
		{
			return !(first == second);
		}

		public static Boolean operator !=(in UnsafeSize112 first, in UnsafeSize112 second)
		{
			return !(first == second);
		}

		public static Boolean operator >(in UnsafeSize112 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >(in UnsafeSize112 first, in UnsafeSize112 second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >=(in UnsafeSize112 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator >=(in UnsafeSize112 first, in UnsafeSize112 second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator <(in UnsafeSize112 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <(in UnsafeSize112 first, in UnsafeSize112 second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <=(in UnsafeSize112 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) <= 0;
		}

		public static Boolean operator <=(in UnsafeSize112 first, in UnsafeSize112 second)
		{
			return first.CompareTo(second) <= 0;
		}

		private const String Data = UnsafeSize.Data;
		public const Int32 Size = 112;
		private fixed Byte Internal[Size];

		public readonly Type Type
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return typeof(UnsafeSize112);
			}
		}

		ref UnsafeSize112 IUnsafeSize<UnsafeSize112>.This
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return ref this;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size;
			}
		}

		readonly Span<Byte> IUnsafeSize<UnsafeSize112>.Full
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this;
			}
		}

		public readonly Boolean IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size <= 0 || AsReadOnlySpan().All(default(Byte));
			}
		}

		public UnsafeSize112(ReadOnlySpan<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}.");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, value.Length);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal UnsafeSize112(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal readonly void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue(Data, AsReadOnlySpan().ToArray(), typeof(Byte[]));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			GetObjectData(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ref UnsafeSize112 Deserialize(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}

			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize112 IUnsafeSize<UnsafeSize112>.Deserialize(SerializationInfo info, StreamingContext context)
		{
			return ref Deserialize(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Support<T>() where T : struct
		{
			return Unsafe.SizeOf<T>() is > 0 and <= Size;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ref T As<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<T> AsReadOnlySpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<Byte> AsSpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<T> AsSpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new Span<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize112 IUnsafeSize<UnsafeSize112>.SetFull()
		{
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize112 Fill(Byte value)
		{
			AsSpan().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize112 Fill<T>(T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize112 Fill<T>(in T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize112 Clear()
		{
			return ref Reset();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize112 Reset()
		{
			return ref Fill(default(Byte));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(Span<Byte> destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(ref UnsafeSize112 destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(Span<Byte> destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(ref UnsafeSize112 destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Byte[] ToArray()
		{
			return AsReadOnlySpan().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly Byte[] IUnsafeSize<UnsafeSize112>.ToFullArray()
		{
			return ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly T[] ToArray<T>() where T : struct
		{
			return AsReadOnlySpan<T>().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly T[] IUnsafeSize<UnsafeSize112>.ToFullArray<T>()
		{
			return ToArray<T>();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref *pointer;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(ReadOnlySpan<Byte> other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(UnsafeSize112 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(in UnsafeSize112 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly Int32 GetHashCode()
		{
			HashCode hash = new HashCode();
			hash.AddBytes(this);
			return hash.ToHashCode();
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other switch
			{
				null => IsEmpty,
				UnsafeSize112 value => Equals(value),
				Memory<Byte> value => Equals(value.Span),
				ReadOnlyMemory<Byte> value => Equals(value.Span),
				_ => false
			};
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(ReadOnlySpan<Byte> other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(UnsafeSize112 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(in UnsafeSize112 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly String ToString()
		{
			return Convert.ToHexString(this);
		}
	}

	[Serializable]
	public unsafe struct UnsafeSize128 : IUnsafeSize<UnsafeSize128>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize128 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize128 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		public static Boolean operator ==(in UnsafeSize128 first, ReadOnlySpan<Byte> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator ==(in UnsafeSize128 first, in UnsafeSize128 second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(in UnsafeSize128 first, ReadOnlySpan<Byte> second)
		{
			return !(first == second);
		}

		public static Boolean operator !=(in UnsafeSize128 first, in UnsafeSize128 second)
		{
			return !(first == second);
		}

		public static Boolean operator >(in UnsafeSize128 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >(in UnsafeSize128 first, in UnsafeSize128 second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >=(in UnsafeSize128 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator >=(in UnsafeSize128 first, in UnsafeSize128 second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator <(in UnsafeSize128 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <(in UnsafeSize128 first, in UnsafeSize128 second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <=(in UnsafeSize128 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) <= 0;
		}

		public static Boolean operator <=(in UnsafeSize128 first, in UnsafeSize128 second)
		{
			return first.CompareTo(second) <= 0;
		}

		private const String Data = UnsafeSize.Data;
		public const Int32 Size = 128;
		private fixed Byte Internal[Size];

		public readonly Type Type
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return typeof(UnsafeSize128);
			}
		}

		ref UnsafeSize128 IUnsafeSize<UnsafeSize128>.This
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return ref this;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size;
			}
		}

		readonly Span<Byte> IUnsafeSize<UnsafeSize128>.Full
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this;
			}
		}

		public readonly Boolean IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size <= 0 || AsReadOnlySpan().All(default(Byte));
			}
		}

		public UnsafeSize128(ReadOnlySpan<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}.");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, value.Length);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal UnsafeSize128(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal readonly void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue(Data, AsReadOnlySpan().ToArray(), typeof(Byte[]));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			GetObjectData(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ref UnsafeSize128 Deserialize(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}

			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize128 IUnsafeSize<UnsafeSize128>.Deserialize(SerializationInfo info, StreamingContext context)
		{
			return ref Deserialize(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Support<T>() where T : struct
		{
			return Unsafe.SizeOf<T>() is > 0 and <= Size;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ref T As<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<T> AsReadOnlySpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<Byte> AsSpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<T> AsSpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new Span<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize128 IUnsafeSize<UnsafeSize128>.SetFull()
		{
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize128 Fill(Byte value)
		{
			AsSpan().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize128 Fill<T>(T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize128 Fill<T>(in T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize128 Clear()
		{
			return ref Reset();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize128 Reset()
		{
			return ref Fill(default(Byte));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(Span<Byte> destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(ref UnsafeSize128 destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(Span<Byte> destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(ref UnsafeSize128 destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Byte[] ToArray()
		{
			return AsReadOnlySpan().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly Byte[] IUnsafeSize<UnsafeSize128>.ToFullArray()
		{
			return ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly T[] ToArray<T>() where T : struct
		{
			return AsReadOnlySpan<T>().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly T[] IUnsafeSize<UnsafeSize128>.ToFullArray<T>()
		{
			return ToArray<T>();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref *pointer;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(ReadOnlySpan<Byte> other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(UnsafeSize128 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(in UnsafeSize128 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly Int32 GetHashCode()
		{
			HashCode hash = new HashCode();
			hash.AddBytes(this);
			return hash.ToHashCode();
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other switch
			{
				null => IsEmpty,
				UnsafeSize128 value => Equals(value),
				Memory<Byte> value => Equals(value.Span),
				ReadOnlyMemory<Byte> value => Equals(value.Span),
				_ => false
			};
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(ReadOnlySpan<Byte> other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(UnsafeSize128 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(in UnsafeSize128 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly String ToString()
		{
			return Convert.ToHexString(this);
		}
	}

	[Serializable]
	public unsafe struct UnsafeSize144 : IUnsafeSize<UnsafeSize144>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize144 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize144 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		public static Boolean operator ==(in UnsafeSize144 first, ReadOnlySpan<Byte> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator ==(in UnsafeSize144 first, in UnsafeSize144 second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(in UnsafeSize144 first, ReadOnlySpan<Byte> second)
		{
			return !(first == second);
		}

		public static Boolean operator !=(in UnsafeSize144 first, in UnsafeSize144 second)
		{
			return !(first == second);
		}

		public static Boolean operator >(in UnsafeSize144 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >(in UnsafeSize144 first, in UnsafeSize144 second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >=(in UnsafeSize144 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator >=(in UnsafeSize144 first, in UnsafeSize144 second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator <(in UnsafeSize144 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <(in UnsafeSize144 first, in UnsafeSize144 second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <=(in UnsafeSize144 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) <= 0;
		}

		public static Boolean operator <=(in UnsafeSize144 first, in UnsafeSize144 second)
		{
			return first.CompareTo(second) <= 0;
		}

		private const String Data = UnsafeSize.Data;
		public const Int32 Size = 144;
		private fixed Byte Internal[Size];

		public readonly Type Type
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return typeof(UnsafeSize144);
			}
		}

		ref UnsafeSize144 IUnsafeSize<UnsafeSize144>.This
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return ref this;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size;
			}
		}

		readonly Span<Byte> IUnsafeSize<UnsafeSize144>.Full
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this;
			}
		}

		public readonly Boolean IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size <= 0 || AsReadOnlySpan().All(default(Byte));
			}
		}

		public UnsafeSize144(ReadOnlySpan<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}.");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, value.Length);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal UnsafeSize144(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal readonly void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue(Data, AsReadOnlySpan().ToArray(), typeof(Byte[]));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			GetObjectData(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ref UnsafeSize144 Deserialize(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}

			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize144 IUnsafeSize<UnsafeSize144>.Deserialize(SerializationInfo info, StreamingContext context)
		{
			return ref Deserialize(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Support<T>() where T : struct
		{
			return Unsafe.SizeOf<T>() is > 0 and <= Size;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ref T As<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<T> AsReadOnlySpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<Byte> AsSpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<T> AsSpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new Span<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize144 IUnsafeSize<UnsafeSize144>.SetFull()
		{
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize144 Fill(Byte value)
		{
			AsSpan().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize144 Fill<T>(T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize144 Fill<T>(in T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize144 Clear()
		{
			return ref Reset();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize144 Reset()
		{
			return ref Fill(default(Byte));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(Span<Byte> destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(ref UnsafeSize144 destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(Span<Byte> destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(ref UnsafeSize144 destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Byte[] ToArray()
		{
			return AsReadOnlySpan().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly Byte[] IUnsafeSize<UnsafeSize144>.ToFullArray()
		{
			return ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly T[] ToArray<T>() where T : struct
		{
			return AsReadOnlySpan<T>().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly T[] IUnsafeSize<UnsafeSize144>.ToFullArray<T>()
		{
			return ToArray<T>();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref *pointer;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(ReadOnlySpan<Byte> other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(UnsafeSize144 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(in UnsafeSize144 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly Int32 GetHashCode()
		{
			HashCode hash = new HashCode();
			hash.AddBytes(this);
			return hash.ToHashCode();
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other switch
			{
				null => IsEmpty,
				UnsafeSize144 value => Equals(value),
				Memory<Byte> value => Equals(value.Span),
				ReadOnlyMemory<Byte> value => Equals(value.Span),
				_ => false
			};
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(ReadOnlySpan<Byte> other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(UnsafeSize144 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(in UnsafeSize144 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly String ToString()
		{
			return Convert.ToHexString(this);
		}
	}

	[Serializable]
	public unsafe struct UnsafeSize160 : IUnsafeSize<UnsafeSize160>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize160 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize160 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		public static Boolean operator ==(in UnsafeSize160 first, ReadOnlySpan<Byte> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator ==(in UnsafeSize160 first, in UnsafeSize160 second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(in UnsafeSize160 first, ReadOnlySpan<Byte> second)
		{
			return !(first == second);
		}

		public static Boolean operator !=(in UnsafeSize160 first, in UnsafeSize160 second)
		{
			return !(first == second);
		}

		public static Boolean operator >(in UnsafeSize160 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >(in UnsafeSize160 first, in UnsafeSize160 second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >=(in UnsafeSize160 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator >=(in UnsafeSize160 first, in UnsafeSize160 second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator <(in UnsafeSize160 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <(in UnsafeSize160 first, in UnsafeSize160 second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <=(in UnsafeSize160 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) <= 0;
		}

		public static Boolean operator <=(in UnsafeSize160 first, in UnsafeSize160 second)
		{
			return first.CompareTo(second) <= 0;
		}

		private const String Data = UnsafeSize.Data;
		public const Int32 Size = 160;
		private fixed Byte Internal[Size];

		public readonly Type Type
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return typeof(UnsafeSize160);
			}
		}

		ref UnsafeSize160 IUnsafeSize<UnsafeSize160>.This
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return ref this;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size;
			}
		}

		readonly Span<Byte> IUnsafeSize<UnsafeSize160>.Full
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this;
			}
		}

		public readonly Boolean IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size <= 0 || AsReadOnlySpan().All(default(Byte));
			}
		}

		public UnsafeSize160(ReadOnlySpan<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}.");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, value.Length);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal UnsafeSize160(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal readonly void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue(Data, AsReadOnlySpan().ToArray(), typeof(Byte[]));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			GetObjectData(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ref UnsafeSize160 Deserialize(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}

			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize160 IUnsafeSize<UnsafeSize160>.Deserialize(SerializationInfo info, StreamingContext context)
		{
			return ref Deserialize(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Support<T>() where T : struct
		{
			return Unsafe.SizeOf<T>() is > 0 and <= Size;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ref T As<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<T> AsReadOnlySpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<Byte> AsSpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<T> AsSpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new Span<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize160 IUnsafeSize<UnsafeSize160>.SetFull()
		{
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize160 Fill(Byte value)
		{
			AsSpan().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize160 Fill<T>(T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize160 Fill<T>(in T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize160 Clear()
		{
			return ref Reset();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize160 Reset()
		{
			return ref Fill(default(Byte));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(Span<Byte> destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(ref UnsafeSize160 destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(Span<Byte> destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(ref UnsafeSize160 destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Byte[] ToArray()
		{
			return AsReadOnlySpan().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly Byte[] IUnsafeSize<UnsafeSize160>.ToFullArray()
		{
			return ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly T[] ToArray<T>() where T : struct
		{
			return AsReadOnlySpan<T>().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly T[] IUnsafeSize<UnsafeSize160>.ToFullArray<T>()
		{
			return ToArray<T>();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref *pointer;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(ReadOnlySpan<Byte> other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(UnsafeSize160 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(in UnsafeSize160 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly Int32 GetHashCode()
		{
			HashCode hash = new HashCode();
			hash.AddBytes(this);
			return hash.ToHashCode();
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other switch
			{
				null => IsEmpty,
				UnsafeSize160 value => Equals(value),
				Memory<Byte> value => Equals(value.Span),
				ReadOnlyMemory<Byte> value => Equals(value.Span),
				_ => false
			};
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(ReadOnlySpan<Byte> other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(UnsafeSize160 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(in UnsafeSize160 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly String ToString()
		{
			return Convert.ToHexString(this);
		}
	}

	[Serializable]
	public unsafe struct UnsafeSize176 : IUnsafeSize<UnsafeSize176>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize176 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize176 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		public static Boolean operator ==(in UnsafeSize176 first, ReadOnlySpan<Byte> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator ==(in UnsafeSize176 first, in UnsafeSize176 second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(in UnsafeSize176 first, ReadOnlySpan<Byte> second)
		{
			return !(first == second);
		}

		public static Boolean operator !=(in UnsafeSize176 first, in UnsafeSize176 second)
		{
			return !(first == second);
		}

		public static Boolean operator >(in UnsafeSize176 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >(in UnsafeSize176 first, in UnsafeSize176 second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >=(in UnsafeSize176 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator >=(in UnsafeSize176 first, in UnsafeSize176 second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator <(in UnsafeSize176 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <(in UnsafeSize176 first, in UnsafeSize176 second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <=(in UnsafeSize176 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) <= 0;
		}

		public static Boolean operator <=(in UnsafeSize176 first, in UnsafeSize176 second)
		{
			return first.CompareTo(second) <= 0;
		}

		private const String Data = UnsafeSize.Data;
		public const Int32 Size = 176;
		private fixed Byte Internal[Size];

		public readonly Type Type
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return typeof(UnsafeSize176);
			}
		}

		ref UnsafeSize176 IUnsafeSize<UnsafeSize176>.This
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return ref this;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size;
			}
		}

		readonly Span<Byte> IUnsafeSize<UnsafeSize176>.Full
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this;
			}
		}

		public readonly Boolean IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size <= 0 || AsReadOnlySpan().All(default(Byte));
			}
		}

		public UnsafeSize176(ReadOnlySpan<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}.");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, value.Length);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal UnsafeSize176(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal readonly void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue(Data, AsReadOnlySpan().ToArray(), typeof(Byte[]));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			GetObjectData(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ref UnsafeSize176 Deserialize(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}

			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize176 IUnsafeSize<UnsafeSize176>.Deserialize(SerializationInfo info, StreamingContext context)
		{
			return ref Deserialize(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Support<T>() where T : struct
		{
			return Unsafe.SizeOf<T>() is > 0 and <= Size;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ref T As<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<T> AsReadOnlySpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<Byte> AsSpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<T> AsSpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new Span<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize176 IUnsafeSize<UnsafeSize176>.SetFull()
		{
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize176 Fill(Byte value)
		{
			AsSpan().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize176 Fill<T>(T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize176 Fill<T>(in T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize176 Clear()
		{
			return ref Reset();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize176 Reset()
		{
			return ref Fill(default(Byte));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(Span<Byte> destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(ref UnsafeSize176 destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(Span<Byte> destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(ref UnsafeSize176 destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Byte[] ToArray()
		{
			return AsReadOnlySpan().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly Byte[] IUnsafeSize<UnsafeSize176>.ToFullArray()
		{
			return ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly T[] ToArray<T>() where T : struct
		{
			return AsReadOnlySpan<T>().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly T[] IUnsafeSize<UnsafeSize176>.ToFullArray<T>()
		{
			return ToArray<T>();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref *pointer;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(ReadOnlySpan<Byte> other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(UnsafeSize176 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(in UnsafeSize176 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly Int32 GetHashCode()
		{
			HashCode hash = new HashCode();
			hash.AddBytes(this);
			return hash.ToHashCode();
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other switch
			{
				null => IsEmpty,
				UnsafeSize176 value => Equals(value),
				Memory<Byte> value => Equals(value.Span),
				ReadOnlyMemory<Byte> value => Equals(value.Span),
				_ => false
			};
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(ReadOnlySpan<Byte> other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(UnsafeSize176 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(in UnsafeSize176 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly String ToString()
		{
			return Convert.ToHexString(this);
		}
	}

	[Serializable]
	public unsafe struct UnsafeSize192 : IUnsafeSize<UnsafeSize192>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize192 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize192 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		public static Boolean operator ==(in UnsafeSize192 first, ReadOnlySpan<Byte> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator ==(in UnsafeSize192 first, in UnsafeSize192 second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(in UnsafeSize192 first, ReadOnlySpan<Byte> second)
		{
			return !(first == second);
		}

		public static Boolean operator !=(in UnsafeSize192 first, in UnsafeSize192 second)
		{
			return !(first == second);
		}

		public static Boolean operator >(in UnsafeSize192 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >(in UnsafeSize192 first, in UnsafeSize192 second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >=(in UnsafeSize192 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator >=(in UnsafeSize192 first, in UnsafeSize192 second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator <(in UnsafeSize192 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <(in UnsafeSize192 first, in UnsafeSize192 second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <=(in UnsafeSize192 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) <= 0;
		}

		public static Boolean operator <=(in UnsafeSize192 first, in UnsafeSize192 second)
		{
			return first.CompareTo(second) <= 0;
		}

		private const String Data = UnsafeSize.Data;
		public const Int32 Size = 192;
		private fixed Byte Internal[Size];

		public readonly Type Type
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return typeof(UnsafeSize192);
			}
		}

		ref UnsafeSize192 IUnsafeSize<UnsafeSize192>.This
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return ref this;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size;
			}
		}

		readonly Span<Byte> IUnsafeSize<UnsafeSize192>.Full
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this;
			}
		}

		public readonly Boolean IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size <= 0 || AsReadOnlySpan().All(default(Byte));
			}
		}

		public UnsafeSize192(ReadOnlySpan<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}.");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, value.Length);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal UnsafeSize192(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal readonly void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue(Data, AsReadOnlySpan().ToArray(), typeof(Byte[]));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			GetObjectData(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ref UnsafeSize192 Deserialize(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}

			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize192 IUnsafeSize<UnsafeSize192>.Deserialize(SerializationInfo info, StreamingContext context)
		{
			return ref Deserialize(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Support<T>() where T : struct
		{
			return Unsafe.SizeOf<T>() is > 0 and <= Size;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ref T As<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<T> AsReadOnlySpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<Byte> AsSpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<T> AsSpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new Span<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize192 IUnsafeSize<UnsafeSize192>.SetFull()
		{
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize192 Fill(Byte value)
		{
			AsSpan().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize192 Fill<T>(T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize192 Fill<T>(in T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize192 Clear()
		{
			return ref Reset();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize192 Reset()
		{
			return ref Fill(default(Byte));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(Span<Byte> destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(ref UnsafeSize192 destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(Span<Byte> destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(ref UnsafeSize192 destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Byte[] ToArray()
		{
			return AsReadOnlySpan().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly Byte[] IUnsafeSize<UnsafeSize192>.ToFullArray()
		{
			return ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly T[] ToArray<T>() where T : struct
		{
			return AsReadOnlySpan<T>().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly T[] IUnsafeSize<UnsafeSize192>.ToFullArray<T>()
		{
			return ToArray<T>();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref *pointer;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(ReadOnlySpan<Byte> other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(UnsafeSize192 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(in UnsafeSize192 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly Int32 GetHashCode()
		{
			HashCode hash = new HashCode();
			hash.AddBytes(this);
			return hash.ToHashCode();
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other switch
			{
				null => IsEmpty,
				UnsafeSize192 value => Equals(value),
				Memory<Byte> value => Equals(value.Span),
				ReadOnlyMemory<Byte> value => Equals(value.Span),
				_ => false
			};
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(ReadOnlySpan<Byte> other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(UnsafeSize192 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(in UnsafeSize192 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly String ToString()
		{
			return Convert.ToHexString(this);
		}
	}

	[Serializable]
	public unsafe struct UnsafeSize208 : IUnsafeSize<UnsafeSize208>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize208 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize208 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		public static Boolean operator ==(in UnsafeSize208 first, ReadOnlySpan<Byte> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator ==(in UnsafeSize208 first, in UnsafeSize208 second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(in UnsafeSize208 first, ReadOnlySpan<Byte> second)
		{
			return !(first == second);
		}

		public static Boolean operator !=(in UnsafeSize208 first, in UnsafeSize208 second)
		{
			return !(first == second);
		}

		public static Boolean operator >(in UnsafeSize208 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >(in UnsafeSize208 first, in UnsafeSize208 second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >=(in UnsafeSize208 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator >=(in UnsafeSize208 first, in UnsafeSize208 second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator <(in UnsafeSize208 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <(in UnsafeSize208 first, in UnsafeSize208 second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <=(in UnsafeSize208 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) <= 0;
		}

		public static Boolean operator <=(in UnsafeSize208 first, in UnsafeSize208 second)
		{
			return first.CompareTo(second) <= 0;
		}

		private const String Data = UnsafeSize.Data;
		public const Int32 Size = 208;
		private fixed Byte Internal[Size];

		public readonly Type Type
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return typeof(UnsafeSize208);
			}
		}

		ref UnsafeSize208 IUnsafeSize<UnsafeSize208>.This
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return ref this;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size;
			}
		}

		readonly Span<Byte> IUnsafeSize<UnsafeSize208>.Full
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this;
			}
		}

		public readonly Boolean IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size <= 0 || AsReadOnlySpan().All(default(Byte));
			}
		}

		public UnsafeSize208(ReadOnlySpan<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}.");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, value.Length);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal UnsafeSize208(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal readonly void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue(Data, AsReadOnlySpan().ToArray(), typeof(Byte[]));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			GetObjectData(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ref UnsafeSize208 Deserialize(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}

			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize208 IUnsafeSize<UnsafeSize208>.Deserialize(SerializationInfo info, StreamingContext context)
		{
			return ref Deserialize(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Support<T>() where T : struct
		{
			return Unsafe.SizeOf<T>() is > 0 and <= Size;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ref T As<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<T> AsReadOnlySpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<Byte> AsSpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<T> AsSpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new Span<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize208 IUnsafeSize<UnsafeSize208>.SetFull()
		{
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize208 Fill(Byte value)
		{
			AsSpan().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize208 Fill<T>(T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize208 Fill<T>(in T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize208 Clear()
		{
			return ref Reset();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize208 Reset()
		{
			return ref Fill(default(Byte));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(Span<Byte> destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(ref UnsafeSize208 destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(Span<Byte> destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(ref UnsafeSize208 destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Byte[] ToArray()
		{
			return AsReadOnlySpan().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly Byte[] IUnsafeSize<UnsafeSize208>.ToFullArray()
		{
			return ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly T[] ToArray<T>() where T : struct
		{
			return AsReadOnlySpan<T>().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly T[] IUnsafeSize<UnsafeSize208>.ToFullArray<T>()
		{
			return ToArray<T>();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref *pointer;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(ReadOnlySpan<Byte> other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(UnsafeSize208 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(in UnsafeSize208 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly Int32 GetHashCode()
		{
			HashCode hash = new HashCode();
			hash.AddBytes(this);
			return hash.ToHashCode();
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other switch
			{
				null => IsEmpty,
				UnsafeSize208 value => Equals(value),
				Memory<Byte> value => Equals(value.Span),
				ReadOnlyMemory<Byte> value => Equals(value.Span),
				_ => false
			};
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(ReadOnlySpan<Byte> other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(UnsafeSize208 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(in UnsafeSize208 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly String ToString()
		{
			return Convert.ToHexString(this);
		}
	}

	[Serializable]
	public unsafe struct UnsafeSize224 : IUnsafeSize<UnsafeSize224>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize224 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize224 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		public static Boolean operator ==(in UnsafeSize224 first, ReadOnlySpan<Byte> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator ==(in UnsafeSize224 first, in UnsafeSize224 second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(in UnsafeSize224 first, ReadOnlySpan<Byte> second)
		{
			return !(first == second);
		}

		public static Boolean operator !=(in UnsafeSize224 first, in UnsafeSize224 second)
		{
			return !(first == second);
		}

		public static Boolean operator >(in UnsafeSize224 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >(in UnsafeSize224 first, in UnsafeSize224 second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >=(in UnsafeSize224 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator >=(in UnsafeSize224 first, in UnsafeSize224 second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator <(in UnsafeSize224 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <(in UnsafeSize224 first, in UnsafeSize224 second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <=(in UnsafeSize224 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) <= 0;
		}

		public static Boolean operator <=(in UnsafeSize224 first, in UnsafeSize224 second)
		{
			return first.CompareTo(second) <= 0;
		}

		private const String Data = UnsafeSize.Data;
		public const Int32 Size = 224;
		private fixed Byte Internal[Size];

		public readonly Type Type
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return typeof(UnsafeSize224);
			}
		}

		ref UnsafeSize224 IUnsafeSize<UnsafeSize224>.This
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return ref this;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size;
			}
		}

		readonly Span<Byte> IUnsafeSize<UnsafeSize224>.Full
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this;
			}
		}

		public readonly Boolean IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size <= 0 || AsReadOnlySpan().All(default(Byte));
			}
		}

		public UnsafeSize224(ReadOnlySpan<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}.");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, value.Length);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal UnsafeSize224(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal readonly void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue(Data, AsReadOnlySpan().ToArray(), typeof(Byte[]));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			GetObjectData(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ref UnsafeSize224 Deserialize(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}

			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize224 IUnsafeSize<UnsafeSize224>.Deserialize(SerializationInfo info, StreamingContext context)
		{
			return ref Deserialize(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Support<T>() where T : struct
		{
			return Unsafe.SizeOf<T>() is > 0 and <= Size;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ref T As<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<T> AsReadOnlySpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<Byte> AsSpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<T> AsSpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new Span<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize224 IUnsafeSize<UnsafeSize224>.SetFull()
		{
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize224 Fill(Byte value)
		{
			AsSpan().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize224 Fill<T>(T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize224 Fill<T>(in T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize224 Clear()
		{
			return ref Reset();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize224 Reset()
		{
			return ref Fill(default(Byte));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(Span<Byte> destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(ref UnsafeSize224 destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(Span<Byte> destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(ref UnsafeSize224 destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Byte[] ToArray()
		{
			return AsReadOnlySpan().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly Byte[] IUnsafeSize<UnsafeSize224>.ToFullArray()
		{
			return ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly T[] ToArray<T>() where T : struct
		{
			return AsReadOnlySpan<T>().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly T[] IUnsafeSize<UnsafeSize224>.ToFullArray<T>()
		{
			return ToArray<T>();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref *pointer;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(ReadOnlySpan<Byte> other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(UnsafeSize224 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(in UnsafeSize224 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly Int32 GetHashCode()
		{
			HashCode hash = new HashCode();
			hash.AddBytes(this);
			return hash.ToHashCode();
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other switch
			{
				null => IsEmpty,
				UnsafeSize224 value => Equals(value),
				Memory<Byte> value => Equals(value.Span),
				ReadOnlyMemory<Byte> value => Equals(value.Span),
				_ => false
			};
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(ReadOnlySpan<Byte> other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(UnsafeSize224 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(in UnsafeSize224 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly String ToString()
		{
			return Convert.ToHexString(this);
		}
	}

	[Serializable]
	public unsafe struct UnsafeSize240 : IUnsafeSize<UnsafeSize240>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize240 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize240 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		public static Boolean operator ==(in UnsafeSize240 first, ReadOnlySpan<Byte> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator ==(in UnsafeSize240 first, in UnsafeSize240 second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(in UnsafeSize240 first, ReadOnlySpan<Byte> second)
		{
			return !(first == second);
		}

		public static Boolean operator !=(in UnsafeSize240 first, in UnsafeSize240 second)
		{
			return !(first == second);
		}

		public static Boolean operator >(in UnsafeSize240 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >(in UnsafeSize240 first, in UnsafeSize240 second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >=(in UnsafeSize240 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator >=(in UnsafeSize240 first, in UnsafeSize240 second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator <(in UnsafeSize240 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <(in UnsafeSize240 first, in UnsafeSize240 second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <=(in UnsafeSize240 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) <= 0;
		}

		public static Boolean operator <=(in UnsafeSize240 first, in UnsafeSize240 second)
		{
			return first.CompareTo(second) <= 0;
		}

		private const String Data = UnsafeSize.Data;
		public const Int32 Size = 240;
		private fixed Byte Internal[Size];

		public readonly Type Type
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return typeof(UnsafeSize240);
			}
		}

		ref UnsafeSize240 IUnsafeSize<UnsafeSize240>.This
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return ref this;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size;
			}
		}

		readonly Span<Byte> IUnsafeSize<UnsafeSize240>.Full
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this;
			}
		}

		public readonly Boolean IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size <= 0 || AsReadOnlySpan().All(default(Byte));
			}
		}

		public UnsafeSize240(ReadOnlySpan<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}.");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, value.Length);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal UnsafeSize240(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal readonly void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue(Data, AsReadOnlySpan().ToArray(), typeof(Byte[]));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			GetObjectData(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ref UnsafeSize240 Deserialize(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}

			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize240 IUnsafeSize<UnsafeSize240>.Deserialize(SerializationInfo info, StreamingContext context)
		{
			return ref Deserialize(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Support<T>() where T : struct
		{
			return Unsafe.SizeOf<T>() is > 0 and <= Size;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ref T As<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<T> AsReadOnlySpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<Byte> AsSpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<T> AsSpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new Span<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize240 IUnsafeSize<UnsafeSize240>.SetFull()
		{
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize240 Fill(Byte value)
		{
			AsSpan().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize240 Fill<T>(T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize240 Fill<T>(in T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize240 Clear()
		{
			return ref Reset();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize240 Reset()
		{
			return ref Fill(default(Byte));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(Span<Byte> destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(ref UnsafeSize240 destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(Span<Byte> destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(ref UnsafeSize240 destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Byte[] ToArray()
		{
			return AsReadOnlySpan().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly Byte[] IUnsafeSize<UnsafeSize240>.ToFullArray()
		{
			return ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly T[] ToArray<T>() where T : struct
		{
			return AsReadOnlySpan<T>().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly T[] IUnsafeSize<UnsafeSize240>.ToFullArray<T>()
		{
			return ToArray<T>();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref *pointer;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(ReadOnlySpan<Byte> other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(UnsafeSize240 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(in UnsafeSize240 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly Int32 GetHashCode()
		{
			HashCode hash = new HashCode();
			hash.AddBytes(this);
			return hash.ToHashCode();
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other switch
			{
				null => IsEmpty,
				UnsafeSize240 value => Equals(value),
				Memory<Byte> value => Equals(value.Span),
				ReadOnlyMemory<Byte> value => Equals(value.Span),
				_ => false
			};
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(ReadOnlySpan<Byte> other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(UnsafeSize240 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(in UnsafeSize240 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly String ToString()
		{
			return Convert.ToHexString(this);
		}
	}

	[Serializable]
	public unsafe struct UnsafeSize256 : IUnsafeSize<UnsafeSize256>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize256 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize256 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		public static Boolean operator ==(in UnsafeSize256 first, ReadOnlySpan<Byte> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator ==(in UnsafeSize256 first, in UnsafeSize256 second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(in UnsafeSize256 first, ReadOnlySpan<Byte> second)
		{
			return !(first == second);
		}

		public static Boolean operator !=(in UnsafeSize256 first, in UnsafeSize256 second)
		{
			return !(first == second);
		}

		public static Boolean operator >(in UnsafeSize256 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >(in UnsafeSize256 first, in UnsafeSize256 second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >=(in UnsafeSize256 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator >=(in UnsafeSize256 first, in UnsafeSize256 second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator <(in UnsafeSize256 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <(in UnsafeSize256 first, in UnsafeSize256 second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <=(in UnsafeSize256 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) <= 0;
		}

		public static Boolean operator <=(in UnsafeSize256 first, in UnsafeSize256 second)
		{
			return first.CompareTo(second) <= 0;
		}

		private const String Data = UnsafeSize.Data;
		public const Int32 Size = 256;
		private fixed Byte Internal[Size];

		public readonly Type Type
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return typeof(UnsafeSize256);
			}
		}

		ref UnsafeSize256 IUnsafeSize<UnsafeSize256>.This
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return ref this;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size;
			}
		}

		readonly Span<Byte> IUnsafeSize<UnsafeSize256>.Full
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this;
			}
		}

		public readonly Boolean IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size <= 0 || AsReadOnlySpan().All(default(Byte));
			}
		}

		public UnsafeSize256(ReadOnlySpan<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}.");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, value.Length);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal UnsafeSize256(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal readonly void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue(Data, AsReadOnlySpan().ToArray(), typeof(Byte[]));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			GetObjectData(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ref UnsafeSize256 Deserialize(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}

			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize256 IUnsafeSize<UnsafeSize256>.Deserialize(SerializationInfo info, StreamingContext context)
		{
			return ref Deserialize(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Support<T>() where T : struct
		{
			return Unsafe.SizeOf<T>() is > 0 and <= Size;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ref T As<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<T> AsReadOnlySpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<Byte> AsSpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<T> AsSpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new Span<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize256 IUnsafeSize<UnsafeSize256>.SetFull()
		{
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize256 Fill(Byte value)
		{
			AsSpan().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize256 Fill<T>(T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize256 Fill<T>(in T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize256 Clear()
		{
			return ref Reset();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize256 Reset()
		{
			return ref Fill(default(Byte));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(Span<Byte> destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(ref UnsafeSize256 destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(Span<Byte> destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(ref UnsafeSize256 destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Byte[] ToArray()
		{
			return AsReadOnlySpan().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly Byte[] IUnsafeSize<UnsafeSize256>.ToFullArray()
		{
			return ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly T[] ToArray<T>() where T : struct
		{
			return AsReadOnlySpan<T>().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly T[] IUnsafeSize<UnsafeSize256>.ToFullArray<T>()
		{
			return ToArray<T>();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref *pointer;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(ReadOnlySpan<Byte> other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(UnsafeSize256 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(in UnsafeSize256 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly Int32 GetHashCode()
		{
			HashCode hash = new HashCode();
			hash.AddBytes(this);
			return hash.ToHashCode();
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other switch
			{
				null => IsEmpty,
				UnsafeSize256 value => Equals(value),
				Memory<Byte> value => Equals(value.Span),
				ReadOnlyMemory<Byte> value => Equals(value.Span),
				_ => false
			};
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(ReadOnlySpan<Byte> other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(UnsafeSize256 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(in UnsafeSize256 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly String ToString()
		{
			return Convert.ToHexString(this);
		}
	}

	[Serializable]
	public unsafe struct UnsafeSize288 : IUnsafeSize<UnsafeSize288>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize288 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize288 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		public static Boolean operator ==(in UnsafeSize288 first, ReadOnlySpan<Byte> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator ==(in UnsafeSize288 first, in UnsafeSize288 second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(in UnsafeSize288 first, ReadOnlySpan<Byte> second)
		{
			return !(first == second);
		}

		public static Boolean operator !=(in UnsafeSize288 first, in UnsafeSize288 second)
		{
			return !(first == second);
		}

		public static Boolean operator >(in UnsafeSize288 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >(in UnsafeSize288 first, in UnsafeSize288 second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >=(in UnsafeSize288 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator >=(in UnsafeSize288 first, in UnsafeSize288 second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator <(in UnsafeSize288 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <(in UnsafeSize288 first, in UnsafeSize288 second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <=(in UnsafeSize288 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) <= 0;
		}

		public static Boolean operator <=(in UnsafeSize288 first, in UnsafeSize288 second)
		{
			return first.CompareTo(second) <= 0;
		}

		private const String Data = UnsafeSize.Data;
		public const Int32 Size = 288;
		private fixed Byte Internal[Size];

		public readonly Type Type
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return typeof(UnsafeSize288);
			}
		}

		ref UnsafeSize288 IUnsafeSize<UnsafeSize288>.This
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return ref this;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size;
			}
		}

		readonly Span<Byte> IUnsafeSize<UnsafeSize288>.Full
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this;
			}
		}

		public readonly Boolean IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size <= 0 || AsReadOnlySpan().All(default(Byte));
			}
		}

		public UnsafeSize288(ReadOnlySpan<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}.");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, value.Length);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal UnsafeSize288(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal readonly void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue(Data, AsReadOnlySpan().ToArray(), typeof(Byte[]));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			GetObjectData(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ref UnsafeSize288 Deserialize(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}

			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize288 IUnsafeSize<UnsafeSize288>.Deserialize(SerializationInfo info, StreamingContext context)
		{
			return ref Deserialize(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Support<T>() where T : struct
		{
			return Unsafe.SizeOf<T>() is > 0 and <= Size;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ref T As<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<T> AsReadOnlySpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<Byte> AsSpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<T> AsSpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new Span<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize288 IUnsafeSize<UnsafeSize288>.SetFull()
		{
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize288 Fill(Byte value)
		{
			AsSpan().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize288 Fill<T>(T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize288 Fill<T>(in T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize288 Clear()
		{
			return ref Reset();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize288 Reset()
		{
			return ref Fill(default(Byte));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(Span<Byte> destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(ref UnsafeSize288 destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(Span<Byte> destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(ref UnsafeSize288 destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Byte[] ToArray()
		{
			return AsReadOnlySpan().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly Byte[] IUnsafeSize<UnsafeSize288>.ToFullArray()
		{
			return ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly T[] ToArray<T>() where T : struct
		{
			return AsReadOnlySpan<T>().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly T[] IUnsafeSize<UnsafeSize288>.ToFullArray<T>()
		{
			return ToArray<T>();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref *pointer;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(ReadOnlySpan<Byte> other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(UnsafeSize288 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(in UnsafeSize288 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly Int32 GetHashCode()
		{
			HashCode hash = new HashCode();
			hash.AddBytes(this);
			return hash.ToHashCode();
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other switch
			{
				null => IsEmpty,
				UnsafeSize288 value => Equals(value),
				Memory<Byte> value => Equals(value.Span),
				ReadOnlyMemory<Byte> value => Equals(value.Span),
				_ => false
			};
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(ReadOnlySpan<Byte> other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(UnsafeSize288 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(in UnsafeSize288 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly String ToString()
		{
			return Convert.ToHexString(this);
		}
	}

	[Serializable]
	public unsafe struct UnsafeSize320 : IUnsafeSize<UnsafeSize320>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize320 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize320 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		public static Boolean operator ==(in UnsafeSize320 first, ReadOnlySpan<Byte> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator ==(in UnsafeSize320 first, in UnsafeSize320 second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(in UnsafeSize320 first, ReadOnlySpan<Byte> second)
		{
			return !(first == second);
		}

		public static Boolean operator !=(in UnsafeSize320 first, in UnsafeSize320 second)
		{
			return !(first == second);
		}

		public static Boolean operator >(in UnsafeSize320 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >(in UnsafeSize320 first, in UnsafeSize320 second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >=(in UnsafeSize320 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator >=(in UnsafeSize320 first, in UnsafeSize320 second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator <(in UnsafeSize320 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <(in UnsafeSize320 first, in UnsafeSize320 second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <=(in UnsafeSize320 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) <= 0;
		}

		public static Boolean operator <=(in UnsafeSize320 first, in UnsafeSize320 second)
		{
			return first.CompareTo(second) <= 0;
		}

		private const String Data = UnsafeSize.Data;
		public const Int32 Size = 320;
		private fixed Byte Internal[Size];

		public readonly Type Type
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return typeof(UnsafeSize320);
			}
		}

		ref UnsafeSize320 IUnsafeSize<UnsafeSize320>.This
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return ref this;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size;
			}
		}

		readonly Span<Byte> IUnsafeSize<UnsafeSize320>.Full
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this;
			}
		}

		public readonly Boolean IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size <= 0 || AsReadOnlySpan().All(default(Byte));
			}
		}

		public UnsafeSize320(ReadOnlySpan<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}.");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, value.Length);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal UnsafeSize320(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal readonly void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue(Data, AsReadOnlySpan().ToArray(), typeof(Byte[]));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			GetObjectData(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ref UnsafeSize320 Deserialize(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}

			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize320 IUnsafeSize<UnsafeSize320>.Deserialize(SerializationInfo info, StreamingContext context)
		{
			return ref Deserialize(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Support<T>() where T : struct
		{
			return Unsafe.SizeOf<T>() is > 0 and <= Size;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ref T As<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<T> AsReadOnlySpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<Byte> AsSpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<T> AsSpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new Span<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize320 IUnsafeSize<UnsafeSize320>.SetFull()
		{
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize320 Fill(Byte value)
		{
			AsSpan().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize320 Fill<T>(T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize320 Fill<T>(in T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize320 Clear()
		{
			return ref Reset();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize320 Reset()
		{
			return ref Fill(default(Byte));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(Span<Byte> destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(ref UnsafeSize320 destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(Span<Byte> destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(ref UnsafeSize320 destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Byte[] ToArray()
		{
			return AsReadOnlySpan().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly Byte[] IUnsafeSize<UnsafeSize320>.ToFullArray()
		{
			return ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly T[] ToArray<T>() where T : struct
		{
			return AsReadOnlySpan<T>().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly T[] IUnsafeSize<UnsafeSize320>.ToFullArray<T>()
		{
			return ToArray<T>();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref *pointer;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(ReadOnlySpan<Byte> other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(UnsafeSize320 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(in UnsafeSize320 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly Int32 GetHashCode()
		{
			HashCode hash = new HashCode();
			hash.AddBytes(this);
			return hash.ToHashCode();
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other switch
			{
				null => IsEmpty,
				UnsafeSize320 value => Equals(value),
				Memory<Byte> value => Equals(value.Span),
				ReadOnlyMemory<Byte> value => Equals(value.Span),
				_ => false
			};
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(ReadOnlySpan<Byte> other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(UnsafeSize320 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(in UnsafeSize320 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly String ToString()
		{
			return Convert.ToHexString(this);
		}
	}

	[Serializable]
	public unsafe struct UnsafeSize352 : IUnsafeSize<UnsafeSize352>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize352 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize352 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		public static Boolean operator ==(in UnsafeSize352 first, ReadOnlySpan<Byte> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator ==(in UnsafeSize352 first, in UnsafeSize352 second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(in UnsafeSize352 first, ReadOnlySpan<Byte> second)
		{
			return !(first == second);
		}

		public static Boolean operator !=(in UnsafeSize352 first, in UnsafeSize352 second)
		{
			return !(first == second);
		}

		public static Boolean operator >(in UnsafeSize352 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >(in UnsafeSize352 first, in UnsafeSize352 second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >=(in UnsafeSize352 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator >=(in UnsafeSize352 first, in UnsafeSize352 second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator <(in UnsafeSize352 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <(in UnsafeSize352 first, in UnsafeSize352 second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <=(in UnsafeSize352 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) <= 0;
		}

		public static Boolean operator <=(in UnsafeSize352 first, in UnsafeSize352 second)
		{
			return first.CompareTo(second) <= 0;
		}

		private const String Data = UnsafeSize.Data;
		public const Int32 Size = 352;
		private fixed Byte Internal[Size];

		public readonly Type Type
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return typeof(UnsafeSize352);
			}
		}

		ref UnsafeSize352 IUnsafeSize<UnsafeSize352>.This
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return ref this;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size;
			}
		}

		readonly Span<Byte> IUnsafeSize<UnsafeSize352>.Full
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this;
			}
		}

		public readonly Boolean IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size <= 0 || AsReadOnlySpan().All(default(Byte));
			}
		}

		public UnsafeSize352(ReadOnlySpan<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}.");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, value.Length);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal UnsafeSize352(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal readonly void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue(Data, AsReadOnlySpan().ToArray(), typeof(Byte[]));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			GetObjectData(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ref UnsafeSize352 Deserialize(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}

			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize352 IUnsafeSize<UnsafeSize352>.Deserialize(SerializationInfo info, StreamingContext context)
		{
			return ref Deserialize(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Support<T>() where T : struct
		{
			return Unsafe.SizeOf<T>() is > 0 and <= Size;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ref T As<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<T> AsReadOnlySpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<Byte> AsSpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<T> AsSpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new Span<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize352 IUnsafeSize<UnsafeSize352>.SetFull()
		{
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize352 Fill(Byte value)
		{
			AsSpan().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize352 Fill<T>(T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize352 Fill<T>(in T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize352 Clear()
		{
			return ref Reset();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize352 Reset()
		{
			return ref Fill(default(Byte));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(Span<Byte> destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(ref UnsafeSize352 destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(Span<Byte> destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(ref UnsafeSize352 destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Byte[] ToArray()
		{
			return AsReadOnlySpan().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly Byte[] IUnsafeSize<UnsafeSize352>.ToFullArray()
		{
			return ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly T[] ToArray<T>() where T : struct
		{
			return AsReadOnlySpan<T>().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly T[] IUnsafeSize<UnsafeSize352>.ToFullArray<T>()
		{
			return ToArray<T>();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref *pointer;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(ReadOnlySpan<Byte> other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(UnsafeSize352 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(in UnsafeSize352 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly Int32 GetHashCode()
		{
			HashCode hash = new HashCode();
			hash.AddBytes(this);
			return hash.ToHashCode();
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other switch
			{
				null => IsEmpty,
				UnsafeSize352 value => Equals(value),
				Memory<Byte> value => Equals(value.Span),
				ReadOnlyMemory<Byte> value => Equals(value.Span),
				_ => false
			};
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(ReadOnlySpan<Byte> other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(UnsafeSize352 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(in UnsafeSize352 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly String ToString()
		{
			return Convert.ToHexString(this);
		}
	}

	[Serializable]
	public unsafe struct UnsafeSize384 : IUnsafeSize<UnsafeSize384>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize384 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize384 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		public static Boolean operator ==(in UnsafeSize384 first, ReadOnlySpan<Byte> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator ==(in UnsafeSize384 first, in UnsafeSize384 second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(in UnsafeSize384 first, ReadOnlySpan<Byte> second)
		{
			return !(first == second);
		}

		public static Boolean operator !=(in UnsafeSize384 first, in UnsafeSize384 second)
		{
			return !(first == second);
		}

		public static Boolean operator >(in UnsafeSize384 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >(in UnsafeSize384 first, in UnsafeSize384 second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >=(in UnsafeSize384 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator >=(in UnsafeSize384 first, in UnsafeSize384 second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator <(in UnsafeSize384 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <(in UnsafeSize384 first, in UnsafeSize384 second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <=(in UnsafeSize384 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) <= 0;
		}

		public static Boolean operator <=(in UnsafeSize384 first, in UnsafeSize384 second)
		{
			return first.CompareTo(second) <= 0;
		}

		private const String Data = UnsafeSize.Data;
		public const Int32 Size = 384;
		private fixed Byte Internal[Size];

		public readonly Type Type
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return typeof(UnsafeSize384);
			}
		}

		ref UnsafeSize384 IUnsafeSize<UnsafeSize384>.This
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return ref this;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size;
			}
		}

		readonly Span<Byte> IUnsafeSize<UnsafeSize384>.Full
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this;
			}
		}

		public readonly Boolean IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size <= 0 || AsReadOnlySpan().All(default(Byte));
			}
		}

		public UnsafeSize384(ReadOnlySpan<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}.");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, value.Length);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal UnsafeSize384(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal readonly void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue(Data, AsReadOnlySpan().ToArray(), typeof(Byte[]));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			GetObjectData(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ref UnsafeSize384 Deserialize(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}

			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize384 IUnsafeSize<UnsafeSize384>.Deserialize(SerializationInfo info, StreamingContext context)
		{
			return ref Deserialize(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Support<T>() where T : struct
		{
			return Unsafe.SizeOf<T>() is > 0 and <= Size;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ref T As<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<T> AsReadOnlySpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<Byte> AsSpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<T> AsSpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new Span<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize384 IUnsafeSize<UnsafeSize384>.SetFull()
		{
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize384 Fill(Byte value)
		{
			AsSpan().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize384 Fill<T>(T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize384 Fill<T>(in T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize384 Clear()
		{
			return ref Reset();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize384 Reset()
		{
			return ref Fill(default(Byte));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(Span<Byte> destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(ref UnsafeSize384 destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(Span<Byte> destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(ref UnsafeSize384 destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Byte[] ToArray()
		{
			return AsReadOnlySpan().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly Byte[] IUnsafeSize<UnsafeSize384>.ToFullArray()
		{
			return ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly T[] ToArray<T>() where T : struct
		{
			return AsReadOnlySpan<T>().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly T[] IUnsafeSize<UnsafeSize384>.ToFullArray<T>()
		{
			return ToArray<T>();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref *pointer;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(ReadOnlySpan<Byte> other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(UnsafeSize384 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(in UnsafeSize384 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly Int32 GetHashCode()
		{
			HashCode hash = new HashCode();
			hash.AddBytes(this);
			return hash.ToHashCode();
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other switch
			{
				null => IsEmpty,
				UnsafeSize384 value => Equals(value),
				Memory<Byte> value => Equals(value.Span),
				ReadOnlyMemory<Byte> value => Equals(value.Span),
				_ => false
			};
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(ReadOnlySpan<Byte> other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(UnsafeSize384 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(in UnsafeSize384 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly String ToString()
		{
			return Convert.ToHexString(this);
		}
	}

	[Serializable]
	public unsafe struct UnsafeSize416 : IUnsafeSize<UnsafeSize416>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize416 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize416 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		public static Boolean operator ==(in UnsafeSize416 first, ReadOnlySpan<Byte> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator ==(in UnsafeSize416 first, in UnsafeSize416 second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(in UnsafeSize416 first, ReadOnlySpan<Byte> second)
		{
			return !(first == second);
		}

		public static Boolean operator !=(in UnsafeSize416 first, in UnsafeSize416 second)
		{
			return !(first == second);
		}

		public static Boolean operator >(in UnsafeSize416 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >(in UnsafeSize416 first, in UnsafeSize416 second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >=(in UnsafeSize416 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator >=(in UnsafeSize416 first, in UnsafeSize416 second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator <(in UnsafeSize416 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <(in UnsafeSize416 first, in UnsafeSize416 second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <=(in UnsafeSize416 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) <= 0;
		}

		public static Boolean operator <=(in UnsafeSize416 first, in UnsafeSize416 second)
		{
			return first.CompareTo(second) <= 0;
		}

		private const String Data = UnsafeSize.Data;
		public const Int32 Size = 416;
		private fixed Byte Internal[Size];

		public readonly Type Type
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return typeof(UnsafeSize416);
			}
		}

		ref UnsafeSize416 IUnsafeSize<UnsafeSize416>.This
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return ref this;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size;
			}
		}

		readonly Span<Byte> IUnsafeSize<UnsafeSize416>.Full
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this;
			}
		}

		public readonly Boolean IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size <= 0 || AsReadOnlySpan().All(default(Byte));
			}
		}

		public UnsafeSize416(ReadOnlySpan<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}.");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, value.Length);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal UnsafeSize416(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal readonly void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue(Data, AsReadOnlySpan().ToArray(), typeof(Byte[]));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			GetObjectData(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ref UnsafeSize416 Deserialize(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}

			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize416 IUnsafeSize<UnsafeSize416>.Deserialize(SerializationInfo info, StreamingContext context)
		{
			return ref Deserialize(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Support<T>() where T : struct
		{
			return Unsafe.SizeOf<T>() is > 0 and <= Size;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ref T As<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<T> AsReadOnlySpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<Byte> AsSpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<T> AsSpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new Span<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize416 IUnsafeSize<UnsafeSize416>.SetFull()
		{
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize416 Fill(Byte value)
		{
			AsSpan().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize416 Fill<T>(T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize416 Fill<T>(in T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize416 Clear()
		{
			return ref Reset();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize416 Reset()
		{
			return ref Fill(default(Byte));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(Span<Byte> destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(ref UnsafeSize416 destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(Span<Byte> destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(ref UnsafeSize416 destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Byte[] ToArray()
		{
			return AsReadOnlySpan().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly Byte[] IUnsafeSize<UnsafeSize416>.ToFullArray()
		{
			return ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly T[] ToArray<T>() where T : struct
		{
			return AsReadOnlySpan<T>().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly T[] IUnsafeSize<UnsafeSize416>.ToFullArray<T>()
		{
			return ToArray<T>();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref *pointer;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(ReadOnlySpan<Byte> other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(UnsafeSize416 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(in UnsafeSize416 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly Int32 GetHashCode()
		{
			HashCode hash = new HashCode();
			hash.AddBytes(this);
			return hash.ToHashCode();
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other switch
			{
				null => IsEmpty,
				UnsafeSize416 value => Equals(value),
				Memory<Byte> value => Equals(value.Span),
				ReadOnlyMemory<Byte> value => Equals(value.Span),
				_ => false
			};
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(ReadOnlySpan<Byte> other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(UnsafeSize416 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(in UnsafeSize416 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly String ToString()
		{
			return Convert.ToHexString(this);
		}
	}

	[Serializable]
	public unsafe struct UnsafeSize448 : IUnsafeSize<UnsafeSize448>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize448 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize448 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		public static Boolean operator ==(in UnsafeSize448 first, ReadOnlySpan<Byte> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator ==(in UnsafeSize448 first, in UnsafeSize448 second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(in UnsafeSize448 first, ReadOnlySpan<Byte> second)
		{
			return !(first == second);
		}

		public static Boolean operator !=(in UnsafeSize448 first, in UnsafeSize448 second)
		{
			return !(first == second);
		}

		public static Boolean operator >(in UnsafeSize448 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >(in UnsafeSize448 first, in UnsafeSize448 second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >=(in UnsafeSize448 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator >=(in UnsafeSize448 first, in UnsafeSize448 second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator <(in UnsafeSize448 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <(in UnsafeSize448 first, in UnsafeSize448 second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <=(in UnsafeSize448 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) <= 0;
		}

		public static Boolean operator <=(in UnsafeSize448 first, in UnsafeSize448 second)
		{
			return first.CompareTo(second) <= 0;
		}

		private const String Data = UnsafeSize.Data;
		public const Int32 Size = 448;
		private fixed Byte Internal[Size];

		public readonly Type Type
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return typeof(UnsafeSize448);
			}
		}

		ref UnsafeSize448 IUnsafeSize<UnsafeSize448>.This
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return ref this;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size;
			}
		}

		readonly Span<Byte> IUnsafeSize<UnsafeSize448>.Full
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this;
			}
		}

		public readonly Boolean IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size <= 0 || AsReadOnlySpan().All(default(Byte));
			}
		}

		public UnsafeSize448(ReadOnlySpan<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}.");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, value.Length);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal UnsafeSize448(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal readonly void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue(Data, AsReadOnlySpan().ToArray(), typeof(Byte[]));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			GetObjectData(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ref UnsafeSize448 Deserialize(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}

			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize448 IUnsafeSize<UnsafeSize448>.Deserialize(SerializationInfo info, StreamingContext context)
		{
			return ref Deserialize(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Support<T>() where T : struct
		{
			return Unsafe.SizeOf<T>() is > 0 and <= Size;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ref T As<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<T> AsReadOnlySpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<Byte> AsSpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<T> AsSpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new Span<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize448 IUnsafeSize<UnsafeSize448>.SetFull()
		{
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize448 Fill(Byte value)
		{
			AsSpan().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize448 Fill<T>(T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize448 Fill<T>(in T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize448 Clear()
		{
			return ref Reset();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize448 Reset()
		{
			return ref Fill(default(Byte));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(Span<Byte> destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(ref UnsafeSize448 destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(Span<Byte> destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(ref UnsafeSize448 destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Byte[] ToArray()
		{
			return AsReadOnlySpan().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly Byte[] IUnsafeSize<UnsafeSize448>.ToFullArray()
		{
			return ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly T[] ToArray<T>() where T : struct
		{
			return AsReadOnlySpan<T>().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly T[] IUnsafeSize<UnsafeSize448>.ToFullArray<T>()
		{
			return ToArray<T>();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref *pointer;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(ReadOnlySpan<Byte> other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(UnsafeSize448 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(in UnsafeSize448 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly Int32 GetHashCode()
		{
			HashCode hash = new HashCode();
			hash.AddBytes(this);
			return hash.ToHashCode();
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other switch
			{
				null => IsEmpty,
				UnsafeSize448 value => Equals(value),
				Memory<Byte> value => Equals(value.Span),
				ReadOnlyMemory<Byte> value => Equals(value.Span),
				_ => false
			};
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(ReadOnlySpan<Byte> other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(UnsafeSize448 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(in UnsafeSize448 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly String ToString()
		{
			return Convert.ToHexString(this);
		}
	}

	[Serializable]
	public unsafe struct UnsafeSize480 : IUnsafeSize<UnsafeSize480>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize480 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize480 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		public static Boolean operator ==(in UnsafeSize480 first, ReadOnlySpan<Byte> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator ==(in UnsafeSize480 first, in UnsafeSize480 second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(in UnsafeSize480 first, ReadOnlySpan<Byte> second)
		{
			return !(first == second);
		}

		public static Boolean operator !=(in UnsafeSize480 first, in UnsafeSize480 second)
		{
			return !(first == second);
		}

		public static Boolean operator >(in UnsafeSize480 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >(in UnsafeSize480 first, in UnsafeSize480 second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >=(in UnsafeSize480 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator >=(in UnsafeSize480 first, in UnsafeSize480 second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator <(in UnsafeSize480 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <(in UnsafeSize480 first, in UnsafeSize480 second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <=(in UnsafeSize480 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) <= 0;
		}

		public static Boolean operator <=(in UnsafeSize480 first, in UnsafeSize480 second)
		{
			return first.CompareTo(second) <= 0;
		}

		private const String Data = UnsafeSize.Data;
		public const Int32 Size = 480;
		private fixed Byte Internal[Size];

		public readonly Type Type
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return typeof(UnsafeSize480);
			}
		}

		ref UnsafeSize480 IUnsafeSize<UnsafeSize480>.This
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return ref this;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size;
			}
		}

		readonly Span<Byte> IUnsafeSize<UnsafeSize480>.Full
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this;
			}
		}

		public readonly Boolean IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size <= 0 || AsReadOnlySpan().All(default(Byte));
			}
		}

		public UnsafeSize480(ReadOnlySpan<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}.");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, value.Length);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal UnsafeSize480(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal readonly void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue(Data, AsReadOnlySpan().ToArray(), typeof(Byte[]));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			GetObjectData(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ref UnsafeSize480 Deserialize(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}

			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize480 IUnsafeSize<UnsafeSize480>.Deserialize(SerializationInfo info, StreamingContext context)
		{
			return ref Deserialize(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Support<T>() where T : struct
		{
			return Unsafe.SizeOf<T>() is > 0 and <= Size;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ref T As<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<T> AsReadOnlySpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<Byte> AsSpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<T> AsSpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new Span<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize480 IUnsafeSize<UnsafeSize480>.SetFull()
		{
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize480 Fill(Byte value)
		{
			AsSpan().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize480 Fill<T>(T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize480 Fill<T>(in T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize480 Clear()
		{
			return ref Reset();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize480 Reset()
		{
			return ref Fill(default(Byte));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(Span<Byte> destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(ref UnsafeSize480 destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(Span<Byte> destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(ref UnsafeSize480 destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Byte[] ToArray()
		{
			return AsReadOnlySpan().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly Byte[] IUnsafeSize<UnsafeSize480>.ToFullArray()
		{
			return ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly T[] ToArray<T>() where T : struct
		{
			return AsReadOnlySpan<T>().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly T[] IUnsafeSize<UnsafeSize480>.ToFullArray<T>()
		{
			return ToArray<T>();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref *pointer;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(ReadOnlySpan<Byte> other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(UnsafeSize480 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(in UnsafeSize480 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly Int32 GetHashCode()
		{
			HashCode hash = new HashCode();
			hash.AddBytes(this);
			return hash.ToHashCode();
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other switch
			{
				null => IsEmpty,
				UnsafeSize480 value => Equals(value),
				Memory<Byte> value => Equals(value.Span),
				ReadOnlyMemory<Byte> value => Equals(value.Span),
				_ => false
			};
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(ReadOnlySpan<Byte> other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(UnsafeSize480 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(in UnsafeSize480 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly String ToString()
		{
			return Convert.ToHexString(this);
		}
	}

	[Serializable]
	public unsafe struct UnsafeSize512 : IUnsafeSize<UnsafeSize512>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize512 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize512 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		public static Boolean operator ==(in UnsafeSize512 first, ReadOnlySpan<Byte> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator ==(in UnsafeSize512 first, in UnsafeSize512 second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(in UnsafeSize512 first, ReadOnlySpan<Byte> second)
		{
			return !(first == second);
		}

		public static Boolean operator !=(in UnsafeSize512 first, in UnsafeSize512 second)
		{
			return !(first == second);
		}

		public static Boolean operator >(in UnsafeSize512 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >(in UnsafeSize512 first, in UnsafeSize512 second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >=(in UnsafeSize512 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator >=(in UnsafeSize512 first, in UnsafeSize512 second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator <(in UnsafeSize512 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <(in UnsafeSize512 first, in UnsafeSize512 second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <=(in UnsafeSize512 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) <= 0;
		}

		public static Boolean operator <=(in UnsafeSize512 first, in UnsafeSize512 second)
		{
			return first.CompareTo(second) <= 0;
		}

		private const String Data = UnsafeSize.Data;
		public const Int32 Size = 512;
		private fixed Byte Internal[Size];

		public readonly Type Type
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return typeof(UnsafeSize512);
			}
		}

		ref UnsafeSize512 IUnsafeSize<UnsafeSize512>.This
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return ref this;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size;
			}
		}

		readonly Span<Byte> IUnsafeSize<UnsafeSize512>.Full
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this;
			}
		}

		public readonly Boolean IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size <= 0 || AsReadOnlySpan().All(default(Byte));
			}
		}

		public UnsafeSize512(ReadOnlySpan<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}.");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, value.Length);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal UnsafeSize512(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal readonly void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue(Data, AsReadOnlySpan().ToArray(), typeof(Byte[]));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			GetObjectData(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ref UnsafeSize512 Deserialize(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}

			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize512 IUnsafeSize<UnsafeSize512>.Deserialize(SerializationInfo info, StreamingContext context)
		{
			return ref Deserialize(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Support<T>() where T : struct
		{
			return Unsafe.SizeOf<T>() is > 0 and <= Size;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ref T As<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<T> AsReadOnlySpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<Byte> AsSpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<T> AsSpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new Span<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize512 IUnsafeSize<UnsafeSize512>.SetFull()
		{
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize512 Fill(Byte value)
		{
			AsSpan().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize512 Fill<T>(T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize512 Fill<T>(in T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize512 Clear()
		{
			return ref Reset();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize512 Reset()
		{
			return ref Fill(default(Byte));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(Span<Byte> destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(ref UnsafeSize512 destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(Span<Byte> destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(ref UnsafeSize512 destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Byte[] ToArray()
		{
			return AsReadOnlySpan().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly Byte[] IUnsafeSize<UnsafeSize512>.ToFullArray()
		{
			return ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly T[] ToArray<T>() where T : struct
		{
			return AsReadOnlySpan<T>().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly T[] IUnsafeSize<UnsafeSize512>.ToFullArray<T>()
		{
			return ToArray<T>();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref *pointer;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(ReadOnlySpan<Byte> other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(UnsafeSize512 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(in UnsafeSize512 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly Int32 GetHashCode()
		{
			HashCode hash = new HashCode();
			hash.AddBytes(this);
			return hash.ToHashCode();
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other switch
			{
				null => IsEmpty,
				UnsafeSize512 value => Equals(value),
				Memory<Byte> value => Equals(value.Span),
				ReadOnlyMemory<Byte> value => Equals(value.Span),
				_ => false
			};
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(ReadOnlySpan<Byte> other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(UnsafeSize512 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(in UnsafeSize512 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly String ToString()
		{
			return Convert.ToHexString(this);
		}
	}

	[Serializable]
	public unsafe struct UnsafeSize576 : IUnsafeSize<UnsafeSize576>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize576 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize576 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		public static Boolean operator ==(in UnsafeSize576 first, ReadOnlySpan<Byte> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator ==(in UnsafeSize576 first, in UnsafeSize576 second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(in UnsafeSize576 first, ReadOnlySpan<Byte> second)
		{
			return !(first == second);
		}

		public static Boolean operator !=(in UnsafeSize576 first, in UnsafeSize576 second)
		{
			return !(first == second);
		}

		public static Boolean operator >(in UnsafeSize576 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >(in UnsafeSize576 first, in UnsafeSize576 second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >=(in UnsafeSize576 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator >=(in UnsafeSize576 first, in UnsafeSize576 second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator <(in UnsafeSize576 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <(in UnsafeSize576 first, in UnsafeSize576 second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <=(in UnsafeSize576 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) <= 0;
		}

		public static Boolean operator <=(in UnsafeSize576 first, in UnsafeSize576 second)
		{
			return first.CompareTo(second) <= 0;
		}

		private const String Data = UnsafeSize.Data;
		public const Int32 Size = 576;
		private fixed Byte Internal[Size];

		public readonly Type Type
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return typeof(UnsafeSize576);
			}
		}

		ref UnsafeSize576 IUnsafeSize<UnsafeSize576>.This
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return ref this;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size;
			}
		}

		readonly Span<Byte> IUnsafeSize<UnsafeSize576>.Full
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this;
			}
		}

		public readonly Boolean IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size <= 0 || AsReadOnlySpan().All(default(Byte));
			}
		}

		public UnsafeSize576(ReadOnlySpan<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}.");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, value.Length);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal UnsafeSize576(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal readonly void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue(Data, AsReadOnlySpan().ToArray(), typeof(Byte[]));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			GetObjectData(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ref UnsafeSize576 Deserialize(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}

			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize576 IUnsafeSize<UnsafeSize576>.Deserialize(SerializationInfo info, StreamingContext context)
		{
			return ref Deserialize(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Support<T>() where T : struct
		{
			return Unsafe.SizeOf<T>() is > 0 and <= Size;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ref T As<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<T> AsReadOnlySpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<Byte> AsSpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<T> AsSpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new Span<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize576 IUnsafeSize<UnsafeSize576>.SetFull()
		{
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize576 Fill(Byte value)
		{
			AsSpan().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize576 Fill<T>(T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize576 Fill<T>(in T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize576 Clear()
		{
			return ref Reset();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize576 Reset()
		{
			return ref Fill(default(Byte));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(Span<Byte> destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(ref UnsafeSize576 destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(Span<Byte> destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(ref UnsafeSize576 destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Byte[] ToArray()
		{
			return AsReadOnlySpan().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly Byte[] IUnsafeSize<UnsafeSize576>.ToFullArray()
		{
			return ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly T[] ToArray<T>() where T : struct
		{
			return AsReadOnlySpan<T>().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly T[] IUnsafeSize<UnsafeSize576>.ToFullArray<T>()
		{
			return ToArray<T>();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref *pointer;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(ReadOnlySpan<Byte> other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(UnsafeSize576 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(in UnsafeSize576 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly Int32 GetHashCode()
		{
			HashCode hash = new HashCode();
			hash.AddBytes(this);
			return hash.ToHashCode();
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other switch
			{
				null => IsEmpty,
				UnsafeSize576 value => Equals(value),
				Memory<Byte> value => Equals(value.Span),
				ReadOnlyMemory<Byte> value => Equals(value.Span),
				_ => false
			};
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(ReadOnlySpan<Byte> other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(UnsafeSize576 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(in UnsafeSize576 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly String ToString()
		{
			return Convert.ToHexString(this);
		}
	}

	[Serializable]
	public unsafe struct UnsafeSize640 : IUnsafeSize<UnsafeSize640>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize640 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize640 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		public static Boolean operator ==(in UnsafeSize640 first, ReadOnlySpan<Byte> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator ==(in UnsafeSize640 first, in UnsafeSize640 second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(in UnsafeSize640 first, ReadOnlySpan<Byte> second)
		{
			return !(first == second);
		}

		public static Boolean operator !=(in UnsafeSize640 first, in UnsafeSize640 second)
		{
			return !(first == second);
		}

		public static Boolean operator >(in UnsafeSize640 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >(in UnsafeSize640 first, in UnsafeSize640 second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >=(in UnsafeSize640 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator >=(in UnsafeSize640 first, in UnsafeSize640 second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator <(in UnsafeSize640 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <(in UnsafeSize640 first, in UnsafeSize640 second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <=(in UnsafeSize640 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) <= 0;
		}

		public static Boolean operator <=(in UnsafeSize640 first, in UnsafeSize640 second)
		{
			return first.CompareTo(second) <= 0;
		}

		private const String Data = UnsafeSize.Data;
		public const Int32 Size = 640;
		private fixed Byte Internal[Size];

		public readonly Type Type
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return typeof(UnsafeSize640);
			}
		}

		ref UnsafeSize640 IUnsafeSize<UnsafeSize640>.This
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return ref this;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size;
			}
		}

		readonly Span<Byte> IUnsafeSize<UnsafeSize640>.Full
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this;
			}
		}

		public readonly Boolean IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size <= 0 || AsReadOnlySpan().All(default(Byte));
			}
		}

		public UnsafeSize640(ReadOnlySpan<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}.");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, value.Length);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal UnsafeSize640(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal readonly void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue(Data, AsReadOnlySpan().ToArray(), typeof(Byte[]));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			GetObjectData(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ref UnsafeSize640 Deserialize(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}

			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize640 IUnsafeSize<UnsafeSize640>.Deserialize(SerializationInfo info, StreamingContext context)
		{
			return ref Deserialize(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Support<T>() where T : struct
		{
			return Unsafe.SizeOf<T>() is > 0 and <= Size;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ref T As<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<T> AsReadOnlySpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<Byte> AsSpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<T> AsSpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new Span<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize640 IUnsafeSize<UnsafeSize640>.SetFull()
		{
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize640 Fill(Byte value)
		{
			AsSpan().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize640 Fill<T>(T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize640 Fill<T>(in T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize640 Clear()
		{
			return ref Reset();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize640 Reset()
		{
			return ref Fill(default(Byte));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(Span<Byte> destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(ref UnsafeSize640 destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(Span<Byte> destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(ref UnsafeSize640 destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Byte[] ToArray()
		{
			return AsReadOnlySpan().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly Byte[] IUnsafeSize<UnsafeSize640>.ToFullArray()
		{
			return ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly T[] ToArray<T>() where T : struct
		{
			return AsReadOnlySpan<T>().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly T[] IUnsafeSize<UnsafeSize640>.ToFullArray<T>()
		{
			return ToArray<T>();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref *pointer;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(ReadOnlySpan<Byte> other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(UnsafeSize640 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(in UnsafeSize640 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly Int32 GetHashCode()
		{
			HashCode hash = new HashCode();
			hash.AddBytes(this);
			return hash.ToHashCode();
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other switch
			{
				null => IsEmpty,
				UnsafeSize640 value => Equals(value),
				Memory<Byte> value => Equals(value.Span),
				ReadOnlyMemory<Byte> value => Equals(value.Span),
				_ => false
			};
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(ReadOnlySpan<Byte> other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(UnsafeSize640 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(in UnsafeSize640 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly String ToString()
		{
			return Convert.ToHexString(this);
		}
	}

	[Serializable]
	public unsafe struct UnsafeSize704 : IUnsafeSize<UnsafeSize704>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize704 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize704 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		public static Boolean operator ==(in UnsafeSize704 first, ReadOnlySpan<Byte> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator ==(in UnsafeSize704 first, in UnsafeSize704 second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(in UnsafeSize704 first, ReadOnlySpan<Byte> second)
		{
			return !(first == second);
		}

		public static Boolean operator !=(in UnsafeSize704 first, in UnsafeSize704 second)
		{
			return !(first == second);
		}

		public static Boolean operator >(in UnsafeSize704 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >(in UnsafeSize704 first, in UnsafeSize704 second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >=(in UnsafeSize704 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator >=(in UnsafeSize704 first, in UnsafeSize704 second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator <(in UnsafeSize704 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <(in UnsafeSize704 first, in UnsafeSize704 second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <=(in UnsafeSize704 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) <= 0;
		}

		public static Boolean operator <=(in UnsafeSize704 first, in UnsafeSize704 second)
		{
			return first.CompareTo(second) <= 0;
		}

		private const String Data = UnsafeSize.Data;
		public const Int32 Size = 704;
		private fixed Byte Internal[Size];

		public readonly Type Type
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return typeof(UnsafeSize704);
			}
		}

		ref UnsafeSize704 IUnsafeSize<UnsafeSize704>.This
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return ref this;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size;
			}
		}

		readonly Span<Byte> IUnsafeSize<UnsafeSize704>.Full
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this;
			}
		}

		public readonly Boolean IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size <= 0 || AsReadOnlySpan().All(default(Byte));
			}
		}

		public UnsafeSize704(ReadOnlySpan<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}.");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, value.Length);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal UnsafeSize704(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal readonly void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue(Data, AsReadOnlySpan().ToArray(), typeof(Byte[]));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			GetObjectData(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ref UnsafeSize704 Deserialize(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}

			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize704 IUnsafeSize<UnsafeSize704>.Deserialize(SerializationInfo info, StreamingContext context)
		{
			return ref Deserialize(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Support<T>() where T : struct
		{
			return Unsafe.SizeOf<T>() is > 0 and <= Size;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ref T As<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<T> AsReadOnlySpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<Byte> AsSpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<T> AsSpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new Span<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize704 IUnsafeSize<UnsafeSize704>.SetFull()
		{
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize704 Fill(Byte value)
		{
			AsSpan().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize704 Fill<T>(T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize704 Fill<T>(in T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize704 Clear()
		{
			return ref Reset();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize704 Reset()
		{
			return ref Fill(default(Byte));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(Span<Byte> destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(ref UnsafeSize704 destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(Span<Byte> destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(ref UnsafeSize704 destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Byte[] ToArray()
		{
			return AsReadOnlySpan().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly Byte[] IUnsafeSize<UnsafeSize704>.ToFullArray()
		{
			return ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly T[] ToArray<T>() where T : struct
		{
			return AsReadOnlySpan<T>().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly T[] IUnsafeSize<UnsafeSize704>.ToFullArray<T>()
		{
			return ToArray<T>();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref *pointer;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(ReadOnlySpan<Byte> other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(UnsafeSize704 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(in UnsafeSize704 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly Int32 GetHashCode()
		{
			HashCode hash = new HashCode();
			hash.AddBytes(this);
			return hash.ToHashCode();
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other switch
			{
				null => IsEmpty,
				UnsafeSize704 value => Equals(value),
				Memory<Byte> value => Equals(value.Span),
				ReadOnlyMemory<Byte> value => Equals(value.Span),
				_ => false
			};
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(ReadOnlySpan<Byte> other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(UnsafeSize704 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(in UnsafeSize704 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly String ToString()
		{
			return Convert.ToHexString(this);
		}
	}

	[Serializable]
	public unsafe struct UnsafeSize768 : IUnsafeSize<UnsafeSize768>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize768 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize768 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		public static Boolean operator ==(in UnsafeSize768 first, ReadOnlySpan<Byte> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator ==(in UnsafeSize768 first, in UnsafeSize768 second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(in UnsafeSize768 first, ReadOnlySpan<Byte> second)
		{
			return !(first == second);
		}

		public static Boolean operator !=(in UnsafeSize768 first, in UnsafeSize768 second)
		{
			return !(first == second);
		}

		public static Boolean operator >(in UnsafeSize768 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >(in UnsafeSize768 first, in UnsafeSize768 second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >=(in UnsafeSize768 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator >=(in UnsafeSize768 first, in UnsafeSize768 second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator <(in UnsafeSize768 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <(in UnsafeSize768 first, in UnsafeSize768 second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <=(in UnsafeSize768 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) <= 0;
		}

		public static Boolean operator <=(in UnsafeSize768 first, in UnsafeSize768 second)
		{
			return first.CompareTo(second) <= 0;
		}

		private const String Data = UnsafeSize.Data;
		public const Int32 Size = 768;
		private fixed Byte Internal[Size];

		public readonly Type Type
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return typeof(UnsafeSize768);
			}
		}

		ref UnsafeSize768 IUnsafeSize<UnsafeSize768>.This
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return ref this;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size;
			}
		}

		readonly Span<Byte> IUnsafeSize<UnsafeSize768>.Full
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this;
			}
		}

		public readonly Boolean IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size <= 0 || AsReadOnlySpan().All(default(Byte));
			}
		}

		public UnsafeSize768(ReadOnlySpan<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}.");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, value.Length);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal UnsafeSize768(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal readonly void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue(Data, AsReadOnlySpan().ToArray(), typeof(Byte[]));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			GetObjectData(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ref UnsafeSize768 Deserialize(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}

			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize768 IUnsafeSize<UnsafeSize768>.Deserialize(SerializationInfo info, StreamingContext context)
		{
			return ref Deserialize(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Support<T>() where T : struct
		{
			return Unsafe.SizeOf<T>() is > 0 and <= Size;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ref T As<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<T> AsReadOnlySpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<Byte> AsSpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<T> AsSpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new Span<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize768 IUnsafeSize<UnsafeSize768>.SetFull()
		{
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize768 Fill(Byte value)
		{
			AsSpan().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize768 Fill<T>(T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize768 Fill<T>(in T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize768 Clear()
		{
			return ref Reset();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize768 Reset()
		{
			return ref Fill(default(Byte));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(Span<Byte> destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(ref UnsafeSize768 destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(Span<Byte> destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(ref UnsafeSize768 destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Byte[] ToArray()
		{
			return AsReadOnlySpan().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly Byte[] IUnsafeSize<UnsafeSize768>.ToFullArray()
		{
			return ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly T[] ToArray<T>() where T : struct
		{
			return AsReadOnlySpan<T>().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly T[] IUnsafeSize<UnsafeSize768>.ToFullArray<T>()
		{
			return ToArray<T>();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref *pointer;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(ReadOnlySpan<Byte> other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(UnsafeSize768 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(in UnsafeSize768 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly Int32 GetHashCode()
		{
			HashCode hash = new HashCode();
			hash.AddBytes(this);
			return hash.ToHashCode();
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other switch
			{
				null => IsEmpty,
				UnsafeSize768 value => Equals(value),
				Memory<Byte> value => Equals(value.Span),
				ReadOnlyMemory<Byte> value => Equals(value.Span),
				_ => false
			};
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(ReadOnlySpan<Byte> other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(UnsafeSize768 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(in UnsafeSize768 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly String ToString()
		{
			return Convert.ToHexString(this);
		}
	}

	[Serializable]
	public unsafe struct UnsafeSize832 : IUnsafeSize<UnsafeSize832>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize832 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize832 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		public static Boolean operator ==(in UnsafeSize832 first, ReadOnlySpan<Byte> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator ==(in UnsafeSize832 first, in UnsafeSize832 second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(in UnsafeSize832 first, ReadOnlySpan<Byte> second)
		{
			return !(first == second);
		}

		public static Boolean operator !=(in UnsafeSize832 first, in UnsafeSize832 second)
		{
			return !(first == second);
		}

		public static Boolean operator >(in UnsafeSize832 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >(in UnsafeSize832 first, in UnsafeSize832 second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >=(in UnsafeSize832 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator >=(in UnsafeSize832 first, in UnsafeSize832 second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator <(in UnsafeSize832 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <(in UnsafeSize832 first, in UnsafeSize832 second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <=(in UnsafeSize832 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) <= 0;
		}

		public static Boolean operator <=(in UnsafeSize832 first, in UnsafeSize832 second)
		{
			return first.CompareTo(second) <= 0;
		}

		private const String Data = UnsafeSize.Data;
		public const Int32 Size = 832;
		private fixed Byte Internal[Size];

		public readonly Type Type
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return typeof(UnsafeSize832);
			}
		}

		ref UnsafeSize832 IUnsafeSize<UnsafeSize832>.This
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return ref this;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size;
			}
		}

		readonly Span<Byte> IUnsafeSize<UnsafeSize832>.Full
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this;
			}
		}

		public readonly Boolean IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size <= 0 || AsReadOnlySpan().All(default(Byte));
			}
		}

		public UnsafeSize832(ReadOnlySpan<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}.");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, value.Length);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal UnsafeSize832(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal readonly void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue(Data, AsReadOnlySpan().ToArray(), typeof(Byte[]));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			GetObjectData(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ref UnsafeSize832 Deserialize(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}

			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize832 IUnsafeSize<UnsafeSize832>.Deserialize(SerializationInfo info, StreamingContext context)
		{
			return ref Deserialize(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Support<T>() where T : struct
		{
			return Unsafe.SizeOf<T>() is > 0 and <= Size;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ref T As<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<T> AsReadOnlySpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<Byte> AsSpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<T> AsSpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new Span<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize832 IUnsafeSize<UnsafeSize832>.SetFull()
		{
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize832 Fill(Byte value)
		{
			AsSpan().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize832 Fill<T>(T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize832 Fill<T>(in T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize832 Clear()
		{
			return ref Reset();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize832 Reset()
		{
			return ref Fill(default(Byte));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(Span<Byte> destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(ref UnsafeSize832 destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(Span<Byte> destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(ref UnsafeSize832 destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Byte[] ToArray()
		{
			return AsReadOnlySpan().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly Byte[] IUnsafeSize<UnsafeSize832>.ToFullArray()
		{
			return ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly T[] ToArray<T>() where T : struct
		{
			return AsReadOnlySpan<T>().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly T[] IUnsafeSize<UnsafeSize832>.ToFullArray<T>()
		{
			return ToArray<T>();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref *pointer;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(ReadOnlySpan<Byte> other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(UnsafeSize832 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(in UnsafeSize832 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly Int32 GetHashCode()
		{
			HashCode hash = new HashCode();
			hash.AddBytes(this);
			return hash.ToHashCode();
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other switch
			{
				null => IsEmpty,
				UnsafeSize832 value => Equals(value),
				Memory<Byte> value => Equals(value.Span),
				ReadOnlyMemory<Byte> value => Equals(value.Span),
				_ => false
			};
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(ReadOnlySpan<Byte> other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(UnsafeSize832 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(in UnsafeSize832 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly String ToString()
		{
			return Convert.ToHexString(this);
		}
	}

	[Serializable]
	public unsafe struct UnsafeSize896 : IUnsafeSize<UnsafeSize896>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize896 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize896 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		public static Boolean operator ==(in UnsafeSize896 first, ReadOnlySpan<Byte> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator ==(in UnsafeSize896 first, in UnsafeSize896 second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(in UnsafeSize896 first, ReadOnlySpan<Byte> second)
		{
			return !(first == second);
		}

		public static Boolean operator !=(in UnsafeSize896 first, in UnsafeSize896 second)
		{
			return !(first == second);
		}

		public static Boolean operator >(in UnsafeSize896 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >(in UnsafeSize896 first, in UnsafeSize896 second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >=(in UnsafeSize896 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator >=(in UnsafeSize896 first, in UnsafeSize896 second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator <(in UnsafeSize896 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <(in UnsafeSize896 first, in UnsafeSize896 second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <=(in UnsafeSize896 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) <= 0;
		}

		public static Boolean operator <=(in UnsafeSize896 first, in UnsafeSize896 second)
		{
			return first.CompareTo(second) <= 0;
		}

		private const String Data = UnsafeSize.Data;
		public const Int32 Size = 896;
		private fixed Byte Internal[Size];

		public readonly Type Type
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return typeof(UnsafeSize896);
			}
		}

		ref UnsafeSize896 IUnsafeSize<UnsafeSize896>.This
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return ref this;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size;
			}
		}

		readonly Span<Byte> IUnsafeSize<UnsafeSize896>.Full
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this;
			}
		}

		public readonly Boolean IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size <= 0 || AsReadOnlySpan().All(default(Byte));
			}
		}

		public UnsafeSize896(ReadOnlySpan<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}.");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, value.Length);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal UnsafeSize896(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal readonly void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue(Data, AsReadOnlySpan().ToArray(), typeof(Byte[]));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			GetObjectData(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ref UnsafeSize896 Deserialize(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}

			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize896 IUnsafeSize<UnsafeSize896>.Deserialize(SerializationInfo info, StreamingContext context)
		{
			return ref Deserialize(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Support<T>() where T : struct
		{
			return Unsafe.SizeOf<T>() is > 0 and <= Size;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ref T As<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<T> AsReadOnlySpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<Byte> AsSpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<T> AsSpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new Span<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize896 IUnsafeSize<UnsafeSize896>.SetFull()
		{
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize896 Fill(Byte value)
		{
			AsSpan().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize896 Fill<T>(T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize896 Fill<T>(in T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize896 Clear()
		{
			return ref Reset();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize896 Reset()
		{
			return ref Fill(default(Byte));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(Span<Byte> destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(ref UnsafeSize896 destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(Span<Byte> destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(ref UnsafeSize896 destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Byte[] ToArray()
		{
			return AsReadOnlySpan().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly Byte[] IUnsafeSize<UnsafeSize896>.ToFullArray()
		{
			return ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly T[] ToArray<T>() where T : struct
		{
			return AsReadOnlySpan<T>().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly T[] IUnsafeSize<UnsafeSize896>.ToFullArray<T>()
		{
			return ToArray<T>();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref *pointer;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(ReadOnlySpan<Byte> other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(UnsafeSize896 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(in UnsafeSize896 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly Int32 GetHashCode()
		{
			HashCode hash = new HashCode();
			hash.AddBytes(this);
			return hash.ToHashCode();
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other switch
			{
				null => IsEmpty,
				UnsafeSize896 value => Equals(value),
				Memory<Byte> value => Equals(value.Span),
				ReadOnlyMemory<Byte> value => Equals(value.Span),
				_ => false
			};
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(ReadOnlySpan<Byte> other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(UnsafeSize896 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(in UnsafeSize896 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly String ToString()
		{
			return Convert.ToHexString(this);
		}
	}

	[Serializable]
	public unsafe struct UnsafeSize960 : IUnsafeSize<UnsafeSize960>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize960 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize960 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		public static Boolean operator ==(in UnsafeSize960 first, ReadOnlySpan<Byte> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator ==(in UnsafeSize960 first, in UnsafeSize960 second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(in UnsafeSize960 first, ReadOnlySpan<Byte> second)
		{
			return !(first == second);
		}

		public static Boolean operator !=(in UnsafeSize960 first, in UnsafeSize960 second)
		{
			return !(first == second);
		}

		public static Boolean operator >(in UnsafeSize960 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >(in UnsafeSize960 first, in UnsafeSize960 second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >=(in UnsafeSize960 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator >=(in UnsafeSize960 first, in UnsafeSize960 second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator <(in UnsafeSize960 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <(in UnsafeSize960 first, in UnsafeSize960 second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <=(in UnsafeSize960 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) <= 0;
		}

		public static Boolean operator <=(in UnsafeSize960 first, in UnsafeSize960 second)
		{
			return first.CompareTo(second) <= 0;
		}

		private const String Data = UnsafeSize.Data;
		public const Int32 Size = 960;
		private fixed Byte Internal[Size];

		public readonly Type Type
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return typeof(UnsafeSize960);
			}
		}

		ref UnsafeSize960 IUnsafeSize<UnsafeSize960>.This
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return ref this;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size;
			}
		}

		readonly Span<Byte> IUnsafeSize<UnsafeSize960>.Full
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this;
			}
		}

		public readonly Boolean IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size <= 0 || AsReadOnlySpan().All(default(Byte));
			}
		}

		public UnsafeSize960(ReadOnlySpan<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}.");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, value.Length);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal UnsafeSize960(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal readonly void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue(Data, AsReadOnlySpan().ToArray(), typeof(Byte[]));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			GetObjectData(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ref UnsafeSize960 Deserialize(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}

			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize960 IUnsafeSize<UnsafeSize960>.Deserialize(SerializationInfo info, StreamingContext context)
		{
			return ref Deserialize(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Support<T>() where T : struct
		{
			return Unsafe.SizeOf<T>() is > 0 and <= Size;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ref T As<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<T> AsReadOnlySpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<Byte> AsSpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<T> AsSpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new Span<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize960 IUnsafeSize<UnsafeSize960>.SetFull()
		{
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize960 Fill(Byte value)
		{
			AsSpan().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize960 Fill<T>(T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize960 Fill<T>(in T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize960 Clear()
		{
			return ref Reset();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize960 Reset()
		{
			return ref Fill(default(Byte));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(Span<Byte> destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(ref UnsafeSize960 destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(Span<Byte> destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(ref UnsafeSize960 destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Byte[] ToArray()
		{
			return AsReadOnlySpan().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly Byte[] IUnsafeSize<UnsafeSize960>.ToFullArray()
		{
			return ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly T[] ToArray<T>() where T : struct
		{
			return AsReadOnlySpan<T>().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly T[] IUnsafeSize<UnsafeSize960>.ToFullArray<T>()
		{
			return ToArray<T>();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref *pointer;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(ReadOnlySpan<Byte> other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(UnsafeSize960 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(in UnsafeSize960 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly Int32 GetHashCode()
		{
			HashCode hash = new HashCode();
			hash.AddBytes(this);
			return hash.ToHashCode();
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other switch
			{
				null => IsEmpty,
				UnsafeSize960 value => Equals(value),
				Memory<Byte> value => Equals(value.Span),
				ReadOnlyMemory<Byte> value => Equals(value.Span),
				_ => false
			};
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(ReadOnlySpan<Byte> other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(UnsafeSize960 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(in UnsafeSize960 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly String ToString()
		{
			return Convert.ToHexString(this);
		}
	}

	[Serializable]
	public unsafe struct UnsafeSize1024 : IUnsafeSize<UnsafeSize1024>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize1024 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize1024 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		public static Boolean operator ==(in UnsafeSize1024 first, ReadOnlySpan<Byte> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator ==(in UnsafeSize1024 first, in UnsafeSize1024 second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(in UnsafeSize1024 first, ReadOnlySpan<Byte> second)
		{
			return !(first == second);
		}

		public static Boolean operator !=(in UnsafeSize1024 first, in UnsafeSize1024 second)
		{
			return !(first == second);
		}

		public static Boolean operator >(in UnsafeSize1024 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >(in UnsafeSize1024 first, in UnsafeSize1024 second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >=(in UnsafeSize1024 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator >=(in UnsafeSize1024 first, in UnsafeSize1024 second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator <(in UnsafeSize1024 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <(in UnsafeSize1024 first, in UnsafeSize1024 second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <=(in UnsafeSize1024 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) <= 0;
		}

		public static Boolean operator <=(in UnsafeSize1024 first, in UnsafeSize1024 second)
		{
			return first.CompareTo(second) <= 0;
		}

		private const String Data = UnsafeSize.Data;
		public const Int32 Size = 1024;
		private fixed Byte Internal[Size];

		public readonly Type Type
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return typeof(UnsafeSize1024);
			}
		}

		ref UnsafeSize1024 IUnsafeSize<UnsafeSize1024>.This
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return ref this;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size;
			}
		}

		readonly Span<Byte> IUnsafeSize<UnsafeSize1024>.Full
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this;
			}
		}

		public readonly Boolean IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size <= 0 || AsReadOnlySpan().All(default(Byte));
			}
		}

		public UnsafeSize1024(ReadOnlySpan<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}.");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, value.Length);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal UnsafeSize1024(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal readonly void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue(Data, AsReadOnlySpan().ToArray(), typeof(Byte[]));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			GetObjectData(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ref UnsafeSize1024 Deserialize(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}

			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize1024 IUnsafeSize<UnsafeSize1024>.Deserialize(SerializationInfo info, StreamingContext context)
		{
			return ref Deserialize(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Support<T>() where T : struct
		{
			return Unsafe.SizeOf<T>() is > 0 and <= Size;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ref T As<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<T> AsReadOnlySpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<Byte> AsSpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<T> AsSpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new Span<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize1024 IUnsafeSize<UnsafeSize1024>.SetFull()
		{
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize1024 Fill(Byte value)
		{
			AsSpan().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize1024 Fill<T>(T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize1024 Fill<T>(in T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize1024 Clear()
		{
			return ref Reset();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize1024 Reset()
		{
			return ref Fill(default(Byte));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(Span<Byte> destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(ref UnsafeSize1024 destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(Span<Byte> destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(ref UnsafeSize1024 destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Byte[] ToArray()
		{
			return AsReadOnlySpan().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly Byte[] IUnsafeSize<UnsafeSize1024>.ToFullArray()
		{
			return ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly T[] ToArray<T>() where T : struct
		{
			return AsReadOnlySpan<T>().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly T[] IUnsafeSize<UnsafeSize1024>.ToFullArray<T>()
		{
			return ToArray<T>();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref *pointer;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(ReadOnlySpan<Byte> other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(UnsafeSize1024 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(in UnsafeSize1024 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly Int32 GetHashCode()
		{
			HashCode hash = new HashCode();
			hash.AddBytes(this);
			return hash.ToHashCode();
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other switch
			{
				null => IsEmpty,
				UnsafeSize1024 value => Equals(value),
				Memory<Byte> value => Equals(value.Span),
				ReadOnlyMemory<Byte> value => Equals(value.Span),
				_ => false
			};
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(ReadOnlySpan<Byte> other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(UnsafeSize1024 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(in UnsafeSize1024 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly String ToString()
		{
			return Convert.ToHexString(this);
		}
	}

	[Serializable]
	public unsafe struct UnsafeSize1152 : IUnsafeSize<UnsafeSize1152>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize1152 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize1152 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		public static Boolean operator ==(in UnsafeSize1152 first, ReadOnlySpan<Byte> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator ==(in UnsafeSize1152 first, in UnsafeSize1152 second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(in UnsafeSize1152 first, ReadOnlySpan<Byte> second)
		{
			return !(first == second);
		}

		public static Boolean operator !=(in UnsafeSize1152 first, in UnsafeSize1152 second)
		{
			return !(first == second);
		}

		public static Boolean operator >(in UnsafeSize1152 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >(in UnsafeSize1152 first, in UnsafeSize1152 second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >=(in UnsafeSize1152 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator >=(in UnsafeSize1152 first, in UnsafeSize1152 second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator <(in UnsafeSize1152 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <(in UnsafeSize1152 first, in UnsafeSize1152 second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <=(in UnsafeSize1152 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) <= 0;
		}

		public static Boolean operator <=(in UnsafeSize1152 first, in UnsafeSize1152 second)
		{
			return first.CompareTo(second) <= 0;
		}

		private const String Data = UnsafeSize.Data;
		public const Int32 Size = 1152;
		private fixed Byte Internal[Size];

		public readonly Type Type
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return typeof(UnsafeSize1152);
			}
		}

		ref UnsafeSize1152 IUnsafeSize<UnsafeSize1152>.This
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return ref this;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size;
			}
		}

		readonly Span<Byte> IUnsafeSize<UnsafeSize1152>.Full
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this;
			}
		}

		public readonly Boolean IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size <= 0 || AsReadOnlySpan().All(default(Byte));
			}
		}

		public UnsafeSize1152(ReadOnlySpan<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}.");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, value.Length);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal UnsafeSize1152(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal readonly void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue(Data, AsReadOnlySpan().ToArray(), typeof(Byte[]));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			GetObjectData(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ref UnsafeSize1152 Deserialize(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}

			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize1152 IUnsafeSize<UnsafeSize1152>.Deserialize(SerializationInfo info, StreamingContext context)
		{
			return ref Deserialize(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Support<T>() where T : struct
		{
			return Unsafe.SizeOf<T>() is > 0 and <= Size;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ref T As<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<T> AsReadOnlySpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<Byte> AsSpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<T> AsSpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new Span<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize1152 IUnsafeSize<UnsafeSize1152>.SetFull()
		{
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize1152 Fill(Byte value)
		{
			AsSpan().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize1152 Fill<T>(T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize1152 Fill<T>(in T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize1152 Clear()
		{
			return ref Reset();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize1152 Reset()
		{
			return ref Fill(default(Byte));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(Span<Byte> destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(ref UnsafeSize1152 destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(Span<Byte> destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(ref UnsafeSize1152 destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Byte[] ToArray()
		{
			return AsReadOnlySpan().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly Byte[] IUnsafeSize<UnsafeSize1152>.ToFullArray()
		{
			return ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly T[] ToArray<T>() where T : struct
		{
			return AsReadOnlySpan<T>().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly T[] IUnsafeSize<UnsafeSize1152>.ToFullArray<T>()
		{
			return ToArray<T>();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref *pointer;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(ReadOnlySpan<Byte> other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(UnsafeSize1152 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(in UnsafeSize1152 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly Int32 GetHashCode()
		{
			HashCode hash = new HashCode();
			hash.AddBytes(this);
			return hash.ToHashCode();
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other switch
			{
				null => IsEmpty,
				UnsafeSize1152 value => Equals(value),
				Memory<Byte> value => Equals(value.Span),
				ReadOnlyMemory<Byte> value => Equals(value.Span),
				_ => false
			};
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(ReadOnlySpan<Byte> other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(UnsafeSize1152 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(in UnsafeSize1152 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly String ToString()
		{
			return Convert.ToHexString(this);
		}
	}

	[Serializable]
	public unsafe struct UnsafeSize1280 : IUnsafeSize<UnsafeSize1280>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize1280 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize1280 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		public static Boolean operator ==(in UnsafeSize1280 first, ReadOnlySpan<Byte> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator ==(in UnsafeSize1280 first, in UnsafeSize1280 second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(in UnsafeSize1280 first, ReadOnlySpan<Byte> second)
		{
			return !(first == second);
		}

		public static Boolean operator !=(in UnsafeSize1280 first, in UnsafeSize1280 second)
		{
			return !(first == second);
		}

		public static Boolean operator >(in UnsafeSize1280 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >(in UnsafeSize1280 first, in UnsafeSize1280 second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >=(in UnsafeSize1280 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator >=(in UnsafeSize1280 first, in UnsafeSize1280 second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator <(in UnsafeSize1280 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <(in UnsafeSize1280 first, in UnsafeSize1280 second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <=(in UnsafeSize1280 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) <= 0;
		}

		public static Boolean operator <=(in UnsafeSize1280 first, in UnsafeSize1280 second)
		{
			return first.CompareTo(second) <= 0;
		}

		private const String Data = UnsafeSize.Data;
		public const Int32 Size = 1280;
		private fixed Byte Internal[Size];

		public readonly Type Type
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return typeof(UnsafeSize1280);
			}
		}

		ref UnsafeSize1280 IUnsafeSize<UnsafeSize1280>.This
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return ref this;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size;
			}
		}

		readonly Span<Byte> IUnsafeSize<UnsafeSize1280>.Full
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this;
			}
		}

		public readonly Boolean IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size <= 0 || AsReadOnlySpan().All(default(Byte));
			}
		}

		public UnsafeSize1280(ReadOnlySpan<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}.");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, value.Length);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal UnsafeSize1280(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal readonly void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue(Data, AsReadOnlySpan().ToArray(), typeof(Byte[]));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			GetObjectData(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ref UnsafeSize1280 Deserialize(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}

			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize1280 IUnsafeSize<UnsafeSize1280>.Deserialize(SerializationInfo info, StreamingContext context)
		{
			return ref Deserialize(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Support<T>() where T : struct
		{
			return Unsafe.SizeOf<T>() is > 0 and <= Size;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ref T As<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<T> AsReadOnlySpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<Byte> AsSpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<T> AsSpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new Span<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize1280 IUnsafeSize<UnsafeSize1280>.SetFull()
		{
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize1280 Fill(Byte value)
		{
			AsSpan().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize1280 Fill<T>(T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize1280 Fill<T>(in T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize1280 Clear()
		{
			return ref Reset();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize1280 Reset()
		{
			return ref Fill(default(Byte));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(Span<Byte> destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(ref UnsafeSize1280 destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(Span<Byte> destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(ref UnsafeSize1280 destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Byte[] ToArray()
		{
			return AsReadOnlySpan().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly Byte[] IUnsafeSize<UnsafeSize1280>.ToFullArray()
		{
			return ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly T[] ToArray<T>() where T : struct
		{
			return AsReadOnlySpan<T>().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly T[] IUnsafeSize<UnsafeSize1280>.ToFullArray<T>()
		{
			return ToArray<T>();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref *pointer;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(ReadOnlySpan<Byte> other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(UnsafeSize1280 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(in UnsafeSize1280 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly Int32 GetHashCode()
		{
			HashCode hash = new HashCode();
			hash.AddBytes(this);
			return hash.ToHashCode();
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other switch
			{
				null => IsEmpty,
				UnsafeSize1280 value => Equals(value),
				Memory<Byte> value => Equals(value.Span),
				ReadOnlyMemory<Byte> value => Equals(value.Span),
				_ => false
			};
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(ReadOnlySpan<Byte> other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(UnsafeSize1280 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(in UnsafeSize1280 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly String ToString()
		{
			return Convert.ToHexString(this);
		}
	}

	[Serializable]
	public unsafe struct UnsafeSize1408 : IUnsafeSize<UnsafeSize1408>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize1408 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize1408 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		public static Boolean operator ==(in UnsafeSize1408 first, ReadOnlySpan<Byte> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator ==(in UnsafeSize1408 first, in UnsafeSize1408 second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(in UnsafeSize1408 first, ReadOnlySpan<Byte> second)
		{
			return !(first == second);
		}

		public static Boolean operator !=(in UnsafeSize1408 first, in UnsafeSize1408 second)
		{
			return !(first == second);
		}

		public static Boolean operator >(in UnsafeSize1408 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >(in UnsafeSize1408 first, in UnsafeSize1408 second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >=(in UnsafeSize1408 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator >=(in UnsafeSize1408 first, in UnsafeSize1408 second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator <(in UnsafeSize1408 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <(in UnsafeSize1408 first, in UnsafeSize1408 second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <=(in UnsafeSize1408 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) <= 0;
		}

		public static Boolean operator <=(in UnsafeSize1408 first, in UnsafeSize1408 second)
		{
			return first.CompareTo(second) <= 0;
		}

		private const String Data = UnsafeSize.Data;
		public const Int32 Size = 1408;
		private fixed Byte Internal[Size];

		public readonly Type Type
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return typeof(UnsafeSize1408);
			}
		}

		ref UnsafeSize1408 IUnsafeSize<UnsafeSize1408>.This
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return ref this;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size;
			}
		}

		readonly Span<Byte> IUnsafeSize<UnsafeSize1408>.Full
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this;
			}
		}

		public readonly Boolean IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size <= 0 || AsReadOnlySpan().All(default(Byte));
			}
		}

		public UnsafeSize1408(ReadOnlySpan<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}.");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, value.Length);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal UnsafeSize1408(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal readonly void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue(Data, AsReadOnlySpan().ToArray(), typeof(Byte[]));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			GetObjectData(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ref UnsafeSize1408 Deserialize(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}

			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize1408 IUnsafeSize<UnsafeSize1408>.Deserialize(SerializationInfo info, StreamingContext context)
		{
			return ref Deserialize(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Support<T>() where T : struct
		{
			return Unsafe.SizeOf<T>() is > 0 and <= Size;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ref T As<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<T> AsReadOnlySpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<Byte> AsSpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<T> AsSpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new Span<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize1408 IUnsafeSize<UnsafeSize1408>.SetFull()
		{
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize1408 Fill(Byte value)
		{
			AsSpan().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize1408 Fill<T>(T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize1408 Fill<T>(in T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize1408 Clear()
		{
			return ref Reset();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize1408 Reset()
		{
			return ref Fill(default(Byte));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(Span<Byte> destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(ref UnsafeSize1408 destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(Span<Byte> destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(ref UnsafeSize1408 destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Byte[] ToArray()
		{
			return AsReadOnlySpan().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly Byte[] IUnsafeSize<UnsafeSize1408>.ToFullArray()
		{
			return ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly T[] ToArray<T>() where T : struct
		{
			return AsReadOnlySpan<T>().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly T[] IUnsafeSize<UnsafeSize1408>.ToFullArray<T>()
		{
			return ToArray<T>();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref *pointer;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(ReadOnlySpan<Byte> other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(UnsafeSize1408 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(in UnsafeSize1408 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly Int32 GetHashCode()
		{
			HashCode hash = new HashCode();
			hash.AddBytes(this);
			return hash.ToHashCode();
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other switch
			{
				null => IsEmpty,
				UnsafeSize1408 value => Equals(value),
				Memory<Byte> value => Equals(value.Span),
				ReadOnlyMemory<Byte> value => Equals(value.Span),
				_ => false
			};
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(ReadOnlySpan<Byte> other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(UnsafeSize1408 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(in UnsafeSize1408 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly String ToString()
		{
			return Convert.ToHexString(this);
		}
	}

	[Serializable]
	public unsafe struct UnsafeSize1536 : IUnsafeSize<UnsafeSize1536>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize1536 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize1536 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		public static Boolean operator ==(in UnsafeSize1536 first, ReadOnlySpan<Byte> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator ==(in UnsafeSize1536 first, in UnsafeSize1536 second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(in UnsafeSize1536 first, ReadOnlySpan<Byte> second)
		{
			return !(first == second);
		}

		public static Boolean operator !=(in UnsafeSize1536 first, in UnsafeSize1536 second)
		{
			return !(first == second);
		}

		public static Boolean operator >(in UnsafeSize1536 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >(in UnsafeSize1536 first, in UnsafeSize1536 second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >=(in UnsafeSize1536 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator >=(in UnsafeSize1536 first, in UnsafeSize1536 second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator <(in UnsafeSize1536 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <(in UnsafeSize1536 first, in UnsafeSize1536 second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <=(in UnsafeSize1536 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) <= 0;
		}

		public static Boolean operator <=(in UnsafeSize1536 first, in UnsafeSize1536 second)
		{
			return first.CompareTo(second) <= 0;
		}

		private const String Data = UnsafeSize.Data;
		public const Int32 Size = 1536;
		private fixed Byte Internal[Size];

		public readonly Type Type
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return typeof(UnsafeSize1536);
			}
		}

		ref UnsafeSize1536 IUnsafeSize<UnsafeSize1536>.This
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return ref this;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size;
			}
		}

		readonly Span<Byte> IUnsafeSize<UnsafeSize1536>.Full
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this;
			}
		}

		public readonly Boolean IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size <= 0 || AsReadOnlySpan().All(default(Byte));
			}
		}

		public UnsafeSize1536(ReadOnlySpan<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}.");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, value.Length);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal UnsafeSize1536(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal readonly void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue(Data, AsReadOnlySpan().ToArray(), typeof(Byte[]));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			GetObjectData(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ref UnsafeSize1536 Deserialize(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}

			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize1536 IUnsafeSize<UnsafeSize1536>.Deserialize(SerializationInfo info, StreamingContext context)
		{
			return ref Deserialize(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Support<T>() where T : struct
		{
			return Unsafe.SizeOf<T>() is > 0 and <= Size;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ref T As<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<T> AsReadOnlySpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<Byte> AsSpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<T> AsSpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new Span<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize1536 IUnsafeSize<UnsafeSize1536>.SetFull()
		{
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize1536 Fill(Byte value)
		{
			AsSpan().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize1536 Fill<T>(T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize1536 Fill<T>(in T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize1536 Clear()
		{
			return ref Reset();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize1536 Reset()
		{
			return ref Fill(default(Byte));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(Span<Byte> destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(ref UnsafeSize1536 destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(Span<Byte> destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(ref UnsafeSize1536 destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Byte[] ToArray()
		{
			return AsReadOnlySpan().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly Byte[] IUnsafeSize<UnsafeSize1536>.ToFullArray()
		{
			return ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly T[] ToArray<T>() where T : struct
		{
			return AsReadOnlySpan<T>().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly T[] IUnsafeSize<UnsafeSize1536>.ToFullArray<T>()
		{
			return ToArray<T>();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref *pointer;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(ReadOnlySpan<Byte> other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(UnsafeSize1536 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(in UnsafeSize1536 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly Int32 GetHashCode()
		{
			HashCode hash = new HashCode();
			hash.AddBytes(this);
			return hash.ToHashCode();
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other switch
			{
				null => IsEmpty,
				UnsafeSize1536 value => Equals(value),
				Memory<Byte> value => Equals(value.Span),
				ReadOnlyMemory<Byte> value => Equals(value.Span),
				_ => false
			};
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(ReadOnlySpan<Byte> other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(UnsafeSize1536 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(in UnsafeSize1536 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly String ToString()
		{
			return Convert.ToHexString(this);
		}
	}

	[Serializable]
	public unsafe struct UnsafeSize1664 : IUnsafeSize<UnsafeSize1664>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize1664 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize1664 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		public static Boolean operator ==(in UnsafeSize1664 first, ReadOnlySpan<Byte> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator ==(in UnsafeSize1664 first, in UnsafeSize1664 second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(in UnsafeSize1664 first, ReadOnlySpan<Byte> second)
		{
			return !(first == second);
		}

		public static Boolean operator !=(in UnsafeSize1664 first, in UnsafeSize1664 second)
		{
			return !(first == second);
		}

		public static Boolean operator >(in UnsafeSize1664 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >(in UnsafeSize1664 first, in UnsafeSize1664 second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >=(in UnsafeSize1664 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator >=(in UnsafeSize1664 first, in UnsafeSize1664 second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator <(in UnsafeSize1664 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <(in UnsafeSize1664 first, in UnsafeSize1664 second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <=(in UnsafeSize1664 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) <= 0;
		}

		public static Boolean operator <=(in UnsafeSize1664 first, in UnsafeSize1664 second)
		{
			return first.CompareTo(second) <= 0;
		}

		private const String Data = UnsafeSize.Data;
		public const Int32 Size = 1664;
		private fixed Byte Internal[Size];

		public readonly Type Type
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return typeof(UnsafeSize1664);
			}
		}

		ref UnsafeSize1664 IUnsafeSize<UnsafeSize1664>.This
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return ref this;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size;
			}
		}

		readonly Span<Byte> IUnsafeSize<UnsafeSize1664>.Full
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this;
			}
		}

		public readonly Boolean IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size <= 0 || AsReadOnlySpan().All(default(Byte));
			}
		}

		public UnsafeSize1664(ReadOnlySpan<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}.");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, value.Length);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal UnsafeSize1664(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal readonly void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue(Data, AsReadOnlySpan().ToArray(), typeof(Byte[]));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			GetObjectData(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ref UnsafeSize1664 Deserialize(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}

			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize1664 IUnsafeSize<UnsafeSize1664>.Deserialize(SerializationInfo info, StreamingContext context)
		{
			return ref Deserialize(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Support<T>() where T : struct
		{
			return Unsafe.SizeOf<T>() is > 0 and <= Size;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ref T As<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<T> AsReadOnlySpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<Byte> AsSpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<T> AsSpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new Span<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize1664 IUnsafeSize<UnsafeSize1664>.SetFull()
		{
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize1664 Fill(Byte value)
		{
			AsSpan().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize1664 Fill<T>(T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize1664 Fill<T>(in T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize1664 Clear()
		{
			return ref Reset();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize1664 Reset()
		{
			return ref Fill(default(Byte));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(Span<Byte> destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(ref UnsafeSize1664 destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(Span<Byte> destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(ref UnsafeSize1664 destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Byte[] ToArray()
		{
			return AsReadOnlySpan().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly Byte[] IUnsafeSize<UnsafeSize1664>.ToFullArray()
		{
			return ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly T[] ToArray<T>() where T : struct
		{
			return AsReadOnlySpan<T>().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly T[] IUnsafeSize<UnsafeSize1664>.ToFullArray<T>()
		{
			return ToArray<T>();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref *pointer;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(ReadOnlySpan<Byte> other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(UnsafeSize1664 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(in UnsafeSize1664 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly Int32 GetHashCode()
		{
			HashCode hash = new HashCode();
			hash.AddBytes(this);
			return hash.ToHashCode();
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other switch
			{
				null => IsEmpty,
				UnsafeSize1664 value => Equals(value),
				Memory<Byte> value => Equals(value.Span),
				ReadOnlyMemory<Byte> value => Equals(value.Span),
				_ => false
			};
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(ReadOnlySpan<Byte> other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(UnsafeSize1664 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(in UnsafeSize1664 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly String ToString()
		{
			return Convert.ToHexString(this);
		}
	}

	[Serializable]
	public unsafe struct UnsafeSize1792 : IUnsafeSize<UnsafeSize1792>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize1792 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize1792 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		public static Boolean operator ==(in UnsafeSize1792 first, ReadOnlySpan<Byte> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator ==(in UnsafeSize1792 first, in UnsafeSize1792 second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(in UnsafeSize1792 first, ReadOnlySpan<Byte> second)
		{
			return !(first == second);
		}

		public static Boolean operator !=(in UnsafeSize1792 first, in UnsafeSize1792 second)
		{
			return !(first == second);
		}

		public static Boolean operator >(in UnsafeSize1792 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >(in UnsafeSize1792 first, in UnsafeSize1792 second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >=(in UnsafeSize1792 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator >=(in UnsafeSize1792 first, in UnsafeSize1792 second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator <(in UnsafeSize1792 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <(in UnsafeSize1792 first, in UnsafeSize1792 second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <=(in UnsafeSize1792 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) <= 0;
		}

		public static Boolean operator <=(in UnsafeSize1792 first, in UnsafeSize1792 second)
		{
			return first.CompareTo(second) <= 0;
		}

		private const String Data = UnsafeSize.Data;
		public const Int32 Size = 1792;
		private fixed Byte Internal[Size];

		public readonly Type Type
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return typeof(UnsafeSize1792);
			}
		}

		ref UnsafeSize1792 IUnsafeSize<UnsafeSize1792>.This
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return ref this;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size;
			}
		}

		readonly Span<Byte> IUnsafeSize<UnsafeSize1792>.Full
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this;
			}
		}

		public readonly Boolean IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size <= 0 || AsReadOnlySpan().All(default(Byte));
			}
		}

		public UnsafeSize1792(ReadOnlySpan<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}.");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, value.Length);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal UnsafeSize1792(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal readonly void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue(Data, AsReadOnlySpan().ToArray(), typeof(Byte[]));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			GetObjectData(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ref UnsafeSize1792 Deserialize(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}

			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize1792 IUnsafeSize<UnsafeSize1792>.Deserialize(SerializationInfo info, StreamingContext context)
		{
			return ref Deserialize(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Support<T>() where T : struct
		{
			return Unsafe.SizeOf<T>() is > 0 and <= Size;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ref T As<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<T> AsReadOnlySpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<Byte> AsSpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<T> AsSpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new Span<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize1792 IUnsafeSize<UnsafeSize1792>.SetFull()
		{
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize1792 Fill(Byte value)
		{
			AsSpan().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize1792 Fill<T>(T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize1792 Fill<T>(in T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize1792 Clear()
		{
			return ref Reset();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize1792 Reset()
		{
			return ref Fill(default(Byte));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(Span<Byte> destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(ref UnsafeSize1792 destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(Span<Byte> destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(ref UnsafeSize1792 destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Byte[] ToArray()
		{
			return AsReadOnlySpan().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly Byte[] IUnsafeSize<UnsafeSize1792>.ToFullArray()
		{
			return ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly T[] ToArray<T>() where T : struct
		{
			return AsReadOnlySpan<T>().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly T[] IUnsafeSize<UnsafeSize1792>.ToFullArray<T>()
		{
			return ToArray<T>();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref *pointer;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(ReadOnlySpan<Byte> other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(UnsafeSize1792 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(in UnsafeSize1792 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly Int32 GetHashCode()
		{
			HashCode hash = new HashCode();
			hash.AddBytes(this);
			return hash.ToHashCode();
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other switch
			{
				null => IsEmpty,
				UnsafeSize1792 value => Equals(value),
				Memory<Byte> value => Equals(value.Span),
				ReadOnlyMemory<Byte> value => Equals(value.Span),
				_ => false
			};
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(ReadOnlySpan<Byte> other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(UnsafeSize1792 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(in UnsafeSize1792 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly String ToString()
		{
			return Convert.ToHexString(this);
		}
	}

	[Serializable]
	public unsafe struct UnsafeSize1920 : IUnsafeSize<UnsafeSize1920>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize1920 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize1920 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		public static Boolean operator ==(in UnsafeSize1920 first, ReadOnlySpan<Byte> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator ==(in UnsafeSize1920 first, in UnsafeSize1920 second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(in UnsafeSize1920 first, ReadOnlySpan<Byte> second)
		{
			return !(first == second);
		}

		public static Boolean operator !=(in UnsafeSize1920 first, in UnsafeSize1920 second)
		{
			return !(first == second);
		}

		public static Boolean operator >(in UnsafeSize1920 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >(in UnsafeSize1920 first, in UnsafeSize1920 second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >=(in UnsafeSize1920 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator >=(in UnsafeSize1920 first, in UnsafeSize1920 second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator <(in UnsafeSize1920 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <(in UnsafeSize1920 first, in UnsafeSize1920 second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <=(in UnsafeSize1920 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) <= 0;
		}

		public static Boolean operator <=(in UnsafeSize1920 first, in UnsafeSize1920 second)
		{
			return first.CompareTo(second) <= 0;
		}

		private const String Data = UnsafeSize.Data;
		public const Int32 Size = 1920;
		private fixed Byte Internal[Size];

		public readonly Type Type
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return typeof(UnsafeSize1920);
			}
		}

		ref UnsafeSize1920 IUnsafeSize<UnsafeSize1920>.This
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return ref this;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size;
			}
		}

		readonly Span<Byte> IUnsafeSize<UnsafeSize1920>.Full
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this;
			}
		}

		public readonly Boolean IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size <= 0 || AsReadOnlySpan().All(default(Byte));
			}
		}

		public UnsafeSize1920(ReadOnlySpan<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}.");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, value.Length);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal UnsafeSize1920(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal readonly void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue(Data, AsReadOnlySpan().ToArray(), typeof(Byte[]));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			GetObjectData(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ref UnsafeSize1920 Deserialize(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}

			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize1920 IUnsafeSize<UnsafeSize1920>.Deserialize(SerializationInfo info, StreamingContext context)
		{
			return ref Deserialize(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Support<T>() where T : struct
		{
			return Unsafe.SizeOf<T>() is > 0 and <= Size;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ref T As<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<T> AsReadOnlySpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<Byte> AsSpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<T> AsSpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new Span<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize1920 IUnsafeSize<UnsafeSize1920>.SetFull()
		{
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize1920 Fill(Byte value)
		{
			AsSpan().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize1920 Fill<T>(T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize1920 Fill<T>(in T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize1920 Clear()
		{
			return ref Reset();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize1920 Reset()
		{
			return ref Fill(default(Byte));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(Span<Byte> destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(ref UnsafeSize1920 destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(Span<Byte> destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(ref UnsafeSize1920 destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Byte[] ToArray()
		{
			return AsReadOnlySpan().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly Byte[] IUnsafeSize<UnsafeSize1920>.ToFullArray()
		{
			return ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly T[] ToArray<T>() where T : struct
		{
			return AsReadOnlySpan<T>().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly T[] IUnsafeSize<UnsafeSize1920>.ToFullArray<T>()
		{
			return ToArray<T>();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref *pointer;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(ReadOnlySpan<Byte> other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(UnsafeSize1920 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(in UnsafeSize1920 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly Int32 GetHashCode()
		{
			HashCode hash = new HashCode();
			hash.AddBytes(this);
			return hash.ToHashCode();
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other switch
			{
				null => IsEmpty,
				UnsafeSize1920 value => Equals(value),
				Memory<Byte> value => Equals(value.Span),
				ReadOnlyMemory<Byte> value => Equals(value.Span),
				_ => false
			};
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(ReadOnlySpan<Byte> other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(UnsafeSize1920 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(in UnsafeSize1920 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly String ToString()
		{
			return Convert.ToHexString(this);
		}
	}

	[Serializable]
	public unsafe struct UnsafeSize2048 : IUnsafeSize<UnsafeSize2048>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize2048 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize2048 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		public static Boolean operator ==(in UnsafeSize2048 first, ReadOnlySpan<Byte> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator ==(in UnsafeSize2048 first, in UnsafeSize2048 second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(in UnsafeSize2048 first, ReadOnlySpan<Byte> second)
		{
			return !(first == second);
		}

		public static Boolean operator !=(in UnsafeSize2048 first, in UnsafeSize2048 second)
		{
			return !(first == second);
		}

		public static Boolean operator >(in UnsafeSize2048 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >(in UnsafeSize2048 first, in UnsafeSize2048 second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >=(in UnsafeSize2048 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator >=(in UnsafeSize2048 first, in UnsafeSize2048 second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator <(in UnsafeSize2048 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <(in UnsafeSize2048 first, in UnsafeSize2048 second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <=(in UnsafeSize2048 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) <= 0;
		}

		public static Boolean operator <=(in UnsafeSize2048 first, in UnsafeSize2048 second)
		{
			return first.CompareTo(second) <= 0;
		}

		private const String Data = UnsafeSize.Data;
		public const Int32 Size = 2048;
		private fixed Byte Internal[Size];

		public readonly Type Type
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return typeof(UnsafeSize2048);
			}
		}

		ref UnsafeSize2048 IUnsafeSize<UnsafeSize2048>.This
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return ref this;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size;
			}
		}

		readonly Span<Byte> IUnsafeSize<UnsafeSize2048>.Full
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this;
			}
		}

		public readonly Boolean IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size <= 0 || AsReadOnlySpan().All(default(Byte));
			}
		}

		public UnsafeSize2048(ReadOnlySpan<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}.");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, value.Length);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal UnsafeSize2048(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal readonly void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue(Data, AsReadOnlySpan().ToArray(), typeof(Byte[]));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			GetObjectData(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ref UnsafeSize2048 Deserialize(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}

			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize2048 IUnsafeSize<UnsafeSize2048>.Deserialize(SerializationInfo info, StreamingContext context)
		{
			return ref Deserialize(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Support<T>() where T : struct
		{
			return Unsafe.SizeOf<T>() is > 0 and <= Size;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ref T As<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<T> AsReadOnlySpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<Byte> AsSpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<T> AsSpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new Span<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize2048 IUnsafeSize<UnsafeSize2048>.SetFull()
		{
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize2048 Fill(Byte value)
		{
			AsSpan().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize2048 Fill<T>(T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize2048 Fill<T>(in T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize2048 Clear()
		{
			return ref Reset();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize2048 Reset()
		{
			return ref Fill(default(Byte));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(Span<Byte> destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(ref UnsafeSize2048 destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(Span<Byte> destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(ref UnsafeSize2048 destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Byte[] ToArray()
		{
			return AsReadOnlySpan().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly Byte[] IUnsafeSize<UnsafeSize2048>.ToFullArray()
		{
			return ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly T[] ToArray<T>() where T : struct
		{
			return AsReadOnlySpan<T>().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly T[] IUnsafeSize<UnsafeSize2048>.ToFullArray<T>()
		{
			return ToArray<T>();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref *pointer;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(ReadOnlySpan<Byte> other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(UnsafeSize2048 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(in UnsafeSize2048 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly Int32 GetHashCode()
		{
			HashCode hash = new HashCode();
			hash.AddBytes(this);
			return hash.ToHashCode();
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other switch
			{
				null => IsEmpty,
				UnsafeSize2048 value => Equals(value),
				Memory<Byte> value => Equals(value.Span),
				ReadOnlyMemory<Byte> value => Equals(value.Span),
				_ => false
			};
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(ReadOnlySpan<Byte> other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(UnsafeSize2048 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(in UnsafeSize2048 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly String ToString()
		{
			return Convert.ToHexString(this);
		}
	}

	[Serializable]
	public unsafe struct UnsafeSize2176 : IUnsafeSize<UnsafeSize2176>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize2176 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize2176 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		public static Boolean operator ==(in UnsafeSize2176 first, ReadOnlySpan<Byte> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator ==(in UnsafeSize2176 first, in UnsafeSize2176 second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(in UnsafeSize2176 first, ReadOnlySpan<Byte> second)
		{
			return !(first == second);
		}

		public static Boolean operator !=(in UnsafeSize2176 first, in UnsafeSize2176 second)
		{
			return !(first == second);
		}

		public static Boolean operator >(in UnsafeSize2176 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >(in UnsafeSize2176 first, in UnsafeSize2176 second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >=(in UnsafeSize2176 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator >=(in UnsafeSize2176 first, in UnsafeSize2176 second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator <(in UnsafeSize2176 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <(in UnsafeSize2176 first, in UnsafeSize2176 second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <=(in UnsafeSize2176 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) <= 0;
		}

		public static Boolean operator <=(in UnsafeSize2176 first, in UnsafeSize2176 second)
		{
			return first.CompareTo(second) <= 0;
		}

		private const String Data = UnsafeSize.Data;
		public const Int32 Size = 2176;
		private fixed Byte Internal[Size];

		public readonly Type Type
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return typeof(UnsafeSize2176);
			}
		}

		ref UnsafeSize2176 IUnsafeSize<UnsafeSize2176>.This
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return ref this;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size;
			}
		}

		readonly Span<Byte> IUnsafeSize<UnsafeSize2176>.Full
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this;
			}
		}

		public readonly Boolean IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size <= 0 || AsReadOnlySpan().All(default(Byte));
			}
		}

		public UnsafeSize2176(ReadOnlySpan<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}.");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, value.Length);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal UnsafeSize2176(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal readonly void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue(Data, AsReadOnlySpan().ToArray(), typeof(Byte[]));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			GetObjectData(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ref UnsafeSize2176 Deserialize(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}

			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize2176 IUnsafeSize<UnsafeSize2176>.Deserialize(SerializationInfo info, StreamingContext context)
		{
			return ref Deserialize(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Support<T>() where T : struct
		{
			return Unsafe.SizeOf<T>() is > 0 and <= Size;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ref T As<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<T> AsReadOnlySpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<Byte> AsSpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<T> AsSpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new Span<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize2176 IUnsafeSize<UnsafeSize2176>.SetFull()
		{
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize2176 Fill(Byte value)
		{
			AsSpan().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize2176 Fill<T>(T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize2176 Fill<T>(in T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize2176 Clear()
		{
			return ref Reset();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize2176 Reset()
		{
			return ref Fill(default(Byte));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(Span<Byte> destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(ref UnsafeSize2176 destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(Span<Byte> destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(ref UnsafeSize2176 destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Byte[] ToArray()
		{
			return AsReadOnlySpan().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly Byte[] IUnsafeSize<UnsafeSize2176>.ToFullArray()
		{
			return ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly T[] ToArray<T>() where T : struct
		{
			return AsReadOnlySpan<T>().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly T[] IUnsafeSize<UnsafeSize2176>.ToFullArray<T>()
		{
			return ToArray<T>();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref *pointer;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(ReadOnlySpan<Byte> other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(UnsafeSize2176 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(in UnsafeSize2176 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly Int32 GetHashCode()
		{
			HashCode hash = new HashCode();
			hash.AddBytes(this);
			return hash.ToHashCode();
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other switch
			{
				null => IsEmpty,
				UnsafeSize2176 value => Equals(value),
				Memory<Byte> value => Equals(value.Span),
				ReadOnlyMemory<Byte> value => Equals(value.Span),
				_ => false
			};
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(ReadOnlySpan<Byte> other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(UnsafeSize2176 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(in UnsafeSize2176 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly String ToString()
		{
			return Convert.ToHexString(this);
		}
	}

	[Serializable]
	public unsafe struct UnsafeSize2304 : IUnsafeSize<UnsafeSize2304>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize2304 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize2304 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		public static Boolean operator ==(in UnsafeSize2304 first, ReadOnlySpan<Byte> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator ==(in UnsafeSize2304 first, in UnsafeSize2304 second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(in UnsafeSize2304 first, ReadOnlySpan<Byte> second)
		{
			return !(first == second);
		}

		public static Boolean operator !=(in UnsafeSize2304 first, in UnsafeSize2304 second)
		{
			return !(first == second);
		}

		public static Boolean operator >(in UnsafeSize2304 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >(in UnsafeSize2304 first, in UnsafeSize2304 second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >=(in UnsafeSize2304 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator >=(in UnsafeSize2304 first, in UnsafeSize2304 second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator <(in UnsafeSize2304 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <(in UnsafeSize2304 first, in UnsafeSize2304 second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <=(in UnsafeSize2304 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) <= 0;
		}

		public static Boolean operator <=(in UnsafeSize2304 first, in UnsafeSize2304 second)
		{
			return first.CompareTo(second) <= 0;
		}

		private const String Data = UnsafeSize.Data;
		public const Int32 Size = 2304;
		private fixed Byte Internal[Size];

		public readonly Type Type
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return typeof(UnsafeSize2304);
			}
		}

		ref UnsafeSize2304 IUnsafeSize<UnsafeSize2304>.This
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return ref this;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size;
			}
		}

		readonly Span<Byte> IUnsafeSize<UnsafeSize2304>.Full
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this;
			}
		}

		public readonly Boolean IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size <= 0 || AsReadOnlySpan().All(default(Byte));
			}
		}

		public UnsafeSize2304(ReadOnlySpan<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}.");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, value.Length);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal UnsafeSize2304(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal readonly void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue(Data, AsReadOnlySpan().ToArray(), typeof(Byte[]));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			GetObjectData(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ref UnsafeSize2304 Deserialize(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}

			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize2304 IUnsafeSize<UnsafeSize2304>.Deserialize(SerializationInfo info, StreamingContext context)
		{
			return ref Deserialize(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Support<T>() where T : struct
		{
			return Unsafe.SizeOf<T>() is > 0 and <= Size;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ref T As<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<T> AsReadOnlySpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<Byte> AsSpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<T> AsSpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new Span<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize2304 IUnsafeSize<UnsafeSize2304>.SetFull()
		{
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize2304 Fill(Byte value)
		{
			AsSpan().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize2304 Fill<T>(T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize2304 Fill<T>(in T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize2304 Clear()
		{
			return ref Reset();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize2304 Reset()
		{
			return ref Fill(default(Byte));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(Span<Byte> destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(ref UnsafeSize2304 destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(Span<Byte> destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(ref UnsafeSize2304 destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Byte[] ToArray()
		{
			return AsReadOnlySpan().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly Byte[] IUnsafeSize<UnsafeSize2304>.ToFullArray()
		{
			return ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly T[] ToArray<T>() where T : struct
		{
			return AsReadOnlySpan<T>().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly T[] IUnsafeSize<UnsafeSize2304>.ToFullArray<T>()
		{
			return ToArray<T>();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref *pointer;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(ReadOnlySpan<Byte> other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(UnsafeSize2304 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(in UnsafeSize2304 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly Int32 GetHashCode()
		{
			HashCode hash = new HashCode();
			hash.AddBytes(this);
			return hash.ToHashCode();
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other switch
			{
				null => IsEmpty,
				UnsafeSize2304 value => Equals(value),
				Memory<Byte> value => Equals(value.Span),
				ReadOnlyMemory<Byte> value => Equals(value.Span),
				_ => false
			};
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(ReadOnlySpan<Byte> other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(UnsafeSize2304 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(in UnsafeSize2304 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly String ToString()
		{
			return Convert.ToHexString(this);
		}
	}

	[Serializable]
	public unsafe struct UnsafeSize2432 : IUnsafeSize<UnsafeSize2432>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize2432 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize2432 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		public static Boolean operator ==(in UnsafeSize2432 first, ReadOnlySpan<Byte> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator ==(in UnsafeSize2432 first, in UnsafeSize2432 second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(in UnsafeSize2432 first, ReadOnlySpan<Byte> second)
		{
			return !(first == second);
		}

		public static Boolean operator !=(in UnsafeSize2432 first, in UnsafeSize2432 second)
		{
			return !(first == second);
		}

		public static Boolean operator >(in UnsafeSize2432 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >(in UnsafeSize2432 first, in UnsafeSize2432 second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >=(in UnsafeSize2432 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator >=(in UnsafeSize2432 first, in UnsafeSize2432 second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator <(in UnsafeSize2432 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <(in UnsafeSize2432 first, in UnsafeSize2432 second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <=(in UnsafeSize2432 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) <= 0;
		}

		public static Boolean operator <=(in UnsafeSize2432 first, in UnsafeSize2432 second)
		{
			return first.CompareTo(second) <= 0;
		}

		private const String Data = UnsafeSize.Data;
		public const Int32 Size = 2432;
		private fixed Byte Internal[Size];

		public readonly Type Type
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return typeof(UnsafeSize2432);
			}
		}

		ref UnsafeSize2432 IUnsafeSize<UnsafeSize2432>.This
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return ref this;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size;
			}
		}

		readonly Span<Byte> IUnsafeSize<UnsafeSize2432>.Full
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this;
			}
		}

		public readonly Boolean IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size <= 0 || AsReadOnlySpan().All(default(Byte));
			}
		}

		public UnsafeSize2432(ReadOnlySpan<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}.");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, value.Length);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal UnsafeSize2432(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal readonly void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue(Data, AsReadOnlySpan().ToArray(), typeof(Byte[]));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			GetObjectData(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ref UnsafeSize2432 Deserialize(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}

			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize2432 IUnsafeSize<UnsafeSize2432>.Deserialize(SerializationInfo info, StreamingContext context)
		{
			return ref Deserialize(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Support<T>() where T : struct
		{
			return Unsafe.SizeOf<T>() is > 0 and <= Size;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ref T As<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<T> AsReadOnlySpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<Byte> AsSpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<T> AsSpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new Span<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize2432 IUnsafeSize<UnsafeSize2432>.SetFull()
		{
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize2432 Fill(Byte value)
		{
			AsSpan().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize2432 Fill<T>(T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize2432 Fill<T>(in T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize2432 Clear()
		{
			return ref Reset();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize2432 Reset()
		{
			return ref Fill(default(Byte));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(Span<Byte> destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(ref UnsafeSize2432 destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(Span<Byte> destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(ref UnsafeSize2432 destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Byte[] ToArray()
		{
			return AsReadOnlySpan().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly Byte[] IUnsafeSize<UnsafeSize2432>.ToFullArray()
		{
			return ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly T[] ToArray<T>() where T : struct
		{
			return AsReadOnlySpan<T>().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly T[] IUnsafeSize<UnsafeSize2432>.ToFullArray<T>()
		{
			return ToArray<T>();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref *pointer;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(ReadOnlySpan<Byte> other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(UnsafeSize2432 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(in UnsafeSize2432 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly Int32 GetHashCode()
		{
			HashCode hash = new HashCode();
			hash.AddBytes(this);
			return hash.ToHashCode();
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other switch
			{
				null => IsEmpty,
				UnsafeSize2432 value => Equals(value),
				Memory<Byte> value => Equals(value.Span),
				ReadOnlyMemory<Byte> value => Equals(value.Span),
				_ => false
			};
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(ReadOnlySpan<Byte> other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(UnsafeSize2432 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(in UnsafeSize2432 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly String ToString()
		{
			return Convert.ToHexString(this);
		}
	}

	[Serializable]
	public unsafe struct UnsafeSize2560 : IUnsafeSize<UnsafeSize2560>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize2560 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize2560 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		public static Boolean operator ==(in UnsafeSize2560 first, ReadOnlySpan<Byte> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator ==(in UnsafeSize2560 first, in UnsafeSize2560 second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(in UnsafeSize2560 first, ReadOnlySpan<Byte> second)
		{
			return !(first == second);
		}

		public static Boolean operator !=(in UnsafeSize2560 first, in UnsafeSize2560 second)
		{
			return !(first == second);
		}

		public static Boolean operator >(in UnsafeSize2560 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >(in UnsafeSize2560 first, in UnsafeSize2560 second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >=(in UnsafeSize2560 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator >=(in UnsafeSize2560 first, in UnsafeSize2560 second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator <(in UnsafeSize2560 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <(in UnsafeSize2560 first, in UnsafeSize2560 second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <=(in UnsafeSize2560 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) <= 0;
		}

		public static Boolean operator <=(in UnsafeSize2560 first, in UnsafeSize2560 second)
		{
			return first.CompareTo(second) <= 0;
		}

		private const String Data = UnsafeSize.Data;
		public const Int32 Size = 2560;
		private fixed Byte Internal[Size];

		public readonly Type Type
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return typeof(UnsafeSize2560);
			}
		}

		ref UnsafeSize2560 IUnsafeSize<UnsafeSize2560>.This
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return ref this;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size;
			}
		}

		readonly Span<Byte> IUnsafeSize<UnsafeSize2560>.Full
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this;
			}
		}

		public readonly Boolean IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size <= 0 || AsReadOnlySpan().All(default(Byte));
			}
		}

		public UnsafeSize2560(ReadOnlySpan<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}.");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, value.Length);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal UnsafeSize2560(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal readonly void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue(Data, AsReadOnlySpan().ToArray(), typeof(Byte[]));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			GetObjectData(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ref UnsafeSize2560 Deserialize(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}

			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize2560 IUnsafeSize<UnsafeSize2560>.Deserialize(SerializationInfo info, StreamingContext context)
		{
			return ref Deserialize(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Support<T>() where T : struct
		{
			return Unsafe.SizeOf<T>() is > 0 and <= Size;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ref T As<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<T> AsReadOnlySpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<Byte> AsSpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<T> AsSpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new Span<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize2560 IUnsafeSize<UnsafeSize2560>.SetFull()
		{
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize2560 Fill(Byte value)
		{
			AsSpan().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize2560 Fill<T>(T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize2560 Fill<T>(in T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize2560 Clear()
		{
			return ref Reset();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize2560 Reset()
		{
			return ref Fill(default(Byte));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(Span<Byte> destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(ref UnsafeSize2560 destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(Span<Byte> destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(ref UnsafeSize2560 destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Byte[] ToArray()
		{
			return AsReadOnlySpan().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly Byte[] IUnsafeSize<UnsafeSize2560>.ToFullArray()
		{
			return ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly T[] ToArray<T>() where T : struct
		{
			return AsReadOnlySpan<T>().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly T[] IUnsafeSize<UnsafeSize2560>.ToFullArray<T>()
		{
			return ToArray<T>();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref *pointer;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(ReadOnlySpan<Byte> other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(UnsafeSize2560 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(in UnsafeSize2560 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly Int32 GetHashCode()
		{
			HashCode hash = new HashCode();
			hash.AddBytes(this);
			return hash.ToHashCode();
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other switch
			{
				null => IsEmpty,
				UnsafeSize2560 value => Equals(value),
				Memory<Byte> value => Equals(value.Span),
				ReadOnlyMemory<Byte> value => Equals(value.Span),
				_ => false
			};
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(ReadOnlySpan<Byte> other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(UnsafeSize2560 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(in UnsafeSize2560 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly String ToString()
		{
			return Convert.ToHexString(this);
		}
	}

	[Serializable]
	public unsafe struct UnsafeSize2688 : IUnsafeSize<UnsafeSize2688>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize2688 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize2688 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		public static Boolean operator ==(in UnsafeSize2688 first, ReadOnlySpan<Byte> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator ==(in UnsafeSize2688 first, in UnsafeSize2688 second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(in UnsafeSize2688 first, ReadOnlySpan<Byte> second)
		{
			return !(first == second);
		}

		public static Boolean operator !=(in UnsafeSize2688 first, in UnsafeSize2688 second)
		{
			return !(first == second);
		}

		public static Boolean operator >(in UnsafeSize2688 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >(in UnsafeSize2688 first, in UnsafeSize2688 second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >=(in UnsafeSize2688 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator >=(in UnsafeSize2688 first, in UnsafeSize2688 second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator <(in UnsafeSize2688 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <(in UnsafeSize2688 first, in UnsafeSize2688 second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <=(in UnsafeSize2688 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) <= 0;
		}

		public static Boolean operator <=(in UnsafeSize2688 first, in UnsafeSize2688 second)
		{
			return first.CompareTo(second) <= 0;
		}

		private const String Data = UnsafeSize.Data;
		public const Int32 Size = 2688;
		private fixed Byte Internal[Size];

		public readonly Type Type
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return typeof(UnsafeSize2688);
			}
		}

		ref UnsafeSize2688 IUnsafeSize<UnsafeSize2688>.This
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return ref this;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size;
			}
		}

		readonly Span<Byte> IUnsafeSize<UnsafeSize2688>.Full
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this;
			}
		}

		public readonly Boolean IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size <= 0 || AsReadOnlySpan().All(default(Byte));
			}
		}

		public UnsafeSize2688(ReadOnlySpan<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}.");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, value.Length);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal UnsafeSize2688(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal readonly void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue(Data, AsReadOnlySpan().ToArray(), typeof(Byte[]));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			GetObjectData(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ref UnsafeSize2688 Deserialize(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}

			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize2688 IUnsafeSize<UnsafeSize2688>.Deserialize(SerializationInfo info, StreamingContext context)
		{
			return ref Deserialize(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Support<T>() where T : struct
		{
			return Unsafe.SizeOf<T>() is > 0 and <= Size;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ref T As<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<T> AsReadOnlySpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<Byte> AsSpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<T> AsSpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new Span<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize2688 IUnsafeSize<UnsafeSize2688>.SetFull()
		{
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize2688 Fill(Byte value)
		{
			AsSpan().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize2688 Fill<T>(T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize2688 Fill<T>(in T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize2688 Clear()
		{
			return ref Reset();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize2688 Reset()
		{
			return ref Fill(default(Byte));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(Span<Byte> destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(ref UnsafeSize2688 destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(Span<Byte> destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(ref UnsafeSize2688 destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Byte[] ToArray()
		{
			return AsReadOnlySpan().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly Byte[] IUnsafeSize<UnsafeSize2688>.ToFullArray()
		{
			return ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly T[] ToArray<T>() where T : struct
		{
			return AsReadOnlySpan<T>().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly T[] IUnsafeSize<UnsafeSize2688>.ToFullArray<T>()
		{
			return ToArray<T>();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref *pointer;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(ReadOnlySpan<Byte> other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(UnsafeSize2688 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(in UnsafeSize2688 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly Int32 GetHashCode()
		{
			HashCode hash = new HashCode();
			hash.AddBytes(this);
			return hash.ToHashCode();
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other switch
			{
				null => IsEmpty,
				UnsafeSize2688 value => Equals(value),
				Memory<Byte> value => Equals(value.Span),
				ReadOnlyMemory<Byte> value => Equals(value.Span),
				_ => false
			};
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(ReadOnlySpan<Byte> other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(UnsafeSize2688 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(in UnsafeSize2688 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly String ToString()
		{
			return Convert.ToHexString(this);
		}
	}

	[Serializable]
	public unsafe struct UnsafeSize2816 : IUnsafeSize<UnsafeSize2816>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize2816 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize2816 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		public static Boolean operator ==(in UnsafeSize2816 first, ReadOnlySpan<Byte> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator ==(in UnsafeSize2816 first, in UnsafeSize2816 second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(in UnsafeSize2816 first, ReadOnlySpan<Byte> second)
		{
			return !(first == second);
		}

		public static Boolean operator !=(in UnsafeSize2816 first, in UnsafeSize2816 second)
		{
			return !(first == second);
		}

		public static Boolean operator >(in UnsafeSize2816 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >(in UnsafeSize2816 first, in UnsafeSize2816 second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >=(in UnsafeSize2816 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator >=(in UnsafeSize2816 first, in UnsafeSize2816 second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator <(in UnsafeSize2816 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <(in UnsafeSize2816 first, in UnsafeSize2816 second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <=(in UnsafeSize2816 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) <= 0;
		}

		public static Boolean operator <=(in UnsafeSize2816 first, in UnsafeSize2816 second)
		{
			return first.CompareTo(second) <= 0;
		}

		private const String Data = UnsafeSize.Data;
		public const Int32 Size = 2816;
		private fixed Byte Internal[Size];

		public readonly Type Type
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return typeof(UnsafeSize2816);
			}
		}

		ref UnsafeSize2816 IUnsafeSize<UnsafeSize2816>.This
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return ref this;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size;
			}
		}

		readonly Span<Byte> IUnsafeSize<UnsafeSize2816>.Full
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this;
			}
		}

		public readonly Boolean IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size <= 0 || AsReadOnlySpan().All(default(Byte));
			}
		}

		public UnsafeSize2816(ReadOnlySpan<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}.");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, value.Length);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal UnsafeSize2816(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal readonly void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue(Data, AsReadOnlySpan().ToArray(), typeof(Byte[]));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			GetObjectData(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ref UnsafeSize2816 Deserialize(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}

			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize2816 IUnsafeSize<UnsafeSize2816>.Deserialize(SerializationInfo info, StreamingContext context)
		{
			return ref Deserialize(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Support<T>() where T : struct
		{
			return Unsafe.SizeOf<T>() is > 0 and <= Size;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ref T As<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<T> AsReadOnlySpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<Byte> AsSpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<T> AsSpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new Span<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize2816 IUnsafeSize<UnsafeSize2816>.SetFull()
		{
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize2816 Fill(Byte value)
		{
			AsSpan().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize2816 Fill<T>(T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize2816 Fill<T>(in T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize2816 Clear()
		{
			return ref Reset();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize2816 Reset()
		{
			return ref Fill(default(Byte));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(Span<Byte> destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(ref UnsafeSize2816 destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(Span<Byte> destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(ref UnsafeSize2816 destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Byte[] ToArray()
		{
			return AsReadOnlySpan().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly Byte[] IUnsafeSize<UnsafeSize2816>.ToFullArray()
		{
			return ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly T[] ToArray<T>() where T : struct
		{
			return AsReadOnlySpan<T>().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly T[] IUnsafeSize<UnsafeSize2816>.ToFullArray<T>()
		{
			return ToArray<T>();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref *pointer;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(ReadOnlySpan<Byte> other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(UnsafeSize2816 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(in UnsafeSize2816 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly Int32 GetHashCode()
		{
			HashCode hash = new HashCode();
			hash.AddBytes(this);
			return hash.ToHashCode();
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other switch
			{
				null => IsEmpty,
				UnsafeSize2816 value => Equals(value),
				Memory<Byte> value => Equals(value.Span),
				ReadOnlyMemory<Byte> value => Equals(value.Span),
				_ => false
			};
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(ReadOnlySpan<Byte> other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(UnsafeSize2816 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(in UnsafeSize2816 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly String ToString()
		{
			return Convert.ToHexString(this);
		}
	}

	[Serializable]
	public unsafe struct UnsafeSize2944 : IUnsafeSize<UnsafeSize2944>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize2944 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize2944 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		public static Boolean operator ==(in UnsafeSize2944 first, ReadOnlySpan<Byte> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator ==(in UnsafeSize2944 first, in UnsafeSize2944 second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(in UnsafeSize2944 first, ReadOnlySpan<Byte> second)
		{
			return !(first == second);
		}

		public static Boolean operator !=(in UnsafeSize2944 first, in UnsafeSize2944 second)
		{
			return !(first == second);
		}

		public static Boolean operator >(in UnsafeSize2944 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >(in UnsafeSize2944 first, in UnsafeSize2944 second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >=(in UnsafeSize2944 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator >=(in UnsafeSize2944 first, in UnsafeSize2944 second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator <(in UnsafeSize2944 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <(in UnsafeSize2944 first, in UnsafeSize2944 second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <=(in UnsafeSize2944 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) <= 0;
		}

		public static Boolean operator <=(in UnsafeSize2944 first, in UnsafeSize2944 second)
		{
			return first.CompareTo(second) <= 0;
		}

		private const String Data = UnsafeSize.Data;
		public const Int32 Size = 2944;
		private fixed Byte Internal[Size];

		public readonly Type Type
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return typeof(UnsafeSize2944);
			}
		}

		ref UnsafeSize2944 IUnsafeSize<UnsafeSize2944>.This
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return ref this;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size;
			}
		}

		readonly Span<Byte> IUnsafeSize<UnsafeSize2944>.Full
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this;
			}
		}

		public readonly Boolean IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size <= 0 || AsReadOnlySpan().All(default(Byte));
			}
		}

		public UnsafeSize2944(ReadOnlySpan<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}.");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, value.Length);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal UnsafeSize2944(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal readonly void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue(Data, AsReadOnlySpan().ToArray(), typeof(Byte[]));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			GetObjectData(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ref UnsafeSize2944 Deserialize(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}

			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize2944 IUnsafeSize<UnsafeSize2944>.Deserialize(SerializationInfo info, StreamingContext context)
		{
			return ref Deserialize(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Support<T>() where T : struct
		{
			return Unsafe.SizeOf<T>() is > 0 and <= Size;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ref T As<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<T> AsReadOnlySpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<Byte> AsSpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<T> AsSpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new Span<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize2944 IUnsafeSize<UnsafeSize2944>.SetFull()
		{
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize2944 Fill(Byte value)
		{
			AsSpan().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize2944 Fill<T>(T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize2944 Fill<T>(in T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize2944 Clear()
		{
			return ref Reset();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize2944 Reset()
		{
			return ref Fill(default(Byte));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(Span<Byte> destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(ref UnsafeSize2944 destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(Span<Byte> destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(ref UnsafeSize2944 destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Byte[] ToArray()
		{
			return AsReadOnlySpan().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly Byte[] IUnsafeSize<UnsafeSize2944>.ToFullArray()
		{
			return ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly T[] ToArray<T>() where T : struct
		{
			return AsReadOnlySpan<T>().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly T[] IUnsafeSize<UnsafeSize2944>.ToFullArray<T>()
		{
			return ToArray<T>();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref *pointer;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(ReadOnlySpan<Byte> other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(UnsafeSize2944 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(in UnsafeSize2944 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly Int32 GetHashCode()
		{
			HashCode hash = new HashCode();
			hash.AddBytes(this);
			return hash.ToHashCode();
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other switch
			{
				null => IsEmpty,
				UnsafeSize2944 value => Equals(value),
				Memory<Byte> value => Equals(value.Span),
				ReadOnlyMemory<Byte> value => Equals(value.Span),
				_ => false
			};
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(ReadOnlySpan<Byte> other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(UnsafeSize2944 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(in UnsafeSize2944 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly String ToString()
		{
			return Convert.ToHexString(this);
		}
	}

	[Serializable]
	public unsafe struct UnsafeSize3072 : IUnsafeSize<UnsafeSize3072>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize3072 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize3072 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		public static Boolean operator ==(in UnsafeSize3072 first, ReadOnlySpan<Byte> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator ==(in UnsafeSize3072 first, in UnsafeSize3072 second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(in UnsafeSize3072 first, ReadOnlySpan<Byte> second)
		{
			return !(first == second);
		}

		public static Boolean operator !=(in UnsafeSize3072 first, in UnsafeSize3072 second)
		{
			return !(first == second);
		}

		public static Boolean operator >(in UnsafeSize3072 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >(in UnsafeSize3072 first, in UnsafeSize3072 second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >=(in UnsafeSize3072 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator >=(in UnsafeSize3072 first, in UnsafeSize3072 second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator <(in UnsafeSize3072 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <(in UnsafeSize3072 first, in UnsafeSize3072 second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <=(in UnsafeSize3072 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) <= 0;
		}

		public static Boolean operator <=(in UnsafeSize3072 first, in UnsafeSize3072 second)
		{
			return first.CompareTo(second) <= 0;
		}

		private const String Data = UnsafeSize.Data;
		public const Int32 Size = 3072;
		private fixed Byte Internal[Size];

		public readonly Type Type
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return typeof(UnsafeSize3072);
			}
		}

		ref UnsafeSize3072 IUnsafeSize<UnsafeSize3072>.This
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return ref this;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size;
			}
		}

		readonly Span<Byte> IUnsafeSize<UnsafeSize3072>.Full
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this;
			}
		}

		public readonly Boolean IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size <= 0 || AsReadOnlySpan().All(default(Byte));
			}
		}

		public UnsafeSize3072(ReadOnlySpan<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}.");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, value.Length);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal UnsafeSize3072(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal readonly void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue(Data, AsReadOnlySpan().ToArray(), typeof(Byte[]));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			GetObjectData(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ref UnsafeSize3072 Deserialize(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}

			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize3072 IUnsafeSize<UnsafeSize3072>.Deserialize(SerializationInfo info, StreamingContext context)
		{
			return ref Deserialize(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Support<T>() where T : struct
		{
			return Unsafe.SizeOf<T>() is > 0 and <= Size;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ref T As<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<T> AsReadOnlySpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<Byte> AsSpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<T> AsSpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new Span<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize3072 IUnsafeSize<UnsafeSize3072>.SetFull()
		{
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize3072 Fill(Byte value)
		{
			AsSpan().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize3072 Fill<T>(T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize3072 Fill<T>(in T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize3072 Clear()
		{
			return ref Reset();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize3072 Reset()
		{
			return ref Fill(default(Byte));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(Span<Byte> destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(ref UnsafeSize3072 destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(Span<Byte> destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(ref UnsafeSize3072 destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Byte[] ToArray()
		{
			return AsReadOnlySpan().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly Byte[] IUnsafeSize<UnsafeSize3072>.ToFullArray()
		{
			return ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly T[] ToArray<T>() where T : struct
		{
			return AsReadOnlySpan<T>().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly T[] IUnsafeSize<UnsafeSize3072>.ToFullArray<T>()
		{
			return ToArray<T>();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref *pointer;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(ReadOnlySpan<Byte> other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(UnsafeSize3072 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(in UnsafeSize3072 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly Int32 GetHashCode()
		{
			HashCode hash = new HashCode();
			hash.AddBytes(this);
			return hash.ToHashCode();
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other switch
			{
				null => IsEmpty,
				UnsafeSize3072 value => Equals(value),
				Memory<Byte> value => Equals(value.Span),
				ReadOnlyMemory<Byte> value => Equals(value.Span),
				_ => false
			};
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(ReadOnlySpan<Byte> other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(UnsafeSize3072 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(in UnsafeSize3072 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly String ToString()
		{
			return Convert.ToHexString(this);
		}
	}

	[Serializable]
	public unsafe struct UnsafeSize3200 : IUnsafeSize<UnsafeSize3200>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize3200 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize3200 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		public static Boolean operator ==(in UnsafeSize3200 first, ReadOnlySpan<Byte> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator ==(in UnsafeSize3200 first, in UnsafeSize3200 second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(in UnsafeSize3200 first, ReadOnlySpan<Byte> second)
		{
			return !(first == second);
		}

		public static Boolean operator !=(in UnsafeSize3200 first, in UnsafeSize3200 second)
		{
			return !(first == second);
		}

		public static Boolean operator >(in UnsafeSize3200 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >(in UnsafeSize3200 first, in UnsafeSize3200 second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >=(in UnsafeSize3200 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator >=(in UnsafeSize3200 first, in UnsafeSize3200 second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator <(in UnsafeSize3200 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <(in UnsafeSize3200 first, in UnsafeSize3200 second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <=(in UnsafeSize3200 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) <= 0;
		}

		public static Boolean operator <=(in UnsafeSize3200 first, in UnsafeSize3200 second)
		{
			return first.CompareTo(second) <= 0;
		}

		private const String Data = UnsafeSize.Data;
		public const Int32 Size = 3200;
		private fixed Byte Internal[Size];

		public readonly Type Type
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return typeof(UnsafeSize3200);
			}
		}

		ref UnsafeSize3200 IUnsafeSize<UnsafeSize3200>.This
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return ref this;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size;
			}
		}

		readonly Span<Byte> IUnsafeSize<UnsafeSize3200>.Full
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this;
			}
		}

		public readonly Boolean IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size <= 0 || AsReadOnlySpan().All(default(Byte));
			}
		}

		public UnsafeSize3200(ReadOnlySpan<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}.");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, value.Length);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal UnsafeSize3200(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal readonly void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue(Data, AsReadOnlySpan().ToArray(), typeof(Byte[]));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			GetObjectData(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ref UnsafeSize3200 Deserialize(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}

			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize3200 IUnsafeSize<UnsafeSize3200>.Deserialize(SerializationInfo info, StreamingContext context)
		{
			return ref Deserialize(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Support<T>() where T : struct
		{
			return Unsafe.SizeOf<T>() is > 0 and <= Size;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ref T As<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<T> AsReadOnlySpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<Byte> AsSpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<T> AsSpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new Span<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize3200 IUnsafeSize<UnsafeSize3200>.SetFull()
		{
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize3200 Fill(Byte value)
		{
			AsSpan().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize3200 Fill<T>(T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize3200 Fill<T>(in T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize3200 Clear()
		{
			return ref Reset();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize3200 Reset()
		{
			return ref Fill(default(Byte));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(Span<Byte> destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(ref UnsafeSize3200 destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(Span<Byte> destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(ref UnsafeSize3200 destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Byte[] ToArray()
		{
			return AsReadOnlySpan().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly Byte[] IUnsafeSize<UnsafeSize3200>.ToFullArray()
		{
			return ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly T[] ToArray<T>() where T : struct
		{
			return AsReadOnlySpan<T>().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly T[] IUnsafeSize<UnsafeSize3200>.ToFullArray<T>()
		{
			return ToArray<T>();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref *pointer;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(ReadOnlySpan<Byte> other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(UnsafeSize3200 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(in UnsafeSize3200 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly Int32 GetHashCode()
		{
			HashCode hash = new HashCode();
			hash.AddBytes(this);
			return hash.ToHashCode();
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other switch
			{
				null => IsEmpty,
				UnsafeSize3200 value => Equals(value),
				Memory<Byte> value => Equals(value.Span),
				ReadOnlyMemory<Byte> value => Equals(value.Span),
				_ => false
			};
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(ReadOnlySpan<Byte> other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(UnsafeSize3200 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(in UnsafeSize3200 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly String ToString()
		{
			return Convert.ToHexString(this);
		}
	}

	[Serializable]
	public unsafe struct UnsafeSize3328 : IUnsafeSize<UnsafeSize3328>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize3328 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize3328 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		public static Boolean operator ==(in UnsafeSize3328 first, ReadOnlySpan<Byte> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator ==(in UnsafeSize3328 first, in UnsafeSize3328 second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(in UnsafeSize3328 first, ReadOnlySpan<Byte> second)
		{
			return !(first == second);
		}

		public static Boolean operator !=(in UnsafeSize3328 first, in UnsafeSize3328 second)
		{
			return !(first == second);
		}

		public static Boolean operator >(in UnsafeSize3328 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >(in UnsafeSize3328 first, in UnsafeSize3328 second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >=(in UnsafeSize3328 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator >=(in UnsafeSize3328 first, in UnsafeSize3328 second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator <(in UnsafeSize3328 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <(in UnsafeSize3328 first, in UnsafeSize3328 second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <=(in UnsafeSize3328 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) <= 0;
		}

		public static Boolean operator <=(in UnsafeSize3328 first, in UnsafeSize3328 second)
		{
			return first.CompareTo(second) <= 0;
		}

		private const String Data = UnsafeSize.Data;
		public const Int32 Size = 3328;
		private fixed Byte Internal[Size];

		public readonly Type Type
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return typeof(UnsafeSize3328);
			}
		}

		ref UnsafeSize3328 IUnsafeSize<UnsafeSize3328>.This
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return ref this;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size;
			}
		}

		readonly Span<Byte> IUnsafeSize<UnsafeSize3328>.Full
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this;
			}
		}

		public readonly Boolean IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size <= 0 || AsReadOnlySpan().All(default(Byte));
			}
		}

		public UnsafeSize3328(ReadOnlySpan<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}.");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, value.Length);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal UnsafeSize3328(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal readonly void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue(Data, AsReadOnlySpan().ToArray(), typeof(Byte[]));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			GetObjectData(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ref UnsafeSize3328 Deserialize(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}

			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize3328 IUnsafeSize<UnsafeSize3328>.Deserialize(SerializationInfo info, StreamingContext context)
		{
			return ref Deserialize(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Support<T>() where T : struct
		{
			return Unsafe.SizeOf<T>() is > 0 and <= Size;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ref T As<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<T> AsReadOnlySpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<Byte> AsSpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<T> AsSpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new Span<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize3328 IUnsafeSize<UnsafeSize3328>.SetFull()
		{
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize3328 Fill(Byte value)
		{
			AsSpan().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize3328 Fill<T>(T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize3328 Fill<T>(in T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize3328 Clear()
		{
			return ref Reset();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize3328 Reset()
		{
			return ref Fill(default(Byte));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(Span<Byte> destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(ref UnsafeSize3328 destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(Span<Byte> destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(ref UnsafeSize3328 destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Byte[] ToArray()
		{
			return AsReadOnlySpan().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly Byte[] IUnsafeSize<UnsafeSize3328>.ToFullArray()
		{
			return ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly T[] ToArray<T>() where T : struct
		{
			return AsReadOnlySpan<T>().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly T[] IUnsafeSize<UnsafeSize3328>.ToFullArray<T>()
		{
			return ToArray<T>();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref *pointer;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(ReadOnlySpan<Byte> other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(UnsafeSize3328 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(in UnsafeSize3328 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly Int32 GetHashCode()
		{
			HashCode hash = new HashCode();
			hash.AddBytes(this);
			return hash.ToHashCode();
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other switch
			{
				null => IsEmpty,
				UnsafeSize3328 value => Equals(value),
				Memory<Byte> value => Equals(value.Span),
				ReadOnlyMemory<Byte> value => Equals(value.Span),
				_ => false
			};
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(ReadOnlySpan<Byte> other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(UnsafeSize3328 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(in UnsafeSize3328 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly String ToString()
		{
			return Convert.ToHexString(this);
		}
	}

	[Serializable]
	public unsafe struct UnsafeSize3456 : IUnsafeSize<UnsafeSize3456>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize3456 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize3456 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		public static Boolean operator ==(in UnsafeSize3456 first, ReadOnlySpan<Byte> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator ==(in UnsafeSize3456 first, in UnsafeSize3456 second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(in UnsafeSize3456 first, ReadOnlySpan<Byte> second)
		{
			return !(first == second);
		}

		public static Boolean operator !=(in UnsafeSize3456 first, in UnsafeSize3456 second)
		{
			return !(first == second);
		}

		public static Boolean operator >(in UnsafeSize3456 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >(in UnsafeSize3456 first, in UnsafeSize3456 second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >=(in UnsafeSize3456 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator >=(in UnsafeSize3456 first, in UnsafeSize3456 second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator <(in UnsafeSize3456 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <(in UnsafeSize3456 first, in UnsafeSize3456 second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <=(in UnsafeSize3456 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) <= 0;
		}

		public static Boolean operator <=(in UnsafeSize3456 first, in UnsafeSize3456 second)
		{
			return first.CompareTo(second) <= 0;
		}

		private const String Data = UnsafeSize.Data;
		public const Int32 Size = 3456;
		private fixed Byte Internal[Size];

		public readonly Type Type
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return typeof(UnsafeSize3456);
			}
		}

		ref UnsafeSize3456 IUnsafeSize<UnsafeSize3456>.This
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return ref this;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size;
			}
		}

		readonly Span<Byte> IUnsafeSize<UnsafeSize3456>.Full
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this;
			}
		}

		public readonly Boolean IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size <= 0 || AsReadOnlySpan().All(default(Byte));
			}
		}

		public UnsafeSize3456(ReadOnlySpan<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}.");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, value.Length);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal UnsafeSize3456(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal readonly void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue(Data, AsReadOnlySpan().ToArray(), typeof(Byte[]));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			GetObjectData(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ref UnsafeSize3456 Deserialize(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}

			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize3456 IUnsafeSize<UnsafeSize3456>.Deserialize(SerializationInfo info, StreamingContext context)
		{
			return ref Deserialize(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Support<T>() where T : struct
		{
			return Unsafe.SizeOf<T>() is > 0 and <= Size;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ref T As<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<T> AsReadOnlySpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<Byte> AsSpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<T> AsSpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new Span<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize3456 IUnsafeSize<UnsafeSize3456>.SetFull()
		{
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize3456 Fill(Byte value)
		{
			AsSpan().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize3456 Fill<T>(T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize3456 Fill<T>(in T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize3456 Clear()
		{
			return ref Reset();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize3456 Reset()
		{
			return ref Fill(default(Byte));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(Span<Byte> destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(ref UnsafeSize3456 destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(Span<Byte> destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(ref UnsafeSize3456 destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Byte[] ToArray()
		{
			return AsReadOnlySpan().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly Byte[] IUnsafeSize<UnsafeSize3456>.ToFullArray()
		{
			return ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly T[] ToArray<T>() where T : struct
		{
			return AsReadOnlySpan<T>().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly T[] IUnsafeSize<UnsafeSize3456>.ToFullArray<T>()
		{
			return ToArray<T>();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref *pointer;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(ReadOnlySpan<Byte> other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(UnsafeSize3456 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(in UnsafeSize3456 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly Int32 GetHashCode()
		{
			HashCode hash = new HashCode();
			hash.AddBytes(this);
			return hash.ToHashCode();
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other switch
			{
				null => IsEmpty,
				UnsafeSize3456 value => Equals(value),
				Memory<Byte> value => Equals(value.Span),
				ReadOnlyMemory<Byte> value => Equals(value.Span),
				_ => false
			};
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(ReadOnlySpan<Byte> other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(UnsafeSize3456 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(in UnsafeSize3456 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly String ToString()
		{
			return Convert.ToHexString(this);
		}
	}

	[Serializable]
	public unsafe struct UnsafeSize3584 : IUnsafeSize<UnsafeSize3584>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize3584 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize3584 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		public static Boolean operator ==(in UnsafeSize3584 first, ReadOnlySpan<Byte> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator ==(in UnsafeSize3584 first, in UnsafeSize3584 second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(in UnsafeSize3584 first, ReadOnlySpan<Byte> second)
		{
			return !(first == second);
		}

		public static Boolean operator !=(in UnsafeSize3584 first, in UnsafeSize3584 second)
		{
			return !(first == second);
		}

		public static Boolean operator >(in UnsafeSize3584 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >(in UnsafeSize3584 first, in UnsafeSize3584 second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >=(in UnsafeSize3584 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator >=(in UnsafeSize3584 first, in UnsafeSize3584 second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator <(in UnsafeSize3584 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <(in UnsafeSize3584 first, in UnsafeSize3584 second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <=(in UnsafeSize3584 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) <= 0;
		}

		public static Boolean operator <=(in UnsafeSize3584 first, in UnsafeSize3584 second)
		{
			return first.CompareTo(second) <= 0;
		}

		private const String Data = UnsafeSize.Data;
		public const Int32 Size = 3584;
		private fixed Byte Internal[Size];

		public readonly Type Type
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return typeof(UnsafeSize3584);
			}
		}

		ref UnsafeSize3584 IUnsafeSize<UnsafeSize3584>.This
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return ref this;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size;
			}
		}

		readonly Span<Byte> IUnsafeSize<UnsafeSize3584>.Full
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this;
			}
		}

		public readonly Boolean IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size <= 0 || AsReadOnlySpan().All(default(Byte));
			}
		}

		public UnsafeSize3584(ReadOnlySpan<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}.");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, value.Length);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal UnsafeSize3584(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal readonly void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue(Data, AsReadOnlySpan().ToArray(), typeof(Byte[]));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			GetObjectData(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ref UnsafeSize3584 Deserialize(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}

			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize3584 IUnsafeSize<UnsafeSize3584>.Deserialize(SerializationInfo info, StreamingContext context)
		{
			return ref Deserialize(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Support<T>() where T : struct
		{
			return Unsafe.SizeOf<T>() is > 0 and <= Size;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ref T As<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<T> AsReadOnlySpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<Byte> AsSpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<T> AsSpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new Span<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize3584 IUnsafeSize<UnsafeSize3584>.SetFull()
		{
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize3584 Fill(Byte value)
		{
			AsSpan().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize3584 Fill<T>(T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize3584 Fill<T>(in T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize3584 Clear()
		{
			return ref Reset();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize3584 Reset()
		{
			return ref Fill(default(Byte));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(Span<Byte> destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(ref UnsafeSize3584 destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(Span<Byte> destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(ref UnsafeSize3584 destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Byte[] ToArray()
		{
			return AsReadOnlySpan().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly Byte[] IUnsafeSize<UnsafeSize3584>.ToFullArray()
		{
			return ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly T[] ToArray<T>() where T : struct
		{
			return AsReadOnlySpan<T>().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly T[] IUnsafeSize<UnsafeSize3584>.ToFullArray<T>()
		{
			return ToArray<T>();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref *pointer;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(ReadOnlySpan<Byte> other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(UnsafeSize3584 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(in UnsafeSize3584 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly Int32 GetHashCode()
		{
			HashCode hash = new HashCode();
			hash.AddBytes(this);
			return hash.ToHashCode();
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other switch
			{
				null => IsEmpty,
				UnsafeSize3584 value => Equals(value),
				Memory<Byte> value => Equals(value.Span),
				ReadOnlyMemory<Byte> value => Equals(value.Span),
				_ => false
			};
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(ReadOnlySpan<Byte> other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(UnsafeSize3584 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(in UnsafeSize3584 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly String ToString()
		{
			return Convert.ToHexString(this);
		}
	}

	[Serializable]
	public unsafe struct UnsafeSize3712 : IUnsafeSize<UnsafeSize3712>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize3712 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize3712 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		public static Boolean operator ==(in UnsafeSize3712 first, ReadOnlySpan<Byte> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator ==(in UnsafeSize3712 first, in UnsafeSize3712 second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(in UnsafeSize3712 first, ReadOnlySpan<Byte> second)
		{
			return !(first == second);
		}

		public static Boolean operator !=(in UnsafeSize3712 first, in UnsafeSize3712 second)
		{
			return !(first == second);
		}

		public static Boolean operator >(in UnsafeSize3712 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >(in UnsafeSize3712 first, in UnsafeSize3712 second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >=(in UnsafeSize3712 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator >=(in UnsafeSize3712 first, in UnsafeSize3712 second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator <(in UnsafeSize3712 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <(in UnsafeSize3712 first, in UnsafeSize3712 second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <=(in UnsafeSize3712 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) <= 0;
		}

		public static Boolean operator <=(in UnsafeSize3712 first, in UnsafeSize3712 second)
		{
			return first.CompareTo(second) <= 0;
		}

		private const String Data = UnsafeSize.Data;
		public const Int32 Size = 3712;
		private fixed Byte Internal[Size];

		public readonly Type Type
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return typeof(UnsafeSize3712);
			}
		}

		ref UnsafeSize3712 IUnsafeSize<UnsafeSize3712>.This
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return ref this;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size;
			}
		}

		readonly Span<Byte> IUnsafeSize<UnsafeSize3712>.Full
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this;
			}
		}

		public readonly Boolean IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size <= 0 || AsReadOnlySpan().All(default(Byte));
			}
		}

		public UnsafeSize3712(ReadOnlySpan<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}.");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, value.Length);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal UnsafeSize3712(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal readonly void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue(Data, AsReadOnlySpan().ToArray(), typeof(Byte[]));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			GetObjectData(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ref UnsafeSize3712 Deserialize(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}

			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize3712 IUnsafeSize<UnsafeSize3712>.Deserialize(SerializationInfo info, StreamingContext context)
		{
			return ref Deserialize(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Support<T>() where T : struct
		{
			return Unsafe.SizeOf<T>() is > 0 and <= Size;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ref T As<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<T> AsReadOnlySpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<Byte> AsSpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<T> AsSpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new Span<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize3712 IUnsafeSize<UnsafeSize3712>.SetFull()
		{
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize3712 Fill(Byte value)
		{
			AsSpan().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize3712 Fill<T>(T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize3712 Fill<T>(in T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize3712 Clear()
		{
			return ref Reset();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize3712 Reset()
		{
			return ref Fill(default(Byte));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(Span<Byte> destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(ref UnsafeSize3712 destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(Span<Byte> destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(ref UnsafeSize3712 destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Byte[] ToArray()
		{
			return AsReadOnlySpan().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly Byte[] IUnsafeSize<UnsafeSize3712>.ToFullArray()
		{
			return ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly T[] ToArray<T>() where T : struct
		{
			return AsReadOnlySpan<T>().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly T[] IUnsafeSize<UnsafeSize3712>.ToFullArray<T>()
		{
			return ToArray<T>();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref *pointer;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(ReadOnlySpan<Byte> other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(UnsafeSize3712 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(in UnsafeSize3712 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly Int32 GetHashCode()
		{
			HashCode hash = new HashCode();
			hash.AddBytes(this);
			return hash.ToHashCode();
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other switch
			{
				null => IsEmpty,
				UnsafeSize3712 value => Equals(value),
				Memory<Byte> value => Equals(value.Span),
				ReadOnlyMemory<Byte> value => Equals(value.Span),
				_ => false
			};
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(ReadOnlySpan<Byte> other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(UnsafeSize3712 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(in UnsafeSize3712 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly String ToString()
		{
			return Convert.ToHexString(this);
		}
	}

	[Serializable]
	public unsafe struct UnsafeSize3840 : IUnsafeSize<UnsafeSize3840>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize3840 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize3840 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		public static Boolean operator ==(in UnsafeSize3840 first, ReadOnlySpan<Byte> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator ==(in UnsafeSize3840 first, in UnsafeSize3840 second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(in UnsafeSize3840 first, ReadOnlySpan<Byte> second)
		{
			return !(first == second);
		}

		public static Boolean operator !=(in UnsafeSize3840 first, in UnsafeSize3840 second)
		{
			return !(first == second);
		}

		public static Boolean operator >(in UnsafeSize3840 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >(in UnsafeSize3840 first, in UnsafeSize3840 second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >=(in UnsafeSize3840 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator >=(in UnsafeSize3840 first, in UnsafeSize3840 second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator <(in UnsafeSize3840 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <(in UnsafeSize3840 first, in UnsafeSize3840 second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <=(in UnsafeSize3840 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) <= 0;
		}

		public static Boolean operator <=(in UnsafeSize3840 first, in UnsafeSize3840 second)
		{
			return first.CompareTo(second) <= 0;
		}

		private const String Data = UnsafeSize.Data;
		public const Int32 Size = 3840;
		private fixed Byte Internal[Size];

		public readonly Type Type
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return typeof(UnsafeSize3840);
			}
		}

		ref UnsafeSize3840 IUnsafeSize<UnsafeSize3840>.This
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return ref this;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size;
			}
		}

		readonly Span<Byte> IUnsafeSize<UnsafeSize3840>.Full
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this;
			}
		}

		public readonly Boolean IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size <= 0 || AsReadOnlySpan().All(default(Byte));
			}
		}

		public UnsafeSize3840(ReadOnlySpan<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}.");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, value.Length);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal UnsafeSize3840(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal readonly void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue(Data, AsReadOnlySpan().ToArray(), typeof(Byte[]));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			GetObjectData(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ref UnsafeSize3840 Deserialize(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}

			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize3840 IUnsafeSize<UnsafeSize3840>.Deserialize(SerializationInfo info, StreamingContext context)
		{
			return ref Deserialize(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Support<T>() where T : struct
		{
			return Unsafe.SizeOf<T>() is > 0 and <= Size;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ref T As<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<T> AsReadOnlySpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<Byte> AsSpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<T> AsSpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new Span<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize3840 IUnsafeSize<UnsafeSize3840>.SetFull()
		{
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize3840 Fill(Byte value)
		{
			AsSpan().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize3840 Fill<T>(T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize3840 Fill<T>(in T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize3840 Clear()
		{
			return ref Reset();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize3840 Reset()
		{
			return ref Fill(default(Byte));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(Span<Byte> destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(ref UnsafeSize3840 destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(Span<Byte> destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(ref UnsafeSize3840 destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Byte[] ToArray()
		{
			return AsReadOnlySpan().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly Byte[] IUnsafeSize<UnsafeSize3840>.ToFullArray()
		{
			return ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly T[] ToArray<T>() where T : struct
		{
			return AsReadOnlySpan<T>().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly T[] IUnsafeSize<UnsafeSize3840>.ToFullArray<T>()
		{
			return ToArray<T>();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref *pointer;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(ReadOnlySpan<Byte> other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(UnsafeSize3840 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(in UnsafeSize3840 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly Int32 GetHashCode()
		{
			HashCode hash = new HashCode();
			hash.AddBytes(this);
			return hash.ToHashCode();
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other switch
			{
				null => IsEmpty,
				UnsafeSize3840 value => Equals(value),
				Memory<Byte> value => Equals(value.Span),
				ReadOnlyMemory<Byte> value => Equals(value.Span),
				_ => false
			};
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(ReadOnlySpan<Byte> other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(UnsafeSize3840 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(in UnsafeSize3840 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly String ToString()
		{
			return Convert.ToHexString(this);
		}
	}

	[Serializable]
	public unsafe struct UnsafeSize3968 : IUnsafeSize<UnsafeSize3968>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize3968 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize3968 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		public static Boolean operator ==(in UnsafeSize3968 first, ReadOnlySpan<Byte> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator ==(in UnsafeSize3968 first, in UnsafeSize3968 second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(in UnsafeSize3968 first, ReadOnlySpan<Byte> second)
		{
			return !(first == second);
		}

		public static Boolean operator !=(in UnsafeSize3968 first, in UnsafeSize3968 second)
		{
			return !(first == second);
		}

		public static Boolean operator >(in UnsafeSize3968 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >(in UnsafeSize3968 first, in UnsafeSize3968 second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >=(in UnsafeSize3968 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator >=(in UnsafeSize3968 first, in UnsafeSize3968 second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator <(in UnsafeSize3968 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <(in UnsafeSize3968 first, in UnsafeSize3968 second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <=(in UnsafeSize3968 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) <= 0;
		}

		public static Boolean operator <=(in UnsafeSize3968 first, in UnsafeSize3968 second)
		{
			return first.CompareTo(second) <= 0;
		}

		private const String Data = UnsafeSize.Data;
		public const Int32 Size = 3968;
		private fixed Byte Internal[Size];

		public readonly Type Type
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return typeof(UnsafeSize3968);
			}
		}

		ref UnsafeSize3968 IUnsafeSize<UnsafeSize3968>.This
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return ref this;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size;
			}
		}

		readonly Span<Byte> IUnsafeSize<UnsafeSize3968>.Full
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this;
			}
		}

		public readonly Boolean IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size <= 0 || AsReadOnlySpan().All(default(Byte));
			}
		}

		public UnsafeSize3968(ReadOnlySpan<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}.");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, value.Length);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal UnsafeSize3968(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal readonly void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue(Data, AsReadOnlySpan().ToArray(), typeof(Byte[]));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			GetObjectData(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ref UnsafeSize3968 Deserialize(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}

			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize3968 IUnsafeSize<UnsafeSize3968>.Deserialize(SerializationInfo info, StreamingContext context)
		{
			return ref Deserialize(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Support<T>() where T : struct
		{
			return Unsafe.SizeOf<T>() is > 0 and <= Size;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ref T As<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<T> AsReadOnlySpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<Byte> AsSpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<T> AsSpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new Span<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize3968 IUnsafeSize<UnsafeSize3968>.SetFull()
		{
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize3968 Fill(Byte value)
		{
			AsSpan().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize3968 Fill<T>(T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize3968 Fill<T>(in T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize3968 Clear()
		{
			return ref Reset();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize3968 Reset()
		{
			return ref Fill(default(Byte));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(Span<Byte> destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(ref UnsafeSize3968 destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(Span<Byte> destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(ref UnsafeSize3968 destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Byte[] ToArray()
		{
			return AsReadOnlySpan().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly Byte[] IUnsafeSize<UnsafeSize3968>.ToFullArray()
		{
			return ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly T[] ToArray<T>() where T : struct
		{
			return AsReadOnlySpan<T>().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly T[] IUnsafeSize<UnsafeSize3968>.ToFullArray<T>()
		{
			return ToArray<T>();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref *pointer;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(ReadOnlySpan<Byte> other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(UnsafeSize3968 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(in UnsafeSize3968 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly Int32 GetHashCode()
		{
			HashCode hash = new HashCode();
			hash.AddBytes(this);
			return hash.ToHashCode();
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other switch
			{
				null => IsEmpty,
				UnsafeSize3968 value => Equals(value),
				Memory<Byte> value => Equals(value.Span),
				ReadOnlyMemory<Byte> value => Equals(value.Span),
				_ => false
			};
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(ReadOnlySpan<Byte> other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(UnsafeSize3968 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(in UnsafeSize3968 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly String ToString()
		{
			return Convert.ToHexString(this);
		}
	}

	[Serializable]
	public unsafe struct UnsafeSize4096 : IUnsafeSize<UnsafeSize4096>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize4096 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize4096 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		public static Boolean operator ==(in UnsafeSize4096 first, ReadOnlySpan<Byte> second)
		{
			return first.Equals(second);
		}

		public static Boolean operator ==(in UnsafeSize4096 first, in UnsafeSize4096 second)
		{
			return first.Equals(second);
		}

		public static Boolean operator !=(in UnsafeSize4096 first, ReadOnlySpan<Byte> second)
		{
			return !(first == second);
		}

		public static Boolean operator !=(in UnsafeSize4096 first, in UnsafeSize4096 second)
		{
			return !(first == second);
		}

		public static Boolean operator >(in UnsafeSize4096 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >(in UnsafeSize4096 first, in UnsafeSize4096 second)
		{
			return first.CompareTo(second) > 0;
		}

		public static Boolean operator >=(in UnsafeSize4096 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator >=(in UnsafeSize4096 first, in UnsafeSize4096 second)
		{
			return first.CompareTo(second) >= 0;
		}

		public static Boolean operator <(in UnsafeSize4096 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <(in UnsafeSize4096 first, in UnsafeSize4096 second)
		{
			return first.CompareTo(second) < 0;
		}

		public static Boolean operator <=(in UnsafeSize4096 first, ReadOnlySpan<Byte> second)
		{
			return first.CompareTo(second) <= 0;
		}

		public static Boolean operator <=(in UnsafeSize4096 first, in UnsafeSize4096 second)
		{
			return first.CompareTo(second) <= 0;
		}

		private const String Data = UnsafeSize.Data;
		public const Int32 Size = 4096;
		private fixed Byte Internal[Size];

		public readonly Type Type
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return typeof(UnsafeSize4096);
			}
		}

		ref UnsafeSize4096 IUnsafeSize<UnsafeSize4096>.This
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return ref this;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size;
			}
		}

		readonly Span<Byte> IUnsafeSize<UnsafeSize4096>.Full
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this;
			}
		}

		public readonly Boolean IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Size <= 0 || AsReadOnlySpan().All(default(Byte));
			}
		}

		public UnsafeSize4096(ReadOnlySpan<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}.");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, value.Length);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal UnsafeSize4096(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal readonly void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue(Data, AsReadOnlySpan().ToArray(), typeof(Byte[]));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			GetObjectData(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal ref UnsafeSize4096 Deserialize(SerializationInfo info, StreamingContext context)
		{
			Byte[] data = info.GetValue(Data, typeof(Byte[])) as Byte[] ?? Array.Empty<Byte>();

			fixed (Byte* source = data)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, Math.Min(data.Length, Size));
			}

			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize4096 IUnsafeSize<UnsafeSize4096>.Deserialize(SerializationInfo info, StreamingContext context)
		{
			return ref Deserialize(info, context);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Support<T>() where T : struct
		{
			return Unsafe.SizeOf<T>() is > 0 and <= Size;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ref T As<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<T> AsReadOnlySpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new ReadOnlySpan<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<Byte> AsSpan()
		{
			fixed (Byte* pointer = Internal)
			{
				return new Span<Byte>(pointer, Size);
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<T> AsSpan<T>() where T : struct
		{
			if (!Support<T>())
			{
				throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
			}

			fixed (Byte* pointer = Internal)
			{
				return new Span<T>(pointer, Size / Unsafe.SizeOf<T>());
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		ref UnsafeSize4096 IUnsafeSize<UnsafeSize4096>.SetFull()
		{
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize4096 Fill(Byte value)
		{
			AsSpan().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize4096 Fill<T>(T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize4096 Fill<T>(in T value) where T : struct
		{
			AsSpan<T>().Fill(value);
			return ref this;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize4096 Clear()
		{
			return ref Reset();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref UnsafeSize4096 Reset()
		{
			return ref Fill(default(Byte));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(Span<Byte> destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly void CopyTo(ref UnsafeSize4096 destination)
		{
			AsReadOnlySpan().CopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(Span<Byte> destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean TryCopyTo(ref UnsafeSize4096 destination)
		{
			return AsReadOnlySpan().TryCopyTo(destination);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Byte[] ToArray()
		{
			return AsReadOnlySpan().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly Byte[] IUnsafeSize<UnsafeSize4096>.ToFullArray()
		{
			return ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly T[] ToArray<T>() where T : struct
		{
			return AsReadOnlySpan<T>().ToArray();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		readonly T[] IUnsafeSize<UnsafeSize4096>.ToFullArray<T>()
		{
			return ToArray<T>();
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref *pointer;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(ReadOnlySpan<Byte> other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(UnsafeSize4096 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Int32 CompareTo(in UnsafeSize4096 other)
		{
			return AsReadOnlySpan().SequenceCompareTo(other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly Int32 GetHashCode()
		{
			HashCode hash = new HashCode();
			hash.AddBytes(this);
			return hash.ToHashCode();
		}

		public override readonly Boolean Equals(Object? other)
		{
			return other switch
			{
				null => IsEmpty,
				UnsafeSize4096 value => Equals(value),
				Memory<Byte> value => Equals(value.Span),
				ReadOnlyMemory<Byte> value => Equals(value.Span),
				_ => false
			};
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(ReadOnlySpan<Byte> other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(UnsafeSize4096 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Boolean Equals(in UnsafeSize4096 other)
		{
			return MemoryExtensions.SequenceEqual(AsReadOnlySpan(), other);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override readonly String ToString()
		{
			return Convert.ToHexString(this);
		}
	}
}

#pragma warning restore CS0649
#pragma warning restore CS0169