// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using NetExtender.FileSystems.Interfaces;
using NetExtender.Types.Exceptions;
using NetExtender.Utilities.Types;

namespace NetExtender.FileSystems
{
    public enum FileSystemHandlerType : Byte
    {
        Unknown,
        Path,
        Link,
        File,
        Directory,
        Drive
    }

    internal sealed class FileSystem : FileSystemHandler
    {
        internal static FileSystem Instance { get; } = new FileSystem();
        
        private FileSystem()
        {
        }
    }

    //TODO: link
    public partial class FileSystemHandler : FileSystemHandler.IHandler
    {
        internal interface IHandler : IUnsafeFileSystemHandler
        {
        }

        public event PropertyChangingEventHandler? PropertyChanging;
        public event PropertyChangedEventHandler? PropertyChanged;

        public virtual Object SyncRoot
        {
            get
            {
                return this;
            }
        }

        public virtual Boolean IsSynchronized
        {
            get
            {
                return SyncRoot is { } root && !ReferenceEquals(this, root);
            }
        }

        public Guid Id { get; }

        public String? Name
        {
            get
            {
                return FileSystemName();
            }
        }
        
        protected DateTimeOffset CreationTimeOffset { get; init; } = DateTimeOffset.Now;

        public DateTime CreationTime
        {
            get
            {
                return CreationTimeFileSystem();
            }
        }

        public DateTime CreationTimeUtc
        {
            get
            {
                return CreationTimeUtcFileSystem();
            }
        }

        public Boolean IsReal
        {
            get
            {
                return IsRealFileSystem();
            }
        }

        #region FileSystem

        protected virtual IFileSystem FileSystem
        {
            get
            {
                return this;
            }
        }

        [Obsolete($"Use {nameof(IFileSystemHandler)} as specified interface {nameof(IPathHandler)}; {nameof(IFileHandler)}; {nameof(IDirectoryHandler)}.")]
        IFileSystemHandler IFileSystem.FileSystem
        {
            get
            {
                return this;
            }
        }

        public IPathHandler Path
        {
            get
            {
                return this;
            }
        }

        public ILinkHandler Link
        {
            get
            {
                return this;
            }
        }

        public IFileHandler File
        {
            get
            {
                return this;
            }
        }

        public IDirectoryHandler Directory
        {
            get
            {
                return this;
            }
        }

        public IDriveHandler Drive
        {
            get
            {
                return this;
            }
        }

        public IEnvironmentHandler Environment
        {
            get
            {
                return this;
            }
        }

        private static Encoding DefaultReadEncoding
        {
            get
            {
                return Encoding.UTF8;
            }
        }

        private static Encoding DefaultWriteEncoding
        {
            get
            {
                return EncodingUtilities.UTF8NoBOM;
            }
        }

        public Encoding? Encoding { get; init; }

        private readonly Encoding? _read;
        public Encoding ReadEncoding
        {
            get
            {
                return _read ?? Encoding ?? DefaultReadEncoding;
            }
            init
            {
                _read = value;
            }
        }

        private readonly Encoding? _write;
        public Encoding WriteEncoding
        {
            get
            {
                return _write ?? Encoding ?? DefaultWriteEncoding;
            }
            init
            {
                _write = value;
            }
        }

        public virtual StringComparer Comparer
        {
            get
            {
                return IsCaseSensitive ? StringComparer.Ordinal : StringComparer.OrdinalIgnoreCase;
            }
        }

        public virtual Boolean IsCaseSensitive
        {
            get
            {
                return true;
            }
        }

        protected internal const String AnyPattern = "*";

        public FileSystemHandler()
            : this(Guid.Empty)
        {
        }

        protected FileSystemHandler(Guid id)
        {
            Id = id;

            if (!IFileSystem.Initialize(this))
            {
                throw new InvalidOperationException($"File system with id: '{Id}' already exists. Use '{nameof(IFileSystem)}.{nameof(IFileSystem.Get)}' or '{nameof(IFileSystem)}.{nameof(IFileSystem.TryGet)}' instead.");
            }
        }

        protected void OnPropertyChanging(PropertyChangingEventArgs args)
        {
            PropertyChanging?.Invoke(this, args);
        }

        protected void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            PropertyChanged?.Invoke(this, args);
        }

        private protected virtual String? FileSystemName()
        {
            return null;
        }

