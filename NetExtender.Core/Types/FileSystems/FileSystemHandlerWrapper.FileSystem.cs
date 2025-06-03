// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using NetExtender.FileSystems.Interfaces;
using NetExtender.Types.Exceptions;

namespace NetExtender.FileSystems
{
    public partial class FileSystemHandlerWrapper<T>
    {
        protected override IFileSystemInfo CreateSymbolicLink(String path, String target, FileSystemHandlerType handler)
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

        protected override IFileSystemInfo? ResolveLinkTarget(String path, Boolean target, FileSystemHandlerType handler)
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

        protected override Boolean Exists([NotNullWhen(true)] String? path, FileSystemHandlerType handler)
        {
            return handler switch
            {
                FileSystemHandlerType.Unknown => throw new AmbiguousFileSystemHandlerException(),
                FileSystemHandlerType.Path => throw new NotSupportedException(),
                FileSystemHandlerType.Link => FileSystem.Link.Exists(path),
                FileSystemHandlerType.File => FileSystem.File.Exists(path),
                FileSystemHandlerType.Directory => FileSystem.Directory.Exists(path),
                FileSystemHandlerType.Drive => throw new NotSupportedException(),
                _ => throw new EnumUndefinedOrNotSupportedException<FileSystemHandlerType>(handler, nameof(handler), null)
            };
        }

        protected override IDirectoryInfo? GetParent(String path, FileSystemHandlerType handler)
        {
            return handler switch
            {
                FileSystemHandlerType.Unknown => throw new AmbiguousFileSystemHandlerException(),
                FileSystemHandlerType.Path => throw new NotSupportedException(),
                FileSystemHandlerType.Link => throw new NotSupportedException(),
                FileSystemHandlerType.File => throw new NotSupportedException(),
                FileSystemHandlerType.Directory => FileSystem.Directory.GetParent(path),
                FileSystemHandlerType.Drive => throw new NotSupportedException(),
                _ => throw new EnumUndefinedOrNotSupportedException<FileSystemHandlerType>(handler, nameof(handler), null)
            };
        }

        protected override FileAttributes GetAttributes(String path, FileSystemHandlerType handler)
        {
            return handler switch
            {
                FileSystemHandlerType.Unknown => throw new AmbiguousFileSystemHandlerException(),
                FileSystemHandlerType.Path => throw new NotSupportedException(),
                FileSystemHandlerType.Link => FileSystem.Link.GetAttributes(path),
                FileSystemHandlerType.File => FileSystem.File.GetAttributes(path),
                FileSystemHandlerType.Directory => throw new NotSupportedException(),
                FileSystemHandlerType.Drive => throw new NotSupportedException(),
                _ => throw new EnumUndefinedOrNotSupportedException<FileSystemHandlerType>(handler, nameof(handler), null)
            };
        }

        protected override void SetAttributes(String path, FileAttributes attributes, FileSystemHandlerType handler)
        {
            switch (handler)
            {
                case FileSystemHandlerType.Unknown:
                    throw new AmbiguousFileSystemHandlerException();
                case FileSystemHandlerType.Path:
                    throw new NotSupportedException();
                case FileSystemHandlerType.Link:
                    FileSystem.Link.SetAttributes(path, attributes);
                    return;
                case FileSystemHandlerType.File:
                    FileSystem.File.SetAttributes(path, attributes);
                    return;
                case FileSystemHandlerType.Directory:
                case FileSystemHandlerType.Drive:
                    throw new NotSupportedException();
                default:
                    throw new EnumUndefinedOrNotSupportedException<FileSystemHandlerType>(handler, nameof(handler), null);
            }
        }

        protected override DateTime GetCreationTime(String path, FileSystemHandlerType handler)
        {
            return handler switch
            {
                FileSystemHandlerType.Unknown => throw new AmbiguousFileSystemHandlerException(),
                FileSystemHandlerType.Path => throw new NotSupportedException(),
                FileSystemHandlerType.Link => FileSystem.Link.GetCreationTime(path),
                FileSystemHandlerType.File => FileSystem.File.GetCreationTime(path),
                FileSystemHandlerType.Directory => FileSystem.Directory.GetCreationTime(path),
                FileSystemHandlerType.Drive => throw new NotSupportedException(),
                _ => throw new EnumUndefinedOrNotSupportedException<FileSystemHandlerType>(handler, nameof(handler), null)
            };
        }

