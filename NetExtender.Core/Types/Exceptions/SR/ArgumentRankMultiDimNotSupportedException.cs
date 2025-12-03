using System;
using System.Runtime.Serialization;
using NetExtender.Types.Exceptions.Interfaces;
using NetExtender.Utilities.Core;

namespace NetExtender.Types.Exceptions
{
    [Serializable]
    public sealed class ArgumentRankMultiDimNotSupportedException<TSR> : ArgumentRankMultiDimNotSupportedException, ISRException<TSR, ArgumentRankMultiDimNotSupportedException<TSR>.Arg_RankMultiDimNotSupported>
    {
        public ArgumentRankMultiDimNotSupportedException()
            : base(ISRExceptionIdentifier<Arg_RankMultiDimNotSupported>.Instance.Resource)
        {
        }

        public ArgumentRankMultiDimNotSupportedException(Exception? exception)
            : base(ISRExceptionIdentifier<Arg_RankMultiDimNotSupported>.Instance.Resource, exception)
        {
        }

        public ArgumentRankMultiDimNotSupportedException(String? parameter)
            : base(ISRExceptionIdentifier<Arg_RankMultiDimNotSupported>.Instance.Resource, parameter)
        {
        }

        public ArgumentRankMultiDimNotSupportedException(String? parameter, Exception? exception)
            : base(ISRExceptionIdentifier<Arg_RankMultiDimNotSupported>.Instance.Resource, parameter, exception)
        {
        }

#if NET8_0_OR_GREATER
        [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId="SYSLIB0051", UrlFormat="https://aka.ms/dotnet-warnings/{0}")]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
#endif
        private ArgumentRankMultiDimNotSupportedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        [ReflectionNaming]
        internal class Arg_RankMultiDimNotSupported : ISRExceptionIdentifier<TSR, Arg_RankMultiDimNotSupported>
        {
        }
    }

    [Serializable]
    public class ArgumentRankMultiDimNotSupportedException : ArgumentException
    {
        public ArgumentRankMultiDimNotSupportedException()
        {
        }

        public ArgumentRankMultiDimNotSupportedException(String? message)
            : base(message)
        {
        }

        public ArgumentRankMultiDimNotSupportedException(String? message, Exception? exception)
            : base(message, exception)
        {
        }

        public ArgumentRankMultiDimNotSupportedException(String? message, String? parameter)
            : base(message, parameter)
        {
        }

        public ArgumentRankMultiDimNotSupportedException(String? message, String? parameter, Exception? exception)
            : base(message, parameter, exception)
        {
        }

#if NET8_0_OR_GREATER
        [Obsolete("This API supports obsolete formatter-based serialization. It should not be called or extended by application code.", DiagnosticId="SYSLIB0051", UrlFormat="https://aka.ms/dotnet-warnings/{0}")]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
#endif
        protected ArgumentRankMultiDimNotSupportedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}