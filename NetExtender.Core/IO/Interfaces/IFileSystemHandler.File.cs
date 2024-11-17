using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.Versioning;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetExtender.IO.Interfaces
{
    public interface IFileHandler
    {
        /// <inheritdoc cref="System.IO.File.CreateSymbolicLink(System.String, System.String)" />
        public FileSystemInfo CreateSymbolicLink(String path, String target);

        /// <inheritdoc cref="System.IO.File.ResolveLinkTarget(System.String, System.Boolean)" />
        public FileSystemInfo? ResolveLinkTarget(String path, Boolean target);

        /// <inheritdoc cref="System.IO.File.Exists(System.String)" />
        public Boolean Exists([NotNullWhen(true)] String? path);

        /// <inheritdoc cref="System.IO.File.GetAttributes(System.String)" />
        public FileAttributes GetAttributes(String path);

        /// <inheritdoc cref="System.IO.File.SetAttributes(System.String, System.IO.FileAttributes)" />
        public void SetAttributes(String path, FileAttributes attributes);

        /// <inheritdoc cref="System.IO.File.GetCreationTime(System.String)" />
        public DateTime GetCreationTime(String path);

        /// <inheritdoc cref="System.IO.File.GetCreationTimeUtc(System.String)" />
        public DateTime GetCreationTimeUtc(String path);

        /// <inheritdoc cref="System.IO.File.GetLastAccessTime(System.String)" />
        public DateTime GetLastAccessTime(String path);

        /// <inheritdoc cref="System.IO.File.GetLastAccessTimeUtc(System.String)" />
        public DateTime GetLastAccessTimeUtc(String path);

        /// <inheritdoc cref="System.IO.File.GetLastWriteTime(System.String)" />
        public DateTime GetLastWriteTime(String path);

        /// <inheritdoc cref="System.IO.File.GetLastWriteTimeUtc(System.String)" />
        public DateTime GetLastWriteTimeUtc(String path);

        /// <inheritdoc cref="System.IO.File.SetCreationTime(System.String, System.DateTime)" />
        public void SetCreationTime(String path, DateTime time);

        /// <inheritdoc cref="System.IO.File.SetCreationTimeUtc(System.String, System.DateTime)" />
        public void SetCreationTimeUtc(String path, DateTime time);

        /// <inheritdoc cref="System.IO.File.SetLastAccessTime(System.String, System.DateTime)" />
        public void SetLastAccessTime(String path, DateTime time);

        /// <inheritdoc cref="System.IO.File.SetLastAccessTimeUtc(System.String, System.DateTime)" />
        public void SetLastAccessTimeUtc(String path, DateTime time);

        /// <inheritdoc cref="System.IO.File.SetLastWriteTime(System.String, System.DateTime)" />
        public void SetLastWriteTime(String path, DateTime time);

        /// <inheritdoc cref="System.IO.File.SetLastWriteTimeUtc(System.String, System.DateTime)" />
        public void SetLastWriteTimeUtc(String path, DateTime time);

        /// <inheritdoc cref="System.IO.File.Open(System.String, System.IO.FileMode)" />
        public FileStream Open(String path, FileMode mode);

        /// <inheritdoc cref="System.IO.File.Open(System.String, System.IO.FileMode, System.IO.FileAccess)" />
        public FileStream Open(String path, FileMode mode, FileAccess access);

        /// <inheritdoc cref="System.IO.File.Open(System.String, System.IO.FileMode, System.IO.FileAccess, System.IO.FileShare)" />
        public FileStream Open(String path, FileMode mode, FileAccess access, FileShare share);

        /// <inheritdoc cref="System.IO.File.OpenRead(System.String)" />
        public FileStream OpenRead(String path);

        /// <inheritdoc cref="System.IO.File.OpenWrite(System.String)" />
        public FileStream OpenWrite(String path);

        /// <inheritdoc cref="System.IO.File.OpenText(System.String)" />
        public StreamReader OpenText(String path);

        /// <inheritdoc cref="System.IO.File.Create(System.String)" />
        public FileStream Create(String path);

        /// <inheritdoc cref="System.IO.File.Create(System.String, System.Int32)" />
        public FileStream Create(String path, Int32 buffer);

        /// <inheritdoc cref="System.IO.File.Create(System.String, System.Int32, System.IO.FileOptions)" />
        public FileStream Create(String path, Int32 buffer, FileOptions options);

        /// <inheritdoc cref="System.IO.File.CreateText(System.String)" />
        public StreamWriter CreateText(String path);

        /// <inheritdoc cref="System.IO.File.AppendText(System.String)" />
        public StreamWriter AppendText(String path);

        /// <inheritdoc cref="System.IO.File.ReadAllBytes(System.String)" />
        public Byte[] ReadAllBytes(String path);

        /// <inheritdoc cref="System.IO.File.ReadAllBytesAsync(System.String, System.Threading.CancellationToken)" />
        public Task<Byte[]> ReadAllBytesAsync(String path);

        /// <inheritdoc cref="System.IO.File.ReadAllBytesAsync(System.String, System.Threading.CancellationToken)" />
        public Task<Byte[]> ReadAllBytesAsync(String path, CancellationToken token);

        /// <inheritdoc cref="System.IO.File.ReadAllText(System.String)" />
        public String ReadAllText(String path);

        /// <inheritdoc cref="System.IO.File.ReadAllText(System.String, System.Text.Encoding)" />
        public String ReadAllText(String path, Encoding encoding);

        /// <inheritdoc cref="System.IO.File.ReadAllTextAsync(System.String, System.Threading.CancellationToken)" />
        public Task<String> ReadAllTextAsync(String path);

        /// <inheritdoc cref="System.IO.File.ReadAllTextAsync(System.String, System.Threading.CancellationToken)" />
        public Task<String> ReadAllTextAsync(String path, CancellationToken token);

        /// <inheritdoc cref="System.IO.File.ReadAllTextAsync(System.String, System.Text.Encoding, System.Threading.CancellationToken)" />
        public Task<String> ReadAllTextAsync(String path, Encoding encoding);

        /// <inheritdoc cref="System.IO.File.ReadAllTextAsync(System.String, System.Text.Encoding, System.Threading.CancellationToken)" />
        public Task<String> ReadAllTextAsync(String path, Encoding encoding, CancellationToken token);

        /// <inheritdoc cref="System.IO.File.ReadLines(System.String)" />
        public IEnumerable<String> ReadLines(String path);

        /// <inheritdoc cref="System.IO.File.ReadLines(System.String, System.Text.Encoding)" />
        public IEnumerable<String> ReadLines(String path, Encoding encoding);

        /// <inheritdoc cref="System.IO.File.ReadAllLines(System.String)" />
        public String[] ReadAllLines(String path);

        /// <inheritdoc cref="System.IO.File.ReadAllLines(System.String, System.Text.Encoding)" />
        public String[] ReadAllLines(String path, Encoding encoding);

        /// <inheritdoc cref="System.IO.File.ReadAllLinesAsync(System.String, System.Threading.CancellationToken)" />
        public Task<String[]> ReadAllLinesAsync(String path);

        /// <inheritdoc cref="System.IO.File.ReadAllLinesAsync(System.String, System.Threading.CancellationToken)" />
        public Task<String[]> ReadAllLinesAsync(String path, CancellationToken token);

        /// <inheritdoc cref="System.IO.File.ReadAllLinesAsync(System.String, System.Text.Encoding, System.Threading.CancellationToken)" />
        public Task<String[]> ReadAllLinesAsync(String path, Encoding encoding);

        /// <inheritdoc cref="System.IO.File.ReadAllLinesAsync(System.String, System.Text.Encoding, System.Threading.CancellationToken)" />
        public Task<String[]> ReadAllLinesAsync(String path, Encoding encoding, CancellationToken token);

        /// <inheritdoc cref="System.IO.File.AppendAllText(System.String, System.String)" />
        public void AppendAllText(String path, String? contents);

        /// <inheritdoc cref="System.IO.File.AppendAllText(System.String, System.String, System.Text.Encoding)" />
        public void AppendAllText(String path, String? contents, Encoding encoding);

        /// <inheritdoc cref="System.IO.File.AppendAllTextAsync(System.String, System.String, System.Threading.CancellationToken)" />
        public Task AppendAllTextAsync(String path, String? contents);

        /// <inheritdoc cref="System.IO.File.AppendAllTextAsync(System.String, System.String, System.Threading.CancellationToken)" />
        public Task AppendAllTextAsync(String path, String? contents, CancellationToken token);

        /// <inheritdoc cref="System.IO.File.AppendAllTextAsync(System.String, System.String, System.Text.Encoding, System.Threading.CancellationToken)" />
        public Task AppendAllTextAsync(String path, String? contents, Encoding encoding);

        /// <inheritdoc cref="System.IO.File.AppendAllTextAsync(System.String, System.String, System.Text.Encoding, System.Threading.CancellationToken)" />
        public Task AppendAllTextAsync(String path, String? contents, Encoding encoding, CancellationToken token);

        /// <inheritdoc cref="System.IO.File.AppendAllLines(System.String, System.Collections.Generic.IEnumerable{System.String})" />
        public void AppendAllLines(String path, IEnumerable<String> contents);

        /// <inheritdoc cref="System.IO.File.AppendAllLines(System.String, System.Collections.Generic.IEnumerable{System.String}, System.Text.Encoding)" />
        public void AppendAllLines(String path, IEnumerable<String> contents, Encoding encoding);

        /// <inheritdoc cref="System.IO.File.AppendAllLinesAsync(System.String, System.Collections.Generic.IEnumerable{System.String}, System.Threading.CancellationToken)" />
        public Task AppendAllLinesAsync(String path, IEnumerable<String> contents);

        /// <inheritdoc cref="System.IO.File.AppendAllLinesAsync(System.String, System.Collections.Generic.IEnumerable{System.String}, System.Threading.CancellationToken)" />
        public Task AppendAllLinesAsync(String path, IEnumerable<String> contents, CancellationToken token);

        /// <inheritdoc cref="System.IO.File.AppendAllLinesAsync(System.String, System.Collections.Generic.IEnumerable{System.String}, System.Text.Encoding, System.Threading.CancellationToken)" />
        public Task AppendAllLinesAsync(String path, IEnumerable<String> contents, Encoding encoding);

        /// <inheritdoc cref="System.IO.File.AppendAllLinesAsync(System.String, System.Collections.Generic.IEnumerable{System.String}, System.Text.Encoding, System.Threading.CancellationToken)" />
        public Task AppendAllLinesAsync(String path, IEnumerable<String> contents, Encoding encoding, CancellationToken token);

        /// <inheritdoc cref="System.IO.File.WriteAllBytes(System.String, System.Byte[])" />
        public void WriteAllBytes(String path, params Byte[]? bytes);

        /// <inheritdoc cref="System.IO.File.WriteAllBytesAsync(System.String, System.Byte[], System.Threading.CancellationToken)" />
        public Task WriteAllBytesAsync(String path, Byte[] bytes);

        /// <inheritdoc cref="System.IO.File.WriteAllBytesAsync(System.String, System.Byte[], System.Threading.CancellationToken)" />
        public Task WriteAllBytesAsync(String path, Byte[] bytes, CancellationToken token);

        /// <inheritdoc cref="System.IO.File.WriteAllText(System.String, System.String)" />
        public void WriteAllText(String path, String? contents);

        /// <inheritdoc cref="System.IO.File.WriteAllText(System.String, System.String, System.Text.Encoding)" />
        public void WriteAllText(String path, String? contents, Encoding encoding);

        /// <inheritdoc cref="System.IO.File.WriteAllTextAsync(System.String, System.String, System.Threading.CancellationToken)" />
        public Task WriteAllTextAsync(String path, String? contents);

        /// <inheritdoc cref="System.IO.File.WriteAllTextAsync(System.String, System.String, System.Threading.CancellationToken)" />
        public Task WriteAllTextAsync(String path, String? contents, CancellationToken token);

        /// <inheritdoc cref="System.IO.File.WriteAllTextAsync(System.String, System.String, System.Text.Encoding, System.Threading.CancellationToken)" />
        public Task WriteAllTextAsync(String path, String? contents, Encoding encoding);

        /// <inheritdoc cref="System.IO.File.WriteAllTextAsync(System.String, System.String, System.Text.Encoding, System.Threading.CancellationToken)" />
        public Task WriteAllTextAsync(String path, String? contents, Encoding encoding, CancellationToken token);

        /// <inheritdoc cref="System.IO.File.WriteAllLines(System.String, System.String[])" />
        public void WriteAllLines(String path, params String[]? contents);

        /// <inheritdoc cref="System.IO.File.WriteAllLines(System.String, System.Collections.Generic.IEnumerable{System.String})" />
        public void WriteAllLines(String path, IEnumerable<String> contents);

        /// <inheritdoc cref="System.IO.File.WriteAllLines(System.String, System.String[], System.Text.Encoding)" />
        public void WriteAllLines(String path, Encoding encoding, params String[]? contents);

        /// <inheritdoc cref="System.IO.File.WriteAllLines(System.String, System.String[], System.Text.Encoding)" />
        public void WriteAllLines(String path, String[] contents, Encoding encoding);

        /// <inheritdoc cref="System.IO.File.WriteAllLines(System.String, System.Collections.Generic.IEnumerable{System.String}, System.Text.Encoding)" />
        public void WriteAllLines(String path, IEnumerable<String> contents, Encoding encoding);

        /// <inheritdoc cref="System.IO.File.WriteAllLinesAsync(System.String, System.Collections.Generic.IEnumerable{System.String}, System.Threading.CancellationToken)" />
        public Task WriteAllLinesAsync(String path, IEnumerable<String> contents);

        /// <inheritdoc cref="System.IO.File.WriteAllLinesAsync(System.String, System.Collections.Generic.IEnumerable{System.String}, System.Threading.CancellationToken)" />
        public Task WriteAllLinesAsync(String path, IEnumerable<String> contents, CancellationToken token);

        /// <inheritdoc cref="System.IO.File.WriteAllLinesAsync(System.String, System.Collections.Generic.IEnumerable{System.String}, System.Text.Encoding, System.Threading.CancellationToken)" />
        public Task WriteAllLinesAsync(String path, IEnumerable<String> contents, Encoding encoding);

        /// <inheritdoc cref="System.IO.File.WriteAllLinesAsync(System.String, System.Collections.Generic.IEnumerable{System.String}, System.Text.Encoding, System.Threading.CancellationToken)" />
        public Task WriteAllLinesAsync(String path, IEnumerable<String> contents, Encoding encoding, CancellationToken token);

        /// <inheritdoc cref="System.IO.File.Encrypt(System.String)" />
        [SupportedOSPlatform("windows")]
        public void Encrypt(String path);

        /// <inheritdoc cref="System.IO.File.Decrypt(System.String)" />
        [SupportedOSPlatform("windows")]
        public void Decrypt(String path);

        /// <inheritdoc cref="System.IO.File.Move(System.String, System.String)" />
        public void Move(String source, String destination);

        /// <inheritdoc cref="System.IO.File.Move(System.String, System.String, System.Boolean)" />
        public void Move(String source, String destination, Boolean overwrite);

        /// <inheritdoc cref="System.IO.File.Copy(System.String, System.String)" />
        public void Copy(String source, String destination);

        /// <inheritdoc cref="System.IO.File.Copy(System.String, System.String, System.Boolean)" />
        public void Copy(String source, String destination, Boolean overwrite);

        /// <inheritdoc cref="System.IO.File.Replace(System.String, System.String, System.String)" />
        public void Replace(String source, String destination, String? backup);

        /// <inheritdoc cref="System.IO.File.Replace(System.String, System.String, System.String, System.Boolean)" />
        public void Replace(String source, String destination, String? backup, Boolean suppress);

        /// <inheritdoc cref="System.IO.File.Delete(System.String)" />
        public void Delete(String path);
    }
}