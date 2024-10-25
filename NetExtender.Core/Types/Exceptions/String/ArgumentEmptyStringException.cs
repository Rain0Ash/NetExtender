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

        public ArgumentEmptyStringException(String? message, Exception? exception)
            : base(message, exception)
        {
        }

        public ArgumentEmptyStringException(String? message, String? parameter)
            : base(message, parameter)
        {
        }

        public ArgumentEmptyStringException(String? message, String? parameter, Exception? exception)
            : base(message, parameter, exception)
        {
        }

        protected ArgumentEmptyStringException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}