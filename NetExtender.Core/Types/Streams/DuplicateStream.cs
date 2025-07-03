using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Types.Exceptions;

namespace NetExtender.Types.Streams
{
    public class DuplicateStream : Stream
    {
        protected Stream Primary { get; }
        protected Stream Secondary { get; }

        public override Boolean CanSeek
        {
            get
            {
                return Primary.CanSeek;
            }
        }

        public override Boolean CanRead
        {
            get
            {
                return Primary.CanRead;
            }
        }

        public override Boolean CanWrite
        {
            get
            {
                return Primary.CanWrite;
            }
        }
        
        public override Int64 Length
        {
            get
            {
                return Primary.Length;
            }
        }

        public override Int64 Position
        {
            get
            {
                return Primary.Position;
            }
            set
            {
                Primary.Position = value;
                Secondary.Position = value;
            }
        }

        public override Boolean CanTimeout
        {
            get
            {
                return Primary.CanTimeout;
            }
        }

        public override Int32 ReadTimeout
        {
            get
            {
                return Primary.ReadTimeout;
            }
            set
            {
                Primary.ReadTimeout = value;
                Secondary.ReadTimeout = value;
            }
        }

        public override Int32 WriteTimeout
        {
            get
            {
                return Primary.WriteTimeout;
            }
            set
            {
                Primary.WriteTimeout = value;
                Secondary.WriteTimeout = value;
            }
        }

        public DuplicateStream(Stream primary, Stream secondary)
        {
            Primary = primary ?? throw new ArgumentNullException(nameof(primary));
            Secondary = secondary ?? throw new ArgumentNullException(nameof(secondary));
        }

        public override void SetLength(Int64 value)
        {
            Primary.SetLength(value);
            Secondary.SetLength(value);
        }

        public override Int64 Seek(Int64 offset, SeekOrigin origin)
        {
            Int64 result = Primary.Seek(offset, origin);
            
            if (result != Secondary.Seek(offset, origin))
            {
                throw new StreamSynchronizationException();
            }
            
            return result;
        }

        public override Int32 ReadByte()
        {
            Int32 result = Primary.ReadByte();
            
            if (result != Secondary.ReadByte())
            {
                throw new StreamSynchronizationException();
            }
            
            return result;
        }

        public override Int32 Read(Span<Byte> buffer)
        {
            Int32 result = Primary.Read(buffer);
            
            if (result != Secondary.Read(buffer))
            {
                throw new StreamSynchronizationException();
            }
            
            return result;
        }

        public override Int32 Read(Byte[] buffer, Int32 offset, Int32 count)
        {
            Int32 result = Primary.Read(buffer, offset, count);

            if (result != Secondary.Read(buffer, offset, count))
            {
                throw new StreamSynchronizationException();
            }
            
            return result;
        }

        public override async ValueTask<Int32> ReadAsync(Memory<Byte> buffer, CancellationToken token = default)
        {
            Int32 result = await Primary.ReadAsync(buffer, token);
            
            if (result != await Secondary.ReadAsync(buffer, token))
            {
                throw new StreamSynchronizationException();
            }
            
            return result;
        }

        [SuppressMessage("Performance", "CA1835:Prefer the \'Memory\'-based overloads for \'ReadAsync\' and \'WriteAsync\'")]
        public override async Task<Int32> ReadAsync(Byte[] buffer, Int32 offset, Int32 count, CancellationToken token)
        {
            Int32 result = await Primary.ReadAsync(buffer, offset, count, token);
            
            if (result != await Secondary.ReadAsync(buffer, offset, count, token))
            {
                throw new StreamSynchronizationException();
            }
            
            return result;
        }

        public override void Write(Byte[] buffer, Int32 offset, Int32 count)
        {
            Primary.Write(buffer, offset, count);
            Secondary.Write(buffer, offset, count);
        }

        public override void Write(ReadOnlySpan<Byte> buffer)
        {
            Primary.Write(buffer);
            Secondary.Write(buffer);
        }

        public override void WriteByte(Byte value)
        {
            Primary.WriteByte(value);
            Secondary.WriteByte(value);
        }

        public override async Task WriteAsync(Byte[] buffer, Int32 offset, Int32 count, CancellationToken token)
        {
            await Task.WhenAll(Primary.WriteAsync(buffer, offset, count, token), Secondary.WriteAsync(buffer, offset, count, token));
        }

        public override async ValueTask WriteAsync(ReadOnlyMemory<Byte> buffer, CancellationToken token = default)
        {
            await Primary.WriteAsync(buffer, token);
            await Secondary.WriteAsync(buffer, token);
        }

        public override void Flush()
        {
            Primary.Flush();
            Secondary.Flush();
        }

        public override async Task FlushAsync(CancellationToken token)
        {
            await Task.WhenAll(Primary.FlushAsync(token), Secondary.FlushAsync(token));
        }

        protected override void Dispose(Boolean disposing)
        {
            if (disposing)
            {
                Primary.Dispose();
                Secondary.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}