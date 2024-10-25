// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using NetExtender.Types.Enums;
using NetExtender.Utilities.Serialization;

namespace NetExtender.Types.Exceptions
{
    [Serializable]
    public abstract class EnumNotSupportedException<T, TEnum> : EnumNotSupportedException<T> where T : unmanaged, Enum where TEnum : Enum<T, TEnum>, new()
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EnumNotSupportedException<T, TEnum> Create(TEnum value)
        {
            return new Exception(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EnumNotSupportedException<T, TEnum> Create(TEnum value, String? message)
        {
            return new Exception(value, message);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EnumNotSupportedException<T, TEnum> Create(TEnum value, String? message, System.Exception? exception)
        {
            return new Exception(value, message, exception);
        }
        
        [return: NotNullIfNotNull("exception")]
        public static implicit operator TEnum?(EnumNotSupportedException<T, TEnum>? exception)
        {
            return exception?.Enum ?? default;
        }
        
        public override Type Type
        {
            get
            {
                return Enum.GetType();
            }
        }

        public Type Underlying
        {
            get
            {
                return Enum.Underlying;
            }
        }

        public new abstract TEnum Enum { get; }

        public sealed override T Value
        {
            get
            {
                return Enum.Id;
            }
        }
        
        protected EnumNotSupportedException()
        {
        }

        protected EnumNotSupportedException(String? message)
            : base(message)
        {
        }

        protected EnumNotSupportedException(String? message, System.Exception? exception)
            : base(message, exception)
        {
        }

        protected EnumNotSupportedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
        
        [Serializable]
        private sealed class Exception : EnumNotSupportedException<T, TEnum>
        {
            public override TEnum Enum { get; }

            public Exception(TEnum value)
                : this(value, null)
            {
            }

            public Exception(TEnum value, String? message)
                : base(value is not null ? message ?? $"Specified value '{value}' of enum type '{value.Underlying}' is not supported." : throw new ArgumentNullException(nameof(value)))
            {
                Enum = value;
            }

            public Exception(TEnum value, String? message, System.Exception? exception)
                : base(value is not null ? message ?? $"Specified value '{value}' of enum type '{value.Underlying}' is not supported." : throw new ArgumentNullException(nameof(value)), exception)
            {
                Enum = value;
            }

            private Exception(SerializationInfo info, StreamingContext context)
                : base(info, context)
            {
                Enum = info.GetValue<TEnum>(nameof(Enum));
            }
            
            public override void GetObjectData(SerializationInfo info, StreamingContext context)
            {
                base.GetObjectData(info, context);
                info.AddValue(nameof(Enum), Enum);
            }
        }
    }

    [Serializable]
    public abstract class EnumNotSupportedException<T> : EnumNotSupportedException where T : unmanaged, Enum
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EnumNotSupportedException<T> Create(T value)
        {
            return new Exception(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EnumNotSupportedException<T> Create(T value, String? message)
        {
            return new Exception(value, message);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EnumNotSupportedException<T> Create(T value, String? message, System.Exception? exception)
        {
            return new Exception(value, message, exception);
        }
        
        public static implicit operator T(EnumNotSupportedException<T>? exception)
        {
            return exception?.Value ?? default;
        }
        
        public override Type Type
        {
            get
            {
                return typeof(T);
            }
        }
        
        public abstract T Value { get; }

        public sealed override Enum Enum
        {
            get
            {
                return Value;
            }
        }

        protected EnumNotSupportedException()
        {
        }

        protected EnumNotSupportedException(String? message)
            : base(message)
        {
        }

        protected EnumNotSupportedException(String? message, System.Exception? exception)
            : base(message, exception)
        {
        }

        protected EnumNotSupportedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        [Serializable]
        private sealed class Exception : EnumNotSupportedException<T>
        {
            public override T Value { get; }

            public Exception(T value)
                : this(value, null)
            {
            }

            public Exception(T value, String? message)
                : base(message ?? $"Specified value '{value}' of enum type '{typeof(T)}' is not supported.")
            {
                Value = value;
            }

            public Exception(T value, String? message, System.Exception? exception)
                : base(message ?? $"Specified value '{value}' of enum type '{typeof(T)}' is not supported.", exception)
            {
                Value = value;
            }

            private Exception(SerializationInfo info, StreamingContext context)
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
    }

    [Serializable]
    public abstract class EnumNotSupportedException : NotSupportedException
    {
        [return: NotNullIfNotNull("exception")]
        public static implicit operator Enum?(EnumNotSupportedException? exception)
        {
            return exception?.Enum;
        }
        
        public virtual Type Type
        {
            get
            {
                return Enum.GetType();
            }
        }
        
        public abstract Enum Enum { get; }
        
        protected EnumNotSupportedException()
        {
        }

        protected EnumNotSupportedException(String? message)
            : base(message)
        {
        }

        protected EnumNotSupportedException(String? message, Exception? exception)
            : base(message, exception)
        {
        }

        protected EnumNotSupportedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}