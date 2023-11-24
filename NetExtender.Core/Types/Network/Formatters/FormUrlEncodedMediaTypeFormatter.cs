// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NetExtender.Utilities.Network.Formatters;
using Newtonsoft.Json.Linq;

namespace NetExtender.Types.Network.Formatters
{
    public class FormUrlEncodedMediaTypeFormatter : MediaTypeFormatter
    {
        public static MediaTypeHeaderValue DefaultMediaType
        {
            get
            {
                return MediaTypeFormatterUtilities.ApplicationFormUrlEncodedMediaType;
            }
        }
        
        private Int32 _maxdepth = 256;
        public override Int32 MaxDepth
        {
            get
            {
                return _maxdepth;
            }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
                }
                
                _maxdepth = value;
            }
        }

        private readonly Int32 _readBufferSize = 32768;
        public Int32 ReadBufferSize
        {
            get
            {
                return _readBufferSize;
            }
            init
            {
                _readBufferSize = value >= 256 ? value : 256;
            }
        }

        internal override Boolean CanWriteAnyTypes { get; }

        public FormUrlEncodedMediaTypeFormatter()
        {
            SupportedMediaType.Add(MediaTypeFormatterUtilities.ApplicationFormUrlEncodedMediaType);
            CanWriteAnyTypes = GetType() != typeof(FormUrlEncodedMediaTypeFormatter);
        }

        protected FormUrlEncodedMediaTypeFormatter(FormUrlEncodedMediaTypeFormatter formatter)
            : base(formatter)
        {
            _maxdepth = formatter.MaxDepth;
            ReadBufferSize = formatter.ReadBufferSize;
            CanWriteAnyTypes = GetType() != typeof(FormUrlEncodedMediaTypeFormatter);
        }
        
        public override Boolean CanReadType(Type? type)
        {
            return type is not null && (type == typeof(FormDataCollection) || typeof(JToken).IsAssignableFrom(type));
        }

        public override Boolean CanWriteType(Type? type)
        {
            return false;
        }

        private Object? ReadFromStream(Type type, Stream stream)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            IEnumerable<KeyValuePair<String, String>> read = ReadFormUrlEncoded(stream, ReadBufferSize);
            if (type == typeof (FormDataCollection))
            {
                return new FormDataCollection(read);
            }

            if (typeof(JToken).IsAssignableFrom(type))
            {
                return FormUrlEncodedJson.Parse(read!, MaxDepth);
            }

            throw new InvalidOperationException($"The '{GetType()}' serializer cannot serialize the type '{type}'.");
        }

        public override Task<Object?> ReadFromStreamAsync(Type type, Stream stream, HttpContent content, ILogger? logger, CancellationToken token)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            if (content is null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            token.ThrowIfCancellationRequested();

            try
            {
                return Task.FromResult(ReadFromStream(type, stream));
            }
            catch (Exception exception)
            {
                return Task.FromException<Object?>(exception);
            }
        }

        protected virtual IEnumerable<KeyValuePair<String, String>> ReadFormUrlEncoded(Stream stream, Int32 size)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            Byte[] buffer = new Byte[size];
            Boolean final = false;
            List<KeyValuePair<String, String>> result = new List<KeyValuePair<String, String>>();
            FormUrlEncodedParser parser = new FormUrlEncodedParser(result, Int64.MaxValue);
            
            do
            {
                Int32 read;
                try
                {
                    read = stream.Read(buffer, 0, buffer.Length);
                    if (read == 0)
                    {
                        final = true;
                    }
                }
                catch (Exception exception)
                {
                    throw new InvalidOperationException("Error reading HTML form URL-encoded data stream.", exception);
                }
                
                Int32 consumed = 0;
                switch (parser.ParseBuffer(buffer, read, ref consumed, final))
                {
                    case HttpParserState.NeedMoreData:
                    case HttpParserState.Done:
                        continue;
                    default:
                        throw new InvalidOperationException($"Error parsing HTML form URL-encoded data, byte {consumed}.");
                }
            } while (!final);

            return result;
        }
    }
}