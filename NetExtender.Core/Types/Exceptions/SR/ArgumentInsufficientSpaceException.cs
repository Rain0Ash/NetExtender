using System;
using System.Runtime.Serialization;
using NetExtender.Types.Exceptions.Interfaces;
using NetExtender.Utilities.Core;

namespace NetExtender.Types.Exceptions
{
    [Serializable]
    public sealed class ArgumentInsufficientSpaceException<TSR> : ArgumentInsufficientSpaceException, ISRException<TSR, ArgumentInsufficientSpaceException<TSR>.Arg_InsufficientSpace>
    {
        public ArgumentInsufficientSpaceException()
            : base(ISRExceptionIdentifier<Arg_InsufficientSpace>.Instance.Resource)
        {
        }

        public ArgumentInsufficientSpaceException(Exception? exception)
            : base(ISRExceptionIdentifier<Arg_InsufficientSpace>.Instance.Resource, exception)
        {
        }

        public ArgumentInsufficientSpaceException(String? parameter)
            : base(ISRExceptionIdentifier<Arg_InsufficientSpace>.Instance.Resource, parameter)
        {
        }

        public ArgumentInsufficientSpaceException(String? parameter, Exception? exception)
            : base(ISRExceptionIdentifier<Arg_InsufficientSpace>.Instance.Resource, parameter, exception)
        {
        }

#if NET8_0_OR_GREATER
        [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId="SYSLIB0051", UrlFormat="https://aka.ms/dotnet-warnings/{0}")]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
#endif
        private ArgumentInsufficientSpaceException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        [ReflectionNaming]
        internal class Arg_InsufficientSpace : ISRExceptionIdentifier<TSR, Arg_InsufficientSpace>
        {
        }
    }

    [Serializable]
    public class ArgumentInsufficientSpaceException : ArgumentException
    {
        public ArgumentInsufficientSpaceException()
        {
        }

        public ArgumentInsufficientSpaceException(String? message)
            : base(message)
        {
        }

        public ArgumentInsufficientSpaceException(String? message, Exception? exception)
            : base(message, exception)
        {
        }

        public ArgumentInsufficientSpaceException(String? message, String? parameter)
            : base(message, parameter)
        {
        }

        public ArgumentInsufficientSpaceException(String? message, String? parameter, Exception? exception)
            : base(message, parameter, exception)
        {
        }

#if NET8_0_OR_GREATER
        [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId="SYSLIB0051", UrlFormat="https://aka.ms/dotnet-warnings/{0}")]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
#endif
        protected ArgumentInsufficientSpaceException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}