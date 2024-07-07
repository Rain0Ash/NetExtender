// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Buffers;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.IO;

namespace NetExtender.Types.Streams
{
    public class RecyclableMemoryStream
    {
        [return: NotNullIfNotNull("stream")]
        public static implicit operator MemoryStream?(RecyclableMemoryStream? stream)
        {
            return stream?.Stream;
        }

        [return: NotNullIfNotNull("stream")]
        public static implicit operator Microsoft.IO.RecyclableMemoryStream?(RecyclableMemoryStream? stream)
        {
            return stream?.Stream;
        }

        protected Microsoft.IO.RecyclableMemoryStream Stream { get; }

        /// <inheritdoc cref="Microsoft.IO.RecyclableMemoryStream.Length"/>
        public Int64 Length
        {
            get
            {
                return Stream.Length;
            }
        }

        /// <inheritdoc cref="Microsoft.IO.RecyclableMemoryStream.Position"/>
        public Int64 Position
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

        /// <inheritdoc cref="Microsoft.IO.RecyclableMemoryStream.Capacity"/>
        public Int32 Capacity
        {
            get
            {
                return Stream.Capacity;
            }
            set
            {
                Stream.Capacity = value;
            }
        }

        /// <inheritdoc cref="Microsoft.IO.RecyclableMemoryStream.Capacity64"/>
        public Int64 LongCapacity
        {
            get
            {
                return Stream.Capacity64;
            }
            set
            {
                Stream.Capacity64 = value;
            }
        }

        /// <inheritdoc cref="Microsoft.IO.RecyclableMemoryStream.ReadTimeout"/>
        public Int32 ReadTimeout
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

        /// <inheritdoc cref="Microsoft.IO.RecyclableMemoryStream.WriteTimeout"/>
        public Int32 WriteTimeout
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

        /// <inheritdoc cref="Microsoft.IO.RecyclableMemoryStream.CanRead"/>
        public Boolean CanRead
        {
            get
            {
                return Stream.CanRead;
            }
        }

        /// <inheritdoc cref="Microsoft.IO.RecyclableMemoryStream.CanSeek"/>
        public Boolean CanSeek
        {
            get
            {
                return Stream.CanSeek;
            }
        }

        /// <inheritdoc cref="Microsoft.IO.RecyclableMemoryStream.CanTimeout"/>
        public Boolean CanTimeout
        {
            get
            {
                return Stream.CanTimeout;
            }
        }

        /// <inheritdoc cref="Microsoft.IO.RecyclableMemoryStream.CanWrite"/>
        public Boolean CanWrite
        {
            get
            {
                return Stream.CanWrite;
            }
        }

        public RecyclableMemoryStream(RecyclableMemoryStreamManager manager)
        {
            if (manager is null)
            {
                throw new ArgumentNullException(nameof(manager));
            }

            Stream = new Microsoft.IO.RecyclableMemoryStream(manager);
        }

        public RecyclableMemoryStream(RecyclableMemoryStreamManager manager, Int64 size)
        {
            if (manager is null)
            {
                throw new ArgumentNullException(nameof(manager));
            }

            Stream = new Microsoft.IO.RecyclableMemoryStream(manager, null, size);
        }

        public RecyclableMemoryStream()
            : this(new RecyclableMemoryStreamManager())
        {
        }

        public RecyclableMemoryStream(RecyclableMemoryStreamManager.Options options)
            : this(new RecyclableMemoryStreamManager(options))
        {
        }

        /// <inheritdoc cref="Microsoft.IO.RecyclableMemoryStream.SetLength"/>
        public void SetLength(Int64 value)
        {
            Stream.SetLength(value);
        }

        /// <inheritdoc cref="Microsoft.IO.RecyclableMemoryStream.Seek"/>
        public Int64 Seek(Int64 offset, SeekOrigin origin)
        {
            return Stream.Seek(offset, origin);
        }

        /// <inheritdoc cref="Microsoft.IO.RecyclableMemoryStream.ReadByte"/>
        public Int32 ReadByte()
        {
            return Stream.ReadByte();
        }

