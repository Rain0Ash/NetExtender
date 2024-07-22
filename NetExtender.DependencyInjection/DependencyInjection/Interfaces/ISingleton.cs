// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Microsoft.Extensions.DependencyInjection;

namespace NetExtender.DependencyInjection.Interfaces
{
    public interface ISingleton<T> : IServiceDependency<T>, ISingleton where T : class
    {
    }
    
    public interface ISingleton : IServiceDependency
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