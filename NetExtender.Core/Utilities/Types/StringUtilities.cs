// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using NetExtender.Types.Maps;
using NetExtender.Types.Maps.Interfaces;
using NetExtender.Types.Strings;
using NetExtender.Types.Strings.Interfaces;

namespace NetExtender.Utilities.Types
{
    public enum SplitType
    {
        Characters,
        NewLine,
        Space,
        NewLineAndSpace,
        UpperCase,
        NewLineAndUpperCase,
        SpaceAndUpperCase,
        All
    }

    public enum JoinType
    {
        Default,
        NotEmpty,
        NotWhiteSpace
    }

    public static class StringUtilities
    {
        /// <summary>
        /// Cached null string task
        /// </summary>
        public static Task<String?> Null { get; } = Task.FromResult<String?>(null);

        /// <summary>
        /// Cached empty string task
        /// </summary>
        public static Task<String> Empty { get; } = Task.FromResult(String.Empty);

        public const String NullString = "null";

        public const String DefaultSeparator = " ";

        public const String NewLine = "\n";

        public const String FormatVariableRegexPattern = @"\{([^\{\}]+)\}";

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String? ToString<T>(T value)
        {
            return value?.ToString();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String? ToString<T>(T value, IFormatProvider? provider)
        {
            return ToString(value, null, provider);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String? ToString<T>(T value, String? format, IFormatProvider? provider)
        {
            if (value is IFormattable formattable)
            {
                return formattable.ToString(format, provider);
            }

            return value?.ToString();
        }
        
        public static Int32 CharLength(this String?[] values)
        {
            if (values is null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            return values.Length switch
            {
                0 => 0,
                1 => values[0]?.Length ?? 0,
                _ => values.WhereNotNull().Sum(str => str.Length)
            };
        }

        public static Int32 CharLength(this IEnumerable<String?> values)
        {
            if (values is null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            return values.WhereNotNull().Sum(str => str.Length);
        }

        public static Int32 CharLength(this IString?[] values)
        {
            if (values is null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            return values.Length switch
            {
                0 => 0,
                1 => values[0]?.Length ?? 0,
                _ => values.WhereNotNull().Sum(str => str.Length)
            };
        }

        public static Int32 CharLength(this IEnumerable<IString?> values)
        {
            if (values is null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            return values.WhereNotNull().Sum(str => str.Length);
        }

        public static Int64 CharLongLength(this String?[] values)
        {
            if (values is null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            return values.Length switch
            {
                0 => 0,
                1 => values[0]?.Length ?? 0,
                _ => values.WhereNotNull().Aggregate<String, Int64>(0, (current, str) => current + str.Length)
            };
        }

        public static Int64 CharLongLength(this IEnumerable<String?> values)
        {
            if (values is null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            return values.WhereNotNull().Aggregate<String, Int64>(0, (current, str) => current + str.Length);
        }

        public static Int64 CharLongLength(this IString?[] values)
        {
            if (values is null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            return values.Length switch
            {
                0 => 0,
                1 => values[0]?.Length ?? 0,
                _ => values.WhereNotNull().Aggregate<IString, Int64>(0, (current, str) => current + str.Length)
            };
        }

        public static Int64 CharLongLength(this IEnumerable<IString?> values)
        {
            if (values is null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            return values.WhereNotNull().Aggregate<IString, Int64>(0, (current, str) => current + str.Length);
        }

        public static IEnumerable<Int32> AllIndexesOf(this String value, String pattern)
        {
            return AllIndexesOf(value, pattern, StringComparison.Ordinal);
        }

        public static IEnumerable<Int32> AllIndexesOf(this String value, String pattern, StringComparison comparison)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (pattern is null)
            {
                throw new ArgumentNullException(nameof(pattern));
            }

            Int32 index = value.IndexOf(pattern, comparison);
            while (index != -1)
            {
                yield return index;
                index = value.IndexOf(pattern, index + pattern.Length, comparison);
            }
        }

        public static IEnumerable<String> GetFormatVariables(String value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return Regex.Matches(value, FormatVariableRegexPattern, RegexOptions.Compiled)
                .Select(match => match.Value)
                .Select(format => Regex.Replace(format.ToLower(), @"\{|\}", String.Empty));
        }

        //TODO: To ImmutableMap;
        private static IMap<Char, Char> Brackets { get; } = new Map<Char, Char>(4)
        {
            {'(', ')'},
            {'{', '}'},
            {'[', ']'},
            {'<', '>'}
        };

        public static Boolean IsBracketsWellFormed(this String value)
        {
            return IsBracketsWellFormed(value, Brackets);
        }

        // ReSharper disable once CognitiveComplexity
        public static Boolean IsBracketsWellFormed(this String value, IEnumerable<KeyValuePair<Char, Char>> pairs)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (pairs is null)
            {
                throw new ArgumentNullException(nameof(pairs));
            }

            if (value.Length <= 0)
            {
                return true;
            }

            IReadOnlyMap<Char, Char> brackets = pairs.AsIReadOnlyMap();

            Stack<Char> order = new Stack<Char>();

            foreach (Char character in value)
            {
                if (brackets.ContainsKey(character))
                {
                    order.Push(character);
                    continue;
                }

                if (!brackets.ContainsValue(character))
                {
                    continue;
                }
                    
                if (order.Count <= 0)
                {
                    return false;
                }

                if (character != brackets.GetValue(order.Peek()))
                {
                    return false;
                }

                order.Pop();
            }

            return order.Count <= 0;
        }

        public static String ReplaceFrom<T>(this String source, IEnumerable<KeyValuePair<String, T?>> dictionary)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (dictionary is null)
            {
                throw new ArgumentNullException(nameof(dictionary));
            }

            return ReplaceFrom(source, dictionary.Select(pair => new KeyValuePair<String, String?>(pair.Key, pair.Value?.ToString())));
        }

        public static String ReplaceFrom(this String value, IEnumerable<KeyValuePair<String, String?>> source)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (value.Length <= 0)
            {
                return String.Empty;
            }

            return source switch
            {
                IDictionary<String, String?> dictionary => ReplaceFrom(value, dictionary),
                IReadOnlyDictionary<String, String?> dictionary => ReplaceFrom(value, dictionary),
                _ => ReplaceFrom(value, (IDictionary<String, String?>) new Dictionary<String, String?>(source))
            };
        }

        private static String ReplaceFrom(String source, IDictionary<String, String?> dictionary)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (dictionary is null)
            {
                throw new ArgumentNullException(nameof(dictionary));
            }

            if (source.Length <= 0)
            {
                return String.Empty;
            }

            String keys = dictionary.Keys.Join("|");

            return Regex.Replace(source, $@"\b({keys})\b",
                match => dictionary.TryGetValue(match.Value, out String? value) ? value ?? String.Empty : String.Empty);
        }

        private static String ReplaceFrom(String source, IReadOnlyDictionary<String, String?> dictionary)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (dictionary is null)
            {
                throw new ArgumentNullException(nameof(dictionary));
            }

            if (source.Length <= 0)
            {
                return String.Empty;
            }

            String keys = dictionary.Keys.Join("|");

            return Regex.Replace(source, $@"\b({keys})\b",
                match => dictionary.TryGetValue(match.Value, out String? value) ? value ?? String.Empty : String.Empty);
        }

        public static String FormatFrom<T>(this String value, IEnumerable<KeyValuePair<String, T?>> source)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (value.Length <= 0)
            {
                return String.Empty;
            }

            StringBuilder builder = new StringBuilder(value);

            Dictionary<String, Int32> keys = new Dictionary<String, Int32>();

            Int32 i = 0;

            void AddToKeys(String key)
            {
                builder = builder.Replace($"{{{key}}}", $"{{{i}}}");
                keys.Add(key, i++);
            }

            Object[] arguments = source.WhereNotNull().DistinctByKey().ForEachKey(AddToKeys).OrderBy(pair => keys[pair.Key]).Values().Cast<Object>().ToArray();

            return String.Format(builder.ToString(), arguments);
        }

        public static Int32 CountExpectedFormatArgs(this String value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (value.Length <= 0)
            {
                return 0;
            }

            const String pattern = @"(?<!\{)(?>\{\{)*\{\d(.*?)";

            MatchCollection matches = Regex.Matches(value, pattern, RegexOptions.Compiled);
            return matches.Select(match => match.Value).Distinct().Count();
        }

        /// <summary>
        /// Trim string after format variables
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static String? TrimAfterFormatVariables(String value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            Match match = Regex.Match(value, FormatVariableRegexPattern, RegexOptions.Compiled);
            return match.Success ? value.Substring(0, match.Index) : null;
        }

        public static String Reverse(this String value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (value.Length <= 1)
            {
                return value;
            }

            Span<Char> buffer = value.Length <= 10000 ? stackalloc Char[value.Length] : new Char[value.Length];

            for (Int32 i = 0; i < value.Length; i++)
            {
                buffer[i] = value[^i];
            }

            return new String(buffer);
        }

        public static String Format(this String format, Object? arg0)
        {
            if (format is null)
            {
                throw new ArgumentNullException(nameof(format));
            }

            return String.Format(format, arg0);
        }

        public static String Format(this IString format, Object? arg0)
        {
            if (format is null)
            {
                throw new ArgumentNullException(nameof(format));
            }

            return Format(format.ToString(), arg0);
        }

        public static String Format(this String format, Object? arg0, Object? arg1)
        {
            if (format is null)
            {
                throw new ArgumentNullException(nameof(format));
            }

            return String.Format(format, arg0, arg1);
        }

        public static String Format(this IString format, Object? arg0, Object? arg1)
        {
            if (format is null)
            {
                throw new ArgumentNullException(nameof(format));
            }

            return Format(format.ToString(), arg0, arg1);
        }

        public static String Format(this String format, Object? arg0, Object? arg1, Object? arg2)
        {
            if (format is null)
            {
                throw new ArgumentNullException(nameof(format));
            }

            return String.Format(format, arg0, arg1, arg2);
        }

        public static String Format(this IString format, Object? arg0, Object? arg1, Object? arg2)
        {
            if (format is null)
            {
                throw new ArgumentNullException(nameof(format));
            }

            return Format(format.ToString(), arg0, arg1, arg2);
        }

        public static String Format(this String format, params Object?[] args)
        {
            if (format is null)
            {
                throw new ArgumentNullException(nameof(format));
            }

            if (args is null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            return String.Format(format, args);
        }

        public static String Format(this IString format, params Object?[] args)
        {
            if (format is null)
            {
                throw new ArgumentNullException(nameof(format));
            }

            if (args is null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            return Format(format.ToString(), args);
        }

        public static String Format(this String format, IFormatProvider? provider, Object? arg0)
        {
            if (format is null)
            {
                throw new ArgumentNullException(nameof(format));
            }

            return String.Format(provider, format, arg0);
        }

        public static String Format(this IString format, IFormatProvider? provider, Object? arg0)
        {
            if (format is null)
            {
                throw new ArgumentNullException(nameof(format));
            }

            return Format(format.ToString(), provider, arg0);
        }

        public static String Format(this String format, IFormatProvider? provider, Object? arg0, Object? arg1)
        {
            if (format is null)
            {
                throw new ArgumentNullException(nameof(format));
            }

            return String.Format(provider, format, arg0, arg1);
        }

        public static String Format(this IString format, IFormatProvider? provider, Object? arg0, Object? arg1)
        {
            if (format is null)
            {
                throw new ArgumentNullException(nameof(format));
            }

            return Format(format.ToString(), provider, arg0, arg1);
        }

        public static String Format(this String format, IFormatProvider? provider, Object? arg0, Object? arg1, Object? arg2)
        {
            if (format is null)
            {
                throw new ArgumentNullException(nameof(format));
            }

            return String.Format(provider, format, arg0, arg1, arg2);
        }

        public static String Format(this IString format, IFormatProvider? provider, Object? arg0, Object? arg1, Object? arg2)
        {
            if (format is null)
            {
                throw new ArgumentNullException(nameof(format));
            }

            return Format(format.ToString(), provider, arg0, arg1, arg2);
        }

        public static String Format(this String format, IFormatProvider? provider, params Object?[] args)
        {
            if (format is null)
            {
                throw new ArgumentNullException(nameof(format));
            }

            if (args is null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            return String.Format(provider, format, args);
        }

        public static String Format(this IString format, IFormatProvider? provider, params Object?[] args)
        {
            if (format is null)
            {
                throw new ArgumentNullException(nameof(format));
            }

            return Format(format.ToString(), provider, args);
        }

        public static Object?[] FormatSafeGetArguments(Object?[]? args, Int32 expected)
        {
            if (args is null)
            {
                return expected > 0 ? Enumerable.Repeat((Object) NullString, expected).ToArray() : Array.Empty<Object>();
            }

            return (args.Length - expected) switch
            {
                0 => args,
                < 0 => args.Concat(Enumerable.Repeat(NullString, expected - args.Length)).ToArray(),
                > 0 => args.Take(expected).ToArray()
            };
        }

        public static String FormatSafe(this String source, params Object?[]? args)
        {
            return FormatSafe(source, null, args);
        }

        public static String FormatSafe(this String source, IFormatProvider? provider, params Object?[]? args)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (args is null)
            {
                return source;
            }

            Int32 expected = CountExpectedFormatArgs(source);

            return expected > 0 ? String.Format(provider, source, FormatSafeGetArguments(args, expected)) : source;
        }

        public static String FormatSafe(this IString source, params Object?[]? args)
        {
            return FormatSafe(source, null, args);
        }

        public static String FormatSafe(this IString source, IFormatProvider? provider, params Object?[]? args)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return FormatSafe(source.ToString(), provider, args);
        }

        public static Boolean EndsWith(this String value, IEnumerable<Char> chars)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (chars is null)
            {
                throw new ArgumentNullException(nameof(chars));
            }

            return chars.Any(value.EndsWith);
        }

        public static Boolean EndsWith(this IString value, IEnumerable<Char> chars)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return EndsWith(value.ToString(), chars);
        }

        public static Boolean EndsWith(this String value, IEnumerable<String> substrings)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (substrings is null)
            {
                throw new ArgumentNullException(nameof(substrings));
            }

            return substrings.Any(value.EndsWith);
        }

        public static Boolean EndsWith(this IString value, IEnumerable<String> substrings)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return EndsWith(value.ToString(), substrings);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNull(this String? value)
        {
            return value is null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNull(this IString? value)
        {
            return value?.ToString() is null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotNull(this String? value)
        {
            return value is not null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotNull(this IString? value)
        {
            return value?.ToString() is not null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsEmpty(this String? value)
        {
            return value == String.Empty;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsEmpty(this IString? value)
        {
            return value is not null && value.Length <= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotEmpty(this String? value)
        {
            return !IsEmpty(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotEmpty(this IString? value)
        {
            return !IsEmpty(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsWhiteSpace(this String? value)
        {
            return value is not null && value.Length > 0 && value.All(Char.IsWhiteSpace);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsWhiteSpace(this IString? value)
        {
            return value is not null && value.Length > 0 && value.All(Char.IsWhiteSpace);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotWhiteSpace(this String? value)
        {
            return !IsWhiteSpace(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotWhiteSpace(this IString? value)
        {
            return !IsWhiteSpace(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsEmptyOrWhiteSpace(this String? value)
        {
            return value is not null && (value.Length <= 0 || value.All(Char.IsWhiteSpace));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsEmptyOrWhiteSpace(this IString? value)
        {
            return value is not null && (value.Length <= 0 || value.All(Char.IsWhiteSpace));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNullOrEmpty(this String? value)
        {
            return String.IsNullOrEmpty(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNullOrEmpty(this IString? value)
        {
            return value is null || value.Length <= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNullOrWhiteSpace(this String? value)
        {
            return String.IsNullOrWhiteSpace(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNullOrWhiteSpace(this IString? value)
        {
            return value is null || value.Length <= 0 || value.All(Char.IsWhiteSpace);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotNullOrEmpty(this String? value)
        {
            return !IsNullOrEmpty(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotNullOrEmpty(this IString? value)
        {
            return !IsNullOrEmpty(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotNullOrWhiteSpace(this String? value)
        {
            return !IsNullOrWhiteSpace(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotNullOrWhiteSpace(this IString? value)
        {
            return !IsNullOrWhiteSpace(value);
        }

        /// <inheritdoc cref="Char.GetUnicodeCategory(String,Int32)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UnicodeCategory GetUnicodeCategory(this String value, Int32 index)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return Char.GetUnicodeCategory(value, index);
        }

        /// <inheritdoc cref="Char.GetUnicodeCategory(String,Int32)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UnicodeCategory GetUnicodeCategory(this IString value, Int32 index)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return GetUnicodeCategory(value.ToString(), index);
        }

        public static Boolean IsAllCharacterInRange(this String value, Int32 min, Int32 max)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (min < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(min));
            }

            if (max < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(max));
            }

            if (value.Length <= 0)
            {
                return true;
            }

            if (max < min)
            {
                (min, max) = (max, min);
            }

            // ReSharper disable once ForCanBeConvertedToForeach
            // ReSharper disable once LoopCanBeConvertedToQuery
            for (Int32 i = 0; i < value.Length; i++)
            {
                Char character = value[i];
                if (character < min || character > max)
                {
                    return false;
                }
            }

            return true;
        }

        public static Boolean IsAllCharacterInRange(this IString value, Int32 min, Int32 max)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return IsAllCharacterInRange(value.ToString(), min, max);
        }

        public static Boolean IsAscii(this String value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (value.Length <= 0)
            {
                return true;
            }

            // ReSharper disable once ForCanBeConvertedToForeach
            // ReSharper disable once LoopCanBeConvertedToQuery
            for (Int32 i = 0; i < value.Length; i++)
            {
                if (value[i] > Byte.MaxValue)
                {
                    return false;
                }
            }

            return true;
        }

        public static Boolean IsAscii(this IString value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return IsAscii(value.ToString());
        }

        public static Boolean IsAsciiCharacters(this String value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return value.Length <= 0 || value.All(chr => chr >= 0x20 && chr <= Byte.MaxValue);
        }

        public static Boolean IsAsciiCharacters(this IString value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return IsAsciiCharacters(value.ToString());
        }

        public static String[] SplitByChars(String value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return value.Split();
        }

        public static String[] SplitByChars(IString value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return SplitByChars(value.ToString());
        }

        public static String[] SplitByNewLine(String value, StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries)
        {
            return SplitByNewLine(value, Int32.MaxValue, options);
        }

        public static String[] SplitByNewLine(String value, Int32 count, StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return value.Split(NewLine, count, options);
        }

        public static String[] SplitByNewLine(IString value, StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return SplitByNewLine(value.ToString(), options);
        }

        public static String[] SplitByNewLine(IString value, Int32 count, StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return SplitByNewLine(value.ToString(), count, options);
        }

        public static String[] SplitBySpace(String value, StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries)
        {
            return SplitBySpace(value, Int32.MaxValue, options);
        }

        public static String[] SplitBySpace(String value, Int32 count, StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return value.Split(' ', count, options);
        }

        public static String[] SplitBySpace(IString value, StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return SplitBySpace(value.ToString(), options);
        }

        public static String[] SplitBySpace(IString value, Int32 count, StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return SplitBySpace(value.ToString(), count, options);
        }

        private static readonly Char[] NewLineAndSpaceChars = {'\n', ' '};

        public static String[] SplitByNewLineAndSpace(String value, StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries)
        {
            return SplitByNewLineAndSpace(value, Int32.MaxValue, options);
        }

        public static String[] SplitByNewLineAndSpace(String value, Int32 count, StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return value.Split(NewLineAndSpaceChars, count, options);
        }

        public static String[] SplitByNewLineAndSpace(IString value, StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return SplitByNewLineAndSpace(value.ToString(), options);
        }

        public static String[] SplitByNewLineAndSpace(IString value, Int32 count, StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return SplitByNewLineAndSpace(value.ToString(), count, options);
        }

        public static String[] SplitByUpperCase(String value, StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries)
        {
            return SplitByUpperCaseInternal(value, options).ToArray();
        }

        public static String[] SplitByUpperCase(IString value, StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return SplitByUpperCase(value.ToString(), options);
        }

        // ReSharper disable once CognitiveComplexity
        private static IEnumerable<String> SplitByUpperCaseInternal(String value, StringSplitOptions options)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (value.Length <= 0)
            {
                yield break;
            }

            Boolean allentries = options == StringSplitOptions.None;

            Int32 index = 0;
            UnicodeCategory ctype = value.GetUnicodeCategory(index);
            String result;
            for (Int32 pos = 1; pos < value.Length; pos++)
            {
                Char current = value[pos];
                UnicodeCategory type = current.GetUnicodeCategory();

                if (ctype == type)
                {
                    continue;
                }

                if (ctype == UnicodeCategory.UppercaseLetter && type == UnicodeCategory.LowercaseLetter)
                {
                    Int32 token = pos - 1;
                    if (token != index)
                    {
                        result = value.Substring(index, token - index);
                        if (allentries || !String.IsNullOrWhiteSpace(result))
                        {
                            yield return result;
                        }

                        index = token;
                    }
                }
                else
                {
                    result = value.Substring(index, pos - index);
                    if (allentries || !String.IsNullOrWhiteSpace(result))
                    {
                        yield return result;
                    }

                    index = pos;
                }

                ctype = type;
            }

            result = value.Substring(index, value.Length - index);
            if (allentries || !String.IsNullOrWhiteSpace(result))
            {
                yield return result;
            }
        }

        private static String[] SplitByUpperCase(IEnumerable<String> split, StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries)
        {
            if (split is null)
            {
                throw new ArgumentNullException(nameof(split));
            }

            return split.SelectManyWhereNotNull(str => SplitByUpperCase(str, options)).ToArray();
        }

        public static String[] SplitBy(this String value, SplitType split = SplitType.NewLine, StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return split switch
            {
                SplitType.Characters => SplitByChars(value),
                SplitType.NewLine => SplitByNewLine(value, options),
                SplitType.Space => SplitBySpace(value, options),
                SplitType.UpperCase => SplitByUpperCase(value, options),
                SplitType.NewLineAndSpace => SplitByNewLineAndSpace(value, options),
                SplitType.NewLineAndUpperCase => SplitByUpperCase(SplitByNewLine(value, options), options),
                SplitType.SpaceAndUpperCase => SplitByUpperCase(SplitBySpace(value, options), options),
                SplitType.All => SplitByUpperCase(SplitByNewLineAndSpace(value, options), options),
                _ => throw new NotSupportedException()
            };
        }

        public static String[] SplitBy(this IString value, SplitType split = SplitType.NewLine, StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return SplitBy(value.ToString(), split, options);
        }

        public static String Concat(this IEnumerable<Char> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (source is String value)
            {
                return value;
            }

            return source.ToStringBuilder().ToString();
        }

        public static String Join<T>(this String? separator, IEnumerable<T?>? values)
        {
            return values is not null ? String.Join(separator, values) : String.Empty;
        }

        public static String Join(this String? separator, IEnumerable<String?>? values)
        {
            return values is not null ? String.Join(separator, values) : String.Empty;
        }

        public static String Join(this String? separator, IEnumerable<String?>? values, JoinType type)
        {
            if (values is null)
            {
                return String.Empty;
            }

            return type switch
            {
                JoinType.Default => Join(separator, values),
                JoinType.NotEmpty => Join(separator, IsNotNullOrEmpty, values),
                JoinType.NotWhiteSpace => Join(separator, IsNotNullOrWhiteSpace, values),
                _ => throw new NotSupportedException()
            };
        }

        public static String Join<T>(this String? separator, params T?[]? values)
        {
            return values is not null ? String.Join(separator, values) : String.Empty;
        }

        public static String Join(this String? separator, params String?[]? values)
        {
            return values is not null ? String.Join(separator, values) : String.Empty;
        }

        public static String Join(this String? separator, JoinType type, params String?[]? values)
        {
            return Join(separator, values, type);
        }

        public static String Join(this String? separator, Func<String?, Boolean> predicate, IEnumerable<String?>? values)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            
            return values is not null ? String.Join(separator, values.Where(predicate)) : String.Empty;
        }

        public static String Join(this String? separator, Func<String?, Boolean> predicate, params String?[]? values)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            
            return values is not null && values.Length > 0 ? String.Join(separator, values.Where(predicate)) : String.Empty;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String Join<T>(this IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return Join(String.Empty, source);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String Join<T>(this IEnumerable<T> source, String? separator)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return Join(separator, source);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String Join<T>(this IEnumerable<T> source, String? separator, String? end)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return $"{Join(separator, source)}{end}";
        }

        /// <summary>
        /// Concatenates all elements of a sequence using the specified separator between each element.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <paramref name="source"/>.</typeparam>
        /// <param name="source">A sequence that contains the objects to concatenate.</param>
        /// <param name="separator">The string to use as a separator.</param>
        /// <param name="start">Start with</param>
        /// <param name="end">End with</param>
        /// <returns>A string holding the concatenated values.</returns>
        public static String Join<T>(this IEnumerable<T> source, String? separator, String? start, String? end)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return $"{start}{Join(separator, source)}{end}";
        }
        
        public static String JoinNewLine<T>(this IEnumerable<T> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return Join(source, NewLine);
        }
        
        public static String JoinNewLine<T>(this IEnumerable<T> source, String? end)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return Join(source, NewLine, end);
        }
        
        public static String JoinNewLine<T>(this IEnumerable<T> source, String? start, String? end)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return Join(source, NewLine, start, end);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String Join<T>(this IEnumerable<T> source, JoinType type)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return Join(String.Empty, source.ToStringEnumerable(), type);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String Join<T>(this IEnumerable<T> source, String? separator, JoinType type)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return Join(separator, source.ToStringEnumerable(), type);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String Join<T>(this IEnumerable<T> source, String? separator, JoinType type, String? end)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return $"{Join(source, separator, type)}{end}";
        }

        public static String Join<T>(this IEnumerable<T> source, String? separator, JoinType type, String? start, String? end)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return $"{start}{Join(source, separator, type)}{end}";
        }
        
        public static String JoinNewLine<T>(this IEnumerable<T> source, JoinType type)
        {
            return Join(source, NewLine, type);
        }
        
        public static String JoinNewLine<T>(this IEnumerable<T> source, JoinType type, String? end)
        {
            return Join(source, NewLine, type, end);
        }
        
        public static String JoinNewLine<T>(this IEnumerable<T> source, JoinType type, String? start, String? end)
        {
            return Join(source, NewLine, type, start, end);
        }
        
        public static String Join<T, TOutput>(this IEnumerable<T> source, Func<T, TOutput> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return Join(source.Select(selector));
        }
        
        public static String Join<T, TOutput>(this IEnumerable<T> source, Func<T, TOutput> selector, String? separator)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return Join(source.Select(selector), separator);
        }
        
        public static String Join<T, TOutput>(this IEnumerable<T> source, Func<T, TOutput> selector, String? separator, String? end)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return Join(source.Select(selector), separator, end);
        }

        public static String Join<T, TOutput>(this IEnumerable<T> source, Func<T, TOutput> selector, String? separator, String? start, String? end)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return Join(source.Select(selector), separator, start, end);
        }
        
        public static String JoinNewLine<T, TOutput>(this IEnumerable<T> source, Func<T, TOutput> selector)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return Join(source.Select(selector), NewLine);
        }
        
        public static String JoinNewLine<T, TOutput>(this IEnumerable<T> source, Func<T, TOutput> selector, String? start)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return Join(source.Select(selector), NewLine, start);
        }
        
        public static String JoinNewLine<T, TOutput>(this IEnumerable<T> source, Func<T, TOutput> selector, String? start, String? end)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return Join(source.Select(selector), NewLine, start, end);
        }
        
        public static String Join<T, TOutput>(this IEnumerable<T> source, Func<T, TOutput> selector, JoinType type)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return Join(source.Select(selector), type);
        }
        
        public static String Join<T, TOutput>(this IEnumerable<T> source, Func<T, TOutput> selector, String? separator, JoinType type)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return Join(source.Select(selector), separator, type);
        }
        
        public static String Join<T, TOutput>(this IEnumerable<T> source, Func<T, TOutput> selector, String? separator, JoinType type, String? end)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return Join(source.Select(selector), separator, type, end);
        }

        public static String Join<T, TOutput>(this IEnumerable<T> source, Func<T, TOutput> selector, String? separator, JoinType type, String? start, String? end)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return Join(source.Select(selector), separator, type, start, end);
        }
        
        public static String JoinNewLine<T, TOutput>(this IEnumerable<T> source, Func<T, TOutput> selector, JoinType type)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return JoinNewLine(source.Select(selector), type);
        }
        
        public static String JoinNewLine<T, TOutput>(this IEnumerable<T> source, Func<T, TOutput> selector, JoinType type, String? start)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return JoinNewLine(source.Select(selector), type, start);
        }
        
        public static String JoinNewLine<T, TOutput>(this IEnumerable<T> source, Func<T, TOutput> selector, JoinType type, String? start, String? end)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            return JoinNewLine(source.Select(selector), type, start, end);
        }

        public static String ReplaceInsert(this String value, ReadOnlySpan<Char> replace)
        {
            return ReplaceInsert(value, replace, 0);
        }

        public static String ReplaceInsert(this String value, ReadOnlySpan<Char> replace, Int32 index)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (replace.IsEmpty)
            {
                return value;
            }

            return value.Length > 0 ? new StringBuilder(value).ReplaceInsert(replace, index).ToString() : replace.ToString();
        }

        public static String ReplaceInsert(this String value, String? replace)
        {
            return ReplaceInsert(value, replace, 0);
        }

        public static String ReplaceInsert(this String value, String? replace, Int32 index)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (replace is null)
            {
                return value;
            }

            if (replace.Length <= 0)
            {
                return value;
            }

            return value.Length > 0 ? new StringBuilder(value).ReplaceInsert(replace, index).ToString() : replace;
        }

        public static IEnumerable<String> WhereNotNullOrEmpty(this IEnumerable<String?> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.WhereNot(String.IsNullOrEmpty)!;
        }

        public static IEnumerable<IString> WhereNotNullOrEmpty(this IEnumerable<IString?> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.WhereNot(IsNullOrEmpty)!;
        }

        public static IEnumerable<String> WhereNotNullOrWhiteSpace(this IEnumerable<String?> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.WhereNot(String.IsNullOrWhiteSpace)!;
        }

        public static IEnumerable<IString> WhereNotNullOrWhiteSpace(this IEnumerable<IString?> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.WhereNot(IsNullOrWhiteSpace)!;
        }

        public static String ToCapitalizeFirstChar(this String value)
        {
            return ToCapitalizeFirstChar(value, null);
        }

        public static String ToCapitalizeFirstChar(this String value, CultureInfo? info)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return value.Length > 0 ? value[0].ToString(info).ToUpper(info) + value.Substring(1) : value;
        }

        public static String ToCapitalizeFirstCharLower(this String value)
        {
            return ToCapitalizeFirstCharLower(value, null);
        }

        public static String ToCapitalizeFirstCharLower(this String value, CultureInfo? info)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return value.Length > 0 ? ToCapitalizeFirstChar(value.ToLower(info), info) : value;
        }

        public static String ToTitleCase(this String value)
        {
            return ToTitleCase(value, null);
        }

        public static String ToTitleCase(this String value, CultureInfo? info)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (value.Length <= 0)
            {
                return value;
            }

            info ??= CultureInfo.CurrentCulture;
            return info.TextInfo.ToTitleCase(value);
        }

        public static String ToTitleCaseLower(this String value)
        {
            return ToTitleCaseLower(value, null);
        }

        public static String ToTitleCaseLower(this String value, CultureInfo? info)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return ToTitleCase(value.ToLower(info), info);
        }

