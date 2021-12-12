// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using NetExtender.Utilities.Types;

namespace NetExtender.Utilities.IO
{
    public static class DirectoryUtilities
    {
        public static Boolean TryCreateDirectory(String path)
        {
            return TryCreateDirectory(path, out _);
        }
        
        public static Boolean TryCreateDirectory(String path, [MaybeNullWhen(false)] out DirectoryInfo result)
        {
            if (path is null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            try
            {
                result = Directory.CreateDirectory(path);
                return true;
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
        }
        
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
        
        public static Boolean TryDelete(this DirectoryInfo info, Boolean recursive)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            try
            {
                info.Delete(recursive);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Renames the specified directory (adds the specified suffix to the end of the name).
        /// </summary>
        /// <param name="directory">The directory to add the suffix to.</param>
        /// <param name="suffix">The suffix to add to the directory.</param>
        public static Boolean AddSuffix(this DirectoryInfo directory, String suffix)
        {
            return AddSuffixInternal(directory, suffix, true);
        }

        /// <summary>
        /// Renames the specified directory (adds the specified suffix to the end of the name).
        /// </summary>
        /// <param name="directory">The directory to add the suffix to.</param>
        /// <param name="suffix">The suffix to add to the directory.</param>
        public static Boolean TryAddSuffix(this DirectoryInfo directory, String suffix)
        {
            return AddSuffixInternal(directory, suffix, false);
        }

        /// <summary>
        /// Renames the specified directory (adds the specified suffix to the end of the name).
        /// </summary>
        /// <param name="directory">The directory to add the suffix to.</param>
        /// <param name="suffix">The suffix to add to the directory.</param>
        /// <param name="isThrow">Is throw or return successful result.</param>
        // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Local
        private static Boolean AddSuffixInternal(this DirectoryInfo directory, String suffix, Boolean isThrow)
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
                return true;
            }

            try
            {
                String name = directory.Name + suffix;

                String? directoryname = Path.GetDirectoryName(directory.FullName);

                if (String.IsNullOrEmpty(directoryname))
                {
                    if (isThrow)
                    {
                        throw new IOException("Directory name is empty or null");
                    }

                    return false;
                }

                String path = Path.Combine(directoryname, name);
                directory.MoveTo(path);
                
                return true;
            }
            catch (Exception)
            {
                if (isThrow)
                {
                    throw;
                }
                
                return false;
            }
        }
        
        /// <summary>
        /// Adds the specified suffix to all directories and files in the specified directory, using the specified search option.
        /// </summary>
        /// <param name="directory">The directory in which to add the suffix to all directories and files.</param>
        /// <param name="suffix">The suffix to add to all directories and files.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean AddSuffixToAll(this DirectoryInfo directory, String suffix)
        {
            return AddSuffixToAll(directory, suffix, null);
        }
        
        /// <summary>
        /// Adds the specified suffix to all directories and files in the specified directory, using the specified search option.
        /// </summary>
        /// <param name="directory">The directory in which to add the suffix to all directories and files.</param>
        /// <param name="suffix">The suffix to add to all directories and files.</param>
        /// <param name="ignore">A collection of directory names to ignore when adding suffix.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean AddSuffixToAll(this DirectoryInfo directory, String suffix, IEnumerable<String>? ignore)
        {
            return AddSuffixToAll(directory, suffix, SearchOption.AllDirectories, ignore);
        }

        /// <summary>
        /// Adds the specified suffix to all directories and files in the specified directory, using the specified search option.
        /// </summary>
        /// <param name="directory">The directory in which to add the suffix to all directories and files.</param>
        /// <param name="suffix">The suffix to add to all directories and files.</param>
        /// <param name="option">Specifies whether to include only the current directory or all subdirectories.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean AddSuffixToAll(this DirectoryInfo directory, String suffix, SearchOption option)
        {
            return AddSuffixToAll(directory, suffix, option, null);
        }

        /// <summary>
        /// Adds the specified suffix to all directories and files in the specified directory, using the specified search option. If a directory matches any of the values in the provided ignore list, the suffix will not be added to it.
        /// </summary>
        /// <param name="directory">The directory in which to add the suffix to all directories and files.</param>
        /// <param name="suffix">The suffix to add to all directories and files.</param>
        /// <param name="option">Specifies whether to include only the current directory or all subdirectories.</param>
        /// <param name="ignore">A collection of directory names to ignore when adding suffix.</param>
        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public static Boolean AddSuffixToAll(this DirectoryInfo directory, String suffix, SearchOption option, IEnumerable<String>? ignore)
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
                return true;
            }

            ignore = ignore.Materialize();
            