        /// <inheritdoc cref="Microsoft.IO.RecyclableMemoryStream.SafeReadByte(ref Int64)"/>
        public Int32 SafeReadByte(ref Int64 position)
        {
            return Stream.SafeReadByte(ref position);
        }

        /// <inheritdoc cref="Microsoft.IO.RecyclableMemoryStream.Read(Byte[],Int32,Int32)"/>
        public Int32 Read(Byte[] buffer, Int32 offset, Int32 count)
        {
            return Stream.Read(buffer, offset, count);
        }

        /// <inheritdoc cref="Microsoft.IO.RecyclableMemoryStream.SafeRead(Byte[],Int32,Int32,ref Int64)"/>
        public Int32 Read(Byte[] buffer, Int32 offset, Int32 count, ref Int64 position)
        {
            return Stream.SafeRead(buffer, offset, count, ref position);
        }

        /// <inheritdoc cref="Microsoft.IO.RecyclableMemoryStream.Read(Span{Byte})"/>
        public Int32 Read(Span<Byte> buffer)
        {
            return Stream.Read(buffer);
        }

        /// <inheritdoc cref="Microsoft.IO.RecyclableMemoryStream.SafeRead(Span{Byte},ref Int64)"/>
        public Int32 Read(Span<Byte> buffer, ref Int64 position)
        {
            return Stream.SafeRead(buffer, ref position);
        }

        /// <inheritdoc cref="Microsoft.IO.RecyclableMemoryStream.ReadAsync(Byte[],Int32,Int32)"/>
        public Task<Int32> ReadAsync(Byte[] buffer, Int32 offset, Int32 count)
        {
            return Stream.ReadAsync(buffer, offset, count);
        }

        /// <inheritdoc cref="Microsoft.IO.RecyclableMemoryStream.ReadAsync(Byte[],Int32,Int32,CancellationToken)"/>
        public Task<Int32> ReadAsync(Byte[] buffer, Int32 offset, Int32 count, CancellationToken token)
        {
            return Stream.ReadAsync(buffer, offset, count, token);
        }

        /// <inheritdoc cref="Microsoft.IO.RecyclableMemoryStream.ReadAsync(Memory{Byte},CancellationToken)"/>
        public ValueTask<Int32> ReadAsync(Memory<Byte> destination)
        {
            return Stream.ReadAsync(destination);
        }

        /// <inheritdoc cref="Microsoft.IO.RecyclableMemoryStream.ReadAsync(Memory{Byte},CancellationToken)"/>
        public ValueTask<Int32> ReadAsync(Memory<Byte> destination, CancellationToken token)
        {
            return Stream.ReadAsync(destination, token);
        }

        /// <inheritdoc cref="Microsoft.IO.RecyclableMemoryStream.WriteByte"/>
        public void WriteByte(Byte value)
        {
            Stream.WriteByte(value);
        }

        /// <inheritdoc cref="Microsoft.IO.RecyclableMemoryStream.Write(Byte[],Int32,Int32)"/>
        public void Write(Byte[] buffer, Int32 offset, Int32 count)
        {
            Stream.Write(buffer, offset, count);
        }

        /// <inheritdoc cref="Microsoft.IO.RecyclableMemoryStream.Write(ReadOnlySpan{Byte})"/>
        public void Write(ReadOnlySpan<Byte> source)
        {
            Stream.Write(source);
        }

        /// <inheritdoc cref="Microsoft.IO.RecyclableMemoryStream.WriteTo(System.IO.Stream)"/>
        public void WriteTo(Stream stream)
        {
            Stream.WriteTo(stream);
        }

        /// <inheritdoc cref="Microsoft.IO.RecyclableMemoryStream.WriteTo(System.IO.Stream,Int64,Int64)"/>
        public void WriteTo(Stream stream, Int64 offset, Int64 count)
        {
            Stream.WriteTo(stream, offset, count);
        }

        /// <inheritdoc cref="Microsoft.IO.RecyclableMemoryStream.WriteTo(Byte[])"/>
        public void WriteTo(Byte[] buffer)
        {
            Stream.WriteTo(buffer);
        }

