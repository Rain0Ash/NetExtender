// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace NetExtender.Types.Network.Formatters.Interfaces
{
    public interface IMediaTypeFormatter : IReadOnlyMediaTypeFormatter
    {
        public new ICollection<MediaTypeHeader> SupportedMediaType { get; }
        public new ICollection<Encoding> SupportedEncoding { get; }
        public new ICollection<MediaTypeMapping> MediaTypeFormatterMapping { get; }
        
        public MediaTypeFormatter GetPerRequestFormatterInstance(Type type, HttpRequestMessage request, MediaTypeHeaderValue media);
        public Task<Object?> ReadFromStreamAsync(Type type, Stream stream, HttpContent content, ILogger? logger);
        public Task<Object?> ReadFromStreamAsync(Type type, Stream stream, HttpContent content, ILogger? logger, CancellationToken token);
        public Task WriteToStreamAsync(Type type, Object? value, Stream stream, HttpContent content, TransportContext? context);
        public Task WriteToStreamAsync(Type type, Object? value, Stream stream, HttpContent content, TransportContext? context, CancellationToken token);
        public Boolean SetDefaultContentHeaders(Type type, HttpContentHeaders headers, MediaTypeHeaderValue? media);
    }
    
    public interface IReadOnlyMediaTypeFormatter
    {
        public Int32 MaximumHttpCollectionKeys { get; }
        public Int32 MaxDepth { get; }
        public IReadOnlyCollection<MediaTypeHeader> SupportedMediaType { get; }
        public IReadOnlyCollection<Encoding> SupportedEncoding { get; }
        public IReadOnlyCollection<MediaTypeMapping> MediaTypeFormatterMapping { get; }
        public IFormatterMemberSelector? Selector { get; }
        
        public Boolean Contains(MediaTypeHeader header);
        public Boolean Contains(MediaTypeHeader header, Encoding encoding);
        public Boolean Contains(Encoding encoding);
        public Boolean Contains(MediaTypeMapping mapping);
        public Boolean CanReadType(Type type);
        public Boolean CanWriteType(Type type);
        public Encoding SelectCharacterEncoding(HttpContentHeaders headers);
    }
}