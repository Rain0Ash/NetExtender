// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace NetExtender.Types.Exceptions
{
    public class ArgumentNullOrWhiteSpaceStringException : ExceptionWrapper<Exception>
    {
        public ArgumentNullOrWhiteSpaceStringException(String? value)
            : base(value is null ? new ArgumentNullException() : value.Length <= 0 ? new ArgumentEmptyStringException() : new ArgumentWhiteSpaceStringException())
        {
        }

        public ArgumentNullOrWhiteSpaceStringException(String? value, String? parameter)
            : base(value is null ? new ArgumentNullException(parameter) : value.Length <= 0 ? new ArgumentEmptyStringException(parameter) : new ArgumentWhiteSpaceStringException(parameter))
        {
        }

        public ArgumentNullOrWhiteSpaceStringException(String? value, String? message, Exception? exception)
            : base(value is null ? new ArgumentNullException(message, exception) : value.Length <= 0 ? new ArgumentEmptyStringException(null, message, exception) : new ArgumentWhiteSpaceStringException(null, message, exception))
        {
        }

        public ArgumentNullOrWhiteSpaceStringException(String? value, String? parameter, String? message)
            : base(value is null ? new ArgumentNullException(parameter, message) : value.Length <= 0 ? new ArgumentEmptyStringException(parameter, message) : new ArgumentWhiteSpaceStringException(parameter, message))
        {
        }

        public ArgumentNullOrWhiteSpaceStringException(String? value, String? parameter, String? message, Exception? exception)
            : base(value is null ? message is not null || exception is not null ? new ArgumentNullException(message, exception) : new ArgumentNullException(parameter) : value.Length <= 0 ? new ArgumentEmptyStringException(parameter, message, exception) : new ArgumentWhiteSpaceStringException(parameter, message, exception))
        {
        }

        protected ArgumentNullOrWhiteSpaceStringException(Exception exception)
            : base(exception)
        {
        }
    }
}