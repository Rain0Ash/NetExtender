using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using NetExtender.FileSystems.Interfaces;
using NetExtender.Utilities.IO;
using NetExtender.Utilities.Types;

namespace NetExtender.FileSystems
{
    public class DirectoryInfoEntry : FileSystemInfoEntry, IDirectoryEntry
    {
        public sealed override DirectoryInfo? Info
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

        public virtual IDirectoryInfo Root
        {
            get
            {
                return Storage.Root(this);
            }
        }

        IDirectoryEntry IDirectoryEntry.Root
        {
            get
            {
                return (IDirectoryEntry) Root;
            }
        }

        public virtual IDirectoryInfo? Parent
        {
            get
            {
                return Storage.Parent(this);
            }
        }

        IDirectoryEntry? IDirectoryEntry.Parent
        {
            get
            {
                return (IDirectoryEntry?) Parent;
            }
        }

        public override IDriveInfo? Drive
        {
            get
            {
                return Storage.Drive(this);
            }
        }

        private protected DirectoryInfoEntry()
        {
        }

        internal DirectoryInfoEntry(IFileSystemStorage storage)
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
        
        public IFileSystemInfo[] GetFileSystemInfos()
        {
            return Storage.GetFileSystemInfos(this).ConvertAll<IFileSystemEntry, IFileSystemInfo>();
        }

        IFileSystemEntry[] IDirectoryEntry.GetFileSystemInfos()
        {
            return (IFileSystemEntry[]) GetFileSystemInfos();
        }

        public IFileSystemInfo[] GetFileSystemInfos(String? pattern)
        {
            return Storage.GetFileSystemInfos(this, pattern).ConvertAll<IFileSystemEntry, IFileSystemInfo>();
        }

        IFileSystemEntry[] IDirectoryEntry.GetFileSystemInfos(String? pattern)
        {
            return (IFileSystemEntry[]) GetFileSystemInfos(pattern);
        }

        public IFileSystemInfo[] GetFileSystemInfos(String? pattern, SearchOption option)
        {
            return Storage.GetFileSystemInfos(this, pattern, option).ConvertAll<IFileSystemEntry, IFileSystemInfo>();
        }

        IFileSystemEntry[] IDirectoryEntry.GetFileSystemInfos(String? pattern, SearchOption option)
        {
            return (IFileSystemEntry[]) GetFileSystemInfos(pattern, option);
        }

        public IFileSystemInfo[] GetFileSystemInfos(String? pattern, EnumerationOptions options)
        {
            return Storage.GetFileSystemInfos(this, pattern, options).ConvertAll<IFileSystemEntry, IFileSystemInfo>();
        }

        IFileSystemEntry[] IDirectoryEntry.GetFileSystemInfos(String? pattern, EnumerationOptions options)
        {
            return (IFileSystemEntry[]) GetFileSystemInfos(pattern, options);
        }

        public IFileInfo[] GetFiles()
        {
            return Storage.GetFiles(this).ConvertAll<IFileEntry, IFileInfo>();
        }

        IFileEntry[] IDirectoryEntry.GetFiles()
        {
            return (IFileEntry[]) GetFiles();
        }

        public IFileInfo[] GetFiles(String? pattern)
        {
            return Storage.GetFiles(this, pattern).ConvertAll<IFileEntry, IFileInfo>();
        }

        IFileEntry[] IDirectoryEntry.GetFiles(String? pattern)
        {
            return (IFileEntry[]) GetFiles(pattern);
        }

        public IFileInfo[] GetFiles(String? pattern, SearchOption option)
        {
            return Storage.GetFiles(this, pattern, option).ConvertAll<IFileEntry, IFileInfo>();
        }

        IFileEntry[] IDirectoryEntry.GetFiles(String? pattern, SearchOption option)
        {
            return (IFileEntry[]) GetFiles(pattern, option);
        }

        public IFileInfo[] GetFiles(String? pattern, EnumerationOptions options)
        {
            return Storage.GetFiles(this, pattern, options).ConvertAll<IFileEntry, IFileInfo>();
        }

        IFileEntry[] IDirectoryEntry.GetFiles(String? pattern, EnumerationOptions options)
        {
            return (IFileEntry[]) GetFiles(pattern, options);
        }

        public IDirectoryInfo[] GetDirectories()
        {
            return Storage.GetDirectories(this).ConvertAll<IDirectoryEntry, IDirectoryInfo>();
        }

        IDirectoryEntry[] IDirectoryEntry.GetDirectories()
        {
            return (IDirectoryEntry[]) GetDirectories();
        }

