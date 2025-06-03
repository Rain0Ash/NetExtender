using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.Serialization;
using NetExtender.FileSystems.Interfaces;
using NetExtender.Utilities.IO;

namespace NetExtender.FileSystems
{
    public class LinkInfoEntry : FileSystemInfoEntry, ILinkEntry
    {
        public sealed override FileSystemInfo? Info
        {
            get
            {
                return Storage.Info(this);
            }
        }
        
        public override String Name
        {
            get
            {
                return Storage.Name(this);
            }
        }

        public override String FullName
        {
            get
            {
                return Storage.FullName(this);
            }
        }

        public override String Extension
        {
            get
            {
                return Storage.Extension(this);
            }
        }

        public override Boolean IsReal
        {
            get
            {
                return Storage.IsReal(this);
            }
        }

        public override Boolean Exists
        {
            get
            {
                return Storage.Exists(this);
            }
        }

        public override FileAttributes Attributes
        {
            get
            {
                return Storage.Attributes(this);
            }
            set
            {
                Storage.Attributes(this, value);
            }
        }

        public override String? LinkTarget
        {
            get
            {
                return Storage.LinkTarget(this);
            }
        }

        public override DateTime CreationTime
        {
            get
            {
                return Storage.CreationTime(this);
            }
            set
            {
                Storage.CreationTime(this, value);
            }
        }

        public override DateTime CreationTimeUtc
        {
            get
            {
                return Storage.CreationTimeUtc(this);
            }
            set
            {
                Storage.CreationTimeUtc(this, value);
            }
        }

        public override DateTime LastAccessTime
        {
            get
            {
                return Storage.LastAccessTime(this);
            }
            set
            {
                Storage.LastAccessTime(this, value);
            }
        }

        public override DateTime LastAccessTimeUtc
        {
            get
            {
                return Storage.LastAccessTimeUtc(this);
            }
            set
            {
                Storage.LastAccessTimeUtc(this, value);
            }
        }

        public override DateTime LastWriteTime
        {
            get
            {
                return Storage.LastWriteTime(this);
            }
            set
            {
                Storage.LastWriteTime(this, value);
            }
        }

        public override DateTime LastWriteTimeUtc
        {
            get
            {
                return Storage.LastWriteTimeUtc(this);
            }
            set
            {
                Storage.LastWriteTimeUtc(this, value);
            }
        }

        public virtual String? DirectoryName
        {
            get
            {
                return Storage.DirectoryName(this);
            }
        }

        public virtual IDirectoryInfo? Directory
        {
            get
            {
                return Storage.GetDirectory(this);
            }
        }

        IDirectoryEntry? ILinkEntry.Directory
        {
            get
            {
                return (IDirectoryEntry?) Directory;
            }
        }

        public override IDriveInfo? Drive
        {
            get
            {
                return Storage.Drive(this);
            }
        }

        private protected LinkInfoEntry()
        {
        }

        internal LinkInfoEntry(IFileSystemStorage storage)
            : base(storage)
        {
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            Storage.GetObjectData(this, info, context);
        }

        public sealed override Boolean CreateAsSymbolicLink(String target)
        {
            return Storage.CreateAsSymbolicLink(this, target);
        }

        public sealed override IFileSystemInfo? ResolveLinkTarget()
        {
            return Storage.ResolveLinkTarget(this);
        }

        public sealed override IFileSystemInfo? ResolveLinkTarget(Boolean final)
        {
            return Storage.ResolveLinkTarget(this, final);
        }

        public virtual void Create()
        {
            Storage.Create(this);
        }

        public sealed override void Delete()
        {
            Storage.Delete(this);
        }

        public sealed override void Refresh()
        {
            Storage.Refresh(this);
        }

        public override Int32 GetHashCode()
        {
            return Storage.GetHashCode(this);
        }

        public override Boolean Equals(Object? other)
        {
            return other switch
            {
                LinkInfoWrapper info => Equals(info.Info),
                ILinkInfo info => Equals(info),
                FileInfoWrapper info => Equals(info.Info),
                FileInfo info => Equals(info),
                IFileInfo info => Equals(info),
                DirectoryInfoWrapper info => Equals(info.Info),
                DirectoryInfo info => Equals(info),
                IDirectoryInfo info => Equals(info),
                FileSystemInfoWrapper info => Equals(info.Info),
                FileSystemInfo info => Equals(info),
                IFileSystemInfo info => Equals(info),
                _ => false
            };
        }

