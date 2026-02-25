using System;
using System.Diagnostics.CodeAnalysis;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Strings.Interfaces
{
    public interface IStringProvider<T>
    {
        public static IStringProvider<T> Default { get; internal set; } = new Provider();

        public String? ToString(T value);
        public String? ToString(in T value);

        [return: NotNullIfNotNull("value")]
        public String? ToString(T value, String? format);

        [return: NotNullIfNotNull("value")]
        public String? ToString(in T value, String? format);

        [return: NotNullIfNotNull("value")]
        public String? ToString(T value, IFormatProvider? provider);

        [return: NotNullIfNotNull("value")]
        public String? ToString(in T value, IFormatProvider? provider);

        [return: NotNullIfNotNull("value")]
        public String? ToString(T value, String? format, IFormatProvider? provider);

        [return: NotNullIfNotNull("value")]
        public String? ToString(in T value, String? format, IFormatProvider? provider);

        public String? GetString(T value);
        public String? GetString(in T value);
        public String? GetString(T value, EscapeType escape);
        public String? GetString(in T value, EscapeType escape);
        public String? GetString(T value, String? format);
        public String? GetString(in T value, String? format);
        public String? GetString(T value, EscapeType escape, String? format);
        public String? GetString(in T value, EscapeType escape, String? format);
        public String? GetString(T value, IFormatProvider? provider);
        public String? GetString(in T value, IFormatProvider? provider);
        public String? GetString(T value, EscapeType escape, IFormatProvider? provider);
        public String? GetString(in T value, EscapeType escape, IFormatProvider? provider);
        public String? GetString(T value, String? format, IFormatProvider? provider);
        public String? GetString(in T value, String? format, IFormatProvider? provider);
        public String? GetString(T value, EscapeType escape, String? format, IFormatProvider? provider);
        public String? GetString(in T value, EscapeType escape, String? format, IFormatProvider? provider);

        internal sealed class Provider : IStringProvider<T>
        {
            public String? ToString(T value)
            {
                return IStringProvider.Default.ToString(value);
            }

            public String? ToString(in T value)
            {
                return IStringProvider.Default.ToString(in value);
            }

            [return: NotNullIfNotNull("value")]
            public String? ToString(T value, String? format)
            {
                return IStringProvider.Default.ToString(value, format);
            }

            [return: NotNullIfNotNull("value")]
            public String? ToString(in T value, String? format)
            {
                return IStringProvider.Default.ToString(in value, format);
            }

            [return: NotNullIfNotNull("value")]
            public String? ToString(T value, IFormatProvider? provider)
            {
                return IStringProvider.Default.ToString(value, provider);
            }

            [return: NotNullIfNotNull("value")]
            public String? ToString(in T value, IFormatProvider? provider)
            {
                return IStringProvider.Default.ToString(in value, provider);
            }

            [return: NotNullIfNotNull("value")]
            public String ?ToString(T value, String? format, IFormatProvider? provider)
            {
                return IStringProvider.Default.ToString(value, format, provider);
            }

            [return: NotNullIfNotNull("value")]
            public String? ToString(in T value, String? format, IFormatProvider? provider)
            {
                return IStringProvider.Default.ToString(in value, format, provider);
            }

            public String? GetString(T value)
            {
                return ToString(value);
            }

            public String? GetString(in T value)
            {
                return ToString(in value);
            }

            public String? GetString(T value, EscapeType escape)
            {
                return ToString(value);
            }

            public String? GetString(in T value, EscapeType escape)
            {
                return ToString(value);
            }

            public String? GetString(T value, String? format)
            {
                return ToString(value, format);
            }

            public String? GetString(in T value, String? format)
            {
                return ToString(in value, format);
            }

            public String? GetString(T value, EscapeType escape, String? format)
            {
                return ToString(value, format);
            }

            public String? GetString(in T value, EscapeType escape, String? format)
            {
                return ToString(in value, format);
            }

            public String? GetString(T value, IFormatProvider? provider)
            {
                return ToString(value, provider);
            }

            public String? GetString(in T value, IFormatProvider? provider)
            {
                return ToString(in value, provider);
            }

            public String? GetString(T value, EscapeType escape, IFormatProvider? provider)
            {
                return ToString(value, provider);
            }

            public String? GetString(in T value, EscapeType escape, IFormatProvider? provider)
            {
                return ToString(in value, provider);
            }

            public String? GetString(T value, String? format, IFormatProvider? provider)
            {
                return ToString(value, format, provider);
            }

            public String? GetString(in T value, String? format, IFormatProvider? provider)
            {
                return ToString(in value, format, provider);
            }

            public String? GetString(T value, EscapeType escape, String? format, IFormatProvider? provider)
            {
                return ToString(value, format, provider);
            }

            public String? GetString(in T value, EscapeType escape, String? format, IFormatProvider? provider)
            {
                return ToString(in value, format, provider);
            }
        }
    }

    public interface IStringProvider
    {
        public static IStringProvider Default { get; internal set; } = new Provider();

        public String? ToString<T>(T value);
        public String? ToString<T>(in T value);

        [return: NotNullIfNotNull("value")]
        public String? ToString<T>(T value, String? format);

        [return: NotNullIfNotNull("value")]
        public String? ToString<T>(in T value, String? format);

        [return: NotNullIfNotNull("value")]
        public String? ToString<T>(T value, IFormatProvider? provider);

        [return: NotNullIfNotNull("value")]
        public String? ToString<T>(in T value, IFormatProvider? provider);

        [return: NotNullIfNotNull("value")]
        public String? ToString<T>(T value, String? format, IFormatProvider? provider);

        [return: NotNullIfNotNull("value")]
        public String? ToString<T>(in T value, String? format, IFormatProvider? provider);

        public String? GetString<T>(T value);
        public String? GetString<T>(in T value);
        public String? GetString<T>(T value, EscapeType escape);
        public String? GetString<T>(in T value, EscapeType escape);
        public String? GetString<T>(T value, String? format);
        public String? GetString<T>(in T value, String? format);
        public String? GetString<T>(T value, EscapeType escape, String? format);
        public String? GetString<T>(in T value, EscapeType escape, String? format);
        public String? GetString<T>(T value, IFormatProvider? provider);
        public String? GetString<T>(in T value, IFormatProvider? provider);
        public String? GetString<T>(T value, EscapeType escape, IFormatProvider? provider);
        public String? GetString<T>(in T value, EscapeType escape, IFormatProvider? provider);
        public String? GetString<T>(T value, String? format, IFormatProvider? provider);
        public String? GetString<T>(in T value, String? format, IFormatProvider? provider);
        public String? GetString<T>(T value, EscapeType escape, String? format, IFormatProvider? provider);
        public String? GetString<T>(in T value, EscapeType escape, String? format, IFormatProvider? provider);

        internal sealed class Provider : IStringProvider
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
            public String ?ToString<T>(T value, String? format, IFormatProvider? provider)
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
                return ToString(value);
            }

            public String? GetString<T>(in T value)
            {
                return ToString(in value);
            }

            public String? GetString<T>(T value, EscapeType escape)
            {
                return ToString(value);
            }

            public String? GetString<T>(in T value, EscapeType escape)
            {
                return ToString(value);
            }

            public String? GetString<T>(T value, String? format)
            {
                return ToString(value, format);
            }

            public String? GetString<T>(in T value, String? format)
            {
                return ToString(in value, format);
            }

            public String? GetString<T>(T value, EscapeType escape, String? format)
            {
                return ToString(value, format);
            }

            public String? GetString<T>(in T value, EscapeType escape, String? format)
            {
                return ToString(in value, format);
            }

            public String? GetString<T>(T value, IFormatProvider? provider)
            {
                return ToString(value, provider);
            }

            public String? GetString<T>(in T value, IFormatProvider? provider)
            {
                return ToString(in value, provider);
            }

            public String? GetString<T>(T value, EscapeType escape, IFormatProvider? provider)
            {
                return ToString(value, provider);
            }

            public String? GetString<T>(in T value, EscapeType escape, IFormatProvider? provider)
            {
                return ToString(in value, provider);
            }

            public String? GetString<T>(T value, String? format, IFormatProvider? provider)
            {
                return ToString(value, format, provider);
            }

            public String? GetString<T>(in T value, String? format, IFormatProvider? provider)
            {
                return ToString(in value, format, provider);
            }

            public String? GetString<T>(T value, EscapeType escape, String? format, IFormatProvider? provider)
            {
                return ToString(value, format, provider);
            }

            public String? GetString<T>(in T value, EscapeType escape, String? format, IFormatProvider? provider)
            {
                return ToString(in value, format, provider);
            }
        }
    }
}