// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.IO;
using System.Runtime.Versioning;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Win32.SafeHandles;
#pragma warning disable CA1041

namespace NetExtender.Types.Streams
{
    public class FileStreamWrapper : FileStreamLieWrapper
    {
        protected Lazy<Stream> Internal { get; }

        protected Stream Stream
        {
            get
            {
                return Internal.Value;
            }
        }

        public override String Name
        {
            get
            {
                return Stream is FileStream stream ? stream.Name : base.Name;
            }
        }

        [Obsolete]
        public override IntPtr Handle
        {
            get
            {
                return Stream is FileStream stream ? stream.Handle : IntPtr.Zero;
            }
        }

        public override SafeFileHandle SafeFileHandle
        {
            get
            {
                return Stream is FileStream stream ? stream.SafeFileHandle : new SafeFileHandle(IntPtr.Zero, false);
            }
        }

        public override Boolean IsAsync
        {
            get
            {
                return Stream is FileStream stream ? stream.IsAsync : base.IsAsync;
            }
        }

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

        // ReSharper disable once UnusedParameter.Local
        private FileStreamWrapper(Object? _)
            : base(Path.GetTempFileName(), null, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite | FileShare.Delete, 0, FileOptions.DeleteOnClose | FileOptions.SequentialScan)
        {
            Internal = null!;
        }

        protected FileStreamWrapper(FileStream stream)
            : this(stream is not null ? stream.Name : throw new ArgumentNullException(nameof(stream)))
        {
            Internal = new Lazy<Stream>(stream);
        }

        public FileStreamWrapper(String path, FileMode mode)
            : this(ValidateArguments(path, mode, null, null, null, null, null))
        {
            Internal = new Lazy<Stream>(() => new FileStream(path, mode));
        }

        public FileStreamWrapper(String path, FileMode mode, FileAccess access)
            : this(ValidateArguments(path, mode, access, null, null, null, null))
        {
            Internal = new Lazy<Stream>(() => new FileStream(path, mode, access));
        }

        public FileStreamWrapper(String path, FileMode mode, FileAccess access, FileShare share)
            : this(ValidateArguments(path, mode, access, share, null, null, null))
        {
            Internal = new Lazy<Stream>(() => new FileStream(path, mode, access, share));
        }

        public FileStreamWrapper(String path, FileMode mode, FileAccess access, FileShare share, Int32 bufferSize)
            : this(ValidateArguments(path, mode, access, share, bufferSize, null, null))
        {
            Internal = new Lazy<Stream>(() => new FileStream(path, mode, access, share, bufferSize));
        }

        public FileStreamWrapper(String path, FileMode mode, FileAccess access, FileShare share, Int32 bufferSize, Boolean useAsync)
            : this(ValidateArguments(path, mode, access, share, bufferSize, useAsync, null))
        {
            Internal = new Lazy<Stream>(() => new FileStream(path, mode, access, share, bufferSize, useAsync));
        }

        public FileStreamWrapper(String path, FileMode mode, FileAccess access, FileShare share, Int32 bufferSize, FileOptions options)
            : this(ValidateArguments(path, mode, access, share, bufferSize, options, null))
        {
            Internal = new Lazy<Stream>(() => new FileStream(path, mode, access, share, bufferSize, options));
        }

        public FileStreamWrapper(String path, FileStreamOptions options)
            : this(ValidateArguments(path, options.Mode, options.Access, options.Share, options.BufferSize, options.Options, options.PreallocationSize))
        {
            Internal = new Lazy<Stream>(() => new FileStream(path, options));
        }

        [Obsolete]
        public FileStreamWrapper(IntPtr handle, FileAccess access)
            : this((Object?) null)
        {
            Internal = new Lazy<Stream>(() => new FileStream(handle, access));
        }

        [Obsolete]
        public FileStreamWrapper(IntPtr handle, FileAccess access, Boolean ownsHandle)
            : this((Object?) null)
        {
            Internal = new Lazy<Stream>(() => new FileStream(handle, access, ownsHandle));
        }

        [Obsolete]
        public FileStreamWrapper(IntPtr handle, FileAccess access, Boolean ownsHandle, Int32 bufferSize)
            : this((Object?) null)
        {
            Internal = new Lazy<Stream>(() => new FileStream(handle, access, ownsHandle, bufferSize));
        }

        [Obsolete]
        public FileStreamWrapper(IntPtr handle, FileAccess access, Boolean ownsHandle, Int32 bufferSize, Boolean isAsync)
            : this((Object?) null)
        {
            Internal = new Lazy<Stream>(() => new FileStream(handle, access, ownsHandle, bufferSize, isAsync));
        }

        public FileStreamWrapper(SafeFileHandle handle, FileAccess access)
            : this(ValidateHandle(handle, access, DefaultBufferSize))
        {
            Internal = new Lazy<Stream>(() => new FileStream(handle, access));
        }

        public FileStreamWrapper(SafeFileHandle handle, FileAccess access, Int32 bufferSize)
            : this(ValidateHandle(handle, access, bufferSize))
        {
            Internal = new Lazy<Stream>(() => new FileStream(handle, access, bufferSize));
        }

        public FileStreamWrapper(SafeFileHandle handle, FileAccess access, Int32 bufferSize, Boolean isAsync)
            : this(ValidateHandle(handle, access, bufferSize, isAsync))
        {
            Internal = new Lazy<Stream>(() => new FileStream(handle, access, bufferSize, isAsync));
        }
        
        [UnsupportedOSPlatform("ios")]
        [UnsupportedOSPlatform("macos")]
        [UnsupportedOSPlatform("tvos")]
        public override void Lock(Int64 position, Int64 length)
        {
            if (Stream is FileStream stream)
            {
                stream.Lock(position, length);
            }
        }

        [UnsupportedOSPlatform("ios")]
        [UnsupportedOSPlatform("macos")]
        [UnsupportedOSPlatform("tvos")]
        public override void Unlock(Int64 position, Int64 length)
        {
            if (Stream is FileStream stream)
            {
                stream.Unlock(position, length);
            }
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

        public override void Flush(Boolean disk)
        {
            if (Stream is FileStream stream)
            {
                stream.Flush(disk);
                return;
            }
            
            Stream.Flush();
        }

        public override Task FlushAsync(CancellationToken token)
        {
            return Stream.FlushAsync(token);
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

        public override Boolean Equals(Object? other)
        {
            return Stream.Equals(other);
        }

        public override String? ToString()
        {
            return Stream.ToString();
        }

        protected override void Dispose(Boolean disposing)
        {
            try
            {
                if (disposing && Internal.IsValueCreated)
                {
                    Stream.Dispose();
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        public override async ValueTask DisposeAsync()
        {
            try
            {
                if (Internal.IsValueCreated)
                {
                    await Stream.DisposeAsync();
                }
            }
            finally
            {
                await base.DisposeAsync();
                GC.SuppressFinalize(this);
            }
        }
    }
}