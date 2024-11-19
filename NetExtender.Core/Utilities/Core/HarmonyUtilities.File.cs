using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.Versioning;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.Types.Interception.Interfaces;

namespace NetExtender.Utilities.Core
{
    public static partial class HarmonyUtilities
    {
        public static class File
        {
            public static IInterceptFileHandler Interceptor { get; }

            static File()
            {
                Interceptor = new InterceptHarmonyFileSystem(typeof(File));
            }

            /// <inheritdoc cref="IInterceptFileHandler.CreateSymbolicLink(System.String, System.String)" />
            public static FileSystemInfo CreateSymbolicLink(String path, String pathToTarget)
            {
                return Interceptor.CreateSymbolicLink(path, pathToTarget);
            }

            /// <inheritdoc cref="IInterceptFileHandler.ResolveLinkTarget(System.String, System.Boolean)" />
            public static FileSystemInfo? ResolveLinkTarget(String linkPath, Boolean returnFinalTarget)
            {
                return Interceptor.ResolveLinkTarget(linkPath, returnFinalTarget);
            }

            /// <inheritdoc cref="IInterceptFileHandler.Exists(System.String)" />
            public static Boolean Exists([NotNullWhen(true)] String? path)
            {
                return Interceptor.Exists(path);
            }

            /// <inheritdoc cref="IInterceptFileHandler.GetAttributes(System.String)" />
            public static FileAttributes GetAttributes(String path)
            {
                return Interceptor.GetAttributes(path);
            }

            /// <inheritdoc cref="IInterceptFileHandler.SetAttributes(System.String, System.IO.FileAttributes)" />
            public static void SetAttributes(String path, FileAttributes fileAttributes)
            {
                Interceptor.SetAttributes(path, fileAttributes);
            }

            /// <inheritdoc cref="IInterceptFileHandler.GetCreationTime(System.String)" />
            public static DateTime GetCreationTime(String path)
            {
                return Interceptor.GetCreationTime(path);
            }

            /// <inheritdoc cref="IInterceptFileHandler.GetCreationTimeUtc(System.String)" />
            public static DateTime GetCreationTimeUtc(String path)
            {
                return Interceptor.GetCreationTimeUtc(path);
            }

            /// <inheritdoc cref="IInterceptFileHandler.GetLastAccessTime(System.String)" />
            public static DateTime GetLastAccessTime(String path)
            {
                return Interceptor.GetLastAccessTime(path);
            }

            /// <inheritdoc cref="IInterceptFileHandler.GetLastAccessTimeUtc(System.String)" />
            public static DateTime GetLastAccessTimeUtc(String path)
            {
                return Interceptor.GetLastAccessTimeUtc(path);
            }

            /// <inheritdoc cref="IInterceptFileHandler.GetLastWriteTime(System.String)" />
            public static DateTime GetLastWriteTime(String path)
            {
                return Interceptor.GetLastWriteTime(path);
            }

            /// <inheritdoc cref="IInterceptFileHandler.GetLastWriteTimeUtc(System.String)" />
            public static DateTime GetLastWriteTimeUtc(String path)
            {
                return Interceptor.GetLastWriteTimeUtc(path);
            }

            /// <inheritdoc cref="IInterceptFileHandler.SetCreationTime(System.String, System.DateTime)" />
            public static void SetCreationTime(String path, DateTime creationTime)
            {
                Interceptor.SetCreationTime(path, creationTime);
            }

            /// <inheritdoc cref="IInterceptFileHandler.SetCreationTimeUtc(System.String, System.DateTime)" />
            public static void SetCreationTimeUtc(String path, DateTime creationTimeUtc)
            {
                Interceptor.SetCreationTimeUtc(path, creationTimeUtc);
            }

            /// <inheritdoc cref="IInterceptFileHandler.SetLastAccessTime(System.String, System.DateTime)" />
            public static void SetLastAccessTime(String path, DateTime lastAccessTime)
            {
                Interceptor.SetLastAccessTime(path, lastAccessTime);
            }

            /// <inheritdoc cref="IInterceptFileHandler.SetLastAccessTimeUtc(System.String, System.DateTime)" />
            public static void SetLastAccessTimeUtc(String path, DateTime lastAccessTimeUtc)
            {
                Interceptor.SetLastAccessTimeUtc(path, lastAccessTimeUtc);
            }

            /// <inheritdoc cref="IInterceptFileHandler.SetLastWriteTime(System.String, System.DateTime)" />
            public static void SetLastWriteTime(String path, DateTime lastWriteTime)
            {
                Interceptor.SetLastWriteTime(path, lastWriteTime);
            }

