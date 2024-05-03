using System;

namespace NetExtender.Types.Exceptions
{
    public class ArgumentNullOrEmptyStringException : ExceptionWrapper<Exception>
    {
        public ArgumentNullOrEmptyStringException(String? value)
            : base(value is null ? new ArgumentNullException() : new ArgumentEmptyStringException())
        {
        }

        public ArgumentNullOrEmptyStringException(String? value, String? message)
            : base(value is null ? new ArgumentNullException(message) : new ArgumentEmptyStringException(message))
        {
        }

        public ArgumentNullOrEmptyStringException(String? value, String? message, Exception? innerException)
            : base(value is null ? new ArgumentNullException(message, innerException) : new ArgumentEmptyStringException(message, innerException))
        {
        }

        public ArgumentNullOrEmptyStringException(String? value, String? message, String? paramName)
            : base(value is null ? new ArgumentNullException(paramName, message) : new ArgumentEmptyStringException(message, paramName))
        {
        }

        public ArgumentNullOrEmptyStringException(String? value, String? message, String? paramName, Exception? innerException)
            : base(value is null ? new ArgumentNullException(message, innerException) : new ArgumentEmptyStringException(message, paramName, innerException))
        {
        }

        protected ArgumentNullOrEmptyStringException(Exception exception)
            : base(exception)
        {
        }
    }
}