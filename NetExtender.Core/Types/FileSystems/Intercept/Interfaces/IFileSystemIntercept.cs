// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.FileSystems.Interfaces
{
    //TODO: all interfaces
    [Obsolete($"Use IFileSystemIntercept as specified interface {nameof(IInterceptPathHandler)}; {nameof(IInterceptFileHandler)}; {nameof(IInterceptDirectoryHandler)}.")]
    public interface IFileSystemIntercept : IFileSystemHandler, IInterceptPathHandler, IInterceptFileHandler, IInterceptDirectoryHandler, IInterceptDriveHandler, IInterceptFileSystem
    {
    }
}