        protected override DateTime GetCreationTimeUtc(String path, FileSystemHandlerType handler)
        {
            return handler switch
            {
                FileSystemHandlerType.Unknown => throw new AmbiguousFileSystemHandlerException(),
                FileSystemHandlerType.Path => throw new NotSupportedException(),
                FileSystemHandlerType.Link => FileSystem.Link.GetCreationTimeUtc(path),
                FileSystemHandlerType.File => FileSystem.File.GetCreationTimeUtc(path),
                FileSystemHandlerType.Directory => FileSystem.Directory.GetCreationTimeUtc(path),
                FileSystemHandlerType.Drive => throw new NotSupportedException(),
                _ => throw new EnumUndefinedOrNotSupportedException<FileSystemHandlerType>(handler, nameof(handler), null)
            };
        }

        protected override DateTime GetLastAccessTime(String path, FileSystemHandlerType handler)
        {
            return handler switch
            {
                FileSystemHandlerType.Unknown => throw new AmbiguousFileSystemHandlerException(),
                FileSystemHandlerType.Path => throw new NotSupportedException(),
                FileSystemHandlerType.Link => FileSystem.Link.GetLastAccessTime(path),
                FileSystemHandlerType.File => FileSystem.File.GetLastAccessTime(path),
                FileSystemHandlerType.Directory => FileSystem.Directory.GetLastAccessTime(path),
                FileSystemHandlerType.Drive => throw new NotSupportedException(),
                _ => throw new EnumUndefinedOrNotSupportedException<FileSystemHandlerType>(handler, nameof(handler), null)
            };
        }

        protected override DateTime GetLastAccessTimeUtc(String path, FileSystemHandlerType handler)
        {
            return handler switch
            {
                FileSystemHandlerType.Unknown => throw new AmbiguousFileSystemHandlerException(),
                FileSystemHandlerType.Path => throw new NotSupportedException(),
                FileSystemHandlerType.Link => FileSystem.Link.GetLastAccessTimeUtc(path),
                FileSystemHandlerType.File => FileSystem.File.GetLastAccessTimeUtc(path),
                FileSystemHandlerType.Directory => FileSystem.Directory.GetLastAccessTimeUtc(path),
                FileSystemHandlerType.Drive => throw new NotSupportedException(),
                _ => throw new EnumUndefinedOrNotSupportedException<FileSystemHandlerType>(handler, nameof(handler), null)
            };
        }

        protected override DateTime GetLastWriteTime(String path, FileSystemHandlerType handler)
        {
            return handler switch
            {
                FileSystemHandlerType.Unknown => throw new AmbiguousFileSystemHandlerException(),
                FileSystemHandlerType.Path => throw new NotSupportedException(),
                FileSystemHandlerType.Link => FileSystem.Link.GetLastWriteTime(path),
                FileSystemHandlerType.File => FileSystem.File.GetLastWriteTime(path),
                FileSystemHandlerType.Directory => FileSystem.Directory.GetLastWriteTime(path),
                FileSystemHandlerType.Drive => throw new NotSupportedException(),
                _ => throw new EnumUndefinedOrNotSupportedException<FileSystemHandlerType>(handler, nameof(handler), null)
            };
        }

        protected override DateTime GetLastWriteTimeUtc(String path, FileSystemHandlerType handler)
        {
            return handler switch
            {
                FileSystemHandlerType.Unknown => throw new AmbiguousFileSystemHandlerException(),
                FileSystemHandlerType.Path => throw new NotSupportedException(),
                FileSystemHandlerType.Link => FileSystem.Link.GetLastWriteTimeUtc(path),
                FileSystemHandlerType.File => FileSystem.File.GetLastWriteTimeUtc(path),
                FileSystemHandlerType.Directory => FileSystem.Directory.GetLastWriteTimeUtc(path),
                FileSystemHandlerType.Drive => throw new NotSupportedException(),
                _ => throw new EnumUndefinedOrNotSupportedException<FileSystemHandlerType>(handler, nameof(handler), null)
            };
        }

        protected override void SetCreationTime(String path, DateTime time, FileSystemHandlerType handler)
        {
            switch (handler)
            {
                case FileSystemHandlerType.Unknown:
                    throw new AmbiguousFileSystemHandlerException();
                case FileSystemHandlerType.Path:
                    throw new NotSupportedException();
                case FileSystemHandlerType.Link:
                    FileSystem.Link.SetCreationTime(path, time);
                    return;
                case FileSystemHandlerType.File:
                    FileSystem.File.SetCreationTime(path, time);
                    return;
                case FileSystemHandlerType.Directory:
                    FileSystem.Directory.SetCreationTime(path, time);
                    return;
                case FileSystemHandlerType.Drive:
                    throw new NotSupportedException();
                default:
                    throw new EnumUndefinedOrNotSupportedException<FileSystemHandlerType>(handler, nameof(handler), null);
            }
        }

