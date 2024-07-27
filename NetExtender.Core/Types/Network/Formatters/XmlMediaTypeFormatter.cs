// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Extensions.Logging;
using NetExtender.Types.Streams;
using NetExtender.Utilities.Core;
using NetExtender.Utilities.Network.Formatters;

namespace NetExtender.Types.Network.Formatters
{
    public class XmlMediaTypeFormatter : MediaTypeFormatter
    {
        protected static XsdDataContractExporter XsdExporter { get; } = new XsdDataContractExporter();
        
        public static MediaTypeHeaderValue DefaultMediaType
        {
            get
            {
                return MediaTypeFormatterUtilities.ApplicationXmlMediaType;
            }
        }

        protected ConcurrentDictionary<Type, Object?> SerializerStorage { get; } = new ConcurrentDictionary<Type, Object?>();

        protected XmlDictionaryReaderQuotas Quotas { get; } = new XmlDictionaryReaderQuotas
        {
            MaxArrayLength = Int32.MaxValue,
            MaxBytesPerRead = Int32.MaxValue,
            MaxNameTableCharCount = Int32.MaxValue,
            MaxStringContentLength = Int32.MaxValue,
            MaxDepth = 256
        };

        public XmlWriterSettings Settings { get; private set; }
        public Boolean UseXmlSerializer { get; set; }

        public Boolean Indent
        {
            get
            {
                return Settings.Indent;
            }
            set
            {
                Settings.Indent = value;
            }
        }

        public override Int32 MaxDepth
        {
            get
            {
                return Quotas.MaxDepth;
            }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
                }
                
