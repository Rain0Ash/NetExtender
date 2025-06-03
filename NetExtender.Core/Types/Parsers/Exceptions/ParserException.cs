// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.Serialization;

namespace NetExtender.Types.Parsers
{
    [Serializable]
    public class ParserException : InvalidOperationException
    {
        public ParserException()
        {
        }
        
        public ParserException(String? message)
            : base(message)
        {
        }
        
        public ParserException(String? message, Exception? exception)
            : base(message, exception)
        {
        }
        
        protected ParserException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}