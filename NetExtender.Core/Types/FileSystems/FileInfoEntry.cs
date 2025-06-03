using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.Serialization;
using NetExtender.FileSystems.Interfaces;
using NetExtender.Utilities.IO;

namespace NetExtender.FileSystems
{
    public class FileInfoEntry : FileSystemInfoEntry, IFileEntry
    {
        public sealed override FileInfo? Info
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

        public virtual Int64 Length
        {
            get
            {
                return Storage.Length(this);
            }
        }

        public virtual Boolean IsReadOnly
        {
            get
            {
                return Storage.IsReadOnly(this);
            }
            set
            {
                Storage.IsReadOnly(this, value);
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

        IDirectoryEntry? IFileEntry.Directory
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

        private protected FileInfoEntry()
        {
        }

        internal FileInfoEntry(IFileSystemStorage storage)
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

        public FileStream Open(FileMode mode)
        {
            return Storage.Open(this, mode);
        }

        public FileStream Open(FileMode mode, FileAccess access)
        {
            return Storage.Open(this, mode, access);
        }

        public FileStream Open(FileMode mode, FileAccess access, FileShare share)
        {
            return Storage.Open(this, mode, access, share);
        }

        public FileStream Open(FileStreamOptions options)
        {
            return Storage.Open(this, options);
        }

        public FileStream OpenRead()
        {
            return Storage.OpenRead(this);
        }

        public FileStream OpenWrite()
        {
            return Storage.OpenWrite(this);
        }

        public StreamReader OpenText()
        {
            return Storage.OpenText(this);
        }

        public FileStream Create()
        {
            return Storage.Create(this);
        }

        public StreamWriter AppendText()
        {
            return Storage.AppendText(this);
        }

        public Boolean Encrypt()
        {
            return Storage.Encrypt(this);
        }

        public Boolean Decrypt()
        {
            return Storage.Decrypt(this);
        }

        public void MoveTo(String destination)
        {
            Storage.MoveTo(this, destination);
        }

        public void MoveTo(String destination, Boolean overwrite)
        {
            Storage.MoveTo(this, destination, overwrite);
        }

        public IFileInfo CopyTo(String destination)
        {
            return Storage.CopyTo(this, destination);
        }

        IFileEntry IFileEntry.CopyTo(String destination)
        {
            return (IFileEntry) CopyTo(destination);
        }

        public IFileInfo CopyTo(String destination, Boolean overwrite)
        {
            return Storage.CopyTo(this, destination, overwrite);
        }

        IFileEntry IFileEntry.CopyTo(String destination, Boolean overwrite)
        {
            return (IFileEntry) CopyTo(destination, overwrite);
        }

        public IFileInfo Replace(String destination, String? backup)
        {
            return Storage.Replace(this, destination, backup);
        }

        IFileEntry IFileEntry.Replace(String destination, String? backup)
        {
            return (IFileEntry) Replace(destination, backup);
        }

        public IFileInfo Replace(String destination, String? backup, Boolean suppress)
        {
            return Storage.Replace(this, destination, backup, suppress);
        }

        IFileEntry IFileEntry.Replace(String destination, String? backup, Boolean suppress)
        {
            return (IFileEntry) Replace(destination, backup, suppress);
        }

        public sealed override void Delete()
        {
            Storage.Delete(this);
        }

        public sealed override void Refresh()
        {
            Storage.Delete(this);
        }

        public override Int32 GetHashCode()
        {
            return Storage.GetHashCode(this);
        }

        public override Boolean Equals(Object? other)
        {
            return other switch
            {
                FileInfoWrapper info => Equals(info.Info),
                FileInfo info => Equals(info),
                IFileInfo info => Equals(info),
                FileSystemInfoWrapper info => Equals(info.Info),
                FileSystemInfo info => Equals(info),
                IFileSystemInfo info => Equals(info),
                _ => false
            };
        }

        public Boolean Equals(FileInfo? other)
        {
            return Storage.Equals(this, other);
        }

        public Boolean Equals(IFileInfo? other)
        {
            return Storage.Equals(this, other);
        }

        public override String ToString()
        {
            return Storage.ToString(this);
        }
    }

    public class FileInfoWrapper : FileSystemInfoWrapper, IFileInfo
    {
        [return: NotNullIfNotNull("value")]
        public static implicit operator FileInfo?(FileInfoWrapper? value)
        {
            return value?.Info;
        }
        
        [return: NotNullIfNotNull("value")]
        public static implicit operator FileInfoWrapper?(FileInfo? value)
        {
            return value is not null ? new FileInfoWrapper(value) : null;
        }

        public sealed override FileInfo Info { get; }

        public virtual Int64 Length
        {
            get
            {
                return Info.Length;
            }
        }

        public virtual Boolean IsReadOnly
        {
            get
            {
                return Info.IsReadOnly;
            }
            set
            {
                OnPropertyChanging(nameof(IsReadOnly));
                Info.IsReadOnly = value;
                OnPropertyChanged(nameof(IsReadOnly));
            }
        }

        public virtual String? DirectoryName
        {
            get
            {
                return Info.DirectoryName;
            }
        }

        public virtual IDirectoryInfo? Directory
        {
            get
            {
                return Create(Info.Directory);
            }
        }

        public override DriveInfo? Drive
        {
            get
            {
                return Info.GetDrive();
            }
        }

        /// <inheritdoc cref="FileInfo(System.String)"/>
        public FileInfoWrapper(String path)
        {
            if (path is null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            Info = new FileInfo(path);
        }

        public FileInfoWrapper(FileInfo info)
        {
            Info = info ?? throw new ArgumentNullException(nameof(info));
        }

        public virtual FileStream Open(FileMode mode)
        {
            return Info.Open(mode);
        }

        public virtual FileStream Open(FileMode mode, FileAccess access)
        {
            return Info.Open(mode, access);
        }

        public virtual FileStream Open(FileMode mode, FileAccess access, FileShare share)
        {
            return Info.Open(mode, access, share);
        }

        public virtual FileStream Open(FileStreamOptions options)
        {
            return Info.Open(options);
        }

        public virtual FileStream OpenRead()
        {
            return Info.OpenRead();
        }

        public virtual FileStream OpenWrite()
        {
            return Info.OpenWrite();
        }

        public virtual StreamReader OpenText()
        {
            return Info.OpenText();
        }

        public virtual FileStream Create()
        {
            return Info.Create();
        }

        public virtual StreamWriter AppendText()
        {
            return Info.AppendText();
        }

        public virtual Boolean Encrypt()
        {
            if (!OperatingSystem.IsWindows())
            {
                return false;
            }
            
            try
            {
                Info.Encrypt();
                return true;
            }
            catch (NotSupportedException)
            {
                return false;
            }
        }

        public virtual Boolean Decrypt()
        {
            if (!OperatingSystem.IsWindows())
            {
                return false;
            }
            
            try
            {
                Info.Decrypt();
                return true;
            }
            catch (NotSupportedException)
            {
                return false;
            }
        }

        public virtual void MoveTo(String destination)
        {
            Info.MoveTo(destination);
        }

        public virtual void MoveTo(String destination, Boolean overwrite)
        {
            Info.MoveTo(destination, overwrite);
        }

        public virtual IFileInfo CopyTo(String destination)
        {
            return Create(Info.CopyTo(destination));
        }

        public virtual IFileInfo CopyTo(String destination, Boolean overwrite)
        {
            return Create(Info.CopyTo(destination, overwrite));
        }

        public virtual IFileInfo Replace(String destination, String? backup)
        {
            return Create(Info.Replace(destination, backup));
        }

        public virtual IFileInfo Replace(String destination, String? backup, Boolean suppress)
        {
            return Create(Info.Replace(destination, backup, suppress));
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
                FileInfoWrapper info => Equals(info.Info),
                FileInfo info => Equals(info),
                IFileInfo info => Equals(info),
                FileSystemInfoWrapper info => Equals(info.Info),
                FileSystemInfo info => Equals(info),
                IFileSystemInfo info => Equals(info),
                _ => false
            };
        }

        public override Boolean Equals(FileSystemInfo? other)
        {
            return other is FileInfo info && Equals(info);
        }

        public override Boolean Equals(IFileSystemInfo? other)
        {
            return other is IFileInfo info && Equals(info);
        }

        public virtual Boolean Equals(FileInfo? other)
        {
            return Info.Equals(other);
        }

        public virtual Boolean Equals(IFileInfo? other)
        {
            return other is FileInfoWrapper info && Equals(info.Info);
        }

        public override String ToString()
        {
            return Info.ToString();
        }
    }
}