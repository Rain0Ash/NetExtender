using System;

namespace NetExtender.WindowsPresentation.Types.Scopes.Interfaces
{
    internal interface IServiceScope : IDisposable
    {
        public IServiceProvider Provider { get; }
    }
    
    internal interface IAsyncServiceScope : IServiceScope, IAsyncDisposable
    {
    }
}