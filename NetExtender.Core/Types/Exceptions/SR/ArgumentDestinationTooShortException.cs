using System;
using System.Runtime.Serialization;
using NetExtender.Types.Exceptions.Interfaces;
using NetExtender.Utilities.Core;

namespace NetExtender.Types.Exceptions
{
    [Serializable]
    public sealed class ArgumentDestinationTooShortException<TSR> : ArgumentDestinationTooShortException, ISRException<TSR, ArgumentDestinationTooShortException<TSR>.Argument_DestinationTooShort>
    {
        public ArgumentDestinationTooShortException()
            : base(ISRExceptionIdentifier<Argument_DestinationTooShort>.Instance.Resource)
        {
        }

        public ArgumentDestinationTooShortException(Exception? exception)
            : base(ISRExceptionIdentifier<Argument_DestinationTooShort>.Instance.Resource, exception)
        {
        }

        public ArgumentDestinationTooShortException(String? parameter)
            : base(ISRExceptionIdentifier<Argument_DestinationTooShort>.Instance.Resource, parameter)
        {
        }

        public ArgumentDestinationTooShortException(String? parameter, Exception? exception)
            : base(ISRExceptionIdentifier<Argument_DestinationTooShort>.Instance.Resource, parameter, exception)
        {
        }

#if NET8_0_OR_GREATER
        [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId="SYSLIB0051", UrlFormat="https://aka.ms/dotnet-warnings/{0}")]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
#endif
        private ArgumentDestinationTooShortException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        [ReflectionNaming]
        internal class Argument_DestinationTooShort : ISRExceptionIdentifier<TSR, Argument_DestinationTooShort>
        {
        }
    }

    [Serializable]
    public class ArgumentDestinationTooShortException : ArgumentException
    {
        public ArgumentDestinationTooShortException()
        {
        }

        public ArgumentDestinationTooShortException(String? message)
            : base(message)
        {
        }

        public ArgumentDestinationTooShortException(String? message, Exception? exception)
            : base(message, exception)
        {
        }

        public ArgumentDestinationTooShortException(String? message, String? parameter)
            : base(message, parameter)
        {
        }

        public ArgumentDestinationTooShortException(String? message, String? parameter, Exception? exception)
            : base(message, parameter, exception)
        {
        }

#if NET8_0_OR_GREATER
        [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId="SYSLIB0051", UrlFormat="https://aka.ms/dotnet-warnings/{0}")]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
#endif
        protected ArgumentDestinationTooShortException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}