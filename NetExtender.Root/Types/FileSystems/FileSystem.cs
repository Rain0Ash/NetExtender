using System;
using System.IO;
using System.Runtime.InteropServices;
using NetExtender.Exceptions;
using NetExtender.FileSystems.Interfaces;
using NetExtender.Utilities.IO;
using NetExtender.Utilities.Types;

namespace NetExtender.FileSystems
{
    internal sealed class FileSystem : FileSystemHandler, INetExtenderUnsafeFileSystemHandler
    {
        internal static FileSystem Instance { get; } = new FileSystem();
        public override SyncRoot SyncRoot { get; } = SyncRoot.Create();

        IUnsafeFileSystem IFileSystem.Unsafe
        {
            get
            {
                return this;
            }
        }

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

        private FileSystem()
            : base(Guid.Empty)
        {
            CreationTimeOffset = default;
        }

        private protected override String? FileSystemName()
        {
            return null;
        }

        private protected override Boolean IsRealFileSystem()
        {
            return true;
        }

        private protected override Boolean IsUnsafeFileSystem()
        {
            return true;
        }

        private static FileSystemInfo CreateSymbolicLinkUnsafe(String path, String target, FileSystemHandlerType handler)
        {
            return handler switch
            {
                FileSystemHandlerType.Unknown => throw new AmbiguousFileSystemHandlerException(),
                FileSystemHandlerType.Path => throw new NotSupportedException(),
                FileSystemHandlerType.Link or FileSystemHandlerType.File => System.IO.File.CreateSymbolicLink(path, target),
                FileSystemHandlerType.Directory => System.IO.Directory.CreateSymbolicLink(path, target),
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

        protected override IFileSystemInfo CreateSymbolicLink(String path, String target, FileSystemHandlerType handler)
        {
            return FileSystemInfoWrapper.Create(CreateSymbolicLinkUnsafe(path, target, handler));
        }

        private static FileSystemInfo? ResolveLinkTargetUnsafe(String path, Boolean target, FileSystemHandlerType handler)
        {
            return handler switch
            {
                FileSystemHandlerType.Unknown => throw new AmbiguousFileSystemHandlerException(),
                FileSystemHandlerType.Path => throw new NotSupportedException(),
                FileSystemHandlerType.Link or FileSystemHandlerType.File => System.IO.File.ResolveLinkTarget(path, target),
                FileSystemHandlerType.Directory => System.IO.Directory.ResolveLinkTarget(path, target),
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

        protected override IFileSystemInfo? ResolveLinkTarget(String path, Boolean target, FileSystemHandlerType handler)
        {
            return FileSystemInfoWrapper.Create(ResolveLinkTargetUnsafe(path, target, handler));
        }

        private static DirectoryInfo CreateDirectoryUnsafe(String path)
        {
            return System.IO.Directory.CreateDirectory(path);
        }

        DirectoryInfo IUnsafeDirectoryHandler.CreateDirectory(String path)
        {
            return CreateDirectoryUnsafe(path);
        }

        public override IDirectoryInfo CreateDirectory(String path)
        {
            return FileSystemInfoWrapper.Create(CreateDirectoryUnsafe(path));
        }

        private static DirectoryInfo CreateDirectoryUnsafe(String path, UnixFileMode mode)
        {
#if NET7_0_OR_GREATER
            return RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? CreateDirectoryUnsafe(path) : System.IO.Directory.CreateDirectory(path, mode);
#else
            return RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? CreateDirectoryUnsafe(path) : throw new NotSupportedException();
#endif
        }

        DirectoryInfo IUnsafeDirectoryHandler.CreateDirectory(String path, UnixFileMode mode)
        {
            return CreateDirectoryUnsafe(path, mode);
        }

        public override IDirectoryInfo CreateDirectory(String path, UnixFileMode mode)
        {
            return FileSystemInfoWrapper.Create(CreateDirectoryUnsafe(path, mode));
        }

        private static DirectoryInfo? GetParentUnsafe(String path, FileSystemHandlerType handler)
        {
            return handler switch
            {
                FileSystemHandlerType.Unknown => throw new AmbiguousFileSystemHandlerException(),
                FileSystemHandlerType.Path => throw new NotSupportedException(),
                FileSystemHandlerType.Link or FileSystemHandlerType.File => throw new NotSupportedException(),
                FileSystemHandlerType.Directory => System.IO.Directory.GetParent(path),
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

        protected override IDirectoryInfo? GetParent(String path, FileSystemHandlerType handler)
        {
            return FileSystemInfoWrapper.Create(GetParentUnsafe(path, handler));
        }

        private static DriveInfo[] GetDrivesUnsafe()
        {
            return DriveInfo.GetDrives();
        }

        DriveInfo[] IUnsafeDriveHandler.GetDrives()
        {
            return GetDrivesUnsafe();
        }

        public override IDriveInfo[] GetDrives()
        {
            return GetDrivesUnsafe().ConvertAll(FileSystemInfoWrapper.Create)!;
        }

        private static DirectoryInfo? GetFolderPathUnsafe(Environment.SpecialFolder folder)
        {
            return folder.ToDirectoryInfo();
        }

        DirectoryInfo? IUnsafeEnvironmentHandler.GetFolderPath(Environment.SpecialFolder folder)
        {
            return GetFolderPathUnsafe(folder);
        }

        public override IDirectoryInfo? GetFolderPath(Environment.SpecialFolder folder)
        {
            return FileSystemInfoWrapper.Create(GetFolderPathUnsafe(folder));
        }

        private static DirectoryInfo? GetFolderPathUnsafe(Environment.SpecialFolder folder, Environment.SpecialFolderOption option)
        {
            return folder.ToDirectoryInfo(option);
        }

        DirectoryInfo? IUnsafeEnvironmentHandler.GetFolderPath(Environment.SpecialFolder folder, Environment.SpecialFolderOption option)
        {
            return GetFolderPathUnsafe(folder, option);
        }

        public override IDirectoryInfo? GetFolderPath(Environment.SpecialFolder folder, Environment.SpecialFolderOption option)
        {
            return FileSystemInfoWrapper.Create(GetFolderPathUnsafe(folder, option));
        }
    }
}