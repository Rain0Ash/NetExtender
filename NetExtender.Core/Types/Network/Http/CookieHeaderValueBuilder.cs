// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;

namespace NetExtender.Types.Network
{
    public class CookieHeaderValueBuilder
    {
        [SuppressMessage("ReSharper", "MemberHidesStaticFromOuterClass")]
        protected static class Parser
        {
            private static String[] Formats { get; } =
            {
                "ddd, d MMM yyyy H:m:s 'GMT'",
                "ddd, d MMM yyyy H:m:s",
                "d MMM yyyy H:m:s 'GMT'",
                "d MMM yyyy H:m:s",
                "ddd, d MMM yy H:m:s 'GMT'",
                "ddd, d MMM yy H:m:s",
                "d MMM yy H:m:s 'GMT'",
                "d MMM yy H:m:s",
                "dddd, d'-'MMM'-'yy H:m:s 'GMT'",
                "dddd, d'-'MMM'-'yy H:m:s",
                "ddd, d'-'MMM'-'yyyy H:m:s 'GMT'",
                "ddd MMM d H:m:s yyyy",
                "ddd, d MMM yyyy H:m:s zzz",
                "ddd, d MMM yyyy H:m:s",
                "d MMM yyyy H:m:s zzz",
                "d MMM yyyy H:m:s"
            };

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static Boolean TryParseExact(String? input, IFormatProvider? provider, DateTimeStyles styles, out DateTimeOffset result)
            {
                return DateTimeOffset.TryParseExact(input, Formats, provider, styles, out result);
            }
        }
        
        public static CookieHeaderValueBuilder Default { get; } = new CookieHeaderValueBuilder();
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual Boolean TryParseExact(String? input, out DateTimeOffset result)
        {
            return Parser.TryParseExact(input, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.AllowWhiteSpaces | DateTimeStyles.AssumeUniversal, out result);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual Boolean TryParseExact(String? input, IFormatProvider? provider, DateTimeStyles styles, out DateTimeOffset result)
        {
            return Parser.TryParseExact(input, provider, styles, out result);
        }
        
        public virtual Boolean TryParse(String? input, [MaybeNullWhen(false)] out CookieHeaderValue result)
        {
            if (String.IsNullOrEmpty(input))
            {
                result = default;
                return false;
            }

            CookieHeaderValue instance = new CookieHeaderValue();
            if (input.Split(';').All(segment => ParseCookieSegment(instance, segment)) && instance.Cookies.Count > 0)
            {
                result = instance;
                return true;
            }

            result = default;
            return false;
        }

        protected virtual Boolean ParseCookieSegment(CookieHeaderValue instance, String? segment)
        {
            if (instance is null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            if (String.IsNullOrWhiteSpace(segment))
            {
                return true;
            }

            String[] pair = segment.Split('=', 2);
            if (pair.Length < 1 || String.IsNullOrWhiteSpace(pair[0]))
            {
                return false;
            }

            static String? GetSegmentValue(IReadOnlyList<String>? pair, String? alternate)
            {
                [return: NotNullIfNotNull("token")]
                static String? UnquoteToken(String? token)
                {
                    if (!String.IsNullOrWhiteSpace(token) && token.Length > 1 && token.StartsWith("\"", StringComparison.Ordinal) && token.EndsWith("\"", StringComparison.Ordinal))
                    {
                        return token.Substring(1, token.Length - 2);
                    }

                    return token;
                }

                return pair?.Count > 1 ? UnquoteToken(pair[1]) : alternate;
            }

            String value = pair[0].Trim();
            if (String.Equals(value, "expires", StringComparison.OrdinalIgnoreCase))
            {
                if (!TryParseExact(GetSegmentValue(pair, null), out DateTimeOffset result))
                {
                    return false;
                }

                instance.Expires = result;
                return true;
            }

            if (String.Equals(value, "max-age", StringComparison.OrdinalIgnoreCase))
            {
                if (!Int32.TryParse(GetSegmentValue(pair, null), NumberStyles.None, NumberFormatInfo.InvariantInfo, out Int32 result))
                {
                    return false;
                }

                instance.MaximumAge = new TimeSpan(0, 0, result);
                return true;
            }

            if (String.Equals(value, "domain", StringComparison.OrdinalIgnoreCase))
            {
                instance.Domain = GetSegmentValue(pair, null);
                return true;
            }

            if (String.Equals(value, "path", StringComparison.OrdinalIgnoreCase))
            {
                instance.Path = GetSegmentValue(pair, "/");
                return true;
            }

            if (String.Equals(value, "secure", StringComparison.OrdinalIgnoreCase))
            {
                if (!String.IsNullOrWhiteSpace(GetSegmentValue(pair, null)))
                {
                    return false;
                }

                instance.Secure = true;
                return true;
            }

            if (String.Equals(value, "httponly", StringComparison.OrdinalIgnoreCase))
            {
                if (!String.IsNullOrWhiteSpace(GetSegmentValue(pair, null)))
                {
                    return false;
                }

                instance.HttpOnly = true;
                return true;
            }
            
            try
            {
                NameValueCollection values = new FormDataCollection(GetSegmentValue(pair, null));
                CookieState cookie = new CookieState(value, values);
                instance.Cookies.Add(cookie);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}