        protected override void SetCreationTimeUtc(String path, DateTime time, FileSystemHandlerType handler)
        {
            switch (handler)
            {
                case FileSystemHandlerType.Unknown:
                    throw new AmbiguousFileSystemHandlerException();
                case FileSystemHandlerType.Path:
                    throw new NotSupportedException();
                case FileSystemHandlerType.Link:
                    FileSystem.Link.SetCreationTimeUtc(path, time);
                    return;
                case FileSystemHandlerType.File:
                    FileSystem.File.SetCreationTimeUtc(path, time);
                    return;
                case FileSystemHandlerType.Directory:
                    FileSystem.Directory.SetCreationTimeUtc(path, time);
                    return;
                case FileSystemHandlerType.Drive:
                    throw new NotSupportedException();
                default:
                    throw new EnumUndefinedOrNotSupportedException<FileSystemHandlerType>(handler, nameof(handler), null);
            }
        }

        protected override void SetLastAccessTime(String path, DateTime time, FileSystemHandlerType handler)
        {
            switch (handler)
            {
                case FileSystemHandlerType.Unknown:
                    throw new AmbiguousFileSystemHandlerException();
                case FileSystemHandlerType.Path:
                    throw new NotSupportedException();
                case FileSystemHandlerType.Link:
                    FileSystem.Link.SetLastAccessTime(path, time);
                    return;
                case FileSystemHandlerType.File:
                    FileSystem.File.SetLastAccessTime(path, time);
                    return;
                case FileSystemHandlerType.Directory:
                    FileSystem.Directory.SetLastAccessTime(path, time);
                    return;
                case FileSystemHandlerType.Drive:
                    throw new NotSupportedException();
                default:
                    throw new EnumUndefinedOrNotSupportedException<FileSystemHandlerType>(handler, nameof(handler), null);
            }
        }

        protected override void SetLastAccessTimeUtc(String path, DateTime time, FileSystemHandlerType handler)
        {
            switch (handler)
            {
                case FileSystemHandlerType.Unknown:
                    throw new AmbiguousFileSystemHandlerException();
                case FileSystemHandlerType.Path:
                    throw new NotSupportedException();
                case FileSystemHandlerType.Link:
                    FileSystem.Link.SetLastAccessTimeUtc(path, time);
                    return;
                case FileSystemHandlerType.File:
                    FileSystem.File.SetLastAccessTimeUtc(path, time);
                    return;
                case FileSystemHandlerType.Directory:
                    FileSystem.Directory.SetLastAccessTimeUtc(path, time);
                    return;
                case FileSystemHandlerType.Drive:
                    throw new NotSupportedException();
                default:
                    throw new EnumUndefinedOrNotSupportedException<FileSystemHandlerType>(handler, nameof(handler), null);
            }
        }

        protected override void SetLastWriteTime(String path, DateTime time, FileSystemHandlerType handler)
        {
            switch (handler)
            {
                case FileSystemHandlerType.Unknown:
                    throw new AmbiguousFileSystemHandlerException();
                case FileSystemHandlerType.Path:
                    throw new NotSupportedException();
                case FileSystemHandlerType.Link:
                    FileSystem.Link.SetLastWriteTime(path, time);
                    return;
                case FileSystemHandlerType.File:
                    FileSystem.File.SetLastWriteTime(path, time);
                    return;
                case FileSystemHandlerType.Directory:
                    FileSystem.Directory.SetLastWriteTime(path, time);
                    return;
                case FileSystemHandlerType.Drive:
                    throw new NotSupportedException();
                default:
                    throw new EnumUndefinedOrNotSupportedException<FileSystemHandlerType>(handler, nameof(handler), null);
            }
        }

        protected override void SetLastWriteTimeUtc(String path, DateTime time, FileSystemHandlerType handler)
        {
            switch (handler)
            {
                case FileSystemHandlerType.Unknown:
                    throw new AmbiguousFileSystemHandlerException();
                case FileSystemHandlerType.Path:
                    throw new NotSupportedException();
                case FileSystemHandlerType.Link:
                    FileSystem.Link.SetLastWriteTimeUtc(path, time);
                    return;
                case FileSystemHandlerType.File:
                    FileSystem.File.SetLastWriteTimeUtc(path, time);
                    return;
                case FileSystemHandlerType.Directory:
                    FileSystem.Directory.SetLastWriteTimeUtc(path, time);
                    return;
                case FileSystemHandlerType.Drive:
                    throw new NotSupportedException();
                default:
                    throw new EnumUndefinedOrNotSupportedException<FileSystemHandlerType>(handler, nameof(handler), null);
            }
        }