        private protected virtual DateTime CreationTimeFileSystem()
        {
            return CreationTimeOffset.LocalDateTime;
        }

        private protected virtual DateTime CreationTimeUtcFileSystem()
        {
            return CreationTimeOffset.UtcDateTime;
        }

        private protected virtual Boolean IsRealFileSystem()
        {
            return true;
        }

        protected virtual IFileSystemInfo CreateSymbolicLink(String path, String target, FileSystemHandlerType handler)
        {
            return handler switch
            {
                FileSystemHandlerType.Unknown => throw new AmbiguousFileSystemHandlerException(),
                FileSystemHandlerType.Path => throw new NotSupportedException(),
                FileSystemHandlerType.Link => throw new NotSupportedException(),
                FileSystemHandlerType.File => FileSystemInfoWrapper.Create(System.IO.File.CreateSymbolicLink(path, target)),
                FileSystemHandlerType.Directory => FileSystemInfoWrapper.Create(System.IO.Directory.CreateSymbolicLink(path, target)),
                FileSystemHandlerType.Drive => throw new NotSupportedException(),
                _ => throw new EnumUndefinedOrNotSupportedException<FileSystemHandlerType>(handler, nameof(handler), null)
            };
        }

        IFileSystemInfo IFileSystemHandler.CreateSymbolicLink(String path, String target)
        {
            return CreateSymbolicLink(path, target, FileSystemHandlerType.Unknown);
        }

        protected virtual IFileSystemInfo? ResolveLinkTarget(String path, Boolean target, FileSystemHandlerType handler)
        {
            return handler switch
            {
                FileSystemHandlerType.Unknown => throw new AmbiguousFileSystemHandlerException(),
                FileSystemHandlerType.Path => throw new NotSupportedException(),
                FileSystemHandlerType.Link => throw new NotSupportedException(),
                FileSystemHandlerType.File => FileSystemInfoWrapper.Create(System.IO.File.ResolveLinkTarget(path, target)),
                FileSystemHandlerType.Directory => FileSystemInfoWrapper.Create(System.IO.Directory.ResolveLinkTarget(path, target)),
                FileSystemHandlerType.Drive => throw new NotSupportedException(),
                _ => throw new EnumUndefinedOrNotSupportedException<FileSystemHandlerType>(handler, nameof(handler), null)
            };
        }

        IFileSystemInfo? IFileSystemHandler.ResolveLinkTarget(String path, Boolean target)
        {
            return ResolveLinkTarget(path, target, FileSystemHandlerType.Unknown);
        }

        protected virtual Boolean Exists([NotNullWhen(true)] String? path, FileSystemHandlerType handler)
        {
            return handler switch
            {
                FileSystemHandlerType.Unknown => throw new AmbiguousFileSystemHandlerException(),
                FileSystemHandlerType.Path => throw new NotSupportedException(),
                FileSystemHandlerType.Link => throw new NotSupportedException(),
                FileSystemHandlerType.File => System.IO.File.Exists(path),
                FileSystemHandlerType.Directory => System.IO.Directory.Exists(path),
                FileSystemHandlerType.Drive => throw new NotSupportedException(),
                _ => throw new EnumUndefinedOrNotSupportedException<FileSystemHandlerType>(handler, nameof(handler), null)
            };
        }

        Boolean IFileSystemHandler.Exists([NotNullWhen(true)] String? path)
        {
            return Exists(path, FileSystemHandlerType.Unknown);
        }

        protected virtual IDirectoryInfo? GetParent(String path, FileSystemHandlerType handler)
        {
            return handler switch
            {
                FileSystemHandlerType.Unknown => throw new AmbiguousFileSystemHandlerException(),
                FileSystemHandlerType.Path => throw new NotSupportedException(),
                FileSystemHandlerType.Link => throw new NotSupportedException(),
                FileSystemHandlerType.File => throw new NotSupportedException(),
                FileSystemHandlerType.Directory => FileSystemInfoWrapper.Create(System.IO.Directory.GetParent(path)),
                FileSystemHandlerType.Drive => throw new NotSupportedException(),
                _ => throw new EnumUndefinedOrNotSupportedException<FileSystemHandlerType>(handler, nameof(handler), null)
            };
        }

        IDirectoryInfo? IFileSystemHandler.GetParent(String path)
        {
            return GetParent(path, FileSystemHandlerType.Unknown);
        }

