// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Runtime.Serialization;
using JetBrains.Annotations;

namespace NetExtender.Exceptions
{
    [Serializable]
    public class CloneException : Exception
    {
        public CloneException()
        {
        }

        public CloneException([CanBeNull] String? message)
            : base(message)
        {
        }

        public CloneException([CanBeNull] String? message, [CanBeNull] Exception? innerException)
            : base(message, innerException)
        {
        }
        
        protected CloneException([NotNull] SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}