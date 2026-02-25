using System;
using System.Runtime.Serialization;
using NetExtender.Exceptions.Interfaces;
using NetExtender.Utilities.Core;

namespace NetExtender.Exceptions
{
    [Serializable]
    public sealed class InvalidOperationNoElementsException<TSR> : InvalidOperationNoElementsException, ISRException<TSR, InvalidOperationNoElementsException<TSR>.NoElements>
    {
        public InvalidOperationNoElementsException()
            : base(ISRExceptionIdentifier<NoElements>.Instance.Resource)
        {
        }

        public InvalidOperationNoElementsException(Exception? exception)
            : base(ISRExceptionIdentifier<NoElements>.Instance.Resource, exception)
        {
        }

#if NET8_0_OR_GREATER
        [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId="SYSLIB0051", UrlFormat="https://aka.ms/dotnet-warnings/{0}")]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
#endif
        private InvalidOperationNoElementsException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        [ReflectionNaming]
        internal class NoElements : ISRExceptionIdentifier<TSR, NoElements>
        {
        }
    }

    [Serializable]
    public class InvalidOperationNoElementsException : InvalidOperationException
    {
        public InvalidOperationNoElementsException()
        {
        }

        public InvalidOperationNoElementsException(String? message)
            : base(message)
        {
        }

        public InvalidOperationNoElementsException(String? message, Exception? exception)
            : base(message, exception)
        {
        }

#if NET8_0_OR_GREATER
        [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId="SYSLIB0051", UrlFormat="https://aka.ms/dotnet-warnings/{0}")]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
#endif
        protected InvalidOperationNoElementsException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}