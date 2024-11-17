using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using NetExtender.IO.Interfaces;

namespace NetExtender.IO
{
    public partial class FileSystemHandler
    {
        public virtual String[] GetLogicalDrives()
        {
            return Directory.GetLogicalDrives();
        }

        public virtual String GetCurrentDirectory()
        {
            return Directory.GetCurrentDirectory();
        }

        public virtual void SetCurrentDirectory(String path)
        {
            Directory.SetCurrentDirectory(path);
        }

        public virtual String GetDirectoryRoot(String path)
        {
            return Directory.GetDirectoryRoot(path);
        }

        public virtual DirectoryInfo CreateDirectory(String path)
        {
            return Directory.CreateDirectory(path);
        }

        FileSystemInfo IDirectoryHandler.CreateSymbolicLink(String path, String target)
        {
            return CreateSymbolicLink(path, target, FileSystemHandlerType.Directory);
        }

        FileSystemInfo? IDirectoryHandler.ResolveLinkTarget(String path, Boolean target)
        {
            return ResolveLinkTarget(path, target, FileSystemHandlerType.Directory);
        }

        Boolean IDirectoryHandler.Exists([NotNullWhen(true)] String? path)
        {
            return Exists(path, FileSystemHandlerType.Directory);
        }

        DirectoryInfo? IDirectoryHandler.GetParent(String path)
        {
            return GetParent(path, FileSystemHandlerType.Directory);
        }

        DateTime IDirectoryHandler.GetCreationTime(String path)
        {
            return GetCreationTime(path, FileSystemHandlerType.Directory);
        }

        DateTime IDirectoryHandler.GetCreationTimeUtc(String path)
        {
            return GetCreationTimeUtc(path, FileSystemHandlerType.Directory);
        }

        DateTime IDirectoryHandler.GetLastAccessTime(String path)
        {
            return GetLastAccessTime(path, FileSystemHandlerType.Directory);
        }

        DateTime IDirectoryHandler.GetLastAccessTimeUtc(String path)
        {
            return GetLastAccessTimeUtc(path, FileSystemHandlerType.Directory);
        }

        DateTime IDirectoryHandler.GetLastWriteTime(String path)
        {
            return GetLastWriteTime(path, FileSystemHandlerType.Directory);
        }

        DateTime IDirectoryHandler.GetLastWriteTimeUtc(String path)
        {
            return GetLastWriteTimeUtc(path, FileSystemHandlerType.Directory);
        }
        
        void IDirectoryHandler.SetCreationTime(String path, DateTime time)
        {
            SetCreationTime(path, time, FileSystemHandlerType.Directory);
        }

        void IDirectoryHandler.SetCreationTimeUtc(String path, DateTime time)
        {
            SetCreationTimeUtc(path, time, FileSystemHandlerType.Directory);
        }

        void IDirectoryHandler.SetLastAccessTime(String path, DateTime time)
        {
            SetLastAccessTime(path, time, FileSystemHandlerType.Directory);
        }

        void IDirectoryHandler.SetLastAccessTimeUtc(String path, DateTime time)
        {
            SetLastAccessTimeUtc(path, time, FileSystemHandlerType.Directory);
        }

        void IDirectoryHandler.SetLastWriteTime(String path, DateTime time)
        {
            SetLastWriteTime(path, time, FileSystemHandlerType.Directory);
        }

        void IDirectoryHandler.SetLastWriteTimeUtc(String path, DateTime time)
        {
            SetLastWriteTimeUtc(path, time, FileSystemHandlerType.Directory);
        }

        public virtual String[] GetFiles(String path)
        {
            return Directory.GetFiles(path);
        }

        public virtual String[] GetFiles(String path, String pattern)
        {
            return Directory.GetFiles(path, pattern);
        }

        public virtual String[] GetFiles(String path, String pattern, SearchOption options)
        {
            return Directory.GetFiles(path, pattern, options);
        }

