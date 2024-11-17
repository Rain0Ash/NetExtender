using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Runtime.Versioning;
using NetExtender.IO.Interfaces;
using NetExtender.Types.Exceptions;

namespace NetExtender.IO
{
    public enum FileSystemHandlerType : Byte
    {
        Unknown,
        Path,
        File,
        Directory
    }

    public partial class FileSystemHandler : IUnsafeFileSystemHandler
    {
        public static class Default
        {
            private static FileSystem Instance { get; } = new FileSystem();

            internal static IUnsafeFileSystemHandler Handler
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Instance;
                }
            }
            
            public static IPathHandler Path
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Instance;
                }
            }
            
            public static IFileHandler File
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Instance;
                }
            }
            
            public static IDirectoryHandler Directory
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get
                {
                    return Instance;
                }
            }

            private sealed class FileSystem : FileSystemHandler
            {
            }
        }
        
        #region FileSystem

        [Serializable]
        protected class AmbiguousFileSystemHandlerException : Exception
        {
            public AmbiguousFileSystemHandlerException()
            {
            }

            protected AmbiguousFileSystemHandlerException(SerializationInfo info, StreamingContext context)
                : base(info, context)
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
        }

        protected virtual FileSystemInfo CreateSymbolicLink(String path, String target, FileSystemHandlerType handler)
        {
            return handler switch
            {
                FileSystemHandlerType.Unknown => throw new AmbiguousFileSystemHandlerException(),
                FileSystemHandlerType.Path => throw new NotSupportedException(),
                FileSystemHandlerType.File => File.CreateSymbolicLink(path, target),
                FileSystemHandlerType.Directory => Directory.CreateSymbolicLink(path, target),
                _ => throw new EnumUndefinedOrNotSupportedException<FileSystemHandlerType>(handler, nameof(handler), null)
            };
        }

        FileSystemInfo IFileSystemHandler.CreateSymbolicLink(String path, String target)
        {
            return CreateSymbolicLink(path, target, FileSystemHandlerType.Unknown);
        }

        protected virtual FileSystemInfo? ResolveLinkTarget(String path, Boolean target, FileSystemHandlerType handler)
        {
            return handler switch
            {
                FileSystemHandlerType.Unknown => throw new AmbiguousFileSystemHandlerException(),
                FileSystemHandlerType.Path => throw new NotSupportedException(),
                FileSystemHandlerType.File => File.ResolveLinkTarget(path, target),
                FileSystemHandlerType.Directory => Directory.ResolveLinkTarget(path, target),
                _ => throw new EnumUndefinedOrNotSupportedException<FileSystemHandlerType>(handler, nameof(handler), null)
            };
        }

        FileSystemInfo? IFileSystemHandler.ResolveLinkTarget(String path, Boolean target)
        {
            return ResolveLinkTarget(path, target, FileSystemHandlerType.Unknown);
        }

        protected virtual Boolean Exists([NotNullWhen(true)] String? path, FileSystemHandlerType handler)
        {
            return handler switch
            {
                FileSystemHandlerType.Unknown => throw new AmbiguousFileSystemHandlerException(),
                FileSystemHandlerType.Path => throw new NotSupportedException(),
                FileSystemHandlerType.File => File.Exists(path),
                FileSystemHandlerType.Directory => Directory.Exists(path),
                _ => throw new EnumUndefinedOrNotSupportedException<FileSystemHandlerType>(handler, nameof(handler), null)
            };
        }

        Boolean IFileSystemHandler.Exists([NotNullWhen(true)] String? path)
        {
            return Exists(path, FileSystemHandlerType.Unknown);
        }

        protected virtual DirectoryInfo? GetParent(String path, FileSystemHandlerType handler)
        {
            return handler switch
            {
                FileSystemHandlerType.Unknown => throw new AmbiguousFileSystemHandlerException(),
                FileSystemHandlerType.Path => throw new NotSupportedException(),
                FileSystemHandlerType.File => throw new NotSupportedException(),
                FileSystemHandlerType.Directory => Directory.GetParent(path),
                _ => throw new EnumUndefinedOrNotSupportedException<FileSystemHandlerType>(handler, nameof(handler), null)
            };
        }

        DirectoryInfo? IFileSystemHandler.GetParent(String path)
        {
            return GetParent(path, FileSystemHandlerType.Unknown);
        }

        protected virtual FileAttributes GetAttributes(String path, FileSystemHandlerType handler)
        {
            return handler switch
            {
                FileSystemHandlerType.Unknown => throw new AmbiguousFileSystemHandlerException(),
                FileSystemHandlerType.Path => throw new NotSupportedException(),
                FileSystemHandlerType.File => File.GetAttributes(path),
                FileSystemHandlerType.Directory => throw new NotSupportedException(),
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
                case FileSystemHandlerType.Directory:
                    throw new NotSupportedException();
                case FileSystemHandlerType.File:
                    File.SetAttributes(path, attributes);
                    return;
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
                FileSystemHandlerType.File => File.GetCreationTime(path),
                FileSystemHandlerType.Directory => Directory.GetCreationTime(path),
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
                FileSystemHandlerType.File => File.GetCreationTimeUtc(path),
                FileSystemHandlerType.Directory => Directory.GetCreationTimeUtc(path),
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
                FileSystemHandlerType.File => File.GetLastAccessTime(path),
                FileSystemHandlerType.Directory => Directory.GetLastAccessTime(path),
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
                FileSystemHandlerType.File => File.GetLastAccessTimeUtc(path),
                FileSystemHandlerType.Directory => Directory.GetLastAccessTimeUtc(path),
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
                FileSystemHandlerType.File => File.GetLastWriteTime(path),
                FileSystemHandlerType.Directory => Directory.GetLastWriteTime(path),
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
                FileSystemHandlerType.File => File.GetLastWriteTimeUtc(path),
                FileSystemHandlerType.Directory => Directory.GetLastWriteTimeUtc(path),
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
                    throw new NotSupportedException();
                case FileSystemHandlerType.File:
                    File.SetCreationTime(path, time);
                    return;
                case FileSystemHandlerType.Directory:
                    Directory.SetCreationTime(path, time);
                    return;
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
                    throw new NotSupportedException();
                case FileSystemHandlerType.File:
                    File.SetCreationTimeUtc(path, time);
                    return;
                case FileSystemHandlerType.Directory:
                    Directory.SetCreationTimeUtc(path, time);
                    return;
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
                    throw new NotSupportedException();
                case FileSystemHandlerType.File:
                    File.SetLastAccessTime(path, time);
                    return;
                case FileSystemHandlerType.Directory:
                    Directory.SetLastAccessTime(path, time);
                    return;
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
                    throw new NotSupportedException();
                case FileSystemHandlerType.File:
                    File.SetLastAccessTimeUtc(path, time);
                    return;
                case FileSystemHandlerType.Directory:
                    Directory.SetLastAccessTimeUtc(path, time);
                    return;
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
                    throw new NotSupportedException();
                case FileSystemHandlerType.File:
                    File.SetLastWriteTime(path, time);
                    return;
                case FileSystemHandlerType.Directory:
                    Directory.SetLastWriteTime(path, time);
                    return;
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
                    throw new NotSupportedException();
                case FileSystemHandlerType.File:
                    File.SetLastWriteTimeUtc(path, time);
                    return;
                case FileSystemHandlerType.Directory:
                    Directory.SetLastWriteTimeUtc(path, time);
                    return;
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
                FileSystemHandlerType.File => File.Create(path),
                FileSystemHandlerType.Directory => throw new NotSupportedException(),
                _ => throw new EnumUndefinedOrNotSupportedException<FileSystemHandlerType>(handler, nameof(handler), null)
            };
        }

        FileStream IFileSystemHandler.Create(String path)
        {
            return Create(path, FileSystemHandlerType.Unknown);
        }

        [SupportedOSPlatform("windows")]
        protected virtual void Encrypt(String path, FileSystemHandlerType handler)
        {
            switch (handler)
            {
                case FileSystemHandlerType.Unknown:
                    throw new AmbiguousFileSystemHandlerException();
                case FileSystemHandlerType.Path:
                case FileSystemHandlerType.Directory:
                    throw new NotSupportedException();
                case FileSystemHandlerType.File:
                    File.Encrypt(path);
                    return;
                default:
                    throw new EnumUndefinedOrNotSupportedException<FileSystemHandlerType>(handler, nameof(handler), null);
            }
        }

        [SupportedOSPlatform("windows")]
        void IFileSystemHandler.Encrypt(String path)
        {
            Encrypt(path, FileSystemHandlerType.Unknown);
        }

        [SupportedOSPlatform("windows")]
        protected virtual void Decrypt(String path, FileSystemHandlerType handler)
        {
            switch (handler)
            {
                case FileSystemHandlerType.Unknown:
                    throw new AmbiguousFileSystemHandlerException();
                case FileSystemHandlerType.Path:
                case FileSystemHandlerType.Directory:
                    throw new NotSupportedException();
                case FileSystemHandlerType.File:
                    File.Decrypt(path);
                    return;
                default:
                    throw new EnumUndefinedOrNotSupportedException<FileSystemHandlerType>(handler, nameof(handler), null);
            }
        }

        [SupportedOSPlatform("windows")]
        void IFileSystemHandler.Decrypt(String path)
        {
            Decrypt(path, FileSystemHandlerType.Unknown);
        }

        protected virtual void Move(String source, String destination, FileSystemHandlerType handler)
        {
            switch (handler)
            {
                case FileSystemHandlerType.Unknown:
                    throw new AmbiguousFileSystemHandlerException();
                case FileSystemHandlerType.Path:
                    throw new NotSupportedException();
                case FileSystemHandlerType.File:
                    File.Move(source, destination);
                    return;
                case FileSystemHandlerType.Directory:
                    Directory.Move(source, destination);
                    return;
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
                case FileSystemHandlerType.Directory:
                    throw new NotSupportedException();
                case FileSystemHandlerType.File:
                    File.Move(source, destination, overwrite);
                    return;
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
                case FileSystemHandlerType.Directory:
                    throw new NotSupportedException();
                case FileSystemHandlerType.File:
                    File.Copy(source, destination);
                    return;
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
                case FileSystemHandlerType.Directory:
                    throw new NotSupportedException();
                case FileSystemHandlerType.File:
                    File.Copy(source, destination, overwrite);
                    return;
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
                case FileSystemHandlerType.Directory:
                    throw new NotSupportedException();
                case FileSystemHandlerType.File:
                    File.Replace(source, destination, backup);
                    return;
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
                case FileSystemHandlerType.Directory:
                    throw new NotSupportedException();
                case FileSystemHandlerType.File:
                    File.Replace(source, destination, backup, suppress);
                    return;
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
                    throw new NotSupportedException();
                case FileSystemHandlerType.File:
                    File.Delete(path);
                    return;
                case FileSystemHandlerType.Directory:
                    Directory.Delete(path);
                    return;
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
                case FileSystemHandlerType.File:
                    throw new NotSupportedException();
                case FileSystemHandlerType.Directory:
                    Directory.Delete(path, recursive);
                    return;
                default:
                    throw new EnumUndefinedOrNotSupportedException<FileSystemHandlerType>(handler, nameof(handler), null);
            }
        }

        void IFileSystemHandler.Delete(String path, Boolean recursive)
        {
            Delete(path, recursive, FileSystemHandlerType.Unknown);
        }
        
        #endregion
    }
}