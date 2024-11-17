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