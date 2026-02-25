using System;
using System.Runtime.Serialization;
using NetExtender.Exceptions.Interfaces;
using NetExtender.Utilities.Core;

namespace NetExtender.Exceptions
{
    [Serializable]
    public sealed class SerializationMissingValuesException<TSR> : SerializationMissingValuesException, ISRException<TSR, SerializationMissingValuesException<TSR>.Serialization_MissingValues>
    {
        public SerializationMissingValuesException()
            : base(ISRExceptionIdentifier<Serialization_MissingValues>.Instance.Resource)
        {
        }

        public SerializationMissingValuesException(Exception? exception)
            : base(ISRExceptionIdentifier<Serialization_MissingValues>.Instance.Resource, exception)
        {
        }

#if NET8_0_OR_GREATER
        [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId="SYSLIB0051", UrlFormat="https://aka.ms/dotnet-warnings/{0}")]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
#endif
        private SerializationMissingValuesException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        [ReflectionNaming]
        internal class Serialization_MissingValues : ISRExceptionIdentifier<TSR, Serialization_MissingValues>
        {
        }
    }

    [Serializable]
    public class SerializationMissingValuesException : SerializationException
    {
        public SerializationMissingValuesException()
        {
        }

        public SerializationMissingValuesException(String? message)
            : base(message)
        {
        }

        public SerializationMissingValuesException(String? message, Exception? exception)
            : base(message, exception)
        {
        }

#if NET8_0_OR_GREATER
        [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId="SYSLIB0051", UrlFormat="https://aka.ms/dotnet-warnings/{0}")]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
#endif
        protected SerializationMissingValuesException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}