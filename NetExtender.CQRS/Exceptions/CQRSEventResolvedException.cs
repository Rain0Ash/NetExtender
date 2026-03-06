using System;
using System.Runtime.Serialization;

namespace NetExtender.CQRS.Exceptions
{
    [Serializable]
    public class CQRSEventResolvedException : InvalidOperationException
    {
        public CQRSEventResolvedException()
        {
        }

        public CQRSEventResolvedException(String? message)
            : base(message)
        {
        }

        public CQRSEventResolvedException(String? message, Exception? exception)
            : base(message, exception)
        {
        }

        protected CQRSEventResolvedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}