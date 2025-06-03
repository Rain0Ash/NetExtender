using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.Serialization;
using NetExtender.FileSystems.Interfaces;
using NetExtender.Utilities.Types;

namespace NetExtender.FileSystems
{
    public class DriveInfoEntry : IDriveEntry
    {
        public event PropertyChangingEventHandler? PropertyChanging;
        public event PropertyChangedEventHandler? PropertyChanged;
        
        public DriveInfo? Info
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

        Guid IDriveInfo.Storage
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

        public virtual Boolean IsReal
        {
            get
            {
                return Storage.IsReal(this);
            }
        }

        public virtual Boolean IsReady
        {
            get
            {
                return Storage.IsReady(this);
            }
        }

        public virtual DriveType DriveType
        {
            get
            {
                return Storage.DriveType(this);
            }
        }

        public virtual String DriveFormat
        {
            get
            {
                return Storage.DriveFormat(this);
            }
        }

        public virtual String? VolumeLabel
        {
            get
            {
                return Storage.VolumeLabel(this);
            }
            set
            {
                Storage.VolumeLabel(this, value);
            }
        }

        public virtual Int64 AvailableFreeSpace
        {
            get
            {
                return Storage.AvailableFreeSpace(this);
            }
        }

        public virtual Int64 TotalFreeSpace
        {
            get
            {
                return Storage.TotalFreeSpace(this);
            }
        }

        public virtual Int64 TotalSize
        {
            get
            {
                return Storage.TotalSize(this);
            }
        }

        public virtual IDirectoryInfo RootDirectory
        {
            get
            {
                return Storage.RootDirectory(this);
            }
        }

        IDirectoryEntry IDriveEntry.RootDirectory
        {
            get
            {
                return (IDirectoryEntry) RootDirectory;
            }
        }

        private protected DriveInfoEntry()
        {
            _storage = null!;
        }

        internal DriveInfoEntry(IFileSystemStorage storage)
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

        public override Int32 GetHashCode()
        {
            return Storage.GetHashCode(this);
        }

        public override Boolean Equals(Object? other)
        {
            return other switch
            {
                DriveInfoWrapper info => Equals(info.Info),
                DriveInfo info => Equals(info),
                IDriveInfo info => Equals(info),
                _ => false
            };
        }

        public Boolean Equals(DriveInfo? other)
        {
            return Storage.Equals(this, other);
        }

        public Boolean Equals(IDriveInfo? other)
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

    public class DriveInfoWrapper : IDriveInfo
    {
        [return: NotNullIfNotNull("value")]
        public static implicit operator DriveInfo?(DriveInfoWrapper? value)
        {
            return value?.Info;
        }
        
        [return: NotNullIfNotNull("value")]
        public static implicit operator DriveInfoWrapper?(DriveInfo? value)
        {
            return value is not null ? new DriveInfoWrapper(value) : null;
        }
        
        public event PropertyChangingEventHandler? PropertyChanging;
        public event PropertyChangedEventHandler? PropertyChanged;
        
        public DriveInfo Info { get; }

        Guid IDriveInfo.Storage
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
                return Name;
            }
        }

        public virtual Boolean IsReal
        {
            get
            {
                return true;
            }
        }

        public virtual Boolean IsReady
        {
            get
            {
                return Info.IsReady;
            }
        }

        public virtual DriveType DriveType
        {
            get
            {
                return Info.DriveType;
            }
        }

        public virtual String DriveFormat
        {
            get
            {
                return Info.DriveFormat;
            }
        }

        public virtual String? VolumeLabel
        {
            get
            {
                return Info.VolumeLabel;
            }
            set
            {
                if (!OperatingSystem.IsWindows())
                {
                    return;
                }

                OnPropertyChanging(nameof(VolumeLabel));
                Info.VolumeLabel = value;
                OnPropertyChanged(nameof(VolumeLabel));
            }
        }

        public virtual Int64 AvailableFreeSpace
        {
            get
            {
                return Info.AvailableFreeSpace;
            }
        }

        public virtual Int64 TotalFreeSpace
        {
            get
            {
                return Info.TotalFreeSpace;
            }
        }

        public virtual Int64 TotalSize
        {
            get
            {
                return Info.TotalSize;
            }
        }

        public virtual IDirectoryInfo RootDirectory
        {
            get
            {
                return FileSystemInfoWrapper.Create(Info.RootDirectory);
            }
        }

        public DriveInfoWrapper(DriveInfo info)
        {
            Info = info ?? throw new ArgumentNullException(nameof(info));
        }

        protected void OnPropertyChanging(String property)
        {
            PropertyChanging?.Invoke(this, new PropertyChanging(property));
        }

        protected void OnPropertyChanged(String property)
        {
            PropertyChanged?.Invoke(this, new PropertyChanged(property));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            ((ISerializable) Info).GetObjectData(info, context);
        }
        
        public override Int32 GetHashCode()
        {
            return Info.GetHashCode();
        }

        public override Boolean Equals(Object? other)
        {
            return other switch
            {
                DriveInfoWrapper info => Equals(info.Info),
                DriveInfo info => Equals(info),
                IDriveInfo info => Equals(info),
                _ => false
            };
        }
        
        public virtual Boolean Equals(DriveInfo? other)
        {
            return Info.Equals(other);
        }

        public virtual Boolean Equals(IDriveInfo? other)
        {
            return other is DriveInfoWrapper info && Equals(info.Info);
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