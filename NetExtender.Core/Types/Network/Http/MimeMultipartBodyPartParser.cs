// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;

namespace NetExtender.Types.Network
{
    public class MimeMultipartBodyPartParser : IDisposable
    {
        protected MimeMultipartParser? Parser { get; set; }
        protected MimeMultipartParser.MimeParserState Status { get; set; }
        protected ArraySegment<Byte>[] Part { get; } = new ArraySegment<Byte>[2];
        protected MimeBodyPart? Current { get; set; }
        protected HttpContent Content { get; }
        protected MultipartStreamProvider Provider { get; }
        protected Int32 Header { get; }
        protected Boolean IsFirst { get; set; } = true;
        protected HttpParserState HeaderStatus { get; set; }

        public MimeMultipartBodyPartParser(
            HttpContent content,
            MultipartStreamProvider provider)
            : this(content, provider, Int64.MaxValue, 4096)
        {
        }

        public MimeMultipartBodyPartParser(HttpContent content, MultipartStreamProvider provider, Int64 message, Int32 header)
        {
            if (content is null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            if (provider is null)
            {
                throw new ArgumentNullException(nameof(provider));
            }

            String? result = ValidateArguments(content, message, true);

            if (result is null)
            {
                throw new ArgumentException($"Can't validate arguments of {nameof(HttpContent)}", nameof(content));
            }

            Parser = new MimeMultipartParser(result, message);
            Current = new MimeBodyPart(provider, header, content);
            Content = content;
            Provider = provider;
            Header = header;
        }

        public static Boolean IsMimeMultipartContent(HttpContent content)
        {
            if (content is null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            try
            {
                return ValidateArguments(content, Int64.MaxValue, false) is not null;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IEnumerable<MimeBodyPart> ParseBuffer(Byte[] data, Int32 read)
        {
            if (Parser is null)
            {
                throw new ObjectDisposedException(nameof(MimeMultipartBodyPartParser));
            }
            
            if (data is null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            Int32 consumed = 0;
            if (read <= 0 && !Parser.IsWaiting)
            {
                Current?.Dispose();
                Current = null;
                throw new IOException("Unexpected end of MIME multipart stream. MIME multipart message is not complete.");
            }
            
            Current?.Segments.Clear();
            while (Parser.CanParseMore(read, consumed))
            {
                Status = Parser.ParseBuffer(data, read, ref consumed, out Part[0], out Part[1], out Boolean isFinalBodyPart);
                
                if (Status != MimeMultipartParser.MimeParserState.BodyPartCompleted && Status != MimeMultipartParser.MimeParserState.NeedMoreData)
                {
                    Current?.Dispose();
                    Current = null;
                    throw new InvalidOperationException($"Error parsing MIME multipart message byte {consumed} of data segment {data}.");
                }
                
                if (IsFirst)
                {
                    if (Status == MimeMultipartParser.MimeParserState.BodyPartCompleted)
                    {
                        IsFirst = false;
                    }
                    
                    continue;
                }

                foreach (ArraySegment<Byte> segment in Part)
                {
                    if (segment.Count <= 0)
                    {
                        continue;
                    }

                    if (HeaderStatus == HttpParserState.Done)
                    {
                        Current?.Segments.Add(segment);
                        continue;
                    }

                    if (segment.Array is null)
                    {
                        throw new InvalidOperationException();
                    }

                    Int32 offset = segment.Offset;
                    HeaderStatus = Current?.Parser?.ParseBuffer(segment.Array, segment.Count + segment.Offset, ref offset) ?? HttpParserState.Invalid;
                    switch (HeaderStatus)
                    {
                        case HttpParserState.Done:
                            Current?.Segments.Add(new ArraySegment<Byte>(segment.Array, offset, segment.Count + segment.Offset - offset));
                            continue;
                        case HttpParserState.NeedMoreData:
                            continue;
                        default:
                            Current?.Dispose();
                            Current = null;
                            throw new InvalidOperationException($"Error parsing MIME multipart body part header byte {offset} of data segment {segment.Array}.");
                    }
                }

                MimeBodyPart? current = Current;
                if (current is not null && Status != MimeMultipartParser.MimeParserState.BodyPartCompleted)
                {
                    yield return current;
                    continue;
                }

                if (current is null)
                {
                    continue;
                }

                current.IsComplete = true;
                current.IsFinal = isFinalBodyPart;
                Current = new MimeBodyPart(Provider, Header, Content);
                Status = MimeMultipartParser.MimeParserState.NeedMoreData;
                HeaderStatus = HttpParserState.NeedMoreData;
                yield return current;
            }
        }

        protected static String? ValidateArguments(HttpContent content, Int64 size, Boolean @throw)
        {
            if (content is null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            if (size < 10)
            {
                return !@throw ? null : throw new ArgumentOutOfRangeException(nameof(size), size, null);
            }
            
            MediaTypeHeaderValue? type = content.Headers.ContentType;
            if (type is null)
            {
                return !@throw ? null : throw new ArgumentException($"Invalid '{nameof(HttpContent)}' instance provided. It does not have a content-type header value. '{nameof(HttpContent)}' instances must have a content-type header starting with 'multipart/'.", nameof(content));
            }

            if (type.MediaType?.StartsWith("multipart", StringComparison.OrdinalIgnoreCase) != true)
            {
                return !@throw ? null : throw new ArgumentException($"Invalid '{nameof(HttpContent)}' instance provided. It does not have a content type header starting with 'multipart/'.", nameof(content));
            }
            
            String? value = null;
            foreach (NameValueHeaderValue parameter in type.Parameters)
            {
                [return: NotNullIfNotNull("token")]
                static String? UnquoteToken(String? token)
                {
                    if (!String.IsNullOrWhiteSpace(token) && token.Length > 1 && token.StartsWith("\"", StringComparison.Ordinal) && token.EndsWith("\"", StringComparison.Ordinal))
                    {
                        return token.Substring(1, token.Length - 2);
                    }

                    return token;
                }

                if (!parameter.Name.Equals("boundary", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                value = UnquoteToken(parameter.Value);
                break;
            }

            return value ?? (!@throw ? null : throw new ArgumentException($"Invalid '{nameof(HttpContent)}' instance provided. It does not have a 'multipart' content-type header with a 'boundary' parameter.", nameof(content)));
        }

        protected void Dispose(Boolean disposing)
        {
            if (!disposing)
            {
                return;
            }

            Parser = null;
            Current?.Dispose();
            Current = null;
        }
    }
}