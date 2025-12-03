using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using NetExtender.Types.Sizes;
using NetExtender.Types.Vectors.Interfaces;
using NetExtender.Utilities.Core;
using NetExtender.Utilities.Types;

#pragma warning disable CS9084
#pragma warning disable CS8500

namespace NetExtender.Types.Vectors
{
    [Serializable]
    public unsafe struct Vector<T, TSize> : IUnsafeVector<Vector<T, TSize>, T, TSize>, IUnsafeSpace<Vector<T, TSize>>, IFormattable where T : struct, IFormattable where TSize : struct, IUnsafeSize<TSize>
    {
        public static explicit operator ReadOnlySpan<Byte>(in Vector<T, TSize> value)
        {
            fixed (Byte* pointer = value.Internal)
            {
                return new ReadOnlySpan<Byte>(pointer, value.Internal.Length);
            }
        }

        public static implicit operator Span<Byte>(in Vector<T, TSize> value)
        {
            fixed (Byte* pointer = value.Internal)
            {
                return new Span<Byte>(pointer, value.Internal.Length);
            }
        }

        public static implicit operator ReadOnlySpan<T>(in Vector<T, TSize> value)
        {
            fixed (Byte* pointer = value.Internal)
            {
                return new ReadOnlySpan<T>(pointer, value.Length);
            }
        }

        public static implicit operator Span<T>(in Vector<T, TSize> value)
        {
            fixed (Byte* pointer = value.Internal)
            {
                return new Span<T>(pointer, value.Length);
            }
        }

        public static explicit operator TSize(in Vector<T, TSize> value)
        {
            return (TSize) value.Internal;
        }

        public static explicit operator Vector<T, TSize>(in TSize value)
        {
            return new Vector<T, TSize>(in value);
        }

        public static explicit operator UnsafeSize<TSize>(in Vector<T, TSize> value)
        {
            return (UnsafeSize<TSize>) value.Internal;
        }

        public static explicit operator Vector<T, TSize>(in UnsafeSize<TSize> value)
        {
            return new Vector<T, TSize>(in value);
        }

        public static explicit operator UnsafeSpace<TSize>(in Vector<T, TSize> value)
        {
            return value.Internal;
        }

        public static explicit operator Vector<T, TSize>(in UnsafeSpace<TSize> value)
        {
            return new Vector<T, TSize>(in value);
        }

        public static Boolean operator ==(in Vector<T, TSize> first, ReadOnlySpan<Byte> second)
        {
            return first.Equals(second);
        }

        public static Boolean operator ==(in Vector<T, TSize> first, in TSize second)
        {
            return first.Equals(second);
        }

        public static Boolean operator ==(in Vector<T, TSize> first, in UnsafeSize<TSize> second)
        {
            return first.Equals(second);
        }

        public static Boolean operator ==(in Vector<T, TSize> first, in UnsafeSpace<TSize> second)
        {
            return first.Equals(second);
        }

        public static Boolean operator ==(in Vector<T, TSize> first, in Vector<T, TSize> second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(in Vector<T, TSize> first, ReadOnlySpan<Byte> second)
        {
            return !(first == second);
        }

        public static Boolean operator !=(in Vector<T, TSize> first, in TSize second)
        {
            return !(first == second);
        }

        public static Boolean operator !=(in Vector<T, TSize> first, in UnsafeSize<TSize> second)
        {
            return !(first == second);
        }

        public static Boolean operator !=(in Vector<T, TSize> first, in UnsafeSpace<TSize> second)
        {
            return !(first == second);
        }

        public static Boolean operator !=(in Vector<T, TSize> first, in Vector<T, TSize> second)
        {
            return !(first == second);
        }

        private UnsafeSpace<TSize> Internal;

        public readonly Type Type
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Internal.Type;
            }
        }