        protected virtual FileAttributes GetAttributes(String path, FileSystemHandlerType handler)
        {
            return handler switch
            {
                FileSystemHandlerType.Unknown => throw new AmbiguousFileSystemHandlerException(),
                FileSystemHandlerType.Path => throw new NotSupportedException(),
                FileSystemHandlerType.Link => throw new NotSupportedException(),
                FileSystemHandlerType.File => System.IO.File.GetAttributes(path),
                FileSystemHandlerType.Directory => throw new NotSupportedException(),
                FileSystemHandlerType.Drive => throw new NotSupportedException(),
                _ => throw new EnumUndefinedOrNotSupportedException<FileSystemHandlerType>(handler, nameof(handler), null)
            };
        }

        FileAttributes IFileSystemHandler.GetAttributes(String path)
        {
            return GetAttributes(path, FileSystemHandlerType.Unknown);
        }

        protected virtual void SetAttributes(String path, FileAttributes attributes, FileSystemHandlerType handler)
        {
            switch (handler)
            {
                case FileSystemHandlerType.Unknown:
                    throw new AmbiguousFileSystemHandlerException();
                case FileSystemHandlerType.Path:
                case FileSystemHandlerType.Link:
                case FileSystemHandlerType.Directory:
                    throw new NotSupportedException();
                case FileSystemHandlerType.File:
                    System.IO.File.SetAttributes(path, attributes);
                    return;
                case FileSystemHandlerType.Drive:
                    throw new NotSupportedException();
                default:
                    throw new EnumUndefinedOrNotSupportedException<FileSystemHandlerType>(handler, nameof(handler), null);
            }
        }

        void IFileSystemHandler.SetAttributes(String path, FileAttributes attributes)
        {
            SetAttributes(path, attributes, FileSystemHandlerType.Unknown);
        }

        protected virtual DateTime GetCreationTime(String path, FileSystemHandlerType handler)
        {
            return handler switch
            {
                FileSystemHandlerType.Unknown => throw new AmbiguousFileSystemHandlerException(),
                FileSystemHandlerType.Path => throw new NotSupportedException(),
                FileSystemHandlerType.Link => throw new NotSupportedException(),
                FileSystemHandlerType.File => System.IO.File.GetCreationTime(path),
                FileSystemHandlerType.Directory => System.IO.Directory.GetCreationTime(path),
                FileSystemHandlerType.Drive => throw new NotSupportedException(),
                _ => throw new EnumUndefinedOrNotSupportedException<FileSystemHandlerType>(handler, nameof(handler), null)
            };
        }

        DateTime IFileSystemHandler.GetCreationTime(String path)
        {
            return GetCreationTime(path, FileSystemHandlerType.Unknown);
        }

        protected virtual DateTime GetCreationTimeUtc(String path, FileSystemHandlerType handler)
        {
            return handler switch
            {
                FileSystemHandlerType.Unknown => throw new AmbiguousFileSystemHandlerException(),
                FileSystemHandlerType.Path => throw new NotSupportedException(),
                FileSystemHandlerType.Link => throw new NotSupportedException(),
                FileSystemHandlerType.File => System.IO.File.GetCreationTimeUtc(path),
                FileSystemHandlerType.Directory => System.IO.Directory.GetCreationTimeUtc(path),
                FileSystemHandlerType.Drive => throw new NotSupportedException(),
                _ => throw new EnumUndefinedOrNotSupportedException<FileSystemHandlerType>(handler, nameof(handler), null)
            };
        }

        DateTime IFileSystemHandler.GetCreationTimeUtc(String path)
        {
            return GetCreationTimeUtc(path, FileSystemHandlerType.Unknown);
        }

        protected virtual DateTime GetLastAccessTime(String path, FileSystemHandlerType handler)
        {
            return handler switch
            {
                FileSystemHandlerType.Unknown => throw new AmbiguousFileSystemHandlerException(),
                FileSystemHandlerType.Path => throw new NotSupportedException(),
                FileSystemHandlerType.Link => throw new NotSupportedException(),
                FileSystemHandlerType.File => System.IO.File.GetLastAccessTime(path),
                FileSystemHandlerType.Directory => System.IO.Directory.GetLastAccessTime(path),
                FileSystemHandlerType.Drive => throw new NotSupportedException(),
                _ => throw new EnumUndefinedOrNotSupportedException<FileSystemHandlerType>(handler, nameof(handler), null)
            };
        }

