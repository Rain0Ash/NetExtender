// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using NetExtender.Initializer.Types.Flags.Interfaces;
using NetExtender.Utilities.Numerics;
using NetExtender.Utilities.Types;

namespace NetExtender.Initializer.Types.Flags
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public readonly unsafe struct EnumFlag<T> : IFlag, IEquatable<EnumFlag<T>> where T : unmanaged, Enum
    {
        public static implicit operator UInt64(EnumFlag<T> value)
        {
            return value.Internal.AsUInt64();
        }
        
        public static implicit operator Flag64(EnumFlag<T> value)
        {
            return new Flag64(value);
        }
        
        public static implicit operator EnumFlag<T>(T value)
        {
            return new EnumFlag<T>(value);
        }
        
        public static implicit operator ReadOnlySpan<Byte>(in EnumFlag<T> value)
        {
            fixed (void* pointer = &value)
            {
                return new ReadOnlySpan<Byte>(pointer, sizeof(T) / sizeof(Byte));
            }
        }
        
        public static implicit operator ReadOnlySpan<UInt16>(in EnumFlag<T> value)
        {
            fixed (void* pointer = &value)
            {
                return new ReadOnlySpan<UInt16>(pointer, sizeof(T) / sizeof(UInt16));
            }
        }
        
        public static implicit operator ReadOnlySpan<UInt32>(in EnumFlag<T> value)
        {
            fixed (void* pointer = &value)
            {
                return new ReadOnlySpan<UInt32>(pointer, sizeof(T) / sizeof(UInt32));
            }
        }
        
        public static implicit operator ReadOnlySpan<UInt64>(in EnumFlag<T> value)
        {
            fixed (void* pointer = &value)
            {
                return new ReadOnlySpan<UInt64>(pointer, sizeof(T) / sizeof(UInt64));
            }
        }
        
        public static Boolean operator ==(EnumFlag<T> first, T second)
        {
            return first.Internal.Equals(second);
        }
        
        public static Boolean operator !=(EnumFlag<T> first, T second)
        {
            return !(first == second);
        }
        
        public static Boolean operator ==(EnumFlag<T> first, EnumFlag<T> second)
        {
            return first.Internal.Equals(second.Internal);
        }
        
        public static Boolean operator !=(EnumFlag<T> first, EnumFlag<T> second)
        {
            return !(first == second);
        }
        
        public static EnumFlag<T> operator |(EnumFlag<T> first, EnumFlag<T> second)
        {
            return new EnumFlag<T>(first.Internal.AsUInt64() | second.Internal.AsUInt64());
        }
        
        public static EnumFlag<T> operator &(EnumFlag<T> first, EnumFlag<T> second)
        {
            return new EnumFlag<T>(first.Internal.AsUInt64() & second.Internal.AsUInt64());
        }
        
        public static EnumFlag<T> operator ^(EnumFlag<T> first, EnumFlag<T> second)
        {
            return new EnumFlag<T>(first.Internal.AsUInt64() ^ second.Internal.AsUInt64());
        }
        
        public static EnumFlag<T> operator ~(EnumFlag<T> value)
        {
            return new EnumFlag<T>(~value.Internal.AsUInt64());
        }
        
        public static EnumFlag<T> operator <<(EnumFlag<T> value, Int32 shift)
        {
            return new EnumFlag<T>(value.Internal.AsUInt64() << shift);
        }
        
        public static EnumFlag<T> operator >>(EnumFlag<T> value, Int32 shift)
        {
            return new EnumFlag<T>(value.Internal.AsUInt64() >> shift);
        }

        public Int32 Size
        {
            get
            {
                return sizeof(EnumFlag<T>);
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

        internal T Internal { get; }

        private EnumFlag(UInt64 value)
        {
            Internal = Unsafe.As<UInt64, T>(ref value);
        }

        public EnumFlag(T value)
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
        
        public Boolean HasFlag(T value)
        {
            UInt64 result = value.AsUInt64();
            return (Internal.AsUInt64() & result) == result;
        }
        
        public Boolean HasFlag(EnumFlag<T> value)
        {
            return HasFlag(value.Internal);
        }
        
        public Boolean HasFlag<TEnum>(TEnum value) where TEnum : unmanaged, Enum
        {
            return HasFlag(MemoryMarshal.CreateSpan(ref value, 1).AsBytes());
        }
        
        public Boolean HasIFlag<TFlag>(TFlag value) where TFlag : IFlag
        {
            return HasFlag(value.AsSpan());
        }

        public override Int32 GetHashCode()
        {
            return HashCode.Combine(Internal);
        }

        public override Boolean Equals(Object? obj)
        {
            return obj switch
            {
                null => false,
                T value => Equals(value),
                EnumFlag<T> value => Equals(value),
                IFlag value => Equals(value),
                _ => false
            };
        }

        public Boolean Equals(ReadOnlySpan<Byte> value)
        {
            return AsSpan().BitwiseEquals(value);
        }

        public Boolean Equals(T other)
        {
            return this == other;
        }

        public Boolean Equals(EnumFlag<T> other)
        {
            return this == other;
        }

        public Boolean Equals(IFlag? other)
        {
            return other is not null && Equals(other.AsSpan());
        }
        
        public override String ToString()
        {
            return Internal.ToString();
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