using System;
using System.Diagnostics.CodeAnalysis;
using NetExtender.Types.Strings.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Strings
{
    public sealed class StringProvider : IStringProvider
    {
        public String? ToString<T>(T value)
        {
            return StringUtilities.ToString(value);
        }

        public String? ToString<T>(in T value)
        {
            return StringUtilities.ToString(in value);
        }

        [return: NotNullIfNotNull("value")]
        public String? ToString<T>(T value, String? format)
        {
            return StringUtilities.ToString(value, format);
        }

        [return: NotNullIfNotNull("value")]
        public String? ToString<T>(in T value, String? format)
        {
            return StringUtilities.ToString(in value, format);
        }

        [return: NotNullIfNotNull("value")]
        public String? ToString<T>(T value, IFormatProvider? provider)
        {
            return StringUtilities.ToString(value, provider);
        }

        [return: NotNullIfNotNull("value")]
        public String? ToString<T>(in T value, IFormatProvider? provider)
        {
            return StringUtilities.ToString(in value, provider);
        }

        [return: NotNullIfNotNull("value")]
        public String? ToString<T>(T value, String? format, IFormatProvider? provider)
        {
            return StringUtilities.ToString(value, format, provider);
        }

        [return: NotNullIfNotNull("value")]
        public String? ToString<T>(in T value, String? format, IFormatProvider? provider)
        {
            return StringUtilities.ToString(in value, format, provider);
        }

        public String? GetString<T>(T value)
        {
            return value.GetString();
        }

        public String? GetString<T>(in T value)
        {
            return value.GetString();
        }

        public String? GetString<T>(T value, EscapeType escape)
        {
            return value.GetString(escape);
        }

        public String? GetString<T>(in T value, EscapeType escape)
        {
            return value.GetString(escape);
        }

        public String? GetString<T>(T value, String? format)
        {
            return value.GetString(format);
        }

        public String? GetString<T>(in T value, String? format)
        {
            return value.GetString(format);
        }

        public String? GetString<T>(T value, EscapeType escape, String? format)
        {
            return value.GetString(escape, format);
        }

        public String? GetString<T>(in T value, EscapeType escape, String? format)
        {
            return value.GetString(escape, format);
        }

        public String? GetString<T>(T value, IFormatProvider? provider)
        {
            return value.GetString(provider);
        }

        public String? GetString<T>(in T value, IFormatProvider? provider)
        {
            return value.GetString(provider);
        }

        public String? GetString<T>(T value, EscapeType escape, IFormatProvider? provider)
        {
            return value.GetString(escape, provider);
        }

        public String? GetString<T>(in T value, EscapeType escape, IFormatProvider? provider)
        {
            return value.GetString(escape, provider);
        }

        public String? GetString<T>(T value, String? format, IFormatProvider? provider)
        {
            return value.GetString(format, provider);
        }

        public String? GetString<T>(in T value, String? format, IFormatProvider? provider)
        {
            return value.GetString(format, provider);
        }

        public String? GetString<T>(T value, EscapeType escape, String? format, IFormatProvider? provider)
        {
            return value.GetString(escape, format, provider);
        }

        public String? GetString<T>(in T value, EscapeType escape, String? format, IFormatProvider? provider)
        {
            return value.GetString(escape, format, provider);
        }
    }
}