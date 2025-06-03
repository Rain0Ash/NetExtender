// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.Serialization;

namespace NetExtender.Types.Exceptions
{
    [Serializable]
    public class ImplicitImplementationNotSupportedException : NotSupportedException
    {
        public ImplicitImplementationNotSupportedException()
        {
        }
        
        public ImplicitImplementationNotSupportedException(String? message)
            : base(message)
        {
        }
        
        public ImplicitImplementationNotSupportedException(String? message, Exception? exception)
            : base(message, exception)
        {
        }
        
        protected ImplicitImplementationNotSupportedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}