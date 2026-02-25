// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Text;
using NetExtender.Exceptions;
using NetExtender.FileSystems.Interfaces;

namespace NetExtender.FileSystems
{
    public class UnsafeFileSystemHandlerWrapper : UnsafeFileSystemHandlerWrapper<IUnsafeFileSystem>
    {
        public sealed override IUnsafePathHandler Path
        {
            get
            {
                return this;
            }
        }

        public sealed override IUnsafeLinkHandler Link
        {
            get
            {
                return this;
            }
        }

        public sealed override IUnsafeFileHandler File
        {
            get
            {
                return this;
            }
        }

        public sealed override IUnsafeDirectoryHandler Directory
        {
            get
            {
                return this;
            }
        }

        public sealed override IUnsafeDriveHandler Drive
        {
            get
            {
                return this;
            }
        }

        public sealed override IUnsafeEnvironmentHandler Environment
        {
            get
            {
                return this;
            }
        }

        protected internal UnsafeFileSystemHandlerWrapper(IUnsafeFileSystem handler)
            : base(handler)
        {
        }
    }

    public class FileSystemHandlerWrapper : FileSystemHandlerWrapper<IFileSystem>
    {
        public sealed override IPathHandler Path
        {
            get
            {
                return this;
            }
        }

        public sealed override ILinkHandler Link
        {
            get
            {
                return this;
            }
        }

        public sealed override IFileHandler File
        {
            get
            {
                return this;
            }
        }

        public sealed override IDirectoryHandler Directory
        {
            get
            {
                return this;
            }
        }

        public sealed override IDriveHandler Drive
        {
            get
            {
                return this;
            }
        }

        public sealed override IEnvironmentHandler Environment
        {
            get
            {
                return this;
            }
        }

        protected internal FileSystemHandlerWrapper(IFileSystem handler)
            : base(handler)
        {
        }

        public static FileSystemHandlerWrapper<T> Create<T>(T handler) where T : class, IFileSystem
        {
            if (!typeof(T).IsAssignableTo(typeof(IUnsafeFileSystem)))
            {
                return new FileSystemHandlerWrapper<T>(handler);
            }

            const BindingFlags binding = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance;
            return (FileSystemHandlerWrapper<T>) Activator.CreateInstance(typeof(UnsafeFileSystemHandlerWrapper<>).MakeGenericType(typeof(T)), binding, handler)!;
        }
    }

    public class UnsafeFileSystemHandlerWrapper<T> : FileSystemHandlerWrapper<T>, INetExtenderUnsafeFileSystemHandler where T : class, IUnsafeFileSystem
    {
        [Obsolete($"Use IUnsafeFileSystemHandler as specified interface {nameof(IUnsafePathHandler)}; {nameof(IUnsafeLinkHandler)}; {nameof(IUnsafeFileHandler)}; {nameof(IUnsafeDirectoryHandler)}; {nameof(IUnsafeDriveHandler)}; {nameof(IUnsafeEnvironmentHandler)}.")]
        IUnsafeFileSystemHandler IUnsafeFileSystem.FileSystem
        {
            get
            {
                return this;
            }
        }

        public override IUnsafePathHandler Path
        {
            get
            {
                return this;
            }
        }

        public override IUnsafeLinkHandler Link
        {
            get
            {
                return this;
            }
        }

        public override IUnsafeFileHandler File
        {
            get
            {
                return this;
            }
        }

        public override IUnsafeDirectoryHandler Directory
        {
            get
            {
                return this;
            }
        }

        public override IUnsafeDriveHandler Drive
        {
            get
            {
                return this;
            }
        }

        public override IUnsafeEnvironmentHandler Environment
        {
            get
            {
                return this;
            }
        }

        protected internal UnsafeFileSystemHandlerWrapper(T handler)
            : base(handler)
        {
        }

        protected virtual FileSystemInfo CreateSymbolicLinkUnsafe(String path, String target, FileSystemHandlerType handler)
        {
            return handler switch
            {
                FileSystemHandlerType.Unknown => throw new AmbiguousFileSystemHandlerException(),
                FileSystemHandlerType.Path => throw new NotSupportedException(),
                FileSystemHandlerType.Link => FileSystem.Link.CreateSymbolicLink(path, target),
                FileSystemHandlerType.File => FileSystem.File.CreateSymbolicLink(path, target),
                FileSystemHandlerType.Directory => FileSystem.Directory.CreateSymbolicLink(path, target),
                FileSystemHandlerType.Drive => throw new NotSupportedException(),
                _ => throw new EnumUndefinedOrNotSupportedException<FileSystemHandlerType>(handler, nameof(handler), null)
            };
        }