        public static String Repeat(this String value, Int32 count)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return count <= 1 ? value : new StringBuilder(value.Length * count).Insert(0, value, count).ToString();
        }

        public static String Shuffle(this String value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return value.Length > 1 ? ((IEnumerable<Char>) value).Shuffle().Join() : value;
        }

        public static String Shuffle(this String value, Int32 index)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (index < 0 || index >= value.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            return value.Length > 1 ? value.Substring(0, index) + value.Substring(index).Shuffle() : value;
        }

        public static String Shuffle(this String value, Int32 index, Int32 length)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (length < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(length));
            }

            if (index < 0 || index + length > value.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            if (length <= 1)
            {
                return value;
            }

            return value.Length > 1 ? value.Substring(0, index) + value.Substring(index, length).Shuffle() + value.Substring(index + length) : value;
        }

        public static Boolean IsMatrix(this IEnumerable<String> value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            using IEnumerator<String> enumerator = value.GetEnumerator();

            if (!enumerator.MoveNext())
            {
                return false;
            }

            Int32? first = enumerator.Current?.RemoveAnsi().Length;

            while (enumerator.MoveNext())
            {
                if (!EqualityComparer<Int32?>.Default.Equals(first, enumerator.Current?.RemoveAnsi().Length))
                {
                    return false;
                }
            }

            return true;
        }

        public static Size GetMatrixSize(this IEnumerable<String> value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            using IEnumerator<String> enumerator = value.GetEnumerator();

            if (!enumerator.MoveNext())
            {
                throw new ArgumentException(@"Value cannot be empty.", nameof(value));
            }

            Int32 first = enumerator.Current?.RemoveAnsi().Length ?? 0;

            Int32 counter;
            for (counter = 1; enumerator.MoveNext(); counter++)
            {
                if (!EqualityComparer<Int32>.Default.Equals(first, enumerator.Current?.RemoveAnsi().Length ?? 0))
                {
                    throw new InvalidOperationException(@"Different values length.");
                }
            }

            return new Size(first, counter);
        }

        private static Regex AnsiRegex { get; } = new Regex(@"\x1B(?:[@-Z\\-_]|\[[0-?]*[ -/]*[@-~])", RegexOptions.Compiled);

        public static Int32 CountAnsi(this String value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return AnsiRegex.Matches(value).Count;
        }

        public static String RemoveAnsi(this String value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return AnsiRegex.Replace(value, String.Empty);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String Comment(this String? value)
        {
            return $"\"{value ?? String.Empty}\"";
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String CommentSingle(String? value)
        {
            return $"\'{value ?? String.Empty}\'";
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String Comment(this String? value, Boolean single)
        {
            return single ? CommentSingle(value) : Comment(value);
        }

        public static String AddPrefix(this String value, String? prefix)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (String.IsNullOrEmpty(prefix))
            {
                return value;
            }

            return prefix + value;
        }

        public static String AddSuffix(this String value, String? suffix)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return String.IsNullOrEmpty(suffix) ? value : value + suffix;
        }

        public static String AddPrefixAndSuffix(this String value, String? additional)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (String.IsNullOrEmpty(additional))
            {
                return value;
            }

            return additional + value + additional;
        }

        public static String RemovePrefix(this String value, String prefix)
        {
            return RemovePrefix(value, prefix, StringComparison.Ordinal);
        }

        public static String RemovePrefix(this String value, String? prefix, StringComparison comparison)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (String.IsNullOrEmpty(prefix))
            {
                return value;
            }

            if (prefix.Length > value.Length || !value.StartsWith(prefix, comparison))
            {
                return value;
            }

            return value.Substring(prefix.Length);
        }

        public static String RemoveSuffix(this String value, String suffix)
        {
            return RemoveSuffix(value, suffix, StringComparison.Ordinal);
        }

        public static String RemoveSuffix(this String value, String? suffix, StringComparison comparison)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (String.IsNullOrEmpty(suffix))
            {
                return value;
            }

            if (value.Length < suffix.Length || !value.EndsWith(suffix, comparison))
            {
                return value;
            }

            return value.Substring(0, value.Length - suffix.Length);
        }

        public static String RemovePrefixAndSuffix(this String value, String trim)
        {
            return RemovePrefixAndSuffix(value, trim, StringComparison.Ordinal);
        }

        public static String RemovePrefixAndSuffix(this String value, String? trim, StringComparison comparison)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (String.IsNullOrEmpty(trim))
            {
                return value;
            }

            if (value.Length < trim.Length)
            {
                return value;
            }

            return value.StartsWith(trim, comparison) switch
            {
                true when value.EndsWith(trim, comparison) => value.Length > 2 * trim.Length ? value.Substring(trim.Length, value.Length - 2 * trim.Length) : String.Empty,
                true => value.Substring(trim.Length),
                false when value.EndsWith(trim, comparison) => value.Substring(0, value.Length - trim.Length),
                _ => value
            };
        }

        private static String ToOppositeCaseInternal(this String value, Func<Char, Char> upper, Func<Char, Char> lower)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (upper is null)
            {
                throw new ArgumentNullException(nameof(upper));
            }

            if (lower is null)
            {
                throw new ArgumentNullException(nameof(lower));
            }
            
            if (value.Length <= 0)
            {
                return value;
            }

            StringBuilder builder = new StringBuilder(value.Length);

            foreach (Char chr in value)
            {
                if (!Char.IsLetter(chr))
                {
                    builder.Append(chr);
                    continue;
                }

                if (Char.IsUpper(chr))
                {
                    builder.Append(lower(chr));
                    continue;
                }

                builder.Append(upper(chr));
            }

            return builder.ToString();
        }

        public static String ToOppositeCase(this String value)
        {
            return ToOppositeCase(value, null);
        }

        public static String ToOppositeCase(this String value, CultureInfo? info)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (value.Length <= 0)
            {
                return value;
            }

            return info is null ? ToOppositeCaseInternal(value, Char.ToUpper, Char.ToLower) :
                ToOppositeCaseInternal(value, chr => Char.ToUpper(chr, info), chr => Char.ToLower(chr, info));
        }

        public static String ToOppositeCaseInvariant(this String value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return value.Length <= 0 ? value : ToOppositeCaseInternal(value, Char.ToUpperInvariant, Char.ToLowerInvariant);
        }

        /// <summary>
        /// Returns the beginning portion of s up to, but not including,
        /// the first occurrence of the character c. If c is not present in
        /// s, then s is returned.
        /// </summary>
        public static String UpTo(this String value, Char character)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            Int32 index = value.IndexOf(character);
            return index <= -1 ? value : value.Substring(0, index);
        }

        /// <summary>
        /// Returns the first string parameter that is not null, has a length greater
        /// than zero, and does not consist only of whitespace.
        /// </summary>
        public static String? Coalesce(params String?[] values)
        {
            if (values is null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            return values.FirstOrDefault(str => !String.IsNullOrWhiteSpace(str));
        }

        /// <summary>
        /// Indents this text by the specified space count.
        /// </summary>
        /// <param name="value">The text to indent.</param>
        /// <param name="count">The number of spaces to indent.</param>
        public static String Indent(this String value, Int32 count)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            String indentation = new String(' ', count);
            String replacement = Environment.NewLine + indentation;
            
            return indentation + value.Replace(Environment.NewLine, replacement);
        }

        /// <inheritdoc cref="OccurrencesOf(String,Char,Boolean)"/>
        public static Int32 OccurrencesOf(this String value, Char character)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return value.Count(chr => chr == character);
        }
        
        /// <inheritdoc cref="OccurrencesOf(String,Char,Boolean)"/>
        public static Int32 OccurrencesInsensitiveOf(this String value, Char character)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            character = Char.ToLowerInvariant(character);
            value = value.ToLowerInvariant();
            
            return value.Count(chr => chr == character);
        }

        /// <summary>
        /// Returns the number of occurrences of the specified character in this string.
        /// </summary>
        /// <param name="value">The text.</param>
        /// <param name="character">The character to count occurrences for.</param>
        /// <param name="insensitive">Determines whether or not to compare in case sensitive or case insensitive way.</param>
        public static Int32 OccurrencesOf(this String value, Char character, Boolean insensitive)
        {
            return insensitive ? OccurrencesInsensitiveOf(value, character) : OccurrencesOf(value, character);
        }

        /// <summary>
        /// Returns a new string in which all occurences of the specified value are removed.
        /// </summary>
        /// <param name="value">The text.</param>
        /// <param name="occurence">The string to seek and remove.</param>
        public static String RemoveAllOf(this String value, String occurence)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (occurence is null)
            {
                throw new ArgumentNullException(nameof(occurence));
            }

            return value.Replace(occurence, String.Empty);
        }

        /// <summary>
        /// Returns a new string in which the first occurence of the specified value is removed.
        /// </summary>
        /// <param name="value">The text.</param>
        /// <param name="occurence">The string to seek and remove its first occurence.</param>
        public static String RemoveFirstOf(this String value, String occurence)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (occurence is null)
            {
                throw new ArgumentNullException(nameof(occurence));
            }

            Int32 index = value.IndexOf(occurence, StringComparison.Ordinal);
            return index < 0 ? value : value.Remove(index, occurence.Length);
        }

        public static unsafe String RemoveAllWhiteSpace(this String value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (value.Length <= 0)
            {
                return String.Empty;
            }

            fixed (Char* pointer = value)
            {
                Char* destination = pointer;
                for (Char* chr = pointer; *chr != 0; chr++)
                {
                    switch (*chr)
                    {
                        case '\u0020':
                        case '\u00A0':
                        case '\u1680':
                        case '\u2000':
                        case '\u2001':
                        case '\u2002':
                        case '\u2003':
                        case '\u2004':
                        case '\u2005':
                        case '\u2006':
                        case '\u2007':
                        case '\u2008':
                        case '\u2009':
                        case '\u200A':
                        case '\u202F':
                        case '\u205F':
                        case '\u3000':
                        case '\u2028':
                        case '\u2029':
                        case '\u0009':
                        case '\u000A':
                        case '\u000B':
                        case '\u000C':
                        case '\u000D':
                        case '\u0085':
                            continue;
                        default:
                            *destination++ = *chr;
                            break;
                    }
                }

                return new String(pointer, 0, (Int32) (destination - pointer));
            }
        }

        /// <summary>
        /// Returns a new string from the provided text with the specified maximum length. If the original text is longer than the specified maximum length, 3 dots (...) are added.
        /// </summary>
        /// <param name="value">The text.</param>
        /// <param name="length">The maximum length of the returned string (excluding the 3 dots).</param>
        public static String ShortenWith3Dots(this String value, Int32 length)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (length < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(length), @"The maximum length must not be less than 1.");
            }

            if (value.Length <= length)
            {
                return value;
            }

            return value.Substring(0, length) + "...";
        }

        /// <summary>
        /// Returns a new string from the provided text with the specified maximum length. If the original text is longer than the specified maximum length, 3 dots (...) are added.
        /// </summary>
        /// <param name="value">The text.</param>
        /// <param name="length">The maximum length of the returned string (including the 3 dots).</param>
        public static String ShortenWith3DotsIncluded(this String value, Int32 length)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (length < 4)
            {
                throw new ArgumentOutOfRangeException(nameof(length), @"The maximum length must not be less than 4.");
            }

            if (value.Length <= length - 3)
            {
                return value;
            }

            return value.Substring(0, length - 3) + "...";
        }
        
        /// <summary>
        /// Returns a new string from the provided text with the specified maximum length. If the original text is longer than the specified maximum length, 3 dots (...) are added.
        /// </summary>
        /// <param name="value">The text.</param>
        /// <param name="length">The maximum length of the returned string.</param>
        /// <param name="included">Included or excluded 3 dots</param>
        public static String ShortenWith3Dots(this String value, Int32 length, Boolean included)
        {
            return included ? ShortenWith3DotsIncluded(value, length) : ShortenWith3Dots(value, length);
        }

        public static Int32 LevenshteinDistance(String first, String second)
        {
            if (first is null)
            {
                throw new ArgumentNullException(nameof(first));
            }

            if (second is null)
            {
                throw new ArgumentNullException(nameof(second));
            }

            Int32[,] values = new Int32[first.Length + 1, second.Length + 1];

            for (Int32 i = 0; i <= first.Length; i++)
            {
                values[i, 0] = i;
            }

            for (Int32 j = 0; j <= second.Length; j++)
            {
                values[0, j] = j;
            }

            for (Int32 i = 1; i <= first.Length; i++)
            {
                for (Int32 j = 1; j <= second.Length; j++)
                {
                    Int32 difference = first[i - 1] == second[j - 1] ? 0 : 1;

                    values[i, j] = Math.Min(Math.Min(values[i - 1, j] + 1, values[i, j - 1] + 1), values[i - 1, j - 1] + difference);
                }
            }

            return values[first.Length, second.Length];
        }
        
        public static Int32 LevenshteinDistance(this String first, String second, StringComparison comparison)
        {
            if (first is null)
            {
                throw new ArgumentNullException(nameof(first));
            }

            if (second is null)
            {
                throw new ArgumentNullException(nameof(second));
            }

            return comparison switch
            {
                StringComparison.CurrentCulture => LevenshteinDistance(first, second),
                StringComparison.CurrentCultureIgnoreCase => LevenshteinDistance(first.ToUpper(), second.ToUpper()),
                StringComparison.InvariantCulture => LevenshteinDistance(first, second),
                StringComparison.InvariantCultureIgnoreCase => LevenshteinDistance(first.ToUpperInvariant(), second.ToUpperInvariant()),
                StringComparison.Ordinal => LevenshteinDistance(first, second),
                StringComparison.OrdinalIgnoreCase => LevenshteinDistance(first.ToUpperInvariant(), second.ToUpperInvariant()),
                _ => throw new NotSupportedException()
            };
        }

        public static Int32 DamerauLevenshteinDistance(this String first, String second)
        {
            return DamerauLevenshteinDistance(first, second, new Int32[first.Length + 1, second.Length + 1]);
        }

        public static Int32 DamerauLevenshteinDistance(this String first, String second, StringComparison comparison)
        {
            return DamerauLevenshteinDistance(first, second, comparison, new Int32[first.Length + 1, second.Length + 1]);
        }

        // ReSharper disable once CognitiveComplexity
        public static Int32 DamerauLevenshteinDistance(this String first, String second, Int32[,] values)
        {
            if (first is null)
            {
                throw new ArgumentNullException(nameof(first));
            }

            if (second is null)
            {
                throw new ArgumentNullException(nameof(second));
            }

            Int32 flength = first.Length;
            Int32 slength = second.Length;

            for (Int32 i = 0; i <= flength; ++i)
            {
                values[i, 0] = i;
            }

            for (Int32 j = 1; j <= slength; ++j)
            {
                values[0, j] = j;
            }

            for (Int32 i = 1; i <= flength; ++i)
            {
                for (Int32 j = 1; j <= slength; ++j)
                {
                    Char fchar = first[i - 1];
                    Char schar = second[j - 1];
                    Int32 cost = fchar == schar ? 0 : 1;

                    Int32 deletion = values[i - 1, j] + 1;
                    Int32 insertion = values[i, j - 1] + 1;
                    Int32 substitution = values[i - 1, j - 1] + cost;

                    values[i, j] = Math.Min(Math.Min(deletion, insertion), substitution);

                    if (i <= 1 || j <= 1 || fchar != second[j - 2] || first[i - 2] != schar)
                    {
                        continue;
                    }

                    Int32 transposition = values[i - 2, j - 2] + cost;
                    values[i, j] = Math.Min(values[i, j], transposition);
                }
            }

            return values[flength, slength];
        }

        public static Int32 DamerauLevenshteinDistance(this String first, String second, StringComparison comparison, Int32[,] values)
        {
            return comparison switch
            {
                StringComparison.CurrentCulture => DamerauLevenshteinDistance(first, second, values),
                StringComparison.CurrentCultureIgnoreCase => DamerauLevenshteinDistance(first.ToUpper(), second.ToUpper(), values),
                StringComparison.InvariantCulture => DamerauLevenshteinDistance(first, second, values),
                StringComparison.InvariantCultureIgnoreCase => DamerauLevenshteinDistance(first.ToUpperInvariant(), second.ToUpperInvariant(), values),
                StringComparison.Ordinal => DamerauLevenshteinDistance(first, second, values),
                StringComparison.OrdinalIgnoreCase => DamerauLevenshteinDistance(first.ToUpperInvariant(), second.ToUpperInvariant(), values),
                _ => throw new NotSupportedException()
            };
        }

        public static String WhereChar(this String value, Func<Char, Boolean> where)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            return value.Length > 0 ? String.Join(String.Empty, value.Where(where)) : String.Empty;
        }

        public static String WhereNotChar(this String value, Func<Char, Boolean> where)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (where is null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            return value.Length > 0 ? String.Join(String.Empty, value.WhereNot(where)) : String.Empty;
        }

        public static String RemoveNonDigitChars(this String value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return String.Join(String.Empty, value.Where(Char.IsDigit));
        }

        public static String RemoveDiacritics(this String value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (value.Length <= 0)
            {
                return value;
            }

            String normalize = value.Normalize(NormalizationForm.FormD);
            StringBuilder builder = new StringBuilder(normalize.Length);

            foreach (Char character in normalize.Where(character => CharUnicodeInfo.GetUnicodeCategory(character) != UnicodeCategory.NonSpacingMark))
            {
                builder.Append(character);
            }

            return builder.ToString().Normalize(NormalizationForm.FormC);
        }

        public static CultureInfo? ToCultureInfo(this StringComparison comparison)
        {
            switch (comparison)
            {
                case StringComparison.CurrentCulture:
                case StringComparison.CurrentCultureIgnoreCase:
                    return CultureInfo.CurrentCulture;
                case StringComparison.InvariantCulture:
                case StringComparison.InvariantCultureIgnoreCase:
                    return CultureInfo.InvariantCulture;
                case StringComparison.Ordinal:
                case StringComparison.OrdinalIgnoreCase:
                    return null;
                default:
                    throw new NotSupportedException();
            }
        }

        public static Boolean IsIgnoreCase(this StringComparison comparison)
        {
            switch (comparison)
            {
                case StringComparison.CurrentCulture:
                case StringComparison.InvariantCulture:
                case StringComparison.Ordinal:
                    return false;
                case StringComparison.CurrentCultureIgnoreCase:
                case StringComparison.InvariantCultureIgnoreCase:
                case StringComparison.OrdinalIgnoreCase:
                    return true;
                default:
                    throw new NotSupportedException();
            }
        }

        public static StringComparison ToIgnoreCase(this StringComparison comparison)
        {
            switch (comparison)
            {
                case StringComparison.CurrentCulture:
                case StringComparison.CurrentCultureIgnoreCase:
                    return StringComparison.CurrentCultureIgnoreCase;
                case StringComparison.InvariantCulture:
                case StringComparison.InvariantCultureIgnoreCase:
                    return StringComparison.InvariantCultureIgnoreCase;
                case StringComparison.Ordinal:
                case StringComparison.OrdinalIgnoreCase:
                    return StringComparison.OrdinalIgnoreCase;
                default:
                    throw new NotSupportedException();
            }
        }

        public static StringComparison ToCaseSensitive(this StringComparison comparison)
        {
            switch (comparison)
            {
                case StringComparison.CurrentCulture:
                case StringComparison.CurrentCultureIgnoreCase:
                    return StringComparison.CurrentCulture;
                case StringComparison.InvariantCulture:
                case StringComparison.InvariantCultureIgnoreCase:
                    return StringComparison.InvariantCulture;
                case StringComparison.Ordinal:
                case StringComparison.OrdinalIgnoreCase:
                    return StringComparison.Ordinal;
                default:
                    throw new NotSupportedException();
            }
        }

        /// <summary>
        /// Converts the specified string to a stream by using UTF-8 encoding that can be read from.
        /// <para>Don't forget to dispose the stream.</para>
        /// </summary>
        /// <param name="value">A string value that the stream will read from.</param>
        public static MemoryStream ToStream(this String value)
        {
            return ToStream(value, Encoding.UTF8);
        }

        /// <summary>
        /// Convert value to a MemoryStream, using the given encoding.
        /// </summary>
        public static MemoryStream ToStream(this String value, Encoding? encoding)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            MemoryStream stream = new MemoryStream();
            using StreamWriter writer = new StreamWriter(stream, encoding ?? Encoding.UTF8);
            writer.Write(value);
            writer.Flush();
            stream.Position = 0;

            return stream;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String MutateString(this String value, Char character)
        {
            return MutateString(value, character, 0);
        }

        public static unsafe String MutateString(this String value, Char character, Int32 position)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (position < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(position));
            }

            if (position > value.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(position));
            }
            
            fixed (Char* pinned = value)
            {
                pinned[position] = character;
            }

            return value;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String MutateString(this String value, String mutate)
        {
            return MutateString(value, mutate, 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String MutateString(this String value, String mutate, Int32 position)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (mutate is null)
            {
                throw new ArgumentNullException(nameof(mutate));
            }

            return MutateString(value, mutate.AsSpan(), position);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String MutateString(this String value, ReadOnlySpan<Char> span)
        {
            return MutateString(value, span, 0);
        }

        public static unsafe String MutateString(this String value, ReadOnlySpan<Char> span, Int32 position)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (position < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(position));
            }

            if (position + span.Length > value.Length)
            {
                throw new ArgumentOutOfRangeException(span.Length > value.Length ? nameof(span) : nameof(position));
            }
            
            fixed (Char* pinned = value)
            {
                for (Int32 i = 0; i < span.Length; i++)
                {
                    pinned[position + i] = span[i];
                }
            }

            return value;
        }

        /// <summary>
        /// Converts input characters into a read-only secure string.
        /// </summary>
        /// <param name="source">The characters to convert.</param>
        [SecurityCritical]
        public static SecureString ToSecureString(this IEnumerable<Char> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            SecureString secure = new SecureString();

            foreach (Char chr in source)
            {
                secure.AppendChar(chr);
            }

            secure.MakeReadOnly();

            return secure;
        }

        /// <summary>
        /// Converts a secure string to string.
        /// </summary>
        /// <param name="value">The secure string to convert.</param>
        [SecurityCritical]
        public static String ToInsecureString(this SecureString value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            IntPtr ptr = Marshal.SecureStringToBSTR(value);

            try
            {
                return Marshal.PtrToStringBSTR(ptr);
            }
            finally
            {
                Marshal.ZeroFreeBSTR(ptr);
            }
        }

        public static IString IStringNull { get; } = new StringAdapter(null);
        public static IString IStringEmpty { get; } = new StringAdapter(String.Empty);

        public static IString ToIString(this String? value)
        {
            if (value is null)
            {
                return IStringNull;
            }

            return value.Length > 0 ? new StringAdapter(value) : IStringEmpty;
        }
    }
}