            /// <inheritdoc cref="IInterceptFileHandler.SetLastWriteTimeUtc(System.String, System.DateTime)" />
            public static void SetLastWriteTimeUtc(String path, DateTime lastWriteTimeUtc)
            {
                Interceptor.SetLastWriteTimeUtc(path, lastWriteTimeUtc);
            }

            /// <inheritdoc cref="IInterceptFileHandler.Open(System.String, System.IO.FileMode)" />
            public static System.IO.FileStream Open(String path, FileMode mode)
            {
                return Interceptor.Open(path, mode);
            }

            /// <inheritdoc cref="IInterceptFileHandler.Open(System.String, System.IO.FileMode, System.IO.FileAccess)" />
            public static System.IO.FileStream Open(String path, FileMode mode, FileAccess access)
            {
                return Interceptor.Open(path, mode, access);
            }

            /// <inheritdoc cref="IInterceptFileHandler.Open(System.String, System.IO.FileMode, System.IO.FileAccess, System.IO.FileShare)" />
            public static System.IO.FileStream Open(String path, FileMode mode, FileAccess access, FileShare share)
            {
                return Interceptor.Open(path, mode, access, share);
            }

            /// <inheritdoc cref="IInterceptFileHandler.OpenRead(System.String)" />
            public static System.IO.FileStream OpenRead(String path)
            {
                return Interceptor.OpenRead(path);
            }

            /// <inheritdoc cref="IInterceptFileHandler.OpenWrite(System.String)" />
            public static System.IO.FileStream OpenWrite(String path)
            {
                return Interceptor.OpenWrite(path);
            }

            /// <inheritdoc cref="IInterceptFileHandler.OpenText(System.String)" />
            public static System.IO.StreamReader OpenText(String path)
            {
                return Interceptor.OpenText(path);
            }

            /// <inheritdoc cref="IInterceptFileHandler.Create(System.String)" />
            public static System.IO.FileStream Create(String path)
            {
                return Interceptor.Create(path);
            }

            /// <inheritdoc cref="IInterceptFileHandler.Create(System.String, System.Int32)" />
            public static System.IO.FileStream Create(String path, Int32 bufferSize)
            {
                return Interceptor.Create(path, bufferSize);
            }

            /// <inheritdoc cref="IInterceptFileHandler.Create(System.String, System.Int32, System.IO.FileOptions)" />
            public static System.IO.FileStream Create(String path, Int32 bufferSize, FileOptions options)
            {
                return Interceptor.Create(path, bufferSize, options);
            }

            /// <inheritdoc cref="IInterceptFileHandler.CreateText(System.String)" />
            public static System.IO.StreamWriter CreateText(String path)
            {
                return Interceptor.CreateText(path);
            }

            /// <inheritdoc cref="IInterceptFileHandler.AppendText(System.String)" />
            public static System.IO.StreamWriter AppendText(String path)
            {
                return Interceptor.AppendText(path);
            }

            /// <inheritdoc cref="IInterceptFileHandler.ReadAllBytes(System.String)" />
            public static Byte[] ReadAllBytes(String path)
            {
                return Interceptor.ReadAllBytes(path);
            }

            /// <inheritdoc cref="IInterceptFileHandler.ReadAllBytesAsync(System.String, System.Threading.CancellationToken)" />
            public static Task<Byte[]> ReadAllBytesAsync(String path)
            {
                return Interceptor.ReadAllBytesAsync(path);
            }

            /// <inheritdoc cref="IInterceptFileHandler.ReadAllBytesAsync(System.String, System.Threading.CancellationToken)" />
            public static Task<Byte[]> ReadAllBytesAsync(String path, CancellationToken token)
            {
                return Interceptor.ReadAllBytesAsync(path, token);
            }

            /// <inheritdoc cref="IInterceptFileHandler.ReadAllText(System.String)" />
            public static String ReadAllText(String path)
            {
                return Interceptor.ReadAllText(path);
            }

            /// <inheritdoc cref="IInterceptFileHandler.ReadAllText(System.String, System.Text.Encoding)" />
            public static String ReadAllText(String path, Encoding encoding)
            {
                return Interceptor.ReadAllText(path, encoding);
            }

            /// <inheritdoc cref="IInterceptFileHandler.ReadAllTextAsync(System.String, System.Threading.CancellationToken)" />
            public static Task<String> ReadAllTextAsync(String path)
            {
                return Interceptor.ReadAllTextAsync(path);
            }

