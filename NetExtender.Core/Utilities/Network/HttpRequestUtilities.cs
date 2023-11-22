// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using NetExtender.Types.Network;
using NetExtender.Utilities.Types;

namespace NetExtender.Utilities.Network
{
    public static class HttpRequestUtilities
    {
        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public static CookieHeaderValue[] GetCookies(this HttpRequestHeaders source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (!source.TryGetValues("Cookie", out IEnumerable<String>? values))
            {
                return Array.Empty<CookieHeaderValue>();
            }

            List<CookieHeaderValue> cookies = new List<CookieHeaderValue>(values.CountIfMaterialized() ?? 16);
            foreach (String input in values)
            {
                if (CookieHeaderValue.TryParse(input, out CookieHeaderValue? result))
                {
                    cookies.Add(result);
                }
            }

            return cookies.ToArray();
        }

        public static CookieHeaderValue[] GetCookies(this HttpRequestHeaders source, String key)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return source.GetCookies().Where(header => header.Cookies.Any(state => String.Equals(state.Key, key, StringComparison.OrdinalIgnoreCase))).ToArray();
        }
        
        public static HttpResponseMessage CreateResponse(this HttpRequestMessage request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return new HttpResponseMessage
            {
                Content = new StreamContent(Stream.Null),
                RequestMessage = request
            };
        }

        public static HttpResponseMessage CreateResponse(this HttpRequestMessage request, HttpStatusCode code)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return new HttpResponseMessage
            {
                Content = new StreamContent(Stream.Null),
                StatusCode = code,
                RequestMessage = request
            };
        }
    }
}