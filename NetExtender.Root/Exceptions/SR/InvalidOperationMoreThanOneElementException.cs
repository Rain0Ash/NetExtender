using System;
using System.Runtime.Serialization;
using NetExtender.Exceptions.Interfaces;
using NetExtender.Utilities.Core;

namespace NetExtender.Exceptions
{
    [Serializable]
    public sealed class InvalidOperationMoreThanOneElementException<TSR> : InvalidOperationMoreThanOneElementException, ISRException<TSR, InvalidOperationMoreThanOneElementException<TSR>.MoreThanOneElement>
    {
        public InvalidOperationMoreThanOneElementException()
            : base(ISRExceptionIdentifier<MoreThanOneElement>.Instance.Resource)
        {
        }

        public InvalidOperationMoreThanOneElementException(Exception? exception)
            : base(ISRExceptionIdentifier<MoreThanOneElement>.Instance.Resource, exception)
        {
        }

#if NET8_0_OR_GREATER
        [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId="SYSLIB0051", UrlFormat="https://aka.ms/dotnet-warnings/{0}")]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
#endif
        private InvalidOperationMoreThanOneElementException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        [ReflectionNaming]
        internal class MoreThanOneElement : ISRExceptionIdentifier<TSR, MoreThanOneElement>
        {
        }
    }

    [Serializable]
    public class InvalidOperationMoreThanOneElementException : InvalidOperationException
    {
        public InvalidOperationMoreThanOneElementException()
        {
        }

        public InvalidOperationMoreThanOneElementException(String? message)
            : base(message)
        {
        }

        public InvalidOperationMoreThanOneElementException(String? message, Exception? exception)
            : base(message, exception)
        {
        }

#if NET8_0_OR_GREATER
        [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId="SYSLIB0051", UrlFormat="https://aka.ms/dotnet-warnings/{0}")]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
#endif
        protected InvalidOperationMoreThanOneElementException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}