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
using System.Net.Sockets;
using System.Runtime.CompilerServices;
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
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static HttpResponseMessage CreateResponse(this HttpRequestMessage request)
        {
            return CreateResponse<HttpResponseMessage>(request);
        }
        
        public static T CreateResponse<T>(this HttpRequestMessage request) where T : HttpResponseMessage, new()
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return new T
            {
                Content = new StreamContent(Stream.Null),
                RequestMessage = request
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static HttpResponseMessage CreateResponse(this HttpRequestMessage request, HttpStatusCode code)
        {
            return CreateResponse<HttpResponseMessage>(request, code);
        }

        public static T CreateResponse<T>(this HttpRequestMessage request, HttpStatusCode code) where T : HttpResponseMessage, new()
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return new T
            {
                Content = new StreamContent(Stream.Null),
                StatusCode = code,
                RequestMessage = request
            };
        }

        public static IOException? IOException(this HttpRequestException exception)
        {
            if (exception is null)
            {
                throw new ArgumentNullException(nameof(exception));
            }

            return exception.StatusCode is null && exception.InnerException is IOException io ? io : null;
        }

        public static Boolean IsIOException(this HttpRequestException exception)
        {
            if (exception is null)
            {
                throw new ArgumentNullException(nameof(exception));
            }

            return IOException(exception) is not null;
        }

        public static Boolean IsIOException(this HttpRequestException exception, [MaybeNullWhen(false)] out IOException result)
        {
            if (exception is null)
            {
                throw new ArgumentNullException(nameof(exception));
            }

            result = IOException(exception);
            return result is not null;
        }

        public static SocketException? SocketException(this HttpRequestException exception)
        {
            if (exception is null)
            {
                throw new ArgumentNullException(nameof(exception));
            }

            return exception.StatusCode is null && exception.InnerException is SocketException socket ? socket : null;
        }

        public static Boolean IsSocketException(this HttpRequestException exception)
        {
            if (exception is null)
            {
                throw new ArgumentNullException(nameof(exception));
            }

            return SocketException(exception) is not null;
        }

        public static Boolean IsSocketException(this HttpRequestException exception, [MaybeNullWhen(false)] out SocketException result)
        {
            if (exception is null)
            {
                throw new ArgumentNullException(nameof(exception));
            }

            result = SocketException(exception);
            return result is not null;
        }
    }
}