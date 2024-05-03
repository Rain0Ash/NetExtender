// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.Serialization;
using NetExtender.Types.Enums;
using NetExtender.Utilities.Types;

namespace NetExtender.Types.Exceptions
{
    public class EnumUndefinedOrNotSupportedException<T, TEnum> : EnumUndefinedOrNotSupportedException<T> where T : unmanaged, Enum where TEnum : Enum<T, TEnum>, new()
    {
        public EnumUndefinedOrNotSupportedException(TEnum value)
            : this(value.Id.In() ? EnumNotSupportedException<T, TEnum>.Create(value) : EnumUndefinedException<T, TEnum>.Create(value))
        {
        }

        public EnumUndefinedOrNotSupportedException(TEnum value, String? message)
            : this(value.Id.In() ? EnumNotSupportedException<T, TEnum>.Create(value, message) : EnumUndefinedException<T, TEnum>.Create(value, null, message))
        {
        }

        public EnumUndefinedOrNotSupportedException(TEnum value, String? paramName, String? message)
            : this(value.Id.In() ? EnumNotSupportedException<T, TEnum>.Create(value, message) : EnumUndefinedException<T, TEnum>.Create(value, paramName, message))
        {
        }

        public EnumUndefinedOrNotSupportedException(TEnum value, String? paramName, String? message, Exception? innerException)
            : this(value.Id.In() ? throw EnumNotSupportedException<T, TEnum>.Create(value, message, innerException) : innerException is not null ? EnumUndefinedException<T, TEnum>.Create(value, message, innerException) : EnumUndefinedException<T, TEnum>.Create(value, paramName, message))
        {
        }
        
        protected EnumUndefinedOrNotSupportedException(Exception exception)
            : base(exception)
        {
        }
    }
    
    public class EnumUndefinedOrNotSupportedException<T> : ExceptionWrapper<Exception> where T : unmanaged, Enum
    {
        public EnumUndefinedOrNotSupportedException(T value)
            : base(value.In() ? EnumNotSupportedException<T>.Create(value) : EnumUndefinedException<T>.Create(value))
        {
        }

        public EnumUndefinedOrNotSupportedException(T value, String? message)
            : base(value.In() ? EnumNotSupportedException<T>.Create(value, message) : EnumUndefinedException<T>.Create(value, null, message))
        {
        }

        public EnumUndefinedOrNotSupportedException(T value, String? paramName, String? message)
            : base(value.In() ? EnumNotSupportedException<T>.Create(value, message) : EnumUndefinedException<T>.Create(value, paramName, message))
        {
        }

        public EnumUndefinedOrNotSupportedException(T value, String? paramName, String? message, Exception? innerException)
            : base(value.In() ? throw EnumNotSupportedException<T>.Create(value, message, innerException) : innerException is not null ? EnumUndefinedException<T>.Create(value, message, innerException) : EnumUndefinedException<T>.Create(value, paramName, message))
        {
        }

        protected EnumUndefinedOrNotSupportedException(Exception exception)
            : base(exception)
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
            
            throw value.In() ? EnumNotSupportedException<T>.Create(value) : EnumUndefinedException<T>.Create(value);
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
            
            throw value.In() ? EnumNotSupportedException<T>.Create(value, message) : EnumUndefinedException<T>.Create(value, null, message);
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
            
            throw value.In() ? EnumNotSupportedException<T>.Create(value, message) : EnumUndefinedException<T>.Create(value, paramName, message);
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
            
            if (value.In())
            {
                throw EnumNotSupportedException<T>.Create(value, message, innerException);
            }

            throw innerException is not null ? EnumUndefinedException<T>.Create(value, message, innerException) : EnumUndefinedException<T>.Create(value, paramName, message);
        }

        protected EnumUndefinedOrNotSupportedThrowableException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}