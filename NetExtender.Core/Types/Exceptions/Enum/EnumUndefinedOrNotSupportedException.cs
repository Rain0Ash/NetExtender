// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.Serialization;

namespace NetExtender.Types.Exceptions
{
    [Serializable]
    public class EnumUndefinedOrNotSupportedException<T> : IncorrentEnumTypeException<T> where T : unmanaged, Enum
    {
        public EnumUndefinedOrNotSupportedException(T value)
            : this(value, true)
        {
        }

        protected EnumUndefinedOrNotSupportedException(T value, Boolean isThrow)
        {
            if (!isThrow)
            {
                return;
            }
            
            throw Enum.IsDefined(value) ? new EnumNotSupportedException<T>(value) : new EnumUndefinedException<T>(value);
        }

        public EnumUndefinedOrNotSupportedException(T value, String? message)
            : this(value, message, true)
        {
        }

        protected EnumUndefinedOrNotSupportedException(T value, String? message, Boolean isThrow)
            : base(message)
        {
            if (!isThrow)
            {
                return;
            }
            
            throw Enum.IsDefined(value) ? new EnumNotSupportedException<T>(value, message) : new EnumUndefinedException<T>(value, null, message);
        }

        public EnumUndefinedOrNotSupportedException(T value, String? paramName, String? message)
            : this(value, paramName, message, true)
        {
        }

        protected EnumUndefinedOrNotSupportedException(T value, String? paramName, String? message, Boolean isThrow)
            : base(message, paramName)
        {
            if (!isThrow)
            {
                return;
            }
            
            throw Enum.IsDefined(value) ? new EnumNotSupportedException<T>(value, message) : new EnumUndefinedException<T>(value, paramName, message);
        }

        public EnumUndefinedOrNotSupportedException(T value, String? paramName, String? message, Exception? innerException)
            : this(value, paramName, message, innerException, true)
        {
        }

        protected EnumUndefinedOrNotSupportedException(T value, String? paramName, String? message, Exception? innerException, Boolean isThrow)
            : base(message, innerException)
        {
            if (!isThrow)
            {
                return;
            }
            
            if (Enum.IsDefined(value))
            {
                throw new EnumNotSupportedException<T>(value, message, innerException);
            }

            throw innerException is not null ? new EnumUndefinedException<T>(value, message, innerException) : new EnumUndefinedException<T>(value, paramName, message);
        }

        protected EnumUndefinedOrNotSupportedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}