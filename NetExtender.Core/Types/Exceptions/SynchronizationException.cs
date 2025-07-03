// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.Serialization;

namespace NetExtender.Types.Exceptions
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

        protected StreamSynchronizationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}