        public Boolean Equals(ILinkInfo? other)
        {
            return Storage.Equals(this, other);
        }

        public Boolean Equals(FileInfo? other)
        {
            return Storage.Equals(this, other);
        }

        public Boolean Equals(IFileInfo? other)
        {
            return Storage.Equals(this, other);
        }

        public Boolean Equals(DirectoryInfo? other)
        {
            return Storage.Equals(this, other);
        }

        public Boolean Equals(IDirectoryInfo? other)
        {
            return Storage.Equals(this, other);
        }

        public override String ToString()
        {
            return Storage.ToString(this);
        }
    }

    public class LinkInfoWrapper : FileSystemInfoWrapper, ILinkInfo
    {
        [return: NotNullIfNotNull("value")]
        public static implicit operator FileSystemInfo?(LinkInfoWrapper? value)
        {
            return value?.Info;
        }
        
        public static implicit operator LinkInfoWrapper?(FileSystemInfo? value)
        {
            return value is { LinkTarget: not null } ? new LinkInfoWrapper(value) : null;
        }

        public sealed override FileSystemInfo Info { get; }

        public virtual String? DirectoryName
        {
            get
            {
                return Info switch
                {
                    FileInfo file => file.DirectoryName,
                    DirectoryInfo directory => directory.FullName,
                    _ => null
                };
            }
        }

        public virtual IDirectoryInfo? Directory
        {
            get
            {
                return Info switch
                {
                    FileInfo file => Create(file.Directory),
                    DirectoryInfo directory => Create(directory),
                    _ => null
                };
            }
        }

        public override DriveInfo? Drive
        {
            get
            {
                return Info.GetDrive();
            }
        }

        public LinkInfoWrapper(FileSystemInfo info)
        {
            Info = info ?? throw new ArgumentNullException(nameof(info));

            if (Info.LinkTarget is null)
            {
                throw new ArgumentException("The link target is not specified.", nameof(info));
            }
        }

        public virtual void Create()
        {
            switch (Info)
            {
                case FileInfo file:
                    file.Create();
                    return;
                case DirectoryInfo directory:
                    directory.Create();
                    return;
                default:
                    throw new NotSupportedException();
            }
        }

        public override void Delete()
        {
            Info.Delete();
        }

        public override void Refresh()
        {
            Info.Refresh();
        }

        public override Int32 GetHashCode()
        {
            return Info.GetHashCode();
        }

        public override Boolean Equals(Object? other)
        {
            return other switch
            {
                LinkInfoWrapper info => Equals(info.Info),
                ILinkInfo info => Equals(info),
                FileInfoWrapper info => Equals(info.Info),
                FileInfo info => Equals(info),
                IFileInfo info => Equals(info),
                DirectoryInfoWrapper info => Equals(info.Info),
                DirectoryInfo info => Equals(info),
                IDirectoryInfo info => Equals(info),
                FileSystemInfoWrapper info => Equals(info.Info),
                FileSystemInfo info => Equals(info),
                IFileSystemInfo info => Equals(info),
                _ => false
            };
        }

        public override Boolean Equals(FileSystemInfo? other)
        {
            return Info.Equals(other);
        }

        public override Boolean Equals(IFileSystemInfo? other)
        {
            return other switch
            {
                ILinkInfo link => Equals(link),
                IFileInfo file => Equals(file),
                IDirectoryInfo directory => Equals(directory),
                _ => false
            };
        }
        
        public virtual Boolean Equals(ILinkInfo? other)
        {
            return other is LinkInfoWrapper info && Equals(info.Info);
        }

        public virtual Boolean Equals(FileInfo? other)
        {
            return Info.Equals(other);
        }

        public virtual Boolean Equals(IFileInfo? other)
        {
            return other is FileInfoWrapper info && Equals(info.Info);
        }

        public virtual Boolean Equals(DirectoryInfo? other)
        {
            return Info.Equals(other);
        }

        public virtual Boolean Equals(IDirectoryInfo? other)
        {
            return other is DirectoryInfoWrapper info && Equals(info.Info);
        }

        public override String ToString()
        {
            return Info.ToString();
        }
    }
}