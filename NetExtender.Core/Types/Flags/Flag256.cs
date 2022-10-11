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
    [StructLayout(LayoutKind.Explicit, Size = 32)]
    public readonly unsafe struct Flag256 : IFlag, IEquatable<Flag256>
    {
        public static implicit operator Flag256(Flag64 value)
        {
            return new Flag256(value);
        }
        
        public static implicit operator Flag256(Flag128 value)
        {
            return new Flag256(value.High, value.Low);
        }
        
        public static implicit operator Flag256(SByte value)
        {
            return new Flag256(Unsafe.As<SByte, Byte>(ref value));
        }
        
        public static implicit operator Flag256(Byte value)
        {
            return new Flag256(value);
        }
        
        public static implicit operator Flag256(Int16 value)
        {
            return new Flag256(Unsafe.As<Int16, UInt16>(ref value));
        }

        public static implicit operator Flag256(UInt16 value)
        {
            return new Flag256(value);
        }
        
        public static implicit operator Flag256(Int32 value)
        {
            return new Flag256(Unsafe.As<Int32, UInt32>(ref value));
        }

        public static implicit operator Flag256(UInt32 value)
        {
            return new Flag256(value);
        }
        
        public static implicit operator Flag256(Int64 value)
        {
            return new Flag256(Unsafe.As<Int64, UInt64>(ref value));
        }

        public static implicit operator Flag256(UInt64 value)
        {
            return new Flag256(value);
        }
        
        public static implicit operator ReadOnlySpan<Byte>(in Flag256 value)
        {
            fixed (void* pointer = &value)
            {
                return new ReadOnlySpan<Byte>(pointer, sizeof(UInt64) * 4 / sizeof(Byte));
            }
        }
        
        public static implicit operator ReadOnlySpan<UInt16>(in Flag256 value)
        {
            fixed (void* pointer = &value)
            {
                return new ReadOnlySpan<UInt16>(pointer, sizeof(UInt64) * 4 / sizeof(UInt16));
            }
        }
        
        public static implicit operator ReadOnlySpan<UInt32>(in Flag256 value)
        {
            fixed (void* pointer = &value)
            {
                return new ReadOnlySpan<UInt32>(pointer, sizeof(UInt64) * 4 / sizeof(UInt32));
            }
        }
        
        public static implicit operator ReadOnlySpan<UInt64>(in Flag256 value)
        {
            fixed (void* pointer = &value)
            {
                return new ReadOnlySpan<UInt64>(pointer, sizeof(UInt64) * 4 / sizeof(UInt64));
            }
        }
        
        public static Boolean operator ==(Flag256 first, Flag256 second)
        {
            return first.High1 == second.High1 && first.High0 == second.High0 && first.Low1 == second.Low1 && first.Low0 == second.Low0;
        }
        
        public static Boolean operator !=(Flag256 first, Flag256 second)
        {
            return !(first == second);
        }
        
        public static Flag256 operator |(Flag256 first, Flag256 second)
        {
            return new Flag256(first.High1 | second.High1, first.High0 | second.High0 , first.Low1 | second.Low1, first.Low0 | second.Low0);
        }
        
        public static Flag256 operator &(Flag256 first, Flag256 second)
        {
            return new Flag256(first.High1 & second.High1, first.High0 & second.High0 , first.Low1 & second.Low1, first.Low0 & second.Low0);
        }
        
        public static Flag256 operator ^(Flag256 first, Flag256 second)
        {
            return new Flag256(first.High1 ^ second.High1, first.High0 ^ second.High0 , first.Low1 ^ second.Low1, first.Low0 ^ second.Low0);
        }
        
        public static Flag256 operator ~(Flag256 value)
        {
            return new Flag256(~value.High1, ~value.High0 , ~value.Low1, ~value.Low0);
        }
        
        public static Flag256 operator <<(Flag256 value, Int32 shift)
        {
            return value.BitwiseShiftLeft(shift);
        }
        
        public static Flag256 operator >>(Flag256 value, Int32 shift)
        {
            return value.BitwiseShiftRight(shift);
        }

        public Int32 Size
        {
            get
            {
                return sizeof(Flag256);
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

        [field: FieldOffset(24)]
        internal UInt64 High1 { get; }
        
        [field: FieldOffset(16)]
        internal UInt64 High0 { get; }
        
        [field: FieldOffset(8)]
        internal UInt64 Low1 { get; }
        
        [field: FieldOffset(0)]
        internal UInt64 Low0 { get; }

        public Flag256(UInt64 low)
            : this(0, low)
        {
        }
        
        public Flag256(UInt64 high, UInt64 low)
            : this(0, high, low)
        {
        }
        
        public Flag256(UInt64 high0, UInt64 low1, UInt64 low0)
            : this(0, high0, low1, low0)
        {
        }
        
        public Flag256(UInt64 high1, UInt64 high0, UInt64 low1, UInt64 low0)
        {
            High1 = high1;
            High0 = high0;
            Low1 = low1;
            Low0 = low0;
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
            return HasFlag(value.AsSpan());
        }

        public Boolean HasFlag(Flag256 value)
        {
            return (High1 & value.High1) == value.High1 &&
                   (High0 & value.High0) == value.High0 &&
                   (Low1 & value.Low1) == value.Low1 &&
                   (Low0 & value.Low0) == value.Low0;
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
            return HashCode.Combine(High1, High0, Low1, Low0);
        }

        public override Boolean Equals(Object? obj)
        {
            return obj switch
            {
                null => false,
                Flag64 value => Equals(value),
                Flag128 value => Equals(value),
                Flag256 value => Equals(value),
                IFlag value => Equals(value),
                IConvertible convertible => Equals(convertible.ToUInt64()),
                _ => false
            };
        }

        public Boolean Equals(ReadOnlySpan<Byte> value)
        {
            return AsSpan().BitwiseEquals(value);
        }

        public Boolean Equals(Flag256 other)
        {
            return this == other;
        }

        public Boolean Equals(IFlag? other)
        {
            return other is not null && Equals(other.AsSpan());
        }
        
        public override String ToString()
        {
            return Convert.ToString(unchecked((Int64) High1), 2) + Convert.ToString(unchecked((Int64) High0), 2) +
                   Convert.ToString(unchecked((Int64) Low1), 2) + Convert.ToString(unchecked((Int64) Low0), 2);
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
                return (new Flag256(1) << index & this) == this;
            }
        }
    }
}