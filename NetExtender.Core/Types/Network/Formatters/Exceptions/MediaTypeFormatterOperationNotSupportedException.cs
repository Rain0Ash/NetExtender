// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.Serialization;

namespace NetExtender.Types.Network.Formatters.Exceptions
{
    [Serializable]
    public class MediaTypeFormatterOperationNotSupportedException : NotSupportedException
    {
        private new const String Message = "Media type formatter operation not supported.";
        
        public MediaTypeFormatterOperationNotSupportedException()
            : base(Message)
        {
        }
        
        public MediaTypeFormatterOperationNotSupportedException(String? message)
            : base(message ?? Message)
        {
        }
        
        public MediaTypeFormatterOperationNotSupportedException(String? message, Exception? exception)
            : base(message ?? Message, exception)
        {
        }
        
        protected MediaTypeFormatterOperationNotSupportedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}