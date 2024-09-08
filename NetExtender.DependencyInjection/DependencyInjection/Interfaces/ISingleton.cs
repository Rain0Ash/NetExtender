// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Microsoft.Extensions.DependencyInjection;

namespace NetExtender.DependencyInjection.Interfaces
{
    public interface ISingleton<T> : IDependencyService<T>, ISingleton where T : class
    {
    }
    
    public interface ISingleton : IDependencyService
    {
        public new ServiceLifetime Lifetime
        {
            get
            {
                return ServiceLifetime.Singleton;
            }
        }
    }
}