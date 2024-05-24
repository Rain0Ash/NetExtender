// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.IO;
using System.IO.Compression;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Types.Exceptions;
using NetExtender.Utilities.Cryptography;

namespace NetExtender.Cryptography.Compression
{
    // ReSharper disable once ClassWithVirtualMembersNeverInherited.Global
    public class CompressionStream : Stream
    {
        private Stream Compression { get; }

        public override Boolean CanRead
        {
            get
            {
                return Compression.CanRead;
            }
        }

        public override Boolean CanSeek
        {
            get
            {
                return Compression.CanSeek;
            }
        }

        public override Boolean CanTimeout
        {
            get
            {
                return Compression.CanTimeout;
            }
        }

        public override Boolean CanWrite
        {
            get
            {
                return Compression.CanWrite;
            }
        }

        public override Int64 Length
        {
            get
            {
                return Compression.Length;
            }
        }

        public override Int64 Position
        {
            get
            {
                return Compression.Position;
            }
            set
            {
                Compression.Position = value;
            }
        }

        public override Int32 ReadTimeout
        {
            get
            {
                return Compression.ReadTimeout;
            }
            set
            {
                Compression.ReadTimeout = value;
            }
        }

        public override Int32 WriteTimeout
        {
            get
            {
                return Compression.WriteTimeout;
            }
            set
            {
                Compression.WriteTimeout = value;
            }
        }

        public virtual Stream BaseStream
        {
            get
            {
                return Compression;
            }
        }

        public CompressionStream(Stream stream, CompressionType type, CompressionMode mode)
            : this(stream, type, mode, false)
        {
        }

        public CompressionStream(Stream stream, CompressionType type, CompressionMode mode, Boolean leaveOpen)
        {
            Compression = type switch
            {
                CompressionType.GZip => new GZipStream(stream, mode, leaveOpen),
                CompressionType.Deflate => new DeflateStream(stream, mode, leaveOpen),
                CompressionType.Brotli => new BrotliStream(stream, mode, leaveOpen),
                _ => throw new EnumUndefinedOrNotSupportedException<CompressionType>(type, nameof(type), null)
            };
        }

        public CompressionStream(Stream stream, CompressionType type, CompressionLevel level)
            : this(stream, type, level, false)
        {
        }

        public CompressionStream(Stream stream, CompressionType type, CompressionLevel level, Boolean leaveOpen)
        {
            Compression = type switch
            {
                CompressionType.GZip => new GZipStream(stream, level, leaveOpen),
                CompressionType.Deflate => new DeflateStream(stream, level, leaveOpen),
                CompressionType.Brotli => new BrotliStream(stream, level, leaveOpen),
                _ => throw new EnumUndefinedOrNotSupportedException<CompressionType>(type, nameof(type), null)
            };
        }

        public override Int64 Seek(Int64 offset, SeekOrigin origin)
        {
            return Compression.Seek(offset, origin);
        }

        public override void SetLength(Int64 value)
        {
            Compression.SetLength(value);
        }

        public override Int32 ReadByte()
        {
            return Compression.ReadByte();
        }

        public override Int32 Read(Span<Byte> buffer)
        {
            return Compression.Read(buffer);
        }

        public override Int32 Read(Byte[] buffer, Int32 offset, Int32 count)
        {
            return Compression.Read(buffer, offset, count);
        }

        public override ValueTask<Int32> ReadAsync(Memory<Byte> buffer, CancellationToken token = new CancellationToken())
        {
            return Compression.ReadAsync(buffer, token);
        }

        public override Task<Int32> ReadAsync(Byte[] buffer, Int32 offset, Int32 count, CancellationToken token)
        {
            return Compression.ReadAsync(buffer, offset, count, token);
        }

        public override IAsyncResult BeginRead(Byte[] buffer, Int32 offset, Int32 count, AsyncCallback? callback, Object? state)
        {
            return Compression.BeginRead(buffer, offset, count, callback, state);
        }

        public override Int32 EndRead(IAsyncResult asyncResult)
        {
            return Compression.EndRead(asyncResult);
        }

        public override void WriteByte(Byte value)
        {
            Compression.WriteByte(value);
        }

        public override void Write(ReadOnlySpan<Byte> buffer)
        {
            Compression.Write(buffer);
        }

        public override void Write(Byte[] buffer, Int32 offset, Int32 count)
        {
            Compression.Write(buffer, offset, count);
        }

        public override ValueTask WriteAsync(ReadOnlyMemory<Byte> buffer, CancellationToken token = new CancellationToken())
        {
            return Compression.WriteAsync(buffer, token);
        }

        public override Task WriteAsync(Byte[] buffer, Int32 offset, Int32 count, CancellationToken token)
        {
            return Compression.WriteAsync(buffer, offset, count, token);
        }

        public override IAsyncResult BeginWrite(Byte[] buffer, Int32 offset, Int32 count, AsyncCallback? callback, Object? state)
        {
            return Compression.BeginWrite(buffer, offset, count, callback, state);
        }

        public override void EndWrite(IAsyncResult asyncResult)
        {
            Compression.EndWrite(asyncResult);
        }

        public override void Flush()
        {
            Compression.Flush();
        }

        public override Task FlushAsync(CancellationToken token)
        {
            return Compression.FlushAsync(token);
        }

        public override void CopyTo(Stream destination, Int32 bufferSize)
        {
            Compression.CopyTo(destination, bufferSize);
        }

        public override Task CopyToAsync(Stream destination, Int32 bufferSize, CancellationToken token)
        {
            return Compression.CopyToAsync(destination, bufferSize, token);
        }

        public override void Close()
        {
            Compression.Close();
        }

        public override ValueTask DisposeAsync()
        {
            return Compression.DisposeAsync();
        }

        [Obsolete("This Remoting API is not supported and throws PlatformNotSupportedException.", DiagnosticId = "SYSLIB0010", UrlFormat = "https://aka.ms/dotnet-warnings/{0}")]
        public override Object InitializeLifetimeService()
        {
            return Compression.InitializeLifetimeService();
        }

        public override Boolean Equals(Object? other)
        {
            return Compression.Equals(other);
        }

        public override Int32 GetHashCode()
        {
            return Compression.GetHashCode();
        }

        public override String? ToString()
        {
            return Compression.ToString();
        }
    }
}