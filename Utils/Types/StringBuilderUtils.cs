// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using JetBrains.Annotations;
using NetExtender.Comparers;
using NetExtender.Types.Strings;
using NetExtender.Types.Strings.Interfaces;

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
            
            // ReSharper disable once PossibleMultipleEnumeration
            StringBuilder builder = new StringBuilder(source.CountIfMaterialized() ?? 16);
            // ReSharper disable once PossibleMultipleEnumeration
            foreach (Char chr in source)
            {
                builder.Append(chr);
            }
            
            return builder;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNull(this StringBuilder? builder)
        {
            return builder is null;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotNull(this StringBuilder? builder)
        {
            return builder is not null;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsEmpty(this StringBuilder? builder)
        {
            return builder is not null && builder.Length <= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotEmpty(this StringBuilder? builder)
        {
            return !IsEmpty(builder);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNullOrEmpty(this StringBuilder? builder)
        {
            return builder is null || builder.Length <= 0;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotNullOrEmpty(this StringBuilder? builder)
        {
            return !IsNullOrEmpty(builder);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsWhiteSpace(this StringBuilder? builder)
        {
            return builder is not null && builder.Length > 0 && builder.AsEnumerable().All(Char.IsWhiteSpace);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotWhiteSpace(this StringBuilder? builder)
        {
            return !IsWhiteSpace(builder);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsEmptyOrWhiteSpace(this StringBuilder? builder)
        {
            return builder is not null && (builder.Length <= 0 || builder.AsEnumerable().All(Char.IsWhiteSpace));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNullOrWhiteSpace(this StringBuilder? builder)
        {
            return builder is null || IsEmptyOrWhiteSpace(builder);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotNullOrWhiteSpace(this StringBuilder? builder)
        {
            return !IsNullOrWhiteSpace(builder);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static StringBuilder Append([NotNull] this StringBuilder builder, [NotNull] IEnumerable<Char> source)
        {
            return AppendRange(builder, source);
        }
        
        public static StringBuilder AppendRange([NotNull] this StringBuilder builder, [NotNull] IEnumerable<Char> source)
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
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static StringBuilder Prepend([NotNull] this StringBuilder builder, [NotNull] IEnumerable<Char> source)
        {
            return PrependRange(builder, source);
        }
        
        public static StringBuilder PrependRange([NotNull] this StringBuilder builder, [NotNull] IEnumerable<Char> source)
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
        
        public static Boolean StartsWith([NotNull] this StringBuilder builder, [NotNull] String value)
        {
            return StartsWith(builder, value, StringComparison.CurrentCulture);
        }

        public static Boolean StartsWith([NotNull] this StringBuilder builder, [NotNull] String value, StringComparison comparison)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (builder.Length < value.Length)
            {
                return false;
            }

            String start = builder.ToString(0, value.Length);
            return start.Equals(value, comparison);
        }
        
        public static Boolean StartsWith([NotNull] this StringBuilder builder, [NotNull] String value, Boolean ignoreCase, CultureInfo? culture)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return builder.Length >= value.Length && builder.ToString(0, value.Length).StartsWith(value, ignoreCase, culture);
        }

        public static Boolean StartsWith([NotNull] this StringBuilder builder, Char value)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.Length > 0 && builder[0] == value;
        }
        
        public static Boolean EndsWith([NotNull] this StringBuilder builder, [NotNull] String value)
        {
            return EndsWith(builder, value, StringComparison.CurrentCulture);
        }

        public static Boolean EndsWith([NotNull] this StringBuilder builder, [NotNull] String value, StringComparison comparison)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (builder.Length < value.Length)
            {
                return false;
            }

            String end = builder.ToString(builder.Length - value.Length, value.Length);
            return end.Equals(value, comparison);
        }
        
        public static Boolean EndsWith([NotNull] this StringBuilder builder, [NotNull] String value, Boolean ignoreCase, CultureInfo? culture)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            
            return builder.Length >= value.Length && builder.ToString(builder.Length - value.Length, value.Length).EndsWith(value, ignoreCase, culture);
        }

        public static Boolean EndsWith([NotNull] this StringBuilder builder, Char value)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.Length > 0 && builder[^1] == value;
        }
        
        public static StringBuilder Trim([NotNull] this StringBuilder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.TrimStart().TrimEnd();
        }

        public static StringBuilder Trim([NotNull] this StringBuilder builder, Char trim)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.TrimStart(trim).TrimEnd(trim);
        }

        public static StringBuilder Trim([NotNull] this StringBuilder builder, params Char[]? trim)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.TrimStart(trim).TrimEnd(trim);
        }
        
        public static StringBuilder TrimStart([NotNull] this StringBuilder builder, [NotNull] Func<Char, Boolean> predicate)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return builder.Length > 0 ? builder.Remove(0, builder.AsEnumerable().CountWhile(predicate)) : builder;
        }

        public static StringBuilder TrimStart([NotNull] this StringBuilder builder)
        {
            return TrimStart(builder, Char.IsWhiteSpace);
        }

        public static StringBuilder TrimStart([NotNull] this StringBuilder builder, Char trim)
        {
            return TrimStart(builder, trim.Equals);
        }

        public static StringBuilder TrimStart([NotNull] this StringBuilder builder, params Char[]? trim)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (builder.Length <= 0 || trim is null || trim.Length <= 0)
            {
                return builder;
            }

            return TrimStart(builder, trim.Contains);
        }

        private static StringBuilder TrimEnd([NotNull] this StringBuilder builder, [NotNull] Func<Char, Boolean> predicate)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            if (builder.Length <= 0)
            {
                return builder;
            }

            Int32 count = builder.AsEnumerable().ReverseCountWhile(predicate);
            return builder.Remove(builder.Length - count, count);
        }
        
        public static StringBuilder TrimEnd([NotNull] this StringBuilder builder)
        {
            return TrimEnd(builder, Char.IsWhiteSpace);
        }

        public static StringBuilder TrimEnd([NotNull] this StringBuilder builder, Char trim)
        {
            return TrimEnd(builder, trim.Equals);
        }

        public static StringBuilder TrimEnd([NotNull] this StringBuilder builder, params Char[]? trim)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            
            if (builder.Length <= 0 || trim is null || trim.Length <= 0)
            {
                return builder;
            }

            return TrimEnd(builder, trim.Contains);
        }

        public static StringBuilder Trim([NotNull] this StringBuilder builder, Int32 length)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.Length > 2 * length ? builder.TrimStart(length).TrimEnd(length) : builder.Clear();
        }
        
        public static StringBuilder TrimStart([NotNull] this StringBuilder builder, Int32 length)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.Length > length ? builder.Remove(0, length) : builder.Clear();
        }
        
        public static StringBuilder TrimEnd([NotNull] this StringBuilder builder, Int32 length)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.Length > length ? builder.Remove(builder.Length - length, length) : builder.Clear();
        }

        public static StringBuilder PadLeft(this StringBuilder builder, Int32 width)
        {
            return PadLeft(builder, width, ' ');
        }

        public static StringBuilder PadLeft([NotNull] this StringBuilder builder, Int32 width, Char padding)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return width > builder.Length ? builder.Prepend(padding.Repeat(Math.Min(width, builder.MaxCapacity) - builder.Length)) : builder;
        }

        public static StringBuilder PadRight(this StringBuilder builder, Int32 width)
        {
            return PadRight(builder, width, ' ');
        }

        public static StringBuilder PadRight([NotNull] this StringBuilder builder, Int32 width, Char padding)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return width > builder.Length ? builder.Append(padding.Repeat(Math.Min(width, builder.MaxCapacity) - builder.Length)) : builder;
        }

        public static StringBuilder Remove([NotNull] this StringBuilder builder, Int32 start)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.Remove(start, builder.Length - start);
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

        public static StringBuilder ReplaceInsert([NotNull] this StringBuilder builder, ReadOnlySpan<Char> replace)
        {
            return ReplaceInsert(builder, replace, 0);
        }

        public static StringBuilder ReplaceInsert([NotNull] this StringBuilder builder, ReadOnlySpan<Char> replace, Int32 index)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            
            if (replace.IsEmpty)
            {
                return builder;
            }

            if (builder.Length <= 0)
            {
                if (replace.Length > builder.MaxCapacity)
                {
                    throw new ArgumentOutOfRangeException(nameof(builder));
                }

                return builder.Append(replace);
            }

            if (index > builder.Length)
            {
                index = builder.Length;
            }
            else if (index + builder.Length < 0)
            {
                index = builder.Length - replace.Length;
            }

            if (index + replace.Length > builder.MaxCapacity)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            if (index >= 0)
            {
                for (Int32 i = 0; i < replace.Length; i++)
                {
                    Int32 position = index + i;

                    if (position < builder.Length)
                    {
                        builder[position] = replace[i];
                    }
                    else
                    {
                        builder.Append(replace[i]);
                    }
                }
            }
            else
            {
                for (Int32 i = replace.Length - 1; i >= 0; i--)
                {
                    Int32 position = index + i;

                    if (position < 0)
                    {
                        builder.Prepend(replace[i]);
                    }
                    else
                    {
                        builder[position] = replace[i];
                    }
                }
            }

            return builder;
        }

        public static StringBuilder Repeat([NotNull] this StringBuilder builder, Int32 count)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (count <= 1)
            {
                return builder;
            }

            if (builder.Length * count > builder.MaxCapacity)
            {
                throw new ArgumentOutOfRangeException(nameof(builder));
            }

            return builder.Insert(0, builder.ToString(), count);
        }
        
        public static StringBuilder Shuffle([NotNull] this StringBuilder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (builder.Length <= 1)
            {
                return builder;
            }

            String str = builder.ToString();
            return builder.Clear().Append(str.Shuffle());
        }
        
        public static StringBuilder Shuffle([NotNull] this StringBuilder builder, Int32 index)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            
            if (index < 0 || index >= builder.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            if (builder.Length <= 1)
            {
                return builder;
            }

            String str = builder.ToString();
            return builder.Clear().Append(str.Shuffle(index));
        }
        
        public static StringBuilder Shuffle([NotNull] this StringBuilder builder, Int32 index, Int32 length)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            
            if (length < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(length));
            }

            if (index < 0 || index + length >= builder.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            if (length <= 0 || builder.Length <= 1)
            {
                return builder;
            }

            String str = builder.ToString();
            return builder.Clear().Append(str.Shuffle(index, length));
        }

        public static StringBuilder ToUpper([NotNull] this StringBuilder builder)
        {
            return ToUpper(builder, null);
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
            return ToLower(builder, null);
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
        
        public static StringBuilder ToCapitalizeFirstChar([NotNull] this StringBuilder builder)
        {
            return ToCapitalizeFirstChar(builder, null);
        }
        
        public static StringBuilder ToCapitalizeFirstChar([NotNull] this StringBuilder builder, CultureInfo info)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (builder.Length <= 0)
            {
                return builder;
            }

            builder[0] = builder[0].ToUpper(info);
            return builder;
        }
        
        public static StringBuilder ToCapitalizeFirstCharLower([NotNull] this StringBuilder builder)
        {
            return ToCapitalizeFirstCharLower(builder, null);
        }
        
        public static StringBuilder ToCapitalizeFirstCharLower([NotNull] this StringBuilder builder, CultureInfo? info)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.Length > 0 ? ToCapitalizeFirstChar(builder.ToLower(info), info) : builder;
        }

        public static StringBuilder ToTitleCase([NotNull] this StringBuilder builder)
        {
            return ToTitleCase(builder, null);
        }
        
        public static StringBuilder ToTitleCase([NotNull] this StringBuilder builder, CultureInfo? info)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (builder.Length <= 0)
            {
                return builder;
            }

            String result = builder.ToString().ToTitleCase(info);
            return builder.Clear().Append(result);
        }
        
        public static StringBuilder ToTitleCaseLower([NotNull] this StringBuilder builder)
        {
            return ToTitleCaseLower(builder, null);
        }

        public static StringBuilder ToTitleCaseLower([NotNull] this StringBuilder builder, CultureInfo? info)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            String result = builder.ToString().ToTitleCaseLower(info);
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
            return IndexOf(builder, value) != -1;
        }

        public static Boolean Contains([NotNull] this StringBuilder builder, Char value, StringComparison comparison)
        {
            return IndexOf(builder, value, comparison) != -1;
        }

        public static Int32 IndexOf([NotNull] this StringBuilder builder, Char value)
        {
            return IndexOf(builder, value, 0);
        }

        public static Int32 IndexOf([NotNull] this StringBuilder builder, Char value, Int32 start)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return IndexOf(builder, value, start, builder.Length - start);
        }

        public static Int32 IndexOf([NotNull] this StringBuilder builder, Char value, StringComparison comparison)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            CharEqualityComparer comparer = new CharEqualityComparer(comparison);
            for (Int32 i = 0; i < builder.Length; i++)
            {
                if (comparer.Equals(builder[i], value))
                {
                    return i;
                }
            }

            return -1;
        }

        public static Int32 IndexOf([NotNull] this StringBuilder builder, Char value, Int32 start, Int32 count)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (start < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(start));
            }

            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            if (start + count >= builder.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }
            
            for (Int32 i = start; i < start + count; i++)
            {
                if (builder[i] == value)
                {
                    return i;
                }
            }

            return -1;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("ReSharper", "StringIndexOfIsCultureSpecific.1")]
        public static Int32 IndexOf([NotNull] this StringBuilder builder, String value)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            
            if (builder.Length < value.Length)
            {
                return -1;
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

            if (start < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(start));
            }
            
            if (builder.Length - start < value.Length)
            {
                return -1;
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
            
            if (start < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(start));
            }

            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            if (count < value.Length)
            {
                return -1;
            }
            
            if (builder.Length - start < value.Length)
            {
                return -1;
            }

            return builder.ToString().IndexOf(value, start, count);
        }

        public static Int32 IndexOf([NotNull] this StringBuilder builder, String value, StringComparison comparison)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            
            if (builder.Length < value.Length)
            {
                return -1;
            }

            return builder.ToString().IndexOf(value, comparison);
        }

        public static Int32 IndexOf([NotNull] this StringBuilder builder, String value, Int32 start, StringComparison comparison)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            
            if (start < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(start));
            }
            
            if (builder.Length - start < value.Length)
            {
                return -1;
            }

            return builder.ToString().IndexOf(value, start, comparison);
        }

        public static Int32 IndexOf([NotNull] this StringBuilder builder, String value, Int32 start, Int32 count, StringComparison comparison)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            
            if (start < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(start));
            }

            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            if (count < value.Length)
            {
                return -1;
            }
            
            if (builder.Length - start < value.Length)
            {
                return -1;
            }

            return builder.ToString().IndexOf(value, start, count, comparison);
        }

        public static Int32 LastIndexOf([NotNull] this StringBuilder builder, Char value)
        {
            return LastIndexOf(builder, value, 0);
        }

        public static Int32 LastIndexOf([NotNull] this StringBuilder builder, Char value, Int32 start)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return LastIndexOf(builder, value, start, builder.Length - start);
        }

        public static Int32 LastIndexOf([NotNull] this StringBuilder builder, Char value, Int32 start, Int32 count)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (start < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(start));
            }

            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            if (start + count >= builder.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }
            
            for (Int32 i = builder.Length - count - 1; i >= start; i--)
            {
                if (builder[i] == value)
                {
                    return i;
                }
            }

            return -1;
        }
        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("ReSharper", "StringLastIndexOfIsCultureSpecific.1")]
        public static Int32 LastIndexOf([NotNull] this StringBuilder builder, String value)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (builder.Length < value.Length)
            {
                return -1;
            }

            return builder.ToString().LastIndexOf(value);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("ReSharper", "StringLastIndexOfIsCultureSpecific.2")]
        public static Int32 LastIndexOf([NotNull] this StringBuilder builder, String value, Int32 start)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (start < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(start));
            }
            
            if (builder.Length - start < value.Length)
            {
                return -1;
            }

            return builder.ToString().LastIndexOf(value, start);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("ReSharper", "StringLastIndexOfIsCultureSpecific.3")]
        public static Int32 LastIndexOf([NotNull] this StringBuilder builder, String value, Int32 start, Int32 count)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (start < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(start));
            }

            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            if (count < value.Length)
            {
                return -1;
            }
            
            if (builder.Length - start < value.Length)
            {
                return -1;
            }

            return builder.ToString().LastIndexOf(value, start, count);
        }

        public static Int32 LastIndexOf([NotNull] this StringBuilder builder, String value, StringComparison comparison)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            
            if (builder.Length < value.Length)
            {
                return -1;
            }

            return builder.ToString().LastIndexOf(value, comparison);
        }

        public static Int32 LastIndexOf([NotNull] this StringBuilder builder, String value, Int32 start, StringComparison comparison)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            
            if (start < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(start));
            }
            
            if (builder.Length - start < value.Length)
            {
                return -1;
            }

            return builder.ToString().LastIndexOf(value, start, comparison);
        }

        public static Int32 LastIndexOf([NotNull] this StringBuilder builder, String value, Int32 start, Int32 count, StringComparison comparison)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            
            if (start < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(start));
            }

            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            if (count < value.Length)
            {
                return -1;
            }
            
            if (builder.Length - start < value.Length)
            {
                return -1;
            }

            return builder.ToString().LastIndexOf(value, start, count, comparison);
        }

        public static Int32 IndexOfAny([NotNull] this StringBuilder builder, Char[] values)
        {
            return IndexOfAny(builder, values, 0);
        }

        public static Int32 IndexOfAny([NotNull] this StringBuilder builder, Char[] values, Int32 start)
        {
            return IndexOfAny(builder, values, start, builder.Length - start);
        }

        public static Int32 IndexOfAny([NotNull] this StringBuilder builder, Char[] values, Int32 start, Int32 count)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (start < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(start));
            }

            if (count <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            if (start + count >= builder.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }
            
            if (!(values?.Length > 0))
            {
                return -1;
            }

            for (Int32 i = start; i < start + count; i++)
            {
                if (values.Contains(builder[i]))
                {
                    return i;
                }
            }

            return -1;
        }
        
        public static Int32 LastIndexOfAny([NotNull] this StringBuilder builder, Char[] values)
        {
            return LastIndexOfAny(builder, values, 0);
        }

        public static Int32 LastIndexOfAny([NotNull] this StringBuilder builder, Char[] values, Int32 start)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return LastIndexOfAny(builder, values, start, builder.Length - start);
        }

        public static Int32 LastIndexOfAny([NotNull] this StringBuilder builder, Char[] values, Int32 start, Int32 count)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (start < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(start));
            }

            if (count <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            if (start + count >= builder.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }
            
            if (!(values?.Length > 0))
            {
                return -1;
            }
            
            for (Int32 i = builder.Length - count - 1; i >= start; i--)
            {
                if (values.Contains(builder[i]))
                {
                    return i;
                }
            }

            return -1;
        }

        public static IEnumerable<StringBuilder> Split([NotNull] this StringBuilder builder, Char separator, StringSplitOptions options = StringSplitOptions.None)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.ToString().Split(separator, options).Select(str => new StringBuilder(str));
        }

        public static IEnumerable<StringBuilder> Split([NotNull] this StringBuilder builder, Char separator, Int32 count, StringSplitOptions options = StringSplitOptions.None)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.ToString().Split(separator, count, options).Select(str => new StringBuilder(str));
        }

        public static IEnumerable<StringBuilder> Split([NotNull] this StringBuilder builder, params Char[]? separator)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.ToString().Split(separator).Select(str => new StringBuilder(str));
        }

        public static IEnumerable<StringBuilder> Split([NotNull] this StringBuilder builder, Char[]? separator, Int32 count)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.ToString().Split(separator, count).Select(str => new StringBuilder(str));
        }

        public static IEnumerable<StringBuilder> Split([NotNull] this StringBuilder builder, Char[]? separator, StringSplitOptions options)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.ToString().Split(separator, options).Select(str => new StringBuilder(str));
        }

        public static IEnumerable<StringBuilder> Split([NotNull] this StringBuilder builder, Char[]? separator, Int32 count, StringSplitOptions options)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.ToString().Split(separator, count, options).Select(str => new StringBuilder(str));
        }

        public static IEnumerable<StringBuilder> Split([NotNull] this StringBuilder builder, String? separator, StringSplitOptions options = StringSplitOptions.None)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.ToString().Split(separator, options).Select(str => new StringBuilder(str));
        }

        public static IEnumerable<StringBuilder> Split([NotNull] this StringBuilder builder, String? separator, Int32 count, StringSplitOptions options = StringSplitOptions.None)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.ToString().Split(separator, count, options).Select(str => new StringBuilder(str));
        }

        public static IEnumerable<StringBuilder> Split([NotNull] this StringBuilder builder, String[]? separator, StringSplitOptions options)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.ToString().Split(separator, options).Select(str => new StringBuilder(str));
        }

        public static IEnumerable<StringBuilder> Split([NotNull] this StringBuilder builder, String[]? separator, Int32 count, StringSplitOptions options)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return builder.ToString().Split(separator, count, options).Select(str => new StringBuilder(str));
        }

        public static StringBuilder Replace([NotNull] this StringBuilder builder, Char before, Char after)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            for (Int32 i = 0; i < builder.Length; i++)
            {
                if (builder[i] == before)
                {
                    builder[i] = after;
                }
            }

            return builder;
        }

        public static StringBuilder Replace([NotNull] this StringBuilder builder, String before, String? after)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            String result = builder.ToString().Replace(before, after);

            if (result.Length > builder.MaxCapacity)
            {
                throw new ArgumentOutOfRangeException(nameof(builder));
            }
            
            return builder.Clear().Append(result);
        }

        public static StringBuilder Replace([NotNull] this StringBuilder builder, String before, String? after, Boolean ignoreCase, CultureInfo? culture)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            String result = builder.ToString().Replace(before, after, ignoreCase, culture);
            
            if (result.Length > builder.MaxCapacity)
            {
                throw new ArgumentOutOfRangeException(nameof(builder));
            }
            
            return builder.Clear().Append(result);
        }

        public static StringBuilder Replace([NotNull] this StringBuilder builder, String before, String? after, StringComparison comparison)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            String result = builder.ToString().Replace(before, after, comparison);
            
            if (result.Length > builder.MaxCapacity)
            {
                throw new ArgumentOutOfRangeException(nameof(builder));
            }
            
            return builder.Clear().Append(result);
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

        public static IString ToIString([NotNull] this StringBuilder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            return new StringBuilderAdapter(builder);
        }
    }
}