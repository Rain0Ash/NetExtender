// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Net;
using System.Net.Mime;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using NetExtender.Types.Exceptions;

namespace NetExtender.Utilities.AspNetCore.Types
{
    public static class HttpContextUtilities
    {
        private const String NoCache = "no-cache";
        private const String NoCacheMaxAge = "no-cache,max-age=";
        private const String NoStore = "no-store";
        private const String NoStoreNoCache = "no-store,no-cache";
        private const String PublicMaxAge = "public,max-age=";
        private const String PrivateMaxAge = "private,max-age=";

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String? Authorization(this HttpContext context)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return Authorization(context, out String? authorization) ? authorization : null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Authorization(this HttpContext context, [MaybeNullWhen(false)] out String authorization)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            
            context.Request.Headers.TryGetValue("Authorization", out StringValues headers);
            return (authorization = headers) is { Length: > 0 };
        }

        public static async ValueTask Exception(this HttpContext context, BusinessException exception)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (exception is null)
            {
                throw new ArgumentNullException(nameof(exception));
            }

            context.Response.ContentType = MediaTypeNames.Application.Json;
            context.Response.StatusCode = (Int32) (exception.Status ?? HttpStatusCode.InternalServerError);
            JsonSerializerOptions? options = context.RequestServices.GetService<IOptions<JsonOptions>>()?.Value.JsonSerializerOptions;
            await context.Response.WriteAsJsonAsync(exception.Business, options).ConfigureAwait(false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ValueTask Exception<T>(this HttpContext context) where T : BusinessException, new()
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            
            return Exception(context, new T());
        }
        
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

            if (profile.NoStore is true)
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