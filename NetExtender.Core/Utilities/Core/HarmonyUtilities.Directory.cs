using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using NetExtender.Types.Interception;
using NetExtender.Types.Interception.Interfaces;

namespace NetExtender.Utilities.Core
{
    public static partial class HarmonyUtilities
    {
        [ReflectionSignature]
        public static class Directory
        {
            public static IInterceptDirectoryHandler Interceptor { get; }

            static Directory()
            {
                Interceptor = new HarmonyFileSystemIntercept(typeof(Directory));
            }

            /// <inheritdoc cref="IInterceptDirectoryHandler.GetLogicalDrives()" />
            public static String[] GetLogicalDrives()
            {
                return Interceptor.GetLogicalDrives();
            }

            /// <inheritdoc cref="IInterceptDirectoryHandler.GetCurrentDirectory()" />
            public static String GetCurrentDirectory()
            {
                return Interceptor.GetCurrentDirectory();
            }

            /// <inheritdoc cref="IInterceptDirectoryHandler.SetCurrentDirectory(System.String)" />
            public static void SetCurrentDirectory(String path)
            {
                Interceptor.SetCurrentDirectory(path);
            }

            /// <inheritdoc cref="IInterceptDirectoryHandler.GetDirectoryRoot(System.String)" />
            public static String GetDirectoryRoot(String path)
            {
                return Interceptor.GetDirectoryRoot(path);
            }

            /// <inheritdoc cref="IInterceptDirectoryHandler.CreateDirectory(System.String)" />
            public static DirectoryInfo CreateDirectory(String path)
            {
                return Interceptor.CreateDirectory(path);
            }

            /// <inheritdoc cref="IInterceptDirectoryHandler.CreateSymbolicLink(System.String, System.String)" />
            public static FileSystemInfo CreateSymbolicLink(String path, String pathToTarget)
            {
                return Interceptor.CreateSymbolicLink(path, pathToTarget);
            }

            /// <inheritdoc cref="IInterceptDirectoryHandler.ResolveLinkTarget(System.String, System.Boolean)" />
            public static FileSystemInfo? ResolveLinkTarget(String linkPath, Boolean returnFinalTarget)
            {
                return Interceptor.ResolveLinkTarget(linkPath, returnFinalTarget);
            }

            /// <inheritdoc cref="IInterceptDirectoryHandler.Exists(System.String)" />
            public static Boolean Exists([NotNullWhen(true)] String? path)
            {
                return Interceptor.Exists(path);
            }

            /// <inheritdoc cref="IInterceptDirectoryHandler.GetParent(System.String)" />
            public static DirectoryInfo? GetParent(String path)
            {
                return Interceptor.GetParent(path);
            }

            /// <inheritdoc cref="IInterceptDirectoryHandler.GetCreationTime(System.String)" />
            public static DateTime GetCreationTime(String path)
            {
                return Interceptor.GetCreationTime(path);
            }

            /// <inheritdoc cref="IInterceptDirectoryHandler.GetCreationTimeUtc(System.String)" />
            public static DateTime GetCreationTimeUtc(String path)
            {
                return Interceptor.GetCreationTimeUtc(path);
            }

            /// <inheritdoc cref="IInterceptDirectoryHandler.GetLastAccessTime(System.String)" />
            public static DateTime GetLastAccessTime(String path)
            {
                return Interceptor.GetLastAccessTime(path);
            }

            /// <inheritdoc cref="IInterceptDirectoryHandler.GetLastAccessTimeUtc(System.String)" />
            public static DateTime GetLastAccessTimeUtc(String path)
            {
                return Interceptor.GetLastAccessTimeUtc(path);
            }

            /// <inheritdoc cref="IInterceptDirectoryHandler.GetLastWriteTime(System.String)" />
            public static DateTime GetLastWriteTime(String path)
            {
                return Interceptor.GetLastWriteTime(path);
            }

            /// <inheritdoc cref="IInterceptDirectoryHandler.GetLastWriteTimeUtc(System.String)" />
            public static DateTime GetLastWriteTimeUtc(String path)
            {
                return Interceptor.GetLastWriteTimeUtc(path);
            }

            /// <inheritdoc cref="IInterceptDirectoryHandler.SetCreationTime(System.String, System.DateTime)" />
            public static void SetCreationTime(String path, DateTime creationTime)
            {
                Interceptor.SetCreationTime(path, creationTime);
            }

            /// <inheritdoc cref="IInterceptDirectoryHandler.SetCreationTimeUtc(System.String, System.DateTime)" />
            public static void SetCreationTimeUtc(String path, DateTime creationTimeUtc)
            {
                Interceptor.SetCreationTimeUtc(path, creationTimeUtc);
            }

