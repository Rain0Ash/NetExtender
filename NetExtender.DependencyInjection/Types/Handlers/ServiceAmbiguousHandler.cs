using System;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using NetExtender.Exceptions;

namespace NetExtender.Utilities.Types
{
    public delegate ServiceDescriptor? ServiceAmbiguousDelegate(ReadOnlySpan<ServiceDescriptorHandler> services);

    public enum ServiceAmbiguousHandlerType
    {
        Ignore,
        Throw,
        New,
        Custom
    }

    public readonly struct ServiceAmbiguousHandler : IStruct<ServiceAmbiguousHandler>
    {
        public static implicit operator ServiceAmbiguousHandler(ServiceAmbiguousHandlerType value)
        {
            // ReSharper disable once SwitchExpressionHandlesSomeKnownEnumValuesWithExceptionInDefault
            return value switch
            {
                ServiceAmbiguousHandlerType.Ignore => Ignore,
                ServiceAmbiguousHandlerType.Throw => Throw,
                ServiceAmbiguousHandlerType.New => New,
                _ => throw new EnumUndefinedOrNotSupportedException<ServiceAmbiguousHandlerType>(value, nameof(value), null)
            };
        }

        public static implicit operator ServiceAmbiguousHandler(ServiceAmbiguousDelegate? value)
        {
            return new ServiceAmbiguousHandler(value);
        }

        public static ServiceAmbiguousHandler Ignore { get; } = new ServiceAmbiguousHandler(null);
        public static ServiceAmbiguousHandler Throw { get; } = new ServiceAmbiguousHandler(static _ => null) { Type = ServiceAmbiguousHandlerType.Throw };
        public static ServiceAmbiguousHandler New { get; } = new ServiceAmbiguousHandler(static _ => null) { Type = ServiceAmbiguousHandlerType.New };

        private ServiceAmbiguousDelegate? Handler { get; }
        public ServiceAmbiguousHandlerType Type { get; private init; }

        public Boolean IsEmpty
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Handler is null;
            }
        }

        private ServiceAmbiguousHandler(ServiceAmbiguousDelegate? handler)
        {
            Handler = handler;
            Type = Handler is null ? ServiceAmbiguousHandlerType.Ignore : ServiceAmbiguousHandlerType.Custom;
        }

        public ServiceDescriptor? Invoke(ReadOnlySpan<ServiceDescriptorHandler> services)
        {
            if (Handler is null)
            {
                throw new InvalidOperationException(nameof(IsEmpty));
            }

            return Handler.Invoke(services);
        }
    }
}