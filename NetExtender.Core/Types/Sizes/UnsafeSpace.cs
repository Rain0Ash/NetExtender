using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using NetExtender.Utilities.Core;

#pragma warning disable CS9084
#pragma warning disable CS8500

namespace NetExtender.Types.Sizes
{
    [Serializable]
    public unsafe struct UnsafeSpace<TSize> : IUnsafeSpace<TSize>, IUnsafeSpace<UnsafeSpace<TSize>> where TSize : struct, IUnsafeSize<TSize>
    {
        public static implicit operator ReadOnlySpan<Byte>(in UnsafeSpace<TSize> value)
        {
            fixed (void* pointer = &value.Internal)
            {
                return new ReadOnlySpan<Byte>(pointer, value.Length);
            }
        }

		public static implicit operator Span<Byte>(in UnsafeSpace<TSize> value)
		{
            fixed (void* pointer = &value.Internal)
            {
                return new Span<Byte>(pointer, value.Length);
            }
		}

        public static explicit operator TSize(in UnsafeSpace<TSize> value)
        {
            return value.Internal;
        }

        public static implicit operator UnsafeSpace<TSize>(in TSize value)
        {
            return new UnsafeSpace<TSize>(in value);
        }

        public static explicit operator UnsafeSize<TSize>(in UnsafeSpace<TSize> value)
        {
            return value.Internal;
        }

        public static implicit operator UnsafeSpace<TSize>(in UnsafeSize<TSize> value)
        {
            return new UnsafeSpace<TSize>(in value);
        }

        public static Boolean operator ==(in UnsafeSpace<TSize> first, ReadOnlySpan<Byte> second)
        {
            return first.Equals(second);
        }

        public static Boolean operator ==(in UnsafeSpace<TSize> first, in TSize second)
        {
            return first.Equals(second);
        }

        public static Boolean operator ==(in UnsafeSpace<TSize> first, in UnsafeSize<TSize> second)
        {
            return first.Equals(second);
        }

        public static Boolean operator ==(in UnsafeSpace<TSize> first, in UnsafeSpace<TSize> second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(in UnsafeSpace<TSize> first, ReadOnlySpan<Byte> second)
        {
            return !(first == second);
        }

        public static Boolean operator !=(in UnsafeSpace<TSize> first, in TSize second)
        {
            return !(first == second);
        }

        public static Boolean operator !=(in UnsafeSpace<TSize> first, in UnsafeSize<TSize> second)
        {
            return !(first == second);
        }

        public static Boolean operator !=(in UnsafeSpace<TSize> first, in UnsafeSpace<TSize> second)
        {
            return !(first == second);
        }

        public static Boolean operator <(in UnsafeSpace<TSize> first, ReadOnlySpan<Byte> second)
        {
            return first.CompareTo(second) < 0;
        }

        public static Boolean operator <(in UnsafeSpace<TSize> first, in TSize second)
        {
            return first.CompareTo(second) < 0;
        }

        public static Boolean operator <(in UnsafeSpace<TSize> first, in UnsafeSize<TSize> second)
        {
            return first.CompareTo(second) < 0;
        }

        public static Boolean operator <(in UnsafeSpace<TSize> first, in UnsafeSpace<TSize> second)
        {
            return first.CompareTo(second) < 0;
        }

        public static Boolean operator <=(in UnsafeSpace<TSize> first, ReadOnlySpan<Byte> second)
        {
            return first.CompareTo(second) <= 0;
        }

        public static Boolean operator <=(in UnsafeSpace<TSize> first, in TSize second)
        {
            return first.CompareTo(second) <= 0;
        }

        public static Boolean operator <=(in UnsafeSpace<TSize> first, in UnsafeSize<TSize> second)
        {
            return first.CompareTo(second) <= 0;
        }

        public static Boolean operator <=(in UnsafeSpace<TSize> first, in UnsafeSpace<TSize> second)
        {
            return first.CompareTo(second) <= 0;
        }

        public static Boolean operator >(in UnsafeSpace<TSize> first, ReadOnlySpan<Byte> second)
        {
            return first.CompareTo(second) > 0;
        }

        public static Boolean operator >(in UnsafeSpace<TSize> first, in TSize second)
        {
            return first.CompareTo(second) > 0;
        }

        public static Boolean operator >(in UnsafeSpace<TSize> first, in UnsafeSize<TSize> second)
        {
            return first.CompareTo(second) > 0;
        }

        public static Boolean operator >(in UnsafeSpace<TSize> first, in UnsafeSpace<TSize> second)
        {
            return first.CompareTo(second) > 0;
        }

        public static Boolean operator >=(in UnsafeSpace<TSize> first, ReadOnlySpan<Byte> second)
        {
            return first.CompareTo(second) >= 0;
        }

        public static Boolean operator >=(in UnsafeSpace<TSize> first, in TSize second)
        {
            return first.CompareTo(second) >= 0;
        }

        public static Boolean operator >=(in UnsafeSpace<TSize> first, in UnsafeSize<TSize> second)
        {
            return first.CompareTo(second) >= 0;
        }

        public static Boolean operator >=(in UnsafeSpace<TSize> first, in UnsafeSpace<TSize> second)
        {
            return first.CompareTo(second) >= 0;
        }

        private const String Data = UnsafeSize.Data;
        private TSize Internal;

		public readonly Type Type
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Internal.Type;
			}
		}

