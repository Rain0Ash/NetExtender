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

        public static Boolean IsBracketsWellFormed(String str)
        {
            Stack<Char> brackets = new Stack<Char>();

            try
            {
                foreach (Char c in str)
                {
                    if (BracketPairs.Keys.Contains(c))
                    {
                        brackets.Push(c);
                    }
                    else
                    {
                        if (!BracketPairs.Values.Contains(c))
                        {
                            continue;
                        }

                        if (c != BracketPairs[brackets.First()])
                        {
                            return false;
                        }

                        brackets.Pop();
                    }
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

        public static Int32 FormatArgsExpected(this String str)
        {
            const String pattern = @"(?<!\{)(?>\{\{)*\{\d(.*?)";

            if (String.IsNullOrWhiteSpace(str))
            {
                return 0;
            }

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

        public static String Format(this String source, params Object[] args)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            Int32 expected = FormatArgsExpected(source);

            if (expected <= 0)
            {
                return source;
            }

            const String nullArgs = @"null";

            args = args.Length < expected
                ? args.Concat(Enumerable.Repeat(nullArgs, expected - args.Length)).ToArray()
                : args.Take(expected).ToArray();

            return String.Format(source, args);
        }

        public static Boolean EndsWith(this String str, IEnumerable<Char> chars)
        {
            return chars.Any(str.EndsWith);
        }

        public static Boolean EndsWith(this String str, IEnumerable<String> substrings)
        {
            return substrings.Any(str.EndsWith);
        }

        public static Boolean IsNull(this String str)
        {
            return str is null;
        }
        
        public static Boolean IsNotNull(this String str)
        {
            return str is not null;
        }

        public static Boolean IsEmpty(this String str)
        {
            return str == String.Empty;
        }
        
        public static Boolean IsNotEmpty(this String str)
        {
            return str != String.Empty;
        }

        public static Boolean IsWhiteSpace(this String str)
        {
            return str is not null && str.Length > 0 && str.All(Char.IsWhiteSpace);
        }

        public static Boolean IsNotWhiteSpace(this String str)
        {
            return !IsWhiteSpace(str);
        }

        public static Boolean IsEmptyOrWhiteSpace(this String str)
        {
            return str is not null && (str.Length <= 0 || str.All(Char.IsWhiteSpace));
        }

        public static Boolean IsNullOrEmpty(this String str)
        {
            return String.IsNullOrEmpty(str);
        }

        public static Boolean IsNullOrWhiteSpace(this String str)
        {
            return String.IsNullOrWhiteSpace(str);
        }

        public static Boolean IsNotNullOrEmpty(this String str)
        {
            return !String.IsNullOrEmpty(str);
        }

        public static Boolean IsNotNullOrWhiteSpace(this String str)
        {
            return !String.IsNullOrWhiteSpace(str);
        }

        /// <inheritdoc cref="char.GetUnicodeCategory(String,Int32)"/>
        public static UnicodeCategory GetUnicodeCategory(this String str, Int32 index)
        {
            return Char.GetUnicodeCategory(str, index);
        }

        public static String[] SplitByChars(String str)
        {
            return str.Split();
        }

        public static String[] SplitByNewLine(String str, StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries)
        {
            return SplitByNewLine(str, Int32.MaxValue, options);
        }

        public static String[] SplitByNewLine(String str, Int32 count, StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries)
        {
            return str.Split('\n', count, options);
        }

        public static String[] SplitBySpace(String str, StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries)
        {
            return SplitBySpace(str, Int32.MaxValue, options);
        }

        public static String[] SplitBySpace(String str, Int32 count, StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries)
        {
            return str.Split(' ', count, options);
        }

        private static readonly Char[] NewLineAndSpaceChars = {'\n', ' '};

        public static String[] SplitByNewLineAndSpace(String str, StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries)
        {
            return SplitByNewLineAndSpace(str, Int32.MaxValue, options);
        }

        public static String[] SplitByNewLineAndSpace(String str, Int32 count, StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries)
        {
            return str.Split(NewLineAndSpaceChars, count, options);
        }

        public static String[] SplitByUpperCase(String str, StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries)
        {
            return SplitByUpperCaseInternal(str, options).ToArray();
        }

        private static IEnumerable<String> SplitByUpperCaseInternal(String str, StringSplitOptions options)
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

        private static String[] SplitByUpperCase(IEnumerable<String> split, StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries)
        {
            return split.SelectMany(str => SplitByUpperCase(str, options)).ToArray();
        }

        public static String[] SplitBy(this String str, SplitType split = SplitType.NewLine, StringSplitOptions options = StringSplitOptions.RemoveEmptyEntries)
        {
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

        public static String Join<T>(this String separator, IEnumerable<T> values)
        {
            return String.Join(separator, values);
        }

        public static String Join(this String separator, IEnumerable<String> values)
        {
            return String.Join(separator, values);
        }

        public static String Join(this String separator, IEnumerable<String> values, JoinType type)
        {
            return type switch
            {
                JoinType.Default => String.Join(separator, values),
                JoinType.NotEmpty => Join(separator, IsNotNullOrEmpty, values),
                JoinType.NotWhiteSpace => Join(separator, IsNotNullOrWhiteSpace, values),
                _ => throw new NotSupportedException()
            };
        }

        public static String Join<T>(this String separator, params T[] values)
        {
            return String.Join(separator, values);
        }

        public static String Join(this String separator, params String[] values)
        {
            return String.Join(separator, values);
        }

        public static String Join(this String separator, JoinType type, params String[] values)
        {
            return Join(separator, values, type);
        }

        public static String Join(this String separator, Func<String, Boolean> predicate, IEnumerable<String> values)
        {
            return String.Join(separator, values.Where(predicate));
        }

        public static String Join(this String separator, Func<String, Boolean> predicate, params String[] values)
        {
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

        public static IEnumerable<String> WhereNotNullOrWhiteSpace([NotNull] this IEnumerable<String> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.WhereNot(String.IsNullOrWhiteSpace);
        }

        public static String CapitalizeFirstChar(this String value)
        {
            if (String.IsNullOrEmpty(value))
            {
                return value;
            }

            return value[0].ToString().ToUpper() + value.Substring(1);
        }

        public static String Repeat(this String value, Int32 count)
        {
            return count <= 1 ? value : new StringBuilder(value.Length * count).Insert(0, value, count).ToString();
        }

        public static Boolean IsMatrix(this IReadOnlyCollection<String> value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (value.Count <= 0)
            {
                throw new ArgumentException(@"Value cannot be an empty collection.", nameof(value));
            }

            return value.AllSame(row => row.RemoveAnsi().Length);
        }

        public static Size GetMatrixSize(this IReadOnlyCollection<String> value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (value.Count <= 0)
            {
                throw new ArgumentException(@"Value cannot be an empty collection.", nameof(value));
            }

            if (!IsMatrix(value))
            {
                throw new ArgumentException(@"Different strings length", nameof(value));
            }

            return new Size(value.First().RemoveAnsi().Length, value.Count);
        }

        private static Regex AnsiRegex { get; } = new Regex(@"\x1B(?:[@-Z\\-_]|\[[0-?]*[ -/]*[@-~])", RegexOptions.Compiled);

        public static Int32 CountAnsi(this String value)
        {
            return AnsiRegex.Matches(value).Count;
        }

        public static String RemoveAnsi(this String value)
        {
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

        /// <summary>
        /// Returns the beginning portion of s up to, but not including,
        /// the first occurrence of the character c. If c is not present in
        /// s, then s is returned.
        /// </summary>
        public static String UpTo(this String value, Char character)
        {
            return value.TakeWhile(ch => ch != character).Join();
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
    }
}