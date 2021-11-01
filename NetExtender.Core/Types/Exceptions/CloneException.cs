// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.Serialization;

namespace NetExtender.Types.Exceptions
{
    [Serializable]
    public class CloneException : Exception
    {
        public CloneException()
        {
        }

        public CloneException(String? message)
            : base(message)
        {
        }

        public CloneException(String? message, Exception? innerException)
            : base(message, innerException)
        {
        }
        
        protected CloneException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}