            Boolean successful = AddSuffixToAllFiles(directory, suffix, SearchOption.TopDirectoryOnly) | AddSuffixToAllDirectories(directory, suffix, SearchOption.TopDirectoryOnly, ignore);

            if (option != SearchOption.AllDirectories)
            {
                return successful;
            }

            DirectoryInfo[] subdirectories = directory.GetDirectories("*", SearchOption.TopDirectoryOnly);
            return subdirectories.Aggregate(successful, (current, subdirectory) => current | AddSuffixToAll(subdirectory, suffix, SearchOption.AllDirectories, ignore));
        }
        
        /// <summary>
        /// Adds the specified suffix to all directories in the specified directory, using the specified search option.
        /// </summary>
        /// <param name="directory">The directory in which to add the suffix to all directories.</param>
        /// <param name="suffix">The suffix to add to all directories.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean AddSuffixToAllDirectories(this DirectoryInfo directory, String suffix)
        {
            return AddSuffixToAllDirectories(directory, suffix, null);
        }
        
        /// <summary>
        /// Adds the specified suffix to all directories in the specified directory, using the specified search option.
        /// </summary>
        /// <param name="directory">The directory in which to add the suffix to all directories.</param>
        /// <param name="suffix">The suffix to add to all directories.</param>
        /// <param name="ignore">A collection of directory names to ignore when adding suffix.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean AddSuffixToAllDirectories(this DirectoryInfo directory, String suffix, IEnumerable<String>? ignore)
        {
            return AddSuffixToAllDirectories(directory, suffix, SearchOption.AllDirectories, ignore);
        }

        /// <summary>
        /// Adds the specified suffix to all directories in the specified directory, using the specified search option.
        /// </summary>
        /// <param name="directory">The directory in which to add the suffix to all directories.</param>
        /// <param name="suffix">The suffix to add to all directories.</param>
        /// <param name="option">Specifies whether to include only the current directory or all subdirectories.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean AddSuffixToAllDirectories(this DirectoryInfo directory, String suffix, SearchOption option)
        {
            return AddSuffixToAllDirectories(directory, suffix, option, null);
        }

