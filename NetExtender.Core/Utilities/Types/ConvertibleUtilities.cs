// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.Contracts;

namespace NetExtender.Utilities.Types
{
    public static class ConvertibleUtilities
    {
        [Pure]
        public static Boolean ToBoolean(this IConvertible value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return value.ToBoolean(null);
        }

        [Pure]
        public static Char ToChar(this IConvertible value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return value.ToChar(null);
        }

        [Pure]
        public static Char32 ToChar32(this IConvertible value)
        {
            return ToChar32(value, null);
        }

        [Pure]
        public static Char32 ToChar32(this IConvertible value, IFormatProvider? provider)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return value.ToUInt32(provider);
        }

        [Pure]
        public static SByte ToSByte(this IConvertible value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return value.ToSByte(null);
        }

        [Pure]
        public static Byte ToByte(this IConvertible value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return value.ToByte(null);
        }

        [Pure]
        public static Int16 ToInt16(this IConvertible value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return value.ToInt16(null);
        }

        [Pure]
        public static UInt16 ToUInt16(this IConvertible value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return value.ToUInt16(null);
        }

        [Pure]
        public static Int32 ToInt32(this IConvertible value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return value.ToInt32(null);
        }

        [Pure]
        public static UInt32 ToUInt32(this IConvertible value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return value.ToUInt32(null);
        }

        [Pure]
        public static Int64 ToInt64(this IConvertible value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return value.ToInt64(null);
        }

        [Pure]
        public static UInt64 ToUInt64(this IConvertible value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return value.ToUInt64(null);
        }

        [Pure]
        public static Single ToSingle(this IConvertible value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return value.ToSingle(null);
        }

        [Pure]
        public static Double ToDouble(this IConvertible value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return value.ToDouble(null);
        }

        [Pure]
        public static Decimal ToDecimal(this IConvertible value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return value.ToDecimal(null);
        }

        [Pure]
        public static DateTime ToDateTime(this IConvertible value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return value.ToDateTime(null);
        }

        [Pure]
        public static String ToString(this IConvertible value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return value.ToString(null);
        }

        [Pure]
        public static Object ToType(this IConvertible value, Type type)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return value.ToType(type, null);
        }
    }
}