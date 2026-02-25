using System;
using System.Runtime.Serialization;
using NetExtender.Exceptions.Interfaces;
using NetExtender.Utilities.Core;

namespace NetExtender.Exceptions
{
    [Serializable]
    public sealed class ArgumentNonZeroLowerBoundException<TSR> : ArgumentNonZeroLowerBoundException, ISRException<TSR, ArgumentNonZeroLowerBoundException<TSR>.Arg_NonZeroLowerBound>
    {
        public ArgumentNonZeroLowerBoundException()
            : base(ISRExceptionIdentifier<Arg_NonZeroLowerBound>.Instance.Resource)
        {
        }

        public ArgumentNonZeroLowerBoundException(Exception? exception)
            : base(ISRExceptionIdentifier<Arg_NonZeroLowerBound>.Instance.Resource, exception)
        {
        }

        public ArgumentNonZeroLowerBoundException(String? parameter)
            : base(ISRExceptionIdentifier<Arg_NonZeroLowerBound>.Instance.Resource, parameter)
        {
        }

        public ArgumentNonZeroLowerBoundException(String? parameter, Exception? exception)
            : base(ISRExceptionIdentifier<Arg_NonZeroLowerBound>.Instance.Resource, parameter, exception)
        {
        }

#if NET8_0_OR_GREATER
        [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId="SYSLIB0051", UrlFormat="https://aka.ms/dotnet-warnings/{0}")]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
#endif
        private ArgumentNonZeroLowerBoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        [ReflectionNaming]
        internal class Arg_NonZeroLowerBound : ISRExceptionIdentifier<TSR, Arg_NonZeroLowerBound>
        {
        }
    }

    [Serializable]
    public class ArgumentNonZeroLowerBoundException : ArgumentException
    {
        public ArgumentNonZeroLowerBoundException()
        {
        }

        public ArgumentNonZeroLowerBoundException(String? message)
            : base(message)
        {
        }

        public ArgumentNonZeroLowerBoundException(String? message, Exception? exception)
            : base(message, exception)
        {
        }

        public ArgumentNonZeroLowerBoundException(String? message, String? parameter)
            : base(message, parameter)
        {
        }

        public ArgumentNonZeroLowerBoundException(String? message, String? parameter, Exception? exception)
            : base(message, parameter, exception)
        {
        }

#if NET8_0_OR_GREATER
        [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId="SYSLIB0051", UrlFormat="https://aka.ms/dotnet-warnings/{0}")]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
#endif
        protected ArgumentNonZeroLowerBoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}