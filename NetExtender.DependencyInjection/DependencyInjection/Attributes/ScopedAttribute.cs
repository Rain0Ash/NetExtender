using System;
using Microsoft.Extensions.DependencyInjection;

namespace NetExtender.DependencyInjection.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public sealed class ScopedAttribute : ServiceDependencyAttribute
    {
        public ScopedAttribute()
            : base(ServiceLifetime.Scoped)
        {
        }
        
        public ScopedAttribute(Type @interface)
            : base(@interface ?? throw new ArgumentNullException(nameof(@interface)), ServiceLifetime.Scoped)
        {
        }
    }
}