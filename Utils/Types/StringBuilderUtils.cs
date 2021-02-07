// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using JetBrains.Annotations;

namespace NetExtender.Utils.Types
{
    public static class StringBuilderUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static StringBuilder ToStringBuilder(this String value)
        {
            return new StringBuilder(value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static StringBuilder ToStringBuilder(this String value, Int32 capacity)
        {
            return new StringBuilder(value, capacity);
        }

        public static StringBuilder ToStringBuilder([NotNull] this IEnumerable<Char> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (source is String str)
            {
                return new StringBuilder(str);
            }

            StringBuilder builder = new StringBuilder();
            foreach (Char chr in source)
            {
                builder.Append(chr);
            }
            
            return builder;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNull(this StringBuilder builder)
        {
            return builder is null;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotNull(this StringBuilder builder)
        {
            return builder is not null;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsEmpty(this StringBuilder builder)
        {
            return builder is not null && builder.Length <= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotEmpty(this StringBuilder builder)
        {
            return !IsEmpty(builder);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNullOrEmpty(this StringBuilder builder)
        {
            return builder is null || builder.Length <= 0;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotNullOrEmpty(this StringBuilder builder)
        {
            return !IsNullOrEmpty(builder);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsWhiteSpace(this StringBuilder builder)
        {
            return builder is not null && builder.Length > 0 && builder.AsEnumerable().All(Char.IsWhiteSpace);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotWhiteSpace(this StringBuilder builder)
        {
            return !IsWhiteSpace(builder);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsEmptyOrWhiteSpace(this StringBuilder builder)
        {
            return builder is not null && (builder.Length <= 0 || builder.AsEnumerable().All(Char.IsWhiteSpace));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNullOrWhiteSpace(this StringBuilder builder)
        {
            return builder is null || IsEmptyOrWhiteSpace(builder);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotNullOrWhiteSpace(this StringBuilder builder)
        {
            return !IsNullOrWhiteSpace(builder);
        }

        public static StringBuilder Append([NotNull] this StringBuilder builder, [NotNull] IEnumerable<Char> source)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return builder.Append(source.ToStringBuilder());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static StringBuilder Prepend([NotNull] this StringBuilder builder, Char value)
        {
            return Prepend(builder, value.ToString());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static StringBuilder Prepend([NotNull] this StringBuilder builder, String value)
        {
            return AddPrefix(builder, value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static StringBuilder Prepend([NotNull] this StringBuilder builder, StringBuilder value)
        {
            return AddPrefix(builder, value);
        }
        
        public static StringBuilder Prepend([NotNull] this StringBuilder builder, [NotNull] IEnumerable<Char> source)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return builder.Prepend(source.ToStringBuilder());
        }
        
        public static StringBuilder AddSuffix([NotNull] this StringBuilder builder, String value)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.Append(value);
        }
        
        public static StringBuilder AddSuffix([NotNull] this StringBuilder builder, StringBuilder value)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.Append(value);
        }
        
        public static StringBuilder AddSuffix([NotNull] this StringBuilder builder, params String[] values)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (values is null)
            {
                return builder;
            }

            switch (values.Length)
            {
                case 0:
                    return builder;
                case 1:
                    return builder.Append(values[0]);
                default:
                    Int32 length = builder.Length + values.WhereNotNull().Sum(str => str.Length);

                    if (length > builder.MaxCapacity)
                    {
                        throw new ArgumentOutOfRangeException(nameof(values));
                    }

                    return values.Aggregate(builder, (sb, str) => sb.Append(str));
            }
        }
        
        public static StringBuilder AddPrefix([NotNull] this StringBuilder builder, String value)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.Insert(0, value);
        }
        
        public static StringBuilder AddPrefix([NotNull] this StringBuilder builder, StringBuilder value)
        {
            return AddPrefix(builder, value.ToString());
        }

        public static StringBuilder AddPrefix([NotNull] this StringBuilder builder, params String[] values)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (values is null)
            {
                return builder;
            }

            switch (values.Length)
            {
                case 0:
                    return builder;
                case 1:
                    return builder.AddPrefix(values[0]);
                default:
                    Int32 length = values.CharLength();

                    if (length + builder.Length > builder.MaxCapacity)
                    {
                        throw new ArgumentOutOfRangeException(nameof(values));
                    }

                    StringBuilder preconcat = new StringBuilder(length, length).AddSuffix(values);
                    return builder.AddPrefix(preconcat);
            }
        }

        public static StringBuilder AddPrefixAndSuffix([NotNull] this StringBuilder builder, String value)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (String.IsNullOrEmpty(value))
            {
                return builder;
            }

            if (builder.Length + 2 * value.Length > builder.MaxCapacity)
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }

            return builder.AddPrefix(value).AddSuffix(value);
        }
        
        public static StringBuilder AddPrefixAndSuffix([NotNull] this StringBuilder builder, StringBuilder value)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (value.IsNullOrEmpty())
            {
                return builder;
            }
            
            if (builder.Length + 2 * value.Length > builder.MaxCapacity)
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }

            return builder.AddPrefix(value).AddSuffix(value);
        }
        
        public static StringBuilder AddPrefixAndSuffix([NotNull] this StringBuilder builder, params String[] values)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (values is null)
            {
                return builder;
            }
            
            if (builder.Length + 2 * values.CharLength() > builder.MaxCapacity)
            {
                throw new ArgumentOutOfRangeException(nameof(values));
            }

            return values.Length switch
            {
                0 => builder,
                1 => AddPrefixAndSuffix(builder, values[0]),
                _ => AddPrefixAndSuffix(builder, values.Join())
            };
        }
        
        public static StringBuilder RemovePrefix(this StringBuilder builder, String prefix)
        {
            return RemovePrefix(builder, prefix, StringComparison.Ordinal);
        }

        public static StringBuilder RemovePrefix(this StringBuilder builder, String prefix, StringComparison comparison)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (prefix is null)
            {
                throw new ArgumentNullException(nameof(prefix));
            }

            if (prefix.Length > builder.Length || !builder.StartsWith(prefix, comparison))
            {
                return builder;
            }

            return builder.Remove(0, prefix.Length);
        }

        public static StringBuilder RemoveSuffix(this StringBuilder builder, String suffix)
        {
            return RemoveSuffix(builder, suffix, StringComparison.Ordinal);
        }

        public static StringBuilder RemoveSuffix(this StringBuilder builder, String suffix, StringComparison comparison)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (suffix is null)
            {
                throw new ArgumentNullException(nameof(suffix));
            }

            if (builder.Length < suffix.Length || !builder.EndsWith(suffix, comparison))
            {
                return builder;
            }

            return builder.Substring(0, builder.Length - suffix.Length);
        }

        public static StringBuilder RemovePrefixAndSuffix(this StringBuilder builder, String trim)
        {
            return RemovePrefixAndSuffix(builder, trim, StringComparison.Ordinal);
        }

        public static StringBuilder RemovePrefixAndSuffix(this StringBuilder builder, String trim, StringComparison comparison)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (builder.Length < trim.Length)
            {
                return builder;
            }

            Boolean starts = builder.StartsWith(trim, comparison);
            Boolean ends = builder.EndsWith(trim, comparison);

            if (starts && ends)
            {
                if (builder.Length <= 2 * trim.Length)
                {
                    return builder.Clear();
                }

                Int32 remove = builder.Length - trim.Length;
                return builder.Substring(trim.Length, remove - trim.Length);
            }

            if (starts)
            {
                return builder.Remove(0, trim.Length);
            }

            if (ends)
            {
                return builder.Substring(0, builder.Length - trim.Length);
            }

            return builder;
        }

        /// <inheritdoc cref="char.GetUnicodeCategory(String,Int32)"/>
        public static UnicodeCategory GetUnicodeCategory([NotNull] this StringBuilder builder, Int32 index)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (index < 0 || index >= builder.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }
            
            return Char.GetUnicodeCategory(builder[index]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String ToString([NotNull] this StringBuilder builder, Int32 start)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.ToString(start, builder.Length - start);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static StringBuilder Substring([NotNull] this StringBuilder builder, Int32 start)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return Substring(builder, start, builder.Length - start);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static StringBuilder Substring([NotNull] this StringBuilder builder, Int32 start, Int32 length)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (start < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(start));
            }

            if (length < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(length));
            }