        /// <inheritdoc cref="Microsoft.IO.RecyclableMemoryStream.WriteTo(Byte[],Int64,Int64)"/>
        public void WriteTo(Byte[] buffer, Int64 offset, Int64 count)
        {
            Stream.WriteTo(buffer, offset, count);
        }

        /// <inheritdoc cref="Microsoft.IO.RecyclableMemoryStream.WriteTo(Byte[],Int64,Int64,Int32)"/>
        public void WriteTo(Byte[] buffer, Int64 offset, Int64 count, Int32 targetOffset)
        {
            Stream.WriteTo(buffer, offset, count, targetOffset);
        }

        /// <inheritdoc cref="Microsoft.IO.RecyclableMemoryStream.WriteAsync(Byte[],Int32,Int32)"/>
        public Task WriteAsync(Byte[] buffer, Int32 offset, Int32 count)
        {
            return Stream.WriteAsync(buffer, offset, count);
        }

        /// <inheritdoc cref="Microsoft.IO.RecyclableMemoryStream.WriteAsync(Byte[],Int32,Int32,CancellationToken)"/>
        public Task WriteAsync(Byte[] buffer, Int32 offset, Int32 count, CancellationToken token)
        {
            return Stream.WriteAsync(buffer, offset, count, token);
        }

        /// <inheritdoc cref="Microsoft.IO.RecyclableMemoryStream.WriteAsync(ReadOnlyMemory{Byte},CancellationToken)"/>
        public ValueTask WriteAsync(ReadOnlyMemory<Byte> source)
        {
            return Stream.WriteAsync(source);
        }

        /// <inheritdoc cref="Microsoft.IO.RecyclableMemoryStream.WriteAsync(ReadOnlyMemory{Byte},CancellationToken)"/>
        public ValueTask WriteAsync(ReadOnlyMemory<Byte> source, CancellationToken token)
        {
            return Stream.WriteAsync(source, token);
        }

        /// <inheritdoc cref="Microsoft.IO.RecyclableMemoryStream.BeginRead(Byte[],Int32,Int32,AsyncCallback,Object)"/>
        public IAsyncResult BeginRead(Byte[] buffer, Int32 offset, Int32 count, AsyncCallback? callback, Object? state)
        {
            return Stream.BeginRead(buffer, offset, count, callback, state);
        }

        /// <inheritdoc cref="Microsoft.IO.RecyclableMemoryStream.EndRead"/>
        public Int32 EndRead(IAsyncResult result)
        {
            return Stream.EndRead(result);
        }

        /// <inheritdoc cref="Microsoft.IO.RecyclableMemoryStream.BeginWrite(Byte[],Int32,Int32,AsyncCallback,Object)"/>
        public IAsyncResult BeginWrite(Byte[] buffer, Int32 offset, Int32 count, AsyncCallback? callback, Object? state)
        {
            return Stream.BeginWrite(buffer, offset, count, callback, state);
        }

        /// <inheritdoc cref="Microsoft.IO.RecyclableMemoryStream.EndWrite"/>
        public void EndWrite(IAsyncResult result)
        {
            Stream.EndWrite(result);
        }

        /// <inheritdoc cref="Microsoft.IO.RecyclableMemoryStream.Advance"/>
        public void Advance(Int32 count)
        {
            Stream.Advance(count);
        }

        /// <inheritdoc cref="Microsoft.IO.RecyclableMemoryStream.CopyTo(System.IO.Stream)"/>
        public void CopyTo(Stream destination)
        {
            Stream.CopyTo(destination);
        }

        /// <inheritdoc cref="Microsoft.IO.RecyclableMemoryStream.CopyTo(System.IO.Stream,Int32)"/>
        public void CopyTo(Stream destination, Int32 bufferSize)
        {
            Stream.CopyTo(destination, bufferSize);
        }

        /// <inheritdoc cref="Microsoft.IO.RecyclableMemoryStream.CopyToAsync(System.IO.Stream)"/>
        public Task CopyToAsync(Stream destination)
        {
            return Stream.CopyToAsync(destination);
        }

        /// <inheritdoc cref="Microsoft.IO.RecyclableMemoryStream.CopyToAsync(System.IO.Stream,Int32)"/>
        public Task CopyToAsync(Stream destination, Int32 bufferSize)
        {
            return Stream.CopyToAsync(destination, bufferSize);
        }

