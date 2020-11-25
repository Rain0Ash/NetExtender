// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.AccessControl;
using System.Text;
using System.Text.RegularExpressions;
using NetExtender.Utils.Types;
using Microsoft.Win32.SafeHandles;
using NetExtender.IO.NTFS.DataStreams;
using NetExtender.Native;
using NetExtender.Native.IO;
using NetExtender.Types.Network;
using NetExtender.Types.Numerics;

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
        Standart,
        Force
    }

    public static class PathUtils
    {
        public const Int32 MaxPathLength = Byte.MaxValue;
        public const Int32 MaxLongPathLength = UInt16.MaxValue;

        public const String LongPathPrefix = @"\\?\";

        public static readonly Char[] Separators = {Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar};

        /// <summary>
        /// Gets the root directory information of the system drive. The system drive is considered the one on which the windows directory is located.
        /// </summary>
        public static String GetSystemDrive()
        {
            String windows = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
            return Path.GetPathRoot(windows);
        }

        public static String GetRelativePath(String path, String reference = null)
        {
            if (String.IsNullOrEmpty(path))
            {
                return null;
            }

            if (String.IsNullOrEmpty(reference))
            {
                reference = Directory.GetCurrentDirectory();
            }

            try
            {
                Uri pathUri = new Uri(path);
                Uri referenceUri = new Uri(reference + Path.DirectorySeparatorChar);
                return Uri.UnescapeDataString(
                    referenceUri.MakeRelativeUri(pathUri).ToString()
                        .Replace('/', Path.DirectorySeparatorChar));
            }
            catch (UriFormatException)
            {
                return null;
            }
        }

        public static Boolean CheckPermissions(this FileSystemInfo info, FileSystemRights access)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            return info switch
            {
                DirectoryInfo directory => directory.CheckPermissions(access),
                FileInfo file => file.CheckPermissions(access),
                _ => throw new NotSupportedException()
            };
        }

        public static Boolean TryCheckPermissions(this FileSystemInfo info, FileSystemRights access)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            return info switch
            {
                DirectoryInfo directory => directory.TryCheckPermissions(access),
                FileInfo file => file.TryCheckPermissions(access),
                _ => throw new NotSupportedException()
            };
        }

        public static String RemoveIllegalChars(String path)
        {
            String regexSearch = new String(Path.GetInvalidFileNameChars()) + new String(Path.GetInvalidPathChars());
            return Regex.Replace(path, $"[{Regex.Escape(regexSearch)}]", String.Empty);
        }

        public static Boolean IsFullPath(String path)
        {
            if (String.IsNullOrEmpty(path))
            {
                return false;
            }

            try
            {
                // ReSharper disable once PossibleNullReferenceException
                return Path.IsPathRooted(path) && !Path.GetPathRoot(path).Equals(Path.DirectorySeparatorChar.ToString(), StringComparison.Ordinal);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static String GetFullPath(String path)
        {
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
            return path.EndsWith(Separators) ? path : $"{path}{Path.DirectorySeparatorChar}";
        }

        public static Boolean IsAbsolutePath(String path)
        {
            return IsValidPath(path) && !IsNetworkPath(path) && Path.IsPathRooted(path) &&
                   Path.GetPathRoot(path)?.Equals(Path.DirectorySeparatorChar.ToString(), StringComparison.Ordinal) == false;
        }

        public static FileSystemInfo GetInfo(String path)
        {
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
            if (path.IsNullOrEmpty())
            {
                return false;
            }

            return type switch
            {
                PathType.None => false,
                PathType.LocalFolder => IsValidFolderPath(path, false),
                PathType.LocalFile => IsValidFilePath(path, false),
                PathType.LocalPath => IsValidPath(path, false),
                PathType.NetworkFolder => IsValidNetworkFolderPath(path),
                PathType.Folder => IsValidFolderPath(path),
                PathType.LocalFile | PathType.NetworkFolder => (IsValidFilePath(path, false) || IsValidNetworkFolderPath(path)),
                PathType.Folder | PathType.LocalFile => (IsValidPath(path, false) || IsValidNetworkFolderPath(path)),
                PathType.NetworkFile => IsValidNetworkFilePath(path),
                PathType.LocalFolder | PathType.NetworkFile => (IsValidFolderPath(path, false) || IsValidNetworkFilePath(path)),
                PathType.File => IsValidFilePath(path),
                PathType.LocalFolder | PathType.LocalFile | PathType.NetworkFile => (IsValidPath(path, false) ||
                                                                                     IsValidNetworkFilePath(path)),
                PathType.NetworkPath => IsValidNetworkPath(path),
                PathType.Folder | PathType.NetworkFile => (IsValidFolderPath(path) || IsValidNetworkPath(path)),
                PathType.File | PathType.NetworkFolder => (IsValidFilePath(path) || IsValidNetworkPath(path)),
                PathType.All => IsValidPath(path),
                _ => IsValidPath(path)
            };
        }

        public static Boolean IsValidPath(String path, PathType type, PathStatus status = PathStatus.All)
        {
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

        private static Boolean IsPathContainsEndSeparator(String path)
        {
            return Separators.Any(chr => path.EndsWith(chr.ToString()));
        }

        public static Boolean IsValidPath(String path, Boolean network = true)
        {
            return !path.IsNullOrEmpty() && !GetFullPath(path).IsNullOrEmpty() && (!IsNetworkPath(path) || network && IsValidNetworkPath(path));
        }

        public static Boolean IsValidFolderPath(String path, Boolean network = true)
        {
            return IsValidPath(path, network);
        }

        public static Boolean IsValidFolderPath(String path, Boolean network, Boolean endseparator)
        {
            return IsValidFolderPath(path, network) && (!endseparator || IsPathContainsEndSeparator(path));
        }

        public static Boolean IsValidFilePath(String path, Boolean network = true)
        {
            return IsValidPath(path, network) && !IsPathContainsEndSeparator(path);
        }

        public static Boolean IsNetworkPath(String path)
        {
            return !path.IsNullOrEmpty() && path.StartsWith("\\");
        }

        public static Boolean IsValidNetworkPath(String path)
        {
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

        public static Boolean IsValidUrl(String path)
        {
            return !path.IsNullOrEmpty() && Uri.TryCreate(path, UriKind.Absolute, out Uri uriResult) &&
                   (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }

        public static Boolean IsUrlContainData(String path)
        {
            if (!IsValidUrl(path))
            {
                return false;
            }

            using FixedWebClient client = new FixedWebClient {HeadOnly = true};

            return !String.IsNullOrEmpty(client.DownloadString(path));
        }

        public static Boolean IsValidNetworkFolderPath(String path)
        {
            return IsValidNetworkPath(path);
        }

        public static Boolean IsValidNetworkFolderPath(String path, Boolean endseparator)
        {
            return IsValidNetworkFolderPath(path) && (!endseparator || IsPathContainsEndSeparator(path));
        }

        public static Boolean IsValidNetworkFilePath(String path)
        {
            return IsValidNetworkPath(path) && !IsPathContainsEndSeparator(path);
        }

        public static Boolean IsExistAsFolder(String path)
        {
            return IsValidFolderPath(path) && Directory.Exists(path);
        }

        public static Boolean IsExistAsFile(String path)
        {
            return IsValidFilePath(path) && File.Exists(path);
        }

        private static Boolean CheckExist(String path)
        {
            return IsExistAsFolder(path) || IsExistAsFile(path);
        }

        public static Boolean IsExist(String path, PathType type = PathType.All)
        {
            return IsValidPath(path, type) && CheckExist(path);
        }

        public static Boolean IsValidWebPath(String path)
        {
            return Uri.TryCreate(path, UriKind.Absolute, out Uri uriResult)
                   && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }

        public static String TrimPath(String path)
        {
            return path.Trim(Separators);
        }

        public static String TrimPathStart(String path)
        {
            return path.TrimStart(Separators);
        }

        public static String TrimPathEnd(String path)
        {
            return path.TrimEnd(Separators);
        }

        public static String GetDirectoryName(String path)
        {
            return Path.GetDirectoryName(path);
        }
        
        public static String GetFileName(String path)
        {
            return Path.GetFileName(path);
        }
        
        public static String GetFileNameWithoutExtension(String path)
        {
            return Path.GetFileNameWithoutExtension(path);
        }
        
        /// <summary>
        /// Extracts the path of the directory in the DirectoryNotFoundException
        /// </summary>
        /// <param name="ex">The exception</param>
        /// <returns>The path of the directory</returns>
        public static String GetPath(this DirectoryNotFoundException ex)
        {
            return GetPathFromMessage(ex.Message);
        }

        /// <summary>
        /// Extracts the path of the directory in the UnauthorizedAccessException
        /// </summary>
        /// <param name="ex">The exception</param>
        /// <returns>The path of the directory</returns>
        public static String GetPath(this UnauthorizedAccessException ex)
        {
            return GetPathFromMessage(ex.Message);
        }

        private static String GetPathFromMessage(String msg)
        {
            Int32 startIndex = msg.IndexOf('\'') + 1;
            Int32 endIndex = msg.LastIndexOf('\'');
            Int32 length = endIndex - startIndex;

            // Assert that atleast 2 apostrophe exist in the message
            return length < 0 ? String.Empty : msg.Substring(startIndex, length);
        }

        /// <summary>
        /// <span style="font-weight:bold;color:#a00;">(Extension Method)</span><br />
        /// Returns a read-only list of alternate data streams for the specified file.
        /// </summary>
        /// <param name="file">
        /// The <see cref="FileSystemInfo"/> to inspect.
        /// </param>
        /// <returns>
        /// A read-only list of <see cref="AlternateDataStreamInfo"/> objects
        /// representing the alternate data streams for the specified file, if any.
        /// If no streams are found, returns an empty list.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="file"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="FileNotFoundException">
        /// The specified <paramref name="file"/> does not exist.
        /// </exception>
        /// <exception cref="SecurityException">
        /// The caller does not have the required permission. 
        /// </exception>
        /// <exception cref="UnauthorizedAccessException">
        /// The caller does not have the required permission.
        /// </exception>
        public static IDictionary<String, AlternateDataStreamInfo> GetAlternateDataStreams(this FileSystemInfo file)
        {
            return GetAlternateDataStreams(file?.FullName);
        }

        /// <summary>
        /// Returns a read-only list of alternate data streams for the specified file.
        /// </summary>
        /// <param name="path">
        /// The full path of the file to inspect.
        /// </param>
        /// <returns>
        /// A read-only list of <see cref="AlternateDataStreamInfo"/> objects
        /// representing the alternate data streams for the specified file, if any.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="path"/> is <see langword="null"/> or an empty string.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="path"/> is not a valid file path.
        /// </exception>
        /// <exception cref="FileNotFoundException">
        /// The specified <paramref name="path"/> does not exist.
        /// </exception>
        /// <exception cref="SecurityException">
        /// The caller does not have the required permission. 
        /// </exception>
        /// <exception cref="UnauthorizedAccessException">
        /// The caller does not have the required permission.
        /// </exception>
        public static IDictionary<String, AlternateDataStreamInfo> GetAlternateDataStreams(String path)
        {
            if (String.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            if (!Safe.FileExists(path))
            {
                throw new FileNotFoundException(null, path);
            }

            return GetDataStreams(path)
                .Select(info => new AlternateDataStreamInfo(path, info))
                .ToImmutableDictionary(info => info.Name, info => info);
        }

        /// <summary>
        /// <span style="font-weight:bold;color:#a00;">(Extension Method)</span><br />
        /// Returns a flag indicating whether the specified alternate data stream exists.
        /// </summary>
        /// <param name="file">
        /// The <see cref="FileInfo"/> to inspect.
        /// </param>
        /// <param name="name">
        /// The name of the stream to find.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the specified stream exists;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="file"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="name"/> contains invalid characters.
        /// </exception>
        public static Boolean AlternateDataStreamExists(this FileSystemInfo file, String name)
        {
            return AlternateDataStreamExists(file?.FullName, name);
        }

        /// <summary>
        /// Returns a flag indicating whether the specified alternate data stream exists.
        /// </summary>
        /// <param name="path">
        /// The path of the file to inspect.
        /// </param>
        /// <param name="name">
        /// The name of the stream to find.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the specified stream exists;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="path"/> is <see langword="null"/> or an empty string.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <para><paramref name="path"/> is not a valid file path.</para>
        /// <para>-or-</para>
        /// <para><paramref name="name"/> contains invalid characters.</para>
        /// </exception>
        public static Boolean AlternateDataStreamExists(String path, String name)
        {
            if (String.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            ValidateStreamName(name);

            return Safe.FileExists(BuildStreamPath(path, name));
        }

        /// <summary>
        /// <span style="font-weight:bold;color:#a00;">(Extension Method)</span><br />
        /// Opens an alternate data stream.
        /// </summary>
        /// <param name="file">
        /// The <see cref="FileInfo"/> which contains the stream.
        /// </param>
        /// <param name="name">
        /// The name of the stream to open.
        /// </param>
        /// <param name="mode">
        /// One of the <see cref="FileMode"/> values, indicating how the stream is to be opened.
        /// </param>
        /// <returns>
        /// An <see cref="AlternateDataStreamInfo"/> representing the stream.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="file"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="FileNotFoundException">
        /// The specified <paramref name="file"/> was not found.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="name"/> contains invalid characters.
        /// </exception>
        /// <exception cref="NotSupportedException">
        /// <paramref name="mode"/> is either <see cref="FileMode.Truncate"/> or <see cref="FileMode.Append"/>.
        /// </exception>
        /// <exception cref="IOException">
        /// <para><paramref name="mode"/> is <see cref="FileMode.Open"/>, and the stream doesn't exist.</para>
        /// <para>-or-</para>
        /// <para><paramref name="mode"/> is <see cref="FileMode.CreateNew"/>, and the stream already exists.</para>
        /// </exception>
        /// <exception cref="SecurityException">
        /// The caller does not have the required permission. 
        /// </exception>
        /// <exception cref="UnauthorizedAccessException">
        /// The caller does not have the required permission, or the file is read-only.
        /// </exception>
        public static AlternateDataStreamInfo OpenAlternateDataStream(this FileSystemInfo file, String name, FileMode mode = FileMode.OpenOrCreate)
        {
            return OpenAlternateDataStream(file?.FullName, name, mode);
        }

        /// <summary>
        /// Opens an alternate data stream.
        /// </summary>
        /// <param name="path">
        /// The path of the file which contains the stream.
        /// </param>
        /// <param name="name">
        /// The name of the stream to open.
        /// </param>
        /// <param name="mode">
        /// One of the <see cref="FileMode"/> values, indicating how the stream is to be opened.
        /// </param>
        /// <returns>
        /// An <see cref="AlternateDataStreamInfo"/> representing the stream.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="path"/> is <see langword="null"/> or an empty string.
        /// </exception>
        /// <exception cref="FileNotFoundException">
        /// The specified <paramref name="path"/> was not found.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <para><paramref name="path"/> is not a valid file path.</para>
        /// <para>-or-</para>
        /// <para><paramref name="name"/> contains invalid characters.</para>
        /// </exception>
        /// <exception cref="NotSupportedException">
        /// <paramref name="mode"/> is either <see cref="FileMode.Truncate"/> or <see cref="FileMode.Append"/>.
        /// </exception>
        /// <exception cref="IOException">
        /// <para><paramref name="mode"/> is <see cref="FileMode.Open"/>, and the stream doesn't exist.</para>
        /// <para>-or-</para>
        /// <para><paramref name="mode"/> is <see cref="FileMode.CreateNew"/>, and the stream already exists.</para>
        /// </exception>
        /// <exception cref="SecurityException">
        /// The caller does not have the required permission. 
        /// </exception>
        /// <exception cref="UnauthorizedAccessException">
        /// The caller does not have the required permission, or the file is read-only.
        /// </exception>
        public static AlternateDataStreamInfo OpenAlternateDataStream(String path, String name, FileMode mode = FileMode.OpenOrCreate)
        {
            if (String.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            if (!Safe.FileExists(path))
            {
                throw new FileNotFoundException("File not found", path);
            }

            ValidateStreamName(name);

            if (mode == FileMode.Truncate || mode == FileMode.Append)
            {
                throw new NotSupportedException($"The specified mode '{mode}' is not supported.");
            }

            String stream = BuildStreamPath(path, name);
            Boolean exists = Safe.FileExists(stream);

            if (!exists && mode == FileMode.Open)
            {
                throw new IOException($"The specified alternate data stream '{name}' does not exist on file '{stream}'.");
            }

            if (exists && mode == FileMode.CreateNew)
            {
                throw new IOException($"The specified alternate data stream '{name}' already exists on file '{path}'.");
            }

            return new AlternateDataStreamInfo(path, name, stream, exists);
        }

        /// <summary>
        /// <span style="font-weight:bold;color:#a00;">(Extension Method)</span><br />
        /// Deletes the specified alternate data stream if it exists.
        /// </summary>
        /// <param name="file">
        /// The <see cref="FileInfo"/> to inspect.
        /// </param>
        /// <param name="name">
        /// The name of the stream to delete.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the specified stream is deleted;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="file"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="name"/> contains invalid characters.
        /// </exception>
        /// <exception cref="SecurityException">
        /// The caller does not have the required permission. 
        /// </exception>
        /// <exception cref="UnauthorizedAccessException">
        /// The caller does not have the required permission, or the file is read-only.
        /// </exception>
        /// <exception cref="IOException">
        /// The specified file is in use. 
        /// </exception>
        public static Boolean DeleteAlternateDataStream(this FileSystemInfo file, String name)
        {
            return DeleteAlternateDataStream(file?.FullName, name);
        }

        /// <summary>
        /// Deletes the specified alternate data stream if it exists.
        /// </summary>
        /// <param name="path">
        /// The path of the file to inspect.
        /// </param>
        /// <param name="name">
        /// The name of the stream to find.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the specified stream is deleted;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="path"/> is <see langword="null"/> or an empty string.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <para><paramref name="path"/> is not a valid file path.</para>
        /// <para>-or-</para>
        /// <para><paramref name="name"/> contains invalid characters.</para>
        /// </exception>
        /// <exception cref="SecurityException">
        /// The caller does not have the required permission. 
        /// </exception>
        /// <exception cref="UnauthorizedAccessException">
        /// The caller does not have the required permission, or the file is read-only.
        /// </exception>
        /// <exception cref="IOException">
        /// The specified file is in use. 
        /// </exception>
        public static Boolean DeleteAlternateDataStream(String path, String name)
        {
            if (String.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            ValidateStreamName(name);

            if (!Safe.FileExists(path))
            {
                return false;
            }

            String stream = BuildStreamPath(path, name);
            return Safe.FileExists(stream) && Safe.DeleteFile(stream);
        }

        public const Char StreamSeparator = ':';

        private const Int32 ErrorFileNotFound = 2;
        private const Int32 ErrorPathNotFound = 3;

        private static readonly Char[] InvalidStreamNameChars = Path.GetInvalidFileNameChars().Where(c => c < 1 || c > 31).ToArray();

        [StructLayout(LayoutKind.Sequential)]
        private readonly struct Win32StreamId
        {
            public readonly Int32 StreamId;
            public readonly Int32 StreamAttributes;
            public readonly LargeInteger Size;
            public readonly Int32 StreamNameSize;
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, BestFitMapping = false, ThrowOnUnmappableChar = true)]
        private static extern Int32 FormatMessage(
            Int32 dwFlags,
            IntPtr lpSource,
            Int32 dwMessageId,
            Int32 dwLanguageId,
            StringBuilder lpBuffer,
            Int32 nSize,
            IntPtr vaListArguments);


        [DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern Boolean GetFileSizeEx(SafeFileHandle handle, out LargeInteger size);

        [DllImport("kernel32", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern Boolean BackupRead(
            SafeFileHandle hFile,
            ref Win32StreamId pBuffer,
            Int32 numberOfBytesToRead,
            out Int32 numberOfBytesRead,
            [MarshalAs(UnmanagedType.Bool)] Boolean abort,
            [MarshalAs(UnmanagedType.Bool)] Boolean processSecurity,
            ref IntPtr context);

        [DllImport("kernel32", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern Boolean BackupRead(
            SafeFileHandle hFile,
            SafeGlobalHandle pBuffer,
            Int32 numberOfBytesToRead,
            out Int32 numberOfBytesRead,
            [MarshalAs(UnmanagedType.Bool)] Boolean abort,
            [MarshalAs(UnmanagedType.Bool)] Boolean processSecurity,
            ref IntPtr context);

        [DllImport("kernel32", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern Boolean BackupSeek(
            SafeFileHandle hFile,
            Int32 bytesToSeekLow,
            Int32 bytesToSeekHigh,
            out Int32 bytesSeekedLow,
            out Int32 bytesSeekedHigh,
            ref IntPtr context);


        private const Int32 NativeErrorCode = -2147024896;

        private static Int32 MakeHRFromErrorCode(Int32 code)
        {
            return NativeErrorCode | code;
        }

        private static String GetErrorMessage(Int32 code)
        {
            StringBuilder lpBuffer = new StringBuilder(0x200);
            return 0 != FormatMessage(0x3200, IntPtr.Zero, code, 0, lpBuffer, lpBuffer.Capacity, IntPtr.Zero) ? lpBuffer.ToString() : $"Unknown error: {code}";
        }

        private static void ThrowIOError(Int32 code, String path)
        {
            switch (code)
            {
                case 0:
                {
                    break;
                }
                case 2: // File not found
                {
                    if (String.IsNullOrEmpty(path))
                    {
                        throw new FileNotFoundException();
                    }

                    throw new FileNotFoundException(null, path);
                }
                case 3: // Directory not found
                {
                    if (String.IsNullOrEmpty(path))
                    {
                        throw new DirectoryNotFoundException();
                    }

                    throw new DirectoryNotFoundException($"Could not find a part of the path '{path}'.");
                }
                case 5: // Access denied
                {
                    if (String.IsNullOrEmpty(path))
                    {
                        throw new UnauthorizedAccessException();
                    }

                    throw new UnauthorizedAccessException($"Access to the path '{path}' was denied.");
                }
                case 15: // Drive not found
                {
                    if (String.IsNullOrEmpty(path))
                    {
                        throw new DriveNotFoundException();
                    }

                    throw new DriveNotFoundException($"Could not find the drive '{path}'. The drive might not be ready or might not be mapped.");
                }
                case 32: // Sharing violation
                {
                    if (String.IsNullOrEmpty(path))
                    {
                        throw new IOException(GetErrorMessage(code), MakeHRFromErrorCode(code));
                    }

                    throw new IOException($"The process cannot access the file '{path}' because it is being used by another process.", MakeHRFromErrorCode(code));
                }
                case 80: // File already exists
                {
                    if (!String.IsNullOrEmpty(path))
                    {
                        throw new IOException($"The file '{path}' already exists.", MakeHRFromErrorCode(code));
                    }

                    break;
                }
                case 87: // Invalid parameter
                {
                    throw new IOException(GetErrorMessage(code), MakeHRFromErrorCode(code));
                }
                case 183: // File or directory already exists
                {
                    if (!String.IsNullOrEmpty(path))
                    {
                        throw new IOException($"Cannot create '{path}' because a file or directory with the same name already exists.", MakeHRFromErrorCode(code));
                    }

                    break;
                }
                case 206: // Path too long
                {
                    throw new PathTooLongException();
                }
                case 995: // Operation cancelled
                {
                    throw new OperationCanceledException();
                }
                default:
                {
                    Marshal.ThrowExceptionForHR(MakeHRFromErrorCode(code));
                    break;
                }
            }
        }

        public static void ThrowLastIOError(String path)
        {
            Int32 code = Marshal.GetLastWin32Error();
            if (code == 0)
            {
                return;
            }

            Int32 hr = Marshal.GetHRForLastWin32Error();
            if (hr >= 0)
            {
                throw new Win32Exception(code);
            }

            ThrowIOError(code, path);
        }

        public static NativeFileAccess ToNative(this FileAccess access)
        {
            NativeFileAccess result = 0;
            if ((access & FileAccess.Read) == FileAccess.Read)
            {
                result |= NativeFileAccess.GenericRead;
            }

            if ((access & FileAccess.Write) == FileAccess.Write)
            {
                result |= NativeFileAccess.GenericWrite;
            }

            return result;
        }

        public static String BuildStreamPath(String path, String name)
        {
            if (String.IsNullOrEmpty(path))
            {
                return String.Empty;
            }

            // Trailing slashes on directory paths don't work:

            String result = path;
            Int32 length = result.Length;
            while (length > 0 && '\\' == result[length - 1])
            {
                length--;
            }

            if (length != result.Length)
            {
                result = length == 0 ? "." : result.Substring(0, length);
            }

            result += StreamSeparator + name + StreamSeparator + "$DATA";

            if (result.Length >= MaxPathLength && !result.StartsWith(LongPathPrefix))
            {
                result = LongPathPrefix + result;
            }

            return result;
        }

        public static void ValidateStreamName(String name)
        {
            if (!String.IsNullOrEmpty(name) && name.IndexOfAny(InvalidStreamNameChars) != -1)
            {
                throw new ArgumentException("The specified stream name contains invalid characters.");
            }
        }
        
        private static class Native
        {
            [DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
            public static extern SafeFileHandle CreateFile(
                String name,
                NativeFileAccess access,
                FileShare share,
                IntPtr security,
                FileMode mode,
                NativeFileFlags flags,
                IntPtr template);

            [DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
            public static extern Int32 GetFileAttributes(String path);

            [DllImport("kernel32.dll")]
            public static extern Int32 GetFileType(SafeFileHandle handle);

            [DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern Boolean DeleteFile(String name);
        }

        internal static class Safe
        {
            public static Int32 GetFileAttributes(String name)
            {
                if (String.IsNullOrEmpty(name))
                {
                    throw new ArgumentNullException(nameof(name));
                }

                Int32 result = Native.GetFileAttributes(name);
                if (result != -1)
                {
                    return result;
                }

                Int32 code = Marshal.GetLastWin32Error();
                switch (code)
                {
                    case ErrorFileNotFound:
                    case ErrorPathNotFound:
                    {
                        break;
                    }
                    default:
                    {
                        ThrowLastIOError(name);
                        break;
                    }
                }

                return result;
            }

            public static Boolean FileExists(String name)
            {
                return GetFileAttributes(name) != -1;
            }

            public static Boolean DeleteFile(String name)
            {
                if (String.IsNullOrEmpty(name))
                {
                    throw new ArgumentNullException(nameof(name));
                }

                if (Native.DeleteFile(name))
                {
                    return true;
                }

                Int32 code = Marshal.GetLastWin32Error();
                switch (code)
                {
                    case ErrorFileNotFound:
                    case ErrorPathNotFound:
                    {
                        break;
                    }
                    default:
                    {
                        ThrowLastIOError(name);
                        break;
                    }
                }

                return true;
            }

            public static SafeFileHandle CreateFile(String path, NativeFileAccess access, FileShare share, IntPtr security, FileMode mode, NativeFileFlags flags, IntPtr template)
            {
                SafeFileHandle result = Native.CreateFile(path, access, share, security, mode, flags, template);
                if (result.IsInvalid || Native.GetFileType(result) == 1)
                {
                    return result;
                }

                result.Dispose();
                throw new NotSupportedException($"The specified file name '{path}' is not a disk-based file.");
            }
        }

        private static Int64 GetFileSize(String path, SafeFileHandle handle)
        {
            Int64 result = 0L;
            if (handle is null || handle.IsInvalid)
            {
                return result;
            }

            if (GetFileSizeEx(handle, out LargeInteger value))
            {
                result = value.ToInt64();
            }
            else
            {
                ThrowLastIOError(path);
            }

            return result;
        }

        public static Int64 GetFileSize(String path)
        {
            Int64 result = 0L;
            if (String.IsNullOrEmpty(path))
            {
                return result;
            }

            using SafeFileHandle handle = Safe.CreateFile(path, NativeFileAccess.GenericRead, FileShare.Read, IntPtr.Zero, FileMode.Open, 0, IntPtr.Zero);
            result = GetFileSize(path, handle);

            return result;
        }

        private static IList<Win32StreamInfo> GetDataStreams(String path)
        {
            if (String.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            if (path.IndexOfAny(Path.GetInvalidPathChars()) != -1)
            {
                throw new ArgumentException(@"The specified stream name contains invalid characters.", nameof(path));
            }

            List<Win32StreamInfo> result = new List<Win32StreamInfo>();

            using SafeFileHandle hFile = Safe.CreateFile(path, NativeFileAccess.GenericRead, FileShare.Read, IntPtr.Zero, FileMode.Open, NativeFileFlags.BackupSemantics,
                IntPtr.Zero);
            using StreamName hName = new StreamName();

            if (hFile.IsInvalid)
            {
                return result;
            }

            Win32StreamId streamId = new Win32StreamId();
            Int32 dwStreamHeaderSize = Marshal.SizeOf(streamId);
            Boolean finished = false;
            IntPtr context = IntPtr.Zero;
            Int32 bytesRead;

            try
            {
                while (!finished)
                {
                    // Read the next stream header:
                    if (!BackupRead(hFile, ref streamId, dwStreamHeaderSize, out bytesRead, false, false, ref context))
                    {
                        finished = true;
                    }
                    else if (dwStreamHeaderSize != bytesRead)
                    {
                        finished = true;
                    }
                    else
                    {
                        // Read the stream name:
                        String name;
                        if (streamId.StreamNameSize <= 0)
                        {
                            name = null;
                        }
                        else
                        {
                            hName.EnsureCapacity(streamId.StreamNameSize);
                            if (!BackupRead(hFile, hName.MemoryBlock, streamId.StreamNameSize, out bytesRead, false, false, ref context))
                            {
                                name = null;
                                finished = true;
                            }
                            else
                            {
                                // Unicode chars are 2 bytes:
                                name = hName.ReadStreamName(bytesRead >> 1);
                            }
                        }

                        // Add the stream info to the result:
                        if (!String.IsNullOrEmpty(name))
                        {
                            result.Add(new Win32StreamInfo((FileStreamType) streamId.StreamId, (FileStreamAttributes) streamId.StreamAttributes, streamId.Size.ToInt64(), name));
                        }

                        // Skip the contents of the stream:
                        if (streamId.Size.Low == 0 && streamId.Size.High == 0)
                        {
                            continue;
                        }

                        if (!finished && !BackupSeek(hFile, streamId.Size.Low, streamId.Size.High, out _, out _, ref context))
                        {
                            finished = true;
                        }
                    }
                }
            }
            finally
            {
                // Abort the backup:
                BackupRead(hFile, hName.MemoryBlock, 0, out bytesRead, true, false, ref context);
            }

            return result;
        }
    }
}