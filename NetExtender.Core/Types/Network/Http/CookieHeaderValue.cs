// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Text;
using NetExtender.Interfaces;

namespace NetExtender.Types.Network
{
    public class CookieHeaderValue : ICloneable<CookieHeaderValue>
    {
        public String? Path { get; set; }
        public String? Domain { get; set; }
        public Boolean Secure { get; set; }
        public Boolean HttpOnly { get; set; }
        public DateTimeOffset? Expires { get; set; }
        public TimeSpan? MaximumAge { get; set; }

        private Collection<CookieState>? _cookies;

        public Collection<CookieState> Cookies
        {
            get
            {
                return _cookies ??= new Collection<CookieState>();
            }
        }

        protected internal CookieHeaderValue()
        {
        }

        public CookieHeaderValue(String key, String value)
        {
            Cookies.Add(new CookieState(key, value));
        }

        public CookieHeaderValue(String key, NameValueCollection values)
        {
            Cookies.Add(new CookieState(key, values));
        }

        private CookieHeaderValue(CookieHeaderValue source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            Path = source.Path;
            Domain = source.Domain;
            Secure = source.Secure;
            HttpOnly = source.HttpOnly;
            Expires = source.Expires;
            MaximumAge = source.MaximumAge;

            foreach (CookieState cookie in source.Cookies)
            {
                Cookies.Add(cookie.Clone());
            }
        }

        public static Boolean TryParse(String? input, [MaybeNullWhen(false)] out CookieHeaderValue result)
        {
            return CookieHeaderValueBuilder.Default.TryParse(input, out result);
        }

        public virtual CookieHeaderValue Clone()
        {
            return new CookieHeaderValue(this);
        }

        public override String ToString()
        {
            static void AppendSegment(StringBuilder builder, ref Boolean first, String key, String? value)
            {
                if (!first)
                {
                    builder.Append(';').Append(' ');
                }

                first = false;
                builder.Append(key);
                if (value is not null)
                {
                    builder.Append('=').Append(value);
                }
            }

            StringBuilder builder = new StringBuilder();
            Boolean first = true;
            foreach (CookieState cookie in Cookies)
            {
                AppendSegment(builder, ref first, cookie.ToString(), null);
            }

            DateTimeOffset? expires = Expires;
            if (expires is not null)
            {
                String value = expires.Value.ToUniversalTime().ToString("r", CultureInfo.InvariantCulture);
                AppendSegment(builder, ref first, "expires", value);
            }

            TimeSpan? age = MaximumAge;
            if (age is not null)
            {
                String value = ((Int32) age.Value.TotalSeconds).ToString(NumberFormatInfo.InvariantInfo);
                AppendSegment(builder, ref first, "max-age", value);
            }

            if (Domain is not null)
            {
                AppendSegment(builder, ref first, "domain", Domain);
            }

            if (Path is not null)
            {
                AppendSegment(builder, ref first, "path", Path);
            }

            if (Secure)
            {
                AppendSegment(builder, ref first, "secure", null);
            }

            if (HttpOnly)
            {
                AppendSegment(builder, ref first, "httponly", null);
            }

            return builder.ToString();
        }

        public CookieState? this[String? key]
        {
            get
            {
                if (String.IsNullOrEmpty(key))
                {
                    return null;
                }

                CookieState? cookie = Cookies.FirstOrDefault((Func<CookieState, Boolean>) (state => String.Equals(state.Key, key, StringComparison.OrdinalIgnoreCase)));
                if (cookie is not null)
                {
                    return cookie;
                }

                cookie = new CookieState(key, String.Empty);
                Cookies.Add(cookie);
                return cookie;
            }
        }
    }
}