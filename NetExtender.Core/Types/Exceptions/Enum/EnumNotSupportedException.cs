// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using NetExtender.Utilities.Serialization;

namespace NetExtender.Types.Exceptions
{
    [Serializable]
    public class EnumNotSupportedException<T> : EnumNotSupportedException where T : unmanaged, Enum
    {
        public static implicit operator T(EnumNotSupportedException<T>? exception)
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

        public EnumNotSupportedException(T value)
            : this(value, null)
        {
        }

        public EnumNotSupportedException(T value, String? message)
            : base(message ?? $"Specified value '{value}' of enum type '{typeof(T)}' is not supported.")
        {
            Value = value;
        }

        public EnumNotSupportedException(T value, String? message, Exception? innerException)
            : base(message ?? $"Specified value '{value}' of enum type '{typeof(T)}' is not supported.", innerException)
        {
            Value = value;
        }

        protected EnumNotSupportedException(SerializationInfo info, StreamingContext context)
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
    public abstract class EnumNotSupportedException : NotSupportedException
    {
        [return: NotNullIfNotNull("exception")]
        public static implicit operator Enum?(EnumNotSupportedException? exception)
        {
            return exception?.Enum;
        }
        
        public abstract Enum Enum { get; }
        
        protected EnumNotSupportedException()
        {
        }

        protected EnumNotSupportedException(String? message)
            : base(message)
        {
        }

        protected EnumNotSupportedException(String? message, Exception? innerException)
            : base(message, innerException)
        {
        }

        protected EnumNotSupportedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}