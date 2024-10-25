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

        public IncorrectEnumTypeException(String? message, Exception? exception)
            : base(message, exception)
        {
        }

        public IncorrectEnumTypeException(String? message, String? parameter)
            : base(message, parameter)
        {
        }

        public IncorrectEnumTypeException(String? message, String? parameter, Exception? exception)
            : base(message, parameter, exception)
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

        protected IncorrectEnumTypeException(String? message, Exception? exception)
            : base(message, exception)
        {
        }

        protected IncorrectEnumTypeException(String? message, String? parameter)
            : base(message, parameter)
        {
        }

        protected IncorrectEnumTypeException(String? message, String? parameter, Exception? exception)
            : base(message, parameter, exception)
        {
        }

        protected IncorrectEnumTypeException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}