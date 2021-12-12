// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Text;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Strings.Interfaces
{
    public interface IString : IComparable, IConvertible, IFormattable, IEnumerable<Char>, IComparable<String?>, IEquatable<String?>, IComparable<IString?>, IEquatable<IString?>, ICloneable
    {
        /// <summary>
        /// Is immutable (ex. String) or mutable (ex. StringBuilder)
        /// </summary>
        public Boolean Immutable { get; }
        
        /// <summary>
        /// Is constant (single inner instance)
        /// </summary>
        public Boolean Constant { get; }
        public Int32 Length { get; }
        public String Text { get; }

        Int32 IComparable<String?>.CompareTo(String? other)
        {
            return CompareTo(other);
        }

        [SuppressMessage("ReSharper", "StringCompareToIsCultureSpecific")]
        public new Int32 CompareTo(String? other)
        {
            return ToString().CompareTo(other);
        }

        [SuppressMessage("ReSharper", "StringCompareIsCultureSpecific.1")]
        public Int32 CompareTo(String? other, IFormatProvider? provider)
        {
            return String.Compare(ToString(provider), other?.ToString(provider));
        }

        public Int32 CompareTo(String? other, StringComparison comparison)
        {
            return String.Compare(ToString(), other, comparison);
        }

        public Int32 CompareTo(String? other, StringComparison comparison, IFormatProvider? provider)
        {
            return String.Compare(ToString(provider), other?.ToString(provider), comparison);
        }

        Int32 IComparable<IString?>.CompareTo(IString? other)
        {
            return CompareTo(other);
        }

        [SuppressMessage("ReSharper", "StringCompareToIsCultureSpecific")]
        public new Int32 CompareTo(IString? other)
        {
            return CompareTo(other?.ToString());
        }

        [SuppressMessage("ReSharper", "StringCompareToIsCultureSpecific")]
        public Int32 CompareTo(IString? other, IFormatProvider? provider)
        {
            return CompareTo(other?.ToString(provider), provider);
        }

        public Int32 CompareTo(IString? other, StringComparison comparison)
        {
            return CompareTo(other?.ToString(), comparison);
        }

        public Int32 CompareTo(IString? other, StringComparison comparison, IFormatProvider? provider)
        {
            return CompareTo(other?.ToString(provider), comparison, provider);
        }

        Boolean IEquatable<String?>.Equals(String? other)
        {
            return Equals(other);
        }

        public new Boolean Equals(String? other)
        {
            return ToString().Equals(other);
        }

        public Boolean Equals(String? other, IFormatProvider? provider)
        {
            return String.Equals(ToString(provider), other?.ToString(provider));
        }

        public Boolean Equals(String? other, StringComparison comparison)
        {
            return String.Equals(ToString(), other, comparison);
        }

        public Boolean Equals(String? other, StringComparison comparison, IFormatProvider? provider)
        {
            return String.Equals(ToString(provider), other?.ToString(provider), comparison);
        }

        Boolean IEquatable<IString?>.Equals(IString? other)
        {
            return Equals(other);
        }

        public new Boolean Equals(IString? other)
        {
            return Equals(other?.ToString());
        }

        public Boolean Equals(IString? other, IFormatProvider? provider)
        {
            return Equals(other?.ToString(provider), provider);
        }

        public Boolean Equals(IString? other, StringComparison comparison)
        {
            return Equals(other?.ToString(), comparison);
        }

        public Boolean Equals(IString? other, StringComparison comparison, IFormatProvider? provider)
        {
            return Equals(other?.ToString(provider), comparison, provider);
        }
        
        public IString ToUpper()
        {
            return ToString().ToUpper().ToIString();
        }

        public IString ToUpper(CultureInfo info)
        {
            return ToString().ToUpper(info).ToIString();
        }

        public IString ToUpperInvariant()
        {
            return ToString().ToUpperInvariant().ToIString();
        }

        public IString ToLower()
        {
            return ToString().ToLower().ToIString();
        }

        public IString ToLower(CultureInfo info)
        {
            return ToString().ToLower(info).ToIString();
        }

        public IString ToLowerInvariant()
        {
            return ToString().ToLowerInvariant().ToIString();
        }

        public IString Normalize()
        {
            return ToString().Normalize().ToIString();
        }

        public IString Normalize(NormalizationForm normalization)
        {
            return ToString().Normalize(normalization).ToIString();
        }
        
        public Boolean IsNormalized()
        {
            return ToString().IsNormalized();
        }

        public Boolean IsNormalized(NormalizationForm normalization)
        {
            return ToString().IsNormalized(normalization);
        }

        public Boolean Contains(String value)
        {
            return ToString().Contains(value);
        }

        public Boolean Contains(String value, StringComparison comparison)
        {
            return ToString().Contains(value, comparison);
        }

        public Boolean Contains(Char value)
        {
            return ToString().Contains(value);
        }

        public Boolean Contains(Char value, StringComparison comparison)
        {
            return ToString().Contains(value, comparison);
        }

        public Int32 IndexOf(Char value)
        {
            return ToString().IndexOf(value);
        }

        public Int32 IndexOf(Char value, Int32 start)
        {
            return ToString().IndexOf(value, start);
        }

        public Int32 IndexOf(Char value, StringComparison comparison)
        {
            return ToString().IndexOf(value, comparison);
        }

        public Int32 IndexOf(Char value, Int32 start, Int32 count)
        {
            return ToString().IndexOf(value, start, count);
        }

        [SuppressMessage("ReSharper", "StringIndexOfIsCultureSpecific.1")]
        public Int32 IndexOf(String value)
        {
            return ToString().IndexOf(value);
        }

        [SuppressMessage("ReSharper", "StringIndexOfIsCultureSpecific.2")]
        public Int32 IndexOf(String value, Int32 start)
        {
            return ToString().IndexOf(value, start);
        }

        [SuppressMessage("ReSharper", "StringIndexOfIsCultureSpecific.3")]
        public Int32 IndexOf(String value, Int32 start, Int32 count)
        {
            return ToString().IndexOf(value, start, count);
        }

        public Int32 IndexOf(String value, StringComparison comparison)
        {
            return ToString().IndexOf(value, comparison);
        }

        public Int32 IndexOf(String value, Int32 start, StringComparison comparison)
        {
            return ToString().IndexOf(value, start, comparison);
        }

        public Int32 IndexOf(String value, Int32 start, Int32 count, StringComparison comparison)
        {
            return ToString().IndexOf(value, start, count, comparison);
        }

        public Int32 LastIndexOf(Char value)
        {
            return ToString().LastIndexOf(value);
        }

        public Int32 LastIndexOf(Char value, Int32 start)
        {
            return ToString().LastIndexOf(value, start);
        }

        public Int32 LastIndexOf(Char value, Int32 start, Int32 count)
        {
            return ToString().LastIndexOf(value, start, count);
        }
        
        [SuppressMessage("ReSharper", "StringLastIndexOfIsCultureSpecific.1")]
        public Int32 LastIndexOf(String value)
        {
            return ToString().LastIndexOf(value);
        }

        [SuppressMessage("ReSharper", "StringLastIndexOfIsCultureSpecific.2")]
        public Int32 LastIndexOf(String value, Int32 start)
        {
            return ToString().LastIndexOf(value, start);
        }

        [SuppressMessage("ReSharper", "StringLastIndexOfIsCultureSpecific.3")]
        public Int32 LastIndexOf(String value, Int32 start, Int32 count)
        {
            return ToString().LastIndexOf(value, start, count);
        }

        public Int32 LastIndexOf(String value, StringComparison comparison)
        {
            return ToString().LastIndexOf(value, comparison);
        }

        public Int32 LastIndexOf(String value, Int32 start, StringComparison comparison)
        {
            return ToString().LastIndexOf(value, start, comparison);
        }

        public Int32 LastIndexOf(String value, Int32 start, Int32 count, StringComparison comparison)
        {
            return ToString().LastIndexOf(value, start, count, comparison);
        }

        public Int32 IndexOfAny(Char[] values)
        {
            return ToString().IndexOfAny(values);
        }

        public Int32 IndexOfAny(Char[] values, Int32 start)
        {
            return ToString().IndexOfAny(values, start);
        }

        public Int32 IndexOfAny(Char[] values, Int32 start, Int32 count)
        {
            return ToString().IndexOfAny(values, start, count);
        }
        
        public Int32 LastIndexOfAny(Char[] values)
        {
            return ToString().LastIndexOfAny(values);
        }

        public Int32 LastIndexOfAny(Char[] values, Int32 start)
        {
            return ToString().LastIndexOfAny(values, start);
        }

        public Int32 LastIndexOfAny(Char[] values, Int32 start, Int32 count)
        {
            return ToString().LastIndexOfAny(values, start, count);
        }

        public IString[] Split(Char separator, StringSplitOptions options = StringSplitOptions.None)
        {
            return ToString().Split(separator, options).Select(StringUtilities.ToIString).ToArray();
        }

        public IString[] Split(Char separator, Int32 count, StringSplitOptions options = StringSplitOptions.None)
        {
            return ToString().Split(separator, count, options).Select(StringUtilities.ToIString).ToArray();
        }

        public IString[] Split(params Char[]? separator)
        {
            return ToString().Split(separator).Select(StringUtilities.ToIString).ToArray();
        }

        public IString[] Split(Char[]? separator, Int32 count)
        {
            return ToString().Split(separator, count).Select(StringUtilities.ToIString).ToArray();
        }

        public IString[] Split(Char[]? separator, StringSplitOptions options)
        {
            return ToString().Split(separator, options).Select(StringUtilities.ToIString).ToArray();
        }

        public IString[] Split(Char[]? separator, Int32 count, StringSplitOptions options)
        {
            return ToString().Split(separator, count, options).Select(StringUtilities.ToIString).ToArray();
        }

        public IString[] Split(String? separator, StringSplitOptions options = StringSplitOptions.None)
        {
            return ToString().Split(separator, options).Select(StringUtilities.ToIString).ToArray();
        }

        public IString[] Split(String? separator, Int32 count, StringSplitOptions options = StringSplitOptions.None)
        {
            return ToString().Split(separator, count, options).Select(StringUtilities.ToIString).ToArray();
        }

        public IString[] Split(String[]? separator, StringSplitOptions options)
        {
            return ToString().Split(separator, options).Select(StringUtilities.ToIString).ToArray();
        }

        public IString[] Split(String[]? separator, Int32 count, StringSplitOptions options)
        {
            return ToString().Split(separator, count, options).Select(StringUtilities.ToIString).ToArray();
        }

        public IString Insert(Int32 start, String value)
        {
            return ToString().Insert(start, value).ToIString();
        }

        public IString Remove(Int32 start)
        {
            return ToString().Remove(start).ToIString();
        }

        public IString Remove(Int32 start, Int32 count)
        {
            return ToString().Remove(start, count).ToIString();
        }

        public IString Replace(Char before, Char after)
        {
            return ToString().Replace(before, after).ToIString();
        }

        public IString Replace(String before, String? after)
        {
            return ToString().Replace(before, after).ToIString();
        }

        public IString Replace(String before, String? after, Boolean ignoreCase, CultureInfo? culture)
        {
            return ToString().Replace(before, after, ignoreCase, culture).ToIString();
        }

        public IString Replace(String before, String? after, StringComparison comparison)
        {
            return ToString().Replace(before, after, comparison).ToIString();
        }

        public IString Substring(Int32 start)
        {
            return ToString().Substring(start).ToIString();
        }

        public IString Substring(Int32 start, Int32 length)
        {
            return ToString().Substring(start, length).ToIString();
        }

        public IString Trim()
        {
            return ToString().Trim().ToIString();
        }

        public IString Trim(Char trim)
        {
            return ToString().Trim(trim).ToIString();
        }

        public IString Trim(params Char[]? trim)
        {
            return ToString().Trim(trim).ToIString();
        }

        public IString TrimStart()
        {
            return ToString().Trim().ToIString();
        }

        public IString TrimStart(Char trim)
        {
            return ToString().Trim(trim).ToIString();
        }

        public IString TrimStart(params Char[]? trim)
        {
            return ToString().Trim(trim).ToIString();
        }

        public IString TrimEnd()
        {
            return ToString().Trim().ToIString();
        }

        public IString TrimEnd(Char trim)
        {
            return ToString().Trim(trim).ToIString();
        }

        public IString TrimEnd(params Char[]? trim)
        {
            return ToString().Trim(trim).ToIString();
        }
        
        public Boolean StartsWith(String value)
        {
            return ToString().StartsWith(value);
        }

        public Boolean StartsWith(String value, StringComparison comparison)
        {
            return ToString().StartsWith(value, comparison);
        }

        public Boolean StartsWith(String value, Boolean ignoreCase, CultureInfo? culture)
        {
            return ToString().StartsWith(value, ignoreCase, culture);
        }

        public Boolean StartsWith(Char value)
        {
            return ToString().StartsWith(value);
        }
        
        public Boolean EndsWith(String value)
        {
            return ToString().EndsWith(value);
        }

        public Boolean EndsWith(String value, StringComparison comparison)
        {
            return ToString().EndsWith(value, comparison);
        }

        public Boolean EndsWith(String value, Boolean ignoreCase, CultureInfo? culture)
        {
            return ToString().EndsWith(value, ignoreCase, culture);
        }

        public Boolean EndsWith(Char value)
        {
            return ToString().EndsWith(value);
        }

        public IString PadLeft(Int32 width)
        {
            return ToString().PadLeft(width).ToIString();
        }

        public IString PadLeft(Int32 width, Char padding)
        {
            return ToString().PadLeft(width, padding).ToIString();
        }

        public IString PadRight(Int32 width)
        {
            return ToString().PadRight(width).ToIString();
        }

        public IString PadRight(Int32 width, Char padding)
        {
            return ToString().PadRight(width, padding).ToIString();
        }
        
        public StringRuneEnumerator EnumerateRunes()
        {
            return ToString().EnumerateRunes();
        }

        public Char[] ToCharArray()
        {
            return ToString().ToCharArray();
        }

        public Char[] ToCharArray(Int32 start, Int32 length)
        {
            return ToString().ToCharArray(start, length);
        }

        public Char this[Int32 index]
        {
            get
            {
                return ToString()[index];
            }
        }

        public Char this[Index index]
        {
            get
            {
                return ToString()[index];
            }
        }

        Int32 IComparable.CompareTo(Object? obj)
        {
            return ToString().CompareTo(obj);
        }

        TypeCode IConvertible.GetTypeCode()
        {
            return ToString().GetTypeCode();
        }

        Boolean IConvertible.ToBoolean(IFormatProvider? provider)
        {
            return ((IConvertible) ToString()).ToBoolean(provider);
        }

        Byte IConvertible.ToByte(IFormatProvider? provider)
        {
            return ((IConvertible) ToString()).ToByte(provider);
        }

        Char IConvertible.ToChar(IFormatProvider? provider)
        {
            return ((IConvertible) ToString()).ToChar(provider);
        }

        DateTime IConvertible.ToDateTime(IFormatProvider? provider)
        {
            return ((IConvertible) ToString()).ToDateTime(provider);
        }

        Decimal IConvertible.ToDecimal(IFormatProvider? provider)
        {
            return ((IConvertible) ToString()).ToDecimal(provider);
        }

        Double IConvertible.ToDouble(IFormatProvider? provider)
        {
            return ((IConvertible) ToString()).ToDouble(provider);
        }

        Int16 IConvertible.ToInt16(IFormatProvider? provider)
        {
            return ((IConvertible) ToString()).ToInt16(provider);
        }

        Int32 IConvertible.ToInt32(IFormatProvider? provider)
        {
            return ((IConvertible) ToString()).ToInt32(provider);
        }

        Int64 IConvertible.ToInt64(IFormatProvider? provider)
        {
            return ((IConvertible) ToString()).ToInt64(provider);
        }

        SByte IConvertible.ToSByte(IFormatProvider? provider)
        {
            return ((IConvertible) ToString()).ToSByte(provider);
        }

        Single IConvertible.ToSingle(IFormatProvider? provider)
        {
            return ((IConvertible) ToString()).ToSingle(provider);
        }

        String IConvertible.ToString(IFormatProvider? provider)
        {
            return ToString(provider);
        }

        public new String ToString(IFormatProvider? provider)
        {
            return ToString().ToString(provider);
        }

        Object IConvertible.ToType(Type conversionType, IFormatProvider? provider)
        {
            return ((IConvertible) ToString()).ToType(conversionType, provider);
        }

        UInt16 IConvertible.ToUInt16(IFormatProvider? provider)
        {
            return ((IConvertible) ToString()).ToUInt16(provider);
        }

        UInt32 IConvertible.ToUInt32(IFormatProvider? provider)
        {
            return ((IConvertible) ToString()).ToUInt32(provider);
        }

        UInt64 IConvertible.ToUInt64(IFormatProvider? provider)
        {
            return ((IConvertible) ToString()).ToUInt64(provider);
        }

        IEnumerator<Char> IEnumerable<Char>.GetEnumerator()
        {
            return GetEnumerator();
        }

        public new IEnumerator<Char> GetEnumerator()
        {
            return ToString().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) ToString()).GetEnumerator();
        }

        Object ICloneable.Clone()
        {
            return Clone();
        }

        public new Object Clone()
        {
            return ToString();
        }

        public String ToString()
        {
            return Text;
        }
    }
}