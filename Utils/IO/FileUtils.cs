// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NetExtender.IO.Shortcut;
using NetExtender.Utils.Types;

namespace NetExtender.Utils.IO
{
    public static class FileUtils
    {
        public static Boolean HasAttribute(String path, FileAttributes attributes)
        {
            return HasAttribute(path, attributes, out _);
        }

        public static Boolean HasAttribute(String path, FileAttributes attributes, out FileAttributes current)
        {
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

        public static FileStream WaitForFile(String path, FileMode mode, FileAccess access, FileShare share)
        {
            for (Int32 tries = 0; tries < 10; tries++)
            {
                FileStream stream = null;
                try
                {
                    stream = new FileStream(path, mode, access, share);
                    return stream;
                }
                catch (IOException)
                {
                    stream?.Dispose();
                    Thread.Sleep(250);
                }
            }

            return null;
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
            return PathUtils.IsExistAsFile(path) ? File.ReadAllBytes(path) : null;
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

        public static String ReadFileText(String path)
        {
            return PathUtils.IsExistAsFile(path) ? File.ReadAllText(path) : null;
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

        public static String[] ReadFileLines(String path)
        {
            return PathUtils.IsExistAsFile(path) ? File.ReadAllLines(path) : null;
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

        public static String GetFileContents(String file, Int32 timeout = 5000)
        {
            StreamReader reader = null;
            Int64 timestamp = Environment.TickCount64;
            try
            {
                if (!PathUtils.IsExistAsFile(file))
                {
                    return String.Empty;
                }

                Boolean opened = false;
                while (!opened)
                {
                    if (Environment.TickCount64 - timestamp >= timeout)
                    {
                        throw new TimeoutException("File opening timed out");
                    }

                    reader = File.OpenText(file);
                    opened = true;
                }

                String contents = reader.ReadToEnd();
                reader.Close();
                return contents;
            }
            finally
            {
                reader?.Close();
                reader?.Dispose();
            }
        }

        public static Task<FileInfo> SafeCreateFileAsync(String path, Byte[] data, Boolean overwrite = false, Int32 buffer = BufferUtils.DefaultBuffer)
        {
            return SafeCreateFileAsync(path, data, overwrite, buffer, CancellationToken.None);
        }

        public static Task<FileInfo> SafeCreateFileAsync(String path, Byte[] data, Boolean overwrite, Int32 buffer, CancellationToken token)
        {
            using Stream stream = data.ToStream();
            return SafeCreateFileAsync(path, stream, overwrite, buffer, token);
        }

        public static Task<FileInfo> SafeCreateFileAsync(String path, Stream stream, Boolean overwrite = false, Int32 buffer = BufferUtils.DefaultBuffer)
        {
            return SafeCreateFileAsync(path, stream, overwrite, buffer, CancellationToken.None);
        }

        public static async Task<FileInfo> SafeCreateFileAsync(String path, Stream stream, Boolean overwrite, Int32 buffer, CancellationToken token)
        {
            if (path is null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            if (stream is null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            if (!PathUtils.IsValidFilePath(path))
            {
                throw new ArgumentException(nameof(path));
            }

            Boolean create = !PathUtils.IsExistAsFile(path);
            if (!overwrite && !create)
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

                if (!create)
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

        public static Boolean CreateShortcut(String file, String dir)
        {
            if (!PathUtils.IsExistAsFile(file))
            {
                return false;
            }

            Shortcut shortcut = new Shortcut(Path.GetFileNameWithoutExtension(file) + ".lnk")
            {
                TargetPath = file,
                WorkingDirectory = Path.GetDirectoryName(file),
                SaveDirectory = dir
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
        /// <param name="file">The file to add the suffix to.</param>
        /// <param name="suffix">The suffix to add to the file.</param>
        public static void AddSuffix(FileInfo file, String suffix)
        {
            if (file is null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            if (suffix is null)
            {
                throw new ArgumentNullException(nameof(suffix));
            }

            if (suffix == String.Empty)
            {
                return;
            }
            
            String name = file.Name + suffix;

            String dir = Path.GetDirectoryName(file.FullName);

            if (String.IsNullOrEmpty(dir))
            {
                throw new IOException("Directory name is empty or null");
            }

            String path = Path.Combine(dir, name);
            file.MoveTo(path);
        }
        
        /// <summary>
        /// Renames the specified file (removes the specified suffix from the end of the name, if present).
        /// </summary>
        /// <param name="file">The file to remove the suffix from.</param>
        /// <param name="suffix">The suffix to remove from the file.</param>
        public static void RemoveSuffix(FileInfo file, String suffix)
        {
            if (file is null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            if (suffix is null)
            {
                throw new ArgumentNullException(nameof(suffix));
            }

            if (suffix == String.Empty)
            {
                return;
            }
            
            if (!file.Name.EndsWith(suffix))
            {
                return;
            }

            String name = file.Name.Substring(0, file.Name.Length - suffix.Length);

            String dir = Path.GetDirectoryName(file.FullName);

            if (String.IsNullOrEmpty(dir))
            {
                throw new IOException("Directory name is empty or null");
            }
                
            String path = Path.Combine(dir, name);
            file.MoveTo(path);
        }
    }
}