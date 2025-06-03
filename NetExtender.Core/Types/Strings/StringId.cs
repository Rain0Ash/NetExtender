using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using NetExtender.Types.Strings.Interfaces;

namespace NetExtender.Types.Strings
{
    public readonly struct StringId : IString, IEqualityStruct<StringId>
    {
        public static implicit operator String?(StringId value)
        {
            return value.Value;
        }

        public static implicit operator StringId(String value)
        {
            return new StringId(value);
        }

        public static Boolean operator ==(StringId first, StringId second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(StringId first, StringId second)
        {
            return !(first == second);
        }

        public static Boolean operator ==(StringId first, String second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(StringId first, String second)
        {
            return !(first == second);
        }

        public static Boolean operator ==(String first, StringId second)
        {
            return new StringId(first).Equals(second);
        }

        public static Boolean operator !=(String first, StringId second)
        {
            return !(first == second);
        }

        private String? Value { get; }

        Boolean IString.Immutable
        {
            get
            {
                return true;
            }
        }

        Boolean IString.Constant
        {
            get
            {
                return true;
            }
        }

        Int32 IString.Length
        {
            get
            {
                return Value?.Length ?? 0;
            }
        }

        String IString.Text
        {
            get
            {
                return Value ?? String.Empty;
            }
        }

        public Boolean IsEmpty
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Value is null;
            }
        }

        public StringId(String? value)
        {
            Value = value;
        }

        public static StringId From(String? value)
        {
            return new StringId(value);
        }

        public Int32 CompareTo(String? other)
        {
            return CompareTo(other, StringComparison.Ordinal);
        }

        public Int32 CompareTo(String? other, StringComparison comparison)
        {
            return String.Compare(Value, other, comparison);
        }

        public Int32 CompareTo(StringId other)
        {
            return CompareTo(other, StringComparison.Ordinal);
        }

        public Int32 CompareTo(StringId other, StringComparison comparison)
        {
            return CompareTo(other.Value, comparison);
        }

        public override Int32 GetHashCode()
        {
            return Value?.GetHashCode() ?? 0;
        }

        public override Boolean Equals(Object? other)
        {
            return other switch
            {
                null => Equals(default(String)),
                String value => Equals(value),
                StringId value => Equals(value),
                IString value => Equals(value.ToString()),
                _ => false
            };
        }

        public Boolean Equals(String? other)
        {
            return Value == other;
        }

        public Boolean Equals(StringId other)
        {
            return Equals(other.Value);
        }

        public override String ToString()
        {
            return Value ?? String.Empty;
        }

        String IFormattable.ToString(String? format, IFormatProvider? provider)
        {
            return ToString();
        }
    }
}