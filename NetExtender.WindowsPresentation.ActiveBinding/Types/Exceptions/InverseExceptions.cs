// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.Serialization;

namespace NetExtender.WindowsPresentation.ActiveBinding
{
    [Serializable]
    public class InverseException : ActiveBindingException
    {
        public InverseException()
        {
        }

        public InverseException(String message)
            : base(message)
        {
        }

        public InverseException(String message, Exception inner)
            : base(message, inner)
        {
        }

        protected InverseException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}