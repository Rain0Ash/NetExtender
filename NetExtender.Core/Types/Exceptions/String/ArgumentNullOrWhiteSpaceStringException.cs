using System;

namespace NetExtender.Types.Exceptions
{
    public class ArgumentNullOrWhiteSpaceStringException : ExceptionWrapper<Exception>
    {
        public ArgumentNullOrWhiteSpaceStringException(String? value)
            : base(value is null ? new ArgumentNullException() : new ArgumentWhiteSpaceStringException())
        {
        }

        public ArgumentNullOrWhiteSpaceStringException(String? value, String? message)
            : base(value is null ? new ArgumentNullException(message) : new ArgumentWhiteSpaceStringException(message))
        {
        }

        public ArgumentNullOrWhiteSpaceStringException(String? value, String? message, Exception? innerException)
            : base(value is null ? new ArgumentNullException(message, innerException) : new ArgumentWhiteSpaceStringException(message, innerException))
        {
        }

        public ArgumentNullOrWhiteSpaceStringException(String? value, String? message, String? paramName)
            : base(value is null ? new ArgumentNullException(paramName, message) : new ArgumentWhiteSpaceStringException(message, paramName))
        {
        }

        public ArgumentNullOrWhiteSpaceStringException(String? value, String? message, String? paramName, Exception? innerException)
            : base(value is null ? new ArgumentNullException(message, innerException) : new ArgumentWhiteSpaceStringException(message, paramName, innerException))
        {
        }

        protected ArgumentNullOrWhiteSpaceStringException(Exception exception)
            : base(exception)
        {
        }
    }
}