            if (start > builder.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(start));
            }

            if (start + length > builder.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(length));
            }
            
            if (length == 0)
            {
                return builder.Clear();
            }

            if (start == 0 && length == builder.Length)
            {
                return builder;
            }

            return builder.Remove(0, start).Remove(length, builder.Length - length);
        }
        
        public static Boolean StartsWith(this StringBuilder builder, String value)
        {
            return EndsWith(builder, value, StringComparison.CurrentCulture);
        }

        public static Boolean StartsWith(this StringBuilder builder, String value, StringComparison comparison)
        {
            if (builder.Length < value.Length)
            {
                return false;
            }

            String start = builder.ToString(0, value.Length);
            return start.Equals(value, comparison);
        }
        
        public static Boolean EndsWith(this StringBuilder builder, String value)
        {
            return EndsWith(builder, value, StringComparison.CurrentCulture);
        }

        public static Boolean EndsWith(this StringBuilder builder, String value, StringComparison comparison)
        {
            if (builder.Length < value.Length)
            {
                return false;
            }

            String end = builder.ToString(builder.Length - value.Length, value.Length);
            return end.Equals(value, comparison);
        }
        
        public static StringBuilder Remove(this StringBuilder builder, Int32 start, Int32 length, out String result)
        {
            return Pop(builder, start, length, out result);
        }

        public static StringBuilder RemoveChar([NotNull] this StringBuilder builder, Int32 index)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.Remove(index, 1);
        }
        
        public static StringBuilder RemoveChar([NotNull] this StringBuilder builder, Int32 index, out Char result)
        {
            return PopChar(builder, index, out result);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Char PopChar([NotNull] this StringBuilder builder, Int32 index)
        {
            PopChar(builder, index, out Char result);
            return result;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static StringBuilder PopChar([NotNull] this StringBuilder builder, Int32 index, out Char result)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            result = builder[index];
            return builder.RemoveChar(index);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static StringBuilder Pop([NotNull] this StringBuilder builder, Int32 index, out Char result)
        {
            return PopChar(builder, index, out result);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String Pop([NotNull] this StringBuilder builder)
        {
            Pop(builder, out String result);
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static StringBuilder Pop([NotNull] this StringBuilder builder, out String result)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            result = builder.ToString();
            return builder.Clear();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String Pop(this StringBuilder builder, Int32 start)
        {
            Pop(builder, start, out String result);
            return result;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static StringBuilder Pop([NotNull] this StringBuilder builder, Int32 start, out String result)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return Pop(builder, start, builder.Length - start, out result);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String Pop([NotNull] this StringBuilder builder, Int32 start, Int32 length)
        {
            Pop(builder, start, length, out String result);
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static StringBuilder Pop([NotNull] this StringBuilder builder, Int32 start, Int32 length, out String result)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            result = builder.ToString(start, length);
            return builder.Remove(start, length);
        }
        
        public static StringBuilder ToUpper([NotNull] this StringBuilder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            String result = builder.ToString().ToUpper();
            return builder.Clear().Append(result);
        }

        public static StringBuilder ToUpper(this StringBuilder builder, CultureInfo info)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            String result = builder.ToString().ToUpper(info);
            return builder.Clear().Append(result);
        }

        public static StringBuilder ToUpperInvariant(this StringBuilder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            String result = builder.ToString().ToUpperInvariant();
            return builder.Clear().Append(result);
        }

        public static StringBuilder ToLower(this StringBuilder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            String result = builder.ToString().ToLower();
            return builder.Clear().Append(result);
        }

        public static StringBuilder ToLower(this StringBuilder builder, CultureInfo info)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            String result = builder.ToString().ToLower(info);
            return builder.Clear().Append(result);
        }

        public static StringBuilder ToLowerInvariant(this StringBuilder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            String result = builder.ToString().ToLowerInvariant();
            return builder.Clear().Append(result);
        }

        public static StringBuilder Normalize(this StringBuilder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            String result = builder.ToString().Normalize();
            return builder.Clear().Append(result);
        }

        public static StringBuilder Normalize(this StringBuilder builder, NormalizationForm normalization)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            String result = builder.ToString().Normalize(normalization);
            return builder.Clear().Append(result);
        }
        
        public static Boolean IsNormalized([NotNull] this StringBuilder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.ToString().IsNormalized();
        }

        public static Boolean IsNormalized([NotNull] this StringBuilder builder, NormalizationForm normalization)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.ToString().IsNormalized(normalization);
        }

        public static Boolean Contains([NotNull] this StringBuilder builder, String value)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.ToString().Contains(value);
        }

        public static Boolean Contains([NotNull] this StringBuilder builder, String value, StringComparison comparison)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.ToString().Contains(value, comparison);
        }

        public static Boolean Contains([NotNull] this StringBuilder builder, Char value)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.ToString().Contains(value);
        }

        public static Boolean Contains([NotNull] this StringBuilder builder, Char value, StringComparison comparison)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.ToString().Contains(value, comparison);
        }

        public static Int32 IndexOf([NotNull] this StringBuilder builder, Char value)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.ToString().IndexOf(value);
        }

        public static Int32 IndexOf([NotNull] this StringBuilder builder, Char value, Int32 start)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.ToString().IndexOf(value, start);
        }

        public static Int32 IndexOf([NotNull] this StringBuilder builder, Char value, StringComparison comparison)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.ToString().IndexOf(value, comparison);
        }

        public static Int32 IndexOf([NotNull] this StringBuilder builder, Char value, Int32 start, Int32 count)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.ToString().IndexOf(value, start, count);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("ReSharper", "StringIndexOfIsCultureSpecific.1")]
        public static Int32 IndexOf([NotNull] this StringBuilder builder, String value)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.ToString().IndexOf(value);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("ReSharper", "StringIndexOfIsCultureSpecific.2")]
        public static Int32 IndexOf([NotNull] this StringBuilder builder, String value, Int32 start)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.ToString().IndexOf(value, start);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("ReSharper", "StringIndexOfIsCultureSpecific.3")]
        public static Int32 IndexOf([NotNull] this StringBuilder builder, String value, Int32 start, Int32 count)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.ToString().IndexOf(value, start, count);
        }

        public static Int32 IndexOf([NotNull] this StringBuilder builder, String value, StringComparison comparison)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.ToString().IndexOf(value, comparison);
        }

        public static Int32 IndexOf([NotNull] this StringBuilder builder, String value, Int32 start, StringComparison comparison)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.ToString().IndexOf(value, start, comparison);
        }

        public static Int32 IndexOf([NotNull] this StringBuilder builder, String value, Int32 start, Int32 count, StringComparison comparison)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.ToString().IndexOf(value, start, count, comparison);
        }

        public static Int32 LastIndexOf([NotNull] this StringBuilder builder, Char value)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.ToString().LastIndexOf(value);
        }

        public static Int32 LastIndexOf([NotNull] this StringBuilder builder, Char value, Int32 start)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.ToString().LastIndexOf(value, start);
        }

        public static Int32 LastIndexOf([NotNull] this StringBuilder builder, Char value, Int32 start, Int32 count)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.ToString().LastIndexOf(value, start, count);
        }
        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("ReSharper", "StringLastIndexOfIsCultureSpecific.1")]
        public static Int32 LastIndexOf(this StringBuilder builder, String value)
        {
            return builder.ToString().LastIndexOf(value);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("ReSharper", "StringLastIndexOfIsCultureSpecific.2")]
        public static Int32 LastIndexOf(this StringBuilder builder, String value, Int32 start)
        {
            return builder.ToString().LastIndexOf(value, start);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("ReSharper", "StringLastIndexOfIsCultureSpecific.3")]
        public static Int32 LastIndexOf(this StringBuilder builder, String value, Int32 start, Int32 count)
        {
            return builder.ToString().LastIndexOf(value, start, count);
        }

        public static Int32 LastIndexOf(this StringBuilder builder, String value, StringComparison comparison)
        {
            return builder.ToString().LastIndexOf(value, comparison);
        }

        public static Int32 LastIndexOf(this StringBuilder builder, String value, Int32 start, StringComparison comparison)
        {
            return builder.ToString().LastIndexOf(value, start, comparison);
        }

        public static Int32 LastIndexOf(this StringBuilder builder, String value, Int32 start, Int32 count, StringComparison comparison)
        {
            return builder.ToString().LastIndexOf(value, start, count, comparison);
        }

        public static Int32 IndexOfAny(this StringBuilder builder, Char[] values)
        {
            return builder.ToString().IndexOfAny(values);
        }

        public static Int32 IndexOfAny(this StringBuilder builder, Char[] values, Int32 start)
        {
            return builder.ToString().IndexOfAny(values, start);
        }

        public static Int32 IndexOfAny(this StringBuilder builder, Char[] values, Int32 start, Int32 count)
        {
            return builder.ToString().IndexOfAny(values, start, count);
        }
        
        public static Int32 LastIndexOfAny(this StringBuilder builder, Char[] values)
        {
            return builder.ToString().LastIndexOfAny(values);
        }

        public static Int32 LastIndexOfAny(this StringBuilder builder, Char[] values, Int32 start)
        {
            return builder.ToString().LastIndexOfAny(values, start);
        }

        public static Int32 LastIndexOfAny(this StringBuilder builder, Char[] values, Int32 start, Int32 count)
        {
            return builder.ToString().LastIndexOfAny(values, start, count);
        }

        public static String[] Split(this StringBuilder builder, Char separator, StringSplitOptions options = StringSplitOptions.None)
        {
            return builder.ToString().Split(separator, options);
        }

        public static String[] Split(this StringBuilder builder, Char separator, Int32 count, StringSplitOptions options = StringSplitOptions.None)
        {
            return builder.ToString().Split(separator, count, options);
        }

        public static String[] Split(this StringBuilder builder, params Char[]? separator)
        {
            return builder.ToString().Split(separator);
        }

        public static String[] Split(this StringBuilder builder, Char[]? separator, Int32 count)
        {
            return builder.ToString().Split(separator, count);
        }

        public static String[] Split(this StringBuilder builder, Char[]? separator, StringSplitOptions options)
        {
            return builder.ToString().Split(separator, options);
        }

        public static String[] Split(this StringBuilder builder, Char[]? separator, Int32 count, StringSplitOptions options)
        {
            return builder.ToString().Split(separator, count, options);
        }

        public static String[] Split(this StringBuilder builder, String? separator, StringSplitOptions options = StringSplitOptions.None)
        {
            return builder.ToString().Split(separator, options);
        }

        public static String[] Split(this StringBuilder builder, String? separator, Int32 count, StringSplitOptions options = StringSplitOptions.None)
        {
            return builder.ToString().Split(separator, count, options);
        }

        public static String[] Split(this StringBuilder builder, String[]? separator, StringSplitOptions options)
        {
            return builder.ToString().Split(separator, options);
        }

        public static String[] Split(this StringBuilder builder, String[]? separator, Int32 count, StringSplitOptions options)
        {
            return builder.ToString().Split(separator, count, options);
        }

        public static StringBuilder Insert(Int32 start, String value)
        {
            return builder.ToString().Insert(start, value);
        }

        public static StringBuilder Remove(Int32 start)
        {
            return ToString().Remove(start);
        }

        public static StringBuilder Remove(Int32 start, Int32 count)
        {
            return ToString().Remove(start, count);
        }

        public static StringBuilder Replace(Char before, Char after)
        {
            return ToString().Replace(before, after);
        }

        public static StringBuilder Replace(String before, String? after)
        {
            return ToString().Replace(before, after);
        }

        public static StringBuilder Replace(String before, String? after, Boolean ignoreCase, CultureInfo? culture)
        {
            return ToString().Replace(before, after, ignoreCase, culture);
        }

        public static StringBuilder Replace(String before, String? after, StringComparison comparison)
        {
            return ToString().Replace(before, after, comparison);
        }

        public static StringBuilder Substring(Int32 start)
        {
            return ToString().Substring(start);
        }

        public static StringBuilder Substring(Int32 start, Int32 length)
        {
            return ToString().Substring(start, length);
        }

        public static StringBuilder Trim()
        {
            return ToString().Trim();
        }

        public static StringBuilder Trim(Char trim)
        {
            return ToString().Trim(trim);
        }

        public static StringBuilder Trim(params Char[]? trim)
        {
            return ToString().Trim(trim);
        }

        public static StringBuilder TrimStart()
        {
            return ToString().Trim();
        }

        public static StringBuilder TrimStart(Char trim)
        {
            return ToString().Trim(trim);
        }

        public static StringBuilder TrimStart(params Char[]? trim)
        {
            return ToString().Trim(trim);
        }

        public static StringBuilder TrimEnd()
        {
            return ToString().Trim();
        }

        public static StringBuilder TrimEnd(Char trim)
        {
            return ToString().Trim(trim);
        }

        public static StringBuilder TrimEnd(params Char[]? trim)
        {
            return ToString().Trim(trim);
        }
        
        public static Boolean StartsWith(String value)
        {
            return ToString().StartsWith(value);
        }

        public static Boolean StartsWith(String value, StringComparison comparison)
        {
            return ToString().StartsWith(value, comparison);
        }

        public static Boolean StartsWith(String value, Boolean ignoreCase, CultureInfo? culture)
        {
            return ToString().StartsWith(value, ignoreCase, culture);
        }

        public static Boolean StartsWith(Char value)
        {
            return ToString().StartsWith(value);
        }
        
        public static Boolean EndsWith(String value)
        {
            return ToString().EndsWith(value);
        }

        public static Boolean EndsWith(String value, StringComparison comparison)
        {
            return ToString().EndsWith(value, comparison);
        }

        public static Boolean EndsWith(String value, Boolean ignoreCase, CultureInfo? culture)
        {
            return ToString().EndsWith(value, ignoreCase, culture);
        }

        public static Boolean EndsWith(Char value)
        {
            return ToString().EndsWith(value);
        }

        public static StringBuilder PadLeft(Int32 width)
        {
            return ToString().PadLeft(width);
        }

        public static StringBuilder PadLeft(Int32 width, Char padding)
        {
            return ToString().PadLeft(width, padding);
        }

        public static StringBuilder PadRight(Int32 width)
        {
            return ToString().PadRight(width);
        }

        public static StringBuilder PadRight(Int32 width, Char padding)
        {
            return ToString().PadRight(width, padding);
        }
        
        public static StringRuneEnumerator EnumerateRunes(this StringBuilder builder)
        {
            return builder.ToString().EnumerateRunes();
        }

        public static Char[] ToCharArray([NotNull] this StringBuilder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.ToString().ToCharArray();
        }

        public static Char[] ToCharArray([NotNull] this StringBuilder builder, Int32 start, Int32 length)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.ToString().ToCharArray(start, length);
        }

        public static IEnumerable<Char> AsEnumerable([NotNull] this StringBuilder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            // ReSharper disable once ForCanBeConvertedToForeach
            for (Int32 i = 0; i < builder.Length; i++)
            {
                yield return builder[i];
            }
        }

        public static IEnumerator<Char> GetEnumerator([NotNull] this StringBuilder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return AsEnumerable(builder).GetEnumerator();
        }
    }
}