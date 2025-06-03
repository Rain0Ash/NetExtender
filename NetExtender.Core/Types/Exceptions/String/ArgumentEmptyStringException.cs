// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.Serialization;

namespace NetExtender.Types.Exceptions
{
    [Serializable]
    public class ArgumentEmptyStringException : ArgumentException
    {
        private new const String Message = "Value cannot be empty.";
        
        public ArgumentEmptyStringException()
            : base(Message)
        {
        }

        public ArgumentEmptyStringException(String? parameter)
            : base(Message, parameter)
        {
        }

        public ArgumentEmptyStringException(String? parameter, Exception? exception)
            : base(Message, parameter, exception)
        {
        }

        public ArgumentEmptyStringException(String? parameter, String? message)
            : base(message ?? Message, parameter)
        {
        }

        public ArgumentEmptyStringException(String? parameter, String? message, Exception? exception)
            : base(message ?? Message, parameter, exception)
        {
        }

        protected ArgumentEmptyStringException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}