// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace NetExtender.FileSystems.Interfaces
{
#pragma warning disable CS0618
    public interface IUnsafeFileSystemHandler : IFileSystemHandler
    {
    }
#pragma warning restore CS0618
    
    //TODO: Change obsolete messages
    [Obsolete($"Use {nameof(IFileSystemHandler)} as specified interface {nameof(IPathHandler)}; {nameof(IFileHandler)}; {nameof(IDirectoryHandler)}.")]
    public interface IFileSystemHandler : IPathHandler, ILinkHandler, IFileHandler, IDirectoryHandler, IDriveHandler, IEnvironmentHandler, IFileSystem
    {
        public new Object SyncRoot { get; }
        public new Boolean IsSynchronized { get; }
        public new Guid Id { get; }
        public new String? Name { get; }
        public new DateTime CreationTime { get; }
        public new DateTime CreationTimeUtc { get; }
        public new Boolean IsReal { get; }
        public new StringComparer Comparer { get; }
        public new Boolean IsCaseSensitive { get; }
        
        #region FileSystem

        public new IFileSystemInfo CreateSymbolicLink(String path, String target);
        public new IFileSystemInfo? ResolveLinkTarget(String path, Boolean target);
        public new Boolean Exists([NotNullWhen(true)] String? path);
        public new DateTime GetCreationTime(String path);
        public new DateTime GetCreationTimeUtc(String path);
        public new DateTime GetLastAccessTime(String path);
        public new DateTime GetLastAccessTimeUtc(String path);
        public new DateTime GetLastWriteTime(String path);
        public new DateTime GetLastWriteTimeUtc(String path);
        public new void SetCreationTime(String path, DateTime time);
        public new void SetCreationTimeUtc(String path, DateTime time);
        public new void SetLastAccessTime(String path, DateTime time);
        public new void SetLastAccessTimeUtc(String path, DateTime time);
        public new void SetLastWriteTime(String path, DateTime time);
        public new void SetLastWriteTimeUtc(String path, DateTime time);
        public new void Move(String source, String destination);
        public new void Delete(String path);

        #endregion

        #region File

        public new FileAttributes GetAttributes(String path);
        public new void SetAttributes(String path, FileAttributes attributes);
        public new FileStream Create(String path);
        public new void Move(String source, String destination, Boolean overwrite);
        public new void Copy(String source, String destination);
        public new void Copy(String source, String destination, Boolean overwrite);
        public new void Replace(String source, String destination, String? backup);
        public new void Replace(String source, String destination, String? backup, Boolean suppress);
        public new Boolean Encrypt(String path);
        public new Boolean Decrypt(String path);

        #endregion

        #region Directory

        public new String[]? GetLogicalDrives();
        public new IDirectoryInfo? GetParent(String path);
        public new void Delete(String path, Boolean recursive);

        #endregion
    }
}