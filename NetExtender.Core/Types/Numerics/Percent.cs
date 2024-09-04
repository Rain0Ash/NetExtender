using System;
using System.Runtime.CompilerServices;

namespace NetExtender.Types.Numerics
{
    //TODO:
    public readonly struct Percent /*: IEquatable<Percent>, IComparable<Percent>, IConvertible, IFormattable*/
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
            return value.Value * 100;
        }
        
        public static implicit operator Decimal(Percent value)
        {
            Double result = value;
            return (Decimal) result;
        }
        
        public static Percent operator +(Percent first, Percent second)
        {
            return Unchecked(first.Value + second.Value);
        }
        
        public static Percent operator -(Percent first, Percent second)
        {
            return Unchecked(first.Value + second.Value);
        }
        
        public static Percent operator *(Percent first, SByte second)
        {
            return Unchecked(first.Value * second);
        }
        
        public static Percent operator *(SByte first, Percent second)
        {
            return Unchecked(first * second.Value);
        }
        
        public static Percent operator *(Percent first, Byte second)
        {
            return Unchecked(first.Value * second);
        }
        
        public static Percent operator *(Byte first, Percent second)
        {
            return Unchecked(first * second.Value);
        }
        
        public static Percent operator *(Percent first, Int16 second)
        {
            return Unchecked(first.Value * second);
        }
        
        public static Percent operator *(Int16 first, Percent second)
        {
            return Unchecked(first * second.Value);
        }
        
        public static Percent operator *(Percent first, UInt16 second)
        {
            return Unchecked(first.Value * second);
        }
        
        public static Percent operator *(UInt16 first, Percent second)
        {
            return Unchecked(first * second.Value);
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
        
        public override Int32 GetHashCode()
        {
            return Value.GetHashCode();
        }
        
        public override Boolean Equals(Object? other)
        {
            return Value.Equals(other);
        }
        
        public override String ToString()
        {
            return $"{Value * 100}%";
        }
    }
}