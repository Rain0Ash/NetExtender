﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NetExtender.Types.Collections;
using NetExtender.Types.Network.Formatters.Exceptions;
using NetExtender.Types.Network.Formatters.Interfaces;
using NetExtender.Types.Sets;
using NetExtender.Utilities.Core;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Network.Formatters
{
    public abstract class MediaTypeFormatter : IMediaTypeFormatter
    {
        protected static Encoding UTF8Encoding { get; } = new UTF8Encoding(false, true);
        private static ConcurrentDictionary<Type, Type> DelegatingEnumerableStorage { get; } = new ConcurrentDictionary<Type, Type>();
        protected static ConcurrentDictionary<Type, ConstructorInfo?> DelegatingEnumerableConstructorCache { get; } = new ConcurrentDictionary<Type, ConstructorInfo?>();

        private Int32 _maximum = Int32.MaxValue;
        public Int32 MaximumHttpCollectionKeys
        {
            get
            {
                return _maximum;
            }
            set
            {
                _maximum = value > 0 ? value : throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }
        }

        protected internal OrderedSet<MediaTypeHeader> SupportedMediaType { get; }
        
        ICollection<MediaTypeHeader> IMediaTypeFormatter.SupportedMediaType
        {
            get
            {
                return SupportedMediaType;
            }
        }
        
        IReadOnlyCollection<MediaTypeHeader> IReadOnlyMediaTypeFormatter.SupportedMediaType
        {
            get
            {
                return SupportedMediaType;
            }
        }
        
        protected internal OrderedSet<Encoding> SupportedEncoding { get; }
        
        ICollection<Encoding> IMediaTypeFormatter.SupportedEncoding
        {
            get
            {
                return SupportedEncoding;
            }
        }
        
        IReadOnlyCollection<Encoding> IReadOnlyMediaTypeFormatter.SupportedEncoding
        {
            get
            {
                return SupportedEncoding;
            }
        }
        
        protected internal OrderedSet<MediaTypeMapping> MediaTypeFormatterMapping { get; }
        
        ICollection<MediaTypeMapping> IMediaTypeFormatter.MediaTypeFormatterMapping
        {
            get
            {
                return MediaTypeFormatterMapping;
            }
        }
        
        IReadOnlyCollection<MediaTypeMapping> IReadOnlyMediaTypeFormatter.MediaTypeFormatterMapping
        {
            get
            {
                return MediaTypeFormatterMapping;
            }
        }
        
        public IFormatterMemberSelector? Selector { get; set; }
        
        internal virtual Boolean CanWriteAnyTypes
        {
            get
            {
                return true;
            }
        }
        
        public abstract Int32 MaxDepth { get; set; }
        
        protected MediaTypeFormatter()
            : this(null)
        {
        }

        protected MediaTypeFormatter(MediaTypeFormatter? formatter)
        {
            SupportedMediaType = formatter?.SupportedMediaType ?? new OrderedSet<MediaTypeHeader>();
            SupportedEncoding = formatter?.SupportedEncoding ?? new OrderedSet<Encoding>();
            MediaTypeFormatterMapping = formatter?.MediaTypeFormatterMapping ?? new OrderedSet<MediaTypeMapping>();
            Selector = formatter?.Selector;
        }
        
        public Boolean Contains(MediaTypeHeader header)
        {
            return SupportedMediaType.Contains(header);
        }
        
        public Boolean Contains(MediaTypeHeader header, Encoding encoding)
        {
            return Contains(header) && Contains(encoding);
        }
        
        public Boolean Contains(Encoding encoding)
        {
            return SupportedEncoding.Contains(encoding);
        }
        
        public Boolean Contains(MediaTypeMapping mapping)
        {
            return MediaTypeFormatterMapping.Contains(mapping);
        }
        
        public abstract Boolean CanReadType(Type type);
        public abstract Boolean CanWriteType(Type type);

        public virtual MediaTypeFormatter GetPerRequestFormatterInstance(Type type, HttpRequestMessage request, MediaTypeHeaderValue media)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return this;
        }

        protected static Boolean TryGetDelegatingType(Type @interface, ref Type? type)
        {
            if (@interface is null)
            {
                throw new ArgumentNullException(nameof(@interface));
            }

            if (type is null || !type.IsInterface || !type.IsGenericType)
            {
                return false;
            }

            if (type.TryGetGenericInterface(@interface) is not { } generic)
            {
                return false;
            }

            type = GetOrAddDelegatingType(type, generic);
            return true;
        }

        public Task<Object?> ReadFromStreamAsync(Type type, Stream stream, HttpContent content, ILogger? logger)
        {
            return ReadFromStreamAsync(type, stream, content, logger, CancellationToken.None);
        }

        public virtual Task<Object?> ReadFromStreamAsync(Type type, Stream stream, HttpContent content, ILogger? logger, CancellationToken token)
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
            
            throw new MediaTypeFormatterOperationNotSupportedException($"The media type formatter of type '{GetType()}' does not support reading because it does not implement the {nameof(ReadFromStreamAsync)} method.");
        }

        public Task WriteToStreamAsync(Type type, Object? value, Stream stream, HttpContent content, TransportContext? context)
        {
            return WriteToStreamAsync(type, value, stream, content, context, CancellationToken.None);
        }

        public virtual Task WriteToStreamAsync(Type type, Object? value, Stream stream, HttpContent content, TransportContext? context, CancellationToken token)
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
            
            throw new MediaTypeFormatterOperationNotSupportedException($"The media type formatter of type '{GetType()}' does not support writing because it does not implement the {nameof(WriteToStreamAsync)} method.");
        }

        public virtual Encoding SelectCharacterEncoding(HttpContentHeaders headers)
        {
            if (headers is null)
            {
                throw new ArgumentNullException(nameof(headers));
            }

            Encoding? encoding = null;
            if (headers.ContentType is not null)
            {
                String? charset = headers.ContentType.CharSet;
                if (!String.IsNullOrWhiteSpace(charset))
                {
                    foreach (Encoding item in SupportedEncoding.Where(item => charset.Equals(item.WebName, StringComparison.OrdinalIgnoreCase)))
                    {
                        encoding = item;
                        break;
                    }
                }
            }
            
            if (encoding is null && SupportedEncoding.Count > 0)
            {
                encoding = SupportedEncoding.First;
            }

            return encoding ?? throw new MediaTypeFormatterEncodingNotFoundException($"No encoding found for media type formatter '{GetType()}'. There must be at least one supported encoding registered in order for the media type formatter to read or write content.");
        }

        // ReSharper disable once CognitiveComplexity
        public virtual Boolean SetDefaultContentHeaders(Type type, HttpContentHeaders headers, MediaTypeHeaderValue? media)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (headers is null)
            {
                throw new ArgumentNullException(nameof(headers));
            }

            if (media is not null)
            {
                headers.ContentType = media.Clone();
            }

            if (headers.ContentType is null)
            {
                MediaTypeHeader value = default;
                if (SupportedMediaType.Count > 0)
                {
                    value = SupportedMediaType.First;
                }

                if (!value.IsEmpty)
                {
                    headers.ContentType = value.Clone();
                }
            }
            
            if (headers.ContentType is not { CharSet: null })
            {
                return false;
            }

            Encoding? encoding = null;
            if (SupportedEncoding.Count > 0)
            {
                encoding = SupportedEncoding.First;
            }

            if (encoding is null)
            {
                return false;
            }

            headers.ContentType.CharSet = encoding.WebName;
            return true;
        }

        private static Type GetOrAddDelegatingType(Type type, Type generic)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (generic is null)
            {
                throw new ArgumentNullException(nameof(generic));
            }

            return DelegatingEnumerableStorage.GetOrAdd(type, static (_, generic) =>
            {
                Type argument = generic.GetGenericArguments()[0];
                Type key = typeof(EnumerableWrapper<>).MakeGenericType(argument);
                ConstructorInfo? constructor = key.GetConstructor(new[] { typeof(IEnumerable<>).MakeGenericType(argument) });
                DelegatingEnumerableConstructorCache.TryAdd(key, constructor);
                return key;
            }, generic);
        }

        private protected static void WritePreamble(Stream stream, Encoding encoding)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            if (encoding is null)
            {
                throw new ArgumentNullException(nameof(encoding));
            }

            Byte[] preamble = encoding.GetPreamble();
            if (preamble.Length <= 0)
            {
                return;
            }

            stream.Write(preamble, 0, preamble.Length);
        }

        internal class MediaTypeHeaderValueCollection : Collection<MediaTypeHeader>
        {
            private static Type MediaType { get; } = typeof(MediaTypeHeaderValue);

            internal MediaTypeHeaderValueCollection(IList<MediaTypeHeader> list)
                : base(list)
            {
            }

            protected override void InsertItem(Int32 index, MediaTypeHeader item)
            {
                ValidateMediaType(item);
                base.InsertItem(index, item);
            }

            protected override void SetItem(Int32 index, MediaTypeHeader item)
            {
                ValidateMediaType(item);
                base.SetItem(index, item);
            }

            private static void ValidateMediaType(MediaTypeHeader item)
            {
                if (item.Value is not { } header)
                {
                    throw new ArgumentNullException(nameof(item));
                }

                FormatterMediaTypeHeaderValue value = new FormatterMediaTypeHeaderValue(header);
                if (value.IsAllMediaRange || value.IsSubtypeMediaRange)
                {
                    throw new ArgumentException($"The '{MediaType.Name}' of '{header.MediaType}' cannot be used as a supported media type because it is a media range.", nameof(item));
                }
            }
        }
    }
}