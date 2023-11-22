// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using NetExtender.Types.Network;

namespace NetExtender.Utilities.Network
{
    public static class HttpResponseUtilities
    {
        public static void AddCookies(this HttpResponseHeaders source, IEnumerable<CookieHeaderValue?>? values)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (values is null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            foreach (CookieHeaderValue? cookie in values)
            {
                if (cookie is null)
                {
                    continue;
                }

                source.TryAddWithoutValidation("Set-Cookie", cookie.ToString());
            }
        }
    }
}