        DateTime IFileSystemHandler.GetLastAccessTime(String path)
        {
            return GetLastAccessTime(path, FileSystemHandlerType.Unknown);
        }

        protected virtual DateTime GetLastAccessTimeUtc(String path, FileSystemHandlerType handler)
        {
            return handler switch
            {
                FileSystemHandlerType.Unknown => throw new AmbiguousFileSystemHandlerException(),
                FileSystemHandlerType.Path => throw new NotSupportedException(),
                FileSystemHandlerType.Link => throw new NotSupportedException(),
                FileSystemHandlerType.File => System.IO.File.GetLastAccessTimeUtc(path),
                FileSystemHandlerType.Directory => System.IO.Directory.GetLastAccessTimeUtc(path),
                FileSystemHandlerType.Drive => throw new NotSupportedException(),
                _ => throw new EnumUndefinedOrNotSupportedException<FileSystemHandlerType>(handler, nameof(handler), null)
            };
        }

        DateTime IFileSystemHandler.GetLastAccessTimeUtc(String path)
        {
            return GetLastAccessTimeUtc(path, FileSystemHandlerType.Unknown);
        }

        protected virtual DateTime GetLastWriteTime(String path, FileSystemHandlerType handler)
        {
            return handler switch
            {
                FileSystemHandlerType.Unknown => throw new AmbiguousFileSystemHandlerException(),
                FileSystemHandlerType.Path => throw new NotSupportedException(),
                FileSystemHandlerType.Link => throw new NotSupportedException(),
                FileSystemHandlerType.File => System.IO.File.GetLastWriteTime(path),
                FileSystemHandlerType.Directory => System.IO.Directory.GetLastWriteTime(path),
                FileSystemHandlerType.Drive => throw new NotSupportedException(),
                _ => throw new EnumUndefinedOrNotSupportedException<FileSystemHandlerType>(handler, nameof(handler), null)
            };
        }

        DateTime IFileSystemHandler.GetLastWriteTime(String path)
        {
            return GetLastWriteTime(path, FileSystemHandlerType.Unknown);
        }

        protected virtual DateTime GetLastWriteTimeUtc(String path, FileSystemHandlerType handler)
        {
            return handler switch
            {
                FileSystemHandlerType.Unknown => throw new AmbiguousFileSystemHandlerException(),
                FileSystemHandlerType.Path => throw new NotSupportedException(),
                FileSystemHandlerType.Link => throw new NotSupportedException(),
                FileSystemHandlerType.File => System.IO.File.GetLastWriteTimeUtc(path),
                FileSystemHandlerType.Directory => System.IO.Directory.GetLastWriteTimeUtc(path),
                FileSystemHandlerType.Drive => throw new NotSupportedException(),
                _ => throw new EnumUndefinedOrNotSupportedException<FileSystemHandlerType>(handler, nameof(handler), null)
            };
        }

        DateTime IFileSystemHandler.GetLastWriteTimeUtc(String path)
        {
            return GetLastWriteTimeUtc(path, FileSystemHandlerType.Unknown);
        }

        protected virtual void SetCreationTime(String path, DateTime time, FileSystemHandlerType handler)
        {
            switch (handler)
            {
                case FileSystemHandlerType.Unknown:
                    throw new AmbiguousFileSystemHandlerException();
                case FileSystemHandlerType.Path:
                case FileSystemHandlerType.Link:
                    throw new NotSupportedException();
                case FileSystemHandlerType.File:
                    System.IO.File.SetCreationTime(path, time);
                    return;
                case FileSystemHandlerType.Directory:
                    System.IO.Directory.SetCreationTime(path, time);
                    return;
                case FileSystemHandlerType.Drive:
                    throw new NotSupportedException();
                default:
                    throw new EnumUndefinedOrNotSupportedException<FileSystemHandlerType>(handler, nameof(handler), null);
            }
        }

        void IFileSystemHandler.SetCreationTime(String path, DateTime time)
        {
            SetCreationTime(path, time, FileSystemHandlerType.Unknown);
        }

