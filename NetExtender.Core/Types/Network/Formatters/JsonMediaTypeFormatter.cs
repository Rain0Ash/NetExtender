// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.Extensions.Logging;
using NetExtender.NewtonSoft.Types.Network.Formatters;
using NetExtender.Types.Streams;
using NetExtender.Utilities.Core;
using NetExtender.Utilities.Network.Formatters;
using NetExtender.Utilities.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace NetExtender.Types.Network.Formatters
{
    public abstract class JsonMediaTypeFormatterAbstraction : MediaTypeFormatter
    {
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

        protected IContractResolver ContractResolver { get; }
        private JsonSerializerSettings? _settings;
        public virtual JsonSerializerSettings Settings
        {
            get
            {
                return _settings ??= new JsonSerializerSettings
                {
                    ContractResolver = ContractResolver,
                    MissingMemberHandling = MissingMemberHandling.Ignore,
                    TypeNameHandling = TypeNameHandling.None
                };
            }
            set
            {
                _settings = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        protected JsonMediaTypeFormatterAbstraction()
        {
            ContractResolver = new JsonMediaTypeFormatterContractResolver(this);
            SupportedEncoding.Add(new UTF8Encoding(false, true));
            SupportedEncoding.Add(new UnicodeEncoding(false, true, true));
        }

        protected JsonMediaTypeFormatterAbstraction(JsonMediaTypeFormatterAbstraction formatter)
            : base(formatter)
        {
            ContractResolver = new JsonMediaTypeFormatterContractResolver(this);
            _maxdepth = formatter.MaxDepth;
            _settings = formatter._settings;
        }

        public override Boolean CanReadType(Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return true;
        }

        public override Boolean CanWriteType(Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return true;
        }
        
        public JsonReader? CreateJsonReader(Type type, Stream stream, Encoding encoding)
        {
            return TryCreateJsonReader(type, stream, encoding, out JsonReader? reader) ? reader : null;
        }
        
        public abstract Boolean TryCreateJsonReader(Type type, Stream stream, Encoding encoding, [MaybeNullWhen(false)] out JsonReader result);
        
        public JsonWriter? CreateJsonWriter(Type type, Stream stream, Encoding encoding)
        {
            return TryCreateJsonWriter(type, stream, encoding, out JsonWriter? writer) ? writer : null;
        }
        
        public abstract Boolean TryCreateJsonWriter(Type type, Stream stream, Encoding encoding, [MaybeNullWhen(false)] out JsonWriter result);

        protected virtual JsonSerializer CreateJsonSerializer()
        {
            try
            {
                return JsonSerializer.Create(Settings) ?? throw new InvalidOperationException($"The '{nameof(CreateJsonSerializer)}' method returned null. It must return a {nameof(JsonSerializer)} instance.");
            }
            catch (Exception exception) when (exception is not InvalidOperationException)
            {
                throw new InvalidOperationException($"The '{nameof(CreateJsonSerializer)}' method threw an exception when attempting to create a ${nameof(JsonSerializer)}.", exception);
            }
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
            
            Encoding encoding = SelectCharacterEncoding(headers);
            
            try
            {
                return ReadFromStream(type, stream, encoding, logger);
            }
            catch (Exception exception)
            {
                if (logger is null)
                {
                    throw;
                }

                logger.LogError("Exception: {0}", exception);
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

        public virtual Object? ReadFromStream(Type type, Stream stream, Encoding encoding, ILogger? logger)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            if (encoding is null)
            {
                throw new ArgumentNullException(nameof(encoding));
            }

            using JsonReader reader = CreateJsonReader(type, stream, encoding) ?? throw new InvalidOperationException($"The '{nameof(CreateJsonReader)}' method returned null. It must return a {nameof(JsonReader)} instance.");
            reader.CloseInput = false;
            reader.MaxDepth = MaxDepth;
            
            JsonSerializer serializer = CreateJsonSerializer();
            
            if (logger is null)
            {
                return serializer.Deserialize(reader, type);
            }

            void Handler(Object? _, Newtonsoft.Json.Serialization.ErrorEventArgs args)
            {
                Exception error = args.ErrorContext.Error;
                logger.LogError(args.ErrorContext.Path, error);
                args.ErrorContext.Handled = true;
            }
            
            serializer.Error += Handler;

            try
            {
                return serializer.Deserialize(reader, type);
            }
            finally
            {
                serializer.Error -= Handler;
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

            Encoding encoding = SelectCharacterEncoding(content.Headers);
            WriteToStream(type, value, stream, encoding);
        }
        
        public virtual void WriteToStream(Type type, Object? value, Stream stream, Encoding encoding)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            if (encoding is null)
            {
                throw new ArgumentNullException(nameof(encoding));
            }

            using JsonWriter writer = CreateJsonWriter(type, stream, encoding) ?? throw new InvalidOperationException($"The '{nameof(CreateJsonWriter)}' method returned null. It must return a {nameof(JsonWriter)} instance.");
            writer.CloseOutput = false;
            CreateJsonSerializer().Serialize(writer, value);
            writer.Flush();
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
    
    public class JsonMediaTypeFormatter : JsonMediaTypeFormatterAbstraction
    {
        protected static XsdDataContractExporter XsdExporter { get; } = new XsdDataContractExporter();
        
        public static MediaTypeHeaderValue DefaultMediaType
        {
            get
            {
                return MediaTypeFormatterUtilities.ApplicationJsonMediaType;
            }
        }

        protected ConcurrentDictionary<Type, DataContractJsonSerializer?> SerializerStorage { get; } = new ConcurrentDictionary<Type, DataContractJsonSerializer?>();
        
        protected RequestHeaderMapping? RequestHeaderMapping { get; }
        
        protected XmlDictionaryReaderQuotas Quotas { get; } = new XmlDictionaryReaderQuotas
        {
            MaxArrayLength = Int32.MaxValue,
            MaxBytesPerRead = Int32.MaxValue,
            MaxNameTableCharCount = Int32.MaxValue,
            MaxStringContentLength = Int32.MaxValue,
            MaxDepth = 256
        };
        
        public Boolean UseJsonSerializer { get; set; }
        public Boolean Indent { get; set; }

        public override Int32 MaxDepth
        {
            get
            {
                return base.MaxDepth;
            }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
                }
                
                base.MaxDepth = value;
                Quotas.MaxDepth = value;
            }
        }

        public JsonMediaTypeFormatter()
            : this(false)
        {
        }

        public JsonMediaTypeFormatter(Boolean text)
        {
            SupportedMediaType.Add(MediaTypeFormatterUtilities.ApplicationJsonMediaType);
            SupportedMediaType.Add(MediaTypeFormatterUtilities.TextJsonMediaType);
            SupportedMediaType.AddIf(MediaTypeFormatterUtilities.TextPlainMediaType, text);
            RequestHeaderMapping = new XmlRequestHeaderMapping();
            MediaTypeFormatterMapping.Add(RequestHeaderMapping);
        }

        protected JsonMediaTypeFormatter(JsonMediaTypeFormatter formatter)
            : base(formatter)
        {
            UseJsonSerializer = formatter.UseJsonSerializer;
            Indent = formatter.Indent;
            base.MaxDepth = formatter.MaxDepth;
            Quotas.MaxDepth = base.MaxDepth;
        }
        
        public override Boolean CanReadType(Type? type)
        {
            return type is not null && (UseJsonSerializer ? SerializerStorage.GetOrAdd(type, value => CreateDataContractSerializer(value, false)) is not null : base.CanReadType(type));
        }

        public override Boolean CanWriteType(Type? type)
        {
            if (type is null)
            {
                return false;
            }
            
            if (!UseJsonSerializer)
            {
                return base.CanWriteType(type);
            }

            return TryGetDelegatingType(typeof(IQueryable<>), ref type) && type is not null && SerializerStorage.GetOrAdd(type, value => CreateDataContractSerializer(value, false)) is not null;
        }

        public override Boolean TryCreateJsonReader(Type type, Stream stream, Encoding encoding, [MaybeNullWhen(false)] out JsonReader result)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            if (encoding is null)
            {
                throw new ArgumentNullException(nameof(encoding));
            }

            try
            {
                StreamReader reader = new StreamReader(stream, encoding);
                result = new JsonTextReader(reader);
                return true;
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
        }

        public override Boolean TryCreateJsonWriter(Type type, Stream stream, Encoding encoding, [MaybeNullWhen(false)] out JsonWriter result)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            if (encoding is null)
            {
                throw new ArgumentNullException(nameof(encoding));
            }

            try
            {
                StreamWriter writer = new StreamWriter(stream, encoding);
                result = Indent ? new JsonTextWriter(writer) { Formatting = Newtonsoft.Json.Formatting.Indented } : new JsonTextWriter(writer);
                return true;
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
        }
        
        public override Object? ReadFromStream(Type type, Stream stream, Encoding encoding, ILogger? logger)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            if (encoding is null)
            {
                throw new ArgumentNullException(nameof(encoding));
            }

            if (!UseJsonSerializer)
            {
                return base.ReadFromStream(type, stream, encoding, logger);
            }

            stream = String.Equals(encoding.WebName, Utf8Encoding.WebName, StringComparison.OrdinalIgnoreCase) ? new UnclosableStreamWrapper(stream) : new TranscodingStream(stream, encoding, Utf8Encoding, true);
            using XmlDictionaryReader reader = JsonReaderWriterFactory.CreateJsonReader(stream, Utf8Encoding, Quotas, null);
            return GetDataContractSerializer(type).ReadObject(reader);
        }

        public override void WriteToStream(Type type, Object? value, Stream stream, Encoding encoding)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            if (encoding is null)
            {
                throw new ArgumentNullException(nameof(encoding));
            }

            if (!UseJsonSerializer)
            {
                base.WriteToStream(type, value, stream, encoding);
                return;
            }

            Type? delegating = type;
            if (value is not null && TryGetDelegatingType(typeof(IQueryable<>), ref delegating) && DelegatingEnumerableConstructorCache.TryGetValue(type, out ConstructorInfo? constructor) && constructor is not null)
            {
                value = constructor.Invoke(new[] { value });
            }
            
            if (delegating is null)
            {
                throw new InvalidOperationException();
            }

            WritePreamble(stream, encoding);

            if (String.Equals(encoding.WebName, Utf8Encoding.WebName, StringComparison.OrdinalIgnoreCase))
            {
                WriteObject(stream, delegating, value);
                return;
            }

            using TranscodingStream transcoding = new TranscodingStream(stream, encoding, Utf8Encoding, true);
            WriteObject(transcoding, delegating, value);
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

            if (UseJsonSerializer && Indent)
            {
                throw new NotSupportedException($"Indentation is not supported by '{typeof(DataContractJsonSerializer)}'.");
            }

            return base.WriteToStreamAsync(type, value, stream, content, context, token);
        }

        private void WriteObject(Stream stream, Type type, Object? value)
        {
            using XmlWriter writer = JsonReaderWriterFactory.CreateJsonWriter(stream, Utf8Encoding, false);
            GetDataContractSerializer(type).WriteObject(writer, value);
        }

        protected virtual DataContractJsonSerializer GetDataContractSerializer(Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return SerializerStorage.GetOrAdd(type, value => CreateDataContractSerializer(value, true)) ?? throw new InvalidOperationException($"The '{nameof(DataContractJsonSerializer)}' serializer cannot serialize the type '{type}'.");
        }

        protected virtual DataContractJsonSerializer? CreateDataContractSerializer(Type type, Boolean @throw)
        {
            try
            {
                XsdExporter.GetRootElementName(type);
                return new DataContractJsonSerializer(type);
            }
            catch (Exception exception)
            {
                return !@throw ? null : throw new InvalidOperationException($"The '{nameof(DataContractJsonSerializer)}' serializer cannot serialize the type '{type}'.", exception);
            }
        }
    }
}