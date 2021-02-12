// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.Serialization;
using JetBrains.Annotations;

namespace NetExtender.Exceptions
{
    [Serializable]
    public class AlreadyInitializedException : InitializeException
    {
        public AlreadyInitializedException()
        {
        }

        public AlreadyInitializedException([CanBeNull] String? message)
            : base(message)
        {
        }

        public AlreadyInitializedException([CanBeNull] String? message, [CanBeNull] Exception? innerException)
            : base(message, innerException)
        {
        }

        public AlreadyInitializedException([CanBeNull] String? message, [CanBeNull] String? paramName)
            : base(message, paramName)
        {
        }

        public AlreadyInitializedException([CanBeNull] String? message, [CanBeNull] String? paramName, [CanBeNull] Exception? innerException)
            : base(message, paramName, innerException)
        {
        }

        protected AlreadyInitializedException([NotNull] SerializationInfo info, StreamingContext context)
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

        public NotInitializedException([CanBeNull] String? message)
            : base(message)
        {
        }

        public NotInitializedException([CanBeNull] String? message, [CanBeNull] Exception? innerException)
            : base(message, innerException)
        {
        }

        public NotInitializedException([CanBeNull] String? message, [CanBeNull] String? paramName)
            : base(message, paramName)
        {
        }

        public NotInitializedException([CanBeNull] String? message, [CanBeNull] String? paramName, [CanBeNull] Exception? innerException)
            : base(message, paramName, innerException)
        {
        }

        protected NotInitializedException([NotNull] SerializationInfo info, StreamingContext context)
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

        public InitializeException([CanBeNull] String? message)
            : base(message)
        {
        }

        public InitializeException([CanBeNull] String? message, [CanBeNull] Exception? innerException)
            : base(message, innerException)
        {
        }

        public InitializeException([CanBeNull] String? message, [CanBeNull] String? paramName)
            : base(message, paramName)
        {
        }

        public InitializeException([CanBeNull] String? message, [CanBeNull] String? paramName, [CanBeNull] Exception? innerException)
            : base(message, paramName, innerException)
        {
        }
        
        protected InitializeException([NotNull] SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}