using System;
using System.Runtime.Serialization;
using NetExtender.Types.Exceptions.Interfaces;
using NetExtender.Utilities.Core;

namespace NetExtender.Types.Exceptions
{
    [Serializable]
    public sealed class ArgumentOutOfRangeNeedNonNegativeNumberException<TSR> : ArgumentOutOfRangeNeedNonNegativeNumberException, ISRException<TSR, ArgumentOutOfRangeNeedNonNegativeNumberException<TSR>.ArgumentOutOfRange_NeedNonNegNum>
    {
        public ArgumentOutOfRangeNeedNonNegativeNumberException()
            : base(null, ISRExceptionIdentifier<ArgumentOutOfRange_NeedNonNegNum>.Instance.Resource)
        {
        }

        public ArgumentOutOfRangeNeedNonNegativeNumberException(String? parameter)
            : base(parameter, ISRExceptionIdentifier<ArgumentOutOfRange_NeedNonNegNum>.Instance.Resource)
        {
        }

        public ArgumentOutOfRangeNeedNonNegativeNumberException(Exception? exception)
            : base(ISRExceptionIdentifier<ArgumentOutOfRange_NeedNonNegNum>.Instance.Resource, exception)
        {
        }

        public ArgumentOutOfRangeNeedNonNegativeNumberException(String? parameter, Object? value)
            : base(parameter, value, ISRExceptionIdentifier<ArgumentOutOfRange_NeedNonNegNum>.Instance.Resource)
        {
        }

#if NET8_0_OR_GREATER
        [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId="SYSLIB0051", UrlFormat="https://aka.ms/dotnet-warnings/{0}")]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
#endif
        private ArgumentOutOfRangeNeedNonNegativeNumberException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        [ReflectionNaming]
        internal class ArgumentOutOfRange_NeedNonNegNum : ISRExceptionIdentifier<TSR, ArgumentOutOfRange_NeedNonNegNum>
        {
        }
    }

    [Serializable]
    public class ArgumentOutOfRangeNeedNonNegativeNumberException : ArgumentOutOfRangeException
    {
        public ArgumentOutOfRangeNeedNonNegativeNumberException()
        {
        }

        public ArgumentOutOfRangeNeedNonNegativeNumberException(String? parameter)
            : base(parameter)
        {
        }

        public ArgumentOutOfRangeNeedNonNegativeNumberException(String? parameter, String? message)
            : base(parameter, message)
        {
        }

        public ArgumentOutOfRangeNeedNonNegativeNumberException(String? message, Exception? exception)
            : base(message, exception)
        {
        }

        public ArgumentOutOfRangeNeedNonNegativeNumberException(String? parameter, Object? value, String? message)
            : base(parameter, value, message)
        {
        }

#if NET8_0_OR_GREATER
        [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId="SYSLIB0051", UrlFormat="https://aka.ms/dotnet-warnings/{0}")]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
#endif
        protected ArgumentOutOfRangeNeedNonNegativeNumberException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}