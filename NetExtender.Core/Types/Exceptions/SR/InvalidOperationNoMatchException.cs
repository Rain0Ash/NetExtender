using System;
using System.Runtime.Serialization;
using NetExtender.Types.Exceptions.Interfaces;
using NetExtender.Utilities.Core;

namespace NetExtender.Types.Exceptions
{
    [Serializable]
    public sealed class InvalidOperationNoMatchException<TSR> : InvalidOperationNoMatchException, ISRException<TSR, InvalidOperationNoMatchException<TSR>.NoMatch>
    {
        public InvalidOperationNoMatchException()
            : base(ISRExceptionIdentifier<NoMatch>.Instance.Resource)
        {
        }

        public InvalidOperationNoMatchException(Exception? exception)
            : base(ISRExceptionIdentifier<NoMatch>.Instance.Resource, exception)
        {
        }

#if NET8_0_OR_GREATER
        [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId="SYSLIB0051", UrlFormat="https://aka.ms/dotnet-warnings/{0}")]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
#endif
        private InvalidOperationNoMatchException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        [ReflectionNaming]
        internal class NoMatch : ISRExceptionIdentifier<TSR, NoMatch>
        {
        }
    }

    [Serializable]
    public class InvalidOperationNoMatchException : InvalidOperationException
    {
        public InvalidOperationNoMatchException()
        {
        }

        public InvalidOperationNoMatchException(String? message)
            : base(message)
        {
        }

        public InvalidOperationNoMatchException(String? message, Exception? exception)
            : base(message, exception)
        {
        }

#if NET8_0_OR_GREATER
        [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId="SYSLIB0051", UrlFormat="https://aka.ms/dotnet-warnings/{0}")]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
#endif
        protected InvalidOperationNoMatchException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}