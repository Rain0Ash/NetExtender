// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Utilities.Numerics;

namespace NetExtender.Utilities.Types
{
    public static class RegexUtilities
    {
        public static Boolean IsValidRegex(String pattern)
        {
            if (String.IsNullOrEmpty(pattern))
            {
                return false;
            }

            try
            {
                _ = Regex.Match(String.Empty, pattern);
                return true;
            }
            catch (ArgumentException)
            {
                return false;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String JoinMatches(String value, String pattern, RegexOptions options = RegexOptions.None)
        {
            return JoinMatches(value, pattern, String.Empty, options);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String JoinMatches(String value, String pattern, String? separator, RegexOptions options = RegexOptions.None)
        {
            return JoinMatches(Regex.Matches(value, pattern, options), separator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String JoinMatches(this Regex regex, String value)
        {
            return JoinMatches(regex, value, String.Empty);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String JoinMatches(this Regex regex, String value, String? separator)
        {
            if (regex is null)
            {
                throw new ArgumentNullException(nameof(regex));
            }

            return JoinMatches(regex.Matches(value), separator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<String> JoinMatchesAsync(String value, String pattern, RegexOptions options = RegexOptions.None)
        {
            return JoinMatchesAsync(value, pattern, String.Empty, options, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<String> JoinMatchesAsync(String value, String pattern, RegexOptions options, CancellationToken token)
        {
            return JoinMatchesAsync(value, pattern, String.Empty, options, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<String> JoinMatchesAsync(String value, String pattern, String? separator, RegexOptions options = RegexOptions.None)
        {
            return JoinMatchesAsync(value, pattern, separator, options, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<String> JoinMatchesAsync(String value, String pattern, String? separator, RegexOptions options, CancellationToken token)
        {
            return JoinMatches(await MatchesAsync(value, pattern, options, token).ConfigureAwait(false), separator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<String> JoinMatchesAsync(this Regex regex, String value)
        {
            return JoinMatchesAsync(regex, value, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<String> JoinMatchesAsync(this Regex regex, String value, CancellationToken token)
        {
            if (regex is null)
            {
                throw new ArgumentNullException(nameof(regex));
            }

            return JoinMatchesAsync(regex, value, String.Empty, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<String> JoinMatchesAsync(this Regex regex, String value, String? separator)
        {
            return JoinMatchesAsync(regex, value, separator, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<String> JoinMatchesAsync(this Regex regex, String value, String? separator, CancellationToken token)
        {
            if (regex is null)
            {
                throw new ArgumentNullException(nameof(regex));
            }

            return JoinMatches(await MatchesAsync(regex, value, token).ConfigureAwait(false), separator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String JoinMatches(this MatchCollection matches)
        {
            return JoinMatches(matches, String.Empty);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String JoinMatches(this MatchCollection matches, String? separator)
        {
            if (matches is null)
            {
                throw new ArgumentNullException(nameof(matches));
            }

            return String.Join(separator ?? String.Empty, GetCaptures(matches).Skip(1));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<String> GetCaptures(String value, String pattern, RegexOptions options = RegexOptions.None)
        {
            return GetCaptures(Regex.Matches(value, pattern, options));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<String> GetCaptures(this Regex regex, String value)
        {
            if (regex is null)
            {
                throw new ArgumentNullException(nameof(regex));
            }

            return GetCaptures(regex.Matches(value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<String>> GetCapturesAsync(String value, String pattern, RegexOptions options = RegexOptions.None)
        {
            return GetCapturesAsync(value, pattern, options, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<IEnumerable<String>> GetCapturesAsync(String value, String pattern, RegexOptions options, CancellationToken token)
        {
            return GetCaptures(await MatchesAsync(value, pattern, options, token).ConfigureAwait(false));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<String>> GetCapturesAsync(this Regex regex, String value)
        {
            return GetCapturesAsync(regex, value, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<IEnumerable<String>> GetCapturesAsync(this Regex regex, String value, CancellationToken token)
        {
            if (regex is null)
            {
                throw new ArgumentNullException(nameof(regex));
            }

            return GetCaptures(await MatchesAsync(regex, value, token).ConfigureAwait(false));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<String> GetCaptures(this MatchCollection matches)
        {
            if (matches is null)
            {
                throw new ArgumentNullException(nameof(matches));
            }

            return matches
                .SelectMany(match => match.Groups.OfType<Group>())
                .SelectMany(group => group.Captures)
                .Select(capture => capture.Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<String> IfMatchGetCaptures(String value, String pattern)
        {
            return IfMatchGetCaptures(value, pattern, RegexOptions.None);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<String> IfMatchGetCaptures(String value, String pattern, RegexOptions options)
        {
            return IfMatchGetCaptures(Regex.Matches(value, pattern, options), value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<String> IfMatchGetCaptures(this Regex regex, String value)
        {
            if (regex is null)
            {
                throw new ArgumentNullException(nameof(regex));
            }

            return IfMatchGetCaptures(regex.Matches(value), value);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<String>> IfMatchGetCapturesAsync(String value, String pattern)
        {
            return IfMatchGetCapturesAsync(value, pattern, RegexOptions.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<String>> IfMatchGetCapturesAsync(String value, String pattern, RegexOptions options)
        {
            return IfMatchGetCapturesAsync(value, pattern, options, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<IEnumerable<String>> IfMatchGetCapturesAsync(String value, String pattern, RegexOptions options, CancellationToken token)
        {
            return IfMatchGetCaptures(await MatchesAsync(value, pattern, options, token).ConfigureAwait(false), value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IEnumerable<String>> IfMatchGetCapturesAsync(this Regex regex, String value)
        {
            return IfMatchGetCapturesAsync(regex, value, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async Task<IEnumerable<String>> IfMatchGetCapturesAsync(this Regex regex, String value, CancellationToken token)
        {
            if (regex is null)
            {
                throw new ArgumentNullException(nameof(regex));
            }

            return IfMatchGetCaptures(await MatchesAsync(regex, value, token).ConfigureAwait(false), value);
        }

        public static IEnumerable<String> IfMatchGetCaptures(this MatchCollection matches, String value)
        {
            if (matches is null)
            {
                throw new ArgumentNullException(nameof(matches));
            }

            String[] captures = GetCaptures(matches).ToArray();
            return captures.FirstOrDefault()?.Equals(value) == true ? captures : Array.Empty<String>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IDictionary<String, IList<String>> MatchNamedCaptures(this Regex regex, String input)
        {
            return MatchNamedCaptures(regex, input, true);
        }

        public static IDictionary<String, IList<String>> MatchNamedCaptures(this Regex regex, String input, Boolean nogroup)
        {
            if (regex is null)
            {
                throw new ArgumentNullException(nameof(regex));
            }

            if (input is null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            MatchCollection matches = regex.Matches(input);
            String[] groups = regex.GetGroupNames();

            return MatchNamedCaptures(matches, groups, nogroup);
        }
        
        public static IDictionary<String, IList<String>> MatchNamedCaptures(this MatchCollection matches, params String?[] groups)
        {
            return MatchNamedCaptures(matches, (IEnumerable<String?>) groups);
        }

        public static IDictionary<String, IList<String>> MatchNamedCaptures(this MatchCollection matches, IEnumerable<String?> groups)
        {
            return MatchNamedCaptures(matches, groups, true);
        }
        
        public static IDictionary<String, IList<String>> MatchNamedCaptures(this MatchCollection matches, Boolean nogroup, params String?[] groups)
        {
            return MatchNamedCaptures(matches, groups, nogroup);
        }

        public static IDictionary<String, IList<String>> MatchNamedCaptures(this MatchCollection matches, IEnumerable<String?> groups, Boolean nogroup)
        {
            if (matches is null)
            {
                throw new ArgumentNullException(nameof(matches));
            }

            Dictionary<String, IList<String>> captures = new Dictionary<String, IList<String>>(matches.Count);
            foreach (String? groupname in groups.WhereNotNull().WhereIfNot(MathUtilities.IsInt32, nogroup))
            {
                foreach (Match match in matches)
                {
                    GroupCollection collection = match.Groups;
                    Group group = collection[groupname];

                    if (group.Captures.Count <= 0)
                    {
                        continue;
                    }

                    captures.GetOrAdd(groupname, () => new List<String>(8)).Add(group.Value);
                }
            }

            captures.TrimExcess();
            return captures;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IDictionary<String, IList<String>>> MatchNamedCapturesAsync(this Regex regex, String input)
        {
            return MatchNamedCapturesAsync(regex, input, true);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IDictionary<String, IList<String>>> MatchNamedCapturesAsync(this Regex regex, String input, Boolean nogroup)
        {
            return MatchNamedCapturesAsync(regex, input, nogroup, CancellationToken.None);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<IDictionary<String, IList<String>>> MatchNamedCapturesAsync(this Regex regex, String input, CancellationToken token)
        {
            return MatchNamedCapturesAsync(regex, input, true, token);
        }

        public static async Task<IDictionary<String, IList<String>>> MatchNamedCapturesAsync(this Regex regex, String input, Boolean nogroup, CancellationToken token)
        {
            if (regex is null)
            {
                throw new ArgumentNullException(nameof(regex));
            }

            MatchCollection matches = await MatchesAsync(regex, input, token).ConfigureAwait(false);
            String[] groups = regex.GetGroupNames();

            return MatchNamedCaptures(matches, groups, nogroup);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Match> MatchAsync(String input, String pattern)
        {
            return MatchAsync(input, pattern, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Match> MatchAsync(String input, String pattern, CancellationToken token)
        {
            return Task.Run(() => Regex.Match(input, pattern), token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Match> MatchAsync(String input, String pattern, RegexOptions options)
        {
            return MatchAsync(input, pattern, options, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Match> MatchAsync(String input, String pattern, RegexOptions options, CancellationToken token)
        {
            return Task.Run(() => Regex.Match(input, pattern, options), token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Match> MatchAsync(String input, String pattern, RegexOptions options, TimeSpan timeout)
        {
            return MatchAsync(input, pattern, options, timeout, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Match> MatchAsync(String input, String pattern, RegexOptions options, TimeSpan timeout, CancellationToken token)
        {
            return Task.Run(() => Regex.Match(input, pattern, options, timeout), token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Match> MatchAsync(this Regex regex, String input)
        {
            return MatchAsync(regex, input, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Match> MatchAsync(this Regex regex, String input, CancellationToken token)
        {
            if (regex is null)
            {
                throw new ArgumentNullException(nameof(regex));
            }

            return Task.Run(() => regex.Match(input), token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Match> MatchAsync(this Regex regex, String input, Int32 startat)
        {
            return MatchAsync(regex, input, startat, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Match> MatchAsync(this Regex regex, String input, Int32 startat, CancellationToken token)
        {
            if (regex is null)
            {
                throw new ArgumentNullException(nameof(regex));
            }

            return Task.Run(() => regex.Match(input, startat), token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Match> MatchAsync(this Regex regex, String input, Int32 beginning, Int32 length)
        {
            return MatchAsync(regex, input, beginning, length, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Match> MatchAsync(this Regex regex, String input, Int32 beginning, Int32 length, CancellationToken token)
        {
            if (regex is null)
            {
                throw new ArgumentNullException(nameof(regex));
            }

            return Task.Run(() => regex.Match(input, beginning, length), token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<MatchCollection> MatchesAsync(String input, String pattern)
        {
            return MatchesAsync(input, pattern, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<MatchCollection> MatchesAsync(String input, String pattern, CancellationToken token)
        {
            return Task.Run(() => Regex.Matches(input, pattern), token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<MatchCollection> MatchesAsync(String input, String pattern, RegexOptions options)
        {
            return MatchesAsync(input, pattern, options, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<MatchCollection> MatchesAsync(String input, String pattern, RegexOptions options, CancellationToken token)
        {
            return Task.Run(() => Regex.Matches(input, pattern, options), token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<MatchCollection> MatchesAsync(String input, String pattern, RegexOptions options, TimeSpan timeout)
        {
            return MatchesAsync(input, pattern, options, timeout, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<MatchCollection> MatchesAsync(String input, String pattern, RegexOptions options, TimeSpan timeout, CancellationToken token)
        {
            return Task.Run(() => Regex.Matches(input, pattern, options, timeout), token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<MatchCollection> MatchesAsync(this Regex regex, String input)
        {
            return MatchesAsync(regex, input, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<MatchCollection> MatchesAsync(this Regex regex, String input, CancellationToken token)
        {
            if (regex is null)
            {
                throw new ArgumentNullException(nameof(regex));
            }

            return Task.Run(() => regex.Matches(input), token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<MatchCollection> MatchesAsync(this Regex regex, String input, Int32 startat)
        {
            return MatchesAsync(regex, input, startat, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<MatchCollection> MatchesAsync(this Regex regex, String input, Int32 startat, CancellationToken token)
        {
            if (regex is null)
            {
                throw new ArgumentNullException(nameof(regex));
            }

            return Task.Run(() => regex.Matches(input, startat), token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<String> ReplaceAsync(String input, String pattern, MatchEvaluator evaluator)
        {
            return ReplaceAsync(input, pattern, evaluator, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<String> ReplaceAsync(String input, String pattern, MatchEvaluator evaluator, CancellationToken token)
        {
            return Task.Run(() => Regex.Replace(input, pattern, evaluator), token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<String> ReplaceAsync(String input, String pattern, MatchEvaluator evaluator, RegexOptions options)
        {
            return ReplaceAsync(input, pattern, evaluator, options, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<String> ReplaceAsync(String input, String pattern, MatchEvaluator evaluator, RegexOptions options, CancellationToken token)
        {
            return Task.Run(() => Regex.Replace(input, pattern, evaluator, options), token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<String> ReplaceAsync(String input, String pattern, MatchEvaluator evaluator, RegexOptions options, TimeSpan timeout)
        {
            return ReplaceAsync(input, pattern, evaluator, options, timeout, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<String> ReplaceAsync(String input, String pattern, MatchEvaluator evaluator, RegexOptions options, TimeSpan timeout, CancellationToken token)
        {
            return Task.Run(() => Regex.Replace(input, pattern, evaluator, options, timeout), token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<String> ReplaceAsync(String input, String pattern, String replacement)
        {
            return ReplaceAsync(input, pattern, replacement, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<String> ReplaceAsync(String input, String pattern, String replacement, CancellationToken token)
        {
            return Task.Run(() => Regex.Replace(input, pattern, replacement), token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<String> ReplaceAsync(String input, String pattern, String replacement, RegexOptions options)
        {
            return ReplaceAsync(input, pattern, replacement, options, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<String> ReplaceAsync(String input, String pattern, String replacement, RegexOptions options, CancellationToken token)
        {
            return Task.Run(() => Regex.Replace(input, pattern, replacement, options), token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<String> ReplaceAsync(String input, String pattern, String replacement, RegexOptions options, TimeSpan timeout)
        {
            return ReplaceAsync(input, pattern, replacement, options, timeout, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<String> ReplaceAsync(String input, String pattern, String replacement, RegexOptions options, TimeSpan timeout, CancellationToken token)
        {
            return Task.Run(() => Regex.Replace(input, pattern, replacement, options, timeout), token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<String> ReplaceAsync(this Regex regex, String input, MatchEvaluator evaluator)
        {
            return ReplaceAsync(regex, input, evaluator, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<String> ReplaceAsync(this Regex regex, String input, MatchEvaluator evaluator, CancellationToken token)
        {
            if (regex is null)
            {
                throw new ArgumentNullException(nameof(regex));
            }

            return Task.Run(() => regex.Replace(input, evaluator), token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<String> ReplaceAsync(this Regex regex, String input, MatchEvaluator evaluator, Int32 count)
        {
            return ReplaceAsync(regex, input, evaluator, count, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<String> ReplaceAsync(this Regex regex, String input, MatchEvaluator evaluator, Int32 count, CancellationToken token)
        {
            if (regex is null)
            {
                throw new ArgumentNullException(nameof(regex));
            }

            return Task.Run(() => regex.Replace(input, evaluator, count), token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<String> ReplaceAsync(this Regex regex, String input, MatchEvaluator evaluator, Int32 count, Int32 startat)
        {
            return ReplaceAsync(regex, input, evaluator, count, startat, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<String> ReplaceAsync(this Regex regex, String input, MatchEvaluator evaluator, Int32 count, Int32 startat, CancellationToken token)
        {
            if (regex is null)
            {
                throw new ArgumentNullException(nameof(regex));
            }

            return Task.Run(() => regex.Replace(input, evaluator, count, startat), token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<String> ReplaceAsync(this Regex regex, String input, String replacement)
        {
            return ReplaceAsync(regex, input, replacement, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<String> ReplaceAsync(this Regex regex, String input, String replacement, CancellationToken token)
        {
            if (regex is null)
            {
                throw new ArgumentNullException(nameof(regex));
            }

            return Task.Run(() => regex.Replace(input, replacement), token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<String> ReplaceAsync(this Regex regex, String input, String replacement, Int32 count)
        {
            return ReplaceAsync(regex, input, replacement, count, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<String> ReplaceAsync(this Regex regex, String input, String replacement, Int32 count, CancellationToken token)
        {
            if (regex is null)
            {
                throw new ArgumentNullException(nameof(regex));
            }

            return Task.Run(() => regex.Replace(input, replacement, count), token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<String> ReplaceAsync(this Regex regex, String input, String replacement, Int32 count, Int32 startat)
        {
            return ReplaceAsync(regex, input, replacement, count, startat, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<String> ReplaceAsync(this Regex regex, String input, String replacement, Int32 count, Int32 startat, CancellationToken token)
        {
            if (regex is null)
            {
                throw new ArgumentNullException(nameof(regex));
            }

            return Task.Run(() => regex.Replace(input, replacement, count, startat), token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<String[]> SplitAsync(String input, String pattern)
        {
            return SplitAsync(input, pattern, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<String[]> SplitAsync(String input, String pattern, CancellationToken token)
        {
            return Task.Run(() => Regex.Split(input, pattern), token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<String[]> SplitAsync(String input, String pattern, RegexOptions options)
        {
            return SplitAsync(input, pattern, options, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<String[]> SplitAsync(String input, String pattern, RegexOptions options, CancellationToken token)
        {
            return Task.Run(() => Regex.Split(input, pattern, options), token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<String[]> SplitAsync(String input, String pattern, RegexOptions options, TimeSpan timeout)
        {
            return SplitAsync(input, pattern, options, timeout, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<String[]> SplitAsync(String input, String pattern, RegexOptions options, TimeSpan timeout, CancellationToken token)
        {
            return Task.Run(() => Regex.Split(input, pattern, options, timeout), token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<String[]> SplitAsync(this Regex regex, String input)
        {
            return SplitAsync(regex, input, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<String[]> SplitAsync(this Regex regex, String input, CancellationToken token)
        {
            if (regex is null)
            {
                throw new ArgumentNullException(nameof(regex));
            }

            return Task.Run(() => regex.Split(input), token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<String[]> SplitAsync(this Regex regex, String input, Int32 startat)
        {
            return SplitAsync(regex, input, startat, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<String[]> SplitAsync(this Regex regex, String input, Int32 startat, CancellationToken token)
        {
            if (regex is null)
            {
                throw new ArgumentNullException(nameof(regex));
            }

            return Task.Run(() => regex.Split(input, startat), token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<String[]> SplitAsync(this Regex regex, String input, Int32 count, Int32 startat)
        {
            return SplitAsync(regex, input, count, startat, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<String[]> SplitAsync(this Regex regex, String input, Int32 count, Int32 startat, CancellationToken token)
        {
            if (regex is null)
            {
                throw new ArgumentNullException(nameof(regex));
            }

            return Task.Run(() => regex.Split(input, count, startat), token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> IsMatchAsync(String input, String pattern)
        {
            return IsMatchAsync(input, pattern, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> IsMatchAsync(String input, String pattern, CancellationToken token)
        {
            return Task.Run(() => Regex.IsMatch(input, pattern), token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> IsMatchAsync(String input, String pattern, RegexOptions options)
        {
            return IsMatchAsync(input, pattern, options, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> IsMatchAsync(String input, String pattern, RegexOptions options, CancellationToken token)
        {
            return Task.Run(() => Regex.IsMatch(input, pattern, options), token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> IsMatchAsync(String input, String pattern, RegexOptions options, TimeSpan timeout)
        {
            return IsMatchAsync(input, pattern, options, timeout, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> IsMatchAsync(String input, String pattern, RegexOptions options, TimeSpan timeout, CancellationToken token)
        {
            return Task.Run(() => Regex.IsMatch(input, pattern, options, timeout), token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> IsMatchAsync(this Regex regex, String input)
        {
            return IsMatchAsync(regex, input, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> IsMatchAsync(this Regex regex, String input, CancellationToken token)
        {
            if (regex is null)
            {
                throw new ArgumentNullException(nameof(regex));
            }

            return Task.Run(() => regex.IsMatch(input), token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> IsMatchAsync(this Regex regex, String input, Int32 startat)
        {
            return IsMatchAsync(regex, input, startat, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Boolean> IsMatchAsync(this Regex regex, String input, Int32 startat, CancellationToken token)
        {
            if (regex is null)
            {
                throw new ArgumentNullException(nameof(regex));
            }

            return Task.Run(() => regex.IsMatch(input, startat), token);
        }
    }
}