        protected virtual void SetCreationTimeUtc(String path, DateTime time, FileSystemHandlerType handler)
        {
            switch (handler)
            {
                case FileSystemHandlerType.Unknown:
                    throw new AmbiguousFileSystemHandlerException();
                case FileSystemHandlerType.Path:
                case FileSystemHandlerType.Link:
                    throw new NotSupportedException();
                case FileSystemHandlerType.File:
                    System.IO.File.SetCreationTimeUtc(path, time);
                    return;
                case FileSystemHandlerType.Directory:
                    System.IO.Directory.SetCreationTimeUtc(path, time);
                    return;
                case FileSystemHandlerType.Drive:
                    throw new NotSupportedException();
                default:
                    throw new EnumUndefinedOrNotSupportedException<FileSystemHandlerType>(handler, nameof(handler), null);
            }
        }

        void IFileSystemHandler.SetCreationTimeUtc(String path, DateTime time)
        {
            SetCreationTimeUtc(path, time, FileSystemHandlerType.Unknown);
        }

        protected virtual void SetLastAccessTime(String path, DateTime time, FileSystemHandlerType handler)
        {
            switch (handler)
            {
                case FileSystemHandlerType.Unknown:
                    throw new AmbiguousFileSystemHandlerException();
                case FileSystemHandlerType.Path:
                case FileSystemHandlerType.Link:
                    throw new NotSupportedException();
                case FileSystemHandlerType.File:
                    System.IO.File.SetLastAccessTime(path, time);
                    return;
                case FileSystemHandlerType.Directory:
                    System.IO.Directory.SetLastAccessTime(path, time);
                    return;
                case FileSystemHandlerType.Drive:
                    throw new NotSupportedException();
                default:
                    throw new EnumUndefinedOrNotSupportedException<FileSystemHandlerType>(handler, nameof(handler), null);
            }
        }

        void IFileSystemHandler.SetLastAccessTime(String path, DateTime time)
        {
            SetLastAccessTime(path, time, FileSystemHandlerType.Unknown);
        }

        protected virtual void SetLastAccessTimeUtc(String path, DateTime time, FileSystemHandlerType handler)
        {
            switch (handler)
            {
                case FileSystemHandlerType.Unknown:
                    throw new AmbiguousFileSystemHandlerException();
                case FileSystemHandlerType.Path:
                case FileSystemHandlerType.Link:
                    throw new NotSupportedException();
                case FileSystemHandlerType.File:
                    System.IO.File.SetLastAccessTimeUtc(path, time);
                    return;
                case FileSystemHandlerType.Directory:
                    System.IO.Directory.SetLastAccessTimeUtc(path, time);
                    return;
                case FileSystemHandlerType.Drive:
                    throw new NotSupportedException();
                default:
                    throw new EnumUndefinedOrNotSupportedException<FileSystemHandlerType>(handler, nameof(handler), null);
            }
        }

        void IFileSystemHandler.SetLastAccessTimeUtc(String path, DateTime time)
        {
            SetLastAccessTimeUtc(path, time, FileSystemHandlerType.Unknown);
        }

        protected virtual void SetLastWriteTime(String path, DateTime time, FileSystemHandlerType handler)
        {
            switch (handler)
            {
                case FileSystemHandlerType.Unknown:
                    throw new AmbiguousFileSystemHandlerException();
                case FileSystemHandlerType.Path:
                case FileSystemHandlerType.Link:
                    throw new NotSupportedException();
                case FileSystemHandlerType.File:
                    System.IO.File.SetLastWriteTime(path, time);
                    return;
                case FileSystemHandlerType.Directory:
                    System.IO.Directory.SetLastWriteTime(path, time);
                    return;
                case FileSystemHandlerType.Drive:
                    throw new NotSupportedException();
                default:
                    throw new EnumUndefinedOrNotSupportedException<FileSystemHandlerType>(handler, nameof(handler), null);
            }
        }

        void IFileSystemHandler.SetLastWriteTime(String path, DateTime time)
        {
            SetLastWriteTime(path, time, FileSystemHandlerType.Unknown);
        }

        protected virtual void SetLastWriteTimeUtc(String path, DateTime time, FileSystemHandlerType handler)
        {
            switch (handler)
            {
                case FileSystemHandlerType.Unknown:
                    throw new AmbiguousFileSystemHandlerException();
                case FileSystemHandlerType.Path:
                case FileSystemHandlerType.Link:
                    throw new NotSupportedException();
                case FileSystemHandlerType.File:
                    System.IO.File.SetLastWriteTimeUtc(path, time);
                    return;
                case FileSystemHandlerType.Directory:
                    System.IO.Directory.SetLastWriteTimeUtc(path, time);
                    return;
                case FileSystemHandlerType.Drive:
                    throw new NotSupportedException();
                default:
                    throw new EnumUndefinedOrNotSupportedException<FileSystemHandlerType>(handler, nameof(handler), null);
            }
        }