        ref Vector<T, TSize> IUnsafeSize<Vector<T, TSize>>.This
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ref this;
            }
        }

        Boolean IUnsafeSpace<Vector<T, TSize>>.HasLength
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return true;
            }
        }

        public Int32 Length
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get
            {
                return Internal.Length / Unsafe.SizeOf<T>();
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                if (value < 0 || value > Size)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), value, $"Count must be between 0 and {Size}.");
                }

                Internal.Length = value * Unsafe.SizeOf<T>();
            }
        }

        readonly Int32 IReadOnlyCollection<T>.Count
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Length;
            }
        }

        public readonly Int32 Size
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Internal.Size / Unsafe.SizeOf<T>();
            }
        }

        public readonly Span<T> Full
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                fixed (void* pointer = &Internal)
                {
                    return new Span<T>(pointer, Size);
                }
            }
        }

        readonly Span<Byte> IUnsafeSize<Vector<T, TSize>>.Full
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                fixed (void* pointer = &Internal)
                {
                    return new Span<Byte>(pointer, Internal.Size);
                }
            }
        }

        readonly Span<Byte> IUnsafeVector.Full
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                fixed (void* pointer = &Internal)
                {
                    return new Span<Byte>(pointer, Internal.Size);
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

        public Vector(ReadOnlySpan<T> value)
        {
            Internal = new TSize();
            ReadOnlySpan<Byte> bytes = value.AsBytes();

            if (bytes.Length > Internal.Size)
            {
                throw new ArgumentOutOfRangeException(nameof(value), bytes.Length, $"Length {value.Length} with element size {Unsafe.SizeOf<T>()} equals {bytes.Length}. It must be less or equals {Internal.Size}.");
            }

            fixed (Byte* source = bytes)
            fixed (void* destination = &Internal)
            {
                UnsafeUtilities.CopyBlock(destination, source, Internal.Length = bytes.Length);
            }
        }

        private Vector(in TSize size)
        {
            Internal = size;
        }

        private Vector(in UnsafeSize<TSize> size)
        {
            Internal = size;
        }

        private Vector(in UnsafeSpace<TSize> size)
        {
            Internal = size;
        }

        internal Vector(SerializationInfo info, StreamingContext context)
        {
            Internal = new UnsafeSpace<TSize>(info, context);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal readonly void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            Internal.GetObjectData(info, context);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        readonly void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            GetObjectData(info, context);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Boolean Support<TStruct>() where TStruct : struct
        {
            return typeof(TStruct) == typeof(T);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        readonly ref TStruct IUnsafeSize<Vector<T, TSize>>.As<TStruct>()
        {
            if (!Support<TStruct>())
            {
                throw new NotSupportedException($"Type '{typeof(TStruct).Name} ({Unsafe.SizeOf<TStruct>()})' not support for '{Type.Name} ({Internal.Length})'.");
            }

            return ref Internal.As<TStruct>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        readonly ReadOnlySpan<Byte> IUnsafeSize<Vector<T, TSize>>.AsReadOnlySpan()
        {
            return Internal.AsReadOnlySpan();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly ReadOnlySpan<T> AsReadOnlySpan()
        {
            fixed (void* pointer = &Internal)
            {
                return new ReadOnlySpan<T>(pointer, Length);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        readonly ReadOnlySpan<TStruct> IUnsafeSize<Vector<T, TSize>>.AsReadOnlySpan<TStruct>()
        {
            return Support<TStruct>() ? Internal.AsReadOnlySpan<TStruct>() : throw new NotSupportedException($"Type '{typeof(TStruct).Name} ({Unsafe.SizeOf<TStruct>()})' not support for '{Type.Name} ({Internal.Length})'.");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        readonly Span<Byte> IUnsafeSize<Vector<T, TSize>>.AsSpan()
        {
            return Internal.AsSpan();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Span<T> AsSpan()
        {
            fixed (void* pointer = &Internal)
            {
                return new Span<T>(pointer, Length);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        readonly Span<TStruct> IUnsafeSize<Vector<T, TSize>>.AsSpan<TStruct>()
        {
            return Support<TStruct>() ? Internal.AsSpan<TStruct>() : throw new NotSupportedException($"Type '{typeof(TStruct).Name} ({Unsafe.SizeOf<TStruct>()})' not support for '{Type.Name} ({Internal.Length})'.");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref Vector<T, TSize> SetFull()
        {
            Length = Size;
            return ref this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        ref Vector<T, TSize> IUnsafeSize<Vector<T, TSize>>.Fill(Byte value)
        {
            Internal.Fill(value);
            return ref this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void IVector<T>.Fill(T value)
        {
            Fill(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref Vector<T, TSize> Fill(T value)
        {
            AsSpan().Fill(value);
            return ref this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        ref Vector<T, TSize> IUnsafeSize<Vector<T, TSize>>.Fill<TStruct>(TStruct value)
        {
            if (!Support<TStruct>())
            {
                throw new NotSupportedException($"Type '{typeof(TStruct).Name} ({Unsafe.SizeOf<TStruct>()})' not support for '{Type.Name} ({Internal.Length})'.");
            }

            Internal.Fill(value);
            return ref this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        ref Vector<T, TSize> IUnsafeSize<Vector<T, TSize>>.Fill<TStruct>(in TStruct value)
        {
            if (!Support<TStruct>())
            {
                throw new NotSupportedException($"Type '{typeof(TStruct).Name} ({Unsafe.SizeOf<TStruct>()})' not support for '{Type.Name} ({Internal.Length})'.");
            }

            Internal.Fill(in value);
            return ref this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref Vector<T, TSize> Clear()
        {
            Internal.Clear();
            return ref this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref Vector<T, TSize> Reset()
        {
            Internal.Reset();
            return ref this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly void CopyTo(Span<Byte> destination)
        {
            Internal.CopyTo(destination);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly void CopyTo(Span<T> destination)
        {
            AsReadOnlySpan().CopyTo(destination);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly void CopyTo(ref Vector<T, TSize> destination)
        {
            Internal.CopyTo(ref destination.Internal);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Boolean TryCopyTo(Span<Byte> destination)
        {
            return Internal.TryCopyTo(destination);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Boolean TryCopyTo(Span<T> destination)
        {
            return AsReadOnlySpan().TryCopyTo(destination);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Boolean TryCopyTo(ref Vector<T, TSize> destination)
        {
            return Internal.TryCopyTo(ref destination.Internal);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        readonly Byte[] IUnsafeSize<Vector<T, TSize>>.ToArray()
        {
            return Internal.ToArray();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        readonly Byte[] IUnsafeSize<Vector<T, TSize>>.ToFullArray()
        {
            return Internal.ToFullArray();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly T[] ToArray()
        {
            return Internal.ToArray<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly T[] ToFullArray()
        {
            return Internal.ToFullArray<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        readonly TStruct[] IUnsafeSize<Vector<T, TSize>>.ToArray<TStruct>()
        {
            return Support<TStruct>() ? Internal.ToArray<TStruct>() : throw new NotSupportedException($"Type '{typeof(TStruct).Name} ({Unsafe.SizeOf<TStruct>()})' not support for '{Type.Name} ({Internal.Length})'.");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        readonly TStruct[] IUnsafeSize<Vector<T, TSize>>.ToFullArray<TStruct>()
        {
            return Support<TStruct>() ? Internal.ToFullArray<TStruct>() : throw new NotSupportedException($"Type '{typeof(TStruct).Name} ({Unsafe.SizeOf<TStruct>()})' not support for '{Type.Name} ({Internal.Size})'.");
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
            return Internal.CompareTo(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Int32 CompareTo(TSize other)
        {
            return Internal.CompareTo(in other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Int32 CompareTo(in TSize other)
        {
            return Internal.CompareTo(in other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Int32 CompareTo(UnsafeSize<TSize> other)
        {
            return Internal.CompareTo(in other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Int32 CompareTo(in UnsafeSize<TSize> other)
        {
            return Internal.CompareTo(in other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Int32 CompareTo(UnsafeSpace<TSize> other)
        {
            return Internal.CompareTo(in other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Int32 CompareTo(in UnsafeSpace<TSize> other)
        {
            return Internal.CompareTo(in other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Int32 CompareTo(Vector<T, TSize> other)
        {
            return Internal.CompareTo(in other.Internal);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Int32 CompareTo(in Vector<T, TSize> other)
        {
            return Internal.CompareTo(in other.Internal);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override readonly Int32 GetHashCode()
        {
            return Internal.GetHashCode();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override readonly Boolean Equals(Object? other)
        {
            return other switch
            {
                Vector<T, TSize> value => Equals(value),
                _ => Internal.Equals(other)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Boolean Equals(ReadOnlySpan<Byte> other)
        {
            return Internal.Equals(other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Boolean Equals(TSize other)
        {
            return Internal.Equals(in other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Boolean Equals(in TSize other)
        {
            return Internal.Equals(in other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Boolean Equals(UnsafeSize<TSize> other)
        {
            return Internal.Equals(in other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Boolean Equals(in UnsafeSize<TSize> other)
        {
            return Internal.Equals(in other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Boolean Equals(UnsafeSpace<TSize> other)
        {
            return Internal.Equals(in other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Boolean Equals(in UnsafeSpace<TSize> other)
        {
            return Internal.Equals(in other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Boolean Equals(Vector<T, TSize> other)
        {
            return Internal.Equals(in other.Internal);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Boolean Equals(in Vector<T, TSize> other)
        {
            return Internal.Equals(in other.Internal);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly Enumerator GetEnumerator()
        {
            return new Enumerator(this);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        readonly IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return GetEnumerator();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        readonly IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override readonly String ToString()
        {
            return ToString(null, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly String ToString(String? format)
        {
            return ToString(format, null);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly String ToString(IFormatProvider? provider)
        {
            return ToString(null, provider);
        }

        public readonly String ToString(String? format, IFormatProvider? provider)
        {
            StringBuilder builder = new StringBuilder(Internal.Length);

            builder.Append('(');

            ReadOnlySpan<T>.Enumerator enumerator = AsReadOnlySpan().GetEnumerator();

            enumerator.MoveNext();
            builder.Append(enumerator.Current.ToString(format, provider));

            while (enumerator.MoveNext())
            {
                builder.Append(',');
                builder.Append(' ');
                builder.Append(enumerator.Current.ToString(format, provider));
            }

            builder.Append(')');
            return builder.ToString();
        }

        public T this[Int32 index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get
            {
                return AsReadOnlySpan()[index];
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                AsSpan()[index] = value;
            }
        }

        readonly T IReadOnlyList<T>.this[Int32 index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return this[index];
            }
        }

        T IVector<T>.this[Int32 index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            readonly get
            {
                return this[index];
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                this[index] = value;
            }
        }

        public struct Enumerator : IEnumerator<T>
        {
            public readonly Vector<T, TSize> Vector;
            private Int32 Index;

            public readonly T Current
            {
                get
                {
                    return Index >= 0 ? Vector[Index] : throw new InvalidOperationException();
                }
            }

            readonly Object? IEnumerator.Current
            {
                get
                {
                    return Index >= 0 ? Vector[Index] : null;
                }
            }

            internal Enumerator(Vector<T, TSize> vector)
            {
                Vector = vector;
                Index = -1;
            }

            public Boolean MoveNext()
            {
                if (Vector.Length <= 0)
                {
                    return false;
                }

                if (Index >= Vector.Length - 1)
                {
                    return false;
                }

                Index++;
                return true;
            }

            public void Reset()
            {
                Index = -1;
            }

            public void Dispose()
            {
                Reset();
            }
        }
    }
}

#pragma warning restore CS9084
#pragma warning restore CS8500