        public IDirectoryInfo[] GetDirectories(String? pattern)
        {
            return Storage.GetDirectories(this, pattern).ConvertAll<IDirectoryEntry, IDirectoryInfo>();
        }

        IDirectoryEntry[] IDirectoryEntry.GetDirectories(String? pattern)
        {
            return (IDirectoryEntry[]) GetDirectories(pattern);
        }

        public IDirectoryInfo[] GetDirectories(String? pattern, SearchOption option)
        {
            return Storage.GetDirectories(this, pattern, option).ConvertAll<IDirectoryEntry, IDirectoryInfo>();
        }

        IDirectoryEntry[] IDirectoryEntry.GetDirectories(String? pattern, SearchOption option)
        {
            return (IDirectoryEntry[]) GetDirectories(pattern, option);
        }

        public IDirectoryInfo[] GetDirectories(String? pattern, EnumerationOptions options)
        {
            return Storage.GetDirectories(this, pattern, options).ConvertAll<IDirectoryEntry, IDirectoryInfo>();
        }

        IDirectoryEntry[] IDirectoryEntry.GetDirectories(String? pattern, EnumerationOptions options)
        {
            return (IDirectoryEntry[]) GetDirectories(pattern, options);
        }

        public IEnumerable<IFileSystemInfo> EnumerateFileSystemInfos()
        {
            return Storage.EnumerateFileSystemInfos(this);
        }

        IEnumerable<IFileSystemEntry> IDirectoryEntry.EnumerateFileSystemInfos()
        {
            return (IEnumerable<IFileSystemEntry>) EnumerateFileSystemInfos();
        }

        public IEnumerable<IFileSystemInfo> EnumerateFileSystemInfos(String? pattern)
        {
            return Storage.EnumerateFileSystemInfos(this, pattern);
        }

        IEnumerable<IFileSystemEntry> IDirectoryEntry.EnumerateFileSystemInfos(String? pattern)
        {
            return (IEnumerable<IFileSystemEntry>) EnumerateFileSystemInfos(pattern);
        }

        public IEnumerable<IFileSystemInfo> EnumerateFileSystemInfos(String? pattern, SearchOption option)
        {
            return Storage.EnumerateFileSystemInfos(this, pattern, option);
        }

        IEnumerable<IFileSystemEntry> IDirectoryEntry.EnumerateFileSystemInfos(String? pattern, SearchOption option)
        {
            return (IEnumerable<IFileSystemEntry>) EnumerateFileSystemInfos(pattern, option);
        }

        public IEnumerable<IFileSystemInfo> EnumerateFileSystemInfos(String? pattern, EnumerationOptions options)
        {
            return Storage.EnumerateFileSystemInfos(this, pattern, options);
        }

        IEnumerable<IFileSystemEntry> IDirectoryEntry.EnumerateFileSystemInfos(String? pattern, EnumerationOptions options)
        {
            return (IEnumerable<IFileSystemEntry>) EnumerateFileSystemInfos(pattern, options);
        }

        public IEnumerable<IFileInfo> EnumerateFiles()
        {
            return Storage.EnumerateFiles(this);
        }

        IEnumerable<IFileEntry> IDirectoryEntry.EnumerateFiles()
        {
            return (IEnumerable<IFileEntry>) EnumerateFiles();
        }

        public IEnumerable<IFileInfo> EnumerateFiles(String? pattern)
        {
            return Storage.EnumerateFiles(this, pattern);
        }

        IEnumerable<IFileEntry> IDirectoryEntry.EnumerateFiles(String? pattern)
        {
            return (IEnumerable<IFileEntry>) EnumerateFiles(pattern);
        }

        public IEnumerable<IFileInfo> EnumerateFiles(String? pattern, SearchOption option)
        {
            return Storage.EnumerateFiles(this, pattern, option);
        }

        IEnumerable<IFileEntry> IDirectoryEntry.EnumerateFiles(String? pattern, SearchOption option)
        {
            return (IEnumerable<IFileEntry>) EnumerateFiles(pattern, option);
        }

        public IEnumerable<IFileInfo> EnumerateFiles(String? pattern, EnumerationOptions options)
        {
            return Storage.EnumerateFiles(this, pattern, options);
        }

        IEnumerable<IFileEntry> IDirectoryEntry.EnumerateFiles(String? pattern, EnumerationOptions options)
        {
            return (IEnumerable<IFileEntry>) EnumerateFiles(pattern, options);
        }

        public IEnumerable<IDirectoryInfo> EnumerateDirectories()
        {
            return Storage.EnumerateDirectories(this);
        }

