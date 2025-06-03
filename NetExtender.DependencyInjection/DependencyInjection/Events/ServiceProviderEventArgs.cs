// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.DependencyInjection.Events
{
    public class ServiceProviderEventArgs : EventArgs
    {
        private readonly Func<IServiceProvider> _factory;
        public IServiceProvider Provider
        {
            get
            {
                return _factory();
            }
        }
        
        public ServiceProviderEventArgs(IServiceProvider provider)
        {
            if (provider is null)
            {
                throw new ArgumentNullException(nameof(provider));
            }
            
            _factory = () => provider;
        }
        
        public ServiceProviderEventArgs(Func<IServiceProvider> provider)
        {
            _factory = provider ?? throw new ArgumentNullException(nameof(provider));
        }
    }
}