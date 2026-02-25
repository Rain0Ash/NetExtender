using System;
using Microsoft.Extensions.DependencyInjection;
using NetExtender.DependencyInjection;

namespace NetExtender.Utilities.Types
{
    public readonly struct ServiceDescriptorHandler : IEquatable<ServiceDescriptorHandler.Affiliation>, IEquatable<ServiceDescriptor>, IEquatable<ServiceDescriptorHandler>
    {
        public enum Affiliation : Byte
        {
            Unknown,
            Source,
            Destination,
            Handle,
            Any
        }

        public static implicit operator Affiliation(ServiceDescriptorHandler value)
        {
            return value.Source;
        }

        public static implicit operator ServiceDescriptor(ServiceDescriptorHandler value)
        {
            return value.Service;
        }

        public static implicit operator ServiceDescriptorHandler(ServiceDescriptor? value)
        {
            return value is not null ? new ServiceDescriptorHandler(value) : default;
        }

        public static Boolean operator ==(ServiceDescriptorHandler first, ServiceDescriptorHandler second)
        {
            return first.Equals(second);
        }

        public static Boolean operator !=(ServiceDescriptorHandler first, ServiceDescriptorHandler second)
        {
            return !(first == second);
        }

        public Affiliation Source { get; }
        public ServiceDescriptor Service { get; }

        public ServiceDescriptorHandler(ServiceDescriptor service)
            : this(service, Affiliation.Any)
        {
        }

        public ServiceDescriptorHandler(ServiceDescriptor service, Affiliation source)
        {
            Source = source;
            Service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public override Int32 GetHashCode()
        {
            return Service is not null ? ServiceDescriptorEqualityComparer.Default.GetHashCode(Service) : 0;
        }

        public override Boolean Equals(Object? other)
        {
            return other switch
            {
                null => Equals((ServiceDescriptor?) null),
                ServiceDescriptorHandler value => Equals(value),
                ServiceDescriptor value => Equals(value),
                Affiliation value => Equals(value),
                _ => false
            };
        }

        public Boolean Equals(Affiliation other)
        {
            return Source is Affiliation.Any || other is Affiliation.Any || Source == other;
        }

        public Boolean Equals(ServiceDescriptor? other)
        {
            return ServiceDescriptorEqualityComparer.Default.Equals(Service, other);
        }

        public Boolean Equals(ServiceDescriptorHandler other)
        {
            return Equals(other.Source) && Equals(other.Service);
        }

        public override String ToString()
        {
            return $"{{ {nameof(Affiliation)}: {Source}, {nameof(Service)}: {Service.GetString()} }}";
        }
    }
}