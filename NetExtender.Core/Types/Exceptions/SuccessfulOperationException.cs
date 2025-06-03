// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.Serialization;

namespace NetExtender.Types.Exceptions
{
    [Serializable]
    public class SuccessfulOperationException : Exception
    {
        private new const String Message = "Successful operation.";
        
        public SuccessfulOperationException()
            : base(Message)
        {
        }
        
        public SuccessfulOperationException(String? message)
            : base(message ?? Message)
        {
        }
        
        public SuccessfulOperationException(Exception? exception)
            : base(Message, exception)
        {
        }
        
        public SuccessfulOperationException(String? message, Exception? exception)
            : base(message ?? Message, exception)
        {
        }
        
        protected SuccessfulOperationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}