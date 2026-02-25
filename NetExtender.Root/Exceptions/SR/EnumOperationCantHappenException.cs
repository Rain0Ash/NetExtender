using System;
using System.Runtime.Serialization;
using NetExtender.Exceptions.Interfaces;
using NetExtender.Utilities.Core;

namespace NetExtender.Exceptions
{
    [Serializable]
    public sealed class EnumOperationCantHappenException<TSR> : EnumOperationCantHappenException, ISRException<TSR, EnumOperationCantHappenException<TSR>.InvalidOperation_EnumOpCantHappen>
    {
        public EnumOperationCantHappenException()
            : base(ISRExceptionIdentifier<InvalidOperation_EnumOpCantHappen>.Instance.Resource)
        {
        }

        public EnumOperationCantHappenException(Exception? exception)
            : base(ISRExceptionIdentifier<InvalidOperation_EnumOpCantHappen>.Instance.Resource, exception)
        {
        }

#if NET8_0_OR_GREATER
        [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId="SYSLIB0051", UrlFormat="https://aka.ms/dotnet-warnings/{0}")]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
#endif
        private EnumOperationCantHappenException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        [ReflectionNaming]
        internal class InvalidOperation_EnumOpCantHappen : ISRExceptionIdentifier<TSR, InvalidOperation_EnumOpCantHappen>
        {
        }
    }

    [Serializable]
    public class EnumOperationCantHappenException : InvalidOperationException
    {
        public EnumOperationCantHappenException()
        {
        }

        public EnumOperationCantHappenException(String? message)
            : base(message)
        {
        }

        public EnumOperationCantHappenException(String? message, Exception? exception)
            : base(message, exception)
        {
        }

#if NET8_0_OR_GREATER
        [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId="SYSLIB0051", UrlFormat="https://aka.ms/dotnet-warnings/{0}")]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
#endif
        protected EnumOperationCantHappenException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}