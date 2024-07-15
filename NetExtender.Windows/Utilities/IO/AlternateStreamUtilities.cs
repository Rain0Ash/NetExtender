// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading.Tasks;
using Microsoft.Win32.SafeHandles;
using NetExtender.IO.FileSystem.NTFS.DataStreams;
using NetExtender.Windows;
using NetExtender.Windows.IO;

namespace NetExtender.Utilities.Windows.IO
{
    public static class AlternateStreamUtilities
    {
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
            if (file is null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            return GetAlternateDataStreams(file.FullName);
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

            if (!WindowsPathUtilities.Safe.FileExists(path))
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
        public static Boolean IsAlternateDataStreamExists(this FileSystemInfo file, String name)
        {
            if (file is null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            return IsAlternateDataStreamExists(file.FullName, name);
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
        public static Boolean IsAlternateDataStreamExists(String path, String name)
        {
            if (String.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            if (!IsValidStreamName(name))
            {
                throw new ArgumentException("The specified stream name contains invalid characters.");
            }

            return WindowsPathUtilities.Safe.FileExists(BuildStreamPath(path, name));
        }

        public static AlternateDataStreamInfo OpenAlternateDataStream(this FileSystemInfo file, String name)
        {
            if (file is null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            return OpenAlternateDataStream(file.FullName, name, FileMode.OpenOrCreate);
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
        public static AlternateDataStreamInfo OpenAlternateDataStream(this FileSystemInfo file, String name, FileMode mode)
        {
            if (file is null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            return OpenAlternateDataStream(file.FullName, name, mode);
        }

        public static AlternateDataStreamInfo OpenAlternateDataStream(String path, String name)
        {
            return OpenAlternateDataStream(path, name, FileMode.OpenOrCreate);
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
        public static AlternateDataStreamInfo OpenAlternateDataStream(String path, String name, FileMode mode)
        {
            if (String.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (!WindowsPathUtilities.Safe.FileExists(path))
            {
                throw new FileNotFoundException(null, path);
            }

            if (!IsValidStreamName(name))
            {
                throw new ArgumentException("The specified stream name contains invalid characters.");
            }

            if (mode == FileMode.Truncate || mode == FileMode.Append)
            {
                throw new NotSupportedException($"The specified mode '{mode}' is not supported.");
            }

            String stream = BuildStreamPath(path, name);
            Boolean exists = WindowsPathUtilities.Safe.FileExists(stream);

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

        public static String ReadAlternateDataStream(String path, String name)
        {
            if (String.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            AlternateDataStreamInfo info = OpenAlternateDataStream(path, name);

            using StreamReader reader = info.OpenText();
            return reader.ReadToEnd();
        }

        public static String ReadAlternateDataStream(this FileSystemInfo path, String name)
        {
            if (path is null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            AlternateDataStreamInfo info = OpenAlternateDataStream(path, name);

            using StreamReader reader = info.OpenText();
            return reader.ReadToEnd();
        }

        public static async Task<String> ReadAlternateDataStreamAsync(String path, String name)
        {
            AlternateDataStreamInfo info = OpenAlternateDataStream(path, name);

            using StreamReader reader = info.OpenText();
            return await reader.ReadToEndAsync().ConfigureAwait(false);
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
            if (file is null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            return DeleteAlternateDataStream(file.FullName, name);
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

            if (!IsValidStreamName(name))
            {
                throw new ArgumentException("The specified stream name contains invalid characters.");
            }

            if (!WindowsPathUtilities.Safe.FileExists(path))
            {
                return false;
            }

            String stream = BuildStreamPath(path, name);
            return WindowsPathUtilities.Safe.FileExists(stream) && WindowsPathUtilities.Safe.DeleteFile(stream);
        }

        public const Char StreamSeparator = ':';

        private static readonly Char[] InvalidStreamNameChars = Path.GetInvalidFileNameChars().Where(c => c < 1 || c > 31).ToArray();

        [StructLayout(LayoutKind.Sequential)]
        private readonly struct LargeInteger
        {
            public static implicit operator Int64(LargeInteger value)
            {
                return value.ToInt64();
            }

            public Int32 High { get; }
            public Int32 Low { get; }

            public LargeInteger(Int32 high, Int32 low)
            {
                High = high;
                Low = low;
            }

            public Int64 ToInt64()
            {
                return High * 0x100000000 + Low;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private readonly struct Win32StreamId
        {
            public readonly Int32 StreamId;
            public readonly Int32 StreamAttributes;
            public readonly LargeInteger Size;
            public readonly Int32 StreamNameSize;
        }

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

            if (result.Length >= WindowsPathUtilities.MaxPathLength && !result.StartsWith(WindowsPathUtilities.LongPathPrefix))
            {
                result = WindowsPathUtilities.LongPathPrefix + result;
            }

            return result;
        }

        public static Boolean IsValidStreamName(String name)
        {
            return String.IsNullOrEmpty(name) || name.IndexOfAny(InvalidStreamNameChars) == -1;
        }

        //TODO: refactoring
        [SuppressMessage("ReSharper", "CognitiveComplexity")]
        private static IEnumerable<Win32StreamInfo> GetDataStreams(String path)
        {
            if (String.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException(nameof(path));
            }

            if (path.IndexOfAny(Path.GetInvalidPathChars()) != -1)
            {
                throw new ArgumentException(@"The specified stream name contains invalid characters.", nameof(path));
            }

            using SafeFileHandle file = WindowsPathUtilities.Safe.CreateFile(path, NativeFileAccess.GenericRead, FileShare.Read, IntPtr.Zero, FileMode.Open, NativeFileFlags.BackupSemantics, IntPtr.Zero);
            using StreamName streamname = new StreamName();

            if (file.IsInvalid)
            {
                yield break;
            }

            Win32StreamId id = new Win32StreamId();
            Int32 headersize = Marshal.SizeOf(id);
            Boolean finished = false;
            IntPtr context = IntPtr.Zero;
            Int32 read;

            try
            {
                while (!finished)
                {
                    // Read the next stream header:
                    if (!BackupRead(file, ref id, headersize, out read, false, false, ref context))
                    {
                        break;
                    }

                    if (headersize != read)
                    {
                        break;
                    }

                    String? name;
                    if (id.StreamNameSize <= 0)
                    {
                        name = null;
                    }
                    else
                    {
                        streamname.EnsureCapacity(id.StreamNameSize);
                        if (!BackupRead(file, streamname.MemoryBlock, id.StreamNameSize, out read, false, false, ref context))
                        {
                            name = null;
                            finished = true;
                        }
                        else
                        {
                            name = streamname.ReadStreamName(read >> 1);
                        }
                    }

                    if (!String.IsNullOrEmpty(name))
                    {
                        yield return new Win32StreamInfo((FileStreamType) id.StreamId, (FileStreamAttributes) id.StreamAttributes, id.Size.ToInt64(), name);
                    }

                    if (id.Size is { Low: 0, High: 0 })
                    {
                        continue;
                    }

                    if (!finished && !BackupSeek(file, id.Size.Low, id.Size.High, out _, out _, ref context))
                    {
                        finished = true;
                    }
                }
            }
            finally
            {
                BackupRead(file, streamname.MemoryBlock, 0, out read, true, false, ref context);
            }
        }
    }
}