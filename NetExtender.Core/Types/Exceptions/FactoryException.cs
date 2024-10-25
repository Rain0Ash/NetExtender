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

        public FactoryException(String? message, Exception? exception)
            : base(message, exception)
        {
        }

        public FactoryException(String? message, String? parameter)
            : base(message, parameter)
        {
        }

        public FactoryException(String? message, String? parameter, Exception? exception)
            : base(message, parameter, exception)
        {
        }

        protected FactoryException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}