// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.Serialization;

namespace NetExtender.Types.Exceptions
{
    [Serializable]
    public class CollectionSynchronizationException : Exception
    {
        public CollectionSynchronizationException()
        {
        }

        public CollectionSynchronizationException(String? message)
            : base(message)
        {
        }

        public CollectionSynchronizationException(String? message, Exception? innerException)
            : base(message, innerException)
        {
        }
        
        protected CollectionSynchronizationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}