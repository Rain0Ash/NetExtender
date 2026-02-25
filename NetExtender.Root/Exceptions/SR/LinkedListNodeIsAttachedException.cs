using System;
using System.Runtime.Serialization;
using NetExtender.Exceptions.Interfaces;
using NetExtender.Utilities.Core;

namespace NetExtender.Exceptions
{
    [Serializable]
    public sealed class LinkedListNodeIsAttachedException<TSR> : LinkedListNodeIsAttachedException, ISRException<TSR, LinkedListNodeIsAttachedException<TSR>.LinkedListNodeIsAttached>
    {
        public LinkedListNodeIsAttachedException()
            : base(ISRExceptionIdentifier<LinkedListNodeIsAttached>.Instance.Resource)
        {
        }

        public LinkedListNodeIsAttachedException(Exception? exception)
            : base(ISRExceptionIdentifier<LinkedListNodeIsAttached>.Instance.Resource, exception)
        {
        }

#if NET8_0_OR_GREATER
        [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId="SYSLIB0051", UrlFormat="https://aka.ms/dotnet-warnings/{0}")]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
#endif
        private LinkedListNodeIsAttachedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        [ReflectionNaming]
        internal class LinkedListNodeIsAttached : ISRExceptionIdentifier<TSR, LinkedListNodeIsAttached>
        {
        }
    }

    [Serializable]
    public class LinkedListNodeIsAttachedException : InvalidOperationException
    {
        public LinkedListNodeIsAttachedException()
        {
        }

        public LinkedListNodeIsAttachedException(String? message)
            : base(message)
        {
        }

        public LinkedListNodeIsAttachedException(String? message, Exception? exception)
            : base(message, exception)
        {
        }

#if NET8_0_OR_GREATER
        [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId="SYSLIB0051", UrlFormat="https://aka.ms/dotnet-warnings/{0}")]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
#endif
        protected LinkedListNodeIsAttachedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}