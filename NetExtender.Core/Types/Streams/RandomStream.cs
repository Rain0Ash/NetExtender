// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Types.Random;
using NetExtender.Types.Random.Interfaces;
using NetExtender.Utilities.Numerics;

namespace NetExtender.Types.Streams
{
    public class RandomStream : Stream
    {
        private IRandom Random { get; }

        public override Boolean CanRead
        {
            get
            {
                return true;
            }
        }

        public override Boolean CanSeek
        {
            get
            {
                return false;
            }
        }

        public override Boolean CanWrite
        {
            get
            {
                return false;
            }
        }

        public override Int64 Length
        {
            get
            {
                throw new NotSupportedException();
            }
        }

        public override Int64 Position
        {
            get
            {
                throw new NotSupportedException();
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        public RandomStream()
            : this(new RandomAdapter())
        {
        }

        public RandomStream(Int32 seed)
            : this(new RandomAdapter(seed))
        {
        }

        public RandomStream(System.Random random)
            : this(new RandomAdapter(random))
        {
        }

        public RandomStream(IRandom random)
        {
            Random = random ?? throw new ArgumentNullException(nameof(random));
        }

        public override Int64 Seek(Int64 offset, SeekOrigin origin)
        {
            throw new NotSupportedException();
        }

        public override void SetLength(Int64 value)
        {
            throw new NotSupportedException();
        }

        public override Int32 ReadByte()
        {
            return Random.NextByte();
        }

        public override Int32 Read(Span<Byte> buffer)
        {
            Random.NextBytes(buffer);
            return buffer.Length;
        }

        public override Int32 Read(Byte[] buffer, Int32 offset, Int32 count)
        {
            Random.NextBytes(buffer.AsSpan(offset, count));
            return count;
        }

        public override ValueTask<Int32> ReadAsync(Memory<Byte> buffer, CancellationToken token = default)
        {
            if (token.IsCancellationRequested)
            {
                return ValueTask.FromCanceled<Int32>(token);
            }

            Read(buffer.Span);
            return ValueTask.FromResult(buffer.Length);
        }

        public override Task<Int32> ReadAsync(Byte[] buffer, Int32 offset, Int32 count, CancellationToken token)
        {
            if (token.IsCancellationRequested)
            {
                return Task.FromCanceled<Int32>(token);
            }

            Read(buffer, offset, count);
            return Task.FromResult(count);
        }

        public override void WriteByte(Byte value)
        {
            throw new NotSupportedException();
        }

        public override void Write(ReadOnlySpan<Byte> buffer)
        {
            throw new NotSupportedException();
        }

        public override void Write(Byte[] buffer, Int32 offset, Int32 count)
        {
            throw new NotSupportedException();
        }

        public override ValueTask WriteAsync(ReadOnlyMemory<Byte> buffer, CancellationToken token = default)
        {
            throw new NotSupportedException();
        }

        public override Task WriteAsync(Byte[] buffer, Int32 offset, Int32 count, CancellationToken token)
        {
            throw new NotSupportedException();
        }

        public override void Flush()
        {
        }

        protected override void Dispose(Boolean disposing)
        {
            Random.Dispose();
        }
    }
}