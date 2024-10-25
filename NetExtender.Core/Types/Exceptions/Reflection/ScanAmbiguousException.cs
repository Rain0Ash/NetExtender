using System;
using System.Runtime.Serialization;

namespace NetExtender.Types.Exceptions
{
    [Serializable]
    public class ScanAmbiguousException : ScanOperationException
    {
        public ScanAmbiguousException()
        {
        }
        
        public ScanAmbiguousException(String? message)
            : base(message)
        {
        }
        
        public ScanAmbiguousException(String? message, Exception? exception)
            : base(message, exception)
        {
        }
        
        protected ScanAmbiguousException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}