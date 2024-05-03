using System;
using System.Runtime.Serialization;

namespace NetExtender.Types.Exceptions
{
    [Serializable]
    public class ArgumentNullStringException : ArgumentNullException
    {
        public ArgumentNullStringException()
        {
        }

        public ArgumentNullStringException(String? paramName)
            : base(paramName)
        {
        }

        public ArgumentNullStringException(String? message, Exception? innerException)
            : base(message, innerException)
        {
        }

        public ArgumentNullStringException(String? paramName, String? message)
            : base(paramName, message)
        {
        }

        protected ArgumentNullStringException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}