using System;
using System.IO;
using System.Runtime.Serialization;
using NetExtender.Exceptions.Interfaces;
using NetExtender.Utilities.Core;

namespace NetExtender.Exceptions
{
    [Serializable]
    public sealed class StreamReadBeyondEOFException<TSR> : FormatInvalidStringException, ISRException<TSR, StreamReadBeyondEOFException<TSR>.IO_EOF_ReadBeyondEOF>
    {
        public StreamReadBeyondEOFException()
            : base(ISRExceptionIdentifier<IO_EOF_ReadBeyondEOF>.Instance.Resource)
        {
        }

        public StreamReadBeyondEOFException(Exception? exception)
            : base(ISRExceptionIdentifier<IO_EOF_ReadBeyondEOF>.Instance.Resource, exception)
        {
        }

#if NET8_0_OR_GREATER
        [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId="SYSLIB0051", UrlFormat="https://aka.ms/dotnet-warnings/{0}")]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
#endif
        private StreamReadBeyondEOFException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        [ReflectionNaming]
        internal class IO_EOF_ReadBeyondEOF : ISRExceptionIdentifier<TSR, IO_EOF_ReadBeyondEOF>
        {
        }
    }

    [Serializable]
    public class StreamReadBeyondEOFException : EndOfStreamException
    {
        public StreamReadBeyondEOFException()
        {
        }

        public StreamReadBeyondEOFException(String? message)
            : base(message)
        {
        }

        public StreamReadBeyondEOFException(String? message, Exception? exception)
            : base(message, exception)
        {
        }

#if NET8_0_OR_GREATER
        [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId="SYSLIB0051", UrlFormat="https://aka.ms/dotnet-warnings/{0}")]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
#endif
        protected StreamReadBeyondEOFException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}