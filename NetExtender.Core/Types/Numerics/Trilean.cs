// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Types.Numerics
{
    public readonly struct Trilean : IConvertible, IEquatable<Trilean>, IEquatable<Boolean>, IEquatable<Boolean?>, IComparable<Trilean>
    {
        private const Byte VTrue = 2;
        private const Byte VTrit = 1;
        private const Byte VFalse = 0;

        public static Trilean True { get; } = new Trilean(VTrue);
        public static Trilean Trit { get; } = new Trilean(VTrit);
        public static Trilean False { get; } = new Trilean(VFalse);

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
                VTrue => true,
                VFalse => false,
                VTrit => null,
                _ => throw new ArgumentOutOfRangeException(nameof(value))
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
            return second switch
            {
                true => first.IsTrue,
                false => first.IsFalse,
                null => first.IsTrit
            };
        }

        public static Boolean operator !=(Trilean first, Boolean? second)
        {
            return !(first == second);
        }

        public static Boolean operator ==(Boolean first, Trilean second)
        {
            return first == (Boolean) second;
        }

        public static Boolean operator !=(Boolean first, Trilean second)
        {
            return first != (Boolean) second;
        }

        public static Boolean operator ==(Boolean? first, Trilean second)
        {
            return first switch
            {
                true => second.IsTrue,
                false => second.IsFalse,
                null => second.IsTrit
            };
        }

        public static Boolean operator !=(Boolean? first, Trilean second)
        {
            return !(first == second);
        }

        public static Trilean operator !(Trilean value)
        {
            return value.Value switch
            {
                VTrue => False,
                VTrit => Trit,
                VFalse => True,
                _ => throw new ArgumentOutOfRangeException(nameof(value))
            };
        }

        public static Trilean operator &(Trilean first, Trilean second)
        {
            return first.Value switch
            {
                VTrue => second.Value switch
                {
                    VTrue => True,
                    VTrit => Trit,
                    VFalse => False,
                    _ => throw new ArgumentOutOfRangeException(nameof(second))
                },
                VTrit => second.Value switch
                {
                    VTrue => Trit,
                    VTrit => Trit,
                    VFalse => False,
                    _ => throw new ArgumentOutOfRangeException(nameof(second))
                },
                VFalse => False,
                _ => throw new ArgumentOutOfRangeException(nameof(first))
            };
        }

        public static Trilean operator &(Trilean first, Boolean second)
        {
            return first.Value switch
            {
                VTrue => second ? True : False,
                VTrit => second ? Trit : False,
                VFalse => False,
                _ => throw new ArgumentOutOfRangeException(nameof(first))
            };
        }

        public static Trilean operator &(Trilean first, Boolean? second)
        {
            return first.Value switch
            {
                VTrue => second switch
                {
                    true => True,
                    false => False,
                    null => Trit
                },
                VTrit => second switch
                {
                    true => Trit,
                    false => False,
                    null => Trit
                },
                VFalse => False,
                _ => throw new ArgumentOutOfRangeException(nameof(first))
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
                VTrue => True,
                VTrit => second.Value switch
                {
                    VTrue => True,
                    VTrit => Trit,
                    VFalse => Trit,
                    _ => throw new ArgumentOutOfRangeException(nameof(second))
                },
                VFalse => second.Value switch
                {
                    VTrue => True,
                    VTrit => Trit,
                    VFalse => False,
                    _ => throw new ArgumentOutOfRangeException(nameof(second))
                },
                _ => throw new ArgumentOutOfRangeException(nameof(first))
            };
        }

        public static Trilean operator |(Trilean first, Boolean second)
        {
            return first.Value switch
            {
                VTrue => True,
                VTrit => second ? True : Trit,
                VFalse => second ? True : False,
                _ => throw new ArgumentOutOfRangeException(nameof(first))
            };
        }

        public static Trilean operator |(Trilean first, Boolean? second)
        {
            return first.Value switch
            {
                VTrue => True,
                VTrit => second switch
                {
                    true => True,
                    false => Trit,
                    null => Trit
                },
                VFalse => second switch
                {
                    true => True,
                    false => False,
                    null => Trit
                },
                _ => throw new ArgumentOutOfRangeException(nameof(first))
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
                VTrue => second.Value switch
                {
                    VTrue => False,
                    VTrit => Trit,
                    VFalse => True,
                    _ => throw new ArgumentOutOfRangeException(nameof(second))
                },
                VTrit => Trit,
                VFalse => second.Value switch
                {
                    VTrue => True,
                    VTrit => Trit,
                    VFalse => False,
                    _ => throw new ArgumentOutOfRangeException(nameof(second))
                },
                _ => throw new ArgumentOutOfRangeException(nameof(first))
            };
        }

        public static Trilean operator ^(Trilean first, Boolean second)
        {
            return first.Value switch
            {
                VTrue => second ? False : True,
                VTrit => Trit,
                VFalse => second ? True : False,
                _ => throw new ArgumentOutOfRangeException(nameof(first))
            };
        }

        public static Trilean operator ^(Trilean first, Boolean? second)
        {
            return first.Value switch
            {
                VTrue => second switch
                {
                    true => False,
                    false => True,
                    null => Trit
                },
                VTrit => Trit,
                VFalse => second switch
                {
                    true => True,
                    false => False,
                    null => Trit
                },
                _ => throw new ArgumentOutOfRangeException(nameof(first))
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

        private Byte Value { get; }

        public Boolean IsTrue
        {
            get
            {
                return Value == VTrue;
            }
        }

        public Boolean IsTrit
        {
            get
            {
                return Value == VTrit;
            }
        }

        public Boolean IsFalse
        {
            get
            {
                return Value == VFalse;
            }
        }

        private Trilean(Byte value)
        {
            if (value > 2)
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }

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

        public Byte ToByte(IFormatProvider? provider)
        {
            return Value;
        }

        public Char ToChar(IFormatProvider? provider)
        {
            return Value switch
            {
                VTrit => '2',
                VTrue => '1',
                _ => '0'
            };
        }

        public DateTime ToDateTime(IFormatProvider? provider)
        {
            throw new InvalidCastException();
        }

        public Decimal ToDecimal(IFormatProvider? provider)
        {
            return Value;
        }

        public Double ToDouble(IFormatProvider? provider)
        {
            return Value;
        }

        public Int16 ToInt16(IFormatProvider? provider)
        {
            return Value;
        }

        public Int32 ToInt32(IFormatProvider? provider)
        {
            return Value;
        }

        public Int64 ToInt64(IFormatProvider? provider)
        {
            return Value;
        }

        public SByte ToSByte(IFormatProvider? provider)
        {
            return (SByte) Value;
        }

        public Single ToSingle(IFormatProvider? provider)
        {
            return Value;
        }

        public String ToString(IFormatProvider? provider)
        {
            return ToString();
        }

        public Object ToType(Type conversionType, IFormatProvider? provider)
        {
            return ((IConvertible) Value).ToType(conversionType, provider);
        }

        public UInt16 ToUInt16(IFormatProvider? provider)
        {
            return Value;
        }

        public UInt32 ToUInt32(IFormatProvider? provider)
        {
            return Value;
        }

        public UInt64 ToUInt64(IFormatProvider? provider)
        {
            return Value;
        }

        public Int32 CompareTo(Trilean other)
        {
            return Value.CompareTo(other.Value);
        }

        public override Int32 GetHashCode()
        {
            return Value;
        }

        public override Boolean Equals(Object? obj)
        {
            return obj switch
            {
                Trilean trilean => this == trilean,
                Boolean boolean => this == boolean,
                _ => false
            };
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
                VTrit => nameof(Trit),
                VTrue => nameof(True),
                _ => nameof(False)
            };
        }
    }
}