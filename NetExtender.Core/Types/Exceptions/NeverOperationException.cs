// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.Serialization;

namespace NetExtender.Types.Exceptions
{
    [Serializable]
    public class NeverOperationException : InvalidOperationException
    {
        public NeverOperationException()
        {
        }

        public NeverOperationException(String? message)
            : base(message)
        {
        }

        public NeverOperationException(Exception? exception)
            : this(exception?.Message, exception)
        {
        }

        public NeverOperationException(String? message, Exception? innerException)
            : base(message, innerException)
        {
        }

        protected NeverOperationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}