// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.Serialization;

namespace NetExtender.Types.Exceptions
{
    [Serializable]
    public class ReflectionExampleException : NotSupportedReflectionException
    {
        private new const String Message = "Operation is not supported because it is meant only for reflection purposes and not for direct invocation.";

        public ReflectionExampleException()
            : base(Message)
        {
        }

        public ReflectionExampleException(String? message)
            : base(message ?? Message)
        {
        }

        public ReflectionExampleException(String? message, Exception? exception)
            : base(message ?? Message, exception)
        {
        }

#if NET8_0_OR_GREATER
        [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId="SYSLIB0051", UrlFormat="https://aka.ms/dotnet-warnings/{0}")]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
#endif
        protected ReflectionExampleException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }

    [Serializable]
    public class NotImplementedReflectionException : NotImplementedException
    {
        private new const String Message = "Reflection operation is not implemented.";

        public NotImplementedReflectionException()
            : base(Message)
        {
        }

        public NotImplementedReflectionException(String? message)
            : base(message ?? Message)
        {
        }

        public NotImplementedReflectionException(String? message, Exception? exception)
            : base(message ?? Message, exception)
        {
        }

#if NET8_0_OR_GREATER
        [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId="SYSLIB0051", UrlFormat="https://aka.ms/dotnet-warnings/{0}")]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
#endif
        protected NotImplementedReflectionException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }

    [Serializable]
    public class NotSupportedReflectionException : NotSupportedException
    {
        private new const String Message = "Reflection operation is not supported.";

        public NotSupportedReflectionException()
            : base(Message)
        {
        }

        public NotSupportedReflectionException(String? message)
            : base(message ?? Message)
        {
        }

        public NotSupportedReflectionException(String? message, Exception? exception)
            : base(message ?? Message, exception)
        {
        }

#if NET8_0_OR_GREATER
        [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId="SYSLIB0051", UrlFormat="https://aka.ms/dotnet-warnings/{0}")]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
#endif
        protected NotSupportedReflectionException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }

    [Serializable]
    public class ReflectionOperationException : InvalidOperationException
    {
        private new const String Message = "Invalid reflection operation.";

        public ReflectionOperationException()
            : base(Message)
        {
        }

        public ReflectionOperationException(String? message)
            : base(message ?? Message)
        {
        }

        public ReflectionOperationException(String? message, Exception? exception)
            : base(message ?? Message, exception)
        {
        }

#if NET8_0_OR_GREATER
        [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId="SYSLIB0051", UrlFormat="https://aka.ms/dotnet-warnings/{0}")]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
#endif
        protected ReflectionOperationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}