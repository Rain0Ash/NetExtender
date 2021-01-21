// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text.RegularExpressions;
using NetExtender.Utils.Types;
using JetBrains.Annotations;
using NetExtender.Watchers;

using NotNullAttribute = JetBrains.Annotations.NotNullAttribute;

namespace NetExtender.Utils.IO
{
    public static class DirectoryUtils
    {
        public static Boolean TryCreateDirectory(String path, PathAction remove = PathAction.Standart)
        {
            return TryCreateDirectory(path, remove, out _);
        }

        public static Boolean TryCreateDirectory(String path, out DirectoryInfo directoryInfo)
        {
            return TryCreateDirectory(path, PathAction.Standart, out directoryInfo);
        }

        public static Boolean TryCreateDirectory(String path, PathAction remove, out DirectoryInfo directoryInfo)
        {
            directoryInfo = null;

            try
            {
                if (Directory.Exists(path))
                {
                    return true;
                }

                directoryInfo = Directory.CreateDirectory(path);

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
                        case PathAction.Standart:
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

        public static DirectoryInfo LatestExist(String path)
        {
            return LatestExist(new DirectoryInfo(path));
        }

        public static DirectoryInfo LatestExist(this FileSystemInfo info)
        {
            return info switch
            {
                DirectoryInfo directory => LatestExist(directory),
                FileInfo file => LatestExist(file),
                _ => throw new NotSupportedException($"{nameof(info)} not supported {info.GetType()}")
            };
        }

        public static DirectoryInfo LatestExist(this FileInfo info)
        {
            return LatestExist(info.Directory);
        }

        public static DirectoryInfo LatestExist(this DirectoryInfo info)
        {
            while (!info.Exists)
            {
                DirectoryInfo parent = info.Parent;
                if (parent is null)
                {
                    break;
                }

                info = parent;
            }

            return info.Exists ? info : null;
        }

        public static Boolean CheckPermissions(String path, FileSystemRights access)
        {
            return CheckPermissions(new DirectoryInfo(path), access);
        }
        
        public static Boolean TryCheckPermissions(String path, FileSystemRights access)
        {
            return TryCheckPermissions(new DirectoryInfo(path), access);
        }

        public static Boolean CheckPermissions(this DirectoryInfo info, FileSystemRights access)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            return info.LatestExist()
                .GetAccessControl()
                .GetAccessRules(true, true, typeof(NTAccount))
                .OfType<FileSystemAccessRule>()
                .Any(rule => (rule.FileSystemRights & access) == access);
        }

        public static Boolean TryCheckPermissions(this DirectoryInfo info, FileSystemRights access)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            try
            {
                return CheckPermissions(info, access);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static IEnumerable<String> GetFiles(IEnumerable<FSWatcher> included, IEnumerable<FSWatcher> excluded, [RegexPattern] String pattern)
        {
            return GetFiles(included, excluded, new Regex(pattern));
        }

        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public static IEnumerable<String> GetFiles(IEnumerable<FSWatcher> included, IEnumerable<FSWatcher> excluded, Regex regex)
        {
            if (included.IsNullOrEmpty())
            {
                yield break;
            }

            HashSet<String> includedFolders = new HashSet<String>();
            HashSet<String> excludedFolders = new HashSet<String>();

            foreach (FSWatcher path in included.Where(path => path.IsExistAsFolder()))
            {
                includedFolders.Add(path.Path);

                if (path.IsRecursive)
                {
                    includedFolders.UnionWith(path.GetEntries(PathType.Folder, true));
                }
            }

            if (excluded is not null)
            {
                foreach (FSWatcher path in excluded.Where(path => path.IsExistAsFolder()))
                {
                    excludedFolders.Add(path.Path);

                    if (path.IsRecursive)
                    {
                        excludedFolders.UnionWith(path.GetEntries(PathType.Folder, true));
                    }
                }
            }

            foreach (String folder in includedFolders.Except(excludedFolders))
            {
                foreach (String file in GetFiles(folder, regex))
                {
                    yield return file;
                }
            }
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        [SuppressMessage("ReSharper", "InconsistentNaming")]
        [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Local")]
        [SuppressMessage("ReSharper", "MemberCanBePrivate.Local")]
        private struct WIN32_FIND_DATA
        {
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

        public static IEnumerable<String> GetDirectories([NotNull] String path, Boolean recursive = false)
        {
            return GetEntries(path, recursive, PathType.Folder);
        }

        public static IEnumerable<String> GetDirectories([NotNull] String path, [NotNull] [RegexPattern] String pattern, Boolean recursive = false)
        {
            return GetEntries(path, pattern, recursive, PathType.Folder);
        }

        public static IEnumerable<String> GetDirectories([NotNull] String path, Regex regex, Boolean recursive = false)
        {
            return GetEntries(path, regex, recursive, PathType.Folder);
        }

        public static IEnumerable<String> GetFiles([NotNull] String path, Boolean recursive = false)
        {
            return GetEntries(path, recursive, PathType.File);
        }

        public static IEnumerable<String> GetFiles([NotNull] String path, [NotNull] [RegexPattern] String pattern, Boolean recursive = false)
        {
            return GetEntries(path, pattern, recursive, PathType.File);
        }

        public static IEnumerable<String> GetFiles([NotNull] String path, Regex regex, Boolean recursive = false)
        {
            return GetEntries(path, regex, recursive, PathType.File);
        }

        public static IEnumerable<String> GetEntries([NotNull] String path, [NotNull] [RegexPattern] String pattern, Boolean recursive = false, PathType type = PathType.All)
        {
            if (String.IsNullOrEmpty(pattern) || pattern == AnySearchPattern)
            {
                return GetEntries(path, recursive, type);
            }

            return GetEntries(path, new Regex(pattern), recursive, type);
        }

        public static IEnumerable<String> GetEntries([NotNull] String path, Boolean recursive = false, PathType type = PathType.All)
        {
            if (String.IsNullOrEmpty(path))
            {
                throw new ArgumentException(@"Value cannot be null or empty.", nameof(path));
            }

            if (type == PathType.None)
            {
                yield break;
            }

            path = PathUtils.ConvertToFolder(path);

            if (!PathUtils.IsValidPath(path, PathType.Folder, PathStatus.Exist))
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

        public static IEnumerable<String> GetEntries([NotNull] String path, Regex regex, Boolean recursive = false, PathType type = PathType.All)
        {
            return GetEntries(path, recursive, type).Where(file => regex.IsMatch(file));
        }

        /// <summary>
        /// Renames the specified directory (adds the specified suffix to the end of the name).
        /// </summary>
        /// <param name="directory">The directory to add the suffix to.</param>
        /// <param name="suffix">The suffix to add to the directory.</param>
        public static void AddSuffix(DirectoryInfo directory, String suffix)
        {
            if (directory is null)
            {
                throw new ArgumentNullException(nameof(directory));
            }

            switch (suffix)
            {
                case null:
                    throw new ArgumentNullException(nameof(suffix));
                case "":
                    return;
            }

            String name = directory.Name + suffix;

            String dir = Path.GetDirectoryName(directory.FullName);

            if (String.IsNullOrEmpty(dir))
            {
                throw new IOException("Directory name is empty or null");
            }

            String path = Path.Combine(dir, name);
            directory.MoveTo(path);
        }

        /// <summary>
        /// Adds the specified suffix to all directories and files in the specified directory, using the specified search option.
        /// </summary>
        /// <param name="directory">The directory in which to add the suffix to all directories and files.</param>
        /// <param name="suffix">The suffix to add to all directories and files.</param>
        /// <param name="option">Specifies whether to include only the current directory or all subdirectories.</param>
        public static void AddSuffixToAll(DirectoryInfo directory, String suffix, SearchOption option = SearchOption.AllDirectories)
        {
            AddSuffixToAll(directory, suffix, option, null);
        }

        /// <summary>
        /// Adds the specified suffix to all directories and files in the specified directory, using the specified search option. If a directory matches any of the values in the provided ignore list, the suffix will not be added to it.
        /// </summary>
        /// <param name="directory">The directory in which to add the suffix to all directories and files.</param>
        /// <param name="suffix">The suffix to add to all directories and files.</param>
        /// <param name="option">Specifies whether to include only the current directory or all subdirectories.</param>
        /// <param name="ignored">A collection of directory names to ignore when adding suffix.</param>
        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public static void AddSuffixToAll(DirectoryInfo directory, String suffix, SearchOption option, IEnumerable<String> ignored)
        {
            AddSuffixToAllFiles(directory, suffix, SearchOption.TopDirectoryOnly);
            AddSuffixToAllDirectories(directory, suffix, SearchOption.TopDirectoryOnly, ignored);

            if (option != SearchOption.AllDirectories)
            {
                return;
            }

            DirectoryInfo[] subdirectories = directory.GetDirectories("*", SearchOption.TopDirectoryOnly);
            foreach (DirectoryInfo subdirectory in subdirectories)
            {
                AddSuffixToAll(subdirectory, suffix, SearchOption.AllDirectories, ignored);
            }
        }

        /// <summary>
        /// Adds the specified suffix to all directories in the specified directory, using the specified search option.
        /// </summary>
        /// <param name="directory">The directory in which to add the suffix to all directories.</param>
        /// <param name="suffix">The suffix to add to all directories.</param>
        /// <param name="option">Specifies whether to include only the current directory or all subdirectories.</param>
        public static void AddSuffixToAllDirectories(DirectoryInfo directory, String suffix, SearchOption option = SearchOption.AllDirectories)
        {
            AddSuffixToAllDirectories(directory, suffix, option, null);
        }

        /// <summary>
        /// Adds the specified suffix to all directories in the specified directory, using the specified search option. If a directory matches any of the values in the provided ignore list, the suffix will not be added to it.
        /// </summary>
        /// <param name="directory">The directory in which to add the suffix to all directories.</param>
        /// <param name="suffix">The suffix to add to all directories.</param>
        /// <param name="option">Specifies whether to include only the current directory or all subdirectories.</param>
        /// <param name="ignored">A collection of directory names to ignore when adding suffix.</param>
        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public static void AddSuffixToAllDirectories(DirectoryInfo directory, String suffix, SearchOption option, IEnumerable<String> ignored)
        {
            DirectoryInfo[] directories = directory.GetDirectories("*", SearchOption.TopDirectoryOnly);

            foreach (DirectoryInfo subdirectory in directories)
            {
                if (ignored is not null && ignored.Contains(subdirectory.Name))
                {
                    continue;
                }

                if (option == SearchOption.AllDirectories)
                {
                    AddSuffixToAllDirectories(subdirectory, suffix, SearchOption.AllDirectories, ignored);
                }

                AddSuffix(subdirectory, suffix);
            }
        }

        /// <summary>
        /// Adds the specified suffix to all files inside the specified directory, using the specified search option.
        /// </summary>
        /// <param name="directory">The directory in which to add the suffix to all files.</param>
        /// <param name="suffix">The suffix to add to all files.</param>
        /// <param name="option">Specifies whether to include only the current directory or all subdirectories.</param>
        public static void AddSuffixToAllFiles(DirectoryInfo directory, String suffix, SearchOption option = SearchOption.AllDirectories)
        {
            FileInfo[] files = directory.GetFiles("*", option);
            foreach (FileInfo file in files)
            {
                FileUtils.AddSuffix(file, suffix);
            }
        }

        /// <summary>
        /// Copies the specified source directory to the specified location.
        /// </summary>
        /// <param name="source">The path of the directory to copy.</param>
        /// <param name="destination">The path where to copy.</param>
        /// <param name="recursive">Determines whether to copy subdirectories.</param>
        /// <param name="overwrite">true to allow an existing file to be overwritten; otherwise, false.</param>
        public static void CopyDirectory(String source, String destination, Boolean recursive = true, Boolean overwrite = true)
        {
            DirectoryInfo directory = new DirectoryInfo(source);
            CopyDirectory(directory, destination, recursive, overwrite);
        }

        /// <summary>
        /// Copies the specified source directory to the specified location.
        /// </summary>
        /// <param name="source">The directory to copy.</param>
        /// <param name="destination">The path where to copy.</param>
        /// <param name="recursive">Determines whether to copy subdirectories.</param>
        /// <param name="overwrite">true to allow an existing file to be overwritten; otherwise, false.</param>
        public static void CopyDirectory(DirectoryInfo source, String destination, Boolean recursive = true, Boolean overwrite = true)
        {
            if (!source.Exists)
            {
                throw new DirectoryNotFoundException($"Source directory {source.FullName} does not exist or could not be found.");
            }

            if (!Directory.Exists(destination))
            {
                _ = Directory.CreateDirectory(destination);
            }

            // get the files in the top directory and copy them to the new location
            FileInfo[] files = source.GetFiles("*", SearchOption.TopDirectoryOnly);
            foreach (FileInfo file in files)
            {
                String path = Path.Combine(destination, file.Name);
                _ = file.CopyTo(path, overwrite);
            }

            if (!recursive)
            {
                return;
            }

            // copy all subdirectories and their contents to the new location
            DirectoryInfo[] subdirectories = source.GetDirectories("*", SearchOption.TopDirectoryOnly);
            foreach (DirectoryInfo subdirectory in subdirectories)
            {
                String path = Path.Combine(destination, subdirectory.Name);
                CopyDirectory(subdirectory, path, true, overwrite);
            }
        }

        /// <summary>
        /// Copies all files whose name starts with the specified prefix in the specified directory, including subdirectories.
        /// <para>If the destination directory doesn't exist, it is created.</para>
        /// </summary>
        /// <param name="source">The path of the source directory.</param>
        /// <param name="destination">The path of the directory where to copy files.</param>
        /// <param name="prefix">The prefix that files must contain in order to be copied.</param>
        /// <param name="overwrite">true to allow an existing file to be overwritten; otherwise, false.</param>
        public static void CopyAllFilesWithPrefix(String source, String destination, String prefix, Boolean overwrite = true)
        {
            CopyAllFilesWithPrefix(source, destination, prefix, SearchOption.AllDirectories, overwrite);
        }

        /// <summary>
        /// Copies all files whose name starts with the specified prefix in the specified directory, using the specified search option.
        /// <para>If the destination directory doesn't exist, it is created.</para>
        /// </summary>
        /// <param name="source">The path of the source directory.</param>
        /// <param name="destination">The path of the directory where to copy files.</param>
        /// <param name="prefix">The prefix that files must contain in order to be copied.</param>
        /// <param name="option">Specifies whether to include only the current directory or all subdirectories.</param>
        /// <param name="overwrite">true to allow an existing file to be overwritten; otherwise, false.</param>
        public static void CopyAllFilesWithPrefix(String source, String destination, String prefix, SearchOption option, Boolean overwrite = true)
        {
            DirectoryInfo info = new DirectoryInfo(source);
            CopyAllFilesWithPrefix(info, destination, prefix, option, overwrite);
        }

        /// <summary>
        /// Copies all files whose name starts with the specified prefix in the specified directory, including subdirectories.
        /// <para>If the destination directory doesn't exist, it is created.</para>
        /// </summary>
        /// <param name="source">The source directory.</param>
        /// <param name="destination">The path of the directory where to copy files.</param>
        /// <param name="prefix">The prefix that files must contain in order to be copied.</param>
        /// <param name="overwrite">true to allow an existing file to be overwritten; otherwise, false.</param>
        public static void CopyAllFilesWithPrefix(DirectoryInfo source, String destination, String prefix, Boolean overwrite = true)
        {
            CopyAllFilesWithPrefix(source, destination, prefix, SearchOption.AllDirectories, overwrite);
        }

        /// <summary>
        /// Copies all files whose name starts with the specified prefix in the specified directory, using the specified search option.
        /// <para>If the destination directory doesn't exist, it is created.</para>
        /// </summary>
        /// <param name="source">The source directory.</param>
        /// <param name="destination">The path of the directory where to copy files.</param>
        /// <param name="prefix">The prefix that files must contain in order to be copied.</param>
        /// <param name="option">Specifies whether to include only the current directory or all subdirectories.</param>
        /// <param name="overwrite">true to allow an existing file to be overwritten; otherwise, false.</param>
        public static void CopyAllFilesWithPrefix(DirectoryInfo source, String destination, String prefix, SearchOption option, Boolean overwrite = true)
        {
            if (!Directory.Exists(destination))
            {
                _ = Directory.CreateDirectory(destination);
            }

            FileInfo[] files = source.GetFiles($"{prefix}*", SearchOption.TopDirectoryOnly);
            foreach (FileInfo file in files)
            {
                String path = Path.Combine(destination, file.Name);
                _ = file.CopyTo(path, overwrite);
            }

            if (option != SearchOption.AllDirectories)
            {
                return;
            }

            DirectoryInfo[] subdirectories = source.GetDirectories("*", SearchOption.TopDirectoryOnly);
            foreach (DirectoryInfo subdirectory in subdirectories)
            {
                String path = Path.Combine(destination, subdirectory.Name);
                CopyAllFilesWithPrefix(subdirectory, path, prefix, SearchOption.AllDirectories, overwrite);
            }
        }

        /// <summary>
        /// Deletes all directories in the specified directory, whose name is not ended by the specified suffix, using the specified search option.
        /// </summary>
        /// <param name="directory">The directory in which to delete all directories.</param>
        /// <param name="suffix">The suffix that directories must not contain in order to be deleted.</param>
        /// <param name="option">Specifies whether to include only the current directory or all subdirectories.</param>
        public static void DeleteAllDirectoriesWithoutSuffix(DirectoryInfo directory, String suffix, SearchOption option = SearchOption.AllDirectories)
        {
            DeleteAllDirectoriesWithoutSuffix(directory, suffix, option, null);
        }

        /// <summary>
        /// Deletes all directories in the specified directory, whose name is not ended by the specified suffix, using the specified search option. If a directory matches any of the values in the provided ignore list, it will not be deleted.
        /// </summary>
        /// <param name="directory">The directory in which to delete all directories.</param>
        /// <param name="suffix">The suffix that directories must not contain in order to be deleted.</param>
        /// <param name="option">Specifies whether to include only the current directory or all subdirectories.</param>
        /// <param name="ignored">A collection of directory names to ignore.</param>
        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public static void DeleteAllDirectoriesWithoutSuffix(DirectoryInfo directory, String suffix, SearchOption option, IEnumerable<String> ignored)
        {
            DirectoryInfo[] subdirectories = directory.GetDirectories("*", SearchOption.TopDirectoryOnly);
            for (Int32 i = subdirectories.Length - 1; i >= 0; --i)
            {
                DirectoryInfo subdirectory = subdirectories[i];

                if (ignored is not null && ignored.Contains(subdirectory.Name))
                {
                    continue;
                }

                if (!subdirectory.Name.EndsWith(suffix))
                {
                    subdirectories[i].Delete(true);
                    continue;
                }

                if (option == SearchOption.AllDirectories)
                {
                    DeleteAllDirectoriesWithoutSuffix(subdirectory, suffix, SearchOption.AllDirectories, ignored);
                }
            }
        }

        /// <summary>
        /// Deletes all directories in the specified directory, whose name is ended by the specified suffix, using the specified search option.
        /// </summary>
        /// <param name="directory">The directory in which to delete all directories.</param>
        /// <param name="suffix">The suffix that directories must contain in order to be deleted.</param>
        /// <param name="option">Specifies whether to include only the current directory or all subdirectories.</param>
        public static void DeleteAllDirectoriesWithSuffix(DirectoryInfo directory, String suffix, SearchOption option = SearchOption.AllDirectories)
        {
            DirectoryInfo[] subdirectories = directory.GetDirectories("*", SearchOption.TopDirectoryOnly);
            for (Int32 i = subdirectories.Length - 1; i >= 0; i--)
            {
                DirectoryInfo subdirectory = subdirectories[i];

                if (subdirectory.Name.EndsWith(suffix))
                {
                    subdirectories[i].Delete(true);
                    continue;
                }

                if (option == SearchOption.AllDirectories)
                {
                    DeleteAllDirectoriesWithSuffix(subdirectory, suffix);
                }
            }
        }

        /// <summary>
        /// Deletes all files in the specified directory, whose name is not ended by the specified suffix, using the specified search option.
        /// </summary>
        /// <param name="directory">The directory in which to delete all files.</param>
        /// <param name="suffix">The suffix that files must not contain in order to be deleted.</param>
        /// <param name="option">Specifies whether to include only the current directory or all subdirectories.</param>
        public static void DeleteAllFilesWithoutSuffix(DirectoryInfo directory, String suffix, SearchOption option = SearchOption.AllDirectories)
        {
            FileInfo[] files = directory.GetFiles("*", option);
            foreach (FileInfo file in files)
            {
                if (!file.Name.EndsWith(suffix))
                {
                    file.Delete();
                }
            }
        }

        /// <summary>
        /// Deletes all files in the specified directory, whose name is ended by the specified suffix, using the specified search option.
        /// </summary>
        /// <param name="directory">The directory in which to delete all files.</param>
        /// <param name="suffix">The suffix that files must contain in order to be deleted.</param>
        /// <param name="option">Specifies whether to include only the current directory or all subdirectories.</param>
        public static void DeleteAllFilesWithSuffix(DirectoryInfo directory, String suffix, SearchOption option = SearchOption.AllDirectories)
        {
            FileInfo[] files = directory.GetFiles("*" + suffix, option);
            foreach (FileInfo file in files)
            {
                file.Delete();
            }
        }

        /// <summary>
        /// Deletes all files and directories in the specified directory, whose name is not ended by the specified suffix, using the specified search option.
        /// </summary>
        /// <param name="directory">The directory in which to delete all files and directories.</param>
        /// <param name="suffix">The suffix that files and directories must not contain in order to be deleted.</param>
        /// <param name="option">Specifies whether to include only the current directory or all subdirectories.</param>
        public static void DeleteAllWithoutSuffix(DirectoryInfo directory, String suffix, SearchOption option = SearchOption.AllDirectories)
        {
            DeleteAllWithoutSuffix(directory, suffix, option, null);
        }

        /// <summary>
        /// Deletes all files and directories in the specified directory, whose name is not ended by the specified suffix, using the specified search option. If a directory matches any of the values in the provided ignore list, it will not be deleted.
        /// </summary>
        /// <param name="directory">The directory in which to delete all files and directories.</param>
        /// <param name="suffix">The suffix that files and directories must not contain in order to be deleted.</param>
        /// <param name="option">Specifies whether to include only the current directory or all subdirectories.</param>
        /// <param name="ignored">A collection of directory names to ignore.</param>
        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public static void DeleteAllWithoutSuffix(DirectoryInfo directory, String suffix, SearchOption option, IEnumerable<String> ignored)
        {
            DeleteAllFilesWithoutSuffix(directory, suffix, SearchOption.TopDirectoryOnly);
            DeleteAllDirectoriesWithoutSuffix(directory, suffix, SearchOption.TopDirectoryOnly, ignored);

            if (option != SearchOption.AllDirectories)
            {
                return;
            }

            DirectoryInfo[] subdirectories = directory.GetDirectories("*", SearchOption.TopDirectoryOnly);

            foreach (DirectoryInfo subdirectory in subdirectories)
            {
                DeleteAllWithoutSuffix(subdirectory, suffix, SearchOption.AllDirectories, ignored);
            }
        }

        /// <summary>
        /// Deletes all files and directories in the specified directory, whose name is ended by the specified suffix, using the specified search option.
        /// </summary>
        /// <param name="directory">The directory in which to delete all files and directories.</param>
        /// <param name="suffix">The suffix that files and directories must contain in order to be deleted.</param>
        /// <param name="option">Specifies whether to include only the current directory or all subdirectories.</param>
        public static void DeleteAllWithSuffix(DirectoryInfo directory, String suffix, SearchOption option = SearchOption.AllDirectories)
        {
            DeleteAllFilesWithSuffix(directory, suffix, SearchOption.TopDirectoryOnly);
            DeleteAllDirectoriesWithSuffix(directory, suffix, SearchOption.TopDirectoryOnly);

            if (option != SearchOption.AllDirectories)
            {
                return;
            }

            DirectoryInfo[] subdirectories = directory.GetDirectories("*", SearchOption.TopDirectoryOnly);

            foreach (DirectoryInfo subdirectory in subdirectories)
            {
                DeleteAllWithSuffix(subdirectory, suffix);
            }
        }

        /// <summary>
        /// Renames the specified directory (removes the specified suffix from the end of the name, if present).
        /// </summary>
        /// <param name="directory">The directory to remove the suffix from.</param>
        /// <param name="suffix">The suffix to remove from the directory.</param>
        public static void RemoveSuffix(DirectoryInfo directory, String suffix)
        {
            if (directory is null)
            {
                throw new ArgumentNullException(nameof(directory));
            }

            switch (suffix)
            {
                case null:
                    throw new ArgumentNullException(nameof(suffix));
                case "":
                    return;
            }

            if (!directory.Name.EndsWith(suffix))
            {
                return;
            }

            String name = directory.Name.Substring(0, directory.Name.Length - suffix.Length);

            String dir = Path.GetDirectoryName(directory.FullName);

            if (String.IsNullOrEmpty(dir))
            {
                throw new IOException("Directory name is empty or null");
            }
                
            String path = Path.Combine(dir, name);
            directory.MoveTo(path);
        }

        /// <summary>
        /// Removes the specified suffix from all directories and files in the specified directory, using the specified search option.
        /// </summary>
        /// <param name="directory">The directory in which to remove the suffix from all directories and files.</param>
        /// <param name="suffix">The suffix to remove from all directories and files.</param>
        /// <param name="option">Specifies whether to include only the current directory or all subdirectories.</param>
        public static void RemoveSuffixFromAll(DirectoryInfo directory, String suffix, SearchOption option = SearchOption.AllDirectories)
        {
            RemoveSuffixFromAllFiles(directory, suffix, SearchOption.TopDirectoryOnly);
            RemoveSuffixFromAllDirectories(directory, suffix, SearchOption.TopDirectoryOnly);

            if (option != SearchOption.AllDirectories)
            {
                return;
            }

            DirectoryInfo[] subdirectories = directory.GetDirectories("*", SearchOption.TopDirectoryOnly);
            foreach (DirectoryInfo subdirectory in subdirectories)
            {
                RemoveSuffixFromAll(subdirectory, suffix);
            }
        }

        /// <summary>
        /// Removes the specified suffix from all directories in the specified directory, using the specified search option.
        /// </summary>
        /// <param name="directory">The directory in which to remove the suffix from all directories.</param>
        /// <param name="suffix">The suffix to remove from all directories.</param>
        /// <param name="option">Specifies whether to include only the current directory or all subdirectories.</param>
        public static void RemoveSuffixFromAllDirectories(DirectoryInfo directory, String suffix, SearchOption option = SearchOption.AllDirectories)
        {
            DirectoryInfo[] directories = directory.GetDirectories("*", SearchOption.TopDirectoryOnly);

            foreach (DirectoryInfo subdirectory in directories)
            {
                if (option == SearchOption.AllDirectories)
                {
                    RemoveSuffixFromAllDirectories(subdirectory, suffix);
                }

                RemoveSuffix(subdirectory, suffix);
            }
        }

        /// <summary>
        /// Removes the specified suffix from all files inside the specified directory, using the specified search option.
        /// </summary>
        /// <param name="directory">The directory in which to remove the suffix from all directories.</param>
        /// <param name="suffix">The suffix to remove from all directories.</param>
        /// <param name="option">Specifies whether to include only the current directory or all subdirectories.</param>
        public static void RemoveSuffixFromAllFiles(DirectoryInfo directory, String suffix, SearchOption option = SearchOption.AllDirectories)
        {
            FileInfo[] files = directory.GetFiles("*" + suffix, option);
            foreach (FileInfo file in files)
            {
                FileUtils.RemoveSuffix(file, suffix);
            }
        }
    }
}