// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.Serialization;
using JetBrains.Annotations;

namespace NetExtender.Exceptions
{
    [Serializable]
    public class InvalidHashException : Exception
    {
        public InvalidHashException()
        {
        }

        protected InvalidHashException([NotNull] SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public InvalidHashException([CanBeNull] String? message)
            : base(message)
        {
        }

        public InvalidHashException([CanBeNull] String? message, [CanBeNull] Exception? innerException)
            : base(message, innerException)
        {
        }
    }
}