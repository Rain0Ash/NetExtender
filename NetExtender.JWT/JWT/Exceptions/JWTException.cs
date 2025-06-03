// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.Serialization;

namespace NetExtender.JWT
{
    [Serializable]
    public abstract class JWTException : Exception
    {
        protected JWTException()
        {
        }

        protected JWTException(String? message)
            : base(message)
        {
        }

        protected JWTException(String? message, Exception? exception)
            : base(message, exception)
        {
        }

        protected JWTException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}