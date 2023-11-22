// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace NetExtender.Types.Streams
{
    public abstract class StreamWrapper : Stream
    {
        protected Stream Stream { get; }

        public override Boolean CanSeek
        {
            get
            {
                return Stream.CanSeek;
            }
        }

        public override Boolean CanRead
        {
            get
            {
                return Stream.CanRead;
            }
        }

        public override Boolean CanWrite
        {
            get
            {
                return Stream.CanWrite;
            }
        }

        public override Int64 Length
        {
            get
            {
                return Stream.Length;
            }
        }

        public override Int64 Position
        {
            get
            {
                return Stream.Position;
            }
            set
            {
                Stream.Position = value;
            }
        }

        public override Boolean CanTimeout
        {
            get
            {
                return Stream.CanTimeout;
            }
        }

        public override Int32 ReadTimeout
        {
            get
            {
                return Stream.ReadTimeout;
            }
            set
            {
                Stream.ReadTimeout = value;
            }
        }

        public override Int32 WriteTimeout
        {
            get
            {
                return Stream.WriteTimeout;
            }
            set
            {
                Stream.WriteTimeout = value;
            }
        }
        
        protected StreamWrapper(Stream stream)
        {
            Stream = stream ?? throw new ArgumentNullException(nameof(stream));
        }

        public override void SetLength(Int64 value)
        {
            Stream.SetLength(value);
        }

        public override Int64 Seek(Int64 offset, SeekOrigin origin)
        {
            return Stream.Seek(offset, origin);
        }

        public override Int32 ReadByte()
        {
            return Stream.ReadByte();
        }

        public override Int32 Read(Span<Byte> buffer)
        {
            return Stream.Read(buffer);
        }

        public override ValueTask<Int32> ReadAsync(Memory<Byte> buffer, CancellationToken token = default)
        {
            return Stream.ReadAsync(buffer, token);
        }

        public override Int32 Read(Byte[] buffer, Int32 offset, Int32 count)
        {
            return Stream.Read(buffer, offset, count);
        }

        public override Task<Int32> ReadAsync(Byte[] buffer, Int32 offset, Int32 count, CancellationToken token)
        {
            return Stream.ReadAsync(buffer, offset, count, token);
        }

        public override void WriteByte(Byte value)
        {
            Stream.WriteByte(value);
        }

        public override void Write(ReadOnlySpan<Byte> buffer)
        {
            Stream.Write(buffer);
        }

        public override ValueTask WriteAsync(ReadOnlyMemory<Byte> buffer, CancellationToken token = default)
        {
            return Stream.WriteAsync(buffer, token);
        }

        public override void Write(Byte[] buffer, Int32 offset, Int32 count)
        {
            Stream.Write(buffer, offset, count);
        }

        public override Task WriteAsync(Byte[] buffer, Int32 offset, Int32 count, CancellationToken token)
        {
            return Stream.WriteAsync(buffer, offset, count, token);
        }

        public override IAsyncResult BeginRead(Byte[] buffer, Int32 offset, Int32 count, AsyncCallback? callback, Object? state)
        {
            return Stream.BeginRead(buffer, offset, count, callback, state);
        }

        public override IAsyncResult BeginWrite(Byte[] buffer, Int32 offset, Int32 count, AsyncCallback? callback, Object? state)
        {
            return Stream.BeginWrite(buffer, offset, count, callback, state);
        }

        public override Int32 EndRead(IAsyncResult result)
        {
            return Stream.EndRead(result);
        }

        public override void EndWrite(IAsyncResult result)
        {
            Stream.EndWrite(result);
        }

        public override void Flush()
        {
            Stream.Flush();
        }

        public override Task FlushAsync(CancellationToken cancellationToken)
        {
            return Stream.FlushAsync(cancellationToken);
        }

        public override void Close()
        {
            Stream.Close();
        }

        public override void CopyTo(Stream destination, Int32 buffer)
        {
            Stream.CopyTo(destination, buffer);
        }

        public override Task CopyToAsync(Stream destination, Int32 buffer, CancellationToken token)
        {
            return Stream.CopyToAsync(destination, buffer, token);
        }

        [Obsolete("Obsolete")]
        public override Object InitializeLifetimeService()
        {
            return Stream.InitializeLifetimeService();
        }

        public override Int32 GetHashCode()
        {
            return Stream.GetHashCode();
        }

        public override Boolean Equals(Object? obj)
        {
            return Stream.Equals(obj);
        }

        public override String? ToString()
        {
            return Stream.ToString();
        }

        protected override void Dispose(Boolean disposing)
        {
            if (disposing)
            {
                Stream.Dispose();
            }

            base.Dispose(disposing);
        }

        public override async ValueTask DisposeAsync()
        {
            await Stream.DisposeAsync();
            GC.SuppressFinalize(this);
        }
    }
}