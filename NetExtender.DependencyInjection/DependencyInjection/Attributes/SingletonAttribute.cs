using System;
using Microsoft.Extensions.DependencyInjection;

namespace NetExtender.DependencyInjection.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public sealed class SingletonAttribute : ServiceDependencyAttribute
    {
        public SingletonAttribute()
            : base(ServiceLifetime.Singleton)
        {
        }
        
        public SingletonAttribute(Type @interface)
            : base(@interface ?? throw new ArgumentNullException(nameof(@interface)), ServiceLifetime.Singleton)
        {
        }
    }
}