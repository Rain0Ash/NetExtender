// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using NetExtender.Types.Flags.Interfaces;
using NetExtender.Utilities.Numerics;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Flags
{
    [Serializable]
    [StructLayout(LayoutKind.Explicit, Size = 16)]
    public readonly unsafe struct Flag128 : IFlag, IEquatable<Flag128>
    {
        public static implicit operator Flag128(Flag64 value)
        {
            return new Flag128(value);
        }

        public static implicit operator Flag128(SByte value)
        {
            return new Flag128(Unsafe.As<SByte, Byte>(ref value));
        }

        public static implicit operator Flag128(Byte value)
        {
            return new Flag128(value);
        }

        public static implicit operator Flag128(Int16 value)
        {
            return new Flag128(Unsafe.As<Int16, UInt16>(ref value));
        }

        public static implicit operator Flag128(UInt16 value)
        {
            return new Flag128(value);
        }

        public static implicit operator Flag128(Int32 value)
        {
            return new Flag128(Unsafe.As<Int32, UInt32>(ref value));
        }

        public static implicit operator Flag128(UInt32 value)
        {
            return new Flag128(value);
        }

        public static implicit operator Flag128(Int64 value)
        {
            return new Flag128(Unsafe.As<Int64, UInt64>(ref value));
        }

        public static implicit operator Flag128(UInt64 value)
        {
            return new Flag128(value);
        }

        public static implicit operator ReadOnlySpan<Byte>(in Flag128 value)
        {
            fixed (void* pointer = &value)
            {
                return new ReadOnlySpan<Byte>(pointer, sizeof(UInt64) * 2 / sizeof(Byte));
            }
        }

        public static implicit operator ReadOnlySpan<UInt16>(in Flag128 value)
        {
            fixed (void* pointer = &value)
            {
                return new ReadOnlySpan<UInt16>(pointer, sizeof(UInt64) * 2 / sizeof(UInt16));
            }
        }

        public static implicit operator ReadOnlySpan<UInt32>(in Flag128 value)
        {
            fixed (void* pointer = &value)
            {
                return new ReadOnlySpan<UInt32>(pointer, sizeof(UInt64) * 2 / sizeof(UInt32));
            }
        }

        public static implicit operator ReadOnlySpan<UInt64>(in Flag128 value)
        {
            fixed (void* pointer = &value)
            {
                return new ReadOnlySpan<UInt64>(pointer, sizeof(UInt64) * 2 / sizeof(UInt64));
            }
        }

        public static Boolean operator ==(Flag128 first, Flag128 second)
        {
            return first.High == second.High && first.Low == second.Low;
        }

        public static Boolean operator !=(Flag128 first, Flag128 second)
        {
            return !(first == second);
        }

        public static Flag128 operator |(Flag128 first, Flag128 second)
        {
            return new Flag128(first.High | second.High, first.Low | second.Low);
        }

        public static Flag128 operator &(Flag128 first, Flag128 second)
        {
            return new Flag128(first.High & second.High, first.Low & second.Low);
        }

        public static Flag128 operator ^(Flag128 first, Flag128 second)
        {
            return new Flag128(first.High ^ second.High, first.Low ^ second.Low);
        }

        public static Flag128 operator ~(Flag128 value)
        {
            return new Flag128(~value.High, ~value.Low);
        }

        public static Flag128 operator <<(Flag128 value, Int32 shift)
        {
            return value.BitwiseShiftLeft(shift);
        }

        public static Flag128 operator >>(Flag128 value, Int32 shift)
        {
            return value.BitwiseShiftRight(shift);
        }

        public Int32 Size
        {
            get
            {
                return sizeof(Flag128);
            }
        }

        public Int32 Count
        {
            get
            {
                return Size * BitUtilities.BitInByte;
            }
        }

        public Int32 PopCount
        {
            get
            {
                return this.BitwisePopCount();
            }
        }

        [field: FieldOffset(8)]
        internal UInt64 High { get; }

        [field: FieldOffset(0)]
        internal UInt64 Low { get; }

        public Flag128(UInt64 low)
            : this(0, low)
        {
        }

        public Flag128(UInt64 high, UInt64 low)
        {
            High = high;
            Low = low;
        }

        public ReadOnlySpan<Byte> AsSpan()
        {
            return this;
        }

        public Boolean HasFlag(ReadOnlySpan<Byte> value)
        {
            Int32 i = 0;
            foreach (Byte current in AsSpan())
            {
                if ((current & value[i++]) != current)
                {
                    return false;
                }
            }

            return true;
        }

        public Boolean HasFlag<T>(T value) where T : unmanaged, Enum
        {
            return HasFlag(value.AsUInt64());
        }

        public Boolean HasFlag(Flag64 value)
        {
            return HasFlag(value.AsSpan());
        }

        public Boolean HasFlag(Flag128 value)
        {
            return (High & value.High) == value.High && (Low & value.Low) == value.Low;
        }

        public Boolean HasFlag(Flag256 value)
        {
            return HasFlag(value.AsSpan());
        }

        public Boolean HasFlag(Flag512 value)
        {
            return HasFlag(value.AsSpan());
        }

        public Boolean HasFlag(Flag1024 value)
        {
            return HasFlag(value.AsSpan());
        }

        public Boolean HasFlag<T>(EnumFlag<T> value) where T : unmanaged, Enum
        {
            return HasFlag(value.AsSpan());
        }

        public Boolean HasFlag(IFlag value)
        {
            return HasFlag(value.AsSpan());
        }

        public override Int32 GetHashCode()
        {
            return HashCode.Combine(High, Low);
        }

        public override Boolean Equals(Object? other)
        {
            return other switch
            {
                null => false,
                Flag64 value => Equals(value),
                Flag128 value => Equals(value),
                IFlag value => Equals(value),
                IConvertible convertible => Equals(convertible.ToUInt64()),
                _ => false
            };
        }

        public Boolean Equals(ReadOnlySpan<Byte> value)
        {
            return AsSpan().BitwiseEquals(value);
        }

        public Boolean Equals(Flag128 other)
        {
            return this == other;
        }

        public Boolean Equals(IFlag? other)
        {
            return other is not null && Equals(other.AsSpan());
        }

        public override String ToString()
        {
            return Convert.ToString(unchecked((Int64) High), 2) + Convert.ToString(unchecked((Int64) Low), 2);
        }

        public IEnumerable<Int32> Enumerate()
        {
            Byte[] values = AsSpan().ToArray();
            Int32[] destination = new Int32[BitUtilities.BitInByte];

            for (Int32 counter = 0; counter < values.Length; counter++)
            {
                Byte value = values[counter];
                if (!BitUtilities.TryGetSetBits(value, destination, out Int32 written))
                {
                    throw new InvalidOperationException();
                }

                for (Int32 i = 0; i < written; i++)
                {
                    yield return destination[i] + counter * BitUtilities.BitInByte;
                }
            }
        }

        public IEnumerable<TEnum> Enumerate<TEnum>() where TEnum : unmanaged, Enum
        {
            return Enumerate().Select(item => Unsafe.As<Int32, TEnum>(ref item));
        }

        public IEnumerator<Boolean> GetEnumerator()
        {
            for (Int32 i = 0; i < Count; i++)
            {
                yield return this[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Boolean this[Int32 index]
        {
            get
            {
                return (new Flag128(1) << index & this) == this;
            }
        }
    }
}