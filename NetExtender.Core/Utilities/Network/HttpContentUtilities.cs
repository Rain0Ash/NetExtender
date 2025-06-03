// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NetExtender.Types.Exceptions;
using NetExtender.Types.Network;
using NetExtender.Types.Network.Exceptions;
using NetExtender.Types.Network.Formatters;
using NetExtender.Types.Network.Formatters.Exceptions;
using NetExtender.Types.Network.Formatters.Interfaces;
using NetExtender.Utilities.Core;
using NetExtender.Utilities.Network.Formatters;
using NetExtender.Utilities.Types;

namespace NetExtender.Utilities.Network
{
    public static class HttpContentUtilities
    {
        private static MediaTypeFormatterCollection? collection;
        private static MediaTypeFormatterCollection DefaultMediaTypeFormatterCollection
        {
            get
            {
                return collection ??= new MediaTypeFormatterCollection();
            }
        }
        
        public static void CopyTo(this HttpContentHeaders source, HttpContentHeaders destination)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (destination is null)
            {
                throw new ArgumentNullException(nameof(destination));
            }

            foreach (KeyValuePair<String, IEnumerable<String>> header in source)
            {
                destination.TryAddWithoutValidation(header.Key, header.Value);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T SetTextContentType<T>(this T content) where T : HttpContent
        {
            if (content is null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            SetTextContentType(content.Headers);
            return content;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T SetTextJsonContentType<T>(this T content) where T : HttpContent
        {
            if (content is null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            SetTextJsonContentType(content.Headers);
            return content;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T SetTextXmlContentType<T>(this T content) where T : HttpContent
        {
            if (content is null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            SetTextXmlContentType(content.Headers);
            return content;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T SetJsonContentType<T>(this T content) where T : HttpContent
        {
            if (content is null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            SetJsonContentType(content.Headers);
            return content;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T SetXmlContentType<T>(this T content) where T : HttpContent
        {
            if (content is null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            SetXmlContentType(content.Headers);
            return content;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T SetContentType<T>(this T content, MediaTypeHeaderValueType type) where T : HttpContent
        {
            if (content is null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            SetContentType(content.Headers, type);
            return content;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T SetContentType<T>(this T content, MediaTypeHeaderValue? type) where T : HttpContent
        {
            if (content is null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            SetContentType(content.Headers, type);
            return content;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean SetTextContentType(this HttpContentHeaders source)
        {
            return SetContentType(source, MediaTypeHeaderValueType.Text);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean SetTextJsonContentType(this HttpContentHeaders source)
        {
            return SetContentType(source, MediaTypeHeaderValueType.TextJson);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean SetTextXmlContentType(this HttpContentHeaders source)
        {
            return SetContentType(source, MediaTypeHeaderValueType.TextXml);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean SetJsonContentType(this HttpContentHeaders source)
        {
            return SetContentType(source, MediaTypeHeaderValueType.Json);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean SetXmlContentType(this HttpContentHeaders source)
        {
            return SetContentType(source, MediaTypeHeaderValueType.Xml);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean SetContentType(this HttpContentHeaders source, MediaTypeHeaderValueType type)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return SetContentType(source, type.Create());
        }

        public static Boolean SetContentType(this HttpContentHeaders source, MediaTypeHeaderValue? type)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (type is not null && String.IsNullOrEmpty(type.CharSet))
            {
                MediaTypeHeaderValue? current = source.ContentType;
                if (current is not null && !String.IsNullOrEmpty(current.CharSet))
                {
                    type.CharSet = current.CharSet;
                }
            }

            source.ContentType = type;
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Object?> ReadAsAsync(this HttpContent content, Type type)
        {
            return ReadAsAsync(content, type, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Object?> ReadAsAsync(this HttpContent content, Type type, CancellationToken token)
        {
            return ReadAsAsync(content, type, DefaultMediaTypeFormatterCollection, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Object?> ReadAsAsync(this HttpContent content, Type type, MediaTypeFormatter formatter)
        {
            return ReadAsAsync(content, type, formatter, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Object?> ReadAsAsync(this HttpContent content, Type type, params MediaTypeFormatter[] formatters)
        {
            return ReadAsAsync(content, type, (IEnumerable<MediaTypeFormatter>) formatters);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Object?> ReadAsAsync(this HttpContent content, Type type, IEnumerable<MediaTypeFormatter> formatters)
        {
            return ReadAsAsync(content, type, formatters, CancellationToken.None);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Object?> ReadAsAsync(this HttpContent content, Type type, MediaTypeFormatter formatter, CancellationToken token)
        {
            return ReadAsAsync(content, type, formatter, null, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Object?> ReadAsAsync(this HttpContent content, Type type, CancellationToken token, params MediaTypeFormatter[] formatters)
        {
            return ReadAsAsync(content, type, formatters, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Object?> ReadAsAsync(this HttpContent content, Type type, IEnumerable<MediaTypeFormatter> formatters, CancellationToken token)
        {
            return ReadAsAsync<Object>(content, type, formatters, null, token);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Object?> ReadAsAsync(this HttpContent content, Type type, MediaTypeFormatter formatter, ILogger? logger)
        {
            return ReadAsAsync(content, type, formatter, logger, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Object?> ReadAsAsync(this HttpContent content, Type type, ILogger? logger, params MediaTypeFormatter[] formatters)
        {
            return ReadAsAsync(content, type, formatters, logger);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Object?> ReadAsAsync(this HttpContent content, Type type, IEnumerable<MediaTypeFormatter> formatters, ILogger? logger)
        {
            return ReadAsAsync(content, type, formatters, logger, CancellationToken.None);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Object?> ReadAsAsync(this HttpContent content, Type type, MediaTypeFormatter formatter, ILogger? logger, CancellationToken token)
        {
            return ReadAsAsync<Object>(content, type, formatter, logger, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Object?> ReadAsAsync(this HttpContent content, Type type, ILogger? logger, CancellationToken token, params MediaTypeFormatter[] formatters)
        {
            return ReadAsAsync(content, type, formatters, logger, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<Object?> ReadAsAsync(this HttpContent content, Type type, IEnumerable<MediaTypeFormatter> formatters, ILogger? logger, CancellationToken token)
        {
            return ReadAsAsync<Object>(content, type, formatters, logger, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<T?> ReadAsAsync<T>(this HttpContent content)
        {
            return ReadAsAsync<T>(content, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<T?> ReadAsAsync<T>(this HttpContent content, CancellationToken token)
        {
            return ReadAsAsync<T>(content, DefaultMediaTypeFormatterCollection, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<T?> ReadAsAsync<T>(this HttpContent content, MediaTypeFormatter formatter)
        {
            return ReadAsAsync<T>(content, formatter, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<T?> ReadAsAsync<T>(this HttpContent content, params MediaTypeFormatter[] formatters)
        {
            return ReadAsAsync<T>(content, (IEnumerable<MediaTypeFormatter>) formatters);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<T?> ReadAsAsync<T>(this HttpContent content, IEnumerable<MediaTypeFormatter> formatters)
        {
            return ReadAsAsync<T>(content, formatters, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<T?> ReadAsAsync<T>(this HttpContent content, MediaTypeFormatter formatter, CancellationToken token)
        {
            return ReadAsAsync<T>(content, formatter, null, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<T?> ReadAsAsync<T>(this HttpContent content, CancellationToken token, params MediaTypeFormatter[] formatters)
        {
            return ReadAsAsync<T>(content, formatters, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<T?> ReadAsAsync<T>(this HttpContent content, IEnumerable<MediaTypeFormatter> formatters, CancellationToken token)
        {
            return ReadAsAsync<T>(content, formatters, null, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<T?> ReadAsAsync<T>(this HttpContent content, MediaTypeFormatter formatter, ILogger? logger)
        {
            return ReadAsAsync<T>(content, formatter, logger, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<T?> ReadAsAsync<T>(this HttpContent content, ILogger? logger, params MediaTypeFormatter[] formatters)
        {
            return ReadAsAsync<T>(content, formatters, logger);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<T?> ReadAsAsync<T>(this HttpContent content, IEnumerable<MediaTypeFormatter> formatters, ILogger? logger)
        {
            return ReadAsAsync<T>(content, formatters, logger, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<T?> ReadAsAsync<T>(this HttpContent content, MediaTypeFormatter formatter, ILogger? logger, CancellationToken token)
        {
            return ReadAsAsync<T>(content, typeof(T), formatter, logger, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<T?> ReadAsAsync<T>(this HttpContent content, ILogger? logger, CancellationToken token, params MediaTypeFormatter[] formatters)
        {
            return ReadAsAsync<T>(content, formatters, logger, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<T?> ReadAsAsync<T>(this HttpContent content, IEnumerable<MediaTypeFormatter> formatters, ILogger? logger, CancellationToken token)
        {
            return ReadAsAsync<T>(content, typeof(T), formatters, logger, token);
        }

        private static Task<T?> ReadAsAsync<T>(HttpContent content, Type type, MediaTypeFormatter formatter, ILogger? logger, CancellationToken token)
        {
            if (content is null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (formatter is null)
            {
                throw new ArgumentNullException(nameof(formatter));
            }

            return ReadAsAsync<T>(content, type, new MediaTypeFormatterCollection(formatter), logger, token);
        }

        private static Task<T?> ReadAsAsync<T>(HttpContent content, Type type, IEnumerable<MediaTypeFormatter> formatters, ILogger? logger, CancellationToken token)
        {
            if (content is null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (formatters is null)
            {
                throw new ArgumentNullException(nameof(formatters));
            }

            if (content is IMediaTypeContent<T> { Value: { } media })
            {
                return Task.FromResult<T?>(media);
            }
            
            MediaTypeHeaderValue header = content.Headers.ContentType ?? MediaTypeFormatterUtilities.ApplicationOctetStreamMediaType;
            
            // ReSharper disable once LocalVariableHidesMember
            MediaTypeFormatterCollection collection = formatters as MediaTypeFormatterCollection ?? new MediaTypeFormatterCollection(formatters);
            if (collection.FindReader(type, header) is { } reader)
            {
                return ReadAsAsyncCore<T>(content, type, reader, logger, token);
            }

            if (content.Headers.ContentLength <= 0)
            {
                return Task.FromResult((T?) ReflectionUtilities.Default(type));
            }

            throw new MediaTypeNotSupportedException(type, header, $"No {nameof(MediaTypeFormatter)} is available to read an object of type '{type}' from content with media type '{header.MediaType?.ToLowerInvariant() ?? "null"}'.") { Formatters = collection };
        }

        private static async Task<T?> ReadAsAsyncCore<T>(HttpContent content, Type type, MediaTypeFormatter formatter, ILogger? logger, CancellationToken token)
        {
            if (content is null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (formatter is null)
            {
                throw new ArgumentNullException(nameof(formatter));
            }

            token.ThrowIfCancellationRequested();
            return (T?) await formatter.ReadFromStreamAsync(type, await content.ReadAsStreamAsync(token), content, logger, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsFormData(this HttpContent content)
        {
            if (content is null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            MediaTypeHeaderValue? header = content.Headers.ContentType;
            return header is not null && String.Equals("application/x-www-form-urlencoded", header.MediaType, StringComparison.OrdinalIgnoreCase);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<NameValueCollection?> ReadAsFormDataAsync(this HttpContent content)
        {
            return ReadAsFormDataAsync(content, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<NameValueCollection?> ReadAsFormDataAsync(this HttpContent content, CancellationToken token)
        {
            if (content is null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            MediaTypeFormatter[] formatters =
            {
                new FormUrlEncodedMediaTypeFormatter()
            };
            
            return ReadAsAsyncCore(content, formatters, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static async Task<NameValueCollection?> ReadAsAsyncCore(HttpContent content, IEnumerable<MediaTypeFormatter> formatters, CancellationToken token)
        {
            if (content is null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            return (await ReadAsAsync<FormDataCollection>(content, formatters, token))?.Collection;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsHttpRequestMessageContent(this HttpContent content)
        {
            if (content is null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            try
            {
                return HttpMessageContent.ValidateHttpMessageContent(content, true, false);
            }
            catch (Exception)
            {
                return false;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsHttpResponseMessageContent(this HttpContent content)
        {
            if (content is null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            try
            {
                return HttpMessageContent.ValidateHttpMessageContent(content, false, false);
            }
            catch (Exception)
            {
                return false;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpRequestMessage> ReadAsHttpRequestMessageAsync(this HttpContent content)
        {
            return ReadAsHttpRequestMessageAsync(content, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpRequestMessage> ReadAsHttpRequestMessageAsync(this HttpContent content, CancellationToken token)
        {
            return ReadAsHttpRequestMessageAsync(content, "http", 32768, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpRequestMessage> ReadAsHttpRequestMessageAsync(this HttpContent content, String scheme)
        {
            return ReadAsHttpRequestMessageAsync(content, scheme, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpRequestMessage> ReadAsHttpRequestMessageAsync(this HttpContent content, String scheme, CancellationToken token)
        {
            return ReadAsHttpRequestMessageAsync(content, scheme, 32768, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpRequestMessage> ReadAsHttpRequestMessageAsync(this HttpContent content, String scheme, Int32 buffer)
        {
            return ReadAsHttpRequestMessageAsync(content, scheme, buffer, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpRequestMessage> ReadAsHttpRequestMessageAsync(this HttpContent content, String scheme, Int32 buffer, CancellationToken token)
        {
            return ReadAsHttpRequestMessageAsync(content, scheme, buffer, 16384, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpRequestMessage> ReadAsHttpRequestMessageAsync(this HttpContent content, String scheme, Int32 buffer, Int32 header)
        {
            return ReadAsHttpRequestMessageAsync(content, scheme, buffer, header, CancellationToken.None);
        }

        public static Task<HttpRequestMessage> ReadAsHttpRequestMessageAsync(this HttpContent content, String scheme, Int32 buffer, Int32 header, CancellationToken token)
        {
            if (content is null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            if (scheme is null)
            {
                throw new ArgumentNullException(nameof(scheme));
            }

            if (!Uri.CheckSchemeName(scheme))
            {
                throw new ArgumentException($"Invalid URI scheme: '{scheme}'. The URI scheme must be a valid '{nameof(Uri)}' scheme.", nameof(scheme));
            }

            if (buffer < 256)
            {
                throw new ArgumentOutOfRangeException(nameof(buffer), buffer, null);
            }

            if (header < 2)
            {
                throw new ArgumentOutOfRangeException(nameof(header), header, null);
            }

            HttpMessageContent.ValidateHttpMessageContent(content, true, true);
            return ReadAsHttpRequestMessageAsyncCore(content, scheme, buffer, header, token);
        }

        private static async Task<HttpRequestMessage> ReadAsHttpRequestMessageAsyncCore(this HttpContent content, String scheme, Int32 size, Int32 header, CancellationToken token)
        {
            if (content is null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            if (scheme is null)
            {
                throw new ArgumentNullException(nameof(scheme));
            }

            token.ThrowIfCancellationRequested();
            Stream stream = await content.ReadAsStreamAsync(token);
            HttpUnsortedRequest request = new HttpUnsortedRequest();
            HttpRequestHeaderParser parser = new HttpRequestHeaderParser(request, Math.Max(2048, header), header);
            Byte[] buffer = new Byte[size];
            Int32 consumed = 0;
            Int32 ready;
            
            do
            {
                try
                {
                    ready = await stream.ReadAsync(buffer, token);
                }
                catch (Exception exception)
                {
                    throw new HttpMessageReadingException(null, exception);
                }
                
                HttpParserState state;
                try
                {
                    state = parser.ParseBuffer(buffer, ready, ref consumed);
                }
                catch (Exception)
                {
                    state = HttpParserState.Invalid;
                }
                
                switch (state)
                {
                    case HttpParserState.NeedMoreData:
                        continue;
                    case HttpParserState.Done:
                        return CreateHttpRequestMessage(scheme, request, stream, ready - consumed);
                    default:
                        throw new HttpMessageHeaderParsingException($"Error parsing HTTP message header byte {consumed} of message {buffer}.");
                }
            } while (ready > 0);
            
            throw new HttpMessageUnexpectedEndException("Unexpected end of HTTP message stream. HTTP message is not complete.");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpResponseMessage> ReadAsHttpResponseMessageAsync(this HttpContent content)
        {
            return ReadAsHttpResponseMessageAsync(content, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpResponseMessage> ReadAsHttpResponseMessageAsync(this HttpContent content, CancellationToken token)
        {
            return ReadAsHttpResponseMessageAsync(content, 32768, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpResponseMessage> ReadAsHttpResponseMessageAsync(this HttpContent content, Int32 buffer)
        {
            return ReadAsHttpResponseMessageAsync(content, buffer, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpResponseMessage> ReadAsHttpResponseMessageAsync(this HttpContent content, Int32 buffer, CancellationToken token)
        {
            return ReadAsHttpResponseMessageAsync(content, buffer, 16384, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpResponseMessage> ReadAsHttpResponseMessageAsync(this HttpContent content, Int32 buffer, Int32 header)
        {
            return ReadAsHttpResponseMessageAsync(content, buffer, header, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<HttpResponseMessage> ReadAsHttpResponseMessageAsync(this HttpContent content, Int32 buffer, Int32 header, CancellationToken token)
        {
            if (content is null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            if (buffer < 256)
            {
                throw new ArgumentOutOfRangeException(nameof(buffer), buffer, null);
            }

            if (header < 2)
            {
                throw new ArgumentOutOfRangeException(nameof(header), header, null);
            }

            HttpMessageContent.ValidateHttpMessageContent(content, false, true);
            return ReadAsHttpResponseMessageAsyncCore(content, buffer, header, token);
        }

        private static async Task<HttpResponseMessage> ReadAsHttpResponseMessageAsyncCore(this HttpContent content, Int32 size, Int32 header, CancellationToken token)
        {
            if (content is null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            token.ThrowIfCancellationRequested();
            Stream stream = await content.ReadAsStreamAsync(token);
            HttpUnsortedResponse response = new HttpUnsortedResponse();
            HttpResponseHeaderParser parser = new HttpResponseHeaderParser(response, 2048, header);
            Byte[] buffer = new Byte[size];
            Int32 consumed = 0;
            Int32 ready;
            do
            {
                try
                {
                    ready = await stream.ReadAsync(buffer, token);
                }
                catch (Exception exception)
                {
                    throw new HttpMessageReadingException(null, exception);
                }
                
                HttpParserState state;
                try
                {
                    state = parser.ParseBuffer(buffer, ready, ref consumed);
                }
                catch (Exception)
                {
                    state = HttpParserState.Invalid;
                }
                
                switch (state)
                {
                    case HttpParserState.NeedMoreData:
                        continue;
                    case HttpParserState.Done:
                        return CreateHttpResponseMessage(response, stream, ready - consumed);
                    default:
                        throw new HttpMessageHeaderParsingException($"Error parsing HTTP message header byte {consumed} of message {buffer}.");
                }
            } while (ready != 0);
            
            throw new HttpMessageUnexpectedEndException("Unexpected end of HTTP message stream. HTTP message is not complete.");
        }

        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        private static Uri CreateRequestUri(String scheme, HttpUnsortedRequest request)
        {
            if (scheme is null)
            {
                throw new ArgumentNullException(nameof(scheme));
            }

            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (!request.HttpHeaders.TryGetValues("Host", out IEnumerable<String>? values))
            {
                throw new HttpMessageHeaderParsingException("An invalid number of 'Host' header fields were present in the HTTP Request. It must contain exactly one 'Host' header field but found 0.");
            }

            if (values.ElementAtOrDefault(0) is not { } element)
            {
                throw new HttpMessageHeaderParsingException($"An invalid number of 'Host' header fields were present in the HTTP Request. It must contain exactly one 'Host' header field but found {values.CountIfMaterialized()?.ToString() ?? "multiple"}.");
            }

            return new Uri(String.Format(CultureInfo.InvariantCulture, "{0}://{1}{2}", scheme, element, request.RequestUri));
        }

        private static HttpContent CreateHeaderFields(HttpHeaders source, HttpHeaders destination, Stream stream, Int32 rewind)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (destination is null)
            {
                throw new ArgumentNullException(nameof(destination));
            }

            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            HttpContentHeaders? headers = null;
            foreach (KeyValuePair<String, IEnumerable<String>> pair in source)
            {
                if (destination.TryAddWithoutValidation(pair.Key, pair.Value))
                {
                    continue;
                }

                if (headers is null)
                {
                    using StringContent content = new StringContent(String.Empty);
                    headers = content.Headers;
                    headers.Clear();
                }

                headers.TryAddWithoutValidation(pair.Key, pair.Value);
            }

            if (headers is null)
            {
                return new StreamContent(Stream.Null);
            }

            if (!stream.CanSeek)
            {
                throw new HttpMessageParsingException($"The 'ContentReadStream' must be seekable in order to create an '{nameof(HttpResponseMessage)}' instance containing an entity body.");
            }

            stream.Seek(-rewind, SeekOrigin.Current);
            HttpContent fields = new StreamContent(stream);
            headers.CopyTo(fields.Headers);
            return fields;
        }

        private static HttpRequestMessage CreateHttpRequestMessage(String scheme, HttpUnsortedRequest request, Stream stream, Int32 rewind)
        {
            if (scheme is null)
            {
                throw new ArgumentNullException(nameof(scheme));
            }

            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (request.Method is null)
            {
                throw new ArgumentException("Method must be not null", nameof(request));
            }

            HttpRequestMessage message = new HttpRequestMessage
            {
                Method = request.Method,
                RequestUri = CreateRequestUri(scheme, request),
                Version = request.Version ?? new Version()
            };
            
            message.Content = CreateHeaderFields(request.HttpHeaders, message.Headers, stream, rewind);
            return message;
        }

        private static HttpResponseMessage CreateHttpResponseMessage(HttpUnsortedResponse response, Stream stream, Int32 rewind)
        {
            if (response is null)
            {
                throw new ArgumentNullException(nameof(response));
            }

            HttpResponseMessage message = new HttpResponseMessage
            {
                StatusCode = response.StatusCode,
                Version = response.Version ?? new Version(),
                ReasonPhrase = response.ReasonPhrase
            };
            
            message.Content = CreateHeaderFields(response.HttpHeaders, message.Headers, stream, rewind);
            return message;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsMimeMultipartContent(this HttpContent content)
        {
            if (content is null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            return MimeMultipartBodyPartParser.IsMimeMultipartContent(content);
        }

        public static Boolean IsMimeMultipartContent(this HttpContent content, String subtype)
        {
            if (String.IsNullOrWhiteSpace(subtype))
            {
                throw new ArgumentNullOrWhiteSpaceStringException(subtype, nameof(subtype));
            }

            return IsMimeMultipartContent(content) && content.Headers.ContentType?.MediaType?.Equals("multipart/" + subtype, StringComparison.OrdinalIgnoreCase) is true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<MultipartMemoryStreamProvider> ReadAsMultipartAsync(this HttpContent content)
        {
            return ReadAsMultipartAsync(content, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<MultipartMemoryStreamProvider> ReadAsMultipartAsync(this HttpContent content, CancellationToken token)
        {
            if (content is null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            return ReadAsMultipartAsync(content, new MultipartMemoryStreamProvider(), 32768, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<T> ReadAsMultipartAsync<T>(this HttpContent content, T provider) where T : MultipartStreamProvider
        {
            return ReadAsMultipartAsync(content, provider, CancellationToken.None);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<T> ReadAsMultipartAsync<T>(this HttpContent content, T provider, CancellationToken token) where T : MultipartStreamProvider
        {
            return ReadAsMultipartAsync(content, provider, 32768, token);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Task<T> ReadAsMultipartAsync<T>(this HttpContent content, T stream, Int32 buffer) where T : MultipartStreamProvider
        {
            return ReadAsMultipartAsync(content, stream, buffer, CancellationToken.None);
        }

        public static async Task<T> ReadAsMultipartAsync<T>(this HttpContent content, T stream, Int32 buffer, CancellationToken token) where T : MultipartStreamProvider
        {
            if (content is null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            buffer = buffer switch
            {
                <= 0 => throw new ArgumentOutOfRangeException(nameof(buffer), buffer, null),
                < 256 => 256,
                _ => buffer
            };

            Stream inner;
            try
            {
                inner = await content.ReadAsStreamAsync(token);
            }
            catch (Exception exception)
            {
                throw new HttpMessageMimeMultipartReadingException("Error reading MIME multipart body part.", exception);
            }

            using MimeMultipartBodyPartParser parser = new MimeMultipartBodyPartParser(content, stream);
            Byte[] data = new Byte[buffer];
            await MultipartReadAsync(new MultipartAsyncContext(inner, parser, data, stream.Contents), token);
            await stream.ExecutePostProcessingAsync(token);
            return stream;
        }

        // ReSharper disable once CognitiveComplexity
        private static async Task MultipartReadAsync(MultipartAsyncContext context, CancellationToken token)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            while (true)
            {
                Int32 read;
                try
                {
                    read = await context.Stream.ReadAsync(context.Data.AsMemory(0, context.Data.Length), token);
                }
                catch (Exception exception)
                {
                    throw new HttpMessageMimeMultipartReadingException("Error reading MIME multipart body part.", exception);
                }

                using IEnumerator<MimeBodyPart> enumerator = context.Parser.ParseBuffer(context.Data, read).GetEnumerator();
                while (true)
                {
                    if (!enumerator.MoveNext())
                    {
                        return;
                    }

                    MimeBodyPart part = enumerator.Current;
                    foreach (ArraySegment<Byte> segment in part.Segments)
                    {
                        try
                        {
                            await part.WriteSegment(segment, token);
                        }
                        catch (Exception exception)
                        {
                            part.Dispose();
                            throw new HttpMessageMimeMultipartReadingException("Error writing MIME multipart body part to output stream.", exception);
                        }
                    }

                    if (CheckIsFinalPart(part, context.Result))
                    {
                        return;
                    }
                }
            }
        }

        private static Boolean CheckIsFinalPart(MimeBodyPart part, ICollection<HttpContent> result)
        {
            if (part is null)
            {
                throw new ArgumentNullException(nameof(part));
            }

            if (result is null)
            {
                throw new ArgumentNullException(nameof(result));
            }

            if (!part.IsComplete)
            {
                return false;
            }

            HttpContent? content = part.GetHttpContent();
            if (content is not null)
            {
                result.Add(content);
            }

            Boolean final = part.IsFinal;
            part.Dispose();
            return final;
        }

        private class MultipartAsyncContext
        {
            public Stream Stream { get; }
            public MimeMultipartBodyPartParser Parser { get; }
            public Byte[] Data { get; }
            public ICollection<HttpContent> Result { get; }
            
            public MultipartAsyncContext(Stream stream, MimeMultipartBodyPartParser parser, Byte[] data, ICollection<HttpContent> result)
            {
                Stream = stream ?? throw new ArgumentNullException(nameof(stream));
                Parser = parser ?? throw new ArgumentNullException(nameof(parser));
                Data = data ?? throw new ArgumentNullException(nameof(data));
                Result = result ?? throw new ArgumentNullException(nameof(result));
            }
        }
    }
}