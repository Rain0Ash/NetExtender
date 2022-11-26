// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Globalization;
using System.Numerics;
using NetExtender.Types.Strings.Interfaces;

namespace NetExtender.Serialization.Ini
{
    public readonly struct IniValue : IString
    {
        public static implicit operator IniValue(SByte value)
        {
            return new IniValue(value);
        }

        public static implicit operator IniValue(Byte value)
        {
            return new IniValue(value);
        }

        public static implicit operator IniValue(Int16 value)
        {
            return new IniValue(value);
        }

        public static implicit operator IniValue(UInt16 value)
        {
            return new IniValue(value);
        }

        public static implicit operator IniValue(Int32 value)
        {
            return new IniValue(value);
        }

        public static implicit operator IniValue(UInt32 value)
        {
            return new IniValue(value);
        }

        public static implicit operator IniValue(Int64 value)
        {
            return new IniValue(value);
        }

        public static implicit operator IniValue(UInt64 value)
        {
            return new IniValue(value);
        }

        public static implicit operator IniValue(Single value)
        {
            return new IniValue(value);
        }

        public static implicit operator IniValue(Double value)
        {
            return new IniValue(value);
        }

        public static implicit operator IniValue(Decimal value)
        {
            return new IniValue(value);
        }

        public static implicit operator IniValue(BigInteger value)
        {
            return new IniValue(value);
        }

        public static implicit operator IniValue(Boolean value)
        {
            return new IniValue(value.ToString());
        }

        public static implicit operator IniValue(String? value)
        {
            return new IniValue(value);
        }

        public static IniValue Default { get; } = new IniValue();

        public Boolean Immutable
        {
            get
            {
                return true;
            }
        }

        public Boolean Constant
        {
            get
            {
                return true;
            }
        }

        public Int32 Length
        {
            get
            {
                return Value?.Length ?? 0;
            }
        }

        public String Text
        {
            get
            {
                return Value ?? String.Empty;
            }
        }

        public String? Value { get; }

        public IniValue(Object? value)
            : this(value?.ToString())
        {
        }

        public IniValue(IFormattable? value)
        {
            Value = value?.ToString(null, CultureInfo.InvariantCulture);
        }

        public IniValue(String? value)
        {
            Value = value;
        }

        public override String ToString()
        {
            return ToString(true, false);
        }

        public String ToString(String? format, IFormatProvider? provider)
        {
            return ToString();
        }

        public String ToString(Boolean whitespace)
        {
            return ToString(true, whitespace);
        }

        public String ToString(Boolean quotes, Boolean whitespace)
        {
            if (Value is null)
            {
                return String.Empty;
            }

            String trimmed = Value.Trim();
            if (!quotes || trimmed.Length < 2 || trimmed[0] != '"' || trimmed[^1] != '"')
            {
                return whitespace ? Value : trimmed;
            }

            String inner = trimmed.Substring(1, trimmed.Length - 2);
            return whitespace ? inner : inner.Trim();
        }
    }
}