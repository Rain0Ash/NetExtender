// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using NetExtender.Utilities.Core;

#pragma warning disable CS9084
#pragma warning disable CS8500

namespace NetExtender.Types.Sizes
{
    internal static class UnsafeSize
    {
        internal const String Data = nameof(Data);
    }
    
    [Serializable]
    public unsafe struct UnsafeSize<TSize> : IUnsafeSpace<TSize>, IUnsafeSpace<UnsafeSize<TSize>> where TSize : struct, IUnsafeSize<TSize>
    {
        public static implicit operator ReadOnlySpan<Byte>(in UnsafeSize<TSize> value)
        {
            fixed (void* pointer = &value.Internal)
            {
                return new ReadOnlySpan<Byte>(pointer, value.Size);
            }
        }

		public static implicit operator Span<Byte>(in UnsafeSize<TSize> value)
		{
            fixed (void* pointer = &value.Internal)
            {
                return new Span<Byte>(pointer, value.Size);
            }
		}

        public static explicit operator TSize(in UnsafeSize<TSize> value)
        {
            return value.Internal;
        }

        public static implicit operator UnsafeSize<TSize>(in TSize value)
        {
            return new UnsafeSize<TSize>(in value);
        }

        public static Boolean operator ==(in UnsafeSize<TSize> first, ReadOnlySpan<Byte> second)
        {
            return first.Equals(second);
        }

        public static Boolean operator ==(in UnsafeSize<TSize> first, in TSize second)
        {
            return first.Equals(second);
        }

        public static Boolean operator ==(in UnsafeSize<TSize> first, in UnsafeSize<TSize> second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(in UnsafeSize<TSize> first, ReadOnlySpan<Byte> second)
        {
            return !(first == second);
        }

        public static Boolean operator !=(in UnsafeSize<TSize> first, in TSize second)
        {
            return !(first == second);
        }

        public static Boolean operator !=(in UnsafeSize<TSize> first, in UnsafeSize<TSize> second)
        {
            return !(first == second);
        }

        public static Boolean operator <(UnsafeSize<TSize> first, ReadOnlySpan<Byte> second)
        {
            return first.CompareTo(second) < 0;
        }

        public static Boolean operator <(in UnsafeSize<TSize> first, in TSize second)
        {
            return first.CompareTo(second) < 0;
        }

        public static Boolean operator <(in UnsafeSize<TSize> first, in UnsafeSize<TSize> second)
        {
            return first.CompareTo(second) < 0;
        }

        public static Boolean operator <=(in UnsafeSize<TSize> first, ReadOnlySpan<Byte> second)
        {
            return first.CompareTo(second) <= 0;
        }

        public static Boolean operator <=(in UnsafeSize<TSize> first, in TSize second)
        {
            return first.CompareTo(second) <= 0;
        }

        public static Boolean operator <=(in UnsafeSize<TSize> first, in UnsafeSize<TSize> second)
        {
            return first.CompareTo(second) <= 0;
        }

        public static Boolean operator >(in UnsafeSize<TSize> first, ReadOnlySpan<Byte> second)
        {
            return first.CompareTo(second) > 0;
        }

        public static Boolean operator >(in UnsafeSize<TSize> first, in TSize second)
        {
            return first.CompareTo(second) > 0;
        }

        public static Boolean operator >(in UnsafeSize<TSize> first, in UnsafeSize<TSize> second)
        {
            return first.CompareTo(second) > 0;
        }

        public static Boolean operator >=(in UnsafeSize<TSize> first, ReadOnlySpan<Byte> second)
        {
            return first.CompareTo(second) >= 0;
        }

        public static Boolean operator >=(in UnsafeSize<TSize> first, in TSize second)
        {
            return first.CompareTo(second) >= 0;
        }

        public static Boolean operator >=(in UnsafeSize<TSize> first, in UnsafeSize<TSize> second)
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

		ref UnsafeSize<TSize> IUnsafeSize<UnsafeSize<TSize>>.This
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return ref this;
			}
		}

        Boolean IUnsafeSpace<TSize>.HasLength
        {
            get
            {
                return false;
            }
        }

        Boolean IUnsafeSpace<UnsafeSize<TSize>>.HasLength
        {
            get
            {
                return false;
            }
        }

