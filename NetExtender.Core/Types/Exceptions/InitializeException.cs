// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.Serialization;

namespace NetExtender.Types.Exceptions
{
    [Serializable]
    public class AlreadyInitializedException : InitializeException
    {
        public AlreadyInitializedException()
        {
        }

        public AlreadyInitializedException(String? message)
            : base(message)
        {
        }

        public AlreadyInitializedException(String? message, Exception? innerException)
            : base(message, innerException)
        {
        }

        public AlreadyInitializedException(String? message, String? paramName)
            : base(message, paramName)
        {
        }

        public AlreadyInitializedException(String? message, String? paramName, Exception? innerException)
            : base(message, paramName, innerException)
        {
        }

        protected AlreadyInitializedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }

    [Serializable]
    public class NotInitializedException : InitializeException
    {
        public NotInitializedException()
        {
        }

        public NotInitializedException(String? message)
            : base(message)
        {
        }

        public NotInitializedException(String? message, Exception? innerException)
            : base(message, innerException)
        {
        }

        public NotInitializedException(String? message, String? paramName)
            : base(message, paramName)
        {
        }

        public NotInitializedException(String? message, String? paramName, Exception? innerException)
            : base(message, paramName, innerException)
        {
        }

        protected NotInitializedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
    
    [Serializable]
    public class InvalidInitializationException : InitializeException
    {
        public InvalidInitializationException()
        {
        }

        public InvalidInitializationException(String? message)
            : base(message)
        {
        }

        public InvalidInitializationException(String? message, Exception? innerException)
            : base(message, innerException)
        {
        }

        public InvalidInitializationException(String? message, String? paramName)
            : base(message, paramName)
        {
        }

        public InvalidInitializationException(String? message, String? paramName, Exception? innerException)
            : base(message, paramName, innerException)
        {
        }

        protected InvalidInitializationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
    
    [Serializable]
    public class InitializeException : ArgumentException
    {
        public InitializeException()
        {
        }

        public InitializeException(String? message)
            : base(message)
        {
        }

        public InitializeException(String? message, Exception? innerException)
            : base(message, innerException)
        {
        }

        public InitializeException(String? message, String? paramName)
            : base(message, paramName)
        {
        }

        public InitializeException(String? message, String? paramName, Exception? innerException)
            : base(message, paramName, innerException)
        {
        }
        
        protected InitializeException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}