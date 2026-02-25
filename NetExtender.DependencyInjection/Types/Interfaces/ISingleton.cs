// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;

namespace NetExtender.DependencyInjection.Interfaces
{
    public interface IMultiSingleton<T> : ISingleton<T>, IMultiDependencyService<T> where T : class
    {
    }

    public interface ISingleSingleton<T> : ISingleton<T>, ISingleDependencyService<T> where T : class
    {
    }

    public interface IReplaceSingleton<T> : ISingleton<T>, IReplaceDependencyService<T> where T : class
    {
    }

    public interface IMultiReplaceSingleton<T> : ISingleton<T>, IMultiReplaceDependencyService<T> where T : class
    {
    }

    public interface ISingleton<T> : IDependencyService<T>, ISingleton where T : class
    {
        ServiceLifetime IDependencyService.Lifetime
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ServiceLifetime.Singleton;
            }
        }
    }

    public interface ISingleton : IDependencyService
    {
        ServiceLifetime IDependencyService.Lifetime
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ServiceLifetime.Singleton;
            }
        }
    }
}