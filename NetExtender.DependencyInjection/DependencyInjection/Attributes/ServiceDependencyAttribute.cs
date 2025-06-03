// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using Microsoft.Extensions.DependencyInjection;
using NetExtender.Types.Exceptions;

namespace NetExtender.DependencyInjection.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class ServiceDependencyAttribute : Attribute
    {
        public Type? Interface { get; }
        public Object? Key { get; }
        public ServiceLifetime Lifetime { get; }
        
        public ServiceDependencyAttribute(ServiceLifetime lifetime)
            : this((Object?) null, lifetime)
        {
        }
        
        public ServiceDependencyAttribute(Object? key, ServiceLifetime lifetime)
        {
            Key = key;
            Lifetime = lifetime;
        }

        public ServiceDependencyAttribute(Type @interface, ServiceLifetime lifetime)
            : this(@interface, null, lifetime)
        {
        }

        public ServiceDependencyAttribute(Type @interface, Object? key, ServiceLifetime lifetime)
        {
            if (@interface is null)
            {
                throw new ArgumentNullException(nameof(@interface));
            }
            
            if (!@interface.IsInterface)
            {
                throw new TypeNotSupportedException(@interface, $"Type '{@interface.Name}' must be interface for {nameof(DependencyInjection)} registration.");
            }
            
            Interface = @interface;
            Key = key;
            Lifetime = lifetime;
        }
    }
}