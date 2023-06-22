using System;
using System.Diagnostics.CodeAnalysis;
using System.IO.MemoryMappedFiles;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Types.Interprocess.Interfaces;

namespace NetExtender.Types.Interprocess
{
    /// <summary>
    /// Wraps a MemoryMappedFile with interprocess synchronization and signaling
    /// </summary>
    internal sealed class InterprocessMemoryMappedFile : IInterprocessMemoryMappedFile
    {
        public event EventHandler? Changed;

        private Task FileWatcherTask { get; }
        private MemoryMappedFile? MappedFile { get; set; }
        private IInterprocessReadWriteLock ReadWriteLock { get; }
        private EventWaitHandle WaitHandle { get; }
        private EventWaitHandle DisposeWaitHandle { get; }

        public Int64 MaximumFileSize { get; }

        public const Int32 DefaultMaxFileSize = 1024 * 1024;

        public InterprocessMemoryMappedFile(String name)
            : this(name, DefaultMaxFileSize)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InterprocessMemoryMappedFile"/> class.
        /// </summary>
        /// <param name="name">A system wide unique name, the name will have a prefix appended before use</param>
        /// <param name="size">The maximum amount of data that can be written to the file memory mapped file</param>
        public InterprocessMemoryMappedFile(String name, Int64 size)
            : this(name, size, new InterprocessReadWriteLock(name))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InterprocessMemoryMappedFile"/> class.
        /// </summary>
        /// <param name="name">A system wide unique name, the name will have a prefix appended before use</param>
        /// <param name="size">The maximum amount of data that can be written to the file memory mapped file</param>
        /// <param name="interprocess">A read/write lock that will be used to control access to the memory mapped file</param>
        public InterprocessMemoryMappedFile(String name, Int64 size, IInterprocessReadWriteLock interprocess)
            : this(CreateOrOpenMemoryMappedFile(name, size), CreateEventWaitHandle(name), size, interprocess)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InterprocessMemoryMappedFile"/> class.
        /// </summary>
        /// <param name="file">An instance of a memory mapped file</param>
        /// <param name="handle">A manual reset EventWaitHandle that is to be used to signal file changes</param>
        /// <param name="size">The maximum amount of data that can be written to the file memory mapped file</param>
        /// <param name="interprocess">A read/write lock that will be used to control access to the memory mapped file</param>
        public InterprocessMemoryMappedFile(MemoryMappedFile file, EventWaitHandle handle, Int64 size, IInterprocessReadWriteLock interprocess)
        {
            MappedFile = file ?? throw new ArgumentNullException(nameof(file));
            WaitHandle = handle ?? throw new ArgumentNullException(nameof(handle));
            ReadWriteLock = interprocess ?? throw new ArgumentNullException(nameof(interprocess));
            MaximumFileSize = size;

            DisposeWaitHandle = new EventWaitHandle(false, EventResetMode.ManualReset);
            FileWatcherTask = Task.Run(FileWatcher);
        }

        /// <summary>
        /// Create or open a MemoryMappedFile that can be used to construct a <see cref="InterprocessMemoryMappedFile"/>
        /// </summary>
        /// <param name="name">A system wide unique name, the name will have a prefix appended</param>
        /// <param name="size">The maximum amount of data that can be written to the file memory mapped file</param>
        /// <returns>A system wide MemoryMappedFile</returns>
        public static MemoryMappedFile CreateOrOpenMemoryMappedFile(String name, Int64 size)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException(@"File must be named", nameof(name));
            }

            if (size <= 0)
            {
                throw new ArgumentException(@"Max file size can not be less than 1 byte", nameof(size));
            }