        protected override FileStream Create(String path, FileSystemHandlerType handler)
        {
            return handler switch
            {
                FileSystemHandlerType.Unknown => throw new AmbiguousFileSystemHandlerException(),
                FileSystemHandlerType.Path => throw new NotSupportedException(),
                FileSystemHandlerType.Link => throw new NotSupportedException(),
                FileSystemHandlerType.File => FileSystem.File.Create(path),
                FileSystemHandlerType.Directory => throw new NotSupportedException(),
                FileSystemHandlerType.Drive => throw new NotSupportedException(),
                _ => throw new EnumUndefinedOrNotSupportedException<FileSystemHandlerType>(handler, nameof(handler), null)
            };
        }

        protected override Boolean Encrypt(String path, FileSystemHandlerType handler)
        {
            return handler switch
            {
                FileSystemHandlerType.Unknown => throw new AmbiguousFileSystemHandlerException(),
                FileSystemHandlerType.Path => throw new NotSupportedException(),
                FileSystemHandlerType.Link => throw new NotSupportedException(),
                FileSystemHandlerType.File => FileSystem.File.Encrypt(path),
                FileSystemHandlerType.Directory => throw new NotSupportedException(),
                FileSystemHandlerType.Drive => throw new NotSupportedException(),
                _ => throw new EnumUndefinedOrNotSupportedException<FileSystemHandlerType>(handler, nameof(handler), null)
            };
        }

        protected override Boolean Decrypt(String path, FileSystemHandlerType handler)
        {
            return handler switch
            {
                FileSystemHandlerType.Unknown => throw new AmbiguousFileSystemHandlerException(),
                FileSystemHandlerType.Path => throw new NotSupportedException(),
                FileSystemHandlerType.Link => throw new NotSupportedException(),
                FileSystemHandlerType.File => FileSystem.File.Decrypt(path),
                FileSystemHandlerType.Directory => throw new NotSupportedException(),
                FileSystemHandlerType.Drive => throw new NotSupportedException(),
                _ => throw new EnumUndefinedOrNotSupportedException<FileSystemHandlerType>(handler, nameof(handler), null)
            };
        }

        protected override void Move(String source, String destination, FileSystemHandlerType handler)
        {
            switch (handler)
            {
                case FileSystemHandlerType.Unknown:
                    throw new AmbiguousFileSystemHandlerException();
                case FileSystemHandlerType.Path:
                    throw new NotSupportedException();
                case FileSystemHandlerType.Link:
                    FileSystem.Link.Move(source, destination);
                    return;
                case FileSystemHandlerType.File:
                    FileSystem.File.Move(source, destination);
                    return;
                case FileSystemHandlerType.Directory:
                    FileSystem.Directory.Move(source, destination);
                    return;
                case FileSystemHandlerType.Drive:
                    throw new NotSupportedException();
                default:
                    throw new EnumUndefinedOrNotSupportedException<FileSystemHandlerType>(handler, nameof(handler), null);
            }
        }

        protected override void Move(String source, String destination, Boolean overwrite, FileSystemHandlerType handler)
        {
            switch (handler)
            {
                case FileSystemHandlerType.Unknown:
                    throw new AmbiguousFileSystemHandlerException();
                case FileSystemHandlerType.Path:
                    throw new NotSupportedException();
                case FileSystemHandlerType.Link:
                    FileSystem.Link.Move(source, destination, overwrite);
                    return;
                case FileSystemHandlerType.File:
                    FileSystem.File.Move(source, destination, overwrite);
                    return;
                case FileSystemHandlerType.Directory:
                case FileSystemHandlerType.Drive:
                    throw new NotSupportedException();
                default:
                    throw new EnumUndefinedOrNotSupportedException<FileSystemHandlerType>(handler, nameof(handler), null);
            }
        }

