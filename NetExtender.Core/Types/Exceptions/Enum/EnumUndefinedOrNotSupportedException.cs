// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.Serialization;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Exceptions
{
    public class EnumUndefinedOrNotSupportedException<T> : ExceptionWrapper<Exception> where T : unmanaged, Enum
    {
        public EnumUndefinedOrNotSupportedException(T value)
            : base(EnumUtilities.IsDefined(value) ? new EnumNotSupportedException<T>(value) : new EnumUndefinedException<T>(value))
        {
        }

        public EnumUndefinedOrNotSupportedException(T value, String? message)
            : base(EnumUtilities.IsDefined(value) ? new EnumNotSupportedException<T>(value, message) : new EnumUndefinedException<T>(value, null, message))
        {
        }

        public EnumUndefinedOrNotSupportedException(T value, String? paramName, String? message)
            : base(EnumUtilities.IsDefined(value) ? new EnumNotSupportedException<T>(value, message) : new EnumUndefinedException<T>(value, paramName, message))
        {
        }

        public EnumUndefinedOrNotSupportedException(T value, String? paramName, String? message, Exception? innerException)
            : base(EnumUtilities.IsDefined(value) ? throw new EnumNotSupportedException<T>(value, message, innerException) : innerException is not null ? new EnumUndefinedException<T>(value, message, innerException) : new EnumUndefinedException<T>(value, paramName, message))
        {
        }
    }
    
    [Serializable]
    public class EnumUndefinedOrNotSupportedThrowableException<T> : IncorrentEnumTypeException<T> where T : unmanaged, Enum
    {
        public EnumUndefinedOrNotSupportedThrowableException(T value)
            : this(value, true)
        {
        }

        public EnumUndefinedOrNotSupportedThrowableException(T value, Boolean @throw)
        {
            if (!@throw)
            {
                return;
            }
            
            throw EnumUtilities.IsDefined(value) ? new EnumNotSupportedException<T>(value) : new EnumUndefinedException<T>(value);
        }

        public EnumUndefinedOrNotSupportedThrowableException(T value, String? message)
            : this(value, message, true)
        {
        }

        public EnumUndefinedOrNotSupportedThrowableException(T value, String? message, Boolean @throw)
            : base(message)
        {
            if (!@throw)
            {
                return;
            }
            
            throw EnumUtilities.IsDefined(value) ? new EnumNotSupportedException<T>(value, message) : new EnumUndefinedException<T>(value, null, message);
        }

        public EnumUndefinedOrNotSupportedThrowableException(T value, String? paramName, String? message)
            : this(value, paramName, message, true)
        {
        }

        public EnumUndefinedOrNotSupportedThrowableException(T value, String? paramName, String? message, Boolean @throw)
            : base(message, paramName)
        {
            if (!@throw)
            {
                return;
            }
            
            throw EnumUtilities.IsDefined(value) ? new EnumNotSupportedException<T>(value, message) : new EnumUndefinedException<T>(value, paramName, message);
        }

        public EnumUndefinedOrNotSupportedThrowableException(T value, String? paramName, String? message, Exception? innerException)
            : this(value, paramName, message, innerException, true)
        {
        }

        public EnumUndefinedOrNotSupportedThrowableException(T value, String? paramName, String? message, Exception? innerException, Boolean @throw)
            : base(message, innerException)
        {
            if (!@throw)
            {
                return;
            }
            
            if (EnumUtilities.IsDefined(value))
            {
                throw new EnumNotSupportedException<T>(value, message, innerException);
            }

            throw innerException is not null ? new EnumUndefinedException<T>(value, message, innerException) : new EnumUndefinedException<T>(value, paramName, message);
        }

        protected EnumUndefinedOrNotSupportedThrowableException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}