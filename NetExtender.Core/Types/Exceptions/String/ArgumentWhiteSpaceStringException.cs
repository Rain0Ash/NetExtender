using System;
using System.Runtime.Serialization;

namespace NetExtender.Types.Exceptions
{
    [Serializable]
    public class ArgumentWhiteSpaceStringException : ArgumentException
    {
        public ArgumentWhiteSpaceStringException()
        {
        }

        public ArgumentWhiteSpaceStringException(String? message)
            : base(message)
        {
        }

        public ArgumentWhiteSpaceStringException(String? message, Exception? exception)
            : base(message, exception)
        {
        }

        public ArgumentWhiteSpaceStringException(String? message, String? parameter)
            : base(message, parameter)
        {
        }

        public ArgumentWhiteSpaceStringException(String? message, String? parameter, Exception? exception)
            : base(message, parameter, exception)
        {
        }

        protected ArgumentWhiteSpaceStringException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}