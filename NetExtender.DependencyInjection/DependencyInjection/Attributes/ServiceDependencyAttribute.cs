using System;
using Microsoft.Extensions.DependencyInjection;
using NetExtender.Types.Exceptions;

namespace NetExtender.DependencyInjection.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class ServiceDependencyAttribute : Attribute
    {
        public Type? Interface { get; }
        public ServiceLifetime Lifetime { get; }
        
        public ServiceDependencyAttribute(ServiceLifetime lifetime)
        {
            Lifetime = lifetime;
        }
        
        public ServiceDependencyAttribute(Type @interface, ServiceLifetime lifetime)
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
            Lifetime = lifetime;
        }
    }
}