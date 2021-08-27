// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text.RegularExpressions;
using NetExtender.Utilities.IO;
using NetExtender.Utilities.Numerics;
using NetExtender.Utilities.Types;

namespace NetExtender.Utilities.Windows.IO
{
    public static class WindowsDirectoryUtilities
    {
        public static Boolean TryCreateDirectory(String path)
        {
            return TryCreateDirectory(path, out _);
        }
        
        public static Boolean TryCreateDirectory(String path, PathAction remove)
        {
            return TryCreateDirectory(path, remove, out _);
        }

        public static Boolean TryCreateDirectory(String path, out DirectoryInfo? info)
        {
            return TryCreateDirectory(path, PathAction.Standard, out info);
        }

        public static Boolean TryCreateDirectory(String path, PathAction remove, out DirectoryInfo? info)
        {
            info = null;

            try
            {
                if (Directory.Exists(path))
                {
                    return true;
                }

                info = Directory.CreateDirectory(path);

                return Directory.Exists(path);
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                try
                {
                    switch (remove)
                    {
                        case PathAction.Standard:
                            if (GetFiles(path).All(file => file.Equals("desktop.ini", StringComparison.OrdinalIgnoreCase)) &&
                                !GetDirectories(path).Any())
                            {
                                Directory.Delete(path, true);
                            }

                            break;
                        case PathAction.Force:
                            Directory.Delete(path, true);
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception)
                {
                    // ignored
                }
            }
        }
        
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Local")]
        [SuppressMessage("ReSharper", "MemberCanBePrivate.Local")]
        private struct WIN32_FIND_DATA
        {
            public static implicit operator FileData(WIN32_FIND_DATA data)
            {
                return new FileData
                {
                    Attributes = (FileAttributes) data.dwFileAttributes,
                    CreationTime = new DateTime(BitUtilities.ToInt64(data.ftCreationTime.dwHighDateTime, data.ftCreationTime.dwLowDateTime)),
                    LastAccessTime = new DateTime(BitUtilities.ToInt64(data.ftLastAccessTime.dwHighDateTime, data.ftLastAccessTime.dwLowDateTime)),
                    LastWriteTime = new DateTime(BitUtilities.ToInt64(data.ftLastWriteTime.dwHighDateTime, data.ftLastWriteTime.dwLowDateTime)),
                    FileSize = BitUtilities.ToUInt64(data.nFileSizeHigh, data.nFileSizeLow)
                };
            }
            
            public UInt32 dwFileAttributes;
            public FILETIME ftCreationTime;
            public FILETIME ftLastAccessTime;
            public FILETIME ftLastWriteTime;
            public UInt32 nFileSizeHigh;
            public UInt32 nFileSizeLow;
            public UInt32 dwReserved0;
            public UInt32 dwReserved1;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public String cFileName;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 14)]
            public String cAlternateFileName;

            public UInt32 dwFileType;
            public UInt32 dwCreatorType;
            public UInt16 wFinderFlags;

            public readonly Boolean IsDirectory
            {
                get
                {
                    const UInt32 directory = (UInt32) FileAttributes.Directory;
                    return (dwFileAttributes & directory) == directory;
                }
            }
        }

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern Boolean FindClose(IntPtr hFindFile);

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern IntPtr FindFirstFileW(String lpFileName, out WIN32_FIND_DATA lpFindFileData);

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern Boolean FindNextFileW(IntPtr hFindFile, out WIN32_FIND_DATA lpFindFileData);
        
        public const String AnySearchPattern = ".*";

        public static IEnumerable<String> GetDirectories(String path, Boolean recursive = false)
        {
            return GetEntries(path, recursive, PathType.Folder);
        }

        public static IEnumerable<String> GetDirectories(String path, String pattern, Boolean recursive = false)
        {
            return GetEntries(path, pattern, recursive, PathType.Folder);
        }

        public static IEnumerable<String> GetDirectories(String path, Regex regex, Boolean recursive = false)
        {
            return GetEntries(path, regex, recursive, PathType.Folder);
        }

        public static IEnumerable<String> GetFiles(String path, Boolean recursive = false)
        {
            return GetEntries(path, recursive, PathType.File);
        }

        public static IEnumerable<String> GetFiles(String path, String pattern, Boolean recursive = false)
        {
            return GetEntries(path, pattern, recursive, PathType.File);
        }

        public static IEnumerable<String> GetFiles(String path, Regex regex, Boolean recursive = false)
        {
            return GetEntries(path, regex, recursive, PathType.File);
        }

