// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.Serialization;

namespace NetExtender.JWT
{
    [Serializable]
    public sealed class JWTFactoryException : JWTException
    {
        private new const String Message = "Can't create a new algorithm without a certificate factory, private key or public key.";
        
        public JWTFactoryException()
            : base(Message)
        {
        }

        public JWTFactoryException(String? message)
            : base(message ?? Message)
        {
        }

        public JWTFactoryException(String? message, Exception? exception)
            : base(message ?? Message, exception)
        {
        }

        public JWTFactoryException(String message, params String[] methods)
            : base($"{message}")
        {
        }

        private JWTFactoryException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}