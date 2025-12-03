using System;
using System.Runtime.Serialization;
using NetExtender.Types.Exceptions.Interfaces;
using NetExtender.Utilities.Core;

namespace NetExtender.Types.Exceptions
{
    [Serializable]
    public sealed class ArithmeticNaNException<TSR> : ArithmeticNaNException, ISRException<TSR, ArithmeticNaNException<TSR>.Arithmetic_NaN>
    {
        public ArithmeticNaNException()
            : base(ISRExceptionIdentifier<Arithmetic_NaN>.Instance.Resource)
        {
        }

        public ArithmeticNaNException(Exception? exception)
            : base(ISRExceptionIdentifier<Arithmetic_NaN>.Instance.Resource, exception)
        {
        }

#if NET8_0_OR_GREATER
        [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId="SYSLIB0051", UrlFormat="https://aka.ms/dotnet-warnings/{0}")]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
#endif
        private ArithmeticNaNException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        [ReflectionNaming]
        internal class Arithmetic_NaN : ISRExceptionIdentifier<TSR, Arithmetic_NaN>
        {
        }
    }

    [Serializable]
    public class ArithmeticNaNException : ArithmeticException
    {
        public ArithmeticNaNException()
        {
        }

        public ArithmeticNaNException(String? message)
            : base(message)
        {
        }

        public ArithmeticNaNException(String? message, Exception? exception)
            : base(message, exception)
        {
        }

#if NET8_0_OR_GREATER
        [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId="SYSLIB0051", UrlFormat="https://aka.ms/dotnet-warnings/{0}")]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
#endif
        protected ArithmeticNaNException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}