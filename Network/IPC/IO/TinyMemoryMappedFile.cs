using System;
using System.IO.MemoryMappedFiles;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Network.IPC.Synchronization;

namespace NetExtender.Network.IPC.IO
{
    /// <summary>
    /// Wraps a MemoryMappedFile with inter process synchronization and signaling
    /// </summary>
    public class TinyMemoryMappedFile : IDisposable, ITinyMemoryMappedFile
    {
        private readonly Task _fileWatcherTask;
        private readonly MemoryMappedFile _memoryMappedFile;
        private readonly ITinyReadWriteLock _readWriteLock;
        private readonly Boolean _disposeLock;
        private readonly EventWaitHandle _fileWaitHandle;

        private readonly EventWaitHandle _disposeWaitHandle;
        private Boolean _disposed;

        public event EventHandler? FileUpdated;

        public Int64 MaxFileSize { get; }

        public const Int32 DefaultMaxFileSize = 1024 * 1024;

        /// <summary>
        /// Initializes a new instance of the TinyMemoryMappedFile class.
        /// </summary>
        /// <param name="name">A system wide unique name, the name will have a prefix appended before use</param>
        /// <param name="maxFileSize">The maximum amount of data that can be written to the file memory mapped file</param>
        public TinyMemoryMappedFile(String name, Int64 maxFileSize = DefaultMaxFileSize)
            : this(name, maxFileSize, new TinyReadWriteLock(name), true)
        {
        }

        /// <summary>
        /// Initializes a new instance of the TinyMemoryMappedFile class.
        /// </summary>
        /// <param name="name">A system wide unique name, the name will have a prefix appended before use</param>
        /// <param name="maxFileSize">The maximum amount of data that can be written to the file memory mapped file</param>
        /// <param name="readWriteLock">A read/write lock that will be used to control access to the memory mapped file</param>
        /// <param name="disposeLock">Set to true if the read/write lock is to be disposed when this instance is disposed</param>
        public TinyMemoryMappedFile(String name, Int64 maxFileSize, ITinyReadWriteLock readWriteLock, Boolean disposeLock)
            : this(CreateOrOpenMemoryMappedFile(name, maxFileSize), CreateEventWaitHandle(name), maxFileSize, readWriteLock, disposeLock)
        {
        }

        /// <summary>
        /// Initializes a new instance of the TinyMemoryMappedFile class.
        /// </summary>
        /// <param name="memoryMappedFile">An instance of a memory mapped file</param>
        /// <param name="fileWaitHandle">A manual reset EventWaitHandle that is to be used to signal file changes</param>
        /// <param name="maxFileSize">The maximum amount of data that can be written to the file memory mapped file</param>
        /// <param name="readWriteLock">A read/write lock that will be used to control access to the memory mapped file</param>
        /// <param name="disposeLock">Set to true if the read/write lock is to be disposed when this instance is disposed</param>
        public TinyMemoryMappedFile(MemoryMappedFile memoryMappedFile, EventWaitHandle fileWaitHandle, Int64 maxFileSize, ITinyReadWriteLock readWriteLock, Boolean disposeLock)
        {
            _readWriteLock = readWriteLock ?? throw new ArgumentNullException(nameof(readWriteLock));
            _memoryMappedFile = memoryMappedFile ?? throw new ArgumentNullException(nameof(memoryMappedFile));
            _fileWaitHandle = fileWaitHandle ?? throw new ArgumentNullException(nameof(fileWaitHandle));
            _disposeLock = disposeLock;

            MaxFileSize = maxFileSize;

            _disposeWaitHandle = new EventWaitHandle(false, EventResetMode.ManualReset);

            _fileWatcherTask = Task.Run(FileWatcher);
        }

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            _disposed = true;
            _disposeWaitHandle.Set();
            // ReSharper disable once AsyncConverter.AsyncWait
            _fileWatcherTask.Wait(TinyReadWriteLock.DefaultWaitTimeout);

            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(Boolean disposing)
        {
            if (!disposing)
            {
                return;
            }

            _memoryMappedFile.Dispose();

            if (_disposeLock && _readWriteLock is TinyReadWriteLock tinyReadWriteLock)
            {
                tinyReadWriteLock.Dispose();
            }

            _fileWaitHandle.Dispose();
            _disposeWaitHandle.Dispose();
        }

