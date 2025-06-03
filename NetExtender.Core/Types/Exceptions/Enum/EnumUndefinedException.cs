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
    public abstract class EnumUndefinedException<T, TEnum> : EnumUndefinedException<T> where T : unmanaged, Enum where TEnum : Enum<T, TEnum>, new()
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EnumUndefinedException<T, TEnum> Create(TEnum value)
        {
            return new Exception(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EnumUndefinedException<T, TEnum> Create(TEnum value, String? parameter)
        {
            return new Exception(value, parameter);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EnumUndefinedException<T, TEnum> Create(TEnum value, String? message, System.Exception? exception)
        {
            return new Exception(value, message, exception);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EnumUndefinedException<T, TEnum> Create(TEnum value, String? parameter, String? message)
        {
            return new Exception(value, parameter, message);
        }

        public static implicit operator T(EnumUndefinedException<T, TEnum>? exception)
        {
            return exception?.Value ?? default;
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
        
        protected EnumUndefinedException()
        {
        }

        protected EnumUndefinedException(String? parameter)
            : base(parameter)
        {
        }

        protected EnumUndefinedException(String? message, System.Exception? exception)
            : base(message, exception)
        {
        }

        protected EnumUndefinedException(String? parameter, T value, String? message)
            : base(parameter, value, message)
        {
        }

        protected EnumUndefinedException(String? parameter, String? message)
            : base(parameter, message)
        {
        }

        protected EnumUndefinedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        [Serializable]
        private sealed class Exception : EnumUndefinedException<T, TEnum>
        {
            public override TEnum Enum { get; }
            
            public Exception(TEnum value)
                : this(value, null, (String?) null)
            {
                Enum = value;
            }

            public Exception(TEnum value, String? parameter)
                : this(value, parameter, (String?) null)
            {
            }

            public Exception(TEnum value, String? message, System.Exception? exception)
                : base(value is not null ? message ?? $"Specified value '{value}' was out of the range of valid values of enum type '{value.Underlying}'." : throw new ArgumentNullException(nameof(value)), exception)
            {
                Enum = value;
            }

            public Exception(TEnum value, String? parameter, String? message)
                : base(parameter, value, value is not null ? message ?? $"Specified value '{value}' was out of the range of valid values of enum type '{value.Underlying}'." : throw new ArgumentNullException(nameof(value)))
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
    public abstract class EnumUndefinedException<T> : EnumUndefinedException where T : unmanaged, Enum
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EnumUndefinedException<T> Create(T value)
        {
            return new Exception(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EnumUndefinedException<T> Create(T value, String? parameter)
        {
            return new Exception(value, parameter);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EnumUndefinedException<T> Create(T value, String? message, System.Exception? exception)
        {
            return new Exception(value, message, exception);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EnumUndefinedException<T> Create(T value, String? parameter, String? message)
        {
            return new Exception(value, parameter, message);
        }

        public static implicit operator T(EnumUndefinedException<T>? exception)
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
        
        protected EnumUndefinedException()
        {
        }

        protected EnumUndefinedException(String? parameter)
            : base(parameter)
        {
        }

        protected EnumUndefinedException(String? message, System.Exception? exception)
            : base(message, exception)
        {
        }

        protected EnumUndefinedException(String? parameter, T value, String? message)
            : base(parameter, value, message)
        {
        }

        protected EnumUndefinedException(String? parameter, String? message)
            : base(parameter, message)
        {
        }

        protected EnumUndefinedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        [Serializable]
        private sealed class Exception : EnumUndefinedException<T>
        {
            public override T Value { get; }
            
            public Exception(T value)
                : this(value, null, (String?) null)
            {
                Value = value;
            }

            public Exception(T value, String? parameter)
                : this(value, parameter, (String?) null)
            {
            }

            public Exception(T value, String? message, System.Exception? exception)
                : base(message ?? $"Specified value '{value}' was out of the range of valid values of enum type '{typeof(T).Name}'.", exception)
            {
                Value = value;
            }

            public Exception(T value, String? parameter, String? message)
                : base(parameter, value, message ?? $"Specified value '{value}' was out of the range of valid values of enum type '{typeof(T).Name}'.")
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
    public abstract class EnumUndefinedException : ArgumentOutOfRangeException
    {
        [return: NotNullIfNotNull("exception")]
        public static implicit operator Enum?(EnumUndefinedException? exception)
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
        
        protected EnumUndefinedException()
        {
        }

        protected EnumUndefinedException(String? parameter)
            : base(parameter)
        {
        }

        protected EnumUndefinedException(String? message, Exception? exception)
            : base(message ?? "Specified value was out of the range of valid values of enum.", exception)
        {
        }

        protected EnumUndefinedException(String? parameter, Enum? value, String? message)
            : base(parameter, value, message is null && value is not null ? $"Specified value '{value}' was out of the range of valid values of enum type '{value.GetType()}'." : message)
        {
        }

        protected EnumUndefinedException(String? parameter, String? message)
            : base(parameter, message ?? "Specified value was out of the range of valid values of enum.")
        {
        }

        protected EnumUndefinedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}