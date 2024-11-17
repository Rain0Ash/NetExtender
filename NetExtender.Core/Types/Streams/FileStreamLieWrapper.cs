using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.CompilerServices;
using Microsoft.Win32.SafeHandles;
using NetExtender.Types.Storages;
using NetExtender.Types.Storages.Interfaces;

#pragma warning disable CA1041

namespace NetExtender.Types.Streams
{
    [return: NotNullIfNotNull("path")]
    public delegate String? FileStreamPathLieHandler2(String? path, ref FileMode mode);
    
    [return: NotNullIfNotNull("path")]
    public delegate String? FileStreamPathLieHandler3(String? path, ref FileMode mode, ref FileAccess access);
    
    [return: NotNullIfNotNull("path")]
    public delegate String? FileStreamPathLieHandler4(String? path, ref FileMode mode, ref FileAccess access, ref FileShare share);
    
    [return: NotNullIfNotNull("path")]
    public delegate String? FileStreamPathLieHandler5(String? path, ref FileMode mode, ref FileAccess access, ref FileShare share, ref Int32 buffer);
    
    [return: NotNullIfNotNull("path")]
    public delegate String? FileStreamPathLieHandler6(String? path, ref FileMode mode, ref FileAccess access, ref FileShare share, ref Int32 buffer, ref FileOptions options);
    
    [return: NotNullIfNotNull("path")]
    public delegate String? FileStreamPathLieHandlerAsync(String? path, ref FileMode mode, ref FileAccess access, ref FileShare share, ref Int32 buffer, ref Boolean async);

    [return: NotNullIfNotNull("path")]
    public delegate String? FileStreamPathLieHandlerOptions(String? path, ref FileStreamOptions options);
    
    public delegate IntPtr FileStreamHandleLieHandler2(IntPtr handle, ref FileAccess access);
    
    public delegate IntPtr FileStreamHandleLieHandler3(IntPtr handle, ref FileAccess access, ref Boolean ownsHandle);
    
    public delegate IntPtr FileStreamHandleLieHandler4(IntPtr handle, ref FileAccess access, ref Boolean ownsHandle, ref Int32 buffer);
    
    public delegate IntPtr FileStreamHandleLieHandler5(IntPtr handle, ref FileAccess access, ref Boolean ownsHandle, ref Int32 buffer, ref Boolean async);
    
    public delegate SafeFileHandle FileStreamSafeHandleLieHandler2(SafeFileHandle handle, ref FileAccess access);
    
    public delegate SafeFileHandle FileStreamSafeHandleLieHandler3(SafeFileHandle handle, ref FileAccess access, ref Int32 buffer);
    
    public delegate SafeFileHandle FileStreamSafeHandleLieHandler4(SafeFileHandle handle, ref FileAccess access, ref Int32 buffer, ref Boolean async);
    
    public class FileStreamLieWrapper : CustomFileStreamWrapper
    {
        public override String Name
        {
            get
            {
                return FileStreamLiePathWrapperStorage.Get(this, base.Name);
            }
        }

        public FileStreamLieWrapper(String path, FileStreamPathLieHandler2? handler, FileMode mode)
            : base(handler is not null ? handler(path, ref mode) : path, mode)
        {
            FileStreamLiePathWrapperStorage.Register(this, handler, base.Name, path);
        }

        public FileStreamLieWrapper(String path, FileStreamPathLieHandler3? handler, FileMode mode, FileAccess access)
            : base(handler is not null ? handler(path, ref mode, ref access) : path, mode, access)
        {
            FileStreamLiePathWrapperStorage.Register(this, handler, base.Name, path);
        }

        public FileStreamLieWrapper(String path, FileStreamPathLieHandler4? handler, FileMode mode, FileAccess access, FileShare share)
            : base(handler is not null ? handler(path, ref mode, ref access, ref share) : path, mode, access, share)
        {
            FileStreamLiePathWrapperStorage.Register(this, handler, base.Name, path);
        }

        public FileStreamLieWrapper(String path, FileStreamPathLieHandler5? handler, FileMode mode, FileAccess access, FileShare share, Int32 buffer)
            : base(handler is not null ? handler(path, ref mode, ref access, ref share, ref buffer) : path, mode, access, share, buffer)
        {
            FileStreamLiePathWrapperStorage.Register(this, handler, base.Name, path);
        }

        public FileStreamLieWrapper(String path, FileStreamPathLieHandlerAsync? handler, FileMode mode, FileAccess access, FileShare share, Int32 buffer, Boolean async)
            : base(handler is not null ? handler(path, ref mode, ref access, ref share, ref buffer, ref async) : path, mode, access, share, buffer, async)
        {
            FileStreamLiePathWrapperStorage.Register(this, handler, base.Name, path);
        }

