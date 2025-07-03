// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.IO;
using NetExtender.FileSystems.Interfaces;

namespace NetExtender.FileSystems
{
    public partial class FileSystemIntercept<T, TInfo>
    {
        public override String[] GetLogicalDrives()
        {
            return Interceptor.Intercept(this, default, Directory.GetLogicalDrives);
        }

        public override String GetCurrentDirectory()
        {
            return Interceptor.Intercept(this, default, Directory.GetCurrentDirectory);
        }

        public override Boolean SetCurrentDirectory(String path)
        {
            return Interceptor.Intercept(this, default, Directory.SetCurrentDirectory, path);
        }

        public override String GetDirectoryRoot(String path)
        {
            return Interceptor.Intercept(this, default, Directory.GetDirectoryRoot, path);
        }

        public override IDirectoryInfo CreateDirectory(String path)
        {
            return Interceptor.Intercept(this, default, Directory.CreateDirectory, path);
        }

        public override String[] GetFiles(String path)
        {
            return Interceptor.Intercept(this, default, Directory.GetFiles, path);
        }

        public override String[] GetFiles(String path, String? pattern)
        {
            return Interceptor.Intercept(this, default, Directory.GetFiles, path, pattern);
        }

        public override String[] GetFiles(String path, String? pattern, SearchOption option)
        {
            return Interceptor.Intercept(this, default, Directory.GetFiles, path, pattern, option);
        }

        public override String[] GetFiles(String path, String? pattern, EnumerationOptions options)
        {
            return Interceptor.Intercept(this, default, Directory.GetFiles, path, pattern, options);
        }

        public override String[] GetDirectories(String path)
        {
            return Interceptor.Intercept(this, default, Directory.GetDirectories, path);
        }

        public override String[] GetDirectories(String path, String? pattern)
        {
            return Interceptor.Intercept(this, default, Directory.GetDirectories, path, pattern);
        }

        public override String[] GetDirectories(String path, String? pattern, SearchOption option)
        {
            return Interceptor.Intercept(this, default, Directory.GetDirectories, path, pattern, option);
        }

        public override String[] GetDirectories(String path, String? pattern, EnumerationOptions options)
        {
            return Interceptor.Intercept(this, default, Directory.GetDirectories, path, pattern, options);
        }

        public override String[] GetFileSystemEntries(String path)
        {
            return Interceptor.Intercept(this, default, Directory.GetFileSystemEntries, path);
        }

        public override String[] GetFileSystemEntries(String path, String? pattern)
        {
            return Interceptor.Intercept(this, default, Directory.GetFileSystemEntries, path, pattern);
        }

        public override String[] GetFileSystemEntries(String path, String? pattern, SearchOption option)
        {
            return Interceptor.Intercept(this, default, Directory.GetFileSystemEntries, path, pattern, option);
        }

        public override String[] GetFileSystemEntries(String path, String? pattern, EnumerationOptions options)
        {
            return Interceptor.Intercept(this, default, Directory.GetFileSystemEntries, path, pattern, options);
        }

        public override IEnumerable<String> EnumerateFiles(String path)
        {
            return Interceptor.Intercept(this, default, Directory.EnumerateFiles, path);
        }

        public override IEnumerable<String> EnumerateFiles(String path, String? pattern)
        {
            return Interceptor.Intercept(this, default, Directory.EnumerateFiles, path, pattern);
        }

        public override IEnumerable<String> EnumerateFiles(String path, String? pattern, SearchOption option)
        {
            return Interceptor.Intercept(this, default, Directory.EnumerateFiles, path, pattern, option);
        }

        public override IEnumerable<String> EnumerateFiles(String path, String? pattern, EnumerationOptions options)
        {
            return Interceptor.Intercept(this, default, Directory.EnumerateFiles, path, pattern, options);
        }

        public override IEnumerable<String> EnumerateDirectories(String path)
        {
            return Interceptor.Intercept(this, default, Directory.EnumerateDirectories, path);
        }

        public override IEnumerable<String> EnumerateDirectories(String path, String? pattern)
        {
            return Interceptor.Intercept(this, default, Directory.EnumerateDirectories, path, pattern);
        }

        public override IEnumerable<String> EnumerateDirectories(String path, String? pattern, SearchOption option)
        {
            return Interceptor.Intercept(this, default, Directory.EnumerateDirectories, path, pattern, option);
        }

        public override IEnumerable<String> EnumerateDirectories(String path, String? pattern, EnumerationOptions options)
        {
            return Interceptor.Intercept(this, default, Directory.EnumerateDirectories, path, pattern, options);
        }

        public override IEnumerable<String> EnumerateFileSystemEntries(String path)
        {
            return Interceptor.Intercept(this, default, Directory.EnumerateFileSystemEntries, path);
        }

        public override IEnumerable<String> EnumerateFileSystemEntries(String path, String? pattern)
        {
            return Interceptor.Intercept(this, default, Directory.EnumerateFileSystemEntries, path, pattern);
        }

        public override IEnumerable<String> EnumerateFileSystemEntries(String path, String? pattern, SearchOption option)
        {
            return Interceptor.Intercept(this, default, Directory.EnumerateFileSystemEntries, path, pattern, option);
        }

        public override IEnumerable<String> EnumerateFileSystemEntries(String path, String? pattern, EnumerationOptions options)
        {
            return Interceptor.Intercept(this, default, Directory.EnumerateFileSystemEntries, path, pattern, options);
        }
    }
}