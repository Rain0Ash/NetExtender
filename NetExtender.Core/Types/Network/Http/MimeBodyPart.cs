// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Utilities.Network;

namespace NetExtender.Types.Network
{
    public class MimeBodyPart : IDisposable
    {
        protected Stream? Stream { get; set; }
        protected MultipartStreamProvider Provider { get; }
        protected HttpContent? Parent { get; set; }
        protected HttpContent? Content { get; set; }
        protected HttpContentHeaders Headers { get; }
        
        public InternetMessageFormatHeaderParser? Parser { get; private set; }
        public List<ArraySegment<Byte>> Segments { get; }
        public Boolean IsComplete { get; set; }
        public Boolean IsFinal { get; set; }

        public MimeBodyPart(MultipartStreamProvider provider, Int32 header, HttpContent? parent)
        {
            Provider = provider ?? throw new ArgumentNullException(nameof(provider));
            Parent = parent;
            Segments = new List<ArraySegment<Byte>>(2);
            
            using StringContent content = new StringContent(String.Empty);
            Headers = content.Headers;
            Headers.Clear();
            
            Parser = new InternetMessageFormatHeaderParser(Headers, header, true);
        }
        
        public HttpContent? GetHttpContent()
        {
            if (Content is null)
            {
                return null;
            }

            Headers.CopyTo(Content.Headers);
            return Content;
        }

        public async Task WriteSegment(ArraySegment<Byte> segment, CancellationToken token)
        {
            if (segment.Array is null)
            {
                return;
            }
            
            await GetOutputStream().WriteAsync(segment.Array, segment.Offset, segment.Count, token);
        }

        private Stream GetOutputStream()
        {
            if (Parent is null)
            {
                throw new ObjectDisposedException(nameof(MimeBodyPart));
            }
            
            if (Stream is not null)
            {
                return Stream;
            }

            try
            {
                Stream = Provider.GetStream(Parent, Headers);
            }
            catch (Exception exception)
            {
                throw new InvalidOperationException($"The stream provider of type '{Provider.GetType()}' threw an exception.", exception);
            }
            
            if (Stream is null)
            {
                throw new InvalidOperationException($"The stream provider of type '{Provider.GetType()}' returned null. It must return a writable '{nameof(System.IO.Stream)}' instance.");
            }

            if (!Stream.CanWrite)
            {
                throw new InvalidOperationException($"The stream provider of type '{Provider.GetType()}' returned a read-only stream. It must return a writable '{nameof(System.IO.Stream)}' instance.");
            }

            Content = new StreamContent(Stream);
            return Stream;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(Boolean disposing)
        {
            if (!disposing)
            {
                return;
            }

            switch (Stream)
            {
                case null:
                    break;
                case MemoryStream memory:
                    memory.Position = 0L;
                    Stream = null;
                    break;
                default:
                    Stream.Close();
                    Stream = null;
                    break;
            }
            
            if (!IsComplete && Content is not null)
            {
                Content.Dispose();
            }

            Content = null;
            
            Parent = null;
            Parser = null;
            Segments.Clear();
        }
    }
}