using System;
using System.Runtime.Serialization;

namespace NetExtender.Types.Exceptions
{
    [Serializable]
    public class ArgumentEmptyStringException : ArgumentException
    {
        public ArgumentEmptyStringException()
        {
        }

        public ArgumentEmptyStringException(String? message)
            : base(message)
        {
        }

        public ArgumentEmptyStringException(String? message, Exception? innerException)
            : base(message, innerException)
        {
        }

        public ArgumentEmptyStringException(String? message, String? paramName)
            : base(message, paramName)
        {
        }

        public ArgumentEmptyStringException(String? message, String? paramName, Exception? innerException)
            : base(message, paramName, innerException)
        {
        }

        protected ArgumentEmptyStringException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}