        /// <summary>
        /// Adds the specified suffix to all directories in the specified directory, using the specified search option. If a directory matches any of the values in the provided ignore list, the suffix will not be added to it.
        /// </summary>
        /// <param name="directory">The directory in which to add the suffix to all directories.</param>
        /// <param name="suffix">The suffix to add to all directories.</param>
        /// <param name="option">Specifies whether to include only the current directory or all subdirectories.</param>
        /// <param name="ignore">A collection of directory names to ignore when adding suffix.</param>
        [SuppressMessage("ReSharper", "CognitiveComplexity")]
        public static Boolean AddSuffixToAllDirectories(this DirectoryInfo directory, String suffix, SearchOption option, IEnumerable<String>? ignore)
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
                return true;
            }
            
            DirectoryInfo[] directories = directory.GetDirectories("*", SearchOption.TopDirectoryOnly);

            if (directories.Length <= 0)
            {
                return true;
            }
            
            HashSet<String>? set = ignore?.ToHashSet();
            
            Boolean successful = false;
            foreach (DirectoryInfo subdirectory in directories)
            {
                if (set is not null && set.Contains(subdirectory.Name))
                {
                    continue;
                }

                if (option == SearchOption.AllDirectories)
                {
                    successful |= AddSuffixToAllDirectories(subdirectory, suffix, SearchOption.AllDirectories, set);
                }

                successful |= AddSuffix(subdirectory, suffix);
            }

            return successful;
        }

        /// <summary>
        /// Adds the specified suffix to all files inside the specified directory, using the specified search option.
        /// </summary>
        /// <param name="directory">The directory in which to add the suffix to all files.</param>
        /// <param name="suffix">The suffix to add to all files.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean AddSuffixToAllFiles(this DirectoryInfo directory, String suffix)
        {
            return AddSuffixToAllFiles(directory, suffix, SearchOption.AllDirectories);
        }

        /// <summary>
        /// Adds the specified suffix to all files inside the specified directory, using the specified search option.
        /// </summary>
        /// <param name="directory">The directory in which to add the suffix to all files.</param>
        /// <param name="suffix">The suffix to add to all files.</param>
        /// <param name="option">Specifies whether to include only the current directory or all subdirectories.</param>
        public static Boolean AddSuffixToAllFiles(this DirectoryInfo directory, String suffix, SearchOption option)
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
                return true;
            }

            FileInfo[] files = directory.GetFiles("*", option);

            if (files.Length <= 0)
            {
                return true;
            }

            Int32 successful = files.Count(file => FileUtilities.TryAddSuffix(file, suffix));
            return successful > 0;
        }

        /// <summary>
        /// Copies the specified source directory to the specified location.
        /// </summary>
        /// <param name="source">The path of the directory to copy.</param>
        /// <param name="destination">The path where to copy.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean CopyDirectory(String source, String destination)
        {
            return CopyDirectory(source, destination, true);
        }

        /// <summary>
        /// Copies the specified source directory to the specified location.
        /// </summary>
        /// <param name="source">The path of the directory to copy.</param>
        /// <param name="destination">The path where to copy.</param>
        /// <param name="overwrite">true to allow an existing file to be overwritten; otherwise, false.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean CopyDirectory(String source, String destination, Boolean overwrite)
        {
            return CopyDirectory(source, destination, true, overwrite);
        }

        /// <summary>
        /// Copies the specified source directory to the specified location.
        /// </summary>
        /// <param name="source">The path of the directory to copy.</param>
        /// <param name="destination">The path where to copy.</param>
        /// <param name="recursive">Determines whether to copy subdirectories.</param>
        /// <param name="overwrite">true to allow an existing file to be overwritten; otherwise, false.</param>
        public static Boolean CopyDirectory(String source, String destination, Boolean recursive, Boolean overwrite)
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
            return CopyDirectory(directory, destination, recursive, overwrite);
        }
        
        /// <summary>
        /// Copies the specified source directory to the specified location.
        /// </summary>
        /// <param name="source">The directory to copy.</param>
        /// <param name="destination">The path where to copy.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean CopyDirectory(this DirectoryInfo source, String destination)
        {
            return CopyDirectory(source, destination, true);
        }

        /// <summary>
        /// Copies the specified source directory to the specified location.
        /// </summary>
        /// <param name="source">The directory to copy.</param>
        /// <param name="destination">The path where to copy.</param>
        /// <param name="overwrite">true to allow an existing file to be overwritten; otherwise, false.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean CopyDirectory(this DirectoryInfo source, String destination, Boolean overwrite)
        {
            return CopyDirectory(source, destination, true, overwrite);
        }

        /// <summary>
        /// Copies the specified source directory to the specified location.
        /// </summary>
        /// <param name="source">The directory to copy.</param>
        /// <param name="destination">The path where to copy.</param>
        /// <param name="recursive">Determines whether to copy subdirectories.</param>
        /// <param name="overwrite">true to allow an existing file to be overwritten; otherwise, false.</param>
        [SuppressMessage("ReSharper", "CognitiveComplexity")]
        public static Boolean CopyDirectory(this DirectoryInfo source, String destination, Boolean recursive, Boolean overwrite)
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
                throw new DirectoryNotFoundException($"Directory {source.FullName} does not exist or could not be found.");
            }

            Boolean successful = false;
            if (!Directory.Exists(destination))
            {
                if (!TryCreateDirectory(destination))
                {
                    return false;
                }

                successful = true;
            }

            // get the files in the top directory and copy them to the new location
            FileInfo[] files = source.GetFiles("*", SearchOption.TopDirectoryOnly);
            foreach (FileInfo file in files)
            {
                String path = Path.Combine(destination, file.Name);
                successful |= file.TryCopyTo(path, overwrite);
            }

            if (!recursive)
            {
                return successful;
            }

            // copy all subdirectories and their contents to the new location
            DirectoryInfo[] subdirectories = source.GetDirectories("*", SearchOption.TopDirectoryOnly);

            if (subdirectories.Length <= 0)
            {
                return successful;
            }
            
            foreach (DirectoryInfo subdirectory in subdirectories)
            {
                String path = Path.Combine(destination, subdirectory.Name);
                successful |= CopyDirectory(subdirectory, path, true, overwrite);
            }

            return successful;
        }

        /// <summary>
        /// Copies all files whose name starts with the specified prefix in the specified directory, including subdirectories.
        /// <para>If the destination directory doesn't exist, it is created.</para>
        /// </summary>
        /// <param name="source">The path of the source directory.</param>
        /// <param name="destination">The path of the directory where to copy files.</param>
        /// <param name="prefix">The prefix that files must contain in order to be copied.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean CopyAllFilesWithPrefix(String source, String destination, String prefix)
        {
            return CopyAllFilesWithPrefix(source, destination, prefix, true);
        }
        
        /// <summary>
        /// Copies all files whose name starts with the specified prefix in the specified directory, including subdirectories.
        /// <para>If the destination directory doesn't exist, it is created.</para>
        /// </summary>
        /// <param name="source">The path of the source directory.</param>
        /// <param name="destination">The path of the directory where to copy files.</param>
        /// <param name="prefix">The prefix that files must contain in order to be copied.</param>
        /// <param name="overwrite">true to allow an existing file to be overwritten; otherwise, false.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean CopyAllFilesWithPrefix(String source, String destination, String prefix, Boolean overwrite)
        {
            return CopyAllFilesWithPrefix(source, destination, prefix, SearchOption.AllDirectories, overwrite);
        }

        /// <summary>
        /// Copies all files whose name starts with the specified prefix in the specified directory, using the specified search option.
        /// <para>If the destination directory doesn't exist, it is created.</para>
        /// </summary>
        /// <param name="source">The path of the source directory.</param>
        /// <param name="destination">The path of the directory where to copy files.</param>
        /// <param name="prefix">The prefix that files must contain in order to be copied.</param>
        /// <param name="option">Specifies whether to include only the current directory or all subdirectories.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean CopyAllFilesWithPrefix(String source, String destination, String prefix, SearchOption option)
        {
            return CopyAllFilesWithPrefix(source, destination, prefix, option, true);
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean CopyAllFilesWithPrefix(String source, String destination, String prefix, SearchOption option, Boolean overwrite)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            DirectoryInfo info = new DirectoryInfo(source);
            return CopyAllFilesWithPrefix(info, destination, prefix, option, overwrite);
        }

        /// <summary>
        /// Copies all files whose name starts with the specified prefix in the specified directory, including subdirectories.
        /// <para>If the destination directory doesn't exist, it is created.</para>
        /// </summary>
        /// <param name="source">The source directory.</param>
        /// <param name="destination">The path of the directory where to copy files.</param>
        /// <param name="prefix">The prefix that files must contain in order to be copied.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean CopyAllFilesWithPrefix(this DirectoryInfo source, String destination, String prefix)
        {
            return CopyAllFilesWithPrefix(source, destination, prefix, true);
        }
        
        /// <summary>
        /// Copies all files whose name starts with the specified prefix in the specified directory, including subdirectories.
        /// <para>If the destination directory doesn't exist, it is created.</para>
        /// </summary>
        /// <param name="source">The source directory.</param>
        /// <param name="destination">The path of the directory where to copy files.</param>
        /// <param name="prefix">The prefix that files must contain in order to be copied.</param>
        /// <param name="option">Specifies whether to include only the current directory or all subdirectories.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean CopyAllFilesWithPrefix(this DirectoryInfo source, String destination, String prefix, SearchOption option)
        {
            return CopyAllFilesWithPrefix(source, destination, prefix, option, true);
        }
        
        /// <summary>
        /// Copies all files whose name starts with the specified prefix in the specified directory, including subdirectories.
        /// <para>If the destination directory doesn't exist, it is created.</para>
        /// </summary>
        /// <param name="source">The source directory.</param>
        /// <param name="destination">The path of the directory where to copy files.</param>
        /// <param name="prefix">The prefix that files must contain in order to be copied.</param>
        /// <param name="overwrite">true to allow an existing file to be overwritten; otherwise, false.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean CopyAllFilesWithPrefix(this DirectoryInfo source, String destination, String prefix, Boolean overwrite)
        {
            return CopyAllFilesWithPrefix(source, destination, prefix, SearchOption.AllDirectories, overwrite);
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
        public static Boolean CopyAllFilesWithPrefix(this DirectoryInfo source, String destination, String prefix, SearchOption option, Boolean overwrite)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (String.IsNullOrEmpty(destination))
            {
                throw new ArgumentException(@"Value cannot be null or empty.", nameof(destination));
            }

            Boolean successful = false;
            if (!Directory.Exists(destination))
            {
                if (!TryCreateDirectory(destination))
                {
                    return false;
                }

                successful = true;
            }

            FileInfo[] files = source.GetFiles($"{prefix}*", SearchOption.TopDirectoryOnly);
            foreach (FileInfo file in files)
            {
                String path = Path.Combine(destination, file.Name);
                successful |= file.TryCopyTo(path, overwrite);
            }

            if (option != SearchOption.AllDirectories)
            {
                return successful;
            }

            DirectoryInfo[] subdirectories = source.GetDirectories("*", SearchOption.TopDirectoryOnly);

            if (subdirectories.Length <= 0)
            {
                return successful;
            }

            foreach (DirectoryInfo subdirectory in subdirectories)
            {
                String path = Path.Combine(destination, subdirectory.Name);
                successful |= CopyAllFilesWithPrefix(subdirectory, path, prefix, SearchOption.AllDirectories, overwrite);
            }

            return successful;
        }
        
        /// <summary>
        /// Deletes all directories in the specified directory, whose name is not ended by the specified suffix, using the specified search option.
        /// </summary>
        /// <param name="directory">The directory in which to delete all directories.</param>
        /// <param name="suffix">The suffix that directories must not contain in order to be deleted.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean DeleteAllDirectoriesWithoutSuffix(this DirectoryInfo directory, String suffix)
        {
            return DeleteAllDirectoriesWithoutSuffix(directory, suffix, null);
        }
        
        /// <summary>
        /// Deletes all directories in the specified directory, whose name is not ended by the specified suffix, using the specified search option.
        /// </summary>
        /// <param name="directory">The directory in which to delete all directories.</param>
        /// <param name="suffix">The suffix that directories must not contain in order to be deleted.</param>
        /// <param name="ignore">A collection of directory names to ignore.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean DeleteAllDirectoriesWithoutSuffix(this DirectoryInfo directory, String suffix, IEnumerable<String>? ignore)
        {
            return DeleteAllDirectoriesWithoutSuffix(directory, suffix, SearchOption.AllDirectories, ignore);
        }
        
        /// <summary>
        /// Deletes all directories in the specified directory, whose name is not ended by the specified suffix, using the specified search option.
        /// </summary>
        /// <param name="directory">The directory in which to delete all directories.</param>
        /// <param name="suffix">The suffix that directories must not contain in order to be deleted.</param>
        /// <param name="option">Specifies whether to include only the current directory or all subdirectories.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean DeleteAllDirectoriesWithoutSuffix(this DirectoryInfo directory, String suffix, SearchOption option)
        {
            return DeleteAllDirectoriesWithoutSuffix(directory, suffix, option, null);
        }

        /// <summary>
        /// Deletes all directories in the specified directory, whose name is not ended by the specified suffix, using the specified search option. If a directory matches any of the values in the provided ignore list, it will not be deleted.
        /// </summary>
        /// <param name="directory">The directory in which to delete all directories.</param>
        /// <param name="suffix">The suffix that directories must not contain in order to be deleted.</param>
        /// <param name="option">Specifies whether to include only the current directory or all subdirectories.</param>
        /// <param name="ignore">A collection of directory names to ignore.</param>
        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        [SuppressMessage("ReSharper", "CognitiveComplexity")]
        public static Boolean DeleteAllDirectoriesWithoutSuffix(this DirectoryInfo directory, String suffix, SearchOption option, IEnumerable<String>? ignore)
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
                return true;
            }
            
            if (!directory.Exists)
            {
                throw new DirectoryNotFoundException($"Directory {directory.FullName} does not exist or could not be found.");
            }

            DirectoryInfo[] subdirectories = directory.GetDirectories("*", SearchOption.TopDirectoryOnly);

            if (subdirectories.Length <= 0)
            {
                return true;
            }
            
            HashSet<String>? set = ignore?.ToHashSet();

            Boolean successful = false;
            for (Int32 i = subdirectories.Length - 1; i >= 0; --i)
            {
                DirectoryInfo subdirectory = subdirectories[i];

                if (set is not null && set.Contains(subdirectory.Name))
                {
                    successful = true;
                    continue;
                }

                if (!subdirectory.Name.EndsWith(suffix))
                {
                    successful |= subdirectories[i].TryDelete(true);
                    continue;
                }

                if (option == SearchOption.AllDirectories)
                {
                    successful |= DeleteAllDirectoriesWithoutSuffix(subdirectory, suffix, SearchOption.AllDirectories, set);
                }
            }

            return successful;
        }

        /// <summary>
        /// Deletes all directories in the specified directory, whose name is ended by the specified suffix, using the specified search option.
        /// </summary>
        /// <param name="directory">The directory in which to delete all directories.</param>
        /// <param name="suffix">The suffix that directories must contain in order to be deleted.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean DeleteAllDirectoriesWithSuffix(this DirectoryInfo directory, String suffix)
        {
            return DeleteAllDirectoriesWithSuffix(directory, suffix, SearchOption.AllDirectories);
        }

        /// <summary>
        /// Deletes all directories in the specified directory, whose name is ended by the specified suffix, using the specified search option.
        /// </summary>
        /// <param name="directory">The directory in which to delete all directories.</param>
        /// <param name="suffix">The suffix that directories must contain in order to be deleted.</param>
        /// <param name="option">Specifies whether to include only the current directory or all subdirectories.</param>
        public static Boolean DeleteAllDirectoriesWithSuffix(this DirectoryInfo directory, String suffix, SearchOption option)
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
                return true;
            }

            if (!directory.Exists)
            {
                throw new DirectoryNotFoundException($"Directory {directory.FullName} does not exist or could not be found.");
            }

            DirectoryInfo[] subdirectories = directory.GetDirectories("*", SearchOption.TopDirectoryOnly);

            if (subdirectories.Length <= 0)
            {
                return true;
            }

            Boolean successful = false;
            for (Int32 i = subdirectories.Length - 1; i >= 0; i--)
            {
                DirectoryInfo subdirectory = subdirectories[i];

                if (subdirectory.Name.EndsWith(suffix))
                {
                    successful |= subdirectories[i].TryDelete(true);
                    continue;
                }

                if (option == SearchOption.AllDirectories)
                {
                    successful |= DeleteAllDirectoriesWithSuffix(subdirectory, suffix);
                }
            }

            return successful;
        }

        /// <summary>
        /// Deletes all files in the specified directory, whose name is not ended by the specified suffix, using the specified search option.
        /// </summary>
        /// <param name="directory">The directory in which to delete all files.</param>
        /// <param name="suffix">The suffix that files must not contain in order to be deleted.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean DeleteAllFilesWithoutSuffix(this DirectoryInfo directory, String suffix)
        {
            return DeleteAllFilesWithoutSuffix(directory, suffix, SearchOption.AllDirectories);
        }

        /// <summary>
        /// Deletes all files in the specified directory, whose name is not ended by the specified suffix, using the specified search option.
        /// </summary>
        /// <param name="directory">The directory in which to delete all files.</param>
        /// <param name="suffix">The suffix that files must not contain in order to be deleted.</param>
        /// <param name="option">Specifies whether to include only the current directory or all subdirectories.</param>
        public static Boolean DeleteAllFilesWithoutSuffix(this DirectoryInfo directory, String suffix, SearchOption option)
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
                return true;
            }

            FileInfo[] files = directory.GetFiles("*", option);

            if (files.Length <= 0)
            {
                return true;
            }
            
            Int32 successful = files.Count(file => !file.Name.EndsWith(suffix) && file.TryDelete());
            return successful > 0;
        }

        /// <summary>
        /// Deletes all files in the specified directory, whose name is ended by the specified suffix, using the specified search option.
        /// </summary>
        /// <param name="directory">The directory in which to delete all files.</param>
        /// <param name="suffix">The suffix that files must contain in order to be deleted.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean DeleteAllFilesWithSuffix(this DirectoryInfo directory, String suffix)
        {
            return DeleteAllWithSuffix(directory, suffix, SearchOption.AllDirectories);
        }

        /// <summary>
        /// Deletes all files in the specified directory, whose name is ended by the specified suffix, using the specified search option.
        /// </summary>
        /// <param name="directory">The directory in which to delete all files.</param>
        /// <param name="suffix">The suffix that files must contain in order to be deleted.</param>
        /// <param name="option">Specifies whether to include only the current directory or all subdirectories.</param>
        public static Boolean DeleteAllFilesWithSuffix(this DirectoryInfo directory, String suffix, SearchOption option)
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
                return true;
            }

            FileInfo[] files = directory.GetFiles("*" + suffix, option);

            if (files.Length <= 0)
            {
                return true;
            }
            
            Int32 successful = files.Count(file => file.TryDelete());
            return successful > 0;
        }

        /// <summary>
        /// Deletes all files and directories in the specified directory, whose name is not ended by the specified suffix, using the specified search option.
        /// </summary>
        /// <param name="directory">The directory in which to delete all files and directories.</param>
        /// <param name="suffix">The suffix that files and directories must not contain in order to be deleted.</param>
        public static Boolean DeleteAllWithoutSuffix(this DirectoryInfo directory, String suffix)
        {
            return DeleteAllWithoutSuffix(directory, suffix, null);
        }
        
        /// <summary>
        /// Deletes all files and directories in the specified directory, whose name is not ended by the specified suffix, using the specified search option.
        /// </summary>
        /// <param name="directory">The directory in which to delete all files and directories.</param>
        /// <param name="suffix">The suffix that files and directories must not contain in order to be deleted.</param>
        /// <param name="ignore">A collection of directory names to ignore.</param>
        public static Boolean DeleteAllWithoutSuffix(this DirectoryInfo directory, String suffix, IEnumerable<String>? ignore)
        {
            return DeleteAllWithoutSuffix(directory, suffix, SearchOption.AllDirectories, ignore);
        }
        
        /// <summary>
        /// Deletes all files and directories in the specified directory, whose name is not ended by the specified suffix, using the specified search option.
        /// </summary>
        /// <param name="directory">The directory in which to delete all files and directories.</param>
        /// <param name="suffix">The suffix that files and directories must not contain in order to be deleted.</param>
        /// <param name="option">Specifies whether to include only the current directory or all subdirectories.</param>
        public static Boolean DeleteAllWithoutSuffix(this DirectoryInfo directory, String suffix, SearchOption option)
        {
            return DeleteAllWithoutSuffix(directory, suffix, option, null);
        }

        /// <summary>
        /// Deletes all files and directories in the specified directory, whose name is not ended by the specified suffix, using the specified search option. If a directory matches any of the values in the provided ignore list, it will not be deleted.
        /// </summary>
        /// <param name="directory">The directory in which to delete all files and directories.</param>
        /// <param name="suffix">The suffix that files and directories must not contain in order to be deleted.</param>
        /// <param name="option">Specifies whether to include only the current directory or all subdirectories.</param>
        /// <param name="ignore">A collection of directory names to ignore.</param>
        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public static Boolean DeleteAllWithoutSuffix(this DirectoryInfo directory, String suffix, SearchOption option, IEnumerable<String>? ignore)
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
                return true;
            }

            ignore = ignore.Materialize();
            
            Boolean successful = DeleteAllFilesWithoutSuffix(directory, suffix, SearchOption.TopDirectoryOnly) | DeleteAllDirectoriesWithoutSuffix(directory, suffix, SearchOption.TopDirectoryOnly, ignore);

            if (option != SearchOption.AllDirectories)
            {
                return successful;
            }

            DirectoryInfo[] subdirectories = directory.GetDirectories("*", SearchOption.TopDirectoryOnly);

            return subdirectories.Aggregate(successful, (current, subdirectory) => current | DeleteAllWithoutSuffix(subdirectory, suffix, SearchOption.AllDirectories, ignore));
        }

        /// <summary>
        /// Deletes all files and directories in the specified directory, whose name is ended by the specified suffix, using the specified search option.
        /// </summary>
        /// <param name="directory">The directory in which to delete all files and directories.</param>
        /// <param name="suffix">The suffix that files and directories must contain in order to be deleted.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean DeleteAllWithSuffix(this DirectoryInfo directory, String suffix)
        {
            return DeleteAllWithSuffix(directory, suffix, SearchOption.AllDirectories);
        }

        /// <summary>
        /// Deletes all files and directories in the specified directory, whose name is ended by the specified suffix, using the specified search option.
        /// </summary>
        /// <param name="directory">The directory in which to delete all files and directories.</param>
        /// <param name="suffix">The suffix that files and directories must contain in order to be deleted.</param>
        /// <param name="option">Specifies whether to include only the current directory or all subdirectories.</param>
        public static Boolean DeleteAllWithSuffix(this DirectoryInfo directory, String suffix, SearchOption option)
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
                return true;
            }

            Boolean successful = DeleteAllFilesWithSuffix(directory, suffix, SearchOption.TopDirectoryOnly) | DeleteAllDirectoriesWithSuffix(directory, suffix, SearchOption.TopDirectoryOnly);

            if (option != SearchOption.AllDirectories)
            {
                return successful;
            }

            DirectoryInfo[] subdirectories = directory.GetDirectories("*", SearchOption.TopDirectoryOnly);
            return subdirectories.Aggregate(successful, (current, subdirectory) => current | DeleteAllWithSuffix(subdirectory, suffix));
        }

        /// <summary>
        /// Renames the specified directory (removes the specified suffix from the end of the name, if present).
        /// </summary>
        /// <param name="directory">The directory to remove the suffix from.</param>
        /// <param name="suffix">The suffix to remove from the directory.</param>
        public static Boolean RemoveSuffix(this DirectoryInfo directory, String suffix)
        {
            return RemoveSuffixInternal(directory, suffix, true);
        }
        
        /// <summary>
        /// Renames the specified directory (removes the specified suffix from the end of the name, if present).
        /// </summary>
        /// <param name="directory">The directory to remove the suffix from.</param>
        /// <param name="suffix">The suffix to remove from the directory.</param>
        public static Boolean TryRemoveSuffix(this DirectoryInfo directory, String suffix)
        {
            return RemoveSuffixInternal(directory, suffix, false);
        }

        /// <summary>
        /// Renames the specified directory (removes the specified suffix from the end of the name, if present).
        /// </summary>
        /// <param name="directory">The directory to remove the suffix from.</param>
        /// <param name="suffix">The suffix to remove from the directory.</param>
        /// <param name="isThrow">Is throw or return successful result.</param>
        // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Local
        private static Boolean RemoveSuffixInternal(this DirectoryInfo directory, String suffix, Boolean isThrow)
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
                return true;
            }
            
            if (!directory.Name.EndsWith(suffix))
            {
                return true;
            }

            try
            {
                String name = directory.Name.Substring(0, directory.Name.Length - suffix.Length);

                String? directoryname = Path.GetDirectoryName(directory.FullName);

                if (String.IsNullOrEmpty(directoryname))
                {
                    if (isThrow)
                    {
                        throw new IOException("Directory name is empty or null");
                    }

                    return false;
                }

                String path = Path.Combine(directoryname, name);
                directory.MoveTo(path);

                return true;
            }
            catch (Exception)
            {
                if (isThrow)
                {
                    throw;
                }

                return false;
            }
        }

        /// <summary>
        /// Removes the specified suffix from all directories and files in the specified directory, using the specified search option.
        /// </summary>
        /// <param name="directory">The directory in which to remove the suffix from all directories and files.</param>
        /// <param name="suffix">The suffix to remove from all directories and files.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean RemoveSuffixFromAll(this DirectoryInfo directory, String suffix)
        {
            return RemoveSuffixFromAll(directory, suffix, SearchOption.AllDirectories);
        }

        /// <summary>
        /// Removes the specified suffix from all directories and files in the specified directory, using the specified search option.
        /// </summary>
        /// <param name="directory">The directory in which to remove the suffix from all directories and files.</param>
        /// <param name="suffix">The suffix to remove from all directories and files.</param>
        /// <param name="option">Specifies whether to include only the current directory or all subdirectories.</param>
        public static Boolean RemoveSuffixFromAll(this DirectoryInfo directory, String suffix, SearchOption option)
        {
            if (directory is null)
            {
                throw new ArgumentNullException(nameof(directory));
            }

            Boolean successful = RemoveSuffixFromAllFiles(directory, suffix, SearchOption.TopDirectoryOnly) | RemoveSuffixFromAllDirectories(directory, suffix, SearchOption.TopDirectoryOnly);

            if (option != SearchOption.AllDirectories)
            {
                return true;
            }

            DirectoryInfo[] subdirectories = directory.GetDirectories("*", SearchOption.TopDirectoryOnly);
            return subdirectories.Aggregate(successful, (current, subdirectory) => current | RemoveSuffixFromAll(subdirectory, suffix));
        }

        /// <summary>
        /// Removes the specified suffix from all directories in the specified directory, using the specified search option.
        /// </summary>
        /// <param name="directory">The directory in which to remove the suffix from all directories.</param>
        /// <param name="suffix">The suffix to remove from all directories.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean RemoveSuffixFromAllDirectories(this DirectoryInfo directory, String suffix)
        {
            return RemoveSuffixFromAllDirectories(directory, suffix, SearchOption.AllDirectories);
        }

        /// <summary>
        /// Removes the specified suffix from all directories in the specified directory, using the specified search option.
        /// </summary>
        /// <param name="directory">The directory in which to remove the suffix from all directories.</param>
        /// <param name="suffix">The suffix to remove from all directories.</param>
        /// <param name="option">Specifies whether to include only the current directory or all subdirectories.</param>
        public static Boolean RemoveSuffixFromAllDirectories(this DirectoryInfo directory, String suffix, SearchOption option)
        {
            if (directory is null)
            {
                throw new ArgumentNullException(nameof(directory));
            }

            DirectoryInfo[] directories = directory.GetDirectories("*", SearchOption.TopDirectoryOnly);

            if (directories.Length <= 0)
            {
                return true;
            }

            Boolean successful = false;
            foreach (DirectoryInfo subdirectory in directories)
            {
                if (option == SearchOption.AllDirectories)
                {
                    successful |= RemoveSuffixFromAllDirectories(subdirectory, suffix);
                }

                successful |= RemoveSuffix(subdirectory, suffix);
            }

            return successful;
        }

        /// <summary>
        /// Removes the specified suffix from all files inside the specified directory, using the specified search option.
        /// </summary>
        /// <param name="directory">The directory in which to remove the suffix from all directories.</param>
        /// <param name="suffix">The suffix to remove from all directories.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean RemoveSuffixFromAllFiles(this DirectoryInfo directory, String suffix)
        {
            return RemoveSuffixFromAllFiles(directory, suffix, SearchOption.AllDirectories);
        }

        /// <summary>
        /// Removes the specified suffix from all files inside the specified directory, using the specified search option.
        /// </summary>
        /// <param name="directory">The directory in which to remove the suffix from all directories.</param>
        /// <param name="suffix">The suffix to remove from all directories.</param>
        /// <param name="option">Specifies whether to include only the current directory or all subdirectories.</param>
        public static Boolean RemoveSuffixFromAllFiles(this DirectoryInfo directory, String suffix, SearchOption option)
        {
            if (directory is null)
            {
                throw new ArgumentNullException(nameof(directory));
            }

            FileInfo[] files = directory.GetFiles("*" + suffix, option);

            if (files.Length <= 0)
            {
                return true;
            }

            Int32 successful = files.Count(file => FileUtilities.TryRemoveSuffix(file, suffix));
            return successful > 0;
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