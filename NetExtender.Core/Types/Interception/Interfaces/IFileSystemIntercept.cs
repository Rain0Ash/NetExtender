using System;
using NetExtender.IO.Interfaces;

namespace NetExtender.Types.Interception.Interfaces
{
#pragma warning disable CS0618
    internal interface IUnsafeFileSystemIntercept : IFileSystemIntercept, IUnsafeFileSystemHandler
    {
    }
#pragma warning restore CS0618
    
    [Obsolete($"Use {nameof(IFileSystemIntercept)} as specified interface {nameof(IInterceptPathHandler)}; {nameof(IInterceptFileHandler)}; {nameof(IInterceptDirectoryHandler)}.")]
    public interface IFileSystemIntercept : IFileSystemHandler, IInterceptPathHandler, IInterceptFileHandler, IInterceptDirectoryHandler
    {
    }
}