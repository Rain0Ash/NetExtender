// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetExtender.FileSystems
{
    public partial class FileSystemHandlerWrapper<T>
    {
        public override FileStream Open(String path, FileMode mode)
        {
            return FileSystem.File.Open(path, mode);
        }

        public override FileStream Open(String path, FileMode mode, FileAccess access)
        {
            return FileSystem.File.Open(path, mode, access);
        }

        public override FileStream Open(String path, FileMode mode, FileAccess access, FileShare share)
        {
            return FileSystem.File.Open(path, mode, access, share);
        }

        public override FileStream Open(String path, FileStreamOptions options)
        {
            return FileSystem.File.Open(path, options);
        }

        public override FileStream OpenRead(String path)
        {
            return FileSystem.File.OpenRead(path);
        }

        public override FileStream OpenWrite(String path)
        {
            return FileSystem.File.OpenWrite(path);
        }

        public override StreamReader OpenText(String path)
        {
            return FileSystem.File.OpenText(path);
        }

        public override FileStream Create(String path, Int32 buffer)
        {
            return FileSystem.File.Create(path, buffer);
        }

        public override FileStream Create(String path, Int32 buffer, FileOptions options)
        {
            return FileSystem.File.Create(path, buffer, options);
        }

        public override StreamWriter CreateText(String path)
        {
            return FileSystem.File.CreateText(path);
        }

        public override StreamWriter AppendText(String path)
        {
            return FileSystem.File.AppendText(path);
        }

        public override Byte[] ReadAllBytes(String path)
        {
            return FileSystem.File.ReadAllBytes(path);
        }

        public override Task<Byte[]> ReadAllBytesAsync(String path, CancellationToken token)
        {
            return FileSystem.File.ReadAllBytesAsync(path, token);
        }

        public override String ReadAllText(String path)
        {
            return FileSystem.File.ReadAllText(path);
        }

        public override String ReadAllText(String path, Encoding? encoding)
        {
            return FileSystem.File.ReadAllText(path, encoding);
        }

        public override Task<String> ReadAllTextAsync(String path, CancellationToken token)
        {
            return FileSystem.File.ReadAllTextAsync(path, token);
        }

        public override Task<String> ReadAllTextAsync(String path, Encoding? encoding, CancellationToken token)
        {
            return FileSystem.File.ReadAllTextAsync(path, encoding, token);
        }

        public override IEnumerable<String> ReadLines(String path)
        {
            return FileSystem.File.ReadAllLines(path);
        }

        public override IEnumerable<String> ReadLines(String path, Encoding? encoding)
        {
            return FileSystem.File.ReadAllLines(path, encoding);
        }

        public override String[] ReadAllLines(String path)
        {
            return FileSystem.File.ReadAllLines(path);
        }

        public override String[] ReadAllLines(String path, Encoding? encoding)
        {
            return FileSystem.File.ReadAllLines(path, encoding);
        }

        public override Task<String[]> ReadAllLinesAsync(String path, CancellationToken token)
        {
            return FileSystem.File.ReadAllLinesAsync(path, token);
        }

        public override Task<String[]> ReadAllLinesAsync(String path, Encoding? encoding, CancellationToken token)
        {
            return FileSystem.File.ReadAllLinesAsync(path, encoding, token);
        }

        public override void AppendAllText(String path, String? contents)
        {
            FileSystem.File.AppendAllText(path, contents);
        }

        public override void AppendAllText(String path, String? contents, Encoding? encoding)
        {
            FileSystem.File.AppendAllText(path, contents, encoding);
        }

        public override Task AppendAllTextAsync(String path, String? contents, CancellationToken token)
        {
            return FileSystem.File.AppendAllTextAsync(path, contents, token);
        }

        public override Task AppendAllTextAsync(String path, String? contents, Encoding? encoding, CancellationToken token)
        {
            return FileSystem.File.AppendAllTextAsync(path, contents, encoding, token);
        }

        public override void AppendAllLines(String path, IEnumerable<String?>? contents)
        {
            FileSystem.File.AppendAllLines(path, contents);
        }

        public override void AppendAllLines(String path, IEnumerable<String?>? contents, Encoding? encoding)
        {
            FileSystem.File.AppendAllLines(path, contents, encoding);
        }

        public override Task AppendAllLinesAsync(String path, IEnumerable<String?>? contents, CancellationToken token)
        {
            return FileSystem.File.AppendAllLinesAsync(path, contents, token);
        }

        public override Task AppendAllLinesAsync(String path, IEnumerable<String?>? contents, Encoding? encoding, CancellationToken token)
        {
            return FileSystem.File.AppendAllLinesAsync(path, contents, encoding, token);
        }

        public override void WriteAllBytes(String path, params Byte[]? bytes)
        {
            FileSystem.File.WriteAllBytes(path, bytes);
        }

        public override Task WriteAllBytesAsync(String path, Byte[]? bytes, CancellationToken token)
        {
            return FileSystem.File.WriteAllBytesAsync(path, bytes, token);
        }

        public override void WriteAllText(String path, String? contents)
        {
            FileSystem.File.WriteAllText(path, contents);
        }

        public override void WriteAllText(String path, String? contents, Encoding? encoding)
        {
            FileSystem.File.WriteAllText(path, contents, encoding);
        }

        public override Task WriteAllTextAsync(String path, String? contents, CancellationToken token)
        {
            return FileSystem.File.WriteAllTextAsync(path, contents, token);
        }

        public override Task WriteAllTextAsync(String path, String? contents, Encoding? encoding, CancellationToken token)
        {
            return FileSystem.File.WriteAllTextAsync(path, contents, encoding, token);
        }

        public override void WriteAllLines(String path, params String?[]? contents)
        {
            FileSystem.File.WriteAllLines(path, contents);
        }

        public override void WriteAllLines(String path, IEnumerable<String?>? contents)
        {
            FileSystem.File.WriteAllLines(path, contents);
        }

        public override void WriteAllLines(String path, String?[]? contents, Encoding? encoding)
        {
            FileSystem.File.WriteAllLines(path, contents, encoding);
        }

        public override void WriteAllLines(String path, IEnumerable<String?>? contents, Encoding? encoding)
        {
            FileSystem.File.WriteAllLines(path, contents, encoding);
        }

        public override Task WriteAllLinesAsync(String path, IEnumerable<String?>? contents, CancellationToken token)
        {
            return FileSystem.File.WriteAllLinesAsync(path, contents, token);
        }

        public override Task WriteAllLinesAsync(String path, IEnumerable<String?>? contents, Encoding? encoding, CancellationToken token)
        {
            return FileSystem.File.WriteAllLinesAsync(path, contents, encoding, token);
        }
    }
}