        FileSystemInfo IUnsafeFileSystemHandler.CreateSymbolicLink(String path, String target)
        {
            return CreateSymbolicLinkUnsafe(path, target, FileSystemHandlerType.Unknown);
        }

        FileSystemInfo IUnsafeLinkHandler.CreateSymbolicLink(String path, String target)
        {
            return CreateSymbolicLinkUnsafe(path, target, FileSystemHandlerType.Link);
        }

        FileSystemInfo IUnsafeFileHandler.CreateSymbolicLink(String path, String target)
        {
            return CreateSymbolicLinkUnsafe(path, target, FileSystemHandlerType.File);
        }

        FileSystemInfo IUnsafeDirectoryHandler.CreateSymbolicLink(String path, String target)
        {
            return CreateSymbolicLinkUnsafe(path, target, FileSystemHandlerType.Directory);
        }

        protected virtual FileSystemInfo? ResolveLinkTargetUnsafe(String path, Boolean target, FileSystemHandlerType handler)
        {
            return handler switch
            {
                FileSystemHandlerType.Unknown => throw new AmbiguousFileSystemHandlerException(),
                FileSystemHandlerType.Path => throw new NotSupportedException(),
                FileSystemHandlerType.Link => FileSystem.Link.ResolveLinkTarget(path, target),
                FileSystemHandlerType.File => FileSystem.File.ResolveLinkTarget(path, target),
                FileSystemHandlerType.Directory => FileSystem.Directory.ResolveLinkTarget(path, target),
                FileSystemHandlerType.Drive => throw new NotSupportedException(),
                _ => throw new EnumUndefinedOrNotSupportedException<FileSystemHandlerType>(handler, nameof(handler), null)
            };
        }

        FileSystemInfo? IUnsafeFileSystemHandler.ResolveLinkTarget(String path, Boolean target)
        {
            return ResolveLinkTargetUnsafe(path, target, FileSystemHandlerType.Unknown);
        }

        FileSystemInfo? IUnsafeLinkHandler.ResolveLinkTarget(String path, Boolean target)
        {
            return ResolveLinkTargetUnsafe(path, target, FileSystemHandlerType.Link);
        }

        FileSystemInfo? IUnsafeFileHandler.ResolveLinkTarget(String path, Boolean target)
        {
            return ResolveLinkTargetUnsafe(path, target, FileSystemHandlerType.File);
        }

        FileSystemInfo? IUnsafeDirectoryHandler.ResolveLinkTarget(String path, Boolean target)
        {
            return ResolveLinkTargetUnsafe(path, target, FileSystemHandlerType.Directory);
        }

        protected virtual DirectoryInfo CreateDirectoryUnsafe(String path)
        {
            return FileSystem.Directory.CreateDirectory(path);
        }

        DirectoryInfo IUnsafeDirectoryHandler.CreateDirectory(String path)
        {
            return CreateDirectoryUnsafe(path);
        }

        protected virtual DirectoryInfo CreateDirectoryUnsafe(String path, UnixFileMode mode)
        {
            return FileSystem.Directory.CreateDirectory(path, mode);
        }

        DirectoryInfo IUnsafeDirectoryHandler.CreateDirectory(String path, UnixFileMode mode)
        {
            return CreateDirectoryUnsafe(path, mode);
        }

        protected virtual DirectoryInfo? GetParentUnsafe(String path, FileSystemHandlerType handler)
        {
            return handler switch
            {
                FileSystemHandlerType.Unknown => throw new AmbiguousFileSystemHandlerException(),
                FileSystemHandlerType.Path => throw new NotSupportedException(),
                FileSystemHandlerType.Link or FileSystemHandlerType.File => throw new NotSupportedException(),
                FileSystemHandlerType.Directory => FileSystem.Directory.GetParent(path),
                FileSystemHandlerType.Drive => throw new NotSupportedException(),
                _ => throw new EnumUndefinedOrNotSupportedException<FileSystemHandlerType>(handler, nameof(handler), null)
            };
        }

        DirectoryInfo? IUnsafeFileSystemHandler.GetParent(String path)
        {
            return GetParentUnsafe(path, FileSystemHandlerType.Unknown);
        }

