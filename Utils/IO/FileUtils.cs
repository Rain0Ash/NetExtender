// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.Win32.SafeHandles;
using NetExtender.IO.Shortcut;
using NetExtender.Utils.Types;

namespace NetExtender.Utils.IO
{
    public static class FileUtils
    {
        public static Boolean HasAttribute([NotNull] String path, FileAttributes attributes)
        {
            return HasAttribute(path, attributes, out _);
        }

        public static Boolean HasAttribute([NotNull] String path, FileAttributes attributes, out FileAttributes current)
        {
            if (path is null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            current = File.GetAttributes(path);
            
            return HasAttribute(current, attributes);
        }
        
        public static Boolean HasAttribute(this FileAttributes current, FileAttributes attributes)
        {
            return (current & attributes) == attributes;
        }
        
        /// <summary>
        /// Add or remove the <see cref="FileAttributes"/> attributes to the file on the specified path.
        /// </summary>
        /// <param name="path">The path to the file.</param>
        /// <param name="attributes">File attribute</param>
        /// <param name="status">Install or remove attributes</param>
        public static Boolean SetAttribute(String path, FileAttributes attributes, Boolean status = true)
        {
            Boolean already = HasAttribute(path, attributes, out FileAttributes current);

            if (already && status || !status && (current & attributes) == 0)
            {
                return true;
            }

            FileAttributes attribute = status ? current | attributes : current & ~attributes;

            try
            {
                File.SetAttributes(path, attribute);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static Boolean CheckPermissions(String path, FileSystemRights access)
        {
            return CheckPermissions(new FileInfo(path), access);
        }

        public static Boolean TryCheckPermissions(String path, FileSystemRights access)
        {
            return TryCheckPermissions(new FileInfo(path), access);
        }
        
        public static Boolean CheckPermissions(this FileInfo info, FileSystemRights access)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            if (info.Exists)
            {
                return info
                    .GetAccessControl()
                    .GetAccessRules(true, true, typeof(NTAccount))
                    .OfType<FileSystemAccessRule>()
                    .Any(rule => (rule.FileSystemRights & access) == access);
            }

            return info.Directory.CheckPermissions(access);
        }

        public static Boolean TryCheckPermissions(this FileInfo info, FileSystemRights access)
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

        /// <summary>
        /// Determines a text file's encoding.
        /// </summary>
        /// <param name="path">The text file to analyze.</param>
        /// <param name="default">Default encoding</param>
        /// <returns>The detected encoding.</returns>
        public static Encoding GetFileEncoding(String path, Encoding @default = null)
        {
            using StreamReader reader = new StreamReader(path, @default ?? Encoding.UTF8, true);
            reader.Peek(); // need this!
            return reader.CurrentEncoding;
        }

        public static Byte[] ReadFileBytes(String path)
        {
            return File.ReadAllBytes(path);
        }

        public static Byte[] TryReadFileBytes(String path)
        {
            try
            {
                return ReadFileBytes(path);
            }
            catch (Exception)
            {
                return null;
            }
        }
        
        public static Boolean TryReadFileBytes(String path, out Byte[] result)
        {
            try
            {
                result = ReadFileBytes(path);
                return true;
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
        }

        public static String ReadFileText(String path)
        {
            return File.ReadAllText(path);
        }
        
        public static String ReadFileText(String path, Encoding encoding)
        {
            return encoding is null ? ReadFileText(path) : File.ReadAllText(path, encoding);
        }

        public static String TryReadFileText(String path)
        {
            try
            {
                return ReadFileText(path);
            }
            catch (Exception)
            {
                return null;
            }
        }
        
        public static String TryReadFileText(String path, Encoding encoding)
        {
            try
            {
                return ReadFileText(path, encoding);
            }
            catch (Exception)
            {
                return null;
            }
        }
        
        public static Boolean TryReadFileText(String path, out String result)
        {
            try
            {
                result = ReadFileText(path);
                return true;
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
        }
        
        public static Boolean TryReadFileText(String path, Encoding encoding, out String result)
        {
            try
            {
                result = ReadFileText(path, encoding);
                return true;
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
        }

        public static String[] ReadFileLines(String path)
        {
            return File.ReadAllLines(path);
        }
        
        public static String[] ReadFileLines(String path, Encoding encoding)
        {
            return encoding is null ? ReadFileLines(path) : File.ReadAllLines(path, encoding);
        }
        
        public static String[] TryReadFileLines(String path)
        {
            try
            {
                return ReadFileLines(path);
            }
            catch (Exception)
            {
                return null;
            }
        }
        
        public static String[] TryReadFileLines(String path, Encoding encoding)
        {
            try
            {
                return ReadFileLines(path, encoding);
            }
            catch (Exception)
            {
                return null;
            }
        }
        
        public static Boolean TryReadFileLines(String path, out String[] result)
        {
            try
            {
                result = ReadFileLines(path);
                return true;
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
        }
        
        public static Boolean TryReadFileLines(String path, Encoding encoding, out String[] result)
        {
            try
            {
                result = ReadFileLines(path, encoding);
                return true;
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
        }

        private static StreamReader GetFileReader(String path, Encoding encoding = null)
        {
            return encoding is null ? new StreamReader(path) : new StreamReader(path, encoding);
        }
        
        public static IEnumerable<String> ReadFileSequential(String path)
        {
            StreamReader file = GetFileReader(path);

            String line;
            while((line = file.ReadLine()) is not null)
            {
                yield return line;
            }
        }
        
        public static IEnumerable<String> ReadFileSequential(String path, Encoding encoding)
        {
            using StreamReader file = GetFileReader(path, encoding);

            String line;
            while((line = file.ReadLine()) is not null)
            {
                yield return line;
            }
        }
        
        public static IEnumerable<String> TryReadFileSequential(String path)
        {
            try
            {
                return ReadFileSequential(path);
            }
            catch (Exception)
            {
                return null;
            }
        }
        
        public static IEnumerable<String> TryReadFileSequential(String path, Encoding encoding)
        {
            try
            {
                return ReadFileSequential(path, encoding);
            }
            catch (Exception)
            {
                return null;
            }
        }
        
        public static Boolean TryReadFileSequential(String path, out IEnumerable<String> result)
        {
            try
            {
                result = ReadFileSequential(path);
                return true;
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
        }
        
        public static Boolean TryReadFileSequential(String path, Encoding encoding, out IEnumerable<String> result)
        {
            try
            {
                result = ReadFileSequential(path, encoding);
                return true;
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
        }

        public static IAsyncDisposable LockFile([NotNull] String path)
        {
            return new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None, 1);
        }
        
        public static Task LockFileAsync([NotNull] String path, CancellationToken token)
        {
            return LockFileAsync(path, Timeout.Infinite, token);
        }
        
        public static Task LockFileAsync([NotNull] String path, TimeSpan timeout)
        {
            return LockFileAsync(path, timeout, CancellationToken.None);
        }
        
        public static Task LockFileAsync([NotNull] String path, Int32 timeout)
        {
            return LockFileAsync(path, timeout, CancellationToken.None);
        }

        public static Task LockFileAsync([NotNull] String path, TimeSpan timeout, CancellationToken token)
        {
            return LockFileAsync(path, timeout.Milliseconds, token);
        }

        public static async Task LockFileAsync([NotNull] String path, Int32 timeout, CancellationToken token)
        {
            try
            {
                await using IAsyncDisposable disposable = LockFile(path);
                await Task.Delay(timeout, token).ConfigureAwait(false);
            }
            catch (TaskCanceledException)
            {
                //ignored
            }
        }

        /// <inheritdoc cref="WaitCreateFileStreamAsync(Func{FileStream},Int32,CancellationToken)"/>
        public static Task<FileStream> WaitCreateFileStreamAsync([NotNull] String path, FileMode mode)
        {
            return WaitCreateFileStreamAsync(path, mode, CancellationToken.None);
        }
        
        /// <inheritdoc cref="WaitCreateFileStreamAsync(Func{FileStream},Int32,CancellationToken)"/>
        public static Task<FileStream> WaitCreateFileStreamAsync([NotNull] String path, FileMode mode, CancellationToken token)
        {
            return WaitCreateFileStreamAsync(path, mode, Timeout.Infinite, token);
        }
        
        /// <inheritdoc cref="WaitCreateFileStreamAsync(Func{FileStream},Int32,CancellationToken)"/>
        public static Task<FileStream> WaitCreateFileStreamAsync([NotNull] String path, FileMode mode, TimeSpan timeout)
        {
            return WaitCreateFileStreamAsync(path, mode, timeout, CancellationToken.None);
        }
        
        /// <inheritdoc cref="WaitCreateFileStreamAsync(Func{FileStream},Int32,CancellationToken)"/>
        public static Task<FileStream> WaitCreateFileStreamAsync([NotNull] String path, FileMode mode, Int32 timeout)
        {
            return WaitCreateFileStreamAsync(path, mode, timeout, CancellationToken.None);
        }

        /// <inheritdoc cref="WaitCreateFileStreamAsync(Func{FileStream},Int32,CancellationToken)"/>
        public static Task<FileStream> WaitCreateFileStreamAsync([NotNull] String path, FileMode mode, TimeSpan timeout, CancellationToken token)
        {
            return WaitCreateFileStreamAsync(path, mode, timeout.Milliseconds, token);
        }

        /// <inheritdoc cref="WaitCreateFileStreamAsync(Func{FileStream},Int32,CancellationToken)"/>
        public static Task<FileStream> WaitCreateFileStreamAsync([NotNull] String path, FileMode mode, Int32 timeout, CancellationToken token)
        {
            return WaitCreateFileStreamAsync(() => new FileStream(path, mode), timeout, token);
        }
        
        /// <inheritdoc cref="WaitCreateFileStreamAsync(Func{FileStream},Int32,CancellationToken)"/>
        public static Task<FileStream> WaitCreateFileStreamAsync([NotNull] String path, FileMode mode, FileAccess access)
        {
            return WaitCreateFileStreamAsync(path, mode, access, CancellationToken.None);
        }
        
        /// <inheritdoc cref="WaitCreateFileStreamAsync(Func{FileStream},Int32,CancellationToken)"/>
        public static Task<FileStream> WaitCreateFileStreamAsync([NotNull] String path, FileMode mode, FileAccess access, CancellationToken token)
        {
            return WaitCreateFileStreamAsync(path, mode, access, Timeout.Infinite, token);
        }
        
        /// <inheritdoc cref="WaitCreateFileStreamAsync(Func{FileStream},Int32,CancellationToken)"/>
        public static Task<FileStream> WaitCreateFileStreamAsync([NotNull] String path, FileMode mode, FileAccess access, TimeSpan timeout)
        {
            return WaitCreateFileStreamAsync(path, mode, access, timeout, CancellationToken.None);
        }
        
        /// <inheritdoc cref="WaitCreateFileStreamAsync(Func{FileStream},Int32,CancellationToken)"/>
        public static Task<FileStream> WaitCreateFileStreamAsync([NotNull] String path, FileMode mode, FileAccess access, Int32 timeout)
        {
            return WaitCreateFileStreamAsync(path, mode, access, timeout, CancellationToken.None);
        }

        /// <inheritdoc cref="WaitCreateFileStreamAsync(Func{FileStream},Int32,CancellationToken)"/>
        public static Task<FileStream> WaitCreateFileStreamAsync([NotNull] String path, FileMode mode, FileAccess access, TimeSpan timeout, CancellationToken token)
        {
            return WaitCreateFileStreamAsync(path, mode, access, timeout.Milliseconds, token);
        }

        /// <inheritdoc cref="WaitCreateFileStreamAsync(Func{FileStream},Int32,CancellationToken)"/>
        public static Task<FileStream> WaitCreateFileStreamAsync([NotNull] String path, FileMode mode, FileAccess access, Int32 timeout, CancellationToken token)
        {
            return WaitCreateFileStreamAsync(() => new FileStream(path, mode, access), timeout, token);
        }
        
        /// <inheritdoc cref="WaitCreateFileStreamAsync(Func{FileStream},Int32,CancellationToken)"/>
        public static Task<FileStream> WaitCreateFileStreamAsync([NotNull] String path, FileMode mode, FileAccess access, FileShare share)
        {
            return WaitCreateFileStreamAsync(path, mode, access, share, CancellationToken.None);
        }
        
        /// <inheritdoc cref="WaitCreateFileStreamAsync(Func{FileStream},Int32,CancellationToken)"/>
        public static Task<FileStream> WaitCreateFileStreamAsync([NotNull] String path, FileMode mode, FileAccess access, FileShare share, CancellationToken token)
        {
            return WaitCreateFileStreamAsync(path, mode, access, share, Timeout.Infinite, token);
        }
        
        /// <inheritdoc cref="WaitCreateFileStreamAsync(Func{FileStream},Int32,CancellationToken)"/>
        public static Task<FileStream> WaitCreateFileStreamAsync([NotNull] String path, FileMode mode, FileAccess access, FileShare share, TimeSpan timeout)
        {
            return WaitCreateFileStreamAsync(path, mode, access, share, timeout, CancellationToken.None);
        }
        
        /// <inheritdoc cref="WaitCreateFileStreamAsync(Func{FileStream},Int32,CancellationToken)"/>
        public static Task<FileStream> WaitCreateFileStreamAsync([NotNull] String path, FileMode mode, FileAccess access, FileShare share, Int32 timeout)
        {
            return WaitCreateFileStreamAsync(path, mode, access, share, timeout, CancellationToken.None);
        }

        /// <inheritdoc cref="WaitCreateFileStreamAsync(Func{FileStream},Int32,CancellationToken)"/>
        public static Task<FileStream> WaitCreateFileStreamAsync([NotNull] String path, FileMode mode, FileAccess access, FileShare share, TimeSpan timeout, CancellationToken token)
        {
            return WaitCreateFileStreamAsync(path, mode, access, share, timeout.Milliseconds, token);
        }

        /// <inheritdoc cref="WaitCreateFileStreamAsync(Func{FileStream},Int32,CancellationToken)"/>
        public static Task<FileStream> WaitCreateFileStreamAsync([NotNull] String path, FileMode mode, FileAccess access, FileShare share, Int32 timeout, CancellationToken token)
        {
            return WaitCreateFileStreamAsync(() => new FileStream(path, mode, access, share), timeout, token);
        }

        /// <inheritdoc cref="WaitCreateFileStreamAsync(Func{FileStream},Int32,CancellationToken)"/>
        public static Task<FileStream> WaitCreateFileStreamAsync([NotNull] String path, FileMode mode, FileAccess access, FileShare share, Int32 bufferSize, TimeSpan timeout)
        {
            return WaitCreateFileStreamAsync(path, mode, access, share, bufferSize, timeout, CancellationToken.None);
        }
        
        /// <inheritdoc cref="WaitCreateFileStreamAsync(Func{FileStream},Int32,CancellationToken)"/>
        public static Task<FileStream> WaitCreateFileStreamAsync([NotNull] String path, FileMode mode, FileAccess access, FileShare share, Int32 bufferSize, Int32 timeout)
        {
            return WaitCreateFileStreamAsync(path, mode, access, share, bufferSize, timeout, CancellationToken.None);
        }

        /// <inheritdoc cref="WaitCreateFileStreamAsync(Func{FileStream},Int32,CancellationToken)"/>
        public static Task<FileStream> WaitCreateFileStreamAsync([NotNull] String path, FileMode mode, FileAccess access, FileShare share, Int32 bufferSize, TimeSpan timeout, CancellationToken token)
        {
            return WaitCreateFileStreamAsync(path, mode, access, share, bufferSize, timeout.Milliseconds, token);
        }

        /// <inheritdoc cref="WaitCreateFileStreamAsync(Func{FileStream},Int32,CancellationToken)"/>
        public static Task<FileStream> WaitCreateFileStreamAsync([NotNull] String path, FileMode mode, FileAccess access, FileShare share, Int32 bufferSize, Int32 timeout, CancellationToken token)
        {
            return WaitCreateFileStreamAsync(() => new FileStream(path, mode, access, share, bufferSize), timeout, token);
        }
        
        /// <inheritdoc cref="WaitCreateFileStreamAsync(Func{FileStream},Int32,CancellationToken)"/>
        public static Task<FileStream> WaitCreateFileStreamAsync([NotNull] String path, FileMode mode, FileAccess access, FileShare share, Int32 bufferSize, FileOptions options)
        {
            return WaitCreateFileStreamAsync(path, mode, access, share, bufferSize, options, CancellationToken.None);
        }
        
        /// <inheritdoc cref="WaitCreateFileStreamAsync(Func{FileStream},Int32,CancellationToken)"/>
        public static Task<FileStream> WaitCreateFileStreamAsync([NotNull] String path, FileMode mode, FileAccess access, FileShare share, Int32 bufferSize, FileOptions options, CancellationToken token)
        {
            return WaitCreateFileStreamAsync(path, mode, access, share, bufferSize, options, Timeout.Infinite, token);
        }
        
        /// <inheritdoc cref="WaitCreateFileStreamAsync(Func{FileStream},Int32,CancellationToken)"/>
        public static Task<FileStream> WaitCreateFileStreamAsync([NotNull] String path, FileMode mode, FileAccess access, FileShare share, Int32 bufferSize, FileOptions options, TimeSpan timeout)
        {
            return WaitCreateFileStreamAsync(path, mode, access, share, bufferSize, options, timeout, CancellationToken.None);
        }
        
        /// <inheritdoc cref="WaitCreateFileStreamAsync(Func{FileStream},Int32,CancellationToken)"/>
        public static Task<FileStream> WaitCreateFileStreamAsync([NotNull] String path, FileMode mode, FileAccess access, FileShare share, Int32 bufferSize, FileOptions options, Int32 timeout)
        {
            return WaitCreateFileStreamAsync(path, mode, access, share, bufferSize, options, timeout, CancellationToken.None);
        }

        /// <inheritdoc cref="WaitCreateFileStreamAsync(Func{FileStream},Int32,CancellationToken)"/>
        public static Task<FileStream> WaitCreateFileStreamAsync([NotNull] String path, FileMode mode, FileAccess access, FileShare share, Int32 bufferSize, FileOptions options, TimeSpan timeout, CancellationToken token)
        {
            return WaitCreateFileStreamAsync(path, mode, access, share, bufferSize, options, timeout.Milliseconds, token);
        }

        /// <inheritdoc cref="WaitCreateFileStreamAsync(Func{FileStream},Int32,CancellationToken)"/>
        public static Task<FileStream> WaitCreateFileStreamAsync([NotNull] String path, FileMode mode, FileAccess access, FileShare share, Int32 bufferSize, FileOptions options, Int32 timeout, CancellationToken token)
        {
            return WaitCreateFileStreamAsync(() => new FileStream(path, mode, access, share, bufferSize, options), timeout, token);
        }

        /// <inheritdoc cref="WaitCreateFileStreamAsync(Func{FileStream},Int32,CancellationToken)"/>
        public static Task<FileStream> WaitCreateFileStreamAsync([NotNull] this SafeFileHandle handle, FileAccess access)
        {
            return WaitCreateFileStreamAsync(handle, access, CancellationToken.None);
        }
        
        /// <inheritdoc cref="WaitCreateFileStreamAsync(Func{FileStream},Int32,CancellationToken)"/>
        public static Task<FileStream> WaitCreateFileStreamAsync([NotNull] this SafeFileHandle handle, FileAccess access, CancellationToken token)
        {
            return WaitCreateFileStreamAsync(handle, access, Timeout.Infinite, token);
        }
        
        /// <inheritdoc cref="WaitCreateFileStreamAsync(Func{FileStream},Int32,CancellationToken)"/>
        public static Task<FileStream> WaitCreateFileStreamAsync([NotNull] this SafeFileHandle handle, FileAccess access, TimeSpan timeout)
        {
            return WaitCreateFileStreamAsync(handle, access, timeout, CancellationToken.None);
        }
        
        /// <inheritdoc cref="WaitCreateFileStreamAsync(Func{FileStream},Int32,CancellationToken)"/>
        public static Task<FileStream> WaitCreateFileStreamAsync([NotNull] this SafeFileHandle handle, FileAccess access, Int32 timeout)
        {
            return WaitCreateFileStreamAsync(handle, access, timeout, CancellationToken.None);
        }

        /// <inheritdoc cref="WaitCreateFileStreamAsync(Func{FileStream},Int32,CancellationToken)"/>
        public static Task<FileStream> WaitCreateFileStreamAsync([NotNull] this SafeFileHandle handle, FileAccess access, TimeSpan timeout, CancellationToken token)
        {
            return WaitCreateFileStreamAsync(handle, access, timeout.Milliseconds, token);
        }

        /// <inheritdoc cref="WaitCreateFileStreamAsync(Func{FileStream},Int32,CancellationToken)"/>
        public static Task<FileStream> WaitCreateFileStreamAsync([NotNull] this SafeFileHandle handle, FileAccess access, Int32 timeout, CancellationToken token)
        {
            return WaitCreateFileStreamAsync(() => new FileStream(handle, access), timeout, token);
        }

        /// <inheritdoc cref="WaitCreateFileStreamAsync(Func{FileStream},Int32,CancellationToken)"/>
        public static Task<FileStream> WaitCreateFileStreamAsync([NotNull] this SafeFileHandle handle, FileAccess access, Int32 bufferSize, TimeSpan timeout)
        {
            return WaitCreateFileStreamAsync(handle, access, bufferSize, timeout, CancellationToken.None);
        }
        
        /// <inheritdoc cref="WaitCreateFileStreamAsync(Func{FileStream},Int32,CancellationToken)"/>
        public static Task<FileStream> WaitCreateFileStreamAsync([NotNull] this SafeFileHandle handle, FileAccess access, Int32 bufferSize, Int32 timeout)
        {
            return WaitCreateFileStreamAsync(handle, access, bufferSize, timeout, CancellationToken.None);
        }

        /// <inheritdoc cref="WaitCreateFileStreamAsync(Func{FileStream},Int32,CancellationToken)"/>
        public static Task<FileStream> WaitCreateFileStreamAsync([NotNull] this SafeFileHandle handle, FileAccess access, Int32 bufferSize, TimeSpan timeout, CancellationToken token)
        {
            return WaitCreateFileStreamAsync(handle, access, bufferSize, timeout.Milliseconds, token);
        }

        /// <inheritdoc cref="WaitCreateFileStreamAsync(Func{FileStream},Int32,CancellationToken)"/>
        public static Task<FileStream> WaitCreateFileStreamAsync([NotNull] this SafeFileHandle handle, FileAccess access, Int32 bufferSize, Int32 timeout, CancellationToken token)
        {
            return WaitCreateFileStreamAsync(() => new FileStream(handle, access, bufferSize), timeout, token);
        }
        
        /// <inheritdoc cref="WaitCreateFileStreamAsync(Func{FileStream},Int32,CancellationToken)"/>
        public static Task<FileStream> WaitCreateFileStreamAsync([NotNull] this SafeFileHandle handle, FileAccess access, Int32 bufferSize, Boolean isAsync)
        {
            return WaitCreateFileStreamAsync(handle, access, bufferSize, isAsync, CancellationToken.None);
        }
        
        /// <inheritdoc cref="WaitCreateFileStreamAsync(Func{FileStream},Int32,CancellationToken)"/>
        public static Task<FileStream> WaitCreateFileStreamAsync([NotNull] this SafeFileHandle handle, FileAccess access, Int32 bufferSize, Boolean isAsync, CancellationToken token)
        {
            return WaitCreateFileStreamAsync(handle, access, bufferSize, isAsync, Timeout.Infinite, token);
        }
        
        /// <inheritdoc cref="WaitCreateFileStreamAsync(Func{FileStream},Int32,CancellationToken)"/>
        public static Task<FileStream> WaitCreateFileStreamAsync([NotNull] this SafeFileHandle handle, FileAccess access, Int32 bufferSize, Boolean isAsync, TimeSpan timeout)
        {
            return WaitCreateFileStreamAsync(handle, access, bufferSize, isAsync, timeout, CancellationToken.None);
        }
        
        /// <inheritdoc cref="WaitCreateFileStreamAsync(Func{FileStream},Int32,CancellationToken)"/>
        public static Task<FileStream> WaitCreateFileStreamAsync([NotNull] this SafeFileHandle handle, FileAccess access, Int32 bufferSize, Boolean isAsync, Int32 timeout)
        {
            return WaitCreateFileStreamAsync(handle, access, bufferSize, isAsync, timeout, CancellationToken.None);
        }

        /// <inheritdoc cref="WaitCreateFileStreamAsync(Func{FileStream},Int32,CancellationToken)"/>
        public static Task<FileStream> WaitCreateFileStreamAsync([NotNull] this SafeFileHandle handle, FileAccess access, Int32 bufferSize, Boolean isAsync, TimeSpan timeout, CancellationToken token)
        {
            return WaitCreateFileStreamAsync(handle, access, bufferSize, isAsync, timeout.Milliseconds, token);
        }

        /// <inheritdoc cref="WaitCreateFileStreamAsync(Func{FileStream},Int32,CancellationToken)"/>
        public static Task<FileStream> WaitCreateFileStreamAsync([NotNull] this SafeFileHandle handle, FileAccess access, Int32 bufferSize, Boolean isAsync, Int32 timeout, CancellationToken token)
        {
            return WaitCreateFileStreamAsync(() => new FileStream(handle, access, bufferSize, isAsync), timeout, token);
        }

        /// <summary>
        /// Tries to create filestream with timeout if file is locked (<see cref="IOException"/>)
        /// </summary>
        /// <param name="factory">FileStream factory</param>
        /// <param name="timeout">Timeout</param>
        /// <param name="token"><see cref="CancellationToken"/></param>
        /// <returns><see cref="FileStream"/></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="TimeoutException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        public static async Task<FileStream> WaitCreateFileStreamAsync([NotNull] Func<FileStream> factory, Int32 timeout, CancellationToken token)
        {
            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            if (timeout != Timeout.Infinite && timeout <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(timeout));
            }

            Int64 timestamp = Environment.TickCount64;

            do
            {
                token.ThrowIfCancellationRequested();
                
                try
                {
                    return factory.Invoke();
                }
                catch (IOException)
                {
                    await Task.Delay(50, token).ConfigureAwait(false);
                }
                
            } while (timeout == -1 || Environment.TickCount64 - timestamp < timeout);
            
            throw new TimeoutException("File opening timed out");
        }

        public static FileStream Reserve([NotNull] this FileStream stream, Int32 count, InformationSize type)
        {
            return Reserve(stream, (Int64) type.ConvertToBytes(count));
        }

        public static FileStream Reserve([NotNull] this FileStream stream, Int64 bytes)
        {
            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            if (bytes <= stream.Length)
            {
                return stream;
            }
            
            stream.SetLength(bytes);
            return stream;
        }

        public static Task<FileInfo> SafeCreateFileAsync([NotNull] String path, Byte[] data, Boolean overwrite = false, Int32 buffer = BufferUtils.DefaultBuffer)
        {
            return SafeCreateFileAsync(path, data, overwrite, buffer, CancellationToken.None);
        }

        public static Task<FileInfo> SafeCreateFileAsync([NotNull] String path, Byte[] data, Boolean overwrite, Int32 buffer, CancellationToken token)
        {
            using Stream stream = data.ToStream();
            return SafeCreateFileAsync(path, stream, overwrite, buffer, token);
        }

        public static Task<FileInfo> SafeCreateFileAsync([NotNull] String path, Stream stream, Boolean overwrite = false, Int32 buffer = BufferUtils.DefaultBuffer)
        {
            return SafeCreateFileAsync(path, stream, overwrite, buffer, CancellationToken.None);
        }

        public static async Task<FileInfo> SafeCreateFileAsync([NotNull] String path, Stream stream, Boolean overwrite, Int32 buffer, CancellationToken token)
        {
            if (path is null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            if (!stream.CanRead)
            {
                throw new NotSupportedException();
            }

            if (!PathUtils.IsValidFilePath(path))
            {
                throw new ArgumentException(null, nameof(path));
            }

            Boolean exist = PathUtils.IsExistAsFile(path);
            if (!overwrite && exist)
            {
                throw new IOException($"File {path} already exist");
            }

            String partname = path + ".part";

            const FileOptions options = FileOptions.Asynchronous | FileOptions.SequentialScan;

            await using FileStream original = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None, 4096, options);

            await using FileStream part = new FileStream(partname, FileMode.Create, FileAccess.Write, FileShare.None, buffer, options);

            await stream.CopyToAsync(part, buffer, token).ConfigureAwait(false);
            await part.FlushAsync(token).ConfigureAwait(false);

            part.Close();
            original.Close();

            if (token.IsCancellationRequested)
            {
                try
                {
                    File.Delete(partname);
                }
                catch (Exception)
                {
                    // ignored
                }

                if (exist)
                {
                    return null;
                }

                try
                {
                    File.Delete(path);
                }
                catch (Exception)
                {
                    // ignored
                }

                return null;
            }

            File.Move(partname, path, true);
            return new FileInfo(path);
        }

        public static Boolean CreateShortcut([NotNull] String path, [NotNull] String directory)
        {
            if (path is null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            if (directory is null)
            {
                throw new ArgumentNullException(nameof(directory));
            }

            if (!PathUtils.IsExistAsFile(path))
            {
                return false;
            }

            Shortcut shortcut = new Shortcut(Path.GetFileNameWithoutExtension(path) + ".lnk")
            {
                TargetPath = path,
                WorkingDirectory = Path.GetDirectoryName(path),
                SaveDirectory = directory,
                IconLocation = path
            };

            try
            {
                shortcut.Save();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
        
        /// <summary>
        /// Renames the specified file (adds the specified suffix to the end of the name).
        /// </summary>
        /// <param name="info">The file to add the suffix to.</param>
        /// <param name="suffix">The suffix to add to the file.</param>
        public static void AddSuffix([NotNull] FileInfo info, [NotNull] String suffix)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            if (suffix is null)
            {
                throw new ArgumentNullException(nameof(suffix));
            }

            if (suffix == String.Empty)
            {
                return;
            }

            String name = info.Name + suffix;

            String dir = Path.GetDirectoryName(info.FullName);

            if (String.IsNullOrEmpty(dir))
            {
                throw new IOException("Directory name is empty or null");
            }

            String path = Path.Combine(dir, name);
            info.MoveTo(path);
        }
        
        /// <summary>
        /// Renames the specified file (removes the specified suffix from the end of the name, if present).
        /// </summary>
        /// <param name="info">The file to remove the suffix from.</param>
        /// <param name="suffix">The suffix to remove from the file.</param>
        public static void RemoveSuffix([NotNull] FileInfo info, [NotNull] String suffix)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            if (suffix is null)
            {
                throw new ArgumentNullException(nameof(suffix));
            }

            if (suffix == String.Empty)
            {
                return;
            }

            if (!info.Name.EndsWith(suffix))
            {
                return;
            }

            String name = info.Name.Substring(0, info.Name.Length - suffix.Length);

            String dir = Path.GetDirectoryName(info.FullName);

            if (String.IsNullOrEmpty(dir))
            {
                throw new IOException("Directory name is empty or null");
            }
                
            String path = Path.Combine(dir, name);
            info.MoveTo(path);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static FileStream ToReadStream([NotNull] this FileInfo info)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            return info.OpenRead();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static FileStream ToWriteStream([NotNull] this FileInfo info)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            return info.OpenWrite();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static FileStream ToStream([NotNull] this FileInfo info)
        {
            return ToStream(info, FileShare.None);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static FileStream ToStream([NotNull] this FileInfo info, FileShare share)
        {
            return ToStream(info, FileMode.OpenOrCreate, FileAccess.ReadWrite, share);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static FileStream ToStream([NotNull] this FileInfo info, FileMode mode)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            return info.Open(mode);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static FileStream ToStream([NotNull] this FileInfo info, FileMode mode, FileAccess access)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            return info.Open(mode, access);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static FileStream ToStream([NotNull] this FileInfo info, FileMode mode, FileAccess access, FileShare share)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            return info.Open(mode, access, share);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsReadOnly(this FileAccess access)
        {
            return !access.HasFlag(FileAccess.Write);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static FileAccess GetFileAccess(Boolean write)
        {
            return write ? FileAccess.ReadWrite : FileAccess.Read;
        }
    }
    
    public readonly struct FileData
    {
        public FileAttributes Attributes { get; init; }
        public DateTime CreationTime { get; init; }
        public DateTime LastAccessTime { get; init; }
        public DateTime LastWriteTime { get; init; }
        public UInt64 FileSize { get; init; }
    }
}