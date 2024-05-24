// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Network
{
    public class HttpMessageContent : HttpContent
    {
        private static ImmutableHashSet<String> SingleValueHeaderFields { get; } = new HashSet<String>(StringComparer.OrdinalIgnoreCase)
        {
            "Cookie",
            "Set-Cookie",
            "X-Powered-By"
        }.ToImmutableHashSet();

        private static ImmutableHashSet<String> SpaceValueHeaderFields { get; } = new HashSet<String>(StringComparer.OrdinalIgnoreCase)
        {
            "User-Agent"
        }.ToImmutableHashSet();

        protected Lazy<Task<Stream>?> Stream { get; }
        protected Boolean Consumed { get; set; }

        public HttpRequestMessage? HttpRequestMessage { get; private set; }
        public HttpResponseMessage? HttpResponseMessage { get; private set; }

        protected HttpContent? Content
        {
            get
            {
                return HttpRequestMessage is null ? HttpResponseMessage?.Content : HttpRequestMessage.Content;
            }
        }

        public HttpMessageContent(HttpRequestMessage request)
        {
            HttpRequestMessage = request ?? throw new ArgumentNullException(nameof(request));
            Headers.ContentType = new MediaTypeHeaderValue("application/http");
            Headers.ContentType.Parameters.Add(new NameValueHeaderValue("msgtype", "request"));
            Stream = new Lazy<Task<Stream>?>(() => Content?.ReadAsStreamAsync());
        }

        public HttpMessageContent(HttpResponseMessage response)
        {
            HttpResponseMessage = response ?? throw new ArgumentNullException(nameof(response));
            Headers.ContentType = new MediaTypeHeaderValue("application/http");
            Headers.ContentType.Parameters.Add(new NameValueHeaderValue("msgtype", "response"));
            Stream = new Lazy<Task<Stream>?>(() => Content?.ReadAsStreamAsync());
        }

        [return: NotNullIfNotNull("token")]
        protected static String? UnquoteToken(String? token)
        {
            if (!String.IsNullOrWhiteSpace(token) && token.StartsWith("\"", StringComparison.Ordinal) && token.EndsWith("\"", StringComparison.Ordinal) && token.Length > 1)
            {
                return token.Substring(1, token.Length - 2);
            }

            return token;
        }

        internal static Boolean ValidateHttpMessageContent(HttpContent content, Boolean isrequest, Boolean @throw)
        {
            if (content is null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            MediaTypeHeaderValue? type = content.Headers.ContentType;
            if (type is null)
            {
                return !@throw ? false : throw new ArgumentException($"Invalid '{nameof(HttpContent)}' instance provided. It does not have a content type header with a value of 'application/http; msgtype={(isrequest ? "request" : "response")}'.", nameof(content));
            }

            String? media = type.MediaType;

            if (media is null)
            {
                throw new InvalidOperationException("Media is null");
            }

            if (!media.Equals("application/http", StringComparison.OrdinalIgnoreCase))
            {
                return !@throw ? false : throw new ArgumentException($"Invalid '{nameof(HttpContent)}' instance provided. It does not have a content type header with a value of 'application/http; msgtype={(isrequest ? "request" : "response")}'.", nameof(content));
            }

            foreach (NameValueHeaderValue parameter in type.Parameters)
            {
                if (!parameter.Name.Equals("msgtype", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                if (UnquoteToken(parameter.Value)?.Equals(isrequest ? "request" : "response", StringComparison.OrdinalIgnoreCase) is true)
                {
                    return true;
                }

                return !@throw ? false : throw new ArgumentException($"Invalid '{nameof(HttpContent)}' instance provided. It does not have a content type header with a value of 'application/http; msgtype={(isrequest ? "request" : "response")}'.", nameof(content));
            }

            return !@throw ? false : throw new ArgumentException($"Invalid '{nameof(HttpContent)}' instance provided. It does not have a content type header with a value of 'application/http; msgtype={(isrequest ? "request" : "response")}'.", nameof(content));
        }

        private Byte[] SerializeHeader()
        {
            StringBuilder message = new StringBuilder(2048);
            HttpHeaders? headers = null;
            HttpContent? content = null;
            
            if (HttpRequestMessage is not null)
            {
                SerializeRequestLine(message, HttpRequestMessage);
                headers = HttpRequestMessage.Headers;
                content = HttpRequestMessage.Content;
            }
            else if (HttpResponseMessage is not null)
            {
                SerializeStatusLine(message, HttpResponseMessage);
                headers = HttpResponseMessage.Headers;
                content = HttpResponseMessage.Content;
            }

            if (headers is not null)
            {
                SerializeHeaderFields(message, headers);
            }
            
            if (content is not null)
            {
                SerializeHeaderFields(message, content.Headers);
            }

            message.Append("\r\n");
            return Encoding.UTF8.GetBytes(message.ToString());
        }

        private static void SerializeHeaderFields(StringBuilder builder, HttpHeaders headers)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (headers is null)
            {
                throw new ArgumentNullException(nameof(headers));
            }

            foreach (KeyValuePair<String, IEnumerable<String>> header in headers)
            {
                if (SingleValueHeaderFields.Contains(header.Key))
                {
                    foreach (String str in header.Value)
                    {
                        builder.Append(header.Key + ": " + str + "\r\n");
                    }
                    
                    return;
                }

                if (SpaceValueHeaderFields.Contains(header.Key))
                {
                    builder.Append(header.Key + ": " + String.Join(" ", header.Value) + "\r\n");
                    return;
                }

                builder.Append(header.Key + ": " + String.Join(", ", header.Value) + "\r\n");
            }
        }

        private static void SerializeRequestLine(StringBuilder builder, HttpRequestMessage request)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            builder.Append(request.Method + " ");
            builder.Append(request.RequestUri?.PathAndQuery + " ");
            // ReSharper disable once ConditionalAccessQualifierIsNonNullableAccordingToAPIContract
            builder.Append("HTTP/" + (request.Version?.ToString(2) ?? "1.1") + "\r\n");
            if (request.Headers.Host is not null)
            {
                return;
            }

            builder.Append("Host: " + request.RequestUri?.Authority + "\r\n");
        }

        private static void SerializeStatusLine(StringBuilder builder, HttpResponseMessage response)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (response is null)
            {
                throw new ArgumentNullException(nameof(response));
            }

            // ReSharper disable once ConditionalAccessQualifierIsNonNullableAccordingToAPIContract
            builder.Append("HTTP/" + (response.Version?.ToString(2) ?? "1.1") + " ");
            builder.Append((Int32) response.StatusCode + " ");
            builder.Append(response.ReasonPhrase + "\r\n");
        }

        protected override async Task SerializeToStreamAsync(Stream stream, TransportContext? context)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            Byte[] buffer = SerializeHeader();
            await stream.WriteAsync(buffer, 0, buffer.Length);
            if (Content is null || Stream.Value is null)
            {
                return;
            }

            ValidateStreamForReading(await Stream.Value);
            await Content.CopyToAsync(stream);
        }

        protected override Boolean TryComputeLength(out Int64 length)
        {
            Int64? value = Content?.Headers.ContentLength;
            if (value is not null)
            {
                length = value.Value;
            }
            else if (Stream.Value is not null)
            {
                if (!Stream.Value.TryGetResult(out Stream? result) || !result.CanSeek)
                {
                    length = -1;
                    return false;
                }
                
                length = result.Length;
            }
            else
            {
                length = 0;
            }
            
            Byte[] array = SerializeHeader();
            length += array.Length;
            return true;
        }

        private void ValidateStreamForReading(Stream stream)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            if (stream is not { CanRead: true })
            {
                throw new NotSupportedException("Stream does not support reading.");
            }

            if (!Consumed)
            {
                Consumed = true;
                return;
            }

            if (stream.CanSeek)
            {
                stream.Position = 0;
                return;
            }

            throw new InvalidOperationException($"The '{nameof(HttpContent)}' of the '{(HttpRequestMessage is not null ? nameof(System.Net.Http.HttpRequestMessage) : nameof(System.Net.Http.HttpResponseMessage))}' has already been read.");
        }

        protected override void Dispose(Boolean disposing)
        {
            if (!disposing)
            {
                base.Dispose(disposing);
                return;
            }

            if (HttpRequestMessage is not null)
            {
                HttpRequestMessage.Dispose();
                HttpRequestMessage = null;
            }

            if (HttpResponseMessage is not null)
            {
                HttpResponseMessage.Dispose();
                HttpResponseMessage = null;
            }

            base.Dispose(disposing);
        }
    }
}