using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;

namespace NetExtender.IO.Interfaces
{
#pragma warning disable CS0618
    internal interface IUnsafeFileSystemHandler : IFileSystemHandler
    {
        public static IUnsafeFileSystemHandler Default
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return FileSystemHandler.Default.Handler;
            }
        }
    }
#pragma warning restore CS0618
    
    [Obsolete($"Use {nameof(IFileSystemHandler)} as specified interface {nameof(IPathHandler)}; {nameof(IFileHandler)}; {nameof(IDirectoryHandler)}.")]
    public interface IFileSystemHandler : IPathHandler, IFileHandler, IDirectoryHandler
    {
        #region FileSystem

        public new FileSystemInfo CreateSymbolicLink(String path, String target);
        public new FileSystemInfo? ResolveLinkTarget(String path, Boolean target);
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

        [SupportedOSPlatform("windows")]
        public new void Encrypt(String path);

        [SupportedOSPlatform("windows")]
        public new void Decrypt(String path);

        #endregion

        #region Directory

        public new DirectoryInfo? GetParent(String path);
        public new void Delete(String path, Boolean recursive);

        #endregion
    }
}