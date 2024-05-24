// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using NetExtender.Types.Strings.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Strings
{
    public sealed class StringBuilderAdapter : StringBase, IString, IEquatable<StringBuilder>
    {
        public static explicit operator StringBuilder(StringBuilderAdapter adapter)
        {
            return adapter.Builder;
        }

        public static implicit operator StringBuilderAdapter(StringBuilder value)
        {
            return new StringBuilderAdapter(value);
        }

        private StringBuilder Builder { get; }

        public override Boolean Immutable
        {
            get
            {
                return false;
            }
        }

        public override Int32 Length
        {
            get
            {
                return Builder.Length;
            }
        }

        public override String Text
        {
            get
            {
                return ToString();
            }
            protected set
            {
                throw new NotSupportedException();
            }
        }

        public StringBuilderAdapter()
            : this(new StringBuilder())
        {
        }

        public StringBuilderAdapter(String value)
        {
            Builder = new StringBuilder(value);
        }

        public StringBuilderAdapter(StringBuilder builder)
        {
            Builder = builder ?? throw new ArgumentNullException(nameof(builder));
        }

        IString IString.ToUpper()
        {
            Builder.ToUpper();
            return this;
        }

        IString IString.ToUpper(CultureInfo info)
        {
            Builder.ToUpper(info);
            return this;
        }

        IString IString.ToUpperInvariant()
        {
            Builder.ToUpperInvariant();
            return this;
        }

        IString IString.ToLower()
        {
            Builder.ToLower();
            return this;
        }

        IString IString.ToLower(CultureInfo info)
        {
            Builder.ToLower(info);
            return this;
        }

        IString IString.ToLowerInvariant()
        {
            Builder.ToLowerInvariant();
            return this;
        }

        IString IString.Normalize()
        {
            Builder.Normalize();
            return this;
        }

        IString IString.Normalize(NormalizationForm normalization)
        {
            Builder.Normalize(normalization);
            return this;
        }

        Boolean IString.IsNormalized()
        {
            return Builder.IsNormalized();
        }

        Boolean IString.IsNormalized(NormalizationForm normalization)
        {
            return Builder.IsNormalized(normalization);
        }

        Boolean IString.Contains(String value)
        {
            return Builder.Contains(value);
        }

        Boolean IString.Contains(String value, StringComparison comparison)
        {
            return Builder.Contains(value, comparison);
        }

        Boolean IString.Contains(Char value)
        {
            return Builder.Contains(value);
        }

        Boolean IString.Contains(Char value, StringComparison comparison)
        {
            return Builder.Contains(value, comparison);
        }

        Int32 IString.IndexOf(Char value)
        {
            return Builder.IndexOf(value);
        }

        Int32 IString.IndexOf(Char value, Int32 start)
        {
            return Builder.IndexOf(value, start);
        }

        Int32 IString.IndexOf(Char value, StringComparison comparison)
        {
            return Builder.IndexOf(value, comparison);
        }

        Int32 IString.IndexOf(Char value, Int32 start, Int32 count)
        {
            return Builder.IndexOf(value, start, count);
        }

        Int32 IString.IndexOf(String value)
        {
            return Builder.IndexOf(value);
        }

        Int32 IString.IndexOf(String value, Int32 start)
        {
            return Builder.IndexOf(value, start);
        }

        Int32 IString.IndexOf(String value, Int32 start, Int32 count)
        {
            return Builder.IndexOf(value, start, count);
        }

        Int32 IString.IndexOf(String value, StringComparison comparison)
        {
            return Builder.IndexOf(value, comparison);
        }

        Int32 IString.IndexOf(String value, Int32 start, StringComparison comparison)
        {
            return Builder.IndexOf(value, start, comparison);
        }

        Int32 IString.IndexOf(String value, Int32 start, Int32 count, StringComparison comparison)
        {
            return Builder.IndexOf(value, start, count, comparison);
        }

        Int32 IString.LastIndexOf(Char value)
        {
            return Builder.LastIndexOf(value);
        }

        Int32 IString.LastIndexOf(Char value, Int32 start)
        {
            return Builder.LastIndexOf(value, start);
        }

        Int32 IString.LastIndexOf(Char value, Int32 start, Int32 count)
        {
            return Builder.LastIndexOf(value, start, count);
        }

        Int32 IString.LastIndexOf(String value)
        {
            return Builder.LastIndexOf(value);
        }

        Int32 IString.LastIndexOf(String value, Int32 start)
        {
            return Builder.LastIndexOf(value, start);
        }

        Int32 IString.LastIndexOf(String value, Int32 start, Int32 count)
        {
            return Builder.LastIndexOf(value, start, count);
        }

        Int32 IString.LastIndexOf(String value, StringComparison comparison)
        {
            return Builder.LastIndexOf(value, comparison);
        }

        Int32 IString.LastIndexOf(String value, Int32 start, StringComparison comparison)
        {
            return Builder.LastIndexOf(value, start, comparison);
        }

        Int32 IString.LastIndexOf(String value, Int32 start, Int32 count, StringComparison comparison)
        {
            return Builder.LastIndexOf(value, start, count, comparison);
        }

        Int32 IString.IndexOfAny(Char[] values)
        {
            return Builder.IndexOfAny(values);
        }

        Int32 IString.IndexOfAny(Char[] values, Int32 start)
        {
            return Builder.IndexOfAny(values, start);
        }

        Int32 IString.IndexOfAny(Char[] values, Int32 start, Int32 count)
        {
            return Builder.IndexOfAny(values, start, count);
        }

        Int32 IString.LastIndexOfAny(Char[] values)
        {
            return Builder.LastIndexOfAny(values);
        }

        Int32 IString.LastIndexOfAny(Char[] values, Int32 start)
        {
            return Builder.LastIndexOfAny(values, start);
        }

        Int32 IString.LastIndexOfAny(Char[] values, Int32 start, Int32 count)
        {
            return Builder.LastIndexOfAny(values, start, count);
        }

        private static IString[] SplitConversion(IEnumerable<StringBuilder> builders)
        {
            return builders.Select(builder => (IString) new StringBuilderAdapter(builder)).ToArray();
        }

        IString[] IString.Split(Char separator, StringSplitOptions options)
        {
            return SplitConversion(Builder.Split(separator, options));
        }

        IString[] IString.Split(Char separator, Int32 count, StringSplitOptions options)
        {
            return SplitConversion(Builder.Split(separator, count, options));
        }

        IString[] IString.Split(params Char[]? separator)
        {
            return SplitConversion(Builder.Split(separator));
        }

        IString[] IString.Split(Char[]? separator, Int32 count)
        {
            return SplitConversion(Builder.Split(separator, count));
        }

        IString[] IString.Split(Char[]? separator, StringSplitOptions options)
        {
            return SplitConversion(Builder.Split(separator, options));
        }

        IString[] IString.Split(Char[]? separator, Int32 count, StringSplitOptions options)
        {
            return SplitConversion(Builder.Split(separator, count, options));
        }

        IString[] IString.Split(String? separator, StringSplitOptions options)
        {
            return SplitConversion(Builder.Split(separator, options));
        }

        IString[] IString.Split(String? separator, Int32 count, StringSplitOptions options)
        {
            return SplitConversion(Builder.Split(separator, count, options));
        }

        IString[] IString.Split(String[]? separator, StringSplitOptions options)
        {
            return SplitConversion(Builder.Split(separator, options));
        }

        IString[] IString.Split(String[]? separator, Int32 count, StringSplitOptions options)
        {
            return SplitConversion(Builder.Split(separator, count, options));
        }

        IString IString.Insert(Int32 start, String value)
        {
            Builder.Insert(start, value);
            return this;
        }

        IString IString.Remove(Int32 start)
        {
            Builder.Remove(start);
            return this;
        }

        IString IString.Remove(Int32 start, Int32 count)
        {
            Builder.Remove(start, count);
            return this;
        }

        IString IString.Replace(Char before, Char after)
        {
            Builder.Replace(before, after);
            return this;
        }

        IString IString.Replace(String before, String? after)
        {
            Builder.Replace(before, after);
            return this;
        }

        IString IString.Replace(String before, String? after, Boolean ignoreCase, CultureInfo? culture)
        {
            Builder.Replace(before, after, ignoreCase, culture);
            return this;
        }

        IString IString.Replace(String before, String? after, StringComparison comparison)
        {
            Builder.Replace(before, after, comparison);
            return this;
        }

        IString IString.Substring(Int32 start)
        {
            Builder.Substring(start);
            return this;
        }

        IString IString.Substring(Int32 start, Int32 length)
        {
            Builder.Substring(start, length);
            return this;
        }

        IString IString.Trim()
        {
            Builder.Trim();
            return this;
        }

        IString IString.Trim(Char trim)
        {
            Builder.Trim(trim);
            return this;
        }

        IString IString.Trim(params Char[]? trim)
        {
            Builder.Trim(trim);
            return this;
        }

        IString IString.TrimStart()
        {
            Builder.TrimStart();
            return this;
        }

        IString IString.TrimStart(Char trim)
        {
            Builder.TrimStart(trim);
            return this;
        }

        IString IString.TrimStart(params Char[]? trim)
        {
            Builder.TrimStart(trim);
            return this;
        }

        IString IString.TrimEnd()
        {
            Builder.TrimEnd();
            return this;
        }

        IString IString.TrimEnd(Char trim)
        {
            Builder.TrimEnd(trim);
            return this;
        }

        IString IString.TrimEnd(params Char[]? trim)
        {
            Builder.TrimEnd(trim);
            return this;
        }

        Boolean IString.StartsWith(String value)
        {
            return Builder.StartsWith(value);
        }

        Boolean IString.StartsWith(String value, StringComparison comparison)
        {
            return Builder.StartsWith(value, comparison);
        }

        Boolean IString.StartsWith(String value, Boolean ignoreCase, CultureInfo? culture)
        {
            return Builder.StartsWith(value, ignoreCase, culture);
        }

        Boolean IString.StartsWith(Char value)
        {
            return Builder.StartsWith(value);
        }

        Boolean IString.EndsWith(String value)
        {
            return Builder.EndsWith(value);
        }

        Boolean IString.EndsWith(String value, StringComparison comparison)
        {
            return Builder.EndsWith(value, comparison);
        }

        Boolean IString.EndsWith(String value, Boolean ignoreCase, CultureInfo? culture)
        {
            return Builder.EndsWith(value, ignoreCase, culture);
        }

        Boolean IString.EndsWith(Char value)
        {
            return Builder.EndsWith(value);
        }

        IString IString.PadLeft(Int32 width)
        {
            Builder.PadLeft(width);
            return this;
        }

        IString IString.PadLeft(Int32 width, Char padding)
        {
            Builder.PadLeft(width, padding);
            return this;
        }

        IString IString.PadRight(Int32 width)
        {
            Builder.PadRight(width);
            return this;
        }

        IString IString.PadRight(Int32 width, Char padding)
        {
            Builder.PadRight(width, padding);
            return this;
        }

        StringRuneEnumerator IString.EnumerateRunes()
        {
            return Builder.EnumerateRunes();
        }

        Char[] IString.ToCharArray()
        {
            return Builder.ToCharArray();
        }

        Char[] IString.ToCharArray(Int32 start, Int32 length)
        {
            return Builder.ToCharArray(start, length);
        }

        Char IString.this[Int32 index]
        {
            get
            {
                return Builder[index];
            }
        }

        Char IString.this[Index index]
        {
            get
            {
                return Builder[index];
            }
        }

        IEnumerator<Char> IString.GetEnumerator()
        {
            return Builder.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IString) this).GetEnumerator();
        }

        public override Boolean Equals(Object? other)
        {
            return ReferenceEquals(this, other) || other is StringBuilderAdapter adapter && Equals(adapter);
        }

        public Boolean Equals(StringBuilder? other)
        {
            return Equals(Builder, other);
        }

        public override Int32 GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), Builder);
        }

        public override String ToString()
        {
            return Builder.ToString();
        }

        public override String ToString(IFormatProvider? provider)
        {
            return ToString();
        }
    }
}