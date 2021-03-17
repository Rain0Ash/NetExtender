// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.IO;
using System.IO.Compression;
using System.Threading;
using System.Threading.Tasks;

namespace NetExtender.Crypto.Compression
{
    // ReSharper disable once ClassWithVirtualMembersNeverInherited.Global
    public class CompressionStream : Stream
    {
        private readonly Stream _compression;

        public override Boolean CanRead
        {
            get
            {
                return _compression.CanRead;
            }
        }

        public override Boolean CanSeek
        {
            get
            {
                return _compression.CanSeek;
            }
        }

        public override Boolean CanTimeout
        {
            get
            {
                return _compression.CanTimeout;
            }
        }

        public override Boolean CanWrite
        {
            get
            {
                return _compression.CanWrite;
            }
        }

        public override Int64 Length
        {
            get
            {
                return _compression.Length;
            }
        }

        public override Int64 Position
        {
            get
            {
                return _compression.Position;
            }
            set
            {
                _compression.Position = value;
            }
        }

        public override Int32 ReadTimeout
        {
            get
            {
                return _compression.ReadTimeout;
            }
            set
            {
                _compression.ReadTimeout = value;
            }
        }

        public override Int32 WriteTimeout
        {
            get
            {
                return _compression.WriteTimeout;
            }
            set
            {
                _compression.WriteTimeout = value;
            }
        }

        public virtual Stream BaseStream
        {
            get
            {
                return _compression;
            }
        }

        public CompressionStream(Stream stream, CompressionType type, CompressionMode mode, Boolean leaveOpen = false)
        {
            _compression = type switch
            {
                CompressionType.GZip => new GZipStream(stream, mode, leaveOpen),
                CompressionType.Deflate => new DeflateStream(stream, mode, leaveOpen),
                CompressionType.Brotli => new BrotliStream(stream, mode, leaveOpen),
                _ => throw new NotSupportedException()
            };
        }
        
        public CompressionStream(Stream stream, CompressionType type, CompressionLevel level, Boolean leaveOpen = false)
        {
            _compression = type switch
            {
                CompressionType.GZip => new GZipStream(stream, level, leaveOpen),
                CompressionType.Deflate => new DeflateStream(stream, level, leaveOpen),
                CompressionType.Brotli => new BrotliStream(stream, level, leaveOpen),
                _ => throw new NotSupportedException()
            };
        }
        
        public override Int64 Seek(Int64 offset, SeekOrigin origin)
        {
            return _compression.Seek(offset, origin);
        }

        public override void SetLength(Int64 value)
        {
            _compression.SetLength(value);
        }
        
        public override Int32 ReadByte()
        {
            return _compression.ReadByte();
        }
        
        public override Int32 Read(Span<Byte> buffer)
        {
            return _compression.Read(buffer);
        }
        
        public override Int32 Read(Byte[] buffer, Int32 offset, Int32 count)
        {
            return _compression.Read(buffer, offset, count);
        }
        
        public override ValueTask<Int32> ReadAsync(Memory<Byte> buffer, CancellationToken token = new CancellationToken())
        {
            return _compression.ReadAsync(buffer, token);
        }
        
        public override Task<Int32> ReadAsync(Byte[] buffer, Int32 offset, Int32 count, CancellationToken token)
        {
            return _compression.ReadAsync(buffer, offset, count, token);
        }

        public override IAsyncResult BeginRead(Byte[] buffer, Int32 offset, Int32 count, AsyncCallback callback, Object? state)
        {
            return _compression.BeginRead(buffer, offset, count, callback, state);
        }
        
        public override Int32 EndRead(IAsyncResult asyncResult)
        {
            return _compression.EndRead(asyncResult);
        }
        
        public override void WriteByte(Byte value)
        {
            _compression.WriteByte(value);
        }
        
        public override void Write(ReadOnlySpan<Byte> buffer)
        {
            _compression.Write(buffer);
        }
        
        public override void Write(Byte[] buffer, Int32 offset, Int32 count)
        {
            _compression.Write(buffer, offset, count);
        }
        
        public override ValueTask WriteAsync(ReadOnlyMemory<Byte> buffer, CancellationToken token = new CancellationToken())
        {
            return _compression.WriteAsync(buffer, token);
        }

        public override Task WriteAsync(Byte[] buffer, Int32 offset, Int32 count, CancellationToken token)
        {
            return _compression.WriteAsync(buffer, offset, count, token);
        }

        public override IAsyncResult BeginWrite(Byte[] buffer, Int32 offset, Int32 count, AsyncCallback callback, Object? state)
        {
            return _compression.BeginWrite(buffer, offset, count, callback, state);
        }

        public override void EndWrite(IAsyncResult asyncResult)
        {
            _compression.EndWrite(asyncResult);
        }

        public override void Flush()
        {
            _compression.Flush();
        }

        public override Task FlushAsync(CancellationToken token)
        {
            return _compression.FlushAsync(token);
        }

        public override void CopyTo(Stream destination, Int32 bufferSize)
        {
            _compression.CopyTo(destination, bufferSize);
        }

        public override Task CopyToAsync(Stream destination, Int32 bufferSize, CancellationToken token)
        {
            return _compression.CopyToAsync(destination, bufferSize, token);
        }
        
        public override void Close()
        {
            _compression.Close();
        }

        public override ValueTask DisposeAsync()
        {
            return _compression.DisposeAsync();
        }

        [Obsolete("This Remoting API is not supported and throws PlatformNotSupportedException.", DiagnosticId = "SYSLIB0010", UrlFormat = "https://aka.ms/dotnet-warnings/{0}")]
        public override Object InitializeLifetimeService()
        {
            return _compression.InitializeLifetimeService();
        }

        public override Boolean Equals(Object? obj)
        {
            return _compression.Equals(obj);
        }

        public override Int32 GetHashCode()
        {
            return _compression.GetHashCode();
        }

        public override String ToString()
        {
            return _compression.ToString();
        }
    }
}