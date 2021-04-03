// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace NetExtender.Utils.AspNetCore.Types
{
    public static class HttpRequestUtils
    {
        [CanBeNull]
        public static String? GetUserAgent([NotNull] this HttpRequest request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return request.Headers.TryGetValue("User-Agent", out StringValues agent) ? (String) agent : null;
        }
    }
}