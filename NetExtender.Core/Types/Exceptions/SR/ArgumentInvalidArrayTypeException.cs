using System;
using System.Runtime.Serialization;
using NetExtender.Types.Exceptions.Interfaces;
using NetExtender.Utilities.Core;

namespace NetExtender.Types.Exceptions
{
    [Serializable]
    public sealed class ArgumentInvalidArrayTypeException<TSR> : ArgumentInvalidArrayTypeException, ISRException<TSR, ArgumentInvalidArrayTypeException<TSR>.Argument_InvalidArrayType>
    {
        public ArgumentInvalidArrayTypeException()
            : base(ISRExceptionIdentifier<Argument_InvalidArrayType>.Instance.Resource)
        {
        }

        public ArgumentInvalidArrayTypeException(Exception? exception)
            : base(ISRExceptionIdentifier<Argument_InvalidArrayType>.Instance.Resource, exception)
        {
        }

        public ArgumentInvalidArrayTypeException(String? parameter)
            : base(ISRExceptionIdentifier<Argument_InvalidArrayType>.Instance.Resource, parameter)
        {
        }

        public ArgumentInvalidArrayTypeException(String? parameter, Exception? exception)
            : base(ISRExceptionIdentifier<Argument_InvalidArrayType>.Instance.Resource, parameter, exception)
        {
        }

#if NET8_0_OR_GREATER
        [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId="SYSLIB0051", UrlFormat="https://aka.ms/dotnet-warnings/{0}")]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
#endif
        private ArgumentInvalidArrayTypeException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        [ReflectionNaming]
        internal class Argument_InvalidArrayType : ISRExceptionIdentifier<TSR, Argument_InvalidArrayType>
        {
        }
    }

    [Serializable]
    public class ArgumentInvalidArrayTypeException : ArgumentException
    {
        public ArgumentInvalidArrayTypeException()
        {
        }

        public ArgumentInvalidArrayTypeException(String? message)
            : base(message)
        {
        }

        public ArgumentInvalidArrayTypeException(String? message, Exception? exception)
            : base(message, exception)
        {
        }

        public ArgumentInvalidArrayTypeException(String? message, String? parameter)
            : base(message, parameter)
        {
        }

        public ArgumentInvalidArrayTypeException(String? message, String? parameter, Exception? exception)
            : base(message, parameter, exception)
        {
        }

#if NET8_0_OR_GREATER
        [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId="SYSLIB0051", UrlFormat="https://aka.ms/dotnet-warnings/{0}")]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
#endif
        protected ArgumentInvalidArrayTypeException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}