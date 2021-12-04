// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.Serialization;

namespace NetExtender.Types.Exceptions
{
    [Serializable]
    public class InvalidHashException : Exception
    {
        public InvalidHashException()
        {
        }

        public InvalidHashException(String? message)
            : base(message)
        {
        }

        public InvalidHashException(String? message, Exception? innerException)
            : base(message, innerException)
        {
        }
        
        protected InvalidHashException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}