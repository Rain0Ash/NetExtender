// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace NetExtender.Utils.Types
{
    public static class RegexUtils
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

        public static String JoinMatches(String str, String pattern, RegexOptions options = RegexOptions.None)
        {
            return JoinMatches(str, pattern, String.Empty, options);
        }

        public static String JoinMatches(String str, String pattern, String separator, RegexOptions options = RegexOptions.None)
        {
            return JoinMatches(Regex.Matches(str, pattern, options), separator);
        }

        public static String JoinMatches(this Regex regex, String str)
        {
            return JoinMatches(regex, str, String.Empty);
        }

        public static String JoinMatches(this Regex regex, String str, String separator)
        {
            if (regex is null)
            {
                throw new ArgumentNullException(nameof(regex));
            }

            return JoinMatches(regex.Matches(str), separator);
        }

        public static Task<String> JoinMatchesAsync(String str, String pattern, RegexOptions options = RegexOptions.None)
        {
            return JoinMatchesAsync(str, pattern, String.Empty, options, CancellationToken.None);
        }

        public static Task<String> JoinMatchesAsync(String str, String pattern, RegexOptions options, CancellationToken token)
        {
            return JoinMatchesAsync(str, pattern, String.Empty, options, token);
        }

        public static Task<String> JoinMatchesAsync(String str, String pattern, String separator, RegexOptions options = RegexOptions.None)
        {
            return JoinMatchesAsync(str, pattern, separator, options, CancellationToken.None);
        }

        public static async Task<String> JoinMatchesAsync(String str, String pattern, String separator, RegexOptions options, CancellationToken token)
        {
            return JoinMatches(await MatchesAsync(str, pattern, options, token).ConfigureAwait(false), separator);
        }

        public static Task<String> JoinMatchesAsync(this Regex regex, String str)
        {
            return JoinMatchesAsync(regex, str, CancellationToken.None);
        }

        public static Task<String> JoinMatchesAsync(this Regex regex, String str, CancellationToken token)
        {
            if (regex is null)
            {
                throw new ArgumentNullException(nameof(regex));
            }

            return JoinMatchesAsync(regex, str, String.Empty, token);
        }

        public static Task<String> JoinMatchesAsync(this Regex regex, String str, String separator)
        {
            return JoinMatchesAsync(regex, str, separator, CancellationToken.None);
        }

        public static async Task<String> JoinMatchesAsync(this Regex regex, String str, String separator, CancellationToken token)
        {
            if (regex is null)
            {
                throw new ArgumentNullException(nameof(regex));
            }

            return JoinMatches(await MatchesAsync(regex, str, token).ConfigureAwait(false), separator);
        }

        public static String JoinMatches(this MatchCollection matches)
        {
            return JoinMatches(matches, String.Empty);
        }

        public static String JoinMatches(this MatchCollection matches, String separator)
        {
            if (matches is null)
            {
                throw new ArgumentNullException(nameof(matches));
            }

            return String.Join(separator ?? String.Empty, GetCaptures(matches).Skip(1));
        }

        public static IEnumerable<String> GetCaptures(String str, String pattern, RegexOptions options = RegexOptions.None)
        {
            return GetCaptures(Regex.Matches(str, pattern, options));
        }

        public static IEnumerable<String> GetCaptures(this Regex regex, String str)
        {
            if (regex is null)
            {
                throw new ArgumentNullException(nameof(regex));
            }

            return GetCaptures(regex.Matches(str));
        }

        public static Task<IEnumerable<String>> GetCapturesAsync(String str, String pattern, RegexOptions options = RegexOptions.None)
        {
            return GetCapturesAsync(str, pattern, options, CancellationToken.None);
        }

        public static async Task<IEnumerable<String>> GetCapturesAsync(String str, String pattern, RegexOptions options, CancellationToken token)
        {
            return GetCaptures(await MatchesAsync(str, pattern, options, token).ConfigureAwait(false));
        }

        public static Task<IEnumerable<String>> GetCapturesAsync(this Regex regex, String str)
        {
            return GetCapturesAsync(regex, str, CancellationToken.None);
        }

        public static async Task<IEnumerable<String>> GetCapturesAsync(this Regex regex, String str, CancellationToken token)
        {
            if (regex is null)
            {
                throw new ArgumentNullException(nameof(regex));
            }

            return GetCaptures(await MatchesAsync(regex, str, token).ConfigureAwait(false));
        }

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

        public static IEnumerable<String> IfMatchGetCaptures(String str, String pattern, RegexOptions options = RegexOptions.None)
        {
            return IfMatchGetCaptures(Regex.Matches(str, pattern, options), str);
        }

        public static IEnumerable<String> IfMatchGetCaptures(this Regex regex, String str)
        {
            if (regex is null)
            {
                throw new ArgumentNullException(nameof(regex));
            }

            return IfMatchGetCaptures(regex.Matches(str), str);
        }

        public static Task<IEnumerable<String>> IfMatchGetCapturesAsync(String str, String pattern, RegexOptions options = RegexOptions.None)
        {
            return IfMatchGetCapturesAsync(str, pattern, options, CancellationToken.None);
        }

        public static async Task<IEnumerable<String>> IfMatchGetCapturesAsync(String str, String pattern, RegexOptions options, CancellationToken token)
        {
            return IfMatchGetCaptures(await MatchesAsync(str, pattern, options, token).ConfigureAwait(false), str);
        }

        public static Task<IEnumerable<String>> IfMatchGetCapturesAsync(this Regex regex, String str)
        {
            return IfMatchGetCapturesAsync(regex, str, CancellationToken.None);
        }

        public static async Task<IEnumerable<String>> IfMatchGetCapturesAsync(this Regex regex, String str, CancellationToken token)
        {
            if (regex is null)
            {
                throw new ArgumentNullException(nameof(regex));
            }

            return IfMatchGetCaptures(await MatchesAsync(regex, str, token).ConfigureAwait(false), str);
        }

        public static IEnumerable<String> IfMatchGetCaptures(this MatchCollection matches, String str)
        {
            if (matches is null)
            {
                throw new ArgumentNullException(nameof(matches));
            }

            String[] captures = GetCaptures(matches).ToArray();
            return captures.FirstOrDefault()?.Equals(str) == true ? captures : null;
        }

        public static IDictionary<String, IList<String>> MatchNamedCaptures(this Regex regex, String input, Boolean nogroup = true)
        {
            if (regex is null)
            {
                throw new ArgumentNullException(nameof(regex));
            }

            MatchCollection matches = regex.Matches(input);
            String[] groupNames = regex.GetGroupNames();

            return MatchNamedCaptures(matches, groupNames, nogroup);
        }

        public static Task<IDictionary<String, IList<String>>> MatchNamedCapturesAsync(this Regex regex, String input, Boolean nogroup = true)
        {
            return MatchNamedCapturesAsync(regex, input, nogroup, CancellationToken.None);
        }

        public static async Task<IDictionary<String, IList<String>>> MatchNamedCapturesAsync(this Regex regex, String input, Boolean nogroup, CancellationToken token)
        {
            if (regex is null)
            {
                throw new ArgumentNullException(nameof(regex));
            }

            MatchCollection matches = await MatchesAsync(regex, input, token).ConfigureAwait(false);
            String[] groupNames = regex.GetGroupNames();

            return MatchNamedCaptures(matches, groupNames, nogroup);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public static IDictionary<String, IList<String>> MatchNamedCaptures(this MatchCollection matches, IEnumerable<String> names, Boolean nogroup)
        {
            if (matches is null)
            {
                throw new ArgumentNullException(nameof(matches));
            }

            IDictionary<String, IList<String>> captures = new Dictionary<String, IList<String>>();
            names = names.Materialize();
            foreach (Match match in matches)
            {
                GroupCollection groups = match.Groups;

                foreach (String? name in names)
                {
                    if (name is null)
                    {
                        continue;
                    }

                    if (nogroup && Int32.TryParse(name, out _) || groups[name].Captures.Count <= 0)
                    {
                        continue;
                    }

                    captures.GetOrAdd(name, () => new List<String>()).Add(groups[name].Value);
                }
            }

            return captures.ToImmutableDictionary();
        }

        public static Task<Match> MatchAsync(String input, String pattern)
        {
            return MatchAsync(input, pattern, CancellationToken.None);
        }

        public static Task<Match> MatchAsync(String input, String pattern, CancellationToken token)
        {
            return Task.Run(() => Regex.Match(input, pattern), token);
        }

        public static Task<Match> MatchAsync(String input, String pattern, RegexOptions options)
        {
            return MatchAsync(input, pattern, options, CancellationToken.None);
        }

        public static Task<Match> MatchAsync(String input, String pattern, RegexOptions options, CancellationToken token)
        {
            return Task.Run(() => Regex.Match(input, pattern, options), token);
        }

        public static Task<Match> MatchAsync(String input, String pattern, RegexOptions options, TimeSpan matchTimeout)
        {
            return MatchAsync(input, pattern, options, matchTimeout, CancellationToken.None);
        }

        public static Task<Match> MatchAsync(String input, String pattern, RegexOptions options, TimeSpan matchTimeout, CancellationToken token)
        {
            return Task.Run(() => Regex.Match(input, pattern, options, matchTimeout), token);
        }

        public static Task<Match> MatchAsync(this Regex regex, String input)
        {
            return MatchAsync(regex, input, CancellationToken.None);
        }

        public static Task<Match> MatchAsync(this Regex regex, String input, CancellationToken token)
        {
            if (regex is null)
            {
                throw new ArgumentNullException(nameof(regex));
            }

            return Task.Run(() => regex.Match(input), token);
        }

        public static Task<Match> MatchAsync(this Regex regex, String input, Int32 startat)
        {
            return MatchAsync(regex, input, startat, CancellationToken.None);
        }

        public static Task<Match> MatchAsync(this Regex regex, String input, Int32 startat, CancellationToken token)
        {
            if (regex is null)
            {
                throw new ArgumentNullException(nameof(regex));
            }

            return Task.Run(() => regex.Match(input, startat), token);
        }

        public static Task<Match> MatchAsync(this Regex regex, String input, Int32 beginning, Int32 length)
        {
            return MatchAsync(regex, input, beginning, length, CancellationToken.None);
        }

        public static Task<Match> MatchAsync(this Regex regex, String input, Int32 beginning, Int32 length, CancellationToken token)
        {
            if (regex is null)
            {
                throw new ArgumentNullException(nameof(regex));
            }

            return Task.Run(() => regex.Match(input, beginning, length), token);
        }

        public static Task<MatchCollection> MatchesAsync(String input, String pattern)
        {
            return MatchesAsync(input, pattern, CancellationToken.None);
        }

        public static Task<MatchCollection> MatchesAsync(String input, String pattern, CancellationToken token)
        {
            return Task.Run(() => Regex.Matches(input, pattern), token);
        }

        public static Task<MatchCollection> MatchesAsync(String input, String pattern, RegexOptions options)
        {
            return MatchesAsync(input, pattern, options, CancellationToken.None);
        }

        public static Task<MatchCollection> MatchesAsync(String input, String pattern, RegexOptions options, CancellationToken token)
        {
            return Task.Run(() => Regex.Matches(input, pattern, options), token);
        }

        public static Task<MatchCollection> MatchesAsync(String input, String pattern, RegexOptions options, TimeSpan matchTimeout)
        {
            return MatchesAsync(input, pattern, options, matchTimeout, CancellationToken.None);
        }

        public static Task<MatchCollection> MatchesAsync(String input, String pattern, RegexOptions options, TimeSpan matchTimeout, CancellationToken token)
        {
            return Task.Run(() => Regex.Matches(input, pattern, options, matchTimeout), token);
        }

        public static Task<MatchCollection> MatchesAsync(this Regex regex, String input)
        {
            return MatchesAsync(regex, input, CancellationToken.None);
        }

        public static Task<MatchCollection> MatchesAsync(this Regex regex, String input, CancellationToken token)
        {
            if (regex is null)
            {
                throw new ArgumentNullException(nameof(regex));
            }

            return Task.Run(() => regex.Matches(input), token);
        }

        public static Task<MatchCollection> MatchesAsync(this Regex regex, String input, Int32 startat)
        {
            return MatchesAsync(regex, input, startat, CancellationToken.None);
        }

        public static Task<MatchCollection> MatchesAsync(this Regex regex, String input, Int32 startat, CancellationToken token)
        {
            if (regex is null)
            {
                throw new ArgumentNullException(nameof(regex));
            }

            return Task.Run(() => regex.Matches(input, startat), token);
        }

        public static Task<String> ReplaceAsync(String input, String pattern, MatchEvaluator evaluator)
        {
            return ReplaceAsync(input, pattern, evaluator, CancellationToken.None);
        }

        public static Task<String> ReplaceAsync(String input, String pattern, MatchEvaluator evaluator, CancellationToken token)
        {
            return Task.Run(() => Regex.Replace(input, pattern, evaluator), token);
        }

        public static Task<String> ReplaceAsync(String input, String pattern, MatchEvaluator evaluator, RegexOptions options)
        {
            return ReplaceAsync(input, pattern, evaluator, options, CancellationToken.None);
        }

        public static Task<String> ReplaceAsync(String input, String pattern, MatchEvaluator evaluator, RegexOptions options, CancellationToken token)
        {
            return Task.Run(() => Regex.Replace(input, pattern, evaluator, options), token);
        }

        public static Task<String> ReplaceAsync(String input, String pattern, MatchEvaluator evaluator, RegexOptions options, TimeSpan matchTimeout)
        {
            return ReplaceAsync(input, pattern, evaluator, options, matchTimeout, CancellationToken.None);
        }

        public static Task<String> ReplaceAsync(String input, String pattern, MatchEvaluator evaluator, RegexOptions options, TimeSpan matchTimeout, CancellationToken token)
        {
            return Task.Run(() => Regex.Replace(input, pattern, evaluator, options, matchTimeout), token);
        }

        public static Task<String> ReplaceAsync(String input, String pattern, String replacement)
        {
            return ReplaceAsync(input, pattern, replacement, CancellationToken.None);
        }

        public static Task<String> ReplaceAsync(String input, String pattern, String replacement, CancellationToken token)
        {
            return Task.Run(() => Regex.Replace(input, pattern, replacement), token);
        }

        public static Task<String> ReplaceAsync(String input, String pattern, String replacement, RegexOptions options)
        {
            return ReplaceAsync(input, pattern, replacement, options, CancellationToken.None);
        }

        public static Task<String> ReplaceAsync(String input, String pattern, String replacement, RegexOptions options, CancellationToken token)
        {
            return Task.Run(() => Regex.Replace(input, pattern, replacement, options), token);
        }

        public static Task<String> ReplaceAsync(String input, String pattern, String replacement, RegexOptions options, TimeSpan matchTimeout)
        {
            return ReplaceAsync(input, pattern, replacement, options, matchTimeout, CancellationToken.None);
        }

        public static Task<String> ReplaceAsync(String input, String pattern, String replacement, RegexOptions options, TimeSpan matchTimeout, CancellationToken token)
        {
            return Task.Run(() => Regex.Replace(input, pattern, replacement, options, matchTimeout), token);
        }

        public static Task<String> ReplaceAsync(this Regex regex, String input, MatchEvaluator evaluator)
        {
            return ReplaceAsync(regex, input, evaluator, CancellationToken.None);
        }

        public static Task<String> ReplaceAsync(this Regex regex, String input, MatchEvaluator evaluator, CancellationToken token)
        {
            if (regex is null)
            {
                throw new ArgumentNullException(nameof(regex));
            }

            return Task.Run(() => regex.Replace(input, evaluator), token);
        }

        public static Task<String> ReplaceAsync(this Regex regex, String input, MatchEvaluator evaluator, Int32 count)
        {
            return ReplaceAsync(regex, input, evaluator, count, CancellationToken.None);
        }

        public static Task<String> ReplaceAsync(this Regex regex, String input, MatchEvaluator evaluator, Int32 count, CancellationToken token)
        {
            if (regex is null)
            {
                throw new ArgumentNullException(nameof(regex));
            }

            return Task.Run(() => regex.Replace(input, evaluator, count), token);
        }

        public static Task<String> ReplaceAsync(this Regex regex, String input, MatchEvaluator evaluator, Int32 count, Int32 startat)
        {
            return ReplaceAsync(regex, input, evaluator, count, startat, CancellationToken.None);
        }

        public static Task<String> ReplaceAsync(this Regex regex, String input, MatchEvaluator evaluator, Int32 count, Int32 startat, CancellationToken token)
        {
            if (regex is null)
            {
                throw new ArgumentNullException(nameof(regex));
            }

            return Task.Run(() => regex.Replace(input, evaluator, count, startat), token);
        }

        public static Task<String> ReplaceAsync(this Regex regex, String input, String replacement)
        {
            return ReplaceAsync(regex, input, replacement, CancellationToken.None);
        }

        public static Task<String> ReplaceAsync(this Regex regex, String input, String replacement, CancellationToken token)
        {
            if (regex is null)
            {
                throw new ArgumentNullException(nameof(regex));
            }

            return Task.Run(() => regex.Replace(input, replacement), token);
        }

        public static Task<String> ReplaceAsync(this Regex regex, String input, String replacement, Int32 count)
        {
            return ReplaceAsync(regex, input, replacement, count, CancellationToken.None);
        }

        public static Task<String> ReplaceAsync(this Regex regex, String input, String replacement, Int32 count, CancellationToken token)
        {
            if (regex is null)
            {
                throw new ArgumentNullException(nameof(regex));
            }

            return Task.Run(() => regex.Replace(input, replacement, count), token);
        }

        public static Task<String> ReplaceAsync(this Regex regex, String input, String replacement, Int32 count, Int32 startat)
        {
            return ReplaceAsync(regex, input, replacement, count, startat, CancellationToken.None);
        }

        public static Task<String> ReplaceAsync(this Regex regex, String input, String replacement, Int32 count, Int32 startat, CancellationToken token)
        {
            if (regex is null)
            {
                throw new ArgumentNullException(nameof(regex));
            }

            return Task.Run(() => regex.Replace(input, replacement, count, startat), token);
        }

        public static Task<String[]> SplitAsync(String input, String pattern)
        {
            return SplitAsync(input, pattern, CancellationToken.None);
        }

        public static Task<String[]> SplitAsync(String input, String pattern, CancellationToken token)
        {
            return Task.Run(() => Regex.Split(input, pattern), token);
        }

        public static Task<String[]> SplitAsync(String input, String pattern, RegexOptions options)
        {
            return SplitAsync(input, pattern, options, CancellationToken.None);
        }

        public static Task<String[]> SplitAsync(String input, String pattern, RegexOptions options, CancellationToken token)
        {
            return Task.Run(() => Regex.Split(input, pattern, options), token);
        }

        public static Task<String[]> SplitAsync(String input, String pattern, RegexOptions options, TimeSpan matchTimeout)
        {
            return SplitAsync(input, pattern, options, matchTimeout, CancellationToken.None);
        }

        public static Task<String[]> SplitAsync(String input, String pattern, RegexOptions options, TimeSpan matchTimeout, CancellationToken token)
        {
            return Task.Run(() => Regex.Split(input, pattern, options, matchTimeout), token);
        }

        public static Task<String[]> SplitAsync(this Regex regex, String input)
        {
            return SplitAsync(regex, input, CancellationToken.None);
        }

        public static Task<String[]> SplitAsync(this Regex regex, String input, CancellationToken token)
        {
            if (regex is null)
            {
                throw new ArgumentNullException(nameof(regex));
            }

            return Task.Run(() => regex.Split(input), token);
        }

        public static Task<String[]> SplitAsync(this Regex regex, String input, Int32 startat)
        {
            return SplitAsync(regex, input, startat, CancellationToken.None);
        }

        public static Task<String[]> SplitAsync(this Regex regex, String input, Int32 startat, CancellationToken token)
        {
            if (regex is null)
            {
                throw new ArgumentNullException(nameof(regex));
            }

            return Task.Run(() => regex.Split(input, startat), token);
        }

        public static Task<String[]> SplitAsync(this Regex regex, String input, Int32 count, Int32 startat)
        {
            return SplitAsync(regex, input, count, startat, CancellationToken.None);
        }

        public static Task<String[]> SplitAsync(this Regex regex, String input, Int32 count, Int32 startat, CancellationToken token)
        {
            if (regex is null)
            {
                throw new ArgumentNullException(nameof(regex));
            }

            return Task.Run(() => regex.Split(input, count, startat), token);
        }

        public static Task<Boolean> IsMatchAsync(String input, String pattern)
        {
            return IsMatchAsync(input, pattern, CancellationToken.None);
        }

        public static Task<Boolean> IsMatchAsync(String input, String pattern, CancellationToken token)
        {
            return Task.Run(() => Regex.IsMatch(input, pattern), token);
        }

        public static Task<Boolean> IsMatchAsync(String input, String pattern, RegexOptions options)
        {
            return IsMatchAsync(input, pattern, options, CancellationToken.None);
        }

        public static Task<Boolean> IsMatchAsync(String input, String pattern, RegexOptions options, CancellationToken token)
        {
            return Task.Run(() => Regex.IsMatch(input, pattern, options), token);
        }

        public static Task<Boolean> IsMatchAsync(String input, String pattern, RegexOptions options, TimeSpan matchTimeout)
        {
            return IsMatchAsync(input, pattern, options, matchTimeout, CancellationToken.None);
        }

        public static Task<Boolean> IsMatchAsync(String input, String pattern, RegexOptions options, TimeSpan matchTimeout, CancellationToken token)
        {
            return Task.Run(() => Regex.IsMatch(input, pattern, options, matchTimeout), token);
        }

        public static Task<Boolean> IsMatchAsync(this Regex regex, String input)
        {
            return IsMatchAsync(regex, input, CancellationToken.None);
        }

        public static Task<Boolean> IsMatchAsync(this Regex regex, String input, CancellationToken token)
        {
            if (regex is null)
            {
                throw new ArgumentNullException(nameof(regex));
            }

            return Task.Run(() => regex.IsMatch(input), token);
        }

        public static Task<Boolean> IsMatchAsync(this Regex regex, String input, Int32 startat)
        {
            return IsMatchAsync(regex, input, startat, CancellationToken.None);
        }

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