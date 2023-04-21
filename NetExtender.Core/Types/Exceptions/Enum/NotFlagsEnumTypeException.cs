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

        public NotFlagsEnumTypeException(String? message, Exception? innerException)
            : base(message, innerException)
        {
        }

        public NotFlagsEnumTypeException(String? message, String? paramName)
            : base(message, paramName)
        {
        }

        public NotFlagsEnumTypeException(String? message, String? paramName, Exception? innerException)
            : base(message, paramName, innerException)
        {
        }

        protected NotFlagsEnumTypeException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
    
    [Serializable]
    public abstract class NotFlagsEnumTypeException : IncorrentEnumTypeException
    {
        protected NotFlagsEnumTypeException()
        {
        }

        protected NotFlagsEnumTypeException(String? message)
            : base(message)
        {
        }

        protected NotFlagsEnumTypeException(String? message, Exception? innerException)
            : base(message, innerException)
        {
        }

        protected NotFlagsEnumTypeException(String? message, String? paramName)
            : base(message, paramName)
        {
        }

        protected NotFlagsEnumTypeException(String? message, String? paramName, Exception? innerException)
            : base(message, paramName, innerException)
        {
        }

        protected NotFlagsEnumTypeException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}