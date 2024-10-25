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

        public ArgumentNullOrWhiteSpaceStringException(String? value, String? message, Exception? exception)
            : base(value is null ? new ArgumentNullException(message, exception) : new ArgumentWhiteSpaceStringException(message, exception))
        {
        }

        public ArgumentNullOrWhiteSpaceStringException(String? value, String? message, String? parameter)
            : base(value is null ? new ArgumentNullException(parameter, message) : new ArgumentWhiteSpaceStringException(message, parameter))
        {
        }

        public ArgumentNullOrWhiteSpaceStringException(String? value, String? message, String? parameter, Exception? exception)
            : base(value is null ? new ArgumentNullException(message, exception) : new ArgumentWhiteSpaceStringException(message, parameter, exception))
        {
        }

        protected ArgumentNullOrWhiteSpaceStringException(Exception exception)
            : base(exception)
        {
        }
    }
}