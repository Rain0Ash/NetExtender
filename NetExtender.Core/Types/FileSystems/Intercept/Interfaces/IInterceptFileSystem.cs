// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.FileSystems.Interfaces
{
    public interface IInterceptFileSystem : IFileSystem
    {
        [Obsolete($"Use {nameof(IFileSystemIntercept)} as specified interface {nameof(IInterceptPathHandler)}; {nameof(IInterceptFileHandler)}; {nameof(IInterceptDirectoryHandler)}.")]
        public new IFileSystemIntercept FileSystem { get; }
        public new IInterceptPathHandler Path { get; }
        public new IInterceptFileHandler File { get; }
        public new IInterceptDirectoryHandler Directory { get; }
    }
}