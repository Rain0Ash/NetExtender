using System;
using System.Runtime.CompilerServices;

namespace NetExtender.Types.Numerics
{
    public readonly struct Percent : IEquatable<Percent>, IComparable<Percent>, IConvertible, IFormattable
    {
        public static implicit operator Percent(SByte value)
        {
            return FromPercentUnchecked(value);
        }
        
        public static implicit operator Percent(Byte value)
        {
            return FromPercentUnchecked(value);
        }
        
        public static implicit operator Percent(Int16 value)
        {
            return FromPercentUnchecked(value);
        }
        
        public static implicit operator Percent(UInt16 value)
        {
            return FromPercentUnchecked(value);
        }
        
        public static implicit operator Percent(Int32 value)
        {
            return FromPercentUnchecked(value);
        }
        
        public static implicit operator Percent(UInt32 value)
        {
            return FromPercentUnchecked(value);
        }
        
        public static implicit operator Percent(Int64 value)
        {
            return FromPercentUnchecked(value);
        }
        
        public static implicit operator Percent(UInt64 value)
        {
            return FromPercentUnchecked(value);
        }
        
        public static implicit operator Percent(Single value)
        {
            return FromPercent(value);
        }
        
        public static implicit operator Percent(Double value)
        {
            return FromPercent(value);
        }
        
        public static implicit operator Percent(Decimal value)
        {
            return FromPercent(value);
        }
        
        public static implicit operator Single(Percent value)
        {
            Double result = value;
            return (Single) result;
        }
        
        public static implicit operator Double(Percent value)
        {
            return value.Value;
        }
        
        public static implicit operator Decimal(Percent value)
        {
            Double result = value;
            return (Decimal) result;
        }
        
        public static Boolean operator ==(Percent first, Percent second)
        {
            return Math.Abs(first.Value - second.Value) < Double.Epsilon;
        }
        
        public static Boolean operator !=(Percent first, Percent second)
        {
            return !(first == second);
        }
        
        public static Boolean operator <(Percent first, Percent second)
        {
            return first.Value < second.Value;
        }
        
        public static Boolean operator >(Percent first, Percent second)
        {
            return first.Value > second.Value;
        }
        
        public static Boolean operator <=(Percent first, Percent second)
        {
            return first.Value <= second.Value;
        }
        
        public static Boolean operator >=(Percent first, Percent second)
        {
            return first.Value >= second.Value;
        }
        
        public static Percent operator +(Percent first, Percent second)
        {
            return Unchecked(first.Value + second.Value);
        }
        
        public static Percent operator -(Percent first, Percent second)
        {
            return Unchecked(first.Value + second.Value);
        }
        
        public static Percent operator *(Percent first, Percent second)
        {
            return Unchecked(first.Value * second.Value);
        }
        
        public static Percent operator /(Percent first, Percent second)
        {
            return Unchecked(first.Value / second.Value);
        }
        
        public static Percent operator %(Percent first, Percent second)
        {
            return Unchecked(first.Value % second.Value);
        }

        private Double Value { get; }
        
        private Percent(Double value)
        {
            Value = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Percent Checked(Double value)
        {
            return Double.IsFinite(value) ? new Percent(value) : throw new InvalidCastException($"Value '{value}' not supported.");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Percent Unchecked(Double value)
        {
            return new Percent(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Percent FromReal(Double value)
        {
            return Checked(value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Percent FromReal(Decimal value)
        {
            return Unchecked((Double) value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Percent FromPercent(Double value)
        {
            return FromReal(value / 100);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Percent FromPercentUnchecked(Double value)
        {
            return Unchecked(value / 100);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Percent FromPercent(Decimal value)
        {
            return FromReal(value / 100);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Percent FromPromille(Double value)
        {
            return FromReal(value / 1000);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Percent FromPromille(Decimal value)
        {
            return FromReal(value / 1000);
        }
        
        TypeCode IConvertible.GetTypeCode()
        {
            return Value.GetTypeCode();
        }
        
        Object IConvertible.ToType(Type conversionType, IFormatProvider? provider)
        {
            return ((IConvertible) Value).ToType(conversionType, provider);
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
        
        public Int32 CompareTo(Percent other)
        {
            return Value.CompareTo(other.Value);
        }
        
        public override Int32 GetHashCode()
        {
            return Value.GetHashCode();
        }
        
        public override Boolean Equals(Object? other)
        {
            return other switch
            {
                Percent percent => Equals(percent),
                _ => Value.Equals(other)
            };
        }
        
        public Boolean Equals(Percent other)
        {
            return this == other;
        }
        
        public override String ToString()
        {
            return $"{Value * 100}%";
        }
        
        public String ToString(String? format, IFormatProvider? provider)
        {
            return $"{(Value * 100).ToString(format, provider)}%";
        }
        
        public String ToString(IFormatProvider? provider)
        {
            return Value.ToString(provider);
        }
    }
}