        [SuppressMessage("ReSharper", "CognitiveComplexity")]
        private static IEnumerable<KeyValuePair<String, WIN32_FIND_DATA>> GetFilesWindowsData(String path, Boolean recursive = false)
        {
            if (String.IsNullOrEmpty(path))
            {
                throw new ArgumentException(@"Value cannot be null or empty.", nameof(path));
            }

            path = PathUtilities.ConvertToFolder(path);

            if (!PathUtilities.IsValidPath(path, PathType.Folder, PathStatus.Exist))
            {
                yield break;
            }

            IntPtr handle = FindFirstFileW(path + "*", out WIN32_FIND_DATA data);

            if (handle.IsNullOrInvalid())
            {
                yield break;
            }
            
            try
            {
                do
                {
                    if (data.cFileName == "." || data.cFileName == "..")
                    {
                        continue;
                    }

                    String entryname = path + data.cFileName;

                    if (data.IsDirectory)
                    {
                        if (!recursive)
                        {
                            continue;
                        }

                        foreach (KeyValuePair<String, WIN32_FIND_DATA> entry in GetFilesWindowsData(entryname, true))
                        {
                            yield return entry;
                        }
                    }
                    else
                    {
                        yield return new KeyValuePair<String, WIN32_FIND_DATA>(entryname, data);
                    }
                } while (FindNextFileW(handle, out data));
            }
            finally
            {
                FindClose(handle);
            }
        }

        public static IEnumerable<KeyValuePair<String, FileData>> GetFilesData(String path, Boolean recursive = false)
        {
            return GetFilesWindowsData(path, recursive).Select(pair => new KeyValuePair<String, FileData>(pair.Key, pair.Value));
        }
        
        public static IEnumerable<KeyValuePair<String, UInt64>> GetFilesSize(String path, Boolean recursive = false)
        {
            return GetFilesWindowsData(path, recursive).Select(pair => new KeyValuePair<String, UInt64>(pair.Key, BitUtilities.ToUInt64(pair.Value.nFileSizeHigh, pair.Value.nFileSizeLow)));
        }

        public static IEnumerable<FileInfo> GetFilesInfo(String path, Boolean recursive = false)
        {
            return GetFiles(path, recursive).Select(entry => new FileInfo(entry));
        }

        public static IEnumerable<String> GetEntries(String path, String pattern, Boolean recursive = false, PathType type = PathType.All)
        {
            if (String.IsNullOrEmpty(pattern) || pattern == AnySearchPattern)
            {
                return GetEntries(path, recursive, type);
            }

            return GetEntries(path, new Regex(pattern), recursive, type);
        }

        [SuppressMessage("ReSharper", "CognitiveComplexity")]
        public static IEnumerable<String> GetEntries(String path, Boolean recursive = false, PathType type = PathType.All)
        {
            if (String.IsNullOrEmpty(path))
            {
                throw new ArgumentException(@"Value cannot be null or empty.", nameof(path));
            }

            if (type == PathType.None)
            {
                yield break;
            }

            path = PathUtilities.ConvertToFolder(path);

            if (!PathUtilities.IsValidPath(path, PathType.Folder, PathStatus.Exist))
            {
                yield break;
            }

            IntPtr handle = FindFirstFileW(path + "*", out WIN32_FIND_DATA data);

            if (handle.IsNullOrInvalid())
            {
                yield break;
            }

            Boolean folder = type.HasFlag(PathType.Folder);
            Boolean file = type.HasFlag(PathType.File);
            
            try
            {
                do
                {
                    if (data.cFileName == "." || data.cFileName == "..")
                    {
                        continue;
                    }

                    String entryname = path + data.cFileName;

                    if (data.IsDirectory)
                    {
                        if (recursive)
                        {
                            foreach (String entry in GetEntries(entryname, true, type))
                            {
                                yield return entry;
                            }
                        }

                        if (folder)
                        {
                            yield return entryname;
                        }
                    }
                    else
                    {
                        if (file)
                        {
                            yield return entryname;
                        }
                    }
                } while (FindNextFileW(handle, out data));
            }
            finally
            {
                FindClose(handle);
            }
        }

        public static IEnumerable<String> GetEntries(String path, Regex regex, Boolean recursive = false, PathType type = PathType.All)
        {
            if (regex is null)
            {
                throw new ArgumentNullException(nameof(regex));
            }

            return GetEntries(path, recursive, type).Where(file => regex.IsMatch(file));
        }
    }
}