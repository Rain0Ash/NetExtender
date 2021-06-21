// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.Serialization;

namespace NetExtender.Exceptions
{
    /// <summary>
    /// Represents an exception that is thrown when not found something.
    /// </summary>
    [Serializable]
    public sealed class NotFoundException : Exception
    {
        public NotFoundException()
        {
        }

        public NotFoundException(String message)
            : base(message)
        {
        }

        public NotFoundException(String message, Exception exception)
            : base(message, exception)
        {
        }

        private NotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}