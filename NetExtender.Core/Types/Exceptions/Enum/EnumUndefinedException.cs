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
        
        public Type Type
        {
            get
            {
                return typeof(T);
            }
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
            : this(value, null, (String?) null)
        {
            Value = value;
        }

        public EnumUndefinedException(T value, String? paramName)
            : this(value, paramName, (String?) null)
        {
        }

        public EnumUndefinedException(T value, String? message, Exception? innerException)
            : base(message ?? $"Specified value '{value}' was out of the range of valid values of enum type '{typeof(T)}'.", innerException)
        {
            Value = value;
        }

        public EnumUndefinedException(T value, String? paramName, String? message)
            : base(paramName, value, message ?? $"Specified value '{value}' was out of the range of valid values of enum type '{typeof(T)}'.")
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
            : base(message ?? "Specified value was out of the range of valid values of enum.", innerException)
        {
        }

        protected EnumUndefinedException(String? paramName, Enum? value, String? message)
            : base(paramName, value, message is null && value is not null ? $"Specified value '{value}' was out of the range of valid values of enum type '{value.GetType()}'." : message)
        {
        }

        protected EnumUndefinedException(String? paramName, String? message)
            : base(paramName, message ?? "Specified value was out of the range of valid values of enum.")
        {
        }

        protected EnumUndefinedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}