        IEnumerable<IDirectoryEntry> IDirectoryEntry.EnumerateDirectories()
        {
            return (IEnumerable<IDirectoryEntry>) EnumerateDirectories();
        }

        public IEnumerable<IDirectoryInfo> EnumerateDirectories(String? pattern)
        {
            return Storage.EnumerateDirectories(this, pattern);
        }

        IEnumerable<IDirectoryEntry> IDirectoryEntry.EnumerateDirectories(String? pattern)
        {
            return (IEnumerable<IDirectoryEntry>) EnumerateDirectories(pattern);
        }

        public IEnumerable<IDirectoryInfo> EnumerateDirectories(String? pattern, SearchOption option)
        {
            return Storage.EnumerateDirectories(this, pattern, option);
        }

        IEnumerable<IDirectoryEntry> IDirectoryEntry.EnumerateDirectories(String? pattern, SearchOption option)
        {
            return (IEnumerable<IDirectoryEntry>) EnumerateDirectories(pattern, option);
        }

        public IEnumerable<IDirectoryInfo> EnumerateDirectories(String? pattern, EnumerationOptions options)
        {
            return Storage.EnumerateDirectories(this, pattern, options);
        }

        IEnumerable<IDirectoryEntry> IDirectoryEntry.EnumerateDirectories(String? pattern, EnumerationOptions options)
        {
            return (IEnumerable<IDirectoryEntry>) EnumerateDirectories(pattern, options);
        }

        public void MoveTo(String destination)
        {
            Storage.MoveTo(this, destination);
        }

        public void Create()
        {
            Storage.Create(this);
        }

        public IDirectoryInfo CreateSubdirectory(String path)
        {
            return Storage.CreateSubdirectory(this, path);
        }

        IDirectoryEntry IDirectoryEntry.CreateSubdirectory(String path)
        {
            return (IDirectoryEntry) CreateSubdirectory(path);
        }

        public sealed override void Delete()
        {
            Storage.Delete(this);
        }

        public void Delete(Boolean recursive)
        {
            Storage.Delete(this, recursive);
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
                DirectoryInfoWrapper info => Equals(info.Info),
                DirectoryInfo info => Equals(info),
                IDirectoryInfo info => Equals(info),
                FileSystemInfoWrapper info => Equals(info.Info),
                FileSystemInfo info => Equals(info),
                IFileSystemInfo info => Equals(info),
                _ => false
            };
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

    public class DirectoryInfoWrapper : FileSystemInfoWrapper, IDirectoryInfo
    {
        [return: NotNullIfNotNull("value")]
        public static implicit operator DirectoryInfo?(DirectoryInfoWrapper? value)
        {
            return value?.Info;
        }
        
        [return: NotNullIfNotNull("value")]
        public static implicit operator DirectoryInfoWrapper?(DirectoryInfo? value)
        {
            return value is not null ? new DirectoryInfoWrapper(value) : null;
        }

        public sealed override DirectoryInfo Info { get; }

        public virtual IDirectoryInfo Root
        {
            get
            {
                return Create(Info.Root);
            }
        }

        public virtual IDirectoryInfo? Parent
        {
            get
            {
                return Create(Info.Parent);
            }
        }

        public override DriveInfo? Drive
        {
            get
            {
                return Info.GetDrive();
            }
        }