        DirectoryInfo? IUnsafeDirectoryHandler.GetParent(String path)
        {
            return GetParentUnsafe(path, FileSystemHandlerType.Directory);
        }

        protected virtual DriveInfo[] GetDrivesUnsafe()
        {
            return FileSystem.Drive.GetDrives();
        }

        DriveInfo[] IUnsafeDriveHandler.GetDrives()
        {
            return GetDrivesUnsafe();
        }

        protected virtual DirectoryInfo? GetFolderPathUnsafe(Environment.SpecialFolder folder)
        {
            return FileSystem.Environment.GetFolderPath(folder);
        }

        DirectoryInfo? IUnsafeEnvironmentHandler.GetFolderPath(Environment.SpecialFolder folder)
        {
            return GetFolderPathUnsafe(folder);
        }

        protected virtual DirectoryInfo? GetFolderPathUnsafe(Environment.SpecialFolder folder, Environment.SpecialFolderOption option)
        {
            return FileSystem.Environment.GetFolderPath(folder, option);
        }

        DirectoryInfo? IUnsafeEnvironmentHandler.GetFolderPath(Environment.SpecialFolder folder, Environment.SpecialFolderOption option)
        {
            return GetFolderPathUnsafe(folder, option);
        }
    }

    public partial class FileSystemHandlerWrapper<T> : FileSystemHandler where T : class, IFileSystem
    {
        protected sealed override T FileSystem { get; }

        public override Object SyncRoot
        {
            get
            {
                return FileSystem.SyncRoot;
            }
        }

        public override Boolean IsSynchronized
        {
            get
            {
                return FileSystem.IsSynchronized;
            }
        }

        public override IPathHandler Path
        {
            get
            {
                return this;
            }
        }

        public override ILinkHandler Link
        {
            get
            {
                return this;
            }
        }

        public override IFileHandler File
        {
            get
            {
                return this;
            }
        }

        public override IDirectoryHandler Directory
        {
            get
            {
                return this;
            }
        }

        public override IDriveHandler Drive
        {
            get
            {
                return this;
            }
        }

        public override IEnvironmentHandler Environment
        {
            get
            {
                return this;
            }
        }

        public sealed override Encoding? Encoding
        {
            get
            {
                return base.Encoding ?? FileSystem.Encoding;
            }
            init
            {
                base.Encoding = value;
            }
        }

        public sealed override Encoding ReadEncoding
        {
            get
            {
                return _read ?? FileSystem.ReadEncoding;
            }
            init
            {
                base.ReadEncoding = value;
            }
        }

        public sealed override Encoding WriteEncoding
        {
            get
            {
                return _write ?? FileSystem.WriteEncoding;
            }
            init
            {
                base.WriteEncoding = value;
            }
        }

        public sealed override StringComparer? Comparer
        {
            get
            {
                return FileSystem.Comparer;
            }
        }

        public sealed override Boolean? IsCaseSensitive
        {
            get
            {
                return FileSystem.IsCaseSensitive;
            }
        }

        protected internal FileSystemHandlerWrapper(T handler)
        {
            FileSystem = handler ?? throw new ArgumentNullException(nameof(handler));
            FileSystem.PropertyChanging += FileSystemPropertyChanging;
            FileSystem.PropertyChanged += FileSystemPropertyChanged;
        }

        private void FileSystemPropertyChanging(Object? sender, PropertyChangingEventArgs args)
        {
            OnPropertyChanging(args);
        }

        private void FileSystemPropertyChanged(Object? sender, PropertyChangedEventArgs args)
        {
            OnPropertyChanged(args);
        }

        private protected sealed override String? FileSystemName()
        {
            return FileSystem.Name;
        }

        private protected sealed override DateTime CreationTimeFileSystem()
        {
            return FileSystem.CreationTime;
        }

        private protected sealed override DateTime CreationTimeUtcFileSystem()
        {
            return FileSystem.CreationTimeUtc;
        }

        private protected sealed override Boolean IsRealFileSystem()
        {
            return FileSystem.IsReal;
        }

        private protected override Boolean IsUnsafeFileSystem()
        {
            return FileSystem.IsUnsafe;
        }

        public override Int32 GetHashCode()
        {
            return FileSystem.GetHashCode();
        }

        public override Boolean Equals(Object? other)
        {
            return FileSystem.Equals(other);
        }

        public override String? ToString()
        {
            return FileSystem.ToString();
        }
    }
}