            /// <inheritdoc cref="IInterceptDirectoryHandler.SetLastAccessTime(System.String, System.DateTime)" />
            public static void SetLastAccessTime(String path, DateTime lastAccessTime)
            {
                Interceptor.SetLastAccessTime(path, lastAccessTime);
            }

            /// <inheritdoc cref="IInterceptDirectoryHandler.SetLastAccessTimeUtc(System.String, System.DateTime)" />
            public static void SetLastAccessTimeUtc(String path, DateTime lastAccessTimeUtc)
            {
                Interceptor.SetLastAccessTimeUtc(path, lastAccessTimeUtc);
            }

            /// <inheritdoc cref="IInterceptDirectoryHandler.SetLastWriteTime(System.String, System.DateTime)" />
            public static void SetLastWriteTime(String path, DateTime lastWriteTime)
            {
                Interceptor.SetLastWriteTime(path, lastWriteTime);
            }

            /// <inheritdoc cref="IInterceptDirectoryHandler.SetLastWriteTimeUtc(System.String, System.DateTime)" />
            public static void SetLastWriteTimeUtc(String path, DateTime lastWriteTimeUtc)
            {
                Interceptor.SetLastWriteTimeUtc(path, lastWriteTimeUtc);
            }

            /// <inheritdoc cref="IInterceptDirectoryHandler.GetFiles(System.String)" />
            public static String[] GetFiles(String path)
            {
                return Interceptor.GetFiles(path);
            }

            /// <inheritdoc cref="IInterceptDirectoryHandler.GetFiles(System.String, System.String)" />
            public static String[] GetFiles(String path, String searchPattern)
            {
                return Interceptor.GetFiles(path, searchPattern);
            }

            /// <inheritdoc cref="IInterceptDirectoryHandler.GetFiles(System.String, System.String, System.IO.SearchOption)" />
            public static String[] GetFiles(String path, String searchPattern, SearchOption searchOption)
            {
                return Interceptor.GetFiles(path, searchPattern, searchOption);
            }

            /// <inheritdoc cref="IInterceptDirectoryHandler.GetFiles(System.String, System.String, System.IO.EnumerationOptions)" />
            public static String[] GetFiles(String path, String searchPattern, EnumerationOptions enumerationOptions)
            {
                return Interceptor.GetFiles(path, searchPattern, enumerationOptions);
            }

            /// <inheritdoc cref="IInterceptDirectoryHandler.GetDirectories(System.String)" />
            public static String[] GetDirectories(String path)
            {
                return Interceptor.GetDirectories(path);
            }

            /// <inheritdoc cref="IInterceptDirectoryHandler.GetDirectories(System.String, System.String)" />
            public static String[] GetDirectories(String path, String searchPattern)
            {
                return Interceptor.GetDirectories(path, searchPattern);
            }

            /// <inheritdoc cref="IInterceptDirectoryHandler.GetDirectories(System.String, System.String, System.IO.SearchOption)" />
            public static String[] GetDirectories(String path, String searchPattern, SearchOption searchOption)
            {
                return Interceptor.GetDirectories(path, searchPattern, searchOption);
            }

            /// <inheritdoc cref="IInterceptDirectoryHandler.GetDirectories(System.String, System.String, System.IO.EnumerationOptions)" />
            public static String[] GetDirectories(String path, String searchPattern, EnumerationOptions enumerationOptions)
            {
                return Interceptor.GetDirectories(path, searchPattern, enumerationOptions);
            }

            /// <inheritdoc cref="IInterceptDirectoryHandler.GetFileSystemEntries(System.String)" />
            public static String[] GetFileSystemEntries(String path)
            {
                return Interceptor.GetFileSystemEntries(path);
            }

            /// <inheritdoc cref="IInterceptDirectoryHandler.GetFileSystemEntries(System.String, System.String)" />
            public static String[] GetFileSystemEntries(String path, String searchPattern)
            {
                return Interceptor.GetFileSystemEntries(path, searchPattern);
            }

            /// <inheritdoc cref="IInterceptDirectoryHandler.GetFileSystemEntries(System.String, System.String, System.IO.SearchOption)" />
            public static String[] GetFileSystemEntries(String path, String searchPattern, SearchOption searchOption)
            {
                return Interceptor.GetFileSystemEntries(path, searchPattern, searchOption);
            }

            /// <inheritdoc cref="IInterceptDirectoryHandler.GetFileSystemEntries(System.String, System.String, System.IO.EnumerationOptions)" />
            public static String[] GetFileSystemEntries(String path, String searchPattern, EnumerationOptions enumerationOptions)
            {
                return Interceptor.GetFileSystemEntries(path, searchPattern, enumerationOptions);
            }

