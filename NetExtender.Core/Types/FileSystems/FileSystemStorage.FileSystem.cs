// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.Serialization;
using NetExtender.FileSystems.Interfaces;

namespace NetExtender.FileSystems
{
    public abstract partial class FileSystemStorage<TLink, TFile, TDirectory, TDrive>
    {
        protected abstract override IFileSystemInfo CreateSymbolicLink(String path, String target, FileSystemHandlerType handler);
        protected abstract override IFileSystemInfo? ResolveLinkTarget(String path, Boolean target, FileSystemHandlerType handler);
        protected abstract override Boolean Exists([NotNullWhen(true)] String? path, FileSystemHandlerType handler);
        protected abstract override IDirectoryInfo? GetParent(String path, FileSystemHandlerType handler);
        protected abstract override FileAttributes GetAttributes(String path, FileSystemHandlerType handler);
        protected abstract override void SetAttributes(String path, FileAttributes attributes, FileSystemHandlerType handler);
        protected abstract override DateTime GetCreationTime(String path, FileSystemHandlerType handler);
        protected abstract override DateTime GetCreationTimeUtc(String path, FileSystemHandlerType handler);
        protected abstract override DateTime GetLastAccessTime(String path, FileSystemHandlerType handler);
        protected abstract override DateTime GetLastAccessTimeUtc(String path, FileSystemHandlerType handler);
        protected abstract override DateTime GetLastWriteTime(String path, FileSystemHandlerType handler);
        protected abstract override DateTime GetLastWriteTimeUtc(String path, FileSystemHandlerType handler);
        protected abstract override void SetCreationTime(String path, DateTime time, FileSystemHandlerType handler);
        protected abstract override void SetCreationTimeUtc(String path, DateTime time, FileSystemHandlerType handler);
        protected abstract override void SetLastAccessTime(String path, DateTime time, FileSystemHandlerType handler);
        protected abstract override void SetLastAccessTimeUtc(String path, DateTime time, FileSystemHandlerType handler);
        protected abstract override void SetLastWriteTime(String path, DateTime time, FileSystemHandlerType handler);
        protected abstract override void SetLastWriteTimeUtc(String path, DateTime time, FileSystemHandlerType handler);
        protected abstract override FileStream Create(String path, FileSystemHandlerType handler);
        protected abstract override Boolean Encrypt(String path, FileSystemHandlerType handler);
        protected abstract override Boolean Decrypt(String path, FileSystemHandlerType handler);

        protected override void Move(String source, String destination, FileSystemHandlerType handler)
        {
            Move(source, destination, false, handler);
        }

        protected abstract override void Move(String source, String destination, Boolean overwrite, FileSystemHandlerType handler);

        protected override void Copy(String source, String destination, FileSystemHandlerType handler)
        {
            Copy(source, destination, false, handler);
        }

        protected abstract override void Copy(String source, String destination, Boolean overwrite, FileSystemHandlerType handler);

        protected override void Replace(String source, String destination, String? backup, FileSystemHandlerType handler)
        {
            Replace(source, destination, backup, false, handler);
        }

        protected abstract override void Replace(String source, String destination, String? backup, Boolean suppress, FileSystemHandlerType handler);

        protected override void Delete(String path, FileSystemHandlerType handler)
        {
            Delete(path, false, handler);
        }

        protected abstract override void Delete(String path, Boolean recursive, FileSystemHandlerType handler);

#region Storage
        protected virtual FileSystemInfo? Info(INode node)
        {
            return (node = Verify(node)) switch
            {
                null => throw new ArgumentNullException(nameof(node)),
                TLink link => Info(link),
                TFile file => Info(file),
                TDirectory directory => Info(directory),
                _ => throw NotSupported(node, nameof(Info))
            };
        }
        
        FileSystemInfo? IFileSystemStorage.Info(IFileSystemEntry entry)
        {
            return Info(Verify<INode, IFileSystemEntry>(entry));
        }

