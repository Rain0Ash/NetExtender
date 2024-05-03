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

        public ArgumentWhiteSpaceStringException(String? message, Exception? innerException)
            : base(message, innerException)
        {
        }

        public ArgumentWhiteSpaceStringException(String? message, String? paramName)
            : base(message, paramName)
        {
        }

        public ArgumentWhiteSpaceStringException(String? message, String? paramName, Exception? innerException)
            : base(message, paramName, innerException)
        {
        }

        protected ArgumentWhiteSpaceStringException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}