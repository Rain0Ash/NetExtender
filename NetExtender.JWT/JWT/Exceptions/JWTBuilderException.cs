// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.Serialization;
using NetExtender.Utilities.Types;

namespace NetExtender.JWT
{
    [Serializable]
    public class JWTBuilderInstantiateException : JWTBuilderException
    {
        private new const String Message = "Can't instantiate '{0}'.";
        private const String MethodMessage = "Can't instantiate '{0}'. Invoke '{1}'.";
        
        public JWTBuilderInstantiateException(String? message)
            : base(message)
        {
        }

        public JWTBuilderInstantiateException(String? message, Exception? exception)
            : base(message, exception)
        {
        }

        public JWTBuilderInstantiateException(Type type)
            : this(type, null)
        {
        }

        public JWTBuilderInstantiateException(Type type, String? method)
            : base(type is not null ? Format(type, method) : throw new ArgumentNullException(nameof(type)))
        {
        }

        protected JWTBuilderInstantiateException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        private static String Format(Type type, String? method)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
            return method is not null ? MethodMessage.Format(type.Name, method) : Message.Format(type);
        }
    }
    
    [Serializable]
    public class JWTBuilderEncodeException : JWTBuilderException
    {
        private new const String Message = "Can't encode token.";
        
        public JWTBuilderEncodeException(String? message)
            : base(message ?? Message)
        {
        }

        public JWTBuilderEncodeException(String? message, Exception? exception)
            : base(message ?? Message, exception)
        {
        }

        public JWTBuilderEncodeException(params String?[]? methods)
            : base(Message, methods)
        {
        }

        protected JWTBuilderEncodeException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
    
    [Serializable]
    public class JWTBuilderDecodeException : JWTBuilderException
    {
        private new const String Message = "Can't decode token.";
        
        public JWTBuilderDecodeException(String? message)
            : base(message ?? Message)
        {
        }

        public JWTBuilderDecodeException(String? message, Exception? exception)
            : base(message ?? Message, exception)
        {
        }

        public JWTBuilderDecodeException(params String?[]? methods)
            : base(Message, methods)
        {
        }

        protected JWTBuilderDecodeException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
    
    [Serializable]
    public class JWTBuilderException : JWTException
    {
        public JWTBuilderException(String? message)
            : base(message)
        {
        }

        public JWTBuilderException(String? message, Exception? exception)
            : base(message, exception)
        {
        }

        public JWTBuilderException(String? message, params String?[]? methods)
            : base(Format(message, methods))
        {
        }

        protected JWTBuilderException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        private static String? Format(String? message, params String?[]? methods)
        {
            String? method = methods is not null ? String.Join("; ", methods.WhereNot(String.IsNullOrEmpty)) : null;
            return method?.Length switch
            {
                > 0 when !String.IsNullOrEmpty(message) => $"{message} You must invoke next methods: ({method}).",
                > 0 => $"You must invoke next methods: ({method}).",
                _ when !String.IsNullOrEmpty(message) => message,
                _ => null
            };
        }
    }
}