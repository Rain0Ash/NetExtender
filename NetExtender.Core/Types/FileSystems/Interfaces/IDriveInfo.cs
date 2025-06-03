using System;
using System.IO;
using NetExtender.Interfaces.Notify;

namespace NetExtender.FileSystems.Interfaces
{
    internal interface IDriveEntry : IDriveInfo, IFileSystemStorageEntry
    {
        /// <inheritdoc cref="DriveInfo.RootDirectory"/>
        public new IDirectoryEntry RootDirectory { get; }
    }
    
    public interface IDriveInfo : IFileSystemStorageInfo, IEquatable<DriveInfo>, IEquatable<IDriveInfo>, INotifyProperty
    {
        public DriveInfo? Info { get; }
        
        public Guid Storage { get; }
        
        /// <inheritdoc cref="DriveInfo.Name"/>
        public new String Name { get; }
        
        /// <inheritdoc cref="DriveInfo.Name"/>
        public String FullName { get; }
        
        /// <inheritdoc cref="DriveInfo.IsReady"/>
        public Boolean IsReady { get; }
        
        /// <inheritdoc cref="DriveInfo.DriveType"/>
        public DriveType DriveType { get; }
        
        /// <inheritdoc cref="DriveInfo.DriveFormat"/>
        public String DriveFormat { get; }
        
        /// <inheritdoc cref="DriveInfo.VolumeLabel"/>
        public String? VolumeLabel { get; set; }
        
        /// <inheritdoc cref="DriveInfo.AvailableFreeSpace"/>
        public Int64 AvailableFreeSpace { get; }
        
        /// <inheritdoc cref="DriveInfo.TotalFreeSpace"/>
        public Int64 TotalFreeSpace { get; }
        
        /// <inheritdoc cref="DriveInfo.TotalSize"/>
        public Int64 TotalSize { get; }
        
        /// <inheritdoc cref="DriveInfo.RootDirectory"/>
        public IDirectoryInfo RootDirectory { get; }
    }
}