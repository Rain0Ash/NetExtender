using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace NetExtender.Initializer.CQRS.Exceptions
{
    [Serializable]
    public class CQRSEventNotifyException : AggregateException
    {
        public CQRSEventNotifyException()
        {
        }

        public CQRSEventNotifyException(Exception exception)
            : base((String?) null, exception)
        {
        }

        public CQRSEventNotifyException(IEnumerable<Exception> exceptions)
            : base(exceptions)
        {
        }

        public CQRSEventNotifyException(params Exception[] exceptions)
            : base(exceptions)
        {
        }

        public CQRSEventNotifyException(String? message)
            : base(message)
        {
        }

        public CQRSEventNotifyException(String? message, Exception exception)
            : base(message, exception)
        {
        }

        public CQRSEventNotifyException(String? message, IEnumerable<Exception> exceptions)
            : base(message, exceptions)
        {
        }

        public CQRSEventNotifyException(String? message, params Exception[] exceptions)
            : base(message, exceptions)
        {
        }

        protected CQRSEventNotifyException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}