        /// <inheritdoc cref="Microsoft.IO.RecyclableMemoryStream.CopyToAsync(System.IO.Stream,CancellationToken)"/>
        public Task CopyToAsync(Stream destination, CancellationToken token)
        {
            return Stream.CopyToAsync(destination, token);
        }

        /// <inheritdoc cref="Microsoft.IO.RecyclableMemoryStream.CopyToAsync(System.IO.Stream,Int32,CancellationToken)"/>
        public Task CopyToAsync(Stream destination, Int32 bufferSize, CancellationToken token)
        {
            return Stream.CopyToAsync(destination, bufferSize, token);
        }

        /// <inheritdoc cref="Microsoft.IO.RecyclableMemoryStream.GetBuffer"/>
        public Byte[] GetBuffer()
        {
            return Stream.GetBuffer();
        }

        /// <inheritdoc cref="Microsoft.IO.RecyclableMemoryStream.TryGetBuffer"/>
        public Boolean TryGetBuffer(out ArraySegment<Byte> buffer)
        {
            return Stream.TryGetBuffer(out buffer);
        }

        /// <inheritdoc cref="Microsoft.IO.RecyclableMemoryStream.GetMemory"/>
        public Memory<Byte> GetMemory()
        {
            return Stream.GetMemory();
        }

        /// <inheritdoc cref="Microsoft.IO.RecyclableMemoryStream.GetMemory"/>
        public Memory<Byte> GetMemory(Int32 size)
        {
            return Stream.GetMemory(size);
        }

        /// <inheritdoc cref="Microsoft.IO.RecyclableMemoryStream.GetSpan"/>
        public Span<Byte> GetSpan()
        {
            return Stream.GetSpan();
        }

        /// <inheritdoc cref="Microsoft.IO.RecyclableMemoryStream.GetSpan"/>
        public Span<Byte> GetSpan(Int32 size)
        {
            return Stream.GetSpan(size);
        }

        /// <inheritdoc cref="Microsoft.IO.RecyclableMemoryStream.GetReadOnlySequence"/>
        public ReadOnlySequence<Byte> GetReadOnlySequence()
        {
            return Stream.GetReadOnlySequence();
        }

        /// <inheritdoc cref="Microsoft.IO.RecyclableMemoryStream.ToArray"/>
        [Obsolete]
        public Byte[] ToArray()
        {
            return Stream.ToArray();
        }

        /// <inheritdoc cref="Microsoft.IO.RecyclableMemoryStream.Flush"/>
        public void Flush()
        {
            Stream.Flush();
        }

        /// <inheritdoc cref="Microsoft.IO.RecyclableMemoryStream.FlushAsync(CancellationToken)"/>
        public Task FlushAsync()
        {
            return Stream.FlushAsync();
        }

        /// <inheritdoc cref="Microsoft.IO.RecyclableMemoryStream.FlushAsync(CancellationToken)"/>
        public Task FlushAsync(CancellationToken token)
        {
            return Stream.FlushAsync(token);
        }

        /// <inheritdoc cref="Microsoft.IO.RecyclableMemoryStream.Close"/>
        public void Close()
        {
            Stream.Close();
        }

        /// <inheritdoc cref="Microsoft.IO.RecyclableMemoryStream.Dispose"/>
        public void Dispose()
        {
            Stream.Dispose();
        }

        /// <inheritdoc cref="Microsoft.IO.RecyclableMemoryStream.DisposeAsync"/>
        public ValueTask DisposeAsync()
        {
            return Stream.DisposeAsync();
        }

        /// <inheritdoc cref="Microsoft.IO.RecyclableMemoryStream.InitializeLifetimeService"/>
        [Obsolete]
        public Object InitializeLifetimeService()
        {
            return Stream.InitializeLifetimeService();
        }

        /// <inheritdoc cref="Microsoft.IO.RecyclableMemoryStream.GetLifetimeService"/>
        [Obsolete]
        public Object GetLifetimeService()
        {
            return Stream.GetLifetimeService();
        }
    }
}