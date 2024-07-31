// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using NetExtender.Types.Exceptions;
using NetExtender.Utilities.Core;

namespace NetExtender.DependencyInjection.Interfaces
{
    public interface IServiceDependency<T> : IServiceDependency where T : class
    {
        static IServiceDependency()
        {
            if (!typeof(T).IsInterface)
            {
                throw new TypeNotSupportedException<T>($"Type '{typeof(T).Name}' must be interface for {nameof(DependencyInjection)} registration.");
            }
        }
    }
    
    public interface IServiceDependency
    {
        public static ImmutableDictionary<Type, ServiceLifetime> Services { get; } = new Dictionary<Type, ServiceLifetime>
        {
            { typeof(ITransient), ServiceLifetime.Transient },
            { typeof(IScoped), ServiceLifetime.Scoped },
            { typeof(ISingleton), ServiceLifetime.Singleton }
        }.ToImmutableDictionary();
        
        public ServiceLifetime Lifetime
        {
            get
            {
                Type type = GetType();
                ImmutableHashSet<Type> interfaces = ReflectionUtilities.Inherit[typeof(IServiceDependency)].Interfaces.Intersect(type.GetInterfaces()).Intersect(Services.Keys);
                
                return interfaces.Count switch
                {
                    0 => throw new InvalidOperationException($"Can't find any of '{typeof(ITransient)}; {typeof(IScoped)}; {typeof(ISingleton)}' interface implementations for type '{type}'."),
                    1 => Services[interfaces.First()],
                    _ => throw new AmbiguousMatchException($"Can't match {nameof(ServiceLifetime)} for type '{type}'.")
                };
            }
        }
    }
}