            /// <inheritdoc cref="IInterceptDirectoryHandler.EnumerateFiles(System.String)" />
            public static IEnumerable<String> EnumerateFiles(String path)
            {
                return Interceptor.EnumerateFiles(path);
            }

            /// <inheritdoc cref="IInterceptDirectoryHandler.EnumerateFiles(System.String, System.String)" />
            public static IEnumerable<String> EnumerateFiles(String path, String searchPattern)
            {
                return Interceptor.EnumerateFiles(path, searchPattern);
            }

            /// <inheritdoc cref="IInterceptDirectoryHandler.EnumerateFiles(System.String, System.String, System.IO.SearchOption)" />
            public static IEnumerable<String> EnumerateFiles(String path, String searchPattern, SearchOption searchOption)
            {
                return Interceptor.EnumerateFiles(path, searchPattern, searchOption);
            }

            /// <inheritdoc cref="IInterceptDirectoryHandler.EnumerateFiles(System.String, System.String, System.IO.EnumerationOptions)" />
            public static IEnumerable<String> EnumerateFiles(String path, String searchPattern, EnumerationOptions enumerationOptions)
            {
                return Interceptor.EnumerateFiles(path, searchPattern, enumerationOptions);
            }

            /// <inheritdoc cref="IInterceptDirectoryHandler.EnumerateDirectories(System.String)" />
            public static IEnumerable<String> EnumerateDirectories(String path)
            {
                return Interceptor.EnumerateDirectories(path);
            }

            /// <inheritdoc cref="IInterceptDirectoryHandler.EnumerateDirectories(System.String, System.String)" />
            public static IEnumerable<String> EnumerateDirectories(String path, String searchPattern)
            {
                return Interceptor.EnumerateDirectories(path, searchPattern);
            }

            /// <inheritdoc cref="IInterceptDirectoryHandler.EnumerateDirectories(System.String, System.String, System.IO.SearchOption)" />
            public static IEnumerable<String> EnumerateDirectories(String path, String searchPattern, SearchOption searchOption)
            {
                return Interceptor.EnumerateDirectories(path, searchPattern, searchOption);
            }

            /// <inheritdoc cref="IInterceptDirectoryHandler.EnumerateDirectories(System.String, System.String, System.IO.EnumerationOptions)" />
            public static IEnumerable<String> EnumerateDirectories(String path, String searchPattern, EnumerationOptions enumerationOptions)
            {
                return Interceptor.EnumerateDirectories(path, searchPattern, enumerationOptions);
            }

            /// <inheritdoc cref="IInterceptDirectoryHandler.EnumerateFileSystemEntries(System.String)" />
            public static IEnumerable<String> EnumerateFileSystemEntries(String path)
            {
                return Interceptor.EnumerateFileSystemEntries(path);
            }

            /// <inheritdoc cref="IInterceptDirectoryHandler.EnumerateFileSystemEntries(System.String, System.String)" />
            public static IEnumerable<String> EnumerateFileSystemEntries(String path, String searchPattern)
            {
                return Interceptor.EnumerateFileSystemEntries(path, searchPattern);
            }

            /// <inheritdoc cref="IInterceptDirectoryHandler.EnumerateFileSystemEntries(System.String, System.String, System.IO.SearchOption)" />
            public static IEnumerable<String> EnumerateFileSystemEntries(String path, String searchPattern, SearchOption searchOption)
            {
                return Interceptor.EnumerateFileSystemEntries(path, searchPattern, searchOption);
            }

            /// <inheritdoc cref="IInterceptDirectoryHandler.EnumerateFileSystemEntries(System.String, System.String, System.IO.EnumerationOptions)" />
            public static IEnumerable<String> EnumerateFileSystemEntries(String path, String searchPattern, EnumerationOptions enumerationOptions)
            {
                return Interceptor.EnumerateFileSystemEntries(path, searchPattern, enumerationOptions);
            }

            /// <inheritdoc cref="IInterceptDirectoryHandler.Move(System.String, System.String)" />
            public static void Move(String sourceDirName, String destDirName)
            {
                Interceptor.Move(sourceDirName, destDirName);
            }

            /// <inheritdoc cref="IInterceptDirectoryHandler.Delete(System.String)" />
            public static void Delete(String path)
            {
                Interceptor.Delete(path);
            }

            /// <inheritdoc cref="IInterceptDirectoryHandler.Delete(System.String, System.Boolean)" />
            public static void Delete(String path, Boolean recursive)
            {
                Interceptor.Delete(path, recursive);
            }
        }
    }
}