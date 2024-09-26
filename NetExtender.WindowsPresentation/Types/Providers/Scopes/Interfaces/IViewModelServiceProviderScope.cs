using System;
using NetExtender.WindowsPresentation.Types.Interfaces;

namespace NetExtender.WindowsPresentation.Types.Scopes.Interfaces
{
    public interface IViewModelServiceProviderScope : IViewModelServiceProvider, IDisposable
    {
        public IViewModelServiceProvider Provider { get; }
    }
    
    public interface IAsyncViewModelServiceProviderScope : IViewModelServiceProviderScope, IAsyncDisposable
    {
    }
}