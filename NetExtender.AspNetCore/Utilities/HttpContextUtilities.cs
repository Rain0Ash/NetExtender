// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace NetExtender.Utilities.AspNetCore.Types
{
    public static class HttpContextUtilities
    {
        public static Task RejectAsync(this HttpContext context)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            context.Response.SetStatusCode(HttpStatusCode.ServiceUnavailable);
            context.Abort();
            return Task.CompletedTask;
        }
        
        private const String NoCache = "no-cache";
        private const String NoCacheMaxAge = "no-cache,max-age=";
        private const String NoStore = "no-store";
        private const String NoStoreNoCache = "no-store,no-cache";
        private const String PublicMaxAge = "public,max-age=";
        private const String PrivateMaxAge = "private,max-age=";

        public static HttpContext ApplyCacheProfile(this HttpContext context, CacheProfile profile)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (profile is null)
            {
                throw new ArgumentNullException(nameof(profile));
            }

            IHeaderDictionary headers = context.Response.Headers;

            if (!String.IsNullOrEmpty(profile.VaryByHeader))
            {
                headers[HeaderNames.Vary] = profile.VaryByHeader;
            }

            if (profile.NoStore == true)
            {
                if (profile.Location != ResponseCacheLocation.None)
                {
                    headers[HeaderNames.CacheControl] = NoStore;
                    return context;
                }

                headers[HeaderNames.CacheControl] = NoStoreNoCache;
                headers[HeaderNames.Pragma] = NoCache;
                return context;
            }

            String duration = profile.Duration.GetValueOrDefault().ToString(CultureInfo.InvariantCulture);
            
            switch (profile.Location)
            {
                case ResponseCacheLocation.Any:
                    headers[HeaderNames.CacheControl] = PublicMaxAge + duration;
                    return context;
                case ResponseCacheLocation.Client:
                    headers[HeaderNames.CacheControl] = PrivateMaxAge + duration;
                    return context;
                case ResponseCacheLocation.None:
                    headers[HeaderNames.CacheControl] = NoCacheMaxAge + duration;
                    headers[HeaderNames.Pragma] = NoCache;
                    return context;
                default:
                    throw new InvalidOperationException($"Unknown {nameof(ResponseCacheLocation)}: {profile.Location}");
            }
        }
    }
}