// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.Serialization;

namespace NetExtender.Exceptions
{
    [Serializable]
    public abstract class SynchronizationException : Exception
    {
        protected SynchronizationException()
        {
        }

        protected SynchronizationException(String? message)
            : base(message)
        {
        }

        protected SynchronizationException(String? message, Exception? exception)
            : base(message, exception)
        {
        }

#if NET8_0_OR_GREATER
        [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId="SYSLIB0051", UrlFormat="https://aka.ms/dotnet-warnings/{0}")]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
#endif
        protected SynchronizationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }

    [Serializable]
    public class CollectionSynchronizationException : SynchronizationException
    {
        public CollectionSynchronizationException()
        {
        }

        public CollectionSynchronizationException(String? message)
            : base(message)
        {
        }

        public CollectionSynchronizationException(String? message, Exception? exception)
            : base(message, exception)
        {
        }

#if NET8_0_OR_GREATER
        [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId="SYSLIB0051", UrlFormat="https://aka.ms/dotnet-warnings/{0}")]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
#endif
        protected CollectionSynchronizationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }

    [Serializable]
    public class StreamSynchronizationException : SynchronizationException
    {
        public StreamSynchronizationException()
        {
        }

        public StreamSynchronizationException(String? message)
            : base(message)
        {
        }

        public StreamSynchronizationException(String? message, Exception? exception)
            : base(message, exception)
        {
        }

#if NET8_0_OR_GREATER
        [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId="SYSLIB0051", UrlFormat="https://aka.ms/dotnet-warnings/{0}")]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
#endif
        protected StreamSynchronizationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}