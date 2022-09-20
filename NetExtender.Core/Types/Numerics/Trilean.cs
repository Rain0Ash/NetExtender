// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Types.Numerics
{
    public readonly struct Trilean : IConvertible, IEquatable<Trilean>, IEquatable<Boolean>, IEquatable<Boolean?>, IComparable<Trilean>
    {
        private enum State : Byte
        {
            False,
            Trit,
            True
        }

        public static Trilean True { get; } = new Trilean(State.True);
        public static Trilean Trit { get; } = new Trilean(State.Trit);
        public static Trilean False { get; } = new Trilean(State.False);

        public static implicit operator Boolean(Trilean value)
        {
#if TritIsFalse
            return value.IsTrue;
#else
            return !value.IsFalse;
#endif
        }

        public static implicit operator Trilean(Boolean value)
        {
            return value ? True : False;
        }

        public static implicit operator Boolean?(Trilean value)
        {
            return value.Value switch
            {
                State.True => true,
                State.False => false,
                State.Trit => null,
                _ => throw new ArgumentOutOfRangeException(nameof(value), value.Value, null)
            };
        }

        public static implicit operator Trilean(Boolean? value)
        {
            return value switch
            {
                true => True,
                false => False,
                null => Trit
            };
        }

        public static Boolean operator ==(Trilean first, Trilean second)
        {
            return first.Value == second.Value;
        }

        public static Boolean operator !=(Trilean first, Trilean second)
        {
            return first.Value != second.Value;
        }

        public static Boolean operator ==(Trilean first, Boolean second)
        {
            return (Boolean) first == second;
        }

        public static Boolean operator !=(Trilean first, Boolean second)
        {
            return (Boolean) first != second;
        }

        public static Boolean operator ==(Trilean first, Boolean? second)
        {
            return (Boolean?) first == second;
        }

        public static Boolean operator !=(Trilean first, Boolean? second)
        {
            return (Boolean?) first != second;
        }

        public static Boolean operator ==(Boolean first, Trilean second)
        {
            return second == first;
        }

        public static Boolean operator !=(Boolean first, Trilean second)
        {
            return second != first;
        }

        public static Boolean operator ==(Boolean? first, Trilean second)
        {
            return second == first;
        }

        public static Boolean operator !=(Boolean? first, Trilean second)
        {
            return second != first;
        }

        public static Boolean operator true(Trilean value)
        {
            return value.IsTrue;
        }

        public static Boolean operator false(Trilean value)
        {
            return value.IsFalse;
        }

        public static Trilean operator !(Trilean value)
        {
            return value.Value switch
            {
                State.True => False,
                State.Trit => Trit,
                State.False => True,
                _ => throw new ArgumentOutOfRangeException(nameof(value), value.Value, null)
            };
        }

        public static Trilean operator &(Trilean first, Trilean second)
        {
            return first.Value switch
            {
                State.True => second.Value switch
                {
                    State.True => True,
                    State.Trit => Trit,
                    State.False => False,
                    _ => throw new ArgumentOutOfRangeException(nameof(second), second.Value, null)
                },
                State.Trit => second.Value switch
                {
                    State.True => Trit,
                    State.Trit => Trit,
                    State.False => False,
                    _ => throw new ArgumentOutOfRangeException(nameof(second), second.Value, null)
                },
                State.False => False,
                _ => throw new ArgumentOutOfRangeException(nameof(first), first.Value, null)
            };
        }

        public static Trilean operator &(Trilean first, Boolean second)
        {
            return first.Value switch
            {
                State.True => second ? True : False,
                State.Trit => second ? Trit : False,
                State.False => False,
                _ => throw new ArgumentOutOfRangeException(nameof(first), first.Value, null)
            };
        }

        public static Trilean operator &(Trilean first, Boolean? second)
        {
            return first.Value switch
            {
                State.True => second switch
                {
                    true => True,
                    false => False,
                    null => Trit
                },
                State.Trit => second switch
                {
                    true => Trit,
                    false => False,
                    null => Trit
                },
                State.False => False,
                _ => throw new ArgumentOutOfRangeException(nameof(first), first.Value, null)
            };
        }

        public static Trilean operator &(Boolean first, Trilean second)
        {
            return second & first;
        }

        public static Trilean operator &(Boolean? first, Trilean second)
        {
            return second & first;
        }

        public static Trilean operator |(Trilean first, Trilean second)
        {
            return first.Value switch
            {
                State.True => True,
                State.Trit => second.Value switch
                {
                    State.True => True,
                    State.Trit => Trit,
                    State.False => Trit,
                    _ => throw new ArgumentOutOfRangeException(nameof(second), second.Value, null)
                },
                State.False => second.Value switch
                {
                    State.True => True,
                    State.Trit => Trit,
                    State.False => False,
                    _ => throw new ArgumentOutOfRangeException(nameof(second), second.Value, null)
                },
                _ => throw new ArgumentOutOfRangeException(nameof(first), first.Value, null)
            };
        }

        public static Trilean operator |(Trilean first, Boolean second)
        {
            return first.Value switch
            {
                State.True => True,
                State.Trit => second ? True : Trit,
                State.False => second ? True : False,
                _ => throw new ArgumentOutOfRangeException(nameof(first), first.Value, null)
            };
        }

        public static Trilean operator |(Trilean first, Boolean? second)
        {
            return first.Value switch
            {
                State.True => True,
                State.Trit => second switch
                {
                    true => True,
                    false => Trit,
                    null => Trit
                },
                State.False => second switch
                {
                    true => True,
                    false => False,
                    null => Trit
                },
                _ => throw new ArgumentOutOfRangeException(nameof(first), first.Value, null)
            };
        }

        public static Trilean operator |(Boolean first, Trilean second)
        {
            return second | first;
        }

        public static Trilean operator |(Boolean? first, Trilean second)
        {
            return second | first;
        }

        public static Trilean operator ^(Trilean first, Trilean second)
        {
            return first.Value switch
            {
                State.True => second.Value switch
                {
                    State.True => False,
                    State.Trit => Trit,
                    State.False => True,
                    _ => throw new ArgumentOutOfRangeException(nameof(second), second.Value, null)
                },
                State.Trit => Trit,
                State.False => second.Value switch
                {
                    State.True => True,
                    State.Trit => Trit,
                    State.False => False,
                    _ => throw new ArgumentOutOfRangeException(nameof(second), second.Value, null)
                },
                _ => throw new ArgumentOutOfRangeException(nameof(first), first.Value, null)
            };
        }

        public static Trilean operator ^(Trilean first, Boolean second)
        {
            return first.Value switch
            {
                State.True => second ? False : True,
                State.Trit => Trit,
                State.False => second ? True : False,
                _ => throw new ArgumentOutOfRangeException(nameof(first), first.Value, null)
            };
        }

        public static Trilean operator ^(Trilean first, Boolean? second)
        {
            return first.Value switch
            {
                State.True => second switch
                {
                    true => False,
                    false => True,
                    null => Trit
                },
                State.Trit => Trit,
                State.False => second switch
                {
                    true => True,
                    false => False,
                    null => Trit
                },
                _ => throw new ArgumentOutOfRangeException(nameof(first), first.Value, null)
            };
        }

        public static Trilean operator ^(Boolean first, Trilean second)
        {
            return second ^ first;
        }

        public static Trilean operator ^(Boolean? first, Trilean second)
        {
            return second ^ first;
        }

        private State Value { get; }

        public Boolean IsTrue
        {
            get
            {
                return Value == State.True;
            }
        }

        public Boolean IsTrit
        {
            get
            {
                return Value == State.Trit;
            }
        }

        public Boolean IsFalse
        {
            get
            {
                return Value == State.False;
            }
        }

        private Trilean(State value)
        {
            Value = value;
        }

        public TypeCode GetTypeCode()
        {
            return TypeCode.Boolean;
        }

        public Boolean ToBoolean(IFormatProvider? provider)
        {
            return this;
        }

        public SByte ToSByte(IFormatProvider? provider)
        {
            return (SByte) Value;
        }

        public Byte ToByte(IFormatProvider? provider)
        {
            return (Byte) Value;
        }

        public Int16 ToInt16(IFormatProvider? provider)
        {
            return (Int16) Value;
        }

        public UInt16 ToUInt16(IFormatProvider? provider)
        {
            return (UInt16) Value;
        }

        public Int32 ToInt32(IFormatProvider? provider)
        {
            return (Int32) Value;
        }

        public UInt32 ToUInt32(IFormatProvider? provider)
        {
            return (UInt32) Value;
        }

        public Int64 ToInt64(IFormatProvider? provider)
        {
            return (Int64) Value;
        }

        public UInt64 ToUInt64(IFormatProvider? provider)
        {
            return (UInt64) Value;
        }

        public Single ToSingle(IFormatProvider? provider)
        {
            return (Single) Value;
        }

        public Double ToDouble(IFormatProvider? provider)
        {
            return (Double) Value;
        }

        public Decimal ToDecimal(IFormatProvider? provider)
        {
            return (Decimal) Value;
        }

        public DateTime ToDateTime(IFormatProvider? provider)
        {
            throw new InvalidCastException();
        }

        public Char ToChar(IFormatProvider? provider)
        {
            return Value switch
            {
                State.Trit => 'U',
                State.True => 'T',
                _ => 'F'
            };
        }

        public String ToString(IFormatProvider? provider)
        {
            return ToString();
        }

        public Object ToType(Type conversionType, IFormatProvider? provider)
        {
            return ((IConvertible) Value).ToType(conversionType, provider);
        }

        public Int32 CompareTo(Trilean other)
        {
            return Value.CompareTo(other.Value);
        }

        public override Int32 GetHashCode()
        {
            return (Int32) Value;
        }

        public override Boolean Equals(Object? obj)
        {
            return obj switch
            {
                State value => Equals(value),
                Trilean value => Equals(value),
                Boolean value => Equals(value),
                null => Equals((Boolean?) null),
                _ => false
            };
        }

        private Boolean Equals(State other)
        {
            return Value == other;
        }

        public Boolean Equals(Trilean other)
        {
            return Value == other.Value;
        }

        public Boolean Equals(Boolean other)
        {
            return this == other;
        }

        public Boolean Equals(Boolean? other)
        {
            return this == other;
        }

        public override String ToString()
        {
            return Value switch
            {
                State.False => nameof(False),
                State.True => nameof(True),
                State.Trit => nameof(Trit),
                _ => throw new ArgumentOutOfRangeException(nameof(Value), Value, null)
            };
        }
    }
}