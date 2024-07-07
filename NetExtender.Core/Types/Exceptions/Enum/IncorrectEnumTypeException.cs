// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace NetExtender.Types.Exceptions
{
    [Serializable]
    public class IncorrectEnumTypeException<T> : IncorrectEnumTypeException
    {
        public sealed override Type Type
        {
            get
            {
                return typeof(T);
            }
        }

        public IncorrectEnumTypeException()
        {
        }

        public IncorrectEnumTypeException(String? message)
            : base(message)
        {
        }

        public IncorrectEnumTypeException(String? message, Exception? innerException)
            : base(message, innerException)
        {
        }

        public IncorrectEnumTypeException(String? message, String? paramName)
            : base(message, paramName)
        {
        }

        public IncorrectEnumTypeException(String? message, String? paramName, Exception? innerException)
            : base(message, paramName, innerException)
        {
        }

        protected IncorrectEnumTypeException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
    
    [Serializable]
    public abstract class IncorrectEnumTypeException : ArgumentException
    {
        [return: NotNullIfNotNull("exception")]
        public static implicit operator Type?(IncorrectEnumTypeException? exception)
        {
            return exception?.Type;
        }
        
        public abstract Type Type { get; }

        protected IncorrectEnumTypeException()
        {
        }

        protected IncorrectEnumTypeException(String? message)
            : base(message)
        {
        }

        protected IncorrectEnumTypeException(String? message, Exception? innerException)
            : base(message, innerException)
        {
        }

        protected IncorrectEnumTypeException(String? message, String? paramName)
            : base(message, paramName)
        {
        }

        protected IncorrectEnumTypeException(String? message, String? paramName, Exception? innerException)
            : base(message, paramName, innerException)
        {
        }

        protected IncorrectEnumTypeException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}