		readonly ref TSize IUnsafeSize<TSize>.This
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return ref Internal.This;
			}
		}

		ref UnsafeSpace<TSize> IUnsafeSize<UnsafeSpace<TSize>>.This
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return ref this;
			}
		}

        Boolean IUnsafeSpace<TSize>.HasLength
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return true;
            }
        }

        Boolean IUnsafeSpace<UnsafeSpace<TSize>>.HasLength
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return true;
            }
        }

        private Int32 _length;
        public Int32 Length
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get
            {
                return _length;
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                if (value < 0 || value > Size)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), value, $"Count must be between 0 and {Size}.");
                }

                _length = value;
            }
        }

        public readonly Int32 Size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Internal.Size;
			}
		}

        public readonly Span<Byte> Full
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                fixed (void* pointer = &Internal)
                {
                    return new Span<Byte>(pointer, Size);
                }
            }
        }

        public readonly Boolean IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Internal.IsEmpty;
			}
		}

		public UnsafeSpace(ReadOnlySpan<Byte> value)
        {
            Internal = new TSize();
            if (value.Length > Internal.Size)
            {
                throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Internal.Size}.");
            }

            fixed (Byte* source = value)
            fixed (void* destination = &Internal)
            {
                UnsafeUtilities.CopyBlock(destination, source, _length = value.Length);
            }
        }

		public UnsafeSpace(in TSize value)
        {
            Internal = value;
            _length = Internal.Size;
        }

        public UnsafeSpace(in TSize value, Int32 length)
        {
            Internal = value;
            _length = Math.Clamp(length, 0, Internal.Size);
        }

        public UnsafeSpace(in UnsafeSize<TSize> value)
        {
            Internal = (TSize) value;
            _length = value.Size;
        }

        public UnsafeSpace(in UnsafeSize<TSize> value, Int32 length)
        {
            Internal = (TSize) value;
            _length = Math.Clamp(length, 0, Internal.Size);
        }

        internal UnsafeSpace(SerializationInfo info, StreamingContext context)
        {
            Internal = new TSize().Deserialize(info, context);
            _length = Math.Clamp(info.GetInt32(nameof(Length)), 0, Internal.Size);
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
        internal ref UnsafeSpace<TSize> Deserialize(SerializationInfo info, StreamingContext context)
        {
            Internal.Deserialize(info, context);
            return ref this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        ref TSize IUnsafeSize<TSize>.Deserialize(SerializationInfo info, StreamingContext context)
        {
            return ref Deserialize(info, context).Internal;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        ref UnsafeSpace<TSize> IUnsafeSize<UnsafeSpace<TSize>>.Deserialize(SerializationInfo info, StreamingContext context)
        {
            return ref Deserialize(info, context);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Boolean Support<T>() where T : struct
        {
            Int32 size = Unsafe.SizeOf<T>();
            return size > 0 && size <= Length;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly ref T As<T>() where T : struct
        {
            if (!Support<T>())
            {
                throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Length})'.");
            }

            fixed (Byte* pointer = AsReadOnlySpan())
            {
                return ref UnsafeUtilities.AsRef<T>(pointer);
            }
        }

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
            fixed (void* pointer = &Internal)
            {
                return new ReadOnlySpan<Byte>(pointer, Length);
            }
		}

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly ReadOnlySpan<T> AsReadOnlySpan<T>() where T : struct
        {
            if (!Support<T>())
            {
                throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Length})'.");
            }

            fixed (void* pointer = &Internal)
            {
                return new ReadOnlySpan<T>(pointer, Length / Unsafe.SizeOf<T>());
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<Byte> AsSpan()
		{
            fixed (void* pointer = &Internal)
            {
                return new Span<Byte>(pointer, Length);
            }
		}

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Span<T> AsSpan<T>() where T : struct
        {
            if (!Support<T>())
            {
                throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Length})'.");
            }

            fixed (void* pointer = &Internal)
            {
                return new Span<T>(pointer, Length / Unsafe.SizeOf<T>());
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        ref TSize IUnsafeSize<TSize>.SetFull()
        {
            return ref Internal;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref UnsafeSpace<TSize> SetFull()
        {
            Length = Size;
            return ref this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        ref TSize IUnsafeSize<TSize>.Fill(Byte value)
        {
            return ref Fill(value).Internal;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref UnsafeSpace<TSize> Fill(Byte value)
        {
            AsSpan().Fill(value);
            return ref this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        ref TSize IUnsafeSize<TSize>.Fill<T>(T value)
        {
            return ref Fill(value).Internal;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref UnsafeSpace<TSize> Fill<T>(T value) where T : struct
        {
            AsSpan<T>().Fill(value);
            return ref this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        ref TSize IUnsafeSize<TSize>.Fill<T>(in T value)
        {
            return ref Fill(in value).Internal;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref UnsafeSpace<TSize> Fill<T>(in T value) where T : struct
        {
            AsSpan<T>().Fill(value);
            return ref this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        ref TSize IUnsafeSize<TSize>.Clear()
        {
            return ref Clear().Internal;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref UnsafeSpace<TSize> Clear()
        {
            Full.Slice(Length).Fill(0);
            return ref this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        ref TSize IUnsafeSize<TSize>.Reset()
        {
            return ref Reset().Internal;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref UnsafeSpace<TSize> Reset()
        {
            Full.Fill(0);
            return ref this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly void CopyTo(Span<Byte> destination)
        {
            AsReadOnlySpan().CopyTo(destination);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly void CopyTo(ref TSize destination)
        {
            fixed (Byte* pointer = destination)
            {
                CopyTo(new Span<Byte>(pointer, destination.Size));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly void CopyTo(ref UnsafeSize<TSize> destination)
        {
            CopyTo(destination);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly void CopyTo(ref UnsafeSpace<TSize> destination)
        {
            CopyTo(ref destination.Internal);
            destination.Length = Length;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Boolean TryCopyTo(Span<Byte> destination)
        {
            return AsReadOnlySpan().TryCopyTo(destination);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Boolean TryCopyTo(ref TSize destination)
        {
            fixed (Byte* pointer = destination)
            {
                return TryCopyTo(new Span<Byte>(pointer, destination.Size));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Boolean TryCopyTo(ref UnsafeSize<TSize> destination)
        {
            return TryCopyTo(destination);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Boolean TryCopyTo(ref UnsafeSpace<TSize> destination)
        {
            if (!TryCopyTo(ref destination.Internal))
            {
                return false;
            }

            destination.Length = Length;
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Byte[] ToArray()
        {
            return AsReadOnlySpan().ToArray();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Byte[] ToFullArray()
        {
            fixed (void* pointer = &Internal)
            {
                return new ReadOnlySpan<Byte>(pointer, Size).ToArray();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly T[] ToArray<T>() where T : struct
        {
            return AsReadOnlySpan<T>().ToArray();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly T[] ToFullArray<T>() where T : struct
        {
            if (!Support<T>())
            {
                throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
            }

            fixed (void* pointer = &Internal)
            {
                return new ReadOnlySpan<T>(pointer, Size / Unsafe.SizeOf<T>()).ToArray();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
		public ref Byte GetPinnableReference()
		{
            fixed (void* pointer = &Internal)
            {
                return ref *(Byte*) pointer;
            }
		}

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Int32 CompareTo(ReadOnlySpan<Byte> other)
        {
            return AsReadOnlySpan().SequenceCompareTo(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Int32 CompareTo(TSize other)
        {
            fixed (Byte* pointer = other)
            {
                return CompareTo(new ReadOnlySpan<Byte>(pointer, other.Size));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Int32 CompareTo(in TSize other)
        {
            fixed (Byte* pointer = other)
            {
                return CompareTo(new ReadOnlySpan<Byte>(pointer, other.Size));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Int32 CompareTo(UnsafeSize<TSize> other)
        {
            return CompareTo((ReadOnlySpan<Byte>) other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Int32 CompareTo(in UnsafeSize<TSize> other)
        {
            return CompareTo((ReadOnlySpan<Byte>) other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Int32 CompareTo(UnsafeSpace<TSize> other)
        {
            return CompareTo((ReadOnlySpan<Byte>) other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Int32 CompareTo(in UnsafeSpace<TSize> other)
        {
            return CompareTo((ReadOnlySpan<Byte>) other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override readonly Int32 GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.AddBytes(this);
            hash.Add(_length);
            return hash.ToHashCode();
        }

        public override readonly Boolean Equals(Object? other)
        {
            return other switch
            {
                null => IsEmpty,
                TSize value => Equals(value),
                UnsafeSize<TSize> value => Equals(value),
                UnsafeSpace<TSize> value => Equals(value),
                Memory<Byte> value => Equals(value.Span),
                ReadOnlyMemory<Byte> value => Equals(value.Span),
                _ => false
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Boolean Equals(ReadOnlySpan<Byte> other)
        {
            return AsReadOnlySpan().SequenceEqual(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Boolean Equals(TSize other)
        {
            fixed (Byte* pointer = other)
            {
                return Equals(new ReadOnlySpan<Byte>(pointer, other.Size));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Boolean Equals(in TSize other)
        {
            fixed (Byte* pointer = other)
            {
                return Equals(new ReadOnlySpan<Byte>(pointer, other.Size));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Boolean Equals(UnsafeSize<TSize> other)
        {
            return Equals((ReadOnlySpan<Byte>) other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Boolean Equals(in UnsafeSize<TSize> other)
        {
            return Equals((ReadOnlySpan<Byte>) other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Boolean Equals(UnsafeSpace<TSize> other)
        {
            return Equals((ReadOnlySpan<Byte>) other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Boolean Equals(in UnsafeSpace<TSize> other)
        {
            return Equals((ReadOnlySpan<Byte>) other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override readonly String ToString()
        {
            return Convert.ToHexString(this);
        }
    }
}

#pragma warning restore CS9084
#pragma warning restore CS8500