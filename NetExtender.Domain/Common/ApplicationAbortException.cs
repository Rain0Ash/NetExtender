// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.Serialization;
using NetExtender.Types.Exceptions;

namespace NetExtender.Domains
{
    [Serializable]
    public sealed class SuccessfulApplicationAbortException : SuccessfulOperationException
    {
        public SuccessfulApplicationAbortException()
        {
        }

        public SuccessfulApplicationAbortException(String? message)
            : base(message)
        {
        }

        public SuccessfulApplicationAbortException(Exception? exception)
            : base(exception)
        {
        }

        public SuccessfulApplicationAbortException(String? message, Exception? exception)
            : base(message, exception)
        {
        }

        private SuccessfulApplicationAbortException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
    
    [Serializable]
    public sealed class ApplicationAbortException : InvalidOperationException
    {
        public ApplicationAbortException()
        {
        }

        public ApplicationAbortException(String? message)
            : base(message)
        {
        }

        public ApplicationAbortException(Exception? exception)
            : base(null, exception)
        {
        }

        public ApplicationAbortException(String? message, Exception? exception)
            : base(message, exception)
        {
        }

        private ApplicationAbortException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}