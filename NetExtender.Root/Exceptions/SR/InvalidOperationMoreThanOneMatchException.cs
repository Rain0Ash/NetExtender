using System;
using System.Runtime.Serialization;
using NetExtender.Exceptions.Interfaces;
using NetExtender.Utilities.Core;

namespace NetExtender.Exceptions
{
    [Serializable]
    public sealed class InvalidOperationMoreThanOneMatchException<TSR> : InvalidOperationMoreThanOneMatchException, ISRException<TSR, InvalidOperationMoreThanOneMatchException<TSR>.MoreThanOneMatch>
    {
        public InvalidOperationMoreThanOneMatchException()
            : base(ISRExceptionIdentifier<MoreThanOneMatch>.Instance.Resource)
        {
        }

        public InvalidOperationMoreThanOneMatchException(Exception? exception)
            : base(ISRExceptionIdentifier<MoreThanOneMatch>.Instance.Resource, exception)
        {
        }

#if NET8_0_OR_GREATER
        [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId="SYSLIB0051", UrlFormat="https://aka.ms/dotnet-warnings/{0}")]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
#endif
        private InvalidOperationMoreThanOneMatchException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        [ReflectionNaming]
        internal class MoreThanOneMatch : ISRExceptionIdentifier<TSR, MoreThanOneMatch>
        {
        }
    }

    [Serializable]
    public class InvalidOperationMoreThanOneMatchException : InvalidOperationException
    {
        public InvalidOperationMoreThanOneMatchException()
        {
        }

        public InvalidOperationMoreThanOneMatchException(String? message)
            : base(message)
        {
        }

        public InvalidOperationMoreThanOneMatchException(String? message, Exception? exception)
            : base(message, exception)
        {
        }

#if NET8_0_OR_GREATER
        [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId="SYSLIB0051", UrlFormat="https://aka.ms/dotnet-warnings/{0}")]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
#endif
        protected InvalidOperationMoreThanOneMatchException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}