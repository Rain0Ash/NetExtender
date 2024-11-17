using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.Versioning;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.IO.Interfaces;

namespace NetExtender.IO
{
    public partial class FileSystemHandler
    {
        FileSystemInfo IFileHandler.CreateSymbolicLink(String path, String target)
        {
            return CreateSymbolicLink(path, target, FileSystemHandlerType.File);
        }

        FileSystemInfo? IFileHandler.ResolveLinkTarget(String path, Boolean target)
        {
            return ResolveLinkTarget(path, target, FileSystemHandlerType.File);
        }

        Boolean IFileHandler.Exists([NotNullWhen(true)] String? path)
        {
            return Exists(path, FileSystemHandlerType.File);
        }

        FileAttributes IFileHandler.GetAttributes(String path)
        {
            return GetAttributes(path, FileSystemHandlerType.File);
        }

        void IFileHandler.SetAttributes(String path, FileAttributes attributes)
        {
            SetAttributes(path, attributes, FileSystemHandlerType.File);
        }

        DateTime IFileHandler.GetCreationTime(String path)
        {
            return GetCreationTime(path, FileSystemHandlerType.File);
        }

        DateTime IFileHandler.GetCreationTimeUtc(String path)
        {
            return GetCreationTimeUtc(path, FileSystemHandlerType.File);
        }

        DateTime IFileHandler.GetLastAccessTime(String path)
        {
            return GetLastAccessTime(path, FileSystemHandlerType.File);
        }

        DateTime IFileHandler.GetLastAccessTimeUtc(String path)
        {
            return GetLastAccessTimeUtc(path, FileSystemHandlerType.File);
        }

        DateTime IFileHandler.GetLastWriteTime(String path)
        {
            return GetLastWriteTime(path, FileSystemHandlerType.File);
        }

        DateTime IFileHandler.GetLastWriteTimeUtc(String path)
        {
            return GetLastWriteTimeUtc(path, FileSystemHandlerType.File);
        }
        
        void IFileHandler.SetCreationTime(String path, DateTime time)
        {
            SetCreationTime(path, time, FileSystemHandlerType.File);
        }

        void IFileHandler.SetCreationTimeUtc(String path, DateTime time)
        {
            SetCreationTimeUtc(path, time, FileSystemHandlerType.File);
        }

        void IFileHandler.SetLastAccessTime(String path, DateTime time)
        {
            SetLastAccessTime(path, time, FileSystemHandlerType.File);
        }

        void IFileHandler.SetLastAccessTimeUtc(String path, DateTime time)
        {
            SetLastAccessTimeUtc(path, time, FileSystemHandlerType.File);
        }

        void IFileHandler.SetLastWriteTime(String path, DateTime time)
        {
            SetLastWriteTime(path, time, FileSystemHandlerType.File);
        }

        void IFileHandler.SetLastWriteTimeUtc(String path, DateTime time)
        {
            SetLastWriteTimeUtc(path, time, FileSystemHandlerType.File);
        }

        public virtual FileStream Open(String path, FileMode mode)
        {
            return File.Open(path, mode);
        }

        public virtual FileStream Open(String path, FileMode mode, FileAccess access)
        {
            return File.Open(path, mode, access);
        }

        public virtual FileStream Open(String path, FileMode mode, FileAccess access, FileShare share)
        {
            return File.Open(path, mode, access, share);
        }

        public virtual FileStream OpenRead(String path)
        {
            return File.OpenRead(path);
        }

        public virtual FileStream OpenWrite(String path)
        {
            return File.OpenWrite(path);
        }

        public virtual StreamReader OpenText(String path)
        {
            return File.OpenText(path);
        }

        FileStream IFileHandler.Create(String path)
        {
            return Create(path, FileSystemHandlerType.File);
        }

        public virtual FileStream Create(String path, Int32 buffer)
        {
            return File.Create(path, buffer);
        }

        public virtual FileStream Create(String path, Int32 buffer, FileOptions options)
        {
            return File.Create(path, buffer, options);
        }

        public virtual StreamWriter CreateText(String path)
        {
            return File.CreateText(path);
        }

        public virtual StreamWriter AppendText(String path)
        {
            return File.AppendText(path);
        }

