// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace NetExtender.DependencyInjection.Comparers
{
    public abstract class ServiceDescriptorEqualityComparer : IEqualityComparer<ServiceDescriptor?>
    {
        public static IEqualityComparer<ServiceDescriptor> Default { get; } = new DefaultServiceDescriptorEqualityComparer();
        public static IEqualityComparer<ServiceDescriptor> Implementation { get; } = new ImplementationServiceDescriptorEqualityComparer();
        public static IEqualityComparer<ServiceDescriptor> Service { get; } = new ServiceTypeServiceDescriptorEqualityComparer();
        
        public abstract Int32 GetHashCode(ServiceDescriptor value);
        public abstract Boolean Equals(ServiceDescriptor? x, ServiceDescriptor? y);
        
        private sealed class DefaultServiceDescriptorEqualityComparer : ServiceDescriptorEqualityComparer
        {
            public override Int32 GetHashCode(ServiceDescriptor value)
            {
                return HashCode.Combine(value.Lifetime, value.ServiceType, value.ImplementationType, value.ImplementationInstance, value.ImplementationFactory);
            }
            
            public override Boolean Equals(ServiceDescriptor? x, ServiceDescriptor? y)
            {
                if (ReferenceEquals(x, y))
                {
                    return true;
                }
                
                if (ReferenceEquals(x, null))
                {
                    return false;
                }
                
                if (ReferenceEquals(y, null))
                {
                    return false;
                }
                
                return x.Lifetime == y.Lifetime && x.ServiceType == y.ServiceType && x.ImplementationType == y.ImplementationType && Equals(x.ImplementationInstance, y.ImplementationInstance) && Equals(x.ImplementationFactory, y.ImplementationFactory);
            }
        }
        
        private sealed class ImplementationServiceDescriptorEqualityComparer : ServiceDescriptorEqualityComparer
        {
            public override Int32 GetHashCode(ServiceDescriptor value)
            {
                return HashCode.Combine(value.Lifetime, value.ServiceType, value.ImplementationType);
            }
            
            public override Boolean Equals(ServiceDescriptor? x, ServiceDescriptor? y)
            {
                if (ReferenceEquals(x, y))
                {
                    return true;
                }
                
                if (ReferenceEquals(x, null))
                {
                    return false;
                }
                
                if (ReferenceEquals(y, null))
                {
                    return false;
                }
                
                return x.Lifetime == y.Lifetime && x.ServiceType == y.ServiceType && x.ImplementationType == y.ImplementationType;
            }
        }
        
        private sealed class ServiceTypeServiceDescriptorEqualityComparer : ServiceDescriptorEqualityComparer
        {
            public override Int32 GetHashCode(ServiceDescriptor value)
            {
                return HashCode.Combine(value.ServiceType);
            }
            
            public override Boolean Equals(ServiceDescriptor? x, ServiceDescriptor? y)
            {
                if (ReferenceEquals(x, y))
                {
                    return true;
                }
                
                if (ReferenceEquals(x, null))
                {
                    return false;
                }
                
                if (ReferenceEquals(y, null))
                {
                    return false;
                }
                
                return x.ServiceType == y.ServiceType;
            }
        }
    }
}