// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using NetExtender.Initializer.Types.Flags.Interfaces;
using NetExtender.Utilities.Numerics;
using NetExtender.Utilities.Types;

namespace NetExtender.Initializer.Types.Flags
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public readonly unsafe struct Flag64 : IFlag, IEquatable<Flag64>
    {
        public static implicit operator UInt64(Flag64 value)
        {
            return value.Internal;
        }
        
        public static implicit operator Flag64(UInt64 value)
        {
            return new Flag64(value);
        }
        
        public static implicit operator ReadOnlySpan<Byte>(in Flag64 value)
        {
            fixed (void* pointer = &value)
            {
                return new ReadOnlySpan<Byte>(pointer, sizeof(UInt64) / sizeof(Byte));
            }
        }
        
        public static implicit operator ReadOnlySpan<UInt16>(in Flag64 value)
        {
            fixed (void* pointer = &value)
            {
                return new ReadOnlySpan<UInt16>(pointer, sizeof(UInt64) / sizeof(UInt16));
            }
        }
        
        public static implicit operator ReadOnlySpan<UInt32>(in Flag64 value)
        {
            fixed (void* pointer = &value)
            {
                return new ReadOnlySpan<UInt32>(pointer, sizeof(UInt64) / sizeof(UInt32));
            }
        }
        
        public static implicit operator ReadOnlySpan<UInt64>(in Flag64 value)
        {
            fixed (void* pointer = &value)
            {
                return new ReadOnlySpan<UInt64>(pointer, sizeof(UInt64) / sizeof(UInt64));
            }
        }
        
        public static Boolean operator ==(Flag64 first, Flag64 second)
        {
            return first.Internal == second.Internal;
        }
        
        public static Boolean operator !=(Flag64 first, Flag64 second)
        {
            return !(first == second);
        }
        
        public static Flag64 operator |(Flag64 first, Flag64 second)
        {
            return new Flag64(first.Internal | second.Internal);
        }
        
        public static Flag64 operator &(Flag64 first, Flag64 second)
        {
            return new Flag64(first.Internal & second.Internal);
        }
        
        public static Flag64 operator ^(Flag64 first, Flag64 second)
        {
            return new Flag64(first.Internal ^ second.Internal);
        }
        
        public static Flag64 operator ~(Flag64 value)
        {
            return new Flag64(~value.Internal);
        }
        
        public static Flag64 operator <<(Flag64 value, Int32 shift)
        {
            return value.BitwiseShiftLeft(shift);
        }
        
        public static Flag64 operator >>(Flag64 value, Int32 shift)
        {
            return value.BitwiseShiftRight(shift);
        }

        public Int32 Size
        {
            get
            {
                return sizeof(Flag64);
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

        internal UInt64 Internal { get; }
        
        public Flag64(UInt64 value)
        {
            Internal = value;
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
        
        public Boolean HasFlag(Flag64 value)
        {
            return (Internal & value.Internal) == value.Internal;
        }

        public Boolean HasFlag<T>(T value) where T : unmanaged, Enum
        {
            return HasFlag(value.AsUInt64());
        }

        public Boolean HasIFlag<T>(T value) where T : IFlag
        {
            return HasFlag(value.AsSpan());
        }
        
        public override Int32 GetHashCode()
        {
            return Internal.GetHashCode();
        }

        public override Boolean Equals(Object? obj)
        {
            return obj switch
            {
                null => false,
                Flag64 value => Equals(value),
                IFlag value => Equals(value),
                IConvertible convertible => Equals(convertible.ToUInt64()),
                _ => false
            };
        }

        public Boolean Equals(ReadOnlySpan<Byte> value)
        {
            return AsSpan().BitwiseEquals(value);
        }

        public Boolean Equals(Flag64 other)
        {
            return this == other;
        }

        public Boolean Equals(IFlag? other)
        {
            return other is not null && Equals(other.AsSpan());
        }

        public override String ToString()
        {
            return Convert.ToString(unchecked((Int64) Internal), 2);
        }
        
        public IEnumerable<Int32> EnumerateSetBits()
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
                return (new Flag64(1) << index & this) == this;
            }
        }
    }
}