        protected virtual void GetObjectData(INode node, SerializationInfo info, StreamingContext context)
        {
            switch (node = Verify(node))
            {
                case null:
                    throw new ArgumentNullException(nameof(node));
                case TLink link:
                    GetObjectData(link, info, context);
                    return;
                case TFile file:
                    GetObjectData(file, info, context);
                    return;
                case TDirectory directory:
                    GetObjectData(directory, info, context);
                    return;
                case TDrive drive:
                    GetObjectData(drive, info, context);
                    return;
                default:
                    throw NotSupported(node, nameof(GetObjectData));
            }
        }
        
        void IFileSystemStorage.GetObjectData(IFileSystemEntry entry, SerializationInfo info, StreamingContext context)
        {
            GetObjectData(Verify<INode, IFileSystemEntry>(entry), info, context);
        }

        protected new virtual String Name(INode node)
        {
            return (node = Verify(node)) switch
            {
                null => throw new ArgumentNullException(nameof(node)),
                TLink link => Name(link),
                TFile file => Name(file),
                TDirectory directory => Name(directory),
                TDrive drive => Name(drive),
                _ => throw NotSupported(node, nameof(Name))
            };
        }

        String IFileSystemStorage.Name(IFileSystemEntry entry)
        {
            return Name(Verify<INode, IFileSystemEntry>(entry));
        }

        protected virtual String FullName(INode node)
        {
            return (node = Verify(node)) switch
            {
                null => throw new ArgumentNullException(nameof(node)),
                TLink link => FullName(link),
                TFile file => FullName(file),
                TDirectory directory => FullName(directory),
                TDrive drive => FullName(drive),
                _ => throw NotSupported(node, nameof(FullName))
            };
        }

        String IFileSystemStorage.FullName(IFileSystemEntry entry)
        {
            return FullName(Verify<INode, IFileSystemEntry>(entry));
        }

        protected virtual String Extension(INode node)
        {
            return (node = Verify(node)) switch
            {
                null => throw new ArgumentNullException(nameof(node)),
                TLink link => Extension(link),
                TFile file => Extension(file),
                TDirectory directory => Extension(directory),
                _ => throw NotSupported(node, nameof(Extension))
            };
        }

        String IFileSystemStorage.Extension(IFileSystemEntry entry)
        {
            return Extension(Verify<INode, IFileSystemEntry>(entry));
        }

        protected new virtual Boolean IsReal(INode node)
        {
            return (node = Verify(node)) switch
            {
                null => throw new ArgumentNullException(nameof(node)),
                TLink link => IsReal(link),
                TFile file => IsReal(file),
                TDirectory directory => IsReal(directory),
                TDrive drive => IsReal(drive),
                _ => throw NotSupported(node, nameof(IsReal))
            };
        }

        Boolean IFileSystemStorage.IsReal(IFileSystemEntry entry)
        {
            return IsReal(Verify<INode, IFileSystemEntry>(entry));
        }

        Boolean IFileSystemStorage.IsReal(IFileSystemStorageEntry entry)
        {
            return IsReal(Verify<INode, IFileSystemStorageEntry>(entry));
        }

        protected virtual Boolean Exists(INode node)
        {
            return (node = Verify(node)) switch
            {
                null => throw new ArgumentNullException(nameof(node)),
                TLink link => Exists(link),
                TFile file => Exists(file),
                TDirectory directory => Exists(directory),
                _ => throw NotSupported(node, nameof(Exists))
            };
        }

        Boolean IFileSystemStorage.Exists(IFileSystemEntry entry)
        {
            return Exists(Verify<INode, IFileSystemEntry>(entry));
        }

        protected virtual FileAttributes Attributes(INode node)
        {
            return (node = Verify(node)) switch
            {
                null => throw new ArgumentNullException(nameof(node)),
                TLink link => Attributes(link),
                TFile file => Attributes(file),
                TDirectory directory => Attributes(directory),
                _ => throw NotSupported(node, nameof(Attributes))
            };
        }

