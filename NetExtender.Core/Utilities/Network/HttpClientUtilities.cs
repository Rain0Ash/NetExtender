// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Types.Network.Formatters;
using NetExtender.Types.Streams;
using NetExtender.Utilities.IO;
using NetExtender.Utilities.Types;

namespace NetExtender.Utilities.Network
{
    public static class HttpClientUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static HttpClient Create()
        {
            return Create(UserAgentUtilities.CurrentSessionUserAgent);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static HttpClient Create(String? agent)
        {
            HttpClient client = new HttpClient();
            return DisposeAndThrowOnInvalidAddUserAgent(client, agent);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static HttpClient Create(this HttpMessageHandler handler)
        {
            if (handler is null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            return Create(handler, UserAgentUtilities.CurrentSessionUserAgent);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static HttpClient Create(params DelegatingHandler[]? handlers)
        {
            if (handlers is null)
            {
                throw new ArgumentNullException(nameof(handlers));
            }

            return Create(new HttpClientHandler(), handlers);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static HttpClient Create(String? agent, params DelegatingHandler[]? handlers)
        {
            if (handlers is null)
            {
                throw new ArgumentNullException(nameof(handlers));
            }

            return Create(new HttpClientHandler(), agent, handlers);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static HttpClient Create(this HttpMessageHandler handler, String? agent)
        {
            if (handler is null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            HttpClient client = new HttpClient(handler);
            return DisposeAndThrowOnInvalidAddUserAgent(client, agent);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static HttpClient Create(this HttpMessageHandler handler, params DelegatingHandler[]? handlers)
        {
            if (handler is null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            return new HttpClient(Pipeline(handler, handlers));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static HttpClient Create(this HttpMessageHandler handler, String? agent, params DelegatingHandler[]? handlers)
        {
            if (handler is null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            HttpClient client = new HttpClient(Pipeline(handler, handlers));
            return DisposeAndThrowOnInvalidAddUserAgent(client, agent);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static HttpClient Create(this HttpMessageHandler handler, Boolean dispose)
        {
            if (handler is null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            return Create(handler, UserAgentUtilities.CurrentSessionUserAgent, dispose);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static HttpClient Create(this HttpMessageHandler handler, String? agent, Boolean dispose)
        {
            if (handler is null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            HttpClient client = new HttpClient(handler, dispose);
            return DisposeAndThrowOnInvalidAddUserAgent(client, agent);
        }

        public static HttpMessageHandler Pipeline(this HttpMessageHandler handler, IEnumerable<DelegatingHandler?>? handlers)
        {
            if (handler is null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            if (handlers is null)
            {
                return handler;
            }

            HttpMessageHandler pipeline = handler;
            foreach (DelegatingHandler? @delegate in handlers.Reverse())
            {
                if (@delegate is null)
                {
                    continue;
                }

                @delegate.InnerHandler = @delegate.InnerHandler is null ? pipeline : throw new InvalidOperationException();
                pipeline = @delegate;
            }
            
            return pipeline;
        }

        public static Boolean AddUserAgentHeader(this HttpClient client, String agent)
        {
            if (client is null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            return UserAgentUtilities.ValidateUserAgent(agent) && client.DefaultRequestHeaders.UserAgent.TryParseAdd(agent);
        }

        private static HttpClient DisposeAndThrowOnInvalidAddUserAgent(HttpClient client, String? agent)
        {
            if (client is null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            if (agent == String.Empty)
            {
                return client;
            }

            if (client.AddUserAgentHeader(agent ?? UserAgentUtilities.CurrentSessionUserAgent))
            {
                return client;
            }

            client.Dispose();
            throw new ArgumentException("Invalid user agent", nameof(agent));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpResponseMessage> PostAsync<T>(this HttpClient client, String requestUri, T value, MediaTypeFormatter formatter)
        {
            return PostAsync(client, requestUri, value, formatter, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpResponseMessage> PostAsync<T>(this HttpClient client, String requestUri, T value, MediaTypeFormatter formatter, CancellationToken token)
        {
            return PostAsync(client, requestUri, value, formatter, (MediaTypeHeaderValue?) null, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpResponseMessage> PostAsync<T>(this HttpClient client, String requestUri, T value, MediaTypeFormatter formatter, String? media)
        {
            return PostAsync(client, requestUri, value, formatter, media, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpResponseMessage> PostAsync<T>(this HttpClient client, String requestUri, T value, MediaTypeFormatter formatter, String? media, CancellationToken token)
        {
            return PostAsync(client, requestUri, value, formatter, MediaTypeContent.BuildHeaderValue(media), token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpResponseMessage> PostAsync<T>(this HttpClient client, String requestUri, T value, MediaTypeFormatter formatter, MediaTypeHeaderValue? media)
        {
            return PostAsync(client, requestUri, value, formatter, media, CancellationToken.None);
        }

        public static Task<HttpResponseMessage> PostAsync<T>(this HttpClient client, String requestUri, T value, MediaTypeFormatter formatter, MediaTypeHeaderValue? media, CancellationToken token)
        {
            if (client is null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            if (requestUri is null)
            {
                throw new ArgumentNullException(nameof(requestUri));
            }

            if (formatter is null)
            {
                throw new ArgumentNullException(nameof(formatter));
            }

            MediaTypeContent content = new MediaTypeContent(value, formatter, media);
            return client.PostAsync(requestUri, content, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpResponseMessage> PostAsync<T>(this HttpClient client, Uri requestUri, T value, MediaTypeFormatter formatter)
        {
            return PostAsync(client, requestUri, value, formatter, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpResponseMessage> PostAsync<T>(this HttpClient client, Uri requestUri, T value, MediaTypeFormatter formatter, CancellationToken token)
        {
            return PostAsync(client, requestUri, value, formatter, (MediaTypeHeaderValue?) null, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpResponseMessage> PostAsync<T>(this HttpClient client, Uri requestUri, T value, MediaTypeFormatter formatter, String? media)
        {
            return PostAsync(client, requestUri, value, formatter, media, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpResponseMessage> PostAsync<T>(this HttpClient client, Uri requestUri, T value, MediaTypeFormatter formatter, String? media, CancellationToken token)
        {
            return PostAsync(client, requestUri, value, formatter, MediaTypeContent.BuildHeaderValue(media), token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpResponseMessage> PostAsync<T>(this HttpClient client, Uri requestUri, T value, MediaTypeFormatter formatter, MediaTypeHeaderValue? media)
        {
            return PostAsync(client, requestUri, value, formatter, media, CancellationToken.None);
        }

        public static Task<HttpResponseMessage> PostAsync<T>(this HttpClient client, Uri requestUri, T value, MediaTypeFormatter formatter, MediaTypeHeaderValue? media, CancellationToken token)
        {
            if (client is null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            if (requestUri is null)
            {
                throw new ArgumentNullException(nameof(requestUri));
            }

            if (formatter is null)
            {
                throw new ArgumentNullException(nameof(formatter));
            }

            MediaTypeContent content = new MediaTypeContent(value, formatter, media);
            return client.PostAsync(requestUri, content, token);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpResponseMessage> PostAsJsonAsync<T>(this HttpClient client, String requestUri, T value)
        {
            return PostAsJsonAsync(client, requestUri, value, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpResponseMessage> PostAsJsonAsync<T>(this HttpClient client, String requestUri, T value, CancellationToken token)
        {
            return PostAsJsonAsync(client, requestUri, value, null, token);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpResponseMessage> PostAsJsonAsync<T>(this HttpClient client, String requestUri, JsonMediaTypeFormatter? formatter, T value)
        {
            return PostAsJsonAsync(client, requestUri, value, formatter, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpResponseMessage> PostAsJsonAsync<T>(this HttpClient client, String requestUri, T value, JsonMediaTypeFormatter? formatter, CancellationToken token)
        {
            return PostAsync(client, requestUri, value, formatter ?? new JsonMediaTypeFormatter(), token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpResponseMessage> PostAsJsonAsync<T>(this HttpClient client, Uri requestUri, T value)
        {
            return PostAsJsonAsync(client, requestUri, value, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpResponseMessage> PostAsJsonAsync<T>(this HttpClient client, Uri requestUri, T value, CancellationToken token)
        {
            return PostAsJsonAsync(client, requestUri, value, null, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpResponseMessage> PostAsJsonAsync<T>(this HttpClient client, Uri requestUri, JsonMediaTypeFormatter? formatter, T value)
        {
            return PostAsJsonAsync(client, requestUri, value, formatter, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpResponseMessage> PostAsJsonAsync<T>(this HttpClient client, Uri requestUri, T value, JsonMediaTypeFormatter? formatter, CancellationToken token)
        {
            return PostAsync(client, requestUri, value, formatter ?? new JsonMediaTypeFormatter(), token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpResponseMessage> PostAsXmlAsync<T>(this HttpClient client, String requestUri, T value)
        {
            return PostAsXmlAsync(client, requestUri, value, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpResponseMessage> PostAsXmlAsync<T>(this HttpClient client, String requestUri, T value, CancellationToken token)
        {
            return PostAsXmlAsync(client, requestUri, value, null, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpResponseMessage> PostAsXmlAsync<T>(this HttpClient client, String requestUri, XmlMediaTypeFormatter? formatter, T value)
        {
            return PostAsXmlAsync(client, requestUri, value, formatter, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpResponseMessage> PostAsXmlAsync<T>(this HttpClient client, String requestUri, T value, XmlMediaTypeFormatter? formatter, CancellationToken token)
        {
            return PostAsync(client, requestUri, value, formatter ?? new XmlMediaTypeFormatter(), token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpResponseMessage> PostAsXmlAsync<T>(this HttpClient client, Uri requestUri, T value)
        {
            return PostAsXmlAsync(client, requestUri, value, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpResponseMessage> PostAsXmlAsync<T>(this HttpClient client, Uri requestUri, T value, CancellationToken token)
        {
            return PostAsXmlAsync(client, requestUri, value, null, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpResponseMessage> PostAsXmlAsync<T>(this HttpClient client, Uri requestUri, T value, XmlMediaTypeFormatter? formatter)
        {
            return PostAsXmlAsync(client, requestUri, value, formatter, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpResponseMessage> PostAsXmlAsync<T>(this HttpClient client, Uri requestUri, T value, XmlMediaTypeFormatter? formatter, CancellationToken token)
        {
            return PostAsync(client, requestUri, value, formatter ?? new XmlMediaTypeFormatter(), token);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpResponseMessage> PutAsync<T>(this HttpClient client, String requestUri, T value, MediaTypeFormatter formatter)
        {
            return PutAsync(client, requestUri, value, formatter, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpResponseMessage> PutAsync<T>(this HttpClient client, String requestUri, T value, MediaTypeFormatter formatter, CancellationToken token)
        {
            return PutAsync(client, requestUri, value, formatter, (MediaTypeHeaderValue?) null, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpResponseMessage> PutAsync<T>(this HttpClient client, String requestUri, T value, MediaTypeFormatter formatter, String? media)
        {
            return PutAsync(client, requestUri, value, formatter, media, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpResponseMessage> PutAsync<T>(this HttpClient client, String requestUri, T value, MediaTypeFormatter formatter, String? media, CancellationToken token)
        {
            return PutAsync(client, requestUri, value, formatter, MediaTypeContent.BuildHeaderValue(media), token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpResponseMessage> PutAsync<T>(this HttpClient client, String requestUri, T value, MediaTypeFormatter formatter, MediaTypeHeaderValue? media)
        {
            return PutAsync(client, requestUri, value, formatter, media, CancellationToken.None);
        }

        public static Task<HttpResponseMessage> PutAsync<T>(this HttpClient client, String requestUri, T value, MediaTypeFormatter formatter, MediaTypeHeaderValue? media, CancellationToken token)
        {
            if (client is null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            if (requestUri is null)
            {
                throw new ArgumentNullException(nameof(requestUri));
            }

            if (formatter is null)
            {
                throw new ArgumentNullException(nameof(formatter));
            }

            MediaTypeContent content = new MediaTypeContent(value, formatter, media);
            return client.PutAsync(requestUri, content, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpResponseMessage> PutAsync<T>(this HttpClient client, Uri requestUri, T value, MediaTypeFormatter formatter)
        {
            return PutAsync(client, requestUri, value, formatter, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpResponseMessage> PutAsync<T>(this HttpClient client, Uri requestUri, T value, MediaTypeFormatter formatter, CancellationToken token)
        {
            return PutAsync(client, requestUri, value, formatter, (MediaTypeHeaderValue?) null, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpResponseMessage> PutAsync<T>(this HttpClient client, Uri requestUri, T value, MediaTypeFormatter formatter, String? media)
        {
            return PutAsync(client, requestUri, value, formatter, media, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpResponseMessage> PutAsync<T>(this HttpClient client, Uri requestUri, T value, MediaTypeFormatter formatter, String? media, CancellationToken token)
        {
            return PutAsync(client, requestUri, value, formatter, MediaTypeContent.BuildHeaderValue(media), token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpResponseMessage> PutAsync<T>(this HttpClient client, Uri requestUri, T value, MediaTypeFormatter formatter, MediaTypeHeaderValue? media)
        {
            return PutAsync(client, requestUri, value, formatter, media, CancellationToken.None);
        }

        public static Task<HttpResponseMessage> PutAsync<T>(this HttpClient client, Uri requestUri, T value, MediaTypeFormatter formatter, MediaTypeHeaderValue? media, CancellationToken token)
        {
            if (client is null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            if (requestUri is null)
            {
                throw new ArgumentNullException(nameof(requestUri));
            }

            if (formatter is null)
            {
                throw new ArgumentNullException(nameof(formatter));
            }

            MediaTypeContent content = new MediaTypeContent(value, formatter, media);
            return client.PutAsync(requestUri, content, token);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpResponseMessage> PutAsJsonAsync<T>(this HttpClient client, String requestUri, T value)
        {
            return PutAsJsonAsync(client, requestUri, value, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpResponseMessage> PutAsJsonAsync<T>(this HttpClient client, String requestUri, T value, CancellationToken token)
        {
            return PutAsJsonAsync(client, requestUri, value, null, token);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpResponseMessage> PutAsJsonAsync<T>(this HttpClient client, String requestUri, JsonMediaTypeFormatter? formatter, T value)
        {
            return PutAsJsonAsync(client, requestUri, value, formatter, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpResponseMessage> PutAsJsonAsync<T>(this HttpClient client, String requestUri, T value, JsonMediaTypeFormatter? formatter, CancellationToken token)
        {
            return PutAsync(client, requestUri, value, formatter ?? new JsonMediaTypeFormatter(), token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpResponseMessage> PutAsJsonAsync<T>(this HttpClient client, Uri requestUri, T value)
        {
            return PutAsJsonAsync(client, requestUri, value, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpResponseMessage> PutAsJsonAsync<T>(this HttpClient client, Uri requestUri, T value, CancellationToken token)
        {
            return PutAsJsonAsync(client, requestUri, value, null, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpResponseMessage> PutAsJsonAsync<T>(this HttpClient client, Uri requestUri, JsonMediaTypeFormatter? formatter, T value)
        {
            return PutAsJsonAsync(client, requestUri, value, formatter, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpResponseMessage> PutAsJsonAsync<T>(this HttpClient client, Uri requestUri, T value, JsonMediaTypeFormatter? formatter, CancellationToken token)
        {
            return PutAsync(client, requestUri, value, formatter ?? new JsonMediaTypeFormatter(), token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpResponseMessage> PutAsXmlAsync<T>(this HttpClient client, String requestUri, T value)
        {
            return PutAsXmlAsync(client, requestUri, value, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpResponseMessage> PutAsXmlAsync<T>(this HttpClient client, String requestUri, T value, CancellationToken token)
        {
            return PutAsXmlAsync(client, requestUri, value, null, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpResponseMessage> PutAsXmlAsync<T>(this HttpClient client, String requestUri, XmlMediaTypeFormatter? formatter, T value)
        {
            return PutAsXmlAsync(client, requestUri, value, formatter, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpResponseMessage> PutAsXmlAsync<T>(this HttpClient client, String requestUri, T value, XmlMediaTypeFormatter? formatter, CancellationToken token)
        {
            return PutAsync(client, requestUri, value, formatter ?? new XmlMediaTypeFormatter(), token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpResponseMessage> PutAsXmlAsync<T>(this HttpClient client, Uri requestUri, T value)
        {
            return PutAsXmlAsync(client, requestUri, value, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpResponseMessage> PutAsXmlAsync<T>(this HttpClient client, Uri requestUri, T value, CancellationToken token)
        {
            return PutAsXmlAsync(client, requestUri, value, null, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpResponseMessage> PutAsXmlAsync<T>(this HttpClient client, Uri requestUri, T value, XmlMediaTypeFormatter? formatter)
        {
            return PutAsXmlAsync(client, requestUri, value, formatter, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpResponseMessage> PutAsXmlAsync<T>(this HttpClient client, Uri requestUri, T value, XmlMediaTypeFormatter? formatter, CancellationToken token)
        {
            return PutAsync(client, requestUri, value, formatter ?? new XmlMediaTypeFormatter(), token);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpResponseMessage> PatchAsync<T>(this HttpClient client, String requestUri, T value, MediaTypeFormatter formatter)
        {
            return PatchAsync(client, requestUri, value, formatter, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpResponseMessage> PatchAsync<T>(this HttpClient client, String requestUri, T value, MediaTypeFormatter formatter, CancellationToken token)
        {
            return PatchAsync(client, requestUri, value, formatter, (MediaTypeHeaderValue?) null, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpResponseMessage> PatchAsync<T>(this HttpClient client, String requestUri, T value, MediaTypeFormatter formatter, String? media)
        {
            return PatchAsync(client, requestUri, value, formatter, media, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpResponseMessage> PatchAsync<T>(this HttpClient client, String requestUri, T value, MediaTypeFormatter formatter, String? media, CancellationToken token)
        {
            return PatchAsync(client, requestUri, value, formatter, MediaTypeContent.BuildHeaderValue(media), token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpResponseMessage> PatchAsync<T>(this HttpClient client, String requestUri, T value, MediaTypeFormatter formatter, MediaTypeHeaderValue? media)
        {
            return PatchAsync(client, requestUri, value, formatter, media, CancellationToken.None);
        }

        public static Task<HttpResponseMessage> PatchAsync<T>(this HttpClient client, String requestUri, T value, MediaTypeFormatter formatter, MediaTypeHeaderValue? media, CancellationToken token)
        {
            if (client is null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            if (requestUri is null)
            {
                throw new ArgumentNullException(nameof(requestUri));
            }

            if (formatter is null)
            {
                throw new ArgumentNullException(nameof(formatter));
            }

            MediaTypeContent content = new MediaTypeContent(value, formatter, media);
            return client.PatchAsync(requestUri, content, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpResponseMessage> PatchAsync<T>(this HttpClient client, Uri requestUri, T value, MediaTypeFormatter formatter)
        {
            return PatchAsync(client, requestUri, value, formatter, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpResponseMessage> PatchAsync<T>(this HttpClient client, Uri requestUri, T value, MediaTypeFormatter formatter, CancellationToken token)
        {
            return PatchAsync(client, requestUri, value, formatter, (MediaTypeHeaderValue?) null, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpResponseMessage> PatchAsync<T>(this HttpClient client, Uri requestUri, T value, MediaTypeFormatter formatter, String? media)
        {
            return PatchAsync(client, requestUri, value, formatter, media, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpResponseMessage> PatchAsync<T>(this HttpClient client, Uri requestUri, T value, MediaTypeFormatter formatter, String? media, CancellationToken token)
        {
            return PatchAsync(client, requestUri, value, formatter, MediaTypeContent.BuildHeaderValue(media), token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpResponseMessage> PatchAsync<T>(this HttpClient client, Uri requestUri, T value, MediaTypeFormatter formatter, MediaTypeHeaderValue? media)
        {
            return PatchAsync(client, requestUri, value, formatter, media, CancellationToken.None);
        }

        public static Task<HttpResponseMessage> PatchAsync<T>(this HttpClient client, Uri requestUri, T value, MediaTypeFormatter formatter, MediaTypeHeaderValue? media, CancellationToken token)
        {
            if (client is null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            if (requestUri is null)
            {
                throw new ArgumentNullException(nameof(requestUri));
            }

            if (formatter is null)
            {
                throw new ArgumentNullException(nameof(formatter));
            }

            MediaTypeContent content = new MediaTypeContent(value, formatter, media);
            return client.PatchAsync(requestUri, content, token);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpResponseMessage> PatchAsJsonAsync<T>(this HttpClient client, String requestUri, T value)
        {
            return PatchAsJsonAsync(client, requestUri, value, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpResponseMessage> PatchAsJsonAsync<T>(this HttpClient client, String requestUri, T value, CancellationToken token)
        {
            return PatchAsJsonAsync(client, requestUri, value, null, token);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpResponseMessage> PatchAsJsonAsync<T>(this HttpClient client, String requestUri, JsonMediaTypeFormatter? formatter, T value)
        {
            return PatchAsJsonAsync(client, requestUri, value, formatter, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpResponseMessage> PatchAsJsonAsync<T>(this HttpClient client, String requestUri, T value, JsonMediaTypeFormatter? formatter, CancellationToken token)
        {
            return PatchAsync(client, requestUri, value, formatter ?? new JsonMediaTypeFormatter(), token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpResponseMessage> PatchAsJsonAsync<T>(this HttpClient client, Uri requestUri, T value)
        {
            return PatchAsJsonAsync(client, requestUri, value, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpResponseMessage> PatchAsJsonAsync<T>(this HttpClient client, Uri requestUri, T value, CancellationToken token)
        {
            return PatchAsJsonAsync(client, requestUri, value, null, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpResponseMessage> PatchAsJsonAsync<T>(this HttpClient client, Uri requestUri, JsonMediaTypeFormatter? formatter, T value)
        {
            return PatchAsJsonAsync(client, requestUri, value, formatter, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpResponseMessage> PatchAsJsonAsync<T>(this HttpClient client, Uri requestUri, T value, JsonMediaTypeFormatter? formatter, CancellationToken token)
        {
            return PatchAsync(client, requestUri, value, formatter ?? new JsonMediaTypeFormatter(), token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpResponseMessage> PatchAsXmlAsync<T>(this HttpClient client, String requestUri, T value)
        {
            return PatchAsXmlAsync(client, requestUri, value, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpResponseMessage> PatchAsXmlAsync<T>(this HttpClient client, String requestUri, T value, CancellationToken token)
        {
            return PatchAsXmlAsync(client, requestUri, value, null, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpResponseMessage> PatchAsXmlAsync<T>(this HttpClient client, String requestUri, XmlMediaTypeFormatter? formatter, T value)
        {
            return PatchAsXmlAsync(client, requestUri, value, formatter, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpResponseMessage> PatchAsXmlAsync<T>(this HttpClient client, String requestUri, T value, XmlMediaTypeFormatter? formatter, CancellationToken token)
        {
            return PatchAsync(client, requestUri, value, formatter ?? new XmlMediaTypeFormatter(), token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpResponseMessage> PatchAsXmlAsync<T>(this HttpClient client, Uri requestUri, T value)
        {
            return PatchAsXmlAsync(client, requestUri, value, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpResponseMessage> PatchAsXmlAsync<T>(this HttpClient client, Uri requestUri, T value, CancellationToken token)
        {
            return PatchAsXmlAsync(client, requestUri, value, null, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpResponseMessage> PatchAsXmlAsync<T>(this HttpClient client, Uri requestUri, T value, XmlMediaTypeFormatter? formatter)
        {
            return PatchAsXmlAsync(client, requestUri, value, formatter, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpResponseMessage> PatchAsXmlAsync<T>(this HttpClient client, Uri requestUri, T value, XmlMediaTypeFormatter? formatter, CancellationToken token)
        {
            return PatchAsync(client, requestUri, value, formatter ?? new XmlMediaTypeFormatter(), token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Task<HttpResponseMessage> DownloadTaskAsync(this HttpClient client, String requestUri, CancellationToken token)
        {
            return DownloadTaskAsync(client, requestUri, HttpCompletionOption.ResponseHeadersRead, token);
        }

        private static async Task<HttpResponseMessage> DownloadTaskAsync(this HttpClient client, String requestUri, HttpCompletionOption option, CancellationToken token)
        {
            if (client is null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            if (requestUri is null)
            {
                throw new ArgumentNullException(nameof(requestUri));
            }

            HttpResponseMessage message = await client.GetAsync(requestUri, option, token).ConfigureAwait(false);
            message.EnsureSuccessStatusCode();

            return message;
        }

        private static Task<HttpResponseMessage?> DownloadTaskAsync(this HttpClient client, String requestUri, HttpCompletionOption option, Byte tries, TimeSpan timeout, Action<Byte>? callback, CancellationToken token)
        {
            if (client is null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            if (requestUri is null)
            {
                throw new ArgumentNullException(nameof(requestUri));
            }

            Task<HttpResponseMessage> DownloadTask(CancellationToken cancel)
            {
                return DownloadTaskAsync(client, requestUri, option, cancel);
            }

            return TaskUtilities.TimeoutRetryTaskAsync(DownloadTask, tries, timeout, callback, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Task<HttpResponseMessage> DownloadTaskAsync(this HttpClient client, Uri requestUri, CancellationToken token)
        {
            return DownloadTaskAsync(client, requestUri, HttpCompletionOption.ResponseHeadersRead, token);
        }

        private static async Task<HttpResponseMessage> DownloadTaskAsync(this HttpClient client, Uri requestUri, HttpCompletionOption option, CancellationToken token)
        {
            if (client is null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            if (requestUri is null)
            {
                throw new ArgumentNullException(nameof(requestUri));
            }

            HttpResponseMessage message = await client.GetAsync(requestUri, option, token).ConfigureAwait(false);
            message.EnsureSuccessStatusCode();

            return message;
        }

        private static Task<HttpResponseMessage?> DownloadTaskAsync(this HttpClient client, Uri requestUri, HttpCompletionOption option, Byte tries, TimeSpan timeout, Action<Byte>? callback, CancellationToken token)
        {
            if (client is null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            if (requestUri is null)
            {
                throw new ArgumentNullException(nameof(requestUri));
            }

            Task<HttpResponseMessage> DownloadTask(CancellationToken cancel)
            {
                return DownloadTaskAsync(client, requestUri, option, cancel);
            }

            return TaskUtilities.TimeoutRetryTaskAsync(DownloadTask, tries, timeout, callback, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<String> GetHeadAsync(this HttpClient client, String requestUri)
        {
            return GetHeadAsync(client, requestUri, CancellationToken.None);
        }

        public static async Task<String> GetHeadAsync(this HttpClient client, String requestUri, CancellationToken token)
        {
            if (client is null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            if (requestUri is null)
            {
                throw new ArgumentNullException(nameof(requestUri));
            }

            using HttpResponseMessage message = await client.SendAsync(new HttpRequestMessage(HttpMethod.Head, requestUri), token).ConfigureAwait(false);
            return await message.Content.ReadAsStringAsync(token).ConfigureAwait(false);
        }

        public static Task<String?> GetHeadAsync(this HttpClient client, String requestUri, Byte tries, TimeSpan timeout, Action<Byte> callback, CancellationToken token)
        {
            if (client is null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            if (requestUri is null)
            {
                throw new ArgumentNullException(nameof(requestUri));
            }

            Task<String> HandlerAsync(CancellationToken cancel)
            {
                return GetHeadAsync(client, requestUri, cancel);
            }

            return TaskUtilities.TimeoutRetryTaskAsync(HandlerAsync, tries, timeout, callback, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<String> GetHeadAsync(this HttpClient client, Uri requestUri)
        {
            return GetHeadAsync(client, requestUri, CancellationToken.None);
        }

        public static async Task<String> GetHeadAsync(this HttpClient client, Uri requestUri, CancellationToken token)
        {
            if (client is null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            if (requestUri is null)
            {
                throw new ArgumentNullException(nameof(requestUri));
            }

            using HttpResponseMessage message = await client.SendAsync(new HttpRequestMessage(HttpMethod.Head, requestUri), token).ConfigureAwait(false);
            return await message.Content.ReadAsStringAsync(token).ConfigureAwait(false);
        }

        public static Task<String?> GetHeadAsync(this HttpClient client, Uri requestUri, Byte tries, TimeSpan timeout, Action<Byte> callback, CancellationToken token)
        {
            if (client is null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            if (requestUri is null)
            {
                throw new ArgumentNullException(nameof(requestUri));
            }

            Task<String> HandlerAsync(CancellationToken cancel)
            {
                return GetHeadAsync(client, requestUri, cancel);
            }

            return TaskUtilities.TimeoutRetryTaskAsync(HandlerAsync, tries, timeout, callback, token);
        }
        
        private const Byte DefaultTries = 3;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<String> DownloadStringAsync(this HttpClient client, String requestUri)
        {
            return DownloadStringAsync(client, requestUri, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<String> DownloadStringAsync(this HttpClient client, String requestUri, CancellationToken token)
        {
            return DownloadStringAsync(client, requestUri, Encoding.UTF8, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<String> DownloadStringAsync(this HttpClient client, String requestUri, Encoding? encoding)
        {
            return DownloadStringAsync(client, requestUri, encoding, CancellationToken.None);
        }

        public static async Task<String> DownloadStringAsync(this HttpClient client, String requestUri, Encoding? encoding, CancellationToken token)
        {
            if (client is null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            if (requestUri is null)
            {
                throw new ArgumentNullException(nameof(requestUri));
            }

            using HttpResponseMessage message = await DownloadTaskAsync(client, requestUri, token).ConfigureAwait(false);
            using HttpContent content = message.Content;

            await using Stream stream = await content.ReadAsStreamAsync(token).ConfigureAwait(false);
            using StreamReader reader = new StreamReader(stream, encoding ?? Encoding.UTF8);

            return await reader.ReadToEndAsync().ConfigureAwait(false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<String?> DownloadStringAsync(this HttpClient client, String requestUri, Byte tries)
        {
            return DownloadStringAsync(client, requestUri, tries, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<String?> DownloadStringAsync(this HttpClient client, String requestUri, Byte tries, CancellationToken token)
        {
            return DownloadStringAsync(client, requestUri, Encoding.UTF8, tries, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<String?> DownloadStringAsync(this HttpClient client, String requestUri, Encoding? encoding, Byte tries)
        {
            return DownloadStringAsync(client, requestUri, encoding, tries, CancellationToken.None);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<String?> DownloadStringAsync(this HttpClient client, String requestUri, Encoding? encoding, Byte tries, CancellationToken token)
        {
            return DownloadStringAsync(client, requestUri, encoding, tries, Time.Minute.OneHalf, token);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<String?> DownloadStringAsync(this HttpClient client, String requestUri, TimeSpan timeout)
        {
            return DownloadStringAsync(client, requestUri, DefaultTries, timeout);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<String?> DownloadStringAsync(this HttpClient client, String requestUri, Byte tries, TimeSpan timeout)
        {
            return DownloadStringAsync(client, requestUri, tries, timeout, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<String?> DownloadStringAsync(this HttpClient client, String requestUri, Byte tries, TimeSpan timeout, CancellationToken token)
        {
            return DownloadStringAsync(client, requestUri, tries, timeout, null, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<String?> DownloadStringAsync(this HttpClient client, String requestUri, Encoding? encoding, TimeSpan timeout)
        {
            return DownloadStringAsync(client, requestUri, encoding, DefaultTries, timeout);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<String?> DownloadStringAsync(this HttpClient client, String requestUri, Encoding? encoding, Byte tries, TimeSpan timeout)
        {
            return DownloadStringAsync(client, requestUri, encoding, tries, timeout, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<String?> DownloadStringAsync(this HttpClient client, String requestUri, Encoding? encoding, Byte tries, TimeSpan timeout, CancellationToken token)
        {
            return DownloadStringAsync(client, requestUri, encoding, tries, timeout, null, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<String?> DownloadStringAsync(this HttpClient client, String requestUri, Byte tries, TimeSpan timeout, Action<Byte>? callback)
        {
            return DownloadStringAsync(client, requestUri, tries, timeout, callback, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<String?> DownloadStringAsync(this HttpClient client, String requestUri, Byte tries, TimeSpan timeout, Action<Byte>? callback, CancellationToken token)
        {
            return DownloadStringAsync(client, requestUri, Encoding.UTF8, tries, timeout, callback, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<String?> DownloadStringAsync(this HttpClient client, String requestUri, Encoding? encoding, Byte tries, TimeSpan timeout, Action<Byte>? callback)
        {
            return DownloadStringAsync(client, requestUri, encoding, tries, timeout, callback, CancellationToken.None);
        }

        public static Task<String?> DownloadStringAsync(this HttpClient client, String requestUri, Encoding? encoding, Byte tries, TimeSpan timeout, Action<Byte>? callback, CancellationToken token)
        {
            if (client is null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            if (requestUri is null)
            {
                throw new ArgumentNullException(nameof(requestUri));
            }

            Task<String> HandlerAsync(CancellationToken cancel)
            {
                return DownloadStringAsync(client, requestUri, encoding, cancel);
            }

            return TaskUtilities.TimeoutRetryTaskAsync(HandlerAsync, tries, timeout, callback, token);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<String> DownloadStringAsync(this HttpClient client, Uri requestUri)
        {
            return DownloadStringAsync(client, requestUri, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<String> DownloadStringAsync(this HttpClient client, Uri requestUri, CancellationToken token)
        {
            return DownloadStringAsync(client, requestUri, Encoding.UTF8, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<String> DownloadStringAsync(this HttpClient client, Uri requestUri, Encoding? encoding)
        {
            return DownloadStringAsync(client, requestUri, encoding, CancellationToken.None);
        }

        public static async Task<String> DownloadStringAsync(this HttpClient client, Uri requestUri, Encoding? encoding, CancellationToken token)
        {
            if (client is null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            if (requestUri is null)
            {
                throw new ArgumentNullException(nameof(requestUri));
            }

            using HttpResponseMessage message = await DownloadTaskAsync(client, requestUri, token).ConfigureAwait(false);
            using HttpContent content = message.Content;

            await using Stream stream = await content.ReadAsStreamAsync(token).ConfigureAwait(false);
            using StreamReader reader = new StreamReader(stream, encoding ?? Encoding.UTF8);

            return await reader.ReadToEndAsync().ConfigureAwait(false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<String?> DownloadStringAsync(this HttpClient client, Uri requestUri, Byte tries)
        {
            return DownloadStringAsync(client, requestUri, tries, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<String?> DownloadStringAsync(this HttpClient client, Uri requestUri, Byte tries, CancellationToken token)
        {
            return DownloadStringAsync(client, requestUri, Encoding.UTF8, tries, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<String?> DownloadStringAsync(this HttpClient client, Uri requestUri, Encoding? encoding, Byte tries)
        {
            return DownloadStringAsync(client, requestUri, encoding, tries, CancellationToken.None);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<String?> DownloadStringAsync(this HttpClient client, Uri requestUri, Encoding? encoding, Byte tries, CancellationToken token)
        {
            return DownloadStringAsync(client, requestUri, encoding, tries, Time.Minute.OneHalf, token);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<String?> DownloadStringAsync(this HttpClient client, Uri requestUri, TimeSpan timeout)
        {
            return DownloadStringAsync(client, requestUri, DefaultTries, timeout);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<String?> DownloadStringAsync(this HttpClient client, Uri requestUri, Byte tries, TimeSpan timeout)
        {
            return DownloadStringAsync(client, requestUri, tries, timeout, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<String?> DownloadStringAsync(this HttpClient client, Uri requestUri, Byte tries, TimeSpan timeout, CancellationToken token)
        {
            return DownloadStringAsync(client, requestUri, tries, timeout, null, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<String?> DownloadStringAsync(this HttpClient client, Uri requestUri, Encoding? encoding, TimeSpan timeout)
        {
            return DownloadStringAsync(client, requestUri, encoding, DefaultTries, timeout);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<String?> DownloadStringAsync(this HttpClient client, Uri requestUri, Encoding? encoding, Byte tries, TimeSpan timeout)
        {
            return DownloadStringAsync(client, requestUri, encoding, tries, timeout, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<String?> DownloadStringAsync(this HttpClient client, Uri requestUri, Encoding? encoding, Byte tries, TimeSpan timeout, CancellationToken token)
        {
            return DownloadStringAsync(client, requestUri, encoding, tries, timeout, null, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<String?> DownloadStringAsync(this HttpClient client, Uri requestUri, Byte tries, TimeSpan timeout, Action<Byte>? callback)
        {
            return DownloadStringAsync(client, requestUri, tries, timeout, callback, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<String?> DownloadStringAsync(this HttpClient client, Uri requestUri, Byte tries, TimeSpan timeout, Action<Byte>? callback, CancellationToken token)
        {
            return DownloadStringAsync(client, requestUri, Encoding.UTF8, tries, timeout, callback, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<String?> DownloadStringAsync(this HttpClient client, Uri requestUri, Encoding? encoding, Byte tries, TimeSpan timeout, Action<Byte>? callback)
        {
            return DownloadStringAsync(client, requestUri, encoding, tries, timeout, callback, CancellationToken.None);
        }

        public static Task<String?> DownloadStringAsync(this HttpClient client, Uri requestUri, Encoding? encoding, Byte tries, TimeSpan timeout, Action<Byte>? callback, CancellationToken token)
        {
            if (client is null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            if (requestUri is null)
            {
                throw new ArgumentNullException(nameof(requestUri));
            }

            Task<String> HandlerAsync(CancellationToken cancel)
            {
                return DownloadStringAsync(client, requestUri, encoding, cancel);
            }

            return TaskUtilities.TimeoutRetryTaskAsync(HandlerAsync, tries, timeout, callback, token);
        }

        private const Boolean DefaultOverwrite = false;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<FileInfo?> DownloadFileTaskAsync(this HttpClient client, String requestUri, String path, Int32 buffer = BufferUtilities.DefaultBuffer)
        {
            return DownloadFileTaskAsync(client, requestUri, path, DefaultOverwrite, buffer, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<FileInfo?> DownloadFileTaskAsync(this HttpClient client, String requestUri, String path, Boolean overwrite = DefaultOverwrite, Int32 buffer = BufferUtilities.DefaultBuffer)
        {
            return DownloadFileTaskAsync(client, requestUri, path, overwrite, buffer, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<FileInfo?> DownloadFileTaskAsync(this HttpClient client, String requestUri, String path, CancellationToken token)
        {
            return DownloadFileTaskAsync(client, requestUri, path, DefaultOverwrite, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<FileInfo?> DownloadFileTaskAsync(this HttpClient client, String requestUri, String path, Boolean overwrite, CancellationToken token)
        {
            return DownloadFileTaskAsync(client, requestUri, path, overwrite, BufferUtilities.DefaultBuffer, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<FileInfo?> DownloadFileTaskAsync(this HttpClient client, String requestUri, String path, Int32 buffer, CancellationToken token)
        {
            return DownloadFileTaskAsync(client, requestUri, path, DefaultOverwrite, buffer, token);
        }

        public static async Task<FileInfo?> DownloadFileTaskAsync(this HttpClient client, String requestUri, String path, Boolean overwrite, Int32 buffer, CancellationToken token)
        {
            if (client is null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            if (requestUri is null)
            {
                throw new ArgumentNullException(nameof(requestUri));
            }

            using HttpResponseMessage message = await DownloadTaskAsync(client, requestUri, HttpCompletionOption.ResponseHeadersRead, token).ConfigureAwait(false);
            using HttpContent content = message.Content;
            await using Stream stream = await content.ReadAsStreamAsync(token).ConfigureAwait(false);

            try
            {
                return await FileUtilities.SafeCreateFileAsync(path, stream, overwrite, buffer, token).ConfigureAwait(false);
            }
            catch (Exception)
            {
                return null;
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<FileInfo?> DownloadFileTaskAsync(this HttpClient client, Uri requestUri, String path, Int32 buffer = BufferUtilities.DefaultBuffer)
        {
            return DownloadFileTaskAsync(client, requestUri, path, DefaultOverwrite, buffer, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<FileInfo?> DownloadFileTaskAsync(this HttpClient client, Uri requestUri, String path, Boolean overwrite = DefaultOverwrite, Int32 buffer = BufferUtilities.DefaultBuffer)
        {
            return DownloadFileTaskAsync(client, requestUri, path, overwrite, buffer, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<FileInfo?> DownloadFileTaskAsync(this HttpClient client, Uri requestUri, String path, CancellationToken token)
        {
            return DownloadFileTaskAsync(client, requestUri, path, DefaultOverwrite, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<FileInfo?> DownloadFileTaskAsync(this HttpClient client, Uri requestUri, String path, Boolean overwrite, CancellationToken token)
        {
            return DownloadFileTaskAsync(client, requestUri, path, overwrite, BufferUtilities.DefaultBuffer, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<FileInfo?> DownloadFileTaskAsync(this HttpClient client, Uri requestUri, String path, Int32 buffer, CancellationToken token)
        {
            return DownloadFileTaskAsync(client, requestUri, path, DefaultOverwrite, buffer, token);
        }

        public static async Task<FileInfo?> DownloadFileTaskAsync(this HttpClient client, Uri requestUri, String path, Boolean overwrite, Int32 buffer, CancellationToken token)
        {
            if (client is null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            if (requestUri is null)
            {
                throw new ArgumentNullException(nameof(requestUri));
            }

            using HttpResponseMessage message = await DownloadTaskAsync(client, requestUri, HttpCompletionOption.ResponseHeadersRead, token).ConfigureAwait(false);
            using HttpContent content = message.Content;
            await using Stream stream = await content.ReadAsStreamAsync(token).ConfigureAwait(false);

            try
            {
                return await FileUtilities.SafeCreateFileAsync(path, stream, overwrite, buffer, token).ConfigureAwait(false);
            }
            catch (Exception)
            {
                return null;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Stream> GetSeekableStreamAsync(this HttpClient client, String requestUri)
        {
            return GetSeekableStreamAsync(client, requestUri, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Stream> GetSeekableStreamAsync(this HttpClient client, String requestUri, CancellationToken token)
        {
            return GetSeekableStreamAsync(client, requestUri, HttpCompletionOption.ResponseHeadersRead, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Stream> GetSeekableStreamAsync(this HttpClient client, String requestUri, HttpCompletionOption option)
        {
            return GetSeekableStreamAsync(client, requestUri, option, CancellationToken.None);
        }

        public static async Task<Stream> GetSeekableStreamAsync(this HttpClient client, String requestUri, HttpCompletionOption option, CancellationToken token)
        {
            if (client is null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            if (requestUri is null)
            {
                throw new ArgumentNullException(nameof(requestUri));
            }

            HttpResponseMessage message = await client.GetAsync(requestUri, option, token).ConfigureAwait(false);
            Stream stream = await message.Content.ReadAsStreamAsync(token).ConfigureAwait(false);

            return new SeekableStream(stream);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Stream> GetSeekableStreamAsync(this HttpClient client, Uri requestUri)
        {
            return GetSeekableStreamAsync(client, requestUri, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Stream> GetSeekableStreamAsync(this HttpClient client, Uri requestUri, CancellationToken token)
        {
            return GetSeekableStreamAsync(client, requestUri, HttpCompletionOption.ResponseHeadersRead, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Stream> GetSeekableStreamAsync(this HttpClient client, Uri requestUri, HttpCompletionOption option)
        {
            return GetSeekableStreamAsync(client, requestUri, option, CancellationToken.None);
        }

        public static async Task<Stream> GetSeekableStreamAsync(this HttpClient client, Uri requestUri, HttpCompletionOption option, CancellationToken token)
        {
            if (client is null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            if (requestUri is null)
            {
                throw new ArgumentNullException(nameof(requestUri));
            }

            HttpResponseMessage message = await client.GetAsync(requestUri, option, token).ConfigureAwait(false);
            Stream stream = await message.Content.ReadAsStreamAsync(token).ConfigureAwait(false);

            return new SeekableStream(stream);
        }
    }
}

namespace System.Net.Http.Json
{
    public static class HttpClientExtensions
    {
        private static class JsonContentFactory<T>
        {
            private static Func<T, JsonTypeInfo<T>, HttpContent> Constructor { get; }

            static JsonContentFactory()
            {
                Type? type = typeof(JsonContent).Assembly.GetType("System.Net.Http.Json.JsonContent`1")?.MakeGenericType(typeof(T));

                if (type is null)
                {
                    throw new NotSupportedException();
                }

                ConstructorInfo? constructor = type.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new[] { typeof(T), typeof(JsonTypeInfo<T>) }, null);

                if (constructor is null)
                {
                    throw new InvalidOperationException($"The constructor for {nameof(JsonContent)}<T> was not found.");
                }

                ParameterExpression value = Expression.Parameter(typeof(T), "value");
                ParameterExpression info = Expression.Parameter(typeof(JsonTypeInfo<T>), "jsonTypeInfo");
                NewExpression expression = Expression.New(constructor, value, info);
                Expression<Func<T, JsonTypeInfo<T>, HttpContent>> lambda = Expression.Lambda<Func<T, JsonTypeInfo<T>, HttpContent>>(expression, value, info);
                Constructor = lambda.Compile();
            }

            public static HttpContent Create(T value, JsonTypeInfo<T> jsonTypeInfo)
            {
                return Constructor(value, jsonTypeInfo);
            }
        }
        
        /// <summary>Sends a PATCH request to the specified Uri containing the <paramref name="value" /> serialized as JSON in the request body.</summary>
        /// <param name="client">The client used to send the request.</param>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <param name="value">The value to serialize.</param>
        /// <param name="options">Options to control the behavior during serialization. The default options are those specified by <see cref="F:System.Text.Json.JsonSerializerDefaults.Web" />.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <typeparam name="TValue">The type of the value to serialize.</typeparam>
        /// <returns>The task object representing the asynchronous operation.</returns>
        [RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed. Use the overload that takes a JsonTypeInfo or JsonSerializerContext, or make sure all of the required types are preserved.")]
        public static Task<HttpResponseMessage> PatchAsJsonAsync<TValue>(this HttpClient client, String? requestUri, TValue value, JsonSerializerOptions? options = null, CancellationToken cancellationToken = default)
        {
            if (client is null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            JsonContent content = JsonContent.Create(value, options: options);
            return client.PatchAsync(requestUri, content, cancellationToken);
        }

        /// <summary>Sends a PATCH request to the specified Uri containing the <paramref name="value" /> serialized as JSON in the request body.</summary>
        /// <param name="client">The client used to send the request.</param>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <param name="value">The value to serialize.</param>
        /// <param name="options">Options to control the behavior during serialization. The default options are those specified by <see cref="F:System.Text.Json.JsonSerializerDefaults.Web" />.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <typeparam name="TValue">The type of the value to serialize.</typeparam>
        /// <returns>The task object representing the asynchronous operation.</returns>
        [RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed. Use the overload that takes a JsonTypeInfo or JsonSerializerContext, or make sure all of the required types are preserved.")]
        public static Task<HttpResponseMessage> PatchAsJsonAsync<TValue>(this HttpClient client, Uri? requestUri, TValue value, JsonSerializerOptions? options = null, CancellationToken cancellationToken = default)
        {
            if (client is null)
            {
                throw new ArgumentNullException(nameof(client));
            }
            
            JsonContent content = JsonContent.Create(value, options: options);
            return client.PatchAsync(requestUri, content, cancellationToken);
        }

        /// <summary>Sends a PATCH request to the specified Uri containing the <paramref name="value" /> serialized as JSON in the request body.</summary>
        /// <param name="client">The client used to send the request.</param>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <param name="value">The value to serialize.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <typeparam name="TValue">The type of the value to serialize.</typeparam>
        /// <returns>The task object representing the asynchronous operation.</returns>
        [RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed. Use the overload that takes a JsonTypeInfo or JsonSerializerContext, or make sure all of the required types are preserved.")]
        public static Task<HttpResponseMessage> PatchAsJsonAsync<TValue>(this HttpClient client, String? requestUri, TValue value, CancellationToken cancellationToken)
        {
            if (client is null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            return client.PatchAsJsonAsync(requestUri, value, (JsonSerializerOptions?) null, cancellationToken);
        }

        /// <summary>Sends a PATCH request to the specified Uri containing the <paramref name="value" /> serialized as JSON in the request body.</summary>
        /// <param name="client">The client used to send the request.</param>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <param name="value">The value to serialize.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <typeparam name="TValue">The type of the value to serialize.</typeparam>
        /// <returns>The task object representing the asynchronous operation.</returns>
        [RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed. Use the overload that takes a JsonTypeInfo or JsonSerializerContext, or make sure all of the required types are preserved.")]
        public static Task<HttpResponseMessage> PatchAsJsonAsync<TValue>(this HttpClient client, Uri? requestUri, TValue value, CancellationToken cancellationToken)
        {
            if (client is null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            return client.PatchAsJsonAsync(requestUri, value, (JsonSerializerOptions?) null, cancellationToken);
        }

        /// <summary>Sends a PATCH request to the specified Uri containing the <paramref name="value" /> serialized as JSON in the request body.</summary>
        /// <param name="client">The client used to send the request.</param>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <param name="value">The value to serialize.</param>
        /// <param name="jsonTypeInfo">Source generated JsonTypeInfo to control the behavior during serialization.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <typeparam name="TValue">The type of the value to serialize.</typeparam>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public static Task<HttpResponseMessage> PatchAsJsonAsync<TValue>(this HttpClient client, String? requestUri, TValue value, JsonTypeInfo<TValue> jsonTypeInfo, CancellationToken cancellationToken = default)
        {
            if (client is null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            HttpContent content = JsonContentFactory<TValue>.Create(value, jsonTypeInfo);
            return client.PatchAsync(requestUri, content, cancellationToken);
        }

        /// <summary>Sends a PATCH request to the specified Uri containing the <paramref name="value" /> serialized as JSON in the request body.</summary>
        /// <param name="client">The client used to send the request.</param>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <param name="value">The value to serialize.</param>
        /// <param name="jsonTypeInfo">Source generated JsonTypeInfo to control the behavior during serialization.</param>
        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <typeparam name="TValue">The type of the value to serialize.</typeparam>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public static Task<HttpResponseMessage> PatchAsJsonAsync<TValue>(this HttpClient client, Uri? requestUri, TValue value, JsonTypeInfo<TValue> jsonTypeInfo, CancellationToken cancellationToken = default)
        {
            if (client is null)
            {
                throw new ArgumentNullException(nameof(client));
            }
            
            HttpContent content = JsonContentFactory<TValue>.Create(value, jsonTypeInfo);
            return client.PatchAsync(requestUri, content, cancellationToken);
        }
    }
}