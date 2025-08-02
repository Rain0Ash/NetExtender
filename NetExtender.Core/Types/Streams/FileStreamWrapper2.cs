// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.Versioning;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Win32.SafeHandles;
using NetExtender.Patch;

namespace NetExtender.Types.Streams
{
    //TODO: Not works without patch. PrivateCoreLib patching also is not work (for release mode).
    [SuppressMessage("Design", "CA1041")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class FileStreamWrapper2 : FileStream
    {
        static FileStreamWrapper2()
        {
            if (NetExtenderFileStreamPatch.Require() is { } exception)
            {
                throw exception;
            }
        }

        protected Lazy<FileStream> Internal { get; } = null!;
        
        protected FileStream Stream
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
                return Stream.Name;
            }
        }

        [Obsolete]
        public override IntPtr Handle
        {
            get
            {
                return Stream.Handle;
            }
        }

        public override SafeFileHandle SafeFileHandle
        {
            get
            {
                return Stream.SafeFileHandle;
            }
        }

        public override Boolean IsAsync
        {
            get
            {
                return Stream.IsAsync;
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

        protected FileStreamWrapper2(FileStream stream)
            : base(Path.GetTempFileName(), FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite | FileShare.Delete, 0, FileOptions.DeleteOnClose | FileOptions.SequentialScan)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            Internal = new Lazy<FileStream>(stream);
        }

        public FileStreamWrapper2(String path, FileMode mode)
            : base(path, mode)
        {
            //Internal = new Lazy<FileStream>(() => new FileStream(path, mode));
        }

        public FileStreamWrapper2(String path, FileMode mode, FileAccess access)
            : base(path, mode, access)
        {
            //Internal = new Lazy<FileStream>(() => new FileStream(path, mode, access));
        }

        public FileStreamWrapper2(String path, FileMode mode, FileAccess access, FileShare share)
            : base(path, mode, access, share)
        {
            //Internal = new Lazy<FileStream>(() => new FileStream(path, mode, access, share));
        }

        public FileStreamWrapper2(String path, FileMode mode, FileAccess access, FileShare share, Int32 bufferSize)
            : base(path, mode, access, share, bufferSize)
        {
            //Internal = new Lazy<FileStream>(() => new FileStream(path, mode, access, share, bufferSize));
        }

        public FileStreamWrapper2(String path, FileMode mode, FileAccess access, FileShare share, Int32 bufferSize, Boolean useAsync)
            : base(path, mode, access, share, bufferSize, useAsync)
        {
            //Internal = new Lazy<FileStream>(() => new FileStream(path, mode, access, share, bufferSize, useAsync));
        }

        public FileStreamWrapper2(String path, FileMode mode, FileAccess access, FileShare share, Int32 bufferSize, FileOptions options)
            : base(path, mode, access, share, bufferSize, options)
        {
            //Internal = new Lazy<FileStream>(() => new FileStream(path, mode, access, share, bufferSize, options));
        }

        public FileStreamWrapper2(String path, FileStreamOptions options)
            : base(path, options)
        {
            //Internal = new Lazy<FileStream>(() => new FileStream(path, options));
        }

        [Obsolete]
        public FileStreamWrapper2(IntPtr handle, FileAccess access)
            : base(handle, access)
        {
            //Internal = new Lazy<FileStream>(() => new FileStream(handle, access));
        }

        [Obsolete]
        public FileStreamWrapper2(IntPtr handle, FileAccess access, Boolean ownsHandle)
            : base(handle, access, ownsHandle)
        {
            //Internal = new Lazy<FileStream>(() => new FileStream(handle, access, ownsHandle));
        }

        [Obsolete]
        public FileStreamWrapper2(IntPtr handle, FileAccess access, Boolean ownsHandle, Int32 bufferSize)
            : base(handle, access, ownsHandle, bufferSize)
        {
            //Internal = new Lazy<FileStream>(() => new FileStream(handle, access, ownsHandle, bufferSize));
        }

        [Obsolete]
        public FileStreamWrapper2(IntPtr handle, FileAccess access, Boolean ownsHandle, Int32 bufferSize, Boolean isAsync)
            : base(handle, access, ownsHandle, bufferSize, isAsync)
        {
            //Internal = new Lazy<FileStream>(() => new FileStream(handle, access, ownsHandle, bufferSize, isAsync));
        }

        public FileStreamWrapper2(SafeFileHandle handle, FileAccess access)
            : base(handle, access)
        {
            //Internal = new Lazy<FileStream>(() => new FileStream(handle, access));
        }

        public FileStreamWrapper2(SafeFileHandle handle, FileAccess access, Int32 bufferSize)
            : base(handle, access, bufferSize)
        {
            //Internal = new Lazy<FileStream>(() => new FileStream(handle, access, bufferSize));
        }

        protected FileStreamWrapper2(SafeFileHandle handle, FileAccess access, Int32 bufferSize, Boolean isAsync)
            : base(handle, access, bufferSize, isAsync)
        {
            //Internal = new Lazy<FileStream>(() => new FileStream(handle, access, bufferSize, isAsync));
        }
        
        [UnsupportedOSPlatform("ios")]
        [UnsupportedOSPlatform("macos")]
        [UnsupportedOSPlatform("tvos")]
        public override void Lock(Int64 position, Int64 length)
        {
            Stream.Lock(position, length);
        }

        [UnsupportedOSPlatform("ios")]
        [UnsupportedOSPlatform("macos")]
        [UnsupportedOSPlatform("tvos")]
        public override void Unlock(Int64 position, Int64 length)
        {
            Stream.Unlock(position, length);
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
            Stream.Flush(disk);
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
            if (disposing && Internal.IsValueCreated)
            {
                Stream.Dispose();
            }

            base.Dispose(disposing);
        }

        public override async ValueTask DisposeAsync()
        {
            if (Internal.IsValueCreated)
            {
                await Stream.DisposeAsync();
            }
            
            GC.SuppressFinalize(this);
        }
    }
}