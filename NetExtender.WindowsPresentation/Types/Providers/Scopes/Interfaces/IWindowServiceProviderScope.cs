using System;
using NetExtender.WindowsPresentation.Types.Interfaces;

namespace NetExtender.WindowsPresentation.Types.Scopes.Interfaces
{
    public interface IWindowServiceProviderScope : IWindowServiceProvider, IDisposable
    {
        public IWindowServiceProvider Provider { get; }
    }
    
    public interface IAsyncWindowServiceProviderScope : IWindowServiceProviderScope, IAsyncDisposable
    {
    }
}