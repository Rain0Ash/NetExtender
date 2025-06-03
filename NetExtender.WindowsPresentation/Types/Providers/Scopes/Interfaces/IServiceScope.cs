// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

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