using System;
using System.Collections.Generic;
using System.IO;

namespace NetExtender.FileSystems.Interfaces
{
    internal interface IDirectoryEntry : IFileSystemEntry, IDirectoryInfo
    {
        /// <inheritdoc cref="DirectoryInfo.Root"/>
        public new IDirectoryEntry Root { get; }
        
        /// <inheritdoc cref="DirectoryInfo.Parent"/>
        public new IDirectoryEntry? Parent { get; }
        
        /// <inheritdoc cref="DirectoryInfo.GetFileSystemInfos()"/>
        public new IFileSystemEntry[] GetFileSystemInfos();
        
        /// <inheritdoc cref="DirectoryInfo.GetFileSystemInfos(System.String)"/>
        public new IFileSystemEntry[] GetFileSystemInfos(String? pattern);

        /// <inheritdoc cref="DirectoryInfo.GetFileSystemInfos(System.String, System.IO.SearchOption)"/>
        public new IFileSystemEntry[] GetFileSystemInfos(String? pattern, SearchOption option);

        /// <inheritdoc cref="DirectoryInfo.GetFileSystemInfos(System.String, System.IO.EnumerationOptions)"/>
        public new IFileSystemEntry[] GetFileSystemInfos(String? pattern, EnumerationOptions options);

        /// <inheritdoc cref="DirectoryInfo.GetFiles()"/>
        public new IFileEntry[] GetFiles();
        
        /// <inheritdoc cref="DirectoryInfo.GetFiles(System.String)"/>
        public new IFileEntry[] GetFiles(String? pattern);
        
        /// <inheritdoc cref="DirectoryInfo.GetFiles(System.String, System.IO.SearchOption)"/>
        public new IFileEntry[] GetFiles(String? pattern, SearchOption option);
        
        /// <inheritdoc cref="DirectoryInfo.GetFiles(System.String, System.IO.EnumerationOptions)"/>
        public new IFileEntry[] GetFiles(String? pattern, EnumerationOptions options);

        /// <inheritdoc cref="DirectoryInfo.GetDirectories()"/>
        public new IDirectoryEntry[] GetDirectories();

        /// <inheritdoc cref="DirectoryInfo.GetDirectories(System.String)"/>
        public new IDirectoryEntry[] GetDirectories(String? pattern);

        /// <inheritdoc cref="DirectoryInfo.GetDirectories(System.String, System.IO.SearchOption)"/>
        public new IDirectoryEntry[] GetDirectories(String? pattern, SearchOption option);

        /// <inheritdoc cref="DirectoryInfo.GetDirectories(System.String, System.IO.EnumerationOptions)"/>
        public new IDirectoryEntry[] GetDirectories(String? pattern, EnumerationOptions options);

        /// <inheritdoc cref="DirectoryInfo.EnumerateFileSystemInfos()"/>
        public new IEnumerable<IFileSystemEntry> EnumerateFileSystemInfos();

        /// <inheritdoc cref="DirectoryInfo.EnumerateFileSystemInfos(System.String)"/>
        public new IEnumerable<IFileSystemEntry> EnumerateFileSystemInfos(String? pattern);

        /// <inheritdoc cref="DirectoryInfo.EnumerateFileSystemInfos(System.String, System.IO.SearchOption)"/>
        public new IEnumerable<IFileSystemEntry> EnumerateFileSystemInfos(String? pattern, SearchOption option);

        /// <inheritdoc cref="DirectoryInfo.EnumerateFileSystemInfos(System.String, System.IO.EnumerationOptions)"/>
        public new IEnumerable<IFileSystemEntry> EnumerateFileSystemInfos(String? pattern, EnumerationOptions options);

        /// <inheritdoc cref="DirectoryInfo.EnumerateFiles()"/>
        public new IEnumerable<IFileEntry> EnumerateFiles();

        /// <inheritdoc cref="DirectoryInfo.EnumerateFiles(System.String)"/>
        public new IEnumerable<IFileEntry> EnumerateFiles(String? pattern);

        /// <inheritdoc cref="DirectoryInfo.EnumerateFiles(System.String, System.IO.SearchOption)"/>
        public new IEnumerable<IFileEntry> EnumerateFiles(String? pattern, SearchOption option);

        /// <inheritdoc cref="DirectoryInfo.EnumerateFiles(System.String, System.IO.EnumerationOptions)"/>
        public new IEnumerable<IFileEntry> EnumerateFiles(String? pattern, EnumerationOptions options);

        /// <inheritdoc cref="DirectoryInfo.EnumerateDirectories()"/>
        public new IEnumerable<IDirectoryEntry> EnumerateDirectories();

        /// <inheritdoc cref="DirectoryInfo.EnumerateDirectories(System.String)"/>
        public new IEnumerable<IDirectoryEntry> EnumerateDirectories(String? pattern);

        /// <inheritdoc cref="DirectoryInfo.EnumerateDirectories(System.String, System.IO.SearchOption)"/>
        public new IEnumerable<IDirectoryEntry> EnumerateDirectories(String? pattern, SearchOption option);

        /// <inheritdoc cref="DirectoryInfo.EnumerateDirectories(System.String, System.IO.EnumerationOptions)"/>
        public new IEnumerable<IDirectoryEntry> EnumerateDirectories(String? pattern, EnumerationOptions options);
        
        /// <inheritdoc cref="DirectoryInfo.CreateSubdirectory(System.String)"/>
        public new IDirectoryEntry CreateSubdirectory(String path);
    }
    
    public interface IDirectoryInfo : IFileSystemInfo, IEquatable<DirectoryInfo>, IEquatable<IDirectoryInfo>
    {
        public new DirectoryInfo? Info { get; }
        
