using System;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text;
using NetExtender.Interfaces.Notify;
using NetExtender.Utilities.Types;

namespace NetExtender.FileSystems.Interfaces
{
    public interface IUnsafeFileSystem : IFileSystem
    {
        public new static IUnsafeFileSystem Default
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return NetExtender.FileSystems.FileSystem.Instance;
            }
        }

        IUnsafeFileSystem IFileSystem.Unsafe
        {
            get
            {
                return this;
            }
        }

        [Obsolete($"Use IUnsafeFileSystemHandler as specified interface {nameof(IUnsafePathHandler)}; {nameof(IUnsafeLinkHandler)}; {nameof(IUnsafeFileHandler)}; {nameof(IUnsafeDirectoryHandler)}; {nameof(IUnsafeDriveHandler)}; {nameof(IUnsafeEnvironmentHandler)}.")]
        public new IUnsafeFileSystemHandler FileSystem { get; }
        public new IUnsafePathHandler Path { get; }
        public new IUnsafeLinkHandler Link { get; }
        public new IUnsafeFileHandler File { get; }
        public new IUnsafeDirectoryHandler Directory { get; }
        public new IUnsafeDriveHandler Drive { get; }
        public new IUnsafeEnvironmentHandler Environment { get; }
    }

    public interface IFileSystem : INotifyProperty, IDisposable
    {
        private static ConcurrentDictionary<Guid, WeakReference<IFileSystem>> Storage { get; } = new ConcurrentDictionary<Guid, WeakReference<IFileSystem>>();

        public static IFileSystem Default
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return IUnsafeFileSystem.Default;
            }
        }

        public IUnsafeFileSystem? Unsafe
        {
            get
            {
                return this as IUnsafeFileSystem;
            }
        }

        public Guid Id { get; }
        public String? Name { get; }
        public Object SyncRoot { get; }
        public Boolean IsSynchronized { get; }
        public DateTime CreationTime { get; }
        public DateTime CreationTimeUtc { get; }
        public Boolean IsReal { get; }
        public Encoding? Encoding { get; }
        public Encoding ReadEncoding { get; }
        public Encoding WriteEncoding { get; }
        public StringComparer? Comparer { get; }
        public Boolean? IsCaseSensitive { get; }

        public Boolean IsUnsafe
        {
            get
            {
                return this is IUnsafeFileSystem;
            }
        }

        [Obsolete($"Use IFileSystemHandler as specified interface {nameof(IPathHandler)}; {nameof(ILinkHandler)}; {nameof(IFileHandler)}; {nameof(IDirectoryHandler)}; {nameof(IDriveHandler)}; {nameof(IEnvironmentHandler)}.")]
        public IFileSystemHandler FileSystem { get; }
        public IPathHandler Path { get; }
        public ILinkHandler Link { get; }
        public IFileHandler File { get; }
        public IDirectoryHandler Directory { get; }
        public IDriveHandler Drive { get; }
        public IEnvironmentHandler Environment { get; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Boolean Initialize(IFileSystem value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return Storage.TryAdd(value.Id, new WeakReference<IFileSystem>(value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IFileSystem? Get(Guid id)
        {
            return TryGet(id, out IFileSystem? result) ? result : null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryGet(Guid id, [MaybeNullWhen(false)] out IFileSystem result)
        {
            return Storage.TryGetWeakValue(id, out result);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Boolean Dispose(IFileSystem value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return Storage.TryWeakRemove(value.Id);
        }
    }
}