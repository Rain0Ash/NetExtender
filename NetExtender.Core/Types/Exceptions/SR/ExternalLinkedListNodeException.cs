using System;
using System.Runtime.Serialization;
using NetExtender.Types.Exceptions.Interfaces;
using NetExtender.Utilities.Core;

namespace NetExtender.Types.Exceptions
{
    [Serializable]
    public sealed class ExternalLinkedListNodeException<TSR> : ExternalLinkedListNodeException, ISRException<TSR, ExternalLinkedListNodeException<TSR>.ExternalLinkedListNode>
    {
        public ExternalLinkedListNodeException()
            : base(ISRExceptionIdentifier<ExternalLinkedListNode>.Instance.Resource)
        {
        }

        public ExternalLinkedListNodeException(Exception? exception)
            : base(ISRExceptionIdentifier<ExternalLinkedListNode>.Instance.Resource, exception)
        {
        }

#if NET8_0_OR_GREATER
        [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId="SYSLIB0051", UrlFormat="https://aka.ms/dotnet-warnings/{0}")]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
#endif
        private ExternalLinkedListNodeException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        [ReflectionNaming]
        internal class ExternalLinkedListNode : ISRExceptionIdentifier<TSR, ExternalLinkedListNode>
        {
        }
    }

    [Serializable]
    public class ExternalLinkedListNodeException : InvalidOperationException
    {
        public ExternalLinkedListNodeException()
        {
        }

        public ExternalLinkedListNodeException(String? message)
            : base(message)
        {
        }

        public ExternalLinkedListNodeException(String? message, Exception? exception)
            : base(message, exception)
        {
        }

#if NET8_0_OR_GREATER
        [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId="SYSLIB0051", UrlFormat="https://aka.ms/dotnet-warnings/{0}")]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
#endif
        protected ExternalLinkedListNodeException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}