        FileAttributes IFileSystemStorage.Attributes(IFileSystemEntry entry)
        {
            return Attributes(Verify<INode, IFileSystemEntry>(entry));
        }

        protected virtual void Attributes(INode node, FileAttributes value)
        {
            switch (node = Verify(node))
            {
                case null:
                    throw new ArgumentNullException(nameof(node));
                case TLink link:
                    Attributes(link, value);
                    return;
                case TFile file:
                    Attributes(file, value);
                    return;
                case TDirectory directory:
                    Attributes(directory, value);
                    return;
                default:
                    throw NotSupported(node, nameof(Attributes));
            }
        }

        void IFileSystemStorage.Attributes(IFileSystemEntry entry, FileAttributes value)
        {
            Attributes(Verify<INode, IFileSystemEntry>(entry), value);
        }

        protected virtual String? LinkTarget(INode node)
        {
            return (node = Verify(node)) switch
            {
                null => throw new ArgumentNullException(nameof(node)),
                TLink link => LinkTarget(link),
                TFile file => LinkTarget(file),
                TDirectory directory => LinkTarget(directory),
                _ => throw NotSupported(node, nameof(LinkTarget))
            };
        }

        String? IFileSystemStorage.LinkTarget(IFileSystemEntry entry)
        {
            return LinkTarget(Verify<INode, IFileSystemEntry>(entry));
        }

        protected new virtual DateTime CreationTime(INode node)
        {
            return (node = Verify(node)) switch
            {
                null => throw new ArgumentNullException(nameof(node)),
                TLink link => CreationTime(link),
                TFile file => CreationTime(file),
                TDirectory directory => CreationTime(directory),
                _ => throw NotSupported(node, nameof(CreationTime))
            };
        }

        DateTime IFileSystemStorage.CreationTime(IFileSystemEntry entry)
        {
            return CreationTime(Verify<INode, IFileSystemEntry>(entry));
        }

        protected new virtual void CreationTime(INode node, DateTime value)
        {
            switch (node = Verify(node))
            {
                case null:
                    throw new ArgumentNullException(nameof(node));
                case TLink link:
                    CreationTime(link, value);
                    return;
                case TFile file:
                    CreationTime(file, value);
                    return;
                case TDirectory directory:
                    CreationTime(directory, value);
                    return;
                default:
                    throw NotSupported(node, nameof(CreationTime));
            }
        }

        void IFileSystemStorage.CreationTime(IFileSystemEntry entry, DateTime value)
        {
            CreationTime(Verify<INode, IFileSystemEntry>(entry), value);
        }

        protected new virtual DateTime CreationTimeUtc(INode node)
        {
            return (node = Verify(node)) switch
            {
                null => throw new ArgumentNullException(nameof(node)),
                TLink link => CreationTimeUtc(link),
                TFile file => CreationTimeUtc(file),
                TDirectory directory => CreationTimeUtc(directory),
                _ => throw NotSupported(node, nameof(CreationTimeUtc))
            };
        }

        DateTime IFileSystemStorage.CreationTimeUtc(IFileSystemEntry entry)
        {
            return CreationTimeUtc(Verify<INode, IFileSystemEntry>(entry));
        }

        protected new virtual void CreationTimeUtc(INode node, DateTime value)
        {
            switch (node = Verify(node))
            {
                case null:
                    throw new ArgumentNullException(nameof(node));
                case TLink link:
                    CreationTimeUtc(link, value);
                    return;
                case TFile file:
                    CreationTimeUtc(file, value);
                    return;
                case TDirectory directory:
                    CreationTimeUtc(directory, value);
                    return;
                default:
                    throw NotSupported(node, nameof(CreationTimeUtc));
            }
        }