        public virtual String[] GetFiles(String path, String pattern, EnumerationOptions options)
        {
            return Directory.GetFiles(path, pattern, options);
        }

        public virtual String[] GetDirectories(String path)
        {
            return Directory.GetDirectories(path);
        }

        public virtual String[] GetDirectories(String path, String pattern)
        {
            return Directory.GetDirectories(path, pattern);
        }

        public virtual String[] GetDirectories(String path, String pattern, SearchOption options)
        {
            return Directory.GetDirectories(path, pattern, options);
        }

        public virtual String[] GetDirectories(String path, String pattern, EnumerationOptions options)
        {
            return Directory.GetDirectories(path, pattern, options);
        }

        public virtual String[] GetFileSystemEntries(String path)
        {
            return Directory.GetFileSystemEntries(path);
        }

        public virtual String[] GetFileSystemEntries(String path, String pattern)
        {
            return Directory.GetFileSystemEntries(path, pattern);
        }

        public virtual String[] GetFileSystemEntries(String path, String pattern, SearchOption options)
        {
            return Directory.GetFileSystemEntries(path, pattern, options);
        }

        public virtual String[] GetFileSystemEntries(String path, String pattern, EnumerationOptions options)
        {
            return Directory.GetFileSystemEntries(path, pattern, options);
        }

        public virtual IEnumerable<String> EnumerateFiles(String path)
        {
            return Directory.EnumerateFiles(path);
        }

        public virtual IEnumerable<String> EnumerateFiles(String path, String pattern)
        {
            return Directory.EnumerateFiles(path, pattern);
        }

        public virtual IEnumerable<String> EnumerateFiles(String path, String pattern, SearchOption options)
        {
            return Directory.EnumerateFiles(path, pattern, options);
        }

        public virtual IEnumerable<String> EnumerateFiles(String path, String pattern, EnumerationOptions options)
        {
            return Directory.EnumerateFiles(path, pattern, options);
        }

        public virtual IEnumerable<String> EnumerateDirectories(String path)
        {
            return Directory.EnumerateDirectories(path);
        }

        public virtual IEnumerable<String> EnumerateDirectories(String path, String pattern)
        {
            return Directory.EnumerateDirectories(path, pattern);
        }

        public virtual IEnumerable<String> EnumerateDirectories(String path, String pattern, SearchOption options)
        {
            return Directory.EnumerateDirectories(path, pattern, options);
        }

        public virtual IEnumerable<String> EnumerateDirectories(String path, String pattern, EnumerationOptions options)
        {
            return Directory.EnumerateDirectories(path, pattern, options);
        }

        public virtual IEnumerable<String> EnumerateFileSystemEntries(String path)
        {
            return Directory.EnumerateFileSystemEntries(path);
        }

        public virtual IEnumerable<String> EnumerateFileSystemEntries(String path, String pattern)
        {
            return Directory.EnumerateFileSystemEntries(path, pattern);
        }

        public virtual IEnumerable<String> EnumerateFileSystemEntries(String path, String pattern, SearchOption options)
        {
            return Directory.EnumerateFileSystemEntries(path, pattern, options);
        }

        public virtual IEnumerable<String> EnumerateFileSystemEntries(String path, String pattern, EnumerationOptions options)
        {
            return Directory.EnumerateFileSystemEntries(path, pattern, options);
        }

        void IDirectoryHandler.Move(String source, String destination)
        {
            Move(source, destination, FileSystemHandlerType.Directory);
        }

        void IFileHandler.Move(String source, String destination)
        {
            Move(source, destination, FileSystemHandlerType.Directory);
        }

        void IDirectoryHandler.Delete(String path)
        {
            Delete(path, FileSystemHandlerType.Directory);
        }

        void IFileHandler.Delete(String path)
        {
            Delete(path, FileSystemHandlerType.Directory);
        }

        void IDirectoryHandler.Delete(String path, Boolean recursive)
        {
            Delete(path, recursive, FileSystemHandlerType.Directory);
        }
    }
}