        Int32 IUnsafeSpace<TSize>.Length
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get
            {
                return Size;
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
            }
        }

        Int32 IUnsafeSpace<UnsafeSize<TSize>>.Length
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get
            {
                return Size;
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
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

        readonly Span<Byte> IUnsafeSize<TSize>.Full
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

        readonly Span<Byte> IUnsafeSize<UnsafeSize<TSize>>.Full
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

		public UnsafeSize(ReadOnlySpan<Byte> value)
        {
            Internal = new TSize();
            if (value.Length > Internal.Size)
            {
                throw new ArgumentOutOfRangeException(nameof(value), value.Length, $"Length must be less or equals {Internal.Size}.");
            }

            fixed (Byte* source = value)
            fixed (void* destination = &Internal)
            {
                UnsafeUtilities.CopyBlock(destination, source, value.Length);
            }
        }

        public UnsafeSize(in TSize value)
        {
            Internal = value;
        }

        internal UnsafeSize(SerializationInfo info, StreamingContext context)
        {
            Internal = new TSize().Deserialize(info, context);
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
        internal ref UnsafeSize<TSize> Deserialize(SerializationInfo info, StreamingContext context)
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
        ref UnsafeSize<TSize> IUnsafeSize<UnsafeSize<TSize>>.Deserialize(SerializationInfo info, StreamingContext context)
        {
            return ref Deserialize(info, context);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Boolean Support<T>() where T : struct
        {
            Int32 size = Unsafe.SizeOf<T>();
            return size > 0 && size <= Size;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly ref T As<T>() where T : struct
        {
            if (!Support<T>())
            {
                throw new NotSupportedException($"Type '{typeof(T).Name} ({Unsafe.SizeOf<T>()})' not support for '{Type.Name} ({Size})'.");
            }
            
            fixed (void* pointer = &Internal)
            {
                return ref UnsafeUtilities.AsRef<T>(pointer);
            }
        }

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly ReadOnlySpan<Byte> AsReadOnlySpan()
		{
            fixed (void* pointer = &Internal)
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
            
            fixed (void* pointer = &Internal)
            {
                return new ReadOnlySpan<T>(pointer, Size / Unsafe.SizeOf<T>());
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
		public readonly Span<Byte> AsSpan()
		{
            fixed (void* pointer = &Internal)
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
            
            fixed (void* pointer = &Internal)
            {
                return new Span<T>(pointer, Size / Unsafe.SizeOf<T>());
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        ref TSize IUnsafeSize<TSize>.SetFull()
        {
            return ref Internal;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        ref UnsafeSize<TSize> IUnsafeSize<UnsafeSize<TSize>>.SetFull()
        {
            return ref this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        ref TSize IUnsafeSize<TSize>.Fill(Byte value)
        {
            return ref Fill(value).Internal;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref UnsafeSize<TSize> Fill(Byte value)
        {
            Internal.Fill(value);
            return ref this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        ref TSize IUnsafeSize<TSize>.Fill<T>(T value)
        {
            return ref Fill(value).Internal;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref UnsafeSize<TSize> Fill<T>(T value) where T : struct
        {
            Internal.Fill(value);
            return ref this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        ref TSize IUnsafeSize<TSize>.Fill<T>(in T value)
        {
            return ref Fill(value).Internal;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref UnsafeSize<TSize> Fill<T>(in T value) where T : struct
        {
            Internal.Fill(in value);
            return ref this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        ref TSize IUnsafeSize<TSize>.Clear()
        {
            return ref Clear().Internal;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref UnsafeSize<TSize> Clear()
        {
            Internal.Clear();
            return ref this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        ref TSize IUnsafeSize<TSize>.Reset()
        {
            return ref Reset().Internal;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref UnsafeSize<TSize> Reset()
        {
            Internal.Reset();
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
            CopyTo(ref destination.Internal);
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
            return TryCopyTo(ref destination.Internal);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Byte[] ToArray()
        {
            return AsReadOnlySpan().ToArray();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Byte[] IUnsafeSize<TSize>.ToFullArray()
        {
            return ToArray();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        Byte[] IUnsafeSize<UnsafeSize<TSize>>.ToFullArray()
        {
            return ToArray();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly T[] ToArray<T>() where T : struct
        {
            return AsReadOnlySpan<T>().ToArray();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        T[] IUnsafeSize<TSize>.ToFullArray<T>()
        {
            return ToArray<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        T[] IUnsafeSize<UnsafeSize<TSize>>.ToFullArray<T>()
        {
            return ToArray<T>();
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
            return CompareTo(in other.Internal);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Int32 CompareTo(in UnsafeSize<TSize> other)
        {
            return CompareTo(in other.Internal);
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
            return Equals(in other.Internal);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Boolean Equals(in UnsafeSize<TSize> other)
        {
            return Equals(in other.Internal);
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