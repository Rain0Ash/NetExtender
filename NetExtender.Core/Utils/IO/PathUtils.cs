// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using NetExtender.Utils.Types;

namespace NetExtender.Utils.IO
{
    [Flags]
    public enum PathType : Byte
    {
        None = 0,
        LocalFolder = 1,
        LocalFile = 2,
        LocalPath = 3,
        NetworkFolder = 4,
        Folder = 5,
        NetworkFile = 8,
        File = 10,
        NetworkPath = 12,
        All = 15
    }

    public enum PathStatus : Byte
    {
        All,
        Exist,
        NotExist
    }

    public enum PathAction : Byte
    {
        None,
        Standard,
        Force
    }

    public static class PathUtils
    {
        public static readonly Char[] Separators = {Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar};

        /// <summary>
        /// Gets the root directory information of the system drive. The system drive is considered the one on which the windows directory is located.
        /// </summary>
        public static String? GetSystemDrive()
        {
            String windows = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
            return Path.GetPathRoot(windows);
        }

        public static Boolean HasAttribute(this FileSystemInfo info, FileAttributes attributes)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            return (info.Attributes & attributes) == attributes;
        }

        public static String? GetRelativePath(String path)
        {
            return GetRelativePath(path, null);
        }

        public static String? GetRelativePath(String path, String? reference)
        {
            if (path is null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            if (path.Length <= 0)
            {
                return String.Empty;
            }

            if (String.IsNullOrEmpty(reference))
            {
                reference = Directory.GetCurrentDirectory();
            }

            try
            {
                Uri uri = new Uri(path);
                Uri refuri = new Uri(reference + Path.DirectorySeparatorChar);
                return Uri.UnescapeDataString(refuri.MakeRelativeUri(uri).ToString().Replace('/', Path.DirectorySeparatorChar));
            }
            catch (UriFormatException)
            {
                return null;
            }
        }

        public static String ChangeExtension(String path, String? extension)
        {
            if (path is null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            return Path.ChangeExtension(path, extension);
        }

        public static String RemoveIllegalChars(String path)
        {
            if (path is null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            String regex = new String(Path.GetInvalidFileNameChars()) + new String(Path.GetInvalidPathChars());
            return Regex.Replace(path, $"[{Regex.Escape(regex)}]", String.Empty);
        }

        public static Boolean IsFullPath(String path)
        {
            if (path is null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            try
            {
                return Path.IsPathRooted(path) && !Path.DirectorySeparatorChar.ToString().Equals(Path.GetPathRoot(path), StringComparison.Ordinal);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static String? GetFullPath(String path)
        {
            if (path is null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            try
            {
                return Path.GetFullPath(String.IsNullOrEmpty(path) ? "." : path);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static String ConvertToFolder(String path)
        {
            if (path is null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            return path.EndsWith(Separators) ? path : $"{path}{Path.DirectorySeparatorChar}";
        }

        public static Boolean IsAbsolutePath(String path)
        {
            if (path is null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            return IsValidPath(path) && !IsNetworkPath(path) && Path.IsPathRooted(path) &&
                   Path.GetPathRoot(path)?.Equals(Path.DirectorySeparatorChar.ToString(), StringComparison.Ordinal) == false;
        }

        public static FileSystemInfo? GetInfo(String path)
        {
            if (path is null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            if (!IsValidPath(path))
            {
                throw new ArgumentException(@"Invalid path", nameof(path));
            }

            if (!IsExist(path))
            {
                return GetPathType(path) switch
                {
                    PathType.None => throw new IOException($"Invalid path '{path}'"),
                    PathType.LocalFolder => new DirectoryInfo(path),
                    PathType.LocalFile => new FileInfo(path),
                    PathType.LocalPath => new FileInfo(path),
                    PathType.NetworkFolder => new DirectoryInfo(path),
                    PathType.Folder => new DirectoryInfo(path),
                    PathType.NetworkFile => new FileInfo(path),
                    PathType.File => new FileInfo(path),
                    PathType.NetworkPath => new FileInfo(path),
                    PathType.All => new FileInfo(path),
                    _ => throw new NotSupportedException()
                };
            }
            
            if (IsExistAsFile(path))
            {
                return new FileInfo(path);
            }

            if (IsExistAsFolder(path))
            {
                return new DirectoryInfo(path);
            }

            return null;
        }

        public static PathType GetPathType(String path)
        {
            if (path is null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            if (IsValidNetworkPath(path))
            {
                return Separators.Any(path.EndsWith) ? PathType.NetworkFolder : PathType.NetworkFile;
            }

            if (IsValidPath(path, false))
            {
                return Separators.Any(path.EndsWith) ? PathType.LocalFolder : PathType.LocalFile;
            }

            return PathType.None;
        }

        private static Boolean CheckValidPath(String path, PathType type)
        {
            if (path is null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            return type switch
            {
                PathType.None => false,
                PathType.LocalFolder => IsValidFolderPath(path, false),
                PathType.LocalFile => IsValidFilePath(path, false),
                PathType.LocalPath => IsValidPath(path, false),
                PathType.NetworkFolder => IsValidNetworkFolderPath(path),
                PathType.Folder => IsValidFolderPath(path),
                PathType.LocalFile | PathType.NetworkFolder => IsValidFilePath(path, false) || IsValidNetworkFolderPath(path),
                PathType.Folder | PathType.LocalFile => IsValidPath(path, false) || IsValidNetworkFolderPath(path),
                PathType.NetworkFile => IsValidNetworkFilePath(path),
                PathType.LocalFolder | PathType.NetworkFile => IsValidFolderPath(path, false) || IsValidNetworkFilePath(path),
                PathType.File => IsValidFilePath(path),
                PathType.LocalFolder | PathType.LocalFile | PathType.NetworkFile => IsValidPath(path, false) || IsValidNetworkFilePath(path),
                PathType.NetworkPath => IsValidNetworkPath(path),
                PathType.Folder | PathType.NetworkFile => IsValidFolderPath(path) || IsValidNetworkPath(path),
                PathType.File | PathType.NetworkFolder => IsValidFilePath(path) || IsValidNetworkPath(path),
                PathType.All => IsValidPath(path),
                _ => IsValidPath(path)
            };
        }

        public static Boolean IsValidPath(String path, PathType type, PathStatus status = PathStatus.All)
        {
            if (path is null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            if (!CheckValidPath(path, type))
            {
                return false;
            }

            return status switch
            {
                PathStatus.All => true,
                PathStatus.Exist => CheckExist(path),
                PathStatus.NotExist => !CheckExist(path),
                _ => false
            };
        }

        public static Boolean IsPathContainsEndSeparator(String path)
        {
            if (path is null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            path = path.Trim();
            return path.Length > 0 && Separators.Any(chr => path.EndsWith(chr.ToString()));
        }
        
        public static Char GetPathSeparator(String path)
        {
            if (path is null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            path = path.Trim();
            return path.Length > 0 && path.Where(Separators.Contains).AllSame(out Char separator) ? separator : Separators[0];
        }

        public static String AddEndSeparator(String path)
        {
            if (path is null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            path = path.TrimEnd();

            if (IsPathContainsEndSeparator(path))
            {
                return path;
            }

            return path + GetPathSeparator(path);
        }

        public static Boolean IsValidPath(String path, Boolean network = true)
        {
            if (path is null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            return !path.IsNullOrEmpty() && !GetFullPath(path).IsNullOrEmpty() && (!IsNetworkPath(path) || network && IsValidNetworkPath(path));
        }

        public static Boolean IsValidFolderPath(String path, Boolean network = true)
        {
            return IsValidPath(path, network);
        }

        public static Boolean IsValidFolderPath(String path, Boolean network, Boolean endseparator)
        {
            if (path is null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            return IsValidFolderPath(path, network) && (!endseparator || IsPathContainsEndSeparator(path));
        }

        public static Boolean IsValidFilePath(String path, Boolean network = true)
        {
            if (path is null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            return IsValidPath(path, network) && !IsPathContainsEndSeparator(path);
        }

        public static Boolean IsNetworkPath(String path)
        {
            if (path is null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            return !String.IsNullOrEmpty(path) && path.StartsWith("\\");
        }

        public static Boolean IsValidNetworkPath(String path)
        {
            if (path is null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            try
            {
                return !path.IsNullOrEmpty() && IsNetworkPath(path) && new Uri(path).IsUnc &&
                       Regex.IsMatch(path, @"^\\{2}[\w-]+(\\{1}(([\w-][\w-\s]*[\w-]+[$$]?)|([\w-][$$]?$)))+");
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static Boolean IsValidNetworkFolderPath(String path)
        {
            if (path is null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            return IsValidNetworkPath(path);
        }

        public static Boolean IsValidNetworkFolderPath(String path, Boolean endseparator)
        {
            if (path is null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            return IsValidNetworkFolderPath(path) && (!endseparator || IsPathContainsEndSeparator(path));
        }

        public static Boolean IsValidNetworkFilePath(String path)
        {
            if (path is null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            return IsValidNetworkPath(path) && !IsPathContainsEndSeparator(path);
        }

        public static Boolean IsExistAsFolder(String path)
        {
            if (path is null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            return IsValidFolderPath(path) && Directory.Exists(path);
        }

        public static Boolean IsExistAsFile(String path)
        {
            if (path is null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            return IsValidFilePath(path) && File.Exists(path);
        }

        private static Boolean CheckExist(String path)
        {
            if (path is null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            return IsExistAsFolder(path) || IsExistAsFile(path);
        }

        public static Boolean IsExist(String path, PathType type = PathType.All)
        {
            if (path is null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            return IsValidPath(path, type) && CheckExist(path);
        }

        public static Boolean IsValidWebPath(String path)
        {
            if (path is null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            return Uri.TryCreate(path, UriKind.Absolute, out Uri? result) && (result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps);
        }

        public static String TrimPath(String path)
        {
            if (path is null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            return path.Trim(Separators);
        }

        public static String TrimPathStart(String path)
        {
            if (path is null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            return path.TrimStart(Separators);
        }

        public static String TrimPathEnd(String path)
        {
            if (path is null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            return path.TrimEnd(Separators);
        }

        public static String? GetDirectoryName(String? path)
        {
            return Path.GetDirectoryName(path);
        }
        
        [return: NotNullIfNotNull("path")]
        public static String? GetFileName(String? path)
        {
            return Path.GetFileName(path);
        }
        
        [return: NotNullIfNotNull("path")]
        public static String? GetFileNameWithoutExtension(String? path)
        {
            return Path.GetFileNameWithoutExtension(path);
        }
        
        /// <summary>
        /// Extracts the path of the directory in the DirectoryNotFoundException
        /// </summary>
        /// <param name="exception">The exception</param>
        /// <returns>The path of the directory</returns>
        public static String GetPath(this DirectoryNotFoundException exception)
        {
            if (exception is null)
            {
                throw new ArgumentNullException(nameof(exception));
            }

            return GetPathFromMessage(exception.Message);
        }

        /// <summary>
        /// Extracts the path of the directory in the UnauthorizedAccessException
        /// </summary>
        /// <param name="exception">The exception</param>
        /// <returns>The path of the directory</returns>
        public static String GetPath(this UnauthorizedAccessException exception)
        {
            if (exception is null)
            {
                throw new ArgumentNullException(nameof(exception));
            }

            return GetPathFromMessage(exception.Message);
        }

        private static String GetPathFromMessage(String value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            Int32 first = value.IndexOf('\'') + 1;
            Int32 last = value.LastIndexOf('\'');
            
            Int32 length = last - first;

            return length < 0 ? String.Empty : value.Substring(first, length);
        }
    }
}