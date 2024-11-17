using System;

namespace NetExtender.Types.Exceptions
{
    public class ArgumentNullOrEmptyStringException : ExceptionWrapper<Exception>
    {
        public ArgumentNullOrEmptyStringException(String? value)
            : base(value is null ? new ArgumentNullException() : new ArgumentEmptyStringException())
        {
        }

        public ArgumentNullOrEmptyStringException(String? value, String? parameter)
            : base(value is null ? new ArgumentNullException(parameter) : new ArgumentEmptyStringException(parameter))
        {
        }

        public ArgumentNullOrEmptyStringException(String? value, String? message, Exception? exception)
            : base(value is null ? new ArgumentNullException(message, exception) : new ArgumentEmptyStringException(null, message, exception))
        {
        }

        public ArgumentNullOrEmptyStringException(String? value, String? parameter, String? message)
            : base(value is null ? new ArgumentNullException(parameter, message) : new ArgumentEmptyStringException(parameter, message))
        {
        }

        public ArgumentNullOrEmptyStringException(String? value, String? parameter, String? message, Exception? exception)
            : base(value is null ? message is not null || exception is not null ? new ArgumentNullException(message, exception) : new ArgumentNullException(parameter) : new ArgumentEmptyStringException(parameter, message, exception))
        {
        }

        protected ArgumentNullOrEmptyStringException(Exception exception)
            : base(exception)
        {
        }
    }
}