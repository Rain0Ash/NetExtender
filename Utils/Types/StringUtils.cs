// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
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
using JetBrains.Annotations;
using NetExtender.Types.Strings;
using NetExtender.Types.Strings.Interfaces;

namespace NetExtender.Utils.Types
{
    public enum SplitType
    {
        Chars,
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

    public static class StringUtils
    {
        /// <summary>
        /// Cached null string task
        /// </summary>
        public static Task<String> Null { get; } = Task.FromResult<String>(null);
        /// <summary>
        /// Cached empty string task
        /// </summary>
        public static Task<String> Empty { get; } = Task.FromResult(String.Empty);

        public const String NullString = "null";

        public const String DefaultSeparator = " ";

        public const String FormatVariableRegexPattern = @"\{([^\{\}]+)\}";

        public static Int32 CharLength([NotNull] this String[] values)
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

        public static Int32 CharLength([NotNull] this IEnumerable<String> values)
        {
            if (values is null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            return values.WhereNotNull().Sum(str => str.Length);
        }
        
        public static Int32 CharLength([NotNull] this IString[] values)
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

        public static Int32 CharLength([NotNull] this IEnumerable<IString> values)
        {
            if (values is null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            return values.WhereNotNull().Sum(str => str.Length);
        }

        public static Int64 CharLongLength([NotNull] this String[] values)
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
        
        public static Int64 CharLongLength([NotNull] this IEnumerable<String> values)
        {
            if (values is null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            return values.WhereNotNull().Aggregate<String, Int64>(0, (current, str) => current + str.Length);
        }
        
        public static Int64 CharLongLength([NotNull] this IString[] values)
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
        
        public static Int64 CharLongLength([NotNull] this IEnumerable<IString> values)
        {
            if (values is null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            return values.WhereNotNull().Aggregate<IString, Int64>(0, (current, str) => current + str.Length);
        }

        public static IEnumerable<Int32> AllIndexesOf(this String str, String pattern, StringComparison comparison = StringComparison.Ordinal)
        {
            Int32 index = str.IndexOf(pattern, comparison);
            while (index != -1)
            {
                yield return index;
                index = str.IndexOf(pattern, index + pattern.Length, comparison);
            }
        }

        public static IEnumerable<String> GetFormatVariables(String str)
        {
            return Regex.Matches(str, FormatVariableRegexPattern, RegexOptions.Compiled)
                .Select(match => match.Value)
                .Select(format => Regex.Replace(format.ToLower(), @"\{|\}", String.Empty));
        }

        private static readonly IImmutableDictionary<Char, Char> BracketPairs = new Dictionary<Char, Char>
        {
            {'(', ')'},
            {'{', '}'},
            {'[', ']'},
            {'<', '>'}
        }.ToImmutableDictionary();

        public static Boolean IsBracketsWellFormed([NotNull] String value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (value.IsEmpty())
            {
                return true;
            }

            Stack<Char> brackets = new Stack<Char>();

            try
            {
                foreach (Char chr in value)
                {
                    if (BracketPairs.Keys.Contains(chr))
                    {
                        brackets.Push(chr);
                        continue;
                    }

                    if (!BracketPairs.Values.Contains(chr))
                    {
                        continue;
                    }

                    if (chr != BracketPairs[brackets.Peek()])
                    {
                        return false;
                    }

                    brackets.Pop();
                }
            }
            catch (Exception)
            {
                return false;
            }

            // Ensure all brackets are closed
            return !brackets.Any();
        }

        public static String ReplaceFromDictionary(this String source, IDictionary<String, Object> replaceDict)
        {
            return ReplaceFromDictionary(source, replaceDict.ToDictionary(key => key.Key, value => value.Value?.ToString()));
        }

        public static String ReplaceFromDictionary(this String source, IDictionary<String, String> replaceDict)
        {
            return Regex.Replace(source, $@"\b({String.Join("|", replaceDict.Keys)})\b",
                m => replaceDict[m.Value]?.ToString() ?? String.Empty);
        }

        public static String FormatFromDictionary(this String source, IDictionary<String, Object> valueDict)
        {
            Int32 i = 0;
            StringBuilder newFormatString = new StringBuilder(source);
            Dictionary<String, Int32> keyToInt = new Dictionary<String, Int32>();
            foreach ((String key, Object _) in valueDict)
            {
                newFormatString = newFormatString.Replace("{" + key + "}", "{" + i + "}");
                keyToInt.Add(key, i);
                i++;
            }

            return String.Format(newFormatString.ToString(), valueDict.OrderBy(x => keyToInt[x.Key]).Select(x => x.Value).ToArray());
        }

        public static Int32 FormatArgsExpected([NotNull] this String str)
        {
            if (str is null)
            {
                throw new ArgumentNullException(nameof(str));
            }

            if (str.Length <= 0)
            {
                return 0;
            }

            const String pattern = @"(?<!\{)(?>\{\{)*\{\d(.*?)";

            MatchCollection matches = Regex.Matches(str, pattern, RegexOptions.Compiled);
            return matches.Select(m => m.Value).Distinct().Count();
        }

        /// <summary>
        /// Trim string after format variables
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static String TrimAfterFormatVariables(String str)
        {
            Match match = Regex.Match(str, FormatVariableRegexPattern, RegexOptions.Compiled);
            return match.Success ? str.Substring(0, match.Index) : null;
        }

        public static String Format([NotNull] this String source, [CanBeNull] params Object[] args)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (args is null)
            {
                return source;
            }

            Int32 expected = FormatArgsExpected(source);

            if (expected <= 0)
            {
                return source;
            }

            args = args.Length < expected
                ? args.Concat(Enumerable.Repeat(NullString, expected - args.Length)).ToArray()
                : args.Take(expected).ToArray();

            return String.Format(source, args);
        }

        public static String Format([NotNull] this IString source, [NotNull] params Object[] args)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return Format(source.ToString(), args);
        }

        public static Boolean EndsWith([NotNull] this String str, [NotNull] IEnumerable<Char> chars)
        {
            if (str is null)
            {
                throw new ArgumentNullException(nameof(str));
            }

            if (chars is null)
            {
                throw new ArgumentNullException(nameof(chars));
            }

            return chars.Any(str.EndsWith);
        }

        public static Boolean EndsWith([NotNull] this IString str, [NotNull] IEnumerable<Char> chars)
        {
            if (str is null)
            {
                throw new ArgumentNullException(nameof(str));
            }

            return EndsWith(str.ToString(), chars);
        }

        public static Boolean EndsWith([NotNull] this String str, [NotNull] IEnumerable<String> substrings)
        {
            if (str is null)
            {
                throw new ArgumentNullException(nameof(str));
            }

            if (substrings is null)
            {
                throw new ArgumentNullException(nameof(substrings));
            }

            return substrings.Any(str.EndsWith);
        }

        public static Boolean EndsWith([NotNull] this IString str, [NotNull] IEnumerable<String> substrings)
        {
            if (str is null)
            {
                throw new ArgumentNullException(nameof(str));
            }

            return EndsWith(str.ToString(), substrings);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNull([CanBeNull] this String str)
        {
            return str is null;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNull([CanBeNull] this IString str)
        {
            return str?.ToString() is null;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotNull([CanBeNull] this String str)
        {
            return str is not null;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotNull([CanBeNull] this IString str)
        {
            return str?.ToString() is not null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsEmpty([CanBeNull] this String str)
        {
            return str == String.Empty;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsEmpty([CanBeNull] this IString str)
        {
            return str is not null && str.Length <= 0;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotEmpty([CanBeNull] this String str)
        {
            return !IsEmpty(str);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotEmpty([CanBeNull] this IString str)
        {
            return !IsEmpty(str);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsWhiteSpace([CanBeNull] this String str)
        {
            return str is not null && str.Length > 0 && str.All(Char.IsWhiteSpace);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsWhiteSpace([CanBeNull] this IString str)
        {
            return str is not null && str.Length > 0 && str.All(Char.IsWhiteSpace);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotWhiteSpace([CanBeNull] this String str)
        {
            return !IsWhiteSpace(str);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotWhiteSpace([CanBeNull] this IString str)
        {
            return !IsWhiteSpace(str);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsEmptyOrWhiteSpace([CanBeNull] this String str)
        {
            return str is not null && (str.Length <= 0 || str.All(Char.IsWhiteSpace));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsEmptyOrWhiteSpace([CanBeNull] this IString str)
        {
            return str is not null && (str.Length <= 0 || str.All(Char.IsWhiteSpace));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNullOrEmpty([CanBeNull] this String str)
        {
            return String.IsNullOrEmpty(str);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNullOrEmpty([CanBeNull] this IString str)
        {
            return str is null || str.Length <= 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNullOrWhiteSpace([CanBeNull] this String str)
        {
            return String.IsNullOrWhiteSpace(str);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNullOrWhiteSpace([CanBeNull] this IString str)
        {
            return str is null || str.Length <= 0 || str.All(Char.IsWhiteSpace);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotNullOrEmpty([CanBeNull] this String str)
        {
            return !IsNullOrEmpty(str);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotNullOrEmpty([CanBeNull] this IString str)
        {
            return !IsNullOrEmpty(str);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotNullOrWhiteSpace([CanBeNull] this String str)
        {
            return !IsNullOrWhiteSpace(str);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsNotNullOrWhiteSpace([CanBeNull] this IString str)
        {
            return !IsNullOrWhiteSpace(str);
        }

        /// <inheritdoc cref="Char.GetUnicodeCategory(String,Int32)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UnicodeCategory GetUnicodeCategory([NotNull] this String str, Int32 index)
        {
            if (str is null)
            {
                throw new ArgumentNullException(nameof(str));
            }

            return Char.GetUnicodeCategory(str, index);
        }
        
        /// <inheritdoc cref="Char.GetUnicodeCategory(String,Int32)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UnicodeCategory GetUnicodeCategory([NotNull] this IString str, Int32 index)
        {
            return GetUnicodeCategory(str.ToString(), index);
        }
        
        public static String[] SplitByChars([NotNull] String str)
        {
            if (str is null)
            {
                throw new ArgumentNullException(nameof(str));
            }

            return str.Split();
        }

        public static String[] SplitByChars([NotNull] IString str)
        {
            if (str is null)
            {
                throw new ArgumentNullException(nameof(str));
            }

            return SplitByChars(str.ToString());
        }

        public static String[] SplitByNewLine([NotNull] String str, StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries)
        {
            return SplitByNewLine(str, Int32.MaxValue, options);
        }

        public static String[] SplitByNewLine([NotNull] String str, Int32 count, StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries)
        {
            if (str is null)
            {
                throw new ArgumentNullException(nameof(str));
            }

            return str.Split('\n', count, options);
        }
        
        public static String[] SplitByNewLine([NotNull] IString str, StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries)
        {
            if (str is null)
            {
                throw new ArgumentNullException(nameof(str));
            }

            return SplitByNewLine(str.ToString(), options);
        }

        public static String[] SplitByNewLine([NotNull] IString str, Int32 count, StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries)
        {
            if (str is null)
            {
                throw new ArgumentNullException(nameof(str));
            }

            return SplitByNewLine(str.ToString(), count, options);
        }

        public static String[] SplitBySpace([NotNull] String str, StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries)
        {
            return SplitBySpace(str, Int32.MaxValue, options);
        }

        public static String[] SplitBySpace([NotNull] String str, Int32 count, StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries)
        {
            if (str is null)
            {
                throw new ArgumentNullException(nameof(str));
            }

            return str.Split(' ', count, options);
        }
        
        public static String[] SplitBySpace([NotNull] IString str, StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries)
        {
            if (str is null)
            {
                throw new ArgumentNullException(nameof(str));
            }

            return SplitBySpace(str.ToString(), options);
        }

        public static String[] SplitBySpace([NotNull] IString str, Int32 count, StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries)
        {
            if (str is null)
            {
                throw new ArgumentNullException(nameof(str));
            }

            return SplitBySpace(str.ToString(), count, options);
        }

        private static readonly Char[] NewLineAndSpaceChars = {'\n', ' '};

        public static String[] SplitByNewLineAndSpace([NotNull] String str, StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries)
        {
            return SplitByNewLineAndSpace(str, Int32.MaxValue, options);
        }

        public static String[] SplitByNewLineAndSpace([NotNull] String str, Int32 count, StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries)
        {
            if (str is null)
            {
                throw new ArgumentNullException(nameof(str));
            }

            return str.Split(NewLineAndSpaceChars, count, options);
        }
        
        public static String[] SplitByNewLineAndSpace([NotNull] IString str, StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries)
        {
            if (str is null)
            {
                throw new ArgumentNullException(nameof(str));
            }

            return SplitByNewLineAndSpace(str.ToString(), options);
        }

        public static String[] SplitByNewLineAndSpace([NotNull] IString str, Int32 count, StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries)
        {
            if (str is null)
            {
                throw new ArgumentNullException(nameof(str));
            }

            return SplitByNewLineAndSpace(str.ToString(), count, options);
        }

        public static String[] SplitByUpperCase([NotNull] String str, StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries)
        {
            return SplitByUpperCaseInternal(str, options).ToArray();
        }
        
        public static String[] SplitByUpperCase([NotNull] IString str, StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries)
        {
            return SplitByUpperCase(str.ToString(), options);
        }

        private static IEnumerable<String> SplitByUpperCaseInternal([CanBeNull] String str, StringSplitOptions options)
        {
            if (String.IsNullOrEmpty(str))
            {
                yield break;
            }

            Boolean allentries = options == StringSplitOptions.None;

            Int32 index = 0;
            UnicodeCategory ctype = str.GetUnicodeCategory(index);
            String value;
            for (Int32 pos = 1; pos < str.Length; pos++)
            {
                Char current = str[pos];
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
                        value = str.Substring(index, token - index);
                        if (allentries || !String.IsNullOrWhiteSpace(value))
                        {
                            yield return value;
                        }

                        index = token;
                    }
                }
                else
                {
                    value = str.Substring(index, pos - index);
                    if (allentries || !String.IsNullOrWhiteSpace(value))
                    {
                        yield return value;
                    }

                    index = pos;
                }

                ctype = type;
            }

            value = str.Substring(index, str.Length - index);
            if (allentries || !String.IsNullOrWhiteSpace(value))
            {
                yield return value;
            }
        }

        private static String[] SplitByUpperCase([NotNull] IEnumerable<String> split, StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries)
        {
            if (split is null)
            {
                throw new ArgumentNullException(nameof(split));
            }

            return split.SelectManyWhereNotNull(str => SplitByUpperCase(str, options)).ToArray();
        }

        public static String[] SplitBy([NotNull] this String str, SplitType split = SplitType.NewLine, StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries)
        {
            if (str is null)
            {
                throw new ArgumentNullException(nameof(str));
            }

            return split switch
            {
                SplitType.Chars => SplitByChars(str),
                SplitType.NewLine => SplitByNewLine(str, options),
                SplitType.Space => SplitBySpace(str, options),
                SplitType.UpperCase => SplitByUpperCase(str, options),
                SplitType.NewLineAndSpace => SplitByNewLineAndSpace(str, options),
                SplitType.NewLineAndUpperCase => SplitByUpperCase(SplitByNewLine(str, options), options),
                SplitType.SpaceAndUpperCase => SplitByUpperCase(SplitBySpace(str, options), options),
                SplitType.All => SplitByUpperCase(SplitByNewLineAndSpace(str, options), options),
                _ => throw new NotSupportedException()
            };
        }

        public static String[] SplitBy([NotNull] this IString str, SplitType split = SplitType.NewLine, StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries)
        {
            if (str is null)
            {
                throw new ArgumentNullException(nameof(str));
            }

            return SplitBy(str.ToString(), split, options);
        }

        public static String Concat([NotNull] this IEnumerable<Char> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (source is String str)
            {
                return str;
            }

            return source.ToStringBuilder().ToString();
        }

        public static String Join<T>(this String separator, [CanBeNull] IEnumerable<T> values)
        {
            return values is not null ? String.Join(separator, values) : String.Empty;
        }

        public static String Join(this String separator, [CanBeNull] IEnumerable<String> values)
        {
            return values is not null ? String.Join(separator, values) : String.Empty;
        }

        public static String Join(this String separator, [CanBeNull] IEnumerable<String> values, JoinType type)
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

        public static String Join<T>(this String separator, params T[] values)
        {
            return values is not null ? String.Join(separator, values) : String.Empty;
        }

        public static String Join(this String separator, params String[] values)
        {
            return values is not null ? String.Join(separator, values) : String.Empty;
        }

        public static String Join(this String separator, JoinType type, params String[] values)
        {
            return Join(separator, values, type);
        }

        public static String Join(this String separator, [NotNull] Func<String, Boolean> predicate, [CanBeNull] IEnumerable<String> values)
        {
            if (values is null)
            {
                return String.Empty;
            }
            
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return String.Join(separator, values.Where(predicate));
        }

        public static String Join(this String separator, [NotNull] Func<String, Boolean> predicate, [CanBeNull] params String[] values)
        {
            if (values is null)
            {
                return String.Empty;
            }
            
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            return values.Length > 0 ? String.Join(separator, values.Where(predicate)) : String.Empty;
        }

        public static IEnumerable<String> WhereNotNullOrEmpty([NotNull] this IEnumerable<String> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.WhereNot(String.IsNullOrEmpty);
        }
        
        public static IEnumerable<IString> WhereNotNullOrEmpty([NotNull] this IEnumerable<IString> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.WhereNot(IsNullOrEmpty);
        }

        public static IEnumerable<String> WhereNotNullOrWhiteSpace([NotNull] this IEnumerable<String> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.WhereNot(String.IsNullOrWhiteSpace);
        }
        
        public static IEnumerable<IString> WhereNotNullOrWhiteSpace([NotNull] this IEnumerable<IString> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.WhereNot(IsNullOrWhiteSpace);
        }

        public static String CapitalizeFirstChar(this String value)
        {
            if (String.IsNullOrEmpty(value))
            {
                return value;
            }

            return value[0].ToString().ToUpper() + value.Substring(1);
        }

        public static String Repeat([NotNull] this String value, Int32 count)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return count <= 1 ? value : new StringBuilder(value.Length * count).Insert(0, value, count).ToString();
        }

        public static Boolean IsMatrix([NotNull] this IEnumerable<String> value)
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

        public static Size GetMatrixSize([NotNull] this IEnumerable<String> value)
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

        public static Int32 CountAnsi([NotNull] this String value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return AnsiRegex.Matches(value).Count;
        }

        public static String RemoveAnsi([NotNull] this String value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return AnsiRegex.Replace(value, String.Empty);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String Comment(this String value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return $"\"{value}\"";
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String CommentSingle(String value)
        {
            return $"\'{value}\'";
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String Comment(this String value, Boolean single)
        {
            return single ? CommentSingle(value) : Comment(value);
        }

        public static String AddPrefix(this String value, String prefix)
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

        public static String AddSuffix(this String value, String suffix)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return String.IsNullOrEmpty(suffix) ? value : value + suffix;
        }

        public static String AddPrefixAndSuffix(this String value, String str)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (String.IsNullOrEmpty(str))
            {
                return value;
            }

            return str + value + str;
        }

        public static String RemovePrefix(this String value, String prefix)
        {
            return RemovePrefix(value, prefix, StringComparison.Ordinal);
        }

        public static String RemovePrefix(this String value, String prefix, StringComparison comparison)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (prefix is null)
            {
                throw new ArgumentNullException(nameof(prefix));
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

        public static String RemoveSuffix(this String value, String suffix, StringComparison comparison)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (suffix is null)
            {
                throw new ArgumentNullException(nameof(suffix));
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

        public static String RemovePrefixAndSuffix([NotNull] this String value, String trim, StringComparison comparison)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
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

        private static String ToOppositeCaseInternal([NotNull] this String value, [NotNull] Func<Char, Char> upper, [NotNull] Func<Char, Char> lower)
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
            
            StringBuilder builder = new StringBuilder();

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

        public static String ToOppositeCase([NotNull] this String value)
        {
            return ToOppositeCase(value, null);
        }
        
        public static String ToOppositeCase([NotNull] this String value, CultureInfo info)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (value.IsEmpty())
            {
                return value;
            }

            Func<Char, Char> upper;
            Func<Char, Char> lower;
            
            if (info is null)
            {
                upper = Char.ToUpper;
                lower = Char.ToLower;
            }
            else
            {
                upper = chr => Char.ToUpper(chr, info);
                lower = chr => Char.ToLower(chr, info);
            }

            return ToOppositeCaseInternal(value, upper, lower);
        }
        
        public static String ToOppositeCaseInvariant([NotNull] this String value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return value.IsEmpty() ? value : ToOppositeCaseInternal(value, Char.ToUpperInvariant, Char.ToLowerInvariant);
        }

        public static String UpperTo([NotNull] this String value, Int32 index)
        {
            return UpperTo(value, index, null);
        }

        public static String UpperTo([NotNull] this String value, Int32 index, [CanBeNull] CultureInfo? info)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            
            return index switch
            {
                -1 => value.ToUpperInvariant(),
                0 => value,
                _ => value.Substring(0, index).ToUpper(info)
            };
        }
        
        public static String UpperToInvariant([NotNull] this String value, Int32 index)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return index switch
            {
                -1 => value.ToUpperInvariant(),
                0 => value,
                _ => value.Substring(0, index).ToUpperInvariant() + value.Substring(index + 1, value.Length - index - 1)
            };
        }

        public static String UpperTo([NotNull] this String value, Char character)
        {
            return UpperTo(value, character, null);
        }
        
        public static String UpperTo([NotNull] this String value, Char character, [CanBeNull] CultureInfo? info)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return UpperTo(value, value.IndexOf(character), info);
        }
        
        public static String UpperToInvariant([NotNull] this String value, Char character)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return UpperToInvariant(value, value.IndexOf(character));
        }
        
        public static String UpperToInclude([NotNull] this String value, Char character)
        {
            return UpperToInclude(value, character, null);
        }
        
        public static String UpperToInclude([NotNull] this String value, Char character, [CanBeNull] IFormatProvider? provider)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            
        }
        
        public static String UpperToIncludeInvariant([NotNull] this String value, Char character)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            
        }

        /// <summary>
        /// Returns the beginning portion of s up to, but not including,
        /// the first occurrence of the character c. If c is not present in
        /// s, then s is returned.
        /// </summary>
        public static String UpTo([NotNull] this String value, Char character)
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
        public static String Coalesce(params String[] values)
        {
            if (values is null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            return values.FirstOrDefault(str => !String.IsNullOrEmpty(str));
        }

        /// <summary>
        /// Indents this text by the specified space count.
        /// </summary>
        /// <param name="value">The text to indent.</param>
        /// <param name="count">The number of spaces to indent.</param>
        public static String Indent(this String value, Int32 count)
        {
            String indentation = new String(' ', count);
            String replacement = Environment.NewLine + indentation;
            return indentation + value.Replace(Environment.NewLine, replacement);
        }

        /// <summary>
        /// Returns the number of occurrences of the specified character in this string.
        /// </summary>
        /// <param name="value">The text.</param>
        /// <param name="character">The character to count occurrences for.</param>
        /// <param name="caseSensitive">Determines whether or not to compare in case sensitive or case insensitive way.</param>
        public static Int32 OccurrencesOf(this String value, Char character, Boolean caseSensitive = true)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (!caseSensitive)
            {
                character = Char.ToLowerInvariant(character);
                value = value.ToLowerInvariant();
            }

            return value.Count(c => c == character);
        }

        /// <summary>
        /// Returns a new string in which all occurences of the specified value are removed.
        /// </summary>
        /// <param name="value">The text.</param>
        /// <param name="str">The string to seek and remove.</param>
        public static String RemoveAllOf(this String value, String str)
        {
            return value.Replace(str, String.Empty);
        }

        /// <summary>
        /// Returns a new string in which the first occurence of the specified value is removed.
        /// </summary>
        /// <param name="value">The text.</param>
        /// <param name="str">The string to seek and remove its first occurence.</param>
        public static String RemoveFirstOf(this String value, String str)
        {
            Int32 index = value.IndexOf(str, StringComparison.Ordinal);
            return index < 0 ? value : value.Remove(index, str.Length);
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
        /// <param name="length">The maximum length of the returned string.</param>
        /// <param name="included">Included or excluded 3 dots</param>
        public static String ShortenWith3Dots(this String value, Int32 length, Boolean included)
        {
            return included ? ShortenWith3DotsIncluded(value, length) : ShortenWith3Dots(value, length);
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

        public static String RemoveDiacritics([NotNull] this String value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (value.IsEmpty())
            {
                return value;
            }

            String normalized = value.Normalize(NormalizationForm.FormD);
            StringBuilder builder = new StringBuilder();
            
            foreach (Char chr in normalized.Where(chr => CharUnicodeInfo.GetUnicodeCategory(chr) != UnicodeCategory.NonSpacingMark))
            {
                builder.Append(chr);
            }
            
            return builder.ToString().Normalize(NormalizationForm.FormC);
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
        public static MemoryStream ToStream(this String value, Encoding encoding)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            MemoryStream stream = new MemoryStream();
            using StreamWriter writer = new StreamWriter(stream, encoding);
            writer.Write(value);
            writer.Flush();
            stream.Position = 0;

            return stream;
        }

        /// <summary>
        /// Converts input characters into a read-only secure string.
        /// </summary>
        /// <param name="source">The characters to convert.</param>
        [SecurityCritical]
        public static SecureString ToSecureString([NotNull] this IEnumerable<Char> source)
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
        public static String ToInsecureString([NotNull] this SecureString value)
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

        public static IString ToIString([CanBeNull] this String value)
        {
            if (value is null)
            {
                return IStringNull;
            }

            return value.Length > 0 ? new StringAdapter(value) : IStringEmpty;
        }
    }
}