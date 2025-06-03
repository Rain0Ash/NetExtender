using System;
using System.IO;

namespace NetExtender.FileSystems.Interfaces
{
    internal interface IFileEntry : IFileSystemEntry, IFileInfo
    {
        /// <inheritdoc cref="FileInfo.Directory"/>
        public new IDirectoryEntry? Directory { get; }

        /// <inheritdoc cref="FileInfo.CopyTo(System.String)"/>
        public new IFileEntry CopyTo(String destination);

        /// <inheritdoc cref="FileInfo.CopyTo(System.String, System.Boolean)"/>
        public new IFileEntry CopyTo(String destination, Boolean overwrite);

        /// <inheritdoc cref="FileInfo.Replace(System.String, System.String?)"/>
        public new IFileEntry Replace(String destination, String? backup);

        /// <inheritdoc cref="FileInfo.Replace(System.String, System.String?, System.Boolean)"/>
        public new IFileEntry Replace(String destination, String? backup, Boolean suppress);
    }

    public interface IFileInfo : IFileSystemInfo, IEquatable<FileInfo>, IEquatable<IFileInfo>
    {
        public new FileInfo? Info { get; }
        
        /// <inheritdoc cref="FileInfo.Length"/>
        public Int64 Length { get; }

        /// <inheritdoc cref="FileInfo.IsReadOnly"/>
        public Boolean IsReadOnly { get; set; }

        /// <inheritdoc cref="FileInfo.DirectoryName"/>
        public String? DirectoryName { get; }

        /// <inheritdoc cref="FileInfo.Directory"/>
        public IDirectoryInfo? Directory { get; }

        /// <inheritdoc cref="FileInfo.Open(System.IO.FileMode)"/>
        public FileStream Open(FileMode mode);

        /// <inheritdoc cref="FileInfo.Open(System.IO.FileMode, System.IO.FileAccess)"/>
        public FileStream Open(FileMode mode, FileAccess access);

        /// <inheritdoc cref="FileInfo.Open(System.IO.FileMode, System.IO.FileAccess, System.IO.FileShare)"/>
        public FileStream Open(FileMode mode, FileAccess access, FileShare share);

        /// <inheritdoc cref="FileInfo.Open(System.IO.FileStreamOptions)"/>
        public FileStream Open(FileStreamOptions options);

        /// <inheritdoc cref="FileInfo.OpenRead()"/>
        public FileStream OpenRead();

        /// <inheritdoc cref="FileInfo.OpenWrite()"/>
        public FileStream OpenWrite();

        /// <inheritdoc cref="FileInfo.OpenText()"/>
        public StreamReader OpenText();

        /// <inheritdoc cref="FileInfo.Create()"/>
        public FileStream Create();

        /// <inheritdoc cref="FileInfo.AppendText()"/>
        public StreamWriter AppendText();

        /// <inheritdoc cref="FileInfo.Encrypt()"/>
        public Boolean Encrypt();

        /// <inheritdoc cref="FileInfo.Decrypt()"/>
        public Boolean Decrypt();

        /// <inheritdoc cref="FileInfo.MoveTo(System.String)"/>
        public void MoveTo(String destination);

        /// <inheritdoc cref="FileInfo.MoveTo(System.String, System.Boolean)"/>
        public void MoveTo(String destination, Boolean overwrite);

        /// <inheritdoc cref="FileInfo.CopyTo(System.String)"/>
        public IFileInfo CopyTo(String destination);

        /// <inheritdoc cref="FileInfo.CopyTo(System.String, System.Boolean)"/>
        public IFileInfo CopyTo(String destination, Boolean overwrite);

        /// <inheritdoc cref="FileInfo.Replace(System.String, System.String?)"/>
        public IFileInfo Replace(String destination, String? backup);

        /// <inheritdoc cref="FileInfo.Replace(System.String, System.String?, System.Boolean)"/>
        public IFileInfo Replace(String destination, String? backup, Boolean suppress);
    }
}