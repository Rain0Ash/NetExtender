// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using Microsoft.Extensions.DependencyInjection;

namespace NetExtender.DependencyInjection.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public sealed class ScopedAttribute : ServiceDependencyAttribute
    {
        private new const ServiceLifetime Lifetime = ServiceLifetime.Scoped;
        
        public ScopedAttribute()
            : base(Lifetime)
        {
        }
        
        public ScopedAttribute(Object? key)
            : base(key, Lifetime)
        {
        }
        
        public ScopedAttribute(Type @interface)
            : base(@interface ?? throw new ArgumentNullException(nameof(@interface)), Lifetime)
        {
        }
        
        public ScopedAttribute(Type @interface, Object? key)
            : base(@interface ?? throw new ArgumentNullException(nameof(@interface)), key, Lifetime)
        {
        }
    }
}