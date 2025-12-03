using System;
using System.Runtime.Serialization;
using NetExtender.Types.Exceptions.Interfaces;
using NetExtender.Utilities.Core;

namespace NetExtender.Types.Exceptions
{
    [Serializable]
    public sealed class FormatInvalidStringException<TSR> : FormatInvalidStringException, ISRException<TSR, FormatInvalidStringException<TSR>.Format_InvalidString>
    {
        public FormatInvalidStringException()
            : base(ISRExceptionIdentifier<Format_InvalidString>.Instance.Resource)
        {
        }

        public FormatInvalidStringException(Exception? exception)
            : base(ISRExceptionIdentifier<Format_InvalidString>.Instance.Resource, exception)
        {
        }

#if NET8_0_OR_GREATER
        [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId="SYSLIB0051", UrlFormat="https://aka.ms/dotnet-warnings/{0}")]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
#endif
        private FormatInvalidStringException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        [ReflectionNaming]
        internal class Format_InvalidString : ISRExceptionIdentifier<TSR, Format_InvalidString>
        {
        }
    }

    [Serializable]
    public class FormatInvalidStringException : FormatException
    {
        public FormatInvalidStringException()
        {
        }

        public FormatInvalidStringException(String? message)
            : base(message)
        {
        }

        public FormatInvalidStringException(String? message, Exception? exception)
            : base(message, exception)
        {
        }

#if NET8_0_OR_GREATER
        [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId="SYSLIB0051", UrlFormat="https://aka.ms/dotnet-warnings/{0}")]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
#endif
        protected FormatInvalidStringException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}