using System;
using System.IO;
using NetExtender.Interfaces.Notify;

namespace NetExtender.FileSystems.Interfaces
{
    internal interface IFileSystemEntry : IFileSystemInfo, IFileSystemStorageEntry
    {
        public new IDriveEntry? Drive { get; }
        
        /// <inheritdoc cref="FileSystemInfo.ResolveLinkTarget(Boolean)"/>
        public new IFileSystemEntry? ResolveLinkTarget();

        /// <inheritdoc cref="FileSystemInfo.ResolveLinkTarget(Boolean)"/>
        public new IFileSystemEntry? ResolveLinkTarget(Boolean final);
    }

    public interface IFileSystemInfo : IEquatable<FileSystemInfo>, IFileSystemStorageInfo, INotifyProperty
    {
        public FileSystemInfo? Info { get; }
        
        public Guid Storage { get; }

        /// <inheritdoc cref="FileSystemInfo.Name"/>
        public new String Name { get; }

        /// <inheritdoc cref="FileSystemInfo.FullName"/>
        public String FullName { get; }

        /// <inheritdoc cref="FileSystemInfo.Extension"/>
        public String Extension { get; }

        /// <inheritdoc cref="FileSystemInfo.Exists"/>
        public Boolean Exists { get; }

        /// <inheritdoc cref="FileSystemInfo.Attributes"/>
        public FileAttributes Attributes { get; set; }

        /// <inheritdoc cref="FileSystemInfo.LinkTarget"/>
        public String? LinkTarget { get; }

        /// <inheritdoc cref="FileSystemInfo.CreationTime"/>
        public DateTime CreationTime { get; set; }

        /// <inheritdoc cref="FileSystemInfo.CreationTimeUtc"/>
        public DateTime CreationTimeUtc { get; set; }

        /// <inheritdoc cref="FileSystemInfo.LastAccessTime"/>
        public DateTime LastAccessTime { get; set; }

        /// <inheritdoc cref="FileSystemInfo.LastAccessTimeUtc"/>
        public DateTime LastAccessTimeUtc { get; set; }

        /// <inheritdoc cref="FileSystemInfo.LastWriteTime"/>
        public DateTime LastWriteTime { get; set; }

        /// <inheritdoc cref="FileSystemInfo.LastWriteTimeUtc"/>
        public DateTime LastWriteTimeUtc { get; set; }
        
        public IDriveInfo? Drive { get; }

        /// <inheritdoc cref="FileSystemInfo.CreateAsSymbolicLink(String)"/>
        public Boolean CreateAsSymbolicLink(String target);

        /// <inheritdoc cref="FileSystemInfo.ResolveLinkTarget(Boolean)"/>
        public IFileSystemInfo? ResolveLinkTarget();

        /// <inheritdoc cref="FileSystemInfo.ResolveLinkTarget(Boolean)"/>
        public IFileSystemInfo? ResolveLinkTarget(Boolean final);

        /// <inheritdoc cref="FileSystemInfo.Delete()"/>
        public void Delete();

        /// <inheritdoc cref="FileSystemInfo.Refresh()"/>
        public void Refresh();
    }
}