        void IFileSystemHandler.SetLastWriteTimeUtc(String path, DateTime time)
        {
            SetLastWriteTimeUtc(path, time, FileSystemHandlerType.Unknown);
        }

        protected virtual FileStream Create(String path, FileSystemHandlerType handler)
        {
            return handler switch
            {
                FileSystemHandlerType.Unknown => throw new AmbiguousFileSystemHandlerException(),
                FileSystemHandlerType.Path => throw new NotSupportedException(),
                FileSystemHandlerType.Link => throw new NotSupportedException(),
                FileSystemHandlerType.File => System.IO.File.Create(path),
                FileSystemHandlerType.Directory => throw new NotSupportedException(),
                FileSystemHandlerType.Drive => throw new NotSupportedException(),
                _ => throw new EnumUndefinedOrNotSupportedException<FileSystemHandlerType>(handler, nameof(handler), null)
            };
        }

        FileStream IFileSystemHandler.Create(String path)
        {
            return Create(path, FileSystemHandlerType.Unknown);
        }

        protected virtual Boolean Encrypt(String path, FileSystemHandlerType handler)
        {
            switch (handler)
            {
                case FileSystemHandlerType.Unknown:
                    throw new AmbiguousFileSystemHandlerException();
                case FileSystemHandlerType.Path:
                case FileSystemHandlerType.Link:
                case FileSystemHandlerType.Directory:
                    throw new NotSupportedException();
                case FileSystemHandlerType.File:
                    if (!OperatingSystem.IsWindows())
                    {
                        return false;
                    }

                    try
                    {
                        System.IO.File.Encrypt(path);
                        return true;
                    }
                    catch (NotSupportedException)
                    {
                        return false;
                    }
                case FileSystemHandlerType.Drive:
                    throw new NotSupportedException();
                default:
                    throw new EnumUndefinedOrNotSupportedException<FileSystemHandlerType>(handler, nameof(handler), null);
            }
        }

        Boolean IFileSystemHandler.Encrypt(String path)
        {
            return Encrypt(path, FileSystemHandlerType.Unknown);
        }

        protected virtual Boolean Decrypt(String path, FileSystemHandlerType handler)
        {
            switch (handler)
            {
                case FileSystemHandlerType.Unknown:
                    throw new AmbiguousFileSystemHandlerException();
                case FileSystemHandlerType.Path:
                case FileSystemHandlerType.Link:
                case FileSystemHandlerType.Directory:
                    throw new NotSupportedException();
                case FileSystemHandlerType.File:
                    if (!OperatingSystem.IsWindows())
                    {
                        return false;
                    }

                    try
                    {
                        System.IO.File.Decrypt(path);
                        return true;
                    }
                    catch (NotSupportedException)
                    {
                        return false;
                    }
                case FileSystemHandlerType.Drive:
                    throw new NotSupportedException();
                default:
                    throw new EnumUndefinedOrNotSupportedException<FileSystemHandlerType>(handler, nameof(handler), null);
            }
        }

        Boolean IFileSystemHandler.Decrypt(String path)
        {
            return Decrypt(path, FileSystemHandlerType.Unknown);
        }

        protected virtual void Move(String source, String destination, FileSystemHandlerType handler)
        {
            switch (handler)
            {
                case FileSystemHandlerType.Unknown:
                    throw new AmbiguousFileSystemHandlerException();
                case FileSystemHandlerType.Path:
                case FileSystemHandlerType.Link:
                    throw new NotSupportedException();
                case FileSystemHandlerType.File:
                    System.IO.File.Move(source, destination);
                    return;
                case FileSystemHandlerType.Directory:
                    System.IO.Directory.Move(source, destination);
                    return;
                case FileSystemHandlerType.Drive:
                    throw new NotSupportedException();
                default:
                    throw new EnumUndefinedOrNotSupportedException<FileSystemHandlerType>(handler, nameof(handler), null);
            }
        }

        void IFileSystemHandler.Move(String source, String destination)
        {
            Move(source, destination, FileSystemHandlerType.Unknown);
        }

