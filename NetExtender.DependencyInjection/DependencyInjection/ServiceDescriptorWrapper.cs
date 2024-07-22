// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using NetExtender.DependencyInjection.Interfaces;

namespace NetExtender.DependencyInjection
{
    public sealed class ServiceDescriptorWrapper : IServiceDependency
    {
        [return: NotNullIfNotNull("wrapper")]
        public static implicit operator ServiceDescriptor?(ServiceDescriptorWrapper? wrapper)
        {
            return wrapper?.Service;
        }

        private ServiceDescriptor Service { get; }

        public Func<IServiceProvider, Object>? ImplementationFactory
        {
            get
            {
                return Service.ImplementationFactory;
            }
        }

        public Object? ImplementationInstance
        {
            get
            {
                return Service.ImplementationInstance;
            }
        }

        public Type? ImplementationType
        {
            get
            {
                return Service.ImplementationType;
            }
        }

        public Type ServiceType
        {
            get
            {
                return Service.ServiceType;
            }
        }

        public ServiceLifetime Lifetime
        {
            get
            {
                return Service.Lifetime;
            }
        }

        public ServiceDescriptorWrapper(ServiceDescriptor service)
        {
            Service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public override Int32 GetHashCode()
        {
            return Service.GetHashCode();
        }

        public override Boolean Equals(Object? other)
        {
            return Service.Equals(other);
        }

        public override String ToString()
        {
            return Service.ToString();
        }
    }
}