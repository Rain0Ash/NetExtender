// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using NetExtender.Types.Network.Formatters.Interfaces;
using NetExtender.Utilities.Core;

namespace NetExtender.Types.Network.Formatters
{
    public class MediaTypeContent : MediaTypeContent<Object?>
    {
        public MediaTypeContent(Object? value, MediaTypeFormatter formatter)
            : base(value, formatter, (MediaTypeHeaderValue?) null)
        {
        }

        public MediaTypeContent(Object? value, MediaTypeFormatter formatter, String? media)
            : base(value, formatter, media)
        {
        }

        public MediaTypeContent(Object? value, MediaTypeFormatter formatter, MediaTypeHeaderValue? media)
            : base(value, formatter, media)
        {
        }
    }
    
    public class MediaTypeContent<T> : HttpContent, IMediaTypeContent<T>
    {
        public Type Type { get; }
        public MediaTypeFormatter Formatter { get; }
        public T Value { get; set; }
        
        Object? IMediaTypeContent.Value
        {
            get
            {
                return Value;
            }
        }

        public MediaTypeContent(T value, MediaTypeFormatter formatter)
            : this(null, value, formatter, (MediaTypeHeaderValue?) null)
        {
        }

        public MediaTypeContent(T value, MediaTypeFormatter formatter, String? media)
            : this(null, value, formatter, media)
        {
        }

        public MediaTypeContent(T value, MediaTypeFormatter formatter, MediaTypeHeaderValue? media)
            : this(null, value, formatter, media)
        {
        }

        protected MediaTypeContent(Type? type, T value, MediaTypeFormatter formatter)
            : this(type, value, formatter, (MediaTypeHeaderValue?) null)
        {
        }

        protected MediaTypeContent(Type? type, T value, MediaTypeFormatter formatter, String? media)
            : this(type, value, formatter, BuildHeaderValue(media))
        {
        }

        protected MediaTypeContent(Type? type, T value, MediaTypeFormatter formatter, MediaTypeHeaderValue? media)
        {
            if (formatter is null)
            {
                throw new ArgumentNullException(nameof(formatter));
            }

            Type = type ?? value?.GetType() ?? typeof(T);
            Formatter = formatter.CanWriteType(Type) ? formatter : throw new InvalidOperationException($"The configured formatter '{formatter.GetType().FullName}' cannot write an object of type '{Type.Name}'.");
            Formatter.SetDefaultContentHeaders(Type, Headers, media);
            Value = value;
        }
        
        [return: NotNullIfNotNull("media")]
        internal static MediaTypeHeaderValue? BuildHeaderValue(String? media)
        {
            return media is not null ? new MediaTypeHeaderValue(media) : null;
        }

        protected override Task SerializeToStreamAsync(Stream stream, TransportContext? context)
        {
            return Formatter.WriteToStreamAsync(Type, Value, stream, this, context);
        }

        protected override Boolean TryComputeLength(out Int64 length)
        {
            length = -1;
            return false;
        }

        private static Boolean IsTypeNullable(Type type)
        {
            return !type.IsValueType || type.TryGetGenericTypeDefinition() == typeof(Nullable<>);
        }
    }
}