            return MemoryMappedFile.CreateOrOpen($"{nameof(InterprocessMemoryMappedFile)}_MemoryMappedFile_{name}", size + sizeof(Int32));
        }

        /// <summary>
        /// Create or open an EventWaitHandle that can be used to construct a <see cref="InterprocessMemoryMappedFile"/>
        /// </summary>
        /// <param name="name">A system wide unique name, the name will have a prefix appended</param>
        /// <returns>A system wide EventWaitHandle</returns>
        public static EventWaitHandle CreateEventWaitHandle(String name)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"{nameof(EventWaitHandle)} must be named", nameof(name));
            }

            return new EventWaitHandle(false, EventResetMode.ManualReset, $"{nameof(InterprocessMemoryMappedFile)}_WaitHandle_{name}");
        }

        /// <summary>
        /// Gets the file size
        /// </summary>
        /// <returns>File size</returns>
        public Int32 GetFileSize()
        {
            if (MappedFile is null)
            {
                throw new ObjectDisposedException(nameof(MappedFile));
            }

            ReadWriteLock.AcquireReadLock();

            try
            {
                using MemoryMappedViewAccessor accessor = MappedFile.CreateViewAccessor();
                return accessor.ReadInt32(0);
            }
            finally
            {
                ReadWriteLock.ReleaseReadLock();
            }
        }

        /// <summary>
        /// Reads the content of the memory mapped file with a read lock in place.
        /// </summary>
        /// <returns>File content</returns>
        public Byte[] Read()
        {
            ReadWriteLock.AcquireReadLock();

            try
            {
                return InternalRead();
            }
            finally
            {
                ReadWriteLock.ReleaseReadLock();
            }
        }

        /// <summary>
        /// Replaces the content of the memory mapped file with a write lock in place.
        /// </summary>
        public void Write(Byte[] data)
        {
            if (data is null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            if (data.Length > MaximumFileSize)
            {
                throw new ArgumentOutOfRangeException(nameof(data), @"Length greater than max file size");
            }

            ReadWriteLock.AcquireWriteLock();

            try
            {
                InternalWrite(data);
            }
            finally
            {
                ReadWriteLock.ReleaseWriteLock();
                WaitHandle.Set();
                WaitHandle.Reset();
            }
        }

        /// <summary>
        /// Reads and then replaces the content of the memory mapped file with a write lock in place.
        /// </summary>
        public void ReadWrite(Func<Byte[], Byte[]> function)
        {
            if (function is null)
            {
                throw new ArgumentNullException(nameof(function));
            }

            ReadWriteLock.AcquireWriteLock();

            try
            {
                InternalWrite(function(InternalRead()));
            }
            finally
            {
                ReadWriteLock.ReleaseWriteLock();
                WaitHandle.Set();
                WaitHandle.Reset();
            }
        }

        private void FileWatcher()
        {
            WaitHandle[] handles = {WaitHandle, DisposeWaitHandle};

            while (MappedFile is not null)
            {
                Int32 result = System.Threading.WaitHandle.WaitAny(handles, InterprocessReadWriteLock.DefaultWaitTimeout);

                if (result == 0 || MappedFile is null)
                {
                    return;
                }

                if (result == 1)
                {
                    Changed?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        private Byte[] InternalRead()
        {
            if (MappedFile is null)
            {
                throw new ObjectDisposedException(nameof(MappedFile));
            }

            using MemoryMappedViewAccessor accessor = MappedFile.CreateViewAccessor();
            Int32 length = accessor.ReadInt32(0);
            Byte[] data = new Byte[length];
            accessor.ReadArray(sizeof(Int32), data, 0, length);
            return data;
        }

        private void InternalWrite(Byte[] data)
        {
            if (data is null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            if (MappedFile is null)
            {
                throw new ObjectDisposedException(nameof(MappedFile));
            }

            if (data.Length > MaximumFileSize)
            {
                throw new ArgumentOutOfRangeException(nameof(data), @"Length greater than max file size");
            }

            using MemoryMappedViewAccessor accessor = MappedFile.CreateViewAccessor();
            accessor.Write(0, data.Length);
            accessor.WriteArray(sizeof(Int32), data, 0, data.Length);
        }

        [SuppressMessage("ReSharper", "AsyncConverter.AsyncWait")]
        public void Dispose()
        {
            if (MappedFile is null)
            {
                return;
            }

            DisposeWaitHandle.Set();
            FileWatcherTask.Wait(InterprocessReadWriteLock.DefaultWaitTimeout);

            MappedFile.Dispose();
            MappedFile = null;

            if (ReadWriteLock is InterprocessReadWriteLock rwlock)
            {
                rwlock.Dispose();
            }

            WaitHandle.Dispose();
            DisposeWaitHandle.Dispose();
        }
    }
}