        protected virtual void Move(String source, String destination, Boolean overwrite, FileSystemHandlerType handler)
        {
            switch (handler)
            {
                case FileSystemHandlerType.Unknown:
                    throw new AmbiguousFileSystemHandlerException();
                case FileSystemHandlerType.Path:
                case FileSystemHandlerType.Link:
                case FileSystemHandlerType.Directory:
                    throw new NotSupportedException();
                case FileSystemHandlerType.File:
                    System.IO.File.Move(source, destination, overwrite);
                    return;
                case FileSystemHandlerType.Drive:
                    throw new NotSupportedException();
                default:
                    throw new EnumUndefinedOrNotSupportedException<FileSystemHandlerType>(handler, nameof(handler), null);
            }
        }

        void IFileSystemHandler.Move(String source, String destination, Boolean overwrite)
        {
            Move(source, destination, overwrite, FileSystemHandlerType.Unknown);
        }

        protected virtual void Copy(String source, String destination, FileSystemHandlerType handler)
        {
            switch (handler)
            {
                case FileSystemHandlerType.Unknown:
                    throw new AmbiguousFileSystemHandlerException();
                case FileSystemHandlerType.Path:
                case FileSystemHandlerType.Link:
                case FileSystemHandlerType.Directory:
                    throw new NotSupportedException();
                case FileSystemHandlerType.File:
                    System.IO.File.Copy(source, destination);
                    return;
                case FileSystemHandlerType.Drive:
                    throw new NotSupportedException();
                default:
                    throw new EnumUndefinedOrNotSupportedException<FileSystemHandlerType>(handler, nameof(handler), null);
            }
        }

        void IFileSystemHandler.Copy(String source, String destination)
        {
            Copy(source, destination, FileSystemHandlerType.Unknown);
        }

        protected virtual void Copy(String source, String destination, Boolean overwrite, FileSystemHandlerType handler)
        {
            switch (handler)
            {
                case FileSystemHandlerType.Unknown:
                    throw new AmbiguousFileSystemHandlerException();
                case FileSystemHandlerType.Path:
                case FileSystemHandlerType.Link:
                case FileSystemHandlerType.Directory:
                    throw new NotSupportedException();
                case FileSystemHandlerType.File:
                    System.IO.File.Copy(source, destination, overwrite);
                    return;
                case FileSystemHandlerType.Drive:
                    throw new NotSupportedException();
                default:
                    throw new EnumUndefinedOrNotSupportedException<FileSystemHandlerType>(handler, nameof(handler), null);
            }
        }

        void IFileSystemHandler.Copy(String source, String destination, Boolean overwrite)
        {
            Copy(source, destination, overwrite, FileSystemHandlerType.Unknown);
        }

        protected virtual void Replace(String source, String destination, String? backup, FileSystemHandlerType handler)
        {
            switch (handler)
            {
                case FileSystemHandlerType.Unknown:
                    throw new AmbiguousFileSystemHandlerException();
                case FileSystemHandlerType.Path:
                case FileSystemHandlerType.Link:
                case FileSystemHandlerType.Directory:
                    throw new NotSupportedException();
                case FileSystemHandlerType.File:
                    System.IO.File.Replace(source, destination, backup);
                    return;
                case FileSystemHandlerType.Drive:
                    throw new NotSupportedException();
                default:
                    throw new EnumUndefinedOrNotSupportedException<FileSystemHandlerType>(handler, nameof(handler), null);
            }
        }

        void IFileSystemHandler.Replace(String source, String destination, String? backup)
        {
            Replace(source, destination, backup, FileSystemHandlerType.Unknown);
        }

        protected virtual void Replace(String source, String destination, String? backup, Boolean suppress, FileSystemHandlerType handler)
        {
            switch (handler)
            {
                case FileSystemHandlerType.Unknown:
                    throw new AmbiguousFileSystemHandlerException();
                case FileSystemHandlerType.Path:
                case FileSystemHandlerType.Link:
                case FileSystemHandlerType.Directory:
                    throw new NotSupportedException();
                case FileSystemHandlerType.File:
                    System.IO.File.Replace(source, destination, backup, suppress);
                    return;
                case FileSystemHandlerType.Drive:
                    throw new NotSupportedException();
                default:
                    throw new EnumUndefinedOrNotSupportedException<FileSystemHandlerType>(handler, nameof(handler), null);
            }
        }

