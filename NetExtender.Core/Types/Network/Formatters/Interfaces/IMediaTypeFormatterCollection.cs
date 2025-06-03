// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Text;

namespace NetExtender.Types.Network.Formatters.Interfaces
{
    public interface IMediaTypeFormatterCollection : IReadOnlyCollection<IMediaTypeFormatter>
    {
        public Boolean Contains(MediaTypeHeader media);
        public Boolean Contains(MediaTypeHeader media, Encoding encoding);
        
        public IMediaTypeFormatter? FindReader(Type type);
        public IMediaTypeFormatter? FindReader(Type type, MediaTypeHeader media);
        public IEnumerable<IMediaTypeFormatter> FindReaders(Type type);
        public IEnumerable<IMediaTypeFormatter> FindReaders(Type type, MediaTypeHeader media);
        public IMediaTypeFormatter? FindWriter(Type type);
        public IMediaTypeFormatter? FindWriter(Type type, MediaTypeHeader media);
        public IEnumerable<IMediaTypeFormatter> FindWriters(Type type);
        public IEnumerable<IMediaTypeFormatter> FindWriters(Type type, MediaTypeHeader media);
    }

    public interface IReadOnlyMediaTypeFormatterCollection : IReadOnlyCollection<IReadOnlyMediaTypeFormatter>
    {
        public Boolean Contains(MediaTypeHeader media);
        public Boolean Contains(MediaTypeHeader media, Encoding encoding);
        
        public IReadOnlyMediaTypeFormatter? FindReader(Type type);
        public IReadOnlyMediaTypeFormatter? FindReader(Type type, MediaTypeHeader media);
        public IEnumerable<IReadOnlyMediaTypeFormatter> FindReaders(Type type);
        public IEnumerable<IReadOnlyMediaTypeFormatter> FindReaders(Type type, MediaTypeHeader media);
        public IReadOnlyMediaTypeFormatter? FindWriter(Type type);
        public IReadOnlyMediaTypeFormatter? FindWriter(Type type, MediaTypeHeader media);
        public IEnumerable<IReadOnlyMediaTypeFormatter> FindWriters(Type type);
        public IEnumerable<IReadOnlyMediaTypeFormatter> FindWriters(Type type, MediaTypeHeader media);
    }
}