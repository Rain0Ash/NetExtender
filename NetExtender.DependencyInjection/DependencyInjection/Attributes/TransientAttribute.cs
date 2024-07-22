using System;
using Microsoft.Extensions.DependencyInjection;

namespace NetExtender.DependencyInjection.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public sealed class TransientAttribute : ServiceDependencyAttribute
    {
        public TransientAttribute()
            : base(ServiceLifetime.Transient)
        {
        }
        
        public TransientAttribute(Type @interface)
            : base(@interface ?? throw new ArgumentNullException(nameof(@interface)), ServiceLifetime.Transient)
        {
        }
    }
}