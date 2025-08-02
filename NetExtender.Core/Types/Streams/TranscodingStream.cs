// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Buffers;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Types.Exceptions;

namespace NetExtender.Types.Streams
{
    public sealed class TranscodingStream : Stream
    {
        private Byte[] ByteBuffer { get; } = new Byte[1];
        private Stream? Stream { get; set; }
        private Encoding StreamEncoding { get; }
        private Encoder? StreamEncoder { get; set; }
        private Decoder? StreamDecoder { get; set; }
        private Boolean LeaveOpen { get; }
        private Encoding Encoding { get; }
        private Encoder? Encoder { get; set; }
        private Decoder? Decoder { get; set; }
        private Int32 BufferMaxSize { get; set; }
        private Byte[]? Buffer { get; set; }
        private Int32 BufferCount { get; set; }
        private Int32 BufferOffset { get; set; }
        
        public override Boolean CanRead
        {
            get
            {
                if (Stream is null)
                {
                    throw new ThisObjectDisposedException(this);
                }
                
                return Stream.CanRead;
            }
        }

        public override Boolean CanSeek
        {
            get
            {
                if (Stream is null)
                {
                    throw new ThisObjectDisposedException(this);
                }
                
                return false;
            }
        }

        public override Boolean CanWrite
        {
            get
            {
                if (Stream is null)
                {
                    throw new ThisObjectDisposedException(this);
                }
                
                return Stream.CanWrite;
            }
        }

        public override Int64 Length
        {
            get
            {
                throw new NotSupportedException("Stream does not support seeking.");
            }
        }

        public override Int64 Position
        {
            get
            {
                throw new NotSupportedException("Stream does not support seeking.");
            }
            set
            {
                throw new NotSupportedException("Stream does not support seeking.");
            }
        }

        public TranscodingStream(Stream stream, Encoding inner, Encoding encoding)
            : this(stream, inner, encoding, false)
        {
        }

        public TranscodingStream(Stream stream, Encoding inner, Encoding encoding, Boolean leaveOpen)
        {
            Stream = stream ?? throw new ArgumentNullException(nameof(stream));
            StreamEncoding = inner ?? throw new ArgumentNullException(nameof(inner));
            Encoding = encoding ?? throw new ArgumentNullException(nameof(encoding));
            LeaveOpen = leaveOpen;
        }

        public override void SetLength(Int64 value)
        {
            throw new NotSupportedException("Stream does not support seeking.");
        }

        public override Int64 Seek(Int64 offset, SeekOrigin origin)
        {
            throw new NotSupportedException("Stream does not support seeking.");
        }

        public override Int32 ReadByte()
        {
            return Read(ByteBuffer, 0, 1) == 0 ? -1 : ByteBuffer[0];
        }

        public override Int32 Read(Span<Byte> buffer)
        {
            if (Stream is null)
            {
                throw new ThisObjectDisposedException(this);
            }
            
            if (!CanRead)
            {
                throw new NotSupportedException("Stream does not support reading.");
            }
            
            if (Encoder is null || Buffer is null || StreamDecoder is null)
            {
                Encoder = Encoding.GetEncoder();
                StreamDecoder = StreamEncoding.GetDecoder();
                BufferMaxSize = StreamEncoding.GetMaxCharCount(4096);
                Buffer = new Byte[Encoding.GetMaxByteCount(BufferMaxSize)];
            }
            
            if (BufferCount <= 0)
            {
                Byte[] array = ArrayPool<Byte>.Shared.Rent(4096);
                Char[] characters = ArrayPool<Char>.Shared.Rent(BufferMaxSize);
                
                try
                {
                    Boolean flush;
                    Int32 read;
                    do
                    {
                        Int32 counter = Stream.Read(array, 0, 4096);
                        flush = counter == 0;
                        Int32 written = StreamDecoder.GetChars(array, 0, counter, characters, 0, flush);
                        read = Encoder.GetBytes(characters, 0, written, Buffer, 0, flush);
                    } while (!flush && read <= 0);
                    
                    BufferOffset = 0;
                    BufferCount = read;
                }
                finally
                {
                    ArrayPool<Byte>.Shared.Return(array);
                    ArrayPool<Char>.Shared.Return(characters);
                }
            }
            
            Int32 count = Math.Min(BufferCount, buffer.Length);
            Buffer.AsSpan(BufferOffset, count).CopyTo(buffer);
            BufferOffset += count;
            BufferCount -= count;
            return count;
        }

