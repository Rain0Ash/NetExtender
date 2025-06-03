// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.Serialization;

namespace NetExtender.Types.Exceptions
{
    [Serializable]
    public class ScanOperationException : ReflectionOperationException
    {
        private new const String Message = "Scanning operation exception.";
        
        public ScanOperationException()
            : base(Message)
        {
        }
        
        public ScanOperationException(String? message)
            : base(message ?? Message)
        {
        }
        
        public ScanOperationException(String? message, Exception? exception)
            : base(message ?? Message, exception)
        {
        }
        
        protected ScanOperationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}