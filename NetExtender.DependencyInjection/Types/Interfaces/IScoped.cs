// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;

namespace NetExtender.DependencyInjection.Interfaces
{
    public interface IMultiScoped<T> : IScoped<T>, IMultiDependencyService<T> where T : class
    {
    }

    public interface ISingleScoped<T> : IScoped<T>, ISingleDependencyService<T> where T : class
    {
    }

    public interface IReplaceScoped<T> : IScoped<T>, IReplaceDependencyService<T> where T : class
    {
    }

    public interface IMultiReplaceScoped<T> : IScoped<T>, IMultiReplaceDependencyService<T> where T : class
    {
    }

    public interface IScoped<T> : IDependencyService<T>, IScoped where T : class
    {
        ServiceLifetime IDependencyService.Lifetime
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ServiceLifetime.Scoped;
            }
        }
    }

    public interface IScoped : IDependencyService
    {
        ServiceLifetime IDependencyService.Lifetime
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return ServiceLifetime.Scoped;
            }
        }
    }
}