using System;
using System.Runtime.Serialization;
using NetExtender.Exceptions.Interfaces;
using NetExtender.Utilities.Core;

namespace NetExtender.Exceptions
{
    [Serializable]
    public sealed class InvalidUtf8Exception<TSR> : InvalidUtf8Exception, ISRException<TSR, InvalidUtf8Exception<TSR>.InvalidOperation_InvalidUtf8>
    {
        public InvalidUtf8Exception()
            : base(ISRExceptionIdentifier<InvalidOperation_InvalidUtf8>.Instance.Resource)
        {
        }

        public InvalidUtf8Exception(Exception? exception)
            : base(ISRExceptionIdentifier<InvalidOperation_InvalidUtf8>.Instance.Resource, exception)
        {
        }

#if NET8_0_OR_GREATER
        [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId="SYSLIB0051", UrlFormat="https://aka.ms/dotnet-warnings/{0}")]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
#endif
        private InvalidUtf8Exception(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        [ReflectionNaming]
        internal class InvalidOperation_InvalidUtf8 : ISRExceptionIdentifier<TSR, InvalidOperation_InvalidUtf8>
        {
        }
    }

    [Serializable]
    public class InvalidUtf8Exception : InvalidOperationException
    {
        public InvalidUtf8Exception()
        {
        }

        public InvalidUtf8Exception(String? message)
            : base(message)
        {
        }

        public InvalidUtf8Exception(String? message, Exception? exception)
            : base(message, exception)
        {
        }

#if NET8_0_OR_GREATER
        [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId="SYSLIB0051", UrlFormat="https://aka.ms/dotnet-warnings/{0}")]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
#endif
        protected InvalidUtf8Exception(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}