        /// <summary>
        /// Gets the file size
        /// </summary>
        /// <returns>File size</returns>
        public Int32 GetFileSize()
        {
            _readWriteLock.AcquireReadLock();

            try
            {
                using MemoryMappedViewAccessor accessor = _memoryMappedFile.CreateViewAccessor();
                return accessor.ReadInt32(0);
            }
            finally
            {
                _readWriteLock.ReleaseReadLock();
            }
        }

        /// <summary>
        /// Reads the content of the memory mapped file with a read lock in place.
        /// </summary>
        /// <returns>File content</returns>
        public Byte[] Read()
        {
            _readWriteLock.AcquireReadLock();

            try
            {
                return InternalRead();
            }
            finally
            {
                _readWriteLock.ReleaseReadLock();
            }
        }

        /// <summary>
        /// Replaces the content of the memory mapped file with a write lock in place.
        /// </summary>
        public void Write(Byte[] data)
        {
            if (data.Length > MaxFileSize)
            {
                throw new ArgumentOutOfRangeException(nameof(data), @"Length greater than max file size");
            }

            _readWriteLock.AcquireWriteLock();

            try
            {
                InternalWrite(data);
            }
            finally
            {
                _readWriteLock.ReleaseWriteLock();
                _fileWaitHandle.Set();
                _fileWaitHandle.Reset();
            }
        }

        /// <summary>
        /// Reads and then replaces the content of the memory mapped file with a write lock in place.
        /// </summary>
        public void ReadWrite(Func<Byte[], Byte[]> updateFunc)
        {
            _readWriteLock.AcquireWriteLock();

            try
            {
                InternalWrite(updateFunc(InternalRead()));
            }
            finally
            {
                _readWriteLock.ReleaseWriteLock();
                _fileWaitHandle.Set();
                _fileWaitHandle.Reset();
            }
        }

        private void FileWatcher()
        {
            WaitHandle[] waitHandles = {_disposeWaitHandle, _fileWaitHandle};

            while (!_disposed)
            {
                Int32 result = WaitHandle.WaitAny(waitHandles, TinyReadWriteLock.DefaultWaitTimeout);

                // Triggers when disposed
                if (result == 0 || _disposed)
                {
                    return;
                }

                // Triggers when the file is changed
                if (result == 1)
                {
                    FileUpdated?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        private Byte[] InternalRead()
        {
            using MemoryMappedViewAccessor accessor = _memoryMappedFile.CreateViewAccessor();
            Int32 length = accessor.ReadInt32(0);
            Byte[] data = new Byte[length];
            accessor.ReadArray(sizeof(Int32), data, 0, length);
            return data;
        }

        private void InternalWrite(Byte[] data)
        {
            if (data.Length > MaxFileSize)
            {
                throw new ArgumentOutOfRangeException(nameof(data), @"Length greater than max file size");
            }

            using MemoryMappedViewAccessor accessor = _memoryMappedFile.CreateViewAccessor();
            accessor.Write(0, data.Length);
            accessor.WriteArray(sizeof(Int32), data, 0, data.Length);
        }

        /// <summary>
        /// Create or open a MemoryMappedFile that can be used to construct a TinyMemoryMappedFile
        /// </summary>
        /// <param name="name">A system wide unique name, the name will have a prefix appended</param>
        /// <param name="maxFileSize">The maximum amount of data that can be written to the file memory mapped file</param>
        /// <returns>A system wide MemoryMappedFile</returns>
        public static MemoryMappedFile CreateOrOpenMemoryMappedFile(String name, Int64 maxFileSize)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException(@"File must be named", nameof(name));
            }

            if (maxFileSize <= 0)
            {
                throw new ArgumentException(@"Max file size can not be less than 1 byte", nameof(maxFileSize));
            }

            return MemoryMappedFile.CreateOrOpen("TinyMemoryMappedFile_MemoryMappedFile_" + name, maxFileSize + sizeof(Int32));
        }

        /// <summary>
        /// Create or open an EventWaitHandle that can be used to construct a TinyMemoryMappedFile
        /// </summary>
        /// <param name="name">A system wide unique name, the name will have a prefix appended</param>
        /// <returns>A system wide EventWaitHandle</returns>
        public static EventWaitHandle CreateEventWaitHandle(String name)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException(@"EventWaitHandle must be named", nameof(name));
            }

            return new EventWaitHandle(false, EventResetMode.ManualReset, "TinyMemoryMappedFile_WaitHandle_" + name);
        }
    }
}