        public override Int32 Read(Byte[] buffer, Int32 offset, Int32 count)
        {
            if (buffer is null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }

            if (offset < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(offset), offset, null);
            }

            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count), count, null);
            }

            if (count > buffer.Length - offset)
            {
                throw new ArgumentOutOfRangeException(nameof(count), count, "Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
            }
            
            return Read(new Span<Byte>(buffer, offset, count));
        }

        public override async ValueTask<Int32> ReadAsync(Memory<Byte> buffer, CancellationToken token = default)
        {
            if (Stream is null)
            {
                throw new ThisObjectDisposedException(this);
            }
            
            if (!CanRead)
            {
                throw new NotSupportedException("Stream does not support reading.");
            }

            if (token.IsCancellationRequested)
            {
                return await Task.FromCanceled<Int32>(token);
            }
            
            if (Encoder is null || Buffer is null || StreamDecoder is null)
            {
                Encoder = Encoding.GetEncoder();
                StreamDecoder = StreamEncoding.GetDecoder();
                BufferMaxSize = StreamEncoding.GetMaxCharCount(4096);
                Buffer = new Byte[Encoding.GetMaxByteCount(BufferMaxSize)];
            }

            Int32 count;
            if (BufferCount <= 0)
            {
                Char[] characters = ArrayPool<Char>.Shared.Rent(BufferMaxSize);
                Byte[] bytes = ArrayPool<Byte>.Shared.Rent(4096);

                try
                {
                    Boolean flush;
                    Int32 read;
                    do
                    {
                        count = await Stream.ReadAsync(bytes, 0, 4096, token).ConfigureAwait(false);
                        flush = count == 0;
                        Int32 chars = StreamDecoder.GetChars(bytes, 0, count, characters, 0, flush);
                        read = Encoder.GetBytes(characters, 0, chars, Buffer, 0, flush);
                    }
                    while (!flush && read == 0);
                    BufferOffset = 0;
                    BufferCount = read;
                }
                finally
                {
                    ArrayPool<Byte>.Shared.Return(bytes);
                    ArrayPool<Char>.Shared.Return(characters);
                }
            }
            
            count = Math.Min(BufferCount, buffer.Length);
            Buffer.AsSpan(BufferOffset, count).CopyTo(buffer.Span);
            BufferOffset += count;
            BufferCount -= count;
            return count;
        }

        public override Task<Int32> ReadAsync(Byte[] buffer, Int32 offset, Int32 count, CancellationToken token)
        {
            if (Stream is null)
            {
                throw new ThisObjectDisposedException(this);
            }
            
            if (!CanRead)
            {
                throw new NotSupportedException("Stream does not support reading.");
            }

            if (Encoder is not null && Buffer is not null && StreamDecoder is not null)
            {
                return ReadAsync(new Memory<Byte>(buffer, offset, count), token).AsTask();
            }

            Encoder = Encoding.GetEncoder();
            StreamDecoder = StreamEncoding.GetDecoder();
            BufferMaxSize = StreamEncoding.GetMaxCharCount(4096);
            Buffer = new Byte[Encoding.GetMaxByteCount(BufferMaxSize)];

            return ReadAsync(new Memory<Byte>(buffer, offset, count), token).AsTask();
        }

        public override void WriteByte(Byte value)
        {
            ByteBuffer[0] = value;
            Write(ByteBuffer, 0, 1);
        }
        
        public override void Write(ReadOnlySpan<Byte> buffer)
        {
            if (Stream is null)
            {
                throw new ThisObjectDisposedException(this);
            }
    
            if (!CanRead)
            {
                throw new NotSupportedException("Stream does not support reading.");
            }

            if (buffer.IsEmpty)
            {
                return;
            }
    
            if (StreamEncoder is null || Decoder is null)
            {
                StreamEncoder = StreamEncoding.GetEncoder();
                Decoder = Encoding.GetDecoder();
            }

            Int32 length = Math.Clamp(buffer.Length, 4096, 1048576);
            Char[] characters = ArrayPool<Char>.Shared.Rent(length);
            Byte[] array = ArrayPool<Byte>.Shared.Rent(length);

            try
            {
                Int32 bprocessed = 0;
                while (bprocessed < buffer.Length)
                {
                    Decoder.Convert(buffer.Slice(bprocessed), characters, false, out Int32 bused, out Int32 cused, out _);
                    bprocessed += bused;

                    Int32 cprocessed = 0;
                    while (cprocessed < cused)
                    {
                        StreamEncoder.Convert(characters.AsSpan(cprocessed, cused - cprocessed), array, false, out cused, out Int32 written, out _);
                        cprocessed += cused;
                        Stream.Write(array, 0, written);
                    }
                }
            }
            finally
            {
                ArrayPool<Char>.Shared.Return(characters);
                ArrayPool<Byte>.Shared.Return(array);
            }
        }

        // ReSharper disable once CognitiveComplexity
        public override void Write(Byte[] buffer, Int32 offset, Int32 count)
        {
            if (Stream is null)
            {
                throw new ThisObjectDisposedException(this);
            }
            
            if (!CanWrite)
            {
                throw new NotSupportedException("Stream does not support writing.");
            }
            
            if (buffer is null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }

            if (offset < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(offset), offset, null);
            }
            
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count), count, null);
            }

            if (count > buffer.Length - offset)
            {
                throw new ArgumentOutOfRangeException(nameof(count), count, "Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
            }
            
            if (count <= 0)
            {
                return;
            }
            
            if (StreamEncoder is null || Decoder is null)
            {
                StreamEncoder = StreamEncoding.GetEncoder();
                Decoder = Encoding.GetDecoder();
            }

            Int32 length = Math.Clamp(buffer.Length, 4096, 1048576);
            Char[] characters = ArrayPool<Char>.Shared.Rent(length);
            Byte[] array = ArrayPool<Byte>.Shared.Rent(length);
            
            try
            {
                Boolean outer;
                do
                {
                    Decoder.Convert(buffer, offset, count, characters, 0, length, false, out Int32 busedouter, out Int32 cusedouter, out outer);
                    offset += busedouter;
                    count -= busedouter;
                    Int32 index = 0;
                    Boolean inner;
                    do
                    {
                        StreamEncoder.Convert(characters, index, cusedouter, array, 0, length, false, out Int32 cusedinner, out Int32 busedinner, out inner);
                        index += cusedinner;
                        cusedouter -= cusedinner;
                        Stream.Write(array, 0, busedinner);
                    } while (!inner);
                } while (!outer);
            }
            finally
            {
                ArrayPool<Char>.Shared.Return(characters);
                ArrayPool<Byte>.Shared.Return(array);
            }
        }
        
        public override async ValueTask WriteAsync(ReadOnlyMemory<Byte> buffer, CancellationToken token = default)
        {
            if (Stream is null)
            {
                throw new ThisObjectDisposedException(this);
            }
    
            if (!CanWrite)
            {
                throw new NotSupportedException("Stream does not support writing.");
            }

            if (buffer.IsEmpty)
            {
                return;
            }
    
            if (token.IsCancellationRequested)
            {
                await Task.FromCanceled(token);
                return;
            }

            if (StreamEncoder is null || Decoder is null)
            {
                StreamEncoder = StreamEncoding.GetEncoder();
                Decoder = Encoding.GetDecoder();
            }

            Int32 length = Math.Clamp(buffer.Length, 4096, 1048576);
            Char[] characters = ArrayPool<Char>.Shared.Rent(length);
            Byte[] bytes = ArrayPool<Byte>.Shared.Rent(length);

            try
            {
                Int32 offset = 0;
                Int32 count = buffer.Length;

                while (count > 0)
                {
                    Decoder.Convert(buffer.Slice(offset, count).Span, characters, false, out Int32 bused, out Int32 cused, out _);
                    offset += bused;
                    count -= bused;

                    Int32 coffset = 0;
                    while (coffset < cused)
                    {
                        StreamEncoder.Convert(characters.AsSpan(coffset, cused - coffset), bytes, false, out cused, out Int32 written, out _);
                        coffset += cused;
                        await Stream.WriteAsync(new Memory<Byte>(bytes, 0, written), token).ConfigureAwait(false);
                    }
                }
            }
            finally
            {
                ArrayPool<Char>.Shared.Return(characters);
                ArrayPool<Byte>.Shared.Return(bytes);
            }
        }

        // ReSharper disable once CognitiveComplexity
        public override async Task WriteAsync(Byte[] buffer, Int32 offset, Int32 count, CancellationToken token)
        {
            if (Stream is null)
            {
                throw new ThisObjectDisposedException(this);
            }
            
            if (!CanWrite)
            {
                throw new NotSupportedException("Stream does not support writing.");
            }
            
            if (buffer is null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }

            if (offset < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(offset), offset, null);
            }
            
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count), count, null);
            }

            if (count > buffer.Length - offset)
            {
                throw new ArgumentOutOfRangeException(nameof(count), count, "Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");
            }
            
            if (token.IsCancellationRequested)
            {
                await Task.FromCanceled<Int32>(token);
                return;
            }
            
            if (count <= 0)
            {
                return;
            }
            
            if (StreamEncoder is null || Decoder is null)
            {
                StreamEncoder = StreamEncoding.GetEncoder();
                Decoder = Encoding.GetDecoder();
            }

            Int32 length = Math.Clamp(buffer.Length, 4096, 1048576);
            Char[] characters = ArrayPool<Char>.Shared.Rent(length);
            Byte[] bytes = ArrayPool<Byte>.Shared.Rent(length);
            try
            {
                Boolean outer;
                do
                {
                    Decoder.Convert(buffer, offset, count, characters, 0, length, false, out Int32 busedouter, out Int32 cusedouter, out outer);
                    offset += busedouter;
                    count -= busedouter;
                    Int32 index = 0;
                    Boolean inner;
                    do
                    {
                        StreamEncoder.Convert(characters, index, cusedouter, bytes, 0, length, false, out Int32 cusedinner, out Int32 busedinner, out inner);
                        index += cusedinner;
                        cusedouter -= cusedinner;
                        await Stream.WriteAsync(bytes, 0, busedinner, token).ConfigureAwait(false);
                    }
                    while (!inner);
                }
                while (!outer);
            }
            finally
            {
                ArrayPool<Char>.Shared.Return(characters);
                ArrayPool<Byte>.Shared.Return(bytes);
            }
        }
        
        public override void Flush()
        {
            if (Stream is null)
            {
                throw new ThisObjectDisposedException(this);
            }
            
            Stream.Flush();
        }

        public override async Task FlushAsync(CancellationToken token)
        {
            if (Stream is null)
            {
                throw new ThisObjectDisposedException(this);
            }
            
            await Stream.FlushAsync(token);
        }
        
        protected override void Dispose(Boolean disposing)
        {
            base.Dispose(disposing);
            
            if (Stream is null)
            {
                return;
            }

            ArraySegment<Byte> Segment()
            {
                if (Decoder is null || StreamEncoder is null)
                {
                    return new ArraySegment<Byte>();
                }

                Char[] characters = Array.Empty<Char>();
                Int32 length = Decoder.GetCharCount(Array.Empty<Byte>(), 0, 0, true);
                if (length > 0)
                {
                    characters = new Char[length];
                    length = Decoder.GetChars(Array.Empty<Byte>(), 0, 0, characters, 0, true);
                }
            
                Byte[] array = Array.Empty<Byte>();
                Int32 count = StreamEncoder.GetByteCount(characters, 0, length, true);
                if (count <= 0)
                {
                    return new ArraySegment<Byte>(array, 0, count);
                }

                array = new Byte[count];
                count = StreamEncoder.GetBytes(characters, 0, length, array, 0, true);
                return new ArraySegment<Byte>(array, 0, count);
            }

            ArraySegment<Byte> segment = Segment();
            if (segment.Count > 0 && segment.Array is not null)
            {
                Stream.Write(segment.Array, segment.Offset, segment.Count);
            }

            if (!LeaveOpen)
            {
                Stream.Dispose();
            }
            
            Stream = null;
        }
    }
}