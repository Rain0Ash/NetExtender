using System;

namespace NetExtender.Types.Exceptions
{
    public class ArgumentNullOrWhiteSpaceStringException : ExceptionWrapper<Exception>
    {
        public ArgumentNullOrWhiteSpaceStringException(String? value)
            : base(value is null ? new ArgumentNullException() : new ArgumentWhiteSpaceStringException())
        {
        }

        public ArgumentNullOrWhiteSpaceStringException(String? value, String? parameter)
            : base(value is null ? new ArgumentNullException(parameter) : new ArgumentWhiteSpaceStringException(parameter))
        {
        }

        public ArgumentNullOrWhiteSpaceStringException(String? value, String? message, Exception? exception)
            : base(value is null ? new ArgumentNullException(message, exception) : new ArgumentWhiteSpaceStringException(null, message, exception))
        {
        }

        public ArgumentNullOrWhiteSpaceStringException(String? value, String? parameter, String? message)
            : base(value is null ? new ArgumentNullException(parameter, message) : new ArgumentWhiteSpaceStringException(parameter, message))
        {
        }

        public ArgumentNullOrWhiteSpaceStringException(String? value, String? parameter, String? message, Exception? exception)
            : base(value is null ? message is not null || exception is not null ? new ArgumentNullException(message, exception) : new ArgumentNullException(parameter) : new ArgumentWhiteSpaceStringException(parameter, message, exception))
        {
        }

        protected ArgumentNullOrWhiteSpaceStringException(Exception exception)
            : base(exception)
        {
        }
    }
}