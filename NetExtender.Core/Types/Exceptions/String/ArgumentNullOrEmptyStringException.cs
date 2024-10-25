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

        public ArgumentNullOrEmptyStringException(String? value, String? message, Exception? exception)
            : base(value is null ? new ArgumentNullException(message, exception) : new ArgumentEmptyStringException(message, exception))
        {
        }

        public ArgumentNullOrEmptyStringException(String? value, String? message, String? parameter)
            : base(value is null ? new ArgumentNullException(parameter, message) : new ArgumentEmptyStringException(message, parameter))
        {
        }

        public ArgumentNullOrEmptyStringException(String? value, String? message, String? parameter, Exception? exception)
            : base(value is null ? new ArgumentNullException(message, exception) : new ArgumentEmptyStringException(message, parameter, exception))
        {
        }

        protected ArgumentNullOrEmptyStringException(Exception exception)
            : base(exception)
        {
        }
    }
}