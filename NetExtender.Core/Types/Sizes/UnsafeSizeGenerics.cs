// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using NetExtender.Types.Sizes.Interfaces;
using NetExtender.Utilities.Core;

// ReSharper disable MemberCanBePrivate.Global
#pragma warning disable CS0169
#pragma warning disable CS0649
namespace NetExtender.Types.Sizes
{
	public unsafe struct UnsafeSize8 : IUnsafeSize<UnsafeSize8>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize8 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, value.Count);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize8 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, value.Count);
			}
		}

		public const Int32 Size = 8;
		private fixed Byte Internal[Size];

		public readonly Int32 Length
		{
			get
			{
				return Size;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			get
			{
				return Count;
			}
		}

		private Int32 _count;
		public Int32 Count
		{
			readonly get
			{
				return _count;
			}
			set
			{
				if (value < 0 || value > Size)
				{
					throw new ArgumentOutOfRangeException(nameof(value), value, $"Count must be between 0 and {Size}");
				}

				_count = value;
			}
		}

		public readonly Boolean IsEmpty
		{
			get
			{
				return Count <= 0;
			}
		}

		public UnsafeSize8(Span<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, _count = value.Length);
			}
		}

		public readonly ref T As<T>()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			return this;
		}

		public readonly Span<Byte> AsSpan()
		{
			return this;
		}

		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = this)
			{
				return ref *pointer;
			}
		}
	}

	public unsafe struct UnsafeSize16 : IUnsafeSize<UnsafeSize16>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize16 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, value.Count);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize16 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, value.Count);
			}
		}

		public const Int32 Size = 16;
		private fixed Byte Internal[Size];

		public readonly Int32 Length
		{
			get
			{
				return Size;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			get
			{
				return Count;
			}
		}

		private Int32 _count;
		public Int32 Count
		{
			readonly get
			{
				return _count;
			}
			set
			{
				if (value < 0 || value > Size)
				{
					throw new ArgumentOutOfRangeException(nameof(value), value, $"Count must be between 0 and {Size}");
				}

				_count = value;
			}
		}

		public readonly Boolean IsEmpty
		{
			get
			{
				return Count <= 0;
			}
		}

		public UnsafeSize16(Span<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, _count = value.Length);
			}
		}

		public readonly ref T As<T>()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			return this;
		}

		public readonly Span<Byte> AsSpan()
		{
			return this;
		}

		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = this)
			{
				return ref *pointer;
			}
		}
	}

	public unsafe struct UnsafeSize24 : IUnsafeSize<UnsafeSize24>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize24 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, value.Count);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize24 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, value.Count);
			}
		}

		public const Int32 Size = 24;
		private fixed Byte Internal[Size];

		public readonly Int32 Length
		{
			get
			{
				return Size;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			get
			{
				return Count;
			}
		}

		private Int32 _count;
		public Int32 Count
		{
			readonly get
			{
				return _count;
			}
			set
			{
				if (value < 0 || value > Size)
				{
					throw new ArgumentOutOfRangeException(nameof(value), value, $"Count must be between 0 and {Size}");
				}

				_count = value;
			}
		}

		public readonly Boolean IsEmpty
		{
			get
			{
				return Count <= 0;
			}
		}

		public UnsafeSize24(Span<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, _count = value.Length);
			}
		}

		public readonly ref T As<T>()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			return this;
		}

		public readonly Span<Byte> AsSpan()
		{
			return this;
		}

		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = this)
			{
				return ref *pointer;
			}
		}
	}

	public unsafe struct UnsafeSize32 : IUnsafeSize<UnsafeSize32>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize32 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, value.Count);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize32 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, value.Count);
			}
		}

		public const Int32 Size = 32;
		private fixed Byte Internal[Size];

		public readonly Int32 Length
		{
			get
			{
				return Size;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			get
			{
				return Count;
			}
		}

		private Int32 _count;
		public Int32 Count
		{
			readonly get
			{
				return _count;
			}
			set
			{
				if (value < 0 || value > Size)
				{
					throw new ArgumentOutOfRangeException(nameof(value), value, $"Count must be between 0 and {Size}");
				}

				_count = value;
			}
		}

		public readonly Boolean IsEmpty
		{
			get
			{
				return Count <= 0;
			}
		}

		public UnsafeSize32(Span<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, _count = value.Length);
			}
		}

		public readonly ref T As<T>()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			return this;
		}

		public readonly Span<Byte> AsSpan()
		{
			return this;
		}

		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = this)
			{
				return ref *pointer;
			}
		}
	}

	public unsafe struct UnsafeSize40 : IUnsafeSize<UnsafeSize40>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize40 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, value.Count);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize40 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, value.Count);
			}
		}

		public const Int32 Size = 40;
		private fixed Byte Internal[Size];

		public readonly Int32 Length
		{
			get
			{
				return Size;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			get
			{
				return Count;
			}
		}

		private Int32 _count;
		public Int32 Count
		{
			readonly get
			{
				return _count;
			}
			set
			{
				if (value < 0 || value > Size)
				{
					throw new ArgumentOutOfRangeException(nameof(value), value, $"Count must be between 0 and {Size}");
				}

				_count = value;
			}
		}

		public readonly Boolean IsEmpty
		{
			get
			{
				return Count <= 0;
			}
		}

		public UnsafeSize40(Span<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, _count = value.Length);
			}
		}

		public readonly ref T As<T>()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			return this;
		}

		public readonly Span<Byte> AsSpan()
		{
			return this;
		}

		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = this)
			{
				return ref *pointer;
			}
		}
	}

	public unsafe struct UnsafeSize48 : IUnsafeSize<UnsafeSize48>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize48 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, value.Count);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize48 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, value.Count);
			}
		}

		public const Int32 Size = 48;
		private fixed Byte Internal[Size];

		public readonly Int32 Length
		{
			get
			{
				return Size;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			get
			{
				return Count;
			}
		}

		private Int32 _count;
		public Int32 Count
		{
			readonly get
			{
				return _count;
			}
			set
			{
				if (value < 0 || value > Size)
				{
					throw new ArgumentOutOfRangeException(nameof(value), value, $"Count must be between 0 and {Size}");
				}

				_count = value;
			}
		}

		public readonly Boolean IsEmpty
		{
			get
			{
				return Count <= 0;
			}
		}

		public UnsafeSize48(Span<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, _count = value.Length);
			}
		}

		public readonly ref T As<T>()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			return this;
		}

		public readonly Span<Byte> AsSpan()
		{
			return this;
		}

		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = this)
			{
				return ref *pointer;
			}
		}
	}

	public unsafe struct UnsafeSize56 : IUnsafeSize<UnsafeSize56>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize56 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, value.Count);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize56 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, value.Count);
			}
		}

		public const Int32 Size = 56;
		private fixed Byte Internal[Size];

		public readonly Int32 Length
		{
			get
			{
				return Size;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			get
			{
				return Count;
			}
		}

		private Int32 _count;
		public Int32 Count
		{
			readonly get
			{
				return _count;
			}
			set
			{
				if (value < 0 || value > Size)
				{
					throw new ArgumentOutOfRangeException(nameof(value), value, $"Count must be between 0 and {Size}");
				}

				_count = value;
			}
		}

		public readonly Boolean IsEmpty
		{
			get
			{
				return Count <= 0;
			}
		}

		public UnsafeSize56(Span<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, _count = value.Length);
			}
		}

		public readonly ref T As<T>()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			return this;
		}

		public readonly Span<Byte> AsSpan()
		{
			return this;
		}

		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = this)
			{
				return ref *pointer;
			}
		}
	}

	public unsafe struct UnsafeSize64 : IUnsafeSize<UnsafeSize64>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize64 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, value.Count);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize64 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, value.Count);
			}
		}

		public const Int32 Size = 64;
		private fixed Byte Internal[Size];

		public readonly Int32 Length
		{
			get
			{
				return Size;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			get
			{
				return Count;
			}
		}

		private Int32 _count;
		public Int32 Count
		{
			readonly get
			{
				return _count;
			}
			set
			{
				if (value < 0 || value > Size)
				{
					throw new ArgumentOutOfRangeException(nameof(value), value, $"Count must be between 0 and {Size}");
				}

				_count = value;
			}
		}

		public readonly Boolean IsEmpty
		{
			get
			{
				return Count <= 0;
			}
		}

		public UnsafeSize64(Span<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, _count = value.Length);
			}
		}

		public readonly ref T As<T>()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			return this;
		}

		public readonly Span<Byte> AsSpan()
		{
			return this;
		}

		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = this)
			{
				return ref *pointer;
			}
		}
	}

	public unsafe struct UnsafeSize80 : IUnsafeSize<UnsafeSize80>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize80 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, value.Count);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize80 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, value.Count);
			}
		}

		public const Int32 Size = 80;
		private fixed Byte Internal[Size];

		public readonly Int32 Length
		{
			get
			{
				return Size;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			get
			{
				return Count;
			}
		}

		private Int32 _count;
		public Int32 Count
		{
			readonly get
			{
				return _count;
			}
			set
			{
				if (value < 0 || value > Size)
				{
					throw new ArgumentOutOfRangeException(nameof(value), value, $"Count must be between 0 and {Size}");
				}

				_count = value;
			}
		}

		public readonly Boolean IsEmpty
		{
			get
			{
				return Count <= 0;
			}
		}

		public UnsafeSize80(Span<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, _count = value.Length);
			}
		}

		public readonly ref T As<T>()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			return this;
		}

		public readonly Span<Byte> AsSpan()
		{
			return this;
		}

		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = this)
			{
				return ref *pointer;
			}
		}
	}

	public unsafe struct UnsafeSize96 : IUnsafeSize<UnsafeSize96>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize96 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, value.Count);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize96 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, value.Count);
			}
		}

		public const Int32 Size = 96;
		private fixed Byte Internal[Size];

		public readonly Int32 Length
		{
			get
			{
				return Size;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			get
			{
				return Count;
			}
		}

		private Int32 _count;
		public Int32 Count
		{
			readonly get
			{
				return _count;
			}
			set
			{
				if (value < 0 || value > Size)
				{
					throw new ArgumentOutOfRangeException(nameof(value), value, $"Count must be between 0 and {Size}");
				}

				_count = value;
			}
		}

		public readonly Boolean IsEmpty
		{
			get
			{
				return Count <= 0;
			}
		}

		public UnsafeSize96(Span<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, _count = value.Length);
			}
		}

		public readonly ref T As<T>()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			return this;
		}

		public readonly Span<Byte> AsSpan()
		{
			return this;
		}

		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = this)
			{
				return ref *pointer;
			}
		}
	}

	public unsafe struct UnsafeSize112 : IUnsafeSize<UnsafeSize112>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize112 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, value.Count);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize112 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, value.Count);
			}
		}

		public const Int32 Size = 112;
		private fixed Byte Internal[Size];

		public readonly Int32 Length
		{
			get
			{
				return Size;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			get
			{
				return Count;
			}
		}

		private Int32 _count;
		public Int32 Count
		{
			readonly get
			{
				return _count;
			}
			set
			{
				if (value < 0 || value > Size)
				{
					throw new ArgumentOutOfRangeException(nameof(value), value, $"Count must be between 0 and {Size}");
				}

				_count = value;
			}
		}

		public readonly Boolean IsEmpty
		{
			get
			{
				return Count <= 0;
			}
		}

		public UnsafeSize112(Span<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, _count = value.Length);
			}
		}

		public readonly ref T As<T>()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			return this;
		}

		public readonly Span<Byte> AsSpan()
		{
			return this;
		}

		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = this)
			{
				return ref *pointer;
			}
		}
	}

	public unsafe struct UnsafeSize128 : IUnsafeSize<UnsafeSize128>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize128 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, value.Count);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize128 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, value.Count);
			}
		}

		public const Int32 Size = 128;
		private fixed Byte Internal[Size];

		public readonly Int32 Length
		{
			get
			{
				return Size;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			get
			{
				return Count;
			}
		}

		private Int32 _count;
		public Int32 Count
		{
			readonly get
			{
				return _count;
			}
			set
			{
				if (value < 0 || value > Size)
				{
					throw new ArgumentOutOfRangeException(nameof(value), value, $"Count must be between 0 and {Size}");
				}

				_count = value;
			}
		}

		public readonly Boolean IsEmpty
		{
			get
			{
				return Count <= 0;
			}
		}

		public UnsafeSize128(Span<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, _count = value.Length);
			}
		}

		public readonly ref T As<T>()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			return this;
		}

		public readonly Span<Byte> AsSpan()
		{
			return this;
		}

		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = this)
			{
				return ref *pointer;
			}
		}
	}

	public unsafe struct UnsafeSize144 : IUnsafeSize<UnsafeSize144>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize144 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, value.Count);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize144 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, value.Count);
			}
		}

		public const Int32 Size = 144;
		private fixed Byte Internal[Size];

		public readonly Int32 Length
		{
			get
			{
				return Size;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			get
			{
				return Count;
			}
		}

		private Int32 _count;
		public Int32 Count
		{
			readonly get
			{
				return _count;
			}
			set
			{
				if (value < 0 || value > Size)
				{
					throw new ArgumentOutOfRangeException(nameof(value), value, $"Count must be between 0 and {Size}");
				}

				_count = value;
			}
		}

		public readonly Boolean IsEmpty
		{
			get
			{
				return Count <= 0;
			}
		}

		public UnsafeSize144(Span<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, _count = value.Length);
			}
		}

		public readonly ref T As<T>()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			return this;
		}

		public readonly Span<Byte> AsSpan()
		{
			return this;
		}

		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = this)
			{
				return ref *pointer;
			}
		}
	}

	public unsafe struct UnsafeSize160 : IUnsafeSize<UnsafeSize160>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize160 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, value.Count);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize160 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, value.Count);
			}
		}

		public const Int32 Size = 160;
		private fixed Byte Internal[Size];

		public readonly Int32 Length
		{
			get
			{
				return Size;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			get
			{
				return Count;
			}
		}

		private Int32 _count;
		public Int32 Count
		{
			readonly get
			{
				return _count;
			}
			set
			{
				if (value < 0 || value > Size)
				{
					throw new ArgumentOutOfRangeException(nameof(value), value, $"Count must be between 0 and {Size}");
				}

				_count = value;
			}
		}

		public readonly Boolean IsEmpty
		{
			get
			{
				return Count <= 0;
			}
		}

		public UnsafeSize160(Span<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, _count = value.Length);
			}
		}

		public readonly ref T As<T>()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			return this;
		}

		public readonly Span<Byte> AsSpan()
		{
			return this;
		}

		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = this)
			{
				return ref *pointer;
			}
		}
	}

	public unsafe struct UnsafeSize176 : IUnsafeSize<UnsafeSize176>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize176 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, value.Count);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize176 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, value.Count);
			}
		}

		public const Int32 Size = 176;
		private fixed Byte Internal[Size];

		public readonly Int32 Length
		{
			get
			{
				return Size;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			get
			{
				return Count;
			}
		}

		private Int32 _count;
		public Int32 Count
		{
			readonly get
			{
				return _count;
			}
			set
			{
				if (value < 0 || value > Size)
				{
					throw new ArgumentOutOfRangeException(nameof(value), value, $"Count must be between 0 and {Size}");
				}

				_count = value;
			}
		}

		public readonly Boolean IsEmpty
		{
			get
			{
				return Count <= 0;
			}
		}

		public UnsafeSize176(Span<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, _count = value.Length);
			}
		}

		public readonly ref T As<T>()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			return this;
		}

		public readonly Span<Byte> AsSpan()
		{
			return this;
		}

		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = this)
			{
				return ref *pointer;
			}
		}
	}

	public unsafe struct UnsafeSize192 : IUnsafeSize<UnsafeSize192>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize192 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, value.Count);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize192 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, value.Count);
			}
		}

		public const Int32 Size = 192;
		private fixed Byte Internal[Size];

		public readonly Int32 Length
		{
			get
			{
				return Size;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			get
			{
				return Count;
			}
		}

		private Int32 _count;
		public Int32 Count
		{
			readonly get
			{
				return _count;
			}
			set
			{
				if (value < 0 || value > Size)
				{
					throw new ArgumentOutOfRangeException(nameof(value), value, $"Count must be between 0 and {Size}");
				}

				_count = value;
			}
		}

		public readonly Boolean IsEmpty
		{
			get
			{
				return Count <= 0;
			}
		}

		public UnsafeSize192(Span<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, _count = value.Length);
			}
		}

		public readonly ref T As<T>()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			return this;
		}

		public readonly Span<Byte> AsSpan()
		{
			return this;
		}

		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = this)
			{
				return ref *pointer;
			}
		}
	}

	public unsafe struct UnsafeSize208 : IUnsafeSize<UnsafeSize208>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize208 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, value.Count);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize208 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, value.Count);
			}
		}

		public const Int32 Size = 208;
		private fixed Byte Internal[Size];

		public readonly Int32 Length
		{
			get
			{
				return Size;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			get
			{
				return Count;
			}
		}

		private Int32 _count;
		public Int32 Count
		{
			readonly get
			{
				return _count;
			}
			set
			{
				if (value < 0 || value > Size)
				{
					throw new ArgumentOutOfRangeException(nameof(value), value, $"Count must be between 0 and {Size}");
				}

				_count = value;
			}
		}

		public readonly Boolean IsEmpty
		{
			get
			{
				return Count <= 0;
			}
		}

		public UnsafeSize208(Span<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, _count = value.Length);
			}
		}

		public readonly ref T As<T>()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			return this;
		}

		public readonly Span<Byte> AsSpan()
		{
			return this;
		}

		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = this)
			{
				return ref *pointer;
			}
		}
	}

	public unsafe struct UnsafeSize224 : IUnsafeSize<UnsafeSize224>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize224 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, value.Count);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize224 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, value.Count);
			}
		}

		public const Int32 Size = 224;
		private fixed Byte Internal[Size];

		public readonly Int32 Length
		{
			get
			{
				return Size;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			get
			{
				return Count;
			}
		}

		private Int32 _count;
		public Int32 Count
		{
			readonly get
			{
				return _count;
			}
			set
			{
				if (value < 0 || value > Size)
				{
					throw new ArgumentOutOfRangeException(nameof(value), value, $"Count must be between 0 and {Size}");
				}

				_count = value;
			}
		}

		public readonly Boolean IsEmpty
		{
			get
			{
				return Count <= 0;
			}
		}

		public UnsafeSize224(Span<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, _count = value.Length);
			}
		}

		public readonly ref T As<T>()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			return this;
		}

		public readonly Span<Byte> AsSpan()
		{
			return this;
		}

		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = this)
			{
				return ref *pointer;
			}
		}
	}

	public unsafe struct UnsafeSize240 : IUnsafeSize<UnsafeSize240>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize240 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, value.Count);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize240 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, value.Count);
			}
		}

		public const Int32 Size = 240;
		private fixed Byte Internal[Size];

		public readonly Int32 Length
		{
			get
			{
				return Size;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			get
			{
				return Count;
			}
		}

		private Int32 _count;
		public Int32 Count
		{
			readonly get
			{
				return _count;
			}
			set
			{
				if (value < 0 || value > Size)
				{
					throw new ArgumentOutOfRangeException(nameof(value), value, $"Count must be between 0 and {Size}");
				}

				_count = value;
			}
		}

		public readonly Boolean IsEmpty
		{
			get
			{
				return Count <= 0;
			}
		}

		public UnsafeSize240(Span<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, _count = value.Length);
			}
		}

		public readonly ref T As<T>()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			return this;
		}

		public readonly Span<Byte> AsSpan()
		{
			return this;
		}

		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = this)
			{
				return ref *pointer;
			}
		}
	}

	public unsafe struct UnsafeSize256 : IUnsafeSize<UnsafeSize256>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize256 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, value.Count);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize256 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, value.Count);
			}
		}

		public const Int32 Size = 256;
		private fixed Byte Internal[Size];

		public readonly Int32 Length
		{
			get
			{
				return Size;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			get
			{
				return Count;
			}
		}

		private Int32 _count;
		public Int32 Count
		{
			readonly get
			{
				return _count;
			}
			set
			{
				if (value < 0 || value > Size)
				{
					throw new ArgumentOutOfRangeException(nameof(value), value, $"Count must be between 0 and {Size}");
				}

				_count = value;
			}
		}

		public readonly Boolean IsEmpty
		{
			get
			{
				return Count <= 0;
			}
		}

		public UnsafeSize256(Span<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, _count = value.Length);
			}
		}

		public readonly ref T As<T>()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			return this;
		}

		public readonly Span<Byte> AsSpan()
		{
			return this;
		}

		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = this)
			{
				return ref *pointer;
			}
		}
	}

	public unsafe struct UnsafeSize288 : IUnsafeSize<UnsafeSize288>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize288 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, value.Count);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize288 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, value.Count);
			}
		}

		public const Int32 Size = 288;
		private fixed Byte Internal[Size];

		public readonly Int32 Length
		{
			get
			{
				return Size;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			get
			{
				return Count;
			}
		}

		private Int32 _count;
		public Int32 Count
		{
			readonly get
			{
				return _count;
			}
			set
			{
				if (value < 0 || value > Size)
				{
					throw new ArgumentOutOfRangeException(nameof(value), value, $"Count must be between 0 and {Size}");
				}

				_count = value;
			}
		}

		public readonly Boolean IsEmpty
		{
			get
			{
				return Count <= 0;
			}
		}

		public UnsafeSize288(Span<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, _count = value.Length);
			}
		}

		public readonly ref T As<T>()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			return this;
		}

		public readonly Span<Byte> AsSpan()
		{
			return this;
		}

		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = this)
			{
				return ref *pointer;
			}
		}
	}

	public unsafe struct UnsafeSize320 : IUnsafeSize<UnsafeSize320>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize320 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, value.Count);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize320 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, value.Count);
			}
		}

		public const Int32 Size = 320;
		private fixed Byte Internal[Size];

		public readonly Int32 Length
		{
			get
			{
				return Size;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			get
			{
				return Count;
			}
		}

		private Int32 _count;
		public Int32 Count
		{
			readonly get
			{
				return _count;
			}
			set
			{
				if (value < 0 || value > Size)
				{
					throw new ArgumentOutOfRangeException(nameof(value), value, $"Count must be between 0 and {Size}");
				}

				_count = value;
			}
		}

		public readonly Boolean IsEmpty
		{
			get
			{
				return Count <= 0;
			}
		}

		public UnsafeSize320(Span<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, _count = value.Length);
			}
		}

		public readonly ref T As<T>()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			return this;
		}

		public readonly Span<Byte> AsSpan()
		{
			return this;
		}

		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = this)
			{
				return ref *pointer;
			}
		}
	}

	public unsafe struct UnsafeSize352 : IUnsafeSize<UnsafeSize352>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize352 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, value.Count);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize352 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, value.Count);
			}
		}

		public const Int32 Size = 352;
		private fixed Byte Internal[Size];

		public readonly Int32 Length
		{
			get
			{
				return Size;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			get
			{
				return Count;
			}
		}

		private Int32 _count;
		public Int32 Count
		{
			readonly get
			{
				return _count;
			}
			set
			{
				if (value < 0 || value > Size)
				{
					throw new ArgumentOutOfRangeException(nameof(value), value, $"Count must be between 0 and {Size}");
				}

				_count = value;
			}
		}

		public readonly Boolean IsEmpty
		{
			get
			{
				return Count <= 0;
			}
		}

		public UnsafeSize352(Span<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, _count = value.Length);
			}
		}

		public readonly ref T As<T>()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			return this;
		}

		public readonly Span<Byte> AsSpan()
		{
			return this;
		}

		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = this)
			{
				return ref *pointer;
			}
		}
	}

	public unsafe struct UnsafeSize384 : IUnsafeSize<UnsafeSize384>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize384 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, value.Count);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize384 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, value.Count);
			}
		}

		public const Int32 Size = 384;
		private fixed Byte Internal[Size];

		public readonly Int32 Length
		{
			get
			{
				return Size;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			get
			{
				return Count;
			}
		}

		private Int32 _count;
		public Int32 Count
		{
			readonly get
			{
				return _count;
			}
			set
			{
				if (value < 0 || value > Size)
				{
					throw new ArgumentOutOfRangeException(nameof(value), value, $"Count must be between 0 and {Size}");
				}

				_count = value;
			}
		}

		public readonly Boolean IsEmpty
		{
			get
			{
				return Count <= 0;
			}
		}

		public UnsafeSize384(Span<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, _count = value.Length);
			}
		}

		public readonly ref T As<T>()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			return this;
		}

		public readonly Span<Byte> AsSpan()
		{
			return this;
		}

		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = this)
			{
				return ref *pointer;
			}
		}
	}

	public unsafe struct UnsafeSize416 : IUnsafeSize<UnsafeSize416>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize416 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, value.Count);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize416 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, value.Count);
			}
		}

		public const Int32 Size = 416;
		private fixed Byte Internal[Size];

		public readonly Int32 Length
		{
			get
			{
				return Size;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			get
			{
				return Count;
			}
		}

		private Int32 _count;
		public Int32 Count
		{
			readonly get
			{
				return _count;
			}
			set
			{
				if (value < 0 || value > Size)
				{
					throw new ArgumentOutOfRangeException(nameof(value), value, $"Count must be between 0 and {Size}");
				}

				_count = value;
			}
		}

		public readonly Boolean IsEmpty
		{
			get
			{
				return Count <= 0;
			}
		}

		public UnsafeSize416(Span<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, _count = value.Length);
			}
		}

		public readonly ref T As<T>()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			return this;
		}

		public readonly Span<Byte> AsSpan()
		{
			return this;
		}

		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = this)
			{
				return ref *pointer;
			}
		}
	}

	public unsafe struct UnsafeSize448 : IUnsafeSize<UnsafeSize448>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize448 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, value.Count);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize448 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, value.Count);
			}
		}

		public const Int32 Size = 448;
		private fixed Byte Internal[Size];

		public readonly Int32 Length
		{
			get
			{
				return Size;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			get
			{
				return Count;
			}
		}

		private Int32 _count;
		public Int32 Count
		{
			readonly get
			{
				return _count;
			}
			set
			{
				if (value < 0 || value > Size)
				{
					throw new ArgumentOutOfRangeException(nameof(value), value, $"Count must be between 0 and {Size}");
				}

				_count = value;
			}
		}

		public readonly Boolean IsEmpty
		{
			get
			{
				return Count <= 0;
			}
		}

		public UnsafeSize448(Span<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, _count = value.Length);
			}
		}

		public readonly ref T As<T>()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			return this;
		}

		public readonly Span<Byte> AsSpan()
		{
			return this;
		}

		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = this)
			{
				return ref *pointer;
			}
		}
	}

	public unsafe struct UnsafeSize480 : IUnsafeSize<UnsafeSize480>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize480 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, value.Count);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize480 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, value.Count);
			}
		}

		public const Int32 Size = 480;
		private fixed Byte Internal[Size];

		public readonly Int32 Length
		{
			get
			{
				return Size;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			get
			{
				return Count;
			}
		}

		private Int32 _count;
		public Int32 Count
		{
			readonly get
			{
				return _count;
			}
			set
			{
				if (value < 0 || value > Size)
				{
					throw new ArgumentOutOfRangeException(nameof(value), value, $"Count must be between 0 and {Size}");
				}

				_count = value;
			}
		}

		public readonly Boolean IsEmpty
		{
			get
			{
				return Count <= 0;
			}
		}

		public UnsafeSize480(Span<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, _count = value.Length);
			}
		}

		public readonly ref T As<T>()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			return this;
		}

		public readonly Span<Byte> AsSpan()
		{
			return this;
		}

		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = this)
			{
				return ref *pointer;
			}
		}
	}

	public unsafe struct UnsafeSize512 : IUnsafeSize<UnsafeSize512>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize512 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, value.Count);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize512 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, value.Count);
			}
		}

		public const Int32 Size = 512;
		private fixed Byte Internal[Size];

		public readonly Int32 Length
		{
			get
			{
				return Size;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			get
			{
				return Count;
			}
		}

		private Int32 _count;
		public Int32 Count
		{
			readonly get
			{
				return _count;
			}
			set
			{
				if (value < 0 || value > Size)
				{
					throw new ArgumentOutOfRangeException(nameof(value), value, $"Count must be between 0 and {Size}");
				}

				_count = value;
			}
		}

		public readonly Boolean IsEmpty
		{
			get
			{
				return Count <= 0;
			}
		}

		public UnsafeSize512(Span<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, _count = value.Length);
			}
		}

		public readonly ref T As<T>()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			return this;
		}

		public readonly Span<Byte> AsSpan()
		{
			return this;
		}

		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = this)
			{
				return ref *pointer;
			}
		}
	}

	public unsafe struct UnsafeSize576 : IUnsafeSize<UnsafeSize576>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize576 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, value.Count);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize576 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, value.Count);
			}
		}

		public const Int32 Size = 576;
		private fixed Byte Internal[Size];

		public readonly Int32 Length
		{
			get
			{
				return Size;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			get
			{
				return Count;
			}
		}

		private Int32 _count;
		public Int32 Count
		{
			readonly get
			{
				return _count;
			}
			set
			{
				if (value < 0 || value > Size)
				{
					throw new ArgumentOutOfRangeException(nameof(value), value, $"Count must be between 0 and {Size}");
				}

				_count = value;
			}
		}

		public readonly Boolean IsEmpty
		{
			get
			{
				return Count <= 0;
			}
		}

		public UnsafeSize576(Span<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, _count = value.Length);
			}
		}

		public readonly ref T As<T>()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			return this;
		}

		public readonly Span<Byte> AsSpan()
		{
			return this;
		}

		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = this)
			{
				return ref *pointer;
			}
		}
	}

	public unsafe struct UnsafeSize640 : IUnsafeSize<UnsafeSize640>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize640 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, value.Count);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize640 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, value.Count);
			}
		}

		public const Int32 Size = 640;
		private fixed Byte Internal[Size];

		public readonly Int32 Length
		{
			get
			{
				return Size;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			get
			{
				return Count;
			}
		}

		private Int32 _count;
		public Int32 Count
		{
			readonly get
			{
				return _count;
			}
			set
			{
				if (value < 0 || value > Size)
				{
					throw new ArgumentOutOfRangeException(nameof(value), value, $"Count must be between 0 and {Size}");
				}

				_count = value;
			}
		}

		public readonly Boolean IsEmpty
		{
			get
			{
				return Count <= 0;
			}
		}

		public UnsafeSize640(Span<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, _count = value.Length);
			}
		}

		public readonly ref T As<T>()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			return this;
		}

		public readonly Span<Byte> AsSpan()
		{
			return this;
		}

		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = this)
			{
				return ref *pointer;
			}
		}
	}

	public unsafe struct UnsafeSize704 : IUnsafeSize<UnsafeSize704>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize704 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, value.Count);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize704 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, value.Count);
			}
		}

		public const Int32 Size = 704;
		private fixed Byte Internal[Size];

		public readonly Int32 Length
		{
			get
			{
				return Size;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			get
			{
				return Count;
			}
		}

		private Int32 _count;
		public Int32 Count
		{
			readonly get
			{
				return _count;
			}
			set
			{
				if (value < 0 || value > Size)
				{
					throw new ArgumentOutOfRangeException(nameof(value), value, $"Count must be between 0 and {Size}");
				}

				_count = value;
			}
		}

		public readonly Boolean IsEmpty
		{
			get
			{
				return Count <= 0;
			}
		}

		public UnsafeSize704(Span<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, _count = value.Length);
			}
		}

		public readonly ref T As<T>()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			return this;
		}

		public readonly Span<Byte> AsSpan()
		{
			return this;
		}

		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = this)
			{
				return ref *pointer;
			}
		}
	}

	public unsafe struct UnsafeSize768 : IUnsafeSize<UnsafeSize768>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize768 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, value.Count);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize768 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, value.Count);
			}
		}

		public const Int32 Size = 768;
		private fixed Byte Internal[Size];

		public readonly Int32 Length
		{
			get
			{
				return Size;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			get
			{
				return Count;
			}
		}

		private Int32 _count;
		public Int32 Count
		{
			readonly get
			{
				return _count;
			}
			set
			{
				if (value < 0 || value > Size)
				{
					throw new ArgumentOutOfRangeException(nameof(value), value, $"Count must be between 0 and {Size}");
				}

				_count = value;
			}
		}

		public readonly Boolean IsEmpty
		{
			get
			{
				return Count <= 0;
			}
		}

		public UnsafeSize768(Span<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, _count = value.Length);
			}
		}

		public readonly ref T As<T>()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			return this;
		}

		public readonly Span<Byte> AsSpan()
		{
			return this;
		}

		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = this)
			{
				return ref *pointer;
			}
		}
	}

	public unsafe struct UnsafeSize832 : IUnsafeSize<UnsafeSize832>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize832 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, value.Count);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize832 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, value.Count);
			}
		}

		public const Int32 Size = 832;
		private fixed Byte Internal[Size];

		public readonly Int32 Length
		{
			get
			{
				return Size;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			get
			{
				return Count;
			}
		}

		private Int32 _count;
		public Int32 Count
		{
			readonly get
			{
				return _count;
			}
			set
			{
				if (value < 0 || value > Size)
				{
					throw new ArgumentOutOfRangeException(nameof(value), value, $"Count must be between 0 and {Size}");
				}

				_count = value;
			}
		}

		public readonly Boolean IsEmpty
		{
			get
			{
				return Count <= 0;
			}
		}

		public UnsafeSize832(Span<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, _count = value.Length);
			}
		}

		public readonly ref T As<T>()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			return this;
		}

		public readonly Span<Byte> AsSpan()
		{
			return this;
		}

		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = this)
			{
				return ref *pointer;
			}
		}
	}

	public unsafe struct UnsafeSize896 : IUnsafeSize<UnsafeSize896>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize896 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, value.Count);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize896 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, value.Count);
			}
		}

		public const Int32 Size = 896;
		private fixed Byte Internal[Size];

		public readonly Int32 Length
		{
			get
			{
				return Size;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			get
			{
				return Count;
			}
		}

		private Int32 _count;
		public Int32 Count
		{
			readonly get
			{
				return _count;
			}
			set
			{
				if (value < 0 || value > Size)
				{
					throw new ArgumentOutOfRangeException(nameof(value), value, $"Count must be between 0 and {Size}");
				}

				_count = value;
			}
		}

		public readonly Boolean IsEmpty
		{
			get
			{
				return Count <= 0;
			}
		}

		public UnsafeSize896(Span<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, _count = value.Length);
			}
		}

		public readonly ref T As<T>()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			return this;
		}

		public readonly Span<Byte> AsSpan()
		{
			return this;
		}

		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = this)
			{
				return ref *pointer;
			}
		}
	}

	public unsafe struct UnsafeSize960 : IUnsafeSize<UnsafeSize960>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize960 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, value.Count);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize960 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, value.Count);
			}
		}

		public const Int32 Size = 960;
		private fixed Byte Internal[Size];

		public readonly Int32 Length
		{
			get
			{
				return Size;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			get
			{
				return Count;
			}
		}

		private Int32 _count;
		public Int32 Count
		{
			readonly get
			{
				return _count;
			}
			set
			{
				if (value < 0 || value > Size)
				{
					throw new ArgumentOutOfRangeException(nameof(value), value, $"Count must be between 0 and {Size}");
				}

				_count = value;
			}
		}

		public readonly Boolean IsEmpty
		{
			get
			{
				return Count <= 0;
			}
		}

		public UnsafeSize960(Span<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, _count = value.Length);
			}
		}

		public readonly ref T As<T>()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			return this;
		}

		public readonly Span<Byte> AsSpan()
		{
			return this;
		}

		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = this)
			{
				return ref *pointer;
			}
		}
	}

	public unsafe struct UnsafeSize1024 : IUnsafeSize<UnsafeSize1024>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize1024 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, value.Count);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize1024 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, value.Count);
			}
		}

		public const Int32 Size = 1024;
		private fixed Byte Internal[Size];

		public readonly Int32 Length
		{
			get
			{
				return Size;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			get
			{
				return Count;
			}
		}

		private Int32 _count;
		public Int32 Count
		{
			readonly get
			{
				return _count;
			}
			set
			{
				if (value < 0 || value > Size)
				{
					throw new ArgumentOutOfRangeException(nameof(value), value, $"Count must be between 0 and {Size}");
				}

				_count = value;
			}
		}

		public readonly Boolean IsEmpty
		{
			get
			{
				return Count <= 0;
			}
		}

		public UnsafeSize1024(Span<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, _count = value.Length);
			}
		}

		public readonly ref T As<T>()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			return this;
		}

		public readonly Span<Byte> AsSpan()
		{
			return this;
		}

		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = this)
			{
				return ref *pointer;
			}
		}
	}

	public unsafe struct UnsafeSize1152 : IUnsafeSize<UnsafeSize1152>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize1152 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, value.Count);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize1152 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, value.Count);
			}
		}

		public const Int32 Size = 1152;
		private fixed Byte Internal[Size];

		public readonly Int32 Length
		{
			get
			{
				return Size;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			get
			{
				return Count;
			}
		}

		private Int32 _count;
		public Int32 Count
		{
			readonly get
			{
				return _count;
			}
			set
			{
				if (value < 0 || value > Size)
				{
					throw new ArgumentOutOfRangeException(nameof(value), value, $"Count must be between 0 and {Size}");
				}

				_count = value;
			}
		}

		public readonly Boolean IsEmpty
		{
			get
			{
				return Count <= 0;
			}
		}

		public UnsafeSize1152(Span<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, _count = value.Length);
			}
		}

		public readonly ref T As<T>()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			return this;
		}

		public readonly Span<Byte> AsSpan()
		{
			return this;
		}

		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = this)
			{
				return ref *pointer;
			}
		}
	}

	public unsafe struct UnsafeSize1280 : IUnsafeSize<UnsafeSize1280>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize1280 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, value.Count);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize1280 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, value.Count);
			}
		}

		public const Int32 Size = 1280;
		private fixed Byte Internal[Size];

		public readonly Int32 Length
		{
			get
			{
				return Size;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			get
			{
				return Count;
			}
		}

		private Int32 _count;
		public Int32 Count
		{
			readonly get
			{
				return _count;
			}
			set
			{
				if (value < 0 || value > Size)
				{
					throw new ArgumentOutOfRangeException(nameof(value), value, $"Count must be between 0 and {Size}");
				}

				_count = value;
			}
		}

		public readonly Boolean IsEmpty
		{
			get
			{
				return Count <= 0;
			}
		}

		public UnsafeSize1280(Span<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, _count = value.Length);
			}
		}

		public readonly ref T As<T>()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			return this;
		}

		public readonly Span<Byte> AsSpan()
		{
			return this;
		}

		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = this)
			{
				return ref *pointer;
			}
		}
	}

	public unsafe struct UnsafeSize1408 : IUnsafeSize<UnsafeSize1408>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize1408 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, value.Count);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize1408 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, value.Count);
			}
		}

		public const Int32 Size = 1408;
		private fixed Byte Internal[Size];

		public readonly Int32 Length
		{
			get
			{
				return Size;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			get
			{
				return Count;
			}
		}

		private Int32 _count;
		public Int32 Count
		{
			readonly get
			{
				return _count;
			}
			set
			{
				if (value < 0 || value > Size)
				{
					throw new ArgumentOutOfRangeException(nameof(value), value, $"Count must be between 0 and {Size}");
				}

				_count = value;
			}
		}

		public readonly Boolean IsEmpty
		{
			get
			{
				return Count <= 0;
			}
		}

		public UnsafeSize1408(Span<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, _count = value.Length);
			}
		}

		public readonly ref T As<T>()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			return this;
		}

		public readonly Span<Byte> AsSpan()
		{
			return this;
		}

		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = this)
			{
				return ref *pointer;
			}
		}
	}

	public unsafe struct UnsafeSize1536 : IUnsafeSize<UnsafeSize1536>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize1536 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, value.Count);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize1536 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, value.Count);
			}
		}

		public const Int32 Size = 1536;
		private fixed Byte Internal[Size];

		public readonly Int32 Length
		{
			get
			{
				return Size;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			get
			{
				return Count;
			}
		}

		private Int32 _count;
		public Int32 Count
		{
			readonly get
			{
				return _count;
			}
			set
			{
				if (value < 0 || value > Size)
				{
					throw new ArgumentOutOfRangeException(nameof(value), value, $"Count must be between 0 and {Size}");
				}

				_count = value;
			}
		}

		public readonly Boolean IsEmpty
		{
			get
			{
				return Count <= 0;
			}
		}

		public UnsafeSize1536(Span<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, _count = value.Length);
			}
		}

		public readonly ref T As<T>()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			return this;
		}

		public readonly Span<Byte> AsSpan()
		{
			return this;
		}

		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = this)
			{
				return ref *pointer;
			}
		}
	}

	public unsafe struct UnsafeSize1664 : IUnsafeSize<UnsafeSize1664>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize1664 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, value.Count);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize1664 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, value.Count);
			}
		}

		public const Int32 Size = 1664;
		private fixed Byte Internal[Size];

		public readonly Int32 Length
		{
			get
			{
				return Size;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			get
			{
				return Count;
			}
		}

		private Int32 _count;
		public Int32 Count
		{
			readonly get
			{
				return _count;
			}
			set
			{
				if (value < 0 || value > Size)
				{
					throw new ArgumentOutOfRangeException(nameof(value), value, $"Count must be between 0 and {Size}");
				}

				_count = value;
			}
		}

		public readonly Boolean IsEmpty
		{
			get
			{
				return Count <= 0;
			}
		}

		public UnsafeSize1664(Span<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, _count = value.Length);
			}
		}

		public readonly ref T As<T>()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			return this;
		}

		public readonly Span<Byte> AsSpan()
		{
			return this;
		}

		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = this)
			{
				return ref *pointer;
			}
		}
	}

	public unsafe struct UnsafeSize1792 : IUnsafeSize<UnsafeSize1792>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize1792 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, value.Count);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize1792 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, value.Count);
			}
		}

		public const Int32 Size = 1792;
		private fixed Byte Internal[Size];

		public readonly Int32 Length
		{
			get
			{
				return Size;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			get
			{
				return Count;
			}
		}

		private Int32 _count;
		public Int32 Count
		{
			readonly get
			{
				return _count;
			}
			set
			{
				if (value < 0 || value > Size)
				{
					throw new ArgumentOutOfRangeException(nameof(value), value, $"Count must be between 0 and {Size}");
				}

				_count = value;
			}
		}

		public readonly Boolean IsEmpty
		{
			get
			{
				return Count <= 0;
			}
		}

		public UnsafeSize1792(Span<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, _count = value.Length);
			}
		}

		public readonly ref T As<T>()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			return this;
		}

		public readonly Span<Byte> AsSpan()
		{
			return this;
		}

		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = this)
			{
				return ref *pointer;
			}
		}
	}

	public unsafe struct UnsafeSize1920 : IUnsafeSize<UnsafeSize1920>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize1920 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, value.Count);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize1920 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, value.Count);
			}
		}

		public const Int32 Size = 1920;
		private fixed Byte Internal[Size];

		public readonly Int32 Length
		{
			get
			{
				return Size;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			get
			{
				return Count;
			}
		}

		private Int32 _count;
		public Int32 Count
		{
			readonly get
			{
				return _count;
			}
			set
			{
				if (value < 0 || value > Size)
				{
					throw new ArgumentOutOfRangeException(nameof(value), value, $"Count must be between 0 and {Size}");
				}

				_count = value;
			}
		}

		public readonly Boolean IsEmpty
		{
			get
			{
				return Count <= 0;
			}
		}

		public UnsafeSize1920(Span<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, _count = value.Length);
			}
		}

		public readonly ref T As<T>()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			return this;
		}

		public readonly Span<Byte> AsSpan()
		{
			return this;
		}

		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = this)
			{
				return ref *pointer;
			}
		}
	}

	public unsafe struct UnsafeSize2048 : IUnsafeSize<UnsafeSize2048>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize2048 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, value.Count);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize2048 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, value.Count);
			}
		}

		public const Int32 Size = 2048;
		private fixed Byte Internal[Size];

		public readonly Int32 Length
		{
			get
			{
				return Size;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			get
			{
				return Count;
			}
		}

		private Int32 _count;
		public Int32 Count
		{
			readonly get
			{
				return _count;
			}
			set
			{
				if (value < 0 || value > Size)
				{
					throw new ArgumentOutOfRangeException(nameof(value), value, $"Count must be between 0 and {Size}");
				}

				_count = value;
			}
		}

		public readonly Boolean IsEmpty
		{
			get
			{
				return Count <= 0;
			}
		}

		public UnsafeSize2048(Span<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, _count = value.Length);
			}
		}

		public readonly ref T As<T>()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			return this;
		}

		public readonly Span<Byte> AsSpan()
		{
			return this;
		}

		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = this)
			{
				return ref *pointer;
			}
		}
	}

	public unsafe struct UnsafeSize2176 : IUnsafeSize<UnsafeSize2176>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize2176 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, value.Count);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize2176 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, value.Count);
			}
		}

		public const Int32 Size = 2176;
		private fixed Byte Internal[Size];

		public readonly Int32 Length
		{
			get
			{
				return Size;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			get
			{
				return Count;
			}
		}

		private Int32 _count;
		public Int32 Count
		{
			readonly get
			{
				return _count;
			}
			set
			{
				if (value < 0 || value > Size)
				{
					throw new ArgumentOutOfRangeException(nameof(value), value, $"Count must be between 0 and {Size}");
				}

				_count = value;
			}
		}

		public readonly Boolean IsEmpty
		{
			get
			{
				return Count <= 0;
			}
		}

		public UnsafeSize2176(Span<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, _count = value.Length);
			}
		}

		public readonly ref T As<T>()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			return this;
		}

		public readonly Span<Byte> AsSpan()
		{
			return this;
		}

		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = this)
			{
				return ref *pointer;
			}
		}
	}

	public unsafe struct UnsafeSize2304 : IUnsafeSize<UnsafeSize2304>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize2304 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, value.Count);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize2304 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, value.Count);
			}
		}

		public const Int32 Size = 2304;
		private fixed Byte Internal[Size];

		public readonly Int32 Length
		{
			get
			{
				return Size;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			get
			{
				return Count;
			}
		}

		private Int32 _count;
		public Int32 Count
		{
			readonly get
			{
				return _count;
			}
			set
			{
				if (value < 0 || value > Size)
				{
					throw new ArgumentOutOfRangeException(nameof(value), value, $"Count must be between 0 and {Size}");
				}

				_count = value;
			}
		}

		public readonly Boolean IsEmpty
		{
			get
			{
				return Count <= 0;
			}
		}

		public UnsafeSize2304(Span<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, _count = value.Length);
			}
		}

		public readonly ref T As<T>()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			return this;
		}

		public readonly Span<Byte> AsSpan()
		{
			return this;
		}

		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = this)
			{
				return ref *pointer;
			}
		}
	}

	public unsafe struct UnsafeSize2432 : IUnsafeSize<UnsafeSize2432>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize2432 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, value.Count);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize2432 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, value.Count);
			}
		}

		public const Int32 Size = 2432;
		private fixed Byte Internal[Size];

		public readonly Int32 Length
		{
			get
			{
				return Size;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			get
			{
				return Count;
			}
		}

		private Int32 _count;
		public Int32 Count
		{
			readonly get
			{
				return _count;
			}
			set
			{
				if (value < 0 || value > Size)
				{
					throw new ArgumentOutOfRangeException(nameof(value), value, $"Count must be between 0 and {Size}");
				}

				_count = value;
			}
		}

		public readonly Boolean IsEmpty
		{
			get
			{
				return Count <= 0;
			}
		}

		public UnsafeSize2432(Span<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, _count = value.Length);
			}
		}

		public readonly ref T As<T>()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			return this;
		}

		public readonly Span<Byte> AsSpan()
		{
			return this;
		}

		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = this)
			{
				return ref *pointer;
			}
		}
	}

	public unsafe struct UnsafeSize2560 : IUnsafeSize<UnsafeSize2560>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize2560 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, value.Count);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize2560 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, value.Count);
			}
		}

		public const Int32 Size = 2560;
		private fixed Byte Internal[Size];

		public readonly Int32 Length
		{
			get
			{
				return Size;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			get
			{
				return Count;
			}
		}

		private Int32 _count;
		public Int32 Count
		{
			readonly get
			{
				return _count;
			}
			set
			{
				if (value < 0 || value > Size)
				{
					throw new ArgumentOutOfRangeException(nameof(value), value, $"Count must be between 0 and {Size}");
				}

				_count = value;
			}
		}

		public readonly Boolean IsEmpty
		{
			get
			{
				return Count <= 0;
			}
		}

		public UnsafeSize2560(Span<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, _count = value.Length);
			}
		}

		public readonly ref T As<T>()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			return this;
		}

		public readonly Span<Byte> AsSpan()
		{
			return this;
		}

		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = this)
			{
				return ref *pointer;
			}
		}
	}

	public unsafe struct UnsafeSize2688 : IUnsafeSize<UnsafeSize2688>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize2688 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, value.Count);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize2688 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, value.Count);
			}
		}

		public const Int32 Size = 2688;
		private fixed Byte Internal[Size];

		public readonly Int32 Length
		{
			get
			{
				return Size;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			get
			{
				return Count;
			}
		}

		private Int32 _count;
		public Int32 Count
		{
			readonly get
			{
				return _count;
			}
			set
			{
				if (value < 0 || value > Size)
				{
					throw new ArgumentOutOfRangeException(nameof(value), value, $"Count must be between 0 and {Size}");
				}

				_count = value;
			}
		}

		public readonly Boolean IsEmpty
		{
			get
			{
				return Count <= 0;
			}
		}

		public UnsafeSize2688(Span<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, _count = value.Length);
			}
		}

		public readonly ref T As<T>()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			return this;
		}

		public readonly Span<Byte> AsSpan()
		{
			return this;
		}

		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = this)
			{
				return ref *pointer;
			}
		}
	}

	public unsafe struct UnsafeSize2816 : IUnsafeSize<UnsafeSize2816>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize2816 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, value.Count);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize2816 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, value.Count);
			}
		}

		public const Int32 Size = 2816;
		private fixed Byte Internal[Size];

		public readonly Int32 Length
		{
			get
			{
				return Size;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			get
			{
				return Count;
			}
		}

		private Int32 _count;
		public Int32 Count
		{
			readonly get
			{
				return _count;
			}
			set
			{
				if (value < 0 || value > Size)
				{
					throw new ArgumentOutOfRangeException(nameof(value), value, $"Count must be between 0 and {Size}");
				}

				_count = value;
			}
		}

		public readonly Boolean IsEmpty
		{
			get
			{
				return Count <= 0;
			}
		}

		public UnsafeSize2816(Span<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, _count = value.Length);
			}
		}

		public readonly ref T As<T>()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			return this;
		}

		public readonly Span<Byte> AsSpan()
		{
			return this;
		}

		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = this)
			{
				return ref *pointer;
			}
		}
	}

	public unsafe struct UnsafeSize2944 : IUnsafeSize<UnsafeSize2944>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize2944 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, value.Count);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize2944 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, value.Count);
			}
		}

		public const Int32 Size = 2944;
		private fixed Byte Internal[Size];

		public readonly Int32 Length
		{
			get
			{
				return Size;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			get
			{
				return Count;
			}
		}

		private Int32 _count;
		public Int32 Count
		{
			readonly get
			{
				return _count;
			}
			set
			{
				if (value < 0 || value > Size)
				{
					throw new ArgumentOutOfRangeException(nameof(value), value, $"Count must be between 0 and {Size}");
				}

				_count = value;
			}
		}

		public readonly Boolean IsEmpty
		{
			get
			{
				return Count <= 0;
			}
		}

		public UnsafeSize2944(Span<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, _count = value.Length);
			}
		}

		public readonly ref T As<T>()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			return this;
		}

		public readonly Span<Byte> AsSpan()
		{
			return this;
		}

		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = this)
			{
				return ref *pointer;
			}
		}
	}

	public unsafe struct UnsafeSize3072 : IUnsafeSize<UnsafeSize3072>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize3072 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, value.Count);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize3072 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, value.Count);
			}
		}

		public const Int32 Size = 3072;
		private fixed Byte Internal[Size];

		public readonly Int32 Length
		{
			get
			{
				return Size;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			get
			{
				return Count;
			}
		}

		private Int32 _count;
		public Int32 Count
		{
			readonly get
			{
				return _count;
			}
			set
			{
				if (value < 0 || value > Size)
				{
					throw new ArgumentOutOfRangeException(nameof(value), value, $"Count must be between 0 and {Size}");
				}

				_count = value;
			}
		}

		public readonly Boolean IsEmpty
		{
			get
			{
				return Count <= 0;
			}
		}

		public UnsafeSize3072(Span<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, _count = value.Length);
			}
		}

		public readonly ref T As<T>()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			return this;
		}

		public readonly Span<Byte> AsSpan()
		{
			return this;
		}

		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = this)
			{
				return ref *pointer;
			}
		}
	}

	public unsafe struct UnsafeSize3200 : IUnsafeSize<UnsafeSize3200>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize3200 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, value.Count);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize3200 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, value.Count);
			}
		}

		public const Int32 Size = 3200;
		private fixed Byte Internal[Size];

		public readonly Int32 Length
		{
			get
			{
				return Size;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			get
			{
				return Count;
			}
		}

		private Int32 _count;
		public Int32 Count
		{
			readonly get
			{
				return _count;
			}
			set
			{
				if (value < 0 || value > Size)
				{
					throw new ArgumentOutOfRangeException(nameof(value), value, $"Count must be between 0 and {Size}");
				}

				_count = value;
			}
		}

		public readonly Boolean IsEmpty
		{
			get
			{
				return Count <= 0;
			}
		}

		public UnsafeSize3200(Span<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, _count = value.Length);
			}
		}

		public readonly ref T As<T>()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			return this;
		}

		public readonly Span<Byte> AsSpan()
		{
			return this;
		}

		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = this)
			{
				return ref *pointer;
			}
		}
	}

	public unsafe struct UnsafeSize3328 : IUnsafeSize<UnsafeSize3328>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize3328 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, value.Count);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize3328 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, value.Count);
			}
		}

		public const Int32 Size = 3328;
		private fixed Byte Internal[Size];

		public readonly Int32 Length
		{
			get
			{
				return Size;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			get
			{
				return Count;
			}
		}

		private Int32 _count;
		public Int32 Count
		{
			readonly get
			{
				return _count;
			}
			set
			{
				if (value < 0 || value > Size)
				{
					throw new ArgumentOutOfRangeException(nameof(value), value, $"Count must be between 0 and {Size}");
				}

				_count = value;
			}
		}

		public readonly Boolean IsEmpty
		{
			get
			{
				return Count <= 0;
			}
		}

		public UnsafeSize3328(Span<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, _count = value.Length);
			}
		}

		public readonly ref T As<T>()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			return this;
		}

		public readonly Span<Byte> AsSpan()
		{
			return this;
		}

		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = this)
			{
				return ref *pointer;
			}
		}
	}

	public unsafe struct UnsafeSize3456 : IUnsafeSize<UnsafeSize3456>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize3456 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, value.Count);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize3456 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, value.Count);
			}
		}

		public const Int32 Size = 3456;
		private fixed Byte Internal[Size];

		public readonly Int32 Length
		{
			get
			{
				return Size;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			get
			{
				return Count;
			}
		}

		private Int32 _count;
		public Int32 Count
		{
			readonly get
			{
				return _count;
			}
			set
			{
				if (value < 0 || value > Size)
				{
					throw new ArgumentOutOfRangeException(nameof(value), value, $"Count must be between 0 and {Size}");
				}

				_count = value;
			}
		}

		public readonly Boolean IsEmpty
		{
			get
			{
				return Count <= 0;
			}
		}

		public UnsafeSize3456(Span<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, _count = value.Length);
			}
		}

		public readonly ref T As<T>()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			return this;
		}

		public readonly Span<Byte> AsSpan()
		{
			return this;
		}

		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = this)
			{
				return ref *pointer;
			}
		}
	}

	public unsafe struct UnsafeSize3584 : IUnsafeSize<UnsafeSize3584>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize3584 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, value.Count);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize3584 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, value.Count);
			}
		}

		public const Int32 Size = 3584;
		private fixed Byte Internal[Size];

		public readonly Int32 Length
		{
			get
			{
				return Size;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			get
			{
				return Count;
			}
		}

		private Int32 _count;
		public Int32 Count
		{
			readonly get
			{
				return _count;
			}
			set
			{
				if (value < 0 || value > Size)
				{
					throw new ArgumentOutOfRangeException(nameof(value), value, $"Count must be between 0 and {Size}");
				}

				_count = value;
			}
		}

		public readonly Boolean IsEmpty
		{
			get
			{
				return Count <= 0;
			}
		}

		public UnsafeSize3584(Span<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, _count = value.Length);
			}
		}

		public readonly ref T As<T>()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			return this;
		}

		public readonly Span<Byte> AsSpan()
		{
			return this;
		}

		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = this)
			{
				return ref *pointer;
			}
		}
	}

	public unsafe struct UnsafeSize3712 : IUnsafeSize<UnsafeSize3712>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize3712 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, value.Count);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize3712 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, value.Count);
			}
		}

		public const Int32 Size = 3712;
		private fixed Byte Internal[Size];

		public readonly Int32 Length
		{
			get
			{
				return Size;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			get
			{
				return Count;
			}
		}

		private Int32 _count;
		public Int32 Count
		{
			readonly get
			{
				return _count;
			}
			set
			{
				if (value < 0 || value > Size)
				{
					throw new ArgumentOutOfRangeException(nameof(value), value, $"Count must be between 0 and {Size}");
				}

				_count = value;
			}
		}

		public readonly Boolean IsEmpty
		{
			get
			{
				return Count <= 0;
			}
		}

		public UnsafeSize3712(Span<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, _count = value.Length);
			}
		}

		public readonly ref T As<T>()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			return this;
		}

		public readonly Span<Byte> AsSpan()
		{
			return this;
		}

		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = this)
			{
				return ref *pointer;
			}
		}
	}

	public unsafe struct UnsafeSize3840 : IUnsafeSize<UnsafeSize3840>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize3840 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, value.Count);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize3840 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, value.Count);
			}
		}

		public const Int32 Size = 3840;
		private fixed Byte Internal[Size];

		public readonly Int32 Length
		{
			get
			{
				return Size;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			get
			{
				return Count;
			}
		}

		private Int32 _count;
		public Int32 Count
		{
			readonly get
			{
				return _count;
			}
			set
			{
				if (value < 0 || value > Size)
				{
					throw new ArgumentOutOfRangeException(nameof(value), value, $"Count must be between 0 and {Size}");
				}

				_count = value;
			}
		}

		public readonly Boolean IsEmpty
		{
			get
			{
				return Count <= 0;
			}
		}

		public UnsafeSize3840(Span<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, _count = value.Length);
			}
		}

		public readonly ref T As<T>()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			return this;
		}

		public readonly Span<Byte> AsSpan()
		{
			return this;
		}

		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = this)
			{
				return ref *pointer;
			}
		}
	}

	public unsafe struct UnsafeSize3968 : IUnsafeSize<UnsafeSize3968>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize3968 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, value.Count);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize3968 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, value.Count);
			}
		}

		public const Int32 Size = 3968;
		private fixed Byte Internal[Size];

		public readonly Int32 Length
		{
			get
			{
				return Size;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			get
			{
				return Count;
			}
		}

		private Int32 _count;
		public Int32 Count
		{
			readonly get
			{
				return _count;
			}
			set
			{
				if (value < 0 || value > Size)
				{
					throw new ArgumentOutOfRangeException(nameof(value), value, $"Count must be between 0 and {Size}");
				}

				_count = value;
			}
		}

		public readonly Boolean IsEmpty
		{
			get
			{
				return Count <= 0;
			}
		}

		public UnsafeSize3968(Span<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, _count = value.Length);
			}
		}

		public readonly ref T As<T>()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			return this;
		}

		public readonly Span<Byte> AsSpan()
		{
			return this;
		}

		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = this)
			{
				return ref *pointer;
			}
		}
	}

	public unsafe struct UnsafeSize4096 : IUnsafeSize<UnsafeSize4096>
	{
		public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize4096 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new ReadOnlySpan<Byte>(pointer, value.Count);
			}
		}

		public static implicit operator Span<Byte>(in UnsafeSize4096 value)
		{
			fixed (Byte* pointer = value.Internal)
			{
				return new Span<Byte>(pointer, value.Count);
			}
		}

		public const Int32 Size = 4096;
		private fixed Byte Internal[Size];

		public readonly Int32 Length
		{
			get
			{
				return Size;
			}
		}

		readonly Int32 IUnsafeSize.Size
		{
			get
			{
				return Count;
			}
		}

		private Int32 _count;
		public Int32 Count
		{
			readonly get
			{
				return _count;
			}
			set
			{
				if (value < 0 || value > Size)
				{
					throw new ArgumentOutOfRangeException(nameof(value), value, $"Count must be between 0 and {Size}");
				}

				_count = value;
			}
		}

		public readonly Boolean IsEmpty
		{
			get
			{
				return Count <= 0;
			}
		}

		public UnsafeSize4096(Span<Byte> value)
		{
			if (value.Length > Size)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Size}");
			}

			fixed (Byte* source = value)
			fixed (Byte* destination = Internal)
			{
				UnsafeUtilities.CopyBlock(destination, source, _count = value.Length);
			}
		}

		public readonly ref T As<T>()
		{
			fixed (Byte* pointer = Internal)
			{
				return ref UnsafeUtilities.AsRef<T>(pointer);
			}
		}

		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
			return this;
		}

		public readonly Span<Byte> AsSpan()
		{
			return this;
		}

		public ref Byte GetPinnableReference()
		{
			fixed (Byte* pointer = this)
			{
				return ref *pointer;
			}
		}
	}
}
#pragma warning restore CS0649
#pragma warning restore CS0169