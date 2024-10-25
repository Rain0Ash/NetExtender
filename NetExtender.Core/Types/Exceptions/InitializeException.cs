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

        public AlreadyInitializedException(String? message, Exception? exception)
            : base(message, exception)
        {
        }

        public AlreadyInitializedException(String? message, String? parameter)
            : base(message, parameter)
        {
        }

        public AlreadyInitializedException(String? message, String? parameter, Exception? exception)
            : base(message, parameter, exception)
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

        public NotInitializedException(String? message, Exception? exception)
            : base(message, exception)
        {
        }

        public NotInitializedException(String? message, String? parameter)
            : base(message, parameter)
        {
        }

        public NotInitializedException(String? message, String? parameter, Exception? exception)
            : base(message, parameter, exception)
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

        public InvalidInitializationException(String? message, Exception? exception)
            : base(message, exception)
        {
        }

        public InvalidInitializationException(String? message, String? parameter)
            : base(message, parameter)
        {
        }

        public InvalidInitializationException(String? message, String? parameter, Exception? exception)
            : base(message, parameter, exception)
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

        public InitializeException(String? message, Exception? exception)
            : base(message, exception)
        {
        }

        public InitializeException(String? message, String? parameter)
            : base(message, parameter)
        {
        }

        public InitializeException(String? message, String? parameter, Exception? exception)
            : base(message, parameter, exception)
        {
        }

        protected InitializeException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}