            /// <inheritdoc cref="IInterceptFileHandler.ReadAllTextAsync(System.String, System.Threading.CancellationToken)" />
            public static Task<String> ReadAllTextAsync(String path, CancellationToken token)
            {
                return Interceptor.ReadAllTextAsync(path, token);
            }

            /// <inheritdoc cref="IInterceptFileHandler.ReadAllTextAsync(System.String, System.Text.Encoding, System.Threading.CancellationToken)" />
            public static Task<String> ReadAllTextAsync(String path, Encoding encoding)
            {
                return Interceptor.ReadAllTextAsync(path, encoding);
            }

            /// <inheritdoc cref="IInterceptFileHandler.ReadAllTextAsync(System.String, System.Text.Encoding, System.Threading.CancellationToken)" />
            public static Task<String> ReadAllTextAsync(String path, Encoding encoding, CancellationToken token)
            {
                return Interceptor.ReadAllTextAsync(path, encoding, token);
            }

            /// <inheritdoc cref="IInterceptFileHandler.ReadLines(System.String)" />
            public static IEnumerable<String> ReadLines(String path)
            {
                return Interceptor.ReadLines(path);
            }

            /// <inheritdoc cref="IInterceptFileHandler.ReadLines(System.String, System.Text.Encoding)" />
            public static IEnumerable<String> ReadLines(String path, Encoding encoding)
            {
                return Interceptor.ReadLines(path, encoding);
            }

            /// <inheritdoc cref="IInterceptFileHandler.ReadAllLines(System.String)" />
            public static String[] ReadAllLines(String path)
            {
                return Interceptor.ReadAllLines(path);
            }

            /// <inheritdoc cref="IInterceptFileHandler.ReadAllLines(System.String, System.Text.Encoding)" />
            public static String[] ReadAllLines(String path, Encoding encoding)
            {
                return Interceptor.ReadAllLines(path, encoding);
            }

            /// <inheritdoc cref="IInterceptFileHandler.ReadAllLinesAsync(System.String, System.Threading.CancellationToken)" />
            public static Task<String[]> ReadAllLinesAsync(String path)
            {
                return Interceptor.ReadAllLinesAsync(path);
            }

            /// <inheritdoc cref="IInterceptFileHandler.ReadAllLinesAsync(System.String, System.Threading.CancellationToken)" />
            public static Task<String[]> ReadAllLinesAsync(String path, CancellationToken token)
            {
                return Interceptor.ReadAllLinesAsync(path, token);
            }

            /// <inheritdoc cref="IInterceptFileHandler.ReadAllLinesAsync(System.String, System.Text.Encoding, System.Threading.CancellationToken)" />
            public static Task<String[]> ReadAllLinesAsync(String path, Encoding encoding)
            {
                return Interceptor.ReadAllLinesAsync(path, encoding);
            }

            /// <inheritdoc cref="IInterceptFileHandler.ReadAllLinesAsync(System.String, System.Text.Encoding, System.Threading.CancellationToken)" />
            public static Task<String[]> ReadAllLinesAsync(String path, Encoding encoding, CancellationToken token)
            {
                return Interceptor.ReadAllLinesAsync(path, encoding, token);
            }

            /// <inheritdoc cref="IInterceptFileHandler.AppendAllText(System.String, System.String)" />
            public static void AppendAllText(String path, String? contents)
            {
                Interceptor.AppendAllText(path, contents);
            }

            /// <inheritdoc cref="IInterceptFileHandler.AppendAllText(System.String, System.String, System.Text.Encoding)" />
            public static void AppendAllText(String path, String? contents, Encoding encoding)
            {
                Interceptor.AppendAllText(path, contents, encoding);
            }

            /// <inheritdoc cref="IInterceptFileHandler.AppendAllTextAsync(System.String, System.String, System.Threading.CancellationToken)" />
            public static Task AppendAllTextAsync(String path, String? contents)
            {
                return Interceptor.AppendAllTextAsync(path, contents);
            }

            /// <inheritdoc cref="IInterceptFileHandler.AppendAllTextAsync(System.String, System.String, System.Threading.CancellationToken)" />
            public static Task AppendAllTextAsync(String path, String? contents, CancellationToken token)
            {
                return Interceptor.AppendAllTextAsync(path, contents, token);
            }

            /// <inheritdoc cref="IInterceptFileHandler.AppendAllTextAsync(System.String, System.String, System.Text.Encoding, System.Threading.CancellationToken)" />
            public static Task AppendAllTextAsync(String path, String? contents, Encoding encoding)
            {
                return Interceptor.AppendAllTextAsync(path, contents, encoding);
            }

