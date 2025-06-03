// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using NetExtender.Interfaces.Notify;

namespace NetExtender.FileSystems.Interfaces
{
    public interface ILinkHandler : INotifyProperty, IDisposable
    {
        public Object SyncRoot { get; }
        public Boolean IsSynchronized { get; }
        public Guid Id { get; }
        public String? Name { get; }
        public DateTime CreationTime { get; }
        public DateTime CreationTimeUtc { get; }
        public Boolean IsReal { get; }
        public StringComparer Comparer { get; }
        public Boolean IsCaseSensitive { get; }

        /// <inheritdoc cref="System.IO.File.CreateSymbolicLink(System.String, System.String)" />
        public IFileSystemInfo CreateSymbolicLink(String path, String target);

        /// <inheritdoc cref="System.IO.File.ResolveLinkTarget(System.String, System.Boolean)" />
        public IFileSystemInfo? ResolveLinkTarget(String path, Boolean target);

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