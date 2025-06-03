// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Text.RegularExpressions;
using NetExtender.Types.Exceptions;
using NetExtender.Utilities.IO;
using NetExtender.Utilities.Numerics;
using NetExtender.Utilities.Types;

namespace NetExtender.Utilities.Windows.IO
{
    public static class WindowsDirectoryUtilities
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryCreateDirectory(String path)
        {
            return TryCreateDirectory(path, out _);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean TryCreateDirectory(String path, PathAction remove)
        {
            return TryCreateDirectory(path, remove, out _);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
                            if (GetFiles(path).All(file => file.Equals("desktop.ini", StringComparison.OrdinalIgnoreCase)) && !GetDirectories(path).Any())
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
                }
            }
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Local")]
        [SuppressMessage("ReSharper", "MemberCanBePrivate.Local")]
        private struct FindData
        {
            public static implicit operator FileData(FindData data)
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

            public readonly Boolean IsCurrentOrParentDirectory
            {
                get
                {
                    return cFileName == "." || cFileName == "..";
                }
            }
        }

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern Boolean FindClose(IntPtr hFindFile);

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern IntPtr FindFirstFileW(String lpFileName, out FindData lpFindFileData);

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern Boolean FindNextFileW(IntPtr hFindFile, out FindData lpFindFileData);