            /// <inheritdoc cref="IInterceptFileHandler.AppendAllTextAsync(System.String, System.String, System.Text.Encoding, System.Threading.CancellationToken)" />
            public static Task AppendAllTextAsync(String path, String? contents, Encoding encoding, CancellationToken token)
            {
                return Interceptor.AppendAllTextAsync(path, contents, encoding, token);
            }

            /// <inheritdoc cref="IInterceptFileHandler.AppendAllLines(System.String, System.Collections.Generic.IEnumerable{System.String})" />
            public static void AppendAllLines(String path, IEnumerable<String> contents)
            {
                Interceptor.AppendAllLines(path, contents);
            }

            /// <inheritdoc cref="IInterceptFileHandler.AppendAllLines(System.String, System.Collections.Generic.IEnumerable{System.String}, System.Text.Encoding)" />
            public static void AppendAllLines(String path, IEnumerable<String> contents, Encoding encoding)
            {
                Interceptor.AppendAllLines(path, contents, encoding);
            }

            /// <inheritdoc cref="IInterceptFileHandler.AppendAllLinesAsync(System.String, System.Collections.Generic.IEnumerable{System.String}, System.Threading.CancellationToken)" />
            public static Task AppendAllLinesAsync(String path, IEnumerable<String> contents)
            {
                return Interceptor.AppendAllLinesAsync(path, contents);
            }

            /// <inheritdoc cref="IInterceptFileHandler.AppendAllLinesAsync(System.String, System.Collections.Generic.IEnumerable{System.String}, System.Threading.CancellationToken)" />
            public static Task AppendAllLinesAsync(String path, IEnumerable<String> contents, CancellationToken token)
            {
                return Interceptor.AppendAllLinesAsync(path, contents, token);
            }

            /// <inheritdoc cref="IInterceptFileHandler.AppendAllLinesAsync(System.String, System.Collections.Generic.IEnumerable{System.String}, System.Text.Encoding, System.Threading.CancellationToken)" />
            public static Task AppendAllLinesAsync(String path, IEnumerable<String> contents, Encoding encoding)
            {
                return Interceptor.AppendAllLinesAsync(path, contents, encoding);
            }

            /// <inheritdoc cref="IInterceptFileHandler.AppendAllLinesAsync(System.String, System.Collections.Generic.IEnumerable{System.String}, System.Text.Encoding, System.Threading.CancellationToken)" />
            public static Task AppendAllLinesAsync(String path, IEnumerable<String> contents, Encoding encoding, CancellationToken token)
            {
                return Interceptor.AppendAllLinesAsync(path, contents, encoding, token);
            }

            /// <inheritdoc cref="IInterceptFileHandler.WriteAllBytes(System.String, System.Byte[])" />
            public static void WriteAllBytes(String path, Byte[] bytes)
            {
                Interceptor.WriteAllBytes(path, bytes);
            }

            /// <inheritdoc cref="IInterceptFileHandler.WriteAllBytesAsync(System.String, System.Byte[], System.Threading.CancellationToken)" />
            public static Task WriteAllBytesAsync(String path, Byte[] bytes)
            {
                return Interceptor.WriteAllBytesAsync(path, bytes);
            }

            /// <inheritdoc cref="IInterceptFileHandler.WriteAllBytesAsync(System.String, System.Byte[], System.Threading.CancellationToken)" />
            public static Task WriteAllBytesAsync(String path, Byte[] bytes, CancellationToken token)
            {
                return Interceptor.WriteAllBytesAsync(path, bytes, token);
            }

            /// <inheritdoc cref="IInterceptFileHandler.WriteAllText(System.String, System.String)" />
            public static void WriteAllText(String path, String? contents)
            {
                Interceptor.WriteAllText(path, contents);
            }

            /// <inheritdoc cref="IInterceptFileHandler.WriteAllText(System.String, System.String, System.Text.Encoding)" />
            public static void WriteAllText(String path, String? contents, Encoding encoding)
            {
                Interceptor.WriteAllText(path, contents, encoding);
            }

            /// <inheritdoc cref="IInterceptFileHandler.WriteAllTextAsync(System.String, System.String, System.Threading.CancellationToken)" />
            public static Task WriteAllTextAsync(String path, String? contents)
            {
                return Interceptor.WriteAllTextAsync(path, contents);
            }

