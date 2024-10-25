// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using NetExtender.Utilities.Serialization;

namespace NetExtender.Types.Exceptions
{
    [Serializable]
    public class TypeNotSupportedException<T> : TypeNotSupportedException
    {
        public TypeNotSupportedException()
            : base(typeof(T))
        {
        }

        public TypeNotSupportedException(String? message)
            : base(typeof(T), message)
        {
        }

        public TypeNotSupportedException(String? message, Exception? exception)
            : base(typeof(T), message, exception)
        {
        }

        protected TypeNotSupportedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }

    [Serializable]
    public class TypeNotSupportedException : NotSupportedException
    {
        [return: NotNullIfNotNull("exception")]
        public static implicit operator Type?(TypeNotSupportedException? exception)
        {
            return exception?.Type;
        }
        
        public Type Type { get; }

        public TypeNotSupportedException(Type type)
        {
            Type = type;
        }

        public TypeNotSupportedException(Type type, String? message)
            : base(message)
        {
            Type = type;
        }

        public TypeNotSupportedException(Type type, String? message, Exception? exception)
            : base(message, exception)
        {
            Type = type;
        }

        protected TypeNotSupportedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Type = info.GetValue<Type>(nameof(Type));
        }
        
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue(nameof(Type), Type);
        }
    }
}