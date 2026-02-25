// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.Serialization;

namespace NetExtender.Exceptions
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

#if NET8_0_OR_GREATER
        [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId="SYSLIB0051", UrlFormat="https://aka.ms/dotnet-warnings/{0}")]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
#endif
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

#if NET8_0_OR_GREATER
        [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId="SYSLIB0051", UrlFormat="https://aka.ms/dotnet-warnings/{0}")]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
#endif
        protected NotFlagsEnumTypeException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}