        public const String AnySearchPattern = ".*";

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<String> GetDirectories(String path)
        {
            return GetDirectories(path, false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<String> GetDirectories(String path, Boolean recursive)
        {
            return GetEntries(path, recursive, PathType.Folder);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<String> GetDirectories(String path, String pattern)
        {
            return GetDirectories(path, pattern, false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<String> GetDirectories(String path, String pattern, Boolean recursive)
        {
            return GetEntries(path, pattern, recursive, PathType.Folder);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<String> GetDirectories(String path, Regex regex)
        {
            return GetDirectories(path, regex, false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<String> GetDirectories(String path, Regex regex, Boolean recursive)
        {
            return GetEntries(path, regex, recursive, PathType.Folder);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<String> GetFiles(String path)
        {
            return GetFiles(path, false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<String> GetFiles(String path, Boolean recursive)
        {
            return GetEntries(path, recursive, PathType.File);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<String> GetFiles(String path, String pattern)
        {
            return GetFiles(path, pattern, false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<String> GetFiles(String path, String pattern, Boolean recursive)
        {
            return GetEntries(path, pattern, recursive, PathType.File);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<String> GetFiles(String path, Regex regex)
        {
            return GetFiles(path, regex, false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<String> GetFiles(String path, Regex regex, Boolean recursive)
        {
            return GetEntries(path, regex, recursive, PathType.File);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IEnumerable<KeyValuePair<String, FindData>> GetFilesWindowsData(String path)
        {
            return GetFilesWindowsData(path, false);
        }

        [SuppressMessage("ReSharper", "CognitiveComplexity")]
        private static IEnumerable<KeyValuePair<String, FindData>> GetFilesWindowsData(String path, Boolean recursive)
        {
            if (String.IsNullOrEmpty(path))
            {
                throw new ArgumentNullOrEmptyStringException(path, nameof(path));
            }

            path = PathUtilities.ConvertToFolder(path);

            if (!PathUtilities.IsValidPath(path, PathType.Folder, PathStatus.Exist))
            {
                yield break;
            }

            IntPtr handle = FindFirstFileW(path + "*", out FindData data);

            if (handle.IsNullOrInvalid())
            {
                yield break;
            }

            try
            {
                do
                {
                    if (data.IsCurrentOrParentDirectory)
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

                        foreach (KeyValuePair<String, FindData> entry in GetFilesWindowsData(entryname, true))
                        {
                            yield return entry;
                        }

                        continue;
                    }

                    yield return new KeyValuePair<String, FindData>(entryname, data);
                } while (FindNextFileW(handle, out data));
            }
            finally
            {
                FindClose(handle);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<String, FileData>> GetFilesData(String path)
        {
            return GetFilesData(path, false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<String, FileData>> GetFilesData(String path, Boolean recursive)
        {
            return GetFilesWindowsData(path, recursive).Select(pair => new KeyValuePair<String, FileData>(pair.Key, pair.Value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<String, UInt64>> GetFilesSize(String path)
        {
            return GetFilesSize(path, false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<KeyValuePair<String, UInt64>> GetFilesSize(String path, Boolean recursive)
        {
            return GetFilesWindowsData(path, recursive)
                .Select(pair => new KeyValuePair<String, UInt64>(pair.Key, BitUtilities.ToUInt64(pair.Value.nFileSizeHigh, pair.Value.nFileSizeLow)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<FileInfo> GetFilesInfo(String path)
        {
            return GetFilesInfo(path, false);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<FileInfo> GetFilesInfo(String path, Boolean recursive)
        {
            return GetFiles(path, recursive).Select(entry => new FileInfo(entry));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<String> GetEntries(String path, String pattern)
        {
            return GetEntries(path, pattern, PathType.All);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<String> GetEntries(String path, String pattern, PathType type)
        {
            return GetEntries(path, pattern, false, type);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<String> GetEntries(String path, String pattern, Boolean recursive)
        {
            return GetEntries(path, pattern, recursive, PathType.All);
        }

        public static IEnumerable<String> GetEntries(String path, String pattern, Boolean recursive, PathType type)
        {
            if (String.IsNullOrEmpty(pattern) || pattern == AnySearchPattern)
            {
                return GetEntries(path, recursive, type);
            }

            return GetEntries(path, new Regex(pattern), recursive, type);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<String> GetEntries(String path)
        {
            return GetEntries(path, PathType.All);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<String> GetEntries(String path, PathType type)
        {
            return GetEntries(path, false, type);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<String> GetEntries(String path, Boolean recursive)
        {
            return GetEntries(path, recursive, PathType.All);
        }

        [SuppressMessage("ReSharper", "CognitiveComplexity")]
        public static IEnumerable<String> GetEntries(String path, Boolean recursive, PathType type)
        {
            if (String.IsNullOrEmpty(path))
            {
                throw new ArgumentNullOrEmptyStringException(path, nameof(path));
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

            IntPtr handle = FindFirstFileW(path + "*", out FindData data);

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
                    if (data.IsCurrentOrParentDirectory)
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<String> GetEntries(String path, Regex regex)
        {
            return GetEntries(path, regex, PathType.All);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<String> GetEntries(String path, Regex regex, PathType type)
        {
            return GetEntries(path, regex, false, type);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<String> GetEntries(String path, Regex regex, Boolean recursive)
        {
            return GetEntries(path, regex, recursive, PathType.All);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<String> GetEntries(String path, Regex regex, Boolean recursive, PathType type)
        {
            if (regex is null)
            {
                throw new ArgumentNullException(nameof(regex));
            }

            return GetEntries(path, recursive, type).Where(file => regex.IsMatch(file));
        }

        [DllImport("shell32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern void SHChangeNotify(Int32 wEventId, Int32 uFlags, IntPtr dwItem1, IntPtr dwItem2);

        public static void SetDirectoryIcon(String directory, String icon)
        {
            SetDirectoryIcon(directory, icon, 0);
        }

        public static void SetDirectoryIcon(String directory, String icon, Int32 index)
        {
            if (String.IsNullOrEmpty(directory))
            {
                throw new ArgumentNullOrEmptyStringException(directory, nameof(directory));
            }

            if (String.IsNullOrEmpty(icon))
            {
                throw new ArgumentNullOrEmptyStringException(icon, nameof(icon));
            }

            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(index), index, "Value must be greater than or equal to 0.");
            }

            String filename = Path.GetFileName(icon);
            String desktop = Path.Combine(directory, "desktop.ini");

            using (StreamWriter writer = new StreamWriter(desktop, false, Encoding.Unicode))
            {
                writer.WriteLine("[.ShellClassInfo]");
                writer.WriteLine($"IconFile={filename}");
                writer.WriteLine($"IconIndex={index}");
                writer.WriteLine("ConfirmFileOp=0");
                writer.WriteLine($"IconResource={filename},{index}");

                writer.Flush();
                writer.Close();
            }

            File.SetAttributes(desktop, File.GetAttributes(desktop) | FileAttributes.Hidden | FileAttributes.Archive | FileAttributes.System);
            File.SetAttributes(directory, File.GetAttributes(directory) | FileAttributes.System);

            String directoryicon = Path.Combine(directory, filename);
            if (String.IsNullOrWhiteSpace(Path.GetDirectoryName(icon)))
            {
                icon = directoryicon;
            }

            if (!String.Equals(directory.TrimEnd(Path.DirectorySeparatorChar), Path.GetDirectoryName(icon)?.TrimEnd(Path.DirectorySeparatorChar), StringComparison.InvariantCultureIgnoreCase))
            {
                File.Copy(icon, directoryicon);
            }

            SHChangeNotify(0x08000000, 0x0000, (IntPtr) null, (IntPtr) null);
        }

        public static Boolean TrySetDirectoryIcon(String directory, String icon)
        {
            return TrySetDirectoryIcon(directory, icon, 0);
        }

        public static Boolean TrySetDirectoryIcon(String directory, String icon, Int32 index)
        {
            if (directory is null)
            {
                throw new ArgumentNullException(nameof(directory));
            }

            if (icon is null)
            {
                throw new ArgumentNullException(nameof(icon));
            }

            if (directory == String.Empty)
            {
                return false;
            }

            if (icon == String.Empty)
            {
                return false;
            }

            if (index < 0)
            {
                return false;
            }

            try
            {
                SetDirectoryIcon(directory, icon, index);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}