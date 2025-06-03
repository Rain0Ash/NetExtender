// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using NetExtender.FileSystems.Interfaces;

namespace NetExtender.FileSystems
{
    public partial class FileSystemHandler
    {
        IFileSystemInfo ILinkHandler.CreateSymbolicLink(String path, String target)
        {
            return CreateSymbolicLink(path, target, FileSystemHandlerType.Link);
        }

        IFileSystemInfo? ILinkHandler.ResolveLinkTarget(String path, Boolean target)
        {
            return ResolveLinkTarget(path, target, FileSystemHandlerType.Link);
        }

        Boolean ILinkHandler.Exists([NotNullWhen(true)] String? path)
        {
            return Exists(path, FileSystemHandlerType.Link);
        }

        FileAttributes ILinkHandler.GetAttributes(String path)
        {
            return GetAttributes(path, FileSystemHandlerType.Link);
        }

        void ILinkHandler.SetAttributes(String path, FileAttributes attributes)
        {
            SetAttributes(path, attributes, FileSystemHandlerType.Link);
        }

        DateTime ILinkHandler.GetCreationTime(String path)
        {
            return GetCreationTime(path, FileSystemHandlerType.Link);
        }

        DateTime ILinkHandler.GetCreationTimeUtc(String path)
        {
            return GetCreationTimeUtc(path, FileSystemHandlerType.Link);
        }

        DateTime ILinkHandler.GetLastAccessTime(String path)
        {
            return GetLastAccessTime(path, FileSystemHandlerType.Link);
        }

        DateTime ILinkHandler.GetLastAccessTimeUtc(String path)
        {
            return GetLastAccessTimeUtc(path, FileSystemHandlerType.Link);
        }

        DateTime ILinkHandler.GetLastWriteTime(String path)
        {
            return GetLastWriteTime(path, FileSystemHandlerType.Link);
        }

        DateTime ILinkHandler.GetLastWriteTimeUtc(String path)
        {
            return GetLastWriteTimeUtc(path, FileSystemHandlerType.Link);
        }
        
        void ILinkHandler.SetCreationTime(String path, DateTime time)
        {
            SetCreationTime(path, time, FileSystemHandlerType.Link);
        }

        void ILinkHandler.SetCreationTimeUtc(String path, DateTime time)
        {
            SetCreationTimeUtc(path, time, FileSystemHandlerType.Link);
        }

        void ILinkHandler.SetLastAccessTime(String path, DateTime time)
        {
            SetLastAccessTime(path, time, FileSystemHandlerType.Link);
        }

        void ILinkHandler.SetLastAccessTimeUtc(String path, DateTime time)
        {
            SetLastAccessTimeUtc(path, time, FileSystemHandlerType.Link);
        }

        void ILinkHandler.SetLastWriteTime(String path, DateTime time)
        {
            SetLastWriteTime(path, time, FileSystemHandlerType.Link);
        }

        void ILinkHandler.SetLastWriteTimeUtc(String path, DateTime time)
        {
            SetLastWriteTimeUtc(path, time, FileSystemHandlerType.Link);
        }

        void ILinkHandler.Move(String source, String destination)
        {
            Move(source, destination, FileSystemHandlerType.Link);
        }

        void ILinkHandler.Move(String source, String destination, Boolean overwrite)
        {
            Move(source, destination, overwrite, FileSystemHandlerType.Link);
        }

        void ILinkHandler.Copy(String source, String destination)
        {
            Copy(source, destination, FileSystemHandlerType.Link);
        }

        void ILinkHandler.Copy(String source, String destination, Boolean overwrite)
        {
            Copy(source, destination, overwrite, FileSystemHandlerType.Link);
        }

        void ILinkHandler.Replace(String source, String destination, String? backup)
        {
            Replace(source, destination, backup, FileSystemHandlerType.Link);
        }

        void ILinkHandler.Replace(String source, String destination, String? backup, Boolean suppress)
        {
            Replace(source, destination, backup, suppress, FileSystemHandlerType.Link);
        }

        void ILinkHandler.Delete(String path)
        {
            Delete(path, FileSystemHandlerType.Link);
        }
    }
}