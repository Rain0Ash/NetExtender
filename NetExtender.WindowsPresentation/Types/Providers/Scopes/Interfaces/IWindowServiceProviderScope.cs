// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

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