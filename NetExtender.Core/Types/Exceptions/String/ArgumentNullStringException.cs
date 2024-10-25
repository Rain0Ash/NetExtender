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

        public ArgumentNullStringException(String? parameter)
            : base(parameter)
        {
        }

        public ArgumentNullStringException(String? message, Exception? exception)
            : base(message, exception)
        {
        }

        public ArgumentNullStringException(String? parameter, String? message)
            : base(parameter, message)
        {
        }

        protected ArgumentNullStringException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}