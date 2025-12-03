using System;
using System.Runtime.Serialization;
using NetExtender.Types.Exceptions.Interfaces;
using NetExtender.Utilities.Core;

namespace NetExtender.Types.Exceptions
{
    [Serializable]
    public sealed class ArgumentOutOfRangeBiggerThanCollectionException<TSR> : ArgumentOutOfRangeBiggerThanCollectionException, ISRException<TSR, ArgumentOutOfRangeBiggerThanCollectionException<TSR>.ArgumentOutOfRange_BiggerThanCollection>
    {
        public ArgumentOutOfRangeBiggerThanCollectionException()
            : base(null, ISRExceptionIdentifier<ArgumentOutOfRange_BiggerThanCollection>.Instance.Resource)
        {
        }

        public ArgumentOutOfRangeBiggerThanCollectionException(String? parameter)
            : base(parameter, ISRExceptionIdentifier<ArgumentOutOfRange_BiggerThanCollection>.Instance.Resource)
        {
        }

        public ArgumentOutOfRangeBiggerThanCollectionException(Exception? exception)
            : base(ISRExceptionIdentifier<ArgumentOutOfRange_BiggerThanCollection>.Instance.Resource, exception)
        {
        }

        public ArgumentOutOfRangeBiggerThanCollectionException(String? parameter, Object? value)
            : base(parameter, value, ISRExceptionIdentifier<ArgumentOutOfRange_BiggerThanCollection>.Instance.Resource)
        {
        }

#if NET8_0_OR_GREATER
        [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId="SYSLIB0051", UrlFormat="https://aka.ms/dotnet-warnings/{0}")]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
#endif
        private ArgumentOutOfRangeBiggerThanCollectionException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        [ReflectionNaming]
        internal class ArgumentOutOfRange_BiggerThanCollection : ISRExceptionIdentifier<TSR, ArgumentOutOfRange_BiggerThanCollection>
        {
        }
    }

    [Serializable]
    public class ArgumentOutOfRangeBiggerThanCollectionException : ArgumentOutOfRangeException
    {
        public ArgumentOutOfRangeBiggerThanCollectionException()
        {
        }

        public ArgumentOutOfRangeBiggerThanCollectionException(String? parameter)
            : base(parameter)
        {
        }

        public ArgumentOutOfRangeBiggerThanCollectionException(String? parameter, String? message)
            : base(parameter, message)
        {
        }

        public ArgumentOutOfRangeBiggerThanCollectionException(String? message, Exception? exception)
            : base(message, exception)
        {
        }

        public ArgumentOutOfRangeBiggerThanCollectionException(String? parameter, Object? value, String? message)
            : base(parameter, value, message)
        {
        }

#if NET8_0_OR_GREATER
        [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId="SYSLIB0051", UrlFormat="https://aka.ms/dotnet-warnings/{0}")]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
#endif
        protected ArgumentOutOfRangeBiggerThanCollectionException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}