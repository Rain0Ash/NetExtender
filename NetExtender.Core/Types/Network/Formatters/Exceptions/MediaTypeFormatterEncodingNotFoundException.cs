// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.Serialization;

namespace NetExtender.Types.Network.Formatters.Exceptions
{
    [Serializable]
    public class MediaTypeFormatterEncodingNotFoundException : InvalidOperationException
    {
        private new const String Message = "Media type formatter encoding not found.";
        
        public MediaTypeFormatterEncodingNotFoundException()
            : base(Message)
        {
        }
        
        public MediaTypeFormatterEncodingNotFoundException(String? message)
            : base(message ?? Message)
        {
        }
        
        public MediaTypeFormatterEncodingNotFoundException(String? message, Exception? exception)
            : base(message ?? Message, exception)
        {
        }
        
        protected MediaTypeFormatterEncodingNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}