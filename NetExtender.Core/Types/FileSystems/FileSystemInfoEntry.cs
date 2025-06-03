using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.Serialization;
using NetExtender.FileSystems.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.FileSystems
{
    public abstract class FileSystemInfoEntry : IFileSystemEntry
    {
        public event PropertyChangingEventHandler? PropertyChanging;
        public event PropertyChangedEventHandler? PropertyChanged;
        
        public virtual FileSystemInfo? Info
        {
            get
            {
                return Storage.Info(this);
            }
        }
        
        private readonly IFileSystemStorage _storage;
        private protected IFileSystemStorage Storage
        {
            get
            {
                return _storage;
            }
            init
            {
                _storage = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        Guid IFileSystemInfo.Storage
        {
            get
            {
                return Storage.Id;
            }
        }

        IFileSystemStorage IFileSystemStorageEntry.FileSystem
        {
            get
            {
                return Storage;
            }
        }
        
        IFileSystem IFileSystemStorageInfo.FileSystem
        {
            get
            {
                return Storage;
            }
        }

        public SyncRoot SyncRoot { get; } = SyncRoot.Create();

        Object IFileSystemStorageInfo.SyncRoot
        {
            get
            {
                return SyncRoot;
            }
        }

        public virtual Boolean IsSynchronized
        {
            get
            {
                return Storage.IsSynchronized;
            }
        }

        public virtual String Name
        {
            get
            {
                return Storage.Name(this);
            }
        }

        public virtual String FullName
        {
            get
            {
                return Storage.FullName(this);
            }
        }

        public virtual String Extension
        {
            get
            {
                return Storage.Extension(this);
            }
        }

        public virtual Boolean IsReal
        {
            get
            {
                return Storage.IsReal(this);
            }
        }

        public virtual Boolean Exists
        {
            get
            {
                return Storage.Exists(this);
            }
        }

        public virtual FileAttributes Attributes
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

        public virtual String? LinkTarget
        {
            get
            {
                return Storage.LinkTarget(this);
            }
        }

        public virtual DateTime CreationTime
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

        public virtual DateTime CreationTimeUtc
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

        public virtual DateTime LastAccessTime
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

        public virtual DateTime LastAccessTimeUtc
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

        public virtual DateTime LastWriteTime
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

        public virtual DateTime LastWriteTimeUtc
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

        public virtual IDriveInfo? Drive
        {
            get
            {
                return Storage.Drive(this);
            }
        }

        IDriveEntry? IFileSystemEntry.Drive
        {
            get
            {
                return (IDriveEntry?) Drive;
            }
        }

        private protected FileSystemInfoEntry()
        {
            _storage = null!;
        }

        internal FileSystemInfoEntry(IFileSystemStorage storage)
        {
            _storage = storage ?? throw new ArgumentNullException(nameof(storage));
        }

        protected void OnPropertyChanging(String property)
        {
            PropertyChanging?.Invoke(this, new PropertyChanging(property));
        }

        void IFileSystemStorageEntry.OnPropertyChanging(String property)
        {
            OnPropertyChanging(property);
        }

        protected void OnPropertyChanged(String property)
        {
            PropertyChanged?.Invoke(this, new PropertyChanged(property));
        }

        void IFileSystemStorageEntry.OnPropertyChanged(String property)
        {
            OnPropertyChanged(property);
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            Storage.GetObjectData(this, info, context);
        }

        public virtual Boolean CreateAsSymbolicLink(String target)
        {
            return Storage.CreateAsSymbolicLink(this, target);
        }

        public virtual IFileSystemInfo? ResolveLinkTarget()
        {
            return Storage.ResolveLinkTarget(this);
        }

        IFileSystemEntry? IFileSystemEntry.ResolveLinkTarget()
        {
            return (IFileSystemEntry?) ResolveLinkTarget();
        }

        public virtual IFileSystemInfo? ResolveLinkTarget(Boolean final)
        {
            return Storage.ResolveLinkTarget(this, final);
        }

        IFileSystemEntry? IFileSystemEntry.ResolveLinkTarget(Boolean final)
        {
            return (IFileSystemEntry?) ResolveLinkTarget(final);
        }

        public virtual void Delete()
        {
            Storage.Delete(this);
        }

        public virtual void Refresh()
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
                FileSystemInfoWrapper info => Equals(info.Info),
                FileSystemInfo info => Equals(info),
                IFileSystemInfo info => Equals(info),
                _ => false
            };
        }

        public virtual Boolean Equals(FileSystemInfo? other)
        {
            return Storage.Equals(this, other);
        }

        public virtual Boolean Equals(IFileSystemInfo? other)
        {
            return Storage.Equals(this, other);
        }

        Boolean IEquatable<IFileSystemStorageInfo>.Equals(IFileSystemStorageInfo? other)
        {
            return Equals(other);
        }

        public override String ToString()
        {
            return Storage.ToString(this);
        }
    }
    
    public abstract class FileSystemInfoWrapper : IFileSystemInfo
    {
        [return: NotNullIfNotNull("value")]
        public static implicit operator FileSystemInfo?(FileSystemInfoWrapper? value)
        {
            return value?.Info;
        }

        public event PropertyChangingEventHandler? PropertyChanging;
        public event PropertyChangedEventHandler? PropertyChanged;

        public abstract FileSystemInfo Info { get; }

        Guid IFileSystemInfo.Storage
        {
            get
            {
                return FileSystem.Id;
            }
        }

        public IFileSystem FileSystem
        {
            get
            {
                return IFileSystem.Default;
            }
        }

        public virtual Object SyncRoot
        {
            get
            {
                return Info;
            }
        }

        public virtual Boolean IsSynchronized
        {
            get
            {
                return FileSystem.IsSynchronized;
            }
        }

        public virtual String Name
        {
            get
            {
                return Info.Name;
            }
        }

        public virtual String FullName
        {
            get
            {
                return Info.FullName;
            }
        }

        public virtual String Extension
        {
            get
            {
                return Info.Extension;
            }
        }

        public virtual Boolean IsReal
        {
            get
            {
                return true;
            }
        }

        public virtual Boolean Exists
        {
            get
            {
                return Info.Exists;
            }
        }

        public virtual FileAttributes Attributes
        {
            get
            {
                return Info.Attributes;
            }
            set
            {
                OnPropertyChanging(nameof(Attributes));
                Info.Attributes = value;
                OnPropertyChanged(nameof(Attributes));
            }
        }

        public virtual String? LinkTarget
        {
            get
            {
                return Info.LinkTarget;
            }
        }

        public virtual DateTime CreationTime
        {
            get
            {
                return Info.CreationTime;
            }
            set
            {
                OnPropertyChanging(nameof(CreationTime));
                Info.CreationTime = value;
                OnPropertyChanged(nameof(CreationTime));
            }
        }

        public virtual DateTime CreationTimeUtc
        {
            get
            {
                return Info.CreationTimeUtc;
            }
            set
            {
                OnPropertyChanging(nameof(CreationTimeUtc));
                Info.CreationTimeUtc = value;
                OnPropertyChanged(nameof(CreationTimeUtc));
            }
        }

        public virtual DateTime LastAccessTime
        {
            get
            {
                return Info.LastAccessTime;
            }
            set
            {
                OnPropertyChanging(nameof(LastAccessTime));
                Info.LastAccessTime = value;
                OnPropertyChanged(nameof(LastAccessTime));
            }
        }

        public virtual DateTime LastAccessTimeUtc
        {
            get
            {
                return Info.LastAccessTimeUtc;
            }
            set
            {
                OnPropertyChanging(nameof(LastAccessTimeUtc));
                Info.LastAccessTimeUtc = value;
                OnPropertyChanged(nameof(LastAccessTimeUtc));
            }
        }

        public virtual DateTime LastWriteTime
        {
            get
            {
                return Info.LastWriteTime;
            }
            set
            {
                OnPropertyChanging(nameof(LastWriteTime));
                Info.LastWriteTime = value;
                OnPropertyChanged(nameof(LastWriteTime));
            }
        }

        public virtual DateTime LastWriteTimeUtc
        {
            get
            {
                return Info.LastWriteTimeUtc;
            }
            set
            {
                OnPropertyChanging(nameof(LastWriteTimeUtc));
                Info.LastWriteTimeUtc = value;
                OnPropertyChanged(nameof(LastWriteTimeUtc));
            }
        }

        public abstract DriveInfo? Drive { get; }

        IDriveInfo? IFileSystemInfo.Drive
        {
            get
            {
                return Create(Drive);
            }
        }

        protected const String AnyPattern = FileSystemHandler.AnyPattern;

        protected void OnPropertyChanging(String property)
        {
            PropertyChanging?.Invoke(this, new PropertyChanging(property));
        }

        protected void OnPropertyChanged(String property)
        {
            PropertyChanged?.Invoke(this, new PropertyChanged(property));
        }
        
        [return: NotNullIfNotNull("value")]
        protected internal static IFileSystemInfo? Create(FileSystemInfo? value)
        {
            return value switch
            {
                null => null,
                FileInfo info => Create(info),
                DirectoryInfo info => Create(info),
                _ => throw new NotSupportedException($"Type '{value.GetType()}' not supported.")
            };
        }

        [return: NotNullIfNotNull("value")]
        protected internal static IFileInfo? Create(FileInfo? value)
        {
            return value is not null ? new FileInfoWrapper(value) : null;
        }

        [return: NotNullIfNotNull("value")]
        protected internal static IDirectoryInfo? Create(DirectoryInfo? value)
        {
            return value is not null ? new DirectoryInfoWrapper(value) : null;
        }

        [return: NotNullIfNotNull("value")]
        protected internal static IDriveInfo? Create(DriveInfo? value)
        {
            return value is not null ? new DriveInfoWrapper(value) : null;
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            Info.GetObjectData(info, context);
        }

        public virtual Boolean CreateAsSymbolicLink(String target)
        {
            Info.CreateAsSymbolicLink(target);
            return true;
        }

        public IFileSystemInfo? ResolveLinkTarget()
        {
            return ResolveLinkTarget(true);
        }

        public virtual IFileSystemInfo? ResolveLinkTarget(Boolean final)
        {
            return Info.ResolveLinkTarget(final) switch
            {
                null => null,
                FileInfo info => new FileInfoWrapper(info),
                DirectoryInfo info => new DirectoryInfoWrapper(info),
                { } info => throw new NotSupportedException($"Type '{info.GetType()}' not supported.")
            };
        }

        public virtual void Delete()
        {
            Info.Delete();
        }

        public virtual void Refresh()
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
                FileSystemInfoWrapper info => Equals(info.Info),
                FileSystemInfo info => Equals(info),
                IFileSystemInfo info => Equals(info),
                _ => false
            };
        }

        public virtual Boolean Equals(FileSystemInfo? other)
        {
            return other is not null && Info.Equals(other);
        }

        public virtual Boolean Equals(IFileSystemInfo? other)
        {
            return other is FileSystemInfoWrapper info && Equals(info.Info);
        }

        Boolean IEquatable<IFileSystemStorageInfo>.Equals(IFileSystemStorageInfo? other)
        {
            return Equals(other);
        }

        public override String ToString()
        {
            return Info.ToString();
        }
    }
}