        void IFileSystemStorage.CreationTimeUtc(IFileSystemEntry entry, DateTime value)
        {
            CreationTimeUtc(Verify<INode, IFileSystemEntry>(entry), value);
        }

        protected virtual DateTime LastAccessTime(INode node)
        {
            return (node = Verify(node)) switch
            {
                null => throw new ArgumentNullException(nameof(node)),
                TLink link => LastAccessTime(link),
                TFile file => LastAccessTime(file),
                TDirectory directory => LastAccessTime(directory),
                _ => throw NotSupported(node, nameof(LastAccessTime))
            };
        }

        DateTime IFileSystemStorage.LastAccessTime(IFileSystemEntry entry)
        {
            return LastAccessTime(Verify<INode, IFileSystemEntry>(entry));
        }

        protected virtual void LastAccessTime(INode node, DateTime value)
        {
            switch (node = Verify(node))
            {
                case null:
                    throw new ArgumentNullException(nameof(node));
                case TLink link:
                    LastAccessTime(link, value);
                    return;
                case TFile file:
                    LastAccessTime(file, value);
                    return;
                case TDirectory directory:
                    LastAccessTime(directory, value);
                    return;
                default:
                    throw NotSupported(node, nameof(LastAccessTime));
            }
        }

        void IFileSystemStorage.LastAccessTime(IFileSystemEntry entry, DateTime value)
        {
            LastAccessTime(Verify<INode, IFileSystemEntry>(entry), value);
        }

        protected virtual DateTime LastAccessTimeUtc(INode node)
        {
            return (node = Verify(node)) switch
            {
                null => throw new ArgumentNullException(nameof(node)),
                TLink link => LastAccessTimeUtc(link),
                TFile file => LastAccessTimeUtc(file),
                TDirectory directory => LastAccessTimeUtc(directory),
                _ => throw NotSupported(node, nameof(LastAccessTimeUtc))
            };
        }

        DateTime IFileSystemStorage.LastAccessTimeUtc(IFileSystemEntry entry)
        {
            return LastAccessTimeUtc(Verify<INode, IFileSystemEntry>(entry));
        }

        protected virtual void LastAccessTimeUtc(INode node, DateTime value)
        {
            switch (node = Verify(node))
            {
                case null:
                    throw new ArgumentNullException(nameof(node));
                case TLink link:
                    LastAccessTimeUtc(link, value);
                    return;
                case TFile file:
                    LastAccessTimeUtc(file, value);
                    return;
                case TDirectory directory:
                    LastAccessTimeUtc(directory, value);
                    return;
                default:
                    throw NotSupported(node, nameof(LastAccessTimeUtc));
            }
        }

        void IFileSystemStorage.LastAccessTimeUtc(IFileSystemEntry entry, DateTime value)
        {
            LastAccessTimeUtc(Verify<INode, IFileSystemEntry>(entry), value);
        }

        protected virtual DateTime LastWriteTime(INode node)
        {
            return (node = Verify(node)) switch
            {
                null => throw new ArgumentNullException(nameof(node)),
                TLink link => LastWriteTime(link),
                TFile file => LastWriteTime(file),
                TDirectory directory => LastWriteTime(directory),
                _ => throw NotSupported(node, nameof(LastWriteTime))
            };
        }

        DateTime IFileSystemStorage.LastWriteTime(IFileSystemEntry entry)
        {
            return LastWriteTime(Verify<INode, IFileSystemEntry>(entry));
        }

        protected virtual void LastWriteTime(INode node, DateTime value)
        {
            switch (node = Verify(node))
            {
                case null:
                    throw new ArgumentNullException(nameof(node));
                case TLink link:
                    LastWriteTime(link, value);
                    return;
                case TFile file:
                    LastWriteTime(file, value);
                    return;
                case TDirectory directory:
                    LastWriteTime(directory, value);
                    return;
                default:
                    throw NotSupported(node, nameof(LastWriteTime));
            }
        }

