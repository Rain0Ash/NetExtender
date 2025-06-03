// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using NetExtender.FileSystems.Interfaces;

namespace NetExtender.FileSystems
{
    public partial class FileSystemHandler
    {
        public virtual String[] GetLogicalDrives()
        {
            return System.IO.Directory.GetLogicalDrives();
        }

        public virtual String GetCurrentDirectory()
        {
            return System.IO.Directory.GetCurrentDirectory();
        }

        public virtual Boolean SetCurrentDirectory(String path)
        {
            if (String.IsNullOrEmpty(path))
            {
                return false;
            }

            System.IO.Directory.SetCurrentDirectory(path);
            return true;
        }

        public virtual String GetDirectoryRoot(String path)
        {
            return System.IO.Directory.GetDirectoryRoot(path);
        }

        public virtual IDirectoryInfo CreateDirectory(String path)
        {
            return FileSystemInfoWrapper.Create(System.IO.Directory.CreateDirectory(path));
        }

        IFileSystemInfo IDirectoryHandler.CreateSymbolicLink(String path, String target)
        {
            return CreateSymbolicLink(path, target, FileSystemHandlerType.Directory);
        }

        IFileSystemInfo? IDirectoryHandler.ResolveLinkTarget(String path, Boolean target)
        {
            return ResolveLinkTarget(path, target, FileSystemHandlerType.Directory);
        }

        Boolean IDirectoryHandler.Exists([NotNullWhen(true)] String? path)
        {
            return Exists(path, FileSystemHandlerType.Directory);
        }

        IDirectoryInfo? IDirectoryHandler.GetParent(String path)
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

        public virtual String[] GetFileSystemEntries(String path)
        {
            return System.IO.Directory.GetFileSystemEntries(path);
        }

        public virtual String[] GetFileSystemEntries(String path, String? pattern)
        {
            return System.IO.Directory.GetFileSystemEntries(path, pattern ?? AnyPattern);
        }

        public virtual String[] GetFileSystemEntries(String path, String? pattern, SearchOption option)
        {
            return System.IO.Directory.GetFileSystemEntries(path, pattern ?? AnyPattern, option);
        }

        public virtual String[] GetFileSystemEntries(String path, String? pattern, EnumerationOptions options)
        {
            return System.IO.Directory.GetFileSystemEntries(path, pattern ?? AnyPattern, options);
        }

        public virtual String[] GetFiles(String path)
        {
            return System.IO.Directory.GetFiles(path);
        }

        public virtual String[] GetFiles(String path, String? pattern)
        {
            return System.IO.Directory.GetFiles(path, pattern ?? AnyPattern);
        }

        public virtual String[] GetFiles(String path, String? pattern, SearchOption option)
        {
            return System.IO.Directory.GetFiles(path, pattern ?? AnyPattern, option);
        }

        public virtual String[] GetFiles(String path, String? pattern, EnumerationOptions options)
        {
            return System.IO.Directory.GetFiles(path, pattern ?? AnyPattern, options);
        }

        public virtual String[] GetDirectories(String path)
        {
            return System.IO.Directory.GetDirectories(path);
        }

        public virtual String[] GetDirectories(String path, String? pattern)
        {
            return System.IO.Directory.GetDirectories(path, pattern ?? AnyPattern);
        }

        public virtual String[] GetDirectories(String path, String? pattern, SearchOption option)
        {
            return System.IO.Directory.GetDirectories(path, pattern ?? AnyPattern, option);
        }

        public virtual String[] GetDirectories(String path, String? pattern, EnumerationOptions options)
        {
            return System.IO.Directory.GetDirectories(path, pattern ?? AnyPattern, options);
        }

        public virtual IEnumerable<String> EnumerateFileSystemEntries(String path)
        {
            return System.IO.Directory.EnumerateFileSystemEntries(path);
        }

        public virtual IEnumerable<String> EnumerateFileSystemEntries(String path, String? pattern)
        {
            return System.IO.Directory.EnumerateFileSystemEntries(path, pattern ?? AnyPattern);
        }

        public virtual IEnumerable<String> EnumerateFileSystemEntries(String path, String? pattern, SearchOption option)
        {
            return System.IO.Directory.EnumerateFileSystemEntries(path, pattern ?? AnyPattern, option);
        }

        public virtual IEnumerable<String> EnumerateFileSystemEntries(String path, String? pattern, EnumerationOptions options)
        {
            return System.IO.Directory.EnumerateFileSystemEntries(path, pattern ?? AnyPattern, options);
        }

        public virtual IEnumerable<String> EnumerateFiles(String path)
        {
            return System.IO.Directory.EnumerateFiles(path);
        }

        public virtual IEnumerable<String> EnumerateFiles(String path, String? pattern)
        {
            return System.IO.Directory.EnumerateFiles(path, pattern ?? AnyPattern);
        }

        public virtual IEnumerable<String> EnumerateFiles(String path, String? pattern, SearchOption option)
        {
            return System.IO.Directory.EnumerateFiles(path, pattern ?? AnyPattern, option);
        }

        public virtual IEnumerable<String> EnumerateFiles(String path, String? pattern, EnumerationOptions options)
        {
            return System.IO.Directory.EnumerateFiles(path, pattern ?? AnyPattern, options);
        }

        public virtual IEnumerable<String> EnumerateDirectories(String path)
        {
            return System.IO.Directory.EnumerateDirectories(path);
        }

        public virtual IEnumerable<String> EnumerateDirectories(String path, String? pattern)
        {
            return System.IO.Directory.EnumerateDirectories(path, pattern ?? AnyPattern);
        }

        public virtual IEnumerable<String> EnumerateDirectories(String path, String? pattern, SearchOption option)
        {
            return System.IO.Directory.EnumerateDirectories(path, pattern ?? AnyPattern, option);
        }

        public virtual IEnumerable<String> EnumerateDirectories(String path, String? pattern, EnumerationOptions options)
        {
            return System.IO.Directory.EnumerateDirectories(path, pattern ?? AnyPattern, options);
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