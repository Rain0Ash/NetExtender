// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.Serialization;

namespace NetExtender.Types.Exceptions
{
    [Serializable]
    public class FactoryException : ArgumentException
    {
        public FactoryException()
        {
        }

        public FactoryException(String? message)
            : base(message)
        {
        }

        public FactoryException(String? message, Exception? innerException)
            : base(message, innerException)
        {
        }

        public FactoryException(String? message, String? paramName)
            : base(message, paramName)
        {
        }

        public FactoryException(String? message, String? paramName, Exception? innerException)
            : base(message, paramName, innerException)
        {
        }
        
        protected FactoryException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}