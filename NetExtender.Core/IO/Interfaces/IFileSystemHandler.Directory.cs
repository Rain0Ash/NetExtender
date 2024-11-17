using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace NetExtender.IO.Interfaces
{
    public interface IDirectoryHandler
    {
        /// <inheritdoc cref="System.IO.Directory.GetLogicalDrives()" />
        public String[] GetLogicalDrives();

        /// <inheritdoc cref="System.IO.Directory.GetCurrentDirectory()" />
        public String GetCurrentDirectory();

        /// <inheritdoc cref="System.IO.Directory.SetCurrentDirectory(System.String)" />
        public void SetCurrentDirectory(String path);

        /// <inheritdoc cref="System.IO.Directory.GetDirectoryRoot(System.String)" />
        public String GetDirectoryRoot(String path);

        /// <inheritdoc cref="System.IO.Directory.CreateDirectory(System.String)" />
        public DirectoryInfo CreateDirectory(String path);

        /// <inheritdoc cref="System.IO.Directory.CreateSymbolicLink(System.String, System.String)" />
        public FileSystemInfo CreateSymbolicLink(String path, String target);

        /// <inheritdoc cref="System.IO.Directory.ResolveLinkTarget(System.String, System.Boolean)" />
        public FileSystemInfo? ResolveLinkTarget(String path, Boolean target);

        /// <inheritdoc cref="System.IO.Directory.Exists(System.String)" />
        public Boolean Exists([NotNullWhen(true)] String? path);

        /// <inheritdoc cref="System.IO.Directory.GetParent(System.String)" />
        public DirectoryInfo? GetParent(String path);

        /// <inheritdoc cref="System.IO.Directory.GetCreationTime(System.String)" />
        public DateTime GetCreationTime(String path);

        /// <inheritdoc cref="System.IO.Directory.GetCreationTimeUtc(System.String)" />
        public DateTime GetCreationTimeUtc(String path);

        /// <inheritdoc cref="System.IO.Directory.GetLastAccessTime(System.String)" />
        public DateTime GetLastAccessTime(String path);

        /// <inheritdoc cref="System.IO.Directory.GetLastAccessTimeUtc(System.String)" />
        public DateTime GetLastAccessTimeUtc(String path);

        /// <inheritdoc cref="System.IO.Directory.GetLastWriteTime(System.String)" />
        public DateTime GetLastWriteTime(String path);

        /// <inheritdoc cref="System.IO.Directory.GetLastWriteTimeUtc(System.String)" />
        public DateTime GetLastWriteTimeUtc(String path);

        /// <inheritdoc cref="System.IO.Directory.SetCreationTime(System.String, System.DateTime)" />
        public void SetCreationTime(String path, DateTime time);

        /// <inheritdoc cref="System.IO.Directory.SetCreationTimeUtc(System.String, System.DateTime)" />
        public void SetCreationTimeUtc(String path, DateTime time);

        /// <inheritdoc cref="System.IO.Directory.SetLastAccessTime(System.String, System.DateTime)" />
        public void SetLastAccessTime(String path, DateTime time);

        /// <inheritdoc cref="System.IO.Directory.SetLastAccessTimeUtc(System.String, System.DateTime)" />
        public void SetLastAccessTimeUtc(String path, DateTime time);

        /// <inheritdoc cref="System.IO.Directory.SetLastWriteTime(System.String, System.DateTime)" />
        public void SetLastWriteTime(String path, DateTime time);

        /// <inheritdoc cref="System.IO.Directory.SetLastWriteTimeUtc(System.String, System.DateTime)" />
        public void SetLastWriteTimeUtc(String path, DateTime time);

        /// <inheritdoc cref="System.IO.Directory.GetFiles(System.String)" />
        public String[] GetFiles(String path);

        /// <inheritdoc cref="System.IO.Directory.GetFiles(System.String, System.String)" />
        public String[] GetFiles(String path, String pattern);

        /// <inheritdoc cref="System.IO.Directory.GetFiles(System.String, System.String, System.IO.SearchOption)" />
        public String[] GetFiles(String path, String pattern, SearchOption options);

        /// <inheritdoc cref="System.IO.Directory.GetFiles(System.String, System.String, System.IO.EnumerationOptions)" />
        public String[] GetFiles(String path, String pattern, EnumerationOptions options);

        /// <inheritdoc cref="System.IO.Directory.GetDirectories(System.String)" />
        public String[] GetDirectories(String path);

        /// <inheritdoc cref="System.IO.Directory.GetDirectories(System.String, System.String)" />
        public String[] GetDirectories(String path, String pattern);

        /// <inheritdoc cref="System.IO.Directory.GetDirectories(System.String, System.String, System.IO.SearchOption)" />
        public String[] GetDirectories(String path, String pattern, SearchOption options);

        /// <inheritdoc cref="System.IO.Directory.GetDirectories(System.String, System.String, System.IO.EnumerationOptions)" />
        public String[] GetDirectories(String path, String pattern, EnumerationOptions options);

        /// <inheritdoc cref="System.IO.Directory.GetFileSystemEntries(System.String)" />
        public String[] GetFileSystemEntries(String path);

        /// <inheritdoc cref="System.IO.Directory.GetFileSystemEntries(System.String, System.String)" />
        public String[] GetFileSystemEntries(String path, String pattern);

        /// <inheritdoc cref="System.IO.Directory.GetFileSystemEntries(System.String, System.String, System.IO.SearchOption)" />
        public String[] GetFileSystemEntries(String path, String pattern, SearchOption options);

        /// <inheritdoc cref="System.IO.Directory.GetFileSystemEntries(System.String, System.String, System.IO.EnumerationOptions)" />
        public String[] GetFileSystemEntries(String path, String pattern, EnumerationOptions options);

        /// <inheritdoc cref="System.IO.Directory.EnumerateFiles(System.String)" />
        public IEnumerable<String> EnumerateFiles(String path);

        /// <inheritdoc cref="System.IO.Directory.EnumerateFiles(System.String, System.String)" />
        public IEnumerable<String> EnumerateFiles(String path, String pattern);

        /// <inheritdoc cref="System.IO.Directory.EnumerateFiles(System.String, System.String, System.IO.SearchOption)" />
        public IEnumerable<String> EnumerateFiles(String path, String pattern, SearchOption options);

        /// <inheritdoc cref="System.IO.Directory.EnumerateFiles(System.String, System.String, System.IO.EnumerationOptions)" />
        public IEnumerable<String> EnumerateFiles(String path, String pattern, EnumerationOptions options);

        /// <inheritdoc cref="System.IO.Directory.EnumerateDirectories(System.String)" />
        public IEnumerable<String> EnumerateDirectories(String path);

        /// <inheritdoc cref="System.IO.Directory.EnumerateDirectories(System.String, System.String)" />
        public IEnumerable<String> EnumerateDirectories(String path, String pattern);

        /// <inheritdoc cref="System.IO.Directory.EnumerateDirectories(System.String, System.String, System.IO.SearchOption)" />
        public IEnumerable<String> EnumerateDirectories(String path, String pattern, SearchOption options);

        /// <inheritdoc cref="System.IO.Directory.EnumerateDirectories(System.String, System.String, System.IO.EnumerationOptions)" />
        public IEnumerable<String> EnumerateDirectories(String path, String pattern, EnumerationOptions options);

        /// <inheritdoc cref="System.IO.Directory.EnumerateFileSystemEntries(System.String)" />
        public IEnumerable<String> EnumerateFileSystemEntries(String path);

        /// <inheritdoc cref="System.IO.Directory.EnumerateFileSystemEntries(System.String, System.String)" />
        public IEnumerable<String> EnumerateFileSystemEntries(String path, String pattern);

        /// <inheritdoc cref="System.IO.Directory.EnumerateFileSystemEntries(System.String, System.String, System.IO.SearchOption)" />
        public IEnumerable<String> EnumerateFileSystemEntries(String path, String pattern, SearchOption options);

        /// <inheritdoc cref="System.IO.Directory.EnumerateFileSystemEntries(System.String, System.String, System.IO.EnumerationOptions)" />
        public IEnumerable<String> EnumerateFileSystemEntries(String path, String pattern, EnumerationOptions options);

        /// <inheritdoc cref="System.IO.Directory.Move(System.String, System.String)" />
        public void Move(String source, String destination);

        /// <inheritdoc cref="System.IO.Directory.Delete(System.String)" />
        public void Delete(String path);

        /// <inheritdoc cref="System.IO.Directory.Delete(System.String, System.Boolean)" />
        public void Delete(String path, Boolean recursive);
    }
}