            /// <inheritdoc cref="IInterceptFileHandler.WriteAllTextAsync(System.String, System.String, System.Threading.CancellationToken)" />
            public static Task WriteAllTextAsync(String path, String? contents, CancellationToken token)
            {
                return Interceptor.WriteAllTextAsync(path, contents, token);
            }

            /// <inheritdoc cref="IInterceptFileHandler.WriteAllTextAsync(System.String, System.String, System.Text.Encoding, System.Threading.CancellationToken)" />
            public static Task WriteAllTextAsync(String path, String? contents, Encoding encoding)
            {
                return Interceptor.WriteAllTextAsync(path, contents, encoding);
            }

            /// <inheritdoc cref="IInterceptFileHandler.WriteAllTextAsync(System.String, System.String, System.Text.Encoding, System.Threading.CancellationToken)" />
            public static Task WriteAllTextAsync(String path, String? contents, Encoding encoding, CancellationToken token)
            {
                return Interceptor.WriteAllTextAsync(path, contents, encoding, token);
            }

            /// <inheritdoc cref="IInterceptFileHandler.WriteAllLines(System.String, System.String[])" />
            public static void WriteAllLines(String path, String[] contents)
            {
                Interceptor.WriteAllLines(path, contents);
            }

            /// <inheritdoc cref="IInterceptFileHandler.WriteAllLines(System.String, System.Collections.Generic.IEnumerable{System.String})" />
            public static void WriteAllLines(String path, IEnumerable<String> contents)
            {
                Interceptor.WriteAllLines(path, contents);
            }

            /// <inheritdoc cref="IInterceptFileHandler.WriteAllLines(System.String, System.String[], System.Text.Encoding)" />
            public static void WriteAllLines(String path, String[] contents, Encoding encoding)
            {
                Interceptor.WriteAllLines(path, contents, encoding);
            }

            /// <inheritdoc cref="IInterceptFileHandler.WriteAllLines(System.String, System.Collections.Generic.IEnumerable{System.String}, System.Text.Encoding)" />
            public static void WriteAllLines(String path, IEnumerable<String> contents, Encoding encoding)
            {
                Interceptor.WriteAllLines(path, contents, encoding);
            }

            /// <inheritdoc cref="IInterceptFileHandler.Encrypt(System.String)" />
            [SupportedOSPlatform("windows")]
            public static void Encrypt(String path)
            {
                Interceptor.Encrypt(path);
            }

            /// <inheritdoc cref="IInterceptFileHandler.Decrypt(System.String)" />
            [SupportedOSPlatform("windows")]
            public static void Decrypt(String path)
            {
                Interceptor.Decrypt(path);
            }

            /// <inheritdoc cref="IInterceptFileHandler.Move(System.String, System.String)" />
            public static void Move(String sourceFileName, String destFileName)
            {
                Interceptor.Move(sourceFileName, destFileName);
            }

            /// <inheritdoc cref="IInterceptFileHandler.Move(System.String, System.String, System.Boolean)" />
            public static void Move(String sourceFileName, String destFileName, Boolean overwrite)
            {
                Interceptor.Move(sourceFileName, destFileName, overwrite);
            }

            /// <inheritdoc cref="IInterceptFileHandler.Copy(System.String, System.String)" />
            public static void Copy(String sourceFileName, String destFileName)
            {
                Interceptor.Copy(sourceFileName, destFileName);
            }

            /// <inheritdoc cref="IInterceptFileHandler.Copy(System.String, System.String, System.Boolean)" />
            public static void Copy(String sourceFileName, String destFileName, Boolean overwrite)
            {
                Interceptor.Copy(sourceFileName, destFileName, overwrite);
            }

            /// <inheritdoc cref="IInterceptFileHandler.Replace(System.String, System.String, System.String)" />
            public static void Replace(String sourceFileName, String destinationFileName, String? destinationBackupFileName)
            {
                Interceptor.Replace(sourceFileName, destinationFileName, destinationBackupFileName);
            }

            /// <inheritdoc cref="IInterceptFileHandler.Replace(System.String, System.String, System.String, System.Boolean)" />
            public static void Replace(String sourceFileName, String destinationFileName, String? destinationBackupFileName, Boolean ignoreMetadataErrors)
            {
                Interceptor.Replace(sourceFileName, destinationFileName, destinationBackupFileName, ignoreMetadataErrors);
            }

            /// <inheritdoc cref="IInterceptFileHandler.Delete(System.String)" />
            public static void Delete(String path)
            {
                Interceptor.Delete(path);
            }
        }
    }
}