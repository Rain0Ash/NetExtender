// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using NetExtender.Utilities.Numerics;

namespace System
{
    [Serializable]
    public readonly struct Char32 : IComparable, IComparable<Char>, IEquatable<Char>, IComparable<Char32>, IEquatable<Char32>, IConvertible, ISpanFormattable
    {
        public static explicit operator Char(Char32 value)
        {
            return value.Value <= Char.MaxValue ? (Char) value.Value : throw new OverflowException();
        }

        public static implicit operator String(Char32 value)
        {
            return Char.ConvertFromUtf32(unchecked((Int32) value.Value));
        }

        public static implicit operator UInt32(Char32 value)
        {
            return value.Value;
        }
        
        public static implicit operator Char32(Char value)
        {
            return new Char32(value);
        }
        
        public static implicit operator Char32(SByte value)
        {
            return new Char32(value);
        }

        public static implicit operator Char32(Byte value)
        {
            return new Char32(value);
        }
        
        public static implicit operator Char32(Int16 value)
        {
            return new Char32(value);
        }

        public static implicit operator Char32(UInt16 value)
        {
            return new Char32(value);
        }

        public static implicit operator Char32(Int32 value)
        {
            return new Char32(value);
        }

        public static implicit operator Char32(UInt32 value)
        {
            return new Char32(value);
        }
        
        public static Boolean operator ==(Char32 first, Int32 second)
        {
            return first.Value == second;
        }
        
        public static Boolean operator ==(Int32 first, Char32 second)
        {
            return first == second.Value;
        }
        
        public static Boolean operator ==(Char32 first, UInt32 second)
        {
            return first.Value == second;
        }
        
        public static Boolean operator ==(UInt32 first, Char32 second)
        {
            return first == second.Value;
        }
        
        public static Boolean operator ==(Char32 first, Char second)
        {
            return first.Value == second;
        }
        
        public static Boolean operator ==(Char first, Char32 second)
        {
            return first == second.Value;
        }
        
        public static Boolean operator ==(Char32 first, Char32 second)
        {
            return first.Value == second.Value;
        }
        
        public static Boolean operator !=(Char32 first, Int32 second)
        {
            return !(first == second);
        }
        
        public static Boolean operator !=(Int32 first, Char32 second)
        {
            return !(first == second);
        }
        
        public static Boolean operator !=(Char32 first, UInt32 second)
        {
            return !(first == second);
        }
        
        public static Boolean operator !=(UInt32 first, Char32 second)
        {
            return !(first == second);
        }
        
        public static Boolean operator !=(Char32 first, Char second)
        {
            return !(first == second);
        }
        
        public static Boolean operator !=(Char first, Char32 second)
        {
            return !(first == second);
        }

        public static Boolean operator !=(Char32 first, Char32 second)
        {
            return !(first == second);
        }
        
        public static Boolean operator >(Char32 first, Int32 second)
        {
            return first.Value > second;
        }
        
        public static Boolean operator >(Int32 first, Char32 second)
        {
            return first > second.Value;
        }
        
        public static Boolean operator >(Char32 first, UInt32 second)
        {
            return first.Value > second;
        }
        
        public static Boolean operator >(UInt32 first, Char32 second)
        {
            return first > second.Value;
        }
        
        public static Boolean operator >(Char32 first, Char second)
        {
            return first.Value > second;
        }
        
        public static Boolean operator >(Char first, Char32 second)
        {
            return first > second.Value;
        }
        
        public static Boolean operator >(Char32 first, Char32 second)
        {
            return first.Value > second.Value;
        }
        
        public static Boolean operator >=(Char32 first, Int32 second)
        {
            return first.Value >= second;
        }
        
        public static Boolean operator >=(Int32 first, Char32 second)
        {
            return first >= second.Value;
        }
        
        public static Boolean operator >=(Char32 first, UInt32 second)
        {
            return first.Value >= second;
        }
        
        public static Boolean operator >=(UInt32 first, Char32 second)
        {
            return first >= second.Value;
        }
        
        public static Boolean operator >=(Char32 first, Char second)
        {
            return first.Value >= second;
        }
        
        public static Boolean operator >=(Char first, Char32 second)
        {
            return first >= second.Value;
        }
        
        public static Boolean operator >=(Char32 first, Char32 second)
        {
            return first.Value >= second.Value;
        }
        
        public static Boolean operator <(Char32 first, Int32 second)
        {
            return first.Value < second;
        }
        
        public static Boolean operator <(Int32 first, Char32 second)
        {
            return first < second.Value;
        }
        
        public static Boolean operator <(Char32 first, UInt32 second)
        {
            return first.Value < second;
        }
        
        public static Boolean operator <(UInt32 first, Char32 second)
        {
            return first < second.Value;
        }
        
        public static Boolean operator <(Char32 first, Char second)
        {
            return first.Value < second;
        }
        
        public static Boolean operator <(Char first, Char32 second)
        {
            return first < second.Value;
        }
        
        public static Boolean operator <(Char32 first, Char32 second)
        {
            return first.Value < second.Value;
        }
        
        public static Boolean operator <=(Char32 first, Int32 second)
        {
            return first.Value <= second;
        }
        
        public static Boolean operator <=(Int32 first, Char32 second)
        {
            return first <= second.Value;
        }
        
        public static Boolean operator <=(Char32 first, UInt32 second)
        {
            return first.Value <= second;
        }
        
        public static Boolean operator <=(UInt32 first, Char32 second)
        {
            return first <= second.Value;
        }
        
        public static Boolean operator <=(Char32 first, Char second)
        {
            return first.Value <= second;
        }
        
        public static Boolean operator <=(Char first, Char32 second)
        {
            return first <= second.Value;
        }
        
        public static Boolean operator <=(Char32 first, Char32 second)
        {
            return first.Value <= second.Value;
        }

        public static Char32 operator +(Char32 first, Int32 second)
        {
            Int64 result = first.Value + second;
            return result is >= 0 and <= UInt32.MaxValue ? new Char32((UInt32) result) : throw new OverflowException();
        }

        public static Char32 operator +(Int32 first, Char32 second)
        {
            Int64 result = first + second.Value;
            return result is >= 0 and <= UInt32.MaxValue ? new Char32((UInt32) result) : throw new OverflowException();
        }

        public static Char32 operator +(Char32 first, UInt32 second)
        {
            return new Char32(first.Value + second);
        }

        public static Char32 operator +(UInt32 first, Char32 second)
        {
            return new Char32(first + second.Value);
        }

        public static Char32 operator +(Char32 first, Char second)
        {
            return new Char32(first.Value + second);
        }

        public static Char32 operator +(Char first, Char32 second)
        {
            return new Char32(first + second.Value);
        }

        public static Char32 operator +(Char32 first, Char32 second)
        {
            return new Char32(first.Value + second.Value);
        }

        public static Char32 operator -(Char32 first, Int32 second)
        {
            Int64 result = first.Value - second;
            return result is >= 0 and <= UInt32.MaxValue ? new Char32((UInt32) result) : throw new OverflowException();
        }

        public static Char32 operator -(Int32 first, Char32 second)
        {
            Int64 result = first - second.Value;
            return result is >= 0 and <= UInt32.MaxValue ? new Char32((UInt32) result) : throw new OverflowException();
        }

        public static Char32 operator -(Char32 first, UInt32 second)
        {
            return new Char32(first.Value - second);
        }

        public static Char32 operator -(UInt32 first, Char32 second)
        {
            return new Char32(first - second.Value);
        }

        public static Char32 operator -(Char32 first, Char second)
        {
            return new Char32(first.Value - second);
        }

        public static Char32 operator -(Char first, Char32 second)
        {
            return new Char32(first - second.Value);
        }

        public static Char32 operator -(Char32 first, Char32 second)
        {
            return new Char32(first.Value - second.Value);
        }

        private UInt32 Value { get; }

        private Char32(Char value)
        {
            Value = value;
        }

        private Char32(Int32 value)
        {
            Value = unchecked((UInt32) value);
        }

        private Char32(UInt32 value)
        {
            Value = value;
        }

        public Char32(Char high, Char low)
        {
            Value = unchecked((UInt32) Char.ConvertToUtf32(high, low));
        }
        
        public void Deconstruct(out Char high, out Char low)
        {
            if (Value <= Char.MaxValue)
            {
                high = Char.MinValue;
                low = (Char) Value;
                return;
            }

            high = (Char) (Value >> sizeof(UInt16) * BitUtilities.BitInByte);
            low = unchecked((Char) Value);
        }

        public Boolean ToChar(out Char result)
        {
            if (Value is >= 0 and <= Char.MaxValue)
            {
                result = (Char) Value;
                return true;
            }

            result = default;
            return false;
        }

        public Int32 CompareTo(Object? obj)
        {
            return obj switch
            {
                Char32 character => CompareTo(character),
                Char character => CompareTo(character),
                _ => Value.CompareTo(obj)
            };
        }

        public Int32 CompareTo(Char other)
        {
            return Value.CompareTo(other);
        }

        public Int32 CompareTo(Char32 other)
        {
            return Value.CompareTo(other.Value);
        }

        public Boolean Equals(Char other)
        {
            return Value == other;
        }

        public Boolean Equals(Char32 other)
        {
            return Value == other.Value;
        }

        public override Boolean Equals(Object? obj)
        {
            return base.Equals(obj);
        }

        public override Int32 GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override String ToString()
        {
            return Value <= Char.MaxValue ? ((Char) Value).ToString() : Char.ConvertFromUtf32(unchecked((Int32) Value));
        }

        public String ToString(IFormatProvider? provider)
        {
            return Value <= Char.MaxValue ? ((Char) Value).ToString(provider) : Char.ConvertFromUtf32(unchecked((Int32) Value)).ToString(provider);
        }

        public String ToString(String? format, IFormatProvider? provider)
        {
            return Value <= Char.MaxValue ? ((IFormattable) (Char) Value).ToString(format, provider) : Char.ConvertFromUtf32(unchecked((Int32) Value)).ToString(provider);
        }

        Boolean ISpanFormattable.TryFormat(Span<Char> destination, out Int32 written, ReadOnlySpan<Char> format, IFormatProvider? provider)
        {
            if (destination.IsEmpty)
            {
                written = 0;
                return false;
            }

            if (Value <= Char.MaxValue)
            {
                destination[0] = (Char) Value;
                written = 1;
                return true;
            }
            
            if (destination.Length < 2)
            {
                written = 0;
                return false;
            }
            
            destination[0] = (Char) Value.High();
            destination[1] = (Char) Value.Low();
            written = 2;
            return true;
        }

        public TypeCode GetTypeCode()
        {
            return TypeCode.Char;
        }

        Boolean IConvertible.ToBoolean(IFormatProvider? provider)
        {
            return ((IConvertible) Value).ToBoolean(provider);
        }

        Char IConvertible.ToChar(IFormatProvider? provider)
        {
            return ((IConvertible) Value).ToChar(provider);
        }

        SByte IConvertible.ToSByte(IFormatProvider? provider)
        {
            return ((IConvertible) Value).ToSByte(provider);
        }

        Byte IConvertible.ToByte(IFormatProvider? provider)
        {
            return ((IConvertible) Value).ToByte(provider);
        }

        Int16 IConvertible.ToInt16(IFormatProvider? provider)
        {
            return ((IConvertible) Value).ToInt16(provider);
        }

        UInt16 IConvertible.ToUInt16(IFormatProvider? provider)
        {
            return ((IConvertible) Value).ToUInt16(provider);
        }

        Int32 IConvertible.ToInt32(IFormatProvider? provider)
        {
            return ((IConvertible) Value).ToInt32(provider);
        }

        UInt32 IConvertible.ToUInt32(IFormatProvider? provider)
        {
            return ((IConvertible) Value).ToUInt32(provider);
        }

        Int64 IConvertible.ToInt64(IFormatProvider? provider)
        {
            return ((IConvertible) Value).ToInt64(provider);
        }

        UInt64 IConvertible.ToUInt64(IFormatProvider? provider)
        {
            return ((IConvertible) Value).ToUInt64(provider);
        }

        Single IConvertible.ToSingle(IFormatProvider? provider)
        {
            return ((IConvertible) Value).ToSingle(provider);
        }

        Double IConvertible.ToDouble(IFormatProvider? provider)
        {
            return ((IConvertible) Value).ToDouble(provider);
        }

        Decimal IConvertible.ToDecimal(IFormatProvider? provider)
        {
            return ((IConvertible) Value).ToDecimal(provider);
        }

        DateTime IConvertible.ToDateTime(IFormatProvider? provider)
        {
            return ((IConvertible) Value).ToDateTime(provider);
        }

        Object IConvertible.ToType(Type type, IFormatProvider? provider)
        {
            return ((IConvertible) Value).ToType(type, provider);
        }
    }
}