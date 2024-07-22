// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Microsoft.Extensions.DependencyInjection;

namespace NetExtender.DependencyInjection.Interfaces
{
    public interface ITransient<T> : IServiceDependency<T>, ITransient where T : class
    {
    }

    public interface ITransient : IServiceDependency
    {
        public new ServiceLifetime Lifetime
        {
            get
            {
                return ServiceLifetime.Transient;
            }
        }
    }
}