// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.Serialization;

namespace NetExtender.Types.Exceptions
{
    [Serializable]
    public class CollectionSyncException : Exception
    {
        public CollectionSyncException()
        {
        }

        public CollectionSyncException(String? message)
            : base(message)
        {
        }

        public CollectionSyncException(String? message, Exception? innerException)
            : base(message, innerException)
        {
        }
        
        protected CollectionSyncException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}