using System;
using System.IO;

namespace NetExtender.FileSystems.Interfaces
{
    internal interface ILinkEntry : ILinkInfo, IFileSystemEntry
    {
        /// <inheritdoc cref="FileInfo.Directory"/>
        public new IDirectoryEntry? Directory { get; }
    }
    
    public interface ILinkInfo : IFileSystemInfo, IEquatable<ILinkInfo>
    {
        /// <inheritdoc cref="FileInfo.DirectoryName"/>
        public String? DirectoryName { get; }

        /// <inheritdoc cref="FileInfo.Directory"/>
        public IDirectoryInfo? Directory { get; }
        
        public void Create();
    }
}