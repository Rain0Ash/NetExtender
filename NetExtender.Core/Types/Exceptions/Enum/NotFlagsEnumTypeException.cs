// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.Serialization;

namespace NetExtender.Types.Exceptions
{
    [Serializable]
    public class NotFlagsEnumTypeException<T> : NotFlagsEnumTypeException where T : unmanaged, Enum
    {
        public sealed override Type Type
        {
            get
            {
                return typeof(T);
            }
        }

        public NotFlagsEnumTypeException()
        {
        }

        public NotFlagsEnumTypeException(String? message)
            : base(message)
        {
        }

        public NotFlagsEnumTypeException(String? message, Exception? exception)
            : base(message, exception)
        {
        }

        public NotFlagsEnumTypeException(String? message, String? parameter)
            : base(message, parameter)
        {
        }

        public NotFlagsEnumTypeException(String? message, String? parameter, Exception? exception)
            : base(message, parameter, exception)
        {
        }

        protected NotFlagsEnumTypeException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
    
    [Serializable]
    public abstract class NotFlagsEnumTypeException : IncorrectEnumTypeException
    {
        protected NotFlagsEnumTypeException()
        {
        }

        protected NotFlagsEnumTypeException(String? message)
            : base(message)
        {
        }

        protected NotFlagsEnumTypeException(String? message, Exception? exception)
            : base(message, exception)
        {
        }

        protected NotFlagsEnumTypeException(String? message, String? parameter)
            : base(message, parameter)
        {
        }

        protected NotFlagsEnumTypeException(String? message, String? parameter, Exception? exception)
            : base(message, parameter, exception)
        {
        }

        protected NotFlagsEnumTypeException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}