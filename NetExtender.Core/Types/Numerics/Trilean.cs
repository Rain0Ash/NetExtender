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
            return value._trilean switch
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
            return first._trilean == second._trilean;
        }

        public static Boolean operator !=(Trilean first, Trilean second)
        {
            return first._trilean != second._trilean;
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
            return value._trilean switch
            {
                VTrue => False,
                VTrit => Trit,
                VFalse => True,
                _ => throw new ArgumentOutOfRangeException(nameof(value))
            };
        }

        public static Trilean operator &(Trilean first, Trilean second)
        {
            return first._trilean switch
            {
                VTrue => second._trilean switch
                {
                    VTrue => True,
                    VTrit => Trit,
                    VFalse => False,
                    _ => throw new ArgumentOutOfRangeException(nameof(second))
                },
                VTrit => second._trilean switch
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
            return first._trilean switch
            {
                VTrue => second ? True : False,
                VTrit => second ? Trit : False,
                VFalse => False,
                _ => throw new ArgumentOutOfRangeException(nameof(first))
            };
        }

        public static Trilean operator &(Trilean first, Boolean? second)
        {
            return first._trilean switch
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
            return first._trilean switch
            {
                VTrue => True,
                VTrit => second._trilean switch
                {
                    VTrue => True,
                    VTrit => Trit,
                    VFalse => Trit,
                    _ => throw new ArgumentOutOfRangeException(nameof(second))
                },
                VFalse => second._trilean switch
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
            return first._trilean switch
            {
                VTrue => True,
                VTrit => second ? True : Trit,
                VFalse => second ? True : False,
                _ => throw new ArgumentOutOfRangeException(nameof(first))
            };
        }

        public static Trilean operator |(Trilean first, Boolean? second)
        {
            return first._trilean switch
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
            return first._trilean switch
            {
                VTrue => second._trilean switch
                {
                    VTrue => False,
                    VTrit => Trit,
                    VFalse => True,
                    _ => throw new ArgumentOutOfRangeException(nameof(second))
                },
                VTrit => Trit,
                VFalse => second._trilean switch
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
            return first._trilean switch
            {
                VTrue => second ? False : True,
                VTrit => Trit,
                VFalse => second ? True : False,
                _ => throw new ArgumentOutOfRangeException(nameof(first))
            };
        }

        public static Trilean operator ^(Trilean first, Boolean? second)
        {
            return first._trilean switch
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

        private readonly Byte _trilean;

        public Boolean IsTrue
        {
            get
            {
                return _trilean == VTrue;
            }
        }

        public Boolean IsTrit
        {
            get
            {
                return _trilean == VTrit;
            }
        }

        public Boolean IsFalse
        {
            get
            {
                return _trilean == VFalse;
            }
        }

        private Trilean(Byte value)
        {
            if (value > 2)
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }

            _trilean = value;
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
            return _trilean;
        }

        public Char ToChar(IFormatProvider? provider)
        {
            return _trilean switch
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
            return _trilean;
        }

        public Double ToDouble(IFormatProvider? provider)
        {
            return _trilean;
        }

        public Int16 ToInt16(IFormatProvider? provider)
        {
            return _trilean;
        }

        public Int32 ToInt32(IFormatProvider? provider)
        {
            return _trilean;
        }

        public Int64 ToInt64(IFormatProvider? provider)
        {
            return _trilean;
        }

        public SByte ToSByte(IFormatProvider? provider)
        {
            return (SByte) _trilean;
        }

        public Single ToSingle(IFormatProvider? provider)
        {
            return _trilean;
        }

        public String ToString(IFormatProvider? provider)
        {
            return ToString();
        }

        public Object ToType(Type conversionType, IFormatProvider? provider)
        {
            return ((IConvertible) _trilean).ToType(conversionType, provider);
        }

        public UInt16 ToUInt16(IFormatProvider? provider)
        {
            return _trilean;
        }

        public UInt32 ToUInt32(IFormatProvider? provider)
        {
            return _trilean;
        }

        public UInt64 ToUInt64(IFormatProvider? provider)
        {
            return _trilean;
        }

        public Int32 CompareTo(Trilean other)
        {
            return _trilean.CompareTo(other._trilean);
        }

        public override Int32 GetHashCode()
        {
            return _trilean;
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
            return _trilean == other._trilean;
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
            return _trilean switch
            {
                VTrit => nameof(Trit),
                VTrue => nameof(True),
                _ => nameof(False)
            };
        }
    }
}