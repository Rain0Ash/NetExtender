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
    [StructLayout(LayoutKind.Explicit, Size = 128)]
    public readonly unsafe struct Flag1024 : IFlag, IEquatable<Flag1024>
    {
        public static implicit operator Flag1024(Flag64 value)
        {
            return new Flag1024(value);
        }
        
        public static implicit operator Flag1024(Flag128 value)
        {
            return new Flag1024(value.High, value.Low);
        }
        
        public static implicit operator Flag1024(Flag256 value)
        {
            return new Flag1024(value.High1, value.High0, value.Low1, value.Low0);
        }
        
        public static implicit operator Flag1024(Flag512 value)
        {
            return new Flag1024(value.High3, value.High2, value.High1, value.High0, value.Low3, value.Low2, value.Low1, value.Low0);
        }
        
        public static implicit operator Flag1024(SByte value)
        {
            return new Flag1024(Unsafe.As<SByte, Byte>(ref value));
        }
        
        public static implicit operator Flag1024(Byte value)
        {
            return new Flag1024(value);
        }
        
        public static implicit operator Flag1024(Int16 value)
        {
            return new Flag1024(Unsafe.As<Int16, UInt16>(ref value));
        }

        public static implicit operator Flag1024(UInt16 value)
        {
            return new Flag1024(value);
        }
        
        public static implicit operator Flag1024(Int32 value)
        {
            return new Flag1024(Unsafe.As<Int32, UInt32>(ref value));
        }

        public static implicit operator Flag1024(UInt32 value)
        {
            return new Flag1024(value);
        }
        
        public static implicit operator Flag1024(Int64 value)
        {
            return new Flag1024(Unsafe.As<Int64, UInt64>(ref value));
        }

        public static implicit operator Flag1024(UInt64 value)
        {
            return new Flag1024(value);
        }
        
        public static implicit operator ReadOnlySpan<Byte>(in Flag1024 value)
        {
            fixed (void* pointer = &value)
            {
                return new ReadOnlySpan<Byte>(pointer, sizeof(UInt64) * 4 / sizeof(Byte));
            }
        }
        
        public static implicit operator ReadOnlySpan<UInt16>(in Flag1024 value)
        {
            fixed (void* pointer = &value)
            {
                return new ReadOnlySpan<UInt16>(pointer, sizeof(UInt64) * 4 / sizeof(UInt16));
            }
        }
        
        public static implicit operator ReadOnlySpan<UInt32>(in Flag1024 value)
        {
            fixed (void* pointer = &value)
            {
                return new ReadOnlySpan<UInt32>(pointer, sizeof(UInt64) * 4 / sizeof(UInt32));
            }
        }
        
        public static implicit operator ReadOnlySpan<UInt64>(in Flag1024 value)
        {
            fixed (void* pointer = &value)
            {
                return new ReadOnlySpan<UInt64>(pointer, sizeof(UInt64) * 4 / sizeof(UInt64));
            }
        }
        
        public static Boolean operator ==(Flag1024 first, Flag1024 second)
        {
            return first.Low7 == second.Low7 && first.Low6 == second.Low6 &&
                   first.Low5 == second.Low5 && first.Low4 == second.Low4 &&
                   first.Low3 == second.Low3 && first.Low2 == second.Low2 &&
                   first.Low1 == second.Low1 && first.Low0 == second.Low0;
        }
        
        public static Boolean operator !=(Flag1024 first, Flag1024 second)
        {
            return !(first == second);
        }
        
        public static Flag1024 operator |(Flag1024 first, Flag1024 second)
        {
            return new Flag1024(first.Low7 | second.Low7, first.Low6 | second.Low6, first.Low5 | second.Low5, first.Low4 | second.Low4,
                               first.Low3 | second.Low3, first.Low2 | second.Low2, first.Low1 | second.Low1, first.Low0 | second.Low0);
        }
        
        public static Flag1024 operator &(Flag1024 first, Flag1024 second)
        {
            return new Flag1024(first.Low7 & second.Low7, first.Low6 & second.Low6, first.Low5 & second.Low5, first.Low4 & second.Low4,
                               first.Low3 & second.Low3, first.Low2 & second.Low2, first.Low1 & second.Low1, first.Low0 & second.Low0);
        }
        
        public static Flag1024 operator ^(Flag1024 first, Flag1024 second)
        {
            return new Flag1024(first.Low7 ^ second.Low7, first.Low6 ^ second.Low6, first.Low5 ^ second.Low5, first.Low4 ^ second.Low4,
                               first.Low3 ^ second.Low3, first.Low2 ^ second.Low2, first.Low1 ^ second.Low1, first.Low0 ^ second.Low0);
        }
        
        public static Flag1024 operator ~(Flag1024 value)
        {
            return new Flag1024(~value.Low7, ~value.Low6, ~value.Low5, ~value.Low4,
                               ~value.Low3, ~value.Low2, ~value.Low1, ~value.Low0);
        }
        
        public static Flag1024 operator <<(Flag1024 value, Int32 shift)
        {
            return value.BitwiseShiftLeft(shift);
        }
        
        public static Flag1024 operator >>(Flag1024 value, Int32 shift)
        {
            return value.BitwiseShiftRight(shift);
        }

        public Int32 Size
        {
            get
            {
                return sizeof(Flag1024);
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
        
        [field: FieldOffset(120)]
        internal UInt64 High7 { get; }
        
        [field: FieldOffset(112)]
        internal UInt64 High6 { get; }
        
        [field: FieldOffset(104)]
        internal UInt64 High5 { get; }
        
        [field: FieldOffset(96)]
        internal UInt64 High4 { get; }

        [field: FieldOffset(88)]
        internal UInt64 High3 { get; }
        
        [field: FieldOffset(80)]
        internal UInt64 High2 { get; }
        
        [field: FieldOffset(72)]
        internal UInt64 High1 { get; }
        
        [field: FieldOffset(64)]
        internal UInt64 High0 { get; }
        
        [field: FieldOffset(56)]
        internal UInt64 Low7 { get; }
        
        [field: FieldOffset(48)]
        internal UInt64 Low6 { get; }
        
        [field: FieldOffset(40)]
        internal UInt64 Low5 { get; }
        
        [field: FieldOffset(32)]
        internal UInt64 Low4 { get; }

        [field: FieldOffset(24)]
        internal UInt64 Low3 { get; }
        
        [field: FieldOffset(16)]
        internal UInt64 Low2 { get; }
        
        [field: FieldOffset(8)]
        internal UInt64 Low1 { get; }
        
        [field: FieldOffset(0)]
        internal UInt64 Low0 { get; }

        public Flag1024(UInt64 low)
            : this(0, low)
        {
        }
        
        public Flag1024(UInt64 high, UInt64 low)
            : this(0, high, low)
        {
        }
        
        public Flag1024(UInt64 high0, UInt64 low1, UInt64 low0)
            : this(0, high0, low1, low0)
        {
        }
        
        public Flag1024(UInt64 high1, UInt64 high0, UInt64 low1, UInt64 low0)
            : this(0, high1, high0, low1, low0)
        {
        }
        
        public Flag1024(UInt64 high0, UInt64 low3, UInt64 low2, UInt64 low1, UInt64 low0)
            : this(0, high0, low3, low2, low1, low0)
        {
        }
        
        public Flag1024(UInt64 high1, UInt64 high0, UInt64 low3, UInt64 low2, UInt64 low1, UInt64 low0)
            : this(0, high1, high0, low3, low2, low1, low0)
        {
        }
        
        public Flag1024(UInt64 low6, UInt64 low5, UInt64 low4, UInt64 low3, UInt64 low2, UInt64 low1, UInt64 low0)
            : this(0, low6, low5, low4, low3, low2, low1, low0)
        {
        }
        
        public Flag1024(UInt64 low7, UInt64 low6, UInt64 low5, UInt64 low4, UInt64 low3, UInt64 low2, UInt64 low1, UInt64 low0)
            : this(0, low7, low6, low5, low4, low3, low2, low1, low0)
        {
        }
        
        public Flag1024(UInt64 high0, UInt64 low7, UInt64 low6, UInt64 low5, UInt64 low4, UInt64 low3, UInt64 low2, UInt64 low1, UInt64 low0)
            : this(0, high0, low7, low6, low5, low4, low3, low2, low1, low0)
        {
        }
        
        public Flag1024(UInt64 high1, UInt64 high0, UInt64 low7, UInt64 low6, UInt64 low5, UInt64 low4, UInt64 low3, UInt64 low2, UInt64 low1, UInt64 low0)
            : this(0, high1, high0, low7, low6, low5, low4, low3, low2, low1, low0)
        {
        }
        
        public Flag1024(UInt64 high2, UInt64 high1, UInt64 high0, UInt64 low7, UInt64 low6, UInt64 low5, UInt64 low4, UInt64 low3, UInt64 low2, UInt64 low1, UInt64 low0)
            : this(0, high2, high1, high0, low7, low6, low5, low4, low3, low2, low1, low0)
        {
        }
        
        public Flag1024(UInt64 high3, UInt64 high2, UInt64 high1, UInt64 high0, UInt64 low7, UInt64 low6, UInt64 low5, UInt64 low4, UInt64 low3, UInt64 low2, UInt64 low1, UInt64 low0)
            : this(0, high3, high2, high1, high0, low7, low6, low5, low4, low3, low2, low1, low0)
        {
        }
        
        public Flag1024(UInt64 high4, UInt64 high3, UInt64 high2, UInt64 high1, UInt64 high0, UInt64 low7, UInt64 low6, UInt64 low5, UInt64 low4, UInt64 low3, UInt64 low2, UInt64 low1, UInt64 low0)
            : this(0, high4, high3, high2, high1, high0, low7, low6, low5, low4, low3, low2, low1, low0)
        {
        }
        
        public Flag1024(UInt64 high5, UInt64 high4, UInt64 high3, UInt64 high2, UInt64 high1, UInt64 high0, UInt64 low7, UInt64 low6, UInt64 low5, UInt64 low4, UInt64 low3, UInt64 low2, UInt64 low1, UInt64 low0)
            : this(0, high5, high4, high3, high2, high1, high0, low7, low6, low5, low4, low3, low2, low1, low0)
        {
        }
        
        public Flag1024(UInt64 high6, UInt64 high5, UInt64 high4, UInt64 high3, UInt64 high2, UInt64 high1, UInt64 high0, UInt64 low7, UInt64 low6, UInt64 low5, UInt64 low4, UInt64 low3, UInt64 low2, UInt64 low1, UInt64 low0)
            : this(0, high6, high5, high4, high3, high2, high1, high0, low7, low6, low5, low4, low3, low2, low1, low0)
        {
        }
        
        public Flag1024(UInt64 high7, UInt64 high6, UInt64 high5, UInt64 high4, UInt64 high3, UInt64 high2, UInt64 high1, UInt64 high0, UInt64 low7, UInt64 low6, UInt64 low5, UInt64 low4, UInt64 low3, UInt64 low2, UInt64 low1, UInt64 low0)
        {
            High7 = high7;
            High6 = high6;
            High5 = high5;
            High4 = high4;
            High3 = high3;
            High2 = high2;
            High1 = high1;
            High0 = high0;
            Low7 = low7;
            Low6 = low6;
            Low5 = low5;
            Low4 = low4;
            Low3 = low3;
            Low2 = low2;
            Low1 = low1;
            Low0 = low0;
        }
        
        public ReadOnlySpan<Byte> AsSpan()
        {
            return this;
        }

        public Boolean HasFlag<T>(T value) where T : unmanaged, Enum
        {
            return HasFlag(value.AsUInt64());
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
            return HasFlag(value.AsSpan());
        }

        public Boolean HasFlag(Flag512 value)
        {
            return HasFlag(value.AsSpan());
        }

        public Boolean HasFlag(Flag1024 value)
        {
            return (High7 & value.High7) == value.High7 && (High6 & value.High6) == value.High6 && (High5 & value.High5) == value.High5 && (High4 & value.High4) == value.High4 &&
                   (High3 & value.High3) == value.High3 && (High2 & value.High2) == value.High2 && (High1 & value.High1) == value.High1 && (High0 & value.High0) == value.High0 && 
                   (Low7 & value.Low7) == value.Low7 && (Low6 & value.Low6) == value.Low6 && (Low5 & value.Low5) == value.Low5 && (Low4 & value.Low4) == value.Low4 &&
                   (Low3 & value.Low3) == value.Low3 && (Low2 & value.Low2) == value.Low2 && (Low1 & value.Low1) == value.Low1 && (Low0 & value.Low0) == value.Low0;
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
            return HashCodeUtilities.Combine(High7, High6, High5, High4, High3, High2, High1, High0, Low7, Low6, Low5, Low4, Low3, Low2, Low1, Low0);
        }

        public override Boolean Equals(Object? obj)
        {
            return obj switch
            {
                null => false,
                Flag64 value => Equals(value),
                Flag128 value => Equals(value),
                Flag256 value => Equals(value),
                Flag512 value => Equals(value),
                Flag1024 value => Equals(value),
                IFlag value => Equals(value),
                IConvertible convertible => Equals(convertible.ToUInt64()),
                _ => false
            };
        }

        public Boolean Equals(ReadOnlySpan<Byte> value)
        {
            return AsSpan().BitwiseEquals(value);
        }

        public Boolean Equals(Flag1024 other)
        {
            return this == other;
        }

        public Boolean Equals(IFlag? other)
        {
            return other is not null && Equals(other.AsSpan());
        }
        
        public override String ToString()
        {
            return Convert.ToString(unchecked((Int64) Low7), 2) + Convert.ToString(unchecked((Int64) Low6), 2) +
                   Convert.ToString(unchecked((Int64) Low5), 2) + Convert.ToString(unchecked((Int64) Low4), 2) +
                   Convert.ToString(unchecked((Int64) Low3), 2) + Convert.ToString(unchecked((Int64) Low2), 2) +
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
                return (new Flag1024(1) << index & this) == this;
            }
        }
    }
}