        public FileStreamLieWrapper(String path, FileStreamPathLieHandler6? handler, FileMode mode, FileAccess access, FileShare share, Int32 buffer, FileOptions options)
            : base(handler is not null ? handler(path, ref mode, ref access, ref share, ref buffer, ref options) : path, mode, access, share, buffer, options)
        {
            FileStreamLiePathWrapperStorage.Register(this, handler, base.Name, path);
        }

        public FileStreamLieWrapper(String path, FileStreamPathLieHandlerOptions? handler, FileStreamOptions options)
            : base(handler is not null ? handler(path, ref options) : path, options)
        {
            FileStreamLiePathWrapperStorage.Register(this, handler, base.Name, path);
        }
        
        [Obsolete]
        public FileStreamLieWrapper(IntPtr handle, FileStreamHandleLieHandler2? handler, FileAccess access)
            : base(handler?.Invoke(handle, ref access) ?? handle, access)
        {
            FileStreamLiePathWrapperStorage.Register(this, handler, base.Name);
        }

        [Obsolete]
        public FileStreamLieWrapper(IntPtr handle, FileStreamHandleLieHandler3? handler, FileAccess access, Boolean ownsHandle)
            : base(handler?.Invoke(handle, ref access, ref ownsHandle) ?? handle, access, ownsHandle)
        {
            FileStreamLiePathWrapperStorage.Register(this, handler, base.Name);
        }

        [Obsolete]
        public FileStreamLieWrapper(IntPtr handle, FileStreamHandleLieHandler4? handler, FileAccess access, Boolean ownsHandle, Int32 buffer)
            : base(handler?.Invoke(handle, ref access, ref ownsHandle, ref buffer) ?? handle, access, ownsHandle, buffer)
        {
            FileStreamLiePathWrapperStorage.Register(this, handler, base.Name);
        }

        [Obsolete]
        public FileStreamLieWrapper(IntPtr handle, FileStreamHandleLieHandler5? handler, FileAccess access, Boolean ownsHandle, Int32 buffer, Boolean async)
            : base(handler?.Invoke(handle, ref access, ref ownsHandle, ref buffer, ref async) ?? handle, access, ownsHandle, buffer, async)
        {
            FileStreamLiePathWrapperStorage.Register(this, handler, base.Name);
        }
        
        public FileStreamLieWrapper(SafeFileHandle handle, FileStreamSafeHandleLieHandler2? handler, FileAccess access)
            : base(handler is not null ? handler(handle, ref access) : handle, access)
        {
            FileStreamLiePathWrapperStorage.Register(this, handler, base.Name);
        }

        public FileStreamLieWrapper(SafeFileHandle handle, FileStreamSafeHandleLieHandler3? handler, FileAccess access, Int32 buffer)
            : base(handler is not null ? handler(handle, ref access, ref buffer) : handle, access, buffer)
        {
            FileStreamLiePathWrapperStorage.Register(this, handler, base.Name);
        }

        public FileStreamLieWrapper(SafeFileHandle handle, FileStreamSafeHandleLieHandler4? handler, FileAccess access, Int32 buffer, Boolean async)
            : base(handler is not null ? handler(handle, ref access, ref buffer, ref async) : handle, access, buffer, async)
        {
            FileStreamLiePathWrapperStorage.Register(this, handler, base.Name);
        }
    }
    
    internal static class FileStreamLiePathWrapperStorage
    {
        private static IStorage<FileStream, Delegate> Storage { get; } = new WeakStorage<FileStream, Delegate>();
        private static IStorage<FileStream, IStorage<String, String>> Map { get; } = new WeakStorage<FileStream, IStorage<String, String>>();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Register(FileStream stream, Delegate? handler, String? name)
        {
            Register(stream, handler, name, null);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void Register(FileStream stream, Delegate? handler, String? name, String? path)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            if (handler is null)
            {
                return;
            }
            
            Storage.Add(stream, handler);

            if (name is null || path is null)
            {
                return;
            }

            lock (stream)
            {
                WeakStorage<String, String> storage = new WeakStorage<String, String>();
                Map.Add(stream, storage);
                storage[name] = path;
                storage[path] = name;
            }
        }

        [return: NotNullIfNotNull("name")]
        public static String? Get(FileStream stream, String? name)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            return Map.TryGetValue(stream, out IStorage<String, String>? storage) && name is not null && storage.TryGetValue(name, out String? result) ? result : name;
        }
    }
}