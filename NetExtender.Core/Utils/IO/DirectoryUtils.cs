// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NetExtender.Utils.Types;

namespace NetExtender.Utils.IO
{
    public static class DirectoryUtils
    {
        public static DirectoryInfo? LatestExist(String path)
        {
            if (path is null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            return LatestExist(new DirectoryInfo(path));
        }

        public static DirectoryInfo? LatestExist(this FileSystemInfo info)
        {
            return info switch
            {
                DirectoryInfo directory => LatestExist(directory),
                FileInfo file => LatestExist(file),
                _ => throw new NotSupportedException($"{nameof(info)} not supported {info.GetType()}")
            };
        }

        public static DirectoryInfo? LatestExist(this FileInfo info)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            DirectoryInfo? directory = info.Directory;

            return directory is not null ? LatestExist(directory) : null;
        }

        public static DirectoryInfo? LatestExist(this DirectoryInfo info)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            while (!info.Exists)
            {
                DirectoryInfo? parent = info.Parent;
                if (parent is null)
                {
                    break;
                }

                info = parent;
            }

            return info.Exists ? info : null;
        }

        /// <summary>
        /// Renames the specified directory (adds the specified suffix to the end of the name).
        /// </summary>
        /// <param name="directory">The directory to add the suffix to.</param>
        /// <param name="suffix">The suffix to add to the directory.</param>
        public static void AddSuffix(this DirectoryInfo directory, String suffix)
        {
            if (directory is null)
            {
                throw new ArgumentNullException(nameof(directory));
            }

            if (suffix is null)
            {
                throw new ArgumentNullException(nameof(suffix));
            }

            if (suffix == String.Empty)
            {
                return;
            }

            String name = directory.Name + suffix;

            String? dir = Path.GetDirectoryName(directory.FullName);

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
        public static void AddSuffixToAll(this DirectoryInfo directory, String suffix, SearchOption option = SearchOption.AllDirectories)
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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public static void AddSuffixToAll(this DirectoryInfo directory, String suffix, SearchOption option, IEnumerable<String> ignored)
        {
            if (directory is null)
            {
                throw new ArgumentNullException(nameof(directory));
            }

            if (suffix is null)
            {
                throw new ArgumentNullException(nameof(suffix));
            }

            if (suffix == String.Empty)
            {
                return;
            }

            ignored = ignored.Materialize();
            
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
        public static void AddSuffixToAllDirectories(this DirectoryInfo directory, String suffix, SearchOption option = SearchOption.AllDirectories)
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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public static void AddSuffixToAllDirectories(this DirectoryInfo directory, String suffix, SearchOption option, IEnumerable<String> ignored)
        {
            if (directory is null)
            {
                throw new ArgumentNullException(nameof(directory));
            }

            if (suffix is null)
            {
                throw new ArgumentNullException(nameof(suffix));
            }

            if (suffix == String.Empty)
            {
                return;
            }
            
            ignored = ignored.Materialize();
            
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
        public static void AddSuffixToAllFiles(this DirectoryInfo directory, String suffix, SearchOption option = SearchOption.AllDirectories)
        {
            if (directory is null)
            {
                throw new ArgumentNullException(nameof(directory));
            }
            
            if (suffix is null)
            {
                throw new ArgumentNullException(nameof(suffix));
            }

            if (suffix == String.Empty)
            {
                return;
            }

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
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (destination is null)
            {
                throw new ArgumentNullException(nameof(destination));
            }

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
        public static void CopyDirectory(this DirectoryInfo source, String destination, Boolean recursive = true, Boolean overwrite = true)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            
            if (String.IsNullOrEmpty(destination))
            {
                throw new ArgumentException(@"Value cannot be null or empty.", nameof(destination));
            }

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
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

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
        public static void CopyAllFilesWithPrefix(this DirectoryInfo source, String destination, String prefix, Boolean overwrite = true)
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
        public static void CopyAllFilesWithPrefix(this DirectoryInfo source, String destination, String prefix, SearchOption option, Boolean overwrite = true)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (String.IsNullOrEmpty(destination))
            {
                throw new ArgumentException(@"Value cannot be null or empty.", nameof(destination));
            }

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
        public static void DeleteAllDirectoriesWithoutSuffix(this DirectoryInfo directory, String suffix, SearchOption option = SearchOption.AllDirectories)
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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public static void DeleteAllDirectoriesWithoutSuffix(this DirectoryInfo directory, String suffix, SearchOption option, IEnumerable<String> ignored)
        {
            if (directory is null)
            {
                throw new ArgumentNullException(nameof(directory));
            }

            if (suffix is null)
            {
                throw new ArgumentNullException(nameof(suffix));
            }

            if (suffix == String.Empty)
            {
                return;
            }
            
            if (!directory.Exists)
            {
                throw new DirectoryNotFoundException($"Directory {directory.FullName} does not exist or could not be found.");
            }

            ignored = ignored.Materialize();
            
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
        public static void DeleteAllDirectoriesWithSuffix(this DirectoryInfo directory, String suffix, SearchOption option = SearchOption.AllDirectories)
        {
            if (directory is null)
            {
                throw new ArgumentNullException(nameof(directory));
            }

            if (suffix is null)
            {
                throw new ArgumentNullException(nameof(suffix));
            }

            if (suffix == String.Empty)
            {
                return;
            }

            if (!directory.Exists)
            {
                throw new DirectoryNotFoundException($"Directory {directory.FullName} does not exist or could not be found.");
            }

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
        public static void DeleteAllFilesWithoutSuffix(this DirectoryInfo directory, String suffix, SearchOption option = SearchOption.AllDirectories)
        {
            if (directory is null)
            {
                throw new ArgumentNullException(nameof(directory));
            }

            if (suffix is null)
            {
                throw new ArgumentNullException(nameof(suffix));
            }

            if (suffix == String.Empty)
            {
                return;
            }

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
        public static void DeleteAllFilesWithSuffix(this DirectoryInfo directory, String suffix, SearchOption option = SearchOption.AllDirectories)
        {
            if (directory is null)
            {
                throw new ArgumentNullException(nameof(directory));
            }
            
            if (suffix is null)
            {
                throw new ArgumentNullException(nameof(suffix));
            }

            if (suffix == String.Empty)
            {
                return;
            }

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
        public static void DeleteAllWithoutSuffix(this DirectoryInfo directory, String suffix, SearchOption option = SearchOption.AllDirectories)
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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public static void DeleteAllWithoutSuffix(this DirectoryInfo directory, String suffix, SearchOption option, IEnumerable<String> ignored)
        {
            if (directory is null)
            {
                throw new ArgumentNullException(nameof(directory));
            }
            
            if (suffix is null)
            {
                throw new ArgumentNullException(nameof(suffix));
            }

            if (suffix == String.Empty)
            {
                return;
            }

            ignored = ignored.Materialize();
            
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
            if (directory is null)
            {
                throw new ArgumentNullException(nameof(directory));
            }
            
            if (suffix is null)
            {
                throw new ArgumentNullException(nameof(suffix));
            }

            if (suffix == String.Empty)
            {
                return;
            }

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
        public static void RemoveSuffix(this DirectoryInfo directory, String suffix)
        {
            if (directory is null)
            {
                throw new ArgumentNullException(nameof(directory));
            }

            if (suffix is null)
            {
                throw new ArgumentNullException(nameof(suffix));
            }

            if (suffix == String.Empty)
            {
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
        public static void RemoveSuffixFromAll(this DirectoryInfo directory, String suffix, SearchOption option = SearchOption.AllDirectories)
        {
            if (directory is null)
            {
                throw new ArgumentNullException(nameof(directory));
            }

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
        public static void RemoveSuffixFromAllDirectories(this DirectoryInfo directory, String suffix, SearchOption option = SearchOption.AllDirectories)
        {
            if (directory is null)
            {
                throw new ArgumentNullException(nameof(directory));
            }

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
        public static void RemoveSuffixFromAllFiles(this DirectoryInfo directory, String suffix, SearchOption option = SearchOption.AllDirectories)
        {
            if (directory is null)
            {
                throw new ArgumentNullException(nameof(directory));
            }

            FileInfo[] files = directory.GetFiles("*" + suffix, option);
            foreach (FileInfo file in files)
            {
                FileUtils.RemoveSuffix(file, suffix);
            }
        }

        public static String GetPath(this Environment.SpecialFolder folder)
        {
            return Environment.GetFolderPath(folder);
        }
        
        public static DirectoryInfo ToDirectoryInfo(this Environment.SpecialFolder folder)
        {
            return new DirectoryInfo(GetPath(folder));
        }
    }
}