        void IFileSystemStorage.LastWriteTime(IFileSystemEntry entry, DateTime value)
        {
            LastWriteTime(Verify<INode, IFileSystemEntry>(entry), value);
        }

        protected virtual DateTime LastWriteTimeUtc(INode node)
        {
            return (node = Verify(node)) switch
            {
                null => throw new ArgumentNullException(nameof(node)),
                TLink link => LastWriteTimeUtc(link),
                TFile file => LastWriteTimeUtc(file),
                TDirectory directory => LastWriteTimeUtc(directory),
                _ => throw NotSupported(node, nameof(LastWriteTimeUtc))
            };
        }

        DateTime IFileSystemStorage.LastWriteTimeUtc(IFileSystemEntry entry)
        {
            return LastWriteTimeUtc(Verify<INode, IFileSystemEntry>(entry));
        }

        protected virtual void LastWriteTimeUtc(INode node, DateTime value)
        {
            switch (node = Verify(node))
            {
                case null:
                    throw new ArgumentNullException(nameof(node));
                case TLink link:
                    LastWriteTimeUtc(link, value);
                    return;
                case TFile file:
                    LastWriteTimeUtc(file, value);
                    return;
                case TDirectory directory:
                    LastWriteTimeUtc(directory, value);
                    return;
                default:
                    throw NotSupported(node, nameof(LastWriteTimeUtc));
            }
        }

        void IFileSystemStorage.LastWriteTimeUtc(IFileSystemEntry entry, DateTime value)
        {
            LastWriteTimeUtc(Verify<INode, IFileSystemEntry>(entry), value);
        }

        protected new virtual TDrive? Drive(INode node)
        {
            return (node = Verify(node)) switch
            {
                null => throw new ArgumentNullException(nameof(node)),
                TLink link => Drive(link),
                TFile file => Drive(file),
                TDirectory directory => Drive(directory),
                TDrive drive => drive,
                _ => throw NotSupported(node, nameof(Drive))
            };
        }

        IDriveEntry? IFileSystemStorage.Drive(IFileSystemEntry entry)
        {
            return Drive(Verify<INode, IFileSystemEntry>(entry));
        }

        protected virtual Boolean CreateAsSymbolicLink(INode node, String target)
        {
            return (node = Verify(node)) switch
            {
                null => throw new ArgumentNullException(nameof(node)),
                TLink link => CreateAsSymbolicLink(link, target),
                TFile file => CreateAsSymbolicLink(file, target),
                TDirectory directory => CreateAsSymbolicLink(directory, target),
                _ => throw NotSupported(node, nameof(CreateAsSymbolicLink))
            };
        }

        Boolean IFileSystemStorage.CreateAsSymbolicLink(IFileSystemEntry entry, String target)
        {
            return CreateAsSymbolicLink(Verify<INode, IFileSystemEntry>(entry), target);
        }

        protected virtual IFileSystemNode? ResolveLinkTarget(INode node)
        {
            return (node = Verify(node)) switch
            {
                null => throw new ArgumentNullException(nameof(node)),
                TLink link => ResolveLinkTarget(link),
                TFile file => ResolveLinkTarget(file),
                TDirectory directory => ResolveLinkTarget(directory),
                _ => throw NotSupported(node, nameof(ResolveLinkTarget))
            };
        }

        IFileSystemEntry? IFileSystemStorage.ResolveLinkTarget(IFileSystemEntry entry)
        {
            return ResolveLinkTarget(Verify<INode, IFileSystemEntry>(entry))?.This;
        }

        protected virtual IFileSystemNode? ResolveLinkTarget(INode node, Boolean final)
        {
            return (node = Verify(node)) switch
            {
                null => throw new ArgumentNullException(nameof(node)),
                TLink link => ResolveLinkTarget(link, final),
                TFile file => ResolveLinkTarget(file, final),
                TDirectory directory => ResolveLinkTarget(directory, final),
                _ => throw NotSupported(node, nameof(ResolveLinkTarget))
            };
        }

