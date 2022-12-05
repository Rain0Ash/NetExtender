// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using NetExtender.Utilities.Serialization;

namespace NetExtender.Types.Exceptions
{
    [Serializable]
    public class EnumUndefinedException<T> : EnumUndefinedException where T : unmanaged, Enum
    {
        public static implicit operator T(EnumUndefinedException<T>? exception)
        {
            return exception?.Value ?? default;
        }
        
        public T Value { get; }

        public override Enum Enum
        {
            get
            {
                return Value;
            }
        }

        public EnumUndefinedException(T value)
        {
            Value = value;
        }

        public EnumUndefinedException(T value, String? paramName)
            : base(paramName, value, null)
        {
            Value = value;
        }

        public EnumUndefinedException(T value, String? message, Exception? innerException)
            : base(message, innerException)
        {
            Value = value;
        }

        public EnumUndefinedException(T value, String? paramName, String? message)
            : base(paramName, value, message)
        {
            Value = value;
        }

        protected EnumUndefinedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Value = info.GetValue<T>(nameof(Value));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue(nameof(Value), Value);
        }
    }

    [Serializable]
    public abstract class EnumUndefinedException : ArgumentOutOfRangeException
    {
        [return: NotNullIfNotNull("exception")]
        public static implicit operator Enum?(EnumUndefinedException? exception)
        {
            return exception?.Enum;
        }
        
        public abstract Enum Enum { get; }
        
        protected EnumUndefinedException()
        {
        }

        protected EnumUndefinedException(String? paramName)
            : base(paramName)
        {
        }

        protected EnumUndefinedException(String? message, Exception? innerException)
            : base(message, innerException)
        {
        }

        protected EnumUndefinedException(String? paramName, Enum? actualValue, String? message)
            : base(paramName, actualValue, message)
        {
        }

        protected EnumUndefinedException(String? paramName, String? message)
            : base(paramName, message)
        {
        }

        protected EnumUndefinedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}