// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.IO;
using NetExtender.FileSystems.Interfaces;

namespace NetExtender.FileSystems
{
    public partial class FileSystemHandlerWrapper<T>
    {
        public override String[] GetLogicalDrives()
        {
            return FileSystem.Directory.GetLogicalDrives();
        }

        public override String GetCurrentDirectory()
        {
            return FileSystem.Directory.GetCurrentDirectory();
        }

        public override Boolean SetCurrentDirectory(String path)
        {
            return FileSystem.Directory.SetCurrentDirectory(path);
        }

        public override String GetDirectoryRoot(String path)
        {
            return FileSystem.Directory.GetDirectoryRoot(path);
        }

        public override IDirectoryInfo CreateDirectory(String path)
        {
            return FileSystem.Directory.CreateDirectory(path);
        }

        public override String[] GetFileSystemEntries(String path)
        {
            return FileSystem.Directory.GetFileSystemEntries(path);
        }

        public override String[] GetFileSystemEntries(String path, String? pattern)
        {
            return FileSystem.Directory.GetFileSystemEntries(path, pattern);
        }

        public override String[] GetFileSystemEntries(String path, String? pattern, SearchOption option)
        {
            return FileSystem.Directory.GetFileSystemEntries(path, pattern, option);
        }

        public override String[] GetFileSystemEntries(String path, String? pattern, EnumerationOptions options)
        {
            return FileSystem.Directory.GetFileSystemEntries(path, pattern, options);
        }

        public override String[] GetFiles(String path)
        {
            return FileSystem.Directory.GetFiles(path);
        }

        public override String[] GetFiles(String path, String? pattern)
        {
            return FileSystem.Directory.GetFiles(path, pattern);
        }

        public override String[] GetFiles(String path, String? pattern, SearchOption option)
        {
            return FileSystem.Directory.GetFiles(path, pattern, option);
        }

        public override String[] GetFiles(String path, String? pattern, EnumerationOptions options)
        {
            return FileSystem.Directory.GetFiles(path, pattern, options);
        }

        public override String[] GetDirectories(String path)
        {
            return FileSystem.Directory.GetDirectories(path);
        }

        public override String[] GetDirectories(String path, String? pattern)
        {
            return FileSystem.Directory.GetDirectories(path, pattern);
        }

        public override String[] GetDirectories(String path, String? pattern, SearchOption option)
        {
            return FileSystem.Directory.GetDirectories(path, pattern, option);
        }

        public override String[] GetDirectories(String path, String? pattern, EnumerationOptions options)
        {
            return FileSystem.Directory.GetDirectories(path, pattern, options);
        }

        public override IEnumerable<String> EnumerateFileSystemEntries(String path)
        {
            return FileSystem.Directory.EnumerateFileSystemEntries(path);
        }

        public override IEnumerable<String> EnumerateFileSystemEntries(String path, String? pattern)
        {
            return FileSystem.Directory.EnumerateFileSystemEntries(path, pattern);
        }

        public override IEnumerable<String> EnumerateFileSystemEntries(String path, String? pattern, SearchOption option)
        {
            return FileSystem.Directory.EnumerateFileSystemEntries(path, pattern, option);
        }

        public override IEnumerable<String> EnumerateFileSystemEntries(String path, String? pattern, EnumerationOptions options)
        {
            return FileSystem.Directory.EnumerateFileSystemEntries(path, pattern, options);
        }

        public override IEnumerable<String> EnumerateFiles(String path)
        {
            return FileSystem.Directory.EnumerateFiles(path);
        }

        public override IEnumerable<String> EnumerateFiles(String path, String? pattern)
        {
            return FileSystem.Directory.EnumerateFiles(path, pattern);
        }

        public override IEnumerable<String> EnumerateFiles(String path, String? pattern, SearchOption option)
        {
            return FileSystem.Directory.EnumerateFiles(path, pattern, option);
        }

        public override IEnumerable<String> EnumerateFiles(String path, String? pattern, EnumerationOptions options)
        {
            return FileSystem.Directory.EnumerateFiles(path, pattern, options);
        }

        public override IEnumerable<String> EnumerateDirectories(String path)
        {
            return FileSystem.Directory.EnumerateDirectories(path);
        }

        public override IEnumerable<String> EnumerateDirectories(String path, String? pattern)
        {
            return FileSystem.Directory.EnumerateDirectories(path, pattern);
        }

        public override IEnumerable<String> EnumerateDirectories(String path, String? pattern, SearchOption option)
        {
            return FileSystem.Directory.EnumerateDirectories(path, pattern, option);
        }

        public override IEnumerable<String> EnumerateDirectories(String path, String? pattern, EnumerationOptions options)
        {
            return FileSystem.Directory.EnumerateDirectories(path, pattern, options);
        }
    }
}