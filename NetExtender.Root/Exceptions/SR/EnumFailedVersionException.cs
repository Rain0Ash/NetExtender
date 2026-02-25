using System;
using System.Runtime.Serialization;
using NetExtender.Exceptions.Interfaces;
using NetExtender.Utilities.Core;

namespace NetExtender.Exceptions
{
    [Serializable]
    public sealed class EnumFailedVersionException<TSR> : EnumFailedVersionException, ISRException<TSR, EnumFailedVersionException<TSR>.InvalidOperation_EnumFailedVersion>
    {
        public EnumFailedVersionException()
            : base(ISRExceptionIdentifier<InvalidOperation_EnumFailedVersion>.Instance.Resource)
        {
        }

        public EnumFailedVersionException(Exception? exception)
            : base(ISRExceptionIdentifier<InvalidOperation_EnumFailedVersion>.Instance.Resource, exception)
        {
        }

#if NET8_0_OR_GREATER
        [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId="SYSLIB0051", UrlFormat="https://aka.ms/dotnet-warnings/{0}")]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
#endif
        private EnumFailedVersionException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        [ReflectionNaming]
        internal class InvalidOperation_EnumFailedVersion : ISRExceptionIdentifier<TSR, InvalidOperation_EnumFailedVersion>
        {
        }
    }

    [Serializable]
    public class EnumFailedVersionException : InvalidOperationException
    {
        public EnumFailedVersionException()
        {
        }

        public EnumFailedVersionException(String? message)
            : base(message)
        {
        }

        public EnumFailedVersionException(String? message, Exception? exception)
            : base(message, exception)
        {
        }

#if NET8_0_OR_GREATER
        [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId="SYSLIB0051", UrlFormat="https://aka.ms/dotnet-warnings/{0}")]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
#endif
        protected EnumFailedVersionException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}