        IFileSystemEntry? IFileSystemStorage.ResolveLinkTarget(IFileSystemEntry entry, Boolean final)
        {
            return ResolveLinkTarget(Verify<INode, IFileSystemEntry>(entry), final)?.This;
        }

        protected virtual void Delete(INode node)
        {
            switch (node = Verify(node))
            {
                case null:
                    throw new ArgumentNullException(nameof(node));
                case TLink link:
                    Delete(link);
                    return;
                case TFile file:
                    Delete(file);
                    return;
                case TDirectory directory:
                    Delete(directory);
                    return;
                default:
                    throw NotSupported(node, nameof(Delete));
            }
        }

        void IFileSystemStorage.Delete(IFileSystemEntry entry)
        {
            Delete(Verify<INode, IFileSystemEntry>(entry));
        }

        protected virtual void Refresh(INode node)
        {
            switch (node = Verify(node))
            {
                case null:
                    throw new ArgumentNullException(nameof(node));
                case TLink link:
                    Refresh(link);
                    return;
                case TFile file:
                    Refresh(file);
                    return;
                case TDirectory directory:
                    Refresh(directory);
                    return;
                default:
                    throw NotSupported(node, nameof(Refresh));
            }
        }

        void IFileSystemStorage.Refresh(IFileSystemEntry entry)
        {
            Refresh(Verify<INode, IFileSystemEntry>(entry));
        }

        protected virtual Int32 GetHashCode(INode? node)
        {
            return (node = Verify(node)) switch
            {
                null => 0,
                TLink link => GetHashCode(link),
                TFile file => GetHashCode(file),
                TDirectory directory => GetHashCode(directory),
                TDrive drive => GetHashCode(drive),
                _ => throw NotSupported(node, nameof(GetHashCode))
            };
        }

        Int32 IFileSystemStorage.GetHashCode(IFileSystemEntry? entry)
        {
            return GetHashCode(Verify<INode, IFileSystemEntry>(entry));
        }

        protected virtual Boolean Equals(INode? node, FileSystemInfo? other)
        {
            return (node = Verify(node)) switch
            {
                null => other is null,
                TLink link => Equals(link, other),
                TFile file => Equals(file, other as FileInfo),
                TDirectory directory => Equals(directory, other as DirectoryInfo),
                TDrive => false,
                _ => throw NotSupported(node, nameof(GetHashCode))
            };
        }

        Boolean IFileSystemStorage.Equals(IFileSystemEntry? entry, FileSystemInfo? other)
        {
            return Equals(Verify<INode, IFileSystemEntry>(entry), other);
        }

        protected virtual Boolean Equals(INode? node, IFileSystemInfo? other)
        {
            return (node = Verify(node)) switch
            {
                null => other is null,
                TLink link => Equals(link, other as ILinkInfo),
                TFile file => Equals(file, other as IFileInfo),
                TDirectory directory => Equals(directory, other as IDirectoryInfo),
                TDrive => false,
                _ => throw NotSupported(node, nameof(GetHashCode))
            };
        }

        Boolean IFileSystemStorage.Equals(IFileSystemEntry? entry, IFileSystemInfo? other)
        {
            return Equals(Verify<INode, IFileSystemEntry>(entry), other);
        }

        [return: NotNullIfNotNull("node")]
        protected virtual String? ToString(INode? node)
        {
            return (node = Verify(node)) switch
            {
                null => null,
                TLink link => ToString(link),
                TFile file => ToString(file),
                TDirectory directory => ToString(directory),
                TDrive drive => ToString(drive),
                _ => throw NotSupported(node, nameof(ToString))
            };
        }

        [return: NotNullIfNotNull("entry")]
        String? IFileSystemStorage.ToString(IFileSystemEntry? entry)
        {
            return ToString(Verify<INode, IFileSystemEntry>(entry));
        }
#endregion
    }
}