// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;

namespace NetExtender.DependencyInjection.Interfaces
{
    public interface IMultiTransient<T> : ITransient<T>, IMultiDependencyService<T> where T : class
    {
    }

    public interface ISingleTransient<T> : ITransient<T> where T : class
    {
    }

    public interface IReplaceTransient<T> : ITransient<T> where T : class
    {
    }

    public interface IMultiReplaceTransient<T> : ITransient<T> where T : class
    {
    }

    public interface ITransient<T> : IDependencyService<T>, ITransient where T : class
    {
        ServiceLifetime IDependencyService.Lifetime
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ServiceLifetime.Transient;
            }
        }
    }

    public interface ITransient : IDependencyService
    {
        ServiceLifetime IDependencyService.Lifetime
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ServiceLifetime.Transient;
            }
        }
    }
}