        public virtual Byte[] ReadAllBytes(String path)
        {
            return File.ReadAllBytes(path);
        }

        public Task<Byte[]> ReadAllBytesAsync(String path)
        {
            return ReadAllBytesAsync(path, CancellationToken.None);
        }

        public virtual Task<Byte[]> ReadAllBytesAsync(String path, CancellationToken token)
        {
            return File.ReadAllBytesAsync(path, token);
        }

        public virtual String ReadAllText(String path)
        {
            return File.ReadAllText(path);
        }

        public virtual String ReadAllText(String path, Encoding encoding)
        {
            return File.ReadAllText(path, encoding);
        }

        public Task<String> ReadAllTextAsync(String path)
        {
            return ReadAllTextAsync(path, CancellationToken.None);
        }

        public virtual Task<String> ReadAllTextAsync(String path, CancellationToken token)
        {
            return File.ReadAllTextAsync(path, token);
        }

        public Task<String> ReadAllTextAsync(String path, Encoding encoding)
        {
            return ReadAllTextAsync(path, encoding, CancellationToken.None);
        }

        public virtual Task<String> ReadAllTextAsync(String path, Encoding encoding, CancellationToken token)
        {
            return File.ReadAllTextAsync(path, encoding, token);
        }

        public virtual IEnumerable<String> ReadLines(String path)
        {
            return File.ReadAllLines(path);
        }

        public virtual IEnumerable<String> ReadLines(String path, Encoding encoding)
        {
            return File.ReadAllLines(path, encoding);
        }

        public virtual String[] ReadAllLines(String path)
        {
            return File.ReadAllLines(path);
        }

        public virtual String[] ReadAllLines(String path, Encoding encoding)
        {
            return File.ReadAllLines(path, encoding);
        }

        public Task<String[]> ReadAllLinesAsync(String path)
        {
            return ReadAllLinesAsync(path, CancellationToken.None);
        }

        public virtual Task<String[]> ReadAllLinesAsync(String path, CancellationToken token)
        {
            return File.ReadAllLinesAsync(path, token);
        }

        public Task<String[]> ReadAllLinesAsync(String path, Encoding encoding)
        {
            return ReadAllLinesAsync(path, encoding, CancellationToken.None);
        }

        public virtual Task<String[]> ReadAllLinesAsync(String path, Encoding encoding, CancellationToken token)
        {
            return File.ReadAllLinesAsync(path, encoding, token);
        }

        public virtual void AppendAllText(String path, String? contents)
        {
            File.AppendAllText(path, contents);
        }

        public virtual void AppendAllText(String path, String? contents, Encoding encoding)
        {
            File.AppendAllText(path, contents, encoding);
        }

        public Task AppendAllTextAsync(String path, String? contents)
        {
            return AppendAllTextAsync(path, contents, CancellationToken.None);
        }

        public virtual Task AppendAllTextAsync(String path, String? contents, CancellationToken token)
        {
            return File.AppendAllTextAsync(path, contents, token);
        }

        public Task AppendAllTextAsync(String path, String? contents, Encoding encoding)
        {
            return AppendAllTextAsync(path, contents, encoding, CancellationToken.None);
        }

        public virtual Task AppendAllTextAsync(String path, String? contents, Encoding encoding, CancellationToken token)
        {
            return File.AppendAllTextAsync(path, contents, encoding, token);
        }

        public virtual void AppendAllLines(String path, IEnumerable<String> contents)
        {
            File.AppendAllLines(path, contents);
        }

        public virtual void AppendAllLines(String path, IEnumerable<String> contents, Encoding encoding)
        {
            File.AppendAllLines(path, contents, encoding);
        }

        public Task AppendAllLinesAsync(String path, IEnumerable<String> contents)
        {
            return AppendAllLinesAsync(path, contents, CancellationToken.None);
        }

        public virtual Task AppendAllLinesAsync(String path, IEnumerable<String> contents, CancellationToken token)
        {
            return File.AppendAllLinesAsync(path, contents, token);
        }

        public Task AppendAllLinesAsync(String path, IEnumerable<String> contents, Encoding encoding)
        {
            return AppendAllLinesAsync(path, contents, encoding, CancellationToken.None);
        }

