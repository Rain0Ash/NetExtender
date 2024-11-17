using System;
using System.Runtime.Serialization;

namespace NetExtender.Types.Exceptions
{
    [Serializable]
    public class ArgumentWhiteSpaceStringException : ArgumentException
    {
        private new const String Message = "Value cannot be whitespace.";
        
        public ArgumentWhiteSpaceStringException()
            : base(Message)
        {
        }

        public ArgumentWhiteSpaceStringException(String? parameter)
            : base(Message, parameter)
        {
        }

        public ArgumentWhiteSpaceStringException(String? parameter, Exception? exception)
            : base(Message, parameter, exception)
        {
        }

        public ArgumentWhiteSpaceStringException(String? parameter, String? message)
            : base(message, parameter)
        {
        }

        public ArgumentWhiteSpaceStringException(String? parameter, String? message, Exception? exception)
            : base(message, parameter, exception)
        {
        }

        protected ArgumentWhiteSpaceStringException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}