        protected override void Copy(String source, String destination, FileSystemHandlerType handler)
        {
            switch (handler)
            {
                case FileSystemHandlerType.Unknown:
                    throw new AmbiguousFileSystemHandlerException();
                case FileSystemHandlerType.Path:
                    throw new NotSupportedException();
                case FileSystemHandlerType.Link:
                    FileSystem.Link.Copy(source, destination);
                    return;
                case FileSystemHandlerType.File:
                    FileSystem.File.Copy(source, destination);
                    return;
                case FileSystemHandlerType.Directory:
                case FileSystemHandlerType.Drive:
                    throw new NotSupportedException();
                default:
                    throw new EnumUndefinedOrNotSupportedException<FileSystemHandlerType>(handler, nameof(handler), null);
            }
        }

        protected override void Copy(String source, String destination, Boolean overwrite, FileSystemHandlerType handler)
        {
            switch (handler)
            {
                case FileSystemHandlerType.Unknown:
                    throw new AmbiguousFileSystemHandlerException();
                case FileSystemHandlerType.Path:
                    throw new NotSupportedException();
                case FileSystemHandlerType.Link:
                    FileSystem.Link.Copy(source, destination, overwrite);
                    return;
                case FileSystemHandlerType.File:
                    FileSystem.File.Copy(source, destination, overwrite);
                    return;
                case FileSystemHandlerType.Directory:
                case FileSystemHandlerType.Drive:
                    throw new NotSupportedException();
                default:
                    throw new EnumUndefinedOrNotSupportedException<FileSystemHandlerType>(handler, nameof(handler), null);
            }
        }

        protected override void Replace(String source, String destination, String? backup, FileSystemHandlerType handler)
        {
            switch (handler)
            {
                case FileSystemHandlerType.Unknown:
                    throw new AmbiguousFileSystemHandlerException();
                case FileSystemHandlerType.Path:
                    throw new NotSupportedException();
                case FileSystemHandlerType.Link:
                    FileSystem.Link.Replace(source, destination, backup);
                    return;
                case FileSystemHandlerType.File:
                    FileSystem.File.Replace(source, destination, backup);
                    return;
                case FileSystemHandlerType.Directory:
                case FileSystemHandlerType.Drive:
                    throw new NotSupportedException();
                default:
                    throw new EnumUndefinedOrNotSupportedException<FileSystemHandlerType>(handler, nameof(handler), null);
            }
        }

        protected override void Replace(String source, String destination, String? backup, Boolean suppress, FileSystemHandlerType handler)
        {
            switch (handler)
            {
                case FileSystemHandlerType.Unknown:
                    throw new AmbiguousFileSystemHandlerException();
                case FileSystemHandlerType.Path:
                    throw new NotSupportedException();
                case FileSystemHandlerType.Link:
                    FileSystem.Link.Replace(source, destination, backup, suppress);
                    return;
                case FileSystemHandlerType.File:
                    FileSystem.File.Replace(source, destination, backup, suppress);
                    return;
                case FileSystemHandlerType.Directory:
                case FileSystemHandlerType.Drive:
                    throw new NotSupportedException();
                default:
                    throw new EnumUndefinedOrNotSupportedException<FileSystemHandlerType>(handler, nameof(handler), null);
            }
        }

        protected override void Delete(String path, FileSystemHandlerType handler)
        {
            switch (handler)
            {
                case FileSystemHandlerType.Unknown:
                    throw new AmbiguousFileSystemHandlerException();
                case FileSystemHandlerType.Path:
                    throw new NotSupportedException();
                case FileSystemHandlerType.Link:
                    FileSystem.Link.Delete(path);
                    return;
                case FileSystemHandlerType.File:
                    FileSystem.File.Delete(path);
                    return;
                case FileSystemHandlerType.Directory:
                    FileSystem.Directory.Delete(path);
                    return;
                case FileSystemHandlerType.Drive:
                    throw new NotSupportedException();
                default:
                    throw new EnumUndefinedOrNotSupportedException<FileSystemHandlerType>(handler, nameof(handler), null);
            }
        }

        protected override void Delete(String path, Boolean recursive, FileSystemHandlerType handler)
        {
            switch (handler)
            {
                case FileSystemHandlerType.Unknown:
                    throw new AmbiguousFileSystemHandlerException();
                case FileSystemHandlerType.Path:
                case FileSystemHandlerType.File:
                    throw new NotSupportedException();
                case FileSystemHandlerType.Directory:
                    FileSystem.Directory.Delete(path, recursive);
                    return;
                case FileSystemHandlerType.Drive:
                    throw new NotSupportedException();
                default:
                    throw new EnumUndefinedOrNotSupportedException<FileSystemHandlerType>(handler, nameof(handler), null);
            }
        }
    }
}