        void IFileSystemHandler.Replace(String source, String destination, String? backup, Boolean suppress)
        {
            Replace(source, destination, backup, suppress, FileSystemHandlerType.Unknown);
        }

        protected virtual void Delete(String path, FileSystemHandlerType handler)
        {
            switch (handler)
            {
                case FileSystemHandlerType.Unknown:
                    throw new AmbiguousFileSystemHandlerException();
                case FileSystemHandlerType.Path:
                case FileSystemHandlerType.Link:
                    throw new NotSupportedException();
                case FileSystemHandlerType.File:
                    System.IO.File.Delete(path);
                    return;
                case FileSystemHandlerType.Directory:
                    System.IO.Directory.Delete(path);
                    return;
                case FileSystemHandlerType.Drive:
                    throw new NotSupportedException();
                default:
                    throw new EnumUndefinedOrNotSupportedException<FileSystemHandlerType>(handler, nameof(handler), null);
            }
        }

        void IFileSystemHandler.Delete(String path)
        {
            Delete(path, FileSystemHandlerType.Unknown);
        }

        protected virtual void Delete(String path, Boolean recursive, FileSystemHandlerType handler)
        {
            switch (handler)
            {
                case FileSystemHandlerType.Unknown:
                    throw new AmbiguousFileSystemHandlerException();
                case FileSystemHandlerType.Path:
                case FileSystemHandlerType.Link:
                case FileSystemHandlerType.File:
                    throw new NotSupportedException();
                case FileSystemHandlerType.Directory:
                    System.IO.Directory.Delete(path, recursive);
                    return;
                case FileSystemHandlerType.Drive:
                    throw new NotSupportedException();
                default:
                    throw new EnumUndefinedOrNotSupportedException<FileSystemHandlerType>(handler, nameof(handler), null);
            }
        }

        void IFileSystemHandler.Delete(String path, Boolean recursive)
        {
            Delete(path, recursive, FileSystemHandlerType.Unknown);
        }
        
        #endregion

        public void Dispose()
        {
            Dispose(true);
            IFileSystem.Dispose(this);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(Boolean disposing)
        {
        }

        ~FileSystemHandler()
        {
            Dispose(false);
            IFileSystem.Dispose(this);
        }
        
        private static partial class Handler
        {
            static Handler()
            {
                const BindingFlags binding = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
                GetVolumeNameHandler = typeof(Path).GetMethod(nameof(GetVolumeName), binding, new[] { typeof(ReadOnlySpan<Char>) })?.CreateDelegate<GetVolumeNameDelegate>();
                GetUncRootLengthHandler = typeof(Path).GetMethod(nameof(GetUncRootLength), binding, new[] { typeof(ReadOnlySpan<Char>) })?.CreateDelegate<GetUncRootLengthDelegate>();
                SetCommandLineArgsHandler = typeof(Environment).GetMethod(nameof(SetCommandLineArgs), binding, new[] { typeof(String[]) })?.CreateDelegate<SetCommandLineArgsDelegate>();
            }
        }
    }

    [Serializable]
    public sealed class AmbiguousFileSystemHandlerException : Exception
    {
        public AmbiguousFileSystemHandlerException()
        {
        }

        public AmbiguousFileSystemHandlerException(String? message)
            : base(message)
        {
        }

        public AmbiguousFileSystemHandlerException(String? message, Exception? exception)
            : base(message, exception)
        {
        }

        private AmbiguousFileSystemHandlerException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
    
    [Serializable]
    public sealed class FileSystemIsNotRealException : InvalidOperationException
    {
        private new const String Message = "Entry '{0}' of file system '{1}' is not a real file system entry.";

        internal FileSystemIsNotRealException(IFileSystemStorageInfo? info)
            : base(Format(info))
        {
        }

        public FileSystemIsNotRealException(String? message)
            : base(message)
        {
        }

        public FileSystemIsNotRealException(String? message, Exception? exception)
            : base(message, exception)
        {
        }

        private FileSystemIsNotRealException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        [return: NotNullIfNotNull("entry")]
        private static String? Format(IFileSystemStorageInfo? entry)
        {
            return entry is not null ? Message.Format(entry.Name ?? StringUtilities.NullString, entry.FileSystem.Name) : null;
        }
    }
}