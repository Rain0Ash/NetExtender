// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.Serialization;
using JetBrains.Annotations;

namespace NetExtender.Exceptions
{
    [Serializable]
    public class CollectionSyncException : Exception
    {
        public CollectionSyncException()
        {
        }

        public CollectionSyncException([CanBeNull] String? message)
            : base(message)
        {
        }

        public CollectionSyncException([CanBeNull] String? message, [CanBeNull] Exception? innerException)
            : base(message, innerException)
        {
        }
        
        protected CollectionSyncException([NotNull] SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}