        /// <inheritdoc cref="DirectoryInfo(System.String)"/>
        public DirectoryInfoWrapper(String path)
        {
            if (path is null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            Info = new DirectoryInfo(path);
        }

        public DirectoryInfoWrapper(DirectoryInfo info)
        {
            Info = info ?? throw new ArgumentNullException(nameof(info));
        }

        public virtual IFileSystemInfo[] GetFileSystemInfos()
        {
            return Info.GetFileSystemInfos().ConvertAll(Create)!;
        }

        public virtual IFileSystemInfo[] GetFileSystemInfos(String? pattern)
        {
            return Info.GetFileSystemInfos(pattern ?? AnyPattern).ConvertAll(Create)!;
        }

        public virtual IFileSystemInfo[] GetFileSystemInfos(String? pattern, SearchOption option)
        {
            return Info.GetFileSystemInfos(pattern ?? AnyPattern, option).ConvertAll(Create)!;
        }

        public virtual IFileSystemInfo[] GetFileSystemInfos(String? pattern, EnumerationOptions options)
        {
            return Info.GetFileSystemInfos(pattern ?? AnyPattern, options).ConvertAll(Create)!;
        }

        public virtual IFileInfo[] GetFiles()
        {
            return Info.GetFiles().ConvertAll(Create)!;
        }

        public virtual IFileInfo[] GetFiles(String? pattern)
        {
            return Info.GetFiles(pattern ?? AnyPattern).ConvertAll(Create)!;
        }

        public virtual IFileInfo[] GetFiles(String? pattern, SearchOption option)
        {
            return Info.GetFiles(pattern ?? AnyPattern, option).ConvertAll(Create)!;
        }

        public virtual IFileInfo[] GetFiles(String? pattern, EnumerationOptions options)
        {
            return Info.GetFiles(pattern ?? AnyPattern, options).ConvertAll(Create)!;
        }

        public virtual IDirectoryInfo[] GetDirectories()
        {
            return Info.GetDirectories().ConvertAll(Create)!;
        }

        public virtual IDirectoryInfo[] GetDirectories(String? pattern)
        {
            return Info.GetDirectories(pattern ?? AnyPattern).ConvertAll(Create)!;
        }

        public virtual IDirectoryInfo[] GetDirectories(String? pattern, SearchOption option)
        {
            return Info.GetDirectories(pattern ?? AnyPattern, option).ConvertAll(Create)!;
        }

        public virtual IDirectoryInfo[] GetDirectories(String? pattern, EnumerationOptions options)
        {
            return Info.GetDirectories(pattern ?? AnyPattern, options).ConvertAll(Create)!;
        }

        public virtual IEnumerable<IFileSystemInfo> EnumerateFileSystemInfos()
        {
            return Info.EnumerateFileSystemInfos().Select(Create)!;
        }

        public virtual IEnumerable<IFileSystemInfo> EnumerateFileSystemInfos(String? pattern)
        {
            return Info.EnumerateFileSystemInfos(pattern ?? AnyPattern).Select(Create)!;
        }

        public virtual IEnumerable<IFileSystemInfo> EnumerateFileSystemInfos(String? pattern, SearchOption option)
        {
            return Info.EnumerateFileSystemInfos(pattern ?? AnyPattern, option).Select(Create)!;
        }

        public virtual IEnumerable<IFileSystemInfo> EnumerateFileSystemInfos(String? pattern, EnumerationOptions options)
        {
            return Info.EnumerateFileSystemInfos(pattern ?? AnyPattern, options).Select(Create)!;
        }

        public virtual IEnumerable<IFileInfo> EnumerateFiles()
        {
            return Info.EnumerateFiles().Select(Create)!;
        }

        public virtual IEnumerable<IFileInfo> EnumerateFiles(String? pattern)
        {
            return Info.EnumerateFiles(pattern ?? AnyPattern).Select(Create)!;
        }

        public virtual IEnumerable<IFileInfo> EnumerateFiles(String? pattern, SearchOption option)
        {
            return Info.EnumerateFiles(pattern ?? AnyPattern, option).Select(Create)!;
        }

        public virtual IEnumerable<IFileInfo> EnumerateFiles(String? pattern, EnumerationOptions options)
        {
            return Info.EnumerateFiles(pattern ?? AnyPattern, options).Select(Create)!;
        }

        public virtual IEnumerable<IDirectoryInfo> EnumerateDirectories()
        {
            return Info.EnumerateDirectories().Select(Create)!;
        }

        public virtual IEnumerable<IDirectoryInfo> EnumerateDirectories(String? pattern)
        {
            return Info.EnumerateDirectories(pattern ?? AnyPattern).Select(Create)!;
        }

        public virtual IEnumerable<IDirectoryInfo> EnumerateDirectories(String? pattern, SearchOption option)
        {
            return Info.EnumerateDirectories(pattern ?? AnyPattern, option).Select(Create)!;
        }

        public virtual IEnumerable<IDirectoryInfo> EnumerateDirectories(String? pattern, EnumerationOptions options)
        {
            return Info.EnumerateDirectories(pattern ?? AnyPattern, options).Select(Create)!;
        }

        public virtual void MoveTo(String destination)
        {
            Info.MoveTo(destination);
        }

        public virtual void Create()
        {
            Info.Create();
        }

        public virtual IDirectoryInfo CreateSubdirectory(String path)
        {
            return Create(Info.CreateSubdirectory(path));
        }

        public override void Delete()
        {
            Info.Delete();
        }

        public virtual void Delete(Boolean recursive)
        {
            Info.Delete(recursive);
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
            return other is DirectoryInfo info && Equals(info);
        }

        public override Boolean Equals(IFileSystemInfo? other)
        {
            return other is IDirectoryInfo info && Equals(info);
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