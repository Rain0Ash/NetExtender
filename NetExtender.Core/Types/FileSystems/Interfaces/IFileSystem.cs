using System;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using NetExtender.Interfaces.Notify;
using NetExtender.Utilities.Types;

namespace NetExtender.FileSystems.Interfaces
{
    public interface IFileSystem : INotifyProperty, IDisposable
    {
        private static ConcurrentDictionary<Guid, WeakReference<IFileSystem>> Storage { get; } = new ConcurrentDictionary<Guid, WeakReference<IFileSystem>>();

        public static IFileSystem Default
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return NetExtender.FileSystems.FileSystem.Instance;
            }
        }
        
        public Guid Id { get; }
        public String? Name { get; }
        public Object SyncRoot { get; }
        public Boolean IsSynchronized { get; }
        public DateTime CreationTime { get; }
        public DateTime CreationTimeUtc { get; }
        public Boolean IsReal { get; }
        public StringComparer Comparer { get; }
        public Boolean IsCaseSensitive { get; }
        
        [Obsolete($"Use {nameof(IFileSystemHandler)} as specified interface {nameof(IPathHandler)}; {nameof(IFileHandler)}; {nameof(IDirectoryHandler)}.")]
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