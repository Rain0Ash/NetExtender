// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Microsoft.Extensions.DependencyInjection;

namespace NetExtender.AspNetCore.Types.DependencyInjection.Interfaces
{
    public interface IServiceDependency
    {
        public ServiceLifetime Lifetime { get; }
    }
}