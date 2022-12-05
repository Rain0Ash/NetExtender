// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.Serialization;

namespace NetExtender.Types.Exceptions
{
    [Serializable]
    public sealed class EnumUndefinedOrNotSupportedException<T> : IncorrentEnumTypeException<T> where T : unmanaged, Enum
    {
        public EnumUndefinedOrNotSupportedException(T value)
        {
            throw Enum.IsDefined(value) ? new EnumNotSupportedException<T>(value) : new EnumUndefinedException<T>(value);
        }

        public EnumUndefinedOrNotSupportedException(T value, String? message)
            : base(message)
        {
            throw Enum.IsDefined(value) ? new EnumNotSupportedException<T>(value, message) : new EnumUndefinedException<T>(value, null, message);
        }

        public EnumUndefinedOrNotSupportedException(T value, String? paramName, String? message)
            : base(message, paramName)
        {
            throw Enum.IsDefined(value) ? new EnumNotSupportedException<T>(value, message) : new EnumUndefinedException<T>(value, paramName, message);
        }

        public EnumUndefinedOrNotSupportedException(T value, String? paramName, String? message, Exception? innerException)
            : base(message, innerException)
        {
            if (Enum.IsDefined(value))
            {
                throw new EnumNotSupportedException<T>(value, message, innerException);
            }

            throw innerException is not null ? new EnumUndefinedException<T>(value, message, innerException) : new EnumUndefinedException<T>(value, paramName, message);
        }

        private EnumUndefinedOrNotSupportedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}