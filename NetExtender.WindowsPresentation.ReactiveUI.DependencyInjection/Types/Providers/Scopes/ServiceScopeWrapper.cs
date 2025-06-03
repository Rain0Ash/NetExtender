// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using NetExtender.WindowsPresentation.Types.Scopes.Interfaces;

namespace NetExtender.WindowsPresentation.ReactiveUI.DependencyInjection.Types.Providers.Scopes
{
    internal sealed class ServiceScopeWrapper : Microsoft.Extensions.DependencyInjection.IServiceScope, WindowsPresentation.Types.Scopes.Interfaces.IServiceScope
    {
        private Microsoft.Extensions.DependencyInjection.IServiceScope Scope { get; }

        public IServiceProvider Provider
        {
            get
            {
                return Scope.ServiceProvider;
            }
        }
        
        public IServiceProvider ServiceProvider
        {
            get
            {
                return Scope.ServiceProvider;
            }
        }
        
        public ServiceScopeWrapper(Microsoft.Extensions.DependencyInjection.IServiceScope scope)
        {
            Scope = scope ?? throw new ArgumentNullException(nameof(scope));
        }
        
        public void Dispose()
        {
            Scope.Dispose();
        }
    }
    
    internal sealed class AsyncServiceScopeWrapper : Microsoft.Extensions.DependencyInjection.IServiceScope, IAsyncServiceScope
    {
        private readonly AsyncServiceScope Scope;
        
        public IServiceProvider Provider
        {
            get
            {
                return Scope.ServiceProvider;
            }
        }
        
        public IServiceProvider ServiceProvider
        {
            get
            {
                return Scope.ServiceProvider;
            }
        }
        
        public AsyncServiceScopeWrapper(ref AsyncServiceScope scope)
        {
            Scope = scope;
        }
        
        public void Dispose()
        {
            Scope.Dispose();
        }

        public async ValueTask DisposeAsync()
        {
            await Scope.DisposeAsync();
        }
    }
}