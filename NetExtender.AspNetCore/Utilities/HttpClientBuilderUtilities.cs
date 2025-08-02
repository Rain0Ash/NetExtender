using System;
using System.Net.Http;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NetExtender.Utilities.Network;

namespace NetExtender.Utilities.AspNetCore.Types
{
    public static class HttpClientBuilderUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IHttpClientBuilder AddHttpTracing(this IHttpClientBuilder builder, Predicate<HttpResponseMessage>? validator)
        {
            return AddHttpTracing(builder, (ILogger?) null, validator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IHttpClientBuilder AddHttpTracing(this IHttpClientBuilder builder, ILogger? logger, Predicate<HttpResponseMessage>? validator)
        {
            return AddHttpTracing(builder, logger, validator, false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IHttpClientBuilder AddHttpTracing(this IHttpClientBuilder builder, Predicate<HttpResponseMessage>? validator, Boolean buffering)
        {
            return AddHttpTracing(builder, (ILogger?) null, validator, buffering);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IHttpClientBuilder AddHttpTracing(this IHttpClientBuilder builder, ILogger? logger, Predicate<HttpResponseMessage>? validator, Boolean buffering)
        {
            return AddHttpTracing(builder, logger, null, validator, buffering);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IHttpClientBuilder AddHttpTracing(this IHttpClientBuilder builder, String? category, Predicate<HttpResponseMessage>? validator)
        {
            return AddHttpTracing(builder, null, category, validator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IHttpClientBuilder AddHttpTracing(this IHttpClientBuilder builder, ILogger? logger, String? category, Predicate<HttpResponseMessage>? validator)
        {
            return AddHttpTracing(builder, logger, category, validator, false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IHttpClientBuilder AddHttpTracing(this IHttpClientBuilder builder, String? category, Predicate<HttpResponseMessage>? validator, Boolean buffering)
        {
            return AddHttpTracing(builder, null, category, validator, buffering);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IHttpClientBuilder AddHttpTracing(this IHttpClientBuilder builder, ILogger? logger, String? category, Predicate<HttpResponseMessage>? validator, Boolean buffering)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (String.IsNullOrEmpty(category))
            {
                category = HttpTracingHandler.LoggerCategory(builder.Name);
            }

            return builder.AddHttpMessageHandler(provider => new HttpTracingHandler(logger ?? provider.GetRequiredService<ILoggerFactory>().CreateLogger(category), validator, buffering));
        }
    }
}