        /// <inheritdoc cref="DirectoryInfo.Root"/>
        public IDirectoryInfo Root { get; }
        
        /// <inheritdoc cref="DirectoryInfo.Parent"/>
        public IDirectoryInfo? Parent { get; }
        
        /// <inheritdoc cref="DirectoryInfo.GetFileSystemInfos()"/>
        public IFileSystemInfo[] GetFileSystemInfos();
        
        /// <inheritdoc cref="DirectoryInfo.GetFileSystemInfos(System.String)"/>
        public IFileSystemInfo[] GetFileSystemInfos(String? pattern);

        /// <inheritdoc cref="DirectoryInfo.GetFileSystemInfos(System.String, System.IO.SearchOption)"/>
        public IFileSystemInfo[] GetFileSystemInfos(String? pattern, SearchOption searchOption);

        /// <inheritdoc cref="DirectoryInfo.GetFileSystemInfos(System.String, System.IO.EnumerationOptions)"/>
        public IFileSystemInfo[] GetFileSystemInfos(String? pattern, EnumerationOptions enumerationOptions);

        /// <inheritdoc cref="DirectoryInfo.GetFiles()"/>
        public IFileInfo[] GetFiles();
        
        /// <inheritdoc cref="DirectoryInfo.GetFiles(System.String)"/>
        public IFileInfo[] GetFiles(String? pattern);
        
        /// <inheritdoc cref="DirectoryInfo.GetFiles(System.String, System.IO.SearchOption)"/>
        public IFileInfo[] GetFiles(String? pattern, SearchOption option);
        
        /// <inheritdoc cref="DirectoryInfo.GetFiles(System.String, System.IO.EnumerationOptions)"/>
        public IFileInfo[] GetFiles(String? pattern, EnumerationOptions options);

        /// <inheritdoc cref="DirectoryInfo.GetDirectories()"/>
        public IDirectoryInfo[] GetDirectories();

        /// <inheritdoc cref="DirectoryInfo.GetDirectories(System.String)"/>
        public IDirectoryInfo[] GetDirectories(String? pattern);

        /// <inheritdoc cref="DirectoryInfo.GetDirectories(System.String, System.IO.SearchOption)"/>
        public IDirectoryInfo[] GetDirectories(String? pattern, SearchOption option);

        /// <inheritdoc cref="DirectoryInfo.GetDirectories(System.String, System.IO.EnumerationOptions)"/>
        public IDirectoryInfo[] GetDirectories(String? pattern, EnumerationOptions options);

        /// <inheritdoc cref="DirectoryInfo.EnumerateFileSystemInfos()"/>
        public IEnumerable<IFileSystemInfo> EnumerateFileSystemInfos();

        /// <inheritdoc cref="DirectoryInfo.EnumerateFileSystemInfos(System.String)"/>
        public IEnumerable<IFileSystemInfo> EnumerateFileSystemInfos(String? pattern);

        /// <inheritdoc cref="DirectoryInfo.EnumerateFileSystemInfos(System.String, System.IO.SearchOption)"/>
        public IEnumerable<IFileSystemInfo> EnumerateFileSystemInfos(String? pattern, SearchOption option);

        /// <inheritdoc cref="DirectoryInfo.EnumerateFileSystemInfos(System.String, System.IO.EnumerationOptions)"/>
        public IEnumerable<IFileSystemInfo> EnumerateFileSystemInfos(String? pattern, EnumerationOptions options);

        /// <inheritdoc cref="DirectoryInfo.EnumerateFiles()"/>
        public IEnumerable<IFileInfo> EnumerateFiles();

        /// <inheritdoc cref="DirectoryInfo.EnumerateFiles(System.String)"/>
        public IEnumerable<IFileInfo> EnumerateFiles(String? pattern);

        /// <inheritdoc cref="DirectoryInfo.EnumerateFiles(System.String, System.IO.SearchOption)"/>
        public IEnumerable<IFileInfo> EnumerateFiles(String? pattern, SearchOption option);

        /// <inheritdoc cref="DirectoryInfo.EnumerateFiles(System.String, System.IO.EnumerationOptions)"/>
        public IEnumerable<IFileInfo> EnumerateFiles(String? pattern, EnumerationOptions options);

        /// <inheritdoc cref="DirectoryInfo.EnumerateDirectories()"/>
        public IEnumerable<IDirectoryInfo> EnumerateDirectories();

        /// <inheritdoc cref="DirectoryInfo.EnumerateDirectories(System.String)"/>
        public IEnumerable<IDirectoryInfo> EnumerateDirectories(String? pattern);

        /// <inheritdoc cref="DirectoryInfo.EnumerateDirectories(System.String, System.IO.SearchOption)"/>
        public IEnumerable<IDirectoryInfo> EnumerateDirectories(String? pattern, SearchOption option);

        /// <inheritdoc cref="DirectoryInfo.EnumerateDirectories(System.String, System.IO.EnumerationOptions)"/>
        public IEnumerable<IDirectoryInfo> EnumerateDirectories(String? pattern, EnumerationOptions options);

        /// <inheritdoc cref="DirectoryInfo.MoveTo(System.String)"/>
        public void MoveTo(String destination);

        /// <inheritdoc cref="DirectoryInfo.Create()"/>
        public void Create();
        
        /// <inheritdoc cref="DirectoryInfo.CreateSubdirectory(System.String)"/>
        public IDirectoryInfo CreateSubdirectory(String path);

        /// <inheritdoc cref="DirectoryInfo.Delete(System.Boolean)"/>
        public void Delete(Boolean recursive);
    }
}