                Quotas.MaxDepth = value;
            }
        }

        public XmlMediaTypeFormatter()
        {
            SupportedMediaType.Add(MediaTypeFormatterUtilities.ApplicationXmlMediaType);
            SupportedMediaType.Add(MediaTypeFormatterUtilities.TextXmlMediaType);
            SupportedEncoding.Add(new UTF8Encoding(false, true));
            SupportedEncoding.Add(new UnicodeEncoding(false, true, true));
            Settings = new XmlWriterSettings
            {
                OmitXmlDeclaration = true,
                CloseOutput = false,
                CheckCharacters = false
            };
        }

        protected XmlMediaTypeFormatter(XmlMediaTypeFormatter? formatter)
            : base(formatter)
        {
            if (formatter is not null)
            {
                UseXmlSerializer = formatter.UseXmlSerializer;
                Quotas.MaxDepth = formatter.MaxDepth;
            }
            
            Settings = formatter?.Settings ?? new XmlWriterSettings
            {
                OmitXmlDeclaration = true,
                CloseOutput = false,
                CheckCharacters = false
            };
        }
        
        public override Boolean CanReadType(Type? type)
        {
            return type is not null && GetSerializer(type, false) is not null;
        }

        public override Boolean CanWriteType(Type? type)
        {
            return type is not null && TryGetDelegatingType(UseXmlSerializer ? typeof(IEnumerable<>) : typeof(IQueryable<>), ref type) && type is not null && GetSerializer(type, false) is not null;
        }
        
        // ReSharper disable once ReturnTypeCanBeNotNullable
        protected internal virtual Object? GetSerializer(Type type, Object? value, HttpContent content)
        {
            return GetSerializerForType(type);
        }
        
        protected Object? GetSerializer(Type type)
        {
            return GetSerializer(type, false);
        }

        protected virtual Object? GetSerializer(Type type, Boolean @throw)
        {
            try
            {
                if (SerializerStorage.TryGetValue(type, out Object? serializer))
                {
                    return serializer;
                }

                serializer = CreateDefaultSerializer(type, @throw);
                SerializerStorage.TryAdd(type, serializer);
                return serializer;
            }
            catch (Exception)
            {
                return null;
            }
        }

        protected virtual Object GetSerializerForType(Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return GetSerializer(type, true) ?? throw new InvalidOperationException($"The '{(UseXmlSerializer ? nameof(XmlSerializer) : nameof(DataContractSerializer))}' serializer cannot serialize the type '{type}'.");
        }
        
        // ReSharper disable once ReturnTypeCanBeNotNullable
        protected internal virtual Object? GetDeserializer(Type type, HttpContent content)
        {
            return GetSerializerForType(type);
        }
        
        protected virtual Object? CreateDefaultSerializer(Type type, Boolean @throw)
        {
            try
            {
                if (UseXmlSerializer)
                {
                    return new XmlSerializer(type);
                }

                XsdExporter.GetRootElementName(type);
                return new DataContractSerializer(type);
            }
            catch (Exception exception)
            {
                return !@throw ? null : throw new InvalidOperationException($"The '{(UseXmlSerializer ? nameof(XmlSerializer) : nameof(DataContractSerializer))}' serializer cannot serialize the type '{type}'.", exception);
            }
        }
        
        public void SetSerializer(Type type, XmlObjectSerializer serializer)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (serializer is null)
            {
                throw new ArgumentNullException(nameof(serializer));
            }

            SerializerStorage.AddOrUpdate(type, serializer, (_, _) => serializer);
        }

        public void SetSerializer<T>(XmlObjectSerializer serializer)
        {
            SetSerializer(typeof(T), serializer);
        }

        public void SetSerializer(Type type, XmlSerializer serializer)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (serializer is null)
            {
                throw new ArgumentNullException(nameof(serializer));
            }

            SerializerStorage.AddOrUpdate(type, serializer, (_, _) => serializer);
        }

        public void SetSerializer<T>(XmlSerializer serializer)
        {
            SetSerializer(typeof(T), serializer);
        }

        public Boolean RemoveSerializer(Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return SerializerStorage.TryRemove(type, out _);
        }
        
        protected internal virtual XmlReader CreateXmlReader(Stream stream, HttpContent content)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            if (content is null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            Encoding encoding = SelectCharacterEncoding(content.Headers);
            stream = String.Equals(encoding.WebName, Utf8Encoding.WebName, StringComparison.OrdinalIgnoreCase) ? new UnclosableStreamWrapper(stream) : new TranscodingStream(stream, encoding, Utf8Encoding, true);
            return XmlDictionaryReader.CreateTextReader(stream, Utf8Encoding, Quotas, null);
        }

        protected internal virtual XmlWriter CreateXmlWriter(Stream stream, HttpContent content)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            if (content is null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            Encoding encoding = SelectCharacterEncoding(content.Headers);
            WritePreamble(stream, encoding);
            Stream output = String.Equals(encoding.WebName, Utf8Encoding.WebName, StringComparison.OrdinalIgnoreCase) ? stream : new TranscodingStream(stream, encoding, Utf8Encoding, true);
            XmlWriterSettings settings = Settings.Clone();
            settings.Encoding = Utf8Encoding;
            settings.CloseOutput = stream != output;
            return XmlWriter.Create(output, settings);
        }

        private Object? ReadFromStream(Type type, Stream stream, HttpContent content, ILogger? logger)
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

            HttpContentHeaders headers = content.Headers;
            Int64? length = headers.ContentLength;
            if (length is null || length == 0)
            {
                return ReflectionUtilities.Default(type);
            }

            Object? result = GetDeserializer(type, content);
            try
            {
                using XmlReader reader = CreateXmlReader(stream, content);
                return result switch
                {
                    null => throw new InvalidOperationException($"The instance returned by {nameof(GetDeserializer)} must not be a null value."),
                    XmlSerializer serializer => serializer.Deserialize(reader),
                    XmlObjectSerializer serializer => serializer.ReadObject(reader),
                    _ => throw new InvalidOperationException($"The instance of type '{result.GetType()}' returned by {nameof(GetDeserializer)} must be an instance of either {nameof(XmlObjectSerializer)} or {nameof(XmlSerializer)}.")
                };
            }
            catch (Exception exception)
            {
                if (logger is null)
                {
                    throw;
                }

                logger?.LogError("Exception: {0}", exception);
                return ReflectionUtilities.Default(type);
            }
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
                return Task.FromResult(ReadFromStream(type, stream, content, logger));
            }
            catch (Exception exception)
            {
                return Task.FromException<Object?>(exception);
            }
        }

        private void WriteToStream(Type type, Object? value, Stream stream, HttpContent content)
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

            Type? delegating = type;
            if (value is not null && (!UseXmlSerializer ? TryGetDelegatingType(typeof(IQueryable<>), ref delegating) : TryGetDelegatingType(typeof(IEnumerable<>), ref delegating)) && DelegatingEnumerableConstructorCache.TryGetValue(type, out ConstructorInfo? constructor) && constructor is not null)
            {
                value = constructor.Invoke(new[] { value });
            }

            if (delegating is null)
            {
                throw new InvalidOperationException();
            }

            Object? result = GetSerializer(delegating, value, content);
            using XmlWriter writer = CreateXmlWriter(stream, content);
            switch (result)
            {
                case null:
                    throw new InvalidOperationException($"The instance returned by {nameof(GetSerializer)} must not be a null value.");
                case XmlSerializer serializer:
                    serializer.Serialize(writer, value);
                    return;
                case XmlObjectSerializer serializer:
                    serializer.WriteObject(writer, value);
                    return;
                default:
                    throw new InvalidOperationException($"The instance of type '{result.GetType()}' returned by {nameof(GetSerializer)} must be an instance of either {nameof(XmlObjectSerializer)} or {nameof(XmlSerializer)}.");
                        
            }
        }

        public override Task WriteToStreamAsync(Type type, Object? value, Stream stream, HttpContent content, TransportContext? context, CancellationToken token)
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

            if (token.IsCancellationRequested)
            {
                return Task.FromCanceled(token);
            }

            try
            {
                WriteToStream(type, value, stream, content);
                return Task.CompletedTask;
            }
            catch (Exception exception)
            {
                return Task.FromException(exception);
            }
        }
    }
}