// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using NetExtender.Cecil;
using NetExtender.Exceptions;
using NetExtender.Utilities.Core;

namespace NetExtender.DependencyInjection.Interfaces
{
    public interface IMultiDependencyService<T> : IDependencyService<T> where T : class
    {
    }

    public interface ISingleDependencyService<T> : IDependencyService<T> where T : class
    {
    }

    public interface IReplaceDependencyService<T> : IDependencyService<T> where T : class
    {
    }

    public interface IMultiReplaceDependencyService<T> : IDependencyService<T> where T : class
    {
    }

    public interface IDependencyService<T> : IDependencyService where T : class
    {
        static IDependencyService()
        {
            if (!typeof(T).IsInterface)
            {
                throw new TypeNotSupportedException<T>($"Type '{typeof(T).Name}' must be interface for {nameof(DependencyInjection)} registration.");
            }
        }
    }

    public interface IDependencyService
    {
        public static ImmutableDictionary<MonoCecilType, ServiceLifetime> Services { get; } = new Dictionary<MonoCecilType, ServiceLifetime>
        {
            { typeof(ITransient), ServiceLifetime.Transient },
            { typeof(IScoped), ServiceLifetime.Scoped },
            { typeof(ISingleton), ServiceLifetime.Singleton }
        }.ToImmutableDictionary();

        public ServiceLifetime Lifetime
        {
            get
            {
                MonoCecilType type = GetType();
                TypeSet interfaces = ReflectionUtilities.Inherit[typeof(IDependencyService)].Interfaces.Intersect(type.Interfaces).Intersect(Services.Keys);

                return interfaces.Count switch
                {
                    0 => throw new InvalidOperationException($"Can't find any of '{nameof(ITransient)}; {nameof(IScoped)}; {nameof(ISingleton)}' interface implementations for type '{type}'."),
                    1 => Services[interfaces.First],
                    _ => throw new AmbiguousMatchException($"Can't match {nameof(ServiceLifetime)} for type '{type}'.")
                };
            }
        }
    }
}