        public virtual Task AppendAllLinesAsync(String path, IEnumerable<String> contents, Encoding encoding, CancellationToken token)
        {
            return File.AppendAllLinesAsync(path, contents, encoding, token);
        }

        public virtual void WriteAllBytes(String path, params Byte[]? bytes)
        {
            if (bytes is not null)
            {
                File.WriteAllBytes(path, bytes);
            }
        }

        public Task WriteAllBytesAsync(String path, Byte[] bytes)
        {
            return WriteAllBytesAsync(path, bytes, CancellationToken.None);
        }

        public virtual Task WriteAllBytesAsync(String path, Byte[] bytes, CancellationToken token)
        {
            return File.WriteAllBytesAsync(path, bytes, token);
        }

        public virtual void WriteAllText(String path, String? contents)
        {
            File.WriteAllText(path, contents);
        }

        public virtual void WriteAllText(String path, String? contents, Encoding encoding)
        {
            File.WriteAllText(path, contents, encoding);
        }

        public Task WriteAllTextAsync(String path, String? contents)
        {
            return WriteAllTextAsync(path, contents, CancellationToken.None);
        }

        public virtual Task WriteAllTextAsync(String path, String? contents, CancellationToken token)
        {
            return File.WriteAllTextAsync(path, contents, token);
        }

        public Task WriteAllTextAsync(String path, String? contents, Encoding encoding)
        {
            return WriteAllTextAsync(path, contents, encoding, CancellationToken.None);
        }

        public virtual Task WriteAllTextAsync(String path, String? contents, Encoding encoding, CancellationToken token)
        {
            return File.WriteAllTextAsync(path, contents, encoding, token);
        }

        public virtual void WriteAllLines(String path, params String[]? contents)
        {
            if (contents is not null)
            {
                File.WriteAllLines(path, contents);
            }
        }

        public virtual void WriteAllLines(String path, IEnumerable<String> contents)
        {
            File.WriteAllLines(path, contents);
        }

        public void WriteAllLines(String path, Encoding encoding, params String[]? contents)
        {
            if (contents is not null)
            {
                WriteAllLines(path, contents, encoding);
            }
        }

        public virtual void WriteAllLines(String path, String[] contents, Encoding encoding)
        {
            File.WriteAllLines(path, contents, encoding);
        }

        public virtual void WriteAllLines(String path, IEnumerable<String> contents, Encoding encoding)
        {
            File.WriteAllLines(path, contents, encoding);
        }

        public Task WriteAllLinesAsync(String path, IEnumerable<String> contents)
        {
            return WriteAllLinesAsync(path, contents, CancellationToken.None);
        }

        public virtual Task WriteAllLinesAsync(String path, IEnumerable<String> contents, CancellationToken token)
        {
            return File.WriteAllLinesAsync(path, contents, token);
        }

        public Task WriteAllLinesAsync(String path, IEnumerable<String> contents, Encoding encoding)
        {
            return WriteAllLinesAsync(path, contents, encoding, CancellationToken.None);
        }

        public virtual Task WriteAllLinesAsync(String path, IEnumerable<String> contents, Encoding encoding, CancellationToken token)
        {
            return File.WriteAllLinesAsync(path, contents, encoding, token);
        }

        [SupportedOSPlatform("windows")]
        void IFileHandler.Encrypt(String path)
        {
            Encrypt(path, FileSystemHandlerType.File);
        }

        [SupportedOSPlatform("windows")]
        void IFileHandler.Decrypt(String path)
        {
            Decrypt(path, FileSystemHandlerType.File);
        }

        void IFileHandler.Move(String source, String destination, Boolean overwrite)
        {
            Move(source, destination, overwrite, FileSystemHandlerType.File);
        }

        void IFileHandler.Copy(String source, String destination)
        {
            Copy(source, destination, FileSystemHandlerType.File);
        }

        void IFileHandler.Copy(String source, String destination, Boolean overwrite)
        {
            Copy(source, destination, overwrite, FileSystemHandlerType.File);
        }

        void IFileHandler.Replace(String source, String destination, String? backup)
        {
            Replace(source, destination, backup, FileSystemHandlerType.File);
        }

        void IFileHandler.Replace(String source, String destination, String? backup, Boolean suppress)
        {
            Replace(source, destination, backup, suppress, FileSystemHandlerType.File);
        }
    }
}