// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.Serialization;

namespace NetExtender.WindowsPresentation.ActiveBinding
{
    [Serializable]
    public class ActiveBindingException : Exception
    {
        public ActiveBindingException()
        {
        }

        public ActiveBindingException(String message)
            : base(message)
        {
        }

        public ActiveBindingException(String message, Exception inner)
            : base(message, inner)
        {
        }

        protected ActiveBindingException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}