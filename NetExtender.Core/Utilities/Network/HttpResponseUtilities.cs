// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using NetExtender.Types.Network;
using NetExtender.Utilities.Network.Formatters;

namespace NetExtender.Utilities.Network
{
    public static class HttpResponseUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String ToHeaderString(this HttpResponseMessage response)
        {
            if (response is null)
            {
                throw new ArgumentNullException(nameof(response));
            }
            
            return response.Headers.ToHeaderString(response.Content.Headers);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String ToHeaderString(this HttpResponseMessage response, Int32 buffer)
        {
            if (response is null)
            {
                throw new ArgumentNullException(nameof(response));
            }
            
            return response.Headers.ToHeaderString(response.Content.Headers, buffer);
        }
        
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T SetTextContentType<T>(this T response) where T : HttpResponseMessage
        {
            if (response is null)
            {
                throw new ArgumentNullException(nameof(response));
            }

            response.Content.SetTextContentType();
            return response;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T SetTextJsonContentType<T>(this T response) where T : HttpResponseMessage
        {
            if (response is null)
            {
                throw new ArgumentNullException(nameof(response));
            }

            response.Content.SetTextJsonContentType();
            return response;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T SetTextXmlContentType<T>(this T response) where T : HttpResponseMessage
        {
            if (response is null)
            {
                throw new ArgumentNullException(nameof(response));
            }

            response.Content.SetTextXmlContentType();
            return response;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T SetJsonContentType<T>(this T response) where T : HttpResponseMessage
        {
            if (response is null)
            {
                throw new ArgumentNullException(nameof(response));
            }

            response.Content.SetJsonContentType();
            return response;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T SetXmlContentType<T>(this T response) where T : HttpResponseMessage
        {
            if (response is null)
            {
                throw new ArgumentNullException(nameof(response));
            }

            response.Content.SetXmlContentType();
            return response;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T SetContentType<T>(this T response, MediaTypeHeaderValueType type) where T : HttpResponseMessage
        {
            if (response is null)
            {
                throw new ArgumentNullException(nameof(response));
            }

            response.Content.SetContentType(type);
            return response;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T SetContentType<T>(this T response, MediaTypeHeaderValue? type) where T : HttpResponseMessage
        {
            if (response is null)
            {
                throw new ArgumentNullException(nameof(response));
            }

            response.Content.SetContentType(type);
            return response;
        }
    }
}